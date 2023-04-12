namespace MControl.Printing.Pdf.Drawing
{
    using System;

    public class EAN128Graphics : Code128Graphics
    {
        private string _b0 = "#";
        private static string _b1 = new string(Code128Graphics.A449[0x66], 1);

        internal override void A432()
        {
            if ((this._b0 != null) && (this._b0 != string.Empty))
            {
                base._A416 = base._A416.Replace(this._b0, _b1);
            }
            base.A432();
        }

        public override BarCodeType BarcodeType
        {
            get
            {
                return BarCodeType.EAN128;
            }
        }

        public string FNC1Substitute
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
    }
}

