using System;
using System.Windows.Forms;
using System.Resources;
using System.Drawing;


namespace MControl.Printing.View.Design
{
    public class DataSourceDlg : System.Windows.Forms.Form//MControl.WinForms.FormBase
	{
		#region NetFram

        //private void //NetReflectedFram()
        //{
        //    this.cbCancel.//NetReflectedFram("ba7fa38f0b671cbc");
        //    this.cbOK.//NetReflectedFram("ba7fa38f0b671cbc");
        //}

		#endregion

		// Fields
		//private object var3;
		private string strCn;//var4;
		private string strSql;//var5;
		public bool connected;//mtd4;
		internal Button cbCancel;
		internal Button cbOK;
		private System.ComponentModel.Container components;
		internal Label Label1;
		internal Label label2;
		internal RichTextBox rtbCn;
		internal RichTextBox rtbSQL;

		public DataSourceDlg()
		{
			//netFramwork.//NetReflectedFram();
			this.components = null;
			this.InitializeComponent();
			//NetReflectedFram();
		}
		protected override void Dispose(bool disposing)
		{
			if (disposing && (this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataSourceDlg));
            this.cbCancel = new Button();
            this.cbOK = new Button();
            this.rtbSQL = new System.Windows.Forms.RichTextBox();
            this.rtbCn = new System.Windows.Forms.RichTextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbCancel
            // 
            this.cbCancel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cbCancel.Location = new System.Drawing.Point(352, 208);
            this.cbCancel.Name = "cbCancel";
            this.cbCancel.Size = new System.Drawing.Size(64, 24);
            this.cbCancel.TabIndex = 17;
            this.cbCancel.Text = "Cancel";
            //this.cbCancel.ToolTipText = "Cancel";
            this.cbCancel.Click += new System.EventHandler(this.var0);
            // 
            // cbOK
            // 
            this.cbOK.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cbOK.Location = new System.Drawing.Point(272, 208);
            this.cbOK.Name = "cbOK";
            this.cbOK.Size = new System.Drawing.Size(64, 24);
            this.cbOK.TabIndex = 16;
            this.cbOK.Text = "OK";
            //this.cbOK.ToolTipText = "OK";
            this.cbOK.Click += new System.EventHandler(this.var1);
            // 
            // rtbSQL
            // 
            this.rtbSQL.Location = new System.Drawing.Point(13, 112);
            this.rtbSQL.Name = "rtbSQL";
            this.rtbSQL.Size = new System.Drawing.Size(403, 88);
            this.rtbSQL.TabIndex = 12;
            this.rtbSQL.Text = "";
            // 
            // rtbCn
            // 
            this.rtbCn.Location = new System.Drawing.Point(13, 32);
            this.rtbCn.Name = "rtbCn";
            this.rtbCn.Size = new System.Drawing.Size(403, 48);
            this.rtbCn.TabIndex = 11;
            this.rtbCn.Text = "";
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(13, 8);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(112, 16);
            this.Label1.TabIndex = 10;
            this.Label1.Text = "Connecting String";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 16);
            this.label2.TabIndex = 20;
            this.label2.Text = "SQL Text";
            // 
            // DataSourceDlg
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(434, 247);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbCancel);
            this.Controls.Add(this.cbOK);
            this.Controls.Add(this.rtbSQL);
            this.Controls.Add(this.rtbCn);
            this.Controls.Add(this.Label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DataSourceDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Set Report DataSource";
            this.Load += new System.EventHandler(this.var2);
            this.Controls.SetChildIndex(this.Label1, 0);
            this.Controls.SetChildIndex(this.rtbCn, 0);
            this.Controls.SetChildIndex(this.rtbSQL, 0);
            this.Controls.SetChildIndex(this.cbOK, 0);
            this.Controls.SetChildIndex(this.cbCancel, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.ResumeLayout(false);

		}

		private void var0(object sender, EventArgs e)
		{
			this.connected = false;
			base.Close();
		}

		private void var1(object sender, EventArgs e)
		{
			this.strSql = this.rtbSQL.Text;
			if (this.rtbCn.Text.EndsWith(";"))
			{
				this.strCn = this.rtbCn.Text;
			}
			else
			{
				this.strCn = this.rtbCn.Text + ";";
			}
			this.connected = true;
			base.Close();
		}

		private void var2(object sender, EventArgs e)
		{
			this.rtbCn.Text = this.strCn;
			this.rtbSQL.Text = this.strSql;
		}

		public string ConnectionString//mtd391
		{
			get
			{
				return this.strCn;
			}
			set
			{
				this.strCn = value;
			}
		}
		public string SqlString//mtd392
		{
			get
			{
				return this.strSql;
			}
			set
			{
				this.strSql = value;
			}
		}

 
	}

}
