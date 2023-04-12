using System;
using System.Runtime.InteropServices.ComTypes;
using SRV=System.Runtime.InteropServices;
using System.Text;

namespace Nistec.Printing.MSExcel.Bin2007
{
	/// <summary>
	/// Summary description for OleReader.
	/// </summary>
	public class OleReader
	{

        public const int CSIDL_LOCAL_APPDATA = 0x001c;
        public const int SHGFP_TYPE_CURRENT = 0;
        public const int MAX_PATH = 256;

        public const int S_OK = 0;
        public const uint E_POINTER = 0x80004003U;

        public const int STGM_SIMPLE    = 0x08000000;
        public const int STGM_CREATE    = 0x00001000;
        public const int STGM_READ      = 0x00000000;
        public const int STGM_READWRITE = 0x00000002;
        public const int STGM_SHARE_EXCLUSIVE  = 0x00000010;
        public const int STGM_SHARE_DENY_WRITE = 0x00000020;

        public const int STGTY_STORAGE	= 1;
        public const int STGTY_STREAM	= 2;
        public const int STGTY_LOCKBYTES = 3;
        public const int STGTY_PROPERTY	= 4;


        [SRV.DllImport("shell32.dll")]
        public static extern Int32 SHGetFolderPath(IntPtr hwndOwner, 
            Int32 nFolder, IntPtr hToken, UInt32 dwFlags, StringBuilder pszPath);

        [SRV.ComImport]
        [SRV.Guid("0000000d-0000-0000-C000-000000000046")]
        [SRV.InterfaceType(SRV.ComInterfaceType.InterfaceIsIUnknown)]
            public interface IEnumSTATSTG
        {
            // The user needs to allocate an STATSTG array whose size is celt.
                [SRV.PreserveSig]
            uint Next(uint celt,
                       [SRV.MarshalAs(SRV.UnmanagedType.LPArray), SRV.Out]STATSTG[] rgelt,
                       out uint pceltFetched);

            void Skip(uint celt);

            void Reset();

                [return: SRV.MarshalAs(SRV.UnmanagedType.Interface)]
                IEnumSTATSTG Clone();
        }

        [SRV.ComImport]
        [SRV.Guid("0000000b-0000-0000-C000-000000000046")]
        [SRV.InterfaceType(SRV.ComInterfaceType.InterfaceIsIUnknown)]
        public interface IStorage 
        {
            
            uint CreateStream(
                /* [string][in] */ string pwcsName,
                /* [in] */ uint grfMode,
                /* [in] */ uint reserved1,
                /* [in] */ uint reserved2,
                /* [out] */ out IStream ppstm);

            uint OpenStream(
                /* [string][in] */ string pwcsName,
                /* [unique][in] */ IntPtr reserved1,
                /* [in] */ uint grfMode,
                /* [in] */ uint reserved2,
                /* [out] */ out IStream ppstm);

            uint CreateStorage(
                /* [string][in] */ string pwcsName,
                /* [in] */ uint grfMode,
                /* [in] */ uint reserved1,
                /* [in] */ uint reserved2,
                /* [out] */ out IStorage ppstg);

            uint OpenStorage(
                /* [string][unique][in] */ string pwcsName,
                /* [unique][in] */ IStorage pstgPriority,
                /* [in] */ uint grfMode,
                /* [unique][in] */ IntPtr snbExclude,
                /* [in] */ uint reserved,
                /* [out] */ out IStorage ppstg);

            void CopyTo(
                /* [in] */ uint ciidExclude,
                /* [size_is][unique][in] */ Guid rgiidExclude,
                /* [unique][in] */ IntPtr snbExclude,
                /* [unique][in] */ IStorage pstgDest);

            void MoveElementTo(
                /* [string][in] */ string pwcsName,
                /* [unique][in] */ IStorage pstgDest,
                /* [string][in] */ string pwcsNewName,
                /* [in] */ uint grfFlags);

            void Commit(
                /* [in] */ uint grfCommitFlags);

            void Revert();

            uint EnumElements(
                /* [in] */ uint reserved1,
                /* [size_is][unique][in] */ IntPtr reserved2,
                /* [in] */ uint reserved3,
                /* [out] */ out IEnumSTATSTG ppenum);

            void DestroyElement(
                /* [string][in] */ string pwcsName);

            void RenameElement(
                /* [string][in] */ string pwcsOldName,
                /* [string][in] */ string pwcsNewName);

            void SetElementTimes(
                /* [string][unique][in] */ string pwcsName,
                /* [unique][in] */ FILETIME pctime,
                /* [unique][in] */ FILETIME patime,
                /* [unique][in] */ FILETIME pmtime);

            void SetClass(
                /* [in] */ Guid clsid);

            void SetStateBits(
                /* [in] */ uint grfStateBits,
                /* [in] */ uint grfMask);

            void Stat(
                /* [out] */ out STATSTG pstatstg,
                /* [in] */ uint grfStatFlag);

        }


