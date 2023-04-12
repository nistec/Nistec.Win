namespace MControl.Printing.Pdf.Core
{
    using MControl.Printing.Pdf.Core.Encrypt;
    using MControl.Printing.Pdf.Core.IO;
    using System;

    internal class A26
    {
        internal A26()
        {
        }

        internal static void A54(ref A55 b0, string b1, A56 b2)
        {
            A54(ref b0, b1, b2, true);
        }

        internal static void A54(ref A55 b0, string b1, A56 b2, bool newline)
        {
            int length = b1.Length;
            byte[] buffer = new byte[length];
            byte[] buffer2 = new byte[(length * 2) + 2];
            buffer2[0] = 0xfe;
            buffer2[1] = 0xff;
            int index = 2;
            bool flag = false;
            for (int i = 0; i < length; i++)
            {
                char ch = b1[i];
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
            byte[] data = null;
            if (flag)
            {
                data = buffer2;
            }
            else
            {
                data = buffer;
            }
            if (b2 != null)
            {
                data = b2.A57(data, data.Length);
                b0.A54("<");
                b0.A58(data);
                if (newline)
                {
                    b0.A59(">");
                }
                else
                {
                    b0.A54("> ");
                }
            }
            else
            {
                b0.A54("(");
                b0.A54(data, data.Length);
                if (newline)
                {
                    b0.A59(")");
                }
                else
                {
                    b0.A54(") ");
                }
            }
        }
    }
}

