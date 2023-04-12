namespace MControl.Printing.Pdf.Core.Fonts
{
    using System;

    internal class A339 : A51
    {
        private byte[] _b0;
        private ushort[] _b1;
        private short _b10;
        private string[] _b2;
        private uint _b3;
        private byte[] _b4;
        private uint _b5;
        private uint _b6;
        private uint _b7;
        private uint _b8;
        private short _b9;

        internal A339(A281 b11) : base(b11)
        {
        }

        protected override void A283(A51 b14, A284 b15)
        {
            A339 A = b14 as A339;
            byte[] buffer = new byte[4];
            buffer[1] = 3;
            this._b0 = buffer;
            this._b4 = new byte[A._b4.Length];
            A._b4.CopyTo(this._b4, 0);
            this._b9 = A._b9;
            this._b10 = A._b10;
            this._b3 = A._b3;
            this._b8 = A._b8;
            this._b6 = A._b6;
            this._b7 = A._b7;
            this._b5 = A._b5;
        }

        protected override void A285(A286 b17)
        {
            this._b0 = b17.A287(4);
            this._b4 = b17.A287(4);
            this._b9 = b17.A305();
            this._b10 = b17.A305();
            this._b3 = b17.A303();
            this._b8 = b17.A303();
            this._b6 = b17.A303();
            this._b7 = b17.A303();
            this._b5 = b17.A303();
            this.b16(b17);
        }

        protected override void A289(A290 b17)
        {
            b17.A291(this._b0);
            b17.A291(this._b4);
            b17.A363(this._b9);
            b17.A363(this._b10);
            b17.A299(this._b3);
            b17.A299(this._b8);
            b17.A299(this._b6);
            b17.A299(this._b7);
            b17.A299(this._b5);
            this.b20(b17);
        }

        internal static float A392(byte[] b23)
        {
            if (b23.Length != 4)
            {
                return 0f;
            }
            byte[] buffer = new byte[4];
            if (BitConverter.IsLittleEndian)
            {
                for (int i = 0; i < 4; i++)
                {
                    buffer[i] = b23[(4 - i) - 1];
                }
            }
            else
            {
                for (int j = 0; j < 4; j++)
                {
                    buffer[j] = b23[j];
                }
            }
            short num3 = BitConverter.ToInt16(buffer, 2);
            ushort num4 = BitConverter.ToUInt16(buffer, 0);
            if (num4 != 0)
            {
                double num5 = Math.Pow(10.0, Math.Ceiling(Math.Log10((double) num4)));
                return Convert.ToSingle((double) (num3 + ((((double) num4) / num5) * Math.Sign(num3))));
            }
            return Convert.ToSingle(num3);
        }

        private int b12()
        {
            float num = this.A391;
            this.b13(num);
            if (num != 2f)
            {
                return 0;
            }
            int num2 = 2 + (this._b1.Length * 2);
            for (int i = 0; i < this._b2.Length; i++)
            {
                num2 += this._b2[i].Length + 1;
            }
            return num2;
        }

        private void b13(float b19)
        {
            if (((b19 != 1f) && (b19 != 2f)) && (b19 != 3f))
            {
                throw new Exception("Invalid Post format in Font File");
            }
        }

        private void b16(A286 b17)
        {
            float num = this.A391;
            this.b13(num);
            if (num == 2f)
            {
                ushort num2 = b17.A302();
                this._b1 = new ushort[num2];
                int num3 = 0;
                for (int i = 0; i < num2; i++)
                {
                    this._b1[i] = b17.A302();
                    if (this._b1[i] > 0x101)
                    {
                        num3++;
                    }
                }
                this._b2 = new string[num3];
                for (int j = 0; j < num3; j++)
                {
                    this._b2[j] = this.b18(b17);
                }
            }
        }

        private string b18(A286 b17)
        {
            string str = "";
            int num = b17.A371();
            for (int i = 0; i < num; i++)
            {
                str = str + ((char) b17.A370());
            }
            return str;
        }

        private void b20(A290 b17)
        {
            float num = this.A391;
            this.b13(num);
            if (num == 2f)
            {
                b17.A297((ushort) this._b1.Length);
                for (int i = 0; i < this._b1.Length; i++)
                {
                    b17.A297(this._b1[i]);
                }
                for (int j = 0; j < this._b2.Length; j++)
                {
                    this.b21(b17, this._b2[j]);
                }
            }
        }

        private void b21(A290 b17, string b22)
        {
            b17.A387((sbyte) b22.Length);
            for (int i = 0; i < b22.Length; i++)
            {
                b17.A388((byte) b22[i]);
            }
        }

        protected internal override string A226
        {
            get
            {
                return "post";
            }
        }

        internal float A391
        {
            get
            {
                return A392(this._b0);
            }
        }

        internal float A393
        {
            get
            {
                return A392(this._b4);
            }
        }

        internal uint A394
        {
            get
            {
                return this._b3;
            }
        }

        internal override int A84
        {
            get
            {
                return (0x20 + this.b12());
            }
        }
    }
}

