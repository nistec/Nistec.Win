using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Nistec.Win;

namespace Nistec.WinForms
{
    public partial class MemoForm : Nistec.WinForms.Controls.McPopUpBase
    {
        public MemoForm()
        {
            InitializeComponent();
        }

        private Control owner;
        private Form form;
        private System.Windows.Forms.Design.IWindowsFormsEditorService edSrv;
        private bool enabledChanged = false;
        private bool enabledSatrted = false;
        protected bool autoSave=true;
        protected bool activate=true;
        protected ScrollBars scrollBars;
      
		public MemoForm(Control memo) : this(memo,false, null)
		{
		}

        public MemoForm(Control memo,bool readOnly, System.Windows.Forms.Design.IWindowsFormsEditorService edSrv)
            : base(memo)
		{
            //if (!(memo is IPopUp))
            //{
            //    throw new ArgumentException("memo should be IPopUp");
            //}
            this.owner = memo;
            //this.memoBox =(IPopUp)memo;
            InitializeComponent();
			//initMemo();
            if (memo is ILayout)
            {
                this.toolBar.StylePainter = ((ILayout)memo).StylePainter;
                this.txtMemo.StylePainter = ((ILayout)memo).StylePainter;
            }
            if (memo is IMemo)
            {
                this.autoSave = ((IMemo)memo).AutoSave;
                scrollBars = ((IMemo)memo).ScrollBars;
            }
            this.txtMemo.RightToLeft = memo.RightToLeft;
            this.txtMemo.Font = memo.Font;
            this.txtMemo.Text = memo.Text;
            this.txtMemo.MaxLength = 2147483600;
            this.txtMemo.ReadOnly = readOnly;// memoBox.ReadOnly;
            enabledSatrted = !this.txtMemo.ReadOnly;

            this.txtMemo.ScrollBars = scrollBars;


            form = owner.FindForm();
            formWire();

			this.edSrv = edSrv;
		}

        private void formWire()
        {
            if (form != null)
            {
                form.AddOwnedForm(this);
                form.Move += new EventHandler(form_Leave);
                form.MouseDown += new MouseEventHandler(form_MouseDown);
                form.Deactivate += new EventHandler(form_Leave);
                form.Leave += new EventHandler(form_Leave);
            }
        }

     
        private void formUnWire()
        {
            if (form != null)
            {
                form.RemoveOwnedForm(this);
                form.Move -= new EventHandler(form_Leave);
                form.MouseDown -= new MouseEventHandler(form_MouseDown);
                form.Deactivate -= new EventHandler(form_Leave);
                form.Leave -= new EventHandler(form_Leave);
            }
        }

        void form_Leave(object sender, EventArgs e)
        {
            if (activate)
                return;
            formClose();
        }

 
        void form_MouseDown(object sender, MouseEventArgs e)
        {
            formClose();
        }

         private void formClose()
        {
            if (this.txtMemo.Text == "")
                this.txtMemo.Text = owner.Text;
            DoClose();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            activate = true;
        }
        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            activate=false;
        }

        public override void ShowPopupForm()
        {
            Size size;
            int width = owner.Width;
            if (owner is IMemo)
            {
                IMemo memo = (IMemo)this.owner;
                if (width < memo.PopupSize.Width)
                    size = memo.PopupSize;
                else
                    size = new Size(width, memo.PopupSize.Height);
            }
            else
            {
                size = new Size(width, McMemo.DefaultMemoSize.Height);
            }
            ShowPopupForm(size);

        }

        public void ShowPopupForm(Size memoSize)
        {

            if (txtMemo.ReadOnly && txtMemo.Text.Length == 0)
            {
                return;
            }

            this.Size = memoSize;

            //this.InvokeDropDown(EventArgs.Empty);
            //Form form1 = owner.FindForm();
            //if (form1 != null)
            //{
            //    form1.AddOwnedForm(this);
            //}
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

 
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad (e);
			base.LockClose = true;
		}
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            formUnWire();
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


        void txtMemo_TextChanged(object sender, EventArgs e)
        {
            if (!enabledSatrted)
                return;
            if (enabledChanged)
                return;
            this.tbSave.Enabled = !txtMemo.ReadOnly;
            enabledChanged = true;
        }

  
        public string GetMemoText()
        {
            return this.txtMemo.Text;
        }

        //private bool printMemo = false;
        //public bool PrintMemo()
        //{
        //    return this.printMemo;
        //}

        public DialogResult GetResult()
        {
            return this.DialogResult;
        }

        private void DoSave()
        {
            base.LockClose = true;

            //base.LockClose = false;
            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void DoClose()
        {

            if (!autoSave && !this.txtMemo.Text.Equals(owner.Text))
            {
                DialogResult dr = MsgBox.ShowQuestionYNC("Save Changes", "Memo");
                if (dr == DialogResult.Cancel)
                    return;
                if (dr == DialogResult.Yes)
                    this.DialogResult = DialogResult.OK;
            }
            base.LockClose = true;
            this.Close();
        }

        private void DoPrint()
        {
            if (this.txtMemo.TextLength == 0) return;
            string text = this.txtMemo.Text;
            if (!autoSave && !this.txtMemo.Text.Equals(owner.Text))
            {
                DialogResult dr = MsgBox.ShowQuestionYNC("Save Changes", "Memo");
                if (dr == DialogResult.Cancel)
                    return;
                if (dr == DialogResult.Yes)
                    this.DialogResult = DialogResult.OK;
            }
            //this.printMemo = true;
            base.LockClose = false;
            this.Close();
            if (text.Length > 0)
            {
                Nistec.Printing.ReportBuilder.PrintTextDocument(text, "", "", true);
            }
        }

        void toolBar_ButtonClick(object sender, ToolButtonClickEventArgs e)
        {
            switch (e.Button.Name)
            {
                case "tbClose":
                    DoClose();
                    break;
                case "tbSave":
                    DoSave();
                    break;
                case "tbPrint":
                    DoPrint();
                    break;
            }
        }

    }
}