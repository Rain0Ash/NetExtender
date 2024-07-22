using System;
using System.Threading;
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
        private static readonly IResettableLazy<IAssemblyMiddlewareManager> _manager = new ResettableLazy<IAssemblyMiddlewareManager>(Create);
        public static IAssemblyMiddlewareManager Manager
        {
            get
            {
                return _manager.Value;
            }
        }

        static MiddlewareUtilities()
        {
            AppDomain.CurrentDomain.AssemblyLoad += OnAssemblyLoad;
        }
        
        private static void OnAssemblyLoad(Object? sender, AssemblyLoadEventArgs args)
        {
            if (_manager.IsValueCreated && _manager.Value is { IsScan: true } manager)
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
            if (!_manager.IsValueCreated)
            {
                _manager.Reset(static () => new T());
                return;
            }
            
            _manager.Reset(() =>
            {
                T @new = new T();
                @new.From(_manager.Value);
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