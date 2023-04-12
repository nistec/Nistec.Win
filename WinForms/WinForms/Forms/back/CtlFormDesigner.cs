using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Collections;
using System.ComponentModel.Design;
using System.Security.Permissions;
using System.Windows.Forms.Design;

using mControl.WinCtl.Forms;
using mControl.WinCtl.Controls;

namespace mControl.WinCtl.Forms.Design
{

	/// <summary>
	/// Summary description for CtlFormDesigner.
	/// </summary>
	[SecurityPermission(SecurityAction.Demand)]
	internal class CtlFormDesigner : DocumentDesigner
	{

		public CtlFormDesigner()
		{
		}

		private DesignerVerbCollection addCaption;


		public override DesignerVerbCollection Verbs
		{
			get
			{
				if(addCaption == null)
				{
					addCaption = new DesignerVerbCollection();
				}
                addCaption.Add(new DesignerVerb("addCaption", new EventHandler(AddCaption)));
                return addCaption;
			}
		}

		void AddCaption(object sender, EventArgs e)
		{
			CtlCaption  ctl=new CtlCaption();
			Control.Container.Add (ctl);
			//((CtlForm) base.Component).Caption=ctl;
		}

	}
}
