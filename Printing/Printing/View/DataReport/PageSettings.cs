namespace Nistec.Printing.View
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Printing;

    public class PageSettings
    {
        private PrinterCollate _var0 = PrinterCollate.Default;
        private bool _var1 = true;
        private PaperSourceKind _var10 = PaperSourceKind.FormSource;
        private bool _var2 = true;
        private PrinterDuplex _var3 = PrinterDuplex.Default;
        private float _var4 = 0f;
        private Nistec.Printing.View.Margins _var5 = new Nistec.Printing.View.Margins();
        private PageOrientation _var6 = PageOrientation.Default;
        private float _var7;
        private float _var8;
        private System.Drawing.Printing.PaperKind _var9 = System.Drawing.Printing.PaperKind.A4;
        private static Nistec.Printing.View.PaperInfos var11;

        public bool ShouldSerializeCollate()
        {
            return (this.Collate != PrinterCollate.Default);
        }

        public bool ShouldSerializeDuplex()
        {
            return (this.Duplex != PrinterDuplex.Default);
        }

        public bool ShouldSerializeGutter()
        {
            return (this.Gutter != 0f);
        }

        public bool ShouldSerializeOrientation()
        {
            return (this.Orientation != PageOrientation.Default);
        }

        public bool ShouldSerializePaperHeight()
        {
            return (this.PaperKind == System.Drawing.Printing.PaperKind.Custom);
        }

        public bool ShouldSerializePaperSource()
        {
            return !this._var2;
        }

        public bool ShouldSerializePaperWidth()
        {
            return (this.PaperKind == System.Drawing.Printing.PaperKind.Custom);
        }

        private static void var12()
        {
            if (var11 == null)
            {
                var11 = new Nistec.Printing.View.PaperInfos();
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.A2, new Size(420, 0x252), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.A3, new Size(0x129, 420), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.A3Extra, new Size(0x142, 0x1bd), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.A3ExtraTransverse, new Size(0x142, 0x1bd), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.A3Rotated, new Size(420, 0x129), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.A3Transverse, new Size(0x129, 420), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.A4, new Size(210, 0x129), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.A4Extra, new Size(0xec, 0x142), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.A4Plus, new Size(210, 330), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.A4Rotated, new Size(0x129, 210), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.A4Small, new Size(210, 0x129), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.A4Transverse, new Size(210, 0x129), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.A5, new Size(0x94, 210), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.A5Extra, new Size(0xae, 0xeb), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.A5Rotated, new Size(210, 0x94), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.A5Transverse, new Size(0x94, 210), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.A6, new Size(0x69, 0x94), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.A6Rotated, new Size(0x94, 0x69), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.APlus, new Size(0xe3, 0x164), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.B4, new Size(250, 0x161), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.B4Envelope, new Size(250, 0x161), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.B4JisRotated, new Size(0x16c, 0x101), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.B5, new Size(0xb0, 250), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.B5Envelope, new Size(0xb0, 250), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.B5Extra, new Size(0xc9, 0x114), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.B5JisRotated, new Size(0x101, 0xb6), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.B5Transverse, new Size(0xb6, 0x101), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.B6Envelope, new Size(0xb0, 0x7d), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.B6Jis, new Size(0x80, 0xb6), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.B6JisRotated, new Size(0xb6, 0x80), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.BPlus, new Size(0x131, 0x1e7), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.C3Envelope, new Size(0x144, 0x1ca), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.C4Envelope, new Size(0xe5, 0x144), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.C5Envelope, new Size(0xa2, 0xe5), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.C65Envelope, new Size(0x72, 0xe5), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.C6Envelope, new Size(0x72, 0xa2), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.CSheet, new Size(0x4268, 0x55f0), false));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.DLEnvelope, new Size(110, 220), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.DSheet, new Size(0x55f0, 0x84d0), false));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.ESheet, new Size(0x84d0, 0xabe0), false));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.Executive, new Size(0x1c52, 0x2904), false));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.Folio, new Size(0x2134, 0x32c8), false));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.GermanLegalFanfold, new Size(0x2134, 0x32c8), false));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.GermanStandardFanfold, new Size(0x2134, 0x2ee0), false));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.InviteEnvelope, new Size(220, 220), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.IsoB4, new Size(250, 0x161), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.ItalyEnvelope, new Size(110, 230), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.JapaneseDoublePostcard, new Size(200, 0x94), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.JapaneseDoublePostcardRotated, new Size(0x94, 200), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.JapanesePostcard, new Size(100, 0x94), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.JapanesePostcardRotated, new Size(0x94, 100), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.Ledger, new Size(0x4268, 0x2af8), false));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.Legal, new Size(0x2134, 0x36b0), false));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.LegalExtra, new Size(0x243b, 0x3a98), false));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.Letter, new Size(0x2134, 0x2af8), false));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.LetterExtra, new Size(0x243b, 0x2ee0), false));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.LetterExtraTransverse, new Size(0x243b, 0x2ee0), false));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.LetterPlus, new Size(0x2134, 0x3192), false));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.LetterRotated, new Size(0x2af8, 0x2134), false));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.LetterSmall, new Size(0x2134, 0x2af8), false));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.LetterTransverse, new Size(0x2053, 0x2af8), false));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.MonarchEnvelope, new Size(0xf23, 0x1d4c), false));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.Note, new Size(0x2134, 0x2af8), false));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.Number10Envelope, new Size(0x101d, 0x251c), false));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.Number11Envelope, new Size(0x1194, 0x2887), false));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.Number12Envelope, new Size(0x128e, 0x2af8), false));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.Number14Envelope, new Size(0x1388, 0x2cec), false));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.Number9Envelope, new Size(0xf23, 0x22ab), false));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.PersonalEnvelope, new Size(0xe29, 0x1964), false));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.Prc16K, new Size(0x92, 0xd7), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.Prc16KRotated, new Size(0x92, 0xd7), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.Prc32K, new Size(0x61, 0x97), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.Prc32KBig, new Size(0x61, 0x97), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.Prc32KBigRotated, new Size(0x97, 0x61), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.Prc32KRotated, new Size(0x97, 0x61), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.PrcEnvelopeNumber1, new Size(0x66, 0xa5), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.PrcEnvelopeNumber10, new Size(0x144, 0x1ca), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.PrcEnvelopeNumber10Rotated, new Size(0x1ca, 0x144), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.PrcEnvelopeNumber1Rotated, new Size(0xa5, 0x66), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.PrcEnvelopeNumber2, new Size(0x66, 0xb0), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.PrcEnvelopeNumber2Rotated, new Size(0xb0, 0x66), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.PrcEnvelopeNumber3, new Size(0x7d, 0xb0), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.PrcEnvelopeNumber3Rotated, new Size(0xb0, 0x7d), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.PrcEnvelopeNumber4, new Size(110, 0xd0), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.PrcEnvelopeNumber4Rotated, new Size(0xd0, 110), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.PrcEnvelopeNumber5, new Size(110, 220), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.PrcEnvelopeNumber5Rotated, new Size(220, 110), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.PrcEnvelopeNumber6, new Size(120, 230), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.PrcEnvelopeNumber6Rotated, new Size(230, 120), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.PrcEnvelopeNumber7, new Size(160, 230), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.PrcEnvelopeNumber7Rotated, new Size(230, 160), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.PrcEnvelopeNumber8, new Size(120, 0x135), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.PrcEnvelopeNumber8Rotated, new Size(0x135, 120), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.PrcEnvelopeNumber9, new Size(0xe5, 0x144), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.PrcEnvelopeNumber9Rotated, new Size(0x144, 0xe5), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.Quarto, new Size(0xd7, 0x113), true));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.Standard10x11, new Size(0x2710, 0x2af8), false));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.Standard10x14, new Size(0x2710, 0x36b0), false));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.Standard11x17, new Size(0x2af8, 0x4268), false));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.Standard12x11, new Size(0x2ee0, 0x2af8), false));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.Standard15x11, new Size(0x3a98, 0x2af8), false));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.Standard9x11, new Size(0x2328, 0x2af8), false));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.Statement, new Size(0x157c, 0x2134), false));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.Tabloid, new Size(0x2af8, 0x4268), false));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.TabloidExtra, new Size(0x2daa, 0x4650), false));
                var11.mtd2(new PaperInfo(System.Drawing.Printing.PaperKind.USStandardFanfold, new Size(0x3a1b, 0x2af8), false));
            }
        }

        public PrinterCollate Collate
        {
            get
            {
                return this._var0;
            }
            set
            {
                this._var0 = value;
            }
        }

        [DefaultValue(true)]
        public bool DefaultPaperSize
        {
            get
            {
                return this._var1;
            }
            set
            {
                this._var1 = value;
            }
        }

        [DefaultValue(true)]
        public bool DefaultPaperSource
        {
            get
            {
                return this._var2;
            }
            set
            {
                this._var2 = value;
            }
        }

        public PrinterDuplex Duplex
        {
            get
            {
                return this._var3;
            }
            set
            {
                this._var3 = value;
            }
        }

        public float Gutter
        {
            get
            {
                return this._var4;
            }
            set
            {
                this._var4 = value;
            }
        }

        public Nistec.Printing.View.Margins Margins
        {
            get
            {
                return this._var5;
            }
            set
            {
                this._var5 = value;
            }
        }

        public PageOrientation Orientation
        {
            get
            {
                return this._var6;
            }
            set
            {
                this._var6 = value;
            }
        }

        public float PaperHeight
        {
            get
            {
                return this._var7;
            }
            set
            {
                this._var7 = value;
            }
        }

        public static Nistec.Printing.View.PaperInfos PaperInfos
        {
            get
            {
                var12();
                return var11;
            }
        }

        public System.Drawing.Printing.PaperKind PaperKind
        {
            get
            {
                return this._var9;
            }
            set
            {
                this._var9 = value;
            }
        }

        public PaperSourceKind PaperSource
        {
            get
            {
                return this._var10;
            }
            set
            {
                this._var10 = value;
            }
        }

        public float PaperWidth
        {
            get
            {
                return this._var8;
            }
            set
            {
                this._var8 = value;
            }
        }
    }
}

