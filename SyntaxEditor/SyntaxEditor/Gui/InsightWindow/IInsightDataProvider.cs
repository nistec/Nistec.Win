
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Collections;

using Nistec.SyntaxEditor.Document;

namespace Nistec.SyntaxEditor.UI.InsightWindow
{
	public interface IInsightDataProvider
	{
		void SetupDataProvider(string fileName, TextArea textArea);
		
		bool CaretOffsetChanged();
		bool CharTyped();
		
		string GetInsightData(int number);
		
		int InsightDataCount {
			get;
		}
	}
}
