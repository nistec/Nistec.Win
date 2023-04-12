
using System;
using System.Windows.Forms;  
using System.Security;
using Microsoft.Win32;

namespace MControl.Configuration
{
	/// <summary>
	///   Config class that utilizes the Windows Registry to retrieve and save its data. </summary>
	///   Each section is then created as a subkey of this location on the registry. </remarks>
	public class Registry : Config
	{
		// Fields
		private RegistryKey m_rootKey = Microsoft.Win32.Registry.LocalMachine; 

		/// <summary>
		///   Initializes a new instance of the Registry class by setting the <see cref="Config.Name" /> to <see cref="Config.DefaultName" />. </summary>
		public Registry()
		{
		}

		/// <summary>
		///   Initializes a new instance of the Registry class by setting the <see cref="RootKey" /> and/or <see cref="Config.Name" />. </summary>
		/// <param name="rootKey">
		///   If not null, this is used to initialize the <see cref="RootKey" /> property. </param>
		/// <param name="subKeyName">
		///   If not null, this is used to initialize the <see cref="Config.Name" /> property. </param>
		public Registry(RegistryKey rootKey, string subKeyName) :
			base("")
		{
			if (rootKey != null)
				m_rootKey = rootKey;
			if (subKeyName != null)
				Name = subKeyName;
		}

		/// <summary>
		///   Initializes a new instance of the Registry class based on another Registry object. </summary>
		/// <param name="reg">
		///   The Registry object whose properties and events are used to initialize the object being constructed. </param>
		public Registry(Registry reg) :
			base(reg)
		{
			m_rootKey = reg.m_rootKey;
		}

		/// <summary>
		///   Gets the default name sub-key registry path. </summary>
		/// <exception cref="InvalidOperationException">
		///   Application.CompanyName or Application.ProductName are empty.</exception>
		/// <remarks>
		///   This is set to "Software\\" + Application.CompanyName + "\\" + Application.ProductName. </remarks>
		public override string DefaultName
		{
			get
			{
				if (Application.CompanyName == "" || Application.ProductName == "")
					throw new InvalidOperationException("Application.CompanyName and/or Application.ProductName are empty and they're needed for the DefaultName.");
				
				return "Software\\" + Application.CompanyName + "\\" + Application.ProductName;			
			}
		}

		/// <summary>
		///   Retrieves a copy of itself. </summary>
		/// <returns>
		///   The return value is a copy of itself as an object. </returns>
		/// <seealso cref="Config.CloneReadOnly" />
		public override object Clone()
		{
			return new Registry(this);
		}

		/// <summary>
		///   Gets or sets the root RegistryKey object to use as the base for the <see cref="Config.Name" />. </summary>
		/// <exception cref="InvalidOperationException">
		///   Setting this property if <see cref="Config.ReadOnly" /> is true. </exception>
		/// <remarks>
		///   By default, this property is set to Microsoft.Win32.Registry.CurrentUser. 
		///</remarks>
		public RegistryKey RootKey
		{
			get 
			{ 
				return m_rootKey; 
			}
			set 
			{ 
				VerifyNotReadOnly();
				if (m_rootKey == value)
					return;
				
				if (!RaiseChangeEvent(true, ConfigChangeType.Other, null, "RootKey", value))
					return;
				
				m_rootKey = value; 
				RaiseChangeEvent(false, ConfigChangeType.Other, null, "RootKey", value);
			}
		}

		/// <summary>
		///   Retrieves a RegistryKey object for the given section. </summary>
		/// <param name="section">
		///   The name of the section to retrieve the key for. </param>
		/// <param name="create">
		///   If true, the key is created if necessary; otherwise it is just opened. </param>
		/// <param name="writable">
		///   If true the key is opened with write access; otherwise it is opened read-only. </param>
		/// <returns>
		///   The return value is a RegistryKey object representing the section's subkey. </returns>
		/// <remarks>
		///   This method returns a key for the equivalent path: <see cref="RootKey" /> + "\\" + <see cref="Config.Name" /> + "\\" + section </remarks>
		protected RegistryKey GetSubKey(string section, bool create, bool writable)		
		{
			VerifyName();
			
			string keyName = Name + "\\" + section;

			if (create)
				return m_rootKey.CreateSubKey(keyName);
			return m_rootKey.OpenSubKey(keyName, writable);
		}

