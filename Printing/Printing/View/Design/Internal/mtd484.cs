namespace MControl.Printing.View.Design
{
    using MControl.Printing.View;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.ComponentModel.Design.Serialization;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Windows.Forms;

    //mtd484
    internal class ReportControlDesigner
    {
        private McReportControl _var0;
        private SectionDesigner _var1;
        private bool _var13;
        private Point _var14 = new Point();
        private Point _var15 = new Point();
        private Rectangle _var16 = new Rectangle();
        private Rectangle _var17 = new Rectangle();
        private Rectangle _var18 = new Rectangle();
        private Point _var2;
        private mtd560 _var3;
        private Point _var35;
        private Point _var36;
        private Point[] _var37 = new Point[2];
        private int _var38;
        private bool _var39;
        private bool _var4 = true;
        private PanelDesiger _var5;
        private ReportViewDesigner _var6;
        private mtd561 _var7 = null;
        private bool _var8 = true;
        private bool _var9 = true;

        internal ReportControlDesigner(PanelDesiger var10, ReportViewDesigner var11)
        {
            _var35 = Point.Empty;

            this._var5 = var10;
            this._var6 = var11;
            this._var3 = new mtd560();
            this._var3.mtd390 += new CollectionChange(this.var12);
        }

        internal void mtd432()
        {
            IDesignerHost host = this._var6.mtd426;
            if ((host != null) && (this._var3.mtd166 > 0))
            {
                using (DesignerTransaction transaction = host.CreateTransaction("Cut-McControls"))
                {
                    this.mtd434();
                    this.mtd433();
                    transaction.Commit();
                }
            }
        }

        internal void mtd433()
        {
            McReportControl component = null;
            this._var8 = false;
            this.mtd502(new object[] { this._var1.mtd393 }, SelectionTypes.Replace);
            IDesignerHost host = this._var6.mtd426;
            if (host != null)
            {
                using (DesignerTransaction transaction = host.CreateTransaction("Delete-McControls"))
                {
                    foreach (mtd562 mtd in this._var3)
                    {
                        component = mtd.mtd563;
                        host.DestroyComponent(component);
                    }
                    this._var3.mtd387();
                    transaction.Commit();
                }
            }
            this._var8 = true;
            this._var5.mtd516(LayoutChangedType.ControlDelete);
        }

        internal void mtd434()
        {
            ArrayList objects = new ArrayList();
            foreach (mtd562 mtd in this._var3)
            {
                objects.Add(mtd.mtd563);
            }
            IDesignerSerializationService service = (IDesignerSerializationService) this._var6.mtd426.GetService(typeof(IDesignerSerializationService));
            if ((service != null) && (objects.Count > 0))
            {
                Cursor current = Cursor.Current;
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    if (service != null)
                    {
                        object graph = service.Serialize(objects);
                        Stream serializationStream = new MemoryStream();
                        new BinaryFormatter().Serialize(serializationStream, graph);
                        serializationStream.Seek(0L, SeekOrigin.Begin);
                        IDataObject data = new DataObject("Mc_CONTROLCOMPONENTS", serializationStream);
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
            IDesignerHost host = this._var6.mtd426;
            using (DesignerTransaction transaction = host.CreateTransaction("Paste-McControls"))
            {
                IDesignerSerializationService service = (IDesignerSerializationService) this._var6.mtd426.GetService(typeof(IDesignerSerializationService));
                if ((service != null) & (this._var1 != null))
                {
                    IDataObject dataObject = Clipboard.GetDataObject();
                    if (dataObject != null)
                    {
                        object data = dataObject.GetData("Mc_CONTROLCOMPONENTS");
                        if (data is Stream)
                        {
                            Section section = this._var1.mtd393;
                            BinaryFormatter formatter = new BinaryFormatter();
                            ((Stream) data).Seek(0L, SeekOrigin.Begin);
                            object serializationData = formatter.Deserialize((Stream) data);
                            ICollection is2 = service.Deserialize(serializationData);
                            if (is2 != null)
                            {
                                foreach (IComponent component in is2)
                                {
                                    if (component is McReportControl)
                                    {
                                        TypeDescriptor.GetProperties(component)["Parent"].SetValue(component, section);
                                        host.Container.Add(component);
                                    }
                                }
                                this.ForceSectionUpdate(section);
                            }
                            transaction.Commit();
                        }
                    }
                }
            }
            this._var5.mtd516(LayoutChangedType.ControlAdd);
        }

        internal void mtd436()
        {
            IDesignerHost host = this._var6.mtd426;
            if (host != null)
            {
                using (DesignerTransaction transaction = host.CreateTransaction("AlignToGrid-McControls"))
                {
                    foreach (mtd562 mtd in this._var3)
                    {
                        PropertyDescriptorCollection properties;
                        if (mtd.mtd563.Type == ControlType.PageBreak)
                        {
                            continue;
                        }
                        if (mtd.mtd563.Type == ControlType.Line)
                        {
                            McLine component = (McLine) mtd.mtd563;
                            properties = TypeDescriptor.GetProperties(component);
                            properties["X1"].SetValue(component, Convert.ToSingle((double) (Math.Ceiling((double) (component.X1 / 6f)) * 6.0)));
                            properties["Y1"].SetValue(component, Convert.ToSingle((double) (Math.Ceiling((double) (component.Y1 / 6f)) * 6.0)));
                            continue;
                        }
                        properties = TypeDescriptor.GetProperties(mtd.mtd563);
                        properties["Left"].SetValue(mtd.mtd563, Convert.ToSingle((double) (Math.Ceiling((double) (mtd.mtd563.Left / 6f)) * 6.0)));
                        properties["Top"].SetValue(mtd.mtd563, Convert.ToSingle((double) (Math.Ceiling((double) (mtd.mtd563.Top / 6f)) * 6.0)));
                    }
                    transaction.Commit();
                }
                this._var5.mtd516(LayoutChangedType.ControlMove);
            }
        }

        internal void mtd437()
        {
            if (!((this._var3.mtd166 < 2) | (this._var0 == null)))
            {
                float left = this._var0.Left;
                IDesignerHost host = this._var6.mtd426;
                if (host != null)
                {
                    using (DesignerTransaction transaction = host.CreateTransaction("AlignLeft-McControls"))
                    {
                        foreach (mtd562 mtd in this._var3)
                        {
                            ControlType type = mtd.mtd563.Type;
                            if (((type != ControlType.PageBreak) && (type != ControlType.Line)) && (string.Compare(this._var0.Name, mtd.mtd563.Name) != 0))
                            {
                                TypeDescriptor.GetProperties(mtd.mtd563)["Left"].SetValue(mtd.mtd563, left);
                            }
                        }
                        transaction.Commit();
                    }
                }
                this._var5.mtd516(LayoutChangedType.ControlMove);
            }
        }

        internal void mtd438()
        {
            if (!((this._var3.mtd166 < 2) | (this._var0 == null)))
            {
                float num2 = this._var0.Left + (this._var0.Width / 2f);
                IDesignerHost host = this._var6.mtd426;
                if (host != null)
                {
                    using (DesignerTransaction transaction = host.CreateTransaction("AlignCenter-McControls"))
                    {
                        foreach (mtd562 mtd in this._var3)
                        {
                            ControlType type = mtd.mtd563.Type;
                            if (((type != ControlType.PageBreak) && (type != ControlType.Line)) && (string.Compare(this._var0.Name, mtd.mtd563.Name) != 0))
                            {
                                float num = num2 - (mtd.mtd563.Left + (mtd.mtd563.Width / 2f));
                                TypeDescriptor.GetProperties(mtd.mtd563)["Left"].SetValue(mtd.mtd563, Math.Max((float) (mtd.mtd563.Left + num), (float) 0f));
                            }
                        }
                        transaction.Commit();
                    }
                }
                this._var5.mtd516(LayoutChangedType.ControlMove);
            }
        }

        internal void mtd439()
        {
            if (!((this._var3.mtd166 < 2) | (this._var0 == null)))
            {
                float num2 = this._var0.Left + this._var0.Width;
                IDesignerHost host = this._var6.mtd426;
                if (host != null)
                {
                    using (DesignerTransaction transaction = host.CreateTransaction("AlignRight-McControls"))
                    {
                        foreach (mtd562 mtd in this._var3)
                        {
                            ControlType type = mtd.mtd563.Type;
                            if (((type != ControlType.PageBreak) && (type != ControlType.Line)) && (string.Compare(this._var0.Name, mtd.mtd563.Name) != 0))
                            {
                                float num = num2 - (mtd.mtd563.Left + mtd.mtd563.Width);
                                TypeDescriptor.GetProperties(mtd.mtd563)["Left"].SetValue(mtd.mtd563, Math.Max((float) (mtd.mtd563.Left + num), (float) 0f));
                            }
                        }
                        transaction.Commit();
                    }
                }
                this._var5.mtd516(LayoutChangedType.ControlMove);
            }
        }

        internal void mtd440()
        {
            if (!((this._var3.mtd166 < 2) | (this._var0 == null)))
            {
                float top = this._var0.Top;
                IDesignerHost host = this._var6.mtd426;
                if (host != null)
                {
                    using (DesignerTransaction transaction = host.CreateTransaction("AlignTop-McControls"))
                    {
                        foreach (mtd562 mtd in this._var3)
                        {
                            ControlType type = mtd.mtd563.Type;
                            if (((type != ControlType.PageBreak) && (type != ControlType.Line)) && (string.Compare(this._var0.Name, mtd.mtd563.Name) != 0))
                            {
                                TypeDescriptor.GetProperties(mtd.mtd563)["Top"].SetValue(mtd.mtd563, top);
                            }
                        }
                        transaction.Commit();
                    }
                }
                this._var5.mtd516(LayoutChangedType.ControlMove);
            }
        }

        internal void mtd441()
        {
            if (!((this._var3.mtd166 < 2) | (this._var0 == null)))
            {
                float num2 = this._var0.Top + (this._var0.Height / 2f);
                IDesignerHost host = this._var6.mtd426;
                if (host != null)
                {
                    using (DesignerTransaction transaction = host.CreateTransaction("AlignMiddle-McControls"))
                    {
                        foreach (mtd562 mtd in this._var3)
                        {
                            ControlType type = mtd.mtd563.Type;
                            if (((type != ControlType.PageBreak) && (type != ControlType.Line)) && (string.Compare(this._var0.Name, mtd.mtd563.Name) != 0))
                            {
                                float num = num2 - (mtd.mtd563.Top + (mtd.mtd563.Height / 2f));
                                TypeDescriptor.GetProperties(mtd.mtd563)["Top"].SetValue(mtd.mtd563, Math.Max((float) (mtd.mtd563.Top + num), (float) 0f));
                            }
                        }
                        transaction.Commit();
                    }
                }
                this._var5.mtd516(LayoutChangedType.ControlMove);
            }
        }

        internal void mtd442()
        {
            if (!((this._var3.mtd166 < 2) | (this._var0 == null)))
            {
                float num2 = this._var0.Top + this._var0.Height;
                IDesignerHost host = this._var6.mtd426;
                if (host != null)
                {
                    using (DesignerTransaction transaction = host.CreateTransaction("AlignBottom-McControls"))
                    {
                        foreach (mtd562 mtd in this._var3)
                        {
                            ControlType type = mtd.mtd563.Type;
                            if (((type != ControlType.PageBreak) && (type != ControlType.Line)) && (string.Compare(this._var0.Name, mtd.mtd563.Name) != 0))
                            {
                                float num = num2 - (mtd.mtd563.Top + mtd.mtd563.Height);
                                TypeDescriptor.GetProperties(mtd.mtd563)["Top"].SetValue(mtd.mtd563, Math.Max((float) (mtd.mtd563.Top + num), (float) 0f));
                            }
                        }
                        transaction.Commit();
                    }
                }
                this._var5.mtd516(LayoutChangedType.ControlMove);
            }
        }

        internal void mtd443()
        {
            if (!((this._var3.mtd166 < 2) | (this._var0 == null)) && ((this._var0.Type != ControlType.Line) & (this._var0.Type != ControlType.PageBreak)))
            {
                float width = this._var0.Width;
                IDesignerHost host = this._var6.mtd426;
                if (host != null)
                {
                    using (DesignerTransaction transaction = host.CreateTransaction("EqualWidth-McControls"))
                    {
                        foreach (mtd562 mtd in this._var3)
                        {
                            ControlType type = mtd.mtd563.Type;
                            if (((type != ControlType.PageBreak) && (type != ControlType.Line)) && (string.Compare(this._var0.Name, mtd.mtd563.Name) != 0))
                            {
                                TypeDescriptor.GetProperties(mtd.mtd563)["Width"].SetValue(mtd.mtd563, width);
                            }
                        }
                        transaction.Commit();
                    }
                }
                this._var5.mtd516(LayoutChangedType.ControlSize);
            }
        }

        internal void mtd444()
        {
            IDesignerHost host = this._var6.mtd426;
            if (host != null)
            {
                using (DesignerTransaction transaction = host.CreateTransaction("SizeToGrid-McControls"))
                {
                    foreach (mtd562 mtd in this._var3)
                    {
                        PropertyDescriptorCollection properties;
                        if (mtd.mtd563.Type == ControlType.PageBreak)
                        {
                            continue;
                        }
                        if (mtd.mtd563.Type == ControlType.Line)
                        {
                            McLine component = (McLine) mtd.mtd563;
                            properties = TypeDescriptor.GetProperties(component);
                            properties["X1"].SetValue(component, Convert.ToSingle((double) (Math.Ceiling((double) (component.X1 / 6f)) * 6.0)));
                            properties["Y1"].SetValue(component, Convert.ToSingle((double) (Math.Ceiling((double) (component.Y1 / 6f)) * 6.0)));
                            properties["X2"].SetValue(component, Convert.ToSingle((double) (Math.Ceiling((double) (component.X2 / 6f)) * 6.0)));
                            properties["Y2"].SetValue(component, Convert.ToSingle((double) (Math.Ceiling((double) (component.Y2 / 6f)) * 6.0)));
                            continue;
                        }
                        properties = TypeDescriptor.GetProperties(mtd.mtd563);
                        properties["Top"].SetValue(mtd.mtd563, Convert.ToSingle((double) (Math.Ceiling((double) (mtd.mtd563.Top / 6f)) * 6.0)));
                        properties["Left"].SetValue(mtd.mtd563, Convert.ToSingle((double) (Math.Ceiling((double) (mtd.mtd563.Left / 6f)) * 6.0)));
                        properties["Width"].SetValue(mtd.mtd563, Convert.ToSingle((double) (Math.Ceiling((double) (mtd.mtd563.Width / 6f)) * 6.0)));
                        properties["Height"].SetValue(mtd.mtd563, Convert.ToSingle((double) (Math.Ceiling((double) (mtd.mtd563.Height / 6f)) * 6.0)));
                    }
                    transaction.Commit();
                }
                this._var5.mtd516(LayoutChangedType.ControlSize);
            }
        }

        internal void mtd445()
        {
            if (!((this._var3.mtd166 < 2) | (this._var0 == null)) && ((this._var0.Type != ControlType.Line) & (this._var0.Type != ControlType.PageBreak)))
            {
                float height = this._var0.Height;
                IDesignerHost host = this._var6.mtd426;
                if (host != null)
                {
                    using (DesignerTransaction transaction = host.CreateTransaction("EqualHeight-McControls"))
                    {
                        foreach (mtd562 mtd in this._var3)
                        {
                            ControlType type = mtd.mtd563.Type;
                            if (((type != ControlType.PageBreak) && (type != ControlType.Line)) && (string.Compare(this._var0.Name, mtd.mtd563.Name) != 0))
                            {
                                TypeDescriptor.GetProperties(mtd.mtd563)["Height"].SetValue(mtd.mtd563, height);
                            }
                        }
                        transaction.Commit();
                    }
                }
                this._var5.mtd516(LayoutChangedType.ControlSize);
            }
        }

        internal void mtd446()
        {
            if (!((this._var3.mtd166 < 2) | (this._var0 == null)) && ((this._var0.Type != ControlType.Line) & (this._var0.Type != ControlType.PageBreak)))
            {
                SizeF size = this._var0.Size;
                IDesignerHost host = this._var6.mtd426;
                if (host != null)
                {
                    using (DesignerTransaction transaction = host.CreateTransaction("EqualSize-McControls"))
                    {
                        foreach (mtd562 mtd in this._var3)
                        {
                            ControlType type = mtd.mtd563.Type;
                            if (((type != ControlType.PageBreak) && (type != ControlType.Line)) && (string.Compare(this._var0.Name, mtd.mtd563.Name) != 0))
                            {
                                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(mtd.mtd563);
                                properties["Width"].SetValue(mtd.mtd563, size.Width);
                                properties["Height"].SetValue(mtd.mtd563, size.Height);
                            }
                        }
                        transaction.Commit();
                    }
                }
                this._var5.mtd516(LayoutChangedType.ControlSize);
            }
        }

        internal void mtd447()
        {
            if (this._var3.mtd166 >= 3)
            {
                int num = 0;
                float num2 = 0f;
                float num3 = 0f;
                using (GraphicsPath path = new GraphicsPath())
                {
                    this._var3.mtd577();
                    foreach (mtd562 mtd in this._var3)
                    {
                        ControlType type = mtd.mtd563.Type;
                        if ((type != ControlType.PageBreak) && (type != ControlType.Line))
                        {
                            RectangleF bounds = mtd.mtd563.Bounds;
                            path.AddRectangle(bounds);
                            num2 += bounds.Width;
                            num++;
                        }
                    }
                    num2 = path.GetBounds().Width - num2;
                    if (num2 <= 0f)
                    {
                        return;
                    }
                    num2 /= (float) (num - 1);
                    num = 0;
                    IDesignerHost host = this._var6.mtd426;
                    if (host != null)
                    {
                        using (DesignerTransaction transaction = host.CreateTransaction("SpaceEqualHorizontal-McControls"))
                        {
                            foreach (mtd562 mtd2 in this._var3)
                            {
                                switch (mtd2.mtd563.Type)
                                {
                                    case ControlType.PageBreak:
                                    case ControlType.Line:
                                    {
                                        continue;
                                    }
                                }
                                if (num == 0)
                                {
                                    num3 = mtd2.mtd563.Left + mtd2.mtd563.Width;
                                    num = 1;
                                    continue;
                                }
                                TypeDescriptor.GetProperties(mtd2.mtd563)["Left"].SetValue(mtd2.mtd563, num3 + num2);
                                num3 = mtd2.mtd563.Left + mtd2.mtd563.Width;
                            }
                            transaction.Commit();
                        }
                    }
                    this._var3.mtd213();
                }
                this._var5.mtd516(LayoutChangedType.ControlMove);
            }
        }

        internal void mtd448()
        {
            if (!((this._var3.mtd166 < 3) | (this._var0 == null)) && !((this._var0.Type == ControlType.Line) & (this._var0.Type == ControlType.PageBreak)))
            {
                int num = 0;
                this._var3.mtd577();
                IDesignerHost host = this._var6.mtd426;
                if (host != null)
                {
                    using (DesignerTransaction transaction = host.CreateTransaction("SpaceIncreaseHorizontal-McControls"))
                    {
                        ControlType type;
                        foreach (mtd562 mtd in this._var3)
                        {
                            num++;
                            type = mtd.mtd563.Type;
                            if ((type != ControlType.PageBreak) && (type != ControlType.Line))
                            {
                                if (string.Compare(mtd.mtd563.Name, this._var0.Name) == 0)
                                {
                                    break;
                                }
                                TypeDescriptor.GetProperties(mtd.mtd563)["Left"].SetValue(mtd.mtd563, Math.Max((float) (mtd.mtd563.Left - 12f), (float) 0f));
                            }
                        }
                        for (int i = num; i < this._var3.mtd166; i++)
                        {
                            mtd562 mtd2 = this._var3[i];
                            type = mtd2.mtd563.Type;
                            if ((type != ControlType.PageBreak) && (type != ControlType.Line))
                            {
                                TypeDescriptor.GetProperties(mtd2.mtd563)["Left"].SetValue(mtd2.mtd563, mtd2.mtd563.Left + 12f);
                            }
                        }
                        transaction.Commit();
                    }
                }
                this._var3.mtd213();
                this._var5.mtd516(LayoutChangedType.ControlMove);
            }
        }

        internal void mtd449()
        {
            if (!((this._var3.mtd166 < 3) | (this._var0 == null)) && !((this._var0.Type == ControlType.Line) & (this._var0.Type == ControlType.PageBreak)))
            {
                int num = 0;
                this._var3.mtd577();
                IDesignerHost host = this._var6.mtd426;
                if (host != null)
                {
                    using (DesignerTransaction transaction = host.CreateTransaction("SpaceDecreaseHorizontal-McControls"))
                    {
                        float num2;
                        ControlType type;
                        foreach (mtd562 mtd in this._var3)
                        {
                            num++;
                            type = mtd.mtd563.Type;
                            switch (type)
                            {
                                case ControlType.PageBreak:
                                case ControlType.Line:
                                {
                                    continue;
                                }
                            }
                            num2 = mtd.mtd563.Left + mtd.mtd563.Width;
                            if (mtd.mtd563.Name == this._var0.Name)
                            {
                                break;
                            }
                            if (num2 < this._var0.Left)
                            {
                                if ((num2 + 12f) > this._var0.Left)
                                {
                                    TypeDescriptor.GetProperties(mtd.mtd563)["Left"].SetValue(mtd.mtd563, mtd.mtd563.Left + (this._var0.Left - num2));
                                    continue;
                                }
                                TypeDescriptor.GetProperties(mtd.mtd563)["Left"].SetValue(mtd.mtd563, mtd.mtd563.Left + 12f);
                            }
                        }
                        num2 = this._var0.Left + this._var0.Width;
                        for (int i = num; i < this._var3.mtd166; i++)
                        {
                            mtd562 mtd2 = this._var3[i];
                            type = mtd2.mtd563.Type;
                            if (((type != ControlType.PageBreak) && (type != ControlType.Line)) && (mtd2.mtd563.Left > num2))
                            {
                                if ((mtd2.mtd563.Left - 12f) < num2)
                                {
                                    TypeDescriptor.GetProperties(mtd2.mtd563)["Left"].SetValue(mtd2.mtd563, num2);
                                }
                                else
                                {
                                    TypeDescriptor.GetProperties(mtd2.mtd563)["Left"].SetValue(mtd2.mtd563, mtd2.mtd563.Left - 12f);
                                }
                            }
                        }
                        transaction.Commit();
                    }
                }
                this._var3.mtd213();
                this._var5.mtd516(LayoutChangedType.ControlMove);
            }
        }

        internal void mtd450()
        {
            if (!((this._var3.mtd166 < 2) | (this._var0 == null)) && !((this._var0.Type == ControlType.Line) & (this._var0.Type == ControlType.PageBreak)))
            {
                int num = this._var3.mtd215(this._var0.Name);
                if (num != -1)
                {
                    this._var3.mtd577();
                    IDesignerHost host = this._var6.mtd426;
                    if (host != null)
                    {
                        using (DesignerTransaction transaction = host.CreateTransaction("SpaceRemoveHorizontal-McControls"))
                        {
                            float num2;
                            ControlType type;
                            mtd562 mtd;
                            mtd562 mtd2;
                            for (int i = num - 1; i >= 0; i--)
                            {
                                mtd2 = this._var3[i];
                                mtd = this._var3[i + 1];
                                type = mtd2.mtd563.Type;
                                if ((type != ControlType.PageBreak) && (type != ControlType.Line))
                                {
                                    num2 = mtd2.mtd563.Left + mtd2.mtd563.Width;
                                    if (num2 < mtd.mtd563.Left)
                                    {
                                        TypeDescriptor.GetProperties(mtd2.mtd563)["Left"].SetValue(mtd2.mtd563, mtd2.mtd563.Left + (mtd.mtd563.Left - num2));
                                    }
                                }
                            }
                            for (int j = num + 1; j < this._var3.mtd166; j++)
                            {
                                mtd2 = this._var3[j];
                                mtd = this._var3[j - 1];
                                type = mtd2.mtd563.Type;
                                if ((type != ControlType.PageBreak) && (type != ControlType.Line))
                                {
                                    num2 = mtd.mtd563.Left + mtd.mtd563.Width;
                                    if (mtd2.mtd563.Left > num2)
                                    {
                                        TypeDescriptor.GetProperties(mtd2.mtd563)["Left"].SetValue(mtd2.mtd563, num2);
                                    }
                                }
                            }
                            transaction.Commit();
                        }
                    }
                    this._var3.mtd213();
                    this._var5.mtd516(LayoutChangedType.ControlMove);
                }
            }
        }

        internal void mtd451()
        {
            if (this._var3.mtd166 >= 3)
            {
                int num = 0;
                float num2 = 0f;
                float num3 = 0f;
                using (GraphicsPath path = new GraphicsPath())
                {
                    this._var3.mtd1();
                    foreach (mtd562 mtd in this._var3)
                    {
                        ControlType type = mtd.mtd563.Type;
                        if ((type != ControlType.PageBreak) && (type != ControlType.Line))
                        {
                            RectangleF bounds = mtd.mtd563.Bounds;
                            path.AddRectangle(bounds);
                            num2 += bounds.Height;
                            num++;
                        }
                    }
                    num2 = path.GetBounds().Height - num2;
                    if (num2 <= 0f)
                    {
                        return;
                    }
                    num2 /= (float) (num - 1);
                    num = 0;
                    IDesignerHost host = this._var6.mtd426;
                    if (host != null)
                    {
                        using (DesignerTransaction transaction = host.CreateTransaction("SpaceEqualVertical-McControls"))
                        {
                            foreach (mtd562 mtd2 in this._var3)
                            {
                                switch (mtd2.mtd563.Type)
                                {
                                    case ControlType.PageBreak:
                                    case ControlType.Line:
                                    {
                                        continue;
                                    }
                                }
                                if (num == 0)
                                {
                                    num3 = mtd2.mtd563.Top + mtd2.mtd563.Height;
                                    num = 1;
                                    continue;
                                }
                                TypeDescriptor.GetProperties(mtd2.mtd563)["Top"].SetValue(mtd2.mtd563, num3 + num2);
                                num3 = mtd2.mtd563.Top + mtd2.mtd563.Height;
                            }
                            transaction.Commit();
                        }
                    }
                }
                this._var3.mtd213();
                this._var5.mtd516(LayoutChangedType.ControlMove);
            }
        }

        internal void mtd452()
        {
            if (!((this._var3.mtd166 < 3) | (this._var0 == null)) && !((this._var0.Type == ControlType.Line) & (this._var0.Type == ControlType.PageBreak)))
            {
                int num = 0;
                this._var3.mtd1();
                IDesignerHost host = this._var6.mtd426;
                if (host != null)
                {
                    using (DesignerTransaction transaction = host.CreateTransaction("SpaceIncreaseVertical-McControls"))
                    {
                        ControlType type;
                        foreach (mtd562 mtd in this._var3)
                        {
                            num++;
                            type = mtd.mtd563.Type;
                            if ((type != ControlType.PageBreak) && (type != ControlType.Line))
                            {
                                if (string.Compare(mtd.mtd563.Name, this._var0.Name) == 0)
                                {
                                    break;
                                }
                                TypeDescriptor.GetProperties(mtd.mtd563)["Top"].SetValue(mtd.mtd563, Math.Max((float) (mtd.mtd563.Top - 12f), (float) 0f));
                            }
                        }
                        for (int i = num; i < this._var3.mtd166; i++)
                        {
                            mtd562 mtd2 = this._var3[i];
                            type = mtd2.mtd563.Type;
                            if ((type != ControlType.PageBreak) && (type != ControlType.Line))
                            {
                                TypeDescriptor.GetProperties(mtd2.mtd563)["Top"].SetValue(mtd2.mtd563, mtd2.mtd563.Top + 12f);
                            }
                        }
                        transaction.Commit();
                    }
                }
                this._var3.mtd213();
                this._var5.mtd516(LayoutChangedType.ControlMove);
            }
        }

        internal void mtd453()
        {
            if (!((this._var3.mtd166 < 3) | (this._var0 == null)) && !((this._var0.Type == ControlType.Line) & (this._var0.Type == ControlType.PageBreak)))
            {
                int num = 0;
                this._var3.mtd1();
                IDesignerHost host = this._var6.mtd426;
                if (host != null)
                {
                    using (DesignerTransaction transaction = host.CreateTransaction("SpaceDecreaseVertical-McControls"))
                    {
                        float num2;
                        ControlType type;
                        foreach (mtd562 mtd in this._var3)
                        {
                            num++;
                            type = mtd.mtd563.Type;
                            switch (type)
                            {
                                case ControlType.PageBreak:
                                case ControlType.Line:
                                {
                                    continue;
                                }
                            }
                            num2 = mtd.mtd563.Top + mtd.mtd563.Height;
                            if (mtd.mtd563.Name == this._var0.Name)
                            {
                                break;
                            }
                            if (num2 < this._var0.Top)
                            {
                                if ((num2 + 12f) > this._var0.Top)
                                {
                                    TypeDescriptor.GetProperties(mtd.mtd563)["Top"].SetValue(mtd.mtd563, mtd.mtd563.Top + (this._var0.Top - num2));
                                    continue;
                                }
                                TypeDescriptor.GetProperties(mtd.mtd563)["Top"].SetValue(mtd.mtd563, mtd.mtd563.Top + 12f);
                            }
                        }
                        num2 = this._var0.Top + this._var0.Height;
                        for (int i = num; i < this._var3.mtd166; i++)
                        {
                            mtd562 mtd2 = this._var3[i];
                            type = mtd2.mtd563.Type;
                            if (((type != ControlType.PageBreak) && (type != ControlType.Line)) && (mtd2.mtd563.Top > num2))
                            {
                                if ((mtd2.mtd563.Top - 12f) < num2)
                                {
                                    TypeDescriptor.GetProperties(mtd2.mtd563)["Top"].SetValue(mtd2.mtd563, num2);
                                }
                                else
                                {
                                    TypeDescriptor.GetProperties(mtd2.mtd563)["Top"].SetValue(mtd2.mtd563, mtd2.mtd563.Top - 12f);
                                }
                            }
                        }
                        transaction.Commit();
                    }
                }
                this._var3.mtd213();
                this._var5.mtd516(LayoutChangedType.ControlMove);
            }
        }

        internal void mtd454()
        {
            if (!((this._var3.mtd166 < 2) | (this._var0 == null)) && !((this._var0.Type == ControlType.Line) & (this._var0.Type == ControlType.PageBreak)))
            {
                int num = this._var3.mtd215(this._var0.Name);
                if (num != -1)
                {
                    this._var3.mtd1();
                    IDesignerHost host = this._var6.mtd426;
                    if (host != null)
                    {
                        using (DesignerTransaction transaction = host.CreateTransaction("SpaceRemoveVertical-McControls"))
                        {
                            float num2;
                            ControlType type;
                            mtd562 mtd;
                            mtd562 mtd2;
                            for (int i = num - 1; i >= 0; i--)
                            {
                                mtd2 = this._var3[i];
                                mtd = this._var3[i + 1];
                                type = mtd2.mtd563.Type;
                                if ((type != ControlType.PageBreak) && (type != ControlType.Line))
                                {
                                    num2 = mtd2.mtd563.Top + mtd2.mtd563.Height;
                                    if (num2 < mtd.mtd563.Top)
                                    {
                                        TypeDescriptor.GetProperties(mtd2.mtd563)["Top"].SetValue(mtd2.mtd563, mtd2.mtd563.Top + (mtd.mtd563.Top - num2));
                                    }
                                }
                            }
                            for (int j = num + 1; j <= (this._var3.mtd166 - 1); j++)
                            {
                                mtd2 = this._var3[j];
                                mtd = this._var3[j - 1];
                                type = mtd2.mtd563.Type;
                                if ((type != ControlType.PageBreak) && (type != ControlType.Line))
                                {
                                    num2 = mtd.mtd563.Top + mtd.mtd563.Height;
                                    if (mtd2.mtd563.Top > num2)
                                    {
                                        TypeDescriptor.GetProperties(mtd2.mtd563)["Top"].SetValue(mtd2.mtd563, num2);
                                    }
                                }
                            }
                            transaction.Commit();
                        }
                    }
                    this._var3.mtd213();
                    this._var5.mtd516(LayoutChangedType.ControlMove);
                }
            }
        }

        internal void mtd455(float var61)
        {
            if (this._var3.mtd166 != 0)
            {
                using (GraphicsPath path = new GraphicsPath())
                {
                    RectangleF bounds;
                    ControlType type;
                    foreach (mtd562 mtd in this._var3)
                    {
                        type = mtd.mtd563.Type;
                        if ((type != ControlType.PageBreak) && (type != ControlType.Line))
                        {
                            bounds = mtd.mtd563.Bounds;
                            path.AddRectangle(bounds);
                        }
                    }
                    bounds = path.GetBounds();
                    float num = (var61 / 2f) - (bounds.Left + (bounds.Width / 2f));
                    if (num == 0f)
                    {
                        return;
                    }
                    if ((bounds.Left + num) < 0f)
                    {
                        num = 0f - bounds.Left;
                    }
                    IDesignerHost host = this._var6.mtd426;
                    if (host != null)
                    {
                        using (DesignerTransaction transaction = host.CreateTransaction("CenterHorizontally-McControls"))
                        {
                            foreach (mtd562 mtd2 in this._var3)
                            {
                                type = mtd2.mtd563.Type;
                                if ((type != ControlType.PageBreak) && (type != ControlType.Line))
                                {
                                    TypeDescriptor.GetProperties(mtd2.mtd563)["Left"].SetValue(mtd2.mtd563, mtd2.mtd563.Left + num);
                                }
                            }
                            transaction.Commit();
                        }
                    }
                }
                this._var5.mtd516(LayoutChangedType.ControlMove);
            }
        }

        internal void mtd456()
        {
            if (this._var3.mtd166 != 0)
            {
                using (GraphicsPath path = new GraphicsPath())
                {
                    RectangleF bounds;
                    foreach (mtd562 mtd in this._var3)
                    {
                        if (mtd.mtd563.Type != ControlType.PageBreak)
                        {
                            bounds = mtd.mtd563.Bounds;
                            path.AddRectangle(bounds);
                        }
                    }
                    bounds = path.GetBounds();
                    float num = (this._var1.mtd393.Height / 2f) - (bounds.Top + (bounds.Height / 2f));
                    if (num == 0f)
                    {
                        return;
                    }
                    if ((bounds.Top + num) < 0f)
                    {
                        num = 0f - bounds.Top;
                    }
                    IDesignerHost host = this._var6.mtd426;
                    if (host != null)
                    {
                        using (DesignerTransaction transaction = host.CreateTransaction("CenterVertically-McControls"))
                        {
                            foreach (mtd562 mtd2 in this._var3)
                            {
                                ControlType type = mtd2.mtd563.Type;
                                if ((type != ControlType.PageBreak) && (type != ControlType.Line))
                                {
                                    TypeDescriptor.GetProperties(mtd2.mtd563)["Top"].SetValue(mtd2.mtd563, mtd2.mtd563.Top + num);
                                }
                            }
                            transaction.Commit();
                        }
                    }
                }
                this._var5.mtd516(LayoutChangedType.ControlMove);
            }
        }

        internal void mtd457()
        {
            if (!((this._var3.mtd166 < 2) | (this._var0 == null)))
            {
                this._var3.mtd213();
                int index = this._var1.mtd393.Controls.IndexOf(this._var0);
                int num2 = this._var1.mtd393.Controls.IndexOf(this._var3[this._var3.mtd166 - 1].mtd563);
                if (index < num2)
                {
                    IDesignerHost host = this._var6.mtd426;
                    if (host != null)
                    {
                        using (DesignerTransaction transaction = host.CreateTransaction("BringToFront-McControls"))
                        {
                            TypeDescriptor.GetProperties(this._var0)["Index"].SetValue(this._var0, num2);
                            Section section = this._var1.mtd393;
                            for (int i = index + 1; i <= num2; i++)
                            {
                                McReportControl component = section.Controls[i];
                                TypeDescriptor.GetProperties(component)["Index"].SetValue(component, i - 1);
                            }
                            transaction.Commit();
                        }
                    }
                    this._var3.mtd213();
                    this._var5.mtd516(LayoutChangedType.ControlZOrder);
                }
            }
        }

        internal void mtd458()
        {
            if (!((this._var3.mtd166 < 2) | (this._var0 == null)))
            {
                this._var3.mtd213();
                int index = this._var1.mtd393.Controls.IndexOf(this._var0.Name);
                int num2 = this._var1.mtd393.Controls.IndexOf(this._var3[0].mtd563.Name);
                if (index > num2)
                {
                    IDesignerHost host = this._var6.mtd426;
                    if (host != null)
                    {
                        using (DesignerTransaction transaction = host.CreateTransaction("SendToBack-McControls"))
                        {
                            TypeDescriptor.GetProperties(this._var0)["Index"].SetValue(this._var0, num2);
                            Section section = this._var1.mtd393;
                            for (int i = num2; i < index; i++)
                            {
                                McReportControl component = section.Controls[i];
                                TypeDescriptor.GetProperties(component)["Index"].SetValue(component, i + 1);
                            }
                            transaction.Commit();
                        }
                    }
                    this._var3.mtd213();
                    this._var5.mtd516(LayoutChangedType.ControlZOrder);
                }
            }
        }

        internal void mtd464()
        {
            this._var9 = true;
            this.var29(null, true);
        }

        internal void mtd465()
        {
            if (this._var1 != null)
            {
                this.var29(this._var1.mtd393, true);
            }
        }

        internal void mtd465(Section var30)
        {
            this.var29(var30, true);
        }

        internal void mtd465(string var31)
        {
            this._var9 = true;
            SectionDesigner mtd = this._var5.mtd497[var31];
            if (mtd != null)
            {
                Section section = mtd.mtd393;
                this.mtd502(new object[] { section }, SelectionTypes.Replace);
            }
        }

        internal void mtd466(McReportControl var33)
        {
            this._var9 = true;
            if (this._var8 && (var33 != null))
            {
                this._var0 = var33;
                if (var33.Parent is Section)
                {
                    SectionDesigner mtd = this._var5.mtd497[((Section) var33.Parent).Name];
                    if (this._var1 != mtd)
                    {
                        this._var1 = mtd;
                        this.var29(this._var1.mtd393, false);
                    }
                }
                this._var3.mtd387();
                this.var21(this.var20(var33));
                this._var5.mtd522(Rectangle.Empty);
            }
        }

        internal void mtd466(string var31, string var34)
        {
            this._var9 = true;
            if ((this._var8 && (var31 != null)) && (var34 != null))
            {
                Section section = this._var6.mtd268.Sections[var31];
                if (section != null)
                {
                    McReportControl control = section.Controls[var34];
                    if (control != null)
                    {
                        this.mtd502(new object[] { control }, SelectionTypes.Replace);
                    }
                }
            }
        }

        internal void mtd467()
        {
            if (this._var9)
            {
                this.var29(null, true);
                this._var9 = false;
            }
        }

        internal void mtd479()
        {
            this._var5.mtd522(this._var1.mtd519);
        }

        internal void mtd491(Border var59)
        {
            foreach (mtd562 mtd in this._var3)
            {
                mtd.mtd563.Border = var59;
                TypeDescriptor.GetProperties(mtd.mtd563)["Border"].SetValue(mtd.mtd563, var59);
            }
            this.var60();
        }

        internal void mtd502(ICollection var64, SelectionTypes var65)
        {
            this._var6.mtd428.SetSelectedComponents(var64, var65);
        }

        internal void mtd503(bool var53)
        {
            this._var3.mtd387();
            if (var53)
            {
                this.mtd479();
            }
        }

        internal void mtd520(Graphics g)
        {
            if (this._var13 | this._var39)
            {
                using (Pen pen = new Pen(Color.Black, 1f))
                {
                    pen.DashStyle = DashStyle.Dash;
                    foreach (mtd562 mtd in this._var3)
                    {
                        if (mtd.mtd563.Type == ControlType.Line)
                        {
                            Point[] pointArray = mtd.mtd569;
                            g.DrawLine(SystemPens.WindowFrame, pointArray[0], pointArray[1]);
                            continue;
                        }
                        g.DrawRectangle(pen, mtd.mtd570);
                    }
                }
            }
        }

        internal void mtd521(mtd562 var28, Graphics g)
        {
            Rectangle insideRect = new Rectangle();
            Rectangle outsideRect = new Rectangle();
            if (var28 == null)
            {
                foreach (mtd562 mtd in this._var3)
                {
                    if (mtd.mtd563.Type != ControlType.Line)
                    {
                        insideRect = Rectangle.Truncate(mtd.mtd563.Bounds);
                        insideRect.Width++;
                        insideRect.Height++;
                        outsideRect = insideRect;
                        outsideRect.Inflate(6, 6);
                        ControlPaint.DrawSelectionFrame(g, false, outsideRect, insideRect, SystemColors.Control);
                    }
                    mtd.mtd573(g);
                }
            }
            else
            {
                if (var28.mtd563.Type != ControlType.Line)
                {
                    insideRect = Rectangle.Round(var28.mtd563.Bounds);
                    insideRect.Width++;
                    insideRect.Height++;
                    outsideRect = insideRect;
                    outsideRect.Inflate(6, 6);
                    ControlPaint.DrawSelectionFrame(g, false, outsideRect, insideRect, SystemColors.Control);
                }
                var28.mtd573(g);
            }
        }

        internal bool mtd523()
        {
            if (this._var13)
            {
                this._var13 = false;
                if (this._var18 != this._var16)
                {
                    this.var26(this._var18.X - this._var16.X, this._var18.Y - this._var16.Y);
                    return true;
                }
                this.var27();
                this.mtd479();
                this._var5.mtd516(LayoutChangedType.ControlMove);
            }
            return false;
        }

        internal bool mtd524()
        {
            if (this._var39)
            {
                this._var39 = false;
                if (this._var35 != this._var36)
                {
                    using (DesignerTransaction transaction = this._var6.mtd426.CreateTransaction("Resize-McReportControl"))
                    {
                        foreach (mtd562 mtd in this._var3)
                        {
                            mtd.mtd567();
                        }
                        transaction.Commit();
                    }
                    this._var5.mtd516(LayoutChangedType.ControlSize);
                    return true;
                }
            }
            return false;
        }

        internal bool mtd528(object var40, MouseEventArgs e)
        {
            if (this._var1 != null)
            {
                this._var7 = this.var41(new Point(e.X - this._var1.mtd480.X, e.Y - this._var1.mtd480.Y), false);
                if (this._var7 != null)
                {
                    return true;
                }
                if (this._var39)
                {
                    this._var15.Y = this._var1.mtd480.Y + (((int) Math.Floor((double) (((double) (e.Y - this._var1.mtd480.Y)) / 6.0))) * 6);
                    this._var15.X = this._var1.mtd480.X + (((int) Math.Floor((double) (((double) (e.X - this._var1.mtd480.X)) / 6.0))) * 6);
                    int num = this._var15.X - this._var14.X;
                    int num2 = this._var15.Y - this._var14.Y;
                    if (this._var38 == 1)
                    {
                        this.var43(ref num);
                        this.var44(ref num2);
                        this.var45();
                    }
                    else if (this._var38 == 2)
                    {
                        this.var43(ref num);
                        this.var45();
                    }
                    else if (this._var38 == 3)
                    {
                        this.var43(ref num);
                        this.var46(ref num2);
                        this.var45();
                    }
                    else if (this._var38 == 4)
                    {
                        this.var46(ref num2);
                        this.var45();
                    }
                    else if (this._var38 == 5)
                    {
                        this.var47(ref num);
                        this.var46(ref num2);
                        this.var45();
                    }
                    else if (this._var38 == 6)
                    {
                        this.var47(ref num);
                        this.var45();
                    }
                    else if (this._var38 == 7)
                    {
                        this.var47(ref num);
                        this.var44(ref num2);
                        this.var45();
                    }
                    else if (this._var38 == 8)
                    {
                        this.var44(ref num2);
                        this.var45();
                    }
                    else if ((this._var38 == 9) | (this._var38 == 10))
                    {
                        this.var48(ref num, ref num2);
                        this.var45();
                    }
                    this._var14.X += num;
                    this._var14.Y += num2;
                    this.mtd479();
                }
            }
            return false;
        }

        internal void mtd529()
        {
            if (this._var7 != null)
            {
                this._var5.Cursor = this._var7.mtd568;
            }
        }

        internal void mtd530(MouseEventArgs e)
        {
            if (this._var13 && (e.Button == MouseButtons.Left))
            {
                this._var15.Y = this._var1.mtd480.Y + (((int) Math.Floor((double) (((double) (e.Y - this._var1.mtd480.Y)) / 6.0))) * 6);
                this._var15.X = this._var1.mtd480.X + (((int) Math.Floor((double) (((double) (e.X - this._var1.mtd480.X)) / 6.0))) * 6);
                int num = this._var15.X - this._var14.X;
                int num2 = this._var15.Y - this._var14.Y;
                this.var24(ref num, ref num2);
                this.var25(num, num2);
                this._var14.X += num;
                this._var14.Y += num2;
                this._var18.X += num;
                this._var18.Y += num2;
                this.mtd479();
            }
        }

        internal bool mtd532(object var40, MouseEventArgs e)
        {
            if (this._var1 == null)
            {
                return false;
            }
            mtd561 mtd = this.var41(new Point(e.X - this._var1.mtd480.X, e.Y - this._var1.mtd480.Y), true);
            if (mtd == null)
            {
                return false;
            }
            this._var38 = mtd.mtd504;
            if ((e.Button & MouseButtons.Left) > MouseButtons.None)
            {
                this._var39 = true;
                this.var42();
                foreach (mtd562 mtd2 in this._var3)
                {
                    mtd2.mtd566(this._var38, this._var36);
                }
                this._var14.X = this._var1.mtd480.X + (((int) Math.Floor((double) (((double) (e.X - this._var1.mtd480.X)) / 6.0))) * 6);
                this._var14.Y = this._var1.mtd480.Y + (((int) Math.Floor((double) (((double) (e.Y - this._var1.mtd480.Y)) / 6.0))) * 6);
            }
            return true;
        }

        internal void mtd533(MouseEventArgs e)
        {
            mtd562 mtd = this.var19(this._var0);
            if (this._var3.mtd166 > 0)
            {
                if (mtd == null)
                {
                    if (Control.ModifierKeys == Keys.Shift)
                    {
                        mtd = this.var20(this._var0);
                    }
                    else
                    {
                        this.mtd503(true);
                        mtd = this.var20(this._var0);
                    }
                }
            }
            else
            {
                mtd = this.var20(this._var0);
            }
            this.var21(mtd);
            this._var8 = false;
            this.var22();
            this._var8 = true;
            this.var23();
            this.mtd479();
            if ((e.Button == MouseButtons.Left) && (Control.ModifierKeys != Keys.Shift))
            {
                this._var13 = true;
                this._var14.X = this._var1.mtd480.X + (((int) Math.Floor((double) (((double) (e.X - this._var1.mtd480.X)) / 6.0))) * 6);
                this._var14.Y = this._var1.mtd480.Y + (((int) Math.Floor((double) (((double) (e.Y - this._var1.mtd480.Y)) / 6.0))) * 6);
            }
        }

        internal void mtd535(Rectangle var56)
        {
            Section section = this._var1.mtd393;
            var56.X -= this._var1.mtd480.X;
            var56.Y -= this._var1.mtd480.Y;
            this._var4 = false;
            foreach (McReportControl control in section.Controls)
            {
                if (var56.IntersectsWith(Rectangle.Round(control.Bounds)))
                {
                    this.var20(control);
                }
            }
            this._var4 = true;
            if (this._var3.mtd166 > 0)
            {
                this._var0 = this._var3[0].mtd563;
                this.var21(this._var3[0]);
                this.var23();
                object[] objArray = new object[this._var3.mtd166];
                int index = 0;
                foreach (mtd562 mtd in this._var3)
                {
                    objArray[index] = mtd.mtd563;
                    index++;
                }
                this.mtd502(objArray, SelectionTypes.Replace);
            }
        }

        internal void mtd548()
        {
            foreach (mtd562 mtd in this._var3)
            {
                mtd.mtd574();
                mtd.mtd571();
            }
        }

        internal void mtd575(McReportControl var55)
        {
            foreach (mtd562 mtd in this._var3)
            {
                if (mtd.mtd563 == var55)
                {
                    this._var3.mtd217(mtd);
                    break;
                }
            }
        }

        internal void mtd575(mtd562 var54)
        {
            this._var3.mtd217(var54);
        }

        private void ForceSectionUpdate(Section s)
        {
            TypeDescriptor.GetProperties(s)["IsDirty"].SetValue(s, s.IsDirty);
        }

        private void var12(int var62, object var63)
        {
            if (this._var4)
            {
                this._var3.mtd213();
            }
        }

        private mtd562 var19(McReportControl var28)
        {
            foreach (mtd562 mtd in this._var3)
            {
                if (string.Compare(mtd.mtd563.Name, var28.Name, true) == 0)
                {
                    return mtd;
                }
            }
            return null;
        }

        private mtd562 var20(McReportControl var33)
        {
            mtd562 mtd = new mtd562(var33);
            this._var3.mtd2(mtd);
            return mtd;
        }

        private void var21(mtd562 var28)
        {
            foreach (mtd562 mtd in this._var3)
            {
                bool flag;
                if (string.Compare(mtd.mtd563.Name, var28.mtd563.Name, true) == 0)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
                foreach (object obj2 in mtd.mtd564)
                {
                    ((mtd561) obj2).mtd565 = flag;
                }
            }
        }

        private void var22()
        {
            if (this._var0 != null)
            {
                SelectionTypes click;
                if (this._var3.mtd166 > 1)
                {
                    click = SelectionTypes.Primary;//.Click;
                }
                else
                {
                    click = SelectionTypes.Replace;
                }
                this._var6.mtd428.SetSelectedComponents(new object[] { this._var0 }, click);
            }
        }

        private void var23()
        {
            Rectangle rectangle;
            mtd562 mtd = this._var3[0];
            if (mtd.mtd563.Type == ControlType.Line)
            {
                rectangle = this.var51(mtd.mtd569);
            }
            else
            {
                rectangle = mtd.mtd570;
            }
            for (int i = 1; i <= (this._var3.mtd166 - 1); i++)
            {
                mtd = this._var3[i];
                if (mtd.mtd563.Type == ControlType.Line)
                {
                    Rectangle b = this.var51(mtd.mtd569);
                    rectangle = Rectangle.Union(rectangle, b);
                }
                else
                {
                    rectangle = Rectangle.Union(rectangle, mtd.mtd570);
                }
            }
            this._var16 = rectangle;
            this._var17 = this._var16;
            this._var18 = this._var16;
        }

        private void var24(ref int var49, ref int var50)
        {
            if ((this._var18.X + var49) < 0)
            {
                if (this._var18.X > 0)
                {
                    var49 = -this._var18.X;
                }
                else
                {
                    var49 = 0;
                }
            }
            if ((this._var18.Y + var50) < 0)
            {
                if (this._var18.Y > 0)
                {
                    var50 = -this._var18.Y;
                }
                else
                {
                    var50 = 0;
                }
            }
        }

        private void var25(int var49, int var50)
        {
            foreach (mtd562 mtd in this._var3)
            {
                mtd.mtd572(var49, var50);
            }
        }

        private void var26(int var49, int var50)
        {
            if ((var49 != 0) || (var50 != 0))
            {
                using (DesignerTransaction transaction = this._var6.mtd426.CreateTransaction("Change-McReportControl-Location"))
                {
                    foreach (mtd562 mtd in this._var3)
                    {
                        if (mtd.mtd563.Type == ControlType.PageBreak)
                        {
                            TypeDescriptor.GetProperties(mtd.mtd563)["Top"].SetValue(mtd.mtd563, mtd.mtd33.Y + var50);
                            continue;
                        }
                        if (mtd.mtd563.Type == ControlType.Line)
                        {
                            McLine component = (McLine) mtd.mtd563;
                            PropertyDescriptorCollection descriptors = TypeDescriptor.GetProperties(component);
                            descriptors["X1"].SetValue(component, component.X1 + var49);
                            descriptors["Y1"].SetValue(component, component.Y1 + var50);
                            descriptors["X2"].SetValue(component, component.X2 + var49);
                            descriptors["Y2"].SetValue(component, component.Y2 + var50);
                            continue;
                        }
                        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(mtd.mtd563);
                        properties["Left"].SetValue(mtd.mtd563, mtd.mtd33.X + var49);
                        properties["Top"].SetValue(mtd.mtd563, mtd.mtd33.Y + var50);
                    }
                    transaction.Commit();
                }
            }
        }

        private void var27()
        {
            foreach (mtd562 mtd in this._var3)
            {
                mtd.mtd571();
            }
        }

        private void var29(Section var30, bool var32)
        {
            this._var9 = true;
            if (this._var8)
            {
                mtd389 mtd = this._var5.mtd497;
                if (var30 == null)
                {
                    this._var1 = null;
                    this._var0 = null;
                    foreach (SectionDesigner mtd3 in mtd)
                    {
                        mtd3.mtd558 = SystemColors.Control;
                    }
                }
                else
                {
                    if ((this._var1 == null) || (this._var1.mtd393 != var30))
                    {
                        this._var1 = mtd[var30.Name];
                    }
                    foreach (SectionDesigner mtd2 in mtd)
                    {
                        if (string.Compare(mtd2.mtd91, var30.Name) == 0)
                        {
                            mtd2.mtd558 = Color.Blue;//.SteelBlue;
                            continue;
                        }
                        mtd2.mtd558 = SystemColors.Control;
                    }
                }
                this._var3.mtd387();
                if (var32)
                {
                    this._var5.mtd522(Rectangle.Empty);
                }
            }
        }

        private mtd561 var41(Point var57, bool var58)
        {
            foreach (mtd562 mtd in this._var3)
            {
                foreach (object obj2 in mtd.mtd564)
                {
                    mtd561 mtd2 = (mtd561) obj2;
                    if (mtd2.mtd576().Contains(var57))
                    {
                        if (var58)
                        {
                            if (mtd.mtd563.Type == ControlType.Line)
                            {
                                McLine line = (McLine) mtd.mtd563;
                                this._var37[0] = new Point((int) line.X1, (int) line.Y1);
                                this._var37[1] = new Point((int) line.X2, (int) line.Y2);
                                return mtd2;
                            }
                            this._var16 = Rectangle.Round(mtd.mtd563.Bounds);
                            this._var18 = this._var16;
                        }
                        return mtd2;
                    }
                }
            }
            return null;
        }

        private void var42()
        {
            if (((this._var38 == 1) | (this._var38 == 2)) | (this._var38 == 8))
            {
                this._var36 = new Point(this._var16.X, this._var16.Y);
            }
            else if ((this._var38 == 3) | (this._var38 == 4))
            {
                this._var36 = new Point(this._var16.X, this._var16.Bottom);
            }
            else if (this._var38 == 5)
            {
                this._var36 = new Point(this._var16.Right, this._var16.Bottom);
            }
            else if ((this._var38 == 6) | (this._var38 == 7))
            {
                this._var36 = new Point(this._var16.Right, this._var16.Y);
            }
            else if (this._var38 == 9)
            {
                this._var36 = new Point(this._var37[0].X, this._var37[0].Y);
            }
            else if (this._var38 == 10)
            {
                this._var36 = new Point(this._var37[1].X, this._var37[1].Y);
            }
        }

        private void var43(ref int var49)
        {
            int num = this._var36.X + var49;
            if (num < 0)
            {
                if (this._var36.X > 0)
                {
                    var49 = -this._var36.X;
                }
                else
                {
                    var49 = 0;
                }
            }
            else if ((num > (this._var16.Right - 6)) && (this._var36.X > (this._var16.Right - 6)))
            {
                var49 = 0;
            }
            this._var36.X += var49;
        }

        private void var44(ref int var50)
        {
            if (((this._var36.Y + var50) > (this._var16.Bottom - 6)) && (this._var36.Y > (this._var16.Bottom - 6)))
            {
                var50 = 0;
            }
            this._var36.Y += var50;
        }

        private void var45()
        {
            foreach (mtd562 mtd in this._var3)
            {
                mtd.mtd572(this._var36, this._var38);
            }
        }

        private void var46(ref int var50)
        {
            if (((this._var36.Y + var50) < (this._var16.Y + 6)) && (this._var36.Y < (this._var16.Y + 6)))
            {
                var50 = 0;
            }
            this._var36.Y += var50;
        }

        private void var47(ref int var49)
        {
            if (((this._var36.X + var49) < (this._var16.X + 6)) && (this._var36.X < (this._var16.X + 6)))
            {
                var49 = 0;
            }
            this._var36.X += var49;
        }

        private void var48(ref int var49, ref int var50)
        {
            int num = this._var36.X + var49;
            if (num < 0)
            {
                if (this._var36.X > 0)
                {
                    var49 = -this._var36.X;
                }
                else
                {
                    var49 = 0;
                }
            }
            int num2 = this._var36.Y + var50;
            if (num2 < 0)
            {
                if (this._var36.Y > 0)
                {
                    var50 = -this._var36.Y;
                }
                else
                {
                    var50 = 0;
                }
            }
            this._var36.X += var49;
            this._var36.Y += var50;
        }

        private Rectangle var51(Point[] var52)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddLine(var52[0], var52[1]);
                return Rectangle.Round(path.GetBounds());
            }
        }

        private void var60()
        {
            this._var1.mtd496(this._var5.mtd495);
            this._var5.mtd522(Rectangle.Empty);
        }

        internal int mtd431
        {
            get
            {
                return this._var3.mtd166;
            }
        }

        internal SectionDesigner mtd474
        {
            get
            {
                return this._var1;
            }
            set
            {
                if ((this._var1 != null) && (this._var1.mtd504 != value.mtd504))
                {
                    this.mtd503(true);
                }
                this._var1 = value;
            }
        }

        internal McReportControl mtd490
        {
            get
            {
                return this._var0;
            }
            set
            {
                this._var0 = value;
            }
        }

        internal Section mtd517
        {
            get
            {
                if (this._var1 != null)
                {
                    return this._var1.mtd393;
                }
                return null;
            }
        }

        internal Point mtd531
        {
            set
            {
                this._var2 = value;
            }
        }

        private class mtd560 : IEnumerable
        {
            private ArrayList _var82 = new ArrayList();

            internal event CollectionChange mtd390;

            internal event CollectionChange mtd391;

            internal mtd560()
            {
            }

            internal void mtd1()
            {
                this._var82.Sort(new mtd580());
            }

            internal void mtd2(ReportControlDesigner.mtd562 var54)
            {
                int count = this._var82.Count;
                this._var82.Add(var54);
                if (this.mtd390 != null)
                {
                    this.mtd390(count, var54);
                }
            }

            internal void mtd213()
            {
                this._var82.Sort(new mtd579());
            }

            internal int mtd215(ReportControlDesigner.mtd562 var63)
            {
                return this._var82.IndexOf(var63);
            }

            internal int mtd215(string var83)
            {
                int num = 0;
                foreach (object obj2 in this._var82)
                {
                    if (string.Compare(((ReportControlDesigner.mtd562)obj2).mtd563.Name, var83, true) == 0)
                    {
                        return num;
                    }
                    num++;
                }
                return -1;
            }

            internal void mtd216(int var62, ReportControlDesigner.mtd562 var63)
            {
                this._var82.Insert(var62, var63);
                if (this.mtd390 != null)
                {
                    this.mtd390(var62, var63);
                }
            }

            internal void mtd217(ReportControlDesigner.mtd562 var63)
            {
                int index = this._var82.IndexOf(var63);
                this._var82.RemoveAt(index);
                if (this.mtd391 != null)
                {
                    this.mtd391(index, var63);
                }
            }

            internal void mtd387()
            {
                this._var82.Clear();
            }

            internal void mtd394(int var62)
            {
                ReportControlDesigner.mtd562 mtd = (ReportControlDesigner.mtd562)this._var82[var62];
                this._var82.RemoveAt(var62);
                if (this.mtd391 != null)
                {
                    this.mtd391(var62, mtd);
                }
            }

            internal void mtd577()
            {
                this._var82.Sort(new mtd581());
            }

            public IEnumerator GetEnumerator()
            {
                return this._var82.GetEnumerator();
            }

            internal int mtd166
            {
                get
                {
                    return this._var82.Count;
                }
            }

            internal ReportControlDesigner.mtd562 this[int var62]
            {
                get
                {
                    return (ReportControlDesigner.mtd562)this._var82[var62];
                }
            }

            internal ReportControlDesigner.mtd562 this[string var83]
            {
                get
                {
                    foreach (object obj2 in this._var82)
                    {
                        ReportControlDesigner.mtd562 mtd = (ReportControlDesigner.mtd562)obj2;
                        if (string.Compare(mtd.mtd563.Name, var83, true) == 0)
                        {
                            return mtd;
                        }
                    }
                    return null;
                }
            }

            internal class mtd579 : IComparer
            {
                public int Compare(object var84, object var85)
                {
                    float index = ((ReportControlDesigner.mtd562)var84).mtd563.Index;
                    float num2 = ((ReportControlDesigner.mtd562)var85).mtd563.Index;
                    int num3 = 0;
                    if (index > num2)
                    {
                        num3 = 1;
                    }
                    if (index < num2)
                    {
                        num3 = -1;
                    }
                    return num3;
                }
            }

            internal class mtd580 : IComparer
            {
                public int Compare(object var84, object var85)
                {
                    float top = ((ReportControlDesigner.mtd562)var84).mtd563.Top;
                    float num2 = ((ReportControlDesigner.mtd562)var85).mtd563.Top;
                    int num3 = 0;
                    if (top > num2)
                    {
                        num3 = 1;
                    }
                    if (top < num2)
                    {
                        num3 = -1;
                    }
                    return num3;
                }
            }

            internal class mtd581 : IComparer
            {
                public int Compare(object var84, object var85)
                {
                    float left = ((ReportControlDesigner.mtd562)var84).mtd563.Left;
                    float num2 = ((ReportControlDesigner.mtd562)var85).mtd563.Left;
                    int num3 = 0;
                    if (left > num2)
                    {
                        num3 = 1;
                    }
                    if (left < num2)
                    {
                        num3 = -1;
                    }
                    return num3;
                }
            }
        }

        private class mtd561
        {
            private int _var74;
            private bool _var77;
            private Cursor _var78;
            private Point _var79;
            private Size _var80;

            internal mtd561(int var62, Cursor var81)
            {
                this._var74 = var62;
                this._var78 = var81;
                this._var80 = new Size(6, 6);
            }

            internal Rectangle mtd576()
            {
                return new Rectangle(this._var79, this._var80);
            }

            internal Point mtd33
            {
                set
                {
                    this._var79 = value;
                }
            }

            internal int mtd504
            {
                get
                {
                    return this._var74;
                }
            }

            internal bool mtd565
            {
                get
                {
                    return this._var77;
                }
                set
                {
                    this._var77 = value;
                }
            }

            internal Cursor mtd568
            {
                get
                {
                    return this._var78;
                }
            }
        }

        internal class mtd562
        {
            private Point[] _var37 = new Point[2];
            private McReportControl _var66;
            private Rectangle _var67 = new Rectangle();
            private Point[] _var68 = new Point[2];
            private ArrayList _var69 = new ArrayList();
            private Cursor[] _var70 = new Cursor[] { Cursors.SizeNWSE, Cursors.SizeWE, Cursors.SizeNESW, Cursors.SizeNS, Cursors.SizeNWSE, Cursors.SizeWE, Cursors.SizeNESW, Cursors.SizeNS, Cursors.Cross };
            internal Rectangle mtd570 = new Rectangle();
            internal Point mtd578;

            internal mtd562(McReportControl var33)
            {
                this._var66 = var33;
                this.mtd571();
                if (this._var66.Type != ControlType.PageBreak)
                {
                    if (this._var66.Type == ControlType.Line)
                    {
                        this.var71();
                    }
                    else
                    {
                        this.var72();
                    }
                }
            }

            internal void mtd566(int var74, Point var73)
            {
                if (this._var66.Type == ControlType.Line)
                {
                    if ((var74 == 1) | (var74 == 9))
                    {
                        this.mtd578 = new Point(this.mtd569[0].X - var73.X, this.mtd569[0].Y - var73.Y);
                    }
                    else
                    {
                        this.mtd578 = new Point(this.mtd569[1].X - var73.X, this.mtd569[1].Y - var73.Y);
                    }
                }
                else if ((((var74 == 1) | (var74 == 2)) | (var74 == 8)) | (var74 == 9))
                {
                    this.mtd578 = new Point(this.mtd570.X - var73.X, this.mtd570.Y - var73.Y);
                }
                else if ((var74 == 3) | (var74 == 4))
                {
                    this.mtd578 = new Point(this.mtd570.X - var73.X, this.mtd570.Bottom - var73.Y);
                }
                else if (var74 == 5)
                {
                    this.mtd578 = new Point(this.mtd570.Right - var73.X, this.mtd570.Bottom - var73.Y);
                }
                else if (((var74 == 6) | (var74 == 7)) | (var74 == 10))
                {
                    this.mtd578 = new Point(this.mtd570.Right - var73.X, this.mtd570.Y - var73.Y);
                }
            }

            internal void mtd567()
            {
                PropertyDescriptorCollection properties;
                if (this._var66.Type == ControlType.Line)
                {
                    McLine component = (McLine) this._var66;
                    properties = TypeDescriptor.GetProperties(component);
                    properties["X1"].SetValue(component, component.X1 + (this.mtd569[0].X - this._var68[0].X));
                    properties["Y1"].SetValue(component, component.Y1 + (this.mtd569[0].Y - this._var68[0].Y));
                    properties["X2"].SetValue(component, component.X2 + (this.mtd569[1].X - this._var68[1].X));
                    properties["Y2"].SetValue(component, component.Y2 + (this.mtd569[1].Y - this._var68[1].Y));
                }
                else if (this._var66.Type != ControlType.PageBreak)
                {
                    properties = TypeDescriptor.GetProperties(this._var66);
                    properties["Top"].SetValue(this._var66, this.mtd33.Y + (this.mtd570.Y - this._var67.Y));
                    properties["Left"].SetValue(this._var66, this.mtd33.X + (this.mtd570.X - this._var67.X));
                    properties["Width"].SetValue(this._var66, Math.Max((float) (this.mtd32.Width + (this.mtd570.Width - this._var67.Width)), (float) 6f));
                    properties["Height"].SetValue(this._var66, Math.Max((float) (this.mtd32.Height + (this.mtd570.Height - this._var67.Height)), (float) 6f));
                }
            }

            internal void mtd571()
            {
                if (this._var66.Type == ControlType.Line)
                {
                    McLine line = (McLine) this._var66;
                    Point point = new Point((int) line.X1, (int) line.Y1);
                    Point point2 = new Point((int) line.X2, (int) line.Y2);
                    this._var68[0] = point;
                    this._var68[1] = point2;
                    this._var37[0] = point;
                    this._var37[1] = point2;
                }
                else
                {
                    this._var67 = Rectangle.Round(this._var66.Bounds);
                    this.mtd570 = this._var67;
                }
            }

            internal void mtd572(Point var73, int var74)
            {
                if (this._var66.Type == ControlType.Line)
                {
                    int num = this.mtd578.X + var73.X;
                    if (num < 0)
                    {
                        num = 0;
                    }
                    int num2 = this.mtd578.Y + var73.Y;
                    if (num2 < 0)
                    {
                        num2 = 0;
                    }
                    if ((var74 == 1) | (var74 == 9))
                    {
                        this.mtd569[0].X = num;
                        this.mtd569[0].Y = num2;
                    }
                    else
                    {
                        this.mtd569[1].X = num;
                        this.mtd569[1].Y = num2;
                    }
                }
                else if ((var74 == 1) | (var74 == 9))
                {
                    this.var43(var73.X);
                    this.var44(var73.Y);
                }
                else if (var74 == 2)
                {
                    this.var43(var73.X);
                }
                else if (var74 == 3)
                {
                    this.var43(var73.X);
                    this.var46(var73.Y);
                }
                else if (var74 == 4)
                {
                    this.var46(var73.Y);
                }
                else if (var74 == 5)
                {
                    this.var47(var73.X);
                    this.var46(var73.Y);
                }
                else if (var74 == 6)
                {
                    this.var47(var73.X);
                }
                else if ((var74 == 7) | (var74 == 10))
                {
                    this.var47(var73.X);
                    this.var44(var73.Y);
                }
                else if (var74 == 8)
                {
                    this.var44(var73.Y);
                }
            }

            internal void mtd572(int var49, int var50)
            {
                if (this._var66.Type == ControlType.Line)
                {
                    Point point = this._var37[0];
                    Point point2 = this._var37[1];
                    this._var37.SetValue(new Point(point.X + var49, point.Y + var50), 0);
                    this._var37.SetValue(new Point(point2.X + var49, point2.Y + var50), 1);
                }
                else
                {
                    this.mtd570.X = Math.Max(this.mtd570.X + var49, 0);
                    this.mtd570.Y = Math.Max(this.mtd570.Y + var50, 0);
                }
            }

            internal void mtd573(Graphics g)
            {
                using (Brush brush = new SolidBrush(Color.Yellow))//.Aqua))
                {
                    foreach (object obj2 in this._var69)
                    {
                        ReportControlDesigner.mtd561 mtd = (ReportControlDesigner.mtd561)obj2;
                        Rectangle rect = mtd.mtd576();
                        if (mtd.mtd565)
                        {
                            g.FillRectangle(brush, rect);
                            g.DrawRectangle(SystemPens.WindowFrame, rect);
                            continue;
                        }
                        g.FillRectangle(SystemBrushes.Window, rect);
                        g.DrawRectangle(SystemPens.WindowFrame, rect);
                    }
                }
            }

            internal void mtd574()
            {
                if (this._var66.Type != ControlType.PageBreak)
                {
                    int width = (int) this._var66.Size.Width;
                    int height = (int) this._var66.Size.Height;
                    int y = ((int) this._var66.Location.Y) - 6;
                    int num4 = ((int) this._var66.Location.Y) + ((height / 2) - 3);
                    int num5 = ((int) this._var66.Location.Y) + height;
                    int x = ((int) this._var66.Location.X) - 6;
                    int num7 = ((int) this._var66.Location.X) + ((width / 2) - 3);
                    int num8 = ((int) this._var66.Location.X) + width;
                    if (this._var66.Type == ControlType.Line)
                    {
                        McLine line = (McLine) this._var66;
                        ((ReportControlDesigner.mtd561)this._var69[0]).mtd33 = new Point(((int)line.X1) - 3, ((int)line.Y1) - 3);
                        ((ReportControlDesigner.mtd561)this._var69[1]).mtd33 = new Point(((int)line.X2) - 3, ((int)line.Y2) - 3);
                    }
                    else
                    {
                        ((ReportControlDesigner.mtd561)this._var69[0]).mtd33 = new Point(x, y);
                        ((ReportControlDesigner.mtd561)this._var69[1]).mtd33 = new Point(x, num4);
                        ((ReportControlDesigner.mtd561)this._var69[2]).mtd33 = new Point(x, num5);
                        ((ReportControlDesigner.mtd561)this._var69[3]).mtd33 = new Point(num7, num5);
                        ((ReportControlDesigner.mtd561)this._var69[4]).mtd33 = new Point(num8, num5);
                        ((ReportControlDesigner.mtd561)this._var69[5]).mtd33 = new Point(num8, num4);
                        ((ReportControlDesigner.mtd561)this._var69[6]).mtd33 = new Point(num8, y);
                        ((ReportControlDesigner.mtd561)this._var69[7]).mtd33 = new Point(num7, y);
                    }
                }
            }

            private void var43(int var75)
            {
                int num = this.mtd578.X + var75;
                if (num < 0)
                {
                    num = 0;
                }
                else if (num > (this._var67.Right - 6))
                {
                    num = this._var67.Right - 6;
                }
                this.mtd570.X = num;
                this.mtd570.Width = this._var67.Right - num;
            }

            private void var44(int var76)
            {
                int num = this.mtd578.Y + var76;
                if (num < 0)
                {
                    num = 0;
                }
                else if (num > (this._var67.Bottom - 6))
                {
                    num = this._var67.Bottom - 6;
                }
                this.mtd570.Y = num;
                this.mtd570.Height = this._var67.Bottom - num;
            }

            private void var46(int var76)
            {
                int num = this.mtd578.Y + var76;
                if (num < (this._var67.Top + 6))
                {
                    num = this._var67.Top + 6;
                }
                this.mtd570.Height = num - this._var67.Top;
            }

            private void var47(int var75)
            {
                int num = this.mtd578.X + var75;
                if (num < (this._var67.X + 6))
                {
                    num = this._var67.X + 6;
                }
                this.mtd570.Width = num - this._var67.X;
            }

            private void var71()
            {
                ReportControlDesigner.mtd561 mtd = new ReportControlDesigner.mtd561(9, this._var70[8]);
                this._var69.Add(mtd);
                mtd = new ReportControlDesigner.mtd561(10, this._var70[8]);
                this._var69.Add(mtd);
                this.mtd574();
            }

            private void var72()
            {
                for (int i = 0; i <= 7; i++)
                {
                    ReportControlDesigner.mtd561 mtd = new ReportControlDesigner.mtd561(i + 1, this._var70[i]);
                    this._var69.Add(mtd);
                }
                this.mtd574();
            }

            internal SizeF mtd32
            {
                get
                {
                    return this._var66.Size;
                }
                set
                {
                    this._var66.Size = value;
                }
            }

            internal PointF mtd33
            {
                get
                {
                    return this._var66.Location;
                }
                set
                {
                    this._var66.Location = value;
                }
            }

            internal McReportControl mtd563
            {
                get
                {
                    return this._var66;
                }
            }

            internal ArrayList mtd564
            {
                get
                {
                    return this._var69;
                }
            }

            internal Point[] mtd569
            {
                get
                {
                    return this._var37;
                }
                set
                {
                    this._var37 = value;
                }
            }

            internal Rectangle mtd576
            {
                get
                {
                    return Rectangle.Round(this._var66.Bounds);
                }
            }
        }
    }
}

