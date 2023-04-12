using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.Data;
using System.Threading; 



namespace Nistec.WinForms
{
	[Description("Color Progress Bar")]
	[System.ComponentModel.ToolboxItem(true)]
	[Designer(typeof(Design.ProgressBarDesigner))]
	[ToolboxBitmap (typeof(McProgressBar),"Toolbox.ProgressBar.bmp")]
	public class McProgressBar :Nistec.WinForms.Controls.McContainer// System.Windows.Forms.Control,ILayout
	{	
		#region Members
		//
		// set default values
		//
		private int _Value = 0;
		private int _Minimum = 0;
		private int _Maximum = 100;
		private int _Step = 10;
		private bool doLoop=false;
		private bool forword=true;
        //private ControlLayout m_ControlLayout;


		private FillStyles _FillStyle = FillStyles.Dashed;

		private Color _BarColor = Color.FromArgb(255, 128, 128);
		private Color _BarExColor = Color.White ;
		private Color _BorderColor = Color.Gray;

		public event EventHandler ValueChanged;
		public event EventHandler Finished;
		public event EventHandler MinimumChanged;
		public event EventHandler MaximumChanged;

		public enum FillStyles
		{
			Solid,
			Dashed
		}
		#endregion

		#region constructor

		public McProgressBar()
		{
			base.Size = new Size(150, 15);
			SetStyle(
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.ResizeRedraw |
				ControlStyles.DoubleBuffer,
				true
				);
			//Nistec.Util.Net.netWinMc.NetFram(this.Name);
		}

        //internal McProgressBar(bool net)
        //{
        //    this.m_netFram=net;
        //}

		protected override void Dispose(bool disposing)
		{
			if(this.countTimer!=null)
			{
				ReleaseTimer();
				countTimer.Tick   -= new System.EventHandler(OnTimerTick);
			}
			base.Dispose (disposing);
		}

		#endregion

		#region override 

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            SerializeBarColor(Color.Empty, false);
        }
		public virtual void OnValueChanged(EventArgs e)
		{
			if(this.ValueChanged!=null)
				this.ValueChanged(this,e);
		}

		public virtual void OnFinished(EventArgs e)
		{
			if(this.Finished!=null)
				this.Finished(this,e);
		}

		public virtual void OnMinimumChanged(EventArgs e)
		{
			if(this.MinimumChanged!=null)
				this.MinimumChanged(this,e);
		}

