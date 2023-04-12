using System;
using System.Data;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Windows.Forms.Design;
using System.Drawing.Design;
using System.Globalization;  


using Nistec.Win32;
using Nistec.Drawing;

namespace Nistec.WinForms
{
    [Designer(typeof(Design.McEditDesigner)), ToolboxItem(true), ToolboxBitmap(typeof(McMemo), "Toolbox.Memo_edit.bmp")]
	[DefaultEvent("SelectedIndexChanged")]
    public class McMemo : Nistec.WinForms.Controls.McComboBase, IMemo//,IMcTextBox,ICombo,IDropDown
	{

        public static Size DefaultMemoSize { get { return new Size(200, 200); } }

        public static void DoDropDown(Control ctl,bool readOnly)
        {
            MemoForm memoPopupForm = new MemoForm(ctl, readOnly,null);
            memoPopupForm.ShowPopupForm();
        }


		#region Members
		// Events
		[Category("Behavior")]
		public event EventHandler DropDown;
		[Category("Behavior")]
		public event EventHandler DropUp;

        public event EventHandler MemoChanged;

		// Fields
		internal MemoForm memoPopupForm;

        private Size popUpSize;
        private bool showToolTip;
        private bool autoSave;
        ///*toolTip*/
        private McToolTip toolTip;
        private ScrollBars scrollBars;
    	#endregion

		#region Ctor

		public McMemo()
		{
            scrollBars = ScrollBars.None;
            autoSave = false;
            showToolTip = true;
			this.memoPopupForm = null;
			popUpSize=new Size(200, 200);
			//this.showMemo = true;

 
		}

		protected override void Dispose(bool disposing)
		{
            if (disposing)// && (base.ButtonBitmap != null))
            {
                //base.ButtonBitmap.Dispose();
                /*toolTip*/
                if (this.toolTip != null)
                {
                    this.toolTip.Dispose();
                    this.toolTip = null;
                }

            }
			base.Dispose(disposing);
		}

		#endregion

		#region comb PopUp

		public override void DoDropDown()
		{
			if(DroppedDown)
			{
                if (memoPopupForm != null)
                {
                    memoPopupForm.Close();
                }
                DroppedDown = false;
              	return;
			}	
			ShowPopUp();
		}

		public override void CloseDropDown()
		{
			if(DroppedDown)
			{
				memoPopupForm.Close();
			}
		}

		public void ShowPopUp()
		{
	
			if (this.memoPopupForm == null)
			{
                if (ReadOnly && this.Text.Length == 0)
                {
                    return;
                }

				this.InvokeDropDown(EventArgs.Empty);
				this.memoPopupForm = new MemoForm(this,this.ReadOnly,null);
                //Form form1 = base.FindForm();
                //if (form1 != null)
                //{
                //    form1.AddOwnedForm(this.memoPopupForm);
                //}
                //Rectangle rectangle1 = base.RectangleToScreen(base.ClientRectangle);
                //this.memoPopupForm.Handle.ToString();
                //this.memoPopupForm.Location = new Point(rectangle1.Left, (rectangle1.Top + base.Height) + 1);
                //if (this.memoPopupForm.Bottom > Screen.PrimaryScreen.WorkingArea.Bottom)
                //{
                //    this.memoPopupForm.Top = (rectangle1.Top - 1) - this.memoPopupForm.Height;
                //}
                //if (Screen.PrimaryScreen.Bounds.Right < this.memoPopupForm.Right)
                //{
                //    this.memoPopupForm.Left = rectangle1.Right - this.memoPopupForm.Width;
                //}
                //this.memoPopupForm.Font = this.Font;
                //this.memoPopupForm.LockClose = true;

				//this.memoPopupForm.ClosePopup += new EventHandler(this.OnClosePopup);
				this.memoPopupForm.Closed += new EventHandler(this.OnClosePopup);
				this.memoPopupForm.ShowPopupForm(this.PopupSize);
				//this.memoPopupForm.Focus();
				DroppedDown=true;
			}
			else
			{
				this.memoPopupForm.ClosePopupForm();
				this.memoPopupForm = null;
				DroppedDown=false;
			}
		}

		#endregion

		#region DropDown methods

		public void InvokeDropDown(EventArgs e)
		{
			this.OnDropDown(e);
		}

		public void InvokeDropUp(EventArgs e)
		{
			this.OnDropUp(e);
		}

//		public void InvokeSelectedIndexChanged(EventArgs e)
//		{
//			this.OnSelectedIndexChanged(e);
//		}

