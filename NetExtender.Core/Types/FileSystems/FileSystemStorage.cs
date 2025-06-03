// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using NetExtender.FileSystems.Interfaces;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.Types;

namespace NetExtender.FileSystems
{
    public abstract partial class FileSystemStorage<TLink, TFile, TDirectory, TDrive> : FileSystemStorage, IFileSystemStorage where TLink : FileSystemStorage<TLink, TFile, TDirectory, TDrive>.LinkNode where TFile : FileSystemStorage<TLink, TFile, TDirectory, TDrive>.FileNode where TDirectory : FileSystemStorage<TLink, TFile, TDirectory, TDrive>.DirectoryNode where TDrive : FileSystemStorage<TLink, TFile, TDirectory, TDrive>.DriveNode
    {
        public sealed override SyncRoot SyncRoot { get; } = SyncRoot.Create();

        public override Boolean IsSynchronized
        {
            get
            {
                return false;
            }
        }

        private readonly String _name;

        protected FileSystemStorage(String name)
        {
            _name = !String.IsNullOrWhiteSpace(name) ? name : throw new ArgumentNullOrWhiteSpaceStringException(name, nameof(name));
        }

        protected FileSystemStorage(Guid id, String name)
            : base(id)
        {
            _name = !String.IsNullOrWhiteSpace(name) ? name : throw new ArgumentNullOrWhiteSpaceStringException(name, nameof(name));
        }
        
        public new virtual String Name()
        {
            return base.Name!;
        }

        private protected sealed override String FileSystemName()
        {
            return _name;
        }

        public new DateTime CreationTime()
        {
            return base.CreationTime;
        }

        private protected sealed override DateTime CreationTimeFileSystem()
        {
            return base.CreationTimeFileSystem();
        }

        public new DateTime CreationTimeUtc()
        {
            return base.CreationTime;
        }

        private protected sealed override DateTime CreationTimeUtcFileSystem()
        {
            return base.CreationTimeFileSystem();
        }

        public new Boolean IsReal()
        {
            return base.IsReal;
        }

        private protected abstract override Boolean IsRealFileSystem();
        
        protected static DateTimeOffset GetUtcDateTimeOffset(DateTime value)
        {
            return value.Kind is DateTimeKind.Unspecified ? DateTime.SpecifyKind(value, DateTimeKind.Utc) : value.ToUniversalTime();
        }

        [return: NotNullIfNotNull("value")]
        protected virtual T? Verify<T>(T? value) where T : class, INode
        {
            if (value is null)
            {
                return null;
            }

            if (!ReferenceEquals(value.Storage, this))
            {
                throw Unverified(value);
            }

            return value.IsInitialize ? value : throw Uninitialized(value);
        }

        [return: NotNullIfNotNull("value")]
        private T? Verify<T, TEntry>(TEntry? value) where T : class, INode where TEntry : class, IFileSystemStorageEntry
        {
            return value switch
            {
                null => Verify(default(T)),
                T node => Verify(node),
                _ => throw new InvalidOperationException($"Entry '{value.GetType()}' must be a '{typeof(T)}' node.")
            };
        }
        
        [return: NotNullIfNotNull("node")]
        protected static Exception? NotSupported(INode? node, String? method)
        {
            return node is not null ? new FileSystemNodeNotSupportMethodException(node, method) : null;
        }

        [return: NotNullIfNotNull("node")]
        protected Exception? Uninitialized(INode? node)
        {
            return Uninitialized(this, node);
        }

        [return: NotNullIfNotNull("node")]
        protected Exception? Uninitialized(INode? node, String? parameter)
        {
            return Uninitialized(this, node, parameter);
        }

        [return: NotNullIfNotNull("node")]
        private static Exception? Uninitialized(IFileSystemStorage storage, INode? node)
        {
            if (storage is null)
            {
                throw new ArgumentNullException(nameof(storage));
            }

            return node is not null ? new FileSystemNodeNotInitializedException(storage, node, null) : null;
        }

        [return: NotNullIfNotNull("node")]
        private static Exception? Uninitialized(IFileSystemStorage storage, INode? node, String? parameter)
        {
            if (storage is null)
            {
                throw new ArgumentNullException(nameof(storage));
            }

            return node is not null ? new FileSystemNodeNotInitializedException(storage, node, parameter) : null;
        }

