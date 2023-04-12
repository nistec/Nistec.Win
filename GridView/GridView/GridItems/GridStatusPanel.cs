using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Nistec.Win;

//using System.Security.Permissions;
//using System.Collections.Specialized;
//using System.Runtime.InteropServices;
//using System.Diagnostics;
//using System.Text;
//using System.Drawing.Design;
//using System.Security;
//using System.Globalization;


namespace Nistec.GridView
{
    /// <summary>
    /// Represent Grid Status Panel class that syncronized with Grid and show summarize panels 
    /// </summary>
    [DesignTimeVisible(false), DefaultProperty("Column"), ToolboxItem(false)]
    public class GridStatusPanel:Component
    {

        #region members

        internal const int MinWidth = 10;
        internal GridStatusBar gridStatusBar;
        internal StatusBarPanel _StatusPanel;
        internal int panelIndex;
        internal string panelName;
 
        private IGridColumn _Column;
        private AggregateMode _AggregateMode;

        private string _MappingName;
        private string _HeaderText;
        private string _Format;
        private int _DecimalPlaces;
        private decimal _Value;
        private int _Width;
        /// <summary>
        /// Width Change event
        /// </summary>
        public event EventHandler WidthChange;
        /// <summary>
        /// Value Change event
        /// </summary>
        public event EventHandler ValueChange;
        #endregion

        #region ctor
        /// <summary>
        /// Initilaized GridStatusPanel
        /// </summary>
        public GridStatusPanel()
        {
            panelName = "";
            _Width = 75;
            _AggregateMode = AggregateMode.Sum;
            _Format = "N";
            _DecimalPlaces = 0;
            panelIndex = -1;
        }
        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (_Column != null)
            {
                UnWireColumn();
                RemovePanel();
            }
            base.Dispose(disposing);
        }
        /// <summary>
        /// Get indicating whether the panel is empty
        /// </summary>
        public bool IsEmpty
        {
            get { return _Column == null;}// string.IsNullOrEmpty(MappingName); }
        }
        /// <summary>
        /// Get empty Grid Status Panel
        /// </summary>
        public static GridStatusPanel Empty
        {
            get { return new GridStatusPanel(); }
        }
        #endregion

        #region properties
        /// <summary>
        /// Get or Set the Column panel
        /// </summary>
        public IGridColumn Column
        {
            get { return _Column; }
            set 
            {
                if (_Column != value)
                {
                    if (value == null)
                    {
                        UnWireColumn();
                        _Column = value;
                        _MappingName = null;
                        //if (DesignMode)
                        //{
                        //    RemovePanel();
                        //}
                    }
                    else
                    {
                        _Column = value;
                        _MappingName = _Column.MappingName;
                        if (string.IsNullOrEmpty(_HeaderText))
                            _HeaderText = _Column.HeaderText;
                        WireColumn();

                        if (DesignMode)
                        {
                            AddPanel();
                        }

                    }
                }
            }
        }

        private void WireColumn()
        {
            _Column.CellValidated += new EventHandler(_Column_CellValidated);
            _Column.WidthChanged += new EventHandler(_Column_WidthChanged);
        }

        private void UnWireColumn()
        {
            _Column.CellValidated -= new EventHandler(_Column_CellValidated);
            _Column.WidthChanged -= new EventHandler(_Column_WidthChanged);
        }

        void _Column_WidthChanged(object sender, EventArgs e)
        {
        }

        void _Column_CellValidated(object sender, EventArgs e)
        {
            if (_Column != null)
            {
                gridStatusBar.SummarizeColumns(this._MappingName);
                //gridStatusBar.Invalidate();
            }
  
        }

        internal int AddPanelInternal(IGridColumn column)//,GridStatusPanel gp)
        {
            _Column = column;
            _MappingName = _Column.MappingName;
            if (string.IsNullOrEmpty(_HeaderText))
                _HeaderText = _Column.HeaderText;
            _StatusPanel =  new StatusBarPanel();
            _StatusPanel.Alignment = HorizontalAlignment.Right;
            _StatusPanel.Width = _Column.Width < MinWidth ? MinWidth : _Column.Width;
            _StatusPanel.Tag = _Column.MappingName;

            //_StatusPanel.ToolTipText = gp.AggregateMode.ToString() + "[" + _MappingName + "]";
            panelIndex = gridStatusBar.Panels.Add(_StatusPanel);
            //panelName = gridStatusBar.Panels[panelIndex].Name;
   
            return panelIndex;
        }

