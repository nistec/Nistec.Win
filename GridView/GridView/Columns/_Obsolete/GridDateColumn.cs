using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing.Design;


using Nistec.Win32;
using Nistec.WinForms;


namespace Nistec.GridView
{
    /// <summary>
    /// Hosts a DateTimePicker control in a cell of a GridColumnStyle for editing date time strings
    /// </summary>
    public class GridDateColumn : GridColumnStyle
    {

        #region Fields

        //private GridDatePicker edit;
        private GridTextBox edit;
        private string format;
        private IFormatProvider formatInfo;
        private string oldValue;
        private MethodInfo parseMethod;
        //private WinMethods.RECT rect;
        private TypeConverter typeConverter;
        //private int xMargin;
        //private int yMargin;

        #endregion

        #region Events

        //public event CellValidatingEventHandler CellValidating;
        //public event EventHandler CellValidated;

        //internal protected virtual bool OnCellValidating(int rowNum, object value)
        //{
        //    if (CellValidating != null)
        //    {
        //        CellValidatingEventArgs evnt = new CellValidatingEventArgs(rowNum, this.MappingName, value);
        //        CellValidating(this, evnt);
        //        return !(evnt.Cancel);
        //    }
        //    return true;
        //}

        //internal protected virtual void OnCellValidated()
        //{
        //    if (CellValidated != null)
        //    {
        //        CellValidated(this, EventArgs.Empty);
        //    }
        //    //this.GridTableStyle.dataGrid.OnDirty(true);
        //}

        #endregion

        #region Ctor
        /// <summary>
        /// Initilaized GridDateColumn
        /// </summary>
        public GridDateColumn()
            : this((PropertyDescriptor)null, (string)null)
        {
        }

        /// <summary>
        /// Initilaized GridDateColumn
        /// </summary>
        /// <param name="prop"></param>
        public GridDateColumn(PropertyDescriptor prop)
            : this(prop, null, false)
        {
        }
        /// <summary>
        /// Initilaized GridDateColumn
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="isDefault"></param>
        public GridDateColumn(PropertyDescriptor prop, bool isDefault)
            : this(prop, null, isDefault)
        {
        }
        /// <summary>
        /// Initilaized GridDateColumn
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="format"></param>
        public GridDateColumn(PropertyDescriptor prop, string format)
            : this(prop, format, false)
        {
        }
        /// <summary>
        /// Initilaized GridDateColumn
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="format"></param>
        /// <param name="isDefault"></param>
        public GridDateColumn(PropertyDescriptor prop, string format, bool isDefault)
            : base(prop, isDefault)
        {
            //this.xMargin = 2;
            //this.yMargin = 1;
            //this.rect = new WinMethods.RECT();
            this.format = null;
            this.formatInfo = null;
            this.oldValue = null;
            this.edit = new GridTextBox(); //new GridDatePicker();
            this.edit.BorderStyle = BorderStyle.None;
            //this.edit.Multiline = true;
            //this.edit.AcceptsReturn = true;
            this.edit.Visible = false;
            this.Format = format;
            //this.UseMask = false;

            base.m_ColumnType = GridColumnType.DateTimeColumn;
            base.m_AllowUnBound = false;
            base.HostControl = this.edit;

        }
        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
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
            this.RollBack();
            this.HideEditBox();
            this.EndEdit();
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
            this.edit.Bounds = Rectangle.Empty;
            //this.edit.CloseDropDown();
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
                    else if (((this.format != null) && (this.format.Length != 0)) && ((this.parseMethod != null) && (this.FormatInfo != null)))
                    {
                        obj1 = this.parseMethod.Invoke(null, new object[] { this.edit.Text, this.FormatInfo });
                        if (obj1 is IFormattable)
                        {
                            this.edit.Text = ((IFormattable)obj1).ToString(this.format, this.formatInfo);
                        }
                        else
                        {
                            this.edit.Text = obj1.ToString();
                        }
                    }
                    else if ((this.typeConverter != null) && this.typeConverter.CanConvertFrom(typeof(string)))
                    {
                        obj1 = this.typeConverter.ConvertFromString(this.edit.Text);
                        this.edit.Text = this.typeConverter.ConvertToString(obj1);
                    }
                    //this.SetColumnValueAtRow(dataSource, rowNum, obj1);

                    if (!Validating(rowNum, obj1))
                    {
                        Abort(rowNum);
                        return false;
                    }
                    this.SetColumnValueAtRow(dataSource, rowNum, obj1);
                    this.OnCellValidated();



                    //if ((!IsValid(obj1)))
                    //{
                    //    Abort(rowNum);
                    //    return false;
                    //}
                    //if (!OnCellValidating(rowNum, obj1))
                    //{
                    //    Abort(rowNum);
                    //    return false;
                    //}

