namespace Nistec.Charts
{
    using System;
    using System.Drawing;

    internal class DataFieldsItem
    {
        private string field;
        private Color itemColor;
        private string key;

        public DataFieldsItem(string field)
        {
            this.field = field;
            this.key = field;
            this.itemColor = Color.Red;
        }

        public string Field
        {
            get
            {
                return this.field;
            }
        }

        public Color ItemColor
        {
            get
            {
                return this.itemColor;
            }
            set
            {
                this.itemColor = value;
            }
        }

        public string Key
        {
            get
            {
                return this.key;
            }
            set
            {
                this.key = value;
            }
        }
    }
}

