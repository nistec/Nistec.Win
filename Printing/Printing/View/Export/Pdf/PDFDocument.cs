namespace Nistec.Printing.View.Pdf
{
    using Nistec.Printing.View;
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Security.Permissions;
    using System.Threading;

    public class PDFDocument
    {
        private PDFVersion _var0;
        private Nistec.Printing.View.Pdf.mtd746 _var1;
        private mtd641 _var10;
        private byte[] _var11;
        private bool _var12;
        private string _var13;
        private bool _var14;
        private Nistec.Printing.View.Pdf.mtd761 _var2;
        private Nistec.Printing.View.Pdf.mtd785 _var3;
        private mtd1058 _var4;
        private Nistec.Printing.View.Pdf.mtd242 _var5;
        private Nistec.Printing.View.Pdf.mtd757 _var6 = null;
        private bool _var7;
        private Nistec.Printing.View.Pdf.SecurityManager _var8;
        private Nistec.Printing.View.Pdf.mtd712 _var9;
        internal static mtd1059 mtd1060;

        public PDFDocument()
        {
            this._var1 = new Nistec.Printing.View.Pdf.mtd746(this);
            this._var2 = new Nistec.Printing.View.Pdf.mtd761(this);
            this._var3 = new Nistec.Printing.View.Pdf.mtd785(this);
            this._var4 = new mtd1058(this);
            this._var5 = new Nistec.Printing.View.Pdf.mtd242(this);
            this._var7 = true;
            this._var0 = PDFVersion.PDF13;
            this._var9 = null;
            this._var8 = null;
            this._var10 = new mtd641(StandardFonts.mtd684, FontStyle.Regular);
            this._var12 = true;
            this._var14 = false;
            this._var11 = Guid.NewGuid().ToByteArray();
        }

        public static void AddFontFileInfo(string fontname, FontStyle fontstyle, string fontfile)
        {
            if (mtd1060 == null)
            {
                mtd1060 = new mtd1059();
            }
            mtd1060.mtd2(fontname, fontstyle, fontfile);
        }

        public byte[] Export(Document document)
        {
            mtd805 mtd = new mtd805();
            this.var15(document, mtd);
            mtd.mtd1009();
            return mtd.mtd784;
        }

        public void Export(Document document, Stream stream)
        {
            try
            {
                if (stream is FileStream)
                {
                    mtd1010 mtd = new mtd1010(stream);
                    this.var15(document, mtd);
                }
                else
                {
                    byte[] buffer = this.Export(document);
                    stream.Write(buffer, 0, buffer.Length);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public void Export(Document document, string filename)
        {
            new FileIOPermission(FileIOPermissionAccess.NoAccess, filename).Demand();
            FileStream stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);
            stream.Seek(0L, SeekOrigin.Begin);
            try
            {
                mtd1010 mtd = new mtd1010(stream);
                this.var15(document, mtd);
            }
            finally
            {
                stream.Flush();
                stream.Close();
            }
        }

        private void var15(Document var16, mtd711 var17)
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            try
            {
                string str;
                this._var6 = new Nistec.Printing.View.Pdf.mtd757(this);
                mtd1061 mtd = new mtd1061(this);
                if (this._var0 == PDFVersion.PDF14)
                {
                    str = "1.4";
                }
                else if (this._var0 == PDFVersion.PDF15)
                {
                    str = "1.5";
                }
                else
                {
                    str = "1.3";
                }
                if (this._var8 != null)
                {
                    if (this._var8.Encryption == Encryption.Use40BitKey)
                    {
                        this._var9 = new mtd806(this);
                    }
                    else
                    {
                        this._var9 = new mtd803(this);
                        if (this._var0 == PDFVersion.PDF13)
                        {
                            str = "1.4";
                        }
                    }
                    this._var9.mtd172();
                }
                else
                {
                    this._var9 = null;
                }
                var17.mtd715("%PDF-" + str);
                var17.mtd715("%PDF-Writer.NET Nistec Ltd.");
                this._var5.mtd780();
                this._var5.mtd710(ref var17);
                this._var3.mtd780();
                this._var3.mtd710(ref var17);
                this._var4.mtd780(var16);
                this._var4.mtd710(ref mtd, ref var17);
                mtd1062 mtd2 = mtd.mtd1063;
                for (int i = 0; i < mtd2.mtd32; i++)
                {
                    mtd2[i].mtd710(ref var17);
                }
                this._var2.mtd780();
                this._var2.mtd710(ref var17);
                this._var1.mtd780();
                this._var1.mtd710(ref var17);
                if (this._var9 != null)
                {
                    this._var9.mtd710(ref var17);
                }
                this._var6.mtd710(ref var17);
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

        internal mtd641 mtd1003
        {
            get
            {
                return this._var10;
            }
        }

        internal Nistec.Printing.View.Pdf.mtd242 mtd242
        {
            get
            {
                return this._var5;
            }
        }

        internal Nistec.Printing.View.Pdf.mtd712 mtd712
        {
            get
            {
                return this._var9;
            }
        }

        internal Nistec.Printing.View.Pdf.mtd746 mtd746
        {
            get
            {
                return this._var1;
            }
        }

        internal Nistec.Printing.View.Pdf.mtd757 mtd757
        {
            get
            {
                return this._var6;
            }
        }

        internal mtd1058 mtd760
        {
            get
            {
                return this._var4;
            }
        }

        internal Nistec.Printing.View.Pdf.mtd761 mtd761
        {
            get
            {
                return this._var2;
            }
        }

        internal Nistec.Printing.View.Pdf.mtd762 mtd762
        {
            get
            {
                return this._var2.mtd762;
            }
        }

        internal Nistec.Printing.View.Pdf.mtd785 mtd785
        {
            get
            {
                return this._var3;
            }
        }

        internal byte[] mtd789
        {
            get
            {
                return this._var11;
            }
        }

        public string Author
        {
            get
            {
                return this._var5.mtd768;
            }
            set
            {
                this._var5.mtd768 = value;
            }
        }

        public bool CenterWindow
        {
            get
            {
                return this._var1.mtd753;
            }
            set
            {
                this._var1.mtd753 = value;
            }
        }

        public bool Compress
        {
            get
            {
                return this._var7;
            }
            set
            {
                this._var7 = value;
            }
        }

        public DateTime CreationDate
        {
            get
            {
                return this._var5.mtd773;
            }
            set
            {
                this._var5.mtd773 = value;
            }
        }

        public string Creator
        {
            get
            {
                return this._var5.mtd771;
            }
            set
            {
                this._var5.mtd771 = value;
            }
        }

        public bool DisplayDocTitle
        {
            get
            {
                return this._var1.mtd754;
            }
            set
            {
                this._var1.mtd754 = value;
            }
        }

        public bool EmbedFont
        {
            get
            {
                return this._var12;
            }
            set
            {
                this._var12 = value;
            }
        }

        public bool FitWindow
        {
            get
            {
                return this._var1.mtd752;
            }
            set
            {
                this._var1.mtd752 = value;
            }
        }

        public bool HideMenubar
        {
            get
            {
                return this._var1.mtd750;
            }
            set
            {
                this._var1.mtd750 = value;
            }
        }

        public bool HideToolbar
        {
            get
            {
                return this._var1.mtd749;
            }
            set
            {
                this._var1.mtd749 = value;
            }
        }

        public bool HideWindowUI
        {
            get
            {
                return this._var1.mtd751;
            }
            set
            {
                this._var1.mtd751 = value;
            }
        }

        public string Keywords
        {
            get
            {
                return this._var5.mtd770;
            }
            set
            {
                this._var5.mtd770 = value;
            }
        }

        public DateTime ModDate
        {
            get
            {
                return this._var5.mtd774;
            }
            set
            {
                this._var5.mtd774 = value;
            }
        }

        public Nistec.Printing.View.Pdf.PageMode NonFullScreenPageMode
        {
            get
            {
                return this._var1.mtd755;
            }
            set
            {
                this._var1.mtd755 = value;
            }
        }

        public Nistec.Printing.View.Pdf.PageMode PageMode
        {
            get
            {
                return this._var1.mtd748;
            }
            set
            {
                this._var1.mtd748 = value;
            }
        }

        public string Producer
        {
            get
            {
                return this._var5.mtd772;
            }
            set
            {
                this._var5.mtd772 = value;
            }
        }

        public string ResourcePath
        {
            get
            {
                return this._var13;
            }
            set
            {
                this._var13 = value;
            }
        }

        public Nistec.Printing.View.Pdf.SecurityManager SecurityManager
        {
            get
            {
                return this._var8;
            }
            set
            {
                this._var8 = value;
            }
        }

        public string Subject
        {
            get
            {
                return this._var5.mtd769;
            }
            set
            {
                this._var5.mtd769 = value;
            }
        }

        public string Title
        {
            get
            {
                return this._var5.mtd767;
            }
            set
            {
                this._var5.mtd767 = value;
            }
        }

        public bool UseLocalFont
        {
            get
            {
                return this._var14;
            }
            set
            {
                this._var14 = value;
            }
        }

        public PDFVersion Version
        {
            get
            {
                return this._var0;
            }
            set
            {
                this._var0 = value;
            }
        }
    }
}

