using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;
using System.Windows.Forms;

using MControl.Win32;
using MControl.Util;

using MControl.Drawing;

using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using MControl.WinForms.Controls;

namespace MControl.WinForms
{

	public class MemoPopupForm : McPopUpBase
	{

		// Fields
        private IPopUp memoBox;
        private Control owner;
        private MControl.WinForms.McPanel panel1;
		private MControl.WinForms.McPanel panel2;
		private MControl.WinForms.McTextBox txtMemo;
		private MControl.WinForms.McButton btnClose;
		private MControl.WinForms.McButton btnOk;
		private MControl.WinForms.McButton btnPrint;
		private MControl.WinForms.McResize ctlResize;
		private IWindowsFormsEditorService edSrv;
        private bool enabledChanged = false;
        private bool enabledSatrted = false;


		//		public MemoPopupForm() 
		//		{
		//		}


  
		public MemoPopupForm(Control memo) : this(memo, null)
		{
		}

        public MemoPopupForm(Control memo, IWindowsFormsEditorService edSrv)
            : base(memo)
		{
            if (!(memo is IPopUp))
            {
                throw new ArgumentException("memo should be IPopUp");
            }
            this.owner = memo;
            this.memoBox =(IPopUp)memo;
            InitializeComponent();
			//initMemo();

            this.txtMemo.RightToLeft = memo.RightToLeft;
            this.txtMemo.Font = memo.Font;
            this.txtMemo.Text = memo.Text;

            this.txtMemo.ReadOnly = memoBox.ReadOnly;
            //this.btnOk.Enabled = !this.txtMemo.ReadOnly;
            enabledSatrted = !this.txtMemo.ReadOnly;


			this.edSrv = null;
            try
            {
                base.SuspendLayout();
                int width = memo.Width;
                Size memoSize = memoBox.PopupSize;
                if (width < memoSize.Width)
                    this.Size = memoBox.PopupSize;
                else
                    this.Size = new Size(width, memoSize.Height);

                this.panel1.Size = this.Size;
                this.Localize();
            }
            catch
            {
            }
		}

        public override void ShowPopupForm()
        {

            if (txtMemo.ReadOnly && txtMemo.Text.Length == 0)
            {
                return;
            }

            //this.InvokeDropDown(EventArgs.Empty);
            Form form1 = owner.FindForm();
            if (form1 != null)
            {
                form1.AddOwnedForm(this);
            }
            Rectangle rectangle1 = owner.RectangleToScreen(owner.ClientRectangle);
            this.Handle.ToString();
            this.Location = new Point(rectangle1.Left, (rectangle1.Top + owner.Height) + 1);
            if (this.Bottom > Screen.PrimaryScreen.WorkingArea.Bottom)
            {
                this.Top = (rectangle1.Top - 1) - this.Height;
            }
            if (Screen.PrimaryScreen.Bounds.Right < this.Right)
            {
                this.Left = rectangle1.Right - this.Width;
            }
            //this.Font = memoBox.Font;
            this.LockClose = true;
            //this.memoPopupForm.ClosePopup += new EventHandler(this.OnClosePopup);
            //this.Closed += new EventHandler(this.OnClosePopup);
            base.ShowPopupForm();
            //this.memoPopupForm.Focus();
            //DroppedDown = true;
        }

		public override object SelectedItem
		{
			get {return  this.txtMemo.Text as object;}
		}

		//		public override int SelectedIndex
		//		{
		//			get {return 0;}
		//		}

