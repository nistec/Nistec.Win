using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Imaging;

namespace Nistec.Printing.View
{
	internal class TIFprop//cls1082   public
	{
		// Fields
		private ImageType _Format;

        public static TIFprop Default()
        {
            TIFprop p = new TIFprop();
            return p;
        }

		internal TIFprop()
		{
			this.SetDefault();
		}

 
		internal static ImageFormat cls1083(ImageType var0)
		{
			if (var0 == ImageType.Bmp)
			{
				return ImageFormat.Bmp;
			}
			if (var0 == ImageType.Gif)
			{
				return ImageFormat.Gif;
			}
			if (var0 == ImageType.Jpeg)
			{
				return ImageFormat.Jpeg;
			}
			if (var0 == ImageType.Emf)
			{
				return ImageFormat.Emf;
			}
			if ((var0 != ImageType.Wmf) && (var0 == ImageType.Tiff))
			{
				return ImageFormat.Tiff;
			}
			return ImageFormat.Png;
		}

		internal void SetDefault()
		{
			this._Format = ImageType.Png;
		}

 
		public bool ShouldSerializeImageType()
		{
			return (this._Format != ImageType.Png);
		}

 
		[Description("Determines Image Type for output.")]
		public ImageType Format
		{
			get
			{
				return this._Format;
			}
			set
			{
				this._Format = value;
			}
		}
 
	}


}
