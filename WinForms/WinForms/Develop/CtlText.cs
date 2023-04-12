using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using System.Text;
using mControl.Util;
using mControl.Data;
using mControl.WinCtl.Controls;
using mControl.WinCtl.Components;
using mControl.Win32;


namespace mControl.WinCtl.Controls
{
	[DefaultEvent("TextChanged"), Designer(mControl.WinCtl.Controls.Design.CtlTextBaseDesigner)]
	public abstract class CtlTextBase : Control
	{

 
		// Events
		[Description("RadioButtonOnTextAlignChanged"), Category("PropertyChanged")]
		public event EventHandler TextAlignChanged;


		// Fields
		private bool acceptsReturn;
		private CharacterCasing characterCasing;
		private bool doubleClickFired;
		private static readonly object EVENT_TEXTALIGNCHANGED;
		private char passwordChar;
		private ScrollBars scrollBars;
		private bool selectionSet;
		private HorizontalAlignment textAlign;

	    private bool validationCancelled;

		public CtlTextBase()
		{

			this.prefHeightCache = -1;
			this.borderStyle = BorderStyle.Fixed3D;
			this.maxLength = 0x7fff;
			this.integralHeightAdjust = false;
			this.selectionStart = -1;
			this.selectionLength = -1;
			this.textBoxFlags = new BitVector32();
			this.textBoxFlags[(CtlTextBase.autoSize | CtlTextBase.hideSelection) | CtlTextBase.wordWrap] = true;
			base.SetStyle(ControlStyles.UserPaint, false);
			base.SetStyle(ControlStyles.FixedHeight, this.textBoxFlags[CtlTextBase.autoSize]);
			this.requestedHeight = this.DefaultSize.Height;
			this.requestedHeight = base.Height;

			this.acceptsReturn = false;
			this.characterCasing = CharacterCasing.Normal;
			this.scrollBars = ScrollBars.None;
			this.textAlign = HorizontalAlignment.Left;
			this.doubleClickFired = false;
			this.selectionSet = false;
			base.SetStyle(ControlStyles.StandardDoubleClick | ControlStyles.StandardClick, false);
		}


		static CtlTextBase()
		{

				CtlTextBase.autoSize = BitVector32.CreateMask();
				CtlTextBase.hideSelection = BitVector32.CreateMask(CtlTextBase.autoSize);
				CtlTextBase.multiline = BitVector32.CreateMask(CtlTextBase.hideSelection);
				CtlTextBase.modified = BitVector32.CreateMask(CtlTextBase.multiline);
				CtlTextBase.readOnly = BitVector32.CreateMask(CtlTextBase.modified);
				CtlTextBase.acceptsTab = BitVector32.CreateMask(CtlTextBase.readOnly);
				CtlTextBase.wordWrap = BitVector32.CreateMask(CtlTextBase.acceptsTab);
				CtlTextBase.creatingHandle = BitVector32.CreateMask(CtlTextBase.wordWrap);
				CtlTextBase.codeUpdateText = BitVector32.CreateMask(CtlTextBase.creatingHandle);
				CtlTextBase.EVENT_ACCEPTSTABCHANGED = new object();
				CtlTextBase.EVENT_AUTOSIZECHANGED = new object();
				CtlTextBase.EVENT_BORDERSTYLECHANGED = new object();
				CtlTextBase.EVENT_HIDESELECTIONCHANGED = new object();
				CtlTextBase.EVENT_MODIFIEDCHANGED = new object();
				CtlTextBase.EVENT_MULTILINECHANGED = new object();
				CtlTextBase.EVENT_READONLYCHANGED = new object();


			CtlTextBase.EVENT_TEXTALIGNCHANGED = new object();
		}


		#region Base

		// Events
		[Category("PropertyChanged"), Description("CtlTextBaseOnAcceptsTabChanged")]
		public event EventHandler AcceptsTabChanged;
		[Description("CtlTextBaseOnAutoSizeChanged"), Category("PropertyChanged")]
		public event EventHandler AutoSizeChanged;
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public event EventHandler BackgroundImageChanged;
		[Description("CtlTextBaseOnBorderStyleChanged"), Category("PropertyChanged")]
		public event EventHandler BorderStyleChanged;
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
		public event EventHandler Click;
		[Description("CtlTextBaseOnHideSelectionChanged"), Category("PropertyChanged")]
		public event EventHandler HideSelectionChanged;
		[Category("PropertyChanged"), Description("CtlTextBaseOnModifiedChanged")]
		public event EventHandler ModifiedChanged;
		[Description("CtlTextBaseOnMultilineChanged"), Category("PropertyChanged")]
		public event EventHandler MultilineChanged;
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public event PaintEventHandler Paint;
		[Description("CtlTextBaseOnReadOnlyChanged"), Category("PropertyChanged")]
		public event EventHandler ReadOnlyChanged;


