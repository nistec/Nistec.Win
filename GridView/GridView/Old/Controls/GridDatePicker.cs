using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Permissions ; 
using mControl.WinCtl.Controls;


namespace mControl.GridStyle.Controls
{
	/// <summary>
	/// Summary description for GridDatePicker.
	/// </summary>
	[System.ComponentModel.ToolboxItem(false)]
	public class GridDatePicker : mControl.WinCtl.Controls.CtlDatePicker  
	{

		#region Members

		// Fields
		internal CtlGrid dataGrid;
		private bool isInEditOrNavigateMode;
  
		#endregion

		#region Constructor

		public GridDatePicker()
		{
			base.NetReflectedFram("ba7fa38f0b671cbc");
			this.isInEditOrNavigateMode = true;
			base.TabStop = false;
			base.SuspendLayout ();
			base.ButtonPedding =0;
			//base.ForcesSize = Font.Height +2;
			//base.ShowErrorProvider =false;
			base.FixSize  =false;
			base.BorderStyle=BorderStyle.FixedSingle   ;
			base.ComboStyle =mControl.WinCtl.Controls.ComboStyles.Button ;    
			base.ResumeLayout (false);
		}

		#endregion

		#region override 

		public override IStyleLayout CtlStyleLayout
		{
			get{return dataGrid.GridLayout as IStyleLayout;} 
		}

		protected override void OnKeyPressInternal(KeyPressEventArgs e)
		{
			base.OnKeyPressInternal (e);
			if ((((e.KeyChar != ' ') || ((Control.ModifierKeys & Keys.Shift) != Keys.Shift)) && !base.ReadOnly) && (((Control.ModifierKeys & Keys.Control) != Keys.Control) || ((Control.ModifierKeys & Keys.Alt) != Keys.None)))
			{
				this.IsInEditOrNavigateMode = false;
				this.dataGrid.ColumnStartedEditing(base.Bounds);
			}
		}

		protected override void OnModifiedInternal(EventArgs e)
		{
			base.OnModifiedInternal (e);
			if (!base.ReadOnly) 
			{
				this.IsInEditOrNavigateMode = false;
				this.dataGrid.ColumnStartedEditing(base.Bounds);
			}
		}

		protected override void OnValueChanged(EventArgs eventargs)
		{
			base.OnValueChanged (eventargs);
			if(!IsHandleCreated )//&& base.IsModified)
				return;
			if (!base.ReadOnly) 
			{
				this.IsInEditOrNavigateMode = false;
				this.dataGrid.ColumnStartedEditing(base.Bounds);
			}
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			return base.ProcessCmdKey (ref msg, keyData);
		}

		protected override bool ProcessDialogKey(Keys keyData)
		{
			if(keyData ==Keys.Tab)
				return true; 
			return base.ProcessDialogKey (keyData);
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
				//this.IsInEditOrNavigateMode = false;
				//this.dataGrid.ColumnStartedEditing(base.Bounds);
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
				if (value&& !base.IsDisposed)
				{
					base.SelectAll();
				}
			}
		}
 
		internal void SelectInternal(int start, int length)
		{
			base.SelectAll ();
		}
    
		#endregion

		#region Combo Methods
         
//		internal new  string GetItemText(object item)
//		{
//			return base.GetItemText (item);
//		}


		#endregion
	}
}
