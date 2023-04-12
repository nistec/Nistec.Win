using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using System.ComponentModel;

namespace Nistec.WinForms
{
    public enum NotifyStyle
    {
        Msg,
        Info,
        Dialog
    }

	/// <summary>
	/// Display An MSN-Messenger-Style NotifyWindow.
	/// </summary>
	public class NotifyWindow : System.Windows.Forms.Form,ILayout
	{
		#region Public Variables
		/// <summary>
		/// Gets or sets the title text to be displayed in the NotifyWindow.
		/// </summary>
		public string Title;
		/// <summary>
		/// Gets or sets the Font used for the title text.
		/// </summary>
		public System.Drawing.Font TitleFont;
		/// <summary>
		/// Gets or sets the Font used when the mouse hovers over the main body of text.
		/// </summary>
		public System.Drawing.Font HoverFont;
		/// <summary>
		/// Gets or sets the Font used when the mouse hovers over the title text.
		/// </summary>
		public System.Drawing.Font TitleHoverFont;
		/// <summary>
		/// Gets or sets the style used when drawing the background of the NotifyWindow.
		/// </summary>
		public BackgroundStyles BackgroundStyle;
		/// <summary>
		/// Gets or sets the Blend used when drawing a gradient background for the NotifyWindow.
		/// </summary>
		public System.Drawing.Drawing2D.Blend Blend;
		/// <summary>
		/// Gets or sets the StringFormat used when drawing text in the NotifyWindow.
		/// </summary>
		public System.Drawing.StringFormat StringFormat;
		/// <summary>
		/// Gets or sets a value specifiying whether or not the window should continue to be displayed if the mouse cursor is inside the bounds
		/// of the NotifyWindow.
		/// </summary>
		public bool WaitOnMouseOver;
		/// <summary>
		/// An EventHandler called when the NotifyWindow main text is clicked.
		/// </summary>
		public event System.EventHandler TextClicked;
		/// <summary>
		/// An EventHandler called when the NotifyWindow title text is clicked.
		/// </summary>
		public event System.EventHandler TitleClicked;
		/// <summary>
		/// Gets or sets the color of the title text.
		/// </summary>
		public System.Drawing.Color TitleColor;
		/// <summary>
		/// Gets or sets the color of the NotifyWindow main text.
		/// </summary>
		public System.Drawing.Color TextColor;
		/// <summary>
		/// Gets or sets the gradient color which will be blended in drawing the background.
		/// </summary>
		public System.Drawing.Color GradientColor;
		/// <summary>
		/// Gets or sets the color of text when the user clicks on it.
		/// </summary>
		public System.Drawing.Color PressedColor;
		/// <summary>
		/// Gets or sets the amount of milliseconds to display the NotifyWindow for.
		/// </summary>
		public int WaitTime;
		/// <summary>
		/// Gets or sets the full height of the NotifyWindow, used after the opening animation has been completed.
		/// </summary>
		public int ActualHeight;
		/// <summary>
		/// Gets or sets the full width of the NotifyWindow.
		/// </summary>
		public int ActualWidth;

		public enum BackgroundStyles { BackwardDiagonalGradient, ForwardDiagonalGradient, HorizontalGradient, VerticalGradient, Solid };
		public enum ClockStates { Opening, Closing, Showing, None };
		public ClockStates ClockState;
		#endregion

		#region Protected Variables
		protected bool closePressed = false, textPressed = false, titlePressed = false, closeHot = false, textHot = false, titleHot = false;
		protected Rectangle rClose, rText, rTitle, rDisplay, rScreen, rGlobClose, rGlobText, rGlobTitle, rGlobDisplay;
		protected System.Windows.Forms.Timer viewClock;
		//private bool FlatBorder=false;
        private NotifyStyle notifyStyle;

        public NotifyStyle NotifyStyle
        {
            get { return notifyStyle; }
            set { notifyStyle = value; }
        }
		#endregion

