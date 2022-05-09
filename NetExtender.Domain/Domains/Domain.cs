// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Domains.Applications.Interfaces;
using NetExtender.Domains.Interfaces;
using NetExtender.Domains.View.Interfaces;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.Application;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Types;

namespace NetExtender.Domains
{
    [SuppressMessage("ReSharper", "AsyncConverter.AsyncMethodNamingHighlighting")]
    public static partial class Domain
    {
        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        private static class CurrentDomain
        {
            private static IDomain? current;
            public static IDomain Current
            {
                get
                {
                    return current ?? throw new NotInitializedException("Domain is not initialized", nameof(Current));
                }
                set
                {
                    ThrowIfAlreadyInitialized();
                    current = value ?? throw new ArgumentNullException(nameof(value));
                }
            }

            public static Boolean Initialized
            {
                get
                {
                    return current is not null;
                }
            }

            public static void ThrowIfAlreadyInitialized()
            {
                if (Initialized)
                {
                    throw new AlreadyInitializedException("Domain already initialized", nameof(Current));
                }
            }
        }

        public static Boolean IsInitialized
        {
            get
            {
                return CurrentDomain.Initialized;
            }
        }

        public static IDomain Current
        {
            get
            {
                return CurrentDomain.Current;
            }
        }

        public static IDomain Create(IDomain domain)
        {
            if (domain is null)
            {
                throw new ArgumentNullException(nameof(domain));
            }
            
            CurrentDomain.ThrowIfAlreadyInitialized();
            CurrentDomain.Current = domain;
            return Current;
        }
        
        public static IDomain Create(String name)
        {
            return Create(new ApplicationData(name, ApplicationVersion.Default));
        }
        
        public static IDomain Create(String name, String identifier)
        {
            return Create(new ApplicationData(name, identifier, ApplicationVersion.Default));
        }
        
        public static IDomain Create(IApplicationData data)
        {
            return Create(new InternalDomain(data));
        }
        
        public static IDomain Create(String name, IApplication application)
        {
            return Create(name).Initialize(application);
        }
        
        public static IDomain Create(String name, String identifier, IApplication application)
        {
            return Create(name, identifier).Initialize(application);
        }
        
        public static IDomain Create(IApplicationData data, IApplication application)
        {
            return Create(data).Initialize(application);
        }
        
        public static IDomain Create(String name, IApplication application, IApplicationView view)
        {
            return Create(name, application).View(view);
        }
        
        public static IDomain Create(String name, String identifier, IApplication application, IApplicationView view)
        {
            return Create(name, identifier, application).View(view);
        }
        
        public static IDomain Create(IApplicationData data, IApplication application, IApplicationView view)
        {
            return Create(data, application).View(view);
        }
        
        public static Task<IDomain> CreateAsync(String name)
        {
            return Task.FromResult(Create(new ApplicationData(name, ApplicationVersion.Default)));
        }
        
        public static Task<IDomain> CreateAsync(String name, CancellationToken token)
        {
            return Task.FromResult(Create(new ApplicationData(name, ApplicationVersion.Default)));
        }
        
        public static Task<IDomain> CreateAsync(String name, String identifier)
        {
            return Task.FromResult(Create(new ApplicationData(name, identifier, ApplicationVersion.Default)));
        }
        
        public static Task<IDomain> CreateAsync(String name, String identifier, CancellationToken token)
        {
            return Task.FromResult(Create(new ApplicationData(name, identifier, ApplicationVersion.Default)));
        }

        public static Task<IDomain> CreateAsync(IApplicationData data)
        {
            return Task.FromResult(Create(new InternalDomain(data)));
        }

        public static Task<IDomain> CreateAsync(IApplicationData data, CancellationToken token)
        {
            return Task.FromResult(Create(new InternalDomain(data)));
        }
        
        public static Task<IDomain> CreateAsync(String name, IApplication application)
        {
            return Task.FromResult(Create(name).Initialize(application));
        }
        
        public static Task<IDomain> CreateAsync(String name, IApplication application, CancellationToken token)
        {
            return Task.FromResult(Create(name).Initialize(application));
        }
        
        public static Task<IDomain> CreateAsync(String name, String identifier, IApplication application)
        {
            return Task.FromResult(Create(name, identifier).Initialize(application));
        }
        
        public static Task<IDomain> CreateAsync(String name, String identifier, IApplication application, CancellationToken token)
        {
            return Task.FromResult(Create(name, identifier).Initialize(application));
        }
        
        public static Task<IDomain> CreateAsync(IApplicationData data, IApplication application)
        {
            return Task.FromResult(Create(data).Initialize(application));
        }
        
        public static Task<IDomain> CreateAsync(IApplicationData data, IApplication application, CancellationToken token)
        {
            return Task.FromResult(Create(data).Initialize(application));
        }
        
        public static Task<IDomain> CreateAsync(String name, IApplication application, IApplicationView view)
        {
            return Create(name, application).ViewAsync(view);
        }
        
        public static Task<IDomain> CreateAsync(String name, IApplication application, IApplicationView view, CancellationToken token)
        {
            return Create(name, application).ViewAsync(view, token);
        }
        
