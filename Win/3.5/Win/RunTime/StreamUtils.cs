using System;
using System.Reflection;
using System.IO;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace MControl.Runtime
{
	/// <summary>
	/// Summary description for Serialization.
	/// </summary>
	public class MemoryStreamUtils
	{
	
		private Hashtable hash;

		public MemoryStreamUtils(Hashtable hashTabl)
		{
			hash=hashTabl;
		}

		public void Save(string filename)
		{

			//MControl.Utility.Security.SymmetricEncryption sec = new MControl.Utility.Security.SymmetricEncryption(MControl.Utility.Security.SymmetricEncryption.WinControlsSymmetricSecretKey);
			FileStream fs = new FileStream(filename, FileMode.Create);
			MemoryStream m = new MemoryStream();

			SaveToMemoryStream(m);
			//MemoryStream m2 = sec.EncryptStream(m);
			
			//fs.Write(m2.GetBuffer(), 0, m2.GetBuffer().Length);
			fs.Write(m.GetBuffer(), 0, m.GetBuffer().Length);
			fs.Flush();
			fs.Close();
			m.Close();
			//m2.Close();
		}
		
		public void Load(string filename)
		{
			FileStream fs = new FileStream(filename, FileMode.Open);
			Byte[] b = new Byte[fs.Length];
			try
			{
				fs.Read(b, 0, (int)fs.Length);
				MemoryStream m = new MemoryStream(b);
				//MControl.Utility.Security.SymmetricEncryption sec = new MControl.Utility.Security.SymmetricEncryption(MControl.Utility.Security.SymmetricEncryption.WinControlsSymmetricSecretKey);
				//LoadFromMemoryStream(sec.DecryptStream(m));
				LoadFromMemoryStream(m);
				m.Close();
			}
			finally
			{
				fs.Close();
			}
		}



		private void SaveToMemoryStream(MemoryStream m) 
		{
			// Create a hashtable of values that will eventually be serialized.
			AssemblyName assemblyname	= Assembly.GetExecutingAssembly().GetName();
			string version				= string.Format("{0}.{1}.{2}", assemblyname.Version.Major.ToString(), assemblyname.Version.Minor.ToString(), assemblyname.Version.Build.ToString());
			//Hashtable hash			 	= new Hashtable();


			//Construct a BinaryFormatter and use it to serialize the data to the stream.
			BinaryFormatter formatter = new BinaryFormatter();
			try 
			{
				formatter.Serialize(m, hash);
			}
			catch (SerializationException e) 
			{
				throw new Exception(e.Message);
			}
		}

		private void LoadFromMemoryStream(MemoryStream m) 
		{
			Hashtable hash  = null;
			try 
			{
				BinaryFormatter formatter = new BinaryFormatter();
				hash = (Hashtable) formatter.Deserialize(m);
			}
			catch (SerializationException e) 
			{
				throw new Exception("Failed to deserialze: " + e.Message);
			}

			string	thisproduct	= Assembly.GetExecutingAssembly().GetName().FullName;
			Version thisversion	= Assembly.GetExecutingAssembly().GetName().Version;
			string	fileproduct	= hash["Product"].ToString();
			string	fileversion	= hash["Version"].ToString(); 

			if (thisproduct != fileproduct)
			{
				throw new Exception("Incorrect product.");
			}

			//Check for current versions.
			string[] asVersion = fileversion.Split(new char[] {'.'});
			if (asVersion.Length != 3)
			{
				//Something is wrong with the version. Doesn't seem to have 3 parts.
				throw new Exception("Incorrect version format.");
			}

			Version version = new Version(int.Parse(asVersion[0]), int.Parse(asVersion[1]), int.Parse(asVersion[2]));
			if (thisversion.Major == version.Major && thisversion.Minor == version.Minor && thisversion.Build == version.Build)
			{

				// Current version. From hash to variables...
				//this.m_DisabledTextColor = Color.FromArgb((int)hash["DisabledTextColor"]);

				//PlansOfColors = PlansColors.Custom;
			}
			else
			{
				// Code here to support other formats.
			}
		}

	}
}
