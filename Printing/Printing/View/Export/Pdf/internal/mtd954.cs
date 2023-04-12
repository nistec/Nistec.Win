namespace Nistec.Printing.View.Pdf
{
    using System;
    using System.Drawing;

    internal class mtd954 : mtd1000
    {
        private Color _var0;
        private bool _var1;

        internal mtd954() : base(mtd1000.mtd1003(), 8f, Color.Black)
        {
            this._var0 = Color.Transparent;
            this._var1 = false;
        }

        internal mtd954(mtd641 var2, float var3) : base(var2, var3, Color.Black)
        {
            this._var0 = Color.Transparent;
            this._var1 = false;
        }

        internal mtd954(mtd641 var2, float var3, Color var4) : base(var2, var3, var4)
        {
            this._var0 = Color.Transparent;
            this._var1 = false;
        }

        internal mtd954(mtd641 var2, float var3, Color var4, Color var0, bool var1) : base(var2, var3, var4)
        {
            this._var0 = var0;
            this._var1 = var1;
        }

        internal override void mtd963(mtd641 font)
        {
            base._mtd1001 = font;
        }

        internal override bool mtd1005
        {
            get
            {
                return (this._var1 | (this.mtd700 != Color.Transparent));
            }
        }

        internal override Color mtd700
        {
            get
            {
                return this._var0;
            }
        }

        internal override bool mtd701
        {
            get
            {
                return this._var1;
            }
        }
    }
}

