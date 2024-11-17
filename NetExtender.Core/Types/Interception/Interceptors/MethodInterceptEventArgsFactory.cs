using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using NetExtender.Types.Interception.Interfaces;

namespace NetExtender.Types.Interception
{
    public abstract class MethodInterceptEventArgsFactory<TArgument, TInfo> : IMethodInterceptEventArgsFactory<TArgument, TInfo>
    {
        public virtual TArgument Create(MethodInfo method, IEnumerable<Object?>? parameters, TInfo? info)
        {
            return Create(method, parameters?.ToImmutableArray() ?? ImmutableArray<Object?>.Empty, info);
        }

        public abstract TArgument Create(MethodInfo method, ImmutableArray<Object?> parameters, TInfo? info);

        public virtual TArgument Create(MethodInfo method, IEnumerable<Object?>? parameters, Exception exception, TInfo? info)
        {
            return Create(method, parameters?.ToImmutableArray() ?? ImmutableArray<Object?>.Empty, exception, info);
        }

        public abstract TArgument Create(MethodInfo method, ImmutableArray<Object?> parameters, Exception exception, TInfo? info);

        public virtual TArgument Create<T>(MethodInfo method, IEnumerable<Object?>? parameters, TInfo? info)
        {
            return Create<T>(method, parameters?.ToImmutableArray() ?? ImmutableArray<Object?>.Empty, info);
        }

        public abstract TArgument Create<T>(MethodInfo method, ImmutableArray<Object?> parameters, TInfo? info);

        public virtual TArgument Create<T>(MethodInfo method, T value, IEnumerable<Object?>? parameters, TInfo? info)
        {
            return Create(method, value, parameters?.ToImmutableArray() ?? ImmutableArray<Object?>.Empty, info);
        }

        public abstract TArgument Create<T>(MethodInfo method, T value, ImmutableArray<Object?> parameters, TInfo? info);

        public virtual TArgument Create<T>(MethodInfo method, IEnumerable<Object?>? parameters, Exception exception, TInfo? info)
        {
            return Create<T>(method, parameters?.ToImmutableArray() ?? ImmutableArray<Object?>.Empty, exception, info);
        }

        public abstract TArgument Create<T>(MethodInfo method, ImmutableArray<Object?> parameters, Exception exception, TInfo? info);
    }
}