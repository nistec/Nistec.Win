using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;


using Nistec.Data;
using Extension.Nistec.Threading;
using System.Threading;

namespace Nistec.WinForms
{
	[System.ComponentModel.ToolboxItem(false)]
	public  class McUserControl : System.Windows.Forms.UserControl,ILayout//,IBind
	{

	
		#region Members
		private System.ComponentModel.Container components = null;
		private Image			m_Image;
		private BorderStyle	    m_BorderStyle;
		private   bool			autoChildrenStyle;
        protected StyleGuide StyleGuideBase;
        private ControlLayout m_ControlLayout;


		#endregion

		#region Constructors

		public McUserControl()
		{
            this.m_ControlLayout = ControlLayout.System;
            this.m_BorderStyle = BorderStyle.None;
			autoChildrenStyle=true;
			InitializeComponent();
            //if (StylePainter == null)
            //{
            //    this.StylePainter = StyleGuideBase;
            //}
		}

        //internal McUserControl(bool net)
        //{	
        //    InitializeComponent();
        //    m_netFram=net;
        //    if (StylePainter == null)
        //    {
        //        this.StylePainter = StyleGuideBase;
        //    }
        //}

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
            this.StyleGuideBase = new Nistec.WinForms.StyleGuide();//this.components);
            //this.StyleGuideBase.ColorStyleChanged += new Nistec.WinForms.ColorStyleChangedEventHandler(this.StyleGuideBase_ColorStyleChanged);
            //this.StyleGuideBase.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(this.StyleGuideBase_PropertyChanged);
            // 
			// McUserControl
			// 
			this.Name = "McUserControl";
		}
		#endregion

        #region ILayout

        protected IStyle m_StylePainter;

        [Category("Style"), DefaultValue(ControlLayout.VistaLayout)]
        public virtual ControlLayout ControlLayout
        {
            get { return m_ControlLayout; }
            set
            {
                if (value != m_ControlLayout)
                {
                    m_ControlLayout = value;
                    this.Invalidate();
                }
            }
        }

 
        [Category("Style"),RefreshProperties(RefreshProperties.All)]
        //[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public IStyle StylePainter
        {
            get { return m_StylePainter; }
            set
            {
                if (m_StylePainter != value)
                {
                    if (this.m_StylePainter != null)
                        this.m_StylePainter.PropertyChanged -= new PropertyChangedEventHandler(StyleGuideBase_PropertyChanged);
                    if (value == null)
                        m_StylePainter = StyleGuideBase;
                    else
                        m_StylePainter = value;
                    if (this.m_StylePainter != null)
                        this.m_StylePainter.PropertyChanged += new PropertyChangedEventHandler(StyleGuideBase_PropertyChanged);
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
                if (this.m_StylePainter != null)
                    return this.m_StylePainter.Layout as IStyleLayout;
                else
                    return StyleLayout.DefaultLayout as IStyleLayout;// this.m_Style as IStyleLayout;
            }
        }

        protected virtual void SetDefaultLayout()
        {
            this.SetStyleLayout(StyleLayout.DefaultStylePlan);
            SetChildrenStyle(false);
        }

        public virtual void SetStyleLayout(IStyle style)
        {
            if (style != null)
            {
                this.SetStyleLayout(style.Layout);
            }
        }

        public virtual void SetStyleLayout(StyleLayout value)
        {
            if (this.m_StylePainter != null)
                this.m_StylePainter.Layout.SetStyleLayout(value);
        }

        public virtual void SetStyleLayout(Styles value)
        {
            if (this.m_StylePainter != null)
                this.m_StylePainter.Layout.SetStyleLayout(value);
        }

  
        protected virtual void OnStylePainterChanged(EventArgs e)
        {
            OnStylePropertyChanged(new PropertyChangedEventArgs("ControlLayout"));
            if (autoChildrenStyle)
            {
                SetChildrenStyle(false);
            }
        }

        protected virtual void SetChildrenStyle(bool clear)
        {
            foreach (Control c in this.Controls)
            {
                if (c is ILayout)
                {
                    ((ILayout)c).StylePainter = clear ? null : this.StylePainter;
                }
            }
            this.Invalidate(true);
        }

        private void StyleGuideBase_ColorStyleChanged(object sender, ColorStyleChangedEventArgs e)
        {
            ColorStyleChanged(e);
        }

        protected virtual void ColorStyleChanged(ColorStyleChangedEventArgs e)
        {
            //
        }

        private void StyleGuideBase_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnStylePropertyChanged(e);
        }

