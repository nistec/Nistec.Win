using System;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Diagnostics;
using System.Threading;
using System.Runtime.Remoting;
using System.Runtime.InteropServices;
using Nistec.WinForms;
using Nistec.Collections;
using Nistec.SyntaxEditor;
using Nistec.SyntaxEditor.Document;
using Nistec.SyntaxEditor.Actions;
using Nistec.SyntaxEditor.UI;
using Nistec.Win;

namespace Nistec.SyntaxEditor
{
    [ToolboxBitmap("Nistec.SyntaxEditor.SyntaxEditor.bmp")]
    [ToolboxItem(true), Description("Nistec Syntax Editor")]
    [Designer(typeof(Design.SyntaxEditorDesigner))]
    public class TextEditor : SyntaxEditorBase /*TextEditorControlBase*/
    {

        #region EditorControl

            protected Panel textAreaPanel = new Panel();
            TextAreaControl primaryTextArea;
            Splitter textAreaSplitter = null;
            TextAreaControl secondaryTextArea = null;

            PrintDocument printDocument = null;

            private void InitEditorControl()
            {
                SetStyle(ControlStyles.ContainerControl, true);
                SetStyle(ControlStyles.Selectable, true);

                textAreaPanel.Dock = DockStyle.Fill;

                Document = (new DocumentFactory()).CreateDocument();
                Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy();

                primaryTextArea = new TextAreaControl(this);
                primaryTextArea.Dock = DockStyle.Fill;
                textAreaPanel.Controls.Add(primaryTextArea);
                InitializeTextAreaControl(primaryTextArea);
                Controls.Add(textAreaPanel);
                ResizeRedraw = true;
                Document.UpdateCommited += new EventHandler(CommitUpdateRequested);
                OptionsChanged();
            }

            public PrintDocument PrintDocument
            {
                get
                {
                    if (printDocument == null)
                    {
                        printDocument = new PrintDocument();
                        printDocument.BeginPrint += new PrintEventHandler(this.BeginPrint);
                        printDocument.PrintPage += new PrintPageEventHandler(this.PrintPage);
                    }
                    return printDocument;
                }
            }

            public override TextAreaControl ActiveTextAreaControl
            {
                get
                {
                    return primaryTextArea;
                }
            }

 

            protected virtual void InitializeTextAreaControl(TextAreaControl newControl)
            {
            }

            public override void OptionsChanged()
            {
                primaryTextArea.OptionsChanged();
                if (secondaryTextArea != null)
                {
                    secondaryTextArea.OptionsChanged();
                }
            }

            public void Split()
            {
                if (secondaryTextArea == null)
                {
                    secondaryTextArea = new TextAreaControl(this);
                    secondaryTextArea.Dock = DockStyle.Bottom;
                    secondaryTextArea.Height = Height / 2;
                    textAreaSplitter = new Splitter();
                    textAreaSplitter.BorderStyle = BorderStyle.FixedSingle;
                    textAreaSplitter.Height = 8;
                    textAreaSplitter.Dock = DockStyle.Bottom;
                    textAreaPanel.Controls.Add(textAreaSplitter);
                    textAreaPanel.Controls.Add(secondaryTextArea);
                    InitializeTextAreaControl(secondaryTextArea);
                    secondaryTextArea.OptionsChanged();
                }
                else
                {
                    textAreaPanel.Controls.Remove(secondaryTextArea);
                    textAreaPanel.Controls.Remove(textAreaSplitter);

                    secondaryTextArea.Dispose();
                    textAreaSplitter.Dispose();
                    secondaryTextArea = null;
                    textAreaSplitter = null;
                }
            }

            public bool EnableUndo
            {
                get
                {
                    return Document.UndoStack.CanUndo;
                }
            }
            public bool EnableRedo
            {
                get
                {
                    return Document.UndoStack.CanRedo;
                }
            }

            public void Undo()
            {
                if (Document.ReadOnly)
                {
                    return;
                }
                if (Document.UndoStack.CanUndo)
                {
                    BeginUpdate();
                    Document.UndoStack.Undo();

                    Document.UpdateQueue.Clear();
                    Document.RequestUpdate(new TextAreaUpdate(TextAreaUpdateType.WholeTextArea));
                    this.primaryTextArea.TextArea.UpdateMatchingBracket();
                    if (secondaryTextArea != null)
                    {
                        this.secondaryTextArea.TextArea.UpdateMatchingBracket();
                    }
                    EndUpdate();
                }
            }

            public void Redo()
            {
                if (Document.ReadOnly)
                {
                    return;
                }
                if (Document.UndoStack.CanRedo)
                {
                    BeginUpdate();
                    Document.UndoStack.Redo();

                    Document.UpdateQueue.Clear();
                    Document.RequestUpdate(new TextAreaUpdate(TextAreaUpdateType.WholeTextArea));
                    this.primaryTextArea.TextArea.UpdateMatchingBracket();
                    if (secondaryTextArea != null)
                    {
                        this.secondaryTextArea.TextArea.UpdateMatchingBracket();
                    }
                    EndUpdate();
                }
            }

            public void SetHighlighting(string name)
            {
                Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy(name);
            }


            #region Update Methods
            public override void EndUpdate()
            {
                base.EndUpdate();
                Document.CommitUpdate();
            }

            void CommitUpdateRequested(object sender, EventArgs e)
            {
                if (IsUpdating)
                {
                    return;
                }
                foreach (TextAreaUpdate update in Document.UpdateQueue)
                {
                    switch (update.TextAreaUpdateType)
                    {
                        case TextAreaUpdateType.PositionToEnd:
                            this.primaryTextArea.TextArea.UpdateToEnd(update.Position.Y);
                            if (this.secondaryTextArea != null)
                            {
                                this.secondaryTextArea.TextArea.UpdateToEnd(update.Position.Y);
                            }
                            break;
                        case TextAreaUpdateType.PositionToLineEnd:
                        case TextAreaUpdateType.SingleLine:
                            this.primaryTextArea.TextArea.UpdateLine(update.Position.Y);
                            if (this.secondaryTextArea != null)
                            {
                                this.secondaryTextArea.TextArea.UpdateLine(update.Position.Y);
                            }
                            break;
                        case TextAreaUpdateType.SinglePosition:
                            this.primaryTextArea.TextArea.UpdateLine(update.Position.Y, update.Position.X, update.Position.X);
                            if (this.secondaryTextArea != null)
                            {
                                this.secondaryTextArea.TextArea.UpdateLine(update.Position.Y, update.Position.X, update.Position.X);
                            }
                            break;
                        case TextAreaUpdateType.LinesBetween:
                            this.primaryTextArea.TextArea.UpdateLines(update.Position.X, update.Position.Y);
                            if (this.secondaryTextArea != null)
                            {
                                this.secondaryTextArea.TextArea.UpdateLines(update.Position.X, update.Position.Y);
                            }
                            break;
                        case TextAreaUpdateType.WholeTextArea:
                            this.primaryTextArea.TextArea.Invalidate();
                            if (this.secondaryTextArea != null)
                            {
                                this.secondaryTextArea.TextArea.Invalidate();
                            }
                            break;
                    }
                }
                Document.UpdateQueue.Clear();
                this.primaryTextArea.TextArea.Update();
                if (this.secondaryTextArea != null)
                {
                    this.secondaryTextArea.TextArea.Update();
                }
                //			Console.WriteLine("-------END");
            }
            #endregion

            #region Printing routines
            int curLineNr = 0;
            float curTabIndent = 0;
            StringFormat printingStringFormat;

            void BeginPrint(object sender, PrintEventArgs ev)
            {
                curLineNr = 0;
                printingStringFormat = (StringFormat)System.Drawing.StringFormat.GenericTypographic.Clone();

                // 100 should be enough for everyone ...err ?
                float[] tabStops = new float[100];
                for (int i = 0; i < tabStops.Length; ++i)
                {
                    tabStops[i] = TabIndent * primaryTextArea.TextArea.TextView.GetWidth(' ');
                }

                printingStringFormat.SetTabStops(0, tabStops);
            }

