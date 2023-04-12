using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using System.Security.Permissions;
using System.Drawing.Imaging;
using Nistec.WinForms;

namespace Nistec.GridView
{

	internal class GridCaption 
	{

		#region Fields
		//		private bool backActive;
		private SolidBrush backBrush;
		//		private Rectangle backButtonRect;
		//		private bool backButtonVisible;
		//		private bool backPressed;
		//		private const int buttonToText = 4;
		private static ColorMap[] colorMap;
		private Grid dataGrid;
		private Font dataGridFont;
				private bool downActive;
				private bool downButtonDown;
				private Rectangle downButtonRect;
				private bool downButtonVisible;
				private bool downPressed;
		//		private static readonly object EVENT_BACKWARDCLICKED;
		//		private static readonly object EVENT_CAPTIONCLICKED;
		//		private static readonly object EVENT_DOWNCLICKED;
		private EventEntry eventList;
		internal EventHandlerList events;
		private SolidBrush foreBrush;
		private CaptionLocation lastMouseLocation;
		//		private static Bitmap leftButtonBitmap;
		//		private static Bitmap leftButtonBitmap_bidi;
				private static Bitmap magnifyingGlassBitmap;
		private static readonly Point minimumBounds;
		private string text;
		private Pen textBorderPen;
		private bool textBorderVisible;
		private Font textFont;
		private const int textPadding = 2;
		private Rectangle textRect;
		private const int xOffset = 3;
		private const int yOffset = 1;


		// Events
		//internal event EventHandler BackwardClicked;
		internal event EventHandler CaptionClicked;
		internal event EventHandler DownClicked;

		#endregion

		#region Ctor

		static GridCaption()
		{
			GridCaption.colorMap = new ColorMap[] { new ColorMap() };
			GridCaption.minimumBounds = new Point(50, 30);
			//			GridCaption.EVENT_BACKWARDCLICKED = new object();
			//			GridCaption.EVENT_DOWNCLICKED = new object();
			//			GridCaption.EVENT_CAPTIONCLICKED = new object();
		}

		internal GridCaption(Grid dataGrid)
		{
			isTablesSet=false;
			tableCount=0;

			this.dataGrid = null;
			//			this.backButtonVisible = false;
						this.downButtonVisible = false;
			this.backBrush = GridCaption.DefaultBackBrush;
			this.foreBrush = GridCaption.DefaultForeBrush;
			this.textBorderPen = GridCaption.DefaultTextBorderPen;

			this.text = "";
			this.textBorderVisible = false;
			this.textFont = null;
			this.dataGridFont = null;
			//			this.backActive = false;
						this.downActive = false;
			//			this.backPressed = false;
						this.downPressed = false;
						this.downButtonDown = false;
			//			this.backButtonRect = new Rectangle();
						this.downButtonRect = new Rectangle();
			this.textRect = new Rectangle();
			this.lastMouseLocation = GridCaption.CaptionLocation.Nowhere;
			this.dataGrid = dataGrid;
			//this.downButtonVisible = dataGrid.ParentRowsVisible;
			this.downButtonVisible = ShouldShowDownButton();
			GridCaption.colorMap[0].OldColor = Color.White;
			GridCaption.colorMap[0].NewColor = this.ForeColor;
			this.OnGridFontChanged();
		}

		#endregion

		#region PopUp

		private bool isTablesSet;
		private int tableCount;
		private Nistec.WinForms.McPopUp ctlPopUp;

		public Nistec.WinForms.McPopUp TablesPopUp
		{
			get
			{
				if(this.ctlPopUp==null)
				{
					this.ctlPopUp=new McPopUp(this.dataGrid);
                    this.ctlPopUp.UseOwnerWidth = false;
                    this.ctlPopUp.SelectedItemClick += new SelectedPopUpItemEventHandler(ctlPopUp_SelectedItemClick);

				}
				return this.ctlPopUp;
			}
		}

        private void ctlPopUp_SelectedItemClick(object sender, SelectedPopUpItemEvent e)
		{
            object tag = e.Item.Tag;
            if (tag == null)
                return;
            if (tag.ToString() != this.dataGrid.DataMember)
            {
                this.dataGrid.forceDefaultTableStyle = true;
                this.dataGrid.DataMember = tag.ToString();
                this.Text = e.Item.Text;
                this.dataGrid.OnNavigate(new NavigateEventArgs(true));
            }
		}

