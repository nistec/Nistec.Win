namespace MControl.Printing.Pdf.Core.IO
{
    using System;

    internal class A243 : A55
    {
        protected byte[] _A550;
        protected int _A551;

        internal A243()
        {
            this._A551 = 0;
            this._A550 = new byte[0x400];
        }

        internal A243(long b0)
        {
            this._A551 = 0;
            if (b0 < 0x400L)
            {
                b0 = 0x400L;
            }
            this._A550 = new byte[b0];
        }

        protected override void _A548(byte b1)
        {
            this.b2();
            this._A550[this._A551] = b1;
            this._A551++;
        }

        internal void A552()
        {
            byte[] sourceArray = this._A550;
            this._A550 = new byte[this._A551];
            Array.Copy(sourceArray, 0, this._A550, 0, this._A551);
        }

        private void b2()
        {
            if (this._A551 >= this._A550.Length)
            {
                byte[] destinationArray = new byte[2 * this._A550.Length];
                Array.Copy(this._A550, destinationArray, this._A551);
                this._A550 = destinationArray;
            }
        }

        internal override int A2
        {
            get
            {
                return this._A551;
            }
        }

        internal byte[] A221
        {
            get
            {
                return this._A550;
            }
        }
    }
}

