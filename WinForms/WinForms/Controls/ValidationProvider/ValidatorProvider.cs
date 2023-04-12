
using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using System.Reflection;
using System.ComponentModel.Design;
using Nistec.WinForms.Controls;
using Nistec.Win;




namespace Nistec.WinForms
{

	#region Exmpale

	//On Constructor
//	ValidatorRule vlr = new ValidatorRule();
//	vlr.ValidatorMethod +=new ValidationEventHandler(vlr_ValidationMethod);
//	this.validatorProvider1.SetValidatorRule(this.textBox1, vlr);

//	private void ValidateButton_Click(object sender, System.EventArgs e)
//	{
//	  this.validatorProvider1.ValidatorMessages(!this.validatorProvider1.Validate());
//	}
//
//	private void vlr_ValidationMethod(object sender, ValidationEventArgs e)
//	{
//	   e.IsValid = e.Value.ToString().Equals("aaa") || e.Value.ToString().Equals("111") ;
//	   e.ErrorMessage = "{McName} is not equal to  ('000', '999').";
//	}

	#endregion

	public enum ValidatorDisplay
	{
		None,
		Static,
		Dynamic,
		Handel
	}

	/// <summary>
	/// Provider validation properties to controls that can be validated.
	/// </summary>
	[ProvideProperty("ValidatorRule", typeof(Control))]
	[Designer(typeof(Nistec.WinForms.ValidatorProviderDesigner))]
	[ToolboxBitmap(typeof(McValidator), "Toolbox.Validator.bmp")]
	public class McValidator : System.ComponentModel.Component, IExtenderProvider
	{
		private Hashtable					_ValidationRules		= new Hashtable();
		private ValidatorRule				_DefaultValidationRule	= new ValidatorRule();
		private McErrorProvider			_ErrorProvider			= new McErrorProvider();
		private ValidatorDisplay			_ValidatorDisplay		=ValidatorDisplay.None;
        private string _ErrorMessageResult = "";
        //public event EventHandler RuleChanged;

		#region "public Validation Methods"

		[DefaultValue(ErrProviders.ErrIcon)]
		public ErrProviders Provider
		{
			get{return this._ErrorProvider.Provider;}
			set{this._ErrorProvider.Provider=value;}
		}

		[DefaultValue(ValidatorDisplay.None)]
		public ValidatorDisplay ValidatorDisplay
		{
			get{return this._ValidatorDisplay;}
			set{this._ValidatorDisplay=value;}
		}

        [DefaultValue("Validator")]
        public string Caption
        {
            get { return this._ErrorProvider.Caption; }
            set { this._ErrorProvider.Caption = value; }
        }

        [Browsable(false), DefaultValue("")]
        public string ErrorMessageResult
        {
            get { return this._ErrorMessageResult; }
        }

        public bool ValidateAll()
		{
            return ValidateAll(this.Provider);
		}

        public bool ValidateAll(ErrProviders provider)
		{
            bool valid = ValidateInternal(provider == ErrProviders.ErrIcon);
			string msg="";
			if(!valid)
			{
                msg = ErrorMessageResult;
                switch (provider)
                {
                    case ErrProviders.None:
                        //Do nothing
                        break;
                    case ErrProviders.ErrIcon:
                        //Do nothing
                        break;
                    case ErrProviders.Info:
                        Nistec.WinForms.MsgDlg.ShowMsg(msg,Caption);
                        break;
                    case ErrProviders.MsgBox:
                        Nistec.WinForms.MsgDlg.ShowDialog(msg, Caption);
                        //MsgBox.ShowError(msg);
                        break;
                    case ErrProviders.NotifyBar:
                        Nistec.WinForms.NotifyWindow.ShowNotifyMsg(null, NotifyStyle.Msg, Caption, msg);
                        break;
                }
			}

			return valid;
		}

        /// <summary>
        /// Perform validation on all controls.
        /// </summary>
        /// <returns>False if any control contains invalid data.</returns>
        public bool Validate()
        {
            bool bIsValid = true;
            ValidatorRule vr = null;
            StringBuilder sb = new StringBuilder();
            foreach (Control ctrl in _ValidationRules.Keys)
            {
                this.Validate(ctrl);

                vr = this.GetValidatorRule(ctrl);
                if (vr != null && vr.IsValid == false)
                {
                    sb.AppendLine(vr.ResultErrorMessage);
                    bIsValid = false;
                }
            }

            _ErrorMessageResult = sb.ToString();
            return bIsValid;
        }

