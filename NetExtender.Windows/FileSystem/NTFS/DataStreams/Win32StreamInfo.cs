// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.IO.FileSystem.NTFS.DataStreams
{
    public readonly struct Win32StreamInfo
    {
        public FileStreamType StreamType { get; }
        public FileStreamAttributes StreamAttributes { get; }
        public Int64 StreamSize { get; }
        public String StreamName { get; }
        
        public Win32StreamInfo(FileStreamType type, FileStreamAttributes attributes, Int64 size, String name)
        {
            StreamType = type;
            StreamAttributes = attributes;
            StreamSize = size;
            StreamName = name;
        }
    }
}