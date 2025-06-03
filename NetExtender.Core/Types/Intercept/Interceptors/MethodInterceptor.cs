// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using System.Threading.Tasks;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Intercept.Interfaces;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Delegates;

namespace NetExtender.Types.Intercept
{
    public class MethodInterceptor<TSender, TInfo> : MethodInterceptor<TSender, IMethodInterceptEventArgs, TInfo> where TSender : IInterceptTargetRaise<IMethodInterceptEventArgs>
    {
        public static MethodInterceptor<TSender, TInfo> Default { get; } = new MethodInterceptor<TSender, TInfo>();
        
        public MethodInterceptor()
        {
            Factory = MethodInterceptorUtilities.Factory<TInfo>.Instance;
        }
    }

    public class MethodInterceptor<TSender, TArgument, TInfo> : MemberInterceptor<TSender, TArgument, MethodInfo>, IAnyMethodInterceptor<TSender, TArgument, TInfo> where TSender : IInterceptTargetRaise<TArgument> where TArgument : class, IMethodInterceptEventArgs
    {
        public IMethodInterceptEventArgsFactory<TArgument, TInfo>? Factory { get; init; }

        // ReSharper disable once CognitiveComplexity
        public virtual void Intercept<TDelegate>(TSender sender, TArgument args, TDelegate @delegate) where TDelegate : struct, IValueAction<TDelegate>
        {
            if (sender is null)
            {
                throw new ArgumentNullException(nameof(sender));
            }

            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            args.Token.ThrowIfCancellationRequested();
            sender.RaiseIntercepting(args);

            if (args.IsIgnore)
            {
                return;
            }

            if (args.Exception is not null)
            {
                throw args.Exception;
            }

            if (args.IsSeal)
            {
                return;
            }

            Boolean successful = true;

            try
            {
                @delegate.Invoke();
            }
            catch (Exception exception)
            {
                successful = false;
                args.Intercept(exception);
                sender.RaiseIntercepted(args);

                if (ReferenceEquals(args.Exception, exception))
                {
                    throw;
                }
            }

            if (successful)
            {
                sender.RaiseIntercepted(args);
            }

            if (args.IsIgnore)
            {
                return;
            }

            if (args.Exception is not null)
            {
                throw args.Exception;
            }
        }

        public TResult Intercept<TDelegate, TResult>(TSender sender, TArgument args, TDelegate @delegate) where TDelegate : struct, IValueFunc<TDelegate, TResult>
        {
            return Intercept<TDelegate, TResult>(sender, args, @delegate, out _);
        }

        // ReSharper disable once RedundantAssignment
        public TResult Intercept<TDelegate, TResult>(TSender sender, TArgument args, TDelegate @delegate, Maybe<TResult> result) where TDelegate : struct, IValueFunc<TDelegate, TResult>
        {
            return Intercept(sender, args, @delegate, out result);
        }

        public virtual TResult Intercept<TDelegate, TResult>(TSender sender, TArgument args, TDelegate @delegate, out Maybe<TResult> result) where TDelegate : struct, IValueFunc<TDelegate, TResult>
        {
            if (sender is null)
            {
                throw new ArgumentNullException(nameof(sender));
            }

            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            if (args is not IMethodInterceptEventArgs<TResult> argument)
            {
                throw new TypeNotSupportedException(typeof(TArgument));
            }
            
            result = default;
            args.Token.ThrowIfCancellationRequested();
            sender.RaiseIntercepting(args);
            
            if (args.Exception is not null)
            {
                throw args.Exception;
            }
            
            if (args.IsSeal)
            {
                return argument.Value;
            }

            try
            {
                result = @delegate.Invoke();
            }
            catch (Exception exception)
            {
                args.Intercept(exception);
                sender.RaiseIntercepted(args);

                if (ReferenceEquals(args.Exception, exception))
                {
                    throw;
                }
            }

            if (result.HasValue)
            {
                argument.Intercept(result.Value);
                sender.RaiseIntercepted(args);
            }

            if (args.Exception is not null)
            {
                throw args.Exception;
            }

            return argument.Value;
        }
        
