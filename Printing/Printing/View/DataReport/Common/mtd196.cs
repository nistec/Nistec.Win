namespace Nistec.Printing.View
{
    using System;
    using System.Data;

    internal class mtd196 : DataFields
    {
        private DataView _var0;

        internal mtd196(Report var1, CodeProvider var2, DataView var0)
            : base(var1, var2)
        {
            this._var0 = var0;
        }

        internal override void mtd172()
        {
            if (this._var0.Table != null)
            {
                DataColumnCollection columns = this._var0.Table.Columns;
                int num = 0;
                foreach (DataColumn column in columns)
                {
                    base._Fields.mtd2(column.ColumnName, num, Type.GetTypeCode(column.DataType), true);
                    num++;
                }
            }
        }

        internal override void mtd174()
        {
            if (!base._mtd175)
            {
                if ((base._mtd179 + 1) >= this._var0.Count)
                {
                    base._Report.EOF = true;
                }
                else
                {
                    base._mtd179++;
                    DataRowView view = this._var0[base._mtd179];
                    foreach (McField field in base._Fields)
                    {
                        field.mtd109();
                        if (field.mtd108)
                        {
                            field.mtd106 = view[field.Name];
                        }
                    }
                    base._Report.mtd117(Msg.DataFetch);
                    if (base._CodeProvider.mtd178)
                    {
                        object[] objArray = new object[] { base._Report, new EventArgs() };
                        base._CodeProvider.mtd71("Report", Methods._DataFetch, objArray);
                    }
                }
            }
            else
            {
                base._mtd175 = false;
                base._mtd179++;
            }
        }
    }
}