        [return: NotNullIfNotNull("node")]
        protected Exception? Unverified(INode? node)
        {
            return Unverified(this, node);
        }

        [return: NotNullIfNotNull("node")]
        private static Exception? Unverified(IFileSystemStorage storage, INode? node)
        {
            if (storage is null)
            {
                throw new ArgumentNullException(nameof(storage));
            }

            return node is not null ? new FileSystemNodeNotBelongStorageException(storage, node) : null;
        }

        protected virtual T New<T, TInitializer>(TInitializer initializer) where T : class, INode<TInitializer>, new() where TInitializer : struct, IInitializer<TInitializer>
        {
            return Verify(new T { Storage = this, Initializer = initializer }) switch
            {
                TLink link => Register(link) as T ?? throw new NeverOperationException(),
                TFile file => Register(file) as T ?? throw new NeverOperationException(),
                TDirectory directory => Register(directory) as T ?? throw new NeverOperationException(),
                TDrive drive => Register(drive) as T ?? throw new NeverOperationException(),
                { } node => Register(node)
            };
        }

        [return: NotNullIfNotNull("node")]
        protected virtual T? Register<T>(T? node) where T : class, INode
        {
            return node;
        }

        [return: NotNullIfNotNull("link")]
        protected virtual TLink? Register(TLink? link)
        {
            return Register<TLink>(link);
        }

        [return: NotNullIfNotNull("file")]
        protected virtual TFile? Register(TFile? file)
        {
            return Register<TFile>(file);
        }

        [return: NotNullIfNotNull("directory")]
        protected virtual TDirectory? Register(TDirectory? directory)
        {
            return Register<TDirectory>(directory);
        }

        [return: NotNullIfNotNull("drive")]
        protected virtual TDrive? Register(TDrive? drive)
        {
            return Register<TDrive>(drive);
        }
    }
    
    public abstract partial class FileSystemStorage : FileSystemHandler
    {
        private const Int32 DefaultFileBufferSize = 4096;
        
        private readonly Int32 _buffer = DefaultFileBufferSize;
        protected Int32 FileBufferSize
        {
            get
            {
                return _buffer;
            }
            init
            {
                _buffer = value switch
                {
                    0 => DefaultFileBufferSize,
                    > 0 => value,
                    _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Buffer size must be greater than zero.")
                };
            }
        }

        protected new virtual String? AnyPattern
        {
            get
            {
                return null;
            }
        }

        protected FileSystemStorage()
        {
        }

        protected FileSystemStorage(Guid id)
            : base(id)
        {
        }

        protected internal interface INode<in TInitializer> : INode where TInitializer : struct, IInitializer<TInitializer>
        {
            public TInitializer Initializer { init; }
            
            internal static ref TInitializer Verify(INode<TInitializer> node, ref TInitializer initializer)
            {
                if (node is null)
                {
                    throw new ArgumentNullException(nameof(node));
                }

                if (initializer.IsEmpty)
                {
                    throw new ArgumentNullException(nameof(initializer));
                }
                
                if (node.IsInitialize)
                {
                    throw new AlreadyInitializedException(null, nameof(Initializer));
                }

                return ref initializer;
            }
        }

        protected internal interface INode
        {
            internal IFileSystemStorage Storage { get; init; }
            internal IFileSystemStorageEntry This { get; }
            internal Boolean IsInitialize { get; }
        }

        protected internal interface IFileSystemNode<in TInitializer> : IFileSystemNode, INode<TInitializer> where TInitializer : struct, IInitializer<TInitializer>
        {
        }

        protected internal interface IFileSystemNode : INode, IFileSystemInfo
        {
            internal new IFileSystemStorage Storage { get; init; }
            internal new IFileSystemEntry This { get; }
        }

        protected internal interface ILinkNode<in TInitializer> : ILinkNode, IFileSystemNode<TInitializer> where TInitializer : struct, IInitializer<TInitializer>
        {
        }

        protected internal interface ILinkNode : IFileSystemNode, ILinkInfo
        {
            internal new ILinkEntry This { get; }
        }

        protected internal interface IFileNode<in TInitializer> : IFileNode, IFileSystemNode<TInitializer> where TInitializer : struct, IInitializer<TInitializer>
        {
        }

        protected internal interface IFileNode : IFileSystemNode, IFileInfo
        {
            internal new IFileEntry This { get; }
        }

