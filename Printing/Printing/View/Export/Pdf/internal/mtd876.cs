namespace Nistec.Printing.View.Pdf
{
    using System;

    internal class mtd876
    {
        private mtd841 _var0;
        private short _var1;
        private int _var2;
        private short _var3;
        private short _var4;
        private short _var5;
        private short _var6;

        internal mtd876(int var2)
        {
            this._var2 = var2;
        }

        internal void mtd710(mtd829 var7)
        {
            var7.mtd893(this._var1);
            var7.mtd893(this._var4);
            var7.mtd893(this._var6);
            var7.mtd893(this._var3);
            var7.mtd893(this._var5);
            if (this._var0 != null)
            {
                this._var0.mtd710(var7);
            }
        }

        internal void mtd842(mtd825 var7)
        {
            this._var1 = var7.mtd892();
            this._var4 = var7.mtd892();
            this._var6 = var7.mtd892();
            this._var3 = var7.mtd892();
            this._var5 = var7.mtd892();
            this.var8(var7);
        }

        private void var8(mtd825 var7)
        {
            if (this._var1 >= 0)
            {
                this._var0 = new mtd841();
            }
            else
            {
                this._var0 = new mtd840();
            }
            this._var0.mtd842(var7, this._var2 - 10);
        }

        internal int mtd32
        {
            get
            {
                return this._var2;
            }
        }

        internal mtd841 mtd878
        {
            get
            {
                return this._var0;
            }
        }

        internal short mtd888
        {
            get
            {
                return this._var3;
            }
        }

        internal short mtd889
        {
            get
            {
                return this._var4;
            }
        }

        internal short mtd890
        {
            get
            {
                return this._var5;
            }
        }

        internal short mtd891
        {
            get
            {
                return this._var6;
            }
        }
    }
}

