using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using mControl.Util;
using mControl.WinCtl.Controls;

 
namespace mControl.GridStyle.Controls
{	
	[System.ComponentModel.ToolboxItem(false)]
	public class MaskedControl : mControl.WinCtl.Controls.CtlEditBase,ICtlTextBox
	{
		#region Members
		
		private mControl.WinCtl.Controls.CtlMaskedBase  m_TextBox;
		private System.ComponentModel.IContainer components = null;
		//protected const int minHeight=13;


		[Category("Behavior")]
		//public event ErrorOcurredEventHandler ErrorOcurred  = null;
		public new event EventHandler TextChanged = null;
	
		#endregion

		#region Constructors
		
		public MaskedControl():base()
		{
			base.m_FixSize=false;
			base.SetStyle(ControlStyles.StandardDoubleClick | ControlStyles.StandardClick, false);
			InitializeComponent();
			this.m_TextBox.TabIndex = this.TabIndex ;
			ResizeRedraw =true;
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated (e);
			SetSize ();
		}

		#endregion

		#region Dispose

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null){
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#endregion

		#region Component Designer generated code

		private void InitializeComponent()
		{
			this.m_TextBox = new mControl.WinCtl.Controls.CtlMaskedBase();
			this.SuspendLayout();
			// 
			// m_TextBox
			// 
			this.m_TextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.m_TextBox.Location = new System.Drawing.Point(0, 0);
			this.m_TextBox.Name = "m_TextBox";
			this.m_TextBox.TabIndex = 0;
			this.m_TextBox.Text = "";
			this.m_TextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_TextBox_KeyDown);
			this.m_TextBox.Leave += new System.EventHandler(this.m_TextBox_Leave);
			this.m_TextBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_TextBox_MouseDown);
			this.m_TextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_TextBox_KeyPress);
			this.m_TextBox.DoubleClick += new System.EventHandler(this.m_TextBox_DoubleClick);
			this.m_TextBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_TextBox_MouseUp);
			this.m_TextBox.Validating += new System.ComponentModel.CancelEventHandler(this.m_TextBox_Validating);
			//this.m_TextBox.ErrorOcurred += new mControl.WinCtl.Controls.ErrorOcurredEventHandler(this.m_TextBox_ErrorOcurred);
			this.m_TextBox.Validated += new System.EventHandler(this.m_TextBox_Validated);
			this.m_TextBox.MouseHover += new System.EventHandler(this.m_TextBox_MouseHover);
			this.m_TextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.m_TextBox_KeyUp);
			this.m_TextBox.MouseEnter += new System.EventHandler(this.m_TextBox_MouseEnter);
			this.m_TextBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.m_TextBox_MouseMove);
			this.m_TextBox.MouseLeave += new System.EventHandler(this.m_TextBox_MouseLeave);
			this.m_TextBox.TextChanged += new System.EventHandler(this.m_TextBox_TextChanged);
			this.m_TextBox.Enter += new System.EventHandler(this.m_TextBox_Enter);
			this.m_TextBox.LostFocus += new System.EventHandler(this.m_TextBox_OnLostFocus);
			this.m_TextBox.GotFocus  += new System.EventHandler(this.m_TextBox_OnGotFocus);
			// 
			// EditBox
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.m_TextBox);
			this.ForeColor = System.Drawing.Color.Black;
			this.Name = "TextControl";
			this.Size = new System.Drawing.Size(104, 20);
			//this.StyleCtl.BackColor = System.Drawing.Color.White;
			//this.StyleCtl.BorderColor = System.Drawing.SystemColors.ControlDark;
			//this.StyleCtl.BorderHotColor = System.Drawing.Color.Blue;
			//this.StyleCtl.FocusedColor = System.Drawing.Color.Gold;
			//this.StyleCtl.ForeColor = System.Drawing.Color.Black;
			this.CausesValidationChanged += new System.EventHandler(this.TextBox_CausesValidationChanged);
			this.ResumeLayout(false);

		}

		#endregion

		#region Events handlers
				
		private void m_TextBox_OnGotFocus(object sender, System.EventArgs e)
		{
			//this.BorderColor  = this.StyleCtl.GetBorderColor (this.Enabled ,true,false);
			DrawControl(this.ContainsFocus);
			this.OnGotFocus(e);
		}

		private void m_TextBox_OnLostFocus(object sender, System.EventArgs e)
		{
			//this.BackColor = this.StyleCtl.GetBackColor(this.Enabled ,this.m_TextBox.ReadOnly);
			DrawControl(this.ContainsFocus);
			this.OnLostFocus(e);
		}

		private void m_TextBox_TextChanged(object sender, System.EventArgs e)
		{
			this.OnTextChanged(e);
			if (this.TextChanged !=null)
				this.TextChanged (this, e);
		}

		private void m_TextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.OnKeyDown(e);		
		}

		private void m_TextBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			this.OnKeyPress(e);
		}

		private void m_TextBox_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.OnKeyUp(e);
		}

