using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using System;
using System.ComponentModel;


using Nistec.WinForms;

namespace Nistec.GridView
{

   
    /// <summary>
    /// Specifies a column in which each cell contains a image for representing a image value
    /// </summary>
    public class GridIconColumn : GridColumnStyle
    {

        #region Members

        private ImageList m_IconList;
        private int m_DefaultIcon;
        private static readonly Size idealControlSize = new Size(16, 16);
        #endregion

        #region Constructor
        /// <summary>
        /// Initilaized  GridIconColumn
        /// </summary>
        public GridIconColumn()
        {
            //this.xMargin = 2;
            //this.yMargin = 1;
            m_IconList = new ImageList();
            m_DefaultIcon = 0;
            this.Width = 20;//this.GetPreferredSize(null, null).Width;
            m_ColumnType = GridColumnType.IconColumn;
            base.m_AllowUnBound = true;


        }
        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (m_IconList != null)
                {
                    this.m_IconList.Dispose();
                    m_IconList = null;
                }
            }
            base.Dispose(disposing);
        }
        #endregion

        #region Property

  
        /// <summary>
        /// Get Control Current Text
        /// </summary>
        [Browsable(false)]
        public override string Text
        {
            get { return ""; }
            set { }
        }
        /// <summary>
        /// Get or set Icon List
        /// </summary>
        public ImageList IconList
        {
            get { return m_IconList; }
            set { m_IconList = value; }
        }

        /// <summary>
        /// Get or Set Default icon number from list
        /// </summary>
        [DefaultValue(0), Description("Default icon number from list")]
        public int DefaultIcon
        {
            get { return m_DefaultIcon; }
            set { m_DefaultIcon = value; }
        }
        /// <summary>
        /// Get or Set indicating the column is bound
        /// </summary>
        [DefaultValue(true)]
        public new bool IsBound/*bound*/
        {
            get { return isBound; }
            set
            {
                if (isBound != value)
                {
                    if (!isBound)
                    {
                        base.MappingName = "";
                    }
                    isBound = value;
                    //if (!isBound)
                    //{
                    //    base.MappingName = "UnBound" +base.GetHashCode().ToString();
                    //} 
                }
            }
        }
        #endregion

        #region overrides
        /// <summary>
        /// When overridden in a derived class, initiates a request to interrupt an edit procedure.
        /// </summary>
        /// <param name="rowNum"></param>
        protected internal override void Abort(int rowNum)
        {
            this.EndEdit();
        }
        /// <summary>
        /// Notifies a column that it must relinquish the focus to the control it is hosting.
        /// </summary>
        protected internal override void ConcedeFocus()
        {
        }
        /// <summary>
        /// EndEdit
        /// </summary>
        protected void EndEdit()
        {
        }
        /// <summary>
        /// EnterNullValue
        /// </summary>
        protected internal override void EnterNullValue()
        {
        }
        /// <summary>
        /// When overridden in a derived class, gets the minimum height of a row
        /// </summary>
        /// <returns></returns>
        protected internal override int GetMinimumHeight()
        {
            return (GridIconColumn.idealControlSize.Height + 2);
        }
        /// <summary>
        /// When overridden in a derived class, gets the height used for automatically resizing columns.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected internal override int GetPreferredHeight(Graphics g, object value)
        {
            return (GridIconColumn.idealControlSize.Height + 2);
        }
        /// <summary>
        /// When overridden in a derived class, gets the width and height of the specified value. The width and height are used when the user navigates to GridTable using the GridColumnStyle
        /// </summary>
        /// <param name="g"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected internal override Size GetPreferredSize(Graphics g, object value)
        {
            return new Size(GridIconColumn.idealControlSize.Width + 2, GridIconColumn.idealControlSize.Height + 2);
        }
        /// <summary>
        /// Set Grid In Column
        /// </summary>
        /// <param name="value"></param>
        protected override void SetGridInColumn(Grid value)
        {
            base.SetGridInColumn(value);
        }
        /// <summary>
        /// Commits changes in the current cell ,When overridden in a derived class, initiates a request to complete an editing procedure. 
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="rowNum"></param>
        /// <returns></returns>
        protected internal override bool Commit(BindManager dataSource, int rowNum)
        {
            return true;
        }
        /// <summary>
        /// Overloaded. Prepares the cell for editing a value.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="rowNum"></param>
        /// <param name="bounds"></param>
        /// <param name="readOnly"></param>
        /// <param name="instantText"></param>
        /// <param name="cellIsVisible"></param>
        protected internal override void Edit(BindManager source, int rowNum, Rectangle bounds, bool readOnly, string instantText, bool cellIsVisible)
        {
            this.editRow = rowNum;
           // base.m_Text = this.GetColumnValueAtRow(source, rowNum);

        }
        /// <summary>
        /// Overloaded. When overridden in a derived class, paints the column in a Grid control. 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="bounds"></param>
        /// <param name="source"></param>
        /// <param name="rowNum"></param>
        protected internal override void Paint(Graphics g, Rectangle bounds, BindManager source, int rowNum)
        {
            this.Paint(g, bounds, source, rowNum, false);
        }
        /// <summary>
        /// Overloaded. When overridden in a derived class, paints the column in a Grid control. 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="bounds"></param>
        /// <param name="source"></param>
        /// <param name="rowNum"></param>
        /// <param name="alignToRight"></param>
        protected internal override void Paint(Graphics g, Rectangle bounds, BindManager source, int rowNum, bool alignToRight)
        {
            this.Paint(g, bounds, source, rowNum, GridTableStyle.dataGrid.BackBrush, GridTableStyle.dataGrid.ForeBrush, alignToRight);
        }
        /// <summary>
        /// Overloaded. When overridden in a derived class, paints the column in a Grid control. 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="bounds"></param>
        /// <param name="source"></param>
        /// <param name="rowNum"></param>
        /// <param name="backBrush"></param>
        /// <param name="foreBrush"></param>
        /// <param name="alignToRight"></param>
        protected internal override void Paint(System.Drawing.Graphics g, System.Drawing.Rectangle bounds, BindManager source, int rowNum, System.Drawing.Brush backBrush, System.Drawing.Brush foreBrush, bool alignToRight)
        {
            if (m_IconList == null) return;

            try
            {
                g.FillRectangle(backBrush, bounds);

                Size imageSize = idealControlSize;
                imageSize = m_IconList.ImageSize;
                Rectangle rect = new Rectangle(bounds.X + ((bounds.Width - imageSize.Width) / 2) + 1, bounds.Y + 1, imageSize.Width, imageSize.Height);

                //Rectangle controlBounds =rectangle1;// this.GetCellBounds(bounds);
                //Rectangle fillRect = new Rectangle((controlBounds.X + 2), (controlBounds.Y + 2), (controlBounds.Width - 2), (controlBounds.Height - 2));

                int val = m_DefaultIcon;

                if (isBound)/*bound*/
                {
                    val = ((int)(this.GetColumnValueAtRow(source, rowNum)));
                }
                int cnt = m_IconList.Images.Count;

                if (val >= 0 && val < cnt)
                {
                    g.DrawImage(m_IconList.Images[val], rect);
                    // g.DrawIcon(mIcon, fillRect)
                }
            }
            catch //(System.Exception Throw) 
            {
                new Exception("Error In Icon List");
            }
        }
        /// <summary>
        /// Allows the column to free resources when the control it hosts is not needed.
        /// </summary>
        protected internal override void ResetHostControl()
        {
        }

        //		protected virtual Rectangle GetCellBounds( Rectangle cellBounds ) 
        //		{
        //			return new Rectangle( 
        //				cellBounds.X + xMargin , 
        //				cellBounds.Y + yMargin, 
        //				cellBounds.Width-(xMargin*2 ),
        //				cellBounds.Height-(yMargin*2 ) );
        //		}
        #endregion
    }
}

