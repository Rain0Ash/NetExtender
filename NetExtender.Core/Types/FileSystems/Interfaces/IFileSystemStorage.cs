using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.Serialization;

namespace NetExtender.FileSystems.Interfaces
{
    internal interface IFileSystemStorage : IUnsafeFileSystemHandler
    {
#region Storage
        public new String? Name();
        public new DateTime CreationTime();
        public new DateTime CreationTimeUtc();
        public new Boolean IsReal();
        public Boolean IsReal(IFileSystemStorageEntry entry);
#endregion

#region FileSystem
        public FileSystemInfo? Info(IFileSystemEntry entry);
        public void GetObjectData(IFileSystemEntry entry, SerializationInfo info, StreamingContext context);
        public String Name(IFileSystemEntry entry);
        public String FullName(IFileSystemEntry entry);
        public String Extension(IFileSystemEntry entry);
        public Boolean IsReal(IFileSystemEntry entry);
        public Boolean Exists(IFileSystemEntry entry);
        public FileAttributes Attributes(IFileSystemEntry entry);
        public void Attributes(IFileSystemEntry entry, FileAttributes value);
        public String? LinkTarget(IFileSystemEntry entry);
        public DateTime CreationTime(IFileSystemEntry entry);
        public void CreationTime(IFileSystemEntry entry, DateTime value);
        public DateTime CreationTimeUtc(IFileSystemEntry entry);
        public void CreationTimeUtc(IFileSystemEntry entry, DateTime value);
        public DateTime LastAccessTime(IFileSystemEntry entry);
        public void LastAccessTime(IFileSystemEntry entry, DateTime value);
        public DateTime LastAccessTimeUtc(IFileSystemEntry entry);
        public void LastAccessTimeUtc(IFileSystemEntry entry, DateTime value);
        public DateTime LastWriteTime(IFileSystemEntry entry);
        public void LastWriteTime(IFileSystemEntry entry, DateTime value);
        public DateTime LastWriteTimeUtc(IFileSystemEntry entry);
        public void LastWriteTimeUtc(IFileSystemEntry entry, DateTime value);
        public new IDriveEntry? Drive(IFileSystemEntry entry);
        public Boolean CreateAsSymbolicLink(IFileSystemEntry entry, String target);
        public IFileSystemEntry? ResolveLinkTarget(IFileSystemEntry entry);
        public IFileSystemEntry? ResolveLinkTarget(IFileSystemEntry entry, Boolean final);
        public void Delete(IFileSystemEntry entry);
        public void Refresh(IFileSystemEntry entry);
        public Int32 GetHashCode(IFileSystemEntry? entry);
        public Boolean Equals(IFileSystemEntry? entry, FileSystemInfo? other);
        public Boolean Equals(IFileSystemEntry? entry, IFileSystemInfo? other);
        
