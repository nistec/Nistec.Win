namespace MControl.Printing.View.Design
{
    using MControl.Printing.View;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    //mtd392
    internal class SectionDesigner : IDisposable
    {
        private int _var0;
        private Section _var1;
        private Font _var10;
        private Font _var11;
        private Image _var12;
        private Image _var13;
        private Image _var14;
        private Color _var2 = SystemColors.Control;
        private Color _var3 = SystemColors.Window;
        private Rectangle _var4;
        private Rectangle _var5;
        private Rectangle _var6;
        private Rectangle _var7;
        private Rectangle _var8;
        private int _var9 = 12;

        internal event MouseEventHandler mtd505;

        internal event MouseEventHandler mtd506;

        internal event MouseEventHandler mtd507;

        internal event MouseEventHandler mtd508;

        internal event MouseEventHandler mtd509;

        internal event MouseEventHandler mtd510;

        internal event MouseEventHandler mtd511;

        internal event MouseEventHandler mtd512;

        internal event MouseEventHandler mtd513;

        internal event MouseEventHandler mtd555;

        internal SectionDesigner(int var15, int var16, float var17, int var18, ref Section var19, ref Font var20, ref Font var21, bool var22, Image var14)
        {
            this._var10 = var20;
            this._var11 = var21;
            this._var1 = var19;
            this._var4 = new Rectangle(0x16, var15, var18, 20);
            this._var5 = new Rectangle(4, var15, 14, 20);
            this._var7 = new Rectangle(var16, this._var4.Bottom, (int) var17, (int) this._var1.Height);
            this._var6 = new Rectangle(0, this._var4.Bottom, 0x16, (int) this._var1.Height);
            this._var8 = new Rectangle(0x16, this._var7.Bottom, this._var7.Width, 4);
            this.var23(this._var6.Width, this._var6.Height + 20);
            this.var24(this._var7.Width, this._var7.Height);
            if (var19.Type == SectionType.ReportDetail)
            {
                this._var14 = var14;
            }
            this.mtd556();
            this.mtd496(var22);
        }

        internal void mtd496(bool var22)
        {
            Graphics g = Graphics.FromImage(this._var13);
            if (this._var1.BackColor == Color.Transparent)
            {
                using (Brush brush = new SolidBrush(Color.White))
                {
                    g.FillRectangle(brush, 0, 0, this._var7.Width, this._var7.Height);
                    goto Label_0095;
                }
            }
            using (Brush brush2 = new SolidBrush(this._var1.BackColor))
            {
                g.FillRectangle(brush2, 0, 0, this._var7.Width, this._var7.Height);
            }
        Label_0095:
            if (var22)
            {
                this.var29(g);
            }
            this._var1.DrawCtl(this, new PaintEventArgs(g, new Rectangle(0, 0, this._var13.Width, this._var13.Height)));
        }

        internal int mtd515(int var15)
        {
            this._var5.Y = var15;
            this._var4.Y = var15;
            this._var6.Y = this._var4.Bottom;
            this._var7.Y = this._var4.Bottom;
            this._var8.Y = this._var7.Bottom;
            return this._var7.Bottom;
        }

        internal bool mtd525(MouseEventArgs e)
        {
            if (this._var7.Contains(new Point(e.X, e.Y)))
            {
                this.mtd508(this, e);
                return true;
            }
            return false;
        }

        internal bool mtd526(MouseEventArgs e, MouseState var25)
        {
            Point pt = new Point(e.X, e.Y);
            if ((this._var0 > 0) && this._var5.Contains(pt))
            {
                if (var25 == MouseState.Move)
                {
                    this.mtd512(this, e);
                }
                else if (var25 == MouseState.Down)
                {
                    this.mtd513(this, e);
                }
                return true;
            }
            if (this._var4.Contains(pt))
            {
                if (var25 == MouseState.Move)
                {
                    this.mtd506(this, e);
                }
                else if (var25 == MouseState.Down)
                {
                    this.mtd507(this, e);
                }
                return true;
            }
            if (this._var7.Contains(pt))
            {
                if (var25 == MouseState.Down)
                {
                    this.mtd509(this, e);
                }
                else if (var25 == MouseState.Move)
                {
                    this.mtd508(this, e);
                }
                else if (var25 == MouseState.DoubleClick)
                {
                    this.mtd555(this, e);
                }
                return true;
            }
            if (this._var6.Contains(pt))
            {
                this.mtd505(this, e);
                return true;
            }
            if (!this._var8.Contains(pt))
            {
                return false;
            }
            if (var25 == MouseState.Move)
            {
                this.mtd510(this, e);
            }
            else if (var25 == MouseState.Down)
            {
                this.mtd511(this, e);
            }
            return true;
        }

        internal Point mtd527(int var31, int var32)
        {
            return new Point(var31 - this._var7.X, var32 - this._var7.Y);
        }

        internal void mtd536(int var35)
        {
            this._var7.X = var35;
            this._var8.X = var35;
        }

        internal void mtd538(int var33)
        {
            this._var6.Height = var33 - this._var6.Top;
            this.var23(this._var6.Width, this._var6.Height + 20);
            this.mtd556();
        }

        internal int mtd539(int var15)
        {
            this._var5.Y = var15;
            this._var6.Y = this._var5.Bottom;
            return this._var6.Bottom;
        }

        internal void mtd540(bool var22)
        {
            this._var7.Height = (int) this._var1.Height;
            this._var6.Height = this._var7.Height;
            this._var8.Y = this._var7.Bottom;
            this.var24(this._var13.Width, this._var7.Height);
            this.mtd496(var22);
            this.var23(this._var12.Width, this._var6.Height + 20);
            this.mtd556();
        }

        internal void mtd542(int var34, bool var22)
        {
            this._var7.Width = Math.Max(var34, 1);
            this._var8.Width = this._var7.Width;
            this.var24(this._var7.Width, this._var7.Height);
            this.mtd496(var22);
        }

        internal bool mtd545(Point var30)
        {
            return this._var7.Contains(var30);
        }

        internal void mtd556()
        {
            Graphics graphics = Graphics.FromImage(this._var12);
            Point[] points = new Point[] { new Point(0x11, 0), new Point(4, 0), new Point(4, 20) };
            Point[] pointArray2 = new Point[] { new Point(4, 0x13), new Point(0x11, 0x13), new Point(0x11, 0) };
            using (Pen pen = new Pen(Color.OldLace))
            {
                graphics.DrawLines(pen, points);
            }
            graphics.DrawLines(SystemPens.GrayText, pointArray2);
            graphics.FillRectangle(SystemBrushes.Window, 4, 20, 14, this._var6.Height);
            int num = 1;
            int num2 = 11;
            int num3 = 0;
            for (int i = this._var9 + 20; i <= (this._var6.Height + 20); i += this._var9)
            {
                num3++;
                switch (num3)
                {
                    case 4:
                        graphics.DrawLine(SystemPens.ControlText, num2 - 3, i, num2 + 3, i);
                        break;

                    case 8:
                    {
                        float height = graphics.MeasureString(num.ToString(), this._var10).Height;
                        float width = graphics.MeasureString(num.ToString(), this._var10).Width;
                        graphics.DrawString(num.ToString(), this._var10, Brushes.Black, (float) (num2 - (width / 2.2f)), (float) (i - (height / 2.2f)));
                        num++;
                        num3 = 0;
                        break;
                    }
                    default:
                        graphics.DrawLine(SystemPens.ControlText, num2 - 1, i, num2 + 1, i);
                        break;
                }
            }
        }

        internal void mtd98(Graphics g)
        {
            this.var26(g);
            this.var27(g);
            this.var28(g);
        }

        public void Dispose()
        {
            this._var13.Dispose();
            this._var12.Dispose();
        }

        private void var23(int var36, int var37)
        {
            Image image = this._var12;
            this._var12 = new Bitmap(Math.Max(var36, 1), Math.Max(var37, 1));
            if (image != null)
            {
                image.Dispose();
            }
        }

        private void var24(int var36, int var37)
        {
            Image image = this._var13;
            this._var13 = new Bitmap(Math.Max(var36, 1), Math.Max(var37, 1));
            if (image != null)
            {
                image.Dispose();
            }
        }

        private void var26(Graphics g)
        {
            Region clip = g.Clip;
            Rectangle rect = new Rectangle(0, this._var5.Y, this._var6.Width, this._var6.Height + 20);
            g.SetClip(rect, CombineMode.Intersect);
            g.DrawImage(this._var12, 0, this._var5.Y);
            g.Clip = clip;
        }

        private void var27(Graphics g)
        {
            Brush controlText;
            using (Brush brush2 = new SolidBrush(this._var2))
            {
                g.FillRectangle(brush2, 0x16, this._var4.Y, this._var4.Width, this._var4.Height);
            }
            g.DrawLine(SystemPens.GrayText, 0x16, this._var4.Y, this._var4.Right, this._var4.Y);
            if (this._var2 == SystemColors.Control)
            {
                controlText = SystemBrushes.ControlText;
            }
            else
            {
                controlText = SystemBrushes.Window;
            }
            if (this._var14 != null)
            {
                g.DrawImage(this._var14, 0x16, this._var4.Y + 3);
                g.DrawString(this._var1.Name, this._var11, controlText, 42f, (float) (this._var4.Y + 5));
            }
            else
            {
                g.DrawString(this._var1.Name, this._var11, controlText, 22f, (float) (this._var4.Y + 2));
            }
        }

        private void var28(Graphics g)
        {
            Region clip = g.Clip;
            g.SetClip(this.mtd519, CombineMode.Intersect);
            g.DrawImage(this._var13, this._var7.X, this._var7.Y);
            g.Clip = clip;
        }

        private void var29(Graphics g)
        {
            float width = 1f / g.DpiX;
            int num2 = this._var9 / 2;
            int num3 = 1;
            using (Pen pen = new Pen(Color.Gray, width))
            {
                for (int i = 6; i <= this._var7.Height; i += num2)
                {
                    if (num3 == 0x10)
                    {
                        num3 = 1;
                        g.DrawLine(SystemPens.ControlLight, 0, i, this._var7.Width, i);
                    }
                    else
                    {
                        for (int k = 6; k <= this._var7.Width; k += num2)
                        {
                            g.DrawLine(pen, (float) k, (float) i, k + width, (float) i);
                        }
                        num3++;
                    }
                }
                num2 = this._var9 * 8;
                for (int j = num2; j <= this._var7.Width; j += num2)
                {
                    g.DrawLine(SystemPens.ControlLight, j, 0, j, this._var7.Height);
                }
            }
        }

        internal int mtd29
        {
            get
            {
                return this._var4.Top;
            }
        }

        internal Section mtd393
        {
            get
            {
                return this._var1;
            }
        }

        internal int mtd408
        {
            get
            {
                return this._var7.Right;
            }
        }

        internal Point mtd480
        {
            get
            {
                return this._var7.Location;
            }
        }

        internal int mtd494
        {
            set
            {
                this._var9 = value;
            }
        }

        internal int mtd504
        {
            get
            {
                return this._var0;
            }
            set
            {
                this._var0 = value;
            }
        }

        internal int mtd514
        {
            get
            {
                return this._var7.Bottom;
            }
        }

        internal Rectangle mtd519
        {
            get
            {
                return new Rectangle(0x16, this._var7.Y, this._var7.Right - 0x16, this._var7.Height);
            }
        }

        internal int mtd534
        {
            get
            {
                return this._var8.Y;
            }
        }

        internal int mtd537
        {
            get
            {
                return this._var7.Y;
            }
        }

        internal int mtd546
        {
            set
            {
                this._var4.Width = value;
            }
        }

        internal Rectangle mtd547
        {
            get
            {
                return new Rectangle(0x16, this._var7.Y, this._var7.Right - 0x16, this._var7.Height);
            }
        }

        internal int mtd557
        {
            set
            {
                this._var7.Width = value;
            }
        }

        internal Color mtd558
        {
            set
            {
                this._var2 = value;
            }
        }

        internal Rectangle mtd559
        {
            get
            {
                return this._var4;
            }
        }

        internal Rectangle mtd57
        {
            get
            {
                return new Rectangle(0, this._var4.Y, this._var4.Width, this._var7.Height + this._var4.Height);
            }
        }

        internal string mtd91
        {
            get
            {
                return this._var1.Name;
            }
        }
    }
}