        /// <summary>
        /// Perform validation on all controls.
        /// </summary>
        /// <param name="showErrorIcon"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        internal bool ValidateInternal(bool showErrorIcon)
        {
            bool bIsValid = true;
            bool shouldResetIcon = Provider == ErrProviders.ErrIcon;
            StringBuilder sb = new StringBuilder();
            ValidatorRule vr = null;
            foreach (Control ctrl in _ValidationRules.Keys)
            {
                vr = this.GetValidatorRule(ctrl);
                string msg = "";
                if (vr != null)
                {
                    Validate(vr, ctrl);
                    if (!vr.IsValid)
                    {
                        bIsValid = false;
                        if (vr.ErrorMessage.Length > 0)
                        {
                            string field = vr.FieldName != "" ? vr.FieldName : ctrl.Name;

                            msg = vr.ErrorMessage.Replace("{McName}", field);
                            msg = msg.Replace("{0}", field);
                        }
                        msg = vr.ResultErrorMessage + " " + msg;
                        sb.Append(msg);
                        sb.Append(Environment.NewLine);
                    }
                    if (showErrorIcon)
                        this._ErrorProvider.SetError(ctrl, msg);
                    else if(shouldResetIcon)
                        this._ErrorProvider.SetError(ctrl, null);

                }
            }
            _ErrorMessageResult = sb.ToString();
            return bIsValid;
        }


		/// <summary>
		/// Reset validator for all controls
		/// </summary>
		public void ResetErrIcon()
		{
			if(Provider==ErrProviders.ErrIcon)
			{
				foreach(Control ctrl in _ValidationRules.Keys)
				{
					this._ErrorProvider.SetError(ctrl, null);
				}
			}
		}

        ///// <summary>
        ///// Get validation error messages.
        ///// </summary>
        //public string ValidatorMessages(bool showErrorIcon)
        //{
        //    return ValidatorMessages(showErrorIcon, false);
        //}

        //private string ValidatorMessages(bool showErrorIcon, bool forces)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    ValidatorRule vr = null;
        //    foreach(Control ctrl in _ValidationRules.Keys)
        //    {
        //        vr = this.GetValidatorRule(ctrl);
        //        string msg="";
        //        if (vr != null) 
        //        {
        //            //Validate(vr,ctrl);
        //            if (!vr.IsValid) 
        //            {
        //                if (vr.ErrorMessage.Length > 0)
        //                {
        //                    string field = vr.FieldName != "" ? vr.FieldName : ctrl.Name;

        //                    msg = vr.ErrorMessage.Replace("{McName}", field);
        //                    msg = msg.Replace("{0}", field);
        //                }
        //                msg=vr.ResultErrorMessage + " " + msg;
        //                sb.Append(msg);
        //                sb.Append(Environment.NewLine);
        //            }
        //            if(Provider==ErrProviders.ErrIcon || forces)
        //            {
        //                if (showErrorIcon)
        //                    this._ErrorProvider.SetError(ctrl, msg);
        //                else
        //                    this._ErrorProvider.SetError(ctrl, null);
        //            }
        //        }
        //    }
        //    return sb.ToString();
        //}
		#endregion

		#region "private helper methods"
		/// <summary>
		/// Perform validation on specific control.
		/// </summary>
		/// <param name="ctrl"></param>
		/// <returns></returns>
		private bool Validate(Control ctrl)
		{
			ValidatorRule vr = this.GetValidatorRule(ctrl);

			if (vr != null) 
			{
				vr.ResultErrorMessage = string.Empty;
				vr.IsValid = true;
			}

			if (vr == null || vr.IsValid)
				vr = this.DataTypeValidate(ctrl);

			if (vr == null || vr.IsValid)
				vr = this.CompareValidate(ctrl);

			if (vr == null || vr.IsValid)
				vr = this.CustomValidate(ctrl);

			if (vr == null || vr.IsValid)
				vr = this.RangeValidate(ctrl);

			if (vr == null || vr.IsValid)
				vr = this.RegularExpressionValidate(ctrl);

			if (vr == null || vr.IsValid)
				vr = this.RequiredFieldValidate(ctrl);

			return (vr == null) ? true : vr.IsValid;
		}

