using System;
using System.Runtime.Serialization;


namespace MControl.Util
{
	/// <summary>
	/// Exception that framwork  can raise
	/// </summary>
	[Serializable]
	public class AppException : ApplicationException 
	{

		/// <summary>
		/// A constructor without parameters
		/// </summary>
		internal AppException () {}

		/// <summary>
		/// A constructor with a message parameter
		/// </summary>
		/// <param name="msg">Message parameter</param>
		public AppException (string msg) : base (msg) {}

		/// <summary>
		/// A constructor with message and inner exception parameters
		/// </summary>
		/// <param name="msg">Message parameter</param>
		/// <param name="inner">Inner exception</param>
		public AppException (string msg, Exception inner) : base (msg, inner) { }

		protected AppException(System.Runtime.Serialization.SerializationInfo _Info, System.Runtime.Serialization.StreamingContext _StreamingContext): 
			base(_Info, _StreamingContext)
		{
		}

	
	}

}
	namespace MControl.Util.Exceptions
	{
		[Serializable]
		public class TypeNotSupportedException : AppException
		{
			public TypeNotSupportedException(Type pType):
				base("Type " + pType.ToString() + " not supported exception")
			{
			}
			public TypeNotSupportedException(Type pType, Exception _InnerException):
				base("Type " + pType.ToString() + " not supported exception", _InnerException)
			{
			}
			protected TypeNotSupportedException(System.Runtime.Serialization.SerializationInfo _Info, System.Runtime.Serialization.StreamingContext _StreamingContext): 
				base(_Info, _StreamingContext)
			{
			}

		}

//		[Serializable]
//		public class UnrecognizedCommandLineParametersException : AppException
//		{
//			public UnrecognizedCommandLineParametersException(string parameter):
//				base("Unrecognized command line parameter " + parameter + ".")
//			{
//			}
//			public UnrecognizedCommandLineParametersException(string parameter, Exception _InnerException):
//				base("Unrecognized command line parameter " + parameter + ".", _InnerException)
//			{
//			}
//
//			protected UnrecognizedCommandLineParametersException(System.Runtime.Serialization.SerializationInfo _Info, System.Runtime.Serialization.StreamingContext _StreamingContext): 
//				base(_Info, _StreamingContext)
//			{
//			}
//
//		}

		public class InvalidFormatException : FormatException
		{

			public InvalidFormatException(string parameter): base("Invalid Format Exception in MControl.BindControl , " + parameter + " is not the correct format.")
			{
			
			}

			public InvalidFormatException(string parameter, Exception _InnerException):	base("Invalid Format Exception occurred in MControl.BindControl , " + parameter + " is not the correct format.", _InnerException)
			{
		
			}

			//Console.WriteLine("An exception occurred while parsing your response: {0}", e.ToString());
		}

	}

