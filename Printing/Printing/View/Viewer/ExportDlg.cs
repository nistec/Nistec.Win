using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Resources;
using System.IO;


namespace Nistec.Printing.View.Viewer
{
    public class ExportDlg : System.Windows.Forms.Form//Nistec.WinForms.FormBase
	{

		#region NetFram

        //private void //NetReflectedFram()
        //{
        //    this.cbCancel.//NetReflectedFram("ba7fa38f0b671cbc");
        //    this.cbOK.//NetReflectedFram("ba7fa38f0b671cbc");
        //    this.ctlOpenAfter.//NetReflectedFram("ba7fa38f0b671cbc");
        //}

		#endregion


		// Fields
		internal bool _cls4;
		private ExportType var5;
		private PDFprop var6;
		private Htmlprop var7;
		private TIFprop var8;
		private CSVprop clsCsv;
		private Button cbCancel;
		private Button cbOK;
		private ComboBox cbxExpFormat;
		private Container components;
		private Label lblExpFormat;
		private PropertyGrid pGrid;
		private CheckBox ctlOpenAfter;
		private string tbFileName;

		public ExportDlg()
		{
			//netFramwork.//NetReflectedFram();
			this.components = null;
			this.InitializeComponent();
			//NetReflectedFram();
			this.Init();
		}

 
		protected override void Dispose(bool disposing)
		{
			if (disposing && (this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

 
		private void InitializeComponent()
		{
            this.lblExpFormat = new System.Windows.Forms.Label();
            this.cbxExpFormat = new System.Windows.Forms.ComboBox();
            this.pGrid = new System.Windows.Forms.PropertyGrid();
            this.cbOK = new System.Windows.Forms.Button();
            this.cbCancel = new System.Windows.Forms.Button();
            this.ctlOpenAfter = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblExpFormat
            // 
            this.lblExpFormat.Location = new System.Drawing.Point(16, 28);
            this.lblExpFormat.Name = "lblExpFormat";
            this.lblExpFormat.Size = new System.Drawing.Size(80, 24);
            this.lblExpFormat.TabIndex = 1;
            this.lblExpFormat.Text = "Export Format";
            // 
            // cbxExpFormat
            // 
            this.cbxExpFormat.Location = new System.Drawing.Point(104, 28);
            this.cbxExpFormat.Name = "cbxExpFormat";
            this.cbxExpFormat.Size = new System.Drawing.Size(264, 21);
            this.cbxExpFormat.TabIndex = 3;
            this.cbxExpFormat.SelectedIndexChanged += new System.EventHandler(this.cbxExpFormat_SelectedIndexChanged);
            // 
            // pGrid
            // 
            this.pGrid.CommandsBackColor = System.Drawing.Color.WhiteSmoke;
            this.pGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.pGrid.Location = new System.Drawing.Point(16, 60);
            this.pGrid.Name = "pGrid";
            this.pGrid.Size = new System.Drawing.Size(352, 152);
            this.pGrid.TabIndex = 5;
            this.pGrid.ToolbarVisible = false;
            // 
            // cbOK
            // 
            this.cbOK.Location = new System.Drawing.Point(200, 240);
            this.cbOK.Name = "cbOK";
            this.cbOK.Size = new System.Drawing.Size(80, 24);
            this.cbOK.TabIndex = 6;
            this.cbOK.Text = "OK";
            this.cbOK.Click += new System.EventHandler(this.cbOK_Click);
            // 
            // cbCancel
            // 
            this.cbCancel.Location = new System.Drawing.Point(288, 240);
            this.cbCancel.Name = "cbCancel";
            this.cbCancel.Size = new System.Drawing.Size(80, 24);
            this.cbCancel.TabIndex = 7;
            this.cbCancel.Text = "Cancel";
            this.cbCancel.Click += new System.EventHandler(this.cbCancel_Click);
            // 
            // ctlOpenAfter
            // 
            this.ctlOpenAfter.BackColor = System.Drawing.Color.Transparent;
            this.ctlOpenAfter.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlOpenAfter.Location = new System.Drawing.Point(16, 240);
            this.ctlOpenAfter.Name = "ctlOpenAfter";
            this.ctlOpenAfter.Size = new System.Drawing.Size(136, 21);
            this.ctlOpenAfter.TabIndex = 8;
            this.ctlOpenAfter.Text = "Open After Saving";
            this.ctlOpenAfter.UseVisualStyleBackColor = false;
            // 
            // ExportDlg
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(386, 271);
            this.Controls.Add(this.ctlOpenAfter);
            this.Controls.Add(this.cbCancel);
            this.Controls.Add(this.cbOK);
            this.Controls.Add(this.pGrid);
            this.Controls.Add(this.cbxExpFormat);
            this.Controls.Add(this.lblExpFormat);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExportDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Export Data";
            this.ResumeLayout(false);

		}

 
		private void Init()
		{
			this.var6 = new PDFprop();
			this.var7 = new Htmlprop();
			this.var8 = new TIFprop();
			this.clsCsv=new CSVprop();
			this.cbxExpFormat.Items.Add("Portable Document Format (PDF)");
			this.cbxExpFormat.Items.Add("HTML Format (HTM)");
			this.cbxExpFormat.Items.Add("Image Format");
            this.cbxExpFormat.Items.Add("Excel Format (xls)");
            this.cbxExpFormat.Items.Add("Text Format (Csv)");
			this.cbxExpFormat.SelectedIndex = 0;
			this._cls4 = false;
		}

 
		private void cbxExpFormat_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (this.cbxExpFormat.SelectedIndex)
			{
				case 0://Pdf
				{
					this.var5 = ExportType.Pdf;
					this.var6.cls111();
					this.pGrid.SelectedObject = this.var6;
					return;
				}
				case 1://Html
				{
					this.var5 = ExportType.Html;
					this.var7.cls111();
					this.pGrid.SelectedObject = this.var7;
					return;
				}
				case 2://Image
				{
					this.var5 = ExportType.Image;
					this.var8.SetDefault();
					this.pGrid.SelectedObject = this.var8;
					return;
				}
                case 3://Excel
                {
                    this.var5 = ExportType.Excel;
                    this.clsCsv.SetDefault();
                    this.pGrid.SelectedObject = this.clsCsv;
                    return;
                }
                case 4://Csv
                {
                    this.var5 = ExportType.Csv;
                    this.clsCsv.SetDefault();
                    this.pGrid.SelectedObject = this.clsCsv;
                    return;
                }
			}
			this.pGrid.SelectedObject = null;
		}

        private bool var10(string var11)
        {
            var11 = var11.ToLower();
            if (this.var5 == ExportType.Pdf)
            {
                if (string.Compare(var11, ".pdf") == 0)
                {
                    return true;
                }
                MessageBox.Show("Enter Valid File Extention (.pdf)", "Export Report");
                return false;
            }
            if (this.var5 == ExportType.Html)
            {
                if (string.Compare(var11, ".html") == 0)
                {
                    return true;
                }
                if (string.Compare(var11, ".htm") == 0)
                {
                    return true;
                }
                MessageBox.Show("Enter Valid File Extention (.html OR .htm)", "Export Report");
                return false;
            }
            if (this.var5 == ExportType.Image)
            {
                if (this.var8.Format == ImageType.Bmp)
                {
                    if (string.Compare(var11, ".bmp") == 0)
                    {
                        return true;
                    }
                    MessageBox.Show("Enter Valid File Extention (.bmp) for ImageFormat.Bmp", "Export Report");
                }
                else if (this.var8.Format == ImageType.Gif)
                {
                    if (string.Compare(var11, ".gif") == 0)
                    {
                        return true;
                    }
                    MessageBox.Show("Enter Valid File Extention (.gif) for ImageFormat.Gif", "Export Report");
                }
                else if (this.var8.Format == ImageType.Jpeg)
                {
                    if (string.Compare(var11, ".jpg") == 0)
                    {
                        return true;
                    }
                    MessageBox.Show("Enter Valid File Extention (.jpg) for ImageFormat.Jpeg", "Export Report");
                }
                else if (this.var8.Format == ImageType.Png)
                {
                    if (string.Compare(var11, ".png") == 0)
                    {
                        return true;
                    }
                    MessageBox.Show("Enter Valid File Extention (.png) for ImageFormat.Png", "Export Report");
                }
                else if (this.var8.Format == ImageType.Tiff)
                {
                    if (string.Compare(var11, ".tif") == 0)
                    {
                        return true;
                    }
                    MessageBox.Show("Enter Valid File Extention (.tif) for ImageFormat.Tiff", "Export Report");
                }
                else if (this.var8.Format == ImageType.Emf)
                {
                    if (string.Compare(var11, ".emf") == 0)
                    {
                        return true;
                    }
                    MessageBox.Show("Enter Valid File Extention (.emf) for ImageFormat.Emf", "Export Report");
                }
                else if (this.var8.Format == ImageType.Wmf)
                {
                    if (string.Compare(var11, ".wmf") == 0)
                    {
                        return true;
                    }
                    MessageBox.Show("Enter Valid File Extention (.wmf) for ImageFormat.Wmf", "Export Report");
                }
            }
            if (this.var5 == ExportType.Excel)
            {
                if (string.Compare(var11, ".xls") == 0)
                {
                    return true;
                }
                MessageBox.Show("Enter Valid File Extention (.xls) for FileFormat.xls", "Export Report");
            }
            if (this.var5 == ExportType.Csv)
            {
                if (string.Compare(var11, ".csv") == 0)
                {
                    return true;
                }
                MessageBox.Show("Enter Valid File Extention (.csv) for FileFormat.csv", "Export Report");
            }
            return false;
        }

 
		private void cbOK_Click(object sender, EventArgs e)
		{
			SetFileName();
			if (this.CheckDirectory())
			{
				this._cls4 = true;
				base.Close();
			}
		}

 
		private void cbCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

 
		//private void cbFileName_Click(object sender, EventArgs e)
		private void SetFileName()
		{
			tbFileName="";
			SaveFileDialog dialog1 = new SaveFileDialog();
			dialog1.Title = "Export Report Document";
			dialog1.AddExtension = true;
            switch (this.cbxExpFormat.SelectedIndex)
            {
                case 0:
                    {
                        dialog1.DefaultExt = "pdf";
                        dialog1.Filter = "PDF Files (*.pdf)|*.pdf";
                        break;
                    }
                case 1:
                    {
                        dialog1.DefaultExt = "html";
                        dialog1.Filter = "HTML Files (*.htm;*.html)|*.htm;*.htm";
                        break;
                    }
                case 2:
                    {
                        if (this.var8.Format != ImageType.Bmp)
                        {
                            if (this.var8.Format == ImageType.Gif)
                            {
                                dialog1.DefaultExt = "gif";
                                dialog1.Filter = "GIF Files (*.gif)|*.gif";
                            }
                            else if (this.var8.Format == ImageType.Jpeg)
                            {
                                dialog1.DefaultExt = "jpg";
                                dialog1.Filter = "JPEG Files (*.jpg)|*.jpg";
                            }
                            else if (this.var8.Format == ImageType.Png)
                            {
                                dialog1.DefaultExt = "png";
                                dialog1.Filter = "PNG Files (*.png)|*.png";
                            }
                            else if (this.var8.Format == ImageType.Tiff)
                            {
                                dialog1.DefaultExt = "tif";
                                dialog1.Filter = "TIFF Files (*.tif)|*.tif";
                            }
                            else if (this.var8.Format == ImageType.Emf)
                            {
                                dialog1.DefaultExt = "emf";
                                dialog1.Filter = "EMF Files (*.emf)|*.emf";
                            }
                            else if (this.var8.Format == ImageType.Wmf)
                            {
                                dialog1.DefaultExt = "wmf";
                                dialog1.Filter = "WMF Files (*.wmf)|*.wmf";
                            }
                            break;
                        }
                        dialog1.DefaultExt = "bmp";
                        dialog1.Filter = "Bitmap Files (*.bmp)|*.bmp";
                        break;
                    }
                case 3:
                    {
                        dialog1.DefaultExt = "xls";
                        dialog1.Filter = "Excel Files (*.xls)|*.xls";
                        break;
                    }
                case 4:
                    {
                        dialog1.DefaultExt = "csv";
                        dialog1.Filter = "Scv Files (*.csv)|*.csv";
                        break;
                    }
            }
			if (dialog1.ShowDialog() == DialogResult.OK)
			{
				this.tbFileName = dialog1.FileName;
			}
		}

 
		private bool CheckDirectory()//var9()
		{
			try
			{
				if ((this.filePath != null) && (this.filePath.Length > 0))
				{
					FileInfo info1 = new FileInfo(this.filePath);
					if (Directory.Exists(info1.DirectoryName))
					{
						if (this.var10(Path.GetExtension(info1.Name)))
						{
							return true;
						}
					}
					else
					{
						MessageBox.Show("Directory does not exist", "Export Report");
					}
				}
				else
				{
					MessageBox.Show("Directory does not exist", "Export Report");
				}
			}
			catch
			{
			}
			return false;
		}

 
		internal ExportType ExportType
		{
			get
			{
				return this.var5;
			}
		}
 
		internal TIFprop TIFprop
		{
			get
			{
				return this.var8;
			}
		}
		internal PDFprop PDFprop
		{
			get
			{
				return this.var6;
			}
		}
 
		internal Htmlprop Htmlprop
		{
			get
			{
				return this.var7;
			}
		}

        internal CSVprop Csvprop
        {
            get
            {
                return this.clsCsv;
            }
        }

		internal string filePath//cls1086
		{
			get
			{
				return this.tbFileName;
			}
		}
 
		internal bool openAfter
		{
			get
			{
				return this.ctlOpenAfter.Checked;
			}
		}

		internal bool CanView// cls4
		{
			get
			{
				return this._cls4;
			}
		}
 
	}

}
