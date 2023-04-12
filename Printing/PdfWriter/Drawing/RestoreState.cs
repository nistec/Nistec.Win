namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.IO;
    using System;

    public class RestoreState : GraphicsElement
    {
        internal override void A119(ref A120 b0, ref A112 b1)
        {
            if (b0.A522.Count > 0)
            {
                b1.A176("Q ");
                b0.A438();
            }
        }
    }
}

