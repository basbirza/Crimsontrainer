using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CrimsonTrainer.Cheats;

namespace CrimsonTrainer
{
    public class CheatManager : IDisposable
    {
        // ── Config ───────────────────────────────────────────────────────────
        public const string ProcessName = "CrimsonDesert";

        private const int ApplyIntervalMs  = 100;
        private const int AttachIntervalMs = 2000;

        // ── Dependencies ─────────────────────────────────────────────────────
        private readonly MemoryManager _mem;

        // ── Entity base ──────────────────────────────────────────────────────
        // Populated once after attaching. Shared with all cheat instances.
        // Layout (from opencheattables.com CT, bbfox0703, 2026):
        //   entity_base + 0x008 = Current HP    (Int64, scaled)
        //   entity_base + 0x018 = Max HP        (Int64, scaled / 10)
        //   entity_base + 0x5A8 = Current Sta   (Int64, scaled)
        //   entity_base + 0x5B8 = Max Sta       (Int64, scaled / 10)
        //   entity_base + 0x638 = Current Spi   (Int64, scaled)
        //   entity_base + 0x648 = Max Spi       (Int64, scaled / 10)
        public IntPtr EntityBase { get; private set; } = IntPtr.Zero;

        // ── Cheats ───────────────────────────────────────────────────────────
        public IReadOnlyList<ICheat>  Cheats          { get; }
        public Cheats.Teleport         Teleport        { get; private set; } = null!;
        public Cheats.SpeedMultiplier  SpeedMultiplier { get; private set; } = null!;
        public GoldEditor              GoldEditor      { get; private set; } = null!;

        // ── Events ───────────────────────────────────────────────────────────
        public event Action<bool>?   AttachStateChanged;
        public event Action<string>? LogMessage;
        public event Action?         CheatsUpdated;
        public event Action<bool>?   EntityStateChanged;  // true = entity found

        // ── Timers ───────────────────────────────────────────────────────────
        private readonly System.Windows.Forms.Timer _applyTimer;
        private readonly System.Windows.Forms.Timer _attachTimer;
        private bool _disposed;

        // ── Constructor ──────────────────────────────────────────────────────
        public CheatManager()
        {
            _mem = new MemoryManager();

            Teleport        = new Cheats.Teleport(_mem, this);
            SpeedMultiplier = new Cheats.SpeedMultiplier(_mem, this);
            GoldEditor      = new GoldEditor(_mem);

            Cheats = new List<ICheat>
            {
                new InfiniteHealth(_mem, this),   // [0] F1
                new InfiniteStamina(_mem, this),  // [1] F2
                new InfiniteGold(_mem, this),     // [2] F3
                new NoCooldown(_mem, this),       // [3] F4
                new FreezePosition(_mem, this),   // [4] F5
                new OneHitKill(_mem, this),       // [5] F6
                new NoFallDamage(_mem, this),     // [6] F7
            };

            _applyTimer  = new System.Windows.Forms.Timer { Interval = ApplyIntervalMs  };
            _applyTimer.Tick  += OnApplyTick;

            _attachTimer = new System.Windows.Forms.Timer { Interval = AttachIntervalMs };
            _attachTimer.Tick += OnAttachTick;
        }

        // ── Lifecycle ────────────────────────────────────────────────────────

        public void Start()
        {
            _attachTimer.Start();
            _applyTimer.Start();
            TryAttach();
        }

        public void Stop()
        {
            _applyTimer.Stop();
            _attachTimer.Stop();
        }

        // ── Entity discovery ─────────────────────────────────────────────────

        /// <summary>
        /// Scan process memory to find the player entity base.
        /// Call this once the game is in-game (character loaded, HP visible).
        /// </summary>
        public void FindEntity()
        {
            if (!_mem.IsAttached)
            {
                LogMessage?.Invoke("Not attached – cannot scan for entity.");
                return;
            }
            LogMessage?.Invoke("Scanning for player entity...");
            EntityBase = _mem.ScanForEntityBase();
            if (EntityBase != IntPtr.Zero)
            {
                LogMessage?.Invoke($"Entity found at 0x{EntityBase.ToInt64():X16}");
                EntityStateChanged?.Invoke(true);
            }
            else
            {
                LogMessage?.Invoke("Entity NOT found. Load the game fully, enter combat, then try again.");
                EntityStateChanged?.Invoke(false);
            }
            CheatsUpdated?.Invoke();
        }

