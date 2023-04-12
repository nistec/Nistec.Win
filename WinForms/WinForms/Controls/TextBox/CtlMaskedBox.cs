using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Collections;
using System.Security.Permissions;
  

using Nistec.Data;
using Nistec.Win32;  
   
namespace Nistec.WinForms
{


    [ToolboxItem(true), Designer(typeof(Design.ControlDesignerBase))]
	[ToolboxBitmap(typeof(McMaskedBox),"Toolbox.MaskedBox.bmp")]
	public class McMaskedBox: Nistec.WinForms.Controls.McMaskedBase,IMcTextBox,ILayout
	{	

		#region Members
		protected bool ProcessKeyAction;
		//protected KeyAction m_KeyAction;

		public new event MessageEventHandler ProccessMessage;
		#endregion

		#region Constructor

		public McMaskedBox(): base()
		{
			ProcessKeyAction=false;
			BorderStyle=BorderStyle.FixedSingle; 
			//m_KeyAction=new KeyAction (this);
			this.SetStyle(ControlStyles.DoubleBuffer, true);

			//Nistec.Util.Net.netWinMc.NetFram(this.Name); 	
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated (e);
            //base.BackColor=LayoutManager.Layout.BackColorInternal;
            //base.ForeColor=LayoutManager.Layout.ForeColorInternal;
			//Nistec.Util.Net.netWinMc.NetFram(this.Name); 	

		}


		#endregion

		#region WndProc

		public override bool OnWndProc(ref Message m)
		{
			if(ProccessMessage != null)
			{
				MessageEventArgs evtArgs = new MessageEventArgs(m, false);
				ProccessMessage(this, evtArgs);
				
				return evtArgs.Result;
			}

			return false;
		}

		[SecurityPermission(SecurityAction.LinkDemand)]
		protected override void WndProc(ref Message m)
		{

//			try
//			{
//				if(!OnWndProc(ref m))
//				{				
//					base.WndProc(ref m);
//				}
//			}
//			catch(Exception ex)
//			{
//				MsgBox.ShowError (ex.Message );
//			}

			
			if(BorderStyle!=BorderStyle.FixedSingle)
			{
				base.WndProc(ref m);
				return;
			}

			base.WndProc(ref m);

			IntPtr hDC = IntPtr.Zero;
			Graphics gdc = null;
		
			switch (m.Msg)
			{
				case WinMsgs.WM_MOUSEHOVER:	
				case WinMsgs.WM_MOUSEMOVE:	
					hDC = WinAPI.GetWindowDC(this.Handle);
					gdc = Graphics.FromHdc(hDC);
					//gdc.DrawRectangle (new Pen (BackColor,2),new Rectangle(0, 0, this.Width, this.Height));
					PaintFlatControl(gdc,true);
					WinAPI.ReleaseDC(m.HWnd, hDC);
					gdc.Dispose();	
					break;
				case WinMsgs.WM_SETFOCUS :	
				case WinMsgs.WM_KILLFOCUS :	
				case WinMsgs.WM_MOUSELEAVE:	
				case WinMsgs.WM_PAINT:	
					hDC = WinAPI.GetWindowDC(this.Handle);
					gdc = Graphics.FromHdc(hDC);
					//gdc.DrawRectangle (new Pen (BackColor,2),new Rectangle(0, 0, this.Width, this.Height));
					PaintFlatControl(gdc,false);
					WinAPI.ReleaseDC(m.HWnd, hDC);
					gdc.Dispose();	
					break;
			}
		}

		//		private void SendPrintClientMsg()
		//		{
		//			// We send this message for the control to redraw the client area
		//			Graphics gClient = this.CreateGraphics();
		//			IntPtr ptrClientDC = gClient.GetHdc();
		//			WinAPI.SendMessage(this.Handle, Win32.Msgs.WM_PRINTCLIENT, ptrClientDC, 0);
		//			gClient.ReleaseHdc(ptrClientDC);
		//			gClient.Dispose();
		//		}


