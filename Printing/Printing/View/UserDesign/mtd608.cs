namespace MControl.Printing.View.Design.UserDesigner
{
    using MControl.Printing.View;
    using MControl.Printing.View.Design;
    using System;
    using System.Collections;
    using System.ComponentModel.Design;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Drawing.Drawing2D;
    using System.Reflection;
    using System.Windows.Forms;

    internal class mtd608 : Panel, IToolboxService
    {
        private ArrayList _var0 = new ArrayList();
        private int _var1 = -1;
        private ImageList _var13;
        private Rectangle _var14;
        internal Control mtd609;
        private CtlInternal var15;
        private CtlInternal var16;
        private CtlInternal var17;
        private CtlInternal var18;
        private CtlInternal var19;
        private CtlInternal var20;
        private CtlInternal var21;
        private CtlInternal var22;
        private CtlInternal var23;
        private CtlInternal var24;

        internal mtd608()
        {
            this.var2();
        }

        public void AddCreator(ToolboxItemCreatorCallback var7, string var8, IDesignerHost var5)
        {
        }

        public void AddLinkedToolboxItem(ToolboxItem var3, string var4, IDesignerHost var5)
        {
        }

        public void AddToolboxItem(ToolboxItem var3, string var4)
        {
            this._var0.Add(var3);
        }

        public ToolboxItem DeserializeToolboxItem(object var6, IDesignerHost var5)
        {
            return null;
        }

        public ToolboxItem GetSelectedToolboxItem(IDesignerHost var5)
        {
            if (this._var1 == -1)
            {
                return null;
            }
            return (ToolboxItem) this._var0[this._var1];
        }

        public ToolboxItemCollection GetToolboxItems(string var4, IDesignerHost var5)
        {
            return this.var9();
        }

        public bool IsSupported(object var6, ICollection var10)
        {
            return false;
        }

        public bool IsToolboxItem(object var6, IDesignerHost var5)
        {
            return false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Matrix transform = graphics.Transform;
            using (SolidBrush brush = new SolidBrush(Color.Gainsboro))
            {
                graphics.FillRectangle(brush, this._var14);
            }
            graphics.DrawRectangle(SystemPens.GrayText, this._var14);
            graphics.DrawLine(SystemPens.GrayText, 0, 80, this._var14.Right, 80);
            graphics.DrawImage(this._var13.Images[10], new Point(8, 8));
            graphics.TranslateTransform(20f, 25f);
            graphics.RotateTransform(90f);
            graphics.DrawString("ToolBox", this.Font, Brushes.Black, (float) 0f, (float) 0f);
            graphics.Transform = transform;
            base.OnPaint(e);
        }

        public void RemoveCreator(string var8, IDesignerHost var5)
        {
        }

        public void RemoveToolboxItem(ToolboxItem var3, string var4)
        {
            this._var0.Remove(var3);
        }

        public void SelectedToolboxItemUsed()
        {
            this._var1 = -1;
            this.var11((CtlInternal) base.Controls[0]);
        }

        public object SerializeToolboxItem(ToolboxItem var3)
        {
            return null;
        }

        public bool SetCursor()
        {
            if (this._var1 == -1)
            {
                if (this.mtd609.Cursor != Cursors.Default)
                {
                    this.mtd609.Cursor = Cursors.Default;
                }
            }
            else if (this.mtd609.Cursor != Cursors.Cross)
            {
                this.mtd609.Cursor = Cursors.Cross;
            }
            return true;
        }

        public void SetSelectedToolboxItem(ToolboxItem var3)
        {
            int num = 0;
            foreach (object obj2 in this._var0)
            {
                if (((ToolboxItem) obj2) == var3)
                {
                    this._var1 = num;
                    break;
                }
                num++;
            }
        }

        public void SetSelectedToolboxItem(int var12)
        {
            this._var1 = var12;
        }

        private void var11(CtlInternal var27)
        {
            base.SuspendLayout();
            foreach (CtlInternal mtd in base.Controls)
            {
                if (mtd == var27)
                {
                    mtd.mtd7 = true;
                }
                else
                {
                    mtd.mtd7 = false;
                }
                mtd.Invalidate();
            }
            base.ResumeLayout(false);
        }

        private void var2()
        {
            this._var14 = new Rectangle(0, 3, 0x1d, 0x144);
            this._var13 = mtd73.GetImageList(Assembly.GetAssembly(typeof(MControl.Printing.View.Design.Report)), "MControl.Printing.View.Resources.ImagesToolbox.bmp", new Size(0x10, 0x10), new Point(0, 0));
            base.SuspendLayout();
            this.var15 = new CtlInternal("Pointer", "Pointer", Color.Gainsboro);
            this.var15.mtd7 = true;
            this.var15.mtd8 = FlatStyle.System;
            this.var15.Location = new Point(3, 0x54);
            this.var15.mtd9 = this._var13.Images[0];
            this.var15.Tag = -1;
            this.var15.MouseDown += new MouseEventHandler(this.var25);
            this.var16 = new CtlInternal("Label", "Label", Color.Gainsboro);
            this.var16.mtd8 = FlatStyle.System;
            this.var16.Location = new Point(3, 0x6c);
            this.var16.mtd9 = this._var13.Images[1];
            this.var16.Tag = 0;
            this.var16.MouseDown += new MouseEventHandler(this.var25);
            this.var17 = new CtlInternal("Textbox", "TextBox", Color.Red);
            this.var17.mtd8 = FlatStyle.System;
            this.var17.Location = new Point(3, 0x84);
            this.var17.mtd9 = this._var13.Images[2];
            this.var17.Tag = 1;
            this.var17.MouseDown += new MouseEventHandler(this.var25);
            this.var18 = new CtlInternal("CheckBox", "CheckBox", Color.Gainsboro);
            this.var18.mtd8 = FlatStyle.System;
            this.var18.Location = new Point(3, 0x9c);
            this.var18.mtd9 = this._var13.Images[3];
            this.var18.Tag = 2;
            this.var18.MouseDown += new MouseEventHandler(this.var25);
            this.var19 = new CtlInternal("Picture", "Picture", Color.Gainsboro);
            this.var19.mtd8 = FlatStyle.System;
            this.var19.Location = new Point(3, 180);
            this.var19.mtd9 = this._var13.Images[4];
            this.var19.Tag = 3;
            this.var19.MouseDown += new MouseEventHandler(this.var25);
            this.var20 = new CtlInternal("Shape", "Shape", Color.Gainsboro);
            this.var20.mtd8 = FlatStyle.System;
            this.var20.Location = new Point(3, 0xcc);
            this.var20.mtd9 = this._var13.Images[5];
            this.var20.Tag = 4;
            this.var20.MouseDown += new MouseEventHandler(this.var25);
            this.var21 = new CtlInternal("Line", "Line", Color.Gainsboro);
            this.var21.mtd8 = FlatStyle.System;
            this.var21.Location = new Point(3, 0xe4);
            this.var21.mtd9 = this._var13.Images[6];
            this.var21.Tag = 5;
            this.var21.MouseDown += new MouseEventHandler(this.var25);
            this.var22 = new CtlInternal("RichTextBox", "RichTextBox", Color.Gainsboro);
            this.var22.mtd8 = FlatStyle.System;
            this.var22.Location = new Point(3, 0xfc);
            this.var22.mtd9 = this._var13.Images[7];
            this.var22.Tag = 6;
            this.var22.MouseDown += new MouseEventHandler(this.var25);
            this.var23 = new CtlInternal("SubReport", "SubReport", Color.Gainsboro);
            this.var23.mtd8 = FlatStyle.System;
            this.var23.Location = new Point(3, 0x114);
            this.var23.mtd9 = this._var13.Images[8];
            this.var23.Tag = 7;
            this.var23.MouseDown += new MouseEventHandler(this.var25);
            this.var24 = new CtlInternal("PageBreak", "PageBreak", Color.Gainsboro);
            this.var24.mtd8 = FlatStyle.System;
            this.var24.Location = new Point(3, 300);
            this.var24.mtd9 = this._var13.Images[9];
            this.var24.Tag = 8;
            this.var24.MouseDown += new MouseEventHandler(this.var25);
            this.BackColor = Color.WhiteSmoke;
            this.Dock = DockStyle.Left;
            base.Name = "pnlTool";
            base.Size = new Size(0x21, 0x264);
            this.BackColor = Color.FromArgb(0xf7, 0xf3, 0xe9);
            base.Controls.AddRange(new Control[] { this.var15, this.var16, this.var17, this.var18, this.var19, this.var20, this.var21, this.var22, this.var23, this.var24 });
            base.ResumeLayout(false);
        }

        private void var25(object var26, MouseEventArgs e)
        {
            CtlInternal mtd = (CtlInternal) var26;
            this.var11(mtd);
            if (((int) mtd.Tag) != -1)
            {
                this.SetSelectedToolboxItem((int) mtd.Tag);
            }
            else if (((int) mtd.Tag) == -1)
            {
                this.SelectedToolboxItemUsed();
            }
        }

        private ToolboxItemCollection var9()
        {
            ToolboxItem[] array = new ToolboxItem[this._var0.Count];
            this._var0.CopyTo(array, 0);
            return new ToolboxItemCollection(array);
        }

        void IToolboxService.AddCreator(ToolboxItemCreatorCallback var7, string var8)
        {
        }

        void IToolboxService.AddLinkedToolboxItem(ToolboxItem var3, IDesignerHost var5)
        {
        }

        void IToolboxService.AddToolboxItem(ToolboxItem var3)
        {
            this._var0.Add(var3);
        }

        ToolboxItem IToolboxService.DeserializeToolboxItem(object var6)
        {
            return null;
        }

        ToolboxItem IToolboxService.GetSelectedToolboxItem()
        {
            if (this._var1 == -1)
            {
                return null;
            }
            return (ToolboxItem) this._var0[this._var1];
        }

        ToolboxItemCollection IToolboxService.GetToolboxItems()
        {
            return this.var9();
        }

        ToolboxItemCollection IToolboxService.GetToolboxItems(IDesignerHost var5)
        {
            return this.var9();
        }

        ToolboxItemCollection IToolboxService.GetToolboxItems(string var4)
        {
            return this.var9();
        }

        bool IToolboxService.IsSupported(object var6, IDesignerHost var5)
        {
            return false;
        }

        bool IToolboxService.IsToolboxItem(object var6)
        {
            return false;
        }

        void IToolboxService.RemoveCreator(string var8)
        {
        }

        void IToolboxService.RemoveToolboxItem(ToolboxItem var3)
        {
            this._var0.Remove(var3);
        }

        public CategoryNameCollection CategoryNames
        {
            get
            {
                return null;
            }
        }

        public string SelectedCategory
        {
            get
            {
                return "ReportView";
            }
            set
            {
            }
        }
    }
}

