
using System;
using System.ComponentModel ;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using mControl.Util;
using mControl.WinCtl.Controls;

using mControl.GridStyle.Controls; 
 
namespace mControl.GridStyle.Columns
{
	
	public class GridMenuColumn : GridColumnStyle 
	{

		#region Members
		// Fields
		private GridStyle.Controls.GridMenu edit;

		//private ButtonState m_ButtonStyle = ButtonState.Flat;
		private string m_Caption="...";
		private string m_CaptionPush="...";
//		private Color m_BorderColor;
//		private Color m_BackColor  ;
//		private Color m_ColorBrush1;
//		private Color m_ColorBrush2;

		#endregion

		#region Constructors
		public GridMenuColumn() : this((PropertyDescriptor) null, (string) null)
		{
			InitColumn();
		}
		public GridMenuColumn(PropertyDescriptor prop) : this(prop, null, false)
		{
			InitColumn();
		}
		public GridMenuColumn(PropertyDescriptor prop, bool isDefault) : this(prop, null, isDefault)
		{
			InitColumn();
		}
		public GridMenuColumn(PropertyDescriptor prop, string format) : this(prop, format, false)
		{
			InitColumn();
			this.Format = format;
		}
		public GridMenuColumn(PropertyDescriptor prop, string format, bool isDefault) : base(prop, isDefault)
		{
			InitColumn();
			this.Format = format;
		}
		private void InitColumn() 
		{
			//m_ButtonStyle = ButtonState.Flat;
	
			this.edit = new GridMenu();
			this.edit.ButtonMenuStyle=ButtonMenuStyles.Default;
			//this.edit.MenuBorderStyle =BorderStyle.FixedSingle; 
			this.edit.Visible = false;
			m_ColumnType = ColumnTypes.MenuColumn  ;
			hostedCtl=this.edit;
		}
		#endregion

		#region internal override

		protected override void Abort(int rowNum)
		{
			this.RollBack();
			this.HideEditBox();
			this.EndEdit();
		}

		protected override bool Commit(CurrencyManager dataSource, int rowNum)
		{
			this.edit.Bounds = Rectangle.Empty;
			this.DebugOut("OnCommit completed without Exception.");
			this.EndEdit();
			return true;
		}
	
		protected override void ConcedeFocus()
		{
			this.edit.Bounds = Rectangle.Empty;
		}

 
		protected override void Edit(CurrencyManager source, int rowNum, Rectangle bounds, bool readOnly, string instantText, bool cellIsVisible)
		{
			this.DebugOut("Begining Edit, rowNum :" + rowNum.ToString());
			Rectangle rectangle1 = bounds;
			this.edit.ReadOnly = (readOnly || this.ReadOnly) || this.DataGridTableStyle.ReadOnly;
			this.edit.Text =this.CaptionPush;//  this.GetText(this.GetColumnValueAtRow(source, rowNum));
			string colValue = this.GetText(this.GetColumnValueAtRow(source, rowNum));
			bool isMouseInRect=false;
		
			if (cellIsVisible)
			{
				bounds.Offset(this.xMargin, 2 * this.yMargin);
				bounds.Width -= this.xMargin;
				bounds.Height -= 2 * this.yMargin;
				Rectangle controlBounds =bounds;
				controlBounds.Inflate (-2,-2);

				Point p  = this.DataGridTableStyle.DataGrid.PointToClient(Cursor.Position);
				Rectangle cursorBounds = new Rectangle(p.X, p.Y, 1, 1);

				isMouseInRect= cursorBounds.IntersectsWith(controlBounds); 

	
				this.DebugOut("edit bounds: " + bounds.ToString());
				this.edit.Bounds = controlBounds;
				this.edit.Visible = true;
				this.DataGridTableStyle.DataGrid.Invalidate(controlBounds);
			}
			else
			{
				this.edit.Bounds = rectangle1;
				this.edit.Visible = false;
			}
			this.edit.RightToLeft = this.DataGridTableStyle.DataGrid.RightToLeft;
			this.edit.Focus();
			this.editRow = rowNum;
			if (!this.edit.ReadOnly)
			{
				this.oldValue = this.edit.Text;
			}
			if (this.edit.Visible && isMouseInRect)
			{
				this.Invalidate();
				Application.DoEvents();
				this.DataGridTableStyle.DataGrid.Invalidate(rectangle1);
				//edit.DroppedDown = true;
				//if(grid.CellClicked!=null)
				//	grid.CellClicked(this,new CellClickEventArgs (this.DataGridTableStyle, rowNum, this.CurrentCol())); 

				grid.OnButtonClicked(this,this.DataGridTableStyle, rowNum, this.CurrentCol()); 
	
				//this.edit.PerformClick (); 
				//if(ButtonClicked!=null)
				//	ButtonClicked(this,new ButtonClickEventArgs (rowNum,this.MappingName ,colValue));//EventArgs.Empty); 
				//HideEditBox();
			}
		}

 
		protected void EndEdit()
		{
			this.edit.IsInEditOrNavigateMode = true;
			this.DebugOut("Ending Edit");
			this.Invalidate();
		}

 
		protected  override void EnterNullValue()
		{
			if ((!this.ReadOnly && this.edit.Visible) && this.edit.IsInEditOrNavigateMode)
			{
				this.edit.Text = this.NullText;
				this.edit.IsInEditOrNavigateMode = false;
				if ((this.DataGridTableStyle != null) && (this.DataGridTableStyle.DataGrid != null))
				{
					((CtlGrid)this.DataGridTableStyle.DataGrid).ColumnStartedEditing(this.edit.Bounds);
				}
			}
		}
 
