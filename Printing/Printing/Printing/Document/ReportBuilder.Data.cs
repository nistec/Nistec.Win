using System;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using Nistec.Printing.Sections;
using Nistec.Printing.Drawing;
using Nistec.Win;
//=using Nistec.Data;

namespace Nistec.Printing
{
	/// <summary>
	/// This class assists with the building of a McPrintDocument
	/// </summary>
	public partial class ReportBuilder
	{
        public const float DefaultColumnWidth = 80;

        public static HorizontalAlignment ConvertRtlToAlignment(System.Windows.Forms.RightToLeft rtl)
        {
            return rtl == RightToLeft.Yes ? HorizontalAlignment.Right : HorizontalAlignment.Left;
        }

        public static void PrintDataView(DataView dv, string header)
        {
            PrintDataView(dv, header, true);
        }

        public static void PrintDataView(DataView dv, string header, bool preview)
        {
            PrintDataView(dv, header, HorizontalAlignment.Left, (IDataField[])null, preview);
        }

        public static void PrintDataView(DataView dv, string header,HorizontalAlignment alignment,McColumn[] columns, bool preview)
        {
            if (dv == null)
            {
                return;
            }

            McPrintDocument rpt = new McPrintDocument();
            ReportBuilder rb = new ReportBuilder(rpt);
            rb.CreateDataDocument(dv, alignment);//,DefaultColumnWidth,fields);
            rb.CreateColumns(columns);
            rb.CreateHeaderAndFooter(header);

            if (preview)
            {
                McPrintPreviewDialog dlg = new McPrintPreviewDialog();
                dlg.Document = rpt;
                dlg.Show();
            }
            else
            {
                rpt.Print();
            }
        }
        public static void PrintDataView(DataView dv, string header, HorizontalAlignment alignment, IDataField[] fields, bool preview)
        {
            if (dv == null)
            {
                return;
            }
            McPrintDocument rpt = new McPrintDocument();
            ReportBuilder rb = new ReportBuilder(rpt);
            //rb.CreateTextDocument("abc","header","footer");//, DefaultColumnWidth, fields);
            rb.CreateDataDocument(dv, alignment);//, DefaultColumnWidth, fields);
            rb.CreateColumns(fields);
            rb.CreateHeaderAndFooter(header);

            if (preview)
            {
                McPrintPreviewDialog dlg = new McPrintPreviewDialog();
                dlg.Document = rpt;
                dlg.Show();
            }
            else
            {
                rpt.Print();
            }
        }
        public void CreateHeaderAndFooter(string header)
        {
            if (!string.IsNullOrEmpty(header))
            {
                this.AddPageHeader(header, HorizontalAlignment.Center);
            }
            this.AddPageFooter("Page %p", "Page %p", "Page %p", HorizontalAlignment.Center);

        }
        public virtual void CreateDataDocument(DataView dv)
        {
            CreateDataDocument(dv, HorizontalAlignment.Left);//, DefaultColumnWidth, null);
        }

        //public virtual void CreateDataDocument(DataView dv, HorizontalAlignment alignment)
        //{
        //    CreateDataDocument(dv, alignment, DefaultColumnWidth, null);
        //}

        public void CreateDataDocument(DataView dv, HorizontalAlignment alignment)
        {

            //ResetStyles();
            this.StartContainer(new Nistec.Printing.Sections.LinearSections());
            TextStyle.ResetStyles();

            // Following line sets up the pen used for lins for tables
            this.DefaultTablePen = this.currentDocument.ThinPen;

            TextStyle.Heading1.StringAlignment = alignment == HorizontalAlignment.Left ? StringAlignment.Near : alignment == HorizontalAlignment.Center ? StringAlignment.Center : StringAlignment.Far;

            //SetDefaultStyle();

            this.currentDocument.SectionData = this.AddDataSection(dv, true);

            this.CurrentSection.HorizontalAlignment = alignment;// HorizontalAlignment.Left;
            this.CurrentSection.UseFullWidth = true;
            this.CurrentSection.UseFullHeight = true;
        }

        //public void CreateDataColumns(McColumn[] fields)
        //{
        //    int colTotalWidth = 0;
        //    int docwidth = this.CurrentDocument.DefaultPageSettings.PaperSize.Width;

        //    this.currentDocument.SectionData.ClearColumns();
        //    if (fields != null)
        //    {
        //        colTotalWidth = GetColumnsTotalWidth(fields);
        //        if (docwidth < colTotalWidth)
        //        {
        //            this.CurrentDocument.DefaultPageSettings.Landscape = true;
        //        }

        //        foreach (McColumn c in fields)
        //        {
        //            if (c.Display)
        //            {
        //                this.AddColumn(c.ColumnName, c.Caption, (float)c.Width, true, true, c.Alignment);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        colTotalWidth = GetColumnsTotalWidth(DefaultColumnWidth);
        //        if (docwidth < colTotalWidth)
        //        {
        //            this.CurrentDocument.DefaultPageSettings.Landscape = true;
        //        }
        //        this.AddAllColumns(DefaultColumnWidth, true, true);
        //    }
        //}

