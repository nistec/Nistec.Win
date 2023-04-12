using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Globalization;
using System.Reflection;
using System.IO;
using System.Resources;


using MControl.Win32; 

namespace MControl.Drawing
{

	#region Enums

    public enum ArrowGlyph
    {
        Up,
        Down,
        Left,
        Right
    }

	public enum Gradient3DBorderStyle
	{
		Raised = 1,
		Sunken = 2
	}

	public enum CommandState
	{
		Normal,
		HotTrack,
		Pushed
	}

	#endregion

	#region RoundedRectangle
	/// <summary>
	/// Rapresents a rounded rectangle, takes a rectangle and a round value from 0 to 1. Can be converted to a GraphicsPath for drawing operations.
	/// See also MControl.Drawing.Utilities.FillRoundedRectangle and DrawRoundedRectangle methods.
	/// </summary>
	public struct RoundedRectangle
	{
		/// <summary>
		/// Costructor
		/// </summary>
		/// <param name="rect"></param>
		/// <param name="roundValue">The amount to round the rectangle. Can be any vavlues from 0 to 1. Set to 0 to draw a standard rectangle, 1 to have a full rounded rectangle.</param>
		public RoundedRectangle(Rectangle rect, double roundValue)
		{
			mRectangle = rect;
			mRoundValue = roundValue;
		}

		private Rectangle mRectangle;
		public Rectangle Rectangle
		{
			get{return mRectangle;}
			set{mRectangle = value;}
		}

		private double mRoundValue;

		/// <summary>
		/// The amount to round the rectangle. Can be any vavlues from 0 to 1. Set to 0 to draw a standard rectangle, 1 to have a full rounded rectangle.
		/// </summary>
		public double RoundValue
		{
			get{return mRoundValue;}
			set
			{
				if (mRoundValue < 0 || mRoundValue > 1)
					throw new ApplicationException("Invalid value, must be a value from 0 to 1");
				mRoundValue = value;
			}
		}

		/// <summary>
		/// Converts this structure to a GraphicsPath object, used to draw to a Graphics device.
		/// Consider that you can create a Region with a GraphicsPath object using one of the Region constructor.
		/// </summary>
		/// <returns></returns>
		public System.Drawing.Drawing2D.GraphicsPath ToGraphicsPath()
		{
			if (mRectangle.IsEmpty)
				return new System.Drawing.Drawing2D.GraphicsPath();

			int roundedStart = 0;
			int roundDiametro = 0;
			if (mRectangle.Height < mRectangle.Width)
			{
				roundedStart = (int)((double)mRectangle.Height * mRoundValue);
				roundDiametro = roundedStart * 2;

			}
			else
			{
				roundedStart = (int)((double)mRectangle.Width * mRoundValue);
				roundDiametro = roundedStart * 2;
			}

			System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();

			//Top
			path.AddLine(roundedStart, 0, mRectangle.Width - roundedStart - 1, 0);
			//Angle Top Right
			path.AddArc(mRectangle.Width - roundDiametro - 1, 0,
				roundDiametro, roundDiametro, 
				270, 90);
			//Right
			path.AddLine(mRectangle.Width - 1, roundedStart, mRectangle.Width - 1,  mRectangle.Height - roundedStart - 1);
			//Angle Bottom Right
			path.AddArc(mRectangle.Width - roundDiametro - 1, mRectangle.Height - roundDiametro - 1,
				roundDiametro, roundDiametro, 
				0, 90);
			//Bottom
			path.AddLine(mRectangle.Width - roundedStart - 1, mRectangle.Height - 1, roundedStart, mRectangle.Height - 1);
			//Angle Bottom Left
			path.AddArc(0, mRectangle.Height - roundDiametro - 1,
				roundDiametro, roundDiametro, 
				90, 90);
			//Left
			path.AddLine(0, mRectangle.Height - roundedStart - 1, 0, roundedStart);
			//Angle Top Left
			path.AddArc(0, 0,
				roundDiametro, roundDiametro, 
				180, 90);

			return path;
		}
	}

	#endregion


	/// <summary>
	/// Drawing utility functions
	/// </summary>
	public class DrawUtils
	{
		private DrawUtils()
		{
		}

   
		#region Draw Controls

		//[CLSCompliantAttribute(false)]
		protected static IntPtr _halfToneBrush = IntPtr.Zero;

		public static void DrawReverseString(Graphics g, 
			String drawText, 
			Font drawFont, 
			Rectangle drawRect,
			Brush drawBrush,
			StringFormat drawFormat)
		{
			GraphicsContainer container = g.BeginContainer();

			// The text will be rotated around the origin (0,0) and so needs moving
			// back into position by using a transform
			g.TranslateTransform(drawRect.Left * 2 + drawRect.Width, 
				drawRect.Top * 2 + drawRect.Height);

			// Rotate the text by 180 degress to reverse the direction 
			g.RotateTransform(180);

			// Draw the string as normal and let then transforms do the work
			g.DrawString(drawText, drawFont, drawBrush, drawRect, drawFormat);

			g.EndContainer(container);
		}

		public static void DrawPlainRaised(Graphics g,
			Rectangle boxRect,
			Color baseColor)
		{
			using(Pen lighlight = new Pen(ControlPaint.LightLight(baseColor)),
					  dark = new Pen(ControlPaint.DarkDark(baseColor)))
			{                                            
				g.DrawLine(lighlight, boxRect.Left, boxRect.Bottom, boxRect.Left, boxRect.Top);
				g.DrawLine(lighlight, boxRect.Left, boxRect.Top, boxRect.Right, boxRect.Top);
				g.DrawLine(dark, boxRect.Right, boxRect.Top, boxRect.Right, boxRect.Bottom);
				g.DrawLine(dark, boxRect.Right, boxRect.Bottom, boxRect.Left, boxRect.Bottom);
			}
		}

