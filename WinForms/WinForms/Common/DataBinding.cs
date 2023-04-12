using System;
using System.Windows.Forms;
using System.Globalization;
using System.ComponentModel;
using Nistec.Win;


namespace Nistec.WinForms
{

	public enum BindingFormat
	{
      String,
      Int,
      Decimal,
	  Currency,
      Date,
      DateTime,
      Boolean,
      YesNo
	}

	#region BindingCtl

	[TypeConverter(typeof(ListBindingConverter))]
	public class BindingCtl:Binding
	{
		//public event BindConvertEventHandler BindFormat;
		private object defaultValue;
 
		public BindingCtl(string propertyName, object dataSource, string dataMember):base(propertyName, dataSource, dataMember)
		{}

		public BindingCtl(string propertyName, object dataSource, string dataMember,object ValueIfNull):base(propertyName, dataSource, dataMember)
		{
			defaultValue=ValueIfNull;
		}

		public object DefaultValue
		{
			get{return defaultValue;}
		}

		protected override void OnFormat(ConvertEventArgs cevent)
		{
			base.OnFormat(cevent);
			if ((!(cevent.Value is DBNull) && (cevent.DesiredType != null)) && (!cevent.DesiredType.IsInstanceOfType(cevent.Value) && (cevent.Value is IConvertible)))
			{
				if(defaultValue!=null)
					cevent.Value =defaultValue;// Convert.ChangeType(cevent.Value, cevent.DesiredType);
			}
		}

//		protected void OnBindFormat(BindConvertEventArgs cevent)
//		{
//			base.OnFormat(cevent);
//			if(BindFormat!=null)
//				BindFormat(this,new BindConvertEventArgs(cevent.Value,cevent.cevent,this.defaultValue));
//			if (this.Format != null)
//			{
//				this.Format(this, cevent);
//			}
//			if ((!(cevent.Value is DBNull) && (cevent.DesiredType != null)) && (!cevent.DesiredType.IsInstanceOfType(cevent.Value) && (cevent.Value is IConvertible)))
//			{
//				cevent.Value =defaultValue;// Convert.ChangeType(cevent.Value, cevent.DesiredType);
//			}
////			if ((!(cevent.Value is DBNull) && (cevent.DesiredType != null)) && (!cevent.DesiredType.IsInstanceOfType(cevent.Value) && (cevent.Value is IConvertible)))
////			{
////				cevent.Value = Convert.ChangeType(cevent.Value, cevent.DesiredType);
////			}
//		}

 	}

	#endregion

	/// <summary>
	/// Summary description for Convert.
	/// </summary>
	public class BindControl
	{
		public BindControl()
		{
		}

		#region Bind Formats

		public BindingCtl BindToObject(string propertyName, object dataSource,string dataMember,object valueIfNull)
		{
			BindingCtl binding =new BindingCtl(propertyName, dataSource, dataMember,valueIfNull);
			binding.Format+=new ConvertEventHandler(NullToObject);
			return binding;
		}

		public Binding BindToString(string propertyName, object dataSource,string dataMember)
		{
			Binding binding =new Binding(propertyName, dataSource, dataMember);
			binding.Format+=new ConvertEventHandler(NullToString);
			return binding;
		}

		public Binding BindToInt(string propertyName, object dataSource,string dataMember)
		{
			Binding binding =new Binding(propertyName, dataSource, dataMember);
			binding.Format+=new ConvertEventHandler(IntToString);
			binding.Parse += new ConvertEventHandler(StringToInt);

			return binding;
		}

		public Binding BindToDecimal(string propertyName, object dataSource,string dataMember)
		{
			Binding binding =new Binding(propertyName, dataSource, dataMember);
			binding.Format+=new ConvertEventHandler(DecimalToString);
			binding.Parse += new ConvertEventHandler(StringToDecimal);

			return binding;
		}

		public Binding BindToBoolean(string propertyName, object dataSource,string dataMember)
		{
			Binding binding =new Binding(propertyName, dataSource, dataMember);
			binding.Format+=new ConvertEventHandler(BooleanToString);
			binding.Parse += new ConvertEventHandler(BooleanStringToBoolean);
			return binding;
		}

		public Binding BindToCurrency(string propertyName, object dataSource,string dataMember)
		{
			Binding binding =new Binding(propertyName, dataSource, dataMember);
			binding.Format+=new ConvertEventHandler(DecimalToCurrencyString);
			binding.Parse += new ConvertEventHandler(CurrencyStringToDecimal);

			return binding;
		}

		public Binding BindToYesNo(string propertyName, object dataSource,string dataMember)
		{
			Binding binding =new Binding(propertyName, dataSource, dataMember);
			binding.Format+=new ConvertEventHandler(BooleanToYesNo);
			binding.Parse += new ConvertEventHandler(YesNoToBoolean);
			return binding;
		}

