namespace Nistec.Printing.View.Pdf
{
    using System;

    internal class mtd652
    {
        internal mtd652()
        {
        }

        internal static void mtd710(ref mtd711 var0, string var1, mtd712 var2)
        {
            int length = var1.Length;
            byte[] buffer = new byte[length];
            byte[] buffer2 = new byte[(length * 2) + 2];
            buffer2[0] = 0xfe;
            buffer2[1] = 0xff;
            int index = 2;
            bool flag = false;
            for (int i = 0; i < length; i++)
            {
                char ch = var1[i];
                if (ch > '\x00ff')
                {
                    flag = true;
                }
                buffer[i] = (byte) ch;
                buffer2[index] = (byte) (ch >> 8);
                index++;
                buffer2[index] = (byte) ch;
                index++;
            }
            byte[] buffer3 = null;
            if (flag)
            {
                buffer3 = buffer2;
            }
            else
            {
                buffer3 = buffer;
            }
            if (var2 != null)
            {
                buffer3 = var2.mtd713(buffer3, buffer3.Length);
                var0.mtd710("<");
                var0.mtd714(buffer3);
                var0.mtd715(">");
            }
            else
            {
                var0.mtd710("(");
                var0.mtd710(buffer3, buffer3.Length);
                var0.mtd715(")");
            }
        }
    }
}

