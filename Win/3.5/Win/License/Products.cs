using System;
using System.Collections.Generic;
using System.Text;

namespace MControl.Util.Net
{

    //nf_1=NetFramReg
    //nf_2=NetReflected
    //nf_4=GetNetAssembly
    //nf_6=checkNetFram
    //ed_7=BytesToString


    //MControl, Version=2.4.0.0, Culture=neutral, PublicKeyToken=7267e8ff9d1f77d0  v_14[]=114,103,232,255,157,31,119,208
    //EnterpriseDB, Version=2.4.0.0, Culture=neutral, PublicKeyToken=0847e55d262dc74f v_14[]=8,71,229,93,38,45,199,79
    //Business, Version=2.4.0.0, Culture=neutral, PublicKeyToken=67c368c91e805727  v_14[]=103,195,104,201,30,128,87,39

    //ProductType
    internal enum v_8
    {
        MControlnet = 0,
        Framework = 1,
        WinForms = 2,
        WebUI = 3,
        GridView = 4,
        ReportView = 5,
        Dal = 6,
        WinUI = 7,
        EnterDB = 8,
        Business = 9,
        Profile=10,
        Ribbon=11,
        Charts=12
  }


    internal class p_1//Products
    {

        internal const string v_50 = "f4a11dc1-4856-4045-8622-6a477628c4ce";//"6E534889-88B1-42A6-89DF-A8745F0FB061";//10001;
        internal const string v_51 = "Net";
        internal const string v_52 = "2.4.0.0";

        //InvalidKey
        internal const string v_24 = "The license key was not set or is invalid. Please make sure that you are correctly licensing the product by setting the LicenseKey property as described in the documentation and then recompile in order to avoid this exception.";
        //InvalidProductCode
        internal const string v_25 = "The license key used to license this MControl product does not match the current product. Please make sure that you are correctly licensing the product as described in the documentation and then recompile in order to avoid this exception.";
        //VersionMismatch
        internal const string v_26 = "The license key used to license this MControl product does not match the current version. Please make sure that you are correctly licensing the product as described in the documentation and then recompile in order to avoid this exception.";
        //ReadRegistry
        internal const string v_27 = "Error reading the license key";
        //UnsupportedKey
        internal const string v_28 = "Unsupported Key";
        //WriteRegistry
        internal const string v_29 = "Error writeing the license key";

        //d79bf865-49e9-4bf0-af1c-2e37c4b0d4a0
        internal const string v_47 = "Software\\MControl\\";
        internal const string v_48 = @"Licenses\baf9f93e-1dd3-404c-8487-5ba142bd0a68\";//7B8C320E-5562-45C4-B6A1-47290195D877\";//A2D75451-F655-41DD-A919-0C59D7809F70\";
        internal const string v_49 = "Desktop";
        internal const int tl = 30;//Trial v_38
        internal const string part1 = "ASD";//part 1 of trial date
        internal const string part2 = "FGH";//part 2 of trial date

        internal const string v_4 = "|";//delimiter
        internal const int v_5 = 9;//productCount
        internal const int v_6 = 13;//assmCount
        internal const string v_7 = " ";//fileDelimiter

        //textArray
        internal static readonly string[] v_1;
        internal static readonly string[] v_2;
        internal static readonly string[] v_3;
                
        //main constructor
        static p_1()
        {
            v_1 = new string[v_6];
            v_2 = new string[v_6];
            v_3 = new string[v_6];


            //Array for v_21 key
            v_1[0] = "";
            v_1[1] = "FRM";// "DDF";
            v_1[2] = "CTL";// "RGT";
            v_1[3] = "WEB";// "MSC";
            v_1[4] = "GRD";// "OPM";
            v_1[5] = "RPT";// "JTH";
            v_1[6] = "DAL";// "TDG";
            v_1[7] = "WZD";// "ASG";

            v_1[8] = "EDB";// "UGJ";
            v_1[9] = "BSN";//"OZS";
            v_1[10] = "PRF";//"LVN";
            v_1[11] = "RBN";//"KUV";
            v_1[12] = "CHT";

             //array for v_21 folder
            v_2[0] = "baf9f93e-1dd3-404c-8487-5ba142bd0a68";//net
            v_2[1] = "f4a11dc1-4856-4045-8622-6a477628c4ce";// Framework
            v_2[2] = "186e3abc-1623-4349-a78b-d046a6f8de52";// WinForms
            v_2[3] = "816c0bc2-5734-42a3-9cf6-f26693e3fd86";// WebUI
            v_2[4] = "70776a93-e3ab-4919-a0f1-ba0723a80a90";// Grid
            v_2[5] = "da54a5e8-8b24-45ed-a753-75eb9e077550";// WinRpt
            v_2[6] = "13d627a4-ff52-4e31-be2e-3bf196d143db";// Dal
            v_2[7] = "659c4e0b-d386-42d1-9a69-eff4fa81432c";// WinUI
          
            v_2[8] = "1e7f2db8-902c-4743-9827-ed3b0878326d";//EnterpriseDB 
            v_2[9] = "bc1a88cb-4cbc-48b5-8cf1-2bd10ef141d1";//Business
            v_2[10] = "003b20c3-9c0b-49c1-966e-09feac99a9ef";//Profile
            v_2[11] = "81a41e48-a6fc-4272-bddf-bc44beb5818b";// Ribbon
            v_2[12] = "784e19e6-7cd7-4b59-9a07-1f00e60765eb";// Charts


            //Array for v_21 Version
            v_3[0] = "2.4.0.0";
            v_3[1] = "2.4.0.0";
            v_3[2] = "2.4.0.0";
            v_3[3] = "2.4.0.0";
            v_3[4] = "2.4.0.0";
            v_3[5] = "2.4.0.0";
            v_3[6] = "2.4.0.0";
            v_3[7] = "2.4.0.0";
            v_3[8] = "2.4.0.0";
            v_3[9] = "2.4.0.0";
            v_3[10] = "2.4.0.0";
            v_3[11] = "2.4.0.0";
            v_3[12] = "2.4.0.0";
       
        }
    }
}
