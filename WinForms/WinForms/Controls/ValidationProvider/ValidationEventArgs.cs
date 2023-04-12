using System;

namespace Nistec.WinForms
{
	/// <summary>
	/// Delegate for custom validation methods.
	/// </summary>
	public delegate	void ValidationEventHandler(object sender, ValidationEventArgs e);

	/// <summary>
	/// Arguments of validation event.
	/// </summary>
	public class ValidationEventArgs : EventArgs
	{
		private object			_Value = null;
		private ValidatorRule	_ValidationRule = null;

		/// <summary>
		/// Default Ctor.
		/// </summary>
		/// <param name="Value"></param>
		/// <param name="vr"></param>
		public ValidationEventArgs(object Value, ValidatorRule vr)
		{
			this._Value = Value;
			this._ValidationRule = vr;
		}

		/// <summary>
		/// Value to validate.
		/// </summary>
		public object Value
		{
			get { return _Value; }
		}

		/// <summary>
		/// Get or set validity of attached validation rule.
		/// </summary>
		public bool IsValid
		{
			get { return this._ValidationRule.IsValid; }
			set { this._ValidationRule.IsValid = value; }
		}

		/// <summary>
		/// Get or set error message to display when validation fail.
		/// </summary>
		public string ErrorMessage
		{
			get { return this._ValidationRule.ErrorMessage; }
			set { this._ValidationRule.ErrorMessage = value; }
		}

		/// <summary>
		/// Allow custom validation class to set IsValid and ErrorMessage
		/// value.
		/// </summary>
		public ValidatorRule ValidatorRule
		{
			get { return this._ValidationRule;}
		}
	}
}
