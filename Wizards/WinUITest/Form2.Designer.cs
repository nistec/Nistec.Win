namespace WizardsTest
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.mcListFinder1 = new MControl.Wizards.McListFinder();
            this.mcTabPanels1 = new MControl.Wizards.McTabPanels();
            this.mcTabTree1 = new MControl.Wizards.McTabTree();
            this.mcTabPage4 = new MControl.WinForms.McTabPage();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.mcTabPage5 = new MControl.WinForms.McTabPage();
            this.mcRichText1 = new MControl.Wizards.McRichText();
            this.mcExplorer1 = new MControl.Wizards.McManagment();
            this.mcTabList1 = new MControl.Wizards.McTabList();
            this.mcTabPage1 = new MControl.WinForms.McTabPage();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.mcTabPage2 = new MControl.WinForms.McTabPage();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.mcTabPage3 = new MControl.WinForms.McTabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.mcTabPage4.SuspendLayout();
            this.mcTabPage1.SuspendLayout();
            this.mcTabPage2.SuspendLayout();
            this.mcTabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "adjust1.gif");
            this.imageList1.Images.SetKeyName(1, "action.gif");
            this.imageList1.Images.SetKeyName(2, "Active.gif");
            this.imageList1.Images.SetKeyName(3, "active-bg.gif");
            this.imageList1.Images.SetKeyName(4, "activity_category.gif");
            this.imageList1.Images.SetKeyName(5, "ActivityPaper.gif");
            this.imageList1.Images.SetKeyName(6, "add_att.gif");
            this.imageList1.Images.SetKeyName(7, "add_co.gif");
            this.imageList1.Images.SetKeyName(8, "add_ov.gif");
            this.imageList1.Images.SetKeyName(9, "add_server.gif");
            this.imageList1.Images.SetKeyName(10, "addres1.gif");
            this.imageList1.Images.SetKeyName(11, "addtsk_tsk.gif");
            // 
            // mcListFinder1
            // 
            this.mcListFinder1.FinderText = "";
            this.mcListFinder1.Location = new System.Drawing.Point(368, 12);
            this.mcListFinder1.Name = "mcListFinder1";
            this.mcListFinder1.OpenAsListOnly = false;
            this.mcListFinder1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.mcListFinder1.SelectionMode = System.Windows.Forms.SelectionMode.One;
            this.mcListFinder1.ShowButtons = true;
            this.mcListFinder1.ShowTitle = true;
            this.mcListFinder1.Size = new System.Drawing.Size(182, 133);
            this.mcListFinder1.TabIndex = 6;
            this.mcListFinder1.TabStop = false;
            this.mcListFinder1.Text = "mcListFinder1";
            this.mcListFinder1.Title = "List Finder Wizard";
            this.mcListFinder1.TitleHeight = 20;
            // 
            // mcTabPanels1
            // 
            this.mcTabPanels1.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.mcTabPanels1.CaptionImage = ((System.Drawing.Image)(resources.GetObject("mcTabPanels1.CaptionImage")));
            this.mcTabPanels1.ListWidth = 120;
            this.mcTabPanels1.Location = new System.Drawing.Point(293, 249);
            this.mcTabPanels1.Name = "mcTabPanels1";
            this.mcTabPanels1.Padding = new System.Windows.Forms.Padding(2);
            this.mcTabPanels1.Size = new System.Drawing.Size(285, 181);
            this.mcTabPanels1.TabIndex = 4;
            this.mcTabPanels1.Text = "mcTabPanels1";
            // 
            // mcTabTree1
            // 
            this.mcTabTree1.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.mcTabTree1.CaptionImage = ((System.Drawing.Image)(resources.GetObject("mcTabTree1.CaptionImage")));
            this.mcTabTree1.ControlLayout = MControl.WinForms.ControlLayout.System;
            this.mcTabTree1.HideTabs = true;
            this.mcTabTree1.ListWidth = 120;
            this.mcTabTree1.Location = new System.Drawing.Point(12, 250);
            this.mcTabTree1.Name = "mcTabTree1";
            this.mcTabTree1.Padding = new System.Windows.Forms.Padding(2);
            this.mcTabTree1.Size = new System.Drawing.Size(275, 180);
            this.mcTabTree1.TabIndex = 3;
            this.mcTabTree1.Text = "mcTabTree1";
            this.mcTabTree1.WizardPages.AddRange(new MControl.WinForms.McTabPage[] {
            this.mcTabPage4,
            this.mcTabPage5});
            // 
            // mcTabPage4
            // 
            this.mcTabPage4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.mcTabPage4.Controls.Add(this.dateTimePicker2);
            this.mcTabPage4.Location = new System.Drawing.Point(4, 4);
            this.mcTabPage4.Name = "mcTabPage4";
            this.mcTabPage4.Size = new System.Drawing.Size(144, 77);
            this.mcTabPage4.Text = "mcTabPage4";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(17, 16);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(74, 20);
            this.dateTimePicker2.TabIndex = 1;
            // 
            // mcTabPage5
            // 
            this.mcTabPage5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.mcTabPage5.Location = new System.Drawing.Point(4, 4);
            this.mcTabPage5.Name = "mcTabPage5";
            this.mcTabPage5.Size = new System.Drawing.Size(144, 77);
            this.mcTabPage5.Text = "mcTabPage5";
            // 
            // mcRichText1
            // 
            this.mcRichText1.BindFormat = MControl.WinForms.BindingFormat.String;
            this.mcRichText1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mcRichText1.DefaultValue = null;
            this.mcRichText1.FixSize = false;
            this.mcRichText1.Location = new System.Drawing.Point(149, 168);
            this.mcRichText1.Name = "mcRichText1";
            this.mcRichText1.Padding = new System.Windows.Forms.Padding(2);
            this.mcRichText1.ShowColors = true;
            this.mcRichText1.ShowOpen = true;
            this.mcRichText1.ShowRedo = true;
            this.mcRichText1.ShowSave = true;
            this.mcRichText1.ShowStamp = true;
            this.mcRichText1.ShowUndo = true;
            this.mcRichText1.Size = new System.Drawing.Size(401, 68);
            this.mcRichText1.TabIndex = 2;
            this.mcRichText1.TabStop = false;
            // 
            // mcExplorer1
            // 
            this.mcExplorer1.CaptionFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mcExplorer1.CaptionImage = ((System.Drawing.Image)(resources.GetObject("mcExplorer1.CaptionImage")));
            this.mcExplorer1.CaptionSubText = "";
            this.mcExplorer1.HideTabs = true;
            this.mcExplorer1.ListCaption = "";
            this.mcExplorer1.ListCaptionVisible = true;
            this.mcExplorer1.ListWidth = 120;
            this.mcExplorer1.Location = new System.Drawing.Point(198, 12);
            this.mcExplorer1.Name = "mcExplorer1";
            this.mcExplorer1.Padding = new System.Windows.Forms.Padding(2);
            this.mcExplorer1.SelectedIndex = -1;
            this.mcExplorer1.Size = new System.Drawing.Size(141, 114);
            this.mcExplorer1.TabIndex = 1;
            this.mcExplorer1.Text = "mcExplorer1";
            // 
            // mcTabList1
            // 
            this.mcTabList1.ControlLayout = MControl.WinForms.ControlLayout.System;
            this.mcTabList1.ImageList = this.imageList1;
            this.mcTabList1.ListWidth = 110;
            this.mcTabList1.Location = new System.Drawing.Point(12, 12);
            this.mcTabList1.Name = "mcTabList1";
            this.mcTabList1.Padding = new System.Windows.Forms.Padding(2);
            this.mcTabList1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.mcTabList1.Size = new System.Drawing.Size(180, 131);
            this.mcTabList1.TabIndex = 0;
            this.mcTabList1.TabPages.AddRange(new MControl.WinForms.McTabPage[] {
            this.mcTabPage1,
            this.mcTabPage2,
            this.mcTabPage3});
            // 
            // mcTabPage1
            // 
            this.mcTabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.mcTabPage1.Controls.Add(this.dateTimePicker1);
            this.mcTabPage1.ImageIndex = 1;
            this.mcTabPage1.ImageList = this.imageList1;
            this.mcTabPage1.Location = new System.Drawing.Point(4, 4);
            this.mcTabPage1.Name = "mcTabPage1";
            this.mcTabPage1.Size = new System.Drawing.Size(59, 124);
            this.mcTabPage1.Text = "mcTabPage1";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(27, 32);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(156, 20);
            this.dateTimePicker1.TabIndex = 0;
            // 
            // mcTabPage2
            // 
            this.mcTabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.mcTabPage2.Controls.Add(this.checkedListBox1);
            this.mcTabPage2.ImageIndex = 2;
            this.mcTabPage2.ImageList = this.imageList1;
            this.mcTabPage2.Location = new System.Drawing.Point(4, 4);
            this.mcTabPage2.Name = "mcTabPage2";
            this.mcTabPage2.Size = new System.Drawing.Size(59, 124);
            this.mcTabPage2.Text = "mcTabPage2";
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(32, 35);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(127, 109);
            this.checkedListBox1.TabIndex = 0;
            // 
            // mcTabPage3
            // 
            this.mcTabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.mcTabPage3.Controls.Add(this.button1);
            this.mcTabPage3.ImageIndex = 4;
            this.mcTabPage3.ImageList = this.imageList1;
            this.mcTabPage3.Location = new System.Drawing.Point(4, 4);
            this.mcTabPage3.Name = "mcTabPage3";
            this.mcTabPage3.Size = new System.Drawing.Size(59, 124);
            this.mcTabPage3.Text = "mcTabPage3";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(39, 51);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(145, 44);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 442);
            this.Controls.Add(this.mcListFinder1);
            this.Controls.Add(this.mcTabPanels1);
            this.Controls.Add(this.mcTabTree1);
            this.Controls.Add(this.mcRichText1);
            this.Controls.Add(this.mcExplorer1);
            this.Controls.Add(this.mcTabList1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.mcTabPage4.ResumeLayout(false);
            this.mcTabPage1.ResumeLayout(false);
            this.mcTabPage2.ResumeLayout(false);
            this.mcTabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MControl.Wizards.McTabList mcTabList1;
        private MControl.WinForms.McTabPage mcTabPage1;
        private MControl.WinForms.McTabPage mcTabPage2;
        private MControl.WinForms.McTabPage mcTabPage3;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Button button1;
        private MControl.Wizards.McManagment mcExplorer1;
        private MControl.Wizards.McRichText mcRichText1;
        private MControl.Wizards.McTabTree mcTabTree1;
        private MControl.Wizards.McTabPanels mcTabPanels1;
        private MControl.Wizards.McListFinder mcListFinder1;
        private MControl.WinForms.McTabPage mcTabPage4;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private MControl.WinForms.McTabPage mcTabPage5;
    }
}