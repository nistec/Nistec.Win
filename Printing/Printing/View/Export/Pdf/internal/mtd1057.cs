namespace Nistec.Printing.View.Pdf
{
    using System;

    internal class mtd1057
    {
        private mtd1055 _var0;
        private float _var1;
        private float _var2;
        private float _var3;

        internal mtd1057(mtd1055 var0)
        {
            this._var0 = var0;
            this._var1 = 0f;
            this._var2 = 0f;
            this._var3 = 0f;
        }

        internal mtd1057(mtd1055 var0, float var1, float var2)
        {
            this._var0 = var0;
            this._var1 = var1;
            this._var2 = var2;
            this._var3 = 0f;
        }

        internal mtd1057(mtd1055 var0, float var1, float var2, float var3)
        {
            this._var0 = var0;
            this._var1 = var1;
            this._var2 = var2;
            this._var3 = var3;
        }

        internal float mtd1056
        {
            get
            {
                return this._var3;
            }
            set
            {
                this._var3 = value;
            }
        }

        internal float mtd28
        {
            get
            {
                return this._var2;
            }
            set
            {
                this._var2 = value;
            }
        }

        internal float mtd29
        {
            get
            {
                return this._var1;
            }
            set
            {
                this._var1 = value;
            }
        }

        internal mtd1055 mtd777
        {
            get
            {
                return this._var0;
            }
            set
            {
                this._var0 = value;
            }
        }
    }
}

