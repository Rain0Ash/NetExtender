// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using NetExtender.Types.Enums.Interfaces;

namespace NetExtender.Types.Enums
{
    internal abstract class UnderlyingEnumOperation<T, TUnderlying> : IUnderlyingEnumOperation<T, TUnderlying> where T : unmanaged, Enum where TUnderlying : unmanaged
    {
        public abstract Boolean IsContinuous { get; }
        public abstract Boolean IsDefined(ref T value);
        public abstract Boolean IsDefined(ref TUnderlying value);
        public abstract EnumMember<T> GetMember(ref T value);
        public abstract Boolean TryParse(String text, out T result);
    }

    internal abstract class ContinuousUnderlyingEnumOperation<T, TUnderlying> : UnderlyingEnumOperation<T, TUnderlying> where T : unmanaged, Enum where TUnderlying : unmanaged
    {
        protected TUnderlying Min { get; }
        protected TUnderlying Max { get; }
        protected EnumMember<T>[] Members { get; }

        public override Boolean IsContinuous
        {
            get
            {
                return true;
            }
        }

        protected ContinuousUnderlyingEnumOperation(TUnderlying min, TUnderlying max, EnumMember<T>[] members)
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