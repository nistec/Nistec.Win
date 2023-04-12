namespace MControl.Printing.Pdf.Core.Element
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core.Encrypt;
    using MControl.Printing.Pdf.Core.IO;
    using System;

    internal class A195 : A91
    {
        private Bookmarks _b0;

        internal A195(Document b1)
        {
            base._A92 = b1;
            this._b0 = new Bookmarks(null);
        }

        internal override void A110()
        {
            A93 A = base._A92.A93;
            if (base._A95 == 0)
            {
                int num2;
                A.A95 = (num2 = A.A95) + 1;
                base._A95 = num2;
            }
            for (int i = 0; i < this._b0.Count; i++)
            {
                this._b0[i].A110(A);
            }
        }

        internal override void A54(ref A55 b2)
        {
            int count = this._b0.Count;
            base._A92.A93.A94(b2.A2, 0);
            b2.A59(string.Format("{0} 0 obj", this.A95));
            b2.A59("<<");
            b2.A59("/Type /Outlines ");
            if (count > 0)
            {
                b2.A59(string.Format("/First {0} 0 R", this._b0[0].A95));
                b2.A59(string.Format("/Last {0} 0 R", this._b0[count - 1].A95));
            }
            b2.A59(string.Format("/Count {0}", count));
            b2.A59(">>");
            b2.A59("endobj");
            this.b3(ref b2, this._b0);
        }

        private void b3(ref A55 b2, Bookmarks b0)
        {
            int count = b0.Count;
            for (int i = 0; i < count; i++)
            {
                Bookmark bookmark = b0[i];
                A56 A = base._A92.A56;
                base._A92.A93.A94(b2.A2, 0);
                b2.A59(string.Format("{0} 0 obj", bookmark.A95));
                if (A != null)
                {
                    A.A100(bookmark.A95, 0);
                }
                b2.A59("<<");
                if ((bookmark.Title != null) && (bookmark.Title.Length > 0))
                {
                    b2.A54("/Title ");
                    if (A != null)
                    {
                        A26.A54(ref b2, bookmark.Title, A);
                    }
                    else
                    {
                        A26.A54(ref b2, A15.A26(bookmark.Title), A);
                    }
                }
                if (bookmark.A209 == null)
                {
                    b2.A59(string.Format("/Parent {0} 0 R", this.A95));
                }
                else
                {
                    b2.A59(string.Format("/Parent {0} 0 R", bookmark.A209.A95));
                }
                if (bookmark.Page != null)
                {
                    b2.A59(string.Format("/Dest [ {0} 0 R /XYZ {1} {2} {3} ]", new object[] { bookmark.Page.A95, bookmark.Left, bookmark.Page.A98(bookmark.Top), bookmark.Zoom / 100f }));
                }
                if (bookmark.Nodes.Count > 0)
                {
                    b2.A59(string.Format("/First {0} 0 R", bookmark.Nodes[0].A95));
                    b2.A59(string.Format("/Last {0} 0 R", bookmark.Nodes[bookmark.Nodes.Count - 1].A95));
                    if ((i + 1) == count)
                    {
                        if ((i - 1) > -1)
                        {
                            b2.A59(string.Format("/Prev {0} 0 R", b0[i - 1].A95));
                        }
                    }
                    else
                    {
                        b2.A59(string.Format("/Next {0} 0 R", b0[i + 1].A95));
                    }
                    if (bookmark.Opened)
                    {
                        b2.A59(string.Format("/Count {0}", bookmark.Nodes.Count));
                    }
                    else
                    {
                        b2.A59(string.Format("/Count -{0}", bookmark.Nodes.Count));
                    }
                }
                else if ((i + 1) == count)
                {
                    if ((i - 1) > -1)
                    {
                        b2.A59(string.Format("/Prev {0} 0 R", b0[i - 1].A95));
                    }
                }
                else
                {
                    b2.A59(string.Format("/Next {0} 0 R", b0[i + 1].A95));
                }
                b2.A59(">>");
                b2.A59("endobj");
                if (bookmark.Nodes.Count > 0)
                {
                    this.b3(ref b2, bookmark.Nodes);
                }
            }
        }

        internal Bookmarks A208
        {
            get
            {
                return this._b0;
            }
        }
    }
}

