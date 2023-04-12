namespace MControl.Charts
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class ColorCollection : CollectionBase
    {
        internal event EventHandler ColorChanged;

        public void Add(Color item)
        {
            base.List.Add(item);
        }

        public new void Clear()
        {
            base.List.Clear();
        }

        public void Insert(int index, Color item)
        {
            base.List.Insert(index, item);
        }

        protected override void OnClearComplete()
        {
            if (this.ColorChanged != null)
            {
                this.ColorChanged(this, new EventArgs());
            }
        }

        protected override void OnInsertComplete(int index, object value)
        {
            if (this.ColorChanged != null)
            {
                this.ColorChanged(this, new EventArgs());
            }
        }

        protected override void OnRemoveComplete(int index, object value)
        {
            if (this.ColorChanged != null)
            {
                this.ColorChanged(this, new EventArgs());
            }
        }

        protected override void OnSetComplete(int index, object oldValue, object newValue)
        {
            if (this.ColorChanged != null)
            {
                this.ColorChanged(this, new EventArgs());
            }
        }

        public void Remove(Color item)
        {
            base.List.Remove(item);
        }

        public new void RemoveAt(int index)
        {
            base.List.RemoveAt(index);
        }

        public Color this[int index]
        {
            get
            {
                return (Color) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

