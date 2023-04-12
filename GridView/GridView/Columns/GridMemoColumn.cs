using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing.Design;
using System.Collections;


using Nistec.Win32;
using Nistec.WinForms;
using Nistec.Data.Advanced;


namespace Nistec.GridView
{
    /// <summary>
    /// Hosts a Memo control in a cell of a GridColumnStyle for editing strings
    /// </summary>
    public class GridMemoColumn : GridColumnStyle
    {

        #region Fields

        private GridMemoBox edit;
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

        //public event ButtonClickEventHandler ButtonClick;
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
        /// Initilaized GridMemoColumn
        /// </summary>
        public GridMemoColumn()
            : this((PropertyDescriptor)null, (string)null)
        {
        }

        /// <summary>
        /// Initilaized GridMemoColumn
        /// </summary>
        /// <param name="prop"></param>
        public GridMemoColumn(PropertyDescriptor prop)
            : this(prop, null, false)
        {
        }
        /// <summary>
        /// Initilaized GridMemoColumn
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="isDefault"></param>
        public GridMemoColumn(PropertyDescriptor prop, bool isDefault)
            : this(prop, null, isDefault)
        {
        }
        /// <summary>
        /// Initilaized GridMemoColumn
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="format"></param>
        public GridMemoColumn(PropertyDescriptor prop, string format)
            : this(prop, format, false)
        {
        }
        /// <summary>
        /// Initilaized GridMemoColumn
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="format"></param>
        /// <param name="isDefault"></param>
        public GridMemoColumn(PropertyDescriptor prop, string format, bool isDefault)
            : base(prop, isDefault)
        {
            //this.xMargin = 1;
            //this.yMargin = 1;
            //this.rect = new WinMethods.RECT();
            this.format = null;
            this.formatInfo = null;
            this.oldValue = null;
            this.edit = new GridMemoBox();
            this.edit.BorderStyle = BorderStyle.None;
            //this.edit.Multiline = true;
            //this.edit.AcceptsReturn = true;
            this.edit.Visible = false;
            this.Format = format;

            base.m_ColumnType = GridColumnType.MemoColumn;
            //this.edit.ButtonClick += new Nistec.WinForms.ButtonClickEventHandler(edit_ButtonClick);
            //this.edit.MemoChanged += new EventHandler(edit_MemoChanged);
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
                        //this.edit.Text = this.typeConverter.ConvertToString(obj1);
                        this.SetValue(obj1);
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
            this.edit.Bounds = Rectangle.Empty;
           // this.edit.CloseDropDown();
        }

        private void DebugOut(string s)
        {
        }

        ///// <summary>
        ///// Overloaded. Prepares the cell for editing a value.
        ///// </summary>
        ///// <param name="source"></param>
        ///// <param name="rowNum"></param>
        ///// <param name="bounds"></param>
        ///// <param name="readOnly"></param>
        ///// <param name="instantText"></param>
        ///// <param name="cellIsVisible"></param>
        //protected internal override void Edit(BindManager source, int rowNum, Rectangle bounds, bool readOnly, string instantText, bool cellIsVisible)
        //{
        //    this.DebugOut("Begining Edit, rowNum :" + rowNum.ToString());
        //    base.OnCellEdit();
        //    if (!m_Enabled) return;

