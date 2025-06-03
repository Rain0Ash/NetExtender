// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using NetExtender.FileSystems.Interfaces;
using NetExtender.Utilities.IO;
using NetExtender.Utilities.Types;

namespace NetExtender.FileSystems
{
    public abstract partial class FileSystemStorage<TLink, TFile, TDirectory, TDrive>
    {
        public abstract override String[] GetLogicalDrives();
        public abstract override String GetCurrentDirectory();
        public abstract override Boolean SetCurrentDirectory(String path);
        public abstract override String GetDirectoryRoot(String path);
        public abstract override IDirectoryInfo CreateDirectory(String path);

        public override String[] GetFileSystemEntries(String path)
        {
            return GetFileSystemEntries(path, AnyPattern);
        }

        public override String[] GetFileSystemEntries(String path, String? pattern)
        {
            return GetFileSystemEntries(path, pattern, SearchOption.TopDirectoryOnly);
        }

        public override String[] GetFileSystemEntries(String path, String? pattern, SearchOption option)
        {
            return GetFileSystemEntries(path, pattern, option.Options());
        }

        public abstract override String[] GetFileSystemEntries(String path, String? pattern, EnumerationOptions options);

        public override String[] GetFiles(String path)
        {
            return GetFiles(path, AnyPattern);
        }

        public override String[] GetFiles(String path, String? pattern)
        {
            return GetFiles(path, pattern, SearchOption.TopDirectoryOnly);
        }

        public override String[] GetFiles(String path, String? pattern, SearchOption option)
        {
            return GetFiles(path, pattern, option.Options());
        }

        public abstract override String[] GetFiles(String path, String? pattern, EnumerationOptions options);

        public override String[] GetDirectories(String path)
        {
            return GetDirectories(path, AnyPattern);
        }

        public override String[] GetDirectories(String path, String? pattern)
        {
            return GetDirectories(path, pattern, SearchOption.TopDirectoryOnly);
        }

        public override String[] GetDirectories(String path, String? pattern, SearchOption option)
        {
            return GetDirectories(path, pattern, option.Options());
        }

        public abstract override String[] GetDirectories(String path, String? pattern, EnumerationOptions options);

        public override IEnumerable<String> EnumerateFileSystemEntries(String path)
        {
            return EnumerateFileSystemEntries(path, AnyPattern);
        }

        public override IEnumerable<String> EnumerateFileSystemEntries(String path, String? pattern)
        {
            return EnumerateFileSystemEntries(path, pattern, SearchOption.TopDirectoryOnly);
        }

        public override IEnumerable<String> EnumerateFileSystemEntries(String path, String? pattern, SearchOption option)
        {
            return EnumerateFileSystemEntries(path, pattern, option.Options());
        }

        public abstract override IEnumerable<String> EnumerateFileSystemEntries(String path, String? pattern, EnumerationOptions options);

        public override IEnumerable<String> EnumerateFiles(String path)
        {
            return EnumerateFiles(path, AnyPattern);
        }

        public override IEnumerable<String> EnumerateFiles(String path, String? pattern)
        {
            return EnumerateFiles(path, pattern, SearchOption.TopDirectoryOnly);
        }

        public override IEnumerable<String> EnumerateFiles(String path, String? pattern, SearchOption option)
        {
            return EnumerateFiles(path, pattern, option.Options());
        }

        public abstract override IEnumerable<String> EnumerateFiles(String path, String? pattern, EnumerationOptions options);

        public override IEnumerable<String> EnumerateDirectories(String path)
        {
            return EnumerateDirectories(path, AnyPattern);
        }

        public override IEnumerable<String> EnumerateDirectories(String path, String? pattern)
        {
            return EnumerateDirectories(path, pattern, SearchOption.TopDirectoryOnly);
        }

        public override IEnumerable<String> EnumerateDirectories(String path, String? pattern, SearchOption option)
        {
            return EnumerateDirectories(path, pattern, option.Options());
        }

        public abstract override IEnumerable<String> EnumerateDirectories(String path, String? pattern, EnumerationOptions options);
        
#region Storage
        protected internal abstract DirectoryInfo? Info(TDirectory node);
        
        DirectoryInfo? IFileSystemStorage.Info(IDirectoryEntry entry)
        {
            return Info(Verify<TDirectory, IDirectoryEntry>(entry));
        }
        
        protected internal abstract void GetObjectData(TDirectory node, SerializationInfo info, StreamingContext context);
        
