using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.ComponentModel;

using mControl.Win32 ;
using mControl.Util;
using mControl.Drawing;
 
namespace mControl.WinCtl.Controls
{

	[ToolboxItem(false)]
	public class CtlCombo : System.Windows.Forms.ComboBox,IStyleCtl
	{	

		#region ComboInfoHelper
		internal class ComboInfoHelper
		{
			[DllImport("user32")] 
			private static extern bool GetComboBoxInfo(IntPtr hwndCombo, ref ComboBoxInfo info);

			#region RECT struct
			[StructLayout(LayoutKind.Sequential)]
				private struct RECT 
			{
				public int Left;
				public int Top;
				public int Right;
				public int Bottom;
			}
			#endregion

			#region ComboBoxInfo Struct
			[StructLayout(LayoutKind.Sequential)]
			private struct ComboBoxInfo 
			{
				public int cbSize;
				public RECT rcItem;
				public RECT rcButton;
				public IntPtr stateButton;
				public IntPtr hwndCombo;
				public IntPtr hwndEdit;
				public IntPtr hwndList;
			}
			#endregion

			public static int GetComboDropDownWidth()
			{
				System.Windows.Forms.ComboBox cb = new System.Windows.Forms.ComboBox();
				int width = GetComboDropDownWidth(cb.Handle);
				cb.Dispose();
				return width;
			}
			public static int GetComboDropDownWidth(IntPtr handle)
			{
				ComboBoxInfo cbi = new ComboBoxInfo();
				cbi.cbSize = Marshal.SizeOf(cbi);
				GetComboBoxInfo(handle, ref cbi);
				int width = cbi.rcButton.Right - cbi.rcButton.Left;
				return width;
			}
		}
		#endregion

		#region Members
		private bool m_AcceptItems = false;
		private System.Windows.Forms.BorderStyle m_BorderStyle= BorderStyle.FixedSingle;

		private Image m_ButtonIcon = null;
	
		private static int DropDownButtonWidth =16;// 17;
		//private ComboValues mValueType=ComboValues.Items; 

		#endregion

		#region Constructor
		static CtlCombo()
		{
			DropDownButtonWidth = ComboInfoHelper.GetComboDropDownWidth();// + 2;
        
		}

        public CtlCombo(): base()
		{
			this.SetStyle(ControlStyles.DoubleBuffer, true);
			base.DropDownStyle=ComboBoxStyle.DropDown;
			m_ButtonIcon=ResourceUtil.LoadImage (Global.ImagesPath + "btnCombo.gif");
			//mControl.Util.Net.netWinCtl.NetFram(this.Name); 
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated (e);
			//mControl.Util.Net.netWinCtl.NetFram(this.Name); 
		}

		#endregion

		#region WndProc
		protected override void WndProc(ref Message m)
		{
			if (this.DropDownStyle == ComboBoxStyle.Simple || this.BorderStyle ==BorderStyle.Fixed3D )
			{
				base.WndProc(ref m);
				return;
			}

			if (m.Msg == 15)
			{
				base.DefWndProc(ref m);
				PaintFlatControl();
			}
			else
			{
				base.WndProc(ref m);
			}

//			IntPtr hDC = IntPtr.Zero;
//			Graphics gdc = null;
//		
//			switch (m.Msg)
//			{
//				case WinMsgs.WM_NCPAINT :// WM_NC_PAINT:	
//					hDC = Win32.WinAPI.GetWindowDC(this.Handle);
//					gdc = Graphics.FromHdc(hDC);
//					Win32.WinAPI.SendMessage(this.Handle, WinMsgs.WM_ERASEBKGND, hDC, 0);
//					SendPrintClientMsg();	// send to draw client area
//					PaintFlatControl(gdc,false);
//					//PaintFlatDropDown(gdc,false);					
//					m.Result = (IntPtr) 1;	// indicate msg has been processed			
//					Win32.WinAPI.ReleaseDC(m.HWnd, hDC);
//					gdc.Dispose();	
//					break;
//
//				case WinMsgs.WM_PAINT:	
//					base.WndProc(ref m);
//					// flatten the border area again
//					hDC = Win32.WinAPI.GetWindowDC(this.Handle);
//					gdc = Graphics.FromHdc(hDC);
//             		gdc.DrawRectangle(new Pen (BackColor,2), new Rectangle(2, 2, this.Width-3, this.Height-3));
//					PaintFlatControl(gdc,false);
//					//PaintFlatDropDown(gdc,false);
//					Win32.WinAPI.ReleaseDC(m.HWnd, hDC);
//					gdc.Dispose();	
//					break;
//				default:
//					base.WndProc(ref m);
//					break;
//			}
		}


