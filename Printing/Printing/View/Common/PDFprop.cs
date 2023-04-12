using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;


namespace Nistec.Printing.View
{
	public class PDFprop //cls1084
	{

		// Fields
		private string var0;
		private PDFVersion _PDFVersion;//var1;
		private bool var2;
		private bool var3;
		private string var4;
		private string var5;
		private Pdf.Encryption _Encryption;//var6;
		private bool var7;

     private string _Author;
     private string _Creator;


        public static PDFprop Default()
        {
            PDFprop p = new PDFprop();
            return p;
        }

		internal PDFprop()
		{
			this.cls111();
		}

 
		internal void cls111()
		{
			this.var0 = string.Empty;
			this._PDFVersion = PDFVersion.PDF14;
			this.var2 = true;
			this.var3 = true;
			this.var4 = string.Empty;
			this.var5 = string.Empty;
			this._Encryption = Pdf.Encryption.Use40BitKey;
			this.var7 = false;
		}

 
		public bool ShouldSerializeEncryption()
		{
			return (this._Encryption != Pdf.Encryption.Use40BitKey);
		}

		public bool ShouldSerializeOwnerPassword()
		{
			return (this.var4 != null);
		}

 
		public bool ShouldSerializeTitle()
		{
			return (this.var0 != null);
		}

 
		public bool ShouldSerializeUserPassword()
		{
			return (this.var5 != null);
		}

 
		public bool ShouldSerializeVersion()
		{
			return (this._PDFVersion != PDFVersion.PDF14);
		}

		[Description("Determines whether to compress content stream."), DefaultValue(true)]
		public bool Compress
		{
			get
			{
				return this.var2;
			}
			set
			{
				this.var2 = value;
			}
		}
 
		[DefaultValue(true), Description("Determines whether to embed font file into PDF.")]
		public bool EmbedFont
		{
			get
			{
				return this.var3;
			}
			set
			{
				this.var3 = value;
			}
		}
 
		[DefaultValue(false), Description("Determines whether to encrypt the PDF file.")]
		public bool Encrypt
		{
			get
			{
				return this.var7;
			}
			set
			{
				this.var7 = value;
			}
		}
 
		[Description("Encryption Key used to encrypt the PDF file.")]
		public Pdf.Encryption Encryption
		{
			get
			{
				return this._Encryption;
			}
			set
			{
				this._Encryption = value;
			}
		}
 
		[Description("Owner password for the PDF Document.")]
		public string OwnerPassword
		{
			get
			{
				return this.var4;
			}
			set
			{
				this.var4 = value;
			}
		}
 
		[Description("Title of the PDF Document.")]
		public string Title
		{
			get
			{
				return this.var0;
			}
			set
			{
				this.var0 = value;
			}
		}
 
		[Description("User password used to encrypt the PDF file.")]
		public string UserPassword
		{
			get
			{
				return this.var5;
			}
			set
			{
				this.var5 = value;
			}
		}
 
		[Description("Determines Version of PDF output.")]
		public PDFVersion Version
		{
			get
			{
				return this._PDFVersion;
			}
			set
			{
				this._PDFVersion = value;
			}
		}

        public string Author
        {
            get
            {
                return this._Author;
            }
            set
            {
                this._Author = value;
            }
        }
        public string Creator
        {
            get
            {
                return this._Creator;
            }
            set
            {
                this._Creator = value;
            }
        }
	}
 
}