        public static Task<IDomain> CreateAsync(String name, String identifier, IApplication application, IApplicationView view)
        {
            return Create(name, identifier, application).ViewAsync(view);
        }
        
        public static Task<IDomain> CreateAsync(String name, String identifier, IApplication application, IApplicationView view, CancellationToken token)
        {
            return Create(name, identifier, application).ViewAsync(view, token);
        }
        
        public static Task<IDomain> CreateAsync(IApplicationData data, IApplication application, IApplicationView view)
        {
            return Create(data, application).ViewAsync(view);
        }
        
        public static Task<IDomain> CreateAsync(IApplicationData data, IApplication application, IApplicationView view, CancellationToken token)
        {
            return Create(data, application).ViewAsync(view, token);
        }

        public static DateTime StartedAt
        {
            get
            {
                return Current.StartedAt;
            }
        }

        public static Boolean? Elevate
        {
            get
            {
                return Current.Elevate;
            }
        }
        
        public static Boolean? IsElevate
        {
            get
            {
                return Current.IsElevate;
            }
        }

        public static ApplicationShutdownMode ShutdownMode
        {
            get
            {
                return Current.ShutdownMode;
            }
            set
            {
                Current.ShutdownMode = value;
            }
        }

        public static Guid Guid
        {
            get
            {
                return Current.Guid;
            }
        }

        public static String ApplicationName
        {
            get
            {
                return Current.ApplicationName;
            }
        }
        
        public static String ApplicationIdentifier
        {
            get
            {
                return Current.ApplicationIdentifier;
            }
        }

        public static String? ApplicationNameOrPath
        {
            get
            {
                return IsInitialized ? ApplicationName : ApplicationUtilities.Path;
            }
        }

        public static String? ApplicationNameOrFriendlyName
        {
            get
            {
                return IsInitialized ? ApplicationName : ApplicationUtilities.FriendlyName;
            }
        }
        
        public static String? ApplicationIdentifierOrPath
        {
            get
            {
                return IsInitialized ? ApplicationIdentifier : ApplicationUtilities.Path;
            }
        }

        public static String? ApplicationIdentifierOrFriendlyName
        {
            get
            {
                return IsInitialized ? ApplicationIdentifier : ApplicationUtilities.FriendlyName;
            }
        }

        public static CultureInfo Culture
        {
            get
            {
                return IsInitialized ? Current.Culture : CultureUtilities.System;
            }
            set
            {
                Current.Culture = value;
            }
        }

        public static ApplicationInfo Information
        {
            get
            {
                return Current.Information;
            }
        }

        public static Boolean AssemblyLoadCallStaticContructor
        {
            get
            {
                return ReflectionUtilities.AssemblyLoadCallStaticContructor;
            }
        }

        public static Assembly[] Assemblies
        {
            get
            {
                return AppDomain.CurrentDomain.GetAssemblies();
            }
        }

        public static IDomain Run()
        {
            return Current.Run();
        }
        
        public static Task<IDomain> RunAsync()
        {
            return Current.RunAsync();
        }
            
        public static Task<IDomain> RunAsync(CancellationToken token)
        {
            return Current.RunAsync(token);
        }

        public static void Shutdown()
        {
            Current.Shutdown();
        }
        
        public static void Shutdown(Int32 code)
        {
            Current.Shutdown(code);
        }

        public static void Shutdown(Boolean force)
        {
            Current.Shutdown(force);
        }

        public static void Shutdown(Int32 code, Boolean force)
        {
            Current.Shutdown(code, force);
        }

        public static Task<Boolean> ShutdownAsync()
        {
            return Current.ShutdownAsync();
        }

        public static Task<Boolean> ShutdownAsync(CancellationToken token)
        {
            return Current.ShutdownAsync(token);
        }

        public static Task<Boolean> ShutdownAsync(Int32 code)
        {
            return Current.ShutdownAsync(code);
        }

        public static Task<Boolean> ShutdownAsync(Int32 code, CancellationToken token)
        {
            return Current.ShutdownAsync(code, token);
        }

        public static Task<Boolean> ShutdownAsync(Int32 code, Int32 milli)
        {
            return Current.ShutdownAsync(code, milli);
        }

        public static Task<Boolean> ShutdownAsync(Int32 code, Int32 milli, CancellationToken token)
        {
            return Current.ShutdownAsync(code, milli, token);
        }

        public static Task<Boolean> ShutdownAsync(Int32 code, Int32 milli, Boolean force)
        {
            return Current.ShutdownAsync(code, milli, force);
        }

        public static Task<Boolean> ShutdownAsync(Int32 code, Int32 milli, Boolean force, CancellationToken token)
        {
            return Current.ShutdownAsync(code, milli, force, token);
        }

        public static void Restart()
        {
            Current.Restart();
        }
        
        public static Task<Boolean> RestartAsync()
        {
            return Current.RestartAsync();
        }

        public static Task<Boolean> RestartAsync(Int32 milli)
        {
            return Current.RestartAsync(milli);
        }

        public static Task<Boolean> RestartAsync(CancellationToken token)
        {
            return Current.RestartAsync(token);
        }

        public static Task<Boolean> RestartAsync(Int32 milli, CancellationToken token)
        {
            return Current.RestartAsync(milli, token);
        }
    }
}