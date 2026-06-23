using System.Drawing;

namespace CrimsonTrainer.Themes
{
    public static class ThemeDefinitions
    {
        private static Color C(string hex)
        {
            hex = hex.TrimStart('#');
            return Color.FromArgb(
                Convert.ToInt32(hex[0..2], 16),
                Convert.ToInt32(hex[2..4], 16),
                Convert.ToInt32(hex[4..6], 16));
        }

        // ════════════════════════════════════════════════════════════
        //  01 · MIDNIGHT FORGE — refined dark / crimson
        // ════════════════════════════════════════════════════════════
        public static readonly TrainerTheme MidnightForge = new()
        {
            Name = "Midnight Forge",

            WinBackground    = C("15161c"),
            TitleBarBg       = C("0f1015"),
            TitleBarText     = C("6b6d7e"),

            HeaderBg         = C("101119"),
            HeaderTitle      = C("e0383c"),
            StatusGreen      = C("34c860"),

            EntityBg         = C("14151d"),
            FindBtnBg        = C("1d1f2b"),
            FindBtnFg        = C("c9cbd8"),
            FindBtnBorder    = C("383b52"),
            EntityLabelFg    = C("34c860"),

            SectionBg        = C("16171f"),
            SectionFg        = C("4d4f63"),

            StatsBg          = C("0f1018"),
            StatBarTrack     = C("20222e"),
            StatLabelFg      = C("8a8c9e"),
            StatValueFg      = C("6a6c80"),
            BarHpStart       = C("28a840"), BarHpEnd  = C("50e870"),
            BarStaStart      = C("2070c8"), BarStaEnd = C("40a0f0"),
            BarSpiStart      = C("8020c0"), BarSpiEnd = C("c050f0"),

            CheatsBg         = C("16171f"),
            DividerLine      = C("2a2c3a"),
            DividerText      = C("4a4c60"),
            BtnInactiveBg    = C("1c1d27"),
            BtnInactiveFg    = C("c4c6d4"),
            BtnInactiveBorder= C("34364a"),
            BtnActiveBg      = C("2c1418"),
            BtnActiveFg      = C("ffffff"),
            BtnActiveBorder  = C("c0303a"),
            HotkeyFg         = C("5a5c70"),

            SliderTrack      = C("34364a"),
            SliderThumb      = C("e0484c"),
            SpeedValueFg     = C("ff7a7e"),

            TeleportBg       = C("14151d"),
            LivePosFg        = C("5a7a5e"),
            SaveBtnBg        = C("16271a"),
            SaveBtnFg        = C("b6d6bc"),
            SaveBtnBorder    = C("2f5a38"),
            TpBtnBg          = C("161a2e"),
            TpBtnFg          = C("b6bce0"),
            TpBtnBorder      = C("34406e"),
            SavedCoordsFg    = C("5a9860"),
            CoordLabelFg     = C("888aa0"),
            CoordInputBg     = C("1a1c28"),
            CoordInputFg     = C("cfd2e0"),
            CoordInputBorder = C("34364a"),
            GoBtnBg          = C("2c1418"),
            GoBtnFg          = C("ff8a8e"),
            GoBtnBorder      = C("c0303a"),

            LogPanelBg       = C("101119"),
            LogBg            = C("0b0c12"),
            LogFg            = C("50d860"),
            LogTitleFg       = C("4d4f63"),

            UIFontFamily     = "Space Grotesk",
            MonoFontFamily   = "JetBrains Mono",
            TitleFontFamily  = "Space Grotesk",
        };

