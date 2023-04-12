using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;
using System.Windows.Forms;
using Nistec.Win32;



namespace Nistec.WinForms.Controls
{
	public class McPopupForm : Form
	{
		// Events
		[Category("Behavior")]
		public event EventHandler ClosePopup;
		[Category("Behavior")]
		public event EventHandler ShowPopup;

		// Fields
		//private EventHandler ClosePopup;
		private Container components;
		public bool LockClose;
		protected Control ParentControl;
		//private EventHandler ShowPopup;

		public McPopupForm() : this(null)
		{
		}

		public McPopupForm(Control parentControl)
		{
			this.components = null;
			this.InitializeComponent();
			base.SetStyle(ControlStyles.ResizeRedraw, true);
			base.SetStyle(ControlStyles.DoubleBuffer, true);
			base.SetStyle(ControlStyles.Selectable, false);
			base.TabStop = false;
			this.ParentControl = parentControl;
			base.ControlBox = false;
		}

		public virtual void ClosePopupForm()
		{
			if (!this.LockClose)
			{
				if (this.ParentControl != null)
				{
					Form form1 = this.ParentControl.FindForm();
					if (form1 != null)
					{
						form1.Activate();
					}
				}
				base.Close();
				this.InvokeClosePopup(EventArgs.Empty);
			}
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
			this.AutoScaleBaseSize = new Size(5, 13);
			base.FormBorderStyle = FormBorderStyle.None;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "McPopupForm";
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.Manual;
			this.Text = "McPopupForm";
			base.TopMost = false;
			base.Deactivate += new EventHandler(this.McPopupForm_Deactivate);
		}

		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed (e);
			this.OnClosePopup(e);
		}

 
		public void InvokeClosePopup(EventArgs e)
		{
			this.OnClosePopup(e);
		}

 
		public void InvokeShowPopup(EventArgs e)
		{
			this.OnShowPopup(e);
		}

 
		protected virtual void OnClosePopup(EventArgs e)
		{
			if (this.ClosePopup != null)
			{
				this.ClosePopup(this, e);
			}
		}

 
		protected virtual void OnShowPopup(EventArgs e)
		{
			if (this.ShowPopup != null)
			{
				this.ShowPopup(this, e);
			}
		}

		public virtual void ShowPopupForm()
		{
			base.Show();
			this.InvokeShowPopup(EventArgs.Empty);
		}

 
		private void McPopupForm_Deactivate(object sender, EventArgs e)
		{
			this.ClosePopupForm();
		}

	}

}
