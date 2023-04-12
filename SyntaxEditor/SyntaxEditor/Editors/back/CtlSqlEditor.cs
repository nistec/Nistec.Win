using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Collections;
using System.IO;
using System.Xml;

using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;

namespace mControl.SyntaxEditor
{

    public class CtlSqlEditor : CtlSyntaxEditor
    {

        #region ctor

        public CtlSqlEditor()
            : base(SyntaxMode.SQL)
        {
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
            //			MainForm frm = (MainForm)MdiParentForm;
            //			
            //			FrmOption frmOption = new FrmOption(frm);
            //			frmOption.ShowDialog();

        }

        //private void miAddToSnippet_Click(object sender, System.EventArgs e)
        //{
        //    FrmAddToSnippet frm = new FrmAddToSnippet(this.Text);
        //    frm.ShowDialogWindow(this);

        //}
        //private void miSnippet_Click(object sender, System.EventArgs e)
        //{
        //    string statement = ((SnippetMenuItem)sender).statement;

        //    statement = statement.Replace(@"\n","\n");
        //    statement = statement.Replace(@"\t","\t");

        //    int cursorPos = this.SelectionStart;

        //    this.Document.Replace(cursorPos,0,statement);

        //    if(statement.IndexOf("{}")>-1)
        //        cursorPos = cursorPos + statement.IndexOf("{}")+1;

        //    this.SetPosition(cursorPos);
        //    this.Refresh();
        //}

        //private void miRunCurrentQuery_Click(object sender, System.EventArgs e)
        //{
        //    this.RunCurrentQuery();
        //}

        //private void miValidateCurrentQuery_Click(object sender, EventArgs e)
        //{
        //    this.ResumeLayout();
        //    string contentHolder = this.Content;
        //    if (this.SelectedText.Length > 0)
        //    {
        //        string validate = "SET NOEXEC ON\n\nGO\n\n" + this.SelectedText + "\n\nGO\n\nSET NOEXEC OFF\n\nGO\n\n";
        //        int len = validate.Length;
        //        int pos = this.Content.IndexOf(this.SelectedText);
        //        if (this.Content.IndexOf("SET NOEXEC ON",0) < 0 && pos >= 0 && len > 0)
        //        {	
        //            this.Content = this.Content.Replace(this.SelectedText, validate);
        //            this.Select(pos, len);
        //        }
        //    }
        //    else
        //    {
        //        this.Content = "SET NOEXEC ON\n\nGO\n\n" + this.Content + "\n\nGO\n\nSET NOEXEC OFF\n\nGO\n\n";
        //    }
        //    //this.RunQuery();
        //    this.Content = contentHolder;
        //    this.ResumeLayout();
        //}

         #endregion

        #region override

        public override void SetSyntaxMode(string mode)
        {
            this.Document.HighlightingStrategy = ICSharpCode.TextEditor.Document.HighlightingStrategyFactory.CreateHighlightingStrategy("SQL");
            // this.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy(token);
        }

