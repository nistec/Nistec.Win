namespace MControl.Printing.Pdf.Core.Fonts
{
    using System;

    internal class A286
    {
        private byte[] _b0;
        private int _b1;

        internal A286(byte[] b2)
        {
            this._b0 = b2;
            this._b1 = 0;
        }

        internal byte[] A287(int b5)
        {
            if ((this.A298 < 0) || (this.A298 >= ((this.A84 - b5) + 1)))
            {
                throw new Exception("Error in reading FontFile");
            }
            byte[] buffer = new byte[b5];
            for (int i = 0; i < b5; i++)
            {
                buffer[i] = this.b3();
            }
            return buffer;
        }

        internal void A300(int b1)
        {
            if ((b1 < 0) || (b1 > this.A84))
            {
                throw new Exception("Error in reading FontFile");
            }
            this._b1 = b1;
        }

        internal void A301(int b4)
        {
            this.A300(this.A298 + b4);
        }

        internal ushort A302()
        {
            if ((this.A298 < 0) || (this.A298 >= (this.A84 - 1)))
            {
                throw new Exception("Error in reading FontFile");
            }
            if (BitConverter.IsLittleEndian)
            {
                return (ushort) ((this.b3() << 8) + this.b3());
            }
            return (ushort) (this.b3() + (this.b3() << 8));
        }

        internal uint A303()
        {
            if ((this.A298 < 0) || (this.A298 >= (this.A84 - 3)))
            {
                throw new Exception("Error in reading FontFile");
            }
            if (BitConverter.IsLittleEndian)
            {
                return (uint) ((((this.b3() << 0x18) + (this.b3() << 0x10)) + (this.b3() << 8)) + this.b3());
            }
            return (uint) (((this.b3() + (this.b3() << 8)) + (this.b3() << 0x10)) + (this.b3() << 0x18));
        }

        internal short A305()
        {
            if ((this.A298 < 0) || (this.A298 >= (this.A84 - 1)))
            {
                throw new Exception("Error in reading FontFile");
            }
            if (BitConverter.IsLittleEndian)
            {
                return (short) ((this.b3() << 8) + this.b3());
            }
            return (short) (this.b3() + (this.b3() << 8));
        }

        internal byte A370()
        {
            if ((this.A298 < 0) || (this.A298 >= this.A84))
            {
                throw new Exception("Error in reading FontFile");
            }
            return this.b3();
        }

        internal sbyte A371()
        {
            return Convert.ToSByte(this.A370());
        }

        internal int A372()
        {
            if ((this.A298 < 0) || (this.A298 >= (this.A84 - 3)))
            {
                throw new Exception("Error in reading FontFile");
            }
            if (BitConverter.IsLittleEndian)
            {
                return ((((this.b3() << 0x18) + (this.b3() << 0x10)) + (this.b3() << 8)) + this.b3());
            }
            return (((this.b3() + (this.b3() << 8)) + (this.b3() << 0x10)) + (this.b3() << 0x18));
        }

        internal A373 A374()
        {
            if ((this.A298 < 0) || (this.A298 >= (this.A84 - 9)))
            {
                throw new Exception("Error in reading FontFile");
            }
            A373 A = new A373();
            A.A375 = this.b3();
            A.A376 = this.b3();
            A.A377 = this.b3();
            A.A378 = this.b3();
            A.A379 = this.b3();
            A.A380 = this.b3();
            A.A381 = this.b3();
            A.A382 = this.b3();
            A.A383 = this.b3();
            A.A384 = this.b3();
            return A;
        }

        internal string A385(int b6)
        {
            string str = "";
            for (int i = 0; i < b6; i += 2)
            {
                str = str + this.A302();
            }
            return str;
        }

        private byte b3()
        {
            return this._b0[this._b1++];
        }

        internal int A298
        {
            get
            {
                return this._b1;
            }
        }

        internal int A84
        {
            get
            {
                return this._b0.Length;
            }
        }
    }
}

