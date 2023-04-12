namespace Nistec.Printing.View
{
    using System;
    using System.Drawing;

    public class FindInfo
    {
        private string _var0;
        private bool _var1 = false;
        private bool _var2 = false;
        private int _var3 = 1;
        internal Page mtd201;
        internal mtd202 mtd203 = new mtd202();
        internal mtd198 mtd204;
        internal RectangleF mtd57 = new RectangleF();

        internal void mtd206(RectangleF var4)
        {
            this.mtd57.X = var4.X - 5f;
            this.mtd57.Y = var4.Y - 5f;
            this.mtd57.Width = var4.Width + 10f;
            this.mtd57.Height = var4.Height + 10f;
        }

        public void Reset()
        {
            this.mtd203.mtd205 = null;
            this.mtd203.mtd200 = -1;
            this.mtd204 = null;
            this.mtd57.X = 0f;
            this.mtd57.Y = 0f;
            this.mtd57.Width = 0f;
            this.mtd57.Height = 0f;
        }

        public Page CurrentPage
        {
            get
            {
                return this.mtd201;
            }
            set
            {
                this.Reset();
                this.mtd201 = value;
            }
        }

        public bool IsMatchCase
        {
            get
            {
                return this._var2;
            }
            set
            {
                this._var2 = value;
            }
        }

        public bool IsMatchWhole
        {
            get
            {
                return this._var1;
            }
            set
            {
                this._var1 = value;
            }
        }

        public bool IsUp
        {
            set
            {
                if (value)
                {
                    this._var3 = 1;
                }
                else
                {
                    this._var3 = -1;
                }
            }
        }

        public RectangleF PointerBound
        {
            get
            {
                return this.mtd57;
            }
        }

        public int Step
        {
            get
            {
                return this._var3;
            }
            set
            {
                this._var3 = value;
            }
        }

        public string Text
        {
            get
            {
                return this._var0;
            }
            set
            {
                this._var0 = value;
            }
        }
    }
}

