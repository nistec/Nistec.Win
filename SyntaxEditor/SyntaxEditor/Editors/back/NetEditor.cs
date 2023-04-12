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
using System.Reflection;

namespace MControl.SyntaxEditor
{
    [ToolboxBitmap("MControl.SyntaxEditor.SyntaxEditor.bmp")]
    [ToolboxItem(true), Description("MControl .Net Syntax Editor")]
    //[Designer(typeof(Design.SyntaxEditorDesigner))]
    public class NetEditor : TextEditor
    {

         
        #region ctor

        public NetEditor()
            : this(true,IntelliMode.Auto)
        {
        }

        public NetEditor(bool useCSharp, IntelliMode inteliMode)
            : base(useCSharp? SyntaxMode.CSharp:SyntaxMode.VBNET,inteliMode)
        {

        }

        protected override void ApplyProperty(string word)
        {
            base.ApplyProperty(word);
            Type type = Type.GetType(word);
            
            base.ShowIntelliWindow(GetMemebers(type));
        }

        protected ListItems GetMemebers(Type type)
        {
            ListItems items = new ListItems();

            foreach (System.Reflection.FieldInfo fi in type.GetFields())
            {
                items.Add(fi.Name, (int)MemberType.Field);
            }
            foreach (System.Reflection.PropertyInfo pi in type.GetProperties())
            {
                items.Add(pi.Name, (int)MemberType.Property);
            }
            foreach (System.Reflection.MethodInfo mi in type.GetMethods())
            {
                items.Add(mi.Name, (int)MemberType.Method);
            }
            return items;
        }


        #endregion

    
    }
}
