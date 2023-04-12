namespace MControl.Printing.Pdf.Core.Element
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Drawing;
    using MControl.Printing.Pdf.Core.IO;
    using System;

    public abstract class A91 : GraphicsElement
    {
        internal Document _A92;
        internal int _A95;

        internal A91()
        {
        }

        internal virtual void A110()
        {
            int num;
            A93 A1 = this._A92.A93;
            A1.A95 = (num = A1.A95) + 1;
            this._A95 = num;
        }

        internal override void A119(ref A120 b1, ref A112 b2)
        {
        }

        internal virtual void A54(ref A55 b0)
        {
        }

        internal virtual string A168
        {
            get
            {
                return null;
            }
        }

        internal virtual int A95
        {
            get
            {
                return this._A95;
            }
            set
            {
            }
        }
    }
}

