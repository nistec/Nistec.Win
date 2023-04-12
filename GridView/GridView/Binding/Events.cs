using System;
using System.Collections;
using System.Text;

namespace mControl.WinCtl.Controls
{
    public delegate void BindItemChangedEventHandler(object sender, BindItemChangedEventArgs e);


    public class BindItemChangedEventArgs : EventArgs
    {
        // Methods
        internal BindItemChangedEventArgs(int index)
        {
            this.index = index;
        }

 

        // Properties
        public int Index
        {
            get
            {
                return this.index;
            }
        }
 

        // Fields
        private int index;
    }
 

}
