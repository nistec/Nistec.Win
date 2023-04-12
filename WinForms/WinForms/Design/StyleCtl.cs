using System;
using System.ComponentModel;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;   
using System.Text;
using Nistec.WinForms.Controls;


namespace Nistec.WinForms
{
	#region Enums

	public enum PainterTypes
	{
		/// <summarry>
		/// Style painter for edit controls
		/// </summary>
		Edit,
		/// <summary>
		/// Style painter for buttons controls
		/// </summary>
		Button,
		/// <summary>
		/// Style painter for container controls
		/// </summary>
		Flat,
		/// <summary>
		/// Style painter for All controls
		/// </summary>
		Guide,
		/// <summary>
		/// Style painter for Grid Control
		/// </summary>
		Grid
}

	#endregion

	#region Static StyleControl:ILayout

    //public sealed class StyleControl
    //{
    //    private static readonly StyleLayout _Layout;

    //    static StyleControl()
    //    {
    //        Styles style = StyleLayout.GetRegistryStyle();
    //        _Layout=new StyleLayout(style);
    //    }

    //    public static StyleLayout Layout
    //    {
    //        get{return _Layout;} 
    //    }
    //    public static Styles StylePlan
    //    {
    //        get{return _Layout.StylePlan;} 
    //    }

    //    #region static properties

    //    public const string ControlColorString = "252, 252, 254";
        
    //    public static Color ControlColor
    //    {
    //        get { return Color.FromArgb(252, 252, 254); }
    //    }
    //    public static Color ButtonHotColor
    //    {
    //        get { return Color.Orange; }
    //    }
    //    public static Color BackColor
    //    {
    //        get { return Color.White; }
    //    }
    //    public static Color ForeColor
    //    {
    //        get { return Color.Black; }
    //    }
    //    public static Color BorderColor
    //    {
    //        get { return Color.FromArgb(49, 106, 197); }
    //    }
    //    public static Color BorderHotColor
    //    {
    //        get { return Color.FromArgb(26, 80, 184); }
    //    }
    //    public static Color FocusedColor
    //    {
    //        get { return Color.RoyalBlue; }
    //    }
    //    public static Color ColorBrush1
    //    {
    //        get { return Color.FromArgb(205, 203, 183); }
    //    }
    //    public static Color ColorBrush2
    //    {
    //        get { return StyleControl.ControlColor; }
    //    }
    //    public static Color CaptionColor
    //    {
    //        get { return Color.FromArgb(0, 78, 152); }
    //    }
    //    public static Color CaptionLightColor
    //    {
    //        get { return Color.FromArgb(204, 223, 252); }
    //    }
 
    //    public static Color InactiveColor
    //    {
    //        get { return Color.Gray; }
    //    }
        
    //    public static Font DefaultFont
    //    {
    //        get { return Form.DefaultFont; }
    //    }

    //    public static Color GetDarkColor(Color color)
    //    {
    //        return ControlPaint.Dark(color);
    //    }

    //    public static Color GetDarkDarkColor(Color color)
    //    {
    //        return ControlPaint.DarkDark(color);
    //    }
    //    public static Color GetLightLightColor(Color color)
    //    {
    //        return ControlPaint.LightLight(color);
    //    }
    //    public static Color DisabledTextColor(Color backColor)
    //    {
    //        Color controlDark = SystemColors.ControlDark;
    //        //if (ControlPaint.IsDarker(backColor, SystemColors.Control))
    //        //{
    //        controlDark = ControlPaint.Dark(backColor);
    //        //}
    //        return controlDark;
    //    }

    //    #endregion

    //}


    //public interface ILayout
    //{
    //    StyleLayout Layout{get;}
    //    Styles StylePlan{get;}
    //}

	#endregion

	#region Interfaces

	public interface ILayout
	{
		void SetStyleLayout(Styles value);
		void SetStyleLayout(StyleLayout value);
		IStyleLayout LayoutManager{get;}
		PainterTypes PainterType{get;}
		IStyle StylePainter {get;set;}
        ControlLayout ControlLayout { get;set;}
 	}

	public interface IStyle
	{
		PainterTypes PainterType{get;}
		Color BackColor{get;set;}
		Color ForeColor{get;set;}
		Color BorderColor{get;set;}
        
		Styles StylePlan{get;set;}
		StyleLayout Layout{get;}

        Font TextFont { get;set;}
        Font CaptionFont { get;set; }
		event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
	}

	public interface IStyleEdit:IStyle
	{
		Color BorderHotColor{get;set;}
		Color FocusedColor{get;set;}
	}

	public interface IStyleContainer:IStyle
	{
		Color BackgroundColor{get;set;}
	}

