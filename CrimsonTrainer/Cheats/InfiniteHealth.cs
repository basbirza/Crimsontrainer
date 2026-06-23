using System;

namespace CrimsonTrainer.Cheats
{
    // Offsets sourced from opencheattables.com CE table (bbfox0703, 2026-06-05):
    //   entity_base + 0x08  = Current HP  (Int64, stored as display_hp * ~100)
    //   entity_base + 0x18  = Max HP      (Int64, stored as display_hp * ~10)
    // Apply: writes Max HP * 10 → Current HP each tick (same logic as CT "Auto fill").
    public class InfiniteHealth : ICheat
    {
        private const int OffsetCurrentHp = 0x08;
        private const int OffsetMaxHp     = 0x18;

        public string Name => "Infinite Health";
        public bool IsActive { get; private set; }

        private readonly MemoryManager _mem;
        private readonly CheatManager  _mgr;

        public InfiniteHealth(MemoryManager mem, CheatManager mgr)
        {
            _mem = mem;
            _mgr = mgr;
        }

        public void Toggle() => IsActive = !IsActive;

        public void Apply()
        {
            if (!IsActive || !_mem.IsAttached || _mgr.EntityBase == IntPtr.Zero) return;

            IntPtr curHpAddr = IntPtr.Add(_mgr.EntityBase, OffsetCurrentHp);
            IntPtr maxHpAddr = IntPtr.Add(_mgr.EntityBase, OffsetMaxHp);

            long maxHp = _mem.ReadInt64(maxHpAddr);
            if (maxHp <= 0) return;

            _mem.WriteInt64(curHpAddr, maxHp * 10);
        }
    }
}
