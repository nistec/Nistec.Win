namespace MControl.Printing.View.Design
{
    using MControl.Printing.View;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    //mtd582
    internal class PropertyExplorer  : ContainerControl
    {
        private ImageList _var0;
        private StringFormat _var1;
        private CtlInternal _var10;
        private CtlInternal _var11;
        private Rectangle _var2;
        private Rectangle _var3;
        private int _var4;
        private CaptionButton _var5;
        private PaneBase _var8;
        private PropertyGrid _var9;

        internal event CaptionDownHandler mtd583;

        internal event PropertyValueChangedEventHandler mtd584;

        internal PropertyExplorer(ImageList var6)
        {
            base.Name = "PropertyExplorer";
            this._var1 = (StringFormat) StringFormat.GenericTypographic.Clone();
            this._var2 = new Rectangle(4, 0, 0x69, 20);
            this._var3 = new Rectangle(this._var2.Right, 0, 0x55, 20);
            this._var5 = CaptionButton.Maximize;
            this._var0 = var6;
            this.var7();
            this._var1.FormatFlags = StringFormatFlags.NoClip;
            this._var1.FormatFlags = StringFormatFlags.NoWrap;
            this._var1.Trimming = StringTrimming.EllipsisCharacter;
            this._var1.Alignment = StringAlignment.Near;
        }

        internal void mtd587()
        {
            this._var5 = CaptionButton.Maximize;
            this._var11.mtd9 = this._var0.Images[2];
            this._var11.mtd6 = "Minimize";
            base.Height = this._var4;
            this._var9.Show();
            if (!base.Visible)
            {
                base.Show();
            }
        }

        internal void mtd588()
        {
            this._var11.Hide();
        }

        internal void mtd589()
        {
            this._var11.Show();
        }

        private void var12(object var16, MouseEventArgs e)
        {
            base.Hide();
            this._var5 = CaptionButton.Close;
            this.mtd583(CaptionButton.Close);
        }

        private void var13(object var16, MouseEventArgs e)
        {
            if (this._var5 == CaptionButton.Maximize)
            {
                this._var5 = CaptionButton.Minimize;
                this._var11.mtd9 = this._var0.Images[1];
                this._var11.mtd6 = "Maximize";
                this._var4 = base.Height;
                this._var9.Hide();
                base.Height = this._var8.Height;
                this.mtd583(CaptionButton.Minimize);
            }
            else if (this._var5 == CaptionButton.Minimize)
            {
                this.mtd587();
                this.mtd583(CaptionButton.Maximize);
            }
            this._var11.Invalidate();
        }

        private void var14(object var16, PaintEventArgs e)
        {
            RectangleF layoutRectangle = new RectangleF(2f, 1f, (float) (this._var8.Width - 0x20), 14f);
            e.Graphics.DrawString("Properties", this.Font, SystemBrushes.ActiveCaptionText, layoutRectangle, this._var1);
        }

        private void var15(object var17, PropertyValueChangedEventArgs e)
        {
            if (this.mtd584 != null)
            {
                this.mtd584(var17, e);
            }
        }

        private void var7()
        {
            this._var8 = new PaneBase();
            this._var9 = new PropertyGrid();
            this._var8.SuspendLayout();
            this._var9.SuspendLayout();
            base.SuspendLayout();
            this._var10 = new CtlInternal("Hide", "Close", new Size(14, 14));
            this._var10.mtd9 = this._var0.Images[0];
            this._var10.Dock = DockStyle.Right;
            this._var10.MouseDown += new MouseEventHandler(this.var12);
            this._var11 = new CtlInternal("Hide", "Minimize", new Size(14, 14));
            this._var11.mtd9 = this._var0.Images[2];
            this._var11.Dock = DockStyle.Right;
            this._var11.MouseDown += new MouseEventHandler(this.var13);
            this._var8.Dock = DockStyle.Top;
            this._var8.Name = "TopBand";
            this._var8.Size = new Size(200, 0x12);
            this._var8.BackColor = SystemColors.ActiveCaption;
            this._var8.DockPadding.Top = 2;
            this._var8.DockPadding.Bottom = 2;
            this._var8.DockPadding.Right = 4;
            this._var8.Paint += new PaintEventHandler(this.var14);
            this._var8.Controls.AddRange(new Control[] { this._var11, this._var10 });
            this._var9.Dock = DockStyle.Fill;
            this._var9.Name = "Properties";
            this._var9.PropertyValueChanged += new PropertyValueChangedEventHandler(this.var15);
            base.Controls.AddRange(new Control[] { this._var9, this._var8 });
            this._var8.ResumeLayout(false);
            this._var9.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        internal PropertyGrid mtd585
        {
            get
            {
                return this._var9;
            }
            set
            {
                this._var9 = value;
            }
        }

        internal CaptionButton mtd586
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
    }
}

