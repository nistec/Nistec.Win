using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;


using Nistec.Win32;
using Nistec.Win;

namespace Nistec.WinForms
{

	public enum ShapeTypes
	{
      Rectangle,
      Ellipse,
      Line,
	  XpLine
	}

	public enum ShapeLayers
	{
		Back,
		Front
	}

	[Designer(typeof(Design.ShapesDesigner))]
	[ToolboxItem(true)]
	[ToolboxBitmap (typeof(McShapes),"Toolbox.Shapes.bmp")]
	public class McShapes : Nistec.WinForms.Controls.McControl//.McBase //,ILayout 
	{
	
		#region Members
		private Color m_LineColor=Color.DarkGray; 
		private Color m_ShapeColor=Color.Transparent; 
		
		private ShapeTypes m_ShapeType=ShapeTypes.Rectangle; 
		private ShapeLayers m_ShapeLayer=ShapeLayers.Back; 
		private int m_LineWidth = 1;

		//xpLine
		private LineDirection m_LineDirection = LineDirection.Horizontal;
		private bool m_UseInterpolation = true;
//		private Color m_StartColor = Color.White;
//		private Color m_EndColor = Color.Black;

	
		#endregion

		#region Constructors
		
		public McShapes()//:base(BorderStyle.None)
		{

			//base.SetStyle(ControlStyles.AllPaintingInWmPaint,true);

			base.SetStyle(ControlStyles.ResizeRedraw,true);
			base.SetStyle(ControlStyles.DoubleBuffer  | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint,true);
			base.SetStyle(ControlStyles.Selectable,false);
			base.SetStyle(ControlStyles.SupportsTransparentBackColor,true);
//			base.BackColor =Color.Transparent;
			base.TabStop=false;
			//base.FixSize=false;
			this.SendToBack (); 
		}

		#endregion

		#region Paint background

		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
			base.OnPaintBackground (pevent);
			if(m_ShapeType==ShapeTypes.Ellipse)
			{
				PaintBackground(pevent,ClientRectangle);
			}
		}

		internal protected void PaintBackground(PaintEventArgs e, Rectangle rectangle)
		{
			if (this.RenderTransparent)
			{
				this.PaintTransparentBackground(e, rectangle);
			}
			if ((this.BackgroundImage != null) && !SystemInformation.HighContrast)
			{
				TextureBrush brush1 = new TextureBrush(this.BackgroundImage, WrapMode.Tile);
				try
				{
					Matrix matrix1 = brush1.Transform;
					matrix1.Translate((float) this.DisplayRectangle.X, (float) this.DisplayRectangle.Y);
					brush1.Transform = matrix1;
					e.Graphics.FillRectangle(brush1, rectangle);
					return;
				}
				finally
				{
					brush1.Dispose();
				}
			}
			Color color1 = this.BackColor;
			bool flag1 = false;
			if ((color1.A == 0xff))//&& (e.Graphics.GetHdc() != IntPtr.Zero)) //&& (this.BitsPerPixel > 8))
			{
				Win32.RECT rect1 = new Win32.RECT(rectangle.X, rectangle.Y, rectangle.Right, rectangle.Bottom);
				//Win32.WinAPI.FillRect (new HandleRef(e, e.HDC), ref rect1, new HandleRef(this, this.BackBrush));
				Win32.WinAPI.FillRect ((IntPtr)new HandleRef(e,this.Handle), ref rect1, (IntPtr)new HandleRef(this, this.BackBrush));
				flag1 = true;
			}
			if (!flag1 && (color1.A > 0))
			{
				if (color1.A == 0xff)
				{
					color1 = e.Graphics.GetNearestColor(color1);
				}
				using (Brush brush2 = new SolidBrush(color1))
				{
					e.Graphics.FillRectangle(brush2, rectangle);
				}
			}
		}

