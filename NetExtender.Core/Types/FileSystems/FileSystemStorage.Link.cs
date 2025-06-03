// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.Serialization;
using NetExtender.FileSystems.Interfaces;

namespace NetExtender.FileSystems
{
    public abstract partial class FileSystemStorage<TLink, TFile, TDirectory, TDrive>
    {
#region Storage
        protected internal abstract FileSystemInfo? Info(TLink node);
        
        FileSystemInfo? IFileSystemStorage.Info(ILinkEntry entry)
        {
            return Info(Verify<TLink, ILinkEntry>(entry));
        }
        
        protected internal abstract void GetObjectData(TLink node, SerializationInfo info, StreamingContext context);
        
        void IFileSystemStorage.GetObjectData(ILinkEntry entry, SerializationInfo info, StreamingContext context)
        {
            GetObjectData(Verify<TLink, ILinkEntry>(entry), info, context);
        }
        
        protected internal new abstract String Name(TLink node);

        String IFileSystemStorage.Name(ILinkEntry entry)
        {
            return Name(Verify<TLink, ILinkEntry>(entry));
        }
        
        protected internal abstract String FullName(TLink node);

        String IFileSystemStorage.FullName(ILinkEntry entry)
        {
            return FullName(Verify<TLink, ILinkEntry>(entry));
        }
        
        protected internal abstract String Extension(TLink node);

        String IFileSystemStorage.Extension(ILinkEntry entry)
        {
            return Extension(Verify<TLink, ILinkEntry>(entry));
        }
        
        protected internal new abstract Boolean IsReal(TLink node);

        Boolean IFileSystemStorage.IsReal(ILinkEntry entry)
        {
            return IsReal(Verify<TLink, ILinkEntry>(entry));
        }
        
        protected internal abstract Boolean Exists(TLink node);

        Boolean IFileSystemStorage.Exists(ILinkEntry entry)
        {
            return Exists(Verify<TLink, ILinkEntry>(entry));
        }
        
        protected internal abstract FileAttributes Attributes(TLink node);

        FileAttributes IFileSystemStorage.Attributes(ILinkEntry entry)
        {
            return Attributes(Verify<TLink, ILinkEntry>(entry));
        }

        protected internal abstract void Attributes(TLink node, FileAttributes value);

        void IFileSystemStorage.Attributes(ILinkEntry entry, FileAttributes value)
        {
            Attributes(Verify<TLink, ILinkEntry>(entry), value);
        }
        
        protected internal abstract String? LinkTarget(TLink node);
        
        String? IFileSystemStorage.LinkTarget(ILinkEntry entry)
        {
            return LinkTarget(Verify<TLink, ILinkEntry>(entry));
        }
        
        protected internal new abstract DateTime CreationTime(TLink node);

        DateTime IFileSystemStorage.CreationTime(ILinkEntry entry)
        {
            return CreationTime(Verify<TLink, ILinkEntry>(entry));
        }
        
        protected internal new abstract void CreationTime(TLink node, DateTime value);

        void IFileSystemStorage.CreationTime(ILinkEntry entry, DateTime value)
        {
            CreationTime(Verify<TLink, ILinkEntry>(entry), value);
        }
        
        protected internal new abstract DateTime CreationTimeUtc(TLink node);

        DateTime IFileSystemStorage.CreationTimeUtc(ILinkEntry entry)
        {
            return CreationTimeUtc(Verify<TLink, ILinkEntry>(entry));
        }
        
        protected internal new abstract void CreationTimeUtc(TLink node, DateTime value);

        void IFileSystemStorage.CreationTimeUtc(ILinkEntry entry, DateTime value)
        {
            CreationTimeUtc(Verify<TLink, ILinkEntry>(entry), value);
        }
        
        protected internal abstract DateTime LastAccessTime(TLink node);

        DateTime IFileSystemStorage.LastAccessTime(ILinkEntry entry)
        {
            return LastAccessTime(Verify<TLink, ILinkEntry>(entry));
        }
        
        protected internal abstract void LastAccessTime(TLink node, DateTime value);

        void IFileSystemStorage.LastAccessTime(ILinkEntry entry, DateTime value)
        {
            LastAccessTime(Verify<TLink, ILinkEntry>(entry), value);
        }
        
        protected internal abstract DateTime LastAccessTimeUtc(TLink node);

        DateTime IFileSystemStorage.LastAccessTimeUtc(ILinkEntry entry)
        {
            return LastAccessTimeUtc(Verify<TLink, ILinkEntry>(entry));
        }
        
        protected internal abstract void LastAccessTimeUtc(TLink node, DateTime value);

        void IFileSystemStorage.LastAccessTimeUtc(ILinkEntry entry, DateTime value)
        {
            LastAccessTimeUtc(Verify<TLink, ILinkEntry>(entry), value);
        }
        
        protected internal abstract DateTime LastWriteTime(TLink node);

        DateTime IFileSystemStorage.LastWriteTime(ILinkEntry entry)
        {
            return LastWriteTime(Verify<TLink, ILinkEntry>(entry));
        }
        
        protected internal abstract void LastWriteTime(TLink node, DateTime value);

        void IFileSystemStorage.LastWriteTime(ILinkEntry entry, DateTime value)
        {
            LastWriteTime(Verify<TLink, ILinkEntry>(entry), value);
        }
        
