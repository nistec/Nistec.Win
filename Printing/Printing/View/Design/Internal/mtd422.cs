namespace MControl.Printing.View.Design
{
    using MControl.Printing.View;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.ComponentModel.Design.Serialization;
    using System.Drawing;
    using System.Drawing.Design;
    using System.IO;
    using System.Reflection;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Windows.Forms;
    using System.Windows.Forms.Design;

    //mtd422
    internal class ReportViewDesigner : ComponentDesigner, IRootDesigner, IDesigner, IDisposable, IToolboxUser
    {
        private IDesignerHost _var0;
        private MenuCommand[] _var1;
        private Splitter _var10;
        private var11 _var12;
        private DesignerVerb _var13;
        private IMenuCommandService _var2;
        private ISelectionService _var3;
        private IComponentChangeService _var4;
        private IToolboxService _var5;
        private ImageList _var6;
        private bool _var7 = false;
        private Control _var8;
        private PanelDesiger _var9;


        public ReportViewDesigner()
        {
            //this._Flag = false;

#if(CLIENT)
		 throw new Exception("Invalid MControl.Client.ReportView License Key");
#else
            MControl.Net.WinRptNet.NetFram("ReportViewDesigner", "DSN");
#endif

        }

        protected override void Dispose(bool var21)
        {
            if (var21)
            {
                if (this._var9 != null)
                {
                    this._var9.Dispose();
                }
                this.var22();
                this.var23();
                this.var24();
            }
            base.Dispose(var21);
        }

        public override void Initialize(IComponent var14)
        {
            base.Initialize(var14);
            this.var15();
            this._var3.SetSelectedComponents(new object[] { var14 }, SelectionTypes.Replace);
            this.var16();
            this.var17();
            this.var18();
            this.var19();
        }

        private void var15()
        {
            this._var0 = (IDesignerHost) this.GetService(typeof(IDesignerHost));
            this._var3 = (ISelectionService) this.GetService(typeof(ISelectionService));
            this._var4 = (IComponentChangeService) this.GetService(typeof(IComponentChangeService));
        }

        private void var16()
        {
            if (this._var9 == null)
            {
                this._var6 = mtd73.GetImageList(Assembly.GetAssembly(typeof(MControl.Printing.View.Design.Report)), "MControl.Printing.View.Resources.ImagesDesigner.bmp", new Size(0x10, 0x10), new Point(0, 0));
                this._var9 = new PanelDesiger(this);
                this._var9.Size = new Size(400, 440);
                this._var9.mtd417(ref this._var6);
                this._var9.Dock = DockStyle.Fill;
            }
            if ((this._var10 == null) && !this._var7)
            {
                this._var10 = new Splitter();
                this._var10.Dock = DockStyle.Bottom;
                this._var10.MinExtra = 40;
                this._var10.MinSize = 40;
            }
            this.var20();
            if (this._var8 == null)
            {
                this._var8 = new Control();
                this._var8.Size = new Size(400, 440);
                if (!this._var7)
                {
                    this._var8.Controls.AddRange(new Control[] { this._var9, this._var10, this._var12 });
                }
                else
                {
                    this._var8.Controls.Add(this._var9);
                }
            }
        }

        private void var17()
        {
            this._var5 = (IToolboxService) this.GetService(typeof(IToolboxService));
            if (this._var5 != null)
            {
                bool flag = true;
                if (this._var5.CategoryNames != null)
                {
                    foreach (string str in this._var5.CategoryNames)
                    {
                        if (str == "ReportView")
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                if (flag)
                {
                    Bitmap[] bitmapArray = mtd73.GetControlsBitmap(Assembly.GetAssembly(typeof(MControl.Printing.View.Design.Report)));
                    this.var25(typeof(McLabel).FullName, "Label", bitmapArray[0]);
                    this.var25(typeof(McTextBox).FullName, "TextBox", bitmapArray[1]);
                    this.var25(typeof(McCheckBox).FullName, "CheckBox", bitmapArray[2]);
                    this.var25(typeof(McPicture).FullName, "Picture", bitmapArray[3]);
                    this.var25(typeof(McShape).FullName, "Shape", bitmapArray[4]);
                    this.var25(typeof(McLine).FullName, "Line", bitmapArray[5]);
                    this.var25(typeof(McRichText).FullName, "RichText", bitmapArray[6]);
                    this.var25(typeof(McSubReport).FullName, "SubReport", bitmapArray[7]);
                    this.var25(typeof(McPageBreak).FullName, "PageBreak", bitmapArray[8]);
                }
            }
        }

        private void var18()
        {
            if (this._var0 != null)
            {
                this._var0.LoadComplete += new EventHandler(this.var31);
                this._var0.Activated += new EventHandler(this.var32);
                this._var0.Deactivated += new EventHandler(this.var33);
                this._var0.TransactionOpened += new EventHandler(this.var34);
                this._var0.TransactionClosed += new DesignerTransactionCloseEventHandler(this.var35);
            }
            if (this._var3 != null)
            {
                this._var3.SelectionChanged += new EventHandler(this.var36);
            }
            if (this._var4 != null)
            {
                this._var4.ComponentAdded += new ComponentEventHandler(this.var37);
                this._var4.ComponentRemoved += new ComponentEventHandler(this.var38);
                this._var4.ComponentChanged += new ComponentChangedEventHandler(this.var39);
            }
        }

        private void var19()
        {
            IMenuCommandService menuCommandService = (IMenuCommandService) this.GetService(typeof(IMenuCommandService));
            if (menuCommandService != null)
            {
                this._var0.RemoveService(typeof(IMenuCommandService));
                this._var2 = new var2(menuCommandService);
                this._var0.AddService(typeof(IMenuCommandService), this._var2);
                this._var1 = new MenuCommand[] { 
                    new MenuCommand(new EventHandler(this.var43), StandardCommands.Cut), new MenuCommand(new EventHandler(this.var43), StandardCommands.Delete), new MenuCommand(new EventHandler(this.var43), StandardCommands.Copy), new var44(new EventHandler(this.var45), new EventHandler(this.var43), StandardCommands.Paste), new var44(new EventHandler(this.var46), new EventHandler(this.var43), StandardCommands.AlignToGrid), new var44(new EventHandler(this.var46), new EventHandler(this.var43), StandardCommands.AlignLeft), new var44(new EventHandler(this.var46), new EventHandler(this.var43), StandardCommands.AlignVerticalCenters), new var44(new EventHandler(this.var46), new EventHandler(this.var43), StandardCommands.AlignRight), new var44(new EventHandler(this.var46), new EventHandler(this.var43), StandardCommands.AlignTop), new var44(new EventHandler(this.var46), new EventHandler(this.var43), StandardCommands.AlignHorizontalCenters), new var44(new EventHandler(this.var46), new EventHandler(this.var43), StandardCommands.AlignBottom), new var44(new EventHandler(this.var46), new EventHandler(this.var43), StandardCommands.SizeToControlWidth), new var44(new EventHandler(this.var46), new EventHandler(this.var43), StandardCommands.SizeToGrid), new var44(new EventHandler(this.var46), new EventHandler(this.var43), StandardCommands.SizeToControlHeight), new var44(new EventHandler(this.var46), new EventHandler(this.var43), StandardCommands.SizeToControl), new var44(new EventHandler(this.var46), new EventHandler(this.var43), StandardCommands.HorizSpaceMakeEqual), 
                    new var44(new EventHandler(this.var46), new EventHandler(this.var43), StandardCommands.HorizSpaceIncrease), new var44(new EventHandler(this.var46), new EventHandler(this.var43), StandardCommands.HorizSpaceDecrease), new var44(new EventHandler(this.var46), new EventHandler(this.var43), StandardCommands.HorizSpaceConcatenate), new var44(new EventHandler(this.var46), new EventHandler(this.var43), StandardCommands.VertSpaceMakeEqual), new var44(new EventHandler(this.var46), new EventHandler(this.var43), StandardCommands.VertSpaceIncrease), new var44(new EventHandler(this.var46), new EventHandler(this.var43), StandardCommands.VertSpaceDecrease), new var44(new EventHandler(this.var46), new EventHandler(this.var43), StandardCommands.VertSpaceConcatenate), new var44(new EventHandler(this.var46), new EventHandler(this.var43), StandardCommands.CenterHorizontally), new var44(new EventHandler(this.var46), new EventHandler(this.var43), StandardCommands.CenterVertically), new var44(new EventHandler(this.var46), new EventHandler(this.var43), StandardCommands.BringToFront), new var44(new EventHandler(this.var46), new EventHandler(this.var43), StandardCommands.SendToBack)
                 };
                foreach (MenuCommand command in this._var1)
                {
                    this._var2.AddCommand(command);
                }
                this._var13 = new DesignerVerb("PageSettings", new EventHandler(this._var9.mtd429));
                this._var2.AddVerb(this._var13);
            }
        }

        private void var20()
        {
            if ((this._var12 == null) && !this._var7)
            {
                this._var12 = new var11(this, base.Component.Site);
                this._var12.ShowLargeIcons = false;
                this._var12.AutoArrange = false;
                this._var12.AutoScroll = true;
                this._var12.Height = 40;
                this._var12.Dock = DockStyle.Bottom;
                if (this._var0 != null)
                {
                    this._var0.AddService(typeof(ComponentTray), this._var12);
                }
            }
        }

        private void var22()
        {
            if (this._var12 != null)
            {
                this._var0.RemoveService(typeof(ComponentTray));
                this._var12.Dispose();
                this._var12 = null;
            }
        }

        private void var23()
        {
            if (this._var0 != null)
            {
                this._var0.LoadComplete -= new EventHandler(this.var31);
                this._var0.Activated -= new EventHandler(this.var32);
                this._var0.Deactivated -= new EventHandler(this.var33);
                this._var0.TransactionOpened -= new EventHandler(this.var34);
                this._var0.TransactionClosed -= new DesignerTransactionCloseEventHandler(this.var35);
            }
            if (this._var3 != null)
            {
                this._var3.SelectionChanged -= new EventHandler(this.var36);
            }
            if (this._var4 != null)
            {
                this._var4.ComponentAdded -= new ComponentEventHandler(this.var37);
                this._var4.ComponentRemoved -= new ComponentEventHandler(this.var38);
                this._var4.ComponentChanged -= new ComponentChangedEventHandler(this.var39);
                this._var4.ComponentChanging -= new ComponentChangingEventHandler(this.var40);
            }
        }

        private void var24()
        {
            if (this._var2 != null)
            {
                if ((this._var1 != null) && (this._var2 != null))
                {
                    foreach (MenuCommand command in this._var1)
                    {
                        this._var2.RemoveCommand(command);
                    }
                    this._var2.RemoveVerb(this._var13);
                    this._var1 = null;
                }
                this._var0.RemoveService(typeof(IMenuCommandService));
                this._var0.AddService(typeof(IMenuCommandService), ((var2) this._var2).VSMenuCommandService);
            }
        }

        private void var25(string var26, string var27, Bitmap var28)
        {
            ToolboxItem toolboxItem = new ToolboxItem();
            toolboxItem.TypeName = var26;
            toolboxItem.DisplayName = var27;
            toolboxItem.Bitmap = var28;
            toolboxItem.Lock();
            this._var5.AddToolboxItem(toolboxItem, "ReportView");
        }

        private void var31(object sender, EventArgs e)
        {
            this._var9.mtd468(this.mtd268);
            this._var0.LoadComplete -= new EventHandler(this.var31);
        }

        private void var32(object sender, EventArgs e)
        {
            this._var9.mtd469(sender, e);
        }

        private void var33(object sender, EventArgs e)
        {
            this._var9.mtd470(sender, e);
        }

        private void var34(object sender, EventArgs e)
        {
            this._var9.mtd459(sender, e);
        }

        private void var35(object sender, DesignerTransactionCloseEventArgs e)
        {
            this._var9.mtd460(sender, e);
        }

        private void var36(object sender, EventArgs e)
        {
            ICollection selectedComponents = this._var3.GetSelectedComponents();
            foreach (IComponent component in selectedComponents)
            {
                if (selectedComponents.Count == 1)
                {
                    if (component is MControl.Printing.View.Design.Report)
                    {
                        this._var9.mtd430.mtd464();
                    }
                    else if (component is Section)
                    {
                        this._var9.mtd430.mtd465((Section) component);
                    }
                    else if (component is McReportControl)
                    {
                        this._var9.mtd430.mtd466((McReportControl) component);
                    }
                    else
                    {
                        this._var9.mtd430.mtd467();
                    }
                    break;
                }
                if (this.var41(component))
                {
                    this._var9.mtd430.mtd467();
                    break;
                }
            }
        }

        private void var37(object var47, ComponentEventArgs e)
        {
            if (this.var41(e.Component))
            {
                this._var12.AddComponent(e.Component);
            }
            else
            {
                this._var9.mtd461(var47, e);
            }
        }

        private void var38(object var47, ComponentEventArgs e)
        {
            Component component = (Component) e.Component;
            if (this.var41(component))
            {
                this._var12.RemoveComponent(component);
                if (object.Equals(this.mtd268.DataSource, component))
                {
                    this.mtd268.DataSource = null;
                }
                if (object.Equals(this.mtd268.DataAdapter, component))
                {
                    this.mtd268.DataAdapter = null;
                }
            }
            else
            {
                this._var9.mtd462(var47, e);
            }
        }

        private void var39(object sender, ComponentChangedEventArgs e)
        {
            this._var9.mtd463(sender, e);
        }

        private void var40(object sender, ComponentChangingEventArgs ce)
        {
        }

        private bool var41(IComponent var42)
        {
            return ((!(var42 is MControl.Printing.View.Design.Report) && !(var42 is McReportControl)) && (!(var42 is Section) && !(var42 is Control)));
        }

        private void var43(object sender, EventArgs e)
        {
            CommandID commandID = ((MenuCommand) sender).CommandID;
            if (commandID == StandardCommands.Cut)
            {
                if (this._var9.mtd430.mtd431 > 0)
                {
                    this._var9.mtd430.mtd432();
                }
                else
                {
                    this._var12.mtd432(this._var3.GetSelectedComponents());
                }
            }
            else if (commandID == StandardCommands.Delete)
            {
                if (this._var9.mtd430.mtd431 > 0)
                {
                    this._var9.mtd430.mtd433();
                }
                else
                {
                    this._var12.mtd433(this._var3.GetSelectedComponents());
                }
            }
            else if (commandID == StandardCommands.Copy)
            {
                if (this._var9.mtd430.mtd431 > 0)
                {
                    this._var9.mtd430.mtd434();
                }
                else
                {
                    this._var12.mtd434(this._var3.GetSelectedComponents());
                }
            }
            else if (commandID == StandardCommands.Paste)
            {
                if (this._var9.mtd430.mtd431 > 0)
                {
                    this._var9.mtd430.mtd435();
                }
                else
                {
                    this._var12.mtd435();
                }
            }
            else if (commandID == StandardCommands.AlignToGrid)
            {
                this._var9.mtd430.mtd436();
            }
            else if (commandID == StandardCommands.AlignLeft)
            {
                this._var9.mtd430.mtd437();
            }
            else if (commandID == StandardCommands.AlignVerticalCenters)
            {
                this._var9.mtd430.mtd438();
            }
            else if (commandID == StandardCommands.AlignRight)
            {
                this._var9.mtd430.mtd439();
            }
            else if (commandID == StandardCommands.AlignTop)
            {
                this._var9.mtd430.mtd440();
            }
            else if (commandID == StandardCommands.AlignHorizontalCenters)
            {
                this._var9.mtd430.mtd441();
            }
            else if (commandID == StandardCommands.AlignBottom)
            {
                this._var9.mtd430.mtd442();
            }
            else if (commandID == StandardCommands.SizeToControlWidth)
            {
                this._var9.mtd430.mtd443();
            }
            else if (commandID == StandardCommands.SizeToGrid)
            {
                this._var9.mtd430.mtd444();
            }
            else if (commandID == StandardCommands.SizeToControlHeight)
            {
                this._var9.mtd430.mtd445();
            }
            else if (commandID == StandardCommands.SizeToControl)
            {
                this._var9.mtd430.mtd446();
            }
            else if (commandID == StandardCommands.HorizSpaceMakeEqual)
            {
                this._var9.mtd430.mtd447();
            }
            else if (commandID == StandardCommands.HorizSpaceIncrease)
            {
                this._var9.mtd430.mtd448();
            }
            else if (commandID == StandardCommands.HorizSpaceDecrease)
            {
                this._var9.mtd430.mtd449();
            }
            else if (commandID == StandardCommands.HorizSpaceConcatenate)
            {
                this._var9.mtd430.mtd450();
            }
            else if (commandID == StandardCommands.VertSpaceMakeEqual)
            {
                this._var9.mtd430.mtd451();
            }
            else if (commandID == StandardCommands.VertSpaceIncrease)
            {
                this._var9.mtd430.mtd452();
            }
            else if (commandID == StandardCommands.VertSpaceDecrease)
            {
                this._var9.mtd430.mtd453();
            }
            else if (commandID == StandardCommands.VertSpaceConcatenate)
            {
                this._var9.mtd430.mtd454();
            }
            else if (commandID == StandardCommands.CenterHorizontally)
            {
                this._var9.mtd430.mtd455(this._var9.mtd268.ReportWidth);
            }
            else if (commandID == StandardCommands.CenterVertically)
            {
                this._var9.mtd430.mtd456();
            }
            else if (commandID == StandardCommands.BringToFront)
            {
                this._var9.mtd430.mtd457();
            }
            else if (commandID == StandardCommands.SendToBack)
            {
                this._var9.mtd430.mtd458();
            }
        }

        private void var45(object sender, EventArgs e)
        {
            MenuCommand command = (MenuCommand) sender;
            bool flag = false;
            IDataObject dataObject = Clipboard.GetDataObject();
            if ((dataObject != null) && (dataObject.GetDataPresent("Mc_CONTROLCOMPONENTS") || dataObject.GetDataPresent("Mc_TRAYCOMPONENTS")))
            {
                flag = true;
            }
            command.Enabled = flag;
        }

        private void var46(object sender, EventArgs e)
        {
            MenuCommand command = (MenuCommand) sender;
            bool flag = false;
            int num = this._var9.mtd430.mtd431;
            switch (num)
            {
                case 1:
                    if ((command.CommandID == StandardCommands.SizeToGrid) || (command.CommandID == StandardCommands.AlignToGrid))
                    {
                        flag = true;
                    }
                    break;

                case 2:
                    if ((((command.CommandID == StandardCommands.HorizSpaceMakeEqual) || (command.CommandID == StandardCommands.HorizSpaceIncrease)) || ((command.CommandID == StandardCommands.HorizSpaceDecrease) || (command.CommandID == StandardCommands.HorizSpaceConcatenate))) || (((command.CommandID == StandardCommands.VertSpaceMakeEqual) || (command.CommandID == StandardCommands.VertSpaceIncrease)) || ((command.CommandID == StandardCommands.VertSpaceDecrease) || (command.CommandID == StandardCommands.VertSpaceConcatenate))))
                    {
                        flag = false;
                    }
                    else
                    {
                        flag = true;
                    }
                    break;

                default:
                    if (num > 2)
                    {
                        flag = true;
                    }
                    break;
            }
            command.Enabled = flag;
        }

        object IRootDesigner.GetView(ViewTechnology var29)
        {
            if (var29 != ViewTechnology.Default)//.WindowsForms)
            {
                throw new ArgumentException("technology");
            }
            return this._var8;
        }

        bool IToolboxUser.GetToolSupported(ToolboxItem var30)
        {
            return true;
        }

        void IToolboxUser.ToolPicked(ToolboxItem var30)
        {
        }

        internal MControl.Printing.View.Design.Report mtd268
        {
            get
            {
                return (MControl.Printing.View.Design.Report) base.Component;
            }
        }

        internal bool mtd424
        {
            get
            {
                return this._var7;
            }
            set
            {
                this._var7 = value;
            }
        }

        internal PanelDesiger mtd425
        {
            get
            {
                return this._var9;
            }
        }

        internal IDesignerHost mtd426
        {
            get
            {
                return this._var0;
            }
        }

        internal IToolboxService mtd427
        {
            get
            {
                return this._var5;
            }
        }

        internal ISelectionService mtd428
        {
            get
            {
                return this._var3;
            }
        }

        ViewTechnology[] IRootDesigner.SupportedTechnologies
        {
            get
            {
                return new ViewTechnology[] { ViewTechnology.Default };//.WindowsForms };
            }
        }

        private class var11 : ComponentTray
        {
            private IToolboxService _var50;
            private IDesignerHost _var51;
            private IDesignerSerializationService _var52;
            private IMenuCommandService _var53;

            internal var11(IDesigner var54, IServiceProvider var55) : base(var54, var55)
            {
                this._var50 = (IToolboxService) var55.GetService(typeof(IToolboxService));
                this._var51 = (IDesignerHost) var55.GetService(typeof(IDesignerHost));
                this._var52 = (IDesignerSerializationService) var55.GetService(typeof(IDesignerSerializationService));
                this._var53 = (IMenuCommandService) var55.GetService(typeof(IMenuCommandService));
            }

            internal void mtd432(ICollection var56)
            {
                this.mtd434(var56);
                this.mtd433(var56);
            }

            internal void mtd433(ICollection var56)
            {
                using (DesignerTransaction transaction = this._var51.CreateTransaction("Delete-TrayComponent"))
                {
                    foreach (IComponent component in var56)
                    {
                        if (this.var57(component))
                        {
                            this.RemoveComponent(component);
                            this._var51.DestroyComponent(component);
                        }
                    }
                    transaction.Commit();
                }
            }

            internal void mtd434(ICollection var56)
            {
                ArrayList list = new ArrayList();
                foreach (IComponent component in var56)
                {
                    if (this.var57(component))
                    {
                        list.Add(component);
                        this.var58(component, list);
                    }
                }
                if ((this._var52 != null) && (list.Count > 0))
                {
                    Cursor current = Cursor.Current;
                    try
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        if (this._var52 != null)
                        {
                            object graph = this._var52.Serialize(list);
                            Stream serializationStream = new MemoryStream();
                            new BinaryFormatter().Serialize(serializationStream, graph);
                            serializationStream.Seek(0L, SeekOrigin.Begin);
                            IDataObject data = new DataObject("Mc_TRAYCOMPONENTS", serializationStream);
                            Clipboard.SetDataObject(data);
                        }
                    }
                    finally
                    {
                        Cursor.Current = current;
                    }
                }
            }

            internal void mtd435()
            {
                using (DesignerTransaction transaction = this._var51.CreateTransaction("Paste-TrayControls"))
                {
                    if (this._var52 != null)
                    {
                        IDataObject dataObject = Clipboard.GetDataObject();
                        if (dataObject != null)
                        {
                            object data = dataObject.GetData("Mc_TRAYCOMPONENTS");
                            if (data is Stream)
                            {
                                BinaryFormatter formatter = new BinaryFormatter();
                                ((Stream) data).Seek(0L, SeekOrigin.Begin);
                                object serializationData = formatter.Deserialize((Stream) data);
                                ICollection is2 = this._var52.Deserialize(serializationData);
                                if (is2 != null)
                                {
                                    foreach (IComponent component in is2)
                                    {
                                        this._var51.Container.Add(component);
                                    }
                                    this._var53.GlobalInvoke(StandardCommands.LineupIcons);
                                }
                            }
                        }
                    }
                    transaction.Commit();
                }
            }

            public override void AddComponent(IComponent var14)
            {
                base.AddComponent(var14);
            }

            protected override bool CanCreateComponentFromTool(ToolboxItem var60)
            {
                Type type = this._var51.GetType(var60.TypeName);
                if (type != null)
                {
                    if (type.IsSubclassOf(typeof(McReportControl)))
                    {
                        return false;
                    }
                    if (type.IsSubclassOf(typeof(Control)))
                    {
                        return false;
                    }
                }
                return true;
            }

            protected override void OnSetCursor()
            {
                if (this._var50 != null)
                {
                    if ((this._var50.SelectedCategory == "ReportView") || !this._var50.SetCursor())
                    {
                        Cursor.Current = Cursors.Default;
                    }
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                }
            }

            private bool var57(IComponent var14)
            {
                foreach (Control control in base.Controls)
                {
                    PropertyDescriptor descriptor = TypeDescriptor.GetProperties(control)["Component"];
                    if ((descriptor != null) && (var14 == descriptor.GetValue(control)))
                    {
                        return true;
                    }
                }
                return false;
            }

            private void var58(IComponent var14, ArrayList var59)
            {
                ComponentDesigner designer = this._var51.GetDesigner(var14) as ComponentDesigner;
                if (designer != null)
                {
                    foreach (IComponent component in designer.AssociatedComponents)
                    {
                        var59.Add(component);
                        this.var58(component, var59);
                    }
                }
            }
        }

        private class var2 : IMenuCommandService
        {
            private IMenuCommandService _var49;

            public var2(IMenuCommandService menuCommandService)
            {
                this._var49 = menuCommandService;
            }

            void IMenuCommandService.AddCommand(MenuCommand command)
            {
                if (this._var49.FindCommand(command.CommandID) == null)
                {
                    this._var49.AddCommand(command);
                }
            }

            void IMenuCommandService.AddVerb(DesignerVerb verb)
            {
                this._var49.AddVerb(verb);
            }

            MenuCommand IMenuCommandService.FindCommand(CommandID commandID)
            {
                return this._var49.FindCommand(commandID);
            }

            bool IMenuCommandService.GlobalInvoke(CommandID commandID)
            {
                return this._var49.GlobalInvoke(commandID);
            }

            void IMenuCommandService.RemoveCommand(MenuCommand command)
            {
                this._var49.RemoveCommand(command);
            }

            void IMenuCommandService.RemoveVerb(DesignerVerb verb)
            {
                this._var49.RemoveVerb(verb);
            }

            void IMenuCommandService.ShowContextMenu(CommandID menuID, int x, int y)
            {
                this._var49.ShowContextMenu(menuID, x, y);
            }

            DesignerVerbCollection IMenuCommandService.Verbs
            {
                get
                {
                    return this._var49.Verbs;
                }
            }

            public IMenuCommandService VSMenuCommandService
            {
                get
                {
                    return this._var49;
                }
            }
        }

        private class var44 : MenuCommand
        {
            private EventHandler _var48;

            public var44(EventHandler statusHandler, EventHandler invokeHandler, CommandID id) : base(invokeHandler, id)
            {
                this._var48 = statusHandler;
            }

            public override int OleStatus
            {
                get
                {
                    if (this._var48 != null)
                    {
                        this._var48(this, EventArgs.Empty);
                    }
                    return base.OleStatus;
                }
            }
        }
    }
}

