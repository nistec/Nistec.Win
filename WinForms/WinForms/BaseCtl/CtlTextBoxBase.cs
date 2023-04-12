using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
//using System.Security.Permissions;


using Nistec.Data;
using Nistec.Win;


namespace Nistec.WinForms.Controls
{

	[ToolboxItem(false)]
    public class McTextBoxBase : System.Windows.Forms.TextBox, IMcTextBox, IBind, IMcEdit
	{	
		#region NetReflectedFram
		internal bool m_netFram=false;

		public void NetReflectedFram(string pk)
		{

            m_netFram = true;

            //try
            //{
            //    // this is done because this method can be called explicitly from code.
            //    System.Reflection.MethodBase method = (System.Reflection.MethodBase) (new System.Diagnostics.StackTrace().GetFrame(1).GetMethod());
            //    m_netFram = Nistec.Net.License.nf_1.nf_2(method, pk);
            //}
            //catch{}
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated (e);
            //if(!DesignMode && !m_netFram)
            //{
            //    Nistec.Util.Net.netWinMc.NetFram(this.Name, "Mc"); 
            //}
		}

		#endregion

		#region Members

		private string originalText;

		private string				m_Format;
		private string				m_DefaultValue;
		private Formats				m_FormatType;
		private int					m_DecPlaces;
		private bool				m_Required;
		
		internal bool useCustomFormat=false;
		
		//protected RangeType					m_RangeValue;
		//private bool m_IsValid;

		public event MessageEventHandler ProccessMessage;
		public event ErrorOcurredEventHandler ValidatingError ;
 	
		#endregion

		#region Constructor

        internal McTextBoxBase()
            : base()
		{
			base.BorderStyle=BorderStyle.FixedSingle; 
			this.SetStyle(ControlStyles.DoubleBuffer, true);

			m_FormatType = Formats.None;
			m_Format = string.Empty;
			m_DefaultValue = string.Empty;
			//m_RangeValue=null;
			m_DecPlaces=2;
			m_Required=false;
		}

		internal McTextBoxBase(bool net): this()
		{
			m_netFram=net;
		}

		#endregion

		#region Virtuals

		public virtual bool OnWndProc(ref Message m)
		{
			if(ProccessMessage != null)
			{
				MessageEventArgs evtArgs = new MessageEventArgs(m, false);
				ProccessMessage(this, evtArgs);
				
				return evtArgs.Result;
			}
            
			return false;
		}

		#endregion

		#region Internal Methods

		internal void SetSelectable(bool value)
		{
			this.SetStyle(ControlStyles.Selectable, value);
		}

		#endregion

		#region Property
 
		[Category("Apperarace"),DefaultValue(BorderStyle.FixedSingle)]
		public new BorderStyle BorderStyle
		{
			get{return base.BorderStyle;}
			set{base.BorderStyle=value;}
		}

        //[Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public override Color BackColor
        //{
        //    get { return base.BackColor; }
        //    set { base.BackColor = value; }
        //}
        //[Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public override Color ForeColor
        //{
        //    get { return base.ForeColor; }
        //    set { base.ForeColor = value; }
        //}

		[Category("Appearance"),DefaultValue(2)]   
		public int DecimalPlaces
		{
			get {return m_DecPlaces;}
			set 
			{
				m_DecPlaces = value;
				Invalidate ();  
			}
		}

		[Category("Behavior"),DefaultValue("")]//,DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string DefaultValue
		{
			get {return m_DefaultValue;}
			set
			{
				m_DefaultValue = value;
				if(DesignMode)
				{
					this.Text=m_DefaultValue;
				}
			}
		}

		[DefaultValue(""),Browsable(false)] 
		public virtual string Format
		{
			get {return m_Format;}
			set {m_Format = value;}
		}

		[Browsable(false)]
		public FieldType BaseFormat 
		{
            get { return WinHelp.GetBaseFormat(m_FormatType); }
		}

