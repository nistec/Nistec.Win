using System;
using System.Drawing;
using System.Collections;

namespace Nistec.SyntaxEditor.Document
{
	/*
	/// <summary>
	/// A simple folding strategy which calculates the folding level
	/// using the indent level of the line.
	/// </summary>
	public class IndentFoldingStrategy : IFoldingStrategy
	{
		/// <remarks>
		/// Calculates the fold level of a specific line.
		/// </remarks>
		public int CalculateFoldLevel(IDocument document, int lineNumber)
		{
			LineSegment line = document.GetLineSegment(lineNumber);
			int foldLevel = 0;
			
			while (document.GetCharAt(line.Offset + foldLevel) == '\t' && foldLevel + 1  < line.TotalLength) {
				++foldLevel;
			}
			
			return foldLevel;
		}
	}*/
}
