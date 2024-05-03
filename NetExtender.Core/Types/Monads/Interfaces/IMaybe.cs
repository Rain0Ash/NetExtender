// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Monads.Interfaces
{
    public interface IMaybe<T> : IMaybe, IEquatable<T>, IEquatable<IMaybe<T>>, IEquatable<INullMaybe<T>>
    {
        public new T Value { get; }
    }
    
    public interface IMaybe
    {
        public Boolean HasValue { get; }
        public Object? Value { get; }
    }
}