using System;
using System.Windows.Forms;
//css_reference F:\ScriptApplication\DllSample\bin\Debug\DllSample.dll;

namespace ScriptLibrary
{		
	public class Script
	{
		[STAThread]
		public static void Main(string[] args)
		{
			DllSample.Class1.Test();
			//MessageBox.Show("ok");
			
			//object Res="testing ok!";
	
		}

	}
	
}