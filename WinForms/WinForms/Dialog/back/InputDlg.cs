using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using mControl.WinCtl.Controls;
using mControl.Util;
 
namespace mControl.WinCtl.Dlg
{

	/// <summary>
	/// Summary description for InputBox.
	/// </summary>
	public class InputDlg : mControl.WinCtl.Forms.FormBase
	{

	#region contructor
		private mControl.WinCtl.Controls.CtlTextBox Input;
		private System.Windows.Forms.Label LblMsg;
		private mControl.WinCtl.Controls.CtlPanel  Rect1;
        private mControl.WinCtl.Controls.CtlButton cmdOK;
        private mControl.WinCtl.Controls.CtlButton cmdExit;
		private System.ComponentModel.IContainer components;

		public InputDlg()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();


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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputDlg));
            this.Input = new mControl.WinCtl.Controls.CtlTextBox();
            this.LblMsg = new System.Windows.Forms.Label();
            this.Rect1 = new mControl.WinCtl.Controls.CtlPanel();
            this.cmdOK = new mControl.WinCtl.Controls.CtlButton();
            this.cmdExit = new mControl.WinCtl.Controls.CtlButton();
            this.Rect1.SuspendLayout();
            this.SuspendLayout();
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
            // Input
            // 
            this.Input.AcceptsReturn = true;
            this.Input.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Input.BackColor = System.Drawing.Color.White;
            this.Input.Location = new System.Drawing.Point(8, 8);
            this.Input.Name = "Input";
            this.Input.Size = new System.Drawing.Size(192, 20);
            this.Input.TabIndex = 0;
            // 
            // LblMsg
            // 
            this.LblMsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.LblMsg.BackColor = System.Drawing.Color.Transparent;
            this.LblMsg.Location = new System.Drawing.Point(50, 36);
            this.LblMsg.Name = "LblMsg";
            this.LblMsg.Size = new System.Drawing.Size(150, 16);
            this.LblMsg.TabIndex = 3;
            this.LblMsg.Click += new System.EventHandler(this.LblMsg_Click);
            // 
            // Rect1
            // 
            this.Rect1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Rect1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Rect1.ControlLayout = mControl.WinCtl.Controls.ControlLayout.Visual;
            this.Rect1.Controls.Add(this.Input);
            this.Rect1.Controls.Add(this.cmdOK);
            this.Rect1.Controls.Add(this.cmdExit);
            this.Rect1.Controls.Add(this.LblMsg);
            this.Rect1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Rect1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Rect1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.Rect1.Location = new System.Drawing.Point(0, 0);
            this.Rect1.Name = "Rect1";
            this.Rect1.Padding = new System.Windows.Forms.Padding(1);
            this.Rect1.Size = new System.Drawing.Size(208, 64);
            this.Rect1.StylePainter = this.StyleGuideBase;
            this.Rect1.TabIndex = 0;
            // 
            // cmdOK
            // 
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOK.Image = ((System.Drawing.Image)(resources.GetObject("cmdOK.Image")));
            this.cmdOK.Location = new System.Drawing.Point(28, 32);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(18, 20);
            this.cmdOK.TabIndex = 4;
            this.cmdOK.ToolTipText = "OK";
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.Location = new System.Drawing.Point(8, 32);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(18, 20);
            this.cmdExit.TabIndex = 5;
            this.cmdExit.ToolTipText = "Cancel";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // InputDlg
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.CancelButton = this.cmdExit;
            this.ClientSize = new System.Drawing.Size(208, 64);
            this.ControlBox = false;
            this.Controls.Add(this.Rect1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InputDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "InputBox";
            this.TopMost = true;
            this.SizeChanged += new System.EventHandler(this.FlyBox_SizeChanged);
            this.Controls.SetChildIndex(this.Rect1, 0);
            this.Rect1.ResumeLayout(false);
            this.Rect1.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

	#region StaticFunction
 	
		private object returnValue=null;

		public static object Open(Control parent,string DisplayValue,string msg,string Caption)
		{
			InputDlg f=new InputDlg();
			if(parent!=null)
			{
				f.RightToLeft= parent.RightToLeft;  
				Point pt=new Point(parent.Left,parent.Top-f.Height );  
				f.Location = parent.Parent.PointToScreen (pt);

				if(parent is IStyleCtl)
				{
					f.SetStyleLayout(((IStyleCtl)parent).CtlStyleLayout.Layout);
					f.SetChildrenStyle();
				}
			}
			f.Text =Caption;
			f.LblMsg.Text =msg; 
			f.Input.Text   =DisplayValue;
			f.Width =parent.Width; 
			f.SetRightToLeft();
	
			f.ShowDialog ();
			return f.returnValue; 			  
		}

		#endregion

	#region Methods
	


		private void SetRightToLeft()
		{
			if(this.RightToLeft==RightToLeft.Yes )
			{
				cmdExit.Left =Input.Left;
				cmdOK.Left =cmdExit.Left + cmdExit.Width +2 ;
				LblMsg.Width  =Input.Width -this.cmdOK.Width -this.cmdExit.Width -4;
				LblMsg.Left =cmdOK.Left + cmdOK.Width +2;
			}
			else
			{
				LblMsg.Left =Input.Left ;
				LblMsg.Width  =Input.Width -this.cmdOK.Width -this.cmdExit.Width -4;
				cmdExit.Left =Input.Left+Input.Width -cmdExit.Width ; 
				cmdOK.Left =cmdExit.Left - cmdOK.Width -2;
			}
		
		}

		private void cmdOK_Click(object sender, System.EventArgs e)
		{
			if(Input.TextLength>0)
			{
				returnValue=Input.Text; 
			}
			this.Close ();
		}

		private void cmdExit_Click(object sender, System.EventArgs e)
		{
			Input.Text ="";
			this.Close ();
		}

		private void FlyBox_SizeChanged(object sender, System.EventArgs e)
		{
		this.Input.Width =this.Width -15;
		this.Rect1.Width =this.Width;
		this.LblMsg.Left =Input.Left;
		this.LblMsg.Width=Input.Width;
		//this.LblMsg.Left =Input.Left+this.cmdExit.Width + this.cmdOK.Width +5  ;
		//this.LblMsg.Width=Input.Width-(this.cmdExit.Width + this.cmdOK.Width +5);
		//this.cmdOK.Left =this.Input.Left;   
		//this.cmdExit.Left =this.cmdOK.Left+this.cmdOK.Width +2;   
		}

		private void LblMsg_Click(object sender, System.EventArgs e)
		{
			Input.Text ="";
			this.Close ();
		}
		#endregion


	}//class
}//namespace

