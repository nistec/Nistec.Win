using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Nistec.GridView;
using Nistec.Win;

namespace GridViewTest
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InitVGridFields();
        }
        public enum Typ
        {
            type1,
            type2,
            type3
        }
        private void InitVGridFields()
        {
            GridField[] fields = new GridField[13];
            string[] list = new string[] {"aaa","bbb","ccc" };
            fields[0] = new GridField("FriendlyName", "", Nistec.WinForms.MultiType.Brows);
            fields[1] = new GridField("ProviderName", "");
            fields[2] = new GridField("PersistSecurityInfo", "");
            fields[3] = new GridField("IntegratedSecurity", "");
            fields[4] = new GridField("TimeOut", 30);
            fields[5] = new GridField("Encrypt", false);
            fields[6] = new GridField("Server", "type1",typeof(Typ));
            fields[7] = new GridField("Database", "aaa", list);
            fields[8] = new GridField("UserID", "");
            fields[9] = new GridField("Password", "");
            fields[10] = new GridField("Asynchronous Processing", false);
            fields[11] = new GridField("PacketSize", 8192);
            fields[12] = new GridField("WorkstationID", "");

            this.vGrid1.SetDataBinding(fields, "CONNECTION");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = this.vGrid1.Fields["Server"].Text;
            this.vGrid1.Fields["Server"].Value = Typ.type2.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Nistec.WinForms.MultiType ft = Nistec.WinForms.MultiType.Boolean;
            this.vGrid1.SetDataBinding(ft, "FieldType");
            this.vGrid1.CaptionText = "Enum";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GridField field = new GridField("Key", "Value");
            this.vGrid1.SetDataBinding(field, "Field");
            this.vGrid1.CaptionText = "Class";
        }

        private void button5_Click(object sender, EventArgs e)
        {

            IColumn col =new McColumn();
            col.ColumnName="col name";
            col.Caption = "caption";
            col.Ordinal = 1;
            col.FieldType = FieldType.Text;

            this.vGrid1.SetDataBinding(col, "IColumn");
            this.vGrid1.CaptionText = "Interface";

        }

        private void button6_Click(object sender, EventArgs e)
        {
            FieldType dt= FieldType.Text;
            //Record rc = new Record("display", dt);
            //this.vGrid1.SetDataBinding(rc, "Record");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string[] list = new string[] { "aaa","bbb","ccc"};
            this.vGrid1.SetDataBinding(list, "array");
            this.vGrid1.CaptionText = "Array";
        }



    }
}