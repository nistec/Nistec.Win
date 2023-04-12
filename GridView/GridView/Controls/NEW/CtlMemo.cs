using System;
using System.Data;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Windows.Forms.Design;
using System.Drawing.Design;
using System.Globalization;  

using mControl.Util;
using mControl.Win32;
using mControl.Drawing;

namespace mControl.WinCtl.Controls
{
	[Designer(typeof(Design.CtlEditDesigner)),ToolboxItem(true),ToolboxBitmap (typeof(CtlDropDown),"Toolbox.Memo_edit.bmp")]
	[DefaultEvent("SelectedIndexChanged")]
	public class CtlMemo : mControl.WinCtl.Controls.CtlButtonEdit//,ICtlTextBox,ICombo,IDropDown
	{

		#region Members
		// Events
		[Category("Behavior")]
		public event EventHandler DropDown;
		[Category("Behavior")]
		public event EventHandler DropUp;

		// Fields
		internal MemoPopupForm memoPopupForm;

		internal Size popUpSize;
	
		#endregion

		#region Ctor

		public CtlMemo()
		{
			this.memoPopupForm = null;
			popUpSize=new Size(200, 200);
			//this.showMemo = true;
		}

		protected override void Dispose(bool disposing)
		{
//			if (disposing && (base.ButtonBitmap != null))
//			{
//				base.ButtonBitmap.Dispose();
//			}
			base.Dispose(disposing);
		}

		#endregion

		#region comb PopUp

		public override void DoDropDown()
		{
			if(m_DroppedDown)
			{
				memoPopupForm.Close();
				m_DroppedDown=false;
				return;
			}	
			ShowPopUp();
		}

		public override void CloseDropDown()
		{
			if(m_DroppedDown)
			{
				memoPopupForm.Close();
			}
		}

		public void ShowPopUp()
		{
	
			if (this.memoPopupForm == null)
			{
				this.InvokeDropDown(EventArgs.Empty);
				this.memoPopupForm = new MemoPopupForm(this);
				Form form1 = base.FindForm();
				if (form1 != null)
				{
					form1.AddOwnedForm(this.memoPopupForm);
				}
				Rectangle rectangle1 = base.RectangleToScreen(base.ClientRectangle);
				this.memoPopupForm.Handle.ToString();
				this.memoPopupForm.Location = new Point(rectangle1.Left, (rectangle1.Top + base.Height) + 1);
				//int num1 = 210;
				//int num2 =232;// 270;
				this.memoPopupForm.Size =this.popUpSize;// new Size(num1, num2);
				if (this.memoPopupForm.Bottom > Screen.PrimaryScreen.WorkingArea.Bottom)
				{
					this.memoPopupForm.Top = (rectangle1.Top - 1) - this.memoPopupForm.Height;
				}
				if (Screen.PrimaryScreen.Bounds.Right < this.memoPopupForm.Right)
				{
					this.memoPopupForm.Left = rectangle1.Right - this.memoPopupForm.Width;
				}
				this.memoPopupForm.Font = this.Font;
				this.memoPopupForm.LockClose = true;
				//this.memoPopupForm.ClosePopup += new EventHandler(this.OnClosePopup);
				this.memoPopupForm.Closed += new EventHandler(this.OnClosePopup);
				this.memoPopupForm.ShowPopupForm();
				//this.memoPopupForm.Focus();
				m_DroppedDown=true;
			}
			else
			{
				this.memoPopupForm.ClosePopupForm();
				this.memoPopupForm = null;
				m_DroppedDown=false;
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
				bool printMemo=this.memoPopupForm.PrintMemo();
				if( this.memoPopupForm.GetResult()==DialogResult.OK)
				{
					this.Text=memoText;
					this.popUpSize=this.memoPopupForm.Size;
	
				}
				this.memoPopupForm.Closed -= new EventHandler(this.OnClosePopup);

				this.memoPopupForm.Dispose();
				this.memoPopupForm = null;
				m_DroppedDown=false;
				if(printMemo)
				{
					this.Print(memoText);
				}
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

		#endregion

		#region override

		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged(e);
			//base.TextBox.Visible = false;
			base.Invalidate();
		}

 
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


		#endregion

		#region Properties



 
		[Browsable(false)]
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

		#endregion

		#region Public methods
		public void Print()
		{
			Print(this.Text);
		}

		public void Print(string text)
		{
			mControl.WinCtl.Printing.PrintDocumentText doc=new mControl.WinCtl.Printing.PrintDocumentText();
			doc.CreateDocument(text);
			doc.Show();
		}
		#endregion
	}
}
 
