namespace Nistec.Printing.View.Pdf
{
    using System;
    using System.Drawing;

    internal class mtd811
    {
        protected PDFDocument _mtd756;
        protected int _mtd759;
        protected string _mtd808;
        protected FontStyle _mtd809;
        protected int _mtd812;
        protected int _mtd813;
        protected int _mtd814 = 0;
        protected string _mtd818;
        protected bool _mtd932;

        internal mtd811()
        {
        }

        internal void mtd172(PDFDocument var0, int var1)
        {
            this._mtd756 = var0;
            this._mtd818 = string.Format("F{0}", var1);
        }

        internal virtual void mtd710(ref mtd711 var6)
        {
        }

        internal virtual void mtd780()
        {
            int num;
            mtd757 mtd1 = this._mtd756.mtd757;
            mtd1.mtd759 = (num = mtd1.mtd759) + 1;
            this._mtd759 = num;
        }

        internal virtual float mtd815(char var2)
        {
            return 0f;
        }

        internal virtual float mtd815(ushort var3)
        {
            return 0f;
        }

        internal virtual float mtd816(char var2, float var4)
        {
            return 0f;
        }

        internal virtual float mtd817(string var5, float var4)
        {
            return 0f;
        }

        internal virtual void mtd935(char var2)
        {
        }

        internal virtual void mtd936(string var5)
        {
        }

        internal virtual string mtd937(string var5)
        {
            string str = string.Empty;
            for (int i = 0; i < var5.Length; i++)
            {
                char ch = var5[i];
                if (ch > '\x00ff')
                {
                    return mtd620.mtd653(var5);
                }
                str = str + mtd620.mtd651(ch);
            }
            return string.Format("{0}{1}{2}", "(", str, ")");
        }

        internal virtual Font Font//mtd132
        {
            get
            {
                return null;
            }
        }

        internal int mtd759
        {
            get
            {
                return this._mtd759;
            }
        }

        internal string mtd763
        {
            get
            {
                return this._mtd818;
            }
        }

        internal string mtd886
        {
            get
            {
                return this._mtd808;
            }
        }

        internal FontStyle mtd934
        {
            get
            {
                return this._mtd809;
            }
        }

        internal bool mtd938
        {
            get
            {
                return this._mtd932;
            }
        }

        internal int mtd939
        {
            get
            {
                return this._mtd812;
            }
        }

        internal int mtd940
        {
            get
            {
                return this._mtd813;
            }
        }

        internal int mtd941
        {
            get
            {
                return this._mtd814;
            }
        }
    }
}

