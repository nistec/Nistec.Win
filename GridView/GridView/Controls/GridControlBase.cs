using System;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Nistec.Win32;
using Nistec.WinForms;
using Nistec.GridView; 

using Nistec.Data;

namespace Nistec.GridView
{
 
    /// <summary>
    /// Represents a Grid Control Base 
    /// </summary>
	[ToolboxBitmap (typeof(Grid))]
	[System.ComponentModel.ToolboxItem(false)]
    public abstract class GridControlBase : Nistec.WinForms.McButtonBase
	{
		#region Members
		internal Grid dataGrid;
		private const int buttonWidth=11;
        internal const int MinimumHeight = 75;
        //internal const int BottmSpace = 12;

	    /// <summary>
        /// DroppedDown member
	    /// </summary>
        protected bool m_DroppedDown;
        /// <summary>
        /// DataView member
        /// </summary>
        protected DataView m_DataView;
        /// <summary>
        /// DataSource member
        /// </summary>
        protected object m_DataSource;
        /// <summary>
        /// VisibleRows member
        /// </summary>
        protected int m_VisibleRows;
        /// <summary>
        /// VisibleWidth member
        /// </summary>
        protected int m_VisibleWidth;
        /// <summary>
        /// m_ReadOnly member
        /// </summary>
        protected bool m_ReadOnly;
        /// <summary>
        /// m_AllowAdd member
        /// </summary>
        protected bool m_AllowAdd;
        /// <summary>
        /// CaptionVisible member
        /// </summary>
        protected bool m_CaptionVisible;
        /// <summary>
        /// CaptionText member
        /// </summary>
        protected string m_CaptionText;
        /// <summary>
        /// resetData member
        /// </summary>
        protected bool resetData;
        /// <summary>
        /// resetLayout member
        /// </summary>
        protected bool resetLayout;
        /// <summary>
        /// gridForm member
        /// </summary>
        protected Form gridForm;
        /// <summary>
        /// pointToGrid member
        /// </summary>
        protected bool pointToGrid = false;
        /// <summary>
        /// curPoint member
        /// </summary>
        protected Point curPoint = Point.Empty;
        /// <summary>
        /// curSize member
        /// </summary>
        protected Size curSize = Size.Empty;
        /// <summary>
        /// collapsed member
        /// </summary>
		internal protected static Image collapsed;
        /// <summary>
        /// expaned member
        /// </summary>
		internal protected static Image expaned;
        /// <summary>
        /// expaned member
        /// </summary>
        internal protected IGridColumn gridColumn;
        /// <summary>
        /// curScreenPoint member
        /// </summary>
        internal Point curScreenPoint = Point.Empty;
        /// <summary>
        /// curRow member
        /// </summary>
        internal protected int curRow = -1;
        /// <summary>
        /// initilaized member
        /// </summary>
        protected bool initilaized = false;

		#endregion

		#region Constructors
		static GridControlBase()
		{
            GridControlBase.collapsed = NativeMethods.LoadImage("Nistec.GridView.Images.collapsed.gif");
            GridControlBase.expaned = NativeMethods.LoadImage("Nistec.GridView.Images.expaned.gif");
 		}
        /// <summary>
        /// Initilaizing GridControlBase
        /// </summary>
        public GridControlBase()
		{
			//base.NetReflectedFram("ba7fa38f0b671cbc");
			m_DroppedDown=false;
			m_VisibleRows=10;
			m_VisibleWidth=0;
			m_ReadOnly=true;
            m_AllowAdd = false;
	        m_CaptionVisible = false;
            m_CaptionText = "";

            resetData=true;
            resetLayout=true;

		}

		#endregion

		#region Dispose
        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
		protected override void Dispose( bool disposing )
		{
            if (this.dataGrid != null)
            {
                this.dataGrid.SizeChanged -= new EventHandler(dataGrid_SizeChanged);
                this.dataGrid.DataSourceChanged -= new EventHandler(dataGrid_DataSourceChanged);
            }
   
			base.Dispose( disposing );
		
		}
		#endregion
	
