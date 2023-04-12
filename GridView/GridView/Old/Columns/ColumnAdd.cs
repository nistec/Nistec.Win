using System.Windows.Forms;
using System.Reflection;
using System.Drawing;
using System.Diagnostics;
using System;
using mControl.Util;
using mControl.GridStyle.Columns ;


   
namespace mControl.GridStyle.Columns 
{
   
	public sealed class ColumnAdd 
	{

		#region TextBoxColunn

		public static GridTextColumn GetTextBoxColumn(
			string MappingName, 
			string HeaderText, 
			int Width,
			string NullText) 
		{
         
			return GetTextBoxColumn(MappingName,HeaderText,Width,NullText,Formats.None,HorizontalAlignment.Left,false);
		}

		public static GridTextColumn GetTextBoxColumn(
			string MappingName, 
			string HeaderText, 
			int Width,
			string NullText,
			Formats  formatType, 
			HorizontalAlignment Alignment,
			bool ReadOnly) 
		{
         
			GridTextColumn Col = new GridTextColumn();
			Col.MappingName = MappingName;
			Col.HeaderText=HeaderText;
			Col.Width =Width;
			Col.NullText=NullText;
			Col.FormatType=formatType;
			Col.Alignment =Alignment;
			Col.ReadOnly=ReadOnly;
			return Col;
		}

		#endregion

		#region ComboBoxColumn

		public static GridComboColumn GetComboBoxColumn(
			string MappingName, 
			string HeaderText, 
			int Width, 
			string NullText)
		{
         
			return GetComboBoxColumn(MappingName,HeaderText,Width,NullText,"",HorizontalAlignment.Left,false,null);
		}

		public static GridComboColumn GetComboBoxColumn(
			string MappingName, 
			string HeaderText, 
			int Width, 
			string NullText,
			string Format,
			bool ReadOnly,
			HorizontalAlignment Alignment, 
			string DisplayMember,
			string ValueMember,
			object DataSource)
		{
         
			GridComboColumn Col = new GridComboColumn();
			Col.MappingName = MappingName;
			Col.HeaderText=HeaderText;
			Col.Width =Width;
			Col.NullText = NullText;
			Col.Format=Format;
			Col.Alignment  = Alignment;
			Col.ReadOnly = ReadOnly;
			Col.DisplayMember =DisplayMember;
			Col.ValueMember =ValueMember;
			Col.DataSource = DataSource;
            
			return Col;
		}
        
		public static GridComboColumn GetComboBoxColumn(
			string MappingName, 
			string HeaderText, 
			int Width, 
			string NullText,
			string Format,
			HorizontalAlignment Alignment,
			bool ReadOnly,
			string[] Items)
		{
         
			GridComboColumn Col = new GridComboColumn();
			Col.MappingName = MappingName;
			Col.HeaderText=HeaderText;
			Col.Width =Width;
			Col.NullText = NullText;
			Col.Format=Format;
			Col.Alignment = Alignment;
			Col.ReadOnly = ReadOnly;
			if(Items!=null)
			{
				Col.Items.AddRange(Items);           
			}
			return Col;
		}
        
		#endregion

		#region CtlMultiBox
		public static GridMultiColumn GetMultiBoxColumn(
			string MappingName, 
			string HeaderText, 
			int Width, 
			string NullText,
			string Format,
			bool ReadOnly,
			HorizontalAlignment Alignment,
			mControl.WinCtl.Controls.MultiComboTypes  CommandType) 
		
		{
         
			GridMultiColumn Col = new GridMultiColumn();
			Col.MappingName = MappingName;
			Col.HeaderText=HeaderText;
			Col.Width =Width;
			Col.NullText = NullText;
			Col.Format=Format;
			Col.Alignment  = Alignment;
			Col.ReadOnly = ReadOnly;
			Col.MultiType =CommandType;
	        
			return Col;
		}
		#endregion

		#region ButtonMenu
		public static GridMenuColumn GetButtonMenuColumn(
			string MappingName, 
			string HeaderText, 
			int Width, 
			bool ReadOnly) 
		
