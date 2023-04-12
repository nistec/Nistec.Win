using System;
using System.Collections.Generic;
using System.Text;

namespace MControl.Win
{

    public struct Currency
    {
        public string Symbol;
        public string Number;
        public string Decimal;
        public decimal Value;
    }

    public struct COLUMN
    {
        public int ColumnOrdinal;
        public string Caption;
    }
}
