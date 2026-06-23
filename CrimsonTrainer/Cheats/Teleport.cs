using System;

namespace CrimsonTrainer.Cheats
{
    // Position offsets from the movement entity (entityCapturePtr = [rsi] at INJECT_ENTCAP):
    //   entityCapturePtr + 0x90  = world position float4 {X, Z_height, Y, W}
    //
    // In the CT the injection at INJECT_ENTCAP captures `[rsi]` as the movement entity.
    // Our entity_base (stats entity) is captured from a different code path (rbx/rdi).
    // If both point to the same struct the offsets below work directly.
    // If not, entity_base + 0xF0 is the fallback (rbp+0xF0 used as position arg in same function).
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

        // Saved coordinates
        public float SavedX { get; private set; }
        public float SavedY { get; private set; }
        public float SavedZ { get; private set; }
        public bool  HasSavedPosition { get; private set; }

        public event Action? PositionChanged;

        public Teleport(MemoryManager mem, CheatManager mgr)
        {
            _mem = mem;
            _mgr = mgr;
        }

        public void Toggle() { }  // Teleport is not a toggle — it's triggered on demand.

        public void Apply() { }   // No per-tick apply; teleport is one-shot via TeleportNow().

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

        // ── Save current position ────────────────────────────────────────────

        public bool SavePosition()
        {
            if (!_mem.IsAttached || _mgr.EntityBase == IntPtr.Zero) return false;

            (SavedX, SavedZ, SavedY) = ReadCurrentPosition();
            HasSavedPosition = true;
            PositionChanged?.Invoke();
            return true;
        }

        // ── Teleport to saved position ───────────────────────────────────────

        public bool TeleportNow()
        {
            if (!_mem.IsAttached || _mgr.EntityBase == IntPtr.Zero || !HasSavedPosition)
                return false;

            _mem.WriteFloat(IntPtr.Add(_mgr.EntityBase, OffsetX), SavedX);
            _mem.WriteFloat(IntPtr.Add(_mgr.EntityBase, OffsetZ), SavedZ);
            _mem.WriteFloat(IntPtr.Add(_mgr.EntityBase, OffsetY), SavedY);
            return true;
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
