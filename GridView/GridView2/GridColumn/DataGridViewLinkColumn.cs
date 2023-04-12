namespace MControl.GridView
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Text;

    /// <summary>Represents a column of cells that contain links in a <see cref="T:MControl.GridView.Grid"></see> control. </summary>
    /// <filterpriority>2</filterpriority>
    [ToolboxBitmap(typeof(GridLinkColumn), "GridLinkColumn.bmp")]
    public class GridLinkColumn : GridColumn
    {
        private static System.Type columnType = typeof(GridLinkColumn);
        private string text;

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridLinkColumn"></see> class. </summary>
        public GridLinkColumn() : base(new GridLinkCell())
        {
        }

        /// <summary>Creates an exact copy of this column.</summary>
        /// <returns>An <see cref="T:System.Object"></see> that represents the cloned <see cref="T:MControl.GridView.GridLinkColumn"></see>.</returns>
        /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridLinkColumn.CellTemplate"></see> property is null. </exception>
        public override object Clone()
        {
            GridLinkColumn column;
            System.Type type = base.GetType();
            if (type == columnType)
            {
                column = new GridLinkColumn();
            }
            else
            {
                column = (GridLinkColumn) Activator.CreateInstance(type);
            }
            if (column != null)
            {
                base.CloneInternal(column);
                column.Text = this.text;
            }
            return column;
        }

        private bool ShouldSerializeActiveLinkColor()
        {
            return !this.ActiveLinkColor.Equals(LinkUtilities.IEActiveLinkColor);
        }

        private bool ShouldSerializeLinkColor()
        {
            return !this.LinkColor.Equals(LinkUtilities.IELinkColor);
        }

        private bool ShouldSerializeVisitedLinkColor()
        {
            return !this.VisitedLinkColor.Equals(LinkUtilities.IEVisitedLinkColor);
        }

        /// <filterpriority>1</filterpriority>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(0x40);
            builder.Append("GridLinkColumn { Name=");
            builder.Append(base.Name);
            builder.Append(", Index=");
            builder.Append(base.Index.ToString(CultureInfo.CurrentCulture));
            builder.Append(" }");
            return builder.ToString();
        }

        /// <summary>Gets or sets the color used to display an active link within cells in the column. </summary>
        /// <returns>A <see cref="T:System.Drawing.Color"></see> that represents the color used to display a link that is being selected. The default value is the user's Internet Explorer setting for the color of links in the hover state.</returns>
        /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridLinkColumn.CellTemplate"></see> property is null.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" /></PermissionSet>
        [Category("Appearance"), Description("Grid_LinkColumnActiveLinkColor")]
        public Color ActiveLinkColor
        {
            get
            {
                if (this.CellTemplate == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridColumn_CellTemplateRequired"));
                }
                return ((GridLinkCell) this.CellTemplate).ActiveLinkColor;
            }
            set
            {
                if (!this.ActiveLinkColor.Equals(value))
                {
                    ((GridLinkCell) this.CellTemplate).ActiveLinkColorInternal = value;
                    if (base.Grid != null)
                    {
                        GridRowCollection rows = base.Grid.Rows;
                        int count = rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            GridLinkCell cell = rows.SharedRow(i).Cells[base.Index] as GridLinkCell;
                            if (cell != null)
                            {
                                cell.ActiveLinkColorInternal = value;
                            }
                        }
                        base.Grid.InvalidateColumn(base.Index);
                    }
                }
            }
        }

        /// <summary>Gets or sets the template used to create new cells.</summary>
        /// <returns>A <see cref="T:MControl.GridView.GridCell"></see> that all other cells in the column are modeled after. The default value is a new <see cref="T:MControl.GridView.GridLinkCell"></see> instance.</returns>
        /// <exception cref="T:System.InvalidCastException">When setting this property to a value that is not of type <see cref="T:MControl.GridView.GridLinkCell"></see>.</exception>
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
                if ((value != null) && !(value is GridLinkCell))
                {
                    throw new InvalidCastException(MControl.GridView.RM.GetString("GridTypeColumn_WrongCellTemplateType", new object[] { "MControl.GridView.GridLinkCell" }));
                }
                base.CellTemplate = value;
            }
        }

        /// <summary>Gets or sets a value that represents the behavior of links within cells in the column.</summary>
        /// <returns>A <see cref="T:System.Windows.Forms.LinkBehavior"></see> value indicating the link behavior. The default is <see cref="F:System.Windows.Forms.LinkBehavior.SystemDefault"></see>.</returns>
        /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridLinkColumn.CellTemplate"></see> property is null.</exception>
        /// <filterpriority>1</filterpriority>
        [DefaultValue(0), Description("Grid_LinkColumnLinkBehavior"), Category("Behavior")]
        public System.Windows.Forms.LinkBehavior LinkBehavior
        {
            get
            {
                if (this.CellTemplate == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridColumn_CellTemplateRequired"));
                }
                return ((GridLinkCell) this.CellTemplate).LinkBehavior;
            }
            set
            {
                if (!this.LinkBehavior.Equals(value))
                {
                    ((GridLinkCell) this.CellTemplate).LinkBehavior = value;
                    if (base.Grid != null)
                    {
                        GridRowCollection rows = base.Grid.Rows;
                        int count = rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            GridLinkCell cell = rows.SharedRow(i).Cells[base.Index] as GridLinkCell;
                            if (cell != null)
                            {
                                cell.LinkBehaviorInternal = value;
                            }
                        }
                        base.Grid.InvalidateColumn(base.Index);
                    }
                }
            }
        }

        /// <summary>Gets or sets the color used to display an unselected link within cells in the column.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color"></see> that represents the color used to initially display a link. The default value is the user's Internet Explorer setting for the link color. </returns>
        /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridLinkColumn.CellTemplate"></see> property is null.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" /></PermissionSet>
        [Category("Appearance"), Description("Grid_LinkColumnLinkColor")]
        public Color LinkColor
        {
            get
            {
                if (this.CellTemplate == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridColumn_CellTemplateRequired"));
                }
                return ((GridLinkCell) this.CellTemplate).LinkColor;
            }
            set
            {
                if (!this.LinkColor.Equals(value))
                {
                    ((GridLinkCell) this.CellTemplate).LinkColorInternal = value;
                    if (base.Grid != null)
                    {
                        GridRowCollection rows = base.Grid.Rows;
                        int count = rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            GridLinkCell cell = rows.SharedRow(i).Cells[base.Index] as GridLinkCell;
                            if (cell != null)
                            {
                                cell.LinkColorInternal = value;
                            }
                        }
                        base.Grid.InvalidateColumn(base.Index);
                    }
                }
            }
        }

        /// <summary>Gets or sets the link text displayed in a column's cells if <see cref="P:MControl.GridView.GridLinkColumn.UseColumnTextForLinkValue"></see> is true.</summary>
        /// <returns>A <see cref="T:System.String"></see> containing the link text.</returns>
        /// <exception cref="T:System.InvalidOperationException">When setting this property, the value of the <see cref="P:MControl.GridView.GridLinkColumn.CellTemplate"></see> property is null.</exception>
        /// <filterpriority>1</filterpriority>
        [DefaultValue((string) null), Category("Appearance"), Description("Grid_LinkColumnText")]
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
                        if (this.UseColumnTextForLinkValue)
                        {
                            base.Grid.OnColumnCommonChange(base.Index);
                        }
                        else
                        {
                            GridRowCollection rows = base.Grid.Rows;
                            int count = rows.Count;
                            for (int i = 0; i < count; i++)
                            {
                                GridLinkCell cell = rows.SharedRow(i).Cells[base.Index] as GridLinkCell;
                                if ((cell != null) && cell.UseColumnTextForLinkValue)
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

        /// <summary>Gets or sets a value indicating whether the link changes color if it has been visited.</summary>
        /// <returns>true if the link changes color when it is selected; otherwise, false. The default is true.</returns>
        /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridLinkColumn.CellTemplate"></see> property is null.</exception>
        /// <filterpriority>1</filterpriority>
        [Description("Grid_LinkColumnTrackVisitedState"), DefaultValue(true), Category("Behavior")]
        public bool TrackVisitedState
        {
            get
            {
                if (this.CellTemplate == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridColumn_CellTemplateRequired"));
                }
                return ((GridLinkCell) this.CellTemplate).TrackVisitedState;
            }
            set
            {
                if (this.TrackVisitedState != value)
                {
                    ((GridLinkCell) this.CellTemplate).TrackVisitedStateInternal = value;
                    if (base.Grid != null)
                    {
                        GridRowCollection rows = base.Grid.Rows;
                        int count = rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            GridLinkCell cell = rows.SharedRow(i).Cells[base.Index] as GridLinkCell;
                            if (cell != null)
                            {
                                cell.TrackVisitedStateInternal = value;
                            }
                        }
                        base.Grid.InvalidateColumn(base.Index);
                    }
                }
            }
        }

        /// <summary>Gets or sets a value indicating whether the <see cref="P:MControl.GridView.GridLinkColumn.Text"></see> property value is displayed as the link text.</summary>
        /// <returns>true if the <see cref="P:MControl.GridView.GridLinkColumn.Text"></see> property value is displayed as the link text; false if the cell <see cref="P:MControl.GridView.GridCell.FormattedValue"></see> property value is displayed as the link text. The default is false.</returns>
        /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridLinkColumn.CellTemplate"></see> property is null.</exception>
        [Category("Appearance"), Description("Grid_LinkColumnUseColumnTextForLinkValue"), DefaultValue(false)]
        public bool UseColumnTextForLinkValue
        {
            get
            {
                if (this.CellTemplate == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridColumn_CellTemplateRequired"));
                }
                return ((GridLinkCell) this.CellTemplate).UseColumnTextForLinkValue;
            }
            set
            {
                if (this.UseColumnTextForLinkValue != value)
                {
                    ((GridLinkCell) this.CellTemplate).UseColumnTextForLinkValueInternal = value;
                    if (base.Grid != null)
                    {
                        GridRowCollection rows = base.Grid.Rows;
                        int count = rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            GridLinkCell cell = rows.SharedRow(i).Cells[base.Index] as GridLinkCell;
                            if (cell != null)
                            {
                                cell.UseColumnTextForLinkValueInternal = value;
                            }
                        }
                        base.Grid.OnColumnCommonChange(base.Index);
                    }
                }
            }
        }

        /// <summary>Gets or sets the color used to display a link that has been previously visited.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color"></see> that represents the color used to display a link that has been visited. The default value is the user's Internet Explorer setting for the visited link color. </returns>
        /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:MControl.GridView.GridLinkColumn.CellTemplate"></see> property is null.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" /></PermissionSet>
        [Category("Appearance"), Description("Grid_LinkColumnVisitedLinkColor")]
        public Color VisitedLinkColor
        {
            get
            {
                if (this.CellTemplate == null)
                {
                    throw new InvalidOperationException(MControl.GridView.RM.GetString("GridColumn_CellTemplateRequired"));
                }
                return ((GridLinkCell) this.CellTemplate).VisitedLinkColor;
            }
            set
            {
                if (!this.VisitedLinkColor.Equals(value))
                {
                    ((GridLinkCell) this.CellTemplate).VisitedLinkColorInternal = value;
                    if (base.Grid != null)
                    {
                        GridRowCollection rows = base.Grid.Rows;
                        int count = rows.Count;
                        for (int i = 0; i < count; i++)
                        {
                            GridLinkCell cell = rows.SharedRow(i).Cells[base.Index] as GridLinkCell;
                            if (cell != null)
                            {
                                cell.VisitedLinkColorInternal = value;
                            }
                        }
                        base.Grid.InvalidateColumn(base.Index);
                    }
                }
            }
        }
    }
}

