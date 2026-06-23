using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CrimsonTrainer
{
    /// <summary>
    /// Registers system-wide (global) hotkeys via RegisterHotKey / UnregisterHotKey.
    /// Hotkeys fire even when another application (i.e. the game) has focus.
    ///
    /// Usage:
    ///   var hk = new GlobalHotkey(handle);
    ///   hk.Register(1, Keys.F1);
    ///   hk.HotkeyPressed += (id) => { ... };
    ///   // in WndProc: hk.ProcessMessage(ref m);
    ///   hk.Dispose();  // unregisters all
    /// </summary>
    public class GlobalHotkey : IDisposable
    {
        // WM_HOTKEY message identifier
        private const int WM_HOTKEY = 0x0312;

        // Modifier key constants (we use no modifier — just bare function keys)
        private const uint MOD_NONE    = 0x0000;
        private const uint MOD_ALT     = 0x0001;
        private const uint MOD_CONTROL = 0x0002;
        private const uint MOD_SHIFT   = 0x0004;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        // ── State ─────────────────────────────────────────────────────────────
        private readonly IntPtr _hwnd;
        private readonly List<int> _registeredIds = new();
        private bool _disposed;

        /// <summary>Fired when a registered hotkey is pressed. Argument is the hotkey ID.</summary>
        public event Action<int>? HotkeyPressed;

        public GlobalHotkey(IntPtr windowHandle)
        {
            _hwnd = windowHandle;
        }

        // ── Registration ──────────────────────────────────────────────────────

        /// <summary>
        /// Register a hotkey. The <paramref name="id"/> must be unique per window.
        /// Use Keys enum values; modifiers default to none (bare F-key).
        /// </summary>
        public bool Register(int id, Keys key, uint modifiers = MOD_NONE)
        {
            bool ok = RegisterHotKey(_hwnd, id, modifiers, (uint)key);
            if (ok) _registeredIds.Add(id);
            return ok;
        }

        public void Unregister(int id)
        {
            UnregisterHotKey(_hwnd, id);
            _registeredIds.Remove(id);
        }

        // ── WndProc hook ──────────────────────────────────────────────────────

        /// <summary>
        /// Call this from the form's WndProc override.
        /// Returns true if the message was a hotkey and was handled.
        /// </summary>
        public bool ProcessMessage(ref Message m)
        {
            if (m.Msg == WM_HOTKEY)
            {
                int id = m.WParam.ToInt32();
                HotkeyPressed?.Invoke(id);
                return true;
            }
            return false;
        }

        // ── IDisposable ───────────────────────────────────────────────────────

        public void Dispose()
        {
            if (!_disposed)
            {
                foreach (int id in _registeredIds)
                    UnregisterHotKey(_hwnd, id);
                _registeredIds.Clear();
                _disposed = true;
            }
            GC.SuppressFinalize(this);
        }
    }
}
