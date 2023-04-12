using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing.Design;
using System.Collections;

using mControl.Util;
using mControl.Win32;
using mControl.WinCtl.Controls;


namespace mControl.GridView
{
    /// <summary>
    /// Hosts a Grid relation control in a cell of a GridColumnStyle for editing relation grid
    /// </summary>
	public class GridRelationColumn : GridColumnStyle
	{

		#region Fields

        private Grid edit;
		private string oldValue;
		private MethodInfo parseMethod;

		private object currentValue;
		private bool alignRight;
		private const int buttonWidth=11;

		#endregion

		#region Ctor
        /// <summary>
        /// collapsed image
        /// </summary>
		internal protected static Image collapsed;
        /// <summary>
        /// expaned image
        /// </summary>
		internal protected static Image expaned;

		static GridRelationColumn()
		{
			GridControlColumn.collapsed =NativeMethods.LoadImage("mControl.GridView.Images.collapsed.gif");
			GridControlColumn.expaned  =NativeMethods.LoadImage ("mControl.GridView.Images.expaned.gif");

		}
        /// <summary>
        /// Initilaizing Grid Control Column
        /// </summary>
		public GridRelationColumn() : this((PropertyDescriptor) null, (string) null)
		{
		}

 
        /// <summary>
        /// nitilaizing Grid Control Column
        /// </summary>
        /// <param name="prop"></param>
		public GridRelationColumn(PropertyDescriptor prop) : this(prop, null, false)
		{
		}
        /// <summary>
        /// nitilaizing Grid Control Column
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="isDefault"></param>
		public GridRelationColumn(PropertyDescriptor prop, bool isDefault) : this(prop, null, isDefault)
		{
		}
        /// <summary>
        /// nitilaizing Grid Control Column
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="format"></param>
		public GridRelationColumn(PropertyDescriptor prop, string format) : this(prop, format, false)
		{
		}
        /// <summary>
        /// nitilaizing Grid Control Column
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="format"></param>
        /// <param name="isDefault"></param>
        public GridRelationColumn(PropertyDescriptor prop, string format, bool isDefault)
            : base(prop, isDefault)
		{
			this.oldValue = null;
			this.edit = new Grid();
			this.edit.Visible = false;
			alignRight=false;
            m_ColumnType = GridColumnType.GridColumn;
            base.m_AllowUnBound = false;
            base.HostControl = this.edit;

			m_Text="";


		}

		#endregion

		#region Methods
        /// <summary>
        /// When overridden in a derived class, initiates a request to interrupt an edit procedure.
        /// </summary>
        /// <param name="rowNum"></param>
		protected internal override void Abort(int rowNum)
		{
			this.isSelected = false;
			this.EndEdit();
		}
        /// <summary>
        /// Notifies a column that it must relinquish the focus to the control it is hosting.
        /// </summary>
		protected internal override void ConcedeFocus()
		{
			this.isSelected = false;
			this.edit.Bounds = Rectangle.Empty;
		}
        /// <summary>
        /// EndEdit
        /// </summary>
		protected void EndEdit()
		{
		}
        /// <summary>
        /// Enter Null Value
        /// </summary>
		protected internal override void EnterNullValue()
		{
		}
        /// <summary>
        /// Commits changes in the current cell ,When overridden in a derived class, initiates a request to complete an editing procedure. 
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="rowNum"></param>
        /// <returns></returns>
		protected internal override bool Commit(BindManager dataSource, int rowNum)
		{
			this.isSelected = false;
			edit.Visible = false;
			return true;
		}

