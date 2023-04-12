using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using Nistec.Win32;

namespace Nistec.Printing
{

    [ToolboxBitmap(typeof(McPrintPreview), "Images.Preview.bmp"), ToolboxItem(true), DefaultProperty("Document")]
	public class McPrintPreview : Control
	{
		// Events
//		[Description("RadioButtonOnStartPageChanged"), Category("PropertyChanged")]
//		public event EventHandler StartPageChanged;
//		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
//		public event EventHandler TextChanged;


		// Fields
		private bool antiAlias;
		private bool autoZoom;
		private const int border = 10;
		private int columns;
		private PrintDocument document;
		private static readonly object EVENT_STARTPAGECHANGED;
 		private Size imageSize;
		private Point lastOffset;
		private bool layoutOk;
		private PreviewPageInfo[] pageInfo;
		private bool pageInfoCalcPending;
		private Point position;
		private int rows;
		private Point screendpi;
		private const int SCROLL_LINE = 5;
		private const int SCROLL_PAGE = 100;
		private int startPage;
		private Size virtualSize;
		private double zoom;


        private bool exceptionPrinting;

        public bool ExceptionPrinting
        {
            get { return exceptionPrinting; }
        }
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
        }
		static McPrintPreview()
		{
			McPrintPreview.EVENT_STARTPAGECHANGED = new object();
		}

		public McPrintPreview()
		{
			this.virtualSize = new Size(1, 1);
			this.position = new Point(0, 0);
			this.startPage = 0;
			this.rows = 1;
			this.columns = 1;
			this.autoZoom = true;
			this.layoutOk = false;
			this.imageSize = Size.Empty;
			this.screendpi = Point.Empty;
			this.zoom = 0.3;
			this.pageInfoCalcPending = false;
			this.exceptionPrinting = false;
			this.ResetBackColor();
			this.ResetForeColor();
			base.Size = new Size(100, 100);
			base.SetStyle(ControlStyles.ResizeRedraw, false);
			base.SetStyle(ControlStyles.Opaque, true);
		}



		private int AdjustScroll(Message m, int pos, int maxPos)
		{
			switch (WinMethods.LOWORD(m.WParam))
			{
				case 0:
				{
					if (pos <= 5)
					{
						pos = 0;
						return pos;
					}
					pos -= 5;
					return pos;
				}
				case 1:
				{
					if (pos >= (maxPos - 5))
					{
						pos = maxPos;
						return pos;
					}
					pos += 5;
					return pos;
				}
				case 2:
				{
					if (pos <= 100)
					{
						pos = 0;
						return pos;
					}
					pos -= 100;
					return pos;
				}
				case 3:
				{
					if (pos >= (maxPos - 100))
					{
						pos = maxPos;
						return pos;
					}
					pos += 100;
					return pos;
				}
				case 4:
				case 5:
				{
					pos = WinMethods.HIWORD(m.WParam);
					return pos;
				}
			}
			return pos;
		}

		private void CalculatePageInfo()
		{
			if (!this.pageInfoCalcPending)
			{
				this.pageInfoCalcPending = true;
				try
				{
					if (this.pageInfo != null)
					{
						return;
					}
					try
					{
						this.ComputePreview();
					}
					catch (Exception exception1)
					{
						this.exceptionPrinting = true;
                        this.errorMessage = exception1.Message;
						//--throw exception1;
					}
					finally
					{
						base.Invalidate();
					}
				}
				finally
				{
					this.pageInfoCalcPending = false;
				}
			}
		}

