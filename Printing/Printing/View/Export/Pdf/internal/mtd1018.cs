namespace Nistec.Printing.View.Pdf
{
    using System;
    using System.Drawing;

    internal class mtd1018
    {
        private float _var0;
        private bool _var1;
        private float _var2;
        private Color _var3;

        internal mtd1018(float var0, float var2, Color var3, bool var1)
        {
            this._var0 = var0;
            this._var2 = var2;
            this._var1 = var1;
            this._var3 = var3;
        }

        internal bool mtd1037
        {
            get
            {
                return this._var1;
            }
        }

        internal float mtd128
        {
            get
            {
                return this._var0;
            }
        }

        internal float mtd30
        {
            get
            {
                return this._var2;
            }
        }

        internal Color mtd59
        {
            get
            {
                return this._var3;
            }
        }
    }
}

