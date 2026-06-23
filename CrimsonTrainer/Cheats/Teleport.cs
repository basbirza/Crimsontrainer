using System;

namespace CrimsonTrainer.Cheats
{
    /// <summary>One named bookmark slot.</summary>
    public struct TeleportSlot
    {
        public string Name;
        public float  X, Y, Z;
        public bool   HasPosition;

        public TeleportSlot(string name)
        {
            Name        = name;
            X = Y = Z   = 0f;
            HasPosition = false;
        }
    }

    // Position offsets from the movement entity (entityCapturePtr = [rsi] at INJECT_ENTCAP):
    //   entityCapturePtr + 0x90  = world position float4 {X, Z_height, Y, W}
    //
    // Layout: [entity + 0x90] = X (float), [entity + 0x94] = Z_height (float), [entity + 0x98] = Y (float)
    //
    // Sources: opencheattables.com CT entry 186 (INJECT_ENTCAP raw AOB), 2026-06-05
    public class Teleport : ICheat
    {
        private const int OffsetX = 0x90;
        private const int OffsetZ = 0x94;  // height
        private const int OffsetY = 0x98;

        public string Name     => "Teleport";
        public bool   IsActive { get; private set; }

        private readonly MemoryManager _mem;
        private readonly CheatManager  _mgr;

        // ── 3 named slots (A = 0, B = 1, C = 2) ────────────────────────────
        public TeleportSlot[] Slots { get; } = new TeleportSlot[3]
        {
            new TeleportSlot("Slot A"),
            new TeleportSlot("Slot B"),
            new TeleportSlot("Slot C"),
        };

        public event Action? PositionChanged;

        public Teleport(MemoryManager mem, CheatManager mgr)
        {
            _mem = mem;
            _mgr = mgr;
        }

        public void Toggle() { }
        public void Apply()  { }

        // ── Read current position ────────────────────────────────────────────

        public (float x, float y, float z) ReadCurrentPosition()
        {
            if (!_mem.IsAttached || _mgr.EntityBase == IntPtr.Zero)
                return (0f, 0f, 0f);

            float x = _mem.ReadFloat(IntPtr.Add(_mgr.EntityBase, OffsetX));
            float z = _mem.ReadFloat(IntPtr.Add(_mgr.EntityBase, OffsetZ));
            float y = _mem.ReadFloat(IntPtr.Add(_mgr.EntityBase, OffsetY));
            return (x, y, z);
        }

        // ── Slot operations ──────────────────────────────────────────────────

        /// <summary>Save current world position into the given slot (0–2).</summary>
        public bool SaveToSlot(int index)
        {
            if ((uint)index >= (uint)Slots.Length) return false;
            if (!_mem.IsAttached || _mgr.EntityBase == IntPtr.Zero) return false;

            var (x, y, z) = ReadCurrentPosition();
            Slots[index].X           = x;
            Slots[index].Y           = y;
            Slots[index].Z           = z;
            Slots[index].HasPosition = true;
            PositionChanged?.Invoke();
            return true;
        }

        /// <summary>Teleport to the saved position in the given slot (0–2).</summary>
        public bool TeleportToSlot(int index)
        {
            if ((uint)index >= (uint)Slots.Length) return false;
            if (!_mem.IsAttached || _mgr.EntityBase == IntPtr.Zero) return false;
            if (!Slots[index].HasPosition) return false;

            return TeleportTo(Slots[index].X, Slots[index].Y, Slots[index].Z);
        }

        // ── Teleport to custom coordinates ───────────────────────────────────

        public bool TeleportTo(float x, float y, float z)
        {
            if (!_mem.IsAttached || _mgr.EntityBase == IntPtr.Zero) return false;

            _mem.WriteFloat(IntPtr.Add(_mgr.EntityBase, OffsetX), x);
            _mem.WriteFloat(IntPtr.Add(_mgr.EntityBase, OffsetZ), z);
            _mem.WriteFloat(IntPtr.Add(_mgr.EntityBase, OffsetY), y);
            return true;
        }
    }
}
