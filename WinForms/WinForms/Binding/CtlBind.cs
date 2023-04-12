using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

using System.Security.Permissions;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Text;
using System.Drawing.Design;
using System.Security;
using System.ComponentModel.Design;


using Nistec.Win32;

namespace Nistec.WinForms
{
	/// <summary>
	/// Summary description for BindControl.
	/// </summary>
	[ToolboxItem(false)]
	public class McBind : Control
	{
		private BindContext bindContext;
		private ControlBindCollection dataBindings;
		private McBind parent;
		
		public McBind()
		{
			this.bindContext=null;
		}


		[Browsable(false), Description("ControlBindingContext"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual BindContext BindContext
		{
			get
			{
				BindContext context = (BindContext) this.bindContext; //this.Properties.GetObject(PropBindingManager);
				if (context != null)
				{
					return context;
				}
				McBind parentInternal = this.ParentInternal;
				if ((parentInternal != null) && parentInternal.CanAccessProperties)
				{
					return parentInternal.BindContext;
				}
				this.bindContext = new BindContext();
				return this.bindContext;
			}
			set
			{
				if (this.bindContext != value)
				{
					this.bindContext=value;
					this.OnBindingContextChanged(EventArgs.Empty);
				}
			}
		}
 

		internal virtual McBind ParentInternal
		{
			get
			{
				return this.parent;
			}
			set
			{
				if (this.parent != value)
				{
					if (value != null)
					{
						value.Controls.Add(this);
					}
					else
					{
						this.parent=value;
						this.parent.Controls.Remove(this);
					}
				}
			}
		}
 
		internal virtual bool CanAccessProperties
		{
			get
			{
				return true;
			}
		}
 
		[Description("ControlBindings"), RefreshProperties(RefreshProperties.All), ParenthesizePropertyName(true), Category("Data"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public new ControlBindCollection DataBindings
		{
			get
			{
				if (dataBindings == null)
				{
					dataBindings = new ControlBindCollection(this);
				}
				return dataBindings;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnBindingContextChanged(EventArgs e)
		{
			if (dataBindings !=null)//  this.Properties.GetObject(Control.PropBindings) != null)
			{
				this.UpdateBindings();
			}
			base.OnBindingContextChanged(e);
		}
 
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnParentBindingContextChanged(EventArgs e)
		{
			this.OnBindingContextChanged(e);
		}

        //private bool CanRaiseEvents
        //{
        //    get
        //    {
        //        return true;
        //    }
        //}

 
		private void UpdateBindings()
		{
			for (int num1 = 0; num1 < this.DataBindings.Count; num1++)
			{
				BindContext.UpdateBinding(this.BindContext, this.DataBindings[num1] as Binder);
			}
		}

		internal static void UpdateBinding(BindContext newBindingContext, Binder binding)
		{
			BindManagerBase base1 = binding.BindManagerBase;
			if (base1 != null)
			{
				base1.Bindings.Remove(binding);
			}
			if (newBindingContext != null)
			{
				if (binding.BindToObject.BindManagerBase is BindPropertyManager)
				{
					BindContext.CheckPropertyBindingCycles(newBindingContext, binding);
				}
				BindToObject obj1 = binding.BindToObject;
				BindManagerBase base2 = newBindingContext.EnsureListManager(obj1.DataSource, obj1.BindingMemberInfo.BindingPath);
				base2.Bindings.Add(binding);
			}
		}

		#region Internal

		private short updateCount;

		public void BeginUpdateInternal()
		{
			if (this.IsHandleCreated)
			{
				if (this.updateCount == 0)
				{
					this.SendMessage(11, 0, 0);
				}
				this.updateCount = (short) (this.updateCount + 1);
			}
		}

		internal IntPtr SendMessage(int msg, int wparam, int lparam)
		{
			return WinMethods.SendMessage(new HandleRef(this, this.Handle), msg, wparam, lparam);
		}

		public bool EndUpdateInternal()
		{
			return this.EndUpdateInternal(true);
		}

 
		public bool EndUpdateInternal(bool invalidate)
		{
			if (this.updateCount <= 0)
			{
				return false;
			}
			this.updateCount = (short) (this.updateCount - 1);
			if (this.updateCount == 0)
			{
				this.SendMessage(11, -1, 0);
				if (invalidate)
				{
					this.Invalidate();
				}
			}
			return true;
		}

		
		[Description("CaptureInternalt"), Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CaptureInternal
		{
			get
			{
				if(this.Handle!= IntPtr.Zero)
				{
					return (WinMethods.GetCapture() == this.Handle);
				}
				//				if (this.window.Handle != IntPtr.Zero)
				//				{
				//					return (UnsafeNativeMethods.GetCapture() == this.window.Handle);
				//				}
				return false;
			}
			set
			{
				if (this.CaptureInternal != value)
				{
					if (value)
					{
						WinMethods.SetCapture(new HandleRef(this, this.Handle));
					}
					else
					{
						WinMethods.ReleaseCapture();
					}
				}
			}
		}

		public Graphics CreateGraphicsInternal()
		{
			return Graphics.FromHwndInternal(this.Handle);
		}

		public virtual bool FocusInternal()
		{
			if (this.CanFocus)
			{
				WinMethods.SetFocus(new HandleRef(this, this.Handle));
			}
			if (this.Focused && (this.Parent != null))//(this.ParentInternal != null))
			{
				//IContainerControl control1 = this.ParentInternal.GetContainerControlInternal();
				IContainerControl control1 = this.Parent.GetContainerControl();//ParentInternal.GetContainerControl();//.GetContainerControlInternal();
				if (control1 != null)
				{
					if (control1 is ContainerControl)
					{
						//((ContainerControl) control1).SetActiveControlInternal(this);
						control1.ActivateControl(this);
					}
					else
					{
						control1.ActiveControl = this;
					}
				}
			}
			return this.Focused;
		}

		#endregion

        [Browsable(true)]
        public override ContextMenu ContextMenu
        {
            get
            {
                return base.ContextMenu;
            }
            set
            {
                base.ContextMenu = value;
            }
        }
	}
}