		/// <summary>
		/// Validate Data Type.
		/// </summary>
		/// <param name="ctrl"></param>
		/// <returns></returns>
		private ValidatorRule DataTypeValidate(Control ctrl)
		{
			ValidatorRule vr = this._ValidationRules[ctrl] as ValidatorRule;

            DataTypeValidate(vr, ctrl);

            //if (vr != null && vr.Operator.Equals(ValidationOperator.DataTypeCheck))
            //{
            //    if (vr.DataType.Equals(this._DefaultValidationRule.DataType)) return vr;

            //        BaseDataType vdt = 
            //        (BaseDataType)Enum.Parse(
            //        typeof(BaseDataType), vr.DataType.ToString());

            //    vr.IsValid = Validator.CanConvert(ctrl.Text, vdt);
            //    if(!vr.IsValid)
            //    {
            //        vr.ResultErrorMessage= RM.GetString(RM.ErrorDataType);
            //    }
            //}

			return vr;
		}

		/// <summary>
		/// Perform CompareValidate on a specific control.
		/// </summary>
		/// <param name="ctrl"></param>
		/// <returns>true if control has no validation rule.</returns>
		private ValidatorRule CompareValidate(Control ctrl)
		{
			ValidatorRule vr = _ValidationRules[ctrl] as ValidatorRule;

			if (vr != null)
			{
                CompareValidate(vr, ctrl);

                //if (this._DefaultValidationRule.ValueToCompare.Equals(vr.ValueToCompare)
                //    && this._DefaultValidationRule.Operator.Equals(vr.Operator)) return vr;

                //vr.IsValid = ValidatorRule.Compare(ctrl.Text, vr.ValueToCompare, vr.Operator, vr);
                //if(!vr.IsValid)
                //{
                //    vr.ResultErrorMessage= string.Format("{0}:{1}",RM.GetString(RM.ErrorValidating),vr.ValueToCompare);
                //}

			}
			
			return vr;
		}

		/// <summary>
		/// Perform Custom Validation on specific control.
		/// </summary>
		/// <param name="ctrl"></param>
		/// <returns></returns>
		private ValidatorRule CustomValidate(Control ctrl)
		{
			ValidatorRule vr = _ValidationRules[ctrl] as ValidatorRule;

			if (vr != null)
			{
                CustomValidate(vr, ctrl);
                //ValidationEventArgs e = new ValidationEventArgs(ctrl.Text, vr);
                //vr.OnCustomValidationMethod(e);
			}
			return vr;
		}


		/// <summary>
		/// Perform RangeType Validation on specific control.
		/// </summary>
		/// <param name="ctrl"></param>
		/// <returns></returns>
		private ValidatorRule RangeValidate(Control ctrl)
		{
			ValidatorRule vr = _ValidationRules[ctrl] as ValidatorRule;

			if (vr != null)
			{

                RangeValidate(vr, ctrl);

//				if(vr.RangeValue==null)
//				{
//					vr.IsValid =true;
//					return vr;
//				}
//				string text=ctrl.Text;
//				if(vr.DataType==BaseDataType.Currency)
//					text=Nistec.Util.Regx.ParseCurrencyToNumber(text,text);
//                vr.IsValid = vr.RangeValue.IsValid(text);

                //if (this.IsDefaultRange(vr)) return vr;

                //vr.IsValid = ValidatorRule.Compare(ctrl.Text, vr.MinimumValue, ValidationOperator.GreaterThanEqual, vr);

                //if (vr.IsValid)
                //    vr.IsValid = ValidatorRule.Compare(ctrl.Text, vr.MaximumValue, ValidationOperator.LessThanEqual, vr);
                //if(!vr.IsValid)
                //{
                //    vr.ResultErrorMessage= RM.GetString(RM.ValueOutOfRange_v2,new object[]{vr.MaximumValue,vr.MinimumValue});
                //    //vr.ResultErrorMessage= string.Format("{0} {1}:{2}",RM.GetString(RM.ValueOutOfRange,new object[]{vr.MinimumValue,vr.MaximumValue}),vr.MinimumValue,vr.MaximumValue);
                //}

			}
			return vr;
		}

