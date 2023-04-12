using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Nistec.Data;
using Nistec.GridView;
using System.Data.SqlClient;
using System.Xml;
using Nistec.Data.Advanced;
using Nistec.Data.Factory;


namespace GridViewTest
{
    public partial class Form1 : Form
    {

        //const string cnn = "Server=192.114.70.146;Uid=sa;Pwd=dima78;Database=Pcm";
        const string cnn = @"Data Source=MCONTROL; Initial Catalog=FrameworkDB; uid=sa;password=tishma; Connection Timeout=30";
        
        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitVGrid();

        }
        DateTime startTime;

        private void InitVGrid()
        {
            GridField[] fields = new GridField[10];

            fields[0] = new GridField("FriendlyName", "framework");
            fields[1] = new GridField("ProviderName", "sql");
            fields[2] = new GridField("PersistSecurityInfo", false);
            fields[3] = new GridField("IntegratedSecurity", "SSPI");
            fields[4] = new GridField("TimeOut", 30);
            fields[5] = new GridField("Encrypt", false);
            fields[6] = new GridField("Server", "");
            fields[7] = new GridField("Database", "");
            fields[8] = new GridField("UserID", "");
            fields[9] = new GridField("Password", "");
        
            this.vGrid1.SetDataBinding(fields, "CONNECTION");
           object o= vGrid1[new GridCell(0,0)];

           //VGridDlg dlg = new VGridDlg();
           //dlg.VGrid.SetDataBinding(fields,"conn");
           //dlg.ShowDialog();
        }

   
        private void button1_Click(object sender, EventArgs e)
        {
            //ReadOrderData("GridColumns");//Connection");//"TableSchems");//"GridColumns");

            //return;
            
            
            DataTable dt=GetDataSource("QueueItems_Log");
            startTime = DateTime.Now;
            
            this.grid1.DataSource = dt;
            GridTextColumn col =(GridTextColumn) this.grid1.Columns["Status"];
            object[,] list = new object[,] { { 0, "none" }, { 1, "process" }, { 2, "completed" } };
            col.SetLookupView(new LookupView( list));

            //TimeSpan ts = DateTime.Now.Subtract(startTime);
            //MessageBox.Show(ts.TotalMilliseconds.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dt = GetDataSource("QueueItems_Log");
            startTime = DateTime.Now;
            this.dataGridView1.DataSource = dt;
            TimeSpan ts = DateTime.Now.Subtract(startTime);
            MessageBox.Show(ts.TotalMilliseconds.ToString());
        }

        private DataTable GetDataSource(string tableName)
        {
           IDbCmd cmd= DbFactory.Create(cnn, Nistec.Data.DBProvider.SqlServer);
           return cmd.ExecuteDataTable(tableName, "select top 10000 * from " + tableName,false);

        }


        private void ReadOrderData(string option)
        {
            DataTable dtSource;

            string queryString = null;
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader reader = null;

            this.grid1.DataSource = null;
            this.grid1.AllowAdd = false;

            switch (option)
            {
                case "GridColumns":
                    this.grid1.Columns.AddRange(new string[] { "ItemID", "QueueName", "Subject" });
                    queryString = "SELECT ItemID, QueueName, Subject FROM QueueItems_Log;";
                    connection = new SqlConnection(cnn);
                    connection.Open();
                    command = new SqlCommand(queryString, connection);
                    reader = command.ExecuteReader();
                    this.grid1.InvokeDataSource(reader, "QueueItems", 10, 1000);
                    break;

                case "TableSchems":

                    queryString = "SELECT * FROM QueueItems_Log;";
                    connection = new SqlConnection(cnn);
                    connection.Open();
                    command = new SqlCommand(queryString, connection);
                    SqlDataAdapter adp = new SqlDataAdapter(queryString, connection);
                    dtSource = new DataTable();
                    adp.FillSchema(dtSource, SchemaType.Source);
                    reader = command.ExecuteReader();
                    this.grid1.InvokeDataSource(reader, dtSource, "QueueItems", 10, 1000);
                    break;
                case "Connection":
                    queryString = "SELECT * FROM QueueItems_Log;";
                    connection = new SqlConnection(cnn);
                    this.grid1.InvokeDataSource(connection, queryString, "QueueItems", 10, 1000);
                    break;
            }

  
        }


        private void button3_Click(object sender, EventArgs e)
        {
            DataTable dt = GetDataSource("QueueItems_Log");
            startTime = DateTime.Now;
            this.grid1.ReBinding(dt);
            TimeSpan ts = DateTime.Now.Subtract(startTime);
            MessageBox.Show(ts.TotalMilliseconds.ToString());

        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataTable dt = GetDataSource("QueueItems_Log");
            startTime = DateTime.Now;
            this.dataGridView1.DataSource = dt;
            TimeSpan ts = DateTime.Now.Subtract(startTime);
            MessageBox.Show(ts.TotalMilliseconds.ToString());

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Nistec.GridView.Develop.GridColumnsAddDlg f = new Nistec.GridView.Develop.GridColumnsAddDlg();
            //f.Show();
            Nistec.GridView.GridPerform.ChartAdd(this.grid1,"Messages groups", "Messages", Aggregate.Sum,  "MessageId", "MessageId","MsgGroup","MessageId<10");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DataTable dt = GetDataSource("QueueItems_Log");
            LookupView view = new LookupView(new object[,] { { "None", 0 }, { "Pending", 1 }, { "Process", 2 }, { "Deliverd", 3 } });
            view.Values.Sorted = true;
            this.gridLabelColumn1.SetLookupView(view);
            this.grid2.DataSource = dt;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //this.gridStatusBar1.SummarizeAllColumnsSetting();
            //this.gridStatusBar1.SummarizeColumns();
            this.grid1.UpdateChanges(cnn, DBProvider.SqlServer,this.grid1.MappingName);
        }
    }
}