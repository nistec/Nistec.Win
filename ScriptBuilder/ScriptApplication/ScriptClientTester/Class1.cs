using System;
using System.IO;
using System.Reflection;

namespace PCM.Engine.AppEngine.Client
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Class1
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>

		private const int NoOrErrorRetValue = -1;	
		
		//[STAThread]
		public  object Run(string[] args)
		{
			int retValue = NoOrErrorRetValue;
			//Timer.timer Tm = new Timer.timer();
			object obj=null;

			if (args.Length == 0)
			{
				Console.WriteLine("Error: Assembly file not found \r\n");
				Console.ReadLine();
				return NoOrErrorRetValue;
			}
			else
			{
				string fileName = args[0];

				if (fileName.Length > 0 && File.Exists(fileName))
				{

					//Load the Assembly
					Assembly assembly = Assembly.LoadFile(fileName); 

					//Tm.Start(); 
					//Run the Assembly
					obj = CreateObject(assembly, "Script","GetVal");
					//Tm.Stop();
 
				}
				else
				{
					Console.WriteLine("Error:  Assembly file does not exist\r\n");
					Console.ReadLine();
				
				}
				//Console.WriteLine("TimeElapsed: " + Tm.ToString());
				//Console.ReadLine();
				
				return obj;//NoOrErrorRetValue;
			}

		}
	
		private object CreateObject(Assembly assembly , string objName,string methodName)
		{
			Type[] Types = assembly.GetTypes();
			System.Reflection.Module[] modules = assembly.GetModules();
			MethodInfo main = assembly.EntryPoint;
			System.Reflection.Module modul = assembly.GetModule("Script.Main");
		
			string str="";
//			foreach (Type oType in Types)
//			{
//	
//			   str=oType.Name.ToString();
//			
//			}

	
			int number = 10;
			object[] Params =null;//= new object[] {number,"testing"};

			// Display all the types contained in the specified assembly.
			foreach (Type oType in Types)
			{
				if(oType.Name.ToString()== "Script")
				{
					MethodInfo mi = oType.GetMethod("GetVal");//, BindingFlags.Public | BindingFlags.Instance, null, 
						//new Type[] {typeof(int),typeof(string) }, null);

					if (mi != null)
					{
						object obj = assembly.CreateInstance(oType.Name); 
						//int ret = (int)mi.Invoke(obj,Params); 
						object resv= mi.Invoke(obj,Params); 
						return resv;
					}
				}

			}
			return null;
		}
	}

}


