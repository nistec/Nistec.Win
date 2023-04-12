
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
    //public interface IMcWizard
    //{
    //    WizardPageCollection WizardPages { get; }
    //    McTabPage SelectedPage { get;set; }
    //    int SelectedIndex { get;set; }
    //    McTabControl McTabControl { get; }
    //    IStyle StylePainter { get;set; }
    //}

    //	public enum McStatus
    //	{
    //		Default,
    //		Yes,
    //		No
    //	}

    public enum WizardType
    {
        Install,
        Configure,
        Controller
    }

    public enum ButtonMode
    {
        Default,
        Disable,
        Hide
    }
    public enum EditListType
    {
        Edit = 0,
        Insert = 1
    }
    public enum WizardPanelType
    {
        Configure,
        Controller
    }

}
