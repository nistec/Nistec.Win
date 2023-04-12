using System;
using System.Data;
using System.Reflection;

namespace MControl.Configuration
{		
	/// <summary>
	///   Abstract base class for all Config classes in this namespace. </summary>
	/// <remarks>
	///   This class contains fields and methods which are common for all the derived Config classes. 
	public abstract class Config : IConfig
	{
		// Fields
		private string m_name;
		private bool m_readOnly;
		
		/// <summary>
		///   Event used to notify that the Config is about to be changed. </summary>
		/// <seealso cref="Changed" />
		public event ConfigChangingHandler Changing;

		/// <summary>
		///   Event used to notify that the Config has been changed. </summary>
		/// <seealso cref="Changing" />
		public event ConfigChangedHandler Changed;				
		
		/// <summary>
		///   Initializes a new instance of the Config class by setting the <see cref="Name" /> to <see cref="DefaultName" />. </summary>
		protected Config()
		{			
			m_name = DefaultName;
		}
		
		/// <summary>
		///   Initializes a new instance of the Config class by setting the <see cref="Name" /> to a value. </summary>
		/// <param name="name">
		///   The name to initialize the <see cref="Name" /> property with. </param>
		protected Config(string name)
		{			
			m_name = name;
		}
		
		/// <summary>
		///   Initializes a new instance of the Config class based on another Config object. </summary>
		/// <param name="Config">
		///   The Config object whose properties and events are used to initialize the object being constructed. </param>
		protected Config(Config Config)
		{			
			m_name = Config.m_name;
			m_readOnly = Config.m_readOnly;			
			Changing = Config.Changing;
			Changed = Config.Changed;
		}
		
		/// <summary>
		///   Gets or sets the name associated with the Config. </summary>
		/// <exception cref="NullReferenceException">
		///   Setting this property to null. </exception>
		/// <exception cref="InvalidOperationException">
		///   Setting this property if ReadOnly is true. </exception>
		public string Name
		{
			get 
			{ 
				return m_name; 
			}
			set 
			{ 
				VerifyNotReadOnly();	
				if (m_name == value.Trim())
					return;
					
				if (!RaiseChangeEvent(true, ConfigChangeType.Name, null, null, value))
					return;
							
				m_name = value.Trim();
				RaiseChangeEvent(false, ConfigChangeType.Name, null, null, value);
			}
		}

		/// <summary>
		///   Gets or sets whether the Config is read-only or not. </summary>
		/// <exception cref="InvalidOperationException">
		///   Setting this property if it's already true. </exception>
		public bool ReadOnly
		{
			get 
			{ 
				return m_readOnly; 
			}
			
			set
			{ 
				VerifyNotReadOnly();
				if (m_readOnly == value)
					return;
				
				if (!RaiseChangeEvent(true, ConfigChangeType.ReadOnly, null, null, value))
					return;
							
				m_readOnly = value;
				RaiseChangeEvent(false, ConfigChangeType.ReadOnly, null, null, value);
			}
		}

		/// <summary>
		///   Gets the name associated with the Config by default. </summary>
		public abstract string DefaultName
		{
			get;
		}

		/// <summary>
		///   Retrieves a copy of itself. </summary>
		/// <returns>
		///   The return value is a copy of itself as an object. </returns>
		public abstract object Clone();

		/// <summary>
		///   Sets the value for an entry inside a section. </summary>
		/// <param name="section">
		///   The name of the section that holds the entry. </param>
		/// <param name="entry">
		///   The name of the entry where the value will be set. </param>
		/// <param name="value">
		///   The value to set. If it's null, the entry should be removed. </param>
		public abstract void SetValue(string section, string entry, object value);
		
		/// <summary>
		///   Retrieves the value of an entry inside a section. </summary>
		/// <param name="section">
		///   The name of the section that holds the entry with the value. </param>
		/// <param name="entry">
		///   The name of the entry where the value is stored. </param>
		/// <returns>
		///   The return value is the entry's value, or null if the entry does not exist. </returns>
		public abstract object GetValue(string section, string entry);

