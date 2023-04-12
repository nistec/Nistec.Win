namespace MControl.Wizards.Permissions
{
    partial class UsersForm
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
            this.ctlPassword = new MControl.WinForms.McTextBox();
            this.lblPassword = new MControl.WinForms.McLabel();
            this.ctlLogInName = new MControl.WinForms.McTextBox();
            this.lblLogInName = new MControl.WinForms.McLabel();
            this.ctlDetails = new MControl.WinForms.McTextBox();
            this.lblDetails = new MControl.WinForms.McLabel();
            this.ctlPermsGroup = new MControl.WinForms.McComboBox();
            this.lblPermsGroup = new MControl.WinForms.McLabel();
            this.ctlUserName = new MControl.WinForms.McTextBox();
            this.lblUserName = new MControl.WinForms.McLabel();
            this.ctlAccountId = new MControl.WinForms.McTextBox();
            this.ctlLabel1 = new MControl.WinForms.McLabel();
            this.ctlLang = new MControl.WinForms.McTextBox();
            this.ctlLabel2 = new MControl.WinForms.McLabel();
            this.ctlUserID = new MControl.WinForms.McTextBox();
            this.ctlLabel3 = new MControl.WinForms.McLabel();
            this.ctlMailAddress = new MControl.WinForms.McTextBox();
            this.ctlLabel4 = new MControl.WinForms.McLabel();
            this.ctlPhone = new MControl.WinForms.McTextBox();
            this.ctlLabel5 = new MControl.WinForms.McLabel();
            this.ctlPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctlNavBar)).BeginInit();
            this.SuspendLayout();
            // 
            // ctlList
            // 
            this.ctlList.BackColor = System.Drawing.Color.White;
            this.ctlNavBar.SetDataField(this.ctlList, "");
            this.ctlList.Dock = System.Windows.Forms.DockStyle.Left;
            this.ctlList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.ctlList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ctlList.ForeColor = System.Drawing.Color.Black;
            this.ctlList.ItemHeight = 18;
            this.ctlList.Location = new System.Drawing.Point(2, 66);
            this.ctlList.Name = "ctlList";
            this.ctlList.ReadOnly = false;
            this.ctlList.Size = new System.Drawing.Size(180, 312);
            this.ctlList.StylePainter = this.StyleGuideBase;
            this.ctlList.TabIndex = 7;
            // 
            // ctlSplitter1
            // 
            this.ctlSplitter1.BackColor = System.Drawing.Color.White;
            this.ctlSplitter1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ctlSplitter1.ForeColor = System.Drawing.Color.Black;
            this.ctlSplitter1.Location = new System.Drawing.Point(182, 66);
            this.ctlSplitter1.Name = "ctlSplitter1";
            this.ctlSplitter1.Size = new System.Drawing.Size(4, 312);
            this.ctlSplitter1.StylePainter = this.StyleGuideBase;
            this.ctlSplitter1.TabIndex = 8;
            this.ctlSplitter1.TabStop = false;
            // 
            // ctlPanel1
            // 
            this.ctlPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlPanel1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ctlPanel1.Controls.Add(this.ctlPhone);
            this.ctlPanel1.Controls.Add(this.ctlLabel5);
            this.ctlPanel1.Controls.Add(this.ctlMailAddress);
            this.ctlPanel1.Controls.Add(this.ctlLabel4);
            this.ctlPanel1.Controls.Add(this.ctlUserID);
            this.ctlPanel1.Controls.Add(this.ctlLabel3);
            this.ctlPanel1.Controls.Add(this.ctlLang);
            this.ctlPanel1.Controls.Add(this.ctlLabel2);
            this.ctlPanel1.Controls.Add(this.ctlAccountId);
            this.ctlPanel1.Controls.Add(this.ctlLabel1);
            this.ctlPanel1.Controls.Add(this.ctlPassword);
            this.ctlPanel1.Controls.Add(this.lblPassword);
            this.ctlPanel1.Controls.Add(this.ctlLogInName);
            this.ctlPanel1.Controls.Add(this.lblLogInName);
            this.ctlPanel1.Controls.Add(this.ctlDetails);
            this.ctlPanel1.Controls.Add(this.lblDetails);
            this.ctlPanel1.Controls.Add(this.ctlPermsGroup);
            this.ctlPanel1.Controls.Add(this.lblPermsGroup);
            this.ctlPanel1.Controls.Add(this.ctlUserName);
            this.ctlPanel1.Controls.Add(this.lblUserName);
            this.ctlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ctlPanel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlPanel1.Location = new System.Drawing.Point(186, 66);
            this.ctlPanel1.Name = "ctlPanel1";
            this.ctlPanel1.Size = new System.Drawing.Size(343, 312);
            this.ctlPanel1.StylePainter = this.StyleGuideBase;
            this.ctlPanel1.TabIndex = 9;
            // 
            // ctlToolBar
            // 
            this.ctlToolBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(203)))), ((int)(((byte)(183)))));
            this.ctlToolBar.Size = new System.Drawing.Size(527, 28);
            // 
            // ctlNavBar
            // 
            this.ctlNavBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctlNavBar.Connection = null;
            this.ctlNavBar.ControlLayout = MControl.WinForms.ControlLayout.Visual;
            this.ctlNavBar.DBProvider = MControl.Data.DBProvider.SqlServer;
            this.ctlNavBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ctlNavBar.EnableButtons = false;
            this.ctlNavBar.Location = new System.Drawing.Point(2, 378);
            this.ctlNavBar.ManagerDataAdapter = null;
            this.ctlNavBar.MessageDelete = "Delete";
            this.ctlNavBar.MessageSaveChanges = "SaveChanges";
            this.ctlNavBar.Name = "ctlNavBar";
            this.ctlNavBar.Padding = new System.Windows.Forms.Padding(2);
            this.ctlNavBar.ShowChangesButtons = true;
            this.ctlNavBar.Size = new System.Drawing.Size(527, 24);
            this.ctlNavBar.SizingGrip = false;
            this.ctlNavBar.StylePainter = this.StyleGuideBase;
            this.ctlNavBar.TabIndex = 0;
            // 
            // StyleGuideBase
            // 
            this.StyleGuideBase.AlternatingColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(230)))), ((int)(((byte)(220)))));
            this.StyleGuideBase.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(106)))), ((int)(((byte)(197)))));
            this.StyleGuideBase.BorderHotColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(80)))), ((int)(((byte)(184)))));
            this.StyleGuideBase.CaptionColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(126)))), ((int)(((byte)(177)))));
            this.StyleGuideBase.ColorBrush1 = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(203)))), ((int)(((byte)(183)))));
            this.StyleGuideBase.ColorBrush2 = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.FlatColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.FocusedColor = System.Drawing.Color.RoyalBlue;
            this.StyleGuideBase.FormColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.StylePlan = MControl.WinForms.Styles.Desktop;
            // 
            // ctlPassword
            // 
            this.ctlPassword.BackColor = System.Drawing.Color.White;
            this.ctlNavBar.SetDataField(this.ctlPassword, "Password");
            this.ctlPassword.ForeColor = System.Drawing.Color.Black;
            this.ctlPassword.Location = new System.Drawing.Point(103, 124);
            this.ctlPassword.Name = "ctlPassword";
            this.ctlPassword.Size = new System.Drawing.Size(211, 20);
            this.ctlPassword.StylePainter = this.StyleGuideBase;
            this.ctlPassword.TabIndex = 22;
            // 
            // lblPassword
            // 
            this.lblPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.lblPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPassword.Location = new System.Drawing.Point(18, 124);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(88, 20);
            this.lblPassword.StylePainter = this.StyleGuideBase;
            this.lblPassword.Text = "Password";
            this.lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctlLogInName
            // 
            this.ctlLogInName.BackColor = System.Drawing.Color.White;
            this.ctlNavBar.SetDataField(this.ctlLogInName, "LogInName");
            this.ctlLogInName.ForeColor = System.Drawing.Color.Black;
            this.ctlLogInName.Location = new System.Drawing.Point(103, 98);
            this.ctlLogInName.Name = "ctlLogInName";
            this.ctlLogInName.Size = new System.Drawing.Size(211, 20);
            this.ctlLogInName.StylePainter = this.StyleGuideBase;
            this.ctlLogInName.TabIndex = 21;
            // 
            // lblLogInName
            // 
            this.lblLogInName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.lblLogInName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogInName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblLogInName.Location = new System.Drawing.Point(18, 98);
            this.lblLogInName.Name = "lblLogInName";
            this.lblLogInName.Size = new System.Drawing.Size(88, 20);
            this.lblLogInName.StylePainter = this.StyleGuideBase;
            this.lblLogInName.Text = "Login Name";
            this.lblLogInName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctlDetails
            // 
            this.ctlDetails.BackColor = System.Drawing.Color.White;
            this.ctlNavBar.SetDataField(this.ctlDetails, "Details");
            this.ctlDetails.ForeColor = System.Drawing.Color.Black;
            this.ctlDetails.Location = new System.Drawing.Point(103, 72);
            this.ctlDetails.Name = "ctlDetails";
            this.ctlDetails.Size = new System.Drawing.Size(211, 20);
            this.ctlDetails.StylePainter = this.StyleGuideBase;
            this.ctlDetails.TabIndex = 20;
            // 
            // lblDetails
            // 
            this.lblDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.lblDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetails.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDetails.Location = new System.Drawing.Point(18, 72);
            this.lblDetails.Name = "lblDetails";
            this.lblDetails.Size = new System.Drawing.Size(88, 20);
            this.lblDetails.StylePainter = this.StyleGuideBase;
            this.lblDetails.Text = "Details";
            this.lblDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctlPermsGroup
            // 
            this.ctlPermsGroup.ButtonToolTip = "";
            this.ctlNavBar.SetDataField(this.ctlPermsGroup, "PermsGroup");
            this.ctlPermsGroup.DisplayMember = "PermsGroupName";
            this.ctlPermsGroup.DropDownWidth = 128;
            this.ctlPermsGroup.IntegralHeight = false;
            this.ctlPermsGroup.Location = new System.Drawing.Point(103, 46);
            this.ctlPermsGroup.Name = "ctlPermsGroup";
            this.ctlPermsGroup.Size = new System.Drawing.Size(211, 20);
            this.ctlPermsGroup.StylePainter = this.StyleGuideBase;
            this.ctlPermsGroup.TabIndex = 19;
            this.ctlPermsGroup.ValueMember = "PermsGroupID";
            // 
            // lblPermsGroup
            // 
            this.lblPermsGroup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.lblPermsGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPermsGroup.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPermsGroup.Location = new System.Drawing.Point(18, 46);
            this.lblPermsGroup.Name = "lblPermsGroup";
            this.lblPermsGroup.Size = new System.Drawing.Size(88, 20);
            this.lblPermsGroup.StylePainter = this.StyleGuideBase;
            this.lblPermsGroup.Text = "Perm Group";
            this.lblPermsGroup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctlUserName
            // 
            this.ctlUserName.AcceptsReturn = true;
            this.ctlUserName.BackColor = System.Drawing.Color.White;
            this.ctlNavBar.SetDataField(this.ctlUserName, "UserName");
            this.ctlUserName.ForeColor = System.Drawing.Color.Black;
            this.ctlUserName.Location = new System.Drawing.Point(103, 20);
            this.ctlUserName.Name = "ctlUserName";
            this.ctlUserName.Size = new System.Drawing.Size(211, 20);
            this.ctlUserName.StylePainter = this.StyleGuideBase;
            this.ctlUserName.TabIndex = 18;
            // 
            // lblUserName
            // 
            this.lblUserName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.lblUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblUserName.Location = new System.Drawing.Point(18, 20);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(88, 20);
            this.lblUserName.StylePainter = this.StyleGuideBase;
            this.lblUserName.Text = "User Name";
            this.lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctlAccountId
            // 
            this.ctlAccountId.BackColor = System.Drawing.Color.White;
            this.ctlNavBar.SetDataField(this.ctlAccountId, "AccountId");
            this.ctlAccountId.ForeColor = System.Drawing.Color.Black;
            this.ctlAccountId.Location = new System.Drawing.Point(103, 149);
            this.ctlAccountId.Name = "ctlAccountId";
            this.ctlAccountId.Size = new System.Drawing.Size(211, 20);
            this.ctlAccountId.StylePainter = this.StyleGuideBase;
            this.ctlAccountId.TabIndex = 31;
            // 
            // ctlLabel1
            // 
            this.ctlLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel1.Location = new System.Drawing.Point(18, 149);
            this.ctlLabel1.Name = "ctlLabel1";
            this.ctlLabel1.Size = new System.Drawing.Size(88, 20);
            this.ctlLabel1.StylePainter = this.StyleGuideBase;
            this.ctlLabel1.Text = "Account Id";
            this.ctlLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctlLang
            // 
            this.ctlLang.BackColor = System.Drawing.Color.White;
            this.ctlNavBar.SetDataField(this.ctlLang, "Lang");
            this.ctlLang.ForeColor = System.Drawing.Color.Black;
            this.ctlLang.Location = new System.Drawing.Point(103, 175);
            this.ctlLang.Name = "ctlLang";
            this.ctlLang.Size = new System.Drawing.Size(211, 20);
            this.ctlLang.StylePainter = this.StyleGuideBase;
            this.ctlLang.TabIndex = 34;
            // 
            // ctlLabel2
            // 
            this.ctlLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel2.Location = new System.Drawing.Point(18, 175);
            this.ctlLabel2.Name = "ctlLabel2";
            this.ctlLabel2.Size = new System.Drawing.Size(88, 20);
            this.ctlLabel2.StylePainter = this.StyleGuideBase;
            this.ctlLabel2.Text = "Lang";
            this.ctlLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctlUserID
            // 
            this.ctlUserID.BackColor = System.Drawing.Color.White;
            this.ctlNavBar.SetDataField(this.ctlUserID, "UserID");
            this.ctlUserID.Enabled = false;
            this.ctlUserID.ForeColor = System.Drawing.Color.Black;
            this.ctlUserID.Location = new System.Drawing.Point(103, 253);
            this.ctlUserID.Name = "ctlUserID";
            this.ctlUserID.Size = new System.Drawing.Size(96, 20);
            this.ctlUserID.StylePainter = this.StyleGuideBase;
            this.ctlUserID.TabIndex = 37;
            // 
            // ctlLabel3
            // 
            this.ctlLabel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel3.Location = new System.Drawing.Point(18, 253);
            this.ctlLabel3.Name = "ctlLabel3";
            this.ctlLabel3.Size = new System.Drawing.Size(88, 20);
            this.ctlLabel3.StylePainter = this.StyleGuideBase;
            this.ctlLabel3.Text = "UserID";
            this.ctlLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctlMailAddress
            // 
            this.ctlMailAddress.BackColor = System.Drawing.Color.White;
            this.ctlNavBar.SetDataField(this.ctlMailAddress, "MailAddress");
            this.ctlMailAddress.ForeColor = System.Drawing.Color.Black;
            this.ctlMailAddress.Location = new System.Drawing.Point(103, 201);
            this.ctlMailAddress.Name = "ctlMailAddress";
            this.ctlMailAddress.Size = new System.Drawing.Size(211, 20);
            this.ctlMailAddress.StylePainter = this.StyleGuideBase;
            this.ctlMailAddress.TabIndex = 40;
            // 
            // ctlLabel4
            // 
            this.ctlLabel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel4.Location = new System.Drawing.Point(18, 201);
            this.ctlLabel4.Name = "ctlLabel4";
            this.ctlLabel4.Size = new System.Drawing.Size(88, 20);
            this.ctlLabel4.StylePainter = this.StyleGuideBase;
            this.ctlLabel4.Text = "MailAddress";
            this.ctlLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctlPhone
            // 
            this.ctlPhone.BackColor = System.Drawing.Color.White;
            this.ctlNavBar.SetDataField(this.ctlPhone, "Phone");
            this.ctlPhone.ForeColor = System.Drawing.Color.Black;
            this.ctlPhone.Location = new System.Drawing.Point(103, 227);
            this.ctlPhone.Name = "ctlPhone";
            this.ctlPhone.Size = new System.Drawing.Size(211, 20);
            this.ctlPhone.StylePainter = this.StyleGuideBase;
            this.ctlPhone.TabIndex = 43;
            // 
            // ctlLabel5
            // 
            this.ctlLabel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel5.Location = new System.Drawing.Point(18, 227);
            this.ctlLabel5.Name = "ctlLabel5";
            this.ctlLabel5.Size = new System.Drawing.Size(88, 20);
            this.ctlLabel5.StylePainter = this.StyleGuideBase;
            this.ctlLabel5.Text = "Phone";
            this.ctlLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UsersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionVisible = true;
            this.ClientSize = new System.Drawing.Size(531, 404);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "UsersForm";
            this.Text = "MControl Users ";
            this.ctlPanel1.ResumeLayout(false);
            this.ctlPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctlNavBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MControl.WinForms.McTextBox ctlPassword;
        private MControl.WinForms.McLabel lblPassword;
        private MControl.WinForms.McTextBox ctlLogInName;
        private MControl.WinForms.McLabel lblLogInName;
        private MControl.WinForms.McTextBox ctlDetails;
        private MControl.WinForms.McLabel lblDetails;
        private MControl.WinForms.McComboBox ctlPermsGroup;
        private MControl.WinForms.McLabel lblPermsGroup;
        private MControl.WinForms.McTextBox ctlUserName;
        private MControl.WinForms.McLabel lblUserName;
        private MControl.WinForms.McTextBox ctlUserID;
        private MControl.WinForms.McLabel ctlLabel3;
        private MControl.WinForms.McTextBox ctlLang;
        private MControl.WinForms.McLabel ctlLabel2;
        private MControl.WinForms.McTextBox ctlAccountId;
        private MControl.WinForms.McLabel ctlLabel1;
        private MControl.WinForms.McTextBox ctlPhone;
        private MControl.WinForms.McLabel ctlLabel5;
        private MControl.WinForms.McTextBox ctlMailAddress;
        private MControl.WinForms.McLabel ctlLabel4;
    }
}