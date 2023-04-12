using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;
using System.Collections;

namespace Nistec.WinForms.Design
{
    internal class CaptionFormDesigner : ParentControlDesigner
{
    // Fields
    private IComponentChangeService componentChangedService;
    private IDesignerHost hostService;
    private McCaptionForm ribbon;
    private ISelectionService selectionService;
    private const int WM_LBUTTONDOWN = 0x201;



        public CaptionFormDesigner()
        {
            //Nistec.Net.WinFormsNet.NetFram("CaptionFormDesigner", "DSN");
        }

    // Methods
    public override void Initialize(IComponent component)
    {
        base.Initialize(component);
        if (-2147483648 != 0)
        {
            if (this.Control is McCaptionForm)
            {
                this.hostService = (IDesignerHost) this.GetService(typeof(IDesignerHost));
                if (0x7fffffff != 0)
                {
                }
                this.componentChangedService = (IComponentChangeService) this.GetService(typeof(IComponentChangeService));
                this.selectionService = (ISelectionService) this.GetService(typeof(ISelectionService));
                if (0xff != 0)
                {
                    goto Label_000E;
                }
            }
            return;
        }
    Label_000E:
        this.ribbon = this.Control as McCaptionForm;
    }

    protected override void WndProc(ref Message m)
    {
        Point point=Point.Empty;
        int msg=0;
        base.WndProc(ref m);
        if (((((uint) msg) + ((uint) msg)) <= uint.MaxValue) && (this.ribbon == null))
        {
                goto Label_000C;
        }
        if (m.HWnd == this.ribbon.Handle)
        {
            msg = m.Msg;
            if (msg == 0x201)
            {
                goto Label_0113;
            }
        }
        return;
    Label_000C:
        if ((((uint) msg) - ((uint) msg)) >= 0)
        {
            return;
        }
    Label_0037:
            return;
    Label_0113:
        this.ribbon.SendMsg(ref m);
            if ((((uint) msg) + ((uint) msg)) < 0)
            {
                return;
            }
                point = this.x3d0370f1e847fa3e((int) m.LParam);
                goto Label_0037;
    }


    private int x31b92269760e6e6d(int x130fbcecf32fe781)
    {
        return (x130fbcecf32fe781 >> 0x10);
    }

    private Point x3d0370f1e847fa3e(int x130fbcecf32fe781)
    {
        return new Point(this.xcb3a309bf0197241(x130fbcecf32fe781), this.x31b92269760e6e6d(x130fbcecf32fe781));
    }

    private int xcb3a309bf0197241(int x130fbcecf32fe781)
    {
        return (x130fbcecf32fe781 & 0xffff);
    }

 
 
}

 
 

}
