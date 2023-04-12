namespace MControl.Printing.Pdf.Core.Fonts
{
    using System;

    internal class A347
    {
        private A311 _b0;
        private short _b1;
        private int _b2;
        private short _b3;
        private short _b4;
        private short _b5;
        private short _b6;

        internal A347(int b2)
        {
            this._b2 = b2;
        }

        internal void A312(A286 b7)
        {
            this._b1 = b7.A305();
            this._b4 = b7.A305();
            this._b6 = b7.A305();
            this._b3 = b7.A305();
            this._b5 = b7.A305();
            this.b8(b7);
        }

        internal void A54(A290 b7)
        {
            b7.A363(this._b1);
            b7.A363(this._b4);
            b7.A363(this._b6);
            b7.A363(this._b3);
            b7.A363(this._b5);
            if (this._b0 != null)
            {
                this._b0.A54(b7);
            }
        }

        private void b8(A286 b7)
        {
            if (this._b1 >= 0)
            {
                this._b0 = new A311();
            }
            else
            {
                this._b0 = new A310();
            }
            this._b0.A312(b7, this._b2 - 10);
        }

        internal int A2
        {
            get
            {
                return this._b2;
            }
        }

        internal A311 A349
        {
            get
            {
                return this._b0;
            }
        }

        internal short A359
        {
            get
            {
                return this._b3;
            }
        }

        internal short A360
        {
            get
            {
                return this._b4;
            }
        }

        internal short A361
        {
            get
            {
                return this._b5;
            }
        }

        internal short A362
        {
            get
            {
                return this._b6;
            }
        }
    }
}

