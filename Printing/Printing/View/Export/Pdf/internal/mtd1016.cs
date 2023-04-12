namespace Nistec.Printing.View.Pdf
{
    using System;
    using System.Reflection;

    internal class mtd1016
    {
        private float _var0;
        private float _var1;
        private float _var2;
        private float _var3;
        private bool _var4;
        private float _var5;
        private mtd1021 _var6;

        internal mtd1016(mtd230 var7)
        {
            this._var2 = var7.mtd1027;
            this._var1 = var7.mtd237;
            this._var4 = var7.mtd1005;
            this._var3 = var7.mtd31;
            this._var5 = var7.mtd30;
            this._var0 = var7.mtd1023;
            this._var6 = new mtd1021(var7.mtd1021);
        }

        internal bool mtd1005
        {
            get
            {
                return this._var4;
            }
        }

        internal float mtd1023
        {
            get
            {
                return this._var0;
            }
        }

        internal virtual float mtd1026
        {
            get
            {
                return 0f;
            }
        }

        internal float mtd1027
        {
            get
            {
                return this._var2;
            }
        }

        internal float mtd237
        {
            get
            {
                return this._var1;
            }
        }

        internal float mtd30
        {
            get
            {
                return this._var5;
            }
        }

        internal float mtd31
        {
            get
            {
                return this._var3;
            }
        }

        internal int mtd32
        {
            get
            {
                return this._var6.mtd32;
            }
        }

        internal mtd1022 this[int var8]
        {
            get
            {
                return this._var6[var8];
            }
        }
    }
}

