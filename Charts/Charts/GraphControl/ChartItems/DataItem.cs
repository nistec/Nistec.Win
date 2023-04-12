namespace Nistec.Charts
{
    using System;
    using System.ComponentModel;

    [Serializable, TypeConverter(typeof(DataItemConverter))]
    public class DataItem
    {
        private string name;

        public DataItem()
        {
            this.name = "New DataItem";
        }

        public DataItem(string dataItem)
        {
            this.name = dataItem;
        }

        public static implicit operator string(DataItem dataItem)
        {
            return dataItem.name;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }
    }
}

