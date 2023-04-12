namespace Nistec.Printing.View.Pdf
{
    using System;

    public class SecurityManager
    {
        private string _var0 = null;
        private string _var1 = null;
        private Nistec.Printing.View.Pdf.Encryption _var10 = Nistec.Printing.View.Pdf.Encryption.Use40BitKey;
        private bool _var2 = false;
        private bool _var3 = false;
        private bool _var4 = false;
        private bool _var5 = false;
        private bool _var6 = false;
        private bool _var7 = false;
        private bool _var8 = false;
        private bool _var9 = false;

        public bool AllowAccessibility
        {
            get
            {
                return this._var6;
            }
            set
            {
                this._var6 = value;
            }
        }

        public bool AllowCopy
        {
            get
            {
                return this._var2;
            }
            set
            {
                this._var2 = value;
            }
        }

        public bool AllowDocumentAssembly
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

        public bool AllowEdit
        {
            get
            {
                return this._var3;
            }
            set
            {
                this._var3 = value;
            }
        }

        public bool AllowFormFilling
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

        public bool AllowHighQualityPrinting
        {
            get
            {
                return this._var9;
            }
            set
            {
                this._var9 = value;
            }
        }

        public bool AllowPrint
        {
            get
            {
                return this._var4;
            }
            set
            {
                this._var4 = value;
            }
        }

        public bool AllowUpdateAnnotsAndFields
        {
            get
            {
                return this._var5;
            }
            set
            {
                this._var5 = value;
            }
        }

        public Nistec.Printing.View.Pdf.Encryption Encryption
        {
            get
            {
                return this._var10;
            }
            set
            {
                this._var10 = value;
            }
        }

        public string OwnerPassword
        {
            get
            {
                return this._var1;
            }
            set
            {
                this._var1 = value;
            }
        }

        public string UserPassword
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

