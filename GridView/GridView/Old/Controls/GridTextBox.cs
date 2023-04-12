using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Permissions ; 
using mControl.WinCtl.Controls;
//using mControl.WinCtl.BaseCtl;
using mControl.Util;


namespace mControl.GridStyle.Controls
{
	/// <summary>
	/// Summary description for GridTextBox.
	/// </summary>
	[System.ComponentModel.ToolboxItem(false)]
	public class GridTextBox : mControl.WinCtl.Controls.CtlEditBase,ICtlTextBox
	{
		#region Members

		// Fields
		internal CtlGrid dataGrid;
		private bool isInEditOrNavigateMode;
		private mControl.WinCtl.Controls.CtlTextBoxBase  m_TextBox;

		[Category("Behavior")]
		public new event EventHandler TextChanged = null;

		#endregion

		#region Constructor

		public GridTextBox()
		{
			base.NetReflectedFram("ba7fa38f0b671cbc");
			this.m_TextBox = new mControl.WinCtl.Controls.CtlTextBoxBase();
			this.m_TextBox.NetReflectedFram("ba7fa38f0b671cbc");

			base.SuspendLayout ();
			//this.m_TextBox.Multiline=true;
			this.m_TextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.m_TextBox.Location = new System.Drawing.Point(0, 0);
			this.m_TextBox.Text = "";
			this.m_TextBox.TextChanged+=new EventHandler(m_TextBox_TextChanged);
	
			this.isInEditOrNavigateMode = true;
			base.TabStop = false;
			//base.ShowErrorProvider =false;
			base.BorderStyle   = System.Windows.Forms.BorderStyle.FixedSingle ;
			base.FixSize =false;
			this.Controls.Add(this.m_TextBox);
			base.ResumeLayout (false);
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated (e);
			SetSize ();
			//this.HideSelection=false;
		}

		#endregion

		#region override 

		public override IStyleLayout CtlStyleLayout
		{
			get{return dataGrid.GridLayout as IStyleLayout;} 
		}