		public static void DrawPlainSunken(Graphics g,
			Rectangle boxRect,
			Color baseColor)
		{
			using(Pen lighlight = new Pen(ControlPaint.LightLight(baseColor)),
					  dark = new Pen(ControlPaint.DarkDark(baseColor)))
			{                                            
				g.DrawLine(dark, boxRect.Left, boxRect.Bottom, boxRect.Left, boxRect.Top);
				g.DrawLine(dark, boxRect.Left, boxRect.Top, boxRect.Right, boxRect.Top);
				g.DrawLine(lighlight, boxRect.Right, boxRect.Top, boxRect.Right, boxRect.Bottom);
				g.DrawLine(lighlight, boxRect.Right, boxRect.Bottom, boxRect.Left, boxRect.Bottom);
			}
		}

//		public static void DrawPlainRaisedBorder(Graphics g, 
//			Rectangle rect,
//			Color lightLight, 
//			Color baseColor,
//			Color dark, 
//			Color darkDark)
//		{
//			if ((rect.Width > 2) && (rect.Height > 2))
//			{
//				using(Pen ll = new Pen(lightLight),
//						  b = new Pen(baseColor),
//						  d = new Pen(dark),
//						  dd = new Pen(darkDark))
//				{
//					int left = rect.Left;
//					int top = rect.Top;
//					int right = rect.Right;
//					int bottom = rect.Bottom;
//
//					// Draw the top border
//					g.DrawLine(b, right-1, top, left, top);
//					g.DrawLine(ll, right-2, top+1, left+1, top+1);
//					g.DrawLine(b, right-3, top+2, left+2, top+2);
//
//					// Draw the left border
//					g.DrawLine(b, left, top, left, bottom-1);
//					g.DrawLine(ll, left+1, top+1, left+1, bottom-2);
//					g.DrawLine(b, left+2, top+2, left+2, bottom-3);
//					
//					// Draw the right
//					g.DrawLine(dd, right-1, top+1, right-1, bottom-1);
//					g.DrawLine(d, right-2, top+2, right-2, bottom-2);
//					g.DrawLine(b, right-3, top+3, right-3, bottom-3);
//
//					// Draw the bottom
//					g.DrawLine(dd, right-1, bottom-1, left, bottom-1);
//					g.DrawLine(d, right-2, bottom-2, left+1, bottom-2);
//					g.DrawLine(b, right-3, bottom-3, left+2, bottom-3);
//				}
//			}
//		}
//
//		public static void DrawPlainRaisedBorderTopOrBottom(Graphics g, 
//			Rectangle rect,
//			Color lightLight, 
//			Color baseColor,
//			Color dark, 
//			Color darkDark,
//			bool drawTop)
//		{
//			if ((rect.Width > 2) && (rect.Height > 2))
//			{
//				using(Pen ll = new Pen(lightLight),
//						  b = new Pen(baseColor),
//						  d = new Pen(dark),
//						  dd = new Pen(darkDark))
//				{
//					int left = rect.Left;
//					int top = rect.Top;
//					int right = rect.Right;
//					int bottom = rect.Bottom;
//
//					if (drawTop)
//					{
//						// Draw the top border
//						g.DrawLine(b, right-1, top, left, top);
//						g.DrawLine(ll, right-1, top+1, left, top+1);
//						g.DrawLine(b, right-1, top+2, left, top+2);
//					}
//					else
//					{
//						// Draw the bottom
//						g.DrawLine(dd, right-1, bottom-1, left, bottom-1);
//						g.DrawLine(d, right-1, bottom-2, left, bottom-2);
//						g.DrawLine(b, right-1, bottom-3, left, bottom-3);
//					}
//				}
//			}
//		}
//
//		public static void DrawPlainSunkenBorder(Graphics g, 
//			Rectangle rect,
//			Color lightLight, 
//			Color baseColor,
//			Color dark, 
//			Color darkDark)
//		{
//			if ((rect.Width > 2) && (rect.Height > 2))
//			{
//				using(Pen ll = new Pen(lightLight),
//						  b = new Pen(baseColor),
//						  d = new Pen(dark),
//						  dd = new Pen(darkDark))
//				{
//					int left = rect.Left;
//					int top = rect.Top;
//					int right = rect.Right;
//					int bottom = rect.Bottom;
//
//					// Draw the top border
//					g.DrawLine(d, right-1, top, left, top);
//					g.DrawLine(dd, right-2, top+1, left+1, top+1);
//					g.DrawLine(b, right-3, top+2, left+2, top+2);
//
//					// Draw the left border
//					g.DrawLine(d, left, top, left, bottom-1);
//					g.DrawLine(dd, left+1, top+1, left+1, bottom-2);
//					g.DrawLine(b, left+2, top+2, left+2, bottom-3);
//					
//					// Draw the right
//					g.DrawLine(ll, right-1, top+1, right-1, bottom-1);
//					g.DrawLine(b, right-2, top+2, right-2, bottom-2);
//					g.DrawLine(b, right-3, top+3, right-3, bottom-3);
//
//					// Draw the bottom
//					g.DrawLine(ll, right-1, bottom-1, left, bottom-1);
//					g.DrawLine(b, right-2, bottom-2, left+1, bottom-2);
//					g.DrawLine(b, right-3, bottom-3, left+2, bottom-3);
//				}
//			}
//		}
//
//		public static void DrawPlainSunkenBorderTopOrBottom(Graphics g, 
//			Rectangle rect,
//			Color lightLight, 
//			Color baseColor,
//			Color dark, 
//			Color darkDark,
//			bool drawTop)
//		{
//			if ((rect.Width > 2) && (rect.Height > 2))
//			{
//				using(Pen ll = new Pen(lightLight),
//						  b = new Pen(baseColor),
//						  d = new Pen(dark),
//						  dd = new Pen(darkDark))
//				{
//					int left = rect.Left;
//					int top = rect.Top;
//					int right = rect.Right;
//					int bottom = rect.Bottom;
//
//					if (drawTop)
//					{
//						// Draw the top border
//						g.DrawLine(d, right-1, top, left, top);
//						g.DrawLine(dd, right-1, top+1, left, top+1);
//						g.DrawLine(b, right-1, top+2, left, top+2);
//					}
//					else
//					{
//						// Draw the bottom
//						g.DrawLine(ll, right-1, bottom-1, left, bottom-1);
//						g.DrawLine(b, right-1, bottom-2, left, bottom-2);
//						g.DrawLine(b, right-1, bottom-3, left, bottom-3);
//					}
//				}
//			}
//		}
//        
//		public static void DrawButtonCommand(Graphics g, 
//			VisualStyle style, 
//			Direction direction, 
//			Rectangle drawRect,
//			CommandState state,
//			Color baseColor,
//			Color trackLight,
//			Color trackBorder)
//		{
//			Rectangle rect = new Rectangle(drawRect.Left, drawRect.Top, drawRect.Width - 1, drawRect.Height - 1);
//        
//			// Draw background according to style
//			switch(style)
//			{
//				case VisualStyle.Plain:
//					// Draw background with back color
//					using(SolidBrush backBrush = new SolidBrush(baseColor))
//						g.FillRectangle(backBrush, rect);
//
//					// Modify according to state
//				switch(state)
//				{
//					case CommandState.HotTrack:
//						DrawPlainRaised(g, rect, baseColor);
//						break;
//					case CommandState.Pushed:
//						DrawPlainSunken(g, rect, baseColor);
//						break;
//				}
//					break;
//				case VisualStyle.IDE:
//					// Draw according to state
//				switch(state)
//				{
//					case CommandState.Normal:
//						// Draw background with back color
//						using(SolidBrush backBrush = new SolidBrush(baseColor))
//							g.FillRectangle(backBrush, rect);
//						break;
//					case CommandState.HotTrack:
//						g.FillRectangle(Brushes.White, rect);
//
//						using(SolidBrush trackBrush = new SolidBrush(trackLight))
//							g.FillRectangle(trackBrush, rect);
//                            
//						using(Pen trackPen = new Pen(trackBorder))
//							g.DrawRectangle(trackPen, rect);
//						break;
//					case CommandState.Pushed:
//						//TODO: draw in a darker background color
//						break;
//				}
//					break;
//			}
//		}
//        
//		public static void DrawSeparatorCommand(Graphics g, 
//			VisualStyle style, 
//			Direction direction, 
//			Rectangle drawRect,
//			Color baseColor)
//		{
//			// Drawing depends on the visual style required
//			if (style == VisualStyle.IDE)
//			{
//				// Draw a single separating line
//				using(Pen dPen = new Pen(ControlPaint.Dark(baseColor)))
//				{            
//					if (direction == Direction.Horizontal)
//						g.DrawLine(dPen, drawRect.Left, drawRect.Top,
//							drawRect.Left, drawRect.Bottom - 1);
//					else
//						g.DrawLine(dPen, drawRect.Left, drawRect.Top,
//							drawRect.Right - 1, drawRect.Top);                    
//				}
//			}
//			else
//			{
//				// Draw a dark/light combination of lines to give an indent
//				using(Pen lPen = new Pen(ControlPaint.Dark(baseColor)),
//						  llPen = new Pen(ControlPaint.LightLight(baseColor)))
//				{							
//					if (direction == Direction.Horizontal)
//					{
//						g.DrawLine(lPen, drawRect.Left, drawRect.Top, drawRect.Left, drawRect.Bottom - 1);
//						g.DrawLine(llPen, drawRect.Left + 1, drawRect.Top, drawRect.Left + 1, drawRect.Bottom - 1);
//					}
//					else
//					{
//						g.DrawLine(lPen, drawRect.Left, drawRect.Top, drawRect.Right - 1, drawRect.Top);                    
//						g.DrawLine(llPen, drawRect.Left, drawRect.Top + 1, drawRect.Right - 1, drawRect.Top + 1);                    
//					}      
//				}
//			}
//		}
//
		public static void DrawDragRectangle(Rectangle newRect, int indent)
		{
			DrawDragRectangles(new Rectangle[]{newRect}, indent);
		}

		public static void DrawDragRectangles(Rectangle[] newRects, int indent)
		{
			if (newRects.Length > 0)
			{
				// Create the first region
				IntPtr newRegion = CreateRectangleRegion(newRects[0], indent);

				for(int index=1; index<newRects.Length; index++)
				{
					// Create the extra region
					IntPtr extraRegion = CreateRectangleRegion(newRects[index], indent);

					// Remove the intersection of the existing and extra regions
					WinAPI.CombineRgn(newRegion, newRegion, extraRegion, (int)MControl.Win32.CombineFlags.RGN_XOR);

					// Remove unwanted intermediate objects
					WinAPI.DeleteObject(extraRegion);
				}

				// Get hold of the DC for the desktop
				IntPtr hDC = WinAPI.GetDC(IntPtr.Zero);

				// Define the area we are allowed to draw into
				WinAPI.SelectClipRgn(hDC, newRegion);

				Win32.RECT rectBox = new Win32.RECT();
				 
				// Get the smallest rectangle that encloses region
				WinAPI.GetClipBox(hDC, ref rectBox);
                 
				IntPtr brushHandler = GetHalfToneBrush();

				// Select brush into the device context
				IntPtr oldHandle = WinAPI.SelectObject(hDC, brushHandler);

				// Blit to screen using provided pattern brush and invert with existing screen contents
				WinAPI.PatBlt(hDC, 
					rectBox.left, 
					rectBox.top, 
					rectBox.right - rectBox.left, 
					rectBox.bottom - rectBox.top, 
					(uint)MControl.Win32.RasterOperations.PATINVERT);

				// Put old handle back again
				WinAPI.SelectObject(hDC, oldHandle);

				// Reset the clipping region
				WinAPI.SelectClipRgn(hDC, IntPtr.Zero);

				// Remove unwanted region object
				WinAPI.DeleteObject(newRegion);

				// Must remember to release the HDC resource!
				WinAPI.ReleaseDC(IntPtr.Zero, hDC);
			}
		}

		protected static IntPtr CreateRectangleRegion(Rectangle rect, int indent)
		{
			Win32.RECT newWinRect = new Win32.RECT();
			newWinRect.left = rect.Left;
			newWinRect.top = rect.Top;
			newWinRect.right = rect.Right;
			newWinRect.bottom = rect.Bottom;

			// Create region for whole of the new rectangle
			IntPtr newOuter = WinAPI.CreateRectRgnIndirect(ref newWinRect);

			// If the rectangle is to small to make an inner object from, then just use the outer
			if ((indent <= 0) || (rect.Width <= indent) || (rect.Height <= indent))
				return newOuter;

			newWinRect.left += indent;
			newWinRect.top += indent;
			newWinRect.right -= indent;
			newWinRect.bottom -= indent;

			// Create region for the unwanted inside of the new rectangle
			IntPtr newInner = WinAPI.CreateRectRgnIndirect(ref newWinRect);

			Win32.RECT emptyWinRect = new Win32.RECT();
			emptyWinRect.left = 0;
			emptyWinRect.top = 0;
			emptyWinRect.right = 0;
			emptyWinRect.bottom = 0;

			// Create a destination region
			IntPtr newRegion = WinAPI.CreateRectRgnIndirect(ref emptyWinRect);

			// Remove the intersection of the outer and inner
			WinAPI.CombineRgn(newRegion, newOuter, newInner, (int)MControl.Win32.CombineFlags.RGN_XOR);

			// Remove unwanted intermediate objects
			WinAPI.DeleteObject(newOuter);
			WinAPI.DeleteObject(newInner);

			// Return the resultant region object
			return newRegion;
		}

