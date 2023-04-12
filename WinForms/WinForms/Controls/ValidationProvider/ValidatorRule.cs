
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.ComponentModel;
using Nistec.Win;
//using System.Web.UI.WebControls;


namespace Nistec.WinForms
{
	/// <summary>
	/// ValidatorRule is designed to be a simple as possible to
	/// reduce overhead in run-time.  It's because each validation
	/// rule can be attach to a control, so we can have a many
	/// instances of this class.
	/// </summary>
	[TypeConverter(typeof(Nistec.WinForms.ValidatorRuleConverter))]
	public class ValidatorRule : ICloneable
	{
		#region "Settings"
		internal string				ResultErrorMessage	= string.Empty;

		private string				_ErrorMessage		= "{McName} is invalid.";
		private string				_InitialValue		= string.Empty;
		private BaseDataType        _DataType			= BaseDataType.String;
		private bool				_IsValid			= true;
		private bool				_IsRequired			= false;
		private bool				_IsCaseSensitive	= true;
		private string				_fieldName			= string.Empty;
		//private RangeType				_Range			    ;//= null;

		//public event EventHandler DataTypeChanged;
		//ValidationDataType
		/// <summary>
		/// Set validation case sensitivity.
		/// </summary>
		[DefaultValue(true), Category("Settings")]
		[Description("Case sensitivity validation works best with String DataType.")]
		public bool CaseSensitive
		{
			get { return _IsCaseSensitive; }
			set { _IsCaseSensitive = value; }
		}

