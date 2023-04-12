using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

using MControl.Win32;


namespace MControl.Drawing
{
	public sealed class McPaint
	{
		// Methods
		private McPaint(){}
		public static Bitmap ConvertToDisabled(Bitmap bmp)
		{
			Bitmap bitmap1 = new Bitmap(bmp.Width, bmp.Height);
			Graphics graphics1 = Graphics.FromImage(bitmap1);
			ImageAttributes attributes1 = new ImageAttributes();
			float[][] singleArrayArray1 = new float[6][];
			singleArrayArray1[0] = new float[] { 0.3f, 0.3f, 0.3f, 0f, 0f };
			singleArrayArray1[1] = new float[] { 0.59f, 0.59f, 0.59f, 0f, 0f };
			singleArrayArray1[2] = new float[] { 0.11f, 0.11f, 0.11f, 0f, 0f };
			float[] singleArray1 = new float[6];
			singleArray1[3] = 0.4f;
			singleArrayArray1[3] = singleArray1;
			singleArray1 = new float[6];
			singleArray1[4] = 0.4f;
			singleArrayArray1[4] = singleArray1;
			singleArray1 = new float[6];
			singleArray1[5] = 0.4f;
			singleArrayArray1[5] = singleArray1;
			ColorMatrix matrix1 = new ColorMatrix(singleArrayArray1);
			attributes1.SetColorMatrix(matrix1);
			graphics1.DrawImage(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, bmp.Width, bmp.Height, GraphicsUnit.Pixel, attributes1);
			graphics1.Dispose();
			return bitmap1;
		}

		public static Bitmap ConvertToGrayscale(Bitmap bmp)
		{
			Bitmap bitmap1 = new Bitmap(bmp.Width, bmp.Height);
			Graphics graphics1 = Graphics.FromImage(bitmap1);
			float[][] singleArrayArray1 = new float[6][];
			singleArrayArray1[0] = new float[] { 0.3f, 0.3f, 0.3f, 0f, 0f };
			singleArrayArray1[1] = new float[] { 0.59f, 0.59f, 0.59f, 0f, 0f };
			singleArrayArray1[2] = new float[] { 0.11f, 0.11f, 0.11f, 0f, 0f };
			float[] singleArray1 = new float[6];
			singleArray1[3] = 1f;
			singleArrayArray1[3] = singleArray1;
			singleArray1 = new float[6];
			singleArray1[4] = 1f;
			singleArrayArray1[4] = singleArray1;
			singleArray1 = new float[6];
			singleArray1[5] = 1f;
			singleArrayArray1[5] = singleArray1;
			ColorMatrix matrix1 = new ColorMatrix(singleArrayArray1);
			ImageAttributes attributes1 = new ImageAttributes();
			attributes1.SetColorMatrix(matrix1);
			graphics1.DrawImage(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, bmp.Width, bmp.Height, GraphicsUnit.Pixel, attributes1);
			graphics1.Dispose();
			return bitmap1;
		}

