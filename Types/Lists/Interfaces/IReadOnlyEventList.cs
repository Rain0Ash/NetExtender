// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using NetExtender.Types.Interfaces;

namespace NetExtender.Types.Lists.Interfaces
{
    public interface IReadOnlyEventList<T> : IReadOnlyList<T>, IEventIndexCollection<T>
    {
    }
}