using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

using Nistec.WinForms.Controls;

namespace Nistec.WinForms
{

	[Designer(typeof(Design.LinkLabelItemDesigner))]
	[System.ComponentModel.ToolboxItem(false)]
    public class LinkLabelItem : Nistec.WinForms.Controls.McBase, IButton
	{
		#region Members
		private System.ComponentModel.Container components = null;
		//protected ToolTip toolTip;
		protected bool m_TextOnly;
		private Color m_HotColor = System.Drawing.Color.Blue ;
		private Rectangle ImageRect;
		private Rectangle TextRect;
		private string m_Target;
        internal Control owner;

		private const int ImageRectWidth =20;

		[Category("Action")]
		public event LinkClickEventHandler LinkClick;

		#endregion

		#region Constructors

		public LinkLabelItem()
		{
			InitializeComponent();
             
			SetStyle(ControlStyles.Selectable,false);
			SetStyle(ControlStyles.StandardDoubleClick,false);
			SetStyle(ControlStyles.StandardClick ,true);
			SetStyle(ControlStyles.SupportsTransparentBackColor ,true);
			this.TabStop =false;
  
			this.m_TextOnly=false; 
			this.BackColor =Color.Transparent; 
			m_Target="";
			ImageRect=new Rectangle (0,0,0,0);
			TextRect=new Rectangle (0,0,0,0);

			SetRects();

		}

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
			//this.toolTip = new ToolTip();
			// 
			// LinkLabelItem
			// 
			this.Cursor = Cursors.Hand ;
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "LinkLabelItem";
			this.Size = new System.Drawing.Size(150, 19);
		}
		#endregion

		#region Overrides
        
		protected override void OnMouseEnter(System.EventArgs e)
		{
            this.ctlState = McState.Hot;
			this.Invalidate(false);
		}

		protected override void OnMouseLeave(System.EventArgs e)
		{
            this.ctlState = McState.Default;
            this.Invalidate(false);
		}

		protected override void OnGotFocus(System.EventArgs e)
		{
            this.ctlState = McState.Focused;
            this.Invalidate(false);
		}

		protected override void OnLostFocus(System.EventArgs e)
		{
            //this.ctlState = McState.Default;
            this.Invalidate(false);
		}

		protected override void OnSizeChanged(System.EventArgs e)
		{
			base.OnSizeChanged(e);
			this.SetRects ();
		}

		protected override void OnParentChanged(EventArgs e)
		{
			this.SetRects ();
			base.OnParentChanged(e);
		}

		protected override void OnTextChanged(EventArgs e) 
		{
			this.SetRects ();
			base.OnTextChanged(e);
		}

		protected override void OnClick(EventArgs e)
		{
			base.OnClick (e);
			if(m_Target.Length >0)
				LinkTarget(this,m_Target);
			if(this.LinkClick !=null)
               LinkClick(this,new Nistec.WinForms.LinkClickEvent (this.Name, this.Text));

           //if (owner != null)
           //{
           //    if (owner is McTaskPanel)
           //    {
           //        ((McTaskPanel)owner).OnItemClicked(this);
           //    }
           //}
		}


		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint (e);

			Graphics g=e.Graphics;

//		}
//
//		protected override void DrawControl(Graphics g,bool hot,bool focused)
//		{
//
			this.BackColor =Color.Transparent;//  this.Parent.BackColor;
			SetRects ();
			 Size txtSize = Nistec.Drawing.TextUtils.GetTextSize(g, Text.Replace("&",""), Font,TextRect.Size );
             Point tPoint =new Point(TextRect.X  + 2,TextRect.Y +( (this.Height  - this.Font.Height)/2 )); 

