
using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.Remoting;
using System.Runtime.InteropServices;
using System.Xml;
using System.Text;

using Nistec.SyntaxEditor.UI.CompletionWindow;
using Nistec.SyntaxEditor.Document;
using Nistec.SyntaxEditor.Actions;

namespace Nistec.SyntaxEditor.UI
{

 

	/// <summary>
	/// This class is used for a basic text area control
	/// </summary>
	[ToolboxItem(false)]
	public abstract class SyntaxEditorBase /*TextEditorControlBase*/ : UserControl
	{
		string    currentFileName = null;
		int       updateLevel     = 0;
		IDocument document;
		
		/// <summary>
		/// This hashtable contains all editor keys, where
		/// the key is the key combination and the value the
		/// action.
		/// </summary>
		protected Hashtable editactions = new Hashtable();

        public ISyntaxProperties SyntaxProperties
        {
			get {
				return document.SyntaxProperties;
			}
			set {
				document.SyntaxProperties = value;
				OptionsChanged();
			}
		}
		
		/// <value>
		/// Current file's character encoding
		/// </value>
		public Encoding Encoding {
			get {
				return SyntaxProperties.Encoding;
			}
			set {
//				if (encoding != null && value != null && !encoding.Equals(value) && !CharacterEncoding.IsUnicode(value)) {
//					Byte[] bytes = encoding.GetBytes(Text);
//					Text = new String(value.GetChars(bytes));
//				}
				SyntaxProperties.Encoding = value;
			}
		}
		
		/// <value>
		/// The current file name
		/// </value>
		[Browsable(false)]
		[ReadOnly(true)]
		public string FileName {
			get {
				return currentFileName;
			}
			set {
				if (currentFileName != value) {
					currentFileName = value;
					OnFileNameChanged(EventArgs.Empty);
				}
			}
		}
		
		/// <value>
		/// true, if the textarea is updating it's status, while
		/// it updates it status no redraw operation occurs.
		/// </value>
		[Browsable(false)]
		public bool IsUpdating {
			get {
				return updateLevel > 0;
			}
		}
		
