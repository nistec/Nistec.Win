namespace Nistec.Printing.ExcelXml
{
    using System;
    using System.Runtime.CompilerServices;

    public class AlignmentOptionsCache : CellSettings, IAlignmentOptions
    {
        [CompilerGenerated]
        private static CellSettings.StylePropertyCache __CachedVerticalAlignmentDelegate;
        [CompilerGenerated]
        private static CellSettings.StylePropertyCache __CachedIndentDelegate;
        [CompilerGenerated]
        private static CellSettings.StylePropertyCache __CachedRotateDelegate;
        [CompilerGenerated]
        private static CellSettings.StylePropertyCache __CachedShrinkToFitDelegate;
        [CompilerGenerated]
        private static CellSettings.StylePropertyCache __CachedHorizontalAlignmentDelegate;
        [CompilerGenerated]
        private static CellSettings.StylePropertyCache __CachedWrapTextDelegate;

        internal AlignmentOptionsCache(Styles parent) : base(parent)
        {
        }

        public HorizontalAlignment Horizontal
        {
            get
            {
                if (__CachedHorizontalAlignmentDelegate == null)
                {
                    __CachedHorizontalAlignmentDelegate = delegate (IStyle style) {
                        return style.Alignment.Horizontal;
                    };
                }
                return (HorizontalAlignment) base.GetCellStyleProperty(__CachedHorizontalAlignmentDelegate);
            }
            set
            {
                base.SetCellStyleProperty(delegate (IStyle style) {
                    return style.Alignment.Horizontal = value;
                });
            }
        }

        public int Indent
        {
            get
            {
                if (__CachedIndentDelegate == null)
                {
                    __CachedIndentDelegate = delegate (IStyle style) {
                        return style.Alignment.Indent;
                    };
                }
                return (int) base.GetCellStyleProperty(__CachedIndentDelegate);
            }
            set
            {
                base.SetCellStyleProperty(delegate (IStyle style) {
                    return style.Alignment.Indent = value;
                });
            }
        }

        public int Rotate
        {
            get
            {
                if (__CachedRotateDelegate == null)
                {
                    __CachedRotateDelegate = delegate (IStyle style) {
                        return style.Alignment.Rotate;
                    };
                }
                return (int) base.GetCellStyleProperty(__CachedRotateDelegate);
            }
            set
            {
                base.SetCellStyleProperty(delegate (IStyle style) {
                    return style.Alignment.Rotate = value;
                });
            }
        }

        public bool ShrinkToFit
        {
            get
            {
                if (__CachedShrinkToFitDelegate == null)
                {
                    __CachedShrinkToFitDelegate = delegate (IStyle style) {
                        return style.Alignment.ShrinkToFit;
                    };
                }
                return (bool) base.GetCellStyleProperty(__CachedShrinkToFitDelegate);
            }
            set
            {
                base.SetCellStyleProperty(delegate (IStyle style) {
                    return style.Alignment.ShrinkToFit = value;
                });
            }
        }

        public VerticalAlignment Vertical
        {
            get
            {
                if (__CachedVerticalAlignmentDelegate == null)
                {
                    __CachedVerticalAlignmentDelegate = delegate (IStyle style) {
                        return style.Alignment.Vertical;
                    };
                }
                return (VerticalAlignment) base.GetCellStyleProperty(__CachedVerticalAlignmentDelegate);
            }
            set
            {
                base.SetCellStyleProperty(delegate (IStyle style) {
                    return style.Alignment.Vertical = value;
                });
            }
        }

        public bool WrapText
        {
            get
            {
                if (__CachedWrapTextDelegate == null)
                {
                    __CachedWrapTextDelegate = delegate (IStyle style) {
                        return style.Alignment.WrapText;
                    };
                }
                return (bool) base.GetCellStyleProperty(__CachedWrapTextDelegate);
            }
            set
            {
                base.SetCellStyleProperty(delegate (IStyle style) {
                    return style.Alignment.WrapText = value;
                });
            }
        }
    }
}

