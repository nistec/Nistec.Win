using System;
using System.ComponentModel;

namespace Nistec.Charts.Utils
{
	/// <summary>
	/// Summary description for MeterFaceTypeConverter.
	/// </summary>
	public class LinearColorsTypeConverter: TypeConverter
	{
        public LinearColorsTypeConverter()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public override bool GetPropertiesSupported(ITypeDescriptorContext context) {
			return true;
		}

		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes) {
            return TypeDescriptor.GetProperties(typeof(LinearColors));
		}

	}
}
