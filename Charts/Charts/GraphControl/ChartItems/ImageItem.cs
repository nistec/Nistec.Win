namespace Nistec.Charts
{
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Web.UI.Design;

    [Serializable]//, TypeConverter(typeof(ImageItemConverter))]
    public class ImageItem
    {
        private string imageUrl;

        public ImageItem()
        {
            this.imageUrl = "";
        }

        public ImageItem(string imageItem)
        {
            this.imageUrl = imageItem;
        }

        public static implicit operator string(ImageItem imageItem)
        {
            return imageItem.ImageUrl;
        }

        [Editor(typeof(UrlEditor), typeof(UITypeEditor))]
        public string ImageUrl
        {
            get
            {
                return this.imageUrl;
            }
            set
            {
                this.imageUrl = value;
            }
        }
    }
}

