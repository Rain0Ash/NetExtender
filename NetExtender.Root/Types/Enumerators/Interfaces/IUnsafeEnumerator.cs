// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;

namespace NetExtender.Types.Enumerators.Interfaces
{
    public interface IUnsafeEnumerator<out T> : IEnumerable<T>, IEnumerator<T>, IUnsafeSize where T : struct
    {
    }
}