            void Advance(ref float x, ref float y, float maxWidth, float size, float fontHeight)
            {
                if (x + size < maxWidth)
                {
                    x += size;
                }
                else
                {
                    x = curTabIndent;
                    y += fontHeight;
                }
            }

            // btw. I hate source code duplication ... but this time I don't care !!!!
            float MeasurePrintingHeight(Graphics g, LineSegment line, float maxWidth)
            {
                float xPos = 0;
                float yPos = 0;
                float fontHeight = Font.GetHeight(g);
                //			bool  gotNonWhitespace = false;
                curTabIndent = 0;
                foreach (TextWord word in line.Words)
                {
                    switch (word.Type)
                    {
                        case TextWordType.Space:
                            Advance(ref xPos, ref yPos, maxWidth, primaryTextArea.TextArea.TextView.GetWidth(' '), fontHeight);
                            //						if (!gotNonWhitespace) {
                            //							curTabIndent = xPos;
                            //						}
                            break;
                        case TextWordType.Tab:
                            Advance(ref xPos, ref yPos, maxWidth, TabIndent * primaryTextArea.TextArea.TextView.GetWidth(' '), fontHeight);
                            //						if (!gotNonWhitespace) {
                            //							curTabIndent = xPos;
                            //						}
                            break;
                        case TextWordType.Word:
                            //						if (!gotNonWhitespace) {
                            //							gotNonWhitespace = true;
                            //							curTabIndent    += TabIndent * primaryTextArea.TextArea.TextView.GetWidth(' ');
                            //						}
                            SizeF drawingSize = g.MeasureString(word.Word, word.Font, new SizeF(maxWidth, fontHeight * 100), printingStringFormat);
                            Advance(ref xPos, ref yPos, maxWidth, drawingSize.Width, fontHeight);
                            break;
                    }
                }
                return yPos + fontHeight;
            }

            void DrawLine(Graphics g, LineSegment line, float yPos, RectangleF margin)
            {
                float xPos = 0;
                float fontHeight = Font.GetHeight(g);
                //			bool  gotNonWhitespace = false;
                curTabIndent = 0;

                foreach (TextWord word in line.Words)
                {
                    switch (word.Type)
                    {
                        case TextWordType.Space:
                            Advance(ref xPos, ref yPos, margin.Width, primaryTextArea.TextArea.TextView.GetWidth(' '), fontHeight);
                            //						if (!gotNonWhitespace) {
                            //							curTabIndent = xPos;
                            //						}
                            break;
                        case TextWordType.Tab:
                            Advance(ref xPos, ref yPos, margin.Width, TabIndent * primaryTextArea.TextArea.TextView.GetWidth(' '), fontHeight);
                            //						if (!gotNonWhitespace) {
                            //							curTabIndent = xPos;
                            //						}
                            break;
                        case TextWordType.Word:
                            //						if (!gotNonWhitespace) {
                            //							gotNonWhitespace = true;
                            //							curTabIndent    += TabIndent * primaryTextArea.TextArea.TextView.GetWidth(' ');
                            //						}
                            g.DrawString(word.Word, word.Font, BrushRegistry.GetBrush(word.Color), xPos + margin.X, yPos);
                            SizeF drawingSize = g.MeasureString(word.Word, word.Font, new SizeF(margin.Width, fontHeight * 100), printingStringFormat);
                            Advance(ref xPos, ref yPos, margin.Width, drawingSize.Width, fontHeight);
                            break;
                    }
                }
            }

            void PrintPage(object sender, PrintPageEventArgs ev)
            {
                Graphics g = ev.Graphics;
                float yPos = ev.MarginBounds.Top;

                while (curLineNr < Document.TotalNumberOfLines)
                {
                    LineSegment curLine = Document.GetLineSegment(curLineNr);
                    if (curLine.Words != null)
                    {
                        float drawingHeight = MeasurePrintingHeight(g, curLine, ev.MarginBounds.Width);
                        if (drawingHeight + yPos > ev.MarginBounds.Bottom)
                        {
                            break;
                        }

                        DrawLine(g, curLine, yPos, ev.MarginBounds);
                        yPos += drawingHeight;
                    }
                    ++curLineNr;
                }

                // If more lines exist, print another page.
                ev.HasMorePages = curLineNr < Document.TotalNumberOfLines;
            }
            #endregion

 
  
        #endregion

        #region members

        private SyntaxMode _SyntaxMode = SyntaxMode.Custom;
        private int keyPressCount = 0;
        internal protected ContextMenu cmShortcutMenu;
        private Regex _findNextRegex;
        private int _findNextStartPos;
        private string lastSearch;
        private string IntText = "";
        //private bool readOnly = false;
        //private string[] reservedWords;

        public event EventHandler OptionDialog;
        public event EventHandler Execute;

        #endregion

        #region intelisence members

        public event IntelliListEventHandler PropertyPressed;
        public event IntelliListEventHandler IntelliPressed;

        const int minIntelliWidth = 120;
        const int maxIntelliWidth = 400;
        //const int chW = 7;
        //const int iconSpace = 35;

        private int lastPos;
        private int firstPos;
        private bool DoInsert;

        private ListItems _WordList = new ListItems();
        private ListItems _IntelliList = new ListItems();
        private ListItems _ReservedWords = new ListItems();

        //private List<WORD> _WordList = new List<WORD>();
        //private List<WORD> _IntelliList = new List<WORD>();

        //private XmlDocument xmlReservedWords;
        //private XmlNodeList xmlNodeList;
        private IntelliWindow _IntelliWindow;

        private bool m_DroppedDown = false;
        private int m_VisibleRows = 8;
        private int m_TextWidth = 80;
        private IntelliMode _IntelliMode = IntelliMode.Auto;

        private Point curPoint = Point.Empty;
        private Size curSize = Size.Empty;
        //private Point offsetPos = Point.Empty;

        private const int MaxHeight = 75;

    
        #endregion
        
        #region ctor

        public TextEditor()
            : this(SyntaxMode.Custom)
        {
        }

        public TextEditor(SyntaxMode mode)
            : this(mode, IntelliMode.Auto)
        {
        }

        public TextEditor(SyntaxMode syntaxMode, IntelliMode inteliMode)
        {

            _SyntaxMode = syntaxMode;
            _IntelliMode = inteliMode;
            InitEditorControl();
            
            //Init();
            //DoInsert = false;
     
            /////
            /////_IntelliWindow
            /////
            //this._IntelliWindow = new WindowList(this);
            //this._IntelliWindow.SelectionChanged += new SelectionChangedEventHandler(_IntelliWindow_SelectionChanged);
            //this._IntelliWindow.Closed += new System.EventHandler(this.OnPopUpClosed);
            
            this.cmShortcutMenu = new System.Windows.Forms.ContextMenu();

            string mode = _SyntaxMode.ToString();// == SyntaxMode.CSharp ? "C#" : _SyntaxMode.ToString();
            this.Document.HighlightingStrategy = Nistec.SyntaxEditor.Document.HighlightingStrategyFactory.CreateHighlightingStrategy(mode);

            this.ActiveTextAreaControl.TextArea.MouseUp += new MouseEventHandler(TextEditor_MouseUp);
            this.ActiveTextAreaControl.TextArea.DragDrop += new DragEventHandler(TextArea_DragDrop);
            this.ActiveTextAreaControl.TextArea.DragEnter += new DragEventHandler(TextArea_DragEnter);
            this.ActiveTextAreaControl.TextArea.Click += new EventHandler(TextArea_Click);
            this.ActiveTextAreaControl.TextArea.KeyPress += new KeyPressEventHandler(TextArea_KeyPress);
            this.ActiveTextAreaControl.TextArea.KeyUp += new System.Windows.Forms.KeyEventHandler(TextArea_KeyUp);
            this.ActiveTextAreaControl.TextArea.KeyDown += new System.Windows.Forms.KeyEventHandler(TextArea_KeyDown);
            SetReservedWords();
        }

 
        //public void SetOffset(Point p)
        //{
        //    this.offsetPos = p;
        //}

