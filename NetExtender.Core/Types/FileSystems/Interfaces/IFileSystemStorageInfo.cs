using System;
using System.Runtime.Serialization;
using NetExtender.Utilities.Types;

namespace NetExtender.FileSystems.Interfaces
{
    internal interface IFileSystemStorageEntry : IFileSystemStorageInfo
    {
        public new IFileSystemStorage FileSystem { get; }
        public new SyncRoot SyncRoot { get; }

        public void OnPropertyChanging(String property);
        public void OnPropertyChanged(String property);
    }
    
    public interface IFileSystemStorageInfo : IEquatable<IFileSystemStorageInfo>, ISerializable
    {
        public IFileSystem FileSystem { get; }
        public Object SyncRoot { get; }
        public Boolean IsSynchronized { get; }
        public String? Name { get; }
        public Boolean IsReal { get; }
    }
}