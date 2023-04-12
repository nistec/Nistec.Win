using System;
using System.Globalization;
using System.Resources;
using System.Windows.Forms;
using System.Reflection;
using System.Collections;
using Nistec.Win;



namespace Nistec.Wizards
{

	public sealed class RML
	{

		#region Ctor
		private ResourceManager resources;
		private static RML loader;
		public readonly static	CultureInfo DefualtCulture;

		private static Hashtable	_Cultures= new Hashtable();
		private	static CultureInfo	_CultureInfo;


		static RML()
		{
			RML.DefualtCulture=new CultureInfo("he",false);
			RML.loader = null;
			RML.SetCultures();
			RML.Culture=CultureInfo.CurrentCulture;
		}

		public RML()
		{
					
			this.resources = new ResourceManager("Business.Rules.Resources.BRM", Assembly.GetExecutingAssembly());
				//this.resources = new ResourceManager("Nistec.Framework.Resources.SR", base.GetType().Module.Assembly);
		}

		private static RML GetLoader()
		{
			if (RML.loader == null)
			{
				lock (typeof(RML))
				{
					if (RML.loader == null)
					{
						RML.loader = new RML();
					}
				}
			}
			return RML.loader;
		}

		#endregion

		#region Cultures

		private static void SetCultures()
		{
			_Cultures.Add("en","English");
			_Cultures.Add("he","Hebrew");
		}

		public static bool isCultureSopperted(string clt)
		{
			return _Cultures.Contains(clt);
		}

		private static CultureInfo Culture
		{
			get
			{
				if(_CultureInfo==null)
				{
				   _CultureInfo= RML.DefualtCulture;
				}
				return _CultureInfo;
			} 
			set
			{
				string cultureName= value.TwoLetterISOLanguageName;//.Name.Substring(0,2);
				if(isCultureSopperted(cultureName))
				{
					_CultureInfo= new CultureInfo(cultureName,false);
				}
				else
				{
					_CultureInfo= RML.DefualtCulture;
				}
			}
		}
		
//		private CultureInfo GetCurrentCulture()
//		{
//			//_CultureInfo=CultureInfo.CurrentCulture;
//			string cultureName=CultureInfo.CurrentCulture.Name.PadLeft(2);
//			if(isCultureSopperted(cultureName))
//			{
//				_CultureInfo= new CultureInfo(cultureName,false);
//			}
//			else
//			{
//				_CultureInfo= BRM.DefualtCulture;
//			}
//			return _CultureInfo;
//		}

		#endregion
 
		#region Methods

		public static bool GetBoolean(string name)
		{
			return RML.GetBoolean(null, name);
		}

 
		public static bool GetBoolean(CultureInfo culture, string name)
		{
			bool flag1 = false;
			RML SRL1 = RML.GetLoader();
			if (SRL1 != null)
			{
				object obj1 = SRL1.resources.GetObject(name, culture);
				if (obj1 is bool)
				{
					flag1 = (bool) obj1;
				}
			}
			return flag1;
		}

		public static byte GetByte(string name)
		{
			return RML.GetByte(null, name);
		}

		public static byte GetByte(CultureInfo culture, string name)
		{
			byte num1 = 0;
			RML SRL1 = RML.GetLoader();
			if (SRL1 != null)
			{
				object obj1 = SRL1.resources.GetObject(name, culture);
				if (obj1 is byte)
				{
					num1 = (byte) obj1;
				}
			}
			return num1;
		}

		public static char GetChar(string name)
		{
			return RML.GetChar(null, name);
		}

		public static char GetChar(CultureInfo culture, string name)
		{
			char ch1 = '\0';
			RML SRL1 = RML.GetLoader();
			if (SRL1 != null)
			{
				object obj1 = SRL1.resources.GetObject(name, culture);
				if (obj1 is char)
				{
					ch1 = (char) obj1;
				}
			}
			return ch1;
		}

 
		public static double GetDouble(string name)
		{
			return RML.GetDouble(null, name);
		}

 
		public static double GetDouble(CultureInfo culture, string name)
		{
			double num1 = 0;
			RML SRL1 = RML.GetLoader();
			if (SRL1 != null)
			{
				object obj1 = SRL1.resources.GetObject(name, culture);
				if (obj1 is double)
				{
					num1 = (double) obj1;
				}
			}
			return num1;
		}

		public static float GetFloat(string name)
		{
			return RML.GetFloat(null, name);
		}

 
		public static float GetFloat(CultureInfo culture, string name)
		{
			float single1 = 0f;
			RML SRL1 = RML.GetLoader();
			if (SRL1 != null)
			{
				object obj1 = SRL1.resources.GetObject(name, culture);
				if (obj1 is float)
				{
					single1 = (float) obj1;
				}
			}
			return single1;
		}

 
		public static int GetInt(string name)
		{
			return RML.GetInt(null, name);
		}

		public static int GetInt(CultureInfo culture, string name)
		{
			int num1 = 0;
			RML SRL1 = RML.GetLoader();
			if (SRL1 != null)
			{
				object obj1 = SRL1.resources.GetObject(name, culture);
				if (obj1 is int)
				{
					num1 = (int) obj1;
				}
			}
			return num1;
		}

