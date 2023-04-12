using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.ComponentModel;

using Nistec.Win32;
  
namespace Nistec.WinForms
{
	[Designer(typeof(Design.SplitterDesigner))]
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(McSplitter),"Toolbox.Splitter.bmp")]
	public class McSplitter: System.Windows.Forms.Splitter ,ILayout 
	{

  
		#region Constructor

		public McSplitter(): base()
		{
			base.BorderStyle=BorderStyle.None ;
			this.SetStyle(ControlStyles.DoubleBuffer, true);
			//Nistec.Util.Net.netWinMc.NetFram(this.Name); 
		}

		#endregion

		#region WndProc
		protected override void WndProc(ref Message m)
		{
		
			IntPtr hDC =IntPtr.Zero ;
			switch (m.Msg)
			{

				case WinMsgs.WM_MOUSEMOVE :	
					base.WndProc(ref m);
					hDC = WinAPI.GetWindowDC(this.Handle);
					PaintFlatControl(hDC,true);
					WinAPI.ReleaseDC(m.HWnd, hDC);
					break;
				case WinMsgs.WM_MOUSELEAVE :	
				case WinMsgs.WM_PAINT:	
					base.WndProc(ref m);
					hDC = WinAPI.GetWindowDC(this.Handle);
					PaintFlatControl(hDC,false);
					WinAPI.ReleaseDC(m.HWnd, hDC);
					break;
				default:
					base.WndProc(ref m);
					break;
			}
		}

		private void PaintFlatControl(IntPtr hDC,bool hot)
		{
			Rectangle rect = this.ClientRectangle ;

			using(Graphics g = Graphics.FromHdc(hDC))
			{
                if (!ShouldSerializeBackColor())
                {
                    if (this.BorderStyle == BorderStyle.FixedSingle)
                    {
                        Rectangle rectB = new Rectangle(rect.X,rect.Y,rect.Width+1,rect.Height+1);
                        this.LayoutManager.DrawBorder(g, rectB, false, this.Enabled, this.Focused, hot);
                    }
                }
                else
                {
                    this.LayoutManager.DrawControl(g, rect, this.BorderStyle, this.Enabled, this.Focused, hot);
                }
			}
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize (e);
			this.Invalidate();
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
                base.BackColor = LayoutManager.Layout.BackgroundColorInternal;
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

		protected IStyle		m_StylePainter;
  
		[Browsable(false)]
		public PainterTypes PainterType
		{
			get{return PainterTypes.Flat;}
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

	}
}
