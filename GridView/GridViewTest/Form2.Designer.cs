namespace GridViewTest
{
    using Nistec.WinForms;
    using Nistec.Win;

    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new Nistec.WinForms.McComboBox();
            this.gridNumericColumn1 = new Nistec.GridView.GridNumericColumn();
            this.gridMultiColumn1 = new Nistec.GridView.GridMultiColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).BeginInit();
            this.SuspendLayout();
            // 
            // grid1
            // 
            this.grid1.AutoAdjust = true;
            this.grid1.BackColor = System.Drawing.Color.White;
            this.grid1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.grid1.CaptionText = "abc";
            this.grid1.Columns.AddRange(new Nistec.GridView.GridColumnStyle[] {
            this.CustomerID,
            this.OrderID,
            this.OrderDate,
            this.ShippedDate,
            this.ShipVia,
            this.Freight,
            this.gridBtn,
            this.IsShipped,
            this.gridProgressColumn1,
            this.gridNumericColumn1,
            this.gridMultiColumn1});
            this.grid1.ControlLayout = Nistec.WinForms.ControlLayout.XpLayout;
            this.grid1.DataMember = "";
            this.grid1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.grid1.ForeColor = System.Drawing.Color.Black;
            this.grid1.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.grid1.Location = new System.Drawing.Point(12, 41);
            this.grid1.Name = "grid1";
            this.grid1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.grid1.Size = new System.Drawing.Size(741, 287);
            this.grid1.StatusBarVisible = true;
            this.grid1.TabIndex = 26;
            this.grid1.ButtonClick += new Nistec.GridView.ButtonClickEventHandler(this.grid1_ButtonClick);
            // 
            // CustomerID
            // 
            this.CustomerID.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
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
            this.OrderID.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.OrderID.Format = "";
            this.OrderID.HeaderText = "OrderID";
            this.OrderID.MappingName = "OrderID";
            this.OrderID.Text = "";
            this.OrderID.Width = 75;
            // 
            // OrderDate
            // 
            this.OrderDate.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.OrderDate.AllowNull = false;
            this.OrderDate.Format = "d";
            this.OrderDate.HeaderText = "OrderDate";
            this.OrderDate.MappingName = "OrderDate";
            this.OrderDate.Width = 94;
            // 
            // ShippedDate
            // 
            this.ShippedDate.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.ShippedDate.AllowNull = false;
            this.ShippedDate.Format = "d";
            this.ShippedDate.HeaderText = "ShippedDate";
            this.ShippedDate.MappingName = "ShippedDate";
            this.ShippedDate.Width = 94;
            // 
            // ShipVia
            // 
            this.ShipVia.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
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
            this.Freight.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.Freight.AllowNull = false;
            this.Freight.Format = "F";
            this.Freight.FormatType = Formats.FixNumber;
            this.Freight.HeaderText = "Freight";
            this.Freight.MappingName = "";
            this.Freight.Width = 81;
            // 
            // gridBtn
            // 
            this.gridBtn.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.gridBtn.HeaderText = "button";
            this.gridBtn.ImageList = null;
            this.gridBtn.IsBound = false;
            this.gridBtn.MappingName = "";
            this.gridBtn.MenuItems.AddRange(new Nistec.WinForms.PopUpItem[] {
            this.popUpItem1,
            this.popUpItem2,
            this.popUpItem3});
            this.gridBtn.Text = "ok";
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
            this.IsShipped.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.IsShipped.HeaderText = "IsShipped";
            this.IsShipped.MappingName = "IsShipped";
            //this.IsShipped.NullValue = ((object)(resources.GetObject("IsShipped.NullValue")));
            this.IsShipped.Text = "";
            this.IsShipped.Width = 50;
            // 
            // gridProgressColumn1
            // 
            this.gridProgressColumn1.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.gridProgressColumn1.HeaderText = "Progress";
            this.gridProgressColumn1.MappingName = "Progress";
            this.gridProgressColumn1.NullText = "0";
            this.gridProgressColumn1.Text = "";
            this.gridProgressColumn1.Width = 75;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Load";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(107, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Load";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(202, 18);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 17);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "Staus Bar";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(289, 18);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(72, 17);
            this.checkBox2.TabIndex = 4;
            this.checkBox2.Text = "Staus Bar";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.ButtonToolTip = "";
            this.comboBox1.IntegralHeight = false;
            this.comboBox1.Location = new System.Drawing.Point(416, 14);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 5;
            // 
            // gridNumericColumn1
            // 
            this.gridNumericColumn1.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.gridNumericColumn1.DecimalPlaces = 0;
            this.gridNumericColumn1.Format = "";
            this.gridNumericColumn1.HeaderText = "Freight";
            this.gridNumericColumn1.MappingName = "Freight";
            this.gridNumericColumn1.MaxValue = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.gridNumericColumn1.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.gridNumericColumn1.NullText = "0";
            this.gridNumericColumn1.Width = 75;
            // 
            // gridMultiColumn1
            // 
            this.gridMultiColumn1.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.gridMultiColumn1.Format = "";
            this.gridMultiColumn1.MappingName = "CustomerID";
            this.gridMultiColumn1.MultiType = Nistec.WinForms.MultiType.Custom;
            this.gridMultiColumn1.Width = 75;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 395);
            this.Controls.Add(this.grid1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form2";
            this.Text = "FormTest";
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

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
        private Nistec.GridView.GridNumericColumn gridNumericColumn1;
        private Nistec.GridView.GridMultiColumn gridMultiColumn1;


    }
}