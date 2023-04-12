namespace Nistec.Charts.Statistics
{
    using System;

    internal class Punct
    {
        private double er;
        private double px;
        private double py;
        private double pyr;

        internal Punct(double x, double y)
        {
            this.px = x;
            this.py = y;
        }

        internal double Er//eroare
        {
            get
            {
                return Math.Round(this.er, 4);
            }
            set
            {
                this.er = value;
            }
        }

        internal double x
        {
            get
            {
                return this.px;
            }
            set
            {
                this.px = value;
            }
        }

        internal double y
        {
            get
            {
                return this.py;
            }
            set
            {
                this.py = value;
            }
        }

        internal double y_recalc
        {
            get
            {
                return Math.Round(this.pyr, 4);
            }
            set
            {
                this.pyr = value;
            }
        }
    }
}

