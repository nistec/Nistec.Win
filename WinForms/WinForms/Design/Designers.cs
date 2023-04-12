//#define Client

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Collections;
using System.ComponentModel.Design;
using System.Security.Permissions;
using System.Windows.Forms.Design;
using Nistec.WinForms.Controls;


//using Nistec.Net.License;

namespace Nistec.WinForms.Design
{

    #region Mc Desiner base

    public class ControlDesignerBase : System.Windows.Forms.Design.ControlDesigner
    {
        public ControlDesignerBase()
        {
//#if(!SERVICE)
//    #if(CLIENT)
//            throw new Exception("Invalid Nistec.WinForms License Key");
//            //Nistec.Util.Net.nf_1.NetLogoOpen(netWinMc.ctlNumber,netWinMc.ctlName,netWinMc.ctlVersion, "ControlDesignerBase" ,"CLT");
//    #else
//                Nistec.Net.WinFormsNet.NetFram("ControlDesigner", "DSN");
//#endif
//#endif
        }

        #region Painter

        private DesignerVerbCollection addPainter;

        public override DesignerVerbCollection Verbs
        {
            get
            {
                if (addPainter == null)
                {
                    addPainter = new DesignerVerbCollection();
                    addPainter.Add(new DesignerVerb("Add Painter", new EventHandler(AddPainter)));
                }
                return addPainter;
            }
        }

        void AddPainter(object sender, EventArgs e)
        {

            switch (((ILayout)Control).PainterType)
            {
                case PainterTypes.Button:
                    AddPainterButton();
                    //Control.Container.Add (new  StyleButton (Control.Container));
                    break;
                case PainterTypes.Flat:
                    AddPainterFlat();
                    //Control.Container.Add (new  StyleContainer (Control.Container));
                    break;
                //				case PainterTypes.Grid:
                //					AddPainterGrid();
                //					//Control.Container.Add (new  StyleContainer (Control.Container));
                //					break;
                default://case PainterTypes.Edit:
                    AddPainterEdit();
                    //Control.Container.Add (new  StyleEdit (Control.Container));
                    break;
            }
        }

        private void AddPainterFlat()
        {
            StyleContainer painter = new StyleContainer(Control.Container);
            Control.Container.Add(painter);
            ((ILayout)Control).StylePainter = painter;
        }
        private void AddPainterEdit()
        {
            StyleEdit painter = new StyleEdit(Control.Container);
            Control.Container.Add(painter);
            ((ILayout)Control).StylePainter = painter;
        }
        private void AddPainterButton()
        {
            StyleButton painter = new StyleButton(Control.Container);
            Control.Container.Add(painter);
            ((ILayout)Control).StylePainter = painter;
        }

        //		private void AddPainterGrid()
        //		{
        //			StyleGrid painter=new  StyleGrid (Control.Container);
        //			Control.Container.Add (painter);
        //			((ILayout)Control).StylePainter=painter;
        //		}
        #endregion
    }

