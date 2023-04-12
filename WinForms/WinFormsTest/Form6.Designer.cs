namespace WinCtlTest
{
    partial class Form6
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form6));
            this.mcMultiBox1 = new Nistec.WinForms.McMultiBox();
            this.mcButton1 = new Nistec.WinForms.McButton();
            this.mcButton2 = new Nistec.WinForms.McButton();
            this.mcButton3 = new Nistec.WinForms.McButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.mcComboBox1 = new Nistec.WinForms.McComboBox();
            this.mcDatePicker1 = new Nistec.WinForms.McDatePicker();
            this.mcCaption1 = new Nistec.WinForms.McCaption();
            this.SuspendLayout();
            // 
            // StyleGuideBase
            // 
            this.StyleGuideBase.AlternatingColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(243)))), ((int)(((byte)(213)))));
            this.StyleGuideBase.BorderColor = System.Drawing.Color.Goldenrod;
            this.StyleGuideBase.BorderHotColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.StyleGuideBase.ButtonBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.StyleGuideBase.CaptionColor = System.Drawing.Color.DarkGoldenrod;
            this.StyleGuideBase.ColorBrush1 = System.Drawing.Color.PaleGoldenrod;
            this.StyleGuideBase.ColorBrush2 = System.Drawing.Color.Ivory;
            this.StyleGuideBase.ColorBrushLower = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(206)))), ((int)(((byte)(103)))));
            this.StyleGuideBase.ColorBrushUpper = System.Drawing.Color.Ivory;
            this.StyleGuideBase.BackgroundColor = System.Drawing.Color.Ivory;
            this.StyleGuideBase.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.StyleGuideBase.FormColor = System.Drawing.Color.Ivory;
            this.StyleGuideBase.StylePlan = Nistec.WinForms.Styles.Goldenrod;
            // 
            // mcMultiBox1
            // 
            this.mcMultiBox1.ButtonToolTip = "Custom";
            this.mcMultiBox1.Filter = "DB files (*.mdb)|*.mdb|All files (*.*)|*.*";
            this.mcMultiBox1.Location = new System.Drawing.Point(30, 236);
            this.mcMultiBox1.MultiType = Nistec.WinForms.MultiType.Brows;
            this.mcMultiBox1.Name = "mcMultiBox1";
            this.mcMultiBox1.Size = new System.Drawing.Size(136, 20);
            this.mcMultiBox1.TabIndex = 0;
            this.mcMultiBox1.Text = "mcMultiBox1";
            this.mcMultiBox1.ButtonClick += new Nistec.WinForms.ButtonClickEventHandler(this.mcMultiBox1_ButtonClick);
            // 
            // mcButton1
            // 
            this.mcButton1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.mcButton1.Location = new System.Drawing.Point(30, 107);
            this.mcButton1.Name = "mcButton1";
            this.mcButton1.Size = new System.Drawing.Size(82, 23);
            this.mcButton1.TabIndex = 10000;
            this.mcButton1.Text = "mcButton1";
            this.mcButton1.ToolTipText = "mcButton1";
            this.mcButton1.Click += new System.EventHandler(this.mcButton1_Click);
            // 
            // mcButton2
            // 
            this.mcButton2.DialogResult = System.Windows.Forms.DialogResult.None;
            this.mcButton2.Location = new System.Drawing.Point(127, 107);
            this.mcButton2.Name = "mcButton2";
            this.mcButton2.Size = new System.Drawing.Size(82, 23);
            this.mcButton2.TabIndex = 10001;
            this.mcButton2.Text = "mcButton2";
            this.mcButton2.ToolTipText = "mcButton1";
            this.mcButton2.Click += new System.EventHandler(this.mcButton2_Click);
            // 
            // mcButton3
            // 
            this.mcButton3.DialogResult = System.Windows.Forms.DialogResult.None;
            this.mcButton3.Location = new System.Drawing.Point(236, 107);
            this.mcButton3.Name = "mcButton3";
            this.mcButton3.Size = new System.Drawing.Size(82, 23);
            this.mcButton3.TabIndex = 10002;
            this.mcButton3.Text = "mcButton3";
            this.mcButton3.ToolTipText = "mcButton1";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "adjust1.gif");
            this.imageList1.Images.SetKeyName(1, "100.gif");
            this.imageList1.Images.SetKeyName(2, "about.gif");
            this.imageList1.Images.SetKeyName(3, "acrobat.gif");
            this.imageList1.Images.SetKeyName(4, "action.gif");
            this.imageList1.Images.SetKeyName(5, "Active.gif");
            this.imageList1.Images.SetKeyName(6, "active-bg.gif");
            this.imageList1.Images.SetKeyName(7, "activity_category.gif");
            this.imageList1.Images.SetKeyName(8, "ActivityPaper.gif");
            this.imageList1.Images.SetKeyName(9, "add_att.gif");
            this.imageList1.Images.SetKeyName(10, "add_co.gif");
            this.imageList1.Images.SetKeyName(11, "add_ov.gif");
            this.imageList1.Images.SetKeyName(12, "add_server.gif");
            this.imageList1.Images.SetKeyName(13, "addres1.gif");
            this.imageList1.Images.SetKeyName(14, "addtsk_tsk.gif");
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(30, 147);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(179, 20);
            this.textBox1.TabIndex = 10003;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // mcComboBox1
            // 
            this.mcComboBox1.ButtonToolTip = "";
            this.mcComboBox1.IntegralHeight = false;
            this.mcComboBox1.Items.AddRange(new object[] {
            "asdasd",
            "asdasda",
            "dasdads",
            "asdasd",
            "asdads"});
            this.mcComboBox1.Location = new System.Drawing.Point(30, 173);
            this.mcComboBox1.Name = "mcComboBox1";
            this.mcComboBox1.Size = new System.Drawing.Size(136, 20);
            this.mcComboBox1.TabIndex = 10004;
            this.mcComboBox1.Text = "mcComboBox1";
            // 
            // mcDatePicker1
            // 
            this.mcDatePicker1.ButtonToolTip = "";
            this.mcDatePicker1.Format = "d";
            this.mcDatePicker1.InputMask = "00/00/0000";
            this.mcDatePicker1.Location = new System.Drawing.Point(30, 199);
            this.mcDatePicker1.MaxValue = new System.DateTime(2999, 12, 31, 0, 0, 0, 0);
            this.mcDatePicker1.MinValue = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.mcDatePicker1.Name = "mcDatePicker1";
            this.mcDatePicker1.Size = new System.Drawing.Size(136, 20);
            this.mcDatePicker1.TabIndex = 10005;
            this.mcDatePicker1.Value = new System.DateTime(2008, 10, 21, 7, 37, 42, 31);
            // 
            // mcCaption1
            // 
            this.mcCaption1.BackColor = System.Drawing.Color.Transparent;
            this.mcCaption1.ControlLayout = Nistec.WinForms.ControlLayout.Visual;
            this.mcCaption1.Dock = System.Windows.Forms.DockStyle.Top;
            this.mcCaption1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mcCaption1.Image = ((System.Drawing.Image)(resources.GetObject("mcCaption1.Image")));
            this.mcCaption1.Location = new System.Drawing.Point(2, 38);
            this.mcCaption1.Name = "mcCaption1";
            this.mcCaption1.SubText = "xvxcvxcvxcvxcvxcvxv";
            this.mcCaption1.Text = "mcCaption1";
            // 
            // Form6
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionVisible = true;
            this.ClientSize = new System.Drawing.Size(581, 316);
            this.Controls.Add(this.mcCaption1);
            this.Controls.Add(this.mcDatePicker1);
            this.Controls.Add(this.mcComboBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.mcButton3);
            this.Controls.Add(this.mcButton2);
            this.Controls.Add(this.mcMultiBox1);
            this.Controls.Add(this.mcButton1);
            this.Name = "Form6";
            this.Text = "Form6wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww";
            this.Controls.SetChildIndex(this.mcButton1, 0);
            this.Controls.SetChildIndex(this.mcMultiBox1, 0);
            this.Controls.SetChildIndex(this.mcButton2, 0);
            this.Controls.SetChildIndex(this.mcButton3, 0);
            this.Controls.SetChildIndex(this.textBox1, 0);
            this.Controls.SetChildIndex(this.mcComboBox1, 0);
            this.Controls.SetChildIndex(this.mcDatePicker1, 0);
            this.Controls.SetChildIndex(this.mcCaption1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Nistec.WinForms.McMultiBox mcMultiBox1;
        private Nistec.WinForms.McButton mcButton1;
        private Nistec.WinForms.McButton mcButton2;
        private Nistec.WinForms.McButton mcButton3;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TextBox textBox1;
        private Nistec.WinForms.McComboBox mcComboBox1;
        private Nistec.WinForms.McDatePicker mcDatePicker1;
        private Nistec.WinForms.McCaption mcCaption1;
    }
}