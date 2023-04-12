using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace MControl.Net.Xml
{
    public class Writer //: XmlWriter
    {

        public static Writer Create()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = ("    ");
            tw = new System.IO.StringWriter();
            return Writer.Create(tw, settings);
           
        }

        static System.IO.TextWriter tw;

        public Writer()
        {

        }

        public override void Close()
        {
            if (tw != null)
            {
                tw.Close();
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected override void Dispose(bool disposing)
        {
            if (tw != null)
            {
                tw.Close();
                tw.Dispose();
                tw = null;
            }
            base.Dispose(disposing);
        }

      }
}
