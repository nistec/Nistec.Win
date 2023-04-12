namespace Nistec.Printing.View
{
    using System;

    public class McField
    {
        private string _var0;
        private object _var1;
        private int _var2;
        private TypeCode _var3;
        private bool _var4;
        private mtd102 _var5;
        private object _var6;
        private object _var7;
        private bool _var8;
        private bool _var9;

        public McField(string name, TypeCode typecode)
        {
            this._var0 = name;
            this._var2 = -1;
            this._var3 = typecode;
            this._var6 = null;
            this._var7 = null;
            this._var8 = false;
            this._var9 = false;
        }

        internal McField(string var0, int var10, TypeCode var11, bool var12)
        {
            this._var0 = var0;
            this._var2 = var10;
            this._var3 = var11;
            this._var6 = null;
            this._var7 = null;
            this._var8 = false;
            this._var9 = var12;
        }

        internal void mtd109()
        {
            this._var8 = true;
            this._var7 = this._var6;
        }

        internal object mtd110(AggregateType var13)
        {
            if (this._var4)
            {
                return this._var5.mtd110(0, this._var5.mtd32, var13);
            }
            return null;
        }

        internal object mtd110(int var2, int var14, AggregateType var13)
        {
            if (this._var4)
            {
                return this._var5.mtd110(var2, var14, var13);
            }
            return null;
        }

        internal void mtd111()
        {
            this._var1 = null;
            this._var8 = false;
            if (this._var4)
            {
                this._var5.mtd111();
            }
        }

        internal bool mtd104
        {
            get
            {
                return this._var4;
            }
            set
            {
                this._var4 = value;
                if (this._var4 && (this._var5 == null))
                {
                    this._var5 = mtd102.mtd105(this._var3);
                }
            }
        }

        internal object mtd106
        {
            get
            {
                return this._var6;
            }
            set
            {
                this._var6 = value;
                this.Value = value;
            }
        }

        internal object mtd107
        {
            get
            {
                return this._var7;
            }
        }

        internal bool mtd108
        {
            get
            {
                return this._var9;
            }
        }

        public int ColIndex
        {
            get
            {
                return this._var2;
            }
        }

        public string Name
        {
            get
            {
                return this._var0;
            }
        }

        public TypeCode Typecode
        {
            get
            {
                return this._var3;
            }
        }

        public object Value
        {
            get
            {
                return this._var1;
            }
            set
            {
                this._var1 = value;
                if (this._var4)
                {
                    if (this._var8)
                    {
                        this._var5.mtd2(this._var1);
                    }
                    else
                    {
                        this._var5.mtd103(this._var1);
                    }
                }
                if (!this._var9)
                {
                    this._var6 = value;
                }
                this._var8 = false;
            }
        }
    }
}

