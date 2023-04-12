using System;
using System.Windows.Forms;
using System.Resources;
using System.Drawing;


namespace MControl.Printing.View.Design
{
    public class InsertFieldDlg : System.Windows.Forms.Form//MControl.WinForms.FormBase
	{
		#region NetFram

        //private void //NetReflectedFram()
        //{
        //    this.cbCancel.//NetReflectedFram("ba7fa38f0b671cbc");
        //    this.cbOK.//NetReflectedFram("ba7fa38f0b671cbc");
        //}

		#endregion

		public InsertFieldDlg()
		{
			//netFramwork.//NetReflectedFram();
			this.components = null;
			this.InitializeComponent();
			//NetReflectedFram();
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
            this.cbCancel = new Button();
            this.cbOK = new Button();
            this.txtField = new System.Windows.Forms.TextBox();
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
            this.StyleGuideBase.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.FocusedColor = System.Drawing.Color.RoyalBlue;
            this.StyleGuideBase.FormColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.StyleGuideBase.StylePlan = MControl.WinForms.Styles.Desktop;
            // 
            // cbCancel
            // 
            this.cbCancel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cbCancel.Location = new System.Drawing.Point(246, 44);
            this.cbCancel.Name = "cbCancel";
            this.cbCancel.Size = new System.Drawing.Size(56, 24);
            this.cbCancel.StylePainter = this.StyleGuideBase;
            this.cbCancel.TabIndex = 7;
            this.cbCancel.Text = "Cancel";
            this.cbCancel.ToolTipText = "Cancel";
            this.cbCancel.Click += new System.EventHandler(this.var0);
            // 
            // cbOK
            // 
            this.cbOK.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cbOK.Location = new System.Drawing.Point(174, 44);
            this.cbOK.Name = "cbOK";
            this.cbOK.Size = new System.Drawing.Size(56, 24);
            this.cbOK.StylePainter = this.StyleGuideBase;
            this.cbOK.TabIndex = 6;
            this.cbOK.Text = "OK";
            this.cbOK.ToolTipText = "OK";
            this.cbOK.Click += new System.EventHandler(this.var1);
            // 
            // txtField
            // 
            this.txtField.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtField.Location = new System.Drawing.Point(6, 12);
            this.txtField.Name = "txtField";
            this.txtField.Size = new System.Drawing.Size(296, 22);
            this.txtField.TabIndex = 5;
            // 
            // InsertFieldDlg
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(314, 83);
            this.Controls.Add(this.cbCancel);
            this.Controls.Add(this.cbOK);
            this.Controls.Add(this.txtField);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InsertFieldDlg";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DataField Name";
            this.Controls.SetChildIndex(this.txtField, 0);
            this.Controls.SetChildIndex(this.cbOK, 0);
            this.Controls.SetChildIndex(this.cbCancel, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

 
		private void var0(object sender, EventArgs e)
		{
			base.Close();
		}

 
		private void var1(object sender, EventArgs e)
		{
			this.mtd4 = true;
			base.Close();
		}


		// Fields
		internal bool mtd4;
		internal Button cbCancel;
		internal Button cbOK;
		private  System.ComponentModel.Container components;
		internal TextBox txtField;
	}

}