		private bool  ShouldShowDownButton()
		{
			if(this.dataGrid==null)
			{
				return false;
			}
//			if(this.dataGrid.TableStyles.Count>1)
//			{
//				return true;
//			}
			if(dataGrid.DataSource is DataSet)
			{
                if (((DataSet)this.dataGrid.DataSource).Tables.Count > 1 && this.dataGrid.AllowNavigation)
				{
					return true;
				}
			}
			return false;
		}

		private void SetTableList()
		{
			if(isTablesSet)return;
			TablesPopUp.MenuItems.Clear();
			tableCount=0;

//			if(this.dataGrid.TableStyles.Count>1)
//			{
//				foreach(GridTableStyle t in this.dataGrid.TableStyles)
//				{
//					TablesPopUp.MenuItems.AddItem(t.TableName,t.MappingName,-1);
//					tableCount++;
//				}
//			}
			if(dataGrid.DataSource is DataSet)
			{
				foreach(DataTable t in ((DataSet)this.dataGrid.DataSource).Tables)
				{
					TablesPopUp.MenuItems.AddItem(t.TableName,t.TableName,-1);
					tableCount++;
				}
			}
			isTablesSet=true;
		}

		public void ShowTableList()
		{
			SetTableList();

            if (this.tableCount == 0) 
                return;

            Rectangle rect=this.dataGrid.CaptionRect();
			int listWidth=(int)TablesPopUp.CalcDropDownWidth();
			Point p=this.dataGrid.Parent.PointToScreen(new Point(this.dataGrid.Right -listWidth-(this.downButtonRect.Width/2) ,this.dataGrid.Top+rect.Top+this.downButtonRect.Height));
			if(this.dataGrid.RightToLeft==RightToLeft.Yes)
			  p=this.dataGrid.Parent.PointToScreen(new Point(this.dataGrid.Left+ rect.X+(this.downButtonRect.Width/2),this.dataGrid.Top+rect.Top+this.downButtonRect.Height));
		
			TablesPopUp.ShowPopUp(p);
		}

		private void PaintDownButton(Graphics g, Rectangle bounds)
		{
			Bitmap bitmap1 = this.GetDetailsBmp();
			lock (bitmap1)
			{
				this.PaintIcon(g, bounds, bitmap1);
			}
		}


		#endregion

		#region Methods

 
		protected virtual void AddEventHandler(object key, Delegate handler)
		{
			lock (this)
			{
				if (handler != null)
				{
					for (GridCaption.EventEntry entry1 = this.eventList; entry1 != null; entry1 = entry1.next)
					{
						if (entry1.key == key)
						{
							entry1.handler = Delegate.Combine(entry1.handler, handler);
							return;
						}
					}
					this.eventList = new GridCaption.EventEntry(this.eventList, key, handler);
				}
			}
		}

 
		private GridCaption.CaptionLocation FindLocation(int x, int y)
		{
//			if (!this.backButtonRect.IsEmpty && this.backButtonRect.Contains(x, y))
//			{
//				return GridCaption.CaptionLocation.BackButton;
//			}
			if (!this.downButtonRect.IsEmpty && this.downButtonRect.Contains(x, y))
			{
				return GridCaption.CaptionLocation.DownButton;
			}
			if (!this.textRect.IsEmpty && this.textRect.Contains(x, y))
			{
				return GridCaption.CaptionLocation.Text;
			}
			return GridCaption.CaptionLocation.Nowhere;
		}


		private Bitmap GetBitmap(string bitmapName)
		{
			Bitmap bitmap1 = null;
			try
			{
				bitmap1 = new Bitmap(typeof(GridCaption),"Images." + bitmapName);
				bitmap1.MakeTransparent();
			}
			catch (Exception)
			{
			}
			return bitmap1;
		}

 
		private Bitmap GetDetailsBmp()
		{
			if (GridCaption.magnifyingGlassBitmap == null)
			{
				GridCaption.magnifyingGlassBitmap = this.GetBitmap("GridCaption.Details.bmp");
			}
			return GridCaption.magnifyingGlassBitmap;
		}

