// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;
using System.IO;
using System.Security;
using Microsoft.Win32.SafeHandles;
using NetExtender.Utils.IO;
using NetExtender.Utils.Types;
using NetExtender.Utils.Windows.IO;
using NetExtender.Utils.Windows.IO.NTFS;
using NetExtender.Windows.IO;

namespace NetExtender.IO.FileSystem.NTFS.DataStreams
{
    /// <summary>
    /// Represents the details of an alternative data stream.
    /// </summary>
    public sealed class AlternateDataStreamInfo : IEquatable<AlternateDataStreamInfo>
    {
        /// <summary>
        /// The equality operator.
        /// </summary>
        /// <param name="first">
        /// The first object.
        /// </param>
        /// <param name="second">
        /// The second object.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the two objects are equal;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static Boolean operator ==(AlternateDataStreamInfo first, AlternateDataStreamInfo second)
        {
            return Equals(first, second);
        }

        /// <summary>
        /// The inequality operator.
        /// </summary>
        /// <param name="first">
        /// The first object.
        /// </param>
        /// <param name="second">
        /// The second object.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the two objects are not equal;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static Boolean operator !=(AlternateDataStreamInfo first, AlternateDataStreamInfo second)
        {
            return !Equals(first, second);
        }
        
        /// <summary>
        /// Returns the full path of this stream.
        /// </summary>
        /// <value>
        /// The full path of this stream.
        /// </value>
        public String FullPath { get; }

        /// <summary>
        /// Returns the full path of the file which contains the stream.
        /// </summary>
        /// <value>
        /// The full file-system path of the file which contains the stream.
        /// </value>
        public String Path { get; }

        /// <summary>
        /// Returns the name of the stream.
        /// </summary>
        /// <value>
        /// The name of the stream.
        /// </value>
        public String Name { get; }

        /// <summary>
        /// Returns a flag indicating whether the specified stream exists.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the stream exists;
        /// otherwise, <see langword="false"/>.
        /// </value>
        public Boolean Exists { get; }

        /// <summary>
        /// Returns the size of the stream, in bytes.
        /// </summary>
        /// <value>
        /// The size of the stream, in bytes.
        /// </value>
        public Int64 Size { get; }

        /// <summary>
        /// Returns the type of data.
        /// </summary>
        /// <value>
        /// One of the <see cref="FileStreamType"/> values.
        /// </value>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public FileStreamType StreamType { get; }

        /// <summary>
        /// Returns attributes of the data stream.
        /// </summary>
        /// <value>
        /// A combination of <see cref="FileStreamAttributes"/> values.
        /// </value>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public FileStreamAttributes Attributes { get; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="AlternateDataStreamInfo"/> class.
        /// </summary>
        /// <param name="path">
        /// The full path of the file.
        /// This argument must not be <see langword="null"/>.
        /// </param>
        /// <param name="info">stream info</param>
        internal AlternateDataStreamInfo(String path, Win32StreamInfo info)
        {
            Path = path;
            Name = info.StreamName;
            StreamType = info.StreamType;
            Attributes = info.StreamAttributes;
            Size = info.StreamSize;
            Exists = true;

            FullPath = NTFSAlternateStreamUtils.BuildStreamPath(Path, Name);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlternateDataStreamInfo"/> class.
        /// </summary>
        /// <param name="path">
        /// The full path of the file.
        /// This argument must not be <see langword="null"/>.
        /// </param>
        /// <param name="name">
        /// The name of the stream
        /// This argument must not be <see langword="null"/>.
        /// </param>
        /// <param name="exists">
        /// <see langword="true"/> if the stream exists;
        /// otherwise, <see langword="false"/>.
        /// </param>
        internal AlternateDataStreamInfo(String path, String name, Boolean exists)
            : this(path, name, null, exists)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlternateDataStreamInfo"/> class.
        /// </summary>
        /// <param name="path">
        /// The full path of the file.
        /// This argument must not be <see langword="null"/>.
        /// </param>
        /// <param name="name">
        /// The name of the stream
        /// This argument must not be <see langword="null"/>.
        /// </param>
        /// <param name="stream">
        /// The full path of the stream</param>
        /// <param name="exists">
        /// <see langword="true"/> if the stream exists;
        /// otherwise, <see langword="false"/>.
        /// </param>
        internal AlternateDataStreamInfo(String path, String name, String? stream, Boolean exists)
        {
            StreamType = FileStreamType.AlternateDataStream;

            Path = path;
            Name = name;
            FullPath = stream ?? NTFSAlternateStreamUtils.BuildStreamPath(path, name);
            Exists = exists;

            if (Exists)
            {
                Size = (Int64) WindowsFileUtils.GetFileSize(FullPath);
            }
        }

        /// <summary>
        /// Opens this alternate data stream.
        /// </summary>
        /// <param name="mode">
        /// A <see cref="FileMode"/> value that specifies whether a stream is created if one does not exist, 
        /// and determines whether the contents of existing streams are retained or overwritten.
        /// </param>
        /// <param name="access">
        /// A <see cref="FileAccess"/> value that specifies the operations that can be performed on the stream. 
        /// </param>
        /// <param name="share">
        /// A <see cref="FileShare"/> value specifying the type of access other threads have to the file. 
        /// </param>
        /// <param name="bufferSize">
        /// The size of the buffer to use.
        /// </param>
        /// <param name="overlapped">
        /// <see langword="true"/> to enable async-IO;
        /// otherwise, <see langword="false"/>.
        /// </param>
        /// <returns>
        /// A <see cref="FileStream"/> for this alternate data stream.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="bufferSize"/> is less than or equal to zero.
        /// </exception>
        /// <exception cref="SecurityException">
        /// The caller does not have the required permission. 
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// The caller does not have the required permission, or the file is read-only.
        /// </exception>
        /// <exception cref="IOException">
        /// The specified file is in use. 
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The path of the stream is invalid.
        /// </exception>
        /// <exception cref="Win32Exception">
        /// There was an error opening the stream.
        /// </exception>
        public FileStream? Open(FileMode mode, FileAccess access, FileShare share = FileShare.None, Int32 bufferSize = BufferUtils.DefaultBuffer, Boolean overlapped = false)
        {
            if (bufferSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bufferSize), bufferSize, null);
            }

