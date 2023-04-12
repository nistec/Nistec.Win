using System;
using System.Net;
using System.Text;
using System.IO;
using System.Web.Util;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net.Mime;
using System.Data;


namespace MControl.Web
{

    public class ATTACHMENT
    {

        public Attachment attachment;
        public readonly string fileName;

        public ATTACHMENT(string file)
        {
            // Create  the file attachment for this e-mail message.
            attachment = new Attachment(file, MediaTypeNames.Application.Octet);
            // Add time stamp information for the file.
            ContentDisposition disposition = attachment.ContentDisposition;
            disposition.CreationDate = System.IO.File.GetCreationTime(file);
            disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
            disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
        }

        public ATTACHMENT(DataSet ds)
        {
            string tempName = Guid.NewGuid().ToString();
            fileName = System.IO.Path.GetTempPath() + tempName + ".htm";// "default.htm";
            if (ds != null)
            {
                ExportUtil.ExportHtml(ds, fileName);
                attachment = new Attachment(fileName);
            }

        }

        public ATTACHMENT(DataTable dt)
        {
            string tempName = Guid.NewGuid().ToString();
            fileName = System.IO.Path.GetTempPath() + tempName + ".htm";// "default.htm";
            if (dt != null)
            {
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                ExportUtil.ExportHtml(ds, fileName);
                attachment = new Attachment(fileName);
            }

        }

        ~ATTACHMENT()
        {
            Dispose();
        }
       
        public void Dispose()
        {
            if (attachment != null)
            {
                attachment.Dispose();
                try
                {
                    System.IO.File.Delete(fileName);
                }
                catch { }
            }
        }


    }

    public class AttchementCollection : System.Collections.CollectionBase//.ArrayList
    {
        public void Add(ATTACHMENT a)
        {
            base.List.Add(a);
        }
        public void Remove(ATTACHMENT a)
        {
            base.List.Remove(a);
        }
    }

}
