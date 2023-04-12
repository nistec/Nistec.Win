namespace MControl.Printing.Pdf.Core.Compression
{
    using System;
    using System.Collections;
    using System.IO;

    internal class A70
    {
        private int _b0;
        private uint _b1;
        private ArrayList _b2 = new ArrayList();
        private int _b3;
        private const int _b4 = 0x1000;

        internal A70()
        {
            this.b5();
        }

        internal void A72(int b13, int b14)
        {
            this._b1 |= (uint) (b13 << this._b0);
            this._b0 += b14;
            if (this._b0 >= 0x10)
            {
                this.b6((byte) this._b1);
                this.b6((byte) (this._b1 >> 8));
                this._b1 = this._b1 >> 0x10;
                this._b0 -= 0x10;
            }
        }

        internal void A77()
        {
            if (this._b0 > 0)
            {
                this.b6((byte) this._b1);
                if (this._b0 > 8)
                {
                    this.b6((byte) (this._b1 >> 8));
                }
            }
            this._b1 = 0;
            this._b0 = 0;
        }

        internal void A78(int b15)
        {
            this.b6((byte) (b15 >> 8));
            this.b6((byte) b15);
        }

        internal void A79(Stream b11)
        {
            if (this.b12 != 0)
            {
                for (int i = 0; i < (this.b12 - 1); i++)
                {
                    byte[] buffer = this.b7(i);
                    b11.Write(buffer, 0, buffer.Length);
                }
                if (this._b3 > 0)
                {
                    b11.Write(this.b10, 0, this._b3);
                }
                if (this._b0 != 0)
                {
                    b11.WriteByte((byte) this._b1);
                    if (this._b0 > 8)
                    {
                        b11.WriteByte((byte) (this._b1 >> 8));
                    }
                }
            }
        }

        private void b5()
        {
            this._b2.Add(new byte[0x1000]);
            this._b3 = 0;
        }

        private void b6(byte b9)
        {
            this.b10[this._b3++] = b9;
            if (this._b3 >= this.b10.Length)
            {
                this.b5();
            }
        }

        private byte[] b7(int b8)
        {
            return (byte[]) this._b2[b8];
        }

        private byte[] b10
        {
            get
            {
                return (byte[]) this._b2[this.b12 - 1];
            }
        }

        private int b12
        {
            get
            {
                return this._b2.Count;
            }
        }
    }
}