        [SRV.DllImport("ole32.dll")]
        public static extern uint StgOpenStorage(
            [SRV.MarshalAs(SRV.UnmanagedType.LPWStr)] string pwcsName, 
            IStorage pstgPriority, 
            uint grfMode, 
            IntPtr snbExclude, 
            uint reserved, 
            out IStorage ppstgOpen);

        [SRV.DllImport("ole32.dll")]
        public static extern uint StgCreateDocfile(
            [SRV.MarshalAs(SRV.UnmanagedType.LPWStr)] string pwcsName, 
            uint grfMode, 
            uint reserved, 
            out IStorage ppstgOpen);


        // a IStorage is the equivalent of a folder
        // a IStream is the equivalent of a file
        // The hierarchy of IStorage/IStream is stored within a single file

        public static uint ReadAndDuplicate(String filename, String output_filename) 
        {
            IStorage src_stg = null;

            //
            // Open an OLE Storage Object
            //
            uint hr = StgOpenStorage(filename, 
                null, 
                STGM_READ | STGM_SHARE_DENY_WRITE,
                IntPtr.Zero, 
                (uint)0, 
                out src_stg);
            if (hr != 0)
                return hr;

            IStorage dest_stg = null;

            //
            // Create an OLE Storage Object
            //
            hr = StgCreateDocfile(output_filename, 
                STGM_READWRITE | STGM_CREATE | STGM_SHARE_EXCLUSIVE,
                0, 
                out dest_stg);

            if (hr != 0)
            {
                SRV.Marshal.ReleaseComObject(src_stg);
                return hr;
            }

            hr = ReadAndDuplicate(src_stg, dest_stg);

            SRV.Marshal.ReleaseComObject(dest_stg);
            SRV.Marshal.ReleaseComObject(src_stg);

            return hr;
        }

        public static uint ReadAndDuplicate(IStorage src, IStorage dest) 
        {
            if (src == null || dest == null)
                return E_POINTER;

            IEnumSTATSTG penum = null;

            uint hr = src.EnumElements((uint)0,
                IntPtr.Zero,
                (uint)0,
                out penum);
            if (hr != 0)
                return hr;

            penum.Reset();

            uint nb = 0;
            STATSTG[] stg = new STATSTG[1];

            hr = penum.Next(1, stg, out nb);

            while (hr == 0 && nb > 0)
            {
                // is it a stream or a storage?
                if (stg[0].type == STGTY_STORAGE)
                {
                    IStorage src_subStg  = null;
                    IStorage dest_subStg = null;

                    hr = src.OpenStorage(stg[0].pwcsName, 
                                null, 
                                STGM_READ | STGM_SHARE_EXCLUSIVE, 
                                IntPtr.Zero, 
                                (uint)0, 
                                out src_subStg);

                    if (hr != 0)
                        return hr;

                    hr = dest.CreateStorage(stg[0].pwcsName, 
                                STGM_READWRITE | STGM_SHARE_EXCLUSIVE, 
                                (uint)0, (uint)0, 
                                out dest_subStg);
                    if (hr != 0)
                    {
                        SRV.Marshal.ReleaseComObject(src_subStg);
                        return hr;
                    }

                    hr = ReadAndDuplicate(src_subStg, dest_subStg);

                    SRV.Marshal.ReleaseComObject(dest_subStg);
                    SRV.Marshal.ReleaseComObject(src_subStg);

                    if (hr != 0)
                        return hr;

                }
                else if (stg[0].type == STGTY_STREAM)
                {
                    IStream src_strm = null;
                    IStream dest_strm = null;

                    hr = src.OpenStream(stg[0].pwcsName, 
                                IntPtr.Zero, 
                                STGM_READ | STGM_SHARE_EXCLUSIVE, 
                                (uint)0, 
                                out src_strm);
                    if (hr != 0)
                        return hr;

                    hr = dest.CreateStream(stg[0].pwcsName, 
                                STGM_READWRITE | STGM_SHARE_EXCLUSIVE, 
                                (uint)0, (uint)0, 
                                out dest_strm);
                    if (hr != 0)
                    {
                        SRV.Marshal.ReleaseComObject(src_strm);
                        return hr;
                    }


                    // read the actual content of the stream
                    //

                    STATSTG statstgstream;
                    src_strm.Stat(out statstgstream, 1);
                    int len = (int) statstgstream.cbSize;
                    byte[] buffer = new byte[len];
                    SRV.GCHandle handle = SRV.GCHandle.Alloc(buffer, SRV.GCHandleType.Pinned);
                    src_strm.Read(buffer, len, IntPtr.Zero);

                    IntPtr cbWritten = IntPtr.Zero;
                    dest_strm.Write(buffer, len, cbWritten);

                    handle.Free();

                    SRV.Marshal.ReleaseComObject(dest_strm);
                    SRV.Marshal.ReleaseComObject(src_strm);
		    
                }

                if (hr != 0)
                    break;

                // iterate
                nb = 0;
                hr = penum.Next(1, stg, out nb);
            }

            SRV.Marshal.ReleaseComObject(penum);

            return hr;
        }

		public static void Read(String filename, String output_filename)
		{
            OleReader.ReadAndDuplicate(filename, output_filename);
		}
	}
}
