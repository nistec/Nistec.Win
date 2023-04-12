using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using mControl.GridStyle;
using mControl.GridStyle.Controls;
using mControl.WinCtl.Controls;
 
namespace mControl.GridStyle.Columns
{
	
	public class GridControlColumn : GridColumnStyle {

		#region Members
		private GridStyle.Controls.GridControl  edit;
		private bool alignRight;
		//private string m_Caption="+";
		//private Color m_BorderColor=SystemColors.ControlDark;
		//private Color m_BackColor=SystemColors.Control;
		private const int buttonWidth=11;
	
		#endregion

		internal protected static Image collapsed;
		internal protected static Image expaned;

		static GridControlColumn()
		{
			GridControlColumn.collapsed =NativeMethods.LoadImage("mControl.GridStyle.Images.collapsed.gif");
			GridControlColumn.expaned  =NativeMethods.LoadImage ("mControl.GridStyle.Images.expaned.gif");

		}

    
		#region Constructor
		public GridControlColumn() : base() 
		{
			alignRight=false;
			edit = new GridStyle.Controls.GridControl ();
			edit.Visible  =false;
			m_ColumnType = ColumnTypes.GridColumn  ;
			hostedCtl =this.edit;
		}

		#endregion

		#region Property

//		[DefaultValue(ButtonState.Flat)]
//		public ButtonState ButtonStyle 
//		{
//			get{return m_ButtonStyle;}
//			set{m_ButtonStyle = value;}
//		}
//        
//		[DefaultValue(typeof(Color),"ControlDark")]
//		public Color BorderColor 
//		{
//			get{return m_BorderColor;}
//			set{m_BorderColor = value;}
//		}

		/*[DefaultValue(typeof(Color),"Blue")]
		public new Color FocusColor 
		{
			get{return m_FocusColor;}
			set{m_FocusColor = value;}
		}*/

		/*public string Caption 
		{
			get{return m_Caption;}
			set{m_Caption = value;}
		}
        
		public string CaptionPush 
		{
			get{return m_CaptionPush;}
			set{m_CaptionPush = value;}
		}*/

		[System.ComponentModel.Browsable(false)] 
		public GridStyle.Controls.GridControl GridControl
		{
			get{return edit;}
		}

		[Browsable(false)]
		public DataGridTableStyle GridTableStyle
		{
			get{return edit.GridTableStyle  ;}
			//set{edit.GridTableStyle  =value;}
		}

		[TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), Description("DataGridDataSourceDescr"), DefaultValue((string) null), Category("Data"), RefreshProperties(RefreshProperties.Repaint)]
		public object DataSource
		{
			get{return edit.DataSource;}
			set{edit.DataSource=value;}
		}

