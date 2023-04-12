using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using Nistec.Drawing;

using Nistec.Data;
using Nistec.WinForms.Controls;
using Nistec.Win;

namespace Nistec.WinForms
{
	[Designer(typeof(Design.McEditDesigner))]
	[ToolboxItem(true)]
	[ToolboxBitmap (typeof(McSpinEdit),"Toolbox.SpinEdit.bmp")]
	public class McSpinEdit : McEditBase,IMcTextBox
	{		
		#region Members
        protected byte TextMargin = 2;

		private NumberBox	m_TextBox;
        private ButtonAlign m_ButtonsAlign;

		private System.Windows.Forms.Timer countTimer;
		private IntPtr m_ptr;
        private decimal m_Increment; 
		private bool	m_ButtonVisible;

        private Nistec.WinForms.Controls.McUpDown m_Button;

		public event EventHandler ValueChanged = null;

		#endregion

		#region Constructors
		public McSpinEdit():base()
		{
			//countTimer = null;
		    m_ptr=IntPtr.Zero ;
			m_Increment=1;
			m_ButtonVisible=true;

			//m_ButtonWidth = 16;
            m_ButtonsAlign = ButtonAlign.Right;
	        SetStyle(ControlStyles.FixedHeight,true );
			InitializeComponent();
	
			m_TextBox.m_netFram=true;
            //m_TextBox.DecimalPlaces =0;
            //m_TextBox.AppendNumberText("0");
            this.OnControlLayoutChanged(EventArgs.Empty);
 			countTimer = new System.Windows.Forms.Timer();
			countTimer.Enabled = false;
			countTimer.Tick   += new System.EventHandler(OnTimerTick);
		}

        //internal McSpinEdit(bool net):this()
        //{
        //    this.m_netFram=net;
        //}

		#endregion

