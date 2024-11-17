using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;

namespace NetExtender.Types.Interception.Interfaces
{
    public interface IMethodInterceptEventArgsFactory<out TArgument, in TInfo>
    {
        public TArgument Create(MethodInfo method, IEnumerable<Object?>? parameters, TInfo? info);
        public TArgument Create(MethodInfo method, ImmutableArray<Object?> parameters, TInfo? info);
        public TArgument Create(MethodInfo method, IEnumerable<Object?>? parameters, Exception exception, TInfo? info);
        public TArgument Create(MethodInfo method, ImmutableArray<Object?> parameters, Exception exception, TInfo? info);
        public TArgument Create<T>(MethodInfo method, IEnumerable<Object?>? parameters, TInfo? info);
        public TArgument Create<T>(MethodInfo method, ImmutableArray<Object?> parameters, TInfo? info);
        public TArgument Create<T>(MethodInfo method, T value, IEnumerable<Object?>? parameters, TInfo? info);
        public TArgument Create<T>(MethodInfo method, T value, ImmutableArray<Object?> parameters, TInfo? info);
        public TArgument Create<T>(MethodInfo method, IEnumerable<Object?>? parameters, Exception exception, TInfo? info);
        public TArgument Create<T>(MethodInfo method, ImmutableArray<Object?> parameters, Exception exception, TInfo? info);
    }
}