		protected static IntPtr GetHalfToneBrush()
		{
			if (_halfToneBrush == IntPtr.Zero)
			{	
				Bitmap bitmap = new Bitmap(8,8,PixelFormat.Format32bppArgb);

				Color white = Color.FromArgb(255,255,255,255);
				Color black = Color.FromArgb(255,0,0,0);

				bool flag=true;

				// Alternate black and white pixels across all lines
				for(int x=0; x<8; x++, flag = !flag)
					for(int y=0; y<8; y++, flag = !flag)
						bitmap.SetPixel(x, y, (flag ? white : black));

				IntPtr hBitmap = bitmap.GetHbitmap();

				MControl.Win32.LOGBRUSH brush = new MControl.Win32.LOGBRUSH();

				//?brush.lbStyle = ((uint)(MControl.Win32.BrushStyles.BS_PATTERN ));
				//?brush.lbHatch = ((uint)(hBitmap));
				//brush.lbStyle = MControl.Win32.BrushStyles.BS_PATTERN;
				//brush.lbHatch = hBitmap;

				_halfToneBrush = WinAPI.CreateBrushIndirect(ref brush);
			}

			return _halfToneBrush;
		}

		#endregion

		#region Methods Control

		public static Bitmap GetTileBitmap(Rectangle rcDest, Bitmap bitmap)
		{
			Bitmap tiledBitmap = new Bitmap(rcDest.Width, rcDest.Height);
			using ( Graphics g = Graphics.FromImage(tiledBitmap) )
			{
				for ( int i = 0; i < tiledBitmap.Width; i += bitmap.Width )
				{
					for ( int j = 0; j < tiledBitmap.Height; j += bitmap.Height )
					{
						g.DrawImage(bitmap, new Point(i, j));					

					}
				}
			}
			return tiledBitmap;
		}

		#endregion

		#region Drawing Arrows

		public static void DrawArrowGlyph(Graphics g, Rectangle rc, ArrowGlyph arrowGlyph, Brush brush)
		{
			DrawArrowGlyph(g, rc, 5, 3, arrowGlyph, brush);
		}

		public static void DrawArrowGlyph(Graphics g, Rectangle rc, int arrowWidth, int arrowHeight, ArrowGlyph arrowGlyph, Brush brush)
		{
			Point[] pts = new Point[3];
			int yMiddle = rc.Top + rc.Height/2-arrowHeight/2+1;
			int xMiddle = rc.Left + rc.Width/2;
			
			if ( arrowGlyph == ArrowGlyph.Up )
			{
				pts[0] = new Point(xMiddle, yMiddle-2);
				pts[1] = new Point(xMiddle-arrowWidth/2-1, yMiddle+arrowHeight-1);
				pts[2] = new Point(xMiddle+arrowWidth/2+1,  yMiddle+arrowHeight-1);
				
			}
			else if ( arrowGlyph == ArrowGlyph.Down )
			{
				pts[0] = new Point(xMiddle-arrowWidth/2, yMiddle);
				pts[1] = new Point(xMiddle+arrowWidth/2+1,  yMiddle);
				pts[2] = new Point(xMiddle, yMiddle+arrowHeight);
			}
			else if ( arrowGlyph == ArrowGlyph.Left )
			{
				yMiddle = rc.Top + rc.Height/2;
				pts[0] = new Point(xMiddle-arrowHeight/2,  yMiddle);
				pts[1] = new Point(pts[0].X+arrowHeight, yMiddle-arrowWidth/2-1);
				pts[2] = new Point(pts[0].X+arrowHeight,  yMiddle+arrowWidth/2+1);

			}
			else if ( arrowGlyph == ArrowGlyph.Right )
			{
				yMiddle = rc.Top + rc.Height/2;
				pts[0] = new Point(xMiddle+arrowHeight/2+1,  yMiddle);
				pts[1] = new Point(pts[0].X-arrowHeight, yMiddle-arrowWidth/2-1);
				pts[2] = new Point(pts[0].X-arrowHeight,  yMiddle+arrowWidth/2+1);
			}

			g.FillPolygon(brush, pts);
		}

		#endregion

		#region Draw Button

		public static void DrawButton(Graphics g,Color buttonColor, Color buttonHotColor, Color ButtonClickColor, Color borderColor, Color borderHotColor, Rectangle buttonRect,bool border_hot,bool btn_hot,bool btn_pressed)
		{
			if(btn_hot)
			{
				if(btn_pressed)
				{
					using (Brush brush = new SolidBrush(ButtonClickColor))
					{
						g.FillRectangle(brush,buttonRect);
					}
				}
				else
				{
					using (Brush brush = new SolidBrush(buttonHotColor))
					{
						g.FillRectangle(brush,buttonRect);
					}
				}
			}
			else
			{
				using (Brush brush = new SolidBrush(buttonColor))
				{
					g.FillRectangle(brush,buttonRect);
				}
			}

			buttonRect = new Rectangle(buttonRect.X-1,buttonRect.Y-1,buttonRect.Width+1,buttonRect.Height+1);
			if(border_hot || btn_hot || btn_pressed)
			{
				using (Pen pen = new Pen(borderHotColor))
				{
					g.DrawRectangle(pen,buttonRect);
				}
			}
			else
			{
				using (Pen pen = new Pen(borderColor))
				{
					g.DrawRectangle(pen,buttonRect);
				}
			}
		}

		#endregion

		#region Draw border

		public static void DrawBorder(Graphics g, Color borderColor, Color borderHotColor,Rectangle controlRect,bool hot)
		{
			controlRect = new Rectangle(controlRect.X,controlRect.Y,controlRect.Width - 1,controlRect.Height - 1);

			if(hot)
			{
				using (Pen pen = new Pen(borderHotColor))
				{
					g.DrawRectangle(pen,controlRect);
				}
			}
			else
			{
				using (Pen pen = new Pen(borderColor))
				{
					g.DrawRectangle(pen,controlRect);
				}
			}
		}

		public static void DrawBorder(Graphics g, Color borderColor, Color borderHotColor, Color FocusedColor,Rectangle controlRect,bool hot,bool Focused)
		{
			controlRect = new Rectangle(controlRect.X,controlRect.Y,controlRect.Width - 1,controlRect.Height - 1);

			if(Focused)
			{
				using (Pen pen = new Pen(FocusedColor))
				{
					g.DrawRectangle(pen,controlRect);
				}
			}
			else if(hot)
			{
				using (Pen pen = new Pen(borderHotColor))
				{
					g.DrawRectangle(pen,controlRect);
				}
			}
			else
			{
				using (Pen pen = new Pen(borderColor))
				{
					g.DrawRectangle(pen,controlRect);
				}
			}
		}

		#endregion

		#region Internal helpers

		private static GraphicsPath GetCapsule(RectangleF baseRect)
		{
			Single diameter;
			RectangleF arcRect;
			GraphicsPath rr = new GraphicsPath();
			try
			{
				if (baseRect.Width > baseRect.Height)
				{
					diameter = baseRect.Height;
					arcRect = new RectangleF(baseRect.Location, new 
						SizeF(diameter, diameter));
					rr.AddArc(arcRect, 90, 180);
					arcRect.X = baseRect.Right - diameter;
					rr.AddArc(arcRect, 270, 180);
				}
				else if (baseRect.Height > baseRect.Width)
				{
					diameter = baseRect.Width;
					arcRect = new RectangleF(baseRect.Location, new
						SizeF(diameter, diameter));
					rr.AddArc(arcRect, 180, 180);
					arcRect.Y = baseRect.Bottom - diameter;
					rr.AddArc(arcRect, 0, 180);
				}
				else
				{
					rr.AddEllipse(baseRect);
				}
			}
			catch
			{
				rr.AddEllipse(baseRect);
			}
			finally
			{
				rr.CloseFigure();
			}

			return rr;
		}

		#endregion

		#region Methods

		public static GraphicsPath GetRoundedRect(RectangleF baseRect, Single radius)
		{
			if (radius <= 0) 
			{
				GraphicsPath p = new GraphicsPath();
				p.AddRectangle(baseRect);
				return p;
			}

			if (radius >= (Math.Min(baseRect.Width, baseRect.Height) / 2.0))
				return GetCapsule(baseRect);

			Single diameter = radius + radius;
			RectangleF arcRect = new RectangleF(baseRect.Location, new SizeF(diameter,diameter));
			
			GraphicsPath rr	= new GraphicsPath();
			rr.AddArc(arcRect, 180, 90);

			arcRect.X = baseRect.Right - diameter;
			rr.AddArc(arcRect, 270, 90);

			arcRect.Y = baseRect.Bottom - diameter;
			rr.AddArc(arcRect, 0, 90);

			arcRect.X = baseRect.Left;
			rr.AddArc(arcRect, 90, 90);

			rr.CloseFigure();	
		
			return rr;
		}

