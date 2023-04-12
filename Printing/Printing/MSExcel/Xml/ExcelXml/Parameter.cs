namespace Nistec.Printing.ExcelXml
{
    using System;
    using System.Runtime.CompilerServices;

    public class Parameter
    {
        [CompilerGenerated]
        private Nistec.Printing.ExcelXml.ParameterType _ParameterType;
        [CompilerGenerated]
        private object _Value;

        internal Parameter(string p)
        {
            this.ParameterType = Nistec.Printing.ExcelXml.ParameterType.String;
            this.Value = p;
        }

        internal Parameter(Formula p)
        {
            this.ParameterType = Nistec.Printing.ExcelXml.ParameterType.Formula;
            this.Value = p;
        }

        internal Parameter(Range p)
        {
            this.ParameterType = Nistec.Printing.ExcelXml.ParameterType.Range;
            this.Value = p;
        }

        public Nistec.Printing.ExcelXml.ParameterType ParameterType
        {
            [CompilerGenerated]
            get
            {
                return this._ParameterType;
            }
            private //[CompilerGenerated]
            set
            {
                this._ParameterType = value;
            }
        }

        public object Value
        {
            [CompilerGenerated]
            get
            {
                return this._Value;
            }
            private //[CompilerGenerated]
            set
            {
                this._Value = value;
            }
        }
    }
}

