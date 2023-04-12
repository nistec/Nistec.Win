namespace MControl.Printing.Pdf.Core.Fonts
{
    using System;
    using System.Drawing;

    internal class A250 : A246
    {
        internal A250(FontStyle b0)
        {
            base._A247 = "Courier-BoldOblique";
            if ((b0 & FontStyle.Underline) == FontStyle.Underline)
            {
                base._A248 = FontStyle.Underline | FontStyle.Italic | FontStyle.Bold;
            }
            else
            {
                base._A248 = FontStyle.Italic | FontStyle.Bold;
            }
        }
    }
}

