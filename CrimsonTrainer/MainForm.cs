using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrimsonTrainer.Themes;

namespace CrimsonTrainer
{
    public partial class MainForm : Form
    {
        private const int HK_HEALTH     = 1;
        private const int HK_STAMINA    = 2;
        private const int HK_GOLD       = 3;
        private const int HK_NOCOOLDOWN = 4;
        private const int HK_FREEZE     = 5;
        private const int HK_OHK        = 6;
        private const int HK_NOFALL     = 7;
        private const int HK_SPEED      = 8;

        private CheatManager _cheatManager = null!;
        private GlobalHotkey _hotkeys      = null!;
        private System.Windows.Forms.Timer _pollTimer = null!;

        private readonly ThemeManager _theme = new();

        public MainForm() => InitializeComponent();

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Apply default theme (Midnight Forge) before anything else is shown
            _theme.EnsureFontsLoaded();
            ApplyTheme(ThemeDefinitions.MidnightForge);

            _cheatManager = new CheatManager();
            _cheatManager.AttachStateChanged += OnAttachStateChanged;
            _cheatManager.EntityStateChanged += OnEntityStateChanged;
            _cheatManager.LogMessage         += AppendLog;
            _cheatManager.CheatsUpdated      += RefreshCheatButtons;
            _cheatManager.Start();

            _hotkeys = new GlobalHotkey(Handle);
            RegisterHotkeys();

            btnHealth.Click     += (_, __) => _cheatManager.ToggleCheat(0);
            btnStamina.Click    += (_, __) => _cheatManager.ToggleCheat(1);
            btnGold.Click       += (_, __) => _cheatManager.ToggleCheat(2);
            btnNoCooldown.Click += (_, __) => _cheatManager.ToggleCheat(3);
            btnFreezePos.Click  += (_, __) => _cheatManager.ToggleCheat(4);
            btnOneHitKill.Click += (_, __) => _cheatManager.ToggleCheat(5);
            btnNoFallDmg.Click  += (_, __) => _cheatManager.ToggleCheat(6);

            btnSpeedToggle.Click += (_, __) => { _cheatManager.ToggleSpeed(); RefreshCheatButtons(); };
            trkSpeed.ValueChanged += OnSpeedSliderChanged;

            btnFindEntity.Click     += OnFindEntityClicked;
            btnTeleportCustom.Click += OnTeleportCustomClicked;

            // Teleport slot buttons
            btnSaveSlotA.Click += (_, __) => OnSaveSlot(0, txtSlotNameA, lblSlotCoordsA);
            btnSaveSlotB.Click += (_, __) => OnSaveSlot(1, txtSlotNameB, lblSlotCoordsB);
            btnSaveSlotC.Click += (_, __) => OnSaveSlot(2, txtSlotNameC, lblSlotCoordsC);
            btnGoSlotA.Click   += (_, __) => OnGoSlot(0);
            btnGoSlotB.Click   += (_, __) => OnGoSlot(1);
            btnGoSlotC.Click   += (_, __) => OnGoSlot(2);

            // Theme picker
            btnTheme0.Click += (_, __) => ApplyTheme(ThemeDefinitions.MidnightForge);
            btnTheme1.Click += (_, __) => ApplyTheme(ThemeDefinitions.Atelier);
            btnTheme2.Click += (_, __) => ApplyTheme(ThemeDefinitions.Sandsworn);
            btnTheme3.Click += (_, __) => ApplyTheme(ThemeDefinitions.Override);

            _cheatManager.Teleport.PositionChanged += RefreshSlotLabels;

            _pollTimer = new System.Windows.Forms.Timer { Interval = 300 };
            _pollTimer.Tick += (_, __) => UpdateLiveStats();
            _pollTimer.Start();

            AppendLog("Trainer started. Waiting for Crimson Desert...");
            AppendLog("Step 1: Launch the game and load a save.");
            AppendLog("Step 2: Click 'Find Entity' once your character is in-game.");
            AppendLog("Step 3: Toggle cheats with buttons or F1–F8.");
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _pollTimer?.Stop();
            _hotkeys?.Dispose();
            _cheatManager?.Dispose();
            _theme.Dispose();
            base.OnFormClosing(e);
        }

        protected override void WndProc(ref Message m)
        {
            if (_hotkeys?.ProcessMessage(ref m) == true) return;
            base.WndProc(ref m);
        }

        // ── Theme switching ───────────────────────────────────────────────────

        private void ApplyTheme(TrainerTheme theme)
        {
            _theme.Apply(this, theme);
            // Re-apply cheat button active states after theme repaint
            RefreshCheatButtons();
            // Highlight the active theme button
            HighlightThemeButton(theme);
        }