        // ReSharper disable once CognitiveComplexity
        public virtual async ValueTask InterceptAsync<TDelegate>(TSender sender, TArgument args, TDelegate @delegate) where TDelegate : struct, IAsyncValueAction<TDelegate>
        {
            if (sender is null)
            {
                throw new ArgumentNullException(nameof(sender));
            }

            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            args.Token.ThrowIfCancellationRequested();
            sender.RaiseIntercepting(args);
            await Wait(args);

            if (args.IsIgnore)
            {
                return;
            }

            if (args.Exception is not null)
            {
                throw args.Exception;
            }

            if (args.IsSeal)
            {
                return;
            }

            Boolean successful = true;

            try
            {
                if (@delegate.IsValue)
                {
                    await @delegate.AsValueTask();
                }
                else
                {
                    await @delegate.AsTask();
                }
            }
            catch (Exception exception)
            {
                successful = false;
                args.Intercept(exception);
                sender.RaiseIntercepted(args);
                await Wait(args);

                if (ReferenceEquals(args.Exception, exception))
                {
                    throw;
                }
            }

            if (successful)
            {
                sender.RaiseIntercepted(args);
                await Wait(args);
            }

            if (args.IsIgnore)
            {
                return;
            }

            if (args.Exception is not null)
            {
                throw args.Exception;
            }
        }

        public ValueTask<TResult> InterceptAsync<TDelegate, TResult>(TSender sender, TArgument args, TDelegate @delegate) where TDelegate : struct, IAsyncValueFunc<TDelegate, TResult>
        {
            return InterceptAsync<TDelegate, TResult>(sender, args, @delegate, default);
        }

        // ReSharper disable once RedundantAssignment
        public virtual async ValueTask<TResult> InterceptAsync<TDelegate, TResult>(TSender sender, TArgument args, TDelegate @delegate, Maybe<TResult> result) where TDelegate : struct, IAsyncValueFunc<TDelegate, TResult>
        {
            if (sender is null)
            {
                throw new ArgumentNullException(nameof(sender));
            }

            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            if (args is not IMethodInterceptEventArgs<TResult> argument)
            {
                throw new TypeNotSupportedException(typeof(TArgument));
            }
            
            args.Token.ThrowIfCancellationRequested();
            sender.RaiseIntercepting(args);
            await Wait(args);
            
            if (args.Exception is not null)
            {
                throw args.Exception;
            }
            
            if (args.IsSeal)
            {
                return argument.Value;
            }

            try
            {
                result = @delegate.IsValue ? await @delegate.AsValueTask() : await @delegate.AsTask();
            }
            catch (Exception exception)
            {
                args.Intercept(exception);
                sender.RaiseIntercepted(args);
                await Wait(args);

                if (ReferenceEquals(args.Exception, exception))
                {
                    throw;
                }
            }

            if (result.HasValue)
            {
                argument.Intercept(result.Value);
                sender.RaiseIntercepted(args);
                await Wait(args);
            }

            if (args.Exception is not null)
            {
                throw args.Exception;
            }

            return argument.Value;
        }