        private void SetReservedWords()
        {
            if (_SyntaxMode == SyntaxMode.Custom)
            {
                return;
            }
            ArrayList list = this.ActiveTextAreaControl.Document.HighlightingStrategy.GetRuleSet(null).KeyWords.ReservedWords;
            string[] array = new string[list.Count];
            list.CopyTo(array);
            _ReservedWords.AddRange(array, 3);
            _ReservedWords.Sort();
            AddReservedWords(true);
        }

        public virtual void AddReservedWords(bool clear)
        {
            if (clear)
                _WordList.Clear();
            if (_ReservedWords != null)
                _WordList.AddRange(_ReservedWords);//, (int)MemberType.Member);
        }

        public virtual void AddReservedWords(string[] words, bool clear)
        {
            if (clear)
                _ReservedWords.Clear();
            if (words != null)
                _ReservedWords.AddRange(words, (int)MemberType.Member);
        }

        public virtual void AddWords(string[] words,MemberType type, bool clear)
        {
            if (clear)
                _WordList.Clear();
            if (words != null)
                _WordList.AddRange(words, (int)type);
        }

        public virtual void ResetIntelliWords(bool addReservedWords)
        {
            _IntelliList.Clear();
            if (addReservedWords && _ReservedWords != null)
                _IntelliList.AddRange(_ReservedWords);//, (int)MemberType.Member);
        }
        #endregion

        #region property

        //[DefaultValue(false)]
        //public new bool ReadOnly
        //{
        //    get { return readOnly; }
        //    set { readOnly = value; }
        //}

        public SyntaxMode SyntaxMode
        {
            get { return _SyntaxMode; }
            set
            {
                if (SyntaxMode != value)
                {
                    _SyntaxMode = value;
                    string mode = _SyntaxMode.ToString();// == SyntaxMode.CSharp ? "C#" : _SyntaxMode.ToString();
                    this.Document.HighlightingStrategy = Nistec.SyntaxEditor.Document.HighlightingStrategyFactory.CreateHighlightingStrategy(mode);
                    SetReservedWords();
                }
            }
        }

        [Browsable(false)]
        public ListItems WordList
        {
            get { return  _WordList; }
        }

        [Browsable(false)]
        public ListItems IntelliList
        {
            get { return _IntelliList; }
        }
        public ListItems ReservedWords
        {
            get { return _ReservedWords; }
            //set { _ReservedWords = value; }
        }
        [DefaultValue(IntelliMode.Auto)]
        public IntelliMode IntelliMode
        {
            get { return _IntelliMode; }
            set { _IntelliMode = value; }
        }

        [Browsable(false),EditorBrowsable(EditorBrowsableState.Advanced),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionStart
        {
            get { return this.ActiveTextAreaControl.Caret.Offset; }
            set { return; }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SelectedText
        {
            get
            {
                return this.ActiveTextAreaControl.TextArea.SelectionManager.SelectedText;
                //return "";
            }
            set
            {
                return;
            }
        }
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string CurrentWord
        {
            get
            {
                int pos = TextUtilities.FindPrevWordStart(this.Document, this.SelectionStart);
                string word = TextUtilities.GetWordAt(this.Document, pos);

                if (word.IndexOf(".") > 0)
                    return word.Substring(word.IndexOf(".") + 1);

                return word;
            }
        }
        #endregion

        #region Context menu
        private void miCopy_Click(object sender, System.EventArgs e)
		{
			this.Copy();
		}
		private void miCut_Click(object sender, System.EventArgs e)
		{
			this.Cut();
		}
		private void miPaste_Click(object sender, System.EventArgs e)
		{
			this.Paste();
		}
		private void miGoToDefinision_Click(object sender, System.EventArgs e)
		{
			this.GoToDefenition();
		}
		private void miGoToRererence_Click(object sender, System.EventArgs e)
		{
			this.GoToReferenceObject();
		}
		private void miGoToAnyRererence_Click(object sender, System.EventArgs e)
		{
			this.GoToReferenceAny();
		}

		private void miOptions_Click(object sender, System.EventArgs e)
		{
            OnOptionDialog(e);
		}

        private void TextEditor_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
            OnMouseUpInternal(e);	
		}
		#endregion

        #region override 

        //public void LoadReservedWords(string xmlFileName, string elementsTagName)
        //{
        //    //xmlReservedWords = xml;

        //    // Snippets
        //    XmlDocument xmlSnippets = new XmlDocument();
        //    xmlSnippets.Load(xmlFileName);//Application.StartupPath + @"\Snippets.xml");
        //    XmlNodeList xmlNodes = xmlSnippets.GetElementsByTagName(elementsTagName);// ("snippets");
        //    if (xmlNodes == null || xmlNodes.Count == 0)
        //        return;
        //    SetNodeList(xmlNodes);

        //    //foreach (XmlNode node in xmlNodeList[0].ChildNodes)
        //    //{
        //    //    ListViewItem lvi = lstv.Items.Add(node.Attributes["name"].Value, -1);
        //    //    string statement = node.InnerText;

        //    //    statement = statement.Replace(@"\n", "\n");
        //    //    statement = statement.Replace(@"\t", "\t");
        //    //    lvi.Tag = statement;

        //    //}

        //}

        //public void SetNodeList(XmlNodeList xml)
        //{
        //   xmlNodeList=xml;
        //}


        public virtual void SetSyntaxMode(string mode)
        {
            this.Document.HighlightingStrategy = Nistec.SyntaxEditor.Document.HighlightingStrategyFactory.CreateHighlightingStrategy(mode);
            // this.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy(token);

        }

        protected virtual void OnOptionDialog(EventArgs e)
        {
            if (OptionDialog != null)
                OptionDialog(this, e);
        }
 
        public virtual void OnExecute(EventArgs e)
        {
            if (Execute != null)
                Execute(this, e);
        }

        protected virtual void OnMouseUpInternal(System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                string word = this.GetCurrentWord();
                IDataObject iData = Clipboard.GetDataObject();

                MenuItem miCopy = new MenuItem("&Copy");
                MenuItem miCut = new MenuItem("C&ut");
                MenuItem miPaste = new MenuItem("&Paste");
                MenuItem miSeparator = new MenuItem("-");
                MenuItem miGoToDefinision = new MenuItem("Go to &Definition");
                MenuItem miGoToRererence = new MenuItem("Go to Object &Reference");
                MenuItem miSeparator3 = new MenuItem("-");
                MenuItem miOptions = new MenuItem("&Options");

                // Events				
                miCopy.Click += new System.EventHandler(this.miCopy_Click);
                miCut.Click += new System.EventHandler(this.miCut_Click);
                miPaste.Click += new System.EventHandler(this.miPaste_Click);
                miGoToDefinision.Click += new System.EventHandler(this.miGoToDefinision_Click);
                miGoToRererence.Click += new System.EventHandler(this.miGoToRererence_Click);
                miOptions.Click += new System.EventHandler(this.miOptions_Click);

                if (!iData.GetDataPresent(DataFormats.Text))
                    miPaste.Enabled = false;

                // Clear all previously added MenuItems.
                cmShortcutMenu.MenuItems.Clear();

                cmShortcutMenu.MenuItems.Add(miCopy);
                cmShortcutMenu.MenuItems.Add(miCut);
                cmShortcutMenu.MenuItems.Add(miPaste);
                cmShortcutMenu.MenuItems.Add(miSeparator);
                cmShortcutMenu.MenuItems.Add(miGoToDefinision);
                cmShortcutMenu.MenuItems.Add(miGoToRererence);
                cmShortcutMenu.MenuItems.Add(miSeparator3);
                cmShortcutMenu.MenuItems.Add(miOptions);

                cmShortcutMenu.Show(this, new Point(e.X, e.Y));

            }

        }