		/// <summary>
		/// Check if validation rule range is default.
		/// </summary>
		/// <param name="vr"></param>
		/// <returns></returns>
		private bool IsDefaultRange(ValidatorRule vr)
		{
			return (this._DefaultValidationRule.MinimumValue.Equals(vr.MinimumValue)
				&& this._DefaultValidationRule.MaximumValue.Equals(vr.MaximumValue));
			
		}

		/// <summary>
		/// Perform Regular Expression Validation on a specific control.
		/// </summary>
		/// <param name="ctrl"></param>
		/// <returns></returns>
		private ValidatorRule RegularExpressionValidate(Control ctrl)
		{
			ValidatorRule vr = _ValidationRules[ctrl] as ValidatorRule;

			if (vr != null)
			{
                RegularExpressionValidate(vr, ctrl);
                //try
                //{
                //    if (this._DefaultValidationRule.RegExPattern.Equals(vr.RegExPattern)) return vr;

                //    vr.IsValid = Validator.ValidateRegex(ctrl.Text, vr.RegExPattern);
                //    if(!vr.IsValid)
                //    {
                //        vr.ResultErrorMessage= RM.GetString(RM.ErrorValidating);
                //    }
                //}
                //catch(Exception ex)
                //{
                //    vr.ResultErrorMessage = "RegEx Validation Exception: " + ex.Message + Environment.NewLine;
                //    vr.IsValid = false;
                //}
			}
			return vr;
		}

		/// <summary>
		/// Perform RequiredField Validation on a specific control.
		/// </summary>
		/// <param name="ctrl"></param>
		/// <returns></returns>
		private ValidatorRule RequiredFieldValidate(Control ctrl)
		{
			ValidatorRule vr = _ValidationRules[ctrl] as ValidatorRule;

			if (vr != null && vr.Required) 
			{
                RequiredFieldValidate(vr, ctrl);
                //ValidatorRule vr2 = new ValidatorRule();
                //vr.IsValid = ctrl.Text.Length>0;
		
                ////vr.IsValid =Validator.ValidateText(ctrl.Text,true,ref msg);
                ////vr.IsValid = !ValidatorRule.Compare(ctrl.Text, vr.InitialValue, ValidationOperator.Equal, vr);
                //if(!vr.IsValid)
                //{
                //    vr.ResultErrorMessage = RM.GetString(RM.RequiredField);
                //}

			}

			return vr;
		}

		#endregion

        #region "private helper methods 2"

        /// <summary>
        /// Validate
        /// </summary>
        /// <param name="vr"></param>
        /// <param name="ctrl"></param>
        /// <returns></returns>
        private bool Validate(ValidatorRule vr,Control ctrl)
        {
            if (vr == null)
                return true;
            if (!vr.IsValid)
                return false;

            vr.ResultErrorMessage = string.Empty;
            //vr.IsValid = true;

            if (vr.IsValid)
               this.DataTypeValidate(vr, ctrl);

            if (vr.IsValid)
                this.CompareValidate(vr, ctrl);

            if (vr.IsValid)
                this.CustomValidate(vr, ctrl);

            if (vr.IsValid)
                this.RangeValidate(vr, ctrl);

            if (vr.IsValid)
                this.RegularExpressionValidate(vr, ctrl);

            if (vr.IsValid)
                this.RequiredFieldValidate(vr, ctrl);

            return vr.IsValid;
        }


        /// <summary>
        /// Validate Data Type.
        /// </summary>
        /// <param name="vr">ValidatorRule</param>
        /// <param name="ctrl">Control</param>
        private void DataTypeValidate(ValidatorRule vr,Control ctrl)
        {
            if (vr != null && vr.Operator.Equals(ValidationOperator.DataTypeCheck))
            {
                if (vr.DataType.Equals(this._DefaultValidationRule.DataType)) return ;

                BaseDataType vdt =
                (BaseDataType)Enum.Parse(
                typeof(BaseDataType), vr.DataType.ToString());

                vr.IsValid = Validator.CanConvert(ctrl.Text, vdt);
                if (!vr.IsValid)
                {
                    vr.ResultErrorMessage = RM.GetString(RM.ErrorDataType);
                }
            }
        }