        //private void initMemo()
        //{
        //    this.txtMemo.RightToLeft=memoBox.RightToLeft;
        //    this.txtMemo.Font=memoBox.Font;
        //    this.txtMemo.Text=memoBox.Text;
        //    this.txtMemo.ReadOnly = memoBox.ReadOnly;
        //    //this.btnOk.Enabled = !this.txtMemo.ReadOnly;
        //    enabledSatrted = !this.txtMemo.ReadOnly;
        //}
 
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
            this.panel1 = new McPanel();
			this.txtMemo = new MControl.WinForms.McTextBox();
			this.panel2=new McPanel();	// new MControl.WinForms.McPanel()();
			this.ctlResize = new MControl.WinForms.McResize();
			this.btnPrint = new MControl.WinForms.McButton();
			this.btnClose = new MControl.WinForms.McButton();
			this.btnOk = new MControl.WinForms.McButton();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
            this.panel1.autoChildrenStyle = true;
            this.panel1.Controls.Add(this.panel2);
			this.panel1.Controls.Add(this.txtMemo);
            this.panel1.Controls.SetChildIndex(this.panel2,0);
            this.panel1.Controls.SetChildIndex(this.txtMemo,0);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(200, 200);
            this.panel1.StylePainter = memoBox.StylePainter;
			this.panel1.TabIndex = 0;
			// 
			// txtMemo
			// 
			this.txtMemo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtMemo.Location = new System.Drawing.Point(0, 0);
			this.txtMemo.Multiline = true;
			this.txtMemo.Name = "txtMemo";
            this.txtMemo.ScrollBars = ScrollBars.Vertical;
            this.txtMemo.Multiline = true;
            this.panel1.StylePainter = memoBox.StylePainter;
            this.txtMemo.TabIndex = 0;
			this.txtMemo.Text = "txtMemo";
            this.txtMemo.TextChanged += new EventHandler(txtMemo_TextChanged);
			// 
			// panel2
			// 
			this.panel2.BorderStyle=BorderStyle.FixedSingle;
			this.panel2.ControlLayout=ControlLayout.Visual;
			this.panel2.Controls.Add(this.ctlResize);
			this.panel2.Controls.Add(this.btnPrint);
			this.panel2.Controls.Add(this.btnClose);
			this.panel2.Controls.Add(this.btnOk);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 157);
			this.panel2.Name = "panel2";
            this.panel2.StylePainter = memoBox.StylePainter;
            this.panel2.Size = new System.Drawing.Size(208, 40);
			this.panel2.TabIndex = 1;
			// 
			// btnOk
			// 
            this.btnOk.Enabled = false;
            this.btnOk.Location = new System.Drawing.Point(8, 16);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(50, 20);
            this.btnOk.StylePainter = memoBox.StylePainter;
            this.btnOk.TabIndex = 0;
			this.btnOk.Text = "Ok";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(62, 16);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(50, 20);
            this.btnClose.StylePainter = memoBox.StylePainter;
            this.btnClose.TabIndex = 1;
			this.btnClose.Text = "Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnPrint
			// 
			this.btnPrint.Location = new System.Drawing.Point(116, 16);
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Size = new System.Drawing.Size(50, 20);
            this.btnPrint.StylePainter = memoBox.StylePainter;
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
            this.ctlResize.StylePainter = memoBox.StylePainter;
            this.ctlResize.TabIndex = 3;
			this.ctlResize.Text = "ctlResize";
            this.ctlResize.LocationChanged += new EventHandler(ctlResize_LocationChanged);
            this.ctlResize.Resizing += new EventHandler(ctlResize_Resizing);
            // 
			// MemoPopupForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(224, 180);
			this.Controls.Add(this.panel1);
			this.Name = "MemoPopupForm";
            //this.SizeGripStyle = SizeGripStyle.Show;
            this.Text = "MemoPopupForm";
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

        void txtMemo_TextChanged(object sender, EventArgs e)
        {
            if (!enabledSatrted) 
                return;
            if (enabledChanged) 
                return;
            this.btnOk.Enabled = true;
            enabledChanged = true;
        }

        void ctlResize_Resizing(object sender, EventArgs e)
        {
            //if (this.panel1 != null)
            //{
            //    this.panel1.Size = ctlResize.CurrentSize;
            //}
        }

        void ctlResize_LocationChanged(object sender, EventArgs e)
        {
            //throw new Exception("The method or operation is not implemented.");

        }

		protected override void OnResize(EventArgs e)
		{
			base.OnResize (e);
			if(this.panel1!=null)
			{
				this.panel1.Size=this.Size;
			}
		}

