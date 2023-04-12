namespace Nistec.Charts
{
    using System;
    using System.Security.Permissions;
    using System.Web;

    /// <summary>Provides data for the <see cref="E:ChartImageMap.Click"></see> event of an <see cref="T:ChartImageMap"></see> control.</summary>
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level=AspNetHostingPermissionLevel.Minimal), AspNetHostingPermission(SecurityAction.InheritanceDemand, Level=AspNetHostingPermissionLevel.Minimal)]
    public class ChartImageMapEventArgs : EventArgs
    {
        private string _postBackValue;

        /// <summary>Initializes a new instance of the <see cref="T:ChartImageMapEventArgs"></see> class.</summary>
        /// <param name="value">The <see cref="T:System.String"></see> object assigned to the <see cref="P:ChartHotSpot.PostBackValue"></see> property of the <see cref="T:ChartHotSpot"></see> object that was clicked. </param>
        public ChartImageMapEventArgs(string value)
        {
            this._postBackValue = value;
        }

        /// <summary>Gets the <see cref="T:System.String"></see> assigned to the <see cref="P:ChartHotSpot.PostBackValue"></see> property of the <see cref="T:ChartHotSpot"></see> object that was clicked.</summary>
        /// <returns>The <see cref="T:System.String"></see> assigned to the <see cref="P:ChartHotSpot.PostBackValue"></see> property of the <see cref="T:ChartHotSpot"></see> object that was clicked.</returns>
        public string PostBackValue
        {
            get
            {
                return this._postBackValue;
            }
        }
    }
}

