using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;

namespace Nistec.SyntaxEditor
{

    public class CSWords
    {
        public static string[] GetCSWords()
        {
            string[] CSWords = new string[] {
        		"this" ,
				"base" ,
				"as" ,
				"is" ,
				"new" ,
				"sizeof" ,
				"typeof" ,
				"true" ,
				"false" ,
				"stackalloc" ,
				"else" ,
				"if" ,
				"switch" ,
				"case" ,
				"default" ,
				"do" ,
				"for" ,
				"foreach" ,
				"in" ,
				"while" ,
				"break" ,
				"continue" ,
				"goto" ,
				"return" ,
				"try" ,
				"throw" ,
				"catch" ,
				"finally" ,
				"checked" ,
				"unchecked" ,
				"fixed" ,
				"unsafe" ,
				"#if" ,
				"#else" ,
			    "#elif" ,
			    "#endif" ,
			    "#define" ,
			    "#undef" ,
			    "#warning" ,
			    "#error" ,
			    "#line" ,
			    "#region" ,
			    "#endregion" ,
			    "bool" ,
			    "byte" ,
			    "char" ,
			    "decimal" ,
			    "double" ,
			    "enum" ,
			    "float" ,
			    "int" ,
			    "long" ,
			    "sbyte" ,
			    "short" ,
			    "struct" ,
		    	"uint" ,
			    "ushort" ,
			    "ulong" ,
			    "class" ,
			    "interface" ,
			    "delegate" ,
			    "object" ,
			    "string" ,
			    "void" ,
			    "explicit" ,
			    "implicit" ,
			    "operator" ,
			    "params" ,
			    "ref" ,
			    "out" ,
			    "abstract" ,
			    "const" ,
			    "event" ,
			    "extern" ,
			    "override" ,
			    "readonly" ,
			    "sealed" ,
			    "static" ,
			    "virtual" ,
				"public" ,
				"protected" ,
				"private" ,
				"internal" ,
			    "namespace" ,
			    "using" ,
			    "lock" ,
			    "get" ,
			    "set" ,
			    "add" ,
			    "remove" ,
			    "null" ,
			    "value" ,
				"TODO" ,
				"FIXME" ,
				"HACK" ,
				"UNDONE" ,
				    "c" ,
				    "code" ,
				    "example" ,
				    "exception" ,
				    "list" ,
				    "para" ,
				    "param" ,
				    "paramref" ,
				    "permission" ,
				    "remarks" ,
				    "returns" ,
				    "see" ,
				    "seealso" ,
				    "summary" ,
				    "value" ,
				    "type" ,
				    "name" ,
				    "cref" ,
				    "item" ,
				    "term" ,
				    "description" ,
				    "listheader" };

            return CSWords;

        }

        public static List<string> GetCSCatalog(string nameSpace)
        {
            List<string> list = new List<string>();
            string[] _CSWords = GetCSWords();

            foreach (string s in _CSWords)
            {
                list.Add(s);
            }

            list.Sort();
            return list;
        }
    }

}
