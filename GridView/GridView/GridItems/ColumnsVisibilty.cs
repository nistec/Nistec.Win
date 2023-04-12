using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Nistec.GridView
{
    internal class ColumnsVisibility
    {
        #region members
        const int minColumnWidth = 10;

        internal GridColumnCollection columns;
        internal readonly int sumColFixed;
        internal readonly int sumColAuto;
        internal readonly int colAuto;
        internal readonly int visibleCols;
        internal readonly int addDiff;
        internal readonly int rowHeader;
        internal readonly int totalDiff;
        #endregion

        internal int VisibleDiff
        {
            get
            {
                if (colAuto > 0)
                    return totalDiff / colAuto;
                if (visibleCols > 0)
                 return totalDiff / visibleCols;
             return 0;

            }
        }

        internal bool IsEmpty
        {
            get
            {
                if (columns == null)
                    return true;
                return columns.Count==0;
            }
        }

        internal bool ShouldResize(GridColumnStyle gc)
        {
            if (gc.IsVisibleInternal)
            {
                if (colAuto > 0)
                {
                    return gc.AutoAdjust && gc.width > minColumnWidth;
                }
                else
                {
                    return gc.width > minColumnWidth;
                }
            }
            return false;
        }

        internal bool ShouldResizeDesign(GridColumnStyle gc)
        {
            if (gc.IsVisibleDesigner)
            {
                if (colAuto > 0)
                {
                    return gc.AutoAdjust && gc.width > minColumnWidth;
                }
                else
                {
                    return gc.width > minColumnWidth;
                }
            }
            return false;
        }

        #region ctor

        internal ColumnsVisibility(Grid grid, bool designMode)
            : this(grid, designMode,false,false)
        { }

        internal ColumnsVisibility(Grid grid, bool designMode, bool showScroll, bool calcHeight)
        {
            if (designMode)
            {
                columns = grid.Columns;
                if (columns == null)
                    return;

                foreach (GridColumnStyle gc in columns)
                {
                    if (gc.IsVisibleDesigner)
                    {
                        if (gc.AutoAdjust)
                        {
                            sumColAuto += gc.Width;
                            colAuto++;
                        }
                        else
                            sumColFixed += gc.Width;
                        visibleCols++;
                    }
                }
            }
            else 
            {
                 columns = grid.GridColumns;
                if (columns == null)
                    return;
                if (grid.VisibleRowCount == 0)
                {
                    totalDiff = 0;
                    return;
                }
                foreach (GridColumnStyle gc in columns)
                {
                    if (gc.IsVisibleInternal/*bound*/)//.Visible && gc.Width>0)// && !string.IsNullOrEmpty(gc.MappingName) )
                    {
                        if (gc.AutoAdjust)
                        {
                            sumColAuto += gc.Width;
                            colAuto++;
                        }
                        else
                            sumColFixed += gc.Width;
                        visibleCols++;
                    }
                }
            }
            //GridColumnStyle lastCol=null;
            addDiff = grid.BorderStyle == BorderStyle.Fixed3D ? 5 : 3;
            rowHeader = grid.RowHeadersVisible ? grid.RowHeaderWidth : 0;
            int scrollwidth = 0;

            if (calcHeight)
            {
                scrollwidth = (grid.CalcRowsHeight(0) > grid.HeightInternal) ? grid.VertScrollBar.Width : 0;
            }
            else if (designMode)
            {
                scrollwidth = showScroll ? grid.VertScrollBar.Width : 0;
            }
            else
            {
                scrollwidth = grid.VisibleVerticalScrollBarWidth;
            }
            if (grid.Width == 0)
            {
                totalDiff = 0;
            }
            else
            {
                totalDiff = grid.Width - (addDiff + scrollwidth + rowHeader + sumColAuto + sumColFixed);
            }
            //int diff = 0;
        }

#endregion

        #region Old
        //internal protected void OnDesignAdjustColumns(bool showScroll)
        //{
        //    if (this.Columns.Count == 0)
        //    {
        //        //MessageBox.Show("No ColumnStyles Define");
        //        return;
        //    }
        //    const int minColumnWidth = 10;

        //    int sumColFixed = 0;
        //    int sumColAuto = 0;
        //    int colAuto = 0;
        //    int visibleCols = 0;
        //    GridColumnCollection gcols = this.Columns;

        //    foreach (GridColumnStyle gc in gcols)
        //    {
        //        if (gc.IsVisibleDesigner/*bound*/)//.Visible && gc.Width>0)// && !string.IsNullOrEmpty(gc.MappingName) )
        //        {
        //            if (gc.AutoAdjust)
        //            {
        //                sumColAuto += gc.Width;
        //                colAuto++;
        //            }
        //            else
        //                sumColFixed += gc.Width;
        //            visibleCols++;
        //        }
        //    }
        //    int addDiff = BorderStyle == BorderStyle.Fixed3D ? 5 : 3;
        //    int rowHeader = RowHeadersVisible ? RowHeaderWidth : 0;
        //    int scrollwidth = showScroll ? this.vertScrollBar.Width : 0;
        //    int totalDiff = this.Width - (addDiff + scrollwidth /*VisibleVerticalScrollBarWidth*/ + rowHeader + sumColAuto + sumColFixed);
        //    int diff = 0;
        //    if (colAuto > 0)
        //    {
        //        diff = totalDiff / colAuto;
        //        if (diff == 0) return;
        //        foreach (GridColumnStyle gc in gcols)
        //        {
        //            if (gc.IsVisibleDesigner/*bound*/)//.Visible && !string.IsNullOrEmpty(gc.MappingName))
        //            {
        //                if (gc.AutoAdjust)
        //                {
        //                    gc.Width = Math.Max(minColumnWidth, gc.Width + diff);
        //                    //gc.Width = (diff < 0) ? Math.Min(DefaultColumnWidth, gc.Width + diff) : Math.Max(DefaultColumnWidth, gc.Width + diff);
        //                }
        //            }
        //        }
        //    }
        //    else if (visibleCols > 0)
        //    {
        //        diff = totalDiff / visibleCols;
        //        if (diff == 0) return;
        //        foreach (GridColumnStyle gc in gcols)
        //        {
        //            if (gc.IsVisibleDesigner/*bound*/)//.Visible && !string.IsNullOrEmpty(gc.MappingName))
        //            {
        //                gc.Width = Math.Max(minColumnWidth, gc.Width + diff);
        //                //gc.Width = (diff < 0) ? Math.Min(DefaultColumnWidth, gc.Width + diff) : Math.Max(DefaultColumnWidth, gc.Width + diff);
        //            }
        //        }
        //    }

        //}

        //internal protected void OnResizeAdjustColumns()
        //{
        //    if (this.Initializing || this.DesignMode)
        //        return;
        //    //bool match = false;
        //    const int minColumnWidth = 10;

        //    //for (int i = 0; i < 2 && !match; i++)
        //    //{
        //        int sumColFixed = 0;
        //        int sumColAuto = 0;
        //        int colAuto = 0;
        //        int visibleCols = 0;
        //        GridColumnCollection gcols = GridColumns;
        //        if (gcols == null)
        //            return;

        //        foreach (GridColumnStyle gc in gcols)
        //        {
        //            if (gc.IsVisibleInternal/*bound*/)//.Visible && gc.Width>0)// && !string.IsNullOrEmpty(gc.MappingName) )
        //            {
        //                if (gc.AutoAdjust)
        //                {
        //                    sumColAuto += gc.Width;
        //                    colAuto++;
        //                }
        //                else
        //                    sumColFixed += gc.Width;
        //                visibleCols++;
        //            }
        //        }
        //        //GridColumnStyle lastCol=null;
        //        int addDiff = BorderStyle == BorderStyle.Fixed3D ? 5 : 3;
        //        int rowHeader = RowHeadersVisible ? RowHeaderWidth : 0;
        //        int totalDiff = this.Width - (addDiff + VisibleVerticalScrollBarWidth + rowHeader + sumColAuto + sumColFixed);
        //        int diff = 0;
        //        if (colAuto > 0)
        //        {
        //            diff = totalDiff / colAuto;
        //            if (diff == 0) return;
        //            foreach (GridColumnStyle gc in gcols)
        //            {
        //                if (gc.IsVisibleInternal/*bound*/)//.Visible && !string.IsNullOrEmpty(gc.MappingName))
        //                {
        //                    if (gc.AutoAdjust)
        //                    {
        //                        gc.Width = Math.Max(minColumnWidth, gc.Width + diff); 
        //                        //if(gc.Width > minColumnWidth) lastCol=gc;
        //                        //gc.Width = (diff < 0) ? Math.MinDefaultColumnWidth, gc.Width + diff) : Math.Max(DefaultColumnWidth, gc.Width + diff);
        //                    }
        //                }
        //            }
        //        }
        //        else if (visibleCols > 0)
        //        {
        //            diff = totalDiff / visibleCols;
        //            if (diff == 0) return;
        //            foreach (GridColumnStyle gc in gcols)
        //            {
        //                if (gc.IsVisibleInternal/*bound*/)//.Visible && !string.IsNullOrEmpty(gc.MappingName))
        //                {
        //                    gc.Width = Math.Max(minColumnWidth, gc.Width + diff);
        //                    //if (gc.Width > minColumnWidth) lastCol = gc;
        //                    //gc.Width = (diff < 0) ? Math.Min(DefaultColumnWidth, gc.Width + diff) : Math.Max(DefaultColumnWidth, gc.Width + diff);
        //                }
        //            }
        //        }
        //        //match = !this.horizScrollBar.Visible;
        //        //if (!match)
        //        //{
        //        //    if(lastCol!=null)
        //        //    lastCol.width = Math.Max(minColumnWidth, lastCol.Width - horizScrollBar.Width);
        //        //}
        //    //}

        //}

        #endregion
    }
}
