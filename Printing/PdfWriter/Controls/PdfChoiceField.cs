namespace MControl.Printing.Pdf.Controls
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core.Controls;
    using System;
    using System.Drawing;

    public abstract class PdfChoiceField : PdfField
    {
        internal PdfFont _A171;
        internal float _A172;
        private string[] _b0;

        internal PdfChoiceField(Page page, string name, string[] items, RectangleF bound, PdfFont font, float fontsize) : base(page)
        {
            base._A154 = name;
            this._b0 = items;
            this.Bounds = bound;
            this._A171 = font;
            this._A172 = fontsize;
        }

        internal override string A155
        {
            get
            {
                return "Ch";
            }
        }

        public bool CommitOnSelChange
        {
            get
            {
                return ((base.A153 & A127.CommitOnSelChange) != A127.Default);
            }
            set
            {
                if (value)
                {
                    base.A153 |= A127.CommitOnSelChange;
                }
                else
                {
                    base.A153 &= ~A127.CommitOnSelChange;
                }
            }
        }

        public PdfFont Font
        {
            get
            {
                return this._A171;
            }
            set
            {
                this._A171 = value;
            }
        }

        public float FontSize
        {
            get
            {
                return this._A172;
            }
            set
            {
                this._A172 = value;
            }
        }

        public string[] Items
        {
            get
            {
                return this._b0;
            }
            set
            {
                if (value != null)
                {
                    this._b0 = value.Clone() as string[];
                }
            }
        }

        public bool MultiSelect
        {
            get
            {
                return ((base.A153 & A127.MultiSelect) != A127.Default);
            }
            set
            {
                if (value)
                {
                    base.A153 |= A127.MultiSelect;
                }
                else
                {
                    base.A153 &= ~A127.MultiSelect;
                }
            }
        }

        public bool Sorted
        {
            get
            {
                return ((base.A153 & A127.Sort) != A127.Default);
            }
            set
            {
                if (value)
                {
                    base.A153 |= A127.Sort;
                }
                else
                {
                    base.A153 &= ~A127.Sort;
                }
            }
        }

        public bool SpellCheck
        {
            get
            {
                return ((base.A153 & A127.DoNotSpellCheck) == A127.Default);
            }
            set
            {
                if (!value)
                {
                    base.A153 |= A127.DoNotSpellCheck;
                }
                else
                {
                    base.A153 &= ~A127.DoNotSpellCheck;
                }
            }
        }
    }
}

