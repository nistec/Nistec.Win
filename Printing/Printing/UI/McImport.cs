using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Nistec.Printing.Data;

namespace Nistec.Printing.UI
{
    [DesignTimeVisible(false), ToolboxItem(false)]
    public partial class McImport : UserControl
    {
        public McImport()
        {
            InitializeComponent();
            Init();
            Display();
        }


        private ImportType currentType;
        private DataTable source;

        public DataTable Source
        {
            get { return source; }
        }

        private void Init()
        {
            this.cboWriterType.Items.AddRange(Enum.GetNames(typeof( ImportType)));

        }

        private string GetFilter()
        {
            switch (currentType)
            {
                case ImportType.Csv:
                    return "Text Files (*.txt;*.csv;*.tab;*.log)|*.txt;*.csv;*.tab;*.log|All Files (*.*)|*.*";
                case ImportType.Excel:
                case ImportType.ExcelXml:
                    return "MS Excel Workbook (*.xls)|*.xls";
                case ImportType.OleDb:
                    return "MS Excel Workbook (*.xls;*.xlsx;*.xlsb)|*.xls;*.xlsx;*.xlsb";
                case ImportType.Xml:
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
            this.btnOk.Enabled = hasDest;
            btnPreview.Enabled = hasData && hasDest;
            butBrowse.Enabled = this.cboWriterType.SelectedIndex > -1;
        }

        private void butBrowse_Click(object sender, EventArgs e)
        {
            this.ofdBrowse.Filter = GetFilter();// "MS Excel Workbook (*.xls)|*.xls";
            if (this.ofdBrowse.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    this.txtWorkbook.Text = this.ofdBrowse.FileName;
                }
                catch (Exception exception2)
                {
                    MessageBox.Show(this, "Failed to open new Workbook: " + exception2.Message, "Open file", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            this.txtWorkbook.Focus();
            Display();
        }

        private void cboWriterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboWriterType.Text.Length > 0)
            {
                currentType = (ImportType)Enum.Parse(typeof(ImportType), this.cboWriterType.Text, true);
            }
            Display();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                this.FindForm().DialogResult = DialogResult.No;
                this.source = null;
                this.source = AdoImport.Import(currentType, this.txtWorkbook.Text, this.chkRowHeader.Checked);
                Display();
                if (this.source != null)
                {
                    this.FindForm().DialogResult = DialogResult.OK;
                    MessageBox.Show("Records successfully imported from the source file", "Import");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Import", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(ex.Message, "Import", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }
  

    }
}
