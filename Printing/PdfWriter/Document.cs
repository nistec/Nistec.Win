namespace MControl.Printing.Pdf
{
    using MControl.Printing.Pdf.Core.Element;
    using MControl.Printing.Pdf.Core.Encrypt;
    using MControl.Printing.Pdf.Controls;
    using MControl.Printing.Pdf.Drawing;
    using MControl.Printing.Pdf.Core.IO;
    using MControl.Printing.Pdf.Core;
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Security.Permissions;
    using System.Threading;
    using System.Web;

    public class Document
    {
        private PdfVersion _b0;
        private MControl.Printing.Pdf.Core.Element.A185 _b1;
        private PdfFont _b10;
        private byte[] _b11;
        private bool _b12;
        private PdfForm _b13;
        private MControl.Printing.Pdf.Core.Element.A195 _b2;
        private MControl.Printing.Pdf.Core.Element.A222 _b3;
        private MControl.Printing.Pdf.Pages _b4;
        private MControl.Printing.Pdf.Core.Element.A199 _b5;
        private MControl.Printing.Pdf.Core.Element.A93 _b6 = null;
        private bool _b7;
        private MControl.Printing.Pdf.SecurityManager _b8;
        private MControl.Printing.Pdf.Core.Encrypt.A56 _b9;

        public Document()
        {
            this._b1 = new MControl.Printing.Pdf.Core.Element.A185(this);
            this._b2 = new MControl.Printing.Pdf.Core.Element.A195(this);
            this._b3 = new MControl.Printing.Pdf.Core.Element.A222(this);
            this._b4 = new MControl.Printing.Pdf.Pages(this);
            this._b5 = new MControl.Printing.Pdf.Core.Element.A199(this);
            this._b7 = true;
            this._b0 = PdfVersion.Pdf13;
            this._b9 = null;
            this._b8 = null;
            this._b10 = new PdfFont(StandardFonts.Helvetica, FontStyle.Regular);
            this._b11 = Guid.NewGuid().ToByteArray();
            this._b12 = false;
            this._b13 = new PdfForm(this);
        }

        public byte[] Generate()
        {
            A243 A = new A243();
            this.b14(A);
            A.A552();
            return A.A221;
        }

        public void Generate(Stream stream)
        {
            try
            {
                if (stream is FileStream)
                {
                    A553 A = new A553(stream);
                    this.b14(A);
                }
                else
                {
                    byte[] buffer = this.Generate();
                    stream.Write(buffer, 0, buffer.Length);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public void Generate(string filename)
        {
            new FileIOPermission(FileIOPermissionAccess.NoAccess, filename).Demand();
            FileStream stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);
            stream.Seek(0L, SeekOrigin.Begin);
            try
            {
                A553 A = new A553(stream);
                this.b14(A);
            }
            finally
            {
                stream.Flush();
                stream.Close();
            }
        }

        public void Generate(HttpResponse response, string filename)
        {
            this.Generate(response, filename, true);
        }

        public void Generate(HttpResponse response, string filename, bool IsInline)
        {
            byte[] buffer = this.Generate();
            this.b15(response, buffer, filename, IsInline);
        }

        private void b14(A55 b16)
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            try
            {
                string str;
                this._b6 = new MControl.Printing.Pdf.Core.Element.A93(this);
                this.b17(this._b4);
                A598 A = new A598(this);
                if (this._b0 == PdfVersion.Pdf14)
                {
                    str = "1.4";
                }
                else if (this._b0 == PdfVersion.Pdf15)
                {
                    str = "1.5";
                }
                else
                {
                    str = "1.3";
                }
                if (this._b8 != null)
                {
                    if (this._b8.Encryption == Encryption.Use40BitKey)
                    {
                        this._b9 = new A244(this);
                    }
                    else
                    {
                        this._b9 = new A241(this);
                        if (this._b0 == PdfVersion.Pdf13)
                        {
                            str = "1.4";
                        }
                    }
                    this._b9.A237();
                }
                else
                {
                    this._b9 = null;
                }
                b16.A59("%Pdf" + str);
                b16.A59("%PdfWriter MControl.Net.");
                this._b5.A110();
                this._b5.A54(ref b16);
                this._b3.A110();
                this._b3.A54(ref b16);
                this._b4.A110();
                this._b4.A54(ref A, ref b16);
                A599 A2 = A.A600;
                for (int i = 0; i < A2.A2; i++)
                {
                    A2[i].A54(ref b16);
                }
                this._b2.A110();
                this._b2.A54(ref b16);
                if (this._b13.Fields.Size > 0)
                {
                    this._b13.A110();
                    this._b13.A54(ref b16);
                }
                this._b1.A110();
                this._b1.A54(ref b16);
                if (this._b9 != null)
                {
                    this._b9.A54(ref b16);
                }
                this._b6.A54(ref b16);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                Thread.CurrentThread.CurrentCulture = currentCulture;
            }
        }

        private void b15(HttpResponse b18, byte[] b19, string b20, bool b21)
        {
            if ((b19 != null) && (b19.Length > 0))
            {
                b18.ClearContent();
                b18.ClearHeaders();
                b18.Buffer = true;
                b18.Expires = 0;
                b18.Cache.SetCacheability(HttpCacheability.Private);
                if (b21)
                {
                    b18.ContentType = "application/pdf";
                    b18.AddHeader("Content-Type", b18.ContentType);
                    b18.AddHeader("Content-Disposition", string.Format("inline;filename={0}.{1}", b20, "pdf"));
                }
                else
                {
                    b18.ContentType = "application/x-msdownload";
                    b18.AddHeader("Content-Type", b18.ContentType);
                    b18.AddHeader("Content-Disposition", string.Format("attachment;filename={0}.{1}", b20, "pdf"));
                }
                b18.BinaryWrite(b19);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                try
                {
                    b18.End();
                }
                catch
                {
                }
            }
        }

        private void b17(MControl.Printing.Pdf.Pages b4)
        {
            PdfFont font = new PdfFont(StandardFonts.Helvetica, FontStyle.Underline);
            for (int i = 0; i < b4.Count; i++)
            {
                Page page = b4[i];
                PdfGraphics graphics = page.Graphics;
                graphics.DrawString(20f, 20f, "PdfWriter", PdfColor.Gray, this.A428, 8f);
                graphics.DrawLinkToURI("http://www.mcontrolnet.com", "www.mcontrolnet.com", font, 8f, PdfColor.Blue, 70f, 30f, false);
            }
        }

        internal MControl.Printing.Pdf.Core.Element.A185 A185
        {
            get
            {
                return this._b1;
            }
        }

        internal MControl.Printing.Pdf.Core.Element.A195 A195
        {
            get
            {
                return this._b2;
            }
        }

        internal MControl.Printing.Pdf.Core.Element.A199 A199
        {
            get
            {
                return this._b5;
            }
        }

        internal MControl.Printing.Pdf.Core.Element.A222 A222
        {
            get
            {
                return this._b3;
            }
        }

        internal byte[] A226
        {
            get
            {
                return this._b11;
            }
        }

        internal PdfFont A428
        {
            get
            {
                return this._b10;
            }
        }

        internal MControl.Printing.Pdf.Core.Encrypt.A56 A56
        {
            get
            {
                return this._b9;
            }
        }

        internal MControl.Printing.Pdf.Core.Element.A93 A93
        {
            get
            {
                return this._b6;
            }
        }

        public string Author
        {
            get
            {
                return this._b5.A201;
            }
            set
            {
                this._b5.A201 = value;
            }
        }

        public MControl.Printing.Pdf.Bookmarks Bookmarks
        {
            get
            {
                return this._b2.A208;
            }
        }

        public bool CacheImages
        {
            get
            {
                return this._b12;
            }
            set
            {
                this._b12 = value;
            }
        }

        public bool CenterWindow
        {
            get
            {
                return this._b1.A191;
            }
            set
            {
                this._b1.A191 = value;
            }
        }

        public bool Compress
        {
            get
            {
                return this._b7;
            }
            set
            {
                this._b7 = value;
            }
        }

        public DateTime CreationDate
        {
            get
            {
                return this._b5.A206;
            }
            set
            {
                this._b5.A206 = value;
            }
        }

        public string Creator
        {
            get
            {
                return this._b5.A204;
            }
            set
            {
                this._b5.A204 = value;
            }
        }

        public bool DisplayDocTitle
        {
            get
            {
                return this._b1.A192;
            }
            set
            {
                this._b1.A192 = value;
            }
        }

        public bool FitWindow
        {
            get
            {
                return this._b1.A190;
            }
            set
            {
                this._b1.A190 = value;
            }
        }

        public PdfForm Form
        {
            get
            {
                return this._b13;
            }
            set
            {
                this._b13 = value;
            }
        }

        public bool HideMenubar
        {
            get
            {
                return this._b1.A188;
            }
            set
            {
                this._b1.A188 = value;
            }
        }

        public bool HideToolbar
        {
            get
            {
                return this._b1.A187;
            }
            set
            {
                this._b1.A187 = value;
            }
        }

        public bool HideWindowUI
        {
            get
            {
                return this._b1.A189;
            }
            set
            {
                this._b1.A189 = value;
            }
        }

        public string Keywords
        {
            get
            {
                return this._b5.A203;
            }
            set
            {
                this._b5.A203 = value;
            }
        }

        public DateTime ModDate
        {
            get
            {
                return this._b5.A207;
            }
            set
            {
                this._b5.A207 = value;
            }
        }

        public MControl.Printing.Pdf.PageMode NonFullScreenPageMode
        {
            get
            {
                return this._b1.A193;
            }
            set
            {
                this._b1.A193 = value;
            }
        }

        public string OpenActionJavaScript
        {
            get
            {
                return this._b1.A194;
            }
            set
            {
                this._b1.A194 = value;
            }
        }

        public MControl.Printing.Pdf.PageMode PageMode
        {
            get
            {
                return this._b1.A186;
            }
            set
            {
                this._b1.A186 = value;
            }
        }

        public MControl.Printing.Pdf.Pages Pages
        {
            get
            {
                return this._b4;
            }
        }

        public string Producer
        {
            get
            {
                return this._b5.A205;
            }
            set
            {
                this._b5.A205 = value;
            }
        }

        public MControl.Printing.Pdf.SecurityManager SecurityManager
        {
            get
            {
                return this._b8;
            }
            set
            {
                this._b8 = value;
            }
        }

        public string Subject
        {
            get
            {
                return this._b5.A202;
            }
            set
            {
                this._b5.A202 = value;
            }
        }

        public string Title
        {
            get
            {
                return this._b5.A200;
            }
            set
            {
                this._b5.A200 = value;
            }
        }

        public PdfVersion Version
        {
            get
            {
                return this._b0;
            }
            set
            {
                this._b0 = value;
            }
        }
    }
}

