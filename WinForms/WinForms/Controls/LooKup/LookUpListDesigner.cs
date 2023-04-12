using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Windows.Forms.Design;


using Nistec.Collections; 
using Nistec.Win32;


namespace Nistec.WinForms.Design
{

		internal class LookUpListDesigner :ControlDesigner
		{

			#region Memmbers
			private McLookUpList ctl;
			private DesignerVerbCollection actions;

			#endregion

			public LookUpListDesigner(){}

			public override void Initialize(IComponent component)
			{
				base.Initialize (component);
				this.ctl=component as McLookUpList;
			}

			protected override void OnPaintAdornments(PaintEventArgs pe)
			{
				base.OnPaintAdornments (pe);

				//if(!ctl.ShowColumnHeaders)
				//	return;
				//ctl.DrawColumnHeaders(pe);
			}

		
			#region UserDefinedVariables

			public override DesignerVerbCollection Verbs
			{
				get
				{
					if(actions == null)
					{
						actions = new DesignerVerbCollection();
							actions.Add(new DesignerVerb("Add Painter", new EventHandler(AddPainter)));

					}
					return actions;
				}
			}

			void AddPainter(object sender, EventArgs e)
			{
				StyleContainer painter=new  StyleContainer (Control.Container);
				Control.Container.Add (painter);
				((ILayout)Control).StylePainter=painter;
			}

			#endregion

		}
		
	}


