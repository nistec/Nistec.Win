using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Security.Permissions;  

using Nistec.Win32;

namespace Nistec.WinForms
{
	[ToolboxItem(true),Designer(typeof(Design.McDesigner))]
	[ToolboxBitmap (typeof(McLinkLabel), "Toolbox.LinkLabel.bmp")]
    public class McLinkLabel : System.Windows.Forms.LinkLabel, ILayout, IButton
	{
	
		#region Members
        private ControlLayout m_ControlLayout;
		private BorderStyle m_BorderStyle=BorderStyle.None;
		protected ToolTip toolTip;
		[Category("Action")]
		public event LinkClickEventHandler LinkClick = null;
		#endregion

		#region Constructors
		
		public McLinkLabel()
		{
			this.toolTip = new ToolTip();
			base.BorderStyle=BorderStyle.None;
			//this.BackColor =Color.Transparent; 
			//Nistec.Util.Net.netWinMc.NetFram(this.Name); 
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated (e);
			//Nistec.Util.Net.netWinMc.NetFram(this.Name); 
		}

		#endregion

		#region Properties
		public String ToolTip
		{
			get { return toolTip.GetToolTip(this); }
			set
			{
				toolTip.RemoveAll();
				toolTip.SetToolTip(this, value);
			}
		}

		#endregion

		#region Methods

		public void PreformLinkClicked(object sender,LinkClickEvent e)
		{
			if(this.Enabled && this.LinkClick  != null )
			{
				//LinkClickEvent e=new LinkClickEvent (target);
				this.LinkClick (sender,e);
			}
		}

		public void LinkTarget(object sender,string target)
		{
			if(this.Enabled && target != null )
			{
				try
				{
					if(null != target )//&& target.StartsWith("www"))
					{
						System.Diagnostics.Process.Start(target);
					}
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message.ToString (),"Nistec" );
				}
			}
		}


		#endregion

		#region WndProc

//		[SecurityPermission(SecurityAction.LinkDemand)]
//		protected override void WndProc(ref Message m)
//		{
//
//			if(BorderStyle!=BorderStyle.FixedSingle)
//			{
//				base.WndProc(ref m);
//				return;
//			}
//
//			IntPtr hDC = IntPtr.Zero;
//			Graphics gdc = null;
//		
//			switch (m.Msg)
//			{
//				case WinMsgs.WM_MOUSEHOVER:	
//				case WinMsgs.WM_MOUSEMOVE:	
//					hDC = WinAPI.GetWindowDC(this.Handle);
//					gdc = Graphics.FromHdc(hDC);
//					//gdc.DrawRectangle (new Pen (BackColor,2),new Rectangle(0, 0, this.Width, this.Height));
//					PaintFlatControl(gdc,true);
//					WinAPI.ReleaseDC(m.HWnd, hDC);
//					gdc.Dispose();	
//					break;
//				case WinMsgs.WM_SETFOCUS :	
//				case WinMsgs.WM_KILLFOCUS :	
//				case WinMsgs.WM_MOUSELEAVE:	
//				case WinMsgs.WM_PAINT:	
//					hDC = WinAPI.GetWindowDC(this.Handle);
//					gdc = Graphics.FromHdc(hDC);
//					//gdc.DrawRectangle (new Pen (BackColor,2),new Rectangle(0, 0, this.Width, this.Height));
//					PaintFlatControl(gdc,false);
//					WinAPI.ReleaseDC(m.HWnd, hDC);
//					gdc.Dispose();	
//					break;
//			}
//		}
//
		private void PaintFlatControl(Graphics g,bool hot)
		{
			//Rectangle rect =this.ClientRectangle;//  new Rectangle(0, 0, this.Width, this.Height);
			
			Rectangle rect = new Rectangle(0, 0, this.Width-1, this.Height-1);
			
			this.LayoutManager.DrawBorder(g,rect,false,this.Enabled,this.Focused,hot);

			
//			if (! this.Enabled  )
//				ControlPaint.DrawBorder(g, rect, SystemColors.Control , ButtonBorderStyle.Solid  );
//			else if (this.ContainsFocus)// Focused )
//				ControlPaint.DrawBorder(g, rect, m_Style.FocusedColor , ButtonBorderStyle.Solid  );
//			else if(hot)
//				ControlPaint.DrawBorder(g, rect, m_Style.BorderHotColor , ButtonBorderStyle.Solid  );
//			else 
//				ControlPaint.DrawBorder(g, rect, m_Style.BorderColor , ButtonBorderStyle.Solid  );
		
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint (e);

			if(this.BorderStyle==BorderStyle.FixedSingle)
			{
				Rectangle rect = new Rectangle(0, 0, this.Width-1, this.Height-1);
				bool hot=false;
				this.LayoutManager.DrawBorder(e.Graphics,rect,false,this.Enabled,this.Focused,hot);
			}
		}


