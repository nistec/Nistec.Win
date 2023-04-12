namespace MControl.Printing.Pdf.Core.Text
{
    using System;

    internal abstract class A566
    {
        private A523 _b0;

        internal A566(A523 b0)
        {
            this._b0 = b0;
        }

        internal virtual float A211
        {
            get
            {
                return 0f;
            }
            set
            {
            }
        }

        internal virtual int A293
        {
            get
            {
                return 0;
            }
            set
            {
            }
        }

        internal virtual bool A575
        {
            get
            {
                return false;
            }
        }

        internal virtual float A576
        {
            set
            {
            }
        }

        internal virtual int A579
        {
            get
            {
                return -1;
            }
        }

        internal A523 A580
        {
            get
            {
                return this._b0;
            }
        }
    }
}