		// Fields
		private static readonly int acceptsTab;
		private static readonly int autoSize;
		private BorderStyle borderStyle;
		private static readonly int codeUpdateText;
		private static readonly int creatingHandle;
		private static readonly object EVENT_ACCEPTSTABCHANGED;
		private static readonly object EVENT_AUTOSIZECHANGED;
		private static readonly object EVENT_BORDERSTYLECHANGED;
		private static readonly object EVENT_HIDESELECTIONCHANGED;
		private static readonly object EVENT_MODIFIEDCHANGED;
		private static readonly object EVENT_MULTILINECHANGED;
		private static readonly object EVENT_READONLYCHANGED;
		private static readonly int hideSelection;
		private bool integralHeightAdjust;
		private int maxLength;
		private static readonly int modified;
		private static readonly int multiline;
		private short prefHeightCache;
		private static readonly int readOnly;
		private int requestedHeight;
		private int selectionLength;
		private int selectionStart;
		private BitVector32 textBoxFlags;
		private static readonly int wordWrap;


		private void AdjustHeight()
		{
			if ((this.Anchor & (AnchorStyles.Bottom | AnchorStyles.Top)) != (AnchorStyles.Bottom | AnchorStyles.Top))
			{
				this.prefHeightCache = -1;
				base.FontHeight = -1;
				int num1 = this.requestedHeight;
				try
				{
					if (this.textBoxFlags[CtlTextBase.autoSize] && !this.textBoxFlags[CtlTextBase.multiline])
					{
						base.Height = this.PreferredHeight;
					}
					else
					{
						int num2 = base.Height;
						if (this.textBoxFlags[CtlTextBase.multiline])
						{
							base.Height = Math.Max(num1, this.PreferredHeight + 2);
						}
						this.integralHeightAdjust = true;
						try
						{
							base.Height = num1;
						}
						finally
						{
							this.integralHeightAdjust = false;
						}
					}
				}
				finally
				{
					this.requestedHeight = num1;
				}
			}
		}

		public void AppendText(string text)
		{
			int num1;
			if (base.IsHandleCreated)
			{
				num1 = SafeNativeMethods.GetWindowTextLength(new HandleRef(this, base.Handle)) + 1;
			}
			else
			{
				num1 = this.TextLength;
			}
			this.SelectInternal(num1, num1);
			this.SelectedText = text;
		}

		public void Clear()
		{
			this.Text = null;
		}

 
		public void ClearUndo()
		{
			if (base.IsHandleCreated)
			{
				base.SendMessage(0xcd, 0, 0);
			}
		}

		public void Copy()
		{
			base.SendMessage(0x301, 0, 0);
		}

		protected override void CreateHandle()
		{
			this.textBoxFlags[CtlTextBase.creatingHandle] = true;
			try
			{
				base.CreateHandle();
				if (this.SetSelectionInCreateHandle)
				{
					this.SetSelectionOnHandle();
				}
			}
			finally
			{
				this.textBoxFlags[CtlTextBase.creatingHandle] = false;
			}
		}

		public void Cut()
		{
			base.SendMessage(0x300, 0, 0);
		}

