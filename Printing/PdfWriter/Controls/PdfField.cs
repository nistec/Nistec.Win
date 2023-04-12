namespace MControl.Printing.Pdf.Controls
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.Controls;
    using MControl.Printing.Pdf.Core.Element;
    using MControl.Printing.Pdf.Drawing;
    using MControl.Printing.Pdf.Core.IO;
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Text;

    public abstract class PdfField : A91
    {
        private RectangleF _b0;
        private string _b1;
        private MControl.Printing.Pdf.Controls.AdditionalActions _b10;
        private string _b2;
        private string _b3;
        private PdfColor _b4;
        private PdfColor _b5;
        private PdfColor _b6;
        private FieldBorderStyle _b7;
        private Page _b8;
        private bool _b9;
        internal A127 A153;
        internal string A173;
        internal ArrayList A174;

        internal PdfField(Page page)
        {
            this._b8 = page;
            this._b4 = PdfColor.Black;
            this._b5 = PdfColor.Transparent;
            this._b6 = new RGBColor(SystemColors.Control);
            this._b7 = FieldBorderStyle.Default;
            this._b10 = new MControl.Printing.Pdf.Controls.AdditionalActions();
        }

        internal override void A110()
        {
            int num;
            A93 A1 = base._A92.A93;
            A1.A95 = (num = A1.A95) + 1;
            base._A95 = num;
            if (this._b10.Count > 0)
            {
                A93 A2 = base._A92.A93;
                A2.A95 += this._b10.Count * 2;
            }
        }

        internal static void A156(ref A120 b14, ref A112 b15, RectangleF b16, PdfColor b5, PdfColor b6, FieldBorderStyle b17, bool b18)
        {
            RectangleF ef = b19(b16, 0.5f);
            if ((b5 != null) && (b5 != PdfColor.Transparent))
            {
                GraphicsElement.A175(ef.X, ef.Y, ef.Width, ef.Height, b5, ref b14, ref b15);
            }
            if (b17 == FieldBorderStyle.Default)
            {
                if (b18)
                {
                    b20(ref b14, ref b15, ef, b6, b6.DarkenColor(0.800000011920929));
                    ef = b19(ef, 1f);
                    b20(ref b14, ref b15, ef, b6.LightenColor(0.10000000149011612), b6.DarkenColor(0.40000000596046448));
                }
                else
                {
                    b20(ref b14, ref b15, ef, b6.DarkenColor(0.5), b6.LightenColor(0.10000000149011612));
                    ef = b19(ef, 1f);
                    b20(ref b14, ref b15, ef, b6.DarkenColor(0.800000011920929), b6);
                }
            }
            else if (b17 == FieldBorderStyle.Plain)
            {
                GraphicsElement.A156(ef.X, ef.Y, ef.Width, ef.Height, new Border(1f, b6, LineStyle.Solid), ref b14, ref b15);
            }
        }

        internal string A165(int b11)
        {
            if (this._b10.Count <= 0)
            {
                return null;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("/AA << ");
            int count = this._b10.Count;
            for (int i = 0; i < count; i++)
            {
                AdditionalAction action = this._b10[i];
                if (action != null)
                {
                    builder.Append(string.Format("/{0} {1} 0 R ", action.A126(), b11));
                    b11 += 2;
                }
            }
            builder.Append(">>");
            return builder.ToString();
        }

        internal void A166(int b11, ref A55 b13)
        {
            int count = this._b10.Count;
            for (int i = 0; i < count; i++)
            {
                AdditionalAction action = this._b10[i];
                if (action != null)
                {
                    action.A54(b11, ref this._A92, ref b13);
                    b11 += 2;
                }
            }
        }

        private static RectangleF b19(RectangleF b23, float b24)
        {
            b23.X += b24 / 2f;
            b23.Y += b24 / 2f;
            b23.Height -= b24;
            b23.Width -= b24;
            return b23;
        }

        private static void b20(ref A120 b14, ref A112 b15, RectangleF b16, PdfColor b21, PdfColor b22)
        {
            b15.A176(string.Format("{0} J", A15.A18(2)));
            b15.A176(string.Format("{0} j", A15.A18(0)));
            b15.A176("10 M");
            b16.Y = b14.A98(b16.Y);
            GraphicsElement.A177(0.5f, b21, LineStyle.Solid, ref b14, ref b15);
            GraphicsElement.A178(b15, b16.X + b16.Width, b16.Y);
            GraphicsElement.A179(b15, b16.X, b16.Y);
            GraphicsElement.A179(b15, b16.X, b16.Y - b16.Height);
            GraphicsElement.A180(b15, GraphicsMode.stroke, false);
            GraphicsElement.A177(0.5f, b22, LineStyle.Solid, ref b14, ref b15);
            GraphicsElement.A178(b15, b16.X, b16.Y - b16.Height);
            GraphicsElement.A179(b15, b16.X + b16.Width, b16.Y - b16.Height);
            GraphicsElement.A179(b15, b16.X + b16.Width, b16.Y);
            GraphicsElement.A180(b15, GraphicsMode.stroke, false);
        }

        internal Page A141
        {
            get
            {
                return this._b8;
            }
        }

        internal abstract string A155 { get; }

        public MControl.Printing.Pdf.Controls.AdditionalActions AdditionalActions
        {
            get
            {
                return this._b10;
            }
        }

        public PdfColor Backcolor
        {
            get
            {
                return this._b5;
            }
            set
            {
                this._b5 = value;
            }
        }

        public virtual PdfColor BorderColor
        {
            get
            {
                return this._b6;
            }
            set
            {
                this._b6 = value;
            }
        }

        public virtual FieldBorderStyle BorderStyle
        {
            get
            {
                return this._b7;
            }
            set
            {
                this._b7 = value;
            }
        }

        public virtual RectangleF Bounds
        {
            get
            {
                return this._b0;
            }
            set
            {
                this._b0 = value;
            }
        }

        public string DefaultValue
        {
            get
            {
                if (this._b1 != null)
                {
                    return this._b1;
                }
                return string.Empty;
            }
            set
            {
                this._b1 = value;
            }
        }

        public bool Export
        {
            get
            {
                return ((this.A153 & A127.NoExport) == A127.Default);
            }
            set
            {
                if (!value)
                {
                    this.A153 |= A127.NoExport;
                }
                else
                {
                    this.A153 &= ~A127.NoExport;
                }
            }
        }

        public PdfColor Forecolor
        {
            get
            {
                return this._b4;
            }
            set
            {
                this._b4 = value;
            }
        }

        public bool ReadOnly
        {
            get
            {
                return ((this.A153 & A127.ReadOnly) != A127.Default);
            }
            set
            {
                if (value)
                {
                    this.A153 |= A127.ReadOnly;
                }
                else
                {
                    this.A153 &= ~A127.ReadOnly;
                }
            }
        }

        public bool Required
        {
            get
            {
                return ((this.A153 & A127.Required) != A127.Default);
            }
            set
            {
                if (value)
                {
                    this.A153 |= A127.Required;
                }
                else
                {
                    this.A153 &= ~A127.Required;
                }
            }
        }

        public bool RightToLeft
        {
            get
            {
                return this._b9;
            }
            set
            {
                this._b9 = value;
            }
        }

        public string ToolTip
        {
            get
            {
                if (this._b2 != null)
                {
                    return this._b2;
                }
                return string.Empty;
            }
            set
            {
                this._b2 = value;
            }
        }

        public string Value
        {
            get
            {
                if (this._b3 != null)
                {
                    return this._b3;
                }
                return string.Empty;
            }
            set
            {
                this._b3 = value;
            }
        }
    }
}

