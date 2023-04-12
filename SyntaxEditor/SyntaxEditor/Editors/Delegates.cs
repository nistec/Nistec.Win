using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Messaging;
using Nistec.Collections;

namespace Nistec.SyntaxEditor
{

    public delegate void IntelliListEventHandler(object sender, IntelliListEventArgs e);

    public class IntelliListEventArgs : EventArgs
    {
        private ListItems items;
        private string word;

        public IntelliListEventArgs(string word)
        {
            this.word = word;
            this.items = new ListItems();
        }

        public string Word
        {
            get { return word; }
        }

        public ListItems Items
        {
            get { return items; }
            set 
            {
                if (value != null)
                {
                    items = value;
                }
            }
        }
    }


}


