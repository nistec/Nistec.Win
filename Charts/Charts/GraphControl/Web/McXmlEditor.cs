namespace Nistec.Charts.Web
{
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Windows.Forms;
    using System.Windows.Forms.Design;

    internal class McXmlEditor : UITypeEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService service = (IWindowsFormsEditorService) provider.GetService(typeof(IWindowsFormsEditorService));
            XmlEditorForm dialog = new XmlEditorForm();
            dialog.richTextBox1.Text = (string) value;
            if (service.ShowDialog(dialog) == DialogResult.OK)
            {
                value = dialog.richTextBox1.Text;
            }
            return value;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }
}