		#region Constructor
		/// <param name="title">Title text displayed in the NotifyWindow</param>
		/// <param name="text">Main text displayedin the NotifyWindow</param>
		public NotifyWindow (string title, string text) : this() { Title = title; Text = text; }
		/// <param name="text">Text displayed in the NotifyWindow</param>
		public NotifyWindow (string text) : this() { Text = text; }
		public NotifyWindow()
		{
			SetStyle (ControlStyles.UserMouse, true);
			SetStyle (ControlStyles.UserPaint, true);
			SetStyle (ControlStyles.AllPaintingInWmPaint, true);		// WmPaint calls OnPaint and OnPaintBackground
			SetStyle (ControlStyles.DoubleBuffer, true);

            notifyStyle = NotifyStyle.Dialog;
			ShowInTaskbar = false;
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			StartPosition = System.Windows.Forms.FormStartPosition.Manual;

			// Default values
			BackgroundStyle = BackgroundStyles.VerticalGradient;
			ClockState = ClockStates.None;
			BackColor = SystemColors.Info;
			GradientColor = Color.WhiteSmoke;
			PressedColor = Color.Gray;
			TitleColor = SystemColors.ControlText;
			TextColor = SystemColors.ControlText;
			WaitOnMouseOver = true;
			ActualWidth = 130;
			ActualHeight = 110;
			WaitTime = 11000;
		}
	
		public static NotifyWindow CreateNotifyWindow(IStyle style, NotifyStyle ns, string Title,string Text)
		{
		 NotifyWindow nf =new NotifyWindow ();
         nf.Title =Title;
         nf.Text =Text;
         nf.SetStyleLayout(style);
         nf.notifyStyle = ns;
         return nf;
		}

        public static void ShowNotifyMsg(IStyle style, NotifyStyle ns, string Title, string Text)
		{
			NotifyWindow nf =new NotifyWindow ();
			nf.Title =Title;
			nf.Text =Text;
			nf.BackColor=SystemColors.Info;
			//nf.TitleColor =Color.White ;

            nf.SetStyleLayout(style);
            nf.notifyStyle = ns;

			//nf.FlatBorder =true;
	      	nf.ActualWidth = 220;
			nf.ActualHeight = 110;
			nf.WaitOnMouseOver = false;
			nf.WaitTime = 2000;
			nf.NotifyDisplay () ;
		}

        public void ShowNotify(int width, int height, bool waitOnMouseOver, int waitTime)
        {
            //this.BackColor = backColor;
            //this.FlatBorder = FlatBorder;
            this.ActualWidth = width;
            this.ActualHeight = height;
            this.WaitOnMouseOver = waitOnMouseOver;
            this.WaitTime = waitTime;
            this.NotifyDisplay();

        }
        public void ShowNotify()
        {
            //this.BackColor = SystemColors.Info;
            //this.FlatBorder = true;
            this.ActualWidth = 220;
            this.ActualHeight = 110;
            this.WaitOnMouseOver = false;
            this.WaitTime = 2000;
            this.NotifyDisplay();

        }
		
		#endregion

		#region Public Methods
		/// <summary>
		/// Sets the width and height of the NotifyWindow.
		/// </summary>
		public void SetDimensions (int width, int height)
		{
			ActualWidth = width;
			ActualHeight = height;
		}