        void IFileSystemStorage.GetObjectData(IDirectoryEntry entry, SerializationInfo info, StreamingContext context)
        {
            GetObjectData(Verify<TDirectory, IDirectoryEntry>(entry), info, context);
        }
        
        protected internal new abstract String Name(TDirectory node);

        String IFileSystemStorage.Name(IDirectoryEntry entry)
        {
            return Name(Verify<TDirectory, IDirectoryEntry>(entry));
        }

        protected internal abstract String FullName(TDirectory node);

        String IFileSystemStorage.FullName(IDirectoryEntry entry)
        {
            return FullName(Verify<TDirectory, IDirectoryEntry>(entry));
        }
        
        protected internal abstract String Extension(TDirectory node);

        String IFileSystemStorage.Extension(IDirectoryEntry entry)
        {
            return Extension(Verify<TDirectory, IDirectoryEntry>(entry));
        }
        
        protected internal new abstract Boolean IsReal(TDirectory node);

        Boolean IFileSystemStorage.IsReal(IDirectoryEntry entry)
        {
            return IsReal(Verify<TDirectory, IDirectoryEntry>(entry));
        }
        
        protected internal abstract Boolean Exists(TDirectory node);

        Boolean IFileSystemStorage.Exists(IDirectoryEntry entry)
        {
            return Exists(Verify<TDirectory, IDirectoryEntry>(entry));
        }
        
        protected internal abstract FileAttributes Attributes(TDirectory node);

        FileAttributes IFileSystemStorage.Attributes(IDirectoryEntry entry)
        {
            return Attributes(Verify<TDirectory, IDirectoryEntry>(entry));
        }

        protected internal abstract void Attributes(TDirectory node, FileAttributes value);

        void IFileSystemStorage.Attributes(IDirectoryEntry entry, FileAttributes value)
        {
            Attributes(Verify<TDirectory, IDirectoryEntry>(entry), value);
        }
        
        protected internal abstract String? LinkTarget(TDirectory node);
        
        String? IFileSystemStorage.LinkTarget(IDirectoryEntry entry)
        {
            return LinkTarget(Verify<TDirectory, IDirectoryEntry>(entry));
        }
        
        protected internal new abstract DateTime CreationTime(TDirectory node);

        DateTime IFileSystemStorage.CreationTime(IDirectoryEntry entry)
        {
            return CreationTime(Verify<TDirectory, IDirectoryEntry>(entry));
        }
        
        protected internal new abstract void CreationTime(TDirectory node, DateTime value);

        void IFileSystemStorage.CreationTime(IDirectoryEntry entry, DateTime value)
        {
            CreationTime(Verify<TDirectory, IDirectoryEntry>(entry), value);
        }
        
        protected internal new abstract DateTime CreationTimeUtc(TDirectory node);

        DateTime IFileSystemStorage.CreationTimeUtc(IDirectoryEntry entry)
        {
            return CreationTimeUtc(Verify<TDirectory, IDirectoryEntry>(entry));
        }
        
        protected internal new abstract void CreationTimeUtc(TDirectory node, DateTime value);

        void IFileSystemStorage.CreationTimeUtc(IDirectoryEntry entry, DateTime value)
        {
            CreationTimeUtc(Verify<TDirectory, IDirectoryEntry>(entry), value);
        }
        
        protected internal abstract DateTime LastAccessTime(TDirectory node);

        DateTime IFileSystemStorage.LastAccessTime(IDirectoryEntry entry)
        {
            return LastAccessTime(Verify<TDirectory, IDirectoryEntry>(entry));
        }
        
        protected internal abstract void LastAccessTime(TDirectory node, DateTime value);

        void IFileSystemStorage.LastAccessTime(IDirectoryEntry entry, DateTime value)
        {
            LastAccessTime(Verify<TDirectory, IDirectoryEntry>(entry), value);
        }
        
        protected internal abstract DateTime LastAccessTimeUtc(TDirectory node);

        DateTime IFileSystemStorage.LastAccessTimeUtc(IDirectoryEntry entry)
        {
            return LastAccessTimeUtc(Verify<TDirectory, IDirectoryEntry>(entry));
        }
        
        protected internal abstract void LastAccessTimeUtc(TDirectory node, DateTime value);

        void IFileSystemStorage.LastAccessTimeUtc(IDirectoryEntry entry, DateTime value)
        {
            LastAccessTimeUtc(Verify<TDirectory, IDirectoryEntry>(entry), value);
        }
        
