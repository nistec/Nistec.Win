namespace MControl.Printing.Pdf.Drawing
{
    using System;

    public class Column
    {
        private float _b0;

        internal Column(float b0)
        {
            this._b0 = b0;
        }

        public float Width
        {
            get
            {
                return this._b0;
            }
        }
    }
}

