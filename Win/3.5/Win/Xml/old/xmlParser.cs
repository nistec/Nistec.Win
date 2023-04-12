using System;
using System.Data;
using System.IO;
using System.Xml;


namespace MControl.Xml
{
	/// <summary>
	/// Summary description for xmlParser.
	/// </summary>
	public class xmlParser
	{

		public xmlParser()
		{
			//
		}

		public  DataSet ReadXmlFile(string file) 
		{
			try 
			{
				System.Data.DataSet DSet = new DataSet();
				DSet.ReadXml(file,XmlReadMode.Auto);
				return DSet;  
			}
			catch(ApplicationException ex)
			{
              throw  new ApplicationException(ex.Message);
			}
		}

		public DataSet ReadXmlStream(string s)
		{
			try 
			{
				StringReader stream=new StringReader(s);
				DataSet DSet = new DataSet();
				XmlTextReader reader = new XmlTextReader (stream);
				DSet.ReadXml(reader,XmlReadMode.Auto);
				
				return DSet;  
			}
			catch(ApplicationException ex)
			{
				throw  new ApplicationException(ex.Message);
			}
		}

	}
}
