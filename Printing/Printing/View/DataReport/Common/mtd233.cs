namespace Nistec.Printing.View
{
    using System;
    using System.Collections;
    using System.Windows.Forms;

    internal class mtd233
    {
        internal McField[] Fields;
        internal McRichText mtd64;

        internal mtd233(ref McRichText var0)
        {
            this.mtd64 = var0;
        }

        internal mtd233(ref McRichText var0, ref FieldsCollection var1)
        {
            this.mtd64 = var0;
            this.var2(ref var1);
        }

        private void var2(ref FieldsCollection var1)
        {
            ArrayList list = new ArrayList();
            if ((this.mtd64.DataBound && (this.mtd64.RTF != null)) && (this.mtd64.RTF.Length > 0))
            {
                foreach (McField field in var1)
                {
                    string text = string.Format("[!{0}]", field.Name);
                    if (this.mtd64.Find(text, RichTextBoxFinds.None) != -1)
                    {
                        list.Add(field);
                    }
                }
                if (list.Count > 0)
                {
                    this.Fields = new McField[list.Count];
                    list.CopyTo(this.Fields);
                }
            }
        }
    }
}