        [return: NotNullIfNotNull("entry")]
        public String? ToString(IFileSystemEntry? entry);
#endregion

#region Link
        public FileSystemInfo? Info(ILinkEntry entry);
        public void GetObjectData(ILinkEntry entry, SerializationInfo info, StreamingContext context);
        public String Name(ILinkEntry entry);
        public String FullName(ILinkEntry entry);
        public String Extension(ILinkEntry entry);
        public Boolean IsReal(ILinkEntry entry);
        public Boolean Exists(ILinkEntry entry);
        public FileAttributes Attributes(ILinkEntry entry);
        public void Attributes(ILinkEntry entry, FileAttributes value);
        public String? LinkTarget(ILinkEntry entry);
        public DateTime CreationTime(ILinkEntry entry);
        public void CreationTime(ILinkEntry entry, DateTime value);
        public DateTime CreationTimeUtc(ILinkEntry entry);
        public void CreationTimeUtc(ILinkEntry entry, DateTime value);
        public DateTime LastAccessTime(ILinkEntry entry);
        public void LastAccessTime(ILinkEntry entry, DateTime value);
        public DateTime LastAccessTimeUtc(ILinkEntry entry);
        public void LastAccessTimeUtc(ILinkEntry entry, DateTime value);
        public DateTime LastWriteTime(ILinkEntry entry);
        public void LastWriteTime(ILinkEntry entry, DateTime value);
        public DateTime LastWriteTimeUtc(ILinkEntry entry);
        public void LastWriteTimeUtc(ILinkEntry entry, DateTime value);
        public String? DirectoryName(ILinkEntry entry);
        public IDirectoryEntry? GetDirectory(ILinkEntry entry);
        public IDriveEntry? Drive(ILinkEntry entry);
        public Boolean CreateAsSymbolicLink(ILinkEntry entry, String target);
        public IFileSystemEntry? ResolveLinkTarget(ILinkEntry entry);
        public IFileSystemEntry? ResolveLinkTarget(ILinkEntry entry, Boolean final);
        public void Create(ILinkEntry entry);
        public void Delete(ILinkEntry entry);
        public void Refresh(ILinkEntry entry);
        public Int32 GetHashCode(ILinkEntry? entry);
        public Boolean Equals(ILinkEntry? entry, FileSystemInfo? other);
        public Boolean Equals(ILinkEntry? entry, IFileSystemInfo? other);
        
        [return: NotNullIfNotNull("entry")]
        public String? ToString(ILinkEntry? entry);
#endregion

#region File
        public FileInfo? Info(IFileEntry entry);
        public void GetObjectData(IFileEntry entry, SerializationInfo info, StreamingContext context);
        public String Name(IFileEntry entry);
        public String FullName(IFileEntry entry);
        public String Extension(IFileEntry entry);
        public Boolean IsReal(IFileEntry entry);
        public Boolean Exists(IFileEntry entry);
        public Int64 Length(IFileEntry entry);
        public Boolean IsReadOnly(IFileEntry entry);
        public void IsReadOnly(IFileEntry entry, Boolean value);
        public FileAttributes Attributes(IFileEntry entry);
        public void Attributes(IFileEntry entry, FileAttributes value);
        public String? LinkTarget(IFileEntry entry);
        public DateTime CreationTime(IFileEntry entry);
        public void CreationTime(IFileEntry entry, DateTime value);
        public DateTime CreationTimeUtc(IFileEntry entry);
        public void CreationTimeUtc(IFileEntry entry, DateTime value);
        public DateTime LastAccessTime(IFileEntry entry);
        public void LastAccessTime(IFileEntry entry, DateTime value);
        public DateTime LastAccessTimeUtc(IFileEntry entry);
        public void LastAccessTimeUtc(IFileEntry entry, DateTime value);
        public DateTime LastWriteTime(IFileEntry entry);
        public void LastWriteTime(IFileEntry entry, DateTime value);
        public DateTime LastWriteTimeUtc(IFileEntry entry);
        public void LastWriteTimeUtc(IFileEntry entry, DateTime value);
        public IDriveEntry? Drive(IFileEntry entry);
        public String? DirectoryName(IFileEntry entry);
        public IDirectoryEntry? GetDirectory(IFileEntry entry);
        public Boolean CreateAsSymbolicLink(IFileEntry entry, String target);
        public IFileSystemEntry? ResolveLinkTarget(IFileEntry entry);
        public IFileSystemEntry? ResolveLinkTarget(IFileEntry entry, Boolean final);
        public FileStream Open(IFileEntry entry, FileMode mode);
        public FileStream Open(IFileEntry entry, FileMode mode, FileAccess access);
        public FileStream Open(IFileEntry entry, FileMode mode, FileAccess access, FileShare share);
        public FileStream Open(IFileEntry entry, FileStreamOptions options);
        public FileStream OpenRead(IFileEntry entry);
        public FileStream OpenWrite(IFileEntry entry);
        public StreamReader OpenText(IFileEntry entry);
        public FileStream Create(IFileEntry entry);
        public StreamWriter AppendText(IFileEntry entry);
        public Boolean Encrypt(IFileEntry entry);
        public Boolean Decrypt(IFileEntry entry);
        public void MoveTo(IFileEntry entry, String destination);
        public void MoveTo(IFileEntry entry, String destination, Boolean overwrite);
        public IFileEntry CopyTo(IFileEntry entry, String destination);
        public IFileEntry CopyTo(IFileEntry entry, String destination, Boolean overwrite);
        public IFileEntry Replace(IFileEntry entry, String destination, String? backup);
        public IFileEntry Replace(IFileEntry entry, String destination, String? backup, Boolean suppress);
        public void Delete(IFileEntry entry);
        public void Refresh(IFileEntry entry);
        public Int32 GetHashCode(IFileEntry? entry);
        public Boolean Equals(IFileEntry? entry, FileInfo? other);
        public Boolean Equals(IFileEntry? entry, IFileInfo? other);
        
