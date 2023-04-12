using System;
using System.Collections;
using System.Runtime.InteropServices;


namespace Nistec.Printing.MSExcel.Bin2007
{
    // The following piece of code reads BIFF12 .bin parts from the Excel 2007 file format

    // developed by Stephane Rodriguez, ARsT Design, http://xlsgen.arstdesign.com, 2006 August

    // It's a starting point for a general purpose read/write .bin library

    // WIN32 interop
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode)]
    public class DEVMODE
    {
        [MarshalAs(UnmanagedType.ByValTStr,SizeConst=32)] public string dmDeviceName;
        public short  dmSpecVersion;
        public short  dmDriverVersion;
        public short  dmSize;
        public short  dmDriverExtra;
        public int    dmFields;

        public short dmOrientation;
        public short dmPaperSize;
        public short dmPaperLength;
        public short dmPaperWidth;

        public short dmScale;
        public short dmCopies;
        public short dmDefaultSource;
        public short dmPrintQuality;
        public short dmColor;
        public short dmDuplex;
        public short dmYResolution;
        public short dmTTOption;
        public short dmCollate;
        [MarshalAs(UnmanagedType.ByValTStr,SizeConst=32)] public string dmFormName;
        public short dmLogPixels;
        public short dmBitsPerPel;
        public int   dmPelsWidth;
        public int   dmPelsHeight;

        public int   dmDisplayFlags;
        public int   dmDisplayFrequency;

        public int   dmICMMethod;
        public int   dmICMIntent;
        public int   dmMediaType;
        public int   dmDitherType;
        public int   dmReserved1;
        public int   dmReserved2;

        public int   dmPanningWidth;
        public int   dmPanningHeight;
    };

    

    class PrinterSettingsReader
    {
        public const int DM_ORIENTATION = 1;

        public static void Read(Workbook w, Hashtable h, byte[] buffer)
        {
            if (buffer.Length == 0)
                return;

            // allocate a temporary memory block, and get the pointer (unsafe)
            IntPtr pdevmode = Marshal.AllocHGlobal(buffer.Length);

            // instantiate our struct
            DEVMODE devmode = new DEVMODE();

            Marshal.Copy(buffer, 0, pdevmode, buffer.Length);

            // copy the buffer into the structure
            Marshal.PtrToStructure(pdevmode, devmode);             

            // clean up
            Marshal.FreeHGlobal(pdevmode);

            Console.WriteLine(String.Format("printer device name : {0}\r\n", devmode.dmDeviceName));
            Console.WriteLine(String.Format("printer paper size : {0}\r\n", devmode.dmFormName));

            if ((devmode.dmFields & DM_ORIENTATION) != 0)
                Console.WriteLine(String.Format("orientation : {0}\r\n", devmode.dmOrientation));

            Console.WriteLine();

        }
    }

}
