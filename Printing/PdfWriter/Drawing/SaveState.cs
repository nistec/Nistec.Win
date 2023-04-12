namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.IO;
    using System;

    public class SaveState : GraphicsElement
    {
        internal override void A119(ref A120 b0, ref A112 b1)
        {
            b1.A176("q ");
            b0.A435();
        }
    }
}

