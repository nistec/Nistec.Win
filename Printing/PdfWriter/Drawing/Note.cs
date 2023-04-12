namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.Element;
    using MControl.Printing.Pdf.Core.IO;
    using System;
    using System.Drawing;

    public class Note : GraphicsElement
    {
        private string _b0;
        private string _b1;
        private float _b2;
        private float _b3;
        private float _b4;
        private float _b5;
        private bool _b6;
        private RGBColor _b7;
        private AnnotIcon _b8;
        private A1 _b9;

        public Note(string title, string content, float x, float y)
        {
            this.b10(title, content, x, y, 0f, 0f, PdfColor.Black, false, AnnotIcon.Note);
        }

        public Note(string title, string content, float x, float y, float width, float height)
        {
            this.b10(title, content, x, y, width, height, PdfColor.Black, false, AnnotIcon.Note);
        }

        public Note(string title, string content, float x, float y, float width, float height, RGBColor color, bool isopen, AnnotIcon icon)
        {
            this.b10(title, content, x, y, width, height, color, isopen, icon);
        }

        public Note(string title, string content, float x, float y, float width, float height, RGBColor color, bool isopen, AnnotIcon icon, string name)
        {
            base._A154 = name;
            this.b10(title, content, x, y, width, height, color, isopen, icon);
        }

        internal override void A119(ref A120 b11, ref A112 b12)
        {
            if (this._b9 != null)
            {
                this._b9 = b11.A121(this._b9);
            }
            else
            {
                float width = this._b4;
                float height = this._b5;
                if (width <= 0f)
                {
                    width = b11.A211;
                }
                if (height <= 0f)
                {
                    height = b11.A212;
                }
                this._b9 = b11.A121(new A125(b11.A97, this._b0, this._b1, new RectangleF(this._b2, b11.A98(this._b3), width, height), this._b7, this._b6, this._b8));
            }
        }

        private void b10(string b0, string b1, float b2, float b3, float b4, float b5, RGBColor b7, bool b6, AnnotIcon b8)
        {
            this._b0 = b0;
            this._b1 = b1;
            this._b2 = b2;
            this._b3 = b3;
            this._b4 = b4;
            this._b5 = b5;
            this._b7 = b7;
            this._b6 = b6;
            this._b8 = b8;
        }

        public RGBColor Color
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

        public string Content
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

        public float Height
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

        public AnnotIcon Icon
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

        public bool IsOpen
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

        public string Title
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

        public float Width
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
                return this._b3;
            }
            set
            {
                this._b3 = value;
            }
        }
    }
}

