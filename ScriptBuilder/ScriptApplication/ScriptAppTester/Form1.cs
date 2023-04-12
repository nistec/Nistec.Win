using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using System.Diagnostics;
using System.IO;
using System.Reflection;

using System.Text;
using System.Runtime.InteropServices;
using MControl.ScriptBuilder;

namespace ScriptAppTester
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox txtScriptFile;
		private System.Windows.Forms.Button btnScriptFile;
		private System.Windows.Forms.Button btnDllPath;
		private System.Windows.Forms.TextBox txtDllPath;
		private System.Windows.Forms.Button btnExec;
		private System.Windows.Forms.TextBox txtMsg;
		private System.Windows.Forms.ComboBox comOptions;
		private System.Windows.Forms.TextBox txtParam;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.TextBox txtDllName;
		private System.Windows.Forms.Label label1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.txtScriptFile = new System.Windows.Forms.TextBox();
			this.btnScriptFile = new System.Windows.Forms.Button();
			this.btnDllPath = new System.Windows.Forms.Button();
			this.txtDllPath = new System.Windows.Forms.TextBox();
			this.btnExec = new System.Windows.Forms.Button();
			this.txtMsg = new System.Windows.Forms.TextBox();
			this.comOptions = new System.Windows.Forms.ComboBox();
			this.txtParam = new System.Windows.Forms.TextBox();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.txtDllName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// txtScriptFile
			// 
			this.txtScriptFile.Location = new System.Drawing.Point(96, 8);
			this.txtScriptFile.Name = "txtScriptFile";
			this.txtScriptFile.Size = new System.Drawing.Size(256, 20);
			this.txtScriptFile.TabIndex = 0;
			this.txtScriptFile.Text = "";
			// 
			// btnScriptFile
			// 
			this.btnScriptFile.Location = new System.Drawing.Point(16, 8);
			this.btnScriptFile.Name = "btnScriptFile";
			this.btnScriptFile.Size = new System.Drawing.Size(72, 24);
			this.btnScriptFile.TabIndex = 1;
			this.btnScriptFile.Text = "ScripFile";
			this.btnScriptFile.Click += new System.EventHandler(this.btnScriptFile_Click);
			// 
			// btnDllPath
			// 
			this.btnDllPath.Location = new System.Drawing.Point(16, 40);
			this.btnDllPath.Name = "btnDllPath";
			this.btnDllPath.Size = new System.Drawing.Size(72, 24);
			this.btnDllPath.TabIndex = 3;
			this.btnDllPath.Text = "DllPath";
			this.btnDllPath.Click += new System.EventHandler(this.btnDllPath_Click);
			// 
			// txtDllPath
			// 
			this.txtDllPath.Location = new System.Drawing.Point(96, 40);
			this.txtDllPath.Name = "txtDllPath";
			this.txtDllPath.Size = new System.Drawing.Size(144, 20);
			this.txtDllPath.TabIndex = 2;
			this.txtDllPath.Text = "";
			// 
			// btnExec
			// 
			this.btnExec.Location = new System.Drawing.Point(16, 72);
			this.btnExec.Name = "btnExec";
			this.btnExec.Size = new System.Drawing.Size(72, 24);
			this.btnExec.TabIndex = 4;
			this.btnExec.Text = "Execute";
			this.btnExec.Click += new System.EventHandler(this.btnExec_Click);
			// 
			// txtMsg
			// 
			this.txtMsg.Location = new System.Drawing.Point(16, 136);
			this.txtMsg.Multiline = true;
			this.txtMsg.Name = "txtMsg";
			this.txtMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtMsg.Size = new System.Drawing.Size(336, 136);
			this.txtMsg.TabIndex = 6;
			this.txtMsg.Text = "";
			// 
			// comOptions
			// 
			this.comOptions.Location = new System.Drawing.Point(96, 72);
			this.comOptions.Name = "comOptions";
			this.comOptions.Size = new System.Drawing.Size(120, 21);
			this.comOptions.TabIndex = 7;
			// 
			// txtParam
			// 
			this.txtParam.Location = new System.Drawing.Point(16, 104);
			this.txtParam.Name = "txtParam";
			this.txtParam.Size = new System.Drawing.Size(336, 20);
			this.txtParam.TabIndex = 8;
			this.txtParam.Text = "";
			// 
			// txtDllName
			// 
			this.txtDllName.Location = new System.Drawing.Point(256, 40);
			this.txtDllName.Name = "txtDllName";
			this.txtDllName.Size = new System.Drawing.Size(96, 20);
			this.txtDllName.TabIndex = 9;
			this.txtDllName.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(240, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(8, 16);
			this.label1.TabIndex = 10;
			this.label1.Text = "/";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(368, 285);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtDllName);
			this.Controls.Add(this.txtParam);
			this.Controls.Add(this.comOptions);
			this.Controls.Add(this.txtMsg);
			this.Controls.Add(this.btnExec);
			this.Controls.Add(this.btnDllPath);
			this.Controls.Add(this.txtDllPath);
			this.Controls.Add(this.btnScriptFile);
			this.Controls.Add(this.txtScriptFile);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad (e);
			this.comOptions.Items.AddRange(Enum.GetNames(typeof(ScriptCompileOption)));
		}

		private void btnScriptFile_Click(object sender, System.EventArgs e)
		{
			openFileDialog1.ShowDialog();
			this.txtScriptFile.Text=openFileDialog1.FileName;
		}

		private void btnDllPath_Click(object sender, System.EventArgs e)
		{
			folderBrowserDialog1.ShowDialog();
			this.txtDllPath.Text=folderBrowserDialog1.SelectedPath;

		}

		private void btnExec_Click(object sender, System.EventArgs e)
		{
			if(this.comOptions.SelectedIndex==-1)return;
		    Exec();
		}

		public string GetCompileOption(ScriptCompileOption op)
		{
			string[] options=new string[]{"/e","/ew","/c","/ca","/cd","/dbg","/s"};
			return  options[(int)op];
		}

		private bool ErrorCompile=false;

		public void Exec()
		{

			string[] arg=null; 
			object res=null;
			string msg="";
			ErrorCompile=false;

			this.txtMsg.Text="";
			ExecScript exec =new ExecScript();

			ScriptCompileOption mode=(ScriptCompileOption)this.comOptions.SelectedIndex;

			if(mode==ScriptCompileOption.Compiled)
			{
				arg=new string [2];
				arg[0]=this.txtScriptFile.Text;
				arg[1]=this.txtDllPath.Text + "\\" + this.txtDllName.Text;
				msg=exec.Compile(arg[0],arg[1],true);
			}
			else if(mode==ScriptCompileOption.Console)
			{
				arg=new string [2];
				arg[0]=GetCompileOption(ScriptCompileOption.Console);
				arg[1]=this.txtScriptFile.Text;
				res=exec.Execute(arg,new PrintDelegate(Print));
				msg = GetResult(res);
			}			
			else if(mode==ScriptCompileOption.Csc)
			{
				arg=new string [3];
				arg[0]=GetCompileOption(ScriptCompileOption.Dll);
				arg[1]=this.txtScriptFile.Text;
				res=exec.Execute(arg,new PrintDelegate(Print));
				msg = GetResult(res);

			}
			else if(mode==ScriptCompileOption.Dll)
			{
				arg=new string [3];
				arg[0]=GetCompileOption(ScriptCompileOption.Dll);
				arg[1]=this.txtScriptFile.Text;
				res=exec.Execute(arg,new PrintDelegate(Print));
				msg = GetResult(res);
			}
			else if(mode==ScriptCompileOption.WinExe)
			{
				arg=new string [3];
				arg[0]=GetCompileOption(ScriptCompileOption.Console);
				arg[1]=this.txtScriptFile.Text;
				res=exec.Execute(arg,new PrintDelegate(Print));
				msg = GetResult(res);

			}

			if(!string.IsNullOrEmpty(msg))
				this.txtMsg.AppendText(msg);
			//MessageBox.Show(msg);
		}

		private string GetResult(object res)
		{
			string s=this.txtScriptFile.Text.Replace("csc","dll");
			
			if(res==null && !ErrorCompile)
				return "Execute Successed Save In " + s;
			else
				return (string)res;
		}

		private void Print(string msg)
		{
			this.txtMsg.AppendText(msg);
			ErrorCompile=true;
		}


	}
}
