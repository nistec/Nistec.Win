using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Text;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using Nistec.Drawing;
using Nistec.Win32;
using Nistec.WinForms.Controls;

namespace Nistec.WinForms
{

	#region ButtonBase

	[System.ComponentModel.ToolboxItem(false)]
    public class McButtonBase : McControl, ILayout, IButtonControl, IButton
	{
		#region Members

		private System.ComponentModel.Container components = null;

        public const int LeftMargin = 7;
        public const int TextMargin = 7;
        protected const int minHeight = 13;


		//protected GraphicsPath		path;
		private Rectangle			bounds;
		//private StringFormat		sf;
		//private Point				iPoint, tPoint;
		private DialogResult		dialogResult;
        private string              toolTipText;
        private bool                autoToolTip;
        // /*toolTip*/ 
        private McToolTip     toolTip;
        private bool                showToolTip;

        private System.Drawing.ContentAlignment m_TextAlign;
        private System.Drawing.ContentAlignment m_ImageAlign;

        internal bool m_MouseDown;
        internal bool isDefault;
        internal ButtonStates state;
        internal Image m_Image;

		private int						m_ImageIndex;
		private ImageList				m_ImageList;
		private ControlLayout			m_ControlLayout;


 		#endregion

		#region Constructor

         public McButtonBase()
		{
			try
			{
				this.SetStyle(ControlStyles.DoubleBuffer, true);
				this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
				this.SetStyle(ControlStyles.UserPaint, true);
				this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
				this.SetStyle(ControlStyles.StandardDoubleClick, false);
				this.SetStyle(ControlStyles.Selectable, true);
				this.Size = new Size(80, 20);
				this.ResizeRedraw = true;
                this.toolTipText = null;
                this.autoToolTip = true;
                this.showToolTip = true;
                m_ControlLayout = ControlLayout.XpLayout;
                m_ImageIndex = -1;

                m_TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                m_ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
                state = ButtonStates.Normal;
                dialogResult = DialogResult.None;
                isDefault = false;

			}
			catch{}

		}

        //internal McButtonBase(bool net)
        //    : this()
        //{
        //    this.m_netFram = net;
        //}

		#endregion

   		#region Overrides

		protected override void OnEnabledChanged(System.EventArgs e)
		{
			base.OnEnabledChanged (e);
			this.Invalidate();//(bounds);
		}

		protected override void OnParentBackColorChanged(EventArgs e)
		{
			base.OnParentBackColorChanged (e);
			this.Invalidate ();
		}

        private void DetachImageList(object sender, EventArgs e)
        {
            this.ImageList = null;
        }

        private void ImageListRecreateHandle(object sender, EventArgs e)
        {
            if (base.IsHandleCreated)
            {
                base.Invalidate();
            }
        }
		#endregion

        #region Paint background

        //		protected override void OnPaintBackground(PaintEventArgs pevent)
        //		{
        //			base.OnPaintBackground (pevent);
        //			//PaintBackground(pevent,this.bounds);//ClientRectangle);
        //
        //		}

        internal protected void PaintButtonBackground(PaintEventArgs e, Rectangle bounds, Brush background)
        {
            if (background == null)
            {
                this.PaintBackground(e, bounds);
            }
            else
            {
                e.Graphics.FillRectangle(background, bounds);
            }
        }

        internal protected void PaintBackground(PaintEventArgs e, Rectangle rectangle)
        {
            if (this.RenderTransparent)
            {
                this.PaintTransparentBackground(e, rectangle);
            }
            if ((this.BackgroundImage != null) && !SystemInformation.HighContrast)
            {
                TextureBrush brush1 = new TextureBrush(this.BackgroundImage, WrapMode.Tile);
                try
                {
                    Matrix matrix1 = brush1.Transform;
                    matrix1.Translate((float)this.DisplayRectangle.X, (float)this.DisplayRectangle.Y);
                    brush1.Transform = matrix1;
                    e.Graphics.FillRectangle(brush1, rectangle);
                    return;
                }
                finally
                {
                    brush1.Dispose();
                }
            }
            Color color1 = this.BackColor;
            bool flag1 = false;
            if ((color1.A == 0xff))//&& (e.Graphics.GetHdc() != IntPtr.Zero)) //&& (this.BitsPerPixel > 8))
            {
                Win32.RECT rect1 = new Win32.RECT(rectangle.X, rectangle.Y, rectangle.Right, rectangle.Bottom);
                //Win32.WinAPI.FillRect (new HandleRef(e, e.HDC), ref rect1, new HandleRef(this, this.BackBrush));
                Win32.WinAPI.FillRect((IntPtr)new HandleRef(e, this.Handle), ref rect1, (IntPtr)new HandleRef(this, this.BackBrush));
                flag1 = true;
            }
            if (!flag1 && (color1.A > 0))
            {
                if (color1.A == 0xff)
                {
                    color1 = e.Graphics.GetNearestColor(color1);
                }
                using (Brush brush2 = new SolidBrush(color1))
                {
                    e.Graphics.FillRectangle(brush2, rectangle);
                }
            }
        }

