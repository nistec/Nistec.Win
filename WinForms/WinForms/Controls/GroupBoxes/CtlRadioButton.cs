using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

using Nistec.WinForms.Controls;
using Nistec.Win;

namespace Nistec.WinForms
{
	[Designer(typeof(Design.CheckBoxDesigner))]
	[DefaultEvent("CheckedChanged"),
	 System.ComponentModel.ToolboxItem(true)]
	[ToolboxBitmap (typeof(McRadioButton), "Toolbox.RadioButton.bmp")]
	public class McRadioButton : Nistec.WinForms.Controls.McBase,ICheckBox,ILayout//, Nistec.WinForms.IFlashabledControl
	{
		#region Members
		private System.ComponentModel.IContainer components = null;
		private LeftRight m_RadioAlign;
		//private IStyleGuide	m_StyleGuide;
		//private bool m_CheckFlash = false;
		private bool m_Checked;
		private bool oldCheck;
		private bool m_ReadOnly = false;
		//private string m_Text = String.Empty;
		private int m_groupIndex;

		private Appearance appearance;
		private bool autoCheck;
		
		//private StyleDesigner m_Style;
		//private Color m_ButtonColor = SystemColors.Control ;
		//private Color m_CheckColor;

		//private FlatStyles mFlatStyle = FlatStyles.FixedSingle ;

	
		[Category("PropertyChanged"), Description("CheckBoxOnAppearanceChanged")]
		public event EventHandler AppearanceChanged;
		[Description("CheckBoxOnCheckedChanged")]
		public event EventHandler CheckedChanged;

		#endregion

		#region Constructors

		public McRadioButton()
		{
			//this.firstfocus = true;
			this.autoCheck = true;
			this.appearance = Appearance.Normal;
			this.TabStop = false;
			base.BorderStyle = BorderStyle.None;

			base.SetStyle(ControlStyles.StandardClick, false);
			SetStyle(ControlStyles.SupportsTransparentBackColor ,true);
			InitializeComponent();

			m_RadioAlign = LeftRight.Left;
			m_Checked = false;
			oldCheck = false;
			m_groupIndex = -1;
			//m_CheckColor = Color.CornflowerBlue;
		}

