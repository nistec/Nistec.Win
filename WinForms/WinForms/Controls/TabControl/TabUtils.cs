using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Globalization;
using System.Reflection;
using System.IO;
using System.Resources;

using Nistec.Win32; 

using Nistec.Win;

namespace Nistec.WinForms
{

    #region Enum

    /// <summary>
    /// BackgroundIDE
    /// </summary>
    public enum BackgroundIDE
    {
        /// <summary>
        /// None
        /// </summary>
        None,
        /// <summary>
        /// Content
        /// </summary>
        Content,
        /// <summary>
        /// Light
        /// </summary>
        Light
    }

    ///// <summary>
    ///// PagesStyleSetting
    ///// </summary>
    //public enum PagesStyleSetting
    //{
    //    /// <summary>
    //    /// None
    //    /// </summary>
    //    None = 0,
    //    /// <summary>
    //    /// All
    //    /// </summary>
    //    All = 1,
    //}
    /// <summary>
    /// AlignmentOptions
    /// </summary>
    public enum AlignmentOptions
    {
        /// <summary>
        /// 
        /// </summary>
        Top = 0,
        /// <summary>
        /// 
        /// </summary>
        Bottom = 1
    }

    #endregion

	#region Enums

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

	#region TabColorUtils

	public class TabColorUtils
	{
		public static Color TabBackgroundFromBaseColor(Color backColor)
		{
			Color backIDE;

			// Check for the 'Classic' control color
			if ((backColor.R == 212) &&
				(backColor.G == 208) &&
				(backColor.B == 200))
			{
				// Use the exact background for this color
				backIDE = Color.FromArgb(247, 243, 233);
			}
			else
			{
				// Check for the 'XP' control color
				if ((backColor.R == 236) &&
					(backColor.G == 233) &&
					(backColor.B == 216))
				{
					// Use the exact background for this color
					backIDE = Color.FromArgb(255, 251, 233);
				}
				else
				{
					// Calculate the IDE background color as only half as dark as the control color
					int red = 255 - ((255 - backColor.R) / 2);
					int green = 255 - ((255 - backColor.G) / 2);
					int blue = 255 - ((255 - backColor.B) / 2);
					backIDE = Color.FromArgb(red, green, blue);
				}
			}
                        
			return backIDE;
		}
	}
	#endregion

	#region DrawTabUtils

	/// <summary>
	/// Drawing utility functions
	/// </summary>
	public class DrawTabUtils
	{
		private DrawTabUtils()
		{
		}

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

		public static void DrawPlainRaisedBorder(Graphics g, 
			Rectangle rect,
			Color lightLight, 
			Color baseColor,
			Color dark, 
			Color darkDark)
		{
			if ((rect.Width > 2) && (rect.Height > 2))
			{
				using(Pen ll = new Pen(lightLight),
						  b = new Pen(baseColor),
						  d = new Pen(dark),
						  dd = new Pen(darkDark))
				{
					int left = rect.Left;
					int top = rect.Top;
					int right = rect.Right;
					int bottom = rect.Bottom;

					// Draw the top border
					g.DrawLine(b, right-1, top, left, top);
					g.DrawLine(ll, right-2, top+1, left+1, top+1);
					g.DrawLine(b, right-3, top+2, left+2, top+2);

					// Draw the left border
					g.DrawLine(b, left, top, left, bottom-1);
					g.DrawLine(ll, left+1, top+1, left+1, bottom-2);
					g.DrawLine(b, left+2, top+2, left+2, bottom-3);
					
					// Draw the right
					g.DrawLine(dd, right-1, top+1, right-1, bottom-1);
					g.DrawLine(d, right-2, top+2, right-2, bottom-2);
					g.DrawLine(b, right-3, top+3, right-3, bottom-3);

					// Draw the bottom
					g.DrawLine(dd, right-1, bottom-1, left, bottom-1);
					g.DrawLine(d, right-2, bottom-2, left+1, bottom-2);
					g.DrawLine(b, right-3, bottom-3, left+2, bottom-3);
				}
			}
		}

