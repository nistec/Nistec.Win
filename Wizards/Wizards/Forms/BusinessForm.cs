using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace MControl.Wizards.Forms
{

	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class BusinessForm : MControl.WinForms.McForm
	{
		private System.Windows.Forms.ImageList imageBar;
		protected internal MControl.WinForms.McResource ctlResource1;
		protected internal MControl.WinForms.McValidator ctlValidator1;
		private System.ComponentModel.IContainer components;

		#region constructor
        public BusinessForm()
		{
            //MControl.Util.Net.netGrid.NetFramGrid("67c368c91e805727");
            //MControl.Util.Net.netWinCtl.NetFramWinCtl("67c368c91e805727");

			InitializeComponent();
			this.KeyPreview=true;
            //this.caption.FormBox.AllowMaximize = false;

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BusinessForm));
            this.imageBar = new System.Windows.Forms.ImageList(this.components);
            this.ctlResource1 = new MControl.WinForms.McResource(this.components);
            this.ctlValidator1 = new MControl.WinForms.McValidator(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ctlResource1)).BeginInit();
            this.SuspendLayout();
            //// 
            //// caption
            //// 
            //this.caption.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(203)))), ((int)(((byte)(183)))));
            //this.caption.ControlLayout = MControl.WinForms.ControlLayout.Visual;
            //this.caption.Dock = System.Windows.Forms.DockStyle.Top;
            //this.caption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            //this.caption.ForeColor = System.Drawing.SystemColors.ControlText;
            //this.caption.Image = ((System.Drawing.Image)(resources.GetObject("caption.Image")));
            //this.caption.ImageSizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            //this.caption.Location = new System.Drawing.Point(2, 2);
            //this.caption.Name = "caption";
            //this.caption.ShowFormBox = true;
            //this.caption.StylePainter = this.StyleGuideBase;
            //this.caption.SubText = "";
            //this.caption.Text = "frmBase";
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
            this.StyleGuideBase.StylePlan = MControl.WinForms.Styles.Desktop;
            // 
            // imageBar
            // 
            this.imageBar.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageBar.ImageStream")));
            this.imageBar.TransparentColor = System.Drawing.Color.Transparent;
            this.imageBar.Images.SetKeyName(0, "");
            this.imageBar.Images.SetKeyName(1, "");
            this.imageBar.Images.SetKeyName(2, "");
            this.imageBar.Images.SetKeyName(3, "");
            this.imageBar.Images.SetKeyName(4, "");
            this.imageBar.Images.SetKeyName(5, "");
            this.imageBar.Images.SetKeyName(6, "");
            this.imageBar.Images.SetKeyName(7, "");
            this.imageBar.Images.SetKeyName(8, "");
            this.imageBar.Images.SetKeyName(9, "");
            this.imageBar.Images.SetKeyName(10, "");
            this.imageBar.Images.SetKeyName(11, "");
            this.imageBar.Images.SetKeyName(12, "");
            this.imageBar.Images.SetKeyName(13, "");
            this.imageBar.Images.SetKeyName(14, "");
            this.imageBar.Images.SetKeyName(15, "");
            this.imageBar.Images.SetKeyName(16, "");
            this.imageBar.Images.SetKeyName(17, "");
            this.imageBar.Images.SetKeyName(18, "");
            this.imageBar.Images.SetKeyName(19, "");
            this.imageBar.Images.SetKeyName(20, "");
            this.imageBar.Images.SetKeyName(21, "");
            this.imageBar.Images.SetKeyName(22, "");
            this.imageBar.Images.SetKeyName(23, "");
            this.imageBar.Images.SetKeyName(24, "");
            this.imageBar.Images.SetKeyName(25, "");
            // 
            // ctlResource1
            // 
            this.ctlResource1.CultureSupport = new string[] {
        "en"};
            this.ctlResource1.ResourceBaseName = "";
            this.ctlResource1.ResourceManager = null;
            // 
            // ctlValidator1
            // 
            this.ctlValidator1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.BlinkIfDifferentError;
            this.ctlValidator1.Icon = ((System.Drawing.Icon)(resources.GetObject("ctlValidator1.Icon")));
            // 
            // BusinessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(296, 221);
            this.CloseOnEscape = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "BusinessForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmBase";
            ((System.ComponentModel.ISupportInitialize)(this.ctlResource1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		#region Keys

	
		protected override bool ProcessDialogKey(Keys keyData)
		{
			//if(Form.ActiveForm!=this)
			//	return false;
	
			bool res=false;
			switch(keyData)
			{
				case Keys.F1://help
					res= GoHelp();
					break;
				case Keys.F2://Preview
					res=  GoPreview();
					break;
				case Keys.F3:
					res=  Gof3();
					break;
				case Keys.F4:
					res=  Gof4();
					break;
				case Keys.F5:
					res=  Gof5();
					break;
				case Keys.F6:
					res=  Gof6();
					break;
				case Keys.F7://serch
					res=  GoFind();
					break;
				case Keys.F8://next
					res=  GoNext();
					break;
				case Keys.F9://Options
					res=  GoOption();
					break;
				case Keys.F10://save
					res=  GoSave();
					break;
				case Keys.F11://new
					res=  GoNew();
					break;
				case Keys.F12://
					res=  Gof12();
					break;
				case Keys.Escape://
					res=  GoEsc();
					break;
				case Keys.Insert://
					res=  GoIns();
					break;
				case Keys.Enter://
					res=  GoEnter();
					break;
				case Keys.Tab://
					res=  GoTab();
					break;
				default:
					return base.ProcessDialogKey (keyData);
			}
			if(!res)
				return base.ProcessDialogKey (keyData);
			else
				return res;
		
		}

		/// <summary>
		/// F1
		/// </summary>
		/// <returns></returns>
		public virtual bool GoHelp()
		{
          return false;
		}
		/// <summary>
		/// F2
		/// </summary>
		/// <returns></returns>
		public virtual bool GoPreview()
		{
			return false;
		}
		/// <summary>
		/// F3
		/// </summary>
		/// <returns></returns>
		public virtual bool Gof3()
		{
			return false;
		}
		/// <summary>
		/// F4
		/// </summary>
		/// <returns></returns>
		public virtual bool Gof4()
		{
			return false;
		}
		/// <summary>
		/// F5
		/// </summary>
		/// <returns></returns>
		public virtual bool Gof5()
		{
			return false;
		}
		/// <summary>
		/// F6
		/// </summary>
		/// <returns></returns>
		public virtual bool Gof6()
		{
			return false;
		}
		/// <summary>
		/// F7
		/// </summary>
		/// <returns></returns>
		public virtual bool GoFind()
		{
			return false;
		}
		/// <summary>
		/// F8
		/// </summary>
		/// <returns></returns>
		public virtual bool GoNext()
		{
			return false;
		}
		/// <summary>
		/// F9
		/// </summary>
		/// <returns></returns>
		public virtual bool GoOption()
		{
			return false;
		}
		/// <summary>
		/// F10
		/// </summary>
		/// <returns></returns>
		public virtual bool GoSave()
		{
			return false;
		}
		/// <summary>
		/// F11
		/// </summary>
		/// <returns></returns>
		public virtual bool GoNew()
		{
			return false;
		}
		/// <summary>
		/// F12
		/// </summary>
		/// <returns></returns>
		public virtual bool Gof12()
		{
			return false;
		}
		/// <summary>
		/// Escape
		/// </summary>
		/// <returns></returns>
		public virtual bool GoEsc()
		{
			DoExit();
			return true;
		}
		/// <summary>
		/// Insert
		/// </summary>
		/// <returns></returns>
		public virtual bool GoIns()
		{
			return false;
		}
		/// <summary>
		/// Enter
		/// </summary>
		/// <returns></returns>
		public virtual bool GoEnter()
		{
			return false;
		}
		/// <summary>
		/// Tab
		/// </summary>
		/// <returns></returns>
		public virtual bool GoTab()
		{
			return false;
		}
		#endregion

		#region Overrides
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad (e);
			//-UIForms.Forms.Add(this);
			
		}

		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed (e);
			//-UIForms.Forms.Remove(this);
		}

		#endregion

		#region IBusinessUI Members

		public virtual void DoExit()
		{
			this.Close();
		}

		public virtual void DoUpdate()
		{
			// TODO:  Add frmBase.Update implementation
		}

		public virtual void DoAddNew()
		{
			// TODO:  Add frmBase.AddNew implementation
		}

		public virtual void DoDelete()
		{
			// TODO:  Add frmBase.Delete implementation
		}

		public virtual void DoHelp()
		{
			// TODO:  Add frmBase.Help implementation
		}

		public virtual void DoPreview()
		{
			// TODO:  Add frmBase.Preview implementation
		}

		public virtual void DoReject()
		{
			// TODO:  Add frmBase.Reject implementation
		}
		#endregion
	}
}
