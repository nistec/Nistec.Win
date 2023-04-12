
using System;
using System.ComponentModel;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing.Design;

using Nistec.WinForms;
using Nistec.Data.Advanced;

namespace Nistec.GridView
{
    /// <summary>
    /// Specifies a column in which each cell contains a string label for representing a read only string value
    /// </summary>
    public class GridLabelColumn : GridColumnStyle
    {

        #region Members
        private bool m_DrawLabel;
        private Color m_ForeColor;
        private Color m_BackColor;
        private string format;
        private IFormatProvider formatInfo;
        private MethodInfo parseMethod;
        private TypeConverter typeConverter;


        private static readonly Size idealControlSize = new Size(16, 16);

        #endregion

        #region Constructor
        /// <summary>
        /// Initilaized GridLabelColumn
        /// </summary>
        /// <param name="prop"></param>
        public GridLabelColumn(PropertyDescriptor prop)
            : this(prop, null, false)
        {
            InitColumn();
        }
        /// <summary>
        /// Initilaized GridLabelColumn
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="format"></param>
        public GridLabelColumn(PropertyDescriptor prop, string format)
            : this(prop, format, false)
        {
            InitColumn();
            this.Format = format;
        }
        /// <summary>
        /// Initilaized GridLabelColumn
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="format"></param>
        /// <param name="isDefault"></param>
        public GridLabelColumn(PropertyDescriptor prop, string format, bool isDefault)
            : base(prop, isDefault)
        {
            InitColumn();
            m_DrawLabel = isDefault;
            this.Format = format;
        }
        /// <summary>
        /// Initilaized GridLabelColumn
        /// </summary>
        public GridLabelColumn()
            : base()
        {
            InitColumn();
        }

        private void InitColumn()
        {

            //this.xMargin = 2;
            //this.yMargin = 1;
            this.format = null;
            this.formatInfo = null;

            m_ForeColor = Color.Black;
            m_BackColor = Color.White;
            m_DrawLabel = false;

            //this.ControlSize = new Size( 80, 12 );
            m_ColumnType = GridColumnType.LabelColumn;
            base.m_AllowUnBound = false;

        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                format = null;
            }
            base.Dispose(disposing);
        }
        #endregion