		[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted=true), PermissionSet(SecurityAction.LinkDemand, Unrestricted=true)]
		internal virtual int GetLength()
		{
			int num1 = 0;
			int num2 = 0;
			UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 0xb0, ref num1, ref num2);
			return (num2 - num1);
		}

 
		[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted=true), PermissionSet(SecurityAction.LinkDemand, Unrestricted=true)]
		internal virtual int GetSelectionStart()
		{
			int num1 = 0;
			int num2 = 0;
			UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 0xb0, ref num1, ref num2);
			return Math.Max(0, num1);
		}

		internal override IntPtr InitializeDCForWmCtlColor(IntPtr dc, int msg)
		{
			if ((msg == 0x138) && !this.ShouldSerializeBackColor())
			{
				return IntPtr.Zero;
			}
			return base.InitializeDCForWmCtlColor(dc, msg);
		}

 
		protected override bool IsInputKey(Keys keyData)
		{
			if ((keyData & Keys.Alt) != Keys.Alt)
			{
				Keys keys1 = keyData & Keys.KeyCode;
				if (keys1 != Keys.Tab)
				{
					switch (keys1)
					{
						case Keys.Prior:
						case Keys.Next:
						case Keys.End:
						case Keys.Home:
							return true;

						case Keys.Escape:
							if (this.Multiline)
							{
								return false;
							}
							goto Label_006E;
					}
				}
				else
				{
					if (this.Multiline && this.textBoxFlags[CtlTextBase.acceptsTab])
					{
						return ((keyData & Keys.Control) == Keys.None);
					}
					return false;
				}
			}
			Label_006E:
				return base.IsInputKey(keyData);
		}

 
		protected virtual void OnAcceptsTabChanged(EventArgs e)
		{
			EventHandler handler1 = base.Events[CtlTextBase.EVENT_ACCEPTSTABCHANGED] as EventHandler;
			if (handler1 != null)
			{
				handler1(this, e);
			}
		}

		protected virtual void OnAutoSizeChanged(EventArgs e)
		{
			EventHandler handler1 = base.Events[CtlTextBase.EVENT_AUTOSIZECHANGED] as EventHandler;
			if (handler1 != null)
			{
				handler1(this, e);
			}
		}

		protected virtual void OnBorderStyleChanged(EventArgs e)
		{
			EventHandler handler1 = base.Events[CtlTextBase.EVENT_BORDERSTYLECHANGED] as EventHandler;
			if (handler1 != null)
			{
				handler1(this, e);
			}
		}

		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.AdjustHeight();
		}

 
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			this.AdjustHeight();
			this.UpdateMaxLength();
			if (this.textBoxFlags[CtlTextBase.modified])
			{
				base.SendMessage(0xb9, 1, 0);
			}
		}

		protected override void OnHandleDestroyed(EventArgs e)
		{
			this.textBoxFlags[CtlTextBase.modified] = this.Modified;
			this.selectionStart = this.SelectionStart;
			this.selectionLength = this.SelectionLength;
			base.OnHandleDestroyed(e);
		}

		protected virtual void OnHideSelectionChanged(EventArgs e)
		{
			EventHandler handler1 = base.Events[CtlTextBase.EVENT_HIDESELECTIONCHANGED] as EventHandler;
			if (handler1 != null)
			{
				handler1(this, e);
			}
		}

		protected virtual void OnModifiedChanged(EventArgs e)
		{
			EventHandler handler1 = base.Events[CtlTextBase.EVENT_MODIFIEDCHANGED] as EventHandler;
			if (handler1 != null)
			{
				handler1(this, e);
			}
		}

 
		protected virtual void OnMultilineChanged(EventArgs e)
		{
			EventHandler handler1 = base.Events[CtlTextBase.EVENT_MULTILINECHANGED] as EventHandler;
			if (handler1 != null)
			{
				handler1(this, e);
			}
		}

		protected virtual void OnReadOnlyChanged(EventArgs e)
		{
			EventHandler handler1 = base.Events[CtlTextBase.EVENT_READONLYCHANGED] as EventHandler;
			if (handler1 != null)
			{
				handler1(this, e);
			}
		}

 
		public void Paste()
		{
			IntSecurity.ClipboardRead.Demand();
			base.SendMessage(770, 0, 0);
		}

		protected override bool ProcessDialogKey(Keys keyData)
		{
			if (((keyData & Keys.Alt) == Keys.None) && ((keyData & Keys.Control) != Keys.None))
			{
				Keys keys1 = keyData & Keys.KeyCode;
				Keys keys2 = keys1;
				if ((keys2 == Keys.Tab) && this.ProcessTabKey((keyData & Keys.Shift) == Keys.None))
				{
					return true;
				}
			}
			return base.ProcessDialogKey(keyData);
		}

 
		private bool ProcessTabKey(bool forward)
		{
			if (base.Parent.SelectNextControl(this, forward, true, true, true))
			{
				return true;
			}
			return false;
		}

		public void ScrollToCaret()
		{
			base.SendMessage(0xb7, 0, 0);
		}

		public void Select(int start, int length)
		{
			if (start < 0)
			{
				throw new ArgumentException(SR.GetString("InvalidArgument", new object[] { "start", start.ToString() }));
			}
			if (length < 0)
			{
				throw new ArgumentException(SR.GetString("InvalidArgument", new object[] { "length", length.ToString() }));
			}
			int num1 = this.TextLength;
			if (start > num1)
			{
				start = num1;
				length = 0;
			}
			this.SelectInternal(start, length);
		}

 
		public void SelectAll()
		{
			this.SelectInternal(0, this.TextLength);
		}

		internal virtual void SelectInternal(int start, int length)
		{
			int num1 = start + length;
			if (base.IsHandleCreated)
			{
				base.SendMessage(0xb1, start, num1);
			}
			else
			{
				this.selectionStart = start;
				this.selectionLength = length;
			}
		}

 
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			if (!this.integralHeightAdjust && (height != base.Height))
			{
				this.requestedHeight = height;
			}
			if (this.textBoxFlags[CtlTextBase.autoSize] && !this.textBoxFlags[CtlTextBase.multiline])
			{
				height = this.PreferredHeight;
			}
			base.SetBoundsCore(x, y, width, height, specified);
		}

		internal void SetSelectionOnHandle()
		{
			if (this.selectionStart > -1)
			{
				if (Marshal.SystemDefaultCharSize == 1)
				{
					int num1 = this.selectionStart;
					int num2 = this.selectionStart + this.selectionLength;
					CtlTextBase.ToDbcsOffsets(this.Text, ref num1, ref num2);
					base.SendMessage(0xb1, num1, num2);
				}
				else
				{
					base.SendMessage(0xb1, this.selectionStart, this.selectionStart + this.selectionLength);
				}
				this.selectionStart = -1;
				this.selectionLength = -1;
			}
		}

 
		internal virtual void SetWordWrapInternal(bool value)
		{
			if (this.textBoxFlags[CtlTextBase.wordWrap] != value)
			{
				this.textBoxFlags[CtlTextBase.wordWrap] = value;
				base.RecreateHandle();
			}
		}

		internal static void ToDbcsOffsets(string str, ref int start, ref int end)
		{
			Encoding encoding1 = Encoding.Default;
			int num1 = start;
			if (start < 0)
			{
				start = 0;
			}
			if (start > str.Length)
			{
				start = str.Length;
			}
			if (end < start)
			{
				end = start;
			}
			if (end > str.Length)
			{
				end = str.Length;
			}
			if (start != 0)
			{
				num1 = encoding1.GetByteCount(str.Substring(0, start));
			}
			end = num1 + encoding1.GetByteCount(str.Substring(start, end - start));
			start = num1;
		}

		public override string ToString()
		{
			string text1 = base.ToString();
			string text2 = this.Text;
			if (text2.Length > 40)
			{
				text2 = text2.Substring(0, 40) + "...";
			}
			return (text1 + ", Text: " + text2.ToString());
		}

		public void Undo()
		{
			base.SendMessage(0xc7, 0, 0);
		}

		internal virtual void UpdateMaxLength()
		{
			if (base.IsHandleCreated)
			{
				base.SendMessage(0xc5, this.maxLength, 0);
			}
		}

 
		private void WmGetDlgCode(ref Message m)
		{
			base.WndProc(ref m);
			if (this.AcceptsTab)
			{
				m.Result = (IntPtr) (((int) m.Result) | 2);
			}
			else
			{
				m.Result = (IntPtr) (((int) m.Result) & -7);
			}
		}

		private void WmReflectCommand(ref Message m)
		{
			if ((!this.textBoxFlags[CtlTextBase.codeUpdateText] && ((((int) m.WParam) >> 0x10) == 0x300)) && (!this.textBoxFlags[CtlTextBase.creatingHandle] && this.CanRaiseTextChangedEvent))
			{
				this.OnTextChanged(EventArgs.Empty);
			}
		}

 
		private void WmSetFont(ref Message m)
		{
			base.WndProc(ref m);
			if (!this.textBoxFlags[CtlTextBase.multiline])
			{
				base.SendMessage(0xd3, 3, 0);
			}
		}

		[SecurityPermission(SecurityAction.LinkDemand)]
		protected override void WndProc(ref Message m)
		{
			switch (m.Msg)
			{
				case 0x30:
					this.WmSetFont(ref m);
					return;

				case 0x87:
					this.WmGetDlgCode(ref m);
					return;

				case 0x2111:
					this.WmReflectCommand(ref m);
					return;
			}
			base.WndProc(ref m);
		}

 
		[Description("TextBoxAcceptsTab"), Category("Behavior"), DefaultValue(false)]
		public bool AcceptsTab
		{
			get
			{
				return this.textBoxFlags[CtlTextBase.acceptsTab];
			}
			set
			{
				if (this.textBoxFlags[CtlTextBase.acceptsTab] != value)
				{
					this.textBoxFlags[CtlTextBase.acceptsTab] = value;
					this.OnAcceptsTabChanged(EventArgs.Empty);
				}
			}
		}
		[Localizable(true), Category("Behavior"), Description("TextBoxAutoSize"), DefaultValue(true), RefreshProperties(RefreshProperties.Repaint)]
		public virtual bool AutoSize
		{
			get
			{
				return this.textBoxFlags[CtlTextBase.autoSize];
			}
			set
			{
				if (this.textBoxFlags[CtlTextBase.autoSize] != value)
				{
					this.textBoxFlags[CtlTextBase.autoSize] = value;
					if (!this.Multiline)
					{
						base.SetStyle(ControlStyles.FixedHeight, value);
						this.AdjustHeight();
					}
					this.OnAutoSizeChanged(EventArgs.Empty);
				}
			}
		}
		[Category("Appearance"), Description("ControlBackColor"), DispId(-501)]
		public override Color BackColor
		{
			get
			{
				if (this.ShouldSerializeBackColor())
				{
					return base.BackColor;
				}
				return SystemColors.Window;
			}
			set
			{
				base.BackColor = value;
			}
		}
 
		[EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
		public override Image BackgroundImage
		{
			get
			{
				return base.BackgroundImage;
			}
			set
			{
				base.BackgroundImage = value;
			}
		}
		[Category("Appearance"), Description("TextBoxBorder"), DefaultValue(2), DispId(-504)]
		public BorderStyle BorderStyle
		{
			get
			{
				return this.borderStyle;
			}
			set
			{
				if (this.borderStyle != value)
				{
					if (!Enum.IsDefined(typeof(BorderStyle), value))
					{
						throw new InvalidEnumArgumentException("value", (int) value, typeof(BorderStyle));
					}
					this.borderStyle = value;
					this.prefHeightCache = -1;
					base.UpdateStyles();
					base.RecreateHandle();
					this.OnBorderStyleChanged(EventArgs.Empty);
				}
			}
		}
 
		internal virtual bool CanRaiseTextChangedEvent
		{
			get
			{
				return true;
			}
		}
 
		[Description("TextBoxCanUndo"), Category("Behavior"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanUndo
		{
			get
			{
				if (base.IsHandleCreated)
				{
					return (((int) base.SendMessage(0xc6, 0, 0)) != 0);
				}
				return false;
			}
		}
 
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand)]
			get
			{
				CreateParams params1 = base.CreateParams;
				params1.ClassName = "EDIT";
				params1.Style |= 0xc0;
				if (!this.textBoxFlags[CtlTextBase.hideSelection])
				{
					params1.Style |= 0x100;
				}
				if (this.textBoxFlags[CtlTextBase.readOnly])
				{
					params1.Style |= 0x800;
				}
				params1.ExStyle &= -513;
				params1.Style &= -8388609;
				switch (this.borderStyle)
				{
					case BorderStyle.FixedSingle:
						params1.Style |= 0x800000;
						break;

					case BorderStyle.Fixed3D:
						params1.ExStyle |= 0x200;
						break;
				}
				if (this.textBoxFlags[CtlTextBase.multiline])
				{
					params1.Style |= 4;
					if (this.textBoxFlags[CtlTextBase.wordWrap])
					{
						params1.Style &= -129;
					}
				}
				return params1;
			}
		}
		internal override Cursor DefaultCursor
		{
			get
			{
				return Cursors.IBeam;
			}
		}
 
		protected override Size DefaultSize
		{
			get
			{
				return new Size(100, this.PreferredHeight);
			}
		}
 
		[Category("Appearance"), Description("ControlForeColor"), DispId(-513)]
		public override Color ForeColor
		{
			get
			{
				if (this.ShouldSerializeForeColor())
				{
					return base.ForeColor;
				}
				return SystemColors.WindowText;
			}
			set
			{
				base.ForeColor = value;
			}
		}
 
		[Category("Behavior"), DefaultValue(true), Description("TextBoxHideSelection")]
		public bool HideSelection
		{
			get
			{
				return this.textBoxFlags[CtlTextBase.hideSelection];
			}
			set
			{
				if (this.textBoxFlags[CtlTextBase.hideSelection] != value)
				{
					this.textBoxFlags[CtlTextBase.hideSelection] = value;
					base.RecreateHandle();
					this.OnHideSelectionChanged(EventArgs.Empty);
				}
			}
		}
 
		[Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), Description("TextBoxLines"), Category("Appearance"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Localizable(true)]
		public string[] Lines
		{
			get
			{
				int num2;
				string text1 = this.Text;
				ArrayList list1 = new ArrayList();
				for (int num1 = 0; num1 < text1.Length; num1 = num2)
				{
					num2 = num1;
					while (num2 < text1.Length)
					{
						//switch (text1[num2])
						//{
						//}
						num2++;
					}
					string text2 = text1.Substring(num1, num2 - num1);
					list1.Add(text2);
					if ((num2 < text1.Length) && (text1[num2] == '\r'))
					{
						num2++;
					}
					if ((num2 < text1.Length) && (text1[num2] == '\n'))
					{
						num2++;
					}
				}
				if ((text1.Length > 0) && ((text1[text1.Length - 1] == '\r') || (text1[text1.Length - 1] == '\n')))
				{
					list1.Add("");
				}
				return (string[]) list1.ToArray(typeof(string));
			}
			set
			{
				if ((value != null) && (value.Length > 0))
				{
					StringBuilder builder1 = new StringBuilder(value[0]);
					for (int num1 = 1; num1 < value.Length; num1++)
					{
						builder1.Append("\r\n");
						builder1.Append(value[num1]);
					}
					this.Text = builder1.ToString();
				}
				else
				{
					this.Text = "";
				}
			}
		}
 
		[Localizable(true), DefaultValue(0x7fff), Category("Behavior"), Description("TextBoxMaxLength")]
		public virtual int MaxLength
		{
			get
			{
				return this.maxLength;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException(SR.GetString("InvalidLowBoundArgumentEx", new object[] { "MaxLength", value.ToString(), "0" }));
				}
				if (this.maxLength != value)
				{
					this.maxLength = value;
					this.UpdateMaxLength();
				}
			}
		}
 
		[Category("Behavior"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("TextBoxModified")]
		public bool Modified
		{
			get
			{
				if (base.IsHandleCreated)
				{
					return (0 != ((int) base.SendMessage(0xb8, 0, 0)));
				}
				return this.textBoxFlags[CtlTextBase.modified];
			}
			set
			{
				if (this.Modified != value)
				{
					if (base.IsHandleCreated)
					{
						base.SendMessage(0xb9, value ? 1 : 0, 0);
					}
					else
					{
						this.textBoxFlags[CtlTextBase.modified] = value;
					}
					this.OnModifiedChanged(EventArgs.Empty);
				}
			}
		}
 
		[Localizable(true), Description("TextBoxMultiline"), RefreshProperties(RefreshProperties.All), Category("Behavior"), DefaultValue(false)]
		public virtual bool Multiline
		{
			get
			{
				return this.textBoxFlags[CtlTextBase.multiline];
			}
			set
			{
				if (this.textBoxFlags[CtlTextBase.multiline] != value)
				{
					this.textBoxFlags[CtlTextBase.multiline] = value;
					if (value)
					{
						base.SetStyle(ControlStyles.FixedHeight, false);
					}
					else
					{
						base.SetStyle(ControlStyles.FixedHeight, this.AutoSize);
					}
					base.RecreateHandle();
					this.AdjustHeight();
					this.OnMultilineChanged(EventArgs.Empty);
				}
			}
		}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("TextBoxPreferredHeight"), Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), Category("Layout")]
		public int PreferredHeight
		{
			get
			{
				if (this.prefHeightCache > -1)
				{
					return this.prefHeightCache;
				}
				int num1 = base.FontHeight;
				if (this.borderStyle != BorderStyle.None)
				{
					num1 += (SystemInformation.BorderSize.Height * 4) + 3;
				}
				this.prefHeightCache = (short) num1;
				return num1;
			}
		}
		[Category("Behavior"), Description("TextBoxReadOnly"), DefaultValue(false)]
		public bool ReadOnly
		{
			get
			{
				return this.textBoxFlags[CtlTextBase.readOnly];
			}
			set
			{
				if (this.textBoxFlags[CtlTextBase.readOnly] != value)
				{
					this.textBoxFlags[CtlTextBase.readOnly] = value;
					if (base.IsHandleCreated)
					{
						base.SendMessage(0xcf, value ? -1 : 0, 0);
					}
					this.OnReadOnlyChanged(EventArgs.Empty);
				}
			}
		}
 
		[Browsable(false), Description("TextBoxSelectedText"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Category("Appearance")]
		public virtual string SelectedText
		{
			get
			{
				if (this.SelectionStart != -1)
				{
					return this.Text.Substring(this.SelectionStart, this.SelectionLength);
				}
				return null;
			}
			set
			{
				if (base.IsHandleCreated)
				{
					base.SendMessage(0xc5, 0x7fff, 0);
					base.SendMessage(0xc2, -1, (value == null) ? "" : value);
					base.SendMessage(0xb9, 0, 0);
					base.SendMessage(0xc5, this.maxLength, 0);
				}
				else
				{
					string text1 = this.Text.Substring(0, this.SelectionStart);
					string text2 = "";
					if ((this.SelectionStart + this.SelectionLength) < this.Text.Length)
					{
						this.Text.Substring(this.SelectionStart + this.SelectionLength);
					}
					base.Text = text1 + value + text2;
				}
				this.ClearUndo();
			}
		}
 
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("TextBoxSelectionLength"), Browsable(false), Category("Appearance")]
		public virtual int SelectionLength
		{
			get
			{
				if (!base.IsHandleCreated)
				{
					return this.selectionLength;
				}
				int num1 = 0;
				int num2 = 0;
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 0xb0, ref num1, ref num2);
				num1 = Math.Max(0, num1);
				num2 = Math.Max(0, num2);
				return (num2 - num1);
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException(SR.GetString("InvalidArgument", new object[] { "value", value.ToString() }));
				}
				if (value != this.selectionLength)
				{
					if (this.SelectionStart != -1)
					{
						this.Select(this.SelectionStart, value);
					}
					else
					{
						this.Select(0, value);
					}
				}
			}
		}
		[Category("Appearance"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("TextBoxSelectionStart")]
		public int SelectionStart
		{
			get
			{
				if (!base.IsHandleCreated)
				{
					return this.selectionStart;
				}
				return this.GetSelectionStart();
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException(SR.GetString("InvalidArgument", new object[] { "value", "" + value.ToString() }));
				}
				int num1 = this.GetLength();
				this.Select(value, Math.Max(num1, 0));
			}
		}
		internal virtual bool SetSelectionInCreateHandle
		{
			get
			{
				return true;
			}
		}
 
		[Localizable(true)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				if (value != base.Text)
				{
					base.Text = value;
					if (base.IsHandleCreated)
					{
						base.SendMessage(0xb9, 0, 0);
					}
				}
			}
		}
		[Browsable(false)]
		public virtual int TextLength
		{
			get
			{
				if (base.IsHandleCreated && (Marshal.SystemDefaultCharSize == 2))
				{
					return SafeNativeMethods.GetWindowTextLength(new HandleRef(this, base.Handle));
				}
				return this.Text.Length;
			}
		}
 
		internal override string WindowText
		{
			[PermissionSet(SecurityAction.LinkDemand, Unrestricted=true)]
			get
			{
				return base.WindowText;
			}
			[PermissionSet(SecurityAction.LinkDemand, Unrestricted=true)]
			set
			{
				if (value == null)
				{
					value = "";
				}
				if (!this.WindowText.Equals(value))
				{
					this.textBoxFlags[CtlTextBase.codeUpdateText] = true;
					try
					{
						base.WindowText = value;
					}
					finally
					{
						this.textBoxFlags[CtlTextBase.codeUpdateText] = false;
					}
				}
			}
		}
		[Description("TextBoxWordWrap"), DefaultValue(true), Localizable(true), Category("Behavior")]
		public bool WordWrap
		{
			get
			{
				return this.textBoxFlags[CtlTextBase.wordWrap];
			}
			set
			{
				this.SetWordWrapInternal(value);
			}
		}
 

		#endregion


		internal static void ToDbcsOffsets(string str, ref int start, ref int end)
		{
			Encoding encoding1 = Encoding.Default;
			int num1 = start;
			if (start < 0)
			{
				start = 0;
			}
			if (start > str.Length)
			{
				start = str.Length;
			}
			if (end < start)
			{
				end = start;
			}
			if (end > str.Length)
			{
				end = str.Length;
			}
			if (start != 0)
			{
				num1 = encoding1.GetByteCount(str.Substring(0, start));
			}
			end = num1 + encoding1.GetByteCount(str.Substring(start, end - start));
			start = num1;
		}


		internal IntPtr SendMessage(int msg, int wparam, int lparam)
		{
			return WinMethods.SendMessage(new HandleRef(this, this.Handle), msg, wparam, lparam);
		}

 

		internal  int GetLength()
		{
			int num1 = 0;
			int num2 = 0;
			WinMethods.SendMessage(new HandleRef(this, base.Handle), 0xb0, ref num1, ref num2);
			num1 = Math.Max(0, num1);
			num2 = Math.Max(0, num2);
			if (Marshal.SystemDefaultCharSize == 1)
			{
				CtlTextBase.ToUnicodeOffsets(this.Text, ref num1, ref num2);
			}
			return (num2 - num1);
		}

		internal  int GetSelectionStart()
		{
			int num1 = 0;
			int num2 = 0;
			WinMethods.SendMessage(new HandleRef(this, base.Handle), 0xb0, ref num1, ref num2);
			num1 = Math.Max(0, num1);
			if ((Marshal.SystemDefaultCharSize == 1) && (num1 != 0))
			{
				CtlTextBase.ToUnicodeOffsets(this.Text, ref num1, ref num2);
			}
			return num1;
		}

		protected override bool IsInputKey(Keys keyData)
		{
			if (this.Multiline && ((keyData & Keys.Alt) == Keys.None))
			{
				Keys keys1 = keyData & Keys.KeyCode;
				if (keys1 == Keys.Return)
				{
					return this.acceptsReturn;
				}
			}
			return base.IsInputKey(keyData);
		}

		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			if (!this.selectionSet)
			{
				this.selectionSet = true;
				if ((this.SelectionLength == 0) && (Control.MouseButtons == MouseButtons.None))
				{
					base.SelectAll();
				}
			}
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			if (this.passwordChar != '\0')
			{
				this.SendMessage(0xcc, this.passwordChar, 0);
			}
		}

 
		protected override void OnMouseUp(MouseEventArgs mevent)
		{
			Point point1 = new Point(mevent.X, mevent.Y);
			point1 = base.PointToScreen(point1);
			if (mevent.Button == MouseButtons.Left)
			{
				if (!validationCancelled && (WinAPI.WindowFromPoint(point1.X, point1.Y) == base.Handle))
				{
					if (!this.doubleClickFired)
					{
						this.OnClick(EventArgs.Empty);
					}
					else
					{
						this.doubleClickFired = false;
						this.OnDoubleClick(EventArgs.Empty);
					}
				}
				this.doubleClickFired = false;
			}
			base.OnMouseUp(mevent);
		}

 
		protected virtual void OnTextAlignChanged(EventArgs e)
		{
			EventHandler handler1 = base.Events[CtlTextBase.EVENT_TEXTALIGNCHANGED] as EventHandler;
			if (handler1 != null)
			{
				handler1(this, e);
			}
		}

		internal  void SelectInternal(int start, int length)
		{
			this.selectionSet = true;
			int num1 = start + length;
			if (base.IsHandleCreated)
			{
				if (Marshal.SystemDefaultCharSize == 1)
				{
					CtlTextBase.ToDbcsOffsets(this.Text, ref start, ref num1);
					this.SendMessage(0xb1, start, num1);
				}
				else
				{
					this.SendMessage(0xb1, start, num1);
				}
			}
			else
			{
				base.SelectionStart = start;
				base.SelectionLength = length;
			}
		}

		private static void ToUnicodeOffsets(string str, ref int start, ref int end)
		{
			Encoding encoding1 = Encoding.Default;
			byte[] buffer1 = encoding1.GetBytes(str);
			if (start < 0)
			{
				start = 0;
			}
			if (start > buffer1.Length)
			{
				start = buffer1.Length;
			}
			if (end < start)
			{
				end = start;
			}
			if (end > buffer1.Length)
			{
				end = buffer1.Length;
			}
			int num1 = start;
			if (start != 0)
			{
				num1 = encoding1.GetCharCount(buffer1, 0, start);
			}
			end = num1 + encoding1.GetCharCount(buffer1, start, end - start);
			start = num1;
		}

		[SecurityPermission(SecurityAction.LinkDemand)]
		protected override void WndProc(ref Message m)
		{
			switch (m.Msg)
			{
				case 0x201:
				{
					MouseButtons buttons1 = Control.MouseButtons;
					this.Focus();//FocusInternal();
					if ((buttons1 == Control.MouseButtons) && !validationCancelled)
					{
						base.WndProc(ref m);
						return;
					}
					return;
				}
				case 0x202:
					base.WndProc(ref m);
					return;

				case 0x203:
					this.doubleClickFired = true;
					base.WndProc(ref m);
					return;
			}
			base.WndProc(ref m);
		}

		[Description("TextBoxAcceptsReturn"), Category("Behavior"), DefaultValue(false)]
		public bool AcceptsReturn
		{
			get
			{
				return this.acceptsReturn;
			}
			set
			{
				this.acceptsReturn = value;
			}
		}
 
		[Description("TextBoxCharacterCasing"), DefaultValue(0), Category("Behavior")]
		public CharacterCasing CharacterCasing
		{
			get
			{
				return this.characterCasing;
			}
			set
			{
				if (this.characterCasing != value)
				{
					if (!Enum.IsDefined(typeof(CharacterCasing), value))
					{
						throw new InvalidEnumArgumentException("value", (int) value, typeof(CharacterCasing));
					}
					this.characterCasing = value;
					base.RecreateHandle();
				}
			}
		}
 
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand)]
			get
			{
				CreateParams params1 = base.CreateParams;
				switch (this.characterCasing)
				{
					case CharacterCasing.Upper:
						params1.Style |= 8;
						break;

					case CharacterCasing.Lower:
						params1.Style |= 0x10;
						break;
				}
				HorizontalAlignment alignment1 = base.RtlTranslateHorizontal(this.textAlign);
				params1.ExStyle &= -4097;
				switch (alignment1)
				{
					case HorizontalAlignment.Left:
						params1.Style = params1.Style;
						break;

					case HorizontalAlignment.Right:
						params1.Style |= 2;
						break;

					case HorizontalAlignment.Center:
						params1.Style |= 1;
						break;
				}
				if (this.Multiline)
				{
					if ((((this.scrollBars & ScrollBars.Horizontal) == ScrollBars.Horizontal) && (this.textAlign == HorizontalAlignment.Left)) && !base.WordWrap)
					{
						params1.Style |= 0x100000;
					}
					if ((this.scrollBars & ScrollBars.Vertical) == ScrollBars.Vertical)
					{
						params1.Style |= 0x200000;
					}
				}
				return params1;
			}
		}
 
		protected override ImeMode DefaultImeMode
		{
			get
			{
				if (this.PasswordChar != '\0')
				{
					return ImeMode.Disable;
				}
				return base.DefaultImeMode;
			}
		}
 
		[Localizable(true), DefaultValue('\0'), Category("Behavior"), Description("TextBoxPasswordChar")]
		public char PasswordChar
		{
			get
			{
				return this.passwordChar;
			}
			set
			{
				if (this.passwordChar != value)
				{
					this.passwordChar = value;
					if (base.IsHandleCreated)
					{
						this.SendMessage(0xcc, value, 0);
						base.Invalidate();
					}
				}
			}
		}
 
		[DefaultValue(0), Description("TextBoxScrollBars"), Localizable(true), Category("Appearance")]
		public ScrollBars ScrollBars
		{
			get
			{
				return this.scrollBars;
			}
			set
			{
				if (this.scrollBars != value)
				{
					if (!Enum.IsDefined(typeof(ScrollBars), value))
					{
						throw new InvalidEnumArgumentException("value", (int) value, typeof(ScrollBars));
					}
					this.scrollBars = value;
					base.RecreateHandle();
				}
			}
		}
 
		[Browsable(false), Description("TextBoxSelectionLength"), Category("Appearance"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override int SelectionLength
		{
			get
			{
				if (!base.IsHandleCreated)
				{
					return base.SelectionLength;
				}
				int num1 = 0;
				int num2 = 0;
				WinMethods.SendMessage(new HandleRef(this, base.Handle), 0xb0, ref num1, ref num2);
				num1 = Math.Max(0, num1);
				num2 = Math.Max(0, num2);
				if (Marshal.SystemDefaultCharSize == 1)
				{
					CtlTextBase.ToUnicodeOffsets(this.Text, ref num1, ref num2);
				}
				return (num2 - num1);
			}
			set
			{
				base.SelectionLength = value;
			}
		}
 
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
				this.selectionSet = false;
			}
		}
		[Localizable(true), Description("TextBoxTextAlign"), DefaultValue(0), Category("Appearance")]
		public HorizontalAlignment TextAlign
		{
			get
			{
				return this.textAlign;
			}
			set
			{
				if (this.textAlign != value)
				{
					if (!Enum.IsDefined(typeof(HorizontalAlignment), value))
					{
						throw new InvalidEnumArgumentException("value", (int) value, typeof(HorizontalAlignment));
					}
					this.textAlign = value;
					base.RecreateHandle();
					this.OnTextAlignChanged(EventArgs.Empty);
				}
			}
		}
	
	}
 

}
