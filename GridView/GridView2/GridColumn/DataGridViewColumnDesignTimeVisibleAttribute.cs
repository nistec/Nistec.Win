namespace MControl.GridView
{
    using System;

    /// <summary>Specifies whether a column type is visible in the <see cref="T:MControl.GridView.Grid"></see> designer. This class cannot be inherited. </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class GridColumnDesignTimeVisibleAttribute : Attribute
    {
        /// <summary>The default <see cref="T:MControl.GridView.GridColumnDesignTimeVisibleAttribute"></see> value, which is <see cref="F:MControl.GridView.GridColumnDesignTimeVisibleAttribute.Yes"></see>, indicating that the column is visible in the <see cref="T:MControl.GridView.Grid"></see> designer. </summary>
        public static readonly GridColumnDesignTimeVisibleAttribute Default = Yes;
        /// <summary>A <see cref="T:MControl.GridView.GridColumnDesignTimeVisibleAttribute"></see> value indicating that the column is not visible in the <see cref="T:MControl.GridView.Grid"></see> designer. </summary>
        public static readonly GridColumnDesignTimeVisibleAttribute No = new GridColumnDesignTimeVisibleAttribute(false);
        private bool visible;
        /// <summary>A <see cref="T:MControl.GridView.GridColumnDesignTimeVisibleAttribute"></see> value indicating that the column is visible in the <see cref="T:MControl.GridView.Grid"></see> designer. </summary>
        public static readonly GridColumnDesignTimeVisibleAttribute Yes = new GridColumnDesignTimeVisibleAttribute(true);

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridColumnDesignTimeVisibleAttribute"></see> class using the default <see cref="P:MControl.GridView.GridColumnDesignTimeVisibleAttribute.Visible"></see> property value of true. </summary>
        public GridColumnDesignTimeVisibleAttribute()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridColumnDesignTimeVisibleAttribute"></see> class using the specified value to initialize the <see cref="P:MControl.GridView.GridColumnDesignTimeVisibleAttribute.Visible"></see> property. </summary>
        /// <param name="visible">The value of the <see cref="P:MControl.GridView.GridColumnDesignTimeVisibleAttribute.Visible"></see> property.</param>
        public GridColumnDesignTimeVisibleAttribute(bool visible)
        {
            this.visible = visible;
        }

        /// <summary>Gets a value indicating whether this object is equivalent to the specified object.</summary>
        /// <returns>true to indicate that the specified object is a <see cref="T:MControl.GridView.GridColumnDesignTimeVisibleAttribute"></see> instance with the same <see cref="P:MControl.GridView.GridColumnDesignTimeVisibleAttribute.Visible"></see> property value as this instance; otherwise, false.</returns>
        /// <param name="obj">The <see cref="T:System.Object"></see> to compare with the current <see cref="T:System.Object"></see>.</param>
        public override bool Equals(object obj)
        {
            if (obj == this)
            {
                return true;
            }
            GridColumnDesignTimeVisibleAttribute attribute = obj as GridColumnDesignTimeVisibleAttribute;
            return ((attribute != null) && (attribute.Visible == this.visible));
        }

        public override int GetHashCode()
        {
            return (typeof(GridColumnDesignTimeVisibleAttribute).GetHashCode() ^ (this.visible ? -1 : 0));
        }

        /// <summary>Gets a value indicating whether this attribute instance is equal to the <see cref="F:MControl.GridView.GridColumnDesignTimeVisibleAttribute.Default"></see> attribute value.</summary>
        /// <returns>true to indicate that this instance is equal to the <see cref="F:MControl.GridView.GridColumnDesignTimeVisibleAttribute.Default"></see> instance; otherwise, false.</returns>
        public override bool IsDefaultAttribute()
        {
            return (this.Visible == Default.Visible);
        }

        /// <summary>Gets a value indicating whether the column type is visible in the <see cref="T:MControl.GridView.Grid"></see> designer.</summary>
        /// <returns>true to indicate that the column type is visible in the <see cref="T:MControl.GridView.Grid"></see> designer; otherwise, false.</returns>
        public bool Visible
        {
            get
            {
                return this.visible;
            }
        }
    }
}