		public Binding BindToDate(string propertyName, object dataSource,string dataMember)
		{
			Binding binding =new Binding(propertyName, dataSource, dataMember);
			binding.Format+=new ConvertEventHandler(DateTimeToDateString);
			binding.Parse += new ConvertEventHandler(DateStringToDateTime);
			return binding;
		}

		public Binding BindToDateTime(string propertyName, object dataSource,string dataMember)
		{
			Binding binding =new Binding(propertyName, dataSource, dataMember);
			binding.Format+=new ConvertEventHandler(DateTimeToDateTimeString);
			binding.Parse += new ConvertEventHandler(DateTimeStringToDateTime);
			return binding;
		}

		#endregion

		#region Convert

		private bool ConvertDBNull(ConvertEventArgs e,object defaultValue)
		{
			if(e.DesiredType != typeof(string)) 
				return false;
			else if(Convert.DBNull.Equals(e.Value))
			{
				e.Value = defaultValue;
				return false;
			}
			return true;
		}
		
		public void StringToNull(object sender, ConvertEventArgs e)
		{
			if( e.Value.ToString()== "[N/A]" || e.Value.ToString().Trim().Length == 0 )
			{
				try
				{
					e.Value = DBNull.Value;
				}
				catch(Exception exp)
				{
					MsgBox.ShowError(exp.Message); // UserMsg("Data entry{ error: " & exp.Message)
				}
			}
		}



		public void NullToObject(object sender, ConvertEventArgs e)
		{
			if( (Convert.DBNull.Equals( e.Value)) || e.Value.ToString().Trim().Length == 0 )
			{
				e.Value =((BindingCtl)sender).DefaultValue;
				//e.Value =e.ValueIfNull;//((mcBinding)sender).DefaultValue;
			}
		}

		public void NullToString(object sender, ConvertEventArgs e)
		{
			if( (Convert.DBNull.Equals( e.Value)) || e.Value.ToString().Trim().Length == 0 )
			{
				e.Value = "";//[N/A]";
			}
		}

		private void IntToString(object sender, ConvertEventArgs e)
		{
			try
			{
				if(Convert.DBNull.Equals(e.Value))
					e.Value = "0";
				else if(e.DesiredType.Equals(typeof(System.Int32)) || e.DesiredType.Equals(typeof(System.Int32)) || e.DesiredType.Equals(typeof(int)))
					e.Value = int.Parse(e.Value.ToString());
			}
			catch (FormatException)
			{
				throw ExceptionHelper.InvalidFormatException(e.Value.ToString());
			}
		}

		private void StringToInt(object sender, ConvertEventArgs e)
		{
			if(e.DesiredType != typeof(int)) return;

			// Converts the string back to decimal using the static Parse method.
			e.Value = int.Parse(e.Value.ToString(),	NumberStyles.Integer, null);
		}

		private void DecimalToString(object sender, ConvertEventArgs e)
		{
			try
			{
				if(Convert.DBNull.Equals(e.Value))
					e.Value = "0.00";
				else if(e.DesiredType.Equals(typeof(decimal)) || e.DesiredType.Equals(typeof(double)) || e.DesiredType.Equals(typeof(float)))
					e.Value = decimal.Parse(e.Value.ToString()).ToString("N");
			}
			catch (FormatException)
			{
				throw ExceptionHelper.InvalidFormatException(e.Value.ToString());
			}
		}

		private void StringToDecimal(object sender, ConvertEventArgs e)
		{
			if(e.DesiredType != typeof(decimal) && e.DesiredType != typeof(double) && e.DesiredType != typeof(float)) return;

			// Converts the string back to decimal using the static Parse method.
			e.Value = decimal.Parse(e.Value.ToString(),	NumberStyles.Number, null);
		}

		private void DecimalToCurrencyString(object sender, ConvertEventArgs e)
		{
			try
			{
				if(Convert.DBNull.Equals(e.Value))
					e.Value = "0.00";
				else if(e.DesiredType.Equals(typeof(System.String)) || e.DesiredType.Equals(typeof(decimal)) || e.DesiredType.Equals(typeof(double)) || e.DesiredType.Equals(typeof(float)))
					e.Value = decimal.Parse(e.Value.ToString()).ToString("c");
			}
			catch (FormatException)
			{
				throw ExceptionHelper.InvalidFormatException(e.Value.ToString());
			}
		}