		public static GraphicsPath GettRoundedTopRect(RectangleF baseRect, Single radius)
		{
			if (radius <= 0) 
			{
				GraphicsPath p = new GraphicsPath();
				p.AddRectangle(baseRect);
				return p;
			}

			Single diameter = radius + radius;
			
			GraphicsPath path1	= new GraphicsPath();

			path1.AddLine(baseRect.Left + radius , baseRect.Top, baseRect.Right - diameter , baseRect.Top);
			path1.AddArc(baseRect.Right - diameter , baseRect.Top, diameter, diameter, 270, 90);
			path1.AddLine(baseRect.Right, baseRect.Top + radius, baseRect.Right, baseRect.Bottom);
			//path.AddLine(baseRect.Right, baseRect.Top , baseRect.Right, baseRect.Bottom);
			path1.AddLine(baseRect.Right, baseRect.Bottom, baseRect.Left , baseRect.Bottom);
			path1.AddArc(baseRect.Left, baseRect.Top, diameter, diameter, 180, 90);

			path1.CloseFigure();	
		
			return path1;
		}

		public static GraphicsPath GettRoundedBottomRect(RectangleF baseRect, Single radius)
		{
			if (radius <= 0) 
			{
				GraphicsPath p = new GraphicsPath();
				p.AddRectangle(baseRect);
				return p;
			}

			Single diameter = radius + radius;
			GraphicsPath path1	= new GraphicsPath();
		
			path1.AddLine(baseRect.Left + radius , baseRect.Bottom, baseRect.Right - radius, baseRect.Bottom);
			path1.AddArc(baseRect.Right - diameter , baseRect.Bottom-diameter, diameter, diameter, 0, 90);
			path1.AddLine(baseRect.Right, baseRect.Bottom - radius, baseRect.Right, baseRect.Top);
			//path1.AddLine(baseRect.Right, baseRect.Top , baseRect.Right, baseRect.Bottom-radius);
			path1.AddLine(baseRect.Right, baseRect.Top, baseRect.Left , baseRect.Top);
			path1.AddLine(baseRect.Left, baseRect.Top, baseRect.Left , baseRect.Bottom-radius);
			path1.AddArc(baseRect.Left, baseRect.Bottom-diameter, diameter, diameter, 90, 90);
			path1.CloseFigure();	
		
			return path1;
		}

		public static void Draw3DRect(Graphics g, Rectangle rc, Color topLeft, Color bottomRight)
		{
			Draw3DRect(g, rc.Left, rc.Top, rc.Width, rc.Height,  topLeft, bottomRight);
		}

		public static void Draw3DRect(Graphics g, int x, int y, int width, int height, Color topLeft, Color bottomRight)
		{
			using (Brush brushTopLeft = new SolidBrush(topLeft),
					   brushBottomRight = new SolidBrush(bottomRight))
			{
				g.FillRectangle(brushTopLeft, x, y, width - 1, 1);
				g.FillRectangle(brushTopLeft, x, y, 1, height - 1);
				g.FillRectangle(brushBottomRight, x + width, y, -1, height);
				g.FillRectangle(brushBottomRight, x, y + height, width, -1);
			}
		}

		public static void DrawRectangleWithExternBound(Graphics g, Pen p, Rectangle r)
		{
			if (p.Width > 0.0)
			{
				r.Y += (int)p.Width-1;
				r.X += (int)p.Width-1;
				r.Width -= (int)(p.Width*2-1);
				r.Height -= (int)(p.Width*2-1);
				g.DrawRectangle(p,r);
			}
		}

		public static Bitmap LoadBitmap(Type assemblyType, string imageName)
		{
			return LoadBitmap(assemblyType, imageName, false, new Point(0,0));
		}

		public static Bitmap LoadBitmap(Type assemblyType, string imageName, Point transparentPixel)
		{
			return LoadBitmap(assemblyType, imageName, true, transparentPixel);
		}

		protected static Bitmap LoadBitmap(Type assemblyType, 
			string imageName, 
			bool makeTransparent, 
			Point transparentPixel)
		{
			Assembly myAssembly = Assembly.GetAssembly(assemblyType);
			Stream imageStream = myAssembly.GetManifestResourceStream(imageName);

			Bitmap image = new Bitmap(imageStream);
			if (makeTransparent)
			{
				Color backColor = image.GetPixel(transparentPixel.X, transparentPixel.Y);
				image.MakeTransparent(backColor);
			}
			    
			return image;
		}

		public static ImageList LoadBitmapStrip(Type assemblyType, string imageName, Size imageSize)
		{
			return LoadBitmapStrip(assemblyType, imageName, imageSize, false, new Point(0,0));
		}
		
		public static ImageList LoadBitmapStrip(Type assemblyType, 
			string imageName, 
			Size imageSize,
			Point transparentPixel)
		{
			return LoadBitmapStrip(assemblyType, imageName, imageSize, true, transparentPixel);
		}

		protected static ImageList LoadBitmapStrip(Type assemblyType, 
			string imageName, 
			Size imageSize,
			bool makeTransparent,
			Point transparentPixel)
		{
			ImageList images = new ImageList();

			images.ImageSize = imageSize;

			Assembly myAssembly = Assembly.GetAssembly(assemblyType);

			Stream imageStream = myAssembly.GetManifestResourceStream(imageName);

			Bitmap pics = new Bitmap(imageStream);

			if (makeTransparent)
			{
				Color backColor = pics.GetPixel(transparentPixel.X, transparentPixel.Y);
				pics.MakeTransparent(backColor);
			}
			    
			images.Images.AddStrip(pics);
			return images;
		}

		public static Cursor LoadCursor(Type assemblyType, string cursorName)
		{
			Assembly myAssembly = Assembly.GetAssembly(assemblyType);

			Stream iconStream = myAssembly.GetManifestResourceStream(cursorName);

			return new Cursor(iconStream);
		}

		public static void DrawIcon(Graphics g,Icon icon,Rectangle drawRect,bool grayed,bool pushed)
		{
			if(g == null || icon == null)
			{
				return;
			}

			if(pushed)
			{
				drawRect.Location = new Point(drawRect.X + 1,drawRect.Y + 1);
			}

			if(grayed)
			{
				Size s = new Size(drawRect.Size.Width-1,drawRect.Size.Height-1);
				ControlPaint.DrawImageDisabled(g,new Bitmap(icon.ToBitmap(),s),drawRect.X,drawRect.Y,Color.Transparent);
			}
			else
			{
				g.DrawIcon(icon,drawRect);
			}
		}

		public static void DrawImage(Graphics g,Image image,Rectangle drawRect,bool grayed,bool pushed)
		{
			if(g == null || image == null)
			{
				return;
			}

			if(pushed)
			{
				drawRect.Location = new Point(drawRect.X + 1,drawRect.Y + 1);
			}

			if(grayed)
			{
				Size s = new Size(drawRect.Size.Width-1,drawRect.Size.Height-1);
				ControlPaint.DrawImageDisabled(g,new Bitmap(image,s),drawRect.X,drawRect.Y,Color.Transparent);
			}
			else
			{
				g.DrawImage(image,drawRect);
			}
		}

		#endregion

		#region Drawing 

		/// <summary>
		/// Draw a border
		/// </summary>
		/// <param name="graphics"></param>
		/// <param name="rectangle"></param>
		/// <param name="border"></param>
//		public static void DrawBorder(Graphics graphics, Rectangle rectangle, RectangleBorder border)
//		{
//			if (border.Left.Width > 0)
//			{
//				using (Pen leftPen = new Pen(border.Left.Color))
//				{
//					leftPen.DashStyle = border.Left.DashStyle;
//
//					for (int i = 0; i < border.Left.Width; i++)
//						graphics.DrawLine(leftPen, rectangle.X+i, rectangle.Y, rectangle.X+i, rectangle.Bottom-1);
//				}
//			}
//
//			if (border.Bottom.Width > 0)
//			{
//				using (Pen bottomPen = new Pen(border.Bottom.Color))
//				{
//					bottomPen.DashStyle = border.Bottom.DashStyle;
//
//					for (int i = 1; i <= border.Bottom.Width; i++)
//						graphics.DrawLine(bottomPen, rectangle.X, rectangle.Bottom-i, rectangle.Right-1, rectangle.Bottom-i);
//				}
//			}
//
//			if (border.Right.Width > 0)
//			{
//				using (Pen rightPen = new Pen(border.Right.Color))
//				{
//					rightPen.DashStyle = border.Right.DashStyle;
//
//					for (int i = 1; i <= border.Right.Width; i++)
//						graphics.DrawLine(rightPen, rectangle.Right-i, rectangle.Y, rectangle.Right-i, rectangle.Bottom-1);
//				}
//			}
//
//			if (border.Top.Width > 0)
//			{
//				using (Pen topPen = new Pen(border.Top.Color))
//				{
//					topPen.DashStyle = border.Top.DashStyle;
//
//					for (int i = 0; i < border.Top.Width; i++)
//						graphics.DrawLine(topPen, rectangle.X, rectangle.Y+i, rectangle.Right-1, rectangle.Y+i);
//				}
//			}
//		}

		/// <summary>
		/// Draw a rounded rectangle with the specified pen.
		/// </summary>
		/// <param name="g"></param>
		/// <param name="roundRect"></param>
		/// <param name="pen"></param>
		public static void DrawRoundedRectangle(Graphics g, RoundedRectangle roundRect, Pen pen)
		{
			g.DrawPath(pen, roundRect.ToGraphicsPath());
		}