        /// <summary>
        /// Manually set the entity base (hex string e.g. "7FF812345678").
        /// </summary>
        public bool SetEntityManual(string hexAddress)
        {
            if (long.TryParse(hexAddress.Replace("0x", "").Replace("0X", ""),
                System.Globalization.NumberStyles.HexNumber, null, out long addr))
            {
                EntityBase = (IntPtr)addr;
                LogMessage?.Invoke($"Entity manually set to 0x{addr:X16}");
                EntityStateChanged?.Invoke(true);
                CheatsUpdated?.Invoke();
                return true;
            }
            LogMessage?.Invoke("Invalid hex address.");
            return false;
        }

        // ── Cheat toggle ─────────────────────────────────────────────────────

        public void ToggleCheat(int index)
        {
            if (index < 0 || index >= Cheats.Count) return;
            if (!_mem.IsAttached)
            {
                LogMessage?.Invoke($"Cannot toggle: not attached to {ProcessName}.");
                return;
            }
            if (EntityBase == IntPtr.Zero)
            {
                LogMessage?.Invoke("Entity not found yet. Click 'Find Entity' first.");
                return;
            }
            Cheats[index].Toggle();
            var c = Cheats[index];
            LogMessage?.Invoke($"{c.Name}: {(c.IsActive ? "ENABLED" : "DISABLED")}");
            CheatsUpdated?.Invoke();
        }

        // ── Live stat reads ──────────────────────────────────────────────────

        /// <summary>Returns raw Int64 values for HP/Stamina/Spirit (current &amp; max).</summary>
        public (long curHp,  long maxHp,
                long curSta, long maxSta,
                long curSpi, long maxSpi) ReadStats()
        {
            if (!_mem.IsAttached || EntityBase == IntPtr.Zero)
                return default;

            long curHp  = _mem.ReadInt64(IntPtr.Add(EntityBase, 0x008));
            long maxHp  = _mem.ReadInt64(IntPtr.Add(EntityBase, 0x018));
            long curSta = _mem.ReadInt64(IntPtr.Add(EntityBase, 0x5A8));
            long maxSta = _mem.ReadInt64(IntPtr.Add(EntityBase, 0x5B8));
            long curSpi = _mem.ReadInt64(IntPtr.Add(EntityBase, 0x638));
            long maxSpi = _mem.ReadInt64(IntPtr.Add(EntityBase, 0x648));
            return (curHp, maxHp, curSta, maxSta, curSpi, maxSpi);
        }

        // ── Speed toggle (separate from Cheats[] list) ───────────────────────

        public void ToggleSpeed()
        {
            if (!_mem.IsAttached)
            {
                LogMessage?.Invoke($"Cannot toggle: not attached to {ProcessName}.");
                return;
            }
            if (EntityBase == IntPtr.Zero)
            {
                LogMessage?.Invoke("Entity not found yet. Click 'Find Entity' first.");
                return;
            }
            SpeedMultiplier.Toggle();
            LogMessage?.Invoke($"Speed Multiplier ({SpeedMultiplier.Multiplier:F1}×): " +
                               $"{(SpeedMultiplier.IsActive ? "ENABLED" : "DISABLED")}");
            CheatsUpdated?.Invoke();
        }

        // ── Timer callbacks ──────────────────────────────────────────────────

        private void OnApplyTick(object? sender, EventArgs e)
        {
            if (!_mem.IsAttached)
            {
                bool any = SpeedMultiplier.IsActive;
                if (!any) foreach (var c in Cheats) if (c.IsActive) { any = true; break; }
                if (any)
                {
                    LogMessage?.Invoke("Warning: process lost. Cheats suspended.");
                    AttachStateChanged?.Invoke(false);
                    EntityBase = IntPtr.Zero;
                    EntityStateChanged?.Invoke(false);
                }
                return;
            }

            foreach (var cheat in Cheats)
            {
                try   { cheat.Apply(); }
                catch (Exception ex) { LogMessage?.Invoke($"Error in {cheat.Name}: {ex.Message}"); }
            }

            try   { SpeedMultiplier.Apply(); }
            catch (Exception ex) { LogMessage?.Invoke($"Error in Speed: {ex.Message}"); }
        }

        private void OnAttachTick(object? sender, EventArgs e)
        {
            if (!_mem.IsAttached) TryAttach();
        }

        private void TryAttach()
        {
            bool ok = _mem.Attach(ProcessName);
            AttachStateChanged?.Invoke(ok);
            if (ok)
            {
                LogMessage?.Invoke($"Attached to {ProcessName}. Click 'Find Entity' to enable cheats.");
                EntityBase = IntPtr.Zero;
                EntityStateChanged?.Invoke(false);
            }
        }

        // ── IDisposable ──────────────────────────────────────────────────────

        public void Dispose()
        {
            if (!_disposed)
            {
                Stop();
                _applyTimer.Dispose();
                _attachTimer.Dispose();
                _mem.Dispose();
                _disposed = true;
            }
            GC.SuppressFinalize(this);
        }
    }
}
