namespace MControl.Printing.Pdf.Drawing
{
    using System;
    using System.Drawing;

    public class WebColor : RGBColor
    {
        public WebColor(string htmlcolor)
        {
            Color color = ColorTranslator.FromHtml(htmlcolor);
            RGBColor.A237(this, color.R, color.G, color.B);
        }
    }
}