		/// <summary>
		/// Displays the NotifyWindow.
		/// </summary>
		private void NotifyDisplay()
		{

			if (Text == null || Text.Length < 1)
				throw new System.Exception ("You must set NotifyWindow.Text before calling Notify()");

			Width = ActualWidth;
			rScreen = Screen.GetWorkingArea (Screen.PrimaryScreen.Bounds);
			Height = 0;
			Top = rScreen.Bottom;
			Left = rScreen.Width - Width - 11;
			
			if (HoverFont == null)
				HoverFont = new Font (Font, Font.Style | FontStyle.Underline);
			if (TitleFont == null)
				TitleFont = Font;
			if (TitleHoverFont == null)
				TitleHoverFont = new Font (TitleFont, TitleFont.Style | FontStyle.Underline);
			if (this.StringFormat == null)
			{
				this.StringFormat = new StringFormat();
				this.StringFormat.Alignment = StringAlignment.Center;
				this.StringFormat.LineAlignment = StringAlignment.Center;
				this.StringFormat.Trimming = StringTrimming.EllipsisWord;
			}

			rDisplay = new Rectangle (0, 0, Width, ActualHeight);
			rClose = new Rectangle (Width - 21, 10, 13, 13);

			int offset;
			if (Title != null)
			{
				using (Graphics fx = CreateGraphics())
				{
					SizeF sz = fx.MeasureString (Title, TitleFont, ActualWidth - rClose.Width - 22, this.StringFormat);
					rTitle = new Rectangle (11, 12, (int) Math.Ceiling (sz.Width), (int) Math.Ceiling (sz.Height));
					offset = (int) Math.Max (Math.Ceiling (sz.Height + rTitle.Top + 2), rClose.Bottom + 5);
				}
			}
			else
			{
				offset = rClose.Bottom + 1;
				rTitle = new Rectangle (-1, -1, 1, 1);
			}

			rText = new Rectangle (11, offset, ActualWidth - 22, ActualHeight - (int)(offset * 1.5));
			// rGlob* are Rectangle's Offset'ed to their actual position on the screen, for use with Cursor.Position.
			rGlobClose = rClose;
			rGlobClose.Offset (Left, rScreen.Bottom - ActualHeight);
			rGlobText = rText;
			rGlobText.Offset (Left, rScreen.Bottom - ActualHeight);
			rGlobTitle = rTitle;
			if (Title != null)
				rGlobTitle.Offset (Left, rScreen.Bottom - ActualHeight);
			rGlobDisplay = rDisplay;
			rGlobDisplay.Offset (Left, rScreen.Bottom - ActualHeight);
			rGlobClose = rClose;
			rGlobClose.Offset (Left, rScreen.Bottom - ActualHeight);
			rGlobDisplay = rDisplay;
			rGlobDisplay.Offset (Left, rScreen.Bottom - ActualHeight);

			// Use unmanaged ShowWindow() and SetWindowPos() instead of the managed Show() to display the window - this method will display
			// the window TopMost, but without stealing focus (namely the SW_SHOWNOACTIVATE and SWP_NOACTIVATE flags)
			ShowWindow (Handle, SW_SHOWNOACTIVATE);
			SetWindowPos (Handle, HWND_TOPMOST, rScreen.Width - ActualWidth - 11, rScreen.Bottom, ActualWidth, 0, SWP_NOACTIVATE);

            if (NotifyStyle == NotifyStyle.Info)
            {
                Top = rScreen.Height - ActualHeight;
                Height = ActualHeight;
                this.Opacity = 0;
            }
			viewClock = new System.Windows.Forms.Timer();
			viewClock.Tick += new System.EventHandler (viewTimer);
			viewClock.Interval = 1;
			viewClock.Start();

			ClockState = ClockStates.Opening;
		}
		#endregion

		#region Drawing
		protected override void OnPaint (System.Windows.Forms.PaintEventArgs e)
		{
			// Draw the close button and text.
			drawCloseButton (e.Graphics);

			Font useFont;  Color useColor;
			if (Title != null)
			{
				if (titleHot)
					useFont = TitleHoverFont;
				else
					useFont = TitleFont;
				if (titlePressed)
					useColor = PressedColor;
				else
					useColor = TitleColor;
				using (SolidBrush sb = new SolidBrush (useColor))
					e.Graphics.DrawString (Title, useFont, sb, rTitle, this.StringFormat);
			}

			if (textHot)
				useFont = HoverFont;
			else
				useFont = Font;
			if (textPressed)
				useColor = PressedColor;
			else
				useColor = TextColor;
			using (SolidBrush sb = new SolidBrush (useColor))
				e.Graphics.DrawString (Text, useFont, sb, rText, this.StringFormat);
		}

