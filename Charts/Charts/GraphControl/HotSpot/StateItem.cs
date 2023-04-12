namespace Nistec.Charts
{
    using System;
    using System.Security.Permissions;
    using System.Web;

    /// <summary>Represents an item that is saved in the <see cref="T:ChartStateBag"></see> class when view state information is persisted between Web requests. This class cannot be inherited.</summary>
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level=AspNetHostingPermissionLevel.Minimal)]
    [Serializable]
    public sealed class ChartStateItem
    {
        private bool isDirty;
        private object value;

        internal ChartStateItem(object initialValue)
        {
            this.value = initialValue;
            this.isDirty = false;
        }

        /// <summary>Gets or sets a value indicating whether the <see cref="T:ChartStateItem"></see> object has been modified.</summary>
        /// <returns>true if the stored <see cref="T:ChartStateItem"></see> object has been modified; otherwise, false.</returns>
        public bool IsDirty
        {
            get
            {
                return this.isDirty;
            }
            set
            {
                this.isDirty = value;
            }
        }

        /// <summary>Gets or sets the value of the <see cref="T:ChartStateItem"></see> object that is stored in the <see cref="T:ChartStateBag"></see> object.</summary>
        /// <returns>The value of the <see cref="T:ChartStateItem"></see> stored in the <see cref="T:ChartStateBag"></see>.</returns>
        public object Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }
    }
}

