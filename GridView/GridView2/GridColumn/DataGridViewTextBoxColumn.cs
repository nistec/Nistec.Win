namespace MControl.GridView
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Text;

    /// <summary>Hosts a collection of <see cref="T:MControl.GridView.GridTextBoxCell"></see> cells.</summary>
    /// <filterpriority>2</filterpriority>
    [ToolboxBitmap(typeof(GridTextBoxColumn), "GridTextBoxColumn.bmp")]
    public class GridTextBoxColumn : GridColumn
    {
        private const int gridTEXTBOXCOLUMN_maxInputLength = 0x7fff;

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridTextBoxColumn"></see> class to the default state.</summary>
        public GridTextBoxColumn() : base(new GridTextBoxCell())
        {
            this.SortMode = GridColumnSortMode.Automatic;
        }

        /// <filterpriority>1</filterpriority>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(0x40);
            builder.Append("GridTextBoxColumn { Name=");
            builder.Append(base.Name);
            builder.Append(", Index=");
            builder.Append(base.Index.ToString(CultureInfo.CurrentCulture));
            builder.Append(" }");
            return builder.ToString();
        }

        /// <summary>Gets or sets the template used to model cell appearance.</summary>
        /// <returns>A <see cref="T:MControl.GridView.GridCell"></see> that all other cells in the column are modeled after.</returns>
        /// <exception cref="T:System.InvalidCastException">The set type is not compatible with type <see cref="T:MControl.GridView.GridTextBoxCell"></see>. </exception>
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
                if ((value != null) && !(value is GridTextBoxCell))
                {
                    throw new InvalidCastException(MControl.GridView.RM.GetString("GridTypeColumn_WrongCellTemplateType", new object[] { "MControl.GridView.GridTextBoxCell" }));
                }
                base.CellTemplate = value;
            }
        }

        /// <summary>Gets or sets the maximum number of characters that can be entered into the text box.</summary>
        /// <returns>The maximum number of characters that can be entered into the text box; the default value is 32767.</returns>
        /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridTextBoxColumn.CellTemplate"></see> property is null.</exception>
        [Description("Grid_TextBoxColumnMaxInputLength"), DefaultValue(0x7fff), Category("Behavior")]
        public int MaxInputLength
        {
            get
            {
                if (this.TextBoxCellTemplate == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridColumn_CellTemplateRequired"));
                }
                return this.TextBoxCellTemplate.MaxInputLength;
            }
            set
            {
                if (this.MaxInputLength != value)
                {
                    this.TextBoxCellTemplate.MaxInputLength = value;
                    if (base.Grid != null)
                    {
                        GridRowCollection rows = base.Grid.Rows;
                        int count = rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            GridTextBoxCell cell = rows.SharedRow(i).Cells[base.Index] as GridTextBoxCell;
                            if (cell != null)
                            {
                                cell.MaxInputLength = value;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>Gets or sets the sort mode for the column.</summary>
        /// <returns>A <see cref="T:MControl.GridView.GridColumnSortMode"></see> that specifies the criteria used to order the rows based on the cell values in a column.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [DefaultValue(1)]
        public GridColumnSortMode SortMode
        {
            get
            {
                return base.SortMode;
            }
            set
            {
                base.SortMode = value;
            }
        }

        private GridTextBoxCell TextBoxCellTemplate
        {
            get
            {
                return (GridTextBoxCell) this.CellTemplate;
            }
        }
    }
}

