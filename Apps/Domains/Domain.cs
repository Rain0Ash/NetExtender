// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
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
using NetExtender.Utils.IO;
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
            return Create(new IPCAppData(name, AppVersion.Default));
        }

        public static IDomain Create(IIPCAppData data)
        {
            CurrentDomain.ThrowIfAlreadyInitialized();
            return Create(new InternalDomain(data));
        }

        public static IDomain Create<TApp>(String name, GUIType type) where TApp : Application, new()
        {
            return Create<TApp>(new IPCAppData(name, AppVersion.Default), type);
        }

        public static IDomain Create<TApp>(IIPCAppData data, GUIType type) where TApp : Application, new()
        {
            return Create(new TApp(), data, type);
        }

        public static IDomain Create<TApp>(String name, TApp app, GUIType type) where TApp : Application, new()
        {
            return Create(app, new IPCAppData(name, AppVersion.Default), type);
        }

        public static IDomain Create<TApp>(TApp app, IIPCAppData data, GUIType type) where TApp : Application, new()
        {
            CurrentDomain.ThrowIfAlreadyInitialized();
            return Create(data).Initialize(app, type);
        }

        public static String FriendlyName { get; } = PathUtils.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName);

        private static String path;
        public static String Path
        {
            get
            {
                if (path is not null)
                {
                    return path;
                }

                String name = Process.GetCurrentProcess().MainModule?.FileName;

                String dir = Directory;

                if (dir is null)
                {
                    return null;
                }

                String file = System.IO.Path.Combine(dir, FriendlyName + ".exe");

                return path ??= PathUtils.IsExistAsFile(file) ? file : name;
            }
        }

        private static String directory;
        public static String Directory
        {
            get
            {
                if (directory is not null)
                {
                    return directory;
                }

                String loc = Assembly.GetEntryAssembly()?.Location;
                if (String.IsNullOrWhiteSpace(loc))
                {
                    return null;
                }

                String dir = PathUtils.GetDirectoryName(loc);

                if (String.IsNullOrEmpty(dir) || !PathUtils.IsExistAsFolder(dir))
                {
                    return null;
                }

                return directory ??= dir;
            }
        }

        public static DateTime BuildDateTime
        {
            get
            {
                return File.GetLastWriteTime(Path);
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

        public static void Run()
        {
            Current.Run();
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