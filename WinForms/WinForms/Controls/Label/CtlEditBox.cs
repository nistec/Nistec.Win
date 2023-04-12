using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using mControl.Util;
 
namespace mControl.WinCtl.Controls
{

	[Designer(typeof(Design.CtlDesigner))]
	[ToolboxItem(false)]
    [ToolboxBitmap(typeof(CtlEditBox), "Toolbox.EditBox.bmp")]
	public class CtlEditBox : mControl.WinCtl.Controls.CtlBase,ILabel,IStyleCtl,IBind
	{
		#region Members
		private System.ComponentModel.Container components = null;
        internal string internalText;
        int labelWidth ;
        ButtonAlign buttonAlign;
        CtlTextBox m_TextBox;

		#endregion

		#region Constructors

		public CtlEditBox()
		{
            buttonAlign = ButtonAlign.Left;
            labelWidth = 60;
            internalText = "label";
			InitializeComponent();
             
			SetStyle(ControlStyles.Selectable,false);
			SetStyle(ControlStyles.StandardDoubleClick,false);
			this.TabStop =false;  
		}

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
            this.m_TextBox = new CtlTextBox();
            // 
            // m_TextBox
            // 
            this.m_TextBox.Size = new Size(80, 20);
            this.m_TextBox.Location = new Point(0, 0);
            this.m_TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.m_TextBox.BackColor = System.Drawing.Color.White;
            this.m_TextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_TextBox.TabIndex = 0;
            this.m_TextBox.Text = "";
            this.m_TextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_TextBox_KeyDown);
            this.m_TextBox.Leave += new System.EventHandler(this.m_TextBox_Leave);
            this.m_TextBox.LostFocus += new System.EventHandler(this.m_TextBox_OnLostFocus);
            this.m_TextBox.GotFocus += new System.EventHandler(this.m_TextBox_OnGotFocus);
            this.m_TextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_TextBox_KeyPress);
            this.m_TextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.m_TextBox_KeyUp);
            this.m_TextBox.TextChanged += new System.EventHandler(this.m_TextBox_TextChanged);
            // 
			// CtlEditBox
			// 
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.m_TextBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.Name = "CtlEditBox";
			this.Size = new System.Drawing.Size(200, 20);
		}
		#endregion

		#region Overrides

        protected override void OnParentChanged(EventArgs e)
		{
			if (Parent == null) return;
			//mPoint=GetPoints();
			base.OnParentChanged(e);
		}


		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint (e);
			Graphics g=e.Graphics;
			Rectangle crect = this.ClientRectangle;
			Rectangle rect=new Rectangle (crect.X ,crect.Y,crect.Width-1 ,crect.Height-1 );

	    	if(ControlLayout==ControlLayout.Flat || ControlLayout==ControlLayout.Visual )
			{
				//this.BackColor = this.Parent.BackColor;
		
				if(ControlLayout==ControlLayout.Visual )
				{
					using (Brush sb=CtlStyleLayout.GetBrushGradient(rect,270f))
					{
						g.FillRectangle (sb,rect);
					}
				}
				else
				{
					using (Brush sb= CtlStyleLayout.GetBrushFlat())
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
                    using (Pen pen = CtlStyleLayout.GetPenBorder())
                    {
                        g.DrawRectangle(pen, rect);
                    }
                }
			}
            else if (ControlLayout == ControlLayout.XpLayout)
            {
                //this.BackColor = this.Parent.BackColor;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                System.Drawing.Drawing2D.GraphicsPath path = mControl.Drawing.DrawUtils.GetRoundedRect(rect, 3);

                using (Brush sb = CtlStyleLayout.GetBrushGradient(rect, 270f))
                {
                    g.FillPath(sb, path);
                }

                using (Pen pen = CtlStyleLayout.GetPenBorder())
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
                    using (Pen pen = CtlStyleLayout.GetPenBorder())
                    {
                        g.DrawRectangle(pen, rect);
                    }
                }
                if (this.Text.Length > 0)
                {
                    using (Brush sb = new SolidBrush(this.ForeColor))
                    {
                        CtlStyleLayout.DrawString(g, sb, rect, base.TextAlign, internalText, this.Font);
                    }
                }

                goto Label_01;
                //this.BackColor = this.Parent.BackColor;
                //ControlPaint.DrawButton (g,rect,ButtonState.Normal  ); 
            }

            if(!string.IsNullOrEmpty(internalText))
                CtlStyleLayout.DrawString(g, rect, base.TextAlign, internalText, this.Font);

            
            Label_01:
            Image image=base.GetCurrentImage();
			if(image!=null)
			   CtlStyleLayout.DrawImage(g,rect,image,this.ImageAlign,true);

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

        protected override void OnStylePainterChanged(EventArgs e)
        {
            base.OnStylePainterChanged(e);
            this.m_TextBox.StylePainter = this.StylePainter;
        }

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
            if (e.PropertyName.Equals("ControlLayout") || e.PropertyName.Equals("FlatColor") || e.PropertyName.StartsWith("ColorBrush"))
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
                this.Font = CtlStyleLayout.Layout.TextFontInternal;
            else if (force)
                this.Font = value;
            this.m_TextBox.Font = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected void SerializeForeColor(Color value, bool force)
        {
            if (ShouldSerializeForeColor())
            {
                base.ForeColor = CtlStyleLayout.Layout.ForeColorInternal;
                m_TextBox.ForeColor = CtlStyleLayout.Layout.ForeColorInternal;
            }
            else if (force)
            {
                base.ForeColor = value;
                m_TextBox.ForeColor = value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected void SerializeBackColor(Color value, bool force)
        {
            if (ShouldSerializeBackColor())
            {
                base.BackColor = CtlStyleLayout.Layout.FlatColorInternal;
            }
            else if (force)
            {
                if (BorderStyle == BorderStyle.None)
                    base.BackColor = Color.Empty;
                else
                    base.BackColor = value;
            }
            m_TextBox.BackColor = CtlStyleLayout.Layout.BackColorInternal;
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
                return BorderStyle.FixedSingle;
            }
            set
            {
            }
        }
        #endregion

        #region m_TextBox Events handlers

        private void m_TextBox_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.OnKeyUp(e);
        }

        private void m_TextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.OnKeyDown(e);
        }

        private void m_TextBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            this.OnKeyPress(e);
        }

        private void m_TextBox_OnGotFocus(object sender, System.EventArgs e)
        {
            this.OnGotFocus(e);
        }

        private void m_TextBox_OnLostFocus(object sender, System.EventArgs e)
        {
            this.OnLostFocus(e);
        }

        private void m_TextBox_TextChanged(object sender, System.EventArgs e)
        {
            this.OnTextChanged(e);
        }

        private void m_TextBox_Leave(object sender, System.EventArgs e)
        {
            base.OnLostFocus(e);
        }

        #endregion

        #region Override

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            //OnControlLayoutChanged(e);
            OnButtonAlignChanged(e);

        }


        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            this.m_TextBox.Focus();
            m_TextBox.SelectAll();
        }

        protected virtual void OnButtonAlignChanged(EventArgs e)
        {
            int textWidth = this.Width - labelWidth;
            if (textWidth <= 0)
                return;
            this.m_TextBox.Width = textWidth;

            if (buttonAlign == ButtonAlign.Right)
            {
                base.TextAlign = ContentAlignment.MiddleRight;
                this.m_TextBox.Location = new Point(0, 0);
            }
            else
            {
                base.TextAlign = ContentAlignment.MiddleLeft;
                this.m_TextBox.Location = new Point(labelWidth , 0);
            }

            //base.OnButtonAlignChanged(e);

            //if (this.m_TextBox == null)
            //    return;

            //int yPos = (this.Height - m_TextBox.Height) / 2;


            //if (!ButtonVisible)// ControlLayout==ControlLayout.System)
            //{
            //    this.m_TextBox.Size = new Size(Size.Width - 6, Size.Height - 4);
            //    yPos = (this.Height - m_TextBox.Height) / 2;
            //    this.m_TextBox.Location = new Point(2, yPos);
            //}
            //else
            //{
            //    Rectangle rect = this.ClientRectangle;

            //    if (this.ButtonAlign == ButtonAlign.Left)
            //    {
            //        this.m_TextBox.Size = new Size(this.Width - m_Button.Width - 6, rect.Height - 4);
            //        yPos = ((rect.Height - m_TextBox.Height) / 2);
            //        this.m_TextBox.Location = new Point(rect.X + m_Button.Left + m_Button.Width + 2, rect.Y + yPos);
            //    }
            //    else
            //    {
            //        this.m_TextBox.Size = new Size(rect.Width - m_Button.Width - 6, rect.Height - 6);
            //        yPos = ((rect.Height - m_TextBox.Height) / 2);
            //        this.m_TextBox.Location = new Point(rect.X + 2, rect.Y + yPos);
            //    }
            //}

            //this.Invalidate();
        }

        //protected override void OnControlLayoutChanged(EventArgs e)
        //{
        //    base.onOnControlLayoutChanged(e);

        //    this.Invalidate();
        //}

        [Browsable(false)]
        public override bool Focused
        {
            get { return m_TextBox.Focused; }
        }

        public new bool Focus()
        {
            return m_TextBox.Focus();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            if (this.m_TextBox != null)
            {
                this.m_TextBox.Font = base.Font;
            }
        }

        protected override void OnRightToLeftChanged(EventArgs e)
        {
            base.OnRightToLeftChanged(e);
            if (this.m_TextBox != null)
            {
                this.m_TextBox.RightToLeft = base.RightToLeft;
            }
        }

        #endregion

        #region Public methods

        public virtual void SelectAll()
        {
            if (!m_TextBox.IsDisposed)
                m_TextBox.SelectAll();
        }

        [Description("ComboBoxSelectionLength"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int SelectionLength
        {
            get
            {
                return this.m_TextBox.SelectionLength;
                //				int[] numArray3 = new int[1];
                //				int[] numArray1 = numArray3;
                //				numArray3 = new int[1];
                //				int[] numArray2 = numArray3;
                //				WinMethods.SendMessage(new HandleRef(this, base.Handle), 320, numArray2, numArray1);
                //				return (numArray1[0] - numArray2[0]);
            }
            set
            {
                this.m_TextBox.SelectionLength = value;
                //				if (value < 0)
                //				{
                //					throw new ArgumentException("InvalidArgument", value.ToString() );
                //				}
                //				this.Select(this.SelectionStart, value);
            }
        }

        [Description("ComboBoxSelectionStart"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionStart
        {
            get
            {
                //				int[] numArray1 = new int[1];
                //				WinMethods.SendMessage(new HandleRef(this, base.Handle), 320, numArray1, null);
                //				return numArray1[0];
                return this.m_TextBox.SelectionStart;
            }
            set
            {
                this.m_TextBox.SelectionStart = value;
                //				if (value < 0)
                //				{
                //					throw new ArgumentException("InvalidArgument", value.ToString() );
                //				}
                //				this.Select(value, this.SelectionLength);
            }
        }

        public void Select(int start, int length)
        {
            this.m_TextBox.Select(start, length);
        }

        #endregion

        #region Properties

        public int LabelWidth
        {
            get
            {
                return labelWidth;
            }
            set
            {
                if (value < 0)
                    value = 0;
                labelWidth = value;
                OnButtonAlignChanged(EventArgs.Empty);
            }
        }

        [Category("Behavior"), DefaultValue("")]
        public string LabelText
        {
            get
            {
                return internalText;
            }
            set
            {
                internalText = value;
            }
        }

        [DefaultValue(ButtonAlign.Left)]
        public ButtonAlign ButtonAlign
        {
            get
            {
                return buttonAlign;
            }
            set
            {
                buttonAlign = value;
                OnButtonAlignChanged(EventArgs.Empty);
            }
        }

        [Browsable(true), Bindable(true),
          DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
          Category("Appearance")]
        public override string Text
        {
            get
            {
                return this.m_TextBox.Text;
            }
            set
            {
                this.m_TextBox.Text = value;
            }
        }

        public override ContextMenuStrip ContextMenuStrip
        {
            get
            {
                return base.ContextMenuStrip;
            }
            set
            {
                base.ContextMenuStrip = value;
                this.m_TextBox.ContextMenuStrip = value;
            }
        }

        [Category("Behavior"), DefaultValue("")]
        public string DefaultValue
        {
            get { return m_TextBox.DefaultValue; }
            set
            {
                m_TextBox.DefaultValue = value;
                //this.m_TextBox.DefaultValue=value;
            }
        }

        [Category("Behavior"), DefaultValue(false)]
        public bool ReadOnly
        {
            get { return m_TextBox.ReadOnly; }
            set
            {
                if (ReadOnly != value)
                {
                    m_TextBox.ReadOnly = value;
                    this.Invalidate(false);
                }
            }
        }
   
        [Category("Appearance"), DefaultValue(HorizontalAlignment.Left)]
        public new HorizontalAlignment TextAlign
        {
            get { return m_TextBox.TextAlign; }
            set
            {
                if (m_TextBox.TextAlign != value)
                {
                    m_TextBox.TextAlign = value;
                    m_TextBox.Invalidate();
                    if (this.DesignMode)
                    {
                        this.Refresh();
                    }
                }
            }
        }

        [Category("Behavior")]
        [DefaultValue(false)]
        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsModified
        {
            get { return m_TextBox.Modified; }
            set { m_TextBox.Modified = value; }
        }

        [Category("Behavior"), DefaultValue(true)]
        public new bool CausesValidation
        {
            get { return m_TextBox.CausesValidation; }
            set { m_TextBox.CausesValidation = value; }
        }

        [Browsable(false)]
        public Rectangle GetTextRect
        {
            get
            {
                return this.m_TextBox.ClientRectangle;
                //return new Rectangle(this.Width - m_ButtonWidth,1,m_ButtonWidth - 1,this.Height - 2);
            }
        }

        #endregion

        #region ICtlTextBox Members

        [DefaultValue("")]
        public virtual string Format
        {
            get { return m_TextBox.Format; }
            set { m_TextBox.Format = value; }
        }

        public void AppendText(string value)
        {
            m_TextBox.AppendText(value);
        }

        public override void ResetText()
        {
            m_TextBox.ResetText();
        }

        public void AppendFormatText(string value)
        {
            m_TextBox.AppendFormatText(value);
        }

        #endregion

        #region IBind Members

        [Browsable(false)]
        public BindingFormat BindFormat
        {
            get { return BindingFormat.String; }
        }

        public string BindPropertyName()
        {
            return "Text";
        }
        public virtual void BindDefaultValue()
        {
            //if (this.DefaultValue.Length > 0)
            //{
            m_TextBox.Text = this.DefaultValue;
            if (base.IsHandleCreated)
            {
                this.OnTextChanged(EventArgs.Empty);
            }
            //}
        }

        #endregion

  
	}

}