namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.Element;
    using MControl.Printing.Pdf.Core.IO;
    using System;
    using System.Drawing;
    using System.IO;

    public class LinkToSound : GraphicsElement
    {
        private float _b0;
        private float _b1;
        private float _b2;
        private float _b3;
        private A103 _b4;
        private A1 _b5;

        public LinkToSound(float x, float y, float width, float height, int bits, int rate, SoundChannels channels, SoundEncoding encoding, SoundIconType iconType, int volume, System.IO.Stream stream, string filepath)
        {
            this.b6(x, y, width, height, bits, rate, channels, encoding, iconType, volume, stream, filepath);
        }

        internal override void A119(ref A120 b15, ref A112 b16)
        {
            if (this._b5 != null)
            {
                this._b5 = b15.A121(this._b5);
            }
            else
            {
                float width = this._b2;
                float height = this._b3;
                if (width <= 0f)
                {
                    width = b15.A211;
                }
                if (height <= 0f)
                {
                    height = b15.A212;
                }
                this._b4._A92 = b15.A97;
                this._b5 = b15.A121(new A124(b15.A97, new RectangleF(this._b0, b15.A98(this._b1), width, height), PdfColor.Black, 0, this._b4, HighlightMode.None));
            }
        }

        private void b6(float b0, float b1, float b2, float b3, int b7, int b8, SoundChannels b9, SoundEncoding b10, SoundIconType b11, int b12, System.IO.Stream b13, string b14)
        {
            this._b0 = b0;
            this._b1 = b1;
            this._b2 = b2;
            this._b3 = b3;
            this._b4 = new A103(null);
            this._b4.A104 = b7;
            this._b4.A105 = b8;
            this._b4.A106 = b9;
            this._b4.A17 = b10;
            this._b4.A107 = b11;
            this._b4.A111 = b12;
            this._b4.A108 = b13;
            this._b4.A109 = b14;
        }

        public int Bits
        {
            get
            {
                return this._b4.A104;
            }
            set
            {
                this._b4.A104 = value;
            }
        }

        public SoundChannels Channels
        {
            get
            {
                return this._b4.A106;
            }
            set
            {
                this._b4.A106 = value;
            }
        }

        public SoundEncoding Encoding
        {
            get
            {
                return this._b4.A17;
            }
            set
            {
                this._b4.A17 = value;
            }
        }

        public string Filepath
        {
            get
            {
                return this._b4.A109;
            }
            set
            {
                this._b4.A109 = value;
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

        public SoundIconType IconType
        {
            get
            {
                return this._b4.A107;
            }
            set
            {
                this._b4.A107 = value;
            }
        }

        public int Rate
        {
            get
            {
                return this._b4.A105;
            }
            set
            {
                this._b4.A105 = value;
            }
        }

        public System.IO.Stream Stream
        {
            get
            {
                return this._b4.A108;
            }
            set
            {
                this._b4.A108 = value;
            }
        }

        public int Volume
        {
            get
            {
                return this._b4.A111;
            }
            set
            {
                this._b4.A111 = value;
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

