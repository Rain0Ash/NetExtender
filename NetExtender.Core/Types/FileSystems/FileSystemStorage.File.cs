// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.FileSystems.Interfaces;

namespace NetExtender.FileSystems
{
    public abstract partial class FileSystemStorage<TLink, TFile, TDirectory, TDrive>
    {
        public override FileStream Open(String path, FileMode mode)
        {
            return Open(path, mode, mode == FileMode.Append ? FileAccess.Write : FileAccess.ReadWrite);
        }

        public override FileStream Open(String path, FileMode mode, FileAccess access)
        {
            return Open(path, mode, access, FileShare.None);
        }

        public abstract override FileStream Open(String path, FileMode mode, FileAccess access, FileShare share);
        public abstract override FileStream Open(String path, FileStreamOptions options);

        public override FileStream OpenRead(String path)
        {
            return Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        }

        public override FileStream OpenWrite(String path)
        {
            return Open(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
        }

        public abstract override StreamReader OpenText(String path);

        public override FileStream Create(String path, Int32 buffer)
        {
            return Create(path, buffer, FileOptions.None);
        }

        public abstract override FileStream Create(String path, Int32 buffer, FileOptions options);
        public abstract override StreamWriter CreateText(String path);
        public abstract override StreamWriter AppendText(String path);

        public abstract override Byte[] ReadAllBytes(String path);
        public abstract override Task<Byte[]> ReadAllBytesAsync(String path, CancellationToken token);

        public override String ReadAllText(String path)
        {
            return ReadAllText(path, ReadEncoding);
        }

        public abstract override String ReadAllText(String path, Encoding? encoding);

        public override Task<String> ReadAllTextAsync(String path, CancellationToken token)
        {
            return ReadAllTextAsync(path, ReadEncoding, token);
        }

        public abstract override Task<String> ReadAllTextAsync(String path, Encoding? encoding, CancellationToken token);

        public override IEnumerable<String> ReadLines(String path)
        {
            return ReadAllLines(path);
        }

        public override IEnumerable<String> ReadLines(String path, Encoding? encoding)
        {
            return ReadAllLines(path, encoding);
        }

        public override String[] ReadAllLines(String path)
        {
            return ReadAllLines(path, ReadEncoding);
        }

        public abstract override String[] ReadAllLines(String path, Encoding? encoding);

        public override Task<String[]> ReadAllLinesAsync(String path, CancellationToken token)
        {
            return ReadAllLinesAsync(path, ReadEncoding, token);
        }

        public abstract override Task<String[]> ReadAllLinesAsync(String path, Encoding? encoding, CancellationToken token);

        public override void AppendAllText(String path, String? contents)
        {
            AppendAllText(path, contents, WriteEncoding);
        }

        public abstract override void AppendAllText(String path, String? contents, Encoding? encoding);

        public override Task AppendAllTextAsync(String path, String? contents, CancellationToken token)
        {
            return AppendAllTextAsync(path, contents, WriteEncoding, token);
        }

        public abstract override Task AppendAllTextAsync(String path, String? contents, Encoding? encoding, CancellationToken token);

        public override void AppendAllLines(String path, IEnumerable<String?>? contents)
        {
            AppendAllLines(path, contents, WriteEncoding);
        }

        public abstract override void AppendAllLines(String path, IEnumerable<String?>? contents, Encoding? encoding);

        public override Task AppendAllLinesAsync(String path, IEnumerable<String?>? contents, CancellationToken token)
        {
            return AppendAllLinesAsync(path, contents, WriteEncoding, token);
        }

        public abstract override Task AppendAllLinesAsync(String path, IEnumerable<String?>? contents, Encoding? encoding, CancellationToken token);
        public abstract override void WriteAllBytes(String path, params Byte[]? bytes);
        public abstract override Task WriteAllBytesAsync(String path, Byte[]? bytes, CancellationToken token);

        public override void WriteAllText(String path, String? contents)
        {
            WriteAllText(path, contents, WriteEncoding);
        }

        public abstract override void WriteAllText(String path, String? contents, Encoding? encoding);

        public override Task WriteAllTextAsync(String path, String? contents, CancellationToken token)
        {
            return WriteAllTextAsync(path, contents, WriteEncoding, token);
        }

        public abstract override Task WriteAllTextAsync(String path, String? contents, Encoding? encoding, CancellationToken token);

        public override void WriteAllLines(String path, params String?[]? contents)
        {
            WriteAllLines(path, (IEnumerable<String?>?) contents);
        }

        public override void WriteAllLines(String path, IEnumerable<String?>? contents)
        {
            WriteAllLines(path, contents, WriteEncoding);
        }

        public override void WriteAllLines(String path, String?[]? contents, Encoding? encoding)
        {
            WriteAllLines(path, (IEnumerable<String?>?) contents, encoding);
        }
        
        public abstract override void WriteAllLines(String path, IEnumerable<String?>? contents, Encoding? encoding);

        public override Task WriteAllLinesAsync(String path, IEnumerable<String?>? contents, CancellationToken token)
        {
            return WriteAllLinesAsync(path, contents, WriteEncoding, token);
        }

        public abstract override Task WriteAllLinesAsync(String path, IEnumerable<String?>? contents, Encoding? encoding, CancellationToken token);

#region Storage
        protected internal abstract FileInfo? Info(TFile node);

        FileInfo? IFileSystemStorage.Info(IFileEntry entry)
        {
            return Info(Verify<TFile, IFileEntry>(entry));
        }
        
        protected internal abstract void GetObjectData(TFile node, SerializationInfo info, StreamingContext context);

        void IFileSystemStorage.GetObjectData(IFileEntry entry, SerializationInfo info, StreamingContext context)
        {
            GetObjectData(Verify<TFile, IFileEntry>(entry), info, context);
        }
        
        protected internal new abstract String Name(TFile node);

        String IFileSystemStorage.Name(IFileEntry entry)
        {
            return Name(Verify<TFile, IFileEntry>(entry));
        }
        
        protected internal abstract String FullName(TFile node);

        String IFileSystemStorage.FullName(IFileEntry entry)
        {
            return FullName(Verify<TFile, IFileEntry>(entry));
        }
        
        protected internal abstract String Extension(TFile node);

        String IFileSystemStorage.Extension(IFileEntry entry)
        {
            return Extension(Verify<TFile, IFileEntry>(entry));
        }
        
        protected internal new abstract Boolean IsReal(TFile node);

        Boolean IFileSystemStorage.IsReal(IFileEntry entry)
        {
            return IsReal(Verify<TFile, IFileEntry>(entry));
        }
        
        protected internal abstract Boolean Exists(TFile node);

        Boolean IFileSystemStorage.Exists(IFileEntry entry)
        {
            return Exists(Verify<TFile, IFileEntry>(entry));
        }
        
        protected internal abstract Int64 Length(TFile node);

        Int64 IFileSystemStorage.Length(IFileEntry entry)
        {
            return Length(Verify<TFile, IFileEntry>(entry));
        }
        
        protected internal abstract Boolean IsReadOnly(TFile node);

        Boolean IFileSystemStorage.IsReadOnly(IFileEntry entry)
        {
            return IsReadOnly(Verify<TFile, IFileEntry>(entry));
        }
        
        protected internal abstract void IsReadOnly(TFile node, Boolean value);

        void IFileSystemStorage.IsReadOnly(IFileEntry entry, Boolean value)
        {
            IsReadOnly(Verify<TFile, IFileEntry>(entry), value);
        }
        
        protected internal abstract FileAttributes Attributes(TFile node);

        FileAttributes IFileSystemStorage.Attributes(IFileEntry entry)
        {
            return Attributes(Verify<TFile, IFileEntry>(entry));
        }
        
        protected internal abstract void Attributes(TFile node, FileAttributes value);

        void IFileSystemStorage.Attributes(IFileEntry entry, FileAttributes value)
        {
            Attributes(Verify<TFile, IFileEntry>(entry), value);
        }
        
        protected internal abstract String? LinkTarget(TFile node);

        String? IFileSystemStorage.LinkTarget(IFileEntry entry)
        {
            return LinkTarget(Verify<TFile, IFileEntry>(entry));
        }
        
        protected internal new abstract DateTime CreationTime(TFile node);

        DateTime IFileSystemStorage.CreationTime(IFileEntry entry)
        {
            return CreationTime(Verify<TFile, IFileEntry>(entry));
        }
        
        protected internal new abstract void CreationTime(TFile node, DateTime value);

        void IFileSystemStorage.CreationTime(IFileEntry entry, DateTime value)
        {
            CreationTime(Verify<TFile, IFileEntry>(entry), value);
        }
        
        protected internal new abstract DateTime CreationTimeUtc(TFile node);

        DateTime IFileSystemStorage.CreationTimeUtc(IFileEntry entry)
        {
            return CreationTimeUtc(Verify<TFile, IFileEntry>(entry));
        }
        
        protected internal new abstract void CreationTimeUtc(TFile node, DateTime value);

        void IFileSystemStorage.CreationTimeUtc(IFileEntry entry, DateTime value)
        {
            CreationTimeUtc(Verify<TFile, IFileEntry>(entry), value);
        }
        
        protected internal abstract DateTime LastAccessTime(TFile node);

        DateTime IFileSystemStorage.LastAccessTime(IFileEntry entry)
        {
            return LastAccessTime(Verify<TFile, IFileEntry>(entry));
        }
        
        protected internal abstract void LastAccessTime(TFile node, DateTime value);

        void IFileSystemStorage.LastAccessTime(IFileEntry entry, DateTime value)
        {
            LastAccessTime(Verify<TFile, IFileEntry>(entry), value);
        }
        
        protected internal abstract DateTime LastAccessTimeUtc(TFile node);

        DateTime IFileSystemStorage.LastAccessTimeUtc(IFileEntry entry)
        {
            return LastAccessTimeUtc(Verify<TFile, IFileEntry>(entry));
        }
        
        protected internal abstract void LastAccessTimeUtc(TFile node, DateTime value);

        void IFileSystemStorage.LastAccessTimeUtc(IFileEntry entry, DateTime value)
        {
            LastAccessTimeUtc(Verify<TFile, IFileEntry>(entry), value);
        }
        
        protected internal abstract DateTime LastWriteTime(TFile node);

        DateTime IFileSystemStorage.LastWriteTime(IFileEntry entry)
        {
            return LastWriteTime(Verify<TFile, IFileEntry>(entry));
        }
        
        protected internal abstract void LastWriteTime(TFile node, DateTime value);

        void IFileSystemStorage.LastWriteTime(IFileEntry entry, DateTime value)
        {
            LastWriteTime(Verify<TFile, IFileEntry>(entry), value);
        }
        
        protected internal abstract DateTime LastWriteTimeUtc(TFile node);

        DateTime IFileSystemStorage.LastWriteTimeUtc(IFileEntry entry)
        {
            return LastWriteTimeUtc(Verify<TFile, IFileEntry>(entry));
        }
        
        protected internal abstract void LastWriteTimeUtc(TFile node, DateTime value);

        void IFileSystemStorage.LastWriteTimeUtc(IFileEntry entry, DateTime value)
        {
            LastWriteTimeUtc(Verify<TFile, IFileEntry>(entry), value);
        }
        
        protected internal abstract String? DirectoryName(TFile node);

        String? IFileSystemStorage.DirectoryName(IFileEntry entry)
        {
            return DirectoryName(Verify<TFile, IFileEntry>(entry));
        }
        
        protected internal abstract TDirectory? GetDirectory(TFile node);

        IDirectoryEntry? IFileSystemStorage.GetDirectory(IFileEntry entry)
        {
            return GetDirectory(Verify<TFile, IFileEntry>(entry));
        }
        
        protected internal new abstract TDrive? Drive(TFile node);

        IDriveEntry? IFileSystemStorage.Drive(IFileEntry entry)
        {
            return Drive(Verify<TFile, IFileEntry>(entry));
        }
        
        protected internal abstract Boolean CreateAsSymbolicLink(TFile node, String target);

        Boolean IFileSystemStorage.CreateAsSymbolicLink(IFileEntry entry, String target)
        {
            return CreateAsSymbolicLink(Verify<TFile, IFileEntry>(entry), target);
        }
        
        protected internal abstract IFileSystemNode? ResolveLinkTarget(TFile node);

        IFileSystemEntry? IFileSystemStorage.ResolveLinkTarget(IFileEntry entry)
        {
            return ResolveLinkTarget(Verify<TFile, IFileEntry>(entry))?.This;
        }
        
        protected internal abstract IFileSystemNode? ResolveLinkTarget(TFile node, Boolean final);

        IFileSystemEntry? IFileSystemStorage.ResolveLinkTarget(IFileEntry entry, Boolean final)
        {
            return ResolveLinkTarget(Verify<TFile, IFileEntry>(entry), final)?.This;
        }
        
        protected internal abstract FileStream Open(TFile node, FileMode mode);

        FileStream IFileSystemStorage.Open(IFileEntry entry, FileMode mode)
        {
            return Open(Verify<TFile, IFileEntry>(entry), mode);
        }
        
        protected internal abstract FileStream Open(TFile node, FileMode mode, FileAccess access);

        FileStream IFileSystemStorage.Open(IFileEntry entry, FileMode mode, FileAccess access)
        {
            return Open(Verify<TFile, IFileEntry>(entry), mode, access);
        }
        
        protected internal abstract FileStream Open(TFile node, FileMode mode, FileAccess access, FileShare share);

        FileStream IFileSystemStorage.Open(IFileEntry entry, FileMode mode, FileAccess access, FileShare share)
        {
            return Open(Verify<TFile, IFileEntry>(entry), mode, access, share);
        }
        
        protected internal abstract FileStream Open(TFile node, FileStreamOptions options);

        FileStream IFileSystemStorage.Open(IFileEntry entry, FileStreamOptions options)
        {
            return Open(Verify<TFile, IFileEntry>(entry), options);
        }
        
        protected internal abstract FileStream OpenRead(TFile node);

        FileStream IFileSystemStorage.OpenRead(IFileEntry entry)
        {
            return OpenRead(Verify<TFile, IFileEntry>(entry));
        }
        
        protected internal abstract FileStream OpenWrite(TFile node);

        FileStream IFileSystemStorage.OpenWrite(IFileEntry entry)
        {
            return OpenWrite(Verify<TFile, IFileEntry>(entry));
        }
        
        protected internal abstract StreamReader OpenText(TFile node);

        StreamReader IFileSystemStorage.OpenText(IFileEntry entry)
        {
            return OpenText(Verify<TFile, IFileEntry>(entry));
        }
        
        protected internal abstract FileStream Create(TFile node);

        FileStream IFileSystemStorage.Create(IFileEntry entry)
        {
            return Create(Verify<TFile, IFileEntry>(entry));
        }
        
        protected internal abstract StreamWriter AppendText(TFile node);

        StreamWriter IFileSystemStorage.AppendText(IFileEntry entry)
        {
            return AppendText(Verify<TFile, IFileEntry>(entry));
        }

        protected internal abstract Boolean Encrypt(TFile node);

        Boolean IFileSystemStorage.Encrypt(IFileEntry entry)
        {
            return Encrypt(Verify<TFile, IFileEntry>(entry));
        }

        protected internal abstract Boolean Decrypt(TFile node);

        Boolean IFileSystemStorage.Decrypt(IFileEntry entry)
        {
            return Decrypt(Verify<TFile, IFileEntry>(entry));
        }

        protected internal abstract void MoveTo(TFile node, String destination);

        void IFileSystemStorage.MoveTo(IFileEntry entry, String destination)
        {
            MoveTo(Verify<TFile, IFileEntry>(entry), destination);
        }
        
        protected internal abstract void MoveTo(TFile node, String destination, Boolean overwrite);

        void IFileSystemStorage.MoveTo(IFileEntry entry, String destination, Boolean overwrite)
        {
            MoveTo(Verify<TFile, IFileEntry>(entry), destination, overwrite);
        }
        
        protected internal abstract TFile CopyTo(TFile node, String destination);

        IFileEntry IFileSystemStorage.CopyTo(IFileEntry entry, String destination)
        {
            return CopyTo(Verify<TFile, IFileEntry>(entry), destination);
        }
        
        protected internal abstract TFile CopyTo(TFile node, String destination, Boolean overwrite);

        IFileEntry IFileSystemStorage.CopyTo(IFileEntry entry, String destination, Boolean overwrite)
        {
            return CopyTo(Verify<TFile, IFileEntry>(entry), destination, overwrite);
        }
        
        protected internal abstract TFile Replace(TFile node, String destination, String? backup);

        IFileEntry IFileSystemStorage.Replace(IFileEntry entry, String destination, String? backup)
        {
            return Replace(Verify<TFile, IFileEntry>(entry), destination, backup);
        }
        
        protected internal abstract TFile Replace(TFile node, String destination, String? backup, Boolean suppress);

        IFileEntry IFileSystemStorage.Replace(IFileEntry entry, String destination, String? backup, Boolean suppress)
        {
            return Replace(Verify<TFile, IFileEntry>(entry), destination, backup, suppress);
        }
        
        protected internal abstract void Delete(TFile node);

        void IFileSystemStorage.Delete(IFileEntry entry)
        {
            Delete(Verify<TFile, IFileEntry>(entry));
        }
        
        protected internal abstract void Refresh(TFile node);

        void IFileSystemStorage.Refresh(IFileEntry entry)
        {
            Refresh(Verify<TFile, IFileEntry>(entry));
        }
        
        protected internal abstract Int32 GetHashCode(TFile? node);

        Int32 IFileSystemStorage.GetHashCode(IFileEntry? entry)
        {
            return GetHashCode(Verify<TFile, IFileEntry>(entry));
        }
        
        protected internal abstract Boolean Equals(TFile? node, FileInfo? other);

        Boolean IFileSystemStorage.Equals(IFileEntry? entry, FileInfo? other)
        {
            return Equals(Verify<TFile, IFileEntry>(entry), other);
        }
        
        protected internal abstract Boolean Equals(TFile? node, IFileInfo? other);

        Boolean IFileSystemStorage.Equals(IFileEntry? entry, IFileInfo? other)
        {
            return Equals(Verify<TFile, IFileEntry>(entry), other);
        }
        
        [return: NotNullIfNotNull("node")]
        protected internal abstract String? ToString(TFile? node);

        [return: NotNullIfNotNull("entry")]
        String? IFileSystemStorage.ToString(IFileEntry? entry)
        {
            return ToString(Verify<TFile, IFileEntry>(entry));
        }
#endregion
    }

    public abstract partial class FileSystemStorage<TLink, TFile, TDirectory, TDrive>
    {
        public new abstract class FileNode<TInitializer> : FileSystemStorage.FileNode<TInitializer> where TInitializer : struct, IInitializer<TInitializer>
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
        public abstract class FileNode<TInitializer> : FileNode, IFileNode<TInitializer> where TInitializer : struct, IInitializer<TInitializer>
        {
            TInitializer INode<TInitializer>.Initializer
            {
                init
                {
                    Initialize(Verify(ref value));
                }
            }

            private protected abstract override Boolean IsInitialize { get; }

            protected FileNode()
            {
            }

            protected FileNode(Guid id)
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
        
        public abstract class FileNode : FileInfoEntry, IFileNode
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

            internal IFileEntry This
            {
                get
                {
                    return this;
                }
            }

            IFileEntry IFileNode.This
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

            protected FileNode()
                : this(Guid.NewGuid())
            {
            }

            protected FileNode(Guid id)
            {
                Id = id;
            }
        }
    }
}