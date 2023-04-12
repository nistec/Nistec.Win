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
	public class GridComboBox : mControl.WinCtl.Controls.CtlComboBox 
	{

		#region Members

		// Fields
		internal CtlGrid dataGrid;
		private bool isInEditOrNavigateMode;
		internal bool editingPosition;
		internal System.Data.DataView internalDataView;

		#endregion

		#region Constructor

		public GridComboBox()
		{
			base.NetReflectedFram("ba7fa38f0b671cbc");
			this.editingPosition=false;
			this.isInEditOrNavigateMode = true;
			base.TabStop = false;
			base.SuspendLayout ();
			base.ButtonPedding =0;
			base.gridBounded=true;
			//base.ForcesSize = Font.Height +2;
			//base.ShowErrorProvider =false;
			base.FixSize  =false;
			base.BorderStyle=BorderStyle.FixedSingle   ;
			base.ComboStyle =mControl.WinCtl.Controls.ComboStyles.Button;    
			base.ResumeLayout (false);

		}

		#endregion

		#region override 

		public override IStyleLayout CtlStyleLayout
		{
			get{return dataGrid.GridLayout as IStyleLayout;} 
		}

//		protected override void OnHandleCreated(EventArgs e)
//		{
//			base.OnHandleCreated (e);
//			//this.edit.SetDataGrid(value);
//			base.StyleCtl.StylePlan =this.dataGrid.interanlGrid.StylePlan;   
//		}

		protected override void OnLeave(EventArgs e)
		{
			base.OnLeave (e);
			//base.CloseDropDown ();
		}

		protected override void OnDropDown(EventArgs e)
		{
			base.OnDropDown (e);
			this.editingPosition=true;
		}

		internal void ClearSelected()
		{
			//try
			//{
				editingPosition=false;
				//base.ResetText();
				//base.SelectedIndex=-1;
			    base.ClearSelection();
			//}
			//catch{}
		}

		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			if(!IsHandleCreated)
				return;
			base.OnSelectedIndexChanged (e);
			if (!base.ReadOnly && editingPosition) 
			{
				this.IsInEditOrNavigateMode = false;
				this.dataGrid.ColumnStartedEditing(base.Bounds);
				editingPosition=false;
			}
		}

		protected override void OnSelectedValueChanged(EventArgs e)
		{
            if(!IsHandleCreated)
				return;
			base.OnSelectedValueChanged (e);
			if (!base.ReadOnly && editingPosition) 
			{
				this.IsInEditOrNavigateMode = false;
				this.dataGrid.ColumnStartedEditing(base.Bounds);
				editingPosition=false;
			}
		}

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
			//if(this.dataGrid.IsHandleCreated)
			//this.StyleCtl.StylePlan=this.dataGrid.interanlGrid.StylePlan;
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
				if (value && !base.IsDisposed)
				{
					base.SelectAll();
				}
			}
		}
 
		internal void SelectInternal(int start, int length)
		{
			base.SelectAll ();
		}

		internal void SetDataView()
		{
			if(base.DataSource!=null)
			{
				try
				{
					this.internalDataView=mControl.Data.DataSourceConvertor.GetDataView(base.DataSource,""); 
					if(this.internalDataView!=null)
					{
						this.internalDataView.Sort=base.ValueMember;
						base.DataSource=this.internalDataView;
					}
				}
				catch{}
			}
		}
    
		#endregion

		#region Combo Methods
         
		internal new  string GetItemText(object item)
		{
			if(this.DataSource !=null) 
			{
				
				//int i=base.DataManagerFind(item,true);
				return base.GetDataRowText(item,this.ValueMember );
  
			}
			else
				return base.GetItemText (item);
		}


		#endregion
	}
}
