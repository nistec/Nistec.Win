using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.ComponentModel;

using Nistec.WinForms;

using System.Threading;
using Nistec.Win;

namespace Nistec.GridView
{

    /// <summary>
    /// Specifies a column in which each cell contains a progress bar for representing a Progress value
    /// </summary>
    public class GridProgressColumn : GridColumnStyle
    {

        #region Members
        private object currentValue;
        private Color m_ProgressTextColor;
        private bool m_ShowPrc;
        private int m_ProgressMin;
        private int m_ProgressMax;
        private static readonly Size idealControlSize = new Size(12, 12);
        //private int xMargin;
        //private int yMargin;

        #endregion

        #region Consructor
        /// <summary>
        /// Initilaized GridProgressColumn
        /// </summary>
        public GridProgressColumn()
            : base()
        {
            this.isSelected = false;
            this.currentValue = Convert.DBNull;
      
            m_ProgressTextColor = Color.Yellow;
            m_ProgressMin = 0;
            m_ProgressMax = 100;
            m_ShowPrc = true;
            //m_TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            NullText = "0";
            base.m_DataType = FieldType.Number;
            m_ColumnType = GridColumnType.ProgressColumn;
        }
        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                currentValue = null;
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
            get { return this.currentValue==null? "": currentValue.ToString(); }
            set { this.m_Text = value; }
        }

        //      [DefaultValue(typeof(Color),"Gray")]
        //		public Color BorderColor 
        //		{
        //			get { return m_BorderColor; }
        //			set { m_BorderColor = value; }
        //		}
        //
        //		[DefaultValue(typeof(Color),"Blue")]
        //		public Color ProgressColor 
        //		{
        //			get { return m_ColorBrush1; }
        //			set { m_ColorBrush1 = value; }
        //		}

        /*public Color BrushColor 
        {
            get { return m_ColorBrush2; }
            set { m_ColorBrush2 = value; }
        }*/

        /// <summary>
        /// Get or Set the progress tex color
        /// </summary>
        [DefaultValue(typeof(Color), "Yellow")]
        public Color ProgressTextColor
        {
            get { return m_ProgressTextColor; }
            set { m_ProgressTextColor = value; }
        }
        /// <summary>
        /// Get or Set indicating the text poercent is showen
        /// </summary>
        [DefaultValue(true)]
        public bool ShowTextPercent
        {
            get { return m_ShowPrc; }
            set { m_ShowPrc = value; }
        }
        /// <summary>
        /// Get or mSet the ninimum value 
        /// </summary>
        [DefaultValue(0)]
        public int MinValue
        {
            get { return m_ProgressMin; }
            set
            {
                if (value >= 0 && value < MaxValue)
                    m_ProgressMin = value;
            }
        }
        /// <summary>
        /// Get or Set the maximum value
        /// </summary>
        [DefaultValue(100)]
        public int MaxValue
        {
            get { return m_ProgressMax; }
            set
            {
                if (value >= 0 && value > MinValue)
                    m_ProgressMax = value;
            }
        }
        /// <summary>
        /// Get Progress Value
        /// </summary>
        /// <returns></returns>
        public int GetProgressValue()
        {
            object oVal = this.GetControlvalue();
            return Types.ToInt(oVal, 0);
        }
        /// <summary>
        /// Reset Progress
        /// </summary>
        /// <param name="rowNum"></param>
        public void ResetProgress(int rowNum)
        {
            this.GridTableStyle.dataGrid[rowNum, this.MappingName] = 0;
            this.Invalidate();

            //			int row=this.GridTableStyle.dataGrid.currentRow;
            //
            //			if(row==rowNum)
            //			{
            //				// CM()
            //				this.SetColumnValueAtRow(GridTableStyle.dataGrid.ListManager,row , m_ProgressMin);
            //				this.Invalidate(); 
            //			}
        }
        /// <summary>
        /// Set Progress Value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="rowNum"></param>
        public void SetProgressValue(int value, int rowNum)
        {
            //			int row=this.GridTableStyle.dataGrid.currentRow;
            //			if(row!=rowNum)
            //				this.GridTableStyle.dataGrid.CurrentRowIndex =rowNum;  


            this.GridTableStyle.dataGrid[rowNum, this.MappingName] = value;
            this.Invalidate();
            Application.DoEvents();


            // CM()
            //SetProgressValue(GridTableStyle.dataGrid.ListManager,row,value);
        }

