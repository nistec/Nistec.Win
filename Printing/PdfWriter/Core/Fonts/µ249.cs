namespace MControl.Printing.Pdf.Core.Fonts
{
    using System;
    using System.Drawing;

    internal class A249 : A246
    {
        internal A249(FontStyle b0)
        {
            base._A247 = "Courier-Bold";
            if ((b0 & FontStyle.Underline) == FontStyle.Underline)
            {
                base._A248 = FontStyle.Underline | FontStyle.Bold;
            }
            else
            {
                base._A248 = FontStyle.Bold;
            }
        }
    }
}

