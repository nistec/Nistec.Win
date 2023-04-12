using System;
using System.IO;
using System.Reflection;

namespace Nistec.ScriptBuilder
{


	/// <summary>
	/// Summary description for ExecDll.
	/// </summary>
	public class ExecDll
	{

		public static object RunAssembly(string fileName)
		{
			return RunAssembly(fileName, AppInfo.ClassName,AppInfo.MainMethod,null);
		}

		public static object RunAssembly(string fileName,object[] Params)
		{
			return RunAssembly(fileName, AppInfo.ClassName,AppInfo.MainMethod,Params);
		}

		public static object RunAssembly(string fileName, string ClassName,object[] Params)
		{
	          return RunAssembly(fileName, ClassName,AppInfo.MainMethod,Params);
		}

        public static object RunAssembly(string fileName, string ClassName, string methodName, object[] Params)
        {

            object res = null;

            if (!File.Exists(fileName))
            {
                throw new Exception("Error:  Assembly file not exist");
            }

            try
            {
                //Load the Assembly
                Assembly assembly = Assembly.LoadFile(fileName);

                // Get a Type object corresponding to assembly.
                Type oType = assembly.GetType(ClassName);

                if (oType == null)
                {
                    throw new Exception("Error:  Load the Assembly Class Name {" + ClassName + "} Failed");
                }

                Type[] typeArray = null;

                MethodInfo mi = null;

                if (Params != null)
                {
                    int prmCount = Params.Length;

                    // Create a Parameters Type array.
                    typeArray = new Type[prmCount];
                    for (int i = 0; i < prmCount; i++)
                    {
                        typeArray.SetValue(typeof(object), i);
                    }
                    mi = oType.GetMethod(methodName, typeArray);

                }
                else
                {
                    Params = new object[] { null };
                    mi = oType.GetMethod(methodName);
                }

                if (mi == null)
                {
                    throw new Exception("Error:  Assembly Main Method {" + methodName + "} not found");
                }

                object obj = assembly.CreateInstance(oType.Name);
                res = mi.Invoke(obj, Params);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static object RunWithArgs(string fileName, string[] Args)
        {
            return RunWithArgs(fileName, AppInfo.ClassName, AppInfo.MainMethod, Args);
        }

        public static object RunWithArgs(string fileName, string ClassName, string[] Args)
        {
            return RunWithArgs(fileName, ClassName, AppInfo.MainMethod, Args);
        }

        public static object RunWithArgs(string fileName, string ClassName, string methodName, string[] Args)
        {

            object res = null;

            if (!File.Exists(fileName))
            {
                throw new Exception("Error:  Assembly file not exist");
            }

            try
            {
                //Load the Assembly
                Assembly assembly = Assembly.LoadFile(fileName);

                // Get a Type object corresponding to assembly.
                Type oType = assembly.GetType(ClassName);

                if (oType == null)
                {
                    throw new Exception("Error:  Load the Assembly Class Name {" + ClassName + "} Failed");
                }

                MethodInfo mi = null;
                object[] Params = null;

                if (Args != null)
                {
                    mi = oType.GetMethod(methodName);
                    Params = new object[] { Args };
                }
                else
                {
                    Params = new object[] { null };
                    mi = oType.GetMethod(methodName);
                }
                if (mi == null)
                {
                    throw new Exception("Error:  Assembly Main Method {" + methodName + "} not found");
                }

                object obj = assembly.CreateInstance(oType.Name);
                res = mi.Invoke(obj, Params);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //internal object Run_Assembly(string fileName, string ClassName,string methodName,object[] Params)
        //{
        //    //Not used
        //    object res=null;

        //    if (! File.Exists(fileName))
        //    {
        //        throw new ApplicationException ("Error:  Assembly file not exist");
        //    }
		
        //    //Load the Assembly
        //    Assembly assembly = Assembly.LoadFile(fileName); 
    
        //    Type[] Types = assembly.GetTypes();
	
        //    // Display all the types contained in the specified assembly.
        //    foreach (Type oType in Types)
        //    {
			    
        //        if(oType.Name.ToString()== ClassName)
        //        {
	
        //            Type[] typeArray =null;
				
        //            if(Params!=null)
        //            {
        //                int prmCount=Params.Length;

        //                // Create a Type array.
        //                typeArray =new Type[prmCount];
        //                for(int i=0;i<prmCount;i++)
        //                {
        //                    typeArray.SetValue(typeof(object),i);							
        //                }
        //            }

					
        //            MethodInfo mi = oType.GetMethod(AppInfo.MainMethod,typeArray);

        //            //					MethodInfo mi = oType.GetMethod(methodName, 
        //            //						BindingFlags.Public | BindingFlags.Instance, null, 
        //            //						typeArray, null);

        //            if (mi != null)
        //            {
        //                object obj = assembly.CreateInstance(oType.Name); 
        //                res= mi.Invoke(obj,Params); 
        //                return res;
        //            }
        //        }
 
        //    }
        //    return null;
        //}

	}

   }
