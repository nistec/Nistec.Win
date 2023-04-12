using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Collections;
using System.Drawing.Drawing2D;


using Nistec.WinForms;
using Nistec.Drawing;

namespace Nistec.GridView
{

    /// <summary>
    /// Specifies a column in which each cell contains a button,Link or menu button
    /// </summary>
    public class GridButtonColumn : GridColumnStyle
    {


        #region Members

        private object currentValue;
        private int hotRow;
        private static readonly Size idealControlSize;
         private GridButtonStyle gridButtonStyle;

        private Nistec.WinForms.PopUpItemsCollection items;
        private McPopUp ctlPopUp;
        private ButtonStates state;
        private bool isItems;
        //private bool droppedDown;

        private ImageList m_ImageList;
        private int m_DefaultImage;
        int calcDropDownWidth = 0;
        Nistec.Threading.ThreadTimer timer;
        Rectangle currentRect = Rectangle.Empty;
 
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the Button Clicked
        /// </summary>
        public event ButtonClickEventHandler ButtonClick;

        #endregion

        #region Constructor

        static GridButtonColumn()
        {
            GridButtonColumn.idealControlSize = new Size(50, 16);
        }
        /// <summary>
        /// Initilaized GridButtonColumn
        /// </summary>
        public GridButtonColumn()
        {
            InitColumn();
        }
        /// <summary>
        /// Initilaized GridButtonColumn
        /// </summary>
        /// <param name="prop"></param>
        public GridButtonColumn(PropertyDescriptor prop)
            : base(prop)
        {
            InitColumn();
        }
        /// <summary>
        /// Initilaized GridButtonColumn
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="isDefault"></param>
        public GridButtonColumn(PropertyDescriptor prop, bool isDefault)
            : base(prop, isDefault)
        {
            InitColumn();
        }

        private void InitColumn()
        {
            this.state = ButtonStates.Normal;
            this.isSelected = false;
            this.hotRow = -1;
            this.currentValue = Convert.DBNull;

            base.m_ColumnType = GridColumnType.ButtonColumn;
            base.m_AllowUnBound = true;

            this.isItems = false;
            this.items = new PopUpItemsCollection();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            this.currentValue = null;
            if (timer != null)
            {
                timer.Elapsed -= new System.Timers.ElapsedEventHandler(timer_Elapsed);
                timer = null;
            }

            if (this.ctlPopUp != null)
            {
                ctlPopUp.SelectedItemClick -= new SelectedPopUpItemEventHandler(popup_SelectedItemClick);
                ctlPopUp.DropDownOcurred -= new EventHandler(ctlPopUp_DropDownOcurred);
                ctlPopUp.DropDownClosed -= new EventHandler(ctlPopUp_DropDownClosed);
                ctlPopUp.Dispose();
                ctlPopUp = null;
            }
            base.Dispose(disposing);
        }


        #endregion

        #region internal override
        /// <summary>
        /// Commits changes in the current cell ,When overridden in a derived class, initiates a request to complete an editing procedure. 
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="rowNum"></param>
        /// <returns></returns>
        protected internal override bool Commit(BindManager dataSource, int rowNum)
        {
            this.isSelected = false;
            this.OnCellLeave();
            return true;
        }
        /// <summary>
        /// When overridden in a derived class, initiates a request to interrupt an edit procedure.
        /// </summary>
        /// <param name="rowNum"></param>
        protected internal override void Abort(int rowNum)
        {
            this.isSelected = false;
            this.Invalidate();
        }
        /// <summary>
        /// Notifies a column that it must relinquish the focus to the control it is hosting.
        /// </summary>
        protected internal override void ConcedeFocus()
        {
            base.ConcedeFocus();
            this.isSelected = false;
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
            //base.OnCellEdit();
            //this.isSelected = true;
            //this.editRow = rowNum;
            //if (isBound)
            //{
            //    this.currentValue = this.GetColumnValueAtRow(source, rowNum);
            //}
            //if (!this.ReadOnly)
            //{
            //    state = ButtonStates.Pushed;
            //    this.Invalidate();
            //    OnClick();
            //    //return true;
            //}
            ////base.Invalidate();
        }
        /// <summary>
        /// Enter Null Value
        /// </summary>
        protected internal override void EnterNullValue()
        {
        }