        //    Rectangle rectangle1 = bounds;
        //    this.edit.ReadOnly = (readOnly || this.ReadOnly) || this.GridTableStyle.dataGrid.ReadOnly;
        //    this.edit.Text = this.GetText(this.GetColumnValueAtRow(source, rowNum));
        //    if (!this.edit.ReadOnly && (instantText != null))
        //    {
        //        this.GridTableStyle.Grid.ColumnStartedEditing(bounds);
        //        this.edit.IsInEditOrNavigateMode = false;
        //        this.edit.Text = instantText;
        //    }
        //    if (cellIsVisible)
        //    {
        //        this.isSelected = true;
        //        bounds.Offset(this.xMargin, 2 * this.yMargin);
        //        bounds.Width -= this.xMargin;
        //        bounds.Height -= 2 * this.yMargin;
        //        this.DebugOut("edit bounds: " + bounds.ToString());
        //        this.edit.Bounds = bounds;
        //        this.edit.Visible = true;
        //        this.edit.TextAlign = this.Alignment;
        //    }
        //    else
        //    {
        //        this.edit.Bounds = rectangle1;
        //        this.edit.Visible = false;
        //    }
        //    //this.edit.RightToLeft = this.GridTableStyle.Grid.RightToLeft;
        //    this.edit.Focus();//.FocusInternal();
        //    this.editRow = rowNum;
        //    if (!this.edit.ReadOnly)
        //    {
        //        this.oldValue = this.edit.Text;
        //    }
        //    if (instantText == null)
        //    {
        //        this.edit.SelectAll();
        //    }
        //    else
        //    {
        //        int num1 = this.edit.Text.Length;
        //        this.edit.Select(num1, 0);
        //    }
        //    if (this.edit.Visible)
        //    {
        //        this.GridTableStyle.Grid.Invalidate(rectangle1);
        //    }

        //}


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
            }
            if (val == null)
                val = "";

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
                //this.DebugOut("edit bounds: " + bounds.ToString());
                this.edit.Bounds = bounds;
                //this.edit.CloseDropDown();
                this.edit.Visible = true;
                this.edit.TextAlign = this.Alignment;
                this.edit.SelectedTextInternal(val.ToString());
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
                //this.edit.SelectAll();
            }
            else
            {
                int num1 = this.edit.Text.Length;
                //this.edit.Select(num1, 0);
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

        private string GetText(object value)
        {
            if (value is DBNull)
            {
                return this.NullText;
            }
            else if (LookupViewInitilaized)
            {
                return m_LookupView.Keys.GetValue(value);
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
        /// Hide EditBox
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
                    this.edit.CloseDropDown();
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
        /// Get or set column format
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
        /// Set the column property descriptor
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
        /// Get or set column read only
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
        public virtual GridMemoBox EditBox
        {
            get
            {
                return this.edit;
            }
        }

        #endregion

        #region Combo Property

  

        #endregion

        #region Combo Methods
       
   
        private string GetDisplyValue(object colValue)
        {
            try
            {
                if (colValue == null)
                {
                    return NullText;
                }
                else
                {
                    object s = edit.Text;//.GetItemText(colValue);
                    if (s == null)
                        s = this.NullText;
                    return s.ToString();
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
   
                    return edit.Text;
            }
            catch
            {
                throw new Exception("Error Get value from Memo box");
                //return NullText;
            }
        }

        private void SetValue(object colValue)
        {
            try
            {
                if (colValue == null)
                {
                    return;
                }
                    edit.Text = colValue.ToString();
            }
            catch
            {
                throw new Exception("Error Display value in Memo box");
                //return NullText;
            }
        }

        #endregion


        private LookupView m_LookupView;
        private bool LookupViewInitilaized = false;
        /// <summary>
        /// Set Lookup View
        /// </summary>
        /// <param name="lookupView"></param>
        public void SetLookupView(LookupView lookupView)
        {
            m_LookupView = lookupView;
            LookupViewInitilaized = lookupView != null && m_LookupView.Initilaized;
            if (LookupViewInitilaized)
            {
                m_LookupView.Values.Sorted = true;
                this.ReadOnly = true;
                //this.edit.FormatType = Formats.None;
            }
        }

        //private void edit_ButtonClick(object sender, Nistec.WinForms.ButtonClickEventArgs e)
        //{
        //    if (ButtonClick != null)
        //    {
        //        ButtonClickEventArgs ev = new ButtonClickEventArgs(base.MappingName, e.Value);
        //        ButtonClick(this, ev);
        //    }
        //    else
        //    {
        //        this.GridTableStyle.dataGrid.OnButtonClick(this, new ButtonClickEventArgs(base.MappingName, e.Value));
        //    }
        //}
    }

}
