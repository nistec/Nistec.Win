namespace Nistec.Printing.ExcelXml
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class InteriorOptionsCache : CellSettings, IInteriorOptions
    {
        [CompilerGenerated]
        private static CellSettings.StylePropertyCache __CachedColorDelegate;

        internal InteriorOptionsCache(Styles parent) : base(parent)
        {
        }

        public System.Drawing.Color Color
        {
            get
            {
                if (__CachedColorDelegate == null)
                {
                    __CachedColorDelegate = delegate (IStyle style) {
                        return style.Interior.Color;
                    };
                }
                return (System.Drawing.Color) base.GetCellStyleProperty(__CachedColorDelegate);
            }
            set
            {
                base.SetCellStyleProperty(delegate (IStyle style) {
                    return style.Interior.Color = value;
                });
            }
        }
    }
}

