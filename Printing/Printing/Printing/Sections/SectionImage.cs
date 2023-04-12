
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;
using Nistec.Printing.Drawing;

namespace Nistec.Printing.Sections
{

	/// <summary>
	/// ReportSectionImage is a simple rectangular section that
	/// prints a provided image.
	/// </summary>
	public class SectionImage : ReportSection
	{
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="image">Image to print</param>
		public SectionImage (Image image)
		{
            this.Image = image;
		}

        Image image;
        float width;
        float height;
        bool preserveAspectRatio = true;
        RectangleF imageRect;

        #region "Properties"

        /// <summary>
        /// Gets or sets the image to draw
        /// </summary>
        public Image Image
        {
            get { return this.image; }
            set { this.image = value; }
        }

        /// <summary>
        /// Gets or sets the width for the picture
        /// UseFullWidth takes precedence if set,
        /// and is the default if Width is not set.
        /// </summary>
        public float Width
        {
            get { return this.width; }
            set { this.width = value; }
        }

        /// <summary>
        /// Gets or sets the height for the picture
        /// UseFullHeight takes precedence if set,
        /// and is the defaul if Height is not set.
        /// </summary>
        public float Height
        {
            get { return this.height; }
            set { this.height = value; }
        }

        /// <summary>
        /// Gets or sets the flag to preserve aspect ratio
        /// Default is true.
        /// </summary>
        public bool PreserveAspectRatio
        {
            get { return this.preserveAspectRatio; }
            set { this.preserveAspectRatio = true; }
        }

        #endregion

        /// <summary>
        /// Gets the max size for the image based on 
        /// the user set ImageSize, the current bounds
        /// and the values of use full width / height
        /// </summary>
        SizeF GetMaxSize (Bounds bounds)
        {
            SizeF maxSize;
            if (this.Width == 0 || this.Height == 0)
            {
                maxSize = bounds.GetSizeF();
            }
            else
            {
                maxSize = new SizeF (this.Width, this.Height);
                if (this.UseFullWidth)
                {
                    maxSize.Width = bounds.Width;
                }
                if (this.UseFullHeight)
                {
                    maxSize.Height = bounds.Height;
                }
            }
            return maxSize;
        }

        /// <summary>
        /// Getts a rectangle for the image
        /// </summary>
        RectangleF GetImageRect (Bounds bounds)
        {
            SizeF maxSize = GetMaxSize (bounds);
            float scaleW = maxSize.Width / image.Width;
            float scaleH = maxSize.Height / image.Height;
            if (PreserveAspectRatio)
            {
                float scale = Math.Min(scaleW, scaleH);
                scaleW = scale;
                scaleH = scale;
            }
            float width = scaleW * image.Width;
            float height = scaleH * image.Height;
            SizeF imgSize = new SizeF (width, height);

            return bounds.GetRectangleF (imgSize, this.HorizontalAlignment, this.VerticalAlignment);
        }



        /// <summary>
        /// This method is called after a size and before a print if
        /// the bounds have changed between the sizing and the printing.
        /// Override this function to update anything based on the new location
        /// </summary>
        /// <param name="originalBounds">Bounds originally passed for sizing</param>
        /// <param name="newBounds">New bounds for printing</param>
        /// <returns>SectionSizeValues for the new values of size, fits, continued</returns>
        /// <remarks>To simply have size recalled, implement the following:
        /// <code>
        ///    this.ResetSize();
        ///    return base.ChangeBounds (originalBounds, newBounds);
        /// </code>
        /// </remarks>
        protected override SectionSizeValues BoundsChanged (
            Bounds originalBounds,
            Bounds newBounds)
        {
            SectionSizeValues retval = new SectionSizeValues();
            this.imageRect = GetImageRect(newBounds);
            retval.RequiredSize = this.imageRect.Size;
            retval.Fits = newBounds.SizeFits(retval.RequiredSize);
            retval.Continued = false;
            return retval;
        }


        /// <summary>
        /// This method is used to perform any required initialization.
        /// This method is called exactly once.
        /// This method is called prior to DoCalcSize and DoPrint.
        /// </summary>
        /// <param name="g">Graphics object to print on.</param>
        protected override void DoBeginPrint (
            Graphics g
            )
        {
        }

        /// <summary>
        /// Called to calculate the size that this section requires on
        /// the next call to Print.  This method will be called once
        /// prior to each call to Print.  
        /// </summary>
        /// <param name="printDocument">The parent McPrintDocument that is printing.</param>
        /// <param name="g">Graphics object to print on.</param>
        /// <param name="bounds">Bounds of the area to print within.
        /// The bounds passed already takes the margins into account - so you cannot
        /// print or do anything within these margins.
        /// </param>
        /// <returns>The values for RequireSize, Fits and Continues for this section.</returns>
        protected override SectionSizeValues DoCalcSize (
            McPrintDocument printDocument,
            Graphics g,
            Bounds bounds
            )
        {
            SectionSizeValues retval = new SectionSizeValues();
            this.imageRect = GetImageRect(bounds);
            retval.RequiredSize = this.imageRect.Size;
            retval.Fits = bounds.SizeFits(retval.RequiredSize);
            retval.Continued = false;
            return retval;
        }

        /// <summary>
        /// Called to actually print this section.  
        /// The DoCalcSize method will be called once prior to each
        /// call of DoPrint.
        /// DoPrint is not called if DoCalcSize sets fits to false.
        /// It should obey the values of Size and Continued as set by
        /// DoCalcSize().
        /// </summary>
        /// <param name="printDocument">The parent McPrintDocument that is printing.</param>
        /// <param name="g">Graphics object to print on.</param>
        /// <param name="bounds">Bounds of the area to print within.
        /// These bounds already take the margins into account.
        /// </param>
        protected override void DoPrint (
            McPrintDocument printDocument,
            Graphics g,
            Bounds bounds
            )
        {
            g.DrawImage (this.image, this.imageRect);
        }

	} // class


}
