using System;
using System.Drawing;


namespace MControl.Printing.View
{
    /// <summary>
    /// Summary description for ServiceMethods.
    /// </summary>
    public sealed class ServiceMethods
    {
        public ServiceMethods() { }

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