        private void HighlightThemeButton(TrainerTheme theme)
        {
            var buttons = new[] { btnTheme0, btnTheme1, btnTheme2, btnTheme3 };
            var themes  = ThemeDefinitions.All;
            for (int i = 0; i < buttons.Length && i < themes.Length; i++)
            {
                bool sel = themes[i].Name == theme.Name;
                buttons[i].FlatAppearance.BorderColor = sel
                    ? theme.HeaderTitle
                    : theme.FindBtnBorder;
                buttons[i].FlatAppearance.BorderSize = sel ? 2 : 1;
            }
        }

        // ── Teleport buttons ─────────────────────────────────────────────────

        private void OnSaveSlot(int index, System.Windows.Forms.TextBox nameBox, System.Windows.Forms.Label coordsLabel)
        {
            if (_cheatManager.Teleport.SaveToSlot(index))
            {
                // Update the slot name from the text box
                _cheatManager.Teleport.Slots[index].Name = nameBox.Text.Trim().Length > 0
                    ? nameBox.Text.Trim() : $"Slot {(char)('A' + index)}";

                var s = _cheatManager.Teleport.Slots[index];
                coordsLabel.Text = $"X={s.X:F1}  Y={s.Y:F1}  Z={s.Z:F1}";
                AppendLog($"[{s.Name}] saved: X={s.X:F1}  Y={s.Y:F1}  Z={s.Z:F1}");
            }
            else
            {
                AppendLog("Cannot save: entity not found.");
            }
        }

        private void OnGoSlot(int index)
        {
            if (_cheatManager.Teleport.TeleportToSlot(index))
            {
                var s = _cheatManager.Teleport.Slots[index];
                AppendLog($"Teleported to [{s.Name}]: X={s.X:F1}  Y={s.Y:F1}  Z={s.Z:F1}");
            }
            else
            {
                AppendLog("Cannot teleport: slot empty or entity not found.");
            }
        }

        private void OnTeleportCustomClicked(object? sender, EventArgs e)
        {
            if (!float.TryParse(txtX.Text, System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.InvariantCulture, out float x) ||
                !float.TryParse(txtY.Text, System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.InvariantCulture, out float y) ||
                !float.TryParse(txtZ.Text, System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.InvariantCulture, out float z))
            {
                AppendLog("Invalid coordinates. Use decimal numbers (e.g. 1234.5).");
                return;
            }
            if (_cheatManager.Teleport.TeleportTo(x, y, z))
                AppendLog($"Teleported to custom: X={x:F1}  Y={y:F1}  Z={z:F1}");
            else
                AppendLog("Cannot teleport: entity not found.");
        }

        private void RefreshSlotLabels()
        {
            if (InvokeRequired) { Invoke(RefreshSlotLabels); return; }

            var slots  = _cheatManager.Teleport.Slots;
            var labels = new[] { lblSlotCoordsA, lblSlotCoordsB, lblSlotCoordsC };

            for (int i = 0; i < slots.Length; i++)
            {
                labels[i].Text = slots[i].HasPosition
                    ? $"X={slots[i].X:F1}  Y={slots[i].Y:F1}  Z={slots[i].Z:F1}"
                    : "--";
            }
        }

        // ── Find Entity button ────────────────────────────────────────────────

        private async void OnFindEntityClicked(object? sender, EventArgs e)
        {
            btnFindEntity.Enabled = false;
            btnFindEntity.Text    = "Scanning...";
            AppendLog("Starting entity scan (may take a few seconds)...");

            await Task.Run(() => _cheatManager.FindEntity());

            btnFindEntity.Enabled = true;
            btnFindEntity.Text    = "🔍 Find Entity";
        }

        // ── Hotkeys ───────────────────────────────────────────────────────────

        private void RegisterHotkeys()
        {
            bool ok = true;
            ok &= _hotkeys.Register(HK_HEALTH,     Keys.F1);
            ok &= _hotkeys.Register(HK_STAMINA,    Keys.F2);
            ok &= _hotkeys.Register(HK_GOLD,       Keys.F3);
            ok &= _hotkeys.Register(HK_NOCOOLDOWN, Keys.F4);
            ok &= _hotkeys.Register(HK_FREEZE,     Keys.F5);
            ok &= _hotkeys.Register(HK_OHK,        Keys.F6);
            ok &= _hotkeys.Register(HK_NOFALL,     Keys.F7);
            ok &= _hotkeys.Register(HK_SPEED,      Keys.F8);

            if (!ok)
                AppendLog("Warning: one or more hotkeys could not be registered (F1–F8 may be in use).");
            else
                AppendLog("Hotkeys: F1=Health  F2=Stamina  F3=Spirit  F4=GodMode  F5=Freeze  F6=OHK  F7=NoFall  F8=Speed");

            _hotkeys.HotkeyPressed += OnHotkeyPressed;
        }

