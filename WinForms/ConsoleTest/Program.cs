using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace ConsoleTest
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {

            System.Diagnostics.Process p=new System.Diagnostics.Process();
            p.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.UseShellExecute = true;
            p.StartInfo.FileName = @"D:\MControl\Bin_2.4.0\WinForms\ConsoleTest\bin\Release\cmd.exe";
           
            p.Start();
            Console.ReadLine();
        }
    }
}
