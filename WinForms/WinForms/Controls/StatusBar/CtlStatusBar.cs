using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Threading;

using Nistec.Win32; 
  
namespace Nistec.WinForms
{
	[ToolboxItem(true),Designer(typeof(Design.McDesigner))]
	[ToolboxBitmap (typeof(McStatusBar),"Toolbox.StatusBar.bmp")]
	public class McStatusBar: System.Windows.Forms.StatusBar ,IStatusBar,ILayout
	{	

		#region Members

        private System.Windows.Forms.BorderStyle m_BorderStyle;
        private ControlLayout m_ControlLayout;
        private int _StartPanelPosition = 0;
        //private int _HScrollPosition = 0;
        private McResize ctlResize;
        private McSpinner ctlSpinner;
        private bool _UseSpinner=false;
        [Category("Property Changed")]
        public event EventHandler ControlLayoutChanged;

        //protected int firstVisiblePanel = 0;
        //protected int neagtivePosition = 0;
        protected bool OwnerDrow = false;

		#endregion

		#region Constructor

		public McStatusBar(): base()
		{
            m_BorderStyle = BorderStyle.FixedSingle;
            m_ControlLayout = ControlLayout.Visual;

			this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            //Nistec.Util.Net.netWinMc.NetFram(this.Name); 


            this.ctlSpinner = new McSpinner();
            //this.ctlSpinner.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.ctlSpinner.Location = new Point(this.Right - 22, this.Bottom - 22);
            this.ctlSpinner.Visible = false;
            this.Controls.Add(this.ctlSpinner);

            this.ctlResize = new McResize();
            //this.ctlResize.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.ctlResize.Location = new Point(this.Right - 22, this.Bottom - 22);
            this.Controls.Add(this.ctlResize);


		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated (e);
			//Nistec.Util.Net.netWinMc.NetFram(this.Name); 
		}

		#endregion

		#region WndProc

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
 
            if (this.m_BorderStyle != BorderStyle.FixedSingle)
            {

                return;
            }

            Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);

            //this.LayoutManager.DrawStatusBar(g,rect,this,this.Enabled,m_PanelBackColor,m_PanelBorderColor);

            if (this.ShowPanels)
            {
                PaintPanels(e.Graphics, rect, this.RightToLeft);
            }
            else
            {
                PaintFlat(e.Graphics, rect, this.RightToLeft);
            }

