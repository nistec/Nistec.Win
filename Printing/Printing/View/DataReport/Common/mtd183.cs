namespace Nistec.Printing.View
{
    using System;

    internal class mtd183 : DataFields
    {
        private XmlDataSource _var0;

        internal mtd183(Report var1, CodeProvider var2, XmlDataSource var3)
            : base(var1, var2)
        {
            this._var0 = var3;
        }

        internal override void mtd172()
        {
            this._var0.mtd172();
            DataFieldSchemaList dataFieldSchemaList = this._var0.DataFieldSchemaList;
            for (int i = 0; i < dataFieldSchemaList.Count; i++)
            {
                DataFieldSchema schema = dataFieldSchemaList[i];
                base._Fields.mtd2(schema.DataFieldName, -1, schema.TypeCode, true);
            }
        }

        internal override void mtd174()
        {
            if (!base._mtd175)
            {
                if (!this._var0.mtd184(base._mtd179 + 1))
                {
                    base._Report.EOF = true;
                }
                else
                {
                    base._mtd179++;
                    foreach (McField field in base._Fields)
                    {
                        field.mtd109();
                        if (field.mtd108)
                        {
                            field.mtd106 = this._var0.mtd185(field.Name);
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

        internal override bool mtd186(object var4)
        {
            return ((this._var0 != null) && (this._var0 == var4));
        }
    }
}

