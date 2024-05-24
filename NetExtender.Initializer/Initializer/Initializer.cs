// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using NetExtender.Types.Exceptions;

namespace NetExtender.Initializer
{
    public abstract class Initializer
    {
        private static Task Stop
        {
            get
            {
                return Task.Delay(TimeSpan.FromMilliseconds(100));
            }
        }

        protected static Awaiter<Int32> Negative
        {
            get
            {
                return Stop.ContinueWith(_ => -1);
            }
        }

        protected static Awaiter<Int32> Zero
        {
            get
            {
                return Stop.ContinueWith(_ => 0);
            }
        }

        protected static Awaiter<Int32> One
        {
            get
            {
                return Stop.ContinueWith(_ => 1);
            }
        }

        protected static Awaiter<Int32> Two
        {
            get
            {
                return Stop.ContinueWith(_ => 2);
            }
        }
        
        protected static Awaiter<Int32> Three
        {
            get
            {
                return Stop.ContinueWith(_ => 3);
            }
        }

        protected static Awaiter<Int32> Four
        {
            get
            {
                return Stop.ContinueWith(_ => 4);
            }
        }

        protected static Awaiter<Int32> Five
        {
            get
            {
                return Stop.ContinueWith(_ => 5);
            }
        }

        protected static Awaiter<Int32> Six
        {
            get
            {
                return Stop.ContinueWith(_ => 6);
            }
        }

        protected static Awaiter<Int32> Seven
        {
            get
            {
                return Stop.ContinueWith(_ => 7);
            }
        }

        protected static Awaiter<Int32> Eight
        {
            get
            {
                return Stop.ContinueWith(_ => 8);
            }
        }

        protected static Awaiter<Int32> Nine
        {
            get
            {
                return Stop.ContinueWith(_ => 9);
            }
        }

        protected static Awaiter<Int32> Ten
        {
            get
            {
                return Stop.ContinueWith(_ => 10);
            }
        }

        protected static Awaiter<Int32> Eleven
        {
            get
            {
                return Stop.ContinueWith(_ => 11);
            }
        }

        protected static Awaiter<Int32> Twelve
        {
            get
            {
                return Stop.ContinueWith(_ => 12);
            }
        }

        protected static Awaiter<Int32> Random
        {
            get
            {
                return Stop.ContinueWith(_ => System.Random.Shared.Next());
            }
        }

        protected Initializer(Boolean subscribe)
        {
            if (!subscribe)
            {
                return;
            }

            AppDomain.CurrentDomain.ProcessExit += ExitShutdown;
            AppDomain.CurrentDomain.UnhandledException += UnhandledException;
        }

        protected virtual void Action(Object? sender, Exception? exception, InitializerUnhandledExceptionState action)
        {
            switch (action)
            {
                case InitializerUnhandledExceptionState.None:
                    return;
                case InitializerUnhandledExceptionState.Exception:
                    goto case InitializerUnhandledExceptionState.None;
                case InitializerUnhandledExceptionState.Shutdown:
                    Shutdown(sender);
                    return;
                case InitializerUnhandledExceptionState.Terminate:
                    Terminate(sender, exception);
                    return;
                default:
                    goto case InitializerUnhandledExceptionState.Terminate;
            }
        }

        private static Boolean IsShutdown(Exception? exception, [MaybeNullWhen(false)] out ShutdownException result)
        {
            while (exception is not null)
            {
                if (exception is ShutdownException shutdown)
                {
                    result = shutdown;
                    return true;
                }

                exception = exception.InnerException;
            }

            result = default;
            return false;
        }

        private void UnhandledException(Object? sender, UnhandledExceptionEventArgs args)
        {
            Exception? exception = args.ExceptionObject as Exception;
            InitializerUnhandledExceptionState state = args.IsTerminating ? InitializerUnhandledExceptionState.Terminate : InitializerUnhandledExceptionState.Exception;
            UnhandledException(sender, exception, state);
        }

        protected void UnhandledException(Object? sender, Exception? exception, InitializerUnhandledExceptionState action)
        {
            if (IsShutdown(exception, out ShutdownException? shutdown))
            {
                ShutdownException(sender, shutdown, ref action);
                return;
            }

            UnhandledException(sender, exception, ref action);
            Action(sender, exception, action);
        }

        protected virtual void UnhandledException(Object? sender, Exception? exception, ref InitializerUnhandledExceptionState action)
        {
            UnhandledException<ExceptionHandler>(sender, exception, ref action);
        }
        
        protected void UnhandledException<T>(Object? sender, Exception? exception, ref InitializerUnhandledExceptionState action) where T : ExceptionHandler, new()
        {
            T handler = new T();
            UnhandledException(handler, sender, exception, ref action);
        }

        private void UnhandledException(ExceptionHandler handler, Object? sender, Exception? exception, ref InitializerUnhandledExceptionState action)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            handler.Handle(this, sender, exception, ref action);
        }

        protected virtual void ShutdownException(Object? sender, ShutdownException exception, ref InitializerUnhandledExceptionState action)
        {
            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            Shutdown(sender, exception.Code);
        }

        private void ExitShutdown(Object? sender, EventArgs args)
        {
            Shutdown(sender, true);
        }

        protected void Shutdown(Object? sender)
        {
            Shutdown(sender, false);
        }

        protected void Shutdown(Object? sender, Boolean exit)
        {
            Shutdown(sender, Environment.ExitCode, exit);
        }

        protected virtual void Shutdown(Object? sender, Int32 code)
        {
            Shutdown(sender, code, false);
        }

