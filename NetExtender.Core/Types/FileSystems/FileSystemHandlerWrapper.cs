// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;
using NetExtender.FileSystems.Interfaces;

namespace NetExtender.FileSystems
{
    public class FileSystemHandlerWrapper : FileSystemHandlerWrapper<IFileSystem>
    {
        public FileSystemHandlerWrapper(IFileSystem handler)
            : base(handler)
        {
        }
    }
    
    public partial class FileSystemHandlerWrapper<T> : FileSystemHandler where T : class, IFileSystem
    {
        protected sealed override T FileSystem { get; }

        public override Object SyncRoot
        {
            get
            {
                return FileSystem.SyncRoot;
            }
        }

        public override Boolean IsSynchronized
        {
            get
            {
                return FileSystem.IsSynchronized;
            }
        }

        public sealed override StringComparer Comparer
        {
            get
            {
                return FileSystem.Comparer;
            }
        }

        public sealed override Boolean IsCaseSensitive
        {
            get
            {
                return FileSystem.IsCaseSensitive;
            }
        }

        public FileSystemHandlerWrapper(T handler)
        {
            FileSystem = handler ?? throw new ArgumentNullException(nameof(handler));
            FileSystem.PropertyChanging += FileSystemPropertyChanging;
            FileSystem.PropertyChanged += FileSystemPropertyChanged;
        }

        private void FileSystemPropertyChanging(Object? sender, PropertyChangingEventArgs args)
        {
            OnPropertyChanging(args);
        }

        private void FileSystemPropertyChanged(Object? sender, PropertyChangedEventArgs args)
        {
            OnPropertyChanged(args);
        }

        private protected sealed override String? FileSystemName()
        {
            return FileSystem.Name;
        }

        private protected sealed override DateTime CreationTimeFileSystem()
        {
            return FileSystem.CreationTime;
        }

        private protected sealed override DateTime CreationTimeUtcFileSystem()
        {
            return FileSystem.CreationTimeUtc;
        }

        private protected sealed override Boolean IsRealFileSystem()
        {
            return FileSystem.IsReal;
        }

        public override Int32 GetHashCode()
        {
            return FileSystem.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return FileSystem.Equals(other);
        }

        public override String? ToString()
        {
            return FileSystem.ToString();
        }
    }
}