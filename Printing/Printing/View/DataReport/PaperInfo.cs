namespace Nistec.Printing.View
{
    using System;
    using System.Drawing;
    using System.Drawing.Printing;

    public class PaperInfo
    {
        private string _var0 = null;
        private PaperKind _var1;
        private Size _var2;
        private bool _var3;

        public PaperInfo(PaperKind paperkind, Size size, bool metric)
        {
            this._var1 = paperkind;
            this._var2 = size;
            this._var3 = metric;
        }

        private static SizeF var4(Size var2, bool var3)
        {
            if (var3)
            {
                return new SizeF(((float) var2.Width) / 25.4f, ((float) var2.Height) / 25.4f);
            }
            return new SizeF(((float) var2.Width) / 1000f, ((float) var2.Height) / 1000f);
        }

        public PaperKind Kind
        {
            get
            {
                return this._var1;
            }
        }

        public bool Metric
        {
            get
            {
                return this._var3;
            }
        }

        public string Name
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

        public SizeF SizeInInch
        {
            get
            {
                return var4(this._var2, this._var3);
            }
        }
    }
}

