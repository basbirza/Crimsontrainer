using System.Drawing;

namespace CrimsonTrainer.Themes
{
    /// <summary>All colours and fonts that define one visual theme.</summary>
    public sealed class TrainerTheme
    {
        public string Name { get; init; } = "";

        // ── Window chrome ──────────────────────────────────────────────────
        public Color WinBackground   { get; init; }
        public Color TitleBarBg      { get; init; }
        public Color TitleBarText    { get; init; }

        // ── Header ─────────────────────────────────────────────────────────
        public Color HeaderBg        { get; init; }
        public Color HeaderTitle     { get; init; }
        public Color StatusGreen     { get; init; }

        // ── Entity row ─────────────────────────────────────────────────────
        public Color EntityBg        { get; init; }
        public Color FindBtnBg       { get; init; }
        public Color FindBtnFg       { get; init; }
        public Color FindBtnBorder   { get; init; }
        public Color EntityLabelFg   { get; init; }

        // ── Section headers ────────────────────────────────────────────────
        public Color SectionBg       { get; init; }
        public Color SectionFg       { get; init; }

        // ── Stats panel ────────────────────────────────────────────────────
        public Color StatsBg         { get; init; }
        public Color StatBarTrack    { get; init; }
        public Color StatLabelFg     { get; init; }
        public Color StatValueFg     { get; init; }
        public Color BarHpStart      { get; init; }
        public Color BarHpEnd        { get; init; }
        public Color BarStaStart     { get; init; }
        public Color BarStaEnd       { get; init; }
        public Color BarSpiStart     { get; init; }
        public Color BarSpiEnd       { get; init; }

        // ── Cheats panel ───────────────────────────────────────────────────
        public Color CheatsBg        { get; init; }
        public Color DividerLine     { get; init; }
        public Color DividerText     { get; init; }
        public Color BtnInactiveBg   { get; init; }
        public Color BtnInactiveFg   { get; init; }
        public Color BtnInactiveBorder { get; init; }
        public Color BtnActiveBg     { get; init; }
        public Color BtnActiveFg     { get; init; }
        public Color BtnActiveBorder { get; init; }
        public Color HotkeyFg        { get; init; }

        // ── Speed slider ───────────────────────────────────────────────────
        public Color SliderTrack     { get; init; }
        public Color SliderThumb     { get; init; }
        public Color SpeedValueFg    { get; init; }

        // ── Teleport panel ─────────────────────────────────────────────────
        public Color TeleportBg      { get; init; }
        public Color LivePosFg       { get; init; }
        public Color SaveBtnBg       { get; init; }
        public Color SaveBtnFg       { get; init; }
        public Color SaveBtnBorder   { get; init; }
        public Color TpBtnBg         { get; init; }
        public Color TpBtnFg         { get; init; }
        public Color TpBtnBorder     { get; init; }
        public Color SavedCoordsFg   { get; init; }
        public Color CoordLabelFg    { get; init; }
        public Color CoordInputBg    { get; init; }
        public Color CoordInputFg    { get; init; }
        public Color CoordInputBorder { get; init; }
        public Color GoBtnBg         { get; init; }
        public Color GoBtnFg         { get; init; }
        public Color GoBtnBorder     { get; init; }

        // ── Log ────────────────────────────────────────────────────────────
        public Color LogPanelBg      { get; init; }
        public Color LogBg           { get; init; }
        public Color LogFg           { get; init; }
        public Color LogTitleFg      { get; init; }

        // ── Fonts ──────────────────────────────────────────────────────────
        public string UIFontFamily   { get; init; } = "Segoe UI";
        public string MonoFontFamily { get; init; } = "Consolas";
        public string TitleFontFamily { get; init; } = "Segoe UI";
    }
}
