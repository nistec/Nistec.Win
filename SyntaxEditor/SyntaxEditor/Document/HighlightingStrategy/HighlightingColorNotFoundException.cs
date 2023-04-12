using System;

namespace Nistec.SyntaxEditor.Document
{
	public class HighlightingColorNotFoundException : Exception
	{
		public HighlightingColorNotFoundException(string name) : base(name)
		{
		}
	}
}
