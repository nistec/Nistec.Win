using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Nistec.SyntaxEditor
{
    public class IntelliWindow : Nistec.WinForms.WindowList
    {
        private ImageList imageList1;
        private System.ComponentModel.IContainer components;

        internal IntelliWindow()
        {

        }
        public IntelliWindow(Control parent)
            : base(parent)
        {
            InitializeComponent();
            base.ImageList = this.imageList1;
            base.ItemHeight = 15;
        }
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IntelliWindow));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "method.gif");
            this.imageList1.Images.SetKeyName(1, "Properties.png");
            this.imageList1.Images.SetKeyName(2, "field.gif");
            this.imageList1.Images.SetKeyName(3, "member.gif");
            this.imageList1.Images.SetKeyName(4, "class.gif");
            this.imageList1.Images.SetKeyName(5, "event.gif");
            this.imageList1.Images.SetKeyName(6, "reference.gif");
            this.imageList1.Images.SetKeyName(7, "field1.gif");
            this.imageList1.Images.SetKeyName(8, "methodBlue.gif");
            this.imageList1.Images.SetKeyName(9, "method2.gif");
            this.imageList1.Images.SetKeyName(10, "properties.gif");
            // 
            // IntelliWindow
            // 
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Name = "IntelliWindow";
            this.ResumeLayout(false);

        }

  
        

    }
}