		[DefaultValue((string) null), Editor("System.Windows.Forms.Design.DataMemberListEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor)), Description("DataGridDataMemberDescr"), Category("Data")]
		public string DataMember
		{
			get{return edit.DataMember;}
			set{edit.DataMember=value;}
		}

		[DefaultValue("")]
		public string RowFilter
		{
			get{return edit.RowFilter;}
			set{edit.RowFilter=value;}
		}

		[DefaultValue("")]
		public string FieldFilter
		{
			get{return edit.FieldFilter;}
			set{edit.FieldFilter=value;}
		}
		
		[DefaultValue(GridStyle.Controls.GridFilterType.None )]
		public GridStyle.Controls.GridFilterType FilterType
		{
			get{return edit.FilterType;}
			set{edit.FilterType=value;}
		}

		[DefaultValue(10 )]
		public int VisibleRows
		{
			get{return edit.VisibleRows;}
			set{edit.VisibleRows=value;}
		}

		public int MaxWidth
		{
			get{return edit.MaxWidth;}
			set
			{
				if(MaxWidth!=value)
				{
					edit.MaxWidth=value;
				}
			}
		}

		public void Init(object dataSource,string dataMember)
		{
			this.DataSource=dataSource;
			this.DataMember=dataMember;
		}

		public void Init(object dataSource,string dataMember,string fieldFilter,GridFilterType filterType)
		{
		   Init(dataSource,dataMember,fieldFilter,filterType,this.VisibleRows);
		}

		public void Init(object dataSource,string dataMember,string fieldFilter,GridFilterType filterType,int visibleRows)
		{
			this.DataSource=dataSource;
			this.DataMember=dataMember;
			this.FieldFilter=fieldFilter;
			this.FilterType=filterType;
			this.VisibleRows=visibleRows;
		}

		#endregion
		
		#region Overrides

		internal protected override void SetDataGridInColumnInternal(DataGrid value,bool forces)
		{
			if(this.edit.dataGrid==value && !forces)
				return;

			this.edit.SetDataGrid(value);
			if(this.grid==null)
				this.grid=((CtlGrid)value).interanlGrid as Grid;

//			Styles style =((CtlGrid)value).interanlGrid.CtlStyleLayout.StylePlan;   
//			if(style==Styles.Custom)   
//				this.edit.StyleCtl.SetStyleLayout(((CtlGrid)value).interanlGrid.CtlStyleLayout.Layout);   
//			else			
//				this.edit.StyleCtl.StylePlan =style;   
			
			//this.edit.StyleCtl.StylePlan =((CtlGrid)value).Style;   
			//this.edit.StyleCtl.StylePlan =((CtlGrid)value).interanlGrid.CtlStyleLayout.StylePlan; 
			//this.edit.StylePainter=grid.StylePainter;
			//m_BorderColor=((CtlGrid)value).GridLayout.BorderColor;
			//m_BackColor =((CtlGrid)value).GridLayout.FlatColor;
			//this.edit.SetStyleLayout(grid.CtlStyleLayout.Layout);
			//this.edit.SetGridColumns();
		}

		protected override void SetDataGridInColumn( DataGrid value ) 
		{
			base.SetDataGridInColumn(value);
			if (this.edit.Parent != null)
			{
				this.edit.Parent.Controls.Remove(this.edit);
			}
			if (value != null)
			{
				value.Controls.Add(this.edit);
			}
			SetDataGridInColumnInternal(value,false);
		}

		
		#endregion

		#region Methods

		protected override bool Commit( CurrencyManager dataSource, int rowNum )
		{
			m_RowEdit = false;
			edit.Visible = false;
			return true;
		}

		protected override void Edit(CurrencyManager source, int rowNum, Rectangle bounds, bool readOnly, string instantText, bool cellIsVisible)
		{
			this.DebugOut("Begining Edit, rowNum :" + rowNum.ToString());
			Rectangle rectangle1 = bounds;
			//this.edit.ReadOnly = (readOnly || this.ReadOnly) || this.DataGridTableStyle.ReadOnly;
			string colValue = this.GetText(this.GetColumnValueAtRow(source, rowNum));
			bool isMouseInRect=false;

			if (cellIsVisible)
			{
				bounds.Offset(this.xMargin, 2 * this.yMargin);
				bounds.Width -= this.xMargin;
				bounds.Height -= 2 * this.yMargin;
				Rectangle controlBounds =GetButtonBounds(bounds,alignRight );
				//controlBounds.Inflate (-2,-2);

				Point p  = this.DataGridTableStyle.DataGrid.PointToClient(Cursor.Position);
				Rectangle cursorBounds = new Rectangle(p.X, p.Y, 1, 1);

				isMouseInRect= cursorBounds.IntersectsWith(controlBounds); 

		
				this.DebugOut("edit bounds: " + bounds.ToString());
				this.edit.Bounds = controlBounds;
				this.edit.ValueFilter=colValue;
				this.edit.Visible = true;
				//this.DataGridTableStyle.DataGrid.Invalidate(controlBounds);
			}
			else
			{
				this.edit.Bounds = rectangle1;
				this.edit.Visible = false;
			}
			this.edit.RightToLeft = this.DataGridTableStyle.DataGrid.RightToLeft;
			this.edit.Focus();
			this.editRow = rowNum;
			if (this.edit.Visible && isMouseInRect)
			{
					this.Invalidate();
					Application.DoEvents();
					this.DataGridTableStyle.DataGrid.Invalidate(rectangle1);
					this.edit.PerformClick ();
				    grid.OnButtonClicked(this,this.DataGridTableStyle,rowNum,CurrentCol());
			}
		}

		protected override void Paint(System.Drawing.Graphics g, System.Drawing.Rectangle bounds, CurrencyManager source, int rowNum, System.Drawing.Brush backBrush, System.Drawing.Brush foreBrush, bool alignToRight) 
		{
			try 
			{
				alignRight=alignToRight;
				Rectangle rectangle1 = bounds;
				StringFormat format1 = new StringFormat();
				StringFormat format2 = new StringFormat();

				Rectangle controlBounds = GetButtonBounds(bounds,alignToRight);
				Rectangle textRect =bounds;
                textRect.Width-=(controlBounds.Width+2);
		
			
				if (alignToRight)
				{
					format1.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
					textRect.X= bounds.X-2;// bounds.Width -controlBounds.Width -2;
				}
				else
				{
					textRect.X= controlBounds.X +controlBounds.Width+2;
				}

				format1.Alignment = (this.Alignment == HorizontalAlignment.Left) ? StringAlignment.Near : ((this.Alignment == HorizontalAlignment.Center) ? StringAlignment.Center : StringAlignment.Far);
				format1.LineAlignment =StringAlignment.Center ;
				format1.FormatFlags |= StringFormatFlags.NoWrap;

				format2.Alignment = StringAlignment.Center ;
				format2.LineAlignment =StringAlignment.Center ;
				format2.FormatFlags |= StringFormatFlags.NoWrap;

				g.FillRectangle(backBrush, bounds);
				//ButtonState bs = m_ButtonStyle;

				using(Brush sb1=grid.CtlStyleLayout.GetBrushBack())// new SolidBrush (m_BackColor))
				{
					g.FillRectangle(sb1,controlBounds );
				}
				using(Pen p=grid.CtlStyleLayout.GetPenBorder())// new Pen (m_BorderColor))
				{
					g.DrawRectangle (p,controlBounds );
				}

				//g.DrawString(m_Caption, this.DataGridTableStyle.DataGrid.Font, foreBrush, (RectangleF) controlBounds , format2);
                 //this.edit.DrawImage(g);
                 g.DrawImage(GridControlColumn.collapsed,(RectangleF) controlBounds);  
				//rectangle1.Offset(0, 2 * this.yMargin);
				//rectangle1.Height -= 2 * this.yMargin;
				string text=this.GetColumnValueAtRow(source,rowNum).ToString ();
				g.DrawString(text, this.DataGridTableStyle.DataGrid.Font, foreBrush, (RectangleF) textRect, format1);
				format1.Dispose();
				format2.Dispose();

			}
			catch  
			{
				throw new Exception("Error In Grid Column column");
			}
		}

		private Rectangle GetButtonBounds(Rectangle bounds, bool alignToRight)
		{
			if (alignToRight)
			{
				return new Rectangle(bounds.X + bounds.Width -buttonWidth-2,bounds.Y+1,buttonWidth ,bounds.Height-2);
			}
			return new Rectangle(bounds.X + 1,bounds.Y+1,buttonWidth ,bounds.Height-2);
		}

		#endregion

	}
}


