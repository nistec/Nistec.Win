using System;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Text;

using System.Windows.Forms;
using Microsoft.Win32;
using System.Reflection;

namespace MControl.Util.Net
{

    /// <summary>
    /// 
    /// </summary>
    public sealed class nf_1
    {

        #region Members

        private string v_21; //product name
        private string v_22;//product version
        private string v_23; //product version without poins
        private string v_46; //the ch8 encoding key

        private string v_62;//licence key
        private string v_63;//date install key

        private const int v_45 = 8;
        private const string v_34 = "GTH54VSK";
        private const int clint = 240;
        private const int clintln = 37;

        private string v_9;//m_Key
        //CTL/WEB/APP/CHK/CLT/SRV/DSN//
        private string v_10 = "CTL";//ChkMode
        //private string m_method="";
        private string mckfile;
        #endregion

        #region Property

        //return ch8 key
        private string gch()
        {
            return v_46;
        }

        #endregion

        #region Main


        //internal constructor from net logo form
        //NetFramReg
        internal nf_1(string v_11, string v_12)//productName,vers
            : this()
        {
            v_21 = v_11;
            v_22 = v_12;
            v_23 = v_12.Replace(".", "");

        }

        //main constructor
        //NetFramReg
        internal nf_1()
        {
        }

        //NetReflected
        public static bool nf_2(MethodBase v_13, string v_14)//method,pk
        {

            try
            {
                // this is done because this v_13 can be called explicitly from code.
                //MethodBase v_13 = (MethodBase) (new System.Diagnostics.StackTrace().GetFrame(1).GetMethod());
                byte[] pk2 = v_13.ReflectedType.Assembly.GetName().GetPublicKeyToken();
                bool v_44 = nf_3(pk2, v_14);

                if (v_44)
                {
                    return true;
                }
                System.Type mt = typeof(MControl.Util.Net.nf_1);
                byte[] pk1 = mt.Assembly.GetName().GetPublicKeyToken();

                return ed_7(pk1).Equals(ed_7(pk2));

            }
            catch
            {
                return false;
            }
        }

        //IsParty
        private static bool nf_3(byte[] v_14, string v_15)//name)
        {
            if (v_15.Equals("0847e55d262dc74f"))//MControl.EnterpriseDB"
            {
                return ed_7(v_14).Equals(ed_7(new byte[] { 8, 71, 229, 93, 38, 45, 199, 79 })); 
            }
            if (v_15.Equals("67c368c91e805727"))//MControl.Business"
            {
                return ed_7(v_14).Equals(ed_7(new byte[] { 103, 195, 104, 201, 30, 128, 87, 39 }));
            }
            if (v_15.Equals("b03f5f7f11d50a3a"))//MControl.Profile"
            {
                return ed_7(v_14).Equals(ed_7(new byte[] { 186, 127, 163, 143, 11, 103, 28, 188 }));
            }
            return false;
        }
        //GetNetAssembly
        public static Assembly nf_4()
        {
            return Assembly.GetAssembly(typeof(MControl.Util.Net.nf_1));
        }

        public static string ed_7(byte[] v_61)
        {
            return (new System.Text.UnicodeEncoding()).GetString(v_61);
        }

        //open registry form
        //NetLogoOpen
        public static void nf_5(string v_16/*productNum*/, string v_11, string v_12, string v_32, bool showDetails, string v_13, string v_17)
        {
            NetLogo.Open(v_16, v_11, v_12, v_32, showDetails, v_13, v_17);
        }

        public static bool nf_5(string v_16, string v_11, string v_12, string v_13, string v_17)
        {
            NetLogo.Open(v_16, v_11, v_12, "License key requested", false, v_13, v_17);
            return false;
        }

        //check license from registry
        //checkNetFram
        public static bool nf_6(string v_11, string v_12, string v_13, string v_17/*mode*/, MethodBase v_18/*methodBase*/)
        {
            nf_1 nf = new nf_1();
            nf.v_10 = v_17;
            if (v_17.Equals("CLT"))
            {
                //MethodBase v_18 = (MethodBase)(new System.Diagnostics.StackTrace().GetFrame(1).GetMethod());
                return nf_2(v_18, v_13);
            }
            return nf.nf_7(v_11, v_12, v_13);
        }


