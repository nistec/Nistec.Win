using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Permissions ; 
using System.Collections;
using System.Collections.Generic;
using Nistec.Win;

//using Nistec.Collections;

namespace Nistec.GridView
{

    /// <summary>
    /// StatusBarMode
    /// </summary>
	public enum StatusBarMode
	{
        /// <summary>
        /// Hide
        /// </summary>
		Hide=0,
        /// <summary>
        /// Show
        /// </summary>
		Show=1,
        /// <summary>
        /// ShowPanels
        /// </summary>
		ShowPanels=2
	}
    /// <summary>
    /// SummarizeMode
    /// </summary>
    public enum SummarizeMode
    {
        /// <summary>
        /// Auto
        /// </summary>
        Auto = 0,
        /// <summary>
        /// Manual
        /// </summary>
        Manual = 1,
        /// <summary>
        /// All
        /// </summary>
        All = 2
    }

  
	/// <summary>
    /// Summary description for GridStatusBar.
	/// </summary>
    [ToolboxItem(true), ToolboxBitmap(typeof(GridStatusBar), "Images.GridStatusBar.bmp")]
	[Designer(typeof(GridStatusBarDesigner))]
	public class GridStatusBar : Nistec.WinForms.McStatusBar   
	{
		#region Members

		// Fields
        //private bool firstLoad=true;
     	//private StatusBarMode m_StatusBarMode; 
		private GridView.Grid grid;
        GridStatusPanelCollection panelCollection;
        private bool showAllColumns;
        private bool showColumnHeaderText;
        private SummarizeMode _SummarizeMode;
        private int decimalPlaces;
        private bool useAggrigationMode;
        //private List<string> columnsToSum;
        private Nistec.Collections.GenericList<string, GridStatusPanel> columnsToSum;
        
        /// <summary>
        /// Summarize Change event
        /// </summary>
        public event EventHandler SummarizeChange;
 
		#endregion

		#region Constructor
        /// <summary>
        /// Initilaized GridStatusBar
        /// </summary>
		public GridStatusBar()
		{
            base.ShowPanels = true;
            useAggrigationMode = false;
            _SummarizeMode = SummarizeMode.Auto;
			//m_StatusBarMode=StatusBarMode.ShowPanels;
            decimalPlaces = 0;
            showAllColumns = false;
            showColumnHeaderText = false;
            panelCollection = new GridStatusPanelCollection(this);
            //columnsToSum = new List<string>();
            columnsToSum = new Nistec.Collections.GenericList<string, GridStatusPanel>();
            panelCollection.CollectionChanged += new CollectionChangeEventHandler(panelCollection_CollectionChanged);
		}
        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

