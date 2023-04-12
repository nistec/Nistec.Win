using System;
using System.Runtime.InteropServices;

using Nistec.Collections ;

namespace Nistec.WinForms
{

	#region TaskPanelCollection

	/// <summary>
	/// Summary description for TaskPanelCollection.
	/// </summary>
	public class TaskPanelCollection : Nistec.Collections.CollectionWithEvents// System.Collections.CollectionBase
	{
        

		#region Public Constructors
		/// <summary>
		/// Initialises a new instance of <see cref="TaskPanelCollection">TaskPanelCollection</see>.
		/// </summary>
        public TaskPanelCollection()
		{
            
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Adds a <see cref="McTaskPanel">McTaskPanel</see> to the end of the collection.
		/// </summary>
		/// <param name="panel">The <see cref="McTaskPanel">McTaskPanel</see> to add.</param>
		public McTaskPanel Add(McTaskPanel panel)
		{
			base.List.Add(panel as object );
			return panel;
		}

		public void AddRange(McTaskPanel[] panels)
		{
			// Use existing method to add each array entry
			foreach(McTaskPanel pnl in panels)
				Add(pnl);
		}

		/// <summary>
		/// Removes the <see cref="McTaskPanel">McTaskPanel</see> from the collection at the specified index.
		/// </summary>
		/// <param name="index">The index of the <see cref="McTaskPanel">McTaskPanel</see> to remove.</param>
		public void Remove(int index)
		{
			if(index >= 0 && index < this.Count)
				this.List.RemoveAt(index);
		}

		/// <summary>
		/// Gets a reference to the <see cref="McTaskPanel">McTaskPanel</see> at the specified index.
		/// </summary>
		/// <param name="index">The index of the <see cref="McTaskPanel">McTaskPanel</see> to retrieve.</param>
		/// <returns></returns>
		public McTaskPanel this[int index]
		{
			// Use base class to process actual collection operation
			get 
			{
				// Ensure the supplied index is valid
				if((index >= this.Count) || (index < 0))
				{
					throw new IndexOutOfRangeException("The supplied index is out of range");
				}

				return (base.List[index] as McTaskPanel); 
			}
		}

		/// <summary>
		/// Inserts a <see cref="McTaskPanel">McTaskPanel</see> at the specified position.
		/// </summary>
		/// <param name="index">The zero-based index at which <i>panel</i> should be inserted.</param>
		/// <param name="panel">The <see cref="McTaskPanel">McTaskPanel</see> to insert into the collection.</param>
		public void Insert(int index, McTaskPanel panel)
		{
			this.List.Insert(index, panel);
		}

		/// <summary>
		/// Copies the elements of the collection to a <see cref="System.Array">System.Array</see>, starting at a particular index.
		/// </summary>
		/// <param name="array">The one-dimensional <see cref="System.Array">System.Array</see> that is the destination of the elements. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <i>array</i> at which copying begins.</param>
		public void CopyTo(System.Array array, System.Int32 index)
		{
			this.List.CopyTo(array, index);
		}

		/// <summary>
		/// Searches for the specified <see cref="McTaskPanel">McTaskPanel</see> and returns the zero-based index of the first occurrence.
		/// </summary>
		/// <param name="panel">The <see cref="McTaskPanel">McTaskPanel</see> to search for.</param>
		/// <returns></returns>
		public int IndexOf(McTaskPanel panel)
		{
			return this.List.IndexOf(panel);
		}

		public bool Contains(McTaskPanel panel)
		{
			// Use base class to process actual collection operation
			return base.List.Contains(panel as object);
		}

		#endregion
	}
	#endregion

	#region PanelControlsCollection
	/// <summary>
	/// Summary description for PanelControlsCollection.
	/// </summary>
	public class PanelControlsCollection :  Nistec.Collections.CollectionWithEvents//System.Collections.CollectionBase
	{
        internal McTaskPanel parent;

		#region Public Constructors
		/// <summary>
		/// Initialises a new instance of <see cref="TaskPanelCollection">PanelControlsCollection</see>.
		/// </summary>
        public PanelControlsCollection(McTaskPanel ctl)
		{
            parent = ctl;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Adds a <see cref="McTaskPanel">McTaskPanel</see> to the end of the collection.
		/// </summary>
		/// <param name="panel">The <see cref="McTaskPanel">McTaskPanel</see> to add.</param>
		public LinkLabelItem Add(Nistec.WinForms.LinkLabelItem  lbl)
		{
            lbl.owner = parent;
			this.List.Add(lbl as object );
			return lbl;
		}

		public void AddRange(Nistec.WinForms.LinkLabelItem[] lbls)
		{
			// Use existing method to add each array entry
			foreach(LinkLabelItem lbl in lbls)
				Add(lbl);
		}

		/// <summary>
		/// Removes the <see cref="McTaskPanel">McTaskPanel</see> from the collection at the specified index.
		/// </summary>
		/// <param name="index">The index of the <see cref="McTaskPanel">McTaskPanel</see> to remove.</param>
		public void Remove(int index)
		{
			if(index >= 0 && index < this.Count)
				this.List.RemoveAt(index);
		}

		/// <summary>
		/// Gets a reference to the <see cref="McTaskPanel">McTaskPanel</see> at the specified index.
		/// </summary>
		/// <param name="index">The index of the <see cref="McTaskPanel">McTaskPanel</see> to retrieve.</param>
		/// <returns></returns>
		public Nistec.WinForms.LinkLabelItem this[int index]
		{
			get
			{
				// Ensure the supplied index is valid
				if((index >= this.Count) || (index < 0))
				{
					throw new IndexOutOfRangeException("The supplied index is out of range");
				}
					return (Nistec.WinForms.LinkLabelItem)this.List[index];
			}
		}

		/// <summary>
		/// Inserts a <see cref="McTaskPanel">McTaskPanel</see> at the specified position.
		/// </summary>
		/// <param name="index">The zero-based index at which <i>panel</i> should be inserted.</param>
		/// <param name="panel">The <see cref="McTaskPanel">McTaskPanel</see> to insert into the collection.</param>
		public void Insert(int index, Nistec.WinForms.LinkLabelItem  lbl)
		{
            lbl.owner = parent;
            this.List.Insert(index, lbl);
		}

		/// <summary>
		/// Copies the elements of the collection to a <see cref="System.Array">System.Array</see>, starting at a particular index.
		/// </summary>
		/// <param name="array">The one-dimensional <see cref="System.Array">System.Array</see> that is the destination of the elements. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <i>array</i> at which copying begins.</param>
		public void CopyTo(System.Array array, System.Int32 index)
		{
			this.List.CopyTo(array, index);
		}

		/// <summary>
		/// Searches for the specified <see cref="McTaskPanel">McTaskPanel</see> and returns the zero-based index of the first occurrence.
		/// </summary>
		/// <param name="panel">The <see cref="McTaskPanel">McTaskPanel</see> to search for.</param>
		/// <returns></returns>
		public int IndexOf(LinkLabelItem  lbl)
		{
			return this.List.IndexOf(lbl);
		}

		public bool Contains(Nistec.WinForms.LinkLabelItem  lbl)
		{
			// Use base class to process actual collection operation
			return base.List.Contains(lbl as object);
		}

		#endregion
	}
	#endregion
}
