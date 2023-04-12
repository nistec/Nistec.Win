using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.CompilerServices;

namespace MControl.Ado.Csv
{
    public class CsvHelper
    {
 

            private string ConvertFieldForCsv(string strField)
            {
                string str = strField;
                if (str == "")
                {
                    return (Convert.ToString('"') + Convert.ToString('"'));
                }
                str = str.Trim();
                string oldValue = Convert.ToString('"');
                string newValue = Convert.ToString('"') + Convert.ToString('"');
                str = str.Replace(oldValue, newValue);
                bool flag = false;
                foreach (char ch in str)
                {
                    switch (ch)
                    {
                        case ',':
                        case '\n':
                        case '"':
                            flag = true;
                            goto Label_0090;
                    }
                }
            Label_0090:
                if (flag)
                {
                    str = '"' + str + '"';
                }
                return str;
            }

            public void ExportToCsv()
            {
                if (this.m_DataGridView == null)
                {
                    throw new Exception("DataGridView property is null");
                }
                this.SetLicensedFunctionality();
                string filePath = "";
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Title = "Save As";
                dialog.Filter = "Comma separated values file (*.csv)|*.csv";
                dialog.FilterIndex = 1;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = dialog.FileName;
                    this.ToCsv(filePath);
                }
                dialog.Dispose();
            }

            public void ExportToCsv(string FilePath)
            {
                if (this.m_DataGridView == null)
                {
                    throw new Exception("DataGridView property is null");
                }
                this.SetLicensedFunctionality();
                this.ToCsv(FilePath);
            }

            private void ToCsv(string FilePath)
            {
                int count = this.m_DataGridView.Rows.Count;
                if (this.m_DataGridView.AllowUserToAddRows)
                {
                    count--;
                }
                if (!this.Licence.Licensed)
                {
                    count /= 4;
                }
                int num2 = this.m_DataGridView.Columns.Count;
                StreamWriter writer = new StreamWriter(FilePath);
                if (this.m_IncludeColumnNames)
                {
                    string strField = "";
                    for (int j = 0; j < num2; j++)
                    {
                        strField = this.m_DataGridView.Columns[j].HeaderText;
                        strField = this.ConvertFieldForCsv(strField);
                        writer.Write(strField);
                        if (j < (num2 - 1))
                        {
                            writer.Write(",");
                        }
                    }
                    writer.WriteLine();
                }
                for (int i = 0; i < count; i++)
                {
                    for (int k = 0; k < num2; k++)
                    {
                        string str = this.m_DataGridView[k, i].FormattedValue.ToString();
                        str = this.ConvertFieldForCsv(str);
                        writer.Write(str);
                        if (k < (num2 - 1))
                        {
                            writer.Write(",");
                        }
                    }
                    if (i < (count - 1))
                    {
                        writer.WriteLine();
                    }
                }
                writer.Close();
                if (this.m_OpenAfterExport)
                {
                    Process.Start(FilePath);
                }
                if (this.ExportCompleted != null)
                {
                    this.ExportCompleted(this, new EventArgs());
                }
            }

 
            [DefaultValue(false), Description("Specifies whether column names will be included in the export file. Applies to csv and html only."), Category("Behavior")]
            public bool IncludeColumnNames
            {
                get
                {
                    return this.m_IncludeColumnNames;
                }
                set
                {
                    this.m_IncludeColumnNames = value;
                }
            }

            [DefaultValue(true), Description("Specifies whether the export file will be opened after the export is completed."), Category("Behavior")]
            public bool OpenAfterExport
            {
                get
                {
                    return this.m_OpenAfterExport;
                }
                set
                {
                    this.m_OpenAfterExport = value;
                }
            }
        }
    }



