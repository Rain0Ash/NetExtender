// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Domains.Applications.Interfaces;
using NetExtender.Domains.Interfaces;
using NetExtender.Exceptions;
using NetExtender.UserInterface.Interfaces;
using NetExtender.Utils.Application;
using NetExtender.Utils.Types;

namespace NetExtender.Domains
{
    public static partial class Domain
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
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
            CurrentDomain.ThrowIfAlreadyInitialized();
            return Create(new ApplicationData(name, ApplicationVersion.Default));
        }

        public static IDomain Create(IApplicationData data)
        {
            CurrentDomain.ThrowIfAlreadyInitialized();
            return Create(new InternalDomain(data));
        }
        
        public static IDomain Create(String name, IApplication application)
        {
            CurrentDomain.ThrowIfAlreadyInitialized();
            return Create(new ApplicationData(name, ApplicationVersion.Default), application);
        }
        
        public static IDomain Create(IApplicationData data, IApplication application)
        {
            CurrentDomain.ThrowIfAlreadyInitialized();
            return Create(data).Initialize(application);
        }

        public static DateTime StartedAt
        {
            get
            {
                return Current.StartedAt;
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

        public static String AppName
        {
            get
            {
                return Current.AppName;
            }
        }
        
        public static String AppShortName
        {
            get
            {
                return Current.AppShortName;
            }
        }

        public static String? AppNameOrPath
        {
            get
            {
                return IsInitialized ? AppName : ApplicationUtils.Path;
            }
        }

        public static String? AppNameOrFriendlyName
        {
            get
            {
                return IsInitialized ? AppName : ApplicationUtils.FriendlyName;
            }
        }
        
        public static String? AppShortNameOrPath
        {
            get
            {
                return IsInitialized ? AppShortName : ApplicationUtils.Path;
            }
        }

        public static String? AppShortNameOrFriendlyName
        {
            get
            {
                return IsInitialized ? AppShortName : ApplicationUtils.FriendlyName;
            }
        }

        public static CultureInfo Culture
        {
            get
            {
                return IsInitialized ? Current.Culture : CultureUtils.System;
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

        public static void Run()
        {
            Current.Run();
        }
        
        public static void Run<T>(T window) where T : IWindow
        {
            Current.Run(window);
        }

        public static void Shutdown(Int32 code = 0)
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

        public static void Restart(Int32 milli)
        {
            Current.Restart(milli);
        }

        public static void Restart(CancellationToken token)
        {
            Current.Restart(token);
        }

        public static void Restart(Int32 milli, CancellationToken token)
        {
            Current.Restart(milli, token);
        }
    }
}