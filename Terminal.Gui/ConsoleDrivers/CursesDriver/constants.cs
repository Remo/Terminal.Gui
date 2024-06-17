/*
 * This file is autogenerated by the attrib.c program, do not edit
 */

//#define XTERM1006

using System.Runtime.InteropServices;

namespace Unix.Terminal;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public partial class Curses
{
    public const int A_NORMAL = 0x0;
    public const int A_STANDOUT = 0x10000;
    public const int A_UNDERLINE = 0x20000;
    public const int A_REVERSE = 0x40000;
    public const int A_BLINK = 0x80000;
    public const int A_DIM = 0x100000;
    public const int A_BOLD = 0x200000;
    public const int A_PROTECT = 0x1000000;
    public const int A_INVIS = 0x800000;
    public const int ACS_LLCORNER = 0x40006d;
    public const int ACS_LRCORNER = 0x40006a;
    public const int ACS_HLINE = 0x400071;
    public const int ACS_ULCORNER = 0x40006c;
    public const int ACS_URCORNER = 0x40006b;
    public const int ACS_VLINE = 0x400078;
    public const int ACS_LTEE = 0x400074;
    public const int ACS_RTEE = 0x400075;
    public const int ACS_BTEE = 0x400076;
    public const int ACS_TTEE = 0x400077;
    public const int ACS_PLUS = 0x40006e;
    public const int ACS_S1 = 0x40006f;
    public const int ACS_S9 = 0x400073;
    public const int ACS_DIAMOND = 0x400060;
    public const int ACS_CKBOARD = 0x400061;
    public const int ACS_DEGREE = 0x400066;
    public const int ACS_PLMINUS = 0x400067;
    public const int ACS_BULLET = 0x40007e;
    public const int ACS_LARROW = 0x40002c;
    public const int ACS_RARROW = 0x40002b;
    public const int ACS_DARROW = 0x40002e;
    public const int ACS_UARROW = 0x40002d;
    public const int ACS_BOARD = 0x400068;
    public const int ACS_LANTERN = 0x400069;
    public const int ACS_BLOCK = 0x400030;
    public const int COLOR_BLACK = 0x0;
    public const int COLOR_RED = 0x1;
    public const int COLOR_GREEN = 0x2;
    public const int COLOR_YELLOW = 0x3;
    public const int COLOR_BLUE = 0x4;
    public const int COLOR_MAGENTA = 0x5;
    public const int COLOR_CYAN = 0x6;
    public const int COLOR_WHITE = 0x7;
    public const int COLOR_GRAY = 0x8;
    public const int KEY_CODE_YES = 0x100;
    public const int ERR = unchecked ((int)0xffffffff);
    public const int TIOCGWINSZ = 0x5413;
    public const int TIOCGWINSZ_MAC = 0x40087468;
    [Flags]
    public enum Event : long
    {
        Button1Pressed = 0x2,
        Button1Released = 0x1,
        Button1Clicked = 0x4,
        Button1DoubleClicked = 0x8,
        Button1TripleClicked = 0x10,
        Button2Pressed = 0x40,
        Button2Released = 0x20,
        Button2Clicked = 0x80,
        Button2DoubleClicked = 0x100,
        Button2TripleClicked = 0x200,
        Button3Pressed = 0x800,
        Button3Released = 0x400,
        Button3Clicked = 0x1000,
        Button3DoubleClicked = 0x2000,
        Button3TripleClicked = 0x4000,
        ButtonWheeledUp = 0x10000,
        ButtonWheeledDown = 0x200000,
        Button4Pressed = 0x80000,
        Button4Released = 0x40000,
        Button4Clicked = 0x100000,
        Button4DoubleClicked = 0x20000,
        Button4TripleClicked = 0x400000,
        ButtonShift = 0x4000000,
        ButtonCtrl = 0x2000000,
        ButtonAlt = 0x8000000,
        ReportMousePosition = 0x10000000,
        AllEvents = 0x7ffffff
    }
#if XTERM1006
    public const int LeftRightUpNPagePPage = unchecked ((int)0x8);
    public const int DownEnd = unchecked ((int)0x6);
    public const int Home = unchecked ((int)0x7);
#else
    public const int LeftRightUpNPagePPage = 0x0;
    public const int DownEnd = 0x0;
    public const int Home = 0x0;
#endif
    public const int KeyBackspace = 0x107;
    public const int KeyUp = 0x103;
    public const int KeyDown = 0x102;
    public const int KeyLeft = 0x104;
    public const int KeyRight = 0x105;
    public const int KeyNPage = 0x152;
    public const int KeyPPage = 0x153;
    public const int KeyHome = 0x106;
    public const int KeyMouse = 0x199;
    public const int KeyEnd = 0x168;
    public const int KeyDeleteChar = 0x14a;
    public const int KeyInsertChar = 0x14b;
    public const int KeyTab = 0x009;
    public const int KeyBackTab = 0x161;
    public const int KeyF1 = 0x109;
    public const int KeyF2 = 0x10a;
    public const int KeyF3 = 0x10b;
    public const int KeyF4 = 0x10c;
    public const int KeyF5 = 0x10d;
    public const int KeyF6 = 0x10e;
    public const int KeyF7 = 0x10f;
    public const int KeyF8 = 0x110;
    public const int KeyF9 = 0x111;
    public const int KeyF10 = 0x112;
    public const int KeyF11 = 0x113;
    public const int KeyF12 = 0x114;
    public const int KeyResize = 0x19a;
    public const int ShiftKeyUp = 0x151;
    public const int ShiftKeyDown = 0x150;
    public const int ShiftKeyLeft = 0x189;
    public const int ShiftKeyRight = 0x192;
    public const int ShiftKeyNPage = 0x18c;
    public const int ShiftKeyPPage = 0x18e;
    public const int ShiftKeyHome = 0x187;
    public const int ShiftKeyEnd = 0x182;
    public const int AltKeyUp = unchecked (0x234 + LeftRightUpNPagePPage);
    public const int AltKeyDown = unchecked (0x20b + DownEnd);
    public const int AltKeyLeft = unchecked (0x21f + LeftRightUpNPagePPage);
    public const int AltKeyRight = unchecked (0x22e + LeftRightUpNPagePPage);
    public const int AltKeyNPage = unchecked (0x224 + LeftRightUpNPagePPage);
    public const int AltKeyPPage = unchecked (0x229 + LeftRightUpNPagePPage);
    public const int AltKeyHome = unchecked (0x215 + Home);
    public const int AltKeyEnd = unchecked (0x210 + DownEnd);
    public const int CtrlKeyUp = unchecked (0x236 + LeftRightUpNPagePPage);
    public const int CtrlKeyDown = unchecked (0x20d + DownEnd);
    public const int CtrlKeyLeft = unchecked (0x221 + LeftRightUpNPagePPage);
    public const int CtrlKeyRight = unchecked (0x230 + LeftRightUpNPagePPage);
    public const int CtrlKeyNPage = unchecked (0x226 + LeftRightUpNPagePPage);
    public const int CtrlKeyPPage = unchecked (0x22b + LeftRightUpNPagePPage);
    public const int CtrlKeyHome = unchecked (0x217 + Home);
    public const int CtrlKeyEnd = unchecked (0x212 + DownEnd);
    public const int ShiftCtrlKeyUp = unchecked (0x237 + LeftRightUpNPagePPage);
    public const int ShiftCtrlKeyDown = unchecked (0x20e + DownEnd);
    public const int ShiftCtrlKeyLeft = unchecked (0x222 + LeftRightUpNPagePPage);
    public const int ShiftCtrlKeyRight = unchecked (0x231 + LeftRightUpNPagePPage);
    public const int ShiftCtrlKeyNPage = unchecked (0x227 + LeftRightUpNPagePPage);
    public const int ShiftCtrlKeyPPage = unchecked (0x22c + LeftRightUpNPagePPage);
    public const int ShiftCtrlKeyHome = unchecked (0x218 + Home);
    public const int ShiftCtrlKeyEnd = unchecked (0x213 + DownEnd);
    public const int ShiftAltKeyUp = unchecked (0x235 + LeftRightUpNPagePPage);
    public const int ShiftAltKeyDown = unchecked (0x20c + DownEnd);
    public const int ShiftAltKeyLeft = unchecked (0x220 + LeftRightUpNPagePPage);
    public const int ShiftAltKeyRight = unchecked (0x22f + LeftRightUpNPagePPage);
    public const int ShiftAltKeyNPage = unchecked (0x225 + LeftRightUpNPagePPage);
    public const int ShiftAltKeyPPage = unchecked (0x22a + LeftRightUpNPagePPage);
    public const int ShiftAltKeyHome = unchecked (0x216 + Home);
    public const int ShiftAltKeyEnd = unchecked (0x211 + DownEnd);
    public const int AltCtrlKeyNPage = unchecked (0x228 + LeftRightUpNPagePPage);
    public const int AltCtrlKeyPPage = unchecked (0x22d + LeftRightUpNPagePPage);
    public const int AltCtrlKeyHome = unchecked (0x219 + Home);
    public const int AltCtrlKeyEnd = unchecked (0x214 + DownEnd);

    // see #949
    public static int LC_ALL { get; }
    static Curses () { LC_ALL = RuntimeInformation.IsOSPlatform (OSPlatform.OSX) ? 0 : 6; }
    public static int ColorPair (int n) { return 0 + n * 256; }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
