namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.IO;
    using System;

    public class CodaBarGraphics : BarcodeGraphics
    {
        private CodaBarStartStopPair _b0 = CodaBarStartStopPair.AA;
        private string _b2;
        private float _b3 = 3f;
        private float _b4;
        private float _b5;
        private static string b1 = (BarcodeGraphics.A424 + "-$:/.+");

        internal override string A429()
        {
            return b1;
        }

        internal override void A430(ref A120 b10, ref A112 b11, float b12)
        {
            float num = (int) (base._A413 + base.X);
            float num2 = (int) b10.A98(this.A440());
            GraphicsElement.A160(base._A421, ref b10, ref b11);
            int length = this._b2.Length;
            for (int i = 0; i < length; i++)
            {
                num += this.b13(ref b10, ref b11, this.b8(this._b2[i]), num, num2);
            }
            base.A444(ref b10, ref b11, num2, b12, null);
        }

        internal override float A431(bool b6)
        {
            float num = 0f;
            if (b6)
            {
                this.A432();
            }
            int length = this._b2.Length;
            for (int i = 0; i < length; i++)
            {
                num += this.b7(this._b2[i]);
            }
            return (num + (base._A413 + base._A415));
        }

        internal override void A432()
        {
            this._b2 = base._A416;
            if (this._b0 == CodaBarStartStopPair.AA)
            {
                this._b2 = 'A' + base._A416 + 'A';
            }
            else if (this._b0 == CodaBarStartStopPair.BB)
            {
                this._b2 = 'B' + base._A416 + 'B';
            }
            else if (this._b0 == CodaBarStartStopPair.CC)
            {
                this._b2 = 'C' + base._A416 + 'C';
            }
            else if (this._b0 == CodaBarStartStopPair.DD)
            {
                this._b2 = 'D' + base._A416 + 'D';
            }
        }

        internal override void A433()
        {
            this._b4 = this._b3 * base._A423;
            this._b5 = base._A423;
        }

        internal override void A86()
        {
            this._b2 = string.Empty;
        }

        public override bool IsValidData(string data)
        {
            if ((data == null) || (data.Length == 0))
            {
                return false;
            }
            int num = 0;
            int length = data.Length;
            if (this._b0 != CodaBarStartStopPair.None)
            {
                char ch = data[0];
                char ch2 = data[length - 1];
                switch (ch)
                {
                    case 'A':
                    case 'B':
                    case 'C':
                    case 'D':
                        num = 1;
                        break;
                }
                if (((ch2 == 'A') || (ch2 == 'B')) || ((ch2 == 'C') || (ch2 == 'D')))
                {
                    length--;
                }
            }
            string str = this.A429();
            for (int i = num; i < length; i++)
            {
                if (str.IndexOf(data[i]) < 0)
                {
                    return false;
                }
            }
            return true;
        }

        private float b13(ref A120 b10, ref A112 b11, string b14, float b15, float b16)
        {
            float num = 0f;
            float num2 = 0f;
            for (int i = 0; i < b14.Length; i++)
            {
                num2 = this.b9(b14[i]);
                if ((i % 2) == 0)
                {
                    GraphicsElement.A437(b11, b15, b16, num2, base._A422);
                    GraphicsElement.A180(b11, GraphicsMode.fill, false);
                }
                b15 += num2;
                num += num2;
            }
            return num;
        }

        private float b7(char c)
        {
            switch (c)
            {
                case '$':
                    return ((2f * this._b4) + (6f * this._b5));

                case '+':
                    return ((3f * this._b4) + (5f * this._b5));

                case '-':
                    return ((2f * this._b4) + (6f * this._b5));

                case '.':
                    return ((3f * this._b4) + (5f * this._b5));

                case '/':
                    return ((3f * this._b4) + (5f * this._b5));

                case '0':
                    return ((2f * this._b4) + (6f * this._b5));

                case '1':
                    return ((2f * this._b4) + (6f * this._b5));

                case '2':
                    return ((2f * this._b4) + (6f * this._b5));

                case '3':
                    return ((2f * this._b4) + (6f * this._b5));

                case '4':
                    return ((2f * this._b4) + (6f * this._b5));

                case '5':
                    return ((2f * this._b4) + (6f * this._b5));

                case '6':
                    return ((2f * this._b4) + (6f * this._b5));

                case '7':
                    return ((2f * this._b4) + (6f * this._b5));

                case '8':
                    return ((2f * this._b4) + (6f * this._b5));

                case '9':
                    return ((2f * this._b4) + (6f * this._b5));

                case ':':
                    return ((3f * this._b4) + (5f * this._b5));

                case 'A':
                    return ((3f * this._b4) + (5f * this._b5));

                case 'B':
                    return ((3f * this._b4) + (5f * this._b5));

                case 'C':
                    return ((3f * this._b4) + (5f * this._b5));

                case 'D':
                    return ((3f * this._b4) + (5f * this._b5));
            }
            return 0f;
        }

        private string b8(char c)
        {
            switch (c)
            {
                case '$':
                    return "nnwwnnnn";

                case '+':
                    return "nnwnwnwn";

                case '-':
                    return "nnnwwnnn";

                case '.':
                    return "wnwnwnnn";

                case '/':
                    return "wnwnnnwn";

                case '0':
                    return "nnnnnwwn";

                case '1':
                    return "nnnnwwnn";

                case '2':
                    return "nnnwnnwn";

                case '3':
                    return "wwnnnnnn";

                case '4':
                    return "nnwnnwnn";

                case '5':
                    return "wnnnnwnn";

                case '6':
                    return "nwnnnnwn";

                case '7':
                    return "nwnnwnnn";

                case '8':
                    return "nwwnnnnn";

                case '9':
                    return "wnnwnnnn";

                case ':':
                    return "wnnnwnwn";

                case 'A':
                    return "nnwwnwnn";

                case 'B':
                    return "nwnwnnwn";

                case 'C':
                    return "nnnwnwwn";

                case 'D':
                    return "nnnwwwnn";
            }
            return "";
        }

        private float b9(char c)
        {
            if (c != 'n')
            {
                return this._b4;
            }
            return this._b5;
        }

        public override BarCodeType BarcodeType
        {
            get
            {
                return BarCodeType.Codabar;
            }
        }

        public override bool CodeTextAsSymbol
        {
            get
            {
                return false;
            }
            set
            {
            }
        }

        public override bool EnableCheckSum
        {
            get
            {
                return false;
            }
            set
            {
            }
        }

        public CodaBarStartStopPair StartStopPair
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

        public float WideNarrowRatio
        {
            get
            {
                return this._b3;
            }
            set
            {
                if (value < 2f)
                {
                    value = 2f;
                }
                if (value > 3f)
                {
                    value = 3f;
                }
                this._b3 = value;
            }
        }
    }
}

