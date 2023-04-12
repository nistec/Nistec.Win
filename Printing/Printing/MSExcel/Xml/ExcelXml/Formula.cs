namespace Nistec.Printing.ExcelXml
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    public class Formula
    {
        [CompilerGenerated]
        private bool _MustHaveParameters;
        internal string Function;
        internal List<Parameter> parameters;

        private Nistec.Printing.ExcelXml.ContentType Content;
 
        public Nistec.Printing.ExcelXml.ContentType ContentType
        {
            get
            {
                return this.Content;
            }
        }

        private object _value;

        public object Value
        {
            get { return _value; }
            set 
            { 
                _value = value;

                switch (value.GetType().FullName)
                {
                    case "System.DateTime":
                        this.Content = Nistec.Printing.ExcelXml.ContentType.DateTime;
                        break;

                    case "System.Byte":
                    case "System.SByte":
                    case "System.Int16":
                    case "System.Int32":
                    case "System.Int64":
                    case "System.UInt16":
                    case "System.UInt32":
                    case "System.UInt64":
                    case "System.Single":
                    case "System.Double":
                    case "System.Decimal":
                        this.Content = Nistec.Printing.ExcelXml.ContentType.Number;
                        break;

                    case "System.Boolean":
                        this.Content = Nistec.Printing.ExcelXml.ContentType.Boolean;
                        break;

                    case "System.String":
                        this.Content = Nistec.Printing.ExcelXml.ContentType.String;
                        break;
  
                    default:
                        this.Content = Nistec.Printing.ExcelXml.ContentType.String;
                        break;
                }

            }
        }

        internal Formula()
        {
            this.parameters = new List<Parameter>();
        }

        public Formula(string function)
        {
            this.Initialize(function);
        }

        public Formula(string function, string parameter)
        {
            this.Initialize(function);
            this.Add(parameter);
        }

        public Formula(string function, Formula formula)
        {
            this.Initialize(function);
            this.Add(formula);
        }

        public Formula(string function, Range range)
        {
            this.Initialize(function);
            this.Add(range);
        }

        public Formula(string function, Range range, Predicate<Cell> cellCompare)
        {
            this.Initialize(function);
            this.Add(range, cellCompare);
        }

        private void Initialize(string function)
        {
            this.parameters = new List<Parameter>();
            this.Function = function;
        }

        public void Add(string parameter)
        {
            Parameter item = new Parameter(parameter);
            this.Parameters.Add(item);
        }

        public void Add(Formula formula)
        {
            Parameter item = new Parameter(formula);
            this.Parameters.Add(item);
        }

        public void Add(Range range)
        {
            Parameter item = new Parameter(range);
            this.Parameters.Add(item);
        }

        public void Add(Range range, Predicate<Cell> cellCompare)
        {
            if (range.CellFrom != null)
            {
                if (range.CellTo == null)
                {
                    if (cellCompare(range.CellFrom))
                    {
                        this.Add(range);
                    }
                }
                else
                {
                    Worksheet parentSheet = range.CellFrom.ParentRow.ParentSheet;
                    int rowIndex = range.CellFrom.ParentRow.RowIndex;
                    int rowIndexTo = range.CellTo.ParentRow.RowIndex;
                    int cellIndex = range.CellFrom.CellIndex;
                    int cellIndexTo = range.CellTo.CellIndex;
                    for (int i = cellIndex; i <= cellIndexTo; i++)
                    {
                        int row = rowIndex;
                        do
                        {
                            if (cellCompare(parentSheet[row,i]))
                            {
                                for (int j = row + 1; j <= rowIndexTo; j++)
                                {
                                    if (!cellCompare(parentSheet[j,i]))
                                    {
                                        this.Add(new Range(parentSheet[row,i], parentSheet[j - 1,i]));
                                        row = j;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                row++;
                            }
                        }
                        while (row <= rowIndexTo);
                    }
                }
            }
        }

 

        internal string ToString(Cell cell)
        {
            Range range;
            if (string.IsNullOrEmpty(this.Function))
            {
                range = this.Parameters[0].Value as Range;
                if (range != null)
                {
                    return range.RangeReference(cell);
                }
                return "";
            }
            string text = "";
            foreach (Parameter parameter in this.Parameters)
            {
                if (parameter.ParameterType == ParameterType.Range)
                {
                    range = parameter.Value as Range;
                    if (parameter != null)
                    {
                        text = text + range.RangeReference(cell) + ",";
                    }
                }
                else if (parameter.ParameterType == ParameterType.Formula)
                {
                    Formula formula = parameter.Value as Formula;
                    if (parameter != null)
                    {
                        text = text + formula.ToString(cell) + ",";
                    }
                }
                else
                {
                    text = text + parameter.Value.ToString() + ",";
                }
            }
            if (this.MustHaveParameters && ((this.Parameters.Count == 0) || string.IsNullOrEmpty(text)))
            {
                return "";
            }
            if (!(string.IsNullOrEmpty(text) || (text[text.Length - 1] != ',')))
            {
                text = text.Substring(0,text.Length - 1);
            }
            return (this.Function.ToUpper(CultureInfo.InvariantCulture) + "(" + text + ")");
        }

        public bool MustHaveParameters
        {
            [CompilerGenerated]
            get
            {
                return this._MustHaveParameters;
            }
            [CompilerGenerated]
            set
            {
                this._MustHaveParameters = value;
            }
        }

        public IList<Parameter> Parameters
        {
            get
            {
                return this.parameters;
            }
        }
    }
}