        private void OnHotkeyPressed(int id)
        {
            if (InvokeRequired) { Invoke(() => OnHotkeyPressed(id)); return; }
            switch (id)
            {
                case HK_HEALTH:     _cheatManager.ToggleCheat(0); break;
                case HK_STAMINA:    _cheatManager.ToggleCheat(1); break;
                case HK_GOLD:       _cheatManager.ToggleCheat(2); break;
                case HK_NOCOOLDOWN: _cheatManager.ToggleCheat(3); break;
                case HK_FREEZE:     _cheatManager.ToggleCheat(4); break;
                case HK_OHK:        _cheatManager.ToggleCheat(5); break;
                case HK_NOFALL:     _cheatManager.ToggleCheat(6); break;
                case HK_SPEED:      _cheatManager.ToggleSpeed(); RefreshCheatButtons(); break;
            }
        }

        // ── UI callbacks ──────────────────────────────────────────────────────

        private void OnAttachStateChanged(bool attached)
        {
            if (InvokeRequired) { Invoke(() => OnAttachStateChanged(attached)); return; }

            lblStatus.Text      = attached ? "● ATTACHED" : "● NOT ATTACHED";
            lblStatus.ForeColor = attached
                ? _theme.Current.StatusGreen
                : Color.FromArgb(200, 80, 80);

            if (!attached) RefreshCheatButtons();
        }

        private void OnEntityStateChanged(bool found)
        {
            if (InvokeRequired) { Invoke(() => OnEntityStateChanged(found)); return; }

            if (found)
            {
                lblEntityStatus.Text      = $"Entity: FOUND  0x{_cheatManager.EntityBase.ToInt64():X}";
                lblEntityStatus.ForeColor = _theme.Current.StatusGreen;
            }
            else
            {
                lblEntityStatus.Text      = "Entity: NOT FOUND";
                lblEntityStatus.ForeColor = Color.FromArgb(200, 80, 80);
            }
        }

        private void RefreshCheatButtons()
        {
            if (InvokeRequired) { Invoke(RefreshCheatButtons); return; }

            var c = _cheatManager.Cheats;
            ApplyButtonState(btnHealth,     c[0].IsActive);
            ApplyButtonState(btnStamina,    c[1].IsActive);
            ApplyButtonState(btnGold,       c[2].IsActive);
            ApplyButtonState(btnNoCooldown, c[3].IsActive);
            ApplyButtonState(btnFreezePos,  c[4].IsActive);
            ApplyButtonState(btnOneHitKill, c[5].IsActive);
            ApplyButtonState(btnNoFallDmg,  c[6].IsActive);
            ApplyButtonState(btnSpeedToggle, _cheatManager.SpeedMultiplier.IsActive);
        }

        // ── Live stats / coords poll ──────────────────────────────────────────

        private void UpdateLiveStats()
        {
            var (curHp, maxHp, curSta, maxSta, curSpi, maxSpi) = _cheatManager.ReadStats();

            bool hasData = maxHp > 0 && maxSta > 0 && maxSpi > 0;

            UpdateStatBar(pnlHpFill,  lblHpVal,  curHp,  maxHp,  hasData);
            UpdateStatBar(pnlStaFill, lblStaVal, curSta, maxSta, hasData);
            UpdateStatBar(pnlSpiFill, lblSpiVal, curSpi, maxSpi, hasData);

            if (_cheatManager.EntityBase != IntPtr.Zero)
            {
                var (x, y, z) = _cheatManager.Teleport.ReadCurrentPosition();
                lblLiveCoords.Text = $"Current:  X={x:F1}  Y={y:F1}  Z={z:F1}";
            }
            else
            {
                lblLiveCoords.Text = "Current: --";
            }
        }

        private static void UpdateStatBar(
            Panel fill, Label val,
            long current, long max, bool hasData)
        {
            if (!hasData || max <= 0)
            {
                fill.Width = 0;
                val.Text   = "--";
                return;
            }

            double pct = Math.Clamp(current / (max * 10.0), 0.0, 1.0);
            fill.Width = (int)(StatBarMaxWidth * pct);

            long dispCur = current / 100;
            long dispMax = max    / 10;
            val.Text = $"{dispCur:N0} / {dispMax:N0}";
        }

        // ── Speed slider ──────────────────────────────────────────────────────

        private void OnSpeedSliderChanged(object? sender, EventArgs e)
        {
            float mult = trkSpeed.Value / 10f;
            _cheatManager.SpeedMultiplier.Multiplier = mult;
            lblSpeedValue.Text = $"{mult:F1}×";
        }

        private void ApplyButtonState(Button btn, bool active)
        {
            _theme.ApplyCheatButton(btn, _theme.Current, active);
            // Preserve the ▶/■ prefix convention
            string raw = btn.Text.TrimStart('▶', '■', ' ');
            btn.Text = active ? $"▶ {raw}" : $"■ {raw}";
        }

        // ── Log ───────────────────────────────────────────────────────────────

        private void AppendLog(string message)
        {
            if (InvokeRequired) { Invoke(() => AppendLog(message)); return; }

            rtbLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}");
            rtbLog.ScrollToCaret();

            if (rtbLog.Lines.Length > 200)
            {
                rtbLog.Select(0, rtbLog.GetFirstCharIndexFromLine(50));
                rtbLog.SelectedText = string.Empty;
            }
        }
    }
}
