namespace Nistec.Printing.View
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    [ToolboxItemFilter("Nistec.Printing.View", ToolboxItemFilterType.Require)]
    public class McPageBreak : McReportControl
    {
        private bool _var0;
        private Font _var1;

        public McPageBreak()
        {
            this.var2();
        }

        public McPageBreak(string name)
        {
            base._mtd91 = name;
            this.var2();
        }

        public override void DrawCtl(object sender, PaintEventArgs e)
        {
            this.var3(e.Graphics, (float) e.ClipRectangle.Width);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this._var1 != null))
            {
                this._var1.Dispose();
            }
            base.Dispose(disposing);
        }

        private void var2()
        {
            base._mtd66 = ControlType.PageBreak;
            this._var0 = true;
            this._var1 = new Font(new FontFamily("Microsoft Sans Serif"), 7f, FontStyle.Regular);
        }

        private void var3(Graphics g, float width)
        {
            this._mtd32.Width = width;
            using (Pen pen = new Pen(Color.Black, 1f))
            {
                pen.DashStyle = DashStyle.Dash;
                g.DrawLine(pen, 0f, this._mtd33.Y, width, this._mtd33.Y);
            }
            float x = (width / 2f) - (g.MeasureString("Page Break", this._var1).Width / 2f);
            g.DrawString("PageBreak", this._var1, SystemBrushes.ControlText, x, this._mtd33.Y - 6f);
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public override Nistec.Printing.View.Border Border
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        [mtd85(mtd88.mtd86), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public override Color BorderBottomColor
        {
            get
            {
                return Color.Transparent;
            }
            set
            {
            }
        }

        [mtd85(mtd88.mtd86), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public override BorderLineStyle BorderBottomStyle
        {
            get
            {
                return BorderLineStyle.None;
            }
            set
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), mtd85(mtd88.mtd86)]
        public override Color BorderLeftColor
        {
            get
            {
                return Color.Transparent;
            }
            set
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), mtd85(mtd88.mtd86)]
        public override BorderLineStyle BorderLeftStyle
        {
            get
            {
                return BorderLineStyle.None;
            }
            set
            {
            }
        }

        [Browsable(false), mtd85(mtd88.mtd86), EditorBrowsable(EditorBrowsableState.Never)]
        public override Color BorderRightColor
        {
            get
            {
                return Color.Transparent;
            }
            set
            {
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), mtd85(mtd88.mtd86)]
        public override BorderLineStyle BorderRightStyle
        {
            get
            {
                return BorderLineStyle.None;
            }
            set
            {
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), mtd85(mtd88.mtd86)]
        public override Color BorderTopColor
        {
            get
            {
                return Color.Transparent;
            }
            set
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), mtd85(mtd88.mtd86)]
        public override BorderLineStyle BorderTopStyle
        {
            get
            {
                return BorderLineStyle.None;
            }
            set
            {
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public override RectangleF Bounds
        {
            get
            {
                return new RectangleF(0f, this.Location.Y, this.Size.Width, 3f);
            }
        }

        [mtd85(mtd88.mtd86), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override string DataField
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        [DefaultValue(true), Category("Design"), Description("Indicates whether the PageBreak is currently enabled"), mtd85(mtd88.mtd86)]
        public bool Enabled
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

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public override float Height
        {
            get
            {
                return 0f;
            }
            set
            {
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public override float Left
        {
            get
            {
                return 0f;
            }
            set
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override PointF Location
        {
            get
            {
                return base._mtd33;
            }
            set
            {
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public override SizeF Size
        {
            get
            {
                return base._mtd32;
            }
            set
            {
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public override float Width
        {
            get
            {
                return 0f;
            }
            set
            {
            }
        }
    }
}

