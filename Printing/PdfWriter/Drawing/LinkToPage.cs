namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.Element;
    using MControl.Printing.Pdf.Core.IO;
    using System;
    using System.Drawing;

    public class LinkToPage : GraphicsElement
    {
        private MControl.Printing.Pdf.Destination _b0;
        private float _b1;
        private float _b2;
        private float _b3;
        private float _b4;
        private A1 _b5;

        public LinkToPage(Page page, float x, float y)
        {
            this.b6(new MControl.Printing.Pdf.Destination(page), x, y, 0f, 0f);
        }

        public LinkToPage(MControl.Printing.Pdf.Destination destination, float x, float y, float width, float height)
        {
            this.b6(destination, x, y, width, height);
        }

        public LinkToPage(Page page, float x, float y, float width, float height)
        {
            this.b6(new MControl.Printing.Pdf.Destination(page), x, y, width, height);
        }

        public LinkToPage(MControl.Printing.Pdf.Destination destination, float x, float y, float width, float height, string name)
        {
            base._A154 = name;
            this.b6(destination, x, y, width, height);
        }

        public LinkToPage(Page page, float x, float y, float width, float height, string name)
        {
            base._A154 = name;
            this.b6(new MControl.Printing.Pdf.Destination(page), x, y, width, height);
        }

        internal override void A119(ref A120 b7, ref A112 b8)
        {
            if (this._b5 != null)
            {
                this._b5 = b7.A121(this._b5);
            }
            else
            {
                float width = this._b3;
                float height = this._b4;
                if (width <= 0f)
                {
                    width = b7.A211;
                }
                if (height <= 0f)
                {
                    height = b7.A212;
                }
                A96 A = new A96(this._b0.Page, this._b0.Top, this._b0.Left, this._b0.Zoom);
                this._b5 = b7.A121(new A124(b7.A97, new RectangleF(this._b1, b7.A98(this._b2), width, height), PdfColor.Black, 0, A, HighlightMode.None));
            }
        }

        private void b6(MControl.Printing.Pdf.Destination b0, float b1, float b2, float b3, float b4)
        {
            this._b0 = b0;
            this._b1 = b1;
            this._b2 = b2;
            this._b3 = b3;
            this._b4 = b4;
        }

        public MControl.Printing.Pdf.Destination Destination
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

        public float Height
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

        public float Width
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

        public float X
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

        public float Y
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
    }
}