		private void CurrencyStringToDecimal(object sender, ConvertEventArgs e)
		{
			if(e.DesiredType != typeof(System.String) && e.DesiredType != typeof(decimal) && e.DesiredType != typeof(double) && e.DesiredType != typeof(float)) return;

			// Converts the string back to decimal using the static Parse method.
			e.Value = decimal.Parse(e.Value.ToString(),	NumberStyles.Currency, null);
		}
        //Date
		private void DateTimeToDateString(object sender, ConvertEventArgs e)
		{
			try
			{
				if(Convert.DBNull.Equals(e.Value))
					e.Value =DateTime.Today.ToString("d");//"dd,MM,yyyy");// "[N/A]";
				else if(e.DesiredType.Equals(typeof(System.String)) || e.DesiredType.Equals(typeof(DateTime))) 
					e.Value =DateTime.Parse(e.Value.ToString()).ToString("d");// ((DateTime) e.Value).ToString("d");
			}
			catch (FormatException)
			{
				throw ExceptionHelper.InvalidFormatException(e.Value.ToString());
			}
		}
        //Date
		private void DateStringToDateTime(object sender, ConvertEventArgs e)
		{
			if(e.DesiredType != typeof(DateTime)) return;

			// Converts the string back to decimal using the static Parse method.
			e.Value = DateTime.Parse(e.Value.ToString()).ToString("d");//"dd,MM,yyyy");
		}
        //DateTime
		private void DateTimeToDateTimeString(object sender, ConvertEventArgs e)
		{
			try
			{
				if(Convert.DBNull.Equals(e.Value))
                    e.Value = DateTime.Now.ToString();//"d"  "[N/A]";
				else if(e.DesiredType.Equals(typeof(System.String)) || e.DesiredType.Equals(typeof(DateTime)))
                    e.Value = DateTime.Parse(e.Value.ToString()).ToString(); //"d" ((DateTime) e.Value).ToString();
			}
			catch (FormatException)
			{
				throw ExceptionHelper.InvalidFormatException(e.Value.ToString());
			}
		}
        //DateTime
		private void DateTimeStringToDateTime(object sender, ConvertEventArgs e)
		{
			if(e.DesiredType != typeof(DateTime)) return;

			// Converts the string back to decimal using the static Parse method.
			e.Value = DateTime.Parse(e.Value.ToString()).ToString();
		}

		private void BooleanToString(object sender, ConvertEventArgs e)
		{
			try
			{
				if(Convert.DBNull.Equals(e.Value))
					e.Value = false;//"[N/A]";
				else if(e.DesiredType.Equals(typeof(bool))) 
					e.Value =bool.Parse(e.Value.ToString());// ((bool) e.Value).ToString();
			}
			catch (FormatException)
			{
				//Specified cast is not valid
				throw ExceptionHelper.InvalidFormatException(e.Value.ToString());
			}
		}

		private void BooleanStringToBoolean(object sender, ConvertEventArgs e)
		{
			if(e.DesiredType != typeof(bool)) return;

			// Converts the string back to decimal using the static Parse method.
			e.Value = bool.Parse(e.Value.ToString());
		}

		public void BooleanToYesNo(object sender, ConvertEventArgs e)
		{
			try
			{
				if(Convert.DBNull.Equals(e.Value))
					e.Value = "No";
				else if(e.DesiredType.Equals(typeof(bool))) 
					e.Value = ((bool) e.Value) ? "Yes": "No";
			}
			catch (FormatException)
			{
				throw ExceptionHelper.InvalidFormatException(e.Value.ToString());
			}
		}
		
		private void YesNoToBoolean(object sender, ConvertEventArgs e)
		{
			if(e.DesiredType != typeof(bool)) return;

			// Converts the string back to decimal using the static Parse method.
			e.Value = bool.Parse(e.Value.ToString()).ToString();
		}

	
		// Handles the Format event for the CheckBox control.
		public void SmallIntToBoolean(object sender, ConvertEventArgs e)
		{
			switch( e.Value.ToString())
			{
				case "1":
					e.Value = true;
					break;
				default:
					e.Value = false;
					break;
			}
		}

		
		// Handles the Parse event for the CheckBox control
		public void BooleanToSmallInt(object sender, ConvertEventArgs e)
		{
			switch( e.Value.ToString())
			{
				case "true":
					e.Value = 1;
					break;
				default:
					e.Value = 0;
					break;
			}
		}
		#endregion

		#region Binding

		public Binding BindFormat(BindingFormat format, string propertyName, object dataSource,string dataMember)
		{
			switch(format)
			{
				case BindingFormat.String:
					return BindToString(propertyName,dataSource,dataMember);
				case BindingFormat.Int:
					return BindToInt(propertyName,dataSource,dataMember);
				case BindingFormat.Decimal:
					return BindToDecimal(propertyName,dataSource,dataMember);
				case BindingFormat.Currency:
					return BindToCurrency(propertyName,dataSource,dataMember);
				case BindingFormat.Date:
					return BindToDate(propertyName,dataSource,dataMember);
				case BindingFormat.DateTime:
					return BindToDateTime(propertyName,dataSource,dataMember);
				case BindingFormat.Boolean:
					return BindToBoolean(propertyName,dataSource,dataMember);
				case BindingFormat.YesNo:
					return BindToYesNo(propertyName,dataSource,dataMember);
				default:
					return BindToString(propertyName,dataSource,dataMember);
			}
		}

		#endregion

	}
}
