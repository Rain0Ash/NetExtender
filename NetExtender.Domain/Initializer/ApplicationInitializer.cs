// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Utilities.Threading;

namespace NetExtender.Domains.Initializer
{
    public abstract class ApplicationInitializer : NetExtender.Initializer.Initializer
    {
        protected static ApplicationInitializer Instance { get; }
        
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

        protected virtual void Initialize()
        {
        }

        private Int32 Internal(String[]? args)
        {
            Initialize();
            Domain.AutoStart(args);
            return 0;
        }

        private async Task<Int32> InternalAsync(String[]? args, CancellationToken token)
        {
            Initialize();
            await Domain.AutoStartAsync(token);
            return 0;
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
    }
}