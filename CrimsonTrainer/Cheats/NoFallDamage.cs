using System;

namespace CrimsonTrainer.Cheats
{
    // No Fall Damage — zeroes the downward velocity accumulator each tick so the engine
    // never accumulates enough fall speed to register damage.
    //
    // Offset source: UNVERIFIED — placeholder until confirmed with Cheat Engine.
    // To find the real offset:
    //   1. CE: scan for float 0.0 while standing on the ground.
    //   2. Jump off a cliff; scan for "decreased value" (negative float = downward).
    //   3. Land → scan for 0.0 again. Remaining candidates are likely fall velocity.
    //   4. Use "Find out what writes to this address" to locate the physics code,
    //      then compute entity_base delta.
    //
    // Alternative (more reliable): patch the fall-damage-apply function via code injection
    //   to NOP the HP-decrease call — requires AOB research in CE (not implemented here).
    //
    // Placeholder: entity_base + 0xB0 (float, fall velocity accumulator)
    // TODO: verify offset with Cheat Engine in-game.
    public class NoFallDamage : ICheat
    {
        // !! NEEDS VERIFICATION
        private const int OffsetFallVelocity = 0xB0;

        public string Name     => "No Fall Damage";
        public bool   IsActive { get; private set; }

        private readonly MemoryManager _mem;
        private readonly CheatManager  _mgr;

        public NoFallDamage(MemoryManager mem, CheatManager mgr)
        {
            _mem = mem;
            _mgr = mgr;
        }

        public void Toggle() => IsActive = !IsActive;

        public void Apply()
        {
            if (!IsActive || !_mem.IsAttached || _mgr.EntityBase == IntPtr.Zero) return;
            // Zero the downward velocity so the engine sees no fall distance when landing
            _mem.WriteFloat(IntPtr.Add(_mgr.EntityBase, OffsetFallVelocity), 0f);
        }
    }
}
