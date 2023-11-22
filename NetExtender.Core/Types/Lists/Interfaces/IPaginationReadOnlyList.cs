// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using NetExtender.Types.Collections.Interfaces;

namespace NetExtender.Types.Lists.Interfaces
{
    public interface IPaginationReadOnlyList<out T, out TCollection> : IPaginationReadOnlyList<T>, IPaginationReadOnlyCollection<T, TCollection> where TCollection : class, IReadOnlyList<T>
    {
    }
    
    public interface IPaginationReadOnlyList<out T> : IPaginationReadOnlyCollection<T>, IReadOnlyList<T>
    {
    }
}