        protected internal abstract DateTime LastWriteTime(TDirectory node);

        DateTime IFileSystemStorage.LastWriteTime(IDirectoryEntry entry)
        {
            return LastWriteTime(Verify<TDirectory, IDirectoryEntry>(entry));
        }
        
        protected internal abstract void LastWriteTime(TDirectory node, DateTime value);

        void IFileSystemStorage.LastWriteTime(IDirectoryEntry entry, DateTime value)
        {
            LastWriteTime(Verify<TDirectory, IDirectoryEntry>(entry), value);
        }
        
        protected internal abstract DateTime LastWriteTimeUtc(TDirectory node);

        DateTime IFileSystemStorage.LastWriteTimeUtc(IDirectoryEntry entry)
        {
            return LastWriteTimeUtc(Verify<TDirectory, IDirectoryEntry>(entry));
        }
        
        protected internal abstract void LastWriteTimeUtc(TDirectory node, DateTime value);

        void IFileSystemStorage.LastWriteTimeUtc(IDirectoryEntry entry, DateTime value)
        {
            LastWriteTimeUtc(Verify<TDirectory, IDirectoryEntry>(entry), value);
        }
        
        protected internal abstract TDirectory Root(TDirectory node);

        IDirectoryEntry IFileSystemStorage.Root(IDirectoryEntry entry)
        {
            return Root(Verify<TDirectory, IDirectoryEntry>(entry));
        }
        
        protected internal abstract TDirectory? Parent(TDirectory node);

        IDirectoryEntry? IFileSystemStorage.Parent(IDirectoryEntry entry)
        {
            return Parent(Verify<TDirectory, IDirectoryEntry>(entry));
        }
        
        protected internal new abstract TDrive? Drive(TDirectory node);

        IDriveEntry? IFileSystemStorage.Drive(IDirectoryEntry entry)
        {
            return Drive(Verify<TDirectory, IDirectoryEntry>(entry));
        }
        
        protected internal abstract Boolean CreateAsSymbolicLink(TDirectory node, String target);

        Boolean IFileSystemStorage.CreateAsSymbolicLink(IDirectoryEntry entry, String target)
        {
            return CreateAsSymbolicLink(Verify<TDirectory, IDirectoryEntry>(entry), target);
        }
        
        protected internal abstract IFileSystemNode? ResolveLinkTarget(TDirectory node);

        IFileSystemEntry? IFileSystemStorage.ResolveLinkTarget(IDirectoryEntry entry)
        {
            return ResolveLinkTarget(Verify<TDirectory, IDirectoryEntry>(entry))?.This;
        }
        
        protected internal abstract IFileSystemNode? ResolveLinkTarget(TDirectory node, Boolean final);

        IFileSystemEntry? IFileSystemStorage.ResolveLinkTarget(IDirectoryEntry entry, Boolean final)
        {
            return ResolveLinkTarget(Verify<TDirectory, IDirectoryEntry>(entry), final)?.This;
        }
        
        protected internal abstract IFileSystemNode[] GetFileSystemInfos(TDirectory node);

        IFileSystemEntry[] IFileSystemStorage.GetFileSystemInfos(IDirectoryEntry entry)
        {
            return GetFileSystemInfos(Verify<TDirectory, IDirectoryEntry>(entry)).ConvertAll(static node => node.This);
        }
        
        protected internal abstract IFileSystemNode[] GetFileSystemInfos(TDirectory node, String? pattern);

        IFileSystemEntry[] IFileSystemStorage.GetFileSystemInfos(IDirectoryEntry entry, String? pattern)
        {
            return GetFileSystemInfos(Verify<TDirectory, IDirectoryEntry>(entry), pattern).ConvertAll(static node => node.This);
        }
        
        protected internal abstract IFileSystemNode[] GetFileSystemInfos(TDirectory node, String? pattern, SearchOption option);

        IFileSystemEntry[] IFileSystemStorage.GetFileSystemInfos(IDirectoryEntry entry, String? pattern, SearchOption option)
        {
            return GetFileSystemInfos(Verify<TDirectory, IDirectoryEntry>(entry), pattern, option).ConvertAll(static node => node.This);
        }
        
        protected internal abstract IFileSystemNode[] GetFileSystemInfos(TDirectory node, String? pattern, EnumerationOptions options);

        IFileSystemEntry[] IFileSystemStorage.GetFileSystemInfos(IDirectoryEntry entry, String? pattern, EnumerationOptions options)
        {
            return GetFileSystemInfos(Verify<TDirectory, IDirectoryEntry>(entry), pattern, options).ConvertAll(static node => node.This);
        }
        
