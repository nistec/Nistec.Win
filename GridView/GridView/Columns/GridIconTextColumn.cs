namespace Nistec.GridView
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using Nistec.WinForms;

    /// <summary>
    /// GridIconIndexForRow delegate
    /// </summary>
    /// <param name="row"></param>
    /// <returns></returns>
    public delegate int GridIconIndexForRow(int row);

    /// <summary>
    /// Hosts a TextBox control and Icon in a cell of a GridColumnStyle for editing strings
    /// </summary>
    public class GridIconTextColumn : GridTextColumn
    {
        private GridIconIndexForRow _GridIconIndex;
        private System.Windows.Forms.ImageList _imageList;

        /// <summary>
        /// GridIconTextColumn ctor
        /// </summary>
        public GridIconTextColumn()
        {
        }
        /// <summary>
        ///  GridIconTextColumn ctor
        /// </summary>
        /// <param name="imageList"></param>
        /// <param name="gridIconIndex"></param>
        public GridIconTextColumn(System.Windows.Forms.ImageList imageList, GridIconIndexForRow gridIconIndex)
        {
            this._imageList = imageList;
            this._GridIconIndex = gridIconIndex;
        }
        /// <summary>
        /// Paint
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
            try
            {
                Image image = null;
                if ((this._imageList != null) && (this._GridIconIndex != null))
                {
                    image = this._imageList.Images[this._GridIconIndex(rowNum)];
                }
                if (image == null)
                {
                    base.Paint(g, bounds, source, rowNum, backBrush, foreBrush, alignToRight);
                }
                else
                {
                    Rectangle rect = new Rectangle(bounds.X, bounds.Y, image.Size.Width, bounds.Height);
                    g.FillRectangle(backBrush, rect);
                    rect.Height = image.Size.Height;
                    g.DrawImage(image, rect);
                    bounds.X += rect.Width;
                    bounds.Width -= rect.Width;
                    base.Paint(g, bounds, source, rowNum, backBrush, foreBrush, alignToRight);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Get or Set GridIconIndex delegate
        /// </summary>
        public GridIconIndexForRow GridIconIndex
        {
            get
            {
                return this._GridIconIndex;
            }
            set
            {
                this._GridIconIndex = value;
            }
        }
        /// <summary>
        /// Get or Set ImageList
        /// </summary>
        public System.Windows.Forms.ImageList ImageList
        {
            get
            {
                return this._imageList;
            }
            set
            {
                this._imageList = value;
            }
        }
    }
}

