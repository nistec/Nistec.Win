namespace Nistec.Printing.Data
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml;

    public abstract class AdoProperties
    {
        public const string DEFAULT_DATE_FORMAT = "dd/MM/yy HH:mm:ss";

        public event EventHandler Changed;

        protected AdoProperties()
        {
        }

        public abstract AdoProperties Clone();
 
        protected void OnChanged()
        {
            if (this.Changed != null)
            {
                this.Changed(this, new EventArgs());
            }
        }

        internal protected AdoTable _DataSource;
        internal protected string _dateFormat = "dd/MM/yy HH:mm:ss";
        protected string _encoding = "UTF-8";
        protected string _filename = "";
        protected bool _firstRowHeaders = false;


 
        //AdoField[] _fields;

        //public AdoField[] Fields
        //{
        //    get { return _fields; }
        //    set { _fields = value; }
        //}

   
    }
}