		public virtual void OnMaximumChanged(EventArgs e)
		{
			if(this.MaximumChanged!=null)
				this.MaximumChanged(this,e);
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
                if (value == Color.Transparent)
                    value = Color.White;
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
            //if (color.IsEmpty || color== Color.Transparent)
            //    color = Color.White;

        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected void SerializeBarColor(Color value, bool force)
        {
            if (ShouldSerializeBackColor())
            {
                _BarColor = LayoutManager.Layout.CaptionColorInternal;
            }
            else if (force)
            {
                _BarColor = value;
            }

            if (ControlLayout == ControlLayout.Flat)
                _BarExColor = _BarColor;
            else
                _BarExColor = LayoutManager.Layout.LightLightColor;

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

        protected override void OnStylePropertyChanged(PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("ControlLayout") || e.PropertyName.Equals("BackColor"))
                SerializeBackColor(Color.Empty, false);
            if (e.PropertyName.Equals("ControlLayout") || e.PropertyName.Equals("ForeColor"))
                SerializeForeColor(Color.Empty, false);
            if (e.PropertyName.Equals("ControlLayout") || e.PropertyName.Equals("TextFont"))
                SerializeFont(Form.DefaultFont, false);

            SerializeBarColor(Color.Empty,false);
            _BorderColor = LayoutManager.Layout.BorderColorInternal;

            if ((DesignMode || IsHandleCreated))
                this.Invalidate(true);
        }
        protected override void OnStylePainterChanged(EventArgs e)
        {
            base.OnStylePainterChanged(e);
            OnStylePropertyChanged(new PropertyChangedEventArgs("ControlLayout"));
        }

 
        [Category("Style")]
        public override ControlLayout ControlLayout
        {
            get { return base.ControlLayout; }
            set
            {
                base.ControlLayout = value;
                OnStylePropertyChanged(new PropertyChangedEventArgs("ControlLayout"));
                this.Invalidate();
            }
        }

        [Description("Progress Bar color")]
        [Category("Style")]
        [DefaultValue(typeof(Color), "255, 128, 128")]
        public Color BarColor
        {
            get
            {
                return _BarColor;
            }
            set
            {
                SerializeBarColor(value,true);
            }
        }

         #endregion


		#region Properties

		[Browsable(false),EditorBrowsable(EditorBrowsableState.Never),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool AutoChildrenStyle
		{
			get{return base.AutoChildrenStyle;}
			set
			{
				base.AutoChildrenStyle=value;
			}
		}



//		[Description( "McProgressBar color")]
//		[Category( "McProgressBar" )]
//		[DefaultValue(typeof(Color),"White")]
//		public Color BarExColor
//		{
//			get
//			{
//				return _BarExColor;
//			}
//			set
//			{
//				_BarExColor = value;
//				this.Invalidate();
//			}
//		}

		[Description( "ProgressBar fill style")]
        [Category("Style")]
		[DefaultValue(FillStyles.Dashed)]
		public FillStyles FillStyle
		{
			get
			{
				return _FillStyle;
			}
			set
			{
				_FillStyle = value;
				this.Invalidate();
			}
		}

		[Description( "The current value for the McProgressBar, "+
			 "in the range specified by the Minimum and Maximum properties." )]
		[Category( "ProgressBar" )]
		// the rest of the Properties windows must be updated when this peroperty is changed.
		[RefreshProperties(RefreshProperties.All)]
		public int Value
		{
			get
			{
				return _Value;
			}
			set
			{
				if (value < _Minimum)
				{
					throw new ArgumentException("'"+value+"' is not a valid value for 'Value'.\n"+
						"'Value' must be between 'Minimum' and 'Maximum'.");
				}

				if (value > _Maximum)
				{
					throw new ArgumentException("'"+value+"' is not a valid value for 'Value'.\n"+
						"'Value' must be between 'Minimum' and 'Maximum'.");
				}
				if(IsHandleCreated)
				{
					if ( _Value!=0 &&  _Value!=value && value % _Step==0)
					{
						Application.DoEvents();
					}
					_Value = value;			
					this.Invalidate();
					OnValueChanged(EventArgs.Empty);
					if(!doLoop && (_Value==_Maximum || _Value==_Minimum))
					{
						OnFinished(EventArgs.Empty);
					}
				}

			}
		}
		
		[Description("The lower bound of the range this McProgressBar is working with.")]
		[Category("ProgressBar")]
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue(0)]
		public int Minimum
		{
			get
			{
				return _Minimum;
			}
			set
			{
				if(_Minimum != value)
				{
					_Minimum = value;

					if (_Minimum > _Maximum)
						_Maximum = _Minimum;
					if (_Minimum > _Value)
						_Value = _Minimum;

					this.Invalidate();
					OnMinimumChanged(EventArgs.Empty);
				}
			}
		}

		[Description("The uppper bound of the range this McProgressBar is working with.")]
		[Category("ProgressBar")]
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue(100)]
		public int Maximum
		{
			get
			{
				return _Maximum;
			}
			set
			{
				if(_Maximum != value)
				{
					_Maximum = value;

					if (_Maximum < _Value)
						_Value = _Maximum;
					if (_Maximum < _Minimum)
						_Minimum = _Maximum;

					this.Invalidate();
					OnMaximumChanged(EventArgs.Empty);
				}
			}
		}

		[Description("The amount to jump the current value of the control by when the Step() method is called.")]
		[Category("ProgressBar")]		
		[DefaultValue(10)]
		public int Step
		{
			get
			{
				return _Step;
			}
			set
			{
				if(value > 0 && _Step!=value)
				{
					_Step = value;
					this.Invalidate();
				}
			}
		}

//		[Description("The border color of McProgressBar")]
//		[Category("McProgressBar")]		
//		[DefaultValue(typeof(Color),"Gray")]
//		public Color BorderColor
//		{
//			get
//			{
//				return _BorderColor;
//			}
//			set
//			{
//				_BorderColor = value;
//				this.Invalidate();
//			}
//		}
		
		#endregion

		#region Timer

		private System.Windows.Forms.Timer countTimer;
		private IntPtr m_ptr;

		private void SetupTimer()
		{
			//countTimer = null;
			m_ptr=IntPtr.Zero ;
			countTimer = new System.Windows.Forms.Timer();
			countTimer.Enabled = false;
			this.countTimer.Interval =10;
			countTimer.Tick   += new System.EventHandler(OnTimerTick);
		}
	
		
		private void OnTimerTick(object sender, EventArgs e)
		{
			IntPtr ptr= Nistec.Win32.WinAPI.GetActiveWindow();
			if(doLoop)
			DoLoopInternal();
			ptr=IntPtr.Zero ;
		}

