using System;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;


namespace MControl.Drawing
{

	#region Enums
	public enum FrameStyle
	{
		Dashed,
		Thick
	}

	public enum McDrawMode
	{
		// Fields
		R2_BLACK = 1,
		R2_COPYPEN = 13,
		R2_MASKNOTPEN = 3,
		R2_MASKPEN = 9,
		R2_MASKPENNOT = 5,
		R2_MERGENOTPEN = 12,
		R2_MERGEPEN = 15,
		R2_MERGEPENNOT = 14,
		R2_NOP = 11,
		R2_NOT = 6,
		R2_NOTCOPYPEN = 4,
		R2_NOTMASKPEN = 8,
		R2_NOTMERGEPEN = 2,
		R2_NOTXORPEN = 10,
		R2_WHITE = 0x10,
		R2_XORPEN = 7
	}

	public enum ColorType
	{
		// Fields
		COLOR_3DDKSHADOW = 0x15,
		COLOR_3DLIGHT = 0x16,
		COLOR_ACTIVEBORDER = 10,
		COLOR_ACTIVECAPTION = 2,
		COLOR_APPWORKSPACE = 12,
		COLOR_BACKGROUND = 1,
		COLOR_BTNFACE = 15,
		COLOR_BTNHIGHLIGHT = 20,
		COLOR_BTNSHADOW = 0x10,
		COLOR_BTNTEXT = 0x12,
		COLOR_CAPTIONTEXT = 9,
		COLOR_GRADIENTACTIVECAPTION = 0x1b,
		COLOR_GRADIENTINACTIVECAPTION = 0x1c,
		COLOR_GRAYTEXT = 0x11,
		COLOR_HIGHLIGHT = 13,
		COLOR_HIGHLIGHTTEXT = 14,
		COLOR_HOTLIGHT = 0x1a,
		COLOR_INACTIVEBORDER = 11,
		COLOR_INACTIVECAPTION = 3,
		COLOR_INACTIVECAPTIONTEXT = 0x13,
		COLOR_INFOBK = 0x18,
		COLOR_INFOTEXT = 0x17,
		COLOR_MENU = 4,
		COLOR_MENUBAR = 30,
		COLOR_MENUHILIGHT = 0x1d,
		COLOR_MENUTEXT = 7,
		COLOR_SCROLLBAR = 0,
		COLOR_WINDOW = 5,
		COLOR_WINDOWFRAME = 6,
		COLOR_WINDOWTEXT = 8
	}

	#endregion

	#region Pens
	public sealed class McPens
	{
		// Fields
		private static Pen controlText;
		private static Pen selected;
		private static Pen selectedText;

		static McPens()
		{
			McPens.controlText = new Pen(McColors.ControlText);
			McPens.selected = new Pen(McColors.Selected);
			McPens.selectedText = new Pen(McColors.SelectedText);
		}

		private McPens()
		{
		}

		public static Pen ControlText
		{
			get
			{
				return McPens.controlText;
			}
		}
 
		public static Pen Selected
		{
			get
			{
				return McPens.selected;
			}
		}
		public static Pen SelectedText
		{
			get
			{
				return McPens.selectedText;
			}
		}

	}
	#endregion

	#region Colors

	public sealed class McColors
	{

		// Fields
		private static Color activeCaptionEnd;
		private static Color activeCaptionStart;
		private static string colorName;
		private static Color[] colors;
		private static Color content;
		private static Color contentDark;
		private static Color controlEnd;
		private static Color controlEndDark;
		private static Color controlEndLight;
		private static Color controlStart;
		private static Color controlStartDark;
		private static Color controlStartLight;
		private static Color controlText;
		private static Color[] customColors;
		private static string customStr;
		private static Color focus;
		private static Color inactiveCaptionEnd;
		private static Color inactiveCaptionStart;
		private static Hashtable localizableColors;
		private static Hashtable localizableSystemColors;
		private static string otherStr;
		private static Color selected;
		private static Color selectedText;
		private static Color[] systemColors;
		private static string systemStr;
		private static string webStr;

