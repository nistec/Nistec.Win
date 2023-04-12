namespace MControl.Printing.View.Design
{
    using MControl.Printing.View;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    
    //mtd590
    internal class ReportExplorer  : ContainerControl
    {
        private var0 _var0;
        private PaneBase _var13;
        private PaneBase _var14;
        private PaneBase _var15;
        private CtlInternal _var16;
        private CtlInternal _var17;
        private ImageList _var3;
        private ImageList _var4;
        private StringFormat _var5;
        private Rectangle _var6;
        private Rectangle _var7;
        private CaptionButton _var8;
        private int _var9;
        internal TreeView mtd594;
        internal TreeView mtd595;

        internal event CaptionDownHandler mtd583;

        internal event TreeViewEventHandler mtd591;

        //internal event TreeViewEventHandler mtd592;

        internal event ItemDragEventHandler mtd593;

        internal ReportExplorer(ImageList var10, ImageList var11)
        {
            base.Name = "ReportExplorer";
            this._var5 = (StringFormat) StringFormat.GenericTypographic.Clone();
            this._var6 = new Rectangle(4, 0, 0x69, 20);
            this._var7 = new Rectangle(this._var6.Right, 0, 0x55, 20);
            this._var0 = var0.var1;
            this._var8 = CaptionButton.Maximize;
            this._var3 = var10;
            this._var4 = var11;
            this._var5.FormatFlags = StringFormatFlags.NoClip;
            this._var5.FormatFlags = StringFormatFlags.NoWrap;
            this._var5.Trimming = StringTrimming.EllipsisCharacter;
            this._var5.Alignment = StringAlignment.Near;
            this.var12();
        }

        internal void mtd587()
        {
            this._var8 = CaptionButton.Maximize;
            this._var17.mtd9 = this._var4.Images[2];
            this._var17.mtd6 = "Minimize";
            base.Height = this._var9;
            this._var13.Show();
            this._var15.Show();
            if (!base.Visible)
            {
                base.Show();
            }
        }

        internal void mtd588()
        {
            this._var17.Hide();
        }

        internal void mtd589()
        {
            this._var17.Show();
        }

        private void var12()
        {
            this._var13 = new PaneBase();
            this._var14 = new PaneBase();
            this._var15 = new PaneBase();
            this.mtd594 = new TreeView();
            this.mtd595 = new TreeView();
            this._var14.SuspendLayout();
            this._var13.SuspendLayout();
            this.mtd594.SuspendLayout();
            this.mtd595.SuspendLayout();
            base.SuspendLayout();
            this._var16 = new CtlInternal("Hide", "Close", new Size(14, 14));
            this._var16.mtd9 = this._var4.Images[0];
            this._var16.Dock = DockStyle.Right;
            this._var16.MouseDown += new MouseEventHandler(this.var18);
            this._var17 = new CtlInternal("Hide", "Minimize", new Size(14, 14));
            this._var17.mtd9 = this._var4.Images[2];
            this._var17.Dock = DockStyle.Right;
            this._var17.MouseDown += new MouseEventHandler(this.var19);
            this._var14.Dock = DockStyle.Top;
            this._var14.Name = "TopBand";
            this._var14.Size = new Size(200, 0x12);
            this._var14.BackColor = SystemColors.ActiveCaption;
            this._var14.DockPadding.Top = 2;
            this._var14.DockPadding.Bottom = 2;
            this._var14.DockPadding.Right = 4;
            this._var14.Paint += new PaintEventHandler(this.var20);
            this._var14.Controls.AddRange(new Control[] { this._var17, this._var16 });
            this._var13.Dock = DockStyle.Bottom;
            this._var13.Name = "Toggle";
            this._var13.Size = new Size(200, 0x18);
            this._var13.BackColor = Color.FromArgb(0xf7, 0xf3, 0xe9);
            this._var13.MouseDown += new MouseEventHandler(this.var21);
            this._var13.Paint += new PaintEventHandler(this.var22);
            this._var15.Dock = DockStyle.Fill;
            this._var15.Name = "PanelTreeView";
            this._var15.DockPadding.Top = 4;
            this._var15.DockPadding.Bottom = 4;
            this._var15.DockPadding.Left = 1;
            this._var15.DockPadding.Right = 1;
            this._var15.Paint += new PaintEventHandler(this.var23);
            this.mtd594.BorderStyle = BorderStyle.None;
            this.mtd594.ImageList = this._var3;
            this.mtd594.Size = new Size(200, 0x98);
            this.mtd594.Dock = DockStyle.Fill;
            this.mtd594.ItemDrag += new ItemDragEventHandler(this.var24);
            this.mtd595.BorderStyle = BorderStyle.None;
            this.mtd595.ImageList = this._var3;
            this.mtd595.Size = new Size(200, 0x98);
            this.mtd595.Dock = DockStyle.Fill;
            this.mtd595.AfterSelect += new TreeViewEventHandler(this.var25);
            this._var15.Controls.AddRange(new Control[] { this.mtd594, this.mtd595 });
            base.Controls.AddRange(new Control[] { this._var15, this._var14, this._var13 });
            base.Resize += new EventHandler(this.var26);
            this._var14.ResumeLayout(false);
            this._var13.ResumeLayout(false);
            this.mtd594.ResumeLayout(false);
            this.mtd595.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void var18(object var27, MouseEventArgs e)
        {
            base.Hide();
            this._var8 = CaptionButton.Close;
            this.mtd583(CaptionButton.Close);
        }

        private void var19(object var27, MouseEventArgs e)
        {
            if (this._var8 == CaptionButton.Maximize)
            {
                this._var8 = CaptionButton.Minimize;
                this._var17.mtd9 = this._var4.Images[1];
                this._var17.mtd6 = "Maximize";
                this._var9 = base.Height;
                base.Height = this._var14.Height;
                this._var13.Hide();
                this._var15.Hide();
                this.mtd583(CaptionButton.Minimize);
            }
            else if (this._var8 == CaptionButton.Minimize)
            {
                this.mtd587();
                this.mtd583(CaptionButton.Maximize);
            }
            this._var17.Invalidate();
        }

        private void var20(object var27, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            RectangleF layoutRectangle = new RectangleF(2f, 1f, (float) (this._var14.Width - 0x20), 14f);
            if (this._var0 == var0.var1)
            {
                graphics.DrawString("Report Explorer", this.Font, SystemBrushes.ActiveCaptionText, layoutRectangle, this._var5);
            }
            else if (this._var0 == var0.var2)
            {
                graphics.DrawString("Data Fields", this.Font, SystemBrushes.ActiveCaptionText, layoutRectangle, this._var5);
            }
        }

        private void var21(object var27, MouseEventArgs e)
        {
            Point pt = new Point(e.X, e.Y);
            if (this._var6.Contains(pt))
            {
                this._var0 = var0.var1;
            }
            else if (this._var7.Contains(pt))
            {
                this._var0 = var0.var2;
            }
            this._var13.Invalidate();
            this._var14.Invalidate();
        }

        private void var22(object var27, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            RectangleF layoutRectangle = new RectangleF((float) (this._var6.X + 20), (float) (this._var6.Y + 4), (float) (this._var6.Width - 0x16), 16f);
            RectangleF ef2 = new RectangleF((float) (this._var7.X + 20), (float) (this._var7.Y + 4), (float) (this._var7.Width - 0x16), 16f);
            this.mtd595.SuspendLayout();
            this.mtd594.SuspendLayout();
            graphics.DrawLine(SystemPens.ControlDarkDark, 0, 0, this._var13.Width, 0);
            if (this._var0 == var0.var1)
            {
                this.mtd595.Visible = true;
                this.mtd594.Visible = false;
                graphics.FillRectangle(SystemBrushes.Control, this._var6);
                graphics.DrawLine(SystemPens.ControlDarkDark, this._var6.Right, this._var6.Y, this._var6.Right, this._var6.Bottom);
                graphics.DrawLine(SystemPens.ControlDarkDark, this._var6.X, this._var6.Bottom, this._var6.Right, this._var6.Bottom);
                graphics.DrawLine(SystemPens.ControlLightLight, this._var6.X - 1, this._var6.Y, this._var6.X - 1, this._var6.Bottom);
                graphics.DrawLine(SystemPens.ControlDark, this._var7.Right, this._var7.Y + 3, this._var7.Right, this._var7.Bottom - 3);
                if (layoutRectangle.Width > 0f)
                {
                    graphics.DrawString("Report Explorer", this.Font, SystemBrushes.ControlText, layoutRectangle, this._var5);
                }
                if (ef2.Width > 0f)
                {
                    graphics.DrawString("Data Fields", this.Font, SystemBrushes.FromSystemColor(SystemColors.GrayText), ef2, this._var5);
                }
            }
            else if (this._var0 == var0.var2)
            {
                this.mtd595.Visible = false;
                this.mtd594.Visible = true;
                graphics.FillRectangle(SystemBrushes.Control, this._var7);
                graphics.DrawLine(SystemPens.ControlDarkDark, this._var7.Right, this._var7.Y, this._var7.Right, this._var7.Bottom);
                graphics.DrawLine(SystemPens.ControlDarkDark, this._var7.X, this._var7.Bottom, this._var7.Right, this._var7.Bottom);
                graphics.DrawLine(SystemPens.ControlLightLight, this._var7.X - 1, this._var7.Y, this._var7.X - 1, this._var7.Bottom);
                if (ef2.Width > 0f)
                {
                    graphics.DrawString("Data Fields", this.Font, SystemBrushes.ControlText, ef2, this._var5);
                }
                if (layoutRectangle.Width > 0f)
                {
                    graphics.DrawString("Report Explorer", this.Font, SystemBrushes.FromSystemColor(SystemColors.GrayText), layoutRectangle, this._var5);
                }
            }
            if ((this._var6.X + 20) <= this._var6.Right)
            {
                graphics.DrawImage(this._var3.Images[10], (int) (this._var6.X + 2), (int) (this._var6.Y + 2));
            }
            if ((this._var7.X + 20) <= this._var7.Right)
            {
                graphics.DrawImage(this._var3.Images[15], (int) (this._var7.X + 2), (int) (this._var7.Y + 2));
            }
            this.mtd595.ResumeLayout(false);
            this.mtd594.ResumeLayout(false);
        }

        private void var23(object var27, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(SystemPens.ControlDark, 0, 3, this._var15.Width - 1, this._var15.Height - 7);
        }

        private void var24(object var27, ItemDragEventArgs e)
        {
            this.mtd593(var27, e);
        }

        private void var25(object var27, TreeViewEventArgs e)
        {
            this.mtd591(var27, e);
        }

        private void var26(object var27, EventArgs e)
        {
            this.var28();
            base.Invalidate();
        }

        private void var28()
        {
            int width = this._var13.Width;
            if (width >= 0xc6)
            {
                this._var6.Width = 0x69;
                this._var7.Width = 0x55;
                this._var7.Location = new Point(this._var6.Right, 0);
            }
            else if ((width - 0x5d) > 0x55)
            {
                this._var6.Width = width - 0x5d;
                this._var7.Location = new Point(this._var6.Right, 0);
            }
            else
            {
                this._var6.Width = (width - 8) / 2;
                this._var7.Width = this._var6.Width;
                this._var7.Location = new Point(this._var6.Right, 0);
            }
        }

        internal CaptionButton mtd586
        {
            get
            {
                return this._var8;
            }
            set
            {
                this._var8 = value;
            }
        }

        private enum var0
        {
            var1,
            var2
        }
    }
}

