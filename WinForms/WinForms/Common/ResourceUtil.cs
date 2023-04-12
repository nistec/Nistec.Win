
using System;
using System.IO;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Nistec.WinForms
{
    public class ResourceUtil
    {

		public static Image LoadImage(string imageName)
		{			
		
			
			Stream strm = Type.GetType("Nistec.WinForms.ResourceUtil").Assembly.GetManifestResourceStream(imageName);
 
			Image im = null;
			if(strm != null)
			{
				im = new System.Drawing.Bitmap(strm);
				strm.Close();
			}

			return im;
		}


		public static Icon LoadIcon(string iconName)
		{			
			
			Stream strm = Type.GetType("Nistec.WinForms.ResourceUtil").Assembly.GetManifestResourceStream(iconName);

			Icon ic = null;
			if(strm != null)
			{
				ic = new System.Drawing.Icon(strm);
				strm.Close();
			}

			return ic;
		}

		public static Image ExtractImage(string p_Image)
		{
					System.Reflection.Assembly  l_as = System.Reflection.Assembly.GetExecutingAssembly();
					return Image.FromStream(l_as.GetManifestResourceStream(p_Image));
		}

    }
}