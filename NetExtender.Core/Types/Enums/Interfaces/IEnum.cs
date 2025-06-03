// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Culture;
using NetExtender.Types.Lists.Interfaces;

namespace NetExtender.Types.Enums.Interfaces
{
    public interface IEnum<T, TEnum> : IEnum<T>, IEquality<IEnum<T, TEnum>> where T : unmanaged, Enum where TEnum : Enum<T, TEnum>, new()
    {
        public new IReadOnlySortedList<TEnum> Flags { get; }
    }

    public interface IEnum<T> : IEnum, IEquality<T>, IEquality<IEnum<T>> where T : unmanaged, Enum
    {
        public new T Id { get; }
        public new IReadOnlySortedList<IEnum<T>> Flags { get; }
    }

    public interface IEnum : IComparable, IFormattable
    {
        public Type Underlying { get; }
        public Enum Id { get; }
        public String Title { get; }
        public Boolean HasIdentifier { get; }
        public LocalizationIdentifier? Identifier { get; }
        public Boolean IsFlags { get; }
        public IReadOnlySortedList<IEnum>? Flags { get; }
        public Boolean IsIntern { get; }
    }
}