		public static Color Dark(Color baseColor, byte value)
		{
			byte num1 = baseColor.R;
			byte num2 = baseColor.G;
			byte num3 = baseColor.B;
			if ((num1 - value) < 0)
			{
				num1 = 0;
			}
			else
			{
				num1 = (byte) (num1 - value);
			}
			if ((num2 - value) < 0)
			{
				num2 = 0;
			}
			else
			{
				num2 = (byte) (num2 - value);
			}
			if ((num3 - value) < 0)
			{
				num3 = 0;
			}
			else
			{
				num3 = (byte) (num3 - value);
			}
			return Color.FromArgb(num1, num2, num3);
		}

 
		public static void DrawBorder(Graphics graphics, Rectangle bounds, bool isFocused, bool flat)
		{
			if (flat)
			{
				Color color1 = SystemColors.ControlDark;
				if (isFocused)
				{
					color1 = McColors.SelectedText;
				}
				using (Pen pen1 = new Pen(color1))
				{
					graphics.DrawRectangle(pen1, bounds.X, bounds.Y, bounds.Width, bounds.Height);
					return;
				}
			}
			bounds.Width++;
			bounds.Height++;
			ControlPaint.DrawBorder3D(graphics, bounds, Border3DStyle.Sunken);
		}

 
		public static void DrawButton(Graphics graphics, Rectangle bounds, Image image, bool isPressed, bool isFocused, bool isMouseOverButton, bool enabled, bool flat)
		{
			if (flat)
			{
				bounds.Width++;
				bounds.Height++;
				Color color1 = McColors.ControlStart;
				Color color2 = McColors.ControlEnd;
				if (isMouseOverButton)
				{
					color1 = McColors.ControlStartLight;
					color2 = McColors.ControlEndLight;
				}
				if (isPressed)
				{
					color1 = McColors.ControlStartDark;
					color2 = McColors.ControlEndDark;
				}
				if (!enabled)
				{
					color1 = color2;
				}
				using (Brush brush1 = new LinearGradientBrush(bounds, color1, color2, 90f))
				{
					graphics.FillRectangle(brush1, bounds);
				}
				Color color3 = SystemColors.ControlDark;
				if (isFocused)
				{
					color3 = McColors.SelectedText;
				}
				using (Pen pen1 = new Pen(color3))
				{
					bounds.X--;
					bounds.Y--;
					bounds.Width++;
					bounds.Height++;
					graphics.DrawRectangle(pen1, bounds);
					goto Label_0136;
				}
			}
			ButtonState state1 = ButtonState.Normal;
			if (isPressed)
			{
				state1 = ButtonState.Pushed;
			}
			if (!enabled)
			{
				state1 = ButtonState.Inactive;
			}
			bounds.Width++;
			bounds.Height++;
			if ((bounds.Width > 0) && (bounds.Height > 0))
			{
				ControlPaint.DrawButton(graphics, bounds, state1);
			}
			Label_0136:
				if (isPressed)
				{
					bounds.X++;
					bounds.Y++;
				}
			if (image != null)
			{
				if (flat)
				{
					bounds.X++;
					bounds.Y++;
				}
				if (enabled)
				{
					graphics.DrawImage(image, new Rectangle(bounds.X + ((bounds.Width - image.Width) / 2), bounds.Y + (((bounds.Height - image.Height) - 1) / 2), 0x10, 0x10));
				}
				else
				{
					McPaint.DrawImageDisabled(graphics, image, bounds.X + ((bounds.Width - image.Width) / 2), bounds.Y + (((bounds.Height - image.Height) - 1) / 2));
				}
			}
		}

		public static void DrawCheck(Graphics graphics, int x, int y, bool enabled)
		{
			x -= 3;
			y -= 3;
			Point point1 = new Point(x, y + 2);
			Point point2 = new Point(x + 2, y + 4);
			Point point3 = new Point(x + 6, y);
			Color color1 = Color.Black;
			if (!enabled)
			{
				color1 = SystemColors.ControlDark;
			}
			using (Pen pen1 = new Pen(color1))
			{
				graphics.DrawLine(pen1, point1, point2);
				graphics.DrawLine(pen1, point2, point3);
				point1.Y++;
				point2.Y++;
				point3.Y++;
				graphics.DrawLine(pen1, point1, point2);
				graphics.DrawLine(pen1, point2, point3);
				point1.Y++;
				point2.Y++;
				point3.Y++;
				graphics.DrawLine(pen1, point1, point2);
				graphics.DrawLine(pen1, point2, point3);
			}
		}

		public static void DrawFocus(Graphics graphics, Rectangle bounds)
		{
			Rectangle rectangle1 = new Rectangle(bounds.X + 2, bounds.Y + 2, bounds.Width - 3, bounds.Height - 3);
			ControlPaint.DrawFocusRectangle(graphics, rectangle1);
		}

		public static void DrawFocus(Graphics graphics, Rectangle bounds, Rectangle buttonBounds)
		{
			Rectangle rectangle1 = new Rectangle(bounds.X + 2, bounds.Y + 2, (bounds.Width - buttonBounds.Width) - 4, bounds.Height - 3);
			ControlPaint.DrawFocusRectangle(graphics, rectangle1);
		}

