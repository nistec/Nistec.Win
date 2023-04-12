namespace Nistec.Charts
{
    using System;
    using System.ComponentModel;
    using System.Security.Permissions;
    using System.Web;

    /// <summary>Defines a circular hot spot region in an <see cref="T:ChartImageMap"></see> control. This class cannot be inherited.</summary>
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level=AspNetHostingPermissionLevel.Minimal)]
    public sealed class ChartCircleHotSpot : ChartHotSpot
    {
        /// <summary>Returns a string that represents the x- and y-coordinates of a <see cref="T:ChartCircleHotSpot"></see> object's center and the length of its radius.</summary>
        /// <returns>A string that represents the x- and y-coordinates of a <see cref="T:ChartCircleHotSpot"></see> object's center and the length of its radius.</returns>
        public override string GetCoordinates()
        {
            return string.Concat(new object[] { this.X, ",", this.Y, ",", this.Radius });
        }

        protected internal override string MarkupName
        {
            get
            {
                return "circle";
            }
        }

        /// <summary>Gets or sets the distance from the center to the edge of the circular region defined by this <see cref="T:ChartCircleHotSpot"></see> object.</summary>
        /// <returns>An integer that represents the distance in pixels from the center to the edge of the circular region defined by this <see cref="T:ChartCircleHotSpot"></see> object. The default is 0.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is less than 0. </exception>
        [Category("Appearance"), Description("CircleHotSpot_Radius"), DefaultValue(0)]
        public int Radius
        {
            get
            {
                object obj2 = base.ViewState["Radius"];
                if (obj2 != null)
                {
                    return (int) obj2;
                }
                return 0;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                base.ViewState["Radius"] = value;
            }
        }

        /// <summary>Gets or sets the x-coordinate of the center of the circular region defined by this <see cref="T:ChartCircleHotSpot"></see> object.</summary>
        /// <returns>The x-coordinate of the center of the circular region defined by this <see cref="T:ChartCircleHotSpot"></see> object. The default is 0.</returns>
        [DefaultValue(0), Category("Appearance"), Description("CircleHotSpot_X")]
        public int X
        {
            get
            {
                object obj2 = base.ViewState["X"];
                if (obj2 == null)
                {
                    return 0;
                }
                return (int) obj2;
            }
            set
            {
                base.ViewState["X"] = value;
            }
        }

        /// <summary>Gets or sets the y-coordinate of the center of the circular region defined by this <see cref="T:ChartCircleHotSpot"></see> object.</summary>
        /// <returns>The y-coordinate of the center of the circular region defined by this <see cref="T:ChartCircleHotSpot"></see> object. The default is 0.</returns>
        [Description("CircleHotSpot_Y"), Category("Appearance"), DefaultValue(0)]
        public int Y
        {
            get
            {
                object obj2 = base.ViewState["Y"];
                if (obj2 == null)
                {
                    return 0;
                }
                return (int) obj2;
            }
            set
            {
                base.ViewState["Y"] = value;
            }
        }
    }
}

