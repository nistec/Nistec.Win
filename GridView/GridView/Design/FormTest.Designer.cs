namespace mControl.GridStyle.Design
{
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
            this.grid1 = new mControl.GridView.Grid();
            this.gridTextColumn1 = new mControl.GridView.GridTextColumn();
            this.gridTextColumn2 = new mControl.GridView.GridTextColumn();
            this.gridTextColumn3 = new mControl.GridView.GridTextColumn();
            this.gridTextColumn4 = new mControl.GridView.GridTextColumn();
            this.gridTextColumn5 = new mControl.GridView.GridTextColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).BeginInit();
            this.SuspendLayout();
            // 
            // grid1
            // 
            this.grid1.BackColor = System.Drawing.Color.White;
            this.grid1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grid1.Columns.AddRange(new mControl.GridView.GridColumnStyle[] {
            this.gridTextColumn1,
            this.gridTextColumn2,
            this.gridTextColumn3,
            this.gridTextColumn4,
            this.gridTextColumn5});
            this.grid1.DataMember = "";
            this.grid1.ForeColor = System.Drawing.Color.Black;
            this.grid1.GridLineStyle = mControl.GridView.GridLineStyle.Solid;
            this.grid1.Location = new System.Drawing.Point(32, 22);
            this.grid1.MappingName = null;
            this.grid1.Name = "grid1";
            this.grid1.PaintAlternating = false;
            this.grid1.Size = new System.Drawing.Size(458, 276);
            this.grid1.TabIndex = 0;
            // 
            // gridTextColumn1
            // 
            this.gridTextColumn1.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.gridTextColumn1.Format = "";
            this.gridTextColumn1.HeaderText = "aaaa";
            this.gridTextColumn1.MappingName = "";
            this.gridTextColumn1.Width = 75;
            // 
            // gridTextColumn2
            // 
            this.gridTextColumn2.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.gridTextColumn2.Format = "";
            this.gridTextColumn2.HeaderText = "2222";
            this.gridTextColumn2.MappingName = "";
            this.gridTextColumn2.Width = 75;
            // 
            // gridTextColumn3
            // 
            this.gridTextColumn3.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.gridTextColumn3.Format = "";
            this.gridTextColumn3.HeaderText = "cccc";
            this.gridTextColumn3.MappingName = "";
            this.gridTextColumn3.Width = 75;
            // 
            // gridTextColumn4
            // 
            this.gridTextColumn4.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.gridTextColumn4.Format = "";
            this.gridTextColumn4.HeaderText = "dddd";
            this.gridTextColumn4.MappingName = "";
            this.gridTextColumn4.Width = 75;
            // 
            // gridTextColumn5
            // 
            this.gridTextColumn5.Alignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.gridTextColumn5.Format = "";
            this.gridTextColumn5.HeaderText = "eeee";
            this.gridTextColumn5.MappingName = "";
            this.gridTextColumn5.Width = 75;
            // 
            // FormTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 356);
            this.Controls.Add(this.grid1);
            this.Name = "FormTest";
            this.Text = "FormTest";
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private mControl.GridView.Grid grid1;
        private mControl.GridView.GridTextColumn gridTextColumn1;
        private mControl.GridView.GridTextColumn gridTextColumn2;
        private mControl.GridView.GridTextColumn gridTextColumn3;
        private mControl.GridView.GridTextColumn gridTextColumn4;
        private mControl.GridView.GridTextColumn gridTextColumn5;

     }
}