		/// <summary>
		///   Retrieves the string value of an entry inside a section, or a default value if the entry does not exist. </summary>
		/// <param name="section">
		///   The name of the section that holds the entry with the value. </param>
		/// <param name="entry">
		///   The name of the entry where the value is stored. </param>
		/// <param name="defaultValue">
		///   The value to return if the entry (or section) does not exist. </param>
		/// <returns>
		///   The return value is the entry's value converted to a string, or the given default value if the entry does not exist. </returns>
		public virtual string GetValue(string section, string entry, string defaultValue)
		{
			object value = GetValue(section, entry);
			return (value == null ? defaultValue : value.ToString());
		}

		/// <summary>
		///   Retrieves the integer value of an entry inside a section, or a default value if the entry does not exist. </summary>
		/// <param name="section">
		///   The name of the section that holds the entry with the value. </param>
		/// <param name="entry">
		///   The name of the entry where the value is stored. </param>
		/// <param name="defaultValue">
		///   The value to return if the entry (or section) does not exist. </param>
		/// <returns>
		///   The return value is the entry's value converted to an integer.  If the value
		///   cannot be converted, or entry does not exist, the
		///   given default value is returned. </returns>
		public virtual int GetValue(string section, string entry, int defaultValue)
		{
			object value = GetValue(section, entry);

			return Types.ToInt(value,defaultValue);

		}

		/// <summary>
		///   Retrieves the double value of an entry inside a section, or a default value if the entry does not exist. </summary>
		/// <param name="section">
		///   The name of the section that holds the entry with the value. </param>
		/// <param name="entry">
		///   The name of the entry where the value is stored. </param>
		/// <param name="defaultValue">
		///   The value to return if the entry (or section) does not exist. </param>
		/// <returns>
		///   The return value is the entry's value converted to a double.  If the value
		///   cannot be converted, or entry does not exist, the
		///   given default value is returned. </returns>
		public virtual double GetValue(string section, string entry, double defaultValue)
		{
			object value = GetValue(section, entry);

			return Types.ToDouble(value,defaultValue);

		}

		/// <summary>
		///   Retrieves the bool value of an entry inside a section, or a default value if the entry does not exist. </summary>
		/// <param name="section">
		///   The name of the section that holds the entry with the value. </param>
		/// <param name="entry">
		///   The name of the entry where the value is stored. </param>
		/// <param name="defaultValue">
		///   The value to return if the entry (or section) does not exist. </param>
		/// <returns>
		///   The return value is the entry's value converted to a bool.  If the value
		///   cannot be converted, or entry does not exist, the
		///   given default value is returned. </returns>
		public virtual bool GetValue(string section, string entry, bool defaultValue)
		{
			object value = GetValue(section, entry);
			return Types.ToBool(value,defaultValue);

		}

		/// <summary>
		///   Determines if an entry exists inside a section. </summary>
		/// <param name="section">
		///   The name of the section that holds the entry. </param>
		/// <param name="entry">
		///   The name of the entry to be checked for existence. </param>
		/// <returns>
		///   If the entry exists inside the section, the return value is true; otherwise false. </returns>
		/// <exception cref="ArgumentNullException">
		///   section is null. </exception>
		public virtual bool HasEntry(string section, string entry)
		{
			string[] entries = GetEntryNames(section);
			
			if (entries == null)
				return false;

			VerifyAndAdjustEntry(ref entry);
			return Array.IndexOf(entries, entry) >= 0;
		}

		/// <summary>
		///   Determines if a section exists. </summary>
		/// <param name="section">
		///   The name of the section to be checked for existence. </param>
		/// <returns>
		///   If the section exists, the return value is true; otherwise false. </returns>
		public virtual bool HasSection(string section)
		{
			string[] sections = GetSectionNames();

			if (sections == null)
				return false;

			VerifyAndAdjustSection(ref section);
			return Array.IndexOf(sections, section) >= 0;
		}

