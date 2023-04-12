using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Security.Permissions;
using Nistec.Win32;


using Nistec.Win;
  
namespace Nistec.WinForms
{

	/// <summary>
	/// Summary description for InputBox.
	/// </summary>
	public class ProgressBox : Nistec.WinForms.FormBase
	{

	#region contructor

		private Nistec.WinForms.McProgressBar  progressBar1;
		private System.Windows.Forms.Label LblMsg;
		private Nistec.WinForms.McButton cmdExit;
		private Nistec.WinForms.McProgressBar progressBar2;
		private Nistec.WinForms.McPanel panel1;
		private Nistec.WinForms.McMove ctlMove;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ProgressBox()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
            this.Size =this.panel1.Size ;  
		}
        public ProgressBox(IStyle style)
        {
            InitializeComponent();
            this.Size = this.panel1.Size;
            this.SetStyleLayout(style);
        }
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				//this.Controls.Remove(this.panel1);
	
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgressBox));
            this.progressBar1 = new Nistec.WinForms.McProgressBar();
            this.LblMsg = new System.Windows.Forms.Label();
            this.cmdExit = new Nistec.WinForms.McButton();
            this.progressBar2 = new Nistec.WinForms.McProgressBar();
            this.panel1 = new Nistec.WinForms.McPanel();
            this.ctlMove = new Nistec.WinForms.McMove();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctlMove)).BeginInit();
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
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.Color.White;
            this.progressBar1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.progressBar1.Location = new System.Drawing.Point(8, 8);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(232, 15);
            this.progressBar1.TabIndex = 5;
            this.progressBar1.TabStop = false;
            this.progressBar1.Value = 0;
            this.progressBar1.Visible = false;
            this.progressBar1.Finished += new System.EventHandler(this.progressBar1_Finished);
            // 
            // LblMsg
            // 
            this.LblMsg.BackColor = System.Drawing.Color.Transparent;
            this.LblMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMsg.Location = new System.Drawing.Point(16, 56);
            this.LblMsg.Name = "LblMsg";
            this.LblMsg.Size = new System.Drawing.Size(224, 24);
            this.LblMsg.TabIndex = 3;
            // 
            // cmdExit
            // 
            this.cmdExit.ControlLayout = Nistec.WinForms.ControlLayout.Visual;
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cmdExit.Location = new System.Drawing.Point(8, 80);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(56, 24);
            this.cmdExit.TabIndex = 0;
            this.cmdExit.Text = "Cancel";
            this.cmdExit.ToolTipText = "Cancel";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // progressBar2
            // 
            this.progressBar2.BackColor = System.Drawing.Color.White;
            this.progressBar2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.progressBar2.Location = new System.Drawing.Point(8, 32);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(232, 15);
            this.progressBar2.TabIndex = 6;
            this.progressBar2.TabStop = false;
            this.progressBar2.Value = 0;
            this.progressBar2.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.ctlMove);
            this.panel1.Controls.Add(this.cmdExit);
            this.panel1.Controls.Add(this.LblMsg);
            this.panel1.Controls.Add(this.progressBar2);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.panel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(1);
            this.panel1.Size = new System.Drawing.Size(248, 112);
            this.panel1.TabIndex = 7;
            this.panel1.Text = "panel1";
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // ctlMove
            // 
            this.ctlMove.BackColor = System.Drawing.Color.White;
            this.ctlMove.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.ctlMove.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlMove.Image = ((System.Drawing.Image)(resources.GetObject("ctlMove.Image")));
            this.ctlMove.Location = new System.Drawing.Point(208, 80);
            this.ctlMove.Name = "ctlMove";
            this.ctlMove.ReadOnly = false;
            this.ctlMove.Size = new System.Drawing.Size(20, 20);
            this.ctlMove.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.ctlMove.TabIndex = 7;
            this.ctlMove.TabStop = false;
            // 
            // ProgressBox
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(250, 112);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(1, 1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgressBox";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ProgressBox";
            this.TopMost = true;
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ctlMove)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

	#region Property
        private bool closeOnFinish = true;
		private ProgressMode progsMode=ProgressMode.Single;

		public enum ProgressMode
		{
			Single,
			Double,
			Loop
		}

        public string ProgressText
        {
            get { return this.LblMsg.Text; }
            set { this.LblMsg.Text = value; }
        }
        public bool CloseOnFinish
        {
            get { return closeOnFinish; }
            set { closeOnFinish = value; }
        }

		#endregion

	#region override 

		[UseApiElements("WindowExStyles.WS_EX_TOPMOST")]
		protected override CreateParams CreateParams 
		{
			[SecurityPermission(SecurityAction.LinkDemand)]
			get
			{
				CreateParams cp = base.CreateParams;

				cp.ExStyle |= (int)Nistec.Win32.WindowExStyles.WS_EX_TOPMOST; 

				return cp;
			}
		}

		[UseApiElements("ShowWindow")]
		private void ShowProgressBar(Form parent,string msg,string Caption,ProgressMode mode)
		{
			if(parent!=null)
			{
				if(parent is ILayout)
				{
					SetStyleLayout(((ILayout)parent).StylePainter.Layout);
				}

				this.Owner =parent;
			}
			this.Text =Caption;
			this.LblMsg.Text =msg;

			switch(mode)
			{
				case ProgressMode.Double:
					progressBar1.Visible =true; 
					progressBar2.Visible =true; 
					break;
				default:
					progressBar1.Visible =true; 
					progressBar2.Visible =false; 
					break;
			}
			this.TopMost =false;
//			Nistec.Win32.WinAPI.ShowWindow(this.Handle,4);
//
//			if(mode==ProgressMode.Loop)
//			{
//				this.DoLoop(); 
//			}
		}

		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed (e);
			//this.Controls.Remove(this.panel1);

			//this.Dispose(true);
		}

		public static ProgressBox ShowProgressBar(Form parent,string msg,	string Caption)
		{
			ProgressBox f=new ProgressBox();
			f.ShowProgressBar(parent, msg,Caption,ProgressMode.Single);
            Nistec.Win32.WinAPI.ShowWindow(f.Handle, WindowShowStyle.ShowNormalNoActivate);
			return f;
		}

		public static ProgressBox ShowProgressBars(Form parent,string msg,	string Caption)
		{
			ProgressBox f=new ProgressBox();
			f.ShowProgressBar(parent, msg,Caption,ProgressMode.Double);
            Nistec.Win32.WinAPI.ShowWindow(f.Handle, WindowShowStyle.ShowNormalNoActivate);
			return f;
		}

		public static ProgressBox ShowProgressLoop(Form parent,string msg,	string Caption)
		{
			ProgressBox f=new ProgressBox();
			f.ShowProgressBar(parent, msg,Caption,ProgressMode.Loop);
            Nistec.Win32.WinAPI.ShowWindow(f.Handle, WindowShowStyle.ShowNormalNoActivate);

			f.DoLoop(); 
			return f;
		}

		#endregion

	#region Move

		private bool isMouseDown=false;
		private int x;
		private int y;

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown (e);
			isMouseDown=true;
			x=e.X;
			y=e.Y;
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp (e);
			isMouseDown=false;
			x=0;
			y=0;
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove (e);
			try
			{
				if(isMouseDown)
				{
					Point p=  new Point(e.X-this.x,e.Y-this.y);
					this.Location=PointToScreen(p);
				}
			}
			catch{}
		}
		private void panel1_MouseDown(object sender, MouseEventArgs e)
		{
			this.OnMouseDown(e);
		}

		private void panel1_MouseUp(object sender, MouseEventArgs e)
		{
			this.OnMouseUp(e);
		}

		private void panel1_MouseMove(object sender, MouseEventArgs e)
		{
			this.OnMouseMove(e);
		}


		#endregion

	#region Methods
		private void cmdExit_Click(object sender, System.EventArgs e)
		{
			this.progressBar1.DoLoop();
			this.Close ();
		}

		public McProgressBar ProgressBar1
		{
			get{return this.progressBar1;}  
		}

		public McProgressBar ProgressBar2
		{
			get{return this.progressBar2;}  
		}

		public void DoLoop()
		{
			this.progressBar1.DoLoop();
		}

		#endregion

		private void progressBar1_Finished(object sender, EventArgs e)
		{
            if (this.progsMode != ProgressMode.Loop && CloseOnFinish)
			{
                CloseWindow();
			}
		}
        private delegate void CloseCallBack();

        private void CloseWindow()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new CloseCallBack(CloseWindow), null);
            }
            else
            {
                this.Close();
            }
        }

	}//class
}//namespace

