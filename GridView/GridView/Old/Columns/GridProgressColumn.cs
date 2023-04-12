using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.ComponentModel;
using mControl.Util;

using System.Threading; 
 
namespace mControl.GridStyle.Columns
{
	
	public class GridProgressColumn : GridColumnStyle 
	{


		#region Members
//		private Color m_ColorBrush1;
//		private Color m_ColorBrush2;
//		private Color m_BorderColor;
		private Color m_ProgressTextColor;
		private bool m_ShowPrc;
		private int m_ProgressMin;
		private int m_ProgressMax;
		
		#endregion

		#region Consructor
		public GridProgressColumn() : base() 
		{
//			m_ColorBrush1=Color.Blue;
//			m_ColorBrush2=Color.Blue;
//			m_BorderColor=Color.Gray ;
			m_ProgressTextColor=Color.Yellow ;
			m_ProgressMin = 0;
			m_ProgressMax = 100;
			m_ShowPrc=true;
			//m_TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            NullText="0";
			m_ColumnType = ColumnTypes.ProgressColumn ;
		}


		#endregion

		#region Property
//        [DefaultValue(typeof(Color),"Gray")]
//		public Color BorderColor 
//		{
//			get { return m_BorderColor; }
//			set { m_BorderColor = value; }
//		}
//
//		[DefaultValue(typeof(Color),"Blue")]
//		public Color ProgressColor 
//		{
//			get { return m_ColorBrush1; }
//			set { m_ColorBrush1 = value; }
//		}

		/*public Color BrushColor 
		{
			get { return m_ColorBrush2; }
			set { m_ColorBrush2 = value; }
		}*/

		[DefaultValue(typeof(Color),"Yellow")]
		public Color ProgressTextColor 
		{
			get { return m_ProgressTextColor; }
			set { m_ProgressTextColor = value; }
		}

		[DefaultValue(true)]
		public bool ShowTextPercent 
		{
			get { return m_ShowPrc ; }
			set { m_ShowPrc = value; }
		}
		
		[DefaultValue(0)]
		public int MinValue 
		{
			get { return m_ProgressMin; }
			set { 
				  if(value>=0 && value < MaxValue)
				    m_ProgressMin = value; 
			    }
		}

		[DefaultValue(100)]
		public int MaxValue 
		{
			get { return m_ProgressMax; }
			set { 
				 if(value>=0 && value > MinValue)
				  m_ProgressMax = value; 
				}
		}

//		[Browsable(false),DefaultValue(0)]
//		public int ProgressValue 
//		{
//			get
//			{
//				object oVal= this.GetControlvalue() ;
//				if(Regx.IsNumeric (oVal.ToString ()))
//					return ((int)oVal);
//				return 0;
//			}
//			set
//			{
//				SetProgressValue(CM(),this.CurrentRow(),value);
//			}
//		}

		public int GetProgressValue()
		{
			object oVal= this.GetControlvalue() ;
			if(Regx.IsNumeric (oVal.ToString ()))
				return ((int)oVal);
			return 0;
		}

		public void ResetProgress(int rowNum)
		{
			if(this.CurrentRow()==rowNum)
			{
				// CM()
				this.SetColumnValueAtRow(grid.ListManager,this.CurrentRow() , m_ProgressMin);
				this.Invalidate(); 
			}
		}

		public void SetProgressValue(int value, int rowNum)
		{
			if(this.CurrentRow()!=rowNum)
				this.DataGridTableStyle.DataGrid.CurrentRowIndex =rowNum;  
			// CM()
			SetProgressValue(grid.ListManager,this.CurrentRow(),value);
		}

		//private bool start=false;

		private void SetProgressValue(CurrencyManager source, int rowNum,int value)
		{
			object oVal= this.GetColumnValueAtRow (source,rowNum);
			int progressValue=0;

			//if(Regx.IsNumeric (oVal.ToString ()))
				progressValue=((int)oVal);
			
			//int mtValue = (value * 100) / (m_ProgressMax - m_ProgressMin);
			if(value > progressValue && progressValue < m_ProgressMax)//((mtValue > m_ProgressValue) && (mtValue <= 100) && (mtValue >= 0))
			{
				//m_ProgressValue = mtValue;
				this.SetColumnValueAtRow(source,rowNum , value);
				//this.DataGridTableStyle.DataGrid.Update();

				this.Invalidate();
				Application.DoEvents();
			}
		}

//		private void RunProgress()
//		{
//
//				object oVal= this.GetColumnValueAtRow (source,rowNum);
//				int progressValue=0;
//
//				if(Regx.IsNumeric (oVal.ToString ()))
//					progressValue=((int)oVal);
//			
//				//int mtValue = (value * 100) / (m_ProgressMax - m_ProgressMin);
//				if(value > progressValue && progressValue < m_ProgressMax)//((mtValue > m_ProgressValue) && (mtValue <= 100) && (mtValue >= 0))
//				{
//					//m_ProgressValue = mtValue;
//					this.SetColumnValueAtRow(source,rowNum , value);
//					//this.DataGridTableStyle.DataGrid.Update();
//
//					this.Invalidate();
//					Application.DoEvents();
//				}
//
//		}