		internal protected void PaintTransparentBackground(PaintEventArgs e, Rectangle rectangle)
		{
			Graphics graphics1 = e.Graphics;
			Control control1 = this.Parent;
			if (control1 != null)
			{
				int num1;
				WinMethods.POINT point1 = new WinMethods.POINT();
				point1.y = num1 = 0;
				point1.x = num1;
				WinMethods.MapWindowPoints(new HandleRef(this, this.Handle), new HandleRef(control1, control1.Handle), point1, 1);
				rectangle.Offset(point1.x, point1.y);
				PaintEventArgs args1 = new PaintEventArgs(graphics1, rectangle);
				GraphicsState state1 = graphics1.Save();
				try
				{
					graphics1.TranslateTransform((float) -point1.x, (float) -point1.y);
					this.InvokePaintBackground(control1, args1);
					graphics1.Restore(state1);
					state1 = graphics1.Save();
					graphics1.TranslateTransform((float) -point1.x, (float) -point1.y);
					this.InvokePaint(control1, args1);
					return;
				}
				finally
				{
					graphics1.Restore(state1);
				}
			}
			graphics1.FillRectangle(SystemBrushes.Control, rectangle);
		}

		internal bool RenderTransparent
		{
			get
			{
				if (this.GetStyle(ControlStyles.SupportsTransparentBackColor))
				{
					return true;//(this.BackColor.A < 0xff);
				}
				return false;
			}
		}

		private IntPtr BackBrush
		{
			get
			{
				IntPtr ptr1;
				//				object obj1 = this.Properties.GetObject(Control.PropBackBrush);
				//				if (obj1 != null)
				//				{
				//					return (IntPtr) obj1;
				//				}
				Color color1 = this.BackColor;
				if ((this.Parent != null) && (this.Parent.BackColor == this.BackColor))
				{
					ptr1 = Win32.WinAPI.CreateSolidBrush(ColorTranslator.ToWin32(color1));
					//return this.Parent.BackBrush;
				}
				else if (ColorTranslator.ToOle(color1) < 0)
				{
					ptr1 = Win32.WinAPI.GetSysColorBrush(ColorTranslator.ToOle(color1) & 0xff);
					//this.SetState(0x200000, false);
				}
				else
				{
					ptr1 = Win32.WinAPI.CreateSolidBrush(ColorTranslator.ToWin32(color1));
					//this.SetState(0x200000, true);
				}
				//this.Properties.SetObject(Control.PropBackBrush, ptr1);
				return ptr1;
			}
		}
		#endregion

		#region Overrides

		protected override void OnPaint(PaintEventArgs pe)
		{
			Graphics g = pe.Graphics;
			Rectangle rectCR =this.ClientRectangle;
			Rectangle rect =new Rectangle (rectCR.X +(m_LineWidth/2),rectCR.Y +(m_LineWidth/2),rectCR.Width-m_LineWidth-1 ,rectCR.Height-m_LineWidth-1 );
            
       		//using(Pen p = new Pen(base.LayoutManager.Layout.BorderColorInternal ,(float)m_LineWidth))
			using(Pen p = new Pen(new SolidBrush(this.m_LineColor) ,(float)m_LineWidth))
			{
				using(Brush b= new SolidBrush(this.m_ShapeColor),bt=new SolidBrush (Color.Transparent ))
				{
		
					if(m_ShapeType==ShapeTypes.Rectangle )
					{
						g.FillRectangle (b,rect);
						g.DrawRectangle (p,rect);
					}
					else if(m_ShapeType== ShapeTypes.Ellipse )
					{
						//g.FillRectangle(bt,rectCR);
						g.FillEllipse  (b,rect);
						g.DrawEllipse (p,rect);
					}
					else if(m_ShapeType== ShapeTypes.Line)
					{
						g.FillRectangle(b,rect);
						g.DrawEllipse (p,rect);
					}
					else if(m_ShapeType== ShapeTypes.XpLine)
					{
						pe.Graphics.Clear(this.BackColor);
						Color m_StartColor =this.ShapeColor;//  Color.White;
						Color m_EndColor =this.m_LineColor;// Color.Black;

						using (LinearGradientBrush lineBrush = new LinearGradientBrush(new Rectangle(0,0,this.Width,this.Height), m_StartColor, m_EndColor,
								   this.m_LineDirection == LineDirection.Horizontal?LinearGradientMode.Horizontal:
								   LinearGradientMode.Vertical))
						{

							if (this.m_UseInterpolation)
							{
								ColorBlend cb = new ColorBlend(3);
								cb.Colors = new Color[3] {m_EndColor, m_StartColor, m_EndColor};
								cb.Positions = new float[3] {0.0f, 0.5f, 1.0f};
								lineBrush.InterpolationColors = cb;
							}
				
							using (GraphicsPath linePath = new GraphicsPath())
							{
								if (this.m_LineDirection == LineDirection.Horizontal)
									linePath.AddLine(0,(int)this.Height/2,this.Width,(int)this.Height/2);
								else
									linePath.AddLine((int)this.Width/2, 0, (int)this.Width/2, this.Bottom); 

								using (Pen pen = new Pen(lineBrush, this.m_LineWidth))
								{
									pe.Graphics.DrawPath(pen, linePath);
								}
							}
						}
					}
				}
			}

			base.OnPaint(pe);
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize (e);
			ReSetting();
		}

