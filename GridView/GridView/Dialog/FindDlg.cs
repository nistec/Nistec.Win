using System;
using System.Windows.Forms ;
using System.Data;
using Nistec;

using Nistec.WinForms;
using Nistec.Win;
using Extension.Nistec.Threading;
 
namespace Nistec.GridView 
{
    
    /// <summary>
    /// Grid Find dialog that can find values in grid rows
    /// </summary>
	public class FindDlg : Nistec.WinForms.FormBase
	{
        
		// Required by the Windows Form Designer
		//private System.ComponentModel.IContainer components;
        
		private const string STATUS_MESSAGE = "Number of records processed: ";
        private const string NO_RECORDS_FOUND_MESSAGE = "No records were found that match the find criteria.";
        private const string CAPTION_TITLE = "Nistec Finder";
        private const string NO_RECORDS_TO_SORT_MESSAGE = "There are no records to sort.";
        private const string UN_KNOWN_DATA_TYPE = "unKnown Data type";
        internal Nistec.WinForms.McLabel Label2;
        internal Nistec.WinForms.McLabel Label1;
		internal Nistec.WinForms.McComboBox cboFilelds;
        
		private System.Windows.Forms.ImageList imageList1;
		private System.ComponentModel.IContainer components;
		private Nistec.WinForms.McButton btnOk;
        private Nistec.WinForms.McButton btnCancel;
        internal Nistec.WinForms.McComboBox ctlFind;
        internal Nistec.WinForms.McCheckBox chkbMatchCase;
        internal Nistec.WinForms.McCheckBox ctlWholeWord;
		//private Nistec.WinCtl.Wizards.WizDialog wizDialog1;
		private Grid grid;
		//private object McParent;

        /// <summary>
        /// Find Dialog ctor
        /// </summary>
        public FindDlg() 
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
			// Add any initialization after the InitializeComponent() call
		}

        /// <summary>
        /// Find Dialog ctor
        /// </summary>
        /// <param name="g"></param>
        public FindDlg(Grid g) 
		{
			InitializeComponent();
            this.grid = g;
            base.SetStyleLayout(g.LayoutManager.Layout);
        }


