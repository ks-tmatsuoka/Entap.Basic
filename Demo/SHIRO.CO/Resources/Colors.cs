using System;
using Xamarin.Forms;

namespace SHIRO.CO
{
    /// <summary>
    /// 色の定義
    /// </summary>
    public class Colors
    {
        // BG
        public static readonly Color BackgroundMain = Color.FromHex(Theme);
        public static readonly Color BackgroundSub = Color.FromHex(GrayLight);
        public static readonly Color BackgroundModal = Color.FromHex("#000000").MultiplyAlpha(20);

        // Bar
        public static readonly Color BarHeader = Color.FromHex(White);
        public static readonly Color BarStatusBar = Color.FromHex(Theme);
        public static readonly Color BarFooter = Color.FromHex(White);

        // Button Background
        public static readonly Color ButtonPositive = Color.FromHex(Theme);
        public static readonly Color ButtonNeutral = Color.FromHex(White);
        public static readonly Color ButtonNegative = Color.FromHex(White);
        public static readonly Color ButtonDisabled = Color.FromHex(Gray);
        // Button Border
        public static readonly Color ButtonBorderTheme = Color.FromHex(Theme);
        public static readonly Color ButtonBorderSub = Color.FromHex(Gray);

        // Text
        public static readonly Color TextDefault = Color.FromHex(Black);
        public static readonly Color TextWhite = Color.FromHex(White);
        public static readonly Color TextContrast = Color.FromHex(BlackLight);
        public static readonly Color TextLight = Color.FromHex(GrayDark);
        public static readonly Color TextLink = Color.FromHex(Link);
        public static readonly Color TextCaution = Color.FromHex(Caution);
        public static readonly Color TextMain = Color.FromHex(Theme);
        public static readonly Color TextAccent = Color.FromHex(Accent);

        // Input
        public static readonly Color InputBorder = Color.FromHex(Gray);
        public static readonly Color InputBorderCaution = Color.FromHex(Caution);
        public static readonly Color InputBorderFocus = Color.FromHex(Accent);

        public static readonly Color Checked = Color.FromHex(Accent);

        public static readonly Color Active = Color.FromHex(Accent);
        public static readonly Color Inactive = Color.FromHex(Gray);

        const string Black = "#4A4A4A";
        const string BlackLight = "#848384";
        const string GrayDark = "#BABABA";
        const string Gray = "#D8D8D8";
        const string GrayLight = "#F7F7F7";
        const string White = "#FFFFFF";
        const string Link = "#007AFF";
        const string Caution = "#FF4C4C";
        const string Theme = "#161616";
        const string Accent = "#40B5FF";
    }
}
