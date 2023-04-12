using System;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Diagnostics;
using System.Threading;
using System.Runtime.Remoting;
using System.Runtime.InteropServices;

using MControl.WinForms;
using MControl.SyntaxEditor;
using MControl.SyntaxEditor.Document;
using MControl.SyntaxEditor.Actions;
using MControl.SyntaxEditor.UI;

namespace MControl.SyntaxEditor
{
    [ToolboxBitmap("MControl.SyntaxEditor.SyntaxEditor.bmp")]
    [ToolboxItem(true), Description("MControl Sql Syntax Editor")]
    //[Designer(typeof(Design.SyntaxEditorDesigner))]
    public class SqlEditor : TextEditor
    {

        List<string> AliasList; 
        #region ctor

        public SqlEditor()
            : this(IntelliMode.Auto)
        {
        }

        public SqlEditor(IntelliMode inteliMode):
            base( SyntaxMode.SQL,inteliMode)
        {
            AliasList = new List<string>();
        }

        private void SetAlias()
        {
            int i = this.Text.ToLower().LastIndexOf("from ", this.SelectionStart);
            if (i > -1)
            {

            }

        }

  
        #endregion

    
    }
}
