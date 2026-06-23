using System;

namespace CrimsonTrainer.Cheats
{
    // Offsets sourced from opencheattables.com CE table (bbfox0703, 2026-06-05):
    //   entity_base + 0x5A8 = Current Stamina (Int64)
    //   entity_base + 0x5B8 = Max Stamina     (Int64)
    public class InfiniteStamina : ICheat
    {
        private const int OffsetCurrentSta = 0x5A8;
        private const int OffsetMaxSta     = 0x5B8;

        public string Name => "Infinite Stamina";
        public bool IsActive { get; private set; }

        private readonly MemoryManager _mem;
        private readonly CheatManager  _mgr;

        public InfiniteStamina(MemoryManager mem, CheatManager mgr)
        {
            _mem = mem;
            _mgr = mgr;
        }

        public void Toggle() => IsActive = !IsActive;

        public void Apply()
        {
            if (!IsActive || !_mem.IsAttached || _mgr.EntityBase == IntPtr.Zero) return;

            IntPtr curStaAddr = IntPtr.Add(_mgr.EntityBase, OffsetCurrentSta);
            IntPtr maxStaAddr = IntPtr.Add(_mgr.EntityBase, OffsetMaxSta);

            long maxSta = _mem.ReadInt64(maxStaAddr);
            if (maxSta <= 0) return;

            _mem.WriteInt64(curStaAddr, maxSta * 10);
        }
    }
}