		protected void HideEditBox()
		{
			bool flag1 = this.edit.Focused;
			this.edit.Visible = false;
			if ((flag1 && (this.DataGridTableStyle != null)) && ((this.DataGridTableStyle.DataGrid != null) && this.DataGridTableStyle.DataGrid.CanFocus))
			{
				this.DataGridTableStyle.DataGrid.Focus();
			}
		}

		protected  override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum)
		{
			this.Paint(g, bounds, source, rowNum, false);
		}

 
		protected  override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum, bool alignToRight)
		{
			this.Paint(g, bounds, source, rowNum, (SolidBrush)new SolidBrush (this.DataGridTableStyle.BackColor), (SolidBrush)new SolidBrush (this.DataGridTableStyle.ForeColor), alignToRight);
		}

		protected  override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum, Brush backBrush, Brush foreBrush, bool alignToRight)
		{
			Rectangle rectangle1 = bounds;
			StringFormat format1 = new StringFormat();
			if (alignToRight)
			{
				format1.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
			}
			format1.Alignment =StringAlignment.Center;// (this.Alignment == HorizontalAlignment.Left) ? StringAlignment.Near : ((this.Alignment == HorizontalAlignment.Center) ? StringAlignment.Center : StringAlignment.Far);
			format1.LineAlignment =StringAlignment.Center ;
			format1.FormatFlags |= StringFormatFlags.NoWrap;
			g.FillRectangle(backBrush, rectangle1);
			rectangle1.Offset(0, 2 * this.yMargin);
			rectangle1.Height -= 2 * this.yMargin;
           
			Rectangle controlBounds =bounds;
			controlBounds.Inflate (-2,-2);

			using (Brush sb= grid.CtlStyleLayout.GetBrushGradient(controlBounds,90f,true))
			{
				g.FillRectangle(sb,controlBounds );
			}
			using(Pen p=grid.CtlStyleLayout.GetPenBorder())// new Pen (m_BorderColor))
			{
				g.DrawRectangle (p,controlBounds );
			}

//			using (System.Drawing.Drawing2D.LinearGradientBrush sb= new System.Drawing.Drawing2D.LinearGradientBrush 
//					   (controlBounds, m_ColorBrush1,m_ColorBrush2, 
//					   System.Drawing.Drawing2D.LinearGradientMode.Vertical))
//			{
//				g.FillRectangle(sb,controlBounds );
//			}
//
//			using(Pen p= new Pen (m_BorderColor))
//			{
//				g.DrawRectangle (p,controlBounds );
//			}

		    
//			using(SolidBrush sb1= new SolidBrush (m_BackColor), sb2= new SolidBrush (m_BorderColor))
//			{
//				g.FillRectangle(sb1,controlBounds );
//				using(Pen p= new Pen (sb2))
//				{
//					g.DrawRectangle (p,controlBounds );
//				}
//			}

			g.DrawString(m_Caption, this.DataGridTableStyle.DataGrid.Font, foreBrush, (RectangleF) controlBounds , format1);
			format1.Dispose();
		}

 
		protected  override void ReleaseHostedControl()
		{
			if (this.edit.Parent != null)
			{
//				foreach(mControl.WinCtl.Controls.ButtonMenuItem item in edit.ButtonItems.MenuItems)
//				{
//					this.edit.Parent.Controls.Remove(item);
//				}
				this.edit.Parent.Controls.Remove(this.edit);
			}
		}

		private void RollBack()
		{
			this.edit.Text = this.oldValue;
		}

 
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
			
			//this.edit.StyleCtl.StylePlan =((CtlGrid)value).interanlGrid.CtlStyleLayout.StylePlan;   
