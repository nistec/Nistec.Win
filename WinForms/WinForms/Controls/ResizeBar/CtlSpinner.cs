using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Nistec.Drawing;

namespace Nistec.WinForms
{
	/// <summary>
	/// Summary description for McMove.
	/// </summary>
	[ToolboxItem(false)]
	public class McSpinner : Nistec.WinForms.McPictureBox
	{
		#region Ctor
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private static Image _image;

        static McSpinner()
		{
            McSpinner._image = DrawUtils.LoadBitmap(Type.GetType("Nistec.WinForms.McSpinner"),
                        "Nistec.WinForms.Images.spinner1.gif");
		}

   
        public McSpinner()
        {
            base.SetStyle(ControlStyles.FixedHeight, true);
            base.SetStyle(ControlStyles.FixedWidth, true);
  
            InitializeComponent();
            base.BackColor = Color.Transparent;
            this.Image = McSpinner._image;// DrawUtils.LoadBitmap(Type.GetType("Nistec.WinForms.McResize"),
            //"Nistec.WinForms.Images.SizeGrip.gif");
            this.Visible = false;
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(McSpinner));
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // McSpinner
            // 
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Image = ((System.Drawing.Image)(resources.GetObject("$this.Image")));
            this.Size = new System.Drawing.Size(20, 20);
            this.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion


        public void ShowSpinner()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new AsyncDelegate(ShowSpinner));
                return;
            }

            if (this.Image == null)
            {
                this.Image = DrawUtils.LoadBitmap(Type.GetType("Nistec.WinForms.McSpinner"),
                "Nistec.WinForms.Images.spinner1.gif");
            }
            this.Visible = true;
            this.BringToFront();
            this.Invalidate();
        }
        public void ResetSpinner()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new AsyncDelegate(ResetSpinner));
                return;
            }

            this.Visible = false;
            //this.SendToBack();

            //this.Image = McResize._image;
        }


	}
}
