using Nistec.Win;
namespace WinCtlTest
{
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
            this.ctlTabControl1 = new Nistec.WinForms.McTabControl();
            this.ctlTabPage1 = new Nistec.WinForms.McTabPage();
            this.mcSpinEdit1 = new Nistec.WinForms.McSpinEdit();
            this.ctlTabPage2 = new Nistec.WinForms.McTabPage();
            this.mcNavBar1 = new Nistec.WinForms.McNavBar();
            this.mcSpinEdit2 = new Nistec.WinForms.McSpinEdit();
            this.mcButton1 = new Nistec.WinForms.McButton();
            this.ctlTabControl1.SuspendLayout();
            this.ctlTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mcNavBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // StyleGuideBase
            // 
            this.StyleGuideBase.AlternatingColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(230)))), ((int)(((byte)(220)))));
            this.StyleGuideBase.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(106)))), ((int)(((byte)(197)))));
            this.StyleGuideBase.BorderHotColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(80)))), ((int)(((byte)(184)))));
            this.StyleGuideBase.CaptionColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(126)))), ((int)(((byte)(177)))));
            this.StyleGuideBase.ColorBrush1 = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(203)))), ((int)(((byte)(183)))));
            this.StyleGuideBase.ColorBrush2 = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.FocusedColor = System.Drawing.Color.RoyalBlue;
            this.StyleGuideBase.FormColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.StylePlan = Nistec.WinForms.Styles.Desktop;
            // 
            // ctlTabControl1
            // 
            this.ctlTabControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlTabControl1.ControlLayout = Nistec.WinForms.ControlLayout.VistaLayout;
            this.ctlTabControl1.Controls.Add(this.ctlTabPage1);
            this.ctlTabControl1.Controls.Add(this.ctlTabPage2);
            this.ctlTabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ctlTabControl1.ItemSize = new System.Drawing.Size(0, 20);
            this.ctlTabControl1.Location = new System.Drawing.Point(94, 135);
            this.ctlTabControl1.Name = "ctlTabControl1";
            this.ctlTabControl1.Size = new System.Drawing.Size(376, 197);
            this.ctlTabControl1.StylePainter = this.StyleGuideBase;
            this.ctlTabControl1.TabIndex = 12;
            this.ctlTabControl1.TabPages.AddRange(new Nistec.WinForms.McTabPage[] {
            this.ctlTabPage1,
            this.ctlTabPage2});
            this.ctlTabControl1.TabStop = false;
            this.ctlTabControl1.Text = "ctlTabControl1";
            // 
            // ctlTabPage1
            // 
            this.ctlTabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlTabPage1.Controls.Add(this.mcSpinEdit1);
            this.ctlTabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ctlTabPage1.Location = new System.Drawing.Point(4, 27);
            this.ctlTabPage1.Name = "ctlTabPage1";
            this.ctlTabPage1.Size = new System.Drawing.Size(367, 165);
            this.ctlTabPage1.StylePainter = this.StyleGuideBase;
            this.ctlTabPage1.Text = "ctlTabPage1";
            // 
            // mcSpinEdit1
            // 
            this.mcSpinEdit1.BackColor = System.Drawing.Color.White;
            this.mcSpinEdit1.ButtonAlign = Nistec.WinForms.ButtonAlign.Right;
            this.mcSpinEdit1.ControlLayout = Nistec.WinForms.ControlLayout.VistaLayout;
            this.mcNavBar1.SetDataField(this.mcSpinEdit1, "");
            this.mcSpinEdit1.DecimalPlaces = 0;
            this.mcSpinEdit1.DefaultValue = "";
            this.mcSpinEdit1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.mcSpinEdit1.ForeColor = System.Drawing.Color.Black;
            this.mcSpinEdit1.Format = "N";
            this.mcSpinEdit1.FormatType = NumberFormats.StandadNumber;
            this.mcSpinEdit1.Location = new System.Drawing.Point(26, 28);
            this.mcSpinEdit1.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.mcSpinEdit1.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.mcSpinEdit1.Name = "mcSpinEdit1";
            this.mcSpinEdit1.Size = new System.Drawing.Size(130, 20);
            this.mcSpinEdit1.StylePainter = this.StyleGuideBase;
            this.mcSpinEdit1.TabIndex = 0;
            this.mcSpinEdit1.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // ctlTabPage2
            // 
            this.ctlTabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlTabPage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ctlTabPage2.Location = new System.Drawing.Point(4, 27);
            this.ctlTabPage2.Name = "ctlTabPage2";
            this.ctlTabPage2.Size = new System.Drawing.Size(367, 165);
            this.ctlTabPage2.StylePainter = this.StyleGuideBase;
            this.ctlTabPage2.Text = "ctlTabPage2";
            // 
            // mcNavBar1
            // 
            this.mcNavBar1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mcNavBar1.Connection = null;
            this.mcNavBar1.ControlLayout = Nistec.WinForms.ControlLayout.VistaLayout;
            this.mcNavBar1.DBProvider = Nistec.Data.DBProvider.OleDb;
            this.mcNavBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.mcNavBar1.EnableButtons = false;
            this.mcNavBar1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.mcNavBar1.Location = new System.Drawing.Point(2, 391);
            this.mcNavBar1.ManagerDataAdapter = null;
            this.mcNavBar1.MessageDelete = "Delete ?";
            this.mcNavBar1.MessageSaveChanges = "Save Changes ?";
            this.mcNavBar1.Name = "mcNavBar1";
            this.mcNavBar1.Padding = new System.Windows.Forms.Padding(2);
            this.mcNavBar1.ShowChangesButtons = false;
            this.mcNavBar1.Size = new System.Drawing.Size(596, 28);
            this.mcNavBar1.SizingGrip = false;
            this.mcNavBar1.StylePainter = this.StyleGuideBase;
            this.mcNavBar1.TabIndex = 13;
            // 
            // mcSpinEdit2
            // 
            this.mcSpinEdit2.BackColor = System.Drawing.Color.White;
            this.mcSpinEdit2.ButtonAlign = Nistec.WinForms.ButtonAlign.Right;
            this.mcSpinEdit2.ControlLayout = Nistec.WinForms.ControlLayout.VistaLayout;
            this.mcNavBar1.SetDataField(this.mcSpinEdit2, "");
            this.mcSpinEdit2.DecimalPlaces = 0;
            this.mcSpinEdit2.DefaultValue = "";
            this.mcSpinEdit2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.mcSpinEdit2.ForeColor = System.Drawing.Color.Black;
            this.mcSpinEdit2.Format = "N";
            this.mcSpinEdit2.FormatType = NumberFormats.StandadNumber;
            this.mcSpinEdit2.Location = new System.Drawing.Point(72, 53);
            this.mcSpinEdit2.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.mcSpinEdit2.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.mcSpinEdit2.Name = "mcSpinEdit2";
            this.mcSpinEdit2.Size = new System.Drawing.Size(130, 20);
            this.mcSpinEdit2.StylePainter = this.StyleGuideBase;
            this.mcSpinEdit2.TabIndex = 10000;
            this.mcSpinEdit2.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // mcButton1
            // 
            this.mcButton1.ControlLayout = Nistec.WinForms.ControlLayout.VistaLayout;
            this.mcButton1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.mcButton1.Location = new System.Drawing.Point(244, 53);
            this.mcButton1.Name = "mcButton1";
            this.mcButton1.Size = new System.Drawing.Size(97, 25);
            this.mcButton1.StylePainter = this.StyleGuideBase;
            this.mcButton1.TabIndex = 10001;
            this.mcButton1.Text = "mcButton1";
            this.mcButton1.ToolTipText = "mcButton1";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionVisible = true;
            this.ClientSize = new System.Drawing.Size(600, 421);
            this.Controls.Add(this.mcButton1);
            this.Controls.Add(this.mcSpinEdit2);
            this.Controls.Add(this.mcNavBar1);
            this.Controls.Add(this.ctlTabControl1);
            this.Name = "Form2";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Form2";
            this.Controls.SetChildIndex(this.ctlTabControl1, 0);
            this.Controls.SetChildIndex(this.mcNavBar1, 0);
            this.Controls.SetChildIndex(this.mcSpinEdit2, 0);
            this.Controls.SetChildIndex(this.mcButton1, 0);
            this.ctlTabControl1.ResumeLayout(false);
            this.ctlTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mcNavBar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Nistec.WinForms.McTabControl ctlTabControl1;
        private Nistec.WinForms.McTabPage ctlTabPage1;
        private Nistec.WinForms.McTabPage ctlTabPage2;
        private Nistec.WinForms.McSpinEdit mcSpinEdit1;
        private Nistec.WinForms.McNavBar mcNavBar1;
        private Nistec.WinForms.McSpinEdit mcSpinEdit2;
        private Nistec.WinForms.McButton mcButton1;
    }
}