		private void m_TextBox_TextChanged(object sender, EventArgs e)
		{
			this.OnTextChanged(e);
			if (this.TextChanged !=null)
				this.TextChanged (this, e);
		}

		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged (e);
			if (!base.ReadOnly && m_TextBox.Modified )
			{
				this.IsInEditOrNavigateMode = false;
				this.dataGrid.ColumnStartedEditing(base.Bounds);
			}
		}

		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			base.OnKeyPress(e);
			if ((((e.KeyChar != ' ') || ((Control.ModifierKeys & Keys.Shift) != Keys.Shift)) && !base.ReadOnly) && (((Control.ModifierKeys & Keys.Control) != Keys.Control) || ((Control.ModifierKeys & Keys.Alt) != Keys.None)))
			{
				this.IsInEditOrNavigateMode = false;
				this.dataGrid.ColumnStartedEditing(base.Bounds);
			}
		}

		protected override bool ProcessDialogKey(Keys keyData)
		{
			if(keyData ==Keys.Tab)
				return true; 
			return base.ProcessDialogKey (keyData);
		}

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			this.dataGrid.ControlOnMouseWheel(e);
		}

		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode=true)]
		protected override void WndProc(ref Message m)
		{
			if (((m.Msg == 770) || (m.Msg == 0x300)) || (m.Msg == 0x303))
			{
				this.IsInEditOrNavigateMode = false;
				this.dataGrid.ColumnStartedEditing(base.Bounds);
			}
			base.WndProc(ref m);
		}

		public new void Focus()
		{
          this.m_TextBox.Focus(); 
		}

		#endregion

		#region ProcessKeyMessage

		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode=true)]
		protected override bool ProcessKeyMessage(ref Message m)
		{
			Keys keys1 = (Keys) ((int) m.WParam);
			Keys keys2 = Control.ModifierKeys;
			if ((((keys1 | keys2) == Keys.Return) || ((keys1 | keys2) == Keys.Escape)) || ((keys1 | keys2) == (Keys.Control | Keys.Return)))
			{
				if (m.Msg == 0x102)
				{
					return true;
				}
				return this.ProcessKeyPreview(ref m);
			}
			if (m.Msg == 0x102)
			{
				if (keys1 == Keys.LineFeed)
				{
					return true;
				}
				return this.ProcessKeyEventArgs(ref m);
			}
			if (m.Msg == 0x101)
			{
				return true;
			}
			Keys keys3 = keys1 & Keys.KeyCode;
			Keys keys4 = keys3;
			if (keys4 <= Keys.A)
			{
				if (keys4 == Keys.Tab)
				{
					if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
					{
						return this.ProcessKeyPreview(ref m);
					}
					return this.ProcessKeyEventArgs(ref m);
				}
				switch (keys4)
				{
					case Keys.Space:
					{
						if (!this.IsInEditOrNavigateMode || ((Control.ModifierKeys & Keys.Shift) != Keys.Shift))
						{
							return this.ProcessKeyEventArgs(ref m);
						}
						if (m.Msg == 0x102)
						{
							return true;
						}
						return this.ProcessKeyPreview(ref m);
					}
					case Keys.Prior:
					case Keys.Next:
					{
						goto Label_0212;
					}
					case Keys.End:
					case Keys.Home:
					{
						if (this.SelectionLength == this.Text.Length)
						{
							return this.ProcessKeyPreview(ref m);
						}
						return this.ProcessKeyEventArgs(ref m);
					}
					case Keys.Left:
					{
						if (((this.SelectionStart + this.SelectionLength) != 0) && (!this.IsInEditOrNavigateMode || (this.SelectionLength != this.Text.Length)))
						{
							return this.ProcessKeyEventArgs(ref m);
						}
						return this.ProcessKeyPreview(ref m);
					}
					case Keys.Up:
					{
						if ((this.Text.IndexOf("\r\n") >= 0) && ((this.SelectionStart + this.SelectionLength) >= this.Text.IndexOf("\r\n")))
						{
							return this.ProcessKeyEventArgs(ref m);
						}
						return this.ProcessKeyPreview(ref m);
					}
					case Keys.Right:
					{
						if ((this.SelectionStart + this.SelectionLength) == this.Text.Length)
						{
							return this.ProcessKeyPreview(ref m);
						}
						return this.ProcessKeyEventArgs(ref m);
					}
					case Keys.Down:
					{
						int num1 = this.SelectionStart + this.SelectionLength;
						if (this.Text.IndexOf("\r\n", num1) == -1)
						{
							return this.ProcessKeyPreview(ref m);
						}
						return this.ProcessKeyEventArgs(ref m);
					}
					case Keys.Select:
					case Keys.Print:
					case Keys.Execute:
					case Keys.Snapshot:
					case Keys.Insert:
					{
						goto Label_0313;
					}
					case Keys.Delete:
					{
						if (this.IsInEditOrNavigateMode)
						{
							if (this.ProcessKeyPreview(ref m))
							{
								return true;
							}
							this.IsInEditOrNavigateMode = false;
							this.dataGrid.ColumnStartedEditing(base.Bounds);
						}
						return this.ProcessKeyEventArgs(ref m);
					}
					case Keys.A:
					{
						if (!this.IsInEditOrNavigateMode || ((Control.ModifierKeys & Keys.Control) != Keys.Control))
						{
							return this.ProcessKeyEventArgs(ref m);
						}
						if (m.Msg == 0x102)
						{
							return true;
						}
						return this.ProcessKeyPreview(ref m);
					}
				}
				goto Label_0313;
			}
			switch (keys4)
			{
				case Keys.Add:
				case Keys.Subtract:
				case Keys.Oemplus:
				case Keys.OemMinus:
				{
					goto Label_0212;
				}
				case Keys.Separator:
				case Keys.Oemcomma:
				{
					goto Label_0313;
				}
				case Keys.F2:
				{
					this.IsInEditOrNavigateMode = false;
					this.SelectionStart = this.Text.Length;
					return true;
				}
				default:
				{
					goto Label_0313;
				}
			}
			Label_0212:
				if (this.IsInEditOrNavigateMode)
				{
					return this.ProcessKeyPreview(ref m);
				}
			return this.ProcessKeyEventArgs(ref m);
			Label_0313:
				return this.ProcessKeyEventArgs(ref m);
		}

		#endregion

		#region internal methods

		public void SetDataGrid(DataGrid parentGrid)
		{
			this.dataGrid = (CtlGrid)parentGrid;
		}
 
		public bool IsInEditOrNavigateMode
		{
			get
			{
				return this.isInEditOrNavigateMode;
			}
			set
			{
				this.isInEditOrNavigateMode = value;
				if (value&& !base.IsDisposed)
				{
					this.SelectAll();
				}
			}
		}
 
		internal void SelectInternal(int start, int length)
		{
			//base.SelectAll ();
			this.SelectionStart = start;
			this.SelectionLength = length;

//			base.selectionSet = true;
//			int num1 = start + length;
//			if (base.IsHandleCreated)
//			{
//				if (Marshal.SystemDefaultCharSize == 1)
//				{
//					TextBoxBase.ToDbcsOffsets(this.Text, ref start, ref num1);
//					base.SendMessage(0xb1, start, num1);
//				}
//				else
//				{
//					base.SendMessage(0xb1, start, num1);
//				}
//			}
//			else
//			{
//				base.SelectionStart = start;
//				base.SelectionLength = length;
//			}
		}
		#endregion

		#region TextBox Overrides Methods

		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged(e);
			this.m_TextBox.Enabled =this.Enabled;  
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged (e);
			SetSize();
		}

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
					m_TextBox.Top = (base.Height - m_TextBox.Height) / 2;
				else
					m_TextBox.Top = 2;
			}
		}

		#endregion

		#region Properties

		[Category("Appearance"),DefaultValue(false),RefreshProperties(RefreshProperties.All)  ]
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
			get{return m_TextBox.Text;}
			set
			{
                		
				if(value.Length ==0) 
				{
					m_TextBox.Text =m_DefaultValue;
					//if(!Required)
					return;
				}
				AppendFormatText(value);
			}
		}

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
				this.m_TextBox.BackColor = value ;
				this.Invalidate();
			}
		}

		#endregion

		#region SelectionProperty CtlTextBox

		public void SelectAll()
		{
			this.m_TextBox.SelectAll (); 
		}

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
			{
				m_TextBox.PasswordChar = value;
			}
		}

		[Category("Behavior")]
		[DefaultValue(System.Windows.Forms.CharacterCasing.Normal)]
		public virtual System.Windows.Forms.CharacterCasing CharacterCasing
		{
			get {return m_TextBox.CharacterCasing;}
			set 
			{
				m_TextBox.CharacterCasing = value;
			}
		}

		[Category("Behavior")]
		[DefaultValue(false)]
		public virtual bool AcceptsReturn
		{
			get {return m_TextBox.AcceptsReturn;}
			set 
			{
				m_TextBox.AcceptsReturn = value;
			}
		}

		[Category("Behavior")]
		[DefaultValue(false)]
		public virtual bool AcceptsTab
		{
			get {return m_TextBox.AcceptsTab;}
			set 
			{
				m_TextBox.AcceptsTab = value;
			}
		}

		[Category("Behavior")]
		[DefaultValue(true)]
		public virtual bool HideSelection
		{
			get {return m_TextBox.HideSelection;}
			set 
			{
				m_TextBox.HideSelection = value;
			}
		}

		[Category("Behavior")]
		[DefaultValue(true)]
		public virtual bool WordWrap
		{
			get {return m_TextBox.WordWrap;}
			set 
			{
				m_TextBox.WordWrap = value;
			}
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
			{
				m_TextBox.ScrollBars = value;
			}
		}

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
			{
				m_TextBox.TextAlign = value;
			}
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
			//m_TextBox.AppendFormatText(value);  
			m_TextBox.Text=value;  
		}

		[Browsable(false)]
		protected override DataTypes BaseFormat 
		{
			get{return m_TextBox.BaseFormat;}
		}

		#endregion

	}
}
