
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

using MControl.WinForms.Controls;
using MControl.Data;


namespace MControl.WinForms
{

 
	/// <summary>
	/// Provider field properties to controls that can be validated.
	/// </summary>
    [ProvideProperty("FieldName", typeof(Control)), ProvideProperty("MenuName", typeof(MenuItem)), ProvideProperty("StripItemName", typeof(ToolStripItem))]
    [ToolboxBitmap(typeof(McPermission), "Toolbox.Perms.bmp")]
	public class McPermission : System.ComponentModel.Component, IExtenderProvider//,ISupportInitialize
	{
		#region Members
		private Hashtable _controls= new Hashtable();
		private ActionMode actionMode=ActionMode.Auto;
        private Form form;

        public event EventHandler PermsValidated;
		#endregion

		#region "Properties"

        public Form CurrentForm
        {
            get { return form; }
            set { form = value; }
        }

        public ActionMode ActionMode
        {
            get { return actionMode; }
            set { actionMode = value; }
        }

        private PermsLevel _DefaultLevel = PermsLevel.FullControl;

        [Browsable(true)]
        public PermsLevel DefaultLevel
        {
            get { return _DefaultLevel; }
            set { _DefaultLevel = value; }
        }

        private PermsLevel _PermsLevel = PermsLevel.FullControl;

        [Browsable(true)]
        public PermsLevel PermsLevel
        {
            get { return _PermsLevel; }
        }

        IPerms perms;