        protected override void OnMouseUpInternal(System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                string word = this.GetCurrentWord();
                IDataObject iData = Clipboard.GetDataObject();
                //MenuItem miRunCurrentQuery = null;
                //MenuItem miValidateCurrentQuery = null;

                MenuItem miCopy = new MenuItem("&Copy");
                MenuItem miCut = new MenuItem("C&ut");
                MenuItem miPaste = new MenuItem("&Paste");
                MenuItem miSeparator = new MenuItem("-");
                MenuItem miGoToDefinision = new MenuItem("Go to &Definition");
                MenuItem miGoToRererence = new MenuItem("Go to Object &Reference");
                //MenuItem miGoToAnyRererence = new MenuItem("Go to an&y Reference");
                //MenuItem miSeparator2 = new MenuItem("-");
                //if (this.SelectedText.Length > 0)
                //{
                //    miRunCurrentQuery = new MenuItem("Run &current selection");
                //    miValidateCurrentQuery = new MenuItem("&Validate current selection");
                //}
                //else
                //{
                //    miRunCurrentQuery = new MenuItem("Run &current query");
                //    miValidateCurrentQuery = new MenuItem("&Validate current query");
                //}
                MenuItem miSeparator3 = new MenuItem("-");
                MenuItem miOptions = new MenuItem("&Options");
                //MenuItem miSeparator4 = new MenuItem("-");
                //MenuItem miSnippets = new MenuItem("&Snippets");
                //MenuItem miAddToSnippets = new MenuItem("&Add to snippets");

                // Events				
                miCopy.Click += new System.EventHandler(this.miCopy_Click);
                miCut.Click += new System.EventHandler(this.miCut_Click);
                miPaste.Click += new System.EventHandler(this.miPaste_Click);
                miGoToDefinision.Click += new System.EventHandler(this.miGoToDefinision_Click);
                miGoToRererence.Click += new System.EventHandler(this.miGoToRererence_Click);
                //miGoToAnyRererence.Click += new System.EventHandler(this.miGoToAnyRererence_Click);
                //miRunCurrentQuery.Click += new System.EventHandler(this.miRunCurrentQuery_Click);
                //miValidateCurrentQuery.Click += new EventHandler(miValidateCurrentQuery_Click);
                miOptions.Click += new System.EventHandler(this.miOptions_Click);
                //miAddToSnippets.Click += new System.EventHandler(this.miAddToSnippet_Click);

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
                //cmShortcutMenu.MenuItems.Add(miGoToAnyRererence);
                //cmShortcutMenu.MenuItems.Add(miSeparator2);
                //cmShortcutMenu.MenuItems.Add(miRunCurrentQuery);
                //cmShortcutMenu.MenuItems.Add(miValidateCurrentQuery);
                cmShortcutMenu.MenuItems.Add(miSeparator3);
                cmShortcutMenu.MenuItems.Add(miOptions);
                //cmShortcutMenu.MenuItems.Add(miSeparator4);
                //cmShortcutMenu.MenuItems.Add(miSnippets);


                // Snippets
                //XmlDocument xmlSnippets = new XmlDocument();
                //xmlSnippets.Load(Application.StartupPath+@"\Snippets.xml");
                //XmlNodeList xmlNodeList = xmlSnippets.GetElementsByTagName("snippets");

                //if(this.SelectedText.Length>1)
                //    miSnippets.MenuItems.Add(miAddToSnippets);

                //foreach(XmlNode node in xmlNodeList[0].ChildNodes)
                //{
                //    SnippetMenuItem snippet = new SnippetMenuItem();
                //    snippet.Text = node.Attributes["name"].Value;
                //    snippet.statement = node.InnerText;
                //    //snippet.Click+= new System.EventHandler(this.miSnippet_Click);

                //    miSnippets.MenuItems.Add(snippet);
                //}

                cmShortcutMenu.Show(this, new Point(e.X, e.Y));

            }

        }
        protected override void OnKeyPressEvent(System.Windows.Forms.KeyEventArgs e)
        {
            base.OnKeyPressEvent(e);
        }

        protected override void OnDragDrop(DragEventArgs e)
        {
            //			if (e.Data.GetData("QueryCommander.General.QCTreeNode", false) != null)
            //			{
            //				QCTreeNode node = (QCTreeNode)e.Data.GetData("QueryCommander.General.QCTreeNode", false);
            //				if(node.objecttype==QCTreeNode.ObjectType.Table ||
            //					node.objecttype==QCTreeNode.ObjectType.View ||
            //					node.objecttype==QCTreeNode.ObjectType.Filed)	
            //				{
            //					System.Drawing.Rectangle r = this.RectangleToClient(this.ClientRectangle);
            //					Point p = new Point(e.X+r.X,e.Y+r.Y);
            //					
            //					_dragPos = this.GetCharIndexFromPosition(p);
            //					_dragObject = node;
            //					
            //					string objectName=node.objectName;
            //					if(node.objecttype==QCTreeNode.ObjectType.Filed)
            //					{
            //						int spacePos = objectName.IndexOf(" ");
            //						if(spacePos>0)
            //							objectName =objectName.Substring(0,spacePos);
            //
            //						objectName= ((QCTreeNode)node.Parent.Parent).objectName + "." + objectName;
            //
            //					}
            //					_dragObject.objectName=objectName;
            //					SetDragAndDropContextMenu(node);
            //
            //					foreach(MainForm.DBConnection c in ((MainForm)this.MdiParentForm).DBConnections)
            //					{
            //						if(c.ConnectionName == node.server)
            //						{
            //							SetDatabaseConnection(node.database,c.Connection);
            //							break;
            //						}
            //					}
            //					
            //					cmDragAndDrp.Show(this,p);
            //					
            //				}
            //				return;
            //			}

            base.OnDragDrop(e);
        }

