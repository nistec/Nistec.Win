using System;
using System.Globalization;



namespace Nistec.WinForms
{

	public sealed class Global
	{
		public static readonly string ImagesPath="Nistec.WinForms.Images.";

 	}


	public sealed class Defaults
	{

		public const int DefHeight=20;
		public const int minHeight=13;
		public const int ButtonWidth=16;

        //private static Styles ctlStyle=Styles.None;

        ////public static ControlLayout ControlLayout {get{return ControlLayout.Flat;}}
        ////public static Styles StylePlan{get{return Styles.Desktop;}}

        //public static Styles GetControlStyle()
        //{
        //    try
        //    {
        //        if (ctlStyle != Styles.None)
        //            return ctlStyle;

        //        Styles style=Styles.Silver;
        //        Microsoft.Win32.RegistryKey rk= Microsoft.Win32.Registry.LocalMachine.OpenSubKey ("Software\\Nistec\\WinForms");
        //        if(rk !=null)
        //        {
        //            string s = rk.GetValue("Style",StyleLayout.DefaultStylePlan.ToString()).ToString();
        //            style=(Styles) Enum.Parse(typeof(Styles), s,true); 
        //            if (style==Styles.Custom || style==Styles.None)
        //            {
        //                //MsgBox.ShowError("Is not a valid Style , the default style is Silver");
        //            }
        //            else
        //            {
        //                return style;
        //            }
        //        }
        //        ctlStyle = style;
        //        return StyleLayout.DefaultStylePlan;
        //    }
        //    catch
        //    {
        //        return StyleLayout.DefaultStylePlan;
        //    }
        //}

	}
}


