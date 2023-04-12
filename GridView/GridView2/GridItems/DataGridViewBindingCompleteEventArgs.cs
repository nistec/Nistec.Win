namespace MControl.GridView
{
    using System;
    using System.ComponentModel;

    /// <summary>Provides data for the <see cref="E:MControl.GridView.Grid.DataBindingComplete"></see> event.</summary>
    /// <filterpriority>2</filterpriority>
    public class GridBindingCompleteEventArgs : EventArgs
    {
        private System.ComponentModel.ListChangedType listChangedType;

        /// <summary>Initializes a new instance of the <see cref="T:MControl.GridView.GridBindingCompleteEventArgs"></see> class.</summary>
        /// <param name="listChangedType">One of the <see cref="T:System.ComponentModel.ListChangedType"></see> values.</param>
        public GridBindingCompleteEventArgs(System.ComponentModel.ListChangedType listChangedType)
        {
            this.listChangedType = listChangedType;
        }

        /// <summary>Gets a value specifying how the list changed.</summary>
        /// <returns>One of the <see cref="T:System.ComponentModel.ListChangedType"></see> values.</returns>
        /// <filterpriority>1</filterpriority>
        public System.ComponentModel.ListChangedType ListChangedType
        {
            get
            {
                return this.listChangedType;
            }
        }
    }
}

