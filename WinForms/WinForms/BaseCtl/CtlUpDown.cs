using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Nistec.Drawing;
using System.Security.Permissions;
using Nistec.Win32;

namespace Nistec.WinForms.Controls
{
	[System.ComponentModel.ToolboxItem(false)]
	public  class McUpDown : System.Windows.Forms.UserControl//,ILayout
	{

		#region Members
		private System.ComponentModel.Container components = null;

        private McUpDown.McButtonUpDown m_Up;
        private McUpDown.McButtonUpDown m_Down;
        private ControlLayout m_ControlLayout;

		internal bool					IsMouseDown;
		internal bool					KeySumDown;
		internal bool					KeyMinusDown;
		protected ILayout				owner;

 

		public event MouseEventHandler DownPress;
		public event MouseEventHandler DownRelease;
		public event MouseEventHandler UpPress;
		public event MouseEventHandler UpRelease;

		#endregion

		#region Constructors

        internal McUpDown(ILayout ctl)
		{		
			//m_ButtonWidth=16;
			owner=ctl;

			IsMouseDown = false;
			KeySumDown = false;
			KeyMinusDown = false;
            m_ControlLayout = ControlLayout.Visual;
 
			SetStyle(ControlStyles.ResizeRedraw,true);
			SetStyle(ControlStyles.DoubleBuffer,true);
			SetStyle(ControlStyles.UserPaint,true);
			SetStyle(ControlStyles.AllPaintingInWmPaint,true);

			InitializeComponent();
			m_Up.Image = ResourceUtil.LoadImage (Global.ImagesPath + "up.ico");
			m_Down.Image = ResourceUtil.LoadImage (Global.ImagesPath + "down.ico");

		}

		#endregion

		#region Dispose

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
		private void InitializeComponent()
		{
            this.m_Up = new McButtonUpDown(this);
            this.m_Down = new McButtonUpDown(this);
			// 
			// m_Up
			// 
			//this.m_Up.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.m_Up.Location = new System.Drawing.Point(0, 0);
			this.m_Up.Name = "m_Up";
			this.m_Up.Size = new System.Drawing.Size(16, 8);
			this.m_Up.TabIndex = 0;
			this.m_Up.MouseDown+=new MouseEventHandler(m_Up_MouseDown);
            this.m_Up.MouseUp +=new MouseEventHandler(m_Up_MouseUp); 
			// 
			// m_Down
			// 
			//this.m_Down.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.m_Down.Location = new System.Drawing.Point(0, 8);
			this.m_Down.Name = "m_Down";
			this.m_Down.Size = new System.Drawing.Size(16, 8);
			this.m_Down.TabIndex = 0;
			this.m_Down.MouseDown +=new MouseEventHandler(m_Down_MouseDown);
			this.m_Down.MouseUp +=new MouseEventHandler(m_Down_MouseUp);
			// 
			// McUpDown
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(m_Up);
			this.Controls.Add(m_Down);
			this.Name = "McUpDown";
			this.Size = new System.Drawing.Size(16, 16);

		}
		#endregion

		#region Overrides

		protected override void OnSizeChanged(System.EventArgs e)
		{
			base.OnSizeChanged (e);
			int height=this.Height ;
			this.Width =16;
			this.m_Up.Height =height/2;
			this.m_Down.Height =height/2;
            this.m_Down.Top =m_Up.Height;  
 		}

		#endregion

		#region ILayout


//		public virtual void SetStyleLayout(StyleLayout value)
//		{
//			this.m_Style.SetStyleLayout(value); 
//			m_Up.StyleMc.SetStyleLayout (value);
//			m_Down.StyleMc.SetStyleLayout (value);
//			this.Invalidate();
//		}
//
//		public virtual void SetStyleLayout(Styles value)
//		{
//			m_Style.SetStyleLayout(value);//this.m_Style.StylePlan=value; 
//			m_Up.StyleMc.SetStyleLayout(value);//.StylePlan= value;
//			m_Down.StyleMc.SetStyleLayout(value);//.StylePlan= value;
//			this.Invalidate();
//		}
//
//		protected virtual void OnStyleGuideChanged(EventArgs e)
//		{
//			this.m_Down.StyleGuide=this.m_StyleGuide;
//			this.m_Up.StyleGuide=this.m_StyleGuide;
//			this.Invalidate();
//		}
//
//		private void m_Style_PropertyChanged(object sender, PropertyChangedEventArgs e)
//		{
//			if(!(DesignMode || IsHandleCreated))
//				return;
//			if(e.PropertyName.Equals("StyleLayout"))
//			{
//				m_Up.StyleMc.SetStyleLayout (m_Style.Layout);
//				m_Down.StyleMc.SetStyleLayout (m_Style.Layout);
//			}
//			else if(e.PropertyName.Equals("StylePlan"))
//			{
//				m_Up.StyleMc.StylePlan= m_Style.StylePlan;
//				m_Down.StyleMc.StylePlan= m_Style.StylePlan;
//			}
//			this.Invalidate();
//		}

