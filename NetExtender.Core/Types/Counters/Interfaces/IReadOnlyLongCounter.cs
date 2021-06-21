// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace NetExtender.Types.Counters.Interfaces
{
    public interface IReadOnlyLongCounter<T> : IReadOnlyDictionary<T, Int64> where T : notnull
    {
    }
}