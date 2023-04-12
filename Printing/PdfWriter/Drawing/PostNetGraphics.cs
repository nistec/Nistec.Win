namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.IO;
    using System;

    public class PostNetGraphics : BarcodeGraphics
    {
        private float _b1 = 9f;
        private float _b2 = 3.6f;
        private float _b3 = 1.44f;
        private float _b4 = 1.872f;
        //private float _b5 = 3.29f;
        private string _b6;
        private string _b7;
        private static string b0 = BarcodeGraphics.A424;

        public PostNetGraphics()
        {
            base.CodeTextVisible = false;
        }

        internal override string A429()
        {
            return b0;
        }

        internal override void A430(ref A120 b13, ref A112 b14, float b3)
        {
            float num = base._A413 + base.X;
            float num2 = b13.A98(this.A440());
            GraphicsElement.A160(base._A421, ref b13, ref b14);
            int length = this._b7.Length;
            for (int i = 0; i < length; i++)
            {
                num += this.b15(ref b13, ref b14, this.b9(this._b7[i]), num, num2);
            }
            base.A444(ref b13, ref b14, num2, b3, this._b6);
        }

        internal override float A431(bool b8)
        {
            float num = 0f;
            if (b8)
            {
                this.A432();
            }
            int length = this._b7.Length;
            for (int i = 0; i < length; i++)
            {
                string str = this.b9(this._b7[i]);
                num += str.Length * (this._b3 + this._b4);
            }
            return (num + (base._A413 + base._A415));
        }

        internal override void A432()
        {
            this._b7 = base._A416;
            if (this.EnableCheckSum)
            {
                this._b6 = b10(this._b7).ToString();
                this._b7 = this._b7 + this._b6;
            }
            else
            {
                this._b6 = "";
            }
            this._b7 = '*' + this._b7 + '*';
        }

        internal override void A433()
        {
        }

        internal override float A445()
        {
            return this._b1;
        }

        internal override void A86()
        {
            this._b6 = string.Empty;
            this._b7 = string.Empty;
        }

        private static char b10(string b11)
        {
            int num = 0;
            int length = b11.Length;
            for (int i = 0; i < length; i++)
            {
                num += BarcodeGraphics.A439(b11[i]);
            }
            num = num % 10;
            if (num != 0)
            {
                num = 10 - num;
            }
            return (char) (num + 0x30);
        }

        private float b12(char c)
        {
            if (c != 'f')
            {
                return this._b2;
            }
            return this._b1;
        }

        private float b15(ref A120 b13, ref A112 b14, string b16, float b17, float b18)
        {
            float num = 0f;
            float num2 = this._b1 - this._b2;
            for (int i = 0; i < b16.Length; i++)
            {
                if (b16[i] == 'f')
                {
                    GraphicsElement.A437(b14, b17, b18, this._b3, this._b1);
                }
                else
                {
                    GraphicsElement.A437(b14, b17, b18 - num2, this._b3, this._b2);
                }
                GraphicsElement.A180(b14, GraphicsMode.fill, false);
                b17 += this._b3 + this._b4;
                num += this._b3 + this._b4;
            }
            return num;
        }

        private string b9(char c)
        {
            switch (c)
            {
                case '*':
                    return "f";

                case '0':
                    return "ffhhh";

                case '1':
                    return "hhhff";

                case '2':
                    return "hhfhf";

                case '3':
                    return "hhffh";

                case '4':
                    return "hfhhf";

                case '5':
                    return "hfhfh";

                case '6':
                    return "hffhh";

                case '7':
                    return "fhhhf";

                case '8':
                    return "fhhfh";

                case '9':
                    return "fhfhh";
            }
            return "";
        }

        public override BarCodeType BarcodeType
        {
            get
            {
                return BarCodeType.PostNet;
            }
        }

        public override float BarHeight
        {
            get
            {
                return base._A422;
            }
            set
            {
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

        public override float Module
        {
            get
            {
                return base._A423;
            }
            set
            {
            }
        }
    }
}

