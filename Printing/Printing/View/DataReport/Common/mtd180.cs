namespace Nistec.Printing.View
{
    using System;
    using System.Collections;
    using System.Reflection;

    internal class mtd180 : DataFields
    {
        private object _var0;
        private IListDataSource _var1;

        internal mtd180(Report var2, CodeProvider var3, IListDataSource var1)
            : base(var2, var3)
        {
            this._var1 = var1;
            this._var0 = null;
        }

        internal override void mtd172()
        {
            DataFieldSchemaList dataFieldSchemaList = this._var1.DataFieldSchemaList;
            for (int i = 0; i < dataFieldSchemaList.Count; i++)
            {
                DataFieldSchema schema = dataFieldSchemaList[i];
                base._Fields.mtd2(schema.DataFieldName, -1, schema.TypeCode, true);
            }
        }

        internal override void mtd174()
        {
            IList list = this._var1.List;
            if (!base._mtd175)
            {
                if ((base._mtd179 + 1) >= list.Count)
                {
                    base._Report.EOF = true;
                }
                else
                {
                    base._mtd179++;
                    this._var0 = list[base._mtd179];
                    foreach (McField field in base._Fields)
                    {
                        field.mtd109();
                        if (field.mtd108)
                        {
                            field.mtd106 = this.mtd181(field.Name);
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

        internal object mtd181(string var4)
        {
            string[] strArray = var4.Split(new char[] { '.' });
            if (strArray.Length == 1)
            {
                return this.var5(null, strArray[0], -1);
            }
            object obj2 = null;
            int num = 0;
            for (int i = 0; i < strArray.Length; i++)
            {
                obj2 = this.var5(obj2, strArray[i], -1);
                num++;
                if ((obj2 != null) && (num == (strArray.Length - 1)))
                {
                    break;
                }
            }
            string str = strArray[strArray.Length - 1];
            if (str == null)
            {
                return null;
            }
            if (obj2 == null)
            {
                return null;
            }
            return this.var5(obj2, str, -1);
        }

        private object var5(object var6, string var7, int var8)
        {
            if (this._var0 != null)
            {
                object obj2 = null;
                if (var6 == null)
                {
                    obj2 = this._var0;
                }
                else
                {
                    obj2 = var6;
                }
                PropertyInfo[] properties = obj2.GetType().GetProperties();
                PropertyInfo info = null;
                int length = properties.Length;
                for (int i = 0; i < length; i++)
                {
                    info = properties[i];
                    if (info == null)
                    {
                        return null;
                    }
                    try
                    {
                        if (var8 == -1)
                        {
                            if (info.Name == var7)
                            {
                                return info.GetValue(obj2, null);
                            }
                        }
                        else if (var8 == i)
                        {
                            return info.GetValue(obj2, null);
                        }
                    }
                    catch (TargetParameterCountException)
                    {
                        return null;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
            }
            return null;
        }
    }
}

