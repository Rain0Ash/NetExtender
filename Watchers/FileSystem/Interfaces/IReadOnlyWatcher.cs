// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;

namespace NetExtender.Watchers.FileSystem.Interfaces
{
    public interface IReadOnlyWatcher : ICloneable, IDisposable
    {
        public event FileSystemEventHandler Changed;
        public event FileSystemEventHandler Created;
        public event FileSystemEventHandler Deleted;
        public event RenamedEventHandler Renamed;
        public event ErrorEventHandler Error;
        
        public String Path { get; }

        public String Filter { get; }

        public Boolean IncludeSubdirectories { get; }
        
        public Boolean EnableRaisingEvents { get; }
        public Int32 InternalBufferSize { get; }
        
        public NotifyFilters NotifyFilter { get; }

        public WaitForChangedResult WaitForChanged(WatcherChangeTypes changeType);
        public WaitForChangedResult WaitForChanged(WatcherChangeTypes changeType, Int32 timeout);
    }
}