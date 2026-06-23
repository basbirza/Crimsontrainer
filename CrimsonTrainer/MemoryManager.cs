using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CrimsonTrainer
{
    public class MemoryManager : IDisposable
    {
        // ── Windows API ──────────────────────────────────────────────────────
        private const uint PROCESS_ALL_ACCESS    = 0x001F0FFF;
        private const uint MEM_COMMIT            = 0x1000;
        private const uint PAGE_NOACCESS         = 0x01;
        private const uint PAGE_GUARD            = 0x100;

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress,
            byte[] lpBuffer, int nSize, out int lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress,
            byte[] lpBuffer, int nSize, out int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll")]
        private static extern int VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress,
            out MEMORY_BASIC_INFORMATION lpBuffer, uint dwLength);

        [StructLayout(LayoutKind.Sequential)]
        private struct MEMORY_BASIC_INFORMATION
        {
            public IntPtr  BaseAddress;
            public IntPtr  AllocationBase;
            public uint    AllocationProtect;
            public IntPtr  RegionSize;
            public uint    State;
            public uint    Protect;
            public uint    Type;
        }

        // ── State ────────────────────────────────────────────────────────────
        private IntPtr  _processHandle = IntPtr.Zero;
        private Process? _process;
        private bool    _disposed;

        public bool IsAttached => _processHandle != IntPtr.Zero
                               && _process != null
                               && !_process.HasExited;

        // ── Attach / Detach ──────────────────────────────────────────────────

        public bool Attach(string processName)
        {
            Detach();
            var processes = Process.GetProcessesByName(processName);
            if (processes.Length == 0) return false;

            _process = processes[0];
            for (int i = 1; i < processes.Length; i++) processes[i].Dispose();

            _processHandle = OpenProcess(PROCESS_ALL_ACCESS, false, _process.Id);
            if (_processHandle == IntPtr.Zero)
            {
                _process.Dispose();
                _process = null;
                return false;
            }
            return true;
        }

        public void Detach()
        {
            if (_processHandle != IntPtr.Zero) { CloseHandle(_processHandle); _processHandle = IntPtr.Zero; }
            _process?.Dispose();
            _process = null;
        }

        // ── Module base ──────────────────────────────────────────────────────

        public IntPtr GetModuleBase(string? moduleName = null)
        {
            if (_process == null) return IntPtr.Zero;
            try
            {
                if (moduleName == null) return _process.MainModule?.BaseAddress ?? IntPtr.Zero;
                foreach (ProcessModule m in _process.Modules)
                    if (string.Equals(m.ModuleName, moduleName, StringComparison.OrdinalIgnoreCase))
                        return m.BaseAddress;
            }
            catch { }
            return IntPtr.Zero;
        }

        // ── Pointer chain ────────────────────────────────────────────────────

        public IntPtr ResolvePointerChain(IntPtr baseAddress, int[] offsets)
        {
            if (!IsAttached || offsets == null || offsets.Length == 0) return baseAddress;
            IntPtr current = baseAddress;
            for (int i = 0; i < offsets.Length - 1; i++)
            {
                current = IntPtr.Add(current, offsets[i]);
                current = ReadPointer(current);
                if (current == IntPtr.Zero) return IntPtr.Zero;
            }
            return IntPtr.Add(current, offsets[^1]);
        }

        private IntPtr ReadPointer(IntPtr address)
        {
            byte[] buf = new byte[8];
            if (!ReadProcessMemory(_processHandle, address, buf, 8, out _)) return IntPtr.Zero;
            return (IntPtr)BitConverter.ToInt64(buf, 0);
        }

        // ── Read helpers ─────────────────────────────────────────────────────

        public int   ReadInt32(IntPtr address)  { var b = new byte[4]; ReadProcessMemory(_processHandle, address, b, 4, out _); return BitConverter.ToInt32(b, 0); }
        public long  ReadInt64(IntPtr address)  { var b = new byte[8]; ReadProcessMemory(_processHandle, address, b, 8, out _); return BitConverter.ToInt64(b, 0); }
        public float ReadFloat(IntPtr address)  { var b = new byte[4]; ReadProcessMemory(_processHandle, address, b, 4, out _); return BitConverter.ToSingle(b, 0); }

        public byte[] ReadBytes(IntPtr address, int count)
        {
            byte[] buf = new byte[count];
            ReadProcessMemory(_processHandle, address, buf, count, out _);
            return buf;
        }

        // ── Write helpers ────────────────────────────────────────────────────

        public bool WriteInt32(IntPtr address, int value)    => WriteProcessMemory(_processHandle, address, BitConverter.GetBytes(value), 4, out _);
        public bool WriteInt64(IntPtr address, long value)   => WriteProcessMemory(_processHandle, address, BitConverter.GetBytes(value), 8, out _);
        public bool WriteFloat(IntPtr address, float value)  => WriteProcessMemory(_processHandle, address, BitConverter.GetBytes(value), 4, out _);
        public bool WriteBytes(IntPtr address, byte[] data)  => WriteProcessMemory(_processHandle, address, data, data.Length, out _);

        // ── AOB / Pattern Scanner ────────────────────────────────────────────

        /// <summary>
        /// Scans all committed readable memory pages and returns the first address
        /// matching <paramref name="pattern"/>. Use 0xFF in <paramref name="mask"/>
        /// for exact bytes and 0x00 for wildcards.
        /// Returns IntPtr.Zero when not found.
        /// </summary>
        public IntPtr ScanForPattern(byte[] pattern, byte[] mask)
        {
            if (!IsAttached) return IntPtr.Zero;

            IntPtr address = IntPtr.Zero;
            while (true)
            {
                int result = VirtualQueryEx(_processHandle, address, out var mbi,
                    (uint)Marshal.SizeOf<MEMORY_BASIC_INFORMATION>());
                if (result == 0) break;

                long regionEnd = (long)mbi.BaseAddress + (long)mbi.RegionSize;

                if (mbi.State == MEM_COMMIT
                    && (mbi.Protect & PAGE_NOACCESS) == 0
                    && (mbi.Protect & PAGE_GUARD) == 0)
                {
                    int size = (int)mbi.RegionSize;
                    byte[] buffer = new byte[size];
                    if (ReadProcessMemory(_processHandle, mbi.BaseAddress, buffer, size, out int bytesRead) && bytesRead > 0)
                    {
                        int found = FindPattern(buffer, bytesRead, pattern, mask);
                        if (found >= 0)
                            return IntPtr.Add(mbi.BaseAddress, found);
                    }
                }

                // Advance to next region
                long next = regionEnd;
                if (next <= (long)address) break; // overflow guard
                address = (IntPtr)next;
            }
            return IntPtr.Zero;
        }

        private static int FindPattern(byte[] buffer, int length, byte[] pattern, byte[] mask)
        {
            int end = length - pattern.Length;
            for (int i = 0; i <= end; i++)
            {
                bool match = true;
                for (int j = 0; j < pattern.Length; j++)
                {
                    if (mask[j] != 0x00 && buffer[i + j] != pattern[j])
                    { match = false; break; }
                }
                if (match) return i;
            }
            return -1;
        }

        // ── Struct-based entity scan ─────────────────────────────────────────

        /// <summary>
        /// Scans committed memory for an entity struct that matches Crimson Desert's
        /// player layout: Int64 HP at +0x08, Int64 Sta at +0x5A8, Int64 Spi at +0x638,
        /// all in the valid range [<paramref name="minVal"/>, <paramref name="maxVal"/>].
        /// Returns the entity base address or IntPtr.Zero.
        /// </summary>
        public IntPtr ScanForEntityBase(long minVal = 1_000, long maxVal = 100_000_000)
        {
            if (!IsAttached) return IntPtr.Zero;

            IntPtr address = IntPtr.Zero;
            while (true)
            {
                int result = VirtualQueryEx(_processHandle, address, out var mbi,
                    (uint)Marshal.SizeOf<MEMORY_BASIC_INFORMATION>());
                if (result == 0) break;

                long regionEnd = (long)mbi.BaseAddress + (long)mbi.RegionSize;

                if (mbi.State == MEM_COMMIT
                    && (mbi.Protect & PAGE_NOACCESS) == 0
                    && (mbi.Protect & PAGE_GUARD) == 0
                    && (long)mbi.RegionSize >= 0x640 + 8)
                {
                    int size = (int)mbi.RegionSize;
                    byte[] buffer = new byte[size];
                    if (ReadProcessMemory(_processHandle, mbi.BaseAddress, buffer, size, out int bytesRead) && bytesRead > (0x640 + 8))
                    {
                        // Walk 8-byte aligned positions looking for valid HP struct
                        int end = bytesRead - (0x640 + 8);
                        for (int i = 0; i <= end; i += 8)
                        {
                            long hp  = BitConverter.ToInt64(buffer, i + 0x08);
                            long sta = BitConverter.ToInt64(buffer, i + 0x5A8);
                            long spi = BitConverter.ToInt64(buffer, i + 0x638);

                            if (hp  >= minVal && hp  <= maxVal &&
                                sta >= minVal && sta <= maxVal &&
                                spi >= minVal && spi <= maxVal)
                            {
                                return IntPtr.Add(mbi.BaseAddress, i);
                            }
                        }
                    }
                }

                long next = regionEnd;
                if (next <= (long)address) break;
                address = (IntPtr)next;
            }
            return IntPtr.Zero;
        }

        // ── IDisposable ──────────────────────────────────────────────────────

        public void Dispose()
        {
            if (!_disposed) { Detach(); _disposed = true; }
            GC.SuppressFinalize(this);
        }
    }
}