		private void ComputeLayout()
		{
			this.layoutOk = true;
			if (this.pageInfo.Length == 0)
			{
				base.ClientSize = base.Size;
			}
			else
			{
				Graphics graphics1 = base.CreateGraphics();
				IntPtr ptr1 = graphics1.GetHdc();
				this.screendpi = new Point(Win32.WinAPI.GetDeviceCaps(new HandleRef(graphics1, ptr1), 0x58), Win32.WinAPI.GetDeviceCaps(new HandleRef(graphics1, ptr1), 90));
				graphics1.ReleaseHdcInternal(ptr1);
				graphics1.Dispose();
				Size size1 = this.pageInfo[this.StartPage].PhysicalSize;
				Size size2 = new Size(McPrintPreview.PixelsToPhysical(new Point(base.Size), this.screendpi));
				if (this.autoZoom)
				{
					double num1 = (size2.Width - (10 * (this.columns + 1))) / ((double) (this.columns * size1.Width));
					double num2 = (size2.Height - (10 * (this.rows + 1))) / ((double) (this.rows * size1.Height));
					this.zoom = Math.Min(num1, num2);
				}
				this.imageSize = new Size((int) (this.zoom * size1.Width), (int) (this.zoom * size1.Height));
				int num3 = (this.imageSize.Width * this.columns) + (10 * (this.columns + 1));
				int num4 = (this.imageSize.Height * this.rows) + (10 * (this.rows + 1));
				this.SetVirtualSizeNoInvalidate(new Size(McPrintPreview.PhysicalToPixels(new Point(num3, num4), this.screendpi)));
			}
		}

 
		private void ComputePreview()
		{
			int num1 = this.StartPage;
			if (this.document == null)
			{
				this.pageInfo = new PreviewPageInfo[0];
			}
			else
			{
				//IntSecurity.SafePrinting.Demand();
				PrintController controller1 = this.document.PrintController;
				PreviewPrintController controller2 = new PreviewPrintController();
				controller2.UseAntiAlias = this.UseAntiAlias;
				this.document.PrintController = new PrintStatusDialog(controller2, "PrintController");
				this.document.Print();
				this.pageInfo = controller2.GetPreviewPageInfo();
				this.document.PrintController = controller1;
			}
			if (num1 != this.StartPage)
			{
				this.OnStartPageChanged(EventArgs.Empty);
			}
		}

		private void InvalidateLayout()
		{
			this.layoutOk = false;
			base.Invalidate();
		}

