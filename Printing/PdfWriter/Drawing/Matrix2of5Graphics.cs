namespace MControl.Printing.Pdf.Drawing
{
    using System;

    public class Matrix2of5Graphics : Industrial2of5Graphics
    {
        internal override void A432()
        {
            base._A450 = base._A416;
            if (this.EnableCheckSum)
            {
                base._A455 = Industrial2of5Graphics.A448(base._A450).ToString();
                base._A450 = base._A450 + base._A455;
            }
            else
            {
                base._A455 = "";
            }
            base._A450 = 'S' + base._A450 + 'S';
        }

        internal override float A452(char c)
        {
            if (c == 'S')
            {
                return (base._A466 + 5f);
            }
            return ((2f * base._A466) + (4f * base._A467));
        }

        internal override string A453(char c)
        {
            switch (c)
            {
                case '0':
                    return "nnwwnn";

                case '1':
                    return "wnnnwn";

                case '2':
                    return "nwnnwn";

                case '3':
                    return "wwnnnn";

                case '4':
                    return "nnwnwn";

                case '5':
                    return "wnwnnn";

                case '6':
                    return "nwwnnn";

                case '7':
                    return "nnnwwn";

                case '8':
                    return "wnnwnn";

                case '9':
                    return "nwnwnn";

                case 'S':
                    return "wnnnnn";
            }
            return "";
        }

        public override BarCodeType BarcodeType
        {
            get
            {
                return BarCodeType.Matrix2of5;
            }
        }
    }
}

