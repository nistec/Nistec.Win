using System;
using System.Data;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using mControl.Win32;
using System.Runtime.InteropServices;
using mControl.WinCtl.Controls;
using mControl.Util;


namespace mControl.Wizards.Controls
{

    public class ToolBarButtonCollection : mControl.Collections.CollectionWithEvents
    {
        internal IToolBar owner;
        internal Control parent;
        //internal ImageList imageList;

        public ToolBarButtonCollection() { }
        public ToolBarButtonCollection(IToolBar bar,Control ctl)
        {
            this.owner = bar;
            this.parent = ctl;
        }

        public CtlToolButton Add(CtlToolButton toolButton)
        {
            toolButton.ParentBar = this.owner;
            toolButton.Parent = parent;
            //value.Text=value.Site.Name;
            base.List.Add(toolButton as object);
            return toolButton;
        }

        public void AddRange(ToolBarButtonCollection values)
        {
            foreach (CtlToolButton itm in values)
            {
                Add(itm);
            }
        }

        public void AddRange(ToolBarButtonCollection values, IToolBar ctl)
        {
            this.owner = ctl;
            foreach (CtlToolButton itm in values)
            {
                itm.ParentBar = ctl;
                itm.Parent = parent;
                Add(itm);
            }
        }

        public void AddRange(CtlToolButton[] values)
        {
            foreach (CtlToolButton itm in values)
            {
                Add(itm);
            }
        }

        public void Remove(CtlToolButton value)
        {
            base.List.Remove(value as object);
        }

        public void Insert(int index, CtlToolButton itm)
        {
            itm.Parent = parent;
            itm.ParentBar = this.owner;
            base.List.Insert(index, itm as object);
        }

        public bool Contains(CtlToolButton value)
        {
            return base.List.Contains(value as object);
        }

        public CtlToolButton this[int index]
        {
            get { return (base.List[index] as CtlToolButton); }
        }

        public CtlToolButton this[string name]
        {
            get
            {
                // Search for a Page with a matching title
                foreach (CtlToolButton itm in base.List)
                    if (itm.Name == name)
                        return itm;
                return null;
            }
        }

        public int IndexOf(CtlToolButton value)
        {
            return base.List.IndexOf(value);
        }


        public void CopyTo(CtlToolButton[] array, System.Int32 index)
        {
            CtlToolButton[] itms = new CtlToolButton[this.Count];
            int i = 0;
            foreach (CtlToolButton obj in base.List)
            {
                array.SetValue(obj, i);
                i++;
            }
        }

        public void CopyTo(object[] array, System.Int32 index)
        {
            object[] itms = new object[this.Count];
            int i = 0;
            foreach (CtlToolButton obj in base.List)
            {
                array.SetValue(obj.Text, i);
                i++;
            }
        }

        public void CopyTo(CtlListCombo.ObjectCollection array)
        {
            foreach (CtlToolButton obj in base.List)
            {
                if (obj.Text != null)
                {
                    array.Add(obj.Text);
                }
            }
        }

        #region AddProperty

        //public CtlToolButton AddItem(string Text)
        //{
        //    return AddItem(Text, null, -1, true);//,true);
        //}
        //public CtlToolButton AddItem(string Text, bool enabled)//,bool Visible)
        //{
        //    return AddItem(Text, null, -1, enabled);//,Visible);
        //}
        //public CtlToolButton AddItem(string Text, int imageIndex)
        //{
        //    return AddItem(Text, null, imageIndex, true);//,true);
        //}
        //public CtlToolButton AddItem(string Text, object tag, int imageIndex)
        //{
        //    return AddItem(Text, tag, imageIndex, true);//,true);
        //}

        //public CtlToolButton AddItem(string Text, int imageIndex, bool enabled)//,bool Visible)
        //{
        //    return AddItem(Text, null, imageIndex, enabled);//,Visible);
        //}

        //public CtlToolButton AddItem(string Text, object tag, int imageIndex, bool enabled)//,bool Visible)
        //{
        //    CtlToolButton item = new CtlToolButton(toolButton);
        //    int indx = this.Count;
        //    item.Text = Text;
        //    item.Tag = tag;
        //    if (toolButton != null && this.toolButton.ImageList != null)
        //    {
        //        item.ImageList = this.toolButton.ImageList;
        //        item.ImageIndex = imageIndex;
        //    }
        //    item.Enabled = enabled;
        //    //item.Visible  =Visible;
        //    //item.owner=this.owner;
        //    return Add(item);
        //}

        #endregion
    }
    
}
