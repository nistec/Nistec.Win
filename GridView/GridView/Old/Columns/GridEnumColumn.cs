
using System;
using System.ComponentModel;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using mControl.Util ;
using mControl.WinCtl.Controls;
   
namespace mControl.GridStyle.Columns
{

	public class GridEnumColumn : GridColumnStyle 
	{

		private MultiComboTypes mCommandType= MultiComboTypes.Custom;
		
		#region Constructor
		public GridEnumColumn() : base() 
		{
			//this.ControlSize = new Size( 0, 12 );
			this.Width =0;
			m_ColumnType = ColumnTypes.EnumColumn  ;
		}
		#endregion
		
		#region Properties

		[DefaultValue(MultiComboTypes.Text)]
		public MultiComboTypes ComboTypes 
		{
			get{return mCommandType;}
			set 
			{
				mCommandType = value;
				this.Invalidate();
			}
		}
		#endregion
	}

}	