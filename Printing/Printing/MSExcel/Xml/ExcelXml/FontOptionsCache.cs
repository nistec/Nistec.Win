namespace Nistec.Printing.ExcelXml
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class FontOptionsCache : CellSettings, IFontOptions
    {
        [CompilerGenerated]
        private static CellSettings.StylePropertyCache __CachedNameDelegate;
        [CompilerGenerated]
        private static CellSettings.StylePropertyCache __CachedUnderlineDelegate;
        [CompilerGenerated]
        private static CellSettings.StylePropertyCache __CachedItalicDelegate;
        [CompilerGenerated]
        private static CellSettings.StylePropertyCache __CachedStrikeoutDelegate;
        [CompilerGenerated]
        private static CellSettings.StylePropertyCache __CachedColorDelegate;
        [CompilerGenerated]
        private static CellSettings.StylePropertyCache __CachedSizeDelegate;
        [CompilerGenerated]
        private static CellSettings.StylePropertyCache __CachedBoldDelegate;

        internal FontOptionsCache(Styles parent) : base(parent)
        {
        }

        public bool Bold
        {
            get
            {
                if (__CachedBoldDelegate == null)
                {
                    __CachedBoldDelegate = delegate (IStyle style) {
                        return style.Font.Bold;
                    };
                }
                return (bool) base.GetCellStyleProperty(__CachedBoldDelegate);
            }
            set
            {
                base.SetCellStyleProperty(delegate (IStyle style) {
                    return style.Font.Bold = value;
                });
            }
        }

        public System.Drawing.Color Color
        {
            get
            {
                if (__CachedColorDelegate == null)
                {
                    __CachedColorDelegate = delegate (IStyle style) {
                        return style.Font.Color;
                    };
                }
                return (System.Drawing.Color) base.GetCellStyleProperty(__CachedColorDelegate);
            }
            set
            {
                base.SetCellStyleProperty(delegate (IStyle style) {
                    return style.Font.Color = value;
                });
            }
        }

        public bool Italic
        {
            get
            {
                if (__CachedItalicDelegate == null)
                {
                    __CachedItalicDelegate = delegate (IStyle style) {
                        return style.Font.Italic;
                    };
                }
                return (bool) base.GetCellStyleProperty(__CachedItalicDelegate);
            }
            set
            {
                base.SetCellStyleProperty(delegate (IStyle style) {
                    return style.Font.Italic = value;
                });
            }
        }

        public string Name
        {
            get
            {
                if (__CachedNameDelegate == null)
                {
                    __CachedNameDelegate = delegate (IStyle style) {
                        return style.Font.Name;
                    };
                }
                return (string) base.GetCellStyleProperty(__CachedNameDelegate);
            }
            set
            {
                base.SetCellStyleProperty(delegate (IStyle style) {
                    return style.Font.Name = value;
                });
            }
        }

        public int Size
        {
            get
            {
                if (__CachedSizeDelegate == null)
                {
                    __CachedSizeDelegate = delegate (IStyle style) {
                        return style.Font.Size;
                    };
                }
                return (int) base.GetCellStyleProperty(__CachedSizeDelegate);
            }
            set
            {
                base.SetCellStyleProperty(delegate (IStyle style) {
                    return style.Font.Size = value;
                });
            }
        }

        public bool Strikeout
        {
            get
            {
                if (__CachedStrikeoutDelegate == null)
                {
                    __CachedStrikeoutDelegate = delegate (IStyle style) {
                        return style.Font.Strikeout;
                    };
                }
                return (bool) base.GetCellStyleProperty(__CachedStrikeoutDelegate);
            }
            set
            {
                base.SetCellStyleProperty(delegate (IStyle style) {
                    return style.Font.Strikeout = value;
                });
            }
        }

        public bool Underline
        {
            get
            {
                if (__CachedUnderlineDelegate == null)
                {
                    __CachedUnderlineDelegate = delegate (IStyle style) {
                        return style.Font.Underline;
                    };
                }
                return (bool) base.GetCellStyleProperty(__CachedUnderlineDelegate);
            }
            set
            {
                base.SetCellStyleProperty(delegate (IStyle style) {
                    return style.Font.Underline = value;
                });
            }
        }
    }
}

