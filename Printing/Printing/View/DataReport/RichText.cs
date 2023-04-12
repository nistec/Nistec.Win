namespace Nistec.Printing.View
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    [ToolboxItemFilter("Nistec.Printing.View", ToolboxItemFilterType.Require)]
    public class McRichText : McReportControl
    {
        private string _var0;
        private bool _var1;
        private RichTextBox _var2;
        private RTFStreamer _var3;
        private bool _var4;

        public McRichText()
        {
            this.var5();
        }

        public McRichText(string name)
        {
            base._mtd91 = name;
            this.var5();
        }

        public override void DrawCtl(object sender, PaintEventArgs e)
        {
            base.DrawCtl(sender, e);
            mtd10.mtd18(e.Graphics, this._var2.Handle, this._var2.BackColor, base._mtd99, this.Bounds, base.DesignMode, this._var4);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._var2.Dispose();
                this._var2 = null;
                this._var0 = null;
                this._var3 = null;
            }
            base.Dispose(disposing);
        }

        public int Find(string text, RichTextBoxFinds option)
        {
            return this._var2.Find(text, option);
        }

        public bool ShouldSerializeBackColor()
        {
            return (this.BackColor != SystemColors.Window);
        }

        public bool ShouldSerializeForeColor()
        {
            return (this.ForeColor != SystemColors.ControlText);
        }

        public bool ShouldSerializeTextFont()
        {
            return (this.TextFont != null);
        }

        private void var5()
        {
            base._mtd66 = ControlType.RichTextField;
            this._var3 = new RTFStreamer();
            this._var1 = false;
            this._var2 = new RichTextBox();
            this._var2.Site = this.Site;
            this._var4 = false;
        }

        [mtd85(mtd88.mtd86), Category("Appearance"), Description("The backcolor used to display text and graphics in the control")]
        public Color BackColor
        {
            get
            {
                return this._var2.BackColor;
            }
            set
            {
                if (this._var2.BackColor != value)
                {
                    if (value == Color.Transparent)
                    {
                        throw new ArgumentException("This control does not support transparent background colors.");
                    }
                    mtd69.mtd70(mtd26.BackColor, mtd56.mtd59, this._var2.BackColor, value);
                    this._var2.BackColor = value;
                    base.mtd93(this);
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int BulletIndent
        {
            get
            {
                return this._var2.BulletIndent;
            }
            set
            {
                this._var2.BulletIndent = value;
            }
        }

        [DefaultValue(false), mtd85(mtd88.mtd86), Category("Data"), Description("Indicates whether or not the control is bound to data")]
        public bool DataBound
        {
            get
            {
                return this._var1;
            }
            set
            {
                this._var1 = value;
            }
        }

        [Category("Appearance"), Description("The Forecolor used to display text and graphics in the control"), mtd85(mtd88.mtd86)]
        public Color ForeColor
        {
            get
            {
                return this._var2.ForeColor;
            }
            set
            {
                if (this._var2.ForeColor != value)
                {
                    mtd69.mtd70(mtd26.ForeColor, mtd56.mtd59, this._var2.ForeColor, value);
                    this._var2.ForeColor = value;
                    base.mtd93(this);
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int MaxLength
        {
            get
            {
                return this._var2.MaxLength;
            }
            set
            {
                this._var2.MaxLength = value;
            }
        }

        [Description("Controls whether text of the control can span more than one line"), mtd85(mtd88.mtd86), Category("Format Text"), DefaultValue(true)]
        public bool MultiLine
        {
            get
            {
                return this._var2.Multiline;
            }
            set
            {
                this._var2.Multiline = value;
            }
        }

        [mtd85(mtd88.mtd86), Description("Indicates whether or not the text is rendered right to left."), Category("Format Text"), DefaultValue(false)]
        public bool RightToLeft
        {
            get
            {
                return this._var4;
            }
            set
            {
                this._var4 = value;
            }
        }

        [mtd85(mtd88.mtd86), TypeConverter(typeof(mtd72)), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public string RTF
        {
            get
            {
                return this._var2.Rtf;
            }
            set
            {
                if (this._var0 != value)
                {
                    mtd69.mtd70(mtd26.mtd52, mtd56.mtd64, this._var0, value);
                    this._var2.Rtf = value;
                    this._var3.mtd52 = value;
                    base.mtd93(this);
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced), Browsable(false), DefaultValue((string) null)]
        public RTFStreamer RTFStream
        {
            get
            {
                return this._var3;
            }
            set
            {
                if (value != null)
                {
                    this._var3 = value;
                    this._var2.Rtf = value.mtd52;
                }
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SelectedText
        {
            get
            {
                return this._var2.SelectedText;
            }
            set
            {
                this._var2.SelectedText = value;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public HorizontalAlignment SelectionAlignment
        {
            get
            {
                return this._var2.SelectionAlignment;
            }
            set
            {
                this._var2.SelectionAlignment = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public bool SelectionBullet
        {
            get
            {
                return this._var2.SelectionBullet;
            }
            set
            {
                this._var2.SelectionBullet = value;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionCharOffset
        {
            get
            {
                return this._var2.SelectionCharOffset;
            }
            set
            {
                this._var2.SelectionCharOffset = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public Color SelectionColor
        {
            get
            {
                return this._var2.SelectionColor;
            }
            set
            {
                this._var2.SelectionColor = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public Font SelectionFont
        {
            get
            {
                return this._var2.SelectionFont;
            }
            set
            {
                this._var2.SelectionFont = value;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionHangingIndent
        {
            get
            {
                return this._var2.SelectionHangingIndent;
            }
            set
            {
                this._var2.SelectionHangingIndent = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int SelectionIndent
        {
            get
            {
                return this._var2.SelectionIndent;
            }
            set
            {
                this._var2.SelectionIndent = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int SelectionLength
        {
            get
            {
                return this._var2.SelectionLength;
            }
            set
            {
                this._var2.SelectionLength = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public bool SelectionProtected
        {
            get
            {
                return this._var2.SelectionProtected;
            }
            set
            {
                this._var2.SelectionProtected = value;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionRightIndent
        {
            get
            {
                return this._var2.SelectionRightIndent;
            }
            set
            {
                this._var2.SelectionRightIndent = value;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionStart
        {
            get
            {
                return this._var2.SelectionStart;
            }
            set
            {
                this._var2.SelectionStart = value;
            }
        }

        [mtd85(mtd88.mtd86), Description("Font used to display the text in the control"), Category("Format Text")]
        public Font TextFont
        {
            get
            {
                return this._var2.Font;
            }
            set
            {
                this._var2.Font = value;
            }
        }
    }
}

