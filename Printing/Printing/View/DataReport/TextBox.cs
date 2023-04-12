namespace Nistec.Printing.View
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Windows.Forms;

    [ToolboxItemFilter("Nistec.Printing.View", ToolboxItemFilterType.Require)]
    public class McTextBox : McReportControl
    {
        private Color _var0;
        private Color _var1;
        private Nistec.Printing.View.SummaryRunning _var10;
        private string _var11;
        private string _var12;
        private object _var13;
        private McField _var14;
        private bool _var15;
        private bool _var2;
        private bool _var3;
        private Font _var4;
        private ContentAlignment _var5;
        private bool _var6;
        private StringFormat _var7;
        private string _var8;
        private AggregateType _var9;

        public McTextBox()
        {
            this.var16();
        }

        public McTextBox(string name)
        {
            base._mtd91 = name;
            this.var16();
        }

        public override void DrawCtl(object sender, PaintEventArgs e)
        {
            string name;
            Graphics graphics = e.Graphics;
            base.DrawCtl(sender, e);
            if ((this._var12 == null) | (this._var12 == ""))
            {
                name = base.Name;
            }
            else
            {
                name = this._var12;
            }
            mtd10.mtd12(ref graphics, this.Bounds, this._var0, name, this._var1, ref this._var4, this._var7, ref this._mtd99);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this._var4 != null)
                {
                    this._var4.Dispose();
                }
                if (this._var7 != null)
                {
                    this._var7.Dispose();
                }
                this._var13 = null;
                this._var14 = null;
            }
            base.Dispose(disposing);
        }

        public bool ShouldSerializeBackColor()
        {
            return (this.BackColor != Color.Transparent);
        }

        public bool ShouldSerializeDistinctField()
        {
            return (this.DistinctField != null);
        }

        public bool ShouldSerializeForeColor()
        {
            return (this.ForeColor != Color.Black);
        }

        public bool ShouldSerializeOutputFormat()
        {
            return (this.OutputFormat != null);
        }

        public bool ShouldSerializeSummaryFunc()
        {
            return (this.SummaryFunc != AggregateType.None);
        }

        public bool ShouldSerializeSummaryRunning()
        {
            return (this.SummaryRunning != Nistec.Printing.View.SummaryRunning.None);
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

        private void var16()
        {
            base._mtd66 = ControlType.TextBox;
            this._var15 = false;
            this._var0 = Color.Transparent;
            this._var1 = Color.Black;
            this._var2 = true;
            this._var3 = false;
            this._var9 = AggregateType.None;
            this._var10 = Nistec.Printing.View.SummaryRunning.None;
            this._var4 = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);
            this._var5 = ContentAlignment.TopLeft;
            this._var6 = true;
            this._var7 = (StringFormat) StringFormat.GenericTypographic.Clone();
            this._var7.LineAlignment = StringAlignment.Near;
            this._var7.Alignment = StringAlignment.Near;
            this._var7.FormatFlags = StringFormatFlags.LineLimit;
        }

        private void var17()
        {
            if (this._var5 == ContentAlignment.TopLeft)
            {
                this._var7.LineAlignment = StringAlignment.Near;
                this._var7.Alignment = StringAlignment.Near;
            }
            else if (this._var5 == ContentAlignment.TopCenter)
            {
                this._var7.LineAlignment = StringAlignment.Near;
                this._var7.Alignment = StringAlignment.Center;
            }
            else if (this._var5 == ContentAlignment.TopRight)
            {
                this._var7.LineAlignment = StringAlignment.Near;
                this._var7.Alignment = StringAlignment.Far;
            }
            else if (this._var5 == ContentAlignment.MiddleLeft)
            {
                this._var7.LineAlignment = StringAlignment.Center;
                this._var7.Alignment = StringAlignment.Near;
            }
            else if (this._var5 == ContentAlignment.MiddleCenter)
            {
                this._var7.LineAlignment = StringAlignment.Center;
                this._var7.Alignment = StringAlignment.Center;
            }
            else if (this._var5 == ContentAlignment.MiddleRight)
            {
                this._var7.LineAlignment = StringAlignment.Center;
                this._var7.Alignment = StringAlignment.Far;
            }
            else if (this._var5 == ContentAlignment.BottomLeft)
            {
                this._var7.LineAlignment = StringAlignment.Far;
                this._var7.Alignment = StringAlignment.Near;
            }
            else if (this._var5 == ContentAlignment.BottomCenter)
            {
                this._var7.LineAlignment = StringAlignment.Far;
                this._var7.Alignment = StringAlignment.Center;
            }
            else if (this._var5 == ContentAlignment.BottomRight)
            {
                this._var7.LineAlignment = StringAlignment.Far;
                this._var7.Alignment = StringAlignment.Far;
            }
        }

        private void var18()
        {
            if (this._var6)
            {
                this._var7.FormatFlags = StringFormatFlags.LineLimit;
            }
            else
            {
                this._var7.FormatFlags = StringFormatFlags.NoWrap;
            }
            if (this._var15)
            {
                this._var7.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
            }
        }

        [Category("Appearance"), mtd85(mtd88.mtd86), Description("The backcolor used to display text and graphics in the control")]
        public Color BackColor
        {
            get
            {
                return this._var0;
            }
            set
            {
                if (this._var0 != value)
                {
                    mtd69.mtd70(mtd26.BackColor, mtd56.mtd59, this._var0, value);
                    this._var0 = value;
                    base.mtd93(this);
                }
            }
        }

        [DefaultValue(true), mtd85(mtd88.mtd86), Category("Appearance"), Description("Indicates whether or not the control can grow to accomodate text")]
        public bool CanGrow
        {
            get
            {
                return this._var2;
            }
            set
            {
                this._var2 = value;
            }
        }

        [Category("Appearance"), mtd85(mtd88.mtd86), DefaultValue(false), Description("Indicates whether or not the control can shrink to accomodate text")]
        public bool CanShrink
        {
            get
            {
                return this._var3;
            }
            set
            {
                this._var3 = value;
            }
        }

        [mtd85(mtd88.mtd86), Category("Data"), Description("Name of the data field used in a distinct summary function")]
        public string DistinctField
        {
            get
            {
                return this._var11;
            }
            set
            {
                this._var11 = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public McField Field
        {
            set
            {
                this._var14 = value;
            }
        }

        [Category("Appearance"), mtd85(mtd88.mtd86), Description("The forecolor used to display text and graphics in the control")]
        public Color ForeColor
        {
            get
            {
                return this._var1;
            }
            set
            {
                if (this._var1 != value)
                {
                    mtd69.mtd70(mtd26.ForeColor, mtd56.mtd59, this._var1, value);
                    this._var1 = value;
                    base.mtd93(this);
                }
            }
        }

        [Editor(typeof(mtd80), typeof(UITypeEditor)), Description("Output Format to be applied for text of the control"), mtd85(mtd88.mtd86), Category("Format Text")]
        public string OutputFormat
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

        [Category("Format Text"), DefaultValue(false), mtd85(mtd88.mtd86), Description("Indicates whether or not the text is rendered right to left.")]
        public bool RightToLeft
        {
            get
            {
                return this._var15;
            }
            set
            {
                if (this._var15 != value)
                {
                    mtd69.mtd70(mtd26.RightToLeft, mtd56.mtd58, this._var15, value);
                    this._var15 = value;
                    this.var18();
                    base.mtd93(this);
                }
            }
        }

        [Category("Data Summary"), mtd85(mtd88.mtd86), Description("Specifies the type of summarization to be performed on the field")]
        public AggregateType SummaryFunc
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

        [Description("Indicates whether the data field summary value will be accumulated or reset for each level(detail, group or page)"), Category("Data Summary"), mtd85(mtd88.mtd86)]
        public Nistec.Printing.View.SummaryRunning SummaryRunning
        {
            get
            {
                return this._var10;
            }
            set
            {
                this._var10 = value;
            }
        }

        [Category("Data"), mtd85(mtd88.mtd86), Description("Formatted text value to be rendered in the control")]
        public string Text
        {
            get
            {
                return this._var12;
            }
            set
            {
                this._var12 = value;
            }
        }

        [mtd85(mtd88.mtd86), Category("Format Text"), Description("Determines the position of the text within the control")]
        public ContentAlignment TextAlign
        {
            get
            {
                return this._var5;
            }
            set
            {
                if (this._var5 != value)
                {
                    mtd69.mtd70(mtd26.ContentAlignment, mtd56.mtd58, this._var5, value);
                    this._var5 = value;
                    this.var17();
                    base.mtd93(this);
                }
            }
        }

        [Category("Format Text"), mtd85(mtd88.mtd86), Description("Font used to display the text in the control")]
        public Font TextFont
        {
            get
            {
                return this._var4;
            }
            set
            {
                if (this._var4 != value)
                {
                    mtd69.mtd70(mtd26.mtd34, mtd56.mtd58, this._var4, value);
                    this._var4 = value;
                    base.mtd93(this);
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public object Value
        {
            get
            {
                if (this._var14 != null)
                {
                    return this._var14.Value;
                }
                return this._var13;
            }
            set
            {
                if (this._var14 != null)
                {
                    this._var14.Value = value;
                }
                else
                {
                    this._var13 = value;
                }
            }
        }

        [Description("Indicates if lines are automatically word-wrapped for multi-line"), mtd85(mtd88.mtd86), DefaultValue(true), Category("Format Text")]
        public bool WordWrap
        {
            get
            {
                return this._var6;
            }
            set
            {
                if (this._var6 != value)
                {
                    mtd69.mtd70(mtd26.LineLimit, mtd56.mtd58, this._var6, value);
                    this._var6 = value;
                    this.var18();
                    base.mtd93(this);
                }
            }
        }
    }
}

