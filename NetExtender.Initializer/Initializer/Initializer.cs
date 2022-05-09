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
        
        protected Initializer()
        {
            AppDomain.CurrentDomain.ProcessExit += Shutdown;
            AppDomain.CurrentDomain.UnhandledException += UnhandledException;
        }
        
        private void UnhandledException(Object sender, UnhandledExceptionEventArgs args)
        {
            UnhandledException(sender, args.ExceptionObject as Exception, args.IsTerminating);
        }
        
        protected virtual void UnhandledException(Object? sender, Exception? exception, Boolean terminating)
        {
        }

        protected virtual void Shutdown(Object? sender, EventArgs args)
        {
        }

        protected internal virtual void InitializeNetExtender(INetExtenderFrameworkInitializer initializer)
        {
        }
    }
}