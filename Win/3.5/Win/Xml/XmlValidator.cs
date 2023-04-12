using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Schema;

namespace MControl.Xml
{
    public class XmlValidator
    {
        public XmlValidator(XmlReaderSettings settings)
        {
            this.settings = settings;        
        }
        /// <summary>
        /// Ctor using default settings
        /// </summary>
        public XmlValidator()
        {
            settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            settings.IgnoreWhitespace = true;
            settings.ProhibitDtd = true;
            settings.ValidationType = ValidationType.DTD;
            //settings.ValidationEventHandler += new ValidationEventHandler(ValidationHandler);
        }

        XmlReaderSettings settings;
        StringBuilder errorMessages;
        bool isValid;
        string result;


        /// <summary>
        /// Get Validate result
        /// </summary>
        public string Result
        {
            get { return result; }
        }

 
        /// <summary>
        /// Validate xml 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public bool ValidateDTD(string url)
        {
            try
            {
                XmlTextReader tr = new XmlTextReader(url);

                return ValidateDTD(tr);
            }
            catch (Exception ex)
            {
                result = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Validate xml 
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="DTD"></param>
        /// <returns></returns>
        public bool ValidateDTD(string xml, string DTD)
        {
            try
            {
                if (xml.StartsWith("<?xml"))
                {
                    int startIndx = xml.IndexOf('<', 0);
                    int endIndx = xml.IndexOf('>', startIndx);
                    xml = xml.Remove(startIndx, 1 + endIndx - startIndx);
                }
                string xmlv = DTD + xml;

                XmlTextReader tr = new XmlTextReader(xmlv, XmlNodeType.Document, null);// ("HeadCount.xml");

                return ValidateDTD(tr);

            }
            catch (Exception ex)
            {
                result = ex.Message;
                return false;
            }
        }
        /// <summary>
        /// Validate xml 
        /// </summary>
        /// <param name="tr"></param>
        /// <returns></returns>
        public bool ValidateDTD(XmlTextReader tr)
        {
            errorMessages = new StringBuilder();
            isValid = true;
            try
            {

                XmlReader vr = XmlReader.Create(tr,settings);

                //XmlValidatingReader vr =XmlValidatingReader.Create(tr,);
                //vr.ValidationType = ValidationType.DTD;
                //vr.ValidationEventHandler += new ValidationEventHandler(ValidationHandler);

                while (vr.Read()) ;

                if (isValid)
                    result = "Validation finished";
                else
                    result = errorMessages.ToString();

            }
            catch (Exception ex)
            {
                result = ex.Message;
                isValid = false;
            }
            return isValid;

        }

        void ValidationHandler(object sender, ValidationEventArgs args)
        {
            isValid = false;
            errorMessages.Append(args.Message);

            //Console.WriteLine("***Validation error");
            //Console.WriteLine("\tSeverity:{0}", args.Severity);
            //Console.WriteLine("\tMessage  :{0}", args.Message);

        }

    }
}
