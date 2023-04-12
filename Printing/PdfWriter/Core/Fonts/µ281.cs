namespace MControl.Printing.Pdf.Core.Fonts
{
    using System;

    internal class A281
    {
        private MControl.Printing.Pdf.Core.Fonts.A292 _b0;
        private A280 _b1;
        private MControl.Printing.Pdf.Core.Fonts.A338 _b10;
        private MControl.Printing.Pdf.Core.Fonts.A339 _b11;
        private A280 _b12;
        private A313 _b13;
        private A280 _b2;
        private MControl.Printing.Pdf.Core.Fonts.A332 _b3;
        private MControl.Printing.Pdf.Core.Fonts.A333 _b4;
        private MControl.Printing.Pdf.Core.Fonts.A334 _b5;
        private MControl.Printing.Pdf.Core.Fonts.A335 _b6;
        private MControl.Printing.Pdf.Core.Fonts.A336 _b7;
        private MControl.Printing.Pdf.Core.Fonts.A337 _b8;

        internal A281()
        {
            this._b13 = new A313(this);
            this._b4 = new MControl.Printing.Pdf.Core.Fonts.A333(this);
            this._b8 = new MControl.Printing.Pdf.Core.Fonts.A337(this);
            this._b5 = new MControl.Printing.Pdf.Core.Fonts.A334(this);
            this._b6 = new MControl.Printing.Pdf.Core.Fonts.A335(this);
            this._b11 = new MControl.Printing.Pdf.Core.Fonts.A339(this);
            this._b10 = new MControl.Printing.Pdf.Core.Fonts.A338(this);
            this._b7 = new MControl.Printing.Pdf.Core.Fonts.A336(this);
            this._b3 = new MControl.Printing.Pdf.Core.Fonts.A332(this);
            this._b0 = new MControl.Printing.Pdf.Core.Fonts.A292(this);
            this._b12 = new A280(this, "prep");
            this._b1 = new A280(this, "cvt ");
            this._b2 = new A280(this, "fpgm");
        }

        internal ushort A257(char b18)
        {
            return this.A257(this.A292[Convert.ToUInt16(b18)]);
        }

        internal ushort A257(ushort b15)
        {
            A352 A = this.A335[b15];
            return A.A353;
        }

        internal void A312(byte[] b21)
        {
            A286 A = new A286(b21);
            this._b13.A312(A);
            this._b4.A312(A);
            this._b5.A312(A);
            this._b8.A312(A);
            this._b6.A312(A);
            this._b7.A312(A);
            this._b3.A312(A);
            this._b12.A312(A);
            this._b1.A312(A);
            this._b2.A312(A);
            this._b10.A312(A);
            this._b11.A312(A);
            this._b0.A312(A);
        }

        internal ushort[] A350(ushort[] b17)
        {
            A346 A = new A346();
            this.b14(0, A);
            for (int i = 0; i < b17.Length; i++)
            {
                this.b14(this.A292[Convert.ToUInt16(b17[i])], A);
            }
            return A.A351;
        }

        internal ushort A354(ushort b18)
        {
            return this.A292[b18];
        }

        internal byte[] A54(ushort[] b17, string b24)
        {
            A281 A = new A281();
            A290 A2 = new A290();
            A284 A3 = new A284();
            A3.A296 = b17;
            A3.A356 = this.A350(b17);
            A3.A357 = b24;
            A.b19(this, A3);
            A.b22(A2);
            return A2.A221;
        }

        private void b14(ushort b15, A346 b16)
        {
            if (b16.A3(b15))
            {
                A347 A = this.A332.A348[b15];
                if ((A != null) && (A.A349 is A310))
                {
                    A310 A2 = A.A349 as A310;
                    for (int i = 0; i < A2.A293; i++)
                    {
                        this.b14(A2[i], b16);
                    }
                }
            }
        }

        private void b19(A281 b20, A284 b21)
        {
            this.A332.A237(b20.A332, b21);
            this.A336.A237(b20.A336);
            this.A333.A237(b20.A333);
            this.A337.A237(b20.A337);
            this.A334.A237(b20.A334);
            this.A335.A237(b20.A335);
            this.A344.A237(b20.A344);
            this.A340.A237(b20.A340);
            this.A341.A237(b20.A341);
            this._b13.A237(b20.A345);
        }

        private void b22(A290 b23)
        {
            this._b13.A54(b23);
            this.A333.A54(b23);
            this.A334.A54(b23);
            this.A337.A54(b23);
            this.A335.A54(b23);
            this.A336.A54(b23);
            this.A332.A54(b23);
            this.A344.A54(b23);
            this.A340.A54(b23);
            this.A341.A54(b23);
            b23.A238();
            this._b13.A319(b23);
            this._b13.A318(b23);
            this.A333.A355(b23);
        }

        internal MControl.Printing.Pdf.Core.Fonts.A292 A292
        {
            get
            {
                return this._b0;
            }
        }

        internal MControl.Printing.Pdf.Core.Fonts.A332 A332
        {
            get
            {
                return this._b3;
            }
        }

        internal MControl.Printing.Pdf.Core.Fonts.A333 A333
        {
            get
            {
                return this._b4;
            }
        }

        internal MControl.Printing.Pdf.Core.Fonts.A334 A334
        {
            get
            {
                return this._b5;
            }
        }

        internal MControl.Printing.Pdf.Core.Fonts.A335 A335
        {
            get
            {
                return this._b6;
            }
        }

        internal MControl.Printing.Pdf.Core.Fonts.A336 A336
        {
            get
            {
                return this._b7;
            }
        }

        internal MControl.Printing.Pdf.Core.Fonts.A337 A337
        {
            get
            {
                return this._b8;
            }
        }

        internal MControl.Printing.Pdf.Core.Fonts.A338 A338
        {
            get
            {
                return this._b10;
            }
        }

        internal MControl.Printing.Pdf.Core.Fonts.A339 A339
        {
            get
            {
                return this._b11;
            }
        }

        internal A280 A340
        {
            get
            {
                return this._b1;
            }
        }

        internal A280 A341
        {
            get
            {
                return this._b2;
            }
        }

        internal bool A342
        {
            get
            {
                return (this.A338.A343 != 2);
            }
        }

        internal A280 A344
        {
            get
            {
                return this._b12;
            }
        }

        internal A313 A345
        {
            get
            {
                return this._b13;
            }
        }
    }
}

