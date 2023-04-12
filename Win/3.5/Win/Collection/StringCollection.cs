
using System;
using System.Collections;
using System.Xml;

namespace MControl.Collections
{
    public class StringCollection : CollectionWithEvents,ICloneable
    {

		public void Add(string str)
		{
			// Use base class to process actual collection operation
			//base.List.Add(value as object);
			lock (base.List)
			{
				base.List.Add(str);
			}
		}

		public void AddRange(string[] values)
		{
			// Use existing method to add each array entry
			//foreach(String item in values)
			//    Add(item);
			lock (base.InnerList)
			{
				base.InnerList.AddRange(values);
			}
		}

		public void AddRange(StringCollection values)
		{
			foreach (string text1 in values)
			{
				this.Add(text1);
			}
		}

		public object Clone()
		{
			object obj1;
			lock (base.List)
			{
				StringCollection collection1 = new StringCollection();
				foreach (string text1 in base.List)
				{
					collection1.Add((string) text1.Clone());
				}
				obj1 = collection1;
			}
			return obj1;
		}

        public void Remove(string str)
        {
            // Use base class to process actual collection operation
            //base.List.Remove(value as object);
			lock (base.List)
			{
				base.List.Remove(str);
			}

        }

        public void Insert(int index, string value)
        {
            // Use base class to process actual collection operation
            //base.List.Insert(index, value as object);
			lock (base.List)
			{
				base.List.Insert(index, value);
			}
		}

 		public bool Contains(string str)
		{
			// Value comparison
			//foreach(String s in base.List)
			//	if (str.Equals(s))
			//		return true;

			//return false;
			
			bool flag1;
			lock (base.List)
			{
				flag1 = base.List.Contains(str);
			}
			return flag1;
		}

        public bool Contains(StringCollection values)
        {
			foreach(String c in values)
			{
	            // Use base class to process actual collection operation
				if (Contains(c))
					return true;
			}

			return false;
        }

 		public string this[int index]
		{
			// Use base class to process actual collection operation
			//get { return (base.List[index] as String); }
			get
			{
				string text1;
				lock (base.List)
				{
					text1 = (string) base.List[index];
				}
				return text1;
			}
			set
			{
				lock (base.List)
				{
					base.List[index] = value;
				}
			}
		}

		public string this[string name]
		{
			get
			{
				string text2;
				lock (base.List)
				{
					foreach (string text1 in base.List)
					{
						if (text1 == name)
						{
							return text1;
						}
					}
					text2 = null;
				}
				return text2;
			}
		}

        public int IndexOf(string str)
        {
            // Find the 0 based index of the requested entry
            //return base.List.IndexOf(value);
			int num1;
			lock (base.List)
			{
				num1 = base.List.IndexOf(str);
			}
			return num1;
		}

		public void CopyTo(StringCollection array, System.Int32 index)
		{
			foreach (string obj in base.List)
				array.Add(obj);
		}


		#region xml

		public void SaveToXml(string name, XmlTextWriter xmlOut)
		{
			xmlOut.WriteStartElement(name);
			xmlOut.WriteAttributeString("Count", this.Count.ToString());

			foreach(String s in base.List)
			{
				xmlOut.WriteStartElement("Item");
				xmlOut.WriteAttributeString("Name", s);
				xmlOut.WriteEndElement();
			}

			xmlOut.WriteEndElement();
		}

		public void LoadFromXml(string name, XmlTextReader xmlIn)
		{
			// Move to next xml node
			if (!xmlIn.Read())
				throw new ArgumentException("Could not read in next expected node");

			// Check it has the expected name
			if (xmlIn.Name != name)
				throw new ArgumentException("Incorrect node name found");

			this.Clear();

			// Grab raw position information
			string attrCount = xmlIn.GetAttribute(0);

			// Convert from string to proper types
			int count = int.Parse(attrCount);

			for(int index=0; index<count; index++)
			{
				// Move to next xml node
				if (!xmlIn.Read())
					throw new ArgumentException("Could not read in next expected node");

				// Check it has the expected name
				if (xmlIn.Name != "Item")
					throw new ArgumentException("Incorrect node name found");

				this.Add(xmlIn.GetAttribute(0));
			}

			if (count > 0)
			{
				// Move over the end element of the collection
				if (!xmlIn.Read())
					throw new ArgumentException("Could not read in next expected node");

				// Check it has the expected name
				if (xmlIn.Name != name)
					throw new ArgumentException("Incorrect node name found");
			}
		}
	
		#endregion

    }
}