		public static void DrawPlainRaisedBorderTopOrBottom(Graphics g, 
			Rectangle rect,
			Color lightLight, 
			Color baseColor,
			Color dark, 
			Color darkDark,
			bool drawTop)
		{
			if ((rect.Width > 2) && (rect.Height > 2))
			{
				using(Pen ll = new Pen(lightLight),
						  b = new Pen(baseColor),
						  d = new Pen(dark),
						  dd = new Pen(darkDark))
				{
					int left = rect.Left;
					int top = rect.Top;
					int right = rect.Right;
					int bottom = rect.Bottom;

					if (drawTop)
					{
						// Draw the top border
						g.DrawLine(b, right-1, top, left, top);
						g.DrawLine(ll, right-1, top+1, left, top+1);
						g.DrawLine(b, right-1, top+2, left, top+2);
					}
					else
					{
						// Draw the bottom
						g.DrawLine(dd, right-1, bottom-1, left, bottom-1);
						g.DrawLine(d, right-1, bottom-2, left, bottom-2);
						g.DrawLine(b, right-1, bottom-3, left, bottom-3);
					}
				}
			}
		}

		public static void DrawPlainSunkenBorder(Graphics g, 
			Rectangle rect,
			Color lightLight, 
			Color baseColor,
			Color dark, 
			Color darkDark)
		{
			if ((rect.Width > 2) && (rect.Height > 2))
			{
				using(Pen ll = new Pen(lightLight),
						  b = new Pen(baseColor),
						  d = new Pen(dark),
						  dd = new Pen(darkDark))
				{
					int left = rect.Left;
					int top = rect.Top;
					int right = rect.Right;
					int bottom = rect.Bottom;

					// Draw the top border
					g.DrawLine(d, right-1, top, left, top);
					g.DrawLine(dd, right-2, top+1, left+1, top+1);
					g.DrawLine(b, right-3, top+2, left+2, top+2);

					// Draw the left border
					g.DrawLine(d, left, top, left, bottom-1);
					g.DrawLine(dd, left+1, top+1, left+1, bottom-2);
					g.DrawLine(b, left+2, top+2, left+2, bottom-3);
					
					// Draw the right
					g.DrawLine(ll, right-1, top+1, right-1, bottom-1);
					g.DrawLine(b, right-2, top+2, right-2, bottom-2);
					g.DrawLine(b, right-3, top+3, right-3, bottom-3);

					// Draw the bottom
					g.DrawLine(ll, right-1, bottom-1, left, bottom-1);
					g.DrawLine(b, right-2, bottom-2, left+1, bottom-2);
					g.DrawLine(b, right-3, bottom-3, left+2, bottom-3);
				}
			}
		}

		public static void DrawPlainSunkenBorderTopOrBottom(Graphics g, 
			Rectangle rect,
			Color lightLight, 
			Color baseColor,
			Color dark, 
			Color darkDark,
			bool drawTop)
		{
			if ((rect.Width > 2) && (rect.Height > 2))
			{
				using(Pen ll = new Pen(lightLight),
						  b = new Pen(baseColor),
						  d = new Pen(dark),
						  dd = new Pen(darkDark))
				{
					int left = rect.Left;
					int top = rect.Top;
					int right = rect.Right;
					int bottom = rect.Bottom;

					if (drawTop)
					{
						// Draw the top border
						g.DrawLine(d, right-1, top, left, top);
						g.DrawLine(dd, right-1, top+1, left, top+1);
						g.DrawLine(b, right-1, top+2, left, top+2);
					}
					else
					{
						// Draw the bottom
						g.DrawLine(ll, right-1, bottom-1, left, bottom-1);
						g.DrawLine(b, right-1, bottom-2, left, bottom-2);
						g.DrawLine(b, right-1, bottom-3, left, bottom-3);
					}
				}
			}
		}
        