        /// <summary>
        /// Form overrides dispose to clean up the component list.
        /// </summary>
        /// <param name="disposing"></param>
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindDlg));
            this.Label2 = new Nistec.WinForms.McLabel();
            this.Label1 = new Nistec.WinForms.McLabel();
            this.cboFilelds = new Nistec.WinForms.McComboBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnOk = new Nistec.WinForms.McButton();
            this.btnCancel = new Nistec.WinForms.McButton();
            this.ctlFind = new Nistec.WinForms.McComboBox();
            this.chkbMatchCase = new Nistec.WinForms.McCheckBox();
            this.ctlWholeWord = new Nistec.WinForms.McCheckBox();
            this.SuspendLayout();
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.Color.White;
            this.Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label2.ImageIndex = 0;
            this.Label2.Location = new System.Drawing.Point(16, 18);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(64, 20);
            this.Label2.StylePainter = this.StyleGuideBase;
            this.Label2.Text = "Column";
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.Color.White;
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label1.ImageIndex = 0;
            this.Label1.Location = new System.Drawing.Point(16, 44);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(64, 20);
            this.Label1.StylePainter = this.StyleGuideBase;
            this.Label1.Text = "Find What";
            // 
            // cboFilelds
            // 
            this.cboFilelds.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilelds.DropDownWidth = 208;
            this.cboFilelds.FixSize = false;
            this.cboFilelds.IntegralHeight = false;
            this.cboFilelds.Location = new System.Drawing.Point(86, 18);
            this.cboFilelds.Name = "cboFilelds";
            this.cboFilelds.Size = new System.Drawing.Size(202, 20);
            this.cboFilelds.StylePainter = this.StyleGuideBase;
            this.cboFilelds.TabStop = false;
            this.cboFilelds.TabIndex = 0;
            this.cboFilelds.SelectedIndexChanged += new System.EventHandler(this.cboFilelds_SelectedIndexChanged);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnOk.Location = new System.Drawing.Point(152, 81);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(64, 24);
            this.btnOk.StylePainter = this.StyleGuideBase;
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "Find Next";
            this.btnOk.ToolTipText = "Find Next -F3";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnCancel.Location = new System.Drawing.Point(224, 81);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(64, 24);
            this.btnCancel.StylePainter = this.StyleGuideBase;
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Exit";
            this.btnCancel.ToolTipText = "Exit - Escape";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ctlFind
            // 
            this.ctlFind.DropDownWidth = 208;
            this.ctlFind.FixSize = false;
            this.ctlFind.IntegralHeight = false;
            this.ctlFind.Location = new System.Drawing.Point(86, 44);
            this.ctlFind.Name = "ctlFind";
            this.ctlFind.Size = new System.Drawing.Size(202, 20);
            this.ctlFind.StylePainter = this.StyleGuideBase;
            this.ctlFind.TabIndex = 1;
            // 
            // chkbMatchCase
            // 
            this.chkbMatchCase.BackColor = System.Drawing.Color.White;
            this.chkbMatchCase.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkbMatchCase.Location = new System.Drawing.Point(16, 75);
            this.chkbMatchCase.Name = "chkbMatchCase";
            this.chkbMatchCase.Size = new System.Drawing.Size(120, 13);
            this.chkbMatchCase.TabIndex = 14;
            this.chkbMatchCase.Text = "Match Case";
            // 
            // ctlWholeWord
            // 
            this.ctlWholeWord.BackColor = System.Drawing.Color.White;
            this.ctlWholeWord.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ctlWholeWord.Location = new System.Drawing.Point(16, 92);
            this.ctlWholeWord.Name = "ctlWholeWord";
            this.ctlWholeWord.Size = new System.Drawing.Size(120, 13);
            this.ctlWholeWord.TabIndex = 17;
            this.ctlWholeWord.Text = "Match whole word";
            // 
            // FindDlg
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(297, 118);
            this.Controls.Add(this.ctlWholeWord);
            this.Controls.Add(this.chkbMatchCase);
            this.Controls.Add(this.ctlFind);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.cboFilelds);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FindDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Find";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FilterDlg_Load);
            this.ResumeLayout(false);

		}

        //private DataTable dt;
        //private DataView dtv;
        //private DataRow[] _dataRows;
        private int count = -1;
        private int index = 0;
        private string currentColumn;
        private string currentSearch;
        private int colIndex;
        private bool FindInAllColumns = false;
        private bool hasComboColumn = false;
        private int currentRow = -1;

        /// <summary>
        /// static Open dialog
        /// </summary>
        /// <param name="g"></param>
		public static void Open(Grid g) 
		{
            FindDlg f = new FindDlg(g);
			//f.grid = g ;
            //f.dtv = ((DataView)(g.DataList));
            //f.dt = f.dtv.Table;
			//f.SetStyleLayout(g.LayoutManager.Layout);
            f.Show();
		}
        /// <summary>
        /// static Open dialog
        /// </summary>
        /// <param name="g"></param>
        /// <param name="ColName"></param>
		public static void Open(Grid g, string ColName) 
		{
            FindDlg f = new FindDlg(g);
            //f.grid = g;
            //f.dtv = ((DataView)(g.DataSource));
            //f.dt = f.dtv.Table;
			f.cboFilelds.Text = ColName;
			f.cboFilelds.Enabled = false;
			//f.SetStyleLayout(g.LayoutManager.Layout);
            f.Show();
		}
        /// <summary>
        /// Set Style Layout of dialog
        /// </summary>
        /// <param name="value"></param>
		public override void SetStyleLayout(Nistec.WinForms.StyleLayout value)
		{
			base.SetStyleLayout (value);
			//this.SetStyleLayout(value);

		}
 
		private void FilterDlg_Load(object sender, System.EventArgs e)
		{
			SetFields();
		}
        
		private void SetFields() 
		{
            this.cboFilelds.Items.Add("All");
			foreach (GridColumnStyle col in grid.GridColumns) 
			{
                if (col.IsBound)
                {
                    this.cboFilelds.Items.Add(col.MappingName);
                    if (col.ColumnType == GridColumnType.ComboColumn)
                    {
                        hasComboColumn = true;
                    }
                }
			}
            this.cboFilelds.SelectedIndex = 0;
            this.ctlFind.Focus();
		}
        
      
		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.Close();
			}
			catch(Exception ex)
			{
				MessageBox.Show (ex.Message ,"Nistec");
			}
		}

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            btnOkClicked();
        }

		private void btnOkClicked()
		{
            
	        if ((this.ctlFind.Text.Length == 0)) 
                return;
			if(this.cboFilelds.Text.Length ==0)
			{
				MessageBox.Show ("Choose one field ","Nistec");
				this.cboFilelds.DroppedDown =true;     
				return;
			}

            try
            {
                if (currentColumn != this.cboFilelds.Text || currentSearch != ctlFind.Text)
                {
                    index = 0;
                }

                currentColumn = this.cboFilelds.Text;
                currentSearch = ctlFind.Text;
                colIndex = this.cboFilelds.SelectedIndex;
                if (colIndex > 0) 
                    colIndex--;
                count = this.grid.RowCount;
                FindInAllColumns = this.cboFilelds.SelectedIndex == 0;
                //FindNext();
                //AsyncStart();
                currentRow = -1;
                base.AsyncBeginInvoke(currentSearch);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Nistec");
            }
		
		}

 

       // private void FindRows(string text)
       // {
       //     _dataRows = dtv.Table.Select(text);

       //}

       // private void FindNext()
       // {
       //     if (index >= count)
       //     {
       //         RM.ShowInfo(NO_RECORDS_FOUND_MESSAGE);
       //     }
       //     this.grid.Select(_dataRows[index].
       // }

        private void FindNext()
        {
            bool flag1 = false;
            if (this.ctlWholeWord.Checked)
            {
                bool matchCase = this.chkbMatchCase.Checked;
                for (int i = index; i < count; i++)
                {
                    if (string.Compare(currentSearch, grid[i, colIndex].ToString(), true) == 0)
                    {
                        flag1 = true;
                        this.grid.Select(i);
                        index = i + 1;
                        break;
                    }
                }
            }
            else
            {
                StringComparison comparesionType = this.chkbMatchCase.Checked ? StringComparison.InvariantCulture : StringComparison.InvariantCultureIgnoreCase;
                for (int i = index; i < count; i++)
                {
                    if (grid[i, colIndex].ToString().IndexOf(currentSearch, comparesionType) > -1)
                    {
                        flag1 = true;
                        this.grid.Select(i);
                        index = i + 1;
                        break;
                    }
                }
            }


            if (!flag1)
            {
                index = 0;
                if (!this.ctlFind.Items.Contains(this.ctlFind.Text))
                {
                    this.ctlFind.Items.Add(this.ctlFind.Text);
                }
                RM.ShowInfo(NO_RECORDS_FOUND_MESSAGE);

            }
        }

 
        private void Find()
        {
            try
            {
                bool flag1 = false;

                if (this.ctlWholeWord.Checked)
                {
                    bool matchCase = this.chkbMatchCase.Checked;
                    for (int i = index; i < count; i++)
                    {
                        if (string.Compare(currentSearch, grid[i, colIndex].ToString(), true) == 0)
                        {
                            flag1 = true;
                            currentRow = i;//  AsyncInvoke(new object[] { i });
                            break;
                        }
                    }
                }
                else
                {
                    StringComparison comparesionType = this.chkbMatchCase.Checked ? StringComparison.InvariantCulture : StringComparison.InvariantCultureIgnoreCase;
                    for (int i = index; i < count; i++)
                    {
                        if (grid[i, colIndex].ToString().IndexOf(currentSearch, comparesionType) > -1)
                        {
                            flag1 = true;
                            currentRow = i;//AsyncInvoke(new object[] { i });
                            break;
                        }
                    }
                }
                if (!flag1)
                {
                    currentRow = -1; // AsyncInvoke(null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Nistec");
            }
        }

        private void FindInRows(int colFrom ,int colTo)
        {
            int i=0;
            //int cols = this.cboFilelds.Items.Count;
            bool wholeWord = this.ctlWholeWord.Checked;
            bool matchCase = this.chkbMatchCase.Checked;
            try
            {
                bool flag1 = false;

                for (int row = index; row < count; row++)
                {
                    bool found = false;
                    i = colFrom;
                    while (!found && i <= colTo)
                    {
                        found = FindInColumn(row, i, wholeWord, matchCase);
                        i++;
                    }
                    if (found)
                    {
                        flag1 = true;
                        currentRow = row;//AsyncInvoke(new object[] { row });
                        break;
                    }
                }
                if (!flag1)
                {
                    currentRow = -1; // AsyncInvoke(null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Nistec");
            }
        }

        private bool FindInColumn(int row, int col, bool wholeWord, bool matchCase)
        {
            try
            {
                bool flag1 = false;
                string cellContent = "";
              
                if (hasComboColumn)
                {
                    cellContent = grid.GetCellText(row, col); 
                }
                else
                {
                    cellContent = grid[row, col].ToString();
                }
                if (string.IsNullOrEmpty(cellContent))
                {
                    return flag1;
                }
                if (wholeWord)
                {
                    if (string.Compare(currentSearch, cellContent, !matchCase) == 0)
                    {
                        flag1 = true;
                        currentRow = row;// AsyncInvoke(new object[] { row });
                     }
                }
                else
                {
                    StringComparison comparesionType = matchCase ? StringComparison.InvariantCulture : StringComparison.InvariantCultureIgnoreCase;
                    if (cellContent.IndexOf(currentSearch, comparesionType) > -1)
                    {
                        flag1 = true;
                        currentRow = row;// AsyncInvoke(new object[] { row });
                    }
                }
                return flag1;
            }
            catch (Exception ex)
            {
                throw ex;
                //MessageBox.Show(ex.Message, "Nistec");
                //return false;
            }
        }
        /// <summary>
        /// Occurs when Async Executing is started Worker
        /// </summary>
        /// <param name="e"></param>
        protected override void OnAsyncExecutingWorker(AsyncCallEventArgs e)
        {
            base.OnAsyncExecutingWorker(e);
            //}
            //protected override void AsyncWorker()
            //{

            if (FindInAllColumns)
                FindInRows(0, this.cboFilelds.Items.Count - 2);
            else
            {
                colIndex = grid.GetColumnIndex(this.currentColumn);
                if (colIndex == -1)
                {
                    MsgBox.ShowError("Invalid column " + currentColumn);
                    return;
                }
                FindInRows(colIndex, colIndex);//Find();
            }
        }

        /// <summary>
        /// Occurs when Async Executing is Completed
        /// </summary>
        /// <param name="e"></param>
        protected override void OnAsyncCompleted(AsyncCallEventArgs e)
        {
            base.OnAsyncCompleted(e);
        
        // protected void AsyncComplited(object arg)
        //{
            try
            {
               
                //base.AsyncComplited(arg);
                if (currentRow >=0)//  /*arg*/ != null)
                {
                    int i = currentRow;// Convert.ToInt32(arg /*arg[0]*/);
                    this.grid.Select(i);
                    this.grid.CurrentRowIndex = i;
                    index = i + 1;
                }
                else //if (!flag1)
                {
                    index = 0;
                    RM.ShowInfo(NO_RECORDS_FOUND_MESSAGE);
                }
                if (!this.ctlFind.Items.Contains(this.ctlFind.Text))
                {
                    this.ctlFind.Items.Add(this.ctlFind.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Nistec");
            }

        }
        /// <summary>
        /// Process Dialog Key
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.F3)
            {
                btnOkClicked();
            }
            else if (keyData == Keys.Escape)
            {
                this.Close();
            }
            return base.ProcessDialogKey(keyData);
        }

        private void cboFilelds_SelectedIndexChanged(object sender, EventArgs e)
        {
            colIndex = this.cboFilelds.SelectedIndex;
        }
 	}
}

