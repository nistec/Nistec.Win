using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace GridTest
{
	/// <summary>
	/// Summary description for Form2.
	/// </summary>
	public class Form2 : System.Windows.Forms.Form
    {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form2()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            mControl.GridView.VGridField vGridField1 = new mControl.GridView.VGridField();
            mControl.GridView.VGridField vGridField2 = new mControl.GridView.VGridField();
            mControl.GridView.VGridField vGridField3 = new mControl.GridView.VGridField();
            mControl.GridView.VGridField vGridField4 = new mControl.GridView.VGridField();
            this.ctlButton1 = new mControl.WinCtl.Controls.CtlButton();
            this.grid2 = new mControl.GridView.GridVirtual();
            this.grid1 = new mControl.GridView.VGrid();
            ((System.ComponentModel.ISupportInitialize)(this.grid2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).BeginInit();
            this.SuspendLayout();
            // 
            // ctlButton1
            // 
            this.ctlButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ctlButton1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.ctlButton1.Location = new System.Drawing.Point(329, 329);
            this.ctlButton1.Name = "ctlButton1";
            this.ctlButton1.Size = new System.Drawing.Size(100, 34);
            this.ctlButton1.TabIndex = 2;
            this.ctlButton1.Text = "ctlButton1";
            this.ctlButton1.ToolTipText = "ctlButton1";
            this.ctlButton1.Click += new System.EventHandler(this.ctlButton1_Click);
            // 
            // grid2
            // 
            this.grid2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grid2.BackColor = System.Drawing.Color.White;
            this.grid2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grid2.ControlLayout = mControl.WinCtl.Controls.ControlLayout.System;
            this.grid2.Dimension = new System.Drawing.Size(5, 5);
            this.grid2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.grid2.ForeColor = System.Drawing.Color.Black;
            this.grid2.GridLineStyle = mControl.GridView.GridLineStyle.Solid;
            this.grid2.Location = new System.Drawing.Point(36, 171);
            this.grid2.MappingName = "VirtualGrid";
            this.grid2.Name = "grid2";
            this.grid2.PaintAlternating = false;
            this.grid2.Size = new System.Drawing.Size(267, 192);
            this.grid2.TabIndex = 1;
            // 
            // grid1
            // 
            this.grid1.AllowAdd = false;
            this.grid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grid1.BackColor = System.Drawing.Color.White;
            this.grid1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grid1.ColumnKeyBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grid1.ColumnKeyForeColor = System.Drawing.Color.Black;
            vGridField1.FieldType = mControl.WinCtl.Controls.MultiComboTypes.Boolean;
            vGridField1.Key = "item1";
            vGridField1.Text = "";
            vGridField1.Value = "";
            vGridField2.FieldType = mControl.WinCtl.Controls.MultiComboTypes.Combo;
            vGridField2.Items.Add("aaaaa");
            vGridField2.Items.Add("bbbbb");
            vGridField2.Items.Add("ccccc");
            vGridField2.Items.Add("dddddd");
            vGridField2.Key = "item2";
            vGridField2.Text = "";
            vGridField2.Value = "";
            vGridField3.FieldType = mControl.WinCtl.Controls.MultiComboTypes.Text;
            vGridField3.Key = "item3";
            vGridField3.Text = "ccccc";
            vGridField3.Value = "ccccc";
            vGridField4.FieldType = mControl.WinCtl.Controls.MultiComboTypes.Text;
            vGridField4.Key = "item4";
            vGridField4.Text = "ddddd";
            vGridField4.Value = "ddddd";
            this.grid1.Fields.AddRange(new mControl.GridView.VGridField[] {
            vGridField1,
            vGridField2,
            vGridField3,
            vGridField4});
            this.grid1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.grid1.ForeColor = System.Drawing.Color.Black;
            this.grid1.GridLineStyle = mControl.GridView.GridLineStyle.Solid;
            this.grid1.Location = new System.Drawing.Point(36, 12);
            this.grid1.Name = "grid1";
            this.grid1.PaintAlternating = false;
            this.grid1.RowHeadersVisible = false;
            this.grid1.Size = new System.Drawing.Size(267, 132);
            this.grid1.TabIndex = 0;
            // 
            // Form2
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(443, 389);
            this.Controls.Add(this.ctlButton1);
            this.Controls.Add(this.grid2);
            this.Controls.Add(this.grid1);
            this.Name = "Form2";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.grid2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

        private mControl.GridView.VGrid grid1;
        private mControl.GridView.GridVirtual grid2;
        private mControl.WinCtl.Controls.CtlButton ctlButton1;





        public System.Data.DataSet DS=new System.Data.DataSet();
		
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad (e);

			System.Data.DataTable dt1=DataSource.CreateDataTable("Tbl1",10,1);
			System.Data.DataTable dt2=DataSource.CreateDataTable("Tbl2",10,1);
			System.Data.DataTable dt3=DataSource.CreateDataTable("Tbl3",10,1);
			DS.Tables.AddRange(new System.Data.DataTable[]{dt1,dt2,dt3});


            mControl.GridView.VGridField[] gs = new mControl.GridView.VGridField[4];
            gs[0] = new mControl.GridView.VGridField("item1", 1);
            gs[1] = new mControl.GridView.VGridField("item2", "hello");
            gs[2] = new mControl.GridView.VGridField("item3", false);
            gs[3] = new mControl.GridView.VGridField("item4", DateTime.Now);
            //this.grid1.SetDataBinding(gs, "gs");
			//this.dataGrid1.DataSource=DataSource.CreateDataTable("tblg",10);

            //this.grid2.Dimension = new Size(70, 5);

		}

        private void ctlButton1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this.grid1.GetValue("item2").ToString());
        }

	}
}
