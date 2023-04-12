namespace MControl.Printing.Pdf
{
    using MControl.Printing.Pdf.Core.Element;
    using MControl.Printing.Pdf.Core.Fonts;
    using MControl.Printing.Pdf.Core.IO;
    using System;
    using System.Drawing;

    public class PdfFont : A91
    {
        private A252 _b0;

        public PdfFont(CJKFontFace cjkfont)
        {
            if (cjkfont == CJKFontFace.HanyangSystemsGothicMedium)
            {
                this._b0 = new A278();
            }
            else if (cjkfont == CJKFontFace.HanyangSystemsShinMyeongJoMedium)
            {
                this._b0 = new A279();
            }
            else if (cjkfont == CJKFontFace.HeiseiKakuGothicW5)
            {
                this._b0 = new A276();
            }
            else if (cjkfont == CJKFontFace.HeiseiMinchoW3)
            {
                this._b0 = new A277();
            }
            else if (cjkfont == CJKFontFace.MonotypeHeiMedium)
            {
                this._b0 = new A273();
            }
            else if (cjkfont == CJKFontFace.MonotypeSungLight)
            {
                this._b0 = new A274();
            }
            else if (cjkfont == CJKFontFace.SinoTypeSongLight)
            {
                this._b0 = new A275();
            }
        }

        public PdfFont(StandardFonts font, FontStyle style)
        {
            if (font == StandardFonts.Courier)
            {
                if ((style & FontStyle.Bold) == FontStyle.Bold)
                {
                    if ((style & FontStyle.Italic) == FontStyle.Italic)
                    {
                        this._b0 = new A250(style);
                    }
                    else
                    {
                        this._b0 = new A249(style);
                    }
                }
                else if ((style & FontStyle.Italic) == FontStyle.Italic)
                {
                    this._b0 = new A251(style);
                }
                else
                {
                    this._b0 = new A245(style);
                }
            }
            else if (font == StandardFonts.Helvetica)
            {
                if ((style & FontStyle.Bold) == FontStyle.Bold)
                {
                    if ((style & FontStyle.Italic) == FontStyle.Italic)
                    {
                        this._b0 = new A264(style);
                    }
                    else
                    {
                        this._b0 = new A263(style);
                    }
                }
                else if ((style & FontStyle.Italic) == FontStyle.Italic)
                {
                    this._b0 = new A265(style);
                }
                else
                {
                    this._b0 = new A260(style);
                }
            }
            else if (font == StandardFonts.TimesRoman)
            {
                if ((style & FontStyle.Bold) == FontStyle.Bold)
                {
                    if ((style & FontStyle.Italic) == FontStyle.Italic)
                    {
                        this._b0 = new A267(style);
                    }
                    else
                    {
                        this._b0 = new A266(style);
                    }
                }
                else if ((style & FontStyle.Italic) == FontStyle.Italic)
                {
                    this._b0 = new A268(style);
                }
                else
                {
                    this._b0 = new A269(style);
                }
            }
            else if (font == StandardFonts.Symbol)
            {
                this._b0 = new A270(style);
            }
            else if (font == StandardFonts.ZapfDingbats)
            {
                this._b0 = new A272(style);
            }
        }

        public PdfFont(Font font, bool embeded)
        {
            this._b0 = new A401(font, embeded);
        }

        public PdfFont(string fontname, string fontfile, FontStyle style, bool embeded)
        {
            this._b0 = new A401(fontname, fontfile, style, embeded);
        }

        internal override void A110()
        {
            this._b0.A110();
        }

        internal float A158(float b3)
        {
            return ((this._b0.A408 * b3) / 1000f);
        }

        internal void A159(string b6)
        {
            this._b0.A159(b6);
        }

        internal string A163(string b6)
        {
            return this._b0.A163(b6);
        }

        internal static bool A198(PdfFont b8, PdfFont b9)
        {
            if ((b8 == null) || (b9 == null))
            {
                return false;
            }
            return ((b8 == b9) || ((((b8.A403 != null) && (b9.A403 != null)) && (b8.A403 == b9.A403)) || ((string.Compare(b8.A357, b9.A357) == 0) && (b8.A405 == b9.A405))));
        }

        internal static bool A198(PdfFont b8, Font b9)
        {
            if ((b8 == null) || (b9 == null))
            {
                return false;
            }
            return ((((b8.A403 != null) && (b9 != null)) && (b8.A403 == b9)) || ((string.Compare(b8.A357, b9.Name) == 0) && (b8.A405 == b9.Style)));
        }

        internal void A237(Document b1, int b2)
        {
            this._b0.A237(b1, b2);
        }

        internal float A256(ushort b4)
        {
            return this._b0.A256(b4);
        }

        internal void A406(char b5)
        {
            this._b0.A406(b5);
        }

        internal override void A54(ref A55 b7)
        {
            this._b0.A54(ref b7);
        }

        internal float A583(float b3)
        {
            return ((this._b0.A409 * b3) / 1000f);
        }

        internal float A584(float b3)
        {
            return ((this._b0.A410 * b3) / 1000f);
        }

        public float GetCharWidth(char ch, float fontsize)
        {
            return this._b0.A257(ch, fontsize);
        }

        public float GetTextWidth(string text, float fontsize)
        {
            return this._b0.A258(text, fontsize);
        }

        public float GlyphWidth(char ch)
        {
            return this._b0.A256(ch);
        }

        public float Height(float fontsize)
        {
            return ((((float) (this._b0.A408 + this._b0.A409)) / 1000f) * fontsize);
        }

        internal override string A168
        {
            get
            {
                return this._b0.A168;
            }
        }

        internal string A357
        {
            get
            {
                return this._b0.A357;
            }
        }

        internal Font A403
        {
            get
            {
                return this._b0.A403;
            }
        }

        internal FontStyle A405
        {
            get
            {
                return this._b0.A405;
            }
        }

        internal override int A95
        {
            get
            {
                return this._b0.A95;
            }
        }

        public int Ascent
        {
            get
            {
                return this._b0.A408;
            }
        }

        public int Descent
        {
            get
            {
                return this._b0.A409;
            }
        }

        public bool IsEmbeddableFont
        {
            get
            {
                return this._b0.A342;
            }
        }
    }
}

