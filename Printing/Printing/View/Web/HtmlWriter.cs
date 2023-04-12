using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Nistec.Printing.View.Html;
using System.Reflection;
using System.Drawing;

namespace Nistec.Printing.View.Web
{
    internal class HtmlWriter
    {
     
        private string _guid = Guid.NewGuid().ToString();

        internal HtmlWriter()
        {
        }

        internal void RenderHtmlDocument(Stream stream, Document doc, string title, int pageFrom, int pageTo)
        {
            StreamWriter writer = new StreamWriter(stream);
            WebImageSource imagesource = new WebImageSource();
            HTMLDocument document = new HTMLDocument();
            document.Title = title;
            this.RenderDocument(writer, title, document.CodePage);
            MemoryStream ms = new MemoryStream();
            document.Export(ms, doc, imagesource, pageFrom, pageTo, false);
            ms.Flush();
            writer.Write(Convert.ToBase64String(ms.ToArray()));
            imagesource.mtd710(writer, this._guid);
            writer.Flush();
        }

        private void RenderDocument(StreamWriter writer, string Subject, string charset)
        {
            writer.WriteLine("From: <Saved by " + Assembly.GetExecutingAssembly().GetName().Name + '>');
            writer.WriteLine("Subject: " + Subject);
            writer.WriteLine("MIME-Version: 1.0");
            writer.WriteLine("Content-Type: multipart/related;");
            writer.WriteLine("    boundary=\"----=_NextPart_" + this._guid + "\";");
            writer.WriteLine("    type=\"text/html\"");
            writer.WriteLine();
            writer.WriteLine("This is a multi-part message in MIME format.");
            writer.WriteLine();
            writer.WriteLine("------=_NextPart_" + this._guid);
            writer.WriteLine("Content-Type: text/html;");
            writer.WriteLine("    charset=\"" + charset + "\"");
            writer.WriteLine("Content-Transfer-Encoding: base64");
            writer.WriteLine("Content-Location: file://");
            writer.WriteLine();
        }
    }

    internal class WebImageSource : ImageSource
    {
        private mtd1130 _var5 = new mtd1130();

        internal WebImageSource()
        {
        }

        internal void mtd710(StreamWriter writer, string part)
        {
            writer.WriteLine();
            writer.WriteLine();
            for (int i = 0; i < this._var5.mtd32; i++)
            {
                mtd1129 mtd = this._var5[i];
                writer.WriteLine("------=_NextPart_" + part);
                writer.WriteLine("Content-Type: image/" + ImageWriter.mtd1117(mtd.mtd9));
                writer.WriteLine("Content-Transfer-Encoding: base64");
                writer.WriteLine("Content-Location: " + mtd.mtd281);
                writer.WriteLine();
                writer.Write(Convert.ToBase64String(mtd.mtd784));
                writer.WriteLine();
                writer.WriteLine();
            }
            writer.WriteLine("------=_NextPart_" + part);
            writer.WriteLine();
        }

        public override string GetSource(Image image)
        {
            mtd1129 mtd = this._var5[image];
            if (mtd != null)
            {
                return mtd.mtd281;
            }
            byte[] buffer = ImageSource.SaveToArray(image);
            string str = string.Format("file://{0}", ImageWriter.mtd1131(image, buffer.Length));
            this._var5.mtd2(new mtd1129(image, buffer, str));
            return str;
        }

        private class mtd1130
        {
            private mtd1129[] _var0 = new mtd1129[10];
            private int _var1;

            internal mtd1130()
            {
            }

            internal void mtd2(mtd1129 var3)
            {
                this.var4();
                this._var0[this._var1] = var3;
                this._var1++;
            }

            private void var4()
            {
                if (this._var1 >= this._var0.Length)
                {
                    mtd1129[] destinationArray = new mtd1129[2 * this._var0.Length];
                    Array.Copy(this._var0, destinationArray, this._var1);
                    this._var0 = destinationArray;
                }
            }

            internal int mtd32
            {
                get
                {
                    return this._var1;
                }
            }

            internal mtd1129 this[int var2]
            {
                get
                {
                    if ((var2 > -1) && (var2 < this._var1))
                    {
                        return this._var0[var2];
                    }
                    return null;
                }
            }

            internal mtd1129 this[Image var3]
            {
                get
                {
                    for (int i = 0; i < this._var1; i++)
                    {
                        mtd1129 mtd = this._var0[i];
                        if (var3 == mtd.mtd9)
                        {
                            return mtd;
                        }
                    }
                    return null;
                }
            }
        }
       
       
    }
}