		public static void DrawImageDisabled(Graphics graphics, Bitmap bmp, int x, int y)
		{
			ImageAttributes attributes1 = new ImageAttributes();
			float[][] singleArrayArray1 = new float[6][];
			singleArrayArray1[0] = new float[] { 0.3f, 0.3f, 0.3f, 0f, 0f };
			singleArrayArray1[1] = new float[] { 0.59f, 0.59f, 0.59f, 0f, 0f };
			singleArrayArray1[2] = new float[] { 0.11f, 0.11f, 0.11f, 0f, 0f };
			float[] singleArray1 = new float[6];
			singleArray1[3] = 0.4f;
			singleArrayArray1[3] = singleArray1;
			singleArray1 = new float[6];
			singleArray1[4] = 0.4f;
			singleArrayArray1[4] = singleArray1;
			singleArray1 = new float[6];
			singleArray1[5] = 0.4f;
			singleArrayArray1[5] = singleArray1;
			ColorMatrix matrix1 = new ColorMatrix(singleArrayArray1);
			attributes1.SetColorMatrix(matrix1);
			graphics.DrawImage(bmp, new Rectangle(x, y, bmp.Width, bmp.Height), 0, 0, bmp.Width, bmp.Height, GraphicsUnit.Pixel, attributes1);
		}

 
		public static void DrawImageDisabled(Graphics graphics, Image image, int x, int y)
		{
			if (image is Bitmap)
			{
				McPaint.DrawImageDisabled(graphics, image as Bitmap, x, y);
			}
			else
			{
				ControlPaint.DrawImageDisabled(graphics, image, x, y, SystemColors.Control);
			}
		}

		public static void DrawItem(Graphics graphics, Rectangle bounds, DrawItemState state, Color backColor, Color foreColor)
		{
			if ((state & DrawItemState.Focus) != DrawItemState.None)
			{
				graphics.FillRectangle(McBrushes.Focus, bounds);
				graphics.DrawRectangle(McPens.SelectedText, bounds.X, bounds.Y, bounds.Width, bounds.Height - 1);
			}
			else if ((state & DrawItemState.Selected) != DrawItemState.None)
			{
				graphics.FillRectangle(McBrushes.Selected, bounds);
				graphics.DrawRectangle(McPens.SelectedText, bounds.X, bounds.Y, bounds.Width, bounds.Height - 1);
			}
			else
			{
				using (SolidBrush brush1 = new SolidBrush(backColor))
				{
					graphics.FillRectangle(brush1, bounds);
				}
			}
		}

 
		public static void DrawItem(Graphics graphics, Rectangle bounds, DrawItemState state, string text, ImageList imageList, int imageIndex, Font font, Color backColor, Color foreColor, RightToLeft rightToLeft)
		{
			McPaint.DrawItem(graphics, bounds, state, text, imageList, imageIndex, font, backColor, foreColor, 0, rightToLeft);
		}

