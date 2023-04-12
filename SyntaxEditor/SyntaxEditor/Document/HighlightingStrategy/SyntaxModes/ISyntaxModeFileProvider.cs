
using System;
using System.Collections;
using System.Xml;

namespace Nistec.SyntaxEditor.Document
{
	public interface ISyntaxModeFileProvider
	{
		ArrayList SyntaxModes {
			get;
		}
		
		XmlTextReader GetSyntaxModeFile(SyntaxMode syntaxMode);
	}
}
