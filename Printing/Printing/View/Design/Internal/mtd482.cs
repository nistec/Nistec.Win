namespace MControl.Printing.View.Design
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    //mtd482
    internal class PanelDesigerBase : Panel
    {
        private Size _var3;
        private Point _var4;
        private HScrollBar var0;
        private var1 var2;

        internal event ScrollEventHandler mtd486;

        internal event ScrollEventHandler mtd487;

        internal PanelDesigerBase()
        {
            this.var5();
            this.var6();
        }

        internal void mtd518()
        {
            if ((base.Height > 0) && (this._var3.Height > base.Height))
            {
                int num = this._var3.Height - base.Height;
                this._var4.Y = 0x16 - num;
                this.var2.mtd137 = num;
            }
        }

        internal void mtd541()
        {
            if ((base.Width > 0) && (this._var3.Width > base.Width))
            {
                int num = this._var3.Width - base.Width;
                this._var4.X = 0x16 - num;
                this.var0.Value = num;
            }
        }

        protected virtual void OnHScroll(object var9, ScrollEventArgs e)
        {
            this._var4.X = 0x16 - e.NewValue;
            if (this.mtd486 != null)
            {
                this.mtd486(var9, e);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            this.var8();
            base.OnResize(e);
        }

        protected void OnVScroll(object var9, ScrollEventArgs e)
        {
            this._var4.Y = 0x16 - e.NewValue;
            if (this.mtd487 != null)
            {
                this.mtd487(this, e);
            }
        }

        private void var5()
        {
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            this._var3 = Size.Empty;
            this._var4 = Point.Empty;
        }

        private void var6()
        {
            this.var2 = new var1();
            this.var0 = new HScrollBar();
            base.SuspendLayout();
            this.var2.mtd550 = 0;
            this.var2.mtd551 = 12;
            this.var2.Dock = DockStyle.Right;
            this.var2.Location = new Point(0x248, 0);
            this.var2.Size = new Size(0x10, 600);
            this.var2.Scroll += new ScrollEventHandler(this.OnVScroll);
            this.var0.Minimum = 0;
            this.var0.SmallChange = 12;
            this.var0.Dock = DockStyle.Bottom;
            this.var0.Location = new Point(0, 0x248);
            this.var0.Size = new Size(600, 0x10);
            this.var0.Scroll += new ScrollEventHandler(this.OnHScroll);
            this.var0.MouseEnter += new EventHandler(this.var7);
            base.Controls.AddRange(new Control[] { this.var0, this.var2 });
            base.Size = new Size(600, 600);
            base.ResumeLayout(false);
        }

        private void var7(object var9, EventArgs e)
        {
            if (this.Cursor != Cursors.Default)
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void var8()
        {
            if ((base.Width > 0) && (this._var3.Width > base.Width))
            {
                this.var0.Maximum = this._var3.Width;
                this.var0.LargeChange = base.Width;
                this.var0.Visible = true;
            }
            else
            {
                this._var4.X = 0x16;
                this.var0.Value = 0;
                this.var0.Visible = false;
            }
            if ((base.Height > 0) && (this._var3.Height > base.Height))
            {
                this.var2.mtd552 = this._var3.Height;
                this.var2.mtd553 = base.Height;
                this.var2.mtd554(this.var0.Visible);
                this.var2.Visible = true;
            }
            else
            {
                this._var4.Y = 0x16;
                this.var2.mtd137 = 0;
                this.var2.Visible = false;
            }
        }

        internal Point mtd500
        {
            get
            {
                return this._var4;
            }
        }

        internal Size mtd549
        {
            get
            {
                return this._var3;
            }
            set
            {
                this._var3 = value;
                this.var8();
            }
        }

        private class var1 : Panel
        {
            private VScrollBar var2;

            internal new event ScrollEventHandler Scroll;

            internal var1()
            {
                this.var6();
            }

            internal void mtd554(bool var12)
            {
                if (var12)
                {
                    base.DockPadding.Bottom = 0x10;
                }
                else
                {
                    base.DockPadding.Bottom = 0;
                }
            }

            private void var10(object var9, ScrollEventArgs e)
            {
                if (this.Scroll != null)
                {
                    this.Scroll(var9, e);
                }
            }

            private void var11(object var9, EventArgs e)
            {
                if (this.Cursor != Cursors.Default)
                {
                    this.Cursor = Cursors.Default;
                }
            }

            private void var6()
            {
                this.var2 = new VScrollBar();
                base.SuspendLayout();
                this.var2.Dock = DockStyle.Fill;
                this.var2.Location = new Point(0, 0);
                this.var2.Size = new Size(0x10, 0x248);
                this.var2.Scroll += new ScrollEventHandler(this.var10);
                this.var2.MouseEnter += new EventHandler(this.var11);
                base.MouseEnter += new EventHandler(this.var11);
                base.Width = 0x10;
                this.BackColor = SystemColors.Control;
                base.Size = new Size(0x10, 600);
                base.DockPadding.Bottom = 0x10;
                base.Controls.Add(this.var2);
                base.ResumeLayout(false);
            }

            internal int mtd137
            {
                get
                {
                    return this.var2.Value;
                }
                set
                {
                    this.var2.Value = value;
                }
            }

            internal int mtd550
            {
                get
                {
                    return this.var2.Minimum;
                }
                set
                {
                    this.var2.Minimum = value;
                }
            }

            internal int mtd551
            {
                get
                {
                    return this.var2.SmallChange;
                }
                set
                {
                    this.var2.SmallChange = value;
                }
            }

            internal int mtd552
            {
                get
                {
                    return this.var2.Maximum;
                }
                set
                {
                    this.var2.Maximum = value;
                }
            }

            internal int mtd553
            {
                get
                {
                    return this.var2.LargeChange;
                }
                set
                {
                    this.var2.LargeChange = value;
                }
            }
        }
    }
}

