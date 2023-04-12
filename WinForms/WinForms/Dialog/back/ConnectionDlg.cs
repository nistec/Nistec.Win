using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Security.Cryptography; 


using System.Security;
using mControl.WinCtl.Controls;
using mControl.Data;

namespace mControl.WinCtl.Dlg
{

	public struct CONECCTION
	{
		//public CONECCTION(){}
		public mControl.Data.DBProvider Provider;
		public string ConnectionString;
		public string DBPath;
		public string ServerName;
		public string DBName;

		public CONECCTION Empty { get { return new CONECCTION(); } }
		public bool IsEmpty { get { return ConnectionString == null; } }
	}

	public class ConnetionDlg : mControl.WinCtl.Forms.CtlForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private mControl.WinCtl.Controls.CtlLabel ctlLabel1;
		private mControl.WinCtl.Controls.CtlButton btnOpen;
		private mControl.WinCtl.Controls.CtlButton btnExit;
		private mControl.WinCtl.Controls.CtlComboBox ctlProviders;
		private mControl.WinCtl.Controls.CtlLabel ctlLabel2;
		private mControl.WinCtl.Controls.CtlLabel ctlLabel3;
		private mControl.WinCtl.Controls.CtlLabel ctlLabel4;
		private mControl.WinCtl.Controls.CtlLabel ctlLabel5;
		private mControl.WinCtl.Controls.CtlTextBox ctlUserId;
		private mControl.WinCtl.Controls.CtlTextBox ctlPassword;
		private mControl.WinCtl.Controls.CtlTextBox ctlServer;
		private mControl.WinCtl.Controls.CtlTextBox ctlDatabase;
		private mControl.WinCtl.Controls.CtlCheckBox checkAuthentication;
		private mControl.WinCtl.Controls.CtlButton btnOk;
		private mControl.WinCtl.Controls.CtlGroupBox ctlGroupServer;
		private mControl.WinCtl.Controls.CtlTabControl ctlTabControl1;
		private mControl.WinCtl.Controls.CtlTabPage tabGeneral;
		private mControl.WinCtl.Controls.CtlTabPage tabConnString;
		private mControl.WinCtl.Controls.CtlTabPage tabProperty;
		private mControl.WinCtl.Controls.CtlTextBox ctlPath;
		private mControl.WinCtl.Controls.CtlTextBox ctlConnectionString;
		private mControl.WinCtl.Controls.CtlTextBox ctlPersistSecurityInfo;
		private mControl.WinCtl.Controls.CtlTextBox ctlIntegratedSecurity;
		private mControl.WinCtl.Controls.CtlTextBox ctlTimeOut;
		private mControl.WinCtl.Controls.CtlTextBox ctlProviderName;
		private mControl.WinCtl.Controls.CtlLabel ctlLabel6;
		private mControl.WinCtl.Controls.CtlLabel ctlLabel7;
		private mControl.WinCtl.Controls.CtlLabel ctlLabel8;
		private mControl.WinCtl.Controls.CtlLabel ctlLabel9;
		private mControl.WinCtl.Controls.CtlLabel ctlLabel10;
		private mControl.WinCtl.Controls.CtlTextBox ctlFriendlyName;
		private mControl.WinCtl.Controls.CtlLabel ctlLabel11;
		private mControl.WinCtl.Controls.CtlTextBox ctlWorkstation;
		private mControl.WinCtl.Controls.CtlLabel ctlLabel12;
		private mControl.WinCtl.Controls.CtlTextBox ctlEncrypt;
		private mControl.WinCtl.Controls.CtlLabel ctlLabel13;
		private mControl.WinCtl.Controls.CtlTextBox ctlPacketSize;
		private mControl.WinCtl.Controls.CtlCheckBox chkPersistSecurityInfo;
		private mControl.WinCtl.Controls.CtlCheckBox chkIntegratedSecurity;
		private mControl.WinCtl.Controls.CtlButton btnTestConn;


