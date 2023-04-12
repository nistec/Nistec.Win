namespace MControl.Printing.Pdf.Core.Fonts
{
    using System;
    using System.Drawing;

    internal class A245 : A246
    {
        internal A245(FontStyle b0)
        {
            base._A247 = "Courier";
            if ((b0 & FontStyle.Underline) == FontStyle.Underline)
            {
                base._A248 = FontStyle.Underline;
            }
        }
    }
}

