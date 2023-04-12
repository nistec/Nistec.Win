
using System;
using System.Data;
                       
//[assembly:CLSCompliant(true)]
namespace MControl.Configuration
{	
	/// <summary>
	///   Base interface for all Config classes in this namespace.</summary>
	public interface IConfigReadOnly : ICloneable
	{
		/// <summary>
		///   Gets the name associated with the Config. </summary>
		/// <remarks>
		///   This should be the name of the file where the data is stored, or something equivalent. </remarks>
		string Name
		{
			get; 
		}

		/// <summary>
		///   Retrieves the value of an entry inside a section. </summary>
		/// <param name="section">
		///   The name of the section that holds the entry with the value. </param>
		/// <param name="entry">
		///   The name of the entry where the value is stored. </param>
		/// <returns>
		///   The return value should be the entry's value, or null if the entry does not exist. </returns>
		/// <seealso cref="HasEntry" />
		object GetValue(string section, string entry);
		
		/// <summary>
		///   Retrieves the value of an entry inside a section, or a default value if the entry does not exist. </summary>
		/// <param name="section">
		///   The name of the section that holds the entry with the value. </param>
		/// <param name="entry">
		///   The name of the entry where the value is stored. </param>
		/// <param name="defaultValue">
		///   The value to return if the entry (or section) does not exist. </param>
		/// <returns>
		///   The return value converted to a string, If the value
		///   cannot be converted, or the entry does not exist, the
		///   given default value should be returned. </returns>
		/// <seealso cref="HasEntry" />
		string GetValue(string section, string entry, string defaultValue);
		
		/// <summary>
		///   Retrieves the value of an entry inside a section, or a default value if the entry does not exist. </summary>
		/// <param name="section">
		///   The name of the section that holds the entry with the value. </param>
		/// <param name="entry">
		///   The name of the entry where the value is stored. </param>
		/// <param name="defaultValue">
		///   The value to return if the entry (or section) does not exist. </param>
		/// <returns>
		///   The return value converted to an integer.  If the value
		///   cannot be converted, or the entry does not exist, the
		///   given default value should be returned. </returns>
		/// <seealso cref="HasEntry" />
		int GetValue(string section, string entry, int defaultValue);

		/// <summary>
		///   Retrieves the value of an entry inside a section, or a default value if the entry does not exist. </summary>
		/// <param name="section">
		///   The name of the section that holds the entry with the value. </param>
		/// <param name="entry">
		///   The name of the entry where the value is stored. </param>
		/// <param name="defaultValue">
		///   The value to return if the entry (or section) does not exist. </param>
		/// <returns>
		///   The return value converted to a double.  If the value
		///   cannot be converted, or the entry does not exist, the
		///   given default value should be returned. </returns>
		/// <seealso cref="HasEntry" />
		double GetValue(string section, string entry, double defaultValue);

		/// <summary>
		///   Retrieves the value of an entry inside a section, or a default value if the entry does not exist. </summary>
		/// <param name="section">
		///   The name of the section that holds the entry with the value. </param>
		/// <param name="entry">
		///   The name of the entry where the value is stored. </param>
		/// <param name="defaultValue">
		///   The value to return if the entry (or section) does not exist. </param>
		/// <returns>
		///   The return value converted to a bool.  If the value
		///   cannot be converted, or the entry does not exist, the
		///   given default value should be returned. </returns>
		/// <remarks>
		///   Note: Boolean values are stored as "True" or "False". </remarks>
		/// <seealso cref="HasEntry" />
		bool GetValue(string section, string entry, bool defaultValue);

		/// <summary>
		///   Determines if an entry exists inside a section. </summary>
		/// <param name="section">
		///   The name of the section that holds the entry. </param>
		/// <param name="entry">
		///   The name of the entry to be checked for existence. </param>
		/// <returns>
		///   If the entry exists inside the section, the return value should be true; otherwise false. </returns>
		/// <seealso cref="HasSection" />
		/// <seealso cref="GetEntryNames" />
		bool HasEntry(string section, string entry);

		/// <summary>
		///   Determines if a section exists. </summary>
		/// <param name="section">
		///   The name of the section to be checked for existence. </param>
		/// <returns>
		///   If the section exists, the return value should be true; otherwise false. </returns>
		/// <seealso cref="HasEntry" />
		/// <seealso cref="GetSectionNames" />
		bool HasSection(string section);

		/// <summary>
		///   Retrieves the names of all the entries inside a section. </summary>
		/// <param name="section">
		///   The name of the section holding the entries. </param>
		/// <returns>
		///   If the section exists, the return value should be an array with the names of its entries; 
		///   otherwise it should be null. </returns>
		/// <seealso cref="HasEntry" />
		/// <seealso cref="GetSectionNames" />
		string[] GetEntryNames(string section);

		/// <summary>
		///   Retrieves the names of all the sections. </summary>
		/// <returns>
		///   The return value should be an array with the names of all the sections. </returns>
		/// <seealso cref="HasSection" />
		/// <seealso cref="GetEntryNames" />
		string[] GetSectionNames();

