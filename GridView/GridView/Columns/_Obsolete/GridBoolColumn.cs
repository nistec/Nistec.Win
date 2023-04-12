using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;


using Nistec.WinForms;

namespace Nistec.GridView
{
    /// <summary>
    /// Specifies a column in which each cell contains a check box for representing a Boolean value
    /// </summary>
    public class GridBoolColumn : GridColumnStyle
    {


        #region Members
        // Fields
        private bool allowNull;
        private object currentValue;
        //private static readonly object EventAllowNull;
        //private static readonly object EventFalseValue;
        //private static readonly object EventTrueValue;
        private static readonly int idealCheckSize;
        private bool isEditing;
        private object nullValue;
        private object trueValue;
        private object falseValue;

        private Color m_BorderColor;
        private Color m_CheckColor;

        /// <summary>
        /// Allow Null Changed event
        /// </summary>
        public event EventHandler AllowNullChanged;
        ///// <summary>
        ///// False Value Changed event
        ///// </summary>
        //public event EventHandler FalseValueChanged;
        ///// <summary>
        ///// True Value Changed event
        ///// </summary>
        //public event EventHandler TrueValueChanged;
        /// <summary>
        /// Checked Changed event
        /// </summary>
        public event EventHandler CheckedChanged;

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

        #region Constructor

        static GridBoolColumn()
        {
            GridBoolColumn.idealCheckSize = 12;
            //GridBoolColumn.EventTrueValue = new object();
            //GridBoolColumn.EventFalseValue = new object();
            //GridBoolColumn.EventAllowNull = new object();
        }
        /// <summary>
        /// Initilaized a new Grid Bool Column class
        /// </summary>
        public GridBoolColumn()
        {
            InitColumn();
        }
        /// <summary>
        /// Initilaized a new Grid Bool Column class
        /// </summary>
        /// <param name="prop"></param>
        public GridBoolColumn(PropertyDescriptor prop)
            : base(prop)
        {
            InitColumn();
        }
        /// <summary>
        /// Initilaized a new Grid Bool Column class
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="isDefault"></param>
        public GridBoolColumn(PropertyDescriptor prop, bool isDefault)
            : base(prop, isDefault)
        {
            InitColumn();
        }

        private void InitColumn()
        {
            m_BorderColor = Color.Blue;
            m_CheckColor = Color.Blue;

            this.isEditing = false;
            this.isSelected = false;
            this.allowNull = false;
            this.currentValue = Convert.DBNull;
            this.trueValue = true;
            this.falseValue = false;
            this.nullValue = Convert.DBNull;

            base.m_ColumnType = GridColumnType.BoolColumn;
            base.m_AllowUnBound = false;
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
                nullValue = null;
                trueValue = null;
                falseValue = null;
            }
            base.Dispose(disposing);
        }

        #endregion

        #region internal override
        /// <summary>
        /// Commits changes in the current cell ,When overridden in a derived class, initiates a request to complete an editing procedure. 
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="rowNum"></param>
        /// <returns></returns>
        protected internal override bool Commit(BindManager dataSource, int rowNum)
        {
            this.isSelected = false;
            if (this.isEditing)
            {

                //				if ((!IsValid( this.currentValue))) 
                //				{
                //					Abort(rowNum);
                //					return false;
                //				}

                if (!OnCellValidating(rowNum, this.currentValue))
                {
                    Abort(rowNum);
                    return false;
                }

                this.SetColumnValueAtRow(dataSource, rowNum, this.currentValue);
                this.OnCellValidated();

                this.isEditing = false;
                this.Invalidate();
            }
            return true;
        }
        /// <summary>
        /// When overridden in a derived class, initiates a request to interrupt an edit procedure.
        /// </summary>
        /// <param name="rowNum"></param>
        protected internal override void Abort(int rowNum)
        {
            this.isSelected = false;
            this.isEditing = false;
            this.Invalidate();
        }
        /// <summary>
        /// Notifies a column that it must relinquish the focus to the control it is hosting.
        /// </summary>
        protected internal override void ConcedeFocus()
        {
            base.ConcedeFocus();
            this.isSelected = false;
            this.isEditing = false;
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
            this.isSelected = true;
            Grid grid1 = this.GridTableStyle.dataGrid;
            if (!grid1.Focused)
            {
                grid1.Focus();
            }
            if (!readOnly && !this.IsReadOnly())
            {
                this.editRow = rowNum;
                this.isEditing = true;
                this.currentValue = this.GetColumnValueAtRow(source, rowNum);
                //this.ToggleValue();
            }
            base.Invalidate();
        }
        /// <summary>
        /// Enter Null Value
        /// </summary>
        protected internal override void EnterNullValue()
        {
            if ((this.AllowNullValue && !this.IsReadOnly()) && (this.currentValue != Convert.DBNull))
            {
                this.currentValue = Convert.DBNull;
                this.Invalidate();
            }
        }


