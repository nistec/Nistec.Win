using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Nistec.WinForms;
using Nistec.GridView;
using Nistec.Testing.Data.Ole;

namespace GridViewTest
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void BindGrid_0()
        {
            DataTable dt = DalOle.Instance.DBApp.Orders();
            dt.TableName = "Orders";

            DataTable dtDetails = DalOle.Instance.DBApp.OrdersDetails();
            dtDetails.TableName = "OrdersDetails";

            DataTable dtProducts = DalOle.Instance.DBApp.Products();
            dtProducts.TableName = "Products";

            this.CustomerID.DataSource = DalOle.Instance.DBApp.Customers().DefaultView;
            this.ShipVia.DataSource = DalOle.Instance.DBApp.Shippers().DefaultView;


            this.comboBox1.DisplayMember = "CompanyName";
            this.comboBox1.ValueMember = "CustomerID";
            this.comboBox1.Sorted = true;
            this.comboBox1.DataSource = DalOle.Instance.DBApp.Customers().DefaultView;
           
            DataSet DS = new DataSet();
            DS.Tables.AddRange(new System.Data.DataTable[] { dt, dtDetails, dtProducts });

            DataRelation rel1 =
            DS.Relations.Add("rel1",
            DS.Tables["Orders"].Columns["OrderID"],
            DS.Tables["OrdersDetails"].Columns["OrderID"], false);

            DataRelation rel2 =
            DS.Relations.Add("rel2",
            DS.Tables["OrdersDetails"].Columns["ProductID"],
            DS.Tables["Products"].Columns["ProductID"], false);

            //this.grid1.Init(DS.Tables["Orders"], "", "Orders");
         
            this.grid1.Init(DS, "Orders", "Orders");
            this.OrderID.DataSource = dtDetails;

        }

        private void button1_Click(object sender, EventArgs e)
        {
 
                    BindGrid_0();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.grid1.SummarizeColumns();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.grid1.StatusBarVisible = this.checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            this.grid1.StatusBar.ShowColumns = this.checkBox2.Checked;
        }

        private void grid1_ButtonClick(object sender, Nistec.GridView.ButtonClickEventArgs e)
        {
            DataTable dtProducts = DalOle.Instance.DBApp.Products();
            dtProducts.TableName = "Products";
            //object o= Nistec.WinUI.Forms.ListDlgForm.OpenDialog(dtProducts, "ProductID", "ProductName", "Products", "");
            //if(o!=null)
            //this.gridMultiColumn1.Text = o.ToString();
           //Nistec.WinUI.Forms.ListDlgForm.OpenDialog( Nistec.WinUI.Forms.ListDlgForm.ListDialogMode.DropDown,this.gridMultiColumn1.EditBox,dtProducts, "ProductID", "ProductName", "Products", "");
    }

    }
}