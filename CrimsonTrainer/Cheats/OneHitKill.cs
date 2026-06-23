using System;

namespace CrimsonTrainer.Cheats
{
    // One-Hit Kill — overwrites the player's base attack power with an enormous value
    // so every hit is lethal against normal enemies.
    //
    // Offset source: UNVERIFIED — placeholder until confirmed with Cheat Engine.
    // To find the real offset:
    //   1. CE: 4-byte or int64 scan for a value that changes when your attack power changes
    //      (e.g. equip a stronger weapon, compare before/after).
    //   2. Alternatively: AOB-search for the attack-calculation code path, find the
    //      attack base pointer and compute entity_base delta.
    //
    // Placeholder: entity_base + 0x700 (Int64, attack base, same scaling as HP ≈ ×100)
    //
    // Note: a more reliable alternative is to find the target/locked-on enemy entity
    //   and zero-write its current HP every tick — see ScanForEntityBase() pattern.
    // TODO: verify offset with Cheat Engine in-game.
    public class OneHitKill : ICheat
    {
        // !! NEEDS VERIFICATION
        private const int  OffsetAttack    = 0x700;
        private const long OverpowerValue  = 999_999_999L;

        public string Name     => "One-Hit Kill";
        public bool   IsActive { get; private set; }

        private long _savedAttack;

        private readonly MemoryManager _mem;
        private readonly CheatManager  _mgr;

        public OneHitKill(MemoryManager mem, CheatManager mgr)
        {
            _mem = mem;
            _mgr = mgr;
        }

        public void Toggle()
        {
            if (!IsActive && _mem.IsAttached && _mgr.EntityBase != IntPtr.Zero)
                _savedAttack = _mem.ReadInt64(IntPtr.Add(_mgr.EntityBase, OffsetAttack));

            IsActive = !IsActive;
            if (!IsActive) Restore();
        }

        public void Apply()
        {
            if (!IsActive || !_mem.IsAttached || _mgr.EntityBase == IntPtr.Zero) return;
            _mem.WriteInt64(IntPtr.Add(_mgr.EntityBase, OffsetAttack), OverpowerValue);
        }

        private void Restore()
        {
            if (_mem.IsAttached && _mgr.EntityBase != IntPtr.Zero && _savedAttack != 0)
                _mem.WriteInt64(IntPtr.Add(_mgr.EntityBase, OffsetAttack), _savedAttack);
        }
    }
}