		/// <summary>
		/// Fill a rounded rectangle with the specified brush.
		/// </summary>
		/// <param name="g"></param>
		/// <param name="roundRect"></param>
		/// <param name="brush"></param>
		public static void FillRoundedRectangle(Graphics g, RoundedRectangle roundRect, Brush brush)
		{
			g.FillRegion(brush, new Region( roundRect.ToGraphicsPath() ));
		}

		/// <summary>
		/// Draw a 3D border inside the specified rectangle using a linear gradient border color.
		/// </summary>
		/// <param name="g"></param>
		/// <param name="p_HeaderRectangle"></param>
		/// <param name="p_BackColor"></param>
		/// <param name="p_DarkColor"></param>
		/// <param name="p_LightColor"></param>
		/// <param name="p_DarkGradientNumber">The width of the dark border</param>
		/// <param name="p_LightGradientNumber">The width of the light border</param>
		/// <param name="p_Style"></param>
		public static void DrawGradient3DBorder(Graphics g, 
			Rectangle p_HeaderRectangle, 
			Color p_BackColor, 
			Color p_DarkColor, 
			Color p_LightColor,
			int p_DarkGradientNumber,
			int p_LightGradientNumber,
			Gradient3DBorderStyle p_Style)
		{
			Color l_TopLeft, l_BottomRight;
			int l_TopLeftWidth, l_BottomRightWidth;
			if (p_Style == Gradient3DBorderStyle.Raised)
			{
				l_TopLeft = p_LightColor;
				l_TopLeftWidth = p_LightGradientNumber;
				l_BottomRight = p_DarkColor;
				l_BottomRightWidth = p_DarkGradientNumber;
			}
			else
			{
				l_TopLeft = p_DarkColor;
				l_TopLeftWidth = p_DarkGradientNumber;
				l_BottomRight = p_LightColor;
				l_BottomRightWidth = p_LightGradientNumber;
			}

			//TopLeftBorder
			Color[] l_TopLeftGradient = CalculateColorGradient(p_BackColor, l_TopLeft, l_TopLeftWidth);
			using (Pen l_Pen = new Pen(l_TopLeftGradient[0]))
			{
				for (int i = 0; i < l_TopLeftGradient.Length; i++)
				{
					l_Pen.Color = l_TopLeftGradient[l_TopLeftGradient.Length - (i+1)];

					//top
					g.DrawLine(l_Pen, p_HeaderRectangle.Left+i, p_HeaderRectangle.Top+i, p_HeaderRectangle.Right-(i+1), p_HeaderRectangle.Top+i);

					//Left
					g.DrawLine(l_Pen, p_HeaderRectangle.Left+i, p_HeaderRectangle.Top+i, p_HeaderRectangle.Left+i, p_HeaderRectangle.Bottom-(i+1));
				}
			}

			//BottomRightBorder
			Color[] l_BottomRightGradient = CalculateColorGradient(p_BackColor, l_BottomRight, l_BottomRightWidth);
			using (Pen l_Pen = new Pen(l_BottomRightGradient[0]))
			{
				for (int i = 0; i < l_BottomRightGradient.Length; i++)
				{
					l_Pen.Color = l_BottomRightGradient[l_BottomRightGradient.Length - (i+1)];

					//bottom
					g.DrawLine(l_Pen, p_HeaderRectangle.Left+i, p_HeaderRectangle.Bottom-(i+1), p_HeaderRectangle.Right-(i+1), p_HeaderRectangle.Bottom-(i+1));

					//right
					g.DrawLine(l_Pen, p_HeaderRectangle.Right-(i+1), p_HeaderRectangle.Top+i, p_HeaderRectangle.Right-(i+1), p_HeaderRectangle.Bottom-(i+1));
				}
			}
		}

		/// <summary>
		/// Paint the Text and the Image passed
		/// </summary>
		/// <param name="g">Graphics device where you can render your image and text</param>
		/// <param name="pDisplayRectangle">Relative rectangle based on the display area, without the borders</param>
		/// <param name="pImage">Image to draw. Can be null.</param>
		/// <param name="pImageAlignment">Alignment of the image</param>
		/// <param name="pImageStretch">True to stretch the image to all the display rectangle</param>
		/// <param name="pImageDisabled">True to draw the image as a disabled image, using CreateDisabledImage method.</param>
		/// <param name="pText">Text to draw (can be null)</param>
		/// <param name="pStringFormat">String format (can be null)</param>
		/// <param name="pAlignTextToImage">True to align the text with the image</param>
		/// <param name="pTextColor">Text Color</param>
		/// <param name="pTextFont">Text Font(can be null)</param>
		/// <param name="pTextDisabled">If true the text is drawed with KnownColor.GrayText color.</param>
		public static void DrawTextAndImage(Graphics g, 
			Rectangle pDisplayRectangle,
			Image pImage,
			System.Drawing.ContentAlignment pImageAlignment,
			bool pImageStretch,
			bool pImageDisabled,
			string pText,
			StringFormat pStringFormat,
			bool pAlignTextToImage,
			Color pTextColor,
			Font pTextFont,
			bool pTextDisabled)
		{
			if (pImageStretch)
				pAlignTextToImage = false; //in this case the image is resized with the size of the rectangle so no align is supported

			if (pDisplayRectangle.Width <= 0 || pDisplayRectangle.Height <= 0)
				return;

			if (pImageDisabled)
				pImage = CreateDisabledImage(pImage, Color.FromKnownColor(KnownColor.Control));
			if (pTextDisabled)
				pTextColor = Color.FromKnownColor(KnownColor.GrayText);

			//Image
			Rectangle rectImage = Rectangle.Empty;
			if (pImage != null)
			{
				if (pImageStretch)
				{
					g.DrawImage(pImage, pDisplayRectangle, new Rectangle(new Point(0,0), pImage.Size), GraphicsUnit.Pixel);
				}
				else
				{
					rectImage = CalculateContentRectangle(pImageAlignment, pDisplayRectangle, pImage.Size);
//#if !MINI
					g.DrawImage(pImage, rectImage);
//#else
//					g.DrawImage(pImage, rectImage, new Rectangle(0, 0, pImage.Width, pImage.Height), GraphicsUnit.Pixel);
//#endif
				}
			}

			//Text
			if (pText != null && pText.Length > 0)
			{
				Rectangle rectDrawText = pDisplayRectangle;
				//Align Text To Image
				if (pImage != null && pAlignTextToImage && pImageStretch == false)
				{
					rectDrawText = CalculateTextRectangleWithContent(pDisplayRectangle, pText, pStringFormat, rectImage, pImageAlignment);

					if (rectDrawText.Width <= 0 || rectDrawText.Height <= 0)
						return;
				}

				DrawString(g, rectDrawText, pText, pStringFormat, pTextColor, pTextFont);
			}
		}

		/// <summary>
		/// Draw the specified string inside the specified rectangle
		/// </summary>
		/// <param name="g"></param>
		/// <param name="pDestination"></param>
		/// <param name="pText"></param>
		/// <param name="pStringFormat"></param>
		/// <param name="pTextColor"></param>
		/// <param name="pTextFont"></param>
		public static void DrawString(Graphics g, Rectangle pDestination, String pText, StringFormat pStringFormat, Color pTextColor, Font pTextFont)
		{
			using (SolidBrush textBrush = new SolidBrush(pTextColor))
			{
				g.DrawString(pText,
					pTextFont,
					textBrush,
					pDestination,
					pStringFormat);
			}
		}

		/// <summary>
		/// Calculates the rectangle available to draw the specified Text aligned with another content (usually an image).
		/// </summary>
		/// <param name="pClientRectangle"></param>
		/// <param name="pText"></param>
		/// <param name="pStringFormat"></param>
		/// <param name="pOtherContentRect"></param>
		/// <param name="pOtherCOntentAlignment"></param>
		/// <returns></returns>
		public static Rectangle CalculateTextRectangleWithContent(Rectangle pClientRectangle, 
													string pText,
													StringFormat pStringFormat,
													Rectangle pOtherContentRect,
													ContentAlignment pOtherContentAlignment)
		{
			if (IsBottom(pOtherContentAlignment) && IsBottom(pStringFormat))
			{
				pClientRectangle.Height -= pOtherContentRect.Height;
			}
			if (IsTop(pOtherContentAlignment) && IsTop(pStringFormat))
			{
				pClientRectangle.Y += pOtherContentRect.Height;
				pClientRectangle.Height -= pOtherContentRect.Height;
			}
			if (IsLeft(pOtherContentAlignment) && IsLeft(pStringFormat))
			{
				pClientRectangle.X += pOtherContentRect.Width;
				pClientRectangle.Width -= pOtherContentRect.Width;
			}
			if (IsRight(pOtherContentAlignment) && IsRight(pStringFormat))
			{
				pClientRectangle.Width -= pOtherContentRect.Width;
			}

			return pClientRectangle;
		}