		public static void DrawItem(Graphics graphics, Rectangle bounds, DrawItemState state, string text, ImageList imageList, int imageIndex, Font font, Color backColor, Color foreColor, int textStartPos, RightToLeft rightToLeft)
		{
			McPaint.DrawItem(graphics, bounds, state, backColor, foreColor);
			int num1 = 0;
			if (((imageList != null) && (imageIndex >= 0)) && (imageIndex < imageList.Images.Count))
			{
				Rectangle rectangle1 = new Rectangle(bounds.X + 1, bounds.Y + ((bounds.Height - imageList.ImageSize.Height) / 2), imageList.ImageSize.Width, imageList.ImageSize.Height);
				imageList.Draw(graphics, rectangle1.X, rectangle1.Y, rectangle1.Width, rectangle1.Height, imageIndex);
				num1 = imageList.ImageSize.Width + 2;
			}
			using (StringFormat format1 = new StringFormat())
			{
				format1.Alignment = StringAlignment.Near;
				format1.LineAlignment = StringAlignment.Center;
				format1.FormatFlags = StringFormatFlags.NoWrap;
				format1.Trimming = StringTrimming.EllipsisCharacter;
				if (rightToLeft == RightToLeft.Yes)
				{
					format1.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
				}
				bounds.X += num1 + textStartPos;
				bounds.Width -= num1 + textStartPos;
				if (text == null)
				{
					return;
				}
				using (SolidBrush brush1 = new SolidBrush(foreColor))
				{
					graphics.DrawString(text, font, brush1, (RectangleF) bounds, format1);
				}
			}
		}

 
		public static void DrawReversibleFrame(Graphics graphics, Rectangle rectangle, Color backColor, FrameStyle style)
		{
			McDrawMode mode1;
			Color color1;
			if (backColor.GetBrightness() < 0.5)
			{
				mode1 = McDrawMode.R2_NOTXORPEN;
				color1 = Color.White;
			}
			else
			{
				mode1 = McDrawMode.R2_XORPEN;
				color1 = Color.Black;
			}
			IntPtr ptr1 = IntPtr.Zero;
			switch (style)
			{
				case FrameStyle.Dashed:
				{
					ptr1 = WinAPI.CreatePen(2, 1, ColorTranslator.ToWin32(backColor));
					break;
				}
				case FrameStyle.Thick:
				{
					ptr1 = WinAPI.CreatePen(0, 1, ColorTranslator.ToWin32(backColor));
					break;
				}
			}
			IntPtr ptr2 = graphics.GetHdc();
			int num1 = WinAPI.SetROP2(ptr2, (int) mode1);
			IntPtr ptr3 = WinAPI.SelectObject(ptr2, WinAPI.GetStockObject(5));
			IntPtr ptr4 = WinAPI.SelectObject(ptr2, ptr1);
			WinAPI.SetBkColor(ptr2, ColorTranslator.ToWin32(color1));
			WinAPI.Rectangle(ptr2, rectangle.X, rectangle.Y, rectangle.Right, rectangle.Bottom);
			WinAPI.SetROP2(ptr2, num1);
			WinAPI.SelectObject(ptr2, ptr3);
			WinAPI.SelectObject(ptr2, ptr4);
			WinAPI.DeleteObject(ptr1);
			graphics.ReleaseHdc(ptr2);
		}

		public static void DrawReversibleFrame(Graphics graphics, int x, int y, int width, int height, Color backColor, FrameStyle style)
		{
			DrawReversibleFrame(graphics, new Rectangle(x, y, width, height), backColor, style);
		}

		public static void DrawReversibleLine(Graphics graphics, Point start, Point end, Color backColor, FrameStyle style)
		{
			McDrawMode mode1;
			Color color1;
			if (backColor.GetBrightness() < 0.5)
			{
				mode1 = McDrawMode.R2_NOTXORPEN;
				color1 = Color.White;
			}
			else
			{
				mode1 = McDrawMode.R2_XORPEN;
				color1 = Color.Black;
			}
			IntPtr ptr1 = IntPtr.Zero;
			switch (style)
			{
				case FrameStyle.Dashed:
				{
					ptr1 = WinAPI.CreatePen(2, 1, ColorTranslator.ToWin32(backColor));
					break;
				}
				case FrameStyle.Thick:
				{
					ptr1 = WinAPI.CreatePen(0, 1, ColorTranslator.ToWin32(backColor));
					break;
				}
			}
			IntPtr ptr2 = graphics.GetHdc();
			int num1 = WinAPI.SetROP2(ptr2, (int) mode1);
			IntPtr ptr3 = WinAPI.SelectObject(ptr2, WinAPI.GetStockObject(5));
			IntPtr ptr4 = WinAPI.SelectObject(ptr2, ptr1);
			WinAPI.SetBkColor(ptr2, ColorTranslator.ToWin32(color1));
			WinAPI.MoveToEx(ptr2, start.X, start.Y, IntPtr.Zero);
			WinAPI.LineTo(ptr2, end.X, end.Y);
			WinAPI.SetROP2(ptr2, num1);
			WinAPI.SelectObject(ptr2, ptr3);
			WinAPI.SelectObject(ptr2, ptr4);
			WinAPI.DeleteObject(ptr1);
			graphics.ReleaseHdc(ptr2);
		}

		public static void DrawReversibleLine(Graphics graphics, int startX, int startY, int endX, int endY, Color backColor, FrameStyle style)
		{
			McPaint.DrawReversibleLine(graphics, new Point(startX, startY), new Point(endX, endY), backColor, style);
		}

