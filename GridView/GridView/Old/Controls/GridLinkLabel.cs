using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Permissions ; 
using mControl.WinCtl.Controls;


namespace mControl.GridStyle.Controls
{
	/// <summary>
	/// Summary description for GridLinkLabel.
	/// </summary>
	[System.ComponentModel.ToolboxItem(false)]
	public class GridLinkLabel : mControl.WinCtl.Controls.CtlLinkLabel  
	{
		#region Members

		// Fields
		internal CtlGrid dataGrid;
		private bool isInEditOrNavigateMode;
		private bool readOnly;
		#endregion

		#region Constructor

		public GridLinkLabel()
		{
			//base.NetReflectedFram("ba7fa38f0b671cbc");
			base.SuspendLayout ();
			this.isInEditOrNavigateMode = true;
			this.readOnly =false;
			base.TabStop = false;
			base.BorderStyle   = System.Windows.Forms.BorderStyle.None  ;
			base.ResumeLayout (false);
		}

		#endregion

		#region override 

		public override IStyleLayout CtlStyleLayout
		{
			get{return dataGrid.GridLayout as IStyleLayout;} 
		}

		protected override void OnLinkClicked(LinkLabelLinkClickedEventArgs e)
		{
			base.OnLinkClicked (e);
			if (!ReadOnly )
			{
				this.IsInEditOrNavigateMode = false;
				//this.dataGrid.ColumnStartedEditing(base.Bounds);
			}
		}

		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			base.OnKeyPress(e);
			e.Handled =true;
			//if ((((e.KeyChar != ' ') || ((Control.ModifierKeys & Keys.Shift) != Keys.Shift)) && !base.ReadOnly) && (((Control.ModifierKeys & Keys.Control) != Keys.Control) || ((Control.ModifierKeys & Keys.Alt) != Keys.None)))
			//{
				this.IsInEditOrNavigateMode = false;
				//this.dataGrid.ColumnStartedEditing(base.Bounds);
			//}
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
				this.IsInEditOrNavigateMode = false;
				//this.dataGrid.ColumnStartedEditing(base.Bounds);
			}
			base.WndProc(ref m);
		}

		#endregion

		#region internal methods

		public void LinkTargetInternal(string target)
		{
			if(target.StartsWith("www"))
			{
				base.LinkTarget(this,target); 
			}
		}

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
				//if (value&& !base.IsDisposed)
				//{
				//	base.SelectAll();
				//}
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
