namespace Nistec.Printing.View.Pdf
{
    using System;

    internal class mtd844
    {
        private uint _var0;
        private uint _var1;
        private uint _var2;
        private byte[] _var3;

        internal mtd844()
        {
        }

        internal void mtd172(mtd708 var4)
        {
            if (var4.mtd789.Length == 4)
            {
                this._var3 = new byte[4];
                for (int i = 0; i < 4; i++)
                {
                    this._var3[i] = Convert.ToByte(var4.mtd789[i]);
                }
                this._var0 = 0;
                this._var2 = 0;
                this._var1 = (uint) var4.mtd736;
            }
        }

        internal void mtd710(mtd829 var5)
        {
            var5.mtd830(this._var3);
            var5.mtd839(this._var0);
            var5.mtd839(this._var2);
            var5.mtd839(this._var1);
        }

        internal void mtd842(mtd825 var5)
        {
            this._var3 = var5.mtd826(4);
            this._var0 = var5.mtd837();
            this._var2 = var5.mtd837();
            this._var1 = var5.mtd837();
        }

        internal void mtd847(mtd829 var5)
        {
            if (this._var0 == 0)
            {
                this._var0 = var5.mtd851(this.mtd737, this.mtd736);
            }
            var5.mtd834(4);
            var5.mtd839(this._var0);
            var5.mtd834(8);
        }

        internal void mtd849(mtd829 var5)
        {
            var5.mtd834(8);
            var5.mtd839(this._var2);
            var5.mtd834(4);
        }

        internal void mtd850(int var2)
        {
            if (this._var2 == 0)
            {
                this._var2 = (uint) var2;
            }
        }

        internal string mtd116
        {
            get
            {
                if (this._var3 == null)
                {
                    return null;
                }
                string str = "";
                for (int i = 0; i < this._var3.Length; i++)
                {
                    str = str + Convert.ToChar(this._var3[i]);
                }
                return str;
            }
        }

        internal int mtd736
        {
            get
            {
                return Convert.ToInt32(this._var1);
            }
        }

        internal int mtd737
        {
            get
            {
                return Convert.ToInt32(this._var2);
            }
        }

        internal static int mtd845
        {
            get
            {
                return 0x10;
            }
        }
    }
}

