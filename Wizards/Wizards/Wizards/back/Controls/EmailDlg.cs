using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using mControl.Util;

namespace mControl.Wizards.Dlg
{
    public partial class EmailDlg : mControl.WinCtl.Forms.CtlForm
    {
        public EmailDlg()
        {
            InitializeComponent();
        }

        public EmailDlg(string from)
            : this(from, "", "", "")
        {

        }

         public EmailDlg(string from, string to, string subject)
            : this(from, to,  subject,"")
        {

        }
        public EmailDlg(string from, string to, string subject, string body)
            : this()
        {
            ctlFrom.Text = from;
            ctlTo.Text = to;
            ctlSubject.Text = subject;
            ctlBody.Text = body;
        }

         public void AddEmailList(string[] items)
        {
            this.ctlTo.Items.AddRange(items);
        }

        public string Body
        {
            get { return this.ctlBody.Text; }
            set { this.ctlBody.Text = value; }
        }
        public string From
        {
            get { return this.ctlFrom.Text; }
            set { this.ctlFrom.Text = value; }
        }
        public string To
        {
            get { return this.ctlTo.Text; }
            set { this.ctlTo.Text = value; }
        }
        public string Subject
        {
            get { return this.ctlSubject.Text; }
            set { this.ctlSubject.Text = value; }
        }

        protected override bool Initialize(object[] args)
        {
            if (args != null)
            {
                //userID=Util.Types.StringToInt(args[0].ToString(),0);
                //if(userID>0)
                //{
                //    SetUserDetails(userID);
                //}
                this.ctlFrom.Focus();
            }
            return true;
        }

        /// <summary>
        /// Open dialog
        /// </summary>
        /// <param name="args">arg[0]=userID</param>
        public override DialogResult OpenDialog(object[] args)
        {
            return base.OpenDialog(args);
        }


        public string[] MailDetails
        {
            get
            {
                return new string[] { this.ctlFrom.Text, this.ctlTo.Text, this.ctlSubject.Text, this.ctlBody.Text };
            }
        }
 
        private void btnExit_Click(object sender, System.EventArgs e)
        {
            ctlFrom.Text = "";
            ctlTo.Text = "";

            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {

            try
            {
                if (ctlFrom.TextLength == 0)
                {
                    RM.ShowError("InvalidFrom");
                }
                else if (ctlTo.Text.Length == 0)
                {
                    RM.ShowError("InvalidTo");
                }
                else
                {
                    Send();
                    this.DialogResult = DialogResult.OK;
                    //Dal.DB.EnterDB.EmailTraceUpdate(this.ctlTo.Text);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                RM.ShowError(ex.Message);
            }

        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            this.txtFileName.Text= mControl.Util.CommonDialog.FileDialog("*.*|*.*");

        }

        protected void Send()
        {

            string[] mail = this.MailDetails;
            mControl.Web.MailClient mailSend = new mControl.Web.MailClient(mail[0], mail[1], mail[2], mail[3]);
            string fileName = this.txtFileName.Text;
            if (fileName.Length > 0)
            {
                if (!System.IO.File.Exists(fileName))
                {
                    MsgBox.ShowError("File not exists :" + fileName);
                    return;
                }
                mailSend.Attachments.Add(new mControl.Web.ATTACHMENT(fileName));
            }

            mailSend.Send();


        }

    }
}