		/// <summary>
		/// Interpolate the specified number of times between start and end color
		/// </summary>
		/// <param name="p_StartColor"></param>
		/// <param name="p_EndColor"></param>
		/// <param name="p_NumberOfGradients"></param>
		/// <returns></returns>
		public static Color[] CalculateColorGradient(Color p_StartColor, Color p_EndColor, int p_NumberOfGradients)
		{
			if (p_NumberOfGradients<2)
				throw new ArgumentException("Invalid Number of gradients, must be 2 or more");
			Color[] l_Colors = new Color[p_NumberOfGradients];
			l_Colors[0] = p_StartColor;
			l_Colors[l_Colors.Length-1] = p_EndColor;

			float l_IncrementR = ((float)(p_EndColor.R-p_StartColor.R)) / (float)p_NumberOfGradients;
			float l_IncrementG = ((float)(p_EndColor.G-p_StartColor.G)) / (float)p_NumberOfGradients;
			float l_IncrementB = ((float)(p_EndColor.B-p_StartColor.B)) / (float)p_NumberOfGradients;

			for (int i = 1; i < (l_Colors.Length-1); i++)
			{
				l_Colors[i] = Color.FromArgb( (int) (p_StartColor.R + l_IncrementR*(float)i ), 
					(int) (p_StartColor.G + l_IncrementG*(float)i ),
					(int) (p_StartColor.B + l_IncrementB*(float)i ) );
			}

			return l_Colors;
		}

		/// <summary>
		/// Calculates the location of an object inside a client rectangle using the specified alignment
		/// </summary>
		/// <param name="align"></param>
		/// <param name="clientRect"></param>
		/// <param name="objectSize"></param>
		/// <returns></returns>
		public static Point CalculateContentLocation(System.Drawing.ContentAlignment align, 
													Rectangle clientRect, 
													Size objectSize)
		{
			//TODO si potrebbe far restituire sempre a questo metodo anche quale parte dell'oggetto risulta visibile (questo è utile quando le immagini sono più grosse e quindi bisogna disegnare solo la parte all'interno della cella), si potrebbe prendere in input e in output un Rectangle anche per l'oggetto

			//default X left
			PointF pointf = Point.Empty;

			if ( IsTop(align) ) //Y Top
				pointf.Y = (float)clientRect.Top;
			else if ( IsBottom(align) ) //Y bottom
				pointf.Y = (float)clientRect.Bottom - objectSize.Height;
			else //Y middle
				pointf.Y = (float)clientRect.Top + ((float)clientRect.Height)/2.0F -  ((float)objectSize.Height)/2.0F;

			if ( IsCenter(align) )//X Center
				pointf.X = (float)clientRect.Left + ((float)clientRect.Width)/2.0F - ((float)objectSize.Width)/2.0F;
			else if ( IsRight(align) )//X Right
				pointf.X = (float)clientRect.Left + (float)clientRect.Width - ((float)objectSize.Width);
			else //X left
				pointf.X = (float)clientRect.Left;

			return Point.Round( pointf );
		}

		/// <summary>
		/// Calculate the rectangle of the content specified
		/// </summary>
		/// <param name="align"></param>
		/// <param name="clientRect"></param>
		/// <param name="objectSize"></param>
		/// <returns></returns>
		public static Rectangle CalculateContentRectangle(System.Drawing.ContentAlignment align, 
			Rectangle clientRect, 
			Size objectSize)
		{
			Point contentPoint = CalculateContentLocation(align, clientRect, objectSize);
			Rectangle rect = new Rectangle(contentPoint, objectSize);
			rect.Intersect(clientRect);
			return rect;
		}

		public static System.Drawing.ContentAlignment StringFormatToContentAlignment(System.Drawing.StringFormat p_StringFormat)
		{
			if (IsBottom(p_StringFormat) && IsLeft(p_StringFormat))
				return System.Drawing.ContentAlignment.BottomLeft;
			else if (IsBottom(p_StringFormat) && IsRight(p_StringFormat))
				return System.Drawing.ContentAlignment.BottomRight;
			else if (IsBottom(p_StringFormat) && IsCenter(p_StringFormat))
				return System.Drawing.ContentAlignment.BottomCenter;

			else if (IsTop(p_StringFormat) && IsLeft(p_StringFormat))
				return System.Drawing.ContentAlignment.TopLeft;
			else if (IsTop(p_StringFormat) && IsRight(p_StringFormat))
				return System.Drawing.ContentAlignment.TopRight;
			else if (IsTop(p_StringFormat) && IsCenter(p_StringFormat))
				return System.Drawing.ContentAlignment.TopCenter;

			else if (IsMiddle(p_StringFormat) && IsLeft(p_StringFormat))
				return System.Drawing.ContentAlignment.MiddleLeft;
			else if (IsMiddle(p_StringFormat) && IsRight(p_StringFormat))
				return System.Drawing.ContentAlignment.MiddleRight;
			else //if (Utility.IsMiddle(StringFormat) && Utility.IsCenter(StringFormat))
				return System.Drawing.ContentAlignment.MiddleCenter;
		}

		public static void ApplyContentAlignmentToStringFormat(System.Drawing.ContentAlignment pAlignment, StringFormat stringFormat)
		{
			if (IsBottom(pAlignment))
				stringFormat.LineAlignment = StringAlignment.Far;
			else if (IsMiddle(pAlignment))
				stringFormat.LineAlignment = StringAlignment.Center;
			else //if (IsTop(pAlignment))
				stringFormat.LineAlignment = StringAlignment.Near;

			if (IsRight(pAlignment))
				stringFormat.Alignment = StringAlignment.Far;
			else if (IsCenter(pAlignment))
				stringFormat.Alignment = StringAlignment.Center;
			else //if (IsLeft(pAlignment))
				stringFormat.Alignment = StringAlignment.Near;
		}

		/// <summary>
		/// Converts the specified image to an array of byte using the specified format.
		/// </summary>
		/// <param name="img"></param>
		/// <param name="imgFormat"></param>
		/// <returns></returns>
		public static byte[] ImageToBytes(System.Drawing.Image img, System.Drawing.Imaging.ImageFormat imgFormat)
		{
			if (img == null)
				return new byte[0];

			byte[] bytes;
			using (System.IO.MemoryStream mem = new System.IO.MemoryStream())
			{
				img.Save(mem, imgFormat);
				bytes = mem.ToArray();
			}
			return bytes;
		}

		/// <summary>
		/// Converts the specified byte array to an Image object.
		/// </summary>
		/// <param name="bytes"></param>
		/// <returns></returns>
		public static System.Drawing.Image BytesToImage(byte[] bytes)
		{
			if (bytes == null || bytes.Length == 0)
				return null;

			System.Drawing.Image img;
			using (System.IO.MemoryStream mem = new System.IO.MemoryStream(bytes))
			{
				img = System.Drawing.Image.FromStream(mem);
			}
			return img;
		}

		private static System.Drawing.Imaging.ImageAttributes s_DisabledImageAttr;
		/// <summary>
		/// Create a disabled version of the image.
		/// </summary>
		/// <param name="image">The image to convert</param>
		/// <param name="background">The Color of the background behind the image. The background parameter is used to calculate the fill color of the disabled image so that it is always visible against the background.</param>
		/// <returns></returns>
		public static Image CreateDisabledImage(Image image, Color background)
		{
			Size imgSize = image.Size;
			if (s_DisabledImageAttr == null)
			{
				float[][] arrayJagged = new float[5][];
				arrayJagged[0] = new float[5] { 0.2125f, 0.2125f, 0.2125f, 0f, 0f } ;
				arrayJagged[1] = new float[5] { 0.2577f, 0.2577f, 0.2577f, 0f, 0f } ;
				arrayJagged[2] = new float[5] { 0.0361f, 0.0361f, 0.0361f, 0f, 0f } ;
				float[] arraySingle = new float[5];
				arraySingle[3] = 1f;
				arrayJagged[3] = arraySingle;
				arrayJagged[4] = new float[5] { 0.38f, 0.38f, 0.38f, 0f, 1f } ;
				System.Drawing.Imaging.ColorMatrix matrix = new System.Drawing.Imaging.ColorMatrix(arrayJagged);
				s_DisabledImageAttr = new System.Drawing.Imaging.ImageAttributes();
				s_DisabledImageAttr.ClearColorKey();

				//				ImageAttributes ia = new ImageAttributes(); 
				//				ColorMatrix cm = new ColorMatrix(); 
				//				cm.Matrix00 = 1/3f;
				//				cm.Matrix01 = 1/3f;
				//				cm.Matrix02 = 1/3f;
				//				cm.Matrix10 = 1/3f;
				//				cm.Matrix11 = 1/3f;
				//				cm.Matrix12 = 1/3f;
				//				cm.Matrix20 = 1/3f;
				//				cm.Matrix21 = 1/3f;
				//				cm.Matrix22 = 1/3f;

				s_DisabledImageAttr.SetColorMatrix(matrix);
			}

			Bitmap bitmap = new Bitmap(image.Width, image.Height);
			using (Graphics g = Graphics.FromImage(bitmap))
			{
				g.DrawImage(image, new Rectangle(0, 0, imgSize.Width, imgSize.Height), 0, 0, imgSize.Width, imgSize.Height, GraphicsUnit.Pixel, s_DisabledImageAttr);
			}

			return bitmap;
		}
		#endregion

