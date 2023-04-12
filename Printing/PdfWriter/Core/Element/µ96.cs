namespace MControl.Printing.Pdf.Core.Element
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core.IO;
    using System;

    internal class A96 : A90
    {
        private Page _b0;
        private float _b1;
        private float _b2;
        private float _b3;

        internal A96(Page b0, float b1, float b2, float b3) : base(b0.A97)
        {
            base._A92 = b0.A97;
            this._b0 = b0;
            this._b1 = b1;
            this._b2 = b2;
            this._b3 = b3;
        }

        internal override void A54(ref A55 b4)
        {
            base.A54(ref b4);
            b4.A59("/S /GoTo");
            b4.A59(string.Format("/D [ {0} 0 R /XYZ {1} {2} {3} ]", new object[] { this._b0.A95, this._b2, this._b0.A98(this._b1), this._b3 / 100f }));
            b4.A59(">>");
            b4.A59("endobj");
        }
    }
}

