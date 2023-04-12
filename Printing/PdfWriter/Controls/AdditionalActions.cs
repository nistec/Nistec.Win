namespace MControl.Printing.Pdf.Controls
{
    using System;
    using System.Reflection;

    public class AdditionalActions
    {
        private AdditionalAction[] _b0 = new AdditionalAction[10];
        private int _b1 = 0;

        internal AdditionalActions()
        {
        }

        public void Add(AdditionalAction action)
        {
            this.b2();
            this._b0[this._b1] = action;
            this._b1++;
        }

        public int IndexOf(AdditionalAction action)
        {
            for (int i = 0; i < this._b1; i++)
            {
                if (action == this._b0[i])
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(AdditionalAction action, int index)
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
            this._b0[index] = action;
            this._b1++;
        }

        public void Remove(AdditionalAction action)
        {
            int index = this.IndexOf(action);
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
                AdditionalAction[] destinationArray = new AdditionalAction[2 * this._b0.Length];
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

        public AdditionalAction this[int index]
        {
            get
            {
                return this._b0[index];
            }
        }
    }
}

