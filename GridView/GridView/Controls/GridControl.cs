using System;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using Nistec.Win32;
using Nistec.WinForms;
using Nistec.GridView; 

using Nistec.Data;
using Nistec.Data.Advanced;
using Nistec.Win;

namespace Nistec.GridView
{
    /// <summary>
    /// Grid Control
    /// </summary>
	[ToolboxBitmap (typeof(Grid))]
	[System.ComponentModel.ToolboxItem(false)]
    public class GridControl : GridControlBase, IDataSource
	{
		#region Members
        private Grid m_Grid;
        private GridPopUp m_GridPopUp;
        private string m_DataMember;
        private string m_RowFilter;
        private string m_FieldFilter;
        private string m_Operator;
        private DataRelation relation;
        private string m_RelationName;
        //private List<string> ParentRelations;
        //private List<string> ChiledRelations;
        //private IDataSource IParent;

		#endregion

 		#region Constructors
		static GridControl()
		{
			GridControl.collapsed =NativeMethods.LoadImage("Nistec.GridView.Images.collapsed.gif");
			GridControl.expaned  =NativeMethods.LoadImage ("Nistec.GridView.Images.expaned.gif");
    	}
        /// <summary>
        /// Initializing Grid Control
        /// </summary>
		public GridControl()
		{

            m_RowFilter = String.Empty;
            m_FieldFilter = String.Empty;
            m_DataMember = String.Empty;
            m_RelationName = String.Empty;
            m_Operator = "=";

   			InitializeComponent();
		}
        /// <summary>
        /// Initializing Grid Control
        /// </summary>
        /// <param name="row"></param>
        public GridControl(int row):this()
        {
            this.curRow = row;
        }
		#endregion

		#region Dispose
        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (m_DroppedDown)
                this.m_GridPopUp.Close();
            if (m_GridPopUp != null)
            {
                m_GridPopUp.Closed -= new System.EventHandler(this.OnPopUpClosed);
            }

            base.Dispose(disposing);

        }
		#endregion

		#region Component Designer generated code
		private void InitializeComponent()
		{
			this.m_Grid = new GridView.Grid();
            this.m_GridPopUp = new GridPopUp(this);//, m_Grid.Size);
      		// 
			// m_Grid
			// 
			this.m_Grid.DataMember = "";
			this.m_Grid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.m_Grid.BackgroundColor  = System.Drawing.Color.White ;
			this.m_Grid.Location = new System.Drawing.Point(0, 0);
            this.m_Grid.CaptionVisible = false;
			this.m_Grid.RowHeadersVisible  =true;
			this.m_Grid.ReadOnly  =true;
			this.m_Grid.Name = "Grid";
			this.m_Grid.TabIndex = 0;
            //
            //m_GridPopUp
            //
            this.m_GridPopUp.Closed += new System.EventHandler(this.OnPopUpClosed);
			// 
			// GridControl
			// 
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Name = "GridControl";
			this.Size = new System.Drawing.Size(24, 19);
		}
		#endregion
		
		#region Overrides

