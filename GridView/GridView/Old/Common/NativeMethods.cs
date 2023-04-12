using System.Windows.Forms;
using System.Drawing;
using System;
using System.IO;
using System.Reflection;

using mControl.Util;
using mControl.GridStyle.Columns;

namespace mControl.GridStyle 
{
 
	public sealed class NativeMethods 
	{


		public static Image LoadImage(string imageName)
		{			
		
			
			Stream strm = Type.GetType("mControl.GridStyle.NativeMethods").Assembly.GetManifestResourceStream(imageName);
 
			Image im = null;
			if(strm != null)
			{
				im = new System.Drawing.Bitmap(strm);
				strm.Close();
			}

			return im;
		}

		public static void ErrMsg(string msg )
		{
			MsgBox.ShowError(msg);
		}

		public static System.Windows.Forms.HorizontalAlignment GetAlignment(System.Drawing.ContentAlignment align)
		{
			switch(align)
			{
				case ContentAlignment.BottomCenter :
				case ContentAlignment.MiddleCenter :
				case ContentAlignment.TopCenter :
					return HorizontalAlignment.Center ;
				case ContentAlignment.BottomLeft :
				case ContentAlignment.MiddleLeft :
				case ContentAlignment.TopLeft :
					return HorizontalAlignment.Left  ;
				case ContentAlignment.BottomRight :
				case ContentAlignment.MiddleRight :
				case ContentAlignment.TopRight :
					return HorizontalAlignment.Right  ;
			}
			return HorizontalAlignment.Left  ;
		}

		public static System.Drawing.ContentAlignment GetAlignment(System.Windows.Forms.HorizontalAlignment align)
		{
			switch(align)
			{
				case HorizontalAlignment.Center  :
					return ContentAlignment.MiddleCenter  ;
				case HorizontalAlignment.Left  :
					return ContentAlignment.MiddleLeft   ;
				case HorizontalAlignment.Right  :
					return ContentAlignment.MiddleRight   ;
			}
			return ContentAlignment.MiddleLeft  ;
		}

		public static System.Windows.Forms.DataGridColumnStyle  GetColumnType(GridStyle.ColumnTypes value)
		{

			switch(value)
			{
				case ColumnTypes.TextColumn:// = 0,
					return new GridTextColumn ();
				case ColumnTypes.ComboColumn:// = 1,
					return new GridComboColumn ();
				case ColumnTypes.DateTimeColumn:// = 2,
					return new GridDateColumn  ();
				case ColumnTypes.LabelColumn:// = 3,
					return new GridLabelColumn ();
				case ColumnTypes.LinkColumn:// = 4,
					return new GridLinkColumn ();
				case ColumnTypes.ButtonColumn:// = 5,
					return new GridButtonColumn ();
				case ColumnTypes.ProgressColumn:// = 6,
					return new GridProgressColumn ();
				case ColumnTypes.BoolColumn:// = 7,
					return new GridBoolColumn ();
				case ColumnTypes.IconColumn:// = 8,
					return new GridIconColumn ();
				case ColumnTypes.MultiColumn:// = 9,
					return new GridMultiColumn ();
				case ColumnTypes.EnumColumn:// = 10,
					return new GridEnumColumn ();
				case ColumnTypes.MenuColumn :// = 11,
					return new GridMenuColumn ();
				case ColumnTypes.GridColumn  :// = 12,
					return new GridControlColumn ();
				default://case ColumnTypes.None://=99
					return new GridLabelColumn ();

			}
		}


		#region Data


//			public DataTable GetTableSchema(string strSQL,string TableName)
//			{
//				SqlDataAdapter da = new SqlDataAdapter (strSQL,m_cnn);
//				DataSet ds = new DataSet ();
//				da.FillSchema  (ds,System.Data.SchemaType.Source , TableName);
//				return ds.Tables [TableName];
//			}
//
//			public DataTable GetPropertySchema(string strSQL)
//			{
//				SqlCommand cmd = new SqlCommand(strSQL,m_cnn);
//				SqlDataReader myReader = cmd.ExecuteReader();
//				return myReader.GetSchemaTable();
//			}  


		#endregion



	}
}