namespace WinCtlTest
{
    partial class FormMdi
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
            this.ctlMenuBar1 = new Nistec.WinForms.McMenuBar();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.mcToolBarContainer1 = new Nistec.WinForms.McToolBarContainer();
            this.mcToolBar2 = new Nistec.WinForms.McToolBar();
            this.mcToolBar1 = new Nistec.WinForms.McToolBar();
            this.mcToolButton4 = new Nistec.WinForms.McToolButton();
            this.mcToolButton3 = new Nistec.WinForms.McToolButton();
            this.mcToolButton2 = new Nistec.WinForms.McToolButton();
            this.mcToolButton1 = new Nistec.WinForms.McToolButton();
            this.mcMemo1 = new Nistec.WinForms.McMemo();
            this.styleEdit1 = new Nistec.WinForms.StyleEdit(this.components);
            this.mcMultiBox1 = new Nistec.WinForms.McMultiBox();
            this.winFormsEdServiceDialogExampleControl1 = new WinCtlTest.WinFormsEdServiceDialogExampleControl();
            this.mcToolBarContainer1.SuspendLayout();
            this.mcToolBar1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctlMenuBar1
            // 
            this.ctlMenuBar1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1});
            // 
            // menuItem1
            // 
            this.ctlMenuBar1.SetDraw(this.menuItem1, true);
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2,
            this.menuItem3,
            this.menuItem4,
            this.menuItem5,
            this.menuItem6,
            this.menuItem7});
            this.menuItem1.Text = "Main";
            // 
            // menuItem2
            // 
            this.ctlMenuBar1.SetDraw(this.menuItem2, true);
            this.menuItem2.Index = 0;
            this.menuItem2.Text = "New";
            // 
            // menuItem3
            // 
            this.ctlMenuBar1.SetDraw(this.menuItem3, true);
            this.menuItem3.Index = 1;
            this.menuItem3.Text = "Open";
            // 
            // menuItem4
            // 
            this.ctlMenuBar1.SetDraw(this.menuItem4, true);
            this.menuItem4.Index = 2;
            this.menuItem4.Text = "Edit";
            // 
            // menuItem5
            // 
            this.ctlMenuBar1.SetDraw(this.menuItem5, true);
            this.menuItem5.Index = 3;
            this.menuItem5.Text = "Ribbon";
            // 
            // menuItem6
            // 
            this.ctlMenuBar1.SetDraw(this.menuItem6, true);
            this.menuItem6.Index = 4;
            this.menuItem6.Text = "input box";
            // 
            // menuItem7
            // 
            this.ctlMenuBar1.SetDraw(this.menuItem7, true);
            this.menuItem7.Index = 5;
            this.menuItem7.Text = "email";
            // 
            // mcToolBarContainer1
            // 
            this.mcToolBarContainer1.Controls.Add(this.mcToolBar2);
            this.mcToolBarContainer1.Controls.Add(this.mcToolBar1);
            this.mcToolBarContainer1.Dock = System.Windows.Forms.DockStyle.Top;
            this.mcToolBarContainer1.Location = new System.Drawing.Point(0, 0);
            this.mcToolBarContainer1.Name = "mcToolBarContainer1";
            this.mcToolBarContainer1.Padding = new System.Windows.Forms.Padding(2);
            this.mcToolBarContainer1.SelectedToolBar = null;
            this.mcToolBarContainer1.Size = new System.Drawing.Size(676, 32);
            this.mcToolBarContainer1.TabIndex = 0;
            this.mcToolBarContainer1.TabStop = false;
            this.mcToolBarContainer1.Text = "mcToolBarContainer1";
            // 
            // mcToolBar2
            // 
            this.mcToolBar2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mcToolBar2.ControlLayout = Nistec.WinForms.ControlLayout.Visual;
            this.mcToolBar2.FixSize = false;
            this.mcToolBar2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mcToolBar2.Location = new System.Drawing.Point(209, 3);
            this.mcToolBar2.Name = "mcToolBar2";
            this.mcToolBar2.Padding = new System.Windows.Forms.Padding(12, 3, 3, 3);
            this.mcToolBar2.SelectedGroup = -1;
            this.mcToolBar2.Size = new System.Drawing.Size(200, 28);
            this.mcToolBar2.TabIndex = 1;
            // 
            // mcToolBar1
            // 
            this.mcToolBar1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mcToolBar1.ControlLayout = Nistec.WinForms.ControlLayout.Visual;
            this.mcToolBar1.Controls.Add(this.mcToolButton4);
            this.mcToolBar1.Controls.Add(this.mcToolButton3);
            this.mcToolBar1.Controls.Add(this.mcToolButton2);
            this.mcToolBar1.Controls.Add(this.mcToolButton1);
            this.mcToolBar1.FixSize = false;
            this.mcToolBar1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mcToolBar1.Location = new System.Drawing.Point(3, 3);
            this.mcToolBar1.Name = "mcToolBar1";
            this.mcToolBar1.Padding = new System.Windows.Forms.Padding(12, 3, 3, 3);
            this.mcToolBar1.SelectedGroup = 2;
            this.mcToolBar1.Size = new System.Drawing.Size(200, 28);
            this.mcToolBar1.TabIndex = 0;
            // 
            // mcToolButton4
            // 
            this.mcToolButton4.ButtonStyle = Nistec.WinForms.ToolButtonStyle.Button;
            this.mcToolButton4.DialogResult = System.Windows.Forms.DialogResult.None;
            this.mcToolButton4.Dock = System.Windows.Forms.DockStyle.Left;
            this.mcToolButton4.GroupIndex = 2;
            this.mcToolButton4.Location = new System.Drawing.Point(78, 3);
            this.mcToolButton4.Name = "mcToolButton4";
            this.mcToolButton4.Size = new System.Drawing.Size(22, 22);
            this.mcToolButton4.TabIndex = 3;
            this.mcToolButton4.ToolTipText = "";
            this.mcToolButton4.Click += new System.EventHandler(this.mcToolButton4_Click);
            // 
            // mcToolButton3
            // 
            this.mcToolButton3.ButtonStyle = Nistec.WinForms.ToolButtonStyle.Button;
            this.mcToolButton3.DialogResult = System.Windows.Forms.DialogResult.None;
            this.mcToolButton3.Dock = System.Windows.Forms.DockStyle.Left;
            this.mcToolButton3.GroupIndex = 1;
            this.mcToolButton3.Location = new System.Drawing.Point(56, 3);
            this.mcToolButton3.Name = "mcToolButton3";
            this.mcToolButton3.Size = new System.Drawing.Size(22, 22);
            this.mcToolButton3.TabIndex = 2;
            this.mcToolButton3.ToolTipText = "";
            // 
            // mcToolButton2
            // 
            this.mcToolButton2.ButtonStyle = Nistec.WinForms.ToolButtonStyle.Button;
            this.mcToolButton2.DialogResult = System.Windows.Forms.DialogResult.None;
            this.mcToolButton2.Dock = System.Windows.Forms.DockStyle.Left;
            this.mcToolButton2.GroupIndex = 3;
            this.mcToolButton2.Location = new System.Drawing.Point(34, 3);
            this.mcToolButton2.Name = "mcToolButton2";
            this.mcToolButton2.Size = new System.Drawing.Size(22, 22);
            this.mcToolButton2.TabIndex = 1;
            this.mcToolButton2.ToolTipText = "";
            // 
            // mcToolButton1
            // 
            this.mcToolButton1.ButtonStyle = Nistec.WinForms.ToolButtonStyle.Button;
            this.mcToolButton1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.mcToolButton1.Dock = System.Windows.Forms.DockStyle.Left;
            this.mcToolButton1.GroupIndex = 0;
            this.mcToolButton1.Location = new System.Drawing.Point(12, 3);
            this.mcToolButton1.Name = "mcToolButton1";
            this.mcToolButton1.Size = new System.Drawing.Size(22, 22);
            this.mcToolButton1.TabIndex = 0;
            this.mcToolButton1.ToolTipText = "";
            // 
            // mcMemo1
            // 
            this.mcMemo1.BackColor = System.Drawing.Color.White;
            this.mcMemo1.ButtonToolTip = "";
            this.mcMemo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.mcMemo1.ForeColor = System.Drawing.Color.Black;
            this.mcMemo1.Location = new System.Drawing.Point(37, 79);
            this.mcMemo1.Name = "mcMemo1";
            this.mcMemo1.ReadOnly = true;
            this.mcMemo1.Size = new System.Drawing.Size(194, 20);
            this.mcMemo1.StylePainter = this.styleEdit1;
            this.mcMemo1.TabIndex = 1;
            this.mcMemo1.Text = "mcMemo1";
            // 
            // styleEdit1
            // 
            this.styleEdit1.BorderColor = System.Drawing.Color.Goldenrod;
            this.styleEdit1.BorderHotColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.styleEdit1.CaptionFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleEdit1.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.styleEdit1.StylePlan = Nistec.WinForms.Styles.Goldenrod;
            this.styleEdit1.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            // 
            // mcMultiBox1
            // 
            this.mcMultiBox1.ButtonToolTip = "";
            this.mcMultiBox1.Items.Add("sadfasdf");
            this.mcMultiBox1.Items.Add("sadfasdf");
            this.mcMultiBox1.Items.Add("asdfasf");
            this.mcMultiBox1.Location = new System.Drawing.Point(36, 130);
            this.mcMultiBox1.MultiType = Nistec.WinForms.MultiType.Custom;
            this.mcMultiBox1.Name = "mcMultiBox1";
            this.mcMultiBox1.Size = new System.Drawing.Size(258, 20);
            this.mcMultiBox1.TabIndex = 2;
            this.mcMultiBox1.Text = "mcMultiBox1";
            this.mcMultiBox1.ButtonClick += new Nistec.WinForms.ButtonClickEventHandler(this.mcMultiBox1_ButtonClick);
            // 
            // winFormsEdServiceDialogExampleControl1
            // 
            this.winFormsEdServiceDialogExampleControl1.BackColor = System.Drawing.Color.Beige;
            this.winFormsEdServiceDialogExampleControl1.Location = new System.Drawing.Point(36, 167);
            this.winFormsEdServiceDialogExampleControl1.Name = "winFormsEdServiceDialogExampleControl1";
            this.winFormsEdServiceDialogExampleControl1.Size = new System.Drawing.Size(210, 74);
            this.winFormsEdServiceDialogExampleControl1.TabIndex = 3;
            this.winFormsEdServiceDialogExampleControl1.TestDialogString = "Test String";
            // 
            // FormMdi
            // 
            this.ClientSize = new System.Drawing.Size(676, 370);
            this.Controls.Add(this.winFormsEdServiceDialogExampleControl1);
            this.Controls.Add(this.mcMultiBox1);
            this.Controls.Add(this.mcMemo1);
            this.Controls.Add(this.mcToolBarContainer1);
            this.Menu = this.ctlMenuBar1;
            this.Name = "FormMdi";
            this.mcToolBarContainer1.ResumeLayout(false);
            this.mcToolBar1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Nistec.WinForms.McMenuBar ctlMenuBar1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem menuItem6;
        private System.Windows.Forms.MenuItem menuItem7;
        private Nistec.WinForms.McToolBarContainer mcToolBarContainer1;
        private Nistec.WinForms.McToolBar mcToolBar1;
        private Nistec.WinForms.McToolButton mcToolButton1;
        private Nistec.WinForms.McToolBar mcToolBar2;
        private Nistec.WinForms.McToolButton mcToolButton4;
        private Nistec.WinForms.McToolButton mcToolButton3;
        private Nistec.WinForms.McToolButton mcToolButton2;
        private Nistec.WinForms.McMemo mcMemo1;
        private Nistec.WinForms.McMultiBox mcMultiBox1;
        private WinFormsEdServiceDialogExampleControl winFormsEdServiceDialogExampleControl1;
        private Nistec.WinForms.StyleEdit styleEdit1;
    }
}