		static McColors()
		{
			McColors.webStr = "Web";
			McColors.systemStr = "System";
			McColors.customStr = "Custom";
			McColors.otherStr = "Other...";
			McColors.colorName = "Color";
			Color[] colorArray1 = new Color[] { 
												  Color.FromArgb(0xff, 0xff, 0xff), Color.FromArgb(0xff, 0xc0, 0xc0), Color.FromArgb(0xff, 0xe0, 0xc0), Color.FromArgb(0xff, 0xff, 0xc0), Color.FromArgb(0xc0, 0xff, 0xc0), Color.FromArgb(0xc0, 0xff, 0xff), Color.FromArgb(0xc0, 0xc0, 0xff), Color.FromArgb(0xff, 0xc0, 0xff), Color.FromArgb(0xe0, 0xe0, 0xe0), Color.FromArgb(0xff, 0x80, 0x80), Color.FromArgb(0xff, 0xc0, 0x80), Color.FromArgb(0xff, 0xff, 0x80), Color.FromArgb(0x80, 0xff, 0x80), Color.FromArgb(0x80, 0xff, 0xff), Color.FromArgb(0x80, 0x80, 0xff), Color.FromArgb(0xff, 0x80, 0xff), 
												  Color.FromArgb(0xc0, 0xc0, 0xc0), Color.FromArgb(0xff, 0, 0), Color.FromArgb(0xff, 0x80, 0), Color.FromArgb(0xff, 0xff, 0), Color.FromArgb(0, 0xff, 0), Color.FromArgb(0, 0xff, 0xff), Color.FromArgb(0, 0, 0xff), Color.FromArgb(0xff, 0, 0xff), Color.FromArgb(0x80, 0x80, 0x80), Color.FromArgb(0xc0, 0, 0), Color.FromArgb(0xc0, 0x40, 0), Color.FromArgb(0xc0, 0xc0, 0), Color.FromArgb(0, 0xc0, 0), Color.FromArgb(0, 0xc0, 0xc0), Color.FromArgb(0, 0, 0xc0), Color.FromArgb(0xc0, 0, 0xc0), 
												  Color.FromArgb(0x40, 0x40, 0x40), Color.FromArgb(0x80, 0, 0), Color.FromArgb(0x80, 0x40, 0), Color.FromArgb(0x80, 0x80, 0), Color.FromArgb(0, 0x80, 0), Color.FromArgb(0, 0x80, 0x80), Color.FromArgb(0, 0, 0x80), Color.FromArgb(0x80, 0, 0x80), Color.FromArgb(0, 0, 0), Color.FromArgb(0x40, 0, 0), Color.FromArgb(0x80, 0x40, 0x40), Color.FromArgb(0x40, 0x40, 0), Color.FromArgb(0, 0x40, 0), Color.FromArgb(0, 0x40, 0x40), Color.FromArgb(0, 0, 0x40), Color.FromArgb(0x40, 0, 0x40) 
												  //Color.FromArgb(0xff, 0xff, 0xff), Color.FromArgb(0xff, 0xff, 0xff), Color.FromArgb(0xff, 0xff, 0xff), Color.FromArgb(0xff, 0xff, 0xff), Color.FromArgb(0xff, 0xff, 0xff), Color.FromArgb(0xff, 0xff, 0xff), Color.FromArgb(0xff, 0xff, 0xff), Color.FromArgb(0xff, 0xff, 0xff), Color.FromArgb(0xff, 0xff, 0xff), Color.FromArgb(0xff, 0xff, 0xff), Color.FromArgb(0xff, 0xff, 0xff), Color.FromArgb(0xff, 0xff, 0xff), Color.FromArgb(0xff, 0xff, 0xff), Color.FromArgb(0xff, 0xff, 0xff), Color.FromArgb(0xff, 0xff, 0xff), Color.FromArgb(0xff, 0xff, 0xff)
											  };
			McColors.customColors = colorArray1;
			colorArray1 = new Color[] { 
										  SystemColors.ActiveBorder, SystemColors.ActiveCaption, SystemColors.ActiveCaptionText, SystemColors.AppWorkspace, SystemColors.Control, SystemColors.ControlDark, SystemColors.ControlDarkDark, SystemColors.ControlLight, SystemColors.ControlLightLight, SystemColors.ControlText, SystemColors.Desktop, SystemColors.GrayText, SystemColors.Highlight, SystemColors.HighlightText, SystemColors.HotTrack, SystemColors.InactiveBorder, 
										  SystemColors.InactiveCaption, SystemColors.InactiveCaptionText, SystemColors.Info, SystemColors.InfoText, SystemColors.Menu, SystemColors.MenuText, SystemColors.ScrollBar, SystemColors.Window, SystemColors.WindowFrame, SystemColors.WindowText
									  };
			McColors.systemColors = colorArray1;
			colorArray1 = new Color[] { 
										  Color.Transparent, Color.Black, Color.DimGray, Color.Gray, Color.DarkGray, Color.Silver, Color.LightGray, Color.Gainsboro, Color.WhiteSmoke, Color.White, Color.RosyBrown, Color.IndianRed, Color.Brown, Color.Firebrick, Color.LightCoral, Color.Maroon, 
										  Color.DarkRed, Color.Red, Color.Snow, Color.MistyRose, Color.Salmon, Color.Tomato, Color.DarkSalmon, Color.Coral, Color.OrangeRed, Color.LightSalmon, Color.Sienna, Color.SeaShell, Color.Chocolate, Color.SaddleBrown, Color.SandyBrown, Color.PeachPuff, 
										  Color.Peru, Color.Linen, Color.Bisque, Color.DarkOrange, Color.BurlyWood, Color.Tan, Color.AntiqueWhite, Color.NavajoWhite, Color.BlanchedAlmond, Color.PapayaWhip, Color.Moccasin, Color.Orange, Color.Wheat, Color.OldLace, Color.FloralWhite, Color.DarkGoldenrod, 
										  Color.Goldenrod, Color.Cornsilk, Color.Gold, Color.Khaki, Color.LemonChiffon, Color.PaleGoldenrod, Color.DarkKhaki, Color.Beige, Color.LightGoldenrodYellow, Color.Olive, Color.Yellow, Color.LightYellow, Color.Ivory, Color.OliveDrab, Color.YellowGreen, Color.DarkOliveGreen, 
										  Color.GreenYellow, Color.Chartreuse, Color.LawnGreen, Color.DarkSeaGreen, Color.ForestGreen, Color.LimeGreen, Color.LightGreen, Color.PaleGreen, Color.DarkGreen, Color.Green, Color.Lime, Color.Honeydew, Color.SeaGreen, Color.MediumSeaGreen, Color.SpringGreen, Color.MintCream, 
										  Color.MediumSpringGreen, Color.MediumAquamarine, Color.Aquamarine, Color.Turquoise, Color.LightSeaGreen, Color.MediumTurquoise, Color.DarkSlateGray, Color.PaleTurquoise, Color.Teal, Color.DarkCyan, Color.Aqua, Color.Cyan, Color.LightCyan, Color.Azure, Color.DarkTurquoise, Color.CadetBlue, 
										  Color.PowderBlue, Color.LightBlue, Color.DeepSkyBlue, Color.SkyBlue, Color.LightSkyBlue, Color.SteelBlue, Color.AliceBlue, Color.DodgerBlue, Color.SlateGray, Color.LightSlateGray, Color.LightSteelBlue, Color.CornflowerBlue, Color.RoyalBlue, Color.MidnightBlue, Color.Lavender, Color.Navy, 
										  Color.DarkBlue, Color.MediumBlue, Color.Blue, Color.GhostWhite, Color.SlateBlue, Color.DarkSlateBlue, Color.MediumSlateBlue, Color.MediumPurple, Color.BlueViolet, Color.Indigo, Color.DarkOrchid, Color.DarkViolet, Color.MediumOrchid, Color.Thistle, Color.Plum, Color.Violet, 
										  Color.Purple, Color.DarkMagenta, Color.Magenta, Color.Fuchsia, Color.Orchid, Color.MediumVioletRed, Color.DeepPink, Color.HotPink, Color.LavenderBlush, Color.PaleVioletRed, Color.Crimson, Color.Pink, Color.LightPink
									  };
			McColors.colors = colorArray1;
			McColors.localizableColors = new Hashtable();
			McColors.localizableSystemColors = new Hashtable();
			McColors.InitColors();
			colorArray1 = McColors.McSystemColors;
			int num1 = 0;
			while (num1 < colorArray1.Length)
			{
				Color color1 = colorArray1[num1];
				McColors.LocalizableSystemColors.Add(color1, color1.Name);
				num1++;
			}
			colorArray1 = McColors.Colors;
			for (num1 = 0; num1 < colorArray1.Length; num1++)
			{
				Color color2 = colorArray1[num1];
				McColors.LocalizableColors.Add(color2, color2.Name);
			}
		}

