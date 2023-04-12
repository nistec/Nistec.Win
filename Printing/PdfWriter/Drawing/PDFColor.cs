namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf.Core.IO;
    using System;

    public abstract class PdfColor
    {
        private static RGBColor _b0;
        private static RGBColor _b1;
        private static RGBColor _b10;
        private static RGBColor _b100;
        private static RGBColor _b101;
        private static RGBColor _b102;
        private static RGBColor _b103;
        private static RGBColor _b104;
        private static RGBColor _b105;
        private static RGBColor _b106;
        private static RGBColor _b107;
        private static RGBColor _b108;
        private static RGBColor _b109;
        private static RGBColor _b11;
        private static RGBColor _b110;
        private static RGBColor _b111;
        private static RGBColor _b112;
        private static RGBColor _b113;
        private static RGBColor _b114;
        private static RGBColor _b115;
        private static RGBColor _b116;
        private static RGBColor _b117;
        private static RGBColor _b118;
        private static RGBColor _b119;
        private static RGBColor _b12;
        private static RGBColor _b120;
        private static RGBColor _b121;
        private static RGBColor _b122;
        private static RGBColor _b123;
        private static RGBColor _b124;
        private static RGBColor _b125;
        private static RGBColor _b126;
        private static RGBColor _b127;
        private static RGBColor _b128;
        private static RGBColor _b129;
        private static RGBColor _b13;
        private static RGBColor _b130;
        private static RGBColor _b131;
        private static RGBColor _b132;
        private static RGBColor _b133;
        private static RGBColor _b134;
        private static RGBColor _b135;
        private static RGBColor _b136;
        private static RGBColor _b137;
        private static RGBColor _b138;
        private static RGBColor _b139;
        private static RGBColor _b14;
        private static RGBColor _b140;
        private static RGBColor _b15;
        private static RGBColor _b16;
        private static RGBColor _b17;
        private static RGBColor _b18;
        private static RGBColor _b19;
        private static RGBColor _b2;
        private static RGBColor _b20;
        private static RGBColor _b21;
        private static RGBColor _b22;
        private static RGBColor _b23;
        private static RGBColor _b24;
        private static RGBColor _b25;
        private static RGBColor _b26;
        private static RGBColor _b27;
        private static RGBColor _b28;
        private static RGBColor _b29;
        private static RGBColor _b3;
        private static RGBColor _b30;
        private static RGBColor _b31;
        private static RGBColor _b32;
        private static RGBColor _b33;
        private static RGBColor _b34;
        private static RGBColor _b35;
        private static RGBColor _b36;
        private static RGBColor _b37;
        private static RGBColor _b38;
        private static RGBColor _b39;
        private static RGBColor _b4;
        private static RGBColor _b40;
        private static RGBColor _b41;
        private static RGBColor _b42;
        private static RGBColor _b43;
        private static RGBColor _b44;
        private static RGBColor _b45;
        private static RGBColor _b46;
        private static RGBColor _b47;
        private static RGBColor _b48;
        private static RGBColor _b49;
        private static RGBColor _b5;
        private static RGBColor _b50;
        private static RGBColor _b51;
        private static RGBColor _b52;
        private static RGBColor _b53;
        private static RGBColor _b54;
        private static RGBColor _b55;
        private static RGBColor _b56;
        private static RGBColor _b57;
        private static RGBColor _b58;
        private static RGBColor _b59;
        private static RGBColor _b6;
        private static RGBColor _b60;
        private static RGBColor _b61;
        private static RGBColor _b62;
        private static RGBColor _b63;
        private static RGBColor _b64;
        private static RGBColor _b65;
        private static RGBColor _b66;
        private static RGBColor _b67;
        private static RGBColor _b68;
        private static RGBColor _b69;
        private static RGBColor _b7;
        private static RGBColor _b70;
        private static RGBColor _b71;
        private static RGBColor _b72;
        private static RGBColor _b73;
        private static RGBColor _b74;
        private static RGBColor _b75;
        private static RGBColor _b76;
        private static RGBColor _b77;
        private static RGBColor _b78;
        private static RGBColor _b79;
        private static RGBColor _b8;
        private static RGBColor _b80;
        private static RGBColor _b81;
        private static RGBColor _b82;
        private static RGBColor _b83;
        private static RGBColor _b84;
        private static RGBColor _b85;
        private static RGBColor _b86;
        private static RGBColor _b87;
        private static RGBColor _b88;
        private static RGBColor _b89;
        private static RGBColor _b9;
        private static RGBColor _b90;
        private static RGBColor _b91;
        private static RGBColor _b92;
        private static RGBColor _b93;
        private static RGBColor _b94;
        private static RGBColor _b95;
        private static RGBColor _b96;
        private static RGBColor _b97;
        private static RGBColor _b98;
        private static RGBColor _b99;

        internal PdfColor()
        {
        }

        internal virtual string A169(bool b142)
        {
            return string.Empty;
        }

        internal virtual void A468(ref A112 b141, bool b142)
        {
        }

        internal static byte A469(byte red, byte green, byte blue)
        {
            return (byte) ((((float) ((red + green) + blue)) / 765f) * 255f);
        }

        internal static byte[] A470(byte r, byte g, byte b)
        {
            float num = ((float) r) / 255f;
            float num2 = ((float) g) / 255f;
            float num3 = ((float) b) / 255f;
            float num4 = Math.Min(1f - num, Math.Min((float) (1f - num2), (float) (1f - num3)));
            float num5 = (num4 == 1f) ? 0f : (((1f - num) - num4) / (1f - num4));
            float num6 = (num4 == 1f) ? 0f : (((1f - num2) - num4) / (1f - num4));
            float num7 = (num4 == 1f) ? 0f : (((1f - num3) - num4) / (1f - num4));
            return new byte[] { ((byte) (num5 * 255f)), ((byte) (num6 * 255f)), ((byte) (num7 * 255f)), ((byte) (num4 * 255f)) };
        }

        internal static short[] A471(short red, short green, short blue)
        {
            short num = (short) (0xff - red);
            short num2 = (short) (0xff - green);
            short num3 = (short) (0xff - blue);
            short num4 = Math.Min(Math.Min(num, num2), num3);
            num = (short) ((((float) (num - num4)) / ((float) (0xff - num4))) * 255f);
            num2 = (short) ((((float) (num2 - num4)) / ((float) (0xff - num4))) * 255f);
            num3 = (short) ((((float) (num3 - num4)) / ((float) (0xff - num4))) * 255f);
            return new short[] { num, num2, num3, num4 };
        }

        internal abstract PdfColor DarkenColor(double percent);
        internal static float GetGray(byte red, byte green, byte blue)
        {
            return (((float) ((red + green) + blue)) / 765f);
        }

        internal abstract PdfColor LightenColor(double percent);

        public static RGBColor AliceBlue
        {
            get
            {
                if (_b0 == null)
                {
                    _b0 = new RGBColor(240, 0xf8, 0xff);
                }
                return _b0;
            }
        }

        public static RGBColor AntiqueWhite
        {
            get
            {
                if (_b1 == null)
                {
                    _b1 = new RGBColor(250, 0xeb, 0xd7);
                }
                return _b1;
            }
        }

        public static RGBColor Aqua
        {
            get
            {
                if (_b2 == null)
                {
                    _b2 = new RGBColor(0, 0xff, 0xff);
                }
                return _b2;
            }
        }

        public static RGBColor Aquamarine
        {
            get
            {
                if (_b3 == null)
                {
                    _b3 = new RGBColor(0x7f, 0xff, 0xd4);
                }
                return _b3;
            }
        }

        public static RGBColor Azure
        {
            get
            {
                if (_b4 == null)
                {
                    _b4 = new RGBColor(240, 0xff, 0xff);
                }
                return _b4;
            }
        }

        public static RGBColor Beige
        {
            get
            {
                if (_b5 == null)
                {
                    _b5 = new RGBColor(0xf5, 0xf5, 220);
                }
                return _b5;
            }
        }

        public static RGBColor Bisque
        {
            get
            {
                if (_b6 == null)
                {
                    _b6 = new RGBColor(0xff, 0xe4, 0xc4);
                }
                return _b6;
            }
        }

        public static RGBColor Black
        {
            get
            {
                if (_b7 == null)
                {
                    _b7 = new RGBColor(0, 0, 0);
                }
                return _b7;
            }
        }

        public static RGBColor BlanchedAlmond
        {
            get
            {
                if (_b8 == null)
                {
                    _b8 = new RGBColor(0xff, 0xeb, 0xcd);
                }
                return _b8;
            }
        }

        public static RGBColor Blue
        {
            get
            {
                if (_b9 == null)
                {
                    _b9 = new RGBColor(0, 0, 0xff);
                }
                return _b9;
            }
        }

        public static RGBColor BlueViolet
        {
            get
            {
                if (_b10 == null)
                {
                    _b10 = new RGBColor(0x8a, 0x2b, 0xe2);
                }
                return _b10;
            }
        }

        public static RGBColor Brown
        {
            get
            {
                if (_b11 == null)
                {
                    _b11 = new RGBColor(0xa5, 0x2a, 0x2a);
                }
                return _b11;
            }
        }

        public static RGBColor BurlyWood
        {
            get
            {
                if (_b12 == null)
                {
                    _b12 = new RGBColor(0xde, 0xb8, 0x87);
                }
                return _b12;
            }
        }

        public static RGBColor CadetBlue
        {
            get
            {
                if (_b13 == null)
                {
                    _b13 = new RGBColor(0x5f, 0x9e, 160);
                }
                return _b13;
            }
        }

        public static RGBColor Chartreuse
        {
            get
            {
                if (_b14 == null)
                {
                    _b14 = new RGBColor(0x7f, 0xff, 0);
                }
                return _b14;
            }
        }

        public static RGBColor Chocolate
        {
            get
            {
                if (_b15 == null)
                {
                    _b15 = new RGBColor(210, 0x69, 30);
                }
                return _b15;
            }
        }

        public static RGBColor Coral
        {
            get
            {
                if (_b16 == null)
                {
                    _b16 = new RGBColor(0xff, 0x7f, 80);
                }
                return _b16;
            }
        }

        public static RGBColor CornflowerBlue
        {
            get
            {
                if (_b17 == null)
                {
                    _b17 = new RGBColor(100, 0x95, 0xed);
                }
                return _b17;
            }
        }

        public static RGBColor Cornsilk
        {
            get
            {
                if (_b18 == null)
                {
                    _b18 = new RGBColor(0xff, 0xf8, 220);
                }
                return _b18;
            }
        }

        public static RGBColor Crimson
        {
            get
            {
                if (_b19 == null)
                {
                    _b19 = new RGBColor(220, 20, 60);
                }
                return _b19;
            }
        }

        public static RGBColor Cyan
        {
            get
            {
                if (_b20 == null)
                {
                    _b20 = new RGBColor(0, 0xff, 0xff);
                }
                return _b20;
            }
        }

        public static RGBColor DarkBlue
        {
            get
            {
                if (_b21 == null)
                {
                    _b21 = new RGBColor(0, 0, 0x8b);
                }
                return _b21;
            }
        }

        public static RGBColor DarkCyan
        {
            get
            {
                if (_b22 == null)
                {
                    _b22 = new RGBColor(0, 0x8b, 0x8b);
                }
                return _b22;
            }
        }

        public static RGBColor DarkGoldenrod
        {
            get
            {
                if (_b23 == null)
                {
                    _b23 = new RGBColor(0xb8, 0x86, 11);
                }
                return _b23;
            }
        }

        public static RGBColor DarkGray
        {
            get
            {
                if (_b24 == null)
                {
                    _b24 = new RGBColor(0xa9, 0xa9, 0xa9);
                }
                return _b24;
            }
        }

        public static RGBColor DarkGreen
        {
            get
            {
                if (_b25 == null)
                {
                    _b25 = new RGBColor(0, 100, 0);
                }
                return _b25;
            }
        }

        public static RGBColor DarkKhaki
        {
            get
            {
                if (_b26 == null)
                {
                    _b26 = new RGBColor(0xbd, 0xb7, 0x6b);
                }
                return _b26;
            }
        }

        public static RGBColor DarkMagenta
        {
            get
            {
                if (_b27 == null)
                {
                    _b27 = new RGBColor(0x8b, 0, 0x8b);
                }
                return _b27;
            }
        }

        public static RGBColor DarkOliveGreen
        {
            get
            {
                if (_b28 == null)
                {
                    _b28 = new RGBColor(0x55, 0x6b, 0x2f);
                }
                return _b28;
            }
        }

        public static RGBColor DarkOrange
        {
            get
            {
                if (_b29 == null)
                {
                    _b29 = new RGBColor(0xff, 140, 0);
                }
                return _b29;
            }
        }

        public static RGBColor DarkOrchid
        {
            get
            {
                if (_b30 == null)
                {
                    _b30 = new RGBColor(0x99, 50, 0xcc);
                }
                return _b30;
            }
        }

        public static RGBColor DarkRed
        {
            get
            {
                if (_b31 == null)
                {
                    _b31 = new RGBColor(0x8b, 0, 0);
                }
                return _b31;
            }
        }

        public static RGBColor DarkSalmon
        {
            get
            {
                if (_b32 == null)
                {
                    _b32 = new RGBColor(0xe9, 150, 0x7a);
                }
                return _b32;
            }
        }

        public static RGBColor DarkSeaGreen
        {
            get
            {
                if (_b33 == null)
                {
                    _b33 = new RGBColor(0x8f, 0xbc, 0x8b);
                }
                return _b33;
            }
        }

        public static RGBColor DarkSlateBlue
        {
            get
            {
                if (_b34 == null)
                {
                    _b34 = new RGBColor(0x48, 0x3d, 0x8b);
                }
                return _b34;
            }
        }

        public static RGBColor DarkSlateGray
        {
            get
            {
                if (_b35 == null)
                {
                    _b35 = new RGBColor(0x2f, 0x4f, 0x4f);
                }
                return _b35;
            }
        }

        public static RGBColor DarkTurquoise
        {
            get
            {
                if (_b36 == null)
                {
                    _b36 = new RGBColor(0, 0xce, 0xd1);
                }
                return _b36;
            }
        }

        public static RGBColor DarkViolet
        {
            get
            {
                if (_b37 == null)
                {
                    _b37 = new RGBColor(0x94, 0, 0xd3);
                }
                return _b37;
            }
        }

        public static RGBColor DeepPink
        {
            get
            {
                if (_b38 == null)
                {
                    _b38 = new RGBColor(0xff, 20, 0x93);
                }
                return _b38;
            }
        }

        public static RGBColor DeepSkyBlue
        {
            get
            {
                if (_b39 == null)
                {
                    _b39 = new RGBColor(0, 0xbf, 0xff);
                }
                return _b39;
            }
        }

        public static RGBColor DimGray
        {
            get
            {
                if (_b40 == null)
                {
                    _b40 = new RGBColor(0x69, 0x69, 0x69);
                }
                return _b40;
            }
        }

        public static RGBColor DodgerBlue
        {
            get
            {
                if (_b41 == null)
                {
                    _b41 = new RGBColor(30, 0x90, 0xff);
                }
                return _b41;
            }
        }

        public static RGBColor Firebrick
        {
            get
            {
                if (_b42 == null)
                {
                    _b42 = new RGBColor(0xb2, 0x22, 0x22);
                }
                return _b42;
            }
        }

        public static RGBColor FloralWhite
        {
            get
            {
                if (_b43 == null)
                {
                    _b43 = new RGBColor(0xff, 250, 240);
                }
                return _b43;
            }
        }

        public static RGBColor ForestGreen
        {
            get
            {
                if (_b44 == null)
                {
                    _b44 = new RGBColor(0x22, 0x8b, 0x22);
                }
                return _b44;
            }
        }

        public static RGBColor Fuchsia
        {
            get
            {
                if (_b45 == null)
                {
                    _b45 = new RGBColor(0xff, 0, 0xff);
                }
                return _b45;
            }
        }

        public static RGBColor Gainsboro
        {
            get
            {
                if (_b46 == null)
                {
                    _b46 = new RGBColor(220, 220, 220);
                }
                return _b46;
            }
        }

        public static RGBColor GhostWhite
        {
            get
            {
                if (_b47 == null)
                {
                    _b47 = new RGBColor(0xf8, 0xf8, 0xff);
                }
                return _b47;
            }
        }

        public static RGBColor Gold
        {
            get
            {
                if (_b48 == null)
                {
                    _b48 = new RGBColor(0xff, 0xd7, 0);
                }
                return _b48;
            }
        }

        public static RGBColor Goldenrod
        {
            get
            {
                if (_b49 == null)
                {
                    _b49 = new RGBColor(0xda, 0xa5, 0x20);
                }
                return _b49;
            }
        }

        public static RGBColor Gray
        {
            get
            {
                if (_b50 == null)
                {
                    _b50 = new RGBColor(0x80, 0x80, 0x80);
                }
                return _b50;
            }
        }

        public static RGBColor Green
        {
            get
            {
                if (_b51 == null)
                {
                    _b51 = new RGBColor(0, 0x80, 0);
                }
                return _b51;
            }
        }

        public static RGBColor GreenYellow
        {
            get
            {
                if (_b52 == null)
                {
                    _b52 = new RGBColor(0xad, 0xff, 0x2f);
                }
                return _b52;
            }
        }

        public static RGBColor Honeydew
        {
            get
            {
                if (_b53 == null)
                {
                    _b53 = new RGBColor(240, 0xff, 240);
                }
                return _b53;
            }
        }

        public static RGBColor HotPink
        {
            get
            {
                if (_b54 == null)
                {
                    _b54 = new RGBColor(0xff, 0x69, 180);
                }
                return _b54;
            }
        }

        public static RGBColor IndianRed
        {
            get
            {
                if (_b55 == null)
                {
                    _b55 = new RGBColor(0xcd, 0x5c, 0x5c);
                }
                return _b55;
            }
        }

        public static RGBColor Indigo
        {
            get
            {
                if (_b56 == null)
                {
                    _b56 = new RGBColor(0x4b, 0, 130);
                }
                return _b56;
            }
        }

        public static RGBColor Ivory
        {
            get
            {
                if (_b57 == null)
                {
                    _b57 = new RGBColor(0xff, 0xff, 240);
                }
                return _b57;
            }
        }

        public static RGBColor Khaki
        {
            get
            {
                if (_b58 == null)
                {
                    _b58 = new RGBColor(240, 230, 140);
                }
                return _b58;
            }
        }

        public static RGBColor Lavender
        {
            get
            {
                if (_b59 == null)
                {
                    _b59 = new RGBColor(230, 230, 250);
                }
                return _b59;
            }
        }

        public static RGBColor LavenderBlush
        {
            get
            {
                if (_b60 == null)
                {
                    _b60 = new RGBColor(0xff, 240, 0xf5);
                }
                return _b60;
            }
        }

        public static RGBColor LawnGreen
        {
            get
            {
                if (_b61 == null)
                {
                    _b61 = new RGBColor(0x7c, 0xfc, 0);
                }
                return _b61;
            }
        }

        public static RGBColor LemonChiffon
        {
            get
            {
                if (_b62 == null)
                {
                    _b62 = new RGBColor(0xff, 250, 0xcd);
                }
                return _b62;
            }
        }

        public static RGBColor LightBlue
        {
            get
            {
                if (_b63 == null)
                {
                    _b63 = new RGBColor(0xad, 0xd8, 230);
                }
                return _b63;
            }
        }

        public static RGBColor LightCoral
        {
            get
            {
                if (_b64 == null)
                {
                    _b64 = new RGBColor(240, 0x80, 0x80);
                }
                return _b64;
            }
        }

        public static RGBColor LightCyan
        {
            get
            {
                if (_b65 == null)
                {
                    _b65 = new RGBColor(0xe0, 0xff, 0xff);
                }
                return _b65;
            }
        }

        public static RGBColor LightGoldenrodYellow
        {
            get
            {
                if (_b66 == null)
                {
                    _b66 = new RGBColor(250, 250, 210);
                }
                return _b66;
            }
        }

        public static RGBColor LightGray
        {
            get
            {
                if (_b67 == null)
                {
                    _b67 = new RGBColor(0xd3, 0xd3, 0xd3);
                }
                return _b67;
            }
        }

        public static RGBColor LightGreen
        {
            get
            {
                if (_b68 == null)
                {
                    _b68 = new RGBColor(0x90, 0xee, 0x90);
                }
                return _b68;
            }
        }

        public static RGBColor LightPink
        {
            get
            {
                if (_b69 == null)
                {
                    _b69 = new RGBColor(0xff, 0xb6, 0xc1);
                }
                return _b69;
            }
        }

        public static RGBColor LightSalmon
        {
            get
            {
                if (_b70 == null)
                {
                    _b70 = new RGBColor(0xff, 160, 0x7a);
                }
                return _b70;
            }
        }

        public static RGBColor LightSeaGreen
        {
            get
            {
                if (_b71 == null)
                {
                    _b71 = new RGBColor(0x20, 0xb2, 170);
                }
                return _b71;
            }
        }

        public static RGBColor LightSkyBlue
        {
            get
            {
                if (_b72 == null)
                {
                    _b72 = new RGBColor(0x87, 0xce, 250);
                }
                return _b72;
            }
        }

        public static RGBColor LightSlateGray
        {
            get
            {
                if (_b73 == null)
                {
                    _b73 = new RGBColor(0x77, 0x88, 0x99);
                }
                return _b73;
            }
        }

        public static RGBColor LightSteelBlue
        {
            get
            {
                if (_b74 == null)
                {
                    _b74 = new RGBColor(0xb0, 0xc4, 0xde);
                }
                return _b74;
            }
        }

        public static RGBColor LightYellow
        {
            get
            {
                if (_b75 == null)
                {
                    _b75 = new RGBColor(0xff, 0xff, 0xe0);
                }
                return _b75;
            }
        }

        public static RGBColor Lime
        {
            get
            {
                if (_b76 == null)
                {
                    _b76 = new RGBColor(0, 0xff, 0);
                }
                return _b76;
            }
        }

        public static RGBColor LimeGreen
        {
            get
            {
                if (_b77 == null)
                {
                    _b77 = new RGBColor(50, 0xcd, 50);
                }
                return _b77;
            }
        }

        public static RGBColor Linen
        {
            get
            {
                if (_b78 == null)
                {
                    _b78 = new RGBColor(250, 240, 230);
                }
                return _b78;
            }
        }

        public static RGBColor Magenta
        {
            get
            {
                if (_b79 == null)
                {
                    _b79 = new RGBColor(0xff, 0, 0xff);
                }
                return _b79;
            }
        }

        public static RGBColor Maroon
        {
            get
            {
                if (_b80 == null)
                {
                    _b80 = new RGBColor(0x80, 0, 0);
                }
                return _b80;
            }
        }

        public static RGBColor MediumAquamarine
        {
            get
            {
                if (_b81 == null)
                {
                    _b81 = new RGBColor(0x66, 0xcd, 170);
                }
                return _b81;
            }
        }

        public static RGBColor MediumBlue
        {
            get
            {
                if (_b82 == null)
                {
                    _b82 = new RGBColor(0, 0, 0xcd);
                }
                return _b82;
            }
        }

        public static RGBColor MediumOrchid
        {
            get
            {
                if (_b83 == null)
                {
                    _b83 = new RGBColor(0xba, 0x55, 0xd3);
                }
                return _b83;
            }
        }

        public static RGBColor MediumPurple
        {
            get
            {
                if (_b84 == null)
                {
                    _b84 = new RGBColor(0x93, 0x70, 0xdb);
                }
                return _b84;
            }
        }

        public static RGBColor MediumSeaGreen
        {
            get
            {
                if (_b85 == null)
                {
                    _b85 = new RGBColor(60, 0xb3, 0x71);
                }
                return _b85;
            }
        }

        public static RGBColor MediumSlateBlue
        {
            get
            {
                if (_b86 == null)
                {
                    _b86 = new RGBColor(0x7b, 0x68, 0xee);
                }
                return _b86;
            }
        }

        public static RGBColor MediumSpringGreen
        {
            get
            {
                if (_b87 == null)
                {
                    _b87 = new RGBColor(0, 250, 0x9a);
                }
                return _b87;
            }
        }

        public static RGBColor MediumTurquoise
        {
            get
            {
                if (_b88 == null)
                {
                    _b88 = new RGBColor(0x48, 0xd1, 0xcc);
                }
                return _b88;
            }
        }

        public static RGBColor MediumVioletRed
        {
            get
            {
                if (_b89 == null)
                {
                    _b89 = new RGBColor(0xc7, 0x15, 0x85);
                }
                return _b89;
            }
        }

        public static RGBColor MidnightBlue
        {
            get
            {
                if (_b90 == null)
                {
                    _b90 = new RGBColor(0x19, 0x19, 0x70);
                }
                return _b90;
            }
        }

        public static RGBColor MintCream
        {
            get
            {
                if (_b91 == null)
                {
                    _b91 = new RGBColor(0xf5, 0xff, 250);
                }
                return _b91;
            }
        }

        public static RGBColor MistyRose
        {
            get
            {
                if (_b92 == null)
                {
                    _b92 = new RGBColor(0xff, 0xe4, 0xe1);
                }
                return _b92;
            }
        }

        public static RGBColor Moccasin
        {
            get
            {
                if (_b93 == null)
                {
                    _b93 = new RGBColor(0xff, 0xe4, 0xb5);
                }
                return _b93;
            }
        }

        public static RGBColor NavajoWhite
        {
            get
            {
                if (_b94 == null)
                {
                    _b94 = new RGBColor(0xff, 0xde, 0xad);
                }
                return _b94;
            }
        }

        public static RGBColor Navy
        {
            get
            {
                if (_b95 == null)
                {
                    _b95 = new RGBColor(0, 0, 0x80);
                }
                return _b95;
            }
        }

        public static RGBColor OldLace
        {
            get
            {
                if (_b96 == null)
                {
                    _b96 = new RGBColor(0xfd, 0xf5, 230);
                }
                return _b96;
            }
        }

        public static RGBColor Olive
        {
            get
            {
                if (_b97 == null)
                {
                    _b97 = new RGBColor(0x80, 0x80, 0);
                }
                return _b97;
            }
        }

        public static RGBColor OliveDrab
        {
            get
            {
                if (_b98 == null)
                {
                    _b98 = new RGBColor(0x6b, 0x8e, 0x23);
                }
                return _b98;
            }
        }

        public static RGBColor Orange
        {
            get
            {
                if (_b99 == null)
                {
                    _b99 = new RGBColor(0xff, 0xa5, 0);
                }
                return _b99;
            }
        }

        public static RGBColor OrangeRed
        {
            get
            {
                if (_b100 == null)
                {
                    _b100 = new RGBColor(0xff, 0x45, 0);
                }
                return _b100;
            }
        }

        public static RGBColor Orchid
        {
            get
            {
                if (_b101 == null)
                {
                    _b101 = new RGBColor(0xda, 0x70, 0xd6);
                }
                return _b101;
            }
        }

        public static RGBColor PaleGoldenrod
        {
            get
            {
                if (_b102 == null)
                {
                    _b102 = new RGBColor(0xee, 0xe8, 170);
                }
                return _b102;
            }
        }

        public static RGBColor PaleGreen
        {
            get
            {
                if (_b103 == null)
                {
                    _b103 = new RGBColor(0x98, 0xfb, 0x98);
                }
                return _b103;
            }
        }

        public static RGBColor PaleTurquoise
        {
            get
            {
                if (_b104 == null)
                {
                    _b104 = new RGBColor(0xaf, 0xee, 0xee);
                }
                return _b104;
            }
        }

        public static RGBColor PaleVioletRed
        {
            get
            {
                if (_b105 == null)
                {
                    _b105 = new RGBColor(0xdb, 0x70, 0x93);
                }
                return _b105;
            }
        }

        public static RGBColor PapayaWhip
        {
            get
            {
                if (_b106 == null)
                {
                    _b106 = new RGBColor(0xff, 0xef, 0xd5);
                }
                return _b106;
            }
        }

        public static RGBColor PeachPuff
        {
            get
            {
                if (_b107 == null)
                {
                    _b107 = new RGBColor(0xff, 0xda, 0xb9);
                }
                return _b107;
            }
        }

        public static RGBColor Peru
        {
            get
            {
                if (_b108 == null)
                {
                    _b108 = new RGBColor(0xcd, 0x85, 0x3f);
                }
                return _b108;
            }
        }

        public static RGBColor Pink
        {
            get
            {
                if (_b109 == null)
                {
                    _b109 = new RGBColor(0xff, 0xc0, 0xcb);
                }
                return _b109;
            }
        }

        public static RGBColor Plum
        {
            get
            {
                if (_b110 == null)
                {
                    _b110 = new RGBColor(0xdd, 160, 0xdd);
                }
                return _b110;
            }
        }

        public static RGBColor PowderBlue
        {
            get
            {
                if (_b111 == null)
                {
                    _b111 = new RGBColor(0xb0, 0xe0, 230);
                }
                return _b111;
            }
        }

        public static RGBColor Purple
        {
            get
            {
                if (_b112 == null)
                {
                    _b112 = new RGBColor(0x80, 0, 0x80);
                }
                return _b112;
            }
        }

        public static RGBColor Red
        {
            get
            {
                if (_b113 == null)
                {
                    _b113 = new RGBColor(0xff, 0, 0);
                }
                return _b113;
            }
        }

        public static RGBColor RosyBrown
        {
            get
            {
                if (_b114 == null)
                {
                    _b114 = new RGBColor(0xbc, 0x8f, 0x8f);
                }
                return _b114;
            }
        }

        public static RGBColor RoyalBlue
        {
            get
            {
                if (_b115 == null)
                {
                    _b115 = new RGBColor(0x41, 0x69, 0xe1);
                }
                return _b115;
            }
        }

        public static RGBColor SaddleBrown
        {
            get
            {
                if (_b116 == null)
                {
                    _b116 = new RGBColor(0x8b, 0x45, 0x13);
                }
                return _b116;
            }
        }

        public static RGBColor Salmon
        {
            get
            {
                if (_b117 == null)
                {
                    _b117 = new RGBColor(250, 0x80, 0x72);
                }
                return _b117;
            }
        }

        public static RGBColor SandyBrown
        {
            get
            {
                if (_b118 == null)
                {
                    _b118 = new RGBColor(0xf4, 0xa4, 0x60);
                }
                return _b118;
            }
        }

        public static RGBColor SeaGreen
        {
            get
            {
                if (_b119 == null)
                {
                    _b119 = new RGBColor(0x2e, 0x8b, 0x57);
                }
                return _b119;
            }
        }

        public static RGBColor SeaShell
        {
            get
            {
                if (_b120 == null)
                {
                    _b120 = new RGBColor(0xff, 0xf5, 0xee);
                }
                return _b120;
            }
        }

        public static RGBColor Sienna
        {
            get
            {
                if (_b121 == null)
                {
                    _b121 = new RGBColor(160, 0x52, 0x2d);
                }
                return _b121;
            }
        }

        public static RGBColor Silver
        {
            get
            {
                if (_b122 == null)
                {
                    _b122 = new RGBColor(0xc0, 0xc0, 0xc0);
                }
                return _b122;
            }
        }

        public static RGBColor SkyBlue
        {
            get
            {
                if (_b123 == null)
                {
                    _b123 = new RGBColor(0x87, 0xce, 0xeb);
                }
                return _b123;
            }
        }

        public static RGBColor SlateBlue
        {
            get
            {
                if (_b124 == null)
                {
                    _b124 = new RGBColor(0x6a, 90, 0xcd);
                }
                return _b124;
            }
        }

        public static RGBColor SlateGray
        {
            get
            {
                if (_b125 == null)
                {
                    _b125 = new RGBColor(0x70, 0x80, 0x90);
                }
                return _b125;
            }
        }

        public static RGBColor Snow
        {
            get
            {
                if (_b126 == null)
                {
                    _b126 = new RGBColor(0xff, 250, 250);
                }
                return _b126;
            }
        }

        public static RGBColor SpringGreen
        {
            get
            {
                if (_b127 == null)
                {
                    _b127 = new RGBColor(0, 0xff, 0x7f);
                }
                return _b127;
            }
        }

        public static RGBColor SteelBlue
        {
            get
            {
                if (_b128 == null)
                {
                    _b128 = new RGBColor(70, 130, 180);
                }
                return _b128;
            }
        }

        public static RGBColor Tan
        {
            get
            {
                if (_b129 == null)
                {
                    _b129 = new RGBColor(210, 180, 140);
                }
                return _b129;
            }
        }

        public static RGBColor Teal
        {
            get
            {
                if (_b130 == null)
                {
                    _b130 = new RGBColor(0, 0x80, 0x80);
                }
                return _b130;
            }
        }

        public static RGBColor Thistle
        {
            get
            {
                if (_b131 == null)
                {
                    _b131 = new RGBColor(0xd8, 0xbf, 0xd8);
                }
                return _b131;
            }
        }

        public static RGBColor Tomato
        {
            get
            {
                if (_b132 == null)
                {
                    _b132 = new RGBColor(0xff, 0x63, 0x47);
                }
                return _b132;
            }
        }

        public static RGBColor Transparent
        {
            get
            {
                if (_b133 == null)
                {
                    _b133 = new RGBColor(0xff, 0xff, 0xff);
                }
                return _b133;
            }
        }

        public static RGBColor Turquoise
        {
            get
            {
                if (_b134 == null)
                {
                    _b134 = new RGBColor(0x40, 0xe0, 0xd0);
                }
                return _b134;
            }
        }

        public static RGBColor Violet
        {
            get
            {
                if (_b135 == null)
                {
                    _b135 = new RGBColor(0xee, 130, 0xee);
                }
                return _b135;
            }
        }

        public static RGBColor Wheat
        {
            get
            {
                if (_b136 == null)
                {
                    _b136 = new RGBColor(0xf5, 0xde, 0xb3);
                }
                return _b136;
            }
        }

        public static RGBColor White
        {
            get
            {
                if (_b137 == null)
                {
                    _b137 = new RGBColor(0xff, 0xff, 0xff);
                }
                return _b137;
            }
        }

        public static RGBColor WhiteSmoke
        {
            get
            {
                if (_b138 == null)
                {
                    _b138 = new RGBColor(0xf5, 0xf5, 0xf5);
                }
                return _b138;
            }
        }

        public static RGBColor Yellow
        {
            get
            {
                if (_b139 == null)
                {
                    _b139 = new RGBColor(0xff, 0xff, 0);
                }
                return _b139;
            }
        }

        public static RGBColor YellowGreen
        {
            get
            {
                if (_b140 == null)
                {
                    _b140 = new RGBColor(0x9a, 0xcd, 50);
                }
                return _b140;
            }
        }
    }
}