		#endregion

		#region Override

		internal protected override void SetDataGridInColumnInternal(DataGrid value,bool forces)
		{
			if(this.grid==null)
				this.grid=((CtlGrid)value).interanlGrid as Grid;

//			m_ColorBrush1 =((CtlGrid)value).GridLayout.BorderHotColor ;   
//			m_ColorBrush2 =((CtlGrid)value).CaptionForeColor  ;   
//			m_BorderColor =((CtlGrid)value).GridLayout.BorderColor ;   
//			m_ProgressTextColor =Color.Yellow;//((CtlGrid)value).interanlGrid.HighlightColor  ;   
		}

		protected override void SetDataGridInColumn(DataGrid value)
		{
			base.SetDataGridInColumn(value);
			SetDataGridInColumnInternal(value,false);
		}

		protected override void Edit(CurrencyManager source, int rowNum, Rectangle bounds, bool readOnly, string instantText, bool cellIsVisible)
		{
		  //
		}
		protected override void Paint(System.Drawing.Graphics g, System.Drawing.Rectangle bounds, CurrencyManager source, int rowNum, System.Drawing.Brush backBrush, System.Drawing.Brush foreBrush, bool alignToRight) 
		{

			Rectangle rectangle1 = bounds;
			StringFormat format1 = new StringFormat();
			format1.Alignment =StringAlignment.Center;
			format1.LineAlignment =StringAlignment.Center ;
			format1.FormatFlags |= StringFormatFlags.NoWrap;
			g.FillRectangle(backBrush, rectangle1);
			rectangle1.Offset(0, 2 * this.yMargin);
			rectangle1.Height -= 2 * this.yMargin;
           
			Rectangle controlBounds =rectangle1;
			controlBounds.Inflate (-2,-2);

			Rectangle fillRect = new Rectangle( 
				controlBounds.X + 2, 
				controlBounds.Y + 2,
				controlBounds.Width - 3,
				controlBounds.Height - 3);

			//g.FillRectangle( backBrush, bounds );
			//Rectangle controlBounds = this.GetCellBounds( bounds );

			try
			{
				int maxWidth = (int)fillRect.Width;
				object oVal=this.GetColumnValueAtRow( source, rowNum );
				int val=0;
				
				if(Regx.IsNumeric (oVal.ToString ()))
					val=(int)oVal;//this.GetColumnValueAtRow( source, rowNum );
			
				val=(int)(val * 100) / (m_ProgressMax - m_ProgressMin);
				double indexWidth = ( (  double ) fillRect.Width ) / 100; // determines the width of each index.
				fillRect.Width = ( int )( val * indexWidth );
				//fillRect.Width = ( int )( ( ( int ) this.GetColumnValueAtRow( source, rowNum ) ) * indexWidth );
				if ( fillRect.Width > maxWidth ) 
				{
					fillRect.Width = maxWidth;
				}

				if ( fillRect.Width > 0 ) 
				{
					using ( Brush sb =grid.CtlStyleLayout.GetBrushCaptionGradient(fillRect,90f,true))// new SolidBrush( m_ColorBrush1 ) ) 
					{
						g.FillRectangle( sb, fillRect );
					}
				}
				using ( Pen p =grid.CtlStyleLayout.GetPenBorder())// new Pen( new SolidBrush(this.m_BorderColor ) ) ) 
				{
					g.DrawRectangle( p, controlBounds ); 
				}
			
				if(m_ShowPrc)
				{
					using ( SolidBrush sb = new SolidBrush( m_ProgressTextColor ) ) 
					{
						if(val>0)
						{
							g.DrawString(val.ToString() + "%", this.DataGridTableStyle.DataGrid.Font, sb, (RectangleF) controlBounds , format1);
						}
					}
				}
				format1.Dispose();
			}
	
			catch(Exception ex)
			{
				MessageBox.Show (ex.Message.ToString ()); 
			}
			finally
			{

			}

		}
		#endregion
			
	}

}