        public void Intercept(TSender sender, TInfo? info, Action method)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, Array.Empty<Object?>(), info);
                Intercept(sender, args, ValueDelegate.Create(method));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public void Intercept<T>(TSender sender, TInfo? info, Action<T> method, T argument)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { argument }, info);
                Intercept(sender, args, ValueDelegate.Create(method, argument));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public void Intercept<T1, T2>(TSender sender, TInfo? info, Action<T1, T2> method, T1 first, T2 second)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { first, second }, info);
                Intercept(sender, args, ValueDelegate.Create(method, first, second));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public void Intercept<T1, T2, T3>(TSender sender, TInfo? info, Action<T1, T2, T3> method, T1 first, T2 second, T3 third)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { first, second, third }, info);
                Intercept(sender, args, ValueDelegate.Create(method, first, second, third));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public void Intercept<T1, T2, T3, T4>(TSender sender, TInfo? info, Action<T1, T2, T3, T4> method, T1 first, T2 second, T3 third, T4 fourth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { first, second, third, fourth }, info);
                Intercept(sender, args, ValueDelegate.Create(method, first, second, third, fourth));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public void Intercept<T1, T2, T3, T4, T5>(TSender sender, TInfo? info, Action<T1, T2, T3, T4, T5> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { first, second, third, fourth, fifth }, info);
                Intercept(sender, args, ValueDelegate.Create(method, first, second, third, fourth, fifth));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public void Intercept<T1, T2, T3, T4, T5, T6>(TSender sender, TInfo? info, Action<T1, T2, T3, T4, T5, T6> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth }, info);
                Intercept(sender, args, ValueDelegate.Create(method, first, second, third, fourth, fifth, sixth));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public void Intercept<T1, T2, T3, T4, T5, T6, T7>(TSender sender, TInfo? info, Action<T1, T2, T3, T4, T5, T6, T7> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh }, info);
                Intercept(sender, args, ValueDelegate.Create(method, first, second, third, fourth, fifth, sixth, seventh));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public void Intercept<T1, T2, T3, T4, T5, T6, T7, T8>(TSender sender, TInfo? info, Action<T1, T2, T3, T4, T5, T6, T7, T8> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth }, info);
                Intercept(sender, args, ValueDelegate.Create(method, first, second, third, fourth, fifth, sixth, seventh, eighth));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public void Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9>(TSender sender, TInfo? info, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth }, info);
                Intercept(sender, args, ValueDelegate.Create(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public void Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(TSender sender, TInfo? info, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth }, info);
                Intercept(sender, args, ValueDelegate.Create(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public void Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(TSender sender, TInfo? info, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh }, info);
                Intercept(sender, args, ValueDelegate.Create(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public void Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(TSender sender, TInfo? info, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth }, info);
                Intercept(sender, args, ValueDelegate.Create(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public void Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(TSender sender, TInfo? info, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth }, info);
                Intercept(sender, args, ValueDelegate.Create(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public void Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(TSender sender, TInfo? info, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth }, info);
                Intercept(sender, args, ValueDelegate.Create(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public void Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(TSender sender, TInfo? info, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth }, info);
                Intercept(sender, args, ValueDelegate.Create(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public void Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(TSender sender, TInfo? info, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth, T16 sixteenth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth, sixteenth }, info);
                Intercept(sender, args, ValueDelegate.Create(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth, sixteenth));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public TResult Intercept<TResult>(TSender sender, TInfo? info, Func<TResult> method)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, Array.Empty<Object?>(), info);
                return Intercept(sender, args, ValueDelegate.Create(method), out Maybe<TResult> _);
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public TResult Intercept<T, TResult>(TSender sender, TInfo? info, Func<T, TResult> method, T argument)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { argument }, info);
                return Intercept(sender, args, ValueDelegate.Create(method, argument), out Maybe<TResult> _);
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public TResult Intercept<T1, T2, TResult>(TSender sender, TInfo? info, Func<T1, T2, TResult> method, T1 first, T2 second)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { first, second }, info);
                return Intercept(sender, args, ValueDelegate.Create(method, first, second), out Maybe<TResult> _);
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public TResult Intercept<T1, T2, T3, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, TResult> method, T1 first, T2 second, T3 third)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { first, second, third }, info);
                return Intercept(sender, args, ValueDelegate.Create(method, first, second, third), out Maybe<TResult> _);
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public TResult Intercept<T1, T2, T3, T4, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, TResult> method, T1 first, T2 second, T3 third, T4 fourth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { first, second, third, fourth }, info);
                return Intercept(sender, args, ValueDelegate.Create(method, first, second, third, fourth), out Maybe<TResult> _);
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public TResult Intercept<T1, T2, T3, T4, T5, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, TResult> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { first, second, third, fourth, fifth }, info);
                return Intercept(sender, args, ValueDelegate.Create(method, first, second, third, fourth, fifth), out Maybe<TResult> _);
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public TResult Intercept<T1, T2, T3, T4, T5, T6, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, TResult> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth }, info);
                return Intercept(sender, args, ValueDelegate.Create(method, first, second, third, fourth, fifth, sixth), out Maybe<TResult> _);
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public TResult Intercept<T1, T2, T3, T4, T5, T6, T7, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, TResult> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh }, info);
                return Intercept(sender, args, ValueDelegate.Create(method, first, second, third, fourth, fifth, sixth, seventh), out Maybe<TResult> _);
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public TResult Intercept<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth }, info);
                return Intercept(sender, args, ValueDelegate.Create(method, first, second, third, fourth, fifth, sixth, seventh, eighth), out Maybe<TResult> _);
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public TResult Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth }, info);
                return Intercept(sender, args, ValueDelegate.Create(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth), out Maybe<TResult> _);
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public TResult Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth }, info);
                return Intercept(sender, args, ValueDelegate.Create(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth), out Maybe<TResult> _);
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public TResult Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh }, info);
                return Intercept(sender, args, ValueDelegate.Create(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh), out Maybe<TResult> _);
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public TResult Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth }, info);
                return Intercept(sender, args, ValueDelegate.Create(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth), out Maybe<TResult> _);
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public TResult Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth }, info);
                return Intercept(sender, args, ValueDelegate.Create(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth), out Maybe<TResult> _);
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public TResult Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth }, info);
                return Intercept(sender, args, ValueDelegate.Create(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth), out Maybe<TResult> _);
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public TResult Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth }, info);
                return Intercept(sender, args, ValueDelegate.Create(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth), out Maybe<TResult> _);
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public TResult Intercept<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth, T16 sixteenth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth, sixteenth }, info);
                return Intercept(sender, args, ValueDelegate.Create(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth, sixteenth), out Maybe<TResult> _);
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask InterceptAsync(TSender sender, TInfo? info, Func<Task> method)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, Array.Empty<Object?>(), info);
                await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask InterceptAsync<T>(TSender sender, TInfo? info, Func<T, Task> method, T argument)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { argument }, info);
                await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, argument));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask InterceptAsync<T1, T2>(TSender sender, TInfo? info, Func<T1, T2, Task> method, T1 first, T2 second)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { first, second }, info);
                await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask InterceptAsync<T1, T2, T3>(TSender sender, TInfo? info, Func<T1, T2, T3, Task> method, T1 first, T2 second, T3 third)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { first, second, third }, info);
                await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask InterceptAsync<T1, T2, T3, T4>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, Task> method, T1 first, T2 second, T3 third, T4 fourth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { first, second, third, fourth }, info);
                await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask InterceptAsync<T1, T2, T3, T4, T5>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, Task> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { first, second, third, fourth, fifth }, info);
                await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, Task> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth }, info);
                await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth, sixth));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, Task> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh }, info);
                await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth, sixth, seventh));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, Task> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth }, info);
                await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth, sixth, seventh, eighth));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, Task> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth }, info);
                await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, Task> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth }, info);
                await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, Task> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh }, info);
                await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, Task> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth }, info);
                await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, Task> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth }, info);
                await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, Task> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth }, info);
                await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, Task> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth }, info);
                await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, Task> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth, T16 sixteenth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth, sixteenth }, info);
                await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth, sixteenth));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask InterceptAsync(TSender sender, TInfo? info, Func<ValueTask> method)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, Array.Empty<Object?>(), info);
                await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask InterceptAsync<T>(TSender sender, TInfo? info, Func<T, ValueTask> method, T argument)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { argument }, info);
                await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, argument));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask InterceptAsync<T1, T2>(TSender sender, TInfo? info, Func<T1, T2, ValueTask> method, T1 first, T2 second)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { first, second }, info);
                await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask InterceptAsync<T1, T2, T3>(TSender sender, TInfo? info, Func<T1, T2, T3, ValueTask> method, T1 first, T2 second, T3 third)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { first, second, third }, info);
                await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask InterceptAsync<T1, T2, T3, T4>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, ValueTask> method, T1 first, T2 second, T3 third, T4 fourth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { first, second, third, fourth }, info);
                await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask InterceptAsync<T1, T2, T3, T4, T5>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, ValueTask> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { first, second, third, fourth, fifth }, info);
                await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, ValueTask> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth }, info);
                await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth, sixth));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, ValueTask> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh }, info);
                await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth, sixth, seventh));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, ValueTask> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth }, info);
                await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth, sixth, seventh, eighth));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, ValueTask> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth }, info);
                await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, ValueTask> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth }, info);
                await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, ValueTask> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh }, info);
                await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, ValueTask> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth }, info);
                await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, ValueTask> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth }, info);
                await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, ValueTask> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth }, info);
                await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, ValueTask> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth }, info);
                await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, ValueTask> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth, T16 sixteenth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }
            
            TArgument? args = null;

            try
            {
                args = Factory.Create(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth, sixteenth }, info);
                await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth, sixteenth));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask<TResult> InterceptAsync<TResult>(TSender sender, TInfo? info, Func<Task<TResult>> method)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, Array.Empty<Object?>(), info);
                return await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method), default(Maybe<TResult>));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask<TResult> InterceptAsync<T, TResult>(TSender sender, TInfo? info, Func<T, Task<TResult>> method, T argument)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { argument }, info);
                return await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, argument), default(Maybe<TResult>));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask<TResult> InterceptAsync<T1, T2, TResult>(TSender sender, TInfo? info, Func<T1, T2, Task<TResult>> method, T1 first, T2 second)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { first, second }, info);
                return await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second), default(Maybe<TResult>));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask<TResult> InterceptAsync<T1, T2, T3, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, Task<TResult>> method, T1 first, T2 second, T3 third)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { first, second, third }, info);
                return await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third), default(Maybe<TResult>));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, Task<TResult>> method, T1 first, T2 second, T3 third, T4 fourth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { first, second, third, fourth }, info);
                return await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth), default(Maybe<TResult>));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, Task<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { first, second, third, fourth, fifth }, info);
                return await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth), default(Maybe<TResult>));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, Task<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth }, info);
                return await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth, sixth), default(Maybe<TResult>));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, Task<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh }, info);
                return await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth, sixth, seventh), default(Maybe<TResult>));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, Task<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth }, info);
                return await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth, sixth, seventh, eighth), default(Maybe<TResult>));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, Task<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth }, info);
                return await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth), default(Maybe<TResult>));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, Task<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth }, info);
                return await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth), default(Maybe<TResult>));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, Task<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh }, info);
                return await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh), default(Maybe<TResult>));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, Task<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth }, info);
                return await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth), default(Maybe<TResult>));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, Task<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth }, info);
                return await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth), default(Maybe<TResult>));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, Task<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth }, info);
                return await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth), default(Maybe<TResult>));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, Task<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth }, info);
                return await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth), default(Maybe<TResult>));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, Task<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth, T16 sixteenth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth, sixteenth }, info);
                return await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth, sixteenth), default(Maybe<TResult>));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask<TResult> InterceptAsync<TResult>(TSender sender, TInfo? info, Func<ValueTask<TResult>> method)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, Array.Empty<Object?>(), info);
                return await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method), default(Maybe<TResult>));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask<TResult> InterceptAsync<T, TResult>(TSender sender, TInfo? info, Func<T, ValueTask<TResult>> method, T argument)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { argument }, info);
                return await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, argument), default(Maybe<TResult>));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask<TResult> InterceptAsync<T1, T2, TResult>(TSender sender, TInfo? info, Func<T1, T2, ValueTask<TResult>> method, T1 first, T2 second)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { first, second }, info);
                return await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second), default(Maybe<TResult>));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask<TResult> InterceptAsync<T1, T2, T3, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, ValueTask<TResult>> method, T1 first, T2 second, T3 third)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { first, second, third }, info);
                return await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third), default(Maybe<TResult>));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, ValueTask<TResult>> method, T1 first, T2 second, T3 third, T4 fourth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { first, second, third, fourth }, info);
                return await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth), default(Maybe<TResult>));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, ValueTask<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { first, second, third, fourth, fifth }, info);
                return await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth), default(Maybe<TResult>));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, ValueTask<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth }, info);
                return await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth, sixth), default(Maybe<TResult>));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, ValueTask<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh }, info);
                return await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth, sixth, seventh), default(Maybe<TResult>));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, ValueTask<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth }, info);
                return await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth, sixth, seventh, eighth), default(Maybe<TResult>));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, ValueTask<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth }, info);
                return await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth), default(Maybe<TResult>));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, ValueTask<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth }, info);
                return await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth), default(Maybe<TResult>));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, ValueTask<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh }, info);
                return await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh), default(Maybe<TResult>));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, ValueTask<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth }, info);
                return await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth), default(Maybe<TResult>));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, ValueTask<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth }, info);
                return await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth), default(Maybe<TResult>));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, ValueTask<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth }, info);
                return await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth), default(Maybe<TResult>));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, ValueTask<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth }, info);
                return await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth), default(Maybe<TResult>));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }

        public async ValueTask<TResult> InterceptAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(TSender sender, TInfo? info, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, ValueTask<TResult>> method, T1 first, T2 second, T3 third, T4 fourth, T5 fifth, T6 sixth, T7 seventh, T8 eighth, T9 ninth, T10 tenth, T11 eleventh, T12 twelfth, T13 thirteenth, T14 fourteenth, T15 fifteenth, T16 sixteenth)
        {
            if (Factory is null)
            {
                throw new InvalidOperationException($"{nameof(Factory)} is not set.");
            }

            TArgument? args = null;

            try
            {
                args = Factory.Create<TResult>(method.Method, new Object?[] { first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth, sixteenth }, info);
                return await InterceptAsync(sender, args, ValueDelegate.CreateAsync(method, first, second, third, fourth, fifth, sixth, seventh, eighth, ninth, tenth, eleventh, twelfth, thirteenth, fourteenth, fifteenth, sixteenth), default(Maybe<TResult>));
            }
            finally
            {
                (args as IDisposable)?.Dispose();
            }
        }
    }

    internal static class MethodInterceptorUtilities
    {
        internal sealed class Factory<TInfo> : MethodInterceptEventArgsFactory<IMethodInterceptEventArgs, TInfo>
        {
            public static Factory<TInfo> Instance { get; } = new Factory<TInfo>();

            private Factory()
            {
            }

            public override IMethodInterceptEventArgs Create(MethodInfo method, IEnumerable<Object?>? parameters, TInfo? info)
            {
                return new MethodInterceptEventArgs(method, parameters);
            }

            public override IMethodInterceptEventArgs Create(MethodInfo method, ImmutableArray<Object?> parameters, TInfo? info)
            {
                return new MethodInterceptEventArgs(method, parameters);
            }

            public override IMethodInterceptEventArgs Create(MethodInfo method, IEnumerable<Object?>? parameters, Exception exception, TInfo? info)
            {
                return new MethodInterceptEventArgs(method, parameters);
            }

            public override IMethodInterceptEventArgs Create(MethodInfo method, ImmutableArray<Object?> parameters, Exception exception, TInfo? info)
            {
                return new MethodInterceptEventArgs(method, parameters);
            }

            public override IMethodInterceptEventArgs Create<T>(MethodInfo method, IEnumerable<Object?>? parameters, TInfo? info)
            {
                return new MethodInterceptEventArgs<T>(method, parameters);
            }

            public override IMethodInterceptEventArgs Create<T>(MethodInfo method, ImmutableArray<Object?> parameters, TInfo? info)
            {
                return new MethodInterceptEventArgs<T>(method, parameters);
            }

            public override IMethodInterceptEventArgs Create<T>(MethodInfo method, T value, IEnumerable<Object?>? parameters, TInfo? info)
            {
                return new MethodInterceptEventArgs<T>(method, value, parameters);
            }

            public override IMethodInterceptEventArgs Create<T>(MethodInfo method, T value, ImmutableArray<Object?> parameters, TInfo? info)
            {
                return new MethodInterceptEventArgs<T>(method, value, parameters);
            }

            public override IMethodInterceptEventArgs Create<T>(MethodInfo method, IEnumerable<Object?>? parameters, Exception exception, TInfo? info)
            {
                return new MethodInterceptEventArgs<T>(method, parameters, exception);
            }

            public override IMethodInterceptEventArgs Create<T>(MethodInfo method, ImmutableArray<Object?> parameters, Exception exception, TInfo? info)
            {
                return new MethodInterceptEventArgs<T>(method, parameters, exception);
            }
        }
    }
}