        protected internal abstract DateTime LastWriteTimeUtc(TLink node);

        DateTime IFileSystemStorage.LastWriteTimeUtc(ILinkEntry entry)
        {
            return LastWriteTimeUtc(Verify<TLink, ILinkEntry>(entry));
        }
        
        protected internal abstract void LastWriteTimeUtc(TLink node, DateTime value);

        void IFileSystemStorage.LastWriteTimeUtc(ILinkEntry entry, DateTime value)
        {
            LastWriteTimeUtc(Verify<TLink, ILinkEntry>(entry), value);
        }
        
        protected internal abstract String? DirectoryName(TLink node);

        String? IFileSystemStorage.DirectoryName(ILinkEntry entry)
        {
            return DirectoryName(Verify<TLink, ILinkEntry>(entry));
        }
        
        protected internal abstract TDirectory? GetDirectory(TLink node);

        IDirectoryEntry? IFileSystemStorage.GetDirectory(ILinkEntry entry)
        {
            return GetDirectory(Verify<TLink, ILinkEntry>(entry));
        }
        
        protected internal new abstract TDrive? Drive(TLink node);

        IDriveEntry? IFileSystemStorage.Drive(ILinkEntry entry)
        {
            return Drive(Verify<TLink, ILinkEntry>(entry));
        }
        
        protected internal abstract Boolean CreateAsSymbolicLink(TLink node, String target);

        Boolean IFileSystemStorage.CreateAsSymbolicLink(ILinkEntry entry, String target)
        {
            return CreateAsSymbolicLink(Verify<TLink, ILinkEntry>(entry), target);
        }
        
        protected internal abstract IFileSystemNode? ResolveLinkTarget(TLink node);

        IFileSystemEntry? IFileSystemStorage.ResolveLinkTarget(ILinkEntry entry)
        {
            return ResolveLinkTarget(Verify<TLink, ILinkEntry>(entry))?.This;
        }
        
        protected internal abstract IFileSystemNode? ResolveLinkTarget(TLink node, Boolean final);

        IFileSystemEntry? IFileSystemStorage.ResolveLinkTarget(ILinkEntry entry, Boolean final)
        {
            return ResolveLinkTarget(Verify<TLink, ILinkEntry>(entry), final)?.This;
        }
        
        protected internal abstract void Create(TLink node);

        void IFileSystemStorage.Create(ILinkEntry entry)
        {
            Create(Verify<TLink, ILinkEntry>(entry));
        }
        
        protected internal abstract void Delete(TLink node);

        void IFileSystemStorage.Delete(ILinkEntry entry)
        {
            Delete(Verify<TLink, ILinkEntry>(entry));
        }
        
        protected internal abstract void Refresh(TLink node);

        void IFileSystemStorage.Refresh(ILinkEntry entry)
        {
            Refresh(Verify<TLink, ILinkEntry>(entry));
        }
        
        protected internal abstract Int32 GetHashCode(TLink? node);

        Int32 IFileSystemStorage.GetHashCode(ILinkEntry? entry)
        {
            return GetHashCode(Verify<TLink, ILinkEntry>(entry));
        }
        
        protected internal abstract Boolean Equals(TLink? node, FileSystemInfo? other);

        Boolean IFileSystemStorage.Equals(ILinkEntry? entry, FileSystemInfo? other)
        {
            return Equals(Verify<TLink, ILinkEntry>(entry), other);
        }
        
        protected internal abstract Boolean Equals(TLink? node, IFileSystemInfo? other);

        Boolean IFileSystemStorage.Equals(ILinkEntry? entry, IFileSystemInfo? other)
        {
            return Equals(Verify<TLink, ILinkEntry>(entry), other);
        }
        
        [return: NotNullIfNotNull("node")]
        protected internal abstract String? ToString(TLink? node);

        [return: NotNullIfNotNull("entry")]
        String? IFileSystemStorage.ToString(ILinkEntry? entry)
        {
            return ToString(Verify<TLink, ILinkEntry>(entry));
        }
#endregion
    }

    public abstract partial class FileSystemStorage<TLink, TFile, TDirectory, TDrive>
    {
        public new abstract class LinkNode<TInitializer> : FileSystemStorage.LinkNode<TInitializer> where TInitializer : struct, IInitializer<TInitializer>
        {
            public override TDirectory? Directory
            {
                get
                {
                    return (TDirectory?) base.Directory;
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
        public abstract class LinkNode<TInitializer> : LinkNode, ILinkNode<TInitializer> where TInitializer : struct, IInitializer<TInitializer>
        {
            TInitializer INode<TInitializer>.Initializer
            {
                init
                {
                    Initialize(Verify(ref value));
                }
            }

            private protected abstract override Boolean IsInitialize { get; }

            protected LinkNode()
            {
            }

            protected LinkNode(Guid id)
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
        
        public abstract class LinkNode : LinkInfoEntry, ILinkNode
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

            internal ILinkEntry This
            {
                get
                {
                    return this;
                }
            }

            ILinkEntry ILinkNode.This
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

            protected LinkNode()
                : this(Guid.NewGuid())
            {
            }

            protected LinkNode(Guid id)
            {
                Id = id;
            }
        }
    }
}