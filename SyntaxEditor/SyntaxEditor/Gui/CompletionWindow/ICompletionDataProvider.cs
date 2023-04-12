
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Collections;

using Nistec.SyntaxEditor.Document;

namespace Nistec.SyntaxEditor.UI.CompletionWindow
{
	public interface ICompletionDataProvider
	{
		ImageList ImageList {
			get;
		}
		string PreSelection {
			get;
		}
		
		ICompletionData[] GenerateCompletionData(string fileName, TextArea textArea, char charTyped);
	}
}