		private Rectangle GetButtonRect()
		{
			Rectangle rect;
			Rectangle rectB;
			if(this.RightToLeft==RightToLeft.Yes )
			{
				rect = new Rectangle(2, 2, DropDownButtonWidth, DropDownButtonWidth);
				rectB = new Rectangle(1, 1, DropDownButtonWidth, this.Height-2);
			}
			else
			{
				rect = new Rectangle(this.Width-DropDownButtonWidth-3, 2, DropDownButtonWidth, DropDownButtonWidth);
				rectB = new Rectangle(this.Width-DropDownButtonWidth-2, 1, DropDownButtonWidth, this.Height-2 );
			}
           return rectB;
		}

		private void SendPrintClientMsg()
		{
			// We send this message for the control to redraw the client area
			//Graphics gClient = this.CreateGraphics();
			using (Graphics g = Graphics.FromHwnd(base.Handle))
			{
				IntPtr ptrClientDC = g.GetHdc();
				Win32.WinAPI.SendMessage(this.Handle, WinMsgs.WM_PRINTCLIENT, ptrClientDC, 0);
				g.ReleaseHdc(ptrClientDC);
				//gClient.Dispose();
			}
		}

		private void PaintFlatControl()//Graphics g,bool hot)
		{
			bool hot=this.IsMouseOverButton;

			using (Graphics g = Graphics.FromHwnd(base.Handle))
			{

				Color color1 = this.BackColor;
				Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
				Rectangle rectangle1 = new Rectangle(0, 0, base.Width - 1, base.Height - 1);
				Rectangle rectangle2 = rectangle1;
				int num1 = SystemInformation.Border3DSize.Width;
				int num2 = SystemInformation.Border3DSize.Height;
	
				using (SolidBrush brush1 = new SolidBrush(color1))
				{
					g.FillRectangle(brush1, 1, 1, rectangle2.Width - 1, num2 - 1);
					g.FillRectangle(brush1, 1, (rectangle2.Bottom - num2) + 1, rectangle2.Width - 1, num2 - 1);
					g.FillRectangle(brush1, 1, 1, num1 - 1, rectangle2.Height - num2);
					g.FillRectangle(brush1, (rectangle2.Right - num2) + 1, 1, rectangle2.Width - 1, rectangle2.Height - num2);
				}
	
				if(m_BorderStyle==BorderStyle.None )
					g.DrawRectangle (new Pen (BackColor,1),rect);
				else
					CtlStyleLayout.DrawBorder(g,rectangle1,false,this.Enabled,this.Focused,hot);

				PaintFlatDropDown(g,hot);
			}
		}

		public void PaintFlatDropDown(Graphics g,bool hot)
		{
			Rectangle rect;
			Rectangle rectB;
			if(this.RightToLeft==RightToLeft.Yes )
			{
				rect = new Rectangle(2, 2, DropDownButtonWidth, DropDownButtonWidth);
				rectB = new Rectangle(1, 1, DropDownButtonWidth, this.Height-2);
			}
			else
			{
				rect = new Rectangle(this.Width-DropDownButtonWidth-3, 2, DropDownButtonWidth, DropDownButtonWidth);
				rectB = new Rectangle(this.Width-DropDownButtonWidth-2, 1, DropDownButtonWidth, this.Height-2 );
			}
        
			g.FillRectangle (new  SolidBrush (BackColor ),rectB);
			CtlStyleLayout.DrawGradientRect(g,rect,90f);
			CtlStyleLayout.DrawBorder(g,rect,false,Enabled,Focused,hot);
			
			//ControlPaint.DrawComboButton(g, rect, ButtonState.FixedSingle);

			if(m_ButtonIcon != null )
			{
				Rectangle ButtonRect   = new Rectangle(rect.X ,rect.Y + (rect.Height - rect.Width)/2 + 1,rect.Width,rect.Height);
				ButtonRect.Location = new Point(rect.X+1,rect.Y +1);
				ButtonRect.Height   = rect.Height ;

				DrawUtils.DrawImage(g,m_ButtonIcon,ButtonRect,false,false);
			}
	
		}


