using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Permissions;
using Nistec.Win32;

using Nistec.Data;

namespace Nistec.WinForms
{
	[System.ComponentModel.ToolboxItem(true),Designer(typeof(Design.McDesigner))]
	[ToolboxBitmap (typeof(McPictureBox),"Toolbox.PictureBox.bmp")]
	public class McPictureBox : System.Windows.Forms.PictureBox,IBind,ILayout
	{

        private BorderStyle m_BorderStyle = BorderStyle.FixedSingle;
 
		#region Constructors

		public McPictureBox()
		{
			base.BorderStyle=BorderStyle.None; 
			InitializeComponent();
		}

		#endregion

		#region Dispose

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
//				if(components != null)
//				{
//					components.Dispose();
//				}
			}
			base.Dispose( disposing );
		}

		#endregion

		#region Component Designer generated code
		private void InitializeComponent()
		{
																		 
			this.Name = "McPictureBox";
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
                base.BackColor = LayoutManager.Layout.BackColorInternal;
            }
            else if (force)
            {
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
                    if (value == BorderStyle.Fixed3D)
                        base.BorderStyle = BorderStyle.Fixed3D;
                    else
                        base.BorderStyle = BorderStyle.None;

                    OnStylePropertyChanged(new PropertyChangedEventArgs("ControlLayout"));
                    this.Invalidate();
                }
            }
        }
        #endregion

		#region WndProc

//		[SecurityPermission(SecurityAction.LinkDemand)]
//		protected override void WndProc(ref Message m)
//		{
//
//			base.WndProc(ref m);
//
//			if(BorderStyle!=BorderStyle.FixedSingle)
//			{
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

			Rectangle rect = new Rectangle(0, 0, this.Width-1, this.Height-1);
			
			this.LayoutManager.DrawBorder(g,rect,false,this.Enabled,this.Focused,hot);

		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint (e);

			if(BorderStyle==BorderStyle.FixedSingle)
			{
				PaintFlatControl(e.Graphics,false);
			}

		}


		#endregion

		#region ILayout

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Category("Style"), DefaultValue(ControlLayout.Visual)]
        public virtual ControlLayout ControlLayout
        {
            get { return ControlLayout.Visual; }
            set
            {
            }
        }

		protected IStyle	m_StylePainter;
 
		[Browsable(false)]
		public PainterTypes PainterType
		{
			get{return PainterTypes.Button;}
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

		#region IBind Members

		private bool readOnly=false;

		//private BindingFormat bindFormat=BindingFormat.String;

		public BindingFormat BindFormat
		{
			get{return BindingFormat.String;}
		}

		public string BindPropertyName()
		{
			return  "Text";
		}

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string DefaultValue
		{
			get{return "";}
			set{}//defaultValue=value;}
		}

		public bool ReadOnly
		{
			get{return this.readOnly;} 
			set
			{
				this.readOnly=value;
			}
		}
        public virtual void BindDefaultValue()
        {
            //
        }
		#endregion
	}
}