		#endregion

		#region Properties

        //[DefaultValue(ControlLayout.Visual)]    
        [Category("Style")]
        public virtual ControlLayout ControlLayout
        {
            get { return m_ControlLayout; }
            set
            {
                if (m_ControlLayout != value)
                {
                    m_ControlLayout = value;
                    this.m_Down.ControlLayout = value;
                    this.m_Up.ControlLayout = value;
                    this.Invalidate();
                }
            }

        }

		[Browsable(false)]
		public bool IsMouseHover
		{
			get
			{
				try
				{
					Point mPos  = Control.MousePosition;
					bool retVal = this.ClientRectangle.Contains(this.PointToClient(mPos));
					return retVal;
				}
				catch{return false;}
			}
		}


		#endregion

		#region Events
		
		public void PreformDownPress()
		{
			m_Down_MouseDown(this,new System.Windows.Forms.MouseEventArgs (MouseButtons.Left ,1,1,1,1));
		}
		public void PreformDownRelease()
		{
			m_Down_MouseUp(this,new System.Windows.Forms.MouseEventArgs (MouseButtons.Left,1,1,1,1));
		}
		public void PreformUpPress()
		{
			m_Up_MouseDown(this,new System.Windows.Forms.MouseEventArgs (MouseButtons.Left,1,1,1,1));
		}
		public void PreformUpRelease()
		{
			m_Up_MouseUp(this,new System.Windows.Forms.MouseEventArgs (MouseButtons.Left,1,1,1,1));
		}

		private void m_Down_MouseDown(object sender, MouseEventArgs e)
		{
			IsMouseDown = true;
			KeySumDown = false;
			KeyMinusDown = true;

           if(DownPress !=null)
               DownPress(this,e);
		}

		private void m_Up_MouseDown(object sender, MouseEventArgs e)
		{
			IsMouseDown = true;
			KeySumDown = true;
			KeyMinusDown = false;
			if(UpPress !=null)
				UpPress(this,e);
		}

		private void m_Down_MouseUp(object sender, MouseEventArgs e)
		{
			IsMouseDown = false;
			KeySumDown = false;
			KeyMinusDown = false;
			if(DownRelease !=null)
				DownRelease(this,e);
		}

		private void m_Up_MouseUp(object sender, MouseEventArgs e)
		{
			IsMouseDown = false;
			KeySumDown = false;
			KeyMinusDown = false;
			if(UpRelease !=null)
				UpRelease(this,e);
		}

		#endregion

		#region McButton

		[DefaultEvent("ButtonClick")]
		private class McButtonUpDown : McButtonBase
		{		
	
			protected McUpDown owner;
	
			#region Constructors

            public McButtonUpDown(McUpDown ctl)
                : base()
			{
				this.owner=ctl;
				base.SetStyle(ControlStyles.Selectable, false);
                base.ControlLayout = ControlLayout.Visual;
  
				InitializeComponent( );
			}

	
			#endregion

			#region Dispose

			protected override void Dispose( bool disposing )
			{
				base.Dispose( disposing );
			}

			#endregion

			#region Component Designer generated code

			private void InitializeComponent()
			{
				// 
				// McButton
				// 
				this.Name = "McButton";
				this.Size = new System.Drawing.Size(70, 20);
				this.ResizeRedraw = true;
				//this.m_Style =new StyleButtonDesigner (this,false,false,true); 
			}
		
			#endregion

			#region Overrides

			protected override void OnPaint(PaintEventArgs e)
			{
				base.OnPaint (e);

				Rectangle bounds=new Rectangle(0,0, this.Width-1, this.Height-1);
				owner.owner.LayoutManager.DrawButtonRect(e.Graphics,bounds,this,base.ControlLayout) ;

				if(m_Image !=null) 
				{
					PointF iPoint=new Point (0,0);
					iPoint.Y=((bounds.Height -m_Image.Height)/2);
					iPoint.X =((bounds.Width -m_Image.Width)/2);

					//if(this.Enabled)
					//{
						e.Graphics.DrawImage (m_Image, (int)iPoint.X+1, (int)iPoint.Y);
					//}
					//else
					//{ //error
						//ControlPaint.DrawImageDisabled(e.Graphics , m_Image, (int)iPoint.X+1, (int)iPoint.Y,Parent.BackColor);
					//}
				}
			}


			#endregion

		}

		#endregion
	}

}