		internal Rectangle GetDetailsButtonRect(Rectangle bounds, bool alignRight)
		{
			Size size1;
			Bitmap bitmap1 = this.GetDetailsBmp();
			lock (bitmap1)
			{
				size1 = bitmap1.Size;
			}
			int num1 = size1.Width;
			return new Rectangle((bounds.Right - 6) - num1, (bounds.Y + 1) + 2, num1, size1.Height);
		}

		internal int GetDetailsButtonWidth()
		{
			Bitmap bitmap1 = this.GetDetailsBmp();
			lock (bitmap1)
			{
				return bitmap1.Size.Width;
			}
		}
 
		internal bool GetDownButtonDirection()
		{
			return this.DownButtonDown;
		}

		protected virtual Delegate GetEventHandler(object key)
		{
			lock (this)
			{
				for (GridCaption.EventEntry entry1 = this.eventList; entry1 != null; entry1 = entry1.next)
				{
					if (entry1.key == key)
					{
						return entry1.handler;
					}
				}
				return null;
			}
		}

		private void Invalidate()
		{
			if (this.dataGrid != null)
			{
				this.dataGrid.InvalidateCaption();
			}
		}

 
		private void InvalidateCaptionRect(Rectangle r)
		{
			if (this.dataGrid != null)
			{
				this.dataGrid.InvalidateCaptionRect(r);
			}
		}

		private void InvalidateLocation(GridCaption.CaptionLocation loc)
		{
			Rectangle rectangle1;
			switch (loc)
			{
				case GridCaption.CaptionLocation.BackButton:
					//rectangle1 = this.backButtonRect;
					//rectangle1.Inflate(1, 1);
					//this.InvalidateCaptionRect(rectangle1);
					return;

				case GridCaption.CaptionLocation.DownButton:
					rectangle1 = this.downButtonRect;
					rectangle1.Inflate(1, 1);
					this.InvalidateCaptionRect(rectangle1);
					return;
			}
		}

		internal void MouseDown(int x, int y)
		{
			GridCaption.CaptionLocation location1 = this.FindLocation(x, y);
			switch (location1)
			{
				case GridCaption.CaptionLocation.BackButton:
//					this.backPressed = true;
//					this.InvalidateLocation(location1);
					return;

				case GridCaption.CaptionLocation.DownButton:
					this.downPressed = true;
					this.InvalidateLocation(location1);
					return;

				case GridCaption.CaptionLocation.Text:
					this.OnCaptionClicked(EventArgs.Empty);
					return;
			}
		}

		internal void MouseLeft()
		{
			GridCaption.CaptionLocation location1 = this.lastMouseLocation;
			this.lastMouseLocation = GridCaption.CaptionLocation.Nowhere;
			this.InvalidateLocation(location1);
		}

		internal void MouseOver(int x, int y)
		{
			GridCaption.CaptionLocation location1 = this.FindLocation(x, y);
			this.InvalidateLocation(this.lastMouseLocation);
			this.InvalidateLocation(location1);
			this.lastMouseLocation = location1;
		}
 
		internal void MouseUp(int x, int y)
		{
			switch (this.FindLocation(x, y))
			{
				case GridCaption.CaptionLocation.BackButton:
//					if (this.backPressed)
//					{
//						this.backPressed = false;
//						this.OnBackwardClicked(EventArgs.Empty);
//					}
					return;

				case GridCaption.CaptionLocation.DownButton:
					if (this.downPressed)
					{
						this.downPressed = false;
						this.OnDownClicked(EventArgs.Empty);
						return;
					}
					return;
			}
		}

 

		protected void OnCaptionClicked(EventArgs e)
		{
			if(this.CaptionClicked!=null)
				this.CaptionClicked(this,e);

//			EventHandler handler1 = (EventHandler) this.Events[GridCaption.EVENT_CAPTIONCLICKED];
//			if (handler1 != null)
//			{
//				handler1(this, e);
//			}
		}

		protected void OnDownClicked(EventArgs e)
		{
			if (this.downButtonVisible)//this.downActive && 
			{
				this.ShowTableList();
				if(this.DownClicked!=null)
					this.DownClicked(this,e);
//				EventHandler handler1 = (EventHandler) this.Events[GridCaption.EVENT_DOWNCLICKED];
//				if (handler1 != null)
//				{
//					handler1(this, e);
//				}
			}
		}
 