        private Rectangle GetButtonBounds(Rectangle bounds)
        {
            int height = this.GridTableStyle.dataGrid.PreferredRowHeight;
            Rectangle rectangle1 = new Rectangle(bounds.X + 2, bounds.Y + 1, bounds.Width - 4, height - 3);
            return rectangle1;
        }
 
        private Rectangle GetButtonBounds(Rectangle bounds, bool alignToRight)
        {
            return new Rectangle(bounds.X + 2, bounds.Y + 1, bounds.Width - 4, bounds.Height - 2);
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
            this.Paint(g, bounds, source, rowNum, (SolidBrush)new SolidBrush(this.GridTableStyle.dataGrid.BackColor), (SolidBrush)new SolidBrush(this.GridTableStyle.dataGrid.ForeColor), alignToRight);
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
            ButtonStates btnState = ButtonStates.Normal;
            Rectangle drawRect = this.GetButtonBounds(bounds);
            
            //using (Brush brushCol = this.GridTableStyle.dataGrid.SelectionBackBrush)
            //{
                g.FillRectangle(backBrush, bounds);
            //}
            if(state == ButtonStates.Pushed && (this.editRow == rowNum) && !this.IsReadOnly())
            {
                btnState = ButtonStates.Pushed;
            }
            else if ((this.hotRow == rowNum) && !this.IsReadOnly())
            {
                btnState = ButtonStates.MouseOver;
            }


            object val = null;
            string text = "";
            if (isBound)
            {
                val = this.GetColumnValueAtRow(source, rowNum);
                text = val == null ? "" : val.ToString();
            }
            else
            {
                text = m_Text;
            }

            if (gridButtonStyle != GridButtonStyle.Link)
            {
                //Region region1 = g.Clip;
                //g.ExcludeClip(drawRect);
                DrawButtonRect(g, drawRect, btnState, 2);
                //this.GridTableStyle.Grid.LayoutManager.DrawButton(g, drawRect, btnState, false, true);
                //this.GridTableStyle.Grid.LayoutManager.DrawButtonRect(g, drawRect,btnState,2);
                //g.Clip = region1;
            }

            int imageX = 0;
            if (m_ImageList != null)
            {
                PaintImage(g, drawRect, val, alignToRight);
                imageX = this.ImageList.ImageSize.Width - 2;
            }
            if (alignToRight)
                drawRect.Width -= imageX;
            else
                drawRect.X += imageX;


            //Paint text
            if (gridButtonStyle == GridButtonStyle.Link)
            {
                if (btnState == ButtonStates.Pushed)//isPush && state != GridButtonState.Normal)
                {
                    using (Brush linkBrush = new SolidBrush(Color.Red))
                        this.PaintText(g, drawRect, text, backBrush, linkBrush, alignToRight);
                }
                else
                {
                    this.PaintText(g, drawRect, text, backBrush, this.GridTableStyle.dataGrid.LinkBrush, alignToRight);
                }
            }
            else
            {
                //TableStyle:this.PaintText(g, drawRect, text, this.GridTableStyle.BackBrush, this.GridTableStyle.ForeBrush, alignToRight);
                this.PaintText(g, drawRect, text, this.GridTableStyle.dataGrid.BackBrush, this.GridTableStyle.dataGrid.ForeBrush, alignToRight);
            }
        }

        private void DrawButtonRect(Graphics g, Rectangle rect, ButtonStates state, float radius)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            System.Drawing.Drawing2D.GraphicsPath path = Nistec.Drawing.DrawUtils.GetRoundedRect(rect, radius);
            Brush sb = null;
            IStyleLayout layout= this.GridTableStyle.Grid.LayoutManager;

            switch (state)
            {
                case ButtonStates.Pushed:
                    sb = layout.GetBrushSelected();
                    break;
                default:
                    sb = layout.GetBrushGradient(rect, 270f);
                    break;
            }
            g.FillPath(sb, path);
            sb.Dispose();
            using (Pen pen = layout.GetPenButton())// 90f))
            {
                g.DrawPath(pen, path);
            }
        }

