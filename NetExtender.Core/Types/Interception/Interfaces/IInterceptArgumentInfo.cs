using System;
using NetExtender.Types.Monads;

namespace NetExtender.Types.Interception.Interfaces
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