// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Types.Dispatchers.Interfaces;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.IO;
using NetExtender.Utilities.Threading;

namespace NetExtender.Utilities.Application
{
    [SuppressMessage("ReSharper", "AsyncConverter.AsyncMethodNamingHighlighting")]
    public static class ApplicationUtilities
    {
        static ApplicationUtilities()
        {
            FriendlyName = GetFriendlyName();
            Directory = GetDirectory();

            if (FriendlyName is not null)
            {
                Path = GetPath(FriendlyName, Directory);
            }

            if (Path is not null)
            {
                BuildTime = GetBuildDateTime(Path);
            }
        }
        
        public static ImmutableArray<String> Arguments
        {
            get
            {
                return Initializer.Initializer.Arguments ?? Environment.GetCommandLineArgs().ToImmutableArray();
            }
        }

        public static String? FriendlyName { get; }
        public static String? Path { get; }

        public static String? Directory { get; }

        public static DateTime? BuildTime { get; }

        public static Int32 ProcessId
        {
            get
            {
#if NET6_0_OR_GREATER
                return Environment.ProcessId;
#else
                return Process.GetCurrentProcess().Id;
#endif
            }
        }

        private static String? GetFriendlyName()
        {
            try
            {
                return PathUtilities.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static String? GetDirectory()
        {
            try
            {
                String? location = ReflectionUtilities.GetEntryAssembly()?.Location;
                if (String.IsNullOrWhiteSpace(location))
                {
                    return null;
                }

                String? directory = PathUtilities.GetDirectoryName(location);

                if (String.IsNullOrEmpty(directory) || !PathUtilities.IsExistAsFolder(directory))
                {
                    return null;
                }

                return directory;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static DateTime? GetBuildDateTime()
        {
            return GetBuildDateTime(Path ?? GetPath());
        }

        private static DateTime? GetBuildDateTime(String? path)
        {
            try
            {
                return path is not null ? File.GetLastWriteTime(path) : null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static String? GetPath()
        {
            return GetPath(FriendlyName ?? GetFriendlyName(), Directory ?? GetDirectory());
        }

        // ReSharper disable once UnusedParameter.Local
        private static String? GetPath(String? name, String? directory)
        {
            try
            {
#if NET6_0_OR_GREATER
                String? process = Environment.ProcessPath;

                if (process is not null)
                {
                    return process;
                }
#endif
                if (name is null)
                {
                    return null;
                }

                if (directory is null)
                {
                    return name;
                }

                String path = System.IO.Path.Combine(directory, name + ".exe");

                if (PathUtilities.IsExistAsFile(path))
                {
                    return path;
                }

                path = System.IO.Path.Combine(directory, name);

                return PathUtilities.IsExistAsFile(path) ? path : name;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static void Shutdown()
        {
            Shutdown(0);
        }

        public static void Shutdown(Int32 code)
        {
            Environment.Exit(code);
        }

        public static void Shutdown(Action<Int32>? shutdown)
        {
            Shutdown(shutdown, 0);
        }

        public static void Shutdown(Action<Int32>? shutdown, Int32 code)
        {
            if (shutdown is null)
            {
                Shutdown(code);
                return;
            }

            shutdown.Invoke(code);
        }

        public static void Shutdown(this IDispatcher? dispatcher)
        {
            Shutdown(dispatcher, 0);
        }

        public static void Shutdown(this IDispatcher? dispatcher, Int32 code)
        {
            Shutdown(dispatcher, null, code);
        }

        public static void Shutdown(this IDispatcher? dispatcher, Action<Int32>? shutdown)
        {
            Shutdown(dispatcher, shutdown, 0);
        }

        public static void Shutdown(this IDispatcher? dispatcher, Action<Int32>? shutdown, Int32 code)
        {
            if (dispatcher is null)
            {
                Shutdown(shutdown, code);
                return;
            }

            dispatcher.Invoke(() => Shutdown(shutdown, code));
        }

        public const Int32 DefaultMilliRestart = 1000;

        public static Task<Boolean> Restart()
        {
            return Restart(DefaultMilliRestart);
        }

        public static Task<Boolean> Restart(Int32 milliseconds)
        {
            return Restart(milliseconds, null);
        }

        public static Task<Boolean> Restart(TimeSpan wait)
        {
            return Restart(wait, null);
        }

        public static Task<Boolean> Restart(CancellationToken token)
        {
            return Restart(DefaultMilliRestart, token);
        }

        public static Task<Boolean> Restart(Int32 milliseconds, CancellationToken token)
        {
            return Restart(milliseconds, null, token);
        }

        public static Task<Boolean> Restart(TimeSpan wait, CancellationToken token)
        {
            return Restart(wait, null, token);
        }

        public static Task<Boolean> Restart(Action<Int32>? shutdown)
        {
            return Restart(DefaultMilliRestart, shutdown);
        }

        public static Task<Boolean> Restart(Int32 milliseconds, Action<Int32>? shutdown)
        {
            return Restart(milliseconds, shutdown, CancellationToken.None);
        }

        public static Task<Boolean> Restart(TimeSpan wait, Action<Int32>? shutdown)
        {
            return Restart(wait, shutdown, CancellationToken.None);
        }

        public static Task<Boolean> Restart(Action<Int32>? shutdown, CancellationToken token)
        {
            return Restart(DefaultMilliRestart, shutdown, token);
        }

        public static Task<Boolean> Restart(Int32 milliseconds, Action<Int32>? shutdown, CancellationToken token)
        {
            return Restart(milliseconds, null, shutdown, token);
        }

        public static Task<Boolean> Restart(TimeSpan wait, Action<Int32>? shutdown, CancellationToken token)
        {
            return Restart(wait, null, shutdown, token);
        }

        public static Task<Boolean> Restart(this IDispatcher? dispatcher, Action<Int32>? shutdown)
        {
            return Restart(DefaultMilliRestart, dispatcher, shutdown);
        }

        public static Task<Boolean> Restart(Int32 milliseconds, IDispatcher? dispatcher, Action<Int32>? shutdown)
        {
            return Restart(milliseconds, dispatcher, shutdown, CancellationToken.None);
        }

        public static Task<Boolean> Restart(TimeSpan wait, IDispatcher? dispatcher, Action<Int32>? shutdown)
        {
            return Restart(wait, dispatcher, shutdown, CancellationToken.None);
        }

        public static Task<Boolean> Restart(this IDispatcher? dispatcher, Action<Int32>? shutdown, CancellationToken token)
        {
            return Restart(DefaultMilliRestart, dispatcher, shutdown, token);
        }

        public static Task<Boolean> Restart(this IDispatcher? dispatcher, Int32 milliseconds, Action<Int32>? shutdown, CancellationToken token)
        {
            return Restart(milliseconds, dispatcher, shutdown, token);
        }

        public static Task<Boolean> Restart(this IDispatcher? dispatcher, TimeSpan wait, Action<Int32>? shutdown, CancellationToken token)
        {
            return Restart(wait, dispatcher, shutdown, token);
        }

        public static Task<Boolean> Restart(Int32 milliseconds, IDispatcher? dispatcher, Action<Int32>? shutdown, CancellationToken token)
        {
            return Restart(TimeSpan.FromMilliseconds(milliseconds), dispatcher, shutdown, token);
        }

        public static async Task<Boolean> Restart(TimeSpan wait, IDispatcher? dispatcher, Action<Int32>? shutdown, CancellationToken token)
        {
            String? path = Path;

            if (String.IsNullOrEmpty(path))
            {
                return false;
            }

            using Process? restart = await new ProcessStartInfo(path)
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true
            }.StartProcessAsync(wait, token).ConfigureAwait(false);

            if (restart is null)
            {
                return false;
            }

            if (token.IsCancellationRequested)
            {
                restart.TryKill(true);
                return false;
            }

            try
            {
                Shutdown(dispatcher, shutdown);
                return true;
            }
            catch (TaskCanceledException)
            {
                restart.TryKill(true);
                return false;
            }
        }
    }
}