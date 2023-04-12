namespace MControl.Printing.Pdf.Drawing
{
    using MControl.Printing.Pdf;
    using MControl.Printing.Pdf.Core;
    using MControl.Printing.Pdf.Core.Drawing;
    using MControl.Printing.Pdf.Core.IO;
    using MControl.Printing.Pdf.Core.Text;
    using System;
    using System.ComponentModel;
    using System.Drawing;

    public class Table : GraphicsElement
    {
        private float _b0;
        private float _b1;
        private float _b10;
        private TableStyle _b11;
        private bool _b12;
        private int _b13;
        private int _b14;
        private float _b2;
        private float _b3;
        private float _b4;
        private bool _b5;
        private int _b6;
        private MControl.Printing.Pdf.Drawing.Rows _b7;
        private MControl.Printing.Pdf.Drawing.Columns _b8;
        private bool _b9;

        public Table()
        {
            this._b11 = new TableStyle(this);
            this.b15();
        }

        public Table(TableStyle style)
        {
            this._b11 = new TableStyle(style, this);
            this.b15();
        }

        internal override void A119(ref A120 b19, ref A112 b20)
        {
            A468(ref b19, ref b20, this, this._b0, this._b1);
        }

        internal static float A434(Table b16, bool b18)
        {
            float num = 0f;
            TableStyle style = null;
            Cell cell = null;
            Cells cells = null;
            A477 A = null;
            Row row = null;
            MControl.Printing.Pdf.Drawing.Rows rows = b16.Rows;
            float cellPadding = b16.CellPadding;
            A497 A2 = new A497();
            for (int i = 0; i < rows.Count; i++)
            {
                row = rows[i];
                cells = row.Cells;
                for (int j = 0; j < cells.Count; j++)
                {
                    cell = cells[j];
                    A = cell.A481();
                    style = cell.A405;
                    if (A != null)
                    {
                        if (A.A47 == A47.A38)
                        {
                            A2.A3(new A498(A.A38, style));
                        }
                        else if (A.A47 == A47.A48)
                        {
                            A2.A3(new A499(A.A38, style, A.A48));
                        }
                        if (A2.A2 > 0)
                        {
                            A500 A3 = new A500(A2, 0f, 0f, style.LineSpace, 0f, style.TextAlign, style.RightToLeft);
                            A3.A501(cell.Width - (2f * cellPadding), b16.A405, b18);
                            float num5 = (A3.A502 - style.LineSpace) + (2f * cellPadding);
                            if (num5 > row.Height)
                            {
                                row.Height = num5;
                            }
                            if (!b18)
                            {
                                cell.A479 = A3.A479;
                            }
                        }
                        A2.A4();
                    }
                }
                num += row.Height;
            }
            return num;
        }

        internal static Table A468(float b0, float b1, float b17, Table b16)
        {
            if (!b16._b12)
            {
                b16._b6 = (b16._b6 > (b16._b7.Count - 1)) ? (b16._b7.Count - 1) : b16._b6;
                float num = 0f;
                for (int i = 0; i < b16._b8.Count; i++)
                {
                    num += b16._b8[i].Width;
                }
                b16._b3 = num;
                b16._b2 = A434(b16, false);
                b16._b9 = true;
                b16._b12 = true;
            }
            b16._b0 = b0;
            b16._b1 = b1;
            if (b16._b2 > b17)
            {
                Row row = null;
                MControl.Printing.Pdf.Drawing.Rows rows = b16.Rows;
                float num3 = b16._b4;
                float num4 = 0f;
                float num5 = b17 - b16._b4;
                if ((b16._b13 > (b16._b6 - 1)) && b16._b5)
                {
                    int num6 = b16._b6;
                    for (int k = 0; k < num6; k++)
                    {
                        num3 += rows[k].Height;
                    }
                }
                bool flag = true;
                for (int j = b16._b13; j < rows.Count; j++)
                {
                    row = rows[j];
                    if (flag)
                    {
                        num4 = b16.A495;
                        flag = false;
                    }
                    if (((num3 - num4) + row.Height) > num5)
                    {
                        if (b16.RepeatHeaderRow)
                        {
                            if ((j > 1) && !row.KeepTogether)
                            {
                                b16._b14 = j + 1;
                                return A496(b16, j, b17, b17 - num3);
                            }
                            b16._b14 = j;
                            return A496(b16, j, b17, 0f);
                        }
                        if ((j > 0) && row.KeepTogether)
                        {
                            b16._b14 = j;
                            return A496(b16, j, b17, 0f);
                        }
                        b16._b14 = j + 1;
                        return A496(b16, j, b17, b17 - num3);
                    }
                    num3 += row.Height - num4;
                    num4 = 0f;
                }
            }
            b16._b14 = b16._b7.Count;
            return null;
        }

        internal static void A468(ref A120 b19, ref A112 b20, Table b16, float b0, float b1)
        {
            MControl.Printing.Pdf.Drawing.Rows rows = b16.Rows;
            Row row = null;
            float num = b1;
            MControl.Printing.Pdf.Border border = null;
            GraphicsElement.A435(ref b19, ref b20);
            GraphicsElement.A446(b20, b0, b19.A98(b1), b16.Width, b16.Height);
            if ((b16._b5 && (b16._b13 > (b16._b6 - 1))) && (b16._b14 > 0))
            {
                int num2 = b16._b6;
                for (int j = 0; j < num2; j++)
                {
                    row = rows[j];
                    b21(ref b19, ref b20, row, b0, num, 0f);
                    num += row.Height;
                }
            }
            float num4 = b16.A495;
            for (int i = b16._b13; i < b16._b14; i++)
            {
                row = rows[i];
                b21(ref b19, ref b20, row, b0, num, num4);
                num += row.Height - num4;
                num4 = 0f;
            }
            border = b16.Border;
            if (border != null)
            {
                GraphicsElement.A156(b0, b1, b16.Width, num - b1, border, ref b19, ref b20);
            }
            GraphicsElement.A438(ref b19, ref b20);
        }

        internal static Table A496(Table b16, int b13, float b17, float b10)
        {
            Table table = new Table();
            MControl.Printing.Pdf.Drawing.Rows rows = b16._b7;
            table._b4 = b16._b4;
            table._b12 = b16._b12;
            table._b8 = b16._b8;
            table._b10 = b10;
            table._b9 = b16._b9;
            table._b5 = b16._b5;
            table._b6 = b16._b6;
            table._b11 = b16._b11;
            table._b3 = b16._b3;
            table._b14 = 0;
            table._b13 = b13;
            table._b7 = rows;
            b16._b2 = b17;
            float num = 0f;
            if (((b13 > (table._b6 - 1)) && table._b5) && (rows.Count > 0))
            {
                int num2 = table._b6;
                for (int j = 0; j < num2; j++)
                {
                    num += rows[j].Height;
                }
            }
            for (int i = b13; i < rows.Count; i++)
            {
                num += rows[i].Height;
            }
            table._b2 = num;
            return table;
        }

        public void MeasureHeight()
        {
            if (!this._b9)
            {
                float num = 0f;
                for (int i = 0; i < this._b8.Count; i++)
                {
                    num += this._b8[i].Width;
                }
                this._b3 = num;
                this._b2 = A434(this, true);
                this._b9 = true;
            }
        }

        private void b15()
        {
            this._b4 = 0f;
            this._b2 = 0f;
            this._b3 = 0f;
            this._b5 = true;
            this._b6 = 1;
            this._b9 = false;
            this._b8 = new MControl.Printing.Pdf.Drawing.Columns();
            this._b7 = new MControl.Printing.Pdf.Drawing.Rows(this);
            this._b12 = false;
            this._b13 = 0;
            this._b14 = 0;
        }

        private static void b21(ref A120 b19, ref A112 b20, Row b22, float b0, float b1, float b23)
        {
            Table table = b22.Table;
            float cellPadding = table.CellPadding;
            float width = table.Width;
            Cells cells = b22.Cells;
            Cell cell = null;
            A477 A = null;
            MControl.Printing.Pdf.Border border = null;
            TableStyle style = null;
            float num3 = 0f;
            float num4 = b0;
            float num5 = b1 - b23;
            float num6 = 0f;
            float num7 = 0f;
            GraphicsElement.A435(ref b19, ref b20);
            GraphicsElement.A446(b20, b0, b19.A98(b1), width, b22.Height - b23);
            if (b22.BackColor != PdfColor.Transparent)
            {
                GraphicsElement.A175(b0, b1, width, b22.Height - b23, b22.BackColor, ref b19, ref b20);
            }
            else if (b22.Table.BackColor != PdfColor.Transparent)
            {
                GraphicsElement.A175(b0, b1, width, b22.Height - b23, b22.Table.BackColor, ref b19, ref b20);
            }
            for (int i = 0; i < cells.Count; i++)
            {
                cell = cells[i];
                A = cell.A481();
                border = cell.Border;
                num3 = cell.Width;
                style = cell.A405;
                GraphicsElement.A435(ref b19, ref b20);
                GraphicsElement.A446(b20, num4, b19.A98(b1), num3, b22.Height - b23);
                if (cell.BackColor != PdfColor.Transparent)
                {
                    GraphicsElement.A175(num4, b1, num3, b22.Height - b23, cell.BackColor, ref b19, ref b20);
                }
                if ((A != null) && ((A.A47 == A47.A38) || (A.A47 == A47.A48)))
                {
                    A478 A2 = cell.A479;
                    if ((A2 != null) && (A2.A2 > 0))
                    {
                        num6 = num4 + cellPadding;
                        num7 = num5;
                        b24(ref num7, cellPadding, cell);
                        A500.A119(ref b19, ref b20, cell.A479, 0, A2.A2, num6, num7, 0f, 0f, style.RightToLeft);
                    }
                }
                if (border != null)
                {
                    GraphicsElement.A156(num4, b1, num3, b22.Height - b23, border, ref b19, ref b20);
                }
                GraphicsElement.A438(ref b19, ref b20);
                num4 += cell.Width;
            }
            border = b22.Border;
            if (border != null)
            {
                GraphicsElement.A156(b0, b1, width, b22.Height - b23, border, ref b19, ref b20);
            }
            GraphicsElement.A438(ref b19, ref b20);
        }

        private static void b24(ref float b1, float b25, Cell b26)
        {
            ContentAlignment contentAlign = b26.ContentAlign;
            A478 A = b26.A479;
            switch (contentAlign)
            {
                case ContentAlignment.TopLeft:
                case ContentAlignment.TopCenter:
                case ContentAlignment.TopRight:
                    b1 += b25;
                    return;

                case ContentAlignment.MiddleLeft:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.MiddleRight:
                    b1 += ((b26.Height - A500.A503(A, 0, A.A2, b25, b25)) / 2f) + b25;
                    return;

                case ContentAlignment.BottomLeft:
                case ContentAlignment.BottomCenter:
                case ContentAlignment.BottomRight:
                    b1 += (b26.Height - A500.A503(b26.A479, 0, A.A2, b25, b25)) + b25;
                    break;
            }
        }

        internal TableStyle A405
        {
            get
            {
                return this._b11;
            }
            set
            {
                this._b11 = value;
            }
        }

        internal bool A473
        {
            get
            {
                return this._b9;
            }
            set
            {
                this._b9 = value;
            }
        }

        internal bool A482
        {
            get
            {
                return this._b12;
            }
        }

        internal float A495
        {
            get
            {
                return this._b10;
            }
            set
            {
                this._b10 = value;
            }
        }

        public PdfColor BackColor
        {
            get
            {
                return this._b11.BackColor;
            }
            set
            {
                if (!this._b12 && (this._b11.BackColor != value))
                {
                    this._b11 = TableStyle.A483(this._b11, value, this);
                }
            }
        }

        public MControl.Printing.Pdf.Border Border
        {
            get
            {
                return this._b11.Border;
            }
            set
            {
                if (!this._b12 && (this._b11.Border != value))
                {
                    this._b11 = TableStyle.A484(this._b11, value, this);
                }
            }
        }

        public float CellPadding
        {
            get
            {
                return this._b4;
            }
            set
            {
                if (!this._b12 && (this._b4 != value))
                {
                    this._b4 = value;
                    this._b9 = false;
                }
            }
        }

        public MControl.Printing.Pdf.Drawing.Columns Columns
        {
            get
            {
                return this._b8;
            }
        }

        public ContentAlignment ContentAlign
        {
            get
            {
                return this._b11.ContentAlign;
            }
            set
            {
                if (!this._b12 && (this._b11.ContentAlign != value))
                {
                    this._b11 = TableStyle.A490(this._b11, value, this);
                }
            }
        }

        public PdfFont Font
        {
            get
            {
                return this._b11.Font;
            }
            set
            {
                if (!this._b12 && (this._b11.Font != value))
                {
                    this._b11 = TableStyle.A157(this._b11, value, this);
                }
            }
        }

        public float FontSize
        {
            get
            {
                return this._b11.FontSize;
            }
            set
            {
                if (!this._b12 && (this._b11.FontSize != value))
                {
                    this._b11 = TableStyle.A486(this._b11, value, this);
                }
            }
        }

        public int HeaderRowCount
        {
            get
            {
                return this._b6;
            }
            set
            {
                if (value < 1)
                {
                    this._b6 = 1;
                }
                else
                {
                    this._b6 = value;
                }
            }
        }

        public float Height
        {
            get
            {
                this.MeasureHeight();
                return this._b2;
            }
        }

        public float LineSpace
        {
            get
            {
                return this._b11.LineSpace;
            }
            set
            {
                if (!this.A482 && (this._b11.LineSpace != value))
                {
                    this._b11 = TableStyle.A487(this._b11, value, this);
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public bool RepeatFirstRow
        {
            get
            {
                return this.RepeatHeaderRow;
            }
            set
            {
                this.RepeatHeaderRow = value;
            }
        }

        public bool RepeatHeaderRow
        {
            get
            {
                return this._b5;
            }
            set
            {
                if (!this._b12 && (this._b5 != value))
                {
                    this._b5 = value;
                }
            }
        }

        public bool RightToLeft
        {
            get
            {
                return this._b11.RightToLeft;
            }
            set
            {
                if (!this._b12 && (this._b11.RightToLeft != value))
                {
                    this._b11 = TableStyle.A491(this._b11, value, this);
                }
            }
        }

        public MControl.Printing.Pdf.Drawing.Rows Rows
        {
            get
            {
                return this._b7;
            }
        }

        public TextAlignment TextAlign
        {
            get
            {
                return this._b11.TextAlign;
            }
            set
            {
                if (!this._b12 && (this._b11.TextAlign != value))
                {
                    this._b11 = TableStyle.A489(this._b11, value, this);
                }
            }
        }

        public PdfColor TextColor
        {
            get
            {
                return this._b11.TextColor;
            }
            set
            {
                if (!this._b12 && (this._b11.TextColor != value))
                {
                    this._b11 = TableStyle.A485(this._b11, value, this);
                }
            }
        }

        public PdfColor TextHighlight
        {
            get
            {
                return this._b11.Highlight;
            }
            set
            {
                if (!this.A482 && (this._b11.Highlight != value))
                {
                    this._b11 = TableStyle.A492(this._b11, value, this);
                }
            }
        }

        public bool TextUnderline
        {
            get
            {
                return this._b11.Underline;
            }
            set
            {
                if (!this.A482 && (this._b11.Underline != value))
                {
                    this._b11 = TableStyle.A493(this._b11, value, this);
                }
            }
        }

        public float Width
        {
            get
            {
                this.MeasureHeight();
                return this._b3;
            }
        }

        public bool Wrap
        {
            get
            {
                return this._b11.Wrap;
            }
            set
            {
                if (!this._b12 && (this._b11.Wrap != value))
                {
                    this._b11 = TableStyle.A488(this._b11, value, this);
                }
            }
        }
    }
}