		protected override void OnPaintBackground (System.Windows.Forms.PaintEventArgs e)
		{
			// First paint the background
			if (BackgroundStyle == BackgroundStyles.Solid)
			{
				using (SolidBrush sb = new SolidBrush (BackColor))
					e.Graphics.FillRectangle (sb, rDisplay);
			}
			else
			{
				LinearGradientMode lgm;
				switch (BackgroundStyle)
				{
					case BackgroundStyles.BackwardDiagonalGradient:
						lgm = LinearGradientMode.BackwardDiagonal;
						break;
					case BackgroundStyles.ForwardDiagonalGradient:
						lgm = LinearGradientMode.ForwardDiagonal;
						break;
					case BackgroundStyles.HorizontalGradient:
						lgm = LinearGradientMode.Horizontal;
						break;
					default:
					case BackgroundStyles.VerticalGradient:
						lgm = LinearGradientMode.Vertical;
						break;
				}
				using (LinearGradientBrush lgb = new LinearGradientBrush (rDisplay, GradientColor, BackColor, lgm))
				{
					if (this.Blend != null)
						lgb.Blend = this.Blend;
					e.Graphics.FillRectangle (lgb, rDisplay);
				}
			}

			// Next draw borders...
			drawBorder (e.Graphics);
		}

		protected virtual void drawBorder (Graphics fx)
		{

            StyleLayout layout = this.LayoutManager.Layout;

                fx.DrawRectangle (layout.GetPenBorder() , 2, 2, Width - 4, ActualHeight - 4);
				Rectangle rct=new Rectangle (rTitle.X ,rTitle.Y-3 ,ActualWidth-rClose.Width -4,rTitle.Height+3 ) ;
                if (this.NotifyStyle == NotifyStyle.Info)
                {
                    fx.FillRectangle(layout.GetBrushFlatLayout(), rct);
                }
                else
                {
                    fx.FillRectangle(layout.GetBrushCaptionGradient(rct,90f,true), rct);
                }

            //if(FlatBorder)
            //{
            //    fx.DrawRectangle (Pens.Blue , 2, 2, Width - 4, ActualHeight - 4);
            //    Rectangle rct=new Rectangle (rTitle.X ,rTitle.Y-3 ,ActualWidth-rClose.Width -4,rTitle.Height+3 ) ;
            //    fx.FillRectangle (new  SolidBrush (Color.LightSlateGray  ) , rct);
            //}
            //else
            //{
            //    fx.DrawRectangle (Pens.Silver, 2, 2, Width - 4, ActualHeight - 4);
			
            //    // Top border
            //    fx.DrawLine (Pens.Silver, 0, 0, Width, 0);
            //    fx.DrawLine (Pens.White, 0, 1, Width, 1);
            //    fx.DrawLine (Pens.DarkGray, 3, 3, Width - 4, 3);
            //    fx.DrawLine (Pens.DimGray, 4, 4, Width - 5, 4);

            //    // Left border
            //    fx.DrawLine (Pens.Silver, 0, 0, 0, ActualHeight);
            //    fx.DrawLine (Pens.White, 1, 1, 1, ActualHeight);
            //    fx.DrawLine (Pens.DarkGray, 3, 3, 3, ActualHeight - 4);
            //    fx.DrawLine (Pens.DimGray, 4, 4, 4, ActualHeight - 5);

            //    // Bottom border
            //    fx.DrawLine (Pens.DarkGray, 1, ActualHeight - 1, Width - 1, ActualHeight - 1);
            //    fx.DrawLine (Pens.White, 3, ActualHeight - 3, Width - 3, ActualHeight - 3);
            //    fx.DrawLine (Pens.Silver, 4, ActualHeight - 4, Width - 4, ActualHeight - 4);

            //    // Right border
            //    fx.DrawLine (Pens.DarkGray, Width - 1, 1, Width - 1, ActualHeight - 1);
            //    fx.DrawLine (Pens.White, Width - 3, 3, Width - 3, ActualHeight - 3);
            //    fx.DrawLine (Pens.Silver, Width - 4, 4, Width - 4, ActualHeight - 4);
            //}
		}

