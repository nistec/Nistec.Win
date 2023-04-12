namespace MControl.Printing.Pdf.Core.Text
{
    using System;

    internal class A578 : A566
    {
        private int _b0;
        private int _b1;
        private float _b2;

        internal A578(int b0, A523 b3) : base(b3)
        {
            this._b0 = b0;
        }

        internal static string A591(A566 b4)
        {
            string str = b4.A580.A38;
            char[] chArray = new char[b4.A293];
            int index = 0;
            int num2 = b4.A579 + b4.A293;
            for (int i = b4.A579; i < num2; i++)
            {
                chArray[index] = str[i];
                index++;
            }
            return new string(chArray);
        }

        internal override float A211
        {
            get
            {
                return this._b2;
            }
            set
            {
                this._b2 = value;
            }
        }

        internal override int A293
        {
            get
            {
                return this._b1;
            }
            set
            {
                this._b1 = value;
            }
        }

        internal override int A579
        {
            get
            {
                return this._b0;
            }
        }
    }
}

