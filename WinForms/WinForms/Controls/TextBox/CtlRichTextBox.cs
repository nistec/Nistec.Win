using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;

using Nistec.Win32;

namespace Nistec.WinForms
{
    [ToolboxItem(true), ToolboxBitmap(typeof(McRichTextBox), "Toolbox.RichTextBox.bmp"), Designer(typeof(Design.ControlDesignerBase))]
	public class McRichTextBox : RichTextBox
	{

		#region Members

		// Fields
		private const uint CFE_AUTOCOLOR = 0x40000000;
		private const uint CFE_BOLD = 1;
		private const uint CFE_ITALIC = 2;
		private const uint CFE_LINK = 0x20;
		private const uint CFE_PROTECTED = 0x10;
		private const uint CFE_STRIKEOUT = 8;
		private const uint CFE_UNDERLINE = 4;
		private const uint CFM_BOLD = 1;
		private const uint CFM_CHARSET = 0x8000000;
		private const uint CFM_COLOR = 0x40000000;
		private const uint CFM_FACE = 0x20000000;
		private const uint CFM_ITALIC = 2;
		private const uint CFM_LINK = 0x20;
		private const uint CFM_OFFSET = 0x10000000;
		private const uint CFM_PROTECTED = 0x10;
		private const uint CFM_SIZE = 0x80000000;
		private const uint CFM_STRIKEOUT = 8;
		private const uint CFM_UNDERLINE = 4;
		private const int SCF_ALL = 4;
		private const int SCF_SELECTION = 1;
		private const int SCF_WORD = 2;

		// Nested Types
		[StructLayout(LayoutKind.Sequential)]
		private struct CHARFORMATSTRUCT
		{
			public int cbSize;
			public uint dwMask;
			public uint dwEffects;
			public int yHeight;
			public int yOffset;
			public int crTextColor;
			public byte bCharSet;
			public byte bPitchAndFamily;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst=0x20)]
			public char[] szFaceName;
		}
		#endregion

		#region Constructor

		public McRichTextBox()
		{
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated (e);
			base.BackColor=LayoutManager.Layout.BackColorInternal;
			base.ForeColor=LayoutManager.Layout.ForeColorInternal;
			//Nistec.Util.Net.netWinMc.NetFram(this.Name); 	

		}

		#endregion

		#region Methods

		public bool SetSelectionBold(bool bold)
		{
			return this.SetSelectionStyle((uint)1, bold ? (uint)1 : (uint)0);
		}

		public bool SetSelectionFont(string face)
		{
			McRichTextBox.CHARFORMATSTRUCT charformatstruct1 = new McRichTextBox.CHARFORMATSTRUCT();
			charformatstruct1.cbSize = Marshal.SizeOf(charformatstruct1);
			charformatstruct1.szFaceName = new char[0x20];
			charformatstruct1.dwMask = 0x20000000;
			face.CopyTo(0, charformatstruct1.szFaceName, 0, Math.Min(0x1f, face.Length));
			IntPtr ptr1 = Marshal.AllocCoTaskMem(Marshal.SizeOf(charformatstruct1));
			Marshal.StructureToPtr(charformatstruct1, ptr1, false);
			int num1 = WinMethods.SendMessage(base.Handle, 0x444, 1, ptr1);
			return (num1 == 0);
		}

		public bool SetSelectionItalic(bool italic)
		{
			return this.SetSelectionStyle((uint)2,italic ? (uint)2 : (uint)0);
		}

 
		public bool SetSelectionSize(int size)
		{
			McRichTextBox.CHARFORMATSTRUCT charformatstruct1 = new McRichTextBox.CHARFORMATSTRUCT();
			charformatstruct1.cbSize = Marshal.SizeOf(charformatstruct1);
			charformatstruct1.dwMask = 0x80000000;
			charformatstruct1.yHeight = size * 20;
			IntPtr ptr1 = Marshal.AllocCoTaskMem(Marshal.SizeOf(charformatstruct1));
			Marshal.StructureToPtr(charformatstruct1, ptr1, false);
			int num1 = WinMethods.SendMessage(base.Handle, 0x444, 1, ptr1);
			return (num1 == 0);
		}

		private bool SetSelectionStyle(uint mask, uint effect)
		{
			McRichTextBox.CHARFORMATSTRUCT charformatstruct1 = new McRichTextBox.CHARFORMATSTRUCT();
			charformatstruct1.cbSize = Marshal.SizeOf(charformatstruct1);
			charformatstruct1.dwMask = mask;
			charformatstruct1.dwEffects = effect;
			IntPtr ptr1 = Marshal.AllocCoTaskMem(Marshal.SizeOf(charformatstruct1));
			Marshal.StructureToPtr(charformatstruct1, ptr1, false);
			int num1 = WinMethods.SendMessage(base.Handle, 0x444, 1, ptr1);
			return (num1 == 0);
		}

		public bool SetSelectionUnderlined(bool underlined)
		{
			return this.SetSelectionStyle((uint)4, underlined ? (uint)4 : (uint)0);
		}

		#endregion

		#region Override

		private void PaintFlatControl(bool hot)
		{

			Graphics g=this.CreateGraphics();
			Rectangle rect = new Rectangle(0, 0, this.Width-1, this.Height-1);
			
			this.LayoutManager.DrawBorder(g,rect,this.ReadOnly,this.Enabled,this.Focused,hot);
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
                base.BackColor = value;
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

        protected virtual void OnStylePropertyChanged(PropertyChangedEventArgs e)
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
        protected virtual void OnStylePainterChanged(EventArgs e)
        {
            OnStylePropertyChanged(new PropertyChangedEventArgs("ControlLayout"));
        }

        #endregion


		#region Property

//		private BorderStyle m_BorderStyle=BorderStyle.Fixed3D;
//
//		public new BorderStyle BorderStyle
//		{
//			get{return m_BorderStyle;}
//			set
//			{
//				if(value==BorderStyle.FixedSingle)
//				{
//					base.BorderStyle=BorderStyle.None;
//				}
//				else
//				{
//					base.BorderStyle=value;
//				}
//				m_BorderStyle=value;
//			}
//		}


        ////[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        //public override Color BackColor
        //{
        //    get{return base.BackColor;}
        //    set{base.BackColor=value;}
        //}
        ////[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        //public override Color ForeColor
        //{
        //    get{return base.ForeColor;}
        //    set{base.ForeColor=value;}
        //}
		#endregion

		#region ILayout

		protected IStyle		m_StylePainter;

		[Category("Style"),DefaultValue(null),RefreshProperties(RefreshProperties.All)]
		public IStyle StylePainter
		{
			get {return m_StylePainter;}
			set 
			{
				if(m_StylePainter!=value)
				{
					if (this.m_StylePainter != null)
						this.m_StylePainter.PropertyChanged -=new PropertyChangedEventHandler(m_Style_PropertyChanged); 
					m_StylePainter = value;
					if (this.m_StylePainter != null)
						this.m_StylePainter.PropertyChanged +=new PropertyChangedEventHandler(m_Style_PropertyChanged); 
					OnStylePainterChanged(EventArgs.Empty);
					this.Invalidate(true);
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


		private void m_Style_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{

			OnStylePropertyChanged(e);
		}

		#endregion

	}
 

}
