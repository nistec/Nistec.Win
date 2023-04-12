using System;
using System.Data;
using mControl.Collections;
using mControl.GridStyle ;

namespace mControl.GridStyle
{
	/// <summary>
	/// TableCollection
	/// </summary>
	public class TableCollection : CollectionWithEvents
	{
		private Grid owner;
		
		public TableCollection(Grid grid)
		{
           this.owner=grid; 
		}

			/// <summary>
			/// Use base class to process actual collection operation
			/// </summary>
			/// <param name="value"></param>
			/// <returns></returns>
			public GridTableStyle Add(GridTableStyle value)
			{
                GridTableStyle ts=value ;
				ts.owner=this.owner;
				base.List.Add(ts as object);
				return ts;
			}

			/// <summary>
			/// Use existing method to add each array entry
			/// </summary>
			/// <param name="values"></param>
			public void AddRange(GridTableStyle[] values)
			{
				
				foreach(GridTableStyle tbl in values)
					Add(tbl);
			}

			/// <summary>
			/// Use base class to process actual collection operation
			/// </summary>
			/// <param name="value"></param>
			public void Remove(GridTableStyle value)
			{
				base.List.Remove(value as object);
			}

			/// <summary>
			/// Use base class to process actual collection operation
			/// </summary>
			/// <param name="index"></param>
			/// <param name="value"></param>
			public void Insert(int index, GridTableStyle value)
			{
				GridTableStyle ts=value ;
				ts.owner=this.owner;
				base.List.Insert(index, ts as object);
			}

			/// <summary>
			/// Use base class to process actual collection operation
			/// </summary>
			/// <param name="value"></param>
			/// <returns></returns>
			public bool Contains(GridTableStyle value)
			{
				return base.List.Contains(value as object);
			}

			/// <summary>
			/// Use base class to process actual collection operation
			/// </summary>
			public GridTableStyle this[int index]
			{
				get { return (base.List[index] as GridTableStyle); }
			}

			/// <summary>
			/// Search for a Table with a matching title
			/// </summary>
			public GridTableStyle this[string TableName]
			{
				get 
				{
					foreach(GridTableStyle tbl in base.List)
						if (tbl.TableName() == TableName)
							return tbl;

					return null;
				}
			}

		  public GridTableStyle GetTable(string mappingName)
		  {
			if(mappingName=="")
				return null;

			foreach(GridTableStyle tbl in base.List)
				if (tbl.MappingName == mappingName )
					return tbl;

			return null;
		  }

			/// <summary>
			/// Find the 0 based index of the requested entry
			/// </summary>
			/// <param name="value"></param>
			/// <returns></returns>
			public int IndexOf(GridTableStyle value)
			{
				
				return base.List.IndexOf(value);
			}
			public int IndexOf(string mapName)
			{
			
				GridTableStyle ts=this.GetTable(mapName);
				if(ts!=null)
				   return base.List.IndexOf(ts);
				return -1;
			}

            /// <summary>
            /// Copy Table collection
            /// </summary>
            /// <param name="array"></param>
			public void CopyTo(TableCollection array)//, System.Int32 index)
			{
				array.owner=this.owner;
				foreach (GridTableStyle obj in base.List)
					array.Add(obj);
			}

			public void CopyTo(GridTableStyle[] array, int index)
			{
                array=new GridTableStyle[this.Count];
				
				int i=index;
				foreach (GridTableStyle obj in base.List)
				{
					obj.owner=this.owner;
					array.SetValue(obj,i);
					i++;
				}
			}

//			public GridTableStyle Add(string tableName)
//			{
//				GridTableStyle item = new GridTableStyle(tableName);
//				return Add(item);
//			}

		}
}