		public virtual void ReSetting()
		{
			if(m_ShapeType== ShapeTypes.Line || m_ShapeType== ShapeTypes.XpLine)
			{
				if(m_LineDirection==LineDirection.Horizontal)
				  this.Height=this.LineWidth;
				if(m_LineDirection==LineDirection.Vertical)
				  this.Width=this.LineWidth;
			}
		}

		#endregion

		#region Properties

		[Category("Style")]
		[DefaultValue(ShapeTypes.Rectangle)]
		public  ShapeTypes ShapeType 
		{
			get {return m_ShapeType ;}
			set
			{
				m_ShapeType= value;
				ReSetting();
				this.Invalidate ();
			}
		}

		[Category("Style")]
		[DefaultValue(ShapeLayers.Back)]
		[System.ComponentModel.RefreshProperties(RefreshProperties.All)]
		public  ShapeLayers ShapeLayer 
		{
			get {return m_ShapeLayer ;}
			set
			{
				if(m_ShapeLayer != value)
				{
					m_ShapeLayer= value;
					if (value== ShapeLayers.Back)
						this.SendToBack (); 
					else
						this.BringToFront();

					this.Invalidate ();
				}
			}
		}

//		[DefaultValue(typeof(System.Drawing.Color ),"Transparent")]
//		public  Color ShapeColor 
//		{
//			get {return m_BackColor ;}
//			set
//			{
//				m_BackColor= value;
//				this.Invalidate ();
//			}
//		}

		[Browsable(false),DefaultValue(typeof(System.Drawing.Color ),"Transparent")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden )]
		public override Color BackColor 
		{
			get {return Color.Transparent ;}
			set{base.BackColor= Color.Transparent;}
		}

		[Category("Appearance"),DefaultValue(typeof(Color),"DarkGray")]
		public Color LineColor
		{
			get {return m_LineColor;}
			set 
			{
				m_LineColor = value;
				this.Invalidate(true);
			}
		}

		[Category("Appearance"),DefaultValue(typeof(Color),"White")]
		public Color ShapeColor
		{
			get {return m_ShapeColor;}
			set 
			{
				m_ShapeColor = value;
				this.Invalidate(true);
			}
		}

		[Category("Appearance"),DefaultValue(1)]
		public int LineWidth
		{
			get {return m_LineWidth;}
			set 
			{   if(value>0)
					m_LineWidth = value;
				ReSetting();
				this.Invalidate(true);
			}
		}

		//xpLine
		[Category("Behavior"),DefaultValue(LineDirection.Horizontal)]
		public LineDirection LineDirection
		{
			get {return this.m_LineDirection;}
			set 
			{
				this.m_LineDirection = value;
				ReSetting();
				this.Invalidate(false);
			}
		}

		//xpLine
		[Category("Behavior"),DefaultValue(true)]
		public bool UseGradientInterpolation
		{
			get {return this.m_UseInterpolation;}
			set 
			{
				this.m_UseInterpolation = value;
				this.Invalidate(false);
			}
		}

		#endregion

	}
}
