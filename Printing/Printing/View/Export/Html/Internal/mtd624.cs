namespace Nistec.Printing.View.Html
{
    using System;
    using System.Drawing;
    using System.IO;

    internal class mtd624
    {
        internal mtd624()
        {
        }

        internal static void mtd22(Color var0, ref StreamWriter var1)
        {
            string str = mtd620.mtd623(var0);
            if (str != string.Empty)
            {
                var1.Write(string.Format("background-color: {0}; ", str));
            }
        }
    }
}

