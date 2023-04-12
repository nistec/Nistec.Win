namespace Nistec.Printing.View.Pdf
{
    using System;

    internal class mtd825
    {
        private byte[] _var0;
        private int _var1;

        internal mtd825(byte[] var2)
        {
            this._var0 = var2;
            this._var1 = 0;
        }

        internal byte[] mtd826(int var5)
        {
            if ((this.mtd832 < 0) || (this.mtd832 >= ((this.mtd736 - var5) + 1)))
            {
                throw new Exception("Error in reading FontFile");
            }
            byte[] buffer = new byte[var5];
            for (int i = 0; i < var5; i++)
            {
                buffer[i] = this.var3();
            }
            return buffer;
        }

        internal void mtd833(int var1)
        {
            if ((var1 < 0) || (var1 > this.mtd736))
            {
                throw new Exception("Error in reading FontFile");
            }
            this._var1 = var1;
        }

        internal void mtd834(int var4)
        {
            this.mtd833(this.mtd832 + var4);
        }

        internal ushort mtd835()
        {
            if ((this.mtd832 < 0) || (this.mtd832 >= (this.mtd736 - 1)))
            {
                throw new Exception("Error in reading FontFile");
            }
            if (BitConverter.IsLittleEndian)
            {
                return (ushort) ((this.var3() << 8) + this.var3());
            }
            return (ushort) (this.var3() + (this.var3() << 8));
        }

        internal uint mtd837()
        {
            if ((this.mtd832 < 0) || (this.mtd832 >= (this.mtd736 - 3)))
            {
                throw new Exception("Error in reading FontFile");
            }
            if (BitConverter.IsLittleEndian)
            {
                return (uint) ((((this.var3() << 0x18) + (this.var3() << 0x10)) + (this.var3() << 8)) + this.var3());
            }
            return (uint) (((this.var3() + (this.var3() << 8)) + (this.var3() << 0x10)) + (this.var3() << 0x18));
        }

        internal short mtd892()
        {
            if ((this.mtd832 < 0) || (this.mtd832 >= (this.mtd736 - 1)))
            {
                throw new Exception("Error in reading FontFile");
            }
            if (BitConverter.IsLittleEndian)
            {
                return (short) ((this.var3() << 8) + this.var3());
            }
            return (short) (this.var3() + (this.var3() << 8));
        }

        internal byte mtd900()
        {
            if ((this.mtd832 < 0) || (this.mtd832 >= this.mtd736))
            {
                throw new Exception("Error in reading FontFile");
            }
            return this.var3();
        }

        internal sbyte mtd901()
        {
            return Convert.ToSByte(this.mtd900());
        }

        internal int mtd902()
        {
            if ((this.mtd832 < 0) || (this.mtd832 >= (this.mtd736 - 3)))
            {
                throw new Exception("Error in reading FontFile");
            }
            if (BitConverter.IsLittleEndian)
            {
                return ((((this.var3() << 0x18) + (this.var3() << 0x10)) + (this.var3() << 8)) + this.var3());
            }
            return (((this.var3() + (this.var3() << 8)) + (this.var3() << 0x10)) + (this.var3() << 0x18));
        }

        internal mtd903 mtd904()
        {
            if ((this.mtd832 < 0) || (this.mtd832 >= (this.mtd736 - 9)))
            {
                throw new Exception("Error in reading FontFile");
            }
            mtd903 mtd = new mtd903();
            mtd.mtd905 = this.var3();
            mtd.mtd906 = this.var3();
            mtd.mtd907 = this.var3();
            mtd.mtd908 = this.var3();
            mtd.mtd909 = this.var3();
            mtd.mtd910 = this.var3();
            mtd.mtd911 = this.var3();
            mtd.mtd912 = this.var3();
            mtd.mtd913 = this.var3();
            mtd.mtd914 = this.var3();
            return mtd;
        }

        internal string mtd915(int var6)
        {
            string str = "";
            for (int i = 0; i < var6; i += 2)
            {
                str = str + this.mtd835();
            }
            return str;
        }

        private byte var3()
        {
            return this._var0[this._var1++];
        }

        internal int mtd736
        {
            get
            {
                return this._var0.Length;
            }
        }

        internal int mtd832
        {
            get
            {
                return this._var1;
            }
        }
    }
}