		/// <summary>
		///   Removes an entry from a section. </summary>
		/// <param name="section">
		///   The name of the section that holds the entry. </param>
		/// <param name="entry">
		///   The name of the entry to remove. </param>
		/// <exception cref="InvalidOperationException">
		///   <see cref="Config.ReadOnly" /> is true. </exception>
		/// <exception cref="ArgumentNullException">
		///   Either section or entry is null. </exception>
		public abstract void RemoveEntry(string section, string entry);

		/// <summary>
		///   Removes a section. </summary>
		/// <param name="section">
		///   The name of the section to remove. </param>
		/// <exception cref="InvalidOperationException">
		///   <see cref="Config.ReadOnly" /> is true. </exception>
		/// <exception cref="ArgumentNullException">
		///   section is null. </exception>
		public abstract void RemoveSection(string section);
		
		/// <summary>
		///   Retrieves the names of all the entries inside a section. </summary>
		/// <param name="section">
		///   The name of the section holding the entries. </param>
		/// <returns>
		///   If the section exists, the return value should be an array with the names of its entries; 
		///   otherwise null. </returns>
		/// <exception cref="ArgumentNullException">
		///   section is null. </exception>
		public abstract string[] GetEntryNames(string section);

		/// <summary>
		///   Retrieves the names of all the sections. </summary>
		/// <returns>
		///   The return value should be an array with the names of all the sections. </returns>
		public abstract string[] GetSectionNames();
		
		/// <summary>
		///   Retrieves a copy of itself and makes it read-only. </summary>
		/// <returns>
		///   The return value is a copy of itself as a IConfigReadOnly object. </returns>
		public virtual IConfigReadOnly CloneReadOnly()
		{
			Config Config = (Config)Clone();
			Config.m_readOnly = true;
			
			return Config;
		}

		/// <summary>
		/// DataTable to Configuration file
		/// </summary>
		/// <param name="dt"></param>
		/// <param name="sectionFieldName"></param>
		/// <param name="entryFieldName"></param>
		/// <param name="valueFieldName"></param>
		public virtual void DataTableToConfig(DataTable dt,string sectionFieldName,string entryFieldName,string valueFieldName)
		{

			if (dt == null)
				return ;
	
			foreach (DataRow dr in dt.Rows)
			{
				SetValue(dr[sectionFieldName].ToString(),dr[entryFieldName].ToString(),dr[valueFieldName]);
			}

		}

		/// <summary>
		/// Configuration file to DataTable
		/// </summary>
		/// <param name="mappingName"></param>
		/// <param name="sectionFieldName"></param>
		/// <param name="entryFieldName"></param>
		/// <param name="valueFieldName"></param>
		/// <returns></returns>
		public virtual DataTable ConfigToDataTable(string mappingName, string sectionFieldName,string entryFieldName,string valueFieldName)
		{
			if (mappingName == null || mappingName=="")
				return null;

			VerifyName();
			
			string[] sections = GetSectionNames();
			if (sections == null)
				return null;
			
			DataTable dt=new DataTable(mappingName);
			dt.Columns.AddRange(new DataColumn[]{new DataColumn(sectionFieldName),new DataColumn(entryFieldName),new DataColumn(valueFieldName)});
			
			foreach (string section in sections)
			{
			
				string[] entries = GetEntryNames(section);
				DataColumn[] columns = new DataColumn[entries.Length];

				foreach (string entry in entries)
				{
					object value = GetValue(section, entry);
					dt.Rows.Add(new object[]{section,entry,value});
				}
			}
			
			return dt;

		}

