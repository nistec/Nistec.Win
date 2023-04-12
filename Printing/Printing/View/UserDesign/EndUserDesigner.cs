namespace MControl.Printing.View.Design.UserDesigner
{
    using MControl.Printing.View;
    using MControl.Printing.View.Design;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Data;
    using System.Data.OleDb;
    using System.Drawing;
    using System.Drawing.Design;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class EndUserDesigner : UserControl
    {
        internal Control _mtd616;
        private IRootDesigner _var10;
        private ISelectionService _var11;
        private mtd426 _var12;
        private mtd608 _var13;
        private mtd606 _var14;
        private ImageList _var19;
        private ImageList _var20;
        private string _var6;
        private Rectangle _var7;
        private Font _var8;
        private mtd365 _var9;
        internal PanelDesiger mtd610;
        internal Panel mtd611;
        internal Panel mtd612;
        internal Panel mtd613;
        internal Splitter mtd614;
        internal Splitter mtd615;
        internal PropertyExplorer propertyExplorer;//mtd617;
        internal ReportExplorer reportExplorer;// reportExplorer;
        private Container components=null;

        public event LayoutChangedHandler LayoutChanged;

        public event EventHandler SelectionChanged;

        public EndUserDesigner()
        {
            this.InitializeComponent();
            this.var0();
            this.var1();
            this.var2();
            this.var3();
            this.var4();
        }

        internal void mtd619()
        {
            ScriptLanguage scriptLanguage = this.Report.ScriptLanguage;
            string script = this.Report.Script;
            string[] strArray = new string[this.Report.Sections.Count];
            if (this._var9 == null)
            {
                this._var9 = new mtd365();
            }
            int index = 0;
            foreach (Section section in this.Report.Sections)
            {
                strArray[index] = section.Name;
                index++;
            }
            this._var9.mtd366(ref strArray, ref scriptLanguage, ref script);
            this.Report.Script = script;
            if (scriptLanguage == ScriptLanguage.VB)
            {
                this.Report.ScriptLanguage = ScriptLanguage.VB;
            }
            else
            {
                this.Report.ScriptLanguage = ScriptLanguage.CSharp;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void Execute(MControl.Printing.View.Design.CommandType cmd)
        {
            switch (cmd)
            {
                case MControl.Printing.View.Design.CommandType.FileNew:
                    this.var26();
                    return;

                case MControl.Printing.View.Design.CommandType.FileOpen:
                    this.var29();
                    return;

                case MControl.Printing.View.Design.CommandType.FileSave:
                    if (!File.Exists(this._var6))
                    {
                        this.var30();
                        return;
                    }
                    this.mtd610.mtd268.SaveLayout(this._var6);
                    return;

                case MControl.Printing.View.Design.CommandType.FileSaveAs:
                    this.var30();
                    return;

                case MControl.Printing.View.Design.CommandType.PageSettings:
                    this.mtd610.mtd429(this, EventArgs.Empty);
                    return;

                case MControl.Printing.View.Design.CommandType.Delete:
                    this.mtd610.mtd430.mtd433();
                    return;

                case MControl.Printing.View.Design.CommandType.Cut:
                    this.mtd610.mtd430.mtd432();
                    return;

                case MControl.Printing.View.Design.CommandType.Paste:
                    this.mtd610.mtd430.mtd435();
                    return;

                case MControl.Printing.View.Design.CommandType.Copy:
                    this.mtd610.mtd430.mtd434();
                    return;

                case MControl.Printing.View.Design.CommandType.Undo:
                case MControl.Printing.View.Design.CommandType.Redo:
                case MControl.Printing.View.Design.CommandType.InsertReportHF:
                case MControl.Printing.View.Design.CommandType.InsertPageHF:
                case MControl.Printing.View.Design.CommandType.InsertRTFField:
                case MControl.Printing.View.Design.CommandType.LockControls:
                case MControl.Printing.View.Design.CommandType.FontName:
                case MControl.Printing.View.Design.CommandType.FontSize:
                case MControl.Printing.View.Design.CommandType.FontBold:
                case MControl.Printing.View.Design.CommandType.FontItalic:
                case MControl.Printing.View.Design.CommandType.FontUnderline:
                case MControl.Printing.View.Design.CommandType.TextAlignLeft:
                case MControl.Printing.View.Design.CommandType.TextAlignCenter:
                case MControl.Printing.View.Design.CommandType.TextAlignRight:
                case MControl.Printing.View.Design.CommandType.TextAlignJustify:
                case MControl.Printing.View.Design.CommandType.ForeColor:
                case MControl.Printing.View.Design.CommandType.BackColor:
                case MControl.Printing.View.Design.CommandType.LineStyle:
                case MControl.Printing.View.Design.CommandType.LineColor:
                case MControl.Printing.View.Design.CommandType.Border:
                case MControl.Printing.View.Design.CommandType.RTFBullets:
                case MControl.Printing.View.Design.CommandType.RTFIndent:
                case MControl.Printing.View.Design.CommandType.RTFOutdent:
                    break;

                case MControl.Printing.View.Design.CommandType.ViewReportExplorer:
                    this.var32();
                    return;

                case MControl.Printing.View.Design.CommandType.ViewPropertyGrid:
                    this.var33();
                    return;

                case MControl.Printing.View.Design.CommandType.ViewGrid:
                    if (!this.mtd610.mtd495)
                    {
                        this.mtd610.mtd495 = true;
                        return;
                    }
                    this.mtd610.mtd495 = false;
                    return;

                case MControl.Printing.View.Design.CommandType.ReOrderGroups:
                    this.mtd610.mtd492();
                    return;

                case MControl.Printing.View.Design.CommandType.SetDataSource:
                    this.var16(this, EventArgs.Empty);
                    return;

                case MControl.Printing.View.Design.CommandType.SizeToGrid:
                    this.mtd610.mtd430.mtd444();
                    return;

                case MControl.Printing.View.Design.CommandType.AlignToGrid:
                    this.mtd610.mtd430.mtd436();
                    return;

                case MControl.Printing.View.Design.CommandType.AlignLeft:
                    this.mtd610.mtd430.mtd437();
                    return;

                case MControl.Printing.View.Design.CommandType.AlignRight:
                    this.mtd610.mtd430.mtd439();
                    return;

                case MControl.Printing.View.Design.CommandType.AlignCenter:
                    this.mtd610.mtd430.mtd438();
                    return;

                case MControl.Printing.View.Design.CommandType.AlignTop:
                    this.mtd610.mtd430.mtd440();
                    return;

                case MControl.Printing.View.Design.CommandType.AlignMiddle:
                    this.mtd610.mtd430.mtd441();
                    return;

                case MControl.Printing.View.Design.CommandType.AlignBottom:
                    this.mtd610.mtd430.mtd442();
                    return;

                case MControl.Printing.View.Design.CommandType.CenterHorizontally:
                    this.mtd610.mtd430.mtd455(this.mtd610.mtd268.ReportWidth);
                    return;

                case MControl.Printing.View.Design.CommandType.CenterVertically:
                    this.mtd610.mtd430.mtd456();
                    return;

                case MControl.Printing.View.Design.CommandType.EqualWidth:
                    this.mtd610.mtd430.mtd443();
                    return;

                case MControl.Printing.View.Design.CommandType.EqualHeight:
                    this.mtd610.mtd430.mtd445();
                    return;

                case MControl.Printing.View.Design.CommandType.EqualSize:
                    this.mtd610.mtd430.mtd446();
                    return;

                case MControl.Printing.View.Design.CommandType.SpaceEqualVertical:
                    this.mtd610.mtd430.mtd451();
                    return;

                case MControl.Printing.View.Design.CommandType.SpaceIncreaseVertical:
                    this.mtd610.mtd430.mtd452();
                    return;

                case MControl.Printing.View.Design.CommandType.SpaceDecreaseVertical:
                    this.mtd610.mtd430.mtd453();
                    return;

                case MControl.Printing.View.Design.CommandType.SpaceRemoveVertical:
                    this.mtd610.mtd430.mtd454();
                    return;

                case MControl.Printing.View.Design.CommandType.SpaceEqualHorizontal:
                    this.mtd610.mtd430.mtd447();
                    return;

                case MControl.Printing.View.Design.CommandType.SpaceIncreaseHorizontal:
                    this.mtd610.mtd430.mtd448();
                    return;

                case MControl.Printing.View.Design.CommandType.SpaceDecreaseHorizontal:
                    this.mtd610.mtd430.mtd449();
                    return;

                case MControl.Printing.View.Design.CommandType.SpaceRemoveHorizontal:
                    this.mtd610.mtd430.mtd450();
                    return;

                case MControl.Printing.View.Design.CommandType.BringToFront:
                    this.mtd610.mtd430.mtd457();
                    return;

                case MControl.Printing.View.Design.CommandType.SendToBack:
                    this.mtd610.mtd430.mtd458();
                    return;

                case MControl.Printing.View.Design.CommandType.EditScript:
                    this.mtd619();
                    break;

                default:
                    return;
            }
        }

        private void InitializeComponent()
        {
            base.Name = "EndUserDesigner";
            base.Size = new Size(400, 400);
            base.Load += new EventHandler(this.var5);
        }

        public void LoadLayout(Stream ms)
        {
            this.var3();
            this._var12.mtd599(ms);
            this.var27();
            this.var25();
            this.var28();
            this.var4();
        }

        public void LoadLayout(string fileName)
        {
            this.var3();
            this._var12.mtd599(fileName);
            this.var27();
            this.var25();
            this.var28();
            this.var4();
        }

        public void SaveLayout(Stream stream)
        {
            this.mtd610.mtd268.SaveLayout(ref stream);
        }

        public void SaveLayout(string fileName)
        {
            this.mtd610.mtd268.SaveLayout(fileName);
        }

        private void var0()
        {
            this._var7 = new Rectangle(0, 3, 0x1d, 0x144);
            this._var8 = this.Font;
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.ResizeRedraw, true);
        }

        private void var1()
        {
            this._var12 = new mtd426();
            this._var13 = new mtd608();
            this._var14 = new mtd606();
            IDesignerHost host = this._var12;
            host.AddService(typeof(IToolboxService), this._var13);
            host.AddService(typeof(IMenuCommandService), this._var14);
            this._var12.ComponentRename += new ComponentRenameEventHandler(this.var15);
            this._var12.mtd172();
            this._var14.AddVerb(new DesignerVerb("DataSettings", new EventHandler(this.var16)));
            this._var10 = (IRootDesigner) this._var12.GetDesigner(this._var12.RootComponent);
            this._var11 = (ISelectionService) this._var12.GetService(typeof(ISelectionService));
            this._var11.SelectionChanged += new EventHandler(this.var17);
            this._var12.mtd425.mtd485 += new LayoutChangedHandler(this.var18);
        }

        private void var15(object var36, ComponentRenameEventArgs e)
        {
            this.var25();
        }

        private void var16(object var31, EventArgs e)
        {
            OleDbDataAdapter dataAdapter = null;
            DataSourceDlg source = new DataSourceDlg();
            source.ConnectionString = "";
            source.SqlString = "";
            if ((this.Report.DataAdapter != null) && (this.Report.DataAdapter is OleDbDataAdapter))
            {
                dataAdapter = (OleDbDataAdapter) this.Report.DataAdapter;
                if (dataAdapter.SelectCommand != null)
                {
                    source.SqlString = dataAdapter.SelectCommand.CommandText;
                    if (dataAdapter.SelectCommand.Connection != null)
                    {
                        source.ConnectionString = dataAdapter.SelectCommand.Connection.ConnectionString;
                    }
                }
            }
            source.ShowDialog();
            if (source.connected)//.mtd4)
            {
                try
                {
                    if (dataAdapter == null)
                    {
                        dataAdapter = new OleDbDataAdapter();
                    }
                    if (dataAdapter.SelectCommand == null)
                    {
                        dataAdapter.SelectCommand = new OleDbCommand(source.SqlString);
                    }
                    else
                    {
                        dataAdapter.SelectCommand.CommandText = source.SqlString;
                    }
                    if (dataAdapter.SelectCommand.Connection == null)
                    {
                        dataAdapter.SelectCommand.Connection = new OleDbConnection(source.ConnectionString);
                    }
                    else
                    {
                        dataAdapter.SelectCommand.Connection.ConnectionString = source.ConnectionString;
                    }
                    this.Report.DataAdapter = dataAdapter;
                }
                catch (Exception exception)
                {
                    this.Report.DataAdapter = null;
                    this.Report.DataSource = null;
                    MessageBox.Show(exception.Message);
                }
                finally
                {
                    if ((dataAdapter.SelectCommand != null) && (dataAdapter.SelectCommand.Connection != null))
                    {
                        dataAdapter.SelectCommand.Connection.Close();
                    }
                }
            }
            this.var27();
        }

        private void var17(object var31, EventArgs e)
        {
            if (this._var11.SelectionCount > 0)
            {
                object[] array = new object[this._var11.SelectionCount];
                this._var11.GetSelectedComponents().CopyTo(array, 0);
                this.propertyExplorer.mtd585.SelectedObjects = array;
            }
            if (this.SelectionChanged != null)
            {
                this.SelectionChanged(this, EventArgs.Empty);
            }
        }

        private void var18(object var31, LayoutChangedArgs e)
        {
            if (((e.Type == LayoutChangedType.ControlAdd) || (e.Type == LayoutChangedType.ControlDelete)) || (((e.Type == LayoutChangedType.SectionAdd) || (e.Type == LayoutChangedType.SectionDelete)) || (e.Type == LayoutChangedType.GroupReOrder)))
            {
                this.var25();
            }
            else if (e.Type == LayoutChangedType.ReportLoad)
            {
                this.var25();
                this.var27();
            }
            if (this.LayoutChanged != null)
            {
                this.LayoutChanged(this, e);
            }
        }

        private void var2()
        {
            this.mtd611 = new Panel();
            this.mtd612 = new Panel();
            this.mtd613 = new Panel();
            this.mtd614 = new Splitter();
            this.mtd615 = new Splitter();
            this._var19 = mtd73.GetImageList(Assembly.GetAssembly(typeof(MControl.Printing.View.Design.Report)), "MControl.Printing.View.Resources.ImagesDesigner.bmp", new Size(0x10, 0x10), new Point(0, 0));
            this._var20 = mtd73.GetImageList(Assembly.GetAssembly(typeof(MControl.Printing.View.Design.Report)), "MControl.Printing.View.Resources.ImagesCaptionIDE.bmp", new Size(12, 11), new Point(0, 0));
            this.reportExplorer = new ReportExplorer(this._var19, this._var20);
            this.propertyExplorer = new PropertyExplorer(this._var20);
            this.mtd612.SuspendLayout();
            this.reportExplorer.SuspendLayout();
            this.propertyExplorer.SuspendLayout();
            base.SuspendLayout();
            this._var13.SuspendLayout();
            this.mtd610 = this._var12.mtd425;
            this._mtd616 = (Control) this._var10.GetView(ViewTechnology.Default);//.WindowsForms);
            this._mtd616.Dock = DockStyle.Fill;
            this._var13.mtd609 = this.mtd610;
            this.mtd611.Dock = DockStyle.Fill;
            this.mtd612.BackColor = SystemColors.Control;
            this.mtd612.Dock = DockStyle.Fill;
            this.mtd612.ForeColor = SystemColors.ControlLight;
            this.mtd612.Location = new Point(0x20, 0);
            this.mtd612.Name = "pnlDesigner";
            this.mtd612.Size = new Size(780, 0x264);
            this.mtd612.Controls.AddRange(new Control[] { this.mtd611, this.mtd615, this.mtd613 });
            this.propertyExplorer.Dock = DockStyle.Fill;
            this.propertyExplorer.mtd583 += new CaptionDownHandler(this.var21);
            this.propertyExplorer.mtd585.Site = new mtd605(this._var12, this.propertyExplorer.mtd585, "PropertyGrid1");
            this.reportExplorer.Size = new Size(200, 200);
            this.reportExplorer.Dock = DockStyle.Top;
            this.reportExplorer.mtd583 += new CaptionDownHandler(this.var22);
            this.reportExplorer.mtd591 += new TreeViewEventHandler(this.var23);
            this.reportExplorer.mtd593 += new ItemDragEventHandler(this.var24);
            this.reportExplorer.mtd583 += new CaptionDownHandler(this.var22);
            this.mtd614.Dock = DockStyle.Top;
            this.mtd614.MinExtra = 60;
            this.mtd614.MinSize = 60;
            this.mtd613.Width = 200;
            this.mtd613.Dock = DockStyle.Right;
            this.mtd613.Controls.AddRange(new Control[] { this.propertyExplorer, this.mtd614, this.reportExplorer });
            this.mtd615.Dock = DockStyle.Right;
            this.mtd615.MinExtra = 0x73;
            this.mtd615.MinSize = 0x73;
            this.mtd611.Dock = DockStyle.Fill;
            this.mtd611.Controls.Add(this._mtd616);
            base.Controls.AddRange(new Control[] { this.mtd612, this._var13 });
            this.var25();
            this._var13.ResumeLayout(false);
            this.propertyExplorer.ResumeLayout(false);
            this.reportExplorer.ResumeLayout(false);
            this.mtd612.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void var21(CaptionButton var34)
        {
            if (var34 == CaptionButton.Maximize)
            {
                this.mtd614.SplitPosition = 200;
                this.mtd614.Enabled = true;
            }
            else
            {
                if (this.reportExplorer.mtd586 == CaptionButton.Minimize)
                {
                    this.reportExplorer.mtd587();
                }
                int minExtra = this.mtd614.MinExtra;
                this.mtd614.MinExtra = 0;
                if (var34 == CaptionButton.Minimize)
                {
                    this.mtd614.SplitPosition = (this.mtd613.Height - 0x12) - this.mtd614.Height;
                }
                else if (var34 == CaptionButton.Close)
                {
                    this.mtd614.SplitPosition = this.mtd613.Height - this.mtd614.Height;
                    if (this.reportExplorer.Visible)
                    {
                        this.reportExplorer.mtd588();
                    }
                    else
                    {
                        this.mtd613.Hide();
                    }
                }
                this.mtd614.MinExtra = minExtra;
                this.mtd614.Enabled = false;
            }
        }

        private void var22(CaptionButton var34)
        {
            if (var34 == CaptionButton.Maximize)
            {
                this.mtd614.Enabled = true;
                this.mtd614.SplitPosition = 200;
            }
            else if (var34 == CaptionButton.Minimize)
            {
                this.mtd614.Enabled = false;
                if (this.propertyExplorer.mtd586 == CaptionButton.Minimize)
                {
                    this.propertyExplorer.mtd587();
                }
            }
            else if (var34 == CaptionButton.Close)
            {
                this.mtd614.Enabled = false;
                if (this.propertyExplorer.Visible)
                {
                    if (this.propertyExplorer.mtd586 == CaptionButton.Minimize)
                    {
                        this.propertyExplorer.mtd587();
                    }
                    this.propertyExplorer.mtd588();
                }
                else
                {
                    this.mtd613.Hide();
                }
            }
        }

        private void var23(object var31, TreeViewEventArgs e)
        {
            if (e.Node.Parent == null)
            {
                this.mtd610.mtd430.mtd502(new object[] { this.Report }, SelectionTypes.Replace);
            }
            else if (string.Compare(e.Node.Parent.Text, "MainReport", true) == 0)
            {
                this.mtd610.mtd430.mtd465(e.Node.Text);
            }
            else
            {
                this.mtd610.mtd430.mtd466(e.Node.Parent.Text, e.Node.Text);
            }
        }

        private void var24(object var31, ItemDragEventArgs e)
        {
            this.reportExplorer.mtd594.DoDragDrop("DataField." + ((TreeNode) e.Item).Text, DragDropEffects.Move | DragDropEffects.Copy | DragDropEffects.Scroll);
        }

        private void var25()
        {
            if (this.reportExplorer != null)
            {
                this.reportExplorer.mtd595.BeginUpdate();
                this.reportExplorer.mtd595.Nodes.Clear();
                this.reportExplorer.mtd595.Nodes.Add(new TreeNode("MainReport", 15, 15));
                MControl.Printing.View.Design.Report rootComponent = (MControl.Printing.View.Design.Report) this._var12.RootComponent;
                for (int i = 0; i < rootComponent.Sections.Count; i++)
                {
                    Section section = rootComponent.Sections[i];
                    this.reportExplorer.mtd595.Nodes[0].Nodes.Add(new TreeNode(section.Name, 11, 11));
                    for (int j = 0; j < section.Controls.Count; j++)
                    {
                        this.reportExplorer.mtd595.Nodes[0].Nodes[i].Nodes.Add(new TreeNode(section.Controls[j].Name, 12, 12));
                    }
                }
                this.reportExplorer.mtd595.Nodes[0].Expand();
                this.reportExplorer.mtd595.EndUpdate();
            }
        }

        private void var26()
        {
            this.var3();
            this._var6 = "";
            this.mtd610.mtd498();
            this.var27();
            this.var25();
            this.var28();
            this.var4();
        }

        private void var27()
        {
            MControl.Printing.View.Design.Report report = this.Report;
            try
            {
                if (report.DataSource != null)
                {
                    if (report.DataSource is DataSet)
                    {
                        DataSet dataSource = (DataSet) report.DataSource;
                        if (dataSource.Tables.Count > 0)
                        {
                            this.var27(dataSource.Tables[0].Columns);
                        }
                    }
                    else if (report.DataSource is DataTable)
                    {
                        this.var27(((DataTable) report.DataSource).Columns);
                    }
                    else if (report.DataSource is DataView)
                    {
                        DataView view = (DataView) report.DataSource;
                        this.var27(view.Table.Columns);
                    }
                    else if (report.DataSource is IDataReader)
                    {
                        this.var27((IDataReader) report.DataSource);
                    }
                    else if (report.DataSource is IListDataSource)
                    {
                        this.var27(((IListDataSource) report.DataSource).DataFieldSchemaList);
                    }
                    else if (report.DataSource is XmlDataSource)
                    {
                        this.var27(((XmlDataSource) report.DataSource).DataFieldSchemaList);
                    }
                    else if (report.DataSource is ExternalDataSource)
                    {
                        this.var27(((ExternalDataSource) report.DataSource).DataFieldSchemaList);
                    }
                    else
                    {
                        this.reportExplorer.mtd594.Nodes.Clear();
                    }
                }
                else if (report.DataAdapter != null)
                {
                    IDataAdapter dataAdapter = report.DataAdapter;
                    DataSet dataSet = new DataSet();
                    dataAdapter.FillSchema(dataSet, SchemaType.Mapped);
                    if (dataSet.Tables.Count > 0)
                    {
                        this.var27(dataSet.Tables[0].Columns);
                    }
                }
                else
                {
                    this.reportExplorer.mtd594.Nodes.Clear();
                }
            }
            catch
            {
                this.reportExplorer.mtd594.Nodes.Clear();
            }
        }

        private void var27(DataFieldSchemaList fieldlist)
        {
            if ((fieldlist != null) && (fieldlist.Count > 0))
            {
                int count = fieldlist.Count;
                this.reportExplorer.mtd594.BeginUpdate();
                this.reportExplorer.mtd594.Nodes.Clear();
                this.reportExplorer.mtd594.Nodes.Add(new TreeNode("DataField", 13, 13));
                for (int i = 0; i < count; i++)
                {
                    this.reportExplorer.mtd594.Nodes[0].Nodes.Add(new TreeNode(fieldlist[i].DataFieldName, 14, 14));
                }
                this.reportExplorer.mtd594.Nodes[0].Expand();
                this.reportExplorer.mtd594.EndUpdate();
            }
        }

        private void var27(DataColumnCollection columns)
        {
            if ((columns != null) && (columns.Count > 0))
            {
                this.reportExplorer.mtd594.BeginUpdate();
                this.reportExplorer.mtd594.Nodes.Clear();
                this.reportExplorer.mtd594.Nodes.Add(new TreeNode("DataField", 13, 13));
                foreach (DataColumn column in columns)
                {
                    this.reportExplorer.mtd594.Nodes[0].Nodes.Add(new TreeNode(column.ColumnName, 14, 14));
                }
                this.reportExplorer.mtd594.Nodes[0].Expand();
                this.reportExplorer.mtd594.EndUpdate();
            }
        }

        private void var27(IDataReader reader)
        {
            if ((reader != null) && (reader.FieldCount > 0))
            {
                int fieldCount = reader.FieldCount;
                this.reportExplorer.mtd594.BeginUpdate();
                this.reportExplorer.mtd594.Nodes.Clear();
                this.reportExplorer.mtd594.Nodes.Add(new TreeNode("DataField", 13, 13));
                for (int i = 0; i < fieldCount; i++)
                {
                    this.reportExplorer.mtd594.Nodes[0].Nodes.Add(new TreeNode(reader.GetName(i), 14, 14));
                }
                this.reportExplorer.mtd594.Nodes[0].Expand();
                this.reportExplorer.mtd594.EndUpdate();
            }
        }

        private void var28()
        {
            base.Width++;
            base.Width--;
        }

        private void var29()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Report files (*.xml)|";
            dialog.FilterIndex = 0;
            dialog.RestoreDirectory = true;
            if (((dialog.ShowDialog() == DialogResult.OK) && dialog.CheckPathExists) && (dialog.FileName.Length > 0))
            {
                this._var6 = dialog.FileName;
                this._var12.mtd599(this._var6);
                this.var27();
                this.var25();
            }
            this.var28();
        }

        private void var3()
        {
            if (this.Report != null)
            {
                this.Report.mtd385 -= new EventHandler(this.var35);
            }
        }

        private void var30()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Report files (*.xml)|";
            dialog.FilterIndex = 0;
            dialog.RestoreDirectory = true;
            if (((dialog.ShowDialog() == DialogResult.OK) && dialog.CheckPathExists) && (dialog.FileName.Length > 0))
            {
                this._var6 = dialog.FileName;
                this.mtd610.mtd268.SaveLayout(this._var6);
            }
        }

        private void var32()
        {
            if (!this.reportExplorer.Visible)
            {
                if (!this.mtd613.Visible)
                {
                    this.mtd613.Show();
                }
                this.reportExplorer.mtd587();
                this.mtd614.Enabled = true;
                if (this.propertyExplorer.Visible)
                {
                    this.propertyExplorer.mtd589();
                    this.reportExplorer.mtd589();
                    this.mtd614.SplitPosition = 200;
                }
                else
                {
                    int minExtra = this.mtd614.MinExtra;
                    this.mtd614.MinExtra = 0;
                    this.mtd614.SplitPosition = this.mtd613.Height - this.mtd614.Height;
                    this.mtd614.MinExtra = minExtra;
                    this.mtd614.Enabled = false;
                }
            }
        }

        private void var33()
        {
            if (!this.propertyExplorer.Visible)
            {
                if (!this.mtd613.Visible)
                {
                    this.mtd613.Show();
                }
                if (this.reportExplorer.Visible)
                {
                    this.reportExplorer.mtd589();
                    this.propertyExplorer.mtd589();
                }
                this.propertyExplorer.mtd587();
                this.mtd614.Enabled = true;
                this.mtd614.SplitPosition = 200;
            }
        }

        private void var35(object var31, EventArgs e)
        {
            if (var31 != null)
            {
                this.var27();
            }
        }

        private void var4()
        {
            if (this.Report != null)
            {
                this.Report.mtd385 += new EventHandler(this.var35);
            }
        }

        private void var5(object sender, EventArgs e)
        {
        }

        public bool ClipBoardStatus
        {
            get
            {
                IDataObject dataObject = Clipboard.GetDataObject();
                return ((dataObject != null) && dataObject.GetDataPresent("Mc_CONTROLCOMPONENTS"));
            }
        }

        public MControl.Printing.View.Design.Report Report
        {
            get
            {
                return this.mtd610.mtd268;
            }
        }

        public int SelectedControlCount
        {
            get
            {
                return this.mtd610.mtd430.mtd431;
            }
        }
    }
}

