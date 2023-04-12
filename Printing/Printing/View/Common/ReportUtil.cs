using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Nistec.Printing.View.Templates;

namespace Nistec.Printing.View
{
    public class ReportUtil
    {

        public const string Unit = "in";//"in" //"cm"
        public const float Dpi = 96f;//96f //37.8f

        public static float CalcPageWidth(ReportTemplate report)
        {
            float pageWidth = report.ReportWidth;

            string unit = ReportUtil.Unit;
            System.Drawing.SizeF size = ReportUtil.GetPaperSize(report.PaperKind.ToString(), ref unit);
            if (size != System.Drawing.SizeF.Empty)
            {
                if (report.Orientation == PageOrientation.Landscape)
                    pageWidth = ReportUtil.ConvertToPoint(size.Height, unit);
                else
                    pageWidth = ReportUtil.ConvertToPoint(size.Width, unit);
            }
            return pageWidth;
        }

        public static System.Drawing.SizeF GetPaperSize(string paper, ref string unit)
        {
            try
            {
                string s = ResexLibrary.GetPaperSize(paper);
                if (s == null || s == "")
                {
                    return System.Drawing.SizeF.Empty;
                }

                string[] res = s.Split(';');
                unit = res[2];

                return new System.Drawing.SizeF(float.Parse(res[0]), float.Parse(res[1]));
            }
            catch
            {
                throw new Exception("Unknown paper");
            }
        }

        public static float ConvertToPoint(float size, string unit)
        {
            //x Pixels / 96 dpi (screen resolution) =  y  inches

            if (unit.Equals("cm"))
            {
                return size * 37.8F;
            }
            else if (unit.Equals("in"))
            {
                return (float)(size * 2.54 * 37.8F);
            }
            else
                return size;
        }


    }
}