        /// <summary>
        /// Perform CompareValidate on a specific control.
        /// </summary>
        /// <param name="vr">ValidatorRule</param>
        /// <param name="ctrl">Control</param>
        private void CompareValidate(ValidatorRule vr, Control ctrl)
        {
            if (vr != null)
            {
                if (this._DefaultValidationRule.ValueToCompare.Equals(vr.ValueToCompare)
                    && this._DefaultValidationRule.Operator.Equals(vr.Operator)) return ;

                vr.IsValid = ValidatorRule.Compare(ctrl.Text, vr.ValueToCompare, vr.Operator, vr);
                if (!vr.IsValid)
                {
                    vr.ResultErrorMessage = string.Format("{0}:{1}", RM.GetString(RM.ErrorValidating), vr.ValueToCompare);
                }

            }
        }

        /// <summary>
        /// Perform Custom Validation on specific control.
        /// </summary>
        /// <param name="vr">ValidatorRule</param>
        /// <param name="ctrl">Control</param>
        private void CustomValidate(ValidatorRule vr, Control ctrl)
        {
            if (vr != null)
            {
                ValidationEventArgs e = new ValidationEventArgs(ctrl.Text, vr);
                vr.OnCustomValidationMethod(e);
            }
        }


        /// <summary>
        /// Perform RangeType Validation on specific control.
        /// </summary>
        /// <param name="vr">ValidatorRule</param>
        /// <param name="ctrl">Control</param>
        private void RangeValidate(ValidatorRule vr, Control ctrl)
        {
            if (vr != null)
            {
                if (this.IsDefaultRange(vr)) return ;

                vr.IsValid = ValidatorRule.Compare(ctrl.Text, vr.MinimumValue, ValidationOperator.GreaterThanEqual, vr);

                if (vr.IsValid)
                    vr.IsValid = ValidatorRule.Compare(ctrl.Text, vr.MaximumValue, ValidationOperator.LessThanEqual, vr);
                if (!vr.IsValid)
                {
                    vr.ResultErrorMessage = RM.GetString(RM.ValueOutOfRange_v2, new object[] { vr.MaximumValue, vr.MinimumValue });
                    //vr.ResultErrorMessage= string.Format("{0} {1}:{2}",RM.GetString(RM.ValueOutOfRange,new object[]{vr.MinimumValue,vr.MaximumValue}),vr.MinimumValue,vr.MaximumValue);
                }

            }
        }

          /// <summary>
        /// Perform Regular Expression Validation on a specific control.
        /// </summary>
        /// <param name="vr">ValidatorRule</param>
        /// <param name="ctrl">Control</param>
        private void RegularExpressionValidate(ValidatorRule vr, Control ctrl)
        {
            if (vr != null)
            {
                try
                {
                    if (this._DefaultValidationRule.RegExPattern.Equals(vr.RegExPattern)) return ;

                    vr.IsValid = Validator.ValidateRegex(ctrl.Text, vr.RegExPattern);
                    if (!vr.IsValid)
                    {
                        vr.ResultErrorMessage = RM.GetString(RM.ErrorValidating);
                    }
                }
                catch (Exception ex)
                {
                    vr.ResultErrorMessage = "RegEx Validation Exception: " + ex.Message + Environment.NewLine;
                    vr.IsValid = false;
                }
            }
        }

        /// <summary>
        /// Perform RequiredField Validation on a specific control.
        /// </summary>
        /// <param name="vr">ValidatorRule</param>
        /// <param name="ctrl">Control</param>
        private void RequiredFieldValidate(ValidatorRule vr, Control ctrl)
        {
            if (vr != null && vr.Required)
            {
                ValidatorRule vr2 = new ValidatorRule();
                vr.IsValid = ctrl.Text.Length > 0;

                //vr.IsValid =Validator.ValidateText(ctrl.Text,true,ref msg);
                //vr.IsValid = !ValidatorRule.Compare(ctrl.Text, vr.InitialValue, ValidationOperator.Equal, vr);
                if (!vr.IsValid)
                {
                    vr.ResultErrorMessage = RM.GetString(RM.RequiredField);
                }

            }

        }