		internal void OnGridFontChanged()
		{
			if ((this.dataGridFont == null) || !this.dataGridFont.Equals(this.dataGrid.Font))
			{
				try
				{
					this.dataGridFont = new Font(this.dataGrid.Font, FontStyle.Bold);
				}
				catch (Exception)
				{
				}
			}
		}
 
		internal void Paint(Graphics g, Rectangle bounds, bool alignRight)
		{
			SizeF ef1 = g.MeasureString(this.text, this.Font);
			Size size1 = new Size(((int) ef1.Width) + 2, this.Font.Height + 2);
			this.downButtonRect = this.GetDetailsButtonRect(bounds, alignRight);
			int num1 =this.GetDetailsButtonWidth();
			//this.backButtonRect = this.GetBackButtonRect(bounds, alignRight, num1);
			int num2 =0 ;//this.backButtonVisible ? ((this.backButtonRect.Width + 3) + 4) : 0;
			int num3 =num1+ 4;// (this.downButtonVisible && !this.dataGrid.ParentRowsIsEmpty()) ? ((num1 + 3) + 4) : 0;
			int num4 = ((bounds.Width - 3) - num2) - num3;
			this.textRect = new Rectangle(bounds.X, bounds.Y + 1, Math.Min(num4, 4 + size1.Width), 4 + size1.Height);
			if (alignRight)
			{
				this.textRect.X = bounds.Right - this.textRect.Width;
//				this.backButtonRect.X = (bounds.X + 12) + num1;
				this.downButtonRect.X = bounds.X + 6;
			}
            /*ControlLayout*/
            switch (this.dataGrid.ControlLayout)
            {
                case ControlLayout.System:
                case ControlLayout.Visual:
                    using (Brush br = this.dataGrid.LayoutManager.GetBrushCaptionGradient(bounds, 270f, false))
                    {
                        g.FillRectangle(br, bounds);
                    }
                    break;
                case ControlLayout.VistaLayout:
                case ControlLayout.XpLayout:
                    g.FillRectangle(this.dataGrid.GetHeaderBackBrush(false,bounds), bounds);
                    break;
                case ControlLayout.Flat:
                    using (Brush br = this.dataGrid.LayoutManager.GetBrushCaption())
                    {
                        g.FillRectangle(br, bounds);
                    }
                    break;
                 default:
                    using (Brush br = this.dataGrid.LayoutManager.GetBrushCaption())
                    {
                        g.FillRectangle(br, bounds);
                    }
                    break;
            }

             //g.FillRectangle(this.dataGrid.GetHeaderBackBrush(bounds), bounds);

            //using (Brush br = this.dataGrid.GetHeaderBackBrush(bounds))
            //{
            //    g.FillRectangle(br, bounds);
            //}

            //using (Brush br = this.dataGrid.LayoutManager.GetBrushCaptionGradient(bounds, 270f, false))
            //{
            //    g.FillRectangle(br, bounds);
            //}

			//g.FillRectangle(this.backBrush, bounds);

//			if (this.backButtonVisible)
//			{
//				this.PaintBackButton(g, this.backButtonRect, alignRight);
//				if (this.backActive && (this.lastMouseLocation == GridCaption.CaptionLocation.BackButton))
//				{
//					this.backButtonRect.Inflate(1, 1);
//					ControlPaint.DrawBorder3D(g, this.backButtonRect, this.backPressed ? Border3DStyle.SunkenInner : Border3DStyle.RaisedInner);
//				}
//			}

			//this.downButtonVisible=true;
            if (downButtonVisible)
            {
                this.PaintDownButton(g, downButtonRect);//this.ImageRect);
            }

			//this.textRect.X+=20;
			this.PaintText(g, this.textRect, alignRight);
//			if (this.downButtonVisible && !this.dataGrid.ParentRowsIsEmpty())
//			{
//				this.PaintDownButton(g, this.downButtonRect);
//				if (this.lastMouseLocation == GridCaption.CaptionLocation.DownButton)
//				{
//					this.downButtonRect.Inflate(1, 1);
//					//ControlPaint.DrawBorder3D(g, this.downButtonRect, this.downPressed ? Border3DStyle.SunkenInner : Border3DStyle.RaisedInner);
//				}
//			}
		}

