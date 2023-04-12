namespace MControl.Printing.Pdf.Core.IO
{
    using System;
    using System.IO;

    internal class A553 : A55
    {
        private Stream _b0;

        internal A553(Stream b0)
        {
            this._b0 = b0;
        }

        protected override void _A548(byte b1)
        {
            this._b0.WriteByte(b1);
        }

        internal override int A2
        {
            get
            {
                return (int) this._b0.Length;
            }
        }
    }
}

