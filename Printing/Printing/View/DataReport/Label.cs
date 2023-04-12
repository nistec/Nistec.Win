namespace Nistec.Printing.View
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    [ToolboxItemFilter("Nistec.Printing.View", ToolboxItemFilterType.Require)]
    public class McLabel : McReportControl
    {
        private string _var0;
        private Color _BackColor;
        private Color _ForeColor;
        private Font _var3;
        private ContentAlignment _var4;
        private bool _var5;
        private StringFormat _var6;
        private bool _var7;

        public McLabel()
        {
            this.var8();
        }

        public McLabel(string name)
        {
            base._mtd91 = name;
            this.var8();
        }

        public override void DrawCtl(object sender, PaintEventArgs e)
        {
            string name;
            Graphics graphics = e.Graphics;
            base.DrawCtl(sender, e);
            if ((this._var0 == null) || (this._var0 == ""))
            {
                name = base.Name;
            }
            else
            {
                name = this._var0;
            }
            mtd10.mtd12(ref graphics, this.Bounds, this._BackColor, name, this._ForeColor, ref this._var3, this._var6, ref this._mtd99);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this._var3 != null)
                {
                    this._var3.Dispose();
                }
                if (this._var6 != null)
                {
                    this._var6.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        public bool ShouldSerializeBackColor()
        {
            return (this.BackColor != Color.Transparent);
        }

        public bool ShouldSerializeText()
        {
            return (this.Text != null);
        }

        public bool ShouldSerializeTextAlign()
        {
            return (this.TextAlign != ContentAlignment.TopLeft);
        }

        public bool ShouldSerializeTextFont()
        {
            return McReportControl.IsDefaultFont(this.TextFont);
        }

        public bool ShoulSerializeForeColor()
        {
            return (this.ForeColor != Color.Black);
        }

        private void var10()
        {
            if (this._var5)
            {
                this._var6.FormatFlags = StringFormatFlags.LineLimit;
            }
            else
            {
                this._var6.FormatFlags = StringFormatFlags.NoWrap;
            }
            if (this._var7)
            {
                this._var6.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
            }
        }

        private void var8()
        {
            base._mtd66 = ControlType.Label;
            this._var7 = false;
            this._BackColor = Color.Transparent;
            this._ForeColor = Color.Black;
            this._var3 = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);
            this._var4 = ContentAlignment.TopLeft;
            this._var5 = true;
            this._var6 = (StringFormat) StringFormat.GenericTypographic.Clone();
            this._var6.LineAlignment = StringAlignment.Near;
            this._var6.Alignment = StringAlignment.Near;
            this._var6.FormatFlags = StringFormatFlags.LineLimit;
        }

        private void var9()
        {
            if (this._var4 == ContentAlignment.TopLeft)
            {
                this._var6.LineAlignment = StringAlignment.Near;
                this._var6.Alignment = StringAlignment.Near;
            }
            else if (this._var4 == ContentAlignment.TopCenter)
            {
                this._var6.LineAlignment = StringAlignment.Near;
                this._var6.Alignment = StringAlignment.Center;
            }
            else if (this._var4 == ContentAlignment.TopRight)
            {
                this._var6.LineAlignment = StringAlignment.Near;
                this._var6.Alignment = StringAlignment.Far;
            }
            else if (this._var4 == ContentAlignment.MiddleLeft)
            {
                this._var6.LineAlignment = StringAlignment.Center;
                this._var6.Alignment = StringAlignment.Near;
            }
            else if (this._var4 == ContentAlignment.MiddleCenter)
            {
                this._var6.LineAlignment = StringAlignment.Center;
                this._var6.Alignment = StringAlignment.Center;
            }
            else if (this._var4 == ContentAlignment.MiddleRight)
            {
                this._var6.LineAlignment = StringAlignment.Center;
                this._var6.Alignment = StringAlignment.Far;
            }
            else if (this._var4 == ContentAlignment.BottomLeft)
            {
                this._var6.LineAlignment = StringAlignment.Far;
                this._var6.Alignment = StringAlignment.Near;
            }
            else if (this._var4 == ContentAlignment.BottomCenter)
            {
                this._var6.LineAlignment = StringAlignment.Far;
                this._var6.Alignment = StringAlignment.Center;
            }
            else if (this._var4 == ContentAlignment.BottomRight)
            {
                this._var6.LineAlignment = StringAlignment.Far;
                this._var6.Alignment = StringAlignment.Far;
            }
        }

        [Description("The backcolor used to display text and graphics in the control"), Category("Appearance"), mtd85(mtd88.mtd86)]
        public Color BackColor
        {
            get
            {
                return this._BackColor;
            }
            set
            {
                if (this._BackColor != value)
                {
                    mtd69.mtd70(mtd26.BackColor, mtd56.mtd59, this._BackColor, value);
                    this._BackColor = value;
                    base.mtd93(this);
                }
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

        [Category("Appearance"), Description("The forecolor used to display text and graphics in the control"), mtd85(mtd88.mtd86)]
        public Color ForeColor
        {
            get
            {
                return this._ForeColor;
            }
            set
            {
                if (this._ForeColor != value)
                {
                    mtd69.mtd70(mtd26.ForeColor, mtd56.mtd59, this._ForeColor, value);
                    this._ForeColor = value;
                    base.mtd93(this);
                }
            }
        }

        [Description("Indicates whether or not the text is rendered right to left."), mtd85(mtd88.mtd86), Category("Format Text"), DefaultValue(false)]
        public bool RightToLeft
        {
            get
            {
                return this._var7;
            }
            set
            {
                if (this._var7 != value)
                {
                    mtd69.mtd70(mtd26.RightToLeft, mtd56.mtd58, this._var7, value);
                    this._var7 = value;
                    this.var10();
                    base.mtd93(this);
                }
            }
        }

        [mtd85(mtd88.mtd86), Category("Design"), Description("Text value to be rendered in the control")]
        public string Text
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

        [mtd85(mtd88.mtd86), Category("Format Text"), Description("Determines the position of the text within the control")]
        public ContentAlignment TextAlign
        {
            get
            {
                return this._var4;
            }
            set
            {
                if (this._var4 != value)
                {
                    mtd69.mtd70(mtd26.ContentAlignment, mtd56.mtd58, this._var4, value);
                    this._var4 = value;
                    this.var9();
                    base.mtd93(this);
                }
            }
        }

        [mtd85(mtd88.mtd86), Category("Format Text"), Description("Font used to display the text in the control")]
        public Font TextFont
        {
            get
            {
                return this._var3;
            }
            set
            {
                if (this._var3 != value)
                {
                    mtd69.mtd70(mtd26.mtd34, mtd56.mtd58, this._var3, value);
                    this._var3 = value;
                    base.mtd93(this);
                }
            }
        }

        [DefaultValue(true), mtd85(mtd88.mtd86), Category("Format Text"), Description("Indicates if lines are automatically word-wrapped for multi-line")]
        public bool WordWrap
        {
            get
            {
                return this._var5;
            }
            set
            {
                if (this._var5 != value)
                {
                    mtd69.mtd70(mtd26.LineLimit, mtd56.mtd58, this._var5, value);
                    this._var5 = value;
                    this.var10();
                    base.mtd93(this);
                }
            }
        }
    }
}

