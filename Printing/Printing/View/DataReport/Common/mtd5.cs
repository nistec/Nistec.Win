namespace Nistec.Printing.View
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Windows.Forms;

    //mtd5
    [ToolboxItemFilter("Nistec.Printing.View", ToolboxItemFilterType.Prevent)]
    internal class CtlInternal : Control
    {
        private var0 _var0;
        private Point _var10;
        private PointF _var11;
        private string _var12;
        private ToolTip _var13;
        private bool _var14;
        private bool _var15;
        private bool _var5;
        private Color _var6;
        private Color _var7;
        private FlatStyle _var8;
        private Image _var9;

        public CtlInternal(string var16, string var17, Color var20)
        {
            base.Name = var16;
            base.Size = new Size(0x18, 0x18);
            this.BackColor = var20;
            this.var19();
            this.mtd6 = var17;
        }

        public CtlInternal(string var16, string var17, Size var18)
        {
            base.Name = var16;
            base.Size = var18;
            this.var19();
            this.mtd6 = var17;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            this._var0 = var0.var3;
            base.Invalidate();
            base.OnMouseDown(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            this._var0 = var0.var1;
            base.Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (this._var0 != var0.var3)
            {
                this._var0 = var0.var2;
            }
            base.Invalidate();
            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            this._var0 = var0.var4;
            base.OnMouseUp(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            if (this._var8 == FlatStyle.Standard)
            {
                if (this._var0 == var0.var2)
                {
                    using (Brush brush = new SolidBrush(this._var6))
                    {
                        graphics.FillRectangle(brush, base.ClientRectangle);
                    }
                    ControlPaint.DrawBorder(graphics, base.ClientRectangle, SystemColors.Highlight, ButtonBorderStyle.Solid);
                }
                else if (this._var0 == var0.var3)
                {
                    using (Brush brush2 = new SolidBrush(this._var7))
                    {
                        graphics.FillRectangle(brush2, base.ClientRectangle);
                    }
                    ControlPaint.DrawBorder(graphics, base.ClientRectangle, SystemColors.ControlDarkDark, ButtonBorderStyle.Solid);
                }
                else
                {
                    ControlPaint.DrawBorder(graphics, base.ClientRectangle, SystemColors.ControlDark, ButtonBorderStyle.Solid);
                }
                if (this._var14)
                {
                    graphics.DrawImage(this._var9, this._var10);
                }
            }
            else if (this._var8 == FlatStyle.Flat)
            {
                if (this._var0 == var0.var2)
                {
                    using (Brush brush3 = new SolidBrush(this._var6))
                    {
                        graphics.FillRectangle(brush3, base.ClientRectangle);
                    }
                    if (this._var14)
                    {
                        this.var22(graphics, this._var9, (float) (this._var10.X + 1), (float) (this._var10.Y + 1));
                        graphics.DrawImage(this._var9, (int) (this._var10.X - 1), (int) (this._var10.Y - 1));
                    }
                    ControlPaint.DrawBorder(graphics, base.ClientRectangle, SystemColors.Highlight, ButtonBorderStyle.Solid);
                }
                else if (this._var0 == var0.var3)
                {
                    using (Brush brush4 = new SolidBrush(this._var7))
                    {
                        graphics.FillRectangle(brush4, base.ClientRectangle);
                    }
                    if (this._var14)
                    {
                        graphics.DrawImage(this._var9, this._var10);
                    }
                    ControlPaint.DrawBorder(graphics, base.ClientRectangle, SystemColors.ControlDark, ButtonBorderStyle.Solid);
                }
                else
                {
                    if (this._var14)
                    {
                        graphics.DrawImage(this._var9, this._var10);
                    }
                    if (this._var8 == FlatStyle.Standard)
                    {
                        ControlPaint.DrawBorder(graphics, base.ClientRectangle, SystemColors.ControlDark, ButtonBorderStyle.Solid);
                    }
                }
            }
            else if (this._var8 == FlatStyle.Popup)
            {
                if (this._var0 == var0.var2)
                {
                    ControlPaint.DrawBorder(graphics, base.ClientRectangle, SystemColors.ControlLightLight, 1, ButtonBorderStyle.Solid, SystemColors.ControlLightLight, 1, ButtonBorderStyle.Solid, SystemColors.ControlDark, 1, ButtonBorderStyle.Solid, SystemColors.ControlDark, 1, ButtonBorderStyle.Solid);
                }
                else if (this._var0 == var0.var3)
                {
                    ControlPaint.DrawBorder(graphics, base.ClientRectangle, SystemColors.ControlDark, 1, ButtonBorderStyle.Solid, SystemColors.ControlDark, 1, ButtonBorderStyle.Solid, SystemColors.ControlLightLight, 1, ButtonBorderStyle.Solid, SystemColors.ControlLightLight, 1, ButtonBorderStyle.Solid);
                }
                graphics.DrawImage(this._var9, this._var10);
            }
            else if (this._var8 == FlatStyle.System)
            {
                if (!this._var5)
                {
                    if (this._var0 == var0.var2)
                    {
                        ControlPaint.DrawBorder(graphics, base.ClientRectangle, SystemColors.ControlLightLight, 1, ButtonBorderStyle.Solid, SystemColors.ControlLightLight, 1, ButtonBorderStyle.Solid, SystemColors.ControlDark, 1, ButtonBorderStyle.Solid, SystemColors.ControlDark, 1, ButtonBorderStyle.Solid);
                    }
                    else if (this._var0 == var0.var3)
                    {
                        ControlPaint.DrawBorder(graphics, base.ClientRectangle, SystemColors.ControlDark, 1, ButtonBorderStyle.Solid, SystemColors.ControlDark, 1, ButtonBorderStyle.Solid, SystemColors.ControlLightLight, 1, ButtonBorderStyle.Solid, SystemColors.ControlLightLight, 1, ButtonBorderStyle.Solid);
                    }
                }
                else
                {
                    ControlPaint.DrawBorder(graphics, base.ClientRectangle, SystemColors.ControlDark, 1, ButtonBorderStyle.Solid, SystemColors.ControlDark, 1, ButtonBorderStyle.Solid, SystemColors.ControlLightLight, 1, ButtonBorderStyle.Solid, SystemColors.ControlLightLight, 1, ButtonBorderStyle.Solid);
                }
                graphics.DrawImage(this._var9, this._var10);
            }
            if (this._var15)
            {
                graphics.DrawString(this.Text, this.Font, SystemBrushes.ControlText, this._var11.X, this._var11.Y);
            }
            base.OnPaint(e);
        }

        private void var19()
        {
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.UserPaint, true);
            this._var13 = new ToolTip();
            this._var13.AutomaticDelay = 250;
            this._var6 = Color.FromArgb(0xb5, 190, 0xd6);
            this._var7 = Color.FromArgb(130, 0x91, 0xb9);
            this._var0 = var0.var1;
            this._var8 = FlatStyle.Popup;
            this._var10 = new Point();
            this._var11 = new PointF();
            this._var14 = false;
            this._var5 = false;
        }

        private void var21()
        {
            if (this._var15 & this._var14)
            {
                this._var10.X = 5;
                this._var10.Y = 5;
                this._var11.X = (this._var10.X + this._var9.Width) + 2;
                this._var11.Y = 8f;
            }
            else if (!this._var15 & this._var14)
            {
                if (this._var9.Width <= base.Width)
                {
                    this._var10.X = (base.Width - this._var9.Width) / 2;
                }
                else
                {
                    this._var10.X = 0;
                }
                if (this._var9.Height <= base.Height)
                {
                    this._var10.Y = (base.Height - this._var9.Height) / 2;
                }
                else
                {
                    this._var10.Y = 0;
                }
            }
            else if (this._var15)
            {
                using (StringFormat format = ((StringFormat) StringFormat.GenericTypographic.Clone()))
                {
                    format.Alignment = StringAlignment.Near;
                    format.LineAlignment = StringAlignment.Near;
                    using (Graphics graphics = Graphics.FromHwnd(new IntPtr(0)))
                    {
                        SizeF ef = graphics.MeasureString(this.Text, this.Font, base.Width, format);
                        this._var11.X = ((base.Width - ef.Width) / 2f) - 3f;
                        this._var11.Y = (base.Height - ef.Height) / 2f;
                    }
                }
            }
        }

        private void var22(Graphics var23, Image var24, float var25, float var26)
        {
            ImageAttributes imageAttr = new ImageAttributes();
            ColorMatrix newColorMatrix = new ColorMatrix();
            newColorMatrix.Matrix00 = 0f;
            newColorMatrix.Matrix11 = 0f;
            newColorMatrix.Matrix22 = 0f;
            newColorMatrix.Matrix33 = 0.3f;
            imageAttr.SetColorMatrix(newColorMatrix);
            var23.DrawImage(var24, new Rectangle((int) var25, (int) var26, var24.Width, var24.Height), 0, 0, var24.Width, var24.Height, GraphicsUnit.Pixel, imageAttr);
        }

        public string mtd6
        {
            get
            {
                return this._var12;
            }
            set
            {
                this._var12 = value;
                if ((this._var12 != string.Empty) && (this._var12 != ""))
                {
                    this._var13.SetToolTip(this, this._var12);
                }
            }
        }

        public bool mtd7
        {
            get
            {
                return this._var5;
            }
            set
            {
                this._var5 = value;
            }
        }

        public FlatStyle mtd8
        {
            set
            {
                this._var8 = value;
            }
        }

        public Image mtd9
        {
            set
            {
                this._var9 = value;
                if (this._var9 != null)
                {
                    this._var14 = true;
                    this.var21();
                }
                else
                {
                    this._var14 = false;
                }
            }
        }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                if ((this.Text != string.Empty) | (this.Text != ""))
                {
                    this._var15 = true;
                    this.var21();
                }
                else
                {
                    this._var15 = false;
                }
            }
        }

        private enum var0
        {
            var1,
            var2,
            var3,
            var4
        }
    }
}

