using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Windows.Forms.Design;
using Nistec.Charts.Utils;

namespace Nistec.Charts
{
	public enum MeterStyle
	{
		Flat,
		LedBorder,
		LedFill
	}

		
	/// <summary>
	/// McMeter
	/// </summary>
    [ToolboxBitmap(typeof(McMeter), "Images.Meter.bmp")]
    public class McMeter : System.Windows.Forms.Control 
	{
	
		#region Fonts, Brushes, Pens

		// font and brush used to draw the text for the scale on the 
		// face of the meter
		Font boldFont;
		SolidBrush numeralBrush;
		RectangleF InsideRect=RectangleF.Empty;
		
		// brushes and pens to draw the face of the meter
		LinearGradientBrush outlineBrush;
		LinearGradientBrush zoneBrush;
		Pen outlinePen;
		Pen meterlinePen;
		#endregion

		#region members
		const float PI=3.141592654F;

		private int scalePieWidth =30;
		private int scaleLineWidth =30;
		private int scaleOffset =180;
		private int scaleInterval =20;
		private int scaleMax =180;
		private int scaleMin =0;
		//private Font scaleFont;
		private int yellowVal=7;
		private int redVal=9;
		private MeterStyle pieStyle=MeterStyle.LedFill;


		// angle used to draw the meter line that displays the on the meter
		int angle;

		// offset is an offset to adjust for the location of the text
		// on the meter face. 
		int offset;

		// x and y are the starting points
		float x , y;

		#endregion

		#region Face Colors
		
		// number color 

		//Color numberColor;
		Color meterlineColor;

        private LinearColors borderColors;
        private LinearColors faceColors;

		
		#endregion

		#region Ctor
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public McMeter() 
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			
			// This is additional buffering to stop flickering when
			// the control is redrawn

			this.SetStyle(System.Windows.Forms.ControlStyles.ResizeRedraw, true);
			this.SetStyle(System.Windows.Forms.ControlStyles.DoubleBuffer, true);
			this.SetStyle(System.Windows.Forms.ControlStyles.AllPaintingInWmPaint, true);

			// initialization after the InitComponent call

			// colors
			//numberColor = Color.Chartreuse;
			borderColors = new LinearColors();
            faceColors = new LinearColors();
			meterlineColor = Color.Chartreuse;
			//Brushes and Pens
		
			//scaleFont = new Font("ArialBold", 12F, Font.Style );
	