                if (panelCollection != null)
                {
                    panelCollection.CollectionChanged -= new CollectionChangeEventHandler(panelCollection_CollectionChanged);
                    GridStatusPanelCollection collection1 = this.panelCollection;
        
                    for (int num1 = 0; num1 < collection1.Count; num1++)
                    {
                        collection1[num1].Dispose();
                    }
                }
                if (grid != null)
                {
                    UnWireGrid();
                }
            }
            base.Dispose(disposing);
        }

 
        //public GridStatusBar(GridView.Grid ctl)
        //{
        //    m_StatusBarMode=StatusBarMode.Hide;
        //    grid=ctl;
        //}

        /// <summary>
        /// SetDataGrid
        /// </summary>
        /// <param name="g"></param>
		public void SetDataGrid(GridView.Grid g)
		{
			this.grid = (GridView.Grid)g;
		}

		#endregion

        #region collection event

        void panelCollection_CollectionChanged(object sender, CollectionChangeEventArgs e)
        {
            GridStatusPanel g = e.Element as GridStatusPanel;
            switch (e.Action)
            {
                case CollectionChangeAction.Add:
                    g.WidthChange += new EventHandler(g_WidthChange);
                    
                    //AddPanel(e.Element as GridStatusPanel);
                    break;
                case CollectionChangeAction.Remove:
                    g.WidthChange -= new EventHandler(g_WidthChange);
                    RemovePanel(e.Element as GridStatusPanel);
                    break;
                case CollectionChangeAction.Refresh:
                    //ClearPanels();
                    break;
            }
        }

        //private void AddPanel(GridStatusPanel _Column)
        //{
        //    StatusBarPanel _StatusPanel = new StatusBarPanel();
        //    _StatusPanel.Alignment = HorizontalAlignment.Right;
        //    _StatusPanel.Width = _Column.Width < GridStatusPanel.MinWidth ? GridStatusPanel.MinWidth : _Column.Width;
        //    _StatusPanel.Tag = _Column.MappingName;
        //    if (!this.Panels.Contains(_StatusPanel))
        //    {
        //        if (DesignMode) this.Container.Add(_StatusPanel);
        //        //_Column.PanelIndex = this.Panels.Add(_StatusPanel);
        //        int index = this.Panels.Add(_StatusPanel);
        //        _Column.panelName = this.Panels[index].Name;
        //    }
        //}

        private void RemovePanel(GridStatusPanel _Column)
        {
            StatusBarPanel _StatusPanel = this.Panels[_Column.panelName];
            if (!this.Panels.Contains(_StatusPanel))
            {
                if (DesignMode) this.Container.Remove(_StatusPanel);
                this.Panels.Remove(_StatusPanel);
                //_Column.PanelIndex = -1;
                _Column.panelName= "";
            }
        }

        private void ClearPanels()
        {
            foreach (object o in this.Container.Components)
            {
                if (o is GridStatusPanel)
                    this.Container.Remove(o as Component);
            }
            this.Panels.Clear();
            //panelCollection.Clear();
        }

        #endregion

        /// <summary>
        /// Get ColumnsToSum collection use on runTime only
        /// </summary>
        public IList<string> ColumnsToSum
        {
            get 
            {
                if (columnsToSum == null || columnsToSum.Count == 0)
                    return null;
                return columnsToSum.Keys; 
            }
        }
        /// <summary>
        /// Add Column to ColumnToSum collection on run time
        /// </summary>
        /// <param name="mappingName"></param>
        public void AddColumnToSum(string mappingName)
        {
            if (this.DesignMode)
                return;
            if (GridColumns == null || GridColumns.Count == 0)
                return;
            
            if (!columnsToSum.ContainsKey(mappingName))
            {
                GridStatusPanel panel = GridColumns[mappingName];
                columnsToSum.Add(mappingName, panel);
            }
        }
        /// <summary>
        /// Add Array of Columns to ColumnToSum collection on run time
        /// </summary>
        /// <param name="mappingNames"></param>
        public void AddColumnToSum(string[] mappingNames)
        {
            foreach (string s in mappingNames)
            {
                AddColumnToSum(s);
            }
        }
        /// <summary>
        /// Remove Column from ColumnToSum collection on run time
        /// </summary>
        /// <param name="mappingName"></param>
        public void RemoveColumnToSum(string mappingName)
        {
            if (columnsToSum.ContainsKey(mappingName))
                columnsToSum.Remove(mappingName);
        }
        /// <summary>
        /// Clear All Columns from ColumnToSum collection on run time
        /// </summary>
        public void ClearColumnToSum()
        {
            columnsToSum.Clear();
        }

  

        #region columns

        /// <summary>
        /// Get or Set indicating that column collection has been initilaized
        /// </summary>
        [Browsable(false),EditorBrowsable(EditorBrowsableState.Advanced),DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden)]
        public bool InitilaizeColumns
        {
            get
            {
                return panelCollection != null && panelCollection.Count > 0;
            }
            set
            {
                if (!InitilaizeColumns && value)
                {
                    SummarizeAllColumnsSetting();
                }
                else if (InitilaizeColumns && !value)
                {
                    ClearAllColumns();
                }
            }
        }

        /// <summary>
        /// Get Grid Status Panel Collection
        /// </summary>
        [Category("Behavior"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Localizable(true)]
        public GridStatusPanelCollection GridColumns
        {
            get
            {
                if (panelCollection == null)
                {
                    panelCollection =new GridStatusPanelCollection(this);
                }
                return panelCollection;
            }
        }
 
        /// <summary>
        /// Summarize All ColumnsSetting
        /// </summary>
        internal void SummarizeAllColumnsSetting()
        {
            GridColumns.Clear();
            this.Panels.Clear();
            GridColumnCollection cols = grid.GridColumns;

            foreach (GridColumnStyle col in cols)
            {
                if (col.IsVisibleInternal)
                {
                    GridStatusPanel gp = new GridStatusPanel();
                    gp.gridStatusBar = this;
                    gp.DecimalPlaces = decimalPlaces;
                    gp.AddPanelInternal(col);//,gp);
                    GridColumns.Add(gp);
                }
            }
            _SummarizeMode = SummarizeMode.All;
        }
        /// <summary>
        /// Summarize Columns
        /// </summary>
        public void SummarizeColumns()
        {
            if (grid == null)
                return;
            bool flag = false;
            if (columnsToSum.Count > 0)
            {
                foreach (GridStatusPanel g in columnsToSum.Values)
                    g.SummarizeStatusPanel(grid, showColumnHeaderText);
                flag = true;
            }
            else
            {
                GridStatusPanelCollection col = GridColumns;
                flag = col.Count > 0;

                foreach (GridStatusPanel g in col)
                {
                    if (useAggrigationMode)
                    {
                        if (g.Column.AggregateMode != AggregateMode.None)
                        {
                            g.SummarizeStatusPanel(grid, showColumnHeaderText);
                        }
                    }
                    else if (g.Column.DataType == FieldType.Number)
                    {
                        g.SummarizeStatusPanel(grid, showColumnHeaderText);
                    }
                }
            }
            if (flag)
            {
                this.Invalidate();
                OnSummarizeChanged(EventArgs.Empty);
            }
            //_SummarizeMode = SummarizeMode.Auto;
        }
        /// <summary>
        /// Summarize Columns
        /// </summary>
        /// <param name="index"></param>
        internal void SummarizeColumns(int index)
        {
            int count = GridColumns.Count;
            if (count < 1 || index < 0 || index > count - 1)
            {
                return;
            }

            GridColumns[index].SummarizeStatusPanel(grid, showColumnHeaderText);
            this.Invalidate();
            OnSummarizeChanged(EventArgs.Empty);
            //_SummarizeMode = SummarizeMode.Manual;
        }
        /// <summary>
        /// Summarize Columns
        /// </summary>
        /// <param name="mappingName"></param>
        internal void SummarizeColumns(string mappingName)
        {
            if( GridColumns.Count < 1)
            {
                return;
            }
            GridColumns[mappingName].SummarizeStatusPanel(grid, showColumnHeaderText);
            //AddColumnToSum(mappingName);
            this.Invalidate();
            OnSummarizeChanged(EventArgs.Empty);
            //_SummarizeMode = SummarizeMode.Manual;
        }

        /// <summary>
        /// Summarize Columns
        /// </summary>
        /// <param name="mappingNames"></param>
        internal void SummarizeColumns(string[] mappingNames)
        {
            if (GridColumns.Count < 1)
            {
                return;
            }
            foreach (string s in mappingNames)
            {
                GridColumns[s].SummarizeStatusPanel(grid, showColumnHeaderText);
                //AddColumnToSum(s);
            }
            this.Invalidate();
            OnSummarizeChanged(EventArgs.Empty);

        }

        /// <summary>
        /// Summarize Clear
        /// </summary>
        public void SummarizeClear()
        {
            foreach (GridStatusPanel g in GridColumns)
            {
                g.ClearStatusPanel();
            }
            this.Invalidate();
            OnSummarizeChanged(EventArgs.Empty);
        }
        /// <summary>
        /// Remove All Columns Summarize
        /// </summary>
        public void ClearAllColumns()
        {
            GridColumns.Clear();
            this.Panels.Clear();
            //columnsToSum.Clear();
        }
 
        /// <summary>
        /// Resize Columns
        /// </summary>
        internal void ResizeColumns()
        {
            if (grid == null)
                return;
            if (_SummarizeMode != SummarizeMode.All)
                return;
            foreach (GridStatusPanel g in GridColumns)
            {
                GridColumnStyle style = grid.GridColumns[g.Column.MappingName];
                if (style.IsVisibleInternal)
                {
                    g.Column.Width = style.Width; ;

                    this.Panels[g.panelIndex].Width = style.Width < GridStatusPanel.MinWidth ? GridStatusPanel.MinWidth : style.Width;
                }
            }
            this.Invalidate();
        }
        /// <summary>
        /// WireColumns
        /// </summary>
        internal void WireColumns()
        {
            foreach (GridStatusPanel g in GridColumns)
            {
                g.WidthChange += new EventHandler(g_WidthChange);
            }
        }
        /// <summary>
        /// UnWireColumns
        /// </summary>
        internal void UnWireColumns()
        {
            foreach (GridStatusPanel g in GridColumns)
            {
                g.WidthChange -= new EventHandler(g_WidthChange);
            }
        }
        void g_WidthChange(object sender, EventArgs e)
        {
            GridStatusPanel pnl=(GridStatusPanel)sender;
            //int index = pnl.panelIndex;
            //if (index > -1 && index < panelCollection.Count)
            //{
            //    this.Panels[index].Width = pnl.Width;
            //}
            string panelName = pnl.panelName;
            if (!string.IsNullOrEmpty(panelName))
            {
                StatusBarPanel p = this.Panels[panelName];
                if(p!=null)
                p.Width = pnl.Width;

            }
        }

        #endregion

        #region override methods

        /// <summary>
        /// Raise Summarize Change event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnSummarizeChanged(EventArgs e)
        {
            if (SummarizeChange != null)
                SummarizeChange(this, EventArgs.Empty);
            else if(grid !=null)
                grid.OnSummarizeChange(this, EventArgs.Empty);
        }

        #endregion

        #region private methods

        void grid_ColumnResize(object sender, ColumnResizeEventArgs e)
        {
            ResizeColumns();
        }

        private void WireGrid()
        {
            grid.ColumnResize += new ColumnResizeHandler(grid_ColumnResize);
            grid.DataSourceChanged += new EventHandler(grid_DataSourceChanged);
            grid.FilterChanged += new EventHandler(grid_FilterChanged);
            grid.Scroll += new EventHandler(grid_Scroll);
        }

        private void UnWireGrid()
        {
            grid.ColumnResize -= new ColumnResizeHandler(grid_ColumnResize);
            grid.DataSourceChanged -= new EventHandler(grid_DataSourceChanged);
            grid.FilterChanged -= new EventHandler(grid_FilterChanged);
            grid.Scroll -= new EventHandler(grid_Scroll);
        }

        void grid_FilterChanged(object sender, EventArgs e)
        {
            SummarizeColumns();
        }

        void grid_DataSourceChanged(object sender, EventArgs e)
        {
            //if (!firstLoad)
            //{
            //    SummarizeClear();
            //    ClearAllColumns();
            //}
            if (showAllColumns)
            {
                SummarizeAllColumnsSetting();
            }
            //else
            //{
                SummarizeColumns();
            //}
            //firstLoad = false;
        }

        private int HScrollPosition = 0;
        private int firstVisiblePanel = 0;
        private int neagtivePosition = 0;

        void grid_Scroll(object sender, EventArgs e)
        {
            if(base.ShowPanels)
            {
                base.OwnerDrow = true;
                this.HScrollPosition = grid.HorizontalOffset;
                this.neagtivePosition = grid.negOffset;
                this.firstVisiblePanel = grid.firstVisibleCol;
                this.Invalidate();
            }
        }

        /// <summary>
        /// PaintPanels
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="rtl"></param>
        protected override void PaintPanels(Graphics g, Rectangle rect, RightToLeft rtl)
        {
            base.PaintPanels(g, rect, rtl);

            Brush sbFlat = LayoutManager.GetBrushFlat();
            Brush sbText = LayoutManager.GetBrushText();
            Pen pen = LayoutManager.GetPenBorder();

            float xp = 0;
            SizeF sf;

            int ctlWidth = this.Width;
            int startPosition = StartPanelPosition;
            if (rtl == RightToLeft.Yes)
            {
                startPosition = ctlWidth - StartPanelPosition;
            }
            int lf = startPosition;
            int calclf = StartPanelPosition;
            Rectangle rectB;
            float yp;

            ContentAlignment ca = ContentAlignment.MiddleCenter;
            int index = -1;
            int width = 0;

            foreach (System.Windows.Forms.StatusBarPanel p in this.Panels)
            {
                index++;
              
                if (index < firstVisiblePanel)
                {
                    continue;
                }
                width = p.Width;
                if (rtl == RightToLeft.Yes)
                {
                    rectB = new Rectangle(lf - (p.Width + 1), 2, p.Width - 2, this.Height - 4);

                    if (index == firstVisiblePanel)
                    {
                        rectB = new Rectangle(lf - ((p.Width - HScrollPosition) + 1), 2, p.Width - (HScrollPosition + 2), this.Height - 4);
                        width = p.Width - HScrollPosition;
                        if (rectB.Width < 0)
                            continue;
                    }
                    if (rectB.X < 0)
                    {
                        calclf += p.Width;
                        continue;
                    }
                }
                else
                {
                    rectB = new Rectangle(lf + 1, 2, p.Width - 2, this.Height - 4);

                    if (index == firstVisiblePanel)
                    {
                        rectB = new Rectangle(lf + 1, 2, p.Width - (HScrollPosition +2), this.Height - 4);
                        width = p.Width - HScrollPosition ;
                        if (rectB.Width < 0)
                            continue;
                    }
                    if (lf + rectB.Width > ctlWidth)
                    {
                        //rectB.Width = ctlWidth - (lf + rectB.Width);
                        //if (rectB.Width < 0)
                        //    continue;
                        calclf += p.Width;
                        continue;
                    }
                }
                g.FillRectangle(sbFlat, rectB);
                xp = 0;
                sf = g.MeasureString(p.Text, this.Font);

                switch (p.Alignment)
                {
                    case System.Windows.Forms.HorizontalAlignment.Left:
                        ca = ContentAlignment.MiddleLeft;
                        xp = (float)rectB.X;
                        break;
                    case System.Windows.Forms.HorizontalAlignment.Right:
                        ca = ContentAlignment.MiddleRight;
                        xp = (float)rectB.X + (float)rectB.Width - sf.Width;
                        break;
                    case System.Windows.Forms.HorizontalAlignment.Center:
                        ca = ContentAlignment.MiddleCenter;
                        xp = (float)rectB.X + (((float)rectB.Width - sf.Width) / 2);
                        break;
                }
                yp = (float)rectB.Y + (((float)rectB.Height - sf.Height) / 2);

                LayoutManager.DrawString(g, rectB, ca, p.Text, this.Font, rtl, true);

                g.DrawRectangle(pen, rectB);

                if (rtl == RightToLeft.Yes)
                {
                    lf -= width;// p.Width;
                }
                else
                {
                    lf += width;// p.Width;
                }
            }

            sbFlat.Dispose();
            sbText.Dispose();
            pen.Dispose();

        }

        #endregion

        #region Properties
        /// <summary>
        /// Get or set Grid
        /// </summary>
        public GridView.Grid Grid
        {
            get { return this.grid; }
            set 
            {
                if (this.grid != value)
                {
                    this.grid = value;

                    if (grid == null)
                    {
                        UnWireGrid();
                    }
                    else
                    {
                        base.StartPanelPosition = grid.RowHeadersVisible ? grid.RowHeaderWidth : 0;
                        WireGrid();
                    }
                }
            }
        }

  
        /// <summary>
        /// Get or Set Show Columns
        /// </summary>
        [DefaultValue(false)]
        public bool ShowColumns
        {
            get { return this.ShowPanels; }
            set 
            { 
                this.ShowPanels = value;
                if (value)
                {
                    base.OwnerDrow = true;
                }
            }
        }
        /// <summary>
        /// et or Set Decimal Places
        /// </summary>
        [DefaultValue(0)]
        public int DecimalPlaces
        {
            get { return this.decimalPlaces; }
            set { this.decimalPlaces = value; }
        }
        /// <summary>
        /// Get or Set Show Column Header Text
        /// </summary>
        [DefaultValue(false)]
        public bool ShowColumnHeaderText
        {
            get { return this.showColumnHeaderText; }
            set { this.showColumnHeaderText = value; }
        }
        /// <summary>
        /// Get or Set indicating to Use Aggrigation Mode
        /// </summary>
        [DefaultValue(false)]
        public bool UseAggrigationMode
        {
            get { return this.useAggrigationMode; }
            set { this.useAggrigationMode = value; }
        }
        /// <summary>
        /// Get or Set TabStop
        /// </summary>
		[Browsable(false)]   
		private new bool TabStop 
		{
			get{return base.TabStop;}
			set{base.TabStop =value;}
		}

        //[Category("Behavior"),DefaultValue(StatusBarMode.Hide)]
        //public StatusBarMode StatusBarMode 
        //{
        //    get{return m_StatusBarMode;}
        //    set
        //    {
        //        if(m_StatusBarMode !=value)
        //        {
        //            m_StatusBarMode =value;
        //            this.Visible= value !=StatusBarMode.Hide;
        //            this.ShowPanels=value==StatusBarMode.ShowPanels;
        //            this.Invalidate();
        //        }
        //    }
        //}

        /// <summary>
        /// Get Mc Style Layout
        /// </summary>
		public override Nistec.WinForms.IStyleLayout LayoutManager
		{
			get
			{
				if(grid!=null)
				   return grid.LayoutManager;
				return base.LayoutManager;
			}
		}

		#endregion

	}

	#region GridStatusBar Desiner

	//[Designer(typeof(McBaseDesigner))]

	internal class GridStatusBarDesigner : System.Windows.Forms.Design.ControlDesigner
	{
		public GridStatusBarDesigner(){}

		protected override void PreFilterProperties(IDictionary properties)
		{
			//base.PreFilterProperties (properties);
			properties.Remove("TabStop");
		}

		protected override void PreFilterAttributes(IDictionary attributes)
		{
			base.PreFilterAttributes (attributes);
	
		}

		protected override void PostFilterProperties(IDictionary Properties)
		{
			Properties.Remove("TabStop");
			Properties.Remove("ForeColor");
			Properties.Remove("BackColor");
			Properties.Remove("AutoScroll");
			Properties.Remove("AutoScrollMargin");
			Properties.Remove("AutoScrollMinSize");
			Properties.Remove("BackgroundImage");
			Properties.Remove("Image");
			Properties.Remove("ImageAlign");
			Properties.Remove("ImageIndex");
			Properties.Remove("ImageList");
			Properties.Remove("AllowDrop");
			Properties.Remove("ContextMenu");
			Properties.Remove("FlatStyle");
			Properties.Remove("Text");
			Properties.Remove("TextAlign");

		}
	}
	#endregion

}
