// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using NetExtender.Types.Collections.Interfaces;

namespace NetExtender.Types.Lists.Interfaces
{
    public interface IPaginationList<T, out TCollection> : IPaginationList<T>, IPaginationCollection<T, TCollection> where TCollection : class, IList<T>
    {
    }
    
    public interface IPaginationList<T> : IPaginationCollection<T>, IList<T>
    {
    }
}