namespace MControl.Printing.Pdf.Core.Fonts
{
    using System;
    using System.Collections;
    using System.Reflection;

    internal class A313
    {
        private ArrayList _b0 = new ArrayList();
        private ushort _b1;
        private ushort _b2;
        private MControl.Printing.Pdf.Core.Fonts.A281 _b3;
        private ushort _b4;
        private ushort _b5;
        private byte[] _b6;

        internal A313(MControl.Printing.Pdf.Core.Fonts.A281 b7)
        {
            this._b3 = b7;
        }

        internal void A237(A313 b11)
        {
            this._b6 = new byte[b11._b6.Length];
            b11._b6.CopyTo(this._b6, 0);
            this._b2 = (ushort) this.A293;
            double y = Math.Floor(Math.Log((double) this._b2, 2.0));
            double num2 = Math.Pow(2.0, y);
            this._b5 = (ushort) ((int) (num2 * 16.0));
            this._b1 = (ushort) ((int) y);
            this._b4 = (ushort) ((this._b2 * 0x10) - this._b5);
        }

        internal void A312(A286 b12)
        {
            b12.A300(0);
            this._b6 = b12.A287(4);
            this._b2 = b12.A302();
            this._b5 = b12.A302();
            this._b1 = b12.A302();
            this._b4 = b12.A302();
            this.b13(b12);
        }

        internal void A317(A51 b14)
        {
            A314 A = new A314();
            A.A237(b14);
            this._b0.Add(A);
        }

        internal void A318(A290 b12)
        {
            b12.A300(0);
            b12.A301(A316);
            for (int i = 0; i < this.A293; i++)
            {
                this[i].A318(b12);
            }
        }

        internal void A319(A290 b12)
        {
            b12.A300(0);
            b12.A301(A316);
            for (int i = 0; i < this.A293; i++)
            {
                this[i].A320(b12);
            }
        }

        internal void A54(A290 b12)
        {
            b12.A300(0);
            b12.A291(this._b6);
            b12.A297(this._b2);
            b12.A297(this._b5);
            b12.A297(this._b1);
            b12.A297(this._b4);
            this.b15(b12);
        }

        private void b13(A286 b12)
        {
            this._b0.Clear();
            for (int i = 0; i < this.b10; i++)
            {
                A314 A = new A314();
                A.A312(b12);
                this._b0.Add(A);
            }
        }

        private void b15(A290 b12)
        {
            for (int i = 0; i < this.A293; i++)
            {
                this[i].A54(b12);
            }
        }

        internal MControl.Printing.Pdf.Core.Fonts.A281 A281
        {
            get
            {
                return this._b3;
            }
        }

        internal int A293
        {
            get
            {
                return this._b0.Count;
            }
        }

        internal static int A316
        {
            get
            {
                return 12;
            }
        }

        internal A314 this[string b9]
        {
            get
            {
                for (int i = 0; i < this.A293; i++)
                {
                    if (this[i].A315 == b9)
                    {
                        return this[i];
                    }
                }
                return null;
            }
        }

        internal A314 this[int b8]
        {
            get
            {
                return (this._b0[b8] as A314);
            }
        }

        private int b10
        {
            get
            {
                return Convert.ToInt32(this._b2);
            }
        }
    }
}

