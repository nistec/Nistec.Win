using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

using Nistec.Drawing;
using System.Runtime.InteropServices;
using Nistec.WinForms.Controls;

namespace Nistec.WinForms
{

	[Designer(typeof(Design.McDesigner))]
	[System.ComponentModel.ToolboxItem(true)]
	[ToolboxBitmap (typeof(McLabel), "Toolbox.Label.bmp")]
	public class McLabel : Nistec.WinForms.Controls.McBase,ILabel,ILayout
	{
		#region Members
		private System.ComponentModel.Container components = null;
		#endregion

		#region Constructors

		public McLabel()
		{
			InitializeComponent();

            base.SetStyle(ControlStyles.Selectable, false);
            base.SetStyle(ControlStyles.StandardDoubleClick, false);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, this.IsOwnerDraw());
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            //CommonProperties.SetSelfAutoSizeInDefaultLayout(this, true);
            //this.labelState[StateFlatStyle] = 2;
            this.labelState[StateUseMnemonic] = 1;
            this.labelState[StateBorderStyle] = 0;
            this.TabStop = false;
            this.requestedHeight = base.Height;
            this.requestedWidth = base.Width;
		}

        //internal McLabel(bool net):this()
        //{
        //    this.m_netFram=net;
        //}
		#endregion

		#region Dispose

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#endregion

		#region Component Designer generated code
		private void InitializeComponent()
		{
			// 
			// McLabel
			// 
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "McLabel";
			this.Size = new System.Drawing.Size(80, 20);
		}
		#endregion

		#region Overrides
		protected override void OnParentChanged(EventArgs e)
		{
			if (Parent == null) return;
			//mPoint=GetPoints();
			base.OnParentChanged(e);
		}

        //protected override void OnTextChanged(EventArgs e) 
        //{
        //    //mPoint=GetPoints();
        //    base.OnTextChanged(e);
        //}
 
        protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint (e);
			Graphics g=e.Graphics;
			Rectangle crect = this.ClientRectangle;
			Rectangle rect=new Rectangle (crect.X ,crect.Y,crect.Width-1 ,crect.Height-1 );

			if(m_BorderStyle==BorderStyle.None) 
			{
				//g.Clear(LayoutManager.Layout.BackgroundColorInternal);
				//this.BackColor=Color.Transparent ; 
				//LayoutManager.DrawString(g,rect,TextAlign,this.Text,this.Font);
                
                //DrowLabel(e.Graphics);
                //OnPaintInternal(e);
                //return;
			}
			else if(ControlLayout==ControlLayout.Flat || ControlLayout==ControlLayout.Visual )
			{
				//this.BackColor = this.Parent.BackColor;
		
				if(ControlLayout==ControlLayout.Visual )
				{
					using (Brush sb=LayoutManager.GetBrushGradient(rect,270f))
					{
						g.FillRectangle (sb,rect);
					}
				}
				else
				{
					using (Brush sb= LayoutManager.GetBrushFlat())
					{
						g.FillRectangle (sb,rect);
					}
				}
                if (this.BorderStyle == BorderStyle.Fixed3D)
                {
                    ControlPaint.DrawBorder3D(g, crect, System.Windows.Forms.Border3DStyle.Sunken);
                }
                else
                {
                    using (Pen pen = LayoutManager.GetPenBorder())
                    {
                        g.DrawRectangle(pen, rect);
                    }
                }
			}
            else if (ControlLayout == ControlLayout.XpLayout || ControlLayout == ControlLayout.VistaLayout)
            {
                //this.BackColor = this.Parent.BackColor;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                System.Drawing.Drawing2D.GraphicsPath path = Nistec.Drawing.DrawUtils.GetRoundedRect(rect, 3);

                using (Brush sb = LayoutManager.GetBrushGradient(rect, 270f))
                {
                    g.FillPath(sb, path);
                }

                using (Pen pen = LayoutManager.GetPenBorder())
                {
                    g.DrawPath(pen, path);
                }
            }

