// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Interfaces;

namespace NetExtender.Types.Monads.Interfaces
{
    public interface INullMaybe<T> : INullMaybe, IMonad<T>, IMaybeEquality<T>, IMaybeEquality<IMaybe<T>>, IMaybeEquality<INullMaybe<T>>, ICloneable<INullMaybe<T>>
    {
        public new T Value { get; }

        public new INullMaybe<T> Clone();
    }
    
    public interface INullMaybe : IMonad, ICloneable<INullMaybe>
    {
        public Object? Value { get; }
        
        public new INullMaybe Clone();
    }
}