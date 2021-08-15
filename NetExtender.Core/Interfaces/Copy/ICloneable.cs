// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Utilities.Types;

namespace NetExtender.Interfaces
{
    public interface ICloneable<out T>
    {
        public T Clone()
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            if (this is ICloneable cloneable)
            {
                return cloneable.Clone<T>();
            }
            
            throw new NotSupportedException($"Base implementation of {nameof(ICloneable<T>)}.{nameof(Clone)} is supported only for {nameof(ICloneable)} interface");
        }
    }
}