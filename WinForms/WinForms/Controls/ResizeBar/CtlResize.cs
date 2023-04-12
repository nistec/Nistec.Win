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
    [Designer(typeof(Design.McResizeDesigner))]
	public class McResize : Nistec.WinForms.McPictureBox
	{
		#region Ctor
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private static Image _image;
        private bool autoLocation;

        static McResize()
		{
            McResize._image = DrawUtils.LoadBitmap(Type.GetType("Nistec.WinForms.McResize"),
        				"Nistec.WinForms.Images.SizeGrip.gif");

		}

   
        public McResize(Form f)
        {
            form = f;
            autoLocation = true;
            MinSize = new Size(200, 80);
            
            base.SetStyle(ControlStyles.FixedHeight, true);
            base.SetStyle(ControlStyles.FixedWidth, true);
            InitializeComponent();
            base.BackColor = Color.Transparent;
  
            this.Image = McResize._image;// DrawUtils.LoadBitmap(Type.GetType("Nistec.WinForms.McResize"),
                            //"Nistec.WinForms.Images.SizeGrip.gif");

             //this.SendToBack();
             SetSizeGrip();
             form.Resize += new EventHandler(form_Resize);
        }



        public McResize()
        {
            autoLocation = false;
            MinSize = new Size(200, 80);
            base.SetStyle(ControlStyles.FixedHeight, true);
            base.SetStyle(ControlStyles.FixedWidth, true);
  
            InitializeComponent();
            base.BackColor = Color.Transparent;
            this.Image = McResize._image;// DrawUtils.LoadBitmap(Type.GetType("Nistec.WinForms.McResize"),
            //"Nistec.WinForms.Images.SizeGrip.gif");
            //this.SendToBack();
            SetSizeGrip();

        }
        private void SetSizeGrip()
        {
            this.MControlRect = base.ClientRectangle;
            this.mSizeGrip = base.ClientRectangle;
        }

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
                UnWireForm();
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(McResize));
			// 
			// McResize
			// 
            //this.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
			this.Image = ((System.Drawing.Image)(resources.GetObject("$this.Image")));
			this.Size = new System.Drawing.Size(20, 20);
			this.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;

		}
		#endregion

		#region Property

		public override Cursor Cursor
		{
			get
			{
				return base.Cursor;
			}
			set
			{
				base.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
			}
		}

        public Size CurrentSize
        {
            get
            {
                return EndSize;
            }

        }
        public Size MinFormSize
        {
            get
            {
                return MinSize;
            }
            set { MinSize = value; }
        }
		#endregion

        private void WireForm()
        {
            if (form != null && autoLocation)
                form.Resize += new EventHandler(form_Resize);
        }
        private void UnWireForm()
        {
            if (form != null && autoLocation)
                form.Resize -= new EventHandler(form_Resize);
        }
        void form_Resize(object sender, EventArgs e)
        {
            SetLocation();
        }

        public virtual void SetLocation()
        {
            if (form != null && autoLocation)
            {
                Rectangle rect = form.ClientRectangle;
                this.Location = new Point(rect.Width - Width, rect.Height - Height);
                form.Invalidate();
            }
        }
		protected override void OnResize(EventArgs e)
		{
			base.OnResize (e);
			this.Size = new System.Drawing.Size(20, 20);
		}

		#region Move
       
		private Form form;
        //private bool isMouseDown=false;
        //private bool isResizeStart=false;
        private Size MinSize;
        private Size EndSize;
        //private int x;
        //private int y;


        private Point grabPoint = Point.Empty;
        private Size formSize = Size.Empty;
        private bool downOverGrip;
        private Rectangle mSizeGrip = Rectangle.Empty;
        private Rectangle MControlRect = Rectangle.Empty;

        public event EventHandler Resizing;

        protected virtual void OnResizing(EventArgs e)
        {
            if (Resizing != null)
                Resizing(this, e);
        }

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated (e);
            if (form == null && !autoLocation)
                form = this.FindForm();
		}
        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);
            UnWireForm();
        }
        //protected override void OnMouseDown(MouseEventArgs e)
        //{
        //    base.OnMouseDown (e);
        //    isMouseDown = true;
        //    x = e.X;
        //    y = e.Y;
 
        //}

        //protected override void OnMouseUp(MouseEventArgs e)
        //{
        //    base.OnMouseUp (e);
        //    if(isResizeStart)
        //    {
        //        form.Size=EndSize;

        //        //this.Location = new Point(form.Right - Width, form.Bottom - Height);
 
        //    }
        //    isResizeStart=false;
        //    isMouseDown=false;
        //    x=0;
        //    y=0;
        //}

        //protected override void OnMouseMove(MouseEventArgs e)
        //{
        //    base.OnMouseMove (e);
        //    try
        //    {
        //        if(isMouseDown)
        //        {
        //            isResizeStart=true;
        //            Size size=form.Size;
        //            int deltaWidth=e.X-this.x;
        //            int deltaHeight=e.Y-this.y;

        //            int width = size.Width + deltaWidth;
        //            int height = size.Height + deltaHeight;
        //            if (width < MinSize.Width)
        //                width = MinSize.Width;
        //            if (height < MinSize.Height)
        //                height = MinSize.Height;

        //            EndSize = new Size(width, height);

        //            //EndSize=new Size(size.Width+deltaWidth,size.Height+deltaHeight);

        //            OnResizing(EventArgs.Empty);
        //            if (form != null)
        //            {
        //                 form.Size = EndSize;
        //                 //form.Invalidate();
        //            }

        //            //Point p = new Point(e.X - this.x, e.Y - this.y);
        //            //Point pform = new Point(p.X - this.Left, p.Y - this.Top);
        //            //form.Size = new Size(size.Width + deltaWidth, size.Height + deltaHeight);
        //            //form.Location = PointToScreen(pform);
        //        }
        //    }
        //    catch{}
        //}


		#endregion

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (this.mSizeGrip.Contains(e.Location))
            {
                this.downOverGrip = true;
                this.Cursor = Cursors.SizeNWSE;
                this.grabPoint = base.PointToScreen(e.Location);
                this.formSize = base.FindForm().Bounds.Size;
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (this.downOverGrip)
            {
                Size size = new Size(Cursor.Position.X - this.grabPoint.X, Cursor.Position.Y - this.grabPoint.Y);
                Console.WriteLine(size.ToString());
                Size size2 = new Size(this.formSize.Width + size.Width, this.formSize.Height + size.Height);
                if (size2.Width < MinSize.Width || size2.Height < MinSize.Height)
                    return;

                EndSize = size2;
                base.FindForm().Size = size2;
                Win32Methods.SendMessage(base.FindForm().Handle, 0x85, 0, 0);
                Win32Methods.SendMessage(base.FindForm().Handle, 15, 0, 0);
                OnResizing(EventArgs.Empty);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.downOverGrip = false;
            this.Cursor = Cursors.Default;
            base.OnMouseUp(e);
        }

 	}
}
