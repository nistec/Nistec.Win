namespace Nistec.Printing.View.Pdf
{
    using System;

    internal abstract class mtd711
    {
        internal static char[] mtd644 = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };

        internal mtd711()
        {
        }

        protected abstract void _mtd1006(byte value);
        internal void mtd1007(string var3)
        {
            this.mtd710(var3);
            this._mtd1006(20);
        }

        internal void mtd710(byte var0)
        {
            this._mtd1006(var0);
        }

        internal void mtd710(string var3)
        {
            int length = var3.Length;
            for (int i = 0; i < length; i++)
            {
                this._mtd1006((byte) var3[i]);
            }
        }

        internal void mtd710(byte[] var1, int var2)
        {
            for (int i = 0; i < var2; i++)
            {
                this._mtd1006(var1[i]);
            }
        }

        internal void mtd714(byte[] var1)
        {
            for (int i = 0; i < var1.Length; i++)
            {
                byte num2 = var1[i];
                this._mtd1006((byte) mtd644[num2 >> 4]);
                this._mtd1006((byte) mtd644[num2 & 15]);
            }
        }

        internal void mtd715(string var3)
        {
            this.mtd710(var3);
            this._mtd1006(13);
        }

        internal void mtd783(string var3)
        {
            this.mtd710(var3);
            this._mtd1006(13);
            this._mtd1006(10);
        }

        internal void mtd968(string var3)
        {
            this.mtd710(var3);
            this._mtd1006(10);
        }

        internal abstract int mtd32 { get; }
    }
}