		public static long GetLong(string name)
		{
			return RML.GetLong(null, name);
		}

		public static long GetLong(CultureInfo culture, string name)
		{
			long num1 = 0;
			RML SRL1 = RML.GetLoader();
			if (SRL1 != null)
			{
				object obj1 = SRL1.resources.GetObject(name, culture);
				if (obj1 is long)
				{
					num1 = (long) obj1;
				}
			}
			return num1;
		}

		public static object GetObject(string name)
		{
			return RML.GetObject(null, name);
		}

		public static object GetObject(CultureInfo culture, string name)
		{
			RML SRL1 = RML.GetLoader();
			if (SRL1 == null)
			{
				return null;
			}
			return SRL1.resources.GetObject(name, culture);
		}

		public static short GetShort(string name)
		{
			return RML.GetShort(null, name);
		}

		public static short GetShort(CultureInfo culture, string name)
		{
			short num1 = 0;
			RML SRL1 = RML.GetLoader();
			if (SRL1 != null)
			{
				object obj1 = SRL1.resources.GetObject(name, culture);
				if (obj1 is short)
				{
					num1 = (short) obj1;
				}
			}
			return num1;
		}

 
		public static string GetString(string name)
		{
			return RML.GetString(null, name);
		}
 
		public static string GetString(CultureInfo culture, string name)
		{
			RML SRL1 = RML.GetLoader();
			try
			{
				if (SRL1 == null)
				{
					return null;
				}
				if(culture==null)
				{
					culture=Culture;// CultureInfo.CurrentCulture;
				}
				else if(!isCultureSopperted(culture.Name))
				{
					culture=Culture;
				}
				return SRL1.resources.GetString(name, culture);
			}
			catch
			{
				try
				{
					return SRL1.resources.GetString(name, RML.DefualtCulture);
				}
				catch
				{
					return name;
				}
			}
		}

		public static string GetString(string name, object arg)
		{
			object[] args=new object[]{arg};
			return RML.GetString(null, name, args);
		}

		public static string GetString(string name, params object[] args)
		{
			return RML.GetString(null, name, args);
		}

 
		public static string GetString(CultureInfo culture, string name, params object[] args)
		{
			RML SRL1 = RML.GetLoader();
			if (SRL1 == null)
			{
				return null;
			}
			string text1 = name;
			if(culture==null)
			{
				culture=RML.Culture;// CultureInfo.CurrentCulture;
			}
			else if(!isCultureSopperted(culture.Name))
			{
				culture=RML.Culture;
			}

			try
			{
				text1 = SRL1.resources.GetString(name, culture);
			}
			catch
			{
				try
				{
					text1= SRL1.resources.GetString(name, RML.Culture);
				}
				catch
				{
					text1 = name;
				}
			}

			if ((args != null) && (args.Length > 0))
			{
				return string.Format(text1, args);
			}
			return text1;
		}

//		private static CultureInfo GetDefaultCulter()
//		{
//			// Creates and initializes the CultureInfo which uses the international sort.
//			return new CultureInfo( 0x0009, false );
//		}

		#endregion

		#region MsgBox

//		public static void ShowFlyMsg(string msg)
//		{
//			Nistec.WinForms.FlyMsg.ShowFlyMsg(BRM.GetString(null, msg));
//		}

		public static void ShowNotifyBoxInfo(string msg)
		{
            Nistec.WinForms.MsgDlg.ShowMsg(RML.GetString(null, msg));
		}

		public static void ShowNotifyBoxMsg(string msg)
		{
            Nistec.WinForms.MsgDlg.ShowMsg(RML.GetString(null, msg), "Business");
		}

		public static DialogResult ShowNotifyBoxDialog(string msg)
		{
            return Nistec.WinForms.MsgDlg.ShowDialog(RML.GetString(null, msg), "Business");
		}

		public static DialogResult ShowQuestion(string name)
		{
			return MsgBox.ShowQuestion(RML.GetString(null, name));
		}

		public static void ShowInfo(string name,object arg)
		{
			MsgBox.ShowInfo(RML.GetString(name,arg));
		}

		public static void ShowInfo(string name)
		{
			MsgBox.ShowInfo(RML.GetString(null, name));
		}

		public static void ShowWarning(string name,object arg)
		{
			MsgBox.ShowWarning(RML.GetString(name,arg));
		}

		public static void ShowWarning(string name)
		{
			MsgBox.ShowWarning(RML.GetString(null, name));
		}

		public static void ShowError(string name,object arg)
		{
			MsgBox.ShowError(RML.GetString(name,arg));
		}

		public static void ShowError(string name)
		{
			MsgBox.ShowError(RML.GetString(null, name));
		}

		#endregion
	
		#region Fields

