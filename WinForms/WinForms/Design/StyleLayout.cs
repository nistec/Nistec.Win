using System;
using System.ComponentModel;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Text;
using System.Runtime.InteropServices;

using Nistec.Win32;
using Nistec.Drawing;



namespace Nistec.WinForms
{

    #region struct

    //	[StructLayout( LayoutKind.Sequential)]
    //	public class LayoutColors
    //	{
    //		#region  Members
    ////		protected Styles m_StylePlan;  
    ////		public bool IsFlatInternal;
    ////		public bool XpDisableInternal;
    ////		public FlatLayout FlatLayoutInternal;
    //		#endregion
    //
    //		#region Colors
    //		public Color ForeColor;
    //		public Color BackColor;
    //		public Color DisableColor;
    //		public Color BorderColor;
    //		public Color BorderHotColor;
    //		public Color FocusedColor;
    //		public Color BackgroundColor;
    //		public Color ColorBrush1;
    //		public Color ColorBrush2;
    //		public Color CaptionColor;
    //		public Color AlternatingColor;
    //
    //		public Color m_HighlightColor;
    //		public Color m_SelectedColor;
    //		public Color m_ContentColor;
    //
    //		#endregion
    //
    //		public LayoutColors(bool isDefult)
    //		{
    //			if(isDefult)SetDefault();
    //		}
    //
    //		private void SetDefault()
    //		{
    //			ForeColor=Color.Black;
    //			BackColor=Color.White;
    //			DisableColor=SystemColors.Control;
    //			//m_HighlightColor=CalcColor(Color.Red,Color.Yellow,120);
    //			//m_SelectedColor=CalcColor(CaptionColorInternal, SystemColors.Window, 30);
    //			//m_ContentColor=CalcColor(SystemColors.Window, SystemColors.Control, 200);
    //		}
    //
    //	}

    #endregion

    #region Enums

    public enum Styles
    {
        Desktop = 0,
        SteelBlue = 1,
        Goldenrod = 2,
        SeaGreen = 3,
        Carmel = 4,
        Silver = 5,
        Media = 6,
        System = 99,
        Custom = 100,
        None = 999
    }

    public enum ControlLayout
    {
        Visual,
        Flat,
        XpLayout,
        VistaLayout,
        System
    }

    public enum FlatLayout
    {
        Light = 0,
        Dark = 1,
        Flat = 2
    }

    public enum ThemeSchemes
    {
        None,
        Blue,
        OliveGreen,
        Silver
    }

    public enum CheckBoxTypes
    {
        CheckBox,
        RadioButton
    }

    public enum McStates
    {
        Normal,
        MouseOver,
        Focused
    }

    #endregion

    //	#region struct

    //[StructLayout( LayoutKind.Sequential  )]
    public class StyleLayout : IStyleLayout
    {
   
        #region static

        public static ControlLayout DefaultControlLayout { get { return ControlLayout.Flat; } }
        public static Styles DefaultStylePlan { get { return Styles.Desktop; } }

        private static Styles _Style = Styles.None;
        private static readonly StyleLayout _Layout;

        static StyleLayout()
		{
            Styles style = StyleLayout.GetRegistryStyle();
			_Layout=new StyleLayout(style);
		}

		public static StyleLayout DefaultLayout
		{
			get{return _Layout;}
		}
        //public static Styles StylePlan
        //{
        //    get{return _Layout.StylePlan;} 
        //}

        public static Styles GetRegistryStyle()
        {
            try
            {
                if (_Style != Styles.None)
                    return _Style;

                Styles style = StyleLayout.DefaultStylePlan;
                Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\Nistec\\WinForms");
                if (rk != null)
                {
                    string s = rk.GetValue("Style", StyleLayout.DefaultStylePlan.ToString()).ToString();
                    try
                    {
                        style = (Styles)Enum.Parse(typeof(Styles), s, true);
                    }
                    catch { }
                    if (style == Styles.Custom || style == Styles.None)
                    {
                        //MsgBox.ShowError("Is not a valid Style , the default style is Silver");
                    }
                }
                _Style = style;
                return _Style;
            }
            catch
            {
                return StyleLayout.DefaultStylePlan;
            }
        }

        //public static Color ControlColor
        //{
        //    get { return Color.FromArgb(252, 252, 254); }
        //}

        //public static Font DefaultFont
        //{
        //    get { return Form.DefaultFont; }
        //}
        //public static Color InactiveColor
        //{
        //    get { return Color.Gray; }
        //}
        
        #endregion

        #region Defaults

        public const string ControlColorString = "252, 252, 254";

        public static Color DefaultControlColor
        {
            get { return Color.FromArgb(252, 252, 254); }
        }
        public static Color DefaultButtonHotColor
        {
            get { return Color.Orange; }
        }
        public static Color DefaultBackColor
        {
            get { return Color.White; }
        }
        public static Color DefaultForeColor
        {
            get { return Color.Black; }
        }
        public static Color DefaultBorderColor
        {
            get { return Color.FromArgb(49, 106, 197); }
        }
        public static Color DefaultBorderHotColor
        {
            get { return Color.FromArgb(26, 80, 184); }
        }
        public static Color DefaultFocusedColor
        {
            get { return Color.RoyalBlue; }
        }
        public static Color DefaultColorBrush1
        {
            get { return Color.FromArgb(205, 203, 183); }
        }
        public static Color DefaultColorBrush2
        {
            get { return StyleLayout.DefaultControlColor; }
        }
        public static Color DefaultCaptionColor
        {
            get { return Color.FromArgb(0, 78, 152); }
        }
        public static Color DefaultCaptionLightColor
        {
            get { return Color.FromArgb(204, 223, 252); }
        }

        public static Color DefaultInactiveColor
        {
            get { return Color.Gray; }
        }

        public static Font DefaultFont
        {
            get { return Form.DefaultFont; }
        }

        public static Color DefaultDarkColor(Color color)
        {
            return ControlPaint.Dark(color);
        }

        public static Color DefaultDarkDarkColor(Color color)
        {
            return ControlPaint.DarkDark(color);
        }
        public static Color DefaultLightLightColor(Color color)
        {
            return ControlPaint.LightLight(color);
        }
        public static Color DefaultDisabledTextColor(Color backColor)
        {
            Color controlDark = SystemColors.ControlDark;
            //if (ControlPaint.IsDarker(backColor, SystemColors.Control))
            //{
            controlDark = ControlPaint.Dark(backColor);
            //}
            return controlDark;
        }

        #endregion

        #region Constructor
        public StyleLayout()
        {
            SetDefault();
            SetStyleColors(StyleLayout.DefaultStylePlan);
        }

        public StyleLayout(Styles style)
        {
            SetDefault();
            this.SetStyleColors(style);
        }

        private void SetDefault()
        {
            ForeColorInternal = Color.Black;
            BackColorInternal = Color.White;
            DisableColorInternal = Color.DarkGray;//SystemColors.Control;
            TextFontInternal = Form.DefaultFont;
            CaptionFontInternal = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));