		protected virtual void drawCloseButton (Graphics fx)
		{
			if (visualStylesEnabled())
				drawThemeCloseButton (fx);
			else
				drawLegacyCloseButton (fx);
		}

		/// <summary>
		/// Draw a Windows XP style close button.
		/// </summary>
		protected void drawThemeCloseButton (Graphics fx)
		{
			IntPtr hTheme = OpenThemeData (Handle, "Window");
			if (hTheme == IntPtr.Zero)
			{
				drawLegacyCloseButton (fx);
				return;
			}
			int stateId;
			if (closePressed)
				stateId = CBS_PUSHED;
			else if (closeHot)
				stateId = CBS_HOT;
			else
				stateId = CBS_NORMAL;
			RECT reClose = new RECT (rClose);
			RECT reClip = reClose; // should fx.VisibleClipBounds be used here?
			IntPtr hDC = fx.GetHdc();
			DrawThemeBackground (hTheme, hDC, WP_CLOSEBUTTON, stateId, ref reClose, ref reClip);
			fx.ReleaseHdc (hDC);
			CloseThemeData (hTheme);
		}

		/// <summary>
		/// Draw a Windows 95 style close button.
		/// </summary>
		protected void drawLegacyCloseButton (Graphics fx)
		{
			ButtonState bState;
			if (closePressed)
				bState = ButtonState.Pushed;
			else // the Windows 95 theme doesn't have a "hot" button
				bState = ButtonState.Normal;
			ControlPaint.DrawCaptionButton (fx, rClose, CaptionButton.Close, bState);
		}

		/// <summary>
		/// Determine whether or not XP Visual Styles are active.  Compatible with pre-UxTheme.dll versions of Windows.
		/// </summary>
		protected bool visualStylesEnabled()
		{
			try
			{
				if (IsThemeActive() == 1)
					return true;
				else
					return false;
			}
			catch (System.DllNotFoundException)  // pre-XP systems which don't have UxTheme.dll
			{
				return false;
			}
		}
		#endregion

		#region Timers and EventHandlers
		protected void viewTimer (object sender, System.EventArgs e)
		{
			switch (ClockState)
			{
				case ClockStates.Opening:
                    if (NotifyStyle == NotifyStyle.Info)
                    {
                        if (this.Opacity >= 1)
                        {
                            ClockState = ClockStates.Showing;
                            viewClock.Interval = WaitTime;
                        }
                        else
                        {
                            this.Opacity += 0.01;
                        }
                    }
                    else
                    {
                        if (Top - 2 <= rScreen.Height - ActualHeight)
                        {
                            Top = rScreen.Height - ActualHeight;
                            Height = ActualHeight;
                            ClockState = ClockStates.Showing;
                            viewClock.Interval = WaitTime;
                        }
                        else
                        {
                            Top -= 2;
                            Height += 2;
                        }
                    }
					break;

				case ClockStates.Showing:
                    if (NotifyStyle == NotifyStyle.Dialog)
                    {
                    }
	                else if (!WaitOnMouseOver || !rGlobDisplay.Contains (Cursor.Position))
					{
						viewClock.Interval = 1;
						ClockState = ClockStates.Closing;
					}
					break;

				case ClockStates.Closing:
                    if (NotifyStyle == NotifyStyle.Info)
                    {
                        if (this.Opacity <=0)
                        {
                            ClockState = ClockStates.None;
                            viewClock.Stop();
                            viewClock.Dispose();
                            Close();
                        }
                        else
                        {
                            this.Opacity -= 0.01;
                        }
                    }
                    else
                    {
                        Top += 2;
                        Height -= 2;
                        if (Top >= rScreen.Height)
                        {
                            ClockState = ClockStates.None;
                            viewClock.Stop();
                            viewClock.Dispose();
                            Close();
                        }
                    }
					break;
			}
		}