        // ════════════════════════════════════════════════════════════
        //  02 · ATELIER — sleek modern / light
        // ════════════════════════════════════════════════════════════
        public static readonly TrainerTheme Atelier = new()
        {
            Name = "Atelier",

            WinBackground    = C("ffffff"),
            TitleBarBg       = C("fafafb"),
            TitleBarText     = C("a0a0ac"),

            HeaderBg         = C("ffffff"),
            HeaderTitle      = C("18181f"),
            StatusGreen      = C("12a06a"),

            EntityBg         = C("fafafb"),
            FindBtnBg        = C("ffffff"),
            FindBtnFg        = C("44454f"),
            FindBtnBorder    = C("e2e2ea"),
            EntityLabelFg    = C("12a06a"),

            SectionBg        = C("fafafb"),
            SectionFg        = C("a6a6b2"),

            StatsBg          = C("ffffff"),
            StatBarTrack     = C("ededf2"),
            StatLabelFg      = C("6a6a76"),
            StatValueFg      = C("9a9aa6"),
            BarHpStart       = C("12a06a"), BarHpEnd  = C("12a06a"),
            BarStaStart      = C("3b82f6"), BarStaEnd = C("3b82f6"),
            BarSpiStart      = C("8b5cf6"), BarSpiEnd = C("8b5cf6"),

            CheatsBg         = C("ffffff"),
            DividerLine      = C("ededf2"),
            DividerText      = C("b4b4c0"),
            BtnInactiveBg    = C("f6f6f9"),
            BtnInactiveFg    = C("3a3b45"),
            BtnInactiveBorder= C("ededf2"),
            BtnActiveBg      = C("e6f7ef"),
            BtnActiveFg      = C("0c6b46"),
            BtnActiveBorder  = C("9fe0c2"),
            HotkeyFg         = C("b4b4c0"),

            SliderTrack      = C("e3e3ea"),
            SliderThumb      = C("12a06a"),
            SpeedValueFg     = C("12a06a"),

            TeleportBg       = C("fafafb"),
            LivePosFg        = C("6a6a76"),
            SaveBtnBg        = C("e6f7ef"),
            SaveBtnFg        = C("0c6b46"),
            SaveBtnBorder    = C("b6e6cd"),
            TpBtnBg          = C("eef2ff"),
            TpBtnFg          = C("2952c8"),
            TpBtnBorder      = C("c7d6ff"),
            SavedCoordsFg    = C("6a6a76"),
            CoordLabelFg     = C("9a9aa6"),
            CoordInputBg     = C("ffffff"),
            CoordInputFg     = C("3a3b45"),
            CoordInputBorder = C("e2e2ea"),
            GoBtnBg          = C("12a06a"),
            GoBtnFg          = C("ffffff"),
            GoBtnBorder      = C("12a06a"),

            LogPanelBg       = C("ffffff"),
            LogBg            = C("f6f6f9"),
            LogFg            = C("3a3b45"),
            LogTitleFg       = C("a6a6b2"),

            UIFontFamily     = "Manrope",
            MonoFontFamily   = "JetBrains Mono",
            TitleFontFamily  = "Manrope",
        };

        // ════════════════════════════════════════════════════════════
        //  03 · SANDSWORN — Crimson Desert lore / ornate gold
        // ════════════════════════════════════════════════════════════
        public static readonly TrainerTheme Sandsworn = new()
        {
            Name = "Sandsworn",

            WinBackground    = C("160f0c"),
            TitleBarBg       = C("0e0908"),
            TitleBarText     = C("8a6f4e"),

            HeaderBg         = C("1c1310"),
            HeaderTitle      = C("c9a24b"),
            StatusGreen      = C("c9a24b"),

            EntityBg         = C("160f0d"),
            FindBtnBg        = C("2a1a14"),
            FindBtnFg        = C("d8b878"),
            FindBtnBorder    = C("5a4026"),
            EntityLabelFg    = C("c9a24b"),

            SectionBg        = C("140d0b"),
            SectionFg        = C("8a6f4e"),

            StatsBg          = C("120b09"),
            StatBarTrack     = C("2a1d16"),
            StatLabelFg      = C("b09060"),
            StatValueFg      = C("9a7e52"),
            BarHpStart       = C("9c2a24"), BarHpEnd  = C("d64a2e"),
            BarStaStart      = C("9c6a1e"), BarStaEnd = C("e0a83a"),
            BarSpiStart      = C("2a6a52"), BarSpiEnd = C("4aa07a"),

            CheatsBg         = C("140d0b"),
            DividerLine      = C("5a4026"),
            DividerText      = C("8a6f4e"),
            BtnInactiveBg    = C("1f1611"),
            BtnInactiveFg    = C("cbb389"),
            BtnInactiveBorder= C("43301f"),
            BtnActiveBg      = C("3a160f"),
            BtnActiveFg      = C("f0d2a0"),
            BtnActiveBorder  = C("b0402a"),
            HotkeyFg         = C("6a543a"),

            SliderTrack      = C("43301f"),
            SliderThumb      = C("c9a24b"),
            SpeedValueFg     = C("e0884a"),

            TeleportBg       = C("160f0d"),
            LivePosFg        = C("7a8a52"),
            SaveBtnBg        = C("21301a"),
            SaveBtnFg        = C("b6c88a"),
            SaveBtnBorder    = C("3f5a28"),
            TpBtnBg          = C("2a1a14"),
            TpBtnFg          = C("d8b878"),
            TpBtnBorder      = C("5a4026"),
            SavedCoordsFg    = C("8aa060"),
            CoordLabelFg     = C("b09060"),
            CoordInputBg     = C("1a120e"),
            CoordInputFg     = C("d8c2a0"),
            CoordInputBorder = C("43301f"),
            GoBtnBg          = C("3a160f"),
            GoBtnFg          = C("e0a060"),
            GoBtnBorder      = C("b0402a"),

            LogPanelBg       = C("100a08"),
            LogBg            = C("0c0806"),
            LogFg            = C("c9a24b"),
            LogTitleFg       = C("8a6f4e"),

            UIFontFamily     = "EB Garamond",
            MonoFontFamily   = "JetBrains Mono",
            TitleFontFamily  = "Cinzel",
        };