		private void DebugOut(string s)
		{
		}
        /// <summary>
        /// Overloaded. Prepares the cell for editing a value.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="rowNum"></param>
        /// <param name="bounds"></param>
        /// <param name="readOnly"></param>
        /// <param name="instantText"></param>
        /// <param name="cellIsVisible"></param>
		protected internal override void Edit(BindManager source, int rowNum, Rectangle bounds, bool readOnly, string instantText, bool cellIsVisible)
		{
            base.OnCellEdit();
            if (!m_Enabled) return;
            if (this.DataSource == null)
                return;

            this.alignRight = this.GridTableStyle.dataGrid.RightToLeft == RightToLeft.Yes;
            bool isDroppedDown = this.edit.DroppedDown;
            if (isDroppedDown)
            {
                if (rowNum != editRow)
                    return;
                Point p = this.GridTableStyle.dataGrid.PointToClient(Cursor.Position);
                RectangleF cursorBounds = new RectangleF(p.X, p.Y, 1, 1);
                RectangleF controlBounds = GetButtonBounds((RectangleF)bounds, alignRight);

                if (!cursorBounds.IntersectsWith(controlBounds))
                {
                    return;
                }
            }
            base.editRow = rowNum;
            base.cellBounds = bounds;
            //edit.curVScroll = this.GridTableStyle.dataGrid.VertScrollBar.Value;
            edit.curRow = rowNum;

			Rectangle rectangle1 = bounds;
			//this.edit.ReadOnly = (readOnly || this.ReadOnly) || this.GridTableStyle.ReadOnly;
			this.currentValue=this.GetColumnValueAtRow(source, rowNum);
			string colValue =this.currentValue!=null? this.currentValue.ToString():"";
			bool isMouseInRect=false;
			this.isSelected = true;

            if (cellIsVisible)
            {
                bounds.Offset(this.xMargin, 2 * this.yMargin);
                bounds.Width -= this.xMargin;
                bounds.Height -= 2 * this.yMargin;
                RectangleF controlBounds = GetButtonBounds((RectangleF)bounds, alignRight);
                //controlBounds.Inflate (-2,-2);

                Point p = this.GridTableStyle.dataGrid.PointToClient(Cursor.Position);
                RectangleF cursorBounds = new RectangleF(p.X, p.Y, 1, 1);

                isMouseInRect = cursorBounds.IntersectsWith(controlBounds);

                //this.DebugOut("edit bounds: " + bounds.ToString());
                this.edit.Bounds = new Rectangle((int)controlBounds.X, (int)controlBounds.Y, (int)controlBounds.Width, (int)controlBounds.Height);
                this.edit.RowFilter = colValue;
                this.edit.Visible = true;
                //this.GridTableStyle.Grid.Invalidate(controlBounds);
            }
            else
            {
                this.edit.Bounds = rectangle1;
                this.edit.Visible = false;
            }
			//this.edit.RightToLeft = this.GridTableStyle.dataGrid.RightToLeft;
            //this.edit.Focus();
            //if (this.edit.Visible && isMouseInRect)
            //{
            //    this.Invalidate();
            //    Application.DoEvents();
            //    this.GridTableStyle.dataGrid.Invalidate(rectangle1);
            //    shouldDropDown = true;//this.edit.DoDropDown();
            //}
 		}

        internal override void MouseUp(int rowNum, MouseEventArgs e)// int x, int y)
        {
            RectangleF controlBounds = GetButtonBounds((RectangleF)cellBounds, alignRight);
            RectangleF cursorBounds = new RectangleF(e.X, e.Y, 1, 1);
            if (cursorBounds.IntersectsWith(controlBounds))
            {
                this.edit.DoDropDown();
            }
        }

 		internal override string GetDisplayText(object value)
		{
			return this.GetText(value);
		}
        /// <summary>
        /// When overridden in a derived class, gets the minimum height of a row
        /// </summary>
        /// <returns></returns>
		protected internal override int GetMinimumHeight()
		{
			return ((base.FontHeight + this.yMargin) + bMargin);
		}
        /// <summary>
        /// When overridden in a derived class, gets the height used for automatically resizing columns.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="value"></param>
        /// <returns></returns>
		protected internal override int GetPreferredHeight(Graphics g, object value)
		{
			return ((base.FontHeight + this.yMargin) + bMargin);
		}
        /// <summary>
        /// When overridden in a derived class, gets the width and height of the specified value. The width and height are used when the user navigates to GridTable using the GridColumnStyle
        /// </summary>
        /// <param name="g"></param>
        /// <param name="value"></param>
        /// <returns></returns>
		protected internal override Size GetPreferredSize(Graphics g, object value)
		{
			Size size1 = Size.Ceiling(g.MeasureString(this.GetText(value), this.GridTableStyle.Grid.Font));
			size1.Width += (this.xMargin * 2) + this.GridTableStyle.dataGrid.GridLineWidth;
			size1.Height += this.yMargin+bMargin;
			return size1;
		}