		private void SetTimer()
		{
			if(this.Enabled)
			{
				m_ptr=IntPtr.Zero; 
				//m_ptr= Nistec.Win32.WinAPI.GetActiveWindow();
				//this.Focus ();
				//this.Invalidate(false);
				//doLoop=true;
				//this.OnTimerTick(this, EventArgs.Empty);
				//this.countTimer.Interval =10;
				this.countTimer.Enabled = true;
			}
		}

		private void ReleaseTimer()
		{
			if(this.countTimer!=null)
			{
				m_ptr=IntPtr.Zero;
				this.countTimer.Stop (); 
				this.countTimer.Enabled = false;
				this.Invalidate(false);
			}
		}

		#endregion

		#region DoLoop

		public void DoLoop()
		{

			if(doLoop)
			{
				StopLoop();
				return;
			}

			if(this.countTimer==null)
			{
				SetupTimer();
			}

			this.Maximum=100;
			this.Minimum=0;
			forword=true;
			this.BackColor= _BarColor;
			doLoop=true;
			SetTimer();
		}

		private void DoLoopInternal()
		{
			if(forword)
			{
				if(this.Value>=this.Maximum)
					forword=false;
				else 
					_Value +=1;
			}
			else
			{
				if(this.Value<=this.Minimum)
					forword=true;
				else
					_Value -=1;
			}

			this.Invalidate (false);
		}

		private void StopLoop()
		{
			ReleaseTimer();
			doLoop=false;
			this.Value=0;
			this.BackColor= Color.White;
		}

		#endregion

		#region public methods
		//
		// Call the PerformStep() method to increase the value displayed by the amount set in the Step property
		//
		public void PerformStep()
		{
			if ((_Value+_Step) < _Maximum)
				Value += _Step;
			else
				Value = _Maximum;

			this.Invalidate();
		}
		
		//
		// Call the PerformStepBack() method to decrease the value displayed by the amount set in the Step property
		//
		public void PerformStepBack()
		{
			if ((Value-+_Step) > _Minimum)
				Value -= _Step;
			else
				Value = _Minimum;

			this.Invalidate();
		}

		//
		// Call the Increment() method to increase the value displayed by an integer you specify
		// 
		public void Increment(int value)
		{
			if (_Value < _Maximum)
				Value += value;
			else
				Value = _Maximum;

			this.Invalidate();
		}
		
		//
		// Call the Decrement() method to decrease the value displayed by an integer you specify
		// 
		public void Decrement(int value)
		{
			if (_Value > _Minimum)
				Value -= value;
			else
				Value = _Minimum;

			this.Invalidate();
		}

		public void Clear()
		{
			this.Value=_Minimum;
		}

		public void Finish()
		{
			this.Value=_Maximum;
		}

		#endregion

		#region override and paint

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			//
			// Calculate matching colors
			//
			if(BackColor==Color.Transparent  )
				BackColor=Color.White;
 
			Color bgColor =BackColor;// ControlPaint.Dark(BackColor);
			Color bkgColor =Parent.BackColor;
			e.Graphics.FillRectangle (new SolidBrush (bkgColor),ClientRectangle ); 

			Color darkColor = ControlPaint.Dark(_BarColor);
            //this.BackColor =Color.White ;  
			//
			// Fill background
			//
			using(SolidBrush bgBrush = new SolidBrush(bgColor))
			{
				// The region of the progress bar!
				int leftbar = 1;
				int topbar = 1;
				int X = this.Width-1;
				int Y = this.Height-1;
				Point[] points = {   new Point(leftbar + 2, topbar), 
									 new Point(X-2, topbar), 
									 new Point(X-1, topbar + 1), 
									 new Point(X, topbar + 2), 
									 new Point(X, Y-3), 
									 new Point(X-1, Y-2), 
									 new Point(X-2, Y-1), 
									 new Point(leftbar + 2, Y), 
									 new Point(leftbar + 1, Y-2), 
									 new Point(leftbar, Y-3), 
									 new Point(leftbar, topbar + 2),
									 new Point(leftbar + 1, topbar + 1),
				};    
				GraphicsPath path = new GraphicsPath();
				path.AddLines(points);

				Region reg = new Region(path);
				e.Graphics.FillRegion(bgBrush, reg); //bgBrush
				//bgBrush.Dispose();
			}
			// 
			// Check for value
			//
			if (_Maximum == _Minimum || _Value == 0)
			{
				// Draw border only and exit;
				drawBorder(e.Graphics);
				return;
			}