                    //this.SetColumnValueAtRow(dataSource, rowNum, obj1);
                    //OnCellValidated();

                }
                catch (Exception)
                {
                    this.RollBack();
                    //RM.ShowError(RM.ErrorNotExpected) ;
                    return false;
                }
                this.DebugOut("OnCommit completed without Exception.");
                this.EndEdit();
            }
            return true;
        }
        /// <summary>
        /// Notifies a column that it must relinquish the focus to the control it is hosting.
        /// </summary>
        protected internal override void ConcedeFocus()
        {
            this.isSelected = false;
            this.edit.Bounds = Rectangle.Empty;
            //this.edit.CloseDropDown();
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
            this.DebugOut("Begining Edit, rowNum :" + rowNum.ToString());
            base.OnCellEdit();
            if (!m_Enabled) return;
            Rectangle rectangle1 = bounds;
            this.edit.ReadOnly = (readOnly || this.ReadOnly) || this.GridTableStyle.dataGrid.ReadOnly;
            //this.edit.ClearSelected();
            //this.edit.editingPosition = false;

            //this.edit.Text = this.GetText(this.GetColumnValueAtRow(source, rowNum));
            if (!this.edit.ReadOnly && (instantText != null))
            {
                this.GridTableStyle.Grid.ColumnStartedEditing(bounds);
                this.edit.IsInEditOrNavigateMode = false;
                this.edit.Text = instantText;
            }
            if (cellIsVisible)
            {
                this.isSelected = true;
                bounds.Offset(this.xMargin, 2 * this.yMargin);
                bounds.Width -= this.xMargin;
                bounds.Height -= 2 * this.yMargin;
                this.DebugOut("edit bounds: " + bounds.ToString());
                this.edit.Bounds = bounds;
                //this.edit.CloseDropDown();
                this.edit.Visible = true;
                this.edit.TextAlign = this.Alignment;
                //this.edit.AppendInternalText(GetText(this.GetColumnValueAtRow(source, rowNum)));
            }
            else
            {
                this.edit.Bounds = rectangle1;
                this.edit.Visible = false;
            }
            //this.edit.RightToLeft = this.GridTableStyle.Grid.RightToLeft;
            this.edit.Focus();//.FocusInternal();
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
                this.edit.Select(num1, 0);
            }
            if (this.edit.Visible)
            {
                this.GridTableStyle.Grid.Invalidate(rectangle1); 
                //this.edit.editingPosition = true;

            }
        }

        /// <summary>
        /// End Edit
        /// </summary>
        protected void EndEdit()
        {
            this.edit.IsInEditOrNavigateMode = true;
            this.DebugOut("Ending Edit");
            this.Invalidate();
        }
        /// <summary>
        /// Enter Null Value
        /// </summary>
        protected internal override void EnterNullValue()
        {
            if ((!this.ReadOnly && this.edit.Visible) && this.edit.IsInEditOrNavigateMode)
            {
                this.edit.Text = this.NullText;
                this.edit.IsInEditOrNavigateMode = false;
                if ((this.GridTableStyle != null) && (this.GridTableStyle.Grid != null))
                {
                    this.GridTableStyle.Grid.ColumnStartedEditing(this.edit.Bounds);
                }
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
            int num1 = 0;
            int num2 = 0;
            string text1 = this.GetText(value);
            while ((num1 != -1) && (num1 < text1.Length))
            {
                num1 = text1.IndexOf("\r\n", num1 + 1);
                num2++;
            }
            return ((base.FontHeight * num2) + this.yMargin+bMargin);
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
                    return ((IFormattable)value).ToString(this.format, this.formatInfo);
                }
                catch (Exception)
                {
                    goto Label_0084;
                }
            }
            if ((this.typeConverter != null) && this.typeConverter.CanConvertTo(typeof(string)))
            {
                return (string)this.typeConverter.ConvertTo(value, typeof(string));
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
            if (this.edit.IsInEditOrNavigateMode)
            {
                if (keyData == Keys.Escape)
                {
                    //this.edit.CloseDropDown();
                    return true;
                }
                return base.KeyPress(rowNum, keyData);
            }
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
            string text1 = this.GetText(this.GetColumnValueAtRow(source, rowNum));
            this.PaintText(g, bounds, text1, alignToRight);
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
            string text1 = this.GetText(this.GetColumnValueAtRow(source, rowNum));
            this.PaintText(g, bounds, text1, backBrush, foreBrush, alignToRight);
        }
        /// <summary>
        /// Paint Text
        /// </summary>
        /// <param name="g"></param>
        /// <param name="bounds"></param>
        /// <param name="text"></param>
        /// <param name="alignToRight"></param>
        protected void PaintText(Graphics g, Rectangle bounds, string text, bool alignToRight)
        {
            this.PaintText(g, bounds, text, this.GridTableStyle.dataGrid.BackBrush, this.GridTableStyle.dataGrid.ForeBrush, alignToRight);
        }

        /// <summary>
        /// Paint Text
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
            }
            format1.Alignment = (this.Alignment == HorizontalAlignment.Left) ? StringAlignment.Near : ((this.Alignment == HorizontalAlignment.Center) ? StringAlignment.Center : StringAlignment.Far);
            format1.FormatFlags |= StringFormatFlags.NoWrap;
            if (this.GridTableStyle.dataGrid.SelectionType == SelectionType.FullRow && this.isSelected)
            {
                backBrush = this.GridTableStyle.dataGrid.BackBrush;
            }
            g.FillRectangle(backBrush, rectangle1);
            rectangle1.Offset(0, 2 * this.yMargin);
            rectangle1.Height -= 2 * this.yMargin;
            g.DrawString(text, this.GridTableStyle.Grid.Font, foreBrush, (RectangleF)rectangle1, format1);
            format1.Dispose();
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
            //this.edit.SetFormatMask();
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
            if (!this.edit.ReadOnly && (instantText != null))
            {
                this.edit.Text = instantText;
            }
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
            get
            {
                if (!this.edit.IsInEditOrNavigateMode)
                {
                    return this.edit == null ? "" : this.edit.Text;
                }
                return base.GetCurrentText();
            }
            set
            {
                if (this.edit.IsInEditOrNavigateMode)
                {
                    SetCurrentText(value);
                }
                this.edit.Text = value;
            }
        }
        /// <summary>
        /// Get or set column Format 
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
                    this.edit.Format = value;
                    this.Invalidate();
                }
            }
        }
        /// <summary>
        /// Get or Set the column Format Info
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), DefaultValue(null)]
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
        /// Set the column Property Descriptor
        /// </summary>
        [DefaultValue((string)null), Description("FormatControlFormatDescr")]
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
        /// Get or Set the coulmn read only
        /// </summary>
        public override bool ReadOnly
        {
            get
            {
                return base.ReadOnly;
            }
            set
            {
                if ((value || !"".Equals(this.format)) || ((this.typeConverter == null) || this.typeConverter.CanConvertFrom(typeof(string))))
                {
                    base.ReadOnly = value;
                }
            }
        }
        ///// <summary>
        ///// Get the Hosted control
        ///// </summary>
        //[Browsable(false)]
        //public virtual GridDatePicker EditBox
        //{
        //    get
        //    {
        //        return this.edit;
        //    }
        //}

        /// <summary>
        /// Get the Hosted control
        /// </summary>
        [Browsable(false)]
        public virtual GridTextBox EditBox
        {
            get
            {
                return this.edit;
            }
        }

        //[Localizable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public Nistec.Win.RangeDate RangeValue
        //{
        //    get { return edit.RangeValue; }
        //    set
        //    {
        //        edit.RangeValue = value;
        //        this.Invalidate();
        //    }
        //}

        ///// <summary>
        ///// Get or Set the minimum value of date time picker
        ///// </summary>
        //[Category("Behavior"), Description("min value")]
        //public DateTime MinValue
        //{
        //    get { return edit.MinValue; }
        //    set
        //    {
        //        edit.MinValue = value;
        //    }
        //}
        ///// <summary>
        ///// Get or set the maximum value of date time picker
        ///// </summary>
        //[Category("Behavior"), Description("max value")]
        //public DateTime MaxValue
        //{
        //    get { return edit.MaxValue; }
        //    set
        //    {
        //        edit.MaxValue = value;
        //    }
        //}

        //		public string InputMask 
        //		{
        //			get	{return edit.InputMask ;}
        //			set
        //			{
        //				if(value==null)value="";
        //				edit.InputMask =value;
        //				this.Invalidate (); 
        //			} 
        //		}

        ///// <summary>
        ///// Get or set indicating to use Mask edit
        ///// </summary>
        //public bool UseMask
        //{
        //    get { return edit.UseMask; }
        //    set
        //    {
        //        edit.UseMask = value;
        //        this.Invalidate();
        //    }
        //}
        ///// <summary>
        ///// Get or Set Button Visible
        ///// </summary>
        //[Category("Layout"), DefaultValue(true)]
        //public bool ButtonVisible
        //{
        //    get { return edit.ButtonVisible; }
        //    set
        //    {
        //        edit.ButtonVisible = value;
        //        this.Invalidate();
        //    }
        //}

 
        #endregion

        /// <summary>
        /// Get Current Value
        /// </summary>
        /// <returns></returns>
        public DateTime CurrentValue()
        {
            return Types.ToDateTime(Text);// this.GridTableStyle.Grid[this.MappingName]);
        }

    }

}
