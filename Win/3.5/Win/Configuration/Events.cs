

using System;

namespace MControl.Configuration
{	
	/// <summary>
	///   Types of changes that may be made to a Config object. </summary>
	/// <remarks>
	///   A variable of this type is passed inside the ConfigChangedArgs object 
	///   for the <see cref="Config.Changing" /> and <see cref="Config.Changed" /> events </remarks>
	/// <seealso cref="ConfigChangedArgs" />
	public enum ConfigChangeType
	{
		/// <summary> 
		///   The change refers to the <see cref="Config.Name" /> property. </summary>		
		/// <remarks> 
		///   <see cref="ConfigChangedArgs.Value" /> will contain the new name. </remarks>
		Name,

		/// <summary> 
		///   The change refers to the <see cref="Config.ReadOnly" /> property. </summary>		
		/// <remarks> 
		///   <see cref="ConfigChangedArgs.Value" /> will be true. </remarks>
		ReadOnly,

		/// <summary> 
		///   The change refers to the <see cref="Config.SetValue" /> method. </summary>		
		/// <remarks> 
		///   <see cref="ConfigChangedArgs.Section" />,  <see cref="ConfigChangedArgs.Entry" />, 
		///   and <see cref="ConfigChangedArgs.Value" /> will be set to the same values passed 
		///   to the SetValue method. </remarks>
		SetValue,

		/// <summary> 
		///   The change refers to the <see cref="Config.RemoveEntry" /> method. </summary>		
		/// <remarks> 
		///   <see cref="ConfigChangedArgs.Section" /> and <see cref="ConfigChangedArgs.Entry" /> 
		///   will be set to the same values passed to the RemoveEntry method. </remarks>
		RemoveEntry,

		/// <summary> 
		///   The change refers to the <see cref="Config.RemoveSection" /> method. </summary>		
		/// <remarks> 
		///   <see cref="ConfigChangedArgs.Section" /> will contain the name of the section passed to the RemoveSection method. </remarks>
		RemoveSection,

		/// <summary> 
		///   The change refers to method or property specific to the Config class. </summary>		
		/// <remarks> 
		///   <see cref="ConfigChangedArgs.Entry" /> will contain the name of the  method or property.
		///   <see cref="ConfigChangedArgs.Value" /> will contain the new value. </remarks>
		Other
	}
	
	/// <summary>
	///   EventArgs class to be passed as the second parameter of a <see cref="Config.Changed" /> event handler. </summary>
	/// <remarks>
	///   This class provides all the information relevant to the change made to the Config.
	///   It is also used as a convenient base class for the ConfigChangingArgs class which is passed 
	///   as the second parameter of the <see cref="Config.Changing" /> event handler. </remarks>
	/// <seealso cref="ConfigChangingArgs" />
	public class ConfigChangedArgs : EventArgs
	{   
		// Fields
		private readonly ConfigChangeType m_changeType;
		private readonly string m_section;
		private readonly string m_entry;
		private readonly object m_value;

		/// <summary>
		///   Initializes a new instance of the ConfigChangedArgs class by initializing all of its properties. </summary>
		/// <param name="changeType">
		///   The type of change made to the Config. </param>
		/// <param name="section">
		///   The name of the section involved in the change or null. </param>
		/// <param name="entry">
		///   The name of the entry involved in the change, or if changeType is set to Other, the name of the method/property that was changed. </param>
		/// <param name="value">
		///   The new value for the entry or method/property, based on the value of changeType. </param>
		/// <seealso cref="ConfigChangeType" />
		public ConfigChangedArgs(ConfigChangeType changeType, string section, string entry, object value) 
		{
			m_changeType = changeType;
			m_section = section;
			m_entry = entry;
			m_value = value;
		}
		
		/// <summary>
		///   Gets the type of change that raised the event. </summary>
		public ConfigChangeType ChangeType
		{
			get 
			{
				return m_changeType;
			}
		}
		
		/// <summary>
		///   Gets the name of the section involved in the change, or null if not applicable. </summary>
		public string Section
		{
			get 
			{
				return m_section;
			}
		}
		
		/// <summary>
		///   Gets the name of the entry involved in the change, or null if not applicable. </summary>
		/// <remarks> 
		///   If <see cref="ChangeType" /> is set to Other, this property holds the name of the 
		///   method/property that was changed. </remarks>
		public string Entry
		{
			get 
			{
				return m_entry;
			}
		}
		
		/// <summary>
		///   Gets the new value for the entry or method/property, based on the value of <see cref="ChangeType" />. </summary>
		public object Value
		{
			get 
			{
				return m_value;
			}
		}
	}

	/// <summary>
	///   EventArgs class to be passed as the second parameter of a <see cref="Config.Changing" /> event handler. </summary>
	/// <remarks>
	///   This class provides all the information relevant to the change about to be made to the Config.
	///   Besides the properties of ConfigChangedArgs, it adds the Cancel property which allows the 
	///   event handler to prevent the change from happening. </remarks>
	/// <seealso cref="ConfigChangedArgs" />
	public class ConfigChangingArgs : ConfigChangedArgs
	{   
		private bool m_cancel;
		
		/// <summary>
		///   Initializes a new instance of the ConfigChangingArgs class by initializing all of its properties. </summary>
		/// <param name="changeType">
		///   The type of change to be made to the Config. </param>
		/// <param name="section">
		///   The name of the section involved in the change or null. </param>
		/// <param name="entry">
		///   The name of the entry involved in the change, or if changeType is set to Other, the name of the method/property that was changed. </param>
		/// <param name="value">
		///   The new value for the entry or method/property, based on the value of changeType. </param>
		/// <seealso cref="ConfigChangeType" />
		public ConfigChangingArgs(ConfigChangeType changeType, string section, string entry, object value) :
			base(changeType, section, entry, value)
		{
		}
		                    
		/// <summary>
		///   Gets or sets whether the change about to the made should be canceled or not. </summary>
		/// <remarks> 
		///   By default this property is set to false, meaning that the change is allowed.  </remarks>
		public bool Cancel
		{
			get 
			{
				return m_cancel;
			}
			set
			{
				m_cancel = value;
			}
		}
	}
   
	/// <summary>
	///   Definition of the <see cref="Config.Changing" /> event handler. </summary>
	/// <remarks>
	///   This definition complies with the .NET Framework's standard for event handlers.
	///   The sender is always set to the Config object that raised the event. </remarks>
	public delegate void ConfigChangingHandler(object sender, ConfigChangingArgs e);

	/// <summary>
	///   Definition of the <see cref="Config.Changed" /> event handler. </summary>
	/// <remarks>
	///   This definition complies with the .NET Framework's standard for event handlers.
	///   The sender is always set to the Config object that raised the event. </remarks>
	public delegate void ConfigChangedHandler(object sender, ConfigChangedArgs e);
}