        internal protected void PaintTransparentBackground(PaintEventArgs e, Rectangle rectangle)
        {
            Graphics graphics1 = e.Graphics;
            Control control1 = this.Parent;
            if (control1 != null)
            {
                int num1;
                WinMethods.POINT point1 = new WinMethods.POINT();
                point1.y = num1 = 0;
                point1.x = num1;
                WinMethods.MapWindowPoints(new HandleRef(this, this.Handle), new HandleRef(control1, control1.Handle), point1, 1);
                rectangle.Offset(point1.x, point1.y);
                PaintEventArgs args1 = new PaintEventArgs(graphics1, rectangle);
                GraphicsState state1 = graphics1.Save();
                try
                {
                    graphics1.TranslateTransform((float)-point1.x, (float)-point1.y);
                    this.InvokePaintBackground(control1, args1);
                    graphics1.Restore(state1);
                    state1 = graphics1.Save();
                    graphics1.TranslateTransform((float)-point1.x, (float)-point1.y);
                    this.InvokePaint(control1, args1);
                    return;
                }
                finally
                {
                    graphics1.Restore(state1);
                }
            }
            graphics1.FillRectangle(SystemBrushes.Control, rectangle);
        }

        internal bool RenderTransparent
        {
            get
            {
                if (this.GetStyle(ControlStyles.SupportsTransparentBackColor))
                {
                    return true;//(this.BackColor.A < 0xff);
                }
                return false;
            }
        }

        private IntPtr BackBrush
        {
            get
            {
                IntPtr ptr1;
                //				object obj1 = this.Properties.GetObject(Control.PropBackBrush);
                //				if (obj1 != null)
                //				{
                //					return (IntPtr) obj1;
                //				}
                Color color1 = this.BackColor;
                if ((this.Parent != null) && (this.Parent.BackColor == this.BackColor))
                {
                    ptr1 = Win32.WinAPI.CreateSolidBrush(ColorTranslator.ToWin32(color1));
                    //return this.Parent.BackBrush;
                }
                else if (ColorTranslator.ToOle(color1) < 0)
                {
                    ptr1 = Win32.WinAPI.GetSysColorBrush(ColorTranslator.ToOle(color1) & 0xff);
                    //this.SetState(0x200000, false);
                }
                else
                {
                    ptr1 = Win32.WinAPI.CreateSolidBrush(ColorTranslator.ToWin32(color1));
                    //this.SetState(0x200000, true);
                }
                //this.Properties.SetObject(Control.PropBackBrush, ptr1);
                return ptr1;
            }
        }
        #endregion

		#region ILayout

		protected IStyle				m_StylePainter;
  
		[Browsable(false)]
		public PainterTypes PainterType
		{
			get{return PainterTypes.Button;}
		}

		[Category("Style"),DefaultValue(null),RefreshProperties(RefreshProperties.All)]
		public IStyle StylePainter
		{
			get {return m_StylePainter;}
			set {
				if(m_StylePainter!=value)
				{
					if (this.m_StylePainter != null)
						this.m_StylePainter.PropertyChanged -=new PropertyChangedEventHandler(m_Style_PropertyChanged); 
					m_StylePainter = value;
					if (this.m_StylePainter != null)
						this.m_StylePainter.PropertyChanged +=new PropertyChangedEventHandler(m_Style_PropertyChanged); 
					OnStylePainterChanged(EventArgs.Empty);
					this.Invalidate();
				}
			}
		}

		[Browsable(false)]
		public virtual IStyleLayout LayoutManager
		{
			get
			{
				if(this.m_StylePainter!=null)
					return this.m_StylePainter.Layout as IStyleLayout;
				else
					return StyleLayout.DefaultLayout as IStyleLayout;// this.m_Style as IStyleLayout;

			}
		}

		public virtual void SetStyleLayout(StyleLayout value)
		{
			if(this.m_StylePainter!=null)
				this.m_StylePainter.Layout.SetStyleLayout(value); 
		}

		public virtual void SetStyleLayout(Styles value)
		{
			if(this.m_StylePainter!=null)
				m_StylePainter.Layout.SetStyleLayout(value);
		}

		protected virtual void OnStylePainterChanged(EventArgs e)
		{
			//
		}

		protected virtual void OnStylePropertyChanged(PropertyChangedEventArgs e)
		{
			if((DesignMode || IsHandleCreated))
				this.Invalidate();
		}

