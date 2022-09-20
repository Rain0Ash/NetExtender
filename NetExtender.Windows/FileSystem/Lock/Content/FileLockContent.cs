// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;

namespace NetExtender.IO.FileSystem.Lock.Content
{
    /// <summary>
    /// Class which gets serialized into the file lock - responsible for letting conflicting
    /// processes know which process owns this lock, when the lock was acquired, and the process name.
    /// </summary>
    [DataContract]
    public class FileLockContent
    {
        /// <summary>
        /// The process ID
        /// </summary>
        [DataMember]
        public Int64 ProcessId { get; set; }

        /// <summary>
        /// The timestamp (DateTime.Now.Ticks)
        /// </summary>
        [DataMember]
        public Int64 Timestamp { get; set; }

        /// <summary>
        /// The name of the process
        /// </summary>
        [DataMember]
        public String? ProcessName { get; set; }
    }
}