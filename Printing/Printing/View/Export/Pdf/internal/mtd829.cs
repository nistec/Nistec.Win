namespace Nistec.Printing.View.Pdf
{
    using System;
    using System.IO;

    internal class mtd829
    {
        private MemoryStream _var0 = new MemoryStream();

        internal mtd829()
        {
        }

        internal void mtd800()
        {
            int num = this.mtd832 % 4;
            for (int i = 4; i > num; i--)
            {
                this.mtd925(0);
            }
        }

        internal void mtd830(byte[] var0)
        {
            for (int i = 0; i < var0.Length; i++)
            {
                this.mtd925(var0[i]);
            }
        }

        internal void mtd830(byte[] var0, bool var4)
        {
            if (var4)
            {
                for (int i = var0.Length - 1; i >= 0; i--)
                {
                    this.mtd925(var0[i]);
                }
            }
            else
            {
                this.mtd830(var0);
            }
        }

        internal void mtd833(int var2)
        {
            this._var0.Seek((long) var2, SeekOrigin.Begin);
        }

        internal void mtd834(int var1)
        {
            this._var0.Position += var1;
        }

        internal void mtd838(ushort var5)
        {
            byte[] bytes = BitConverter.GetBytes(var5);
            this.mtd830(bytes, BitConverter.IsLittleEndian);
        }

        internal void mtd839(uint var5)
        {
            byte[] bytes = BitConverter.GetBytes(var5);
            this.mtd830(bytes, BitConverter.IsLittleEndian);
        }

        internal uint mtd851(int var6, int var7)
        {
            uint num5;
            int num = var7 % 4;
            if (num > 0)
            {
                var7 += 4 - num;
            }
            var7 /= 4;
            int num2 = this.mtd832;
            try
            {
                this.mtd833(var6);
                uint num3 = 0;
                for (int i = 0; i < var7; i++)
                {
                    num3 += this.var8();
                }
                num5 = num3;
            }
            finally
            {
                this.mtd833(num2);
            }
            return num5;
        }

        internal void mtd893(short var5)
        {
            byte[] bytes = BitConverter.GetBytes(var5);
            this.mtd830(bytes, BitConverter.IsLittleEndian);
        }

        internal void mtd922(string var5)
        {
            for (int i = 0; i < var5.Length; i++)
            {
                this.mtd838(var5[i]);
            }
        }

        internal void mtd923(mtd903 var5)
        {
            this.mtd925(var5.mtd905);
            this.mtd925(var5.mtd906);
            this.mtd925(var5.mtd907);
            this.mtd925(var5.mtd908);
            this.mtd925(var5.mtd909);
            this.mtd925(var5.mtd910);
            this.mtd925(var5.mtd911);
            this.mtd925(var5.mtd912);
            this.mtd925(var5.mtd913);
            this.mtd925(var5.mtd914);
        }

        internal void mtd924(sbyte var5)
        {
            this.mtd925((byte) var5);
        }

        internal void mtd925(byte var3)
        {
            this._var0.WriteByte(var3);
        }

        internal void mtd926(int var5)
        {
            byte[] bytes = BitConverter.GetBytes(var5);
            this.mtd830(bytes, BitConverter.IsLittleEndian);
        }

        private uint var8()
        {
            if ((this.mtd832 < 0) || (this.mtd832 >= (this.mtd736 - 3)))
            {
                throw new Exception("error when working with .ttf file");
            }
            if (BitConverter.IsLittleEndian)
            {
                return (uint) ((((this._var0.ReadByte() << 0x18) + (this.var9() << 0x10)) + (this.var9() << 8)) + this.var9());
            }
            return (uint) (((this.var9() + (this.var9() << 8)) + (this.var9() << 0x10)) + (this.var9() << 0x18));
        }

        private byte var9()
        {
            return (byte) this._var0.ReadByte();
        }

        internal int mtd736
        {
            get
            {
                return (int) this._var0.Length;
            }
        }

        internal byte[] mtd784
        {
            get
            {
                return this._var0.ToArray();
            }
        }

        internal int mtd832
        {
            get
            {
                return (int) this._var0.Position;
            }
        }
    }
}

