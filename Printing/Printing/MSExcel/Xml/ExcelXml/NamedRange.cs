namespace Nistec.Printing.ExcelXml
{
    using System;

    internal class NamedRange
    {
        internal string Name;
        internal Nistec.Printing.ExcelXml.Range Range;
        internal Nistec.Printing.ExcelXml.Worksheet Worksheet;

        internal NamedRange(Nistec.Printing.ExcelXml.Range range, string name, Nistec.Printing.ExcelXml.Worksheet ws)
        {
            if (range == null)
            {
                throw new ArgumentNullException("range");
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }
            this.Worksheet = ws;
            this.Range = range;
            this.Name = name;
        }
    }
}

