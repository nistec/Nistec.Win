using System;
using System.Collections;

namespace MControl.Collections
{

	#region CollectionChangeEventArgs

	/*public class CollectionChange : EventArgs
	{
		private int index = -1;

		public CollectionChange(int index)
		{
			this.index = index;
		}

		public int Index
		{
			get {return this.index;}
		}
	}*/

    // Declare the event signatures
    public delegate void CollectionClear();
    public delegate void CollectionChange(int index, object value);

	#endregion

    public class CollectionWithEvents : CollectionBase
    {
        // Collection change events
        public event CollectionClear Clearing;
        public event CollectionClear Cleared;
        public event CollectionChange Inserting;
        public event CollectionChange Inserted;
        public event CollectionChange Removing;
        public event CollectionChange Removed;
	
        // Overrides for generating events
        protected override void OnClear()
        {
            // Any attached event handlers?
            if (Clearing != null)
                Clearing();
        }

        /// <summary>Performs additional custom processes after clearing the contents of the collection.</summary>
        protected override void OnClearComplete()
        {
            // Any attached event handlers?
            if (Cleared != null)
                Cleared();
        }

        /// <summary>Occurs before the method is called.</summary>
        /// <param name="value">The Item that is inserted into the Collection. </param>
        /// <param name="index">The index in the collection that the Item is inserted at. </param>
        protected override void OnInsert(int index, object value)
        {
            // Any attached event handlers?
            if (Inserting != null)
                Inserting(index, value);
        }

        /// <summary>Occurs after the Collection.Insert method completes.</summary>
        /// <param name="value">The Item that was inserted into the Collection. </param>
        /// <param name="index">The index in the collection that the Item was inserted at. </param>
        protected override void OnInsertComplete(int index, object value)
        {
            // Any attached event handlers?
            if (Inserted != null)
                Inserted(index, value);
        }

        protected override void OnRemove(int index, object value)
        {
            // Any attached event handlers?
            if (Removing != null)
                Removing(index, value);
        }

        /// <summary>Occurs after the Collection.Remove method completes.</summary>
        /// <param name="value">The Item that was removed from the <see cref="T:MControl.Charts.ParameterCollection"></see>. </param>
        /// <param name="index">The index in the collection that the Item was removed from. </param>
        protected override void OnRemoveComplete(int index, object value)
        {
            // Any attached event handlers?
            if (Removed != null)
                Removed(index, value);
        }

        /// <summary>Determines the index of a specified Item object in the collection.</summary>
        /// <returns>The index of keyItem, if it is found in the collection; otherwise, -1.</returns>
        /// <param name="keyItem">The Item to locate in the Collection.</param>
        protected int IndexOf(object value)
        {
            // Find the 0 based index of the requested entry
            return base.List.IndexOf(value);
        }

        ///// <summary>Performs additional custom processes when validating a value.</summary>
        ///// <param name="o">The object being validated. </param>
        ///// <exception cref="T:System.ArgumentNullException">The object is null. </exception>
        ///// <exception cref="T:System.ArgumentException">The object is not an instance of the <see cref="T:MControl.Charts.KeyItem"></see> class or one of its derived classes. </exception>
        //protected override void OnValidate(object o)
        //{
        //    base.OnValidate(o);
        //    if (!(o is KeyItem))
        //    {
        //        throw new ArgumentException(System.Web.SR.GetString("ParameterCollection_NotParameter"), "o");
        //    }
        //}
    }
}
