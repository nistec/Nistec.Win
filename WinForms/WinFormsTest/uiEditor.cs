using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace WinCtlTest
{
    // Provides an example control that displays instructions in design mode,
    // with which the example UITypeEditor is associated.
    public class WinFormsEdServiceDialogExampleControl : UserControl
    {
        [EditorAttribute(typeof(TestDialogEditor), typeof(UITypeEditor))]
        public string TestDialogString
        {
            get
            {
                return localDialogTestString;
            }
            set
            {
                localDialogTestString = value;
            }
        }
        private string localDialogTestString;

        public WinFormsEdServiceDialogExampleControl()
        {
            localDialogTestString = "Test String";
            this.Size = new Size(210, 74);
            this.BackColor = Color.Beige;
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            if (this.DesignMode)
            {
                e.Graphics.DrawString("Use the Properties window to show", new Font("Arial", 8), new SolidBrush(Color.Black), 5, 5);
                e.Graphics.DrawString("a Form dialog box, using the", new Font("Arial", 8), new SolidBrush(Color.Black), 5, 17);
                e.Graphics.DrawString("IWindowsFormsEditorService, for", new Font("Arial", 8), new SolidBrush(Color.Black), 5, 29);
                e.Graphics.DrawString("configuring this control's", new Font("Arial", 8), new SolidBrush(Color.Black), 5, 41);
                e.Graphics.DrawString("TestDialogString property.", new Font("Arial", 8), new SolidBrush(Color.Black), 5, 53);
            }
            else
            {
                e.Graphics.DrawString("This example requires design mode.", new Font("Arial", 8), new SolidBrush(Color.Black), 5, 5);
            }
        }
    }

    // Example UITypeEditor that uses the IWindowsFormsEditorService 
    // to display a Form.
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class TestDialogEditor : System.Drawing.Design.UITypeEditor
    {
        public TestDialogEditor()
        {
        }

        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            // Indicates that this editor can display a Form-based interface.
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            // Attempts to obtain an IWindowsFormsEditorService.
            IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (edSvc == null)
                return null;

            // Displays a StringInputDialog Form to get a user-adjustable 
            // string value.
            StringInputDialog form = new StringInputDialog((string)value);
            if (edSvc.ShowDialog(form) == DialogResult.OK)
                return form.inputTextBox.Text;

            // If OK was not pressed, return the original value
            return value;
        }
    }

}
