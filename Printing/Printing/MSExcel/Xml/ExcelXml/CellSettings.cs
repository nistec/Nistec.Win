namespace Nistec.Printing.ExcelXml
{
    using System;
    using System.Runtime.CompilerServices;

    public abstract class CellSettings
    {
        internal Styles Parent;

        internal CellSettings()
        {
        }

        internal CellSettings(Styles parent)
        {
            if (parent == null)
            {
                throw new ArgumentNullException("parent");
            }
            this.Parent = parent;
        }

        internal object GetCellStyleProperty(StylePropertyCache getDelegate)
        {
            if (this.GetParentBook() == null)
            {
                return getDelegate(this.Parent.FirstCell());
            }
            XmlStyle styleByID = this.GetParentBook().GetStyleByID(this.Parent.StyleID);
            return getDelegate(styleByID);
        }

        internal virtual Workbook GetParentBook()
        {
            return this.Parent.GetParentBook();
        }

        internal void SetCellStyleProperty(StylePropertyCache setDelegate)
        {
            Styles.IterateFunction ifFunc = null;
            if (this.GetParentBook() == null)
            {
                if (ifFunc == null)
                {
                    ifFunc = delegate (Cell cell) {
                        setDelegate(cell);
                    };
                }
                this.Parent.IterateAndApply(ifFunc);
            }
            else
            {
                XmlStyle styles = new XmlStyle(this.GetParentBook().GetStyleByID(this.Parent.StyleID));
                setDelegate(styles);
                this.Parent.StyleID = this.GetParentBook().AddStyle(styles);
            }
        }

        internal delegate object StylePropertyCache(IStyle styles);
    }
}