		{
			GridMenuColumn Col = new GridMenuColumn();
			Col.MappingName = MappingName;
			Col.HeaderText=HeaderText;
			Col.Width =Width;
			Col.ReadOnly = ReadOnly;
	        
			return Col;
		}
		#endregion

		#region ButtonColumn

		public static GridButtonColumn GetButtonColumn(
			string MappingName, 
			string HeaderText, 
			int Width, 
			bool ReadOnly) 
		{
         
			GridButtonColumn Col = new GridButtonColumn();
			Col.MappingName = MappingName;
			Col.HeaderText=HeaderText;
			Col.Width =Width;
			Col.ReadOnly = ReadOnly;
			return Col;
		}

		#endregion

		#region Progreess

		public static GridProgressColumn GetProgressBarColumn(
			string MappingName, 
			string HeaderText, 
			int Width) 
		{
         
			GridProgressColumn Col = new GridProgressColumn();
			Col.MappingName = MappingName;
			Col.HeaderText=HeaderText;
			Col.Width =Width;
			return Col;
		}
		#endregion

		#region LinkColumn

		public static GridLinkColumn GetLinkColumn(
			string MappingName, 
			string HeaderText, 
			int Width, 
			string NullText,
			System.Windows.Forms.HorizontalAlignment Alignment,
			bool ReadOnly) 
		{
         
			GridLinkColumn Col = new GridLinkColumn();
			Col.MappingName = MappingName;
			Col.HeaderText=HeaderText;
			Col.Width =Width;
			Col.NullText = NullText;
			Col.Alignment = Alignment;
			return Col;
		}

		#endregion

		#region LabelColumn

		public static GridLabelColumn GetLabelColumn(
			string MappingName, 
			string HeaderText, 
			int Width, 
			string NullText,
			string Format,
			System.Windows.Forms.HorizontalAlignment Alignment) 
		{
         
			GridLabelColumn Col = new GridLabelColumn();
			Col.MappingName = MappingName;
			Col.HeaderText=HeaderText;
			Col.Width =Width;
			Col.NullText = NullText;
			Col.Format=Format;
			Col.Alignment = Alignment;
			return Col;
		}

		#endregion

		#region DatePickerColumn

		public static GridDateColumn GetDatePickerColumn(
			string MappingName, 
			string HeaderText, 
			int Width, 
			string NullText)
		{
         
			return GetDatePickerColumn(MappingName,HeaderText,Width,NullText,"dd/MM/yyy",HorizontalAlignment.Left,false,new mControl.Util.RangeDate(Range.MinDate,Range.MaxDate));
		}

		public static GridDateColumn GetDatePickerColumn(
			string MappingName, 
			string HeaderText, 
			int Width, 
			string NullText,
			string Format,
			HorizontalAlignment Alignment,
			bool ReadOnly,
			RangeDate rangeDate)
		{
         
			GridDateColumn Col = new GridDateColumn();
			Col.MappingName = MappingName;
			Col.HeaderText=HeaderText;
			Col.Width =Width;
			Col.NullText = NullText;
			Col.Format=Format;
			Col.Alignment = Alignment;
			Col.ReadOnly = ReadOnly;
			Col.RangeValue  = rangeDate;
	        
			return Col;
		}

		#endregion

		#region IconColumn

		public static GridIconColumn GetIconColumn(
			string MappingName, 
			string HeaderText, 
			int Width, 
			ImageList IconList) 
		{
         
			GridIconColumn Col = new GridIconColumn();
			Col.MappingName = MappingName;
			Col.HeaderText=HeaderText;
			Col.Width =Width;
			Col.IconList  = IconList;
			return Col;
		}

		#endregion

		#region BoolColumn
		
		public static GridBoolColumn GetBoolColumn(
			string MappingName, 
			string HeaderText, 
			int Width,
			bool NullValue,
			object FalsValue,
			object TrueValue) 
		{

			GridBoolColumn Col = new  GridBoolColumn ();
			Col.MappingName = MappingName;
			Col.HeaderText=HeaderText;
			Col.Width =Width;
			Col.NullValue  = NullValue;
			Col.FalseValue =FalsValue;
			Col.TrueValue  =TrueValue;

			return Col;
		}
		#endregion
	}
}