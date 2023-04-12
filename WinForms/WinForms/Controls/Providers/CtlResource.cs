
using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using System.Reflection;
using System.ComponentModel.Design;
using System.Globalization;
using System.Resources;

using Nistec.WinForms.Controls;
using Nistec.Win;


namespace Nistec.WinForms
{

    public enum ActionMode
    {
        Auto,
        Manual
    }

	/// <summary>
	/// Provider field properties to controls that can be validated.
	/// </summary>
	[ProvideProperty("FieldName", typeof(Control)),ProvideProperty("MenuName",typeof(MenuItem))]
	[ToolboxBitmap(typeof(McResource), "Toolbox.Resource.bmp")]
	public class McResource : System.ComponentModel.Component, IExtenderProvider,ISupportInitialize
	{
		#region Members
		private Hashtable					_FieldResources			= new Hashtable();
		private Hashtable					_Cultures				= new Hashtable();
		private ResourceManager				resources;
		private string				        resourcesName="";
		private string				        culture	="CurrentCulture";
		private string[]					cultureSupport=new string[]{"en"};
		public 	CultureInfo					_CultureInfo;
		private ActionMode					actionMode=ActionMode.Auto;
        private Form form;
		public readonly static				CultureInfo DefualtCulture;

		#endregion

		#region "Properties"

        public Form CurrentForm
        {
            get { return form; }
            set { form = value; }
        }

		[Browsable(true),Description("Define Resourse Culture supported")]
		public string[] CultureSupport
		{
			get{return cultureSupport;}
			set
			{
				if(cultureSupport!=value)
				{
					if(value== null || value.Length==0)
						cultureSupport= new string[]{"en"};
					else
						cultureSupport=value;

					SetCulters();
				}
			}
		}

		[Browsable(true),Description("Define Resourse Culture"),DefaultValue("CurrentCulture")]
		public string CultureName
		{
			get{return culture;}
			set
			{
               
				if(value== null || value =="")
					value="CurrentCulture";
				if(culture !=value)
				{
					culture=value;
					GetCurrentCulture();
				}
			}
		}

		private CultureInfo GetCurrentCulture()
		{
			CultureInfo ci=CultureInfo.CurrentCulture;

			if(culture.Equals("CurrentCulture"))
				_CultureInfo= CultureInfo.CurrentCulture;
		    else if(culture.Equals(""))
				_CultureInfo= CultureInfo.CurrentCulture;
	        else
				_CultureInfo = new CultureInfo(culture,false);

			if(!isCultureSopperted(_CultureInfo.Name))
			{
				_CultureInfo= McResource.DefualtCulture;
			}
			return _CultureInfo;
		}

        public ActionMode ActionMode
        {
            get { return actionMode; }
            set { actionMode = value; }
        }

		public string ResourceBaseName
		{
			get{return resourcesName;}
			set
			{
				if(value==null)
					value="";
				resourcesName=value;
			}
		}

		//[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(false)]
		public ResourceManager ResourceManager
		{
			get{return resources;}
			set{resources=value;}
		}


		private void SetCulters()
		{
			_Cultures.Clear();

			if (cultureSupport == null || cultureSupport.Length == 0)
			{
             return;
			}

			try
			{
				for(int i=0;i<cultureSupport.Length;i++)
				{
					_Cultures.Add(cultureSupport[i],cultureSupport[i]);
				}
			}
			catch(Exception ex)
			{
              MsgBox.ShowError(ex.Message); 
			}
		}

		public bool isCultureSopperted(string clt)
		{
			return _Cultures.Contains(clt);
		}

		#endregion

		#region Extended Properties

		public void SetFieldName(object control, string value)
		{

			if (control != null)
			{
				// only throw error in DesignMode
				if (base.DesignMode) 
				{
					if ((control is MenuItem) || !this.CanExtend(control))
						throw new InvalidOperationException(control.GetType().ToString() 
							+ " is not supported by the field provider.");
				}
			
				if (value == null) 
				{
					value = string.Empty;
				}

				if (value.Length == 0) 
				{
					_FieldResources.Remove(control);
					//if(ActionMode==ActionMode.Auto  && control is Control)
					  //  ((Control)control).HandleCreated-=new EventHandler(McResource_HandleCreated);
				}
				else if(!this._FieldResources.Contains(control)) 
				{
					this._FieldResources.Add(control, value);
					//if(ActionMode==ActionMode.Auto && control is Control)
					//	((Control)control).HandleCreated+=new EventHandler(McResource_HandleCreated);
				}
                else 
                {
                    this._FieldResources[control]= value;
                    //if(ActionMode==ActionMode.Auto && control is Control)
                    //	((Control)control).HandleCreated+=new EventHandler(McResource_HandleCreated);
                }

			}
		}


