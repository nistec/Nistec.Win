namespace Nistec.Printing.View.Pdf
{
    using System;
    using System.Text;

    internal abstract class mtd712
    {
        protected SecurityManager _mtd790;
        protected byte[] _mtd791;
        protected byte[] _mtd792;
        protected byte[] _mtd793;
        protected byte[] _mtd794;
        protected int _mtd795;
        protected mtd796 _mtd797;
        protected mtd798 _mtd799;
        private byte[] _var0;
        private PDFDocument _var1;
        private int _var2;

        internal mtd712(PDFDocument var1)
        {
            this._mtd790 = var1.SecurityManager;
            this._var1 = var1;
            this._mtd797 = new mtd796();
            this._mtd799 = new mtd798();
            this._var0 = new byte[] { 
                40, 0xbf, 0x4e, 0x5e, 0x4e, 0x75, 0x8a, 0x41, 100, 0, 0x4e, 0x56, 0xff, 250, 1, 8, 
                0x2e, 0x2e, 0, 0xb6, 0xd0, 0x68, 0x3e, 0x80, 0x2f, 12, 0xa9, 0xfe, 100, 0x53, 0x69, 0x7a
             };
            this._mtd791 = null;
            this._mtd792 = null;
            this._mtd794 = null;
            this._mtd793 = null;
        }

        internal abstract void mtd172();
        internal virtual void mtd710(ref mtd711 var7)
        {
            this._var1.mtd757.mtd758(var7.mtd32, 0);
            var7.mtd715(string.Format("{0} 0 obj", this.mtd759));
            var7.mtd715("<<");
        }

        internal abstract byte[] mtd713(byte[] var4, int var5);
        internal abstract void mtd775(int var2, int var3);
        internal byte[] mtd801(string var6)
        {
            if (((var6 != null) && (var6 != string.Empty)) && (var6.Length > 0))
            {
                byte[] destinationArray = new byte[0x20];
                byte[] bytes = Encoding.ASCII.GetBytes(var6);
                Array.Copy(bytes, destinationArray, bytes.Length);
                Array.Copy(this._var0, 0, destinationArray, bytes.Length, 0x20 - bytes.Length);
                return destinationArray;
            }
            return this._var0;
        }

        internal virtual int mtd802()
        {
            int num = -4;
            if (!this._mtd790.AllowPrint)
            {
                num -= 4;
            }
            if (!this._mtd790.AllowEdit)
            {
                num -= 8;
            }
            if (!this._mtd790.AllowCopy)
            {
                num -= 0x10;
            }
            if (!this._mtd790.AllowUpdateAnnotsAndFields)
            {
                num -= 0x20;
            }
            return num;
        }

        internal int mtd759
        {
            get
            {
                if (this._var2 == 0)
                {
                    int num;
                    mtd757 mtd1 = this._var1.mtd757;
                    mtd1.mtd759 = (num = mtd1.mtd759) + 1;
                    this._var2 = num;
                }
                return this._var2;
            }
        }

        internal byte[] mtd789
        {
            get
            {
                return this._var1.mtd789;
            }
        }

        internal byte[] mtd800
        {
            get
            {
                return this._var0;
            }
        }
    }
}

