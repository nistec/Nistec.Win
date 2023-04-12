namespace Nistec.Printing.View.Html
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Drawing.Imaging;

    public class FileImageSource : ImageSource
    {
        private string _var3;
        private ArrayList _var4;

        public FileImageSource(string var5)
        {
            this._var3 = var5;
            this._var4 = new ArrayList();
        }

        public override string GetSource(Image image)
        {
            int num = 0;
            string filename = null;
            var0 var = null;
            while (num < this._var4.Count)
            {
                var = (var0) this._var4[num];
                if (image == var.mtd9)
                {
                    return var.mtd635;
                }
                num++;
            }
            ImageFormat png = ImageFormat.Png;
            string ext = ".png";
            ImageSource.SetImageFormat(image, out png, out ext);
            filename = string.Format(@"{0}\img{1}{2}", this._var3, num, ext);
            ImageSource.SaveToFile(image, png, filename);
            this._var4.Add(new var0(image, filename));
            return filename;
        }

        private class var0
        {
            private Image var1;
            private string var2;

            internal var0(Image var1, string var2)
            {
                this.var1 = var1;
                this.var2 = var2;
            }

            internal string mtd635
            {
                get
                {
                    return this.var2;
                }
            }

            internal Image mtd9
            {
                get
                {
                    return this.var1;
                }
            }
        }
    }
}

