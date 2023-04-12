using System;
using System.Configuration;

namespace MControl.Web.Configuration
{



	/// <summary>
	/// Configuration for caching
    /// <example>
    /// <!-- MControl - CachingHandler -->
    ///	<sectionGroup name="MControl">
    ///		<section name="Caching" requirePermission="false" type="MControl.Web.Configuration.CachingSection, MControl.Web.CachingHandler" />
    ///	</sectionGroup>
    ///	<httpHandlers>
    ///		<!-- MControl - CachingHandler -->
    ///		<add verb="*" path="*.gif.ashx" type="MControl.Web.CachingHandler, MControl.Web.CachingHandler"/>
    ///		<add verb="*" path="*.jpg.ashx" type="MControl.Web.CachingHandler, MControl.Web.CachingHandler"/>
    ///		<add verb="*" path="*.png.ashx" type="MControl.Web.CachingHandler, MControl.Web.CachingHandler"/>
    ///	</httpHandlers>
    ///	<!-- MControl - CachingHandler -->
    ///<MControl>
	///	<Caching CachingTimeSpan="1">
	///		<FileExtensions>
	///			<clear />
	///			<add Extension="gif" ContentType="image\gif" />
	///			<add Extension="jpg" ContentType="image\jpeg" />
	///			<add Extension="png" ContentType="image\png" />
	///		</FileExtensions>
	///	</Caching>
    ///</MControl>
    /// </example>
	/// </summary>
	public class CachingSection : ConfigurationSection
	{
		[ConfigurationProperty("CachingTimeSpan", IsRequired = true)]
		public TimeSpan CachingTimeSpan 
		{
			get { return (TimeSpan)base["CachingTimeSpan"]; }
			set { base["CachingTimeSpan"] = value; }
		}

		[ConfigurationProperty("FileExtensions", IsDefaultCollection = true, IsRequired = true)]
		public FileExtensionCollection FileExtensions 
		{
			get { return ((FileExtensionCollection)base["FileExtensions"]); }
		}
	}

	/// <summary>
	/// List of available file extensions
	/// </summary>
	public class FileExtensionCollection : ConfigurationElementCollection
	{
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.AddRemoveClearMap;
			}
		}

		public FileExtension this[int index]
		{
			get { return (FileExtension)BaseGet(index); }
			set
			{
				if (BaseGet(index) != null)
				{
					BaseRemoveAt(index);
				}
				BaseAdd(index, value);
			}
		}

		public new FileExtension this[string extension]
		{
			get { return (FileExtension)BaseGet(extension); }
			set
			{
				if (BaseGet(extension) != null)
				{
					BaseRemove(extension);
				}
				BaseAdd(value);
			}
		}

		public void Add(FileExtension element)
		{
			BaseAdd(element);
		}

		public void Clear()
		{
			BaseClear();
		}

		protected override ConfigurationElement CreateNewElement()
		{
			return new FileExtension();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((FileExtension)element).Extension;
		}

		public void Remove(FileExtension element)
		{
			BaseRemove(element.Extension);
		}

		public void Remove(string name)
		{
			BaseRemove(name);
		}

		public void RemoveAt(int index)
		{
			BaseRemoveAt(index);
		}
	}

	/// <summary>
	/// Configuration for a file extension
	/// </summary>
	public class FileExtension : ConfigurationElement
	{
		[ConfigurationProperty("Extension", IsRequired = true)]
		public string Extension
		{
			get { return (string)base["Extension"]; }
			set { base["Extension"] = value.Replace(".", ""); }
		}

		[ConfigurationProperty("ContentType", IsRequired = true)]
		public string ContentType
		{
			get { return (string)base["ContentType"]; }
			set { base["ContentType"] = value; }
		}
	}
}
