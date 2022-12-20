using System;
using System.Runtime.InteropServices;

namespace HexedInstaller
{
    class OpenFileDialog
    {
        [DllImport("Comdlg32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool GetOpenFileName(ref OpenFileName openFileName_0);

        public static string OpenfileDialog()
        {
            OpenFileName openFileName_ = default;
            openFileName_.lStructSize = Marshal.SizeOf<OpenFileName>();
            openFileName_.lpstrFilter = "All files(*.*)\0\0";
            openFileName_.lpstrFile = new string(new char[256]);
            openFileName_.nMaxFile = openFileName_.lpstrFile.Length;
            openFileName_.lpstrFileTitle = new string(new char[64]);
            openFileName_.nMaxFileTitle = openFileName_.lpstrFileTitle.Length;
            openFileName_.lpstrTitle = "Select VRChat.exe";
            if (!GetOpenFileName(ref openFileName_)) return string.Empty;
            return openFileName_.lpstrFile;
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct OpenFileName
    {
        public int lStructSize;

        public IntPtr hwndOwner;

        public IntPtr hInstance;

        public string lpstrFilter;

        public string lpstrCustomFilter;

        public int nMaxCustFilter;

        public int nFilterIndex;

        public string lpstrFile;

        public int nMaxFile;

        public string lpstrFileTitle;

        public int nMaxFileTitle;

        public string lpstrInitialDir;

        public string lpstrTitle;

        public int Flags;

        public short nFileOffset;

        public short nFileExtension;

        public string lpstrDefExt;

        public IntPtr lCustData;

        public IntPtr lpfnHook;

        public string lpTemplateName;

        public IntPtr pvReserved;

        public int dwReserved;

        public int flagsEx;
    }
}
