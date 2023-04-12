namespace Nistec.Printing.View
{
    using System;

    internal class mtd126
    {
        internal bool mtd130;
        internal Border _Border;//mtd131;
        internal McLocation _Location;//mtd144;
        internal McReportControl RptControl;//mtd212;
        internal bool mtd254;

        internal mtd126()
        {
        }

        internal mtd126(ref McReportControl var0)
        {
            this.RptControl = var0;
            this._Location = new McLocation(var0.Left, var0.Top, var0.Width, var0.Height, false);
            this._Border = var0.Border;
            this.mtd130 = var0.Visible;
        }

        internal virtual float mtd128
        {
            get
            {
                return this._Location.Left;
            }
        }

        internal virtual float mtd129
        {
            get
            {
                return this._Location.Top;
            }
            set
            {
                McLocation.mtd257(ref this._Location, value);
            }
        }

        internal virtual float Width//mtd30
        {
            get
            {
                return this._Location.Width;
            }
        }

        internal virtual float Height//mtd31
        {
            get
            {
                return this._Location.Height;
            }
            set
            {
                McLocation.mtd243(ref this._Location, value);
            }
        }

        internal ControlType ControlType//mtd66
        {
            get
            {
                return this.RptControl.Type;
            }
        }

        internal string Name//mtd91
        {
            get
            {
                return this.RptControl.Name;
            }
        }
    }
}

