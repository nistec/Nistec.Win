using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using MControl.Util;
using MControl.Data;
using MControl.Data.Util;

namespace MControl.Wizards.Permissions
{
	/// <summary>
    /// Summary description for LogInForm.
	/// </summary>
    public class LogInForm : MControl.WinForms.McForm   
	{
		private MControl.WinForms.McTextBox txtUser;
		private MControl.WinForms.McTextBox txtPwd;
		private MControl.WinForms.McLabel lblUser;
		private MControl.WinForms.McLabel lblPWD;
		private MControl.WinForms.McValidator frmEntryValidator;
		private MControl.WinForms.McButton btnSubmit;
		private System.ComponentModel.IContainer components;
		private MControl.WinForms.McButton btnExit;

        public LogInForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			    
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}
        public LogInForm(IDalBase dalBase, bool createPerms)
        {
            InitializeComponent();
            DalBase = dalBase;
            _CreatePerms = createPerms;
          
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
            this.components = new System.ComponentModel.Container();
            MControl.WinForms.ValidatorRule validatorRule1 = new MControl.WinForms.ValidatorRule();
            MControl.WinForms.ValidatorRule validatorRule2 = new MControl.WinForms.ValidatorRule();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogInForm));
            this.txtUser = new MControl.WinForms.McTextBox();
            this.txtPwd = new MControl.WinForms.McTextBox();
            this.lblUser = new MControl.WinForms.McLabel();
            this.lblPWD = new MControl.WinForms.McLabel();
            this.frmEntryValidator = new MControl.WinForms.McValidator(this.components);
            this.btnSubmit = new MControl.WinForms.McButton();
            this.btnExit = new MControl.WinForms.McButton();
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
            this.StyleGuideBase.FlatColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.FocusedColor = System.Drawing.Color.RoyalBlue;
            this.StyleGuideBase.FormColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.StylePlan = MControl.WinForms.Styles.Desktop;
            // 
            // txtUser
            // 
            this.txtUser.BackColor = System.Drawing.Color.White;
            this.txtUser.Location = new System.Drawing.Point(99, 61);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(144, 20);
            this.txtUser.StylePainter = this.StyleGuideBase;
            this.txtUser.TabIndex = 0;
            validatorRule1.ErrorMessage = "{McName} invalid User name.";
            validatorRule1.FieldName = "txtUser";
            validatorRule1.RegExPattern = "[\\w+\\s*]+";
            validatorRule1.Required = true;
            this.frmEntryValidator.SetValidatorRule(this.txtUser, validatorRule1);
            // 
            // txtPwd
            // 
            this.txtPwd.BackColor = System.Drawing.Color.White;
            this.txtPwd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txtPwd.ForeColor = System.Drawing.Color.Black;
            this.txtPwd.Location = new System.Drawing.Point(99, 85);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.Size = new System.Drawing.Size(144, 20);
            this.txtPwd.StylePainter = this.StyleGuideBase;
            this.txtPwd.TabIndex = 1;
            validatorRule2.FieldName = "txtPwd";
            validatorRule2.RegExPattern = "^\\w+$";
            validatorRule2.Required = true;
            this.frmEntryValidator.SetValidatorRule(this.txtPwd, validatorRule2);
            this.txtPwd.Enter += new System.EventHandler(this.txtPwd_Enter);
            // 
            // lblUser
            // 
            this.lblUser.BackColor = System.Drawing.Color.Ivory;
            this.lblUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblUser.Location = new System.Drawing.Point(35, 61);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(65, 20);
            this.lblUser.StylePainter = this.StyleGuideBase;
            this.lblUser.Text = "User";
            this.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPWD
            // 
            this.lblPWD.BackColor = System.Drawing.Color.Ivory;
            this.lblPWD.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPWD.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPWD.Location = new System.Drawing.Point(35, 85);
            this.lblPWD.Name = "lblPWD";
            this.lblPWD.Size = new System.Drawing.Size(65, 20);
            this.lblPWD.StylePainter = this.StyleGuideBase;
            this.lblPWD.Text = "Password";
            this.lblPWD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmEntryValidator
            // 
            this.frmEntryValidator.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.BlinkIfDifferentError;
            this.frmEntryValidator.Icon = ((System.Drawing.Icon)(resources.GetObject("frmEntryValidator.Icon")));
            this.frmEntryValidator.ValidatorDisplay = MControl.WinForms.ValidatorDisplay.Dynamic;
            // 
            // btnSubmit
            // 
            this.btnSubmit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSubmit.Location = new System.Drawing.Point(93, 124);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(70, 20);
            this.btnSubmit.StylePainter = this.StyleGuideBase;
            this.btnSubmit.TabIndex = 3;
            this.btnSubmit.Text = "Ok";
            this.btnSubmit.ToolTipText = "Ok";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnExit
            // 
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btnExit.Location = new System.Drawing.Point(173, 124);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(70, 20);
            this.btnExit.StylePainter = this.StyleGuideBase;
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "Exit";
            this.btnExit.ToolTipText = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // LogInForm
            // 
            this.AcceptButton = this.btnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.CancelButton = this.btnExit;
            this.CaptionVisible = true;
            this.ClientSize = new System.Drawing.Size(277, 178);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.lblPWD);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.txtPwd);
            this.Controls.Add(this.txtUser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Location = new System.Drawing.Point(500, 400);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LogInForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Log in";
            this.TopMost = true;
            this.Controls.SetChildIndex(this.txtUser, 0);
            this.Controls.SetChildIndex(this.txtPwd, 0);
            this.Controls.SetChildIndex(this.lblUser, 0);
            this.Controls.SetChildIndex(this.lblPWD, 0);
            this.Controls.SetChildIndex(this.btnSubmit, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

        public static DialogResult Open(IDalBase dalBase, bool createPerms)
        {
            LogInForm f=new LogInForm();
            f.DalBase = dalBase;
            f._CreatePerms = createPerms;
            return f.ShowDialog();
        }

        private IDalBase DalBase;
        private int _GroupID = -1;
        private int _UserID = -1;
        private bool _CreatePerms = false;
        private ActivePerms perms;
        private ActiveUser activeUser;

		private void btnSubmit_Click(object sender, System.EventArgs e)
		{
			bool res = 	this.frmEntryValidator.ValidateAll();
			if(res)
			{
                //DataRow row;
                try
                {
                    if (DalBase == null)
                    {
                        throw new Exception("Invalid Dal Connection String");
                    }
                    activeUser = new ActiveUser(DalBase, txtUser.Text, txtPwd.Text);

                    if (activeUser.IsEmpty)
                    {
                        MsgBox.ShowError("Invalid Acount");
                        return;
                    }

                    try
                    {
                        this._GroupID = activeUser.PermsGroup;
                        this._UserID = activeUser.UserId;
                        if (_CreatePerms)
                        {
                            perms = ActivePerms.Instance;
                            perms.Init(activeUser);
                        }
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
                catch (MControl.Data.DalException ex)
                {
                    MsgBox.ShowError("Database Connection Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    MsgBox.ShowError("Application Error: " + ex.Message);
                }
            }
			else
			{
                MsgBox.ShowError("Invalid Charecters");
			}
		
		}

		private void btnExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		[Browsable(false)]
		public int GroupID
		{
			get{return _GroupID;} 
		}
        [Browsable(false)]
        public int UserID
        {
            get { return _GroupID; }
        }
        [Browsable(false)]
        public ActiveUser ActiveUser
        {
            get { return activeUser; }
        }

        [Browsable(false)]
        public ActivePerms Perms
        {
            get { return perms; }
        }

        private void txtPwd_Enter(object sender, EventArgs e)
        {
            this.txtPwd.SelectAll();
        }

        
	}
}
