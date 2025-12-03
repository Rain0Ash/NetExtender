// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Interfaces;

namespace NetExtender.Types.Monads.Interfaces
{
    public interface IWeakMaybe<T> : IWeakMaybe, IMonad<T>, IMaybeEquality<T, T>, IMaybeEquality<T, IMaybe<T>>, IMaybeEquality<T, INullMaybe<T>>, IMaybeEquality<T, IWeakMaybe<T>>, ICloneable<IWeakMaybe<T>>
    {
        public new T Value { get; }
        public new Maybe<T> Maybe { get; }

        public new IWeakMaybe<T> Clone();
    }

    public interface IWeakMaybe : IMonad, ICloneable<IWeakMaybe>
    {
        public Boolean HasValue { get; }
        public Object? Value { get; }
        public Maybe<Object?> Maybe { get; }
        public Boolean IsNull { get; }
        public Boolean IsAlive { get; }

        public new IWeakMaybe Clone();
    }
}