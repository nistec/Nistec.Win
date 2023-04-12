
using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace PCM.Engine.Utility
{
	
	/// <summary>
	/// Class which is a placeholder for general infoirmation of the script file
	/// </summary>
	class ScriptInfo : ParserBase 
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="statement">'Import' statement from the script file to be parsed</param>
		public ScriptInfo(string statement)
		{
			RegexFind(statement, @"\w*,?", ref fileName);
			fileName = fileName.Trim(" ,".ToCharArray());
			
			string rename = "";
			foreach (string match in RegexGetMatches(statement, @"rename_namespace\s*\(\s* \w+ \s* , \s* \w+ \s*\)", null))
			{
				if (match.Length > 0)
				{
					rename = match.Replace("rename_namespace","").Trim("()".ToCharArray());
					if (parseParams == null)
						parseParams = new ParsingParams();

					parseParams.AddRenameNamespaceMap(rename.Split(",".ToCharArray()));
				}
			}
		}
		public ParsingParams parseParams;	
	}

}