		private McColors()
		{
		}

		private static Color CalcColor(Color front, Color back, int alpha)
		{
			Color color1 = Color.FromArgb(0xff, front);
			Color color2 = Color.FromArgb(0xff, back);
			float single1 = color1.R;
			float single2 = color1.G;
			float single3 = color1.B;
			float single4 = color2.R;
			float single5 = color2.G;
			float single6 = color2.B;
			float single7 = ((single1 * alpha) / 255f) + (single4 * (((float) (0xff - alpha)) / 255f));
			byte num1 = (byte) single7;
			float single8 = ((single2 * alpha) / 255f) + (single5 * (((float) (0xff - alpha)) / 255f));
			byte num2 = (byte) single8;
			float single9 = ((single3 * alpha) / 255f) + (single6 * (((float) (0xff - alpha)) / 255f));
			byte num3 = (byte) single9;
			return Color.FromArgb(0xff, num1, num2, num3);
		}

		public static void InitColors()
		{
			McColors.activeCaptionStart = McPaint.GetSysColor(ColorType.COLOR_GRADIENTACTIVECAPTION);
			McColors.activeCaptionEnd = SystemColors.ActiveCaption;
			McColors.inactiveCaptionStart = McPaint.GetSysColor(ColorType.COLOR_GRADIENTINACTIVECAPTION);
			McColors.inactiveCaptionEnd = SystemColors.InactiveCaption;
			McColors.focus = McColors.CalcColor(SystemColors.Highlight, SystemColors.Window, 70);
			McColors.selected = McColors.CalcColor(SystemColors.Highlight, SystemColors.Window, 30);
			McColors.selectedText = McColors.CalcColor(SystemColors.Highlight, SystemColors.Window, 220);
			McColors.content = McColors.CalcColor(SystemColors.Window, SystemColors.Control, 200);
			McColors.contentDark = McPaint.Dark(McColors.Content, 10);
			McColors.controlStart = McPaint.Light(SystemColors.Control, 30);
			McColors.controlEnd = McPaint.Dark(SystemColors.Control, 10);
			McColors.controlText = SystemColors.ControlText;
			McColors.controlStartLight = McPaint.Light(McColors.ControlStart, 20);
			McColors.controlEndLight = McPaint.Light(McColors.ControlEnd, 20);
			McColors.controlStartDark = McPaint.Dark(McColors.ControlStart, 20);
			McColors.controlEndDark = McPaint.Dark(McColors.ControlEnd, 20);
		}

