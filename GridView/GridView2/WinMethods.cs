namespace MControl.Util
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    internal static class NativeMethods
    {

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
            public RECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }

            public RECT(Rectangle r)
            {
                this.left = r.Left;
                this.top = r.Top;
                this.right = r.Right;
                this.bottom = r.Bottom;
            }

            public static MControl.Util.NativeMethods.RECT FromXYWH(int x, int y, int width, int height)
            {
                return new MControl.Util.NativeMethods.RECT(x, y, x + width, y + height);
            }

            public System.Drawing.Size Size
            {
                get
                {
                    return new System.Drawing.Size(this.right - this.left, this.bottom - this.top);
                }
            }
        }


    }
}