	public interface IStyleButton:IStyle
	{
        Color ButtonBorderColor { get;set;}
        Color BorderHotColor { get;set;}
		Color FocusedColor{get;set;}
		Color BackgroundColor{get;set;}
		Color ColorBrush1{get;set;}
		Color ColorBrush2{get;set;}
	}

	public interface IStyleGrid:IStyle
	{
		Color BorderHotColor {get;set;}
		Color CaptionBackColor {get;set;}
		Color CaptionForeColor {get;set;}
		Color AlternatingColor {get;set;}
		//Color GridBackColor {get;set;}
		//Color GridForeColor {get;set;}
		Color HeaderBackColor {get;}
		Color HeaderForeColor {get;}
		Color SelectionBackColor {get;}
		Color SelectionForeColor {get;}

//		Color ColorBrush1{get;}
//		Color ColorBrush2{get;}
//		Color BackgroundColor{get;}
//		Color FocusedColor{get;}
	}

	public interface IStyleGuide:IStyle
	{
		Color BorderHotColor{get;set;}
		Color FocusedColor{get;set;}
		Color BackgroundColor{get;set;}
		Color ColorBrush1{get;set;}
		Color ColorBrush2{get;set;}
		Color DisableColor{get;set;}
		Color AlternatingColor{get;set;}
		Color CaptionColor{get;set;}
		Color FormColor{get;set;}
		Color HighlightColor{get;}
		Color ContentColor{get;}
		Color SelectedColor{get;}
	}
	#endregion

	#region StyleBase
	
	/// <summary>
	/// Summary description for StyleBase.
	/// </summary>

	[DesignTimeVisible(false),ToolboxItem(false)]
	public abstract class StyleBase :System.ComponentModel.Component,IStyle
	{

		#region Members
		protected float m_Radius=3;
		//protected Control m_Parent;

		private System.ComponentModel.Container components = null;
		protected StyleLayout m_StyleLayout;
		protected Form m_Form;
		protected Color FormColorInternal;
        protected ControlLayout m_ControlLayout;

		#endregion

		#region Events

		[Category("StyleChanges")]
		public event ColorStyleChangedEventHandler ColorStyleChanged=null;
		[Category("StyleChanges")]
		public event PropertyChangedEventHandler PropertyChanged; 

		/// <summary>
		/// Deelegate delc
		/// </summary>
		//public delegate void PropertyChangedEventHandler(PropertyChangedEventArgs e);
		protected  void OnPropertyChanged(string propertyname)
		{
			if(PropertyChanged!=null)
				PropertyChanged(this,new  PropertyChangedEventArgs(propertyname));
		}

		protected  void OnColorStyleChanged(Styles style)
		{
			if(ColorStyleChanged!=null)
				ColorStyleChanged (this,new  ColorStyleChangedEventArgs(style));
		}

		#endregion

		#region Constructors

		public StyleBase(System.ComponentModel.IContainer container)
		{
			container.Add(this);
            InitMcStyleBase(StyleLayout.DefaultStylePlan);
		}

		public StyleBase(System.ComponentModel.IContainer container,Styles style)
		{
			container.Add(this);
			InitMcStyleBase(style);
		}

		public StyleBase(System.ComponentModel.IContainer container,Form form)
		{
			container.Add(this);
            InitMcStyleBase(StyleLayout.DefaultStylePlan);
			m_Form=form;  
		}

		public StyleBase()
		{
            InitMcStyleBase(StyleLayout.DefaultStylePlan);
		}

		private void InitMcStyleBase(Styles style)
		{
			m_StyleLayout= new StyleLayout(style);
			components = new System.ComponentModel.Container();
			m_StyleLayout.ForeColorInternal = Color.Black;
			m_StyleLayout.BackColorInternal = Color.White;
			//base.HighlightColorInternal = Color.Gold  ;
			//base.ClickColor = System.Drawing.Color.WhiteSmoke;
		}

		protected override void Dispose(bool disposing)
		{
			if(disposing)
			{
//				if(this.components!=null)
//				{
//					foreach(object c in this.components.Components)
//					{
//						Control ctl=c as Control;
//						if(c is ILayout)
//						{
//							if(((ILayout)ctl).StylePainter==this)
//							{
//								((ILayout)ctl).StylePainter=null;
//								//c.Refresh(); 
//							}
//						}
//					}
//				}
			}
			base.Dispose (disposing);
		}

		#endregion

		#region Settings

		//[Browsable(false)]
		public abstract PainterTypes PainterType{get;}

		public StyleLayout Layout
		{
			get{return this.m_StyleLayout;}
            //set { m_StyleLayout = value; }
		}

//		public IStyleLayout StyleBase
//		{
//			get{return (IStyleLayout)this.m_StyleLayout;}
//		}

		public void SetStyleLayout(StyleLayout value)
		{
			m_StyleLayout=value;//\\.SetStyleLayout (value); 
		}

