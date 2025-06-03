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
        protected internal abstract DriveInfo? Info(TDrive node);
        
        DriveInfo? IFileSystemStorage.Info(IDriveEntry entry)
        {
            return Info(Verify<TDrive, IDriveEntry>(entry));
        }
        
        protected internal abstract void GetObjectData(TDrive node, SerializationInfo info, StreamingContext context);
        
        void IFileSystemStorage.GetObjectData(IDriveEntry entry, SerializationInfo info, StreamingContext context)
        {
            GetObjectData(Verify<TDrive, IDriveEntry>(entry), info, context);
        }
        
        protected new abstract String Name(TDrive node);

        String IFileSystemStorage.Name(IDriveEntry entry)
        {
            return Name(Verify<TDrive, IDriveEntry>(entry));
        }
        
        protected internal abstract String FullName(TDrive node);

        String IFileSystemStorage.FullName(IDriveEntry entry)
        {
            return FullName(Verify<TDrive, IDriveEntry>(entry));
        }
        
        protected new abstract Boolean IsReal(TDrive node);

        Boolean IFileSystemStorage.IsReal(IDriveEntry entry)
        {
            return IsReal(Verify<TDrive, IDriveEntry>(entry));
        }
        
        protected internal abstract Boolean IsReady(TDrive node);

        Boolean IFileSystemStorage.IsReady(IDriveEntry entry)
        {
            return IsReady(Verify<TDrive, IDriveEntry>(entry));
        }
        
        protected internal abstract DriveType DriveType(TDrive node);

        DriveType IFileSystemStorage.DriveType(IDriveEntry entry)
        {
            return DriveType(Verify<TDrive, IDriveEntry>(entry));
        }
        
        protected internal abstract String DriveFormat(TDrive node);

        String IFileSystemStorage.DriveFormat(IDriveEntry entry)
        {
            return DriveFormat(Verify<TDrive, IDriveEntry>(entry));
        }
        
        protected internal abstract String? VolumeLabel(TDrive node);

        String? IFileSystemStorage.VolumeLabel(IDriveEntry entry)
        {
            return VolumeLabel(Verify<TDrive, IDriveEntry>(entry));
        }

        protected internal abstract void VolumeLabel(TDrive node, String? value);

        void IFileSystemStorage.VolumeLabel(IDriveEntry entry, String? value)
        {
            VolumeLabel(Verify<TDrive, IDriveEntry>(entry), value);
        }
        
        protected internal abstract Int64 AvailableFreeSpace(TDrive node);

        Int64 IFileSystemStorage.AvailableFreeSpace(IDriveEntry entry)
        {
            return AvailableFreeSpace(Verify<TDrive, IDriveEntry>(entry));
        }
        
        protected internal abstract Int64 TotalFreeSpace(TDrive node);

        Int64 IFileSystemStorage.TotalFreeSpace(IDriveEntry entry)
        {
            return TotalFreeSpace(Verify<TDrive, IDriveEntry>(entry));
        }
        
        protected internal abstract Int64 TotalSize(TDrive node);

        Int64 IFileSystemStorage.TotalSize(IDriveEntry entry)
        {
            return TotalSize(Verify<TDrive, IDriveEntry>(entry));
        }
        
        protected internal abstract TDirectory RootDirectory(TDrive node);

        IDirectoryEntry IFileSystemStorage.RootDirectory(IDriveEntry entry)
        {
            return RootDirectory(Verify<TDrive, IDriveEntry>(entry));
        }
        
        protected internal abstract Int32 GetHashCode(TDrive? node);

        Int32 IFileSystemStorage.GetHashCode(IDriveEntry? entry)
        {
            return GetHashCode(Verify<TDrive, IDriveEntry>(entry));
        }
        
        protected internal abstract Boolean Equals(TDrive? node, DriveInfo? other);

        Boolean IFileSystemStorage.Equals(IDriveEntry? entry, DriveInfo? other)
        {
            return Equals(Verify<TDrive, IDriveEntry>(entry), other);
        }
        
        protected internal abstract Boolean Equals(TDrive? node, IDriveInfo? other);

        Boolean IFileSystemStorage.Equals(IDriveEntry? entry, IDriveInfo? other)
        {
            return Equals(Verify<TDrive, IDriveEntry>(entry), other);
        }
        
        [return: NotNullIfNotNull("node")]
        protected internal abstract String? ToString(TDrive? node);

        [return: NotNullIfNotNull("entry")]
        String? IFileSystemStorage.ToString(IDriveEntry? entry)
        {
            return ToString(Verify<TDrive, IDriveEntry>(entry));
        }
#endregion
    }

    public abstract partial class FileSystemStorage<TLink, TFile, TDirectory, TDrive>
    {
        public new abstract class DriveNode<TInitializer> : FileSystemStorage.DriveNode<TInitializer> where TInitializer : struct, IInitializer<TInitializer>
        {
            public override TDirectory RootDirectory
            {
                get
                {
                    return (TDirectory) base.RootDirectory;
                }
            }
        }
    }

    public abstract partial class FileSystemStorage
    {
        public abstract class DriveNode<TInitializer> : DriveNode, IDriveNode<TInitializer> where TInitializer : struct, IInitializer<TInitializer>
        {
            TInitializer INode<TInitializer>.Initializer
            {
                init
                {
                    Initialize(Verify(ref value));
                }
            }

            private protected abstract override Boolean IsInitialize { get; }

            protected DriveNode()
            {
            }

            protected DriveNode(Guid id)
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
        
        public abstract class DriveNode : DriveInfoEntry, IDriveNode
        {
            public Guid Id { get; }

            IFileSystemStorage IDriveNode.Storage
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

            internal IDriveEntry This
            {
                get
                {
                    return this;
                }
            }

            IDriveEntry IDriveNode.This
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

            protected DriveNode()
                : this(Guid.NewGuid())
            {
            }

            protected DriveNode(Guid id)
            {
                Id = id;
            }
        }
    }
}