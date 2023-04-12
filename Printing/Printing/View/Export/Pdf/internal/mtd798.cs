namespace Nistec.Printing.View.Pdf
{
    using System;

    internal class mtd798
    {
        private byte[] _var0 = new byte[0x100];

        internal mtd798()
        {
        }

        internal byte[] mtd713(byte[] var1, byte[] var2)
        {
            return this.mtd713(var1, var1.Length, var2, var2.Length);
        }

        internal byte[] mtd713(byte[] var1, byte[] var2, int var3)
        {
            return this.mtd713(var1, var1.Length, var2, var3);
        }

        internal byte[] mtd713(byte[] var1, int var4, byte[] var2, int var3)
        {
            int index = 0;
            int num2 = 0;
            for (int i = 0; i < 0x100; i++)
            {
                this._var0[i] = (byte) i;
            }
            for (int j = 0; j < 0x100; j++)
            {
                index = ((index + this._var0[j]) + var2[j % var3]) & 0xff;
                byte num5 = this._var0[j];
                this._var0[j] = this._var0[index];
                this._var0[index] = num5;
            }
            byte[] array = new byte[0x100];
            this._var0.CopyTo(array, 0);
            byte[] buffer2 = new byte[var4];
            index = 0;
            for (int k = 0; k < var4; k++)
            {
                index = (index + 1) & 0xff;
                num2 = (num2 + array[index]) & 0xff;
                byte num7 = array[index];
                array[index] = array[num2];
                array[num2] = num7;
                byte num8 = var1[k];
                byte num9 = array[(array[index] + array[num2]) & 0xff];
                buffer2[k] = (byte) (num8 ^ num9);
            }
            return buffer2;
        }
    }
}

