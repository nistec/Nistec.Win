
using System;
using System.ComponentModel;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace mControl.GridStyle.Columns
{

	public class GridLabelColumn : GridColumnStyle {
	
		#region Members
		 private bool m_DrawLabel;
		 private Color m_ForeColor;
		 private Color m_BackColor;
		#endregion

		#region Constructor

		public  GridLabelColumn(PropertyDescriptor prop) : this(prop, null, false)
		{
			InitColumn();
		}

		public GridLabelColumn(PropertyDescriptor prop, string format) : this(prop, format, false)
		{
			InitColumn();
			this.Format = format;
		}
		public GridLabelColumn(PropertyDescriptor prop, string format, bool isDefault) : base(prop, isDefault)
		{
			InitColumn();
			m_DrawLabel=isDefault;
			this.Format = format;
		}

		public GridLabelColumn() : base() 
		{
			InitColumn();
		}

		private void InitColumn() 
		{

			m_ForeColor=Color.Black ;
			m_BackColor=Color.White ;
			m_DrawLabel=false;

			//this.ControlSize = new Size( 80, 12 );
			m_ColumnType = ColumnTypes.LabelColumn  ;
		}
		#endregion
	
		#region override

		protected override void PaintText(Graphics g, Rectangle textBounds, string text, Brush backBrush, Brush foreBrush, bool alignToRight)
		{
			Rectangle rectangle1 = textBounds;
			StringFormat format1 = new StringFormat();
			if (alignToRight)
			{
				format1.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
			}
			format1.Alignment = (this.Alignment == HorizontalAlignment.Left) ? StringAlignment.Near : ((this.Alignment == HorizontalAlignment.Center) ? StringAlignment.Center : StringAlignment.Far);
			format1.LineAlignment =StringAlignment.Center ;
			format1.FormatFlags |= StringFormatFlags.NoWrap;
		
			rectangle1.Offset(0, 2 * this.yMargin);
			rectangle1.Height -= 2 * this.yMargin;
	
			if(!m_DrawLabel)
			{
				g.FillRectangle( backBrush, textBounds);
				g.DrawString(text, this.DataGridTableStyle.DataGrid.Font, foreBrush , (RectangleF) rectangle1, format1);
			}
			else
			{
				using( Brush sb1= new SolidBrush (BackColor),sb2= new SolidBrush (ForeColor))
				{
					g.FillRectangle( sb1, textBounds);
					g.DrawString(text, this.DataGridTableStyle.DataGrid.Font, sb2, (RectangleF) rectangle1, format1);
				}
			}
			format1.Dispose();
		}

		#endregion

		#region property

		[DefaultValue(false)]
		public bool DrawLabel 
		{
			get{return m_DrawLabel ;}
			set
			{
				m_DrawLabel = value;
				this.Invalidate (); 
			}
		}

//	    [DefaultValue(false)]
//		public new bool ShowSum 
//		{
//			get{return m_IsSum;}
//			set
//			{
//				if(m_FormatType ==DataTypes.Number)    
//					m_IsSum = value;
//				else 
//					m_IsSum =false;
//				this.Invalidate (); 
//			}
//		}

		[DefaultValue(typeof(Color),"White")]
		public Color BackColor 
		{
			get{return m_BackColor;}
			set
			{   if(value != Color.Transparent)
					m_BackColor = value;
				this.Invalidate ();
			}
		}

		[DefaultValue(typeof(Color),"Black")]
		public Color ForeColor 
		{
			get{return m_ForeColor ;}
			set
			{
				m_ForeColor  = value;
				this.Invalidate ();
			}
		}

//		[DefaultValue(DataTypes.Text )]
//		public new DataTypes FormatType 
//		{
//			get	{return mFormatType;}
//			set
//			{
//				mFormatType=value;
//				if(value==DataTypes.Number)
//				{
//					this.TextAlignment=System.Windows.Forms.HorizontalAlignment.Right ;
//					mIsSum=true;
//				}
//				else 
//					mIsSum=false;
//				this.Invalidate (); 
//			} 
//		}

		#endregion
	}

}	