// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Numerics.Interfaces
{
    public interface IUnderlyingType
    {
        public Type UnderlyingType { get; }
        public Int32 Size { get; }
    }

    public interface IUnderlyingType<out T> : IUnderlyingType
    {
        public T Value { get; }
    }
}