		#endregion

		#region overrides
		
		protected override void OnSelectedValueChanged(EventArgs e)
		{
			base.OnSelectedValueChanged (e);
			this.Invalidate();
		}

		protected override void OnLostFocus(System.EventArgs e)
		{
			base.OnLostFocus(e);
			if(this.BorderStyle !=BorderStyle.Fixed3D )
				PaintFlatControl();//this.CreateGraphics (),false);
		}

		protected override void OnGotFocus(System.EventArgs e)
		{
			base.OnGotFocus(e);
			if(this.BorderStyle !=BorderStyle.Fixed3D )
				PaintFlatControl();//this.CreateGraphics (),false);
		}
		
		protected override void OnMouseLeave(System.EventArgs e)
		{
			base.OnMouseLeave(e);
			if(this.BorderStyle !=BorderStyle.Fixed3D )
				PaintFlatControl();//this.CreateGraphics (),false);
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter (e);
			if(this.BorderStyle !=BorderStyle.Fixed3D )
				PaintFlatControl();//this.CreateGraphics (),true);
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize (e);
			this.Invalidate();
		}

		protected override void OnValidating(System.ComponentModel.CancelEventArgs e)
		{
			base.OnValidating(e);
			e.Cancel = Validate_Item();
		
			if (e.Cancel)
			{
				throw new Exception (RM.GetString(RM.ContentNotExistInList));
			}
		}

		#endregion

		#region Methods

		private bool Validate_Item()
		{
			if (this.m_AcceptItems)
			{
				int index = this.Items.IndexOf(this.Text);
				if (index > -1) 
				{
					this.SelectedIndex = index;
				}
				return (index<0);
			}
			return false;
		}


		#endregion

		#region IStyleCtl

		protected IStyle			m_StylePainter;

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
		public virtual IStyleLayout CtlStyleLayout
		{
			get
			{
				if(this.m_StylePainter!=null)
					return this.m_StylePainter.Layout as IStyleLayout;
				else
					return StyleControl.Layout as IStyleLayout;// this.m_Style as IStyleLayout;

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

		protected virtual void OnStylePainterChanged(EventArgs e)
		{
			this.BackColor=CtlStyleLayout.Layout.BackColorInternal;
			this.ForeColor=CtlStyleLayout.Layout.ForeColorInternal;
		}

		protected virtual void OnStylePropertyChanged(PropertyChangedEventArgs e)
		{
			if((DesignMode || IsHandleCreated))
			{
				this.BackColor=CtlStyleLayout.Layout.BackColorInternal;
				this.ForeColor=CtlStyleLayout.Layout.ForeColorInternal;
				this.Invalidate(true);
			}
		}

		private void m_Style_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{

			OnStylePropertyChanged(e);
		}

		#endregion

		#region Properties

		[Browsable(false)]
		public bool IsMouseOverButton
		{
			get
			{
				Rectangle rectangle1 = GetButtonRect();//CtlPaint.GetButtonRect(new Rectangle(0, 0, base.Width - 1, base.Height - 1), this.Flat);
				if (rectangle1.Contains(base.PointToClient(Cursor.Position)))
				{
					return true;
				}
				return false;
			}
		}
	
		[Category("Behavior")]
		public bool AcceptItems
		{
			get {return m_AcceptItems;}
			set {m_AcceptItems = value;}
		}

//		[Category("Style")]
//		public virtual Color ButtonColor
//		{
//			get {return m_ButtonColor;}
//			set 
//			{
//				m_ButtonColor = value;
//				this.Invalidate ();}
//		}

//		[Category("Behavior")]
//		public ComboValues ValueType
//		{
//			get {return mValueType;}
//			set{mValueType = value;}
//		}

		
		[Category("Behavior")]
		public System.Windows.Forms.BorderStyle BorderStyle
		{
			get {return m_BorderStyle;}
			set{m_BorderStyle = value;
				this.Invalidate ();
			}
		}

		[Browsable(false)]
		public Image Image
		{
			get {return m_ButtonIcon;}
		}

		#endregion

		#region IBind Members

		public string BindPropertyName()
		{
			return "SelectedValue";
		}

		#endregion

	}
}