            else
            {

                using (Brush sb = new SolidBrush(this.BackColor))
                {
                    g.FillRectangle(sb, rect);
                }

                if (this.BorderStyle == BorderStyle.Fixed3D)
                {
                    ControlPaint.DrawBorder3D(g, crect, System.Windows.Forms.Border3DStyle.Sunken);
                }
                else
                {
                    using (Pen pen = LayoutManager.GetPenBorder())
                    {
                        g.DrawRectangle(pen, rect);
                    }
                }
                //if (this.Text.Length > 0)
                //{
                //    using (Brush sb = new SolidBrush(this.ForeColor))
                //    {
                //        LayoutManager.DrawString(g, sb, rect, this.TextAlign, this.Text, this.Font);
                //    }
                //}

                goto Label_01;
                //this.BackColor = this.Parent.BackColor;
                //ControlPaint.DrawButton (g,rect,ButtonState.Normal  ); 
            }

            //if(this.Text.Length>0)
            //    LayoutManager.DrawString(g,rect,this.TextAlign,this.Text,this.Font);
            
            Label_01:

            DrowLabel(e.Graphics);

            //Image image=base.GetCurrentImage();
            //if(image!=null)
            //   LayoutManager.DrawImage(g,rect,image,this.ImageAlign,true);

        }

        #endregion

        #region  2005

        //private System.Collections.Specialized.BitVector32 labelState;
        private int[] labelState = new int[5];

        private static readonly int StateAnimating=3;
        private static readonly int StateAutoEllipsis=2;
        private static readonly int StateBorderStyle=0;
        //private static readonly int StateFlatStyle;
        //private static readonly int StateAutoSize = 1;
        private static readonly int StateUseMnemonic = 4;

        private LayoutUtils.MeasureTextCache textMeasurementCache;
        private ToolTip textToolTip;
        private bool showToolTip;
        private int requestedHeight;
        private int requestedWidth;


        protected override void OnTextChanged(EventArgs e)
        {
            //using (IDisposable disposable = LayoutTransaction.CreateTransactionIf(this.AutoSize, this.ParentInternal, this, PropertyNames.Text))
            //{
            this.MeasureTextCache.InvalidateCache();
            base.OnTextChanged(e);
            if (this.AutoSize)
            {
                this.AdjustSize();
                base.Invalidate();
            }
            //}
        }

        internal bool IsOwnerDraw()
        {
            return (this.ControlLayout !=  ControlLayout.System);
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if ((specified & BoundsSpecified.Height) != BoundsSpecified.None)
            {
                this.requestedHeight = height;
            }
            if ((specified & BoundsSpecified.Width) != BoundsSpecified.None)
            {
                this.requestedWidth = width;
            }
            if (this.AutoSize)// && this.SelfSizing)
            {
                Size preferredSize = base.PreferredSize;
                width = preferredSize.Width;
                height = preferredSize.Height;
            }
            base.SetBoundsCore(x, y, width, height, specified);
        }

        //internal void SetToolTip(ToolTip toolTip)
        //{
        //    if ((toolTip != null) && !this.controlToolTip)
        //    {
        //        this.controlToolTip = true;
        //    }
        //}

        #region draw image

        private void ResetImage()
        {
            this.Image = null;
        }

        private bool ShouldSerializeImage()
        {
            return (this.Image != null);
        }

        internal void Animate()
        {
            this.Animate(((!base.DesignMode && base.Visible) && base.Enabled) && (this.Parent != null));
        }

        private void Animate(bool animate)
        {
            bool flag = this.labelState[StateAnimating] != 0;
            if (animate != flag)
            {
                Image image = this.Image;
                if (animate)
                {
                    if (image != null)
                    {
                        ImageAnimator.Animate(image, new EventHandler(this.OnFrameChanged));
                        this.labelState[StateAnimating] = animate ? 1 : 0;
                    }
                }
                else if (image != null)
                {
                    ImageAnimator.StopAnimate(image, new EventHandler(this.OnFrameChanged));
                    this.labelState[StateAnimating] = animate ? 1 : 0;
                }
            }
        }

        private void OnFrameChanged(object o, EventArgs e)
        {
            if (base.InvokeRequired)
            {
                base.BeginInvoke(new EventHandler(this.OnFrameChanged), new object[] { o, e });
            }
            //else if (base.IsWindowObscured)
            //{
            //    this.StopAnimate();
            //}
            else
            {
                base.Invalidate();
            }
        }

        internal void StopAnimate()
        {
            this.Animate(false);
        }

        protected void DrawImage(Graphics g, Image image, Rectangle r, ContentAlignment align)
        {
            Rectangle rectangle = LayoutUtils.CalcImageRenderBounds(image, r, align);
            if (!base.Enabled)
            {
                ControlPaint.DrawImageDisabled(g, image, rectangle.X, rectangle.Y, this.BackColor);
            }
            else
            {
                g.DrawImage(image, rectangle.X, rectangle.Y, image.Width, image.Height);
            }
        }

        #endregion

        //[DefaultValue(false), EditorBrowsable(EditorBrowsableState.Always), Description("LabelAutoSize"), Category("Layout"), RefreshProperties(RefreshProperties.All), Localizable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Browsable(true)]
        //public override bool AutoSize
        //{
        //    get
        //    {
        //        return base.AutoSize;
        //    }
        //    set
        //    {
        //        if (this.AutoSize != value)
        //        {
        //            base.AutoSize = value;
        //            this.AdjustSize();
        //        }
        //    }
        //}

        [Description("LabelAutoEllipsis"), Category("Behavior"), DefaultValue(false), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public bool AutoEllipsis
        {
            get
            {
                return (this.labelState[StateAutoEllipsis] != 0);
            }
            set
            {
                if (this.AutoEllipsis != value)
                {
                    this.labelState[StateAutoEllipsis] = value ? 1 : 0;
                    MeasureTextCache.InvalidateCache();
                    this.OnAutoEllipsisChanged();
                    if (value && (this.textToolTip == null))
                    {
                        this.textToolTip = new ToolTip();
                    }
                    if (this.Parent != null)
                    {
                        //LayoutTransaction.DoLayoutIf(this.AutoSize, this.Parent, this, PropertyNames.AutoEllipsis);
                    }
                    base.Invalidate();
                }
            }
        }

        internal LayoutUtils.MeasureTextCache MeasureTextCache
        {
            get
            {
                if (this.textMeasurementCache == null)
                {
                    this.textMeasurementCache = new LayoutUtils.MeasureTextCache();
                }
                return this.textMeasurementCache;
            }
        }

        internal virtual void OnAutoEllipsisChanged()
        {
        }
        
        internal int WindowStyle
        {
            get
            {
                return (int)((long) Win32.WinAPI.GetWindowLong(new HandleRef(this, this.Handle), -16));
            }
            set
            {
                Win32.WinAPI.SetWindowLong(new HandleRef(this, this.Handle), -16, new HandleRef(null, (IntPtr)value));
            }
        }

        [DefaultValue(true), Description("LabelUseMnemonic"), Category("Appearance")]
        internal bool UseMnemonic
        {
            get
            {
                return (this.labelState[StateUseMnemonic] != 0);
            }
            set
            {
                if (this.UseMnemonic != value)
                {
                    this.labelState[StateUseMnemonic] = value ? 1 : 0;
                    this.MeasureTextCache.InvalidateCache();
                    //using (IDisposable disposable = LayoutTransaction.CreateTransactionIf(this.AutoSize, this.Parent, this, PropertyNames.Text))
                    //{
                        this.AdjustSize();
                        base.Invalidate();
                    //}
                    if (base.IsHandleCreated)
                    {
                        int windowStyle = this.WindowStyle;
                        if (!this.UseMnemonic)
                        {
                            windowStyle |= 0x80;
                        }
                        else
                        {
                            windowStyle &= -129;
                        }
                        this.WindowStyle = windowStyle;
                    }
                }
            }
        }


        internal void AdjustSize()
        {
            if (/*this.SelfSizing &&*/ (this.AutoSize || (((this.Anchor & (AnchorStyles.Right | AnchorStyles.Left)) != (AnchorStyles.Right | AnchorStyles.Left)) && ((this.Anchor & (AnchorStyles.Bottom | AnchorStyles.Top)) != (AnchorStyles.Bottom | AnchorStyles.Top)))))
            {
                int requestedHeight = this.requestedHeight;
                int requestedWidth = this.requestedWidth;
                try
                {
                    Size size = this.AutoSize ? base.PreferredSize : new Size(requestedWidth, requestedHeight);
                    base.Size = size;
                }
                finally
                {
                    this.requestedHeight = requestedHeight;
                    this.requestedWidth = requestedWidth;
                }
            }
        }

        internal virtual StringFormat CreateStringFormat()
        {
            return LayoutUtils.CreateStringFormat(this, this.TextAlign, this.AutoEllipsis, this.UseMnemonic,this.DesignMode);
        }

 
        #region not UseCompatibleTextRendering

        //private TextFormatFlags CreateTextFormatFlags()
        //{
        //    return this.CreateTextFormatFlags(base.Size - this.GetBordersAndPadding());
        //}

        //internal virtual TextFormatFlags CreateTextFormatFlags(Size constrainingSize)
        //{
        //    TextFormatFlags flags = LayoutUtils.CreateTextFormatFlags(this, this.TextAlign, this.AutoEllipsis, this.UseMnemonic, this.DesignMode);
        //    if (!this.MeasureTextCache.TextRequiresWordBreak(this.Text, this.Font, constrainingSize, flags))
        //    {
        //        flags &= ~(TextFormatFlags.TextBoxControl | TextFormatFlags.WordBreak);
        //    }
        //    return flags;
        //}

        //private Size GetBordersAndPadding()
        //{
        //    Size size = base.Padding.Size;
        //    //if (this.UseCompatibleTextRendering)
        //    //{
        //        //if (this.BorderStyle != BorderStyle.None)
        //        //{
        //        //    size.Height += 6;
        //        //    size.Width += 2;
        //        //    return size;
        //        //}
        //        //size.Height += 3;
        //        //return size;
        //    //}
        //    Size sizeFromClient = this.SizeFromClientSize(Size.Empty);
        //    size.Height += sizeFromClient.Height;
        //    size.Width += sizeFromClient.Width;

        //        //size += this.SizeFromClientSize(Size.Empty);
        //    if (this.BorderStyle == BorderStyle.Fixed3D)
        //    {
        //        size += new Size(2, 2);
        //    }
        //    return size;
        //}

        #endregion

 
        protected void DrowLabel(Graphics g)
        {
            Color nearestColor;
            this.Animate();
            Rectangle r = LayoutUtils.DeflateRect(base.ClientRectangle, base.Padding);
            ImageAnimator.UpdateFrames();
            Image image = this.Image;
            if (image != null)
            {
                this.DrawImage(g, image, r, base.RtlTranslateAlignment(this.ImageAlign));
            }
            IntPtr hdc = g.GetHdc();
            try
            {
                using (Graphics graphics = Graphics.FromHdc(hdc))
                {
                    nearestColor = graphics.GetNearestColor(base.Enabled ? this.ForeColor : LayoutManager.Layout.DisableColor);
                }
            }
            finally
            {
                g.ReleaseHdc();
            }
            if (this.AutoEllipsis)
            {
                Rectangle clientRectangle = base.ClientRectangle;
                Size preferredSize = this.GetPreferredSize(new Size(clientRectangle.Width, clientRectangle.Height));
                this.showToolTip = (clientRectangle.Width < preferredSize.Width) || (clientRectangle.Height < preferredSize.Height);
            }
            else
            {
                this.showToolTip = false;
            }
            //if (this.UseCompatibleTextRendering)
            //{
            using (StringFormat format = this.CreateStringFormat())
            {
                if (base.Enabled)
                {
                    using (Brush brush = new SolidBrush(nearestColor))
                    {
                        g.DrawString(this.Text, this.Font, brush, r, format);
                        goto Label_01BF;
                    }
                }
                ControlPaint.DrawStringDisabled(g, this.Text, this.Font, nearestColor, r, format);
                goto Label_01BF;
            }
            //}
            //TextFormatFlags flags = this.CreateTextFormatFlags();
            //if (base.Enabled)
            //{
            //    TextRenderer.DrawText(e.Graphics, this.Text, this.Font, r, nearestColor, flags);
            //}
            //else
            //{
            //    Color foreColor = StyleControl.DisabledTextColor(this.BackColor);
            //    TextRenderer.DrawText(e.Graphics, this.Text, this.Font, r, foreColor, flags);
            //}
        Label_01BF:
            return;// base.OnPaint(e);
        }

	   #endregion

		#region Properties

		[Browsable(false),EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool TabStop
		{
			get {return base.TabStop;}
			set 
			{
				base.TabStop=value;
			}
		}

		[Browsable(false),EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new int TabIndex
		{
			get {return base.TabIndex;}
			set 
			{
				base.TabIndex=value;
			}
		}

		#endregion

        #region StyleProperty

        [Category("Style"), DefaultValue(typeof(Color), "WindowText")]
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set
            {
                SerializeForeColor(value, true);
            }
        }

        [Category("Style"), DefaultValue(typeof(Color), "Window")]
        public override Color BackColor
        {
            get { return base.BackColor; }
            set
            {
                SerializeBackColor(value, true);
            }
        }

        protected override void OnStylePropertyChanged(PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("ControlLayout") || e.PropertyName.Equals("BackgroundColor") || e.PropertyName.StartsWith("ColorBrush"))
                SerializeBackColor(Color.Empty, false);
            if (e.PropertyName.Equals("ControlLayout") || e.PropertyName.Equals("ForeColor"))
                SerializeForeColor(Color.Empty, false);
            if (e.PropertyName.Equals("ControlLayout") || e.PropertyName.Equals("TextFont"))
                SerializeFont(Form.DefaultFont, false);
            if ((DesignMode || IsHandleCreated))
                this.Invalidate(true);
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected void SerializeFont(Font value, bool force)
        {
            if (ShouldSerializeForeColor())
                this.Font = LayoutManager.Layout.TextFontInternal;
            else if (force)
                this.Font = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected void SerializeForeColor(Color value, bool force)
        {
            if (ShouldSerializeForeColor())
            {
                base.ForeColor = LayoutManager.Layout.ForeColorInternal;
            }
            else if (force)
            {
                base.ForeColor = value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected void SerializeBackColor(Color value, bool force)
        {
            if (ShouldSerializeBackColor())
            {
                base.BackColor = LayoutManager.Layout.BackgroundColorInternal;
            }
            else if (force)
            {
                if (BorderStyle == BorderStyle.None)
                    base.BackColor = Color.Empty;
                else
                    base.BackColor = value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected bool ShouldSerializeBackColor()
        {
            return IsHandleCreated && StylePainter != null && BorderStyle != BorderStyle.None;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected bool ShouldSerializeForeColor()
        {
            return IsHandleCreated && StylePainter != null;
        }

        [Category("Style"), DefaultValue(BorderStyle.FixedSingle)]
        public override BorderStyle BorderStyle
        {
            get
            {
                return base.BorderStyle;
            }
            set
            {
                if (base.BorderStyle != value)
                {
                    base.BorderStyle = value;
                    SerializeBackColor(Color.Empty, true);
                }
            }
        }
        #endregion

  
	}

}