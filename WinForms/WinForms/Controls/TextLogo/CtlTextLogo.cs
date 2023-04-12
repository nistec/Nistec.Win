using System;
using System.Windows.Forms;
using System.ComponentModel; 
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace mControl.WinCtl.Controls
{	
	[System.ComponentModel.ToolboxItem(false)]
	[ToolboxBitmap (typeof(CtlTextLogo),"Toolbox.TextLogo.bmp")]
	public class CtlTextLogo : System.Windows.Forms.Control  
	{

		#region Members

		private Color _AnimateColor=Color.White ;
		private int _GradientShiftMin=-50;
		private int _GradientShiftStart=100;
		private int _GradientShiftEnd=200;
		//private string _TextLogo="mControl";
		private int intCurrentGradientShift = 10;
		private int intGradiantStep = 5;

		#endregion

		#region Constructor
		public CtlTextLogo() 
		{
			//This call is required by the Windows Form Designer.
			InitializeComponent();
			_AnimateColor=this.BackColor; 
		}


		//Form overrides dispose to clean up the component list.
		protected override void Dispose(bool disposing) 
		{
			if (disposing) 
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		//Required by the Windows Form Designer
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Timer tmrAnimation;

		private void InitializeComponent() 
		{
			this.components = new System.ComponentModel.Container();
			this.tmrAnimation = new System.Windows.Forms.Timer(this.components);
			// 
			// tmrAnimation
			// 
			this.tmrAnimation.Enabled = true;
			this.tmrAnimation.Interval = 200;
			this.tmrAnimation.Tick += new System.EventHandler(this.TimerOnTick);
			// 
			// ctlLogo
			// 
			this.BackColor = System.Drawing.SystemColors.Window;
			this.Font = new Font("Microsoft Sans Serif", 24, FontStyle.Bold, GraphicsUnit.Point);
			this.Name = "frmLogo";
			this.Size = new System.Drawing.Size(570, 374);
			this.Text = "mControl";

		}

		#endregion

		#region Property
 
		[DefaultValue(typeof(Color),"Window")]
		public Color AnimateColor
		{
			get{return _AnimateColor;}
			set{_AnimateColor=value;}
		}

		[DefaultValue(100)]
		public int GradientShiftStart
		{
			get{return _GradientShiftStart;}
			set{_GradientShiftStart=value;}
		}

		[DefaultValue(200)]
		public int GradientShiftEnd
		{
			get{return _GradientShiftEnd;}
			set{_GradientShiftEnd=value;}
		}

		[DefaultValue(-50)]
		public int GradientShiftMin
		{
			get{return _GradientShiftMin;}
			set{_GradientShiftMin=value;}
		}

		#endregion

		#region Timer
		// This subroutine handles the Tick event for the Timer. 
		// This is where the animation takes place.
		protected void TimerOnTick(object obj ,EventArgs ea ) 
		{

		
			// Obtain the Graphics object exposed by the Form.
			Graphics grfx = CreateGraphics();
			// Set the font type, text, and determine its size.

			//Font font = new Font("Microsoft Sans Serif", _FontSize, 
			//    FontStyle.Bold, GraphicsUnit.Point);

			string strText  = Text;
			SizeF sizfText = new SizeF(grfx.MeasureString(strText, this.Font ));

			// Set the point at which the text will be drawn: centered
			// in the client area.

			PointF ptfTextStart = new PointF(Convert.ToSingle(ClientSize.Width - sizfText.Width) / 2,
				Convert.ToSingle(ClientSize.Height - sizfText.Height) / 2);

			// Set the gradient start and end point, the latter being adjusted
			// by a changing value to give the animation affect.
			PointF ptfGradientStart = new PointF(0, 0);
			PointF ptfGradientEnd = new PointF(intCurrentGradientShift, _GradientShiftStart );
			// Instantiate the brush used for drawing the text.
			LinearGradientBrush grBrush = new LinearGradientBrush(ptfGradientStart,
				ptfGradientEnd, Color.Blue, _AnimateColor);
			// Draw the text centered on the client area.
			grfx.DrawString(strText, this.Font , grBrush, ptfTextStart);
			grfx.Dispose();
			// Shift the gradient, reversing it when it gets to a certain value.
			intCurrentGradientShift += intGradiantStep;

			if (intCurrentGradientShift >= _GradientShiftStart)
			{
				intGradiantStep = -5;
			} 
			else if ( intCurrentGradientShift == -50) 
			{
				intGradiantStep = 5;
			}
        
		}

		protected override void OnResize(EventArgs ea)
		{

			// Obtain the Graphics object exposed by the Form and erase any drawings.
			Graphics grfx  = CreateGraphics();
			grfx.Clear(BackColor);
		}

		#endregion
	}
}
