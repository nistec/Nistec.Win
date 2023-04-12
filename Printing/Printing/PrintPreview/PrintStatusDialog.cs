using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using System.Threading;


namespace Nistec.Printing
{

	[System.ComponentModel.ToolboxItem(false)]
	public class PrintStatusDialog : PrintController
	{

		// Fields
		private BackgroundThread backgroundThread;
		private string dialogTitle;
		private PrintDocument document;
		private int pageNumber;
		private PrintController underlyingController;

		public PrintStatusDialog(PrintController underlyingController) : this(underlyingController, "PrintController")//SR.GetString("PrintControllerWithStatusDialog_DialogTitlePrint"))
		{
		}

		public PrintStatusDialog(PrintController underlyingController, string dialogTitle)
		{
			this.underlyingController = underlyingController;
			this.dialogTitle = dialogTitle;
		}

		public override void OnEndPage(PrintDocument document, PrintPageEventArgs e)
		{
			this.underlyingController.OnEndPage(document, e);
			if ((this.backgroundThread != null) && this.backgroundThread.canceled)
			{
				e.Cancel = true;
			}
			this.pageNumber++;
			base.OnEndPage(document, e);
		}

 
		public override void OnEndPrint(PrintDocument document, PrintEventArgs e)
		{
			this.underlyingController.OnEndPrint(document, e);
			if ((this.backgroundThread != null) && this.backgroundThread.canceled)
			{
				e.Cancel = true;
			}
			if (this.backgroundThread != null)
			{
				this.backgroundThread.Stop();
			}
			base.OnEndPrint(document, e);
		}

 
		public override Graphics OnStartPage(PrintDocument document, PrintPageEventArgs e)
		{
			base.OnStartPage(document, e);
			if (this.backgroundThread != null)
			{
				this.backgroundThread.UpdateLabel();
			}
			Graphics graphics1 = this.underlyingController.OnStartPage(document, e);
			if ((this.backgroundThread != null) && this.backgroundThread.canceled)
			{
				e.Cancel = true;
			}
			return graphics1;
		}

		public override void OnStartPrint(PrintDocument document, PrintEventArgs e)
		{
			base.OnStartPrint(document, e);
			this.document = document;
			this.pageNumber = 1;
			if (SystemInformation.UserInteractive)
			{
				this.backgroundThread = new PrintStatusDialog.BackgroundThread(this);
			}
			try
			{
				this.underlyingController.OnStartPrint(document, e);
			}
			catch (Exception exception1)
			{
				if (this.backgroundThread != null)
				{
					this.backgroundThread.Stop();
				}
				throw exception1;
			}
			finally
			{
				if ((this.backgroundThread != null) && this.backgroundThread.canceled)
				{
					e.Cancel = true;
				}
			}
		}

 
		#region class BackgroundThread
		
		// Nested Types
		private class BackgroundThread
		{

			// Fields
			private bool alreadyStopped;
			internal bool canceled;
			private PrintStatusDialog.StatusDialog dialog;
			private PrintStatusDialog parent;
			private Thread thread;
			private Form frm;


			internal BackgroundThread(PrintStatusDialog parent)
			{
				frm=Form.ActiveForm;
				this.canceled = false;
				this.alreadyStopped = false;
				this.parent = parent;
				this.thread = new Thread(new ThreadStart(this.Run));
                this.thread.SetApartmentState(ApartmentState.STA);//.ApartmentState = ApartmentState.STA;
				this.thread.Start();
			}

			[PermissionSet(SecurityAction.Assert, XML="<PermissionSet class=\"System.Security.PermissionSet\"\r\n               version=\"1\">\r\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\r\n                version=\"1\"\r\n                Flags=\"UnmanagedCode\"/>\r\n   <IPermission class=\"System.Security.Permissions.UIPermission, mscorlib, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\r\n                version=\"1\"\r\n                Window=\"AllWindows\"/>\r\n</PermissionSet>\r\n")]
			private void Run()
			{
				PrintStatusDialog.BackgroundThread thread1;
				try
				{
					lock ((thread1 = this))
					{
						if (this.alreadyStopped)
						{
							return;
						}
						this.dialog = new PrintStatusDialog.StatusDialog(this, this.parent.dialogTitle);
						this.ThreadUnsafeUpdateLabel();
						this.dialog.Visible = true;
					}
					if (!this.alreadyStopped)
					{
						Application.Run(this.dialog);
					}
				}
				finally
				{
					lock ((thread1 = this))
					{
                        //if(frm!=null)
                        //    frm.Activate();
						if (this.dialog != null)
						{
							this.dialog.Dispose();
							this.dialog = null;
						}
					}
				}
			}

			internal void Stop()
			{
				lock (this)
				{
					if ((this.dialog != null) && this.dialog.IsHandleCreated)
					{
						this.dialog.BeginInvoke(new MethodInvoker(this.dialog.Close));
					}
					else
					{
						this.alreadyStopped = true;
					}
				}
			}

			private void ThreadUnsafeUpdateLabel()
			{
				//this.dialog.label1.Text ="NowPrinting";// SR.GetString("PrintControllerWithStatusDialog_NowPrinting", new object[] { this.parent.pageNumber, this.parent.document.DocumentName });
				this.dialog.label1.Text ="Printing " + this.parent.pageNumber.ToString() + "  " + this.parent.document.DocumentName ;
			}

			internal void UpdateLabel()
			{
				if ((this.dialog != null) && this.dialog.IsHandleCreated)
				{
					this.dialog.BeginInvoke(new MethodInvoker(this.ThreadUnsafeUpdateLabel));
				}
			}


		}

		#endregion

		#region Class StatusDialog

		private class StatusDialog : Form
		{
	
			// Fields
			private PrintStatusDialog.BackgroundThread backgroundThread;
			private Button button1;
			internal Label label1;

			internal StatusDialog(PrintStatusDialog.BackgroundThread backgroundThread, string dialogTitle)
			{
				this.InitializeComponent();
				this.backgroundThread = backgroundThread;
				this.Text = dialogTitle;
				base.MinimumSize = base.Size;
			}

			private void button1_Click(object sender, EventArgs e)
			{
				this.button1.Enabled = false;
				this.label1.Text ="Canceling";// SR.GetString("PrintControllerWithStatusDialog_Canceling");
				this.backgroundThread.canceled = true;
			}

 
			private void InitializeComponent()
			{
				this.label1 = new Label();
				this.button1 = new Button();
				this.label1.Location = new Point(8, 0x10);
				this.label1.TextAlign = ContentAlignment.MiddleCenter;
				this.label1.Size = new Size(240, 0x40);
				this.label1.TabIndex = 1;
				this.label1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
				this.button1.Size = new Size(0x4b, 0x17);
				this.button1.TabIndex = 0;
				this.button1.Text ="Cancel";// SR.GetString("PrintControllerWithStatusDialog_Cancel");
				this.button1.Location = new Point(0x58, 0x58);
				this.button1.Anchor = AnchorStyles.Bottom;
				this.button1.Click += new EventHandler(this.button1_Click);
				this.AutoScaleBaseSize = new Size(5, 13);
				base.MaximizeBox = false;
				base.ControlBox = false;
				base.MinimizeBox = false;
				base.ClientSize = new Size(0x100, 0x7a);
				base.CancelButton = this.button1;
				base.SizeGripStyle = SizeGripStyle.Hide;
				base.StartPosition = FormStartPosition.CenterScreen;
				base.ShowInTaskbar = false;
				base.Controls.Add(this.label1);
				base.Controls.Add(this.button1);
			}
			#endregion
 

		}
	}
 

}