        //check license from registry
        //checkNetFram
        public static bool nf_6(string v_11, string v_12, string v_13, string v_17)
        {
            nf_1 nf = new nf_1();
            nf.v_10 = v_17;
            if (v_17.Equals("CLT"))
            {
                MethodBase v_18 = (MethodBase) (new System.Diagnostics.StackTrace().GetFrame(1).GetMethod());
                return nf_2(v_18, v_13);
            }
            return nf.nf_7(v_11, v_12, v_13);
        }

        //check license from registry and return true if licence ok
        //checkV
        private bool nf_7(string v_11, string v_12, string v_13)
        {

            v_21 = v_11;
            v_22 = v_12;
            v_23 = v_12.Replace(".", "");
            string v_16 = "0";
            string mtd=v_13;
            try
            {
                if(v_10.Equals("SERVER"))
                {
                    mckfile = v_13;
                    mtd = "mckFile";
                }
                v_8 lp = (v_8)Enum.Parse(typeof(v_8), v_11,true);
                v_16 = nf_16(lp);
                nf_20(v_16);
                string v_19 = v_62;//licenseKey
                string v_20 = p_1.v_3[(int)lp];//prdVers
                bool flag1 = false;
    
                if (this.v_10.Equals("SRV"))
                {
                    string regk = mtd;
                    if (regk.Equals(String.Empty))
                    {
                        return false;
                    }
                    if (regk != null)
                    {
                        string dn = nf_11(regk, (int)lp);
                        if (v_46 == "")
                        {
                            return false;
                        }
                        flag1 = double.Parse(dn) > nf_32(0);
                    }
                    return flag1;
                }
                if ((v_19 == null) || (v_19.Length == 0))
                {
                    //ThrowException.ThrowLicenseException(this.LicenseeType, null, "The license key was not set or is invalid. Please make sure that you are correctly licensing the product by setting the LicenseKey property as described in the documentation and then recompile in order to avoid this exception.");
                    //MControl.Framework.MsgBox.ShowError("The license key was not set or is invalid. Please make sure that you are correctly licensing the product by setting the LicenseKey property as described in the documentation and then recompile in order to avoid this exception.");
                    if (v_10 != "WEB")//!IsWeb)
                        NetLogo.Open(v_16, v_11, v_12, p_1.v_24, false, mtd, v_10);
                    return false;
                }
                if (v_22 != v_20)//v_52)
                {
                    //throw new mCtlLicenseManagerException("Incorrect version");
                    //MControl.Framework.MsgBox.ShowError("Incorrect version");
                    if (v_10 != "WEB")//!IsWeb)
                        NetLogo.Open(v_16, v_11, v_12, p_1.v_26, true, mtd, v_10);
                    return false;
                }

                this.nf_18(v_19);
                nf_12(lp);

                string v_44 = nf_34(v_19);
                string v_17 = v_44.Substring(v_44.Length - 1, 1);

                if (!v_17.Equals("A") && !v_17.Equals("T") && !v_17.Equals("F") && !v_17.Equals("C"))
                {
                    //throw new mCtlLicenseManagerException("v_28");
                    if (v_10 != "WEB")//!IsWeb)
                        NetLogo.Open(v_16, v_11, v_12, p_1.v_25, true, mtd, v_10);
                    return false;
                }

                string[] slt = v_44.Split('|');

                if (slt.Length < 4)
                {
                    return false;
                }
                int i = (int)lp;
                if (slt[0].Equals(lp.ToString()) && slt[1].Equals(p_1.v_2[i]) && slt[2].Equals(v_23) && slt[3].Equals(p_1.v_1[i] + v_17))
                {
                    flag1 = true;
                }
                else
                {
                    return false;
                }

                if (v_17.Equals("F"))
                {
                    return true;
                }

                string netins = nf_30(v_63);
                //netins=nf_30(netins,part1);
                //netins=nf_30(netins,part2);

                if (netins == "")
                {
                    flag1 = false;
                }
                else if (v_17.Equals("A"))
                {
                    flag1 = nf_33(netins, p_1.tl, v_16);//*12);
                }
                else if (v_17.Equals("T"))
                {
                    flag1 = nf_33(netins, p_1.tl, v_16);
                }
                else if (v_17.Equals("C"))//Client
                {
                    if (this.v_10.Equals("DSN"))
                        return false;
                    flag1 = nf_33(netins, p_1.tl, v_16);
                }
                else
                {
                    return false;
                }

                if (!flag1)
                {
                    if (v_10 != "WEB")//!IsWeb)
                        NetLogo.Open(v_16, v_11, v_12, p_1.v_24, true, mtd, v_10);
                }

                return flag1;

            }
            catch//(Exception ex)
            {
                //MessageBox.Show (ex.Message,"MControl",MessageBoxButtons.OK  ,MessageBoxIcon.Exclamation );
                //MControl.Framework.MsgBox.ShowError("The license key was not set or is invalid. Please make sure that you are correctly licensing the product by setting the LicenseKey property as described in the documentation and then recompile in order to avoid this exception.");
                if (v_10 != "WEB")//!IsWeb)
                    NetLogo.Open(v_16, v_11, v_12, p_1.v_27, true, mtd, v_10);
                return false;
            }

        }



