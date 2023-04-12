namespace MControl.Printing.Pdf.Core
{
    using MControl.Printing.Pdf.Drawing;
    using MControl.Printing.Pdf.Core.IO;
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.InteropServices;

    internal class A215
    {
        internal A215()
        {
        }

        internal static bool A216(ImageFormat b20, PixelFormat b19, ColorSpace b1)
        {
            return (((b20.Guid == ImageFormat.Jpeg.Guid) && (b19 != PixelFormat.Format1bppIndexed)) && ((b19 != PixelFormat.Format8bppIndexed) && (b1 == ColorSpace.RGB)));
        }

        internal static A112 A217(ref Image b0)
        {
            if (b0 != null)
            {
                MemoryStream stream = new MemoryStream();
                new Bitmap(b0).Save(stream, ImageFormat.Jpeg);
                return new A112(stream.GetBuffer(), (int) stream.Length);
            }
            return null;
        }

        internal static A112 A218(ref Bitmap b0, ColorSpace b1, int b2)
        {
            if (b0 == null)
            {
                throw new ArgumentNullException("image");
            }
            b3(ref b0, b2);
            A112 A = null;
            try
            {
                switch (b0.PixelFormat)
                {
                    case PixelFormat.Format1bppIndexed:
                        A = b4(ref b0);
                        goto Label_00FD;

                    case PixelFormat.Format4bppIndexed:
                        A = b5(ref b0, b1);
                        goto Label_00FD;

                    case PixelFormat.Format8bppIndexed:
                        A = b6(ref b0, b1);
                        goto Label_00FD;

                    case PixelFormat.Format16bppRgb555:
                    case PixelFormat.Format16bppRgb565:
                    case PixelFormat.Format16bppArgb1555:
                        A = b7(ref b0, b1);
                        goto Label_00FD;

                    case PixelFormat.Format24bppRgb:
                        A = b8(ref b0, b1);
                        goto Label_00FD;

                    case PixelFormat.Format32bppRgb:
                    case PixelFormat.Format32bppPArgb:
                    case PixelFormat.Format32bppArgb:
                        A = b9(ref b0, b1);
                        goto Label_00FD;

                    case PixelFormat.Format48bppRgb:
                    case PixelFormat.Format64bppPArgb:
                    case PixelFormat.Format64bppArgb:
                        break;

                    default:
                        goto Label_00FD;
                }
                A = b10(ref b0, b1);
            }
            catch
            {
            }
        Label_00FD:
            if (((A != null) && (A.A2 > 0)) && (b0.PixelFormat != PixelFormat.Format1bppIndexed))
            {
                A.A123();
            }
            return A;
        }

        internal static A112 A219(ref Image b0)
        {
            Bitmap bitmap;
            int num;
            if (b0 == null)
            {
                return null;
            }
            A112 A = new A112();
            if (b0 is Bitmap)
            {
                bitmap = (Bitmap) b0;
            }
            else
            {
                if (b0 is Metafile)
                {
                    bitmap = new Bitmap(b0);
                    bitmap = new Bitmap(b0.Width, b0.Height);
                    bitmap.SetResolution(b0.HorizontalResolution, b0.VerticalResolution);
                    System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
                    graphics.FillRectangle(new SolidBrush(Color.White), 0, 0, bitmap.Width, bitmap.Height);
                    try
                    {
                        graphics.DrawImage(b0, 0, 0);
                        goto Label_0099;
                    }
                    finally
                    {
                        graphics.Dispose();
                    }
                }
                bitmap = new Bitmap(b0);
            }
        Label_0099:
            num = 0;
            while (num < bitmap.Height)
            {
                for (int i = 0; i < bitmap.Width; i++)
                {
                    Color pixel = bitmap.GetPixel(i, num);
                    A.A54(pixel.R);
                    A.A54(pixel.G);
                    A.A54(pixel.B);
                }
                num++;
            }
            A.A123();
            return A;
        }

        internal static int A220(PixelFormat b19)
        {
            if (b19 > PixelFormat.Format16bppArgb1555)
            {
                if (b19 <= PixelFormat.Format48bppRgb)
                {
                    PixelFormat format2 = b19;
                    if (format2 != PixelFormat.Format32bppPArgb)
                    {
                        if (format2 == PixelFormat.Format16bppGrayScale)
                        {
                            return 1;
                        }
                        if (format2 == PixelFormat.Format48bppRgb)
                        {
                            return 0x10;
                        }
                        return 8;
                    }
                    return 8;
                }
                if (b19 != PixelFormat.Format64bppPArgb)
                {
                    if (b19 == PixelFormat.Format32bppArgb)
                    {
                        return 8;
                    }
                    if (b19 != PixelFormat.Format64bppArgb)
                    {
                        return 8;
                    }
                }
                return 8;
            }
            PixelFormat format = b19;
            if (format <= PixelFormat.Format32bppRgb)
            {
                switch (format)
                {
                    case PixelFormat.Format16bppRgb555:
                    case PixelFormat.Format16bppRgb565:
                    case PixelFormat.Format24bppRgb:
                    case PixelFormat.Format32bppRgb:
                        goto Label_0054;
                }
                goto Label_0056;
            }
            if (format != PixelFormat.Format1bppIndexed)
            {
                if ((format == PixelFormat.Format8bppIndexed) || (format == PixelFormat.Format16bppArgb1555))
                {
                    goto Label_0054;
                }
                goto Label_0056;
            }
            return 1;
        Label_0054:
            return 8;
        Label_0056:
            return 8;
        }

        private static A112 b10(ref Bitmap b0, ColorSpace b1)
        {
            if (b0 == null)
            {
                throw new ArgumentNullException("image");
            }
            A112 A = new A112();
            if (((b0.PixelFormat == PixelFormat.Format48bppRgb) || (b0.PixelFormat == PixelFormat.Format64bppArgb)) || (b0.PixelFormat == PixelFormat.Format64bppPArgb))
            {
                int num;
                Type t = typeof(ColorData16);
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, b0.Width, b0.Height);
                BitmapData bitmapdata = b0.LockBits(rect, ImageLockMode.ReadWrite, b0.PixelFormat);
                int stride = bitmapdata.Stride;
                long num3 = bitmapdata.Scan0.ToInt64();
                IntPtr ptr = new IntPtr(num3);
                if (b0.PixelFormat == PixelFormat.Format48bppRgb)
                {
                    num = Marshal.SizeOf(t);
                }
                else
                {
                    num = Marshal.SizeOf(t) + 2;
                }
                long num4 = num3;
                for (int i = 0; i < b0.Height; i++)
                {
                    ptr = new IntPtr(num3 + (i * stride));
                    num4 = ptr.ToInt64();
                    for (int j = 0; j < b0.Width; j++)
                    {
                        ColorData16 data2 = (ColorData16) Marshal.PtrToStructure(ptr, t);
                        switch (b1)
                        {
                            case ColorSpace.RGB:
                                A.A54(BitConverter.GetBytes(data2.Red), 2);
                                A.A54(BitConverter.GetBytes(data2.Green), 2);
                                A.A54(BitConverter.GetBytes(data2.Blue), 2);
                                break;

                            case ColorSpace.CMYK:
                            {
                                short[] numArray = PdfColor.A471(data2.Red, data2.Green, data2.Blue);
                                A.A54(BitConverter.GetBytes(numArray[0]), 2);
                                A.A54(BitConverter.GetBytes(numArray[1]), 2);
                                A.A54(BitConverter.GetBytes(numArray[2]), 2);
                                A.A54(BitConverter.GetBytes(numArray[3]), 2);
                                break;
                            }
                            case ColorSpace.GrayScale:
                            {
                                byte[] bytes = BitConverter.GetBytes((short) (((data2.Red + data2.Green) + data2.Blue) / 3));
                                A.A54(bytes, 2);
                                break;
                            }
                        }
                        num4 += num;
                        ptr = new IntPtr(num4);
                    }
                }
                b0.UnlockBits(bitmapdata);
                bitmapdata = null;
            }
            return A;
        }

        private static void b11(ref A112 b14, ColorData b15, ColorSpace b1)
        {
            b11(ref b14, b15.Red, b15.Green, b15.Blue, b1);
        }

        private static void b11(ref A112 b14, ColorDataEx b15, ColorSpace b1)
        {
            b11(ref b14, b15.Red, b15.Green, b15.Blue, b1);
        }

        private static void b11(ref A112 b14, Color b15, ColorSpace b1)
        {
            b11(ref b14, b15.R, b15.G, b15.B, b1);
        }

        private static void b11(ref A112 b14, byte b16, byte b17, byte b18, ColorSpace b1)
        {
            switch (b1)
            {
                case ColorSpace.RGB:
                    b14.A54(b16);
                    b14.A54(b17);
                    b14.A54(b18);
                    return;

                case ColorSpace.CMYK:
                {
                    byte[] buffer = PdfColor.A470(b16, b17, b18);
                    b14.A54(buffer, 4);
                    return;
                }
                case ColorSpace.GrayScale:
                    b14.A54(PdfColor.A469(b16, b17, b18));
                    return;
            }
        }

        private static ColorData b12(ref IntPtr b13)
        {
            ColorData data;
            byte num = Marshal.ReadByte(b13);
            long num2 = b13.ToInt64() + 1L;
            b13 = new IntPtr(num2);
            byte num3 = Marshal.ReadByte(b13);
            byte[] bytes = new byte[] { num, num3 };
            BitArray array = new BitArray(bytes);
            BitArray array2 = new BitArray(0x18);
            int num4 = 0;
            int num5 = 0;
            int num6 = array.Length - 1;
            while (num5 < num6)
            {
                switch (num5)
                {
                    case 0:
                    case 5:
                    case 10:
                        num4 += 3;
                        break;
                }
                array2[num4] = array[num5];
                num4++;
                num5++;
            }
            byte[] buffer2 = new byte[3];
            array2.CopyTo(buffer2, 0);
            data.Blue = buffer2[0];
            data.Green = buffer2[1];
            data.Red = buffer2[2];
            return data;
        }

        private static void b3(ref Bitmap b0, int b2)
        {
            if (b2 != -1)
            {
                FrameDimension dimension = new FrameDimension(b0.FrameDimensionsList[0]);
                int frameCount = b0.GetFrameCount(dimension);
                if ((frameCount > 0) && (b2 < frameCount))
                {
                    b0.SelectActiveFrame(dimension, b2);
                }
            }
        }

        private static A112 b4(ref Bitmap b0)
        {
            if (b0 == null)
            {
                throw new ArgumentNullException("image");
            }
            A112 A = new A112();
            if (b0.PixelFormat == PixelFormat.Format1bppIndexed)
            {
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, b0.Width, b0.Height);
                BitmapData bitmapdata = b0.LockBits(rect, ImageLockMode.ReadWrite, b0.PixelFormat);
                long num = bitmapdata.Scan0.ToInt64();
                IntPtr source = new IntPtr(num);
                ColorPalette palette = b0.Palette;
                int num2 = (b0.Width / 8) + (((b0.Width & 7) != 0) ? 1 : 0);
                int stride = bitmapdata.Stride;
                byte[] destination = new byte[b0.Height * stride];
                Marshal.Copy(source, destination, 0, destination.Length);
                BitArray array = new BitArray(destination);
                destination = new byte[b0.Height * num2];
                int num4 = num2 * 8;
                int index = 0;
                int num6 = 0x80;
                int num7 = 0;
                for (int i = 0; i < b0.Height; i++)
                {
                    int num9 = (i * stride) * 8;
                    for (int j = 0; j < num4; j += 8)
                    {
                        int num11 = num9 + j;
                        for (int k = 7; k >= 0; k--)
                        {
                            int num13 = num11 + k;
                            if (array[num13])
                            {
                                num7 |= num6;
                            }
                            num6 = num6 >> 1;
                        }
                        destination[index] = (byte) num7;
                        index++;
                        num6 = 0x80;
                        num7 = 0;
                    }
                }
                A = A554.A123(destination, b0.Width, b0.Height);
                b0.UnlockBits(bitmapdata);
                bitmapdata = null;
            }
            return A;
        }

        private static A112 b5(ref Bitmap b0, ColorSpace b1)
        {
            if (b0 == null)
            {
                throw new ArgumentNullException("image");
            }
            A112 A = new A112();
            if (b0.PixelFormat == PixelFormat.Format4bppIndexed)
            {
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, b0.Width, b0.Height);
                BitmapData bitmapdata = b0.LockBits(rect, ImageLockMode.ReadWrite, b0.PixelFormat);
                int stride = bitmapdata.Stride;
                long num2 = bitmapdata.Scan0.ToInt64();
                IntPtr ptr = new IntPtr(num2);
                ColorPalette palette = b0.Palette;
                long num3 = num2;
                Color white = Color.White;
                bool flag = false;
                for (int i = 0; i < rect.Height; i++)
                {
                    num3 = new IntPtr(num2 + (i * stride)).ToInt64();
                    for (int j = 0; j < rect.Width; j += 2)
                    {
                        ptr = new IntPtr(num3);
                        byte num6 = Marshal.ReadByte(ptr);
                        int num7 = ((j + 1) == rect.Width) ? 1 : 2;
                        for (int k = 0; k < num7; k++)
                        {
                            Color color2;
                            int index = (k == 0) ? ((num6 >> 4) & 15) : (num6 & 15);
                            if (index < palette.Entries.Length)
                            {
                                color2 = palette.Entries[index];
                                if ((color2.A == 0) && !flag)
                                {
                                    white = color2;
                                    flag = true;
                                }
                            }
                            else if (flag)
                            {
                                color2 = white;
                            }
                            else
                            {
                                color2 = Color.White;
                            }
                            b11(ref A, color2, b1);
                        }
                        num3 += 1L;
                    }
                }
                b0.UnlockBits(bitmapdata);
                bitmapdata = null;
            }
            return A;
        }

        private static A112 b6(ref Bitmap b0, ColorSpace b1)
        {
            if (b0 == null)
            {
                throw new ArgumentNullException("image");
            }
            A112 A = new A112();
            if (b0.PixelFormat == PixelFormat.Format8bppIndexed)
            {
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, b0.Width, b0.Height);
                BitmapData bitmapdata = b0.LockBits(rect, ImageLockMode.ReadWrite, b0.PixelFormat);
                int stride = bitmapdata.Stride;
                long num2 = bitmapdata.Scan0.ToInt64();
                IntPtr ptr = new IntPtr(num2);
                ColorPalette palette = b0.Palette;
                long num3 = num2;
                Color white = Color.White;
                bool flag = false;
                for (int i = 0; i < rect.Height; i++)
                {
                    num3 = new IntPtr(num2 + (i * stride)).ToInt64();
                    for (int j = 0; j < rect.Width; j++)
                    {
                        Color color2;
                        ptr = new IntPtr(num3);
                        byte index = Marshal.ReadByte(ptr);
                        if (index < palette.Entries.Length)
                        {
                            color2 = palette.Entries[index];
                            if ((color2.A == 0) && !flag)
                            {
                                white = color2;
                                flag = true;
                            }
                        }
                        else if (flag)
                        {
                            color2 = white;
                        }
                        else
                        {
                            color2 = Color.White;
                        }
                        b11(ref A, color2, b1);
                        num3 += 1L;
                    }
                }
                b0.UnlockBits(bitmapdata);
                bitmapdata = null;
            }
            return A;
        }

        private static A112 b7(ref Bitmap b0, ColorSpace b1)
        {
            if (b0 == null)
            {
                throw new ArgumentNullException("image");
            }
            A112 A = new A112();
            if (b0.PixelFormat == PixelFormat.Format16bppRgb555)
            {
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, b0.Width, b0.Height);
                BitmapData bitmapdata = b0.LockBits(rect, ImageLockMode.ReadWrite, b0.PixelFormat);
                int stride = bitmapdata.Stride;
                long num2 = bitmapdata.Scan0.ToInt64();
                IntPtr ptr = new IntPtr(num2);
                long num3 = num2;
                for (int i = 0; i < b0.Height; i++)
                {
                    ptr = new IntPtr(num2 + (i * stride));
                    num3 = ptr.ToInt64();
                    for (int j = 0; j < b0.Width; j++)
                    {
                        ColorData data2 = b12(ref ptr);
                        b11(ref A, data2, b1);
                        num3 = ptr.ToInt64() + 1L;
                        ptr = new IntPtr(num3);
                    }
                }
                b0.UnlockBits(bitmapdata);
                bitmapdata = null;
            }
            return A;
        }

        private static A112 b8(ref Bitmap b0, ColorSpace b1)
        {
            if (b0 == null)
            {
                throw new ArgumentNullException("image");
            }
            A112 A = new A112();
            if (b0.PixelFormat == PixelFormat.Format24bppRgb)
            {
                Type t = typeof(ColorData);
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, b0.Width, b0.Height);
                BitmapData bitmapdata = b0.LockBits(rect, ImageLockMode.ReadWrite, b0.PixelFormat);
                int stride = bitmapdata.Stride;
                long num2 = bitmapdata.Scan0.ToInt64();
                IntPtr ptr = new IntPtr(num2);
                int num3 = Marshal.SizeOf(t);
                long num4 = num2;
                for (int i = 0; i < b0.Height; i++)
                {
                    ptr = new IntPtr(num2 + (i * stride));
                    num4 = ptr.ToInt64();
                    for (int j = 0; j < b0.Width; j++)
                    {
                        ColorData data2 = (ColorData) Marshal.PtrToStructure(ptr, t);
                        b11(ref A, data2, b1);
                        num4 += num3;
                        ptr = new IntPtr(num4);
                    }
                }
                b0.UnlockBits(bitmapdata);
                bitmapdata = null;
            }
            return A;
        }

        private static A112 b9(ref Bitmap b0, ColorSpace b1)
        {
            if (b0 == null)
            {
                throw new ArgumentNullException("image");
            }
            A112 A = new A112();
            if (((b0.PixelFormat == PixelFormat.Format32bppArgb) || (b0.PixelFormat == PixelFormat.Format32bppRgb)) || (b0.PixelFormat == PixelFormat.Format32bppPArgb))
            {
                Type t = typeof(ColorDataEx);
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, b0.Width, b0.Height);
                BitmapData bitmapdata = b0.LockBits(rect, ImageLockMode.ReadWrite, b0.PixelFormat);
                int stride = bitmapdata.Stride;
                long num2 = bitmapdata.Scan0.ToInt64();
                IntPtr ptr = new IntPtr(num2);
                int num3 = Marshal.SizeOf(t);
                long num4 = num2;
                for (int i = 0; i < b0.Height; i++)
                {
                    ptr = new IntPtr(num2 + (i * stride));
                    num4 = ptr.ToInt64();
                    for (int j = 0; j < b0.Width; j++)
                    {
                        ColorDataEx ex = (ColorDataEx) Marshal.PtrToStructure(ptr, t);
                        b11(ref A, ex, b1);
                        num4 += num3;
                        ptr = new IntPtr(num4);
                    }
                }
                b0.UnlockBits(bitmapdata);
                bitmapdata = null;
            }
            return A;
        }
    }
}

