// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Culture;

namespace NetExtender.Types.Enums.Interfaces
{
    public interface IEnum<T, TEnum> : IEnum<T>, IEquatable<IEnum<T, TEnum>>, IComparable<IEnum<T, TEnum>> where T : unmanaged, Enum where TEnum : Enum<T, TEnum>, new()
    {
    }
    
    public interface IEnum<T> : IEnum, IEquatable<T>, IEquatable<IEnum<T>>, IComparable<T>, IComparable<IEnum<T>> where T : unmanaged, Enum
    {
        public T Id { get; }
    }
    
    public interface IEnum : IComparable, IFormattable
    {
        public Type Underlying { get; }
        public String Title { get; }
        public LocalizationIdentifier? Identifier { get; }
        public Boolean HasIdentifier { get; }
        public Boolean IsIntern { get; }
    }
}