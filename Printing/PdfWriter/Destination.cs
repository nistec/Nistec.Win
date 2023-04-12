namespace MControl.Printing.Pdf
{
    using System;

    public class Destination
    {
        private MControl.Printing.Pdf.Page _b0;
        private float _b1;
        private float _b2;
        private float _b3;

        public Destination(MControl.Printing.Pdf.Page page)
        {
            this._b0 = page;
            this._b1 = 0f;
            this._b2 = 0f;
            this._b3 = 0f;
        }

        public Destination(MControl.Printing.Pdf.Page page, float top, float left)
        {
            this._b0 = page;
            this._b1 = top;
            this._b2 = left;
            this._b3 = 0f;
        }

        public Destination(MControl.Printing.Pdf.Page page, float top, float left, float zoom)
        {
            this._b0 = page;
            this._b1 = top;
            this._b2 = left;
            this._b3 = zoom;
        }

        public float Left
        {
            get
            {
                return this._b2;
            }
            set
            {
                this._b2 = value;
            }
        }

        public MControl.Printing.Pdf.Page Page
        {
            get
            {
                return this._b0;
            }
            set
            {
                this._b0 = value;
            }
        }

        public float Top
        {
            get
            {
                return this._b1;
            }
            set
            {
                this._b1 = value;
            }
        }

        public float Zoom
        {
            get
            {
                return this._b3;
            }
            set
            {
                this._b3 = value;
            }
        }
    }
}