		[DefaultValue(""), Category("Data")]
			//[Editor(typeof(Nistec.WinForms.FieldActionRuleEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string GetFieldName(object control)
		{
			object text = _FieldResources[control];
			if (text == null) 
			{
				text = string.Empty;
			}
			return text.ToString();
		}

		public void SetMenuName(object control, string value)
		{

			if (control != null)
			{
				// only throw error in DesignMode
				if (base.DesignMode) 
				{
					if (!(control is MenuItem) ||  !this.CanExtend(control))
						throw new InvalidOperationException(control.GetType().ToString() 
							+ " is not supported by the field provider.");
				}
			
				if (value == null) 
				{
					value = string.Empty;
				}

				if (value.Length == 0) 
				{
					_FieldResources.Remove(control);
				}
				else if(!this._FieldResources.Contains(control)) 
				{
					this._FieldResources.Add(control, value);
				}

			}
		}


		/// <summary>
		/// Get field rule.
		/// </summary>
		/// <param name="control"></param>
		/// <returns></returns>
		[DefaultValue(""), Category("Data")]
			//[Editor(typeof(Nistec.WinForms.FieldActionRuleEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string GetMenuName(object control)
		{
			object text = _FieldResources[control];
			if (text == null) 
			{
				text = string.Empty;
			}
			return text.ToString();
		}


		// not used
//		private void McResource_HandleCreated(object sender, EventArgs e)
//		{
//			try
//			{
//				if(ActionMode==ActionMode.Auto)
//				{
//					Control ctl=(Control)sender;
//			
//					string field=this._FieldResources[ctl].ToString();
//					ctl.Text= GetResourceString(field);
//					ctl.HandleCreated-=new EventHandler(McResource_HandleCreated);
//				}
//
//			}
//			catch(Exception ex)
//			{
//				MsgBox.ShowError(ex.Message);
//			}
//		}

		#endregion

		#region Action Methods

		public void SetFileds()
		{
			if(this.initialising){return;}
			if(resources==null)
			{
				SetResourceManager();
			}

			SetFileds(resources,_CultureInfo);
		}

		public void SetFileds(ResourceManager rm )
		{
			if(this.initialising){return;}
			SetFileds(rm,_CultureInfo);
		}
		public void SetFileds(ResourceManager rm ,string cultureInfo )
		{
			if(this.initialising){return;}
			CultureInfo info=new CultureInfo(cultureInfo,false);
			SetFileds(rm,info);
		}

		public void SetFileds(ResourceManager rm ,CultureInfo info)
		{
			if(this.initialising || this.DesignMode){return;}

            if (form!=null && actionMode == ActionMode.Auto)
            {
                FieldSettings(form,info);

            }
            else
            {
                foreach (DictionaryEntry d in this._FieldResources)
                {
                    if ((d.Value != null) && !(d.Value.Equals("")))
                    {
                        if (d.Key is Control)
                        {
                            Control ctl = (Control)d.Key;
                            //if(ctl.IsHandleCreated)
                            //ctl.Text=rm.GetString(d.Value.ToString(),info);
                            ctl.Text = this.GetString(info, d.Value.ToString());
                        }
                        else if (d.Key is MenuItem)
                        {
                            //((MenuItem)d.Key).Text=rm.GetString(d.Value.ToString(),info);
                            ((MenuItem)d.Key).Text = this.GetString(info, d.Value.ToString());
                        }
                    }
                }
            }
		}


