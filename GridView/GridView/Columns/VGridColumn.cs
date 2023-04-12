using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing.Design;
using System.Collections;


using Nistec.Win32;
using Nistec.WinForms;


namespace Nistec.GridView
{
    /// <summary>
    /// Hosts a VGrid control in a cell of a GridColumnStyle for editing relational records
    /// </summary>
	public class VGridColumn : GridColumnStyle
	{

		#region Fields

        private VGridControl edit;
		private string format;
		private IFormatProvider formatInfo;
		private string oldValue;
		private MethodInfo parseMethod;
		private TypeConverter typeConverter;
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

		static VGridColumn()
		{
			VGridColumn.collapsed =NativeMethods.LoadImage("Nistec.GridView.Images.collapsed.gif");
			VGridColumn.expaned  =NativeMethods.LoadImage ("Nistec.GridView.Images.expaned.gif");

		}
        /// <summary>
        /// Initilaized VGridColumn
        /// </summary>
		public VGridColumn() : this((PropertyDescriptor) null, (string) null)
		{
		}

         
        /// <summary>
        /// Initilaized VGridColumn
        /// </summary>
        /// <param name="prop"></param>
		public VGridColumn(PropertyDescriptor prop) : this(prop, null, false)
		{
		}
        /// <summary>
        /// Initilaized VGridColumn
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="isDefault"></param>
		public VGridColumn(PropertyDescriptor prop, bool isDefault) : this(prop, null, isDefault)
		{
		}
        /// <summary>
        /// Initilaized VGridColumn
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="format"></param>
		public VGridColumn(PropertyDescriptor prop, string format) : this(prop, format, false)
		{
		}
        /// <summary>
        /// Initilaized VGridColumn
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="format"></param>
        /// <param name="isDefault"></param>
        public VGridColumn(PropertyDescriptor prop, string format, bool isDefault)
            : base(prop, isDefault)
		{
            //this.xMargin = 2;
            //this.yMargin = 1;
			//this.rect = new WinMethods.RECT();
			this.format = null;
			this.formatInfo = null;
			this.oldValue = null;
			this.edit = new VGridControl();
			this.edit.Visible = false;
            this.edit.gridColumn =this as IGridColumn;
			
            this.Format = format;
			alignRight=false;
            m_ColumnType = GridColumnType.VGridColumn;
            base.m_AllowUnBound = true;
            base.HostControl = this.edit;
			m_Text="";


		}
        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                currentValue = null;
                format = null;
                oldValue = null;
            }
            base.Dispose(disposing);
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
        /// EnterNullValue
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
            
            bool isDroppedDown = this.edit.DroppedDown;
            if (isDroppedDown)
            {
                if (rowNum != editRow)
                    return;
            }
            
            this.alignRight = this.GridTableStyle.dataGrid.RightToLeft == RightToLeft.Yes;
            Point p = this.GridTableStyle.dataGrid.PointToClient(Cursor.Position);
            RectangleF cursorBounds = new RectangleF(p.X, p.Y, 1, 1);

            Rectangle GlypRect = bounds;
            GlypRect.Offset(this.xMargin, 2 * this.yMargin);
            GlypRect.Width -= this.xMargin;
            GlypRect.Height -= 2 * this.yMargin;
            RectangleF controlBounds = GetButtonBounds((RectangleF)GlypRect, alignRight);

            if (!cursorBounds.IntersectsWith(controlBounds))
                return ;

            base.editRow = rowNum;
            base.cellBounds = bounds;
            edit.curRow = rowNum;
            this.currentValue = "";
            if (isBound)/*bound*/
            {
                this.currentValue = this.GetColumnValueAtRow(source, rowNum);
            }
            this.isSelected = true;