		public void SetStyleLayout(Styles value)
		{
			m_StyleLayout.StylePlan=value;//\\.SetStyleLayout (value);
		}

		public virtual Styles GetMcStyle()
		{
			return Layout.StylePlan;
		}

		#endregion

		#region Internal Properties

		internal virtual float Radius
		{
			get {return m_Radius;}
			set{m_Radius=value;}
		}

		internal virtual bool IsFlat
		{
			get {return Layout.IsFlatInternal;}
			set{Layout.IsFlatInternal=value;}
		}

		internal virtual bool XpDisable
		{
			get {return Layout.XpDisableInternal ;}
			set{Layout.XpDisableInternal=value;}
		}

		//[System.ComponentModel.RefreshProperties(RefreshProperties.All )]
		internal void SetPainterAction(string cmd,PainterTypes type)
		{

			if(cmd=="Release")
			{
				foreach(Component ctl in this.Container.Components)
				{
					if (ctl is ILayout)
					{
						ILayout cs= (ILayout)ctl as ILayout;

						//if(cs.StylePainter!=null)
						//{
							// if(cs.StylePainter.GetType() is IStyleGuide)
							if(cs.PainterType==type || type==PainterTypes.Guide)
								cs.StylePainter=null;
						//}
					}
				}
			}
			else if(cmd=="Forces")
			{
	
				foreach(Component ctl in this.Container.Components)
				{
					if (ctl is ILayout)
					{
						ILayout cs= (ILayout)ctl as ILayout;
						if(type ==PainterTypes.Guide)
						{
							cs.StylePainter=this as IStyle;
							if(m_Form ==null)
								m_Form=((Control)ctl).FindForm();
						}
						else if(cs.PainterType==type)
							cs.StylePainter=this as IStyle;
					}
 			
				}
				if(type ==PainterTypes.Guide)
				{
					if(m_Form!=null)
					 	m_Form.BackColor=this.FormColorInternal;
				}
			}
            else if (cmd == "ControlLayout")
            {
                GetActiveDesignForm();
                if (m_Form != null)
                    SetControlLayout(GetActiveDesignForm(), this.ControlsLayout);
            }
			//if(ColorStyleChanged!=null)
			//	ColorStyleChanged(this,new ColorStyleChangedEventArgs(m_StyleLayout.StylePlan));
		}

		internal Form GetActiveDesignForm()
		{
			if(m_Form!=null)
				return m_Form;
            try
            {
                foreach (Component ctl in this.Container.Components)
                {
                    if (ctl is Control)
                    {
                        m_Form = ((Control)ctl).FindForm();
                        if (m_Form != null)
                            break;
                    }
                }
            }
            catch { }
                return m_Form;
		}

        public void SetControlLayout(Form form, ControlLayout layout)
        {
            SetControlLayout(form, layout, PainterTypes.Guide);
       }

        public void SetControlLayout(Form form, ControlLayout layout, PainterTypes type)
        {
            foreach (Control ctl in form.Controls)
            {
                if (ctl is ILayout)
                {
                    SetControlLayout(ctl, layout, type);
                }
            }
        }

        private void SetControlLayout(Control c, ControlLayout layout, PainterTypes type)
        {
            if (c is McPanel || c is McTabControl || c is McContainer)
            {
                ILayout cs = (ILayout)c as ILayout;
                if (type == PainterTypes.Guide || cs.PainterType == type)
                {
                    cs.ControlLayout = layout;
                }
                

                foreach (Control ctl in c.Controls)
                {
                    if (ctl is ILayout)
                    {
                        SetControlLayout(ctl, layout, type);
                        //cs = (ILayout)ctl as ILayout;
                        //if (type == PainterTypes.Guide || cs.PainterType == type)
                        //{
                        //    cs.ControlLayout = layout;
                        //}
                    }

                }
            }
            else if (c is ILayout)
            {
                ILayout cs = (ILayout)c as ILayout;
                if (type == PainterTypes.Guide || cs.PainterType == type)
                {
                    cs.ControlLayout = layout;
                }
            }
        }
		#endregion

		#region Color Properties

		[Category("Style"),System.ComponentModel.RefreshProperties(RefreshProperties.All )]
		public virtual Styles StylePlan
		{
			get {return m_StyleLayout.StylePlan;}
			set
			{

				if(m_StyleLayout.StylePlan!=value )
				{
					m_StyleLayout.StylePlan=value;
					if(value!=Styles.None)
					{
						//\\m_StyleLayout.SetStyleLayout (value);
                        m_StyleLayout.StylePlan = value;
						OnColorStyleChanged(value);
						if(value==Styles.Custom)
							OnPropertyChanged("StyleLayout");
						else
							OnPropertyChanged("StylePlan");
		
						//SetStyleLayout();
						if(PainterType==PainterTypes.Guide)
						{
                          this.FormColorInternal=Layout.ColorBrush2Internal;
						}
						
					}
				}
			}
		}

