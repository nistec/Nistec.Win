
using System;
using Nistec.Collections;
 
namespace Nistec.WinForms
{
    public class TabPageCollection : CollectionWithEvents
    {
        public McTabPage Add(McTabPage value)
        {
            // Use base class to process actual collection operation
            base.List.Add(value as object);

            return value;
        }

        public void AddRange(McTabPage[] values)
        {
            // Use existing method to add each array entry
            foreach(McTabPage page in values)
                Add(page);
        }

        public void Remove(McTabPage value)
        {
            // Use base class to process actual collection operation
            base.List.Remove(value as object);
        }

        public void Insert(int index, McTabPage value)
        {
            // Use base class to process actual collection operation
            base.List.Insert(index, value as object);
        }

		public void MoveTo(int index, McTabPage value)
		{
			// Use base class to process actual collection operation
			base.List.Remove(value as object);
			base.List.Insert(index, value as object);
		}

        public bool Contains(McTabPage value)
        {
            // Use base class to process actual collection operation
            return base.List.Contains(value as object);
        }

        public McTabPage this[int index]
        {
            // Use base class to process actual collection operation
            get 
            {
                if (index < 0 || index >= Count)
                    return null;
                return (base.List[index] as McTabPage); 
            }
        }

        public McTabPage this[string title]
        {
            get 
            {
                // Search for a Page with a matching title
                foreach(McTabPage page in base.List)
                    if (page.Text == title)
                        return page;

                return null;
            }
        }

        public int IndexOf(McTabPage value)
        {
            // Find the 0 based index of the requested entry
            return base.List.IndexOf(value);
        }

		public McTabPage Add(string title)
		{
			McTabPage item = new McTabPage(title);
			return Add(item);
		}
		
//		public McTabPage Add(string title, StyleGuide style)
//		{
//			McTabPage item = new McTabPage(title);
//			//item.StylePlan = style;
//			return Add(item);
//		}

		public void CopyTo(TabPageCollection array, System.Int32 index)
		{
			foreach (McTabPage obj in base.List)
				array.Add(obj);
		}

    }
}