            if (cellIsVisible)
            {
                this.edit.Bounds = new Rectangle((int)controlBounds.X, (int)controlBounds.Y, (int)controlBounds.Width, (int)controlBounds.Height);
                if (!isDroppedDown)
                {
                    this.edit.DataSource = this.GridTableStyle.dataGrid.GetCurrentDataRow();
                }
                this.edit.Visible = true;
                this.edit.DoDropDown();
            }
            else
            {
                this.edit.Bounds = bounds;
                this.edit.Visible = false;
            }
 		}

        internal override bool MouseDown(int rowNum, int x, int y)
        {
            //this.GridTableStyle.Grid.CurrentRowIndex = rowNum;
            //base.editRow = rowNum;
            if ((this.isSelected && (this.editRow == rowNum)) && !this.IsReadOnly())
                return true;
            return false;
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

                RectangleF controlBounds = GetButtonBounds(bounds, alignToRight);
                Rectangle textRect = bounds;
                textRect.Width -= (int)(controlBounds.Width + 2);


                if (alignToRight)
                {
                    //format1.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
                    textRect.X = bounds.X - 2;// bounds.Width -controlBounds.Width -2;
                }
                else
                {
                    textRect.X = (int)(controlBounds.X + controlBounds.Width + 2);
                }

                if (this.GridTableStyle.dataGrid.SelectionType == SelectionType.FullRow && this.isSelected)
                {
                    backBrush = this.GridTableStyle.dataGrid.BackBrush;
                    foreBrush = this.GridTableStyle.dataGrid.ForeBrush;
                }

                g.FillRectangle(backBrush, bounds);

                if (this.DataSource != null)
                {
                    if (this.editRow == rowNum && edit.DroppedDown)
                        g.DrawImage(VGridColumn.expaned, (RectangleF)controlBounds);
                    else
                        g.DrawImage(VGridColumn.collapsed, (RectangleF)controlBounds);
                }

                string text1 = this.m_Text;
                if (text1.Length == 0 && isBound)/*bound*/
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
        /// <summary>
        /// Set Column Value At Row
        /// </summary>
        /// <param name="lm"></param>
        /// <param name="row"></param>
        /// <param name="value"></param>
        protected internal override void SetColumnValueAtRow(BindManager lm, int row, object value)
        {
            object obj1 = value;
            this.currentValue = obj1;
            //base.SetColumnValueAtRow(lm, row, obj1);
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
			this.edit.SetGrid(value);
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
        [Browsable(false)]
        public override string Text
        {
            get { return this.currentValue==null? "" : currentValue.ToString(); }
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
        /// Get or Set column fornat info
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
        /// Get or Set column read only property
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
        public virtual VGridControl EditBox
        {
            get
            {
                return this.edit;
            }
        }
 
		#endregion

		#region Combo Property
        /// <summary>
        /// Get or set column data source
        /// </summary>
 		[TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), Description("GridDataSourceDescr"), DefaultValue((string) null), Category("Data"), RefreshProperties(RefreshProperties.Repaint)]
		public object DataSource
		{
			get{return edit.DataSource;}
			set{edit.DataSource=value;}
		}
        /// <summary>
        /// Get or set the column shouled Point ToGrid
        /// </summary>
        [DefaultValue(false)]
        public bool PointToGrid
        {
            get { return edit.PointToGrid; }
            set{edit.PointToGrid = value;}
        }
        /// <summary>
        /// Get or set the Caption Visible
        /// </summary>
        [DefaultValue(false)]
        public bool CaptionVisible
        {
            get { return edit.CaptionVisible; }
            set { edit.CaptionVisible = value; }
        }
        /// <summary>
        /// Get or set Caption Text
        /// </summary>
        [DefaultValue("")]
        public string CaptionText
        {
            get { return edit.CaptionText; }
            set { edit.CaptionText = value; }
        }
        /// <summary>
        /// Get or set Label
        /// </summary>
		[DefaultValue("")]
        public string Label//Text
		{
			get{return m_Text;}
			set{m_Text=value;	}
		}
 
        /// <summary>
        /// Get or set Visible rows
        /// </summary>
		[DefaultValue(10 )]
		public int VisibleRows
		{
			get{return edit.VisibleRows;}
			set{edit.VisibleRows=value;}
		}
        /// <summary>
        /// Get or set the column is bound
        /// </summary>
        [DefaultValue(true)]
        public new bool IsBound/*bound*/
        {
            get { return isBound; }
            set 
            {
                if (isBound != value)
                {
                    if (!isBound)
                    {
                        base.MappingName = "";
                    } 
                    isBound = value;
                    //if (!isBound)
                    //{
                    //    base.MappingName = "UnBound" +base.GetHashCode().ToString();
                    //} 
                }
            }
        }
 
		#endregion

	}

}
