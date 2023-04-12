namespace Nistec.Printing.ExcelXml
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class BorderOptionsCache : CellSettings, IBorderOptions
    {
        [CompilerGenerated]
        private static CellSettings.StylePropertyCache __CachedBorderSidesDelegate;
        [CompilerGenerated]
        private static CellSettings.StylePropertyCache __CachedBorderlineDelegate;
        [CompilerGenerated]
        private static CellSettings.StylePropertyCache __CachedColorDelegate;
        [CompilerGenerated]
        private static CellSettings.StylePropertyCache __CachedWeightDelegate;

        internal BorderOptionsCache(Styles parent) : base(parent)
        {
        }

        public System.Drawing.Color Color
        {
            get
            {
                if (__CachedColorDelegate == null)
                {
                    __CachedColorDelegate = delegate (IStyle style) {
                        return style.Border.Color;
                    };
                }
                return (System.Drawing.Color) base.GetCellStyleProperty(__CachedColorDelegate);
            }
            set
            {
                base.SetCellStyleProperty(delegate (IStyle style) {
                    return style.Border.Color = value;
                });
            }
        }

        public Borderline LineStyle
        {
            get
            {
                if (__CachedBorderlineDelegate == null)
                {
                    __CachedBorderlineDelegate = delegate (IStyle style) {
                        return style.Border.LineStyle;
                    };
                }
                return (Borderline) base.GetCellStyleProperty(__CachedBorderlineDelegate);
            }
            set
            {
                base.SetCellStyleProperty(delegate (IStyle style) {
                    return style.Border.LineStyle = value;
                });
            }
        }

        public BorderSides Sides
        {
            get
            {
                if (__CachedBorderSidesDelegate == null)
                {
                    __CachedBorderSidesDelegate = delegate (IStyle style) {
                        return style.Border.Sides;
                    };
                }
                return (BorderSides) base.GetCellStyleProperty(__CachedBorderSidesDelegate);
            }
            set
            {
                base.SetCellStyleProperty(delegate (IStyle style) {
                    return style.Border.Sides = value;
                });
            }
        }

        public int Weight
        {
            get
            {
                if (__CachedWeightDelegate == null)
                {
                    __CachedWeightDelegate = delegate (IStyle style) {
                        return style.Border.Weight;
                    };
                }
                return (int) base.GetCellStyleProperty(__CachedWeightDelegate);
            }
            set
            {
                base.SetCellStyleProperty(delegate (IStyle style) {
                    return style.Border.Weight = value;
                });
            }
        }
    }
}

