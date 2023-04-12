namespace Nistec.Charts
{
    using System;
    using System.ComponentModel;
    using System.Security.Permissions;
    using System.Web;

    /// <summary>Defines a polygon-shaped hot spot region in an <see cref="T:ChartImageMap"></see> control. This class cannot be inherited.</summary>
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level=AspNetHostingPermissionLevel.Minimal)]
    [Serializable]
    public sealed class ChartPolygonHotSpot : ChartHotSpot
    {
        /// <summary>Returns a string that represents the coordinates of the vertexes of a <see cref="T:ChartPolygonHotSpot"></see> object.</summary>
        /// <returns>A string that represents the coordinates of the vertexes of a <see cref="T:ChartPolygonHotSpot"></see> object. The default value is an empty string ("").</returns>
        public override string GetCoordinates()
        {
            return this.Coordinates;
        }

        /// <summary>A string of coordinates that represents the vertexes of a <see cref="T:ChartPolygonHotSpot"></see> object.</summary>
        /// <returns>A string that represents the coordinates of a <see cref="T:ChartPolygonHotSpot"></see> object's vertexes.</returns>
        [DefaultValue(""), Category("Appearance"), Description("PolygonHotSpot_Coordinates")]
        public string Coordinates
        {
            get
            {
                string str = base.ViewState["Coordinates"] as string;
                if (str == null)
                {
                    return string.Empty;
                }
                return str;
            }
            set
            {
                base.ViewState["Coordinates"] = value;
            }
        }

        protected internal override string MarkupName
        {
            get
            {
                return "poly";
            }
        }
    }
}

