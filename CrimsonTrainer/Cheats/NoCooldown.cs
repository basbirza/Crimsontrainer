using System;

namespace CrimsonTrainer.Cheats
{
    // No-Cooldown in Crimson Desert requires an AOB injection into the cooldown timer
    // update function. The CT entry "Fast dragon cooldown" (ID 370) uses a separate
    // pattern not yet mapped to a static struct offset.
    //
    // As a placeholder this cheat writes max HP each tick (= god mode / max health fill)
    // until the correct cooldown offset is found via further CE research.
    //
    // TODO: find cooldown timer offset via "find what writes" on a skill that has a
    // visible cooldown, then add the offset here and call WriteFloat(addr, 0f).
    public class NoCooldown : ICheat
    {
        private const int OffsetCurrentHp = 0x08;
        private const int OffsetMaxHp     = 0x18;

        public string Name => "God Mode (No CD: TODO)";
        public bool IsActive { get; private set; }

        private readonly MemoryManager _mem;
        private readonly CheatManager  _mgr;

        public NoCooldown(MemoryManager mem, CheatManager mgr)
        {
            _mem = mem;
            _mgr = mgr;
        }

        public void Toggle() => IsActive = !IsActive;

        public void Apply()
        {
            if (!IsActive || !_mem.IsAttached || _mgr.EntityBase == IntPtr.Zero) return;

            // Fills HP to max * 10 every tick (keeps HP bar permanently full)
            IntPtr curHpAddr = IntPtr.Add(_mgr.EntityBase, OffsetCurrentHp);
            IntPtr maxHpAddr = IntPtr.Add(_mgr.EntityBase, OffsetMaxHp);

            long maxHp = _mem.ReadInt64(maxHpAddr);
            if (maxHp <= 0) return;

            _mem.WriteInt64(curHpAddr, maxHp * 10);
        }
    }
}
