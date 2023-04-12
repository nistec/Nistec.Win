using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Collections;
  

using Nistec.Win;
  
namespace Nistec.WinForms
{

	[ToolboxItem(false)]
	public class NumberBox: Nistec.WinForms.Controls.McTextBoxBase
	{	

		#region Base Members

		private NumberFormats m_FormatType;
		private decimal m_DefaultValue;
		//private RangeNumber m_RangeValue;
		private decimal m_Value;
        private decimal m_MinValue;
        private decimal m_MaxValue;

		internal bool ChangingText;
		internal bool UserEdit;

		public event EventHandler ValueChanged;
        //public new event EventHandler TextChanged;
	
		#endregion

		#region Constructor

		internal NumberBox(bool net):this()
		{
			base.m_netFram=net;
		}

		public NumberBox(): base()
		{
            m_DefaultValue = 0;
            m_Value = 0;
            m_MinValue = 0;
            m_MaxValue = 100;
		
			FormatType= NumberFormats.StandadNumber;
			//m_RangeValue= new RangeNumber(RangeType.MinNumber,RangeType.MaxNumber);
            base.DecimalPlaces = 0;
            this.AppendNumberText("0");
		}

		#endregion

		#region override

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            int strt = SelectionStart;
            e.Handled = HandelKey.HandleNumeric(this, SelectedText, ref strt, SelectionLength, DecimalPlaces, e.KeyChar);
            SelectionStart = strt;
            if (!e.Handled)
            {
                base.OnKeyPress(e);
            }
        }

		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus (e);
			if (this.UserEdit)
			{
				this.ValidateEditText();
			}

		}

		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged (e);
			if (this.ChangingText)
			{
				this.ChangingText = false;
 			}
			else
			{
				this.UserEdit = true;
			}
		}

		protected virtual void OnValueChanged(EventArgs e)
		{
            this.UserEdit = false;
			if(ValueChanged!=null)
				ValueChanged(this,EventArgs.Empty);
		}

		internal protected virtual void ValidateEditText()
		{
			this.ParseEditText();
			this.UpdateEditText();
		}

		protected void ParseEditText()
		{
			try
			{
				//if(this.RangeValue.IsValid(this.Text))
				//{
					this.Value =decimal.Parse(this.Text);
				//}
			}
			catch (Exception)
			{
				return;
			}
			finally
			{
				this.UserEdit = false;
			}
		}
		protected virtual void UpdateEditText()
		{
			if (this.UserEdit)
			{
				this.ParseEditText();
			}
			this.ChangingText = true;
			//base.Text = this.Value.ToString(this.Format);
			AppendNumberText(this.Value);
		}

		#endregion

        #region range

        public bool IsValidNumber(double value)
        {
            return ((decimal)value <= m_MaxValue && (decimal)value >= m_MinValue);
        }

        public bool IsValidNumber(decimal value)
        {
            return ((decimal)value <= m_MaxValue && (decimal)value >= m_MinValue);
        }

        public bool IsValidNumber(int value)
        {
            return ((decimal)value <= m_MaxValue && (decimal)value >= m_MinValue);
        }

        public bool IsValidNumber(object obj)
        {
            decimal value = System.Convert.ToDecimal(obj);
            return (value <= m_MaxValue && value >= m_MinValue);
        }

        public bool IsValidNumber(string s)
        {
            decimal value = decimal.Parse(s);
            return (value <= m_MaxValue && value >= m_MinValue);
        }
        #endregion

        #region public Property

        //[Category("Behavior"),Description ("RangeType of min and max value")]
        //public RangeNumber RangeValue
        //{
        //    get	{return m_RangeValue;}
        //    set	
        //    {
        //        //if(!m_RangeValue.IsEmpty)//TODO: !=null)
        //        m_RangeValue = value;
        //    }
        //}

        [Category("Behavior"), Description("min value")]
        public decimal MinValue
        {
            get { return m_MinValue; }
            set
            {
                m_MinValue = value;
            }
        }
        [Category("Behavior"), Description("max value")]
        public decimal MaxValue
        {
            get { return m_MaxValue; }
            set
            {
                m_MaxValue = value;
            }
        }
		[Category("Behavior"),DefaultValue(0),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new decimal DefaultValue
		{
			get {return m_DefaultValue;}
			set
			{
				m_DefaultValue = value;
				if(DesignMode)
				{
					this.Value=m_DefaultValue;
				}
			}
		}

		[Category("Behavior"),Bindable(true),Browsable(true)]
		public decimal Value
		{
			get{return m_Value;}
			set
			{
				//m_Value=value;
				//base.Text=value.ToString ();

				if (value != m_Value)
				{
					//if (IsValidNumber(value))
					//{
						//-m_Value = value;
						//-this.OnValueChanged(EventArgs.Empty);
						//this.UpdateEditText();
						//base.Text=value.ToString (this.Format);
						AppendNumberText(value);
                        this.OnValueChanged(EventArgs.Empty);

                   
					//}
				}
			}
		}

		[Category("Behavior"),Bindable(true),Browsable(true)]
		public override string Text
		{
			get{return base.Text;}
			set
			{
				if(Text!=value)
				{
                    //string s=value;
                    //decimal val=0;

                    //if(s.Length ==0) 
                    //{
                    //    val =m_DefaultValue;
                    //    //s =m_DefaultValue.ToString (m_Format);
                    //}
                    //else
                    //{
                    //    if(m_FormatType== NumberFormats.Money)
                    //    {
                    //        val=decimal.Parse (Regx.ParseCurrencyToNumber(value,m_DefaultValue));
                    //    }
				
                    //    else
                    //    {
                    //        val=Regx.ParseDecimal(value,m_DefaultValue);
                    //    }
                    //}
                    ////decimal val=decimal.Parse (s);
                    //if (m_RangeValue.IsValid (val))
                    //{
						AppendNumberText(value);
                        this.OnValueChanged(EventArgs.Empty);
						//base.Text =s;
						//-ParseEditText();
						//m_Value=decimal.Parse (Text);
					//}
				}
			}
		}

		[Category("Appearance"),DefaultValue(NumberFormats.GeneralNumber)]   
		public new NumberFormats FormatType
		{
			get{return m_FormatType;}
			set
			{
				if(m_FormatType!=value)
				{
					m_FormatType=value;
                    Format = WinHelp.GetFormat(value, Format);
					Invalidate ();
				}
			}
		}

		internal bool AppendNumberText(decimal value)
		{
            if (!IsValidNumber(value))//(base.TextLength > 0 && this.Value == value) || 
            {
                return false;
            }
			switch(m_FormatType)
			{
				case NumberFormats.GeneralNumber:
					base.Text = value.ToString(Format);
					break;
				case NumberFormats.Money:
                    base.Text = WinHelp.ParseToCurrency(value.ToString(), m_DefaultValue, DecimalPlaces); 
					break;
				default:
					base.Text = value.ToString(Format + DecimalPlaces.ToString());
					break;
			}
            this.m_Value = value;
            //this.OnValueChanged(EventArgs.Empty);
            return true;
		}


		public void AppendNumberText(string value)
		{

			if(value.Length ==0) 
			{
				AppendNumberText(DefaultValue);
                return;
			}
            string strval = value;
			try
			{
				switch(m_FormatType)
				{
					case NumberFormats.Money:
                        strval = WinHelp.ParseCurrencyToNumber(value, DefaultValue);
						//base.Text =Regx.ParseToCurrency(value,DefaultValue,DecimalPlaces); 
						break;
					default:
                        strval = value;
						//base.Text =Regx.ParseToDecimal(value,m_DefaultValue,Format,DecimalPlaces); 
						break;
				}
                decimal val = Types.ToDecimal(strval, DefaultValue);
                AppendNumberText(val);
 
			}
			catch(Exception ex)
			{
				throw new ApplicationException (ex.Message );
			}
		}

        //public bool IsValidNumber(string s)
        //{
        //   return m_RangeValue.IsValid (s); 
        //}

        //public bool IsValidNumber(decimal value)
        //{
        //    return m_RangeValue.IsValid (value); 
        //}

        //public bool IsValidNumber(double value)
        //{
        //    return m_RangeValue.IsValid (value); 
        //}

		public void IncrementValue(decimal increment)
		{
			decimal val = Value;
			if (increment ==0)
			{
				return;
			}
			else if (increment >0)
			{
				val+=increment;
				if(val <=   (decimal)MaxValue)
				{
					this.Value=val;
				}
			}
			else
			{
				val-=increment;
				if(val >= (decimal)MinValue)
				{
					this.Value=val;
				}
			}
		}

		public override  bool IsValidating(ref string msg)
		{

			switch(m_FormatType )
			{
				case NumberFormats.Money:
					return Validator.ValidateCurrency(Text,RangeNumber.Empty, ref msg);
				default:
					return  Validator.ValidateNumber(Text,false, ref msg);
			}	
		}


		#endregion

		#region IControlKeyAction Members
		
		public override  void ActionDefaultValue()
		{
			base.Text =m_DefaultValue.ToString (Format);  
		}
        protected override void OnEnterAction()
        {
            base.OnEnterAction();
            if (UserEdit)
                OnValueChanged(EventArgs.Empty);

        }
		#endregion

	}
}
