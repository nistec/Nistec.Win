using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections;
using mControl.WinCtl.Controls;
using mControl.Util ;

using mControl.GridStyle.Controls;

  
namespace mControl.GridStyle.Columns 
{

	public class GridMultiColumn : GridColumnStyle
	{

		#region Members
		// Fields

		//private string m_ValueMember;
		//private string m_DisplayMember;
		//private System.Data.DataView m_DataSource;
		private GridEnumColumn MultiEnumCol;
	
		private GridStyle.Controls.GridMultiBox    edit;

//		public event CellValidatingEventHandler CellValidating;
//		public event EventHandler CellValidated;

		#endregion

		#region Constructors
		public GridMultiColumn() : this((PropertyDescriptor) null, (string) null)
		{
			InitColumn();
		}
		public GridMultiColumn(PropertyDescriptor prop) : this(prop, null, false)
		{
			InitColumn();
		}
		public GridMultiColumn(PropertyDescriptor prop, bool isDefault) : this(prop, null, isDefault)
		{
			InitColumn();
		}
		public GridMultiColumn(PropertyDescriptor prop, string format) : this(prop, format, false)
		{
			InitColumn();
			this.Format = format;
		}
		public GridMultiColumn(PropertyDescriptor prop, string format, bool isDefault) : base(prop, isDefault)
		{
			InitColumn();
			this.Format = format;
		}
		private void InitColumn() 
		{
			this.edit = new GridMultiBox();
			this.edit.Visible = false;
			m_ColumnType = ColumnTypes.MultiColumn ;
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
			if (!this.edit.IsInEditOrNavigateMode)
			{
				try
				{
					object obj1 =GetValue();// this.edit.Text;
					if (this.NullText.Equals(obj1))
					{
						obj1 = Convert.DBNull;
						this.SetValue (this.NullText);// this.edit.Text = this.NullText;
					}
					else if (((this.m_Format!= null) && (this.m_Format.Length != 0)) && ((this.m_parseMethod != null) && (this.FormatInfo != null)))
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
						//this.edit.Text = this.m_typeConverter.ConvertToString(obj1);
						this.SetValue(obj1);  
					}
					this.SetValue(obj1);  
					this.SetColumnValueAtRow(dataSource, rowNum, obj1);

					if ((!IsValid(obj1))) 
					{
						Abort(rowNum);
						return false;
					}
					if(!OnCellValidating(rowNum,obj1))//!=null)
					{
						Abort(rowNum);
						return false;
					}
					object oldVal=this.GetColumnValueAtRow (dataSource, rowNum);
					this.SetColumnValueAtRow(dataSource, rowNum, obj1);
					if(m_AggregateMode!=AggregateMode.None)//(m_IsSum)
					{
						RunSum(oldVal,obj1);
					}
					OnCellValidated();  
				}
				catch (Exception)
				{
					this.RollBack();
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
			
			if (MultiEnumCol!=null)//String.Empty )
			{
				SetMultiEnumColumn();
			}

			
			//this.edit.Text = this.GetText(this.GetColumnValueAtRow(source, rowNum));
			this.edit.Text =this.GetDisplyValue (this.GetColumnValueAtRow(source, rowNum));
			if (!this.edit.ReadOnly && (instantText != null))
			{
				((CtlGrid)this.DataGridTableStyle.DataGrid).ColumnStartedEditing(bounds);
				this.edit.IsInEditOrNavigateMode = false;
				//this.edit.Text = instantText;
				SetValue(instantText); 
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

		//		protected override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum)
		//		{
		//			this.Paint(g, bounds, source, rowNum, false);
		//		}

		protected override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum, bool alignToRight)
		{
			//string text1 = this.GetText(this.GetColumnValueAtRow(source, rowNum));
			string text1=this.GetDisplyValue(this.GetColumnValueAtRow(source, rowNum));
			this.PaintText(g, bounds, text1, alignToRight);
		}

 
		protected override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum, Brush backBrush, Brush foreBrush, bool alignToRight)
		{
			//string text1 = this.GetText(this.GetColumnValueAtRow(source, rowNum));
			string text1=this.GetDisplyValue(this.GetColumnValueAtRow(source, rowNum));
			this.PaintText(g, bounds, text1, backBrush, foreBrush, alignToRight);
		}

 
		protected override void PaintText(Graphics g, Rectangle bounds, string text, bool alignToRight)
		{
			this.PaintText(g, bounds, text, (SolidBrush) SystemBrushes.Window, (SolidBrush) SystemBrushes.WindowText , alignToRight);
			//this.PaintText(g, bounds, text, this.DataGridTableStyle.BackBrush, this.DataGridTableStyle.ForeBrush, alignToRight);
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
			//this.edit.StyleCtl.StylePlan =((CtlGrid)value).interanlGrid.CtlStyleLayout.StylePlan;   
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

		[DefaultValue(true)]
		public override bool AllowNull 
		{
			get{return m_AllowNull;}
			set{m_AllowNull = value;}
		}

//		[DefaultValue(false)]
//		public new bool ShowSum 
//		{
//			get{return m_IsSum;}
//			set
//			{
//				if(this.m_DataType==DataTypes.Number)    
//					m_IsSum = value;
//				else 
//					m_IsSum =false;
//				this.Invalidate (); 
//			}
//		}

		[DefaultValue("")]
		public override string NullText 
		{
			get{return base.NullText;}
			set{base.NullText = GetFormatText(value);}
		}
    

		[Browsable(false)]
		public virtual GridMultiBox MultiBox
		{
			get
			{
				return this.edit;
			}
		}
		#endregion

		#region Combo Property

		[Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor)), Description("ComboBoxItemsDescr"), Category("Data"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Localizable(true)]
		public  ArrayList Items 
		{
			get { return edit.Items ; }
			set 
			{ //mItems = value; 
				edit.Items.AddRange (value);}
		}

		[DefaultValue((string) null), Category("Data"), Description("ListControlDataSourceDescr"), TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), RefreshProperties(RefreshProperties.Repaint)]
		public object DataSource
		{
			get{return edit.DataSource;}
			set
			{
				edit.DataSource = value;
				this.Invalidate();
			}
		}
        
