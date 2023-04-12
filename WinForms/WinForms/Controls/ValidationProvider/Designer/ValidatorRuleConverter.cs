
using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;

namespace Nistec.WinForms
{
	/// <summary>
	/// ValidatorRuleConverter allow ValidatorRule to be Designer's serializable.
	/// </summary>
	public class ValidatorRuleConverter : TypeConverter
	{
		/// <summary>
		/// Override so Designer can Serialize ValidatorRule.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="destinationType"></param>
		/// <returns></returns>
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
            //System.Windows.Forms.MessageBox.Show("vld3.0");
			return (destinationType == typeof(InstanceDescriptor)) ? true : base.CanConvertTo(context, destinationType);
		}

		/// <summary>
		/// Override so designer can Deserialize ValidatorRule.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="culture"></param>
		/// <param name="value"></param>
		/// <param name="destinationType"></param>
		/// <returns></returns>
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
            //System.Windows.Forms.MessageBox.Show("vld3.1");
			if (destinationType == typeof(InstanceDescriptor))
			{
				Type[] typeArray = new Type[0] {} ;
 
				// get default constructor
				System.Reflection.ConstructorInfo ci =
					typeof(ValidatorRule).GetConstructor(typeArray);
            
				return new InstanceDescriptor(ci, typeArray, false);
			}
    
			return base.ConvertTo(context, culture, value, destinationType);

		}
	}
}