		#region ContentAlign Utility
		public static bool IsBottom(System.Drawing.ContentAlignment a)
		{
			return (a == System.Drawing.ContentAlignment.BottomCenter ||
				a == System.Drawing.ContentAlignment.BottomLeft ||
				a == System.Drawing.ContentAlignment.BottomRight);
		}
		public static bool IsTop(System.Drawing.ContentAlignment a)
		{
			return (a == System.Drawing.ContentAlignment.TopCenter ||
				a == System.Drawing.ContentAlignment.TopLeft ||
				a == System.Drawing.ContentAlignment.TopRight);
		}
		public static bool IsMiddle(System.Drawing.ContentAlignment a)
		{
			return (a == System.Drawing.ContentAlignment.MiddleCenter ||
				a == System.Drawing.ContentAlignment.MiddleLeft ||
				a == System.Drawing.ContentAlignment.MiddleRight);
		}
		public static bool IsCenter(System.Drawing.ContentAlignment a)
		{
			return (a == System.Drawing.ContentAlignment.BottomCenter ||
				a == System.Drawing.ContentAlignment.MiddleCenter ||
				a == System.Drawing.ContentAlignment.TopCenter);
		}
		public static bool IsLeft(System.Drawing.ContentAlignment a)
		{
			return (a == System.Drawing.ContentAlignment.BottomLeft ||
				a == System.Drawing.ContentAlignment.MiddleLeft ||
				a == System.Drawing.ContentAlignment.TopLeft);
		}
		public static bool IsRight(System.Drawing.ContentAlignment a)
		{
			return (a == System.Drawing.ContentAlignment.BottomRight ||
				a == System.Drawing.ContentAlignment.MiddleRight ||
				a == System.Drawing.ContentAlignment.TopRight);
		}

		public static bool IsBottom(StringFormat a)
		{
			return (a.LineAlignment == StringAlignment.Far);
		}
		public static bool IsTop(StringFormat a)
		{
			return (a.LineAlignment == StringAlignment.Near);
		}
		public static bool IsMiddle(StringFormat a)
		{
			return (a.LineAlignment == StringAlignment.Center);
		}
		public static bool IsCenter(StringFormat a)
		{
			return (a.Alignment == StringAlignment.Center);
		}
		public static bool IsLeft(StringFormat a)
		{
			return (a.Alignment == StringAlignment.Near);
		}
		public static bool IsRight(StringFormat a)
		{
			return (a.Alignment == StringAlignment.Far);
		}
		#endregion

		#region Methods
//		static  public void Draw3DRect(Graphics g, Rectangle rc, Color topLeft, Color bottomRight)
//		{
//			Draw3DRect(g, rc.Left, rc.Top, rc.Width, rc.Height,  topLeft, bottomRight);
//
//		}
//
//		static  public void Draw3DRect(Graphics g, int x, int y, int width, int height, Color topLeft, Color bottomRight)
//		{
//			g.FillRectangle(new SolidBrush(topLeft), x, y, width - 1, 1);
//			g.FillRectangle(new SolidBrush(topLeft), x, y, 1, height - 1);
//			g.FillRectangle(new SolidBrush(bottomRight), x + width, y, -1, height);
//			g.FillRectangle(new SolidBrush(bottomRight), x, y + height, width, -1);
//		}

		static public void StrechBitmap(Graphics gDest, Rectangle rcDest, Bitmap bitmap)
		{

			// Draw From bitmap
			IntPtr hDCTo = gDest.GetHdc();
			WinAPI.SetStretchBltMode(hDCTo, (int)StrechModeFlags.HALFTONE);
			IntPtr hDCFrom = WinAPI.CreateCompatibleDC(hDCTo);
					
			IntPtr hOldFromBitmap = WinAPI.SelectObject(hDCFrom, bitmap.GetHbitmap());
			WinAPI.StretchBlt(hDCTo, rcDest.Left , rcDest.Top, rcDest.Width, rcDest.Height, hDCFrom, 
				0 , 0, bitmap.Width, bitmap.Height, (int)PatBltTypes.SRCCOPY);
                
			// Cleanup
			WinAPI.SelectObject(hDCFrom, hOldFromBitmap);
			gDest.ReleaseHdc(hDCTo);

		}

		static public void StrechBitmap(Graphics gDest, Rectangle rcDest, Rectangle rcSource, Bitmap bitmap)
		{

			// Draw From bitmap
			IntPtr hDCTo = gDest.GetHdc();
			WinAPI.SetStretchBltMode(hDCTo, (int)StrechModeFlags.COLORONCOLOR);
			IntPtr hDCFrom = WinAPI.CreateCompatibleDC(hDCTo);
					
			IntPtr hOldFromBitmap = WinAPI.SelectObject(hDCFrom, bitmap.GetHbitmap());
			WinAPI.StretchBlt(hDCTo, rcDest.Left , rcDest.Top, rcDest.Width, rcDest.Height, hDCFrom, 
				rcSource.Left , rcSource.Top, rcSource.Width, rcSource.Height, (int)PatBltTypes.SRCCOPY);
                
			// Cleanup
			WinAPI.SelectObject(hDCFrom, hOldFromBitmap);
			gDest.ReleaseHdc(hDCTo);

		}

		static public Bitmap GetStrechedBitmap(Graphics gDest, Rectangle rcDest, Bitmap bitmap)
		{

			// Draw To bitmap
			Bitmap newBitmap = new Bitmap(rcDest.Width, rcDest.Height);
			Graphics gBitmap = Graphics.FromImage(newBitmap); 
			IntPtr hDCTo = gBitmap.GetHdc();
			WinAPI.SetStretchBltMode(hDCTo, (int)StrechModeFlags.COLORONCOLOR);
			IntPtr hDCFrom = WinAPI.CreateCompatibleDC(hDCTo);
									
			IntPtr hOldFromBitmap = WinAPI.SelectObject(hDCFrom, bitmap.GetHbitmap());
			WinAPI.StretchBlt(hDCTo, rcDest.Left , rcDest.Top, rcDest.Width, rcDest.Height, hDCFrom, 
				0 , 0, bitmap.Width, bitmap.Height, (int)PatBltTypes.SRCCOPY);
                
			// Cleanup
			WinAPI.SelectObject(hDCFrom, hOldFromBitmap);
			gBitmap.ReleaseHdc(hDCTo);

			return newBitmap;

		}

//		static public Bitmap GetTileBitmap(Rectangle rcDest, Bitmap bitmap)
//		{
//
//			Bitmap tiledBitmap = new Bitmap(rcDest.Width, rcDest.Height);
//			using ( Graphics g = Graphics.FromImage(tiledBitmap) )
//			{
//				for ( int i = 0; i < tiledBitmap.Width; i += bitmap.Width )
//				{
//					for ( int j = 0; j < tiledBitmap.Height; j += bitmap.Height )
//					{
//						g.DrawImage(bitmap, new Point(i, j));					
//
//					}
//				}
//			}
//			return tiledBitmap;
//		}

		static public void DrawArrowGlyph(Graphics g, Rectangle rc, bool up, Brush brush)
		{
			// Draw arrow glyph with the default 
			// size of 5 pixel wide and 3 pixel high
			DrawArrowGlyph(g, rc, 5, 3, up, brush);
		}

		static public void DrawArrowGlyph(Graphics g, Rectangle rc, int arrowWidth, int arrowHeight, bool up, Brush brush)
		{
			
			// Tip: use an odd number for the arrowWidth and 
			// arrowWidth/2+1 for the arrowHeight 
			// so that the arrow gets the same pixel number
			// on the left and on the right and get symetrically painted
			Point[] pts = new Point[3];
			int yMiddle = rc.Top + rc.Height/2-arrowHeight/2+1;
			int xMiddle = rc.Left + rc.Width/2;
			if ( up )
			{
				pts[0] = new Point(xMiddle, yMiddle-2);
				pts[1] = new Point(xMiddle-arrowWidth/2-1, yMiddle+arrowHeight-1);
				pts[2] = new Point(xMiddle+arrowWidth/2+1,  yMiddle+arrowHeight-1);
				
			}
			else
			{
				pts[0] = new Point(xMiddle-arrowWidth/2, yMiddle);
				pts[1] = new Point(xMiddle+arrowWidth/2+1,  yMiddle);
				pts[2] = new Point(xMiddle, yMiddle+arrowHeight);
			}
			g.FillPolygon(brush, pts);
		}

		#endregion

		#region Methods

		public static string LoadToolboxBitmap(string imageName)
		{			
			//return Type.GetType("MControl.ResourceUtils").Assembly.GetManifestResourceStream("MControl.WinForms." + imageName);
			//return Application.UserAppDataPath +   imageName;
			System.Reflection.Assembly l_as = System.Reflection.Assembly.GetExecutingAssembly();
			return l_as.Location + imageName;
		}

		public static Image ExtractImage(string p_Image)
		{
			System.Reflection.Assembly  l_as = System.Reflection.Assembly.GetExecutingAssembly();
			return Image.FromStream(l_as.GetManifestResourceStream(p_Image));
			//return Image.FromStream(l_as.GetManifestResourceStream("MControl.WinForms.Images." + p_Image));
		}

//		public static Cursor LoadCursor(Type assemblyType, string cursorName)
//		{
//			// Get the assembly that contains the bitmap resource
//			Assembly myAssembly = Assembly.GetAssembly(assemblyType);
//
//			// Get the resource stream containing the images
//			Stream iconStream = myAssembly.GetManifestResourceStream(cursorName);
//
//			// Load the Icon from the stream
//			return new Cursor(iconStream);
//		}
    


		public static Image LoadImage(string imageName)
		{			
		
			
			Stream strm = Type.GetType("MControl.Drawing.ResourceUtils").Assembly.GetManifestResourceStream(imageName);
			//Stream strm = Type.GetType("MControl.ResourceUtils").Assembly.GetManifestResourceStream("MControl.WinForms.Images." + imageName);
 
			Image im = null;
			if(strm != null)
			{
				im = new System.Drawing.Bitmap(strm);
				strm.Close();
			}

			return im;
		}