		public static void DrawString(Graphics graphics, string text, Font font, Brush brush, Rectangle layoutRectangle, StringFormat format, float angle)
		{
			if (angle != 0f)
			{
				Region region1 = graphics.Clip;
				graphics.SetClip(layoutRectangle, CombineMode.Intersect);
				GraphicsState state1 = graphics.Save();
				graphics.TranslateTransform((float) (layoutRectangle.Left + (layoutRectangle.Width / 2)), (float) (layoutRectangle.Top + (layoutRectangle.Height / 2)));
				graphics.RotateTransform(angle);
				layoutRectangle.X = -layoutRectangle.Width / 2;
				layoutRectangle.Y = -layoutRectangle.Height / 2;
				Rectangle rectangle1 = new Rectangle(layoutRectangle.X, layoutRectangle.Y, layoutRectangle.Width, layoutRectangle.Height);
				if (((angle > 45f) && (angle < 135f)) || ((angle > 225f) && (angle < 315f)))
				{
					rectangle1 = new Rectangle(layoutRectangle.Y, layoutRectangle.X, layoutRectangle.Height, layoutRectangle.Width);
				}
				graphics.DrawString(text, font, brush, (RectangleF) rectangle1, format);
				graphics.Restore(state1);
				graphics.SetClip(region1, CombineMode.Replace);
			}
			else
			{
				graphics.DrawString(text, font, brush, (RectangleF) layoutRectangle, format);
			}
		}

		public static void FillReversibleRectangle(Graphics graphics, Rectangle bounds, Color backColor)
		{
			int num1 = McPaint.GetColorRop(backColor, 0xa50065, 0x5a0049);
			int num2 = McPaint.GetColorRop(backColor, 6, 6);
			IntPtr ptr1 = graphics.GetHdc();
			IntPtr ptr2 = WinAPI.CreateSolidBrush(ColorTranslator.ToWin32(backColor));
			int num3 = WinAPI.SetROP2(ptr1, num2);
			IntPtr ptr3 = WinAPI.SelectObject(ptr1, ptr2);
			WinAPI.PatBlt(ptr1, bounds.X, bounds.Y, bounds.Width, bounds.Height, num1);
			WinAPI.SetROP2(ptr1, num3);
			WinAPI.SelectObject(ptr1, ptr3);
			WinAPI.DeleteObject(ptr2);
			graphics.ReleaseHdc(ptr1);
		}

		public static void FillReversibleRectangle(Graphics graphics, int x, int y, int width, int height, Color backColor)
		{
			McPaint.FillReversibleRectangle(graphics, new Rectangle(x, y, width, height), backColor);
		}

		public static Rectangle GetButtonRect(Rectangle bounds, bool flat)
		{
			return McPaint.GetButtonRect(bounds, flat, SystemInformation.HorizontalScrollBarArrowWidth - 1);
		}

		public static Rectangle GetButtonRect(Rectangle bounds, bool flat, int buttonWidth)
		{
			int num1 = SystemInformation.Border3DSize.Width;
			int num2 = SystemInformation.Border3DSize.Height;
			if (flat)
			{
				num1 = num2 = 1;
			}
			return new Rectangle((bounds.Right - buttonWidth) - num1, bounds.Top + num2, buttonWidth, bounds.Height - (num2 * 2));
		}

		public static int GetColorRop(Color color, int darkROP, int lightROP)
		{
			if (color.GetBrightness() < 0.5f)
			{
				return darkROP;
			}
			return lightROP;
		}

		public static Rectangle GetContentRect(Rectangle bounds, bool flat)
		{
			Rectangle rectangle1 = McPaint.GetButtonRect(bounds, flat);
			int num1 = SystemInformation.Border3DSize.Width;
			int num2 = SystemInformation.Border3DSize.Height;
			if (flat)
			{
				num1 = num2 = 1;
			}
			return new Rectangle(bounds.Left + num1, bounds.Top + num2, (bounds.Width - (num1 * 2)) - rectangle1.Width, bounds.Height - (num2 * 2));
		}