        [Browsable(true)]
        public IPerms Perms
        {
            get { return perms; }
            set { perms = value; }
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
					_controls.Remove(control);
					//if(ActionMode==ActionMode.Auto  && control is Control)
					  //  ((Control)control).HandleCreated-=new EventHandler(McResource_HandleCreated);
				}
				else if(!this._controls.Contains(control)) 
				{
                    this._controls.Add(control, value);
					//if(ActionMode==ActionMode.Auto && control is Control)
					//	((Control)control).HandleCreated+=new EventHandler(McResource_HandleCreated);
				}
                else 
                {
                    this._controls[control] = value;
                    //if(ActionMode==ActionMode.Auto && control is Control)
                    //	((Control)control).HandleCreated+=new EventHandler(McResource_HandleCreated);
                }

			}
		}


		[DefaultValue(""), Category("Data")]
			//[Editor(typeof(MControl.WinForms.FieldActionRuleEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string GetFieldName(object control)
		{
			object text = _controls[control];
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
					_controls.Remove(control);
				}
                else if (!this._controls.Contains(control)) 
				{
                    this._controls.Add(control, value);
				}

			}
		}


		/// <summary>
		/// Get field rule.
		/// </summary>
		/// <param name="control"></param>
		/// <returns></returns>
		[DefaultValue(""), Category("Data")]
			//[Editor(typeof(MControl.WinForms.FieldActionRuleEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string GetMenuName(object control)
		{
            object text = _controls[control];
			if (text == null) 
			{
				text = string.Empty;
			}
			return text.ToString();
		}


        public void SetStripItemName(object control, string value)
		{

			if (control != null)
			{
				// only throw error in DesignMode
				if (base.DesignMode) 
				{
					if (!(control is ToolStripItem) ||  !this.CanExtend(control))
						throw new InvalidOperationException(control.GetType().ToString() 
							+ " is not supported by the field provider.");
				}
			
				if (value == null) 
				{
					value = string.Empty;
				}

				if (value.Length == 0) 
				{
					_controls.Remove(control);
				}
                else if (!this._controls.Contains(control)) 
				{
                    this._controls.Add(control, value);
				}

			}
		}


		/// <summary>
		/// Get field rule.
		/// </summary>
		/// <param name="control"></param>
		/// <returns></returns>
		[DefaultValue(""), Category("Data")]
			//[Editor(typeof(MControl.WinForms.FieldActionRuleEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string GetStripItemName(object control)
		{
            object text = _controls[control];
			if (text == null) 
			{
				text = string.Empty;
			}
			return text.ToString();
		}


		#endregion

        #region Perms

         public void SetPerms()
        {
            if (/*this.initialising ||*/ perms == null) { return; }

            SetPerms(Perms, DefaultLevel);

        }

        public void SetPerms(IPerms perm)
        {
            perms = perm;
            if (/*this.initialising ||*/ perms == null) { return; }

            SetPerms(Perms, DefaultLevel);

        }

        public virtual void SetPerms(IPerms perms, PermsLevel defaultLevel)
        {

  			if(/*this.initialising ||*/ this.DesignMode){return;}

            if (form!=null && actionMode == ActionMode.Auto)
            {
                _PermsLevel = perms.GetPermsLevel(form.Name, defaultLevel);
                PermissionsSettings(form);
            }
            else
            {
                foreach (DictionaryEntry d in this._controls)
                {
                    if ((d.Value != null) && !(d.Value.Equals("")))
                    {
                        if (d.Key is Control)
                        {
                            Control ctl = (Control)d.Key;
                            //if(ctl.IsHandleCreated)
                            //ctl.Text=rm.GetString(d.Value.ToString(),info);
                            SetPerms(ctl);
                        }
                        else if (d.Key is MenuItem)
                        {
                            //((MenuItem)d.Key).Text=rm.GetString(d.Value.ToString(),info);
                            //level = perms.GetPermsLevel(((MenuItem)d.Key).Name, DefaultLevel);
                            SetPerms(((MenuItem)d.Key));
                        }
                        else if (d.Key is ToolStripItem)
                        {
                            //((MenuItem)d.Key).Text=rm.GetString(d.Value.ToString(),info);
                            //level = perms.GetPermsLevel(((MenuItem)d.Key).Name, DefaultLevel);
                            SetPerms(((ToolStripItem)d.Key));
                        }
                    }
                }
            }
            OnPermsValidated(EventArgs.Empty);
        }

        protected virtual void OnPermsValidated(EventArgs e)
        {
            if (PermsValidated != null)
            {
                PermsValidated(this,e);
            }
        }

        public virtual void SetPerms(PermsLevel level)
        {
            _PermsLevel = level;
            if (this.form != null)
            {
                PermissionsSettings(this.form);
                OnPermsValidated(EventArgs.Empty);
            }
        }

        public virtual void SetPerms(Control c)
        {
            PermsLevel level = perms.GetPermsLevel(c.Name, DefaultLevel);
            SetPerms(c,level);
        }
        public virtual void SetPerms(MenuItem c)
        {
            PermsLevel level = perms.GetPermsLevel(c.Name, DefaultLevel);
            SetPerms(c, level);
        }

        public virtual void SetPerms(ToolStripItem c)
        {
            PermsLevel level = perms.GetPermsLevel(c.Name, DefaultLevel);
            SetPerms(c, level);
        }

        public virtual void SetPerms(Control c, PermsLevel level)
        {
            switch (level)
            {
                case PermsLevel.DenyAll:
                    c.Enabled = false;
                    break;
                case PermsLevel.EditOnly:
                case PermsLevel.ReadOnly:
                    if (c is IBind)
                    {
                        ((IBind)c).ReadOnly = true;
                    }
                    break;
                case PermsLevel.FullControl:
                    {
                        if (!c.Enabled)
                            c.Enabled = true;
                    }
                    break;
            }
        }


        public virtual void SetPerms(MenuItem c, PermsLevel level)
        {
            switch (level)
            {
                case PermsLevel.DenyAll:
                case PermsLevel.ReadOnly:
                    c.Enabled = false;
                    break;
                case PermsLevel.EditOnly:
                case PermsLevel.FullControl:
                    {
                        if (!c.Enabled)
                            c.Enabled = true;
                    }
                    break;
            }
        }

        public virtual void SetPerms(ToolStripItem c, PermsLevel level)
        {
            switch (level)
            {
                case PermsLevel.DenyAll:
                case PermsLevel.ReadOnly:
                    c.Enabled = false;
                    break;
                case PermsLevel.EditOnly:
                case PermsLevel.FullControl:
                    {
                        if (!c.Enabled)
                            c.Enabled = true;
                    }
                    break;
            }
        }

        protected virtual void PermissionsSettings(Control cc)
        {
            foreach (Control c in cc.Controls)
            {
                if (c is FormBox || c is McNavBar)
                {
                    continue;
                }
                else if (c is ContainerControl || c is McPanel || c is McGroupBox || c is McTabControl || c is McContainer)
                {
                    if (_PermsLevel == PermsLevel.DenyAll)
                    {
                        //if (!(c is McNavBar))//-- || c is McTaskBar || c is McTaskPanel))
                            c.Enabled = false;
                    }
                    else
                        PermissionsSettings(c);
                }
                else if (c is IBind)
                {
                    IBind cb = ((IBind)c);
                    switch (_PermsLevel)
                    {
                        case PermsLevel.DenyAll:
                            c.Enabled = false;
                            break;
                        case PermsLevel.EditOnly:
                        case PermsLevel.ReadOnly:
                            cb.ReadOnly = true;
                            break;
                        case PermsLevel.FullControl:
                            if (!c.Enabled)
                                c.Enabled = true;
                            if (cb.ReadOnly)
                                cb.ReadOnly = false;

                            break;
                    }
                }
                else if (c is IButton)
                {
                    IButton cb = ((IButton)c);
                    switch (_PermsLevel)
                    {
                        case PermsLevel.DenyAll:
                        case PermsLevel.EditOnly:
                        case PermsLevel.ReadOnly:
                            cb.Enabled = false;
                            break;
                        case PermsLevel.FullControl:
                            if (!c.Enabled)
                                c.Enabled = true;
                            break;
                    }
                }
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
		public McPermission(System.ComponentModel.IContainer container):this()
		{
			container.Add(this);

		}

		/// <summary>
		/// Default Ctor.
		/// </summary>
        public McPermission()
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
			if ((extendee is System.Windows.Forms.TextBox) 
				|| (extendee is Button)
				|| (extendee is Panel)
				|| (extendee is MenuItem)
                || (extendee is ToolStripItem)
                || (extendee is McPanel)
				|| (extendee is McButton)
                || (extendee is McGroupBox)
                || (extendee is IMcEdit)) return true;

			return false;
		}

		#endregion

		#region Ctor

        static McPermission()
		{
           
		}

		#endregion
 
		#region ISupportInitialize Members

        //private bool initialising=true;

        //public void BeginInit()
        //{
        //    this.initialising = true;
        //}

        //public void EndInit()
        //{
        //    this.initialising = false;
        //    SetPerms();
        //}

		#endregion
	}
}
