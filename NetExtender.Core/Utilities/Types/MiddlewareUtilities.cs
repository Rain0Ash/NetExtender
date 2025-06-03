// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Middlewares;
using NetExtender.Types.Middlewares.Interfaces;
using NetExtender.Types.Monads;
using NetExtender.Types.Monads.Interfaces;

namespace NetExtender.Utilities.Types
{
    public interface IAssemblyMiddlewareManager : IMiddlewareManager, IScannable
    {
    }
    
    public static partial class MiddlewareUtilities
    {
        private static readonly IResettableLazy<IAssemblyMiddlewareManager> manager = new ResettableLazy<IAssemblyMiddlewareManager>(Create);
        public static IAssemblyMiddlewareManager Manager
        {
            get
            {
                return manager.Value;
            }
        }

        static MiddlewareUtilities()
        {
            AppDomain.CurrentDomain.AssemblyLoad += OnAssemblyLoad;
        }
        
        private static void OnAssemblyLoad(Object? sender, AssemblyLoadEventArgs args)
        {
            if (MiddlewareUtilities.manager.IsValueCreated && MiddlewareUtilities.manager.Value is { IsScan: true } manager)
            {
                manager.Scan(args.LoadedAssembly);
            }
        }
        
        private static IAssemblyMiddlewareManager Create()
        {
            return (IAssemblyMiddlewareManager) new MiddlewareManager().Scan();
        }
        
        public static void Reset<T>() where T : class, IAssemblyMiddlewareManager, new()
        {
            if (!manager.IsValueCreated)
            {
                manager.Reset(static () => new T());
                return;
            }
            
            manager.Reset(() =>
            {
                T @new = new T();
                @new.From(manager.Value);
                return @new;
            });
        }
        
        private class MiddlewareManager : NetExtender.Types.Middlewares.MiddlewareManager, IAssemblyMiddlewareManager
        {
            public Boolean IsScan
            {
                get
                {
                    return true;
                }
            }
            
            public MiddlewareManager()
            {
            }
            
            public MiddlewareManager(MiddlewareManagerOptions? options)
                : base(options)
            {
            }
        }
    }
}