		private void m_Style_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{

			OnStylePropertyChanged(e);
		}

		#endregion

        #region Properties

		[Browsable(false)]
        public ButtonStates ButtonState
		{
            get { return state; }
		}

		[Category("Appearance")]
		public override string Text
		{
			get {return base.Text;}

			set
			{ 
				base.Text = value; 
				this.Invalidate();
			}
		}

		[Browsable(false)]
		public bool IsMouseHover
		{
			get
			{
				try
				{
					Point mPos  = Control.MousePosition;
					bool retVal = this.ClientRectangle.Contains(this.PointToClient(mPos));
					return retVal;
				}
				catch{return false;}
			}
		}

		[Category("Appearance"),DefaultValue(null)]
		//System.ComponentModel.RefreshProperties(RefreshProperties.Repaint)]
		public virtual Image Image
		{
			get	{ return m_Image; }
			set
			{
				if(m_Image != value)
				{
					m_Image = value;
					this.Invalidate();
				}
			}
		}

        [Category("Behavior"), DefaultValue(true)]
        public virtual bool AutoToolTip
        {
            get { return autoToolTip; }
            set
            {
                autoToolTip = value;
            }
        }
        
       [Category("Behavior"), DefaultValue(true)]
        public virtual bool ShowToolTip
        {
            get { return showToolTip; }
            set
            {
                if (showToolTip != value)
                {
                    showToolTip = value;
                    /*toolTip*/
                    if (value && (this.toolTip == null))
                    {
                        this.toolTip = new McToolTip();
                    }
                    base.Invalidate();
                }
            }
        }
 
