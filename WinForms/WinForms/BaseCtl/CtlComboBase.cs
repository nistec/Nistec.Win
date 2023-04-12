using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Nistec.Drawing;
using System.Collections;
using System.Runtime.InteropServices;

using Nistec.Win32;

namespace Nistec.WinForms.Controls
{

	[DefaultEvent("ButtonClick"),
	System.ComponentModel.ToolboxItem(false)]
	public class McComboBase : McButtonEdit,IMcTextBox 
	{

		#region Members
		internal McTextBoxBase m_TextBox;

		#endregion

		#region Constructors

        internal McComboBase()
            : base()
		{
			InitializeComponent();
			//this.m_Button.m_netFram=true;
			//this.m_TextBox.m_netFram=true;
			//m_TextBox.Bounds =base.m_TextRect;
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
			}
			base.Dispose( disposing );
		}

		#endregion

		#region Component Designer generated code
		private void InitializeComponent()
		{
			//System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(McComboBase));
			this.m_TextBox = new Nistec.WinForms.Controls.McTextBoxBase(true);
			this.SuspendLayout();
			// 
			// m_TextBox
			// 
			this.m_TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.m_TextBox.BackColor = System.Drawing.Color.White;
			this.m_TextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.m_TextBox.Location = new System.Drawing.Point(2, 2);
			this.m_TextBox.Name = "m_TextBox";
			this.m_TextBox.Size =new System.Drawing.Size(78, 13);
			this.m_TextBox.TabIndex = 0;
			this.m_TextBox.Text = "";
			this.m_TextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_TextBox_KeyDown);
			this.m_TextBox.Leave += new System.EventHandler(this.m_TextBox_Leave);
			this.m_TextBox.LostFocus += new System.EventHandler(this.m_TextBox_OnLostFocus);
			this.m_TextBox.GotFocus += new System.EventHandler(this.m_TextBox_OnGotFocus);
			this.m_TextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_TextBox_KeyPress);
			//this.m_TextBox.ErrorOcurred += new Nistec.WinForms.ErrorOcurredEventHandler(this.m_TextBoxErrorOcurred);
			this.m_TextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.m_TextBox_KeyUp);
			this.m_TextBox.TextChanged += new System.EventHandler(this.m_TextBox_TextChanged);
            // 
			// McComboBase
			// 
			//this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.m_TextBox);
			this.Name = "McComboBase";
			//this.Size = base.Size;//new System.Drawing.Size(104, 20);
			this.ResumeLayout(false);

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
			base.OnLostFocus (e);
		}

		#endregion

		#region Override

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged (e);
            OnControlLayoutChanged(e);

		}


		protected override void OnEnter(EventArgs e)
		{
			base.OnEnter (e);
			this.m_TextBox.Focus();  
			m_TextBox.SelectAll();
		}

        protected override void OnButtonAlignChanged(EventArgs e)
        {
            base.OnButtonAlignChanged(e);

            if (this.m_TextBox == null)
                return;

            int yPos = (this.Height - m_TextBox.Height) / 2;


            if (!ButtonVisible)// ControlLayout==ControlLayout.System)
            {
                this.m_TextBox.Size = new Size(Size.Width - 6, Size.Height - 4);
                yPos = (this.Height - m_TextBox.Height) / 2;
                this.m_TextBox.Location = new Point(2, yPos);
            }
            else
            {
                Rectangle rect = this.ClientRectangle;

                if (this.ButtonAlign == ButtonAlign.Left)
                {
                    this.m_TextBox.Size = new Size(this.Width - m_Button.Width - 6, rect.Height - 4);
                    yPos = ((rect.Height - m_TextBox.Height) / 2);
                    this.m_TextBox.Location = new Point(rect.X + m_Button.Left + m_Button.Width + 2, rect.Y + yPos);
                }
                else
                {
                    this.m_TextBox.Size = new Size(rect.Width - m_Button.Width - 6, rect.Height - 6);
                    yPos = ((rect.Height - m_TextBox.Height) / 2);
                    this.m_TextBox.Location = new Point(rect.X + 2, rect.Y + yPos);
                }
            }

            this.Invalidate();
        }

        protected override void OnControlLayoutChanged(EventArgs e)
		{
            base.OnControlLayoutChanged(e);

 			this.Invalidate();
		}

		[Browsable(false)]
		public override bool Focused
		{
			get{ return m_TextBox.Focused; }
		}

		public new  bool Focus()
		{
			return m_TextBox.Focus ();
		}

		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged (e);
			if(this.m_TextBox!=null)
			{
				this.m_TextBox.Font=base.Font;
			}
		}

		protected override void OnRightToLeftChanged(EventArgs e)
		{
			base.OnRightToLeftChanged (e);
			if(this.m_TextBox!=null)
			{
				this.m_TextBox.RightToLeft=base.RightToLeft;
			}
		}
	
		#endregion

		#region Public methods

		public virtual void  SelectAll()
		{
			if(!m_TextBox.IsDisposed)
				m_TextBox.SelectAll ();
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
				this.m_TextBox.SelectionLength=value;
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
				this.m_TextBox.SelectionStart=value;
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
		
//		[Category("Appearance"),DefaultValue(false)]
//		public  bool AutoHide
//		{
//			get {return m_AutoHide;}
//			set
//			{
//				if(m_AutoHide != value)
//				{
//					m_AutoHide = value;
//					if(m_AutoHide)
//						this.m_Button.Visible=this.Focused;
//					this.Invalidate(false);
//				}
//			}
//		}

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

		[Category("Behavior"),DefaultValue("")]//,DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string DefaultValue
		{
			get {return base.DefaultValue;}
			set
			{
				base.DefaultValue=value;
				//this.m_TextBox.DefaultValue=value;
			}
		}

		[Category("Behavior"),DefaultValue(false)]
		public override bool ReadOnly
		{
			get {return base.ReadOnly;}
			set
			{
				if(ReadOnly != value)
				{
					base.ReadOnly = value;
					m_TextBox.ReadOnly=value;
					this.Invalidate(false);
				}
			}
		}
		[Browsable(true),Bindable(true),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
		Category("Appearance")]
		public override string Text
		{
			get {return m_TextBox.Text;}
			set
			{
				//if(! AcceptItemsOnly )//this.Enabled && !m_TextBox.ReadOnly) 
				m_TextBox.Text = value;
			}
		}


		[Category("Appearance"),DefaultValue(HorizontalAlignment.Left)]
		public override HorizontalAlignment TextAlign
		{
			get {return m_TextBox.TextAlign;}
			set 
			{
				if(m_TextBox.TextAlign !=value)
				{
					m_TextBox.TextAlign = value;
					m_TextBox.Invalidate();
					if(this.DesignMode)
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
		public override bool IsModified
		{
			get {return m_TextBox.Modified;}
			set {m_TextBox.Modified = value;}
		}

		[Category("Behavior"),DefaultValue(true)]
		public new bool CausesValidation
		{
			get {return m_TextBox.CausesValidation;}
			set{m_TextBox.CausesValidation = value;}
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

		#region IMcTextBox Members

		[DefaultValue("")]
		public virtual string Format
		{
			get{return m_TextBox.Format;}
			set{m_TextBox.Format=value;}
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
			m_TextBox.AppendFormatText(value);  
		}
	
		#endregion

	}

}
