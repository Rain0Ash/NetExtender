using System;

namespace NetExtender.Network.IPC.IO
{
    public interface IInterprocessMemoryMappedFile
    {
        /// <summary>
        /// Called whenever the file is written to
        /// </summary>
        public event EventHandler? FileUpdated;

        /// <summary>
        /// The maximum amount of data that can be written to the file
        /// </summary>
        public Int64 MaxFileSize { get; }

        /// <summary>
        /// Gets the file size
        /// </summary>
        /// <returns>File size</returns>
        public Int32 GetFileSize();

        /// <summary>
        /// Reads the content of the memory mapped file with a read lock in place.
        /// </summary>
        /// <returns>File content</returns>
        public Byte[] Read();

        /// <summary>
        /// Replaces the content of the memory mapped file with a write lock in place.
        /// </summary>
        public void Write(Byte[] data);

        /// <summary>
        /// Reads and then replaces the content of the memory mapped file with a write lock in place.
        /// </summary>
        public void ReadWrite(Func<Byte[], Byte[]> updateFunc);
    }
}
