namespace MControl.Printing.Pdf.Drawing
{
    using System;
    using System.Reflection;

    public class SubPathList
    {
        private SubPath[] _b0 = new SubPath[10];
        private int _b1 = 0;

        internal SubPathList()
        {
        }

        public void Add(SubPath subpath)
        {
            this.b2();
            this._b0[this._b1] = subpath;
            this._b1++;
        }

        public int IndexOf(SubPath subpath)
        {
            for (int i = 0; i < this._b1; i++)
            {
                if (subpath == this._b0[i])
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(SubPath subpath, int index)
        {
            if ((index < 0) || (index > this._b1))
            {
                throw new ArgumentOutOfRangeException("index", "ArgumentOutOfRange");
            }
            this.b2();
            if (index < this._b1)
            {
                Array.Copy(this._b0, index, this._b0, index + 1, this._b1 - index);
            }
            this._b0[index] = subpath;
            this._b1++;
        }

        public void Remove(SubPath subpath)
        {
            int index = this.IndexOf(subpath);
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
            if (index < this._b1)
            {
                Array.Copy(this._b0, index + 1, this._b0, index, this._b1 - index);
            }
            this._b0[this._b1] = null;
        }

        private void b2()
        {
            if (this._b1 >= this._b0.Length)
            {
                SubPath[] destinationArray = new SubPath[2 * this._b0.Length];
                Array.Copy(this._b0, destinationArray, this._b1);
                this._b0 = destinationArray;
            }
        }

        public int Count
        {
            get
            {
                return this._b1;
            }
        }

        public SubPath this[int index]
        {
            get
            {
                return this._b0[index];
            }
        }
    }
}