        /// <summary>
        /// PaintImage
        /// </summary>
        /// <param name="g"></param>
        /// <param name="bounds"></param>
        /// <param name="val"></param>
        /// <param name="alignToRight"></param>
        protected void PaintImage(Graphics g, Rectangle bounds, object val, bool alignToRight)
        {
            try
            {
                Size imageSize = idealControlSize;
                imageSize = m_ImageList.ImageSize;
                Rectangle rect = new Rectangle(bounds.X, bounds.Y + ((bounds.Height - imageSize.Height) / 2), imageSize.Width, imageSize.Height);
                if (alignToRight)
                {
                    rect = new Rectangle(bounds.X + bounds.Width - (imageSize.Width), bounds.Y + ((bounds.Height - imageSize.Height) / 2), imageSize.Width, imageSize.Height);
                }
                //int val = ((int)(this.GetColumnValueAtRow(source, rowNum)));
                int indx = 0;
                int cnt = m_ImageList.Images.Count;
                if (val != null)
                {
                    indx = Types.ToInt(val, 0);
                }
                if (!(indx >= 0 && indx < cnt))
                {
                    indx = m_DefaultImage;
                }
                if (indx >= 0 && indx < cnt)
                {
                    g.DrawImage(m_ImageList.Images[indx], rect);
                }
            }
            catch //(System.Exception Throw) 
            {
                new Exception("Error In Button Image List");
            }
        }
        /// <summary>
        /// PaintText
        /// </summary>
        /// <param name="g"></param>
        /// <param name="textBounds"></param>
        /// <param name="text"></param>
        /// <param name="backBrush"></param>
        /// <param name="foreBrush"></param>
        /// <param name="alignToRight"></param>
        protected void PaintText(Graphics g, Rectangle textBounds, string text, Brush backBrush, Brush foreBrush, bool alignToRight)
        {
            Rectangle rectangle1 = textBounds;
            using (StringFormat format1 = new StringFormat())
            {
                format1.FormatFlags |= StringFormatFlags.NoWrap;
                if (alignToRight)
                {
                    format1.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
                }
                format1.Alignment = (this.Alignment == HorizontalAlignment.Left) ? StringAlignment.Near : ((this.Alignment == HorizontalAlignment.Center) ? StringAlignment.Center : StringAlignment.Far);

                if (gridButtonStyle == GridButtonStyle.Link)
                {

                    //g.FillRectangle(backBrush, rectangle1);
                    //rectangle1.Offset(0, 2 * this.yMargin);
                    //rectangle1.Height -= 2 * this.yMargin;
                    using (Font font = new System.Drawing.Font(this.GridTableStyle.dataGrid.Font.Name, (float)8.25f, System.Drawing.FontStyle.Underline))
                    {
                        g.DrawString(text, font, foreBrush, (RectangleF)rectangle1, format1);
                    }

                }
                else
                {
                    //format1.Alignment = StringAlignment.Center;
                    //rectangle1.Y -= 2;
                    g.DrawString(text, this.GridTableStyle.dataGrid.Font, foreBrush, (RectangleF)rectangle1, format1);
                }
            }
            //format1.Dispose();
        }

