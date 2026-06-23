using System;

namespace CrimsonTrainer.Cheats
{
    // Movement speed multiplier — writes a float multiplier to the speed field each tick.
    //
    // Offset source: UNVERIFIED — placeholder until confirmed with Cheat Engine.
    // To find the real offset:
    //   1. In CE: scan for float 1.0 while standing still (speed = default).
    //   2. Walk and scan for "changed value" → narrows to a few candidates.
    //   3. Use "Find out what writes to this address" to identify the instruction.
    //   4. Compute entity_base offset by subtracting EntityBase from the candidate address.
    //
    // Placeholder: entity_base + 0x2C0 (float, default = 1.0f)
    // TODO: verify in-game with Cheat Engine before shipping.
    public class SpeedMultiplier : ICheat
    {
        // !! NEEDS VERIFICATION
        private const int   OffsetSpeed    = 0x2C0;
        private const float DefaultSpeed   = 1.0f;

        public string Name     => "Speed Multiplier";
        public bool   IsActive { get; private set; }

        // Slider value — set by the UI TrackBar (0.2× – 5.0×)
        private float _multiplier = 2.0f;
        public float Multiplier
        {
            get => _multiplier;
            set => _multiplier = Math.Clamp(value, 0.2f, 5.0f);
        }

        private readonly MemoryManager _mem;
        private readonly CheatManager  _mgr;

        public SpeedMultiplier(MemoryManager mem, CheatManager mgr)
        {
            _mem = mem;
            _mgr = mgr;
        }

        public void Toggle()
        {
            IsActive = !IsActive;
            if (!IsActive) Restore();
        }

        public void Apply()
        {
            if (!IsActive || !_mem.IsAttached || _mgr.EntityBase == IntPtr.Zero) return;
            _mem.WriteFloat(IntPtr.Add(_mgr.EntityBase, OffsetSpeed), _multiplier);
        }

        private void Restore()
        {
            if (_mem.IsAttached && _mgr.EntityBase != IntPtr.Zero)
                _mem.WriteFloat(IntPtr.Add(_mgr.EntityBase, OffsetSpeed), DefaultSpeed);
        }
    }
}
