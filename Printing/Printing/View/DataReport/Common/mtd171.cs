namespace Nistec.Printing.View
{
    using Microsoft.CSharp;
    using Microsoft.VisualBasic;
    using System;
    using System.CodeDom.Compiler;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;

    //mtd171
    internal class CodeProvider 
    {
        private CodeDomProvider _codeDomProvider=null;//_codeDomProvider = null;
        private Assembly _var1;
        private object _var2;
        private string _var3;
        private ScriptLanguage _var4;
        private CompilerErrorCollection _var5;
        //private ICodeCompiler _var6;
        private CompilerParameters _var7;
        private CompilerResults _var8;
        private MethodInfo _var9;
        internal bool mtd178;

        internal CodeProvider()
        {
        }

        internal bool mtd282()
        {
            if ((this._var3 != null) && (this._var3.Length > 4))
            {
                string[] strArray = new string[] { "_Start", "_End", "_DataInitialize", "_DataFetch", "_NoData", "_Initialize", "_OnPaint" };
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (Regex.IsMatch(this._var3, strArray[i]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        internal bool mtd284(bool var11)
        {
            if (this._var7 == null)
            {
                this._var7 = new CompilerParameters();
                this._var7.GenerateExecutable = false;
                this._var7.GenerateInMemory = true;
                this._var7.IncludeDebugInformation = false;
                this._var7.ReferencedAssemblies.Add("System.dll");
                this._var7.ReferencedAssemblies.Add("System.Xml.dll");
                this._var7.ReferencedAssemblies.Add("System.Data.dll");
                this._var7.ReferencedAssemblies.Add("System.Drawing.dll");
                this._var7.ReferencedAssemblies.Add("System.Windows.Forms.dll");
                this._var7.ReferencedAssemblies.Add("Nistec.Printing.View.dll");
                this._var7.ReferencedAssemblies.Add("Nistec.Printing.View.Design.dll");
                this._var7.MainClass = "ReportScript";
            }
            switch (this.ScriptLanguage)
            {
                case ScriptLanguage.VB:
                    if (this._codeDomProvider != null)
                    {
                        if (!(this._codeDomProvider is VBCodeProvider))
                        {
                            this._codeDomProvider = new VBCodeProvider();
                            //this._var6 = this._codeDomProvider.CreateCompiler();
                        }
                        break;
                    }
                    this._codeDomProvider = new VBCodeProvider();
                    //this._var6 = this._codeDomProvider.CreateCompiler();
                    break;

                case ScriptLanguage.CSharp:
                    if (this._codeDomProvider != null)
                    {
                        if (!(this._codeDomProvider is CSharpCodeProvider))
                        {
                            this._codeDomProvider = new CSharpCodeProvider();
                            //this._var6 = this._codeDomProvider.CreateCompiler();
                        }
                        break;
                    }
                    this._codeDomProvider = new CSharpCodeProvider();
                    //this._var6 = this._codeDomProvider.CreateCompiler();
                    break;
            }
            //this._var8 = this._var6.CompileAssemblyFromSource(this._var7, this.var10());
            this._var8 = this._codeDomProvider.CompileAssemblyFromSource(this._var7, this.var10());
            this._var5 = this._var8.Errors;
            if (this._var8.Errors.Count != 0)
            {
                return false;
            }
            if (var11)
            {
                this._var1 = this._var8.CompiledAssembly;
                this._var2 = this._var1.CreateInstance("Nistec.Printing.View.ReportScript");
                if (this._var2 != null)
                {
                    this.mtd178 = true;
                }
            }
            return true;
        }

        internal void Clear()//mtd364
        {
            if (this._codeDomProvider != null)
            {
                this._codeDomProvider.Dispose();
                this._codeDomProvider = null;
            }
            this._var2 = null;
            this._var3 = null;
            if (this._var5 != null)
            {
                this._var5.Clear();
                this._var5 = null;
            }
            //this._var6 = null;
            this._var7 = null;
            this._var8 = null;
            this._var9 = null;
        }

        internal bool mtd71(string var12, Methods var13, object[] var14)
        {
            this._var9 = this._var2.GetType().GetMethod(string.Format("{0}{1}", var12, var13));
            if (this._var9 == null)
            {
                return false;
            }
            this._var9.Invoke(this._var2, var14);
            return true;
        }

        private string var10()
        {
            StringBuilder builder = new StringBuilder();
            if (this._var4 == ScriptLanguage.VB)
            {
                builder.Append("Imports System" + '\r');
                builder.Append("Imports System.Xml" + '\r');
                builder.Append("Imports System.Data" + '\r');
                builder.Append("Imports Nistec.Printing.View.Design" + '\r');
                builder.Append("Namespace Nistec.Printing.View" + '\r');
                builder.Append("Public Class ReportScript" + '\r');
                builder.Append(this._var3 + '\r');
                builder.Append("End Class" + '\r');
                builder.Append("End Namespace" + '\r');
            }
            else if (this._var4 == ScriptLanguage.CSharp)
            {
                builder.Append("using System;" + '\r');
                builder.Append("using System.Xml;" + '\r');
                builder.Append("using System.Data;" + '\r');
                builder.Append("using Nistec.Printing.View.Design;" + '\r');
                builder.Append("namespace Nistec.Printing.View" + '\r');
                builder.Append("{" + '\r');
                builder.Append("public class ReportScript" + '\r');
                builder.Append("{");
                builder.Append(this._var3 + '\r');
                builder.Append("}" + '\r');
                builder.Append("}" + '\r');
            }
            return builder.ToString();
        }

        internal string Text//mtd281
        {
            get
            {
                return this._var3;
            }
            set
            {
                this._var3 = value;
            }
        }

        internal ScriptLanguage ScriptLanguage//mtd283
        {
            get
            {
                return this._var4;
            }
            set
            {
                this._var4 = value;
            }
        }

        internal CompilerErrorCollection ErrorCollection//mtd363
        {
            get
            {
                return this._var5;
            }
            set
            {
                this._var5 = value;
            }
        }
    }
}

