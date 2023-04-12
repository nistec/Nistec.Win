using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;

namespace MControl.Util.Net
{

	/// <summary>
	/// Summary description for Registry dialog.
	/// </summary>
	internal class NetLogo : System.Windows.Forms.Form
	{

	#region Members
		private	string v_55="0";
		private string v_56="";
		private string v_57="";

		//height = 216=non  320=visible
		private const int detailsHide=208;// 216;
		private const int detailsShow=304;//  296;

		private bool detailVisible;
		private static bool _Load;


	#endregion

	#region contructor

		private System.Windows.Forms.Panel panel6;
		private System.Windows.Forms.TextBox txtDetails;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.LinkLabel cmdExit;
		private System.Windows.Forms.LinkLabel cmdSite;
		private System.Windows.Forms.LinkLabel cmdDetails;
		private System.Windows.Forms.LinkLabel cmdRegistry;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Timer timer1;
		private System.ComponentModel.IContainer components;

		public NetLogo(string v_53,string v_11,string v_54)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
            Application.DoEvents();
			v_55=v_53;
			v_56=v_11;
			v_57=v_54;
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
		#endregion

	#region Windows Form Designer generated code
		/// <summary>
		/// Required v_13 for Designer support - do not modify
		/// the contents of this v_13 with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NetLogo));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cmdExit = new System.Windows.Forms.LinkLabel();
            this.cmdSite = new System.Windows.Forms.LinkLabel();
            this.cmdDetails = new System.Windows.Forms.LinkLabel();
            this.cmdRegistry = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.txtDetails = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(16, 56);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(328, 98);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox1, "www.MControlnet.com");
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.ForeColor = System.Drawing.Color.White;
            this.cmdExit.LinkColor = System.Drawing.Color.White;
            this.cmdExit.Location = new System.Drawing.Point(16, 176);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(32, 16);
            this.cmdExit.TabIndex = 14;
            this.cmdExit.TabStop = true;
            this.cmdExit.Text = "Exit";
            this.toolTip1.SetToolTip(this.cmdExit, "Exit");
            this.cmdExit.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.cmdExit_LinkClicked);
            // 
            // cmdSite
            // 
            this.cmdSite.ForeColor = System.Drawing.Color.White;
            this.cmdSite.LinkColor = System.Drawing.Color.White;
            this.cmdSite.Location = new System.Drawing.Point(232, 176);
            this.cmdSite.Name = "cmdSite";
            this.cmdSite.Size = new System.Drawing.Size(120, 16);
            this.cmdSite.TabIndex = 13;
            this.cmdSite.TabStop = true;
            this.cmdSite.Text = "www.MControlnet.com";
            this.cmdSite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.cmdSite_LinkClicked);
            // 
            // cmdDetails
            // 
            this.cmdDetails.ForeColor = System.Drawing.Color.White;
            this.cmdDetails.LinkColor = System.Drawing.Color.White;
            this.cmdDetails.Location = new System.Drawing.Point(64, 176);
            this.cmdDetails.Name = "cmdDetails";
            this.cmdDetails.Size = new System.Drawing.Size(56, 16);
            this.cmdDetails.TabIndex = 15;
            this.cmdDetails.TabStop = true;
            this.cmdDetails.Text = "Details";
            this.toolTip1.SetToolTip(this.cmdDetails, "Show details");
            this.cmdDetails.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.cmdDetails_LinkClicked);
            // 
            // cmdRegistry
            // 
            this.cmdRegistry.ForeColor = System.Drawing.Color.White;
            this.cmdRegistry.LinkColor = System.Drawing.Color.White;
            this.cmdRegistry.Location = new System.Drawing.Point(16, 16);
            this.cmdRegistry.Name = "cmdRegistry";
            this.cmdRegistry.Size = new System.Drawing.Size(72, 16);
            this.cmdRegistry.TabIndex = 15;
            this.cmdRegistry.TabStop = true;
            this.cmdRegistry.Text = "Registration";
            this.toolTip1.SetToolTip(this.cmdRegistry, "Registration setting");
            this.cmdRegistry.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.cmdRegistry_LinkClicked);
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(152, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 16);
            this.label1.TabIndex = 16;
            this.label1.Text = "MControl-net Version 2.2.0 ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel6
            // 
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(200, 100);
            this.panel6.TabIndex = 0;
            // 
            // txtDetails
            // 
            this.txtDetails.BackColor = System.Drawing.Color.White;
            this.txtDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDetails.Location = new System.Drawing.Point(16, 216);
            this.txtDetails.Multiline = true;
            this.txtDetails.Name = "txtDetails";
            this.txtDetails.ReadOnly = true;
            this.txtDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDetails.Size = new System.Drawing.Size(328, 72);
            this.txtDetails.TabIndex = 12;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.SteelBlue;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.pictureBox2);
            this.panel3.Controls.Add(this.txtDetails);
            this.panel3.Controls.Add(this.cmdRegistry);
            this.panel3.Controls.Add(this.cmdDetails);
            this.panel3.Controls.Add(this.cmdSite);
            this.panel3.Controls.Add(this.cmdExit);
            this.panel3.Controls.Add(this.pictureBox1);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(360, 208);
            this.panel3.TabIndex = 13;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.White;
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(152, 184);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(8, 8);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 17;
            this.pictureBox2.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox2, "www.MControlnet.com");
            this.pictureBox2.Visible = false;
            // 
            // NetLogo
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(360, 208);
            this.Controls.Add(this.panel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NetLogo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registry";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

	#region Methods

        private static bool registred = false;
        private static bool loaded = false;

        public static bool Registred
		{
            get { return registred; }
		}

        public static bool IsLoad
        {
            get { return loaded;}// retValue; }
        }

        public static void Open(string v_53, string v_11, string v_54, string v_32, bool showDetails, string v_13, string v_17)
        {
            if (_Load || v_17 == "CHK" || v_17 == "WEB")
            {
                return;// false;
            }

            pv_53 = v_53;
            pv_11 = v_11;
            pv_54 = v_54;
            pv_32 = v_32;
            pshowDetails = showDetails;
            pv_13 = v_13;
            //pv_17 = v_17;

            Thread th = new Thread(new ThreadStart(NetLogo.ShowLogo));
            th.Start();
            Thread.Sleep(2000);
           
             //return NetLogo.registred;


        }
        //static Thread th;
        static string pv_53;
        static string pv_11;
        static string pv_54;
        static string pv_32;
        static bool pshowDetails;
        static string pv_13;
        //static string pv_17;

        private void timer1_Tick(object sender, EventArgs e)
        {
            Application.DoEvents();

            //if (!_Load)
            //{
            //    this.Close();
            //}
        }
        //[UseApiElements("ShowWindow")]
        private void ShowWindow()
        {
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);

           // this.TopMost =true;
            //MControl.Win32.WinAPI.ShowWindow(this.Handle, 4);
            this.Show();
            this.timer1.Interval = 200;
            this.timer1.Start();
            this.timer1.Enabled = true;
           
            this.Invalidate(true);
            this.txtDetails.Text = pv_32 + "\r\n" + " Method: " + pv_13;
            this.label1.Text = pv_11 + " registry";
            this.TopMost = true;
            if (pshowDetails)
            {
                this.DetailTogle(true);
            }

            Application.DoEvents();
            if (pshowDetails)
            {
                this.DetailTogle(true);
            }

            while (_Load)
            {
                Application.DoEvents();
            }
         }

        private static void ShowLogo()
        {
            NetLogo frm = new NetLogo(pv_53, pv_11, pv_54);
            frm.ShowWindow();
            //frm.txtDetails.Text = pv_32 + "\r\n" + " Method: " + pv_13;
            //frm.label1.Text = pv_11 + " registry";
            //frm.TopMost = true;
            //if (pshowDetails)
            //{
            //    frm.DetailTogle(true);
            //}

            //Application.DoEvents();
            //if (pshowDetails)
            //{
            //    frm.DetailTogle(true);
            //}
            //frm.ShowWindow();


            //if (pv_17 == "APP")
            //{
            //    frm.Show();
            //    //frm.ShowDialog();
            //    //NetLogo.registred = f.retValue;// dr==DialogResult.OK;

            //}
            //else//v_17=="CTL"
            //{
            //    frm.Show();
            //}
            //while (true)//NetLogo.loaded)
            //{
            //    Application.DoEvents();
            //}

        }

        //public static bool Open(string v_53, string v_11, string v_54, string v_32, bool showDetails, string v_13, string v_17)
        //{
        //    if (_Load || v_17 == "CHK" || v_17 == "WEB")
        //    {
        //        return false;
        //    }

        //    NetLogo frm = new NetLogo(v_53, v_11, v_54);
        //    frm.txtDetails.Text = v_32 + "\r\n" + " Method: " + v_13;
        //    frm.label1.Text = v_11 + " registry";
        //    frm.TopMost = true;
        //    if (showDetails)
        //    {
        //        frm.DetailTogle(true);
        //    }

        //    Application.DoEvents();
        //    if (showDetails)
        //    {
        //        frm.DetailTogle(true);
        //    }
        //    if (v_17 == "APP")
        //    {
        //        frm.ShowDialog();
        //        //NetLogo.registred = f.retValue;// dr==DialogResult.OK;

        //    }
        //    else//v_17=="CTL"
        //    {
        //        frm.Show();
        //    }
        //    return NetLogo.registred;
        //}

		#endregion

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad (e);
			_Load=true;
		}

		protected override void OnClosed(EventArgs e)
		{
            this.Hide(); 
            base.OnClosed(e);
            NetLogo.loaded = false;
			_Load=false;
		}

		private void btnDetails_Click(object sender, System.EventArgs e)
		{
		 DetailTogle();
		}

		private void DetailTogle(bool visible)
		{
			if(this.detailVisible !=visible)
			{
				DetailTogle();
			}
		}

		private void cmdRegistry_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			string v_60=GetKeyFile();
			if(v_60!="")
			{
				int v_44=ED.ed_3(v_60);
				if(v_44>0)
				{
					//retValue=true;
                    NetLogo.registred = true;
					MessageBox.Show("License key Registrated successfuly","MControl-net");
					this.Close();
				}
			}

		}

		private void cmdSite_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("IExplore.exe", this.cmdSite.Text);
		}

		private void pictureBox1_Click(object sender, System.EventArgs e)
		{
			System.Diagnostics.Process.Start("IExplore.exe", this.cmdSite.Text);
		}

		private void cmdDetails_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
		  DetailTogle();
		}

		private void cmdExit_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			if(MessageBox.Show ("Close Without registry ? ","MControl",MessageBoxButtons.YesNo ,MessageBoxIcon.Question  )==DialogResult.Yes )
			{
				this.Close(); 
			}
		}

		private void DetailTogle()
		{
      
			if(this.detailVisible)
			{
				this.Height= detailsHide;
				this.detailVisible=false;
			}
			else
			{
				this.Height= detailsShow;
				this.detailVisible=true;
			}
		}

		private string GetKeyFile()
		{
			//string file=""; 
			OpenFileDialog openFile = new OpenFileDialog();
			openFile.DefaultExt = "mck";
			// The Filter property requires a search string after the pipe ( | )
			openFile.Filter = "File Key (*.mck)|*.mck";
			openFile.ShowDialog();
			string fileName = openFile.FileName.ToString ();
			//if( fileName.Length > 0 ) 
			//{
	            
			// Read the file as one string.
			//System.IO.StreamReader _File =
			//	new System.IO.StreamReader(fileName);
			//file = _File.ReadToEnd();
			//_File.Close();
			//}
			return fileName;
		}

	}//class
}//namespace

