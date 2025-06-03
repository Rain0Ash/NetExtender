// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.StrongId
{
    /// <summary>
    /// Interfaces that implement the strongly-typed ID
    /// </summary>
    [Flags]
    public enum StrongIdInterfaceType : Byte
    {
        /// <summary>
        /// Don't implement any additional members for the strongly-typed ID
        /// </summary>
        None = 0,
        
        // ReSharper disable once InvalidXmlDocComment
        /// <summary>
        /// Implement the <see cref="IParseable{T}"/> interface
        /// </summary>
        Parseable = 1,

        /// <summary>
        /// Implement the <see cref="IEquatable{T}"/> interface
        /// </summary>
        Equatable = 2,

        /// <summary>
        /// Implement the <see cref="IComparable{T}"/> interface
        /// </summary>
        Comparable = 4,
        
        /// <summary>
        /// Implement the <see cref="IFormattable"/> interface
        /// </summary>
        Formattable = 8,
        
        /// <summary>
        /// Implement the <see cref="System.Runtime.Serialization.ISerializable"/> interface
        /// </summary>
        Serializable = 16,
        
        All = Parseable | Equatable | Comparable | Formattable | Serializable
    }
}