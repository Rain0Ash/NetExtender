// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Interfaces
{
    public interface IValidable<T> : IReadOnlyValidable<T?>
    {
        public new Func<T?, Boolean> Validate { get; set; }
    }
}