		[Category("BaseColor"),DefaultValue(typeof(System.Drawing.Color),"White")]
		public virtual Color BackColor
		{
			get {return Layout.BackColorInternal;}
			set
			{
				if(value!=Color.Empty )
				{
					Layout.BackColorInternal=value;
					OnPropertyChanged("BackColor");
				}
			}
		}

		[Category("BaseColor"),DefaultValue(typeof(System.Drawing.Color),"Black")]
		public virtual Color ForeColor
		{
			get {return Layout.ForeColorInternal;}
			set
			{
				if(value!=Color.Empty )
				{
					Layout.ForeColorInternal=value;
					OnPropertyChanged("ForeColor");
				}
			}
		}

		[Category("BaseColor"),DefaultValue(typeof(System.Drawing.Color),"Gray")]
		public virtual Color BorderColor
		{
			get {return Layout.BorderColorInternal;}
			set
			{
				if(value!=Color.Empty )
				{
					Layout.BorderColorInternal=value;
					OnPropertyChanged("BorderColor");
				}
			}
		}

        [Category("Font")]//, DefaultValue(typeof(System.Drawing.Font), "Gray")]
        public virtual Font TextFont
        {
            get { return Layout.TextFontInternal; }
            set
            {
                if (value != null)
                {
                    Layout.TextFontInternal = value;
                    OnPropertyChanged("TextFont");
                }
            }
        }
        [Category("Font")]//, DefaultValue(typeof(System.Drawing.Font), "Gray")]
        public virtual Font CaptionFont
        {
            get { return Layout.CaptionFontInternal; }
            set
            {
                if (value != null)
                {
                    Layout.CaptionFontInternal = value;
                    OnPropertyChanged("CaptionFont");
                }
            }
        }

        [Category("Style"), DefaultValue(ControlLayout.Visual),DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden)]
        public virtual ControlLayout ControlsLayout
        {
            get { return m_ControlLayout; }
            set
            {
                    m_ControlLayout = value;
            }
        }

		internal Color DisabledColor
		{
			get {return Layout.DisableColorInternal;}
		}

		internal Color ReadOnlyColor
		{
			get {return Layout.BackColorInternal ;}
		}

		internal Color NegativeNumberColor
		{
			get {return Color.Red ;}
		}

		internal Color FlashColor
		{
			get {return SystemColors.Info ;}
		}

		internal Color FlatBackColor
		{
			get 
			{
				return Layout.BackgroundColorInternal;
			}
		}

		#endregion

		#region Static Methods

		public static StyleLayout GetNewStyleLayout(Styles style)
		{
			StyleLayout sl=new StyleLayout (style);
			//sl.SetStyleColors (style);
			return sl;
		}

		public static Color InteliBorderColor(bool hot, Color borderHotColor, Color borderColor)
		{
			if(hot)
			{
				return borderHotColor;
			}
			else
			{
				return borderColor;
			}
		}

		public static Color InteliButtonColor(bool hot,bool pressed, Color buttonColor, Color ButtonClickColor, Color buttonHotColor)
		{
			if(hot)
			{
				if(pressed)
				{
					return ButtonClickColor;
				}
				else
				{
					return buttonHotColor;
				}
			}
			else
			{
				return buttonColor;
			}
		}


		public static Color InteliEditColor(bool editReadOnly,bool enabled, Color editColor, Color ReadOnlyColor, Color DisabledColor)
		{
			if(!editReadOnly && enabled)
			{
				return editColor;
			}
			else
			{
				if(editReadOnly && enabled)
				{
					return ReadOnlyColor;
				}
				else
				{ 
					return DisabledColor;
				}
			}
		}

		public static Color InteliEditColor(bool editReadOnly,bool enabled, bool flash, Color editColor, Color flashColor, Color ReadOnlyColor, Color DisabledColor)
		{
			if(!editReadOnly && enabled)
			{
				if (!flash) return editColor;
				else return flashColor;
			}
			else
			{
				if(editReadOnly && enabled)
				{
					return ReadOnlyColor;
				}
				else
				{ 
					return DisabledColor;
				}
			}
		}

