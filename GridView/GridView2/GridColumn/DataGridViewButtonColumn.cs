namespace MControl.GridView
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Text;

    /// <summary>Hosts a collection of <see cref="T:MControl.GridView.GridButtonCell"></see> objects.</summary>
    /// <filterpriority>2</filterpriority>
    [ToolboxBitmap(typeof(GridButtonColumn), "GridButtonColumn.bmp")]
    public class GridButtonColumn : GridColumn
    {
        private static System.Type columnType = typeof(GridButtonColumn);
        private string text;

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridButtonColumn"></see> class to the default state.</summary>
        public GridButtonColumn() : base(new GridButtonCell())
        {
            GridCellStyle style = new GridCellStyle();
            style.AlignmentInternal = GridContentAlignment.MiddleCenter;
            this.DefaultCellStyle = style;
        }

        /// <summary>Creates an exact copy of this column.</summary>
        /// <returns>An <see cref="T:System.Object"></see> that represents the cloned <see cref="T:MControl.GridView.GridButtonColumn"></see>.</returns>
        /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridButtonColumn.CellTemplate"></see> property is null. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public override object Clone()
        {
            GridButtonColumn column;
            System.Type type = base.GetType();
            if (type == columnType)
            {
                column = new GridButtonColumn();
            }
            else
            {
                column = (GridButtonColumn) Activator.CreateInstance(type);
            }
            if (column != null)
            {
                base.CloneInternal(column);
                column.Text = this.text;
            }
            return column;
        }

        private bool ShouldSerializeDefaultCellStyle()
        {
            if (!base.HasDefaultCellStyle)
            {
                return false;
            }
            GridCellStyle defaultCellStyle = this.DefaultCellStyle;
            if ((((defaultCellStyle.BackColor.IsEmpty && defaultCellStyle.ForeColor.IsEmpty) && (defaultCellStyle.SelectionBackColor.IsEmpty && defaultCellStyle.SelectionForeColor.IsEmpty)) && (((defaultCellStyle.Font == null) && defaultCellStyle.IsNullValueDefault) && (defaultCellStyle.IsDataSourceNullValueDefault && string.IsNullOrEmpty(defaultCellStyle.Format)))) && ((defaultCellStyle.FormatProvider.Equals(CultureInfo.CurrentCulture) && (defaultCellStyle.Alignment == GridContentAlignment.MiddleCenter)) && ((defaultCellStyle.WrapMode == GridTriState.NotSet) && (defaultCellStyle.Tag == null))))
            {
                return !defaultCellStyle.Padding.Equals(Padding.Empty);
            }
            return true;
        }

        /// <filterpriority>1</filterpriority>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(0x40);
            builder.Append("GridButtonColumn { Name=");
            builder.Append(base.Name);
            builder.Append(", Index=");
            builder.Append(base.Index.ToString(CultureInfo.CurrentCulture));
            builder.Append(" }");
            return builder.ToString();
        }

        /// <summary>Gets or sets the template used to create new cells.</summary>
        /// <returns>A <see cref="T:MControl.GridView.GridCell"></see> that all other cells in the column are modeled after.</returns>
        /// <exception cref="T:System.InvalidCastException">The specified value when setting this property could not be cast to a <see cref="T:MControl.GridView.GridButtonCell"></see>. </exception>
        /// <filterpriority>1</filterpriority>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public override GridCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                if ((value != null) && !(value is GridButtonCell))
                {
                    throw new InvalidCastException(MControl.GridView.RM.GetString("GridTypeColumn_WrongCellTemplateType", new object[] { "MControl.GridView.GridButtonCell" }));
                }
                base.CellTemplate = value;
            }
        }

        /// <summary>Gets or sets the column's default cell style.</summary>
        /// <returns>The <see cref="T:MControl.GridView.GridCellStyle"></see> to be applied as the default style.</returns>
        [Description("Grid_ColumnDefaultCellStyle"), Category("Appearance"), Browsable(true)]
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

        /// <summary>Gets or sets the flat-style appearance of the button cells in the column.</summary>
        /// <returns>A <see cref="T:System.Windows.Forms.FlatStyle"></see> value indicating the appearance of the buttons in the column. The default is <see cref="F:System.Windows.Forms.FlatStyle.Standard"></see>.</returns>
        /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridButtonColumn.CellTemplate"></see> property is null. </exception>
        /// <filterpriority>1</filterpriority>
        [Category("Appearance"), Description("Grid_ButtonColumnFlatStyle"), DefaultValue(2)]
        public System.Windows.Forms.FlatStyle FlatStyle
        {
            get
            {
                if (this.CellTemplate == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridColumn_CellTemplateRequired"));
                }
                return ((GridButtonCell) this.CellTemplate).FlatStyle;
            }
            set
            {
                if (this.FlatStyle != value)
                {
                    ((GridButtonCell) this.CellTemplate).FlatStyle = value;
                    if (base.Grid != null)
                    {
                        GridRowCollection rows = base.Grid.Rows;
                        int count = rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            GridButtonCell cell = rows.SharedRow(i).Cells[base.Index] as GridButtonCell;
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

        /// <summary>Gets or sets the default text displayed on the button cell.</summary>
        /// <returns>A <see cref="T:System.String"></see> that contains the text. The default is <see cref="F:System.String.Empty"></see>.</returns>
        /// <exception cref="T:System.InvalidOperationException">When setting this property, the value of the <see cref="P:MControl.GridView.GridButtonColumn.CellTemplate"></see> property is null. </exception>
        /// <filterpriority>1</filterpriority>
        [Category("Appearance"), Description("Grid_ButtonColumnText"), DefaultValue((string) null)]
        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                if (!string.Equals(value, this.text, StringComparison.Ordinal))
                {
                    this.text = value;
                    if (base.Grid != null)
                    {
                        if (this.UseColumnTextForButtonValue)
                        {
                            base.Grid.OnColumnCommonChange(base.Index);
                        }
                        else
                        {
                            GridRowCollection rows = base.Grid.Rows;
                            int count = rows.Count;
                            for (int i = 0; i < count; i++)
                            {
                                GridButtonCell cell = rows.SharedRow(i).Cells[base.Index] as GridButtonCell;
                                if ((cell != null) && cell.UseColumnTextForButtonValue)
                                {
                                    base.Grid.OnColumnCommonChange(base.Index);
                                    return;
                                }
                            }
                            base.Grid.InvalidateColumn(base.Index);
                        }
                    }
                }
            }
        }

        /// <summary>Gets or sets a value indicating whether the <see cref="P:MControl.GridView.GridButtonColumn.Text"></see> property value is displayed as the button text for cells in this column.</summary>
        /// <returns>true if the <see cref="P:MControl.GridView.GridButtonColumn.Text"></see> property value is displayed on buttons in the column; false if the <see cref="P:MControl.GridView.GridCell.FormattedValue"></see> property value of each cell is displayed on its button. The default is false.</returns>
        /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridButtonColumn.CellTemplate"></see> property is null.</exception>
        [Category("Appearance"), DefaultValue(false), Description("Grid_ButtonColumnUseColumnTextForButtonValue")]
        public bool UseColumnTextForButtonValue
        {
            get
            {
                if (this.CellTemplate == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridColumn_CellTemplateRequired"));
                }
                return ((GridButtonCell) this.CellTemplate).UseColumnTextForButtonValue;
            }
            set
            {
                if (this.UseColumnTextForButtonValue != value)
                {
                    ((GridButtonCell) this.CellTemplate).UseColumnTextForButtonValueInternal = value;
                    if (base.Grid != null)
                    {
                        GridRowCollection rows = base.Grid.Rows;
                        int count = rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            GridButtonCell cell = rows.SharedRow(i).Cells[base.Index] as GridButtonCell;
                            if (cell != null)
                            {
                                cell.UseColumnTextForButtonValueInternal = value;
                            }
                        }
                        base.Grid.OnColumnCommonChange(base.Index);
                    }
                }
            }
        }
    }
}

