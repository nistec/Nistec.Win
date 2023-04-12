namespace MControl.Printing.Pdf.Controls
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.Controls;
    using MControl.Printing.Pdf.Core.Element;
    using MControl.Printing.Pdf.Core.Encrypt;
    using MControl.Printing.Pdf.Core.IO;
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Reflection;

    public class PdfRadioGroup : PdfField
    {
        private int _b0;

        internal PdfRadioGroup(Page page, string name) : base(page)
        {
            this._b0 = 0;
            base._A154 = name;
            base.A174 = new ArrayList();
            base.A153 |= A127.Radio | A127.NoToggleToOff;
            base.A153 &= ~A127.PushButton;
        }

        internal override void A119(ref A120 b1, ref A112 b2)
        {
            base.DefaultValue = this.b3();
            base.Value = base.DefaultValue;
            base._A92 = b1.A97;
            b1.A164(this, true, false);
            for (int i = 0; i < base.A174.Count; i++)
            {
                (base.A174[i] as PdfRadioButton).A119(ref b1, ref b2);
            }
        }

        internal override void A54(ref A55 b4)
        {
            A56 A = base._A92.A56;
            if (A != null)
            {
                A.A100(this.A95, 0);
            }
            base._A92.A93.A94(b4.A2, 0);
            b4.A59(string.Format("{0} 0 obj", this.A95));
            b4.A59("<<");
            b4.A59("/FT /Btn");
            b4.A54("/T ");
            if (A != null)
            {
                A26.A54(ref b4, base.Name, A);
            }
            else
            {
                A26.A54(ref b4, A15.A26(base.Name), A);
            }
            b4.A59(string.Format("/V /{0}", base.Value));
            b4.A59(string.Format("/DV /{0}", base.DefaultValue));
            b4.A59(string.Format("/Ff {0} ", A15.A18((float) ((long) base.A153))));
            b4.A54("/TU ");
            if (A != null)
            {
                A26.A54(ref b4, base.ToolTip, A);
            }
            else
            {
                A26.A54(ref b4, A15.A26(base.ToolTip), A);
            }
            if (base.A174.Count > 0)
            {
                b4.A54("/Kids [ ");
                for (int i = 0; i < base.A174.Count; i++)
                {
                    A91 A3 = (A91) base.A174[i];
                    b4.A54(string.Format("{0} 0 R ", A3.A95));
                }
                b4.A59("]");
            }
            b4.A59(">>");
            b4.A59("endobj");
        }

        public PdfRadioButton AddRadioButton(string name, RectangleF bound)
        {
            PdfRadioButton button = new PdfRadioButton(this, name, bound);
            base.A174.Add(button);
            if (this._b0 == (base.A174.Count - 1))
            {
                button._A182 = true;
            }
            return button;
        }

        public PdfRadioButton AddRadioButton(string name, float left, float top)
        {
            return this.AddRadioButton(name, new RectangleF(left, top, 10f, 10f));
        }

        public void SetDefaultButton(PdfRadioButton radio)
        {
            if ((radio == null) && base.A174.Contains(radio))
            {
                this.SetDefaultButton(base.A174.IndexOf(radio));
            }
        }

        public void SetDefaultButton(int index)
        {
            if (((base.A174.Count > 0) && (index >= 0)) && ((index < base.A174.Count) && (index != this._b0)))
            {
                (base.A174[index] as PdfRadioButton)._A182 = true;
                (base.A174[this._b0] as PdfRadioButton)._A182 = false;
                this._b0 = index;
            }
        }

        public void SetDefaultButton(string name)
        {
            for (int i = 0; i < base.A174.Count; i++)
            {
                if ((base.A174[i] as PdfRadioButton).Name == name)
                {
                    this.SetDefaultButton(i);
                    return;
                }
            }
        }

        private string b3()
        {
            if (base.A174.Count > 0)
            {
                return (base.A174[this._b0] as PdfRadioButton).Name;
            }
            return string.Empty;
        }

        internal override string A155
        {
            get
            {
                return "Btn";
            }
        }

        public int Count
        {
            get
            {
                return base.A174.Count;
            }
        }

        public PdfRadioButton this[string name]
        {
            get
            {
                int count = this.Count;
                PdfRadioButton button = null;
                for (int i = 0; i < count; i++)
                {
                    button = base.A174[i] as PdfRadioButton;
                    if (button.Name == name)
                    {
                        return button;
                    }
                }
                return null;
            }
        }

        public PdfRadioButton this[int index]
        {
            get
            {
                if ((index >= base.A174.Count) || (index <= 0))
                {
                    throw new ArgumentException("Index is out of range.", "index");
                }
                return (base.A174[index] as PdfRadioButton);
            }
        }
    }
}

