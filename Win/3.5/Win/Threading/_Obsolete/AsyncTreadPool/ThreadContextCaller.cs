using System;
using System.Threading;
using System.Globalization;
using System.Security.Principal;
using System.Reflection;
using System.Runtime.Remoting.Contexts;

namespace MControl.Threading
{
	#region ThreadContextCaller class

	/// <summary>
	/// This class stores the caller thread context in order to restore
	/// it when the work item is executed in the context of the thread 
	/// from the pool.
	/// Note that we can't store the thread's CompressedStack, because 
	/// it throws a security exception
	/// </summary>
	internal class ThreadContextCaller
	{
		private CultureInfo _culture = null;
		private CultureInfo _cultureUI = null;
		private IPrincipal _principal;
		private System.Runtime.Remoting.Contexts.Context _context;

		private static FieldInfo _fieldInfo = GetFieldInfo();

		private static FieldInfo GetFieldInfo()
		{
			Type threadType = typeof(Thread);
			return threadType.GetField(
				"m_Context",
				BindingFlags.Instance | BindingFlags.NonPublic);
		}

		/// <summary>
		/// Constructor
		/// </summary>
		private ThreadContextCaller()
		{
		}

		/// <summary>
		/// Captures the current thread context
		/// </summary>
		/// <returns></returns>
		public static ThreadContextCaller Capture()
		{
			ThreadContextCaller threadContextCaller = new ThreadContextCaller();

			Thread thread = Thread.CurrentThread;
			threadContextCaller._culture = thread.CurrentCulture;
			threadContextCaller._cultureUI = thread.CurrentUICulture;
			threadContextCaller._principal = Thread.CurrentPrincipal;
			threadContextCaller._context = Thread.CurrentContext;
			return threadContextCaller;
		}

		/// <summary>
		/// Applies the thread context stored earlier
		/// </summary>
		/// <param name="threadContextCaller"></param>
		public static void Apply(ThreadContextCaller threadContextCaller)
		{
			Thread thread = Thread.CurrentThread;
			thread.CurrentCulture = threadContextCaller._culture;
			thread.CurrentUICulture = threadContextCaller._cultureUI;
			Thread.CurrentPrincipal = threadContextCaller._principal;

			// Uncomment the following block to enable the Thread.CurrentThread
/*
			if (null != _fieldInfo)
			{
				_fieldInfo.SetValue(
					Thread.CurrentThread, 
					threadContextCaller._context);
			}
*/			
		}
	}

	#endregion
}
