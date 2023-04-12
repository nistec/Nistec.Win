using System;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using mControl.Win32;
using mControl.WinCtl.Controls;
using mControl.GridView; 
using mControl.Util;

namespace mControl.GridView
{


	[ToolboxBitmap (typeof(Grid))]
	[System.ComponentModel.ToolboxItem(false)]
	public class GridControl :mControl.WinCtl.Controls.CtlButtonBase 
	{
		#region Members
		internal Grid dataGrid;
		private const int buttonWidth=11;
		private System.ComponentModel.Container components   = null;
		private Grid m_Grid;
		private GridPopUp m_GridPopUp;
 		private bool m_DroppedDown;
		private DataView m_DataView;
		private object m_DataSource;
		private string m_DataMember;
		private string m_RowFilter;
		private string m_FieldFilter;
		//private GridFilterType m_FilterType; 
		private string m_Operator;
		private int m_VisibleRows;
		private int m_VisibleWidth;
		//private int m_MaxWidth;
		private bool m_ReadOnly;
        private bool m_CaptionVisible;
        private string m_CaptionText;

        private bool resetData;
        private bool resetLayout;
        private Form gridForm;
        private DataRelation relation;

        private bool pointToGrid = false;
        private Point curPoint = Point.Empty;
        private Size curSize = Size.Empty;
        
        const int MinimumHeight = 75;

		internal protected static Image collapsed;
		internal protected static Image expaned;

		#endregion

 		#region Constructors
		static GridControl()
		{
			GridControl.collapsed =NativeMethods.LoadImage("mControl.GridView.Images.collapsed.gif");
			GridControl.expaned  =NativeMethods.LoadImage ("mControl.GridView.Images.expaned.gif");

		}

		public GridControl()
		{
			base.NetReflectedFram("ba7fa38f0b671cbc");
			m_DroppedDown=false;
			m_RowFilter=String.Empty;
			m_FieldFilter=String.Empty;
			m_DataMember=String.Empty;
			//m_FilterType=GridFilterType.Equal; 
			m_Operator="=";
			m_VisibleRows=10;
			m_VisibleWidth=0;
			//m_MaxWidth=Grid.MaxGridWidth;
			m_ReadOnly=true;
	        m_CaptionVisible = false;
            m_CaptionText = "";

            resetData=true;
            resetLayout=true;

			InitializeComponent();
			//base.FixSize=true;

//			this.collapsed =NativeMethods.LoadImage("mControl.GridView.Images.collapsed.gif");
//			this.expaned  =NativeMethods.LoadImage ("mControl.GridView.Images.expaned.gif");

//			popUpSize=new Size(200, 200);
		}

		#endregion

		#region Dispose
		protected override void Dispose( bool disposing )
		{
			if(	m_DroppedDown)
				this.m_GridPopUp.Close ();
            if (this.dataGrid != null)
            {
                this.dataGrid.SizeChanged -= new EventHandler(dataGrid_SizeChanged);
                this.dataGrid.DataSourceChanged -= new EventHandler(dataGrid_DataSourceChanged);
            }
            if (m_GridPopUp != null)
            {
                m_GridPopUp.Closed -= new System.EventHandler(this.OnPopUpClosed);
            }
  
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		
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
			this.m_Grid.RowHeadersVisible  =false;
			this.m_Grid.ReadOnly  =true;
			this.m_Grid.Name = "Grid";
			this.m_Grid.TabIndex = 0;
            ///
            ///m_GridPopUp
            ///
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

		public override IStyleLayout CtlStyleLayout
		{
			get{return dataGrid.CtlStyleLayout as IStyleLayout;} 
		}

		private void OnPopUpClosed(object sender,System.EventArgs e)
		{
			m_DroppedDown = false;
            m_GridPopUp.DisposePopUp(false);
			Invalidate(false);
		}

		public void DoDropDown()
		{
			base.PerformClick ();
		}

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

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint (e);
    		DrawImage(e.Graphics);
		}

		internal void DrawImage(Graphics g)
		{

			Rectangle bounds=new Rectangle(0,-1,buttonWidth,buttonWidth);

			Image image=null;
			if(m_DroppedDown)
				image=expaned; 
			else
				image=collapsed; 
	
			if(image !=null) 
			{
				g.DrawImage (image,bounds);
			}
		}

		protected override void OnStylePropertyChanged(PropertyChangedEventArgs e)
		{
			base.OnStylePropertyChanged (e);
			m_Grid.SetStyleLayout(this.dataGrid.CtlStyleLayout.StylePlan);
		}

        
		#endregion

        #region relation

        //private string[] relationTemplate;
       // private string[] parentColumns;

        //private void SetRelationFilter()
        //{
        //    string field = "";
           
        //    if (this.DataSource is DataSet)
        //    {
        //        DataSet ds = (DataSet)this.DataSource;
                
