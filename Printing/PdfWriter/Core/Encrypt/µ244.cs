namespace MControl.Printing.Pdf.Core.Encrypt
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core.IO;
    using System;

    internal class A244 : A56
    {
        internal A244(Document b0) : base(b0)
        {
            base._A230 = new byte[5];
        }

        internal override void A100(int b1, int b2)
        {
            A243 A = new A243();
            A.A54(base._A230, base._A230.Length);
            A.A54((byte) b1);
            A.A54((byte) (b1 >> 8));
            A.A54((byte) (b1 >> 0x10));
            A.A54((byte) b2);
            A.A54((byte) (b2 >> 8));
            base._A231 = base._A234.A242(A.A221, 0, A.A2);
        }

        internal override void A237()
        {
            byte[] buffer = base.A239(base._A227.UserPassword);
            byte[] buffer2 = base.A239(base._A227.OwnerPassword);
            byte[] buffer3 = base._A234.A242(buffer2);
            base._A229 = base._A236.A57(buffer, buffer3, 5);
            base._A232 = this.A240();
            byte[] destinationArray = new byte[0x54];
            Array.Copy(buffer, 0, destinationArray, 0, 0x20);
            Array.Copy(base._A229, 0, destinationArray, 0x20, 0x20);
            Array.Copy(BitConverter.GetBytes(base._A232), 0, destinationArray, 0x40, 4);
            Array.Copy(base.A226, 0, destinationArray, 0x44, 0x10);
            Array.Copy(base._A234.A242(destinationArray), 0, base._A230, 0, 5);
            base._A228 = base._A236.A57(base.A238, base._A230);
        }

        internal override void A54(ref A55 b5)
        {
            base.A54(ref b5);
            b5.A59("/Filter /Standard");
            b5.A59("/Length 40");
            b5.A59("/V 1");
            b5.A59("/R 2");
            b5.A54("/O <");
            b5.A58(base._A229);
            b5.A59(">");
            b5.A54("/U <");
            b5.A58(base._A228);
            b5.A59(">");
            b5.A59(string.Format("/P {0} ", this.A240()));
            b5.A59(">>");
            b5.A59("endobj");
        }

        internal override byte[] A57(byte[] b3, int b4)
        {
            return base._A236.A57(b3, b4, base._A231, 10);
        }
    }
}

