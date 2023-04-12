namespace Nistec.Charts
{
    using System;
    using System.ComponentModel;
    using System.Security.Permissions;
    using System.Web;

    /// <summary>Defines a rectangular hot spot region in an <see cref="T:ChartImageMap"></see> control. This class cannot be inherited.</summary>
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level=AspNetHostingPermissionLevel.Minimal)]
    [Serializable]
    public sealed class ChartRectangleHotSpot : ChartHotSpot
    {
        /// <summary>Returns a string that represents the x -and y-coordinates of a <see cref="T:ChartRectangleHotSpot"></see> object's top left corner and the x- and y-coordinates of its bottom right corner.</summary>
        /// <returns>A string that represents the x- and y-coordinates of a <see cref="T:ChartRectangleHotSpot"></see> object's top left corner and the x- and y-coordinates of its bottom right corner.</returns>
        public override string GetCoordinates()
        {
            return string.Concat(new object[] { this.Left, ",", this.Top, ",", this.Right, ",", this.Bottom });
        }

        /// <summary>Gets or sets the y-coordinate of the bottom side of the rectangular region defined by this <see cref="T:ChartRectangleHotSpot"></see> object.</summary>
        /// <returns>The y-coordinate of the bottom side of the rectangular region defined by this <see cref="T:ChartRectangleHotSpot"></see> object. The default is 0.</returns>
        [Description("RectangleHotSpot_Bottom"), Category("Appearance"), DefaultValue(0)]
        public int Bottom
        {
            get
            {
                object obj2 = base.ViewState["Bottom"];
                if (obj2 == null)
                {
                    return 0;
                }
                return (int) obj2;
            }
            set
            {
                base.ViewState["Bottom"] = value;
            }
        }

        /// <summary>Gets or sets the x-coordinate of the left side of the rectangular region defined by this <see cref="T:ChartRectangleHotSpot"></see> object.</summary>
        /// <returns>The x-coordinate of the left side of the rectangular region defined by this <see cref="T:ChartRectangleHotSpot"></see> object. The default is 0.</returns>
        [Description("RectangleHotSpot_Left"), Category("Appearance"), DefaultValue(0)]
        public int Left
        {
            get
            {
                object obj2 = base.ViewState["Left"];
                if (obj2 == null)
                {
                    return 0;
                }
                return (int) obj2;
            }
            set
            {
                base.ViewState["Left"] = value;
            }
        }

        protected internal override string MarkupName
        {
            get
            {
                return "rect";
            }
        }

        /// <summary>Gets or sets the x-coordinate of the right side of the rectangular region defined by this <see cref="T:ChartRectangleHotSpot"></see> object.</summary>
        /// <returns>The x-coordinate of the right side of the rectangular region defined by this <see cref="T:ChartRectangleHotSpot"></see> object. The default is 0.</returns>
        [Category("Appearance"), DefaultValue(0), Description("RectangleHotSpot_Right")]
        public int Right
        {
            get
            {
                object obj2 = base.ViewState["Right"];
                if (obj2 == null)
                {
                    return 0;
                }
                return (int) obj2;
            }
            set
            {
                base.ViewState["Right"] = value;
            }
        }

        /// <summary>Gets or sets the y-coordinate of the top side of the rectangular region defined by this <see cref="T:ChartRectangleHotSpot"></see> object.</summary>
        /// <returns>The y-coordinate of the top side of the rectangular region defined by this <see cref="T:ChartRectangleHotSpot"></see> object. The default is 0.</returns>
        [Category("Appearance"), Description("RectangleHotSpot_Top"), DefaultValue(0)]
        public int Top
        {
            get
            {
                object obj2 = base.ViewState["Top"];
                if (obj2 == null)
                {
                    return 0;
                }
                return (int) obj2;
            }
            set
            {
                base.ViewState["Top"] = value;
            }
        }
    }
}

