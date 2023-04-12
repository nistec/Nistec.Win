using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Data;
using System.Drawing;
using Nistec.Printing.View.Templates;
using Nistec.Printing.View.Pdf;
using Nistec.Printing.View.Html;
using Nistec.Printing.View.Img;
using Nistec.Printing.View.Viewer;
using System.IO;
//using Nistec.Printing.View.Design;

namespace Nistec.Printing.View
{
	/// <summary>
	/// Summary description for ViewBuilder.
	/// </summary>
	public class DataViewer
	{
      

        public static void ViewReport(DataTable dataSource)
		{
            ViewBuilder builder = new ViewBuilder();
            builder.CreateReport(dataSource);
            ReportTemplate report = builder.Generate();
            ReportViewer.Preview(report);
		}

		public static void ViewReport(DataTable dataSource,DataGridTableStyle tableStyle)
		{
            ViewBuilder builder = new ViewBuilder();
            builder.CreateReport(dataSource, tableStyle);
            ReportTemplate report = builder.Generate();
            ReportViewer.Preview(report);

		}

		public static void ViewReport(string dataSource,string header,bool rightToLeft)
		{
            ViewBuilder builder = new ViewBuilder();
            builder.CreateReport(dataSource, header, rightToLeft);
            ReportTemplate report = builder.Generate();
            ReportViewer.Preview(report);
		}

        public static void PrintReport(DataTable dataSource)
        {
            ViewBuilder builder = new ViewBuilder();
            builder.CreateReport(dataSource);
            ReportTemplate report = builder.Generate();

            //PrintDialog pd = new PrintDialog();
            //pd.ShowDialog();
            //report.pr = report.p;
            report.Document.Print();
        }

  
		

	}
}