        #endregion

		#region "Properties"
		/// <summary>
		/// Set validation rule.
		/// </summary>
		/// <param name="control"></param>
		/// <param name="vr"></param>
		public void SetValidatorRule(object control, ValidatorRule vr)
		{
            //MessageBox.Show("vld0");
            //MessageBox.Show("vld0" + control.ToString());

			if (control != null)
			{
				// only throw error in DesignMode
				if (base.DesignMode) 
				{
					if (!this.CanExtend(control))
						throw new InvalidOperationException(control.GetType().ToString() 
							+ " is not supported by the validator provider.");

//					if (!this.IsDefaultRange(vr) 
//						&& ValidatorRule.Compare(vr.MinimumValue, vr.MaximumValue, ValidationOperator.GreaterThanEqual, vr))
//						throw new ArgumentException("MinimumValue must not be greater than or equal to MaximumValue.");
				}
               
				//ValidatorRule vrOld = this._ValidationRules[control] as ValidatorRule;

                bool flag = _ValidationRules.ContainsKey(control);

				// if new rule is valid and in not DesignMode, clone rule
				if ((vr != null) && !base.DesignMode)
				{
					vr = vr.Clone() as ValidatorRule;
				} 
				
				
				if (vr == null)	// if new is null, no more validation
				{
					this._ValidationRules.Remove(control);
					((Control)control).Validating-=new CancelEventHandler(McValidatorProvider_Validating); 
				}
                else if (!flag)//vrOld == null)
				{
					this._ValidationRules.Add(control, vr);
				}
                else if ((vr != null) && (flag))//(vrOld != null))
				{
					this._ValidationRules[control] = vr;
				}

				if(this._ValidationRules.Contains(control))
				{
					((Control)control).Validating+=new CancelEventHandler(McValidatorProvider_Validating); 
				}

			}
		}

		private void McValidatorProvider_Validating(object sender, CancelEventArgs e)
		{
			if(_ValidatorDisplay==ValidatorDisplay.Dynamic || _ValidatorDisplay==ValidatorDisplay.Handel)
			{
				bool ok=McValidating(((Control)sender),true);
				if(!ok && _ValidatorDisplay==ValidatorDisplay.Handel)
				{
					e.Cancel=true;
				}
			}
		}

		private bool McValidating(Control control,bool showErrorMessage)
		{
			bool bIsValid = true;
			ValidatorRule vr = null;
			if(_ValidationRules.Contains(control))
			{
				this.Validate(control);
			
				vr = this.GetValidatorRule(control);
				if (vr != null && vr.IsValid == false)
					bIsValid = false;

				StringBuilder sb = new StringBuilder();
				if (bIsValid == false) 
				{
                    if (vr.ErrorMessage.Length > 0)
                    {
                        string field = vr.FieldName;// != "" ? vr.FieldName : ctrl.Name;

                        string msg = vr.ErrorMessage.Replace("{McName}", field);
                        msg = msg.Replace("{0}", field);
                        sb.Append(msg);
                        sb.Append(Environment.NewLine);
                    }
					//vr.ResultErrorMessage += vr.ErrorMessage.Replace("{McName}", control.Name);
					
                    sb.Append(vr.ResultErrorMessage);
					sb.Append(Environment.NewLine);
				}
				if (!bIsValid && showErrorMessage)
					this._ErrorProvider.SetError(control, sb.ToString());
				else if(bIsValid && Provider==ErrProviders.ErrIcon)
					this._ErrorProvider.SetError(control, "");
	
			}
			return bIsValid;
		}
		

		public void RemoveValidatorRule(object control, ValidatorRule vr)
		{
			if (control != null)
			{
				if (vr != null)	// if new is null, no more validation
				{
					this._ValidationRules.Remove(control);
					((Control)control).Validating-=new CancelEventHandler(McValidatorProvider_Validating); 
					vr=null;
				}

			}

		}


