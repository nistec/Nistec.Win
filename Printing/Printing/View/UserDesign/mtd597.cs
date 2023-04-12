namespace MControl.Printing.View.Design.UserDesigner
{
    using System;
    using System.Collections;
    using System.ComponentModel.Design;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    internal class mtd597 : ISelectionService
    {
        private ArrayList _var0 = new ArrayList();

        public event EventHandler SelectionChanged;

        public event EventHandler SelectionChanging;

        internal mtd597()
        {
        }

        public bool GetComponentSelected(object var1)
        {
            return this._var0.Contains(var1);
        }

        public ICollection GetSelectedComponents()
        {
            return this._var0.ToArray();
        }

        public void SetSelectedComponents(ICollection var2, SelectionTypes var3)
        {
            bool flag = false;
            bool flag2 = false;
            if ((var2 != null) && (var2.Count != 0))
            {
                if (this.SelectionChanging != null)
                {
                    this.SelectionChanging(this, EventArgs.Empty);
                }
                if ((var3 & SelectionTypes.Primary) == SelectionTypes.Primary)//.Click)
                {
                    flag = (Control.ModifierKeys & Keys.Control) == Keys.Control;
                    flag2 = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
                }
                if (var3 == SelectionTypes.Replace)
                {
                    this._var0.Clear();
                    foreach (object obj2 in var2)
                    {
                        if ((obj2 != null) && !this._var0.Contains(obj2))
                        {
                            this._var0.Add(obj2);
                        }
                    }
                }
                else
                {
                    if ((!flag && !flag2) && (var2.Count == 1))
                    {
                        foreach (object obj3 in var2)
                        {
                            if (!this._var0.Contains(obj3))
                            {
                                this._var0.Clear();
                            }
                        }
                    }
                    foreach (object obj4 in var2)
                    {
                        if (obj4 == null)
                        {
                            continue;
                        }
                        if (flag || flag2)
                        {
                            if (this._var0.Contains(obj4))
                            {
                                this._var0.Remove(obj4);
                            }
                            else
                            {
                                this._var0.Insert(0, obj4);
                            }
                            continue;
                        }
                        if (!this._var0.Contains(obj4))
                        {
                            this._var0.Add(obj4);
                            continue;
                        }
                        this._var0.Remove(obj4);
                        this._var0.Insert(0, obj4);
                    }
                }
                if (this.SelectionChanged != null)
                {
                    this.SelectionChanged(this, EventArgs.Empty);
                }
            }
        }

        void ISelectionService.SetSelectedComponents(ICollection var2)
        {
            this.SetSelectedComponents(var2, SelectionTypes.Replace);
        }

        public object PrimarySelection
        {
            get
            {
                if (this._var0.Count > 0)
                {
                    return this._var0[0];
                }
                return null;
            }
        }

        public int SelectionCount
        {
            get
            {
                return this._var0.Count;
            }
        }
    }
}

