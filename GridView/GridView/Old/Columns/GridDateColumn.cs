using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using System;
using System.ComponentModel;
using System.Globalization;
using mControl.WinCtl.Controls;
using mControl.Util;

using mControl.GridStyle.Controls; 
   
namespace mControl.GridStyle.Columns 
{
	public class GridDateColumn : GridColumnStyle 
	{

		#region Members
		// Fields
		private GridStyle.Controls.GridDatePicker edit;

		//private RangeDate m_RangeValue=new RangeDate (); 
		private	DateTimeFormatInfo dateFormat = CultureInfo.CurrentCulture.DateTimeFormat;				

//		public event CellValidatingEventHandler CellValidating;
//		public event EventHandler CellValidated;

		#endregion

		#region Constructors
		public GridDateColumn() : this((PropertyDescriptor) null, (string) null)
		{
			InitColumn();
		}
		public GridDateColumn(PropertyDescriptor prop) : this(prop, null, false)
		{
			InitColumn();
		}
		public GridDateColumn(PropertyDescriptor prop, bool isDefault) : this(prop, null, isDefault)
		{
			InitColumn();
		}
		public GridDateColumn(PropertyDescriptor prop, string format) : this(prop, format, false)
		{
			InitColumn();
			this.Format = format;
		}
		public GridDateColumn(PropertyDescriptor prop, string format, bool isDefault) : base(prop, isDefault)
		{
			InitColumn();
			this.Format = format;
		}
		private void InitColumn() 
		{
	        this.m_DataType =DataTypes.Date;  
			this.edit = new GridDatePicker();
			this.edit.Visible = false;
			this.edit.Visible = false;
			m_ColumnType = ColumnTypes.DateTimeColumn  ;
			hostedCtl =this.edit;
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
			if (!this.edit.IsInEditOrNavigateMode)
			{
				try
				{
					object obj1 = this.edit.Text;
					if (this.NullText.Equals(obj1))
					{
						obj1 = Convert.DBNull;
						this.edit.Text = this.NullText;
					}
					else if (((this.m_Format != null) && (this.m_Format.Length != 0)) && ((this.m_parseMethod != null) && (this.FormatInfo != null)))
					{
						obj1 = this.m_parseMethod.Invoke(null, new object[] { this.edit.Text, this.FormatInfo });
						if (obj1 is IFormattable)
						{
							this.edit.Text = ((IFormattable) obj1).ToString(this.m_Format, this.m_formatInfo);
						}
						else
						{
							this.edit.Text = obj1.ToString();
						}
					}
					else if ((this.m_typeConverter != null) && this.m_typeConverter.CanConvertFrom(typeof(string)))
					{
						obj1 = this.m_typeConverter.ConvertFromString(this.edit.Text);
						this.edit.Text = this.m_typeConverter.ConvertToString(obj1);
					}
					if ((!IsValid( obj1))) 
					{
						Abort(rowNum);
						return false;
					}
					if(!OnCellValidating(rowNum,obj1))//!=null)
					{
						Abort(rowNum);
						return false;
					}

					this.SetColumnValueAtRow(dataSource, rowNum, obj1);
					OnCellValidated();  
			
				}
				catch (Exception)
				{
					this.RollBack();
					NativeMethods.ErrMsg (RM.GetString(RM.ErrorNotExpected ));
					return false;
				}
				this.DebugOut("OnCommit completed without Exception.");
				this.EndEdit();
			}
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
			string s = this.GetText(this.GetColumnValueAtRow(source, rowNum));
//			if(mControl.Util.Info.IsDateTime(s))
//			{
//				this.edit.Text =s;// this.GetText(this.GetColumnValueAtRow(source, rowNum));
//			}
			try
			{
				this.edit.Text =s;// this.GetText(this.GetColumnValueAtRow(source, rowNum));
			}
			catch{}
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
				this.DebugOut("edit bounds: " + bounds.ToString());
				this.edit.Bounds = bounds;
				this.edit.CloseDropDown (); 
				this.edit.Visible = true;
				this.edit.TextAlign = this.Alignment;
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
				this.edit.SelectAll();
			}
			else
			{
				int num1 = this.edit.Text.Length;
				this.edit.SelectInternal(num1, 0);
			}
			if (this.edit.Visible)
			{
				this.DataGridTableStyle.DataGrid.Invalidate(rectangle1);
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
			g.DrawString(text, this.DataGridTableStyle.DataGrid.Font, foreBrush, (RectangleF) rectangle1, format1);
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
		public virtual GridDatePicker DatePicker
		{
			get
			{
				return this.edit;
			}
		}

		[DefaultValue(true)]
		public override bool AllowNull 
		{
			get{return m_AllowNull;}
			set{m_AllowNull = value;}
		}

		[Browsable(true),DefaultValue("d")]
		public override string Format 
		{
			get	{return base.Format;}
			set
			{
				if(value==null)
					value="";
				base.Format=value;
				edit.Format =value;
			} 
		}

		[DefaultValue( Formats.None )]
		public RangeDate RangeValue 
		{
			get	{return edit.RangeValue ;}
			set
			{
				edit.RangeValue =value;
				this.Invalidate (); 
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
//		public  Styles Style
//		{
//			get {return edit.StyleCtl.StylePlan;}
//			//set{edit.StyleCtl.StylePlan =value;} 
//		}
	}
}

