namespace Nistec.Printing.View
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Data;
    using System.Drawing.Design;
    using System.Windows.Forms;
    using System.Windows.Forms.Design;

    internal class mtd79 : UITypeEditor
    {
        private IWindowsFormsEditorService _var0;

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            object obj2;
            McReportControl instance = (McReportControl) context.Instance;
            if (((context == null) || (context.Instance == null)) || (provider == null))
            {
                return value;
            }
            ListBox box = new ListBox();
            box.MouseDown += new MouseEventHandler(this.var1);
            box.BorderStyle = BorderStyle.None;
            try
            {
                this._var0 = (IWindowsFormsEditorService) provider.GetService(typeof(IWindowsFormsEditorService));
                IDesignerHost service = (IDesignerHost) instance.Site.GetService(typeof(IDesignerHost));
                if ((this._var0 == null) || (service == null))
                {
                    return value;
                }
                Report rootComponent = (Report) service.RootComponent;
                if (rootComponent == null)
                {
                    return value;
                }
                string[] strArray = this.var2(rootComponent);
                if (strArray == null)
                {
                    return value;
                }
                this.var3(box, strArray);
                box.SelectedItem = value;
                this._var0.DropDownControl(box);
                if (box.SelectedItem != null)
                {
                    return box.SelectedItem;
                }
                obj2 = value;
            }
            finally
            {
                box.Dispose();
                box = null;
                this._var0 = null;
            }
            return obj2;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            if ((context != null) && (context.Instance != null))
            {
                return UITypeEditorEditStyle.DropDown;
            }
            return base.GetEditStyle(context);
        }

        private void var1(object var4, MouseEventArgs var5)
        {
            if (this._var0 != null)
            {
                this._var0.CloseDropDown();
            }
        }

        private string[] var2(Report var8)
        {
            try
            {
                if (var8.DataSource != null)
                {
                    if (var8.DataSource is DataSet)
                    {
                        DataSet dataSource = (DataSet) var8.DataSource;
                        if (dataSource.Tables.Count > 0)
                        {
                            return this.var9(dataSource.Tables[0].Columns);
                        }
                    }
                    else
                    {
                        if (var8.DataSource is DataTable)
                        {
                            return this.var9(((DataTable) var8.DataSource).Columns);
                        }
                        if (var8.DataSource is DataView)
                        {
                            DataView view = (DataView) var8.DataSource;
                            return this.var9(view.Table.Columns);
                        }
                        if (var8.DataSource is IDataReader)
                        {
                            return this.var9((IDataReader) var8.DataSource);
                        }
                        if (var8.DataSource is IListDataSource)
                        {
                            return this.var9(((IListDataSource) var8.DataSource).DataFieldSchemaList);
                        }
                        if (var8.DataSource is XmlDataSource)
                        {
                            return this.var9(((XmlDataSource) var8.DataSource).DataFieldSchemaList);
                        }
                        if (var8.DataSource is ExternalDataSource)
                        {
                            return this.var9(((ExternalDataSource) var8.DataSource).DataFieldSchemaList);
                        }
                    }
                    goto Label_019C;
                }
                if (var8.DataAdapter != null)
                {
                    IDataAdapter dataAdapter = var8.DataAdapter;
                    DataSet dataSet = new DataSet();
                    dataAdapter.FillSchema(dataSet, SchemaType.Mapped);
                    if (dataSet.Tables.Count > 0)
                    {
                        return this.var9(dataSet.Tables[0].Columns);
                    }
                    goto Label_019C;
                }
            }
            catch
            {
            }
            return null;
        Label_019C:
            return null;
        }

        private void var3(ListBox var6, string[] var7)
        {
            if ((var7 != null) && (var7.Length > 0))
            {
                var6.Items.Clear();
                foreach (string str in var7)
                {
                    var6.Items.Add(str);
                }
            }
        }

        private string[] var9(DataFieldSchemaList fieldlist)
        {
            if ((fieldlist == null) || (fieldlist.Count <= 0))
            {
                return null;
            }
            int count = fieldlist.Count;
            string[] strArray = new string[count];
            for (int i = 0; i < count; i++)
            {
                strArray[i] = fieldlist[i].DataFieldName;
            }
            return strArray;
        }

        private string[] var9(DataColumnCollection columns)
        {
            if ((columns == null) || (columns.Count <= 0))
            {
                return null;
            }
            int count = columns.Count;
            string[] strArray = new string[count];
            for (int i = 0; i < count; i++)
            {
                strArray[i] = columns[i].ColumnName;
            }
            return strArray;
        }

        private string[] var9(IDataReader reader)
        {
            if ((reader == null) || (reader.FieldCount <= 0))
            {
                return null;
            }
            int fieldCount = reader.FieldCount;
            string[] strArray = new string[fieldCount];
            for (int i = 0; i < fieldCount; i++)
            {
                strArray[i] = reader.GetName(i);
            }
            return strArray;
        }
    }
}