			boldFont = new Font("ArialBold", 12F, Font.Style | FontStyle.Bold);
			numeralBrush = new SolidBrush(ForeColor);// numberColor);
			outlineBrush = new LinearGradientBrush(ClientRectangle, borderColors.ColorStart, borderColors.ColorEnd, 45, true);
            zoneBrush = new LinearGradientBrush(ClientRectangle, faceColors.ColorStart, faceColors.ColorEnd, faceColors.ColorAngle, true);
			outlinePen = new Pen(outlineBrush, 32);
			meterlinePen = new Pen(meterlineColor, 3);
			x=0;
			y=0;
			offset = 0; 
			this.angle = 0;
		
			
			// make sure control is redrawn when it's resized
			SetStyle(System.Windows.Forms.ControlStyles.ResizeRedraw, true);
            this.borderColors.PropertyChanged += new LinearColors.PropertyChangedEventHandler(this.MeterBorderColors_PropertyChanged);
            this.faceColors.PropertyChanged += new LinearColors.PropertyChangedEventHandler(this.MeterFaceColors_PropertyChanged);


		}



		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing ) 
		{
			if( disposing ) 
			{
				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
			numeralBrush.Dispose();
			outlineBrush.Dispose();
			outlinePen.Dispose();
			meterlinePen.Dispose();

		}
		#endregion

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() 
		{
			// 
			// Meter
			// 
			this.Name = "Meter";
			this.ClientSize = new System.Drawing.Size(448, 232);
			this.Location = new System.Drawing.Point(32, 16);
			
		
		}
		#endregion

		#region Drawing Methods
		
		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged (e);
			this.scaleOffset=this.Width-this.Height-20;//-this.scaleFont.Height;
		}

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs Pe) 
		{
			

			Graphics g = Pe.Graphics;
			DrawPie(g);
			FillPie(g);
			
			this.offset= -30; //-30 is the offset for when the line is pointing to
			// zero or 180. 
		
			DrawLine(g, angle, offset);
		
			float ofs=((float)this.Width*(float)this.Height)/300f;
            if (scaleMin > 0)
            {
                this.offset = -30;
            }

			for ( int a = scaleMin; a <= scaleMax; a += scaleInterval) 
			{
                float ang = (((float)(a - scaleMin) / (float)(scaleMax - scaleMin))) * 180;
				DrawScale(g, (int)ang,a.ToString(),scaleOffset);
			}
	
		}


		private void DrawLine(Graphics g, int angle, int offset) 
		{	
			Matrix m = new Matrix();
			PointF center = 
				new PointF(this.ClientRectangle.Width/2 ,this.ClientRectangle.Bottom - scalePieWidth);// (scaleFont.Height+8) );
			float radius=3;
			g.DrawEllipse( meterlinePen, center.X-radius, center.Y-radius, radius*2 , radius*2 );
			
			m.RotateAt( angle+180, center );
			m.Translate( center.X, center.Y );
			g.Transform = m;
			g.DrawLine( meterlinePen, 0, 0, this.ClientRectangle.Width/2 + offset , 0 );
			g.DrawLine( new Pen(Brushes.White,1), 0, 0, this.ClientRectangle.Width/2 + offset , 0 );
			//g.FillEllipse( meterlinePen.Brush, this.ClientRectangle.Width/2 + offset-(radius*2), -radius, radius*2 , radius*2 );
			g.FillRectangle( meterlinePen.Brush, this.ClientRectangle.Width/2 + offset-(radius*3), -radius, radius*2 , radius*2 );
		
		}
		
		//not used
		private void DrawPolygon(Graphics g,PointF center,float fDepth, float fLength, Color color, float fRadians)
		{

			PointF A=new PointF( (float)(center.X+ fDepth*2*System.Math.Sin(fRadians+PI/2)), 
				(float)(center.Y - fDepth*2*System.Math.Cos(fRadians+PI/2)) );
			PointF B=new PointF( (float)(center.X+ fDepth*2*System.Math.Sin(fRadians-PI/2)),
				(float)(center.Y - fDepth*2*System.Math.Cos(fRadians-PI/2)) );
			PointF C=new PointF( (float)(center.X+ fLength*System.Math.Sin(fRadians)), 
				(float) (center.Y - fLength*System.Math.Cos(fRadians)) );
			PointF D=new PointF( (float)(center.X- fDepth*4*System.Math.Sin(fRadians)), 
				(float)(center.Y + fDepth*4*System.Math.Cos(fRadians) ));
			PointF[] points={A,D,B,C};
			g.FillPolygon( new SolidBrush(color), points );
		}


		private void DrawPie(Graphics g) 
		{
			
			float inflate=scalePieWidth/2;
            InsideRect = new RectangleF(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - (inflate + 4), ClientRectangle.Height - (inflate + 4));
		
			g.SmoothingMode = SmoothingMode.AntiAlias; 
			//g.DrawPie(new Pen(outlineBrush, scalePieWidth), x + this.ClientRectangle.Width/16 ,y + this.ClientRectangle.Height/16 , (this.ClientRectangle.Width -this.ClientRectangle.Width/8),((this.ClientRectangle.Height * 2)-this.ClientRectangle.Height/4), 0, -180);
			
            x=inflate/2 + 2;
			y=inflate/2 + 2;
			switch(pieStyle)
			{
				case MeterStyle.Flat:
					g.DrawPie(new Pen(outlineBrush, scalePieWidth), x + InsideRect.Width/16 ,y + InsideRect.Height/16 , (InsideRect.Width -InsideRect.Width/8),((InsideRect.Height * 2)-InsideRect.Height/4), 0, -180);
					g.FillPie(zoneBrush, x + InsideRect.Width/16 ,y + InsideRect.Height/16 , (InsideRect.Width -InsideRect.Width/8),((InsideRect.Height * 2)-InsideRect.Height/4), 0, -180);
					break;
				case MeterStyle.LedBorder:
					DrawLedPie( g);
					break;
				case MeterStyle.LedFill:
					DrawLedPieWithFill(g);			
					break;
			}
		}
		private float GetYellowTop()
		{
			return 180 * (float)(redVal/(float)(scaleMax-scaleMin));
		}
		private float GetGreenTop()
		{
			return 180 * (float)(yellowVal/(float)(scaleMax-scaleMin));
			//return 180 * (float)((scaleMax-(scaleMax-yellowVal))/(float)(scaleMax-scaleMin));
		}


		private void DrawLedPie(Graphics g) 
		{

            //g.DrawPie(new Pen(new SolidBrush(Color.Green), scalePieWidth + 4), x + InsideRect.Width / 16, y + InsideRect.Height / 16, (InsideRect.Width - InsideRect.Width / 8), ((InsideRect.Height * 2) - InsideRect.Height / 4), 180, 180);

            g.DrawPie(new Pen(new SolidBrush(Color.Red), scalePieWidth), x + InsideRect.Width / 16, y + InsideRect.Height / 16, (InsideRect.Width - InsideRect.Width / 8), ((InsideRect.Height * 2) - InsideRect.Height / 4), 180, 180);
            g.DrawPie(new Pen(new SolidBrush(Color.Yellow), scalePieWidth), x + InsideRect.Width / 16, y + InsideRect.Height / 16, (InsideRect.Width - InsideRect.Width / 8), ((InsideRect.Height * 2) - InsideRect.Height / 4), 180, GetYellowTop());
            g.DrawPie(new Pen(new SolidBrush(Color.LightGreen), scalePieWidth), x + InsideRect.Width / 16, y + InsideRect.Height / 16, (InsideRect.Width - InsideRect.Width / 8), ((InsideRect.Height * 2) - InsideRect.Height / 4), 180, GetGreenTop());
            //g.DrawPie(new Pen(new SolidBrush(Color.AliceBlue), 2), x - 4 + InsideRect.Width / 16, y - 4 + InsideRect.Height / 16, (InsideRect.Width + 8 - InsideRect.Width / 8), 16 + ((InsideRect.Height * 2) - InsideRect.Height / 4), 0, -180);
         
             //clean bottom border
            g.FillRectangle(new SolidBrush(BackColor), ClientRectangle.X, ClientRectangle.Bottom - (scalePieWidth/1.5f), ClientRectangle.Width, scalePieWidth);
   
            
            //fill inside
            g.FillPie(zoneBrush, x + InsideRect.Width / 16, y + InsideRect.Height / 16, (InsideRect.Width - InsideRect.Width / 8), ((InsideRect.Height * 2) - InsideRect.Height / 4), 0, -180);
       
            //Pie border inner
            g.DrawPie(new Pen(new SolidBrush(Color.Green), 1), x + InsideRect.Width / 16 , y + InsideRect.Height / 16 , (InsideRect.Width - InsideRect.Width / 8), ((InsideRect.Height * 2) - InsideRect.Height / 4) , 180, 180);

            int ofs = scalePieWidth / 2;

            //Pie border outer
            g.DrawPie(new Pen(new SolidBrush(Color.Green), 1), x + InsideRect.Width / 16 - ofs, y + InsideRect.Height / 16 - ofs, (InsideRect.Width - InsideRect.Width / 8) + ofs * 2, ((InsideRect.Height * 2) - InsideRect.Height / 4) + ofs * 2, 180, 180);

            //draw  bottom border
            //g.DrawLine(new Pen(new SolidBrush(Color.Green), 1), x + InsideRect.X, InsideRect.Bottom - 1, x + InsideRect.Width, InsideRect.Bottom - 1);
        }


		private void DrawLedPieWithFill(Graphics g) 
		{
            //old
            //g.DrawPie(new Pen(new SolidBrush(Color.AliceBlue), 2), x-4 + InsideRect.Width/16 ,y-4 + InsideRect.Height/16 , (InsideRect.Width+8 -InsideRect.Width/8),16+((InsideRect.Height * 2)-InsideRect.Height/4), 0, -180);

			//draw and fill red pie
			g.DrawPie(new Pen(new SolidBrush(Color.Red), scalePieWidth), x + InsideRect.Width/16 ,y + InsideRect.Height/16 , (InsideRect.Width -InsideRect.Width/8),((InsideRect.Height * 2)-InsideRect.Height/4), 180, 180);
			g.FillPie(new SolidBrush(Color.Red), x + InsideRect.Width/16 ,y + InsideRect.Height/16 , (InsideRect.Width -InsideRect.Width/8),((InsideRect.Height * 2)-InsideRect.Height/4), 180, 180);
            //draw and fill Yellow pie
            g.DrawPie(new Pen(new SolidBrush(Color.Yellow), scalePieWidth), x + InsideRect.Width / 16, y + InsideRect.Height / 16, (InsideRect.Width - InsideRect.Width / 8), ((InsideRect.Height * 2) - InsideRect.Height / 4), 180, GetYellowTop());
			g.FillPie(new SolidBrush(Color.Yellow), x + InsideRect.Width/16 ,y + InsideRect.Height/16 , (InsideRect.Width -InsideRect.Width/8),((InsideRect.Height * 2)-InsideRect.Height/4), 180, GetYellowTop());
            //draw and fill Green pie
            g.DrawPie(new Pen(new SolidBrush(Color.LightGreen), scalePieWidth), x + InsideRect.Width / 16, y + InsideRect.Height / 16, (InsideRect.Width - InsideRect.Width / 8), ((InsideRect.Height * 2) - InsideRect.Height / 4), 180, GetGreenTop());
			g.FillPie(new SolidBrush(Color.LightGreen), x + InsideRect.Width/16 ,y + InsideRect.Height/16 , (InsideRect.Width -InsideRect.Width/8),((InsideRect.Height * 2)-InsideRect.Height/4), 180, GetGreenTop());

            int ofs = scalePieWidth / 2;

            //Pie border outer
            g.DrawPie(new Pen(new SolidBrush(Color.Green), 1), x + InsideRect.Width / 16 - ofs, y + InsideRect.Height / 16 - ofs, (InsideRect.Width - InsideRect.Width / 8) + ofs * 2, ((InsideRect.Height * 2) - InsideRect.Height / 4) + ofs * 2, 180, 180);
 
            //clean bottom border
            g.FillRectangle(new SolidBrush(BackColor),ClientRectangle.X,InsideRect.Bottom-1,ClientRectangle.Width,scalePieWidth+1);
    
            //draw  bottom border
            //g.DrawLine(new Pen(new SolidBrush(Color.Green), 1), x + InsideRect.X, InsideRect.Bottom - 1, x + InsideRect.Width, InsideRect.Bottom - 1);
        }


		private void FillPie(Graphics g) 
		{
			//g.FillPie(zoneBrush, x + InsideRect.Width/16 ,y + InsideRect.Height/16 , (InsideRect.Width -InsideRect.Width/8),((InsideRect.Height * 2)-InsideRect.Height/4), 0, -180);
			//g.FillPie(zoneBrush, x + this.ClientRectangle.Width/16 ,y + this.ClientRectangle.Height/16 , (this.ClientRectangle.Width -this.ClientRectangle.Width/8),((this.ClientRectangle.Height * 2)-this.ClientRectangle.Height/4), 0, -180);
		}
	
		private void DrawScale(Graphics g,int angle,string text, int offset) 
		{
			Matrix 	m = new Matrix();
			PointF center = 
				new PointF(this.ClientRectangle.Width/2 ,this.ClientRectangle.Bottom-(Font.Height+4) );

			StringFormat format = new StringFormat();
			format.Alignment= StringAlignment.Center;
			format.FormatFlags= StringFormatFlags.NoClip | StringFormatFlags.NoWrap | StringFormatFlags.NoFontFallback;
			SizeF        
				textSize = 
				new SizeF(g.MeasureString(text, Font));
			RectangleF   
				textRect = 
				new RectangleF(new PointF(center.X - offset - textSize.Width/2, center.Y-textSize.Height/2), 
				textSize);

			m.RotateAt(angle, center );
			g.Transform = m;
			g.SmoothingMode = SmoothingMode.AntiAlias;
			g.DrawString( text, Font, numeralBrush, textRect, format );		
		
		}

		#endregion

		#region Advandce Property

			

		public MeterStyle MeterStyle 
		{
			get 
			{
				return pieStyle;
			}
			set 
			{
				if(pieStyle!=value)
				{
					pieStyle= value;
				}
				Invalidate();
			}
		}

		public int ScaleValue 
		{
			get 
			{
				return angle * (scaleMax-scaleMin)/180;
			}
			set 
			{
                if (value > scaleMax)
                    value = scaleMax;

				if((scaleMax-scaleMin) >0)
				{
					angle= value*180 / (scaleMax-scaleMin);
				}
				Invalidate();
			}
		}

	
		public int ScaleMeterLineWidth 
		{
			get 
			{
				return this.scaleLineWidth;
			}
			set 
			{
				if(scaleLineWidth != value)
				{
					this.scaleLineWidth = value;
					this.meterlinePen=new Pen(new SolidBrush(meterlineColor),value);
					Invalidate();
				}
			}
		}

	
		public int ScalePieWidth 
		{
			get 
			{
				return this.scalePieWidth;
			}
			set 
			{
                if (scalePieWidth != value)
				{
					this.scalePieWidth = value;
					this.outlinePen=new Pen(outlineBrush,value);
					Invalidate();
				}
			}
		}
	
		internal int ScaleOffset
		{
			get 
			{
				return this.scaleOffset;
				//return (this.Width-this.Height/2)/2; // this.scaleOffset;
			}
			set 
			{
				this.scaleOffset = value;
				Invalidate();
			}
		}

	
		public int ScaleInterval 
		{
			get 
			{
				return this.scaleInterval;
			}
			set 
			{
				this.scaleInterval = value;
				Invalidate();
			}
		}

	
		public int ScaleMax 
		{
			get 
			{
				return this.scaleMax;
			}
			set 
			{
				this.scaleMax = value;
				Invalidate();
			}
		}
	
		public int ScaleMin 
		{
			get 
			{
				return this.scaleMin;
			}
			set 
			{
				this.scaleMin = value;
				Invalidate();
			}
		}

	
		public int ScaleLedYellow
		{
			get
			{
				return yellowVal;
			}
			set
			{
				if(value > 0)
				{
					yellowVal=value;
					this.Invalidate();
				}
			}
		}

	
		public int ScaleLedRed
		{
			get
			{
				return redVal;
			}
			set
			{
				if(value > 0)
				{
					redVal=value;
					this.Invalidate();
				}
			}
		}

        public void SetAutoScale(int value, int max, int interval )
        {
            if (value < 0 || value > max || interval <= 0 || max<=0)
                return;
            scaleMax = max;
            scaleInterval = interval;
            redVal = (int)((float)scaleMax * 0.9F);
            yellowVal = (int)((float)scaleMax * 0.65F);
            ScaleValue = value;
            this.Invalidate();
        }

		#endregion

		#region Meter Properties

		[Browsable(false)]
			// This angle is used to draw the meter needle that points to the current value

		public int Angle 
		{
			get 
			{
				return this.angle;
			}
			set 
			{
				this.angle = value;
				Invalidate();
			}
		}

		[
		Description("Color of the needle on the meter"),
		CategoryAttribute("Appearance"),
				
		]
		public Color MeterlineColor 
		{
			get 
			{
				return meterlineColor;
			}
			set 
			{

				meterlineColor = value;
				meterlinePen.Color = meterlineColor;
				Invalidate();
			}
		}
        
		[
		Description("StartColor and  End Color for the Outline of the Meter"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		CategoryAttribute("Appearance")
				
		]
		public LinearColors BorderColors 
		{
			get 
			{
				return this.borderColors;
			}
			set 
			{
				this.borderColors = value;
			
			}
		}
		
		private void MeterBorderColors_PropertyChanged(string colorChanged) 
		{
			UpdateBorderColors();
			Invalidate();	
		}

		private void UpdateBorderColors() 
		{
			outlineBrush.Dispose();
			outlineBrush = new LinearGradientBrush(ClientRectangle, borderColors.ColorStart, borderColors.ColorEnd, 45, true);
		
		}

		[
		Description("StartColor and End Color for the backgound of the Meter. ColorAngle is the Angle at which the colors blend."),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		CategoryAttribute("Appearance")
			
		]

        public LinearColors BackgoundColors 
		{
			get 
			{
				return this.faceColors;
			}
			set 
			{
				this.faceColors = value;
			
			}
		}
		
		private void MeterFaceColors_PropertyChanged(string colorChanged) 
		{
			UpdateFaceColors();
			Invalidate();	
		}

		private void UpdateFaceColors() 
		{
			zoneBrush.Dispose();
            zoneBrush = new LinearGradientBrush(ClientRectangle, faceColors.ColorStart, faceColors.ColorEnd, faceColors.ColorAngle, true);
		
		}
		#endregion

		#region Designer Attributes that are not browsable

		[Browsable(false)]
		public override RightToLeft RightToLeft 
		{
			get 
			{
				return base.RightToLeft;
			}
			set 
			{
				base.RightToLeft = value;
			}
		}
		
		[Browsable(false)]
		public override Image BackgroundImage 
		{
			get 
			{
				return base.BackgroundImage;
			}
			set 
			{
				base.BackgroundImage = value;
			}
		}
		
		//[Browsable(false)]
		public override Font Font 
		{
			get 
			{
				return base.Font;
			}
			set 
			{
				base.Font = value;
			}
		}

		//[Browsable(false)]
		public override Color ForeColor 
		{
			get 
			{
				return base.ForeColor;
			}
			set 
			{
				base.ForeColor = value;
                numeralBrush.Color = value;
			}
		}

		[Browsable(false)]
		public override Cursor Cursor 
		{
			get 
			{
				return base.Cursor;
			}
			set 
			{
				base.Cursor = value;
			}
		}

		[Browsable(false)]
		public override string Text 
		{
			get 
			{
				return base.Text;
			}
			set 
			{
				base.Text = value;
			}
		}


		

		#endregion


	}
}
