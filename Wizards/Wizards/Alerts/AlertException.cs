using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using System.Reflection;
using MControl.Data;

namespace MControl.Alerts
{

	/// <summary>
    /// MControl AlertException
	/// </summary>
    [ComVisible(true)]
    [Serializable]
    public class AlertException : ApplicationException
    {
 
        public AlertException(string Namespace, string msg)
            : base(msg)
        {
            OnException(Namespace, ExceptionType.NonSpecified, AlertPriority.Normal, null, msg);
        }

        public AlertException(string Namespace, ExceptionType type, string orderId, string msg, AlertPriority exp)
            : base(msg)
        {
            OnException(Namespace, type, exp, orderId, msg);
        }

        public AlertException(string Namespace, ExceptionType type, string msg, AlertPriority exp)
            : base(msg)
        {
            OnException(Namespace, type, exp, null, msg);
        }

        protected virtual void OnException(string Namespace, ExceptionType type, AlertPriority exp, string orderId, string message)
        {
            

        }

        protected virtual void OnException(IDalBase dalBase, string Namespace, ExceptionType type, AlertPriority exp, string orderId, string message)
        {
            DBSave(Namespace, type, exp, orderId, message);

            ActiveAlertException active = new ActiveAlertException(dalBase, (int)type);
            if (!active.IsEmpty)
                return;

            for (int i = 0; i < active.Count; i++)
            {
                active.CurrentIndex = i;
                AlertTypes alertType = (AlertTypes)active.AlertType;
                if (alertType == AlertTypes.None)
                {
                    continue;
                }
                if (alertType == AlertTypes.Mail | alertType == AlertTypes.MailAndSms | alertType == AlertTypes.SiteAndMail | alertType == AlertTypes.AllAlarms)
                {
                    string mail = active.MailAddress;
                    if (!string.IsNullOrEmpty(mail))
                    {
                        OnSendMail(mail, message, type.ToString());
                    }
                }
                if (alertType == AlertTypes.Sms | alertType == AlertTypes.MailAndSms | alertType == AlertTypes.AllAlarms)
                {
                    string phone = active.Phone;
                    if (!string.IsNullOrEmpty(phone))
                    {
                        OnSendSMS(phone, "Exception: " + type.ToString() + " Message: " + message);
                    }
                }
                if (alertType == AlertTypes.Site | alertType == AlertTypes.SiteAndMail | alertType == AlertTypes.AllAlarms)
                {
                    string url = active.Url;
                    if (!string.IsNullOrEmpty(url))
                    {
                        OnSendHttp(url, "Exception: " + type.ToString() + " Message: " + message);
                    }
                }
            }
        }

        private void DBSave(string Namespace, ExceptionType type, AlertPriority priority, string orderId, string msg)
        {
            MethodBase method = (MethodBase)(new System.Diagnostics.StackTrace().GetFrame(1).GetMethod());
            OnDBSave(Namespace, type, priority, orderId, msg, method.Name);
        }

        protected virtual void OnDBSave(string Namespace, ExceptionType type, AlertPriority priority,string orderId, string msg, string method)
        {

        }

        protected virtual void OnSendMail(string ToAddress, string message, string Subject)
        {
        }

        protected virtual void OnSendSMS(string To, string message)
        {

        }

        protected virtual void OnSendHttp(string url, string message)
        {

        }
    }
}

