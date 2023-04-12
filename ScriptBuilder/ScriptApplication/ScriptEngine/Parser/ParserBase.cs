
using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Diagnostics;
using System.Text.RegularExpressions;

using PCM.ScriptEngine; 

namespace PCM.Engine.Utility
{
	
	/// <summary>
	/// ParserBase is a base class that implements some ReularExtressions functionality.
	/// </summary>
	class ParserBase
	{

		#region Members

		public string fileName = "";
		private int headerLinesTotal = 0;

		public delegate object ParseStatement(string statement);
		private const RegexOptions regexOptions =	RegexOptions.Multiline | 
			RegexOptions.IgnorePatternWhitespace| 
			RegexOptions.Compiled;


		static public string headerTemplate =	
			@"/*" + Environment.NewLine +				
			@" Created by {0}" + 
			@" Original location: {1}" + Environment.NewLine +
			@" C# source equivalent of {2}" + Environment.NewLine +
			@" compiler-generated file created {3} - DO NOT EDIT!" + Environment.NewLine +
			@"*/" + Environment.NewLine;

		#endregion

		#region Methods
		/// <summary>
		/// Returns array of matches; Optionally every match can be processed with delegate ParseStatement.
		/// </summary>
		static public ArrayList RegexGetMatches(string text, string patern, ParseStatement parser)
		{
			ArrayList retval = new ArrayList();
			Regex regex = new Regex(patern, regexOptions);
			Match m = regex.Match(text);
			string retvalStr = "";
			while (m.Success) 
			{
				retvalStr = m.ToString();
				if (parser != null)
					retval.Add(parser(retvalStr));
				else
					retval.Add(retvalStr);
				m = m.NextMatch();
			}
			return retval;
		}
		
		/// <summary> 
		/// Returns array of matches for a statement (word(s) ended with ';'); Optionally every match can be processed with delegate ParseStatement. 
		/// </summary> 
		static public ArrayList GetStatements(string text, string patern, ParseStatement parser) 
		{
			ArrayList retval = new ArrayList(); 
			Regex regex = new Regex(patern, regexOptions); 
			Match m = regex.Match(text);

			string retvalStr = ""; 

			while (m.Success) 
			{
				int startPos = m.Index; 
				int endPos = text.IndexOf(";", m.Index); 

				if (endPos != -1) 
				{
					retvalStr = text.Substring(startPos, endPos - startPos);
					if (parser != null) 
						retval.Add(parser(retvalStr));
					else 
						retval.Add(retvalStr);
				}
				m = m.NextMatch();
			}
			return retval; 
		}

		/// <summary>
		/// Replaces all search matches; Optionally replacing can be done repeatedly until no replacement can be done.
		/// </summary>
		static public string RegexReplace(string text, string patern, string replacement, bool recurcive)
		{
			string retval = text;
			Regex regex = new Regex(patern, regexOptions);
			int oldLength = 0;
			do
			{
				oldLength = retval.Length;
				retval = regex.Replace(retval, replacement);
			}
			while(recurcive && oldLength != retval.Length);
			return retval;
		}

		/// <summary>
		/// Retruns position of the first match.
		/// </summary>
		static public int RegexFind(string text, string patern)
		{
			Regex regex = new Regex(patern, regexOptions);
			Match m = regex.Match(text); 
			if (m.Success) 
				return m.Index;
			else
				return -1;
		}

		/// <summary>
		/// Retruns position of the first match and populates out paramater with the matchins result. 
		/// </summary>
		static public int RegexFind(string text, string patern, ref string match)
		{
			Regex regex = new Regex(patern, regexOptions);
			Match m = regex.Match(text); 
			if (m.Success) 
			{
				match = m.ToString();
				return m.Index;
			}
			else
				return -1;
		}


		public string ComposeHeader(string path)
		{
			return string.Format(headerTemplate, AppInfo.appTitle , path, fileName, DateTime.Now);
		}

		public int HeaderLinesTotal 
		{
			get 
			{
				if (headerLinesTotal == 0)
				{
					StringReader strReader = new StringReader(headerTemplate);
					while (strReader.ReadLine() != null) 
					{
						headerLinesTotal++;
					}
				}
				return headerLinesTotal;
			}
		}
		#endregion
	}
	
}