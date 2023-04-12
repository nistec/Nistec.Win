namespace GridViewTest
{
    using Nistec.WinForms;
    using Nistec.Win;

    partial class FormTest
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTest));
            this.panel = new System.Windows.Forms.Panel();
            this.mcTextBox1 = new Nistec.WinForms.McTextBox();
            this.comboBox1 = new Nistec.WinForms.McComboBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1 = new Nistec.WinForms.McTabControl();
            this.tabPage1 = new Nistec.WinForms.McTabPage();
            this.grid1 = new Nistec.GridView.Grid();
            this.CustomerID = new Nistec.GridView.GridComboColumn();
            this.OrderID = new Nistec.GridView.VGridColumn();
            this.OrderDate = new Nistec.GridView.GridDateColumn();
            this.ShippedDate = new Nistec.GridView.GridDateColumn();
            this.ShipVia = new Nistec.GridView.GridComboColumn();
            this.Freight = new Nistec.GridView.GridTextColumn();
            this.gridBtn = new Nistec.GridView.GridButtonColumn();
            this.popUpItem1 = new Nistec.WinForms.PopUpItem();
            this.popUpItem2 = new Nistec.WinForms.PopUpItem();
            this.popUpItem3 = new Nistec.WinForms.PopUpItem();
            this.IsShipped = new Nistec.GridView.GridBoolColumn();
            this.gridProgressColumn1 = new Nistec.GridView.GridProgressColumn();
            this.tabPage2 = new Nistec.WinForms.McTabPage();
            this.panel.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.mcTextBox1);
            this.panel.Controls.Add(this.comboBox1);
            this.panel.Controls.Add(this.checkBox2);
            this.panel.Controls.Add(this.checkBox1);
            this.panel.Controls.Add(this.button2);
            this.panel.Controls.Add(this.button1);
            this.panel.Location = new System.Drawing.Point(9, 68);
            this.panel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(1112, 75);
            this.panel.TabIndex = 0;
            // 
            // mcTextBox1
            // 
            this.mcTextBox1.Location = new System.Drawing.Point(902, 22);
            this.mcTextBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.mcTextBox1.Name = "mcTextBox1";
            this.mcTextBox1.Size = new System.Drawing.Size(180, 26);
            this.mcTextBox1.TabIndex = 6;
            // 
            // comboBox1
            // 
            this.comboBox1.ButtonToolTip = "";
            this.comboBox1.IntegralHeight = false;
            this.comboBox1.Location = new System.Drawing.Point(624, 22);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(182, 26);
            this.comboBox1.TabIndex = 5;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(434, 28);
            this.checkBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(106, 24);
            this.checkBox2.TabIndex = 4;
            this.checkBox2.Text = "Staus Bar";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(303, 28);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(106, 24);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "Staus Bar";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(160, 18);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 35);
            this.button2.TabIndex = 2;
            this.button2.Text = "Load";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(18, 18);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 35);
            this.button1.TabIndex = 1;
            this.button1.Text = "Load";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControl1.ItemSize = new System.Drawing.Size(0, 20);
            this.tabControl1.Location = new System.Drawing.Point(4, 83);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Size = new System.Drawing.Size(1137, 483);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.TabPages.AddRange(new Nistec.WinForms.McTabPage[] {
            this.tabPage1,
            this.tabPage2});
            this.tabControl1.TabStop = false;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.tabPage1.Controls.Add(this.grid1);
            this.tabPage1.Location = new System.Drawing.Point(4, 27);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage1.Size = new System.Drawing.Size(1128, 451);
            this.tabPage1.Text = "Grid Sample";
            // 
            // grid1
            // 
            this.grid1.AutoAdjust = true;
            this.grid1.BackColor = System.Drawing.Color.White;
            this.grid1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.grid1.CaptionText = "Grid Relation";
            this.grid1.Columns.AddRange(new Nistec.GridView.GridColumnStyle[] {
            this.CustomerID,
            this.OrderID,
            this.OrderDate,
            this.ShippedDate,
            this.ShipVia,
            this.Freight,
            this.gridBtn,
            this.IsShipped,
            this.gridProgressColumn1});
            this.grid1.ControlLayout = Nistec.WinForms.ControlLayout.XpLayout;
            this.grid1.DataMember = "";
            this.grid1.DisableOnLoading = false;
            this.grid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.grid1.ForeColor = System.Drawing.Color.Black;
            this.grid1.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.grid1.Location = new System.Drawing.Point(4, 5);
            this.grid1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grid1.Name = "grid1";
            this.grid1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grid1.Size = new System.Drawing.Size(1120, 441);
            this.grid1.StatusBarVisible = true;
            this.grid1.TabIndex = 26;
            this.grid1.ButtonClick += new Nistec.GridView.ButtonClickEventHandler(this.grid1_ButtonClick);
            // 
            // CustomerID
            // 
            this.CustomerID.AllowNull = false;
            this.CustomerID.AutoAdjust = true;
            this.CustomerID.DisplayMember = "CompanyName";
            this.CustomerID.Format = "";
            this.CustomerID.HeaderText = "CustomerID";
            this.CustomerID.MappingName = "";
            this.CustomerID.Sorted = true;
            this.CustomerID.ValueMember = "CustomerID";
            this.CustomerID.Width = 148;
            // 
            // OrderID
            // 
            this.OrderID.Format = "";
            this.OrderID.HeaderText = "OrderID";
            this.OrderID.MappingName = "OrderID";
            this.OrderID.Text = "";
            this.OrderID.Width = 75;
            // 
            // OrderDate
            // 
            this.OrderDate.AllowNull = false;
            this.OrderDate.Format = "d";
            this.OrderDate.HeaderText = "OrderDate";
            this.OrderDate.MappingName = "OrderDate";
            this.OrderDate.Width = 94;
            // 
            // ShippedDate
            // 
            this.ShippedDate.AllowNull = false;
            this.ShippedDate.Format = "d";
            this.ShippedDate.HeaderText = "ShippedDate";
            this.ShippedDate.MappingName = "ShippedDate";
            this.ShippedDate.Width = 94;
            // 
            // ShipVia
            // 
            this.ShipVia.AllowNull = false;
            this.ShipVia.DisplayMember = "CompanyName";
            this.ShipVia.Format = "";
            this.ShipVia.HeaderText = "ShipVia";
            this.ShipVia.MappingName = "ShipVia";
            this.ShipVia.Sorted = true;
            this.ShipVia.ValueMember = "ShipperID";
            this.ShipVia.Width = 94;
            // 
            // Freight
            // 
            this.Freight.AllowNull = false;
            this.Freight.Format = "F";
            this.Freight.FormatType = Nistec.Win.Formats.FixNumber;
            this.Freight.HeaderText = "Freight";
            this.Freight.MappingName = "Freight";
            this.Freight.Width = 81;
            // 
            // gridBtn
            // 
            this.gridBtn.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.gridBtn.HeaderText = "CustomerID";
            this.gridBtn.ImageList = null;
            this.gridBtn.MappingName = "CustomerID";
            this.gridBtn.MenuItems.AddRange(new Nistec.WinForms.PopUpItem[] {
            this.popUpItem1,
            this.popUpItem2,
            this.popUpItem3});
            this.gridBtn.Width = 50;
            // 
            // popUpItem1
            // 
            this.popUpItem1.Text = "item1";
            // 
            // popUpItem2
            // 
            this.popUpItem2.Text = "item2";
            // 
            // popUpItem3
            // 
            this.popUpItem3.Text = "item3";
            // 
            // IsShipped
            // 
            this.IsShipped.HeaderText = "IsShipped";
            this.IsShipped.MappingName = "IsShipped";
            //this.IsShipped.NullValue = ((object)(resources.GetObject("IsShipped.NullValue")));
            this.IsShipped.Text = "";
            this.IsShipped.Width = 50;
            // 
            // gridProgressColumn1
            // 
            this.gridProgressColumn1.HeaderText = "Progress";
            this.gridProgressColumn1.MappingName = "Progress";
            this.gridProgressColumn1.NullText = "0";
            this.gridProgressColumn1.Text = "";
            this.gridProgressColumn1.Width = 75;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.tabPage2.Location = new System.Drawing.Point(4, 27);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage2.Size = new System.Drawing.Size(1128, 451);
            this.tabPage2.Text = "Grid Sample 2";
            // 
            // FormTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionVisible = true;
            this.ClientSize = new System.Drawing.Size(1145, 571);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel);
            this.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.Name = "FormTest";
            this.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Text = "Grid Sample";
            this.Controls.SetChildIndex(this.panel, 0);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private Nistec.WinForms.McTabControl tabControl1;
        private Nistec.WinForms.McTabPage tabPage1;
        private Nistec.WinForms.McTabPage tabPage2;
        private System.Windows.Forms.Button button1;
        private Nistec.GridView.GridComboColumn CustomerID;
        private Nistec.GridView.GridDateColumn OrderDate;
        private Nistec.GridView.GridDateColumn ShippedDate;
        private Nistec.GridView.GridComboColumn ShipVia;
        private Nistec.GridView.GridTextColumn Freight; private Nistec.GridView.GridButtonColumn gridBtn;
        private PopUpItem popUpItem1;
        private PopUpItem popUpItem2;
        private PopUpItem popUpItem3;
        private Nistec.GridView.Grid grid1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private Nistec.GridView.GridBoolColumn IsShipped;
        private Nistec.GridView.VGridColumn OrderID;
        private Nistec.GridView.GridProgressColumn gridProgressColumn1;
        private Nistec.WinForms.McComboBox comboBox1;
        private McTextBox mcTextBox1;


    }
}