		#endregion

	}

	#endregion

	#region StyleEdit
	
	/// <summary>
	/// Summary description for StyleEdit.
	/// </summary>
	/// 
	[DesignTimeVisible(true),ToolboxItem(false),ToolboxBitmap (typeof(StyleEdit),"Toolbox.StyleGuide.bmp"),Designer(typeof(Design.PainterDesigner))]
	public class StyleEdit :StyleBase,IStyleEdit
	{

		#region User Defined Variables

		public StyleEdit():base(){}
		public StyleEdit(System.ComponentModel.IContainer container):base(container){}

		//[Browsable(false)]
		public override PainterTypes PainterType
		{
			get{return PainterTypes.Edit;}
		}

		#endregion

		#region Properties

		[Category("EditColor"),DefaultValue(typeof(System.Drawing.Color),"Blue")]
		public Color BorderHotColor
		{
			get {return Layout.BorderHotColorInternal;}
			set
			{
				if(value!=Color.Empty )
				{
					Layout.BorderHotColorInternal=value;
					OnPropertyChanged("BorderHotColor");
				}
			}
		}

		[Category("EditColor"),DefaultValue(typeof(System.Drawing.Color),"Gold")]
		public Color FocusedColor
		{
			get {return Layout.FocusedColorInternal;}
			set
			{
				if(value!=Color.Empty )
				{
					Layout.FocusedColorInternal=value;
					OnPropertyChanged("FocusedColor");
				}
			}
		}
		#endregion

	}

	#endregion

	#region StyleContainer
	
	/// <summary>
	/// Summary description for StyleContainer.
	/// </summary>
	/// 
	[DesignTimeVisible(true),ToolboxItem(false),ToolboxBitmap (typeof(StyleContainer),"Toolbox.StyleGuide.bmp"),Designer(typeof(Design.PainterDesigner))]
	public class StyleContainer :StyleBase,IStyleContainer
	{

		#region User Defined Variables

		public StyleContainer():base(){}
		public StyleContainer(System.ComponentModel.IContainer container):base(container){}

		//[Browsable(false)]
		public override PainterTypes PainterType
		{
			get{return PainterTypes.Flat;}
		}

		#endregion

		#region Properties

		[Category("BackgroundColor"),DefaultValue(typeof(System.Drawing.Color),"Control")]
		public virtual Color BackgroundColor
		{
			get {return Layout.BackgroundColorInternal;}
			set
			{
				if(Layout.BackgroundColorInternal!=value)
				{
					Layout.BackgroundColorInternal=value;
					OnPropertyChanged("BackgroundColor");
				}
			}
		}		
		#endregion

	}

	#endregion

	#region StyleButton
	
	/// <summary>
	/// Summary description for StyleDesigner.
	/// </summary>
	/// 
	[DesignTimeVisible(true),ToolboxItem(false),ToolboxBitmap (typeof(StyleButton),"Toolbox.StyleGuide.bmp"),Designer(typeof(Design.PainterDesigner))]
	public class StyleButton :StyleBase,IStyleButton
	{

		#region User Defined Variables

		public StyleButton():base(){}
		public StyleButton(System.ComponentModel.IContainer container):base(container){}
	
		//[Browsable(false)]
		public override PainterTypes PainterType
		{
			get{return PainterTypes.Button;}
		}

		#endregion

		#region Properties

		[DefaultValue(false)]
		[RefreshProperties(RefreshProperties.All )] 
		internal override bool IsFlat
		{
			get {return Layout.IsFlatInternal;}
			set
			{
				if(Layout.IsFlatInternal !=value)
				{
					Layout.IsFlatInternal=value;
					OnPropertyChanged("IsFlat");
				}
			}
		}


		[Category("BackgroundColor"),DefaultValue(typeof(System.Drawing.Color),"Control")]
		public Color BackgroundColor
		{
			get {return Layout.BackgroundColorInternal;}
			set
			{
				if(Layout.BackgroundColorInternal!=value && value !=Color.Empty)
				{
					Layout.SetFlatColor(value); 
					OnPropertyChanged("BackgroundColor");
				}
			}
		}

		//[RefreshProperties(RefreshProperties.All )]
		[Category("BackgroundColor"),DefaultValue(FlatLayout.Light)]
		public FlatLayout FlatLayout
		{
			get {return Layout.FlatLayoutInternal;}
			set
			{
				if(Layout.FlatLayoutInternal!=value )
				{
					Layout.FlatLayoutInternal=value;
					Layout.SetFlatColor(Layout.BackgroundColorInternal); 
					OnPropertyChanged("FlatLayout");
				}
			}
		}

		[Category("BackgroundColor"),DefaultValue(typeof(System.Drawing.Color ),"LightSteelBlue")]    
		public Color ColorBrush1
		{
			get {return Layout.ColorBrush1Internal;}
			set
			{
				if(value !=Color.Empty)
				{
					Layout.ColorBrush1Internal=value;
					OnPropertyChanged("ColorBrush1");
				}
			}
		}

		[Category("BackgroundColor"),DefaultValue(typeof(System.Drawing.Color ),"AliceBlue")]    
		public Color ColorBrush2
		{
			get {return Layout.ColorBrush2Internal;}
			set
			{
				if(value !=Color.Empty)
				{
					Layout.ColorBrush2Internal=value;
					OnPropertyChanged("ColorBrush2");
				}
			}
		}

        [Category("BackgroundColor"), DefaultValue(typeof(System.Drawing.Color), "LightSteelBlue")]
        public Color ColorBrushLower
        {
            get { return Layout.ColorBrushLowerInternal; }
            set
            {
                if (value != Color.Empty)
                {
                    Layout.ColorBrushLowerInternal = value;
                    OnPropertyChanged("ColorBrushLower");
                }
            }
        }

        [Category("BackgroundColor"), DefaultValue(typeof(System.Drawing.Color), "AliceBlue")]
        public Color ColorBrushUpper
        {
            get { return Layout.ColorBrushUpperInternal; }
            set
            {
                if (value != Color.Empty)
                {
                    Layout.ColorBrushUpperInternal = value;
                    OnPropertyChanged("ColorBrushUpper");
                }
            }
        }
		[Category("EditColor"),DefaultValue(typeof(System.Drawing.Color),"Blue")]
		public Color BorderHotColor
		{
			get {return Layout.BorderHotColorInternal;}
			set
			{
				if(value !=Color.Empty)
				{
					Layout.BorderHotColorInternal=value;
					OnPropertyChanged("BorderHotColor");
				}
			}
		}
		
		[Category("EditColor"),DefaultValue(typeof(System.Drawing.Color),"Gold")]
		public Color FocusedColor
		{
			get {return Layout.FocusedColorInternal;}
			set
			{
				if(value !=Color.Empty)
				{
					Layout.FocusedColorInternal=value;
					OnPropertyChanged("FocusedColor");
				}
			}
		}
        [Category("BorderColor"), DefaultValue(typeof(System.Drawing.Color), "Navy")]
        public Color ButtonBorderColor
        {
            get { return Layout.ButtonBorderColorInternal; }
            set
            {
                if (value != Color.Empty)
                {
                    Layout.ButtonBorderColorInternal = value;
                    OnPropertyChanged("ButtonBorderColor");
                }
            }
        }
		#endregion

	}

	#endregion

	#region StyleGuide

	[DesignTimeVisible(true),ToolboxItem(true),ToolboxBitmap (typeof(StyleGuide),"Toolbox.StyleGuide.bmp"),Designer(typeof(Design.PainterDesigner))]
	public class StyleGuide :StyleBase,IStyleGuide
	{

		#region Constructors

		public StyleGuide():base(){}
		public StyleGuide(System.ComponentModel.IContainer container):base(container){}

		public StyleGuide(System.ComponentModel.IContainer container,Form form):base(container,form)
		{
			//m_Form=form;  
		}

		//[Browsable(false)]
		public override PainterTypes PainterType
		{
			get{return PainterTypes.Guide;}
		}


		#endregion

		#region Color Property

		[Category("BorderColor"),DefaultValue(typeof(Color ),"Blue")]    
		public Color BorderHotColor
		{
			get {return m_StyleLayout.BorderHotColorInternal;}
			set
			{
				if(m_StyleLayout.BorderHotColorInternal!=value)
				{
					m_StyleLayout.BorderHotColorInternal=value;
					OnPropertyChanged("BorderHotColor");
				}
			}
		}

		[Category("EditColor"),DefaultValue(typeof(Color ),"Gold")]    
		public Color FocusedColor
		{
			get {return m_StyleLayout.FocusedColorInternal;}
			set
			{
				if(m_StyleLayout.FocusedColorInternal!=value)
				{
					m_StyleLayout.FocusedColorInternal=value;
					OnPropertyChanged("FocusedColor");
				}
			}
		}

		[Category("BackgroundColor"),DefaultValue(typeof(Color ),"Control")]    
		public Color BackgroundColor
		{
			get {return m_StyleLayout.BackgroundColorInternal;}
			set
			{
				if(m_StyleLayout.BackgroundColorInternal!=value)
				{
					m_StyleLayout.BackgroundColorInternal=value;
					OnPropertyChanged("BackgroundColor");
				}
			}
		}

		[Category("BackgroundColor"),DefaultValue(typeof(Color ),"LightSteelBlue")]    
		public Color ColorBrush1
		{
			get {return m_StyleLayout.ColorBrush1Internal;}
			set
			{
				if(m_StyleLayout.ColorBrush1Internal!=value)
				{
					m_StyleLayout.ColorBrush1Internal=value;
					OnPropertyChanged("ColorBrush1");
				}
			}
		}

		[Category("BackgroundColor"),DefaultValue(typeof(Color ),"AliceBlue")]    
		public Color ColorBrush2
		{
			get {return m_StyleLayout.ColorBrush2Internal;}
			set
			{
				if(m_StyleLayout.ColorBrush2Internal!=value)
				{
					m_StyleLayout.ColorBrush2Internal=value;
					OnPropertyChanged("ColorBrush2");
				}
			}
		}

        [Category("BackgroundColor"), DefaultValue(typeof(Color), "LightSteelBlue")]
        public Color ColorBrushLower
        {
            get { return m_StyleLayout.ColorBrushLowerInternal; }
            set
            {
                if (m_StyleLayout.ColorBrushLowerInternal != value)
                {
                    m_StyleLayout.ColorBrushLowerInternal = value;
                    OnPropertyChanged("ColorBrushLower");
                }
            }
        }

        [Category("BackgroundColor"), DefaultValue(typeof(Color), "AliceBlue")]
        public Color ColorBrushUpper
        {
            get { return m_StyleLayout.ColorBrushUpperInternal; }
            set
            {
                if (m_StyleLayout.ColorBrushUpperInternal != value)
                {
                    m_StyleLayout.ColorBrushUpperInternal = value;
                    OnPropertyChanged("ColorBrushUpper");
                }
            }
        }
		[Category("GridColor"),DefaultValue(typeof(Color ),"Navy")]    
		public Color CaptionColor
		{
			get {return m_StyleLayout.CaptionColorInternal;}
			set
			{
				if(m_StyleLayout.CaptionColorInternal!=value)
				{
					m_StyleLayout.CaptionColorInternal=value;
                    if (StylePlan == Styles.Custom)
                    {
                        m_StyleLayout.CaptionLightColorInternal = m_StyleLayout.LightLightColor;
                    }
					OnPropertyChanged("CaptionColor");
				}
			}
		}

		[Category("GridColor"),DefaultValue(typeof(Color ),"216, 225, 239")]    
		public Color AlternatingColor
		{
			get {return m_StyleLayout.AlternatingColorInternal;}
			set
			{
				if(m_StyleLayout.AlternatingColorInternal!=value)
				{
					m_StyleLayout.AlternatingColorInternal=value;
					OnPropertyChanged("AlternatingColor");
				}
			}
		}

		[Category("EditColor"),DefaultValue(typeof(Color ),"Control")]    
		public Color DisableColor
		{
			get {return m_StyleLayout.DisableColorInternal;}
			set
			{
				if(m_StyleLayout.DisableColorInternal!=value)
				{
					m_StyleLayout.DisableColorInternal=value;
					OnPropertyChanged("DisableColor");
				}
			}
		}
        [Category("BorderColor"), DefaultValue(typeof(System.Drawing.Color), "Navy")]
        public Color ButtonBorderColor
        {
            get { return Layout.ButtonBorderColorInternal; }
            set
            {
                if (value != Color.Empty)
                {
                    Layout.ButtonBorderColorInternal = value;
                    OnPropertyChanged("ButtonBorderColor");
                }
            }
        }
		[Category("ExtraColor"),DefaultValue(typeof(Color ),"192, 192, 255")]    
		public Color HighlightColor
		{
			get {return m_StyleLayout.HighlightColorInternal;}
		}

		[Category("ExtraColor")]
		public Color ContentColor
		{
			get {return m_StyleLayout.ContentColorInternal;}
		}
		[Category("ExtraColor")]
		public Color SelectedColor
		{
			get {return m_StyleLayout.SelectedColorInternal;}
		}

		#endregion

		#region Style Properties

		//[System.ComponentModel.RefreshProperties(RefreshProperties.All )]
		[Category("Style")]
		public override Styles StylePlan
		{
			get {return base.StylePlan;}
			set
			{

				base.StylePlan=value;
				SetFormColor();
				//SetStyleLayout();
			}
		}

		#endregion

		#region FormProperty

//		//[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Localizable(true)]
//		[Category("Form")]//,DefaultValue(null)]
//		public Form CurrentForm
//		{
//			get{return m_Form;} 
//			set{
//				m_Form=value;
//				if(m_Form==null)
//				{
//                  //m_Form=Form.ActiveForm;
//				}
//				if(m_Form!=null)
//				{
//                  m_Form.BackColor=this.FormColor;
//				  m_Form.Invalidate(); 
//				}
//			}
//		}

		[Category("Form"),DefaultValue(typeof(System.Drawing.Color ),"Control")]    
		public Color FormColor
		{
			get 
			{
				if(FormColorInternal.IsEmpty)
				{
                   FormColorInternal=ColorBrush2;
				}
				return FormColorInternal;
			}
			set
			{
				if(value.IsEmpty)
				{
					value=ColorBrush2;
				}
				if(FormColorInternal!=value)
				{
					FormColorInternal=value;
					SetFormColor();
					OnPropertyChanged("FormColor");
				}
			}
		}

		private void SetFormColor ()
		{
			m_Form=GetActiveDesignForm();
			if(m_Form!=null)
			{
				m_Form.BackColor =FormColorInternal;
			}
		}
		#endregion

	}
	#endregion

	#region StyleGrid

	//[System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand, UnmanagedCode=true)]
	[DesignTimeVisible(true),ToolboxItem(false),ToolboxBitmap (typeof(StyleGrid),"Toolbox.StyleGuide.bmp"),Designer(typeof(Design.PainterDesigner))]
	public class StyleGrid:StyleBase,IStyleGrid
	{

		#region User Defined Variables

		public StyleGrid():base(){}
		public StyleGrid(System.ComponentModel.IContainer container):base(container){}
	
		//[Browsable(false)]
		public override PainterTypes PainterType
		{
			get{return PainterTypes.Grid;}
		}

		#endregion

		#region Properties

		/// <summary>
		/// BorderHotColor
		/// </summary>
		public Color BorderHotColor
		{
			get {return Layout.BorderHotColorInternal;}
			set
			{
				Layout.BorderHotColorInternal=value;
				//ctl.BorderHotColor=value;
				OnPropertyChanged("BorderHotColor");
			}
		}

        /// <summary>
        /// CaptionColor
        /// </summary>
        public Color CaptionLightColor
        {
            get { return Layout.CaptionLightColorInternal; }
            set
            {
                Layout.CaptionLightColorInternal = value;
                //ctl.CaptionBackColor=value;
                OnPropertyChanged("CaptionBackColor");
            }
        }

		/// <summary>
		/// CaptionColor
		/// </summary>
		public Color CaptionBackColor
		{
			get {return Layout.CaptionColorInternal;}
			set
			{
				Layout.CaptionColorInternal=value;
				//ctl.CaptionBackColor=value;
				OnPropertyChanged("CaptionBackColor");
			}
		}

		/// <summary>
		/// ColorBrush2
		/// </summary>
		public Color CaptionForeColor
		{
			get {return Layout.CaptionTextColorInternal;}
			set
			{
                //Layout.ColorBrush2Internal = value;
                Layout.CaptionTextColorInternal = value;
				//ctl.CaptionForeColor=value;
				OnPropertyChanged("CaptionForeColor");
			}
		}

		/// <summary>
		/// AlternatingColor
		/// </summary>
		public Color AlternatingColor
		{
			get {return Layout.AlternatingColorInternal;}
			set
			{
				Layout.AlternatingColorInternal=value;
				//ctl.CaptionForeColor=value;
				OnPropertyChanged("AlternatingColor");
			}
		}

		/// <summary>
		/// BorderColor
		/// </summary>
		public Color HeaderBackColor
		{
			get {return Layout.HeaderColorInternal;}
	        set
            {
                //Layout.ColorBrush2Internal = value;
                Layout.HeaderColorInternal = value;
                //ctl.CaptionForeColor=value;
                OnPropertyChanged("HeaderBackColor");
            }

		}

		/// <summary>
		/// LightLightColor
		/// </summary>
		public Color HeaderForeColor
		{
            get { return Layout.HeaderTextColorInternal; }
            set
            {
                Layout.HeaderTextColorInternal = value;
                //ctl.CaptionForeColor=value;
                OnPropertyChanged("HeaderForeColor");
            }

		}

		/// <summary>
		/// DarkColor
		/// </summary>
		public Color SelectionBackColor
		{
			get{ return Layout.CaptionColorInternal;} //.BorderColorInternal;}//.DarkColor;}
		}

		/// <summary>
		/// BackColor
		/// </summary>
		public Color SelectionForeColor
		{
			get {return Layout.BackColorInternal;}
		}

//		/// <summary>
//		/// BackColor
//		/// </summary>
//		public Color GridBackColor
//		{
//			get {return Layout.BackColorInternal;}
//			set
//			{
//				Layout.BackColorInternal=value;
//				//ctl.GridBackColor=value;
//				OnPropertyChanged("BackColor");
//			}
//		}
//
//		/// <summary>
//		/// ForeColor
//		/// </summary>
//		public Color GridForeColor
//		{
//			get {return Layout.ForeColorInternal;}
//			set
//			{
//				Layout.ForeColorInternal=value;
//				//ctl.GridForeColor=value;
//				OnPropertyChanged("ForeColor");
//			}
//		}

//		public Color ColorBrush1
//		{
//			get {return Layout.ColorBrush1Internal;}
//		}
//		public Color ColorBrush2
//		{
//			get {return Layout.ColorBrush2Internal;}
//		}
//		public Color BackgroundColor
//		{
//			get {return Layout.BackgroundColorInternal;}
//		}
//		public Color FocusedColor
//		{
//			get {return Layout.FocusedColorInternal;}
//		}

		#endregion
        
	}

	#endregion
}