		/// <summary>
		///   Sets the value for an entry inside a section. </summary>
		/// <param name="section">
		///   The name of the section that holds the entry. </param>
		/// <param name="entry">
		///   The name of the entry where the value will be set. </param>
		/// <param name="value">
		///   The value to set. If it's null, the entry is removed. </param>
		public override void SetValue(string section, string entry, object value)
		{
			// If the value is null, remove the entry
			if (value == null)
			{
				RemoveEntry(section, entry);
				return;
			}
			
			VerifyNotReadOnly();
			VerifyAndAdjustSection(ref section);
			VerifyAndAdjustEntry(ref entry);
			
			if (!RaiseChangeEvent(true, ConfigChangeType.SetValue, section, entry, value))
				return;
			
			using (RegistryKey subKey = GetSubKey(section, true, true))
				subKey.SetValue(entry, value);
			
			RaiseChangeEvent(false, ConfigChangeType.SetValue, section, entry, value);
		}

		/// <summary>
		///   Retrieves the value of an entry inside a section. </summary>
		/// <param name="section">
		///   The name of the section that holds the entry with the value. </param>
		/// <param name="entry">
		///   The name of the entry where the value is stored. </param>
		/// <returns>
		///   The return value is the entry's value, or null if the entry does not exist. </returns>
		public override object GetValue(string section, string entry)
		{
			VerifyAndAdjustSection(ref section);
			VerifyAndAdjustEntry(ref entry);

			using (RegistryKey subKey = GetSubKey(section, false, false))
				return (subKey == null ? null : subKey.GetValue(entry));
		}

		/// <summary>
		///   Removes an entry from a section. </summary>
		/// <param name="section">
		///   The name of the section that holds the entry. </param>
		/// <param name="entry">
		///   The name of the entry to remove. </param>
		public override void RemoveEntry(string section, string entry)
		{
			VerifyNotReadOnly();
			VerifyAndAdjustSection(ref section);
			VerifyAndAdjustEntry(ref entry);
			
			using (RegistryKey subKey = GetSubKey(section, false, true))
			{
				if (subKey != null && subKey.GetValue(entry) != null)
				{
					if (!RaiseChangeEvent(true, ConfigChangeType.RemoveEntry, section, entry, null))
						return;
			
					subKey.DeleteValue(entry, false);
					RaiseChangeEvent(false, ConfigChangeType.RemoveEntry, section, entry, null);
				}
			}	
		}

		/// <summary>
		///   Removes a section. </summary>
		/// <param name="section">
		///   The name of the section to remove. </param>
		public override void RemoveSection(string section)
		{
			VerifyNotReadOnly();
			VerifyName();
			VerifyAndAdjustSection(ref section);
			
			using (RegistryKey key = m_rootKey.OpenSubKey(Name, true))
			{
				if (key != null && HasSection(section))
				{
					if (!RaiseChangeEvent(true, ConfigChangeType.RemoveSection, section, null, null))
						return;
					
					key.DeleteSubKeyTree(section);
					RaiseChangeEvent(false, ConfigChangeType.RemoveSection, section, null, null);
				}
			}	
		}
		
		/// <summary>
		///   Retrieves the names of all the entries inside a section. </summary>
		/// <param name="section">
		///   The name of the section holding the entries. </param>
		public override string[] GetEntryNames(string section)
		{
			VerifyAndAdjustSection(ref section);

			using (RegistryKey subKey = GetSubKey(section, false, false))
			{
				if (subKey == null)
					return null;
				
				return subKey.GetValueNames();
			}				
		}		

		/// <summary>
		///   Retrieves the names of all the sections. </summary>
		/// <returns>
		///   If the XML file exists, the return value is an array with the names of all the sections;
		///   otherwise it's null. </returns>
		public override string[] GetSectionNames()
		{
			VerifyName();
			
			using (RegistryKey key = m_rootKey.OpenSubKey(Name))
			{
				if (key == null)
					return null;				
				return key.GetSubKeyNames();
			}				
		}		
	}
}