            SafeFileHandle handle = WindowsPathUtils.Safe.CreateFile(FullPath, access.ToNative(), share, IntPtr.Zero, mode, overlapped ? NativeFileFlags.Overlapped : 0, IntPtr.Zero);
            if (!handle.IsInvalid)
            {
                return new FileStream(handle, access, bufferSize, overlapped);
            }

            Exception? exception = WindowsPathUtils.Safe.GetLastIOException(FullPath);

            if (exception is not null)
            {
                throw exception;
            }

            return null;
        }

        /// <summary>
        /// Opens this alternate data stream.
        /// </summary>
        /// <param name="mode">
        /// A <see cref="FileMode"/> value that specifies whether a stream is created if one does not exist, 
        /// and determines whether the contents of existing streams are retained or overwritten.
        /// </param>
        /// <returns>
        /// A <see cref="FileStream"/> for this alternate data stream.
        /// </returns>
        /// <exception cref="SecurityException">
        /// The caller does not have the required permission. 
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// The caller does not have the required permission, or the file is read-only.
        /// </exception>
        /// <exception cref="IOException">
        /// The specified file is in use. 
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The path of the stream is invalid.
        /// </exception>
        /// <exception cref="Win32Exception">
        /// There was an error opening the stream.
        /// </exception>
        public FileStream? Open(FileMode mode)
        {
            FileAccess access = mode == FileMode.Append ? FileAccess.Write : FileAccess.ReadWrite;
            return Open(mode, access);
        }

        /// <summary>
        /// Opens this stream for reading.
        /// </summary>
        /// <returns>
        /// A read-only <see cref="FileStream"/> for this stream.
        /// </returns>
        /// <exception cref="SecurityException">
        /// The caller does not have the required permission. 
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// The caller does not have the required permission, or the file is read-only.
        /// </exception>
        /// <exception cref="IOException">
        /// The specified file is in use. 
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The path of the stream is invalid.
        /// </exception>
        /// <exception cref="Win32Exception">
        /// There was an error opening the stream.
        /// </exception>
        public FileStream? OpenRead()
        {
            return Open(FileMode.Open, FileAccess.Read, FileShare.Read);
        }

        /// <summary>
        /// Opens this stream for writing.
        /// </summary>
        /// <returns>
        /// A write-only <see cref="FileStream"/> for this stream.
        /// </returns>
        /// <exception cref="SecurityException">
        /// The caller does not have the required permission. 
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// The caller does not have the required permission, or the file is read-only.
        /// </exception>
        /// <exception cref="IOException">
        /// The specified file is in use. 
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The path of the stream is invalid.
        /// </exception>
        /// <exception cref="Win32Exception">
        /// There was an error opening the stream.
        /// </exception>
        public FileStream? OpenWrite()
        {
            return Open(FileMode.OpenOrCreate, FileAccess.Write);
        }

        /// <summary>
        /// Opens this stream as a text file.
        /// </summary>
        /// <returns>
        /// A <see cref="StreamReader"/> which can be used to read the contents of this stream.
        /// </returns>
        /// <exception cref="SecurityException">
        /// The caller does not have the required permission. 
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// The caller does not have the required permission, or the file is read-only.
        /// </exception>
        /// <exception cref="IOException">
        /// The specified file is in use. 
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The path of the stream is invalid.
        /// </exception>
        /// <exception cref="Win32Exception">
        /// There was an error opening the stream.
        /// </exception>
        public StreamReader OpenText()
        {
            Stream? stream = Open(FileMode.Open, FileAccess.Read, FileShare.Read);
            return new StreamReader(stream!);
        }
        
        /// <summary>
        /// Deletes this stream from the parent file.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if the stream was deleted;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="SecurityException">
        /// The caller does not have the required permission. 
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// The caller does not have the required permission, or the file is read-only.
        /// </exception>
        /// <exception cref="IOException">
        /// The specified file is in use. 
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The path of the stream is invalid.
        /// </exception>
        public Boolean Delete()
        {
            return WindowsPathUtils.Safe.DeleteFile(FullPath);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="obj">
        /// An object to compare with this object.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the current object is equal to the <paramref name="obj"/> parameter;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public override Boolean Equals(Object? obj)
        {
            return Equals(obj as AlternateDataStreamInfo);
        }

        /// <summary>
        /// Returns a value indicating whether
        /// this instance is equal to another instance.
        /// </summary>
        /// <param name="other">
        /// The instance to compare to.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the current object is equal to the <paramref name="other"/> parameter;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public Boolean Equals(AlternateDataStreamInfo? other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            return comparer.Equals(Path ?? String.Empty, other.Path ?? String.Empty)
                   && comparer.Equals(Name ?? String.Empty, other.Name ?? String.Empty);
        }
        
        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="Object"/>.
        /// </returns>
        public override Int32 GetHashCode()
        {
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            return comparer.GetHashCode(Path ?? String.Empty) ^ comparer.GetHashCode(Name ?? String.Empty);
        }
        
        /// <summary>
        /// Returns a <see cref="String"/> that represents the current instance.
        /// </summary>
        /// <returns>
        /// A <see cref="String"/> that represents the current instance.
        /// </returns>
        public override String ToString()
        {
            return FullPath;
        }
    }
}