		/// <summary>
		/// Get validation rule.
		/// </summary>
		/// <param name="control"></param>
		/// <returns></returns>
		[DefaultValue(null), Category("Data")]
		[Editor(typeof(Nistec.WinForms.ValidatorRuleEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public ValidatorRule GetValidatorRule(object control)
		{
            //if (_ValidationRules == null)
            //    _ValidationRules = new Hashtable();

            if (!_ValidationRules.Contains(control))
            {
                return null;
            }
			return (this._ValidationRules[control] as ValidatorRule);
		}
		#endregion

		#region "ErrorProvider properties delegation"
		/// <summary>
		/// Icon display when validation failed.
		/// </summary>
		[Category("Appearance"), Description("Icon display when validation failed."), Localizable(true)]
		public Icon Icon
		{
			get { return this._ErrorProvider.Icon; }
			set { this._ErrorProvider.Icon = value; }
		}

		/// <summary>
		/// BlinkRate of ErrorIcon.
		/// </summary>
		[RefreshProperties(RefreshProperties.Repaint), Description("BlinkRate of ErrorIcon."), Category("Behavior"), DefaultValue(250)]
		public int BlinkRate
		{
			get { return this._ErrorProvider.BlinkRate;}
			set { this._ErrorProvider.BlinkRate = value; }
		}

		/// <summary>
		/// Get or set Blink Behavior.
		/// </summary>
		[DefaultValue(0), Category("Behavior"), Description("Blink Behavior.")]
		public ErrorBlinkStyle BlinkStyle
		{
			get { return this._ErrorProvider.BlinkStyle; }
			set { this._ErrorProvider.BlinkStyle = value; }
		}
 
		/// <summary>
		/// Get Error Icon alignment.
		/// </summary>
		/// <param name="control"></param>
		/// <returns></returns>
		[DefaultValue(3), Category("Appearance"), Localizable(true), Description("Get Error Icon alignment.")]
		public ErrorIconAlignment GetIconAlignment(Control control)
		{
			return this._ErrorProvider.GetIconAlignment(control);
		}
 
		/// <summary>
		/// Get Error Icon padding.
		/// </summary>
		/// <param name="control"></param>
		/// <returns></returns>
		[Description("Get Error Icon padding."), DefaultValue(0), Localizable(true), Category("Appearance")]
		public int GetIconPadding(Control control)
		{
			return this._ErrorProvider.GetIconPadding(control);
		}
 
		/// <summary>
		/// Set Error Icon alignment.
		/// </summary>
		/// <param name="control"></param>
		/// <param name="value"></param>
		public void SetIconAlignment(Control control, ErrorIconAlignment value)
		{
			this._ErrorProvider.SetIconAlignment(control, value);
		}
 
		/// <summary>
		/// Set Error Icon padding.
		/// </summary>
		/// <param name="control"></param>
		/// <param name="padding"></param>
		public void SetIconPadding(Control control, int padding)
		{
			this._ErrorProvider.SetIconPadding(control, padding);
		}
		#endregion

		#region "Component Construction"
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Designer Ctor.
		/// </summary>
		/// <param name="container"></param>
		public McValidator(System.ComponentModel.IContainer container):this()
		{
			container.Add(this);
		}

		/// <summary>
		/// Default Ctor.
		/// </summary>
		public McValidator()
		{
            _ErrorProvider.Caption = "Validator";
            InitializeComponent();
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		#endregion

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			//_DefaultValidationRule.DataTypeChanged+=new EventHandler(_DefaultValidationRule_DataTypeChanged);
           
		}

//		private void _DefaultValidationRule_DataTypeChanged(object sender, EventArgs e)
//		{
//            if(this.RuleChanged!=null)
//               this.RuleChanged(sender,e);
//		}
		#endregion

		#region IExtenderProvider Members

		/// <summary>
		/// Determine if ValidationProvider support a component.
		/// </summary>
		/// <param name="extendee"></param>
		/// <returns></returns>
		public bool CanExtend(object extendee)
		{
			if ((extendee is System.Windows.Forms.TextBox) 
				|| (extendee is ComboBox)|| (extendee is McEditBase)) return true;

			return false;
		}

		#endregion

	}
}
