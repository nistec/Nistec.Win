using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace WinCtlTest
{
   
	
      // Example Form for entering a string.
    public class StringInputDialog : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Button ok_button;
        private System.Windows.Forms.Button cancel_button;
        public System.Windows.Forms.TextBox inputTextBox;

        public StringInputDialog(string text)
        {
            InitializeComponent();
            inputTextBox.Text = text;
        }

        private void InitializeComponent()
        {
            this.ok_button = new System.Windows.Forms.Button();
            this.cancel_button = new System.Windows.Forms.Button();
            this.inputTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            this.ok_button.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.ok_button.Location = new System.Drawing.Point(180, 43);
            this.ok_button.Name = "ok_button";
            this.ok_button.TabIndex = 1;
            this.ok_button.Text = "OK";      
            this.ok_button.DialogResult = System.Windows.Forms.DialogResult.OK;            
            this.cancel_button.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.cancel_button.Location = new System.Drawing.Point(260, 43);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.TabIndex = 2;
            this.cancel_button.Text = "Cancel";            
            this.cancel_button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.inputTextBox.Location = new System.Drawing.Point(6, 9);
            this.inputTextBox.Name = "inputTextBox";
            this.inputTextBox.Size = new System.Drawing.Size(327, 20);
            this.inputTextBox.TabIndex = 0;
            this.inputTextBox.Text = "";            
            this.inputTextBox.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right);
            this.ClientSize = new System.Drawing.Size(342, 73);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                          this.inputTextBox,
                                                                          this.cancel_button,
                                                                          this.ok_button});
            this.MinimumSize = new System.Drawing.Size(350, 100);
            this.Name = "StringInputDialog";
            this.Text = "String Input Dialog";
            this.ResumeLayout(false);
        }		
    }

  

}



