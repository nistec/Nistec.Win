namespace MControl.Printing.Pdf.Drawing
{
    using System;
    using System.Text;

    public class Code93ExtendedGraphics : Code93Graphics
    {
        private static string b0 = BarcodeGraphics.A427;

        internal override string A429()
        {
            return b0;
        }

        internal override string A451()
        {
            int length = base._A416.Length;
            StringBuilder builder = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                string str = this.b1(base._A416[i]);
                if (str != null)
                {
                    builder.Append(str);
                }
                else
                {
                    builder.Append(base._A416[i]);
                }
            }
            return builder.ToString();
        }

        private string b1(char c)
        {
            switch (c)
            {
                case '\0':
                    return ":U";

                case '\x0001':
                    return "?A";

                case '\x0002':
                    return "?B";

                case '\x0003':
                    return "?C";

                case '\x0004':
                    return "?D";

                case '\x0005':
                    return "?E";

                case '\x0006':
                    return "?F";

                case '\a':
                    return "?G";

                case '\b':
                    return "?H";

                case '\t':
                    return "?I";

                case '\n':
                    return "?J";

                case '\v':
                    return "?K";

                case '\f':
                    return "?L";

                case '\r':
                    return "?M";

                case '\x000e':
                    return "?N";

                case '\x000f':
                    return "?O";

                case '\x0010':
                    return "?P";

                case '\x0011':
                    return "?Q";

                case '\x0012':
                    return "?R";

                case '\x0013':
                    return "?S";

                case '\x0014':
                    return "?T";

                case '\x0015':
                    return "?U";

                case '\x0016':
                    return "?V";

                case '\x0017':
                    return "?W";

                case '\x0018':
                    return "?X";

                case '\x0019':
                    return "?Y";

                case '\x001a':
                    return "?Z";

                case '\x001b':
                    return ":A";

                case '\x001c':
                    return ":B";

                case '\x001d':
                    return ":C";

                case '\x001e':
                    return ":D";

                case '\x001f':
                    return ":E";

                case '!':
                    return "!A";

                case '"':
                    return "!B";

                case '#':
                    return "!C";

                case '$':
                    return "!D";

                case '%':
                    return "!E";

                case '&':
                    return "!F";

                case '\'':
                    return "!G";

                case '(':
                    return "!H";

                case ')':
                    return "!I";

                case '*':
                    return "!J";

                case '+':
                    return "!K";

                case ',':
                    return "!L";

                case '/':
                    return "!O";

                case ':':
                    return "!Z";

                case ';':
                    return ":F";

                case '<':
                    return ":G";

                case '=':
                    return ":H";

                case '>':
                    return ":I";

                case '?':
                    return ":J";

                case '@':
                    return ":V";

                case '[':
                    return ":K";

                case '\\':
                    return ":L";

                case ']':
                    return ":M";

                case '^':
                    return ":N";

                case '_':
                    return ":O";

                case '`':
                    return ":W";

                case 'a':
                    return "~A";

                case 'b':
                    return "~B";

                case 'c':
                    return "~C";

                case 'd':
                    return "~D";

                case 'e':
                    return "~E";

                case 'f':
                    return "~F";

                case 'g':
                    return "~G";

                case 'h':
                    return "~H";

                case 'i':
                    return "~I";

                case 'j':
                    return "~J";

                case 'k':
                    return "~K";

                case 'l':
                    return "~L";

                case 'm':
                    return "~M";

                case 'n':
                    return "~N";

                case 'o':
                    return "~O";

                case 'p':
                    return "~P";

                case 'q':
                    return "~Q";

                case 'r':
                    return "~R";

                case 's':
                    return "~S";

                case 't':
                    return "~T";

                case 'u':
                    return "~U";

                case 'v':
                    return "~V";

                case 'w':
                    return "~W";

                case 'x':
                    return "~X";

                case 'y':
                    return "~Y";

                case 'z':
                    return "~Z";

                case '{':
                    return ":P";

                case '|':
                    return ":Q";

                case '}':
                    return ":R";

                case '~':
                    return ":S";

                case '\x007f':
                    return ":T";
            }
            return null;
        }

        public override BarCodeType BarcodeType
        {
            get
            {
                return BarCodeType.Code93Extended;
            }
        }

        public override bool CodeTextAsSymbol
        {
            get
            {
                return false;
            }
            set
            {
            }
        }
    }
}

