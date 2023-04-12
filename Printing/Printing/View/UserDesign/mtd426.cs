namespace MControl.Printing.View.Design.UserDesigner
{
    using MControl.Printing.View;
    using MControl.Printing.View.Design;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.ComponentModel.Design.Serialization;
    using System.IO;
    using System.Runtime.CompilerServices;

    [ProvideProperty("Name", typeof(IComponent))]
    internal class mtd426 : IDesignerHost, IServiceContainer, IServiceProvider, IContainer, IDisposable, IComponentChangeService, ITypeDescriptorFilterService, IExtenderProvider, IExtenderProviderService, IExtenderListService
    {
        private IServiceContainer _var0 = new ServiceContainer();
        private Stack _var1 = new Stack();
        //private Hashtable _var2 = new Hashtable(CaseInsensitiveHashCodeProvider.Default, CaseInsensitiveComparer.Default);
        private Hashtable _var2 = new Hashtable();// new myComparer());
        private IRootDesigner _var3;
        private ArrayList _var4 = new ArrayList();
        private IComponent _var5;
        private bool _var6 = true;

        public event EventHandler Activated;

        public event ComponentEventHandler ComponentAdded;

        public event ComponentEventHandler ComponentAdding;

        public event ComponentChangedEventHandler ComponentChanged;

        public event ComponentChangingEventHandler ComponentChanging;

        public event ComponentEventHandler ComponentRemoved;

        public event ComponentEventHandler ComponentRemoving;

        public event ComponentRenameEventHandler ComponentRename;

        public event EventHandler Deactivated;

        public event EventHandler LoadComplete;

        public event DesignerTransactionCloseEventHandler TransactionClosed;

        public event DesignerTransactionCloseEventHandler TransactionClosing;

        public event EventHandler TransactionOpened;

        public event EventHandler TransactionOpening;

        class myComparer : IEqualityComparer
        {
            public new bool Equals(object x, object y)
            {
                return x.Equals(y);
            }

            public int GetHashCode(object obj)
            {
                return obj.ToString().ToLower().GetHashCode();
            }
        }

        internal mtd426()
        {
            this._var0.AddService(typeof(INameCreationService), new mtd596());
            this._var0.AddService(typeof(IDesignerHost), this);
            this._var0.AddService(typeof(IContainer), this);
            this._var0.AddService(typeof(IComponentChangeService), this);
            this._var0.AddService(typeof(ITypeDescriptorFilterService), this);
            this._var0.AddService(typeof(IExtenderListService), this);
            this._var0.AddService(typeof(IExtenderProviderService), this);
            this.AddExtenderProvider(this);
            this._var0.AddService(typeof(ISelectionService), new mtd597());
            this._var0.AddService(typeof(IDesignerSerializationService), new mtd598());
        }

        internal void mtd172()
        {
            this._var5 = this.CreateComponent(typeof(MControl.Printing.View.Design.Report), null);
            this._var3 = (IRootDesigner) TypeDescriptor.CreateDesigner(this._var5, typeof(IRootDesigner));
            ((ReportViewDesigner)this._var3).mtd424 = true;
            PageHeader header = (PageHeader) this.CreateComponent(typeof(PageHeader), "PageHeader");
            ReportDetail detail = (ReportDetail) this.CreateComponent(typeof(ReportDetail), "ReportDetail");
            PageFooter footer = (PageFooter) this.CreateComponent(typeof(PageFooter), "PageFooter");
            header.Height = 36f;
            detail.Height = 192f;
            footer.Height = 36f;
            MControl.Printing.View.Design.Report report = (MControl.Printing.View.Design.Report) this._var5;
            report.ReportWidth = 576f;
            report.Sections.Add(header);
            report.Sections.Add(detail);
            report.Sections.Add(footer);
            this._var3.Initialize(this._var5);
            this._var6 = false;
            this.Activate();
            if (this.LoadComplete != null)
            {
                this.LoadComplete(this, new EventArgs());
            }
        }

        internal void mtd599(Stream var8)
        {
            this._var6 = true;
            this._var2.Clear();
            if (this._var5.Site.Name != "Report1")
            {
                this._var5.Site.Name = "Report1";
            }
            this._var2.Add(this._var5.Site.Name, this._var5);
            MControl.Printing.View.Design.Report report1 = (MControl.Printing.View.Design.Report) this._var5;
            PanelDesiger mtd = ((ReportViewDesigner)this._var3).mtd425;
            mtd.SuspendLayout();
            mtd.mtd499(ref var8, this);
            mtd.ResumeLayout(false);
            this._var6 = false;
        }

        internal void mtd599(string var7)
        {
            this._var6 = true;
            this._var2.Clear();
            if (this._var5.Site.Name != "Report1")
            {
                this._var5.Site.Name = "Report1";
            }
            this._var2.Add(this._var5.Site.Name, this._var5);
            MControl.Printing.View.Design.Report report1 = (MControl.Printing.View.Design.Report) this._var5;
            PanelDesiger mtd = ((ReportViewDesigner)this._var3).mtd425;
            mtd.SuspendLayout();
            mtd.mtd499(var7, this);
            mtd.ResumeLayout(false);
            this._var6 = false;
        }

        internal bool mtd600(string var9)
        {
            return this._var2.Contains(var9);
        }

        internal void mtd601(bool var10)
        {
            if (this.TransactionClosing != null)
            {
                this.TransactionClosing(this, new DesignerTransactionCloseEventArgs(var10,true));
            }
        }

        internal void mtd602(bool var10)
        {
            if (this.TransactionClosed != null)
            {
                this.TransactionClosed(this, new DesignerTransactionCloseEventArgs(var10,true));
            }
            this._var1.Pop();
        }

        internal void mtd603(object var16, string var17, string var18)
        {
            if (this._var2.Contains(var17))
            {
                this._var2.Remove(var17);
                this._var2.Add(var18, var16);
            }
            if ((this.ComponentRename != null) && !this._var6)
            {
                this.ComponentRename(this, new ComponentRenameEventArgs(var16, var17, var18));
            }
        }

        public void Activate()
        {
            if (this.Activated != null)
            {
                this.Activated(this, EventArgs.Empty);
            }
        }
        public void Deactivate()
        {
            if (this.Deactivated != null)
            {
                this.Deactivated(this, EventArgs.Empty);
            }
        }
        public void Add(IComponent var16, string var9)
        {
            if (var16 == null)
            {
                throw new ArgumentNullException("Cannot Add null component to the container.");
            }
            if ((var16.Site != null) && (var16.Site.Container != this))
            {
                var16.Site.Container.Remove(var16);
            }
            if ((var9 == null) || (var9.Length == 0))
            {
                var9 = ((INameCreationService) this.GetService(typeof(INameCreationService))).CreateName(this, var16.GetType());
            }
            if (this.mtd600(var9))
            {
                throw new ArgumentException("A component with this name already exists in the container.");
            }
            var16.Site = new mtd605(this, var16, var9);
            this.var11(this, new ComponentEventArgs(var16));
            if (var16 is IExtenderProvider)
            {
                ((IExtenderProviderService) this.GetService(typeof(IExtenderProviderService))).AddExtenderProvider((IExtenderProvider) var16);
            }
            this._var2.Add(var16.Site.Name, var16);
            this.var13(this, new ComponentEventArgs(var16));
        }

        public void AddExtenderProvider(IExtenderProvider var33)
        {
            if (!this._var4.Contains(var33))
            {
                this._var4.Add(var33);
            }
        }

        public void AddService(Type var22, ServiceCreatorCallback var24, bool var23)
        {
            this._var0.AddService(var22, var24, var23);
        }

        public bool CanExtend(object var32)
        {
            return (var32 is IComponent);
        }

        public IComponent CreateComponent(Type var19, string var9)
        {
            IComponent component = (IComponent) Activator.CreateInstance(var19);
            this.Add(component, var9);
            return component;
        }

        public DesignerTransaction CreateTransaction(string var21)
        {
            DesignerTransaction transaction = null;
            if ((this._var1.Count == 0) && (this.TransactionOpening != null))
            {
                this.TransactionOpening(this, EventArgs.Empty);
            }
            if (var21 == null)
            {
                transaction = new mtd604(this);
            }
            else
            {
                transaction = new mtd604(this, var21);
            }
            this._var1.Push(transaction);
            if (this.TransactionOpened != null)
            {
                this.TransactionOpened(this, EventArgs.Empty);
            }
            return transaction;
        }

        public void DestroyComponent(IComponent var16)
        {
            if (var16.Site.Container == this)
            {
                this.Remove(var16);
                var16.Dispose();
            }
        }

        public void Dispose()
        {
            foreach (string str in this._var2.Keys)
            {
                IComponent key = (IComponent) this._var2[str];
                this._var2.Remove(key);
                key.Dispose();
            }
            this._var2.Clear();
        }

        public bool FilterAttributes(IComponent var16, IDictionary var30)
        {
            IDesigner designer = this.GetDesigner(var16);
            if ((designer != null) && (designer is IDesignerFilter))
            {
                ((IDesignerFilter) designer).PreFilterAttributes(var30);
                ((IDesignerFilter) designer).PostFilterAttributes(var30);
            }
            return false;
        }

        public bool FilterEvents(IComponent var16, IDictionary var31)
        {
            IDesigner designer = this.GetDesigner(var16);
            if ((designer != null) && (designer is IDesignerFilter))
            {
                ((IDesignerFilter) designer).PreFilterEvents(var31);
                ((IDesignerFilter) designer).PostFilterEvents(var31);
            }
            return false;
        }

        public bool FilterProperties(IComponent var16, IDictionary var29)
        {
            IDesigner designer = this.GetDesigner(var16);
            if (designer is IDesignerFilter)
            {
                ((IDesignerFilter) designer).PreFilterProperties(var29);
                ((IDesignerFilter) designer).PostFilterProperties(var29);
            }
            return false;
        }

        public IDesigner GetDesigner(IComponent var16)
        {
            if (var16 is MControl.Printing.View.Design.Report)
            {
                return this._var3;
            }
            return null;
        }

        public IExtenderProvider[] GetExtenderProviders()
        {
            IExtenderProvider[] array = new IExtenderProvider[this._var4.Count];
            this._var4.CopyTo(array, 0);
            return array;
        }

        [ParenthesizePropertyName(true), Category("Design"), Description("The variable used to refer to this component in source code."), DesignOnly(true), Browsable(true)]
        public string GetName(IComponent var16)
        {
            if (var16.Site == null)
            {
                throw new InvalidOperationException("Component is not sited.");
            }
            return var16.Site.Name;
        }

        public object GetService(Type var22)
        {
            return this._var0.GetService(var22);
        }

        public Type GetType(string var20)
        {
            ITypeResolutionService service = (ITypeResolutionService) this.GetService(typeof(ITypeResolutionService));
            if (service != null)
            {
                return service.GetType(var20);
            }
            return Type.GetType(var20);
        }

        public void OnComponentChanged(object var16, MemberDescriptor var26, object var27, object var28)
        {
            if ((this.ComponentChanged != null) && !this._var6)
            {
                this.ComponentChanged(this, new ComponentChangedEventArgs(var16, var26, var27, var28));
            }
        }

        public void OnComponentChanging(object var16, MemberDescriptor var26)
        {
            if ((this.ComponentChanging != null) && !this._var6)
            {
                this.ComponentChanging(this, new ComponentChangingEventArgs(var16, var26));
            }
        }

        public void Remove(IComponent var16)
        {
            if ((var16 != null) && (var16.Site != null))
            {
                this.var14(this, new ComponentEventArgs(var16));
                if (var16 is IExtenderProvider)
                {
                    ((IExtenderProviderService) this.GetService(typeof(IExtenderProviderService))).RemoveExtenderProvider((IExtenderProvider) var16);
                }
                this._var2.Remove(var16.Site.Name);
                this.var15(this, new ComponentEventArgs(var16));
                var16.Site = null;
            }
        }

        public void RemoveExtenderProvider(IExtenderProvider var33)
        {
            if (this._var4.Contains(var33))
            {
                this._var4.Remove(var33);
            }
        }

        public void RemoveService(Type var22, bool var23)
        {
            this._var0.RemoveService(var22, var23);
        }

        public void SetName(IComponent var16, string var9)
        {
            if (var16.Site == null)
            {
                throw new InvalidOperationException("Component is not sited.");
            }
            var16.Site.Name = var9;
        }

        private void var11(object var12, ComponentEventArgs e)
        {
            if ((this.ComponentAdding != null) && !this._var6)
            {
                this.ComponentAdding(var12, e);
            }
        }

        private void var13(object var12, ComponentEventArgs e)
        {
            if ((this.ComponentAdded != null) && !this._var6)
            {
                this.ComponentAdded(var12, e);
            }
        }

        private void var14(object var12, ComponentEventArgs e)
        {
            if (this.ComponentRemoving != null)
            {
                this.ComponentRemoving(var12, e);
            }
        }

        private void var15(object var12, ComponentEventArgs e)
        {
            if (this.ComponentRemoved != null)
            {
                this.ComponentRemoved(var12, e);
            }
        }

        IComponent IDesignerHost.CreateComponent(Type var19)
        {
            IComponent component = (IComponent) Activator.CreateInstance(var19);
            this.Add(component, null);
            return component;
        }

        DesignerTransaction IDesignerHost.CreateTransaction()
        {
            return this.CreateTransaction(null);
        }

        void IServiceContainer.AddService(Type var22, ServiceCreatorCallback var24)
        {
            this._var0.AddService(var22, var24);
        }

        void IServiceContainer.AddService(Type var22, object var25)
        {
            this._var0.AddService(var22, var25);
        }

        void IServiceContainer.AddService(Type var22, object var25, bool var23)
        {
            this._var0.AddService(var22, var25, var23);
        }

        void IServiceContainer.RemoveService(Type var22)
        {
            this._var0.RemoveService(var22);
        }

        void IContainer.Add(IComponent var16)
        {
            this.Add(var16, null);
        }

        internal PanelDesiger mtd425
        {
            get
            {
                return ((ReportViewDesigner)this._var3).mtd425;
            }
        }

        public ComponentCollection Components
        {
            get
            {
                IComponent[] array = new IComponent[this._var2.Count];
                if (this._var2.Count > 0)
                {
                    this._var2.Values.CopyTo(array, 0);
                }
                return new ComponentCollection(array);
            }
        }

        public IContainer Container
        {
            get
            {
                return this;
            }
        }

        public bool InTransaction
        {
            get
            {
                return (this._var1.Count > 0);
            }
        }

        public bool IsLoading
        {
            get
            {
                return this._var6;
            }
        }

        public bool Loading
        {
            get
            {
                return this._var6;
            }
        }

        public IComponent RootComponent
        {
            get
            {
                return this._var5;
            }
        }

        public string RootComponentClassName
        {
            get
            {
                if (this._var5 != null)
                {
                    return this._var5.GetType().Name;
                }
                return null;
            }
        }

        public string TransactionDescription
        {
            get
            {
                if (this.InTransaction)
                {
                    DesignerTransaction transaction = (DesignerTransaction) this._var1.Peek();
                    return transaction.Description;
                }
                return null;
            }
        }
    }
}