        [return: NotNullIfNotNull("entry")]
        public String? ToString(IFileEntry? entry);
#endregion

#region Directory
        public DirectoryInfo? Info(IDirectoryEntry entry);
        public void GetObjectData(IDirectoryEntry entry, SerializationInfo info, StreamingContext context);
        public String Name(IDirectoryEntry entry);
        public String FullName(IDirectoryEntry entry);
        public String Extension(IDirectoryEntry entry);
        public Boolean IsReal(IDirectoryEntry entry);
        public Boolean Exists(IDirectoryEntry entry);
        public FileAttributes Attributes(IDirectoryEntry entry);
        public void Attributes(IDirectoryEntry entry, FileAttributes value);
        public String? LinkTarget(IDirectoryEntry entry);
        public DateTime CreationTime(IDirectoryEntry entry);
        public void CreationTime(IDirectoryEntry entry, DateTime value);
        public DateTime CreationTimeUtc(IDirectoryEntry entry);
        public void CreationTimeUtc(IDirectoryEntry entry, DateTime value);
        public DateTime LastAccessTime(IDirectoryEntry entry);
        public void LastAccessTime(IDirectoryEntry entry, DateTime value);
        public DateTime LastAccessTimeUtc(IDirectoryEntry entry);
        public void LastAccessTimeUtc(IDirectoryEntry entry, DateTime value);
        public DateTime LastWriteTime(IDirectoryEntry entry);
        public void LastWriteTime(IDirectoryEntry entry, DateTime value);
        public DateTime LastWriteTimeUtc(IDirectoryEntry entry);
        public void LastWriteTimeUtc(IDirectoryEntry entry, DateTime value);
        public IDirectoryEntry Root(IDirectoryEntry entry);
        public IDirectoryEntry? Parent(IDirectoryEntry entry);
        public IDriveEntry? Drive(IDirectoryEntry entry);
        public Boolean CreateAsSymbolicLink(IDirectoryEntry entry, String target);
        public IFileSystemEntry? ResolveLinkTarget(IDirectoryEntry entry);
        public IFileSystemEntry? ResolveLinkTarget(IDirectoryEntry entry, Boolean final);
        public IFileSystemEntry[] GetFileSystemInfos(IDirectoryEntry entry);
        public IFileSystemEntry[] GetFileSystemInfos(IDirectoryEntry entry, String? pattern);
        public IFileSystemEntry[] GetFileSystemInfos(IDirectoryEntry entry, String? pattern, SearchOption option);
        public IFileSystemEntry[] GetFileSystemInfos(IDirectoryEntry entry, String? pattern, EnumerationOptions options);
        public IFileEntry[] GetFiles(IDirectoryEntry entry);
        public IFileEntry[] GetFiles(IDirectoryEntry entry, String? pattern);
        public IFileEntry[] GetFiles(IDirectoryEntry entry, String? pattern, SearchOption option);
        public IFileEntry[] GetFiles(IDirectoryEntry entry, String? pattern, EnumerationOptions options);
        public IDirectoryEntry[] GetDirectories(IDirectoryEntry entry);
        public IDirectoryEntry[] GetDirectories(IDirectoryEntry entry, String? pattern);
        public IDirectoryEntry[] GetDirectories(IDirectoryEntry entry, String? pattern, SearchOption option);
        public IDirectoryEntry[] GetDirectories(IDirectoryEntry entry, String? pattern, EnumerationOptions options);
        public IEnumerable<IFileSystemEntry> EnumerateFileSystemInfos(IDirectoryEntry entry);
        public IEnumerable<IFileSystemEntry> EnumerateFileSystemInfos(IDirectoryEntry entry, String? pattern);
        public IEnumerable<IFileSystemEntry> EnumerateFileSystemInfos(IDirectoryEntry entry, String? pattern, SearchOption option);
        public IEnumerable<IFileSystemEntry> EnumerateFileSystemInfos(IDirectoryEntry entry, String? pattern, EnumerationOptions options);
        public IEnumerable<IFileEntry> EnumerateFiles(IDirectoryEntry entry);
        public IEnumerable<IFileEntry> EnumerateFiles(IDirectoryEntry entry, String? pattern);
        public IEnumerable<IFileEntry> EnumerateFiles(IDirectoryEntry entry, String? pattern, SearchOption option);
        public IEnumerable<IFileEntry> EnumerateFiles(IDirectoryEntry entry, String? pattern, EnumerationOptions options);
        public IEnumerable<IDirectoryEntry> EnumerateDirectories(IDirectoryEntry entry);
        public IEnumerable<IDirectoryEntry> EnumerateDirectories(IDirectoryEntry entry, String? pattern);
        public IEnumerable<IDirectoryEntry> EnumerateDirectories(IDirectoryEntry entry, String? pattern, SearchOption option);
        public IEnumerable<IDirectoryEntry> EnumerateDirectories(IDirectoryEntry entry, String? pattern, EnumerationOptions options);
        public void MoveTo(IDirectoryEntry entry, String destination);
        public void Create(IDirectoryEntry entry);
        public IDirectoryEntry CreateSubdirectory(IDirectoryEntry entry, String path);
        public void Delete(IDirectoryEntry entry);
        public void Delete(IDirectoryEntry entry, Boolean recursive);
        public void Refresh(IDirectoryEntry entry);
        public Int32 GetHashCode(IDirectoryEntry? entry);
        public Boolean Equals(IDirectoryEntry? entry, DirectoryInfo? other);
        public Boolean Equals(IDirectoryEntry? entry, IDirectoryInfo? other);
        
