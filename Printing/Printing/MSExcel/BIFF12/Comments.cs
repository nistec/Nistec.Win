using System;
using System.Collections;

namespace Nistec.Printing.MSExcel.Bin2007
{
    // The following piece of code reads BIFF12 .bin parts from the Excel 2007 file format

    // developed by Stephane Rodriguez, ARsT Design, http://xlsgen.arstdesign.com, 2006 August

    // It's a starting point for a general purpose read/write .bin library

    class CommentsRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "comments";
        }
    }

    class CommentsEndRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "/comments";
        }
    }

    class AuthorsRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "authors";
        }
    }

    class AuthorsEndRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "/authors";
        }
    }

    class AuthorRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "author";
        }
    }

    class CommentListRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "commentList";
        }
    }

    class CommentListEndRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "/commentList";
        }
    }
 
    class CommentRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "comment";
        }
    }

    class CommentEndRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "/comment";
        }
    }
     
    class TextRecord : BaseRecord
    {
        public override void Read(byte[] buffer, ref UInt32 offset, int recid, int reclen, Hashtable h, Workbook w)
        {
        }

        public override String GetTag()
        {
            return "text";
        }
    }
    
 
}