		/// <summary>
		///   Retrieves a DataSet object containing every section, entry, and value in the Config. </summary>
		/// <returns>
		///   If the Config exists, the return value is a DataSet object representing the Config; otherwise it's null. </returns>
		/// <exception cref="InvalidOperationException">
		///	  <see cref="Config.Name" /> is null or empty. </exception>
		/// <remarks>
		///   The returned DataSet will be named using the <see cref="Name" /> property.  
		///   It will contain one table for each section, and each entry will be represented by a column inside the table.
		///   Each table will contain only one row where the values will stored corresponding to each column (entry). 
		///</remarks>
		public virtual DataSet ConfigToDataSet()
		{
			VerifyName();
			
			string[] sections = GetSectionNames();
			if (sections == null)
				return null;
			
			DataSet ds = new DataSet(Name);
			
			// Add a table for each section
			foreach (string section in sections)
			{
				DataTable table = ds.Tables.Add(section);
				
				// Retrieve the column names and values
				string[] entries = GetEntryNames(section);
				DataColumn[] columns = new DataColumn[entries.Length];
				object[] values = new object[entries.Length];								

				int i = 0;
				foreach (string entry in entries)
				{
					object value = GetValue(section, entry);
				
					columns[i] = new DataColumn(entry, value.GetType());
					values[i++] = value;
				}
												
				// Add the columns and values to the table
				table.Columns.AddRange(columns);
				table.Rows.Add(values);								
			}
			
			return ds;
		}
		
		/// <summary>
		///   Writes the data of every table from a DataSet into this Config. </summary>
		/// <param name="ds">
		///   The DataSet object containing the data to be set. </param>
		/// <exception cref="InvalidOperationException">
		///   <see cref="Config.ReadOnly" /> is true or
		///   <see cref="Config.Name" /> is null or empty. </exception>
		/// <exception cref="ArgumentNullException">
		///   ds is null. </exception>
		/// <remarks>
		///   Each table in the DataSet represents a section of the Config.  
		///   Each column of each table represents an entry.  And for each column, the corresponding value
		///   of the first row is the value to be passed to <see cref="SetValue" />.  
		///   Note that only the first row is imported; additional rows are ignored.
		///</remarks>
		public virtual void DataSetToConfig(DataSet ds)
		{
			if (ds == null)
				throw new ArgumentNullException("ds");
			
			// Create a section for each table
			foreach (DataTable table in ds.Tables)
			{
				string section = table.TableName;
				DataRowCollection rows = table.Rows;				
				if (rows.Count == 0)
					continue;

				// Loop through each column and add it as entry with value of the first row				
				foreach (DataColumn column in table.Columns)
				{
					string entry = column.ColumnName;
					object value = rows[0][column];
					
					SetValue(section, entry, value);
				}
			}
		}

		/// <summary>
		///   Gets the name of the file to be used as the default, without the Config-specific extension. </summary>
		/// <remarks>
		///   For Windows applications, this property returns the full path of the executable.  
		///   For Web applications, this returns the full path of the web.config file without 
		///   the .config extension.  </remarks>
		protected string DefaultNameWithoutExtension
		{
			get
			{
				try
				{
					string file = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
					return file.Substring(0, file.LastIndexOf('.'));
				}
				catch
				{
					return "Config";  // if all else fails
				}
			}
		}

		/// <summary>
		///   Verifies the given section name is not null and trims it. </summary>
		/// <param name="section">
		///   The section name to verify and adjust. </param>
		/// <exception cref="ArgumentNullException">
		///   section is null. </exception>
		/// <remarks>
		///   This method may be used by derived classes to make sure that a valid
		///   section name has been passed, and to make any necessary adjustments to it
		///   before passing it to the corresponding APIs. </remarks>
		protected virtual void VerifyAndAdjustSection(ref string section)
		{
			if (section == null)
				throw new ArgumentNullException("section");			
			
			section = section.Trim();
		}

		/// <summary>
		///   Verifies the given entry name is not null and trims it. </summary>
		/// <param name="entry">
		///   The entry name to verify and adjust. </param>
		/// <remarks>
		///   This method may be used by derived classes to make sure that a valid
		///   entry name has been passed, and to make any necessary adjustments to it
		///   before passing it to the corresponding APIs. </remarks>
		/// <exception cref="ArgumentNullException">
		///   entry is null. </exception>
		protected virtual void VerifyAndAdjustEntry(ref string entry)
		{
			if (entry == null)
				throw new ArgumentNullException("entry");			

			entry = entry.Trim();
		}
		
