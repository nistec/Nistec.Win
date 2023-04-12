using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Security.Permissions;
using mControl.Util;
using mControl.Win32;
using mControl.WinCtl.Controls;
using System.Windows.Forms.Design;

namespace mControl.GridView
{
	internal class GridPopUp : mControl.WinCtl.Controls.CtlPopUpBase 
	{
		internal Grid dataGrid;
		private System.ComponentModel.IContainer components = null;
		protected Control mparent = null;
		private System.Windows.Forms.Panel panel1;
		//private IWindowsFormsEditorService edSrv;

		
		#region Constructors

		public GridPopUp(Control parent,Size size) : base(parent)
		{
			mparent = parent;

      
			dataGrid=((GridControl)parent).InternalGrid;
            InitializeComponent();
   			//((GridControl)parent).Dock=System.Windows.Forms.DockStyle.Fill ;
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
			this.dataGrid = new mControl.GridView.Grid();
			this.panel1 = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGrid
			// 
			this.dataGrid.DataMember = "";
			this.dataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid.Location = new System.Drawing.Point(0, 0);
			this.dataGrid.Name = "dataGrid";
			this.dataGrid.SelectionType = mControl.GridView.SelectionType.Cell;
			//this.dataGrid.interanlGrid.StylePlan = mControl.WinCtl.Controls.Styles.SteelBlue;
            this.dataGrid.TabIndex = 0;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.White;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.panel1.Controls.Add(dataGrid);
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
            //this.TopMost = true;
            this.TopLevel = false;
			((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		
		#region Overrides

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad (e);
			base.LockClose = true;
  		}

		public override void ClosePopupForm()
		{
			base.ClosePopupForm();
            //if (!base.LockClose && (this.edSrv != null))
            //{
            //    this.edSrv.CloseDropDown();
            //}
		}

		#endregion

		#region Properties

		public GridCell  CurrentCell
		{
			get {return this.dataGrid.CurrentCell;}
		}

		public override object SelectedItem
		{
			get
			{
				return this.dataGrid.CurrentCell as object;
			}
		}

		internal new bool LockClose
		{
			get{return base.LockClose; }
			set{base.LockClose=value;}
		}

		#endregion
	}
}
