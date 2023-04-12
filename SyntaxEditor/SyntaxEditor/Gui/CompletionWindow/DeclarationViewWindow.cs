
using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Collections;
using System.Runtime.InteropServices;

using Nistec.SyntaxEditor.Document;
using Nistec.SyntaxEditor.Util;
using Nistec.SyntaxEditor;

namespace Nistec.SyntaxEditor.UI.CompletionWindow
{
	public interface IDeclarationViewWindow
	{
		string Description {
			get;
			set;
		}
		void ShowDeclarationViewWindow();
		void CloseDeclarationViewWindow();
	}
	
	public class DeclarationViewWindow : Form, IDeclarationViewWindow
	{
		string description = String.Empty;
		
		public string Description {
			get {
				return description;
			}
			set {
				description = value;
				Refresh();
			}
		}
		
		public DeclarationViewWindow(Form parent)
		{
			SetStyle(ControlStyles.Selectable, false);
			StartPosition   = FormStartPosition.Manual;
			FormBorderStyle = FormBorderStyle.None;
			Owner           = parent;
			ShowInTaskbar   = false;
			Size            = new Size(0, 0);
		}
		
		public void ShowDeclarationViewWindow()
		{
			AbstractCompletionWindow.ShowWindow(base.Handle, AbstractCompletionWindow.SW_SHOWNA);
		}
		
		public void CloseDeclarationViewWindow()
		{
			Close();
			Dispose();
		}
		
		protected override void OnPaint(PaintEventArgs pe)
		{
			TipPainterTools.DrawHelpTipFromCombinedDescription
				(this, pe.Graphics, Font, null, description);
		}
		
		protected override void OnPaintBackground(PaintEventArgs pe)
		{
			if (description != null && description.Length > 0) {
				pe.Graphics.FillRectangle(SystemBrushes.Info, pe.ClipRectangle);
			}
		}
	}
}
