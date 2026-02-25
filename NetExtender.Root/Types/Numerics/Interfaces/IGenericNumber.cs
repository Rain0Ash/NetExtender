// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Numerics.Interfaces
{
    public interface IGenericNumber : IUnderlyingType, IEquality<ValueType>, IEquality<GenericNumber>, IAnyEquality, ISpanFormattable, IConvertible
    {
    }

    public interface IGenericNumber<T> : IUnderlyingType<T>, IEquality<T>, IAnyEquality, ISpanFormattable, IConvertible
    {
    }
}