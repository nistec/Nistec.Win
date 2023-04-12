using System;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Nistec.Win32;
using Nistec.WinForms;
using Nistec.GridView;
using Nistec.Win; 


namespace Nistec.GridView
{
    /// <summary>
    /// VGridControl
    /// </summary>
	[ToolboxBitmap (typeof(Grid))]
	[System.ComponentModel.ToolboxItem(false)]
    public class VGridControl : GridControlBase
	{
		#region Members
		private VGrid m_Grid;
		private GridPopUp m_GridPopUp;
        private const int m_width=160;

		#endregion

		#region Constructors
        /// <summary>
        /// Initilaized VGridControl
        /// </summary>
        public VGridControl():base()
		{
	    	InitializeComponent();
		}

		#endregion

		#region Dispose
        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
		protected override void Dispose( bool disposing )
		{
			if(	m_DroppedDown)
				this.m_GridPopUp.Close ();
             if (m_GridPopUp != null)
            {
                m_GridPopUp.Closed -= new System.EventHandler(this.OnPopUpClosed);
            }
  
 		base.Dispose( disposing );
		
		}
		#endregion

		#region Component Designer generated code
		private void InitializeComponent()
		{
			this.m_Grid = new GridView.VGrid();
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
            //
            //m_GridPopUp
            //
            this.m_GridPopUp.Closed += new System.EventHandler(this.OnPopUpClosed);
			// 
			// GridControl
			// 
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Name = "VGridControl";
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
        /// On mouse Click
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
        /// occurs on key down
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
        /// Get the internal Grid
        /// </summary>
        public Grid InternalGrid
        {
            get { return m_Grid; }
        }
        /// <summary>
        /// Get or Set read only property
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

 
		#endregion

        #region Setting
        /// <summary>
        /// ResetData
        /// </summary>
        public void ResetData()
        {
            m_Grid.SetDataBinding(DataSource,dataGrid.MappingName);
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
            m_Grid.ReadOnly = true;// m_ReadOnly;

            m_Grid.CaptionVisible = m_CaptionVisible;
            m_Grid.CaptionText = m_CaptionText;

            //this.m_VisibleWidth =CalcGridWidth();
            resetLayout=false;
		}

        private void SetDimension()
        {
            curPoint = Point.Empty;
            curSize = Size.Empty;
            int width = 3 + this.m_VisibleWidth;
            int CalcHeight = this.CalcGridHeight();
            int height = CalcHeight;

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
                //    curPoint = new Point(dataGrid.Left + (this.Left - (width - this.Width)), dataGrid.Top + this.Bottom + 2);//this.Left - (width - this.Width), this.Bottom + 2);
                //else
                //    curPoint = new Point(dataGrid.Left + this.Left, dataGrid.Top + this.Bottom + 2);
                height = Math.Min(height, Math.Max(0, height - ((curPoint.Y + height) - gridForm.ClientSize.Height)));
            }
            //if (CalcHeight >= height)
            //    width += 40;// Grid.DefaultScrollWidth;
            curSize = new Size(width, height);

        }
        /// <summary>
         /// CalcGridWidth
        /// </summary>
        /// <returns></returns>
 		internal int CalcGridWidth()
		{
			
			int rowHeader=m_Grid.RowHeadersVisible ? m_Grid.RowHeaderWidth :0;
            int width = 2 * m_Grid.PreferredColumnWidth;
            //if (m_DataView != null)//DataSource != null)
            //{
            //    int i = m_DataView.Table.Columns.Count;
            //    width = i * m_Grid.PreferredColumnWidth;
            //}
            //else
            //{
            //    width = this.Width;
            //}
            
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
            int colHeader = m_Grid.ColumnHeadersVisible ? Grid.DefaultColumnHeaderHeight : 0;
            int cnt = m_DataView.Count <= this.m_VisibleRows ? m_DataView.Count : this.m_VisibleRows;
            int height = 3 + rowAdd + captionHeight + colHeader + (cnt * m_Grid.PreferredRowHeight);

            int scrollBottom = 0;
            //if(MaxWidth<=this.m_VisibleWidth)
            //    scrollBottom+=Grid.DefaultScrollWidth;

            //int height=m_Grid.Height;
            height += colHeader + scrollBottom;

            return height;
        }

        private bool CanShow()
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
        /// <summary>
        /// OnGridDataSourceChanged
        /// </summary>
        /// <param name="e"></param>
        protected override void OnGridDataSourceChanged(EventArgs e)
        {
            if (m_DroppedDown && m_GridPopUp != null)
            {
                m_GridPopUp.Close();
                m_DroppedDown = false;
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
                    m_GridPopUp.Close();
                    m_DroppedDown = false;
                    return;
                }
                m_GridPopUp.Top = curScreenPoint.Y + rect.Bottom;//+ BottmSpace;

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

		#region Show

		[Nistec.Win.UseApiElements("ShowWindow")]
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
            //if (!cmdFilter())
            //    return;
            this.m_VisibleWidth = CalcGridWidth();
 
            SetDimension();
            this.m_GridPopUp.ShowPopUp(m_GridPopUp.Handle, curSize,curPoint);
			m_DroppedDown = true;
			this.Invalidate();
		}
  		#endregion

        #region ComboPopUp

        internal class GridPopUp : Nistec.WinForms.Controls.McPopUpBase
        {
            internal Grid dataGrid;
            protected VGridControl mparent = null;
            private bool dispose = false;

            #region Constructors

            public GridPopUp(VGridControl parent)//, Size size)
                : base(parent)
            {
                mparent = parent;
                this.KeyPreview = false;
                this.dataGrid = ((VGridControl)parent).InternalGrid;

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
                dataGrid.AdjustColumns();
    
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