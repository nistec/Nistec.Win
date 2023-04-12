namespace Nistec.Printing.View.Pdf
{
    using System;
    using System.Drawing;

    internal abstract class mtd1000
    {
        protected mtd641 _mtd1001;
        protected Color _mtd1002;
        protected float _mtd988;

        internal mtd1000(mtd641 var0, float var1, Color var2)
        {
            if (var0 == null)
            {
                this._mtd1001 = mtd1003();
            }
            else
            {
                this._mtd1001 = var0;
            }
            if (var1 < 1f)
            {
                this._mtd988 = 8f;
            }
            else
            {
                this._mtd988 = var1;
            }
            this._mtd1002 = var2;
        }

        internal static mtd641 mtd1003()
        {
            return new mtd641(StandardFonts.mtd684, FontStyle.Regular);
        }

        internal virtual void mtd963(mtd641 var0)
        {
        }

        internal Color mtd1004
        {
            get
            {
                return this._mtd1002;
            }
        }

        internal virtual bool mtd1005
        {
            get
            {
                return false;
            }
        }

        internal mtd641 Font//mtd132
        {
            get
            {
                return this._mtd1001;
            }
        }

        internal virtual Color mtd700
        {
            get
            {
                return Color.Transparent;
            }
        }

        internal virtual bool mtd701
        {
            get
            {
                return false;
            }
        }

        internal float mtd997
        {
            get
            {
                return this._mtd988;
            }
        }
    }
}

