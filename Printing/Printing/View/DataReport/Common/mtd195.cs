namespace Nistec.Printing.View
{
    using System;
    using System.Data;

    internal class mtd195 : DataFields
    {
        private DataTable _var0;

        internal mtd195(Report var1, CodeProvider var2, DataSet var3)
            : base(var1, var2)
        {
            this._var0 = var3.Tables[0];
        }

        internal mtd195(Report var1, CodeProvider var2, DataTable var0)
            : base(var1, var2)
        {
            this._var0 = var0;
        }

        internal override void mtd172()
        {
            DataColumnCollection columns = this._var0.Columns;
            int num = 0;
            foreach (DataColumn column in columns)
            {
                base._Fields.mtd2(column.ColumnName, num, Type.GetTypeCode(column.DataType), true);
                num++;
            }
        }

        internal override void mtd174()
        {
            if (!base._mtd175)
            {
                DataRowCollection rows = this._var0.Rows;
                if ((base._mtd179 + 1) >= rows.Count)
                {
                    base._Report.EOF = true;
                }
                else
                {
                    base._mtd179++;
                    DataRow row = rows[base._mtd179];
                    foreach (McField field in base._Fields)
                    {
                        field.mtd109();
                        if (field.ColIndex != -1)
                        {
                            field.mtd106 = row[field.ColIndex];
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