        protected internal abstract TFile[] GetFiles(TDirectory node);

        IFileEntry[] IFileSystemStorage.GetFiles(IDirectoryEntry entry)
        {
            return GetFiles(Verify<TDirectory, IDirectoryEntry>(entry)).ConvertAll(static node => node.This);
        }
        
        protected internal abstract TFile[] GetFiles(TDirectory node, String? pattern);

        IFileEntry[] IFileSystemStorage.GetFiles(IDirectoryEntry entry, String? pattern)
        {
            return GetFiles(Verify<TDirectory, IDirectoryEntry>(entry), pattern).ConvertAll(static node => node.This);
        }
        
        protected internal abstract TFile[] GetFiles(TDirectory node, String? pattern, SearchOption option);

        IFileEntry[] IFileSystemStorage.GetFiles(IDirectoryEntry entry, String? pattern, SearchOption option)
        {
            return GetFiles(Verify<TDirectory, IDirectoryEntry>(entry), pattern, option).ConvertAll(static node => node.This);
        }
        
        protected internal abstract TFile[] GetFiles(TDirectory node, String? pattern, EnumerationOptions options);

        IFileEntry[] IFileSystemStorage.GetFiles(IDirectoryEntry entry, String? pattern, EnumerationOptions options)
        {
            return GetFiles(Verify<TDirectory, IDirectoryEntry>(entry), pattern, options).ConvertAll(static node => node.This);
        }
        
        protected internal abstract TDirectory[] GetDirectories(TDirectory node);

        IDirectoryEntry[] IFileSystemStorage.GetDirectories(IDirectoryEntry entry)
        {
            return GetDirectories(Verify<TDirectory, IDirectoryEntry>(entry)).ConvertAll(static node => node.This);
        }
        
        protected internal abstract TDirectory[] GetDirectories(TDirectory node, String? pattern);

        IDirectoryEntry[] IFileSystemStorage.GetDirectories(IDirectoryEntry entry, String? pattern)
        {
            return GetDirectories(Verify<TDirectory, IDirectoryEntry>(entry), pattern).ConvertAll(static node => node.This);
        }
        
        protected internal abstract TDirectory[] GetDirectories(TDirectory node, String? pattern, SearchOption option);

        IDirectoryEntry[] IFileSystemStorage.GetDirectories(IDirectoryEntry entry, String? pattern, SearchOption option)
        {
            return GetDirectories(Verify<TDirectory, IDirectoryEntry>(entry), pattern, option).ConvertAll(static node => node.This);
        }
        
        protected internal abstract TDirectory[] GetDirectories(TDirectory node, String? pattern, EnumerationOptions options);

        IDirectoryEntry[] IFileSystemStorage.GetDirectories(IDirectoryEntry entry, String? pattern, EnumerationOptions options)
        {
            return GetDirectories(Verify<TDirectory, IDirectoryEntry>(entry), pattern, options).ConvertAll(static node => node.This);
        }
        
        protected internal abstract IEnumerable<IFileSystemNode> EnumerateFileSystemInfos(TDirectory node);

        IEnumerable<IFileSystemEntry> IFileSystemStorage.EnumerateFileSystemInfos(IDirectoryEntry entry)
        {
            return EnumerateFileSystemInfos(Verify<TDirectory, IDirectoryEntry>(entry)).Select(static node => node.This);
        }
        
        protected internal abstract IEnumerable<IFileSystemNode> EnumerateFileSystemInfos(TDirectory node, String? pattern);

        IEnumerable<IFileSystemEntry> IFileSystemStorage.EnumerateFileSystemInfos(IDirectoryEntry entry, String? pattern)
        {
            return EnumerateFileSystemInfos(Verify<TDirectory, IDirectoryEntry>(entry), pattern).Select(static node => node.This);
        }
        
        protected internal abstract IEnumerable<IFileSystemNode> EnumerateFileSystemInfos(TDirectory node, String? pattern, SearchOption option);

        IEnumerable<IFileSystemEntry> IFileSystemStorage.EnumerateFileSystemInfos(IDirectoryEntry entry, String? pattern, SearchOption option)
        {
            return EnumerateFileSystemInfos(Verify<TDirectory, IDirectoryEntry>(entry), pattern, option).Select(static node => node.This);
        }
        
