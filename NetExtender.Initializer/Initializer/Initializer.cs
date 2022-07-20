// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading.Tasks;

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

        protected Initializer(Boolean subscribe)
        {
            if (!subscribe)
            {
                return;
            }

            AppDomain.CurrentDomain.ProcessExit += ExitShutdown;
            AppDomain.CurrentDomain.UnhandledException += UnhandledException;
        }
        
        private void UnhandledException(Object? sender, UnhandledExceptionEventArgs args)
        {
            Exception? exception = args.ExceptionObject as Exception;
            InitializerUnhandledExceptionState state = args.IsTerminating ? InitializerUnhandledExceptionState.Terminate : InitializerUnhandledExceptionState.Exception;
            UnhandledException(sender, exception, state);
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

        protected void UnhandledException(Object? sender, Exception? exception, InitializerUnhandledExceptionState action)
        {
            UnhandledException(sender, exception, ref action);
            Action(sender, exception, action);
        }

        protected virtual void UnhandledException(Object? sender, Exception? exception, ref InitializerUnhandledExceptionState action)
        {
        }

        private void ExitShutdown(Object? sender, EventArgs args)
        {
            Shutdown(sender, true);
        }
        
        protected void Shutdown(Object? sender)
        {
            Shutdown(sender, false);
        }

        protected virtual void Shutdown(Object? sender, Boolean exit)
        {
            Environment.Exit(Environment.ExitCode);
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