		protected override void OnMouseMove (System.Windows.Forms.MouseEventArgs e)
		{
			if (Title != null && rGlobTitle.Contains (Cursor.Position) && !textPressed && !closePressed)
			{
				Cursor = Cursors.Hand;
				titleHot = true;
				textHot = false;  closeHot = false;
				Invalidate();
			}
			else if (rGlobText.Contains (Cursor.Position) && !titlePressed && !closePressed)
			{
				Cursor = Cursors.Hand;
				textHot = true;
				titleHot = false;  closeHot = false;
				Invalidate();
			}
			else if (rGlobClose.Contains (Cursor.Position) && !titlePressed && !textPressed)
			{
				Cursor = Cursors.Hand;
				closeHot = true;
				titleHot = false;  textHot = false;
				Invalidate();
			}
			else if ((textHot || titleHot || closeHot) && (!titlePressed && !textPressed && !closePressed))
			{
				Cursor = Cursors.Default;
				titleHot = false;  textHot = false;  closeHot = false;
				Invalidate();
			}
			base.OnMouseMove (e);
		}

		protected override void OnMouseDown (System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (rGlobClose.Contains (Cursor.Position))
				{
					closePressed = true;
					closeHot = false;
					Invalidate();
				}
				else if (rGlobText.Contains (Cursor.Position))
				{
					textPressed = true;
					Invalidate();
				}
				else if (Title != null && rGlobTitle.Contains (Cursor.Position))
				{
					titlePressed = true;
					Invalidate();
				}
			}
			base.OnMouseDown (e);
		}

		private void InitializeComponent()
		{
			// 
			// NotifyWindow
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Name = "NotifyWindow";

		}