//			m_BorderColor=((CtlGrid)value).GridLayout.BorderColor ;
//			m_BackColor =((CtlGrid)value).GridLayout.FlatColor ;
//			m_ColorBrush1 =((CtlGrid)value).GridLayout.ColorBrush1 ;
//			m_ColorBrush2 =((CtlGrid)value).GridLayout.ColorBrush2 ;
		}

		protected override void SetDataGridInColumn(DataGrid value)
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

		protected  override void UpdateUI(CurrencyManager source, int rowNum, string instantText)
		{
			this.edit.Text = this.GetText(this.GetColumnValueAtRow(source, rowNum));
			if (!this.edit.ReadOnly && (instantText != null))
			{
				this.edit.Text = instantText;
			}
		}

		#endregion
 
		#region Virtual

		protected new string GetDisplayText(object value)
		{
			return this.GetText(value);
		}

		//		protected virtual bool KeyPress(int rowNum, Keys keyData)
		//		{
		//			if (this.edit.IsInEditOrNavigateMode)
		//			{
		//				return base.KeyPress(rowNum, keyData);
		//			}
		//			return false;
		//		}

		#endregion

		#region Properties

		[Browsable(false)]
		public virtual GridMenu ButtonMenu
		{
			get
			{
				return this.edit;
			}
		}


//		[DefaultValue(ButtonState.Flat)]
//		public ButtonState ButtonStyle 
//		{
//			get{return m_ButtonStyle;}
//			set{m_ButtonStyle = value;}
//		}
        
//		[DefaultValue(typeof(Color),"Blue")]
//		public Color BorderColor 
//		{
//			get{return m_BorderColor;}
//			set{m_BorderColor = value;}
//		}

		/*[DefaultValue(typeof(Color),"Gold")]
		public new Color FocusColor 
		{
			get{return m_FocusColor;}
			set{m_FocusColor = value;}
		}*/

		[DefaultValue("...")]
		public string Caption 
		{
			get{return m_Caption;}
			set{m_Caption = value;}
		}
        
		[DefaultValue("...")]
		public string CaptionPush 
		{
			get{return m_CaptionPush;}
			set{m_CaptionPush = value;}
		}

