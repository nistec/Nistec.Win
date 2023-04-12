namespace MControl.Printing.Pdf.Core.Encrypt
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core.IO;
    using System;

    internal class A241 : A56
    {
        internal A241(Document b0) : base(b0)
        {
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
            byte[] sourceArray = base.A239(base._A227.UserPassword);
            byte[] buffer2 = base.A239(base._A227.OwnerPassword);
            byte[] buffer3 = sourceArray;
            byte[] destinationArray = new byte[0x10];
            byte[] buffer5 = buffer2;
            buffer5 = base._A234.A242(buffer5);
            for (int i = 0; i < 50; i++)
            {
                buffer5 = base._A234.A242(buffer5);
            }
            Array.Copy(buffer5, destinationArray, 0x10);
            buffer3 = base._A236.A57(buffer3, destinationArray);
            byte[] buffer6 = new byte[destinationArray.Length];
            for (int j = 1; j < 20; j++)
            {
                for (int n = 0; n < destinationArray.Length; n++)
                {
                    buffer6[n] = (byte) (destinationArray[n] ^ j);
                }
                buffer3 = base._A236.A57(buffer3, buffer6);
            }
            base._A229 = buffer3;
            base._A232 = this.A240();
            buffer3 = new byte[0x10];
            destinationArray = new byte[0x54];
            Array.Copy(sourceArray, 0, destinationArray, 0, 0x20);
            Array.Copy(base._A229, 0, destinationArray, 0x20, 0x20);
            Array.Copy(BitConverter.GetBytes(base._A232), 0, destinationArray, 0x40, 4);
            Array.Copy(base.A226, 0, destinationArray, 0x44, 0x10);
            buffer5 = base._A234.A242(destinationArray);
            for (int k = 0; k < 50; k++)
            {
                buffer5 = base._A234.A242(buffer5);
            }
            Array.Copy(buffer5, 0, buffer3, 0, 0x10);
            base._A230 = buffer3;
            int num5 = base._A230.Length + 5;
            if (num5 > 0x10)
            {
                num5 = 0x10;
            }
            base._A231 = new byte[num5];
            buffer3 = new byte[0x20];
            destinationArray = new byte[0x30];
            base.A238.CopyTo(destinationArray, 0);
            base.A226.CopyTo(destinationArray, 0x20);
            buffer5 = base._A234.A242(destinationArray);
            buffer5 = base._A236.A57(buffer5, base._A230);
            byte[] buffer7 = base._A230;
            buffer6 = new byte[buffer7.Length];
            for (int m = 1; m < 20; m++)
            {
                for (int num7 = 0; num7 < buffer7.Length; num7++)
                {
                    buffer6[num7] = (byte) (buffer7[num7] ^ m);
                }
                buffer5 = base._A236.A57(buffer5, buffer6);
            }
            Array.Copy(buffer5, 0, buffer3, 0, 0x10);
            Array.Copy(base.A238, 0, buffer3, 0x10, 0x10);
            base._A228 = buffer3;
        }

        internal override int A240()
        {
            int num = base.A240();
            if (!base._A227.AllowFormFilling)
            {
                num -= 0x100;
            }
            if (!base._A227.AllowAccessibility)
            {
                num -= 0x200;
            }
            if (!base._A227.AllowDocumentAssembly)
            {
                num -= 0x400;
            }
            if (!base._A227.AllowHighQualityPrinting)
            {
                num -= 0x800;
            }
            return num;
        }

        internal override void A54(ref A55 b5)
        {
            base.A54(ref b5);
            b5.A59("/Filter /Standard");
            b5.A59("/Length 128");
            b5.A59("/V 2");
            b5.A59("/R 3");
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
            return base._A236.A57(b3, b4, base._A231, base._A231.Length);
        }
    }
}