		//		private void InitializeOld()
		//		{
		//			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MemoPopupForm));
		//			this.txtMemo = new MControl.WinForms.McTextBox();
		//			this.panel1 = new MControl.WinForms.McPanel();
		//			this.ctlResize = new MControl.WinForms.McResize();
		//			this.btnPrint = new MControl.WinForms.McButton();
		//			this.btnClose = new MControl.WinForms.McButton();
		//			this.btnOk = new MControl.WinForms.McButton();
		//			this.panel1.SuspendLayout();
		//			this.SuspendLayout();
		//			// 
		//			// txtMemo
		//			// 
		//			this.txtMemo.Dock = System.Windows.Forms.DockStyle.Fill;
		//			this.txtMemo.Location = new Point(0,0);
		//			this.txtMemo.Multiline = true;
		//			this.txtMemo.Name = "txtMemo";
		//			this.txtMemo.Size = new Size(224,200);
		//			this.txtMemo.TabIndex = 0;
		//			this.txtMemo.Text = "";
		//			// 
		//			// panel1
		//			// 
		//			this.panel1.AccessibleDescription = resources.GetString("panel1.AccessibleDescription");
		//			this.panel1.AccessibleName = resources.GetString("panel1.AccessibleName");
		//			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("panel1.Anchor")));
		//			this.panel1.AutoScroll = ((bool)(resources.GetObject("panel1.AutoScroll")));
		//			this.panel1.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("panel1.AutoScrollMargin")));
		//			this.panel1.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("panel1.AutoScrollMinSize")));
		//			this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
		//			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		//			this.panel1.Controls.Add(this.ctlResize);
		//			this.panel1.Controls.Add(this.btnPrint);
		//			this.panel1.Controls.Add(this.btnClose);
		//			this.panel1.Controls.Add(this.btnOk);
		//			this.panel1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("panel1.Dock")));
		//			this.panel1.Enabled = ((bool)(resources.GetObject("panel1.Enabled")));
		//			this.panel1.Font = ((System.Drawing.Font)(resources.GetObject("panel1.Font")));
		//			this.panel1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("panel1.ImeMode")));
		//			this.panel1.Location = ((System.Drawing.Point)(resources.GetObject("panel1.Location")));
		//			this.panel1.Name = "panel1";
		//			this.panel1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("panel1.RightToLeft")));
		//			this.panel1.Size = ((System.Drawing.Size)(resources.GetObject("panel1.Size")));
		//			this.panel1.TabIndex = ((int)(resources.GetObject("panel1.TabIndex")));
		//			this.panel1.Text = resources.GetString("panel1.Text");
		//			this.panel1.Visible = ((bool)(resources.GetObject("panel1.Visible")));
		//			// 
		//			// ctlResize
		//			// 
		//			this.ctlResize.AccessibleDescription = resources.GetString("ctlResize.AccessibleDescription");
		//			this.ctlResize.AccessibleName = resources.GetString("ctlResize.AccessibleName");
		//			this.ctlResize.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ctlResize.Anchor")));
		//			this.ctlResize.BorderStyle = System.Windows.Forms.BorderStyle.None;
		//			this.ctlResize.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
		//			this.ctlResize.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ctlResize.Dock")));
		//			this.ctlResize.Enabled = ((bool)(resources.GetObject("ctlResize.Enabled")));
		//			this.ctlResize.Font = ((System.Drawing.Font)(resources.GetObject("ctlResize.Font")));
		//			this.ctlResize.Image = ((System.Drawing.Image)(resources.GetObject("ctlResize.Image")));
		//			this.ctlResize.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ctlResize.ImeMode")));
		//			this.ctlResize.Location = ((System.Drawing.Point)(resources.GetObject("ctlResize.Location")));
		//			this.ctlResize.Name = "ctlResize";
		//			this.ctlResize.ReadOnly = false;
		//			this.ctlResize.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ctlResize.RightToLeft")));
		//			this.ctlResize.Size = ((System.Drawing.Size)(resources.GetObject("ctlResize.Size")));
		//			this.ctlResize.SizeMode = ((System.Windows.Forms.PictureBoxSizeMode)(resources.GetObject("ctlResize.SizeMode")));
		//			this.ctlResize.TabIndex = ((int)(resources.GetObject("ctlResize.TabIndex")));
		//			this.ctlResize.Text = resources.GetString("ctlResize.Text");
		//			this.ctlResize.Visible = ((bool)(resources.GetObject("ctlResize.Visible")));
		//			// 
		//			// btnPrint
		//			// 
		//			this.btnPrint.AccessibleDescription = resources.GetString("btnPrint.AccessibleDescription");
		//			this.btnPrint.AccessibleName = resources.GetString("btnPrint.AccessibleName");
		//			this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnPrint.Anchor")));
		//			this.btnPrint.ControlLayout = MControl.WinForms.ControlLayout.XpLayout;
		//			this.btnPrint.DialogResult = System.Windows.Forms.DialogResult.None;
		//			this.btnPrint.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnPrint.Dock")));
		//			this.btnPrint.Enabled = ((bool)(resources.GetObject("btnPrint.Enabled")));
		//			this.btnPrint.FixSize = false;
		//			this.btnPrint.Font = ((System.Drawing.Font)(resources.GetObject("btnPrint.Font")));
		//			this.btnPrint.ImageIndex = ((int)(resources.GetObject("btnPrint.ImageIndex")));
		//			this.btnPrint.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnPrint.ImeMode")));
		//			this.btnPrint.Location = ((System.Drawing.Point)(resources.GetObject("btnPrint.Location")));
		//			this.btnPrint.Name = "btnPrint";
		//			this.btnPrint.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnPrint.RightToLeft")));
		//			this.btnPrint.Size = ((System.Drawing.Size)(resources.GetObject("btnPrint.Size")));
		//			this.btnPrint.TabIndex = ((int)(resources.GetObject("btnPrint.TabIndex")));
		//			this.btnPrint.Text = resources.GetString("btnPrint.Text");
		//			this.btnPrint.Visible = ((bool)(resources.GetObject("btnPrint.Visible")));
		//			// 
		//			// btnClose
		//			// 
		//			this.btnClose.AccessibleDescription = resources.GetString("btnClose.AccessibleDescription");
		//			this.btnClose.AccessibleName = resources.GetString("btnClose.AccessibleName");
		//			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnClose.Anchor")));
		//			this.btnClose.ControlLayout = MControl.WinForms.ControlLayout.XpLayout;
		//			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.None;
		//			this.btnClose.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnClose.Dock")));
		//			this.btnClose.Enabled = ((bool)(resources.GetObject("btnClose.Enabled")));
		//			this.btnClose.FixSize = false;
		//			this.btnClose.Font = ((System.Drawing.Font)(resources.GetObject("btnClose.Font")));
		//			this.btnClose.ImageIndex = ((int)(resources.GetObject("btnClose.ImageIndex")));
		//			this.btnClose.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnClose.ImeMode")));
		//			this.btnClose.Location = ((System.Drawing.Point)(resources.GetObject("btnClose.Location")));
		//			this.btnClose.Name = "btnClose";
		//			this.btnClose.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnClose.RightToLeft")));
		//			this.btnClose.Size = ((System.Drawing.Size)(resources.GetObject("btnClose.Size")));
		//			this.btnClose.TabIndex = ((int)(resources.GetObject("btnClose.TabIndex")));
		//			this.btnClose.Text = resources.GetString("btnClose.Text");
		//			this.btnClose.Visible = ((bool)(resources.GetObject("btnClose.Visible")));
		//			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
		//			// 
		//			// btnOk
		//			// 
		//			this.btnOk.AccessibleDescription = resources.GetString("btnOk.AccessibleDescription");
		//			this.btnOk.AccessibleName = resources.GetString("btnOk.AccessibleName");
		//			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("btnOk.Anchor")));
		//			this.btnOk.ControlLayout = MControl.WinForms.ControlLayout.XpLayout;
		//			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.None;
		//			this.btnOk.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("btnOk.Dock")));
		//			this.btnOk.Enabled = ((bool)(resources.GetObject("btnOk.Enabled")));
		//			this.btnOk.FixSize = false;
		//			this.btnOk.Font = ((System.Drawing.Font)(resources.GetObject("btnOk.Font")));
		//			this.btnOk.ImageIndex = ((int)(resources.GetObject("btnOk.ImageIndex")));
		//			this.btnOk.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("btnOk.ImeMode")));
		//			this.btnOk.Location = ((System.Drawing.Point)(resources.GetObject("btnOk.Location")));
		//			this.btnOk.Name = "btnOk";
		//			this.btnOk.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("btnOk.RightToLeft")));
		//			this.btnOk.Size = ((System.Drawing.Size)(resources.GetObject("btnOk.Size")));
		//			this.btnOk.TabIndex = ((int)(resources.GetObject("btnOk.TabIndex")));
		//			this.btnOk.Text = resources.GetString("btnOk.Text");
		//			this.btnOk.Visible = ((bool)(resources.GetObject("btnOk.Visible")));
		//			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
		//			// 
		//			// MemoPopupForm
		//			// 
		//			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
		//			this.AccessibleName = resources.GetString("$this.AccessibleName");
		//			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
		//			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
		//			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
		//			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
		//			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
		//			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
		//			this.Controls.Add(this.panel1);
		//			this.Controls.Add(this.txtMemo);
		//			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
		//			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
		//			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		//			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
		//			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
		//			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
		//			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
		//			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
		//			this.Name = "MemoPopupForm";
		//			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
		//			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
		//			this.Text = resources.GetString("$this.Text");
		//			this.panel1.ResumeLayout(false);
		//			this.ResumeLayout(false);
		//
		//		}
		//
		// 
		private void Localize()
		{
		}

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			base.LockClose = true;

			//base.LockClose = false;
			this.DialogResult=DialogResult.OK;
			this.Close();

		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			if(!this.txtMemo.Text.Equals(memoBox.Text))
			{
				DialogResult dr= MsgBox.ShowQuestionYNC("Save Changes","Memo");
				if(dr==DialogResult.Cancel)
					return;
				if(dr==DialogResult.Yes)
					this.DialogResult=DialogResult.OK;
			}
			base.LockClose = true;
			this.Close();
		}

		
		public string GetMemoText()
		{
			return this.txtMemo.Text;
		}

		private bool printMemo=false;
		public bool  PrintMemo()
		{
			return this.printMemo;
		}

		public DialogResult GetResult()
		{
			return this.DialogResult;
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
			if(this.txtMemo.TextLength==0)return;
			if(!this.txtMemo.Text.Equals(memoBox.Text))
			{
				DialogResult dr= MsgBox.ShowQuestionYNC("Save Changes","Memo");
				if(dr==DialogResult.Cancel)
					return;
				if(dr==DialogResult.Yes)
					this.DialogResult=DialogResult.OK;
			}
			this.printMemo=true;
			base.LockClose = false;
			this.Close();
		}
	}
}