        public void CreateColumns(McColumn[] cols)
        {
            this.currentDocument.SectionData.ClearColumns();

            int docwidth = this.CurrentDocument.DefaultPageSettings.PaperSize.Width;
            int colTotalWidth = 0;

            if (cols != null)
            {
                colTotalWidth = GetColumnsTotalWidth(cols);
                if (docwidth < colTotalWidth)
                {
                    this.CurrentDocument.DefaultPageSettings.Landscape = true;
                }

                foreach (McColumn c in cols)
                {
                    if (c.Display)
                    {
                        ReportDataColumn rdc = this.AddColumn(c.ColumnName, c.Caption, (float)c.Width, true, true);
                        if (c.FieldType == FieldType.Number)
                            rdc.ColumnAlignment = HorizontalAlignment.Right;
                        else
                            rdc.ColumnAlignment = c.Alignment;
                    }
                }
                //isColumnCreated = true;
            }
        }

        public void CreateColumns(IDataField[] fields)
        {
            int colTotalWidth = 0;
            int docwidth = this.CurrentDocument.DefaultPageSettings.PaperSize.Width;

            this.currentDocument.SectionData.ClearColumns();
            if (fields != null)
            {
                colTotalWidth =(int) (((float)fields.Length)*DefaultColumnWidth);
                if (docwidth < colTotalWidth)
                {
                    this.CurrentDocument.DefaultPageSettings.Landscape = true;
                }

                foreach (McColumn c in fields)
                {
                        this.AddColumn(c.ColumnName, c.Caption, (float)c.Width, true, true, c.Alignment);
                }
            }
            else
            {
                colTotalWidth = GetColumnsTotalWidth(DefaultColumnWidth);
                if (docwidth < colTotalWidth)
                {
                    this.CurrentDocument.DefaultPageSettings.Landscape = true;
                }
                this.AddAllColumns(DefaultColumnWidth, true, true);
            }
        }
        public void ResetStyles()
        {
            TextStyle.ResetStyles();
            //isResetStyle = true;
        }

        public void SetCellMargins(float top, float left, float right, float bottom)
        {
            TextStyle.TableRow.MarginNear = left / 10;// 0.1f;
            TextStyle.TableRow.MarginFar = right / 10;//0.1f;
            TextStyle.TableRow.MarginTop = top / 10;// 0.05f;
            TextStyle.TableRow.MarginBottom = bottom / 10;//0.05f;
        }

        public void SetHeaderMargins(float top, float left, float right, float bottom)
        {
            TextStyle.TableHeader.MarginNear = left / 10;// 0.1f;
            TextStyle.TableHeader.MarginFar = right / 10;//0.1f;
            TextStyle.TableHeader.MarginTop = top / 10;// 0.05f;
            TextStyle.TableHeader.MarginBottom = bottom / 10;//0.05f;
        }

        public void SetHeader(Brush headerBack, Brush headerFore, Brush background)
        {
            TextStyle.Heading1.Size = 24;
            TextStyle.Heading1.Bold = false;
            TextStyle.TableHeader.BackgroundBrush = headerBack;//Brushes.Navy;
            TextStyle.TableHeader.Brush = headerFore;//Brushes.White;
            TextStyle.TableRow.BackgroundBrush = background;//Brushes.White;//
        }

        public void SetDefaultStyle()
        {
            //if (!isResetStyle)
            //    ResetStyles();
            SetHeader(Brushes.Navy, Brushes.White, Brushes.Transparent);
            SetHeaderMargins(0.5f, 1f, 1f, 0.5f);
            SetHeaderMargins(0.1f, 0.1f, 0.1f, 0.1f);
        }

        public void SetOrientation(bool landscape)
        {
            this.CurrentDocument.DefaultPageSettings.Landscape = landscape;
        }

  
        public virtual ReportDataColumn AddColumn(McColumn col)
        {
            if (col == null)
            {
                throw new ArgumentException("Invalid Column control");
            }
            ReportDataColumn rdc = this.AddColumn(col.ColumnName, col.Caption, (float)col.Width, true, true);
            rdc.ColumnAlignment = col.Alignment;
            return rdc;
        }

        public virtual ReportDataColumn AddColumn(string colName, string caption, int width)
        {
            if (colName != null || caption == null || width < 0)
            {
                throw new ArgumentException("Incorrect parameters");
            }
            ReportDataColumn rdc = this.AddColumn(colName, caption, (float)width, true, true);
            return rdc;
        }

        public virtual ReportDataColumn AddColumn(string colName, string caption, int width, HorizontalAlignment alignment)
        {
            if (colName != null || caption == null || width < 0)
            {
                throw new ArgumentException("Incorrect parameters");
            }
            ReportDataColumn rdc = this.AddColumn(colName, caption, (float)width, true, true);
            rdc.ColumnAlignment = alignment;
            return rdc;
        }

        private int GetColumnsTotalWidth(McColumn[] cols)
        {
            int colTotalWidth = 0;
            if (cols != null)
            {
                foreach (McColumn c in cols)
                {
                    if (c.Display)
                    {
                        colTotalWidth += c.Width;
                    }
                }
            }
            return colTotalWidth;
        }

        private int GetColumnsTotalWidth( float maxColWidth)
        {
            int colTotalWidth = 0;
            DataView dv = this.currentDocument.SectionData.DataSource;
            if (dv != null)
            {
                colTotalWidth = dv.Table.Columns.Count * (int)maxColWidth;
            }
            return colTotalWidth;
        }


	} // end class


}