		private void PaintIcon(Graphics g, Rectangle bounds, Bitmap b)
		{
			ImageAttributes attributes1 = new ImageAttributes();
			attributes1.SetRemapTable(GridCaption.colorMap, ColorAdjustType.Bitmap);
			g.DrawImage(b, bounds, 0, 0, bounds.Width, bounds.Height, GraphicsUnit.Pixel, attributes1);
			attributes1.Dispose();
		}

 
		private void PaintText(Graphics g, Rectangle bounds, bool alignToRight)
		{
			Rectangle rectangle1 = bounds;
			if ((rectangle1.Width > 0) && (rectangle1.Height > 0))
			{
				if (this.textBorderVisible)
				{
					g.DrawRectangle(this.textBorderPen, rectangle1.X, rectangle1.Y, rectangle1.Width - 1, rectangle1.Height - 1);
					rectangle1.Inflate(-1, -1);
				}
//				Rectangle rectangle2 = rectangle1;
//				rectangle2.Height = 2;
//				g.FillRectangle(this.backBrush, rectangle2);
//				rectangle2.Y = rectangle1.Bottom - 2;
//				g.FillRectangle(this.backBrush, rectangle2);
//				rectangle2 = new Rectangle(rectangle1.X, rectangle1.Y + 2, 2, rectangle1.Height - 4);
//				g.FillRectangle(this.backBrush, rectangle2);
//				rectangle2.X = rectangle1.Right - 2;
//				g.FillRectangle(this.backBrush, rectangle2);
//				rectangle1.Inflate(-2, -2);
//				g.FillRectangle(this.backBrush, rectangle1);
				
				StringFormat format1 = new StringFormat();
				if (alignToRight)
				{
					format1.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
					format1.Alignment = StringAlignment.Far;
				}

                /*ControlLayout*/
                switch (this.dataGrid.ControlLayout)
                {
                    case ControlLayout.XpLayout:
                        g.DrawString(this.text, this.Font, this.dataGrid.LayoutManager.GetBrushCaption(), (RectangleF)rectangle1, format1);
                        break;
                    default:
                        g.DrawString(this.text, this.Font, this.dataGrid.LayoutManager.GetBrushCaptionText(), (RectangleF)rectangle1, format1);
                        break;
                }

				//g.DrawString(this.text, this.Font,this.dataGrid.LayoutManager.GetBrushCaptionText(), (RectangleF) rectangle1, format1);
				
                //g.DrawString(this.text, this.Font, this.foreBrush, (RectangleF) rectangle1, format1);
				format1.Dispose();
			}
		}

		protected virtual void RaiseEvent(object key, EventArgs e)
		{
			Delegate delegate1 = this.GetEventHandler(key);
			if (delegate1 != null)
			{
				((EventHandler) delegate1)(this, e);
			}
		}

 
		protected virtual void RemoveEventHandler(object key, Delegate handler)
		{
			lock (this)
			{
				if (handler != null)
				{
					GridCaption.EventEntry entry1 = this.eventList;
					GridCaption.EventEntry entry2 = null;
					while (entry1 != null)
					{
						if (entry1.key == key)
						{
							entry1.handler = Delegate.Remove(entry1.handler, handler);
							if (entry1.handler == null)
							{
								if (entry2 == null)
								{
									this.eventList = entry1.next;
									return;
								}
								entry2.next = entry1.next;
							}
							return;
						}
						entry2 = entry1;
						entry1 = entry1.next;
					}
				}
			}
		}

		protected virtual void RemoveEventHandlers()
		{
			this.eventList = null;
		}

		internal void ResetBackColor()
		{
			if (this.ShouldSerializeBackColor())
			{
				this.backBrush = GridCaption.DefaultBackBrush;
				this.Invalidate();
			}
		}

		internal void ResetFont()
		{
			this.textFont = null;
			this.Invalidate();
		}

 
		internal void ResetForeColor()
		{
			if (this.ShouldSerializeForeColor())
			{
				this.foreBrush = GridCaption.DefaultForeBrush;
				this.Invalidate();
			}
		}

