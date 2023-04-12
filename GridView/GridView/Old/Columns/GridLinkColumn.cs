using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing ;
using System.Reflection;
using mControl.WinCtl.Controls;

using mControl.GridStyle.Controls ;

namespace mControl.GridStyle.Columns 
{
    
	public class GridLinkColumn : GridColumnStyle 
	{

		#region Members
		// Fields
		private GridLinkLabel  edit;
		private Color linkColor; 

		//public event EventHandler LinkClicked;
		//public event mControl.WinCtl.Controls.LinkClickEventHandler LinkClicked;

		#endregion

		#region Constructors

		public GridLinkColumn() : this((PropertyDescriptor) null, (string) null)
		{
			InitColumn();
		}
		public GridLinkColumn(PropertyDescriptor prop) : this(prop, null, false)
		{
			InitColumn();
		}
		public GridLinkColumn(PropertyDescriptor prop, bool isDefault) : this(prop, null, isDefault)
		{
			InitColumn();
		}
		public GridLinkColumn(PropertyDescriptor prop, string format) : this(prop, format, false)
		{
			InitColumn();
			this.Format = format;
		}
		public GridLinkColumn(PropertyDescriptor prop, string format, bool isDefault) : base(prop, isDefault)
		{
			InitColumn();
			this.Format = format;
		}
		private void InitColumn() 
		{
			linkColor=Color.Blue;
			this.edit = new GridLinkLabel();
			this.edit.BorderStyle = BorderStyle.None;
			//this.edit.Multiline = true;
			this.edit.Visible = false;
			m_ColumnType = ColumnTypes.LinkColumn ;
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
			bool isMouseInRect=false;

			this.edit.ReadOnly = (readOnly || this.ReadOnly) || this.DataGridTableStyle.ReadOnly;
			this.edit.Text = this.GetText(this.GetColumnValueAtRow(source, rowNum));
			if (!this.edit.ReadOnly && (instantText != null))
			{
				((CtlGrid)this.DataGridTableStyle.DataGrid).ColumnStartedEditing(bounds);
				this.edit.IsInEditOrNavigateMode = false;
				this.edit.Text = instantText;
			}
			if (cellIsVisible)
			{
				bounds.Offset(this.xMargin, 2 * this.yMargin);
				bounds.Width -= this.xMargin;
				bounds.Height -= 2 * this.yMargin;

				Point p  = this.DataGridTableStyle.DataGrid.PointToClient(Cursor.Position);
				Rectangle cursorBounds = new Rectangle(p.X, p.Y, 1, 1);

				isMouseInRect= cursorBounds.IntersectsWith(bounds); 

				this.DebugOut("edit bounds: " + bounds.ToString());
				this.edit.Bounds = bounds;
				this.edit.BackColor = this.DataGridTableStyle.BackColor; 
				this.edit.Visible = true;
				this.edit.TextAlign = NativeMethods.GetAlignment (this.Alignment);
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
			if (instantText == null)
			{
				//this.edit.SelectAll();
			}
			else
			{
				int num1 = this.edit.Text.Length;
				//this.edit.SelectInternal(num1, 0);
			}
			if (this.edit.Visible && isMouseInRect)
			{
				this.Invalidate();
				Application.DoEvents();

				this.DataGridTableStyle.DataGrid.Invalidate(rectangle1);
				edit.LinkTargetInternal (edit.Text);// new mControl.WinCtl.Controls.LinkClickEvent (edit.Text) );
				grid.OnButtonClicked(this,this.DataGridTableStyle, rowNum, this.CurrentCol()); 
				//if(grid.CellClicked!=null)
				//	grid.CellClicked(this,new CellClickEventArgs (this.DataGridTableStyle,rowNum,this.CurrentCol()));
				//if(LinkClicked!=null)
				//	LinkClicked(this,new mControl.WinCtl.Controls.LinkClickEvent (edit.Text));//EventArgs.Empty); 
				HideEditBox();
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
			g.FillRectangle(backBrush, rectangle1);
			rectangle1.Offset(0, 2 * this.yMargin);
			rectangle1.Height -= 2 * this.yMargin;
			Font linkFont = new Font(this.DataGridTableStyle.DataGrid.Font, FontStyle.Underline);
		
		
			g.DrawString(text, linkFont, new SolidBrush(linkColor), (RectangleF) rectangle1, format1);
			format1.Dispose();
		}

 
		protected  override void ReleaseHostedControl()
		{
			if (this.edit.Parent != null)
			{
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

		internal  override bool KeyPress(int rowNum, Keys keyData)
		{
			if (this.edit.IsInEditOrNavigateMode)
			{
				return base.KeyPress(rowNum, keyData);
			}
			return false;
		}

		#endregion

		#region Properties

		[Browsable(false)]
		public virtual GridLinkLabel   LinkLabel
		{
			get
			{
				return this.edit;
			}
		}

  
		[DefaultValue("")]
		public override string NullText 
		{
			get{return base.NullText;}
			set{base.NullText = GetFormatText(value);}
		}
    
		#endregion

//		[Category("Style")]
//		public  mControl.WinCtl.Controls.Styles Style
//		{
//			get {return edit.StyleCtl.StylePlan;}
//			//set{edit.StyleCtl.StylePlan =value;} 
//		}

	}
}

