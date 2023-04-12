namespace Nistec.Printing.View
{
    using System;
    using System.Data;

    internal class mtd197 : DataFields
    {
        private IDataReader _var0;

        internal mtd197(Report var1, CodeProvider var2, IDataReader var0)
            : base(var1, var2)
        {
            this._var0 = var0;
        }

        internal override void mtd111()
        {
            base.mtd111();
            if ((this._var0 != null) && !this._var0.IsClosed)
            {
                this._var0.Close();
            }
        }

        internal override void mtd172()
        {
            DataRowCollection rows = this._var0.GetSchemaTable().Rows;
            int num = 0;
            foreach (DataRow row in rows)
            {
                base._Fields.mtd2((string)row["ColumnName"], num, Type.GetTypeCode((Type)row["DataType"]), true);
                num++;
            }
        }

        internal override void mtd174()
        {
            if (!base._mtd175)
            {
                if (!this._var0.Read())
                {
                    base._Report.EOF = true;
                }
                else
                {
                    base._mtd179++;
                    foreach (McField field in base._Fields)
                    {
                        field.mtd109();
                        if (field.ColIndex != -1)
                        {
                            field.mtd106 = this._var0[field.ColIndex];
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