        protected internal abstract IEnumerable<IFileSystemNode> EnumerateFileSystemInfos(TDirectory node, String? pattern, EnumerationOptions options);

        IEnumerable<IFileSystemEntry> IFileSystemStorage.EnumerateFileSystemInfos(IDirectoryEntry entry, String? pattern, EnumerationOptions options)
        {
            return EnumerateFileSystemInfos(Verify<TDirectory, IDirectoryEntry>(entry), pattern, options).Select(static node => node.This);
        }
        
        protected internal abstract IEnumerable<TFile> EnumerateFiles(TDirectory node);

        IEnumerable<IFileEntry> IFileSystemStorage.EnumerateFiles(IDirectoryEntry entry)
        {
            return EnumerateFiles(Verify<TDirectory, IDirectoryEntry>(entry));
        }
        
        protected internal abstract IEnumerable<TFile> EnumerateFiles(TDirectory node, String? pattern);

        IEnumerable<IFileEntry> IFileSystemStorage.EnumerateFiles(IDirectoryEntry entry, String? pattern)
        {
            return EnumerateFiles(Verify<TDirectory, IDirectoryEntry>(entry), pattern);
        }
        
        protected internal abstract IEnumerable<TFile> EnumerateFiles(TDirectory node, String? pattern, SearchOption option);

        IEnumerable<IFileEntry> IFileSystemStorage.EnumerateFiles(IDirectoryEntry entry, String? pattern, SearchOption option)
        {
            return EnumerateFiles(Verify<TDirectory, IDirectoryEntry>(entry), pattern, option);
        }
        
        protected internal abstract IEnumerable<TFile> EnumerateFiles(TDirectory node, String? pattern, EnumerationOptions options);

        IEnumerable<IFileEntry> IFileSystemStorage.EnumerateFiles(IDirectoryEntry entry, String? pattern, EnumerationOptions options)
        {
            return EnumerateFiles(Verify<TDirectory, IDirectoryEntry>(entry), pattern, options);
        }
        
        protected internal abstract IEnumerable<TDirectory> EnumerateDirectories(TDirectory node);

        IEnumerable<IDirectoryEntry> IFileSystemStorage.EnumerateDirectories(IDirectoryEntry entry)
        {
            return EnumerateDirectories(Verify<TDirectory, IDirectoryEntry>(entry));
        }
        
        protected internal abstract IEnumerable<TDirectory> EnumerateDirectories(TDirectory node, String? pattern);

        IEnumerable<IDirectoryEntry> IFileSystemStorage.EnumerateDirectories(IDirectoryEntry entry, String? pattern)
        {
            return EnumerateDirectories(Verify<TDirectory, IDirectoryEntry>(entry), pattern);
        }
        
        protected internal abstract IEnumerable<TDirectory> EnumerateDirectories(TDirectory node, String? pattern, SearchOption option);

        IEnumerable<IDirectoryEntry> IFileSystemStorage.EnumerateDirectories(IDirectoryEntry entry, String? pattern, SearchOption option)
        {
            return EnumerateDirectories(Verify<TDirectory, IDirectoryEntry>(entry), pattern, option);
        }
        
        protected internal abstract IEnumerable<TDirectory> EnumerateDirectories(TDirectory node, String? pattern, EnumerationOptions options);

        IEnumerable<IDirectoryEntry> IFileSystemStorage.EnumerateDirectories(IDirectoryEntry entry, String? pattern, EnumerationOptions options)
        {
            return EnumerateDirectories(Verify<TDirectory, IDirectoryEntry>(entry), pattern, options);
        }
        
        protected internal abstract void MoveTo(TDirectory node, String destination);

        void IFileSystemStorage.MoveTo(IDirectoryEntry entry, String destination)
        {
            MoveTo(Verify<TDirectory, IDirectoryEntry>(entry), destination);
        }
        
        protected internal abstract void Create(TDirectory node);

        void IFileSystemStorage.Create(IDirectoryEntry entry)
        {
            Create(Verify<TDirectory, IDirectoryEntry>(entry));
        }
        
        protected internal abstract TDirectory CreateSubdirectory(TDirectory node, String path);

        IDirectoryEntry IFileSystemStorage.CreateSubdirectory(IDirectoryEntry entry, String path)
        {
            return CreateSubdirectory(Verify<TDirectory, IDirectoryEntry>(entry), path);
        }
        
        protected internal abstract void Delete(TDirectory node);