		public static void DrawButtonCommand(Graphics g, 
			VisualStyle style, 
			Direction direction, 
			Rectangle drawRect,
			CommandState state,
			Color baseColor,
			Color trackLight,
			Color trackBorder,StyleLayout layout)
		{
			Rectangle rect = new Rectangle(drawRect.Left, drawRect.Top, drawRect.Width - 1, drawRect.Height - 1);
        

			// Draw background according to style
			switch(style)
			{
				case VisualStyle.Plain:
					// Draw background with back color
					using(SolidBrush backBrush = new SolidBrush(baseColor))
						g.FillRectangle(backBrush, rect);

					// Modify according to state
				switch(state)
				{
					case CommandState.HotTrack:
						DrawPlainRaised(g, rect, baseColor);
						break;
					case CommandState.Pushed:
						DrawPlainSunken(g, rect, baseColor);
						break;
				}
					break;
				case VisualStyle.IDE:
					// Draw according to state
				switch(state)
				{
					case CommandState.Normal:
						// Draw background with back color
						using(SolidBrush backBrush = new SolidBrush(baseColor))
							g.FillRectangle(backBrush, rect);
						break;
					case CommandState.HotTrack:
						g.FillRectangle(Brushes.White, rect);

						using(Brush trackBrush =layout.GetBrushGradient(rect,90f,true))// new SolidBrush(trackLight))
							g.FillRectangle(trackBrush, rect);
                            
						using(Pen trackPen = new Pen(trackBorder))
							g.DrawRectangle(trackPen, rect);
						break;
					case CommandState.Pushed:
						//TODO: draw in a darker background color
						using(Brush trackBrush =layout.GetBrushGradient(rect,90f))
							g.FillRectangle(trackBrush, rect);
                            
						using(Pen trackPen = new Pen(trackBorder))
							g.DrawRectangle(trackPen, rect);
						break;
				}
					break;
			}
		}
        
		public static void DrawSeparatorCommand(Graphics g, 
			VisualStyle style, 
			Direction direction, 
			Rectangle drawRect,
			Color baseColor)
		{
			// Drawing depends on the visual style required
			if (style == VisualStyle.IDE)
			{
				// Draw a single separating line
				using(Pen dPen = new Pen(ControlPaint.Dark(baseColor)))
				{            
					if (direction == Direction.Horizontal)
						g.DrawLine(dPen, drawRect.Left, drawRect.Top,
							drawRect.Left, drawRect.Bottom - 1);
					else
						g.DrawLine(dPen, drawRect.Left, drawRect.Top,
							drawRect.Right - 1, drawRect.Top);                    
				}
			}
			else
			{
				// Draw a dark/light combination of lines to give an indent
				using(Pen lPen = new Pen(ControlPaint.Dark(baseColor)),
						  llPen = new Pen(ControlPaint.LightLight(baseColor)))
				{							
					if (direction == Direction.Horizontal)
					{
						g.DrawLine(lPen, drawRect.Left, drawRect.Top, drawRect.Left, drawRect.Bottom - 1);
						g.DrawLine(llPen, drawRect.Left + 1, drawRect.Top, drawRect.Left + 1, drawRect.Bottom - 1);
					}
					else
					{
						g.DrawLine(lPen, drawRect.Left, drawRect.Top, drawRect.Right - 1, drawRect.Top);                    
						g.DrawLine(llPen, drawRect.Left, drawRect.Top + 1, drawRect.Right - 1, drawRect.Top + 1);                    
					}      
				}
			}
		}

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
					WinAPI.CombineRgn(newRegion, newRegion, extraRegion, (int)Nistec.Win32.CombineFlags.RGN_XOR);

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
					(uint)Nistec.Win32.RasterOperations.PATINVERT);

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
			WinAPI.CombineRgn(newRegion, newOuter, newInner, (int)Nistec.Win32.CombineFlags.RGN_XOR);

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

				Nistec.Win32.LOGBRUSH brush = new Nistec.Win32.LOGBRUSH();

				//?brush.lbStyle = ((uint)(Nistec.Win32.BrushStyles.BS_PATTERN ));
				//?brush.lbHatch = ((uint)(hBitmap));
				//brush.lbStyle = Nistec.Win32.BrushStyles.BS_PATTERN;
				//brush.lbHatch = hBitmap;

				_halfToneBrush = WinAPI.CreateBrushIndirect(ref brush);
			}

			return _halfToneBrush;
		}

	}
	#endregion

}
