// *****************************************************************************
// 
//  MControl
//  All rights reserved. The software and associated documentation 
//  supplied here under are the proprietary information of MControl Consulting 
//	Limited supplied subject to 
//	licence terms.
// 
//  MControl.Framework Version 1.2
// *****************************************************************************

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;   
using System.Globalization;
using System.Data;

namespace MControl.Win
{

	#region CommonDialog

	public sealed class CommonDlg
	{

		#region SaveAs
		public static string SaveAs(string filter)
		{
			return SaveAs(filter,"");
		}

		public static string SaveAs(string filter,string initialDirectory)
		{
			string Text="";
			SaveFileDialog  FileDlg =new SaveFileDialog ();
			//FileDlg.CheckFileExists=true;
			FileDlg.CheckPathExists =true;
			FileDlg.OverwritePrompt =true;
			FileDlg.AddExtension  =true;
			if(filter.Length>0)
				FileDlg.Filter =filter;//"Save as file type";
			if(initialDirectory.Length>0)
				FileDlg.InitialDirectory=initialDirectory;
            
			//if(DefaultExt=="")
			//	FileDlg.DefaultExt  = "All files (*.txt)|*.txt|All files (*.*)|*.*" ;
			//else
			//	FileDlg.DefaultExt  = DefaultExt;// + " files (*." + DefaultExt + ")|*." + DefaultExt  ;
	
			FileDlg.FilterIndex = 2 ;
			FileDlg.RestoreDirectory = true ;
		
			//System.IO.Stream myStream;
			if(FileDlg.ShowDialog() == DialogResult.OK)
			{
				//if((myStream = FileDlg.OpenFile())!= null)
				//{
				Text=FileDlg.FileName; 
				// Insert code to read the stream here.
				//myStream.Close( );
				//}
			} 
			return Text;
		}
		#endregion

		#region FileDialog
	
		/// <summary>
		/// Return file path
		/// </summary>
		/// <param name="DefaultExt">"All files (*.txt)|*.txt|All files (*.*)|*.*"</param>
		/// <returns></returns>
		public static string FileDialog(string filter)
		{
			return FileDialog(filter,"");
		}

		/// <summary>
		/// Return file path
		/// </summary>
		/// <param name="DefaultExt">"All files (*.txt)|*.txt|All files (*.*)|*.*"</param>
		/// <returns></returns>
		public static string FileDialog(string filter,string initialDirectory)
		{
			string Text="";
			OpenFileDialog FileDlg = new OpenFileDialog();

			//FileDlg.Filter  = "All files (*.txt)|*.txt|All files (*.*)|*.*" ;
			if(filter.Length>0)
				FileDlg.Filter  = filter;// + " files (*." + DefaultExt + ")|*." + DefaultExt  ;
			if(initialDirectory.Length>0)
				FileDlg.InitialDirectory=initialDirectory;
	
			FileDlg.FilterIndex = 2 ;
			FileDlg.RestoreDirectory = true ;
			FileDlg.Multiselect =false;	
	
			//System.IO.Stream myStream;
			if(FileDlg.ShowDialog() == DialogResult.OK)
			{
				//if((myStream = FileDlg.OpenFile())!= null)
				//{
				Text=FileDlg.FileName; 
				// Insert code to read the stream here.
				//myStream.Close( );
				//}
			} 
			return Text;
		}
		#endregion
		
		#region FolderDialog

		public static string FolderDialog()
		{		
			string Text="";
			FolderBrowserDialog FolderDlg = new FolderBrowserDialog();
			
			// Set the help text description for the FolderBrowserDialog.
			FolderDlg.Description = 
				"Select the directory that you want to use as the default.";

			// Do not allow the user to create new files via the FolderBrowserDialog.
			FolderDlg.ShowNewFolderButton = false;

			// Default to the My Documents folder.
			//FolderDlg.RootFolder = Environment.SpecialFolder.Personal;
			if(FolderDlg.ShowDialog() == DialogResult.OK)
				Text=FolderDlg.SelectedPath; 
			return Text;
		
		}
		#endregion

		#region ColorDialog

		public static string ColorDialog(string ColorName)
		{		
			string Text="";
			ColorDialog cp = new ColorDialog();
			cp.AllowFullOpen = true ;
			cp.ShowHelp = true ;
			if(ColorName!="")
				cp.Color = System.Drawing.Color.FromName (ColorName); 

			if(cp.ShowDialog() == DialogResult.OK)
			{
				//Color = cp.Color;
				Text=cp.Color.Name ;
			}
		return Text;		
		}
		
		public static Color ColorDialog()
		{		
			Color Res=SystemColors.Control;
			ColorDialog cp = new ColorDialog();
			cp.AllowFullOpen = true ;
			cp.ShowHelp = true ;
	
			if(cp.ShowDialog() == DialogResult.OK)
				Res = cp.Color;
			return Res;		
		}

		#endregion

		#region FontDialog

		public static Font FontDialog()
		{		
			//Font Res ;
			FontDialog fp = new FontDialog();
			fp.AllowSimulations = true ;
			fp.AllowVectorFonts  = true ;
			fp.ShowHelp = true ;
	
			if(fp.ShowDialog() == DialogResult.OK)
				return fp.Font;
	        else
				return null;
		
				//Res =new Font ( fp.Font.Name ,(float)8.25 ,fp.Font.Style ) ;
				//this.Text=fp.Font.FontFamily.Name + " , " + fp.Font.Size.ToString () + " , " + fp.Font.Style.ToString ();
		}
		#endregion

	}
	#endregion
}
