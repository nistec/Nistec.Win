
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Collections;
using Nistec.SyntaxEditor;

namespace Nistec.SyntaxEditor.UI.CompletionWindow
{
	public interface ICompletionData : IComparable
	{
		int ImageIndex {
			get;
		}
		
		string[] Text {
			get;
		}
		
		string Description {
			get;
		}
		
		void InsertAction(TextEditor/*TextEditorControl*/ control);
	}
}
