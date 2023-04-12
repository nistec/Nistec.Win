using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Nistec.Printing.Data;

namespace Nistec.Printing.UI
{
    [DesignTimeVisible(false), ToolboxItem(false)]
    public partial class McExport : UserControl
    {
        public McExport()
        {
            InitializeComponent();
            Init();
        }

        public McExport(DataTable dt)
        {
            InitializeComponent();
            Init();
            source = dt;
            Display();
        }

        private ExportType currentType;
        private DataTable source;

        public DataTable Source
        {
            get { return source; }
            set 
            { 
                source = value;
                Display();
            }
        }

        private void Init()
        {
            this.cboWriterType.Items.AddRange(Enum.GetNames(typeof( ExportType)));

        }

        private string GetFilter()
        {
            switch (currentType)
            {
                case ExportType.Csv:
                    return "Text Files (*.txt;*.csv;*.tab;*.log)|*.txt;*.csv;*.tab;*.log|All Files (*.*)|*.*";
                case ExportType.Excel:
                    return "MS Excel Workbook (*.xls)|*.xls";
                case ExportType.Html:
                    return "Html Page (*.htm)|*.htm";
                case ExportType.Pdf:
                    return "Pdf File(*.Pdf)|*.Pdf";
                case ExportType.Xml:
                    return "Xml File(*.xml)|*.xml";
                default:
                    return "All Files (*.*)|*.*";

            }
        }

        private string GetTableName()
        {
            string tableName = this.cboTable.Text;
            if (string.IsNullOrEmpty(tableName))
                tableName = "Table";
            return tableName;
        }

        private bool IsValid()
        {
            return this.source != null && this.txtWorkbook.Text.Length > 0;
        }

        private void Display()
        {
            bool hasData = this.source != null;
            bool hasDest = this.txtWorkbook.Text.Length > 0;
            this.btnOk.Enabled = hasData && hasDest;
            btnPreview.Enabled = hasDest;
            butBrowse.Enabled = this.cboWriterType.SelectedIndex > -1;
        }

        private void butBrowse_Click(object sender, EventArgs e)
        {
            this.sfdSaveAs.Filter = GetFilter();// "MS Excel Workbook (*.xls)|*.xls";
            if (this.sfdSaveAs.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    try
                    {
                        if (File.Exists(this.sfdSaveAs.FileName))
                        {
                            File.Delete(this.sfdSaveAs.FileName);
                        }
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(this, "Failed to overwrite existing Workbook: " + exception.Message, "Create Workbook", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    this.txtWorkbook.Text = this.sfdSaveAs.FileName;
                }
                catch (Exception exception2)
                {
                    MessageBox.Show(this, "Failed to create new Workbook: " + exception2.Message, "Create Workbook", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            this.txtWorkbook.Focus();
            Display();
        }

        private void cboWriterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboWriterType.Text.Length > 0)
            {
                currentType = (ExportType)Enum.Parse(typeof(ExportType), this.cboWriterType.Text, true);
            }
            Display();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                bool ok = AdoExport.Export(currentType, source, this.txtWorkbook.Text, this.chkRowHeader.Checked);
                if (!ok)
                    return;
                if (this.chkOpenAfter.Checked)
                {
                    Process myProcess = new Process();
                    myProcess.StartInfo.FileName = this.txtWorkbook.Text;
                    //myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                    myProcess.Start();
                }
                this.FindForm().DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Export", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                ReportBuilder.PrintDataView(source.DefaultView, GetTableName());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Export", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        private void Close()
        {
            this.FindForm().Close();
        }
    }
}