        //        if (ds.Relations.Count > 0)
        //        {
        //            relationTemplate = new string[ds.Relations.Count];
        //            int j = 0;
        //            foreach (DataRelation rel in ds.Relations)
        //            {
        //                //if (rel.ChildTable.TableName.Equals(this.DataMember))
        //                //{
        //                for (int i = 0; i < rel.ChildColumns.Length; i++)
        //                {
        //                    field += rel.ChildColumns[i].ColumnName + "=" + rel.ParentColumns[i].ColumnName + "";
        //                    if(i < rel.ChildColumns.Length - 1) 
        //                        field += " AND " ;
        //                }
        //                //}
        //                j++;
        //            }
        //            relationTemplate[j] = field;
        //        }
        //    }

        //}

        private DataTable GetRelationRows(int relationIndex)
        {
            DataSet ds = (DataSet)this.DataSource;
            DataRelation drel = ds.Relations[relationIndex];
            return GetRelationRows(ds,drel);
        }

        private DataTable GetRelationRows(string relationName)
        {
            DataSet ds = (DataSet)this.DataSource;
            DataRelation drel= ds.Relations[relationName];
            return GetRelationRows(ds,drel);
        }

        private DataTable GetRelationRows(DataSet ds,DataRelation relation)
        {
            //DataSet ds = (DataSet)this.DataSource;
            DataRowView drv = dataGrid.GetCurrentDataRow();
            if (drv == null)
                return null;
            //DataRow[] drs = drv.Row.GetChildRows(relation);
            string tblChiled = relation.ChildTable.TableName;
            DataTable dt = ds.Tables[tblChiled].Clone();
            
            //foreach (DataRow dr in drs)
            //{
            //    dt.ImportRow(dr);
            //}

            DataSet dsr = new DataSet();
            dsr.Merge(drv.Row.GetChildRows(relation));
            dt=dsr.Tables[0];
            m_DataView = dt.DefaultView;
            return dt;
        }

        public void SetRelationRows(string relationName)
        {
            if (m_Grid.DataSource == null)
            {
                this.DataSource = this.dataGrid.DataSource;
                ResetData();
            }
            m_Grid.SetDataBinding(GetRelationRows(relationName), "");
        }

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
            m_Grid.SetDataBinding(GetRelationRows(ds,relation), "");
        }

        #endregion

        #region Setting

        public void ResetData()
        {

            m_Grid.SetStyleLayout(this.dataGrid.CtlStyleLayout.Layout);
            m_Grid.BeginInit();
            m_Grid.ReadOnly = this.m_ReadOnly;
            m_Grid.DataSource = this.DataSource;
            m_Grid.DataMember = this.DataMember;
            //m_Grid.MappingName="tbl2";
            m_Grid.EndInit();

   
            m_Grid.CaptionText = m_Grid.MappingName;
            m_DataView = m_Grid.DataList;// DataList;


            resetData = false;
        }
		public void ResetLayout()
		{
			m_Grid.SetStyleLayout(this.dataGrid.CtlStyleLayout.Layout);
            this.RightToLeft= m_Grid.RightToLeft = dataGrid.RightToLeft;
            m_Grid.ReadOnly = m_ReadOnly;

            m_Grid.CaptionVisible = m_CaptionVisible;
            m_Grid.CaptionText = m_CaptionText;

            this.m_VisibleWidth =CalcGridWidth();
  			resetLayout=false;
		}