		#region Overrides
        /// <summary>
        /// Get Mc Style Layout
        /// </summary>
		public override IStyleLayout LayoutManager
		{
			get{return dataGrid.LayoutManager as IStyleLayout;} 
		}
        /// <summary>
        /// Occurs OnDropUp
        /// </summary>
        protected virtual void OnDropUp()
		{
			m_DroppedDown = false;
            //m_GridPopUp.DisposePopUp(false);
			Invalidate(false);
		}
        /// <summary>
        /// Do Drop Down
        /// </summary>
		public void DoDropDown()
		{
			base.PerformClick ();
		}
        /// <summary>
        /// Occurs on paint control
        /// </summary>
        /// <param name="e"></param>
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
        /// <summary>
        /// Set the Grid parent of the control
        /// </summary>
        /// <param name="parentGrid"></param>
        public void SetGrid(Grid parentGrid)
        {
            this.dataGrid = (Grid)parentGrid;
            gridForm = parentGrid.FindForm();
            this.dataGrid.SizeChanged += new EventHandler(dataGrid_SizeChanged);
            this.dataGrid.DataSourceChanged += new EventHandler(dataGrid_DataSourceChanged);
            this.dataGrid.Scroll += new EventHandler(dataGrid_Scroll);
            initilaized = true;
        }
         void dataGrid_Scroll(object sender, EventArgs e)
        {
            OnVerticalScrollChanged(e);
        }

        void dataGrid_DataSourceChanged(object sender, EventArgs e)
        {
            OnGridDataSourceChanged(e);
        }

        void dataGrid_SizeChanged(object sender, EventArgs e)
        {
            OnGridSizeChanged(e);
        }
        /// <summary>
        /// OnGridDataSourceChanged
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnGridDataSourceChanged(EventArgs e)
        {

        }
        /// <summary>
        /// OnGridSizeChanged
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnGridSizeChanged(EventArgs e)
        {

        }

        /// <summary>
        /// OnVerticalScrollChanged
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnVerticalScrollChanged(EventArgs e)
        {

        }

        internal void SetGridScreenPoint()
        {
            curScreenPoint = GetGridScreenPoint();
        }
        internal Point GetGridScreenPoint()
        {
            int x = this.dataGrid.Left;
            int y = this.dataGrid.Top;
            Control cur = this.dataGrid;
            while (cur.Parent != null && !(cur.Parent is Form))
            {
                cur = cur.Parent;
                x += cur.Left;
                y += cur.Top;

            }

            //do
            //{
            //    cur = cur.Parent;
            //    x += cur.Left;
            //    y += cur.Top;

            //}
            //while (cur.Parent != null && !(cur.Parent is Form));
            return new Point(x, y);
        }
		#endregion

		#region Properties
        /// <summary>
        /// Get indicating if is Dropped Down
        /// </summary>
        public bool DroppedDown
        {
            get { return m_DroppedDown; }
        }
        /// <summary>
        /// Get indicating if is Initilaized
        /// </summary>
        public bool Initilaized
        {
            get { return initilaized; }
        }
        /// <summary>
        /// Get or Set Point To Grid
        /// </summary>
        public bool PointToGrid
        {
            get { return pointToGrid; }
            set
            {
                pointToGrid = value;
                resetLayout = true;
            }
        }
        /// <summary>
        /// Get or Set is Caption Visible
        /// </summary>
        public bool CaptionVisible
		{
            get { return m_CaptionVisible; }
            set 
            { 
                m_CaptionVisible = value;
                resetLayout = true;
            }
		}
        /// <summary>
        /// Get or Set Caption Text
        /// </summary>
        public string CaptionText
        {
            get { return m_CaptionText; }
            set 
            { 
                m_CaptionText = value;
                resetLayout = true;
            }
        }
        /// <summary>
        /// Get or Set Visible Rows
        /// </summary>
		public int VisibleRows
		{
			get{return m_VisibleRows;}
			set
            {
                m_VisibleRows=value;
                resetLayout = true;
            }
		}
        /// <summary>
        /// Get or Set ReadOnly
        /// </summary>
        public virtual bool ReadOnly
		{
			get{return m_ReadOnly;}
			set
			{
				if(this.ReadOnly!=value)
				{
					m_ReadOnly=value;
                    if (value)
                    {
                        m_AllowAdd = false;
                    }
                    resetLayout = true;
				}
			}
		}


        /// <summary>
        /// Get or Set the control data source
        /// </summary>
        [TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), Description("GridDataSourceDescr"), DefaultValue((string) null), Category("Data"), RefreshProperties(RefreshProperties.Repaint)]
        public virtual object DataSource
		{
			get{return m_DataSource;}
			set
            {
                m_DataSource=value;
                resetData = true;
            }
		}

 

  		#endregion

    }


}