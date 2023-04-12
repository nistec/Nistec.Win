using System;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using Nistec.Printing.Sections;
using Nistec.Printing.Drawing;
//=using Nistec.Data;

namespace Nistec.Printing
{
	/// <summary>
	/// This class assists with the building of a McPrintDocument
	/// </summary>
	public partial class ReportBuilder
	{

        public static void PrintTextDocument(string bodyText, string headerText, string footerText)
        {
            PrintTextDocument(bodyText, headerText, footerText, true);
        }

        public static void PrintTextDocument(string bodyText, string headerText, string footerText, bool preview)
        {
            McPrintDocument rpt = new McPrintDocument();
            ReportBuilder rb = new ReportBuilder(rpt);
            rb.CreateTextDocument(bodyText, headerText, footerText);
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

        public virtual void AddHeaderText(string text, bool bold, Brush brush)
        {
            //Header
            TextStyle style = new TextStyle(TextStyle.PageHeader);
            style.Bold = bold;
            style.Brush = brush;
            this.AddTextSection(text, style);
        }

        public virtual void AddFooterText(string text, bool bold, Brush brush)
        {
            //Footer
            TextStyle style = new TextStyle(TextStyle.PageFooter);
            style.Bold = bold;
            style.Brush = brush;
            this.AddTextSection(text, style);
        }

        public virtual void AddBodyText(string text, bool bold, Brush brush)
        {
            //Body
            TextStyle style = new TextStyle(TextStyle.Normal);
            style.Bold = bold;
            style.Brush = brush;
            this.AddTextSection(text, style);
        }

        public virtual void CreateTextDocument(string bodyText)
        {
            this.AddTextSection(bodyText);
            this.FinishLinearLayout();
        }

        public virtual void CreateTextDocument(string bodyText, string headerText, string footerText)
        {
            this.StartContainer(new Nistec.Printing.Sections.LinearSections());
            TextStyle.ResetStyles();

           //Header
            if (string.IsNullOrEmpty(headerText))
            {
                TextStyle header = new TextStyle(TextStyle.PageHeader);
                header.Bold = true;
                header.Brush = Brushes.Blue;
                this.AddTextSection(headerText, header);
            }

            //Body
            TextStyle body = new TextStyle(TextStyle.Normal);
            this.AddTextSection(bodyText, body);

            //Footer
            if (string.IsNullOrEmpty(footerText))
            {
                this.AddTextSection(footerText, TextStyle.PageFooter);
            }
            this.FinishLinearLayout();
        }


	} // end class


}