        /// <summary>
        /// Set Column Value At Row
        /// </summary>
        /// <param name="lm"></param>
        /// <param name="row"></param>
        /// <param name="value"></param>
        protected internal override void SetColumnValueAtRow(BindManager lm, int row, object value)
        {
            object obj1 = value;
            this.currentValue = obj1;
            //base.SetColumnValueAtRow(lm, row, obj1);
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
        /// When overridden in a derived class, gets the minimum height of a row
        /// </summary>
        /// <returns></returns>
        protected internal override int GetMinimumHeight()
        {
            return (GridButtonColumn.idealControlSize.Height + 2);
        }
        /// <summary>
        /// When overridden in a derived class, gets the height used for automatically resizing columns.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected internal override int GetPreferredHeight(Graphics g, object value)
        {
            return (GridButtonColumn.idealControlSize.Height + 2);
        }
        /// <summary>
        /// When overridden in a derived class, gets the width and height of the specified value. The width and height are used when the user navigates to GridTable using the GridColumnStyle
        /// </summary>
        /// <param name="g"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected internal override Size GetPreferredSize(Graphics g, object value)
        {
            return new Size(GridButtonColumn.idealControlSize.Width + 2, GridButtonColumn.idealControlSize.Height + 2);
        }
        /// <summary>
        /// Get Current Value
        /// </summary>
        /// <returns></returns>
        protected internal string GetCurrentValue()
        {
            if (m_Text.Length>0)// this.useCaptionText)
            {
                return this.m_Text;
            }
            return this.currentValue == null ? "" : this.currentValue.ToString();

        }
        /// <summary>
        /// Allows the column to free resources when the control it hosts is not needed.
        /// </summary>
        protected internal override void ResetHostControl()
        {
        }

        #endregion

        #region internal events

        //internal override void MouseMove(int rowNum, int x, int y)
        //{
        //    Cursor.Current = Cursors.Hand;
        //    if (this.hotRow != rowNum)
        //    {
        //        hotRow = rowNum;
        //        this.Invalidate(); 
        //        HotTimer();
        //    }
        //}
        private void HotTimer()
        {
            if (timer == null)
            {
                timer = new Nistec.Threading.ThreadTimer(2000);
                timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            }
            if (!timer.Enabled)
            {
                timer.Start();
            }
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            hotRow = -1;
            timer.Stop();
            this.Invalidate(); 
        }

        internal override bool MouseDown(int rowNum, int x, int y)
        {
            //if (this.editRow != rowNum && !this.ReadOnly)
            //    this.GridTableStyle.Grid.Edit(rowNum);
            this.GridTableStyle.Grid.CurrentRowIndex = rowNum;
            base.editRow = rowNum;
            
            int colIndex= this.GridTableStyle.Grid.GridColumns.IndexOf(this);
            currentRect = this.GridTableStyle.Grid.GetCellBounds(rowNum, colIndex);

            this.GridTableStyle.Grid.CurrentCell = new GridCell(rowNum, colIndex);

            if (isBound)
            {
                this.currentValue = this.GridTableStyle.Grid[ rowNum,colIndex];
            }

            if (!this.ReadOnly)
            {
                state = ButtonStates.Pushed;
                base.InvalidateGrid(false);
                //OnClick();
                return true;
            }
            return false;
        }

        internal override void MouseUp(int rowNum,MouseEventArgs e)
        {
            //base.MouseUp(rowNum,e);
            //if(state!=GridButtonState.Pushed)return;
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
            else if (e.Button == MouseButtons.Left)
            {
                if (!this.ReadOnly)
                {
                    OnClick();
                }
                editRow = -1;
                state = ButtonStates.Normal;
                base.InvalidateGrid(true);
            }
            base.MouseUp(rowNum, e);
        }

        /// <summary>
        /// OnCellLeave
        /// </summary>
        protected internal override void OnCellLeave()
        {
            base.OnCellLeave();
            if (state == ButtonStates.Pushed)
            {
                state = ButtonStates.Normal;
                base.InvalidateGrid(false);
            }
        }

        /// <summary>
        /// Raise button click event
        /// </summary>
        /// <param name="value"></param>
        protected virtual void OnButtonClick(object value)
        {
            //state = ButtonStates.Focused;

            if (ButtonClick != null)
            {
                ButtonClickEventArgs e = new ButtonClickEventArgs(base.MappingName, value);
                ButtonClick(this, e);
            }
            else
            {
                this.GridTableStyle.dataGrid.OnButtonClick(this, new ButtonClickEventArgs(base.MappingName, value));
            }

            Application.DoEvents();
            //this.GridTableStyle.dataGrid.Invalidate(this.GridTableStyle.dataGrid.GetCurrentCellBounds());

            //if ((this.GridTableStyle != null) && ((this.GridTableStyle.dataGrid != null) && this.GridTableStyle.dataGrid.CanFocus))
            //{
            //    this.GridTableStyle.dataGrid.Focus();
            //}
            //state = ButtonStates.Normal;
            //this.Invalidate();

        }
        /// <summary>
        /// Occurs when on mouse clicked
        /// </summary>
        protected virtual void OnClick()
        {

            if (items.Count > 0 &&  gridButtonStyle==GridButtonStyle.ButtonMenu)
            {

                if (!this.isItems)
                {
                    if (this.ctlPopUp == null)
                    {
                        this.ctlPopUp = new McPopUp(this.GridTableStyle.dataGrid);
                        this.ctlPopUp.ImageList = this.m_ImageList;
                        this.ctlPopUp.UseOwnerWidth = false;
                        ctlPopUp.SelectedItemClick += new SelectedPopUpItemEventHandler(popup_SelectedItemClick);
                        ctlPopUp.DropDownOcurred += new EventHandler(ctlPopUp_DropDownOcurred);
                        ctlPopUp.DropDownClosed += new EventHandler(ctlPopUp_DropDownClosed);
                    }

                    ctlPopUp.MenuItems.AddRange(MenuItems, this.GridTableStyle.dataGrid, this.ctlPopUp);
                    calcDropDownWidth =(int) this.ctlPopUp.CalcDropDownWidth();
                    this.isItems = true;
                }

                if (/*droppedDown*/this.ctlPopUp.DroppedDown)
                {
                    this.ctlPopUp.ClosePopUp();
                    //this.droppedDown = false;
                    return;
                }
                this.ctlPopUp.DropDownWidth = Math.Max(calcDropDownWidth, this.width);
                Rectangle rect = currentRect;// this.GridTableStyle.dataGrid.GetCellBounds(editRow, this.GridTableStyle.dataGrid.GetColumnIndex(base.nameSite.Name));// this.GridTableStyle.dataGrid.GetCurrentCellBounds();

                Point p = this.GridTableStyle.dataGrid.PointToScreen(new Point(rect.X, rect.Bottom));//this.GridTableStyle.dataGrid.Left + rect.X, this.GridTableStyle.dataGrid.Top + rect.Bottom));
                ctlPopUp.ShowPopUp(p);

            }
            else
            {
                OnButtonClick(this.currentValue);
            }
        }

        private void ctlPopUp_DropDownOcurred(object sender, EventArgs e)
        {
            //this.droppedDown = true;
        }

        private void ctlPopUp_DropDownClosed(object sender, EventArgs e)
        {
            //this.droppedDown = false;
        }

        private void popup_SelectedItemClick(object sender, SelectedPopUpItemEvent e)
        {
            OnButtonClick(e.Item.Text);
        }

        internal override bool KeyPress(int rowNum, Keys keyData)
        {
            if ((this.isSelected && (this.editRow == rowNum)) && !this.IsReadOnly())
            {
                if (keyData == Keys.Insert)
                {
                    state = ButtonStates.Pushed;
                    OnClick();
                }
                //return true;
            }
            return false;
        }


        #endregion

        #region Properties

       /// <summary>
        /// Get Control Current Text
        /// </summary>
        [Browsable(true),DefaultValue("")]
        public override string Text
        {
            get 
            {
                if (isBound)
                {
                    return this.currentValue == null ? "" : currentValue.ToString();
                }
                return m_Text;
            }
            set { this.m_Text = value; }
        }

        /// <summary>
        /// Get button menue items
        /// </summary>
        [Category("Items"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Localizable(true)]
        public PopUpItemsCollection MenuItems
        {
            get
            {
                return items;
            }
        }

        /// <summary>
        /// Get or Set the button style
        /// </summary>
        [DefaultValue(GridButtonStyle.Button)]
        public GridButtonStyle ButtonStyle
        {
            get { return gridButtonStyle; }
            set
            {
                gridButtonStyle = value;
                this.Invalidate();
            }
        }
        /// <summary>
        /// Get the Grid button state
        /// </summary>
        [Browsable(false)]
        public ButtonStates GridButtonState
        {
            get { return this.state; }
        }
 
        /// <summary>
        /// Get or Set button image list
        /// </summary>
        public ImageList ImageList
        {
            get { return m_ImageList; }
            set { m_ImageList = value; }
        }
        /// <summary>
        /// Get or Set the default image
        /// </summary>
        [DefaultValue(0), Description("Default image number from list")]
        public int DefaultImage
        {
            get { return m_DefaultImage; }
            set { m_DefaultImage = value; }
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

    }
}
