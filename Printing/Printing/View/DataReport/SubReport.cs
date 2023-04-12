namespace Nistec.Printing.View
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    [ToolboxItemFilter("Nistec.Printing.View", ToolboxItemFilterType.Require)]
    public class McSubReport : McReportControl
    {
        private bool _var0;
        private bool _var1;
        private Report _var2;

        public McSubReport()
        {
            this.var3();
        }

        public McSubReport(string name)
        {
            base._mtd91 = name;
            this.var3();
        }

        internal void mtd22(Graphics var5, RectangleF var6)
        {
            this.var4(var5, var6);
        }

        public override void DrawCtl(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            base.DrawCtl(sender, e);
            this.var4(graphics, this.Bounds);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._var2 = null;
            }
            base.Dispose(disposing);
        }

        private void var3()
        {
            base._mtd66 = ControlType.SubReport;
            this._var0 = true;
            this._var1 = false;
        }

        private void var4(Graphics var5, RectangleF var6)
        {
            this.Border.Render(var5, var6);
        }

        [Category("Appearance"), Description("Indicates whether or not the control can grow to accomodate text"), DefaultValue(true), mtd85(mtd88.mtd86)]
        public bool CanGrow
        {
            get
            {
                return this._var0;
            }
            set
            {
                this._var0 = value;
            }
        }

        [DefaultValue(false), Description("Indicates whether or not the control can shrink to accomodate text"), Category("Appearance"), mtd85(mtd88.mtd86)]
        public bool CanShrink
        {
            get
            {
                return this._var1;
            }
            set
            {
                this._var1 = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public Report Report
        {
            get
            {
                return this._var2;
            }
            set
            {
                this._var2 = value;
            }
        }
    }
}