    [SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
    public class ScrollableDesignerseBase : ScrollableControlDesigner
    {

        public ScrollableDesignerseBase()
        {
            //#if(!SERVICE)
            //    #if(CLIENT)
            //                throw new Exception("Invalid Nistec.WinForms License Key");
            //                //Nistec.Util.Net.nf_1.NetLogoOpen(netWinMc.ctlNumber,netWinMc.ctlName,netWinMc.ctlVersion,"ScrollableDesignerseBase" ,"CLT");
            //    #else
            //            Nistec.Net.WinFormsNet.NetFram("ScrollableDesignerse", "DSN");
            //#endif
            //#endif
        }

    }

    public class ParentControlDesignerBase : System.Windows.Forms.Design.ParentControlDesigner
    {
        public ParentControlDesignerBase()
        {
            //#if(!SERVICE)
            //    #if(CLIENT)
            //                throw new Exception("Invalid Nistec.WinForms License Key");
            //                //Nistec.Util.Net.nf_1.NetLogoOpen(netWinMc.ctlNumber,netWinMc.ctlName,netWinMc.ctlVersion,"ParentControlDesignerBase","CLT");
            //    #else
            //            Nistec.Net.WinFormsNet.NetFram("ParentControlDesigner", "DSN");
            //#endif
            //#endif
        }
    }

    #endregion

    #region McDesignerBase

    //public class McBaseDesigner : ControlDesignerBase
    //{
    //    public McBaseDesigner() { }

    //    protected override void PostFilterProperties(IDictionary Properties)
    //    {

    //        //Properties.Remove("ForeColor");
    //        //Properties.Remove("BackColor");

    //        //			Properties.Remove("AutoScroll");
    //        //			Properties.Remove("AutoScrollMargin");
    //        //			Properties.Remove("AutoScrollMinSize");
    //        //			Properties.Remove("BackgroundImage");
    //        //			Properties.Remove("Image");
    //        //			Properties.Remove("ImageAlign");
    //        //			Properties.Remove("ImageIndex");
    //        //			Properties.Remove("ImageList");
    //        //			Properties.Remove("AllowDrop");
    //        //			Properties.Remove("ContextMenu");
    //        //			Properties.Remove("FlatStyle");
    //        //			Properties.Remove("Text");
    //        //			Properties.Remove("TextAlign");
    //    }

    //}


    #endregion

    #region ItemCtl Desiner

    //internal class ItemMcDesigner : ControlDesignerBase
    //{
    //    public ItemMcDesigner() { }

    //    protected override void PostFilterProperties(IDictionary Properties)
    //    {

    //        Properties.Remove("AccessibleDescription");
    //        Properties.Remove("AccessibleName");
    //        Properties.Remove("AccessibleRole");
    //        Properties.Remove("AllowDrop");
    //        Properties.Remove("Anchor");
    //        Properties.Remove("BackColor");
    //        Properties.Remove("BackgroundImage");
    //        Properties.Remove("CausesValidation");
    //        Properties.Remove("ContextMenu");
    //        Properties.Remove("Dock");
    //        Properties.Remove("Font");
    //        Properties.Remove("ForeColor");
    //        Properties.Remove("ImeMode");
    //        Properties.Remove("Location");
    //        Properties.Remove("RightToLeft");
    //        Properties.Remove("Site");
    //        Properties.Remove("Size");
    //        Properties.Remove("TabIndex");
    //        Properties.Remove("TabStop");

    //    }
    //}
    #endregion

    #region McButtonEdit Desiner


    internal class McButtonEditDesigner : ControlDesignerBase
    {
        public McButtonEditDesigner() { }

        protected override void PostFilterProperties(IDictionary Properties)
        {
            //Properties.Remove("ForeColor");
            //Properties.Remove("BackColor");
            Properties.Remove("AutoScroll");
            Properties.Remove("AutoScrollMargin");
            Properties.Remove("AutoScrollMinSize");
            Properties.Remove("BackgroundImage");
            Properties.Remove("Image");
            Properties.Remove("ImageAlign");
            Properties.Remove("ImageIndex");
        }
    }
    #endregion

    #region McDesigner

    internal class McButtonDesigner : ControlDesignerBase
    {
        public McButtonDesigner() { }

        protected override void PostFilterProperties(IDictionary Properties)
        {
            Properties.Remove("ForeColor");
            Properties.Remove("BackColor");
            //Properties.Remove("TabStop");
            Properties.Remove("AutoScroll");
            Properties.Remove("AutoScrollMargin");
            Properties.Remove("AutoScrollMinSize");
            Properties.Remove("BackgroundImage");
            Properties.Remove("ShowErrorProvider");
        }
    }

    internal class McDesigner : ControlDesignerBase
    {
        public McDesigner() { }

        protected override void PostFilterProperties(IDictionary Properties)
        {
            //Properties.Remove("ForeColor");
            //Properties.Remove("BackColor");
            //Properties.Remove("TabStop");
            Properties.Remove("AutoScroll");
            Properties.Remove("AutoScrollMargin");
            Properties.Remove("AutoScrollMinSize");
            Properties.Remove("BackgroundImage");
            Properties.Remove("ShowErrorProvider");
        }
    }
    #endregion

    #region McEditDesigner

    internal class McEditDesigner : ControlDesignerBase
    {
        public McEditDesigner() { }

        protected override void PostFilterProperties(IDictionary Properties)
        {
            //Properties.Remove("ForeColor");
            //Properties.Remove("BackColor");
            //Properties.Remove("TabStop");
            Properties.Remove("AutoScroll");
            Properties.Remove("AutoScrollMargin");
            Properties.Remove("AutoScrollMinSize");
            Properties.Remove("BackgroundImage");
        }
    }
    #endregion

    #region Caption Designer

    //[SecurityPermission(SecurityAction.PermitOnly, UnmanagedCode = true)]
    internal class CaptionDesigner : PanelDesigner
    {

        private McCaption caption;

        public CaptionDesigner() { }


        public override void Initialize(IComponent component)
        {
            base.Initialize(component);

            this.caption = component as McCaption;
            //this.caption.Dock = DockStyle.Top;
            //this.caption.Location = new Point(0, 0);
            //			ISelectionService ss = (ISelectionService)GetService(typeof(ISelectionService));
            //			if (ss != null) 
            //			{
            //				ss.SelectionChanged += new EventHandler(OnSelectionChanged);
            //			}
        }

        public override SelectionRules SelectionRules
        {
            get
            {
                return SelectionRules.Locked;//.BottomSizeable;
            }
        }

        protected override void PostFilterProperties(IDictionary Properties)
        {
            //Properties.Remove("ForeColor");
            //Properties.Remove("BackColor");
            Properties.Remove("BorderStyle");
            Properties.Remove("AutoScroll");
            Properties.Remove("AutoScrollMargin");
            Properties.Remove("AutoScrollMinSize");
            Properties.Remove("BackgroundImage");
            Properties.Remove("BackgroundImageLayout");
            Properties.Remove("AllowDrop");
            Properties.Remove("ContextMenu");
            Properties.Remove("ContextMenuStrip");
            Properties.Remove("Margin");
            Properties.Remove("MaximumSize");
            Properties.Remove("MinimumSize");
            Properties.Remove("Padding");
            Properties.Remove("AccessibleName");
            Properties.Remove("AccessibleDescription");
            Properties.Remove("AccessibleRole");
            Properties.Remove("Anchor");
            Properties.Remove("CausesValidation");
            Properties.Remove("TabStop");
        }

        #region Designer
        //		//internal class HelpLabelDesigner : System.Windows.Forms.Design.ControlDesigner 
        //		//{
        //
        //			private bool trackSelection = true;
        //
        //			/// <summary>
        //			/// This property is added to the control's set of properties in the 
        //			/// PreFilterProperties method.  Note that on designers, properties that are
        //			/// explictly declared by TypeDescriptor.CreateProperty can be declared as
        //			/// private on the designer.  This helps to keep the designer's public
        //			/// object model clean.
        //			/// </summary>
        //			private bool TrackSelection
        //			{
        //				get
        //				{
        //					return trackSelection;
        //				}
        //				set
        //				{
        //					trackSelection = value;
        //					if (trackSelection)
        //					{
        //						ISelectionService ss = (ISelectionService)GetService(typeof(ISelectionService));
        //						if (ss != null)
        //						{
        //							UpdateHelpLabelSelection(ss);
        //						}
        //					}
        //					else
        //					{
        //						//McCaption helpLabel = (McCaption)Control;
        //						if (caption.activeControl != null)
        //						{
        //							caption.activeControl = null;
        //							caption.PaintActiveHelpText();
        //							caption.Invalidate();
        //						}
        //					}
        //				}
        //			}
        //
        //			//			public override DesignerVerbCollection Verbs
        //			//			{
        //			//				get
        //			//				{
        //			//					DesignerVerb[] verbs = new DesignerVerb[] {
        //			//																  new DesignerVerb("Sample Verb", new EventHandler(OnSampleVerb))
        //			//															  };
        //			//					return new DesignerVerbCollection(verbs);
        //			//				}
        //			//			}
        //
        //			//
        //			// <doc>
        //			// <desc>
        //			//      Overrides Dispose.  Here we remove our handler for the selection changed
        //			//      event.  With designers, it is critical that they clean up any events they
        //			//      have attached.  Otherwise, during the course of an editing session many
        //			//      designers might get created and never destroyed.
        //			// </desc>
        //			// </doc>
        //			//
        //			protected override void Dispose(bool disposing) 
        //			{
        //				if (disposing)
        //				{
        //					ISelectionService ss = (ISelectionService)GetService(typeof(ISelectionService));
        //					if (ss != null) 
        //					{
        //						ss.SelectionChanged -= new EventHandler(OnSelectionChanged);
        //					}
        //				}
        //
        //				base.Dispose(disposing);
        //			}
        //
        //
        //			private void OnSampleVerb(object sender, EventArgs e)
        //			{
        //				//MessageBox.Show("You have just invoked a sample verb.  Normally, this would do something interesting.");
        //			}
        //
        //			//
        //			// <doc>
        //			// <desc>
        //			//      Our handler for the selection change event.  Here we update the active control within
        //			//      the help label.
        //			// </desc>
        //			// </doc>
        //			//
        //			private void OnSelectionChanged(object sender, EventArgs e) 
        //			{
        //	
        //				if (trackSelection)
        //				{
        //					ISelectionService ss = (ISelectionService)sender;
        //					UpdateHelpLabelSelection(ss);
        //				}
        //			}
        //
        //			protected override void PreFilterProperties(IDictionary properties)
        //			{
        //				// Always call base first in PreFilter* methods, and last in PostFilter*
        //				// methods.
        //				base.PreFilterProperties(properties);
        //
        //				// We add a design-time property called TrackSelection that is used to track
        //				// the active selection.  If the user sets this to true (the default), then
        //				// we will listen to selection change events and update the control's active
        //				// control to point to the current primary selection.
        //				properties["TrackSelection"] = TypeDescriptor.CreateProperty(
        //					this.GetType(),   // the type this property is defined on
        //					"TrackSelection", // the name of the property
        //					typeof(bool),   // the type of the property
        //					new Attribute[] {CategoryAttribute.Design});  // attributes
        //			}
        //
        //			/// <summary>
        //			/// This is a helper method that, given a selection service, will update the active control
        //			/// of our help label with the currently active selection.
        //			/// </summary>
        //			/// <param name="ss"></param>
        //			private void UpdateHelpLabelSelection(System.ComponentModel.Design.ISelectionService ss)
        //			{
        //				Control c = (Control)ss.PrimarySelection;
        //				if (c != null)
        //				{
        //					caption.activeControl = c;
        //					caption.PaintActiveHelpText();
        //					caption.Invalidate();
        //				}
        //				else
        //				{
        //					if (caption.activeControl != null)
        //					{
        //						caption.activeControl = null;
        //						caption.PaintActiveHelpText();
        //						caption.Invalidate();
        //					}
        //				}
        //			}
        //		//}

        #endregion

    }

    #endregion

    #region PanellDesigner

    [SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
    internal class PanelDesigner : ScrollableDesignerseBase
    {

        private McPanel panel;

        public PanelDesigner() { }

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            this.panel = component as McPanel;
 
        }


        protected override void PostFilterProperties(IDictionary Properties)
        {
            //Properties.Remove("ForeColor");
            //Properties.Remove("BackColor");
            //Properties.Remove("BorderStyle");
        }

        private void DrawBorder(Graphics graphics)
        {
            Color color1;
            Control control1 = this.Control;
            Rectangle rectangle1 = control1.ClientRectangle;
            if (control1.BackColor.GetBrightness() < 0.5)
            {
                color1 = ControlPaint.Light(control1.BackColor);
            }
            else
            {
                color1 = ControlPaint.Dark(control1.BackColor);
            }
            Pen pen1 = new Pen(color1);
            pen1.DashStyle = DashStyle.Dash;
            rectangle1.Width--;
            rectangle1.Height--;
            graphics.DrawRectangle(pen1, rectangle1);
            pen1.Dispose();
        }

        protected override void OnPaintAdornments(PaintEventArgs pe)
        {
            if (base.Component is McPanel)
            {
                Nistec.WinForms.McPanel panel1 = (McPanel)base.Component;
                if (panel1.BorderStyle == BorderStyle.None)
                {
                    this.DrawBorder(pe.Graphics);
                }
            }
            base.OnPaintAdornments(pe);
        }

        #region Painter

        private DesignerVerbCollection addPainter;

        public override DesignerVerbCollection Verbs
        {
            get
            {
                if (addPainter == null)
                {
                    addPainter = new DesignerVerbCollection();
                    addPainter.Add(new DesignerVerb("Add Painter", new EventHandler(AddPainter)));
                }
                return addPainter;
            }
        }

        void AddPainter(object sender, EventArgs e)
        {
            StyleContainer painter = new StyleContainer(Control.Container);
            Control.Container.Add(painter);
            this.panel.StylePainter = painter;
        }

        #endregion
    }


    #endregion

    #region CheckBoxDesigner

    internal class CheckBoxDesigner : ControlDesignerBase
    {
        public CheckBoxDesigner() { }

        protected override void PostFilterProperties(IDictionary Properties)
        {
            //Properties.Remove("ForeColor");
            //Properties.Remove("BackColor");
            Properties.Remove("AutoScroll");
            Properties.Remove("AutoScrollMargin");
            Properties.Remove("AutoScrollMinSize");
            Properties.Remove("BackgroundImage");
            Properties.Remove("ShowErrorProvider");
            Properties.Remove("ControlLayout"); 
        }
    }
    #endregion

    #region ComboBoxDesigner

    [SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
    internal class ComboBoxDesigner : ControlDesignerBase
    {
        public ComboBoxDesigner()
        {
            this.propChanged = null;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.propChanged != null))
            {
                ((McComboBox)this.Control).StyleChanged -= this.propChanged;
            }
            base.Dispose(disposing);
        }

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            this.propChanged = new EventHandler(this.OnControlPropertyChanged);
            ((McComboBox)this.Control).StyleChanged += this.propChanged;
        }