		internal void SetDownButtonDirection(bool pointDown)
		{
			this.DownButtonDown = pointDown;
			this.isTablesSet=false;
		}

 
		internal bool ShouldSerializeBackColor()
		{
			return !this.backBrush.Equals(GridCaption.DefaultBackBrush);
		}

 
		internal bool ShouldSerializeFont()
		{
			if (this.textFont != null)
			{
				return !this.textFont.Equals(this.dataGridFont);
			}
			return false;
		}

		internal bool ShouldSerializeForeColor()
		{
			return !this.foreBrush.Equals(GridCaption.DefaultForeBrush);
		}

 
		internal bool ToggleDownButtonDirection()
		{
			this.DownButtonDown = !this.DownButtonDown;
			return this.DownButtonDown;
		}

 
 

		#endregion

		#region Properties
		internal Color BackColor
		{
			get
			{
				return this.backBrush.Color;
			}
			set
			{
				if (!this.backBrush.Color.Equals(value))
				{
					if (value.IsEmpty)
					{
						throw new ArgumentException("GridEmptyColor",  "Caption BackColor" );
					}
					this.backBrush = new SolidBrush(value);
					this.Invalidate();
				}
			}
		}
 
		internal static SolidBrush DefaultBackBrush
		{
			get
			{
				return (SolidBrush) SystemBrushes.ActiveCaption;
			}
		}
 
		internal static SolidBrush DefaultForeBrush
		{
			get
			{
				return (SolidBrush) SystemBrushes.ActiveCaptionText;
			}
		}
 
		internal static Pen DefaultTextBorderPen
		{
			get
			{
				return new Pen(SystemColors.ActiveCaptionText);
			}
		}
 
		internal bool DownButtonActive
		{
			get
			{
				return this.downActive;
			}
			set
			{
				if (this.downActive != value)
				{
					this.downActive = value;
					this.InvalidateCaptionRect(this.downButtonRect);
				}
			}
		}
 
		private bool DownButtonDown
		{
			get
			{
				return this.downButtonDown;
			}
			set
			{
				if (this.downButtonDown != value)
				{
					this.downButtonDown = value;
					this.InvalidateLocation(GridCaption.CaptionLocation.DownButton);
				}
			}
		}
 
		internal bool DownButtonVisible
		{
			get
			{
				return this.downButtonVisible;
			}
			set
			{
				if (this.downButtonVisible != value)
				{
					this.downButtonVisible = value;
					this.Invalidate();
				}
			}
		}
		internal EventHandlerList Events
		{
			get
			{
				if (this.events == null)
				{
					this.events = new EventHandlerList();
				}
				return this.events;
			}
		}
 
		internal Font Font
		{
			get
			{
				if (this.textFont == null)
				{
					return this.dataGridFont;
				}
				return this.textFont;
			}
			set
			{
				if ((this.textFont == null) || !this.textFont.Equals(value))
				{
					this.textFont = value;
					if (this.dataGrid.Caption != null)
					{
						this.dataGrid.RecalculateFonts();
						this.dataGrid.PerformLayout();
						this.dataGrid.Invalidate();
					}
				}
			}
		}
 
		internal Color ForeColor
		{
			get
			{
				return this.foreBrush.Color;
			}
			set
			{
				if (value.IsEmpty)
				{
					throw new ArgumentException("GridEmptyColor", "Caption ForeColor");
				}
				this.foreBrush = new SolidBrush(value);
				GridCaption.colorMap[0].NewColor = this.ForeColor;
				this.Invalidate();
			}
		}
		internal Point MinimumBounds
		{
			get
			{
				return GridCaption.minimumBounds;
			}
		}
 