		public static Color ActiveCaptionEnd
		{
			get
			{
				return McColors.activeCaptionEnd;
			}
			set
			{
				McColors.activeCaptionEnd = value;
			}
		}
		public static Color ActiveCaptionStart
		{
			get
			{
				return McColors.activeCaptionStart;
			}
			set
			{
				McColors.activeCaptionStart = value;
			}
		}
		public static string ColorName
		{
			get
			{
				return McColors.colorName;
			}
			set
			{
				McColors.colorName = value;
			}
		}
 
		public static Color[] Colors
		{
			get
			{
				return McColors.colors;
			}
		}
		public static Color Content
		{
			get
			{
				return McColors.content;
			}
			set
			{
				McColors.content = value;
			}
		}
		public static Color ContentDark
		{
			get
			{
				return McColors.contentDark;
			}
			set
			{
				McColors.contentDark = value;
			}
		}
		public static Color ControlEnd
		{
			get
			{
				return McColors.controlEnd;
			}
			set
			{
				McColors.controlEnd = value;
			}
		}
 
		public static Color ControlEndDark
		{
			get
			{
				return McColors.controlEndDark;
			}
			set
			{
				McColors.controlEndDark = value;
			}
		}
 
		public static Color ControlEndLight
		{
			get
			{
				return McColors.controlEndLight;
			}
			set
			{
				McColors.controlEndLight = value;
			}
		}
		public static Color ControlStart
		{
			get
			{
				return McColors.controlStart;
			}
			set
			{
				McColors.controlStart = value;
			}
		}
		public static Color ControlStartDark
		{
			get
			{
				return McColors.controlStartDark;
			}
			set
			{
				McColors.controlStartDark = value;
			}
		}
 
		public static Color ControlStartLight
		{
			get
			{
				return McColors.controlStartLight;
			}
			set
			{
				McColors.controlStartLight = value;
			}
		}
		public static Color ControlText
		{
			get
			{
				return McColors.controlText;
			}
			set
			{
				McColors.controlText = value;
			}
		}
 