		private void PaintFlatControl(Graphics g,bool hot)
		{

			Rectangle rect = new Rectangle(0, 0, this.Width-1, this.Height-1);
			this.LayoutManager.DrawBorder(g,rect,this.ReadOnly,this.Enabled,this.Focused,hot);

//			Rectangle rect =this.ClientRectangle;//  new Rectangle(0, 0, this.Width, this.Height);
//			//g.DrawRectangle (new Pen (BackColor,2),rect);
//			if (! this.Enabled  )
//				ControlPaint.DrawBorder(g, rect, SystemColors.Control , ButtonBorderStyle.Solid  );
//			else if (this.ContainsFocus)// Focused )
//				ControlPaint.DrawBorder(g, rect, m_Style.FocusedColor , ButtonBorderStyle.Solid  );
//			else if(hot)
//				ControlPaint.DrawBorder(g, rect, m_Style.BorderHotColor , ButtonBorderStyle.Solid  );
//			else 
//				ControlPaint.DrawBorder(g, rect, m_Style.BorderColor , ButtonBorderStyle.Solid  );
		
		}

		//		protected override void OnLostFocus(System.EventArgs e)
		//		{
		//			base.OnLostFocus(e);
		//			if(this.BorderStyle ==BorderStyle.FixedSingle)
		//				PaintFlatControl(this.CreateGraphics (),false);
		//		}
		//
		//		protected override void OnGotFocus(System.EventArgs e)
		//		{
		//			base.OnGotFocus(e);
		//			if(this.BorderStyle ==BorderStyle.FixedSingle)
		//				PaintFlatControl(this.CreateGraphics (),false);
		//		}
		//		
		//		protected override void OnMouseLeave(System.EventArgs e)
		//		{
		//			base.OnMouseLeave(e);
		//			if(this.BorderStyle ==BorderStyle.FixedSingle)
		//				PaintFlatControl(this.CreateGraphics (),false);
		//		}
		//
		//		protected override void OnMouseEnter(EventArgs e)
		//		{
		//			base.OnMouseEnter (e);
		//			if(this.BorderStyle ==BorderStyle.FixedSingle)
		//				PaintFlatControl(this.CreateGraphics (),true);
		//		}

//		protected override void OnReadOnlyChanged(EventArgs e)
//		{
//			base.OnReadOnlyChanged (e);
//			OnBackColorChanged(e);
//		}

//		protected override void OnBackColorChanged(EventArgs e)
//		{
//			base.OnBackColorChanged (e);
//			base.BackColor=LayoutManager.Layout.GetBackColor(this.Enabled,this.ReadOnly);
//		}

		#endregion

		#region Validation events

		protected override void OnValidating(System.ComponentModel.CancelEventArgs e)
		{
			base.OnValidating(e);
			//IsValidating();
		}

		protected override void OnValidated(EventArgs e)
		{
			base.OnValidated(e);
		}

		#endregion

		#region IControlKeyAction Members

//		[Category("Action")]
//		[System.ComponentModel.RefreshProperties(RefreshProperties.Repaint)]
//		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
//		public KeyAction KeyAction
//		{
//			get {return m_KeyAction;}
//			set {m_KeyAction= value;}
//		}

//		protected override bool ProcessDialogKey(Keys keyData)
//		{
//			bool proc=false;
//
//			if (keyData == Keys.Escape )
//			{
//				proc= OnEscapeAction(m_KeyAction.OnEscapeAction);
//			} 
//			else if (keyData == Keys.Enter)
//			{
//				proc=OnEnterAction(m_KeyAction.OnEnterAction);
//			} 
//			else if (keyData == Keys.Insert )
//			{
//				proc=OnInsertAction(m_KeyAction.OnInsertAction);
//			}
//
//			if(!proc)
//				proc= base.ProcessDialogKey(keyData);
//			return proc;
//		}
//
//		protected virtual bool OnInsertAction(InsertAction action)
//		{
//			bool proc=false; 
//			switch(action)
//			{
//				case InsertAction.None:
//					proc=false;
//					break;
//				case InsertAction.Custom :
//					m_KeyAction.SetCustomValue(this.BaseFormat);
//					break;
//				case InsertAction.DefaultValue :
//					base.ActionDefaultValue();
//					break;
//				case InsertAction.DropDown:
//					base.ActionDropDown(true);
//					break;
//				case InsertAction.Click :
//					base.ActionClick();  
//					break;
//			}
//			return proc;	
//		}
//
//		protected virtual bool OnEnterAction(EnterAction action)
//		{
//			bool proc=false; 
//			switch(action)
//			{
//				case EnterAction.None:
//					proc=false;
//					break;
//				case EnterAction.Validation:
//					base.IsValidating();
//					break;
//				case EnterAction.MoveNext:
//					base.ActionTabNext();
//					break;
//			}
//			return proc;	
//		}
//
//		protected virtual bool OnEscapeAction(EscapeAction action)
//		{
//			bool proc=false; 
//			switch(action)
//			{
//				case EscapeAction.None:
//					proc=false;
//					break;
//				case EscapeAction.Clear :
//					this.Text ="";
//					break;
//				case EscapeAction.CloseForm: 
//					this.FindForm ().Close(); 
//					break;
//				case EscapeAction.Undo: 
//					ActionUndo();
//					break;
//				case EscapeAction.DropDownClose: 
//					base.ActionDropDown(false);
//					break;
//			}
//			return proc;	
//		}

//		public override bool IsValidating()
//		{
//			bool ok=base.IsValidating ();
//			if(ok)
//			{
//				if(this.Modified )//DataBindings.Count <=0 )
//					this.AppendFormatText(base.Text );// Text =base.Text;
//			}
//			return ok;
//		}