        protected virtual void FieldSettings(Control cc, CultureInfo info)
        {
            cc.Text = this.GetString(info, cc.Name, cc.Text);

            foreach (Control c in cc.Controls)
            {
                if (c is ContainerControl || c is McPanel || c is McGroupBox || c is McTabControl)
                {
                    FieldSettings(c, info);
                }
                else if (this.CanExtend(c))
                {
                    c.Text = this.GetString(info, c.Name, c.Text);
                }
            }
        }
 
//		public string GetString(ResourceManager rm ,string culterInfo, string text)
//		{
//			CultureInfo info=new CultureInfo(cultureInfo,false);
//			return rm.GetString(text,info);
//		}
//		
//		public string GetString(ResourceManager rm ,string text)
//		{
//			return rm.GetString(text,CultureInfo.CurrentCulture);
//		}
//
//		public string GetString(string text)
//		{
//			if(resources==null)
//			{
//				SetResourceManager();
//			}
//			
//			if(resources!=null)
//			{
//				return resources.GetString(text,CultureInfo.CurrentCulture);
//			}
//			return text;
//		}

		public void SetResourceManager()  
		{  
			if(this.resourcesName=="")
			{
              MessageBox.Show("ResourceBaseName not define","Nistec");
			}
			else
			{
				SetResourceManager(this.resourcesName,Assembly.GetEntryAssembly());
			}
		}

		public void SetResourceManager(string resourcesName)  
		{  
			SetResourceManager(resourcesName,Assembly.GetCallingAssembly());//GetEntryAssembly());
		}

