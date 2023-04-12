using System;
using System.Text;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices; 
using System.Reflection;
using System.IO;

namespace MControl.Configuration
{
	/// <summary>
	///   Config class that utilizes an INI-formatted file to retrieve and save its data. </summary>
	public class Ini : Config
	{
		/// <summary>
		///   Initializes a new instance of the Ini class by setting the <see cref="Config.Name" /> to <see cref="Config.DefaultName" />. </summary>
		public Ini()
		{
		}

		/// <summary>
		///   Initializes a new instance of the Ini class by setting the <see cref="Config.Name" /> to the given file name. </summary>
		/// <param name="fileName">
		///   The name of the INI file to initialize the <see cref="Config.Name" /> property with. </param>
		public Ini(string fileName) :
			base(fileName)
		{
		}

		/// <summary>
		///   Initializes a new instance of the Ini class based on another Ini object. </summary>
		/// <param name="ini">
		///   The Ini object whose properties and events are used to initialize the object being constructed. </param>
		public Ini(Ini ini) :
			base(ini)
		{
		}

		/// <summary>
		///   Gets the default name for the INI file. </summary>
		/// <remarks>
		///   For Windows apps, this property returns the name of the executable plus .ini ("program.exe.ini").
		///   For Web apps, this property returns the full path of <i>web.ini</i> based on the root folder.
		///   This property is used to set the <see cref="Config.Name" /> property inside the default constructor.</remarks>
		public override string DefaultName
		{
			get
			{
				return DefaultNameWithoutExtension + ".ini";
			}
		}

		/// <summary>
		///   Retrieves a copy of itself. </summary>
		/// <returns>
		///   The return value is a copy of itself as an object. </returns>
		/// <seealso cref="Config.CloneReadOnly" />
		public override object Clone()
		{
			return new Ini(this);
		}

		[DllImport("kernel32", SetLastError=true)]
		static extern int WritePrivateProfileString(string section, string key, string value, string fileName);
		[DllImport("kernel32", SetLastError=true)]
		static extern int WritePrivateProfileString(string section, string key, int value, string fileName);
		[DllImport("kernel32", SetLastError=true)]
		static extern int WritePrivateProfileString(string section, int key, string value, string fileName);
		[DllImport("kernel32")]
		static extern int GetPrivateProfileString(string section, string key, string defaultValue, StringBuilder result, int size, string fileName);
		[DllImport("kernel32")]
		static extern int GetPrivateProfileString(string section, int key, string defaultValue, [MarshalAs(UnmanagedType.LPArray)] byte[] result, int size, string fileName);
		[DllImport("kernel32")]
		static extern int GetPrivateProfileString(int section, string key, string defaultValue, [MarshalAs(UnmanagedType.LPArray)] byte[] result, int size, string fileName);


		// The Win32 API methods
//		[DllImport("kernel32", SetLastError=true)]
//        static extern int WritePrivateConfigString(string section, string key, string value, string fileName);
//        [DllImport("kernel32", SetLastError=true)]
//		static extern int WritePrivateConfigString(string section, string key, int value, string fileName);
//        [DllImport("kernel32", SetLastError=true)]
//        static extern int WritePrivateConfigString(string section, int key, string value, string fileName);
//        [DllImport("kernel32")]
//        static extern int GetPrivateConfigString(string section, string key, string defaultValue, StringBuilder result, int size, string fileName);
//        [DllImport("kernel32")]
//        static extern int GetPrivateConfigString(string section, int key, string defaultValue, [MarshalAs(UnmanagedType.LPArray)] byte[] result, int size, string fileName);
//        [DllImport("kernel32")]
//        static extern int GetPrivateConfigString(int section, string key, string defaultValue, [MarshalAs(UnmanagedType.LPArray)] byte[] result, int size, string fileName);

		/// <summary>
		///   Sets the value for an entry inside a section. If the INI file does not exist, it is created.</summary>
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
			VerifyName();
			VerifyAndAdjustSection(ref section);
			VerifyAndAdjustEntry(ref entry);
			
