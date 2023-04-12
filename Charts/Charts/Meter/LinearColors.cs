using System;
using System.Drawing;
using System.ComponentModel;

namespace Nistec.Charts.Utils
{
	/// <summary>
    /// LinearColors.
	/// </summary>
	[TypeConverter(typeof(LinearColorsTypeConverter))]
	public class LinearColors
	{

		System.Drawing.Color color1;
		

		public System.Drawing.Color ColorStart 
        {
			get {
                return this.color1;
			}
			set {
                this.color1 = value;
                PropertyChanged("ColorStart");
			}
		}

		System.Drawing.Color color2;

        public System.Drawing.Color ColorEnd
        {
			get {
                return this.color2;
			}
			set {
                this.color2 = value;
                PropertyChanged("ColorEnd");
			}
		}

		int zoneAngle;

        public int ColorAngle
        {
			get {
				return zoneAngle;
			}
			set {
				this.zoneAngle = value;
				PropertyChanged("ColorAngle");
			}
		}


        public LinearColors()
		{
			
		    this.color1 = System.Drawing.Color.LightBlue;//.Blue;
			this.color2 = System.Drawing.Color.White;//.Green;
			this.zoneAngle = 45;
		}

		public override string ToString() {
            return "Linear Colors";
		}

		#region Events
		public event PropertyChangedEventHandler PropertyChanged; 
		#endregion

		public delegate void PropertyChangedEventHandler( string propertyname);



	}
}