        protected override void OnDragEnter(DragEventArgs e)
        {

            //			if (e.Data.GetData("QueryCommander.General.QCTreeNode", false) != null)
            //			{
            //				QCTreeNode node = (QCTreeNode)e.Data.GetData("QueryCommander.General.QCTreeNode", false);
            //				if(node.objecttype==QCTreeNode.ObjectType.Table ||node.objecttype==QCTreeNode.ObjectType.View)
            //					((DragEventArgs)e).Effect = DragDropEffects.Copy;
            //				else if(node.objecttype==QCTreeNode.ObjectType.Filed)
            //					((DragEventArgs)e).Effect = DragDropEffects.Copy;
            //
            //				return;
            //			}
            //

            base.OnDragEnter(e);
        }

        //protected override void ComplementWord()
        //{
        //    try
        //    {
        //        lstv_Commands.Items.Clear();
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

        //        if (word.Length == 0 &&
        //            this.Text.Substring(lastPos - 3, 3).ToUpper() == "ON ")
        //        {
        //            // JOINING 
        //            SQL.SQLStatement statement = new SQL.SQLStatement(this.Text, this.SelectionStart, SQL.SQLStatement.SearchOrder.asc, DBConnectionType.MicrosoftSqlClient);
        //            if (DatabaseFactory.GetConnectionType(dbConnection) == DBConnectionType.MicrosoftSqlClient)
        //            {
        //                isJoining = true;

        //                QueryCommander.Database.Microsoft.Sql2000.DataManager db = (Database.Microsoft.Sql2000.DataManager)DatabaseFactory.CreateNew(dbConnection);

        //                foreach (string join in db.GetJoiningReferences(DatabaseName, statement.CurrentTable, dbConnection, statement.Tables, statement.Aliases))
        //                    lstv_Commands.Items.Add(join, 2);
        //            }
        //        }

        //        int dotPos = word.IndexOf(".");
        //        if (dotPos > 0 && DatabaseFactory.GetConnectionType(dbConnection) != Database.DBConnectionType.DB2)
        //        {

        //            word = word.Substring(dotPos + 1);
        //            firstPos += dotPos + 1;
        //        }

        //        if (word.Length == 0 && !isJoining)
        //        {
        //            // Snippets
        //            XmlDocument xmlSnippets = new XmlDocument();
        //            xmlSnippets.Load(Application.StartupPath + @"\Snippets.xml");
        //            XmlNodeList xmlNodeList = xmlSnippets.GetElementsByTagName("snippets");

        //            foreach (XmlNode node in xmlNodeList[0].ChildNodes)
        //            {
        //                ListViewItem lvi = lstv_Commands.Items.Add(node.Attributes["name"].Value, 5);
        //                string statement = node.InnerText;

        //                statement = statement.Replace(@"\n", "\n");
        //                statement = statement.Replace(@"\t", "\t");
        //                lvi.Tag = statement;

        //            }
        //        }
        //        else if (!isJoining)
        //        {
        //            //Clear
        //            foreach (ListViewItem l in lstv_Commands.Items)
        //                l.Remove();

        //            // Variables
        //            if (word.Substring(0, 1) == "@")
        //            {
        //                foreach (string var in this.GetVariables(word))
        //                {
        //                    if ((var.Length * fac) > textWidth)
        //                        textWidth = var.Length * fac;

        //                    lstv_Commands.Items.Add(var, 2);
        //                }
        //            }
        //            else
        //            {
        //                //Reserved Words
        //                foreach (XmlNode node in syntaxReader.xmlNodeList[0].ChildNodes)
        //                {
        //                    if (node.Name.StartsWith(word.ToUpper()))
        //                    {
        //                        if ((node.Name.Length * fac) > textWidth)
        //                            textWidth = node.Name.Length * fac;
        //                        lstv_Commands.Items.Add(node.Name, 2);
        //                    }
        //                }

        //                //Operations
        //                IDatabaseManager db = DatabaseFactory.CreateNew(dbConnection);
        //                //						Database db = new Database();
        //                ArrayList DatabaseObjects = db.GetDatabasesObjects(DatabaseName, word, dbConnection);

        //                foreach (Database.DBObject dbObject in DatabaseObjects)
        //                {
        //                    if ((dbObject.Name.Length * fac) > textWidth)
        //                        textWidth = dbObject.Name.Length * fac;

        //                    //							ListViewItem lvi;
        //                    switch (dbObject.Type.ToUpper())
        //                    {
        //                        case "V ": //Tables
        //                            lstv_Commands.Items.Add(dbObject.Name, 4);
        //                            break;
        //                        case "U ": //Tables
        //                            lstv_Commands.Items.Add(dbObject.Name, 4);
        //                            break;
        //                        case "P ": //Stored Procedures
        //                            lstv_Commands.Items.Add(dbObject.Name, 1);
        //                            break;
        //                        case "FN": //Functions
        //                            lstv_Commands.Items.Add("dbo." + dbObject.Name, 0);
        //                            break;
        //                        case "TF": //Functions
        //                            lstv_Commands.Items.Add("dbo." + dbObject.Name, 0);
        //                            break;
        //                        case "TR": //Triggers
        //                            lstv_Commands.Items.Add("dbo." + dbObject.Name, 0);
        //                            break;
        //                        default:
        //                            lstv_Commands.Items.Add(dbObject.Name, 2);
        //                            break;
        //                    }
        //                }
        //            }
        //        }

