using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Nistec.Win;

namespace Nistec.GridView
{
    /// <summary>
    /// Provides an editing notification interface
    /// </summary>
    public interface IGridColumnEditingNotifyService
    {
        /// <summary>
        /// Informs the Grid that the user has begun editing the column
        /// </summary>
        /// <param name="editingControl"></param>
        void ColumnStartedEditing(Control editingControl);

    }

    /// <summary>
    /// IGridColumn
    /// </summary>
    public interface IGridColumn
    {
        /// <summary>
        /// Get or Set HeaderText
        /// </summary>
        string HeaderText { get;set; }
        /// <summary>
        /// Get or Set MappingName
        /// </summary>
        string MappingName { get;set; }
        /// <summary>
        /// Get or Set Width
        /// </summary>
        int Width { get;set; }
        /// <summary>
        /// Get or Set Visible
        /// </summary>
        bool Visible { get;set; }
        /// <summary>
        /// Get the GridColumnType
        /// </summary>
        GridColumnType ColumnType { get;}
        /// <summary>
        /// Get FieldType
        /// </summary>
        FieldType DataType { get; }
        /// <summary>
        /// Get value indicating if Is Bound
        /// </summary>
        bool IsBound { get; }
        /// <summary>
        /// Get the current cell bounds
        /// </summary>
        Rectangle Bounds { get; }
        /// <summary>
        /// Get the AggregateMode
        /// </summary>
        AggregateMode AggregateMode { get; }
        /// <summary>
        /// Width Changed event
        /// </summary>
        event EventHandler WidthChanged;
        /// <summary>
        /// Cell Validated event
        /// </summary>
        event EventHandler CellValidated;

    }

    //public enum GridControlType
    //{
    //    Grid,
    //    VGrid
    //}

    //public interface IGridControl
    //{
    //    Control Parent { get;set;}
    //    bool Visible { get;set;}
    //    bool Focused { get;}
    //    Rectangle Bounds { get;set;}
    //    RightToLeft RightToLeft { get;set;}
    //    string Text { get;set;}
    //    bool Focus();
    //    void DoDropDown();
    //    void SetGrid(Grid parentGrid);
    //    Grid InternalGrid { get; }

    //    bool DroppedDown { get;}
    //    bool CaptionVisible { get;set;}
    //    string CaptionText { get;set;}
    //    int VisibleRows { get;set;}
    //    bool ReadOnly { get;set;}
    //    object DataSource { get;set;}
    //}

}
