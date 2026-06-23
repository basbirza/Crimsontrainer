using CrimsonTrainer.Themes;

namespace CrimsonTrainer
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        // ── Controls ──────────────────────────────────────────────────────────
        private System.Windows.Forms.Panel    pnlHeader;
        private System.Windows.Forms.Label    lblTitle;
        private System.Windows.Forms.Label    lblStatus;
        // ── Theme toolbar ─────────────────────────────────────────────────────
        private System.Windows.Forms.Panel    pnlThemeBar;
        private System.Windows.Forms.Button   btnTheme0;
        private System.Windows.Forms.Button   btnTheme1;
        private System.Windows.Forms.Button   btnTheme2;
        private System.Windows.Forms.Button   btnTheme3;
        // ── Entity row ────────────────────────────────────────────────────────
        private System.Windows.Forms.Panel    pnlEntity;
        private System.Windows.Forms.Button   btnFindEntity;
        private System.Windows.Forms.Label    lblEntityStatus;
        // ── Cheats ────────────────────────────────────────────────────────────
        private System.Windows.Forms.Panel    pnlCheats;
        private System.Windows.Forms.Button   btnHealth;
        private System.Windows.Forms.Button   btnStamina;
        private System.Windows.Forms.Button   btnGold;
        private System.Windows.Forms.Button   btnNoCooldown;
        private System.Windows.Forms.Button   btnFreezePos;
        private System.Windows.Forms.Button   btnOneHitKill;
        private System.Windows.Forms.Button   btnNoFallDmg;
        private System.Windows.Forms.Label    lblHotkeyHealth;
        private System.Windows.Forms.Label    lblHotkeyStamina;
        private System.Windows.Forms.Label    lblHotkeyGold;
        private System.Windows.Forms.Label    lblHotkeyNoCooldown;
        private System.Windows.Forms.Label    lblHotkeyFreeze;
        private System.Windows.Forms.Label    lblHotkeyOHK;
        private System.Windows.Forms.Label    lblHotkeyNoFall;
        private System.Windows.Forms.Label    lblDividerCombat;
        // ── Speed ─────────────────────────────────────────────────────────────
        private System.Windows.Forms.Panel    pnlSpeed;
        private System.Windows.Forms.Button   btnSpeedToggle;
        private System.Windows.Forms.TrackBar trkSpeed;
        private System.Windows.Forms.Label    lblSpeedValue;
        private System.Windows.Forms.Label    lblHotkeySpeed;
        private System.Windows.Forms.Label    lblDividerMovement;
        // ── Live stats ────────────────────────────────────────────────────────
        private System.Windows.Forms.Panel    pnlStats;
        private System.Windows.Forms.Panel    pnlHpOuter;
        private System.Windows.Forms.Panel    pnlHpFill;
        private System.Windows.Forms.Label    lblHpLabel;
        private System.Windows.Forms.Label    lblHpVal;
        private System.Windows.Forms.Panel    pnlStaOuter;
        private System.Windows.Forms.Panel    pnlStaFill;
        private System.Windows.Forms.Label    lblStaLabel;
        private System.Windows.Forms.Label    lblStaVal;
        private System.Windows.Forms.Panel    pnlSpiOuter;
        private System.Windows.Forms.Panel    pnlSpiFill;
        private System.Windows.Forms.Label    lblSpiLabel;
        private System.Windows.Forms.Label    lblSpiVal;
        // ── Teleport ─────────────────────────────────────────────────────────
        private System.Windows.Forms.Label    lblLiveCoords;
        private System.Windows.Forms.Panel    pnlTeleport;
        private System.Windows.Forms.Button   btnSavePos;
        private System.Windows.Forms.Button   btnTeleport;
        private System.Windows.Forms.Label    lblCoords;
        private System.Windows.Forms.TextBox  txtX;
        private System.Windows.Forms.TextBox  txtY;
        private System.Windows.Forms.TextBox  txtZ;
        private System.Windows.Forms.Label    lblXLabel;
        private System.Windows.Forms.Label    lblYLabel;
        private System.Windows.Forms.Label    lblZLabel;
        private System.Windows.Forms.Button   btnTeleportCustom;
        // ── Log ──────────────────────────────────────────────────────────────
        private System.Windows.Forms.GroupBox grpLog;
        private System.Windows.Forms.RichTextBox rtbLog;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader           = new System.Windows.Forms.Panel();
            this.lblTitle            = new System.Windows.Forms.Label();
            this.lblStatus           = new System.Windows.Forms.Label();
            this.pnlThemeBar         = new System.Windows.Forms.Panel();
            this.btnTheme0           = new System.Windows.Forms.Button();
            this.btnTheme1           = new System.Windows.Forms.Button();
            this.btnTheme2           = new System.Windows.Forms.Button();
            this.btnTheme3           = new System.Windows.Forms.Button();
            this.pnlEntity           = new System.Windows.Forms.Panel();
            this.btnFindEntity       = new System.Windows.Forms.Button();
            this.lblEntityStatus     = new System.Windows.Forms.Label();
            this.pnlCheats           = new System.Windows.Forms.Panel();
            this.btnHealth           = new System.Windows.Forms.Button();
            this.lblHotkeyHealth     = new System.Windows.Forms.Label();
            this.btnStamina          = new System.Windows.Forms.Button();
            this.lblHotkeyStamina    = new System.Windows.Forms.Label();
            this.btnGold             = new System.Windows.Forms.Button();
            this.lblHotkeyGold       = new System.Windows.Forms.Label();
            this.btnNoCooldown       = new System.Windows.Forms.Button();
            this.lblHotkeyNoCooldown = new System.Windows.Forms.Label();
            this.lblDividerCombat    = new System.Windows.Forms.Label();
            this.btnFreezePos        = new System.Windows.Forms.Button();
            this.lblHotkeyFreeze     = new System.Windows.Forms.Label();
            this.btnOneHitKill       = new System.Windows.Forms.Button();
            this.lblHotkeyOHK        = new System.Windows.Forms.Label();
            this.btnNoFallDmg        = new System.Windows.Forms.Button();
            this.lblHotkeyNoFall     = new System.Windows.Forms.Label();
            this.lblDividerMovement  = new System.Windows.Forms.Label();
            this.pnlSpeed            = new System.Windows.Forms.Panel();
            this.btnSpeedToggle      = new System.Windows.Forms.Button();
            this.trkSpeed            = new System.Windows.Forms.TrackBar();
            this.lblSpeedValue       = new System.Windows.Forms.Label();
            this.lblHotkeySpeed      = new System.Windows.Forms.Label();
            this.pnlStats            = new System.Windows.Forms.Panel();
            this.pnlHpOuter          = new System.Windows.Forms.Panel();
            this.pnlHpFill           = new System.Windows.Forms.Panel();
            this.lblHpLabel          = new System.Windows.Forms.Label();
            this.lblHpVal            = new System.Windows.Forms.Label();
            this.pnlStaOuter         = new System.Windows.Forms.Panel();
            this.pnlStaFill          = new System.Windows.Forms.Panel();
            this.lblStaLabel         = new System.Windows.Forms.Label();
            this.lblStaVal           = new System.Windows.Forms.Label();
            this.pnlSpiOuter         = new System.Windows.Forms.Panel();
            this.pnlSpiFill          = new System.Windows.Forms.Panel();
            this.lblSpiLabel         = new System.Windows.Forms.Label();
            this.lblSpiVal           = new System.Windows.Forms.Label();
            this.lblLiveCoords       = new System.Windows.Forms.Label();
            this.pnlTeleport         = new System.Windows.Forms.Panel();
            this.btnSavePos          = new System.Windows.Forms.Button();
            this.btnTeleport         = new System.Windows.Forms.Button();
            this.lblCoords           = new System.Windows.Forms.Label();
            this.txtX                = new System.Windows.Forms.TextBox();
            this.txtY                = new System.Windows.Forms.TextBox();
            this.txtZ                = new System.Windows.Forms.TextBox();
            this.lblXLabel           = new System.Windows.Forms.Label();
            this.lblYLabel           = new System.Windows.Forms.Label();
            this.lblZLabel           = new System.Windows.Forms.Label();
            this.btnTeleportCustom   = new System.Windows.Forms.Button();
            this.grpLog              = new System.Windows.Forms.GroupBox();
            this.rtbLog              = new System.Windows.Forms.RichTextBox();

            // ── Header panel ─────────────────────────────────────────────────
            this.pnlHeader.Tag       = ThemeManager.TAG_HEADER;
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(20, 20, 30);
            this.pnlHeader.Dock      = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height    = 70;
            this.pnlHeader.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblTitle, this.lblStatus
            });

            this.lblTitle.Tag       = ThemeManager.TAG_TITLE_LABEL;
            this.lblTitle.Text      = "CRIMSON DESERT TRAINER";
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(200, 50, 50);
            this.lblTitle.Font      = new System.Drawing.Font("Segoe UI", 14f, System.Drawing.FontStyle.Bold);
            this.lblTitle.AutoSize  = true;
            this.lblTitle.Location  = new System.Drawing.Point(10, 8);

            this.lblStatus.Tag       = ThemeManager.TAG_STATUS_LABEL;
            this.lblStatus.Text      = "● NOT ATTACHED";
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(200, 80, 80);
            this.lblStatus.Font      = new System.Drawing.Font("Segoe UI", 9f);
            this.lblStatus.AutoSize  = true;
            this.lblStatus.Location  = new System.Drawing.Point(12, 42);

            // ── Theme toolbar ─────────────────────────────────────────────────
            this.pnlThemeBar.Tag       = ThemeManager.TAG_ENTITY_ROW;
            this.pnlThemeBar.Dock      = System.Windows.Forms.DockStyle.Top;
            this.pnlThemeBar.Height    = 34;
            this.pnlThemeBar.BackColor = System.Drawing.Color.FromArgb(18, 18, 28);
            this.pnlThemeBar.Padding   = new System.Windows.Forms.Padding(8, 5, 8, 5);

            BuildThemeButton(this.btnTheme0, "Midnight Forge", new System.Drawing.Point(8,   5));
            BuildThemeButton(this.btnTheme1, "Atelier",        new System.Drawing.Point(108, 5));
            BuildThemeButton(this.btnTheme2, "Sandsworn",      new System.Drawing.Point(208, 5));
            BuildThemeButton(this.btnTheme3, "Override",       new System.Drawing.Point(308, 5));

            this.pnlThemeBar.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.btnTheme0, this.btnTheme1, this.btnTheme2, this.btnTheme3
            });

            // ── Entity panel ──────────────────────────────────────────────────
            this.pnlEntity.Tag       = ThemeManager.TAG_ENTITY_ROW;
            this.pnlEntity.Dock      = System.Windows.Forms.DockStyle.Top;
            this.pnlEntity.Height    = 50;
            this.pnlEntity.BackColor = System.Drawing.Color.FromArgb(25, 25, 38);
            this.pnlEntity.Padding   = new System.Windows.Forms.Padding(10, 8, 10, 8);

            this.btnFindEntity.Tag       = ThemeManager.TAG_FIND_BTN;
            this.btnFindEntity.Text      = "🔍 Find Entity";
            this.btnFindEntity.Location  = new System.Drawing.Point(10, 10);
            this.btnFindEntity.Size      = new System.Drawing.Size(130, 30);
            this.btnFindEntity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFindEntity.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(80, 80, 120);
            this.btnFindEntity.BackColor = System.Drawing.Color.FromArgb(40, 40, 65);
            this.btnFindEntity.ForeColor = System.Drawing.Color.White;
            this.btnFindEntity.Font      = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            this.btnFindEntity.Cursor    = System.Windows.Forms.Cursors.Hand;

            this.lblEntityStatus.Tag       = ThemeManager.TAG_ENTITY_LABEL;
            this.lblEntityStatus.Text      = "Entity: NOT FOUND";
            this.lblEntityStatus.ForeColor = System.Drawing.Color.FromArgb(200, 80, 80);
            this.lblEntityStatus.Font      = new System.Drawing.Font("Segoe UI", 9f);
            this.lblEntityStatus.AutoSize  = true;
            this.lblEntityStatus.Location  = new System.Drawing.Point(155, 18);

            this.pnlEntity.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.btnFindEntity, this.lblEntityStatus
            });

            // ── Cheats panel ─────────────────────────────────────────────────
            this.pnlCheats.Tag       = ThemeManager.TAG_CHEATS_PANEL;
            this.pnlCheats.Dock      = System.Windows.Forms.DockStyle.Top;
            this.pnlCheats.Height    = 335;
            this.pnlCheats.BackColor = System.Drawing.Color.FromArgb(30, 30, 40);
            this.pnlCheats.Padding   = new System.Windows.Forms.Padding(10);

            BuildCheatRow(this.btnHealth,     this.lblHotkeyHealth,     "Infinite Health",  "F1", 10,  10);
            BuildCheatRow(this.btnStamina,    this.lblHotkeyStamina,    "Infinite Stamina", "F2", 10,  55);
            BuildCheatRow(this.btnGold,       this.lblHotkeyGold,       "Infinite Spirit",  "F3", 10, 100);
            BuildCheatRow(this.btnNoCooldown, this.lblHotkeyNoCooldown, "God Mode",         "F4", 10, 145);

            this.btnHealth.Tag     = ThemeManager.TAG_BTN_INACTIVE;
            this.btnStamina.Tag    = ThemeManager.TAG_BTN_INACTIVE;
            this.btnGold.Tag       = ThemeManager.TAG_BTN_INACTIVE;
            this.btnNoCooldown.Tag = ThemeManager.TAG_BTN_INACTIVE;

            this.lblHotkeyHealth.Tag     = ThemeManager.TAG_HOTKEY;
            this.lblHotkeyStamina.Tag    = ThemeManager.TAG_HOTKEY;
            this.lblHotkeyGold.Tag       = ThemeManager.TAG_HOTKEY;
            this.lblHotkeyNoCooldown.Tag = ThemeManager.TAG_HOTKEY;

            // COMBAT divider
            this.lblDividerCombat.Tag       = ThemeManager.TAG_DIVIDER;
            this.lblDividerCombat.Text      = "──────────────  COMBAT  ──────────────";
            this.lblDividerCombat.ForeColor = System.Drawing.Color.FromArgb(80, 80, 100);
            this.lblDividerCombat.Font      = new System.Drawing.Font("Segoe UI", 7.5f);
            this.lblDividerCombat.AutoSize  = false;
            this.lblDividerCombat.Size      = new System.Drawing.Size(390, 14);
            this.lblDividerCombat.Location  = new System.Drawing.Point(10, 190);
            this.lblDividerCombat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            BuildCheatRow(this.btnFreezePos,  this.lblHotkeyFreeze,  "Freeze Position", "F5", 10, 208);
            BuildCheatRow(this.btnOneHitKill, this.lblHotkeyOHK,     "One-Hit Kill",    "F6", 10, 253);
            BuildCheatRow(this.btnNoFallDmg,  this.lblHotkeyNoFall,  "No Fall Damage",  "F7", 10, 298);

            this.btnFreezePos.Tag  = ThemeManager.TAG_BTN_INACTIVE;
            this.btnOneHitKill.Tag = ThemeManager.TAG_BTN_INACTIVE;
            this.btnNoFallDmg.Tag  = ThemeManager.TAG_BTN_INACTIVE;

            this.lblHotkeyFreeze.Tag  = ThemeManager.TAG_HOTKEY;
            this.lblHotkeyOHK.Tag     = ThemeManager.TAG_HOTKEY;
            this.lblHotkeyNoFall.Tag  = ThemeManager.TAG_HOTKEY;

            this.pnlCheats.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.btnHealth,     this.lblHotkeyHealth,
                this.btnStamina,    this.lblHotkeyStamina,
                this.btnGold,       this.lblHotkeyGold,
                this.btnNoCooldown, this.lblHotkeyNoCooldown,
                this.lblDividerCombat,
                this.btnFreezePos,  this.lblHotkeyFreeze,
                this.btnOneHitKill, this.lblHotkeyOHK,
                this.btnNoFallDmg,  this.lblHotkeyNoFall,
            });

            // ── Speed panel ───────────────────────────────────────────────────
            this.pnlSpeed.Tag       = ThemeManager.TAG_CHEATS_PANEL;
            this.pnlSpeed.Dock      = System.Windows.Forms.DockStyle.Top;
            this.pnlSpeed.Height    = 68;
            this.pnlSpeed.BackColor = System.Drawing.Color.FromArgb(25, 25, 38);

            // MOVEMENT divider
            this.lblDividerMovement.Tag       = ThemeManager.TAG_DIVIDER;
            this.lblDividerMovement.Text      = "─────────────  MOVEMENT  ─────────────";
            this.lblDividerMovement.ForeColor = System.Drawing.Color.FromArgb(80, 80, 100);
            this.lblDividerMovement.Font      = new System.Drawing.Font("Segoe UI", 7.5f);
            this.lblDividerMovement.AutoSize  = false;
            this.lblDividerMovement.Size      = new System.Drawing.Size(390, 14);
            this.lblDividerMovement.Location  = new System.Drawing.Point(10, 2);
            this.lblDividerMovement.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            BuildCheatRow(this.btnSpeedToggle, null!, "⚡ Speed Multiplier", "", 10, 21);
            this.btnSpeedToggle.Tag  = ThemeManager.TAG_BTN_INACTIVE;
            this.btnSpeedToggle.Size = new System.Drawing.Size(195, 36);

            this.trkSpeed.Tag           = ThemeManager.TAG_SPEED_SLIDER;
            this.trkSpeed.Location      = new System.Drawing.Point(213, 23);
            this.trkSpeed.Size          = new System.Drawing.Size(140, 30);
            this.trkSpeed.Minimum       = 2;
            this.trkSpeed.Maximum       = 50;
            this.trkSpeed.Value         = 20;
            this.trkSpeed.SmallChange   = 1;
            this.trkSpeed.LargeChange   = 5;
            this.trkSpeed.TickFrequency = 5;
            this.trkSpeed.TickStyle     = System.Windows.Forms.TickStyle.BottomRight;
            this.trkSpeed.BackColor     = System.Drawing.Color.FromArgb(25, 25, 38);

            this.lblSpeedValue.Tag       = ThemeManager.TAG_SPEED_VALUE;
            this.lblSpeedValue.Text      = "2.0×";
            this.lblSpeedValue.ForeColor = System.Drawing.Color.FromArgb(140, 200, 255);
            this.lblSpeedValue.Font      = new System.Drawing.Font("Consolas", 9f, System.Drawing.FontStyle.Bold);
            this.lblSpeedValue.AutoSize  = false;
            this.lblSpeedValue.Size      = new System.Drawing.Size(36, 20);
            this.lblSpeedValue.Location  = new System.Drawing.Point(357, 30);
            this.lblSpeedValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.lblHotkeySpeed.Tag       = ThemeManager.TAG_HOTKEY;
            this.lblHotkeySpeed.Text      = "[F8]";
            this.lblHotkeySpeed.Location  = new System.Drawing.Point(378, 30);
            this.lblHotkeySpeed.AutoSize  = true;
            this.lblHotkeySpeed.ForeColor = System.Drawing.Color.FromArgb(140, 140, 180);
            this.lblHotkeySpeed.Font      = new System.Drawing.Font("Segoe UI", 9f);

            this.pnlSpeed.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblDividerMovement,
                this.btnSpeedToggle, this.trkSpeed, this.lblSpeedValue, this.lblHotkeySpeed,
            });

            // ── Live stats panel ─────────────────────────────────────────────
            this.pnlStats.Tag       = ThemeManager.TAG_STATS_PANEL;
            this.pnlStats.Dock      = System.Windows.Forms.DockStyle.Top;
            this.pnlStats.Height    = 76;
            this.pnlStats.BackColor = System.Drawing.Color.FromArgb(18, 18, 28);

            BuildStatRow(
                this.pnlHpOuter,  this.pnlHpFill,
                this.lblHpLabel,  this.lblHpVal,
                "HP",
                System.Drawing.Color.FromArgb(40, 200, 80), 10);

            BuildStatRow(
                this.pnlStaOuter, this.pnlStaFill,
                this.lblStaLabel, this.lblStaVal,
                "STA",
                System.Drawing.Color.FromArgb(40, 130, 220), 32);

            BuildStatRow(
                this.pnlSpiOuter, this.pnlSpiFill,
                this.lblSpiLabel, this.lblSpiVal,
                "SPI",
                System.Drawing.Color.FromArgb(160, 60, 220), 54);

            this.lblHpLabel.Tag  = ThemeManager.TAG_STAT_LABEL;
            this.lblStaLabel.Tag = ThemeManager.TAG_STAT_LABEL;
            this.lblSpiLabel.Tag = ThemeManager.TAG_STAT_LABEL;

            this.lblHpVal.Tag  = ThemeManager.TAG_STAT_VALUE;
            this.lblStaVal.Tag = ThemeManager.TAG_STAT_VALUE;
            this.lblSpiVal.Tag = ThemeManager.TAG_STAT_VALUE;

            this.pnlHpOuter.Tag  = ThemeManager.TAG_BAR_TRACK;
            this.pnlStaOuter.Tag = ThemeManager.TAG_BAR_TRACK;
            this.pnlSpiOuter.Tag = ThemeManager.TAG_BAR_TRACK;

            this.pnlHpFill.Tag  = ThemeManager.TAG_BAR_HP;
            this.pnlStaFill.Tag = ThemeManager.TAG_BAR_STA;
            this.pnlSpiFill.Tag = ThemeManager.TAG_BAR_SPI;

            this.pnlStats.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblHpLabel,  this.pnlHpOuter,  this.lblHpVal,
                this.lblStaLabel, this.pnlStaOuter, this.lblStaVal,
                this.lblSpiLabel, this.pnlSpiOuter, this.lblSpiVal,
            });

            // ── Teleport panel ───────────────────────────────────────────────
            this.pnlTeleport.Tag       = ThemeManager.TAG_TELEPORT;
            this.pnlTeleport.Dock      = System.Windows.Forms.DockStyle.Top;
            this.pnlTeleport.Height    = 137;
            this.pnlTeleport.BackColor = System.Drawing.Color.FromArgb(28, 28, 42);

            this.lblLiveCoords.Tag       = ThemeManager.TAG_LIVE_POS;
            this.lblLiveCoords.Text      = "Current: --";
            this.lblLiveCoords.ForeColor = System.Drawing.Color.FromArgb(100, 170, 100);
            this.lblLiveCoords.Font      = new System.Drawing.Font("Consolas", 7.5f);
            this.lblLiveCoords.AutoSize  = true;
            this.lblLiveCoords.Location  = new System.Drawing.Point(10, 10);

            this.btnSavePos.Tag       = ThemeManager.TAG_SAVE_BTN;
            this.btnSavePos.Text      = "📍 Save Position";
            this.btnSavePos.Location  = new System.Drawing.Point(10, 32);
            this.btnSavePos.Size      = new System.Drawing.Size(140, 30);
            this.btnSavePos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSavePos.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(80, 120, 80);
            this.btnSavePos.BackColor = System.Drawing.Color.FromArgb(35, 65, 35);
            this.btnSavePos.ForeColor = System.Drawing.Color.White;
            this.btnSavePos.Font      = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            this.btnSavePos.Cursor    = System.Windows.Forms.Cursors.Hand;

            this.btnTeleport.Tag       = ThemeManager.TAG_TP_BTN;
            this.btnTeleport.Text      = "🚀 Teleport";
            this.btnTeleport.Location  = new System.Drawing.Point(158, 32);
            this.btnTeleport.Size      = new System.Drawing.Size(120, 30);
            this.btnTeleport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTeleport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(80, 80, 140);
            this.btnTeleport.BackColor = System.Drawing.Color.FromArgb(35, 35, 80);
            this.btnTeleport.ForeColor = System.Drawing.Color.White;
            this.btnTeleport.Font      = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            this.btnTeleport.Cursor    = System.Windows.Forms.Cursors.Hand;

            this.lblCoords.Tag       = ThemeManager.TAG_SAVED_COORDS;
            this.lblCoords.Text      = "Saved: --";
            this.lblCoords.ForeColor = System.Drawing.Color.FromArgb(140, 200, 140);
            this.lblCoords.Font      = new System.Drawing.Font("Consolas", 8f);
            this.lblCoords.AutoSize  = true;
            this.lblCoords.Location  = new System.Drawing.Point(10, 70);

            int row2y = 94;
            this.lblXLabel.Tag = ThemeManager.TAG_COORD_LABEL;
            this.lblXLabel.Text = "X:"; this.lblXLabel.AutoSize = true;
            this.lblXLabel.ForeColor = System.Drawing.Color.Silver;
            this.lblXLabel.Location = new System.Drawing.Point(10, row2y + 3);

            this.txtX.Tag = ThemeManager.TAG_COORD_INPUT;
            this.txtX.Location = new System.Drawing.Point(26, row2y); this.txtX.Size = new System.Drawing.Size(72, 22);
            this.txtX.BackColor = System.Drawing.Color.FromArgb(40, 40, 55); this.txtX.ForeColor = System.Drawing.Color.White;
            this.txtX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtX.Font = new System.Drawing.Font("Consolas", 8.5f);

            this.lblYLabel.Tag = ThemeManager.TAG_COORD_LABEL;
            this.lblYLabel.Text = "Y:"; this.lblYLabel.AutoSize = true;
            this.lblYLabel.ForeColor = System.Drawing.Color.Silver;
            this.lblYLabel.Location = new System.Drawing.Point(104, row2y + 3);

            this.txtY.Tag = ThemeManager.TAG_COORD_INPUT;
            this.txtY.Location = new System.Drawing.Point(118, row2y); this.txtY.Size = new System.Drawing.Size(72, 22);
            this.txtY.BackColor = System.Drawing.Color.FromArgb(40, 40, 55); this.txtY.ForeColor = System.Drawing.Color.White;
            this.txtY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtY.Font = new System.Drawing.Font("Consolas", 8.5f);

            this.lblZLabel.Tag = ThemeManager.TAG_COORD_LABEL;
            this.lblZLabel.Text = "Z:"; this.lblZLabel.AutoSize = true;
            this.lblZLabel.ForeColor = System.Drawing.Color.Silver;
            this.lblZLabel.Location = new System.Drawing.Point(198, row2y + 3);

            this.txtZ.Tag = ThemeManager.TAG_COORD_INPUT;
            this.txtZ.Location = new System.Drawing.Point(212, row2y); this.txtZ.Size = new System.Drawing.Size(72, 22);
            this.txtZ.BackColor = System.Drawing.Color.FromArgb(40, 40, 55); this.txtZ.ForeColor = System.Drawing.Color.White;
            this.txtZ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtZ.Font = new System.Drawing.Font("Consolas", 8.5f);

            this.btnTeleportCustom.Tag       = ThemeManager.TAG_GO_BTN;
            this.btnTeleportCustom.Text      = "Go";
            this.btnTeleportCustom.Location  = new System.Drawing.Point(290, row2y - 1);
            this.btnTeleportCustom.Size      = new System.Drawing.Size(40, 24);
            this.btnTeleportCustom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTeleportCustom.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(80, 80, 140);
            this.btnTeleportCustom.BackColor = System.Drawing.Color.FromArgb(35, 35, 80);
            this.btnTeleportCustom.ForeColor = System.Drawing.Color.White;
            this.btnTeleportCustom.Font      = new System.Drawing.Font("Segoe UI", 8f, System.Drawing.FontStyle.Bold);
            this.btnTeleportCustom.Cursor    = System.Windows.Forms.Cursors.Hand;

            this.pnlTeleport.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblLiveCoords,
                this.btnSavePos, this.btnTeleport, this.lblCoords,
                this.lblXLabel, this.txtX, this.lblYLabel, this.txtY,
                this.lblZLabel, this.txtZ, this.btnTeleportCustom
            });

            // ── Log group ────────────────────────────────────────────────────
            this.grpLog.Tag       = ThemeManager.TAG_LOG_PANEL;
            this.grpLog.Text      = "Log";
            this.grpLog.ForeColor = System.Drawing.Color.Silver;
            this.grpLog.Dock      = System.Windows.Forms.DockStyle.Fill;
            this.grpLog.BackColor = System.Drawing.Color.FromArgb(25, 25, 35);
            this.grpLog.Controls.Add(this.rtbLog);

            this.rtbLog.Tag         = ThemeManager.TAG_LOG_BOX;
            this.rtbLog.Dock        = System.Windows.Forms.DockStyle.Fill;
            this.rtbLog.BackColor   = System.Drawing.Color.FromArgb(15, 15, 20);
            this.rtbLog.ForeColor   = System.Drawing.Color.LightGreen;
            this.rtbLog.Font        = new System.Drawing.Font("Consolas", 8.5f);
            this.rtbLog.ReadOnly    = true;
            this.rtbLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbLog.ScrollBars  = System.Windows.Forms.RichTextBoxScrollBars.Vertical;

            // ── Form ─────────────────────────────────────────────────────────
            this.Tag             = ThemeManager.TAG_WIN_BG;
            this.Text            = "Crimson Desert Trainer";
            this.BackColor       = System.Drawing.Color.FromArgb(30, 30, 40);
            this.ForeColor       = System.Drawing.Color.White;
            this.Size            = new System.Drawing.Size(420, 992);
            this.MinimumSize     = new System.Drawing.Size(420, 874);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox     = false;
            this.StartPosition   = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Font            = new System.Drawing.Font("Segoe UI", 9f);

            // Controls are added bottom-up because DockStyle.Top stacks from the top.
            this.Controls.Add(this.grpLog);
            this.Controls.Add(this.pnlTeleport);
            this.Controls.Add(this.pnlSpeed);
            this.Controls.Add(this.pnlCheats);
            this.Controls.Add(this.pnlStats);
            this.Controls.Add(this.pnlEntity);
            this.Controls.Add(this.pnlThemeBar);
            this.Controls.Add(this.pnlHeader);

            this.components = new System.ComponentModel.Container();
        }

        // ── Constants ─────────────────────────────────────────────────────────

        internal const int StatBarMaxWidth = 210;

        // ── Helpers ───────────────────────────────────────────────────────────

        private static void BuildThemeButton(
            System.Windows.Forms.Button btn,
            string label,
            System.Drawing.Point location)
        {
            btn.Tag       = ThemeManager.TAG_THEME_BTN;
            btn.Text      = label;
            btn.Location  = location;
            btn.Size      = new System.Drawing.Size(97, 24);
            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(60, 60, 80);
            btn.FlatAppearance.BorderSize  = 1;
            btn.BackColor  = System.Drawing.Color.FromArgb(35, 35, 55);
            btn.ForeColor  = System.Drawing.Color.FromArgb(180, 180, 200);
            btn.Font       = new System.Drawing.Font("Segoe UI", 7.5f, System.Drawing.FontStyle.Bold);
            btn.Cursor     = System.Windows.Forms.Cursors.Hand;
        }

        private static void BuildStatRow(
            System.Windows.Forms.Panel outer,
            System.Windows.Forms.Panel fill,
            System.Windows.Forms.Label nameLabel,
            System.Windows.Forms.Label valLabel,
            string name,
            System.Drawing.Color barColor,
            int y)
        {
            nameLabel.Text      = name;
            nameLabel.ForeColor = System.Drawing.Color.FromArgb(160, 160, 160);
            nameLabel.Font      = new System.Drawing.Font("Consolas", 8f);
            nameLabel.AutoSize  = false;
            nameLabel.Size      = new System.Drawing.Size(28, 14);
            nameLabel.Location  = new System.Drawing.Point(10, y + 3);

            outer.Location  = new System.Drawing.Point(40, y + 3);
            outer.Size      = new System.Drawing.Size(StatBarMaxWidth, 8);
            outer.BackColor = System.Drawing.Color.FromArgb(40, 40, 55);

            fill.Location  = new System.Drawing.Point(0, 0);
            fill.Size      = new System.Drawing.Size(0, 8);
            fill.BackColor = barColor;
            outer.Controls.Add(fill);

            valLabel.Text      = "--";
            valLabel.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            valLabel.Font      = new System.Drawing.Font("Consolas", 7.5f);
            valLabel.AutoSize  = false;
            valLabel.Size      = new System.Drawing.Size(130, 14);
            valLabel.Location  = new System.Drawing.Point(256, y + 3);
        }

        private static void BuildCheatRow(
            System.Windows.Forms.Button btn,
            System.Windows.Forms.Label? lbl,
            string text, string hotkey,
            int x, int y)
        {
            btn.Text      = text;
            btn.Location  = new System.Drawing.Point(x, y);
            btn.Size      = new System.Drawing.Size(250, 36);
            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(80, 80, 100);
            btn.FlatAppearance.BorderSize  = 1;
            btn.BackColor  = System.Drawing.Color.FromArgb(45, 45, 60);
            btn.ForeColor  = System.Drawing.Color.White;
            btn.Font       = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
            btn.TextAlign  = System.Drawing.ContentAlignment.MiddleLeft;
            btn.Padding    = new System.Windows.Forms.Padding(8, 0, 0, 0);
            btn.Cursor     = System.Windows.Forms.Cursors.Hand;

            if (lbl != null)
            {
                lbl.Text      = $"[{hotkey}]";
                lbl.Location  = new System.Drawing.Point(x + 260, y + 9);
                lbl.AutoSize  = true;
                lbl.ForeColor = System.Drawing.Color.FromArgb(140, 140, 180);
                lbl.Font      = new System.Drawing.Font("Segoe UI", 9f);
            }
        }
    }
}
