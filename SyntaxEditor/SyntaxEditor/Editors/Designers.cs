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

//using Nistec.Net.License;

namespace Nistec.SyntaxEditor.Design
{

    public class SyntaxEditorDesigner : System.Windows.Forms.Design.ParentControlDesigner
    {
        public SyntaxEditorDesigner()
        {
//#if(CLIENT)
//                throw new Exception("Invalid Nistec.Client.SyntaxEditor License Key");
//                //Nistec.Util.Net.nf_1.NetLogoOpen(netSyntaxEditor.ctlNumber,netSyntaxEditor.ctlName,netSyntaxEditor.ctlVersion,"ParentControlDesignerBase","Ctl");
//#else
//            Nistec.Net.SyntaxEditorNet.NetFram("SyntaxEditorDesigner", "DSN");
//#endif
        }
    }

 }