            //m_HighlightColor=CalcColor(Color.Red,Color.Yellow,120);
            //m_SelectedColor=CalcColor(CaptionColorInternal, SystemColors.Window, 30);
            //m_ContentColor=CalcColor(SystemColors.Window, SystemColors.Control, 200);
        }
        #endregion

        #region Events

        [Category("StyleChanges")]
        public event ColorStyleChangedEventHandler ColorStyleChanged = null;
        [Category("StyleChanges")]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Deelegate delc
        /// </summary>
        //public delegate void PropertyChangedEventHandler(PropertyChangedEventArgs e);
        protected void OnPropertyChanged(string propertyname)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
        }

        protected void OnColorStyleChanged(Styles style)
        {
            if (ColorStyleChanged != null)
                ColorStyleChanged(this, new ColorStyleChangedEventArgs(style));
        }

        #endregion

        #region Members
        //		private Color m_HighlightColor;
        //		private Color m_SelectedColor;
        //		private Color m_ContentColor;
        private Styles m_StylePlan;

        internal protected bool IsFlatInternal;
        internal protected bool XpDisableInternal;
        internal protected FlatLayout FlatLayoutInternal;

        internal protected Color ForeColorInternal;
        internal protected Color BackColorInternal;
        internal protected Color DisableColorInternal;

        internal protected Color BorderColorInternal;
        internal protected Color BorderHotColorInternal;
        internal protected Color FocusedColorInternal;
        internal protected Color BackgroundColorInternal;
        internal protected Color ButtonBorderColorInternal;

        internal protected Color ColorBrush1Internal;
        internal protected Color ColorBrush2Internal;
        internal protected Color ColorBrushLowerInternal;
        internal protected Color ColorBrushUpperInternal;

        internal protected Color CaptionColorInternal;
        internal protected Color AlternatingColorInternal;
        internal protected Color CaptionLightColorInternal;
        internal protected Color HeaderColorInternal;
        internal protected Color HeaderTextColorInternal;
        internal protected Color CaptionTextColorInternal;


        internal protected Color HighlightColorInternal;
        internal protected Color SelectedColorInternal;
        internal protected Color ContentColorInternal;
        internal protected Color SelectedTextColorInternal;

        internal protected Font TextFontInternal;
        internal protected Font CaptionFontInternal;

        //this.labelTitle.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));

        #endregion

        #region Colors Properties

        internal protected Color DarkColor { get { return CaptionColorInternal; } }
        //internal protected Color DarkDarkColor {get {return ControlPaint.DarkDark(ColorBrush1Internal);}}
        //internal protected Color DarkDarkColor {get {return ControlPaint.Dark(ColorBrush1Internal,50);}}

        internal protected Color LightColor { get { return ControlPaint.Light(ColorBrush1Internal); } }
        internal protected Color LightLightColor { get { return ControlPaint.LightLight(ColorBrush1Internal); } }

        internal protected Color ClickColor { get { return Color.LightGray; } }
        //internal protected Color FormColor {get {return ColorBrush2Internal;}}

        //		internal protected Color GetDarkColor (float prc){return ControlPaint.Dark(ColorBrush1Internal,prc);}
        //		internal protected Color GetLightColor(float prc){return ControlPaint.Light (ColorBrush1Internal,prc);}
        //		internal protected Color GetLightColor(Color color,float prc){return ControlPaint.Light (color,prc);}



        #endregion

        #region Public properties

        public Color ForeColor { get { return ForeColorInternal; } }
        public Color BackColor { get { return BackColorInternal; } }
        public Color DisableColor { get { return DisableColorInternal; } }

        public Color BorderColor { get { return BorderColorInternal; } }
        public Color BorderHotColor { get { return BorderHotColorInternal; } }
        public Color FocusedColor { get { return FocusedColorInternal; } }
        public Color BackgroundColor { get { return BackgroundColorInternal; } }
        public Color ButtonBorderColor { get { return ButtonBorderColorInternal; } }

        public Color ColorBrush1 { get { return ColorBrush1Internal; } }
        public Color ColorBrush2 { get { return ColorBrush2Internal; } }
        public Color ColorBrushLower { get { return ColorBrushLowerInternal; } }
        public Color ColorBrushUpper { get { return ColorBrushUpperInternal; } }
        public Color CaptionColor { get { return CaptionColorInternal; } }
        public Color AlternatingColor { get { return AlternatingColorInternal; } }
        public Color CaptionLightColor { get { return CaptionLightColorInternal; } }
        public Color HeaderColor { get { return HeaderColorInternal; } }
        public Color HeaderTextColor { get { return HeaderTextColorInternal; } }
        public Color CaptionTextColor { get { return CaptionTextColorInternal; } }

        public Color HighlightColor { get { return HighlightColorInternal; } }
        public Color SelectedColor { get { return SelectedColorInternal; } }
        public Color ContentColor { get { return ContentColorInternal; } }
        public Color SelectedTextColor { get { return SelectedTextColorInternal; } }


        #endregion

        #region properties

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [System.ComponentModel.RefreshProperties(RefreshProperties.All)]
        public virtual Styles StylePlan
        {
            get { return this.m_StylePlan; }
            set
            {
                if (m_StylePlan != value)
                {
                    SetStyleColors(value);
                    OnColorStyleChanged(value);
                    if (value == Styles.Custom)
                        OnPropertyChanged("StyleLayout");
                    else
                        OnPropertyChanged("StylePlan");

                }
            }
        }

        private Color CalcHeaderColor()
        {
            return CalcColor(ColorBrush1Internal, ColorBrush2Internal, 30);
        }

        private Color CalcSelectedColor()
        {
            return CalcColor(CaptionColorInternal, SystemColors.Window, 30);
        }

        private Color CalcSelectedTextColor()
        {
            return SystemColors.Window;//CalcColor(SystemColors.Window,CaptionColorInternal, 30);

        }

        private Color CalcContentColor()
        {
            return CalcColor(SystemColors.Window, SystemColors.Control, 200);
        }

        private Color CalcHighlightColor()
        {
            return Color.Orange;// CalcColor(Color.Red, Color.Yellow, 120);
        }

        internal protected virtual Color FormColor
        {
            get
            {
                return ColorBrush2Internal;
            }
        }

        #endregion

        #region Function

        private Color CalcColor(Color front, Color back, int alpha)
        {
            Color color1 = Color.FromArgb(0xff, front);
            Color color2 = Color.FromArgb(0xff, back);
            float single1 = color1.R;
            float single2 = color1.G;
            float single3 = color1.B;
            float single4 = color2.R;
            float single5 = color2.G;
            float single6 = color2.B;
            float single7 = ((single1 * alpha) / 255f) + (single4 * (((float)(0xff - alpha)) / 255f));
            byte num1 = (byte)single7;
            float single8 = ((single2 * alpha) / 255f) + (single5 * (((float)(0xff - alpha)) / 255f));
            byte num2 = (byte)single8;
            float single9 = ((single3 * alpha) / 255f) + (single6 * (((float)(0xff - alpha)) / 255f));
            byte num3 = (byte)single9;
            return Color.FromArgb(0xff, num1, num2, num3);
        }

        internal protected void SetFlatColor()// FlatColorSetting()
        {
            if (StylePlan == Styles.Custom || FlatLayoutInternal == FlatLayout.Flat)
            {
                // do nothing
            }
            else if (FlatLayoutInternal == FlatLayout.Light)
                BackgroundColorInternal = ColorBrush2Internal;
            else
                BackgroundColorInternal = ColorBrush1Internal;

        }

        internal protected void SetFlatColor(Color value)
        {
            if (StylePlan == Styles.Custom || StylePlan == Styles.None)
            {
                switch (this.FlatLayoutInternal)
                {
                    case FlatLayout.Dark:
                        BackgroundColorInternal = ControlPaint.Dark(value);
                        break;
                    case FlatLayout.Light:
                        BackgroundColorInternal = ControlPaint.LightLight(value);
                        break;
                    default://case FlatLayout.Flat:
                        BackgroundColorInternal = value;
                        break;
                }
            }
            else
            {
                SetFlatColor();
            }
        }

        internal protected Color GetFlatColor(FlatLayout layout)
        {
            switch (layout)
            {
                case FlatLayout.Dark:
                    return this.ColorBrush1Internal;
                case FlatLayout.Light:
                    return this.ColorBrush2Internal;
                default://FlatLayout.Flat:
                    return this.BackgroundColorInternal;
            }
        }

        #endregion

        #region Style Methods

        [Browsable(false)]
        public StyleLayout Layout
        {
            get { return this; }
        }

        public virtual void SetStyleLayout(Styles value)
        {
            if (StylePlan != Styles.None && StylePlan != value)
            {
                SetStyleColors(value);
            }
        }

        public virtual void SetStyleLayout(StyleLayout value)
        {
            if (StylePlan == Styles.None)
            {
                //do nothing
                return;
            }
            if (value.StylePlan == Styles.Custom)
            {
                StylePlan = value.StylePlan;
                BorderColorInternal = value.BorderColorInternal;
                BorderHotColorInternal = value.BorderHotColorInternal;
                FocusedColorInternal = value.FocusedColorInternal;
                ColorBrush1Internal = value.ColorBrush1Internal;
                ColorBrush2Internal = value.ColorBrush2Internal;
                ColorBrushLowerInternal = value.ColorBrushLowerInternal;
                ColorBrushUpperInternal = value.ColorBrushUpperInternal;
                CaptionColorInternal = value.CaptionColorInternal;
                BackgroundColorInternal = value.BackgroundColorInternal;
                CaptionLightColorInternal = value.CaptionLightColorInternal;
                ButtonBorderColorInternal = value.ButtonBorderColorInternal;
                //m_SelectedColor=CalcColor(CaptionColorInternal, SystemColors.Window, 30);

                ForeColorInternal = value.ForeColorInternal;
                BackColorInternal = value.BackColorInternal;
                DisableColorInternal = value.DisableColorInternal;
                FlatLayoutInternal = value.FlatLayoutInternal;
                HeaderTextColorInternal = value.HeaderTextColorInternal;
                CaptionTextColorInternal = value.CaptionTextColorInternal;
                HeaderColorInternal = value.HeaderColorInternal;

                HighlightColorInternal = CalcHighlightColor();
                SelectedColorInternal = CalcSelectedColor();
                ContentColorInternal = CalcContentColor();
                SelectedTextColorInternal = CalcSelectedTextColor();
                //HeaderColorInternal = CalcHeaderColor();
                AlternatingColorInternal = value.AlternatingColorInternal;


            }
            else if (StylePlan != value.StylePlan)
            {
                SetStyleColors(value.StylePlan);
            }
        }

        private void SetStyleColors(Styles value)
        {

            m_StylePlan = value;
            switch (value)
            {
                case Styles.SteelBlue:
                    BorderColorInternal = Color.SteelBlue;
                    BorderHotColorInternal = Color.Blue;
                    FocusedColorInternal = Color.FromArgb(0, 0, 192);//.Gold ;
                    ColorBrush1Internal = Color.LightSteelBlue;
                    ColorBrush2Internal = Color.AliceBlue;
                    ColorBrushLowerInternal = Color.FromArgb(137, 174, 237);
                    ColorBrushUpperInternal = Color.AliceBlue;
                    CaptionColorInternal = Color.SteelBlue;//.Navy;
                    CaptionLightColorInternal = Color.LightSteelBlue;
                    ButtonBorderColorInternal = Color.Navy;
                    break;
                case Styles.Goldenrod:
                    BorderColorInternal = Color.Goldenrod;
                    BorderHotColorInternal = Color.FromArgb(190, 128, 0);//Goldenrod ;
                    FocusedColorInternal = Color.FromArgb(128, 64, 0);
                    ColorBrush1Internal = Color.PaleGoldenrod;
                    ColorBrush2Internal = Color.Ivory;//.LightGoldenrodYellow;
                    ColorBrushLowerInternal =  Color.FromArgb(234, 206, 103);
                    ColorBrushUpperInternal = Color.Ivory;//.LightGoldenrodYellow;
                    CaptionColorInternal = Color.DarkGoldenrod;
                    CaptionLightColorInternal = Color.FromArgb(244, 237, 176);
                    ButtonBorderColorInternal = Color.FromArgb(128, 64, 0);
                    break;
                case Styles.SeaGreen:
                    BorderColorInternal = Color.FromArgb(152, 186, 168);//.SeaGreen ;
                    BorderHotColorInternal = Color.ForestGreen;// Color.FromArgb(0, 64, 64);
                    FocusedColorInternal = Color.FromArgb(0, 64, 0);
                    ColorBrush1Internal = Color.FromArgb(175, 214, 194);//.DarkSeaGreen;
                    ColorBrush2Internal = Color.MintCream;
                    ColorBrushLowerInternal = Color.FromArgb(152, 200, 194);//.DarkSeaGreen;
                    ColorBrushUpperInternal = Color.MintCream;
                    CaptionColorInternal = Color.FromArgb(92, 119, 129);//(52, 99, 79);//Color.SeaGreen;//Color.DarkGreen ;
                    CaptionLightColorInternal = Color.FromArgb(163, 198, 180);
                    ButtonBorderColorInternal = Color.FromArgb(52, 99, 79);
                    break;
                case Styles.Carmel:
                    BorderColorInternal = Color.Tan;
                    BorderHotColorInternal = Color.Peru;//.SaddleBrown;
                    FocusedColorInternal = Color.FromArgb(125, 87, 17);// Color.FromArgb(192, 192, 0);
                    ColorBrush1Internal = Color.FromArgb(211, 193, 162);
                    ColorBrush2Internal = Color.OldLace;//.Linen;
                    ColorBrushLowerInternal = Color.BurlyWood;
                    ColorBrushUpperInternal = Color.OldLace;//.Linen;
                    CaptionColorInternal = Color.FromArgb(165, 131, 107);//(165, 151, 127);//160, 122, 81);//142, 94, 57);//);//Color.FromArgb(84, 52, 84);//Color.Purple ;
                    CaptionLightColorInternal = Color.FromArgb(227, 217, 204);
                    ButtonBorderColorInternal = Color.FromArgb(125, 87, 17);
                    break;
                case Styles.Silver:
                    BorderColorInternal = Color.DimGray;
                    BorderHotColorInternal = Color.SlateGray;
                    FocusedColorInternal = Color.CornflowerBlue;//.RosyBrown;
                    ColorBrush1Internal = Color.Silver;
                    ColorBrush2Internal = Color.WhiteSmoke;
                    ColorBrushLowerInternal = Color.LightSlateGray;
                    ColorBrushUpperInternal = Color.WhiteSmoke;
                    CaptionColorInternal = Color.SlateGray;//.FromArgb(160, 156, 142);// SystemColors.ActiveCaption;// Color.FromArgb(64, 64, 74);//Color.Black ;
                    CaptionLightColorInternal = Color.FromArgb(226, 221, 201);
                    ButtonBorderColorInternal = Color.Navy;
                    break;
                case Styles.Media:
                    BorderColorInternal = Color.Gray;
                    BorderHotColorInternal = Color.SlateGray;
                    FocusedColorInternal = Color.Blue;
                    ColorBrush1Internal = Color.FromArgb(68, 76, 93);
                    ColorBrush2Internal = Color.FromArgb(243, 247, 252);
                    ColorBrushLowerInternal = Color.FromArgb(68, 76, 93);
                    ColorBrushUpperInternal = Color.FromArgb(243, 247, 252);
                    CaptionColorInternal = Color.FromArgb(68, 76, 93);
                    CaptionLightColorInternal = Color.FromArgb(238, 243, 250);
                    ButtonBorderColorInternal = Color.Navy;
                    break;
                case Styles.Desktop:
                    BorderColorInternal = Color.FromArgb(49, 106, 197);// SystemColors.Highlight;// Desktop;
                    BorderHotColorInternal = Color.FromArgb(26, 80, 184);// SystemColors.HotTrack;
                    FocusedColorInternal = Color.RoyalBlue;//.FromArgb(193, 192, 182);// SystemColors.ActiveBorder;
                    ColorBrush1Internal = Color.FromArgb(205, 203, 183);// SystemColors.ButtonFace;// this.CalcColor(SystemColors.ControlLight, BorderColorInternal, 220);

                    //ColorBrush1Internal=Color.FromArgb(224, 224, 224);
                    ColorBrush2Internal = StyleLayout.DefaultControlColor;// Color.FromArgb(247, 245, 232);//System.Drawing.Color.White;
                    ColorBrushLowerInternal = Color.LightSlateGray;//.FromArgb(137, 174, 237);// SystemColors.ButtonFace;// this.CalcColor(SystemColors.ControlLight, BorderColorInternal, 220);
                    ColorBrushUpperInternal = Color.WhiteSmoke;// Color.FromArgb(247, 245, 232);//System.Drawing.Color.White;
                    CaptionColorInternal = Color.FromArgb(99, 126, 177);//(0, 78, 152);//SystemColors.ActiveCaption ;
                    CaptionLightColorInternal = Color.FromArgb(204, 223, 252);
                    ButtonBorderColorInternal = Color.Navy;
                    break;
                case Styles.None:
                case Styles.Custom:
                    //do nothing
                    goto lblExit;
                //break;
                default://System
                    BorderColorInternal = SystemColors.ActiveBorder;// Gray;
                    BorderHotColorInternal = SystemColors.Highlight;// Gray;
                    FocusedColorInternal = SystemColors.ControlDark;// Gray;
                    ColorBrush1Internal = SystemColors.Control;
                    ColorBrush2Internal = SystemColors.ControlLight;
                    ColorBrushLowerInternal = SystemColors.ControlDark;
                    ColorBrushUpperInternal = SystemColors.ControlLight;
                    CaptionColorInternal = SystemColors.Highlight;// Navy;
                    CaptionLightColorInternal = SystemColors.InactiveCaptionText;
                    ButtonBorderColorInternal = Color.Navy;
                    break;
            }

            AlternatingColorInternal = LightLightColor;
            //FocusedColorInternal = DarkColor;
            SetFlatColor();

            HighlightColorInternal = CalcHighlightColor();
            SelectedColorInternal = CalcSelectedColor();
            ContentColorInternal = CalcContentColor();
            SelectedTextColorInternal = CalcSelectedTextColor();
            HeaderColorInternal = CalcHeaderColor();
            HeaderTextColorInternal = ForeColorInternal;
            CaptionTextColorInternal = ColorBrush2Internal;

        lblExit:
            return;
            //ForeColorInternal=m_ForeColor; 
            //BackColorInternal=m_BackColor ; 
            //DisableColorInternal=m_DisableColor ; 
            //m_SelectedColor=CalcColor(CaptionColorInternal, SystemColors.Window, 30);

        }


        #endregion

        #region Brush and Pens Internal

        public Brush GetBrushFlatGradient(Rectangle rectangle, float angle)
        {
            Color color1 = ControlPaint.Dark(BackgroundColorInternal);// this.CalcColor(this.BackgroundColorInternal,BackgroundColorInternal,200); 
            Color color2 = ControlPaint.Light(BackgroundColorInternal);
            return new LinearGradientBrush(rectangle, color1, color2, angle);
        }

        #endregion

        #region Brushes and Pens

        public Brush GetBrushGradientDark(Rectangle rectangle, float angle)
        {
            return new LinearGradientBrush(rectangle, ColorBrushLowerInternal, ColorBrushUpperInternal, angle);
        }

        public Brush GetBrushGradientDark(Rectangle rectangle, float angle, bool revers)
        {
            if (revers)
                return new LinearGradientBrush(rectangle, ColorBrushUpperInternal, ColorBrushLowerInternal, angle);
            else
                return new LinearGradientBrush(rectangle, ColorBrushLowerInternal, ColorBrushUpperInternal, angle);
        }

        public Brush GetBrushGradient(Rectangle rectangle, float angle)
        {
            return new LinearGradientBrush(rectangle, ColorBrush1Internal, ColorBrush2Internal, angle);
        }

        public Brush GetBrushButton(Rectangle rectangle, float angle)
        {
            return new LinearGradientBrush(rectangle, ColorBrush2Internal, Color.White, angle);
        }

        public Brush GetBrushGradient(Rectangle rectangle, float angle, bool revers)
        {
            if (revers)
                return new LinearGradientBrush(rectangle, ColorBrush2Internal, ColorBrush1Internal, angle);
            else
                return new LinearGradientBrush(rectangle, ColorBrush1Internal, ColorBrush2Internal, angle);
        }

        public Brush GetBrushCaptionGradient(Rectangle rectangle, float angle, bool revers)
        {
            if (revers)
                return new LinearGradientBrush(rectangle, CaptionLightColorInternal, CaptionColorInternal, angle);
            else
                return new LinearGradientBrush(rectangle, CaptionColorInternal, CaptionLightColorInternal, angle);
        }

        public Brush GetBrushGradient(Rectangle rectangle)
        {
            return new LinearGradientBrush(rectangle, ColorBrush1Internal, ColorBrush2Internal, 270f);
        }

        public Brush GetBrushGradient(Rectangle rectangle, Color start, Color end, float angle)
        {
            return new LinearGradientBrush(rectangle, start, end, angle);
        }

        public Brush GetBrushFlat()
        {
            return new SolidBrush(BackgroundColorInternal);
        }
        public Brush GetBrushFlat(FlatLayout value)
        {
            if (value == FlatLayout.Dark)
                return new SolidBrush(ColorBrush1Internal);
            else if (value == FlatLayout.Light)
                return new SolidBrush(ColorBrush2Internal);
            else
                return new SolidBrush(BackgroundColorInternal);
        }
        public Brush GetBrushClick()
        {
            return new SolidBrush(ClickColor);
        }
        public Brush GetBrushText()
        {
            return new SolidBrush(ForeColorInternal);
        }
        public Brush GetBrushText(bool Enabled)
        {
            if (Enabled)
                return new SolidBrush(ForeColorInternal);
            else
                return new SolidBrush(DisableColorInternal);
        }
        public Brush GetBrushText(bool Enabled, bool ReadOnly)
        {
            if (ReadOnly)
                return new SolidBrush(ForeColorInternal);
            else if (Enabled)
                return new SolidBrush(ForeColorInternal);
            else
                return new SolidBrush(DisableColorInternal);
        }
        public Brush GetBrushBack()
        {
            return new SolidBrush(BackColorInternal);
        }
        public Brush GetBrushBack(bool Enabled)
        {
            if (Enabled)
                return new SolidBrush(BackColorInternal);
            else
                return new SolidBrush(DisableColorInternal);
        }
        public Brush GetBrushBack(bool Enabled, bool ReadOnly)
        {
            if (ReadOnly)
                return new SolidBrush(DisableColorInternal);
            else if (Enabled)
                return new SolidBrush(BackColorInternal);
            else
                return new SolidBrush(DisableColorInternal);
        }
        public Brush GetBrushAlternating()
        {
            return new SolidBrush(AlternatingColorInternal);
        }
        public Brush GetBrushSelected()
        {
            return new SolidBrush(SelectedColorInternal);
        }
        public Brush GetBrushSelectedText()
        {
            return new SolidBrush(SelectedTextColorInternal);
        }
        public Brush GetBrushCaption()
        {
            return new SolidBrush(CaptionColorInternal);
        }
        public Brush GetBrushCaptionText()
        {
            return new SolidBrush(CaptionTextColorInternal);//SelectedTextColorInternal);
        }

        public Brush GetBrushFlatLayout()
        {
            return new SolidBrush(BackgroundColorInternal);
        }
        public Brush GetBrushDisabled()
        {
            return new SolidBrush(DisableColorInternal);
        }
        public Brush GetBrushContent()
        {
            return new SolidBrush(ContentColorInternal);
        }
        public Brush GetBrushHot()
        {
            return new SolidBrush(BorderHotColorInternal);
        }
        public Brush GetBrushHeader()
        {
            return new SolidBrush(HeaderColorInternal);
        }
        public Brush GetBrushHeaderText()
        {
            return new SolidBrush(HeaderTextColorInternal);
        }
        public Pen GetPenBorder()
        {
            return new Pen(BorderColorInternal);
        }
        public Pen GetPenHot()
        {
            return new Pen(BorderHotColorInternal);
        }
        public Pen GetPenFocused()
        {
            return new Pen(FocusedColorInternal);
        }
        public Pen GetPenButton()
        {
            return new Pen(ButtonBorderColorInternal);
        }
        public Pen GetPenDisable()
        {
            return new Pen(DisableColorInternal);
        }
        public Pen GetPenDark()
        {
            return new Pen(ColorBrush1Internal);
        }
        public Pen GetPenLight()
        {
            return new Pen(ColorBrush2Internal);
        }
        public Pen GetPenInActive()
        {
            return new Pen(Color.DarkGray);
        }

        public Pen GetPenBorder(bool readOnly, bool Enable, bool Focused, bool hot)
        {
            if (!Enable)
            {
                if (readOnly)
                    return new Pen(BorderColorInternal);
                else
                    return new Pen(DisableColorInternal);
            }
            else if (Focused)
                return new Pen(FocusedColorInternal);
            else if (hot)
                return new Pen(BorderHotColorInternal);
            else
                return new Pen(BorderColorInternal);
        }


        #endregion

        #region Fonts

        public Font TextFont
        {
            get { return TextFontInternal; }
        }

        public Font CaptionFont
        {
            get { return CaptionFontInternal; }
        }

        #endregion

        #region Public Methods

        public override string ToString()
        {
            return this.StylePlan.ToString();
        }

        //		public static bool IsXpLayout(ControlLayout ctlLayout)
        //		{
        //			if(ctlLayout ==ControlLayout.XpFlat || ctlLayout==ControlLayout.XpLayout)
        //				return true;
        //			return false;
        //		}

        public static bool IsGradientLayout(ControlLayout ctlLayout)
        {
            if (ctlLayout == ControlLayout.Visual || ctlLayout == ControlLayout.XpLayout || ctlLayout== ControlLayout.VistaLayout)
                return true;
            return false;
        }

        public ThemeSchemes GetCurrentThemeScheme()
        {
            try
            {
                StringBuilder sb1 = new StringBuilder(256);
                StringBuilder sb2 = new StringBuilder(256);
                int i = Win32.WinAPI.GetCurrentThemeName(sb1, sb1.Capacity, sb2, sb2.Capacity, null, 0);
                string str = sb2.ToString();

                switch (str)
                {
                    case @"HomeStead":
                        return ThemeSchemes.OliveGreen;
                    case @"Metallic":
                        return ThemeSchemes.Silver;
                    default:
                        return ThemeSchemes.Blue;
                }
            }
            catch (Exception)
            {
                return ThemeSchemes.Blue;
            }
        }
        #endregion

        #region Drawing Base Methods

        public Color GetBorderColor(bool readOnly, bool Enable, bool Focused, bool hot)//,FlatStyles style)
        {
            if (!Enable)
            {
                if (readOnly)
                    return this.BorderColorInternal;
                else
                    return this.DisableColorInternal;
            }
            else if (Focused)
                return this.FocusedColorInternal;
            else if (hot)
                return this.BorderHotColorInternal;
            else
                return this.BorderColorInternal;
        }

        public Color GetBackColor(bool Enable, bool ReadOnly)
        {
            if (ReadOnly)
                return this.BackColorInternal;
            if (!Enable)
                return this.DisableColorInternal;
            else
                return this.BackColorInternal;
        }

        public Color GetForeColor(bool Enable, bool ReadOnly)
        {
            if (ReadOnly)
                return this.ForeColorInternal;
            if (!Enable)
                return this.DisableColorInternal;
            else
                return this.ForeColorInternal;
        }

        public void DrawMcEdit(Graphics g, Rectangle ctlRect, Control ctl, System.Windows.Forms.BorderStyle borderStyle, bool Enable, bool Focused, bool hot, bool readOnly)
        {

            //if(ctl!=null)
            //ctl.BackColor = GetBackColor(Enable,readOnly);

            if (borderStyle == BorderStyle.Fixed3D)
            {
                ControlPaint.DrawBorder3D(g, ctlRect, Border3DStyle.Sunken);
            }
            else if (borderStyle != BorderStyle.None)
            {
                Rectangle rect = new Rectangle(ctlRect.X, ctlRect.Y, ctlRect.Width - 1, ctlRect.Height - 1);
                System.Drawing.Color penColor;

                if (!Enable)
                {
                    if (readOnly)
                        penColor = this.BorderColorInternal;
                    else
                        penColor = this.DisableColorInternal;
                }
                else if (Focused)
                    penColor = this.FocusedColorInternal;
                else if (hot)
                    penColor = this.BorderHotColorInternal;
                else
                    penColor = this.BorderColorInternal;

                using (Pen pen = new Pen(penColor, 1))
                {
                    g.DrawRectangle(pen, rect);
                }
            }

        }

        public void DrawControl(Graphics g, Rectangle rect, System.Windows.Forms.BorderStyle borderStyle, bool Enable, bool Focused, bool hot)
        {
            Pen pen;
            Brush sb;

            if (!Enable)
            {
                sb = this.GetBrushDisabled();
                pen = this.GetPenDisable();
            }
            else if (Focused)
            {
                sb = this.GetBrushBack();
                pen = this.GetPenFocused();
            }
            else if (hot)
            {
                sb = this.GetBrushBack();
                pen = this.GetPenHot();
            }
            else
            {
                sb = this.GetBrushBack();
                pen = this.GetPenBorder();
            }

            g.FillRectangle(sb, rect);

            if (borderStyle == BorderStyle.FixedSingle)
            {
                g.DrawRectangle(pen, rect);
            }
            sb.Dispose();
            pen.Dispose();
        }

        public void DrawBorder3D(Graphics g, Rectangle rect)
        {
            ControlPaint.DrawBorder3D(g, rect, System.Windows.Forms.Border3DStyle.Sunken);
        }

        public void DrawBorder3D(Graphics g, Rectangle rect, System.Windows.Forms.Border3DStyle borderStyle)
        {
            ControlPaint.DrawBorder3D(g, rect, borderStyle);
        }

        public void DrawBorder(Graphics g, Rectangle rect, bool readOnly, bool Enable, bool Focused, bool hot)
        {
            using (Pen p = this.GetPenBorder(readOnly, Enable, Focused, hot))
            {
                g.DrawRectangle(p, rect);
            }
        }

        public void DrawBorder(Graphics g, System.Drawing.Drawing2D.GraphicsPath path, bool readOnly, bool Enable, bool Focused, bool hot)
        {
            using (Pen p = this.GetPenBorder(readOnly, Enable, Focused, hot))
            {
                g.DrawPath(p, path);
            }
        }

        public virtual void DrawBackColor(Graphics g, Rectangle rect, bool Enabled, bool ReadOnly)
        {
            using (Brush sb = this.GetBrushBack(Enabled, ReadOnly))
            {
                g.FillRectangle(sb, rect);
            }
        }

        public virtual void DrawForeColor(Graphics g, Rectangle rect, HorizontalAlignment TextAlign, string Text, Font font)
        {
            using (StringFormat format = new StringFormat())
            {
                format.LineAlignment = StringAlignment.Center;

                if (TextAlign == HorizontalAlignment.Left)
                    format.Alignment = StringAlignment.Near;
                else if (TextAlign == HorizontalAlignment.Center)
                    format.Alignment = StringAlignment.Center;
                else if (TextAlign == HorizontalAlignment.Right)
                    format.Alignment = StringAlignment.Far;

                //format.HotkeyPrefix  = HotkeyPrefix.Show;
                using (Brush bs = this.GetBrushText())
                {
                    g.DrawString(Text, font, bs, rect, format);
                }
            }
        }

        public StringAlignment HorizontalToStringAlignment(HorizontalAlignment align)
        {
            if (align == HorizontalAlignment.Left)
                return StringAlignment.Near;
            else if (align == HorizontalAlignment.Center)
                return StringAlignment.Center;
            else //if (align == HorizontalAlignment.Right)
                return StringAlignment.Far;
        }

        /*public void DrawForeColor(Graphics g,Point p)
        {
            using(StringFormat format  = new StringFormat())
            {
                format.LineAlignment = StringAlignment.Near ;
                format.Alignment = StringAlignment.Center;
                format.HotkeyPrefix  =  System.Drawing.Text.HotkeyPrefix.Show;
			
                //format.HotkeyPrefix  = HotkeyPrefix.Show;
                using (Brush bs = new SolidBrush(Parent.ForeColorInternal ))
                {
                    //g.DrawString(Parent.Text,Parent.Font,bs,rect,format);
                    g.DrawString(Parent.Text, Parent.Font, bs, p, format);
                }
            }
        }*/

        public virtual void DrawString(Graphics g, Brush bs, Rectangle rect, ContentAlignment alignment, string Text, Font font)
        {
            SizeF size = g.MeasureString(Text, font);
            PointF iPoint = GetContentPoint(alignment, rect, size, 1, 1);
            g.DrawString(Text, font, bs, iPoint.X, iPoint.Y);
        }

        public virtual void DrawString(Graphics g, Rectangle rect, ContentAlignment alignment, string Text, Font font)
        {
            SizeF size = g.MeasureString(Text, font);
            PointF iPoint = GetContentPoint(alignment, rect, size, 1, 1);
            using (Brush bs = this.GetBrushText())
            {
                g.DrawString(Text, font, bs, iPoint.X, iPoint.Y);
            }
        }

        public virtual void DrawString(Graphics g, Rectangle rect, ContentAlignment alignment, string Text, Font font, bool Enabled)
        {
            SizeF size = g.MeasureString(Text, font);
            PointF iPoint = GetContentPoint(alignment, rect, size, 1, 1);
            using (Brush bs = this.GetBrushText(Enabled))
            {
                g.DrawString(Text, font, bs, iPoint.X, iPoint.Y);
            }
        }

        public virtual void DrawString(Graphics g, Rectangle rect, ContentAlignment alignment, string Text, Font font, bool clicked, bool Enabled)
        {
            SizeF size = g.MeasureString(Text, font, rect.Width);
            PointF iPoint = GetContentPoint(alignment, rect, size, 0, 0);
            using (Brush bs = this.GetBrushText(Enabled))
            {
                if (clicked)
                    g.DrawString(Text, font, bs, iPoint.X + 1, iPoint.Y + 1);
                else
                    g.DrawString(Text, font, bs, iPoint.X, iPoint.Y);

            }
        }

        public virtual void DrawString(Graphics g, Rectangle rect, ContentAlignment alignment, string Text, Font font, RightToLeft rtl, bool Enabled)
        {
            using (StringFormat sf = GetStringFormat(alignment, false, rtl))
            {
                using (Brush bs = this.GetBrushText(Enabled))
                {
                    g.DrawString(Text, font, bs, (RectangleF)rect, sf);
                }
            }
        }

 
        public virtual void DrawTextAndImage(Graphics g, Rectangle rect, Image image, RightToLeft rtl, string text, Font font, bool Enabled)
        {
            DrawTextAndImage(g, rect, image, (rtl == RightToLeft.Yes) ? HorizontalAlignment.Right : HorizontalAlignment.Left, text, font, Enabled);
        }

        public virtual void DrawTextAndImage(Graphics g, Rectangle rect, Image image, HorizontalAlignment alignment, string text, Font font, bool Enabled)
        {
            Rectangle imageRect = Rectangle.Empty;
            Rectangle textRect = Rectangle.Empty;
            Size size = new Size(0, 0);
            int imageGap = 0;
            if (image != null)
            {
                size = image.Size;
                imageGap = 2;
            }
            switch (alignment)
            {
                case HorizontalAlignment.Left:
                    imageRect = new Rectangle(rect.X + imageGap, rect.Y, size.Width, size.Height);
                    textRect = new Rectangle(imageRect.Right + 2, rect.Y, rect.Width - size.Width - 2 - imageGap, rect.Height);
                    break;
                case HorizontalAlignment.Right:
                    imageRect = new Rectangle(rect.Width - size.Width - imageGap, rect.Y, size.Width, size.Height);
                    textRect = new Rectangle(rect.X + 2, rect.Y, rect.Width - size.Width - 2 - imageGap-2, rect.Height);
                    break;
                default:
                    SizeF stringSize = g.MeasureString(text, font);
                    if (stringSize.Width > (rect.Width-size.Width))
                    {
                        imageRect = new Rectangle(rect.X + imageGap, rect.Y, size.Width, size.Height);
                        textRect = new Rectangle(rect.X + imageRect.Right + 2, rect.Y, rect.Width - size.Width - 2 - imageGap, rect.Height);
                    }
                    else
                    {
                        imageRect = new Rectangle((rect.Width - (int)stringSize.Width - imageRect.Width-2-imageGap) / 2, rect.Y, size.Width, size.Height);
                        textRect = new Rectangle(imageRect.Right, rect.Y, rect.Width - size.Width, rect.Height);
                    }
                    break;
            }

            if (image != null)
            {
                imageRect.Y = (rect.Height - size.Height) / 2;
                g.DrawImage(image, imageRect);
            }
            if (string.IsNullOrEmpty(text))
                return;

            using (StringFormat sf = new StringFormat())
            {
                if (alignment == HorizontalAlignment.Left)
                    sf.Alignment = StringAlignment.Near;
                else if (alignment == HorizontalAlignment.Center)
                    sf.Alignment = StringAlignment.Near;//.Center;
                else //if (align == HorizontalAlignment.Right)
                    sf.Alignment = StringAlignment.Far;

                //sf.Alignment = HorizontalToStringAlignment(alignment);
                sf.LineAlignment = StringAlignment.Center;
                sf.Trimming = StringTrimming.EllipsisCharacter;
                sf.FormatFlags = StringFormatFlags.NoClip |
                StringFormatFlags.NoWrap;

                RectangleF drawRectF = (RectangleF)textRect;
                drawRectF.Height = font.Height;
                drawRectF.Y = (rect.Height - font.Height) / 2;
                using (Brush bs = this.GetBrushText(Enabled))
                {
                    g.DrawString(text, font, bs, (RectangleF)drawRectF, sf);
                }

            }
        }

        public StringFormat GetStringFormat(ContentAlignment alignment, bool wordWrap, RightToLeft rtl)
        {
            StringFormat format1 = new StringFormat();
            format1.Trimming = StringTrimming.EllipsisCharacter;
            //format1.FormatFlags = 0;
            format1.FormatFlags = StringFormatFlags.NoClip;
            if (!wordWrap)
            {
                format1.FormatFlags |= StringFormatFlags.NoWrap;
            }
            format1.HotkeyPrefix = HotkeyPrefix.Show;
            if (rtl == RightToLeft.Yes)
            {
                format1.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
            }
            //			if ((this.TextAlign == ContentAlignment.MiddleCenter) && (base.ImageAlign == ContentAlignment.MiddleCenter))
            //			{
            //				format1.Alignment = StringAlignment.Center;
            //				format1.LineAlignment = StringAlignment.Center;
            //				return format1;
            //			}
            //			ContentAlignment alignment1 = this.TextAlign;
            if (alignment <= ContentAlignment.MiddleCenter)
            {
                switch (alignment)
                {
                    case ContentAlignment.TopLeft:
                        {
                            format1.Alignment = StringAlignment.Near;
                            format1.LineAlignment = StringAlignment.Near;
                            return format1;
                        }
                    case ContentAlignment.TopCenter:
                        {
                            format1.Alignment = StringAlignment.Center;
                            format1.LineAlignment = StringAlignment.Near;
                            return format1;
                        }
                    case (ContentAlignment.TopCenter | ContentAlignment.TopLeft):
                        {
                            return format1;
                        }
                    case ContentAlignment.TopRight:
                        {
                            format1.Alignment = StringAlignment.Far;
                            format1.LineAlignment = StringAlignment.Near;
                            return format1;
                        }
                    case ContentAlignment.MiddleLeft:
                        {
                            format1.Alignment = StringAlignment.Near;
                            format1.LineAlignment = StringAlignment.Center;
                            return format1;
                        }
                    case ContentAlignment.MiddleCenter:
                        {
                            format1.Alignment = StringAlignment.Center;
                            format1.LineAlignment = StringAlignment.Center;
                            return format1;
                        }
                }
                return format1;
            }
            if (alignment <= ContentAlignment.BottomLeft)
            {
                if (alignment == ContentAlignment.MiddleRight)
                {
                    format1.Alignment = StringAlignment.Far;
                    format1.LineAlignment = StringAlignment.Center;
                    return format1;
                }
                if (alignment == ContentAlignment.BottomLeft)
                {
                    format1.Alignment = StringAlignment.Near;
                    format1.LineAlignment = StringAlignment.Far;
                }
                return format1;
            }
            if (alignment == ContentAlignment.BottomCenter)
            {
                format1.Alignment = StringAlignment.Center;
                format1.LineAlignment = StringAlignment.Far;
                return format1;
            }
            if (alignment == ContentAlignment.BottomRight)
            {
                format1.Alignment = StringAlignment.Far;
                format1.LineAlignment = StringAlignment.Far;
            }
            return format1;
        }


        public virtual void DrawButtonImage(Graphics g, Rectangle rect, Image image, ContentAlignment alignment, bool Enabled, int pedingX, int pedingY)
        {
            //Image image=((IButton)Parent).Image;
            if (image != null)
            {
                SizeF size = new SizeF(image.Width, image.Height);
                PointF iPoint = GetContentPoint(alignment, rect, size, pedingX, pedingY);
                if (Enabled)
                {
                    g.DrawImage(image, (int)iPoint.X, (int)iPoint.Y);
                }
                else
                {
                    try
                    {
                        ControlPaint.DrawImageDisabled(g, image, (int)iPoint.X + 1, (int)iPoint.Y + 1, SystemColors.Control);
                    }
                    catch { }
                }
            }
        }

        public virtual void DrawImage(Graphics g, Rectangle rect, Image image, ContentAlignment alignment, bool Enabled)//,Color backColor)
        {
            if (image != null)
            {
                SizeF size = new SizeF(image.Width, image.Height);
                PointF iPoint = GetContentPoint(alignment, rect, size, 2, 1);
                if (Enabled)
                    g.DrawImage(image, (int)iPoint.X, (int)iPoint.Y);
                else
                {
                    try
                    {
                        ControlPaint.DrawImageDisabled(g, image, (int)iPoint.X, (int)iPoint.Y, SystemColors.Control);
                    }
                    catch { }
                }
            }
        }

         public PointF GetContentPoint(ContentAlignment alignment, Rectangle rect, SizeF contentSize, int peddingX, int peddingY)
        {
            PointF p = new Point(0, 0);
            float difY = ((rect.Height - contentSize.Height) / 2);
            float difX = ((rect.Width - contentSize.Width) / 2);
            switch (alignment)
            {
                case ContentAlignment.BottomCenter:
                    p.X = rect.X + (difX < 0 ? 0 : difX);
                    p.Y = rect.Y + rect.Height - contentSize.Height - peddingY;
                    break;
                case ContentAlignment.BottomLeft:
                    p.X = rect.X + peddingX;
                    p.Y = rect.Y + rect.Height - contentSize.Height - peddingY;
                    break;
                case ContentAlignment.BottomRight:
                    p.X = rect.Right - contentSize.Width - peddingX;
                    p.Y = rect.Y + rect.Height - contentSize.Height - peddingY;
                    break;
                case ContentAlignment.MiddleCenter:
                    p.X = rect.X + (difX < 0 ? 0 : difX);
                    p.Y = rect.Top + (difY < 0 ? 0 : difY);
                    break;
                case ContentAlignment.MiddleLeft:
                    p.X = rect.X + peddingX;
                    p.Y = rect.Top + (difY < 0 ? 0 : difY);
                    break;
                case ContentAlignment.MiddleRight:
                    p.X = rect.Right - contentSize.Width - peddingX;
                    p.Y = rect.Top + (difY < 0 ? 0 : difY);
                    break;
                case ContentAlignment.TopCenter:
                    p.X = rect.X + (difX < 0 ? 0 : difX);
                    p.Y = rect.Top + peddingY;
                    break;
                case ContentAlignment.TopLeft:
                    p.X = rect.X + peddingX;
                    p.Y = rect.Top + peddingY;
                    break;
                case ContentAlignment.TopRight:
                    p.X = rect.Right - contentSize.Width - peddingX;
                    p.Y = rect.Top + peddingY;
                    break;
            }

            return p;
        }

        #endregion

        #region Drowing Rects

        public void DrawShadow(Graphics g, Rectangle rect, int width, bool top)
        {
            FillGradientRect(g, new Rectangle(rect.Right, rect.Top, width, rect.Height + width), 0f);
            if (top)
                FillGradientRect(g, new Rectangle(rect.Left, rect.Bottom, rect.Width + width, width), 90f);
            else
                FillGradientRect(g, new Rectangle(rect.Left, rect.Top - width, rect.Width + width, width), 270f);
        }

        public void FillGradientColor(Graphics g, Rectangle rect)
        {
            using (System.Drawing.Drawing2D.LinearGradientBrush sb = new System.Drawing.Drawing2D.LinearGradientBrush
                       (rect, this.ColorBrush1Internal, this.ColorBrush2Internal,
                       System.Drawing.Drawing2D.LinearGradientMode.Vertical))
            {

                g.FillRectangle(sb, rect);
            }
        }

        public void FillGradientRect(Graphics g, Rectangle rect, float angle)
        {
            using (Brush sb = this.GetBrushGradient(rect, angle))
            {
                g.FillRectangle(sb, rect);
            }
        }
        public void FillGradientPath(Graphics g, Rectangle rect, GraphicsPath path, float angle)
        {
            using (Brush sb = this.GetBrushGradient(rect, angle))
            {
                g.FillPath(sb, path);
            }
        }

        public void DrawButtonRect(Graphics g, Rectangle rect, ButtonStates state, float radius)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            System.Drawing.Drawing2D.GraphicsPath path = Nistec.Drawing.DrawUtils.GetRoundedRect(rect, radius);
            Brush sb = null;
            switch (state)
            {
                case ButtonStates.Pushed:
                    sb = this.GetBrushSelected();
                    break;
                default:
                    sb = this.GetBrushGradient(rect, 270f);
                    break;
            }
            g.FillPath(sb, path);
            sb.Dispose();
            using (Pen pen = this.GetPenButton())// 90f))
            {
                g.DrawPath(pen, path);
            }
        }

        public void DrawGradientRoundedRect(Graphics g, Rectangle rect, float radius, float angle)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            System.Drawing.Drawing2D.GraphicsPath path = Nistec.Drawing.DrawUtils.GetRoundedRect(rect, radius);
            using (Brush sb = this.GetBrushGradient(rect, angle))// 90f))
            {
                g.FillPath(sb, path);
            }
            using (Pen pen = this.GetPenBorder())
            {
                g.DrawPath(pen, path);
            }
        }
        public void FillRect(Graphics g, Rectangle rect, Color color)
        {
            using (Brush sb = new SolidBrush(color))
            {
                g.FillRectangle(sb, rect);
            }
        }
        //public void FillRect(Graphics g, Rectangle rect, Brush sb)
        //{
        //    using (sb)
        //    {
        //        g.FillRectangle(sb, rect);
        //    }
        //}

        public void DrawRect(Graphics g, Rectangle rect, Color color)
        {
            using (Pen pen = new Pen(color))
            {
                g.DrawRectangle(pen, rect);
            }
        }

        //public void DrawRect(Graphics g, Rectangle rect, Pen pen)
        //{
        //    using (pen)
        //    {
        //        g.DrawRectangle(pen, rect);
        //    }
        //}
        public void DrawPath(Graphics g, GraphicsPath path, Color color)
        {
            using (Pen pen = new Pen(color))
            {
                g.DrawPath(pen, path);
            }
        }

        //public void DrawPath(Graphics g, GraphicsPath path, Pen pen)
        //{
        //    using (pen)
        //    {
        //        g.DrawPath(pen, path);
        //    }
        //}
        #endregion

        #region Drawing McLabel Methods

        //		public void DrawLabel(Graphics g,Rectangle rect,Image image,ContentAlignment ImageAlign,BorderStyle borderStyle)
        //		{
        //			DrawLabel(g,rect,"",ContentAlignment.MiddleCenter,image,ImageAlign,null,borderStyle);
        //		}
        //
        //		public void DrawLabel(Graphics g,Rectangle rect,string Text,ContentAlignment TextAlign,Font font,BorderStyle borderStyle)
        //		{
        //			DrawLabel(g,rect,Text,TextAlign,null,ContentAlignment.MiddleCenter,font,borderStyle);
        //		}

        private void DrawLabel(Graphics g, Rectangle rect, ILabel ctl, ControlLayout ctlLayout)//string Text,ContentAlignment TextAlign,Image image,ContentAlignment ImageAlign,Font font,BorderStyle borderStyle)
        {
            if (ctl.BorderStyle == BorderStyle.None)
            {
                g.Clear(this.BackgroundColorInternal);
            }
            else if (ctlLayout == ControlLayout.Flat || ctlLayout == ControlLayout.Visual)
            {

                if (ctlLayout == ControlLayout.Visual)
                {
                    using (Brush sb = this.GetBrushGradient(rect, 90f))
                    {
                        g.FillRectangle(sb, rect);
                    }
                }
                else
                {
                    using (Brush sb = this.GetBrushFlat())
                    {
                        g.FillRectangle(sb, rect);
                    }
                }
                using (Pen pen = this.GetPenBorder())
                {
                    g.DrawRectangle(pen, rect);
                }
            }
            else if (ctlLayout == ControlLayout.XpLayout || ctlLayout== ControlLayout.VistaLayout )
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                System.Drawing.Drawing2D.GraphicsPath path = Nistec.Drawing.DrawUtils.GetRoundedRect(rect, 3);

                using (Brush sb = this.GetBrushGradient(rect, 90f))
                {
                    g.FillPath(sb, path);
                }

                using (Pen pen = this.GetPenBorder())
                {
                    g.DrawPath(pen, path);
                }
            }

            else
            {
                ControlPaint.DrawButton(g, rect, ButtonState.Normal);
            }
            if (ctl.Text.Length > 0)
                DrawString(g, rect, ctl.TextAlign, ctl.Text, ctl.Font);
            if (ctl.Image != null)
                DrawImage(g, rect, ctl.Image, ctl.ImageAlign, true);


        }

        #endregion

        #region Drawing McPanel Methods

        private void DrawContainer(Graphics g, Rectangle bounds, Control ctl, System.Windows.Forms.BorderStyle borderStyle)
        {
            Rectangle rect = bounds;
            if (borderStyle != BorderStyle.Fixed3D)
            {
                rect = new Rectangle(rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
            }

            ctl.BackColor = this.BackgroundColorInternal;//BackgroundColorInternal;

            if (borderStyle == BorderStyle.FixedSingle)
            {
                using (Pen pen = this.GetPenBorder())
                {
                    g.DrawRectangle(pen, rect);
                }
            }
            else if (borderStyle == BorderStyle.Fixed3D)
                ControlPaint.DrawBorder3D(g, rect, System.Windows.Forms.Border3DStyle.Sunken);
        }


        private void DrawPanel(Graphics g, Rectangle bounds, System.Windows.Forms.BorderStyle borderStyle)
        {
            Rectangle rect = bounds;
            if (borderStyle != BorderStyle.Fixed3D)
            {
                rect = new Rectangle(rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
            }

            //Parent.BackColorInternal =this.BackgroundColorInternal;

            using (Brush sb = this.GetBrushFlat())
            {
                g.FillRectangle(sb, rect);
            }

            if (borderStyle == BorderStyle.FixedSingle)
            {
                using (Pen pen = this.GetPenBorder())
                {
                    g.DrawRectangle(pen, rect);
                }
            }
            else if (borderStyle == BorderStyle.Fixed3D)
                ControlPaint.DrawBorder3D(g, rect, System.Windows.Forms.Border3DStyle.Sunken);

        }

        private void DrawPanelXP(Graphics g, Rectangle bounds, IPanel ctl, ControlLayout ctlLayout, bool revers)
        {
            Rectangle rect = bounds;
            BorderStyle borderStyle = ctl.BorderStyle;
            if (borderStyle != BorderStyle.Fixed3D)
            {
                rect = new Rectangle(rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
            }

            //Parent.BackColorInternal =this.BackgroundColorInternal;

            if (ctlLayout == ControlLayout.Flat || ctlLayout == ControlLayout.Visual)
            {
                if (ctlLayout == ControlLayout.Visual)// && !this.IsFlatInternal )
                {
                    using (Brush sb = this.GetBrushGradient(rect, 90f, revers))
                    {
                        g.FillRectangle(sb, rect);
                    }
                }
                else
                {
                    using (Brush sb = this.GetBrushFlat())
                    {
                        g.FillRectangle(sb, rect);
                    }
                }

                if (borderStyle == BorderStyle.FixedSingle)
                {
                    using (Pen pen = this.GetPenBorder())
                    {
                        g.DrawRectangle(pen, rect);
                    }
                }
                else if (borderStyle == BorderStyle.Fixed3D)
                    ControlPaint.DrawBorder3D(g, rect, System.Windows.Forms.Border3DStyle.Sunken);
            }

            else if (ctlLayout == ControlLayout.XpLayout || ctlLayout== ControlLayout.VistaLayout)
            {
                using (SolidBrush sb = new SolidBrush(ctl.BackColor))
                {
                    g.FillRectangle(sb, bounds);
                }

                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                System.Drawing.Drawing2D.GraphicsPath path = Nistec.Drawing.DrawUtils.GetRoundedRect(rect, 4);

                using (Brush sb = this.GetBrushGradient(rect, 90f, revers))
                {
                    g.FillPath(sb, path);
                }

                if (borderStyle == BorderStyle.FixedSingle)
                {
                    using (Pen pen = this.GetPenBorder())
                    {
                        g.DrawPath(pen, path);
                    }
                }
                else if (borderStyle == BorderStyle.Fixed3D)
                    ControlPaint.DrawBorder3D(g, rect, System.Windows.Forms.Border3DStyle.Sunken);
            }
        }


        #endregion

        #region Drawing McButton Methods

        public void DrawButtonBorder(Graphics g, Rectangle rect, IButton ctl, ControlLayout ctlLayout)
        {
            ButtonStates state = ctl.ButtonState;

            Pen pen;

            if (!ctl.Enabled)
            {
                pen = this.GetPenBorder();
            }
            else if (state == ButtonStates.Pushed)//Clicked)
            {
                pen = this.GetPenFocused();
            }
            else if (state == ButtonStates.MouseOver)//hot)
            {
                pen = this.GetPenHot();
            }
            else
            {
                pen = this.GetPenBorder();
            }

            if (ctlLayout == ControlLayout.XpLayout || ctlLayout == ControlLayout.Visual || ctlLayout== ControlLayout.VistaLayout)
            {
                g.CompositingQuality = CompositingQuality.GammaCorrected;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                //Highlight path 
                System.Drawing.Drawing2D.GraphicsPath path = Nistec.Drawing.DrawUtils.GetRoundedRect(rect, 2);
                g.DrawPath(pen, path);
            }
            else
            {
                g.DrawRectangle(pen, rect);
            }
            pen.Dispose();
        }

        public void DrawButtonRect(Graphics g, Rectangle rect, IButton ctl, ControlLayout ctlLayout)
        {
            ButtonStates state = ctl.ButtonState;

            //if (ctlLayout == ControlLayout.System)
            //{
            //    Rectangle sysRect = new Rectangle(rect.X, rect.Y, rect.Width + 1, rect.Height + 1);
            //    if (!ctl.Enabled)
            //        ControlPaint.DrawButton(g, sysRect, ButtonState.Inactive);
            //    else if (state == ButtonStates.Pushed)//Clicked)
            //        ControlPaint.DrawButton(g, sysRect, ButtonState.Pushed);
            //    else if (state == ButtonStates.MouseOver)//hot)
            //        ControlPaint.DrawButton(g, sysRect, ButtonState.Flat);
            //    else
            //        ControlPaint.DrawButton(g, sysRect, ButtonState.Normal);
            //    return;
            //}

            Brush sb;
            Pen pen;

            if (!ctl.Enabled)
            {
                sb = this.GetBrushDisabled();
                pen = this.GetPenBorder();
            }
            else if (state == ButtonStates.Pushed)//Clicked)
            {
                sb = this.GetBrushClick();
                pen = this.GetPenFocused();
            }
            else if (state == ButtonStates.MouseOver)//hot)
            {
                sb = new SolidBrush(this.ContentColorInternal);//LightLightColor);
                pen = this.GetPenHot();
            }
            else //if (ctlLayout == ControlLayout.XpLayout || ctlLayout == ControlLayout.Visual)
            {
                sb = this.GetBrushGradient(rect, 270f);
                pen = this.GetPenBorder();
            }
   
            //			else
            //			{
            //				sb=this.GetBrushFlat();
            //				pen =this.GetPenBorder();
            //			}


            if (ctlLayout == ControlLayout.XpLayout || ctlLayout== ControlLayout.VistaLayout)
            {
                Rectangle xpRect = rect;
                xpRect.Inflate(-1, -1);
                g.CompositingQuality = CompositingQuality.GammaCorrected;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                //Highlight path 
                System.Drawing.Drawing2D.GraphicsPath path = Nistec.Drawing.DrawUtils.GetRoundedRect(xpRect, 2);
                pen = this.GetPenFocused();

                //g.FillRectangle(new SolidBrush(Color.Transparent), rect);
                g.FillPath(sb, path);
                g.DrawPath(pen, path);
            }
            else
            {
                g.FillRectangle(sb, rect);
                g.DrawRectangle(pen, rect);
            }
            sb.Dispose();
            pen.Dispose();
        }

        //public void DrawButton(Graphics g, Rectangle rect, IButton ctl, ControlLayout ctlLayout, float radius, bool isDefault)
        //{

        //    //DrawButtonVista(g, rect, ctl, ctlLayout, radius, isDefault);
        //    //return;

        //    const int innerOffset = 2;
        //    ButtonStates state = ctl.ButtonState;
        //    //Inner Rect
        //    Rectangle innerRect = new Rectangle(rect.X + innerOffset, rect.Y + innerOffset, rect.Width - (innerOffset * 2), rect.Height - (innerOffset * 2));

        //    if (ctlLayout == ControlLayout.System)
        //    {
        //        Rectangle sysRect = new Rectangle(rect.X, rect.Y, rect.Width + 1, rect.Height + 1);
        //        if (!ctl.Enabled)
        //            ControlPaint.DrawButton(g, sysRect, ButtonState.Inactive);
        //        else if (state == ButtonStates.Pushed)//Clicked)
        //            ControlPaint.DrawButton(g, sysRect, ButtonState.Pushed);
        //        else if (state == ButtonStates.MouseOver)//hot)
        //            ControlPaint.DrawButton(g, sysRect, ButtonState.Flat);
        //        else
        //            ControlPaint.DrawButton(g, sysRect, ButtonState.Normal);

        //        if (ctl.Focused)
        //            ControlPaint.DrawFocusRectangle(g, new Rectangle(3, 3, rect.Width - 6, rect.Height - 6));
        //        if (isDefault)
        //            ControlPaint.DrawBorder(g, new Rectangle(3, 3, rect.Width - 6, rect.Height - 6), this.ColorBrush1Internal, ButtonBorderStyle.Dotted);

        //        goto Label_01;
        //    }
        //    else if (ctlLayout == ControlLayout.XpLayout)
        //    {


        //        DrawButtonInternal(g, rect, ctl, isDefault);

        //        Pen pn;

        //        //if (!ctl.Enabled)
        //        //    pn = this.GetPenBorder();
        //        //else if (state == ButtonStates.Pushed)//Clicked)
        //        //    pn = this.GetPenFocused();
        //        //else if (state == ButtonStates.MouseOver)//hot)
        //        //    pn = this.GetPenHot();
        //        //else
        //        pn = this.GetPenButton();//.GetPenBorder();
        //        Rectangle xpRect = rect;
        //        xpRect.Inflate(-1, -1);
        //        g.CompositingQuality = CompositingQuality.GammaCorrected;
        //        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        //        //Highlight path 
        //        System.Drawing.Drawing2D.GraphicsPath path = Nistec.Drawing.DrawUtils.GetRoundedRect(xpRect, 2);
        //        g.DrawPath(pn, path);
        //        pn.Dispose();

        //        goto Label_01;
        //    }


        //    Brush sb;
        //    Pen pen;
        //    Brush sbH;

        //    if (!ctl.Enabled)
        //    {
        //        sbH = GetBrushDisabled();
        //        sb = this.GetBrushContent();//.GetBrushDisabled();
        //        pen = this.GetPenBorder();
        //    }
        //    else if (state == ButtonStates.MouseOver)//hot)
        //    {
        //        sbH = new SolidBrush(this.HighlightColorInternal);// this.GetBrushGradient(rect,Color.Gold,Color.OrangeRed,270f);
        //        sb = this.GetBrushGradient(rect, 270f);
        //        pen = this.GetPenButton();
        //    }
        //    else if (state == ButtonStates.Pushed)//|| ctl.Focused)//Clicked)
        //    {
        //        sbH = GetBrushSelected();// new SolidBrush(this.FocusedColorInternal);// Color.CornflowerBlue);// this.GetBrushGradient(rect,Color.Gold,Color.OrangeRed,270f);
        //        sb = this.GetBrushClick();
        //        pen = this.GetPenButton();
        //    }
        //    else if (ctl.Focused)
        //    {
        //        sbH = new SolidBrush(this.FocusedColorInternal);// Color.CornflowerBlue);// this.GetBrushGradient(rect,Color.Gold,Color.OrangeRed,270f);
        //        sb = this.GetBrushGradient(rect, 270f);
        //        pen = this.GetPenButton();
        //    }
        //    else //if(ctlLayout==ControlLayout.Visual || ctlLayout==ControlLayout.XpLayout)// && !IsFlatInternal )
        //    {
        //        if (isDefault)
        //            sbH = new SolidBrush(this.FocusedColorInternal);// Color.CornflowerBlue);// this.GetBrushGradient(rect,Color.Gold,Color.OrangeRed,270f);
        //        else
        //            sbH = this.GetBrushGradient(rect, this.BorderColorInternal, this.ColorBrush1Internal, 270f);


        //        sb = this.GetBrushGradient(rect, 270f);
        //        pen = this.GetPenButton();

        //        //if (isDefault)
        //        //    pen = this.GetPenFocused();
        //        //else
        //        //    pen = this.GetPenBorder();
        //    }

        //    if (ctlLayout == ControlLayout.Visual)
        //    {

        //        g.CompositingQuality = CompositingQuality.GammaCorrected;
        //        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        //        //Highlight path 
        //        System.Drawing.Drawing2D.GraphicsPath path = Nistec.Drawing.DrawUtils.GetRoundedRect(rect, radius);
        //        //Inner path
        //        //System.Drawing.Drawing2D.GraphicsPath innerPath = Nistec.Drawing.DrawUtils.GetRoundedRect(innerRect,m_Radius);



        //        //if (state == ButtonStates.MouseOver)//hot)
        //        //    sbH = new SolidBrush(this.HighlightColorInternal);// this.GetBrushGradient(rect,Color.Gold,Color.OrangeRed,270f);
        //        //else if (state == ButtonStates.Pushed)
        //        //    sbH = GetBrushSelected();// new SolidBrush(this.FocusedColorInternal);// Color.CornflowerBlue);// this.GetBrushGradient(rect,Color.Gold,Color.OrangeRed,270f);
        //        //else if (ctl.Focused && state != ButtonStates.Pushed)
        //        //    sbH =GetBrushSelected();// new SolidBrush(this.FocusedColorInternal);// Color.CornflowerBlue);// this.GetBrushGradient(rect,Color.Gold,Color.OrangeRed,270f);
        //        //else
        //        //    sbH = this.GetBrushGradient(rect, this.BorderColorInternal, this.ColorBrush1Internal, 270f);

        //        //Draw Highlight
        //        g.FillPath(sbH, path);
        //        //Draw inner rect 
        //        g.FillRectangle(sb, innerRect);
        //        //sbH.Dispose();
        //        g.DrawPath(pen, path);

        //    }

        //    else
        //    {
        //        g.FillRectangle(sbH, rect);
        //        g.FillRectangle(sb, innerRect);
        //        g.DrawRectangle(pen, rect);
        //    }
        //    sbH.Dispose();
        //    sb.Dispose();
        //    pen.Dispose();

        //    //Label_01:
        ////if(ctl.Focused)
        ////	ControlPaint.DrawFocusRectangle(g, new Rectangle(3, 3, rect.Width-6, rect.Height-6));       
        ////if (isDefault) 
        ////    ControlPaint.DrawBorder(g, new Rectangle(3, 3, rect.Width-6, rect.Height-6), this.ColorBrush1Internal,ButtonBorderStyle.Dotted);

        //    Label_01:

        //    if (ctl.Text.Length > 0)
        //    {
        //        DrawString(g, innerRect, ctl.TextAlign, ctl.Text, ctl.Font, ctl.RightToLeft, ctl.Enabled);
        //        //DrawString(g, innerRect, ctl.TextAlign, ctl.Text, ctl.Font, (state == ButtonStates.Pushed), ctl.Enabled);
        //    }
        //    if (ctl.Image != null)
        //        DrawButtonImage(g, innerRect, ctl.Image, ctl.ImageAlign, ctl.Enabled, 4, 2);
        //    else if (ctl.ImageList != null && ctl.ImageIndex >= 0)
        //        DrawButtonImage(g, innerRect, ctl.ImageList.Images[ctl.ImageIndex], ctl.ImageAlign, ctl.Enabled, 4, 2);
        //}

        public void DrawButton(Graphics g, Rectangle rect, ButtonStates state, bool isDefault, bool Enabled)
        {
            const int innerOffset = 2;

            //Inner Rect
            Rectangle innerRect = new Rectangle(rect.X + innerOffset, rect.Y + innerOffset, rect.Width - (innerOffset * 2), rect.Height - (innerOffset * 2));

            Brush sb;
            Pen pen;
            Brush sbH;

            if (!Enabled)
            {
                sbH = GetBrushDisabled();
                sb = this.GetBrushContent();//.GetBrushDisabled();
                pen = this.GetPenBorder();
            }
            else if (state == ButtonStates.MouseOver)//hot)
            {
                sbH = new SolidBrush(this.HighlightColorInternal);
                sb = this.GetBrushGradient(rect, 270f);
                pen = this.GetPenButton();
            }
            else if (state == ButtonStates.Pushed)
            {
                sbH = GetBrushSelected();
                sb = this.GetBrushClick();
                pen = this.GetPenButton();
            }
            else if (state == ButtonStates.Focused)
            {
                sbH = new SolidBrush(this.FocusedColorInternal);
                sb = this.GetBrushGradient(rect, 270f);
                pen = this.GetPenButton();
            }
            else
            {
                if (isDefault)
                    sbH = new SolidBrush(this.FocusedColorInternal);
                else
                    sbH = this.GetBrushGradient(rect, this.BorderColorInternal, this.ColorBrush1Internal, 270f);


                sb = this.GetBrushGradient(rect, 270f);
                pen = this.GetPenButton();

            }

            g.CompositingQuality = CompositingQuality.GammaCorrected;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //Highlight path 
            System.Drawing.Drawing2D.GraphicsPath path = Nistec.Drawing.DrawUtils.GetRoundedRect(rect, 3);


            //Draw Highlight
            g.FillPath(sbH, path);
            //Draw inner rect 
            g.FillRectangle(sb, innerRect);
            //sbH.Dispose();
            g.DrawPath(pen, path);

            path.Dispose();
            sbH.Dispose();
            sb.Dispose();
            pen.Dispose();

        }

        public void DrawButton(Graphics g, Rectangle rect, ButtonStates state, bool isDefault, string Text, ContentAlignment TextAlign, Image image, ContentAlignment ImageAlign, RightToLeft rtl, bool Enabled)
        {
            
            DrawButton(g, rect, state, isDefault, Enabled);
            const int innerOffset = 2;
            Rectangle innerRect = new Rectangle(rect.X + innerOffset, rect.Y + innerOffset, rect.Width - (innerOffset * 2), rect.Height - (innerOffset * 2));
            if (Text.Length > 0)
            {
                DrawString(g, innerRect, TextAlign, Text, StyleLayout.DefaultFont, rtl, Enabled);
            }
            if (image != null)
            {
                DrawButtonImage(g, innerRect, image, ImageAlign, Enabled, 4, 2);
            }
        }


        public void DrawButton(Graphics g, Rectangle rect, IButton ctl, ControlLayout ctlLayout, float radius, bool isDefault)
        {
            const int innerOffset = 2;
            Rectangle innerRect = new Rectangle(rect.X + innerOffset, rect.Y + innerOffset, rect.Width - (innerOffset * 2), rect.Height - (innerOffset * 2));

            switch (ctlLayout)
            {
                case ControlLayout.System:
                    DrawButtonSystem(g, rect, ctl, radius, isDefault);
                    break;
                case ControlLayout.Flat:
                    DrawButtonVisual(g, rect, ctl, radius, isDefault,true);
                    break;
                case ControlLayout.Visual:
                    DrawButtonVisual(g, rect, ctl, radius, isDefault,false);
                    break;
                case ControlLayout.XpLayout:
                    DrawButtonXP(g, rect, ctl, radius, isDefault);
                    break;
                case ControlLayout.VistaLayout:
                    DrawButtonVista(g, rect, ctl, radius, isDefault);
                    break;
            }
            if (ctl.Text.Length > 0)
            {
                DrawString(g, innerRect, ctl.TextAlign, ctl.Text, ctl.Font, ctl.RightToLeft, ctl.Enabled);
                //DrawString(g, innerRect, ctl.TextAlign, ctl.Text, ctl.Font, (state == ButtonStates.Pushed), ctl.Enabled);
            }
            if (ctl.Image != null)
                DrawButtonImage(g, innerRect, ctl.Image, ctl.ImageAlign, ctl.Enabled, 4, 2);
            else if (ctl.ImageList != null && ctl.ImageIndex >= 0)
                DrawButtonImage(g, innerRect, ctl.ImageList.Images[ctl.ImageIndex], ctl.ImageAlign, ctl.Enabled, 4, 2);
        }

        #endregion


        #region DrawButton Features

        public void DrawButtonSystem(Graphics g, Rectangle rect, IButton ctl, float radius, bool isDefault)
        {
            const int innerOffset = 2;
            ButtonStates state = ctl.ButtonState;
            //Inner Rect
            Rectangle innerRect = new Rectangle(rect.X + innerOffset, rect.Y + innerOffset, rect.Width - (innerOffset * 2), rect.Height - (innerOffset * 2));

            Rectangle sysRect = new Rectangle(rect.X, rect.Y, rect.Width + 1, rect.Height + 1);
            if (!ctl.Enabled)
                ControlPaint.DrawButton(g, sysRect, ButtonState.Inactive);
            else if (state == ButtonStates.Pushed)//Clicked)
                ControlPaint.DrawButton(g, sysRect, ButtonState.Pushed);
            else if (state == ButtonStates.MouseOver)//hot)
                ControlPaint.DrawButton(g, sysRect, ButtonState.Flat);
            else
                ControlPaint.DrawButton(g, sysRect, ButtonState.Normal);

            if (ctl.Focused)
                ControlPaint.DrawFocusRectangle(g, new Rectangle(3, 3, rect.Width - 6, rect.Height - 6));
            if (isDefault)
                ControlPaint.DrawBorder(g, new Rectangle(3, 3, rect.Width - 6, rect.Height - 6), this.ColorBrush1Internal, ButtonBorderStyle.Dotted);

        }

        public void DrawButtonVisual(Graphics g, Rectangle rect, IButton ctl, float radius, bool isDefault,bool IsFlat)
        {
            const int innerOffset = 2;
            ButtonStates state = ctl.ButtonState;
            //Inner Rect
            Rectangle innerRect = new Rectangle(rect.X + innerOffset, rect.Y + innerOffset, rect.Width - (innerOffset * 2), rect.Height - (innerOffset * 2));

            Brush sb;
            Pen pen;
            Brush sbH;

            if (!ctl.Enabled)
            {
                sbH = GetBrushDisabled();
                sb = this.GetBrushContent();//.GetBrushDisabled();
                pen = this.GetPenBorder();
            }
            else if (state == ButtonStates.MouseOver)//hot)
            {
                sbH = new SolidBrush(this.HighlightColorInternal);// this.GetBrushGradient(rect,Color.Gold,Color.OrangeRed,270f);
                sb = this.GetBrushGradient(rect, 270f);
                pen = this.GetPenButton();
            }
            else if (state == ButtonStates.Pushed)//|| ctl.Focused)//Clicked)
            {
                sbH = GetBrushSelected();// new SolidBrush(this.FocusedColorInternal);// Color.CornflowerBlue);// this.GetBrushGradient(rect,Color.Gold,Color.OrangeRed,270f);
                sb = this.GetBrushClick();
                pen = this.GetPenButton();
            }
            else if (ctl.Focused)
            {
                sbH = new SolidBrush(this.FocusedColorInternal);// Color.CornflowerBlue);// this.GetBrushGradient(rect,Color.Gold,Color.OrangeRed,270f);
                sb = this.GetBrushGradient(rect, 270f);
                pen = this.GetPenButton();
            }
            else
            {
                if (isDefault)
                    sbH = new SolidBrush(this.FocusedColorInternal);// Color.CornflowerBlue);// this.GetBrushGradient(rect,Color.Gold,Color.OrangeRed,270f);
                else
                    sbH = this.GetBrushGradient(rect, this.BorderColorInternal, this.ColorBrush1Internal, 270f);


                sb = this.GetBrushGradient(rect, 270f);
                pen = this.GetPenButton();

            }

            if (!IsFlat)
            {

                g.CompositingQuality = CompositingQuality.GammaCorrected;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                //Highlight path 
                System.Drawing.Drawing2D.GraphicsPath path = Nistec.Drawing.DrawUtils.GetRoundedRect(rect, radius);
                //Inner path
                //System.Drawing.Drawing2D.GraphicsPath innerPath = Nistec.Drawing.DrawUtils.GetRoundedRect(innerRect,m_Radius);

                //Draw Highlight
                g.FillPath(sbH, path);
                //Draw inner rect 
                g.FillRectangle(sb, innerRect);
                //sbH.Dispose();
                g.DrawPath(pen, path);

            }

            else
            {
                g.FillRectangle(sbH, rect);
                g.FillRectangle(sb, innerRect);
                g.DrawRectangle(pen, rect);
            }

            sbH.Dispose();
            sb.Dispose();
            pen.Dispose();

        }


        public void DrawButtonVista(Graphics g, Rectangle bounds, IButton ctl, float radius, bool isDefault)
        {
            const int innerOffset = 2;
            const int alpha = 60;

            ButtonStates state = ctl.ButtonState;
            Rectangle rect = new Rectangle(bounds.X + 1, bounds.Y + 1, bounds.Width - 2, bounds.Height - 2);
            Rectangle innerRect = new Rectangle(rect.X + innerOffset, rect.Y + innerOffset, rect.Width - (innerOffset * 2), rect.Height - (innerOffset * 2));
            Rectangle upperRect = new Rectangle(innerRect.X, innerRect.Y, innerRect.Width, 2 * (innerRect.Height / 3));
            Rectangle lowerRect = new Rectangle(innerRect.X, 3 + (innerRect.Height / 2), innerRect.Width, 2 + (innerRect.Height / 2));

            GraphicsPath boundPath = DrawUtils.GetRoundedRect(bounds, radius);
            GraphicsPath path = DrawUtils.GetRoundedRect(rect, radius);
            //GraphicsPath innerPath = DrawUtils.GetRoundedRect(innerRect, radius);
            GraphicsPath upperPath = DrawUtils.GettRoundedTopRect(upperRect, radius);
            GraphicsPath lowerPath = DrawUtils.GettRoundedBottomRect(lowerRect, radius);


            Pen pen;
            Brush sbBound;
            Brush sbBack;
            //Brush sbInner;
            Brush sbUpper;
            Brush sbLower;

            sbBound = GetBrushFlat(FlatLayout.Dark);
            //sbInner = this.GetBrushGradient(upperRect, ColorBrushLowerInternal, ColorBrushUpperInternal, 270f);

            pen = this.GetPenButton();

            if (!ctl.Enabled)
            {
                sbBack = GetBrushFlat(FlatLayout.Dark);
            }
            if (state == ButtonStates.MouseOver)//hot)
            {
                sbBack = GetBrushFlat(FlatLayout.Light);
            }
            else if (state == ButtonStates.Pushed || ctl.Focused)//Clicked)
            {
                //sbBack = new SolidBrush(this.FocusedColorInternal);
                sbBack = new SolidBrush(this.CalcColor(ColorBrushUpperInternal, CaptionColorInternal, 160));
            }
            else
            {
                if (isDefault)
                    sbBack = new SolidBrush(this.CalcColor(ColorBrushUpperInternal, CaptionColorInternal, 160));
                else
                    sbBack = GetBrushFlat(FlatLayout.Dark);

            }

            //sbBack = new SolidBrush(this.CalcColor(ColorBrushLowerInternal, CaptionColorInternal, 200));

            sbUpper = this.GetBrushGradient(upperRect, this.CalcColor(ColorBrushUpperInternal, ColorBrushLowerInternal, 160), ColorBrushUpperInternal, 270f);
            sbLower = this.GetBrushGradient(lowerRect, this.ColorBrushLowerInternal, this.CalcColor(ColorBrushUpperInternal, ColorBrushLowerInternal, alpha), 90f);
            //sbMiddel = this.GetBrushGradient(middelRect ,this.ColorBrushLowerInternal, this.CalcColor(ColorBrushLowerInternal, ColorBrushUpperInternal, 50), 270f);


            g.CompositingQuality = CompositingQuality.GammaCorrected;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            g.FillPath(sbBound, boundPath);
            g.FillPath(sbBack, path);
            //g.FillPath(sbBack, boundPath);
            //g.FillPath(sbInner, innerPath);
            g.FillPath(sbUpper, upperPath);
            g.FillPath(sbLower, lowerPath);
            g.DrawPath(pen, path);

            sbBound.Dispose();
            sbBack.Dispose();
            //sbInner.Dispose();
            sbUpper.Dispose();
            sbLower.Dispose();
            pen.Dispose();

        }

         public void DrawButtonXP(Graphics g, Rectangle rect, IButton ctl, float radius, bool isDefault)
        {
            DrawButtonInternal(g, rect, ctl, isDefault);

            const int innerOffset = 2;
            ButtonStates state = ctl.ButtonState;
            //Inner Rect
            Rectangle innerRect = new Rectangle(rect.X + innerOffset, rect.Y + innerOffset, rect.Width - (innerOffset * 2), rect.Height - (innerOffset * 2));

            Pen pn;

            pn = this.GetPenButton();
            Rectangle xpRect = rect;
            xpRect.Inflate(-1, -1);
            g.CompositingQuality = CompositingQuality.GammaCorrected;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //Highlight path 
            System.Drawing.Drawing2D.GraphicsPath path = Nistec.Drawing.DrawUtils.GetRoundedRect(xpRect, 2);
            g.DrawPath(pn, path);
            pn.Dispose();


            //if (ctl.Text.Length > 0)
            //    DrawString(g, innerRect, ctl.TextAlign, ctl.Text, ctl.Font, (state == ButtonStates.Pushed), ctl.Enabled);
            //if (ctl.Image != null)
            //    DrawButtonImage(g, innerRect, ctl.Image, ctl.ImageAlign, ctl.Enabled, 4, 2);
            //else if (ctl.ImageList != null && ctl.ImageIndex >= 0)
            //    DrawButtonImage(g, innerRect, ctl.ImageList.Images[ctl.ImageIndex], ctl.ImageAlign, ctl.Enabled, 4, 2);
        }

        internal void DrawButtonInternal(Graphics g, Rectangle rect, IButton ctl, bool isDefault)
        {
            Styles style = StylePlan;

            Rectangle[] rects0;
            Rectangle[] rects1;

            int X = ctl.Width;
            int Y = ctl.Height;

            rects0 = new Rectangle[2];
            rects0[0] = new Rectangle(2, 4, 2, Y - 8);
            rects0[1] = new Rectangle(X - 4, 4, 2, Y - 8);

            rects1 = new Rectangle[8];
            rects1[0] = new Rectangle(2, 1, 2, 2);
            rects1[1] = new Rectangle(1, 2, 2, 2);
            rects1[2] = new Rectangle(X - 4, 1, 2, 2);
            rects1[3] = new Rectangle(X - 3, 2, 2, 2);
            rects1[4] = new Rectangle(2, Y - 3, 2, 2);
            rects1[5] = new Rectangle(1, Y - 4, 2, 2);
            rects1[6] = new Rectangle(X - 4, Y - 3, 2, 2);
            rects1[7] = new Rectangle(X - 3, Y - 4, 2, 2);

            Point[] points = {
								 new Point(1, 0),
								 new Point(X-1, 0),
								 new Point(X-1, 1),
								 new Point(X, 1),
								 new Point(X, Y-1),
								 new Point(X-1, Y-1),
								 new Point(X-1, Y),
								 new Point(1, Y),
								 new Point(1, Y-1),
								 new Point(0, Y-1),
								 new Point(0, 1),
								 new Point(1, 1)};

            GraphicsPath path = new GraphicsPath();
            path.AddLines(points);

            //ctl.Region = new Region(path);

            LinearGradientBrush
                brush00, brush01,
                brush02, brush03, brush05, brush07, brush09;
            SolidBrush
                      brush06, brush08, _brush01, _brush02;//,brush04
            Pen
                pen01, pen02,
                pen03, pen04,
                pen05, pen06,
                pen07, pen08,
                pen09, pen10,
                pen11, pen12, pen13,
                //pen14, pen15, pen16,
                pen17, pen18, pen19, pen20, pen21, pen22, pen23, pen24, _pen01, _pen02;


            //CreatePensBrushes(style);
            //if (ctl.Region == null) return;


            brush00 = new LinearGradientBrush(new Rectangle(0, 0, X, Y), Color.FromArgb(64, 171, 168, 137), Color.FromArgb(92, 255, 255, 255), 85.0f);

            LinearGradientBrush _brush;

            if (style == Styles.System)//.SeaGreen)
            {
                brush01 = new LinearGradientBrush(new Rectangle(2, 2, X - 5, Y - 7), Color.FromArgb(255, 255, 246), Color.FromArgb(246, 243, 224), 90.0f);
                brush05 = new LinearGradientBrush(new Rectangle(2, 2, X - 5, Y - 7), Color.FromArgb(238, 230, 210), Color.FromArgb(236, 228, 206), 90.0f);
                _brush = new LinearGradientBrush(new Rectangle(2, 3, X - 4, Y - 7), Color.FromArgb(228, 212, 191), Color.FromArgb(229, 217, 195), 90.0f);
                pen17 = new Pen(_brush);
                _brush = new LinearGradientBrush(new Rectangle(3, 3, X - 4, Y - 7), Color.FromArgb(232, 219, 197), Color.FromArgb(234, 224, 201), 90.0f);
                pen18 = new Pen(_brush);
                pen19 = new Pen(Color.FromArgb(223, 205, 180));
                pen20 = new Pen(Color.FromArgb(231, 217, 195));
                pen21 = new Pen(Color.FromArgb(242, 236, 216));
                pen22 = new Pen(Color.FromArgb(248, 244, 228));
                brush06 = new SolidBrush(Color.FromArgb(255, 255, 255));
                brush07 = ButtonXpBrush07(new Rectangle(3, 3, X - 6, Y - 7));
                brush08 = new SolidBrush(Color.FromArgb(198, 197, 215));
                brush02 = new LinearGradientBrush(new Rectangle(2, 3, X - 4, Y - 7), Color.FromArgb(177, 203, 128), Color.FromArgb(144, 193, 84), 90.0f);
                brush03 = new LinearGradientBrush(new Rectangle(2, 3, X - 4, Y - 7), Color.FromArgb(237, 190, 150), Color.FromArgb(227, 145, 79), 90.0f);
                pen06 = new Pen(Color.FromArgb(194, 209, 143));
                pen07 = new Pen(Color.FromArgb(177, 203, 128));
                pen08 = new Pen(Color.FromArgb(144, 193, 84));
                pen09 = new Pen(Color.FromArgb(168, 167, 102));
                pen10 = new Pen(Color.FromArgb(252, 197, 149));
                pen11 = new Pen(Color.FromArgb(237, 190, 150));
                pen12 = new Pen(Color.FromArgb(227, 145, 79));
                pen13 = new Pen(Color.FromArgb(207, 114, 37));
                //pen14 = new Pen(Color.FromArgb(55, 98, 6));
                //pen15 = new Pen(Color.FromArgb(109, 138, 77));
                //pen16 = new Pen(Color.FromArgb(192, 109, 138, 77));
                brush09 = ButtonXpBrush09(new Rectangle(3, 3, X - 5, Y - 8));
                pen23 = new Pen(Color.FromArgb(255, 255, 255));
                pen24 = new Pen(Color.FromArgb(172, 171, 189));
                pen01 = new Pen(Color.FromArgb(243, 238, 219));
                pen02 = new Pen(Color.FromArgb(236, 225, 201));
                pen03 = new Pen(Color.FromArgb(227, 209, 184));
                _brush = new LinearGradientBrush(new Rectangle(X - 3, 4, 1, Y - 5), Color.FromArgb(251, 247, 232), Color.FromArgb(64, 216, 181, 144), 90.0f);
                pen04 = new Pen(_brush);
                _brush = new LinearGradientBrush(new Rectangle(X - 2, 4, 1, Y - 5), Color.FromArgb(246, 241, 224), Color.FromArgb(64, 194, 156, 120), 90.0f);
                pen05 = new Pen(_brush);
                _brush01 = new SolidBrush(Color.FromArgb(64, 202, 196, 184));
                _brush02 = new SolidBrush(Color.FromArgb(246, 242, 233));
                _pen01 = new Pen(Color.FromArgb(202, 196, 184));
                _pen02 = new Pen(Color.FromArgb(170, 202, 196, 184));
            }
            else
            {
                brush01 = new LinearGradientBrush(new Rectangle(2, 2, X - 5, Y - 7), Color.FromArgb(255, 255, 255), Color.FromArgb(240, 240, 234), 90.0f);
                brush05 = new LinearGradientBrush(new Rectangle(2, 2, X - 5, Y - 7), Color.FromArgb(229, 228, 221), Color.FromArgb(226, 226, 218), 90.0f);
                _brush = new LinearGradientBrush(new Rectangle(2, 3, X - 4, Y - 7), Color.FromArgb(216, 212, 203), Color.FromArgb(218, 216, 207), 90.0f);
                pen17 = new Pen(_brush);
                _brush = new LinearGradientBrush(new Rectangle(3, 3, X - 4, Y - 7), Color.FromArgb(221, 218, 209), Color.FromArgb(223, 222, 214), 90.0f);
                pen18 = new Pen(_brush);
                pen19 = new Pen(Color.FromArgb(209, 204, 192));
                pen20 = new Pen(Color.FromArgb(220, 216, 207));
                pen21 = new Pen(Color.FromArgb(234, 233, 227));
                pen22 = new Pen(Color.FromArgb(242, 241, 238));
                brush06 = new SolidBrush(Color.FromArgb(255, 255, 255));
                brush07 = ButtonXpBrush07(new Rectangle(3, 3, X - 6, Y - 7));
                brush08 = new SolidBrush(Color.FromArgb(198, 197, 215));
                brush02 = new LinearGradientBrush(new Rectangle(2, 3, X - 4, Y - 7), Color.FromArgb(186, 211, 245), Color.FromArgb(137, 173, 228), 90.0f);
                brush03 = new LinearGradientBrush(new Rectangle(2, 3, X - 4, Y - 7), Color.FromArgb(253, 216, 137), Color.FromArgb(248, 178, 48), 90.0f);
                pen06 = new Pen(Color.FromArgb(206, 231, 255));
                pen07 = new Pen(Color.FromArgb(188, 212, 246));
                pen08 = new Pen(Color.FromArgb(137, 173, 228));
                pen09 = new Pen(Color.FromArgb(105, 130, 238));
                pen10 = new Pen(Color.FromArgb(255, 240, 207));
                pen11 = new Pen(Color.FromArgb(253, 216, 137));
                pen12 = new Pen(Color.FromArgb(248, 178, 48));
                pen13 = new Pen(Color.FromArgb(229, 151, 0));
                //pen14 = new Pen(Color.FromArgb(0, 60, 116));
                //pen15 = new Pen(Color.FromArgb(85, 125, 162));
                //pen16 = new Pen(Color.FromArgb(192, 85, 125, 162));
                brush09 = ButtonXpBrush09(new Rectangle(3, 3, X - 5, Y - 8));
                pen23 = new Pen(Color.FromArgb(255, 255, 255));
                pen24 = new Pen(Color.FromArgb(172, 171, 189));
                pen01 = new Pen(Color.FromArgb(236, 235, 230));
                pen02 = new Pen(Color.FromArgb(226, 223, 214));
                pen03 = new Pen(Color.FromArgb(214, 208, 197));
                _brush = new LinearGradientBrush(new Rectangle(X - 3, 4, 1, Y - 5), Color.FromArgb(245, 244, 242), Color.FromArgb(64, 186, 174, 160), 90.0f);
                pen04 = new Pen(_brush);
                _brush = new LinearGradientBrush(new Rectangle(X - 2, 4, 1, Y - 5), Color.FromArgb(240, 238, 234), Color.FromArgb(64, 175, 168, 142), 90.0f);
                pen05 = new Pen(_brush);
                if (style == Styles.Silver)
                {
                    _brush01 = new SolidBrush(Color.FromArgb(64, 196, 195, 191));
                    _brush02 = new SolidBrush(Color.FromArgb(241, 241, 237));
                    _pen01 = new Pen(Color.FromArgb(196, 195, 191));
                    _pen02 = new Pen(Color.FromArgb(170, 196, 195, 191));

                }
                else
                {
                    _brush01 = new SolidBrush(Color.FromArgb(64, 201, 199, 186));
                    _brush02 = new SolidBrush(Color.FromArgb(245, 244, 234));
                    _pen01 = new Pen(Color.FromArgb(201, 199, 186));
                    _pen02 = new Pen(Color.FromArgb(170, 201, 199, 186));
                }
            }
            _brush.Dispose();

            g.CompositingQuality = CompositingQuality.GammaCorrected;

            if (!ctl.Enabled)
            {
                g.FillRectangle(_brush02, 2, 2, X - 4, Y - 4);
                g.DrawLine(_pen01, 3, 1, X - 4, 1);
                g.DrawLine(_pen01, 3, Y - 2, X - 4, Y - 2);
                g.DrawLine(_pen01, 1, 3, 1, Y - 4);
                g.DrawLine(_pen01, X - 2, 3, X - 2, Y - 4);

                g.DrawLine(_pen02, 1, 2, 2, 1);
                g.DrawLine(_pen02, 1, Y - 3, 2, Y - 2);
                g.DrawLine(_pen02, X - 2, 2, X - 3, 1);
                g.DrawLine(_pen02, X - 2, Y - 3, X - 3, Y - 2);
                g.FillRectangles(_brush01, rects1);
            }
            else
            {

                g.FillRectangle(brush00, new Rectangle(0, 0, X, Y));
                switch (ctl.ButtonState)
                {
                    case ButtonStates.Normal:

                        //g.FillRectangle(silverBrush06, 2, 2, X - 4, Y - 4);
                        //g.FillRectangle(silverBrush07, 3, 4, X - 6, Y - 8);
                        //g.FillRectangle(silverBrush08, 2, Y - 4, X - 4, 2);

                        g.FillRectangle(brush01, 2, 2, X - 4, Y - 7);
                        g.DrawLine(pen01, 2, Y - 5, X - 2, Y - 5);
                        g.DrawLine(pen02, 2, Y - 4, X - 2, Y - 4);
                        g.DrawLine(pen03, 2, Y - 3, X - 2, Y - 3);
                        g.DrawLine(pen04, X - 4, 4, X - 4, Y - 5);
                        g.DrawLine(pen05, X - 3, 4, X - 3, Y - 5);

                        if (isDefault)
                        {
                            g.FillRectangles(brush02, rects0);
                            g.DrawLine(pen06, 2, 2, X - 3, 2);
                            g.DrawLine(pen07, 2, 3, X - 3, 3);
                            g.DrawLine(pen08, 2, Y - 4, X - 3, Y - 4);
                            g.DrawLine(pen09, 2, Y - 3, X - 3, Y - 3);
                        }

                        break;

                    case ButtonStates.MouseOver:

                        //g.FillRectangle(silverBrush06, 2, 2, X - 4, Y - 4);
                        //g.FillRectangle(silverBrush07, 3, 4, X - 6, Y - 8);
                        //g.FillRectangle(silverBrush08, 2, Y - 4, X - 4, 2);

                        g.FillRectangle(brush01, 2, 2, X - 4, Y - 7);
                        g.DrawLine(pen01, 2, Y - 5, X - 4, Y - 5);
                        g.DrawLine(pen02, 2, Y - 4, X - 4, Y - 4);
                        g.DrawLine(pen03, 2, Y - 3, X - 4, Y - 3);
                        g.DrawLine(pen04, X - 4, 4, X - 4, Y - 5);
                        g.DrawLine(pen05, X - 3, 4, X - 3, Y - 5);

                        g.FillRectangles(brush03, rects0);
                        g.DrawLine(pen10, 2, 2, X - 3, 2);
                        g.DrawLine(pen11, 2, 3, X - 3, 3);
                        g.DrawLine(pen12, 2, Y - 4, X - 3, Y - 4);
                        g.DrawLine(pen13, 2, Y - 3, X - 3, Y - 3);

                        break;

                    case ButtonStates.Pushed:
                        //g.FillRectangle(silverBrush06, 2, 2, X - 4, Y - 4);
                        //g.FillRectangle(brush09, 3, 4, X - 6, Y - 9);
                        //g.DrawLine(pen24, 4, 3, X - 4, 3);

                        g.FillRectangle(brush05, 2, 4, X - 4, Y - 8);
                        g.DrawLine(pen17, 2, 3, 2, Y - 4);
                        g.DrawLine(pen18, 3, 3, 3, Y - 4);
                        g.DrawLine(pen19, 2, 2, X - 3, 2);
                        g.DrawLine(pen20, 2, 3, X - 3, 3);
                        g.DrawLine(pen21, 2, Y - 4, X - 3, Y - 4);
                        g.DrawLine(pen22, 2, Y - 3, X - 3, Y - 3);
                        break;
                }

                //if (this.Focused) ControlPaint.DrawFocusRectangle(g,
                //    new Rectangle(3, 3, X - 6, Y - 6), Color.Black, this.BackColor);
            }

            brush01.Dispose();

            brush02.Dispose();
            brush03.Dispose();
            //brush04.Dispose();
            brush05.Dispose();
            brush06.Dispose();
            brush07.Dispose();
            brush08.Dispose();
            brush09.Dispose();

            pen01.Dispose();
            pen02.Dispose();
            pen03.Dispose();
            pen04.Dispose();
            pen05.Dispose();
            pen06.Dispose();
            pen07.Dispose();
            pen08.Dispose();
            pen09.Dispose();
            pen10.Dispose();
            pen11.Dispose();
            pen12.Dispose();
            pen13.Dispose();
            //pen14.Dispose();
            //pen15.Dispose();
            //pen16.Dispose();

            pen17.Dispose();
            pen18.Dispose();
            pen19.Dispose();
            pen20.Dispose();
            pen21.Dispose();
            pen22.Dispose();
            pen23.Dispose();
            pen24.Dispose();

            _brush01.Dispose();
            _brush02.Dispose();
            _pen01.Dispose();
            _pen02.Dispose();

        }

        private LinearGradientBrush ButtonXpBrush07(Rectangle rect)
        {
            LinearGradientBrush brush = new LinearGradientBrush(rect
                , Color.FromArgb(253, 253, 253), Color.FromArgb(201, 200, 220), 90.0f);

            float[] relativeIntensities = { 0.0f, 0.008f, 1.0f };
            float[] relativePositions = { 0.0f, 0.32f, 1.0f };

            Blend blend = new Blend();
            blend.Factors = relativeIntensities;
            blend.Positions = relativePositions;
            brush.Blend = blend;
            return brush;
        }
        private LinearGradientBrush ButtonXpBrush09(Rectangle rect)
        {
            LinearGradientBrush brush = new LinearGradientBrush(rect
                , Color.FromArgb(172, 171, 191), Color.FromArgb(248, 252, 253), 90.0f);
            float[] relativeIntensities = { 0.0f, 0.992f, 1.0f };
            float[] relativePositions = { 0.0f, 0.68f, 1.0f };

            Blend blend = new Blend();
            blend.Factors = relativeIntensities;
            blend.Positions = relativePositions;
            brush.Blend = blend;
            return brush;
        }


        #endregion

        #region Drawing McCheckBox Methods

        public void DrawCheckBox(Graphics g, Rectangle rect, ICheckBox ctl)
        {

            if (ctl.Appearance == Appearance.Button)
            {
                if (ctl.Checked)
                    DrawButtonRect(g, rect, ButtonStates.Pushed,2);
                else
                    DrawButtonRect(g, rect, ButtonStates.Normal,2);

                this.DrawString(g, rect, ContentAlignment.MiddleCenter, ctl.Text, ctl.Font, ctl.Enabled);
                return;
            }

            g.SmoothingMode = SmoothingMode.AntiAlias;

            bool drawBorder = ctl.BorderStyle != BorderStyle.None;
            Brush sb;
            Pen pen;
            Pen penH = new Pen(this.HighlightColorInternal,2);

            bool isHover = false;

            if (!ctl.Enabled)
            {
                sb = this.GetBrushDisabled();
                pen = this.GetPenDisable();
            }
            else if (ctl.Focused)
            {
                sb = this.GetBrushBack();
                pen = this.GetPenFocused();
            }
            else if (ctl.IsMouseHover)//hot
            {
                sb = this.GetBrushBack();
                pen = this.GetPenHot();
                isHover = true;
            }
            else
            {
                sb = this.GetBrushBack();
                pen = this.GetPenBorder();
            }

            if (ctl.ControlLayout != ControlLayout.System)
            {

            }
            if (drawBorder)
            {
                if (ctl.ControlLayout == ControlLayout.System)
                    sb = new SolidBrush(ctl.BackColor);
                g.FillRectangle(sb, rect);
                g.DrawRectangle(pen, rect);
            }

            Rectangle CheckRect = ctl.CheckRect();

            if (ctl.CheckBoxType == CheckBoxTypes.CheckBox)
            {

                using (Brush sbg = this.GetBrushGradient(CheckRect, 220f))
                {
                    g.FillRectangle(sbg, CheckRect);
                }

                if (isHover)
                {
                    Rectangle insudeRect = new Rectangle(CheckRect.X + 1, CheckRect.Y + 1, CheckRect.Width - 2, CheckRect.Height - 2);
                    g.DrawRectangle(penH, insudeRect);
                }
                g.DrawRectangle(pen, CheckRect);


                if (ctl.Checked)
                {
                    //Rectangle drawRect = new Rectangle(CheckRect.Left + 3, CheckRect.Top + 3, 5, 5);
                    //using (Brush sb2 = new SolidBrush(this.BorderHotColorInternal))
                    //{
                    //    g.FillRectangle(sb2, drawRect);
                    //}
                    RectangleF rectV = (RectangleF)CheckRect;
                    using (Pen p = new Pen(Color.DarkGreen, 2))
                    {
                        g.DrawLine(p, rectV.X + 2f, rectV.Y + 4, rectV.X + 6f, rectV.Bottom - 3f);
                        g.DrawLine(p, rectV.X + 4f, rectV.Bottom - 3f, rectV.Right - 2f, rectV.Top + 3f);
                    }
                }

            }
            else if (ctl.CheckBoxType == CheckBoxTypes.RadioButton)
            {
                using (Brush sbg = this.GetBrushGradient(CheckRect, 220f))
                {
                    g.FillEllipse(sbg, CheckRect);
                }

                if (isHover)
                {
                    Rectangle insudeRect = new Rectangle(CheckRect.X + 1, CheckRect.Y + 1, CheckRect.Width - 2, CheckRect.Height - 2);
                    g.DrawEllipse(penH, insudeRect);
                }
                g.DrawEllipse(pen, CheckRect);

                if (ctl.Checked)
                {
                    RectangleF drawRect = new RectangleF(CheckRect.Left + 3, CheckRect.Top + 3, CheckRect.Width - 6, CheckRect.Height - 6);
                    using (Brush sb2 = new SolidBrush(Color.DarkGreen))//this.BorderHotColorInternal))
                    {
                        g.FillEllipse(sb2, drawRect);
                    }
                }
            }
            penH.Dispose();
            sb.Dispose();
            pen.Dispose();
            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;
            format.Alignment = StringAlignment.Near;

            format.HotkeyPrefix = HotkeyPrefix.Show;

            Rectangle txtRect = new Rectangle(rect.Left +
                CheckRect.Right + 2,
                rect.Top - 1,
                rect.Right,
                rect.Bottom);

            Brush fb = (ctl.ControlLayout == ControlLayout.System) ? new SolidBrush(ctl.ForeColor) : this.GetBrushText();

            g.DrawString(ctl.Text, ctl.Font, fb, txtRect, format);
            fb.Dispose();
            format.Dispose();
        }

        public void DrawCheckBox(Graphics g, Rectangle CheckRect, CheckBoxTypes type, Appearance appearance, ButtonStates state, bool Checked, bool Enabled)
        {

            Brush sb;
            Pen pen;
            Pen penH = new Pen(this.HighlightColorInternal,2);

            bool isHover = false;

            if (!Enabled)
            {
                sb = this.GetBrushDisabled();
                pen = this.GetPenDisable();
            }
            else if (state== ButtonStates.Focused)
            {
                sb = this.GetBrushBack();
                pen = this.GetPenFocused();
            }
            else if (state == ButtonStates.MouseOver)//hot
            {
                sb = this.GetBrushBack();
                pen = this.GetPenHot();
                isHover = true;
            }
            else
            {
                sb = this.GetBrushBack();
                pen = this.GetPenBorder();
            }

            if (type == CheckBoxTypes.CheckBox)
            {

                using (Brush sbg = this.GetBrushGradient(CheckRect, 220f))
                {
                    g.FillRectangle(sbg, CheckRect);
                }

                if (isHover)
                {
                    Rectangle insudeRect = new Rectangle(CheckRect.X + 1, CheckRect.Y + 1, CheckRect.Width - 2, CheckRect.Height - 2);
                    g.DrawRectangle(penH, insudeRect);
                }
                g.DrawRectangle(pen, CheckRect);


                if (Checked)
                {
                    //Rectangle drawRect = new Rectangle(CheckRect.Left + 3, CheckRect.Top + 3, 5, 5);
                    //using (Brush sb2 = new SolidBrush(this.BorderHotColorInternal))
                    //{
                    //    g.FillRectangle(sb2, drawRect);
                    //}
                    RectangleF rectV = (RectangleF)CheckRect;
                    using (Pen p = new Pen(Color.DarkGreen, 2))
                    {
                        g.DrawLine(p, rectV.X + 2f, rectV.Y + 4, rectV.X + 6f, rectV.Bottom - 3f);
                        g.DrawLine(p, rectV.X + 4f, rectV.Bottom - 3f, rectV.Right - 2f, rectV.Top + 3f);
                    }
                }

            }
            else if (type == CheckBoxTypes.RadioButton)
            {
                using (Brush sbg = this.GetBrushGradient(CheckRect, 220f))
                {
                    g.FillEllipse(sbg, CheckRect);
                }

                if (isHover)
                {
                    Rectangle insudeRect = new Rectangle(CheckRect.X + 1, CheckRect.Y + 1, CheckRect.Width - 2, CheckRect.Height - 2);
                    g.DrawEllipse(penH, insudeRect);
                }
                g.DrawEllipse(pen, CheckRect);

                if (Checked)
                {
                    RectangleF drawRect = new RectangleF(CheckRect.Left + 3, CheckRect.Top + 3, CheckRect.Width - 6, CheckRect.Height - 6);
                    using (Brush sb2 = new SolidBrush(Color.DarkGreen))//this.BorderHotColorInternal))
                    {
                        g.FillEllipse(sb2, drawRect);
                    }
                }
            }
            penH.Dispose();
            sb.Dispose();
            pen.Dispose();

        }

        #endregion

        #region DrawItem

        public void DrawItemRect(Graphics graphics, Rectangle bounds, DrawItemState state)
        {
            if ((state & DrawItemState.Focus) != DrawItemState.None)
            {
                graphics.FillRectangle(this.GetBrushSelected(), bounds);
                graphics.DrawRectangle(this.GetPenFocused(), bounds.X, bounds.Y, bounds.Width, bounds.Height - 1);
            }
            else if ((state & DrawItemState.Selected) != DrawItemState.None)
            {
                graphics.FillRectangle(this.GetBrushSelected(), bounds);
                graphics.DrawRectangle(this.GetPenHot(), bounds.X, bounds.Y, bounds.Width, bounds.Height - 1);
            }
            else
            {
                using (Brush brush1 = this.GetBrushBack())
                {
                    graphics.FillRectangle(brush1, bounds);
                }
            }
        }


        //		public  void DrawItem(Graphics graphics, Rectangle bounds, DrawItemState state, string text, ImageList imageList, int imageIndex,Font font,RightToLeft rightToLeft)
        //		{
        //			DrawItem(graphics, bounds, state, text, imageList, imageIndex, 0,font,rightToLeft);
        //		}

        //public void DrawItem(Graphics graphics, Rectangle bounds, DrawItemState state, string text, ImageList imageList, int imageIndex, int textStartPos,Font font,RightToLeft rightToLeft)

        public void DrawItem(Graphics graphics, Rectangle bounds, Control ctl, DrawItemState state, string text)
        {
            DrawItemRect(graphics, bounds, state);
            int num1 = 0;
            int textStartPos = 0;
            using (StringFormat format1 = new StringFormat())
            {
                if (ctl.RightToLeft == RightToLeft.Yes)
                {
                    format1.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
                    format1.Alignment = StringAlignment.Far;
                }
                else
                {
                    format1.Alignment = StringAlignment.Near;
                    bounds.X += num1 + textStartPos;
                }
                format1.LineAlignment = StringAlignment.Center;
                format1.FormatFlags = StringFormatFlags.NoWrap;
                format1.Trimming = StringTrimming.EllipsisCharacter;

                bounds.Width -= num1 + textStartPos;
                if (text == null)
                {
                    return;
                }
                Font font = ctl.Font;
                using (Brush brush1 = this.GetBrushText())
                {
                    graphics.DrawString(text, font, brush1, (RectangleF)bounds, format1);
                }
            }
        }

        public void DrawItem(Graphics graphics, Rectangle bounds, IMcList ctl, DrawItemState state, string text, int imageIndex)
        {
            if (ctl.DrawItemStyle == DrawItemStyle.LinkLabel)
            {
                using (Brush brush1 = this.GetBrushBack())
                {
                    graphics.FillRectangle(brush1, bounds);
                }
            }
            else
            {
                DrawItemRect(graphics, bounds, state);
            }
            int num1 = 0;
            int textStartPos = 0;
            if (ctl.ImageList != null)
            {
                ImageList imageList = ctl.ImageList;
                if ((imageIndex >= 0) && (imageIndex < imageList.Images.Count))
                {
                    Rectangle rectImage;
                    if (ctl.RightToLeft == RightToLeft.Yes)
                        rectImage = new Rectangle(bounds.Width - imageList.ImageSize.Width - 1, bounds.Y + ((bounds.Height - imageList.ImageSize.Height) / 2), imageList.ImageSize.Width, imageList.ImageSize.Height);
                    else
                        rectImage = new Rectangle(bounds.X + 1, bounds.Y + ((bounds.Height - imageList.ImageSize.Height) / 2), imageList.ImageSize.Width, imageList.ImageSize.Height);

                    //Rectangle rectangle1 = new Rectangle(bounds.X + 1, bounds.Y + ((bounds.Height - imageList.ImageSize.Height) / 2), imageList.ImageSize.Width, imageList.ImageSize.Height);
                    imageList.Draw(graphics, rectImage.X, rectImage.Y, rectImage.Width, rectImage.Height, imageIndex);
                    num1 = imageList.ImageSize.Width + 2;
                }
            }
            using (StringFormat format1 = new StringFormat())
            {
                if (ctl.RightToLeft == RightToLeft.Yes)
                {
                    format1.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
                    format1.Alignment = StringAlignment.Far;
                }
                else
                {
                    format1.Alignment = StringAlignment.Near;
                    bounds.X += num1 + textStartPos;
                }
                format1.LineAlignment = StringAlignment.Center;
                format1.FormatFlags = StringFormatFlags.NoWrap;
                format1.Trimming = StringTrimming.EllipsisCharacter;

                bounds.Width -= num1 + textStartPos;
                if (text == null)
                {
                    return;
                }
                Font font = ctl.Font;
                if (ctl.DrawItemStyle == DrawItemStyle.LinkLabel)
                {
                    if (((state & DrawItemState.Focus) != DrawItemState.None) || ((state & DrawItemState.Selected) != DrawItemState.None))
                    {
                        font = new Font(ctl.Font, FontStyle.Underline);
                    }
                }
                using (Brush brush1 = this.GetBrushText())
                {
                    graphics.DrawString(text, font, brush1, (RectangleF)bounds, format1);
                }
            }
        }

        //		public void DrawItem(Graphics g, Rectangle bounds,PopUpItem ctl,DrawItemStyle drawStyle, DrawItemState state,bool rightToLeft,int imageRectWidth )
        //		{
        //	
        //			if(drawStyle==DrawItemStyle.LinkLabel)
        //			{
        //				using (Brush brush1 = this.GetBrushBack())
        //				{
        //					g.FillRectangle(brush1, bounds);
        //				}
        //			}
        //			else
        //			{
        //				DrawItemRect(g, bounds, state);
        //			}
        //
        //	
        //			//int num1 = 0;
        //			//int textStartPos=0;
        //
        //			if(drawStyle==DrawItemStyle.CheckBox)
        //			{
        //	
        //				Image image=null;
        //				if(ctl.Checked)
        //					image=ResourceUtil.ExtractImage (Global.ImagesPath + "checked1.gif") ;
        //				else
        //					image=ResourceUtil.ExtractImage (Global.ImagesPath + "checked2.gif") ;
        //        
        //				Rectangle rectCheck;
        //				if (rightToLeft)
        //					rectCheck = new Rectangle(bounds.Width-image.Width-1, bounds.Y + ((bounds.Height - image.Height) / 2), image.Width, image.Height);
        //				else
        //					rectCheck = new Rectangle(bounds.X + 1, bounds.Y + ((bounds.Height - image.Height) / 2), image.Width, image.Height);
        // 
        //				if(image!=null)
        //				g.DrawImage(image,rectCheck.X, rectCheck.Y, rectCheck.Width, rectCheck.Height);
        //				//num1 = image.Width + 2;
        //				imageRectWidth=image.Width + 2;
        //			}
        //			else if(ctl.ImageList!=null )
        //			{
        //				ImageList imageList=ctl.ImageList;
        //				if(imageRectWidth >= imageList.ImageSize.Width)
        //				{
        //					int imageIndex=ctl.ImageIndex;
        //
        //					if ((imageIndex >= 0) && (imageIndex < imageList.Images.Count))
        //					{
        //						Rectangle rectImage;
        //						if (rightToLeft)
        //							rectImage = new Rectangle(bounds.Width-imageList.ImageSize.Width-1, bounds.Y + ((bounds.Height - imageList.ImageSize.Height) / 2), imageList.ImageSize.Width, imageList.ImageSize.Height);
        //						else
        //							rectImage = new Rectangle(bounds.X + 1, bounds.Y + ((bounds.Height - imageList.ImageSize.Height) / 2), imageList.ImageSize.Width, imageList.ImageSize.Height);
        //		
        //						//Rectangle rectangle1 = new Rectangle(bounds.X + 1, bounds.Y + ((bounds.Height - imageList.ImageSize.Height) / 2), imageList.ImageSize.Width, imageList.ImageSize.Height);
        //						imageList.Draw(g, rectImage.X, rectImage.Y, rectImage.Width, rectImage.Height, imageIndex);
        //						//num1 = imageList.ImageSize.Width + 2;
        //					}
        //				}
        //			}
        //			using (StringFormat format1 = new StringFormat())
        //			{
        //				if (rightToLeft)
        //				{
        //					format1.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
        //					format1.Alignment = StringAlignment.Far;
        //				}
        //				else
        //				{
        //					format1.Alignment = StringAlignment.Near;
        //					bounds.X += imageRectWidth;// + textStartPos;
        //				}
        //				format1.LineAlignment = StringAlignment.Center;
        //				format1.FormatFlags = StringFormatFlags.NoWrap;
        //				format1.Trimming = StringTrimming.EllipsisCharacter;
        //	
        //				bounds.Width -= imageRectWidth;// + textStartPos;
        //				if (ctl.Text == null)
        //				{
        //					return;
        //				}
        //				Font font= Control.DefaultFont;
        //				if(drawStyle==DrawItemStyle.LinkLabel)
        //				{
        //					if (((state & DrawItemState.Focus) != DrawItemState.None)||((state & DrawItemState.Selected) != DrawItemState.None))
        //					{
        //						font=new Font(font,FontStyle.Underline); 
        //					}
        //				}
        //
        //				using (Brush brush1 =this.GetBrushText())
        //				{
        //					g.DrawString(ctl.Text, font, brush1, (RectangleF) bounds, format1);
        //				}
        //			}	
        //		}
        //

        #endregion

        #region Draw McStatusBar

        //		public void DrawStatusBar(Graphics g,Rectangle rect,IStatusBar ctl,bool Enable,Color panelBack,Color panelBorder)
        //		{
        //			Pen pen;
        //			Brush sb;
        //
        //			if(!Enable)
        //			{
        //				sb=this.GetBrushDisabled();
        //				pen= this.GetPenDisable();
        //			}
        //			else
        //			{
        //				sb= this.GetBrushFlat();
        //				pen=this.GetPenBorder();
        //			}
        //
        //			g.FillRectangle (sb,rect);
        //			g.DrawRectangle (pen,rect);
        //			sb.Dispose();
        //			pen.Dispose();
        //
        //			Brush sbText=this.GetBrushText();
        //			Brush sbPanelBack=new SolidBrush(panelBack);
        //			Font font=ctl.Font;
        //			int height=ctl.Height;            
        //			float xp=0;
        //			SizeF sf;
        //
        //			if(ctl.ShowPanels)
        //			{
        //				//PaintPanels(g);
        //				int lf=ctl.Left;
        //
        //				Rectangle rectB;
        //
        //				foreach(System.Windows.Forms.StatusBarPanel p  in ctl.Panels )
        //				{
        //					rectB =new Rectangle(lf+1 , 2 , p.Width-2, height-4);
        //                 
        //					g.FillRectangle(sbPanelBack, rectB );
        //					xp=0;
        //					sf=g.MeasureString (p.Text ,font);
        //		
        //					switch(p.Alignment)
        //					{
        //						case System.Windows.Forms.HorizontalAlignment.Left  :
        //							xp=(float)rectB.X;
        //							break;
        //						case System.Windows.Forms.HorizontalAlignment.Right  :
        //							xp= (float)rectB.X + (float)rectB.Width - sf.Width ;
        //							break;
        //						case System.Windows.Forms.HorizontalAlignment.Center :
        //							xp= (float)rectB.X + (((float)rectB.Width - sf.Width)/2) ;
        //							break;
        //					}
        //					float yp= (float)rectB.Y +(((float)rectB.Height -sf.Height)/2) ;
        //		
        //					g.DrawString (p.Text ,font ,sbText,xp ,yp );  
        //					ControlPaint.DrawBorder(g, rectB, panelBorder , ButtonBorderStyle.Solid  );
        //					lf+=p.Width ;
        //				}
        //
        //			}
        //			else
        //			{
        //				xp=0;
        //				sf=g.MeasureString (ctl.Text ,font);
        //		
        //				switch(ctl.RightToLeft)
        //				{
        //					case System.Windows.Forms.RightToLeft.Yes  :
        //						xp= (float)rect.X + (float)rect.Width - sf.Width ;
        //						break;
        //					default:
        //						xp=(float)rect.X;
        //						break;
        //				}
        //				float yp= (float)rect.Y +(((float)rect.Height -sf.Height)/2) ;
        //		
        //				g.DrawString (ctl.Text ,font ,sbText,xp ,yp );  
        //			
        //			}
        //
        //			sbText.Dispose();
        //			sbPanelBack.Dispose();
        //			font.Dispose();
        //
        //
        //		}

        #endregion

        #region Draw MenuItem

        private string ConvertShortcut(Shortcut shortcut)
        {
            if (shortcut == Shortcut.None)
            {
                return "";
            }
            Keys keys1 = (Keys)shortcut;
            return TypeDescriptor.GetConverter(typeof(Keys)).ConvertToString(keys1);
        }

        public Brush GetBrushMenuBar(Rectangle rectangle, float angle)//, bool revers)
        {
            return new LinearGradientBrush(rectangle, this.ColorBrush1Internal, this.ColorBrush2Internal, angle);
        }

        public Brush GetBrushTextDisable()
        {
            return new SolidBrush(McPaint.Light(this.ForeColorInternal, 150));
        }

        public Brush GetBrushBar()
        {
            return new SolidBrush(this.BackgroundColorInternal);
        }


        //		public virtual void DrawItem(MenuItem sender, DrawItemEventArgs e,Image image ,bool rightToLeft)
        //		{
        //		   DrawItem(sender, e,image ,rightToLeft,Color.Empty);
        //		}

        public virtual void DrawMenuItem(MenuItem sender, DrawItemEventArgs e, Image image, bool rightToLeft, bool drawBar)//Color BarColor)
        {

            const int grWidth = 0x18;
            int ShortcutWidth = 0;
            int ArrowWidth = 15;
            Font font = SystemInformation.MenuFont;
            MenuItem item1 = (MenuItem)sender;

            using (Bitmap bitmap1 = new Bitmap(e.Bounds.Width, e.Bounds.Height))
            {
                using (Graphics graphics1 = Graphics.FromImage(bitmap1))
                {
                    StringFormat format2;
                    Rectangle rectIcon;
                    Rectangle rectTextItem;
                    Rectangle rectItem = new Rectangle(0, 0, e.Bounds.Width - 1, e.Bounds.Height - 1); ;
                    Rectangle rectIconBar;
                    Rectangle rectText;

                    string textShort = "";

                    if (item1.ShowShortcut)
                    {
                        textShort = this.ConvertShortcut(item1.Shortcut);
                        SizeF ef1 = graphics1.MeasureString(textShort, font);
                        float single1 = ef1.Width;
                        ShortcutWidth = 2 + (int)single1;
                    }

                    if (rightToLeft)
                    {
                        rectIcon = new Rectangle(e.Bounds.Width - grWidth, 0, grWidth, e.Bounds.Height - 1);
                        rectTextItem = new Rectangle(0, 0, e.Bounds.Width - grWidth, e.Bounds.Height);
                        rectIconBar = new Rectangle(e.Bounds.Width - grWidth, 0, grWidth, e.Bounds.Height);
                        SizeF txtSize = graphics1.MeasureString(item1.Text, font);
                        rectText = rectTextItem;
                        //rectText = new Rectangle(ShortcutWidth+(rectTextItem.Width-ShortcutWidth)-((int)txtSize.Width),rectTextItem.Y,(int)txtSize.Width,rectTextItem.Height);
                        rectText.X -= 5;

                    }
                    else
                    {
                        rectIcon = new Rectangle(0, 0, grWidth, e.Bounds.Height - 1);
                        rectTextItem = new Rectangle(grWidth, 0, e.Bounds.Width, e.Bounds.Height);
                        rectIconBar = new Rectangle(0, 0, grWidth, e.Bounds.Height);
                        rectText = rectTextItem;
                        rectText.X += 5;
                    }

                    Brush sbSelected = this.GetBrushSelected();
                    Brush sbContent = this.GetBrushContent();
                    Brush sbText = this.GetBrushText();
                    Pen penSelected = this.GetPenHot();


                    if (item1.Parent is MainMenu)
                    {
                        rectText = rectItem;
                    }
                    else if (((e.State & DrawItemState.Selected) <= DrawItemState.None) | !item1.Enabled)
                    {
                        using (Brush brush1 = GetBrushMenuBar(rectIconBar, rightToLeft ? 0f : 180f))// McBrushes.GetControlBrush(rectIconBar, 0f))
                        {
                            graphics1.FillRectangle(brush1, rectIconBar);
                        }
                    }
                    if (((item1.Text.Length >= 4) && (item1.Text.Substring(0, 2) == "--")) && (item1.Text.Substring(item1.Text.Length - 2) == "--"))
                    {

                        using (Brush sb = GetBrushMenuBar(rectIconBar, 90f))
                        {
                            graphics1.FillRectangle(sb, rectItem);//McBrushes.GetActiveCaptionBrush(rectItem, 90f), rectItem);
                        }
                        using (StringFormat format1 = new StringFormat())
                        {
                            format1.Alignment = StringAlignment.Center;
                            format1.LineAlignment = StringAlignment.Center;
                            format1.FormatFlags = StringFormatFlags.NoWrap;
                            format1.Trimming = StringTrimming.EllipsisCharacter;
                            format1.HotkeyPrefix = HotkeyPrefix.Hide;


                            using (Brush brush2 = new SolidBrush(SystemColors.ActiveCaptionText))
                            {
                                graphics1.DrawString(item1.Text.Substring(2, item1.Text.Length - 4), font, brush2, (RectangleF)rectItem, format1);
                            }
                        }
                        graphics1.DrawRectangle(SystemPens.ControlDark, rectItem);
                        goto Label_06DD;
                    }
                    //Seperator
                    if (item1.Text == "-")
                    {
                        int lineHeight = rectTextItem.Y + (rectTextItem.Height / 2);

                        graphics1.FillRectangle(sbContent, rectTextItem);
                        if (rightToLeft)
                            graphics1.DrawLine(SystemPens.ControlDark, rectTextItem.X + 2, lineHeight, rectTextItem.Width - 2, lineHeight);
                        else
                            graphics1.DrawLine(SystemPens.ControlDark, rectTextItem.X + 8, lineHeight, rectTextItem.Width, lineHeight);

                        goto Label_06DD;
                    }

                    //Image
                    Rectangle rectImage = rectTextItem;

                    Rectangle barRect = new Rectangle(0, -2, e.Bounds.Width, e.Bounds.Height + 2);

                    if (image != null)
                    {
                        if (rightToLeft)
                            rectImage = new Rectangle(rectTextItem.Width + ((grWidth - image.Width) / 2), rectTextItem.Y + ((0x16 - image.Height) / 2), image.Width, image.Height);
                        else
                            rectImage = new Rectangle((grWidth - image.Width) / 2, rectTextItem.Y + ((0x16 - image.Height) / 2), image.Width, image.Height);
                    }
                    if (((e.State & DrawItemState.Selected) > DrawItemState.None) && item1.Enabled)
                    {
                        graphics1.FillRectangle(sbSelected, barRect);// rectItem);//McBrushes.Focus, rectItem);
                        graphics1.DrawRectangle(penSelected, rectItem);//McPens.SelectedText, rectItem);
                    }
                    else
                    {
                        if (item1.Parent is MainMenu)
                        {	//Draw Top menuBar item
                            //Rectangle barRect=new Rectangle(0, -2, e.Bounds.Width, e.Bounds.Height);
                            using (Brush brush4 = drawBar ? this.GetBrushMenuBar(barRect, 270f) : new SolidBrush(SystemColors.Control))
                            {
                                graphics1.FillRectangle(brush4, barRect);
                            }
                            goto Label_040D;
                        }
                        graphics1.FillRectangle(sbContent, rectTextItem);
                    }
                Label_040D://String
                    format2 = new StringFormat();
                    try
                    {
                        if (item1.Parent is MainMenu)
                        {
                            format2.Alignment = StringAlignment.Center;
                        }
                        else
                        {
                            if (rightToLeft)
                                format2.Alignment = StringAlignment.Far;
                            else
                                format2.Alignment = StringAlignment.Near;
                        }
                        format2.LineAlignment = StringAlignment.Center;
                        format2.FormatFlags |= StringFormatFlags.NoWrap;
                        format2.HotkeyPrefix = HotkeyPrefix.Show;
                        if (item1.Enabled)
                        {
                            if (item1.DefaultItem)
                            {
                                using (Font font1 = new Font(font, FontStyle.Bold))
                                {
                                    graphics1.DrawString(item1.Text, font1, sbText, (RectangleF)rectText, format2);
                                    goto Label_051C;
                                }
                            }
                            graphics1.DrawString(item1.Text, font, sbText, (RectangleF)rectText, format2);
                            goto Label_051C;

                        }
                        using (Brush sbDisable = this.GetBrushTextDisable())// McPaint.Light(colorText, 150)))
                        {
                            graphics1.DrawString(item1.Text, font, sbDisable, (RectangleF)rectText, format2);
                        }
                    }
                    finally
                    {
                        if (format2 != null)
                        {
                            format2.Dispose();
                        }
                    }
                Label_051C://Shortcut
                    if (item1.ShowShortcut)
                    {
                        using (StringFormat format3 = new StringFormat())
                        {
                            format3.LineAlignment = StringAlignment.Center;
                            Rectangle shortRect;
                            if (rightToLeft)
                            {
                                format3.Alignment = StringAlignment.Near;
                                shortRect = new Rectangle(rectItem.X + ArrowWidth + 10, rectItem.Y, rectItem.Width - ArrowWidth, rectItem.Height);
                            }
                            else
                            {
                                format3.Alignment = StringAlignment.Far;
                                shortRect = new Rectangle(rectItem.X, rectItem.Y, rectItem.Width - ArrowWidth, rectItem.Height);
                            }
                            if (item1.Enabled)
                            {
                                graphics1.DrawString(textShort, font, sbText, (RectangleF)shortRect, format3);//new Rectangle(rectItem.X, rectItem.Y, rectItem.Width - 15, rectItem.Height), format3);
                                goto Label_060C;

                            }
                            ControlPaint.DrawStringDisabled(graphics1, textShort, font, SystemColors.Control, (RectangleF)shortRect, format3);
                        }
                    }
                Label_060C://Checked
                    if (item1.Checked && item1.Enabled)
                    {
                        int iconPointX;
                        int iconPointY = rectTextItem.Y + 11;
                        if (rightToLeft)
                        {
                            iconPointX = rectTextItem.Width + 11 + 1;
                            rectIcon.X += 2;
                        }
                        else
                        {
                            iconPointX = 11;
                            rectIcon.X++;
                        }

                        rectIcon.Y++;
                        rectIcon.Width -= 4;
                        rectIcon.Height -= 2;

                        graphics1.FillRectangle(sbSelected, rectIcon);//McBrushes.Focus, rectItem);
                        graphics1.DrawRectangle(penSelected, rectIcon);//McPens.SelectedText, rectItem);

                        if (image == null)
                        {
                            McPaint.DrawCheck(graphics1, iconPointX, iconPointY, true);
                        }
                    }
                    if (image != null)
                    {

                        if (item1.Enabled)
                        {
                            if (rightToLeft)
                                graphics1.DrawImage(image, rectImage.X, rectImage.Y, rectImage.Width, rectImage.Height);
                            else
                                graphics1.DrawImage(image, rectImage.X, rectImage.Y, rectImage.Width, rectImage.Height);
                        }
                        else
                        {
                            McPaint.DrawImageDisabled(graphics1, image as Bitmap, rectImage.X, rectImage.Y);
                        }
                    }
                Label_06DD:
                    e.Graphics.DrawImage(bitmap1, e.Bounds.Left, e.Bounds.Top, e.Bounds.Width, e.Bounds.Height);

                    sbSelected.Dispose();
                    penSelected.Dispose();
                    sbContent.Dispose();
                    sbText.Dispose();
                    graphics1.Dispose();
                }
            }
        }

        #endregion

    }


    //	#endregion


}
