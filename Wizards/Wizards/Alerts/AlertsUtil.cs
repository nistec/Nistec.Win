using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using System.Web;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using System.Threading;
using System.Data;
using System.Reflection;
using MControl.WinUI.DataAccess;
using MControl.Data;

namespace MControl.Alerts
{

    public class AlertConfig
    {
    public string SmsTo;
    public string SmsMessage;
    public string IPAddress;
    public string MailAddress;
    public string SmsLang;
    public string SmsBillingCode;
    public string SmsCategory;
    public string SmsEncode;
    public string SmsGateway;
    public int SmsMaxDidits;
    public string SmsPassword;
    public string SmsSchema;
    public string SmsSender;
    public int SmsSigments;
    public string SmsType;
    public string SmsUser;
    public string SmtpServer;
    }

	[ComVisible(true)]
	public class AlertsUtil
	{
        private static AlertConfig config;
        private static IDalBase _IDalBase;

        public static void Init(IDalBase dalBase)
		{
            _IDalBase = dalBase;
            AlertsUtil.CreateConfig();
		}

        private static void CreateConfig()
        {
            DalAlerts dal = new DalAlerts(_IDalBase);
            DataTable dt = dal.Alert_Config();
            MControl.Data.ActiveConfig aconfig = new MControl.Data.ActiveConfig(dt);
            config = new AlertConfig();

            config.IPAddress = aconfig.GetStringValue("IPAddress","MC");
            config.MailAddress = aconfig.GetStringValue("MailAddress", "MC");
            config.SmsLang = aconfig.GetStringValue("SmsLang", "MC");
            config.SmsBillingCode = aconfig.GetStringValue("SmsBillingCode", "MC");
            config.SmsCategory = aconfig.GetStringValue("SmsCategory", "MC");
            config.SmsEncode = aconfig.GetStringValue("SmsEncode", "MC");
            config.SmsGateway = aconfig.GetStringValue("SmsGateway", "MC");
            config.SmsMaxDidits = aconfig.GetValue("SmsMaxDidits","MC", (int)70);
            config.SmsPassword = aconfig.GetStringValue("SmsPassword","MC");
            config.SmsSchema = aconfig.GetStringValue("SmsSchema","MC");
            config.SmsSender = aconfig.GetStringValue("SmsSender", "MC");
            config.SmsSigments = aconfig.GetValue("SmsSigments", "MC", (int)1);
            config.SmsType = aconfig.GetStringValue("SmsType", "MC");
            config.SmsUser = aconfig.GetStringValue("SmsUser", "MC");
            config.SmtpServer = aconfig.GetStringValue("SmtpServer", "MC");
        }

        public static void DBSave(string Namespace, ExceptionType type, AlertPriority lvl, string orderId,string msg)
		{
			MethodBase method = (MethodBase) (new System.Diagnostics.StackTrace().GetFrame(1).GetMethod());
            DalAlerts cmd = new DalAlerts(_IDalBase);
            cmd.Alert_Exeptions(Namespace,msg, (int)lvl, method.Name,orderId);
		}

        public static void DBSave(string Namespace, ExceptionType type, AlertPriority lvl, string msg)
        {
            MethodBase method = (MethodBase)(new System.Diagnostics.StackTrace().GetFrame(1).GetMethod());
            DalAlerts cmd = new DalAlerts(_IDalBase);
            cmd.Alert_Exeptions(Namespace, msg, (int)lvl, method.Name, null);
        }

 
		public static void SendMail(string Server , string FromAddress, string ToAddress,string message,string attachmentFile, string Subject)
		{
			try
			{
                MControl.Web.MailClient mail = new MControl.Web.MailClient(FromAddress,ToAddress,Subject,message);
                mail.Send();
  			
			}
			catch (Exception ex)
			{
				throw ex;

			}
		}

        public static void SendMail(string ToAddress, string message, string Subject)
        {
            try
            {
                string FromAddress = "";
                MControl.Web.MailClient mail = new MControl.Web.MailClient(FromAddress,ToAddress, Subject, message);
                mail.Send();

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }


        public static string SendSMS(string To, string message)
        {
            try
            {
                MControl.Web.HttpUtil req = new MControl.Web.HttpUtil(config.SmsGateway);
                string smsMessage = config.SmsSchema;

                if (!string.IsNullOrEmpty(config.SmsTo) && smsMessage.Contains("{SmsTo}"))
                    smsMessage = smsMessage.Replace("{SmsTo}", To);
                else
                    throw new Exception("Invalid SmsTo");

                if (!string.IsNullOrEmpty(config.SmsMessage) && smsMessage.Contains("{SmsMessage}"))
                    smsMessage = smsMessage.Replace("{SmsMessage}", message);
                else
                    throw new Exception("Invalid SmsMessage");

                if (!string.IsNullOrEmpty(config.SmsBillingCode) && smsMessage.Contains("{SmsBillingCode}"))
                    smsMessage = smsMessage.Replace("{SmsBillingCode}", config.SmsBillingCode);
                if (!string.IsNullOrEmpty(config.SmsCategory) && smsMessage.Contains("{SmsCategory}"))
                    smsMessage = smsMessage.Replace("{SmsCategory}", config.SmsCategory);
                if (!string.IsNullOrEmpty(config.SmsEncode) && smsMessage.Contains("{SmsEncode}"))
                    smsMessage = smsMessage.Replace("{SmsEncode}", config.SmsEncode);
                if (!string.IsNullOrEmpty(config.SmsPassword) && smsMessage.Contains("{SmsPassword}"))
                    smsMessage = smsMessage.Replace("{SmsPassword}", config.SmsPassword);
                if (!string.IsNullOrEmpty(config.SmsSender) && smsMessage.Contains("{SmsSender}"))
                    smsMessage = smsMessage.Replace("{SmsSender}", config.SmsSender);
                if (!string.IsNullOrEmpty(config.SmsType) && smsMessage.Contains("{SmsType}"))
                    smsMessage = smsMessage.Replace("{SmsType}", config.SmsType);
                if (!string.IsNullOrEmpty(config.SmsUser) && smsMessage.Contains("{SmsUser}"))
                    smsMessage = smsMessage.Replace("{SmsUser}", config.SmsUser);
                if (!string.IsNullOrEmpty(config.SmsLang) && smsMessage.Contains("{SmaLang}"))
                    smsMessage = smsMessage.Replace("{SmaLang}", config.SmsLang);
                //if (!string.IsNullOrEmpty(config.SmsMaxDidits))
                //    smsMessage = smsMessage.Replace("{SmsMaxDidits}", config.SmsMaxDidits.ToString());
                //if (!string.IsNullOrEmpty(config.SmsSigments))
                //    smsMessage = smsMessage.Replace("{SmsSigments}", config.SmsSigments.ToString());

                return req.AsyncRequest(smsMessage);

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

       
        public static void SendHttp(string url,string message)
        {
            try
            {
                MControl.Web.HttpUtil req = new MControl.Web.HttpUtil(url);
                req.AsyncRequest(message);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

		

	}

}

