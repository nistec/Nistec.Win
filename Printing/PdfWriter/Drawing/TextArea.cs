namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.IO;
    using MControl.Printing.Pdf.Core.Text;
    using System;
    using System.Reflection;

    public class TextArea : GraphicsElement
    {
        private float _b0;
        private float _b1;
        private float _b10;
        private float _b11;
        private float _b12;
        private bool _b13;
        private bool _b14;
        private float _b15;
        private TextAlignment _b16;
        private bool _b17;
        private bool _b18;
        private float _b19;
        private float _b2;
        private A478 _b20;
        private int _b21;
        private int _b22;
        private float _b3;
        private A497 _b4;
        private float _b5;
        private float _b6;
        private float _b7;
        private float _b8;
        private float _b9;

        internal TextArea()
        {
        }

        public TextArea(float width, float height)
        {
            this._b2 = width;
            this._b3 = height;
            this.b23();
        }

        public TextArea(float width, float height, string name)
        {
            base._A154 = name;
            this._b2 = width;
            this._b3 = height;
            this.b23();
        }

        internal override void A119(ref A120 b25, ref A112 b26)
        {
            GraphicsElement.A435(ref b25, ref b26);
            GraphicsElement.A446(b26, this._b0, b25.A98(this._b1), this._b2, this._b3);
            A500.A119(ref b25, ref b26, this._b20, this._b21, this._b22, this._b0, this._b1, this._b10, this._b8, this._b13);
            GraphicsElement.A438(ref b25, ref b26);
        }

        internal TextArea A468(float b0, float b1, float b27)
        {
            this._b0 = b0;
            this._b1 = b1;
            this._b3 = b27;
            if (!this._b18)
            {
                A500 A = new A500(this._b4, this._b5, this._b6, this._b12, this._b15, this._b16, this._b13);
                A.A501((((this._b2 - this._b8) - this._b9) - this._b6) - this._b7, new TextStyle(), false);
                this._b19 = ((A.A502 - this._b12) + this._b10) + this._b11;
                this._b20 = A.A479;
                this._b17 = true;
                this._b18 = true;
            }
            float num = this._b10;
            b27 -= this._b11;
            A526 A2 = null;
            if ((this._b20 != null) && (this._b20.A2 > 0))
            {
                for (int i = this._b21; i < this._b20.A2; i++)
                {
                    A2 = this._b20[i];
                    if (((A2.A527 + A2.A212) + num) > b27)
                    {
                        if (this._b14)
                        {
                            this._b22 = i;
                        }
                        else
                        {
                            this._b22 = i + 1;
                        }
                        return this.b28(i);
                    }
                    num += (A2.A527 + A2.A212) + A2.A528;
                }
            }
            this._b22 = this._b20.A2;
            return null;
        }

        public void AddContent(string text, TextStyle style)
        {
            if (!this._b18)
            {
                this._b4.A3(new A498(text, style));
                this._b17 = false;
            }
        }

        public void AddContent(string text, TextStyle style, string hyperlink)
        {
            if (!this._b18)
            {
                this._b4.A3(new A499(text, style, hyperlink));
                this._b17 = false;
            }
        }

        public float MeasureHeight()
        {
            if (!this._b17)
            {
                A500 A = new A500(this._b4, this._b5, this._b6, this._b12, this._b15, this._b16, this._b13);
                A.A501((((this._b2 - this._b8) - this._b9) - this._b6) - this._b7, new TextStyle(), true);
                this._b19 = ((A.A502 - this._b12) + this._b10) + this._b11;
                this._b17 = true;
            }
            return this._b19;
        }

        public void NewLine()
        {
            this.NewLine(0f, this._b16);
        }

        public void NewLine(TextAlignment alignment)
        {
            this.NewLine(0f, alignment);
        }

        public void NewLine(float spacing)
        {
            this.NewLine(spacing, this._b16);
        }

        public void NewLine(float spacing, TextAlignment alignment)
        {
            if (!this._b18)
            {
                this._b4.A3(new A524(alignment, spacing));
                this._b17 = false;
            }
        }

        public void NewPara()
        {
            this.NewPara(this._b15, this._b16);
        }

        public void NewPara(TextAlignment alignment)
        {
            this.NewPara(this._b15, alignment);
        }

        public void NewPara(float spacing)
        {
            this.NewPara(spacing, this._b16);
        }

        public void NewPara(float spacing, TextAlignment alignment)
        {
            if (!this._b18)
            {
                this._b4.A3(new A525(alignment, spacing));
                this._b17 = false;
            }
        }

        private void b23()
        {
            this._b4 = new A497();
            this._b5 = 0f;
            this._b6 = 0f;
            this._b7 = 0f;
            this._b8 = 0f;
            this._b9 = 0f;
            this._b10 = 0f;
            this._b11 = 0f;
            this._b12 = 0f;
            this._b15 = 0f;
            this._b16 = TextAlignment.Left;
            this._b17 = false;
            this._b18 = false;
            this._b19 = 0f;
            this._b21 = 0;
            this._b22 = 0;
            this._b13 = false;
            this._b14 = false;
            this._b20 = null;
        }

        private TextArea b28(int b21)
        {
            TextArea area = new TextArea();
            area._b16 = this._b16;
            area._b11 = this._b11;
            area._b18 = this._b18;
            area._b5 = this._b5;
            area._b3 = this._b3;
            area._b4 = this._b4;
            area._b14 = this._b14;
            area._b6 = this._b6;
            area._b8 = this._b8;
            area._b22 = 0;
            area._b21 = b21;
            area._b20 = this._b20;
            area._b12 = this._b12;
            area._b17 = this._b17;
            area._b19 = A500.A503(this._b20, b21, this._b20.A2, this._b10, this._b11);
            area._b15 = this._b15;
            area._b7 = this._b7;
            area._b9 = this._b9;
            area._b13 = this._b13;
            area._b10 = this._b10;
            area._b2 = this._b2;
            return area;
        }

        internal int A2
        {
            get
            {
                return this._b4.A2;
            }
        }

        public TextAlignment Alignment
        {
            get
            {
                return this._b16;
            }
            set
            {
                if (!this._b18 && (this._b16 != value))
                {
                    this._b16 = value;
                    this._b17 = false;
                }
            }
        }

        public float BottomPad
        {
            get
            {
                return this._b11;
            }
            set
            {
                if ((!this._b18 && (this._b11 != value)) && (value >= 0f))
                {
                    this._b11 = value;
                    this._b17 = false;
                }
            }
        }

        public float FirstIndent
        {
            get
            {
                return this._b5;
            }
            set
            {
                if ((!this._b18 && (this._b5 != value)) && (value >= 0f))
                {
                    this._b5 = value;
                    this._b17 = false;
                }
            }
        }

        public float Height
        {
            get
            {
                return this._b3;
            }
        }

        internal A523 this[int b24]
        {
            get
            {
                return this._b4[b24];
            }
        }

        public bool KeepTogether
        {
            get
            {
                return this._b14;
            }
            set
            {
                this._b14 = value;
            }
        }

        public float LeftIndent
        {
            get
            {
                return this._b6;
            }
            set
            {
                if ((!this._b18 && (this._b6 != value)) && (value >= 0f))
                {
                    this._b6 = value;
                    this._b17 = false;
                }
            }
        }

        public float LeftPad
        {
            get
            {
                return this._b8;
            }
            set
            {
                if ((!this._b18 && (this._b8 != value)) && (value >= 0f))
                {
                    this._b8 = value;
                    this._b17 = false;
                }
            }
        }

        public float LineSpace
        {
            get
            {
                return this._b12;
            }
            set
            {
                if ((!this._b18 && (this._b12 != value)) && (value >= 0f))
                {
                    this._b12 = value;
                    this._b17 = false;
                }
            }
        }

        public float ParaSpace
        {
            get
            {
                return this._b15;
            }
            set
            {
                if ((!this._b18 && (this._b15 != value)) && (value >= 0f))
                {
                    this._b15 = value;
                    this._b17 = false;
                }
            }
        }

        public float RightIndent
        {
            get
            {
                return this._b7;
            }
            set
            {
                if ((!this._b18 && (this._b7 != value)) && (value >= 0f))
                {
                    this._b7 = value;
                    this._b17 = false;
                }
            }
        }

        public float RightPad
        {
            get
            {
                return this._b9;
            }
            set
            {
                if ((!this._b18 && (this._b9 != value)) && (value >= 0f))
                {
                    this._b9 = value;
                    this._b17 = false;
                }
            }
        }

        public bool RightToLeft
        {
            get
            {
                return this._b13;
            }
            set
            {
                this._b13 = value;
            }
        }

        public float TopPad
        {
            get
            {
                return this._b10;
            }
            set
            {
                if ((!this._b18 && (this._b10 != value)) && (value >= 0f))
                {
                    this._b10 = value;
                    this._b17 = false;
                }
            }
        }

        public float Width
        {
            get
            {
                return this._b2;
            }
        }
    }
}

