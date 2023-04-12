using System;
using System.Drawing;
using System.Data;
using mControl.WinCtl.Controls;
using mControl.WinCtl.Printing;
using mControl.Data;

namespace mControl.GridStyle
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
				//object header=mControl.WinCtl.Dlg.InputBox.Open(null," ","Insert Header ","Printing");	
				//if(header==null) return;
//				Printing.CtlPrintDocument printDocument=new Printing.CtlPrintDocument();
//				WinCtl.Printing.DataViewDocument rptList=new DataViewDocument(grid.DataList,grid.CaptionText);
			
				CtlColumn[] cols= CreateColumns(grid);
				
				Data.ExportColumnType[] exCol=new mControl.Data.ExportColumnType[cols.Length];
				for(int i=0;i<cols.Length;i++)
				{
					exCol[i].ColumnOrdinal=cols[i].ColumnOrdinal;
					exCol[i].Caption=cols[i].Caption;
				}
//				printDocument.DataSource=grid.DataList;
//				printDocument.ColumnType=exCol;

				WinCtl.Printing.DataViewDocument rptList=new DataViewDocument(grid.DataList,grid.CaptionText);

				rptList.SetDefaultStyle();
				rptList.CreateDocument(
					DataViewDocument.ConvertRtlToAlignment(grid.RightToLeft),
					(float)grid.PreferredColumnWidth,
					CreateColumns(grid));
				rptList.Show();
//				printDocument.ReportSetting=rptList as Printing.IReportDocument;
//				CtlPrintPreviewDialog dlg=new CtlPrintPreviewDialog();
//				dlg.Document=printDocument;
//				dlg.Show();
//				dlg.Activate();
			}
			catch(Exception ex)
			{
				Util.MsgBox.ShowError(ex.Message);
			}
		}

		public static CtlColumn[] CreateColumns(Grid grid)
		{
			GridColumnStyle[] gcs= grid.GetVisibleColumns();
			CtlColumn[] ctl=new CtlColumn[gcs.Length];
			int i=0;
			foreach(GridColumnStyle c in gcs)
			{
				CtlColumn col= new CtlColumn(c.MappingName,c.HeaderText,c.Width,c.DataType);
				col.Display=c.Visible;
				ctl[i]=col;
				i++;
			}
			return ctl;
		}

		public static mControl.Data.ExportColumnType[]  CreateExportColumns(Grid grid)
		{
			GridColumnStyle[] gcs= grid.GetVisibleColumns();
			ExportColumnType[] ctl=new ExportColumnType[gcs.Length];
			DataTable dt=grid.DataList.Table;

			int i=0;
			foreach(GridColumnStyle c in gcs)
			{
				ExportColumnType col= new ExportColumnType();
				col.Caption=c.HeaderText;
				col.ColumnOrdinal=dt.Columns[c.MappingName].Ordinal;
				ctl[i]=col;
				i++;
			}
			return ctl;
		}

	}

}