        void IFileSystemStorage.Delete(IDirectoryEntry entry)
        {
            Delete(Verify<TDirectory, IDirectoryEntry>(entry));
        }
        
        protected internal abstract void Delete(TDirectory node, Boolean recursive);

        void IFileSystemStorage.Delete(IDirectoryEntry entry, Boolean recursive)
        {
            Delete(Verify<TDirectory, IDirectoryEntry>(entry), recursive);
        }
        
        protected internal abstract void Refresh(TDirectory node);

        void IFileSystemStorage.Refresh(IDirectoryEntry entry)
        {
            Refresh(Verify<TDirectory, IDirectoryEntry>(entry));
        }
        
        protected internal abstract Int32 GetHashCode(TDirectory? node);

        Int32 IFileSystemStorage.GetHashCode(IDirectoryEntry? entry)
        {
            return GetHashCode(Verify<TDirectory, IDirectoryEntry>(entry));
        }
        
        protected internal abstract Boolean Equals(TDirectory? node, DirectoryInfo? other);

        Boolean IFileSystemStorage.Equals(IDirectoryEntry? entry, DirectoryInfo? other)
        {
            return Equals(Verify<TDirectory, IDirectoryEntry>(entry), other);
        }
        
        protected internal abstract Boolean Equals(TDirectory? node, IDirectoryInfo? other);

        Boolean IFileSystemStorage.Equals(IDirectoryEntry? entry, IDirectoryInfo? other)
        {
            return Equals(Verify<TDirectory, IDirectoryEntry>(entry), other);
        }
        
        [return: NotNullIfNotNull("node")]
        protected internal abstract String? ToString(TDirectory? node);

        [return: NotNullIfNotNull("entry")]
        String? IFileSystemStorage.ToString(IDirectoryEntry? entry)
        {
            return ToString(Verify<TDirectory, IDirectoryEntry>(entry));
        }
#endregion
    }

    public abstract partial class FileSystemStorage<TLink, TFile, TDirectory, TDrive>
    {
        public new abstract class DirectoryNode<TInitializer> : FileSystemStorage.DirectoryNode<TInitializer> where TInitializer : struct, IInitializer<TInitializer>
        {
            public override TDirectory? Parent
            {
                get
                {
                    return (TDirectory?) base.Parent;
                }
            }

            public override TDirectory Root
            {
                get
                {
                    return (TDirectory) base.Root;
                }
            }

            public override TDrive? Drive
            {
                get
                {
                    return (TDrive?) base.Drive;
                }
            }
        }
    }

    public abstract partial class FileSystemStorage
    {
        public abstract class DirectoryNode<TInitializer> : DirectoryNode, IDirectoryNode<TInitializer> where TInitializer : struct, IInitializer<TInitializer>
        {
            TInitializer INode<TInitializer>.Initializer
            {
                init
                {
                    Initialize(Verify(ref value));
                }
            }

            private protected abstract override Boolean IsInitialize { get; }

            protected DirectoryNode()
            {
            }

            protected DirectoryNode(Guid id)
                : base(id)
            {
            }

            protected virtual void Initialize(in TInitializer initializer)
            {
            }

            protected virtual ref TInitializer Verify(ref TInitializer initializer)
            {
                return ref INode<TInitializer>.Verify(this, ref initializer);
            }
        }

        public abstract class DirectoryNode : DirectoryInfoEntry, IDirectoryNode
        {
            public Guid Id { get; }

            IFileSystemStorage IFileSystemNode.Storage
            {
                get
                {
                    return Storage;
                }
                init
                {
                    Storage = value;
                }
            }

            IFileSystemStorage INode.Storage
            {
                get
                {
                    return Storage;
                }
                init
                {
                    Storage = value;
                }
            }

            private protected virtual Boolean IsInitialize
            {
                get
                {
                    // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
                    return Storage is not null;
                }
            }

            Boolean INode.IsInitialize
            {
                get
                {
                    return IsInitialize;
                }
            }

            internal IDirectoryEntry This
            {
                get
                {
                    return this;
                }
            }

            IDirectoryEntry IDirectoryNode.This
            {
                get
                {
                    return This;
                }
            }

            IFileSystemEntry IFileSystemNode.This
            {
                get
                {
                    return This;
                }
            }

            IFileSystemStorageEntry INode.This
            {
                get
                {
                    return This;
                }
            }

            protected DirectoryNode()
                : this(Guid.NewGuid())
            {
            }

            protected DirectoryNode(Guid id)
            {
                Id = id;
            }
        }
    }
}