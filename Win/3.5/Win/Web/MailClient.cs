using System;
using System.Net;
using System.Text;
using System.IO;
using System.Web.Util;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net.Mime;
using System.Data;
using System.Collections;


namespace MControl.Web
{

  
    public class MailClient : System.Net.Mail.SmtpClient
    {

        private System.Net.Mail.MailMessage mailMessage;
        private AttchementCollection attachments;
        private bool mailSent = false;
         
        public MailMessage MailMessage
        {
            get { return mailMessage;}
        }

        public AttchementCollection Attachments
        {
            get 
            {
                if (attachments == null)
                    attachments = new AttchementCollection();
                return attachments; 
            }
        }

        public MailClient()
        {
            mailMessage = new System.Net.Mail.MailMessage();
        }

        public MailClient(string FromAddress, string ToAddress, string Subject)
        {
            mailMessage = new System.Net.Mail.MailMessage(FromAddress, ToAddress);
            mailMessage.Subject = Subject;
        }

        public MailClient(string FromAddress, string ToAddress, string Subject, string body)
            : this(FromAddress, ToAddress, Subject, body, false)
        {
            
        }

        public MailClient(string FromAddress, string ToAddress, string Subject, string body, bool isHtml)
        {
            try
            {
                mailMessage = new System.Net.Mail.MailMessage(FromAddress, ToAddress);
                mailMessage.Subject = Subject;
                mailMessage.Body = body;
                if (isHtml)
                {
                    SetBodyHtml();
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        public void SetBodyHtml(string body,string bodyEncoding, string subjectEncoding)
        {
            mailMessage.Body = body;
            mailMessage.BodyEncoding = Encoding.GetEncoding(bodyEncoding);
            mailMessage.SubjectEncoding = Encoding.GetEncoding(subjectEncoding);
            mailMessage.IsBodyHtml = true;
        }
        public void SetBodyHtml(string bodyEncoding, string subjectEncoding)
        {
            mailMessage.BodyEncoding = Encoding.GetEncoding(bodyEncoding);
            mailMessage.SubjectEncoding = Encoding.GetEncoding(subjectEncoding);
            mailMessage.IsBodyHtml = true;
        }
        public void SetBodyHtml()
        {
            mailMessage.BodyEncoding = Encoding.GetEncoding("utf-8");
            mailMessage.SubjectEncoding = Encoding.GetEncoding("utf-8");
            mailMessage.IsBodyHtml = true;
        }
        public void SetBodyHtml(string body)
        {
            mailMessage.Body = body;
            mailMessage.BodyEncoding = Encoding.GetEncoding("utf-8");
            mailMessage.SubjectEncoding = Encoding.GetEncoding("utf-8");
            mailMessage.IsBodyHtml = true;
        }
        public void Send(SmtpDeliveryMethod deliveryMethod)
        {
            base.DeliveryMethod = deliveryMethod;
            Send();
        }

        public void SendFromIis()
        {
            base.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.PickupDirectoryFromIis;
            Send();
        }

        public void SendFromNetwork()
        {
            base.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            Send();
        }

        public void Send()
        {
            try
            {

                if (mailMessage == null)
                {
                    throw new Exception("Invalid Mail message");
                }
                if (attachments != null)
                {
                     foreach (ATTACHMENT ma in Attachments)
                    {
                        mailMessage.Attachments.Add(ma.attachment);
                    }
                }
                if (base.Host == null || base.Host == "")
                {
                    base.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.PickupDirectoryFromIis;
                }
                // Add credentials if the SMTP server requires them.
                base.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                base.Send(mailMessage);
                if (attachments != null)
                {
                    foreach (ATTACHMENT ma in Attachments)
                    {
                         ma.Dispose();
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public void SendAsync()
        {
            mailSent = false;

            if (mailMessage == null)
            {
                throw new System.Exception("Invalid Mail message");
            }
            if (attachments != null)
            {
                foreach (ATTACHMENT ma in Attachments)
                {
                    mailMessage.Attachments.Add(ma.attachment);
                }
            }

              // Set the method that is called back when the send operation ends.
            base.SendCompleted += new SendCompletedEventHandler(client_SendCompleted);

            if (base.Host == null || base.Host == "")
            {
                base.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.PickupDirectoryFromIis;
            }
            // Add credentials if the SMTP server requires them.
            base.Credentials = CredentialCache.DefaultNetworkCredentials;

            string userState = "start mail";
            base.SendAsync(mailMessage, userState);

            // If the user canceled the send, and mail hasn't been sent yet,
            // then cancel the pending operation.
            if (mailSent == false)
            {
                base.SendAsyncCancel();
            }
            // Clean up.
            if (attachments != null)
            {
                foreach (ATTACHMENT ma in Attachments)
                {
                    ma.Dispose();
                }
            }
            mailMessage.Dispose();
        }

        void client_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {

            // Get the unique identifier for this asynchronous operation.
            String token = (string)e.UserState;

            if (e.Cancelled)
            {
                Console.WriteLine("[{0}] Send canceled.", token);
            }
            if (e.Error != null)
            {
                Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
            }
            else
            {
                Console.WriteLine("Message sent.");
                mailSent = true;
            }

            //throw new Exception("The method or operation is not implemented.");
        }



    }
}