			//
			// The following is the width of the bar. This will vary with each value.
			//
			int fillWidth = (this.Width * _Value) / (_Maximum - _Minimum);
			
			//
			// GDI+ doesn't like rectangles 0px wide or high
			//
			if (fillWidth == 0)
			{
				// Draw border only and exit;
				drawBorder(e.Graphics);
				return;
			}

			//Make the bars of the progress complete, just like XP bars
			if(fillWidth%8 != 0)
			{
				int rest = fillWidth % 8;
				fillWidth += (8-rest);
			}

			//
			// Rectangles for upper and lower half of bar
			//
			Rectangle chunkbar = new Rectangle(3,2, fillWidth, this.Height-4);
			//Rectangle topRect = new Rectangle(0, 0, fillWidth, this.Height / 2);
			//Rectangle buttomRect = new Rectangle(0, this.Height / 2, fillWidth, this.Height / 2);

			//
			// The gradient brush
			//
			//LinearGradientBrush brush;			

			//
			// Paint upper half
			//
	
			using(LinearGradientBrush brush = new LinearGradientBrush(chunkbar, _BarExColor, _BarColor, 90.0f))
			{
				float[] relativeIntensities = {0.1f, 1.0f, 1.0f, 1.0f, 1.0f, 0.85f, 0.1f}; 
				float[] relativePositions =   {0.0f, 0.2f, 0.5f, 0.5f, 0.5f, 0.8f, 1.0f};

				// create a Blend object and assign it to silverBrush07
				Blend blend = new Blend();
				blend.Factors = relativeIntensities;
				blend.Positions = relativePositions; 
				brush.Blend = blend;

				e.Graphics.FillRectangle(brush, chunkbar);
				//brush.Dispose();
			}
			//
			// Calculate separator's setting
			//
			// Separator is made smaller like the XP progress
			int sepWidth = 8;//(int)(this.Height * .67);
			int sepCount = (int)(fillWidth / sepWidth);
			//Color sepColor = ControlPaint.LightLight(_BarColor);
			Color sepColor = bgColor;//ControlPaint.Dark(_BarColor);

			//
			// Paint separators
			//
			switch (_FillStyle)
			{
				case FillStyles.Dashed:
					// Draw each separator line
					for (int i = 0; i <= sepCount; i++)
					{
						e.Graphics.DrawLine(new Pen(sepColor, 2),
							sepWidth * i+3, 0, sepWidth * i+3, this.Height);
					}
					break;

				case FillStyles.Solid:
					// Draw nothing
					break;

				default:
					break;
			}

			//
			// Draw border and exit
			//
			//-drawBorder(e.Graphics);
            using (Pen pb = new Pen(_BorderColor, 1))
            {
                Rectangle rec = this.ClientRectangle;
                rec.Width--;
                rec.Height--;
                e.Graphics.DrawPath(pb, Nistec.Drawing.DrawUtils.GetRoundedRect(rec, 2));
            }
		}

		//
		// Draw border
		//
		protected void drawBorder(Graphics g)
		{

			// additions by DDISoft
			int X = this.ClientRectangle.Width;
			int Y = this.ClientRectangle.Height;
			Point[] points = {   new Point(1, 2), 
								 new Point(2, 1), 
								 new Point(3, 0), 
								 new Point(X-4, 0), 
								 new Point(X-3, 1), 
								 new Point(X-2, 2), 
								 new Point(X-2, Y-3), 
								 new Point(X-3, Y-2), 
								 new Point(X-4, Y-1), 
								 new Point(3, Y-1), 
								 new Point(2, Y-2),
								 new Point(1, Y-3),
								 new Point(1, 2), 
			};  
      
			Point[] points2 = {  new Point(2, 2), 
								 new Point(3, 1), 
								 new Point(4, 1), 
								 new Point(X-5, 1), 
								 new Point(X-4, 1), 
								 new Point(X-3, 2), 
			};


            g.DrawCurve(new Pen(Brushes.Gray, 1), points, 0);
            g.DrawCurve(new Pen(Brushes.LightGray, 1), points2, 0);
            g.DrawLine(new Pen(Brushes.LightGray, 1), 2, 2, 2, Y - 3);

			//Old original code
			/*Rectangle borderRect = new Rectangle(0, 0,
				ClientRectangle.Width - 1, ClientRectangle.Height - 1);
			g.DrawRectangle(new Pen(_BorderColor, 1), borderRect);*/
		}
		#endregion

	}

}