        protected virtual bool OnKeyPressInternal(System.Windows.Forms.KeyEventArgs e)
        {
            if (ReadOnly)
            {
                e.Handled = true;
                return true;
            }
            string keyString = e.ToString();
            string line = this.ActiveTextAreaControl.Caret.Line.ToString();
            string col = this.ActiveTextAreaControl.Caret.Column.ToString();
            //((MainForm)MdiParentForm).SetPandelPositionInfo(line,col);
            if (e.Alt == true && e.KeyValue == 88)
            {
                e.Handled = false;
                OnExecute(EventArgs.Empty);
                return false;
            }

            //if (e.KeyCode == Keys.Down)
            //    lstv.Focus();

            if (e.KeyCode == Keys.F5)
                OnExecute(EventArgs.Empty);

            if (((Control.ModifierKeys & Keys.Control) == Keys.Control) && e.KeyValue == 32
                || e.KeyValue == 190)
            {
                if (e.KeyValue == 190)//point 
                {
                    SetPropertyPos();
                }
                else//space
                {
                    e.Handled = true;
                    primaryTextArea.TextArea.handleSpase = true;
                    if (!m_DroppedDown)
                    {
                        ComplementWord((char)e.KeyValue);
                    }
                    return true;
                }
            }
            else if (((Control.ModifierKeys & Keys.Control) == Keys.Control) && e.KeyValue == 67 || e.KeyValue == 99)
            {
                ToggleComment();
            }
            else if (((Control.ModifierKeys & Keys.Control) == Keys.Control) && e.KeyValue == 70)
            {   //ctrl + F
                Find(true);
            }
            //else if (((Control.ModifierKeys & Keys.Control) == Keys.Control) && e.KeyValue == 72)
            //{   //ctrl + H
            //    Find(true);
            //}
            else if (((Control.ModifierKeys & Keys.Control) == Keys.Control) && e.KeyValue == 71)
            {  //ctrl + G
                GoToLine();
            }
            else if (m_DroppedDown)
            {
                if (e.KeyData == Keys.Space)
                {
                    this.ClosePopUp();
                }
                else
                {
                    string word = GetLastWord((char)e.KeyValue);
                    if (word.Length > 0)
                    {
                        ComplementWord(word);
                    }
                    else
                    {
                        this.ClosePopUp();
                    }
                }
            }
            return false;
        }

