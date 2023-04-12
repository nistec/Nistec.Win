namespace MControl.Printing.Pdf
{
    using MControl.Printing.Pdf.Core.Collection;
    using MControl.Printing.Pdf.Core.Element;
    using MControl.Printing.Pdf.Core.IO;
    using MControl.Printing.Pdf.Core;
    using System;
    using System.Reflection;

    public class Pages
    {
        private A7 _b0;
        private Document _b1;
        private int _b2;

        internal Pages(Document doc)
        {
            this._b1 = doc;
            this._b0 = new A7();
        }

        internal void A110()
        {
            int num2;
            A93 A1 = this._b1.A93;
            A1.A95 = (num2 = A1.A95) + 1;
            this._b2 = num2;
            for (int i = 0; i < this._b0.A2; i++)
            {
                this._b0[i].A110();
            }
        }

        internal void A54(ref A598 b3, ref A55 b4)
        {
            this._b1.A93.A94(b4.A2, 0);
            b4.A59(string.Format("{0} 0 obj", this._b2));
            b4.A59("<<");
            b4.A59("/Type /Pages");
            b4.A54("/Kids [ ");
            int num = this._b0.A2;
            for (int i = 0; i < num; i++)
            {
                b4.A54(string.Format("{0} 0 R ", this._b0[i].A95));
            }
            b4.A59("]");
            b4.A59(string.Format("/Count {0}", num));
            b4.A59(">>");
            b4.A59("endobj");
            if (num > 0)
            {
                for (int j = 0; j < num; j++)
                {
                    this._b0[j].A54(ref b3, ref b4);
                }
            }
        }

        public void Add(Page page)
        {
            page.A97 = this._b1;
            this._b0.A3(page);
        }

        public int IndexOf(Page page)
        {
            return this._b0.A11(page);
        }

        public void Insert(Page page, int index)
        {
            page.A97 = this._b1;
            this._b0.A8(page, index);
        }

        public void Remove(Page page)
        {
            this._b0.A10(page);
        }

        public void RemoveAt(int index)
        {
            this._b0.A9(index);
        }

        internal int A95
        {
            get
            {
                return this._b2;
            }
        }

        public int Count
        {
            get
            {
                return this._b0.A2;
            }
        }

        public Page this[int index]
        {
            get
            {
                return this._b0[index];
            }
        }
    }
}