			if (!RaiseChangeEvent(true, ConfigChangeType.SetValue, section, entry, value))
				return;
					
			
			if (WritePrivateProfileString(section, entry, value.ToString(), Name) == 0)
				throw new Win32Exception();
			//if (WritePrivateConfigString(section, entry, value.ToString(), Name) == 0)
			//	throw new Win32Exception();

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
			VerifyName();
			VerifyAndAdjustSection(ref section);
			VerifyAndAdjustEntry(ref entry);

			// Loop until the buffer has grown enough to fit the value
			for (int maxSize = 250; true; maxSize *= 2)
			{
				StringBuilder result = new StringBuilder(maxSize);
            	int size = GetPrivateProfileString(section, entry, "", result, maxSize, Name);
				
				if (size < maxSize - 1)
				{					
					if (size == 0 && !HasEntry(section, entry))
						return null;
					return result.ToString();
				}
			}
		}

		/// <summary>
		///   Removes an entry from a section. </summary>
		/// <param name="section">
		///   The name of the section that holds the entry. </param>
		/// <param name="entry">
		///   The name of the entry to remove. </param>
		public override void RemoveEntry(string section, string entry)
		{
			// Verify the entry exists
			if (!HasEntry(section, entry))
				return;
				
			VerifyNotReadOnly();
			VerifyName();
			VerifyAndAdjustSection(ref section);
			VerifyAndAdjustEntry(ref entry);
			
			if (!RaiseChangeEvent(true, ConfigChangeType.RemoveEntry, section, entry, null))
				return;
			
			if (WritePrivateProfileString(section, entry, 0, Name) == 0)
				throw new Win32Exception();

			RaiseChangeEvent(false, ConfigChangeType.RemoveEntry, section, entry, null);
		}

		/// <summary>
		///   Removes a section. </summary>
		/// <param name="section">
		///   The name of the section to remove. </param>
		public override void RemoveSection(string section)
		{
			// Verify the section exists
			if (!HasSection(section))
				return;
			
			VerifyNotReadOnly();
			VerifyName();
			VerifyAndAdjustSection(ref section);
			
			if (!RaiseChangeEvent(true, ConfigChangeType.RemoveSection, section, null, null))
				return;
			
			if (WritePrivateProfileString(section, 0, "", Name) == 0)
				throw new Win32Exception();

			RaiseChangeEvent(false, ConfigChangeType.RemoveSection, section, null, null);
		}

		/// <summary>
		///   Retrieves the names of all the entries inside a section. </summary>
		/// <param name="section">
		///   The name of the section holding the entries. </param>
		/// <returns>
		///   If the section exists, the return value is an array with the names of its entries; 
		///   otherwise it's null. </returns>
		public override string[] GetEntryNames(string section)
		{
			// Verify the section exists
			if (!HasSection(section))
				return null;

			VerifyAndAdjustSection(ref section);
			    
			// Loop until the buffer has grown enough to fit the value
			for (int maxSize = 500; true; maxSize *= 2)
			{
				byte[] bytes = new byte[maxSize];				
            	int size = GetPrivateProfileString(section, 0, "", bytes, maxSize, Name);
				
				if (size < maxSize - 2)
				{
					// Convert the buffer to a string and split it
					string entries = System.Text.Encoding.ASCII.GetString(bytes, 0, size - (size > 0 ? 1 : 0));			
					if (entries == "")
						return new string[0];
		            return entries.Split(new char[] {'\0'});			
				}
			}
		}

		/// <summary>
		///   Retrieves the names of all the sections. </summary>
		/// <returns>
		///   If the INI file exists, the return value is an array with the names of all the sections;
		///   otherwise it's null. </returns>
		/// <seealso cref="Config.HasSection" />
		/// <seealso cref="GetEntryNames" />
		public override string[] GetSectionNames()
		{
			// Verify the file exists
			if (!File.Exists(Name))
				return null;
			
			// Loop until the buffer has grown enough to fit the value
			for (int maxSize = 500; true; maxSize *= 2)
			{
				byte[] bytes = new byte[maxSize];				
            	int size = GetPrivateProfileString(0, "", "", bytes, maxSize, Name);
				
				if (size < maxSize - 2)
				{
					// Convert the buffer to a string and split it
					string sections = System.Text.Encoding.ASCII.GetString(bytes, 0, size - (size > 0 ? 1 : 0));			
					if (sections == "")
						return new string[0];
		            return sections.Split(new char[] {'\0'});			
				}
			}
		}
	}
}
