
using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;

using Nistec.Collections;
using Nistec.Drawing;
using Nistec.WinForms;
using Nistec.WinForms.Design;

namespace Nistec.Wizards
{


     public class WizardPageCollection : CollectionWithEvents
    {
        internal McWizard wizOwner;

        public WizardPage Add(McTabPage value)
        {
            // Create a WizardPage from the McTabPage
            WizardPage wp = new WizardPage();
            wp.Text = value.Text;//.Title;
            wp.wizParent = wizOwner;

            //            wp.Control = value.Control;
            //            wp.ImageIndex = value.ImageIndex;
            //            wp.ImageList = value.ImageList;
            //            wp.Icon = value.Icon;
            //            wp.Selected = value.Selected;
            //            wp.StartFocus = value.StartFocus;

            return Add(wp);
        }

        public WizardPage Add(WizardPage value)
        {
            value.wizParent = wizOwner;
            base.List.Add(value as object);

            return value;
        }

        public void AddRange(WizardPage[] values)
        {
            // Use existing method to add each array entry
            foreach (WizardPage page in values)
                Add(page);
        }

        public void Remove(WizardPage value)
        {
            // Use base class to process actual collection operation
            base.List.Remove(value as object);
        }

        public void Insert(int index, WizardPage value)
        {
            value.wizParent = wizOwner;
            base.List.Insert(index, value as object);
        }

        public void MoveTo(int index, WizardPage value)
        {
            // Use base class to process actual collection operation
            base.List.Remove(value as object);
            base.List.Insert(index, value as object);
        }

        public bool Contains(WizardPage value)
        {
            // Use base class to process actual collection operation
            return base.List.Contains(value as object);
        }

        public WizardPage this[int index]
        {
            // Use base class to process actual collection operation
            get { return (base.List[index] as WizardPage); }
        }

        public WizardPage this[string title]
        {
            get
            {
                // Search for a Page with a matching title
                foreach (WizardPage page in base.List)
                    if (page.Text == title)
                        return page;

                return null;
            }
        }

        public int IndexOf(WizardPage value)
        {
            // Find the 0 based index of the requested entry
            return base.List.IndexOf(value);
        }
    }

}
