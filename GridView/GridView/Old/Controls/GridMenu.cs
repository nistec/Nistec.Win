using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Permissions ; 
using mControl.WinCtl.Controls;


namespace mControl.GridStyle.Controls
{
	/// <summary>
	/// Summary description for GridComboBox.
	/// </summary>
	[System.ComponentModel.ToolboxItem(false)]
	public class GridMenu : mControl.WinCtl.Controls.CtlButtonMenu  
	{

		#region Members

		// Fields
		internal CtlGrid dataGrid;
		private bool isInEditOrNavigateMode;
		private bool readOnly;

		#endregion

		#region Constructor

		public GridMenu()
		{
			base.NetReflectedFram("ba7fa38f0b671cbc");
			this.isInEditOrNavigateMode = true;
			this.readOnly=false;
			base.TabStop = false;
			base.SuspendLayout ();
			base.ButtonPedding =0;
			base.FixSize  =false;
			base.BorderStyle=BorderStyle.FixedSingle   ;
			base.ResumeLayout (false);
		}

		#endregion

		#region override 

		public override IStyleLayout CtlStyleLayout
		{
			get{return dataGrid.GridLayout as IStyleLayout;} 
		}

		protected override void OnLeave(EventArgs e)
		{
			base.OnLeave (e);
			//base.CloseDropDown ();
		}

//		protected  override void OnItemClick(int index, mControl.WinCtl.Controls.ItemClickEventArgs e)
//		{
//			base.OnItemClick (index, e);
//			if (base.Enabled ) 
//			{
//				this.IsInEditOrNavigateMode = false;
//				this.dataGrid.ColumnStartedEditing(base.Bounds);
//			}
//		}

		protected override bool ProcessDialogKey(Keys keyData)
		{
			if(keyData ==Keys.Tab)
	           return true; 
			return base.ProcessDialogKey (keyData);
		}

		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			base.OnKeyPress(e);
			e.Handled =true;
			//if ((((e.KeyChar != ' ') || ((Control.ModifierKeys & Keys.Shift) != Keys.Shift)) && !base.ReadOnly) && (((Control.ModifierKeys & Keys.Control) != Keys.Control) || ((Control.ModifierKeys & Keys.Alt) != Keys.None)))
			//{
			//	this.IsInEditOrNavigateMode = false;
			//	this.dataGrid.ColumnStartedEditing(base.Bounds);
			//}
		}

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			this.dataGrid.ControlOnMouseWheel(e);
		}

		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode=true)]
		protected override void WndProc(ref Message m)
		{
			if (((m.Msg == 770) || (m.Msg == 0x300)) || (m.Msg == 0x303))
			{
				this.IsInEditOrNavigateMode = false;
				this.dataGrid.ColumnStartedEditing(base.Bounds);
			}
			base.WndProc(ref m);
		}

		#endregion

		#region internal methods

		public void SetDataGrid(DataGrid parentGrid)
		{
			this.dataGrid = (CtlGrid)parentGrid;
		}
 
		public bool IsInEditOrNavigateMode
		{
			get
			{
				return this.isInEditOrNavigateMode;
			}
			set
			{
				this.isInEditOrNavigateMode = value;
			}
		}
 
		public bool ReadOnly
		{
			get
			{
				return this.readOnly;
			}
			set
			{
				this.readOnly = value;
			}
		}

		#endregion

	
	}
}
