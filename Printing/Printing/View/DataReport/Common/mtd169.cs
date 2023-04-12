namespace Nistec.Printing.View
{
    using System;

    internal class mtd169 : DataFields
    {
        private ExternalDataSource _var0;

        internal mtd169(Report var1, CodeProvider var2, ExternalDataSource var0)
            : base(var1, var2)
        {
            this._var0 = var0;
        }

        internal override void mtd172()
        {
            DataFieldSchemaList dataFieldSchemaList = this._var0.DataFieldSchemaList;
            DataFieldSchema schema = null;
            for (int i = 0; i < dataFieldSchemaList.Count; i++)
            {
                schema = dataFieldSchemaList[i];
                base._Fields.mtd2(schema.DataFieldName, -1, schema.TypeCode, false);
            }
        }

        internal override void mtd174()
        {
            if (!base._mtd175)
            {
                foreach (McField field in base._Fields)
                {
                    field.mtd109();
                }
                base._Report.mtd117(Msg.DataFetch);
                if (base._CodeProvider.mtd178)
                {
                    object[] objArray = new object[] { base._Report, new EventArgs() };
                    base._CodeProvider.mtd71("Report", Methods._DataFetch, objArray);
                }
                base._mtd179++;
            }
            else
            {
                base._mtd175 = false;
                base._mtd179++;
            }
        }
    }
}

