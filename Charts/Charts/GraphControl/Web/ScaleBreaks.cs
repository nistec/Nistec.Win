namespace Nistec.Charts.Web
{
    using System;
    using System.Drawing;

    internal class ScaleBreaks
    {
        public static void DrawScaleBreak(XmlChart xmlchart, Graphics g, Color BackColor, int ScaleBreakPos, int ScaleBreakHeigh, int x, int width, int h3D)
        {
            xmlchart.CurrentPhase = Phase.ScaleBreak;
            width++;
            Pen black = Pens.Black;
            Brush brush = new SolidBrush(BackColor);
            g.FillRectangle(brush, x + h3D, ScaleBreakPos - ScaleBreakHeigh, width - h3D, ScaleBreakHeigh);
            xmlchart.AddBar(ScaleBreakHeigh, width - h3D, (float) (x + h3D), (float) (ScaleBreakPos - ScaleBreakHeigh), new ColorItem(BackColor).ToString(), 0xff, 0, 0, "", "");
            g.DrawLine(black, x + h3D, ScaleBreakPos, x + width, ScaleBreakPos);
            xmlchart.AddLine(x + h3D, ScaleBreakPos - ScaleBreakHeigh, x + width, ScaleBreakPos - ScaleBreakHeigh, 1, "0", 0xff);
            g.DrawLine(black, (int) (x + h3D), (int) (ScaleBreakPos - ScaleBreakHeigh), (int) (x + width), (int) (ScaleBreakPos - ScaleBreakHeigh));
            xmlchart.AddLine(x + h3D, ScaleBreakPos - ScaleBreakHeigh, x + width, ScaleBreakPos - ScaleBreakHeigh, 1, "0", 0xff);
            Point[] points = new Point[] { new Point((x + h3D) + 1, ScaleBreakPos), new Point((x + h3D) + 1, ScaleBreakPos - ScaleBreakHeigh), new Point(x - 1, (ScaleBreakPos - ScaleBreakHeigh) + h3D), new Point(x - 1, ScaleBreakPos + h3D) };
            g.FillPolygon(brush, points);
            xmlchart.AddQuadrilater((x + h3D) + 1, ScaleBreakPos, (x + h3D) + 1, ScaleBreakPos - ScaleBreakHeigh, x - 1, (ScaleBreakPos - ScaleBreakHeigh) + h3D, x - 1, ScaleBreakPos + h3D, 0, new ColorItem(BackColor).ToString(), 0xff);
            g.DrawLine(black, (x + h3D) + 1, ScaleBreakPos, x, ScaleBreakPos + h3D);
            xmlchart.AddLine((x + h3D) + 1, ScaleBreakPos, x, ScaleBreakPos + h3D, 1, "0", 0xff);
            g.DrawLine(black, (x + h3D) + 1, ScaleBreakPos - ScaleBreakHeigh, x, (ScaleBreakPos - ScaleBreakHeigh) + h3D);
            xmlchart.AddLine((x + h3D) + 1, ScaleBreakPos - ScaleBreakHeigh, x, (ScaleBreakPos - ScaleBreakHeigh) + h3D, 1, "0", 0xff);
        }

        public static bool UsesScaleBreaks(ChartType ChartType)
        {
            if ((((ChartType != ChartType.Bars) && (ChartType != ChartType.Pipes)) && ((ChartType != ChartType.BarsMulti) && (ChartType != ChartType.Bars3D))) && ((ChartType !=ChartType.BarsWide3D) && (ChartType !=ChartType.BarsMulti3D)))
            {
                return false;
            }
            return true;
        }

        public static decimal ValRec(object DecimalValue, decimal ScaleBreakMin, decimal ScaleBreakMax)
        {
            decimal num = Convert.ToDecimal(DecimalValue);
            if (num <= ScaleBreakMin)
            {
                return num;
            }
            if (num >= ScaleBreakMax)
            {
                return ((num - ScaleBreakMax) + ScaleBreakMin);
            }
            return ScaleBreakMin;
        }
    }
}