        //internal McRadioButton(bool net):this()
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
			// McRadioButton
			// 
			this.Name = "McRadioButton";
			this.Size = new System.Drawing.Size(80, 20);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RadioButton_MouseUp);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.RadioButton_KeyUp);
		}
		#endregion

		#region Events handlers
		
		private void RadioButton_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(!this.ReadOnly && e.KeyData == Keys.Space)
			{
				if(!this.Checked)
				{
					this.Checked = true;
					//OnCheckedChanged();
				}
			}
		}
		
		private void RadioButton_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(!this.ReadOnly)
			{
				if(!this.Checked)
				{
					this.Checked = true;
				}

				if (this.Checked != this.oldCheck) 
				{
					this.oldCheck = this.Checked;
					//OnCheckedChanged();
				}
			}
		}

		#endregion

		#region Virtuals

		protected void OnCheckedChanged(EventArgs e)
		{
			if(this.m_Checked)
			{
				if(this.Parent is McGroupBox)// && m_groupIndex> -1)
				{
					((McGroupBox)this.Parent).SetGroupIndex(m_groupIndex);
					foreach (Control ctl in this.Parent.Controls)
					{
						if (ctl is McRadioButton && ctl != this && ((McRadioButton)ctl).Checked) 
						
						{
							((McRadioButton)ctl).Checked = false;	
						}
					}

				}
			}
			if(this.CheckedChanged != null)
			{
				this.CheckedChanged(this,e);
			}
		}

		#endregion

		#region Overrides

		protected override void OnClick(EventArgs e)
		{
			if (this.autoCheck)
			{
				this.Checked = true;
			}
			base.OnClick(e);
		}
		protected virtual void OnAppearanceChanged(EventArgs e)
		{
			if (AppearanceChanged != null)
			{
				AppearanceChanged(this, e);
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint (e);
			Rectangle Crect = this.ClientRectangle;
			Rectangle rect = new Rectangle(Crect.X,Crect.Y,Crect.Width-1 ,Crect.Height-1);
            if (this.Appearance == Appearance.Button)
            {
                if (this.Checked)
                    this.LayoutManager.DrawButtonRect(e.Graphics, rect, ButtonStates.Pushed, 2);
                else
                    this.LayoutManager.DrawButtonRect(e.Graphics, rect, ButtonStates.Normal, 2);

                if (this.Text.Length > 0)
                    this.LayoutManager.DrawString(e.Graphics, rect, this.TextAlign, this.Text, this.Font, this.Enabled);
                if (this.Image != null)
                    this.LayoutManager.DrawImage(e.Graphics, rect,this.Image, this.ImageAlign, this.Enabled);
            }
            else
            {
                this.LayoutManager.DrawCheckBox(e.Graphics, rect, this);
            }

		}

//		protected override void DrawControl(Graphics g,bool hot,bool focused)
//		{
//			//Rectangle checkRect = CheckRect();
//			Rectangle Crect = this.ClientRectangle;
//			Rectangle rect = new Rectangle(Crect.X,Crect.Y,Crect.Width-1 ,Crect.Height-1);
//
//			this.LayoutManager.DrawCheckBox(g,rect,this);
//
////			if(this.appearance ==Appearance.Button)
////			{
////				if(this.Checked)
////					ControlPaint.DrawButton(g,rect,ButtonState.Pushed);
////				else
////					ControlPaint.DrawButton(g,rect,ButtonState.Normal);
////
////				m_Style.DrawString(g,rect,this.TextAlign);
////			}
////			else
////			{
////				m_Style.DrawCheckBoxColor(g,rect,hot,focused);  
////			}
//		}
//
		protected override bool ProcessMnemonic(char charCode)
		{
			if (this.CanSelect && IsMnemonic(charCode, Text))
			{
				this.Checked = !Checked; 
				this.Invalidate();
				OnCheckedChanged(EventArgs.Empty);
				this.Focus();
				return true;
			}
			else {return false;}
		}

		#endregion

		#region Internal helpers

		internal void CheckedInternal(bool value)
		{
			m_Checked = value;
			this.OnCheckedChanged(new EventArgs());

			if (m_Checked && this.Parent != null)
			{
				foreach (Control ctl in this.Parent.Controls)
				{
					if (ctl is McRadioButton && 
						ctl != this && ((McRadioButton)ctl).Checked) 
						//((McRadioButton)ctl).GroupIndex == this.GroupIndex) 
					{
						((McRadioButton)ctl).CheckedInternal(false);// .Checked = false;	
					}
				}
			}

			this.Invalidate(false);				

		}

		public Rectangle CheckRect()
		{
			Rectangle rect;
			if(m_RadioAlign == LeftRight.Left)
			{
				rect = new Rectangle(4,(this.Height - 12)/2,12,12);
			}
			else
			{
				rect = new Rectangle((this.Width - 9)/2,(this.Height - 12)/2,12,12);
			}			
			return rect;
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
                base.BackColor = LayoutManager.Layout.BackColorInternal;
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

        [Category("Style"), DefaultValue(BorderStyle.None)]
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

		#region Properties


        [Category("Style"),Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override ControlLayout ControlLayout
        {
            get { return base.ControlLayout; }
            set
            {
                base.ControlLayout = value;
            }

        }


		[Description("CheckBoxAutoCheck"), DefaultValue(true), Category("Behavior")]
		public bool AutoCheck
		{
			get
			{
				return this.autoCheck;
			}
			set
			{
				this.autoCheck = value;
			}
		}

		internal bool OwnerDraw
		{
			get
			{
				return true;//(m_Style.StyleMc. this.FlatStyle != FlatStyle.System);
			}
		}


        [Description("CheckBoxAppearance"), DefaultValue(Appearance.Normal), Category("Appearance"), Localizable(true)]
		public Appearance Appearance
		{
			get
			{
				return this.appearance;
			}
			set
			{
				if (!Enum.IsDefined(typeof(Appearance), value))
				{
					throw new InvalidEnumArgumentException("value", (int) value, typeof(Appearance));
				}
				if (this.appearance != value)
				{
					this.appearance = value;
                    if (appearance==Appearance.Button && this.BorderStyle == BorderStyle.None)
                    {
                        this.BorderStyle = BorderStyle.FixedSingle;
                    }
                    if (this.OwnerDraw)
					{
						this.Refresh();
					}
					else
					{
						base.UpdateStyles();
					}
					this.OnAppearanceChanged(EventArgs.Empty);
				}
			}
		}
 

		[Category("Behavior"),DefaultValue(false)]
		public virtual bool ReadOnly
		{
			get {return m_ReadOnly;}
			set
			{
				if(m_ReadOnly != value)
				{
					m_ReadOnly = value;
					this.Invalidate(false);
				}
			}
		}

		public CheckBoxTypes CheckBoxType
		{
			get{return CheckBoxTypes.RadioButton;}
		}

		[DefaultValue(false), MergableProperty(false), Bindable(true),Category("Appearance")]
		public bool Checked
		{
			get {return m_Checked;}

			set
			{
				if(m_Checked != value)
				{
					m_Checked = value;
					this.OnCheckedChanged(new EventArgs());
					this.Invalidate(false);				
				}
			}
		}

//		[Category("Behavior"),DefaultValue(false)]
//		public bool ReadOnly
//		{
//			get {return m_ReadOnly;}
//			set
//			{
//				m_ReadOnly = value;
//				this.Invalidate(false);
//			}
//		}

		[Browsable(true),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
		Category("Appearance"),
		Bindable(true)]
		public override string Text
		{
			get {return base.Text;}
			set
			{
				base.Text = value;
				this.Invalidate();
			}
		}

		[Browsable(true),DefaultValue(-1),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
		Category("Behavior")]
		public int GroupIndex
		{
			get{return m_groupIndex;}
			set{m_groupIndex = value;}
		}
		
		#endregion

	}
}