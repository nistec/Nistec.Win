namespace Nistec.Printing.View.Pdf
{
    using System;
    using System.Drawing;

    internal class mtd688 : mtd819
    {
        private static int[] var0 = new int[] { 
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
            0x116, 0x3ce, 0x3c1, 0x3ce, 980, 0x2cf, 0x315, 790, 0x317, 690, 960, 0x3ab, 0x225, 0x357, 0x38f, 0x3a5, 
            0x38f, 0x3b1, 0x3ce, 0x2f3, 0x34e, 0x2fa, 0x2f9, 0x23b, 0x2a5, 0x2fb, 760, 0x2f7, 0x2f2, 0x1ee, 0x228, 0x219, 
            0x241, 0x2b4, 0x312, 0x314, 0x314, 790, 0x319, 0x31a, 0x330, 0x337, 0x315, 0x349, 0x337, 0x341, 0x330, 0x33f, 
            0x39b, 0x2e8, 0x2d3, 0x2ed, 790, 0x318, 0x2b7, 0x308, 0x300, 0x318, 0x2f7, 0x2c3, 0x2c4, 0x2aa, 0x2bd, 0x33a, 
            0x32f, 0x315, 0x315, 0x2c3, 0x2af, 0x2b8, 0x2b1, 0x312, 0x313, 0x2c9, 0x317, 0x311, 0x317, 0x369, 0x2f9, 0x2fa, 
            0x2fa, 0x2f7, 0x2f7, 0x37c, 0x37c, 0x314, 0x310, 0x1b6, 0x8a, 0x115, 0x19f, 0x188, 0x188, 0x29c, 0x29c, 0, 
            390, 390, 0x13d, 0x13d, 0x114, 0x114, 0x1fd, 0x1fd, 410, 410, 0xea, 0xea, 0x14e, 0x14e, 0, 0, 
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
            0, 0x2dc, 0x220, 0x220, 910, 0x29b, 760, 760, 0x308, 0x253, 0x2b6, 0x272, 0x314, 0x314, 0x314, 0x314, 
            0x314, 0x314, 0x314, 0x314, 0x314, 0x314, 0x314, 0x314, 0x314, 0x314, 0x314, 0x314, 0x314, 0x314, 0x314, 0x314, 
            0x314, 0x314, 0x314, 0x314, 0x314, 0x314, 0x314, 0x314, 0x314, 0x314, 0x314, 0x314, 0x314, 0x314, 0x314, 0x314, 
            0x314, 0x314, 0x314, 0x314, 0x37e, 0x346, 0x3f8, 0x1ca, 0x2ec, 0x39c, 0x2ec, 0x396, 0x39f, 0x3a0, 0x3a0, 0x342, 
            0x369, 0x33c, 0x39c, 0x39c, 0x395, 930, 0x3a3, 0x1cf, 0x373, 0x344, 0x344, 0x363, 0x363, 0x2b8, 0x2b8, 0x36a, 
            0, 0x36a, 760, 0x3b2, 0x303, 0x361, 0x303, 0x378, 0x3c7, 0x378, 0x33f, 0x369, 0x39f, 970, 0x396, 0
         };

        internal mtd688(FontStyle var1)
        {
            base._mtd808 = "ZapfDingbats";
            if ((var1 & FontStyle.Underline) == FontStyle.Underline)
            {
                base._mtd809 = FontStyle.Underline;
            }
        }

        protected override int _mtd810(int var2)
        {
            return var0[var2];
        }
    }
}