		/// <summary>
		///   Verifies the Name property is not empty or null. </summary>
		/// <remarks>
		///   This method may be used by derived classes to make sure that the 
		///   APIs are working with a valid Name (file name) </remarks>
		/// <exception cref="InvalidOperationException">
		///   name is empty or null. </exception>
		/// <seealso cref="Name" />
		protected internal virtual void VerifyName()
		{
			if (m_name == null || m_name == "")
				throw new InvalidOperationException("Operation not allowed because Name property is null or empty.");
		}

		/// <summary>
		///   Verifies the ReadOnly property is not true. </summary>
		/// <remarks>
		///   This method may be used by derived classes as a convenient way to 
		///   validate that modifications to the Config can be made. </remarks>
		/// <exception cref="InvalidOperationException">
		///   ReadOnly is true. </exception>
		/// <seealso cref="ReadOnly" />
		protected internal virtual void VerifyNotReadOnly()
		{
			if (m_readOnly)
				throw new InvalidOperationException("Operation not allowed because ReadOnly property is true.");			
		}
		
		/// <summary>
		///   Raises either the Changing or Changed event. </summary>
		/// <param name="changing">
		///   If true, the Changing event is raised otherwise it's Changed. </param>
		/// <param name="changeType">
		///   The type of change being made. </param>
		/// <param name="section">
		///   The name of the section that was involved in the change or null if not applicable. </param>
		/// <param name="entry">
		///   The name of the entry that was involved in the change or null if not applicable. 
		///   If changeType is equal to Other, entry is the name of the property involved in the change.</param>
		/// <param name="value">
		///   The value that was changed or null if not applicable. </param>
		/// <returns>
		///   The return value is based on the event raised.  If the Changing event was raised, 
		///   the return value is the opposite of ConfigChangingArgs.Cancel; otherwise it's true.</returns>
		/// <remarks>
		///   This method may be used by derived classes as a convenient alternative to calling 
		///   OnChanging and OnChanged.  For example, a typical call to OnChanging would require
		///   four lines of code, which this method reduces to two. </remarks>
		/// <seealso cref="Changing" />
		/// <seealso cref="Changed" />
		/// <seealso cref="OnChanging" />
		/// <seealso cref="OnChanged" />
		protected bool RaiseChangeEvent(bool changing, ConfigChangeType changeType, string section, string entry, object value)
		{
			if (changing)
			{
				// Don't even bother if there are no handlers.
				if (Changing == null)
					return true;

				ConfigChangingArgs e = new ConfigChangingArgs(changeType, section, entry, value);
				OnChanging(e);
				return !e.Cancel;
			}
			
			// Don't even bother if there are no handlers.
			if (Changed != null)
				OnChanged(new ConfigChangedArgs(changeType, section, entry, value));
			return true;
		}
		                          
		/// <summary>
		///   Raises the Changing event. </summary>
		/// <param name="e">
		///   The arguments object associated with the Changing event. </param>
		/// <remarks>
		///   This method should be invoked prior to making a change to the Config so that the
		///   Changing event is raised, giving a chance to the handlers to prevent the change from
		///   happening (by setting e.Cancel to true). This method calls each individual handler 
		///   associated with the Changing event and checks the resulting e.Cancel flag.  
		///   If it's true, it stops and does not call of any remaining handlers since the change 
		///   needs to be prevented anyway. </remarks>
		/// <seealso cref="Changing" />
		/// <seealso cref="OnChanged" />
		protected virtual void OnChanging(ConfigChangingArgs e)
		{
			if (Changing == null)
				return;

			foreach (ConfigChangingHandler handler in Changing.GetInvocationList())
			{
				handler(this, e);
				
				// If a particular handler cancels the event, stop
				if (e.Cancel)
					break;
			}
		}

		/// <summary>
		///   Raises the Changed event. </summary>
		/// <param name="e">
		///   The arguments object associated with the Changed event. </param>
		/// <remarks>
		///   This method should be invoked after a change to the Config has been made so that the
		///   Changed event is raised, giving a chance to the handlers to be notified of the change. </remarks>
		/// <seealso cref="Changed" />
		/// <seealso cref="OnChanging" />
		protected virtual void OnChanged(ConfigChangedArgs e)
		{
			if (Changed != null)
				Changed(this, e);
		}
		
	}	
}
