// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;

namespace NetExtender.Watchers.FileSystem.Interfaces
{
    /// <summary>
    /// Defines properties, events and methods for a FileSystemWatcher-like class
    /// </summary>
    public interface IWatcher : IReadOnlyWatcher
    {
        public new Boolean EnableRaisingEvents { get; set; }
        public new String Filter { get; set; }
        public new Boolean IncludeSubdirectories { get; set; }
        public new Int32 InternalBufferSize { get; set; }
        public new NotifyFilters NotifyFilter { get; set; }
        public new String Path { get; set; }
        
        public void StartWatch();
        public void StopWatch();
    }
}