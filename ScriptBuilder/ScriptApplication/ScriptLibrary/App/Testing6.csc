using System;
using System.Windows.Forms;


namespace ScriptLibrary
{		
	public class Script
	{
		//public static object Res2=null;
		
		[STAThread]
		public static void1 Main()
		{			
			MessageBox.Show("testing!");
			
			//for (int i = 0; i < args.Length; i++)
			//{ 
			//	Console.WriteLine(args[i]);
			//}

			object Res="testing ok!";
			//MessageBox.Show(Res.ToString());	
			//return "testing ok!";
		}

		public static object GetVal()
		{			
			return "ok";
		}

	}
	
}