		internal string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				if (value == null)
				{
					this.text = "";
				}
				else
				{
					this.text = value;
				}
				this.Invalidate();
			}
		}
 
		internal bool TextBorderVisible
		{
			get
			{
				return this.textBorderVisible;
			}
			set
			{
				this.textBorderVisible = value;
				this.Invalidate();
			}
		}
 

		#endregion

		#region base

		//		protected void OnBackwardClicked(EventArgs e)
		//		{
		//			if (this.backActive)
		//			{
		//				if(this.BackwardClicked!=null)
		//					this.BackwardClicked(this,e);
		//
		//				EventHandler handler1 = (EventHandler) this.Events[GridCaption.EVENT_BACKWARDCLICKED];
		//				if (handler1 != null)
		//				{
		//					handler1(this, e);
		//				}
		//			}
		//		}

		//		private Bitmap GetBackButtonBmp(bool alignRight)
		//		{
		//			if (alignRight)
		//			{
		//				if (GridCaption.leftButtonBitmap_bidi == null)
		//				{
		//					GridCaption.leftButtonBitmap_bidi = this.GetBitmap("GridCaption.backarrow_bidi.bmp");
		//				}
		//				return GridCaption.leftButtonBitmap_bidi;
		//			}
		//			if (GridCaption.leftButtonBitmap == null)
		//			{
		//				GridCaption.leftButtonBitmap = this.GetBitmap("GridCaption.backarrow.bmp");
		//			}
		//			return GridCaption.leftButtonBitmap;
		//		}

 
		//		internal Rectangle GetBackButtonRect(Rectangle bounds, bool alignRight, int downButtonWidth)
		//		{
		//			Size size1;
		//			Bitmap bitmap1 = this.GetBackButtonBmp(false);
		//			lock (bitmap1)
		//			{
		//				size1 = bitmap1.Size;
		//			}
		//			return new Rectangle(((bounds.Right - 12) - downButtonWidth) - size1.Width, (bounds.Y + 1) + 2, size1.Width, size1.Height);
		//		}
		 
		//		private void PaintBackButton(Graphics g, Rectangle bounds, bool alignRight)
		//		{
		//			Bitmap bitmap1 = this.GetBackButtonBmp(alignRight);
		//			lock (bitmap1)
		//			{
		//				this.PaintIcon(g, bounds, bitmap1);
		//			}
		//		}

		//		private void PaintDownButton(Graphics g, Rectangle bounds)
		//		{
		//			Bitmap bitmap1 = this.GetDetailsBmp();
		//			lock (bitmap1)
		//			{
		//				this.PaintIcon(g, bounds, bitmap1);
		//			}
		//		}

		//		internal bool BackButtonActive
		//		{
		//			get
		//			{
		//				return this.backActive;
		//			}
		//			set
		//			{
		//				if (this.backActive != value)
		//				{
		//					this.backActive = value;
		//					this.InvalidateCaptionRect(this.backButtonRect);
		//				}
		//			}
		//		}
		//		internal bool BackButtonVisible
		//		{
		//			get
		//			{
		//				return this.backButtonVisible;
		//			}
		//			set
		//			{
		//				if (this.backButtonVisible != value)
		//				{
		//					this.backButtonVisible = value;
		//					this.Invalidate();
		//				}
		//			}
		//		}

		//		internal event EventHandler BackwardClicked
		//		{
		//			add
		//			{
		//				this.Events.AddHandler(GridCaption.EVENT_BACKWARDCLICKED, value);
		//			}
		//			remove
		//			{
		//				this.Events.RemoveHandler(GridCaption.EVENT_BACKWARDCLICKED, value);
		//			}
		//		}
		// 
		//		internal event EventHandler CaptionClicked
		//		{
		//			add
		//			{
		//				this.Events.AddHandler(GridCaption.EVENT_CAPTIONCLICKED, value);
		//			}
		//			remove
		//			{
		//				this.Events.RemoveHandler(GridCaption.EVENT_CAPTIONCLICKED, value);
		//			}
		//		}
		//		internal event EventHandler DownClicked
		//		{
		//			add
		//			{
		//				this.Events.AddHandler(GridCaption.EVENT_DOWNCLICKED, value);
		//			}
		//			remove
		//			{
		//				this.Events.RemoveHandler(GridCaption.EVENT_DOWNCLICKED, value);
		//			}
		//		}

		#endregion

		#region Nested Types

		internal enum CaptionLocation
		{
			Nowhere,
			BackButton,
			DownButton,
			Text
		}

		private sealed class EventEntry
		{
			// Methods
			internal EventEntry(GridCaption.EventEntry next, object key, Delegate handler)
			{
				this.next = next;
				this.key = key;
				this.handler = handler;
			}

 

			// Fields
			internal Delegate handler;
			internal object key;
			internal GridCaption.EventEntry next;
		}

		#endregion

	}

}