        protected override void OnDragDrop(DragEventArgs e)
        {
            if (ReadOnly)
            {
                return;
            }

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Assign the file names to a string array, in 
                // case the user has selected multiple files.
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                try
                {
                    string fullName = files[0];
                    if (files.Length > 1)
                        return;
                    //MainForm mainform = (MainForm)MdiParentForm;
                    string fileName = Path.GetFileName(fullName);
                    //string line;
                    string fileContent = "";

                    //FrmEditor frmquery = new FrmEditor();//(MdiParentForm);

                    StreamReader sr = new StreamReader(fullName);
                    fileContent = sr.ReadToEnd();
                    //					while ((line = sr.ReadLine()) != null) 
                    //					{
                    //						fileContent += "\n" + line;
                    //					}
                    sr.Close();
                    sr = null;

                    this.Text = fileContent;
                    this.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            base.OnDragDrop(e);
        }

        protected override void OnDragEnter( DragEventArgs e)
        {
            if (((DragEventArgs)e).Data.GetDataPresent(DataFormats.FileDrop))
                ((DragEventArgs)e).Effect = DragDropEffects.Copy;
            // else
            //    ((DragEventArgs)e).Effect = DragDropEffects.None;

        }

        private int PreviusIndexOf(string character)
        {
            for (int i = this.SelectionStart; i > 0; i--)
            {
                if (this.Text.Substring(i - 1, 1) == character)
                {
                    return i;
                }
            }
            return 0;
        }

        #endregion

        #region old intelisence

        //private void SetPropertyPos()
        //{
        //    try
        //    {
        //        lastPos = this.SelectionStart;
        //        string t = this.Text.Substring(0, lastPos);
        //        int lastSpace = t.LastIndexOf(" ");
        //        int lastEnter = t.LastIndexOf("\n");
        //        int lastTab = t.LastIndexOf("\t");

        //        firstPos = lastSpace > lastEnter ? lastSpace : lastEnter;
        //        firstPos = firstPos > lastTab ? firstPos : lastTab;
        //        firstPos++;
        //        string word = t.Substring(firstPos, t.Length - firstPos);
        //        int dotPos = word.IndexOf(".");
        //        if (dotPos > 0)
        //        {
        //            word = word.Substring(dotPos + 1);
        //            firstPos += dotPos + 1;
        //        }
        //        ApplyProperty(word);

        //    }
        //    catch { return; }
        //}
        //private void ToggleComment()
        //{
        //    try
        //    {
        //        int start = this.ActiveTextAreaControl.SelectionManager.SelectionCollection[0].Offset;

        //        string[] lineArray = this.SelectedText.Split('\n');

        //        if (this.ActiveTextAreaControl.SelectionManager.IsSelected(start))
        //        {
        //            if (lineArray.Length > 1)
        //            {
        //                int currpos = start;
        //                for (int xx = 0; xx <= lineArray.Length - 1; xx++)
        //                {
        //                    if (lineArray[xx].Length == 0 && xx == (lineArray.Length - 1))
        //                        break;
        //                    if (lineArray[xx].IndexOf("--", 0) == 0)
        //                    {
        //                        this.Document.Replace(currpos, 2, "");
        //                        currpos -= 2;
        //                    }
        //                    else
        //                    {
        //                        this.Document.Insert(currpos, "--");
        //                        currpos += 2;
        //                    }
        //                    currpos += lineArray[xx].Length + 1;
        //                }
        //            }
        //        }
        //    }
        //    catch { return; } // probably no selected text
        //}

        //protected virtual void ComplementWord()
        //{
        //    try
        //    {
        //        lstv.Items.Clear();
        //        int textWidth = 200;
        //        int fac = 5;
        //        bool isJoining = false;
        //        lastPos = this.SelectionStart;
        //        firstPos = 0;
        //        ToolTip toolTip1 = new ToolTip();
        //        toolTip1.AutoPopDelay = 5000;
        //        toolTip1.InitialDelay = 1000;
        //        toolTip1.ReshowDelay = 500;
        //        toolTip1.ShowAlways = true;

        //        int lastSpace = PreviusIndexOf(" ");
        //        int lastEnter = PreviusIndexOf("\n");
        //        int firstTab = PreviusIndexOf("\t");

        //        if (lastSpace > 0)
        //            firstPos = lastSpace;
        //        if (lastEnter > firstPos)
        //            firstPos = lastEnter;
        //        if (firstTab > firstPos)
        //            firstPos = firstTab;

        //        string word = (this.Text.Substring(firstPos, lastPos - firstPos));

        //        int dotPos = word.IndexOf(".");

        //        if (word.Length == 0 && !isJoining)
        //        {
        //            // Snippets
        //            //XmlDocument xmlSnippets = new XmlDocument();
        //            //xmlSnippets.Load(Application.StartupPath + @"\Snippets.xml");
        //            //XmlNodeList xmlNodeList = xmlSnippets.GetElementsByTagName("snippets");

        //            //foreach (XmlNode node in xmlNodeList[0].ChildNodes)
        //            //{
        //            //    ListViewItem lvi = lstv.Items.Add(node.Attributes["name"].Value, 5);
        //            //    string statement = node.InnerText;

        //            //    statement = statement.Replace(@"\n", "\n");
        //            //    statement = statement.Replace(@"\t", "\t");
        //            //    lvi.Tag = statement;

        //            //}
        //        }
        //        else if (!isJoining)
        //        {
        //            //Clear
        //            foreach (ListViewItem l in lstv.Items)
        //                l.Remove();

        //            // Variables
        //            //if (word.Substring(0, 1) == "@")
        //            //{
        //            foreach (string var in this.GetVariables(word))
        //            {
        //                if ((var.Length * fac) > textWidth)
        //                    textWidth = var.Length * fac;

        //                lstv.Items.Add(var, 2);
        //            }
        //            //}
        //            //else
        //            //{
        //            if (xmlNodeList != null && xmlNodeList.Count > 0)
        //            {
        //                //Reserved Words
        //                foreach (XmlNode node in xmlNodeList[0].ChildNodes)
        //                {
        //                    if (node.Name.StartsWith(word.ToUpper()))
        //                    {
        //                        if ((node.Name.Length * fac) > textWidth)
        //                            textWidth = node.Name.Length * fac;
        //                        lstv.Items.Add(node.Name, 2);
        //                    }
        //                }
        //            }

        //            //Operations

        //            foreach (string name in _WordList)
        //            {
        //                lstv.Items.Add(name);
        //            }
        //            //}
        //        }

        //        if (lstv.Items.Count == 0)	  //No match
        //            return;
        //        else if (lstv.Items.Count == 1) //One Match
        //        {
        //            this.Document.Replace(firstPos, lastPos - firstPos, lstv.Items[0].Text);
        //            int pos = firstPos + lstv.Items[0].Text.Length;

        //            this.SetPosition(pos);
        //        }
        //        else								  //Selection is required
        //        {
        //            //DoInsert = true;
        //            int formHeight = this.Size.Height;
        //            int formWidth = this.Size.Width;
        //            lstv.Width = textWidth;

        //            Point pt = this.GetPositionFromCharIndex(this.SelectionStart);
        //            pt.Y += this.Font.Height;

        //            if (pt.Y + lstv.Height > formHeight)
        //                pt.Y = pt.Y - (lstv.Height + (this.Font.Height / 2));

        //            if (pt.X + lstv.Width > formWidth)
        //                pt.X = pt.X - lstv.Width;

        //            lstv.Location = pt;

        //            lstv.Visible = true;
        //            lstv.Focus();
        //            lstv.Items[0].Selected = true;
        //        }

        //    }
        //    catch { return; }

        //}

        //protected virtual void ApplyProperty(string word)
        //{
        //    try
        //    {
        //        //ArrayList DatabaseObjects = null;
        //        if (word.Length > 0)
        //        {
        //            //Clear
        //            foreach (ListViewItem l in lstv.Items)
        //                l.Remove();

        //            //word = GetAliasTableName(word);

        //            if (_WordList.Count > 0)
        //                lstv.Items.Add("*", 2);

        //            foreach (string column in _WordList)
        //            {
        //                //string column = dbObjectProperties.Name.IndexOf("(") > 0 ? dbObjectProperties.Name.Substring(0, dbObjectProperties.Name.IndexOf("(")) : dbObjectProperties.Name;
        //                lstv.Items.Add(column.Trim(), 3);
        //            }

        //            lastPos++;
        //            firstPos = lastPos;
        //            if (lstv.Items.Count == 0)	  //No match
        //                return;
        //            else if (lstv.Items.Count == 1) //One Match
        //            {
        //                this.Select(firstPos, lastPos - firstPos);
        //                this.SelectedText = lstv.Items[0].Text;
        //            }
        //            else						  //Selection is required
        //            {
        //                //DoInsert = true;
        //                int formHeight = this.Size.Height;
        //                int formWidth = this.Size.Width;
        //                lstv.Width = 200;

        //                Point pt = this.GetPositionFromCharIndex(this.SelectionStart);
        //                pt.Y += this.Font.Height;

        //                if (pt.Y + lstv.Height > formHeight)
        //                    pt.Y = pt.Y - (lstv.Height + (this.Font.Height / 2));

        //                if (pt.X + lstv.Width > formWidth)
        //                    pt.X = pt.X - lstv.Width;

        //                lstv.Location = pt;

        //                lstv.Visible = true;
        //                lstv.Focus();
        //                lstv.Items[0].Selected = true;
        //            }
        //        }
        //    }
        //    catch { return; }

        //}

        #endregion

        #region Public methods

        public List<string> GetVariables(string Stringmatch)
        {
            List<string> arrList = new List<string>();
            string s = this.Text;
            string pat = @"\100+\w+";
            Regex r = new Regex(pat, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Match m;

            for (m = r.Match(s); m.Success; m = m.NextMatch())
            {
                if (!arrList.Contains(m.Value))
                    arrList.Add(m.Value);

            }
            return arrList;
        }
        public string GetLastWord(char c)
        {

            lastPos = this.SelectionStart;
            string t = this.Text.Substring(0, lastPos);
            if (TextUtilities.IsLetterDigitOrUnderscore(c))
            {
                t += c.ToString();
                lastPos++;
            }
            int offset = lastPos - 1;
            while (offset > 0 && TextUtilities.IsLetterDigitOrUnderscore(t[offset]))
            {
                --offset;
            }
            if (offset == 0 && TextUtilities.IsLetterDigitOrUnderscore(t[offset]))
                firstPos = offset;
            else
                firstPos = offset + 1;
            return t.Substring(firstPos, t.Length - firstPos);
        }

        public string GetLastWord()
        {

            lastPos = this.SelectionStart;
            string t = this.Text.Substring(0, lastPos);
            
            int offset = lastPos - 1;
            while (offset > 0 && TextUtilities.IsLetterDigitOrUnderscore(t[offset]))
            {
                --offset;
            }
            if (offset == 0 && TextUtilities.IsLetterDigitOrUnderscore(t[offset]))
                firstPos = offset;
            else
                firstPos = offset + 1;
            return t.Substring(firstPos, t.Length - firstPos);


            //lastPos = this.SelectionStart;
            //firstPos = 0;

            //int lastSpace = PreviusIndexOf(" ");
            //int lastEnter = PreviusIndexOf("\n");
            //int firstTab = PreviusIndexOf("\t");

            //if (lastSpace > 0)
            //    firstPos = lastSpace;
            //if (lastEnter > firstPos)
            //    firstPos = lastEnter;
            //if (firstTab > firstPos)
            //    firstPos = firstTab;

            //return (this.Text.Substring(firstPos, lastPos - firstPos));

        }

         public Point GetPositionFromCharIndex(int position)
        {
            Point p;
            try
            {
                // FIX!				

                p = this.ActiveTextAreaControl.Caret.ScreenPosition;//.Position;
                Point thispos=this.Location;
                return new Point(p.X + thispos.X, p.Y + thispos.Y); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public int GetLineFromCharIndex(int offSet)
        {
            return this.Document.GetLineNumberForOffset(offSet);
        }

        public string GetCurrentWord()
        {
            int pos = this.ActiveTextAreaControl.Caret.Offset;
            string word =Nistec.SyntaxEditor.Document.TextUtilities.GetWordAt(this.Document, pos);
            if (word.Length == 0 && (this.Text.Length > pos - 1))
                word = Nistec.SyntaxEditor.Document.TextUtilities.GetWordAt(this.Document, pos - 1);

            return word.Trim();
        }

        public void Select(int startPos, int length)
        {
            Point startPoint = this.ActiveTextAreaControl.TextArea.Document.OffsetToPosition(startPos);
            Point endPoint = this.ActiveTextAreaControl.TextArea.Document.OffsetToPosition(startPos + length);

            this.ActiveTextAreaControl.TextArea.SelectionManager.SetSelection(startPoint, endPoint);
        }

        public string GetText(int startPos, int length)
        {
            return this.Document.TextBufferStrategy.GetText(startPos, length);
        }

        public string Mark(int startPos, int length)
        {
            Point startPoint = GetPositionFromCharIndex(startPos);
            Point endPoint = GetPositionFromCharIndex(startPos + length);
            this.ActiveTextAreaControl.TextArea.SelectionManager.SetSelection(startPoint, endPoint);
            return this.Document.TextBufferStrategy.GetText(startPos, length);
        }

        public void UndoAction()
        {
            this.Undo();
        }
 
        public void SetSelectionUnderlineStyle(object underlineStyle)
        {

            return;
        }
        public void SetSelectionUnderlineColor(object underlineColor)
        {
            return;
        }

        public void SetPosition(int pos)
        {
            TextArea textArea = this.ActiveTextAreaControl.TextArea;
            textArea.Caret.Position = this.Document.OffsetToPosition(pos);
        }

        public void SetLine(int line)
        {
            TextArea textArea = this.ActiveTextAreaControl.TextArea;
            textArea.Caret.Column = 0;
            textArea.Caret.Line = line;
            textArea.Caret.UpdateCaretPosition();
        }

        public void Paste()
        {
            if (ReadOnly)
            {
                return;
            }

            this.ActiveTextAreaControl.TextArea.ClipboardHandler.Paste(null, null);

        }
        public void Copy()
        {
            this.ActiveTextAreaControl.TextArea.ClipboardHandler.Copy(null, null);
        }
        public void Cut()
        {
            this.ActiveTextAreaControl.TextArea.ClipboardHandler.Cut(null, null);
        }

        /// <summary>
        /// GoToDefenition
        /// </summary>
        public void GoToDefenition()
        {
            this.Cursor = Cursors.WaitCursor;
            string objectName = this.GetCurrentWord();
            if (objectName.Length == 0)
            {
                MessageBox.Show("The referenced object was not found", "Go to reference", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Cursor = Cursors.Default;
                return;
            }

            if (_findNextRegex != null && lastSearch.Equals(objectName))
                FindNext();
            else
                this.Find(new Regex(objectName), 0);
            lastSearch = objectName;
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        ///GoToReferenceObject
         /// </summary>
        public void GoToReferenceObject()
        {
            this.Cursor = Cursors.WaitCursor;
            string objectName = this.GetCurrentWord();
            if (objectName.IndexOf(".") > -1)
                objectName = objectName.Substring(objectName.IndexOf(".") + 1);

            if (objectName.Length == 0)
            {
                MessageBox.Show("The referenced object was not found", "Go to reference", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Cursor = Cursors.Default;
                return;
            }

            if (_findNextRegex.Equals(objectName))
                FindNext();
            else
                this.Find(new Regex(objectName), 0);
            lastSearch = objectName;

            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// GoToReferenceAny
        /// </summary>
        public void GoToReferenceAny()
        {
            this.Cursor = Cursors.WaitCursor;
            string objectName = this.GetCurrentWord();
            if (objectName.Length == 0)
            {
                MessageBox.Show("The referenced object was not found", "Go to reference", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Cursor = Cursors.Default;
                return;
            }

            if (_findNextRegex.Equals(objectName))
                FindNext();
            else
                this.Find(new Regex(objectName), 0);
            lastSearch = objectName;
            this.Cursor = Cursors.Default;
        }

        public void Find(bool replace)
        {
            SearchDlg dlg = new SearchDlg(this, replace);
            dlg.Show();
        }

        /// <summary>
        /// Searches for specified pattern
        /// </summary>
        /// <param name="pathern"></param>
        /// <param name="startPos"></param>
        /// <param name="richTextBoxFinds"></param>
        /// <returns></returns>
        public int Find(Regex regex, int startPos)
        {
            string context = this.Text.Substring(startPos);

            Match m = regex.Match(context);
            if (!m.Success)
            {
                MessageBox.Show("The specified text was not found.", "Nistec", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return 0;
            }

            int line = this.Document.GetLineNumberForOffset(m.Index + startPos);
            this.ActiveTextAreaControl.TextArea.ScrollTo(line);
     
            this.Select(m.Index + startPos, m.Length);
            _findNextRegex = regex;
            _findNextStartPos = m.Index + startPos;

            this.SetPosition(m.Index + m.Length + startPos);
            return m.Index + m.Length + startPos;
        }
        /// <summary>
        /// 
        /// </summary>
        public void FindNext()
        {
            if (_findNextRegex != null)
                Find(_findNextRegex, _findNextStartPos + 1);
        }
        /// <summary>
        /// Searches for specified pattern and replaces it
        /// </summary>
        /// <param name="pathern"></param>
        /// <param name="startPos"></param>
        /// <param name="richTextBoxFinds"></param>
        /// <returns></returns>
        public int Replace(Regex regex, int startPos, string replaceWith)
        {
            if (ReadOnly)
            {
                return -1;
            }

            if (this.SelectedText.Length > 0)
            {
                int start = this.ActiveTextAreaControl.SelectionManager.SelectionCollection[0].Offset;
                int length = this.SelectedText.Length;
                this.Document.Replace(start, length, replaceWith);

                return Find(regex, length + start);

            }

            string context = this.Text.Substring(startPos);

            Match m = regex.Match(context);

            if (!m.Success)
            {
                MessageBox.Show("The specified text was not found.", "Nistec", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return 0;
            }
            this.Document.Replace(m.Index + startPos, m.Length, replaceWith);

            return m.Index + m.Length + startPos;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="regex"></param>
        /// <param name="replaceWith"></param>
        public void ReplaceAll(Regex regex, string replaceWith)
        {
            if (ReadOnly)
            {
                return;
            }

            string context = this.Text;

            this.Text = regex.Replace(this.Text, replaceWith);

        }
        /// <summary>
        /// GoToLine
        /// </summary>
        /// <typeparam name="?"></typeparam>
        /// <param name="?"></param>
        /// <returns></returns>
         public void GoToLine()
        {
            int lineNumber = this.GetLineFromCharIndex(this.SelectionStart);
            // Fix Goto Bug
            lineNumber++;
            GotoLineDlg frmGotoLine = new GotoLineDlg(this, lineNumber, this.Document.LineSegmentCollection.Count);
            frmGotoLine.Show();
        }

        /// <summary>
        /// Sets cursor to requested line
        /// </summary>
        /// <param name="line"></param>
        public void GoToLine(int line)
        {
            // Fix Goto Bug
            int offset = this.Document.GetLineSegment(line - 1).Offset;
            int length = this.Document.GetLineSegment(line - 1).Length;
            this.ActiveTextAreaControl.TextArea.ScrollTo(line - 1);
            if (length == 0)
                length++;
            this.Select(offset, length);
            this.SetLine(line - 1);
        }

        #endregion	

        #region internal event

        //protected override void OnDragDrop(DragEventArgs drgevent)
        //{
        //    base.OnDragDrop(drgevent);
        //}

        //private void TextEditor_KeyPressEvent(object sender, System.Windows.Forms.KeyEventArgs e)
        //{
        //    OnKeyPressInternal(e);
        //}

        private void TextArea_DragDrop(object sender, DragEventArgs e)
        {
            //base.OnDragDrop(e);
            OnDragDrop(e);
        }
  
        private void TextArea_DragEnter(object sender, DragEventArgs e)
        {
            OnDragEnter(e);
        }

        private void TextArea_Click(object sender, EventArgs e)
        {
            string line = this.ActiveTextAreaControl.Caret.Line.ToString();
            string col = this.ActiveTextAreaControl.Caret.Column.ToString();
            //((MainForm)MdiParentForm).SetPandelPositionInfo(line,col);
        }
        
        private void TextArea_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                //case ' ':
                //    e.Handled = true;
                //    return;
                // Counts the backspaces.
                case '\b':
                    break;
                // Counts the ENTER keys.
                case '\r':
                    break;
                // Counts the ESC keys.  
                case (char)27:
                    break;
                // Counts all other keys.
                default:
                    keyPressCount = keyPressCount + 1;
                    if (this.IntText.IndexOf("*") <= 0)
                    {
                        this.IntText = this.IntText + "*";
                    }
                    break;
            }
            this.OnKeyPress(e);
        }

        private void TextArea_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back || e.KeyCode == Keys.Enter)
            {
                if (this.IntText.IndexOf("*") <= 0)
                {
                    this.IntText = this.IntText + "*";
                }
            }
            //this.OnKeyUp(e);
        }
       private void TextArea_KeyDown(object sender, KeyEventArgs e)
        {
            if (ReadOnly)
            {
                e.Handled = true;
                return;
            }
            e.Handled = OnKeyPressInternal(e);
            if (!e.Handled)
            {
                this.OnKeyDown(e);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
        
            if (m_DroppedDown)
            {
                switch (keyData)
                {
                    case Keys.Escape:
                    case Keys.Enter:
                    case Keys.Up:
                    case Keys.Down:
                        IntelliCommandKey(keyData);
                        //base.ProcessCmdKey(ref msg, keyData);
                        return true;
                    case Keys.Back:
                    case Keys.Left:
                        string word = GetLastWord();//(char)keyData);
                        if (word.Length <=1)
                        {
                            this.ClosePopUp();
                        }
                        break;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

         #endregion

        #region Intellisence


        private void IntelliCommandKey(Keys keyData)
        {
            if (_IntelliWindow != null)
            {
                _IntelliWindow.CommandKey(keyData);
            }

        }

        private void OnIntelliPressedInternal(string word)
        {
            if (!IsValidList()) return ;

            ResetIntelliWords(false);
            if (word == null)
            {
                _IntelliList.AddRange(_WordList);
            }
            else
            {
                _IntelliList.AddRange(GetWords(_WordList, word.ToLower()));
            }
            OnIntelliPressed(new IntelliListEventArgs(word));
        }

        protected virtual void OnIntelliPressed(IntelliListEventArgs e)
        {
            if (IntelliPressed != null)
                IntelliPressed(this, e);


            if (e.Items.Count>0)
            {
                _IntelliList.AddRange(e.Items);
            }
            if (_IntelliList.Count > 0)
            {
                m_TextWidth = Math.Min(maxIntelliWidth, _IntelliList.MaxWidth);//+iconSpace);
                ShowIntelliWindow();
            }
        }

        protected virtual void OnPropertyPressed(string word)
        {
            OnPropertyPressed(new IntelliListEventArgs(word));
        }

        protected virtual void OnPropertyPressed(IntelliListEventArgs e)
        {
            if (PropertyPressed != null)
                PropertyPressed(this, e);

            if (e.Items.Count>0)
            {
                ShowIntelliWindow(e.Items);
            }
        }   

        public void ShowIntelliWindow(string[] properties, string startWith)
        {
            ListItems wlist = new ListItems();
            wlist.AddRange(properties, (int)MemberType.Property, startWith);

            ShowIntelliWindow(wlist);
        }

        public void ShowIntelliWindow(string[] list,MemberType type)
        {
            ListItems words = new ListItems();
            words.AddRange(list,(int)type);
            ShowIntelliWindow(words);
        }

        public void ShowIntelliWindow(string[] list)
        {
            ListItems words = new ListItems();
            words.AddRange(list);
            ShowIntelliWindow(words);
        }

        public void ShowIntelliWindow(ListItems list)
        {

            //int width = 800;
            int maxW = 0;

            //_IntelliList.Clear();
            ResetIntelliWords(false);
            _IntelliList.AddRange(list);
            maxW = _IntelliList.MaxWidth;

            m_TextWidth = Math.Min(maxIntelliWidth, maxW );//+ iconSpace);
            //return _IntelliList.Count;
            ShowIntelliWindow();
            DoInsert = true;
        }

        private void CreateIntelliWindow()
        {
            this._IntelliWindow = new IntelliWindow(this);
            this._IntelliWindow.SelectionChanged += new SelectionChangedEventHandler(_IntelliWindow_SelectionChanged);
            this._IntelliWindow.Closed += new System.EventHandler(this.OnPopUpClosed);

        }
        private void ReleaseIntelliWindow()
        {
            this._IntelliWindow.SelectionChanged -= new SelectionChangedEventHandler(_IntelliWindow_SelectionChanged);
            this._IntelliWindow.Closed -= new System.EventHandler(this.OnPopUpClosed);
            this._IntelliWindow = null;
        }
        [UseApiElements("ShowWindow")]
        private void ShowIntelliWindow()
        {
            //if (this._IntelliWindow == null)
            //    return; //throw new Exception("Error in GridTableStyle ");
            if (_IntelliList.Count == 0)
                return;

            SetDimension();

            CreateIntelliWindow();
            this._IntelliWindow.DoDropDown(_IntelliList, curPoint);

            //this._IntelliWindow.ShowPopUp(_IntelliList, _IntelliWindow.Handle, curSize, curPoint);
            m_DroppedDown = true;
            //this.Invalidate();
        }

        private void ComplementWord(char c)
        {

            string word = GetLastWord(c);

            ComplementWord(word);

        }

        private void ComplementWord()
        {
  
            string word = GetLastWord();

            ComplementWord(word);

        }

        public void ComplementWord(string word)
        {
            if (!IsValidList())
                return;

            if (_IntelliMode== IntelliMode.Auto)
            {
                if (word.Length == 0)
                {
                    OnIntelliPressedInternal(null);// SetIntelliList();
                }
                else if (m_DroppedDown)
                {
                    _IntelliWindow.FindString(word);
                    return;
                }
                else
                {
                    OnIntelliPressedInternal(word);//SetIntelliList(word);
                }
            }

            else if (_IntelliList.Count == 0)	  //No match
                return;
            else if (_IntelliList.Count == 1) //One Match
            {
                CompleteIntelliWord(_IntelliList[0]);
            }
            else	  //Selection is required
            {
                ShowIntelliWindow();
            }
        }

        private void SetPropertyPos()
        {
            try
            {
                lastPos = this.SelectionStart;
                string t = this.Text.Substring(0, lastPos);
                
                int offset=lastPos-1;
                while (offset > 0 && TextUtilities.IsLetterDigitOrUnderscore(t[offset]))
                {
                    --offset;
                }
                firstPos = offset;

                //int lastSpace = t.LastIndexOf(" ");
                //int lastEnter = t.LastIndexOf("\n");
                //int lastTab = t.LastIndexOf("\t");

                //firstPos = lastSpace > lastEnter ? lastSpace : lastEnter;
                //firstPos = firstPos > lastTab ? firstPos : lastTab;

                if (firstPos > 0)
                {
                    firstPos++;
                }
                string word = t.Substring(firstPos, t.Length - firstPos);
   
                int dotPos = word.IndexOf(".");
                if (dotPos > 0)
                {
                    word = word.Substring(dotPos + 1);
                    firstPos += dotPos + 1;

                    lastPos++;
                    firstPos = lastPos;

                }
                //ApplyProperty(word);
                OnPropertyPressed(word);
            }
            catch { return; }
        }

        //protected virtual void ApplyProperty(string word)
        //{
        //    //try
        //    //{
        //    //    //ArrayList DatabaseObjects = null;
        //    //    if (word.Length > 0)
        //    //    {
        //    //        SetIntelliList(word);

        //    //        lastPos++;
        //    //        firstPos = lastPos;
        //    //        if (_IntelliList.Count == 0)	  //No match
        //    //            return;
        //    //        else if (_IntelliList.Count == 1) //One Match
        //    //        {
        //    //            this.Select(firstPos, lastPos - firstPos);
        //    //            this.SelectedText = _IntelliList[0].ToString();
        //    //        }
        //    //        else						  //Selection is required
        //    //        {
        //    //            ShowIntelliWindow();
        //    //        }
        //    //    }
        //    //}
        //    //catch { return; }

        //}
 
   

        private void ToggleComment()
        {
            try
            {
                int start = this.ActiveTextAreaControl.SelectionManager.SelectionCollection[0].Offset;

                string[] lineArray = this.SelectedText.Split('\n');

                if (this.ActiveTextAreaControl.SelectionManager.IsSelected(start))
                {
                    if (lineArray.Length > 1)
                    {
                        int currpos = start;
                        for (int xx = 0; xx <= lineArray.Length - 1; xx++)
                        {
                            if (lineArray[xx].Length == 0 && xx == (lineArray.Length - 1))
                                break;
                            if (lineArray[xx].IndexOf("--", 0) == 0)
                            {
                                this.Document.Replace(currpos, 2, "");
                                currpos -= 2;
                            }
                            else
                            {
                                this.Document.Insert(currpos, "--");
                                currpos += 2;
                            }
                            currpos += lineArray[xx].Length + 1;
                        }
                    }
                }
            }
            catch { return; } // probably no selected text
        }

        internal void ClosePopUp()
        {
            if (_IntelliWindow != null)
            {
                _IntelliWindow.Close();
            }
        }

        private void OnPopUpClosed(object sender, System.EventArgs e)
        {
            DoInsert = false;
            m_DroppedDown = false;
           //_IntelliWindow.Dispose();//.DisposePopUp(false);
            ReleaseIntelliWindow();
            Invalidate(false);
            this.Focus();
        }

        private void _IntelliWindow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CompleteIntelliWord(e.Value);
        }

        private void CompleteIntelliWord()
        {
            CompleteIntelliWord(_IntelliWindow.SelectedText);
        }

        internal void CompleteIntelliWord(object item)
        {
            if (item != null) //One Match
            {
                int pos = 0;
                if (DoInsert)
                {
                    this.Document.Insert(++lastPos, item.ToString());
                    pos = lastPos + item.ToString().Length;
                }
                else
                {
                    this.Document.Replace(firstPos, lastPos - firstPos, item.ToString());
                    pos = firstPos + item.ToString().Length;
                }
                this.SetPosition(pos);
            }
        }

        private void SetDimension()
        {
            curPoint = Point.Empty;
            curSize = Size.Empty;

            int height = CalcListHeight();

            int formHeight = this.Size.Height;
            int formWidth = this.Size.Width;
            int width = m_TextWidth <= minIntelliWidth ? minIntelliWidth : m_TextWidth;
            width = Math.Min(width, maxIntelliWidth);

            Point pt = this.GetPositionFromCharIndex(this.SelectionStart);
            
            pt.Y += this.Font.Height;

            //if (pt.Y + height > formHeight)
            //{
            //   if (height < formHeight)
            //       pt.Y = pt.Y - (height + (this.Font.Height / 2));
            //   else
            //       pt.Y -= this.Font.Height;
            //}
            //if (pt.X + width > formWidth)
            //    pt.X = pt.X - width;

             //this._IntelliWindow.Parent = this.ParentForm;
            
            //if (this.RightToLeft == RightToLeft.Yes)
            //    curPoint = new Point(this.Left - (width - this.Width), this.Bottom + 2);
            //else
            //    curPoint = new Point(this.Left, this.Bottom + 2);

            curPoint = pt;
            curSize = new Size(width, height);
        }

 
        //private int CalcListWidth()
        //{
        //    //int width = 800;
        //    //int chW=12;
        //    int maxW=0;

        //    foreach (object o in _IntelliList)
        //    {
        //        maxW = Math.Max(maxW, o.ToString().Length * chW);
        //    }

        //    return Math.Min(maxInteliWidth, maxW);

        //}

        private int CalcListHeight()
        {
            int cnt = _IntelliList.Count <= this.m_VisibleRows ? _IntelliList.Count : this.m_VisibleRows;
            int height = (cnt * (ListView.DefaultFont.Height+5));
            //height = Math.Min(height, this.Height);

            int scrollBottom = 30;
            //if(MaxWidth<=this.m_VisibleWidth)
            //    scrollBottom+=Grid.DefaultScrollWidth;

            //int height=m_Grid.Height;
            //height += scrollBottom;

            if (height > this.Height)
                height = this.Height - scrollBottom;

            return height;
        }

        private bool IsValidList()
        {
            bool ok = true;

            switch (_IntelliMode)
            {
                case IntelliMode.None:
                    ok = false;
                    break;
                case IntelliMode.Auto:
                    if (this._ReservedWords != null)
                    {
                        ok = true;
                    }
                    else if (this._WordList == null || _WordList.Count == 0)
                    {
                        ok = false;
                    }
                    break;
                case IntelliMode.Manual:
                    if (this._ReservedWords != null)
                    {
                        ok = true;
                    }
                    else if (this._IntelliList == null || _IntelliList.Count == 0)
                    {
                        ok = false;
                    }
                    break;
            }

            return ok;
        }

        // private int SetIntelliList(string word)
        //{
        //    if (!IsValidList()) return 0;
            
        //    //int width = 800;
        //    //int chW = 10;
        //    int maxW = 0;

        //    //_IntelliList.Clear();
        //    ResetIntelliWords(false);

        //    _IntelliList.AddRange(GetWords(_WordList, word.ToLower()));
        //    maxW = _IntelliList.MaxWidth;

        //    //foreach (string o in _WordList)
        //    //{
        //    //    if (o.ToString().ToLower().StartsWith(word.ToLower()))
        //    //    {
        //    //        _IntelliList.Add(o);
        //    //        maxW = Math.Max(maxW, o.ToString().Length * chW);
        //    //    }
        //    //}
        //    m_TextWidth= Math.Min(maxIntelliWidth, maxW);//+iconSpace);
        //    return _IntelliList.Count;
        //}

        private ListItems GetWords(ListItems list, string word)
        {
            ListItems intelliList = new ListItems();
            //int maxW = 0;
            foreach (ListItem o in list)
            {
                if (o.Text.ToLower().StartsWith(word.ToLower()))
                {
                    intelliList.Add(o);
                    //maxW = Math.Max(maxW, o.Width);
                }
            }
            //intelliList.maxWidth = maxW;
            return intelliList;
        }

        //private int SetIntelliList()
        //{
        //    if (!IsValidList()) return 0;
           
        //    //int width = 400;
        //    //int chW = 7;
        //    int maxW = 0;

        //    //_IntelliList.Clear();
        //    ResetIntelliWords(false);

        //    _IntelliList.AddRange(_WordList);
        //    maxW = _IntelliList.MaxWidth;

        //    //foreach (string o in _WordList)
        //    //{
        //    //    _IntelliList.Add(o);
        //    //    maxW = Math.Max(maxW, o.ToString().Length * chW);
        //    //}

        //    m_TextWidth = Math.Min(maxIntelliWidth, maxW );//+ iconSpace);

        //    return _IntelliList.Count;
        //}

        //private int ParseText(string s)
        //{
        //    //_IntelliList.Clear();
        //    ResetIntelliWords(false);
         
        //    //words.Initialize();
        //    int count = 0;
        //    Regex r = new Regex(@"\w+|[^A-Za-z0-9_ []\f\t\v]", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        //    Match m;

        //    for (m = r.Match(s); m.Success; m = m.NextMatch())
        //    {
        //        if (count >= _WordList.Count) break;
        //        //words[count].Word = m.Value;
        //        //words[count].Position = m.Index;
        //        //words[count].Length = m.Length;
        //        _IntelliList.Add(m.Value);
        //        count++;
        //    }
        //    return count;
        //}
        #endregion

  

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // textAreaPanel
            // 
            this.textAreaPanel.Size = new System.Drawing.Size(232, 236);
            // 
            // TextEditor
            // 
            this.Name = "TextEditor";
            this.Size = new System.Drawing.Size(232, 236);
            this.ResumeLayout(false);

        }
    }
}
