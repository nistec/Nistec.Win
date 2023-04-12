using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Security.Permissions;
using mControl.Util;
using mControl.Win32;
using mControl.WinCtl.Controls;

namespace mControl.GridStyle.Controls
{
	internal class GridPopUp : mControl.WinCtl.Controls.CtlPopUpBase 
	{
		internal CtlGrid mGrid;
		private System.ComponentModel.IContainer components = null;
		protected Control mparent = null;
		private System.Windows.Forms.Panel panel1;
  
		
		#region Constructors

		public GridPopUp(Control parent,Size size) : base(parent)
		{
			mparent = parent;
	         
			InitializeComponent();
			mGrid=((GridControl)parent).InternalGrid.DataGrid;
			this.panel1.Controls.Add(mGrid);//((GridControl)parent).InternalGrid);
			((GridControl)parent).Dock=System.Windows.Forms.DockStyle.Fill ;
			this.Size =size; 
			this.Height  =this.Height ; 
	
		}
		
		#endregion

		#region Dispose
		protected override void Dispose( bool disposing )
		{
			this.panel1.Controls.Clear ();
   
			if( disposing )
			{
				//mparent.Controls[0].LostFocus -= new System.EventHandler(this.ParentControlLostFocus);
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		#endregion

		#region Windows Form Designer generated code
		private void InitializeComponent()
		{
			this.mGrid = new mControl.GridStyle.CtlGrid();
			this.panel1 = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.mGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// mGrid
			// 
			this.mGrid.DataMember = "";
			this.mGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.mGrid.Location = new System.Drawing.Point(0, 0);
			this.mGrid.Name = "mGrid";
			this.mGrid.SelectionType = mControl.GridStyle.SelectionTypes.Cell;
			//this.mGrid.interanlGrid.StylePlan = mControl.WinCtl.Controls.Styles.SteelBlue;
			this.mGrid.TabIndex = 0;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.White;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(160, 160);
			this.panel1.TabIndex = 1;
			// 
			// GridPopUp
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(160, 160);
			this.Controls.Add(this.panel1);
			this.Name = "GridPopUp";
			this.Text = "GridPopUp";
			((System.ComponentModel.ISupportInitialize)(this.mGrid)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		
		#region Overrides
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			base.OnPaint(e);

			if (this.mparent is CtlDatePicker)
			{
				Rectangle rect = new Rectangle(this.ClientRectangle.Location,new Size(this.ClientRectangle.Width - 1,this.ClientRectangle.Height - 1));
				
				using(Pen pen = new Pen( Color.Blue))
				{
					e.Graphics.DrawRectangle(pen,rect);
				}
			}
		}

		public override void PostMessage(ref Message m)
		{
			Message msg = new Message();
			msg.HWnd    = mGrid.Handle;
			msg.LParam  = m.LParam;
			msg.Msg     = m.Msg;
			msg.Result  = m.Result;
			msg.WParam  = m.WParam;

			//mGrid.PostMessage(ref msg);
		}

		#endregion

		#region Properties

		public System.Windows.Forms.DataGridCell  CurrentCell
		{
			get {return this.mGrid.CurrentCell;}
		}

		public override object SelectedItem
		{
			get
			{
				return this.mGrid.CurrentCell as object;
			}
		}


		#endregion
	}
}
