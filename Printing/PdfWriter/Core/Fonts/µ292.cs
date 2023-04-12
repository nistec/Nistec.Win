namespace MControl.Printing.Pdf.Core.Fonts
{
    using System;
    using System.Collections;
    using System.Reflection;
    using System.Text;

    internal class A292 : A51
    {
        private ushort[] _b0;
        private ushort[] _b1;
        private int _b10;
        private Encoding _b11;
        private ushort[] _b2;
        private ushort[] _b3;
        private ushort[] _b4;
        private int _b5;
        private ushort _b6;
        private ushort _b7;
        private ushort _b8;
        private ushort _b9;

        internal A292(A281 b12) : base(b12)
        {
            this._b2 = new ushort[0x10000];
        }

        protected override void A283(A51 b14, A284 b15)
        {
            A292 A = b14 as A292;
            this._b7 = A._b7;
            this._b6 = A._b6;
            for (int i = 0; i < b15.A296.Length; i++)
            {
                ushort index = b15.A296[i];
                this._b2[index] = A[index];
            }
            this.b16(b15);
        }

        protected override void A285(A286 b19)
        {
            int num = b19.A298;
            this._b7 = b19.A302();
            int num2 = Convert.ToInt32(b19.A302());
            int num3 = -1;
            for (int i = 0; i < num2; i++)
            {
                ushort num5 = b19.A302();
                this._b8 = b19.A302();
                if ((((num5 == 3) && (this._b8 == 1)) || ((num5 == 3) && (this._b8 == 0))) || ((num5 == 3) && (this._b8 == 3)))
                {
                    num3 = Convert.ToInt32(b19.A303());
                    break;
                }
                b19.A301(4);
            }
            if (num3 == -1)
            {
                throw new Exception("Unicode CMap not found in font file");
            }
            b19.A300(num);
            b19.A301(num3);
            this.b21(b19);
        }

        protected override void A289(A290 b19)
        {
            int num = b19.A298;
            b19.A297(this._b7);
            b19.A297(1);
            b19.A297(3);
            b19.A297(1);
            b19.A299((uint) ((b19.A298 - num) + 4));
            this.b20(b19);
        }

        private void b16(A284 b15)
        {
            this._b5 = 0;
            for (int i = 0; i < (b15.A296.Length - 1); i++)
            {
                if (b15.A296[i + 1] != (b15.A296[i] + 1))
                {
                    this._b5++;
                }
            }
            this._b5 += 2;
            this._b1 = new ushort[this._b5];
            this._b4 = new ushort[this._b5];
            this._b0 = new ushort[this._b5];
            this._b3 = new ushort[this._b5];
            if (b15.A296.Length > 0)
            {
                int index = 0;
                this._b4[0] = b15.A296[0];
                for (int j = 0; j < (b15.A296.Length - 1); j++)
                {
                    if (b15.A296[j + 1] != (b15.A296[j] + 1))
                    {
                        this._b1[index] = b15.A296[j];
                        this._b4[index + 1] = b15.A296[j + 1];
                        index++;
                    }
                }
                this._b1[index] = b15.A296[b15.A296.Length - 1];
                this._b1[index + 1] = 0xffff;
                this._b4[index + 1] = 0xffff;
            }
            this.b17();
        }

        private void b17()
        {
            int num = 0;
            for (int i = 0; i < this._b5; i++)
            {
                bool flag = true;
                for (int j = this._b4[i]; j < this._b1[i]; j++)
                {
                    if (this._b2[j + 1] != (this._b2[j] + 1))
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    this._b3[i] = 0;
                    int num4 = this._b2[this._b4[i]] - this._b4[i];
                    if (num4 < 0)
                    {
                        num4 += 0x10000;
                    }
                    this._b0[i] = (ushort) num4;
                }
                else
                {
                    this._b3[i] = (ushort) (num + ((this._b3.Length - i) * 2));
                    num += ((this._b1[i] - this._b4[i]) + 1) * 2;
                    this._b0[i] = 0;
                }
            }
        }

        private void b18(A290 b19)
        {
            for (int i = 0; i < this._b3.Length; i++)
            {
                if (this._b3[i] != 0)
                {
                    for (int j = this._b4[i]; j <= this._b1[i]; j++)
                    {
                        b19.A297(this._b2[j]);
                    }
                }
            }
        }

        private void b20(A290 b19)
        {
            int num = b19.A298;
            b19.A297(4);
            b19.A297(0);
            b19.A297(this._b6);
            b19.A297((ushort) (this._b5 * 2));
            double y = Math.Floor(Math.Log((double) this._b5, 2.0));
            double num3 = Math.Pow(2.0, y) * 2.0;
            b19.A297((ushort) num3);
            b19.A297((ushort) y);
            b19.A297((ushort) ((this._b5 * 2) - num3));
            for (int i = 0; i < this._b1.Length; i++)
            {
                b19.A297(this._b1[i]);
            }
            b19.A297(0);
            for (int j = 0; j < this._b4.Length; j++)
            {
                b19.A297(this._b4[j]);
            }
            for (int k = 0; k < this._b0.Length; k++)
            {
                b19.A297(this._b0[k]);
            }
            for (int m = 0; m < this._b3.Length; m++)
            {
                b19.A297(this._b3[m]);
            }
            this.b18(b19);
            int num8 = b19.A298 - num;
            b19.A300(num);
            b19.A301(2);
            b19.A297((ushort) num8);
            b19.A300(num);
            b19.A301(num8);
        }

        private void b21(A286 b19)
        {
            this._b10 = b19.A298;
            this._b9 = b19.A302();
            if (this._b9 == 2)
            {
                this.b22(b19);
            }
            else
            {
                if (this._b9 != 4)
                {
                    throw new Exception("Invalid CMap format");
                }
                this.b23(b19);
            }
        }

        private void b22(A286 b19)
        {
            ushort num = b19.A302();
            this._b6 = b19.A302();
            ushort[] numArray = new ushort[0x100];
            ArrayList list = new ArrayList();
            for (int i = 0; i < numArray.Length; i++)
            {
                numArray[i] = (ushort) (b19.A302() / 8);
                if (!list.Contains(numArray[i]))
                {
                    list.Add(numArray[i]);
                }
            }
            A304[] AArray = new A304[list.Count];
            int num3 = b19.A298 + (list.Count * 8);
            for (int j = 0; j < list.Count; j++)
            {
                ushort num5 = b19.A302();
                ushort num6 = b19.A302();
                short num7 = b19.A305();
                int num8 = b19.A298;
                ushort num9 = b19.A302();
                AArray[j] = new A304(num5, num6, num7, (num9 - (num3 - num8)) / 2);
            }
            int num10 = num - (b19.A298 - this._b10);
            ushort[] numArray2 = new ushort[num10 / 2];
            for (int k = 0; k < numArray2.Length; k++)
            {
                numArray2[k] = b19.A302();
            }
            for (int m = 0; m < 0x100; m++)
            {
                if ((numArray[m] > 0) || (m == 0))
                {
                    int index = numArray[m];
                    for (int n = 0; n < AArray[index].A306; n++)
                    {
                        ushort num15 = numArray2[AArray[index].A307 + n];
                        if (num15 != 0)
                        {
                            num15 = (ushort) (num15 + ((ushort) AArray[index].A308));
                            this._b2[(m << 8) | (AArray[index].A309 + n)] = num15;
                        }
                    }
                }
            }
        }

        private void b23(A286 b19)
        {
            b19.A302();
            this._b6 = b19.A302();
            this._b5 = Convert.ToInt32((int) (b19.A302() / 2));
            this._b1 = new ushort[this._b5];
            this._b4 = new ushort[this._b5];
            this._b0 = new ushort[this._b5];
            this._b3 = new ushort[this._b5];
            b19.A301(6);
            for (int i = 0; i < this._b5; i++)
            {
                this._b1[i] = b19.A302();
            }
            b19.A301(2);
            for (int j = 0; j < this._b5; j++)
            {
                this._b4[j] = b19.A302();
            }
            for (int k = 0; k < this._b5; k++)
            {
                this._b0[k] = b19.A302();
            }
            for (int m = 0; m < this._b5; m++)
            {
                this._b3[m] = b19.A302();
            }
            int num5 = b19.A298;
            for (int n = 0; n < this._b1.Length; n++)
            {
                for (int num7 = this._b4[n]; num7 <= this._b1[n]; num7++)
                {
                    ushort num8 = 0;
                    if ((this._b3[n] != 0) && (num7 != 0xffff))
                    {
                        int num9 = (((((this._b3[n] / 2) + num7) - this._b4[n]) + n) - this._b3.Length) * 2;
                        b19.A300(num5);
                        b19.A301(num9);
                        num8 = b19.A302();
                        if (num8 != 0)
                        {
                            num8 = (ushort) ((num8 + this._b0[n]) & 0xffff);
                        }
                    }
                    else
                    {
                        num8 = (ushort) ((num7 + this._b0[n]) & 0xffff);
                    }
                    this._b2[num7] = num8;
                }
            }
        }

        protected internal override string A226
        {
            get
            {
                return "cmap";
            }
        }

        internal int A293
        {
            get
            {
                return this._b2.Length;
            }
        }

        internal Encoding A294
        {
            get
            {
                return this._b11;
            }
            set
            {
                this._b11 = value;
            }
        }

        internal ushort A295
        {
            get
            {
                return this._b9;
            }
        }

        internal override int A84
        {
            get
            {
                int num = ((14 + (2 * this._b5)) + 2) + (6 * this._b5);
                for (int i = 0; i < this._b5; i++)
                {
                    if (this._b3[i] != 0)
                    {
                        num += ((this._b1[i] - this._b4[i]) + 1) * 2;
                    }
                }
                return (12 + num);
            }
        }

        internal ushort this[ushort b13]
        {
            get
            {
                ushort index = 0;
                if (this._b9 == 4)
                {
                    index = (this._b8 == 0) ? ((ushort) (b13 + 0xf000)) : b13;
                }
                if ((this._b9 == 2) && (this._b11 != null))
                {
                    byte[] buffer = Encoding.Convert(Encoding.Unicode, this._b11, Encoding.Unicode.GetBytes(new char[] { (char)b13 }));
                    index = (buffer.Length > 1) ? ((ushort) ((buffer[0] << 8) | buffer[1])) : buffer[0];
                }
                return this._b2[index];
            }
        }
    }
}