        //check if license key from user and check if is valid
        //checkK
        private bool nf_8(string v_11, string v_12, string v_19, ref string v_32)
        {
            v_21 = v_11;//product=
            v_22 = v_12;//productVers=
            v_23 = v_12.Replace(".", "");//productReg=

            try
            {
                v_8 lp = (v_8)Enum.Parse(typeof(v_8), v_11);
                string v_16 = nf_16(lp);

                if ((v_19 == null) || (v_19.Length == 0))
                {
                    v_32 = p_1.v_24;
                    return false;
                }

                this.nf_18(v_19);
                nf_12(lp);

                string v_44 = nf_34(v_19);
                string v_17 = v_44.Substring(v_44.Length - 1, 1);

                string[] slt = v_44.Split('|');

                if (slt.Length < 4)
                {
                    return false;
                }
                int i = (int)lp;
                //bool flag1=false;
                if (slt[0].Equals(lp.ToString()) && slt[1].Equals(p_1.v_2[i]) && slt[2].Equals(v_23) && slt[3].Equals(p_1.v_1[i] + v_17))
                {
                    return true;
                }

                v_32 = p_1.v_25;
                return false;

            }
            catch//(Exception ex)
            {
                //MessageBox.Show (ex.Message,"MControl",MessageBoxButtons.OK  ,MessageBoxIcon.Exclamation );
                v_32 = p_1.v_24;
                return false;
            }

        }
        //chkmode
        private int nf_9(string v_17,string v_16)
        {
            int flag1 = 0;//0=invalid 1=trial 2=ok

            if (v_17.Equals("F"))
            {
                return 2;
            }
            else if (v_17.Equals("C"))//Client
            {
                flag1 = this.v_10.Equals("DSN") ? 0 : 2;
                return flag1;
            }

            string netins = nf_30(v_63);

            if (netins == "")
            {
                //flag1=0;
            }
            else if (v_17.Equals("A"))
            {
                bool v_44 = nf_33(netins, p_1.tl, v_16);
                flag1 = v_44 ? 2 : 0;
            }
            else if (v_17.Equals("T"))
            {
                bool v_44 = nf_33(netins, p_1.tl, v_16);
                flag1 = v_44 ? 1 : 0;
            }
            else
            {
                return 0;
            }
            return flag1;
        }