        protected internal interface IDirectoryNode<in TInitializer> : IDirectoryNode, IFileSystemNode<TInitializer> where TInitializer : struct, IInitializer<TInitializer>
        {
        }

        protected internal interface IDirectoryNode : IFileSystemNode, IDirectoryInfo
        {
            internal new IDirectoryEntry This { get; }
        }

        protected internal interface IDriveNode<in TInitializer> : IDriveNode, INode<TInitializer> where TInitializer : struct, IInitializer<TInitializer>
        {
        }

        protected internal interface IDriveNode : INode, IDriveInfo
        {
            internal new IFileSystemStorage Storage { get; init; }
            internal new IDriveEntry This { get; }
        }

        internal interface IInitializer<TStorage, TInitializer> : IInitializer<TInitializer> where TStorage : class, IFileSystemStorage where TInitializer : struct
        {
            public TStorage Storage { get; init; }
        }

        public interface IInitializer<TInitializer> : IEquatableStruct<TInitializer> where TInitializer : struct
        {
        }
    }

    [Serializable]
    public sealed class FileSystemNodeNotBelongStorageException : InvalidOperationException
    {
        private new const String Message = "Node does not belong to current storage.";
        private const String FormatMessage = "The '{0}' node with file system '{1}' does not belong to current storage '{2}'.";
        
        public FileSystemNodeNotBelongStorageException()
        {
        }

        public FileSystemNodeNotBelongStorageException(String? message)
            : base(message)
        {
        }

        public FileSystemNodeNotBelongStorageException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

        internal FileSystemNodeNotBelongStorageException(IFileSystemStorage? storage, FileSystemStorage.INode? node)
            : base(Format(storage, node))
        {
        }

        public FileSystemNodeNotBelongStorageException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        private static String Format(IFileSystemStorage? owner, FileSystemStorage.INode? node)
        {
            return owner is not null && node is { Storage: { } storage } ? FormatMessage.Format(node.GetType().Name, storage.GetType().Name, owner.GetType().Name) : Message;
        }
    }

    [Serializable]
    public sealed class FileSystemNodeNotInitializedException : NotInitializedException
    {
        private new const String Message = "Node is not initialized.";
        private const String FormatMessage = "The '{0}' node with file system '{1}' and storage '{2}' is not initialized.";
        
        public FileSystemNodeNotInitializedException()
        {
        }

        public FileSystemNodeNotInitializedException(String? message)
            : base(message)
        {
        }

        public FileSystemNodeNotInitializedException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

        public FileSystemNodeNotInitializedException(String? message, String? parameter)
            : base(message, parameter)
        {
        }

        public FileSystemNodeNotInitializedException(String? message, String? parameter, Exception? exception)
            : base(message, parameter, exception)
        {
        }

        internal FileSystemNodeNotInitializedException(IFileSystemStorage? storage, FileSystemStorage.INode? node)
            : base(Format(storage, node))
        {
        }

        internal FileSystemNodeNotInitializedException(IFileSystemStorage? storage, FileSystemStorage.INode? node, String? parameter)
            : base(Format(storage, node), parameter)
        {
        }

        public FileSystemNodeNotInitializedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        private static String Format(IFileSystemStorage? owner, FileSystemStorage.INode? node)
        {
            return owner is not null && node is { Storage: { } storage } ? FormatMessage.Format(node.GetType().Name, storage.GetType().Name, owner.GetType().Name) : Message;
        }
    }

    [Serializable]
    public sealed class FileSystemNodeNotSupportMethodException : NotSupportedException
    {
        private new const String Message = "Node does not support this method.";
        private const String FormatMessage = "Node '{0}' does not support method '{1}' for storage '{2}'.";
        
        public FileSystemNodeNotSupportMethodException()
        {
        }

        public FileSystemNodeNotSupportMethodException(String? message)
            : base(message)
        {
        }

        public FileSystemNodeNotSupportMethodException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

        internal FileSystemNodeNotSupportMethodException(FileSystemStorage.INode? node, String? method)
            : base(Format(node, method))
        {
        }

        public FileSystemNodeNotSupportMethodException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        private static String Format(FileSystemStorage.INode? node, String? method)
        {
            return node is { Storage: { } storage } ? FormatMessage.Format(node.GetType().Name, method ?? StringUtilities.NullString, storage.GetType().Name) : Message;
        }
    }
}