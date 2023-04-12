
using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Nistec.ScriptBuilder
{
	

	/// <summary>
	/// ParsingParams is an class that holds parsing parameters. 
	/// At this moment they are namespace renaming rules only.
	/// </summary>
	class ParsingParams 
	{
		#region Public interface...

		public ParsingParams()
		{
			renameNamespaceMap = new ArrayList();
		}
		public string[][] RenameNamespaceMap
		{
			get {return (string[][])renameNamespaceMap.ToArray(typeof(string[])); }
		}
		public void AddRenameNamespaceMap(string[] names)
		{
			renameNamespaceMap.Add(names);
		}
		/// <summary>
		/// Compare() is to be used to help with implementation of IComparer for sorting operations.
		/// </summary>
		public static int Compare(ParsingParams xPrams, ParsingParams yPrams)
		{
			if (xPrams == null && yPrams == null)
				return 0;

			int retval =  xPrams == null ? -1 : (yPrams == null ? 1 : 0); 
			
			if (retval == 0)
			{
				string[][] xNames = xPrams.RenameNamespaceMap;
				string[][] yNames = yPrams.RenameNamespaceMap;
				retval = Comparer.Default.Compare(xNames.Length, yNames.Length);
				if (retval == 0)
				{
					for (int i = 0; i < xNames.Length && retval == 0; i++)
					{
						retval = Comparer.Default.Compare(xNames[i].Length, yNames[i].Length);
						if (retval == 0)
						{
							for (int j = 0; j < xNames[i].Length; j++)
							{
								retval = Comparer.Default.Compare(xNames[i][j], yNames[i][j]);
							}
						}
					}
				}
			}
			return retval;
		}

		#endregion
		private ArrayList renameNamespaceMap;
	}

}