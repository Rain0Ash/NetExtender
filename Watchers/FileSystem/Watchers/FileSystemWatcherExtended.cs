// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using DynamicData.Annotations;
using NetExtender.Watchers.FileSystem.Interfaces;

namespace NetExtender.Watchers.FileSystem
{
    public class FileSystemWatcherExtended : FileSystemWatcher, IWatcher
    {
        public FileSystemWatcherExtended()
        {
        }

        public FileSystemWatcherExtended([NotNull] String path)
            : base(path)
        {
        }

        public FileSystemWatcherExtended([NotNull] String path, [NotNull] String filter)
            : base(path, filter)
        {
        }

        public Object Clone()
        {
            return new FileSystemWatcherExtended(Path, Filter)
            {
                Filter = Filter,
                IncludeSubdirectories = IncludeSubdirectories,
                NotifyFilter = NotifyFilter,
                InternalBufferSize = InternalBufferSize,
            };
        }

        public void StartWatch()
        {
            EnableRaisingEvents = true;
        }

        public void StopWatch()
        {
            EnableRaisingEvents = false;
        }
    }
}