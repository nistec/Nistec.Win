namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.IO;
    using System;

    public class Code11Graphics : BarcodeGraphics
    {
        private string _b2;
        private float _b3 = 2f;
        private Code11CheckSum _b4 = Code11CheckSum.UseBoth_CandK;
        private float _b5;
        private float _b6;
        private string _b7;
        private static string b0 = (BarcodeGraphics.A424 + '-');
        private static string b1 = "0123456789-";

        internal override string A429()
        {
            return b0;
        }

        internal override void A430(ref A120 b12, ref A112 b13, float b14)
        {
            float num = (int) (base._A413 + base.X);
            float num2 = (int) b12.A98(this.A440());
            GraphicsElement.A160(base._A421, ref b12, ref b13);
            int length = this._b2.Length;
            for (int i = 0; i < length; i++)
            {
                num += this.b15(ref b12, ref b13, this.b10(this._b2[i]), num, num2);
            }
            base.A444(ref b12, ref b13, num2, b14, this._b7);
        }

        internal override float A431(bool b8)
        {
            float num = 0f;
            if (b8)
            {
                this.A432();
            }
            int length = this._b2.Length;
            for (int i = 0; i < length; i++)
            {
                num += this.b9(this._b2[i]);
            }
            num += length - 1;
            num *= base._A423;
            return (num + (base._A413 + base._A415));
        }

        internal override void A432()
        {
            int length = base._A416.Length;
            this._b2 = base._A416;
            this._b7 = string.Empty;
            if (this._b4 != Code11CheckSum.None)
            {
                char ch = Code93Graphics.A448(this._b2, b1, 10, 11);
                this._b2 = this._b2 + ch;
                this._b7 = this._b7 + ch;
                if ((length >= 10) && (this._b4 == Code11CheckSum.UseBoth_CandK))
                {
                    ch = Code93Graphics.A448(this._b2, b1, 9, 11);
                    this._b2 = this._b2 + ch;
                    this._b7 = this._b7 + ch;
                }
            }
            this._b2 = '*' + this._b2 + '*';
        }

        internal override void A433()
        {
            this._b5 = this._b3 * base._A423;
            this._b6 = base._A423;
        }

        internal override void A86()
        {
            this._b7 = string.Empty;
            this._b2 = string.Empty;
        }

        private string b10(char c)
        {
            switch (c)
            {
                case '*':
                    return "nnwwnn";

                case '-':
                    return "nnwnnn";

                case '0':
                    return "nnnnwn";

                case '1':
                    return "wnnnwn";

                case '2':
                    return "nwnnwn";

                case '3':
                    return "wwnnnn";

                case '4':
                    return "nnwnwn";

                case '5':
                    return "wnwnnn";

                case '6':
                    return "nwwnnn";

                case '7':
                    return "nnnwwn";

                case '8':
                    return "wnnwnn";

                case '9':
                    return "wnnnnn";
            }
            return "";
        }

        private float b11(char c)
        {
            if (c == 'w')
            {
                return this._b5;
            }
            return this._b6;
        }

        private float b15(ref A120 b12, ref A112 b13, string b16, float b17, float b18)
        {
            float num = 0f;
            float num2 = 0f;
            for (int i = 0; i < b16.Length; i++)
            {
                num2 = this.b11(b16[i]);
                if ((i % 2) == 0)
                {
                    GraphicsElement.A437(b13, b17, b18, num2, base._A422);
                    GraphicsElement.A180(b13, GraphicsMode.fill, false);
                }
                b17 += num2;
                num += num2;
            }
            return num;
        }

        private float b9(char c)
        {
            switch (c)
            {
                case '*':
                    return 7f;

                case '-':
                    return 6f;

                case '0':
                    return 6f;

                case '1':
                    return 7f;

                case '2':
                    return 7f;

                case '3':
                    return 7f;

                case '4':
                    return 7f;

                case '5':
                    return 7f;

                case '6':
                    return 7f;

                case '7':
                    return 7f;

                case '8':
                    return 7f;

                case '9':
                    return 6f;
            }
            return 0f;
        }

        public override BarCodeType BarcodeType
        {
            get
            {
                return BarCodeType.Code11;
            }
        }

        public Code11CheckSum CheckSum
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

        public override bool EnableCheckSum
        {
            get
            {
                return true;
            }
            set
            {
            }
        }
    }
}