		private string GetText(object value)
		{
			if (value is DBNull)
			{
				return this.NullText;
			}
			if (((this.format != null) && (this.format.Length != 0)) && (value is IFormattable))
			{
				try
				{
					return ((IFormattable) value).ToString(this.format, this.formatInfo);
				}
				catch (Exception)
				{
					goto Label_0084;
				}
			}
			if ((this.typeConverter != null) && this.typeConverter.CanConvertTo(typeof(string)))
			{
				return (string) this.typeConverter.ConvertTo(value, typeof(string));
			}
			Label_0084:
				if (value == null)
				{
					return "";
				}
			return value.ToString();
		}

        /// <summary>
        /// Hide Edit Box
        /// </summary>
		protected void HideEditBox()
		{
			bool flag1 = this.edit.Focused;
			this.edit.Visible = false;
			if ((flag1 && (this.GridTableStyle != null)) && ((this.GridTableStyle.Grid != null) && this.GridTableStyle.Grid.CanFocus))
			{
				this.GridTableStyle.Grid.FocusInternal();
			}
		}

 
		internal override bool KeyPress(int rowNum, Keys keyData)
		{
//			if (this.edit.IsInEditOrNavigateMode)
//			{
//				return base.KeyPress(rowNum, keyData);
//			}
			return false;
		}
        /// <summary>
        /// Overloaded. When overridden in a derived class, paints the column in a Grid control. 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="bounds"></param>
        /// <param name="source"></param>
        /// <param name="rowNum"></param>
		protected internal override void Paint(Graphics g, Rectangle bounds, BindManager source, int rowNum)
		{
			this.Paint(g, bounds, source, rowNum, false);
		}
        /// <summary>
        /// Overloaded. When overridden in a derived class, paints the column in a Grid control. 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="bounds"></param>
        /// <param name="source"></param>
        /// <param name="rowNum"></param>
        /// <param name="alignToRight"></param>
		protected internal override void Paint(Graphics g, Rectangle bounds, BindManager source, int rowNum, bool alignToRight)
		{
			this.Paint(g, bounds, source, rowNum,GridTableStyle.dataGrid.BackBrush,GridTableStyle.dataGrid.ForeBrush, alignToRight);
		}
        /// <summary>
        /// Overloaded. When overridden in a derived class, paints the column in a Grid control. 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="bounds"></param>
        /// <param name="source"></param>
        /// <param name="rowNum"></param>
        /// <param name="backBrush"></param>
        /// <param name="foreBrush"></param>
        /// <param name="alignToRight"></param>
		protected internal override void Paint(Graphics g, Rectangle bounds, BindManager source, int rowNum, Brush backBrush, Brush foreBrush, bool alignToRight)
		{

			try 
			{
				Rectangle rectangle1 = bounds;

				//Rectangle controlBounds=new Rectangle(bounds.X+2,bounds.Y+2,buttonWidth,buttonWidth);//, this.Width-1, this.Height-1);

				RectangleF controlBounds = GetButtonBounds(bounds,alignToRight);
				Rectangle textRect =bounds;
				textRect.Width-=(int)(controlBounds.Width+2);
		
			
				if (alignToRight)
				{
					//format1.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
					textRect.X= bounds.X-2;// bounds.Width -controlBounds.Width -2;
				}
				else
				{
					textRect.X=(int) (controlBounds.X +controlBounds.Width+2);
				}

				if (this.GridTableStyle.dataGrid.SelectionType==SelectionType.FullRow && this.isSelected )
				{
					backBrush =this.GridTableStyle.dataGrid.BackBrush;
					foreBrush =this.GridTableStyle.dataGrid.ForeBrush;
				}

				g.FillRectangle(backBrush, bounds);
				//ButtonState bs = m_ButtonStyle;

//				using(Brush sb1=GridTableStyle.dataGrid.CtlStyleLayout.GetBrushBack())// new SolidBrush (m_BackColor))
//				{
//					//g.FillRectangle(sb1,controlBounds );
//				}
//				using(Pen p=GridTableStyle.dataGrid.CtlStyleLayout.GetPenBorder())// new Pen (m_BorderColor))
//				{
//					//g.DrawRectangle (p,controlBounds );
//				}

				//g.DrawString(m_Caption, this.GridTableStyle.Grid.Font, foreBrush, (RectangleF) controlBounds , format2);
				//this.edit.DrawImage(g);
				//if(this.editRow != rowNum)
				if(this.DataSource!=null)
				{
                    if (this.editRow == rowNum && edit.DroppedDown)
                        g.DrawImage(GridControlColumn.expaned, (RectangleF)controlBounds);  
                    else
                        g.DrawImage(GridControlColumn.collapsed, (RectangleF)controlBounds);  
				}
				string text1 =this.m_Text;
				if(text1.Length==0)
				{
					text1 = this.GetText(this.GetColumnValueAtRow(source, rowNum));
				}
				this.PaintText(g, textRect, text1, backBrush, foreBrush, alignToRight);
			}
			catch  
			{
				throw new Exception("Error In Grid Column column");
			}
	
		}

 
        /// <summary>
        /// PaintText
        /// </summary>
        /// <param name="g"></param>
        /// <param name="textBounds"></param>
        /// <param name="text"></param>
        /// <param name="backBrush"></param>
        /// <param name="foreBrush"></param>
        /// <param name="alignToRight"></param>
		protected void PaintText(Graphics g, Rectangle textBounds, string text, Brush backBrush, Brush foreBrush, bool alignToRight)
		{

		
			Rectangle rectangle1 = textBounds;
	
			StringFormat format1 = new StringFormat();
			if (alignToRight)
			{
				format1.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
				//rectangle1.X= textBounds.X-2;
			}
			else
			{
				//rectangle1.X+= textBounds.Width+2;
			}
			format1.Alignment = (this.Alignment == HorizontalAlignment.Left) ? StringAlignment.Near : ((this.Alignment == HorizontalAlignment.Center) ? StringAlignment.Center : StringAlignment.Far);
			format1.FormatFlags |= StringFormatFlags.NoWrap;
			//g.FillRectangle(backBrush, rectangle1);
			rectangle1.Offset(0, 2 * this.yMargin);
			rectangle1.Height -= 2 * this.yMargin;
			g.DrawString(text, this.GridTableStyle.Grid.Font, foreBrush, (RectangleF) rectangle1, format1);
			format1.Dispose();
		}

 
		private RectangleF GetButtonBounds(RectangleF bounds, bool alignToRight)
		{

			if (alignToRight)
			{
				return new RectangleF(bounds.X + bounds.Width -buttonWidth-2,bounds.Y+1.5f,buttonWidth ,buttonWidth);
				//return new Rectangle(bounds.X + bounds.Width -buttonWidth-2,bounds.Y+1,buttonWidth ,bounds.Height-2);
			}
			return new RectangleF(bounds.X,bounds.Y+1.5f,buttonWidth,buttonWidth);//, this.Width-1, this.Height-1);
			//return new Rectangle(bounds.X + 1,bounds.Y+1,buttonWidth ,bounds.Height-2);
		}