		/// <summary>
		/// Data Type of the validation.
		/// </summary>
		[DefaultValue(BaseDataType.String), Category("Settings")]
		[Description("DataType of control value.")]
		public BaseDataType DataType
		{
			get { return _DataType; }
			set 
			{
				if(_DataType != value)
				{
					_DataType = value;
					//OnDataTypeChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>
		/// ErrorMessage result of validation.
		/// </summary>
		[DefaultValue("{McName} is invalid."), Category("Settings")]
		[Description("Message to display/return iwhen validation failed.")]
		public string ErrorMessage
		{
			get { return _ErrorMessage; }
			set { _ErrorMessage = (value == null) ? string.Empty : value; }
		}

		/// <summary>
		/// Get validity of control value after Validate method is called.
		/// </summary>
		[DefaultValue(true), Browsable(false)]
		public bool IsValid
		{
			get { return _IsValid; }
			set { _IsValid = value; }
		}

//		/// <summary>
//		/// The default is "".
//		/// using "[Choose value]" in a DropDownList control. In this case, 
//		/// the required value must be different than the initial value of 
//		/// "[Choose a value]". InitialValue supports this requirement. 
//		/// </summary>
		[DefaultValue(""), Category("Settings")]
		[Description("Initial value of control.")]
		internal string InitialValue
		{
			get { return _InitialValue; }
			set { _InitialValue = (value == null) ? string.Empty : value; }
		}

		/// <summary>
		/// Cause validation to check if field is required.
		/// </summary>
		[DefaultValue(false), Category("Settings")]
		[Description("Require a value.  Validate to false if control value matches InitialValue.")]
		public bool Required
		{
			get { return _IsRequired; }
			set { _IsRequired = value; }
		}

		/// <summary>
		/// Cause validation to check if field is required.
		/// </summary>
		[DefaultValue(""),Category("Settings")]
		[Description("Visible name of control")]
		public string FieldName
		{
			get { return _fieldName; }
			set { _fieldName = value; }
		}

		#endregion

		#region "Compare Validation Settings"
		private ValidationOperator				_Operator			= ValidationOperator.DataTypeCheck;
		private string							_ValueToCompare		= string.Empty;
		
		/// <summary>
		/// Get or set operator to use to compare.
		/// </summary>
		[DefaultValue(ValidationOperator.DataTypeCheck), Category("Compare")]
		[Description("Type of comparison to perform with ValueToCompare.  Default is data type checking if DataType is not String.")]
		public ValidationOperator Operator
		{
			get { return _Operator; }
			set { _Operator = value; }
		}

		/// <summary>
		/// Get or set value use to compare with the control value.
		/// </summary>
		[DefaultValue(""), Category("Compare")]
		[Description("This is use in combination with Operator.")]
		public string ValueToCompare
		{
			get { return _ValueToCompare; }
			set { _ValueToCompare = (value == null) ? string.Empty : value; }
		}
		#endregion

		#region "RangeType Validation Settings"
		private string				_MinimumValue = string.Empty;
		private string				_MaximumValue = string.Empty;

		/// <summary>
		/// RangeValidator Minimum Value.
		/// </summary>
		[DefaultValue(""), Category("RangeType Value")]
		[Description("Minimum value the control can have not include DataType of String.")]
		public string MinimumValue 
		{
			get { return _MinimumValue; }
			set 
			{
				_MinimumValue = (value == null) ? string.Empty : value; 
			}
		}

		/// <summary>
		/// RangeValidator MaximumValue Value.
		/// </summary>
		[DefaultValue(""), Category("RangeType Value")]
		[Description("Maximum value the control can have not include DataType of String.")]
		public string MaximumValue
		{
			get { return _MaximumValue; }
			set 
			{
				_MaximumValue = (value == null) ? string.Empty : value; 
			}
		}

//		[DefaultValue(null), Category("RangeType Value")]
//		[Description("Maximum value the control can have not include DataType of String.")]
//		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
//		private RangeType RangeValue
//		{
//			get { return _Range; }
//			set{_Range=value;}
//		}
//
//		private void AddRangeValue()
//		{
//			if(_Range!=null)
//				_Range=null;
//
//			switch(_DataType)
//			{
//				case BaseDataType.String:
//					_Range =null;
//					break;
//				case BaseDataType.Double:
//				case BaseDataType.Currency:
//					_Range =new  RangeType(RangeType.MinNumber,RangeType.MaxNumber);						
//					break;
//				case BaseDataType.Integer:
//					_Range =new  RangeType(int.MinValue,int.MaxValue);						
//					break;
//				case BaseDataType.Date:
//					_Range =new  RangeType(RangeType.MinDate,RangeType.MaxDate );						
//					break;
//				default:
//					_Range =null;
//					break;
//			}
//			if(_Range!=null)
//			{
//				_MinimumValue=_Range.MinValue;
//				_MaximumValue=_Range.MaxValue;
//			}
//
//		}
//
//		internal void RemoveRangeValue()
//		{
//			_Range =null;						
//		}

//		protected virtual void OnDataTypeChanged(EventArgs e)
//		{
//			if(this.DataTypeChanged!=null)
//			{
//               this.DataTypeChanged(this,e);
//			}
//		}
	
		#endregion

		#region "Regular Expression Validation Settings"
		private string		_RegExPattern	= string.Empty;

		/// <summary>
		/// Regular Expression Pattern to use for validation.
		/// </summary>
		[DefaultValue(""), Category("Regular Expression")]
		[Description("Regular Expression Pattern to use for validator.")]
		public string RegExPattern
		{
			get { return _RegExPattern; }
			set { _RegExPattern = (value == null) ? string.Empty : value; }
		}
		#endregion

		
		/// <summary>
		/// Allow for attachment of custom validation method.
		/// </summary>
		public event ValidationEventHandler CustomValidationMethod;

		/// <summary>
		/// Delegate invoking of validation method.
		/// </summary>
		/// <param name="e"></param>
		internal protected virtual void OnCustomValidationMethod(ValidationEventArgs e)
		{
			if (this.CustomValidationMethod != null)
				this.CustomValidationMethod(this, e);
		}

		/// <summary>
		/// Compare two values.
		/// </summary>
		/// <param name="leftText"></param>
		/// <param name="rightText"></param>
		/// <param name="op"></param>
		/// <param name="vr"></param>
		/// <returns></returns>
		public static bool Compare(	string leftText, 
									string rightText, 
									ValidationOperator op, 
									ValidatorRule vr)//ValidationCompareOperator
		{
			if (false == vr.CaseSensitive && vr.DataType == BaseDataType.String)
			{
				leftText = leftText.ToLower();
				rightText = rightText.ToLower();
			}
			bool res= Validator.CompareValues(leftText, rightText, op, vr.DataType);
			return res;//Validator.CompareValues(leftText, rightText, op, vr.DataType);
		}

		#region ICloneable Members

		/// <summary>
		/// ValidatorRule is memberwised cloneable.
		/// </summary>
		/// <returns></returns>
		public object Clone()
		{
			return this.MemberwiseClone();
		}

		#endregion
	}
}