		/// <summary>
		///   Retrieves a DataSet object containing every section, entry, and value in the Config. </summary>
		/// <returns>
		///   If the Config exists, the return value should be a DataSet object representing the Config; otherwise it's null. </returns>
		/// <remarks>
		///   The returned DataSet should be named using the <see cref="Name" /> property.  
		///   It should contain one table for each section, and each entry should be represented by a column inside the table.
		///   Each table should contain only one row where the values will be stored corresponding to each column (entry). 
		/// </remarks>
		DataSet ConfigToDataSet();
	}

	/// <summary>
	///   Interface implemented by all Config classes in this namespace.
	///   It represents a normal Config. </summary>
	/// <seealso cref="IConfigReadOnly" />
	/// <seealso cref="Config" />
	public interface IConfig : IConfigReadOnly
	{
		/// <summary>
		///   Gets or sets the name associated with the Config. </summary>
		/// <remarks>
		///   This should be the name of the file where the data is stored, or something equivalent.
		///   When setting this property, the <see cref="ReadOnly" /> property should be checked and if true, an InvalidOperationException should be raised.
		///   The <see cref="Changing" /> and <see cref="Changed" /> events should be raised before and after this property is changed. </remarks>
		/// <seealso cref="DefaultName" />
		new string Name
		{
			get; 
			set;
		}

		/// <summary>
		///   Gets the name associated with the Config by default. </summary>
		/// <remarks>
		///   This is used to set the default Name of the Config and it is typically based on 
		///   the name of the executable plus some extension. </remarks>
		/// <seealso cref="Name" />
		string DefaultName
		{
			get;
		}

		/// <summary>
		///   Gets or sets whether the Config is read-only or not. </summary>
		/// <remarks>
		///   A read-only Config should not allow any operations that alter sections,
		///   entries, or values, such as <see cref="SetValue" /> or <see cref="RemoveEntry" />.  
		///   Once a Config has been marked read-only.</remarks>
		/// <seealso cref="CloneReadOnly" />
		/// <seealso cref="IConfigReadOnly" />
		bool ReadOnly
		{
			get; 
			set;
		}		
	
		/// <summary>
		///   Sets the value for an entry inside a section. </summary>
		/// <param name="section">
		///   The name of the section that holds the entry. </param>
		/// <param name="entry">
		///   The name of the entry where the value will be set. </param>
		/// <param name="value">
		///   The value to set. If it's null, the entry should be removed. </param>
		/// <remarks>
		///   This method should check the <see cref="ReadOnly" /> property and throw an InvalidOperationException if it's true.
		///   It should also raise the <see cref="Changing" /> and <see cref="Changed" /> events before and after the value is set. </remarks>
		/// <seealso cref="IConfigReadOnly.GetValue" />
		void SetValue(string section, string entry, object value);
		
		/// <summary>
		///   Removes an entry from a section. </summary>
		/// <param name="section">
		///   The name of the section that holds the entry. </param>
		/// <param name="entry">
		///   The name of the entry to remove. </param>
		/// <remarks>
		///   This method should check the <see cref="ReadOnly" /> property and throw an InvalidOperationException if it's true.
		///   It should also raise the <see cref="Changing" /> and <see cref="Changed" /> events before and after the entry is removed. </remarks>
		/// <seealso cref="RemoveSection" />
		void RemoveEntry(string section, string entry);

		/// <summary>
		///   Removes a section. </summary>
		/// <param name="section">
		///   The name of the section to remove. </param>
		/// <remarks>
		///   This method should check the <see cref="ReadOnly" /> property and throw an InvalidOperationException if it's true.
		///   It should also raise the <see cref="Changing" /> and <see cref="Changed" /> events before and after the section is removed. </remarks>
		/// <seealso cref="RemoveEntry" />
		void RemoveSection(string section);
		
		/// <summary>
		///   Writes the data of every table from a DataSet into this Config. </summary>
		/// <param name="ds">
		///   The DataSet object containing the data to be set. </param>
		/// <remarks>
		///   Each table in the DataSet should be used to represent a section of the Config.  
		///   Each column of each table should represent an entry.  And for each column, the corresponding value
		///   of the first row is the value that should be passed to <see cref="SetValue" />.  
		///</remarks>
		/// <seealso cref="IConfigReadOnly.ConfigToDataSet" />
		void DataSetToConfig(DataSet ds);
		
		/// <summary>
		///   Creates a copy of itself and makes it read-only. </summary>
		/// <returns>
		///   The return value should be a copy of itself as an IConfigReadOnly object. </returns>
		/// <seealso cref="ReadOnly" />
		IConfigReadOnly CloneReadOnly();
		
		/// <summary>
		///   Event that should be raised just before the Config is to be changed to allow the change to be canceled. </summary>
		/// <seealso cref="Changed" />
		event ConfigChangingHandler Changing;

		/// <summary>
		///   Event that should be raised right after the Config has been changed. </summary>
		/// <seealso cref="Changing" />
		event ConfigChangedHandler Changed;				
	}
}