			using(StringFormat sf =new StringFormat())
			{
				sf.LineAlignment = StringAlignment.Near  ;
				sf.Alignment     = StringAlignment.Near      ;
		
				if (RightToLeft ==RightToLeft.Yes ) 
				{
					tPoint = new Point(TextRect.Width - txtSize.Width-2,TextRect.Y +( (this.Height  - this.Font.Height)/2 )); 
				}

				if(!Enabled)
				{
					g.DrawString(Text, this.Font, new SolidBrush (ForeColor), tPoint , sf);
				}
				else if(ctlState==McState.Hot || ctlState==McState.Focused)//(hot || focused)
				{
					FontStyle fontStyle = FontStyle.Underline; 
					Font font=new Font (this.Font ,fontStyle);
					g.DrawString(Text, font, new SolidBrush (m_HotColor), tPoint, sf);
				}
				else
				{
					g.DrawString(Text, this.Font,new SolidBrush ( ForeColor), tPoint, sf);

				}
			}

			Image image=base.GetCurrentImage();
			if (image != null && !m_TextOnly )
			{
				if (this.Enabled) 
					g.DrawImage(image, ImageRect.X +2,ImageRect.Y+2); 
				else 
					ControlPaint.DrawImageDisabled(g, image, ImageRect.X +2, ImageRect.Y +2,this.BackColor);
			}

		}

	   #endregion

		#region Internal Points

		private void SetRects()
		{
			Rectangle rect =ClientRectangle; 
			if(m_TextOnly )
			{
				TextRect.Location =new Point(rect.X ,rect.Y  ); 
				TextRect.Size  =new Size(rect.Width,rect.Height);
			}
			else
			{
				if (RightToLeft ==RightToLeft.Yes ) 
				{
					ImageRect.Location =new Point(rect.Width -ImageRectWidth ,rect.Y ); 
					TextRect.Location =new Point(rect.X  ,rect.Y ); 
				}
				else
				{
					ImageRect.Location =new Point( rect.X  ,rect.Y ); 
					TextRect.Location =new Point(rect.X +ImageRectWidth ,rect.Y); 
				}
				ImageRect.Size  =new Size(ImageRectWidth,ImageRectWidth);
				TextRect.Size  =new Size(rect.Width-ImageRectWidth,rect.Height);
			}
		}

		private Rectangle GetBounds()
		{
			return new Rectangle(0, 0, this.Width-1, this.Height-1);
		}
 
		#endregion

		#region Properties

        [Category("Style"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override ControlLayout ControlLayout
        {
            get { return base.ControlLayout; }
            set
            {
                base.ControlLayout = value;
            }

        }

		[Category("Style")]
		[DefaultValue(typeof(Color),"Blue")]
		public Color HotColor
		{
			get {return m_HotColor;}
			set 
			{
				m_HotColor = value;
				this.Invalidate();
			}
		}

//		public HorizontalAlignment TextAlignment
//		{
//			get{return m_Alignment;}
//			set
//			{
//				m_Alignment=value;
//				//GetPoints();
//				this.Invalidate ();
//		
//			}
//		}

		[Category("Behavior")]
		[DefaultValue(false)]
		public bool IsTextOnly
		{
			get { return m_TextOnly; }
			set
			{
				m_TextOnly=value;
				this.Invalidate ();
			}
		}

//		[Category("Behavior")]
//		[DefaultValue("")]
//		public String ToolTip
//		{
//			get { return toolTip.GetToolTip(this); }
//			set
//			{
//				toolTip.RemoveAll();
//				toolTip.SetToolTip(this, value);
//			}
//		}

		[Category("Behavior")]
		[DefaultValue("")]
		public string Target
		{
			get	{ return m_Target; }
			set
			{
				m_Target = value;
				this.Invalidate();
			}
		}

        [Browsable(false)]
        public ButtonStates ButtonState
        {
            get { return ButtonStates.Normal; }
        }
		#endregion

		#region Public Methods

		public void LinkTarget(object sender,string target)
		{
			if(this.Enabled && target != null )
			{
				try
				{
					if(null != target )//&& target.StartsWith("www"))
					{
						System.Diagnostics.Process.Start(target);
					}
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message.ToString (),"Nistec" );
				}
			}
		}

	
	#endregion


	}

}