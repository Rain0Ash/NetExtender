// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.Enums.Interfaces;

namespace NetExtender.Types.Enums
{
    internal abstract class UnderlyingEnumOperation<T, TUnderlying> : IUnderlyingEnumOperation<T, TUnderlying> where T : unmanaged, Enum where TUnderlying : unmanaged
    {
        public abstract Boolean IsContinuous { get; }
        public abstract Boolean IsDefined(T value);
        public abstract Boolean IsDefined(TUnderlying value);
        public abstract EnumMember<T> GetMember(T value);
        public abstract Boolean TryGetMember(T value, [MaybeNullWhen(false)] out EnumMember<T> result);
        public abstract Boolean TryParse(String value, out T result);
    }

    internal abstract class ContinuousUnderlyingEnumOperation<T, TUnderlying> : UnderlyingEnumOperation<T, TUnderlying> where T : unmanaged, Enum where TUnderlying : unmanaged
    {
        protected TUnderlying Min { get; }
        protected TUnderlying Max { get; }
        protected ImmutableArray<EnumMember<T>> Members { get; }

        public override Boolean IsContinuous
        {
            get
            {
                return true;
            }
        }

        protected ContinuousUnderlyingEnumOperation(TUnderlying min, TUnderlying max, ImmutableArray<EnumMember<T>> members)
        {
            Min = min;
            Max = max;
            Members = members;
        }
    }

    internal abstract class DiscontinuousUnderlyingEnumOperation<T, TUnderlying> : UnderlyingEnumOperation<T, TUnderlying> where T : unmanaged, Enum where TUnderlying : unmanaged
    {
        protected ImmutableDictionary<TUnderlying, EnumMember<T>> Value { get; }

        public override Boolean IsContinuous
        {
            get
            {
                return false;
            }
        }

        protected DiscontinuousUnderlyingEnumOperation(ImmutableDictionary<TUnderlying, EnumMember<T>> value)
        {
            Value = value;
        }
    }
}