        protected virtual void OnStylePropertyChanged(PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("ControlLayout") || e.PropertyName.Equals("ColorBrush1") || e.PropertyName.Equals("BackgroundColor"))
                SerializeBackColor(Color.Empty, false);
            if (e.PropertyName.Equals("ControlLayout") || e.PropertyName.Equals("ForeColor"))
                SerializeForeColor(Color.Empty, false);

            if ((DesignMode || IsHandleCreated))
                this.Invalidate(true);
        }

        public Nistec.WinForms.PainterTypes PainterType
        {
            get
            {

                return Nistec.WinForms.PainterTypes.Guide;
            }
        }

 
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected void SerializeForeColor(Color value, bool force)
        {
            if (ShouldSerializeForeColor())
                base.ForeColor = LayoutManager.Layout.ForeColorInternal;
            else if (force)
                base.ForeColor = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected void SerializeBackColor(Color value, bool force)
        {
            switch (m_ControlLayout)
            {
                case ControlLayout.Visual:
                case ControlLayout.XpLayout:
                case ControlLayout.VistaLayout:
                    base.BackColor = LayoutManager.Layout.ColorBrush1Internal;
                    break;
                default:
                    if (IsHandleCreated)
                        base.BackColor = LayoutManager.Layout.BackgroundColorInternal;
                    else if (force)
                        base.BackColor = value;
                    break;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected bool ShouldSerializeBackColor()
        {
            if (!IsHandleCreated)
                return false;
            return true;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected bool ShouldSerializeForeColor()
        {
            return IsHandleCreated;
        }

        #endregion

		#region Properties

		[Category("Style"),Browsable(true),EditorBrowsable(EditorBrowsableState.Advanced),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool AutoChildrenStyle
		{
			get{return autoChildrenStyle;}
			set
			{
				if(autoChildrenStyle!=value)
				{
					autoChildrenStyle=value;
					SetChildrenStyle(!value);
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

		[Category("Appearance")]
		[DefaultValue(null),
		System.ComponentModel.RefreshProperties(RefreshProperties.Repaint)]
		public Image Image
		{
			get	{ return m_Image; }
			set
			{
				if(m_Image != value)
				{
					m_Image = value;
					this.Invalidate();
				}
			}
		}

  
		[Category("Appearance")]//,DefaultValue(BorderStyle.FixedSingle)]
		public new System.Windows.Forms.BorderStyle BorderStyle
		{
			get {return m_BorderStyle;}
			set 
			{
				if(m_BorderStyle != value)
				{
					m_BorderStyle = value;
					//SetSize();
					this.Invalidate ();
				}
			}
		}


		#endregion

        #region Overrides

        protected override void OnControlAdded(ControlEventArgs e)
		{
			base.OnControlAdded(e);
			if( e.Control is ILayout)// && Parent is ILayout)
			{
				((ILayout)e.Control).StylePainter=this.StylePainter;//((ILayout)Parent).StyleGuide; 
			}
		}

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			base.OnPaint(e);

			//DrawControl(e.Graphics);

            Rectangle rect = ClientRectangle;
            rect.Width --;
            rect.Height --;

                switch (ControlLayout)
                {
                    case ControlLayout.Visual:
                    case ControlLayout.XpLayout:
                    case ControlLayout.VistaLayout:
                        using (Brush sb = LayoutManager.GetBrushGradient(rect, 270f))
                        {
                            e.Graphics.FillRectangle(sb, rect);
                        }
                        break;
                    default:
                        //using (Brush b = LayoutManager.GetBrushFlat())
                        //{
                        //    e.Graphics.FillRectangle(b, rect);
                        //}
                        break;
                }

            if (m_BorderStyle == BorderStyle.FixedSingle)
            {
                using (Pen pen = LayoutManager.GetPenBorder())
                {
                    e.Graphics.DrawRectangle(pen, rect);
                }
                //this.LayoutManager.DrawBorder(e.Graphics, rect, this.ReadOnly, this.Enabled, false, false);
            }
            else if (m_BorderStyle == BorderStyle.Fixed3D)
            {
                ControlPaint.DrawBorder3D(e.Graphics, ClientRectangle, System.Windows.Forms.Border3DStyle.Sunken);
            }

            //if (m_BorderStyle == BorderStyle.FixedSingle)
            //{
            //    Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            //    this.LayoutManager.DrawBorder(e.Graphics, rect, this.ReadOnly, this.Enabled, false, false);
            //}
            //else if (m_BorderStyle == BorderStyle.Fixed3D)
            //{
            //    Rectangle rect = this.ClientRectangle;
            //    ControlPaint.DrawBorder3D(e.Graphics, rect, Border3DStyle.Sunken);
            //}

		}

        //protected override void OnMouseEnter(System.EventArgs e)
        //{
        //    base.OnMouseEnter(e);
        //    DrawControl(true);
        //}

        //protected override void OnMouseLeave(System.EventArgs e)
        //{
        //    base.OnMouseLeave(e);
        //    DrawControl(false);
        //}

        //protected override void OnGotFocus(System.EventArgs e)
        //{
        //    base.OnGotFocus(e);
        //    //ResetError();
        //    DrawControl(false);
        //}

        //protected override void OnLostFocus(System.EventArgs e)
        //{
        //    base.OnLostFocus(e);
        //    DrawControl(false);
        //}

        //protected override void OnSizeChanged(System.EventArgs e)
        //{
        //    base.OnSizeChanged (e);
        //    SetSize();
        //}

        //protected override void OnFontChanged(EventArgs e)
        //{
        //    base.OnFontChanged (e);
        //    SetSize();
        //}

		protected override void OnEnabledChanged(System.EventArgs e)
		{
			base.OnEnabledChanged (e);
            this.Invalidate();// DrawControl(false);
		}

		protected override void OnParentBackColorChanged(EventArgs e)
		{
			base.OnParentBackColorChanged (e);
			this.Invalidate ();
		}

		#endregion

        #region Async Command
        //private AsyncCommand acmd;

        //[Browsable(false)]
        //protected AsyncCommand AsyncCommand
        //{
        //    get { return acmd; }
        //}
        //[Browsable(false)]
        //public AsyncState AsyncCommandState
        //{
        //    get
        //    {
        //        if (acmd == null)
        //            return AsyncState.None;
        //        return acmd.AsyncState;
        //    }
        //}
        //public void AsyncCmd(IDbConnection cnn, string sql, string tableName)
        //{
        //    AsyncCmd(cnn, sql, tableName, AsyncProgressLevel.None);
        //}
        //public void AsyncCmd(IDbConnection cnn, string sql, string tableName, AsyncProgressLevel level)
        //{
        //    if (AsyncCommandState == AsyncState.Started)
        //        return;

        //    _currentAsync = 2;
        //    if (acmd != null)
        //    {
        //        UnWireAsyncCmd();
        //        acmd.Dispose();
        //        acmd = null;
        //    }
        //    acmd = new AsyncCommand(cnn, level);
        //    acmd.AsyncCompleted += new AsyncDataResultEventHandler(acmd_ExecutingResultEvent);
        //    acmd.AsyncProgress += new AsyncProgressEventHandler(async_ExecutingProgressEvent);
        //    acmd.AsyncCancelExecuting += new EventHandler(async_CancelExecutingEvent);
        //    acmd.AsyncBeginInvoke(sql, tableName);
        //}

        //private void UnWireAsyncCmd()
        //{
        //    if (acmd == null) return;
        //    acmd.AsyncCompleted -= new AsyncDataResultEventHandler(acmd_ExecutingResultEvent);
        //    acmd.AsyncProgress -= new AsyncProgressEventHandler(async_ExecutingProgressEvent);
        //    acmd.AsyncCancelExecuting -= new EventHandler(async_CancelExecutingEvent);
        //    _currentAsync = 0;
        //}

        //void acmd_ExecutingResultEvent(object sender, AsyncDataResultEventArgs e)
        //{
        //    OnAsyncCommandCompleted(e);
        //}

        //protected virtual void OnAsyncCommandCompleted(AsyncDataResultEventArgs e)
        //{
        //    UnWireAsyncCmd();
        //}
        #endregion

        #region Async Invoke

        private AsyncInvoke async;
        private int _currentAsync;//0=none,1=invoke,2=cmd

        [Browsable(false)]
        protected AsyncInvoke AsyncInvoke
        {
            get { return async; }
        }
        [Browsable(false)]
        public AsyncState AsyncState
        {
            get
            {
                if (async == null)
                    return AsyncState.None;
                return async.AsyncState;
            }
        }
        public void AsyncBeginInvoke(object args)
        {
            AsyncBeginInvoke(args, AsyncProgressLevel.None);
        }
        public void AsyncBeginInvoke(object args, AsyncProgressLevel level)
        {
            if (AsyncState == AsyncState.Started)
                return;
            if (async != null)
            {
                UnWireAsyncInvoke();
                async.Dispose();
                async = null;
            }
            _currentAsync = 1;
            async = new AsyncInvoke(level);
            async.AsyncCompleted += new AsyncCallEventHandler(async_ExecutingResultEvent);
            async.AsyncProgress += new AsyncProgressEventHandler(async_ExecutingProgressEvent);
            async.AsyncExecutingWorker += new AsyncCallEventHandler(async_AsyncExecutingWorker);
            async.AsyncCancelExecuting += new EventHandler(async_CancelExecutingEvent);
            async.AsyncBeginInvoke(args);
        }

        protected void AsyncDispose()
        {
            if (async != null)
            {
                UnWireAsyncInvoke();
                async.Dispose();
                async = null;
            }
            //if (acmd != null)
            //{
            //    UnWireAsyncCmd();
            //    acmd.Dispose();
            //    acmd = null;
            //}
        }

        private void UnWireAsyncInvoke()
        {
            if (async == null) return;
            async.AsyncCompleted -= new AsyncCallEventHandler(async_ExecutingResultEvent);
            async.AsyncProgress -= new AsyncProgressEventHandler(async_ExecutingProgressEvent);
            async.AsyncExecutingWorker -= new AsyncCallEventHandler(async_AsyncExecutingWorker);
            async.AsyncCancelExecuting -= new EventHandler(async_CancelExecutingEvent);
            _currentAsync = 0;
        }
        protected virtual void AsyncCancelExecution()
        {
            if (async != null && _currentAsync == 1)
                async.StopCurrentExecution();
            //else if (acmd != null && _currentAsync == 2)
            //    acmd.StopCurrentExecution();
        }
        void async_CancelExecutingEvent(object sender, EventArgs e)
        {
            OnAsyncCancelExecuting(e);
        }
        void async_AsyncExecutingWorker(object sender, AsyncCallEventArgs e)
        {
            OnAsyncExecutingWorker(e);
        }

        void async_ExecutingProgressEvent(object sender, AsyncProgressEventArgs e)
        {
            OnAsyncExecutingProgress(e);
        }

        void async_ExecutingResultEvent(object sender, AsyncCallEventArgs e)
        {
            OnAsyncCompleted(e);
        }

        protected virtual void OnAsyncCancelExecuting(EventArgs e)
        {
            if (_currentAsync == 1)
                UnWireAsyncInvoke();
            //else if (_currentAsync == 2)
            //    UnWireAsyncCmd();
        }

        protected virtual void OnAsyncExecutingWorker(AsyncCallEventArgs e)
        {
           
        }

        protected virtual void OnAsyncExecutingProgress(AsyncProgressEventArgs e)
        {

        }
        protected virtual void OnAsyncCompleted(AsyncCallEventArgs e)
        {
            UnWireAsyncInvoke();
        }

        #endregion



	}
}
