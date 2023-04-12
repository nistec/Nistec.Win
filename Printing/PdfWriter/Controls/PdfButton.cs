namespace MControl.Printing.Pdf.Controls
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.Controls;
    using MControl.Printing.Pdf.Core.Element;
    using MControl.Printing.Pdf.Core.Encrypt;
    using MControl.Printing.Pdf.Drawing;
    using MControl.Printing.Pdf.Core.IO;
    using System;
    using System.Drawing;

    public class PdfButton : PdfField
    {
        private string _b0;
        private PdfFont _b1;
        private float _b2;
        private A136 _b3;

        internal PdfButton(Page page, string name, string caption, RectangleF bound, PdfFont font, float fontsize) : base(page)
        {
            base.A153 |= A127.PushButton;
            base.A153 &= ~(A127.RichText | A127.Radio | A127.NoToggleToOff);
            base._A154 = name;
            this._b0 = caption;
            this.Bounds = bound;
            this._b1 = font;
            this._b2 = fontsize;
            base.Backcolor = new RGBColor(SystemColors.Control);
        }

        internal override void A119(ref A120 b4, ref A112 b5)
        {
            base.DefaultValue = this.Caption;
            base.Value = this.Caption;
            PdfField.A156(ref b4, ref b5, this.Bounds, base.Backcolor, this.BorderColor, this.BorderStyle, true);
            float num = b4.A98(this.Bounds.Y);
            float x = this.Bounds.X;
            if (this.Caption != null)
            {
                this._b1 = GraphicsElement.A157(this._b1, this._b2, ref b4, ref b5);
                x += (this.Bounds.Width - this._b1.GetTextWidth(this.Caption, this._b2)) / 2f;
                num -= (this.Bounds.Height + this._b1.A158(this._b2)) / 2f;
                this._b1.A159(this.Caption);
                GraphicsElement.A160(base.Forecolor, ref b4, ref b5);
                string caption = this.Caption;
                if (base.RightToLeft)
                {
                    caption = GraphicsElement.A161(caption);
                }
                GraphicsElement.A162(b5, x, num, 0f, this._b1.A163(caption), false);
            }
            base._A92 = b4.A97;
            b4.A164(this, true, true);
        }

        internal override void A54(ref A55 b6)
        {
            int num = this.A95 + 1;
            A56 A = base._A92.A56;
            if (A != null)
            {
                A.A100(this.A95, 0);
            }
            base._A92.A93.A94(b6.A2, 0);
            b6.A59(string.Format("{0} 0 obj", this.A95));
            b6.A59("<<");
            b6.A59("/FT /Btn");
            b6.A54("/T ");
            if (A != null)
            {
                A26.A54(ref b6, base.Name, A);
            }
            else
            {
                A26.A54(ref b6, A15.A26(base.Name), A);
            }
            b6.A54("/V ");
            if (A != null)
            {
                A26.A54(ref b6, base.Value, A);
            }
            else
            {
                A26.A54(ref b6, A15.A26(base.Value), A);
            }
            b6.A54("/DV ");
            if (A != null)
            {
                A26.A54(ref b6, base.DefaultValue, A);
            }
            else
            {
                A26.A54(ref b6, A15.A26(base.DefaultValue), A);
            }
            b6.A59(string.Format("/Ff {0} ", A15.A18((float) ((long) base.A153))));
            b6.A54("/TU ");
            if (A != null)
            {
                A26.A54(ref b6, base.ToolTip, A);
            }
            else
            {
                A26.A54(ref b6, A15.A26(base.ToolTip), A);
            }
            b6.A59("/Type /Annot");
            b6.A59("/Subtype /Widget");
            float num2 = base.A141.A98(this.Bounds.Y);
            b6.A59(string.Format("/Rect {0}", A15.A21(this.Bounds.X, num2 - this.Bounds.Height, this.Bounds.Right, num2)));
            string str = base.A165(num);
            if ((str != null) && (str.Length > 0))
            {
                b6.A59(str);
            }
            b6.A59("/MK <<>>");
            b6.A59(string.Format("/P {0} 0 R", base.A141.A95));
            this.b7(ref b6, ref A);
            b6.A59("/F 4");
            b6.A59(">>");
            b6.A59("endobj");
            base.A166(num, ref b6);
        }

        public void AddActionFormReset()
        {
            this.AddActionFormReset(false, null);
        }

        public void AddActionFormReset(bool include, PdfField[] fields)
        {
            this._b3 = new A151(include, fields);
        }

        public void AddActionFormSubmit(string uri, FormSubmitFlags flags)
        {
            this.AddActionFormSubmit(uri, flags, null);
        }

        public void AddActionFormSubmit(string uri, FormSubmitFlags flags, PdfField[] fields)
        {
            this._b3 = new A152(uri, flags, fields);
        }

        public void AddActionGoToFile(string filepath)
        {
            this._b3 = new A147(filepath);
        }

        public void AddActionGoToLastPage()
        {
            this._b3 = new A136(A128.A133);
        }

        public void AddActionGoToPage(Page page)
        {
            this.AddActionGoToPage(page, 0f, 0f, 0f);
        }

        public void AddActionGoToPage(Page page, float top, float left, float zoom)
        {
            this._b3 = new A148(page, top, left, zoom);
        }

        public void AddActionGoToUri(string uri)
        {
            this._b3 = new A149(uri);
        }

        public void AddActionJavaScript(string javascript)
        {
            this._b3 = new A150(javascript);
        }

        private void b7(ref A55 b6, ref A56 e)
        {
            if (this._b3 != null)
            {
                if (this._b3.A137 == A128.A129)
                {
                    b6.A59("/A <<");
                    b6.A59("/F <<");
                    b6.A54("/F ");
                    if (e != null)
                    {
                        A26.A54(ref b6, this._b3.A48, e);
                    }
                    else
                    {
                        A26.A54(ref b6, A15.A26(this._b3.A48), e);
                    }
                    b6.A59("/FS /URL");
                    b6.A59(">>");
                    b6.A59("/S /SubmitForm");
                    b6.A59(string.Format("/Flags {0} ", A15.A18((float) ((long) this._b3.A139))));
                    b6.A59(string.Format(">>", new object[0]));
                }
                else if (this._b3.A137 == A128.A86)
                {
                    b6.A59("/A <<");
                    b6.A59("/S /ResetForm");
                    b6.A59(string.Format("/Flags {0} ", this._b3.A140 ? 0 : 1));
                    b6.A59(string.Format(">>", new object[0]));
                }
                else if (this._b3.A137 == A128.A130)
                {
                    b6.A59("/A <<");
                    b6.A59("/S /URI");
                    b6.A54("/URI ");
                    if (e != null)
                    {
                        A26.A54(ref b6, this._b3.A48, e);
                    }
                    else
                    {
                        A26.A54(ref b6, A15.A26(this._b3.A48), e);
                    }
                    b6.A59(string.Format(">>", new object[0]));
                }
                else if (this._b3.A137 == A128.A133)
                {
                    b6.A59("/A << ");
                    b6.A59("/S /Named ");
                    b6.A59("/N /LastPage ");
                    b6.A59(string.Format(">>", new object[0]));
                }
                else if (this._b3.A137 == A128.A131)
                {
                    b6.A59("/A << ");
                    b6.A59("/S /GoTo ");
                    b6.A59(string.Format("/D [ {0} 0 R /XYZ {1} {2} {3} ]", new object[] { this._b3.A141.A95, this._b3.A142, this._b3.A141.A98(this._b3.A143), this._b3.A144 / 100f }));
                    b6.A59(string.Format(">>", new object[0]));
                }
                else if (this._b3.A137 == A128.A134)
                {
                    b6.A59("/A << ");
                    A99.A54(ref b6, this._b3.A145, e);
                    b6.A59(string.Format(">>", new object[0]));
                }
                else if (this._b3.A137 == A128.A132)
                {
                    b6.A59("/A << ");
                    A101.A102(ref b6, base._A92, this.A95, this._b3.A146);
                    b6.A59(string.Format(">>", new object[0]));
                }
            }
        }

        internal override string A155
        {
            get
            {
                return "Btn";
            }
        }

        public string Caption
        {
            get
            {
                return this._b0;
            }
            set
            {
                if (value == null)
                {
                    this._b0 = string.Empty;
                }
                else
                {
                    this._b0 = value;
                }
            }
        }

        public PdfFont Font
        {
            get
            {
                return this._b1;
            }
            set
            {
                this._b1 = value;
            }
        }

        public float FontSize
        {
            get
            {
                return this._b2;
            }
            set
            {
                if (value <= 0f)
                {
                    value = 8f;
                }
                this._b2 = value;
            }
        }
    }
}

