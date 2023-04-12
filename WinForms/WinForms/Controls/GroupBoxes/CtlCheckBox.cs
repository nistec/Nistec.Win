using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

using Nistec.Data;
using Nistec.WinForms.Controls;
using Nistec.Win;

namespace Nistec.WinForms
{


	[Designer(typeof(Design.CheckBoxDesigner))]
	[DefaultEvent("CheckedChanged")]
	[System.ComponentModel.ToolboxItem(true)]
	[ToolboxBitmap (typeof(McCheckBox), "Toolbox.CheckBox.bmp")]
	public class McCheckBox : Nistec.WinForms.Controls.McBase,ICheckBox,ILayout,IBind	
	{

		#region Enums
//		public enum  CheckBoxValues
//		{  
//			/// <summary>
//			/// Checked value 0=False , 1= True
//			/// </summary>
//			Bit=1,
//			/// <summary>
//			/// Checked value 0=False , -1= True
//			/// </summary>
//			Vb=2
//		}
		#endregion

		#region Members
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.CheckState m_CheckState; 
 		private bool			m_ReadOnly;

		private LeftRight m_CheckAlign;


		private Appearance appearance;
		private bool autoCheck;
		private bool threeState=false;


		//private bool m_CheckFlash = false;
		//private bool m_Checked;
		//private bool m_ReadOnly = false;
		//private string m_Text = String.Empty;	
      	//private Color m_CheckColor = Color.CornflowerBlue;
        //private CheckBoxValues m_CheckBoxValue; 
          
		//[Category("Action")]
		//public event System.EventHandler CheckedChanged;

		// Events
		[Category("PropertyChanged"), Description("CheckBoxOnAppearanceChanged")]
		public event EventHandler AppearanceChanged;
		[Description("CheckBoxOnCheckedChanged")]
		public event EventHandler CheckedChanged;
		[Description("CheckBoxOnCheckStateChanged")]
		public event EventHandler CheckStateChanged;
		//[EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
		//public event EventHandler DoubleClick;


		#endregion

		#region Constructors

		public McCheckBox():base(BorderStyle.None)
		{
			//this.checkAlign = ContentAlignment.MiddleLeft;
			this.autoCheck = true;
			//base.BorderStyle = BorderStyle.None;
	
			base.SetStyle(ControlStyles.StandardDoubleClick | ControlStyles.StandardClick, false);
			base.SetStyle(ControlStyles.SupportsTransparentBackColor ,true);
			InitializeComponent();

			m_CheckAlign = LeftRight.Left;
			//m_Checked = false;
			m_CheckState=System.Windows.Forms.CheckState.Unchecked; 
			//m_CheckBoxValue=CheckBoxValues.Bit;
  
		}

        //internal McCheckBox(bool net):this()
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
			// McCheckBox
			// 
			this.Name = "McCheckBox";
			this.Size = new System.Drawing.Size(80, 20);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CheckBox_MouseUp);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CheckBox_KeyUp);
		}
		#endregion

		#region Events handlers

        public void ToggleChecked()
        {
            if (!ReadOnly)
            {
                this.Checked = !this.Checked;
                OnCheckedChanged(EventArgs.Empty);
            }
        }

		private void CheckBox_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
            ToggleChecked();
		}

		private void CheckBox_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(!this.ReadOnly )
			{
				if(e.KeyData == Keys.Space)
					ToggleChecked();
				else if(e.KeyData == Keys.Oemplus)
                    this.Checked = true;
				else if(e.KeyData == Keys.OemMinus)
					this.Checked = false;
			}
		}

		#endregion

		#region Overrides

		protected override void OnClick(EventArgs e)
		{
			if (this.autoCheck)
			{
				switch (this.CheckState)
				{
					case CheckState.Unchecked:
					{
						this.CheckState = CheckState.Checked;
						break;
					}
					case CheckState.Checked:
					{
						if (!this.threeState)
						{
							this.CheckState = CheckState.Unchecked;
							break;
						}
						this.CheckState = CheckState.Indeterminate;
						break;
					}
					default:
					{
						this.CheckState = CheckState.Unchecked;
						break;
					}
				}
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

		#region Virtuals

		protected virtual void OnCheckedChanged(System.EventArgs e)
		{
			if(this.CheckedChanged != null)
			{
				this.CheckedChanged(this,e);
			}
		}

		protected virtual void OnCheckStateChanged(System.EventArgs e)
		{
			if(this.CheckStateChanged != null)
			{
				this.CheckStateChanged(this,e);
			}
		}

		#endregion
		
		#region Internal helpers

		public Rectangle CheckRect()
		{

			Rectangle rect;
			if(m_CheckAlign == LeftRight.Left){
				rect = new Rectangle(4,(this.Height - 12)/2,12,12);
			}
			else{
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

        [Category("Style"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
                    if (appearance == Appearance.Button && this.BorderStyle == BorderStyle.None)
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
			get{return CheckBoxTypes.CheckBox;}
		}


		[Bindable(true), Category("Appearance"), RefreshProperties(RefreshProperties.All), Description("CheckBoxChecked"), DefaultValue(false)]
		public bool Checked
		{
			get
			{
				return (this.m_CheckState != CheckState.Unchecked);
			}
			set
			{
				if (value != this.Checked)
				{
					this.CheckState = value ? CheckState.Checked : CheckState.Unchecked;
				}
			}
		}

		[DefaultValue(CheckState.Unchecked), RefreshProperties(RefreshProperties.All), Bindable(true), Category("Appearance"), Description("CheckBoxCheckState")]
		public CheckState CheckState
		{
			get
			{
				return m_CheckState;
			}
			set
			{
				if (!Enum.IsDefined(typeof(CheckState), value))
				{
					throw new InvalidEnumArgumentException("value", (int) value, typeof(CheckState));
				}
				if (this.m_CheckState != value)
				{
					bool flag1 = this.Checked;
					this.m_CheckState = value;
					if (base.IsHandleCreated)
					{
						//NativeMethods.SendMessage(0xf1, (int) this.m_CheckState, 0);
                        SendMessage(0xf1, (int)this.m_CheckState, 0);
                        this.Invalidate();
					}
					if (flag1 != this.Checked)
					{
						this.OnCheckedChanged(EventArgs.Empty);
					}
					this.OnCheckStateChanged(EventArgs.Empty);
				}
			}
		}
        internal IntPtr SendMessage(int msg, int wparam, int lparam)
        {
            return Win32.WinMethods.SendMessage(new System.Runtime.InteropServices.HandleRef(this, this.Handle), msg, wparam, lparam);
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

		#endregion

		#region IBind Members

		[Browsable(false)]
		public virtual BindingFormat BindFormat
		{
			get{return BindingFormat.Boolean;}
		}

		public string BindPropertyName()
		{
			return "Checked";
		}

        public virtual void BindDefaultValue()
        {
            bool bValue = false;
            this.m_CheckState = bValue ? CheckState.Checked : CheckState.Unchecked;
            if (base.IsHandleCreated)
            {
                SendMessage(0xf1, (int)this.m_CheckState, 0);
                this.Invalidate();
            }
            this.OnCheckedChanged(EventArgs.Empty);

        }

		#endregion
	}
}
