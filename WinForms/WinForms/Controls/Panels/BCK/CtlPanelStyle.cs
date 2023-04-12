using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using mControl.Util;
using mControl.Win32;


namespace mControl.WinCtl.Controls
{
	//[Designer( typeof(Design.PanelDesigner)),DefaultProperty("BorderStyle"), DefaultEvent("Paint")]
	[ToolboxItem(false)]
	[ToolboxBitmap (typeof(CtlPanel),"Toolbox.Panel.bmp")]
	public class CtlPanelStyle : CtlPanel
	{

		#region Members
		private GradientStyle gardientStyle=GradientStyle.TopToBottom;
        internal bool allowGradient = true;
		#endregion

		#region Contructors
		
		public CtlPanelStyle():base
            ()
		{
            this.Name = "CtlPanelStyle";
		}
        public CtlPanelStyle(ControlLayout ctlLayout)
            : base(ctlLayout)
		{
            this.Name = "CtlPanelStyle";
        }

        internal CtlPanelStyle(bool net):base(net)
		{
            this.Name = "CtlPanelStyle";
		}

		#endregion

		#region Dispose

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
                //if(components != null)
                //{
                //    components.Dispose();
                //}
			}
			base.Dispose( disposing );
		}

		#endregion

		#region override

        //private bool OwnerDraw
        //{
        //    get
        //    {
        //        return (this.m_ControlLayout != ControlLayout.XpLayout && this.m_ControlLayout != ControlLayout.VistaLayout);
        //    }
        //}

        //private void WmEraseBkgnd(ref Message m)
        //{
        //    Win32.RECT rect1 = new Win32.RECT();
        //    Win32.WinAPI.GetClientRect(base.Handle, ref rect1);
        //    Graphics graphics1 = Graphics.FromHdcInternal(m.WParam);
        //    Brush brush1 = new SolidBrush(this.BackColor);
        //    graphics1.FillRectangle(brush1, rect1.left, rect1.top, rect1.right - rect1.left, rect1.bottom - rect1.top);
        //    graphics1.Dispose();
        //    brush1.Dispose();
        //    m.Result = (IntPtr) 1;
        //}

        //[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode=true)]
        //protected override void WndProc(ref Message m)
        //{
        //    if (this.OwnerDraw)
        //    {
        //        base.WndProc(ref m);
        //    }
        //    else
        //    {
        //        int num1 = m.Msg;
        //        if ((num1 == 20) || (num1 == 0x318))
        //        {
        //            this.WmEraseBkgnd(ref m);
        //        }
        //        else
        //        {
        //            base.WndProc(ref m);
        //        }
        //    }
        //}

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //WinAPI.ShowScrollBar(this.Handle, (int)ScrollBarTypes.SB_BOTH, 0);
            Rectangle rect = this.ClientRectangle;
            DrawContainer(e.Graphics, rect, true);

            //switch (m_ControlLayout)
            //{
            //    case ControlLayout.Flat:
            //        DrawContainer(e.Graphics, rect, StylePainter != null);//true
            //        break;
            //    case ControlLayout.Visual:
            //    case ControlLayout.XpLayout:
            //    case ControlLayout.VistaLayout:
            //        if (allowGradient)
            //            DrawPanelXP(e.Graphics, rect);
            //        else
            //            DrawContainer(e.Graphics, rect, StylePainter != null);
            //        break;
            //    default:
            //        DrawContainer(e.Graphics, rect, StylePainter != null);//false
            //        break;
            //}
            //base.OnPaint(e);
        }

 		private void DrawPanelXP(Graphics g,Rectangle bounds)
		{
			float gradiaentAngle=(float)this.gardientStyle;
			Rectangle 	rect=new Rectangle (bounds.X ,bounds.Y,bounds.Width-1 ,bounds.Height-1);

			if(m_ControlLayout==ControlLayout.Visual )
			{
				using(Brush sb=CtlStyleLayout.GetBrushGradient(rect,gradiaentAngle))
				{
					g.FillRectangle (sb,rect);
				}
				
				if(m_BorderStyle ==BorderStyle.FixedSingle)
				{
					using(Pen pen=CtlStyleLayout.GetPenBorder())
					{
						g.DrawRectangle (pen,rect);
					}
				}
				else if(m_BorderStyle==BorderStyle.Fixed3D)
					ControlPaint.DrawBorder3D (g,rect,System.Windows.Forms.Border3DStyle.Sunken);
			}

            else if (m_ControlLayout == ControlLayout.XpLayout )
			{
				using (SolidBrush sb=new SolidBrush (this.Parent.BackColor))
				{
					g.FillRectangle(sb,bounds);
				}

				g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
				System.Drawing.Drawing2D.GraphicsPath path = mControl.Drawing.DrawUtils.GetRoundedRect(rect, 4);

				using(Brush sb=CtlStyleLayout.GetBrushGradient(rect,gradiaentAngle))
				{
					g.FillPath  (sb,path);
				}
		
				if(m_BorderStyle==BorderStyle.FixedSingle)
				{
					using(Pen pen=CtlStyleLayout.GetPenBorder())
					{
						g.DrawPath (pen,path);
					}
				}
				else if(m_BorderStyle==BorderStyle.Fixed3D)
					ControlPaint.DrawBorder3D (g,bounds,System.Windows.Forms.Border3DStyle.Sunken);
			}
            else if (m_ControlLayout == ControlLayout.VistaLayout)
            {
                using (SolidBrush sb = new SolidBrush(this.Parent.BackColor))
                {
                    g.FillRectangle(sb, bounds);
                }

                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                System.Drawing.Drawing2D.GraphicsPath path = mControl.Drawing.DrawUtils.GetRoundedRect(rect, 4);

                using (Brush sb = CtlStyleLayout.GetBrushGradientDark(rect, gradiaentAngle))
                {
                    g.FillPath(sb, path);
                }

                if (m_BorderStyle == BorderStyle.FixedSingle)
                {
                    using (Pen pen = CtlStyleLayout.GetPenBorder())
                    {
                        g.DrawPath(pen, path);
                    }
                }
                else if (m_BorderStyle == BorderStyle.Fixed3D)
                    ControlPaint.DrawBorder3D(g, bounds, System.Windows.Forms.Border3DStyle.Sunken);
            }
        }

 		#endregion

		#region Properties

	
		[Category("Style"),DefaultValue(GradientStyle.TopToBottom), Browsable(true),EditorBrowsable(EditorBrowsableState.Advanced),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual GradientStyle GradientStyle
		{
			get{return this.gardientStyle;}
			set
			{
				if(gardientStyle!=value)
				{
					gardientStyle=value;
					this.Invalidate();
				}
			}
		}


		#endregion

		#region Methods

//		public void SetGradiaentAngle(float value)
//		{
//			if (!(value >=0 && value <=360))
//			{
//				throw new ArgumentException("Value must be between 0 and 360");
//			}
//			gradiaentAngle=value;
//		}


		public override string ToString()
		{
			string text1 = base.ToString();
			return (text1 + ControlLayout.ToString());
		}

		#endregion

	}
}
