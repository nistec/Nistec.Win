namespace MControl.GridView
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Globalization;
    using System.Text;
    using MControl.Util;

    /// <summary>Represents the formatting and style information applied to individual cells within a <see cref="T:MControl.GridView.Grid"></see> control.</summary>
    /// <filterpriority>2</filterpriority>
    [Editor("System.Windows.Forms.Design.GridCellStyleEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), TypeConverter(typeof(GridCellStyleConverter))]
    public class GridCellStyle : ICloneable
    {
        private Grid grid;
        private const string gridCELLSTYLE_nullText = "";
        private static readonly int PropAlignment = PropertyStore.CreateKey();
        private static readonly int PropBackColor = PropertyStore.CreateKey();
        private static readonly int PropDataSourceNullValue = PropertyStore.CreateKey();
        private PropertyStore propertyStore;
        private static readonly int PropFont = PropertyStore.CreateKey();
        private static readonly int PropForeColor = PropertyStore.CreateKey();
        private static readonly int PropFormat = PropertyStore.CreateKey();
        private static readonly int PropFormatProvider = PropertyStore.CreateKey();
        private static readonly int PropNullValue = PropertyStore.CreateKey();
        private static readonly int PropPadding = PropertyStore.CreateKey();
        private static readonly int PropSelectionBackColor = PropertyStore.CreateKey();
        private static readonly int PropSelectionForeColor = PropertyStore.CreateKey();
        private static readonly int PropTag = PropertyStore.CreateKey();
        private static readonly int PropWrapMode = PropertyStore.CreateKey();
        private GridCellStyleScopes scope;

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridCellStyle"></see> class using default property values.</summary>
        public GridCellStyle()
        {
            this.propertyStore = new PropertyStore();
            this.scope = GridCellStyleScopes.None;
        }

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridCellStyle"></see> class using the property values of the specified <see cref="T:MControl.GridView.GridCellStyle"></see>.</summary>
        /// <param name="gridCellStyle">The <see cref="T:MControl.GridView.GridCellStyle"></see> used as a template to provide initial property values. </param>
        /// <exception cref="T:System.ArgumentNullException">gridCellStyle is null.</exception>
        public GridCellStyle(GridCellStyle gridCellStyle)
        {
            if (gridCellStyle == null)
            {
                throw new ArgumentNullException("gridCellStyle");
            }
            this.propertyStore = new PropertyStore();
            this.scope = GridCellStyleScopes.None;
            this.BackColor = gridCellStyle.BackColor;
            this.ForeColor = gridCellStyle.ForeColor;
            this.SelectionBackColor = gridCellStyle.SelectionBackColor;
            this.SelectionForeColor = gridCellStyle.SelectionForeColor;
            this.Font = gridCellStyle.Font;
            this.NullValue = gridCellStyle.NullValue;
            this.DataSourceNullValue = gridCellStyle.DataSourceNullValue;
            this.Format = gridCellStyle.Format;
            if (!gridCellStyle.IsFormatProviderDefault)
            {
                this.FormatProvider = gridCellStyle.FormatProvider;
            }
            this.AlignmentInternal = gridCellStyle.Alignment;
            this.WrapModeInternal = gridCellStyle.WrapMode;
            this.Tag = gridCellStyle.Tag;
            this.PaddingInternal = gridCellStyle.Padding;
        }

        internal void AddScope(Grid grid, GridCellStyleScopes scope)
        {
            this.scope |= scope;
            this.grid = grid;
        }

        /// <summary>Applies the specified <see cref="T:MControl.GridView.GridCellStyle"></see> to the current <see cref="T:MControl.GridView.GridCellStyle"></see>.</summary>
        /// <param name="gridCellStyle">The <see cref="T:MControl.GridView.GridCellStyle"></see> to apply to the current <see cref="T:MControl.GridView.GridCellStyle"></see>.</param>
        /// <exception cref="T:System.ArgumentNullException">gridCellStyle is null.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public virtual void ApplyStyle(GridCellStyle gridCellStyle)
        {
            if (gridCellStyle == null)
            {
                throw new ArgumentNullException("gridCellStyle");
            }
            if (!gridCellStyle.BackColor.IsEmpty)
            {
                this.BackColor = gridCellStyle.BackColor;
            }
            if (!gridCellStyle.ForeColor.IsEmpty)
            {
                this.ForeColor = gridCellStyle.ForeColor;
            }
            if (!gridCellStyle.SelectionBackColor.IsEmpty)
            {
                this.SelectionBackColor = gridCellStyle.SelectionBackColor;
            }
            if (!gridCellStyle.SelectionForeColor.IsEmpty)
            {
                this.SelectionForeColor = gridCellStyle.SelectionForeColor;
            }
            if (gridCellStyle.Font != null)
            {
                this.Font = gridCellStyle.Font;
            }
            if (!gridCellStyle.IsNullValueDefault)
            {
                this.NullValue = gridCellStyle.NullValue;
            }
            if (!gridCellStyle.IsDataSourceNullValueDefault)
            {
                this.DataSourceNullValue = gridCellStyle.DataSourceNullValue;
            }
            if (gridCellStyle.Format.Length != 0)
            {
                this.Format = gridCellStyle.Format;
            }
            if (!gridCellStyle.IsFormatProviderDefault)
            {
                this.FormatProvider = gridCellStyle.FormatProvider;
            }
            if (gridCellStyle.Alignment != GridContentAlignment.NotSet)
            {
                this.AlignmentInternal = gridCellStyle.Alignment;
            }
            if (gridCellStyle.WrapMode != GridTriState.NotSet)
            {
                this.WrapModeInternal = gridCellStyle.WrapMode;
            }
            if (gridCellStyle.Tag != null)
            {
                this.Tag = gridCellStyle.Tag;
            }
            if (gridCellStyle.Padding != System.Windows.Forms.Padding.Empty)
            {
                this.PaddingInternal = gridCellStyle.Padding;
            }
        }

        /// <summary>Creates an exact copy of this <see cref="T:MControl.GridView.GridCellStyle"></see>.</summary>
        /// <returns>A <see cref="T:MControl.GridView.GridCellStyle"></see> that represents an exact copy of this cell style.</returns>
        public virtual GridCellStyle Clone()
        {
            return new GridCellStyle(this);
        }

        /// <summary>Returns a value indicating whether this instance is equivalent to the specified object.</summary>
        /// <returns>true if o is a <see cref="T:MControl.GridView.GridCellStyle"></see> and has the same property values as this instance; otherwise, false.</returns>
        /// <param name="o">An object to compare with this instance, or null. </param>
        /// <filterpriority>1</filterpriority>
        public override bool Equals(object o)
        {
            GridCellStyle dgvcs = o as GridCellStyle;
            return ((dgvcs != null) && (this.GetDifferencesFrom(dgvcs) == GridCellStyleDifferences.None));
        }

        internal GridCellStyleDifferences GetDifferencesFrom(GridCellStyle dgvcs)
        {
            bool flag = ((((dgvcs.Alignment != this.Alignment) || (dgvcs.DataSourceNullValue != this.DataSourceNullValue)) || ((dgvcs.Font != this.Font) || (dgvcs.Format != this.Format))) || (((dgvcs.FormatProvider != this.FormatProvider) || (dgvcs.NullValue != this.NullValue)) || ((dgvcs.Padding != this.Padding) || (dgvcs.Tag != this.Tag)))) || (dgvcs.WrapMode != this.WrapMode);
            bool flag2 = (((dgvcs.BackColor != this.BackColor) || (dgvcs.ForeColor != this.ForeColor)) || (dgvcs.SelectionBackColor != this.SelectionBackColor)) || (dgvcs.SelectionForeColor != this.SelectionForeColor);
            if (flag)
            {
                return GridCellStyleDifferences.AffectPreferredSize;
            }
            if (flag2)
            {
                return GridCellStyleDifferences.DoNotAffectPreferredSize;
            }
            return GridCellStyleDifferences.None;
        }

        /// <filterpriority>1</filterpriority>
        public override int GetHashCode()
        {
            return WindowsFormsUtils.GetCombinedHashCodes(new int[] { this.Alignment, this.WrapMode, this.Padding.GetHashCode(), this.Format.GetHashCode(), this.BackColor.GetHashCode(), this.ForeColor.GetHashCode(), this.SelectionBackColor.GetHashCode(), this.SelectionForeColor.GetHashCode(), (this.Font == null) ? 1 : this.Font.GetHashCode(), (this.NullValue == null) ? 1 : this.NullValue.GetHashCode(), (this.DataSourceNullValue == null) ? 1 : this.DataSourceNullValue.GetHashCode(), (this.Tag == null) ? 1 : this.Tag.GetHashCode() });
        }

        private void OnPropertyChanged(GridCellStylePropertyInternal property)
        {
            if ((this.grid != null) && (this.scope != GridCellStyleScopes.None))
            {
                this.grid.OnCellStyleContentChanged(this, property);
            }
        }

        internal void RemoveScope(GridCellStyleScopes scope)
        {
            this.scope &= ~scope;
            if (this.scope == GridCellStyleScopes.None)
            {
                this.grid = null;
            }
        }

        private bool ShouldSerializeBackColor()
        {
            bool flag;
            this.Properties.GetColor(PropBackColor, out flag);
            return flag;
        }

        private bool ShouldSerializeFont()
        {
            return (this.Properties.GetObject(PropFont) != null);
        }

        private bool ShouldSerializeForeColor()
        {
            bool flag;
            this.Properties.GetColor(PropForeColor, out flag);
            return flag;
        }

        private bool ShouldSerializeFormatProvider()
        {
            return (this.Properties.GetObject(PropFormatProvider) != null);
        }

        private bool ShouldSerializePadding()
        {
            return (this.Padding != System.Windows.Forms.Padding.Empty);
        }

        private bool ShouldSerializeSelectionBackColor()
        {
            bool flag;
            this.Properties.GetObject(PropSelectionBackColor, out flag);
            return flag;
        }

        private bool ShouldSerializeSelectionForeColor()
        {
            bool flag;
            this.Properties.GetColor(PropSelectionForeColor, out flag);
            return flag;
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }

        /// <summary>Returns a string indicating the current property settings of the <see cref="T:MControl.GridView.GridCellStyle"></see>.</summary>
        /// <returns>A string indicating the current property settings of the <see cref="T:MControl.GridView.GridCellStyle"></see>.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(0x80);
            builder.Append("GridCellStyle {");
            bool flag = true;
            if (this.BackColor != Color.Empty)
            {
                builder.Append(" BackColor=" + this.BackColor.ToString());
                flag = false;
            }
            if (this.ForeColor != Color.Empty)
            {
                if (!flag)
                {
                    builder.Append(",");
                }
                builder.Append(" ForeColor=" + this.ForeColor.ToString());
                flag = false;
            }
            if (this.SelectionBackColor != Color.Empty)
            {
                if (!flag)
                {
                    builder.Append(",");
                }
                builder.Append(" SelectionBackColor=" + this.SelectionBackColor.ToString());
                flag = false;
            }
            if (this.SelectionForeColor != Color.Empty)
            {
                if (!flag)
                {
                    builder.Append(",");
                }
                builder.Append(" SelectionForeColor=" + this.SelectionForeColor.ToString());
                flag = false;
            }
            if (this.Font != null)
            {
                if (!flag)
                {
                    builder.Append(",");
                }
                builder.Append(" Font=" + this.Font.ToString());
                flag = false;
            }
            if (!this.IsNullValueDefault && (this.NullValue != null))
            {
                if (!flag)
                {
                    builder.Append(",");
                }
                builder.Append(" NullValue=" + this.NullValue.ToString());
                flag = false;
            }
            if (!this.IsDataSourceNullValueDefault && (this.DataSourceNullValue != null))
            {
                if (!flag)
                {
                    builder.Append(",");
                }
                builder.Append(" DataSourceNullValue=" + this.DataSourceNullValue.ToString());
                flag = false;
            }
            if (!string.IsNullOrEmpty(this.Format))
            {
                if (!flag)
                {
                    builder.Append(",");
                }
                builder.Append(" Format=" + this.Format);
                flag = false;
            }
            if (this.WrapMode != GridTriState.NotSet)
            {
                if (!flag)
                {
                    builder.Append(",");
                }
                builder.Append(" WrapMode=" + this.WrapMode.ToString());
                flag = false;
            }
            if (this.Alignment != GridContentAlignment.NotSet)
            {
                if (!flag)
                {
                    builder.Append(",");
                }
                builder.Append(" Alignment=" + this.Alignment.ToString());
                flag = false;
            }
            if (this.Padding != System.Windows.Forms.Padding.Empty)
            {
                if (!flag)
                {
                    builder.Append(",");
                }
                builder.Append(" Padding=" + this.Padding.ToString());
                flag = false;
            }
            if (this.Tag != null)
            {
                if (!flag)
                {
                    builder.Append(",");
                }
                builder.Append(" Tag=" + this.Tag.ToString());
                flag = false;
            }
            builder.Append(" }");
            return builder.ToString();
        }

        /// <summary>Gets or sets a value indicating the position of the cell content within a <see cref="T:MControl.GridView.Grid"></see> cell.</summary>
        /// <returns>One of the <see cref="T:MControl.GridView.GridContentAlignment"></see> values. The default is <see cref="F:MControl.GridView.GridContentAlignment.NotSet"></see>.</returns>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The property value is not a valid <see cref="T:MControl.GridView.GridContentAlignment"></see> value. </exception>
        /// <filterpriority>1</filterpriority>
        [Description("GridCellStyleAlignment"), DefaultValue(0), Category("Layout")]
        public GridContentAlignment Alignment
        {
            get
            {
                bool flag;
                int integer = this.Properties.GetInteger(PropAlignment, out flag);
                if (flag)
                {
                    return (GridContentAlignment) integer;
                }
                return GridContentAlignment.NotSet;
            }
            set
            {
                switch (value)
                {
                    case GridContentAlignment.NotSet:
                    case GridContentAlignment.TopLeft:
                    case GridContentAlignment.TopCenter:
                    case GridContentAlignment.TopRight:
                    case GridContentAlignment.MiddleLeft:
                    case GridContentAlignment.MiddleCenter:
                    case GridContentAlignment.MiddleRight:
                    case GridContentAlignment.BottomLeft:
                    case GridContentAlignment.BottomCenter:
                    case GridContentAlignment.BottomRight:
                        this.AlignmentInternal = value;
                        return;
                }
                throw new InvalidEnumArgumentException("value", (int) value, typeof(GridContentAlignment));
            }
        }

        internal GridContentAlignment AlignmentInternal
        {
            set
            {
                if (this.Alignment != value)
                {
                    this.Properties.SetInteger(PropAlignment, (int) value);
                    this.OnPropertyChanged(GridCellStylePropertyInternal.Other);
                }
            }
        }

        /// <summary>Gets or sets the background color of a <see cref="T:MControl.GridView.Grid"></see> cell.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color"></see> that represents the background color of a cell. The default is <see cref="F:System.Drawing.Color.Empty"></see>.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        [Category("Appearance")]
        public Color BackColor
        {
            get
            {
                return this.Properties.GetColor(PropBackColor);
            }
            set
            {
                Color backColor = this.BackColor;
                if (!value.IsEmpty || this.Properties.ContainsObject(PropBackColor))
                {
                    this.Properties.SetColor(PropBackColor, value);
                }
                if (!backColor.Equals(this.BackColor))
                {
                    this.OnPropertyChanged(GridCellStylePropertyInternal.Color);
                }
            }
        }

        /// <summary>Gets or sets the value saved to the data source when the user enters a null value into a cell.</summary>
        /// <returns>The value saved to the data source when the user specifies a null cell value. The default is <see cref="F:System.DBNull.Value"></see>.</returns>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        public object DataSourceNullValue
        {
            get
            {
                if (this.Properties.ContainsObject(PropDataSourceNullValue))
                {
                    return this.Properties.GetObject(PropDataSourceNullValue);
                }
                return DBNull.Value;
            }
            set
            {
                object dataSourceNullValue = this.DataSourceNullValue;
                if ((dataSourceNullValue != value) && ((dataSourceNullValue == null) || !dataSourceNullValue.Equals(value)))
                {
                    if ((value == DBNull.Value) && this.Properties.ContainsObject(PropDataSourceNullValue))
                    {
                        this.Properties.RemoveObject(PropDataSourceNullValue);
                    }
                    else
                    {
                        this.Properties.SetObject(PropDataSourceNullValue, value);
                    }
                    this.OnPropertyChanged(GridCellStylePropertyInternal.Other);
                }
            }
        }

        /// <summary>Gets or sets the font applied to the textual content of a <see cref="T:MControl.GridView.Grid"></see> cell.</summary>
        /// <returns>The <see cref="T:System.Drawing.Font"></see> applied to the cell text. The default is null.</returns>
        /// <filterpriority>1</filterpriority>
        [Category("Appearance")]
        public System.Drawing.Font Font
        {
            get
            {
                return (System.Drawing.Font) this.Properties.GetObject(PropFont);
            }
            set
            {
                System.Drawing.Font font = this.Font;
                if ((value != null) || this.Properties.ContainsObject(PropFont))
                {
                    this.Properties.SetObject(PropFont, value);
                }
                if ((((font == null) && (value != null)) || ((font != null) && (value == null))) || (((font != null) && (value != null)) && !font.Equals(this.Font)))
                {
                    this.OnPropertyChanged(GridCellStylePropertyInternal.Font);
                }
            }
        }

        /// <summary>Gets or sets the foreground color of a <see cref="T:MControl.GridView.Grid"></see> cell.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color"></see> that represents the foreground color of a cell. The default is <see cref="F:System.Drawing.Color.Empty"></see>.</returns>
        /// <filterpriority>1</filterpriority>
        [Category("Appearance")]
        public Color ForeColor
        {
            get
            {
                return this.Properties.GetColor(PropForeColor);
            }
            set
            {
                Color foreColor = this.ForeColor;
                if (!value.IsEmpty || this.Properties.ContainsObject(PropForeColor))
                {
                    this.Properties.SetColor(PropForeColor, value);
                }
                if (!foreColor.Equals(this.ForeColor))
                {
                    this.OnPropertyChanged(GridCellStylePropertyInternal.ForeColor);
                }
            }
        }

        /// <summary>Gets or sets the format string applied to the textual content of a <see cref="T:MControl.GridView.Grid"></see> cell.</summary>
        /// <returns>A string that indicates the format of the cell value. The default is <see cref="F:System.String.Empty"></see>.</returns>
        /// <filterpriority>1</filterpriority>
        [DefaultValue(""), Category("Behavior"), EditorBrowsable(EditorBrowsableState.Advanced), Editor("System.Windows.Forms.Design.FormatStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string Format
        {
            get
            {
                object obj2 = this.Properties.GetObject(PropFormat);
                if (obj2 == null)
                {
                    return string.Empty;
                }
                return (string) obj2;
            }
            set
            {
                string format = this.Format;
                if (((value != null) && (value.Length > 0)) || this.Properties.ContainsObject(PropFormat))
                {
                    this.Properties.SetObject(PropFormat, value);
                }
                if (!format.Equals(this.Format))
                {
                    this.OnPropertyChanged(GridCellStylePropertyInternal.Other);
                }
            }
        }

        /// <summary>Gets or sets the object used to provide culture-specific formatting of <see cref="T:MControl.GridView.Grid"></see> cell values.</summary>
        /// <returns>An <see cref="T:System.IFormatProvider"></see> used for cell formatting. The default is <see cref="P:System.Globalization.CultureInfo.CurrentUICulture"></see>.</returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        public IFormatProvider FormatProvider
        {
            get
            {
                object obj2 = this.Properties.GetObject(PropFormatProvider);
                if (obj2 == null)
                {
                    return CultureInfo.CurrentCulture;
                }
                return (IFormatProvider) obj2;
            }
            set
            {
                object obj2 = this.Properties.GetObject(PropFormatProvider);
                this.Properties.SetObject(PropFormatProvider, value);
                if (value != obj2)
                {
                    this.OnPropertyChanged(GridCellStylePropertyInternal.Other);
                }
            }
        }

        /// <summary>Gets a value indicating whether the <see cref="P:MControl.GridView.GridCellStyle.DataSourceNullValue"></see> property has been set.</summary>
        /// <returns>true if the value of the <see cref="P:MControl.GridView.GridCellStyle.DataSourceNullValue"></see> property is the default value; otherwise, false.</returns>
        [EditorBrowsable(EditorBrowsableState.Advanced), Browsable(false)]
        public bool IsDataSourceNullValueDefault
        {
            get
            {
                return (!this.Properties.ContainsObject(PropDataSourceNullValue) || (this.Properties.GetObject(PropDataSourceNullValue) == DBNull.Value));
            }
        }

        /// <summary>Gets a value that indicates whether the <see cref="P:MControl.GridView.GridCellStyle.FormatProvider"></see> property has been set.</summary>
        /// <returns>true if the <see cref="P:MControl.GridView.GridCellStyle.FormatProvider"></see> property is the default value; otherwise, false.</returns>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        public bool IsFormatProviderDefault
        {
            get
            {
                return (this.Properties.GetObject(PropFormatProvider) == null);
            }
        }

        /// <summary>Gets a value indicating whether the <see cref="P:MControl.GridView.GridCellStyle.NullValue"></see> property has been set.</summary>
        /// <returns>true if the value of the <see cref="P:MControl.GridView.GridCellStyle.NullValue"></see> property is the default value; otherwise, false.</returns>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        public bool IsNullValueDefault
        {
            get
            {
                if (!this.Properties.ContainsObject(PropNullValue))
                {
                    return true;
                }
                object obj2 = this.Properties.GetObject(PropNullValue);
                return ((obj2 is string) && obj2.Equals(""));
            }
        }

        /// <summary>Gets or sets the <see cref="T:MControl.GridView.Grid"></see> cell display value corresponding to a cell value of <see cref="F:System.DBNull.Value"></see> or null.</summary>
        /// <returns>The object used to indicate a null value in a cell. The default is <see cref="F:System.String.Empty"></see>.</returns>
        /// <filterpriority>1</filterpriority>
        [TypeConverter(typeof(StringConverter)), DefaultValue(""), Category("Data")]
        public object NullValue
        {
            get
            {
                if (this.Properties.ContainsObject(PropNullValue))
                {
                    return this.Properties.GetObject(PropNullValue);
                }
                return "";
            }
            set
            {
                object nullValue = this.NullValue;
                if ((nullValue != value) && ((nullValue == null) || !nullValue.Equals(value)))
                {
                    if (((value is string) && value.Equals("")) && this.Properties.ContainsObject(PropNullValue))
                    {
                        this.Properties.RemoveObject(PropNullValue);
                    }
                    else
                    {
                        this.Properties.SetObject(PropNullValue, value);
                    }
                    this.OnPropertyChanged(GridCellStylePropertyInternal.Other);
                }
            }
        }

        /// <summary>Gets or sets the space between the edge of a <see cref="T:MControl.GridView.GridCell"></see> and its content.</summary>
        /// <returns>A <see cref="T:System.Windows.Forms.Padding"></see> that represents the space between the edge of a <see cref="T:MControl.GridView.GridCell"></see> and its content.</returns>
        /// <filterpriority>1</filterpriority>
        [Category("Layout")]
        public System.Windows.Forms.Padding Padding
        {
            get
            {
                return this.Properties.GetPadding(PropPadding);
            }
            set
            {
                if (((value.Left < 0) || (value.Right < 0)) || ((value.Top < 0) || (value.Bottom < 0)))
                {
                    if (value.All != -1)
                    {
                        value.All = 0;
                    }
                    else
                    {
                        value.Left = Math.Max(0, value.Left);
                        value.Right = Math.Max(0, value.Right);
                        value.Top = Math.Max(0, value.Top);
                        value.Bottom = Math.Max(0, value.Bottom);
                    }
                }
                this.PaddingInternal = value;
            }
        }

        internal System.Windows.Forms.Padding PaddingInternal
        {
            set
            {
                if (value != this.Padding)
                {
                    this.Properties.SetPadding(PropPadding, value);
                    this.OnPropertyChanged(GridCellStylePropertyInternal.Other);
                }
            }
        }

        internal PropertyStore Properties
        {
            get
            {
                return this.propertyStore;
            }
        }

        internal GridCellStyleScopes Scope
        {
            get
            {
                return this.scope;
            }
            set
            {
                this.scope = value;
            }
        }

        /// <summary>Gets or sets the background color used by a <see cref="T:MControl.GridView.Grid"></see> cell when it is selected.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color"></see> that represents the background color of a selected cell. The default is <see cref="F:System.Drawing.Color.Empty"></see>.</returns>
        /// <filterpriority>1</filterpriority>
        [Category("Appearance")]
        public Color SelectionBackColor
        {
            get
            {
                return this.Properties.GetColor(PropSelectionBackColor);
            }
            set
            {
                Color selectionBackColor = this.SelectionBackColor;
                if (!value.IsEmpty || this.Properties.ContainsObject(PropSelectionBackColor))
                {
                    this.Properties.SetColor(PropSelectionBackColor, value);
                }
                if (!selectionBackColor.Equals(this.SelectionBackColor))
                {
                    this.OnPropertyChanged(GridCellStylePropertyInternal.Color);
                }
            }
        }

        /// <summary>Gets or sets the foreground color used by a <see cref="T:MControl.GridView.Grid"></see> cell when it is selected.</summary>
        /// <returns>A <see cref="T:System.Drawing.Color"></see> that represents the foreground color of a selected cell. The default is <see cref="F:System.Drawing.Color.Empty"></see>.</returns>
        /// <filterpriority>1</filterpriority>
        [Category("Appearance")]
        public Color SelectionForeColor
        {
            get
            {
                return this.Properties.GetColor(PropSelectionForeColor);
            }
            set
            {
                Color selectionForeColor = this.SelectionForeColor;
                if (!value.IsEmpty || this.Properties.ContainsObject(PropSelectionForeColor))
                {
                    this.Properties.SetColor(PropSelectionForeColor, value);
                }
                if (!selectionForeColor.Equals(this.SelectionForeColor))
                {
                    this.OnPropertyChanged(GridCellStylePropertyInternal.Color);
                }
            }
        }

        /// <summary>Gets or sets an object that contains additional data related to the <see cref="T:MControl.GridView.GridCellStyle"></see>.</summary>
        /// <returns>An object that contains additional data. The default is null.</returns>
        /// <filterpriority>1</filterpriority>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public object Tag
        {
            get
            {
                return this.Properties.GetObject(PropTag);
            }
            set
            {
                if ((value != null) || this.Properties.ContainsObject(PropTag))
                {
                    this.Properties.SetObject(PropTag, value);
                }
            }
        }

        /// <summary>Gets or sets a value indicating whether textual content in a <see cref="T:MControl.GridView.Grid"></see> cell is wrapped to subsequent lines or truncated when it is too long to fit on a single line.</summary>
        /// <returns>One of the <see cref="T:MControl.GridView.GridTriState"></see> values. The default is <see cref="F:MControl.GridView.GridTriState.NotSet"></see>.</returns>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The property value is not a valid <see cref="T:MControl.GridView.GridTriState"></see> value. </exception>
        /// <filterpriority>1</filterpriority>
        [DefaultValue(0), Category("Layout")]
        public GridTriState WrapMode
        {
            get
            {
                bool flag;
                int integer = this.Properties.GetInteger(PropWrapMode, out flag);
                if (flag)
                {
                    return (GridTriState) integer;
                }
                return GridTriState.NotSet;
            }
            set
            {
                if (!System.Windows.Forms.ClientUtils.IsEnumValid(value, (int) value, 0, 2))
                {
                    throw new InvalidEnumArgumentException("value", (int) value, typeof(GridTriState));
                }
                this.WrapModeInternal = value;
            }
        }

        internal GridTriState WrapModeInternal
        {
            set
            {
                if (this.WrapMode != value)
                {
                    this.Properties.SetInteger(PropWrapMode, (int) value);
                    this.OnPropertyChanged(GridCellStylePropertyInternal.Other);
                }
            }
        }

        internal enum GridCellStylePropertyInternal
        {
            Color,
            Other,
            Font,
            ForeColor
        }
    }
}