		private Rectangle GetButtonBounds(Rectangle bounds, bool alignToRight)
		{
			//Rectangle controlBounds=new Rectangle(bounds.X+2,bounds.Y+2,buttonWidth,buttonWidth);//, this.Width-1, this.Height-1);

			if (alignToRight)
			{
				return new Rectangle(bounds.X + bounds.Width -buttonWidth-2,bounds.Y+2,buttonWidth ,buttonWidth);
				//return new Rectangle(bounds.X + bounds.Width -buttonWidth-2,bounds.Y+1,buttonWidth ,bounds.Height-2);
			}
			return new Rectangle(bounds.X+2,bounds.Y+2,buttonWidth,buttonWidth);//, this.Width-1, this.Height-1);
			//return new Rectangle(bounds.X + 1,bounds.Y+1,buttonWidth ,bounds.Height-2);
		}

        /// <summary>
        /// Release Hosted Control
        /// </summary>
		protected internal override void ReleaseHostedControl()
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

 
        /// <summary>
        /// Set Grid In Column
        /// </summary>
        /// <param name="value"></param>
		protected override void SetGridInColumn(Grid value)
		{
			base.SetGridInColumn(value);
			if (this.edit.Parent != null)
			{
				this.edit.Parent.Controls.Remove(this.edit);
			}
			if (value != null)
			{
				value.Controls.Add(this.edit);
			}
			this.edit.Init(value);
		}
        /// <summary>
        /// Updates the value of a specified row with the given text.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="rowNum"></param>
        /// <param name="instantText"></param>
		protected internal override void UpdateUI(BindManager source, int rowNum, string instantText)
		{
			this.edit.Text = this.GetText(this.GetColumnValueAtRow(source, rowNum));
//			if (!this.edit.ReadOnly && (instantText != null))
//			{
//				this.edit.Text = instantText;
//			}
		}
        /// <summary>
        /// Allows the column to free resources when the control it hosts is not needed.
        /// </summary>
        protected internal override void ResetHostControl()
        {
            this.edit.RightToLeft = this.GridTableStyle.Grid.RightToLeft;
            this.edit.ForeColor = this.GridTableStyle.dataGrid.ForeColor;
            this.edit.Font = this.GridTableStyle.dataGrid.Font;
        }

