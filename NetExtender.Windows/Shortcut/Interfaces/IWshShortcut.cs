// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.InteropServices;

namespace NetExtender.Windows.Shortcut.Interfaces
{
    [ComImport]
    [TypeLibType(0x1040)]
    [Guid("F935DC23-1CF0-11D0-ADB9-00C04FD58A0B")]
    internal interface IWshShortcut
    {
        [DispId(0)]
        public String FullName { [return: MarshalAs(UnmanagedType.BStr)] [DispId(0)] get; }
        
        [DispId(0x3e8)]
        public String? Arguments { [return: MarshalAs(UnmanagedType.BStr)] [DispId(0x3e8)] get; [param: In, MarshalAs(UnmanagedType.BStr)] [DispId(0x3e8)] set; }
        
        [DispId(0x3e9)]
        public String? Description { [return: MarshalAs(UnmanagedType.BStr)] [DispId(0x3e9)] get; [param: In, MarshalAs(UnmanagedType.BStr)] [DispId(0x3e9)] set; }
        
        [DispId(0x3ea)]
        public String? Hotkey { [return: MarshalAs(UnmanagedType.BStr)] [DispId(0x3ea)] get; [param: In, MarshalAs(UnmanagedType.BStr)] [DispId(0x3ea)] set; }
        
        [DispId(0x3eb)]
        public String? IconLocation { [return: MarshalAs(UnmanagedType.BStr)] [DispId(0x3eb)] get; [param: In, MarshalAs(UnmanagedType.BStr)] [DispId(0x3eb)] set; }
        
        [DispId(0x3ec)]
        public String RelativePath { [param: In, MarshalAs(UnmanagedType.BStr)] [DispId(0x3ec)] set; }
        
        [DispId(0x3ed)]
        public String TargetPath { [return: MarshalAs(UnmanagedType.BStr)] [DispId(0x3ed)] get; [param: In, MarshalAs(UnmanagedType.BStr)] [DispId(0x3ed)] set; }
        
        [DispId(0x3ee)]
        public Int32 WindowStyle { [DispId(0x3ee)] get; [param: In] [DispId(0x3ee)] set; }
        
        [DispId(0x3ef)]
        public String WorkingDirectory { [return: MarshalAs(UnmanagedType.BStr)] [DispId(0x3ef)] get; [param: In, MarshalAs(UnmanagedType.BStr)] [DispId(0x3ef)] set; }
        
        [TypeLibFunc(0x40), DispId(0x7d0)]
        public void Load([In, MarshalAs(UnmanagedType.BStr)] String path);
        
        [DispId(0x7d1)]
        public void Save();
    }
}