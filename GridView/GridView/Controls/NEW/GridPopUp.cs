using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;
using System.Windows.Forms;

using mControl.Win32;
using mControl.Util;
using mControl.WinCtl.Controls;
using mControl.Drawing;

using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace mControl.GridStyle
{

	public class GridPopUp : CtlPopUpBase
	{

		// Fields

		internal Grid dataGrid;
		protected GridControl mparent = null;


		//private CtlMemo memoBox;
		private System.Windows.Forms.Panel panel1;
		private mControl.WinCtl.Controls.CtlPanel panel2;
		private mControl.WinCtl.Controls.CtlButton btnClose;
		private mControl.WinCtl.Controls.CtlButton btnOk;
		private mControl.WinCtl.Controls.CtlButton btnPrint;
		private mControl.WinCtl.Controls.CtlResize ctlResize;
		private IWindowsFormsEditorService edSrv;

		public GridPopUp(GridControl parent,Size size) : this(parent,size,null)
		{

		}

		public GridPopUp(GridControl parent,Size size, IWindowsFormsEditorService edSrv) : base(parent)
		{
			mparent = parent;
	         
			InitializeComponent();
			dataGrid=((GridControl)parent).InternalGrid;
			//this.panel1.Controls.Add(dataGrid);
			//((GridControl)parent).Dock=System.Windows.Forms.DockStyle.Fill ;
		
			initMemo();
			this.edSrv = null;
			try
			{
				base.SuspendLayout();
				this.Size=mparent.popUpSize;
				this.panel1.Size=this.Size;
				this.Localize();
			}
			catch
			{
			}

			
			this.Size =size; 
			this.Height  =this.Height ; 
	
		}


		private void initMemo()
		{
			//			this.txtMemo.RightToLeft=memoBox.RightToLeft;
			//			this.txtMemo.Font=memoBox.Font;
			//			this.txtMemo.Text=memoBox.Text;
		}
 
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad (e);
			base.LockClose = true;
		}

		private void btOther_Click(object sender, EventArgs e)
		{
			base.LockClose = true;

			base.LockClose = false;
		}

		public override void ClosePopupForm()
		{
			base.ClosePopupForm();
			if (!base.LockClose && (this.edSrv != null))
			{
				this.edSrv.CloseDropDown();
			}
		}

		private void DrawItem(bool web, object sender, DrawItemEventArgs e)
		{

		}

		private void InitializeComponent()
		{
			Initialize();
		}

		private void Initialize()
		{
			this.panel1 = new System.Windows.Forms.Panel();
			this.dataGrid = new mControl.GridStyle.Grid();
			this.panel2=new CtlPanel();	// new mControl.WinCtl.Controls.CtlPanel()();
			this.ctlResize = new mControl.WinCtl.Controls.CtlResize();
			this.btnPrint = new mControl.WinCtl.Controls.CtlButton();
			this.btnClose = new mControl.WinCtl.Controls.CtlButton();
			this.btnOk = new mControl.WinCtl.Controls.CtlButton();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Controls.Add(this.dataGrid);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(200, 200);
			this.panel1.TabIndex = 0;
			// 
			// dataGrid
			// 
			this.dataGrid.DataMember = "";
			this.dataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid.Location = new System.Drawing.Point(0, 0);
			this.dataGrid.Name = "dataGrid";
			this.dataGrid.SelectionType = mControl.GridStyle.SelectionTypes.Cell;
			//this.dataGrid.interanlGrid.StylePlan = mControl.WinCtl.Controls.Styles.SteelBlue;
			this.dataGrid.TabIndex = 0;
			// 
			// panel2
			// 
			this.panel2.BorderStyle=BorderStyle.FixedSingle;
			this.panel2.ControlLayout=ControlsLayout.Gradient;
			this.panel2.Controls.Add(this.ctlResize);
			this.panel2.Controls.Add(this.btnPrint);
			this.panel2.Controls.Add(this.btnClose);
			this.panel2.Controls.Add(this.btnOk);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 157);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(208, 40);
			this.panel2.TabIndex = 1;
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(8, 16);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(50, 20);
			this.btnOk.TabIndex = 0;
			this.btnOk.Text = "Ok";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(62, 16);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(50, 20);
			this.btnClose.TabIndex = 1;
			this.btnClose.Text = "Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnPrint
			// 
			this.btnPrint.Location = new System.Drawing.Point(116, 16);
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Size = new System.Drawing.Size(50, 20);
			this.btnPrint.TabIndex = 2;
			this.btnPrint.Text = "Print";
			this.btnPrint.Click +=new EventHandler(btnPrint_Click);
			// 
			// ctlResize
			// 
			this.ctlResize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.ctlResize.BorderStyle = BorderStyle.None;
			this.ctlResize.BackColor=Color.Transparent;
			this.ctlResize.Location = new System.Drawing.Point(182, 16);
			this.ctlResize.Name = "ctlResize";
			this.ctlResize.Size = new System.Drawing.Size(20, 16);
			this.ctlResize.TabIndex = 3;
			this.ctlResize.Text = "ctlResize";
			// 
			// MemoPopupForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(224, 180);
			this.Controls.Add(this.panel1);
			this.Name = "GridPopupForm";
			this.Text = "GridPopupForm";
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize (e);
			if(this.panel1!=null)
			{
				this.panel1.Size=this.Size;
			}
		}

		private void Localize()
		{
		}

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			base.LockClose = true;

			base.LockClose = false;
			this.DialogResult=DialogResult.OK;
			this.Close();

		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			//			if(!this.txtMemo.Text.Equals(memoBox.Text))
			//			{
			//				DialogResult dr= Util.MsgBox.ShowQuestionYNC("Save Changes","Memo");
			//				if(dr==DialogResult.Cancel)
			//					return;
			//				if(dr==DialogResult.Yes)
			//					this.DialogResult=DialogResult.OK;
			//			}
			base.LockClose = true;
			this.Close();
		}

		
		//		public string GetMemoText()
		//		{
		//			return this.txtMemo.Text;
		//		}
		//
		//		private bool printMemo=false;
		//		public bool  PrintMemo()
		//		{
		//			return this.printMemo;
		//		}

		public DialogResult GetResult()
		{
			return this.DialogResult;
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
			//			if(this.txtMemo.TextLength==0)return;
			//			if(!this.txtMemo.Text.Equals(memoBox.Text))
			//			{
			//				DialogResult dr= Util.MsgBox.ShowQuestionYNC("Save Changes","Memo");
			//				if(dr==DialogResult.Cancel)
			//					return;
			//				if(dr==DialogResult.Yes)
			//					this.DialogResult=DialogResult.OK;
			//			}
			//			this.printMemo=true;
			//			base.LockClose = false;
			//			this.Close();
		}

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
