using System;
using System.Drawing;
using System.Data;
using MControl.WinForms;
using MControl.Printing;
using MControl.Data;

namespace MControl.GridView
{
	/// <summary>
	/// Summary description for PrintDataView.
	/// </summary>
	internal class PrintGridDataView
	{
	
		internal static void Print(Grid grid)
		{
			try
			{

				DataView dv=grid.DataList;
				if(dv==null)
					return;
			
				McColumn[] cols= CreateColumns(grid);
                ReportBuilder.PrintDataView(dv, grid.CaptionText, ReportBuilder.ConvertRtlToAlignment(grid.RightToLeft),cols,true);

                //AdoField[] exCol = new AdoField[cols.Length];
                //for(int i=0;i<cols.Length;i++)
                //{
                //    exCol[i] = new AdoField(cols[i].ColumnName, cols[i].Caption, cols[i].Ordinal);
                //}

                //Printing.DataViewDocument rptList=new DataViewDocument(dv,grid.CaptionText);

                //rptList.SetDefaultStyle();
                //rptList.CreateDocument(
                //    DataViewDocument.ConvertRtlToAlignment(grid.RightToLeft),
                //    (float)grid.PreferredColumnWidth,
                //    CreateColumns(grid));
                //rptList.Show();
			}
			catch(Exception ex)
			{
				MsgBox.ShowError(ex.Message);
			}
		}

        public static McColumn[] CreateColumns(Grid grid)
        {
            GridColumnStyle[] gcs = grid.GetBoundsColumns();
            McColumn[] ctl = new McColumn[gcs.Length];
            int i = 0;
            foreach (GridColumnStyle c in gcs)
            {
                McColumn col = new McColumn(c.MappingName, c.HeaderText, c.Width, c.DataType);
                col.Display = c.Visible;
                ctl[i] = col;
                i++;
            }
            return ctl;
        }

        public static MControl.Printing.AdoField[] CreateExportColumns(Grid grid)
		{
			DataView dv=grid.DataList;
			if(dv==null)
				return null;

			DataTable dt=dv.Table;
		
			GridColumnStyle[] gcs= grid.GetVisibleColumns();
            MControl.Printing.AdoField[] ctl = new MControl.Printing.AdoField[gcs.Length];

			int i=0;
			foreach(GridColumnStyle c in gcs)
			{
                //ExportField col = new ExportField();
                //col.Caption=c.HeaderText;
                //col.ColumnOrdinal=dt.Columns[c.MappingName].Ordinal;
                ctl[i] = new MControl.Printing.AdoField(c.MappingName, c.HeaderText, dt.Columns[c.MappingName].Ordinal);
				i++;
			}
			return ctl;
		}
 
	}

}