		private void OnPopUpClosed(object sender,System.EventArgs e)
		{
			m_DroppedDown = false;
            m_GridPopUp.DisposePopUp(false);
			Invalidate(false);
		}
        /// <summary>
        /// Occurs on clicked
        /// </summary>
        /// <param name="e"></param>
 		protected override void OnClick(EventArgs e)
		{
			base.OnClick (e);
			if(m_DroppedDown )
			{
				m_GridPopUp.Close();
				return;
			}	
		
			ShowPopUp();			

		}
        /// <summary>
        /// OnKeyDown
        /// </summary>
        /// <param name="e"></param>
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);

			if (e.Shift  && e.KeyCode == Keys.Down)
				this.ShowPopUp();
			else if ((e.Shift && e.KeyCode == Keys.Up) || (e.KeyCode == Keys.Escape)) 
			{
				if(m_DroppedDown && m_GridPopUp != null)
				{
					m_GridPopUp.Close();
					m_DroppedDown = false;
				}
			}
		}
        /// <summary>
        /// On Style Property Changed
        /// </summary>
        /// <param name="e"></param>
   		protected override void OnStylePropertyChanged(PropertyChangedEventArgs e)
		{
			base.OnStylePropertyChanged (e);
			m_Grid.SetStyleLayout(this.dataGrid.LayoutManager.StylePlan);
		}
        /// <summary>
        /// Get the internal grid
        /// </summary>
        public Grid InternalGrid
        {
            get { return m_Grid; }
        }
        /// <summary>
        /// Get or Set the control is read only
        /// </summary>
        public override bool ReadOnly
        {
            get { return base.ReadOnly; }
            set
            {
                if (base.ReadOnly != value)
                {
                    base.ReadOnly = value;
                    if (this.m_Grid != null)
                    {
                        this.m_Grid.ReadOnly = value;
                    }
                }
            }
        }

        /// <summary>
        /// OnGridDataSourceChanged
        /// </summary>
        /// <param name="e"></param>
        protected override void OnGridDataSourceChanged(EventArgs e)
        {
            if (m_DroppedDown)
            {
                this.ClosePopUp();
            }

            //ResetData();
        }
        /// <summary>
        /// OnGridSizeChanged
        /// </summary>
        /// <param name="e"></param>
        protected override void OnGridSizeChanged(EventArgs e)
        {
            this.m_VisibleWidth = CalcGridWidth();
        }

        /// <summary>
        /// OnVerticalScrollChanged
        /// </summary>
        /// <param name="e"></param>
        protected override void OnVerticalScrollChanged(EventArgs e)
        {
            base.OnVerticalScrollChanged(e);
            if (m_DroppedDown && curRow > -1)
            {
                Rectangle rect = this.dataGrid.GetRowRect(curRow);
                if (rect == Rectangle.Empty)
                {
                    this.dataGrid.Collapse(curRow);
                    ClosePopUp();
                    m_DroppedDown = false;
                    return;
                }

                m_GridPopUp.Top = curScreenPoint.Y + rect.Bottom;// +BottmSpace;


                //int value = this.dataGrid.VertScrollBar.Value;
                //if (value != curVScroll)
                //{
                //    int delta = value - curVScroll;
                //    curVScroll = value;
                //    m_GridPopUp.Top -= delta * dataGrid.PreferredRowHeight;
                //}
            }
        }
		#endregion

        #region relation

        private DataTable GetRelationRows(int relationIndex)
        {
            try
            {
                if (!(this.DataSource is DataSet))
                    return null;

                DataSet ds = (DataSet)this.DataSource;
                if(relationIndex <0 || relationIndex >=ds.Relations.Count)
                    return null;
                DataRelation drel = ds.Relations[relationIndex];
                return GetRelationRows(ds, drel);
            }
            catch (Exception ex) 
            {
                MsgBox.ShowError(ex.Message);
                return null;
            }
        }

        private DataTable GetRelationRows(string relationName)
        {
            try
            {
                if (!(this.DataSource is DataSet))
                    return null;
                DataSet ds = (DataSet)this.DataSource;
                if (!ds.Relations.Contains(relationName))
                    return null;
                DataRelation drel = ds.Relations[relationName];
                return GetRelationRows(ds, drel);
            }
            catch (Exception ex)
            {
                MsgBox.ShowError(ex.Message);
                return null;
            }

        }

        private DataTable GetRelationRows(DataSet ds,DataRelation relation)
        {
            //DataSet ds = (DataSet)this.DataSource;
            DataRowView drv = dataGrid.GetCurrentDataRow();
            if (drv == null)
                return null;
            //string tblChiled = relation.ChildTable.TableName;
            //DataTable dt = ds.Tables[tblChiled].Clone();
            DataTable dt = null;
            //ds.Merge(new DataRow[] { drv.Row });

            DataRow[] drs=drv.Row.GetChildRows(relation);
            if (drs == null || drs.Length == 0)
            {
                dt = null;
                m_DataView = null;
            }
            else
            {
                DataSet dsr = new DataSet();
                dsr.Merge(drs);
                dt = dsr.Tables[0];
                m_DataView = dt.DefaultView;
            }
            return dt;
        }
  
        /// <summary>
        /// Set Relation Rows
        /// </summary>
        /// <param name="relationName"></param>
        public void SetRelationRows(string relationName)
        {
            //IParent = parent;
            InitDataSource(relationName);

            if (m_Grid.DataSource == null)
            {
                //this.DataSource = this.dataGrid.DataSource;
                ResetData();
            }
            //SetRelationRows(relationName);
            DataTable dt = GetRelationRows(relationName);
            if (dt != null)
            {
                m_Grid.SetDataBinding(dt, "");
                //m_Grid.TableStyle.SetRelationsList(ChiledRelations, this);
            }
        }
        private void InitDataSource(string relationName)
        {
            if (this.DataSource == null)
            {
                this.DataSource =dataGrid.DataSource;
            }
            if (this.RelationName.Equals(relationName))
                return;

            if (!(DataSource is DataSet))
                return;
            DataSet ds = (DataSet)this.DataSource;
            foreach (DataRelation rel in ds.Relations)
            {
                if (rel.RelationName.Equals(relationName))
                {
                    this.DataMember = rel.ChildTable.TableName;
                    this.m_RelationName = relationName;
                    this.relation = rel;
                    break;
                }
            }
            //if (ParentRelations == null)
            //{
            //    ParentRelations = new List<string>();
            //    foreach (DataRelation rel in ds.Relations)
            //    {
            //        this.ParentRelations.Add(rel.RelationName);
            //    }
            //}
            //if (ChiledRelations == null)
            //{
            //    ChiledRelations = new List<string>();
            //    foreach (DataRelation rel in ds.Relations)
            //    {
            //        if (rel.ParentTable.TableName.Equals(this.DataMember))
            //        {
            //            this.ChiledRelations.Add(rel.RelationName);
            //        }
            //    }
            //}
        }

        //private void SetRelationRows(string relationName)
        //{
        //    try
        //    {
        //        if (!(this.DataSource is DataSet))
        //            return ;
        //        DataSet ds = (DataSet)this.DataSource;
        //        if (!ds.Relations.Contains(relationName))
        //            return ;
        //        DataRelation drel = ds.Relations[relationName];
        //        DataRowView drv = dataGrid.GetCurrentDataRow();
        //        if (drv == null)
        //            return ;

        //        m_DataView = drv.CreateChildView(relation);

        //        string prefix = "";
        //        string sufix = "";
        //        string rowFilter = "";
        //        //string sort = "";
        //        //List<object> keys = new List<object>();
        //        foreach (DataColumn c in drel.ParentColumns)
        //        {
        //            //keys.Add(drv[c.ColumnName]);
        //            //sort += c.ColumnName + ",";
        //            sufix = "";
        //            if (c.DataType == typeof(string))
        //                sufix = "'";
        //            else if (c.DataType == typeof(DateTime))
        //                sufix = "'";

        //            rowFilter += prefix + c.ColumnName + "=" + sufix + drv[c.ColumnName] + sufix;
        //            prefix = " AND ";
        //        }

        //        //m_DataView.Sort = sort.TrimEnd(',');
        //        //DataRowView[] rowsv = m_DataView.FindRows(keys.ToArray());
        //        //DataRow[] drs = new DataRow[rowsv.Length];
        //        m_DataView.RowFilter = rowFilter;
                
        //        m_Grid.SetDataBinding(m_DataView, "");
        //        //m_Grid.TableStyle.SetRelationsList(ChiledRelations, this);

        //    }
        //    catch (Exception ex)
        //    {
        //        MsgBox.ShowError(ex.Message);
        //    }

        //}

        /// <summary>
        /// SetRelationRows
        /// </summary>
        public void SetRelationRows()
        {
            if (relation == null)
            {
                throw new ArgumentException("Invalid relation ");
            }
            if (m_Grid.DataSource == null)
            {
                this.DataSource = this.dataGrid.DataSource;
                ResetData();
            }
            DataSet ds = (DataSet)this.DataSource;
            if (ds != null)
            {
                m_Grid.SetDataBinding(GetRelationRows(ds, relation), "");
            }
        }

        #endregion

        #region Setting

        /// <summary>
        /// ResetData
        /// </summary>
        public void ResetData()
        {

            m_Grid.SetStyleLayout(this.dataGrid.LayoutManager.Layout);
            m_Grid.BeginInit();
            m_Grid.ReadOnly = this.m_ReadOnly;
            m_Grid.DataMember = this.DataMember;
            m_Grid.DataSource = this.DataSource;
            //m_Grid.MappingName="tbl2";
            m_Grid.EndInit();

   
            m_Grid.CaptionText = m_Grid.MappingName;
            m_DataView = m_Grid.DataList;// DataList;


            resetData = false;
        }
        /// <summary>
        /// ResetLayout
        /// </summary>
		public void ResetLayout()
		{
			m_Grid.SetStyleLayout(this.dataGrid.LayoutManager.Layout);
            this.RightToLeft= m_Grid.RightToLeft = dataGrid.RightToLeft;
            m_Grid.ReadOnly = m_ReadOnly;

            m_Grid.CaptionVisible = m_CaptionVisible;
            m_Grid.CaptionText = m_CaptionText;

            this.m_VisibleWidth =CalcGridWidth();
  			resetLayout=false;
		}

  
        /// <summary>
         /// Calc Grid Width
        /// </summary>
        /// <returns></returns>
 		public int CalcGridWidth()
		{
			int width=0;
			int rowHeader=m_Grid.RowHeadersVisible ? m_Grid.RowHeaderWidth :0;

            if (m_DataView!=null)//DataSource != null)
			{
				int i=m_DataView.Table.Columns.Count;
				width=i*m_Grid.PreferredColumnWidth;
			}
			else
			{
				width=this.Width;	
			}
            
			width += rowHeader; 

            int gridWidth =0;

            if(this.RightToLeft==RightToLeft.Yes)
                gridWidth = dataGrid.Width - (this.dataGrid.Width- this.Right+3);// -dataGrid.VisibleVerticalScrollBarWidth;
            else
                gridWidth = dataGrid.Width - this.Left - 3;// -dataGrid.VisibleVerticalScrollBarWidth;

            return Math.Min(width, gridWidth);

		}

        internal void SetDimension()
        {
            curPoint = Point.Empty;
            curSize = Size.Empty;
            int width = 3 + this.m_VisibleWidth;
            int height = this.CalcGridHeight();

            if (PointToGrid)
            {
                this.m_GridPopUp.Parent = dataGrid;
                if (this.RightToLeft == RightToLeft.Yes)
                    curPoint = new Point(this.Left - (width - this.Width), this.Bottom + 2);
                else
                    curPoint = new Point(this.Left, this.Bottom + 2);

                int h = Math.Min(height, height - ((dataGrid.Top + curPoint.Y + height) - dataGrid.Bottom));
                if (h < MinimumHeight)
                {
                    height = Math.Min(height, this.Top - 2);
                    curPoint.Y = this.Top - height - 2;
                }
                else
                {
                    height = h;
                }
            }
            else
            {
                this.m_GridPopUp.Parent = gridForm;
                if (curScreenPoint == Point.Empty)
                {
                    base.SetGridScreenPoint();
                }

                if (this.RightToLeft == RightToLeft.Yes)
                    curPoint = new Point(curScreenPoint.X + (this.Left - (width - this.Width)), curScreenPoint.Y + this.Bottom );
                else
                    curPoint = new Point(curScreenPoint.X + this.Left, curScreenPoint.Y + this.Bottom);

                //if (this.RightToLeft == RightToLeft.Yes)
                //    curPoint = new Point(dataGrid.Left + (this.Left - (width - this.Width)), dataGrid.Top + this.Bottom );
                //else
                //    curPoint = new Point(dataGrid.Left + this.Left, dataGrid.Top + this.Bottom + 2);

                curPoint = this.PointToScreen(new Point(this.Bounds.Left, this.Bounds.Bottom + 2));

                height = Math.Min(height, Math.Max(0, height - ((curPoint.Y + height) - gridForm.ClientSize.Height)));
            }

            curSize = new Size(width, height);

        }

        internal void SetDimension(Point pt)
        {
            curPoint = Point.Empty;
            curSize = Size.Empty;
            PointToGrid = false;

            int width = 3 + this.m_VisibleWidth;
            int height = this.CalcGridHeight();
            if (curScreenPoint == Point.Empty)
            {
                base.SetGridScreenPoint();
            }
            this.m_GridPopUp.Parent = gridForm;

            if (this.RightToLeft == RightToLeft.Yes)
                curPoint = new Point(curScreenPoint.X + (pt.X - (width - this.Width)), curScreenPoint.Y + pt.Y /*+ this.Height*/);
            else
                curPoint = new Point(curScreenPoint.X + pt.X, curScreenPoint.Y + pt.Y);


            int h = Math.Min(height, Math.Max(0, height - ((curPoint.Y + height) - gridForm.ClientSize.Height)));
            height = h <= 0 ? height : height;

            curSize = new Size(width, height);

        }

        internal int CalcGridHeight()
        {
            int rowAdd = m_Grid.AllowAdd ? m_Grid.PreferredRowHeight : 0;
            int captionHeight = m_CaptionVisible ? GridView.Grid.DefaultCaptionHeight : 0;
            int colHeader = m_Grid.ColumnHeadersVisible ? Grid.DefaultColumnHeaderHeight : 0;
            //int cnt =m_DataView.Count <= this.m_VisibleRows?m_DataView.Count:this.m_VisibleRows;
            int cnt = m_Grid.RowCount;
            cnt = cnt <= this.m_VisibleRows ? cnt : this.m_VisibleRows;
            int height = 3 + rowAdd + captionHeight + colHeader + (cnt * m_Grid.PreferredRowHeight);

            int scrollBottom = 0;
            //if(MaxWidth<=this.m_VisibleWidth)
            //    scrollBottom+=Grid.DefaultScrollWidth;

            //int height=m_Grid.Height;
            height += colHeader + scrollBottom;

            return height;
        }
        internal bool CanShow()
        {
            if (m_Grid == null)
                return false;
            if (this.DataSource == null)
            {
                MsgBox.ShowWarning("Invalid DataSource");
                return false;
            }
            return true;
        }

        #endregion

		#region Filter

		private Type colType() 
		{
			if(m_FieldFilter!="")
			   return m_DataView.Table.Columns[m_FieldFilter].DataType;
           return typeof(string);
		}

		private bool cmdFilter( ) 
		{
			string strFilter="";

			if(this.m_RowFilter.Length==0 || this.m_FieldFilter.Length==0)
			{
				strFilter = "";
                goto Label_01;
			}
            Type coltype = colType();

            if (coltype== typeof(string))//.EndsWith("String")) 
			{
 					strFilter = (this.m_FieldFilter  + (" " 
						+ (this.m_Operator  + (" \'" 
						+ (this.m_RowFilter + "\'")))));
			}
            else if (coltype == typeof(DateTime))//.EndsWith("DateTime")) 
			{
				strFilter = (this.m_FieldFilter  + (" " 
					+ (this.m_Operator  + (" \'" 
					+ (this.m_RowFilter + "\'")))));
			}
			else //if (Info.IsNumber (this.m_RowFilter )) 
			{
				strFilter = (this.m_FieldFilter  + (" " 
					+ (this.m_Operator  + (" " + this.m_RowFilter ))));
			}
 	Label_01:		

			if(m_RowFilter!=strFilter)
			{
				m_RowFilter=strFilter;
				m_Grid.RemoveFilter();
				if(strFilter.Length>0)
				{
					m_Grid.SetFilter(strFilter);
				}
			}
			// //Display the number of rows in the view
			if ((m_DataView.Count == 0)) 
			{
				//MessageBox.Show ("NO RECORDS FOUND","Nistec",MessageBoxButtons.OK , MessageBoxIcon.Information );
                return false;  
			}
	
			return true;
		}
        
		private void cmdRemoveFilter() 
		{
			m_RowFilter="";
			m_DataView.RowFilter = "";
			m_Grid.DataSource = m_DataSource;
		}

 		#endregion

		#region Show

		[UseApiElements("ShowWindow")]
		private void ShowPopUp()
		{
			if(!CanShow())
	            return;
            if (resetData)
                ResetData();
            if (resetLayout)
				ResetLayout();
			if(this.m_Grid ==null )
				throw new Exception("Error in GridTableStyle ");
            if (!cmdFilter())
                return;

             SetDimension();
            this.m_GridPopUp.ShowPopUp(m_GridPopUp.Handle, curSize,curPoint);
			m_DroppedDown = true;
			this.Invalidate();
		}


        [Nistec.Win.UseApiElements("ShowWindow")]
        internal void ShowPopUpInternal(Point pt)
        {
            if (!CanShow())
                return;
            if (resetLayout)
                ResetLayout();
            if (this.m_Grid == null)
                throw new Exception("Error in GridTableStyle ");

            SetDimension(pt);
            this.m_GridPopUp.ShowPopUp(m_GridPopUp.Handle, curSize, curPoint);
            m_GridPopUp.Start = true;
            m_DroppedDown = true;
            this.Invalidate();
        }

        internal void ClosePopUp()
        {
            if (m_GridPopUp != null)
            {
                m_GridPopUp.Close();
            }
        }

  
		#endregion

		#region Properties

       // [TypeConverter(typeof(RelationshipConverter)), Editor("Microsoft.VSDesigner.Data.Design.DataRelationEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), DefaultProperty("RelationName")]
       //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("DataCategory_Data"), Description("DataSetRelationsDescr"),Editor("Microsoft.VSDesigner.Data.Design.DataRelationCollectionEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        /// <summary>
        /// Get or Set Relation
        /// </summary>
        public DataRelation Relation
        {
            get { return relation; }
            set
            {
                relation = value;
                if (relation == null)
                    m_RelationName = "";
                else
                    m_RelationName = relation.RelationName;
                resetLayout = true;
            }
        }
        /// <summary>
        /// Get or Set DataMember
        /// </summary>
 		public string DataMember
		{
			get{return m_DataMember;}
			set
            {
                m_DataMember=value;
                resetData = true;
            }
		}
        /// <summary>
        /// Get the control data relation name
        /// </summary>
        public virtual string RelationName
        {
            get { return m_RelationName; }
            //set
            //{
            //    m_RelationName = value;
            //    resetData = true;
            //}
        }
        /// <summary>
        /// Get or Set the relation Foreign Key
        /// </summary>
        public string ForeignKey
		{
			get{return m_FieldFilter;}
			set
            {
                m_FieldFilter=value;
                resetData = true;
            }
		}
        /// <summary>
        /// Get or Set Row Filter
        /// </summary>
		public string RowFilter
		{
            get { return m_RowFilter; }
            set { m_RowFilter = value; }
		}
        /// <summary>
        /// Get or Set AllowAdd
        /// </summary>
        public bool AllowAdd
        {
            get { return m_AllowAdd; }
            set { m_AllowAdd = value; }
        }

		#endregion

        #region ComboPopUp

        internal class GridPopUp : Nistec.WinForms.Controls.McPopUpBase
        {
            internal Grid dataGrid;
            protected GridControl mparent = null;
            private bool dispose = false;

            #region Constructors

            public GridPopUp(GridControl parent)//, Size size)
                : base(parent)
            {
                mparent = parent;
                this.KeyPreview = false;
                this.dataGrid = ((GridControl)parent).InternalGrid;

                this.dataGrid.BorderStyle = BorderStyle.FixedSingle;
                this.dataGrid.DataMember = "";
                this.dataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
                this.dataGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
                this.dataGrid.Location = new System.Drawing.Point(0, 0);
                this.dataGrid.Name = "dataGrid";
                this.dataGrid.SelectionType = Nistec.GridView.SelectionType.Cell;
                
                
                this.Controls.Add(this.dataGrid);
                this.Name = "GridPopUp";
                this.Text = "GridPopUp";
                //this.TopMost = true;
                this.TopLevel = false;
           }

            public void ShowPopUp(IntPtr hwnd, Size size, Point pt)
            {
                this.Location = pt;
                this.Size = size;
                this.Height = this.Height;
                this.LockClose = true;
                base.ShowPopUp(hwnd, WindowShowStyle.ShowNormalNoActivate);
                base.Start = true;
                this.BringToFront();
             }

             #endregion

            #region Dispose

            public void DisposePopUp(bool disposing)
            {
                dispose = disposing;
                this.Dispose(disposing);
            }

            protected override void Dispose(bool disposing)
            {
                //this.panel1.Controls.Clear ();

                //if (disposing)
                //{
                //    //mparent.Controls[0].LostFocus -= new System.EventHandler(this.ParentControlLostFocus);
                //    if (components != null)
                //    {
                //        components.Dispose();
                //    }
                //}
                base.Dispose(dispose);// disposing );
            }
            #endregion

            #region Overrides

            protected override void OnClosed(System.EventArgs e)
            {
                this.Hide();
                base.OnClosed(e);
            }

            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);
                base.LockClose = true;
            }

            protected override bool ProcessDialogKey(Keys keyData)
            {

                switch ((keyData & Keys.KeyCode))
                {
                    case Keys.W:
                        if ((keyData & Keys.Control) != Keys.None)
                            this.Close();
                        break;
                }
                 return base.ProcessDialogKey(keyData);
            }
  
            #endregion

            #region Properties

            public GridCell CurrentCell
            {
                get { return this.dataGrid.CurrentCell; }
            }

            public override object SelectedItem
            {
                get
                {
                    return this.dataGrid.CurrentCell as object;
                }
            }

            internal new bool LockClose
            {
                get { return base.LockClose; }
                set { base.LockClose = value; }
            }

            #endregion
        }

        #endregion

	}


}