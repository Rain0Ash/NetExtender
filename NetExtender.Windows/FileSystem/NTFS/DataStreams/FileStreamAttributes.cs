// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.IO.FileSystem.NTFS.DataStreams
{
    /// <summary>
    /// Represents the attributes of a file stream.
    /// </summary>
    [Flags]
    public enum FileStreamAttributes
    {
        /// <summary>
        /// No attributes.
        /// </summary>
        None = 0,
        /// <summary>
        /// Set if the stream contains data that is modified when read.
        /// </summary>
        ModifiedWhenRead = 1,
        /// <summary>
        /// Set if the stream contains security data.
        /// </summary>
        ContainsSecurity = 2,
        /// <summary>
        /// Set if the stream contains properties.
        /// </summary>
        ContainsProperties = 4,
        /// <summary>
        /// Set if the stream is sparse.
        /// </summary>
        Sparse = 8,
    }
}
