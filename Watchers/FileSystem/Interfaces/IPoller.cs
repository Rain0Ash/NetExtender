﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Threading.Tasks;

namespace NetExtender.Watchers.FileSystem.Interfaces
{
    public interface IPoller : IDisposable, ICloneable
    {
        Boolean EnableRaisingEvents { get; set; }
        String Filter { get; set; }
        Boolean IncludeSubdirectories { get; set; }
        String Path { get; set; }
        TimeSpan Polling { get; set; }
        PollingType PollingType { get; set; }
        
        event FileSystemEventHandler Created;
        event FileSystemEventHandler Deleted;
        event ErrorEventHandler Error;

        Task ForcePollAsync(Boolean returnWhenPolled);
        void ForcePoll();
    }
}