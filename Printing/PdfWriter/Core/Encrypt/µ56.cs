namespace MControl.Printing.Pdf.Core.Encrypt
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core.Element;
    using MControl.Printing.Pdf.Core.IO;
    using System;
    using System.Text;

    internal abstract class A56
    {
        protected SecurityManager _A227;
        protected byte[] _A228;
        protected byte[] _A229;
        protected byte[] _A230;
        protected byte[] _A231;
        protected int _A232;
        protected A233 _A234;
        protected A235 _A236;
        private byte[] _b0;
        private Document _b1;
        private int _b2;

        internal A56(Document b1)
        {
            this._A227 = b1.SecurityManager;
            this._b1 = b1;
            this._A234 = new A233();
            this._A236 = new A235();
            this._b0 = new byte[] { 
                40, 0xbf, 0x4e, 0x5e, 0x4e, 0x75, 0x8a, 0x41, 100, 0, 0x4e, 0x56, 0xff, 250, 1, 8, 
                0x2e, 0x2e, 0, 0xb6, 0xd0, 0x68, 0x3e, 0x80, 0x2f, 12, 0xa9, 0xfe, 100, 0x53, 0x69, 0x7a
             };
            this._A228 = null;
            this._A229 = null;
            this._A231 = null;
            this._A230 = null;
        }

        internal abstract void A100(int objIndex, int genID);
        internal abstract void A237();
        internal byte[] A239(string b3)
        {
            if (((b3 != null) && (b3 != string.Empty)) && (b3.Length > 0))
            {
                byte[] destinationArray = new byte[0x20];
                byte[] bytes = Encoding.ASCII.GetBytes(b3);
                Array.Copy(bytes, destinationArray, bytes.Length);
                Array.Copy(this._b0, 0, destinationArray, bytes.Length, 0x20 - bytes.Length);
                return destinationArray;
            }
            return this._b0;
        }

        internal virtual int A240()
        {
            int num = -4;
            if (!this._A227.AllowPrint)
            {
                num -= 4;
            }
            if (!this._A227.AllowEdit)
            {
                num -= 8;
            }
            if (!this._A227.AllowCopy)
            {
                num -= 0x10;
            }
            if (!this._A227.AllowUpdateAnnotsAndFields)
            {
                num -= 0x20;
            }
            return num;
        }

        internal virtual void A54(ref A55 b4)
        {
            this._b1.A93.A94(b4.A2, 0);
            b4.A59(string.Format("{0} 0 obj", this.A95));
            b4.A59("<<");
        }

        internal abstract byte[] A57(byte[] data, int size);

        internal byte[] A226
        {
            get
            {
                return this._b1.A226;
            }
        }

        internal byte[] A238
        {
            get
            {
                return this._b0;
            }
        }

        internal int A95
        {
            get
            {
                if (this._b2 == 0)
                {
                    int num;
                    A93 A1 = this._b1.A93;
                    A1.A95 = (num = A1.A95) + 1;
                    this._b2 = num;
                }
                return this._b2;
            }
        }
    }
}

