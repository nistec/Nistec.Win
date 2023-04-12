namespace Nistec.Printing.View.Pdf
{
    using Nistec.Printing.View;
    using System;
    using System.Drawing;

    internal class mtd984
    {
        internal float mtd764 = 1f;
        internal float mtd765 = 1f;
        internal mtd641 mtd987 = null;
        internal float mtd988 = 8f;
        internal Color mtd989 = Color.Black;
        internal LineStyle mtd990 = LineStyle.Solid;
        internal float mtd991 = 1f;
        internal Color mtd992 = Color.Black;
        internal RectangleF mtd994;

        internal mtd984()
        {
        }

        internal mtd984 mtd253()
        {
            mtd984 mtd = new mtd984();
            mtd.mtd991 = this.mtd991;
            mtd.mtd990 = this.mtd990;
            mtd.mtd992 = this.mtd992;
            mtd.mtd989 = this.mtd989;
            mtd.mtd994 = this.mtd994;
            mtd.mtd988 = this.mtd988;
            mtd.mtd987 = this.mtd987;
            mtd.mtd764 = this.mtd764;
            mtd.mtd765 = this.mtd765;
            return mtd;
        }

        internal mtd641 Font//mtd132
        {
            get
            {
                return this.mtd987;
            }
        }

        internal Color BorderColor
        {
            get
            {
                return this.mtd992;
            }
        }

        internal LineStyle LineStyle//mtd42
        {
            get
            {
                return this.mtd990;
            }
        }

        internal Color mtd995
        {
            get
            {
                return this.mtd989;
            }
        }

        internal float mtd996
        {
            get
            {
                return this.mtd991;
            }
        }

        internal float mtd997
        {
            get
            {
                return this.mtd988;
            }
        }

        internal float mtd998
        {
            get
            {
                return this.mtd764;
            }
        }

        internal float mtd999
        {
            get
            {
                return this.mtd765;
            }
        }
    }
}

