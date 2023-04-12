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

using mControl.Util;
using mControl.Win32;

namespace mControl.WinCtl.Controls
{
	/// <summary>
	/// Summary description for BindControl.
	/// </summary>
	[ToolboxItem(false)]
	public class CtlBindControl:Control
	{
		private BindContext bindContext;
		private ControlBindCollection dataBindings;
		
		[Category("CatPropertyChanged"), Description("ControlOnBindingContextChanged")]
		public new event EventHandler BindingContextChanged;
	
		
//		[Category("CatPropertyChanged"), Description("ControlOnBindingContextChanged")]
//		public event EventHandler BindingContextChanged
//		{
//			add
//			{
//				base.Events.AddHandler(Control.EventBindingContext, value);
//			}
//			remove
//			{
//				base.Events.RemoveHandler(Control.EventBindingContext, value);
//			}
//		}
// 



		public CtlBindControl()
		{
			this.bindContext=null;
		}

//		public BindManager GetBindManager(object dataSource,string dataMember) 
//		{
//			BindingManagerBase bm = (BindingManagerBase) this.BindingContext[dataSource, dataMember];
//			return BindManager.GetBindManager(dataSource,dataMember, bm);
//		}


		[Description("ControlBindingContext"), Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new BindContext BindingContext
		{
			get
			{
				if(this.bindContext==null)
				{
					this.bindContext=new BindContext();
					//this.OnBindingContextChanged(EventArgs.Empty);

					//base.BindingContext= new BindingContext();
					//return this.bindContext;
				}
				return this.bindContext;// base.BindingContext as BindContext;
//				BindContext context1 =bindContext;// (BindContext) this.Properties.GetObject(Control.PropBindingManager);
//				if (context1 != null)
//				{
//					return context1;
//				}
//				CtlBindControl control1 = this.Parent as CtlBindControl;
//				if (control1 != null) //&& control1.CanAccessProperties)
//				{
//					return control1.BindingContext;
//				}
//				return null;
			}
			set
			{
				if(bindContext != value)
				{
					this.bindContext=value;
					//base.BindingContext=value;
					this.OnBindingContextChanged(EventArgs.Empty);
				}
//				if (this.Properties.GetObject(Control.PropBindingManager) != value)
//				{
//					this.Properties.SetObject(Control.PropBindingManager, value);
//					
//					this.OnBindingContextChanged(EventArgs.Empty);
//				}
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
			base.OnBindingContextChanged(e);
			if (dataBindings !=null)//  this.Properties.GetObject(Control.PropBindings) != null)
			{
				this.UpdateBindings();
			}
			if (this.CanRaiseEvents)
			{
				if(this.BindingContextChanged!=null)
					this.BindingContextChanged(this,EventArgs.Empty);
//				EventHandler handler1 = base.Events[Control.EventBindingContext] as EventHandler;
//				if (handler1 != null)
//				{
//					handler1(this, e);
//				}
			}
			Control.ControlCollection collection1 =this.Controls;// Control.ControlCollection;// this.Properties.GetObject(Control.PropControlsCollection);
			
			if (collection1 != null)
			{
				for (int num1 = 0; num1 < collection1.Count; num1++)
				{
					//if(collection1[num1] is CtlBindControl)
					//((CtlBindControl)collection1[num1]).OnParentBindingContextChanged(e);
					//base.OnBindingContextChanged(e);
				}
			}
			//base.OnBindingContextChanged(e);
		}

 
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnParentBindingContextChanged(EventArgs e)
		{
			base.OnBindingContextChanged(e);

//			if (this.Properties.GetObject(Control.PropBindingManager) == null)
//			{
//				this.OnBindingContextChanged(e);
//			}
		}

		private bool CanRaiseEvents
		{
			get
			{
//				if (this.IsActiveX)
//				{
//					return !this.ActiveXEventsFrozen;
//				}
				return true;
			}
		}

 
		private void UpdateBindings()
		{
			for (int num1 = 0; num1 < this.DataBindings.Count; num1++)
			{
				BindContext.UpdateBinding(this.BindingContext, this.DataBindings[num1] as Binder);
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
					BindContext.CheckPropertyBindCycles(newBindingContext, binding);
				}
				BindToObject obj1 = binding.BindToObject;
				BindManagerBase base2 = newBindingContext.EnsureBindListManager(obj1.DataSource, obj1.BindingMemberInfo.BindingPath);
				base2.Bindings.Add(binding);
			}
		}

		#region properties

		//		[Description("ControlBindingContextDescr"), Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		//		public override BindingContext BindingContext
		//		{
		//			get
		//			{
		//				BindContext context1 = (BindContext) base.BindingContext;//this.Properties.GetObject(Control.PropBindingManager);
		//				if (context1 != null)
		//				{
		//					return context1;
		//				}
		//				Control control1 = this.ParentInternal;
		//				if (control1 != null) //&& control1.CanAccessProperties)
		//				{
		//					return control1.BindingContext;
		//				}
		//				return null;
		//			}
		//			set
		//			{
		//				//if (this.Properties.GetObject(Control.PropBindingManager) != value)
		//				//{
		//				//	this.Properties.SetObject(Control.PropBindingManager, value);
		//					
		//					base.BindingContext=value;
		//					this.OnBindingContextChanged(EventArgs.Empty);
		//				//}
		//			}
		//		}
		// 
		//		[EditorBrowsable(EditorBrowsableState.Advanced)]
		//		protected virtual void OnBindContextChanged(EventArgs e)
		//		{
		//			if (this.BindingContext!=null)//(this.Properties.GetObject(Control.PropBindings) != null)
		//			{
		//				this.UpdateBindings();
		//			}
		//			if(this.BindingContextChanged!=null)
		//			{
		//				this.BindingContextChanged(this,e);
		//			}
		////			if (this.CanRaiseEvents)
		////			{
		////				EventHandler handler1 = base.Events[Control.EventBindingContext] as EventHandler;
		////				if (handler1 != null)
		////				{
		////					handler1(this, e);
		////				}
		////			}
		//
		//			BindControl.ControlCollection collection1 =new BindControl.ControlCollection(this);// (BindControl.ControlCollection) this.Properties.GetObject(BindControl.PropControlsCollection);
		//			if (collection1 != null)
		//			{
		//				for (int num1 = 0; num1 < collection1.Count; num1++)
		//				{
		//					//-collection1[num1].OnParentBindingContextChanged(e);
		//				}
		//			}
		//		}
		//
		//		private void UpdateBindings()
		//		{
		//			for (int num1 = 0; num1 < this.DataBindings.Count; num1++)
		//			{
		//				BindContext.UpdateBinding((BindContext)this.BindingContext, this.DataBindings[num1]);
		//			}
		//		}
		//
		//		[EditorBrowsable(EditorBrowsableState.Advanced)]
		//		protected override void OnParentBindingChanged(EventArgs e)
		//		{
		//			if (this.Properties.GetObject(Control.PropBindingManager) == null)
		//			{
		//				this.OnBindingContextChanged(e);
		//			}
		//		}

 


		//		public new event EventHandler BindingContextChanged;
		//public new event EventHandler ParentBindingChanged;
 
		//		[Category("PropertyChanged"), Description("ControlOnBindingContextChanged")]
		//		public event EventHandler BindingContextChanged
		//		{
		//			add
		//			{
		//				base.Events.AddHandler(Control.EventBindingContext, value);
		//			}
		//			remove
		//			{
		//				base.Events.RemoveHandler(Control.EventBindingContext, value);
		//			}
		//		}
 
		#endregion

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

//		public virtual Control ParentInternal
//		{
//			get
//			{
//				return this.Parent;
//			}
//			set
//			{
//				if (this.Parent != value)
//				{
//					if (value != null)
//					{
//						value.Controls.Add(this);
//					}
//					else
//					{
//						this.Parent.Controls.Remove(this);
//					}
//				}
//			}
//		}

		//		internal string GetListName()
		//		{
		//			if (this.listManager.List is ITypedList)
		//			{
		//				return ((ITypedList) this.listManager.List).GetListName(null);
		//			}
		//			return this.GetType().ToString();// this.finalType.Name;
		//		}
		//
		//		private string GetListName(ArrayList listAccessors)
		//		{
		//			if (this.listManager.List is ITypedList)
		//			{
		//				PropertyDescriptor[] descriptorArray1 = new PropertyDescriptor[listAccessors.Count];
		//				listAccessors.CopyTo(descriptorArray1, 0);
		//				return ((ITypedList) this.listManager.List).GetListName(descriptorArray1);
		//			}
		//			return "";
		//		}
 
		//		internal void SetActiveControlInternal(Control value)
		//		{
		//			if ((this.activeControl != value) || ((value != null) && !value.Focused))
		//			{
		//				bool flag1;
		//				if ((value != null) && !base.Contains(value))
		//				{
		//					throw new ArgumentException(SR.GetString("CannotActivateControl"));
		//				}
		//				ContainerControl control1 = this;
		//				if ((value != null) && (value.ParentInternal != null))
		//				{
		//					control1 = value.ParentInternal.GetContainerControlInternal() as ContainerControl;
		//				}
		//				if (control1 != null)
		//				{
		//					flag1 = control1.ActivateControlInternal(value, false);
		//				}
		//				else
		//				{
		//					flag1 = this.AssignActiveControlInternal(value);
		//				}
		//				if ((control1 != null) && flag1)
		//				{
		//					ContainerControl control2 = this;
		//					while ((control2.ParentInternal != null) && (control2.ParentInternal.GetContainerControlInternal() is ContainerControl))
		//					{
		//						control2 = control2.ParentInternal.GetContainerControlInternal() as ContainerControl;
		//					}
		//					if (control2.ContainsFocus && (((value == null) || !(value is UserControl)) || ((value is UserControl) && !((UserControl) value).HasFocusableChild())))
		//					{
		//						control1.FocusActiveControlInternal();
		//					}
		//				}
		//			}
		//		}
		//
		//		internal IContainerControl GetContainerControlInternal()
		//		{
		//			Control control1 = this;
		//			while ((control1 != null) && !Control.IsFocusManagingContainerControl(control1))
		//			{
		//				control1 = control1.ParentInternal;
		//			}
		//			return (IContainerControl) control1;
		//		}
		//


		#endregion

	}
}
