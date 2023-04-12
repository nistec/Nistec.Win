namespace MControl.Printing.Pdf.Core.Encrypt
{
    using System;

    internal class A235
    {
        private byte[] _b0 = new byte[0x100];

        internal A235()
        {
        }

        internal byte[] A57(byte[] b1, byte[] b2)
        {
            return this.A57(b1, b1.Length, b2, b2.Length);
        }

        internal byte[] A57(byte[] b1, byte[] b2, int b3)
        {
            return this.A57(b1, b1.Length, b2, b3);
        }

        internal byte[] A57(byte[] b1, int b4, byte[] b2, int b3)
        {
            int index = 0;
            int num2 = 0;
            for (int i = 0; i < 0x100; i++)
            {
                this._b0[i] = (byte) i;
            }
            for (int j = 0; j < 0x100; j++)
            {
                index = ((index + this._b0[j]) + b2[j % b3]) & 0xff;
                byte num5 = this._b0[j];
                this._b0[j] = this._b0[index];
                this._b0[index] = num5;
            }
            byte[] array = new byte[0x100];
            this._b0.CopyTo(array, 0);
            byte[] buffer2 = new byte[b4];
            index = 0;
            for (int k = 0; k < b4; k++)
            {
                index = (index + 1) & 0xff;
                num2 = (num2 + array[index]) & 0xff;
                byte num7 = array[index];
                array[index] = array[num2];
                array[num2] = num7;
                byte num8 = b1[k];
                byte num9 = array[(array[index] + array[num2]) & 0xff];
                buffer2[k] = (byte) (num8 ^ num9);
            }
            return buffer2;
        }
    }
}

