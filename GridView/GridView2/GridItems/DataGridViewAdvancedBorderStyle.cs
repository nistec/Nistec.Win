namespace MControl.GridView
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    /// <summary>Contains border styles for the cells in a <see cref="T:MControl.GridView.Grid"></see> control.</summary>
    /// <filterpriority>2</filterpriority>
    public sealed class GridAdvancedBorderStyle : ICloneable
    {
        private bool all;
        private GridAdvancedCellBorderStyle banned1;
        private GridAdvancedCellBorderStyle banned2;
        private GridAdvancedCellBorderStyle banned3;
        private GridAdvancedCellBorderStyle bottom;
        private GridAdvancedCellBorderStyle left;
        private Grid owner;
        private GridAdvancedCellBorderStyle right;
        private GridAdvancedCellBorderStyle top;

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridAdvancedBorderStyle"></see> class. </summary>
        public GridAdvancedBorderStyle() : this(null, GridAdvancedCellBorderStyle.NotSet, GridAdvancedCellBorderStyle.NotSet, GridAdvancedCellBorderStyle.NotSet)
        {
        }

        internal GridAdvancedBorderStyle(Grid owner) : this(owner, GridAdvancedCellBorderStyle.NotSet, GridAdvancedCellBorderStyle.NotSet, GridAdvancedCellBorderStyle.NotSet)
        {
        }

        internal GridAdvancedBorderStyle(Grid owner, GridAdvancedCellBorderStyle banned1, GridAdvancedCellBorderStyle banned2, GridAdvancedCellBorderStyle banned3)
        {
            this.all = true;
            this.top = GridAdvancedCellBorderStyle.None;
            this.left = GridAdvancedCellBorderStyle.None;
            this.right = GridAdvancedCellBorderStyle.None;
            this.bottom = GridAdvancedCellBorderStyle.None;
            this.owner = owner;
            this.banned1 = banned1;
            this.banned2 = banned2;
            this.banned3 = banned3;
        }

        /// <summary>Determines whether the specified object is equal to the current <see cref="T:MControl.GridView.GridAdvancedBorderStyle"></see>.</summary>
        /// <returns>true if other is a <see cref="T:MControl.GridView.GridAdvancedBorderStyle"></see> and the values for the <see cref="P:MControl.GridView.GridAdvancedBorderStyle.Top"></see>, <see cref="P:MControl.GridView.GridAdvancedBorderStyle.Bottom"></see>, <see cref="P:MControl.GridView.GridAdvancedBorderStyle.Left"></see>, and <see cref="P:MControl.GridView.GridAdvancedBorderStyle.Right"></see> properties are equal to their counterpart in the current <see cref="T:MControl.GridView.GridAdvancedBorderStyle"></see>; otherwise, false.</returns>
        /// <param name="other">An <see cref="T:System.Object"></see> to be compared.</param>
        /// <filterpriority>1</filterpriority>
        public override bool Equals(object other)
        {
            GridAdvancedBorderStyle style = other as GridAdvancedBorderStyle;
            if (style == null)
            {
                return false;
            }
            return ((((style.all == this.all) && (style.top == this.top)) && ((style.left == this.left) && (style.bottom == this.bottom))) && (style.right == this.right));
        }

        /// <filterpriority>1</filterpriority>
        public override int GetHashCode()
        {
            return WindowsFormsUtils.GetCombinedHashCodes(new int[] { this.top, this.left, this.bottom, this.right });
        }

        object ICloneable.Clone()
        {
            GridAdvancedBorderStyle style = new GridAdvancedBorderStyle(this.owner, this.banned1, this.banned2, this.banned3);
            style.all = this.all;
            style.top = this.top;
            style.right = this.right;
            style.bottom = this.bottom;
            style.left = this.left;
            return style;
        }

        /// <summary>Returns a string that represents the <see cref="T:MControl.GridView.GridAdvancedBorderStyle"></see>.</summary>
        /// <returns>A string that represents the <see cref="T:MControl.GridView.GridAdvancedBorderStyle"></see>.</returns>
        /// <filterpriority>1</filterpriority>
        public override string ToString()
        {
            return ("GridAdvancedBorderStyle { All=" + this.All.ToString() + ", Left=" + this.Left.ToString() + ", Right=" + this.Right.ToString() + ", Top=" + this.Top.ToString() + ", Bottom=" + this.Bottom.ToString() + " }");
        }

        /// <summary>Gets or sets the border style for all of the borders of a cell.</summary>
        /// <returns>One of the <see cref="T:MControl.GridView.GridAdvancedCellBorderStyle"></see> values.</returns>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:MControl.GridView.GridAdvancedCellBorderStyle"></see> values.</exception>
        /// <exception cref="T:System.ArgumentException">The specified value when setting this property is <see cref="F:MControl.GridView.GridAdvancedCellBorderStyle.NotSet"></see>.-or-The specified value when setting this property is <see cref="F:MControl.GridView.GridAdvancedCellBorderStyle.OutsetDouble"></see>, <see cref="F:MControl.GridView.GridAdvancedCellBorderStyle.OutsetPartial"></see>, or <see cref="F:MControl.GridView.GridAdvancedCellBorderStyle.InsetDouble"></see> and this <see cref="T:MControl.GridView.GridAdvancedBorderStyle"></see> instance was retrieved through the <see cref="P:MControl.GridView.Grid.AdvancedCellBorderStyle"></see> property.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        public GridAdvancedCellBorderStyle All
        {
            get
            {
                if (!this.all)
                {
                    return GridAdvancedCellBorderStyle.NotSet;
                }
                return this.top;
            }
            set
            {
                if (!System.Windows.Forms.ClientUtils.IsEnumValid(value, (int) value, 0, 7))
                {
                    throw new InvalidEnumArgumentException("value", (int) value, typeof(GridAdvancedCellBorderStyle));
                }
                if (((value == GridAdvancedCellBorderStyle.NotSet) || (value == this.banned1)) || ((value == this.banned2) || (value == this.banned3)))
                {
                    throw new ArgumentException(MControl.GridView.RM.GetString("Grid_AdvancedCellBorderStyleInvalid", new object[] { "All" }));
                }
                if (!this.all || (this.top != value))
                {
                    this.all = true;
                    this.top = this.left = this.right = this.bottom = value;
                    if (this.owner != null)
                    {
                        this.owner.OnAdvancedBorderStyleChanged(this);
                    }
                }
            }
        }

        /// <summary>Gets or sets the style for the bottom border of a cell.</summary>
        /// <returns>One of the <see cref="T:MControl.GridView.GridAdvancedCellBorderStyle"></see> values.</returns>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:MControl.GridView.GridAdvancedCellBorderStyle"></see> values.</exception>
        /// <exception cref="T:System.ArgumentException">The specified value when setting this property is <see cref="F:MControl.GridView.GridAdvancedCellBorderStyle.NotSet"></see>.</exception>
        /// <filterpriority>1</filterpriority>
        public GridAdvancedCellBorderStyle Bottom
        {
            get
            {
                if (this.all)
                {
                    return this.top;
                }
                return this.bottom;
            }
            set
            {
                if (!System.Windows.Forms.ClientUtils.IsEnumValid(value, (int) value, 0, 7))
                {
                    throw new InvalidEnumArgumentException("value", (int) value, typeof(GridAdvancedCellBorderStyle));
                }
                if (value == GridAdvancedCellBorderStyle.NotSet)
                {
                    throw new ArgumentException(MControl.GridView.RM.GetString("Grid_AdvancedCellBorderStyleInvalid", new object[] { "Bottom" }));
                }
                this.BottomInternal = value;
            }
        }

        internal GridAdvancedCellBorderStyle BottomInternal
        {
            set
            {
                if ((this.all && (this.top != value)) || (!this.all && (this.bottom != value)))
                {
                    if (this.all && (this.right == GridAdvancedCellBorderStyle.OutsetDouble))
                    {
                        this.right = GridAdvancedCellBorderStyle.Outset;
                    }
                    this.all = false;
                    this.bottom = value;
                    if (this.owner != null)
                    {
                        this.owner.OnAdvancedBorderStyleChanged(this);
                    }
                }
            }
        }

        /// <summary>Gets the style for the left border of a cell.</summary>
        /// <returns>One of the <see cref="T:MControl.GridView.GridAdvancedCellBorderStyle"></see> values.</returns>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:MControl.GridView.GridAdvancedCellBorderStyle"></see>.</exception>
        /// <exception cref="T:System.ArgumentException">The specified value when setting this property is <see cref="F:MControl.GridView.GridAdvancedCellBorderStyle.NotSet"></see>.-or-The specified value when setting this property is <see cref="F:MControl.GridView.GridAdvancedCellBorderStyle.InsetDouble"></see> or <see cref="F:MControl.GridView.GridAdvancedCellBorderStyle.OutsetDouble"></see> and this <see cref="T:MControl.GridView.GridAdvancedBorderStyle"></see> instance has an associated <see cref="T:MControl.GridView.Grid"></see> control with a <see cref="P:System.Windows.Forms.Control.RightToLeft"></see> property value of true.</exception>
        /// <filterpriority>1</filterpriority>
        public GridAdvancedCellBorderStyle Left
        {
            get
            {
                if (this.all)
                {
                    return this.top;
                }
                return this.left;
            }
            set
            {
                if (!System.Windows.Forms.ClientUtils.IsEnumValid(value, (int) value, 0, 7))
                {
                    throw new InvalidEnumArgumentException("value", (int) value, typeof(GridAdvancedCellBorderStyle));
                }
                if (value == GridAdvancedCellBorderStyle.NotSet)
                {
                    throw new ArgumentException(MControl.GridView.RM.GetString("Grid_AdvancedCellBorderStyleInvalid", new object[] { "Left" }));
                }
                this.LeftInternal = value;
            }
        }

        internal GridAdvancedCellBorderStyle LeftInternal
        {
            set
            {
                if ((this.all && (this.top != value)) || (!this.all && (this.left != value)))
                {
                    if (((this.owner != null) && this.owner.RightToLeftInternal) && ((value == GridAdvancedCellBorderStyle.InsetDouble) || (value == GridAdvancedCellBorderStyle.OutsetDouble)))
                    {
                        throw new ArgumentException(MControl.GridView.RM.GetString("Grid_AdvancedCellBorderStyleInvalid", new object[] { "Left" }));
                    }
                    if (this.all)
                    {
                        if (this.right == GridAdvancedCellBorderStyle.OutsetDouble)
                        {
                            this.right = GridAdvancedCellBorderStyle.Outset;
                        }
                        if (this.bottom == GridAdvancedCellBorderStyle.OutsetDouble)
                        {
                            this.bottom = GridAdvancedCellBorderStyle.Outset;
                        }
                    }
                    this.all = false;
                    this.left = value;
                    if (this.owner != null)
                    {
                        this.owner.OnAdvancedBorderStyleChanged(this);
                    }
                }
            }
        }

        /// <summary>Gets the style for the right border of a cell.</summary>
        /// <returns>One of the <see cref="T:MControl.GridView.GridAdvancedCellBorderStyle"></see> values.</returns>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:MControl.GridView.GridAdvancedCellBorderStyle"></see>.</exception>
        /// <exception cref="T:System.ArgumentException">The specified value when setting this property is <see cref="F:MControl.GridView.GridAdvancedCellBorderStyle.NotSet"></see>.-or-The specified value when setting this property is <see cref="F:MControl.GridView.GridAdvancedCellBorderStyle.InsetDouble"></see> or <see cref="F:MControl.GridView.GridAdvancedCellBorderStyle.OutsetDouble"></see> and this <see cref="T:MControl.GridView.GridAdvancedBorderStyle"></see> instance has an associated <see cref="T:MControl.GridView.Grid"></see> control with a <see cref="P:System.Windows.Forms.Control.RightToLeft"></see> property value of false.</exception>
        /// <filterpriority>1</filterpriority>
        public GridAdvancedCellBorderStyle Right
        {
            get
            {
                if (this.all)
                {
                    return this.top;
                }
                return this.right;
            }
            set
            {
                if (!System.Windows.Forms.ClientUtils.IsEnumValid(value, (int) value, 0, 7))
                {
                    throw new InvalidEnumArgumentException("value", (int) value, typeof(GridAdvancedCellBorderStyle));
                }
                if (value == GridAdvancedCellBorderStyle.NotSet)
                {
                    throw new ArgumentException(MControl.GridView.RM.GetString("Grid_AdvancedCellBorderStyleInvalid", new object[] { "Right" }));
                }
                this.RightInternal = value;
            }
        }

        internal GridAdvancedCellBorderStyle RightInternal
        {
            set
            {
                if ((this.all && (this.top != value)) || (!this.all && (this.right != value)))
                {
                    if (((this.owner != null) && !this.owner.RightToLeftInternal) && ((value == GridAdvancedCellBorderStyle.InsetDouble) || (value == GridAdvancedCellBorderStyle.OutsetDouble)))
                    {
                        throw new ArgumentException(MControl.GridView.RM.GetString("Grid_AdvancedCellBorderStyleInvalid", new object[] { "Right" }));
                    }
                    if (this.all && (this.bottom == GridAdvancedCellBorderStyle.OutsetDouble))
                    {
                        this.bottom = GridAdvancedCellBorderStyle.Outset;
                    }
                    this.all = false;
                    this.right = value;
                    if (this.owner != null)
                    {
                        this.owner.OnAdvancedBorderStyleChanged(this);
                    }
                }
            }
        }

        /// <summary>Gets the style for the top border of a cell.</summary>
        /// <returns>One of the <see cref="T:MControl.GridView.GridAdvancedCellBorderStyle"></see> values.</returns>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:MControl.GridView.GridAdvancedCellBorderStyle"></see>.</exception>
        /// <exception cref="T:System.ArgumentException">The specified value when setting this property is <see cref="F:MControl.GridView.GridAdvancedCellBorderStyle.NotSet"></see>.</exception>
        /// <filterpriority>1</filterpriority>
        public GridAdvancedCellBorderStyle Top
        {
            get
            {
                return this.top;
            }
            set
            {
                if (!System.Windows.Forms.ClientUtils.IsEnumValid(value, (int) value, 0, 7))
                {
                    throw new InvalidEnumArgumentException("value", (int) value, typeof(GridAdvancedCellBorderStyle));
                }
                if (value == GridAdvancedCellBorderStyle.NotSet)
                {
                    throw new ArgumentException(MControl.GridView.RM.GetString("Grid_AdvancedCellBorderStyleInvalid", new object[] { "Top" }));
                }
                this.TopInternal = value;
            }
        }

        internal GridAdvancedCellBorderStyle TopInternal
        {
            set
            {
                if ((this.all && (this.top != value)) || (!this.all && (this.top != value)))
                {
                    if (this.all)
                    {
                        if (this.right == GridAdvancedCellBorderStyle.OutsetDouble)
                        {
                            this.right = GridAdvancedCellBorderStyle.Outset;
                        }
                        if (this.bottom == GridAdvancedCellBorderStyle.OutsetDouble)
                        {
                            this.bottom = GridAdvancedCellBorderStyle.Outset;
                        }
                    }
                    this.all = false;
                    this.top = value;
                    if (this.owner != null)
                    {
                        this.owner.OnAdvancedBorderStyleChanged(this);
                    }
                }
            }
        }
    }
}