        [return: NotNullIfNotNull("entry")]
        public String? ToString(IDirectoryEntry? entry);
#endregion

#region Drive
        public DriveInfo? Info(IDriveEntry entry);
        public void GetObjectData(IDriveEntry entry, SerializationInfo info, StreamingContext context);
        public String Name(IDriveEntry entry);
        public String FullName(IDriveEntry entry);
        public Boolean IsReal(IDriveEntry entry);
        public Boolean IsReady(IDriveEntry entry);
        public DriveType DriveType(IDriveEntry entry);
        public String DriveFormat(IDriveEntry entry);
        public String? VolumeLabel(IDriveEntry entry);
        public void VolumeLabel(IDriveEntry entry, String? value);
        public Int64 AvailableFreeSpace(IDriveEntry entry);
        public Int64 TotalFreeSpace(IDriveEntry entry);
        public Int64 TotalSize(IDriveEntry entry);
        public IDirectoryEntry RootDirectory(IDriveEntry entry);
        public Int32 GetHashCode(IDriveEntry? entry);
        public Boolean Equals(IDriveEntry? entry, DriveInfo? other);
        public Boolean Equals(IDriveEntry? entry, IDriveInfo? other);
        
        [return: NotNullIfNotNull("entry")]
        public String? ToString(IDriveEntry? entry);
#endregion
    }
}