            if (ShowProgress)
            {
                PaintProgress(e.Graphics, rect);
            }

        }

        protected virtual void PaintPanels(Graphics g, Rectangle rect, RightToLeft rtl)
        {

            Brush sbBack = null;
            Pen pen = LayoutManager.GetPenBorder();

            switch (m_ControlLayout)
            {
                case ControlLayout.Flat:
                    sbBack = LayoutManager.GetBrushFlat();
                    break;
                case ControlLayout.Visual:
                case ControlLayout.XpLayout:
                case ControlLayout.VistaLayout:
                    sbBack = LayoutManager.GetBrushGradient(rect, 270f);
                    break;
                case ControlLayout.System:
                    sbBack = LayoutManager.GetBrushFlat(FlatLayout.Dark);
                    break;

            }
            g.FillRectangle(sbBack, rect);
            g.DrawRectangle(pen, rect);

            sbBack.Dispose();

            if (this.OwnerDrow)
            {
                pen.Dispose();
                return;
            }

            //this.LayoutManager.DrawStatusBar(g,rect,this,this.Enabled,m_PanelBackColor,m_PanelBorderColor);

            IStyleLayout isb = this.LayoutManager;
            Brush sbFlat = isb.GetBrushFlat();
            Brush sbText = isb.GetBrushText();
            //pen = isb.GetPenBorder();

            float xp = 0;
            SizeF sf;

            int ctlWidth = this.Width;
            int startPosition = _StartPanelPosition;
            if (rtl== RightToLeft.Yes)
            {
                startPosition = ctlWidth - _StartPanelPosition;
            }

            int lf = startPosition;
            int calclf = _StartPanelPosition;
            Rectangle rectB;
            float yp;

            //StringFormat strf = LayoutManager.GetStringFormat(ContentAlignment.MiddleRight, true, this.RightToLeft == RightToLeft.Yes);
            //Brush sbBack=new SolidBrush (m_PanelBackColor);
            //Brush sb2=this.LayoutManager.GetBrushText();
            ContentAlignment ca = ContentAlignment.MiddleCenter;
            foreach (System.Windows.Forms.StatusBarPanel p in this.Panels)
            {
                if (rtl== RightToLeft.Yes)
                {
                    rectB = new Rectangle(lf - (p.Width + 1), 2, p.Width - 2, this.Height - 4);
                }
                else
                {
                    rectB = new Rectangle(lf + 1, 2, p.Width - 2, this.Height - 4);
                }
                g.FillRectangle(sbFlat, rectB);
                xp = 0;
                sf = g.MeasureString(p.Text, this.Font);

                switch (p.Alignment)
                {
                    case System.Windows.Forms.HorizontalAlignment.Left:
                        ca = ContentAlignment.MiddleLeft;
                        xp = (float)rectB.X;
                        break;
                    case System.Windows.Forms.HorizontalAlignment.Right:
                        ca = ContentAlignment.MiddleRight;
                        xp = (float)rectB.X + (float)rectB.Width - sf.Width;
                        break;
                    case System.Windows.Forms.HorizontalAlignment.Center:
                        ca = ContentAlignment.MiddleCenter;
                        xp = (float)rectB.X + (((float)rectB.Width - sf.Width) / 2);
                        break;
                }
                yp = (float)rectB.Y + (((float)rectB.Height - sf.Height) / 2);
                //LayoutManager.DrawString(g, rectB, ca, p.Text, this.Font);

                LayoutManager.DrawString(g, rectB, ca, p.Text, this.Font, rtl, true);

                //g.DrawString(p.Text, this.Font, sbText, xp, yp);

                g.DrawRectangle(pen, rectB);
                //ControlPaint.DrawBorder(g, rectB, m_PanelBorderColor , ButtonBorderStyle.Solid  );
                if (rtl== RightToLeft.Yes)
                {
                    lf -= p.Width;
                }
                else
                {
                    lf += p.Width;
                }
            }

            sbFlat.Dispose();
            sbText.Dispose();
            pen.Dispose();
        }

        protected virtual void PaintFlat(Graphics g, Rectangle rect,RightToLeft rtl)
        {

            Brush sbBack = null;
            Pen pen = LayoutManager.GetPenBorder();

            switch (m_ControlLayout)
            {
                case ControlLayout.Visual:
                case ControlLayout.XpLayout:
                case ControlLayout.VistaLayout:
                    sbBack = LayoutManager.GetBrushGradient(rect, 270f);
                    break;
                default://case ControlLayout.System:
                    sbBack = LayoutManager.GetBrushFlat();
                    break;

            }
            g.FillRectangle(sbBack, rect);
            g.DrawRectangle(pen, rect);

            sbBack.Dispose();
            pen.Dispose();


            //IStyleLayout isb = this.LayoutManager;
            //Brush sbFlat = isb.GetBrushFlat();
            //Pen pen = isb.GetPenBorder();
            //g.FillRectangle(sbFlat, rect);
            //g.DrawRectangle(pen, rect);

            Brush sbText = this.LayoutManager.GetBrushText();
            float xp = 0;
            SizeF sf;

            
            sf = g.MeasureString(this.Text, this.Font);

            if (rtl== RightToLeft.Yes)
            {
                xp = (float)rect.X + (float)rect.Width - sf.Width;
            }
            else
            {
                xp = (float)rect.X;
            }

            float yp = (float)rect.Y + (((float)rect.Height - sf.Height) / 2);

            g.DrawString(Text, this.Font, sbText, xp, yp);

            sbText.Dispose();
            //sbFlat.Dispose();
            //pen.Dispose();
        }

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);
        //    if (this.m_BorderStyle != BorderStyle.FixedSingle)
        //    {
               
        //        return;
        //    }

        //    Graphics g = e.Graphics;

        //    Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);

        //     //this.LayoutManager.DrawStatusBar(g,rect,this,this.Enabled,m_PanelBackColor,m_PanelBorderColor);

        //    IStyleLayout isb = this.LayoutManager;
        //    Brush sbFlat = isb.GetBrushFlat();
        //    Brush sbText = isb.GetBrushText();
        //    Pen pen = isb.GetPenBorder();
        //    Brush sbBack=null;

        //    switch (m_ControlLayout)
        //    {
        //        case ControlLayout.Flat:
        //            sbBack = isb.GetBrushFlat();
        //            break;
        //        case ControlLayout.Visual:
        //        case ControlLayout.XpLayout:
        //            sbBack = isb.GetBrushGradient(rect, 270f);
        //            break;
        //        case ControlLayout.System:
        //            sbBack = isb.GetBrushFlat(FlatLayout.Dark);
        //            break;

        //    }
 
        //    float xp = 0;
        //    SizeF sf;

        //    if (this.ShowPanels)
        //    {


        //        g.FillRectangle(sbBack, rect);
        //        g.DrawRectangle(pen, rect);

        //        //PaintPanels(g);

        //        RightToLeft rtl = this.RightToLeft;
        //        int ctlWidth = this.Width;
        //        int startPosition = _StartPanelPosition;
        //        if (rtl== RightToLeft.Yes)
        //        {
        //            startPosition = ctlWidth-_StartPanelPosition;
        //        }
        //        int lf = startPosition;
        //        int calclf = _StartPanelPosition;
        //        Rectangle rectB;
        //        float yp;

        //        //StringFormat strf = LayoutManager.GetStringFormat(ContentAlignment.MiddleRight, true, this.RightToLeft == RightToLeft.Yes);
        //        //Brush sbBack=new SolidBrush (m_PanelBackColor);
        //        //Brush sb2=this.LayoutManager.GetBrushText();
        //        ContentAlignment ca= ContentAlignment.MiddleCenter;
        //        int index = 0;
        //        int width = 0;
        //        int pos = this.neagtivePosition;
        //        foreach (System.Windows.Forms.StatusBarPanel p in this.Panels)
        //        {
        //            if (index < firstVisiblePanel)
        //            {
        //                continue;
        //            }
        //            index++;
        //            width = p.Width;
        //            if (rtl == RightToLeft.Yes)
        //            {
        //                rectB = new Rectangle(lf - (p.Width + 1), 2, p.Width - 2, this.Height - 4);
        //                if (calclf < _HScrollPosition)// || rectB.X < 0)
        //                {
        //                    lf -= (_HScrollPosition+5);
        //                    calclf += p.Width;
        //                    continue;
        //                }
        //                if (rectB.X < 0)
        //                {
        //                    calclf += p.Width;
        //                    continue;
        //                }
        //            }
        //            else
        //            {
        //                rectB = new Rectangle(lf + 1, 2, p.Width - 2, this.Height - 4);

        //                if (index == firstVisiblePanel)
        //                {
        //                    rectB = new Rectangle(lf + 1, 2, p.Width - _HScrollPosition - 2, this.Height - 4);
        //                    width = rectB.Width;
        //                    if (rectB.Width < 0)
        //                        continue;
        //                }
        //                //else
        //                //{
        //                //    rectB = new Rectangle(lf + 1, 2, p.Width - 2, this.Height - 4);
        //                //    if (calclf < _HScrollPosition)//|| lf + rectB.Width > ctlWidth)
        //                //    {
        //                //        lf = _HScrollPosition + 5;
        //                //        calclf += p.Width;
        //                //        continue;
        //                //    }
        //                //}
        //                if (lf + rectB.Width > ctlWidth)
        //                {
        //                    calclf += p.Width;
        //                    continue;
        //                }
        //            }
        //            g.FillRectangle(sbFlat, rectB);
        //            xp = 0;
        //            sf = g.MeasureString(p.Text, this.Font);

        //            switch (p.Alignment)
        //            {
        //                case System.Windows.Forms.HorizontalAlignment.Left:
        //                    ca= ContentAlignment.MiddleLeft;
        //                    xp = (float)rectB.X;
        //                    break;
        //                case System.Windows.Forms.HorizontalAlignment.Right:
        //                    ca= ContentAlignment.MiddleRight;
        //                    xp = (float)rectB.X + (float)rectB.Width - sf.Width;
        //                    break;
        //                case System.Windows.Forms.HorizontalAlignment.Center:
        //                     ca= ContentAlignment.MiddleCenter;
        //                    xp = (float)rectB.X + (((float)rectB.Width - sf.Width) / 2);
        //                    break;
        //            }
        //            yp = (float)rectB.Y + (((float)rectB.Height - sf.Height) / 2);
        //            //LayoutManager.DrawString(g, rectB, ca, p.Text, this.Font);
                    
        //            LayoutManager.DrawString(g, rectB, ca, p.Text, this.Font, rtl, true);
      
        //            //g.DrawString(p.Text, this.Font, sbText, xp, yp);

        //            g.DrawRectangle(pen, rectB);
        //            //ControlPaint.DrawBorder(g, rectB, m_PanelBorderColor , ButtonBorderStyle.Solid  );
        //            if (rtl == RightToLeft.Yes)
        //            {
        //                lf -= width;// p.Width;
        //            }
        //            else
        //            {
        //                lf += width;// p.Width;
        //            }
        //        }
        //        //sbBack.Dispose();
        //    }
        //    else
        //    {

        //        g.FillRectangle(sbFlat, rect);
        //        g.DrawRectangle(pen, rect);
        //        sf = g.MeasureString(this.Text, this.Font);

        //        switch (this.RightToLeft)
        //        {
        //            case System.Windows.Forms.RightToLeft.Yes:
        //                xp = (float)rect.X + (float)rect.Width - sf.Width;
        //                break;
        //            default:
        //                xp = (float)rect.X;
        //                break;
        //        }
        //        float yp = (float)rect.Y + (((float)rect.Height - sf.Height) / 2);

        //        g.DrawString(Text, this.Font, sbText, xp, yp);

        //    }
        //    sbFlat.Dispose();
        //    sbText.Dispose();
        //    sbBack.Dispose();
        //    pen.Dispose();
        //    if (ShowProgress)
        //    {
        //        PaintProgress(g, rect);
        //    }

        //}


		#endregion

		#region InternalEvents

        private void ctlSpinnerResize()
        {
            int height = Math.Min(20, this.Height - 4);
            this.ctlSpinner.Size =new Size(height, height);
            this.ctlSpinner.Location = new Point(this.Width - 22, (this.Height - height)/2);

        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ctlSpinnerResize();
            this.ctlResize.Location = new Point(this.Width - 22, this.Height - 22);
            this.Invalidate();
        }

        protected virtual void OnControlLayoutChanged(EventArgs e)
        {
             if (ControlLayoutChanged != null)
                ControlLayoutChanged(this, e);
        }


		#endregion

		#region Properties

        //[DefaultValue(ControlLayout.Visual)]    
        [Category("Style")]
        public virtual ControlLayout ControlLayout
        {
            get { return m_ControlLayout; }
            set
            {
                m_ControlLayout = value;
                this.OnControlLayoutChanged(EventArgs.Empty);
                this.Invalidate();
            }

        }

        [Category("Style")]
        public BorderStyle BorderStyle
        {
            get { return m_BorderStyle; }
            set
            {
                m_BorderStyle = value;
                this.Invalidate();
            }
        }
        
        [Category("Style")]
        public int StartPanelPosition
        {
            get { return _StartPanelPosition; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("value should be postive number");
                }
                _StartPanelPosition = value;
                this.Invalidate();
            }
        }
        
        //[Category("Style")]
        //public int HScrollPosition
        //{
        //    get { return _HScrollPosition; }
        //    set
        //    {
        //        _HScrollPosition = value;
        //        this.Invalidate();
        //    }
        //}


        [Category("Style")]
        public new bool SizingGrip
        {
            get 
            {
                if(BorderStyle!=  BorderStyle.FixedSingle)
                    return base.SizingGrip;
                return ctlResize.Visible; 
            }
            set
            {
                if (BorderStyle != BorderStyle.FixedSingle)
                {
                    ctlResize.Visible = false;
                    base.SizingGrip = value;
                }
                else
                {
                    ctlResize.Visible = value;
                }

                this.Invalidate();
            }
        }
		#endregion

		#region ILayout

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

		protected virtual void OnStylePainterChanged(EventArgs e)
		{
			this.BackColor=LayoutManager.Layout.BackgroundColorInternal;
			this.ForeColor=LayoutManager.Layout.ForeColorInternal;
		}

		protected virtual void OnStylePropertyChanged(PropertyChangedEventArgs e)
		{
			if((DesignMode || IsHandleCreated))
			{
				this.BackColor=LayoutManager.Layout.BackgroundColorInternal;
				this.ForeColor=LayoutManager.Layout.ForeColorInternal;
				this.Invalidate(true);
			}
		}

		private void m_Style_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{

			OnStylePropertyChanged(e);
		}

		#endregion

        #region progress

        private bool m_ShowProgress=false;
        private int m_ProgressMin=0;
        private int m_ProgressMax=100;
        private int m_ProgressValue = 0;
        private bool m_HideOnEnd = true;
        private bool m_Recycle = false;
        private bool m_IsRecycleRunning = false;
        private Thread _AsyncThread;
        //private delegate void AsyncDelegate();

        [DefaultValue(false)]
        public bool ShowProgress
        {
            get { return m_ShowProgress; }
            set 
            { 
                m_ShowProgress = value;
                this.Invalidate();
            }
        }

        [DefaultValue(0)]
        public int ProgressMin
        {
            get { return m_ProgressMin; }
            set
            {
                if (value >= 0 && value < m_ProgressMin)
                    m_ProgressMin = value;
            }
        }

        [DefaultValue(100)]
        public int ProgressMax
        {
            get { return m_ProgressMax; }
            set
            {
                if (value >= 0 && value > m_ProgressMin)
                    m_ProgressMax = value;
            }
        }

        public int ProgressValue
        {
            get { return m_ProgressValue; }
            set
            {
                if (value <= m_ProgressMax && value >= m_ProgressMin)
                {
                    m_ProgressValue = value;
                    this.Invalidate();
                }
            }
        }

        public bool IsRecycleRunning
        {
            get { return m_IsRecycleRunning; }
        }

        public void SetProgress(int min,int max,int value)
        {
            if (value <= max && value >= min)
            {
                m_Recycle = false;
                m_ProgressMax = max;
                m_ProgressMin = min;
                m_ProgressValue = value;
                this.Invalidate();
            }
        }

        public void SetProgress(int min, int max,bool hideOnEnd)
        {
                m_Recycle = false;
                m_ProgressMax = max;
                m_ProgressMin = min;
                m_ProgressValue = min;
                m_ShowProgress = true;
                m_HideOnEnd = hideOnEnd;
                this.Invalidate();
        }

        public void SetProgress(int value)
        {
            if (value >= m_ProgressMax)
            {
                ResetProgress(m_HideOnEnd);
            }
            else if (value >= m_ProgressMin)
            {
                m_ProgressValue = value;
                this.Invalidate();
            }
        }

        public void StartProgressResycle(bool useSpinner)
        {
            _UseSpinner = useSpinner;
            if (_UseSpinner)
            {
                ctlSpinner.ShowSpinner();
            }
            else
            {
                m_Recycle = true;
                ProgressResycleAsyncStart();
            }
            m_IsRecycleRunning = true;
        }
        public void StopProgressResycle()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new AsyncDelegate(StopProgressResycle));
                return;
            }

            if (_UseSpinner)
            {
                ctlSpinner.ResetSpinner();
            }
            else
            {
                m_Recycle = true;
                if (_AsyncThread != null && _AsyncThread.IsAlive)
                {
                    _AsyncThread.Abort();
                }
                ResetProgress(false);
            }
            m_IsRecycleRunning = false;
        }

        protected virtual void ProgressResycleAsyncStart()
        {
            if (_AsyncThread != null && _AsyncThread.IsAlive)
            {
                return;
            }
            _AsyncThread = new Thread(new ThreadStart(ProgressResycleAsyncWorker));
            _AsyncThread.IsBackground = true;
            _AsyncThread.Start();
        }

        protected virtual void ProgressResycleAsyncWorker()
        {
            for (int i = 0; i <= 100; ++i)
            {
                SetProgress(i);
                if (i == 100) i = 0;
                Thread.Sleep(10);
            }
        }

        public void ResetProgress(bool show)
        {
            m_ProgressValue = m_ProgressMin;
            m_ShowProgress = show;
            this.Invalidate();
        }

        protected virtual void PaintProgress(Graphics g, Rectangle bounds)
        {
            int rectWidth = 80;
            int resycleWidth = 20;
            int rectHeight = bounds.Height - 4;
            Rectangle rect = new Rectangle(bounds.Width - (rectWidth + 22), (bounds.Height - rectHeight) / 2, rectWidth, rectHeight);

            using (Brush sbBack = LayoutManager.GetBrushBack())
            {
                g.FillRectangle(sbBack, rect);

                Rectangle fillRect = new Rectangle(rect.Left + 2, rect.Top + 2, rect.Width - 3, rect.Height - 3);

                int maxWidth = (int)fillRect.Width;
                int val = ProgressValue;

                val = (int)(val * 100) / (m_ProgressMax - m_ProgressMin);
                double indexWidth = ((double)fillRect.Width) / 100; // determines the width of each index.
                fillRect.Width = (int)(val * indexWidth);
                if (fillRect.Width > maxWidth)
                {
                    fillRect.Width = maxWidth;
                }
                if (fillRect.Width > 0)
                {
                    using (Brush sb = LayoutManager.GetBrushCaptionGradient(fillRect, 90f, true))
                    {
                        g.FillRectangle(sb, fillRect);
                    }
                }
                if (m_Recycle && fillRect.Width > resycleWidth)
                {
                    Rectangle rectClear = new Rectangle(fillRect.X, fillRect.Y, fillRect.Width - resycleWidth, fillRect.Height);
                    g.FillRectangle(sbBack, rectClear);
                }
            }
            using (Pen pen = LayoutManager.GetPenBorder())
            {
                g.DrawRectangle(pen, rect);
            }
        }

        #endregion

 
       
    }
}
