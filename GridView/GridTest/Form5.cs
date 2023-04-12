using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GridTest
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            BindData();
        }

        public System.Data.DataSet DS = new System.Data.DataSet();

        private void BindData()
        {
            //this.gridControlColumn1.MappingName="tbl2";
            //this.gridControlColumn1.DataSource=DataSource.CreateDataTable("tbl2",10);
            //this.label1.MappingName="tbl";
            DS.Relations.Clear();
            DS.Tables.Clear();

            System.Data.DataTable dt1 = DataSource.CreateDataTable("Tbl1", 10, 1);
            System.Data.DataTable dt2 = DataSource.CreateDataTable("Tbl2", 10, 12);
            System.Data.DataTable dt3 = DataSource.CreateDataTable("Tbl3", 10, 1);
            DS.Tables.AddRange(new System.Data.DataTable[] { dt1, dt2, dt3 });

            DataRelation rel1 =
            DS.Relations.Add("rel1",
            DS.Tables["Tbl1"].Columns["Icon"],
            DS.Tables["Tbl2"].Columns["Icon"], false);

            DataRelation rel2 =
            DS.Relations.Add("rel2",
            DS.Tables["Tbl1"].Columns["Icon"],
            DS.Tables["Tbl3"].Columns["Icon"], false);


            //DS.Tables.Add(dt1);

            //gridControlColumn1.DataMember = "Tbl2";
            //gridControlColumn1.DataSource = DS;
            //gridControlColumn1.ForeignKey = "Icon";
            //gridControlColumn1.CaptionVisible = true;


            this.dataGridView1.DataMember = "Tbl1";
            this.dataGridView1.DataSource = DS;
     
            //this.grid1.MappingName = "Tbl1";
            //this.grid1.DataSource = dt1;

        }

    }
}