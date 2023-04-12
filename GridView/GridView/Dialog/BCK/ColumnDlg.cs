using System;
using System.Windows.Forms ;
using System.Data;
using mControl;

namespace mControl.GridView 
{
    
    
	public class ColumnDlg : mControl.WinCtl.Forms.FormBase 
	{
        
		// Required by the Windows Form Designer
		//private System.ComponentModel.IContainer components;
        
		// NOTE: The following procedure is required by the Windows Form Designer
		// It can be modified using the Windows Form Designer.  
		// Do not modify it using the code editor.
		internal System.Windows.Forms.GroupBox CtlGroupBox1;
		internal System.Windows.Forms.Label Label2;
		protected internal System.Windows.Forms.ComboBox comboBox1;
		protected internal System.Windows.Forms.TextBox MappingName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		internal System.Windows.Forms.Button cmdCancel;
		protected internal System.Windows.Forms.NumericUpDown colWidth;
		protected internal System.Windows.Forms.TextBox ColumnName;
		internal System.Windows.Forms.Button cmdOK;
        
		public ColumnDlg() 
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
			// Add any initialization after the InitializeComponent() call
		}
        
		// Form overrides dispose to clean up the component list.
		protected override void Dispose(bool disposing) 
		{
			/*if (disposing) 
			{
				if (!(components == null)) 
				{
					components.Dispose();
				}
			}*/
			base.Dispose(disposing);
		}
        
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent() 
		{
			this.CtlGroupBox1 = new System.Windows.Forms.GroupBox();
			this.colWidth = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.ColumnName = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.MappingName = new System.Windows.Forms.TextBox();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.Label2 = new System.Windows.Forms.Label();
			this.cmdOK = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.CtlGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.colWidth)).BeginInit();
			this.SuspendLayout();
			// 
			// CtlGroupBox1
			// 
			this.CtlGroupBox1.Controls.Add(this.colWidth);
			this.CtlGroupBox1.Controls.Add(this.label4);
			this.CtlGroupBox1.Controls.Add(this.ColumnName);
			this.CtlGroupBox1.Controls.Add(this.label3);
			this.CtlGroupBox1.Controls.Add(this.label1);
			this.CtlGroupBox1.Controls.Add(this.MappingName);
			this.CtlGroupBox1.Controls.Add(this.comboBox1);
			this.CtlGroupBox1.Controls.Add(this.Label2);
			this.CtlGroupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.CtlGroupBox1.Location = new System.Drawing.Point(16, 8);
			this.CtlGroupBox1.Name = "CtlGroupBox1";
			this.CtlGroupBox1.Size = new System.Drawing.Size(296, 152);
			this.CtlGroupBox1.TabIndex = 3;
			this.CtlGroupBox1.TabStop = false;
			this.CtlGroupBox1.Text = "Column Style";
			// 
			// colWidth
			// 
			this.colWidth.Location = new System.Drawing.Point(88, 120);
			this.colWidth.Name = "colWidth";
			this.colWidth.Size = new System.Drawing.Size(88, 20);
			this.colWidth.TabIndex = 16;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 120);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(80, 24);
			this.label4.TabIndex = 15;
			this.label4.Text = "Width";
			// 
			// ColumnName
			// 
			this.ColumnName.Location = new System.Drawing.Point(88, 88);
			this.ColumnName.Name = "ColumnName";
			this.ColumnName.Size = new System.Drawing.Size(200, 20);
			this.ColumnName.TabIndex = 14;
			this.ColumnName.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 88);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(80, 24);
			this.label3.TabIndex = 13;
			this.label3.Text = "Header Text";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 56);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 24);
			this.label1.TabIndex = 12;
			this.label1.Text = "MappingName";
			// 
			// MappingName
			// 
			this.MappingName.Location = new System.Drawing.Point(88, 56);
			this.MappingName.Name = "MappingName";
			this.MappingName.Size = new System.Drawing.Size(200, 20);
			this.MappingName.TabIndex = 11;
			this.MappingName.Text = "";
			// 
			// comboBox1
			// 
			this.comboBox1.Items.AddRange(new object[] {
														   "TextColumn",
														   "ComboColumn",
														   "DateTimeColumn",
														   "LabelColumn",
														   "LinkColumn",
														   "ButtonColumn",
														   "ProgressColumn",
														   "BoolColumn",
														   "IconColumn",
														   "MultiColumn",
														   "EnumColumn",
														   "None"});
			this.comboBox1.Location = new System.Drawing.Point(88, 24);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(200, 21);
			this.comboBox1.TabIndex = 10;
			this.comboBox1.Text = "None";
			// 
			// Label2
			// 
			this.Label2.Location = new System.Drawing.Point(8, 24);
			this.Label2.Name = "Label2";
			this.Label2.Size = new System.Drawing.Size(72, 23);
			this.Label2.TabIndex = 9;
			this.Label2.Text = "Column Type";
			// 
			// cmdOK
			// 
			this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.cmdOK.Location = new System.Drawing.Point(240, 176);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.TabIndex = 10;
			this.cmdOK.Text = "OK";
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.Location = new System.Drawing.Point(160, 176);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.TabIndex = 11;
			this.cmdCancel.Text = "Cancel";
			// 
			// ColumnDlg
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(322, 207);
          	this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.CtlGroupBox1);
			this.Controls.Add(this.cmdOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ColumnDlg";
			this.Text = "Columns Style";
			this.TopMost = true;
			this.CtlGroupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.colWidth)).EndInit();
			this.ResumeLayout(false);

		}
        
		public static void Open() 
		{
			ColumnDlg f = new ColumnDlg();
			f.ShowDialog();
		}

        public static void Open(GridColumnType type, string mappName, string headerText, int width) 
		{
			ColumnDlg f = new ColumnDlg();
			f.SetColumnTypes (type);
			f.MappingName.Text=mappName;
			f.ColumnName.Text=headerText;
			f.colWidth.Value=(decimal)width;
			f.ShowDialog();
		}

		private void cmdOK_Click(object sender, System.EventArgs e) 
		{
			this.DialogResult =DialogResult.OK;
			this.Close();
		}


        private void SetColumnTypes(GridColumnType value)
		{
			//string val="None";
			int selIndex=0;
				switch(value)   
				{
                    case GridColumnType.TextColumn:
						selIndex=0;// "TextColumn";
						break;
                    case GridColumnType.ComboColumn:
						selIndex=0;// "ComboColumn";
						break;
                    case GridColumnType.DateTimeColumn:
						selIndex=0;// "DateTimeColumn" ;
						break;
                    case GridColumnType.LabelColumn:
						selIndex=0;// "LabelColumn" ;
						break;
//					case ColumnTypes.LinkColumn:
//						selIndex=0;// "LinkColumn"  ;
//						break;
                    case GridColumnType.ButtonColumn:
						selIndex=0;// "ButtonColumn" ;
						break;
                    case GridColumnType.ProgressColumn:
						selIndex=0;// "ProgressColumn" ;
						break;
                    case GridColumnType.BoolColumn:
						selIndex=0;// "BoolColumn"  ;
						break;
                    case GridColumnType.IconColumn:
						selIndex=0;// "IconColumn"  ;
						break;
                    case GridColumnType.MultiColumn:
						selIndex=0;// "MultiColumn"  ;
						break;
//					case ColumnTypes.EnumColumn:
//						selIndex=0;// "EnumColumn"  ;
//						break;
//					case ColumnTypes.MenuColumn:
//						selIndex=0;// "MenuColumn" ;
//						break;
                    case GridColumnType.GridColumn:
						selIndex=0;// "GridColumn"  ;
						break;
					default:
						selIndex=0;// "None";
						break;
				}
			this.comboBox1.SelectedIndex=selIndex;
		}


		public string Column
		{
		    get{return this.comboBox1.SelectedText ;}
		}

        public GridColumnType ColumnStyle
		{
			get
			{
			
				switch(this.comboBox1.SelectedItem.ToString ())   
				{
					case "TextColumn":
                        return GridColumnType.TextColumn;
					case "ComboColumn":
                        return GridColumnType.ComboColumn;
					case "DateTimeColumn":
                        return GridColumnType.DateTimeColumn;
					case "LabelColumn":
                        return GridColumnType.LabelColumn;
//					case "LinkColumn":
//						return  ColumnTypes.LinkColumn ;
					case "ButtonColumn":
                        return GridColumnType.ButtonColumn;
					case "ProgressColumn":
                        return GridColumnType.ProgressColumn;
					case "BoolColumn":
                        return GridColumnType.BoolColumn;
					case "IconColumn":
                        return GridColumnType.IconColumn;
					case "MultiColumn":
                        return GridColumnType.MultiColumn;
//					case "EnumColumn":
//						return  ColumnTypes.EnumColumn ;
//					case "MenuColumn":
//						return  ColumnTypes.MenuColumn;
					case "GridColumn":
                        return GridColumnType.GridColumn;
					default:
                        return GridColumnType.None;
				}
			}
		}

		public string MappName
		{
			get{return this.MappingName.Text  ;}
		}

		public string HeaderText
		{
			get{return this.ColumnName.Text  ;}
		}

		public int ColWidth
		{
			get{return (int)this.colWidth.Value  ;}
		}
	}
}