		public static Color[] CustomColors
		{
			get
			{
				return McColors.customColors;
			}
		}
		public static string CustomStr
		{
			get
			{
				return McColors.customStr;
			}
			set
			{
				McColors.customStr = value;
			}
		}
		public static Color Focus
		{
			get
			{
				return McColors.focus;
			}
			set
			{
				McColors.focus = value;
			}
		}
		public static Color InactiveCaptionEnd
		{
			get
			{
				return McColors.inactiveCaptionEnd;
			}
			set
			{
				McColors.inactiveCaptionEnd = value;
			}
		}
		public static Color InactiveCaptionStart
		{
			get
			{
				return McColors.inactiveCaptionStart;
			}
			set
			{
				McColors.inactiveCaptionStart = value;
			}
		}
		public static Hashtable LocalizableColors
		{
			get
			{
				return McColors.localizableColors;
			}
		}
 
		public static Hashtable LocalizableSystemColors
		{
			get
			{
				return McColors.localizableSystemColors;
			}
		}
		public static string OtherStr
		{
			get
			{
				return McColors.otherStr;
			}
			set
			{
				McColors.otherStr = value;
			}
		}
		public static Color Selected
		{
			get
			{
				return McColors.selected;
			}
			set
			{
				McColors.selected = value;
			}
		}
		public static Color SelectedText
		{
			get
			{
				return McColors.selectedText;
			}
			set
			{
				McColors.selectedText = value;
			}
		}
		public static Color[] McSystemColors
		{
			get
			{
				return McColors.systemColors;
			}
		}
		public static string SystemStr
		{
			get
			{
				return McColors.systemStr;
			}
			set
			{
				McColors.systemStr = value;
			}
		}
		public static string WebStr
		{
			get
			{
				return McColors.webStr;
			}
			set
			{
				McColors.webStr = value;
			}
		}

	}

	#endregion

	#region Brushes

	public sealed class McBrushes
	{

		// Fields
		private static Brush content;
		private static Brush contentDark;
		private static Brush focus;
		private static HatchStyle[] hatchStyles;
		private static Hashtable localizedHatchStyles;
		private static Brush selected;
		private static Brush selectedText;

		static McBrushes()
		{
			McBrushes.content = new SolidBrush(McColors.Content);
			McBrushes.contentDark = new SolidBrush(McColors.ContentDark);
			McBrushes.focus = new SolidBrush(McColors.Focus);
			McBrushes.selected = new SolidBrush(McColors.Selected);
			McBrushes.selectedText = new SolidBrush(McColors.SelectedText);
			McBrushes.localizedHatchStyles = new Hashtable();
			HatchStyle[] styleArray1 = new HatchStyle[0x34];
			styleArray1[0] = HatchStyle.BackwardDiagonal;
			styleArray1[1] = HatchStyle.Cross;
			styleArray1[2] = HatchStyle.DarkDownwardDiagonal;
			styleArray1[3] = HatchStyle.DarkHorizontal;
			styleArray1[4] = HatchStyle.DarkUpwardDiagonal;
			styleArray1[5] = HatchStyle.DarkVertical;
			styleArray1[6] = HatchStyle.DashedDownwardDiagonal;
			styleArray1[7] = HatchStyle.DashedHorizontal;
			styleArray1[8] = HatchStyle.DashedUpwardDiagonal;
			styleArray1[9] = HatchStyle.DashedVertical;
			styleArray1[10] = HatchStyle.DiagonalBrick;
			styleArray1[11] = HatchStyle.DiagonalCross;
			styleArray1[12] = HatchStyle.Divot;
			styleArray1[13] = HatchStyle.DottedDiamond;
			styleArray1[14] = HatchStyle.DottedGrid;
			styleArray1[15] = HatchStyle.ForwardDiagonal;
			styleArray1[0x11] = HatchStyle.HorizontalBrick;
			styleArray1[0x12] = HatchStyle.LargeCheckerBoard;
			styleArray1[0x13] = HatchStyle.LargeConfetti;
			styleArray1[20] = HatchStyle.LightDownwardDiagonal;
			styleArray1[0x15] = HatchStyle.LightHorizontal;
			styleArray1[0x16] = HatchStyle.LightUpwardDiagonal;
			styleArray1[0x17] = HatchStyle.LightVertical;
			styleArray1[0x18] = HatchStyle.NarrowHorizontal;
			styleArray1[0x19] = HatchStyle.NarrowVertical;
			styleArray1[0x1a] = HatchStyle.OutlinedDiamond;
			styleArray1[0x1b] = HatchStyle.Percent05;
			styleArray1[0x1c] = HatchStyle.Percent10;
			styleArray1[0x1d] = HatchStyle.Percent20;
			styleArray1[30] = HatchStyle.Percent25;
			styleArray1[0x1f] = HatchStyle.Percent30;
			styleArray1[0x20] = HatchStyle.Percent40;
			styleArray1[0x21] = HatchStyle.Percent50;
			styleArray1[0x22] = HatchStyle.Percent60;
			styleArray1[0x23] = HatchStyle.Percent70;
			styleArray1[0x24] = HatchStyle.Percent75;
			styleArray1[0x25] = HatchStyle.Percent80;
			styleArray1[0x26] = HatchStyle.Percent90;
			styleArray1[0x27] = HatchStyle.Plaid;
			styleArray1[40] = HatchStyle.Shingle;
			styleArray1[0x29] = HatchStyle.SmallCheckerBoard;
			styleArray1[0x2a] = HatchStyle.SmallConfetti;
			styleArray1[0x2b] = HatchStyle.SmallGrid;
			styleArray1[0x2c] = HatchStyle.SolidDiamond;
			styleArray1[0x2d] = HatchStyle.Sphere;
			styleArray1[0x2e] = HatchStyle.Trellis;
			styleArray1[0x2f] = HatchStyle.Vertical;
			styleArray1[0x30] = HatchStyle.Weave;
			styleArray1[0x31] = HatchStyle.WideDownwardDiagonal;
			styleArray1[50] = HatchStyle.WideUpwardDiagonal;
			styleArray1[0x33] = HatchStyle.ZigZag;
			McBrushes.hatchStyles = styleArray1;
			styleArray1 = McBrushes.HatchStyles;
			for (int num1 = 0; num1 < styleArray1.Length; num1++)
			{
				HatchStyle style1 = styleArray1[num1];
				McBrushes.LocalizedHatchStyles.Add(style1, style1.ToString());
			}
		}

