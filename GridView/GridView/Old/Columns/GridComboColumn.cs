using System;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing ;
using System.Reflection;
 
using mControl.WinCtl.Controls;
using mControl.Data;
using mControl.Util;
using mControl.GridStyle;

using mControl.GridStyle.Controls;
 
namespace mControl.GridStyle.Columns 
{
 
	public class GridComboColumn : GridColumnStyle 
	{

		#region Members
		// Fields
		private GridStyle.Controls.GridComboBox  edit;

//		public event CellValidatingEventHandler CellValidating;
//		public event EventHandler CellValidated;

		#endregion

		#region Constructors
		public GridComboColumn() : this((PropertyDescriptor) null, (string) null)
		{
			InitColumn();
		}
		public GridComboColumn(PropertyDescriptor prop) : this(prop, null, false)
		{
			InitColumn();
		}
		public GridComboColumn(PropertyDescriptor prop, bool isDefault) : this(prop, null, isDefault)
		{
			InitColumn();
		}
		public GridComboColumn(PropertyDescriptor prop, string format) : this(prop, format, false)
		{
			InitColumn();
			this.Format = format;
		}
		public GridComboColumn(PropertyDescriptor prop, string format, bool isDefault) : base(prop, isDefault)
		{
			InitColumn();
			this.Format = format;
		}
		private void InitColumn() 
		{
			this.edit = new GridComboBox();
			this.edit.Visible = false;
			m_ColumnType = ColumnTypes.ComboColumn ;
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
					object obj1 =GetValue();// this.edit.Text;
					if (obj1.Equals(Convert.DBNull))
					{
						obj1 =this.NullText;// Convert.DBNull;
						//this.SetValue (this.NullText);// this.edit.Text = this.NullText;
					}
//					else if (((this.format != null) && (this.format.Length != 0)) && ((this.m_parseMethod != null) && (this.FormatInfo != null)))
//					{
//						obj1 = this.m_parseMethod.Invoke(null, new object[] { this.edit.Text, this.FormatInfo });
//						if (obj1 is IFormattable)
//						{
//							this.edit.Text = ((IFormattable) obj1).ToString(this.format, this.m_formatInfo);
//						}
//						else
//						{
//							this.edit.Text = obj1.ToString();
//						}
//					}
//					else if ((this.m_typeConverter != null) && this.m_typeConverter.CanConvertFrom(typeof(string)))
//					{
//						obj1 = this.m_typeConverter.ConvertFromString(this.edit.Text);
//						//this.edit.Text = this.m_typeConverter.ConvertToString(obj1);
//						this.SetValue(obj1);  
//					}
					//this.SetValue(obj1);  
					//this.SetColumnValueAtRow(dataSource, rowNum, obj1);

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
					this.SetColumnValueAtRow(dataSource, rowNum, obj1);
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
			this.edit.ClearSelected();
			//this.edit.Text = this.GetText(this.GetColumnValueAtRow(source, rowNum));
            //this.edit.Text =this.GetDisplyValue (this.GetColumnValueAtRow(source, rowNum));
			object val ="";
			this.edit.editingPosition=false;
			
			if(this.edit.DroppedDown)
			{
				val=GetValue();//this.edit.Text;
			}
			else
			{
				val =this.GetColumnValueAtRow(source, rowNum);//this.GetDisplyValue (this.GetColumnValueAtRow(source, rowNum));
			}
			if (!this.edit.ReadOnly && (instantText != null))
			{
				((CtlGrid)this.DataGridTableStyle.DataGrid).ColumnStartedEditing(bounds);
				this.edit.IsInEditOrNavigateMode = false;
				//this.edit.Text = instantText;
                //SetValue(instantText); 
				val=instantText;
			}
			if (cellIsVisible)
			{
				bounds.Offset(this.xMargin, 2 * this.yMargin);
				bounds.Width -= this.xMargin;
				bounds.Height -= 2 * this.yMargin;
				this.DebugOut("edit bounds: " + bounds.ToString());
				this.edit.Bounds = bounds;
				this.edit.CloseDropDown (); 
				//this.edit.Width=bounds.Width;
				this.edit.Visible = true;
	
				//this.edit.Text =val;
				//SetValue(val);
				this.edit.Text=GetDisplyValue(val);
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

		[DefaultValue("")]
		public override string NullText 
		{
			get{return base.NullText;}
			set{base.NullText = GetFormatText(value);}
		}
    

		[Browsable(false)]
		public virtual GridComboBox ComboBox
		{
			get
			{
				return this.edit;
			}
		}
		#endregion

		#region Combo Property

		[Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor)), Description("ComboBoxItemsDescr"), Category("Data"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Localizable(true)]
		public  mControl.WinCtl.Controls.CtlListCombo.ObjectCollection Items 
		{
			get { return edit.Items; }
			//set 
			//{ //mItems = value; 
			//	edit.Items.AddRange (value);}
		}

		[DefaultValue((string) null), Category("Data"), Description("ListControlDataSourceDescr"), TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), RefreshProperties(RefreshProperties.Repaint)]
		public object DataSource
		{
			get{return edit.DataSource;}
			set
			{
				edit.DataSource = value;
				//edit.sourceContext=base.grid.DataSource;
				//edit.mappingName=base.MappingName;
				this.edit.SetDataView();
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
				//mValueMember = value;
				edit.ValueMember = value;
				//if (!(edit.DataSource == null)) 
				//{
					//edit.DataSource.Sort = value;
				//	this.edit.SetDataView();
				//}
				this.Invalidate();
			}
		}

		[Category("Layout"), DefaultValue(0)]
		public int DropDownWidth
		{
			get{return edit.DropDownWidth;}
			set 
			{
				edit.DropDownWidth = value;
				this.Invalidate();
			}
		}

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
//		[DefaultValue(System.Windows.Forms.ComboBoxStyle.DropDown)]
//		public System.Windows.Forms.ComboBoxStyle DropDownStyle
//		{
//			get{return edit.DropDownStyle;}
//			set{edit.DropDownStyle = value;}
//		}
 
		#endregion

		#region Public Methods

		public void SetDataSource(object dataSource,string valueMember,string displyMember)
		{
			this.DataSource =dataSource;
			this.ValueMember =valueMember;
			this.DisplayMember =displyMember;
		}

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
					string s=edit.GetItemText (colValue);
					if(s==null || s=="")
					  s= this.NullText;
					return s;
				}
			}
			catch  
			{
				throw new Exception("Error Display value in combo box");
				//return NullText;
			}
		}

		private object GetValue() 
		{
			try 
			{
				if(edit.DataSource!=null)
				{
					return edit.SelectedValue;
				}
				else
				{
					return edit.SelectedItem ;
				}
			}
			catch  
			{
				throw new Exception("Error Get value in combo box");
				//return NullText;
			}
		}

		private void SetValue(object colValue) 
		{
			try 
			{
				if (colValue==null) 
				{
					return;
				}
				if(edit.DataSource!=null)
				{
					edit.SelectedValue=colValue;
				}
				else
				{
					edit.SelectedItem=colValue ;
				}
			}
			catch  
			{
				throw new Exception("Error Display value in combo box");
				//return NullText;
			}
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