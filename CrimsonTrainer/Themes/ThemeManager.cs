using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CrimsonTrainer.Themes
{
    /// <summary>
    /// Loads bundled font files and applies themes to the trainer window.
    /// </summary>
    public sealed class ThemeManager : IDisposable
    {
        private readonly PrivateFontCollection _pfc = new();
        private bool _fontsLoaded;

        // Tag constants — set on controls in Designer so the painter can
        // identify each role without relying on fragile name matching.
        public const string TAG_WIN_BG        = "win_bg";
        public const string TAG_TITLEBAR      = "titlebar";
        public const string TAG_HEADER        = "header";
        public const string TAG_TITLE_LABEL   = "title_label";
        public const string TAG_STATUS_LABEL  = "status_label";
        public const string TAG_ENTITY_ROW    = "entity_row";
        public const string TAG_FIND_BTN      = "find_btn";
        public const string TAG_ENTITY_LABEL  = "entity_label";
        public const string TAG_SECTION       = "section";
        public const string TAG_STATS_PANEL   = "stats_panel";
        public const string TAG_STAT_LABEL    = "stat_label";
        public const string TAG_STAT_VALUE    = "stat_value";
        public const string TAG_BAR_TRACK     = "bar_track";
        public const string TAG_BAR_HP        = "bar_hp";
        public const string TAG_BAR_STA       = "bar_sta";
        public const string TAG_BAR_SPI       = "bar_spi";
        public const string TAG_CHEATS_PANEL  = "cheats_panel";
        public const string TAG_DIVIDER       = "divider";
        public const string TAG_BTN_INACTIVE  = "btn_inactive";
        public const string TAG_BTN_ACTIVE    = "btn_active";    // currently active cheat
        public const string TAG_HOTKEY        = "hotkey";
        public const string TAG_SPEED_SLIDER  = "speed_slider";
        public const string TAG_SPEED_VALUE   = "speed_value";
        public const string TAG_TELEPORT      = "teleport";
        public const string TAG_LIVE_POS      = "live_pos";
        public const string TAG_SAVE_BTN      = "save_btn";
        public const string TAG_TP_BTN        = "tp_btn";
        public const string TAG_SAVED_COORDS  = "saved_coords";
        public const string TAG_COORD_LABEL   = "coord_label";
        public const string TAG_COORD_INPUT   = "coord_input";
        public const string TAG_GO_BTN        = "go_btn";
        public const string TAG_LOG_PANEL     = "log_panel";
        public const string TAG_LOG_BOX       = "log_box";
        public const string TAG_LOG_TITLE     = "log_title";
        public const string TAG_THEME_BTN     = "theme_btn";

        public TrainerTheme Current { get; private set; } = ThemeDefinitions.MidnightForge;

        // ── Font access ───────────────────────────────────────────────────

        public void EnsureFontsLoaded()
        {
            if (_fontsLoaded) return;
            _fontsLoaded = true;

            LoadEmbeddedFont("CrimsonTrainer.Fonts.SpaceGrotesk.ttf");
            LoadEmbeddedFont("CrimsonTrainer.Fonts.Manrope.ttf");
            LoadEmbeddedFont("CrimsonTrainer.Fonts.EBGaramond.ttf");
            LoadEmbeddedFont("CrimsonTrainer.Fonts.Cinzel.ttf");
            LoadEmbeddedFont("CrimsonTrainer.Fonts.JetBrainsMono.ttf");
            LoadEmbeddedFont("CrimsonTrainer.Fonts.JetBrainsMono-Bold.ttf");
        }

        private void LoadEmbeddedFont(string resourceName)
        {
            using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
            if (stream == null) return;

            var data = new byte[stream.Length];
            _ = stream.Read(data, 0, data.Length);

            var handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            try { _pfc.AddMemoryFont(handle.AddrOfPinnedObject(), data.Length); }
            finally { handle.Free(); }
        }

        public Font GetFont(string family, float size, FontStyle style = FontStyle.Regular)
        {
            // Try the private collection first, fall back to system font.
            foreach (FontFamily ff in _pfc.Families)
            {
                if (ff.Name.Equals(family, StringComparison.OrdinalIgnoreCase))
                {
                    // Variable fonts may not report Bold/Italic as available styles;
                    // fall back to Regular so we never throw.
                    if (!ff.IsStyleAvailable(style)) style = FontStyle.Regular;
                    return new Font(ff, size, style);
                }
            }
            return new Font(family, size, style);   // system font fallback
        }

        // ── Apply theme to a whole form ───────────────────────────────────

        public void Apply(Control root, TrainerTheme theme)
        {
            Current = theme;
            EnsureFontsLoaded();
            ApplyRecursive(root, theme);
        }

        private void ApplyRecursive(Control ctrl, TrainerTheme t)
        {
            string tag = ctrl.Tag as string ?? "";

            switch (tag)
            {
                case TAG_WIN_BG:
                    ctrl.BackColor = t.WinBackground;
                    break;

                case TAG_TITLEBAR:
                    ctrl.BackColor = t.TitleBarBg;
                    ctrl.ForeColor = t.TitleBarText;
                    SetFont(ctrl, t.MonoFontFamily, 9f);
                    break;

                case TAG_HEADER:
                    ctrl.BackColor = t.HeaderBg;
                    break;

                case TAG_TITLE_LABEL:
                    ctrl.BackColor = t.HeaderBg;
                    ctrl.ForeColor = t.HeaderTitle;
                    SetFont(ctrl, t.TitleFontFamily, 13f, FontStyle.Bold);
                    break;

                case TAG_STATUS_LABEL:
                    ctrl.BackColor = t.HeaderBg;
                    ctrl.ForeColor = t.StatusGreen;
                    SetFont(ctrl, t.MonoFontFamily, 9f);
                    break;

                case TAG_ENTITY_ROW:
                    ctrl.BackColor = t.EntityBg;
                    break;

                case TAG_FIND_BTN when ctrl is Button btn:
                    btn.BackColor = t.FindBtnBg;
                    btn.ForeColor = t.FindBtnFg;
                    btn.FlatAppearance.BorderColor = t.FindBtnBorder;
                    SetFont(ctrl, t.UIFontFamily, 9.5f, FontStyle.Bold);
                    break;

                case TAG_ENTITY_LABEL:
                    ctrl.BackColor = t.EntityBg;
                    ctrl.ForeColor = t.EntityLabelFg;
                    SetFont(ctrl, t.MonoFontFamily, 9f);
                    break;

                case TAG_SECTION:
                    ctrl.BackColor = t.SectionBg;
                    ctrl.ForeColor = t.SectionFg;
                    SetFont(ctrl, t.UIFontFamily, 8f, FontStyle.Bold);
                    break;

                case TAG_STATS_PANEL:
                    ctrl.BackColor = t.StatsBg;
                    break;

                case TAG_STAT_LABEL:
                    ctrl.BackColor = t.StatsBg;
                    ctrl.ForeColor = t.StatLabelFg;
                    SetFont(ctrl, t.UIFontFamily, 8.5f);
                    break;

                case TAG_STAT_VALUE:
                    ctrl.BackColor = t.StatsBg;
                    ctrl.ForeColor = t.StatValueFg;
                    SetFont(ctrl, t.MonoFontFamily, 8.5f);
                    break;

                case TAG_BAR_TRACK:
                    ctrl.BackColor = t.StatBarTrack;
                    break;

                case TAG_BAR_HP:
                    ctrl.BackColor = t.BarHpEnd;
                    break;

                case TAG_BAR_STA:
                    ctrl.BackColor = t.BarStaEnd;
                    break;

                case TAG_BAR_SPI:
                    ctrl.BackColor = t.BarSpiEnd;
                    break;

                case TAG_CHEATS_PANEL:
                    ctrl.BackColor = t.CheatsBg;
                    break;

                case TAG_DIVIDER:
                    ctrl.BackColor = t.CheatsBg;
                    ctrl.ForeColor = t.DividerText;
                    SetFont(ctrl, t.UIFontFamily, 8f, FontStyle.Bold);
                    break;

                case TAG_BTN_INACTIVE when ctrl is Button b:
                    ApplyCheatButton(b, t, false);
                    break;

                case TAG_BTN_ACTIVE when ctrl is Button b:
                    ApplyCheatButton(b, t, true);
                    break;

                case TAG_HOTKEY:
                    ctrl.BackColor = t.CheatsBg;
                    ctrl.ForeColor = t.HotkeyFg;
                    SetFont(ctrl, t.MonoFontFamily, 8.5f);
                    break;

                case TAG_SPEED_VALUE:
                    ctrl.BackColor = t.CheatsBg;
                    ctrl.ForeColor = t.SpeedValueFg;
                    SetFont(ctrl, t.MonoFontFamily, 9.5f, FontStyle.Bold);
                    break;

                case TAG_SPEED_SLIDER when ctrl is TrackBar tb:
                    tb.BackColor = t.CheatsBg;
                    break;

                case TAG_TELEPORT:
                    ctrl.BackColor = t.TeleportBg;
                    break;

                case TAG_LIVE_POS:
                    ctrl.BackColor = t.TeleportBg;
                    ctrl.ForeColor = t.LivePosFg;
                    SetFont(ctrl, t.MonoFontFamily, 8.5f);
                    break;

                case TAG_SAVE_BTN when ctrl is Button sb:
                    sb.BackColor = t.SaveBtnBg;
                    sb.ForeColor = t.SaveBtnFg;
                    sb.FlatAppearance.BorderColor = t.SaveBtnBorder;
                    SetFont(ctrl, t.UIFontFamily, 9f, FontStyle.Bold);
                    break;

                case TAG_TP_BTN when ctrl is Button tb2:
                    tb2.BackColor = t.TpBtnBg;
                    tb2.ForeColor = t.TpBtnFg;
                    tb2.FlatAppearance.BorderColor = t.TpBtnBorder;
                    SetFont(ctrl, t.UIFontFamily, 9f, FontStyle.Bold);
                    break;

                case TAG_SAVED_COORDS:
                    ctrl.BackColor = t.TeleportBg;
                    ctrl.ForeColor = t.SavedCoordsFg;
                    SetFont(ctrl, t.MonoFontFamily, 8.5f);
                    break;

                case TAG_COORD_LABEL:
                    ctrl.BackColor = t.TeleportBg;
                    ctrl.ForeColor = t.CoordLabelFg;
                    SetFont(ctrl, t.UIFontFamily, 9f);
                    break;

                case TAG_COORD_INPUT when ctrl is TextBox tx:
                    tx.BackColor = t.CoordInputBg;
                    tx.ForeColor = t.CoordInputFg;
                    SetFont(ctrl, t.MonoFontFamily, 9f);
                    break;

                case TAG_GO_BTN when ctrl is Button gb:
                    gb.BackColor = t.GoBtnBg;
                    gb.ForeColor = t.GoBtnFg;
                    gb.FlatAppearance.BorderColor = t.GoBtnBorder;
                    SetFont(ctrl, t.UIFontFamily, 9f, FontStyle.Bold);
                    break;

                case TAG_LOG_PANEL:
                    ctrl.BackColor = t.LogPanelBg;
                    break;

                case TAG_LOG_BOX when ctrl is RichTextBox rtb:
                    rtb.BackColor = t.LogBg;
                    rtb.ForeColor = t.LogFg;
                    SetFont(ctrl, t.MonoFontFamily, 8.5f);
                    break;

                case TAG_LOG_TITLE:
                    ctrl.BackColor = t.LogPanelBg;
                    ctrl.ForeColor = t.LogTitleFg;
                    SetFont(ctrl, t.UIFontFamily, 8f, FontStyle.Bold);
                    break;

                case TAG_THEME_BTN when ctrl is Button thb:
                    thb.BackColor = t.EntityBg;
                    thb.ForeColor = t.EntityLabelFg;
                    thb.FlatAppearance.BorderColor = t.FindBtnBorder;
                    SetFont(ctrl, t.UIFontFamily, 8f, FontStyle.Bold);
                    break;
            }

            foreach (Control child in ctrl.Controls)
                ApplyRecursive(child, t);
        }

        // ── Helpers ───────────────────────────────────────────────────────

        public void ApplyCheatButton(Button btn, TrainerTheme t, bool active)
        {
            btn.BackColor = active ? t.BtnActiveBg   : t.BtnInactiveBg;
            btn.ForeColor = active ? t.BtnActiveFg   : t.BtnInactiveFg;
            btn.FlatAppearance.BorderColor = active ? t.BtnActiveBorder : t.BtnInactiveBorder;
            SetFont(btn, t.UIFontFamily, 9.5f, FontStyle.Bold);
        }

        private void SetFont(Control ctrl, string family, float size,
                             FontStyle style = FontStyle.Regular)
        {
            var old = ctrl.Font;
            ctrl.Font = GetFont(family, size, style);
            old?.Dispose();
        }

        public void Dispose() => _pfc.Dispose();
    }
}
