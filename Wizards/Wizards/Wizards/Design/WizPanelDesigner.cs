using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using mControl.Wizards.Controls;

namespace mControl.Wizards.Design
{
    internal class WizPanelDesigner : ParentControlDesigner
    {

        // Fields
        private WizPanel tabPage;

        public WizPanelDesigner()
        {
        }

        protected override void PostFilterProperties(IDictionary Properties)
        {

            //Properties.Remove("ForeColor");
            //Properties.Remove("BackColor");
            Properties.Remove("AutoScroll");
            Properties.Remove("AutoScrollMargin");
            Properties.Remove("AutoScrollMinSize");
            Properties.Remove("BackgroundImage");

        }

        //public override bool CanBeParentedTo(IDesigner parentDesigner)
        //{
        //    return (parentDesigner.Component is CtlTabPages);
        //}


        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            this.tabPage = component as WizPanel;
        }

        protected override bool DrawGrid
        {
            get
            {
                return false;//true;
            }
        }

        //public override SelectionRules SelectionRules
        //{
        //    get
        //    {
        //        return SelectionRules.None;
        //    }
        //}

  
    }

}
