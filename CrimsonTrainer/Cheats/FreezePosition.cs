using System;

namespace CrimsonTrainer.Cheats
{
    // Captures the player's world position on enable and re-writes it every tick,
    // effectively locking the character in place (no knockback, no gravity).
    //
    // Uses the confirmed Teleport offsets:
    //   entity_base + 0x90 = X  (float)
    //   entity_base + 0x94 = Z_height (float)
    //   entity_base + 0x98 = Y  (float)
    // Source: opencheattables.com CT entry 186 (bbfox0703, 2026-06-05) ✓
    public class FreezePosition : ICheat
    {
        private const int OffsetX = 0x90;
        private const int OffsetZ = 0x94;
        private const int OffsetY = 0x98;

        public string Name     => "Freeze Position";
        public bool   IsActive { get; private set; }

        private readonly MemoryManager _mem;
        private readonly CheatManager  _mgr;

        private float _frozenX, _frozenZ, _frozenY;

        public FreezePosition(MemoryManager mem, CheatManager mgr)
        {
            _mem = mem;
            _mgr = mgr;
        }

        public void Toggle()
        {
            if (!IsActive && _mem.IsAttached && _mgr.EntityBase != IntPtr.Zero)
            {
                // Snapshot position at the moment of activation
                _frozenX = _mem.ReadFloat(IntPtr.Add(_mgr.EntityBase, OffsetX));
                _frozenZ = _mem.ReadFloat(IntPtr.Add(_mgr.EntityBase, OffsetZ));
                _frozenY = _mem.ReadFloat(IntPtr.Add(_mgr.EntityBase, OffsetY));
            }
            IsActive = !IsActive;
        }

        public void Apply()
        {
            if (!IsActive || !_mem.IsAttached || _mgr.EntityBase == IntPtr.Zero) return;

            _mem.WriteFloat(IntPtr.Add(_mgr.EntityBase, OffsetX), _frozenX);
            _mem.WriteFloat(IntPtr.Add(_mgr.EntityBase, OffsetZ), _frozenZ);
            _mem.WriteFloat(IntPtr.Add(_mgr.EntityBase, OffsetY), _frozenY);
        }
    }
}
