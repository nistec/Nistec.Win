namespace Nistec.Printing.View.Pdf
{
    using Nistec.Printing.View;
    using System;

    internal class mtd60 : mtd942
    {
        internal mtd60()
        {
        }

        internal static void mtd23(float mtd970, float mtd971, PropDoc mtd972, ref mtd944 mtd973, ref mtd742 mtd974)
        {
            float num = (mtd972.LinePositionX1 * 72f) + mtd970;
            float num2 = mtd973.mtd947((mtd972.LinePositionY1 * 72f) + mtd971);
            float num3 = (mtd972.LinePositionX2 * 72f) + mtd970;
            float num4 = mtd973.mtd947((mtd972.LinePositionY2 * 72f) + mtd971);
            mtd942.mtd975(mtd972.mtd43, mtd972.BorderColor, mtd942.mtd976(mtd972.LineStyle), ref mtd973, ref mtd974);
            mtd942.mtd977(mtd974, num, num2);
            mtd942.mtd978(mtd974, num3, num4);
            mtd942.mtd950(mtd974, mtd672.mtd674, false);
        }
    }
}