        private void AddPanel()
        {
            //if (panelIndex > -1)
            //{
            //    MessageBox.Show(panelIndex.ToString());
            //    return;
            //}
 
            //if (_PanelKey != null && gridStatusBar.Panels.ContainsKey(_PanelKey))
            //    return;

            //if (gridStatusBar == null)
            //{
            //    foreach (object c in this.Container.Components)
            //    {
            //        if (c is GridStatusBar)
            //        {
            //            gridStatusBar = (GridStatusBar)c;
            //            break;
            //        }
            //    }
            //}
            //else
            //{
            //    foreach (StatusBarPanel sb in gridStatusBar.Panels)
            //    {
            //        if (sb.Tag.Equals(this.MappingName))
            //        {
            //            MessageBox.Show(this.MappingName);
            //            return;
            //        }
            //    }

            //}
            if (_StatusPanel == null && gridStatusBar != null && !string.IsNullOrEmpty( panelName))//panelIndex > -1)
            {
                _StatusPanel = gridStatusBar.Panels[panelName];//panelIndex];
            }
            else if (gridStatusBar != null && _StatusPanel == null)
            {
                _StatusPanel = new StatusBarPanel();
                _StatusPanel.Alignment = HorizontalAlignment.Right;
                _StatusPanel.Width = _Column.Width < MinWidth ? MinWidth : _Column.Width;
                _StatusPanel.Tag = _Column.MappingName;
                if (!gridStatusBar.Panels.Contains(_StatusPanel))
                {
                    if (DesignMode) this.Container.Add(_StatusPanel);
                    panelIndex = gridStatusBar.Panels.Add(_StatusPanel);
                    //MessageBox.Show(_StatusPanel.Name);
                    panelName = gridStatusBar.Panels[panelIndex].Name;
                }
            }

        }
        private void RemovePanel()
        {
            //if (panelIndex == -1)
            //    return;
            if (_StatusPanel != null)
            {
                if (DesignMode) this.Container.Remove(_StatusPanel);
                gridStatusBar.Panels.Remove(_StatusPanel);
            }

            //else if (gridStatusBar != null)
            //{
           
            //    if (!string.IsNullOrEmpty(panelName))//(panelIndex > -1 && panelIndex < gridStatusBar.Panels.)
            //    {
            //        _StatusPanel = gridStatusBar.Panels[panelName];//panelIndex];
            //        //if (DesignMode) this.Container.Remove(_StatusPanel);
            //        gridStatusBar.Panels.Remove(_StatusPanel);
            //    }
            //}
            panelIndex = -1;
            panelName = "";
        }

        //[Category("Behavior"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]//, Localizable(true)]
        //public StatusBarPanel StatusPanel
        //{
        //    get { return _StatusPanel; }
        //    //set { _StatusPanel = value; }
        //}

        /// <summary>
        /// Get or Set Aggregate Mode
        /// </summary>
        public AggregateMode AggregateMode
        {
            get { return _AggregateMode; }
            set { _AggregateMode = value; }
        }
        /// <summary>
        /// Get or set Mapping name
        /// </summary>
        public string MappingName
        {
            get { return _MappingName; }
        }
        //[Browsable(false)]
        //public int PanelIndex
        //{
        //    get { return panelIndex; }
        //    set { panelIndex = value; }
        //}

        /// <summary>
        /// Get or set Panel name
        /// </summary>
        public string PanelName
        {
            get { return panelName; }
            set { panelName = value; }
        }
        /// <summary>
        /// Get or Set Panel header text
        /// </summary>
        public string HeaderText
        {
            get { return _HeaderText; }
            set { _HeaderText = value; }
        }
        /// <summary>
        /// Get or set panel format
        /// </summary>
        public string Format
        {
            get { return _Format; }
            set { _Format = value; }
        }
        /// <summary>
        /// Get or set the panel Decimal Places
        /// </summary>
        public int DecimalPlaces
        {
            get { return _DecimalPlaces; }
            set { _DecimalPlaces = value; }
        }
        /// <summary>
        /// Get panel value
        /// </summary>
        public decimal Value
        {
            get { return _Value; }
            //set { _Value = value; }
        }
        /// <summary>
        /// Get or set panel width
        /// </summary>
        public int Width
        {
            get { return _Width; }
            set 
            {
                if (_Width != value)
                {
                    _Width = value;
                    if (WidthChange != null)
                        WidthChange(this, EventArgs.Empty);
                }
             }
        }
 
        #endregion

        #region methods
        /// <summary>
        /// Summarize Column To String
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public string SummarizeColumnToString(Grid grid)
        {
            if (this.Column.DataType != FieldType.Number)
                return "";

                object res =GridPerform.Compute(grid,this.AggregateMode, this.MappingName);
                _Value = Types.ToDecimal(res, 0);
                if (ValueChange != null)
                    ValueChange(this, EventArgs.Empty);
                string val = _Value.ToString();
                return WinHelp.FormatDecimal(val, Format, DecimalPlaces, 0);
        }

        delegate void SummarizeStatusCallback(Grid grid, bool showHeader);

        /// <summary>
        /// Summarize Status Panel
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="showHeader"></param>
        public void SummarizeStatusPanel(Grid grid, bool showHeader)
        {
            try
            {
                if (_StatusPanel == null)
                {
                    if (gridStatusBar != null && !string.IsNullOrEmpty(panelName))//panelIndex > -1)
                        _StatusPanel = gridStatusBar.Panels[panelName];//panelIndex];
                    else
                        return;
                }

                if (this.gridStatusBar.InvokeRequired)
                {
                    SummarizeStatusCallback d = new SummarizeStatusCallback(SummarizeStatusPanel);
                    this.gridStatusBar.Invoke(d, new object[] { grid, showHeader });
                    //this.gridStatusBar.Invoke(new Nistec.WinForms.AsyncDelegateArgs(SummarizeStatusPanel), new object[] { grid, showHeader });
                }
                else
                {
                    if (showHeader)
                    {
                        _StatusPanel.Text = this.HeaderText + " : " + SummarizeColumnToString(grid);
                    }
                    else
                    {
                        _StatusPanel.Text = SummarizeColumnToString(grid);
                    }
                }
            }
            finally { }
        }
        /// <summary>
        /// Clear Status Panel
        /// </summary>
         public void ClearStatusPanel()
        {
            try
            {
                if (_StatusPanel == null)
                {
                    if (gridStatusBar != null && !string.IsNullOrEmpty( panelName))//panelIndex > -1)
                        _StatusPanel = gridStatusBar.Panels[panelName];//panelIndex];
                    else
                        return;
                }
                _Value = 0;
                _StatusPanel.Text = "";
            }
            finally { }
        }
        #endregion
    }
 
}