		private McBrushes()
		{
		}

		public static Brush GetActiveCaptionBrush(Rectangle rectangle, float angle)
		{
			return new LinearGradientBrush(rectangle, McColors.ActiveCaptionStart, McColors.ActiveCaptionEnd, angle);
		}

		public static Brush GetActiveCaptionLightBrush(Rectangle rectangle, float angle)
		{
			return new LinearGradientBrush(rectangle, McPaint.Light(McColors.ActiveCaptionStart, 20), McPaint.Light(McColors.ActiveCaptionEnd, 20), angle);
		}
 
		public static Brush GetControlBrush(Rectangle rectangle, float angle)
		{
			return new LinearGradientBrush(rectangle, McColors.ControlStart, McColors.ControlEnd, angle);
		}

		public static Brush GetControlDarkBrush(Rectangle rectangle, float angle)
		{
			return new LinearGradientBrush(rectangle, McColors.ControlStartDark, McColors.ControlEndDark, angle);
		}

		public static Brush GetControlLightBrush(Rectangle rectangle, float angle)
		{
			return new LinearGradientBrush(rectangle, McColors.ControlStartLight, McColors.ControlEndLight, angle);
		}

		public static HatchStyle HatchStyleFromName(string name)
		{
			HatchStyle[] styleArray1 = McBrushes.HatchStyles;
			for (int num1 = 0; num1 < styleArray1.Length; num1++)
			{
				HatchStyle style1 = styleArray1[num1];
				style1.ToString();
				if (style1.ToString() == name)
				{
					return style1;
				}
			}
			return HatchStyle.BackwardDiagonal;
		}

 
		public static Brush Content
		{
			get
			{
				return McBrushes.content;
			}
		}
 
		public static Brush ContentDark
		{
			get
			{
				return McBrushes.contentDark;
			}
		}
 
		public static Brush Focus
		{
			get
			{
				return McBrushes.focus;
			}
		}
		public static HatchStyle[] HatchStyles
		{
			get
			{
				return McBrushes.hatchStyles;
			}
		}
		public static Hashtable LocalizedHatchStyles
		{
			get
			{
				return McBrushes.localizedHatchStyles;
			}
		}
 
		public static Brush Selected
		{
			get
			{
				return McBrushes.selected;
			}
		}
 
		public static Brush SelectedText
		{
			get
			{
				return McBrushes.selectedText;
			}
		}
 

	}


	#endregion
}
