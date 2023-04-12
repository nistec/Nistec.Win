using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Globalization;
using System.Collections;
using Microsoft.Win32;


namespace MControl.Util.Net
{

	internal struct pckage
	{
        public string v_30;//subKey
		public string n;
		public string k;
	}

	/// <summary>
	/// Summary description for EDF.
	/// </summary>
	internal class ED
	{
		internal ED()
		{
		}
        //SetLMValue
		internal static void ed_1(string v_30/*subKey*/,string v_58,object v_59)
		{
			RegistryKey rk=Registry.LocalMachine.OpenSubKey(v_30,true);
			if(rk!=null)
			{
				rk.SetValue(v_58, v_59);
			}
		}
        //GetLMValue
		internal static object ed_2(string v_30,string v_58)
		{
			RegistryKey rk=Registry.LocalMachine.OpenSubKey(v_30,true);
			object v_44=null;
			if(rk!=null)
			{
				v_44=rk.GetValue(v_58);
			}
			return v_44;
		}
        //RunRegistry
		internal static int ed_3(string v_60)
		{
			
			ED ed=new  ED();
			try
			{
				string s="";
				int v_44=0;
				s=ed.ed_5(v_60);
				v_44=ed.ed_10(s);
				return v_44;
			}
			catch(Exception)// ex)
			{
				System.Windows.Forms.MessageBox.Show("license key failed"); 
				return 0;
			}
		}
        //RunRegistryFromKey
        internal static int ed_4(string v_34)
        {

            ED ed = new ED();
            try
            {
                int v_44 = 0;
                v_44 = ed.ed_10(v_34);
                return v_44;
            }
            catch (Exception)// ex)
            {
                //System.Windows.Forms.MessageBox.Show("license key failed"); 
                return 0;
            }
        }

		#region Read

		//textBox2.Text = ed_5(@"D:\MControl\Data.bin");
        //ReadFile
		internal string ed_5(string v_60) 
		{
			StringBuilder output = new StringBuilder();

			FileStream fs = new FileStream(v_60, FileMode.OpenOrCreate);
			BinaryReader r = new BinaryReader(fs);

			r.BaseStream.Seek(0,SeekOrigin.Begin);    

			output.Append(r.ReadString() );

			fs.Close();
			return ed_8(output.ToString(),true);
		}
        //BytesFromString
		private static byte[] ed_6(string stringValue)
		{
			return (new UnicodeEncoding()).GetBytes(stringValue);
		}
        //BytesToString
		private static string ed_7(byte[] v_61)
		{
			return (new UnicodeEncoding()).GetString(v_61);
		}

        //decryptString
		internal string ed_8(string s, bool base64)
		{
			try
			{
				byte[] v_61;
				if (base64)
				{
					v_61 = Convert.FromBase64String(s);
				}
				else
				{
					v_61 = ed_6(s);
				}

				MemoryStream msEnc = new MemoryStream(v_61);
				MemoryStream msPlain = ed_9(msEnc);
				return ed_7(msPlain.GetBuffer());
			}
			catch (Exception)
			{
				//Debug.WriteLine(ex.ToString());
				return String.Empty;
			}
		}
        //DecryptStream
		private MemoryStream ed_9(MemoryStream ens)
		{
			try
			{
				RijndaelManaged oCSP = new RijndaelManaged();
				byte[] Key = {0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16};
				byte[] IV = {0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16};

				oCSP.Key = Key;
				oCSP.IV = IV;					
                ICryptoTransform ct = oCSP.CreateDecryptor();
                CryptoStream cs = new CryptoStream(ens, ct, CryptoStreamMode.Read);
                byte[] v_61 = new byte[ens.Length];
                int iBytesIn = cs.Read(v_61, 0, (int)ens.Length);
				cs.Close();
				MemoryStream plainStream = new MemoryStream();
				plainStream.Write(v_61, 0, iBytesIn);
				return plainStream;
			}
			catch (Exception)
			{
				//Debug.WriteLine(ex.ToString());
				return null;
			}
		}

		#endregion

		#region Registry

		//private const string v_47= "Software\\MControl\\" ;
        //private const string v_48=@"Licenses\7B8C320E-5562-45C4-B6A1-47290195D877\";//A2D75451-F655-41DD-A919-0C59D7809F70\";
        //private const string v_4="|";
        ////private const int v_5=7;
        //private const string v_7=" ";

        //RegisterKey
		internal int ed_10(string output)
		{
			ArrayList list =new   ArrayList();

			string[] v_21=output.Split(' ');
			if(v_21.Length % 3 !=0)
			{
				throw new Exception("Not a valid license key");
			}
			int i=0;
			while(i<v_21.Length)
			{
				pckage p=new pckage();
				p.v_30=v_21[i];
				p.n=v_21[i+1];
				p.k=v_21[i+2];
				list.Add(p);
				i+=3;
			}

			for(i=0;i<list.Count;i++)
			{
				pckage p=(pckage)list[i];
				ed_11(p.v_30,null,p.n);
				ed_11(p.v_30,"key",p.k);
			}
			return i;
		}

        //SetValue
		private void ed_11(string v_30,string v_58,object v_59)
		{
			Microsoft.Win32.RegistryKey rk=Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(p_1.v_48 +v_30,true);
			if(rk==null)
			{
                rk = Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(p_1.v_48 + v_30);
			}
			if(rk!=null)
			{
				rk.SetValue(v_58, v_59);
			}
		}
	
		#endregion

	}

}