        //CalcGuid
        private int nf_10(string v_31)//guid
        {
            int sum = 0;
            char[] chs = v_31.ToCharArray();

            foreach (char c in chs)
            {
                if (char.IsLetter(c))
                {
                    sum += int.Parse(c.ToString(), System.Globalization.NumberStyles.HexNumber);
                }
                else if (char.IsDigit(c))
                {
                    sum += int.Parse(c.ToString());
                }
            }
            return sum;
        }
        //calcsrv
        private string nf_11(string k, int x)
        {
            string std = "";
            string kch = "";

            string s = p_1.v_2[x].Substring(0, 8);
            char[] chs = s.ToCharArray();
            foreach (char c in chs)
            {
                kch += char.IsDigit(c) ? c.ToString() : "0";
            }
            v_46 = kch;

            string kst = nf_34(k);

            string ty = kst.Substring(0, 3);

            if (p_1.v_1[x].Equals(ty))
            {
                std = nf_29(kst.Substring(3, kst.Length - 3));
            }

            return std;
        }
        #endregion

        #region product encode key

        //set product encode key
        //SetProductVers
        private void nf_12(v_8 lp)
        {

            string v_43 = nf_15(lp);
            int l = (int)lp;
            char c = v_23[v_23.Length - 1];
            string pv = c.ToString() + v_43 + l.ToString() + v_23 + v_34;

            char[] ch = pv.ToCharArray();
            string v_44 = "";
            byte[] bt = new byte[v_45];
            byte cnt = 0;
            v_46 = "";

            for (int i = 0; i < ch.Length && cnt < v_45; i++)
            {

                v_44 = nf_14(nf_17(ch[i], i).ToString());
                bt.SetValue(byte.Parse(v_44[v_44.Length - 1].ToString()), cnt);
                cnt++;
            }

            for (int k = 0; k < v_45; k++)
            {
                v_46 += bt[k].ToString();
            }

        }
        //MapNumberToChar
        private string nf_13(string s)
        {
            if ((s == null) || (s.Length == 0))
            {
                throw new Exception(p_1.v_27);//  mCtlLicenseManagerException();
            }

            uint num1 = uint.Parse(s);
            uint num2 = (uint)(1 << ((7 - ((int)num1 % 8)) & 0x1f));
            if (num2 == 0)
            {
                num1 = num1 % 8;
                return num1.ToString();
            }
            return num2.ToString();
        }
        //MapStringToNumber
        private string nf_14(string s)
        {
            if ((s == null) || (s.Length == 0))
            {
                throw new Exception(p_1.v_27);// mCtlLicenseManagerException();
            }
            uint num1 = uint.Parse(s);
            uint num2 = (uint)(1 << ((7 - ((int)num1 % 8)) & 0x1f));
            if (num2 == 0)
            {
                num1 = num1 % 8;
                return num1.ToString();
            }
            return num2.ToString();
        }
        //GetProductCodeName
        private string nf_15(v_8 lp)
        {
            int i = (int)lp;
            return p_1.v_1[(int)lp];
        }
        //GetProductName
        private string nf_16(v_8 lp)
        {
            int i = (int)lp;
            return p_1.v_2[(int)lp];
        }
        //GetAlphaNumValue
        private int nf_17(char v_33, int i)
        {
            int num1 = 0;
            for (num1 = 0; num1 < 0x20; num1 = (int)(num1 + 1))
            {
                if ("ABJCKTDL4UEMW71FNX52YGP98Z63HRS0"[num1] == v_33)
                {
                    return num1 + i;
                }
            }
            return (num1 + i) % v_45;
            //return num1 % v_45;
        }
        //CopyKeyNoDashes
        private void nf_18(string v_34)
        {
            this.v_9 = "";
            if (v_34 != null)
            {
                v_9 = v_34;//.Replace("-","");

            }
        }
        //Reset
        private void nf_19()
        {
            this.v_9 = "";
        }

        #endregion

        #region public Registry


