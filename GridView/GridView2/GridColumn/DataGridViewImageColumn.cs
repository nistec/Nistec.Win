namespace MControl.GridView
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Text;

    /// <summary>Hosts a collection of <see cref="T:MControl.GridView.GridImageCell"></see> objects.</summary>
    /// <filterpriority>2</filterpriority>
    [ToolboxBitmap(typeof(GridImageColumn), "GridImageColumn.bmp")]
    public class GridImageColumn : GridColumn
    {
        private static System.Type columnType = typeof(GridImageColumn);
        private System.Drawing.Icon icon;
        private System.Drawing.Image image;

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridImageColumn"></see> class, configuring it for use with cell values of type <see cref="T:System.Drawing.Image"></see>.</summary>
        public GridImageColumn() : this(false)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridImageColumn"></see> class, optionally configuring it for use with <see cref="T:System.Drawing.Icon"></see> cell values.</summary>
        /// <param name="valuesAreIcons">true to indicate that the <see cref="P:MControl.GridView.GridCell.Value"></see> property of cells in this column will be set to values of type <see cref="T:System.Drawing.Icon"></see>; false to indicate that they will be set to values of type <see cref="T:System.Drawing.Image"></see>.</param>
        public GridImageColumn(bool valuesAreIcons) : base(new GridImageCell(valuesAreIcons))
        {
            GridCellStyle style = new GridCellStyle();
            style.AlignmentInternal = GridContentAlignment.MiddleCenter;
            if (valuesAreIcons)
            {
                style.NullValue = GridImageCell.ErrorIcon;
            }
            else
            {
                style.NullValue = GridImageCell.ErrorBitmap;
            }
            this.DefaultCellStyle = style;
        }

        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public override object Clone()
        {
            GridImageColumn column;
            System.Type type = base.GetType();
            if (type == columnType)
            {
                column = new GridImageColumn();
            }
            else
            {
                column = (GridImageColumn) Activator.CreateInstance(type);
            }
            if (column != null)
            {
                base.CloneInternal(column);
                column.Icon = this.icon;
                column.Image = this.image;
            }
            return column;
        }

        private bool ShouldSerializeDefaultCellStyle()
        {
            GridImageCell cellTemplate = this.CellTemplate as GridImageCell;
            if (cellTemplate != null)
            {
                object errorIcon;
                if (!base.HasDefaultCellStyle)
                {
                    return false;
                }
                if (cellTemplate.ValueIsIcon)
                {
                    errorIcon = GridImageCell.ErrorIcon;
                }
                else
                {
                    errorIcon = GridImageCell.ErrorBitmap;
                }
                GridCellStyle defaultCellStyle = this.DefaultCellStyle;
                if ((((defaultCellStyle.BackColor.IsEmpty && defaultCellStyle.ForeColor.IsEmpty) && (defaultCellStyle.SelectionBackColor.IsEmpty && defaultCellStyle.SelectionForeColor.IsEmpty)) && (((defaultCellStyle.Font == null) && errorIcon.Equals(defaultCellStyle.NullValue)) && (defaultCellStyle.IsDataSourceNullValueDefault && string.IsNullOrEmpty(defaultCellStyle.Format)))) && ((defaultCellStyle.FormatProvider.Equals(CultureInfo.CurrentCulture) && (defaultCellStyle.Alignment == GridContentAlignment.MiddleCenter)) && ((defaultCellStyle.WrapMode == GridTriState.NotSet) && (defaultCellStyle.Tag == null))))
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
            builder.Append("GridImageColumn { Name=");
            builder.Append(base.Name);
            builder.Append(", Index=");
            builder.Append(base.Index.ToString(CultureInfo.CurrentCulture));
            builder.Append(" }");
            return builder.ToString();
        }

        /// <summary>Gets or sets the template used to create new cells.</summary>
        /// <returns>A <see cref="T:MControl.GridView.GridCell"></see> that all other cells in the column are modeled after.</returns>
        /// <exception cref="T:System.InvalidCastException">The set type is not compatible with type <see cref="T:MControl.GridView.GridImageCell"></see>. </exception>
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
                if ((value != null) && !(value is GridImageCell))
                {
                    throw new InvalidCastException(MControl.GridView.RM.GetString("GridTypeColumn_WrongCellTemplateType", new object[] { "MControl.GridView.GridImageCell" }));
                }
                base.CellTemplate = value;
            }
        }

        /// <summary>Gets or sets the column's default cell style.</summary>
        /// <returns>The <see cref="T:MControl.GridView.GridCellStyle"></see> to be applied as the default style.</returns>
        [Category("Appearance"), Browsable(true), Description("Grid_ColumnDefaultCellStyle")]
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

        /// <summary>Gets or sets a string that describes the column's image. </summary>
        /// <returns>The textual description of the column image. The default is <see cref="F:System.String.Empty"></see>.</returns>
        /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridImageColumn.CellTemplate"></see> property is null.</exception>
        /// <filterpriority>1</filterpriority>
        [Description("GridImageColumn_Description"), Browsable(true), DefaultValue(""), Category("Appearance")]
        public string Description
        {
            get
            {
                if (this.CellTemplate == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridColumn_CellTemplateRequired"));
                }
                return this.ImageCellTemplate.Description;
            }
            set
            {
                if (this.CellTemplate == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridColumn_CellTemplateRequired"));
                }
                this.ImageCellTemplate.Description = value;
                if (base.Grid != null)
                {
                    GridRowCollection rows = base.Grid.Rows;
                    int count = rows.Count;
                    for (int i = 0; i < count; i++)
                    {
                        GridImageCell cell = rows.SharedRow(i).Cells[base.Index] as GridImageCell;
                        if (cell != null)
                        {
                            cell.Description = value;
                        }
                    }
                }
            }
        }

        /// <summary>Gets or sets the icon displayed in the cells of this column when the cell's <see cref="P:MControl.GridView.GridCell.Value"></see> property is not set and the cell's <see cref="P:MControl.GridView.GridImageCell.ValueIsIcon"></see> property is set to true.</summary>
        /// <returns>The <see cref="T:System.Drawing.Icon"></see> to display. The default is null.</returns>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Drawing.Icon Icon
        {
            get
            {
                return this.icon;
            }
            set
            {
                this.icon = value;
                if (base.Grid != null)
                {
                    base.Grid.OnColumnCommonChange(base.Index);
                }
            }
        }

        /// <summary>Gets or sets the image displayed in the cells of this column when the cell's <see cref="P:MControl.GridView.GridCell.Value"></see> property is not set and the cell's <see cref="P:MControl.GridView.GridImageCell.ValueIsIcon"></see> property is set to false.</summary>
        /// <returns>The <see cref="T:System.Drawing.Image"></see> to display. The default is null.</returns>
        /// <filterpriority>1</filterpriority>
        [Category("Appearance"), DefaultValue((string) null), Description("GridImageColumn_Image")]
        public System.Drawing.Image Image
        {
            get
            {
                return this.image;
            }
            set
            {
                this.image = value;
                if (base.Grid != null)
                {
                    base.Grid.OnColumnCommonChange(base.Index);
                }
            }
        }

        private GridImageCell ImageCellTemplate
        {
            get
            {
                return (GridImageCell) this.CellTemplate;
            }
        }

        /// <summary>Gets or sets the image layout in the cells for this column.</summary>
        /// <returns>A <see cref="T:MControl.GridView.GridImageCellLayout"></see> that specifies the cell layout. The default is <see cref="F:MControl.GridView.GridImageCellLayout.Normal"></see>.</returns>
        /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridImageColumn.CellTemplate"></see> property is null. </exception>
        /// <filterpriority>1</filterpriority>
        [Description("GridImageColumn_ImageLayout"), DefaultValue(1), Category("Appearance")]
        public GridImageCellLayout ImageLayout
        {
            get
            {
                if (this.CellTemplate == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridColumn_CellTemplateRequired"));
                }
                GridImageCellLayout imageLayout = this.ImageCellTemplate.ImageLayout;
                if (imageLayout == GridImageCellLayout.NotSet)
                {
                    imageLayout = GridImageCellLayout.Normal;
                }
                return imageLayout;
            }
            set
            {
                if (this.ImageLayout != value)
                {
                    this.ImageCellTemplate.ImageLayout = value;
                    if (base.Grid != null)
                    {
                        GridRowCollection rows = base.Grid.Rows;
                        int count = rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            GridImageCell cell = rows.SharedRow(i).Cells[base.Index] as GridImageCell;
                            if (cell != null)
                            {
                                cell.ImageLayoutInternal = value;
                            }
                        }
                        base.Grid.OnColumnCommonChange(base.Index);
                    }
                }
            }
        }

        /// <summary>Gets or sets a value indicating whether cells in this column display <see cref="T:System.Drawing.Icon"></see> values.</summary>
        /// <returns>true if cells display values of type <see cref="T:System.Drawing.Icon"></see>; false if cells display values of type <see cref="T:System.Drawing.Image"></see>. The default is false.</returns>
        /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridImageColumn.CellTemplate"></see> property is null.</exception>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public bool ValuesAreIcons
        {
            get
            {
                if (this.ImageCellTemplate == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridColumn_CellTemplateRequired"));
                }
                return this.ImageCellTemplate.ValueIsIcon;
            }
            set
            {
                if (this.ValuesAreIcons != value)
                {
                    this.ImageCellTemplate.ValueIsIconInternal = value;
                    if (base.Grid != null)
                    {
                        GridRowCollection rows = base.Grid.Rows;
                        int count = rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            GridImageCell cell = rows.SharedRow(i).Cells[base.Index] as GridImageCell;
                            if (cell != null)
                            {
                                cell.ValueIsIconInternal = value;
                            }
                        }
                        base.Grid.OnColumnCommonChange(base.Index);
                    }
                    if ((value && (this.DefaultCellStyle.NullValue is Bitmap)) && (((Bitmap) this.DefaultCellStyle.NullValue) == GridImageCell.ErrorBitmap))
                    {
                        this.DefaultCellStyle.NullValue = GridImageCell.ErrorIcon;
                    }
                    else if ((!value && (this.DefaultCellStyle.NullValue is System.Drawing.Icon)) && (((System.Drawing.Icon) this.DefaultCellStyle.NullValue) == GridImageCell.ErrorIcon))
                    {
                        this.DefaultCellStyle.NullValue = GridImageCell.ErrorBitmap;
                    }
                }
            }
        }
    }
}