		/// <value>
		/// The current document
		/// </value>
		[Browsable(false),EditorBrowsable(EditorBrowsableState.Advanced),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IDocument Document {
			get {
				return document;
			}
			set {
				document = value;
				document.UndoStack.TextEditorControl = this;
			}
		}
		
		[Browsable(true)]
		public override string Text {
			get {
				return Document.TextContent;
			}
			set {
				Document.TextContent = value;
			}
		}
		
		static Font ParseFont(string font)
		{
			string[] descr = font.Split(new char[]{',', '='});
			return new Font(descr[1], Single.Parse(descr[3]));
		}
		
		/// <value>
		/// If set to true the contents can't be altered.
		/// </value>
		[Browsable(false)]
		[ReadOnly(true)]
		public bool ReadOnly {
			get {
				return Document.ReadOnly;
			}
			set {
				Document.ReadOnly = value;
			}
		}
		
		[Browsable(false)]
		public bool IsInUpdate {
			get {
				return this.updateLevel > 0; 
			}
		}
		
		/// <value>
		/// supposedly this is the way to do it according to .NET docs,
		/// as opposed to setting the size in the constructor
		/// </value>
		protected override Size DefaultSize {
			get {
				return new Size(100, 100);
			}
		}
		
#region Document Properties
		/// <value>
		/// If true spaces are shown in the textarea
		/// </value>
		[Category("Appearance")]
		[DefaultValue(false)]
		[Description("If true spaces are shown in the textarea")]
		public bool ShowSpaces {
			get {
				return document.SyntaxProperties.ShowSpaces;
			}
			set {
				document.SyntaxProperties.ShowSpaces = value;
				OptionsChanged();
			}
		}
		
		/// <value>
		/// If true antialiased fonts are used inside the textarea
		/// </value>
		[Category("Appearance")]
		[DefaultValue(false)]
		[Description("If true antialiased fonts are used inside the textarea")]
		public bool UseAntiAliasFont {
			get { 
				return document.SyntaxProperties.UseAntiAliasedFont;
			}
			set { 
				document.SyntaxProperties.UseAntiAliasedFont = value;
				OptionsChanged();
			}
		}
		
		/// <value>
		/// If true tabs are shown in the textarea
		/// </value>
		[Category("Appearance")]
		[DefaultValue(false)]
		[Description("If true tabs are shown in the textarea")]
		public bool ShowTabs {
			get { 
				return document.SyntaxProperties.ShowTabs;
			}
			set {
				document.SyntaxProperties.ShowTabs = value;
				OptionsChanged();
			}
		}
		
		/// <value>
		/// If true EOL markers are shown in the textarea
		/// </value>
		[Category("Appearance")]
		[DefaultValue(false)]
		[Description("If true EOL markers are shown in the textarea")]
		public bool ShowEOLMarkers {
			get {
				return document.SyntaxProperties.ShowEOLMarker;
			}
			set {
				document.SyntaxProperties.ShowEOLMarker = value;
				OptionsChanged();
			}
		}
		
		/// <value>
		/// If true the horizontal ruler is shown in the textarea
		/// </value>
		[Category("Appearance")]
		[DefaultValue(false)]
		[Description("If true the horizontal ruler is shown in the textarea")]
		public bool ShowHRuler {
			get { 
				return document.SyntaxProperties.ShowHorizontalRuler;
			}
			set { 
				document.SyntaxProperties.ShowHorizontalRuler = value;
				OptionsChanged();
			}
		}
		
		/// <value>
		/// If true the vertical ruler is shown in the textarea
		/// </value>
		[Category("Appearance")]
		[DefaultValue(false)]
		[Description("If true the vertical ruler is shown in the textarea")]
		public bool ShowVRuler {
			get {
				return document.SyntaxProperties.ShowVerticalRuler;
			}
			set {
				document.SyntaxProperties.ShowVerticalRuler = value;
				OptionsChanged();
			}
		}
		
		/// <value>
		/// The row in which the vertical ruler is displayed
		/// </value>
		[Category("Appearance")]
		[DefaultValue(80)]
		[Description("The row in which the vertical ruler is displayed")]
		public int VRulerRow {
			get {
				return document.SyntaxProperties.VerticalRulerRow;
			}
			set {
				document.SyntaxProperties.VerticalRulerRow = value;
				OptionsChanged();
			}
		}
		
		/// <value>
		/// If true line numbers are shown in the textarea
		/// </value>
		[Category("Appearance")]
		[DefaultValue(true)]
		[Description("If true line numbers are shown in the textarea")]
		public bool ShowLineNumbers {
			get {
				return document.SyntaxProperties.ShowLineNumbers;
			}
			set {
				document.SyntaxProperties.ShowLineNumbers = value;
				OptionsChanged();
			}
		}
		
		/// <value>
		/// If true invalid lines are marked in the textarea
		/// </value>
		[Category("Appearance")]
		[DefaultValue(false)]
		[Description("If true invalid lines are marked in the textarea")]
		public bool ShowInvalidLines {
			get { 
				return document.SyntaxProperties.ShowInvalidLines;
			}
			set { 
				document.SyntaxProperties.ShowInvalidLines = value;
				OptionsChanged();
			}
		}
		
		/// <value>
		/// If true folding is enabled in the textarea
		/// </value>
		[Category("Appearance")]
		[DefaultValue(true)]
		[Description("If true folding is enabled in the textarea")]
		public bool EnableFolding {
			get { 
				return document.SyntaxProperties.EnableFolding;
			}
			set {
				document.SyntaxProperties.EnableFolding = value;
				OptionsChanged();
			}
		}
		
		[Category("Appearance")]
		[DefaultValue(true)]
		[Description("If true matching brackets are highlighted")]
		public bool ShowMatchingBracket {
			get { 
				return document.SyntaxProperties.ShowMatchingBracket;
			}
			set {
				document.SyntaxProperties.ShowMatchingBracket = value;
				OptionsChanged();
			}
		}
		
		[Category("Appearance")]
		[DefaultValue(true)]
		[Description("If true the icon bar is displayed")]
		public bool IconBarVisible {
			get { 
				return document.SyntaxProperties.IsIconBarVisible;
			}
			set {
				document.SyntaxProperties.IsIconBarVisible = value;
				OptionsChanged();
			}
		}
		
		/// <value>
		/// The width in spaces of a tab character
		/// </value>
		[Category("Appearance")]
		[DefaultValue(4)]
		[Description("The width in spaces of a tab character")]
		public int TabIndent {
			get { 
				return document.SyntaxProperties.TabIndent;
			}
			set { 
				document.SyntaxProperties.TabIndent = value;
				OptionsChanged();
			}
		}
		
		/// <value>
		/// The line viewer style
		/// </value>
		[Category("Appearance")]
		[DefaultValue(LineViewerStyle.None)]
		[Description("The line viewer style")]
		public LineViewerStyle LineViewerStyle {
			get { 
				return document.SyntaxProperties.LineViewerStyle;
			}
			set { 
				document.SyntaxProperties.LineViewerStyle = value;
				OptionsChanged();
			}
		}

		/// <value>
		/// The indent style
		/// </value>
		[Category("Behavior")]
		[DefaultValue(IndentStyle.Smart)]
		[Description("The indent style")]
		public IndentStyle IndentStyle {
			get { 
				return document.SyntaxProperties.IndentStyle;
			}
			set { 
				document.SyntaxProperties.IndentStyle = value;
				OptionsChanged();
			}
		}
		
		/// <value>
		/// if true spaces are converted to tabs
		/// </value>
		[Category("Behavior")]
		[DefaultValue(false)]
		[Description("Converts tabs to spaces while typing")]
		public bool ConvertTabsToSpaces {
			get { 
				return document.SyntaxProperties.ConvertTabsToSpaces;
			}
			set { 
				document.SyntaxProperties.ConvertTabsToSpaces = value;
				OptionsChanged();
			}
		}
		
		/// <value>
		/// if true spaces are converted to tabs
		/// </value>
		[Category("Behavior")]
		[DefaultValue(false)]
		[Description("Creates a backup copy for overwritten files")]
		public bool CreateBackupCopy {
			get { 
				return document.SyntaxProperties.CreateBackupCopy;
			}
			set { 
				document.SyntaxProperties.CreateBackupCopy = value;
				OptionsChanged();
			}
		}
		
		/// <value>
		/// if true spaces are converted to tabs
		/// </value>
		[Category("Behavior")]
		[DefaultValue(false)]
		[Description("Hide the mouse cursor while typing")]
		public bool HideMouseCursor {
			get { 
				return document.SyntaxProperties.HideMouseCursor;
			}
			set { 
				document.SyntaxProperties.HideMouseCursor = value;
				OptionsChanged();
			}
		}
		
		/// <value>
		/// if true spaces are converted to tabs
		/// </value>
		[Category("Behavior")]
		[DefaultValue(false)]
		[Description("Allows the caret to be places beyonde the end of line")]
		public bool AllowCaretBeyondEOL {
			get { 
				return document.SyntaxProperties.AllowCaretBeyondEOL;
			}
			set { 
				document.SyntaxProperties.AllowCaretBeyondEOL = value;
				OptionsChanged();
			}
		}
		/// <value>
		/// if true spaces are converted to tabs
		/// </value>
		[Category("Behavior")]
		[DefaultValue(BracketMatchingStyle.After)]
		[Description("Specifies if the bracket matching should match the bracket before or after the caret.")]
		public BracketMatchingStyle BracketMatchingStyle {
			get {
				return document.SyntaxProperties.BracketMatchingStyle;
			}
			set {
				document.SyntaxProperties.BracketMatchingStyle = value;
				OptionsChanged();
			}
		}
		
		/// <value>
		/// The base font of the text area. No bold or italic fonts
		/// can be used because bold/italic is reserved for highlighting
		/// purposes.
		/// </value>
		[Browsable(true)]
		[Description("The base font of the text area. No bold or italic fonts can be used because bold/italic is reserved for highlighting purposes.")]
		public override Font Font {
			get {
				return document.SyntaxProperties.Font;
			}
			set {
				document.SyntaxProperties.Font = value;
				OptionsChanged();
			}
		}
			
#endregion

		public abstract TextAreaControl ActiveTextAreaControl {
			get;
		}
		
		protected SyntaxEditorBase /*TextEditorControlBase*/()
		{
			GenerateDefaultActions();
			HighlightingManager.Manager.ReloadSyntaxHighlighting += new EventHandler(ReloadHighlighting);
		}
		
		
		void ReloadHighlighting(object sender, EventArgs e)
		{
			if (Document.HighlightingStrategy != null) {
				Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy(Document.HighlightingStrategy.Name);
				OptionsChanged();
            }
		}

  	
				
		internal IEditAction GetEditAction(Keys keyData)
		{
			return (IEditAction)editactions[keyData];
		}

		void GenerateDefaultActions()
		{
			editactions[Keys.Left] = new CaretLeft();
			editactions[Keys.Left | Keys.Shift] = new ShiftCaretLeft();
			editactions[Keys.Left | Keys.Control] = new WordLeft();
			editactions[Keys.Left | Keys.Control | Keys.Shift] = new ShiftWordLeft();
			editactions[Keys.Right] = new CaretRight();
			editactions[Keys.Right | Keys.Shift] = new ShiftCaretRight();
			editactions[Keys.Right | Keys.Control] = new WordRight();
			editactions[Keys.Right | Keys.Control | Keys.Shift] = new ShiftWordRight();
			editactions[Keys.Up] = new CaretUp();
			editactions[Keys.Up | Keys.Shift] = new ShiftCaretUp();
			editactions[Keys.Up | Keys.Control] = new ScrollLineUp();
			editactions[Keys.Down] = new CaretDown();
			editactions[Keys.Down | Keys.Shift] = new ShiftCaretDown();
			editactions[Keys.Down | Keys.Control] = new ScrollLineDown();
			
			editactions[Keys.Insert] = new ToggleEditMode();
			editactions[Keys.Insert | Keys.Control] = new Copy();
			editactions[Keys.Insert | Keys.Shift] = new Paste();
			editactions[Keys.Delete] = new Delete();
			editactions[Keys.Delete | Keys.Shift] = new Cut();
			editactions[Keys.Home] = new Home();
			editactions[Keys.Home | Keys.Shift] = new ShiftHome();
			editactions[Keys.Home | Keys.Control] = new MoveToStart();
			editactions[Keys.Home | Keys.Control | Keys.Shift] = new ShiftMoveToStart();
			editactions[Keys.End] = new End();
			editactions[Keys.End | Keys.Shift] = new ShiftEnd();
			editactions[Keys.End | Keys.Control] = new MoveToEnd();
			editactions[Keys.End | Keys.Control | Keys.Shift] = new ShiftMoveToEnd();
			editactions[Keys.PageUp] = new MovePageUp();
			editactions[Keys.PageUp | Keys.Shift] = new ShiftMovePageUp();
			editactions[Keys.PageDown] = new MovePageDown();
			editactions[Keys.PageDown | Keys.Shift] = new ShiftMovePageDown();
			
			editactions[Keys.Return] = new Return();
			editactions[Keys.Tab] = new Tab();
			editactions[Keys.Tab | Keys.Shift] = new ShiftTab();
			editactions[Keys.Back] = new Backspace();
			editactions[Keys.Back | Keys.Shift] = new Backspace();
			
			editactions[Keys.X | Keys.Control] = new Cut();
			editactions[Keys.C | Keys.Control] = new Copy();
			editactions[Keys.V | Keys.Control] = new Paste();
			
			editactions[Keys.A | Keys.Control] = new SelectWholeDocument();
			editactions[Keys.Escape] = new ClearAllSelections();
			
			editactions[Keys.Divide | Keys.Control] = new ToggleComment();
			editactions[Keys.OemQuestion | Keys.Control] = new ToggleComment();
			
			editactions[Keys.Back | Keys.Alt]  = new Actions.Undo();
			editactions[Keys.Z | Keys.Control] = new Actions.Undo();
			editactions[Keys.Y | Keys.Control] = new Redo();
			
			editactions[Keys.Delete | Keys.Control] = new DeleteWord();
			editactions[Keys.Back | Keys.Control]   = new WordBackspace();
			editactions[Keys.D | Keys.Control]      = new DeleteLine();
			editactions[Keys.D | Keys.Shift | Keys.Control]      = new DeleteToLineEnd();
			
			editactions[Keys.B | Keys.Control]      = new GotoMatchingBrace();
		}
		
		/// <remarks>
		/// Call this method before a long update operation this
		/// 'locks' the text area so that no screen update occurs.
		/// </remarks>
		public virtual void BeginUpdate()
		{
			++updateLevel;
		}
		
		/// <remarks>
		/// Call this method to 'unlock' the text area. After this call
		/// screen update can occur. But no automatical refresh occurs you
		/// have to commit the updates in the queue.
		/// </remarks>
		public virtual void EndUpdate()
		{
			Debug.Assert(updateLevel > 0);
			updateLevel = Math.Max(0, updateLevel - 1);
		}
		
		public void LoadFile(string fileName)
		{
			LoadFile(fileName, true);
		}
		/// <remarks>
		/// Loads a file given by fileName
		/// </remarks>
		public void LoadFile(string fileName, bool autoLoadHighlighting)
		{
			BeginUpdate();
			document.TextContent = String.Empty;
			document.UndoStack.ClearAll();
			document.BookmarkManager.Clear();
			if (autoLoadHighlighting) {
				document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategyForFile(fileName);
			}
			
			StreamReader stream;
			if (Encoding != null) {
				stream = new StreamReader(fileName, Encoding);
			} else {
				stream = new StreamReader(fileName);
			}
			Document.TextContent = stream.ReadToEnd();
			stream.Close();
			
			this.FileName = fileName;
			OptionsChanged();
			Document.UpdateQueue.Clear();
			EndUpdate();
			
			Refresh();
		}
		
		/// <remarks>
		/// Saves a file given by fileName
		/// </remarks>
		public void SaveFile(string fileName)
		{
			if (document.SyntaxProperties.CreateBackupCopy) {
				MakeBackupCopy(fileName);
			}
			
			StreamWriter stream;
			if (Encoding != null && Encoding.CodePage != 65001) {
				stream = new StreamWriter(fileName, false, Encoding);
			} else {
				stream = new StreamWriter(fileName, false);
			}
			
			foreach (LineSegment line in Document.LineSegmentCollection) {
				stream.Write(Document.GetText(line.Offset, line.Length));
				stream.Write(document.SyntaxProperties.LineTerminator);
			}
			
			stream.Close();
			
			this.FileName = fileName;
		}
		
		void MakeBackupCopy(string fileName) 
		{
			try {
				if (File.Exists(fileName)) {
					string backupName = fileName + ".bak";
					if (File.Exists(backupName)) {
						File.Delete(backupName);
					}
					File.Copy(fileName, backupName);
				}
			} catch (Exception) {
//				IMessageService messageService =(IMessageService)ServiceManager.Services.GetService(typeof(IMessageService));
//				messageService.ShowError(e, "Can not create backup copy of " + fileName);
			}
		}
		
		public abstract void OptionsChanged();
		
		// Localization ISSUES
		
		// used in insight window
		public virtual string GetRangeDescription(int selectedItem, int itemCount)
		{
			StringBuilder sb=new StringBuilder(selectedItem.ToString());
			sb.Append(" from ");
			sb.Append(itemCount.ToString());
			return sb.ToString();
		}
		
		/// <remarks>
		/// Overwritten refresh method that locks if the control is in
		/// an update cycle.
		/// </remarks>
		public override void Refresh()
		{
			if (IsUpdating) {
				return;
			}
			base.Refresh();
		}
		
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				HighlightingManager.Manager.ReloadSyntaxHighlighting -= new EventHandler(ReloadHighlighting);
			}
			base.Dispose(disposing);
		}
		
//		public virtual IDeclarationViewWindow CreateDeclarationViewWindow(Form parent)
//		{
//			return new DeclarationViewWindow(parent);
//		}
		
		protected virtual void OnFileNameChanged(EventArgs e)
		{
			if (FileNameChanged != null) {
				FileNameChanged(this, e);
			}
		}
		
		protected virtual void OnChanged(EventArgs e)
		{
			if (Changed != null) {
				Changed(this, e);
			}
		}
		
		public event EventHandler FileNameChanged;
		public event EventHandler Changed;
	}
}
