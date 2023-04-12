namespace Nistec.Charts
{
    using System;
    using System.ComponentModel;

    [Serializable]//, TypeConverter(typeof(KeyItemConverter))]
    public class KeyItem
    {
        internal string hint;
        private string name;

        public KeyItem()
        {
            this.name = "New KeyItem";
        }

        public KeyItem(string keyItem)
        {
            this.name = keyItem;
        }

        public static implicit operator string(KeyItem keyItem)
        {
            return keyItem.name;
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

