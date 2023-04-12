using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.ComponentModel;

using Nistec.Win32;
  
namespace Nistec.WinForms
{
	[Designer(typeof(Design.McDesigner))]
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(McTreeView), "Toolbox.TreeView.bmp")]
	public class McTreeView: System.Windows.Forms.TreeView ,ILayout //,IMcKeyAction
	{	

		#region Constructor

		public McTreeView(): base()
		{
			base.BorderStyle=BorderStyle.FixedSingle; 
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated (e);
			//Nistec.Util.Net.netWinMc.NetFram(this.Name); 
		}

		#endregion

		#region WndProc
		protected override void WndProc(ref Message m)
		{
			if(this.BorderStyle !=BorderStyle.FixedSingle)
			{
				base.WndProc(ref m);
				return;
			}

			IntPtr hDC = IntPtr.Zero;
			Graphics gdc = null;
			switch (m.Msg)
			{
//				case WinMsgs.WM_MOUSEHOVER:	
//				case WinMsgs.WM_MOUSEMOVE:	
//					base.WndProc(ref m);
//					hDC = WinAPI.GetWindowDC(this.Handle);
//					gdc = Graphics.FromHdc(hDC);
//					PaintFlatControl(gdc,true);
//					WinAPI.ReleaseDC(m.HWnd, hDC);
//					gdc.Dispose();	
//					break;
//				case WinMsgs.WM_SETFOCUS :	
//				case WinMsgs.WM_KILLFOCUS :	
//				case WinMsgs.WM_MOUSELEAVE:	
				case WinMsgs.WM_PAINT:	
					base.WndProc(ref m);
					hDC = WinAPI.GetWindowDC(this.Handle);
					gdc = Graphics.FromHdc(hDC);
					PaintFlatControl(gdc,false);
					WinAPI.ReleaseDC(m.HWnd, hDC);
					gdc.Dispose();	
					break;
				default:
					base.WndProc(ref m);
					break;
			}
		}

		private void PaintFlatControl(Graphics g,bool hot)
		{

			Rectangle rect = new Rectangle(0, 0, this.Width-1, this.Height-1);
			
			this.LayoutManager.DrawBorder(g,rect,false,this.Enabled,this.Focused,hot);

//			Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
//
//			if (! this.Enabled  )
//				ControlPaint.DrawBorder(g, rect, m_Style.DisabledColor , ButtonBorderStyle.Solid  );
//			else if (Focused )
//				ControlPaint.DrawBorder(g, rect, m_Style.FocusedColor , ButtonBorderStyle.Solid  );
//			else if(hot)
//				ControlPaint.DrawBorder(g, rect, m_Style.BorderHotColor , ButtonBorderStyle.Solid  );
//			else 
//				ControlPaint.DrawBorder(g, rect, m_Style.BorderColor , ButtonBorderStyle.Solid  );
		}

		#endregion

		#region InternalEvents
		protected override void OnResize(EventArgs e)
		{
			base.OnResize (e);
			this.Invalidate();
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

        [Category("Apperarace"), DefaultValue(BorderStyle.FixedSingle)]
        public new BorderStyle BorderStyle
        {
            get { return base.BorderStyle; }
            set { base.BorderStyle = value; }
        }

        protected virtual void OnStylePainterChanged(EventArgs e)
        {
            OnStylePropertyChanged(new PropertyChangedEventArgs("ControlLayout"));
        }

        #endregion


		#region ILayout

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Category("Style"), DefaultValue(ControlLayout.Visual)]
        public virtual ControlLayout ControlLayout
        {
            get { return ControlLayout.Visual; }
            set
            {
            }
        }

		protected IStyle		m_StylePainter;

		[Browsable(false)]
		public PainterTypes PainterType
		{
			get{return PainterTypes.Edit;}
		}
		
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

		#region TreeBoxMethods

		public void Lock()
		{
			Win32.WinAPI.LockWindowUpdate(base.Handle);
		}

		protected override void OnClick(EventArgs e)
		{
			base.OnClick(e);
			Point point1 = base.PointToClient(Cursor.Position);
			TreeNode node1 = base.GetNodeAt(point1);
			base.SelectedNode = node1;
		}
 
		public void SetBold(TreeNode node, bool isBold)
		{
			McTreeView.SetBold(this, node, isBold);
		}

		public static void SetBold(McTreeView treeView, TreeNode node, bool isBold)
		{
			try
			{
				if (node.Handle != IntPtr.Zero)
				{
					TVITEMEX tvitemex1 = new TVITEMEX();
					tvitemex1.mask = 0x18;
					tvitemex1.hItem = node.Handle;
					tvitemex1.state = isBold ? 0x10 : 0;
					tvitemex1.stateMask = 0x10;
					IntPtr ptr1 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(TVITEMEX)));
					Marshal.StructureToPtr(tvitemex1, ptr1, true);
					Win32.WinAPI.SendMessage(treeView.Handle, 0x113f, 0, ptr1);
					Marshal.FreeHGlobal(ptr1);
				}
			}
			catch
			{
			}
		}

		public void Unlock()
		{
			Win32.WinAPI.LockWindowUpdate(IntPtr.Zero);
		}


		// Fields
		private const int TV_FIRST = 0x1100;
		private const int TVIF_HANDLE = 0x10;
		private const int TVIF_STATE = 8;
		private const int TVIS_BOLD = 0x10;
		private const int TVM_SETITEMW = 0x113f;
	#endregion

	}
}
