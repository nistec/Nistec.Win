using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MControl.Data;
using MControl.Printing;
using MControl.Printing.UI;
using MControl.Printing.View;
using MControl.Data.Factory;

namespace PrintingDemo
{
    public partial class Form1 : Form
    {

        public static string GetConnectionString()
        {
            string ComoonFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles);
            string DBpath = ComoonFolder + @"\MControl\Data\NorhwindDB.mdb";
            return string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};", DBpath);
        }

        //const string cnn = "Data Source=IL-TLV-NTRUJMAN; Initial Catalog=FrameworkDB; Integrated Security=SSPI; Connection Timeout=30";

        public Form1()
        {
            InitializeComponent();
        }

        private DataTable GetData()
        {
            IDbCmd cmd = DbFactory.Create(GetConnectionString(), DBProvider.OleDb);
            return cmd.ExecuteDataTable("Products", "Select * from products",false);
            //DataTable dt = new DataTable("preview");
            //dt.Columns.Add("ID");
            //dt.Columns.Add("Name");
            //dt.Columns.Add("Address");
            //dt.Rows.Add("1","aaa","adress a");
            //dt.Rows.Add("2", "bbb", "adress b");
            //dt.Rows.Add("3", "ccc", "adress c");
            //return dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            ReportBuilder.PrintDataView(GetData().DefaultView, "Products"); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ExportDlg ed = new ExportDlg(GetData());
            ed.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ImportDlg id = new ImportDlg();
            id.ShowDialog();
            DataTable dt= id.Source;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PrintDlg pd = new PrintDlg(GetData());
            pd.Show();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            ViewBuilder.Preview(GetData(), "","", PageOrientation.Default,false);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            InvoiceBuilder builder = new InvoiceBuilder(MControl.Printing.View.InvoiceType.InvoiceIL);
            builder.AddTitle("קונטרול נט", "ראשון לציון");
            builder.AddHeader("1234", "קשת טי וי", "תל אביב", DateTime.Now.ToString(), "חיוב חודש מרץ");
            builder.AddFooter("קונטרול נט ט.ל.ח", 0.16f);
            builder.AddRow("123", "שירות", 1m, 20.50m);
            builder.AddRow("456", "חלקי חילוף", 1m, 70.50m);
            builder.AddRow("789", "התקנה", 1m, 86.52m);
           
            //builder.CreateInvoice();
            builder.Generate();
            builder.Preview();
        }
    }
}