        private void OnControlPropertyChanged(object sender, EventArgs e)
        {
            //			ISelectionUIService service1 = (ISelectionUIService) this.GetService(typeof(ISelectionUIService));
            //			if (service1 != null)
            //			{
            //				service1.SyncComponent((IComponent) sender);
            //			}
        }


        public override SelectionRules SelectionRules
        {
            get
            {
                SelectionRules rules1 = base.SelectionRules;
                object obj1 = base.Component;
                PropertyDescriptor descriptor1 = TypeDescriptor.GetProperties(obj1)["DropDownStyle"];
                if (descriptor1 == null)
                {
                    return rules1;
                }
                ComboBoxStyle style1 = (ComboBoxStyle)descriptor1.GetValue(obj1);
                if ((style1 != ComboBoxStyle.DropDown) && (style1 != ComboBoxStyle.DropDownList))
                {
                    return rules1;
                }
                return (rules1 & ((SelectionRules)(-4)));
            }
        }

        protected override void PostFilterProperties(IDictionary Properties)
        {
            //Properties.Remove("ForeColor");
            //Properties.Remove("BackColor");
            Properties.Remove("AutoScroll");
            Properties.Remove("AutoScrollMargin");
            Properties.Remove("AutoScrollMinSize");
            Properties.Remove("BackgroundImage");
            Properties.Remove("Image");
            //Properties.Remove("ShowErrorProvider");
        }

