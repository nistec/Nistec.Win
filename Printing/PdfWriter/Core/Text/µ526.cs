namespace MControl.Printing.Pdf.Core.Text
{
    using System;
    using System.Reflection;

    internal class A526
    {
        private float _b0;
        private float _b1;
        private float _b2;
        private float _b3;
        private bool _b4;
        private float _b5;
        private A565 _b6;

        internal A526(A568 b7)
        {
            this._b2 = b7.A572;
            this._b1 = b7.A528;
            this._b4 = b7.A506;
            this._b3 = b7.A212;
            this._b5 = b7.A211;
            this._b0 = b7.A569;
            this._b6 = new A565(b7.A565);
        }

        internal int A2
        {
            get
            {
                return this._b6.A2;
            }
        }

        internal float A211
        {
            get
            {
                return this._b5;
            }
        }

        internal float A212
        {
            get
            {
                return this._b3;
            }
        }

        internal bool A506
        {
            get
            {
                return this._b4;
            }
        }

        internal virtual float A527
        {
            get
            {
                return 0f;
            }
        }

        internal float A528
        {
            get
            {
                return this._b1;
            }
        }

        internal float A569
        {
            get
            {
                return this._b0;
            }
        }

        internal float A572
        {
            get
            {
                return this._b2;
            }
        }

        internal A566 this[int b8]
        {
            get
            {
                return this._b6[b8];
            }
        }
    }
}

