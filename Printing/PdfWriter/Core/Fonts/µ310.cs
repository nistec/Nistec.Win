namespace MControl.Printing.Pdf.Core.Fonts
{
    using System;
    using System.Collections;
    using System.Reflection;

    internal class A310 : A311
    {
        private ArrayList b0 = new ArrayList();

        internal A310()
        {
        }

        internal override void A312(A286 b3, int b4)
        {
            int num = b3.A298;
            this.b2(b3);
            b3.A300(num);
            base.A312(b3, b4);
        }

        private void b2(A286 b3)
        {
            ushort num = 0;
            do
            {
                num = b3.A302();
                this.b0.Add(b3.A302());
                if ((num & 1) != 0)
                {
                    b3.A301(4);
                }
                else
                {
                    b3.A301(2);
                }
                if ((num & 8) != 0)
                {
                    b3.A301(2);
                }
                else if ((num & 0x40) != 0)
                {
                    b3.A301(4);
                }
                else if ((num & 0x80) != 0)
                {
                    b3.A301(8);
                }
            }
            while ((num & 0x20) != 0);
        }

        internal int A293
        {
            get
            {
                return this.b0.Count;
            }
        }

        internal ushort this[int b1]
        {
            get
            {
                return (ushort) this.b0[b1];
            }
        }
    }
}