        //Get registry key
        //gnetreg
        private void nf_20(string v_35)
        {
            v_62 = "";
            v_63 = "";

            try
            {
#if(SERVER)
                nf_35(v_35, v_23);
#else
                if (v_10 == "SERVER")
                {
                    nf_35(v_35, v_23);
                }
                else
                {
                    RegistryKey rk = Registry.ClassesRoot.OpenSubKey(p_1.v_48 + v_35);
                    if (rk != null)
                    {
                        v_62 = rk.GetValue("", "").ToString();
                        v_63 = rk.GetValue("key", "").ToString();
                    }
                }
#endif
            }
            catch
            {
                //return "";
            }
        }

  
        //gnetregclient
        private string nf_21(string v_35)
        {
            //v_62="";
            //v_63="";

            try
            {

                RegistryKey rk = Registry.ClassesRoot.OpenSubKey(p_1.v_48 + v_35);
                if (rk != null)
                {
                    return rk.GetValue("client", "").ToString();
                    //v_62= rk.GetValue ("client","").ToString ();
                    //v_63= rk.GetValue ("key","").ToString ();
                }
                return "";
            }
            catch
            {
                return "";
            }
        }

        //GetDalConnections
        public static string GetDalServer()
        {
            string file = null;
            RegistryKey rk = Registry.LocalMachine.OpenSubKey(@"Software\MControl\Net_2.4");
            if (rk != null)
            {
                object o= rk.GetValue("Location");
                if (o == null)
                {
                    throw new Exception("Invalid Location");
                }
                file = o.ToString() + @"\DalServer_240.xml";
                if (File.Exists(file))
                {
                    throw new Exception("File DalServer not exists");
                }

                return  file;

            }
            return null;
        }
        #endregion

