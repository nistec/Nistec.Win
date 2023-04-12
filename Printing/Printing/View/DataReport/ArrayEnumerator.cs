namespace Nistec.Printing.View
{
    using System;
    using System.Collections;

    public class ArrayEnumerator : IEnumerator
    {
        private int _var0;
        private int _var1;
        private Array _var2;

        public ArrayEnumerator(Array var2)
        {
            this._var2 = var2;
            this._var0 = -1;
            this._var1 = var2.Length;
        }

        public ArrayEnumerator(Array var2, int var3)
        {
            this._var2 = var2;
            this._var0 = -1;
            this._var1 = var3;
        }

        public bool MoveNext()
        {
            if (this._var0 < this._var1)
            {
                this._var0++;
                return (this._var0 < this._var1);
            }
            return false;
        }

        public void Reset()
        {
            this._var0 = -1;
        }

        public object Current
        {
            get
            {
                if (this._var0 < 0)
                {
                    throw new InvalidOperationException("InvalidOperation_EnumNotStarted");
                }
                if (this._var0 >= this._var1)
                {
                    throw new InvalidOperationException("InvalidOperation_EnumEnded");
                }
                return this._var2.GetValue(this._var0);
            }
        }
    }
}