        [Category("Behavior"), DefaultValue(null), Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor)), Localizable(true), Description("ToolTipText")]
        public string ToolTipText
        {
            get
            {

                if (!this.AutoToolTip || !string.IsNullOrEmpty(this.toolTipText))
                {
                    return this.toolTipText;
                }
                string text = this.Text;
                if (McToolTip.ContainsMnemonic(text))
                {
                    text = string.Join("", text.Split(new char[] { '&' }));
                }
                return text;
            }
            set
            {
                this.toolTipText = value;
            }
        }

  
 		[Category("Appearance"),DefaultValue(System.Drawing.ContentAlignment.MiddleCenter)]
		public virtual System.Drawing.ContentAlignment TextAlign
		{
			get{ return m_TextAlign; }

			set
			{
				if(m_TextAlign != value)
				{
					m_TextAlign = value;
					this.Invalidate();
				}
			}
		}

		[Category("Appearance"),DefaultValue(System.Drawing.ContentAlignment.MiddleCenter)]
		public virtual System.Drawing.ContentAlignment ImageAlign
		{
			get{ return m_ImageAlign; }

			set
			{
				if(m_ImageAlign != value)
				{
					m_ImageAlign = value;
					this.Invalidate();
				}
			}
		}

		//[Description("ButtonImageIndex"), Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor)), Category("Appearance"), DefaultValue(-1), Localizable(true), TypeConverter(typeof(ImageIndexConverter))]
		[Description("ButtonImageIndex"), Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design", typeof(System.Drawing.Design.UITypeEditor)), Category("Appearance"), DefaultValue(-1), Localizable(true), TypeConverter(typeof(ImageIndexConverter))]
		public int ImageIndex
		{
			get
			{
				if (((this.m_ImageIndex != -1) && (this.m_ImageList != null)) && (this.m_ImageIndex >= this.m_ImageList.Images.Count))
				{
					return (this.m_ImageList.Images.Count - 1);
				}
				return this.m_ImageIndex;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentException("InvalidLowBoundArgumentEx");
				}
				if (this.m_ImageIndex != value)
				{
					if (value != -1)
					{
						this.m_Image = null;
					}
					this.m_ImageIndex = value;
					base.Invalidate();
				}
			}
		}
		[Description("ButtonImageList"), DefaultValue((string) null), Category("Appearance")]
		public ImageList ImageList
		{
			get
			{
				return this.m_ImageList;
			}
			set
			{
				if (this.m_ImageList != value)
				{
					EventHandler handler1 = new EventHandler(this.ImageListRecreateHandle);
					EventHandler handler2 = new EventHandler(this.DetachImageList);
					if (this.m_ImageList != null)
					{
						this.m_ImageList.RecreateHandle -= handler1;
						this.m_ImageList.Disposed -= handler2;
					}
                    //if (value != null)
                    //{
                    //    this.m_Image = null;
                    //}
					this.m_ImageList = value;
					if (value != null)
					{
						value.RecreateHandle += handler1;
						value.Disposed += handler2;
					}
					base.Invalidate();
				}
			}
		}

        [Category("Style"), DefaultValue(ControlLayout.XpLayout)]
		public virtual ControlLayout ControlLayout 
		{
			get {return m_ControlLayout;}
			set
			{
                if (m_ControlLayout != value)
                {
                    m_ControlLayout = value;
                    //CreatePainer(value);
                    this.Invalidate();
                }
			}

		}


		#endregion

		#region Protected Methods
    
		protected override void Dispose(bool disposing)
		{
			if( disposing )
			{
				DisposePensBrushes();


                if (this.m_ImageList != null)
                {
                    this.m_ImageList.Disposed -= new EventHandler(this.DetachImageList);
                }
                /*toolTip*/
                if (this.toolTip != null)
                {
                    this.toolTip.Dispose();
                    this.toolTip = null;
                }
				if(components != null)
				{	
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		
		protected override void OnMouseMove(MouseEventArgs e)
		{
            if (state != ButtonStates.Pushed)
            {
                if (!bounds.Contains(e.X, e.Y))
                    state = ButtonStates.Normal;
                else
                    state = ButtonStates.MouseOver;
                this.Invalidate(bounds);
            }
			base.OnMouseMove(e);
		}
	
        protected override void OnMouseEnter(EventArgs e)
		{
			state = ButtonStates.MouseOver;
			this.Invalidate(bounds);

            if ((!base.DesignMode /*&& this.AutoEllipsis*/) && (this.ShowToolTip && (this.toolTip != null)))
            {
                /*toolTip*/
                 this.toolTip.Show(ToolTipText, this);
                 //McToolTip.Instance.Show(ToolTipText, this); 
            }

            base.OnMouseEnter(e);
  		}

		protected override void OnMouseLeave(EventArgs e)
		{
			state = ButtonStates.Normal;
            //McToolTip.Instance.Hide(this);
            /*toolTip*/
            if (this.toolTip != null)
            {
                this.toolTip.Hide(this);
            }

            this.Invalidate();
            base.OnMouseLeave(e);
		}

		protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) != MouseButtons.Left) return;

			if (bounds.Contains(e.X, e.Y))
			{
				state = ButtonStates.Pushed;
				this.Focus();
			} 
			else state = ButtonStates.Normal;
            this.m_MouseDown = true;
            this.Invalidate(bounds);
            base.OnMouseDown(e);
		}

		protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
				state = ButtonStates.Normal;
            this.m_MouseDown = false;
            this.Invalidate(bounds);
			base.OnMouseUp(e);
		}

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            /*toolTip*/
            if (showToolTip && (this.toolTip == null))
            {
                this.toolTip = new McToolTip();
            }
        }

 		protected override void OnEnter(System.EventArgs e)
		{
			this.Invalidate(bounds);
			base.OnEnter(e);
		}

		protected override void OnLeave(System.EventArgs e)
		{
			this.Invalidate(bounds);
			base.OnLeave(e);
		}

		protected override void OnClick(System.EventArgs e)
		{
            if (state == ButtonStates.Pushed)
			{
				state = ButtonStates.Normal;
				this.Invalidate(bounds);			
			}
			if (this.dialogResult != DialogResult.None)
			{
				Form form = (Form)this.FindForm();
				form.DialogResult = this.DialogResult;
			}
			base.OnClick(e);
		}

		protected override void OnKeyDown(System.Windows.Forms.KeyEventArgs e)
		{
			if(this.Enabled && (e.KeyData == Keys.Enter || e.KeyData == Keys.Space))
			{
				state=ButtonStates.Pushed; 
				this.Invalidate(bounds);
			}
			base.OnKeyDown(e);
		}

		protected override void OnKeyUp(System.Windows.Forms.KeyEventArgs e)
		{
			base.OnKeyUp(e);

			if(this.Enabled && (e.KeyData == Keys.Space || e.KeyData == Keys.Enter))
			{
				this.Invalidate(bounds);
				if(state==ButtonStates.Pushed)
					this.PerformClick();
			}
		}

		protected override bool ProcessMnemonic(char charCode)
		{
			if (Control.IsMnemonic(charCode, base.Text))
			{
				this.PerformClick();
				return true; 
			}
			return base.ProcessMnemonic(charCode); 
		} 

		protected override void OnSizeChanged(System.EventArgs e)
		{
			bounds = new Rectangle(0, 0, this.Width, this.Height);
			OnParentChanged(e);
			base.OnSizeChanged(e);
		}

		protected override void OnParentChanged(EventArgs e)
		{
			if (Parent == null) return;
			//GetPoints();
			CreateRegion();
			base.OnParentChanged(e);
		}

		protected override void OnTextChanged(EventArgs e) 
		{
            //if (sf != null) sf.Dispose();
            //sf = new StringFormat();
            //sf.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			//GetPoints();
			base.OnTextChanged(e);
		}

        //protected virtual void PaintXP(System.Windows.Forms.PaintEventArgs e)
        //{
        //    if (m_Image != null)
        //    {
        //        if (this.Enabled)
        //            e.Graphics.DrawImage(m_Image, iPoint); 
        //        else
        //            ControlPaint.DrawImageDisabled(e.Graphics, m_Image, iPoint.X, iPoint.Y, this.BackColor);
        //    }
        //    else if (m_ImageIndex != -1 && m_ImageIndex < this.ImageList.Images.Count)
        //    {
        //        if (this.Enabled)
        //            this.ImageList.Draw(e.Graphics, iPoint.X, iPoint.Y, m_ImageIndex);
        //        else
        //            ControlPaint.DrawImageDisabled(e.Graphics, this.ImageList.Images[m_ImageIndex], iPoint.X, iPoint.Y, this.BackColor);
        //    }

        //    if (Text != "")
        //    {
        //        e.Graphics.DrawString(Text, Font, LayoutManager.GetBrushText(), tPoint, sf);
        //    }
		
        //}

		#endregion

		#region Private Methods

        internal void PaintInternal(System.Windows.Forms.PaintEventArgs e)
        {
            //if (m_Image != null)
            //{
            //    if (this.Enabled)
            //        e.Graphics.DrawImage(m_Image, iPoint);
            //    else
            //        ControlPaint.DrawImageDisabled(e.Graphics, m_Image, iPoint.X, iPoint.Y, this.BackColor);
            //}
            //else if (m_ImageIndex != -1 && m_ImageIndex < this.ImageList.Images.Count)
            //{
            //    if (this.Enabled)
            //        this.ImageList.Draw(e.Graphics, iPoint.X, iPoint.Y, m_ImageIndex);
            //    else
            //        ControlPaint.DrawImageDisabled(e.Graphics, this.ImageList.Images[m_ImageIndex], iPoint.X, iPoint.Y, this.BackColor);
            //}

            //if (Text != "")
            //{
            //    e.Graphics.DrawString(Text, Font, LayoutManager.GetBrushText(), tPoint, sf);
            //}

        }

        //private void GetPoints()
        //{
        //    int X = this.Width, Y = this.Height;
			
        //    if (Image != null) 
        //    {
        //        if (Text.Length == 0)
        //            iPoint = new Point((X - Image.Width)/2,this.Top + ((Y - Image.Height)/2)); 
        //        else 
        //            iPoint = new Point(LeftMargin, ((Y - Image.Height)/2));
	
        //        Size size = TextUtils.GetTextSize(this.CreateGraphics(), Text.Replace("&",""), Font, new Size(X, Y));
        //        int SizeX=(LeftMargin + Image.Width + TextMargin);

        //        tPoint = new Point(SizeX - TextMargin + ((X-SizeX - size.Width)/2), (Y - this.Font.Height)/2);
        //        //tPoint = new Point(X-(BT.LeftMargin + Image.Width + BT.TextMargin), (Y - this.Font.Height)/2);
        //    }
        //    else 
        //    {
        //        Size size = TextUtils.GetTextSize(this.CreateGraphics(), Text.Replace("&",""), Font, new Size(X, Y));
        //        tPoint = new Point((X - size.Width - 2)/2, (Y - this.Font.Height)/2);					
        //    }	
        //}

		#endregion

		#region Virtual methods

  
		virtual protected void CreateRegion()
		{
		}
        virtual protected void CreatePensBrushes(Styles m_Style)
        {
        //    if (textBrush != null) textBrush.Dispose();
        //    textBrush = new SolidBrush(ForeColor);
        }
        virtual protected void DisposePensBrushes()
        {
           //if (textBrush != null) textBrush.Dispose();
        }

		#endregion

		#region Implementation of IButtonControl

		public void PerformClick()
		{
			if (base.CanSelect)
			{
				OnClick(EventArgs.Empty);
			}
		}

		public void NotifyDefault(bool value)
		{
			this.isDefault = value;
			this.Invalidate();
		}

		public System.Windows.Forms.DialogResult DialogResult
		{
			get
			{
				return this.dialogResult;
			}
			set
			{
				this.dialogResult = value;
			}
		}


		#endregion

 
 	}
	#endregion

	#region SupportButtonXP
	
	#region ColorManager

	public class ColorManager
	{

		#region Brushes (enable state)

		public static LinearGradientBrush Brush00(Rectangle rect)
		{
			return new LinearGradientBrush(rect
				, Color.FromArgb(64, 171, 168, 137), Color.FromArgb(92, 255, 255, 255), 85.0f);
		}

		public static LinearGradientBrush Brush01(Styles style, Rectangle rect)
		{
			switch(style)
			{
				case Styles.SeaGreen:
					return new LinearGradientBrush(rect
						, Color.FromArgb(255, 255, 246), Color.FromArgb(246, 243, 224), 90.0f);
				default:
					return new LinearGradientBrush(rect
						, Color.FromArgb(255, 255, 255), Color.FromArgb(240, 240, 234), 90.0f);
			}
		}

		public static LinearGradientBrush Brush02(Styles style, Rectangle rect)
		{
			switch(style)
			{
				case Styles.SeaGreen:
					return new LinearGradientBrush(rect
						, Color.FromArgb(177, 203, 128), Color.FromArgb(144, 193, 84), 90.0f);
				default:
					return new LinearGradientBrush(rect
						, Color.FromArgb(186, 211, 245), Color.FromArgb(137, 173, 228), 90.0f);
			}
		}

		public static LinearGradientBrush Brush03(Styles style, Rectangle rect)
		{
			switch(style)
			{
				case Styles.SeaGreen:
					return new LinearGradientBrush(rect
						, Color.FromArgb(237, 190, 150), Color.FromArgb(227, 145, 79), 90.0f);
				default:
					return new LinearGradientBrush(rect
						, Color.FromArgb(253, 216, 137), Color.FromArgb(248, 178, 48), 90.0f);
			}
		}

        //Corners color
		public static SolidBrush Brush04(Styles style)
		{
			switch (style)
			{
				case Styles.Silver:		return new SolidBrush(Color.FromArgb(92, 85, 125, 162));
				case Styles.SeaGreen:	return new SolidBrush(Color.FromArgb(92, 109, 138, 77));
                default:  return new SolidBrush(Color.FromArgb(92, 85, 125, 162));
			}
		}

		public static LinearGradientBrush Brush05(Styles style, Rectangle rect)
		{
			switch (style)
			{
				case Styles.SeaGreen:
					return new LinearGradientBrush(rect
						, Color.FromArgb(238, 230, 210), Color.FromArgb(236, 228, 206), 90.0f);
				default:
					return new LinearGradientBrush(rect
						, Color.FromArgb(229, 228, 221), Color.FromArgb(226, 226, 218), 90.0f);
			}
		}

		public static SolidBrush Brush06()
		{
			return new SolidBrush(Color.FromArgb(255, 255, 255));
		}

		public static LinearGradientBrush Brush07(Rectangle rect)
		{
			LinearGradientBrush brush = new LinearGradientBrush(rect
				, Color.FromArgb(253, 253, 253), Color.FromArgb(201, 200, 220), 90.0f);
			
			float[] relativeIntensities = {0.0f, 0.008f, 1.0f};
			float[] relativePositions   = {0.0f, 0.32f, 1.0f};

			Blend blend = new Blend();
			blend.Factors = relativeIntensities;
			blend.Positions = relativePositions;
			brush.Blend = blend;
			return brush;
		}

		public static SolidBrush Brush08()
		{
			return new SolidBrush(Color.FromArgb(198, 197, 215));
		}

		public static LinearGradientBrush Brush09(Rectangle rect)
		{
			LinearGradientBrush brush = new LinearGradientBrush(rect
				, Color.FromArgb(172, 171, 191), Color.FromArgb(248, 252, 253), 90.0f);
			float[] relativeIntensities = {0.0f, 0.992f, 1.0f};
			float[] relativePositions   = {0.0f, 0.68f, 1.0f};

			Blend blend = new Blend();
			blend.Factors = relativeIntensities;
			blend.Positions = relativePositions;
			brush.Blend = blend;
			return brush;
		}

		#endregion

		#region Brushes (disable state)

		//[CLSCompliantAttribute(false)]
		public static SolidBrush _Brush01(Styles style)
		{
			switch (style)
			{
				case Styles.SeaGreen:
					return new SolidBrush(Color.FromArgb(64, 202, 196, 184));
				case Styles.Silver:
					return new SolidBrush(Color.FromArgb(64, 196, 195, 191));
				default:
					return new SolidBrush(Color.FromArgb(64, 201, 199, 186));
			}
		}

		//[CLSCompliantAttribute(false)]
		public static SolidBrush _Brush02(Styles style)
		{
			switch (style)
			{
				case Styles.SeaGreen:
					return new SolidBrush(Color.FromArgb(246, 242, 233));
				case Styles.Silver:
					return new SolidBrush(Color.FromArgb(241, 241, 237));
				default:
					return new SolidBrush(Color.FromArgb(245, 244, 234));
			}
		}

		#endregion

		#region Pens (enable state)

		public static Pen Pen01(Styles style)
		{
			switch (style)
			{
				case Styles.SeaGreen:	return new Pen(Color.FromArgb(243, 238, 219));
                default: return new Pen(Color.FromArgb(236, 235, 230));
			}
		}

		public static Pen Pen02(Styles style)
		{
			switch (style)
			{
				case Styles.SeaGreen:	return new Pen(Color.FromArgb(236, 225, 201));
				default:							return new Pen(Color.FromArgb(226, 223, 214));
			}
		}

		public static Pen Pen03(Styles style)
		{
			switch (style)
			{
				case Styles.SeaGreen:	return new Pen(Color.FromArgb(227, 209, 184));
				default:							return new Pen(Color.FromArgb(214, 208, 197));
			}
		}

		public static Pen Pen04(Styles style, Rectangle rect)
		{
			LinearGradientBrush _brush;
			Pen pen;

			switch (style)
			{
				case Styles.SeaGreen:	
					_brush = new LinearGradientBrush(rect
						, Color.FromArgb(251, 247, 232), Color.FromArgb(64, 216, 181, 144), 90.0f);

					pen = new Pen(_brush);
					_brush.Dispose();
					return pen;

				default:
					_brush = new LinearGradientBrush(rect
						, Color.FromArgb(245, 244, 242), Color.FromArgb(64, 186, 174, 160), 90.0f);

                    pen = new Pen (_brush);
					_brush.Dispose();
					return pen;		
			}
		}

		public static Pen Pen05(Styles style, Rectangle rect)
		{
			LinearGradientBrush _brush;
			Pen pen;

			switch (style)
			{
				case Styles.SeaGreen:	
					_brush = new LinearGradientBrush(rect
						, Color.FromArgb(246, 241, 224), Color.FromArgb(64, 194, 156, 120), 90.0f);

					pen = new Pen(_brush);
					_brush.Dispose();
					return pen;

				default:							
					_brush = new LinearGradientBrush(rect
						, Color.FromArgb(240, 238, 234), Color.FromArgb(64, 175, 168, 142), 90.0f);
					
					pen = new Pen(_brush);
					_brush.Dispose();
					return pen;
			}
		}

		public static Pen Pen06(Styles style)
		{
			switch (style)
			{
				case Styles.SeaGreen:	return new Pen(Color.FromArgb(194, 209, 143));
				default:							return new Pen(Color.FromArgb(206, 231, 255));
			}
		}

		public static Pen Pen07(Styles style)
		{
			switch (style)
			{
				case Styles.SeaGreen:	return new Pen(Color.FromArgb(177, 203, 128));
				default:							return new Pen(Color.FromArgb(188, 212, 246));
			}
		}

		public static Pen Pen08(Styles style)
		{
			switch (style)
			{
				case Styles.SeaGreen:	return new Pen(Color.FromArgb(144, 193, 84));
				default:							return new Pen(Color.FromArgb(137, 173, 228));
			}
		}

		public static Pen Pen09(Styles style)
		{
			switch (style)
			{
				case Styles.SeaGreen:	return new Pen(Color.FromArgb(168, 167, 102));
				default:							return new Pen(Color.FromArgb(105, 130, 238));
			}
		}

		public static Pen Pen10(Styles style)
		{
			switch (style)
			{
				case Styles.SeaGreen:	return new Pen(Color.FromArgb(252, 197, 149));
				default:							return new Pen(Color.FromArgb(255, 240, 207));
			}
		}

		public static Pen Pen11(Styles style)
		{
			switch (style)
			{
				case Styles.SeaGreen:	return new Pen(Color.FromArgb(237, 190, 150));
				default:							return new Pen(Color.FromArgb(253, 216, 137));
			}
		}

		public static Pen Pen12(Styles style)
		{
			switch (style)
			{
				case Styles.SeaGreen:	return new Pen(Color.FromArgb(227, 145, 79));
				default:							return new Pen(Color.FromArgb(248, 178, 48));
			}
		}

		public static Pen Pen13(Styles style)
		{
			switch (style)
			{
				case Styles.SeaGreen:	return new Pen(Color.FromArgb(207, 114, 37));	
				default:							return new Pen(Color.FromArgb(229, 151, 0));
			}
		}

        //Border Pen Dark
		public static Pen Pen14(Styles style)
		{
			switch (style)
			{
				case Styles.SeaGreen:	return new Pen(Color.FromArgb(55, 98, 6));
                default: return new Pen(Color.FromArgb(0, 60, 116));
			}
		}

        //Border Pen Medium
		public static Pen Pen15(Styles style)
		{
			switch (style)
			{
				case Styles.SeaGreen:	return new Pen(Color.FromArgb(109, 138, 77));
                default:  return new Pen(Color.FromArgb(85, 125, 162));
			}
		}

        //Border Pen Light
		public static Pen Pen16(Styles style)
		{
			switch (style)
			{
				case Styles.SeaGreen:	return new Pen(Color.FromArgb(192, 109, 138, 77));
                default: return new Pen(Color.FromArgb(192, 85, 125, 162));
			}
		}

		public static Pen Pen17(Styles style, Rectangle rect)
		{
			LinearGradientBrush _brush;
			Pen pen;

			switch (style)
			{
				case Styles.SeaGreen:
					_brush = new LinearGradientBrush(rect
						, Color.FromArgb(228, 212, 191), Color.FromArgb(229, 217, 195), 90.0f);
					pen = new Pen(_brush);
					_brush.Dispose();
					return pen;

				default:
					_brush = new LinearGradientBrush(rect
						, Color.FromArgb(216, 212, 203), Color.FromArgb(218, 216, 207), 90.0f);
                    pen = new Pen(_brush);
					_brush.Dispose();
					return pen;
			}
		}

		public static Pen Pen18(Styles style, Rectangle rect)
		{
			LinearGradientBrush _brush;
			Pen pen;

			switch (style)
			{
				case Styles.SeaGreen:
					_brush = new LinearGradientBrush(rect
						, Color.FromArgb(232, 219, 197), Color.FromArgb(234, 224, 201), 90.0f);
					pen = new Pen(_brush);
					_brush.Dispose();
					return pen;

				default:
					_brush = new LinearGradientBrush(rect
						, Color.FromArgb(221, 218, 209), Color.FromArgb(223, 222, 214), 90.0f);
					pen = new Pen(_brush);
					_brush.Dispose();
					return pen;
			}
		}

		public static Pen Pen19(Styles style)
		{
			switch (style)
			{
				case Styles.SeaGreen:	return new Pen(Color.FromArgb(223, 205, 180));
				default:							return new Pen(Color.FromArgb(209, 204, 192));
			}
		}

		public static Pen Pen20(Styles style)
		{
			switch (style)
			{
				case Styles.SeaGreen:	return new Pen(Color.FromArgb(231, 217, 195));
				default:							return new Pen(Color.FromArgb(220, 216, 207));
			}
		}

		public static Pen Pen21(Styles style)
		{
			switch (style)
			{
				case Styles.SeaGreen:	return new Pen(Color.FromArgb(242, 236, 216));
				default:							return new Pen(Color.FromArgb(234, 233, 227));
			}
		}

		public static Pen Pen22(Styles style)
		{
			switch (style)
			{
				case Styles.SeaGreen:	return new Pen(Color.FromArgb(248, 244, 228));
				default:							return new Pen(Color.FromArgb(242, 241, 238));
			}
		}

		public static Pen Pen23()
		{
			return new Pen(Color.FromArgb(255, 255, 255));
		}

		public static Pen Pen24()
		{
			return new Pen(Color.FromArgb(172, 171, 189));
		}

		#endregion

		#region Pens (disable state)

		//[CLSCompliantAttribute(false)]
		public static Pen _Pen01(Styles style)
		{
			switch (style)
			{
				case Styles.SeaGreen:
					return new Pen(Color.FromArgb(202, 196, 184));
				case Styles.Silver:
					return new Pen(Color.FromArgb(196, 195, 191));
				default:
					return new Pen(Color.FromArgb(201, 199, 186));
			}
		}

		//[CLSCompliantAttribute(false)]
		public static Pen _Pen02(Styles style)
		{
			switch (style)
			{
				case Styles.SeaGreen:
					return new Pen(Color.FromArgb(170, 202, 196, 184));
				case Styles.Silver:
					return new Pen(Color.FromArgb(170, 196, 195, 191));
				default:
					return new Pen(Color.FromArgb(170, 201, 199, 186));
			}
		}

		#endregion

		#region Constructor
		public ColorManager()
		{
		}
		#endregion
	}

	#endregion

	#region Margin

    //public struct BT
    //{
    //    public const int LeftMargin = 7;
    //    public const int TextMargin = 7;
    //}
	#endregion

	#region TextUtil
/*
	public class TextUtil
	{
		public static Size GetTextSize(Graphics graphics, string text, Font font, Size size)
		{
			if (text.Length == 0) return Size.Empty;

			StringFormat format = new StringFormat();
			format.FormatFlags = StringFormatFlags.FitBlackBox; //MeasureTrailingSpaces;

			RectangleF layoutRect = new System.Drawing.RectangleF(0, 0, size.Width, size.Height);
			CharacterRange[] chRange = { new CharacterRange(0, text.Length)};
			Region[] regs = new Region[1];

			format.SetMeasurableCharacterRanges(chRange);

			regs = graphics.MeasureCharacterRanges(text, font, layoutRect, format);
			Rectangle rect = Rectangle.Round(regs[0].GetBounds(graphics));
			
			return new Size(rect.Width, rect.Height);
		}

		private TextUtil()
		{
		}
	}*/
	#endregion

	#endregion

}

