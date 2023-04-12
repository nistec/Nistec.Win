using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Nistec.Printing.Sections;
using Nistec.Printing.View;
using Nistec.Win;

namespace Nistec.Printing.UI
{
    [DesignTimeVisible(false), ToolboxItem(false)]
    public partial class McPrint : UserControl
    {
        public McPrint()
        {
            InitializeComponent();
        }

        public McPrint(DataTable dt)
        {
            InitializeComponent();
            CreateDocument(dt);
            source = dt;
        }
        public McPrint(McPrintDocument doc)
        {
            InitializeComponent();
            document = doc;
            this.pageSetupDialog1.Document = this.document;
            Display();
        }
        private DataTable source;
        private McPrintDocument document;

        public McPrintDocument Document
        {
            get 
            {
                return document; 
            }
            set 
            {
                if (document != value)
                {
                    document = value;
                    this.pageSetupDialog1.Document = this.document;
                    Display();
                }
            }
        }

        private McPrintDocument GetDocument()
        {
            if (source != null)
            {
                CreateDocument(source);
            }
            return document;
        }

        public void CreateDocument(DataTable dt)
        {
            source = dt;
            this.txtHeader.Text = dt.TableName;
            document = new McPrintDocument();
            document.DocumentName = this.txtHeader.Text;
            ReportBuilder rb = new ReportBuilder(document);
            rb.CreateDataDocument(dt.DefaultView);
            //rb.CreateHeaderAndFooter(this.txtHeader.Text);
            this.pageSetupDialog1.Document = this.document;
            Display();
        }
        private void DocumentSettings()
        {
            if (this.chkShowHeader.Checked)
            {
                document.AddPageHeader(this.txtHeader.Text, HorizontalAlignment.Center);
            }
            if (this.chkShowFooter.Checked)
            {
                document.AddPageFooter(null,true,true,HorizontalAlignment.Center);
            }
            if (this.chkHeaderStyle.Checked)
            {
                document.SetTableHeaderStyle(Brushes.Navy, Brushes.White, Brushes.Transparent);
            }
        }

        private string GetFilter()
        {
           return "All Files (*.*)|*.*";
        }

  
        private void Display()
        {
            bool hasData = this.document != null;
            this.btnOk.Enabled = hasData ;
            btnPreview.Enabled = hasData;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                DocumentSettings();
                document.Print();
                Close();
            }
            catch (Exception ex)
            {
                MsgBox.ShowError(ex.Message);
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                DocumentSettings();
                McPrintPreviewDialog dlg = new McPrintPreviewDialog();
                dlg.Document = Document;
                dlg.Show();
                Close();
            }
            catch (Exception ex)
            {
                MsgBox.ShowError(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void rbLandscape_CheckedChanged(object sender, EventArgs e)
        {
            this.document.DefaultPageSettings.Landscape = true;
        }

        private void rbPortrait_CheckedChanged(object sender, EventArgs e)
        {
            this.document.DefaultPageSettings.Landscape = false;
        }

        private void Close()
        {
            this.FindForm().Close();
        }

 
    }
}
