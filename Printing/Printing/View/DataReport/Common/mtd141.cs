namespace Nistec.Printing.View
{
    using System;
    using System.Drawing;

    //public
    internal class mtd141
    {
        private mtd163 _var0;
        private mtd164 _var1;

        internal void mtd142(mtd163 var2)
        {
            this._var0 = var2;
            this._var1 = var2.mtd167;
        }

        public bool mtd168(PropDoc var3, int var4)
        {
            if ((var4 > -1) && (var4 < this.mtd166))
            {
                mtd126 mtd = this._var1[var4];
                if (mtd.ControlType == ControlType.TextBox)
                {
                    var3.mtd142((mtd158) mtd);
                    return true;
                }
                if (mtd.ControlType == ControlType.Label)
                {
                    var3.mtd142((mtd148) mtd);
                    return true;
                }
                if (mtd.ControlType == ControlType.Picture)
                {
                    var3.mtd142((mtd152) mtd);
                    return true;
                }
                if (mtd.ControlType == ControlType.Line)
                {
                    var3.mtd142((mtd149) mtd);
                    return true;
                }
                if (mtd.ControlType == ControlType.RichTextField)
                {
                    var3.mtd142((mtd155) mtd);
                    return true;
                }
                if (mtd.ControlType == ControlType.CheckBox)
                {
                    var3.mtd142((mtd136) mtd);
                    return true;
                }
                if (mtd.ControlType == ControlType.Shape)
                {
                    var3.mtd142((mtd156) mtd);
                    return true;
                }
                if (mtd.ControlType == ControlType.SubReport)
                {
                    var3.mtd142((mtd161) mtd);
                    return true;
                }
            }
            return false;
        }

        public SectionType SectionType//mtd165
        {
            get
            {
                return this._var0._SectionType;
            }
        }

        public int mtd166
        {
            get
            {
                return this._var1.mtd166;
            }
        }

        public float mtd29
        {
            get
            {
                return this._var0.mtd29;
            }
        }

        public float Height//mtd31
        {
            get
            {
                return this._var0.Height;
            }
        }

        public Color BackColor//mtd39
        {
            get
            {
                return this._var0.BackColor;
            }
        }

        public bool mtd86
        {
            get
            {
                return this._var0.mtd86;
            }
        }
    }
}