        #region override
        /// <summary>
        /// When overridden in a derived class, initiates a request to interrupt an edit procedure.
        /// </summary>
        /// <param name="rowNum"></param>
        protected internal override void Abort(int rowNum)
        {
            this.isSelected = false;
            this.EndEdit();
        }
        /// <summary>
        /// Notifies a column that it must relinquish the focus to the control it is hosting.
        /// </summary>
        protected internal override void ConcedeFocus()
        {
            this.isSelected = false;
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

        //		internal override string GetDisplayText(object value)
        //		{
        //			return this.GetText(value);
        //		}
        /// <summary>
        /// When overridden in a derived class, gets the minimum height of a row
        /// </summary>
        /// <returns></returns>
        protected internal override int GetMinimumHeight()
        {
            return (GridLabelColumn.idealControlSize.Height + 2);
        }
        /// <summary>
        /// When overridden in a derived class, gets the height used for automatically resizing columns.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected internal override int GetPreferredHeight(Graphics g, object value)
        {
            return (GridLabelColumn.idealControlSize.Height + 2);
        }
        /// <summary>
        /// When overridden in a derived class, gets the width and height of the specified value. The width and height are used when the user navigates to GridTable using the GridColumnStyle
        /// </summary>
        /// <param name="g"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected internal override Size GetPreferredSize(Graphics g, object value)
        {
            return new Size(GridLabelColumn.idealControlSize.Width + 2, GridLabelColumn.idealControlSize.Height + 2);
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
            this.isSelected = true;
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
            string text1 = this.GetText(this.GetColumnValueAtRow(source, rowNum));
            this.PaintText(g, bounds, text1, alignToRight);
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
            string text1 = this.GetText(this.GetColumnValueAtRow(source, rowNum));
            this.PaintText(g, bounds, text1, backBrush, foreBrush, alignToRight);
        }
        /// <summary>
        /// PaintText
        /// </summary>
        /// <param name="g"></param>
        /// <param name="bounds"></param>
        /// <param name="text"></param>
        /// <param name="alignToRight"></param>
        protected void PaintText(Graphics g, Rectangle bounds, string text, bool alignToRight)
        {
            //TableStyle:this.PaintText(g, bounds, text, this.GridTableStyle.BackBrush, this.GridTableStyle.ForeBrush, alignToRight);
            this.PaintText(g, bounds, text, this.GridTableStyle.dataGrid.BackBrush, this.GridTableStyle.dataGrid.ForeBrush, alignToRight);
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
        protected virtual void PaintText(Graphics g, Rectangle textBounds, string text, Brush backBrush, Brush foreBrush, bool alignToRight)
        {
            Rectangle rectangle1 = textBounds;
            StringFormat format1 = new StringFormat();
            if (alignToRight)
            {
                format1.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
            }
            format1.Alignment = (this.Alignment == HorizontalAlignment.Left) ? StringAlignment.Near : ((this.Alignment == HorizontalAlignment.Center) ? StringAlignment.Center : StringAlignment.Far);
            //format1.LineAlignment =StringAlignment.Center ;
            format1.FormatFlags |= StringFormatFlags.NoWrap;

            rectangle1.Offset(0, 2 * this.yMargin);
            rectangle1.Height -= 2 * this.yMargin;

            if (!m_DrawLabel)
            {
                if (this.GridTableStyle.dataGrid.SelectionType == SelectionType.FullRow && this.isSelected)
                {
                    backBrush = this.GridTableStyle.dataGrid.BackBrush;
                    foreBrush = this.GridTableStyle.dataGrid.ForeBrush;
                }

                g.FillRectangle(backBrush, textBounds);
                g.DrawString(text, this.GridTableStyle.dataGrid.Font, foreBrush, (RectangleF)rectangle1, format1);
            }
            else
            {
                using (Brush sb1 = new SolidBrush(BackColor),
                           sb2 = new SolidBrush(ForeColor))
                {
                    g.FillRectangle(sb1, textBounds);
                    g.DrawString(text, this.GridTableStyle.dataGrid.Font, sb2, (RectangleF)rectangle1, format1);
                }
            }
            format1.Dispose();
        }

        private string GetText(object value)
        {
            if (value is DBNull)
            {
                return this.NullText;
            }
            if (LookupViewInitilaized)
            {
                return m_LookupView.Keys.GetValue(value);
            }
            if (((this.format != null) && (this.format.Length != 0)) && (value is IFormattable))
            {
                try
                {
                    return ((IFormattable)value).ToString(this.format, this.formatInfo);
                }
                catch (Exception)
                {
                    goto Label_0084;
                }
            }
            if ((this.typeConverter != null) && this.typeConverter.CanConvertTo(typeof(string)))
            {
                return (string)this.typeConverter.ConvertTo(value, typeof(string));
            }
        Label_0084:
            if (value == null)
            {
                return "";
            }
            return value.ToString();
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

        #region property

       /// <summary>
        /// Get Control Current Text
        /// </summary>
        [Browsable(false)]
        public override string Text
        {
            get { return ""; }
            set {  }
        }
        /// <summary>
        /// Get or Set indicate whether to Draw Label colors
        /// </summary>
        [DefaultValue(false)]
        public bool DrawLabel
        {
            get { return m_DrawLabel; }
            set
            {
                m_DrawLabel = value;
                this.Invalidate();
            }
        }

        //	    [DefaultValue(false)]
        //		public new bool ShowSum 
        //		{
        //			get{return m_IsSum;}
        //			set
        //			{
        //				if(m_FormatType ==FieldType.Number)    
        //					m_IsSum = value;
        //				else 
        //					m_IsSum =false;
        //				this.Invalidate (); 
        //			}
        //		}

        /// <summary>
        /// Get or Set back color when drow label is true
        /// </summary>
        [DefaultValue(typeof(Color), "White")]
        public Color BackColor
        {
            get { return m_BackColor; }
            set
            {
                if (value != Color.Transparent)
                    m_BackColor = value;
                this.Invalidate();
            }
        }
        /// <summary>
        /// Get or Set fore color when draw label is true
        /// </summary>
        [DefaultValue(typeof(Color), "Black")]
        public Color ForeColor
        {
            get { return m_ForeColor; }
            set
            {
                m_ForeColor = value;
                this.Invalidate();
            }
        }
        /// <summary>
        /// Get or Set column format
        /// </summary>
        [Editor("System.Windows.Forms.Design.GridColumnStyleFormatEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string Format
        {
            get
            {
                return this.format;
            }
            set
            {
                if (value == null)
                {
                    value = "";
                }
                if ((this.format == null) || !this.format.Equals(value))
                {
                    this.format = value;
                    if (((this.format.Length == 0) && (this.typeConverter != null)) && !this.typeConverter.CanConvertFrom(typeof(string)))
                    {
                        this.ReadOnly = true;
                    }
                    this.Invalidate();
                }
            }
        }
        /// <summary>
        /// Get or Set column format info
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), DefaultValue(null)]
        public IFormatProvider FormatInfo
        {
            get
            {
                return this.formatInfo;
            }
            set
            {
                if ((this.formatInfo == null) || !this.formatInfo.Equals(value))
                {
                    this.formatInfo = value;
                }
            }
        }

        /// <summary>
        /// Set column property descriptor
        /// </summary>
        [DefaultValue((string)null), Description("FormatControlFormatDescr")]
        public override PropertyDescriptor PropertyDescriptor
        {
            set
            {
                base.PropertyDescriptor = value;
                if ((this.PropertyDescriptor != null) && (this.PropertyDescriptor.PropertyType != typeof(object)))
                {
                    this.typeConverter = TypeDescriptor.GetConverter(this.PropertyDescriptor.PropertyType);
                    this.parseMethod = this.PropertyDescriptor.PropertyType.GetMethod("Parse", new Type[] { typeof(string), typeof(IFormatProvider) });
                }
            }
        }

        //		[DefaultValue(FieldType.Text )]
        //		public new FieldType FormatType 
        //		{
        //			get	{return mFormatType;}
        //			set
        //			{
        //				mFormatType=value;
        //				if(value==FieldType.Number)
        //				{
        //					this.TextAlignment=System.Windows.Forms.HorizontalAlignment.Right ;
        //					mIsSum=true;
        //				}
        //				else 
        //					mIsSum=false;
        //				this.Invalidate (); 
        //			} 
        //		}

        #endregion

        private LookupView m_LookupView;
        private bool LookupViewInitilaized = false;
        /// <summary>
        /// Set Lookup View
        /// </summary>
        /// <param name="lookupView"></param>
        public void SetLookupView(LookupView lookupView)
        {
            m_LookupView = lookupView;
            LookupViewInitilaized =lookupView!=null && m_LookupView.Initilaized;
        }

    }

}