		#region Dispose
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
//				if(components != null){
//					components.Dispose();
//				}
			}
			base.Dispose( disposing );
		}
		#endregion

		#region Component Designer generated code
		private void InitializeComponent()
		{
			this.m_TextBox = new Nistec.WinForms.NumberBox();
			this.m_Button = new Nistec.WinForms.Controls.McUpDown(this);
			this.SuspendLayout();
			// 
			// m_TextBox
			// 
            this.m_TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.m_TextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.m_TextBox.DefaultValue = new System.Decimal(new int[] {
																		   0,
																		   0,
																		   0,
																		   0});
			this.m_TextBox.Format = "N";
			this.m_TextBox.FormatType = NumberFormats.StandadNumber;
			this.m_TextBox.Location = new System.Drawing.Point(3, 2);
			this.m_TextBox.Name = "m_TextBox";
			this.m_TextBox.TabIndex = 0;
            //this.m_TextBox.Text = "0.00";
            //this.m_TextBox.Value = new System.Decimal(new int[] {
            //                                                        0,
            //                                                        0,
            //                                                        0,
            //                                                        0});
			this.m_TextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_TextBox_KeyDown);
			this.m_TextBox.GotFocus += new System.EventHandler(this.m_TextBox_OnGotFocus);
			this.m_TextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.m_TextBox_KeyUp);
			this.m_TextBox.TextChanged += new System.EventHandler(this.m_TextBox_TextChanged);
			this.m_TextBox.LostFocus += new System.EventHandler(this.m_TextBox_OnLostFocus);
			this.m_TextBox.ValueChanged+=new EventHandler(m_TextBox_ValueChanged);
            this.m_TextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_TextBox_KeyPress);
            // 
			// m_Button
			// 
			this.m_Button.Location = new System.Drawing.Point(100, 2);
			this.m_Button.Name = "McUpDown";
			this.m_Button.Size = new System.Drawing.Size(16, 16);
			this.m_Button.TabIndex = 1;
			this.m_Button.DownPress +=new MouseEventHandler(Mc_DownPress);
			this.m_Button.DownRelease +=new MouseEventHandler(Mc_DownRelease);
			this.m_Button.UpPress +=new MouseEventHandler(Mc_UpPress);
			this.m_Button.UpRelease +=new MouseEventHandler(Mc_UpRelease);
			// 
			// McSpinEdit
			// 
			this.Controls.Add(this.m_TextBox);
			this.Controls.Add(this.m_Button);
			this.Name = "McSpinEdit";
			this.Size = new System.Drawing.Size(118, 19);
			this.ResumeLayout(false);
		}
		#endregion

		#region UpDown

		private void OnTimerTick(object sender, EventArgs e)
		{
			if (m_Button.IsMouseDown && this.ContainsFocus  )
			{
				IntPtr ptr= Nistec.Win32.WinAPI.GetActiveWindow();
				if(m_ptr!=ptr || m_ptr == IntPtr.Zero)
				{
					m_Button.IsMouseDown=false;
					return;
				}
				if (m_Button.KeySumDown)
				 {
					 this.ChangeValue(true);
				 }
				 else if (m_Button.KeyMinusDown)
				 {
					 this.ChangeValue(false);
				 }
				ptr=IntPtr.Zero ;
			}
			else
				ReleaseUpDown();

		}

		private void Mc_UpPress (object sender, MouseEventArgs e)
		{
			SetUpDown(e);
		}
		private void Mc_UpRelease (object sender, MouseEventArgs e)
		{
			ReleaseUpDown();	
		}
	
		private void Mc_DownPress (object sender, MouseEventArgs e)
		{
			SetUpDown(e);
		}
		private void Mc_DownRelease(object sender, MouseEventArgs e)
		{
			ReleaseUpDown();	
		}


		private void SetUpDown(MouseEventArgs e)
		{
			if(this.Enabled && !this.ReadOnly && e.Button == MouseButtons.Left)
			{
				m_ptr=IntPtr.Zero; 
				m_ptr= Nistec.Win32.WinAPI.GetActiveWindow();
				this.Focus ();
				this.Invalidate(false);
				this.OnTimerTick(this, e);
				this.countTimer.Interval =200;
				this.countTimer.Enabled = true;
			}
		}

		private void ReleaseUpDown()
		{
			m_ptr=IntPtr.Zero;
			this.countTimer.Stop (); 
			this.countTimer.Enabled = false;
			this.Invalidate(false);
		}

		private void ChangeValue(bool ChangeUp)
		{
            if (ReadOnly)
                return;           

			decimal val = Value;
			if (ChangeUp)
			{
				//decimal val = (decimal)Types.StringToDouble(this.Text);
				val+=m_Increment;
				if(val <=   (decimal)m_TextBox.MaxValue)
				{
					//this.Text = val.ToString(CultureInfo.CurrentCulture);
					this.Value=val;
				}
			}
			else
			{
				//decimal val = (decimal)Types.StringToDouble(this.Text);
				val-=m_Increment;
				if(val >= (decimal)m_TextBox.MinValue)
				{
					//this.Text = val.ToString(CultureInfo.CurrentCulture);
					this.Value=val;
				}
			}
		}

		#endregion

		#region McTextBox Event Methods

		private void m_TextBox_OnLostFocus(object sender, System.EventArgs e)
		{
			this.OnLostFocus (e);
		}

		private void m_TextBox_OnGotFocus(object sender, System.EventArgs e)
		{
			this.OnGotFocus (e);
		}

		private void m_TextBox_TextChanged(object sender, System.EventArgs e)
		{
			base.OnTextChanged(e);
		}

		private void m_TextBox_ValueChanged(object sender, EventArgs e)
		{
			this.OnValueChanged(e);
		}

		private void m_TextBox_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Up)
			{
                m_Button.PreformUpRelease ();
			}
			else if (e.KeyCode == Keys.Down)
			{
				m_Button.PreformDownRelease ();
			}
		}

		private void m_TextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
            if (ReadOnly)
            {
                return;
            }

			if (e.KeyCode == Keys.Up)
			{
				m_Button.PreformUpPress ();
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.Down)
			{
				m_Button.PreformDownPress ();
				e.Handled = true;
			}//Fix_Value
			else if ((e.KeyCode == Keys.Return) && this.m_TextBox.UserEdit)
			{
				this.m_TextBox.ValidateEditText();
			}
		}

        private void m_TextBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            this.OnKeyPress(e);
        }
 		#endregion

		#region Overrides

		protected virtual void OnValueChanged(EventArgs e)
		{
			if(ValueChanged!=null)
			  ValueChanged(this,EventArgs.Empty);
		}

		protected override void OnMouseWheel(MouseEventArgs e)
		{
            if (ReadOnly)
            {
                base.OnMouseWheel(e);
                return;
            }

			bool flag1 = true;
			int wheelDelta = e.Delta;
			if (Math.Abs(wheelDelta) >= 120)
			{
				if (wheelDelta < 0)
				{
					flag1 = false;
				}
				if (flag1)
				{
					ChangeValue(true);// m_Button.PreformUpPress ();
				}
				else
				{
					ChangeValue(false);//m_Button.PreformDownPress ();
				}
				wheelDelta = 0;
			}
			base.OnMouseWheel(e);
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged (e);
            OnButtonAlignChanged(e); //SetSize();
		}

        protected virtual void OnButtonAlignChanged(EventArgs e)
        {
  
            if (this.m_TextBox == null)
                return;

            Rectangle rect = this.ClientRectangle;
            int yPos = (this.Height - m_TextBox.Height) / 2;
            int borderMargin = 1;
            int textWidth=0;//
          
            if (!ButtonVisible)// ControlLayout==ControlLayout.System)
            {
                textWidth=rect.Width-(this.TextMargin*2)-(borderMargin*2);
                this.m_TextBox.Location = new Point(borderMargin + TextMargin, yPos);
            }
            else
            {
                m_Button.Top = ButtonPedding;
                m_Button.Height = rect.Height - (ButtonPedding * 2);// -4;
                textWidth = rect.Width - m_Button.Width - ButtonPedding - (this.TextMargin * 2) - (borderMargin * 2);
                    
                if (this.ButtonAlign == ButtonAlign.Left)
                {
                    m_Button.Left = rect.Left + ButtonPedding;
                    this.m_TextBox.Location = new Point(rect.X + m_Button.Right + TextMargin , rect.Y + yPos);
                }
                else
                {
                    m_Button.Left = rect.Right - m_Button.Width - ButtonPedding;
                    this.m_TextBox.Location = new Point(rect.X + TextMargin , rect.Y + yPos);
                }
  
            }
            this.m_TextBox.Size = new Size(textWidth, Size.Height - 4);
   
            this.Invalidate();
        }

        protected override void OnControlLayoutChanged(EventArgs e)
        {

            if (!ButtonVisible)
                goto Label_01;

            if (base.ControlLayout == ControlLayout.Flat)
            {
                ButtonPedding = 0;
                this.m_Button.ControlLayout = ControlLayout.Visual;
            }
            else
            {
                ButtonPedding = 2;
                this.m_Button.ControlLayout = ControlLayout;
            }

            Label_01:
            OnButtonAlignChanged(e);// this.SetSize(); //OnButtonAlignChanged(e);
            base.OnControlLayoutChanged(e);

            this.Invalidate();
        }

        //protected void SetSize()
        //{
        //    //base.SetSize ();

        //    Rectangle rect=this.ClientRectangle;

        //    if(m_ButtonVisible)
        //    {
        //        m_TextBox.Width = rect.Width - m_Button.Width - (2+TextMargin + ButtonPedding*2);// 6;
        //    }
        //    else
        //    {
        //        m_TextBox.Width = rect.Width - (2+TextMargin + ButtonPedding*2);// 4;
        //        return;
        //    }

        //    //if (BorderStyle == BorderStyle.None)
        //    //    m_TextBox.Top = 0;
        //    //else
        //    m_TextBox.Top = (rect.Height - m_TextBox.Height) / 2;
        //    m_Button.Top = ButtonPedding;
        //    if (m_ButtonsAlign == ButtonAlign.Left)
        //    {
        //        m_Button.Left = rect.Left + ButtonPedding;
        //        m_TextBox.Left = m_Button.Right + TextMargin;
        //    }
        //    else
        //    {
        //        m_Button.Left = rect.Right - m_Button.Width - ButtonPedding;
        //        m_TextBox.Left = rect.Left + TextMargin;
        //    }
        //    m_Button.Height = rect.Height - (ButtonPedding*2);// -4;
        //}
		#endregion

		#region Validation

		protected override void OnValidating(System.ComponentModel.CancelEventArgs e)
		{
			base.OnValidating(e);
			IsValidating();// !mCalendar.IsValidateDate ();
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
            if (e.PropertyName.Equals("ControlLayout") || e.PropertyName.Equals("BackColor"))
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
                this.m_TextBox.ForeColor = base.ForeColor;
            }
            else if (force)
            {
                base.ForeColor = value;
                this.m_TextBox.ForeColor = value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected void SerializeBackColor(Color value, bool force)
        {
            if (ShouldSerializeBackColor())
            {
                base.BackColor = LayoutManager.Layout.BackColorInternal;
                this.m_TextBox.BackColor = base.BackColor;
            }
            else if (force)
            {
                base.BackColor = value;
                this.m_TextBox.BackColor = value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected bool ShouldSerializeBackColor()
        {
            return IsHandleCreated && StylePainter != null;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected bool ShouldSerializeForeColor()
        {
            return IsHandleCreated && StylePainter != null;
        }

        #endregion

 		#region Appearance Property

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

 		[Category("Appearance"),DefaultValue(true)]
		public bool ButtonVisible
		{
			get{return m_ButtonVisible;}
			set
			{
				if(m_ButtonVisible!=value)
				{
					m_ButtonVisible=value;
					this.m_Button.Visible=value;
                    OnControlLayoutChanged(EventArgs.Empty);
					this.Invalidate();
				}
			}
		}

		[Category("Appearance"),DefaultValue(LeftRight.Right)]
        public ButtonAlign ButtonAlign
		{
			get{ return m_ButtonsAlign; }

			set{ 
				if(m_ButtonsAlign != value)
				{
					m_ButtonsAlign = value;
                    OnButtonAlignChanged(EventArgs.Empty); //SetSize();
					Invalidate(false);
				}
			}
		}

		[DefaultValue("0"),Browsable(false),Category("Appearance"),Bindable(true),EditorBrowsable(EditorBrowsableState.Advanced),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string Text
		{
			get{ return m_TextBox.Text; }
			set{this.m_TextBox.Text = value;}
		}

		[Category("Behavior"),DefaultValue(typeof(decimal),"1")]
		public decimal Increment
		{
			get{ return m_Increment; }

			set{
				if(value >0)
				{
                  this.m_Increment=value;
				}
			   }
		}

        //[Category("Behavior"),Description ("RangeType of min and max value")]
        //public RangeNumber RangeValue
        //{
        //    get	{return m_TextBox.RangeValue ;}
        //    set	
        //    {   
        //        m_TextBox.RangeValue=value;
        //    }
        //}
        [Category("Behavior"), Description("min value")]
        public decimal MinValue
        {
            get { return m_TextBox.MinValue; }
            set
            {
                m_TextBox.MinValue = value;
            }
        }
        [Category("Behavior"), Description("max value")]
        public decimal MaxValue
        {
            get { return m_TextBox.MaxValue; }
            set
            {
                m_TextBox.MaxValue = value;
            }
        }

	
		[DefaultValue(0),Category("Behavior"),Bindable(true)]
		public decimal Value
		{
			get{return m_TextBox.Value;}
			set{m_TextBox.Value=value;}
		}

		[Category("Appearance"),DefaultValue(2)]   
		public int DecimalPlaces
		{
			get {return m_TextBox.DecimalPlaces;}
			set 
			{
				m_TextBox.DecimalPlaces = value;
				Invalidate ();  
			}
		}
		[Category("Behavior"),DefaultValue(HorizontalAlignment.Left )]
		public HorizontalAlignment TextAlign
		{
			get{ return m_TextBox.TextAlign; }
			set{ m_TextBox.TextAlign = value; }
		}

        [Category("Behavior"), DefaultValue("0")]
        public override string DefaultValue
        {
            get { return base.DefaultValue; }
            set
            {
                if (base.DefaultValue != value)
                {
                    if (!WinHelp.IsNumber(value))
                    {
                        throw new FormatException("Incorrect value ,number required");
                    }
                    base.DefaultValue = value;
                }
            }
        }
		#endregion

		#region IMcTextBox Members

		[Category("Appearance"),DefaultValue(NumberFormats.GeneralNumber)]   
		public NumberFormats FormatType
		{
			get{return m_TextBox.FormatType;}
			set
			{
				m_TextBox.FormatType=value;
				this.Invalidate();
			}
		}

		[Category("Appearance"),DefaultValue("")]
		public string Format
		{
			get {return m_TextBox.Format;}
			set {m_TextBox.Format = value;}
		}

		[Category("Behavior"),DefaultValue(false)]
		public override bool ReadOnly
		{
			get {return m_TextBox.ReadOnly;}
			set
			{//if(m_TextBox.ReadOnly != value)
				m_TextBox.ReadOnly = value;}
		}

		public void AppendText(string value)
		{
			m_TextBox.AppendText (value);  
		}

		public override void ResetText()
		{
			m_TextBox.ResetText() ;  
		}

		public void AppendFormatText(string value)
		{
			m_TextBox.AppendNumberText(value);// AppendFormatText(value);  
		}

		[Browsable(false)]
		protected override FieldType BaseFormat 
		{
			get{return m_TextBox.BaseFormat;}
		}

        [Browsable(false)]
        public override bool Focused
        {
            get { return m_TextBox.Focused; }
        }

        public new bool Focus()
        {
            return m_TextBox.Focus();
        }

        public virtual void SelectAll()
        {
            //if (!m_TextBox.IsDisposed)
                m_TextBox.SelectAll();
        }

        [Description("SelectionLength"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int SelectionLength
        {
            get
            {
                return this.m_TextBox.SelectionLength;
             }
            set
            {
                this.m_TextBox.SelectionLength = value;
             }
        }

        [Description("SelectionStart"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionStart
        {
            get
            {
                 return this.m_TextBox.SelectionStart;
            }
            set
            {
                this.m_TextBox.SelectionStart = value;
             }
        }

        public void Select(int start, int length)
        {
            this.m_TextBox.Select(start, length);
        }


		#endregion

		#region IControlKeyAction Members

		protected override bool OnEnterAction()//EnterAction action)
		{
            IsValidating();
                //OnValueChanged(EventArgs.Empty);
			return false;
		}

		protected void ActionUndo()
		{
			m_TextBox.ActionUndo(); 
		}
		
		protected void ActionDefaultValue()
		{
			m_TextBox.ActionDefaultValue(); 
		}

		public override bool IsValidating()
		{
			if(! this.m_TextBox.CausesValidation )
			{
				m_IsValid=true;
				return true;
			}
			string msg="";
			bool ok= m_TextBox.IsValidating (ref msg);
	
			if(!ok)
			{
				m_IsValid=false;
				OnErrorOcurred( new ErrorOcurredEventArgs(msg));
			}
			else
			{
				if(m_TextBox.Modified)
					m_TextBox.AppendNumberText (Text);
				m_IsValid=true;
			}
			return ok;

		}

		#endregion

		#region IBind Members

		[Browsable(false)]
		public override BindingFormat BindFormat
		{
			get{return BindingFormat.Decimal;}
		}

		public override string BindPropertyName()
		{
			return "Value";
		}
        public override void BindDefaultValue()
        {
            //if (this.DefaultValue.Length > 0)
            //{
                this.Value =Types.ToDecimal( this.DefaultValue,0);
                if (base.IsHandleCreated)
                {
                    this.OnValueChanged(EventArgs.Empty);
                }
            //}
        }
		#endregion

	}
}