		public void InvalidatePreview()
		{
			this.pageInfo = null;
			this.InvalidateLayout();
		}

 
		protected override void OnPaint(PaintEventArgs pevent)
		{
			Brush brush1 = new SolidBrush(this.BackColor);
            try
            {
                if ((this.pageInfo == null) || (this.pageInfo.Length == 0))
                {
                    pevent.Graphics.FillRectangle(brush1, base.ClientRectangle);
                    if ((this.pageInfo != null) || this.exceptionPrinting)
                    {
                        StringFormat format1 = new StringFormat();
                        //format1.Alignment = ControlPaint.TranslateAlignment(ContentAlignment.MiddleCenter);
                        //format1.LineAlignment = ControlPaint.TranslateLineAlignment(ContentAlignment.MiddleCenter);
                        format1.Alignment = WinMethods.TranslateLineAlignment(ContentAlignment.MiddleCenter);
                        format1.LineAlignment = WinMethods.TranslateLineAlignment(ContentAlignment.MiddleCenter);

                        SolidBrush brush2 = new SolidBrush(this.ForeColor);
                        try
                        {
                            if (this.exceptionPrinting)
                            {
                                pevent.Graphics.DrawString("PrintPreviewExceptionPrinting", this.Font, brush2, (RectangleF)base.ClientRectangle, format1);
                                goto Label_03D5;
                            }
                            pevent.Graphics.DrawString("PrintPreviewNoPages", this.Font, brush2, (RectangleF)base.ClientRectangle, format1);
                            goto Label_03D5;
                        }
                        finally
                        {
                            brush2.Dispose();
                            format1.Dispose();
                        }
                    }
                    base.BeginInvoke(new MethodInvoker(this.CalculatePageInfo));
                }
                else
                {
                    if (!this.layoutOk)
                    {
                        this.ComputeLayout();
                    }
                    Point point1 = McPrintPreview.PhysicalToPixels(new Point(this.imageSize), this.screendpi);
                    Point point2 = new Point(this.VirtualSize);
                    Point point3 = new Point(Math.Max(0, (base.Size.Width - point2.X) / 2), Math.Max(0, (base.Size.Height - point2.Y) / 2));
                    point3.X -= this.Position.X;
                    point3.Y -= this.Position.Y;
                    this.lastOffset = point3;
                    int num1 = McPrintPreview.PhysicalToPixels(10, this.screendpi.X);
                    int num2 = McPrintPreview.PhysicalToPixels(10, this.screendpi.Y);
                    Region region1 = pevent.Graphics.Clip;
                    Rectangle[] rectangleArray1 = new Rectangle[this.rows * this.columns];
                    try
                    {
                        for (int num3 = 0; num3 < this.rows; num3++)
                        {
                            for (int num4 = 0; num4 < this.columns; num4++)
                            {
                                int num5 = (this.StartPage + num4) + (num3 * this.columns);
                                if (num5 < this.pageInfo.Length)
                                {
                                    int num6 = (point3.X + (num1 * (num4 + 1))) + (point1.X * num4);
                                    int num7 = (point3.Y + (num2 * (num3 + 1))) + (point1.Y * num3);
                                    rectangleArray1[num5 - this.StartPage] = new Rectangle(num6, num7, point1.X, point1.Y);
                                    pevent.Graphics.ExcludeClip(rectangleArray1[num5 - this.StartPage]);
                                }
                            }
                        }
                        pevent.Graphics.FillRectangle(brush1, base.ClientRectangle);
                    }
                    finally
                    {
                        pevent.Graphics.Clip = region1;
                    }
                    for (int num8 = 0; num8 < rectangleArray1.Length; num8++)
                    {
                        if ((num8 + this.StartPage) < this.pageInfo.Length)
                        {
                            Rectangle rectangle1 = rectangleArray1[num8];
                            pevent.Graphics.DrawRectangle(Pens.Black, rectangle1);
                            rectangle1.Inflate(-1, -1);
                            pevent.Graphics.FillRectangle(new SolidBrush(this.ForeColor), rectangle1);
                            if (this.pageInfo[num8 + this.StartPage].Image != null)
                            {
                                pevent.Graphics.DrawImage(this.pageInfo[num8 + this.StartPage].Image, rectangle1);
                            }
                            rectangle1.Width--;
                            rectangle1.Height--;
                            pevent.Graphics.DrawRectangle(Pens.Black, rectangle1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
			finally
			{
				brush1.Dispose();
			}
			Label_03D5:
				base.OnPaint(pevent);
		}

		protected override void OnResize(EventArgs eventargs)
		{
			if (this.autoZoom)
			{
				this.InvalidateLayout();
			}
			else
			{
				McPrintPreview.PhysicalToPixels(new Point(this.imageSize), this.screendpi);
				Point point1 = new Point(this.VirtualSize);
				Point point2 = new Point(Math.Max(0, (base.Size.Width - point1.X) / 2), Math.Max(0, (base.Size.Height - point1.Y) / 2));
				point2.X -= this.Position.X;
				point2.Y -= this.Position.Y;
				if ((this.lastOffset.X != point2.X) || (this.lastOffset.Y != point2.Y))
				{
					base.Invalidate();
				}
			}
			base.OnResize(eventargs);
		}

		protected virtual void OnStartPageChanged(EventArgs e)
		{
			EventHandler handler1 = base.Events[McPrintPreview.EVENT_STARTPAGECHANGED] as EventHandler;
			if (handler1 != null)
			{
				handler1(this, e);
			}
		}

 
		private static Point PhysicalToPixels(Point physical, Point dpi)
		{
			return new Point(McPrintPreview.PhysicalToPixels(physical.X, dpi.X), McPrintPreview.PhysicalToPixels(physical.Y, dpi.Y));
		}

 
		private static Size PhysicalToPixels(Size physicalSize, Point dpi)
		{
			return new Size(McPrintPreview.PhysicalToPixels(physicalSize.Width, dpi.X), McPrintPreview.PhysicalToPixels(physicalSize.Height, dpi.Y));
		}

 
		private static int PhysicalToPixels(int physicalSize, int dpi)
		{
			return (int) (((double) (physicalSize * dpi)) / 100);
		}

 
		private static Point PixelsToPhysical(Point pixels, Point dpi)
		{
			return new Point(McPrintPreview.PixelsToPhysical(pixels.X, dpi.X), McPrintPreview.PixelsToPhysical(pixels.Y, dpi.Y));
		}

 
		private static Size PixelsToPhysical(Size pixels, Point dpi)
		{
			return new Size(McPrintPreview.PixelsToPhysical(pixels.Width, dpi.X), McPrintPreview.PixelsToPhysical(pixels.Height, dpi.Y));
		}

		private static int PixelsToPhysical(int pixels, int dpi)
		{
			return (int) ((pixels * 100) / ((double) dpi));
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void ResetBackColor()
		{
			this.BackColor = SystemColors.AppWorkspace;
		}

 
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void ResetForeColor()
		{
			this.ForeColor = Color.White;
		}

 
		private void SetPositionNoInvalidate(Point value)
		{
			Point point1 = this.position;
			this.position = value;
			this.position.X = Math.Min(this.position.X, this.virtualSize.Width - base.Width);
			this.position.Y = Math.Min(this.position.Y, this.virtualSize.Height - base.Height);
			if (this.position.X < 0)
			{
				this.position.X = 0;
			}
			if (this.position.Y < 0)
			{
				this.position.Y = 0;
			}
	
			Rectangle rectangle1 = base.ClientRectangle;
			Win32.RECT rect1 = Win32.RECT.FromXYWH(rectangle1.X, rectangle1.Y, rectangle1.Width, rectangle1.Height);
			Win32.WinAPI.ScrollWindow(new HandleRef(this, base.Handle), point1.X - this.position.X, point1.Y - this.position.Y, ref rect1, ref rect1);
			Win32.WinAPI.SetScrollPos(new HandleRef(this, base.Handle), 0, this.position.X, true);
			Win32.WinAPI.SetScrollPos(new HandleRef(this, base.Handle), 1, this.position.Y, true);

//			Nistec.Win32.RECT rect1 =  Nistec.Win32.RECT(rectangle1.X, rectangle1.Y, rectangle1.Width, rectangle1.Height);
//			Nistec.Win32.sScrollWindow(new HandleRef(this, base.Handle), point1.X - this.position.X, point1.Y - this.position.Y, ref rect1, ref rect1);
//			UnsafeWinMethods.SetScrollPos(new HandleRef(this, base.Handle), 0, this.position.X, true);
//			UnsafeWinMethods.SetScrollPos(new HandleRef(this, base.Handle), 1, this.position.Y, true);

		}

		internal void SetVirtualSizeNoInvalidate(Size value)
		{
			this.virtualSize = value;
			this.SetPositionNoInvalidate(this.position);
			WinMethods.SCROLLINFO scrollinfo1 = new WinMethods.SCROLLINFO();
			scrollinfo1.fMask = 3;
			scrollinfo1.nMin = 0;
			scrollinfo1.nMax = Math.Max(base.Height, this.virtualSize.Height) - 1;
			scrollinfo1.nPage = base.Height;
			WinMethods.SetScrollInfo(new HandleRef(this, base.Handle), 1, scrollinfo1, true);
			scrollinfo1.fMask = 3;
			scrollinfo1.nMin = 0;
			scrollinfo1.nMax = Math.Max(base.Width, this.virtualSize.Width) - 1;
			scrollinfo1.nPage = base.Width;
			WinMethods.SetScrollInfo(new HandleRef(this, base.Handle), 0, scrollinfo1, true);
		}

		internal virtual bool ShouldSerializeBackColor()
		{
			return !this.BackColor.Equals(SystemColors.AppWorkspace);
		}

		internal virtual bool ShouldSerializeForeColor()
		{
			return !this.ForeColor.Equals(Color.White);
		}

		private void WmHScroll(ref Message m)
		{
			if (m.LParam != IntPtr.Zero)
			{
				base.WndProc(ref m);
			}
			else
			{
				Point point1 = this.position;
				int num1 = point1.X;
				int num2 = Math.Max(base.Width, this.virtualSize.Width);
				point1.X = this.AdjustScroll(m, num1, num2);
				this.Position = point1;
			}
		}

		private void WmKeyDown(ref Message msg)
		{
			Keys keys1 = ((Keys) ((int) msg.WParam)) | Control.ModifierKeys;
			switch ((keys1 & Keys.KeyCode))
			{
				case Keys.Prior:
				{
					this.StartPage--;
					return;
				}
				case Keys.Next:
				{
					this.StartPage++;
					return;
				}
				case Keys.End:
				{
					if ((keys1 & ((Keys) (-65536))) == Keys.Control)
					{
						this.StartPage = this.pageInfo.Length;
					}
					return;
				}
				case Keys.Home:
				{
					if ((keys1 & ((Keys) (-65536))) == Keys.Control)
					{
						this.StartPage = 0;
					}
					return;
				}
			}
		}

		private void WmVScroll(ref Message m)
		{
			if (m.LParam != IntPtr.Zero)
			{
				base.WndProc(ref m);
			}
			else
			{
				Point point1 = this.Position;
				int num1 = point1.Y;
				int num2 = Math.Max(base.Height, this.virtualSize.Height);
				point1.Y = this.AdjustScroll(m, num1, num2);
				this.Position = point1;
			}
		}

 
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode=true)]
		protected override void WndProc(ref Message m)
		{
			int num1 = m.Msg;
			if (num1 != 0x100)
			{
				switch (num1)
				{
					case 0x114:
					{
						this.WmHScroll(ref m);
						return;
					}
					case 0x115:
					{
						this.WmVScroll(ref m);
						return;
					}
				}
			}
			else
			{
				this.WmKeyDown(ref m);
				return;
			}
			base.WndProc(ref m);
		}

		[Description("PrintPreviewAutoZoom"), DefaultValue(true), Category("Behavior")]
		public bool AutoZoom
		{
			get
			{
				return this.autoZoom;
			}
			set
			{
				this.autoZoom = value;
				this.InvalidateLayout();
			}
		}
 
		[DefaultValue(1), Category("Layout"), Description("PrintPreviewColumns")]
		public int Columns
		{
			get
			{
				return this.columns;
			}
			set
			{
				this.columns = value;
				this.InvalidateLayout();
			}
		}
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode=true)]
			get
			{
				CreateParams params1 = base.CreateParams;
				params1.Style |= 0x100000;
				params1.Style |= 0x200000;
				return params1;
			}
		}
 
		[Category("Behavior"), DefaultValue((string) null), Description("PrintPreviewDocument")]
		public PrintDocument Document
		{
			get
			{
				return this.document;
			}
			set
			{
				this.document = value;
				this.InvalidatePreview();
			}
		}
 
		[Category("Layout"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("ControlWithScrollbarsPosition")]
		private Point Position
		{
			get
			{
				return this.position;
			}
			set
			{
				this.SetPositionNoInvalidate(value);
			}
		}
 
		[Category("Behavior"), DefaultValue(1), Description("PrintPreviewRows")]
		public int Rows
		{
			get
			{
				return this.rows;
			}
			set
			{
				this.rows = value;
				this.InvalidateLayout();
			}
		}
 
		public int PagesCount
		{
			get
			{
				if (this.pageInfo != null)
				{
					return this.pageInfo.Length;
				}
				return -1;
			}
		}

		public int CurrentPage
		{
			get
			{
				int num1 = this.startPage;
				if (this.pageInfo != null)
				{
					num1 = Math.Min(num1, this.pageInfo.Length - (this.rows * this.columns));
				}
				return Math.Max(num1, 0);
			}
		}

		[DefaultValue(0), Description("PrintPreviewStartPage"), Category("Behavior")]
		public int StartPage
		{
			get
			{
				int num1 = this.startPage;
				if (this.pageInfo != null)
				{
					num1 = Math.Min(num1, this.pageInfo.Length - (this.rows * this.columns));
				}
				return Math.Max(num1, 0);
			}
			set
			{
				int num1 = this.StartPage;
				this.startPage = value;
				if (num1 != this.startPage)
				{
					this.InvalidateLayout();
					this.OnStartPageChanged(EventArgs.Empty);
				}
			}
		}
 
		[Bindable(false), EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}
 
		[Category("Behavior"), DefaultValue(false), Description("PrintPreviewAntiAlias")]
		public bool UseAntiAlias
		{
			get
			{
				return this.antiAlias;
			}
			set
			{
				this.antiAlias = value;
			}
		}
		[Browsable(false), Category("Layout"), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("ControlWithScrollbarsVirtualSize")]
		private Size VirtualSize
		{
			get
			{
				return this.virtualSize;
			}
			set
			{
				this.SetVirtualSizeNoInvalidate(value);
				base.Invalidate();
			}
		}
 
		[Description("PrintPreviewZoom"), Category("Behavior")]
		public double Zoom
		{
			get
			{
				return this.zoom;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentException("PrintPreviewControlZoomNegative");
				}
				this.autoZoom = false;
				this.zoom = value;
				this.InvalidateLayout();
			}
		}
		[Description("RadioButtonOnStartPageChanged"), Category("PropertyChanged")]
		public event EventHandler StartPageChanged
		{
			add
			{
				base.Events.AddHandler(McPrintPreview.EVENT_STARTPAGECHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(McPrintPreview.EVENT_STARTPAGECHANGED, value);
			}
		}
 
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TextChanged
		{
			add
			{
				base.TextChanged += value;
			}
			remove
			{
				base.TextChanged -= value;
			}
		}





	}
 

}
