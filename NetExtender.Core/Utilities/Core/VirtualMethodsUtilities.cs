using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace NetExtender.Utilities.Core
{
    public static class VirtualMethodsUtilities
    {
        [SuppressMessage("ReSharper", "CoVariantArrayConversion")]
        private static class Storage
        {
            public static MethodInfo Action<TAction>(TAction @delegate, out ParameterExpression instance, out ParameterExpression[] parameters, out MethodCallExpression call) where TAction : Delegate
            {
                if (@delegate is null)
                {
                    throw new ArgumentNullException(nameof(@delegate));
                }
                
                if (@delegate.Method is not { } method)
                {
                    throw new ArgumentException("Action must contains internal method.");
                }
                
                if (typeof(Action).GetMethod(nameof(System.Action.Invoke)) is null)
                {
                    throw new ArgumentException($"Action '{@delegate.GetType()}' must contains method '{nameof(System.Action.Invoke)}'.");
                }
                
                if (method.DeclaringType is null)
                {
                    throw new ArgumentException($"Action '{@delegate.GetType()}' must contains declaring type.");
                }
                
                if (method is { IsAbstract: false, IsVirtual: false })
                {
                    throw new ArgumentException($"Action '{@delegate.GetType()}' must be virtual or abstract.");
                }
                
                if (!method.ReturnType.IsVoid())
                {
                    throw new ArgumentException($"Action '{@delegate.GetType()}' must return '{typeof(void)}'.");
                }
                
                instance = Expression.Parameter(typeof(TAction), nameof(instance));
                parameters = method.GetParameters().Select(static parameter => Expression.Parameter(parameter.ParameterType, parameter.Name)).ToArray();
                call = Expression.Call(instance, method, parameters);
                return method;
            }
            
            public static MethodInfo Function<TFunction>(TFunction @delegate, out ParameterExpression instance, out ParameterExpression[] parameters, out MethodCallExpression call) where TFunction : Delegate
            {
                if (@delegate is null)
                {
                    throw new ArgumentNullException(nameof(@delegate));
                }
                
                if (@delegate.Method is not { } method)
                {
                    throw new ArgumentException("Function must contains internal method.");
                }
                
                if (typeof(Action).GetMethod(nameof(System.Action.Invoke)) is null)
                {
                    throw new ArgumentException($"Function '{@delegate.GetType()}' must contains method '{nameof(System.Action.Invoke)}'.");
                }
                
                if (method.DeclaringType is null)
                {
                    throw new ArgumentException($"Function '{@delegate.GetType()}' must contains declaring type.");
                }
                
                if (method is { IsAbstract: false, IsVirtual: false })
                {
                    throw new ArgumentException($"Function '{@delegate.GetType()}' must be virtual or abstract.");
                }
                
                if (method.ReturnType.IsVoid())
                {
                    throw new ArgumentException($"Function '{@delegate.GetType()}' must return value.");
                }
                
                instance = Expression.Parameter(typeof(TFunction), nameof(instance));
                parameters = method.GetParameters().Select(static parameter => Expression.Parameter(parameter.ParameterType, parameter.Name)).ToArray();
                call = Expression.Call(instance, method, parameters);
                return method;
            }
        }
        
        private static class Storage<TDelegate> where TDelegate : Delegate
        {
            private static TDelegate? @delegate;
            public static TDelegate? Delegate
            {
                get
                {
                    return @delegate;
                }
                set
                {
                    @delegate = value ?? throw new ArgumentNullException(nameof(value));
                }
            }
        }
        
        private static Action<TSource> Create<TSource>(Action action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            
            MethodInfo method = Storage.Action(action, out ParameterExpression? instance, out ParameterExpression[]? parameters, out MethodCallExpression? call);
            return Expression.Lambda<Action<TSource>>(call, method.Name, parameters.Prepend(instance)).Compile();
        }
        
        private static Action<TSource, T> Create<TSource, T>(Action<T> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            
            MethodInfo method = Storage.Action(action, out ParameterExpression? instance, out ParameterExpression[]? parameters, out MethodCallExpression? call);
            return Expression.Lambda<Action<TSource, T>>(call, method.Name, parameters.Prepend(instance)).Compile();
        }
        
        private static Action<TSource, T1, T2> Create<TSource, T1, T2>(Action<T1, T2> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            
            MethodInfo method = Storage.Action(action, out ParameterExpression? instance, out ParameterExpression[]? parameters, out MethodCallExpression? call);
            return Expression.Lambda<Action<TSource, T1, T2>>(call, method.Name, parameters.Prepend(instance)).Compile();
        }

        private static Action<TSource, T1, T2, T3> Create<TSource, T1, T2, T3>(Action<T1, T2, T3> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            
            MethodInfo method = Storage.Action(action, out ParameterExpression? instance, out ParameterExpression[]? parameters, out MethodCallExpression? call);
            return Expression.Lambda<Action<TSource, T1, T2, T3>>(call, method.Name, parameters.Prepend(instance)).Compile();
        }

        private static Action<TSource, T1, T2, T3, T4> Create<TSource, T1, T2, T3, T4>(Action<T1, T2, T3, T4> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            
            MethodInfo method = Storage.Action(action, out ParameterExpression? instance, out ParameterExpression[]? parameters, out MethodCallExpression? call);
            return Expression.Lambda<Action<TSource, T1, T2, T3, T4>>(call, method.Name, parameters.Prepend(instance)).Compile();
        }

        private static Action<TSource, T1, T2, T3, T4, T5> Create<TSource, T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            
            MethodInfo method = Storage.Action(action, out ParameterExpression? instance, out ParameterExpression[]? parameters, out MethodCallExpression? call);
            return Expression.Lambda<Action<TSource, T1, T2, T3, T4, T5>>(call, method.Name, parameters.Prepend(instance)).Compile();
        }

        private static Action<TSource, T1, T2, T3, T4, T5, T6> Create<TSource, T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            
            MethodInfo method = Storage.Action(action, out ParameterExpression? instance, out ParameterExpression[]? parameters, out MethodCallExpression? call);
            return Expression.Lambda<Action<TSource, T1, T2, T3, T4, T5, T6>>(call, method.Name, parameters.Prepend(instance)).Compile();
        }

        private static Action<TSource, T1, T2, T3, T4, T5, T6, T7> Create<TSource, T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            
            MethodInfo method = Storage.Action(action, out ParameterExpression? instance, out ParameterExpression[]? parameters, out MethodCallExpression? call);
            return Expression.Lambda<Action<TSource, T1, T2, T3, T4, T5, T6, T7>>(call, method.Name, parameters.Prepend(instance)).Compile();
        }

        private static Action<TSource, T1, T2, T3, T4, T5, T6, T7, T8> Create<TSource, T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            
            MethodInfo method = Storage.Action(action, out ParameterExpression? instance, out ParameterExpression[]? parameters, out MethodCallExpression? call);
            return Expression.Lambda<Action<TSource, T1, T2, T3, T4, T5, T6, T7, T8>>(call, method.Name, parameters.Prepend(instance)).Compile();
        }

        private static Action<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9> Create<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            
            MethodInfo method = Storage.Action(action, out ParameterExpression? instance, out ParameterExpression[]? parameters, out MethodCallExpression? call);
            return Expression.Lambda<Action<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9>>(call, method.Name, parameters.Prepend(instance)).Compile();
        }

        private static Action<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Create<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            
            MethodInfo method = Storage.Action(action, out ParameterExpression? instance, out ParameterExpression[]? parameters, out MethodCallExpression? call);
            return Expression.Lambda<Action<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>>(call, method.Name, parameters.Prepend(instance)).Compile();
        }

        private static Action<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Create<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            
            MethodInfo method = Storage.Action(action, out ParameterExpression? instance, out ParameterExpression[]? parameters, out MethodCallExpression? call);
            return Expression.Lambda<Action<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>>(call, method.Name, parameters.Prepend(instance)).Compile();
        }

        private static Action<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Create<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            
            MethodInfo method = Storage.Action(action, out ParameterExpression? instance, out ParameterExpression[]? parameters, out MethodCallExpression? call);
            return Expression.Lambda<Action<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>>(call, method.Name, parameters.Prepend(instance)).Compile();
        }

        private static Action<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Create<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            
            MethodInfo method = Storage.Action(action, out ParameterExpression? instance, out ParameterExpression[]? parameters, out MethodCallExpression? call);
            return Expression.Lambda<Action<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>>(call, method.Name, parameters.Prepend(instance)).Compile();
        }

        private static Action<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Create<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            
            MethodInfo method = Storage.Action(action, out ParameterExpression? instance, out ParameterExpression[]? parameters, out MethodCallExpression? call);
            return Expression.Lambda<Action<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>>(call, method.Name, parameters.Prepend(instance)).Compile();
        }

        private static Action<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Create<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            
            MethodInfo method = Storage.Action(action, out ParameterExpression? instance, out ParameterExpression[]? parameters, out MethodCallExpression? call);
            return Expression.Lambda<Action<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>>(call, method.Name, parameters.Prepend(instance)).Compile();
        }
        
        private static Func<TSource, TResult> Create<TSource, TResult>(Func<TResult> function)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }
            
            MethodInfo method = Storage.Function(function, out ParameterExpression? instance, out ParameterExpression[]? parameters, out MethodCallExpression? call);
            return Expression.Lambda<Func<TSource, TResult>>(Expression.Convert(call, method.ReturnType), method.Name, parameters.Prepend(instance)).Compile();
        }

        private static Func<TSource, T1, TResult> Create<TSource, T1, TResult>(Func<T1, TResult> function)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }
            
            MethodInfo method = Storage.Function(function, out ParameterExpression? instance, out ParameterExpression[]? parameters, out MethodCallExpression? call);
            return Expression.Lambda<Func<TSource, T1, TResult>>(Expression.Convert(call, method.ReturnType), method.Name, parameters.Prepend(instance)).Compile();
        }

        private static Func<TSource, T1, T2, TResult> Create<TSource, T1, T2, TResult>(Func<T1, T2, TResult> function)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }
            
            MethodInfo method = Storage.Function(function, out ParameterExpression? instance, out ParameterExpression[]? parameters, out MethodCallExpression? call);
            return Expression.Lambda<Func<TSource, T1, T2, TResult>>(Expression.Convert(call, method.ReturnType), method.Name, parameters.Prepend(instance)).Compile();
        }

        private static Func<TSource, T1, T2, T3, TResult> Create<TSource, T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> function)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }
            
            MethodInfo method = Storage.Function(function, out ParameterExpression? instance, out ParameterExpression[]? parameters, out MethodCallExpression? call);
            return Expression.Lambda<Func<TSource, T1, T2, T3, TResult>>(Expression.Convert(call, method.ReturnType), method.Name, parameters.Prepend(instance)).Compile();
        }

        private static Func<TSource, T1, T2, T3, T4, TResult> Create<TSource, T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> function)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }
            
            MethodInfo method = Storage.Function(function, out ParameterExpression? instance, out ParameterExpression[]? parameters, out MethodCallExpression? call);
            return Expression.Lambda<Func<TSource, T1, T2, T3, T4, TResult>>(Expression.Convert(call, method.ReturnType), method.Name, parameters.Prepend(instance)).Compile();
        }

        private static Func<TSource, T1, T2, T3, T4, T5, TResult> Create<TSource, T1, T2, T3, T4, T5, TResult>(Func<T1, T2, T3, T4, T5, TResult> function)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }
            
            MethodInfo method = Storage.Function(function, out ParameterExpression? instance, out ParameterExpression[]? parameters, out MethodCallExpression? call);
            return Expression.Lambda<Func<TSource, T1, T2, T3, T4, T5, TResult>>(Expression.Convert(call, method.ReturnType), method.Name, parameters.Prepend(instance)).Compile();
        }

        private static Func<TSource, T1, T2, T3, T4, T5, T6, TResult> Create<TSource, T1, T2, T3, T4, T5, T6, TResult>(Func<T1, T2, T3, T4, T5, T6, TResult> function)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }
            
            MethodInfo method = Storage.Function(function, out ParameterExpression? instance, out ParameterExpression[]? parameters, out MethodCallExpression? call);
            return Expression.Lambda<Func<TSource, T1, T2, T3, T4, T5, T6, TResult>>(Expression.Convert(call, method.ReturnType), method.Name, parameters.Prepend(instance)).Compile();
        }

        private static Func<TSource, T1, T2, T3, T4, T5, T6, T7, TResult> Create<TSource, T1, T2, T3, T4, T5, T6, T7, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, TResult> function)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }
            
            MethodInfo method = Storage.Function(function, out ParameterExpression? instance, out ParameterExpression[]? parameters, out MethodCallExpression? call);
            return Expression.Lambda<Func<TSource, T1, T2, T3, T4, T5, T6, T7, TResult>>(Expression.Convert(call, method.ReturnType), method.Name, parameters.Prepend(instance)).Compile();
        }

        private static Func<TSource, T1, T2, T3, T4, T5, T6, T7, T8, TResult> Create<TSource, T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> function)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }
            
            MethodInfo method = Storage.Function(function, out ParameterExpression? instance, out ParameterExpression[]? parameters, out MethodCallExpression? call);
            return Expression.Lambda<Func<TSource, T1, T2, T3, T4, T5, T6, T7, T8, TResult>>(Expression.Convert(call, method.ReturnType), method.Name, parameters.Prepend(instance)).Compile();
        }

        private static Func<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> Create<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> function)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }
            
            MethodInfo method = Storage.Function(function, out ParameterExpression? instance, out ParameterExpression[]? parameters, out MethodCallExpression? call);
            return Expression.Lambda<Func<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>>(Expression.Convert(call, method.ReturnType), method.Name, parameters.Prepend(instance)).Compile();
        }

        private static Func<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> Create<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> function)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }
            
            MethodInfo method = Storage.Function(function, out ParameterExpression? instance, out ParameterExpression[]? parameters, out MethodCallExpression? call);
            return Expression.Lambda<Func<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>>(Expression.Convert(call, method.ReturnType), method.Name, parameters.Prepend(instance)).Compile();
        }

        private static Func<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> Create<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> function)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }
            
            MethodInfo method = Storage.Function(function, out ParameterExpression? instance, out ParameterExpression[]? parameters, out MethodCallExpression? call);
            return Expression.Lambda<Func<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>>(Expression.Convert(call, method.ReturnType), method.Name, parameters.Prepend(instance)).Compile();
        }

        private static Func<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> Create<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> function)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }
            
            MethodInfo method = Storage.Function(function, out ParameterExpression? instance, out ParameterExpression[]? parameters, out MethodCallExpression? call);
            return Expression.Lambda<Func<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>>(Expression.Convert(call, method.ReturnType), method.Name, parameters.Prepend(instance)).Compile();
        }

        private static Func<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> Create<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> function)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }
            
            MethodInfo method = Storage.Function(function, out ParameterExpression? instance, out ParameterExpression[]? parameters, out MethodCallExpression? call);
            return Expression.Lambda<Func<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>>(Expression.Convert(call, method.ReturnType), method.Name, parameters.Prepend(instance)).Compile();
        }

        private static Func<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> Create<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> function)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }
            
            MethodInfo method = Storage.Function(function, out ParameterExpression? instance, out ParameterExpression[]? parameters, out MethodCallExpression? call);
            return Expression.Lambda<Func<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>>(Expression.Convert(call, method.ReturnType), method.Name, parameters.Prepend(instance)).Compile();
        }

        private static Func<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> Create<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> function)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }
            
            MethodInfo method = Storage.Function(function, out ParameterExpression? instance, out ParameterExpression[]? parameters, out MethodCallExpression? call);
            return Expression.Lambda<Func<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>>(Expression.Convert(call, method.ReturnType), method.Name, parameters.Prepend(instance)).Compile();
        }

        public static Action<TSource> Get<TSource>(Action action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            
            return Storage<Action<TSource>>.Delegate ??= Create<TSource>(action);
        }

        public static Action<TSource, T1> Get<TSource, T1>(Action<T1> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            
            return Storage<Action<TSource, T1>>.Delegate ??= Create<TSource, T1>(action);
        }

        public static Action<TSource, T1, T2> Get<TSource, T1, T2>(Action<T1, T2> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            
            return Storage<Action<TSource, T1, T2>>.Delegate ??= Create<TSource, T1, T2>(action);
        }

        public static Action<TSource, T1, T2, T3> Get<TSource, T1, T2, T3>(Action<T1, T2, T3> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            
            return Storage<Action<TSource, T1, T2, T3>>.Delegate ??= Create<TSource, T1, T2, T3>(action);
        }

        public static Action<TSource, T1, T2, T3, T4> Get<TSource, T1, T2, T3, T4>(Action<T1, T2, T3, T4> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            
            return Storage<Action<TSource, T1, T2, T3, T4>>.Delegate ??= Create<TSource, T1, T2, T3, T4>(action);
        }

        public static Action<TSource, T1, T2, T3, T4, T5> Get<TSource, T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            
            return Storage<Action<TSource, T1, T2, T3, T4, T5>>.Delegate ??= Create<TSource, T1, T2, T3, T4, T5>(action);
        }

        public static Action<TSource, T1, T2, T3, T4, T5, T6> Get<TSource, T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            
            return Storage<Action<TSource, T1, T2, T3, T4, T5, T6>>.Delegate ??= Create<TSource, T1, T2, T3, T4, T5, T6>(action);
        }

        public static Action<TSource, T1, T2, T3, T4, T5, T6, T7> Get<TSource, T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            
            return Storage<Action<TSource, T1, T2, T3, T4, T5, T6, T7>>.Delegate ??= Create<TSource, T1, T2, T3, T4, T5, T6, T7>(action);
        }

        public static Action<TSource, T1, T2, T3, T4, T5, T6, T7, T8> Get<TSource, T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            
            return Storage<Action<TSource, T1, T2, T3, T4, T5, T6, T7, T8>>.Delegate ??= Create<TSource, T1, T2, T3, T4, T5, T6, T7, T8>(action);
        }

        public static Action<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9> Get<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            
            return Storage<Action<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9>>.Delegate ??= Create<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9>(action);
        }

        public static Action<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Get<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            
            return Storage<Action<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>>.Delegate ??= Create<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(action);
        }

        public static Action<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Get<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            
            return Storage<Action<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>>.Delegate ??= Create<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(action);
        }

        public static Action<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Get<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            
            return Storage<Action<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>>.Delegate ??= Create<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(action);
        }

        public static Action<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Get<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            
            return Storage<Action<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>>.Delegate ??= Create<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(action);
        }

        public static Action<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Get<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            
            return Storage<Action<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>>.Delegate ??= Create<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(action);
        }

        public static Action<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Get<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            
            return Storage<Action<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>>.Delegate ??= Create<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(action);
        }

        public static Func<TSource, TResult> Get<TSource, TResult>(Func<TResult> function)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }
            
            return Storage<Func<TSource, TResult>>.Delegate ??= Create<TSource, TResult>(function);
        }

        public static Func<TSource, T1, TResult> Get<TSource, T1, TResult>(Func<T1, TResult> function)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }
            
            return Storage<Func<TSource, T1, TResult>>.Delegate ??= Create<TSource, T1, TResult>(function);
        }

        public static Func<TSource, T1, T2, TResult> Get<TSource, T1, T2, TResult>(Func<T1, T2, TResult> function)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }
            
            return Storage<Func<TSource, T1, T2, TResult>>.Delegate ??= Create<TSource, T1, T2, TResult>(function);
        }

        public static Func<TSource, T1, T2, T3, TResult> Get<TSource, T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> function)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }
            
            return Storage<Func<TSource, T1, T2, T3, TResult>>.Delegate ??= Create<TSource, T1, T2, T3, TResult>(function);
        }

        public static Func<TSource, T1, T2, T3, T4, TResult> Get<TSource, T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> function)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }
            
            return Storage<Func<TSource, T1, T2, T3, T4, TResult>>.Delegate ??= Create<TSource, T1, T2, T3, T4, TResult>(function);
        }

        public static Func<TSource, T1, T2, T3, T4, T5, TResult> Get<TSource, T1, T2, T3, T4, T5, TResult>(Func<T1, T2, T3, T4, T5, TResult> function)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }
            
            return Storage<Func<TSource, T1, T2, T3, T4, T5, TResult>>.Delegate ??= Create<TSource, T1, T2, T3, T4, T5, TResult>(function);
        }

        public static Func<TSource, T1, T2, T3, T4, T5, T6, TResult> Get<TSource, T1, T2, T3, T4, T5, T6, TResult>(Func<T1, T2, T3, T4, T5, T6, TResult> function)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }
            
            return Storage<Func<TSource, T1, T2, T3, T4, T5, T6, TResult>>.Delegate ??= Create<TSource, T1, T2, T3, T4, T5, T6, TResult>(function);
        }

        public static Func<TSource, T1, T2, T3, T4, T5, T6, T7, TResult> Get<TSource, T1, T2, T3, T4, T5, T6, T7, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, TResult> function)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }
            
            return Storage<Func<TSource, T1, T2, T3, T4, T5, T6, T7, TResult>>.Delegate ??= Create<TSource, T1, T2, T3, T4, T5, T6, T7, TResult>(function);
        }

        public static Func<TSource, T1, T2, T3, T4, T5, T6, T7, T8, TResult> Get<TSource, T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> function)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }
            
            return Storage<Func<TSource, T1, T2, T3, T4, T5, T6, T7, T8, TResult>>.Delegate ??= Create<TSource, T1, T2, T3, T4, T5, T6, T7, T8, TResult>(function);
        }

        public static Func<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> Get<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> function)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }
            
            return Storage<Func<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>>.Delegate ??= Create<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(function);
        }

        public static Func<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> Get<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> function)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }
            
            return Storage<Func<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>>.Delegate ??= Create<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(function);
        }

        public static Func<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> Get<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> function)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }
            
            return Storage<Func<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>>.Delegate ??= Create<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(function);
        }

        public static Func<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> Get<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> function)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }
            
            return Storage<Func<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>>.Delegate ??= Create<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(function);
        }

        public static Func<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> Get<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> function)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }
            
            return Storage<Func<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>>.Delegate ??= Create<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(function);
        }

        public static Func<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> Get<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> function)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }
            
            return Storage<Func<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>>.Delegate ??= Create<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(function);
        }

        public static Func<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> Get<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> function)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }
            
            return Storage<Func<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>>.Delegate ??= Create<TSource, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(function);
        }
    }
}