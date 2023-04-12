namespace MControl.Printing.Pdf.Drawing
{
    using System;
    using System.Reflection;

    public class GraphicsElements
    {
        private GraphicsElement[] _b0 = new GraphicsElement[50];
        private int _b1 = 0;

        internal GraphicsElements()
        {
        }

        public void Add(GraphicsElement element)
        {
            this.b2();
            this._b0[this._b1] = element;
            this._b1++;
        }

        public int IndexOf(GraphicsElement element)
        {
            for (int i = 0; i < this._b1; i++)
            {
                if (element == this._b0[i])
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(GraphicsElement element, int index)
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
            this._b0[index] = element;
            this._b1++;
        }

        public void Remove(GraphicsElement element)
        {
            int index = this.IndexOf(element);
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
                GraphicsElement[] destinationArray = new GraphicsElement[2 * this._b0.Length];
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

        public GraphicsElement this[int index]
        {
            get
            {
                return this._b0[index];
            }
        }

        public GraphicsElement this[string name]
        {
            get
            {
                GraphicsElement element = null;
                if ((name != null) && (name.Length > 0))
                {
                    for (int i = 0; i < this._b1; i++)
                    {
                        element = this._b0[i];
                        if (string.Compare(element.Name, name, true) == 0)
                        {
                            return element;
                        }
                    }
                }
                return element;
            }
        }
    }
}

