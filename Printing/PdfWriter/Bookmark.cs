namespace MControl.Printing.Pdf
{
    using MControl.Printing.Pdf.Core.Element;
    using System;

    public class Bookmark
    {
        private string _b0;
        private MControl.Printing.Pdf.Page _b1;
        private float _b2;
        private float _b3;
        private float _b4;
        private bool _b5;
        private int _b6;
        private Bookmark _b7;
        private Bookmarks _b8;

        public Bookmark(string title, MControl.Printing.Pdf.Page page)
        {
            this._b0 = title;
            this._b1 = page;
            this._b2 = 0f;
            this._b3 = 0f;
            this._b4 = 0f;
            this._b5 = true;
            this._b8 = new Bookmarks(this);
        }

        public Bookmark(string title, MControl.Printing.Pdf.Page page, float top)
        {
            this._b0 = title;
            this._b1 = page;
            this._b2 = 0f;
            this._b3 = top;
            this._b4 = 0f;
            this._b5 = true;
            this._b8 = new Bookmarks(this);
        }

        public Bookmark(string title, MControl.Printing.Pdf.Page page, float top, float left)
        {
            this._b0 = title;
            this._b1 = page;
            this._b2 = left;
            this._b3 = top;
            this._b4 = 0f;
            this._b5 = true;
            this._b8 = new Bookmarks(this);
        }

        public Bookmark(string title, MControl.Printing.Pdf.Page page, float top, float left, float zoom)
        {
            this._b0 = title;
            this._b1 = page;
            this._b2 = left;
            this._b3 = top;
            this._b4 = zoom;
            this._b5 = true;
            this._b8 = new Bookmarks(this);
        }

        public Bookmark(string title, MControl.Printing.Pdf.Page page, float top, float left, float zoom, bool opened)
        {
            this._b0 = title;
            this._b1 = page;
            this._b2 = left;
            this._b3 = top;
            this._b4 = zoom;
            this._b5 = opened;
            this._b8 = new Bookmarks(this);
        }

        internal void A110(A93 b9)
        {
            int num2;
            b9.A95 = (num2 = b9.A95) + 1;
            this._b6 = num2;
            for (int i = 0; i < this._b8.Count; i++)
            {
                this._b8[i].A110(b9);
            }
        }

        internal Bookmark A209
        {
            get
            {
                return this._b7;
            }
            set
            {
                this._b7 = value;
            }
        }

        internal int A95
        {
            get
            {
                return this._b6;
            }
        }

        public float Left
        {
            get
            {
                return this._b2;
            }
        }

        public Bookmarks Nodes
        {
            get
            {
                return this._b8;
            }
        }

        public bool Opened
        {
            get
            {
                return this._b5;
            }
        }

        public MControl.Printing.Pdf.Page Page
        {
            get
            {
                return this._b1;
            }
        }

        public string Title
        {
            get
            {
                return this._b0;
            }
        }

        public float Top
        {
            get
            {
                return this._b3;
            }
        }

        public float Zoom
        {
            get
            {
                return this._b4;
            }
        }
    }
}

