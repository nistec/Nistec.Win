namespace Nistec.Printing.View
{
    using System;

    public class Margins
    {
        private float _var0;
        private float _var1;
        private float _var2;
        private float _var3;

        public Margins()
        {
            this._var0 = 1f;
            this._var1 = 1f;
            this._var2 = 1f;
            this._var3 = 1f;
        }

        public Margins(float left, float top, float right, float bottom)
        {
            this._var0 = left;
            this._var1 = top;
            this._var2 = right;
            this._var3 = bottom;
        }

        public bool ShouldSerializeMarginBottom()
        {
            return (this.MarginBottom != 1f);
        }

        public bool ShouldSerializeMarginLeft()
        {
            return (this.MarginLeft != 1f);
        }

        public bool ShouldSerializeMarginRight()
        {
            return (this.MarginRight != 1f);
        }

        public bool ShouldSerializeMarginTop()
        {
            return (this.MarginTop != 1f);
        }

        public float MarginBottom
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

        public float MarginLeft
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

        public float MarginRight
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

        public float MarginTop
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
    }
}

