namespace MControl.Printing.Pdf
{
    using System;

    public class SecurityManager
    {
        private string _b0 = null;
        private string _b1 = null;
        private MControl.Printing.Pdf.Encryption _b10 = MControl.Printing.Pdf.Encryption.Use40BitKey;
        private bool _b2 = false;
        private bool _b3 = false;
        private bool _b4 = false;
        private bool _b5 = false;
        private bool _b6 = false;
        private bool _b7 = false;
        private bool _b8 = false;
        private bool _b9 = false;

        public bool AllowAccessibility
        {
            get
            {
                return this._b6;
            }
            set
            {
                this._b6 = value;
            }
        }

        public bool AllowCopy
        {
            get
            {
                return this._b2;
            }
            set
            {
                this._b2 = value;
            }
        }

        public bool AllowDocumentAssembly
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

        public bool AllowEdit
        {
            get
            {
                return this._b3;
            }
            set
            {
                this._b3 = value;
            }
        }

        public bool AllowFormFilling
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

        public bool AllowHighQualityPrinting
        {
            get
            {
                return this._b9;
            }
            set
            {
                this._b9 = value;
            }
        }

        public bool AllowPrint
        {
            get
            {
                return this._b4;
            }
            set
            {
                this._b4 = value;
            }
        }

        public bool AllowUpdateAnnotsAndFields
        {
            get
            {
                return this._b5;
            }
            set
            {
                this._b5 = value;
            }
        }

        public MControl.Printing.Pdf.Encryption Encryption
        {
            get
            {
                return this._b10;
            }
            set
            {
                this._b10 = value;
            }
        }

        public string OwnerPassword
        {
            get
            {
                return this._b1;
            }
            set
            {
                this._b1 = value;
            }
        }

        public string UserPassword
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

