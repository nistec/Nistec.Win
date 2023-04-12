using System;
using System.Collections.Specialized;

namespace MControl.Win
{
	#region UseApiElementsAttribute
	
	[AttributeUsage(AttributeTargets.Property|AttributeTargets.Method|
		 AttributeTargets.Constructor)]
	public sealed class UseApiElementsAttribute : Attribute
	{
		StringCollection elementsNames = new StringCollection();
		
		public UseApiElementsAttribute(string elementNames) 
		{
			this.elementsNames.Add(elementNames);
		}

		public StringCollection ElementsNames
		{
			get {return elementsNames;}
		}	
	}

	#endregion
}
