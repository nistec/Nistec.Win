namespace Nistec.Printing.View
{
    using System;
    using System.Drawing.Printing;
    using System.Reflection;

    public class PaperInfos
    {
        private PaperInfo[] _var0 = new PaperInfo[120];
        private int _var1;

        internal PaperInfos()
        {
        }

        internal void mtd2(PaperInfo var2)
        {
            this.var3();
            this._var0[this._var1] = var2;
            this._var1++;
        }

        private void var3()
        {
            if (this._var1 >= this._var0.Length)
            {
                PaperInfo[] destinationArray = new PaperInfo[2 * this._var0.Length];
                Array.Copy(this._var0, destinationArray, this._var1);
                this._var0 = destinationArray;
            }
        }

        public PaperInfo this[PaperKind paperkind]
        {
            get
            {
                for (int i = 0; i < this._var1; i++)
                {
                    PaperInfo info = this._var0[i];
                    if (paperkind == info.Kind)
                    {
                        return info;
                    }
                }
                return null;
            }
        }

        public PaperInfo this[int index]
        {
            get
            {
                if ((index > -1) && (index < this._var1))
                {
                    return this._var0[index];
                }
                return null;
            }
        }

        public int Size
        {
            get
            {
                return this._var1;
            }
        }
    }
}

