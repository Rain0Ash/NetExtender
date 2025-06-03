// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Monads;

namespace NetExtender.Types.Intercept.Interfaces
{
    public interface IMemberInterceptArgumentInfo<out TMember, T> : IMemberInterceptArgumentInfo<TMember>
    {
        public Maybe<T> Value { get; }
    }

    public interface IMemberInterceptArgumentInfo<out TMember> : IMemberInterceptArgumentInfo
    {
        public TMember Member { get; }
    }

    public interface IMemberInterceptArgumentInfo : IInterceptArgumentInfo
    {
        public Exception? Exception { get; }
    }

    public interface IInterceptArgumentInfo
    {
    }
}