//		private void m_TextBox_NumberOcurred(object sender, mControl.WinCtl.Controls.NumberOcurredEventArgs e)
//		{
//			e.NegativeNumberColor = this.StyleCtl.NegativeNumberColor;
//			e.PositiveNumberColor = this.StyleCtl.ForeColor  ;
//			//Text=System.Convert.ToDecimal(  Text).ToString("0.00"); 
//		}

//		private void m_TextBox_ErrorOcurred(object sender, mControl.WinCtl.Controls.ErrorOcurredEventArgs e)
//		{
//			if (e.Message.Length > 0)
//			{
//				if (this.ErrorOcurred != null)
//				{
//					ErrorOcurredEventArgs oArg = new ErrorOcurredEventArgs(e.Message);
//					ErrorOcurred(this, oArg); 
//				}
//			}
//			//
//			//if (this.ErrorBox.ShowErrorProvider) 
//			//	base.Errors.SetError(this, e.Message);
//		}

		private void TextBox_CausesValidationChanged(object sender, System.EventArgs e)
		{
			m_TextBox.CausesValidation = this.CausesValidation;
		}

		#endregion

		#region Overrides

		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged(e);
			//this.BackColor = this.StyleCtl.GetBackColor(this.m_TextBox.ReadOnly );
			this.m_TextBox.Enabled =this.Enabled;  
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged (e);
			SetSize ();
    	}

		#endregion

		#region Internal Methods

		protected override void DrawControl(Graphics g,bool hot,bool focused)
		{
			Rectangle rect =new Rectangle(0, 0, this.Width-1, this.Height-1);
	
			if(BorderStyle==BorderStyle.FixedSingle )
			{
				CtlStyleLayout.DrawBorder(g,rect,this.ReadOnly,this.Enabled,focused,hot);
			}
			else if(BorderStyle==BorderStyle.Fixed3D )
			{

				rect =new Rectangle(0, 0, this.Width, this.Height);
				ControlPaint.DrawBorder3D (g, rect, Border3DStyle.Sunken );
			}

		}

		protected override void OnStylePropertyChanged(PropertyChangedEventArgs e)
		{
			base.OnStylePropertyChanged (e);
			this.Invalidate ();
		}

		protected override void SetSize()
		{
			base.SetSize ();

			if(!IsHandleCreated)
				return;
  
			if(this.Height < (minHeight))
				this.Height=minHeight;


			if(base.FixSize )
			{
				if(BorderStyle==BorderStyle.Fixed3D  || BorderStyle==BorderStyle.FixedSingle  ) 
				{
					m_TextBox.Height=this.Height-6;
					m_TextBox.Left  = 2;
					m_TextBox.Top = (base.Height  - m_TextBox.Height) / 2;
					m_TextBox.Width  = this.Width -4 ;
				}
				else if(BorderStyle==BorderStyle.None)
				{
					//this.Height =GetSize (); 
					if(!m_TextBox.Multiline)
						m_TextBox.Top = (base.Height  - m_TextBox.Height) / 2;
					else
						m_TextBox.Top = 1;

					m_TextBox.Left  = 1;
					m_TextBox.Height = this.Height-2 ;
					m_TextBox.Width  = this.Width-3  ;
				}
			}
			else
			{
				m_TextBox.Left  = 2;
				m_TextBox.Height = this.Height-4 ;
				m_TextBox.Width  = this.Width -4 ;
	
				if(!m_TextBox.Multiline)
					m_TextBox.Top = (base.Height  - m_TextBox.Height) / 2;
				else
					m_TextBox.Top = 2;
			}
		}

		public void SelectAll()
		{
			this.m_TextBox.SelectAll (); 
		}

		#endregion

		#region Properties

		[Category("Appearance"),DefaultValue(false),RefreshProperties(RefreshProperties.All) ]
		public override bool FixSize
		{
			get{return base.FixSize;}
			set
			{
				base.FixSize = value;
				SetSize ();
				this.Invalidate ();
			}
		}

		[Category("Appearance"),Bindable(true),Browsable(true),DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override string Text
		{
			get {return m_TextBox.Text;}
			set {m_TextBox.Text = value;}
		}

		[DefaultValue(true)]
		public new bool CausesValidation
		{
			get {return m_TextBox.CausesValidation;}
			set{m_TextBox.CausesValidation = value;}
		}

		[Browsable(false),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool IsModified
		{
			get {return m_TextBox.Modified;}
			set {m_TextBox.Modified = value;}
		}

		#endregion

		#region Masked Public Property

		[Category("Masked")]
		[Description("define char to replace input char in value property")] 
		[DefaultValue('\0')]
		public char ReplaceChar
		{
			get{return m_TextBox.ReplaceChar;}
			set{m_TextBox.ReplaceChar=value;}
		}


		[Category("Masked")]
		[Description("Text value without mask char ")] 
		[DefaultValue("")]
		public string Value
		{
			get{return m_TextBox.Value;}		
		}

		[Category("Masked")]
		[Description("Mask format ")] 
		[RefreshProperties(RefreshProperties.All)]
		public string MaskFormat
		{
			get{return m_TextBox.MaskFormat;}
			//set{ mskFormat = value;}
		}


		[Category("Masked")]
		[Description("Sets the Input Char default '_'")] 
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue('_')]
		public char InputChar
		{
			// "_" default
			get{return m_TextBox.InputChar ;}
			set{m_TextBox.InputChar = value;}
		}

	
		[Category("Masked")]
		[Description("Sets the Input Mask "
			 + "(digit 0 = required  9 = optional)"
			 + "(letter A= required a = optional)"
			 + "(letter or digit D = required d = optional)")]
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue("")]
		public string InputMask
		{
			get{return m_TextBox.InputMask;}
			set{m_TextBox.InputMask =value;}
		}

		[Category("Masked")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public new bool IsValid
		{
			get{return m_TextBox.IsValid ;}
		}

		[Category("Masked")]
		[Description("Throw Error On Invalid Text/Value Property")]
		[Browsable(false)]
		[DefaultValue("")]
		public bool ErrorInvalid
		{
			get{return m_TextBox.ErrorInvalid ;}
			set{m_TextBox.ErrorInvalid  = value;}
		}

		#endregion

		#region HideProperty CtlTextBox

		[Browsable(false),DefaultValue(0)]
		public virtual int SelectionLength
		{
			get {return m_TextBox.SelectionLength ;}
			set {m_TextBox.SelectionLength = value;}
		}

		[Browsable(false),DefaultValue(0)]
		public virtual int SelectionStart
		{
			get {return m_TextBox.SelectionStart ;}
			set {m_TextBox.SelectionStart = value;}
		}

		[Browsable(false),DefaultValue("")]
		public virtual string SelectedText
		{
			get {return m_TextBox.SelectedText ;}
			set {m_TextBox.SelectedText = value;}
		}

		#endregion

		#region Behavior Property CtlTextBox

		[Category("Behavior")]
		[DefaultValue(32767)]
		public virtual int MaxLength
		{
			get {return m_TextBox.MaxLength;}
			set 
			{
				if(m_TextBox.MaxLength != value)
					m_TextBox.MaxLength = value;}
		}

		[Category("Behavior")]
		[DefaultValue('\0')]
		public virtual char PasswordChar
		{
			get {return m_TextBox.PasswordChar;}
			set 
			{//if(m_TextBox.PasswordChar != value)
				m_TextBox.PasswordChar = value;}
		}

		[Category("Behavior")]
		[DefaultValue(System.Windows.Forms.CharacterCasing.Normal)]
		public virtual System.Windows.Forms.CharacterCasing CharacterCasing
		{
			get {return m_TextBox.CharacterCasing;}
			set 
			{//if(m_TextBox.CharacterCasing != value)
				m_TextBox.CharacterCasing = value;}
		}

		[Category("Behavior")]
		[DefaultValue(false)]
		public virtual bool AcceptsReturn
		{
			get {return m_TextBox.AcceptsReturn;}
			set 
			{//if(m_TextBox.AcceptsReturn != value)
				m_TextBox.AcceptsReturn = value;}
		}

		[Category("Behavior")]
		[DefaultValue(false)]
		public virtual bool AcceptsTab
		{
			get {return m_TextBox.AcceptsTab;}
			set 
			{//if(m_TextBox.AcceptsTab != value)
				m_TextBox.AcceptsTab = value;}
		}

		[Category("Behavior")]
		[DefaultValue(true)]
		public virtual bool HideSelection
		{
			get {return m_TextBox.HideSelection;}
			set 
			{//if(m_TextBox.HideSelection != value)
				m_TextBox.HideSelection = value;}
		}

		[Category("Behavior")]
		[DefaultValue(true)]
		public virtual bool WordWrap
		{
			get {return m_TextBox.WordWrap;}
			set 
			{//if(m_TextBox.WordWrap != value)
				m_TextBox.WordWrap = value;}
		}

		[Category("Behavior")]
		[DefaultValue(false)]
		public virtual bool Multiline
		{
			get {return m_TextBox.Multiline;}
			set
			{
				if(m_TextBox.Multiline != value)
				{
					m_TextBox.Multiline     = value; 
					m_TextBox.AcceptsReturn = true;
					//if(value && FlatStyle== FlatStyles.FixFlat )
					//{
					//FlatStyle = FlatStyles.Flat;
					this.Invalidate ();
				}
			}
		}
        
		[Category("Behavior")]
		[DefaultValue(System.Windows.Forms.ScrollBars.None)]
		public virtual System.Windows.Forms.ScrollBars ScrollBars
		{
			get {return m_TextBox.ScrollBars;}
			set 
			{//if(m_TextBox.ScrollBars != value)
				m_TextBox.ScrollBars = value;}
		}

		//[DefaultValue(null)]
		//public virtual string[] Lines
		//{
		//	get {return m_TextBox.Lines;}
		//	set {m_TextBox.Lines = value;}
		//}

		#endregion

		#region Appearance Property CtlTextBox

		[Category("Appearance")]
		[DefaultValue(Formats.None)]   
		public Formats FormatType
		{
			get{return m_TextBox.FormatType;}
			set
			{
				m_TextBox.FormatType=value;
				Invalidate ();
			}
		}

		[Category("Appearance")]
		[DefaultValue(System.Windows.Forms.HorizontalAlignment.Left)]
		public virtual HorizontalAlignment TextAlign
		{
			get {return this.m_TextBox.TextAlign;}
			set 
			{//if(m_TextBox.TextAlign != value)
				m_TextBox.TextAlign = value;}
		}

		#endregion

		#region ICtlTextBox Members

		[Category("Appearance")]
		[DefaultValue("")]
		public string Format
		{
			get {return m_TextBox.Format;}
			set {m_TextBox.Format = value;}
		}

		[Category("Appearance")]
		[DefaultValue(2)]   
		public int DecimalPlaces
		{
			get {return m_TextBox.DecimalPlaces ;}
			set 
			{
				m_TextBox.DecimalPlaces =value;
				Invalidate ();  
			}
		}

//		[Category("Behavior")]
//		[DefaultValue(false)]
//		public bool Required
//		{
//			get	{return m_TextBox.Required;}
//			set	{m_TextBox.Required = value;}
//		}

		[Category("Behavior")]
		[DefaultValue(false)]
		public override bool ReadOnly
		{
			get {return base.ReadOnly;}
			set
			{//if(m_TextBox.ReadOnly != value)
				base.ReadOnly =value;
				m_TextBox.ReadOnly = value;}
		}

		public void AppendText(string value)
		{
			m_TextBox.AppendText (value);  
		}

		public override void ResetText()
		{
			base.ResetText ();  
			m_TextBox.ResetText() ;  
		}

		public void AppendFormatText(string value)
		{
			m_TextBox.AppendFormatText(value);  
		}

		[Browsable(false)]
		protected override DataTypes BaseFormat 
		{
			get{return m_TextBox.BaseFormat;}
		}

		#endregion

		#region IControlKeyAction Members

//		protected override void ActionUndo()
//		{
//			m_TextBox.ActionUndo (); 
//		}
//		
//		protected override void ActionDefaultValue()
//		{
//			m_TextBox.ActionDefaultValue (); 
//		}
//
//		protected override  bool ActionValidating()
//		{
//			return m_TextBox.ActionValidating ();
//		}

		#endregion

		#region StyleProperty


		[Category("Style")]
		[Browsable(false)]
		public override Color ForeColor
		{
			get {return m_TextBox.ForeColor;}
			set 
			{
				this.m_TextBox.ForeColor = value;
				this.Invalidate();
			}
		}

		[Category("Style")]
		[Browsable(false)]
		public override Color BackColor
		{
			get {return m_TextBox.BackColor;}
			set 
			{
				this.m_TextBox.BackColor = value;
				this.Invalidate();
			}
		}


		#endregion
		
		#region TextBoxEvent

		private void m_TextBox_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			this.OnMouseDown (e);
		}

		private void m_TextBox_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			this.OnMouseUp (e);
		}

		private void m_TextBox_DoubleClick(object sender, System.EventArgs e)
		{
			this.OnDoubleClick (e);
		}

		private void m_TextBox_Enter(object sender, System.EventArgs e)
		{
			this.OnEnter  (e);
		}

		private void m_TextBox_Leave(object sender, System.EventArgs e)
		{
			this.OnLeave  (e);
		}

		private void m_TextBox_MouseLeave(object sender, System.EventArgs e)
		{
			this.OnMouseLeave  (e);
		}

		private void m_TextBox_Validated(object sender, System.EventArgs e)
		{
			this.OnValidated (e);
		}

		private void m_TextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (!this.CausesValidation)// || (!Required && this.Text ==String.Empty))
				return;
			this.OnValidating (e);
		}

		private void m_TextBox_MouseEnter(object sender, System.EventArgs e)
		{
			this.OnMouseEnter (e);
		}

		private void m_TextBox_MouseHover(object sender, System.EventArgs e)
		{
			this.OnMouseHover (e);
		}

		private void m_TextBox_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			this.OnMouseMove (e);
		}
		#endregion

	}
}