		protected override void OnMouseUp (System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (closePressed)
				{
					Cursor = Cursors.Default;
					closePressed = false;
					closeHot = false;
					Invalidate();
                    if (rGlobClose.Contains(Cursor.Position))
                        CloseWindow();
				}
				else if (textPressed)
				{
					Cursor = Cursors.Default;
					textPressed = false;
					textHot = false;
					Invalidate();
					if (rGlobText.Contains (Cursor.Position))
					{
                        CloseWindow();
						if (TextClicked != null)
							TextClicked (this, new System.EventArgs());
					}
				}
				else if (titlePressed)
				{
					Cursor = Cursors.Default;
					titlePressed = false;
					titleHot = false;
					Invalidate();
					if (rGlobTitle.Contains (Cursor.Position))
					{
                        CloseWindow();
						if (TitleClicked != null)
							TitleClicked (this, new System.EventArgs());
					}
				}
			}
			base.OnMouseUp (e);
		}

        private void CloseWindow()
        {
            if (NotifyStyle == NotifyStyle.Dialog)
            {
                viewClock.Interval = 1;
                ClockState = ClockStates.Closing;
            }
            else 
            {
                Close();
            }
        }

		#endregion

		#region P/Invoke
		// DrawThemeBackground()
		protected const Int32 WP_CLOSEBUTTON = 18;
		protected const Int32 CBS_NORMAL = 1;
		protected const Int32 CBS_HOT = 2;
		protected const Int32 CBS_PUSHED = 3;
	
		[StructLayout (LayoutKind.Explicit)]
		protected struct RECT
		{
			[FieldOffset (0)] public Int32 Left;
			[FieldOffset (4)] public Int32 Top;
			[FieldOffset (8)] public Int32 Right;
			[FieldOffset (12)] public Int32 Bottom;

			public RECT (System.Drawing.Rectangle bounds)
			{
				Left = bounds.Left;
				Top = bounds.Top;
				Right = bounds.Right;
				Bottom = bounds.Bottom;
			}
		}

		// SetWindowPos()
		protected const Int32 HWND_TOPMOST = -1;
		protected const Int32 SWP_NOACTIVATE = 0x0010;

		// ShowWindow()
		protected const Int32 SW_SHOWNOACTIVATE = 4;

		// UxTheme.dll
		[DllImport ("UxTheme.dll")]
		protected static extern Int32 IsThemeActive();
		
		[DllImport ("UxTheme.dll")]
		protected static extern IntPtr OpenThemeData (IntPtr hWnd, [MarshalAs (UnmanagedType.LPTStr)] string classList);
		
		[DllImport ("UxTheme.dll")]
		protected static extern void CloseThemeData (IntPtr hTheme);
		
		[DllImport ("UxTheme.dll")]
		protected static extern void DrawThemeBackground (IntPtr hTheme, IntPtr hDC, Int32 partId, Int32 stateId, ref RECT rect, ref RECT clipRect);

		// user32.dll
		[DllImport ("user32.dll")]
		protected static extern bool ShowWindow (IntPtr hWnd, Int32 flags);
		
		[DllImport ("user32.dll")]
		protected static extern bool SetWindowPos (IntPtr hWnd, Int32 hWndInsertAfter, Int32 X, Int32 Y, Int32 cx, Int32 cy, uint uFlags);
		#endregion

        #region ILayout

        private ControlLayout m_ControlLayout = ControlLayout.Visual;
        protected IStyle m_StylePainter;

        [Category("Style"), DefaultValue(ControlLayout.Visual)]
        public virtual ControlLayout ControlLayout
        {
            get { return m_ControlLayout; }
            set
            {
                m_ControlLayout = value;
            }
        }

        [Browsable(false)]
        public PainterTypes PainterType
        {
            get { return PainterTypes.Flat; }
        }

        [Category("Style"), DefaultValue(null), RefreshProperties(RefreshProperties.All)]
        public IStyle StylePainter
        {
            get { return m_StylePainter; }
            set
            {
                m_StylePainter = value;
                //if (m_StylePainter != value)
                //{
                //    if (this.m_StylePainter != null)
                //        this.m_StylePainter.PropertyChanged -= new PropertyChangedEventHandler(m_Style_PropertyChanged);
                //    m_StylePainter = value;
                //    if (this.m_StylePainter != null)
                //        this.m_StylePainter.PropertyChanged += new PropertyChangedEventHandler(m_Style_PropertyChanged);
                //    OnStylePainterChanged(EventArgs.Empty);
                //    this.Invalidate();
                //}
            }
        }

        [Browsable(false)]
        public virtual IStyleLayout LayoutManager
        {
            get
            {
                if (this.m_StylePainter != null)
                    return this.m_StylePainter.Layout as IStyleLayout;
                else
                    return StyleLayout.DefaultLayout as IStyleLayout;

            }
        }


        protected virtual void SetDefaultLayout()
        {
            this.SetStyleLayout(StyleLayout.DefaultStylePlan);
        }

        public virtual void SetStyleLayout(IStyle style)
        {
            //if (style is StyleGuide)
            //{
                this.StylePainter = style;
            //}
            if (style != null)
            {
                this.SetStyleLayout(style.Layout);
            }
        }

        public virtual void SetStyleLayout(StyleLayout value)
        {
            if (this.m_StylePainter != null)
            {
                this.m_StylePainter.Layout.SetStyleLayout(value);
                Invalidate();
            }
        }

        public virtual void SetStyleLayout(Styles value)
        {
            if (this.m_StylePainter != null)
            {
                m_StylePainter.Layout.SetStyleLayout(value);
                Invalidate();
            }
        }

        protected virtual void OnStylePainterChanged(EventArgs e)
        {
            //
        }

        protected virtual void OnStylePropertyChanged(PropertyChangedEventArgs e)
        {
            if ((DesignMode || IsHandleCreated))
                this.Invalidate();
        }

        private void m_Style_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            OnStylePropertyChanged(e);
        }

        #endregion

	}
}
