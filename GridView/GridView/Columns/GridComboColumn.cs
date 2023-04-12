using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing.Design;


using Nistec.Win32;
using Nistec.WinForms;
using Nistec.Win;


namespace Nistec.GridView
{
    /// <summary>
    /// Hosts a ComboBox control in a cell of a GridColumnStyle for editing strings
    /// </summary>
    public class GridComboColumn : GridColumnStyle
    {

        #region Fields

        private GridComboBox edit;
        private string format;
        private IFormatProvider formatInfo;
        private string oldValue;
        private MethodInfo parseMethod;
        private TypeConverter typeConverter;
        private bool isErrorInList;

        #endregion

        #region Ctor
        /// <summary>
        /// Initilaized Grid Combo Column
        /// </summary>
        public GridComboColumn()
            : this((PropertyDescriptor)null, (string)null)
        {
        }

        /// <summary>
        /// Initilaized Grid Combo Column
        /// </summary>
        /// <param name="prop"></param>
        public GridComboColumn(PropertyDescriptor prop)
            : this(prop, null, false)
        {
        }
        /// <summary>
        /// Initilaized Grid Combo Column
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="isDefault"></param>
        public GridComboColumn(PropertyDescriptor prop, bool isDefault)
            : this(prop, null, isDefault)
        {
        }
        /// <summary>
        /// Initilaized Grid Combo Column
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="format"></param>
        public GridComboColumn(PropertyDescriptor prop, string format)
            : this(prop, format, false)
        {
        }
        /// <summary>
        /// Initilaized Grid Combo Column
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="format"></param>
        /// <param name="isDefault"></param>
        public GridComboColumn(PropertyDescriptor prop, string format, bool isDefault)
            : base(prop, isDefault)
        {
            //this.xMargin = 2;
            //this.yMargin = 1;
            //this.rect = new WinMethods.RECT();
            this.format = null;
            this.formatInfo = null;
            this.oldValue = null;
            this.edit = new GridComboBox();
            this.edit.BorderStyle = BorderStyle.None;
            //this.edit.Multiline = true;
            //this.edit.AcceptsReturn = true;
            this.edit.Visible = false;
            this.Format = format;
            this.isErrorInList = false;

            base.m_ColumnType = GridColumnType.ComboColumn;
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
            this.edit.CloseDropDown();
            if (!this.edit.IsInEditOrNavigateMode)
            {
                try
                {
                    object obj1 = GetValue();// this.edit.Text;
                    if (this.NullText.Equals(obj1))
                    {
                        obj1 = Convert.DBNull;
                        this.edit.Text = this.NullText;
                    }
                    //					else if (((this.format != null) && (this.format.Length != 0)) && ((this.parseMethod != null) && (this.FormatInfo != null)))
                    //					{
                    //						obj1 = this.parseMethod.Invoke(null, new object[] { this.edit.Text, this.FormatInfo });
                    //						if (obj1 is IFormattable)
                    //						{
                    //							this.edit.Text = ((IFormattable) obj1).ToString(this.format, this.formatInfo);
                    //						}
                    //						else
                    //						{
                    //							this.edit.Text = obj1.ToString();
                    //						}
                    //					}
                    //					else if ((this.typeConverter != null) && this.typeConverter.CanConvertFrom(typeof(string)))
                    //					{
                    //						obj1 = this.typeConverter.ConvertFromString(this.edit.Text);
                    //						//this.edit.Text = this.typeConverter.ConvertToString(obj1);
                    //					}
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
                catch //(Exception ex)
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
            this.edit.CloseDropDown();
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

            this.edit.ClearSelected();
            //this.edit.Text = this.GetText(this.GetColumnValueAtRow(source, rowNum));
            object val = "";
            this.edit.editingPosition = false;

            if (this.edit.DroppedDown)
            {
                val = GetValue();
            }
            else
            {
                val = this.GetColumnValueAtRow(source, rowNum);
                edit.currentValue = val;
            }


            if (!this.edit.ReadOnly && (instantText != null))
            {
                this.GridTableStyle.Grid.ColumnStartedEditing(bounds);
                this.edit.IsInEditOrNavigateMode = false;
                //this.edit.Text = instantText;
                val = instantText;
            }
            if (cellIsVisible)
            {
                this.isSelected = true;
                bounds.Offset(this.xMargin, 2 * this.yMargin);
                bounds.Width -= this.xMargin;
                bounds.Height -= 2 * this.yMargin;
                this.DebugOut("edit bounds: " + bounds.ToString());
                this.edit.Bounds = bounds;
                this.edit.CloseDropDown();
                this.edit.Visible = true;
                this.edit.TextAlign = this.Alignment;
                this.edit.Text = GetDisplyValue(val);
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
            }
        }

        /// <summary>
        /// EndEdit
        /// </summary>
        protected void EndEdit()
        {
            this.edit.IsInEditOrNavigateMode = true;
            this.DebugOut("Ending Edit");
            this.Invalidate();
        }
        /// <summary>
        /// EnterNullValue
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

        private string GetText(BindManager source, int rowNum)
        {
            if (isSelected && editRow == rowNum)
            {
                return edit.SelectedItem.ToString();//.Text;
            }
            else
            {
                return this.GetDisplyValue(this.GetColumnValueAtRow(source, rowNum));
            }
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
            //string text1 = this.GetText(this.GetColumnValueAtRow(source, rowNum));

            //string text1 = "";
            //if (isSelected && editRow == rowNum)
            //{
            //    text1 = edit.SelectedItem.ToString();//.Text;
            //}
            //else
            //{
            //    text1 = this.GetDisplyValue(this.GetColumnValueAtRow(source, rowNum));
            //}
            this.PaintText(g, bounds, GetText(source,rowNum), alignToRight);
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
            //string text1 = "";
            //if (isSelected && editRow == rowNum)
            //{
            //    text1 = edit.ToString();//.Text;
            //}
            //else
            //{
            //    text1 = this.GetDisplyValue(this.GetColumnValueAtRow(source, rowNum));
            //}
            this.PaintText(g, bounds, GetText(source,rowNum), backBrush, foreBrush, alignToRight);
        }
        /// <summary>
        /// PaintText
        /// </summary>
        /// <param name="g"></param>
        /// <param name="bounds"></param>
        /// <param name="text"></param>
        /// <param name="alignToRight"></param>
        protected void PaintText(Graphics g, Rectangle bounds, string text, bool alignToRight)
        {
            //TableStyle:this.PaintText(g, bounds, text, this.GridTableStyle.BackBrush, this.GridTableStyle.ForeBrush, alignToRight);
            this.PaintText(g, bounds, text, this.GridTableStyle.dataGrid.BackBrush, this.GridTableStyle.dataGrid.ForeBrush, alignToRight);
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
        /// Get or Set column format info
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
        /// Set the Property Descriptor
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
        /// Get or Set the column is read only
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
        /// <summary>
        /// Get the Hosted control
        /// </summary>
        [Browsable(false)]
        public virtual GridComboBox EditBox
        {
            get
            {
                return this.edit;
            }
        }

        #endregion

        #region Combo Property

        /// <summary>
        /// Get the combo items collection
        /// </summary>
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor)), Description("ComboBoxItemsDescr"), Category("Data"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Localizable(true)]
        public Nistec.WinForms.Controls.McListItems.ObjectCollection Items
        {
            get { return edit.Items; }
        }
        /// <summary>
        /// Get or Set the combo data source
        /// </summary>
        [DefaultValue((string)null), Category("Data"), Description("ListControlDataSourceDescr"), TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), RefreshProperties(RefreshProperties.Repaint)]
        public object DataSource
        {
            get { return edit.DataSource; }
            set
            {
                edit.DataSource = value;
                this.edit.SetDataView();
                this.Invalidate();
            }
        }
        /// <summary>
        /// Get or Set the combo Display Member
        /// </summary>
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor)), Category("Data"), TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), DefaultValue(""), Description("ListControlDisplayMemberDescr")]
        public string DisplayMember
        {
            get { return edit.DisplayMember; }
            set
            {
                edit.DisplayMember = value;
                this.Invalidate();
            }
        }
        /// <summary>
        /// Get or Set the combo Value Member
        /// </summary>
        [DefaultValue(""), Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor)), Category("Data"), Description("ListControlValueMemberDescr")]
        public string ValueMember
        {
            get { return edit.ValueMember; }
            set
            {
                edit.ValueMember = value;
                this.Invalidate();
            }
        }
        /// <summary>
        /// Get or Set the combo Drop Down Width
        /// </summary>
        [Category("Layout"), DefaultValue(0)]
        public int DropDownWidth
        {
            get { return edit.DropDownWidth; }
            set
            {
                edit.DropDownWidth = value;
                this.Invalidate();
            }
        }
        /// <summary>
        /// Get or Set indicating is Button Visible
        /// </summary>
        [Category("Layout"), DefaultValue(true)]
        public bool ButtonVisible
        {
            get { return edit.ButtonVisible; }
            set
            {
                edit.ButtonVisible = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Get or Set indicating is Button Visible
        /// </summary>
        [Category("Layout"), DefaultValue(false)]
        public bool Sorted
        {
            get { return edit.Sorted; }
            set
            {
                edit.Sorted = value;
            }
        }

        #endregion

        #region Combo Methods
        /// <summary>
        /// Set Data Source
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="valueMember"></param>
        /// <param name="displyMember"></param>
        public void SetDataSource(object dataSource, string valueMember, string displyMember)
        {
            this.DataSource = dataSource;
            this.ValueMember = valueMember;
            this.DisplayMember = displyMember;
        }


        internal string GetDisplyValue(object colValue)
        {
            string s = this.NullText;
            try
            {
                if (this.isErrorInList)
                {
                    if (colValue != null)
                        s = colValue.ToString();
                    return s;
                }
                if (colValue == null)
                {
                    return NullText;
                }
                else
                {
                    s = edit.GetItemText(colValue);
                    if (s == null || s == "")
                        s = this.NullText;
                    return s;
                }
            }
            catch
            {
                this.isErrorInList = true;
                MsgBox.ShowError("Error Display value in combo box");
                return s;
                //throw new Exception("Error Display value in combo box");
                //return NullText;
            }
        }

        private object GetValue()
        {
            try
            {
                if (edit.DataSource != null)
                {
                    return edit.GetItemValue();//.SelectedValue;
                }
                else
                {
                    return edit.SelectedItem;
                }
            }
            catch
            {
                throw new Exception("Error Get value in combo box");
                //return NullText;
            }
        }

        //		private void SetValue(object colValue) 
        //		{
        //			try 
        //			{
        //				if (colValue==null) 
        //				{
        //					return;
        //				}
        //				if(edit.DataSource!=null)
        //				{
        //					edit.SelectedValue=colValue;
        //				}
        //				else
        //				{
        //					edit.SelectedItem=colValue ;
        //				}
        //			}
        //			catch  
        //			{
        //				throw new Exception("Error Display value in combo box");
        //				//return NullText;
        //			}
        //		}

        #endregion

    }

}
