namespace MControl.GridView
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Text;

    /// <summary>Hosts a collection of <see cref="T:MControl.GridView.GridCheckBoxCell"></see> objects.</summary>
    /// <filterpriority>2</filterpriority>
    [ToolboxBitmap(typeof(GridCheckBoxColumn), "GridCheckBoxColumn.bmp")]
    public class GridCheckBoxColumn : GridColumn
    {
        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridCheckBoxColumn"></see> class to the default state.</summary>
        public GridCheckBoxColumn() : this(false)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridCheckBoxColumn"></see> and configures it to display check boxes with two or three states. </summary>
        /// <param name="threeState">true to display check boxes with three states; false to display check boxes with two states. </param>
        public GridCheckBoxColumn(bool threeState) : base(new GridCheckBoxCell(threeState))
        {
            GridCellStyle style = new GridCellStyle();
            style.AlignmentInternal = GridContentAlignment.MiddleCenter;
            if (threeState)
            {
                style.NullValue = CheckState.Indeterminate;
            }
            else
            {
                style.NullValue = false;
            }
            this.DefaultCellStyle = style;
        }

        private bool ShouldSerializeDefaultCellStyle()
        {
            GridCheckBoxCell cellTemplate = this.CellTemplate as GridCheckBoxCell;
            if (cellTemplate != null)
            {
                object indeterminate;
                if (cellTemplate.ThreeState)
                {
                    indeterminate = CheckState.Indeterminate;
                }
                else
                {
                    indeterminate = false;
                }
                if (!base.HasDefaultCellStyle)
                {
                    return false;
                }
                GridCellStyle defaultCellStyle = this.DefaultCellStyle;
                if ((((defaultCellStyle.BackColor.IsEmpty && defaultCellStyle.ForeColor.IsEmpty) && (defaultCellStyle.SelectionBackColor.IsEmpty && defaultCellStyle.SelectionForeColor.IsEmpty)) && (((defaultCellStyle.Font == null) && defaultCellStyle.NullValue.Equals(indeterminate)) && (defaultCellStyle.IsDataSourceNullValueDefault && string.IsNullOrEmpty(defaultCellStyle.Format)))) && ((defaultCellStyle.FormatProvider.Equals(CultureInfo.CurrentCulture) && (defaultCellStyle.Alignment == GridContentAlignment.MiddleCenter)) && ((defaultCellStyle.WrapMode == GridTriState.NotSet) && (defaultCellStyle.Tag == null))))
                {
                    return !defaultCellStyle.Padding.Equals(Padding.Empty);
                }
            }
            return true;
        }

        /// <filterpriority>1</filterpriority>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(0x40);
            builder.Append("GridCheckBoxColumn { Name=");
            builder.Append(base.Name);
            builder.Append(", Index=");
            builder.Append(base.Index.ToString(CultureInfo.CurrentCulture));
            builder.Append(" }");
            return builder.ToString();
        }

        /// <summary>Gets or sets the template used to create new cells.</summary>
        /// <returns>A <see cref="T:MControl.GridView.GridCell"></see> that all other cells in the column are modeled after. The default value is a new <see cref="T:MControl.GridView.GridCheckBoxCell"></see> instance.</returns>
        /// <exception cref="T:System.InvalidCastException">The property is set to a value that is not of type <see cref="T:MControl.GridView.GridCheckBoxCell"></see>. </exception>
        /// <filterpriority>1</filterpriority>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override GridCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                if ((value != null) && !(value is GridCheckBoxCell))
                {
                    throw new InvalidCastException(MControl.GridView.RM.GetString("GridTypeColumn_WrongCellTemplateType", new object[] { "MControl.GridView.GridCheckBoxCell" }));
                }
                base.CellTemplate = value;
            }
        }

        private GridCheckBoxCell CheckBoxCellTemplate
        {
            get
            {
                return (GridCheckBoxCell) this.CellTemplate;
            }
        }

        /// <summary>Gets or sets the column's default cell style.</summary>
        /// <returns>The <see cref="T:MControl.GridView.GridCellStyle"></see> to be applied as the default style.</returns>
        [Browsable(true), Description("Grid_ColumnDefaultCellStyle"), Category("Appearance")]
        public override GridCellStyle DefaultCellStyle
        {
            get
            {
                return base.DefaultCellStyle;
            }
            set
            {
                base.DefaultCellStyle = value;
            }
        }

        /// <summary>Gets or sets the underlying value corresponding to a cell value of false, which appears as an unchecked box.</summary>
        /// <returns>An <see cref="T:System.Object"></see> representing a value that the cells in this column will treat as a false value. The default is null.</returns>
        /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridCheckBoxColumn.CellTemplate"></see> property is null. </exception>
        /// <filterpriority>1</filterpriority>
        [Description("Grid_CheckBoxColumnFalseValue"), TypeConverter(typeof(StringConverter)), Category("Data"), DefaultValue((string) null)]
        public object FalseValue
        {
            get
            {
                if (this.CheckBoxCellTemplate == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridColumn_CellTemplateRequired"));
                }
                return this.CheckBoxCellTemplate.FalseValue;
            }
            set
            {
                if (this.FalseValue != value)
                {
                    this.CheckBoxCellTemplate.FalseValueInternal = value;
                    if (base.Grid != null)
                    {
                        GridRowCollection rows = base.Grid.Rows;
                        int count = rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            GridCheckBoxCell cell = rows.SharedRow(i).Cells[base.Index] as GridCheckBoxCell;
                            if (cell != null)
                            {
                                cell.FalseValueInternal = value;
                            }
                        }
                        base.Grid.InvalidateColumn(base.Index);
                    }
                }
            }
        }

        /// <summary>Gets or sets the flat style appearance of the check box cells.</summary>
        /// <returns>A <see cref="T:System.Windows.Forms.FlatStyle"></see> value indicating the appearance of cells in the column. The default is <see cref="F:System.Windows.Forms.FlatStyle.Standard"></see>.</returns>
        /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridCheckBoxColumn.CellTemplate"></see> property is null. </exception>
        /// <filterpriority>1</filterpriority>
        [DefaultValue(2), Category("Appearance"), Description("Grid_CheckBoxColumnFlatStyle")]
        public System.Windows.Forms.FlatStyle FlatStyle
        {
            get
            {
                if (this.CheckBoxCellTemplate == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridColumn_CellTemplateRequired"));
                }
                return this.CheckBoxCellTemplate.FlatStyle;
            }
            set
            {
                if (this.FlatStyle != value)
                {
                    this.CheckBoxCellTemplate.FlatStyle = value;
                    if (base.Grid != null)
                    {
                        GridRowCollection rows = base.Grid.Rows;
                        int count = rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            GridCheckBoxCell cell = rows.SharedRow(i).Cells[base.Index] as GridCheckBoxCell;
                            if (cell != null)
                            {
                                cell.FlatStyleInternal = value;
                            }
                        }
                        base.Grid.OnColumnCommonChange(base.Index);
                    }
                }
            }
        }

        /// <summary>Gets or sets the underlying value corresponding to an indeterminate or null cell value, which appears as a disabled checkbox.</summary>
        /// <returns>An <see cref="T:System.Object"></see> representing a value that the cells in this column will treat as an indeterminate value. The default is null.</returns>
        /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridCheckBoxColumn.CellTemplate"></see> property is null. </exception>
        /// <filterpriority>1</filterpriority>
        [Category("Data"), TypeConverter(typeof(StringConverter)), DefaultValue((string) null), Description("Grid_CheckBoxColumnIndeterminateValue")]
        public object IndeterminateValue
        {
            get
            {
                if (this.CheckBoxCellTemplate == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridColumn_CellTemplateRequired"));
                }
                return this.CheckBoxCellTemplate.IndeterminateValue;
            }
            set
            {
                if (this.IndeterminateValue != value)
                {
                    this.CheckBoxCellTemplate.IndeterminateValueInternal = value;
                    if (base.Grid != null)
                    {
                        GridRowCollection rows = base.Grid.Rows;
                        int count = rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            GridCheckBoxCell cell = rows.SharedRow(i).Cells[base.Index] as GridCheckBoxCell;
                            if (cell != null)
                            {
                                cell.IndeterminateValueInternal = value;
                            }
                        }
                        base.Grid.InvalidateColumn(base.Index);
                    }
                }
            }
        }

        /// <summary>Gets or sets a value indicating whether the hosted check box cells will allow three check states rather than two.</summary>
        /// <returns>true if the hosted <see cref="T:MControl.GridView.GridCheckBoxCell"></see> objects are able to have a third, indeterminate, state; otherwise, false. The default is false.</returns>
        /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridCheckBoxColumn.CellTemplate"></see> property is null.</exception>
        /// <filterpriority>1</filterpriority>
        [DefaultValue(false), Description("Grid_CheckBoxColumnThreeState"), Category("Behavior")]
        public bool ThreeState
        {
            get
            {
                if (this.CheckBoxCellTemplate == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridColumn_CellTemplateRequired"));
                }
                return this.CheckBoxCellTemplate.ThreeState;
            }
            set
            {
                if (this.ThreeState != value)
                {
                    this.CheckBoxCellTemplate.ThreeStateInternal = value;
                    if (base.Grid != null)
                    {
                        GridRowCollection rows = base.Grid.Rows;
                        int count = rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            GridCheckBoxCell cell = rows.SharedRow(i).Cells[base.Index] as GridCheckBoxCell;
                            if (cell != null)
                            {
                                cell.ThreeStateInternal = value;
                            }
                        }
                        base.Grid.InvalidateColumn(base.Index);
                    }
                    if ((value && (this.DefaultCellStyle.NullValue is bool)) && !((bool) this.DefaultCellStyle.NullValue))
                    {
                        this.DefaultCellStyle.NullValue = CheckState.Indeterminate;
                    }
                    else if ((!value && (this.DefaultCellStyle.NullValue is CheckState)) && (((CheckState) this.DefaultCellStyle.NullValue) == CheckState.Indeterminate))
                    {
                        this.DefaultCellStyle.NullValue = false;
                    }
                }
            }
        }

        /// <summary>Gets or sets the underlying value corresponding to a cell value of true, which appears as a checked box.</summary>
        /// <returns>An <see cref="T:System.Object"></see> representing a value that the cell will treat as a true value. The default is null.</returns>
        /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridCheckBoxColumn.CellTemplate"></see> property is null.</exception>
        /// <filterpriority>1</filterpriority>
        [TypeConverter(typeof(StringConverter)), Description("Grid_CheckBoxColumnTrueValue"), Category("Data"), DefaultValue((string) null)]
        public object TrueValue
        {
            get
            {
                if (this.CheckBoxCellTemplate == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridColumn_CellTemplateRequired"));
                }
                return this.CheckBoxCellTemplate.TrueValue;
            }
            set
            {
                if (this.TrueValue != value)
                {
                    this.CheckBoxCellTemplate.TrueValueInternal = value;
                    if (base.Grid != null)
                    {
                        GridRowCollection rows = base.Grid.Rows;
                        int count = rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            GridCheckBoxCell cell = rows.SharedRow(i).Cells[base.Index] as GridCheckBoxCell;
                            if (cell != null)
                            {
                                cell.TrueValueInternal = value;
                            }
                        }
                        base.Grid.InvalidateColumn(base.Index);
                    }
                }
            }
        }
    }
}

