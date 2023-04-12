namespace Nistec.Printing.ExcelXml
{
    using System;
    using System.Runtime.CompilerServices;

    public abstract class Styles : CellSettings, IStyle
    {
        private AlignmentOptionsCache __Alignment;
        private FontOptionsCache __Font;
        private InteriorOptionsCache __Interior;
        [CompilerGenerated]
        private BorderOptionsCache __Border;
        [CompilerGenerated]
        private string _StyleID;
        [CompilerGenerated]
        private static CellSettings.StylePropertyCache __CachedFormatDelegate;

        internal Styles()
        {
            this.StyleID = "";
            this.__Font = new FontOptionsCache(this);
            this.__Alignment = new AlignmentOptionsCache(this);
            this.__Interior = new InteriorOptionsCache(this);
            this._Border = new BorderOptionsCache(this);
            base.Parent = this;
        }

        internal abstract Cell FirstCell();
        internal bool HasDefaultStyle()
        {
            return (this.StyleID == "Default");
        }

        internal abstract void IterateAndApply(IterateFunction ifFunc);

        private BorderOptionsCache _Border
        {
            [CompilerGenerated]
            get
            {
                return this.__Border;
            }
            [CompilerGenerated]
            set
            {
                this.__Border = value;
            }
        }

        public IAlignmentOptions Alignment
        {
            get
            {
                return this.__Alignment;
            }
            set
            {
                this.__Alignment = (AlignmentOptionsCache)value;
            }
        }

        public IBorderOptions Border
        {
            get
            {
                return this._Border;
            }
            set
            {
                this._Border = (BorderOptionsCache) value;
            }
        }

        public DisplayFormat DisplayFormat
        {
            get
            {
                if (__CachedFormatDelegate == null)
                {
                    __CachedFormatDelegate = delegate (IStyle style) {
                        return style.DisplayFormat;
                    };
                }
                return (DisplayFormat) base.GetCellStyleProperty(__CachedFormatDelegate);
            }
            set
            {
                base.SetCellStyleProperty(delegate (IStyle style) {
                    return style.DisplayFormat = value;
                });
            }
        }

        public IFontOptions Font
        {
            get
            {
                return this.__Font;
            }
            set
            {
                this.__Font = (FontOptionsCache)value;
            }
        }

        public IInteriorOptions Interior
        {
            get
            {
                return this.__Interior;
            }
            set
            {
                this.__Interior = (InteriorOptionsCache)value;
            }
        }

        public XmlStyle Style
        {
            get
            {
                if (this.GetParentBook() == null)
                {
                    return this.FirstCell().GetParentBook().GetStyleByID(this.StyleID);
                }
                return this.GetParentBook().GetStyleByID(this.StyleID);
            }
            set
            {
                IterateFunction ifFunc = null;
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                if (this.GetParentBook() == null)
                {
                    if (ifFunc == null)
                    {
                        ifFunc = delegate (Cell cell) {
                            cell.Style = value;
                        };
                    }
                    this.IterateAndApply(ifFunc);
                }
                else
                {
                    this.StyleID = this.GetParentBook().AddStyle(value);
                }
            }
        }

        internal string StyleID
        {
            [CompilerGenerated]
            get
            {
                return this._StyleID;
            }
            [CompilerGenerated]
            set
            {
                this._StyleID = value;
            }
        }

        internal delegate void IterateFunction(Cell cell);
    }
}

