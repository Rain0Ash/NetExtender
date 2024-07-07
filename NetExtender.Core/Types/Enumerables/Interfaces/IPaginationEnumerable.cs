// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;

namespace NetExtender.Types.Enumerables.Interfaces
{
    public interface IPaginationEnumerable<out T, out TCollection> : IPaginationEnumerable<T> where TCollection : class, IEnumerable<T>
    {
        public TCollection Source { get; }
    }
    
    public interface IPaginationEnumerable<out T> : IPaginationEnumerable, IEnumerable<T>
    {
    }

    public interface IPaginationEnumerable : IEnumerable
    {
        /// <summary>
        /// Page index
        /// </summary>
        public Int32 Index { get; }
        
        /// <summary>
        /// Page number
        /// <see cref="Index"/> + 1
        /// </summary>
        public Int32 Page { get; }
        
        /// <summary>
        /// Total pages
        /// <seealso cref="Page"/>
        /// </summary>
        public Int32 Total { get; }
        
        /// <summary>
        /// Count of page items
        /// </summary>
        public Int32 Items { get; }
        
        /// <summary>
        /// Page size
        /// </summary>
        public Int32 Size { get; }
        public Boolean CanResize { get; }

        /// <summary>
        /// Total count of items
        /// </summary>
        public Int32 Count { get; }
        
        public Boolean HasPrevious { get; }
        public Boolean HasNext { get; }
        
        public Boolean Resize(Int32 size);
        public Boolean Resize(Int32 size, Boolean resize);
    }
}