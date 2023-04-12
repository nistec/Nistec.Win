using System;
using System.Windows.Forms;

namespace ScriptLibrary
{		
	public class Script
	{
		public static object Res=null;
		
		static public object Main(string[] args)
		{			
			//MessageBox.Show("testing!");
			
			for (int i = 0; i < args.Length; i++)
			{ 
				Console.WriteLine(args[i]);
			}

			Res="testing ok!";
	
			return "testing ok!";
		}

		public static object GetVal()
		{			
			return "ok";
		}

	}
	
}