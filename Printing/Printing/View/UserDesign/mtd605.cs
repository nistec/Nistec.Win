namespace MControl.Printing.View.Design.UserDesigner
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.ComponentModel.Design;

    internal class mtd605 : ISite, IServiceProvider, IDictionaryService
    {
        private mtd426 _var0;
        private IComponent _var1;
        private Hashtable _var2;
        private string _var3;

        public mtd605(mtd426 var4, IComponent var5, string var6)
        {
            this._var0 = var4;
            this._var3 = var6;
            this._var1 = var5;
        }

        public object GetKey(object var9)
        {
            if (this._var2 != null)
            {
                IDictionaryEnumerator enumerator = this._var2.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    if (enumerator.Value == var9)
                    {
                        return enumerator.Key;
                    }
                }
            }
            return null;
        }

        public object GetService(Type var7)
        {
            if (var7 == typeof(IDictionaryService))
            {
                return this;
            }
            return this._var0.GetService(var7);
        }

        public object GetValue(object var8)
        {
            if (this._var2 != null)
            {
                return this._var2[var8];
            }
            return null;
        }

        public void SetValue(object var8, object var9)
        {
            if (this._var2 == null)
            {
                this._var2 = new Hashtable();
            }
            if (var9 == null)
            {
                this._var2.Remove(var8);
            }
            else
            {
                this._var2[var8] = var9;
            }
        }

        public IComponent Component
        {
            get
            {
                return this._var1;
            }
        }

        public IContainer Container
        {
            get
            {
                return this._var0.Container;
            }
        }

        public bool DesignMode
        {
            get
            {
                return true;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Name
        {
            get
            {
                return this._var3;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Cannot set a component's name to a null value.");
                }
                if (this._var3 != value)
                {
                    if (this._var0.mtd600(value))
                    {
                        throw new ArgumentException(string.Format("There is already a component named '{0}'.  Components must have unique names, and names must be case-insensitive.  A name also cannot conflict with the name of any component in an inherited class.", value));
                    }
                    string str = this._var3;
                    if (!this._var0.IsLoading)
                    {
                        MemberDescriptor descriptor = TypeDescriptor.CreateProperty(this._var1.GetType(), "Name", typeof(string), new Attribute[0]);
                        this._var0.OnComponentChanging(this._var1, descriptor);
                        this._var3 = value;
                        this._var0.mtd603(this._var1, str, this._var3);
                        this._var0.OnComponentChanged(this._var1, descriptor, str, this._var3);
                    }
                }
            }
        }
    }
}