		private void OnClosePopup(object sender, EventArgs e)
		{
			if (this.memoPopupForm != null)
			{
				this.InvokeDropUp(e);
				string memoText=this.memoPopupForm.GetMemoText();
				//bool printMemo=this.memoPopupForm.PrintMemo();
				if( this.memoPopupForm.GetResult()==DialogResult.OK)
				{
					this.Text=memoText;
					this.popUpSize=this.memoPopupForm.Size;
                    //this.ResetToolTip();
                    this.Invalidate();
                    OnMemoChanged(e);
				}
				this.memoPopupForm.Closed -= new EventHandler(this.OnClosePopup);

				this.memoPopupForm.Dispose();
				this.memoPopupForm = null;
				DroppedDown=false;
                //if(printMemo)
                //{
                //    this.Print(memoText);
                //}
			}
		}

		protected virtual void OnDropDown(EventArgs e)
		{
			if (this.DropDown != null)
			{
				this.DropDown(this, e);
			}
		}

		protected virtual void OnDropUp(EventArgs e)
		{
			if (this.DropUp != null)
			{
				this.DropUp(this, e);
			}
		}

        protected virtual void OnMemoChanged(EventArgs e)
        {
            if (MemoChanged != null)
                MemoChanged(this, e);
        }

		#endregion

		#region override

        //private void ResetToolTip()
        //{
        //    if (showToolTip )
        //    {
        //        McToolTip.Instance.ClearToolTip(this);

        //        if (!string.IsNullOrEmpty(this.Text))

        //            McToolTip.Instance.SetToolTip(this, Text);
        //    }

