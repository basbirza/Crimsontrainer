using System;
using System.Globalization;

namespace CrimsonTrainer.Cheats
{
    /// <summary>
    /// Direct-write Silver editor.
    ///
    /// Silver in Crimson Desert is stored in the inventory item table (item ID 980),
    /// not in the entity stats struct, so there is no fixed entity_base offset.
    ///
    /// Usage:
    ///   1. Open Cheat Engine alongside the trainer.
    ///   2. Value-scan for your current Silver amount (4-byte or 8-byte).
    ///   3. Copy the found address and paste it into the "CE Addr" field.
    ///   4. Type the desired amount and press Set.
    ///
    /// TODO: replace manual address entry with an automatic AOB scan for
    ///       INJECT_INF_ARROW_N_OTHERS (item-decrease intercept, item ID 980).
    /// </summary>
    public class GoldEditor
    {
        public IntPtr GoldAddress { get; set; } = IntPtr.Zero;
        public bool   IsAddressSet => GoldAddress != IntPtr.Zero;

        private readonly MemoryManager _mem;

        public GoldEditor(MemoryManager mem) { _mem = mem; }

        /// <summary>Returns the current Silver value, or -1 if unavailable.</summary>
        public long ReadGold()
        {
            if (!_mem.IsAttached || GoldAddress == IntPtr.Zero) return -1;
            return _mem.ReadInt64(GoldAddress);
        }

        /// <summary>Writes the given amount to the Silver address.</summary>
        public bool WriteGold(long amount)
        {
            if (!_mem.IsAttached || GoldAddress == IntPtr.Zero) return false;
            _mem.WriteInt64(GoldAddress, amount);
            return true;
        }

        /// <summary>
        /// Parses a hex string (with or without "0x" prefix) and stores it as GoldAddress.
        /// Returns false if the string is not valid hex or resolves to zero.
        /// </summary>
        public bool SetAddressHex(string hex)
        {
            if (string.IsNullOrWhiteSpace(hex)) return false;
            hex = hex.Trim();
            if (hex.StartsWith("0x", StringComparison.OrdinalIgnoreCase) ||
                hex.StartsWith("0X", StringComparison.OrdinalIgnoreCase))
                hex = hex.Substring(2);
            if (long.TryParse(hex, NumberStyles.HexNumber, null, out long addr) && addr > 0)
            {
                GoldAddress = (IntPtr)addr;
                return true;
            }
            return false;
        }
    }
}
