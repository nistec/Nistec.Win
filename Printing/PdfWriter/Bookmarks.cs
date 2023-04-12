namespace MControl.Printing.Pdf
{
    using System;
    using System.Reflection;

    public class Bookmarks
    {
        private Bookmark[] _b0;
        private int _b1 = 0;
        private Bookmark _b2;

        internal Bookmarks(Bookmark b2)
        {
            this._b2 = b2;
            this._b0 = new Bookmark[10];
        }

        public void Add(Bookmark bookmark)
        {
            this.b3();
            bookmark.A209 = this._b2;
            this._b0[this._b1] = bookmark;
            this._b1++;
        }

        public int IndexOf(Bookmark bookmark)
        {
            for (int i = 0; i < this._b1; i++)
            {
                if (bookmark == this._b0[i])
                {
                    return i;
                }
            }
            return -1;
        }

        public void Remove(Bookmark bookmark)
        {
            int index = this.IndexOf(bookmark);
            if (index != -1)
            {
                this.RemoveAt(index);
            }
        }

        public void RemoveAt(int index)
        {
            if ((index < 0) || (index >= this._b1))
            {
                throw new ArgumentOutOfRangeException("index", "ArgumentOutOfRange");
            }
            this._b1--;
            this._b0[index].A209 = null;
            if (index < this._b1)
            {
                Array.Copy(this._b0, index + 1, this._b0, index, this._b1 - index);
            }
            this._b0[this._b1] = null;
        }

        private void b3()
        {
            if (this._b1 >= this._b0.Length)
            {
                Bookmark[] sourceArray = this._b0;
                this._b0 = new Bookmark[2 * this._b0.Length];
                Array.Copy(sourceArray, this._b0, this._b1);
            }
        }

        public int Count
        {
            get
            {
                return this._b1;
            }
        }

        public Bookmark this[int index]
        {
            get
            {
                return this._b0[index];
            }
        }
    }
}