        private Rectangle GetCheckBoxBounds(Rectangle bounds, bool alignToRight)
        {
            if (alignToRight)
            {
                return new Rectangle(bounds.X + ((bounds.Width - GridBoolColumn.idealCheckSize) / 2), bounds.Y + 2, (bounds.Width < GridBoolColumn.idealCheckSize) ? bounds.Width : GridBoolColumn.idealCheckSize, GridBoolColumn.idealCheckSize);
            }
            return new Rectangle(Math.Max(0, bounds.X + ((bounds.Width - GridBoolColumn.idealCheckSize) / 2)), bounds.Y + 2, (bounds.Width < GridBoolColumn.idealCheckSize) ? bounds.Width : GridBoolColumn.idealCheckSize, GridBoolColumn.idealCheckSize);
            //			if (alignToRight)
            //			{
            //				return new Rectangle(bounds.X + ((bounds.Width - GridBoolColumn.idealCheckSize) / 2), bounds.Y + ((bounds.Height - GridBoolColumn.idealCheckSize) / 2), (bounds.Width < GridBoolColumn.idealCheckSize) ? bounds.Width : GridBoolColumn.idealCheckSize, GridBoolColumn.idealCheckSize);
            //			}
            //			return new Rectangle(Math.Max(0, bounds.X + ((bounds.Width - GridBoolColumn.idealCheckSize) / 2)), Math.Max(0, bounds.Y + ((bounds.Height - GridBoolColumn.idealCheckSize) / 2)), (bounds.Width < GridBoolColumn.idealCheckSize) ? bounds.Width : GridBoolColumn.idealCheckSize, GridBoolColumn.idealCheckSize);
        }
        /// <summary>
        /// Get Column Value At Row
        /// </summary>
        /// <param name="lm"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        protected internal override object GetColumnValueAtRow(BindManager lm, int row)
        {
            object obj1 = base.GetColumnValueAtRow(lm, row);
            object obj2 = Convert.DBNull;
            if (obj1 != null)
            {
                string val = obj1.ToString().ToLower();
                if (val.Equals("true") || obj1.Equals(1))
                {
                    return true;
                }
                if (val.Equals("false") || obj1.Equals(0))
                {
                    obj2 = false;
                }
            }
            return obj2;
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
            this.Paint(g, bounds, source, rowNum, (SolidBrush)new SolidBrush(this.GridTableStyle.dataGrid.BackColor), (SolidBrush)new SolidBrush(this.GridTableStyle.dataGrid.ForeColor), alignToRight);
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
        //protected internal override void Paint(Graphics g, Rectangle bounds, BindManager source, int rowNum, Brush backBrush, Brush foreBrush, bool alignToRight)
        //{
        //    this.GridTableStyle.dataGrid.FindForm().Text = "editRow: " + editRow.ToString() + " rowNum: " + rowNum.ToString();

        //    object obj1 = (this.isEditing && (this.editRow == rowNum)) ? this.currentValue : this.GetColumnValueAtRow(source, rowNum);
        //    //object obj1 =  (this.editRow == rowNum) ? this.currentValue : this.GetColumnValueAtRow(source, rowNum);


        //    ButtonState state1 = ButtonState.Flat;// .Inactive;
        //    if (!Convert.IsDBNull(obj1))
        //    {
        //        state1 = ((bool)obj1) ? ButtonState.Checked : ButtonState.Normal;
        //    }
        //    Rectangle rectangle1 = this.GetCheckBoxBounds(bounds, alignToRight);
        //    //Region region1 = g.Clip;
        //    //g.ExcludeClip(rectangle1);
        //    //Brush brushBck = this.GridTableStyle.IsDefault ? this.GridTableStyle.Grid.SelectionBackBrush : this.GridTableStyle.SelectionBackBrush;

        //    //Brush brushCol = (SolidBrush)new SolidBrush(this.GridTableStyle.dataGrid.SelectionBackColor);

        //    //if (this.GridTableStyle.dataGrid.SelectionType == SelectionType.FullRow && this.isSelected)
        //    //{
        //    //    brushCol = this.GridTableStyle.dataGrid.BackBrush;
        //    //}

        //    //if ((this.isSelected && (this.editRow == rowNum)) && !this.IsReadOnly())
        //    //{
        //    //    g.FillRectangle(brushCol, bounds);
        //    //}
        //    //else
        //    //{
        //    //    g.FillRectangle(backBrush, bounds);
        //    //}
        //    g.FillRectangle(backBrush, bounds);

        //    //g.Clip = region1;
        //    //brushCol.Dispose();
        //    Rectangle drawRect = new Rectangle(rectangle1.Left + 3, rectangle1.Top + 3, 6, 6);

        //    //new SolidBrush (m_CheckColor)
        //    using (Brush sb1 = this.GridTableStyle.dataGrid.LayoutManager.GetBrushHot(), sb2 = new SolidBrush(Color.White), sb3 = new SolidBrush(Color.LightGray))
        //    {
        //        g.FillRectangle(sb2, rectangle1);
        //        if (state1 == ButtonState.Flat)//Inactive)
        //        {

        //            g.FillRectangle(sb3, drawRect);
        //            //ControlPaint.DrawMixedCheckBox(g, rectangle1, ButtonState.Checked);
        //        }
        //        else if (state1 == ButtonState.Checked)//Inactive)
        //        {
        //            g.FillRectangle(sb1, drawRect);
        //            //ControlPaint.DrawMixedCheckBox(g, rectangle1, ButtonState.Checked);
        //        }
        //        else
        //        {
        //            g.FillRectangle(sb2, drawRect);
        //            //ControlPaint.DrawCheckBox(g, rectangle1, state1);
        //        }
        //    }

        //    using (Pen pen = this.GridTableStyle.dataGrid.LayoutManager.GetPenBorder())
        //    {
        //        rectangle1.Width -= 1;
        //        rectangle1.Height -= 1;
        //        g.DrawRectangle(pen, rectangle1);
        //    }
        //    //ControlPaint.DrawBorder(g, rectangle1,grid.BorderColor, ButtonBorderStyle.Solid );

        //    //if(bounds==McGrid.m_FocusBounds )  
        //    //	ControlPaint.DrawBorder(g, bounds, m_FocusColor , ButtonBorderStyle.Solid );
        //    //else
        //    //	ControlPaint.DrawBorder(g, bounds, Color.Transparent  , ButtonBorderStyle.None  );


        //    if ((this.IsReadOnly() && this.isSelected) && (source.Position == rowNum))
        //    {
        //        bounds.Inflate(-1, -1);
        //        Pen pen1 = new Pen(this.GridTableStyle.dataGrid.SelectionBackColor);// (brushCol);
        //        pen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;//Dash;
        //        g.DrawRectangle(pen1, bounds);
        //        pen1.Dispose();
        //        bounds.Inflate(1, 1);
        //    }
        //}

        protected internal override void Paint(Graphics g, Rectangle bounds, BindManager source, int rowNum, Brush backBrush, Brush foreBrush, bool alignToRight)
        {
            ButtonStates state = ButtonStates.Normal;
            object val = (this.isEditing && (this.editRow == rowNum)) ? this.currentValue : this.GetColumnValueAtRow(source, rowNum);
            bool Checked = Convert.IsDBNull(val) ? false : ((bool)val);

            Rectangle checkRect = this.GetCheckBoxBounds(bounds, alignToRight);
            g.FillRectangle(backBrush, bounds);
            DrawCheckBox(g, checkRect, state, Checked, true);

        }

        private void DrawCheckBox(Graphics g, Rectangle CheckRect, ButtonStates state, bool Checked, bool Enabled)
        {
            IStyleLayout layout = this.GridTableStyle.Grid.LayoutManager;

            Rectangle insideRect = CheckRect;
            //insideRect.Inflate(-1, -1);
            using (Brush sbg = layout.GetBrushGradient(insideRect, 220f))
            {
                g.FillRectangle(sbg, insideRect);
            }

            using (Pen pen = layout.GetPenBorder())
            {
                g.DrawRectangle(pen, CheckRect);
            }

            if (Checked)
            {
                //Rectangle drawRect = new Rectangle(CheckRect.Left + 3, CheckRect.Top + 3, CheckRect.Width - 6, CheckRect.Height - 6);
                //using (Brush sb2 = new SolidBrush(layout.BorderHotColorInternal))
                //{
                //    g.FillRectangle(sb2, drawRect);
                //}

                RectangleF rect = (RectangleF)CheckRect;
                using (Pen p = new Pen(Color.DarkGreen, 2))
                {
                    g.DrawLine(p, rect.X + 2f, rect.Y + 4, rect.X + 6f, rect.Bottom - 3f);
                    g.DrawLine(p, rect.X + 4f, rect.Bottom - 3f, rect.Right - 2f, rect.Top + 3f);
                }
            }
        }

        /// <summary>
        /// Set Column Value At Row
        /// </summary>
        /// <param name="lm"></param>
        /// <param name="row"></param>
        /// <param name="value"></param>
        protected internal override void SetColumnValueAtRow(BindManager lm, int row, object value)
        {
            object val = null;
            
            if (value != null)
            {
                string strVal = value.ToString().ToLower();
                if (strVal.Equals("true") || value.Equals(1))
                {
                    val= true;
                }
                else if (strVal.Equals("false") || value.Equals(0))
                {
                    val = false;
                }
                else if (Convert.IsDBNull(value))
                {
                    val = this.NullValue; ;
                }
            }
            this.currentValue = val;
            base.SetColumnValueAtRow(lm, row, val);


            //object obj1 = null;
            //bool flag1 = true;
            //if (flag1.Equals(value))
            //{
            //    obj1 = this.TrueValue;
            //}
            //else
            //{
            //    flag1 = false;
            //    if (flag1.Equals(value))
            //    {
            //        obj1 = this.FalseValue;
            //    }
            //    else if (Convert.IsDBNull(value))
            //    {
            //        obj1 = this.NullValue;
            //    }
            //}
            //this.currentValue = obj1;
            //base.SetColumnValueAtRow(lm, row, obj1);
        }
        /// <summary>
        /// Sets the Grid for the column. 
        /// </summary>
        /// <param name="value"></param>
        protected override void SetGridInColumn(Grid value)
        {
            base.SetGridInColumn(value);
        }

        /// <summary>
        /// When overridden in a derived class, gets the minimum height of a row
        /// </summary>
        /// <returns></returns>
        protected internal override int GetMinimumHeight()
        {
            return (GridBoolColumn.idealCheckSize + 2);
        }
        /// <summary>
        /// When overridden in a derived class, gets the height used for automatically resizing columns.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected internal override int GetPreferredHeight(Graphics g, object value)
        {
            return (GridBoolColumn.idealCheckSize + 2);
        }
        /// <summary>
        /// When overridden in a derived class, gets the width and height of the specified value. The width and height are used when the user navigates to GridTable using the GridColumnStyle
        /// </summary>
        /// <param name="g"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected internal override Size GetPreferredSize(Graphics g, object value)
        {
            return new Size(GridBoolColumn.idealCheckSize + 2, GridBoolColumn.idealCheckSize + 2);
        }
        /// <summary>
        /// Allows the column to free resources when the control it hosts is not needed.
        /// </summary>
        protected internal override void ResetHostControl()
        {
        }

        #endregion

        #region internal events

        internal override bool KeyPress(int rowNum, Keys keyData)
        {
            if ((this.isSelected && (this.editRow == rowNum)) && (!this.IsReadOnly() && ((keyData & Keys.KeyCode) == Keys.Space)))
            {
                this.ToggleValue();
                this.Invalidate();
                return true;
            }
            return base.KeyPress(rowNum, keyData);
        }

        internal override bool MouseDown(int rowNum, int x, int y)
        {
            base.MouseDown(rowNum, x, y);

            //this.GridTableStyle.Grid.CurrentRowIndex = rowNum;
            //base.editRow = rowNum;

            if ((this.isSelected && (this.editRow == rowNum)) && !this.IsReadOnly())
            {
                //this.isEditing = true;
                //this.ToggleValue();
                //this.Invalidate();
                return true;
            }
            return false;
        }

        internal override void MouseUp(int rowNum, MouseEventArgs e)
        {
            base.MouseUp(rowNum, e);
            if (e.Button == MouseButtons.Right)
            {
                if (!this.GridTableStyle.Grid.AllowColumnContextMenu)
                    return;
                GridColumnContexMenu mnu = this.GridTableStyle.Grid._GridColumnContexMenu;
                if (mnu == null)
                {
                    mnu = new GridColumnContexMenu(this.GridTableStyle.Grid);
                }
                mnu.Show(this.GridTableStyle.Grid.PointToScreen(new Point(e.X, e.Y)));
            }
            else
            {
                if ((this.isSelected && (this.editRow == rowNum)) && !this.IsReadOnly())
                {
                    //this.isEditing = true;
                    this.ToggleValue();
                    this.Invalidate();
                }
            }
        }
        private void OnAllowNullChanged(EventArgs e)
        {
            if (AllowNullChanged != null)
                AllowNullChanged(this, e);

            //			EventHandler handler1 = base.Events[GridBoolColumn.EventAllowNull] as EventHandler;
            //			if (handler1 != null)
            //			{
            //				handler1(this, e);
            //			}
        }

        //private void OnFalseValueChanged(EventArgs e)
        //{
        //    if (FalseValueChanged != null)
        //        FalseValueChanged(this, e);

        //    //			EventHandler handler1 = base.Events[GridBoolColumn.EventFalseValue] as EventHandler;
        //    //			if (handler1 != null)
        //    //			{
        //    //				handler1(this, e);
        //    //			}
        //}


        //private void OnTrueValueChanged(EventArgs e)
        //{
        //    if (TrueValueChanged != null)
        //        TrueValueChanged(this, e);

        //    //			EventHandler handler1 = base.Events[GridBoolColumn.EventTrueValue] as EventHandler;
        //    //			if (handler1 != null)
        //    //			{
        //    //				handler1(this, e);
        //    //			}
        //}

 
        #endregion

        #region Properties

        /// <summary>
        /// Get Control Current Text
        /// </summary>
        [Browsable(false)]
        public override string Text
        {
            get { return this.currentValue==null?"":currentValue.ToString(); }
            set { this.m_Text = value; }
        }


        private void ToggleValue()
        {
            if ((this.currentValue is bool) && !((bool)this.currentValue))
            {
                this.currentValue = true;
            }
            else if (this.AllowNullValue)
            {
                if (Convert.IsDBNull(this.currentValue))
                {
                    this.currentValue = false;
                }
                else
                {
                    this.currentValue = Convert.DBNull;
                }
            }
            else
            {
                this.currentValue = false;
            }
            this.isEditing = true;
            this.GridTableStyle.dataGrid.ColumnStartedEditing(Rectangle.Empty);
            if (this.CheckedChanged != null)
            {
                this.CheckedChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Get or Set indicating column allow null value
        /// </summary>
        [Browsable(true), Description("GridBooleanColumnAllowNullValue"), DefaultValue(false), Category("Behavior")]
        public bool AllowNullValue
        {
            get
            {
                return this.allowNull;
            }
            set
            {
                if (this.allowNull != value)
                {
                    this.allowNull = value;
                    if (!value && Convert.IsDBNull(this.currentValue))
                    {
                        this.currentValue = false;
                        this.Invalidate();
                    }
                    this.OnAllowNullChanged(EventArgs.Empty);
                }
            }
        }
        ///// <summary>
        ///// Get or Set the false value
        ///// </summary>
        //[DefaultValue(false), TypeConverter(typeof(StringConverter))]
        //public object FalseValue
        //{
        //    get
        //    {
        //        return this.falseValue;
        //    }
        //    set
        //    {
        //        if (!this.falseValue.Equals(value))
        //        {
        //            this.falseValue = value;
        //            this.OnFalseValueChanged(EventArgs.Empty);
        //            this.Invalidate();
        //        }
        //    }
        //}
        /// <summary>
        /// Get or Set the null value
        /// </summary>
        [TypeConverter(typeof(StringConverter))]
        public object NullValue
        {
            get
            {
                return this.nullValue;
            }
            set
            {
                if (!this.nullValue.Equals(value))
                {
                    this.nullValue = value;
                    //this.OnFalseValueChanged(EventArgs.Empty);
                    this.Invalidate();
                }
            }
        }
        ///// <summary>
        ///// Get or Set the true value
        ///// </summary>
        //[DefaultValue(true), TypeConverter(typeof(StringConverter))]
        //public object TrueValue
        //{
        //    get
        //    {
        //        return this.trueValue;
        //    }
        //    set
        //    {
        //        if (!this.trueValue.Equals(value))
        //        {
        //            this.trueValue = value;
        //            this.OnTrueValueChanged(EventArgs.Empty);
        //            this.Invalidate();
        //        }
        //    }
        //}

 
        #endregion

        #region Events

        //		public event EventHandler AllowNullChanged
        //		{
        //			add
        //			{
        //				base.AddHandler(GridBoolColumn.EventAllowNull, value);
        //			}
        //			remove
        //			{
        //				base.RemoveHandler(GridBoolColumn.EventAllowNull, value);
        //			}
        //		}
        // 
        //		public event EventHandler FalseValueChanged
        //		{
        //			add
        //			{
        //				base.AddHandler(GridBoolColumn.EventFalseValue, value);
        //			}
        //			remove
        //			{
        //				base.RemoveHandler(GridBoolColumn.EventFalseValue, value);
        //			}
        //		}
        #endregion

        /// <summary>
        /// Get current value
        /// </summary>
        /// <returns></returns>
        public bool CurrentValue()
        {
            return Types.ToBool(this.GridTableStyle.Grid[this.MappingName], false);
        }
        /// <summary>
        /// Get or Set indicating the column is bound
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
    }
}
