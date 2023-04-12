namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.IO;
    using System;

    public class Transformation : GraphicsElement
    {
        private float _b0 = 0f;
        private float _b1 = 0f;
        private float _b2 = 0f;
        private float _b3 = 0f;
        private float _b4 = 0f;
        private float _b5 = 1f;
        private float _b6 = 1f;
        private float _b7 = 0f;
        private float _b8 = 0f;

        internal override void A119(ref A120 b9, ref A112 b10)
        {
            if (this._b4 > 0f)
            {
                GraphicsElement.A436(this._b0, this._b1, this._b2, -this._b3, this._b4, ref b9, ref b10);
            }
            else
            {
                GraphicsElement.A170(this._b2, -this._b3, ref b10);
            }
            GraphicsElement.A512(this._b0, this._b1, this._b5, this._b6, ref b9, ref b10);
            GraphicsElement.A516(this._b0, this._b1, this._b7, this._b8, ref b9, ref b10);
        }

        public float Rotate
        {
            get
            {
                return this._b4;
            }
            set
            {
                this._b4 = value;
            }
        }

        public float X
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

        public float xScale
        {
            get
            {
                return this._b5;
            }
            set
            {
                this._b5 = value;
            }
        }

        public float xSkewAngle
        {
            get
            {
                return this._b7;
            }
            set
            {
                this._b7 = value;
            }
        }

        public float xTranslate
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

        public float Y
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

        public float yScale
        {
            get
            {
                return this._b6;
            }
            set
            {
                this._b6 = value;
            }
        }

        public float ySkewAngle
        {
            get
            {
                return this._b8;
            }
            set
            {
                this._b8 = value;
            }
        }

        public float yTranslate
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

