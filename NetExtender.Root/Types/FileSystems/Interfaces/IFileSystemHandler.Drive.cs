// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Text;
using NetExtender.Interfaces.Notify;

namespace NetExtender.FileSystems.Interfaces
{
    public interface IUnsafeDriveHandler : IDriveHandler
    {
        /// <inheritdoc cref="IDriveHandler.GetDrives()"/>
        public new DriveInfo[] GetDrives();
    }

    public interface IDriveHandler : INotifyProperty, IDisposable
    {
        public Guid Id { get; }
        public String? Name { get; }
        public Object SyncRoot { get; }
        public Boolean IsSynchronized { get; }
        public DateTime CreationTime { get; }
        public DateTime CreationTimeUtc { get; }
        public Boolean IsReal { get; }
        public Encoding? Encoding { get; }
        public Encoding ReadEncoding { get; }
        public Encoding WriteEncoding { get; }
        public StringComparer? Comparer { get; }
        public Boolean? IsCaseSensitive { get; }

        /// <inheritdoc cref="System.IO.DriveInfo.GetDrives()"/>
        public IDriveInfo[] GetDrives();
    }
}