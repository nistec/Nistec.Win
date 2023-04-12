namespace MControl.Printing.Pdf.Core.Compression
{
    using System;
    using System.Collections;

    internal class A74
    {
        private int b0;
        private byte[] b1;
        private ArrayList b2;
        private const int b3 = 0x102;
        private const int b4 = 0x8000;
        private const int b5 = 3;
        private int b6;
        private int b7;

        internal A74(byte[] b1) : this(b1, 15)
        {
        }

        internal A74(byte[] b1, int b8)
        {
            this.b2 = new ArrayList();
            if ((b8 < 8) || (b8 > 15))
            {
                b8 = 15;
            }
            this.b7 = ((int) 1) << b8;
            this.b0 = this.b7 - 0x102;
            this.A86(b1);
        }

        internal bool A76(A75 b13)
        {
            if (b13 == null)
            {
                return false;
            }
            if (this.b6 >= this.b1.Length)
            {
                return false;
            }
            byte num = this.b1[this.b6];
            this.b9(num);
            int count = this.b2.Count;
            if (count <= 0)
            {
                b13.A83 = num;
                b13.A82 = true;
                this.b14(1);
            }
            else
            {
                int num3 = 0;
                int num4 = 0;
                for (int i = 0; i < count; i++)
                {
                    int num6 = this.b11((int) this.b2[i]);
                    if (num6 > num4)
                    {
                        num3 = (int) this.b2[i];
                        num4 = num6;
                        if (num4 > 0x102)
                        {
                            num4 = 0x102;
                            break;
                        }
                    }
                }
                if (num4 < 3)
                {
                    num4 = 1;
                }
                if (num4 > 1)
                {
                    if (num3 > 0x8000)
                    {
                        throw new Exception("The phrase offset is out of bounds");
                    }
                    b13.A85 = num3;
                    b13.A84 = num4;
                    b13.A82 = false;
                }
                else
                {
                    b13.A83 = num;
                    b13.A82 = true;
                }
                this.b14(num4);
            }
            return true;
        }

        internal void A86()
        {
            this.b6 = 0;
        }

        internal void A86(byte[] input)
        {
            this.b1 = input;
            this.A86();
        }

        private int b11(int b12)
        {
            int num = this.b1.Length - this.b6;
            int num2 = 1;
            int num3 = (0x102 < num) ? 0x102 : num;
            for (int i = 1; i < num3; i++)
            {
                if (this.b1[(this.b6 - b12) + i] != this.b1[this.b6 + i])
                {
                    return num2;
                }
                num2++;
            }
            return num2;
        }

        private void b14(int b15)
        {
            this.b6 += b15;
        }

        private void b9(byte b10)
        {
            this.b2.Clear();
            int num = (this.b0 < this.b6) ? this.b0 : this.b6;
            for (int i = 1; i <= num; i++)
            {
                if (this.b1[this.b6 - i] == b10)
                {
                    this.b2.Add(i);
                }
            }
        }
    }
}

