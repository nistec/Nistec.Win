namespace Nistec.Printing.View.Pdf
{
    using System;

    internal class mtd1033 : mtd1022
    {
        private int _var0;
        private int _var1;
        private float _var2;

        internal mtd1033(int var0, mtd1015 var3) : base(var3)
        {
            this._var0 = var0;
        }

        internal static string mtd1044(mtd1022 var4)
        {
            string str = var4.mtd1034.mtd51;
            char[] chArray = new char[var4.mtd166];
            int index = 0;
            int num2 = var4.mtd504 + var4.mtd166;
            for (int i = var4.mtd504; i < num2; i++)
            {
                chArray[index] = str[i];
                index++;
            }
            return new string(chArray);
        }

        internal override int mtd166
        {
            get
            {
                return this._var1;
            }
            set
            {
                this._var1 = value;
            }
        }

        internal override float mtd30
        {
            get
            {
                return this._var2;
            }
            set
            {
                this._var2 = value;
            }
        }

        internal override int mtd504
        {
            get
            {
                return this._var0;
            }
        }
    }
}

