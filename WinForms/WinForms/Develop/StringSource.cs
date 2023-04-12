using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace mControl.Util
{

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity, Guid("EAC04BC0-3791-11d2-BB95-0060977B464C"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAutoCompleteServ
    {
        int Init([In] HandleRef hwndEdit, [In] System.Runtime.InteropServices.ComTypes.IEnumString punkACL, [In] string pwszRegKeyPath, [In] string pwszQuickComplete);
        void Enable([In] bool fEnable);
        int SetOptions([In] int dwFlag);
        void GetOptions([Out] IntPtr pdwFlag);
    }


    public class StringSource : System.Runtime.InteropServices.ComTypes.IEnumString
    {
        // Fields
        private static Guid autoCompleteClsid = new Guid("{00BB2763-6A77-11D0-A535-00C04FD7D062}");
        private IAutoCompleteServ autoCompleteObject2;
        private int current;
        private int size;
        private string[] strings;

        [return: MarshalAs(UnmanagedType.Interface)]
        [DllImport("ole32.dll", ExactSpelling = true, PreserveSig = false)]
        public static extern object CoCreateInstance([In] ref Guid clsid, [MarshalAs(UnmanagedType.Interface)] object punkOuter, int context, [In] ref Guid iid);

        // Methods
        public StringSource(string[] strings)
        {
            Array.Clear(strings, 0, this.size);
            if (strings != null)
            {
                this.strings = strings;
            }
            this.current = 0;
            this.size = (strings == null) ? 0 : strings.Length;
            Guid iid = typeof(IAutoCompleteServ).GUID;
            object obj2 = CoCreateInstance(ref autoCompleteClsid, null, 1, ref iid);
            this.autoCompleteObject2 = (IAutoCompleteServ)obj2;
        }

        public bool Bind(HandleRef edit, int options)
        {
            if (this.autoCompleteObject2 == null)
            {
                return false;
            }
            try
            {
                this.autoCompleteObject2.SetOptions(options);
                this.autoCompleteObject2.Init(edit, this, null, null);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void RefreshList(string[] newSource)
        {
            Array.Clear(this.strings, 0, this.size);
            if (this.strings != null)
            {
                this.strings = newSource;
            }
            this.current = 0;
            this.size = (this.strings == null) ? 0 : this.strings.Length;
        }

        public void ReleaseAutoComplete()
        {
            if (this.autoCompleteObject2 != null)
            {
                Marshal.ReleaseComObject(this.autoCompleteObject2);
                this.autoCompleteObject2 = null;
            }
        }

        void System.Runtime.InteropServices.ComTypes.IEnumString.Clone(out System.Runtime.InteropServices.ComTypes.IEnumString ppenum)
        {
            ppenum = new StringSource(this.strings);
        }

        int System.Runtime.InteropServices.ComTypes.IEnumString.Next(int celt, string[] rgelt, IntPtr pceltFetched)
        {
            if (celt < 0)
            {
                return -2147024809;
            }
            int index = 0;
            while ((this.current < this.size) && (celt > 0))
            {
                rgelt[index] = this.strings[this.current];
                this.current++;
                index++;
                celt--;
            }
            if (pceltFetched != IntPtr.Zero)
            {
                Marshal.WriteInt32(pceltFetched, index);
            }
            if (celt != 0)
            {
                return 1;
            }
            return 0;
        }

        void System.Runtime.InteropServices.ComTypes.IEnumString.Reset()
        {
            this.current = 0;
        }

        int System.Runtime.InteropServices.ComTypes.IEnumString.Skip(int celt)
        {
            this.current += celt;
            if (this.current >= this.size)
            {
                return 1;
            }
            return 0;
        }
    }

}
