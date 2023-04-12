namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.IO;
    using System;

    public class Industrial2of5Graphics : BarcodeGraphics
    {
        internal string _A450;
        internal string _A455;
        internal float _A466;
        internal float _A467;
        private float _b1 = 2.5f;
        private static string b0 = BarcodeGraphics.A424;

        internal override string A429()
        {
            return b0;
        }

        internal override void A430(ref A120 b5, ref A112 b6, float b7)
        {
            float num = (int) (base._A413 + base.X);
            float num2 = (int) b5.A98(this.A440());
            GraphicsElement.A160(base._A421, ref b5, ref b6);
            int length = this._A450.Length;
            for (int i = 0; i < length; i++)
            {
                num += this.b8(ref b5, ref b6, this.A453(this._A450[i]), num, num2);
            }
            base.A444(ref b5, ref b6, num2, b7, this._A455);
        }

        internal override float A431(bool b2)
        {
            float num = 0f;
            if (b2)
            {
                this.A432();
            }
            int length = this._A450.Length;
            for (int i = 0; i < length; i++)
            {
                num += this.A452(this._A450[i]);
            }
            return (num + (base._A413 + base._A415));
        }

        internal override void A432()
        {
            this._A450 = base._A416;
            if (this.EnableCheckSum)
            {
                this._A455 = A448(this._A450).ToString();
                this._A450 = this._A450 + this._A455;
            }
            else
            {
                this._A455 = "";
            }
            this._A450 = 'S' + this._A450 + 'F';
        }

        internal override void A433()
        {
            this._A466 = this._b1 * base._A423;
            this._A467 = base._A423;
        }

        internal static char A448(string b3)
        {
            bool flag = true;
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            for (int i = b3.Length - 1; i >= 0; i--)
            {
                if (flag)
                {
                    num += BarcodeGraphics.A439(b3[i]);
                }
                else
                {
                    num2 += BarcodeGraphics.A439(b3[i]);
                }
                flag = !flag;
            }
            num3 = num2 + (num * 3);
            num3 = num3 % 10;
            if (num3 != 0)
            {
                num3 = 10 - num3;
            }
            return (char) (num3 + 0x30);
        }

        internal virtual float A452(char c)
        {
            if ((c != 'S') && (c != 'F'))
            {
                return ((2f * this._A466) + (8f * this._A467));
            }
            return ((2f * this._A466) + (4f * this._A467));
        }

        internal virtual string A453(char c)
        {
            switch (c)
            {
                case '0':
                    return "nnnnwnwnnn";

                case '1':
                    return "wnnnnnnnwn";

                case '2':
                    return "nnwnnnnnwn";

                case '3':
                    return "wnwnnnnnnn";

                case '4':
                    return "nnnnwnnnwn";

                case '5':
                    return "wnnnwnnnnn";

                case '6':
                    return "nnwnwnnnnn";

                case '7':
                    return "nnnnnnwnwn";

                case '8':
                    return "wnnnnnwnnn";

                case '9':
                    return "nnwnnnwnnn";

                case 'F':
                    return "wnnnwn";

                case 'S':
                    return "wnwnnn";
            }
            return "";
        }

        internal override void A86()
        {
            this._A455 = string.Empty;
            this._A450 = string.Empty;
        }

        private float b4(char c)
        {
            if (c != 'n')
            {
                return this._A466;
            }
            return this._A467;
        }

        private float b8(ref A120 b5, ref A112 b6, string b9, float b10, float b11)
        {
            float num = 0f;
            float num2 = 0f;
            for (int i = 0; i < b9.Length; i++)
            {
                num2 = this.b4(b9[i]);
                if ((i % 2) == 0)
                {
                    GraphicsElement.A437(b6, b10, b11, num2, base._A422);
                    GraphicsElement.A180(b6, GraphicsMode.fill, false);
                }
                b10 += num2;
                num += num2;
            }
            return num;
        }

        public override BarCodeType BarcodeType
        {
            get
            {
                return BarCodeType.Industrial2of5;
            }
        }

        public float WideNarrowRatio
        {
            get
            {
                return this._b1;
            }
            set
            {
                if (value < 2.5f)
                {
                    value = 2.5f;
                }
                this._b1 = value;
            }
        }
    }
}

