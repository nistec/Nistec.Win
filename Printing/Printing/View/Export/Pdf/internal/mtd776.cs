namespace Nistec.Printing.View.Pdf
{
    using System;

    internal class mtd776
    {
        private string _var0;
        private mtd1055 _var1;
        private float _var2;
        private float _var3;
        private float _var4;
        private bool _var5;
        private int _var6;
        private mtd776 _var7;
        private mtd762 _var8;

        internal mtd776(string var0, mtd1055 var1)
        {
            this._var0 = var0;
            this._var1 = var1;
            this._var2 = 0f;
            this._var3 = 0f;
            this._var4 = 0f;
            this._var5 = true;
            this._var8 = new mtd762(this);
        }

        internal mtd776(string var0, mtd1055 var1, float var3)
        {
            this._var0 = var0;
            this._var1 = var1;
            this._var2 = 0f;
            this._var3 = var3;
            this._var4 = 0f;
            this._var5 = true;
            this._var8 = new mtd762(this);
        }

        internal mtd776(string var0, mtd1055 var1, float var3, float var2)
        {
            this._var0 = var0;
            this._var1 = var1;
            this._var2 = var2;
            this._var3 = var3;
            this._var4 = 0f;
            this._var5 = true;
            this._var8 = new mtd762(this);
        }

        internal mtd776(string var0, mtd1055 var1, float var3, float var2, float var4)
        {
            this._var0 = var0;
            this._var1 = var1;
            this._var2 = var2;
            this._var3 = var3;
            this._var4 = var4;
            this._var5 = true;
            this._var8 = new mtd762(this);
        }

        internal mtd776(string var0, mtd1055 var1, float var3, float var2, float var4, bool var5)
        {
            this._var0 = var0;
            this._var1 = var1;
            this._var2 = var2;
            this._var3 = var3;
            this._var4 = var4;
            this._var5 = var5;
            this._var8 = new mtd762(this);
        }

        internal void mtd780(mtd757 var9)
        {
            int num2;
            var9.mtd759 = (num2 = var9.mtd759) + 1;
            this._var6 = num2;
            for (int i = 0; i < this._var8.mtd166; i++)
            {
                this._var8[i].mtd780(var9);
            }
        }

        internal float mtd1056
        {
            get
            {
                return this._var4;
            }
        }

        internal mtd776 mtd208
        {
            get
            {
                return this._var7;
            }
            set
            {
                this._var7 = value;
            }
        }

        internal float mtd28
        {
            get
            {
                return this._var2;
            }
        }

        internal float mtd29
        {
            get
            {
                return this._var3;
            }
        }

        internal int mtd759
        {
            get
            {
                return this._var6;
            }
        }

        internal string mtd767
        {
            get
            {
                return this._var0;
            }
        }

        internal mtd1055 mtd777
        {
            get
            {
                return this._var1;
            }
        }

        internal mtd762 mtd778
        {
            get
            {
                return this._var8;
            }
        }

        internal bool mtd779
        {
            get
            {
                return this._var5;
            }
        }
    }
}

