using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.Timers;
using Nistec.Drawing;

namespace Nistec.WinForms
{
	public delegate void DelegateCloseSplash();


	public class SplashForm : Form
	{
		//private static System.Drawing.Bitmap _image;
	
		public static void ShowSplash(string bitmpPath)
		{
			SplashForm.StartSplash(bitmpPath, Color.FromArgb(128, 128, 128)); 
		
			// simulating operations at startup
			System.Threading.Thread.Sleep(2000);

			// close the splash screen
			SplashForm.CloseSplash();
		}

		#region Constructor

		//static SplashForm()
		//{
		//    // Create a strip of images by loading an embedded bitmap resource
		//    _image = DrawUtils.LoadBitmap(Type.GetType("Nistec.EnterpriseDB.DBConsole"),
		//        "EnterpriseDB.Images.Embedded.Splash.bmp");
         
		//}
   
		public SplashForm(String imageFile, Color col)
		{
			//            Debug.Assert(imageFile != null && imageFile.Length > 0, 
			//                "A valid file path has to be given");
			// ====================================================================================
			// Setup the form
			// ==================================================================================== 
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.ShowInTaskbar = false;
			this.TopMost = true;

			// make form transparent
			this.TransparencyKey = this.BackColor;

			// tie up the events
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SplashForm_KeyUp);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.SplashForm_Paint);
			this.MouseDown += new MouseEventHandler(SplashForm_MouseClick);

			// load and make the bitmap transparent
			m_bmp =new Bitmap(imageFile);

			if(m_bmp == null)
				throw new Exception("Failed to load the bitmap file ");// + imageFile);
			m_bmp.MakeTransparent(col);//Color.FromArgb(128, 128, 128));//

			// resize the form to the size of the iamge
			this.Width = m_bmp.Width;
			this.Height = m_bmp.Height;

			// center the form
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

			// thread handling
			m_delegateClose = new DelegateCloseSplash(InternalCloseSplash);
		}
		#endregion // Constructor

		#region Public methods
		// this can be used for About dialogs
		public static void ShowModal(String imageFile, Color col)
		{
			m_imageFile = imageFile;
			m_transColor = col;//Color.FromArgb(128, 128, 128);//
			MySplashThreadFunc();
		}
		// Call this method with the image file path and the color 
		// in the image to be rendered transparent
		public static void StartSplash(String imageFile, Color col)
		{
			m_imageFile = imageFile;
			m_transColor = col;//Color.FromArgb(128, 128, 128);
			// Create and Start the splash thread
			Thread InstanceCaller = new Thread(new ThreadStart(MySplashThreadFunc));
			InstanceCaller.Start();
		}

		// Call this at the end of your apps initialization to close the splash screen
		public static void CloseSplash()
		{
			if(m_instance != null)
				m_instance.Invoke(m_instance.m_delegateClose);

		}
		#endregion // Public methods

		#region Dispose
		protected override void Dispose( bool disposing )
		{
			m_bmp.Dispose();
			base.Dispose( disposing );
			m_instance = null;
		}
		#endregion // Dispose

		#region Threading code
		// ultimately this is called for closing the splash window
		void InternalCloseSplash()
		{
			this.Close();
			this.Dispose();
		}
		// this is called by the new thread to show the splash screen
		private static void MySplashThreadFunc()
		{
			m_instance = new SplashForm(m_imageFile, m_transColor);
			m_instance.TopMost = false;
			m_instance.ShowDialog();
		}
		#endregion // Multithreading code

		#region Event Handlers

		void SplashForm_MouseClick(object sender, MouseEventArgs e)
		{
			this.InternalCloseSplash();
		}

		private void SplashForm_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			e.Graphics.DrawImage(m_bmp, 0,0);
		}

		private void SplashForm_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Escape)
				this.InternalCloseSplash();
		}
		#endregion // Event Handlers

		#region Private variables
		private static SplashForm m_instance;
		private static String m_imageFile;
		private static Color m_transColor;
		private Bitmap m_bmp;
		private DelegateCloseSplash m_delegateClose;
		#endregion
	}
}
