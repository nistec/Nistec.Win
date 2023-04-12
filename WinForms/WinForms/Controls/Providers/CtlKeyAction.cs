
using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using System.Reflection;
using System.ComponentModel.Design;
using Nistec.WinForms.Controls;




namespace Nistec.WinForms
{


	/// <summary>
	/// Provider keyAction properties to controls that can be validated.
	/// </summary>
	[ProvideProperty("KeyRule", typeof(Control))]
	//[Designer(typeof(Nistec.WinForms.KeyRuleProviderDesigner))]
    [ToolboxItem(false), ToolboxBitmap(typeof(McKeyAction), "Toolbox.KeyAction.bmp")]
	public class McKeyAction : System.ComponentModel.Component, IExtenderProvider
	{
		private Hashtable					_KeyActionRules			= new Hashtable();
		private Keys						_SpecialKey				=Keys.Insert;

		#region "Properties"


		/// <summary>
		/// Set the cpecial key 
		/// </summary>
		[Category("Behavior"),DefaultValue(Keys.Insert)]
		public Keys SpecialKey
		{
			get{return _SpecialKey;}
			set{_SpecialKey=value;}
		}

		/// <summary>
		/// Set keyAction rule.
		/// </summary>
		/// <param name="control"></param>
		/// <param name="kr"></param>
		public void SetKeyRule(object control, string value)
		{

			if (control != null)
			{
				// only throw error in DesignMode
				if (base.DesignMode) 
				{
					if (!this.CanExtend(control))
						throw new InvalidOperationException(control.GetType().ToString() 
							+ " is not supported by the keyAction provider.");
				}
			
				if (value == null) 
				{
					value = string.Empty;
				}

				if (value.Length == 0) 
				{
					_KeyActionRules.Remove(control);
					((Control)control).KeyDown-=new KeyEventHandler(McKeyAction_KeyDown);
				}
				else if(!this._KeyActionRules.Contains(control)) 
				{
					this._KeyActionRules.Add(control, value);
					((Control)control).KeyDown+=new KeyEventHandler(McKeyAction_KeyDown);
				}

			}
		}

		private void McKeyAction_KeyDown(object sender, KeyEventArgs e)
		{
			Control ctl= (Control)sender;
			e.Handled= McAction(ctl,e.KeyData);
		}

		private bool McAction(Control control,Keys key)
		{

			if(_KeyActionRules.Contains(control))
			{
				if(key==_SpecialKey)
				{
					SetCustomValue(control,_KeyActionRules[control].ToString());
				}
			}
			return false;
		}

		public void RemoveKeyRule(object control)
		{
			if (control != null)
			{
				this._KeyActionRules.Remove(control);
				((Control)control).KeyDown-=new KeyEventHandler(McKeyAction_KeyDown);
			}

		}

		/// <summary>
		/// Get keyAction rule.
		/// </summary>
		/// <param name="control"></param>
		/// <returns></returns>
		[DefaultValue(""), Category("Data")]
		//[Editor(typeof(Nistec.WinForms.KeyActionRuleEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string GetKeyRule(object control)
		{
			object text = _KeyActionRules[control];
			if (text == null) 
			{
				text = string.Empty;
			}
			return text.ToString();
		}


		#endregion

		#region Action Methods


		public void SetCustomValue(Control ctl,string value)
		{
			if(value.Length==0)
			{
				//return "";
			}
			else if(value.StartsWith("[") && value.EndsWith("]"))
			{
				Form form=ctl.FindForm();
				foreach(Control c in form.Controls)
				{
					if("[" + c.Name + "]" == value)
					{
						ctl.Text= c.Text; 
						break;
					}
				}
				//return "";
			}
			else if(value.StartsWith("{") && value.EndsWith("}"))
			{
				string val= GetInternalValue(value.Substring(1,value.Length-2));
				if(val !="")
					ctl.Text= val;
			}
			else
			{
				ctl.Text= value;
			}
		}

		private string GetInternalValue(string val)
		{
			val=val.ToLower();
			string retVal="";
 
			if(val.Equals("now"))
			{
					retVal= DateTime.Now.ToString();
			}
			else if(val.Equals("today"))
			{
					retVal= DateTime.Today.ToShortDateString();
			}
			else if(val.Equals("time"))
			{
					retVal =DateTime.Today.ToShortTimeString();
			}
			else if(val.Equals("zero") || val.Equals("0"))
			{
					retVal= "0";
			}
			else
				retVal= "";

			return retVal;
		}


		#endregion

		#region Component Construction
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Designer Ctor.
		/// </summary>
		/// <param name="container"></param>
		public McKeyAction(System.ComponentModel.IContainer container):this()
		{
			container.Add(this);
			//InitializeComponent();
		}

		/// <summary>
		/// Default Ctor.
		/// </summary>
		public McKeyAction()
		{
			InitializeComponent();
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		#endregion

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
           
		}

	

		#endregion

		#region IExtenderProvider Members

		/// <summary>
		/// Determine if KeyActionProvider support a component.
		/// </summary>
		/// <param name="extendee"></param>
		/// <returns></returns>
		public bool CanExtend(object extendee)
		{
			if ((extendee is System.Windows.Forms.TextBox) 
				|| (extendee is ComboBox)|| (extendee is McEditBase)) return true;

			return false;
		}

		#endregion

	}
}
