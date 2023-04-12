using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;


  
namespace Nistec.WinForms
{

	/// <summary>
	/// Summary description for InputBox.
	/// </summary>
	public class InputBox : Nistec.WinForms.McForm
	{

	#region contructor
		private Nistec.WinForms.McTextBox  Input;
		private System.Windows.Forms.Label LblMsg;
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.Button cmdExit;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public InputBox()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			//this.Input.m_netFram=true;
	
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
            this.Input = new Nistec.WinForms.McTextBox();
            this.LblMsg = new System.Windows.Forms.Label();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdExit = new System.Windows.Forms.Button();
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
            // Input
            // 
            this.Input.AcceptsReturn = true;
            this.Input.Location = new System.Drawing.Point(70, 41);
            this.Input.MaxLength = 0;
            this.Input.Name = "Input";
            this.Input.Size = new System.Drawing.Size(243, 20);
            this.Input.TabIndex = 0;
            // 
            // LblMsg
            // 
            this.LblMsg.BackColor = System.Drawing.Color.Transparent;
            this.LblMsg.Location = new System.Drawing.Point(70, 70);
            this.LblMsg.Name = "LblMsg";
            this.LblMsg.Size = new System.Drawing.Size(243, 45);
            this.LblMsg.TabIndex = 3;
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(8, 40);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(56, 24);
            this.cmdOK.TabIndex = 1;
            this.cmdOK.Text = "OK";
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdExit.Location = new System.Drawing.Point(8, 64);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(56, 24);
            this.cmdExit.TabIndex = 2;
            this.cmdExit.Text = "Cancel";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // InputBox
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.CancelButton = this.cmdExit;
            this.CaptionVisible = true;
            this.ClientSize = new System.Drawing.Size(326, 134);
            this.ControlLayout = Nistec.WinForms.ControlLayout.Visual;
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.Input);
            this.Controls.Add(this.LblMsg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InputBox";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InputBox";
            this.TopMost = true;
            this.Controls.SetChildIndex(this.LblMsg, 0);
            this.Controls.SetChildIndex(this.Input, 0);
            this.Controls.SetChildIndex(this.cmdExit, 0);
            this.Controls.SetChildIndex(this.cmdOK, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

	#region StaticFunction
 	
		private string returnValue=string.Empty;

		public static string Open(Form parent,string DisplayValue,string msg,string Caption)
		{
			InputBox f=new InputBox();
			if(parent!=null)
			{
                if (parent is ILayout)
                {
                    f.StylePainter = ((ILayout)parent).StylePainter;
                    //base.StyleGuideBase=((ILayout)parent).StylePainter as StyleGuide;//.GetCurrentGuide();
                    //f.SetStyleLayout(((ILayout)parent).LayoutManager.Layout);
                    f.SetChildrenStyle();
                }


				f.RightToLeft= parent.RightToLeft;  
				f.Owner =parent;
			}
			f.Text =Caption;
			f.LblMsg.Text =msg; 
			f.Input.Text   =DisplayValue;
			//f.SetRightToLeft();
			f.ShowDialog ();
			return f.returnValue; 			  
		}

		#endregion

	#region Methods

		private void SetRightToLeft()
		{
			if(this.RightToLeft==RightToLeft.Yes )
			{
			 cmdOK.Left =Input.Left;
			 cmdExit.Left =Input.Left;
             LblMsg.Left =Input.Left + cmdOK.Width +5;
			}
			else
			{
				LblMsg.Left =Input.Left ;
			    cmdOK.Left =Input.Left + LblMsg.Width +5 ;
				cmdExit.Left =cmdOK.Left; 
			}
		
		}

		private void cmdOK_Click(object sender, System.EventArgs e)
		{
			if(Input.Text.Length>0)
			{
				returnValue=Input.Text; 
			}
			    this.DialogResult=DialogResult.OK;
				this.Close ();
		}

		private void cmdExit_Click(object sender, System.EventArgs e)
		{
			returnValue=null;
			Input.Text ="";
			this.DialogResult=DialogResult.No;
			this.Close ();
		}

		#endregion

	
	}//class
}//namespace

