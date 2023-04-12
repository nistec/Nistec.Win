namespace Nistec.Printing.View
{
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;

    internal class mtd80 : UITypeEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (((context != null) && (context.Instance != null)) && (provider != null))
            {
                OutputFormatDlg format = new OutputFormatDlg();
                format.ShowDialog();
                if (format.mtd4)
                {
                    return format.mtd3;
                }
            }
            return value;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            if ((context != null) && (context.Instance != null))
            {
                return UITypeEditorEditStyle.Modal;
            }
            return base.GetEditStyle(context);
        }
    }
}