        private void SetDimension()
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
                if (this.RightToLeft == RightToLeft.Yes)
                    curPoint = new Point(dataGrid.Left + (this.Left - (width - this.Width)), dataGrid.Top + this.Bottom + 2);//this.Left - (width - this.Width), this.Bottom + 2);
                else
                    curPoint = new Point(dataGrid.Left + this.Left, dataGrid.Top + this.Bottom + 2);
                height = Math.Min(height, Math.Max(0, height - ((curPoint.Y + height) - gridForm.ClientSize.Height)));
            }

            curSize = new Size(width,height);

            //Point pt = this.Parent.PointToScreen(new Point(this.Left, this.Bottom + 2));
            //if (this.RightToLeft == RightToLeft.Yes)
            //    pt = this.Parent.PointToScreen(new Point(this.Left-(m_Grid.Size.Width-this.Width ),this.Bottom+2 ));

            //Rectangle screenRect = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
            //if(screenRect.Bottom < pt.Y + m_GridPopUp.Height)
            //{
            //    pt.Y = pt.Y - m_GridPopUp.Height - this.Height - 1;
            //}

            //if(screenRect.Right < pt.X + m_GridPopUp.Width)
            //{
            //    pt.X = screenRect.Right - m_GridPopUp.Width - 2;
            //}


         }

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
            
			//width += rowHeader; 

            int gridWidth =0;

            if(this.RightToLeft==RightToLeft.Yes)
                gridWidth = dataGrid.Width - (this.dataGrid.Width- this.Right+3);// -dataGrid.VisibleVerticalScrollBarWidth;
            else
                gridWidth = dataGrid.Width - this.Left - 3;// -dataGrid.VisibleVerticalScrollBarWidth;

            return Math.Min(width, gridWidth);

		}
		
		private int CalcGridHeight()
		{
            int rowAdd = m_Grid.AllowAdd ? m_Grid.PreferredRowHeight : 0;
            int captionHeight = m_CaptionVisible ? GridView.Grid.DefaultCaptionHeight : 0;
			int colHeader=m_Grid.ColumnHeadersVisible ? Grid.DefaultColumnHeaderHeight :0;
			int cnt =m_DataView.Count <= this.m_VisibleRows?m_DataView.Count:this.m_VisibleRows;
            int height = 3 + rowAdd + captionHeight + colHeader + (cnt * m_Grid.PreferredRowHeight);

			int scrollBottom=0;
            //if(MaxWidth<=this.m_VisibleWidth)
            //    scrollBottom+=Grid.DefaultScrollWidth;
	
			//int height=m_Grid.Height;
			height+=colHeader+scrollBottom;
	
			return height;
		}

		private bool CanShow()
		{
			if(m_Grid==null)
				return false;
			if(this.DataSource ==null)
			{
				MsgBox.ShowWarning ("Invalid DataSource");
				return false;
			}
			return true;
		}

		public void SetGrid(Grid parentGrid)
		{
			this.dataGrid = (Grid)parentGrid;
            gridForm = parentGrid.FindForm();
            this.dataGrid.SizeChanged += new EventHandler(dataGrid_SizeChanged);
            this.dataGrid.DataSourceChanged += new EventHandler(dataGrid_DataSourceChanged);
        }

        void dataGrid_DataSourceChanged(object sender, EventArgs e)
        {
            ResetData();
        }

        void dataGrid_SizeChanged(object sender, EventArgs e)
        {
            this.m_VisibleWidth = CalcGridWidth();
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
			else //if (Regx.IsNumeric (this.m_RowFilter )) 
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
				//MessageBox.Show ("NO RECORDS FOUND","mControl",MessageBoxButtons.OK , MessageBoxIcon.Information );
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

		[mControl.Util.UseApiElements("ShowWindow")]
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


        [mControl.Util.UseApiElements("ShowWindow")]
        internal void ShowPopUpInternal(Point pt)
        {
            if (!CanShow())
                return;
            //if (resetData)
            //    ResetData();
            if (resetLayout)
                ResetLayout();
            if (this.m_Grid == null)
                throw new Exception("Error in GridTableStyle ");

            SetDimension();
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
        public DataRelation Relation
        {
            get { return relation; }
            set
            {
                relation = value;
                resetLayout = true;
            }
        }

        public bool DroppedDown
        {
            get { return m_DroppedDown; }
        }

        public bool PointToGrid
		{
            get { return pointToGrid; }
            set 
            {
                pointToGrid = value;
                resetLayout = true;
            }
		}

        public bool CaptionVisible
		{
            get { return m_CaptionVisible; }
            set 
            { 
                m_CaptionVisible = value;
                resetLayout = true;
            }
		}
        public string CaptionText
        {
            get { return m_CaptionText; }
            set 
            { 
                m_CaptionText = value;
                resetLayout = true;
            }
        }
		public int VisibleRows
		{
			get{return m_VisibleRows;}
			set
            {
                m_VisibleRows=value;
                resetLayout = true;
            }
		}

 		public Grid InternalGrid
		{
			get{return m_Grid;}
		}

		public bool ReadOnly
		{
			get{return m_ReadOnly;}
			set
			{
				if(this.ReadOnly!=value)
				{
					m_ReadOnly=value;
                    resetLayout = true;

					if(this.m_Grid!=null)
					{
						this.m_Grid.ReadOnly=value;
					}
				}
			}
		}


		//[TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), Description("GridDataSourceDescr"), DefaultValue((string) null), Category("Data"), RefreshProperties(RefreshProperties.Repaint)]
		public object DataSource
		{
			get{return m_DataSource;}
			set
            {
                m_DataSource=value;
                resetData = true;
            }
		}

		public string DataMember
		{
			get{return m_DataMember;}
			set
            {
                m_DataMember=value;
                resetData = true;
            }
		}

        public string ForeignKey
		{
			get{return m_FieldFilter;}
			set
            {
                m_FieldFilter=value;
                resetData = true;
            }
		}

		public string RowFilter
		{
            get { return m_RowFilter; }
            set { m_RowFilter = value; }
		}

		#endregion

        #region ComboPopUp

        internal class GridPopUp : mControl.WinCtl.Controls.CtlPopUpBase
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
                this.dataGrid.SelectionType = mControl.GridView.SelectionType.Cell;
                
                
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
                base.ShowPopUp(hwnd,4);
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