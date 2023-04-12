using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace mControl.WinCtl.Controls.Design
{
	/// <summary>
	/// Summary description for FormColors.
	/// </summary>
	public class ControlColors : mControl.WinCtl.Forms.FormBase
    {
		private mControl.WinCtl.Controls.CtlButton panel2;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private CtlButton ctlButton1;
        private CtlButton ctlButtonXP1;
        private CtlButton ctlButtonXP2;
        private Panel panel1;
        private Panel panel6;
        private CtlComboBox ctlComboBox1;
        private CtlComboBox ctlComboBox2;
        private CtlComboBox ctlComboBox3;
        private CtlButtonBase ctlButton2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        public ControlColors()
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.panel2 = new mControl.WinCtl.Controls.CtlButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.ctlButton1 = new mControl.WinCtl.Controls.CtlButton();
            this.ctlButtonXP1 = new mControl.WinCtl.Controls.CtlButton();
            this.ctlButtonXP2 = new mControl.WinCtl.Controls.CtlButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.ctlComboBox1 = new mControl.WinCtl.Controls.CtlComboBox();
            this.ctlComboBox2 = new mControl.WinCtl.Controls.CtlComboBox();
            this.ctlComboBox3 = new mControl.WinCtl.Controls.CtlComboBox();
            this.ctlButton2 = new mControl.WinCtl.Controls.CtlButtonBase();
            this.SuspendLayout();
            // 
            // StyleGuideBase
            // 
            this.StyleGuideBase.AlternatingColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(225)))), ((int)(((byte)(239)))));
            this.StyleGuideBase.BorderColor = System.Drawing.Color.SteelBlue;
            this.StyleGuideBase.BorderHotColor = System.Drawing.Color.Blue;
            this.StyleGuideBase.CaptionColor = System.Drawing.Color.Navy;
            this.StyleGuideBase.ColorBrush1 = System.Drawing.Color.LightSteelBlue;
            this.StyleGuideBase.ColorBrush2 = System.Drawing.Color.AliceBlue;
            this.StyleGuideBase.FlatColor = System.Drawing.Color.AliceBlue;
            this.StyleGuideBase.FocusedColor = System.Drawing.Color.Navy;
            this.StyleGuideBase.FormColor = System.Drawing.Color.AliceBlue;
            this.StyleGuideBase.StylePlan = mControl.WinCtl.Controls.Styles.SteelBlue;
            // 
            // panel2
            // 
            this.panel2.ControlLayout = mControl.WinCtl.Controls.ControlsLayout.XpLayout;
            this.panel2.DialogResult = System.Windows.Forms.DialogResult.None;
            this.panel2.Location = new System.Drawing.Point(26, 24);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(195, 39);
            this.panel2.StylePainter = this.StyleGuideBase;
            this.panel2.TabIndex = 2;
            this.panel2.Text = "Button";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Gold;
            this.panel3.Location = new System.Drawing.Point(632, 57);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(64, 16);
            this.panel3.TabIndex = 3;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(60)))), ((int)(((byte)(116)))));
            this.panel4.Location = new System.Drawing.Point(632, 261);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(64, 16);
            this.panel4.TabIndex = 4;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(85)))), ((int)(((byte)(125)))), ((int)(((byte)(162)))));
            this.panel5.Location = new System.Drawing.Point(632, 214);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(64, 16);
            this.panel5.TabIndex = 5;
            // 
            // ctlButton1
            // 
            this.ctlButton1.ControlLayout = mControl.WinCtl.Controls.ControlsLayout.Gradient;
            this.ctlButton1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.ctlButton1.ImageIndex = 0;
            this.ctlButton1.Location = new System.Drawing.Point(302, 24);
            this.ctlButton1.Name = "ctlButton1";
            this.ctlButton1.Size = new System.Drawing.Size(131, 39);
            this.ctlButton1.TabIndex = 6;
            this.ctlButton1.Text = "Button";
            // 
            // ctlButtonXP1
            // 
            this.ctlButtonXP1.ControlLayout = mControl.WinCtl.Controls.ControlsLayout.System;
            this.ctlButtonXP1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.ctlButtonXP1.ImageIndex = 0;
            this.ctlButtonXP1.Location = new System.Drawing.Point(302, 69);
            this.ctlButtonXP1.Name = "ctlButtonXP1";
            this.ctlButtonXP1.Size = new System.Drawing.Size(131, 29);
            this.ctlButtonXP1.TabIndex = 7;
            this.ctlButtonXP1.Text = "Button";
            // 
            // ctlButtonXP2
            // 
            this.ctlButtonXP2.ControlLayout = mControl.WinCtl.Controls.ControlsLayout.XpLayout;
            this.ctlButtonXP2.DialogResult = System.Windows.Forms.DialogResult.None;
            this.ctlButtonXP2.ImageIndex = 0;
            this.ctlButtonXP2.Location = new System.Drawing.Point(28, 82);
            this.ctlButtonXP2.Name = "ctlButtonXP2";
            this.ctlButtonXP2.Size = new System.Drawing.Size(193, 33);
            this.ctlButtonXP2.StylePainter = this.StyleGuideBase;
            this.ctlButtonXP2.TabIndex = 8;
            this.ctlButtonXP2.Text = "Button";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(125)))), ((int)(((byte)(162)))));
            this.panel1.Location = new System.Drawing.Point(632, 283);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(64, 16);
            this.panel1.TabIndex = 9;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(85)))), ((int)(((byte)(125)))), ((int)(((byte)(162)))));
            this.panel6.Location = new System.Drawing.Point(632, 305);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(64, 16);
            this.panel6.TabIndex = 10;
            // 
            // ctlComboBox1
            // 
            this.ctlComboBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctlComboBox1.ControlLayout = mControl.WinCtl.Controls.ControlsLayout.XpLayout;
            this.ctlComboBox1.DrawMode = System.Windows.Forms.DrawMode.Normal;
            this.ctlComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.ctlComboBox1.IntegralHeight = false;
            this.ctlComboBox1.ItemHeight = 13;
            this.ctlComboBox1.Location = new System.Drawing.Point(26, 141);
            this.ctlComboBox1.Name = "ctlComboBox1";
            this.ctlComboBox1.Size = new System.Drawing.Size(195, 20);
            this.ctlComboBox1.TabIndex = 15;
            this.ctlComboBox1.Text = "ctlComboBox1";
            // 
            // ctlComboBox2
            // 
            this.ctlComboBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctlComboBox2.ControlLayout = mControl.WinCtl.Controls.ControlsLayout.Gradient;
            this.ctlComboBox2.DrawMode = System.Windows.Forms.DrawMode.Normal;
            this.ctlComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.ctlComboBox2.IntegralHeight = false;
            this.ctlComboBox2.ItemHeight = 13;
            this.ctlComboBox2.Location = new System.Drawing.Point(28, 178);
            this.ctlComboBox2.Name = "ctlComboBox2";
            this.ctlComboBox2.Size = new System.Drawing.Size(195, 20);
            this.ctlComboBox2.TabIndex = 16;
            this.ctlComboBox2.Text = "ctlComboBox2";
            // 
            // ctlComboBox3
            // 
            this.ctlComboBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctlComboBox3.ControlLayout = mControl.WinCtl.Controls.ControlsLayout.Flat;
            this.ctlComboBox3.DrawMode = System.Windows.Forms.DrawMode.Normal;
            this.ctlComboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.ctlComboBox3.IntegralHeight = false;
            this.ctlComboBox3.ItemHeight = 13;
            this.ctlComboBox3.Location = new System.Drawing.Point(26, 214);
            this.ctlComboBox3.Name = "ctlComboBox3";
            this.ctlComboBox3.Size = new System.Drawing.Size(195, 20);
            this.ctlComboBox3.TabIndex = 17;
            this.ctlComboBox3.Text = "ctlComboBox3";
            // 
            // ctlButton2
            // 
            this.ctlButton2.ControlLayout = mControl.WinCtl.Controls.ControlsLayout.Gradient;
            this.ctlButton2.DialogResult = System.Windows.Forms.DialogResult.None;
            this.ctlButton2.ImageIndex = 0;
            this.ctlButton2.Location = new System.Drawing.Point(311, 132);
            this.ctlButton2.Name = "ctlButton2";
            this.ctlButton2.Size = new System.Drawing.Size(122, 29);
            this.ctlButton2.TabIndex = 18;
            this.ctlButton2.Text = "Button";
            // 
            // ControlColors
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(728, 403);
            this.Controls.Add(this.ctlButton2);
            this.Controls.Add(this.ctlComboBox3);
            this.Controls.Add(this.ctlComboBox2);
            this.Controls.Add(this.ctlComboBox1);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ctlButtonXP2);
            this.Controls.Add(this.ctlButtonXP1);
            this.Controls.Add(this.ctlButton1);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Name = "ControlColors";
            this.Text = "Border";
            this.ResumeLayout(false);

		}
		#endregion
	}
}
