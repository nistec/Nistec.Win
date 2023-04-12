namespace Nistec.Charts
{
    using System;
    using System.ComponentModel;
    using System.Drawing;

    [Serializable]//, TypeConverter(typeof(ColorItemConverter))]
    public class ColorItem
    {
        private Color c;

        public ColorItem()
        {
            this.c = Color.Red;
        }

        public ColorItem(Color color)
        {
            this.c = color;
        }

        public ColorItem(string color)
        {
            string[] strArray = color.Split(new char[] { ',' });
            this.c = Color.FromArgb(int.Parse(strArray[0]), int.Parse(strArray[1]), int.Parse(strArray[2]), int.Parse(strArray[3]));
        }

        public static implicit operator Color(ColorItem color)
        {
            return color.c;
        }

        public override string ToString()
        {
            return ("0X" + this.color.R.ToString("X2") + this.color.G.ToString("X2") + this.color.B.ToString("X2"));
        }

        public Color color
        {
            get
            {
                return this.c;
            }
            set
            {
                this.c = value;
            }
        }

        public string  Name
        {
            get
            {
                return this.c.Name;
            }
            set
            {
                this.c = Color.FromName(value);
            }
        }
    }
}

