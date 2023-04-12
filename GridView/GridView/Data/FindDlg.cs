using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

using mControl.ReportView;
using mControl.WinCtl.Controls;

namespace mControl.ReportView.Viewer
{

	public class FindDlg : mControl.WinCtl.Forms.FormBase
	{

		// Fields
		private FindInfo _FindInfo;//var7;
		private CtlPrintViewer _PrintViewer;//var8;
		internal CtlButton cbCancel;
		internal CtlButton cbFindNext;
		internal CtlCombo cbxFind;
		internal CtlCheckBox chkbMatchCase;
		internal CtlCheckBox chkbMatchWhole;
		private Container components;
		internal ErrorProvider ErrorProvider1;
		internal CtlGroupBox gbDirection;
		internal Label lblErrorText;
		internal Label lblFind;
		internal CtlRadioButton rbDown;
		internal CtlRadioButton rbUp;


		public FindDlg()
		{
			this.components = null;
			this.InitializeComponent();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && (this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

 
		private void InitializeComponent()
		{
			this.lblErrorText = new System.Windows.Forms.Label();
			this.gbDirection = new mControl.WinCtl.Controls.CtlGroupBox();
			this.rbDown = new mControl.WinCtl.Controls.CtlRadioButton();
			this.rbUp = new mControl.WinCtl.Controls.CtlRadioButton();
			this.chkbMatchCase = new mControl.WinCtl.Controls.CtlCheckBox();
			this.chkbMatchWhole = new mControl.WinCtl.Controls.CtlCheckBox();
			this.cbCancel = new mControl.WinCtl.Controls.CtlButton();
			this.cbFindNext = new mControl.WinCtl.Controls.CtlButton();
			this.cbxFind = new mControl.WinCtl.Controls.CtlCombo();
			this.lblFind = new System.Windows.Forms.Label();
			this.ErrorProvider1 = new System.Windows.Forms.ErrorProvider();
			this.gbDirection.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblErrorText
			// 
			this.lblErrorText.Location = new System.Drawing.Point(89, 39);
			this.lblErrorText.Name = "lblErrorText";
			this.lblErrorText.Size = new System.Drawing.Size(120, 16);
			this.lblErrorText.TabIndex = 15;
			// 
			// gbDirection
			// 
			this.gbDirection.Controls.Add(this.rbDown);
			this.gbDirection.Controls.Add(this.rbUp);
			this.gbDirection.Location = new System.Drawing.Point(137, 63);
			this.gbDirection.Name = "gbDirection";
			this.gbDirection.Size = new System.Drawing.Size(128, 48);
			this.gbDirection.TabIndex = 14;
			this.gbDirection.TabStop = false;
			this.gbDirection.Text = "Direction";
			// 
			// rbDown
			// 
			this.rbDown.Checked = true;
			this.rbDown.Location = new System.Drawing.Point(64, 16);
			this.rbDown.Name = "rbDown";
			this.rbDown.Size = new System.Drawing.Size(56, 16);
			this.rbDown.TabIndex = 1;
			this.rbDown.TabStop = true;
			this.rbDown.Text = "Down";
			this.rbDown.CheckedChanged += new System.EventHandler(this.var0);
			// 
			// rbUp
			// 
			this.rbUp.Location = new System.Drawing.Point(8, 16);
			this.rbUp.Name = "rbUp";
			this.rbUp.Size = new System.Drawing.Size(40, 16);
			this.rbUp.TabIndex = 0;
			this.rbUp.Text = "Up";
			this.rbUp.CheckedChanged += new System.EventHandler(this.var0);
			// 
			// chkbMatchCase
			// 
			this.chkbMatchCase.Location = new System.Drawing.Point(9, 87);
			this.chkbMatchCase.Name = "chkbMatchCase";
			this.chkbMatchCase.Size = new System.Drawing.Size(120, 24);
			this.chkbMatchCase.TabIndex = 13;
			this.chkbMatchCase.Text = "Match Case";
			this.chkbMatchCase.CheckedChanged += new System.EventHandler(this.var1);
			// 
			// chkbMatchWhole
			// 
			this.chkbMatchWhole.Location = new System.Drawing.Point(9, 63);
			this.chkbMatchWhole.Name = "chkbMatchWhole";
			this.chkbMatchWhole.Size = new System.Drawing.Size(128, 24);
			this.chkbMatchWhole.TabIndex = 12;
			this.chkbMatchWhole.Text = "Match whole word";
			this.chkbMatchWhole.CheckedChanged += new System.EventHandler(this.var2);
			// 
			// cbCancel
			// 
			this.cbCancel.Location = new System.Drawing.Point(273, 39);
			this.cbCancel.Name = "cbCancel";
			this.cbCancel.Size = new System.Drawing.Size(64, 24);
			this.cbCancel.TabIndex = 11;
			this.cbCancel.Text = "Cancel";
			this.cbCancel.Click += new System.EventHandler(this.var3);
			// 
			// cbFindNext
			// 
			this.cbFindNext.Location = new System.Drawing.Point(273, 7);
			this.cbFindNext.Name = "cbFindNext";
			this.cbFindNext.Size = new System.Drawing.Size(64, 24);
			this.cbFindNext.TabIndex = 10;
			this.cbFindNext.Text = "Find Next";
			this.cbFindNext.Click += new System.EventHandler(this.FindNext_Click);
			// 
			// cbxFind
			// 
			this.cbxFind.Location = new System.Drawing.Point(89, 7);
			this.cbxFind.Name = "cbxFind";
			this.cbxFind.Size = new System.Drawing.Size(176, 21);
			this.cbxFind.TabIndex = 9;
			this.cbxFind.Leave += new System.EventHandler(this.var5);
			// 
			// lblFind
			// 
			this.lblFind.Location = new System.Drawing.Point(9, 7);
			this.lblFind.Name = "lblFind";
			this.lblFind.Size = new System.Drawing.Size(64, 16);
			this.lblFind.TabIndex = 8;
			this.lblFind.Text = "Find What";
			// 
			// ErrorProvider1
			// 
			this.ErrorProvider1.ContainerControl = this;
			// 
			// FindDlg
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(346, 119);
			this.Controls.Add(this.lblErrorText);
			this.Controls.Add(this.gbDirection);
			this.Controls.Add(this.chkbMatchCase);
			this.Controls.Add(this.chkbMatchWhole);
			this.Controls.Add(this.cbCancel);
			this.Controls.Add(this.cbFindNext);
			this.Controls.Add(this.cbxFind);
			this.Controls.Add(this.lblFind);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FindDlg";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Find";
			this.TopMost = true;
			this.Closing += new System.ComponentModel.CancelEventHandler(this.var6);
			this.gbDirection.ResumeLayout(false);
			this.ResumeLayout(false);

		}

 
		private void var0(object sender, EventArgs e)
		{
			if (this.rbUp.Checked)
			{
				this._FindInfo.IsUp = true;
			}
			else
			{
				this._FindInfo.IsUp = false;
			}
		}

 
		private void var1(object sender, EventArgs e)
		{
			if (this.chkbMatchCase.Checked)
			{
				this._FindInfo.IsMatchCase = true;
			}
			else
			{
				this._FindInfo.IsMatchCase = false;
			}
		}

 
		private void var2(object sender, EventArgs e)
		{
			if (this.chkbMatchWhole.Checked)
			{
				this._FindInfo.IsMatchWhole = true;
			}
			else
			{
				this._FindInfo.IsMatchWhole = false;
			}
		}

 
		private void var3(object sender, EventArgs e)
		{
			this._FindInfo.Reset();
			this._PrintViewer.InvalidateRegions();
			base.Hide();
		}

		private void FindNext_Click(object sender, EventArgs e)
		{
			this.lblErrorText.Text = "";
			this.ErrorProvider1.SetError(this.lblErrorText, "");
			if (this.cbxFind.Text.Length > 0)
			{
				this._FindInfo.Text = this.cbxFind.Text;
				this._PrintViewer.FindText();
			}
		}

 
		private void var5(object sender, EventArgs e)
		{
			bool flag1 = false;
			for (int num1 = 0; num1 < this.cbxFind.Items.Count; num1++)
			{
				if (string.Compare(this.cbxFind.Text, this.cbxFind.Items[num1].ToString()) == 0)
				{
					flag1 = true;
					break;
				}
			}
			if (!flag1)
			{
				this.cbxFind.Items.Add(this.cbxFind.Text);
			}
		}

 
		private void var6(object sender, CancelEventArgs e)
		{
			e.Cancel = true;
			this._FindInfo.Reset();
			this._PrintViewer.InvalidateRegions();
			base.Hide();
		}

 
		internal CtlPrintViewer printViewer
		{
			set
			{
				this._PrintViewer = value;
				this._FindInfo = this._PrintViewer.findInfo;
			}
		}

	}
 
}
