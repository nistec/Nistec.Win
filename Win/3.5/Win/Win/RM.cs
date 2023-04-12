using System;
using System.Globalization;
using System.Resources;
using System.Windows.Forms;
using System.Reflection;
using System.Collections;


namespace MControl.Win
{

	public sealed class RM
	{

		#region Ctor
		private ResourceManager resources;
		private static RM loader;
		public readonly static	CultureInfo DefualtCulture;

		private static Hashtable	_Cultures= new Hashtable();
		private	static CultureInfo	_CultureInfo;


		static RM()
		{
			RM.DefualtCulture=new CultureInfo("en",false);
			RM.loader = null;
			RM.SetCultures();
			RM.Culture=CultureInfo.CurrentCulture;
		}

		public RM()
		{
					
			this.resources = new ResourceManager("MControl.Framework.Resources.SR", Assembly.GetExecutingAssembly());
				//this.resources = new ResourceManager("MControl.Framework.Resources.SR", base.GetType().Module.Assembly);
		}

        public RM(string resource,Assembly assembly)
        {

            this.resources = new ResourceManager(resource, assembly);
            //this.resources = new ResourceManager("MControl.Framework.Resources.SR", base.GetType().Module.Assembly);
        }

        public RM(ResourceManager resource)
        {
            this.resources = resource;
        }

		#endregion

		#region Cultures

        public static Hashtable Cultures
        {
            get { return _Cultures; }
        }

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
				   _CultureInfo= RM.DefualtCulture;
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
					_CultureInfo= RM.DefualtCulture;
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
//				_CultureInfo= RM.DefualtCulture;
//			}
//			return _CultureInfo;
//		}

		#endregion
 
		#region Methods

		public static bool GetBoolean(string name)
		{
			return RM.GetBoolean(null, name);
		}

 
		public static bool GetBoolean(CultureInfo culture, string name)
		{
			bool flag1 = false;
			RM SRL1 = RM.GetLoader();
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
			return RM.GetByte(null, name);
		}

		public static byte GetByte(CultureInfo culture, string name)
		{
			byte num1 = 0;
			RM SRL1 = RM.GetLoader();
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
			return RM.GetChar(null, name);
		}

		public static char GetChar(CultureInfo culture, string name)
		{
			char ch1 = '\0';
			RM SRL1 = RM.GetLoader();
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
			return RM.GetDouble(null, name);
		}

 
		public static double GetDouble(CultureInfo culture, string name)
		{
			double num1 = 0;
			RM SRL1 = RM.GetLoader();
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
			return RM.GetFloat(null, name);
		}

 
		public static float GetFloat(CultureInfo culture, string name)
		{
			float single1 = 0f;
			RM SRL1 = RM.GetLoader();
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
			return RM.GetInt(null, name);
		}

		public static int GetInt(CultureInfo culture, string name)
		{
			int num1 = 0;
			RM SRL1 = RM.GetLoader();
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

		private static RM GetLoader()
		{
			if (RM.loader == null)
			{
				lock (typeof(RM))
				{
					if (RM.loader == null)
					{
						RM.loader = new RM();
					}
				}
			}
			return RM.loader;
		}

		public static long GetLong(string name)
		{
			return RM.GetLong(null, name);
		}

		public static long GetLong(CultureInfo culture, string name)
		{
			long num1 = 0;
			RM SRL1 = RM.GetLoader();
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
			return RM.GetObject(null, name);
		}

		public static object GetObject(CultureInfo culture, string name)
		{
			RM SRL1 = RM.GetLoader();
			if (SRL1 == null)
			{
				return null;
			}
			return SRL1.resources.GetObject(name, culture);
		}

		public static short GetShort(string name)
		{
			return RM.GetShort(null, name);
		}

		public static short GetShort(CultureInfo culture, string name)
		{
			short num1 = 0;
			RM SRL1 = RM.GetLoader();
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
			return RM.GetString(null, name);
		}
 
		public static string GetString(CultureInfo culture, string name)
		{
			RM SRL1 = RM.GetLoader();
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
					return SRL1.resources.GetString(name, RM.DefualtCulture);
				}
				catch
				{
					return name;
				}
			}
		}

		public static string GetString(string name, params object[] args)
		{
			return RM.GetString(null, name, args);
		}

		public static string GetString(CultureInfo culture,string name, string args)
		{
			return RM.GetString(null, name, new object[]{args});
		}

 
		public static string GetString(CultureInfo culture, string name, params object[] args)
		{
			RM SRL1 = RM.GetLoader();
			if (SRL1 == null)
			{
				return null;
			}
			string text1 = name;
			if(culture==null)
			{
				culture=RM.Culture;// CultureInfo.CurrentCulture;
			}
			else if(!isCultureSopperted(culture.Name))
			{
				culture=RM.Culture;
			}

			try
			{
				text1 = SRL1.resources.GetString(name, culture);
			}
			catch
			{
				try
				{
					text1= SRL1.resources.GetString(name, RM.Culture);
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

		public static DialogResult ShowQuestion(string name)
		{
			return MsgBox.ShowQuestion(RM.GetString(null,name));
		}

		public static void ShowInfo(string name)
		{
			MsgBox.ShowInfo(RM.GetString(null,name));
		}

		public static void ShowWarning(string name)
		{
			MsgBox.ShowWarning(RM.GetString(null, name));
		}

		public static void ShowError(string name)
		{
			MsgBox.ShowError(RM.GetString(null, name));
		}


		public static DialogResult ShowQuestion(string name,string args)
		{
			return MsgBox.ShowQuestion(RM.GetString(null,name,args));
		}

		public static void ShowInfo(string name,string args)
		{
			MsgBox.ShowInfo(RM.GetString(null,name,args));
		}

		public static void ShowWarning(string name,string args)
		{
			MsgBox.ShowWarning(RM.GetString(null,name,args));
		}

		public static void ShowError(string name,string args)
		{
			MsgBox.ShowError(RM.GetString(null,name,args));
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
		public const string ErrorNotExpected_v =  "ErrorNotExpected";
		public const string LargerIndexThanZero =  "LargerIndexThanZero";
		public const string NullArgument =  "NullArgument";
		//public const string KeyNotConfigured =  "KeyNotConfigured";
		//public const string SizeOfKeyInvalidates =  "SizeOfKeyInvalidates";
		public const string FileNotFound_v =  "FileNotFound";
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
	
		public const string InvalidDateFormat_v = "InvalidDateFormat";
		public const string InvalidDate = "InvalidDate";
		public const string InvalidEmail = "InvalidEmail";
		public const string InvalidIp = "InvalidIp";
		public const string InvalidTelephoneFormat = "InvalidTelephoneFormat";
		public const string InvalidTelephoneAreaFormat = "InvalidTelephoneAreaFormat";
		public const string InvalidZipFormat = "InvalidZipFormat";
		public const string InvalidTimeFormat_v = "InvalidTimeFormat";
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
		public const string ElementNotFound_v = "ElementNotFound";
		public const string NodeNotFound_v = "NodeNotFound";
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
		public const string ValueOutOfRange_v2 = "OutOfRange";
		public const string InvalidRegx = "InvalidRegx"; 
		public const string ValueNotMatchMask_v = "ValueNotMatchMask"; 

		public const string InvalidCast_v="InvalidCast";

		#endregion

	}
 

}
