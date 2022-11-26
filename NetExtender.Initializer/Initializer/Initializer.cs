// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using NetExtender.Types.Exceptions;

namespace NetExtender.Initializer
{
    public abstract class Initializer
    {
        public static Task Stop
        {
            get
            {
                return Task.Delay(TimeSpan.FromMilliseconds(100));
            }
        }

        public static Task<Int32> Zero
        {
            get
            {
                return Stop.ContinueWith(_ => 0);
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

        protected internal virtual void InitializeNetExtender(INetExtenderFrameworkInitializer initializer)
        {
        }
    }
}