        // ════════════════════════════════════════════════════════════
        //  04 · OVERRIDE — cyberpunk hacker terminal
        // ════════════════════════════════════════════════════════════
        public static readonly TrainerTheme Override = new()
        {
            Name = "Override",

            WinBackground    = C("070809"),
            TitleBarBg       = C("0a0c0e"),
            TitleBarText     = C("2a6a66"),

            HeaderBg         = C("070809"),
            HeaderTitle      = C("2ee6d6"),
            StatusGreen      = C("2ee6d6"),

            EntityBg         = C("0a0c0e"),
            FindBtnBg        = C("070809"),
            FindBtnFg        = C("2ee6d6"),
            FindBtnBorder    = C("2ee6d6"),
            EntityLabelFg    = C("2ee6d6"),

            SectionBg        = C("090b0d"),
            SectionFg        = C("2a6a66"),

            StatsBg          = C("070809"),
            StatBarTrack     = C("11201f"),
            StatLabelFg      = C("2ee6d6"),
            StatValueFg      = C("2a8a82"),
            BarHpStart       = C("4ade80"), BarHpEnd  = C("a3ff5e"),
            BarStaStart      = C("2ee6d6"), BarStaEnd = C("5af0ff"),
            BarSpiStart      = C("ff3da6"), BarSpiEnd = C("ff7ac6"),

            CheatsBg         = C("090b0d"),
            DividerLine      = C("173433"),
            DividerText      = C("ff3da6"),
            BtnInactiveBg    = C("0c0f11"),
            BtnInactiveFg    = C("8ad6cf"),
            BtnInactiveBorder= C("173433"),
            BtnActiveBg      = C("0c2a29"),
            BtnActiveFg      = C("2ee6d6"),
            BtnActiveBorder  = C("2ee6d6"),
            HotkeyFg         = C("2a6a66"),

            SliderTrack      = C("173433"),
            SliderThumb      = C("2ee6d6"),
            SpeedValueFg     = C("2ee6d6"),

            TeleportBg       = C("0a0c0e"),
            LivePosFg        = C("4ade80"),
            SaveBtnBg        = C("071a0f"),
            SaveBtnFg        = C("4ade80"),
            SaveBtnBorder    = C("2a6a3a"),
            TpBtnBg          = C("1a0714"),
            TpBtnFg          = C("ff3da6"),
            TpBtnBorder      = C("6a2a52"),
            SavedCoordsFg    = C("4ade80"),
            CoordLabelFg     = C("2ee6d6"),
            CoordInputBg     = C("0c0f11"),
            CoordInputFg     = C("8ad6cf"),
            CoordInputBorder = C("173433"),
            GoBtnBg          = C("070809"),
            GoBtnFg          = C("2ee6d6"),
            GoBtnBorder      = C("2ee6d6"),

            LogPanelBg       = C("060708"),
            LogBg            = C("050607"),
            LogFg            = C("2ee6d6"),
            LogTitleFg       = C("2a6a66"),

            UIFontFamily     = "JetBrains Mono",
            MonoFontFamily   = "JetBrains Mono",
            TitleFontFamily  = "JetBrains Mono",
        };

        public static readonly TrainerTheme[] All =
            [MidnightForge, Atelier, Sandsworn, Override];
    }
}
