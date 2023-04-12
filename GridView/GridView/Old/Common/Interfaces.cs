using System;
using System.Windows.Forms;
using System.Drawing;
using mControl.GridStyle;
using mControl.WinCtl.Controls;

namespace mControl.GridStyle
{
	
	#region IGrid
	public interface IGrid
	{
		//ColumnCollection  Columns {get;}
		//GridTableStyle GridTableStyle {get;}
		string DataMember {get;set;}
		object DataSource {get;set;}
		bool CaptionVisible {get;set;} 
		System.Data.DataView DataList  {get;}
		int HeightInternal {get;} 
		int Width {get;set;} 
		int PreferredColumnWidth  {get;set;}
		int PreferredRowHeight  {get;set;}
		bool ColumnHeadersVisible  {get;set;}
		string CaptionText {get;set;}
		mControl.WinCtl.Controls.IStyleLayout CtlStyleLayout{get;}
		IStyleGrid GridLayout{get;}
		//void SumPanel(int index,decimal diff);
	}

	#endregion

	#region IGridColumnStyle

	public interface IGridColumnStyle 
	{	
		bool AllowNull {get;set;}
		string Format  {get;set;}
		ColumnTypes ColumnType  {get;}
		//CurrencyManager CM();
		//System.Data.DataView GetDataView();  
		//object GetControlvalue();  
		//int CurrentRow();
		//int CurrentCol(); 
		//DataGridColumnStyle CurrentColType( int col );  
		//int GetColIndex(string colName);
	}

	#endregion
}
