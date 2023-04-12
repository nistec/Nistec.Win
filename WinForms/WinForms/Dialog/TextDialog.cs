using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Nistec.WinForms
{

	/// <summary>
    /// Summary description for TextDialog.
	/// </summary>
	public class TextDialog : Form// Nistec.WinForms.FormBase
	{

	#region contructor

		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.Button cmdExit;
		private System.Windows.Forms.Panel panelFooter;
		private System.Windows.Forms.RichTextBox Input;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        public TextDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdExit = new System.Windows.Forms.Button();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.Input = new System.Windows.Forms.RichTextBox();
            this.panelFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(0, 0);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(56, 24);
            this.cmdOK.TabIndex = 1;
            this.cmdOK.Text = "OK";
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdExit.Location = new System.Drawing.Point(56, 0);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(56, 24);
            this.cmdExit.TabIndex = 2;
            this.cmdExit.Text = "Cancel";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // panelFooter
            // 
            this.panelFooter.Controls.Add(this.cmdExit);
            this.panelFooter.Controls.Add(this.cmdOK);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 279);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.panelFooter.Size = new System.Drawing.Size(370, 24);
            this.panelFooter.TabIndex = 4;
            // 
            // Input
            // 
            this.Input.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Input.Location = new System.Drawing.Point(0, 0);
            this.Input.Multiline = true;
            this.Input.Name = "Input";
            this.Input.Size = new System.Drawing.Size(370, 279);
            this.Input.TabIndex = 5;
            // 
            // TextEditor
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(370, 303);
            this.Controls.Add(this.Input);
            this.Controls.Add(this.panelFooter);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TextDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Text Dialog";
            this.TopMost = true;
            this.Controls.SetChildIndex(this.panelFooter, 0);
            this.Controls.SetChildIndex(this.Input, 0);
            this.panelFooter.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

	#region StaticFunction
 	
		private string mReturnValue="";

        public static string Open(string Value, string Caption,bool readOnly)
        {
            TextDialog f = new TextDialog();
            f.Text = Caption;
            f.Input.Text = Value;
            f.Input.ReadOnly = readOnly;
            f.cmdExit.Visible = !readOnly;
            f.ShowDialog();
            return f.mReturnValue;
        }

		public static string Open(string Value,string Caption,System.Windows.Forms.RightToLeft rightToleft )
		{
            TextDialog f = new TextDialog();
			f.Text =Caption;
			f.Input.Text =Value;
			f.RightToLeft=rightToleft;
			f.ShowDialog ();
			return f.mReturnValue; 			  
		}

        public static void Open(string Value, string Caption, System.Windows.Forms.RightToLeft rightToleft,bool readOnly)
        {
            TextDialog f = new TextDialog();
            f.Text = Caption;
            f.Input.Text = Value;
            f.RightToLeft = rightToleft;
            f.Input.ReadOnly = readOnly;
            f.cmdExit.Visible = !readOnly;
            f.ShowDialog();
        }
		#endregion

	#region Property

		public string ReturnValue
		{
			get{return mReturnValue;}
		}

	#endregion

	#region Methods

		
		private void cmdOK_Click(object sender, System.EventArgs e)
		{
			DialogResult=DialogResult.OK; 
			mReturnValue=Input.Text; 
			this.Close ();
		}

		private void cmdExit_Click(object sender, System.EventArgs e)
		{
            mReturnValue = ""; 
			this.Close ();
		}

		#endregion

	}//class
}//namespace

