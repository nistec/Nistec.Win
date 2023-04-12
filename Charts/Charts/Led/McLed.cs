
using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Nistec.Charts
{
	/// <summary>
	/// </summary>
    [ToolboxBitmap(typeof(McLed),"Images.Led.bmp")]
    public class McLed : System.Windows.Forms.UserControl
    {

        #region members

        private const int textWidth =40;

        private bool drawBorder = false;
        private int yellowVal = 7;
        private int redVal = 9;
        private int minVal = 0;
        private int maxVal = 10;
        private bool drawText = false;
        private bool horizental = false;
 
        int ledVal;								// meter value 
		int peakVal=0;							// Peak value
		int ledCount = 10;						// Number of LEDs
		int peakMsec = 1000;					// Peak Indicator time is 1 sec.
		
		protected Timer timer1;					// Timer to determine how long the peak indicator persists 

		// Array of LED colours			Unlit surround		Lit surround	Lit centre
		//													Unlit centre
		Color[] ledColours = new Color[]{Color.DarkRed, 	Color.Red,		Color.White,
                        				Color.DarkRed, 		Color.Red,		Color.White,
										Color.DarkRed,		Color.Red,		Color.White,
										Color.DarkGoldenrod,Color.Orange,	Color.White,
										Color.DarkGoldenrod,Color.Orange,	Color.White,
										Color.DarkGoldenrod,Color.Orange,	Color.White,
										Color.DarkGoldenrod,Color.Orange,	Color.White,
										Color.DarkGreen,	Color.Green,	Color.White,
                        				Color.DarkGreen,	Color.Green,	Color.White,
                        				Color.DarkGreen,	Color.Green,	Color.White,
                        				Color.DarkGreen,	Color.Green,	Color.White,
                        				Color.DarkGreen,	Color.Green,	Color.White,
                        				Color.DarkGreen,	Color.Green,	Color.White,
                        				Color.DarkGreen,	Color.Green,	Color.White,
                        				Color.DarkGreen,	Color.Green,	Color.White};

        #endregion

        #region Ctor

        public McLed()
		{
            this.Name = "McLed";
			this.Size = new System.Drawing.Size(20, 100);		// Default size for control
			timer1 = new Timer(); 
            timer1.Interval = peakMsec;							// Peak indicator time
            timer1.Enabled = false;
            timer1.Tick += new EventHandler(timer1_Tick);
        }
        #endregion

        #region override

        protected override void OnPaint(PaintEventArgs e)
		{
			Graphics g = e.Graphics;
            if (horizental)
                DrawLedsH(g);
            else
			   DrawLedsV(g);

			//DrawLeds(g);
			if(drawBorder)
			{
				DrawBorders(g);
			}
        }

        #endregion

        #region properties

        [Browsable(false),EditorBrowsable(EditorBrowsableState.Advanced)]
		internal int Volume
		// Determine how many LEDs to light - valid range 0 - 15
		{
			get
			{
				return ledVal;
			}
			set
			{
                int oldVal = Volume;

				if (value < 0)
				{
					ledVal = 0;
				}
				else if (value > ledCount)
				{
					ledVal = ledCount;
				}
				else
				{
					ledVal = value;
				}
				
				// New peak value
				if (ledVal > peakVal)
				{
					peakVal = ledVal;
					//timer1.Enabled = true;	
				}
                if (oldVal != value)
                {
                    this.Invalidate();
                }
			}
		}
		
        [Browsable(false)]
		public int ScaleValue
			// Determine how many LEDs to light - valid range 0 - 15
		{
			get
			{
				return (int)((float)ledVal *(float)(maxVal-minVal)/(float)ledCount);
			}
			set
			{
				// Do not allow negative value
                if (value >= maxVal)
                {
                    float val = (float)maxVal / (float)(maxVal - minVal) * (float)ledCount;
                    Volume = (int)val;
                }
				else if (value >= minVal)
				{
					float val = (float)value /(float)(maxVal-minVal)*(float)ledCount;
                    Volume = (int)val;
				}
			}
		}

        public bool ScaleHorizental
        {
            get
            {
                return horizental;
            }
            set
            {
                if (value != horizental)
                {
                    horizental = value;
                    
                    this.Invalidate();
                }
            }
        }
        public bool DrawText
        {
            get
            {
                return drawText;
            }
            set
            {
                if (value != drawText)
                {
                    drawText = value;
                    
                    this.Invalidate();
                }
            }
        }

        public int ScaleLedCount
		{
			get
			{
				return ledCount;
			}
			set
			{
				if(value > 0)
				{
					ledCount=value;
					this.Invalidate();
				}
			}
		}

        [Browsable(false)]
        public int ScalePeakVal
        {
            get
            {
                return peakVal;
            }

        }

	
		public bool DrawBorder
		{
			get
			{
				return drawBorder;
			}
			set
			{
				if(value != drawBorder)
				{
					drawBorder=value;
					this.Invalidate();
				}
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


        public int ScaleMin
		{
			get
			{
				return minVal;
			}
			set
			{
				if(value < maxVal && value >= 0)
				{
					minVal=value;
					this.Invalidate();
				}
			}
		}

        public int ScaleMax
		{
			get
			{
				return maxVal;
			}
			set
			{
				if(value > minVal)
				{
					maxVal=value;
					this.Invalidate();
				}
			}
        }

        public void SetAutoScale(int value, int max)
        {
            if (value > max || max <= 0)
                return;
            maxVal = max;
            redVal = (int)((float)maxVal * 0.9F);
            yellowVal = (int)((float)maxVal * 0.65F);
            ScaleValue = value;
            this.Invalidate();
        }
        #endregion

        #region methods

        private float GetRedTop()
		{
			return ledCount * (float)(redVal/(float)(maxVal-minVal));
		}

		private float GetYellowTop()
		{
			return ledCount * (float)(yellowVal/(float)(maxVal-minVal));
		}


		private void timer1_Tick(object sender, EventArgs e)
		{
			//timer1.Enabled = false;					
			//peakVal = 0;							
			//this.Invalidate();
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged (e);
			this.Invalidate();
		}

        private void DrawLedsV(Graphics g)
        {
            int textOffset = drawText ? textWidth : 0;

            // Rectangle values for each individual LED - fit them nicely inside the border
            int ledLeft = textOffset + this.ClientRectangle.Left + 3;
            int ledTop = this.ClientRectangle.Top + 3;
            int ledWidth = (textOffset + 6) > this.ClientRectangle.Width ? this.ClientRectangle.Width : this.ClientRectangle.Width - (textOffset + 6);
            int ledHeight = this.ClientRectangle.Height / ledCount - 1;

            float redTop = GetRedTop();//  ledCount*0.9f;
            float goldTop = GetYellowTop();// ledCount*0.7f;


            // Create the LED rectangle
            Rectangle ledRect = new Rectangle(ledLeft, ledTop, ledWidth, ledHeight);

            GraphicsPath gp = new GraphicsPath();					// Create Graphics Path
            gp.AddRectangle(ledRect);								// Add the rectangle
            PathGradientBrush pgb = new PathGradientBrush(gp);		// brush for shiny LEDs

            Color activeColor = BackColor;

            // Two ints in the FOR LOOP, because the graphics are offset from the top, but the LED
            // values start from the bottom...
            for (int i = 0, j = ledCount; i < ledCount; i++, j--)
            {

                activeColor = GetActiveColor(j, (int)redTop, (int)goldTop);

                // Use a matrix to move the LED graphics down according to the value of i
                Matrix mx = new Matrix();
                mx.Translate(0, i * (ledHeight + 1));
                g.Transform = mx;

                DrawLed(g, pgb, j, ledRect, activeColor);

                if (drawText && j % 2 == 0)
                {
                    DrawLedText(g, j, ledRect);
                }
            }
            CleanGraphics(g);
            gp.Dispose();
        }


        private void DrawLedsH(Graphics g)
        {

            // Rectangle values for each individual LED - fit them nicely inside the border
            int ledLeft = this.ClientRectangle.Left + 3;
            int ledTop = this.ClientRectangle.Top + 3;
            int ledWidth = this.ClientRectangle.Width / ledCount - 1;
            int ledHeight = this.ClientRectangle.Height - 6;// / ledCount - 1;

            float redTop = GetRedTop();//  ledCount*0.9f;
            float goldTop = GetYellowTop();// ledCount*0.7f;


            // Create the LED rectangle
            Rectangle ledRect = new Rectangle(ledLeft, ledTop, ledWidth, ledHeight);

            GraphicsPath gp = new GraphicsPath();					// Create Graphics Path
            gp.AddRectangle(ledRect);								// Add the rectangle
            PathGradientBrush pgb = new PathGradientBrush(gp);		// brush for shiny LEDs

            Color activeColor = BackColor;

            for (int j = 0; j < ledCount; j++)
            {
                activeColor = GetActiveColor(j, (int)redTop, (int)goldTop);

                // Use a matrix to move the LED graphics down according to the value of i
                Matrix mx = new Matrix();
                mx.Translate(j * (ledWidth + 1), 0);
                g.Transform = mx;

                DrawLed(g, pgb, j, ledRect, activeColor);

                //if (drawText && j % 2 == 0)
                //{
                //    DrawLedText(g, j, ledRect);
                //}

            }
            CleanGraphics(g);
            gp.Dispose();
        }

        private Color GetActiveColor(int j, int redTop, int goldTop)
        {
            if (j > (int)redTop)
            {
                return Color.Red;
            }
            else if (j > (int)goldTop)
            {
                return Color.Orange;
            }
            else
            {
                return Color.LimeGreen;
            }
        }

        private void DrawLed(Graphics g, PathGradientBrush pgb, int index, Rectangle ledRect, Color activeColor)
        {
            if (ledVal >= index )
            {
                pgb.SurroundColors = new Color[] { activeColor };
            }
            else
            {
                pgb.SurroundColors = new Color[] { BackColor };
            }
            pgb.CenterColor = Color.White;
            
            // Light LED fom the centre.
            pgb.CenterPoint = new PointF(ledRect.X + ledRect.Width / 2, ledRect.Y + ledRect.Height / 2);
            g.FillRectangle(pgb, ledRect);
            if (index == peakVal)
            {
                g.DrawRectangle(new Pen(Brushes.Black, 2), ledRect.X, ledRect.Y, ledRect.Width - 1, ledRect.Height - 1);
            }
            else
            {
                g.DrawRectangle(new Pen(activeColor, 1), ledRect.X, ledRect.Y, ledRect.Width - 1, ledRect.Height - 1);
            }

        }

        private void DrawLedText(Graphics g, int index, Rectangle ledRect)
        {
            float v = (float)index / ledCount * (float)(maxVal - minVal);
            int iv = (int)v;
            Rectangle textRect = new Rectangle(this.ClientRectangle.Left + 3, ledRect.Y, textWidth, Font.Height);

            g.DrawString(iv.ToString(), Font, new SolidBrush(ForeColor), textRect);

        }

        private void CleanGraphics(Graphics g)
        {
            // Translate back to original position to draw the border
            Matrix mx1 = new Matrix();
            mx1.Translate(0, 0);
            g.Transform = mx1;
        }

        //not used
		private void DrawLeds(Graphics g)
		{
			// Rectangle values for each individual LED - fit them nicely inside the border
			int ledLeft = this.ClientRectangle.Left + 3;
			int ledTop = this.ClientRectangle.Top + 3;
			int ledWidth = this.ClientRectangle.Width - 6;
			int ledHeight = this.ClientRectangle.Height / ledCount -2 ;	
			
			// Create the LED rectangle
			Rectangle ledRect = new Rectangle(ledLeft, ledTop, ledWidth, ledHeight);
			
			GraphicsPath gp = new GraphicsPath();					// Create Graphics Path
			gp.AddRectangle(ledRect);								// Add the rectangle
			PathGradientBrush pgb = new PathGradientBrush(gp);		// Nice brush for shiny LEDs
			
			// Two ints in the FOR LOOP, because the graphics are offset from the top, but the LED
			// values start from the bottom...
			for(int i=0, j=ledCount; i<ledCount; i++, j--)
      		{
      			// Light the LED if it's under current value, or if it's the peak value.
				if (((j <= ledVal) | (j == peakVal)))
				{
					pgb.CenterColor=ledColours[i*3+2];
					pgb.SurroundColors=new Color[]{ledColours[i*3+1]};
				}
				// Otherwise, don't light it.
				else
				{
					pgb.CenterColor=ledColours[i*3+1];
					pgb.SurroundColors=new Color[]{ledColours[i*3]};
				}
        		
        		// Light LED fom the centre.
        		pgb.CenterPoint=new PointF(ledRect.X + ledRect.Width/2, ledRect.Y + ledRect.Height/2);
      			
      			// Use a matrix to move the LED graphics down according to the value of i
      			Matrix mx = new Matrix();
      			mx.Translate(0, i * (ledHeight + 1));
        		g.Transform=mx;
        		g.FillRectangle(pgb, ledRect);
      		}

			// Translate back to original position to draw the border
      		Matrix mx1 = new Matrix();
			mx1.Translate(0, 0);
			g.Transform = mx1;

			gp.Dispose(); 
		}
		
		private void DrawBorders(Graphics g)
		{
			int PenWidth = (int)Pens.White.Width;
			
			// Draw the outer 3D border round the control
			//
			g.DrawLine(Pens.White, 
				new Point(this.ClientRectangle.Left, this.ClientRectangle.Top),
				new Point(this.ClientRectangle.Width - PenWidth, this.ClientRectangle.Top)); //Top
			g.DrawLine(Pens.White,
				new Point(this.ClientRectangle.Left, this.ClientRectangle.Top), 
				new Point(this.ClientRectangle.Left, this.ClientRectangle.Height - PenWidth)); //Left
			g.DrawLine(Pens.DarkGray,
				new Point(this.ClientRectangle.Left, this.ClientRectangle.Height - PenWidth), 
				new Point(this.ClientRectangle.Width - PenWidth, this.ClientRectangle.Height - PenWidth)); //Bottom
			g.DrawLine(Pens.DarkGray,
				new Point(this.ClientRectangle.Width - PenWidth, this.ClientRectangle.Top), 
				new Point(this.ClientRectangle.Width - PenWidth, this.ClientRectangle.Height - PenWidth)); //Right
			
			
			// Draw the inner 3D border round the LEDs
			//
			
			// Set the size to fit nicely inside the control.
			int ledBorderLeft = this.ClientRectangle.Left + 2;
			int ledBorderTop = this.ClientRectangle.Top + 2;
			int ledBorderWidth = this.ClientRectangle.Width - 3;
			int ledBorderHeight = this.ClientRectangle.Height - 3;
			
			// Draw the border
			g.DrawLine(Pens.DarkGray, new Point(ledBorderLeft, ledBorderTop), new Point(ledBorderWidth, ledBorderTop)); //Top
			g.DrawLine(Pens.DarkGray, new Point(ledBorderLeft, ledBorderTop), new Point(ledBorderLeft, ledBorderHeight)); //Left
			g.DrawLine(Pens.White, new Point(ledBorderLeft, ledBorderHeight), new Point(ledBorderWidth, ledBorderHeight)); //Bottom
			g.DrawLine(Pens.White, new Point(ledBorderWidth, ledBorderTop), new Point(ledBorderWidth, ledBorderHeight)); //Right
			// Extra line to overwrite any LED which shows between the inner and outer border.
			g.DrawLine(Pens.LightGray, new Point(ledBorderLeft, ledBorderHeight + 1), new Point(ledBorderWidth, ledBorderHeight +1));

        }
        #endregion
    }
}