        #region ClassesRoot Registry

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //Get Decoded key by product number
        //GetUnet
        private string nf_22(long n)
        {
            RegistryKey rk = Registry.ClassesRoot.OpenSubKey(p_1.v_48 + n.ToString());
            try
            {
                if (rk != null)
                {
                    string s = rk.GetValue("", null).ToString();
                    if (s != null)
                        s = nf_34(s);
                    return s;
                }
                else
                    return "";
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //check if License key exist
        //kuexist
        private bool nf_23(string k)
        {
            RegistryKey rk = Registry.ClassesRoot.OpenSubKey(k);
            return rk != null;
        }

        //Create ClassesRoot License
        //creatnetuk
        internal bool nf_24(string k, string v_35, string v_36, string v_12, ref string v_32)
        {
            try
            {
                if (nf_8(v_35, v_12, k, ref v_32))
                {
                    RegistryKey rk;
                    rk = Registry.ClassesRoot.CreateSubKey(p_1.v_48 + v_36);

                    if (rk != null)
                    {
                        //rk.SetValue ("netp",v_36);
                        rk.SetValue("", k);
                        rk.SetValue("key", nf_27());
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        internal void nf_241(string v_36)
        {
            try
            {
                RegistryKey rk;
                rk = Registry.ClassesRoot.CreateSubKey(p_1.v_48 + v_36);

                if (rk != null)
                {
                    rk.SetValue("key", nf_27());
                }
            }
            catch
            {
            }
        }

        #endregion

        #region LocalMachine Registry

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// //GetField
        private string nf_25(string v_35)
        {
            try
            {
                RegistryKey rk = Registry.LocalMachine.OpenSubKey(p_1.v_47 + v_35);
                if (rk != null)
                {
                    string s = rk.GetValue(v_35, null).ToString();
                    return s;
                }
                else
                    return "";
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// //netpexist
        private bool nf_26(string k)
        {
            RegistryKey rk = Registry.LocalMachine.OpenSubKey(k);
            return rk != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// //getdatek
        private string nf_27()
        {
            string s = DateTime.Today.Year.ToString("0000");
            return s.Substring(1, s.Length - 1) + "ASD" + DateTime.Today.Month.ToString("00") + "FGH" + DateTime.Today.Day.ToString("00");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="tp"></param>
        /// <param name="ver"></param>
        /// <returns></returns>
        //create registry key local machine
        //creatnetreg
        internal bool nf_28(string tp, string ver)
        {
            try
            {
                if (!this.nf_26(p_1.v_47 + tp))
                {
                    RegistryKey rk;
                    rk = Registry.LocalMachine.CreateSubKey(p_1.v_47 + tp);

                    if (rk != null)
                    {
                        rk.SetValue("", tp);
                        rk.SetValue("netv", ver);
                        rk.SetValue("Style", p_1.v_49);
                        //rk.SetValue ("ins",GetnetDate(0));
                        return true;
                    }
                }
                else
                    return true;

                return false;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region functions
        //oatdat
        private string nf_29(string d)
        {
            try
            {
                return DateTime.FromOADate(double.Parse(d)).ToOADate().ToString();
            }
            catch
            {
                return "";
            }
        }
        //GetDnUdat
        private string nf_30(string d)
        {
            try
            {
                return DateTime.FromOADate(double.Parse(nf_34(d))).ToString();
            }
            catch
            {
                return "";
            }
        }

        //		private string nf_30(string d,string r)
        //		{
        //			return d.Replace(r,"-");;
        //		}

        //		private string GetnetDate(double daysAdd)
        //		{
        //			DateTime dt=DateTime.Now.AddDays (daysAdd); 
        //			return dt.Year.ToString ()+dt.Month.ToString ()+dt.Day.ToString ()  ;
        //		}

        //		private DateTime GetnetkDate(string d)
        //		{
        //			try
        //			{
        //				return DateTime.Parse(d);//.FromOADate(double.Parse(d));//en2 (d)));
        //			}
        //			catch
        //			{
        //				return new DateTime(1900,12,31);//  DateTime.MaxValue;
        //			}
        //		}

        //Getnetin
        private double nf_31(string d, double v_37)
        {
            try
            {
                return DateTime.Parse(d).AddDays(v_37).ToOADate();
            }
            catch
            {
                return 0;
            }
        }
        //Getnetnw
        private double nf_32(double d)
        {
            return DateTime.Today.AddDays(d).ToOADate();
        }

        #endregion

        #region Validation


        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="v_38"></param>
        /// <returns></returns>
        /// //validnetdat
        private bool nf_33(string s, int v_38,string v_36)
        {
            try
            {

                if (nf_31(s, v_38) < nf_32(0))
                {
                    nf_241(v_36);
                    return false;
                }
                else
                    return true;
                //bool v_44=false;
                //				DateTime dt= GetnetkDate(s);
                //				dt=dt.AddDays (v_38);
                //				if(dt < DateTime.Today )
                //					return false;
                //				else
                //					return true; 
            }
            catch
            {
                return false;
            }
        }

 
        #endregion

        #region Encode Decode

        //private  string DeString(string s)
        //en2
        private string nf_34(string s)
        {
            string netc = gch().ToString();
            string tmp;
            using (MemoryStream v_39 = new MemoryStream(), v_40 = new MemoryStream())
            {
                byte[] v_41 = Convert.FromBase64String(s);
                v_39.Write(v_41, 0, v_41.Length);
                v_39.Flush();
                v_39.Seek(0, SeekOrigin.Begin);

                //en4(v_39, v_40, netc);

                DESCryptoServiceProvider DESProvider = new DESCryptoServiceProvider();
                DESProvider.Key = ASCIIEncoding.ASCII.GetBytes(netc);
                DESProvider.IV = ASCIIEncoding.ASCII.GetBytes(netc);
                ICryptoTransform desDecrypt = DESProvider.CreateDecryptor();

                using (CryptoStream cryptostreamDecr = new CryptoStream(v_40, desDecrypt, CryptoStreamMode.Write))
                {
                    byte[] v_42 = new byte[v_39.Length];
                    v_39.Read(v_42, 0, v_42.Length);
                    cryptostreamDecr.Write(v_42, 0, v_42.Length);
                    cryptostreamDecr.FlushFinalBlock();

                    v_40.Seek(0, SeekOrigin.Begin);


                    StreamReader outputStreamReader = new StreamReader(v_40);
                    tmp = outputStreamReader.ReadToEnd();

                    outputStreamReader.Close();

                    v_39.Close();
                }
            }

            return tmp;
        }


        //private void DeStream(Stream streamInput, Stream streamOutput, string netc)
        //en4
        private void nf_35(Stream var1, Stream var2, string netc)
        {
            DESCryptoServiceProvider DESProvider = new DESCryptoServiceProvider();
            DESProvider.Key = ASCIIEncoding.ASCII.GetBytes(netc);
            DESProvider.IV = ASCIIEncoding.ASCII.GetBytes(netc);
            ICryptoTransform desDecrypt = DESProvider.CreateDecryptor();

            using (CryptoStream cryptostreamDecr = new CryptoStream(var2, desDecrypt, CryptoStreamMode.Write))
            {
                byte[] v_42 = new byte[var1.Length];
                var1.Read(v_42, 0, v_42.Length);
                cryptostreamDecr.Write(v_42, 0, v_42.Length);
                cryptostreamDecr.FlushFinalBlock();
            }
            var2.Seek(0, SeekOrigin.Begin);
        }

        public static string dep(string s,string ch)
        {
            string tmp;
            using (MemoryStream v_39 = new MemoryStream(), v_40 = new MemoryStream())
            {
                byte[] v_41 = Convert.FromBase64String(s);
                v_39.Write(v_41, 0, v_41.Length);
                v_39.Flush();
                v_39.Seek(0, SeekOrigin.Begin);

                //en4(v_39, v_40, netc);

                DESCryptoServiceProvider DESProvider = new DESCryptoServiceProvider();
                DESProvider.Key = ASCIIEncoding.ASCII.GetBytes(ch);
                DESProvider.IV = ASCIIEncoding.ASCII.GetBytes(ch);
                ICryptoTransform desDecrypt = DESProvider.CreateDecryptor();

                using (CryptoStream cryptostreamDecr = new CryptoStream(v_40, desDecrypt, CryptoStreamMode.Write))
                {
                    byte[] v_42 = new byte[v_39.Length];
                    v_39.Read(v_42, 0, v_42.Length);
                    cryptostreamDecr.Write(v_42, 0, v_42.Length);
                    cryptostreamDecr.FlushFinalBlock();

                    v_40.Seek(0, SeekOrigin.Begin);


                    StreamReader outputStreamReader = new StreamReader(v_40);
                    tmp = outputStreamReader.ReadToEnd();

                    outputStreamReader.Close();

                    v_39.Close();
                }
            }

            return tmp;
        }

        #endregion

        #region ReadFile

        //ParsePinXml
        public void nf_35(string pin, string vers)
        {
            try
            {
                //string file = Environment.CurrentDirectory + "\\MControlNet_230.mck";
                string s = nf_36();
                if (s == null)
                    throw new Exception("Invalid file");
                
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.LoadXml(s);
                System.Xml.XmlNode node = null;
                node = doc.SelectSingleNode("//version");
                if (node == null)
                {
                    throw new Exception("Incorrect version");
                }
                if (!node.InnerText.Equals(vers))
                {
                    throw new Exception("Incorrect version");
                }

                node = doc.SelectSingleNode("//pin_" + pin);
                if (node == null || !node.HasChildNodes)
                {
                    throw new Exception("Incorrect version");
                }

                System.Xml.XmlNodeList list = node.ChildNodes;
                if (list.Count != 2)
                {
                    throw new Exception("Incorrect version");
                }

                node = list.Item(0);
                v_62 = node.InnerText;
                node = list.Item(1);
                v_63 = node.InnerText;
            }
            catch (Exception)
            {

            }

        }

        //ReadFileMck
        public string nf_36()
        {
           string file =mckfile;

           if (mckfile == null || !File.Exists(mckfile))
           {
               file = Environment.CurrentDirectory + "\\MControl_230.pin";
           }

            //string file = (mckfile!=null ? mckfile: Environment.CurrentDirectory) + "\\MControl_230.pin";
            if(! File.Exists(file))
            {
                throw new Exception("File not found " + file ); 
            }
            FileStream fs = null;
            BinaryReader r = null;
            string output = null;
            try
            {
                fs = new FileStream(file, FileMode.Open);
                r = new BinaryReader(fs);

                r.BaseStream.Seek(0, SeekOrigin.Begin);
                output = r.ReadString();
                ED ed = new ED();
                return ed.ed_8(output, true);
                //return output;
            }
            catch (Exception ex)
            {
                output = ex.Message;
                return null;
            }
            finally
            {
                r.Close();
                fs.Close();
            }

        }


        #endregion

    }
}