using System;

namespace CrimsonTrainer.Cheats
{
    // Silver (= currency) in Crimson Desert is stored in the INVENTORY item table,
    // not in the player stats struct. Item ID 980 = Silver.
    // The CT table handles this via a separate AOB (INJECT_INF_ARROW_N_OTHERS)
    // that intercepts the item-decrease code and blocks it for item ID 980.
    //
    // This cheat instead freezes Spirit (at +0x638) at max as a placeholder,
    // since a clean Silver implementation needs a different AOB injection approach.
    // TODO: implement Silver via INJECT_INF_ARROW_N_OTHERS AOB (item ID 980, #define Silver #1859+Copper chain).
    public class InfiniteGold : ICheat
    {
        private const int OffsetCurrentSpi = 0x638;
        private const int OffsetMaxSpi     = 0x648;

        public string Name => "Infinite Spirit (Gold: TODO)";
        public bool IsActive { get; private set; }

        private readonly MemoryManager _mem;
        private readonly CheatManager  _mgr;

        public InfiniteGold(MemoryManager mem, CheatManager mgr)
        {
            _mem = mem;
            _mgr = mgr;
        }

        public void Toggle() => IsActive = !IsActive;

        public void Apply()
        {
            if (!IsActive || !_mem.IsAttached || _mgr.EntityBase == IntPtr.Zero) return;

            IntPtr curSpiAddr = IntPtr.Add(_mgr.EntityBase, OffsetCurrentSpi);
            IntPtr maxSpiAddr = IntPtr.Add(_mgr.EntityBase, OffsetMaxSpi);

            long maxSpi = _mem.ReadInt64(maxSpiAddr);
            if (maxSpi <= 0) return;

            _mem.WriteInt64(curSpiAddr, maxSpi * 10);
        }
    }
}