        //        if (lstv_Commands.Items.Count == 0)	  //No match
        //            return;
        //        else if (lstv_Commands.Items.Count == 1) //One Match
        //        {
        //            this.Document.Replace(firstPos, lastPos - firstPos, lstv_Commands.Items[0].Text);
        //            int pos = firstPos + lstv_Commands.Items[0].Text.Length;

        //            this.SetPosition(pos);
        //        }
        //        else								  //Selection is required
        //        {
        //            DoInsert = true;
        //            int formHeight = this.Size.Height;
        //            int formWidth = this.Size.Width;
        //            lstv_Commands.Width = textWidth;

        //            Point pt = this.GetPositionFromCharIndex(this.SelectionStart);
        //            pt.Y += this.Font.Height;

        //            if (pt.Y + lstv_Commands.Height > formHeight)
        //                pt.Y = pt.Y - (lstv_Commands.Height + (this.Font.Height / 2));

        //            if (pt.X + lstv_Commands.Width > formWidth)
        //                pt.X = pt.X - lstv_Commands.Width;

        //            lstv_Commands.Location = pt;

        //            lstv_Commands.Visible = true;
        //            lstv_Commands.Focus();
        //            lstv_Commands.Items[0].Selected = true;
        //        }

        //    }
        //    catch { return; }

        //}

        protected override void ApplyProperty2(string word)
        {
            //			try
            //			{
            //				ArrayList DatabaseObjects = null;
            //				if(word.Length>0)
            //				{
            //					//Clear
            //					foreach(ListViewItem l in lstv_Commands.Items)
            //						l.Remove();
            //
            //					word = GetAliasTableName(word);
            //			
            //					//Properties
            //					IDatabaseManager db = DatabaseFactory.CreateNew(dbConnection);
            //					if(DatabaseFactory.GetConnectionType(dbConnection)!=QueryCommander.Database.DBConnectionType.DB2)
            //						DatabaseObjects = db.GetDatabasesObjectProperties(DatabaseName,word,dbConnection);
            //					else
            //						DatabaseObjects = db.GetDatabasesObjectProperties("",word,dbConnection);
            //					if(DatabaseObjects == null)
            //						return;
            //				
            //					if(DatabaseObjects.Count>0)
            //						lstv_Commands.Items.Add("*",2);
            //
            //					foreach(Database.DBObjectProperties dbObjectProperties in DatabaseObjects)
            //					{
            //						string column=dbObjectProperties.Name.IndexOf("(")>0?dbObjectProperties.Name.Substring(0,dbObjectProperties.Name.IndexOf("(")):dbObjectProperties.Name;
            //						lstv_Commands.Items.Add(column.Trim(),3);
            //					}
            //					//					}
            //					lastPos++;
            //					firstPos = lastPos;
            //					if(lstv_Commands.Items.Count==0)	  //No match
            //						return;
            //					else if(lstv_Commands.Items.Count==1) //One Match
            //					{
            //						this.Select(firstPos,lastPos-firstPos);
            //						this.SelectedText = lstv_Commands.Items[0].Text;
            //					}
            //					else								  //Selection is required
            //					{
            //						DoInsert=true;
            //						int formHeight = this.Size.Height;
            //						int formWidth = this.Size.Width;
            //						lstv_Commands.Width=200;
            //
            //						Point pt = this.GetPositionFromCharIndex(this.SelectionStart);
            //						pt.Y += this.Font.Height;
            //
            //						if(pt.Y + lstv_Commands.Height > formHeight)
            //							pt.Y = pt.Y- (lstv_Commands.Height+(this.Font.Height/2));
            //
            //						if(pt.X + lstv_Commands.Width > formWidth)
            //							pt.X = pt.X - lstv_Commands.Width;
            //
            //						lstv_Commands.Location = pt;
            //
            //						lstv_Commands.Visible = true;
            //						lstv_Commands.Focus();
            //						lstv_Commands.Items[0].Selected = true;	
            //					}
            //				}
            //			}
            //			catch{return;}

        }


        #endregion
 
    }
}
