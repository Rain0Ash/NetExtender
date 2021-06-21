// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Interfaces
{
    public interface IReadOnlyValidable<in T> : IBaseValidable
    {
        public Func<T?, Boolean> Validate { get; }
        
        public new Boolean IsValid
        {
            get
            {
                return Validate?.Invoke(default) != false;
            }
        }
    }
}