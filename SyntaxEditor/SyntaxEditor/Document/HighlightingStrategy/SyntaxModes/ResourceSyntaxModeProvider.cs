
using System;
using System.Collections;
using System.Reflection;
using System.Xml;
using System.IO;

namespace Nistec.SyntaxEditor.Document
{
	public class ResourceSyntaxModeProvider : ISyntaxModeFileProvider
	{
		ArrayList syntaxModes = null;
		
		public ArrayList SyntaxModes {
			get {
				return syntaxModes;
			}
		}
		
		public ResourceSyntaxModeProvider()
		{
			Assembly assembly = typeof(SyntaxMode).Assembly;
            Stream syntaxModeStream = assembly.GetManifestResourceStream("Nistec.SyntaxEditor.data.SyntaxModes.xml");
			if (syntaxModeStream == null) throw new ApplicationException();
			syntaxModes = SyntaxMode.GetSyntaxModes(syntaxModeStream);
		}
		
		public XmlTextReader GetSyntaxModeFile(SyntaxMode syntaxMode)
		{
			Assembly assembly = typeof(SyntaxMode).Assembly;
            return new XmlTextReader(assembly.GetManifestResourceStream("Nistec.SyntaxEditor.data.syntaxmodes." + syntaxMode.FileName));
		}
	}
}