        public new BorderStyle BorderStyle
        {
            get
            {
                return m_BorderStyle;
            }
            set
            {
                if (m_BorderStyle != value)
                {
                    m_BorderStyle = value;
                    if (value == BorderStyle.None)
                    {
                        base.BorderStyle = BorderStyle.None;
                        this.BackColor = Color.Empty;
                    }
                    else if (value == BorderStyle.Fixed3D)
                        base.BorderStyle = BorderStyle.Fixed3D;
                    else
                        base.BorderStyle = BorderStyle.None;
 
                    OnStylePropertyChanged(new PropertyChangedEventArgs("ControlLayout"));
                    this.Invalidate();
                }
            }
        }


		#endregion

        #region StyleProperty

        [Category("Style"), DefaultValue(typeof(Color), "WindowText")]
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set
            {
                SerializeForeColor(value, true);
            }
        }

        [Category("Style"), DefaultValue(typeof(Color), "Window")]
        public override Color BackColor
        {
            get { return base.BackColor; }
            set
            {
                SerializeBackColor(value, true);
            }
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected void SerializeFont(Font value, bool force)
        {
            if (ShouldSerializeForeColor())
                this.Font = LayoutManager.Layout.TextFontInternal;
            else if (force)
                this.Font = value;
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected void SerializeForeColor(Color value, bool force)
        {
            if (ShouldSerializeForeColor())
            {
                base.ForeColor = LayoutManager.Layout.ForeColorInternal;
            }
            else if (force)
            {
                base.ForeColor = value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected void SerializeBackColor(Color value, bool force)
        {
            if (ShouldSerializeBackColor())
            {
                if (BorderStyle == BorderStyle.None)
                    base.BackColor = Color.Empty;
                else
                base.BackColor = LayoutManager.Layout.BackColorInternal;
            }
            else if (force)
            {
                if (BorderStyle == BorderStyle.None)
                    base.BackColor = Color.Empty;
                else
                    base.BackColor = value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected bool ShouldSerializeBackColor()
        {
            return IsHandleCreated && StylePainter != null;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected bool ShouldSerializeForeColor()
        {
            return IsHandleCreated && StylePainter != null;
        }

        protected virtual void OnStylePropertyChanged(PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("ControlLayout") || e.PropertyName.Equals("BackColor"))
                SerializeBackColor(Color.Empty, false);
            if (e.PropertyName.Equals("ControlLayout") || e.PropertyName.Equals("ForeColor"))
                SerializeForeColor(Color.Empty, false);
            if (e.PropertyName.Equals("ControlLayout") || e.PropertyName.Equals("TextFont"))
                SerializeFont(Form.DefaultFont, false);
            if ((DesignMode || IsHandleCreated))
                this.Invalidate(true);
        }
        protected virtual void OnStylePainterChanged(EventArgs e)
        {
            OnStylePropertyChanged(new PropertyChangedEventArgs("ControlLayout"));
        }

        #endregion

        #region StyleProperty

        [Category("Style")]
        public virtual ControlLayout ControlLayout
        {
            get { return m_ControlLayout; }
            set
            {
                m_ControlLayout = value;
                OnStylePropertyChanged(new PropertyChangedEventArgs("ControlLayout"));
                this.Invalidate();
            }

        }

         #endregion


		#region ILayout

		protected IStyle		m_StylePainter;
  
		[Browsable(false)]
		public PainterTypes PainterType
		{
			get{return PainterTypes.Edit;}
		}

		[Category("Style"),DefaultValue(null),RefreshProperties(RefreshProperties.All)]
		public IStyle StylePainter
		{
			get {return m_StylePainter;}
			set 
			{
				if(m_StylePainter!=value)
				{
					if (this.m_StylePainter != null)
						this.m_StylePainter.PropertyChanged -=new PropertyChangedEventHandler(m_Style_PropertyChanged); 
					m_StylePainter = value;
					if (this.m_StylePainter != null)
						this.m_StylePainter.PropertyChanged +=new PropertyChangedEventHandler(m_Style_PropertyChanged); 
					OnStylePainterChanged(EventArgs.Empty);
					this.Invalidate(true);
				}
			}
		}

		[Browsable(false)]
		public virtual IStyleLayout LayoutManager
		{
			get
			{
				if(this.m_StylePainter!=null)
					return this.m_StylePainter.Layout as IStyleLayout;
				else
					return StyleLayout.DefaultLayout as IStyleLayout;// this.m_Style as IStyleLayout;

			}
		}

		public virtual void SetStyleLayout(StyleLayout value)
		{
			if(this.m_StylePainter!=null)
				this.m_StylePainter.Layout.SetStyleLayout(value); 
		}

		public virtual void SetStyleLayout(Styles value)
		{
			if(this.m_StylePainter!=null)
				m_StylePainter.Layout.SetStyleLayout(value);
		}


		private void m_Style_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{

			OnStylePropertyChanged(e);
		}

		#endregion

        [Browsable(false)]
        public bool IsMouseHover
        {
            get
            {
                try
                {
                    Point mPos = Control.MousePosition;
                    bool retVal = this.ClientRectangle.Contains(this.PointToClient(mPos));
                    return retVal;
                }
                catch { return false; }
            }
        }
        [Browsable(false)]
        public ButtonStates ButtonState
        {
            get { return ButtonStates.Normal; }
        }

	}

}
