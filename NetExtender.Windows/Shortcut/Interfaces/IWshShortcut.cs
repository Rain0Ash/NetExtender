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

        [DispId(0x3E8)]
        public String? Arguments { [return: MarshalAs(UnmanagedType.BStr)] [DispId(0x3E8)] get; [param: In, MarshalAs(UnmanagedType.BStr)] [DispId(0x3E8)] set; }

        [DispId(0x3E9)]
        public String? Description { [return: MarshalAs(UnmanagedType.BStr)] [DispId(0x3E9)] get; [param: In, MarshalAs(UnmanagedType.BStr)] [DispId(0x3E9)] set; }

        [DispId(0x3EA)]
        public String? Hotkey { [return: MarshalAs(UnmanagedType.BStr)] [DispId(0x3EA)] get; [param: In, MarshalAs(UnmanagedType.BStr)] [DispId(0x3EA)] set; }

        [DispId(0x3EB)]
        public String? IconLocation { [return: MarshalAs(UnmanagedType.BStr)] [DispId(0x3EB)] get; [param: In, MarshalAs(UnmanagedType.BStr)] [DispId(0x3EB)] set; }

        [DispId(0x3EC)]
        public String RelativePath { [param: In, MarshalAs(UnmanagedType.BStr)] [DispId(0x3EC)] set; }

        [DispId(0x3ED)]
        public String TargetPath { [return: MarshalAs(UnmanagedType.BStr)] [DispId(0x3ED)] get; [param: In, MarshalAs(UnmanagedType.BStr)] [DispId(0x3ED)] set; }

        [DispId(0x3EE)]
        public Int32 WindowStyle { [DispId(0x3EE)] get; [param: In] [DispId(0x3EE)] set; }

        [DispId(0x3EF)]
        public String WorkingDirectory { [return: MarshalAs(UnmanagedType.BStr)] [DispId(0x3EF)] get; [param: In, MarshalAs(UnmanagedType.BStr)] [DispId(0x3EF)] set; }

        [TypeLibFunc(0x40), DispId(0x7D0)]
        public void Load([In, MarshalAs(UnmanagedType.BStr)] String path);

        [DispId(0x7D1)]
        public void Save();
    }
}