		public const string IlegalChar =  "IlegalChar";
		public const string RequiredField =  "RequiredField";
		public const string IndexOutBounds =  "IndexOutBounds";
		public const string Yes =  "Yes";
		public const string No =  "No";
		public const string OK =  "OK";
		public const string Cancel =  "Cancel";
		public const string Retry =  "Retry";
		public const string Abort =  "Abort";
		public const string Ignore =  "Ignore";
		public const string Information =  "Information";
		public const string Error =  "Error";
		public const string Confirmation =  "Confirmation";
		public const string ErrorNotExpected =  "ErrorNotExpected";
		public const string LargerIndexThanZero =  "LargerIndexThanZero";
		public const string NullArgument =  "NullArgument";
		public const string KeyNotConfigured_arg =  "KeyNotConfigured";
		public const string SizeOfKeyInvalidates_arg =  "SizeOfKeyInvalidates";
		public const string FileNotFound =  "FileNotFound";
		public const string Progress =  "Progress";
		public const string MistakeConfigRemoting =  "MistakeConfigRemoting";
		public const string ErrorConvertToNumber =  "ErrorConvertToNumber";
		public const string ErrorConvertToDate =  "ErrorConvertToDate";
		public const string True =  "True";
		public const string False =  "False";
		public const string On =  "On";
		public const string Off =  "Off";
		public const string ErrorDataType =  "ErrorDataType";
		public const string ErrorValidating =  "ErrorValidating";
		public const string ErrorIncorrectFormat =  "ErrorIncorrectFormat";
		public const string KeyNotFound_arg =  "KeyNotFound";


/*
		public const string OnlyDigitsBarAccepted = "OnlyDigitsBarAccepted";
		public const string OnlyDigitsPointsAccepted = "OnlyDigitsPointsAccepted";
		public const string OnlyDigitsMinusSpaceAccepted = "OnlyDigitsMinusSpaceAccepted";
		public const string OnlyDigitsMinusParenthesesAccepted = "OnlyDigitsMinusParenthesesAccepted";
		public const string OnlyDigitsMinusPointsAccepted = "OnlyDigitsMinusPointsAccepted";
		public const string OnlyDigitsMinusAccepted = "OnlyDigitsMinusAccepted";
		public const string OnlyDigitsMinusPointsBarsAccepted = "OnlyDigitsMinusPointsBarsAccepted";
		public const string OnlyDigitsTwoPointsAccepted = "OnlyDigitsTwoPointsAccepted";
		public const string OnlyDigitsPointsBarsAccepted = "OnlyDigitsPointsBarsAccepted";
		public const string OnlyDigitsMinusBarsAccepted = "OnlyDigitsMinusBarsAccepted";
		public const string InvalidDateFormat = "InvalidDateFormat";
		public const string InvalidDate = "InvalidDate";
		public const string InvalidEmail = "InvalidEmail";
		public const string InvalidIp = "InvalidIp";
		public const string InvalidTelephoneFormat = "InvalidTelephoneFormat";
		public const string InvalidTelephoneAreaFormat = "InvalidTelephoneAreaFormat";
		public const string InvalidZipFormat = "InvalidZipFormat";
		public const string InvalidTimeFormat = "InvalidTimeFormat";
		public const string InvalidTime = "InvalidTime";
		public const string InvalidNumber = "InvalidNumber";
		public const string InvalidInscriptionFormat = "InvalidInscriptionFormat";
		public const string InvalidInscription = "InvalidInscription";
		public const string OnlyDigitsAccepted = "OnlyDigitsAccepted";
		public const string InvalidZip = "InvalidZip";
		public const string InvalidAreaCode = "InvalidAreaCode";
		public const string ContentNotExistInList = "ContentNotExistInList";
		public const string InaccessibleNode = "InaccessibleNode";
		public const string IncorrectNodeName = "IncorrectNodeName";
		public const string FinalElementNotFound = "FinalElementNotFound";
		public const string ShowAll = "ShowAll";
		public const string HideAll = "HideAll";
		public const string ElementNotFound = "ElementNotFound";
		public const string NodeNotFound = "NodeNotFound";
		public const string CantInsert = "CantInsert";
		public const string CantRemove = "CantRemove";
		public const string Close = "Close";
		public const string Prominent = "Prominent";
		public const string CalculateAgain = "CalculateAgain";
		public const string PreviousPage = "PreviousPage";
		public const string NextPage = "NextPage";
		public const string NewPageVertical = "NewPageVertical";
		public const string NewPageHorizontal = "NewPageHorizontal";
		public const string InvalidDirectory = "InvalidDirectory";
		public const string Previous = "Previous";
		public const string Next = "Next";
		public const string Finish = "Finish";
		public const string StepNotFound = "StepNotFound";
		public const string FirstStepCannotBeNull = "FirstStepCannotBeNull";
		public const string CloseWizard = "CloseWizard";
		public const string MessageToLeaveAssistant = "MessageToLeaveAssistant";
		public const string NoExistTaskPanels = "NoExistTaskPanels";
		public const string ValueOutOfRange = "OutOfRange";
		public const string InvalidRegx = "InvalidRegx"; 
		public const string ValueNotMatchMask = "ValueNotMatchMask"; 
*/
		#endregion

	}
 

}