		#endregion
 
		#region Properties

        /// <summary>
        /// Get Control Current Text
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Text
        {
            get { return this.currentValue==null? "":currentValue.ToString() ; }
            set { this.m_Text = value; }
        }

        /// <summary>
        /// Get or Set column format
        /// </summary>
		[Editor("System.Windows.Forms.Design.GridColumnStyleFormatEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string Format
		{
			get
			{
				return this.format;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				if ((this.format == null) || !this.format.Equals(value))
				{
					this.format = value;
					if (((this.format.Length == 0) && (this.typeConverter != null)) && !this.typeConverter.CanConvertFrom(typeof(string)))
					{
						this.ReadOnly = true;
					}
					this.Invalidate();
				}
			}
		}
 
        /// <summary>
        /// Get or set column format info
        /// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced),DefaultValue(null)]
		public IFormatProvider FormatInfo
		{
			get
			{
				return this.formatInfo;
			}
			set
			{
				if ((this.formatInfo == null) || !this.formatInfo.Equals(value))
				{
					this.formatInfo = value;
				}
			}
		}
        /// <summary>
        /// Set column property descriptor
        /// </summary>
		[DefaultValue((string) null), Description("FormatControlFormatDescr")]
		public override PropertyDescriptor PropertyDescriptor
		{
			set
			{
				base.PropertyDescriptor = value;
				if ((this.PropertyDescriptor != null) && (this.PropertyDescriptor.PropertyType != typeof(object)))
				{
					this.typeConverter = TypeDescriptor.GetConverter(this.PropertyDescriptor.PropertyType);
					this.parseMethod = this.PropertyDescriptor.PropertyType.GetMethod("Parse", new Type[] { typeof(string), typeof(IFormatProvider) });
				}
			}
		}
 
        /// <summary>
        /// Get or set read only property
        /// </summary>
		public override bool ReadOnly
		{
			get
			{
				return base.ReadOnly;
			}
			set
			{
				//if ((value || !"".Equals(this.format)) || ((this.typeConverter == null) || this.typeConverter.CanConvertFrom(typeof(string))))
				//{
					base.ReadOnly = value;
				    
				    this.edit.InternalGrid.ReadOnly=value;
				//}
			}
		}
        /// <summary>
        /// Get the Hosted control
        /// </summary>
		[Browsable(false)]
		public virtual GridControl EditBox
		{
			get
			{
				return this.edit;
			}
		}
 
		#endregion

		#region Combo Property
        /// <summary>
        /// Get or Set the DataSource
        /// </summary>
		[TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), Description("GridDataSourceDescr"), DefaultValue((string) null), Category("Data"), RefreshProperties(RefreshProperties.Repaint)]
		public object DataSource
		{
			get{return edit.DataSource;}
			set{edit.DataSource=value;}
		}
        /// <summary>
        ///  Get or Set the DataMember
        /// </summary>
		[DefaultValue((string) null), Editor("System.Windows.Forms.Design.DataMemberListEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor)), Description("GridDataMemberDescr"), Category("Data")]
		public string DataMember
		{
			get{return edit.DataMember;}
			set{edit.DataMember=value;}
		}
        /// <summary>
        /// Get or Set the Relation
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("Data"), Description("DataSetRelations"), Editor("Microsoft.VSDesigner.Data.Design.DataRelationEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        public System.Data.DataRelation Relation
        {
            get { return edit.Relation; }
            set{ edit.Relation = value; }
        }
        /// <summary>
        /// Get or Set if is PointToGrid
        /// </summary>
        [DefaultValue(false)]
        public bool PointToGrid
        {
            get { return edit.PointToGrid; }
            set{edit.PointToGrid = value;}
        }
        /// <summary>
        /// Get or Set if Caption is Visible
        /// </summary>
        [DefaultValue(false)]
        public bool CaptionVisible
        {
            get { return edit.CaptionVisible; }
            set { edit.CaptionVisible = value; }
        }
        /// <summary>
        /// Get or Set the Caption Text
        /// </summary>
        [DefaultValue("")]
        public string CaptionText
        {
            get { return edit.CaptionText; }
            set { edit.CaptionText = value; }
        }
        /// <summary>
        /// Get or Set the Label Text
        /// </summary>
		[DefaultValue("")]
		public string Label//Text
		{
			get{return m_Text;}
			set{m_Text=value;	}
		}
        /// <summary>
        /// Get or Set the Foreign Key
        /// </summary>
        [DefaultValue("")]
		public string ForeignKey
		{
            get { return edit.ForeignKey; }
            set { edit.ForeignKey = value; }
		}
        /// <summary>
        /// Get or Set the Visible Rows
        /// </summary>
  		[DefaultValue(10 )]
		public int VisibleRows
		{
			get{return edit.VisibleRows;}
			set{edit.VisibleRows=value;}
		}

        /// <summary>
        /// Init data source
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="dataMember"></param>
 		public void Init(object dataSource,string dataMember)
		{
			this.DataSource=dataSource;
			this.DataMember=dataMember;
		}
        /// <summary>
        /// Init data source
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="dataMember"></param>
        /// <param name="fieldFilter"></param>
        /// <param name="filterType"></param>
		public void Init(object dataSource,string dataMember,string fieldFilter,GridFilterType filterType)
		{
			Init(dataSource,dataMember,fieldFilter,/*filterType,*/this.VisibleRows);
		}
        /// <summary>
        /// Init data source
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="dataMember"></param>
        /// <param name="fieldFilter"></param>
        /// <param name="visibleRows"></param>
		public void Init(object dataSource,string dataMember,string fieldFilter,/*GridFilterType filterType,*/int visibleRows)
		{
			this.DataSource=dataSource;
			this.DataMember=dataMember;
			this.ForeignKey=fieldFilter;
			this.VisibleRows=visibleRows;
		}
 
		#endregion

	}

}