        //}

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            /*toolTip*/
            if (showToolTip && (this.toolTip == null))
            {
                this.toolTip = new McToolTip();
            }
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            /*toolTip*/
            if ((!base.DesignMode) && (this.ShowToolTip && (this.toolTip != null)))
            {
                if (!string.IsNullOrEmpty(this.Text))
                {
                    this.toolTip.Show(Text, this);
                    //McToolTip.Instance.Show(Text,this);
                }
            }

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            /*toolTip*/
            if (this.toolTip != null)
            {
                this.toolTip.Hide(this);
            }
            //McToolTip.Instance.Hide(this);
            this.Invalidate();
            base.OnMouseLeave(e);
        }
        //protected override void OnEnabledChanged(EventArgs e)
        //{
        //    base.OnEnabledChanged(e);
        //    //base.TextBox.Visible = false;
        //    base.Invalidate();
        //}

 		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if ((e.Button & MouseButtons.Left) > MouseButtons.None)
			{
				ShowPopUp();
			}
			else
			{
				if (this.memoPopupForm != null)
				{
					this.memoPopupForm.ClosePopupForm();
				}
				this.memoPopupForm = null;
			}
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if (this.memoPopupForm != null)
			{
				this.memoPopupForm.LockClose = false;
			}
		}

        protected void CustomPaint(PaintEventArgs e)
        {
           // base.OnPaint(e);

            string selectedTxt = this.Text;

            if (selectedTxt.Length == 0)
                return;

            int offset = 2;
            bool isLeft = this.ButtonAlign == ButtonAlign.Left;
            int ItemHeight =/*ctlLayout== ControlLayout.Flat ? Height:*/ Height - 7;
            int top = (Height - ItemHeight) / 2;
            int btnWidth = base.GetButtonRect.Width;
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            Rectangle bounds = new Rectangle(rect.X + 2, rect.Y + 2, rect.Width - btnWidth - 5, rect.Height - 4);
            //Rectangle txtRect = new Rectangle(2,top,rect.Width-2-btnWidth,ItemHeight);

            if (isLeft)
            {
                bounds = new Rectangle(rect.X + btnWidth + 3, rect.Y + 2, rect.Width - btnWidth - 5, rect.Height - 4);
            }

            Brush brushtxt;
            bounds.Width -= 1;
            bounds.Height -= 1;

            switch (base.ControlLayout)
            {
                case ControlLayout.System:
                    e.Graphics.FillRectangle(LayoutManager.GetBrushCaption(), bounds);
                    e.Graphics.DrawRectangle(LayoutManager.GetPenFocused(), bounds);

                    brushtxt = new SolidBrush(Color.White);
                    break;
                case ControlLayout.Flat:
                    offset = 0;
                    ItemHeight=Height-2;
                    top = 1;
                    //txtRect = new Rectangle(0, 1, rect.Width - 2 - btnWidth, Height-2);
                    brushtxt = LayoutManager.GetBrushText();
                    break;
                default:
                    e.Graphics.FillRectangle(LayoutManager.GetBrushSelected(), bounds);
                    e.Graphics.DrawRectangle(Pens.DarkGray, bounds);
                    brushtxt = LayoutManager.GetBrushText();
                    break;
            }
            Rectangle txtRect = new Rectangle(offset, top, rect.Width - 2 - btnWidth, ItemHeight);

            if (isLeft)
            {
                txtRect.X += btnWidth + offset;
            }

   
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            if (this.TextAlign == HorizontalAlignment.Center)
                sf.Alignment = StringAlignment.Center;
            else if (this.TextAlign == HorizontalAlignment.Left)
                sf.Alignment = StringAlignment.Near;
            else if (this.TextAlign == HorizontalAlignment.Right)
                sf.Alignment = StringAlignment.Far;

            e.Graphics.DrawString(this.Text, Font, brushtxt, txtRect, sf);
            sf.Dispose();
            brushtxt.Dispose();

        }

		#endregion

		#region Properties

        [Browsable(true), Bindable(true)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

        [DefaultValue(true)]
        public bool ShowToolTip
        {
            get
            {
                return showToolTip;
            }
            set
            {
                showToolTip = value;
            }
        }
        [DefaultValue(typeof(Size),"200,200")]
        public Size PopupSize
        {
            get
            {
                return popUpSize;
            }
            set
            {
                popUpSize = value;
            }
        }
        [DefaultValue(false)]
        public bool AutoSave
        {
            get
            {
                return autoSave;
            }
            set
            {
                autoSave = value;
            }
        }

        [DefaultValue(ScrollBars.None)]
        public ScrollBars ScrollBars
        {
            get
            {
                return scrollBars;
            }
            set
            {
                scrollBars = value;
            }
        }
		#endregion

        #region StyleProperty

        [Category("Style"), DefaultValue(typeof(Color), "WindowText")]
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set
            {
                SerializeForeColor(value, true);
            }
        }

        [Category("Style"), DefaultValue(typeof(Color), "Window")]
        public override Color BackColor
        {
            get { return base.BackColor; }
            set
            {
                SerializeBackColor(value, true);
            }
        }

        protected override void OnStylePropertyChanged(PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("ControlLayout") || e.PropertyName.Equals("BackColor"))
                SerializeBackColor(Color.Empty, false);
            if (e.PropertyName.Equals("ControlLayout") || e.PropertyName.Equals("ForeColor"))
                SerializeForeColor(Color.Empty, false);
            if (e.PropertyName.Equals("ControlLayout") || e.PropertyName.Equals("TextFont"))
                SerializeFont(Form.DefaultFont, false);
            if ((DesignMode || IsHandleCreated))
                this.Invalidate(true);
        }
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //protected void SerializeFont(Font value, bool force)
        //{
        //    if (ShouldSerializeForeColor())
        //        this.Font = LayoutManager.Layout.TextFontInternal;
        //    else if (force)
        //        this.Font = value;
        //}

        //[EditorBrowsable(EditorBrowsableState.Never)]
        //protected void SerializeForeColor(Color value, bool force)
        //{
        //    if (ShouldSerializeForeColor())
        //    {
        //        base.ForeColor = LayoutManager.Layout.ForeColorInternal;
        //    }
        //    else if (force)
        //    {
        //        base.ForeColor = value;
        //    }
        //}

        //[EditorBrowsable(EditorBrowsableState.Never)]
        //protected void SerializeBackColor(Color value, bool force)
        //{
        //    if (ShouldSerializeBackColor())
        //    {
        //        base.BackColor = LayoutManager.Layout.BackColorInternal;
        //    }
        //    else if (force)
        //    {
        //        base.BackColor = value;
        //    }
        //}

        //[EditorBrowsable(EditorBrowsableState.Never)]
        //protected bool ShouldSerializeBackColor()
        //{
        //    return IsHandleCreated && StylePainter != null;
        //}

        //[EditorBrowsable(EditorBrowsableState.Never)]
        //protected bool ShouldSerializeForeColor()
        //{
        //    return IsHandleCreated && StylePainter != null;
        //}

        #endregion

		#region Public methods
		public void Print()
		{
			Print(this.Text);
		}

		public void Print(string text)
		{
			Nistec.Printing.ReportBuilder.PrintTextDocument( text,"","",true);
		}
		#endregion
	}
}
 