		[Category("Appearance"),DefaultValue(Formats.None)]   
		public virtual Formats FormatType
		{
			get{return m_FormatType;}
			set
			{
				if(m_FormatType!=value)
				{
					m_FormatType=value;
                    m_Format = WinHelp.GetFormat(value);
					Invalidate ();
				}
			}
		}

		[Category("Behavior"),DefaultValue(false)] 
		public virtual bool Required
		{
			get {return m_Required;}
			set {m_Required = value;}
		}

//		public bool IsValid
//		{
//			get{return m_IsValid;}
//		}

		#endregion

		#region IMcTextBox Members

		public void AppendFormatText(string value)
		{
			try
			{
				switch(m_FormatType)
				{
					case Formats.None:
						base.Text =value;
						break;
					case Formats.GeneralNumber:
						base.Text =Regx.ParseToDecimal(value,m_DefaultValue,m_Format); 
						break;
					case Formats.FixNumber:
					case Formats.StandadNumber:
					case Formats.Percent:
						base.Text =Regx.ParseToDecimal(value,m_DefaultValue,m_Format,m_DecPlaces); 
						break;
					case Formats.Money:
                        base.Text = WinHelp.ParseToCurrency(value, m_DefaultValue, m_DecPlaces); 
						break;
					case Formats.LongDate:
					case Formats.GeneralDate:
					case Formats.ShortDate:
						base.Text =Types.FormatDate(value,m_Format,m_DefaultValue);
						break;
					case Formats.LongTime:
					case Formats.ShortTime:
						base.Text =Regx.ParseToTime(value,m_DefaultValue,m_Format); 
						break;
				}
				//this.Modified = false;

			}
			catch(Exception ex)
			{
				//throw new Exception (ex.Message );
				MsgBox.ShowError(ex.Message);
			}
		}
	
		#endregion

		#region Handel key Event

		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			int strt=SelectionStart;
			
			switch(m_FormatType)
			{
				case Formats.None:
					base.OnKeyPress(e);
					return;
				case Formats.GeneralNumber:
				case Formats.FixNumber:
				case Formats.StandadNumber:
				case Formats.Money:
				case Formats.Percent:
					e.Handled = HandelKey.HandleNumeric(this,SelectedText,ref strt,SelectionLength,m_DecPlaces,e.KeyChar);
					SelectionStart=strt; 
					break;
				case Formats.LongDate:
					base.OnKeyPress(e);
					break;
				case Formats.GeneralDate:
					e.Handled = HandelKey.HandleMaskGeneralDateTime (this,SelectedText,ref strt,SelectionLength,e.KeyChar);
					SelectionStart=strt; 
					break;
				case Formats.ShortDate:
					e.Handled = HandelKey.HandleMaskDate (this,SelectedText,ref strt,SelectionLength,e.KeyChar);
					SelectionStart=strt; 
					//e.Handled = HandelKey.HandleDate(this,SelectedText,e.KeyChar);
					break;
				case Formats.LongTime:
					e.Handled = HandelKey.HandleMaskLongTime (this,SelectedText,ref strt,SelectionLength,e.KeyChar);
					SelectionStart=strt; 
					break;
				case Formats.ShortTime:
					e.Handled = HandelKey.HandleMaskShortTime (this,SelectedText,ref strt,SelectionLength,e.KeyChar);
					SelectionStart=strt; 
					break;
				default:
					e.Handled = false;
					break;
			}
            if (!e.Handled)
            {
                base.OnKeyPress(e);
            }
		}

		#endregion

		#region Validation events

//		protected override void OnValidating(System.ComponentModel.CancelEventArgs e)
//		{
//			base.OnValidating(e);
//			//bool ok=IsValidating();
//			//m_IsValid= ok && !e.Cancel;
//		}