		public static Cursor GetCursor(Assembly cursorAssembly, string cursorName)
		{
			try
			{
				Stream stream1 = cursorAssembly.GetManifestResourceStream(cursorName);
				if (stream1 != null)
				{
					return new Cursor(stream1);
				}
			}
			catch
			{
			}
			return null;
		}

		public static Cursor GetCursor(string assemblyName, string cursorName)
		{
			Assembly[] assemblyArray1 = AppDomain.CurrentDomain.GetAssemblies();
			Assembly[] assemblyArray2 = assemblyArray1;
			for (int num1 = 0; num1 < assemblyArray2.Length; num1++)
			{
				Assembly assembly1 = assemblyArray2[num1];
				if (assembly1.GetName().Name == assemblyName)
				{
					return McPaint.GetCursor(assembly1, cursorName);
				}
			}
			return null;
		}

		public static Cursor GetCursor(Type type, string cursorName)
		{
			return McPaint.GetCursor(type.Module.Assembly, cursorName);
		}

		public static Bitmap GetImage(Assembly imageAssembly, string imageName)
		{
			try
			{
				Stream stream1 = imageAssembly.GetManifestResourceStream(imageName);
				if (stream1 != null)
				{
					Bitmap bitmap1 = new Bitmap(stream1);
					McPaint.MakeImageBackgroundAlphaZero(bitmap1);
					return bitmap1;
				}
			}
			catch
			{
			}
			return null;
		}

		public static Bitmap GetImage(string assemblyName, string imageName)
		{
			Assembly[] assemblyArray1 = AppDomain.CurrentDomain.GetAssemblies();
			Assembly[] assemblyArray2 = assemblyArray1;
			for (int num1 = 0; num1 < assemblyArray2.Length; num1++)
			{
				Assembly assembly1 = assemblyArray2[num1];
				if (assembly1.GetName().Name == assemblyName)
				{
					return McPaint.GetImage(assembly1, imageName);
				}
			}
			return null;
		}

		public static Bitmap GetImage(Type type, string imageName)
		{
			return McPaint.GetImage(type.Module.Assembly, imageName);
		}

		public static Color GetSysColor(ColorType colorType)
		{
			return ColorTranslator.FromWin32(WinAPI.GetSysColor((int) colorType));
		}

		public static Color Light(Color baseColor, byte value)
		{
			byte num1 = baseColor.R;
			byte num2 = baseColor.G;
			byte num3 = baseColor.B;
			if ((num1 + value) > 0xff)
			{
				num1 = 0xff;
			}
			else
			{
				num1 = (byte) (num1 + value);
			}
			if ((num2 + value) > 0xff)
			{
				num2 = 0xff;
			}
			else
			{
				num2 = (byte) (num2 + value);
			}
			if ((num3 + value) > 0xff)
			{
				num3 = 0xff;
			}
			else
			{
				num3 = (byte) (num3 + value);
			}
			return Color.FromArgb(num1, num2, num3);
		}

		public static void MakeImageBackgroundAlphaZero(Bitmap image)
		{
			Color color1 = image.GetPixel(0, image.Height - 1);
			image.MakeTransparent();
			Color color2 = Color.FromArgb(0, color1);
			image.SetPixel(0, image.Height - 1, color2);
		}

 
		public static Bitmap ReplaceImageColor(Bitmap bmp, Color colorForReplace, Color replacedColor)
		{
			Bitmap bitmap1 = new Bitmap(bmp.Width, bmp.Height);
			Graphics graphics1 = Graphics.FromImage(bitmap1);
			ImageAttributes attributes1 = new ImageAttributes();
			ColorMap map1 = new ColorMap();
			map1.OldColor = replacedColor;
			map1.NewColor = colorForReplace;
			attributes1.SetRemapTable(new ColorMap[] { map1 });
			graphics1.DrawImage(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, bmp.Width, bmp.Height, GraphicsUnit.Pixel, attributes1);
			graphics1.Dispose();
			return bitmap1;
		}

		public static bool ScrollWindow(IntPtr hWnd, int xAmount, int yAmount)
		{
			return WinAPI.ScrollWindowEx(hWnd, xAmount, yAmount, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, 10);
		}

	}


}
