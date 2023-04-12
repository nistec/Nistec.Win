using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Nistec.WinForms
{
    public class Structures
    {
        // Nested Types
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct NCCALCSIZE_PARAMS
        {
            public Structures.RECT rc0;
            public Structures.RECT rc1;
            public Structures.RECT rc2;
            public IntPtr lppos;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
            public RECT(Rectangle rect)
            {
                this.left = rect.Left;
                this.top = rect.Top;
                this.right = rect.Right;
                this.bottom = rect.Bottom;
            }

            public RECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }

            public Rectangle Rect
            {
                get
                {
                    return new Rectangle(this.left, this.top, this.right - this.left, this.bottom - this.top);
                }
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWPOS
        {
            public IntPtr hwnd;
            public IntPtr hwndInsertAfter;
            public int x;
            public int y;
            public int cx;
            public int cy;
            public int flags;
        }
    }
}
