// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace NetExtender.Types.Counters.Interfaces
{
    public interface IReadOnlyCounter<T> : IReadOnlyDictionary<T, Int32> where T : notnull
    {
    }
}