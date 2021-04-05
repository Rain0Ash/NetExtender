// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using JetBrains.Annotations;
using NetExtender.Apps.Data;
using NetExtender.Apps.Data.Common;
using NetExtender.Apps.Data.Interfaces;
using NetExtender.Apps.Domains.Interfaces;
using NetExtender.Exceptions;
using NetExtender.GUI;
using NetExtender.GUI.Common.Interfaces;
using NetExtender.Utils.Application;
using NetExtender.Utils.Types;
using WPFApp = System.Windows.Application;

namespace NetExtender.Apps.Domains
{
    public static partial class Domain
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        private static class CurrentDomain
        {
            private static IDomain current;
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

        public static IDomain Create([NotNull] IDomain domain)
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
            return Create(new AppData(name, AppVersion.Default));
        }

        public static IDomain Create(IAppData data)
        {
            CurrentDomain.ThrowIfAlreadyInitialized();
            return Create(new InternalDomain(data));
        }

        public static IDomain Create<TApp>(String name, GUIType type) where TApp : Application, new()
        {
            CurrentDomain.ThrowIfAlreadyInitialized();
            return Create<TApp>(new AppData(name, AppVersion.Default), type);
        }

        public static IDomain Create<TApp>(IAppData data, GUIType type) where TApp : Application, new()
        {
            CurrentDomain.ThrowIfAlreadyInitialized();
            return Create(new TApp(), data, type);
        }

        public static IDomain Create<TApp>(String name, TApp app, GUIType type) where TApp : Application, new()
        {
            CurrentDomain.ThrowIfAlreadyInitialized();
            return Create(app, new AppData(name, AppVersion.Default), type);
        }

        public static IDomain Create<TApp>(TApp app, IAppData data, GUIType type) where TApp : Application, new()
        {
            CurrentDomain.ThrowIfAlreadyInitialized();
            return Create(data).Initialize(app, type);
        }

        public static String FriendlyName
        {
            get
            {
                return ApplicationUtils.FriendlyName;
            }
        }
        
        public static String Directory
        {
            get
            {
                return ApplicationUtils.Directory;
            }
        }

        public static String Path
        {
            get
            {
                return ApplicationUtils.Path;
            }
        }

        public static DateTime? BuildDateTime
        {
            get
            {
                return ApplicationUtils.BuildDateTime;
            }
        }

        public static DateTime StartedAt
        {
            get
            {
                return Current.StartedAt;
            }
        }

        public static ShutdownMode ShutdownMode
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

        public static String AppNameOrPath
        {
            get
            {
                return IsInitialized ? AppName : Path;
            }
        }

        public static String AppNameOrFriendlyName
        {
            get
            {
                return IsInitialized ? AppName : FriendlyName;
            }
        }
        
        public static String AppShortNameOrPath
        {
            get
            {
                return IsInitialized ? AppShortName : Path;
            }
        }

        public static String AppShortNameOrFriendlyName
        {
            get
            {
                return IsInitialized ? AppShortName : FriendlyName;
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

        public static AppInformation Information
        {
            get
            {
                return Current.Information;
            }
        }
        
        public static String UrlSchemeProtocolName
        {
            get
            {
                return Current.UrlSchemeProtocolName;
            }
        }

        public static Boolean? IsUrlSchemeProtocolRegister
        {
            get
            {
                return Current.IsUrlSchemeProtocolRegister;
            }
            set
            {
                Current.IsUrlSchemeProtocolRegister = value;
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

        public static Task SendMessageAsync(Byte[] message)
        {
            return Current.SendMessageAsync(message);
        }

        public static Task SendMessageAsync(IEnumerable<Byte[]> message)
        {
            return Current.SendMessageAsync(message);
        }
    }
}