		[Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor)), Category("Data"), TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), DefaultValue(""), Description("ListControlDisplayMemberDescr")]
		public string DisplayMember
		{
			get{return edit.DisplayMember;}
			set 
			{
				edit.DisplayMember = value;
				this.Invalidate();
			}
		}
        
		[DefaultValue(""), Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor)), Category("Data"), Description("ListControlValueMemberDescr")]
		public string ValueMember
		{
			get{return edit.ValueMember;}
			set 
			{
				edit.ValueMember = value;
				this.Invalidate();
			}
		}

		public GridEnumColumn MultiEnumColumn 
		{
			get{return MultiEnumCol;}
			set 
			{
				MultiEnumCol = value;
				this.Invalidate();
			}
		}

		[DefaultValue(MultiComboTypes.Text)]
		public  MultiComboTypes MultiType 
		{
			get{return edit.MultiType;}
			set 
			{
				//mCommandType = value;
				edit.MultiType   = value;
				this.Invalidate();
			}
		}

//		[Category("Layout"), DefaultValue(0)]
//		public int DropDownWidth
//		{
//			get{return edit.DropDownWidth;}
//			set 
//			{
//				edit.DropDownWidth = value;
//				this.Invalidate();
//			}
//		}

		[Category("Layout"), DefaultValue(0)]
		public ComboStyles ComboStyle
		{
			get{return  edit.ComboStyle;}
			set 
			{
				edit.ComboStyle = value;
				this.Invalidate();
			}
		}

		#endregion

		#region Public Methods

//		public void SetDataSource(object dataSource,string valueMember,string displyMember)
//		{
//			this.DataSource =dataSource;
//			this.ValueMember =valueMember;
//			this.DisplayMember =displyMember;
//		}

		#endregion

		#region Combo Methods

		private string GetDisplyValue(object colValue) 
		{
			try 
			{
				if (colValue==null) 
				{
					return NullText;
				}
				else
				{
					object itm= edit.GetItemText (colValue);
					if(itm==null)
						return "";
					return itm.ToString();
				}
			}
			catch  
			{
				throw new Exception("Error Display value in Multi box");
				//return NullText;
			}
		}

		private object GetValue() 
		{
			try 
			{
				if(edit.MultiType ==MultiComboTypes.Combo )
				{
//					if(edit.DataSource!=null)
//					{
//						return edit.SelectedValue;
//					}
//					else
//					{
						return edit.SelectedItem ;
					//}
				}
				else
					return edit.Text; 
			}
			catch  
			{
				throw new Exception("Error Get value from Multi box");
				//return NullText;
			}
		}

		private void SetValue(object colValue) 
		{
			try 
			{
				if(this.edit.MultiType ==MultiComboTypes.Combo )
				{
					if (colValue==null) 
					{
						return;
					}
//					if(edit.DataSource!=null)
//					{
//						edit.SelectedValue=colValue;
//					}
//					else
//					{
						edit.SelectedItem=colValue ;
					//}
				}
				else
					edit.Text =colValue.ToString ();
			}
			catch  
			{
				throw new Exception("Error Display value in Multi box");
				//return NullText;
			}
		}

		private void SetMultiEnumColumn()
		{
			if (MultiEnumCol!=null)//String.Empty )
			{
				//string col=this.GetDataView().Table.Rows [rowNum][MultiEnumCol].ToString ();
				//string col=this.GetDataView().Table.Rows [rowNum][MultiEnumCol.MappingName].ToString ();
				object col=MultiEnumCol.GetControlvalue();
            	if(col==null)
					return;
               int multiType=Util.Types.StringToInt(col.ToString(),0);//  Regx.ParseInt(col,0);
			   edit.MultiType =(MultiComboTypes)multiType;
			}
		}

		#endregion

	}

}	