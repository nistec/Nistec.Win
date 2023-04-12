namespace MControl.Printing.Pdf.Core.Fonts
{
    using System;
    using System.Drawing;

    internal class A251 : A246
    {
        internal A251(FontStyle b0)
        {
            base._A247 = "Courier-Oblique";
            if ((b0 & FontStyle.Underline) == FontStyle.Underline)
            {
                base._A248 = FontStyle.Underline | FontStyle.Italic;
            }
            else
            {
                base._A248 = FontStyle.Italic;
            }
        }
    }
}