        //private bool start=false;

        private void SetProgressValue(BindManager source, int rowNum, int value)
        {
            object oVal = this.GetColumnValueAtRow(source, rowNum);
            int progressValue = 0;

            //if(Info.IsNumber (oVal.ToString ()))
            progressValue = ((int)oVal);

            //int mtValue = (value * 100) / (m_ProgressMax - m_ProgressMin);
            if (value > progressValue && progressValue < m_ProgressMax)//((mtValue > m_ProgressValue) && (mtValue <= 100) && (mtValue >= 0))
            {
                //m_ProgressValue = mtValue;
                this.SetColumnValueAtRow(source, rowNum, value);
                //this.GridTableStyle.Grid.Update();

                this.GridTableStyle.InvalidateColumn(this);
                this.Invalidate();
                Application.DoEvents();
            }
        }

        #endregion

        #region Override
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
            return (GridProgressColumn.idealControlSize.Height + 2);
        }
        /// <summary>
        /// When overridden in a derived class, gets the height used for automatically resizing columns.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected internal override int GetPreferredHeight(Graphics g, object value)
        {
            return (GridProgressColumn.idealControlSize.Height + 2);
        }
        /// <summary>
        /// When overridden in a derived class, gets the width and height of the specified value. The width and height are used when the user navigates to GridTable using the GridColumnStyle
        /// </summary>
        /// <param name="g"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected internal override Size GetPreferredSize(Graphics g, object value)
        {
            return new Size(GridProgressColumn.idealControlSize.Width + 2, GridProgressColumn.idealControlSize.Height + 2);
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
            this.isSelected = false;
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
            //this.isSelected = true;
            //Grid grid1 = this.GridTableStyle.dataGrid;
            //if (!grid1.Focused)
            //{
            //    grid1.Focus();
            //}
            //if (!readOnly && !this.IsReadOnly())
            //{
                this.editRow = rowNum;
                this.currentValue = this.GetColumnValueAtRow(source, rowNum);
            //}

            //base.Invalidate();
            //base.OnCellEdit();

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
        protected internal override void Paint(Graphics g, Rectangle bounds, BindManager source, int rowNum, Brush backBrush, Brush foreBrush, bool alignToRight)
        {
            int height = this.GridTableStyle.dataGrid.PreferredRowHeight;

            Rectangle rect = new Rectangle(bounds.X + 2, bounds.Y + 2, bounds.Width - 4, height - 4);

            g.FillRectangle(backBrush, bounds);

            int value= Types.ToInt(this.GetColumnValueAtRow(source, rowNum),0);
   
            DrawProgressBar(g,rect,value);

            if (m_ShowPrc)
            {
                this.PaintText(g, rect, value.ToString() + "%");
            }   
            
            //Brush brushCol = (SolidBrush)new SolidBrush(this.GridTableStyle.dataGrid.SelectionBackColor);
            //if (this.GridTableStyle.dataGrid.SelectionType == SelectionType.FullRow && this.isSelected)
            //{
            //    brushCol = this.GridTableStyle.dataGrid.BackBrush;
            //}

            //if (this.isSelected && (source.Position == rowNum))
            //{
            //    g.FillRectangle(brushCol, bounds);
            //}
            //else
            //{
            //    g.FillRectangle(backBrush, bounds);
            //}
      
            //Rectangle drawRect = rect;// new Rectangle(rect.Left + 2,rect.Top + 2, rect.Width-4, rect.Height-4);

            //Rectangle fillRect = drawRect;//new Rectangle(rect.Left + 1,rect.Top + 1, rect.Width-2, rect.Height-2);

            //int maxWidth = (int)fillRect.Width;
            //object oVal = this.GetColumnValueAtRow(source, rowNum);
            //int val = 0;

            //if (Info.IsNumber(oVal.ToString()))
            //    val = (int)oVal;//this.GetColumnValueAtRow( source, rowNum );

            //val = (int)(val * 100) / (m_ProgressMax - m_ProgressMin);
            //double indexWidth = ((double)fillRect.Width) / 100; // determines the width of each index.
            //fillRect.Width = (int)(val * indexWidth);
            ////fillRect.Width = ( int )( ( ( int ) this.GetColumnValueAtRow( source, rowNum ) ) * indexWidth );
            //if (fillRect.Width > maxWidth)
            //{
            //    fillRect.Width = maxWidth;
            //}

            //if (fillRect.Width > 0)
            //{
            //    using (Brush sb = GridTableStyle.dataGrid.LayoutManager.GetBrushCaptionGradient(fillRect, 90f, true))
            //    {
            //        g.FillRectangle(sb, fillRect);
            //    }
            //}

            //using (Pen pen = this.GridTableStyle.dataGrid.LayoutManager.GetPenBorder())
            //{
            //    //rect.Width-=1;
            //    //rect.Height-=1;
            //    g.DrawRectangle(pen, rect);
            //}
            //if (m_ShowPrc)
            //{
            //    //rect.Y+=((height-16)/2);
            //    this.PaintText(g, rect, val.ToString() + "%");
            //}
        }

        private void DrawProgressBar(Graphics g, Rectangle rect, int value)
        {
            IStyleLayout layout = this.GridTableStyle.Grid.LayoutManager;

            Rectangle fillRect = rect;
            int maxWidth = (int)fillRect.Width;
            int val = (int)(value * 100) / (m_ProgressMax - m_ProgressMin);
            double indexWidth = ((double)fillRect.Width) / 100; // determines the width of each index.
            fillRect.Width = (int)(val * indexWidth);
            if (fillRect.Width > maxWidth)
            {
                fillRect.Width = maxWidth;
            }

            if (fillRect.Width > 0)
            {
                using (Brush sb = layout.GetBrushCaptionGradient(fillRect, 90f, true))
                {
                    g.FillRectangle(sb, fillRect);
                }
            }

            using (Pen pen = layout.GetPenBorder())
            {
                g.DrawRectangle(layout.GetPenBorder(), rect);
            }
        }

        /// <summary>
        /// PaintText
        /// </summary>
        /// <param name="g"></param>
        /// <param name="textBounds"></param>
        /// <param name="text"></param>
        protected void PaintText(Graphics g, Rectangle textBounds, string text)
        {
            Rectangle rectangle1 = textBounds;
            StringFormat format1 = new StringFormat();
            format1.Alignment = StringAlignment.Center;
            format1.FormatFlags |= StringFormatFlags.NoWrap;

            //rectangle1.Y -= 2;
            using (SolidBrush sb = new SolidBrush(m_ProgressTextColor))
            {
                g.DrawString(text, GridTableStyle.dataGrid.Font, sb, (RectangleF)rectangle1, format1);
            }

            //g.DrawString(text, this.GridTableStyle.dataGrid.Font, foreBrush, (RectangleF) rectangle1, format1);

            format1.Dispose();
        }
        /// <summary>
        /// Allows the column to free resources when the control it hosts is not needed.
        /// </summary>
        protected internal override void ResetHostControl()
        {
        }
        internal override void MouseUp(int rowNum, MouseEventArgs e)
        {
            base.MouseUp(rowNum, e);
            if (e.Button == MouseButtons.Right)
            {
                if (!this.GridTableStyle.Grid.AllowColumnContextMenu)
                    return;
                GridColumnContexMenu mnu = this.GridTableStyle.Grid._GridColumnContexMenu;
                if (mnu == null)
                {
                    mnu = new GridColumnContexMenu(this.GridTableStyle.Grid);
                }
                mnu.Show(this.GridTableStyle.Grid.PointToScreen(new Point(e.X, e.Y)));
            }
        }

        #endregion

    }

}