        protected virtual void Shutdown(Object? sender, Int32 code, Boolean exit)
        {
            Environment.Exit(code);
        }

        protected virtual void Terminate(Object? sender, Exception? exception)
        {
            Environment.Exit(exception?.HResult ?? 1);
        }

        // ReSharper disable once AsyncConverter.AsyncMethodNamingHighlighting
        protected static Awaiter<Int32> Exit()
        {
            return Zero;
        }

        // ReSharper disable once AsyncConverter.AsyncMethodNamingHighlighting
        protected static Awaiter<Int32> Exit(Int32 code)
        {
            return code switch
            {
                -1 => Negative,
                0 => Zero,
                1 => One,
                2 => Two,
                3 => Three,
                4 => Four,
                5 => Five,
                6 => Six,
                7 => Seven,
                8 => Eight,
                9 => Nine,
                10 => Ten,
                11 => Eleven,
                12 => Twelve,
                _ => Stop.ContinueWith(_ => code)
            };
        }

        // ReSharper disable once AsyncConverter.AsyncMethodNamingHighlighting
        protected static Awaiter<Int32> Exit(TimeSpan delay)
        {
            return Exit(delay, 0);
        }

        // ReSharper disable once AsyncConverter.AsyncMethodNamingHighlighting
        protected static Awaiter<Int32> Exit(TimeSpan delay, Int32 code)
        {
            return Task.Delay(delay).ContinueWith(_ => code);
        }

        protected internal virtual void InitializeNetExtender(INetExtenderFrameworkInitializer initializer)
        {
        }
        
        protected class ExceptionHandler
        {
            private Boolean DynamicHandle(Initializer initializer, Object? sender, Exception? exception, ref InitializerUnhandledExceptionState action)
            {
                if (initializer is null)
                {
                    throw new ArgumentNullException(nameof(initializer));
                }

                Type current = GetType();
                MethodInfo? handler = null;
                Type? type = initializer.GetType();
                while (type is not null && type != typeof(Initializer))
                {
                    const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
                    handler = current.GetMethod(nameof(Handle), binding, null, new[] { type, typeof(Object), typeof(Exception), typeof(InitializerUnhandledExceptionState).MakeByRefType() }, null);

                    if (handler is not null)
                    {
                        break;
                    }

                    type = type.BaseType;
                }
                
                if (handler is null || handler.DeclaringType == typeof(ExceptionHandler))
                {
                    return false;
                }

                Object?[] parameters = { initializer, sender, exception, action };
                handler.Invoke(this, parameters);
                action = (InitializerUnhandledExceptionState) (parameters[3] ?? action);
                return true;
            }

            internal Boolean Handle(Initializer initializer, Object? sender, Exception? exception, ref InitializerUnhandledExceptionState action)
            {
                if (initializer is null)
                {
                    throw new ArgumentNullException(nameof(initializer));
                }

                if (DynamicHandle(initializer, sender, exception, ref action))
                {
                    return true;
                }
                
                Handle(sender, exception, ref action);
                return false;
            }

            public virtual void Handle(Object? sender, Exception? exception, ref InitializerUnhandledExceptionState action)
            {
                Console.WriteLine(exception);
            }
        }
        
        public readonly struct Awaiter<T> : IEquatable<Awaiter<T>>
        {
            public static implicit operator Awaiter<T>(Task<T>? value)
            {
                return value is not null ? new Awaiter<T>(value) : default;
            }
            
            public static implicit operator Task<T>(Awaiter<T> value)
            {
                return value.Internal ?? Task.FromResult(default(T)!);
            }
            
            public static Boolean operator ==(Awaiter<T> first, Awaiter<T> second)
            {
                return first.Equals(second);
            }

            public static Boolean operator !=(Awaiter<T> first, Awaiter<T> second)
            {
                return !(first == second);
            }
            
            private Task<T>? Internal { get; }

            public Boolean IsCompleted
            {
                get
                {
                    return Internal?.IsCompleted ?? true;
                }
            }

            public Boolean IsCompletedSuccessfully
            {
                get
                {
                    return Internal?.IsCompletedSuccessfully ?? true;
                }
            }

            public Boolean IsCanceled
            {
                get
                {
                    return Internal?.IsCanceled ?? false;
                }
            }

            public Boolean IsFaulted
            {
                get
                {
                    return Internal?.IsFaulted ?? false;
                }
            }
            
            public Awaiter(Task<T> value)
            {
                Internal = value ?? throw new ArgumentNullException(nameof(value));
            }
            
            public TaskAwaiter<T> GetAwaiter()
            {
                return Internal?.GetAwaiter() ?? Task.FromResult(default(T)!).GetAwaiter();
            }

            public ConfiguredTaskAwaitable<T> ConfigureAwait(Boolean continueOnCapturedContext)
            {
                return Internal?.ConfigureAwait(continueOnCapturedContext) ?? Task.FromResult(default(T)!).ConfigureAwait(continueOnCapturedContext);
            }
        
            public override Int32 GetHashCode()
            {
                return Internal?.GetHashCode() ?? 0;
            }

            public override Boolean Equals(Object? other)
            {
                return other switch
                {
                    Awaiter<T> result => Equals(result),
                    Task<T> result => Equals(result),
                    _ => false
                };
            }

            public Boolean Equals(Task<T>? other)
            {
                return Internal?.Equals(other) ?? Internal == other;
            }

            public Boolean Equals(Awaiter<T> other)
            {
                return Internal?.Equals(other.Internal) ?? Internal == other.Internal;
            }
        }
    }
}