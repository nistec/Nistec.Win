namespace MControl.Printing.Pdf.Core.Fonts
{
    using System;
    using System.IO;

    internal class A290
    {
        private MemoryStream _b0 = new MemoryStream();

        internal A290()
        {
        }

        internal void A238()
        {
            int num = this.A298 % 4;
            for (int i = 4; i > num; i--)
            {
                this.A388(0);
            }
        }

        internal void A291(byte[] b0)
        {
            for (int i = 0; i < b0.Length; i++)
            {
                this.A388(b0[i]);
            }
        }

        internal void A291(byte[] b0, bool b4)
        {
            if (b4)
            {
                for (int i = b0.Length - 1; i >= 0; i--)
                {
                    this.A388(b0[i]);
                }
            }
            else
            {
                this.A291(b0);
            }
        }

        internal void A297(ushort b5)
        {
            byte[] bytes = BitConverter.GetBytes(b5);
            this.A291(bytes, BitConverter.IsLittleEndian);
        }

        internal void A299(uint b5)
        {
            byte[] bytes = BitConverter.GetBytes(b5);
            this.A291(bytes, BitConverter.IsLittleEndian);
        }

        internal void A300(int b2)
        {
            this._b0.Seek((long) b2, SeekOrigin.Begin);
        }

        internal void A301(int b1)
        {
            this._b0.Position += b1;
        }

        internal uint A322(int b6, int b7)
        {
            uint num5;
            int num = b7 % 4;
            if (num > 0)
            {
                b7 += 4 - num;
            }
            b7 /= 4;
            int num2 = this.A298;
            try
            {
                this.A300(b6);
                uint num3 = 0;
                for (int i = 0; i < b7; i++)
                {
                    num3 += this.b8();
                }
                num5 = num3;
            }
            finally
            {
                this.A300(num2);
            }
            return num5;
        }

        internal void A363(short b5)
        {
            byte[] bytes = BitConverter.GetBytes(b5);
            this.A291(bytes, BitConverter.IsLittleEndian);
        }

        internal void A386(A373 b5)
        {
            this.A388(b5.A375);
            this.A388(b5.A376);
            this.A388(b5.A377);
            this.A388(b5.A378);
            this.A388(b5.A379);
            this.A388(b5.A380);
            this.A388(b5.A381);
            this.A388(b5.A382);
            this.A388(b5.A383);
            this.A388(b5.A384);
        }

        internal void A387(sbyte b5)
        {
            this.A388((byte) b5);
        }

        internal void A388(byte b3)
        {
            this._b0.WriteByte(b3);
        }

        internal void A389(int b5)
        {
            byte[] bytes = BitConverter.GetBytes(b5);
            this.A291(bytes, BitConverter.IsLittleEndian);
        }

        internal void A390(string b5)
        {
            for (int i = 0; i < b5.Length; i++)
            {
                this.A297(b5[i]);
            }
        }

        private uint b8()
        {
            if ((this.A298 < 0) || (this.A298 >= (this.A84 - 3)))
            {
                throw new Exception("error when working with .ttf file");
            }
            if (BitConverter.IsLittleEndian)
            {
                return (uint) ((((this._b0.ReadByte() << 0x18) + (this.b9() << 0x10)) + (this.b9() << 8)) + this.b9());
            }
            return (uint) (((this.b9() + (this.b9() << 8)) + (this.b9() << 0x10)) + (this.b9() << 0x18));
        }

        private byte b9()
        {
            return (byte) this._b0.ReadByte();
        }

        internal byte[] A221
        {
            get
            {
                return this._b0.ToArray();
            }
        }

        internal int A298
        {
            get
            {
                return (int) this._b0.Position;
            }
        }

        internal int A84
        {
            get
            {
                return (int) this._b0.Length;
            }
        }
    }
}