		protected override void OnValidated(EventArgs e)
		{
			base.OnValidated(e);
			if(this.Modified)
			{
               if(m_Format!="")// m_FormatType != Formats.None)
				  this.AppendFormatText(base.Text);
				originalText=base.Text;
				this.Modified = false;
			}
		}

		internal virtual void OnValidatingError(string msg)
		{
			if (msg.Length == 0)return;
			OnValidatingError(new ErrorOcurredEventArgs(msg));
		}

		protected virtual void OnValidatingError(ErrorOcurredEventArgs e)
		{
			if (this.ValidatingError != null)
				this.ValidatingError(this,e);
//			if (e.Message.Length > 0)
//			{
//				Nistec.WinForms.ErrProvider.ShowError(this,ErrProviders.MsgBox,e.Message);
//			}
		}

		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged (e);
			if(!IsHandleCreated)
				this.Modified = false;
			else if (originalText != base.Text)
				this.Modified = true;
			else
				this.Modified = false;

		}


		#endregion

		#region Actions


        protected override bool ProcessDialogKey(Keys keyData)
        {

            if (keyData == Keys.Escape)
            {
                OnEscapeAction();
            }
            else if (keyData == Keys.Enter)
            {
                OnEnterAction();
            }
            else if (keyData == Keys.Insert)
            {
                OnInsertAction();
            }

            return base.ProcessDialogKey(keyData);
        }

        protected virtual void OnInsertAction()
        {
        }

        protected virtual void OnEnterAction()
        {
        }

        protected virtual void OnEscapeAction()
        {
        }

        public int GetIntValue()
        {
            return Types.ToInt(this.Text, 0);
        }
        public double GetDoubleValue()
        {
            return Types.ToDouble(this.Text, 0);
        }
        public decimal GetDecimalValue()
        {
            return Types.ToDecimal(this.Text, 0);
        }


		public virtual void ActionTabNext()
		{
			ProcessDialogKey(Keys.Tab);
		}

		public virtual  void ActionClick()
		{
			this.InvokeOnClick(this,new System.EventArgs ());  
		}

		public virtual void ActionUndo()
		{
			if(this.CanUndo)
				this.Undo(); 
		}
		
		public virtual void ActionDefaultValue()
		{
			base.Text =m_DefaultValue;  
		}

		protected virtual  bool IsValidating()
		{
			string msg="";
	        bool ok=IsValidating(ref msg);
			if(!ok)
			{
				OnValidatingError(msg);
			}
		    return ok;
		}
		
		public virtual  bool IsValidating(ref string msg)
		{

			bool ok=true;
			ok =  Validator.ValidateText(Text,m_Required, ref msg);
			if(!ok) return false;
			
			switch(m_FormatType )
			{
				case Formats.None:
					ok =true;
					break;
				case Formats.GeneralNumber:
				case Formats.FixNumber:
				case Formats.StandadNumber:
				case Formats.Percent:
					ok =  Validator.ValidateNumber(Text,false, ref msg);//!IsValidNumber();
					break;
				case Formats.Money:
					ok = Validator.ValidateCurrency(Text,RangeNumber.Empty, ref msg);
					break;
				case Formats.GeneralDate:
				case Formats.LongDate:
				case Formats.ShortDate:
					ok = Validator.ValidateDate(Text,ref msg);
					break;
				case Formats.LongTime:
				case Formats.ShortTime:
					ok = Validator.ValidateTime(Text,ref msg);
					break;
				default:
					ok = true;
					break;
			}	
			return ok;
		}

		#endregion

		#region IBind Members

		[Browsable(false)]
		public BindingFormat BindFormat
		{
			get{return BindingFormat.String;}
		}

		public string BindPropertyName()
		{
			return "Text";
		}
        public virtual void BindDefaultValue()
        {
            //if (this.DefaultValue.Length > 0)
            //{
                this.Text = this.DefaultValue;
                if (base.IsHandleCreated)
                {
                    this.OnTextChanged(EventArgs.Empty);
                }
            //}
        }

		#endregion
	}
}