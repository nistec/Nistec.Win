namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.IO;
    using System;
    using System.Collections;

    public class Code128Graphics : BarcodeGraphics
    {
        private int[] _b4;
        internal static char[] A449 = b8();
        private static string b0 = BarcodeGraphics.A427;
        private static char[] b1 = b9();
        private static char[] b2 = b10();
        private static char[] b3 = b11();
        private const int b5 = 0;
        private const int b6 = 1;
        private const int b7 = 2;

        internal override string A429()
        {
            return b0;
        }

        internal override void A430(ref A120 b29, ref A112 b30, float b31)
        {
            float num = (int) (base._A413 + base.X);
            float num2 = (int) b29.A98(this.A440());
            GraphicsElement.A160(base._A421, ref b29, ref b30);
            int length = this._b4.Length;
            for (int i = 0; i < length; i++)
            {
                num += this.b32(ref b29, ref b30, b20(this._b4[i]), num, num2);
            }
            base.A444(ref b29, ref b30, num2, b31, null);
        }

        internal override float A431(bool b16)
        {
            float num = 0f;
            if (b16)
            {
                this.A432();
            }
            int length = this._b4.Length;
            for (int i = 0; i < length; i++)
            {
                num += b17(this._b4[i]);
            }
            num *= base._A423;
            return (num + (base._A413 + base._A415));
        }

        internal override void A432()
        {
            int num23;
            int[] numArray;
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = -1;
            int num5 = 0;
            ArrayList list = new ArrayList(2 * base._A416.Length);
            int length = base._A416.Length;
            for (int i = 0; i < length; i++)
            {
                char ch = base._A416[i];
                if (this.b21(ch))
                {
                    num3++;
                    if (num > 0)
                    {
                        switch (num4)
                        {
                            case -1:
                                list.Add(0x67);
                                num4 = 0;
                                break;

                            case 0:
                                break;

                            default:
                                if ((num4 == 1) && (num == 1))
                                {
                                    list.Add(0x62);
                                }
                                else
                                {
                                    list.Add(0x65);
                                    num4 = 0;
                                }
                                break;
                        }
                        for (int k = num5; k < i; k++)
                        {
                            list.Add(b25(base._A416[k], true));
                        }
                        num5 = i;
                        num = 0;
                        continue;
                    }
                    if (num2 <= 0)
                    {
                        continue;
                    }
                    switch (num4)
                    {
                        case -1:
                            list.Add(0x68);
                            num4 = 1;
                            break;

                        case 1:
                            break;

                        default:
                            if ((num4 == 0) && (num2 == 1))
                            {
                                list.Add(0x62);
                            }
                            else
                            {
                                list.Add(100);
                                num4 = 1;
                            }
                            break;
                    }
                    for (int j = num5; j < i; j++)
                    {
                        list.Add(b25(base._A416[j], false));
                    }
                    num5 = i;
                    num2 = 0;
                    continue;
                }
                if (this.b23(ch))
                {
                    num++;
                    if (num2 > 0)
                    {
                        switch (num4)
                        {
                            case -1:
                                list.Add(0x68);
                                num4 = 1;
                                break;

                            case 1:
                                break;

                            default:
                                if ((num4 == 0) && (num2 == 1))
                                {
                                    list.Add(0x62);
                                }
                                else
                                {
                                    list.Add(100);
                                    num4 = 1;
                                }
                                break;
                        }
                        for (int m = num5; m < i; m++)
                        {
                            list.Add(b25(base._A416[m], false));
                        }
                        num5 = i;
                        num2 = 0;
                        continue;
                    }
                    if (num3 > 0)
                    {
                        if (num3 >= 4)
                        {
                            if (num4 == -1)
                            {
                                list.Add(0x69);
                            }
                            else if (num4 != 2)
                            {
                                list.Add(0x63);
                            }
                            if ((num3 % 2) == 0)
                            {
                                int num11 = 0;
                                for (int n = num5; n < i; n++)
                                {
                                    num11 = BarcodeGraphics.A439(base._A416[n]) * 10;
                                    n++;
                                    num11 += BarcodeGraphics.A439(base._A416[n]);
                                    list.Add(num11);
                                }
                                num4 = 2;
                            }
                            else
                            {
                                int num13 = 0;
                                for (int num14 = num5; num14 < (i - 1); num14++)
                                {
                                    num13 = BarcodeGraphics.A439(base._A416[num14]) * 10;
                                    num14++;
                                    num13 += BarcodeGraphics.A439(base._A416[num14]);
                                    list.Add(num13);
                                }
                                list.Add(0x65);
                                list.Add(b25(base._A416[i - 1], true));
                                num4 = 0;
                            }
                        }
                        else
                        {
                            if (num4 == -1)
                            {
                                list.Add(0x67);
                            }
                            else if (num4 != 0)
                            {
                                list.Add(0x65);
                                num4 = 0;
                            }
                            for (int num15 = num5; num15 < i; num15++)
                            {
                                list.Add(b25(base._A416[num15], true));
                            }
                        }
                        num5 = i;
                        num3 = 0;
                    }
                    continue;
                }
                num2++;
                if (num > 0)
                {
                    switch (num4)
                    {
                        case -1:
                            list.Add(0x67);
                            num4 = 0;
                            break;

                        case 0:
                            break;

                        default:
                            if ((num4 == 1) && (num == 1))
                            {
                                list.Add(0x62);
                            }
                            else
                            {
                                list.Add(0x65);
                                num4 = 0;
                            }
                            break;
                    }
                    for (int num16 = num5; num16 < i; num16++)
                    {
                        list.Add(b25(base._A416[num16], true));
                    }
                    num5 = i;
                    num = 0;
                    continue;
                }
                if (num3 > 0)
                {
                    if (num3 >= 4)
                    {
                        if (num4 == -1)
                        {
                            list.Add(0x69);
                        }
                        else if (num4 != 2)
                        {
                            list.Add(0x63);
                        }
                        if ((num3 % 2) == 0)
                        {
                            int num17 = 0;
                            for (int num18 = num5; num18 < i; num18++)
                            {
                                num17 = BarcodeGraphics.A439(base._A416[num18]) * 10;
                                num18++;
                                num17 += BarcodeGraphics.A439(base._A416[num18]);
                                list.Add(num17);
                            }
                            num4 = 2;
                        }
                        else
                        {
                            int num19 = 0;
                            for (int num20 = num5; num20 < (i - 1); num20++)
                            {
                                num19 = BarcodeGraphics.A439(base._A416[num20]) * 10;
                                num20++;
                                num19 += BarcodeGraphics.A439(base._A416[num20]);
                                list.Add(num19);
                            }
                            list.Add(100);
                            list.Add(b25(base._A416[i - 1], false));
                            num4 = 1;
                        }
                    }
                    else
                    {
                        if (num4 == -1)
                        {
                            list.Add(0x68);
                        }
                        else if (num4 != 1)
                        {
                            list.Add(100);
                            num4 = 1;
                        }
                        for (int num21 = num5; num21 < i; num21++)
                        {
                            list.Add(b25(base._A416[num21], false));
                        }
                    }
                    num5 = i;
                    num3 = 0;
                }
            }
            if (num <= 0)
            {
                if (num2 <= 0)
                {
                    if (num3 > 0)
                    {
                        if (num3 >= 4)
                        {
                            if (num4 == -1)
                            {
                                list.Add(0x69);
                            }
                            else if (num4 != 2)
                            {
                                list.Add(0x63);
                            }
                            if ((num3 % 2) == 0)
                            {
                                int num24 = 0;
                                for (int num25 = num5; num25 < length; num25++)
                                {
                                    num24 = BarcodeGraphics.A439(base._A416[num25]) * 10;
                                    num25++;
                                    num24 += BarcodeGraphics.A439(base._A416[num25]);
                                    list.Add(num24);
                                }
                            }
                            else
                            {
                                int num26 = 0;
                                for (int num27 = num5; num27 < (length - 1); num27++)
                                {
                                    num26 = BarcodeGraphics.A439(base._A416[num27]) * 10;
                                    num27++;
                                    num26 += BarcodeGraphics.A439(base._A416[num27]);
                                    list.Add(num26);
                                }
                                list.Add(100);
                                list.Add(b25(base._A416[length - 1], false));
                                num4 = 1;
                            }
                        }
                        else
                        {
                            if (num4 == -1)
                            {
                                list.Add(0x68);
                            }
                            else if (num4 != 1)
                            {
                                list.Add(100);
                            }
                            for (int num28 = num5; num28 < length; num28++)
                            {
                                list.Add(b25(base._A416[num28], false));
                            }
                        }
                    }
                    goto Label_0854;
                }
                switch (num4)
                {
                    case -1:
                        list.Add(0x68);
                        goto Label_0697;

                    case 1:
                        goto Label_0697;
                }
                if ((num4 == 0) && (num2 == 1))
                {
                    list.Add(0x62);
                }
                else
                {
                    list.Add(100);
                }
            }
            else
            {
                switch (num4)
                {
                    case -1:
                        list.Add(0x67);
                        break;

                    case 0:
                        break;

                    default:
                        if ((num4 == 1) && (num == 1))
                        {
                            list.Add(0x62);
                        }
                        else
                        {
                            list.Add(0x65);
                        }
                        break;
                }
                for (int num22 = num5; num22 < length; num22++)
                {
                    list.Add(b25(base._A416[num22], true));
                }
                goto Label_0854;
            }
        Label_0697:
            num23 = num5;
            while (num23 < length)
            {
                list.Add(b25(base._A416[num23], false));
                num23++;
            }
        Label_0854:
            numArray = this.b26(list);
            this._b4 = numArray;
        }

        internal override void A433()
        {
        }

        internal override void A86()
        {
            this._b4 = null;
        }

        private static char[] b10()
        {
            int index = 0;
            string str = "\x0001\x0002\x0003\x0004\x0005\x0006\a\b\t\n\v\f\r\x000e\x000f\x0010\x0011\x0012\x0013\x0014\x0015\x0016\x0017\x0018\x0019\x001a\x001b\x001c\x001d\x001e\x001f";
            int num2 = str.Length + 1;
            char[] chArray = new char[num2];
            chArray[index] = '\0';
            index++;
            b12(ref chArray, ref index, str);
            return chArray;
        }

        private static char[] b11()
        {
            int num = 0;
            string str = "`abcdefghijklmnopqrstuvwxyz{|}~\x007f";
            char[] chArray = new char[str.Length];
            b12(ref chArray, ref num, str);
            return chArray;
        }

        private static void b12(ref char[] b13, ref int b14, string b15)
        {
            int length = b15.Length;
            for (int i = 0; i < length; i++)
            {
                b13[b14] = b15[i];
                b14++;
            }
        }

        private static float b17(int b19)
        {
            switch (b19)
            {
                case 0:
                    return 11f;

                case 1:
                    return 11f;

                case 2:
                    return 11f;

                case 3:
                    return 11f;

                case 4:
                    return 11f;

                case 5:
                    return 11f;

                case 6:
                    return 11f;

                case 7:
                    return 11f;

                case 8:
                    return 11f;

                case 9:
                    return 11f;

                case 10:
                    return 11f;

                case 11:
                    return 11f;

                case 12:
                    return 11f;

                case 13:
                    return 11f;

                case 14:
                    return 11f;

                case 15:
                    return 11f;

                case 0x10:
                    return 11f;

                case 0x11:
                    return 11f;

                case 0x12:
                    return 11f;

                case 0x13:
                    return 11f;

                case 20:
                    return 11f;

                case 0x15:
                    return 11f;

                case 0x16:
                    return 11f;

                case 0x17:
                    return 11f;

                case 0x18:
                    return 11f;

                case 0x19:
                    return 11f;

                case 0x1a:
                    return 11f;

                case 0x1b:
                    return 11f;

                case 0x1c:
                    return 11f;

                case 0x1d:
                    return 11f;

                case 30:
                    return 11f;

                case 0x1f:
                    return 11f;

                case 0x20:
                    return 11f;

                case 0x21:
                    return 11f;

                case 0x22:
                    return 11f;

                case 0x23:
                    return 11f;

                case 0x24:
                    return 11f;

                case 0x25:
                    return 11f;

                case 0x26:
                    return 11f;

                case 0x27:
                    return 11f;

                case 40:
                    return 11f;

                case 0x29:
                    return 11f;

                case 0x2a:
                    return 11f;

                case 0x2b:
                    return 11f;

                case 0x2c:
                    return 11f;

                case 0x2d:
                    return 11f;

                case 0x2e:
                    return 11f;

                case 0x2f:
                    return 11f;

                case 0x30:
                    return 11f;

                case 0x31:
                    return 11f;

                case 50:
                    return 11f;

                case 0x33:
                    return 11f;

                case 0x34:
                    return 11f;

                case 0x35:
                    return 11f;

                case 0x36:
                    return 11f;

                case 0x37:
                    return 11f;

                case 0x38:
                    return 11f;

                case 0x39:
                    return 11f;

                case 0x3a:
                    return 11f;

                case 0x3b:
                    return 11f;

                case 60:
                    return 38f;

                case 0x3d:
                    return 11f;

                case 0x3e:
                    return 11f;

                case 0x3f:
                    return 11f;

                case 0x40:
                    return 11f;

                case 0x41:
                    return 11f;

                case 0x42:
                    return 11f;

                case 0x43:
                    return 11f;

                case 0x44:
                    return 11f;

                case 0x45:
                    return 11f;

                case 70:
                    return 11f;

                case 0x47:
                    return 11f;

                case 0x48:
                    return 11f;

                case 0x49:
                    return 11f;

                case 0x4a:
                    return 11f;

                case 0x4b:
                    return 11f;

                case 0x4c:
                    return 11f;

                case 0x4d:
                    return 11f;

                case 0x4e:
                    return 11f;

                case 0x4f:
                    return 11f;

                case 80:
                    return 11f;

                case 0x51:
                    return 11f;

                case 0x52:
                    return 11f;

                case 0x53:
                    return 11f;

                case 0x54:
                    return 11f;

                case 0x55:
                    return 11f;

                case 0x56:
                    return 11f;

                case 0x57:
                    return 11f;

                case 0x58:
                    return 11f;

                case 0x59:
                    return 11f;

                case 90:
                    return 11f;

                case 0x5b:
                    return 11f;

                case 0x5c:
                    return 11f;

                case 0x5d:
                    return 11f;

                case 0x5e:
                    return 11f;

                case 0x5f:
                    return 11f;

                case 0x60:
                    return 11f;

                case 0x61:
                    return 11f;

                case 0x62:
                    return 11f;

                case 0x63:
                    return 11f;

                case 100:
                    return 11f;

                case 0x65:
                    return 11f;

                case 0x66:
                    return 11f;

                case 0x67:
                    return 11f;

                case 0x68:
                    return 11f;

                case 0x69:
                    return 11f;

                case 0x6a:
                    return 13f;
            }
            return 0f;
        }

        private float b18(char c)
        {
            return (base._A423 * (c - '0'));
        }

        private static string b20(int b14)
        {
            switch (b14)
            {
                case 0:
                    return "212222";

                case 1:
                    return "222122";

                case 2:
                    return "222221";

                case 3:
                    return "121223";

                case 4:
                    return "121322";

                case 5:
                    return "131222";

                case 6:
                    return "122213";

                case 7:
                    return "122312";

                case 8:
                    return "132212";

                case 9:
                    return "221213";

                case 10:
                    return "221312";

                case 11:
                    return "231212";

                case 12:
                    return "112232";

                case 13:
                    return "122132";

                case 14:
                    return "122231";

                case 15:
                    return "113222";

                case 0x10:
                    return "123122";

                case 0x11:
                    return "123221";

                case 0x12:
                    return "223211";

                case 0x13:
                    return "221132";

                case 20:
                    return "221231";

                case 0x15:
                    return "213212";

                case 0x16:
                    return "223112";

                case 0x17:
                    return "312131";

                case 0x18:
                    return "311222";

                case 0x19:
                    return "321122";

                case 0x1a:
                    return "321221";

                case 0x1b:
                    return "312212";

                case 0x1c:
                    return "322112";

                case 0x1d:
                    return "322211";

                case 30:
                    return "212123";

                case 0x1f:
                    return "212321";

                case 0x20:
                    return "232121";

                case 0x21:
                    return "111323";

                case 0x22:
                    return "131123";

                case 0x23:
                    return "131321";

                case 0x24:
                    return "112313";

                case 0x25:
                    return "132113";

                case 0x26:
                    return "132311";

                case 0x27:
                    return "211313";

                case 40:
                    return "231113";

                case 0x29:
                    return "231311";

                case 0x2a:
                    return "112133";

                case 0x2b:
                    return "112331";

                case 0x2c:
                    return "132131";

                case 0x2d:
                    return "113123";

                case 0x2e:
                    return "113321";

                case 0x2f:
                    return "133121";

                case 0x30:
                    return "313121";

                case 0x31:
                    return "211331";

                case 50:
                    return "231131";

                case 0x33:
                    return "213113";

                case 0x34:
                    return "213311";

                case 0x35:
                    return "213131";

                case 0x36:
                    return "311123";

                case 0x37:
                    return "311321";

                case 0x38:
                    return "331121";

                case 0x39:
                    return "312113";

                case 0x3a:
                    return "312311";

                case 0x3b:
                    return "332111";

                case 60:
                    return "314111";

                case 0x3d:
                    return "221411";

                case 0x3e:
                    return "431111";

                case 0x3f:
                    return "111224";

                case 0x40:
                    return "111422";

                case 0x41:
                    return "121124";

                case 0x42:
                    return "121421";

                case 0x43:
                    return "141122";

                case 0x44:
                    return "141221";

                case 0x45:
                    return "112214";

                case 70:
                    return "112412";

                case 0x47:
                    return "122114";

                case 0x48:
                    return "122411";

                case 0x49:
                    return "142112";

                case 0x4a:
                    return "142211";

                case 0x4b:
                    return "241211";

                case 0x4c:
                    return "221114";

                case 0x4d:
                    return "413111";

                case 0x4e:
                    return "241112";

                case 0x4f:
                    return "134111";

                case 80:
                    return "111242";

                case 0x51:
                    return "121142";

                case 0x52:
                    return "121241";

                case 0x53:
                    return "114212";

                case 0x54:
                    return "124112";

                case 0x55:
                    return "124211";

                case 0x56:
                    return "411212";

                case 0x57:
                    return "421112";

                case 0x58:
                    return "421211";

                case 0x59:
                    return "212141";

                case 90:
                    return "214121";

                case 0x5b:
                    return "412121";

                case 0x5c:
                    return "111143";

                case 0x5d:
                    return "111341";

                case 0x5e:
                    return "131141";

                case 0x5f:
                    return "114113";

                case 0x60:
                    return "114311";

                case 0x61:
                    return "411113";

                case 0x62:
                    return "411311";

                case 0x63:
                    return "113141";

                case 100:
                    return "114131";

                case 0x65:
                    return "311141";

                case 0x66:
                    return "411131";

                case 0x67:
                    return "211412";

                case 0x68:
                    return "211214";

                case 0x69:
                    return "211232";

                case 0x6a:
                    return "2331112";
            }
            return "";
        }

        private bool b21(char b22)
        {
            return ((b22 >= '0') && (b22 <= '9'));
        }

        private bool b23(char b22)
        {
            int length = b2.Length;
            for (int i = 0; i < length; i++)
            {
                if (b2[i] == b22)
                {
                    return true;
                }
            }
            return false;
        }

        private bool b24(char b22)
        {
            int length = b3.Length;
            for (int i = 0; i < length; i++)
            {
                if (b3[i] == b22)
                {
                    return true;
                }
            }
            return false;
        }

        private static int b25(char b22, bool b28)
        {
            char[] chArray;
            if (b28)
            {
                chArray = A449;
            }
            else
            {
                chArray = b1;
            }
            int length = chArray.Length;
            for (int i = 0; i < length; i++)
            {
                if (b22 == chArray[i])
                {
                    return i;
                }
            }
            return -1;
        }

        private int[] b26(ArrayList b27)
        {
            int num2;
            int num3;
            int[] numArray;
            int count = b27.Count;
            if (this.BarcodeType == BarCodeType.EAN128)
            {
                numArray = new int[b27.Count + 3];
                numArray[0] = (int) b27[0];
                numArray[1] = 0x66;
                num2 = numArray[0] + 0x66;
                num3 = 2;
            }
            else
            {
                numArray = new int[b27.Count + 2];
                numArray[0] = (int) b27[0];
                num2 = numArray[0];
                num3 = 1;
            }
            for (int i = 1; i < count; i++)
            {
                int num = (int) b27[i];
                numArray[num3] = num;
                num2 += num * num3;
                num3++;
            }
            numArray[numArray.Length - 2] = num2 % 0x67;
            numArray[numArray.Length - 1] = 0x6a;
            return numArray;
        }

        private float b32(ref A120 b29, ref A112 b30, string b33, float b34, float b35)
        {
            float num = 0f;
            float num2 = 0f;
            for (int i = 0; i < b33.Length; i++)
            {
                num2 = this.b18(b33[i]);
                if ((i % 2) == 0)
                {
                    GraphicsElement.A437(b30, b34, b35, num2, base._A422);
                    GraphicsElement.A180(b30, GraphicsMode.fill, false);
                }
                b34 += num2;
                num += num2;
            }
            return num;
        }

        private static char[] b8()
        {
            int num = 0;
            string str = " !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_";
            string str2 = "\x0001\x0002\x0003\x0004\x0005\x0006\a\b\t\n\v\f\r\x000e\x000f\x0010\x0011\x0012\x0013\x0014\x0015\x0016\x0017\x0018\x0019\x001a\x001b\x001c\x001d\x001e\x001f\x00f5\x00f6\x00f7\x00f8\x00f9\x00fa\x00fb\x00fc\x00fd\x00fe\x00ff";
            int num2 = (str.Length + str2.Length) + 1;
            char[] chArray = new char[num2];
            b12(ref chArray, ref num, str);
            chArray[num] = '\0';
            num++;
            b12(ref chArray, ref num, str2);
            return chArray;
        }

        private static char[] b9()
        {
            int num = 0;
            string str = " !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~\x007f\x00f5\x00f6\x00f7\x00f8\x00f9\x00fa\x00fb\x00fc\x00fd\x00fe\x00ff";
            char[] chArray = new char[str.Length];
            b12(ref chArray, ref num, str);
            return chArray;
        }

        public override BarCodeType BarcodeType
        {
            get
            {
                return BarCodeType.Code128;
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

        public override bool EnableCheckSum
        {
            get
            {
                return true;
            }
            set
            {
            }
        }
    }
}

