// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Initializer;
using NetExtender.Utilities.Application;
using NetExtender.Utilities.Threading;

namespace NetExtender.Domains.Initializer
{
    public abstract class ApplicationInitializer : NetExtender.Initializer.Initializer
    {
        private static ApplicationInitializer? instance;
        protected static ApplicationInitializer Instance
        {
            get
            {
                return instance ?? throw new InvalidOperationException();
            }
            set
            {
                if (instance is not null)
                {
                    throw new InvalidOperationException();
                }
                
                instance = value;
            }
        }

        static ApplicationInitializer()
        {
            Assembly? assembly = Assembly.GetEntryAssembly();
            
            if (assembly is null)
            {
                throw new EntryPointNotFoundException();
            }

            Type[] derived = assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(ApplicationInitializer))).ToArray();

            Instance = derived.Length switch
            {
                0 => throw new EntryPointNotFoundException(),
                1 => (ApplicationInitializer?) Activator.CreateInstance(derived[0]),
                _ => throw new AmbiguousMatchException()
            } ?? throw new InvalidOperationException();
        }

        protected ApplicationInitializer()
            : base(instance is not null)
        {
        }

        protected virtual void Initialize()
        {
        }

        private Int32 Internal(String[]? args)
        {
            try
            {
                Initialize();
                Domain.AutoStart(args);
                return 0;
            }
            catch (Exception exception)
            {
                UnhandledException(this, exception, InitializerUnhandledExceptionState.Terminate);
                return exception.HResult;
            }
        }

        private async Task<Int32> InternalAsync(String[]? args, CancellationToken token)
        {
            try
            {
                Initialize();
                await Domain.AutoStartAsync(token);
                return 0;
            }
            catch (Exception exception)
            {
                UnhandledException(this, exception, InitializerUnhandledExceptionState.Terminate);
                return exception.HResult;
            }
        }

        public Int32 Start(String[]? args)
        {
            return ThreadUtilities.STA(Internal, args);
        }

        public Task<Int32> StartAsync(String[]? args)
        {
            return StartAsync(args, CancellationToken.None);
        }

        public async Task<Int32> StartAsync(String[]? args, CancellationToken token)
        {
            return await ThreadUtilities.STA(InternalAsync, args, token);
        }
        
        protected override void Shutdown(Object? sender, Boolean exit)
        {
            if (exit)
            {
                return;
            }

            if (Domain.IsInitialized)
            {
                Domain.Shutdown();
                return;
            }

            ApplicationUtilities.Shutdown();
        }

        protected override void Terminate(Object? sender, Exception? exception)
        {
            Int32 code = exception?.HResult ?? 1;
            
            if (Domain.IsInitialized)
            {
                Domain.Shutdown(code, true);
                return;
            }
            
            ApplicationUtilities.Shutdown(code);
        }
    }
}