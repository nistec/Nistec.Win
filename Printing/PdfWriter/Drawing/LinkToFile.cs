namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.Element;
    using MControl.Printing.Pdf.Core.IO;
    using System;
    using System.Drawing;

    public class LinkToFile : GraphicsElement
    {
        private float _b0;
        private float _b1;
        private float _b2;
        private float _b3;
        private string _b4;
        private A1 _b5;

        public LinkToFile(float x, float y, float width, float height, string filepath, string name)
        {
            this._b0 = x;
            this._b1 = y;
            this._b2 = width;
            this._b3 = height;
            this._b4 = filepath;
            base._A154 = name;
        }

        internal override void A119(ref A120 b6, ref A112 b7)
        {
            if (this._b5 != null)
            {
                this._b5 = b6.A121(this._b5);
            }
            else
            {
                float width = this._b2;
                float height = this._b3;
                if (width <= 0f)
                {
                    width = b6.A211;
                }
                if (height <= 0f)
                {
                    height = b6.A212;
                }
                A101 A = new A101(b6.A97, this._b4);
                this._b5 = b6.A121(new A124(b6.A97, new RectangleF(this._b0, b6.A98(this._b1), width, height), PdfColor.Black, 0, A, HighlightMode.None));
            }
        }

        public string Filepath
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

        public float Height
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

        public float Width
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
    }
}