		public void SetResourceManager(string resourcesName,System.Reflection.Assembly assembly)  
		{  
			try
			{
                if (this.resources == null && assembly!=null)  
				{  
					this.resourcesName=resourcesName;
					resources = new ResourceManager(this.resourcesName,assembly);  
					//this.resources = new ResourceManager(resourcesNamespace, base.GetType().Module.Assembly);
				} 
			}
			catch(Exception ex)
			{
				throw ex;
			}
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
		public McResource(System.ComponentModel.IContainer container):this()
		{
			container.Add(this);
			//InitializeComponent();

		}

		/// <summary>
		/// Default Ctor.
		/// </summary>
		public McResource()
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
//				foreach (DictionaryEntry d in this._FieldResources)
//				{
//					if((d.Value!=null) && !(d.Value.Equals("")))
//					{
//						if(d.Key is Control)
//						{
//							Control ctl=(Control)d.Key;
//							ctl.HandleCreated-=new EventHandler(McResource_HandleCreated);
//						}
//					}
//				}

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
		/// Determine if FieldActionProvider support a component.
		/// </summary>
		/// <param name="extendee"></param>
		/// <returns></returns>
		public bool CanExtend(object extendee)
		{
			if ((extendee is System.Windows.Forms.Label) 
				|| (extendee is Button)
				|| (extendee is Panel)
				|| (extendee is MenuItem)
				|| (extendee is McPanel)
				|| (extendee is McButton)
                || (extendee is McGroupBox)
                || (extendee is McBase)) return true;

			return false;
		}

		#endregion

		#region Ctor
//		//private ResourceManager resources;
//		private static McResource loader;
//
//
		static McResource()
		{
			McResource.DefualtCulture=new CultureInfo("en",false);
			//McResource.loader = null;
		}

		#endregion
 
		#region RM Methods

		public bool GetBoolean(string name)
		{
			return GetBoolean(null, name);
		}

 
		public  bool GetBoolean(CultureInfo culture, string name)
		{
			bool flag1 = false;
			if (resources != null)
			{
				object obj1 = resources.GetObject(name, culture);
				if (obj1 is bool)
				{
					flag1 = (bool) obj1;
				}
			}
			return flag1;
		}

		public  byte GetByte(string name)
		{
			return GetByte(null, name);
		}

		public  byte GetByte(CultureInfo culture, string name)
		{
			byte num1 = 0;
			if (resources != null)
			{
				object obj1 = resources.GetObject(name, culture);
				if (obj1 is byte)
				{
					num1 = (byte) obj1;
				}
			}
			return num1;
		}

		public char GetChar(string name)
		{
			return GetChar(null, name);
		}

		public char GetChar(CultureInfo culture, string name)
		{
			char ch1 = '\0';
			if (resources != null)
			{
				object obj1 = resources.GetObject(name, culture);
				if (obj1 is char)
				{
					ch1 = (char) obj1;
				}
			}
			return ch1;
		}

 
		public double GetDouble(string name)
		{
			return GetDouble(null, name);
		}

 
		public double GetDouble(CultureInfo culture, string name)
		{
			double num1 = 0;
			if (resources != null)
			{
				object obj1 = resources.GetObject(name, culture);
				if (obj1 is double)
				{
					num1 = (double) obj1;
				}
			}
			return num1;
		}

		public float GetFloat(string name)
		{
			return GetFloat(null, name);
		}

 
		public float GetFloat(CultureInfo culture, string name)
		{
			float single1 = 0f;
			if (resources != null)
			{
				object obj1 = resources.GetObject(name, culture);
				if (obj1 is float)
				{
					single1 = (float) obj1;
				}
			}
			return single1;
		}

 
		public int GetInt(string name)
		{
			return GetInt(null, name);
		}

		public int GetInt(CultureInfo culture, string name)
		{
			int num1 = 0;
			if (resources != null)
			{
				object obj1 = resources.GetObject(name, culture);
				if (obj1 is int)
				{
					num1 = (int) obj1;
				}
			}
			return num1;
		}

		public long GetLong(string name)
		{
			return GetLong(null, name);
		}

		public long GetLong(CultureInfo culture, string name)
		{
			long num1 = 0;
			if (resources != null)
			{
				object obj1 = resources.GetObject(name, culture);
				if (obj1 is long)
				{
					num1 = (long) obj1;
				}
			}
			return num1;
		}

		public object GetObject(string name)
		{
			return GetObject(null, name);
		}

		public object GetObject(CultureInfo culture, string name)
		{
			if (resources == null)
			{
				return null;
			}
			return resources.GetObject(name, culture);
		}

		public short GetShort(string name)
		{
			return GetShort(null, name);
		}

		public short GetShort(CultureInfo culture, string name)
		{
			short num1 = 0;
			if (resources != null)
			{
				object obj1 = resources.GetObject(name, culture);
				if (obj1 is short)
				{
					num1 = (short) obj1;
				}
			}
			return num1;
		}

 
		public string GetString(string name)
		{
			return GetString(null, name);
		}

 
		public string GetString(CultureInfo culture, string name)
		{
			try
			{
				if (resources == null)
				{
					return null;
				}
				if(culture==null)
				{
					culture=_CultureInfo;// CultureInfo.CurrentCulture;
				}
				return resources.GetString(name, culture);
			}
			catch//(Exception ex)
			{
				try
				{
					return resources.GetString(name, _CultureInfo);
				}
				catch//(Exception ex1)
				{
					return name;
				}
			}
		}

        public string GetString(CultureInfo culture, string name,string valueIfNull)
        {
            try
            {
                if (resources == null)
                {
                    return valueIfNull;
                }
                if (culture == null)
                {
                    culture = _CultureInfo;// CultureInfo.CurrentCulture;
                }
                return resources.GetString(name, culture);
            }
            catch//(Exception ex)
            {
                try
                {
                    return resources.GetString(name, _CultureInfo);
                }
                catch//(Exception ex1)
                {
                    return valueIfNull;
                }
            }
        }

		public string GetString(string name, params object[] args)
		{
			return GetString(null, name, args);
		}

 
		public string GetString(CultureInfo culture, string name, params object[] args)
		{
			if (resources == null)
			{
				return null;
			}
			string text1 = name;
			try
			{
				text1 = resources.GetString(name, culture);
			}
			catch
			{
				try
				{
					text1= resources.GetString(name, _CultureInfo);
				}
				catch
				{
					text1 = name;
				}
			}

			if ((args != null) && (args.Length > 0))
			{
				return string.Format(text1, args);
			}
			return text1;
		}

		#endregion

		#region MsgBox

		public DialogResult ShowQuestion(string name)
		{
			return MsgBox.ShowQuestion(GetString(null, name));
		}

		public void ShowInfo(string name)
		{
			MsgBox.ShowInfo(GetString(null, name));
		}

		public void ShowWarning(string name)
		{
			MsgBox.ShowWarning(GetString(null, name));
		}

		public void ShowError(string name)
		{
			MsgBox.ShowError(GetString(null, name));
		}

		#endregion

		#region ISupportInitialize Members

		private bool initialising=true;

		public void BeginInit()
		{
			this.initialising = true;
		}

		public void EndInit()
		{
			this.initialising = false;
            GetCurrentCulture();
			if(this.resourcesName!="")
			{
				SetFileds();
			}
		}

		#endregion
	}
}