		public ConnetionDlg()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			InitCombo();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnetionDlg));
            this.btnOpen = new mControl.WinCtl.Controls.CtlButton();
            this.ctlPath = new mControl.WinCtl.Controls.CtlTextBox();
            this.ctlProviders = new mControl.WinCtl.Controls.CtlComboBox();
            this.btnOk = new mControl.WinCtl.Controls.CtlButton();
            this.btnExit = new mControl.WinCtl.Controls.CtlButton();
            this.ctlLabel1 = new mControl.WinCtl.Controls.CtlLabel();
            this.ctlUserId = new mControl.WinCtl.Controls.CtlTextBox();
            this.ctlPassword = new mControl.WinCtl.Controls.CtlTextBox();
            this.ctlLabel3 = new mControl.WinCtl.Controls.CtlLabel();
            this.ctlLabel2 = new mControl.WinCtl.Controls.CtlLabel();
            this.ctlServer = new mControl.WinCtl.Controls.CtlTextBox();
            this.ctlDatabase = new mControl.WinCtl.Controls.CtlTextBox();
            this.ctlLabel4 = new mControl.WinCtl.Controls.CtlLabel();
            this.ctlLabel5 = new mControl.WinCtl.Controls.CtlLabel();
            this.checkAuthentication = new mControl.WinCtl.Controls.CtlCheckBox();
            this.btnTestConn = new mControl.WinCtl.Controls.CtlButton();
            this.ctlGroupServer = new mControl.WinCtl.Controls.CtlGroupBox();
            this.ctlTabControl1 = new mControl.WinCtl.Controls.CtlTabControl();
            this.tabGeneral = new mControl.WinCtl.Controls.CtlTabPage();
            this.tabProperty = new mControl.WinCtl.Controls.CtlTabPage();
            this.chkIntegratedSecurity = new mControl.WinCtl.Controls.CtlCheckBox();
            this.chkPersistSecurityInfo = new mControl.WinCtl.Controls.CtlCheckBox();
            this.ctlLabel13 = new mControl.WinCtl.Controls.CtlLabel();
            this.ctlPacketSize = new mControl.WinCtl.Controls.CtlTextBox();
            this.ctlLabel12 = new mControl.WinCtl.Controls.CtlLabel();
            this.ctlEncrypt = new mControl.WinCtl.Controls.CtlTextBox();
            this.ctlLabel11 = new mControl.WinCtl.Controls.CtlLabel();
            this.ctlWorkstation = new mControl.WinCtl.Controls.CtlTextBox();
            this.ctlLabel10 = new mControl.WinCtl.Controls.CtlLabel();
            this.ctlFriendlyName = new mControl.WinCtl.Controls.CtlTextBox();
            this.ctlLabel9 = new mControl.WinCtl.Controls.CtlLabel();
            this.ctlLabel8 = new mControl.WinCtl.Controls.CtlLabel();
            this.ctlLabel7 = new mControl.WinCtl.Controls.CtlLabel();
            this.ctlLabel6 = new mControl.WinCtl.Controls.CtlLabel();
            this.ctlProviderName = new mControl.WinCtl.Controls.CtlTextBox();
            this.ctlTimeOut = new mControl.WinCtl.Controls.CtlTextBox();
            this.ctlIntegratedSecurity = new mControl.WinCtl.Controls.CtlTextBox();
            this.ctlPersistSecurityInfo = new mControl.WinCtl.Controls.CtlTextBox();
            this.tabConnString = new mControl.WinCtl.Controls.CtlTabPage();
            this.ctlConnectionString = new mControl.WinCtl.Controls.CtlTextBox();
            this.ctlGroupServer.SuspendLayout();
            this.ctlTabControl1.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.tabProperty.SuspendLayout();
            this.tabConnString.SuspendLayout();
            this.SuspendLayout();
            // 
            // caption
            // 
            this.caption.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(203)))), ((int)(((byte)(183)))));
            this.caption.ControlLayout = mControl.WinCtl.Controls.ControlLayout.Visual;
            this.caption.Dock = System.Windows.Forms.DockStyle.Top;
            this.caption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.caption.ForeColor = System.Drawing.SystemColors.ControlText;
            this.caption.Image = ((System.Drawing.Image)(resources.GetObject("caption.Image")));
            this.caption.ImageSizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.caption.Location = new System.Drawing.Point(2, 2);
            this.caption.Name = "caption";
            this.caption.ShowFormBox = true;
            this.caption.ShowMaximize = false;
            this.caption.StylePainter = this.StyleGuideBase;
            this.caption.SubText = "";
            this.caption.Text = "Connection Dialog";
            // 
            // StyleGuideBase
            // 
            this.StyleGuideBase.AlternatingColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(230)))), ((int)(((byte)(220)))));
            this.StyleGuideBase.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(106)))), ((int)(((byte)(197)))));
            this.StyleGuideBase.BorderHotColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(80)))), ((int)(((byte)(184)))));
            this.StyleGuideBase.CaptionColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(78)))), ((int)(((byte)(152)))));
            this.StyleGuideBase.ColorBrush1 = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(203)))), ((int)(((byte)(183)))));
            this.StyleGuideBase.ColorBrush2 = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.FlatColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.FocusedColor = System.Drawing.Color.RoyalBlue;
            this.StyleGuideBase.FormColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.StylePlan = mControl.WinCtl.Controls.Styles.Desktop;
            // 
            // btnOpen
            // 
            this.btnOpen.ControlLayout = mControl.WinCtl.Controls.ControlLayout.Visual;
            this.btnOpen.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnOpen.Location = new System.Drawing.Point(16, 40);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(64, 20);
            this.btnOpen.StylePainter = this.StyleGuideBase;
            this.btnOpen.TabIndex = 3;
            this.btnOpen.Text = "Open..";
            this.btnOpen.ToolTipText = "Open..";
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // ctlPath
            // 
            this.ctlPath.BackColor = System.Drawing.Color.White;
            this.ctlPath.ForeColor = System.Drawing.Color.Black;
            this.ctlPath.Location = new System.Drawing.Point(88, 40);
            this.ctlPath.Name = "ctlPath";
            this.ctlPath.ReadOnly = true;
            this.ctlPath.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ctlPath.Size = new System.Drawing.Size(200, 20);
            this.ctlPath.StylePainter = this.StyleGuideBase;
            this.ctlPath.TabIndex = 0;
            // 
            // ctlProviders
            // 
            this.ctlProviders.ButtonToolTip = "";
            this.ctlProviders.DropDownWidth = 200;
            this.ctlProviders.IntegralHeight = false;
            this.ctlProviders.Location = new System.Drawing.Point(88, 16);
            this.ctlProviders.Name = "ctlProviders";
            this.ctlProviders.Size = new System.Drawing.Size(200, 20);
            this.ctlProviders.StylePainter = this.StyleGuideBase;
            this.ctlProviders.TabIndex = 23;
            this.ctlProviders.SelectedIndexChanged += new System.EventHandler(this.ctlProviders_SelectedIndexChanged);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnOk.Location = new System.Drawing.Point(183, 336);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(64, 24);
            this.btnOk.StylePainter = this.StyleGuideBase;
            this.btnOk.TabIndex = 24;
            this.btnOk.Text = "Ok";
            this.btnOk.ToolTipText = "Ok";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(255, 336);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(64, 24);
            this.btnExit.StylePainter = this.StyleGuideBase;
            this.btnExit.TabIndex = 25;
            this.btnExit.Text = "Exit";
            this.btnExit.ToolTipText = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // ctlLabel1
            // 
            this.ctlLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlLabel1.FixSize = false;
            this.ctlLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel1.Location = new System.Drawing.Point(16, 16);
            this.ctlLabel1.Name = "ctlLabel1";
            this.ctlLabel1.Size = new System.Drawing.Size(64, 20);
            this.ctlLabel1.StylePainter = this.StyleGuideBase;
            this.ctlLabel1.Text = "Provider";
            // 
            // ctlUserId
            // 
            this.ctlUserId.BackColor = System.Drawing.Color.White;
            this.ctlUserId.Enabled = false;
            this.ctlUserId.ForeColor = System.Drawing.Color.Black;
            this.ctlUserId.Location = new System.Drawing.Point(112, 96);
            this.ctlUserId.Name = "ctlUserId";
            this.ctlUserId.Size = new System.Drawing.Size(144, 20);
            this.ctlUserId.StylePainter = this.StyleGuideBase;
            this.ctlUserId.TabIndex = 27;
            // 
            // ctlPassword
            // 
            this.ctlPassword.BackColor = System.Drawing.Color.White;
            this.ctlPassword.Enabled = false;
            this.ctlPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ctlPassword.ForeColor = System.Drawing.Color.Black;
            this.ctlPassword.Location = new System.Drawing.Point(112, 120);
            this.ctlPassword.Name = "ctlPassword";
            this.ctlPassword.PasswordChar = '*';
            this.ctlPassword.Size = new System.Drawing.Size(144, 20);
            this.ctlPassword.StylePainter = this.StyleGuideBase;
            this.ctlPassword.TabIndex = 28;
            // 
            // ctlLabel3
            // 
            this.ctlLabel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlLabel3.FixSize = false;
            this.ctlLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel3.Location = new System.Drawing.Point(8, 120);
            this.ctlLabel3.Name = "ctlLabel3";
            this.ctlLabel3.Size = new System.Drawing.Size(96, 20);
            this.ctlLabel3.StylePainter = this.StyleGuideBase;
            this.ctlLabel3.Text = "Password";
            this.ctlLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctlLabel2
            // 
            this.ctlLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlLabel2.FixSize = false;
            this.ctlLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel2.Location = new System.Drawing.Point(8, 96);
            this.ctlLabel2.Name = "ctlLabel2";
            this.ctlLabel2.Size = new System.Drawing.Size(96, 20);
            this.ctlLabel2.StylePainter = this.StyleGuideBase;
            this.ctlLabel2.Text = "UserID";
            this.ctlLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctlServer
            // 
            this.ctlServer.BackColor = System.Drawing.Color.White;
            this.ctlServer.ForeColor = System.Drawing.Color.Black;
            this.ctlServer.Location = new System.Drawing.Point(80, 16);
            this.ctlServer.Name = "ctlServer";
            this.ctlServer.Size = new System.Drawing.Size(176, 20);
            this.ctlServer.StylePainter = this.StyleGuideBase;
            this.ctlServer.TabIndex = 30;
            this.ctlServer.Text = "(local)";
            // 
            // ctlDatabase
            // 
            this.ctlDatabase.BackColor = System.Drawing.Color.White;
            this.ctlDatabase.ForeColor = System.Drawing.Color.Black;
            this.ctlDatabase.Location = new System.Drawing.Point(80, 40);
            this.ctlDatabase.Name = "ctlDatabase";
            this.ctlDatabase.Size = new System.Drawing.Size(176, 20);
            this.ctlDatabase.StylePainter = this.StyleGuideBase;
            this.ctlDatabase.TabIndex = 31;
            // 
            // ctlLabel4
            // 
            this.ctlLabel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlLabel4.FixSize = false;
            this.ctlLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel4.Location = new System.Drawing.Point(8, 16);
            this.ctlLabel4.Name = "ctlLabel4";
            this.ctlLabel4.Size = new System.Drawing.Size(64, 20);
            this.ctlLabel4.StylePainter = this.StyleGuideBase;
            this.ctlLabel4.Text = "Server";
            // 
            // ctlLabel5
            // 
            this.ctlLabel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlLabel5.FixSize = false;
            this.ctlLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel5.Location = new System.Drawing.Point(8, 40);
            this.ctlLabel5.Name = "ctlLabel5";
            this.ctlLabel5.Size = new System.Drawing.Size(64, 20);
            this.ctlLabel5.StylePainter = this.StyleGuideBase;
            this.ctlLabel5.Text = "Database";
            // 
            // checkAuthentication
            // 
            this.checkAuthentication.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.checkAuthentication.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkAuthentication.Location = new System.Drawing.Point(8, 72);
            this.checkAuthentication.Name = "checkAuthentication";
            this.checkAuthentication.Size = new System.Drawing.Size(128, 13);
            this.checkAuthentication.StylePainter = this.StyleGuideBase;
            this.checkAuthentication.TabIndex = 34;
            this.checkAuthentication.Text = "Use Authentication";
            this.checkAuthentication.CheckedChanged += new System.EventHandler(this.checkAuthentication_CheckedChanged);
            // 
            // btnTestConn
            // 
            this.btnTestConn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTestConn.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnTestConn.Location = new System.Drawing.Point(7, 192);
            this.btnTestConn.Name = "btnTestConn";
            this.btnTestConn.Size = new System.Drawing.Size(112, 24);
            this.btnTestConn.StylePainter = this.StyleGuideBase;
            this.btnTestConn.TabIndex = 35;
            this.btnTestConn.Text = "Connection Test";
            this.btnTestConn.ToolTipText = "Connection Test";
            this.btnTestConn.Click += new System.EventHandler(this.btnTestConn_Click);
            // 
            // ctlGroupServer
            // 
            this.ctlGroupServer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlGroupServer.Controls.Add(this.ctlServer);
            this.ctlGroupServer.Controls.Add(this.ctlLabel5);
            this.ctlGroupServer.Controls.Add(this.ctlLabel4);
            this.ctlGroupServer.Controls.Add(this.ctlDatabase);
            this.ctlGroupServer.Controls.Add(this.checkAuthentication);
            this.ctlGroupServer.Controls.Add(this.ctlLabel3);
            this.ctlGroupServer.Controls.Add(this.ctlPassword);
            this.ctlGroupServer.Controls.Add(this.ctlUserId);
            this.ctlGroupServer.Controls.Add(this.ctlLabel2);
            this.ctlGroupServer.Enabled = false;
            this.ctlGroupServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ctlGroupServer.ForeColor = System.Drawing.Color.Black;
            this.ctlGroupServer.Location = new System.Drawing.Point(16, 72);
            this.ctlGroupServer.Name = "ctlGroupServer";
            this.ctlGroupServer.ReadOnly = false;
            this.ctlGroupServer.Size = new System.Drawing.Size(272, 152);
            this.ctlGroupServer.StylePainter = this.StyleGuideBase;
            this.ctlGroupServer.TabIndex = 36;
            this.ctlGroupServer.TabStop = false;
            this.ctlGroupServer.Text = "Server";
            // 
            // ctlTabControl1
            // 
            this.ctlTabControl1.ControlLayout = mControl.WinCtl.Controls.ControlLayout.XpLayout;
            this.ctlTabControl1.Controls.Add(this.tabGeneral);
            this.ctlTabControl1.Controls.Add(this.tabProperty);
            this.ctlTabControl1.Controls.Add(this.tabConnString);
            this.ctlTabControl1.ItemSize = new System.Drawing.Size(0, 22);
            this.ctlTabControl1.Location = new System.Drawing.Point(8, 64);
            this.ctlTabControl1.Name = "ctlTabControl1";
            this.ctlTabControl1.Size = new System.Drawing.Size(304, 264);
            this.ctlTabControl1.TabIndex = 37;
            this.ctlTabControl1.TabPages.AddRange(new mControl.WinCtl.Controls.CtlTabPage[] {
            this.tabGeneral,
            this.tabProperty,
            this.tabConnString});
            this.ctlTabControl1.TabStop = false;
            this.ctlTabControl1.Text = "ctlTabControl1";
            this.ctlTabControl1.SelectedIndexChanged += new System.EventHandler(this.ctlTabControl1_SelectedIndexChanged);
            // 
            // tabGeneral
            // 
            this.tabGeneral.BackColor = System.Drawing.Color.White;
            this.tabGeneral.Controls.Add(this.ctlProviders);
            this.tabGeneral.Controls.Add(this.ctlGroupServer);
            this.tabGeneral.Controls.Add(this.ctlPath);
            this.tabGeneral.Controls.Add(this.btnOpen);
            this.tabGeneral.Controls.Add(this.ctlLabel1);
            this.tabGeneral.Location = new System.Drawing.Point(4, 29);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Size = new System.Drawing.Size(295, 230);
            this.tabGeneral.Text = "General";
            // 
            // tabProperty
            // 
            this.tabProperty.BackColor = System.Drawing.Color.White;
            this.tabProperty.Controls.Add(this.chkIntegratedSecurity);
            this.tabProperty.Controls.Add(this.chkPersistSecurityInfo);
            this.tabProperty.Controls.Add(this.ctlLabel13);
            this.tabProperty.Controls.Add(this.ctlPacketSize);
            this.tabProperty.Controls.Add(this.ctlLabel12);
            this.tabProperty.Controls.Add(this.ctlEncrypt);
            this.tabProperty.Controls.Add(this.ctlLabel11);
            this.tabProperty.Controls.Add(this.ctlWorkstation);
            this.tabProperty.Controls.Add(this.ctlLabel10);
            this.tabProperty.Controls.Add(this.ctlFriendlyName);
            this.tabProperty.Controls.Add(this.ctlLabel9);
            this.tabProperty.Controls.Add(this.ctlLabel8);
            this.tabProperty.Controls.Add(this.ctlLabel7);
            this.tabProperty.Controls.Add(this.ctlLabel6);
            this.tabProperty.Controls.Add(this.ctlProviderName);
            this.tabProperty.Controls.Add(this.ctlTimeOut);
            this.tabProperty.Controls.Add(this.ctlIntegratedSecurity);
            this.tabProperty.Controls.Add(this.ctlPersistSecurityInfo);
            this.tabProperty.Location = new System.Drawing.Point(4, 29);
            this.tabProperty.Name = "tabProperty";
            this.tabProperty.Size = new System.Drawing.Size(295, 230);
            this.tabProperty.Text = "Properties";
            // 
            // chkIntegratedSecurity
            // 
            this.chkIntegratedSecurity.BackColor = System.Drawing.Color.White;
            this.chkIntegratedSecurity.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkIntegratedSecurity.Location = new System.Drawing.Point(152, 140);
            this.chkIntegratedSecurity.Name = "chkIntegratedSecurity";
            this.chkIntegratedSecurity.Size = new System.Drawing.Size(16, 13);
            this.chkIntegratedSecurity.TabIndex = 58;
            // 
            // chkPersistSecurityInfo
            // 
            this.chkPersistSecurityInfo.BackColor = System.Drawing.Color.White;
            this.chkPersistSecurityInfo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkPersistSecurityInfo.Location = new System.Drawing.Point(152, 118);
            this.chkPersistSecurityInfo.Name = "chkPersistSecurityInfo";
            this.chkPersistSecurityInfo.Size = new System.Drawing.Size(16, 13);
            this.chkPersistSecurityInfo.TabIndex = 57;
            // 
            // ctlLabel13
            // 
            this.ctlLabel13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlLabel13.FixSize = false;
            this.ctlLabel13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel13.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel13.Location = new System.Drawing.Point(16, 184);
            this.ctlLabel13.Name = "ctlLabel13";
            this.ctlLabel13.Size = new System.Drawing.Size(120, 20);
            this.ctlLabel13.StylePainter = this.StyleGuideBase;
            this.ctlLabel13.Text = "Packet Size";
            this.ctlLabel13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctlPacketSize
            // 
            this.ctlPacketSize.BackColor = System.Drawing.Color.White;
            this.ctlPacketSize.DecimalPlaces = 0;
            this.ctlPacketSize.ForeColor = System.Drawing.Color.Black;
            this.ctlPacketSize.Format = "F";
            this.ctlPacketSize.FormatType = mControl.Util.Formats.FixNumber;
            this.ctlPacketSize.Location = new System.Drawing.Point(152, 184);
            this.ctlPacketSize.Name = "ctlPacketSize";
            this.ctlPacketSize.ReadOnly = true;
            this.ctlPacketSize.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ctlPacketSize.Size = new System.Drawing.Size(136, 20);
            this.ctlPacketSize.StylePainter = this.StyleGuideBase;
            this.ctlPacketSize.TabIndex = 55;
            this.ctlPacketSize.Text = "8192";
            this.ctlPacketSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ctlLabel12
            // 
            this.ctlLabel12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlLabel12.FixSize = false;
            this.ctlLabel12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel12.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel12.Location = new System.Drawing.Point(16, 64);
            this.ctlLabel12.Name = "ctlLabel12";
            this.ctlLabel12.Size = new System.Drawing.Size(120, 20);
            this.ctlLabel12.StylePainter = this.StyleGuideBase;
            this.ctlLabel12.Text = "Encrypt";
            this.ctlLabel12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctlEncrypt
            // 
            this.ctlEncrypt.BackColor = System.Drawing.Color.White;
            this.ctlEncrypt.ForeColor = System.Drawing.Color.Black;
            this.ctlEncrypt.Location = new System.Drawing.Point(152, 64);
            this.ctlEncrypt.Name = "ctlEncrypt";
            this.ctlEncrypt.ReadOnly = true;
            this.ctlEncrypt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ctlEncrypt.Size = new System.Drawing.Size(136, 20);
            this.ctlEncrypt.StylePainter = this.StyleGuideBase;
            this.ctlEncrypt.TabIndex = 53;
            this.ctlEncrypt.Text = "False";
            // 
            // ctlLabel11
            // 
            this.ctlLabel11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlLabel11.FixSize = false;
            this.ctlLabel11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel11.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel11.Location = new System.Drawing.Point(16, 40);
            this.ctlLabel11.Name = "ctlLabel11";
            this.ctlLabel11.Size = new System.Drawing.Size(120, 20);
            this.ctlLabel11.StylePainter = this.StyleGuideBase;
            this.ctlLabel11.Text = "Workstation ID";
            this.ctlLabel11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctlWorkstation
            // 
            this.ctlWorkstation.BackColor = System.Drawing.Color.White;
            this.ctlWorkstation.ForeColor = System.Drawing.Color.Black;
            this.ctlWorkstation.Location = new System.Drawing.Point(152, 40);
            this.ctlWorkstation.Name = "ctlWorkstation";
            this.ctlWorkstation.ReadOnly = true;
            this.ctlWorkstation.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ctlWorkstation.Size = new System.Drawing.Size(136, 20);
            this.ctlWorkstation.StylePainter = this.StyleGuideBase;
            this.ctlWorkstation.TabIndex = 51;
            // 
            // ctlLabel10
            // 
            this.ctlLabel10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlLabel10.FixSize = false;
            this.ctlLabel10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel10.Location = new System.Drawing.Point(16, 88);
            this.ctlLabel10.Name = "ctlLabel10";
            this.ctlLabel10.Size = new System.Drawing.Size(120, 20);
            this.ctlLabel10.StylePainter = this.StyleGuideBase;
            this.ctlLabel10.Text = "FriendlyName";
            this.ctlLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctlFriendlyName
            // 
            this.ctlFriendlyName.BackColor = System.Drawing.Color.White;
            this.ctlFriendlyName.ForeColor = System.Drawing.Color.Black;
            this.ctlFriendlyName.Location = new System.Drawing.Point(152, 88);
            this.ctlFriendlyName.Name = "ctlFriendlyName";
            this.ctlFriendlyName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ctlFriendlyName.Size = new System.Drawing.Size(136, 20);
            this.ctlFriendlyName.StylePainter = this.StyleGuideBase;
            this.ctlFriendlyName.TabIndex = 49;
            // 
            // ctlLabel9
            // 
            this.ctlLabel9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlLabel9.FixSize = false;
            this.ctlLabel9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel9.Location = new System.Drawing.Point(16, 160);
            this.ctlLabel9.Name = "ctlLabel9";
            this.ctlLabel9.Size = new System.Drawing.Size(120, 20);
            this.ctlLabel9.StylePainter = this.StyleGuideBase;
            this.ctlLabel9.Text = "Time Out";
            this.ctlLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctlLabel8
            // 
            this.ctlLabel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlLabel8.FixSize = false;
            this.ctlLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel8.Location = new System.Drawing.Point(16, 136);
            this.ctlLabel8.Name = "ctlLabel8";
            this.ctlLabel8.Size = new System.Drawing.Size(120, 20);
            this.ctlLabel8.StylePainter = this.StyleGuideBase;
            this.ctlLabel8.Text = "IntegratedSecurity";
            this.ctlLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctlLabel7
            // 
            this.ctlLabel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlLabel7.FixSize = false;
            this.ctlLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel7.Location = new System.Drawing.Point(16, 112);
            this.ctlLabel7.Name = "ctlLabel7";
            this.ctlLabel7.Size = new System.Drawing.Size(120, 20);
            this.ctlLabel7.StylePainter = this.StyleGuideBase;
            this.ctlLabel7.Text = "PersistSecurityInfo";
            this.ctlLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctlLabel6
            // 
            this.ctlLabel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ctlLabel6.FixSize = false;
            this.ctlLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlLabel6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlLabel6.Location = new System.Drawing.Point(16, 16);
            this.ctlLabel6.Name = "ctlLabel6";
            this.ctlLabel6.Size = new System.Drawing.Size(120, 20);
            this.ctlLabel6.StylePainter = this.StyleGuideBase;
            this.ctlLabel6.Text = "Provider Name";
            this.ctlLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctlProviderName
            // 
            this.ctlProviderName.BackColor = System.Drawing.Color.White;
            this.ctlProviderName.ForeColor = System.Drawing.Color.Black;
            this.ctlProviderName.Location = new System.Drawing.Point(152, 16);
            this.ctlProviderName.Name = "ctlProviderName";
            this.ctlProviderName.ReadOnly = true;
            this.ctlProviderName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ctlProviderName.Size = new System.Drawing.Size(136, 20);
            this.ctlProviderName.StylePainter = this.StyleGuideBase;
            this.ctlProviderName.TabIndex = 43;
            // 
            // ctlTimeOut
            // 
            this.ctlTimeOut.BackColor = System.Drawing.Color.White;
            this.ctlTimeOut.DecimalPlaces = 0;
            this.ctlTimeOut.ForeColor = System.Drawing.Color.Black;
            this.ctlTimeOut.Format = "F";
            this.ctlTimeOut.FormatType = mControl.Util.Formats.FixNumber;
            this.ctlTimeOut.Location = new System.Drawing.Point(152, 160);
            this.ctlTimeOut.Name = "ctlTimeOut";
            this.ctlTimeOut.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ctlTimeOut.Size = new System.Drawing.Size(136, 20);
            this.ctlTimeOut.StylePainter = this.StyleGuideBase;
            this.ctlTimeOut.TabIndex = 41;
            this.ctlTimeOut.Text = "30";
            this.ctlTimeOut.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ctlIntegratedSecurity
            // 
            this.ctlIntegratedSecurity.BackColor = System.Drawing.Color.White;
            this.ctlIntegratedSecurity.ForeColor = System.Drawing.Color.Black;
            this.ctlIntegratedSecurity.Location = new System.Drawing.Point(184, 136);
            this.ctlIntegratedSecurity.Name = "ctlIntegratedSecurity";
            this.ctlIntegratedSecurity.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ctlIntegratedSecurity.Size = new System.Drawing.Size(104, 20);
            this.ctlIntegratedSecurity.StylePainter = this.StyleGuideBase;
            this.ctlIntegratedSecurity.TabIndex = 39;
            this.ctlIntegratedSecurity.Text = "SSPI";
            // 
            // ctlPersistSecurityInfo
            // 
            this.ctlPersistSecurityInfo.BackColor = System.Drawing.Color.White;
            this.ctlPersistSecurityInfo.ForeColor = System.Drawing.Color.Black;
            this.ctlPersistSecurityInfo.Location = new System.Drawing.Point(184, 112);
            this.ctlPersistSecurityInfo.Name = "ctlPersistSecurityInfo";
            this.ctlPersistSecurityInfo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ctlPersistSecurityInfo.Size = new System.Drawing.Size(104, 20);
            this.ctlPersistSecurityInfo.StylePainter = this.StyleGuideBase;
            this.ctlPersistSecurityInfo.TabIndex = 37;
            this.ctlPersistSecurityInfo.Text = "False";
            // 
            // tabConnString
            // 
            this.tabConnString.BackColor = System.Drawing.Color.White;
            this.tabConnString.Controls.Add(this.ctlConnectionString);
            this.tabConnString.Controls.Add(this.btnTestConn);
            this.tabConnString.Location = new System.Drawing.Point(4, 29);
            this.tabConnString.Name = "tabConnString";
            this.tabConnString.Size = new System.Drawing.Size(295, 230);
            this.tabConnString.Text = "ConnectionString";
            // 
            // ctlConnectionString
            // 
            this.ctlConnectionString.BackColor = System.Drawing.Color.White;
            this.ctlConnectionString.ForeColor = System.Drawing.Color.Black;
            this.ctlConnectionString.Location = new System.Drawing.Point(16, 16);
            this.ctlConnectionString.Multiline = true;
            this.ctlConnectionString.Name = "ctlConnectionString";
            this.ctlConnectionString.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ctlConnectionString.Size = new System.Drawing.Size(272, 152);
            this.ctlConnectionString.TabIndex = 1;
            // 
            // ConnetionDlg
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(329, 377);
            this.Controls.Add(this.ctlTabControl1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "ConnetionDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connection Dialog";
            this.Controls.SetChildIndex(this.caption, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.ctlTabControl1, 0);
            this.ctlGroupServer.ResumeLayout(false);
            this.ctlGroupServer.PerformLayout();
            this.ctlTabControl1.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.tabGeneral.PerformLayout();
            this.tabProperty.ResumeLayout(false);
            this.tabProperty.PerformLayout();
            this.tabConnString.ResumeLayout(false);
            this.tabConnString.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		private Data.DBProvider provider;
		//private string dbPath="";
		private string dbName="";
		private Control Ctl;

		protected override bool Initialize(object[] args)
		{
			defaultLayout=true;
			if(args!=null)
			{
				if(args[0]!=null)
				{
					this.ctlProviders.SelectedIndex= Util.Types.ToInt(args[0].ToString(),0);
					this.ctlProviders.Enabled=false;
					this.provider=(DBProvider)this.ctlProviders.SelectedIndex;
					this.btnTestConn.Enabled=this.provider==DBProvider.SqlServer;
				}
				if(args.Length>1 && args[1] is Control)
				{
					this.Ctl=(Control)args[1];
					if(this.Ctl is IStyleCtl)
					{
						this.SetStyleLayout(((IStyleCtl)this.Ctl).CtlStyleLayout.Layout);
						this.SetChildrenStyle();
						defaultLayout=false;
					}
				}
			}
			if(defaultLayout)
				base.SetDefaultLayout();
			return true;
		}

		/// <summary>
		/// Open Form
		/// </summary>
		/// <param name="args">arg[0]=DBProvider number,arg[1]=Parent Control</param>
		public override void Open(object[] args)
		{
			base.Open (args);
		}

		public static CONECCTION OpenDialog(Control ctl)
		{
			ConnetionDlg dlg=new ConnetionDlg();
			dlg.Initialize(new object[]{null,ctl});
			dlg.ShowDialog();
			return dlg.Connection;
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad (e);
			if(!base.Initialized)
			{

			}
		}

		private void InitCombo()
		{
			this.ctlProviders.Items.Add("Microsoft.Jet.OLEDB.4.0");
			this.ctlProviders.Items.Add("Microsoft.Sql Server");
		}

		private void Setting()
		{

			this.provider= (DBProvider)this.ctlProviders.SelectedIndex;
			this.ctlProviderName.Text=provider.ToString();
			bool enabledServer=this.ctlProviders.SelectedIndex==1;
			this.ctlGroupServer.Enabled=enabledServer;
			this.btnOpen.Enabled=this.ctlProviders.SelectedIndex==0;
			this.ctlConnectionString.Text="";
			this.btnTestConn.Enabled=this.provider==DBProvider.SqlServer;

		}

		private void ctlProviders_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			Setting();
		}

		private void btnOpen_Click(object sender, System.EventArgs e)
		{
			if(this.ctlProviders.SelectedIndex==0)//Access
			{
				string path=Data.OleDb.DBCmd.OpenAccessDB();
				//PrintDbUtil du=new PrintDbUtil();
				//string path=du.OpenAccessDB();
				if(!File.Exists(path))
					return;
				//this.dbPath=path;
				this.ctlPath.Text=path;
				FileInfo fi=new FileInfo(path);
				this.dbName=fi.Name;
				this.ctlConnectionString.Text=Data.OleDb.DBCmd.GetProvider(ctlPath.Text,"mdb");
			}
		}

		private void btnExit_Click(object sender, System.EventArgs e)
		{
			this.DialogResult=DialogResult.No;
			this.Close();
		}

		private string GetConnection()
		{
			string cn= "";
			this.ctlConnectionString.Text="";
			if(this.ctlProviders.SelectedIndex==0)
			{
				if(this.ctlPath.Text.Length==0 )
				{
					Util.MsgBox.ShowWarning("Enter DB Path");
					return "";
				}
				cn=Data.OleDb.DBCmd.GetProvider(this.ctlPath.Text,"mdb");
				this.ctlConnectionString.Text=	cn;
				return cn ;
			}
			//localhost
			if(this.ctlServer.Text.Length==0 || this.ctlDatabase.Text.Length==0)
			{
				Util.MsgBox.ShowWarning("Enter Server name and Databse name");
				return "";
			}


			DataSource ds=new DataSource();
			switch(this.ctlProviders.SelectedIndex)
			{
				case 0:
					ds.ConnectionType=DBConnectionType.MicrosoftOleDb;
					ds.Name=this.DBName;
					break;
				case 1:
					ds.ConnectionType=DBConnectionType.MicrosoftSqlClient;
					ds.Name=this.DataBaseName;
					break;
			}

			ds.InitialCatalog=this.ctlDatabase.Text;
			ds.Name=this.ServerName;
			ds.FriendlyName=this.ctlFriendlyName.Text;
			if(this.chkIntegratedSecurity.Checked)
				ds.IntegratedSecurity=this.ctlIntegratedSecurity.Text;
			if(this.chkPersistSecurityInfo.Checked)
				ds.PersistSecurityInfo=this.ctlPersistSecurityInfo.Text;
			ds.TimeOut=this.ctlTimeOut.Text;

			if(this.checkAuthentication.Checked)
			{
				ds.UserName=this.ctlUserId.Text;
				ds.Password=this.ctlPassword.Text;
			}
			return ds.ConnectionString;
		}
		
		private string GetSqlConnection()
		{

			string cn= "";
			this.ctlConnectionString.Text="";
			if(this.ctlProviders.SelectedIndex==0)
			{
				if(this.ctlPath.Text.Length==0 )
				{
					Util.MsgBox.ShowWarning("Enter DB Path");
					return "";
				}
				cn=Data.OleDb.DBCmd.GetProvider(this.ctlPath.Text,"mdb");
				this.ctlConnectionString.Text=	cn;
				return cn ;
			}
			//localhost
			if(this.ctlServer.Text.Length==0 || this.ctlDatabase.Text.Length==0)
			{
				Util.MsgBox.ShowWarning("Enter Server name and Databse name");
				return "";
			}
			cn= string.Format("Data Source={0};Integrated Security=SSPI;Initial Catalog={1};",this.ctlServer.Text,this.ctlDatabase.Text);

			if(this.checkAuthentication.Checked)
			{
				if(this.ctlUserId.TextLength>0)
					cn+="User ID=" + ctlUserId.Text + ";";
				if(this.ctlPassword.TextLength>0)
					cn+="Password=" + ctlPassword.Text + ";";
			}
			else
			{
				//cn+=";integrated security=SSPI;persist security info=True;" ;
			}
			this.ctlConnectionString.Text=cn;
			return cn;
		}
	
		private bool TestConncteion()
		{
			string strConn=this.ctlConnectionString.Text;
			if(strConn.Length==0)
			{
				strConn=GetConnection();
			}
			if(strConn=="")
			{
				TestComplit=-1;
				return false;
			}
			this.provider=(DBProvider)this.ctlProviders.SelectedIndex;
			bool ok= TestConnection(provider,strConn);

			TestComplit=ok?1:-1;
			return ok;
		}

		private void checkAuthentication_CheckedChanged(object sender, System.EventArgs e)
		{
			bool enabled=this.checkAuthentication.Checked;
			this.ctlUserId.Enabled=enabled;
			this.ctlPassword.Enabled=enabled;
		}

		private int TestComplit=0;

		private void btnTestConn_Click(object sender, System.EventArgs e)
		{
			TestComplit=0;
			bool action=false;
			while(TestComplit==0)
			{
				Application.DoEvents();
				if(!action)
				{
					TestConncteion();
					action=true;
				}
			}
			if(TestComplit==1)
			{
				Util.MsgBox.ShowInfo("Connection Succssed");
				return;
			}
		}

		private void btnOk_Click(object sender, System.EventArgs e)
		{

			if(!IsValidConnection())
				return;

			conn=new CONECCTION();
			conn.ConnectionString=this.ctlConnectionString.Text;
			conn.Provider=this.DBProvider;
			conn.DBPath=this.DBPath;
			conn.ServerName=this.ServerName;
			switch(provider)
			{
				case DBProvider.OleDb:
					conn.DBName=this.DBName;
					break;
				case DBProvider.SqlServer:
					conn.DBName=this.DataBaseName;
					break;
			}
	
			this.DialogResult=DialogResult.OK;
			this.Hide();
			this.Close();
		}

		private CONECCTION conn;

		public CONECCTION Connection
		{
			get{return this.conn;}
		}

		private bool IsValidConnection()
		{
			if(this.ctlConnectionString.TextLength==0)
			{
				this.ctlConnectionString.Text=GetConnection();
			}

			if(this.ctlConnectionString.TextLength==0)
			{
				Util.MsgBox.ShowError("InvalidConnectionString");
				return false;
			}
			if(this.DBName.Length==0 && this.DataBaseName.Length==0)
			{
				Util.MsgBox.ShowError("InvalidDBname");
				return false;
			}
			if(this.DBProvider==DBProvider.OleDb && this.DBPath.Length==0)
			{
				Util.MsgBox.ShowError("InvalidDBPatch");
				return false;
			}
			if(this.DBProvider==DBProvider.SqlServer && this.ServerName.Length==0)
			{
				Util.MsgBox.ShowError("InvalidServerName");
				return false;
			}
			return true;
		}

		private void ctlTabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.ctlTabControl1.SelectedIndex==2)
			{
				this.ctlConnectionString.Text=GetConnection();
			}
		}

		public Data.DBProvider DBProvider
		{
			get{return this.provider;}
		}
		public string DBName
		{
			get{return this.dbName;}
		}
		public string DBPath
		{
			get{return this.ctlPath.Text;}
		}
		
		public string ConnectionString
		{
			get{return this.ctlConnectionString.Text;}
		}
		public string ServerName
		{
			get{return this.ctlServer.Text;}
		}
		public string DataBaseName
		{
			get{return this.ctlDatabase.Text;}
		}

		public bool TestConnection(DBProvider provider, string connectionString)
		{
			IDbConnection conn = null;

			switch (provider)
			{
				case DBProvider.OleDb:
					conn = new OleDbConnection(connectionString);
					//conn.DBName = this.DBName;
					break;
				case DBProvider.SqlServer:
					conn = new SqlConnection(connectionString);
					//conn.DBName = this.DataBaseName;
					break;
			}
			if (conn == null) return false;
			try
			{
				conn.Open();
				return true;
			}
			catch (Exception ex)
			{
				Util.MsgBox.ShowError(ex.Message);
				return false;
			}
			finally
			{
				conn.Close();
			}
		}

 
		#region DataSource
        
		[Serializable]
			public class DataSource
		{
			public Guid ID;
			public string Provider;
			public string IntegratedSecurity;
			public string Name;
			public string FriendlyName;
			public string PersistSecurityInfo;
			public string InitialCatalog;
			public string UserName;
			public string Password;
			public string TimeOut;
			public bool IsConnected; 
			public object Connection;
			public DBConnectionType ConnectionType;
			public bool IsEncrypted;
		

			public string ConnectionString
			{
				get
				{
					string cString="";
					switch (ConnectionType)
					{
						case DBConnectionType.MicrosoftSqlClient:
							cString=MicrosoftSqlClientConnectionString;
							break;
						case DBConnectionType.MicrosoftOleDb:
							cString=MicrosoftOleDbConnectionString;
							break;
						case DBConnectionType.OracleOleDb:
							cString=OracleOleDbConnectionString;
							break;
						case DBConnectionType.MySQL:
							cString=MySQLClientConnectionString;
							break;
						case DBConnectionType.SybaseASE:
							cString=MicrosoftSqlClientConnectionString;
							break;
						case DBConnectionType.Firebird:
							cString=FirebirdConnectionString;
							break;
						case DBConnectionType.DB2:
							cString=DB2ConnectionString;
							break;
						default:
							cString=MicrosoftSqlClientConnectionString;
							break;
					}
				
					return cString;
				}
			}
			private string MicrosoftOleDbConnectionString
			{
				get
				{
					if(IntegratedSecurity.Length==0)
					{
						return String.Format("Provider=sqloledb;Data Source={0};Initial Catalog={1};User Id={2};Password={3};",
							Name,
							InitialCatalog,
							UserName,
							Password);
					}
					else
					{
						return String.Format("Provider=sqloledb;Data Source={0};Initial Catalog={1};;Integrated Security=SSPI;",
							Name,
							InitialCatalog);
					}
				}
			}
		
			private string MicrosoftSqlClientConnectionString
			{
				get
				{
					string cString = "Data Source=" + Name + "; Initial Catalog=" + InitialCatalog;
					//"; Persist Security Info=" + PersistSecurityInfo +
					//"; Initial Catalog=" + InitialCatalog;
					//"; User ID=" + UserName + 
					//"; Password=" + Password + 
					//"; Connection Timeout=" + TimeOut;
					
					if(this.UserName!=null)
						cString += "; User ID=" + this.UserName; 
					if(this.Password!=null)
						cString += "; Password=" + this.Password; 
					if(this.PersistSecurityInfo!=null)
						cString += "; Persist Security Info=" + this.PersistSecurityInfo;
					if(this.IntegratedSecurity!=null)
						cString += "; Integrated Security=" + this.IntegratedSecurity;
					if(this.TimeOut!=null)
						cString += "; Connection Timeout=" + this.TimeOut;
					return cString;	
				}
			}
			private string OracleOleDbConnectionString
			{
				get
				{
					if(IntegratedSecurity.Length==0)
					{
						return String.Format("Data Source={0};User Id={1};Password={2}",
							Name,
							UserName,
							Password);
					}
					else
					{
						return String.Format("Data Source={0}",
							Name);
					}
				}
			}
			private string MySQLClientConnectionString
			{
				get
				{
					string pattern = "Data Source={0};user id={1}; password={2}; database={3};";
					return String.Format(pattern,
						Name,
						UserName,
						Password,
						InitialCatalog);
				}
			}
			private string FirebirdConnectionString
			{
				get
				{
					string pattern = "ServerType={0};User={1};Password={2};Dialect={3};Database={4}";
					return String.Format(pattern,
						"1",
						UserName,
						Password,
						"3",
						Name);
				}
			}
			private string DB2ConnectionString
			{
				get
				{
					string pattern = "Server={0};Database={3};UID={1};PWD={2}";
					return String.Format(pattern,
						Name,
						UserName,
						Password,
						InitialCatalog);
				}
			}
		}
		[Serializable]
			public class DataSourceCollection:CollectionBase
		{
			public virtual void Add( string Provider,
				string IntegratedSecurity,
				string Name,
				string PersistSecurityInfo,
				string InitialCatalog,
				string UserName,
				string Password,
				string TimeOut,
				bool IsConnected,
				DBConnectionType connectionType)
			{
				DataSource ds = new DataSource();
				ds.ID = Guid.NewGuid();
				ds.Provider = Provider;
				ds.IntegratedSecurity = IntegratedSecurity;
				ds.Name = Name;
				ds.PersistSecurityInfo = PersistSecurityInfo;
				ds.InitialCatalog = InitialCatalog;
				ds.UserName = UserName;
				ds.Password = Password;
				ds.TimeOut = TimeOut;
				ds.IsConnected = IsConnected;
				ds.ConnectionType=connectionType;
				this.Add(ds);
			}
			public virtual void Add(DataSource NewDataSource){this.List.Add(NewDataSource);}
			public virtual DataSource this[int Index]{get{return (DataSource)this.List[Index];}}
			public DataSource FindByID(Guid id)
			{
				foreach(DataSource ds in this)
				{
					if(ds.ID == id)
						return ds;
				}
				return null;
			}
		
			public void Delete(DataSource ds)
			{
				int index = 0;
				foreach(DataSource d in this)
				{
					if(d.ID == ds.ID)
					{
						this.RemoveAt(index);
						DataSourceFactory.Save(this);
						//					this.Save();
						return;
					}
					index ++;
				}
			
			}
		}
		public enum DBConnectionType
		{
			MicrosoftSqlClient=0,
			MicrosoftOleDb=1,
			OracleOleDb=2,
			MySQL=3,
			SybaseASE=4,
			Firebird=5,
			DB2=6
		}
		public class DataSourceFactory
		{
			public static DataSourceCollection GetDataSources()
			{
				DataSourceCollection dataSourceCollection;
				bool SaveEncrypted = false;

				string filename = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DataSources.config");
				if (!System.IO.File.Exists(filename))
					return (new DataSourceCollection());

				XmlSerializer ser = new XmlSerializer(typeof(DataSourceCollection));
				TextReader reader = new StreamReader(filename);
				dataSourceCollection = (DataSourceCollection)ser.Deserialize(reader);
				reader.Close();

				foreach (DataSource ds in dataSourceCollection)
				{
					if (!ds.IsEncrypted)
						SaveEncrypted = true;
					else
						ds.Password = Security.Decrypt(ds.Password);
				}

				if (SaveEncrypted)
					Save(dataSourceCollection);


				return dataSourceCollection;
			}
			public static void Save(DataSourceCollection dataSourceCollection)
			{
				foreach (DataSource ds in dataSourceCollection)
				{
					ds.IsEncrypted = true;
					ds.Password = Security.Encrypt(ds.Password);
				}

				string filename = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DataSources.config");
				XmlSerializer ser = new XmlSerializer(typeof(DataSourceCollection));
				TextWriter writer = new StreamWriter(filename);
				ser.Serialize(writer, dataSourceCollection);
				writer.Close();

				// Reset password
				foreach (DataSource ds in dataSourceCollection)
					ds.Password = Security.Decrypt(ds.Password);

			}
		}

		public class Security
		{
			private const string PASSWORD = "{1FCC37D8-E00B-4bef-99C3-529DC051082B}";

			public static string Encrypt(string clearText)
			{
				string Password = PASSWORD;
				byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(clearText);
				PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
					new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

				//			byte[] encryptedData = Encrypt(clearBytes, 
				//				pdb.GetBytes(32), pdb.GetBytes(16)); 

				MemoryStream ms = new MemoryStream();

				Rijndael alg = Rijndael.Create();

				alg.Key = pdb.GetBytes(32);
				alg.IV = pdb.GetBytes(16);

				CryptoStream cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write);

				cs.Write(clearBytes, 0, clearBytes.Length);

				cs.Close();

				byte[] encryptedData = ms.ToArray();

				return Convert.ToBase64String(encryptedData);
			}

			public static string Decrypt(string cipherText)
			{
				string Password = PASSWORD;
				byte[] cipherBytes = Convert.FromBase64String(cipherText);

				PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
					new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

				//			byte[] decryptedData = Decrypt(cipherBytes, 
				//				pdb.GetBytes(32), pdb.GetBytes(16)); 

				MemoryStream ms = new MemoryStream();

				Rijndael alg = Rijndael.Create();
				alg.Key = pdb.GetBytes(32);
				alg.IV = pdb.GetBytes(16);

				CryptoStream cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write);

				cs.Write(cipherBytes, 0, cipherBytes.Length);
				cs.Close();
				byte[] decryptedData = ms.ToArray();

				return System.Text.Encoding.Unicode.GetString(decryptedData);
			}


		}
		#endregion

	}
}
