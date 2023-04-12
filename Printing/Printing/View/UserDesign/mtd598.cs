namespace MControl.Printing.View.Design.UserDesigner
{
    using MControl.Printing.View;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Runtime.Serialization;

    internal class mtd598 : IDesignerSerializationService
    {
        internal mtd598()
        {
        }

        public ICollection Deserialize(object var1)
        {
            ArrayList list = new ArrayList();
            foreach (mtd607 mtd in (ArrayList) var1)
            {
                list.Add(mtd.mtd563);
            }
            return list;
        }

        public object Serialize(ICollection var0)
        {
            ArrayList list = new ArrayList();
            foreach (object obj2 in var0)
            {
                mtd607 mtd = new mtd607(obj2 as McReportControl);
                list.Add(mtd);
            }
            return list;
        }

        [Serializable]
        private class mtd607 : ISerializable
        {
            private McReportControl _var2;

            internal mtd607(McReportControl var2)
            {
                this._var2 = var2;
            }

            private mtd607(SerializationInfo var3, StreamingContext var4)
            {
                this.var5(var3, var4);
            }

            public void GetObjectData(SerializationInfo var3, StreamingContext var4)
            {
                this.var6(var3, var4);
            }

            private void var5(SerializationInfo var3, StreamingContext context)
            {
                ControlType cType = (ControlType) var3.GetValue("ControlType", typeof(ControlType));
                this._var2 = this.var7(cType);
                if (this._var2 != null)
                {
                    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this._var2);
                    foreach (SerializationEntry entry in var3)
                    {
                        PropertyDescriptor descriptor = properties[entry.Name];
                        if (((descriptor != null) && (entry.Value != null)) && (descriptor.PropertyType == entry.ObjectType))
                        {
                            descriptor.SetValue(this._var2, entry.Value);
                        }
                    }
                }
            }

            private void var6(SerializationInfo var3, StreamingContext var4)
            {
                if (this._var2 != null)
                {
                    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this._var2);
                    var3.AddValue("ControlType", this._var2.Type);
                    foreach (PropertyDescriptor descriptor in properties)
                    {
                        if (descriptor.Attributes.Contains(mtd85.mtd86) && descriptor.ShouldSerializeValue(this._var2))
                        {
                            var3.AddValue(descriptor.Name, descriptor.GetValue(this._var2));
                        }
                    }
                }
            }

            private McReportControl var7(ControlType cType)
            {
                if (cType == ControlType.Label)
                {
                    return (McReportControl) Activator.CreateInstance(typeof(McLabel));
                }
                if (cType == ControlType.TextBox)
                {
                    return (McReportControl) Activator.CreateInstance(typeof(McTextBox));
                }
                if (cType == ControlType.Line)
                {
                    return (McReportControl) Activator.CreateInstance(typeof(McLine));
                }
                if (cType == ControlType.Shape)
                {
                    return (McReportControl) Activator.CreateInstance(typeof(McShape));
                }
                if (cType == ControlType.Picture)
                {
                    return (McReportControl) Activator.CreateInstance(typeof(McPicture));
                }
                if (cType == ControlType.PageBreak)
                {
                    return (McReportControl) Activator.CreateInstance(typeof(McPageBreak));
                }
                if (cType == ControlType.CheckBox)
                {
                    return (McReportControl) Activator.CreateInstance(typeof(McCheckBox));
                }
                if (cType == ControlType.RichTextField)
                {
                    return (McReportControl) Activator.CreateInstance(typeof(McRichText));
                }
                if (cType == ControlType.SubReport)
                {
                    return (McReportControl) Activator.CreateInstance(typeof(McSubReport));
                }
                return null;
            }

            internal McReportControl mtd563
            {
                get
                {
                    return this._var2;
                }
            }
        }
    }
}