		public static Icon LoadIcon(string iconName)
		{			
			
			Stream strm = Type.GetType("MControl.Drawing.ResourceUtils").Assembly.GetManifestResourceStream(iconName);
			//Stream strm = Type.GetType("MControl.ResourceUtils").Assembly.GetManifestResourceStream("MControl.WinForms.Images." + iconName);

			Icon ic = null;
			if(strm != null)
			{
				ic = new System.Drawing.Icon(strm);
				strm.Close();
			}

			return ic;
		}

		public static Icon LoadIcon(Type assemblyType, string iconName)
		{
			// Get the assembly that contains the bitmap resource
			Assembly myAssembly = Assembly.GetAssembly(assemblyType);

			// Get the resource stream containing the images
			Stream iconStream = myAssembly.GetManifestResourceStream(iconName);

			// Load the Icon from the stream
			return new Icon(iconStream);
		}

		public static Icon LoadIcon(Type assemblyType, string iconName, Size iconSize)
		{
			// Load the entire Icon requested (may include several different Icon sizes)
			Icon rawIcon = LoadIcon(assemblyType, iconName);
			
			// Create and return a new Icon that only contains the requested size
			return new Icon(rawIcon, iconSize); 
		}

//		public static Bitmap LoadBitmap(Type assemblyType, string imageName)
//		{
//			return LoadBitmap(assemblyType, imageName, false, new Point(0,0));
//		}
//
//		public static Bitmap LoadBitmap(Type assemblyType, string imageName, Point transparentPixel)
//		{
//			return LoadBitmap(assemblyType, imageName, true, transparentPixel);
//		}
//
//		public static ImageList LoadBitmapStrip(Type assemblyType, string imageName, Size imageSize)
//		{
//			return LoadBitmapStrip(assemblyType, imageName, imageSize, false, new Point(0,0));
//		}
//
//		public static ImageList LoadBitmapStrip(Type assemblyType, 
//			string imageName, 
//			Size imageSize,
//			Point transparentPixel)
//		{
//			return LoadBitmapStrip(assemblyType, imageName, imageSize, true, transparentPixel);
//		}
//
//		protected static Bitmap LoadBitmap(Type assemblyType, 
//			string imageName, 
//			bool makeTransparent, 
//			Point transparentPixel)
//		{
//			// Get the assembly that contains the bitmap resource
//			Assembly myAssembly = Assembly.GetAssembly(assemblyType);
//
//			// Get the resource stream containing the images
//			Stream imageStream = myAssembly.GetManifestResourceStream(imageName);
//
//			// Load the bitmap from stream
//			Bitmap image = new Bitmap(imageStream);
//
//			if (makeTransparent)
//			{
//				Color backColor = image.GetPixel(transparentPixel.X, transparentPixel.Y);
//    
//				// Make backColor transparent for Bitmap
//				image.MakeTransparent(backColor);
//			}
//			    
//			return image;
//		}
//
//		protected static ImageList LoadBitmapStrip(Type assemblyType, 
//			string imageName, 
//			Size imageSize,
//			bool makeTransparent,
//			Point transparentPixel)
//		{
//			// Create storage for bitmap strip
//			ImageList images = new ImageList();
//
//			// Define the size of images we supply
//			images.ImageSize = imageSize;
//
//			// Get the assembly that contains the bitmap resource
//			Assembly myAssembly = Assembly.GetAssembly(assemblyType);
//
//			// Get the resource stream containing the images
//			Stream imageStream = myAssembly.GetManifestResourceStream(imageName);
//
//			// Load the bitmap strip from resource
//			Bitmap pics = new Bitmap(imageStream);
//
//			if (makeTransparent)
//			{
//				Color backColor = pics.GetPixel(transparentPixel.X, transparentPixel.Y);
//    
//				// Make backColor transparent for Bitmap
//				pics.MakeTransparent(backColor);
//			}
//			    
//			// Load them all !
//			images.Images.AddStrip(pics);
//
//			return images;
//		}
 
		#endregion

		#region Icons

		public static Icon LoadIconStream(Type assemblyType, string iconName)
		{
			// Get the assembly that contains the bitmap resource
			Assembly myAssembly = Assembly.GetAssembly(assemblyType);

			// Get the resource stream containing the images
			Stream iconStream = myAssembly.GetManifestResourceStream(iconName);

			// Load the Icon from the stream
			return new Icon(iconStream);
		}

		public static Icon LoadIconStream(Type assemblyType, string iconName, Size iconSize)
		{
			// Load the entire Icon requested (may include several different Icon sizes)
			Icon rawIcon = LoadIconStream(assemblyType, iconName);
			
			// Create and return a new Icon that only contains the requested size
			return new Icon(rawIcon, iconSize); 
		}

		// The difference between the "LoadXStream" and "LoadXResource" functions is that
		// the load stream functions will load a embedded resource -- a file that you choose
		// to "embed as resource" while the load resource functions work with resource files
		// that have structure (.resX and .resources) and thus can hold several different 
		// resource items.

		public static Icon LoadIconResource(Type assemblyType, string resourceHolder, string imageName) 
		{
			// Get the assembly that contains the bitmap resource
			Assembly thisAssembly = Assembly.GetAssembly(assemblyType);
			ResourceManager rm = new ResourceManager(resourceHolder, thisAssembly);
			Icon icon = (Icon)rm.GetObject(imageName);
			return icon;
		}

		#endregion

		#region Bitmap

		public static Bitmap LoadBitmapStream(Type assemblyType, string imageName)
		{
			return LoadBitmapStream(assemblyType, imageName, false, new Point(0,0));
		}

		public static Bitmap LoadBitmapStream(Type assemblyType, string imageName, Point transparentPixel)
		{
			return LoadBitmapStream(assemblyType, imageName, true, transparentPixel);
		}

		protected static Bitmap LoadBitmapStream(Type assemblyType, string imageName, 
			bool makeTransparent, Point transparentPixel)
		{
			// Get the assembly that contains the bitmap resource
			Assembly myAssembly = Assembly.GetAssembly(assemblyType);

			// Get the resource stream containing the images
			Stream imageStream = myAssembly.GetManifestResourceStream(imageName);

			// Load the bitmap from stream
			Bitmap image = new Bitmap(imageStream);

			if (makeTransparent)
			{
				Color backColor = image.GetPixel(transparentPixel.X, transparentPixel.Y);
    
				// Make backColor transparent for Bitmap
				image.MakeTransparent(backColor);
			}
			    
			return image;
		}

		public static Bitmap LoadBitmapResource(Type assemblyType, string resourceHolder, string imageName) 
		{
			// Get the assembly that contains the bitmap resource
			Assembly thisAssembly = Assembly.GetAssembly(assemblyType);
			ResourceManager rm = new ResourceManager(resourceHolder, thisAssembly);
			Bitmap bitmap = (Bitmap)rm.GetObject(imageName);
			return bitmap;
		}

		#endregion

		#region ImageList

		public static ImageList LoadImageListStream(Type assemblyType, 
			string imageName, 
			Size imageSize)
		{
			return LoadImageListStream(assemblyType, imageName, imageSize, false, new Point(0,0));
		}

		public static ImageList LoadImageListStream(Type assemblyType, 
			string imageName, 
			Size imageSize,
			Point transparentPixel)
		{
			return LoadImageListStream(assemblyType, imageName, imageSize, true, transparentPixel);
		}

		public static ImageList LoadImageListStream(Type assemblyType, 
			string imageName, 
			Size imageSize,
			bool makeTransparent,
			Point transparentPixel)
		{
			// Create storage for bitmap strip
			ImageList images = new ImageList();

			// Define the size of images we supply
			images.ImageSize = imageSize;

			// Get the assembly that contains the bitmap resource
			Assembly myAssembly = Assembly.GetAssembly(assemblyType);

			// Get the resource stream containing the images
			Stream imageStream = myAssembly.GetManifestResourceStream(imageName);

			// Load the bitmap strip from resource
			Bitmap pics = new Bitmap(imageStream);

			if (makeTransparent)
			{
				Color backColor = pics.GetPixel(transparentPixel.X, transparentPixel.Y);
    
				// Make backColor transparent for Bitmap
				pics.MakeTransparent(backColor);
			}
			    
			// Load them all !
			images.Images.AddStrip(pics);

			return images;
		}

		public static ImageList LoadImageListResource(Type assemblyType, string resourceHolder,
			string imageName, Size imageSize)
		
		{
			return LoadImageListResource(assemblyType, resourceHolder, imageName, imageSize, false, new Point(0,0));
		}

		public static ImageList LoadImageListResource(Type assemblyType, string resourceHolder,
			string imageName, 
			Size imageSize,
			bool makeTransparent,
			Point transparentPixel)
		{
			// Create storage for bitmap strip
			ImageList images = new ImageList();

			// Define the size of images we supply
			images.ImageSize = imageSize;

			// Get the assembly that contains the bitmap resource
			Assembly thisAssembly = Assembly.GetAssembly(assemblyType);
			ResourceManager rm = new ResourceManager(resourceHolder, thisAssembly);
			Bitmap pics = (Bitmap)rm.GetObject(imageName);

			if (makeTransparent)
			{
				Color backColor = pics.GetPixel(transparentPixel.X, transparentPixel.Y);
    
				// Make backColor transparent for Bitmap
				pics.MakeTransparent(backColor);
			}
			    
			// Load the image
			images.Images.AddStrip(pics);

			return images;
		}

		#endregion

	}

}