		#endregion

        #region StyleProperty

        [Browsable(false),DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden),  Category("Style"), DefaultValue(ControlLayout.Visual)]
        public virtual ControlLayout ControlLayout
        {
            get { return ControlLayout.Visual; }
            set
            {
            }
        }

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
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected void SerializeFont(Font value, bool force)
        {
            if (ShouldSerializeForeColor())
                this.Font = LayoutManager.Layout.TextFontInternal;
            else if (force)
                this.Font = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected void SerializeForeColor(Color value, bool force)
        {
            if (ShouldSerializeForeColor())
            {
                base.ForeColor = LayoutManager.Layout.ForeColorInternal;
            }
            else if (force)
            {
                base.ForeColor = value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected void SerializeBackColor(Color value, bool force)
        {
            if (ShouldSerializeBackColor())
            {
                base.BackColor = LayoutManager.Layout.BackColorInternal;
            }
            else if (force)
            {
                base.BackColor = value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected bool ShouldSerializeBackColor()
        {
            return IsHandleCreated && StylePainter != null;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected bool ShouldSerializeForeColor()
        {
            return IsHandleCreated && StylePainter != null;
        }

        protected virtual void OnStylePropertyChanged(PropertyChangedEventArgs e)
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
        protected virtual void OnStylePainterChanged(EventArgs e)
        {
            OnStylePropertyChanged(new PropertyChangedEventArgs("ControlLayout"));
        }

        #endregion

		#region ILayout

		protected IStyle		m_StylePainter;
 
		[Browsable(false)]
		public PainterTypes PainterType
		{
			get{return PainterTypes.Edit;}
		}
		
		[Category("Style"),DefaultValue(null),RefreshProperties(RefreshProperties.All)]
		public IStyle StylePainter
		{
			get {return m_StylePainter;}
			set 
			{
				if(m_StylePainter!=value)
				{
					if (this.m_StylePainter != null)
						this.m_StylePainter.PropertyChanged -=new PropertyChangedEventHandler(m_Style_PropertyChanged); 
					m_StylePainter = value;
					if (this.m_StylePainter != null)
						this.m_StylePainter.PropertyChanged +=new PropertyChangedEventHandler(m_Style_PropertyChanged); 
					OnStylePainterChanged(EventArgs.Empty);
					this.Invalidate(true);
				}
			}
		}

		[Browsable(false)]
		public virtual IStyleLayout LayoutManager
		{
			get
			{
				if(this.m_StylePainter!=null)
					return this.m_StylePainter.Layout as IStyleLayout;
				else
					return StyleLayout.DefaultLayout as IStyleLayout;// this.m_Style as IStyleLayout;

			}
		}

		public virtual void SetStyleLayout(StyleLayout value)
		{
			if(this.m_StylePainter!=null)
				this.m_StylePainter.Layout.SetStyleLayout(value); 
		}

		public virtual void SetStyleLayout(Styles value)
		{
			if(this.m_StylePainter!=null)
				m_StylePainter.Layout.SetStyleLayout(value);
		}

		private void m_Style_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{

			OnStylePropertyChanged(e);
		}

		#endregion


	}
}