        // Fields
        private EventHandler propChanged;
    }

    #endregion

    #region MultiBoxDesigner

    internal class MultiBoxDesigner : ParentControlDesignerBase
    {
        private ISelectionService _selectionService = null;

        public ISelectionService SelectionService
        {
            get
            {
                // Is this the first time the accessor has been called?
                if (_selectionService == null)
                {
                    // Then grab and cache the required interface
                    _selectionService = (ISelectionService)GetService(typeof(ISelectionService));
                }

                return _selectionService;
            }
        }

        internal void RaiseFilterProperties(IDictionary Properties)
        {
            PostFilterProperties(Properties);
        }

        protected override void PostFilterProperties(IDictionary Properties)
        {

            //FieldType mct= ((Nistec.WinForms.McMultiBox)base.Control).MultiType;

            //Properties.Remove("ForeColor");
            //Properties.Remove("BackColor");
            Properties.Remove("AutoScroll");
            Properties.Remove("AutoScrollMargin");
            Properties.Remove("AutoScrollMinSize");
            Properties.Remove("BackgroundImage");
            Properties.Remove("Image");
        }
        protected override bool DrawGrid
        {
            get { return false; }
        }

    }

    #endregion

    #region NavigatoreDesigner

    internal class NavigatoreDesigner : ParentControlDesignerBase
    {
        public NavigatoreDesigner() { }

        #region verbs

        private DesignerVerbCollection verbs;//addPainter;

        public override DesignerVerbCollection Verbs
        {
            get
            {
                if (verbs == null)
                {
                    verbs = new DesignerVerbCollection();
                    verbs.Add(new DesignerVerb("Add Painter", new EventHandler(AddPainter)));
                }
                return verbs;
            }
        }

        void AddPainter(object sender, EventArgs e)
        {
            StyleContainer painter = new StyleContainer(Control.Container);
            Control.Container.Add(painter);
            ((ILayout)Control).StylePainter = painter;
        }

        void AddDataSet(object sender, EventArgs e)
        {
            Control.Container.Add(new System.Data.DataSet());
        }

        #endregion

        protected override bool DrawGrid
        {
            get
            {
                return false;
            }
        }


        protected override void PostFilterProperties(IDictionary Properties)
        {
            Properties.Remove("ForeColor");
            Properties.Remove("BackColor");
            Properties.Remove("AutoScroll");
            Properties.Remove("AutoScrollMargin");
            Properties.Remove("AutoScrollMinSize");
            Properties.Remove("BackgroundImage");
            Properties.Remove("Image");
            Properties.Remove("ImageAlign");
            Properties.Remove("ShowErrorProvider");
            Properties.Remove("TextAlign");
            Properties.Remove("Text");
        }
    }

    internal class NavBarDesigner : ParentControlDesignerBase
    {
        public NavBarDesigner() { }

        #region verbs

        private DesignerVerbCollection verbs;//addPainter;

        public override DesignerVerbCollection Verbs
        {
            get
            {
                if (verbs == null)
                {
                    verbs = new DesignerVerbCollection();
                    verbs.Add(new DesignerVerb("Add Painter", new EventHandler(AddPainter)));
                    verbs.Add(new DesignerVerb("Add DataSet", new EventHandler(AddDataSet)));

                }
                return verbs;
            }
        }

        void AddPainter(object sender, EventArgs e)
        {
            StyleContainer painter = new StyleContainer(Control.Container);
            Control.Container.Add(painter);
            ((ILayout)Control).StylePainter = painter;
        }

        void AddDataSet(object sender, EventArgs e)
        {
            Control.Container.Add(new System.Data.DataSet());
        }

        #endregion

        protected override bool DrawGrid
        {
            get
            {
                return false;
            }
        }


        protected override void PostFilterProperties(IDictionary Properties)
        {
            Properties.Remove("ForeColor");
            Properties.Remove("BackColor");
            Properties.Remove("AutoScroll");
            Properties.Remove("AutoScrollMargin");
            Properties.Remove("AutoScrollMinSize");
            Properties.Remove("BackgroundImage");
            Properties.Remove("Image");
            Properties.Remove("ImageAlign");
            Properties.Remove("ShowErrorProvider");
            Properties.Remove("TextAlign");
            Properties.Remove("Text");
        }
    }
    #endregion

    #region ColorPickerDesigner

    internal class ColorPickerDesigner : ControlDesignerBase
    {
        public ColorPickerDesigner() { }

        protected override void PostFilterProperties(IDictionary Properties)
        {
            Properties.Remove("ForeColor");
            Properties.Remove("BackColor");
            Properties.Remove("AutoScroll");
            Properties.Remove("AutoScrollMargin");
            Properties.Remove("AutoScrollMinSize");
            Properties.Remove("BackgroundImage");
            Properties.Remove("Image");
            //Properties.Remove("ShowErrorProvider");
        }
    }
    #endregion

    #region McShapes Desiner

    internal class ShapesDesigner : ControlDesignerBase
    {
        public ShapesDesigner() { }

        protected override void PostFilterProperties(IDictionary Properties)
        {
            Properties.Remove("ForeColor");
            Properties.Remove("BackColor");
            Properties.Remove("AutoScroll");
            Properties.Remove("AutoScrollMargin");
            Properties.Remove("AutoScrollMinSize");
            Properties.Remove("CausesValidation");
            Properties.Remove("BackgroundImage");
            Properties.Remove("Image");
            Properties.Remove("ImageAlign");
            Properties.Remove("ImageIndex");
            Properties.Remove("ImageList");
            Properties.Remove("AllowDrop");
            Properties.Remove("ContextMenu");
            Properties.Remove("FlatStyle");
            Properties.Remove("Text");
            Properties.Remove("TextAlign");
            Properties.Remove("BorderStyle");
            Properties.Remove("TabStop");
            Properties.Remove("RightToLeft");
            Properties.Remove("FixSize");
        }
    }
    #endregion

    #region DropDownDesigner

    //internal class DropDownDesigner : ControlDesignerBase
    //{
    //    public DropDownDesigner() { }

    //    protected override void PostFilterProperties(IDictionary Properties)
    //    {

    //        Properties.Remove("AccessibleDescription");
    //        Properties.Remove("AccessibleName");
    //        Properties.Remove("AccessibleRole");
    //        Properties.Remove("Anchor");
    //        Properties.Remove("ForeColor");
    //        Properties.Remove("BackColor");
    //        Properties.Remove("BackgroundImage");
    //        Properties.Remove("Image");
    //        Properties.Remove("ImageAlign");
    //        Properties.Remove("ImageIndex");
    //        Properties.Remove("ImageList");
    //        Properties.Remove("AllowDrop");
    //        Properties.Remove("ContextMenu");
    //        Properties.Remove("Doc");
    //        Properties.Remove("ContextMenu");
    //        Properties.Remove("Font");
    //        Properties.Remove("Location");
    //        Properties.Remove("TabIndex");
    //        Properties.Remove("TabStop");

    //        Properties.Remove("DroppedDown");
    //        Properties.Remove("SelectedIndex");
    //        Properties.Remove("SelectedItem");
    //        Properties.Remove("SelectedValue");
    //        Properties.Remove("InternalList");

    //    }
    //}
    #endregion

    #region McSplitter Desiner

    internal class SplitterDesigner : ControlDesignerBase
    {
        public SplitterDesigner() { }

        protected override void PostFilterProperties(IDictionary Properties)
        {
            //Properties.Remove("ForeColor");
            //Properties.Remove("BackColor");
            //Properties.Remove("BorderStyle");
        }
    }
    #endregion

    #region LinkLabelItemDesigner

    internal class LinkLabelItemDesigner : ControlDesignerBase
    {
        public LinkLabelItemDesigner() { }

        protected override void PostFilterProperties(IDictionary Properties)
        {
            Properties.Remove("ImageAlign");
            Properties.Remove("TextAlign");
            Properties.Remove("TabStop");
            Properties.Remove("AutoScroll");
            Properties.Remove("AutoScrollMargin");
            Properties.Remove("AutoScrollMinSize");
            Properties.Remove("BackgroundImage");
            Properties.Remove("ShowErrorProvider");
            Properties.Remove("BorderStyle");
            Properties.Remove("ControlLayout");
            Properties.Remove("BackColor");
            Properties.Remove("StyleGuide");
        }
    }
    #endregion

    #region McListControlDesigner

    [SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
    internal class McListControlDesigner : ControlDesignerBase
    {
        public McListControlDesigner() { }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                IComponentChangeService service1 = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
                if (service1 != null)
                {
                    service1.ComponentRename -= new ComponentRenameEventHandler(this.OnComponentRename);
                }
            }
            base.Dispose(disposing);
        }

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            IComponentChangeService service1 = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
            if (service1 != null)
            {
                service1.ComponentRename += new ComponentRenameEventHandler(this.OnComponentRename);
            }
        }

        private void OnComponentRename(object sender, ComponentRenameEventArgs e)
        {
            if (e.Component == base.Component)
            {
                this.UpdateControlName(e.NewName);
            }
        }

        protected override void OnCreateHandle()
        {
            base.OnCreateHandle();
            PropertyDescriptor descriptor1 = TypeDescriptor.GetProperties(base.Component)["Name"];
            if (descriptor1 != null)
            {
                this.UpdateControlName(descriptor1.GetValue(base.Component).ToString());
            }
        }


        private void UpdateControlName(string name)
        {
            McListControl box1 = (McListControl)this.Control;
            if (box1.IsHandleCreated && (box1.Items.Count == 0))
            {
                Win32.WinAPI.SendMessage(box1.Handle, 0x184, 0, 0);
                Win32.WinAPI.SendMessage(box1.Handle, 0x180, 0, name);
            }
        }

        protected override void PostFilterProperties(IDictionary Properties)
        {
            //Properties.Remove("ForeColor");
            //Properties.Remove("BackColor");
        }

    }


    #endregion

    #region Progress Desiner

    internal class ProgressBarDesigner : ControlDesignerBase
    {
        public ProgressBarDesigner()
        { }

        // clean up some unnecessary properties
        protected override void PostFilterProperties(IDictionary Properties)
        {
            //Properties.Remove("ForeColor");
            //Properties.Remove("BackColor");
            Properties.Remove("AllowDrop");
            Properties.Remove("BackgroundImage");
            Properties.Remove("ContextMenu");
            Properties.Remove("FlatStyle");
            Properties.Remove("Image");
            Properties.Remove("ImageAlign");
            Properties.Remove("ImageIndex");
            Properties.Remove("ImageList");
            Properties.Remove("Text");
            Properties.Remove("TextAlign");
        }
    }
    #endregion

    #region LabelDesigner

    internal class McStyleDesigner : ControlDesignerBase
    {
        public McStyleDesigner() { }

        protected override void PostFilterProperties(IDictionary Properties)
        {
            Properties.Remove("Site");
            Properties.Remove("Container");
        }
    }
    #endregion

    #region GroupBoxDesiger

    [SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
    internal class GroupBoxDesigner : ParentControlDesignerBase
    {

        private McGroupBox groupBox;
        public GroupBoxDesigner()
        {
        }

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            this.groupBox = component as McGroupBox;
        }


        #region Painter

        private DesignerVerbCollection addPainter;

        public override DesignerVerbCollection Verbs
        {
            get
            {
                if (addPainter == null)
                {
                    addPainter = new DesignerVerbCollection();
                    addPainter.Add(new DesignerVerb("Add Painter", new EventHandler(AddPainter)));
                }
                return addPainter;
            }
        }

        void AddPainter(object sender, EventArgs e)
        {
            StyleContainer painter = new StyleContainer(Control.Container);
            Control.Container.Add(painter);
            this.groupBox.StylePainter = painter;
        }

        #endregion

        protected override void OnPaintAdornments(PaintEventArgs pe)
        {
            if (this.DrawGrid)
            {
                Control control1 = this.Control;
                Rectangle rectangle1 = this.Control.DisplayRectangle;
                rectangle1.Width++;
                rectangle1.Height++;
                ControlPaint.DrawGrid(pe.Graphics, rectangle1, base.GridSize, control1.BackColor);
            }
            if (base.Inherited)
            {
                if (this.inheritanceUI == null)
                {
                    this.inheritanceUI = (InheritanceUI)this.GetService(typeof(InheritanceUI));
                }
                if (this.inheritanceUI != null)
                {
                    pe.Graphics.DrawImage(this.inheritanceUI.InheritanceGlyph, 0, 0);
                }
            }
        }


        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x84)
            {
                base.WndProc(ref m);
                if (((int)m.Result) != -1)
                {
                    return;
                }
                m.Result = (IntPtr)1;
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        protected override Point DefaultControlLocation
        {
            get
            {
                McGroupBox box1 = (McGroupBox)this.Control;
                return new Point(box1.DisplayRectangle.X, box1.DisplayRectangle.Y);
            }
        }

        // Fields
        private InheritanceUI inheritanceUI;
    }

    #endregion

    #region ContainerDesiger

    [SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
    internal class ContainerDesigner : ParentControlDesignerBase
    {

        public ContainerDesigner()
        {
        }

        protected override void OnPaintAdornments(PaintEventArgs pe)
        {
            if (this.DrawGrid)
            {
                Control control1 = this.Control;
                Rectangle rectangle1 = this.Control.DisplayRectangle;
                rectangle1.Width++;
                rectangle1.Height++;
                ControlPaint.DrawGrid(pe.Graphics, rectangle1, base.GridSize, control1.BackColor);
            }
            if (base.Inherited)
            {
                if (this.inheritanceUI == null)
                {
                    this.inheritanceUI = (InheritanceUI)this.GetService(typeof(InheritanceUI));
                }
                if (this.inheritanceUI != null)
                {
                    pe.Graphics.DrawImage(this.inheritanceUI.InheritanceGlyph, 0, 0);
                }
            }
        }


        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x84)
            {
                base.WndProc(ref m);
                if (((int)m.Result) != -1)
                {
                    return;
                }
                m.Result = (IntPtr)1;
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        protected override Point DefaultControlLocation
        {
            get
            {
                McContainer box1 = (McContainer)this.Control;
                return new Point(box1.DisplayRectangle.X, box1.DisplayRectangle.Y);
            }
        }

        // Fields
        private InheritanceUI inheritanceUI;
    }

    #endregion

    #region InheritanceUI

    [SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
    public class InheritanceUI
    {
        public InheritanceUI()
        {
        }

        public void AddInheritedControl(Control c, InheritanceLevel level)
        {
            string text1;
            if (this.tooltip == null)
            {
                this.tooltip = new ToolTip();
                this.tooltip.ShowAlways = true;
            }
            if (level == InheritanceLevel.InheritedReadOnly)
            {
                text1 = "DesignerInheritedReadOnly";
            }
            else
            {
                text1 = "DesignerInherited";
            }
            this.tooltip.SetToolTip(c, text1);
            foreach (Control control1 in c.Controls)
            {
                if (control1.Site == null)
                {
                    this.tooltip.SetToolTip(control1, text1);
                }
            }
        }

        public void Dispose()
        {
            if (this.tooltip != null)
            {
                this.tooltip.Dispose();
            }
        }


        public void RemoveInheritedControl(Control c)
        {
            if ((this.tooltip != null) && (this.tooltip.GetToolTip(c).Length > 0))
            {
                this.tooltip.SetToolTip(c, null);
                foreach (Control control1 in c.Controls)
                {
                    if (control1.Site == null)
                    {
                        this.tooltip.SetToolTip(control1, null);
                    }
                }
            }
        }


        public Bitmap InheritanceGlyph
        {
            get
            {
                if (InheritanceUI.inheritanceGlyph == null)
                {
                    InheritanceUI.inheritanceGlyph = new Bitmap(typeof(InheritanceUI), "InheritedGlyph.bmp");
                    InheritanceUI.inheritanceGlyph.MakeTransparent();
                }
                return InheritanceUI.inheritanceGlyph;
            }
        }
        public Rectangle InheritanceGlyphRectangle
        {
            get
            {
                if (InheritanceUI.inheritanceGlyphRect == Rectangle.Empty)
                {
                    Size size1 = this.InheritanceGlyph.Size;
                    InheritanceUI.inheritanceGlyphRect = new Rectangle(0, 0, size1.Width, size1.Height);
                }
                return InheritanceUI.inheritanceGlyphRect;
            }
        }

        // Fields
        private static Bitmap inheritanceGlyph;
        private static Rectangle inheritanceGlyphRect;
        private ToolTip tooltip;
    }


    #endregion

    #region McResize

    [SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
    internal class McResizeDesigner : ControlDesignerBase
    {
        public McResizeDesigner()
        {
            
        }

        protected override void Dispose(bool disposing)
        {
             base.Dispose(disposing);
        }

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            ((McResize)this.Control).BackColor = Color.Transparent;
            //((McResize)this.Control).SetLocation();
        }

        
         public override SelectionRules SelectionRules
        {
            get
            {
                return SelectionRules.None;
            }
        }

        protected override void PostFilterProperties(IDictionary Properties)
        {
            //Properties.Remove("ForeColor");
            //Properties.Remove("BackColor");
            Properties.Remove("AutoScroll");
            Properties.Remove("AutoScrollMargin");
            Properties.Remove("AutoScrollMinSize");
            Properties.Remove("BackgroundImage");
            Properties.Remove("Image");
            //Properties.Remove("ShowErrorProvider");
        }

     }

    #endregion


}