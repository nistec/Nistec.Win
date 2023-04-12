using System;
using System.Collections;
using System.Security.Permissions;
using System.Runtime.Remoting;


namespace MControl.Runtime
{
	[StrongNameIdentityPermission(SecurityAction.LinkDemand,
	PublicKey="0024000004800000940000000602000000240000525341310004000001000100870642fef850a2320db38a236b46449b56ab3ac4afa9215e2a24ae0a243c602b5349ceebc135c4f2feb4b2a68879b102ed3cdf946ebbd8e4f3f847bfbb38dc58f1818b7cac92f344e2480bc5007095577081d75fd8b89c864af12d582f041d6022c08b8128a53e8b3a39a84dc8b7adea628675403d8d64528c8c45dea7931ac9")]
	class RemotingHelpers
	{
		private static bool _isInit;
		private static IDictionary _wellKnownTypes=new Hashtable();

		public static Object GetObject(Type type) 
		{
			if (! _isInit) InitTypeCache();
			WellKnownClientTypeEntry entr = (WellKnownClientTypeEntry) _wellKnownTypes[type];

			if (entr == null) 
				throw new RemotingException(RM.GetString(RM.MistakeConfigRemoting));

			return Activator.GetObject(entr.ObjectType,entr.ObjectUrl);
		}

		public static void InitTypeCache() 
		{
			_isInit = true;
			_wellKnownTypes.Clear();

			foreach (WellKnownClientTypeEntry entr in 
				RemotingConfiguration.GetRegisteredWellKnownClientTypes()) 
			{
				if (entr.ObjectType == null) 
				{
					throw new RemotingException(RM.GetString(RM.MistakeConfigRemoting));
				}
				_wellKnownTypes.Add (entr.ObjectType,entr);
			}
		}
	}
}