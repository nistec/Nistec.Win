using System;

namespace ReportPrinting
{
	/// <summary>
	/// A class representing margins.  Each value is the amount of
	/// margin on each side of a rectangular area.
	/// </summary>
	public class Margins
	{
        /// <summary>
        /// Default constructor
        /// </summary>
		public Margins()
		{
		}

        /// <summary>
        /// Constructor that sets all margins
        /// </summary>
        /// <param name="left">The margin on the left side, in inches</param>
        /// <param name="right">The margin on the right side, in inches</param>
        /// <param name="top">The margin on the top, in inches</param>
        /// <param name="bottom">The margin on the bottom, in inches</param>
        public Margins (float left, float right, float top, float bottom)
        {
            this.Left = left;
            this.Right = right;
            this.Top = top;
            this.Bottom = bottom;
        }

        /// <summary>
        /// Gets or sets the margin on the left side, in inches
        /// </summary>
        public float Left;
        /// <summary>
        /// Gets or sets the margin on the right side, in inches
        /// </summary>
        public float Right;
        /// <summary>
        /// Gets or sets the margin on the top, in inches
        /// </summary>
        public float Top;
        /// <summary>
        /// Gets or sets the margin on the bottom, in inches
        /// </summary>
        public float Bottom;

	}
}
