namespace Nistec.Printing.View.Design
{
    using Nistec.Printing.View;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.ComponentModel.Design.Serialization;
    using System.Drawing.Printing;

    ////[Designer(typeof(ReportViewDesigner), typeof(IRootDesigner)), RootDesignerSerializer(typeof(UICodeDomSerializer), typeof(CodeDomSerializer), true), ToolboxItemFilter("Nistec.Printing.View", ToolboxItemFilterType.Prevent)]
    //[Designer(typeof(ReportViewDesigner), typeof(IRootDesigner)), DesignerSerializer(typeof(UICodeDomSerializer), typeof(CodeDomSerializer)), ToolboxItemFilter("Nistec.Printing.View", ToolboxItemFilterType.Prevent)]
    public class ReportDesign : Nistec.Printing.View.Report
    {
        public bool ShouldSerializeCollate()
        {
            return (this.Collate != PrinterCollate.Default);
        }

        public bool ShouldSerializeDuplex()
        {
            return (this.Duplex != PrinterDuplex.Default);
        }

        public bool ShouldSerializeMarginBottom()
        {
            return (this.MarginBottom != 1f);
        }

        public bool ShouldSerializeMarginLeft()
        {
            return (this.MarginLeft != 1f);
        }

        public bool ShouldSerializeMarginRight()
        {
            return (this.MarginRight != 1f);
        }

        public bool ShouldSerializeMarginTop()
        {
            return (this.MarginTop != 1f);
        }

        public bool ShouldSerializeOrientation()
        {
            return (this.Orientation != PageOrientation.Default);
        }

        public bool ShouldSerializePaperHeight()
        {
            return (this.PaperHeight != 0f);
        }

        public bool ShouldSerializePaperKind()
        {
            return !base._PageSettings.DefaultPaperSize;
        }

        public bool ShouldSerializePaperSource()
        {
            return !base._PageSettings.DefaultPaperSource;
        }

        public bool ShouldSerializePaperWidth()
        {
            return (this.PaperWidth != 0f);
        }

        [Browsable(false)]
        public PrinterCollate Collate
        {
            get
            {
                return base._PageSettings.Collate;
            }
            set
            {
                base._PageSettings.Collate = value;
            }
        }

        [DefaultValue(true), Browsable(false)]
        public bool DefaultPaperSize
        {
            get
            {
                return base._PageSettings.DefaultPaperSize;
            }
            set
            {
                base._PageSettings.DefaultPaperSize = value;
            }
        }

        [DefaultValue(true), Browsable(false)]
        public bool DefaultPaperSource
        {
            get
            {
                return base._PageSettings.DefaultPaperSource;
            }
            set
            {
                base._PageSettings.DefaultPaperSource = value;
            }
        }

        [Browsable(false)]
        public PrinterDuplex Duplex
        {
            get
            {
                return base._PageSettings.Duplex;
            }
            set
            {
                base._PageSettings.Duplex = value;
            }
        }

        [Browsable(false)]
        public float MarginBottom
        {
            get
            {
                return base._PageSettings.Margins.MarginBottom;
            }
            set
            {
                base._PageSettings.Margins.MarginBottom = value;
            }
        }

        [Browsable(false)]
        public float MarginLeft
        {
            get
            {
                return base._PageSettings.Margins.MarginLeft;
            }
            set
            {
                base._PageSettings.Margins.MarginLeft = value;
            }
        }

        [Browsable(false)]
        public float MarginRight
        {
            get
            {
                return base._PageSettings.Margins.MarginRight;
            }
            set
            {
                base._PageSettings.Margins.MarginRight = value;
            }
        }

        [Browsable(false)]
        public float MarginTop
        {
            get
            {
                return base._PageSettings.Margins.MarginTop;
            }
            set
            {
                base._PageSettings.Margins.MarginTop = value;
            }
        }

        [Browsable(false)]
        public PageOrientation Orientation
        {
            get
            {
                return base._PageSettings.Orientation;
            }
            set
            {
                base._PageSettings.Orientation = value;
            }
        }

        [Browsable(false)]
        public float PaperHeight
        {
            get
            {
                return base._PageSettings.PaperHeight;
            }
            set
            {
                base._PageSettings.PaperHeight = value;
            }
        }

        [Browsable(false)]
        public System.Drawing.Printing.PaperKind PaperKind
        {
            get
            {
                return base._PageSettings.PaperKind;
            }
            set
            {
                base._PageSettings.PaperKind = value;
            }
        }

        [Browsable(false)]
        public PaperSourceKind PaperSource
        {
            get
            {
                return base._PageSettings.PaperSource;
            }
            set
            {
                base._PageSettings.PaperSource = value;
            }
        }

        [Browsable(false)]
        public float PaperWidth
        {
            get
            {
                return base._PageSettings.PaperWidth;
            }
            set
            {
                base._PageSettings.PaperWidth = value;
            }
        }
    }
}

