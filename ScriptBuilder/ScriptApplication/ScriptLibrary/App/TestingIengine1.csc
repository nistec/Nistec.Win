using System;
using System.Windows.Forms;
//css_reference C:\PCM_1.0\References\BIN\Debug\PCM.IEngine.dll;

namespace ScriptLibrary
{		
	public class Script
	{
		//public static object Res2=null;
		[STAThread]
		public static void Main(string[] args)
		{
			int opId=PCM.IEngine.AppMethods.GetOperatorID("972545233349");			
			MessageBox.Show(opId.ToString());
			
			for (int i = 0; i < args.Length; i++)
			{ 
				Console.WriteLine(args[i]);
			}

			object Res="testing ok!";
	
			//return "testing ok!";
		}

		public static object GetVal()
		{			
			return "ok";
		}

	}
	
}