
using System;
using System.ComponentModel;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using Nistec.WinForms;

namespace Nistec.GridView
{
    /// <summary>
    /// Grid Enum Column using by Multi box and VGrid control
    /// </summary>
    public class GridEnumColumn : GridColumnStyle 
	{

		//private MultiComboTypes fieldType= MultiComboTypes.Custom;
		
		#region Constructor
        /// <summary>
        /// Initilaized GridEnumColumn
        /// </summary>
		public GridEnumColumn() : base() 
		{
			this.Width =0;
            this.Visible = false;
            m_ColumnType = GridColumnType.None;
            base.m_AllowUnBound = false;

		}
		#endregion
		
		#region Properties

        //[DefaultValue(MultiComboTypes.Text)]
        //public MultiComboTypes FieldTypes 
        //{
        //    get { return fieldType; }
        //    set 
        //    {
        //        fieldType = value;
        //        this.Invalidate();
        //    }
        //}
		#endregion

        #region methots

        internal MultiType CurrentType()
        {
            try
            {
                object val = base.GridTableStyle.dataGrid[base.MappingName];
                if (val != null)
                {
                    if (WinHelp.IsNumeric(val))
                    {
                        return (MultiType)(int)Types.ToInt(val, (int)MultiType.Text);
                    }
                    else
                    {
                        return (MultiType)Enum.Parse(typeof(MultiType), val.ToString(), true);
                    }
               }
            }
            catch {}

            return MultiType.Text;

        }

        #endregion

        #region override
        /// <summary>
        /// Overloaded. Prepares the cell for editing a value.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="rowNum"></param>
        /// <param name="bounds"></param>
        /// <param name="readOnly"></param>
        protected internal override void Edit(BindManager source, int rowNum, Rectangle bounds, bool readOnly)
        {
            base.Edit(source, rowNum, bounds, readOnly);
        }
        /// <summary>
        /// Overloaded. Prepares the cell for editing a value.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="rowNum"></param>
        /// <param name="bounds"></param>
        /// <param name="readOnly"></param>
        /// <param name="instantText"></param>
        protected internal override void Edit(BindManager source, int rowNum, Rectangle bounds, bool readOnly, string instantText)
        {
            base.Edit(source, rowNum, bounds, readOnly, instantText);
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
            //throw new Exception("The method or operation is not implemented.");
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
            //throw new Exception("The method or operation is not implemented.");
        }
        /// <summary>
        /// When overridden in a derived class, gets the minimum height of a row
        /// </summary>
        /// <returns></returns>
        protected internal override int GetMinimumHeight()
        {
            return 0;
            //throw new Exception("The method or operation is not implemented.");
        }
        /// <summary>
        /// When overridden in a derived class, gets the height used for automatically resizing columns.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected internal override int GetPreferredHeight(Graphics g, object value)
        {
            return Grid.DefaultRowHeight;
            //throw new Exception("The method or operation is not implemented.");
        }
        /// <summary>
        /// When overridden in a derived class, gets the width and height of the specified value. The width and height are used when the user navigates to GridTable using the GridColumnStyle
        /// </summary>
        /// <param name="g"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected internal override Size GetPreferredSize(Graphics g, object value)
        {
            return Size.Empty;
            //throw new Exception("The method or operation is not implemented.");
        }
        /// <summary>
        /// When overridden in a derived class, initiates a request to interrupt an edit procedure.
        /// </summary>
        /// <param name="rowNum"></param>
        protected internal override void Abort(int rowNum)
        {
            //throw new Exception("The method or operation is not implemented.");
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
            //throw new Exception("The method or operation is not implemented.");
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
            //throw new Exception("The method or operation is not implemented.");
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
            base.Paint(g, bounds, source, rowNum, backBrush, foreBrush, alignToRight);
        }
        /// <summary>
        /// Get Control Current Text
        /// </summary>
        [Browsable(false)]
        public override string Text
        {
            get { return (string)CurrentType().ToString(); }
            //throw new Exception("The method or operation is not implemented.");
            set {  }
        }
        /// <summary>
        /// Allows the column to free resources when the control it hosts is not needed.
        /// </summary>
        protected internal override void ResetHostControl()
        {
         }

        #endregion


    }

}	