//		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
//		public mControl.Collections.ButtonMenuCollection  MenuItems
//		{
//			get { return edit.MenuItems; }
//		}
//
//		[DefaultValue(mControl.WinCtl.Controls.ButtonMenuStyles.Combo )]
//		public mControl.WinCtl.Controls.ButtonMenuStyles ButtonMenuStyle
//		{
//			get { return edit.ButtonMenuStyle; }
//			set { edit.ButtonMenuStyle = value;} 
//		}

		[Category("Behavior")]
		public  CtlContextMenu DropDownMenu
		{
			get
			{
				return edit.DropDownMenu;
			}
			set
			{
				edit.DropDownMenu = value;
				//this.dropDownMenu.RightToLeft=this.RightToLeft;
				//this.dropDownMenu.StyleCtl.StylePlan=this.CtlStyleLayout.StylePlan;
			}
		}


		#endregion
		
		#region Overrides

//		protected override void SetDataGridInColumn( DataGrid value ) 
//		{
//			base.SetDataGridInColumn( value );
//			if ( !value.Controls.Contains( edit ) ) 
//			{
//				value.Controls.Add( edit );
//				edit.RightToLeft  =this.DataGridTableStyle.DataGrid.RightToLeft;//   this.RightToLeft ;
//				edit.ForeColor   =this.DataGridTableStyle.ForeColor;
//				this.SetRightToLeft (edit.RightToLeft);
//			}
//		}
//
//
//
//		protected override bool Commit( CurrencyManager dataSource, int rowNum )
//		{
//			mRowEdit = false;
//			edit.Visible = false;
//			return true;
//		}
//
//		protected override void Edit(CurrencyManager source, int rowNum, Rectangle bounds, bool readOnly, string instantText, bool cellIsVisible) 
//		{
//			if(readOnly )
//				return;
//			
//			edit.Text  =this.m_CaptionPush ;
//	
//			if (cellIsVisible) 
//			{
//				//  get cursor coordinates
//				Point p = this.DataGridTableStyle.DataGrid.PointToClient(Cursor.Position);
//				Rectangle cursorBounds = new Rectangle(p.X, p.Y, 1, 1);
//		        
//				Rectangle controlBounds=GetCellBounds(bounds);
//				edit.Bounds =controlBounds;
//				edit.Location = new Point(controlBounds.X, controlBounds.Y);
//		
//				edit.Visible = true;
//				DataGridTableStyle.DataGrid.Invalidate(bounds);
//				//edit.TextAlignment  =Alignment.Center; 
//				edit.BringToFront();
//				edit.Focus();
//	
//				//edit.ShowPopUp ();
//				if (cursorBounds.IntersectsWith(controlBounds)) 
//				{
//					edit.DroppedDown = true;
//				}
//	
//			} 
//			else 
//			{
//				edit.Visible = false;
//			}
//
//			mPrevRow = rowNum;
//		}
//
//
//		protected override void Paint(System.Drawing.Graphics g, System.Drawing.Rectangle bounds, CurrencyManager source, int rowNum, System.Drawing.Brush backBrush, System.Drawing.Brush foreBrush, bool alignToRight) 
//		{
//			try 
//			{
//				g.FillRectangle(backBrush, bounds);
//				Rectangle controlBounds = this.GetCellBounds(bounds);
//				Rectangle focusBounds = controlBounds;
//				focusBounds.Inflate(-2, -2);
//				System.Drawing.RectangleF BoundsF = new System.Drawing.RectangleF(((float)(bounds.X)), ((float)(bounds.Y)), ((float)(bounds.Width)), ((float)(bounds.Height)));
//				BoundsF.Inflate(-3, -3);
//				ButtonState bs = m_ButtonStyle;
//		
//				ControlPaint.DrawButton(g, controlBounds, bs);
//				ControlPaint.DrawBorder(g, controlBounds, BorderColor, ButtonBorderStyle.Solid);
//				g.DrawString(m_Caption, this.DataGridTableStyle.DataGrid.Font, foreBrush, BoundsF, stringFormat);
//
//			}
//			catch  
//			{
//				throw new Exception("Error In Button Menu");
//			}
//		}

		#endregion

//		[Category("Style")]
//		public  mControl.WinCtl.Controls.Styles Style
//		{
//			get {return edit.StyleCtl.StylePlan;}
//			//set{edit.StyleCtl.StylePlan =value;} 
//		}

	}

}