namespace MControl.Printing.Pdf.Core.Compression
{
    using System;

    internal class A64
    {
        private const uint _b0 = 0xfff1;
        private uint _b1 = 1;

        internal A64()
        {
        }

        internal long A65(byte[] b2)
        {
            return this.A65(b2, 0, b2.Length);
        }

        internal long A65(int[] b2)
        {
            for (int i = 0; i < b2.Length; i++)
            {
                this.A65(BitConverter.GetBytes(b2[i]));
            }
            return (long) this._b1;
        }

        internal long A65(byte[] b2, int b3, int b4)
        {
            uint num = this._b1 & 0xffff;
            uint num2 = this._b1 >> 0x10;
            while (b4 > 0)
            {
                int num3 = 0xed8;
                if (num3 > b4)
                {
                    num3 = b4;
                }
                b4 -= num3;
                while (--num3 >= 0)
                {
                    num += (uint) (b2[b3++] & 0xff);
                    num2 += num;
                }
                num = num % 0xfff1;
                num2 = num2 % 0xfff1;
            }
            this._b1 = (num2 << 0x10) | num;
            return (long) this._b1;
        }

        internal static long A66(byte[] b2)
        {
            return new A64().A65(b2, 0, b2.Length);
        }
    }
}

