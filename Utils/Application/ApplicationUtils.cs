// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using NetExtender.Utils.Numerics;
using NetExtender.Apps.Domains;
using NetExtender.Utils.IO;
using NetExtender.Utils.OS;

namespace NetExtender.Utils.Application
{
    public static class ApplicationUtils
    {
        public static String? FriendlyName { get; }
        public static String? Path { get; }

        public static String? Directory { get; }

        public static DateTime? BuildDateTime { get; }

        private static String? GetFriendlyNameInternal()
        {
            try
            {
                return PathUtils.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static String? GetDirectoryInternal()
        {
            try
            {
                String location = Assembly.GetEntryAssembly()?.Location;
                if (String.IsNullOrWhiteSpace(location))
                {
                    return null;
                }

                String directory = PathUtils.GetDirectoryName(location);

                if (String.IsNullOrEmpty(directory) || !PathUtils.IsExistAsFolder(directory))
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

        private static DateTime? GetBuildDateTimeInternal()
        {
            return GetBuildDateTimeInternal(Path ?? GetPathInternal());
        }

        private static DateTime? GetBuildDateTimeInternal(String path)
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

        private static String? GetPathInternal()
        {
            return GetPathInternal(FriendlyName ?? GetFriendlyNameInternal(), Directory ?? GetDirectoryInternal());
        }

        private static String? GetPathInternal(String name, String directory)
        {
            try
            {
                if (name is null)
                {
                    return null;
                }
                
                if (directory is null)
                {
                    return name;
                }

                String file = System.IO.Path.Combine(directory, name + ".exe");

                return PathUtils.IsExistAsFile(file) ? file : name;
            }
            catch (Exception)
            {
                return null;
            }
        }

        static ApplicationUtils()
        {
            FriendlyName = GetFriendlyNameInternal();
            Directory = GetDirectoryInternal();
            
            if (FriendlyName is not null)
            {
                Path = GetPathInternal(FriendlyName, Directory);
            }

            if (Path is not null)
            {
                BuildDateTime = GetBuildDateTimeInternal(Path);
            }
        }
        
        public static void Shutdown(Int32 code = 0)
        {
            Environment.Exit(code);
        }

        public static void Shutdown(Action<Int32> shutdown, Int32 code = 0)
        {
            if (shutdown is null)
            {
                Shutdown(code);
                return;
            }

            shutdown.Invoke(code);
        }

        public static void Shutdown(Dispatcher dispatcher, Int32 code = 0)
        {
            Shutdown(dispatcher, null, code);
        }
        
        public static void Shutdown(Dispatcher dispatcher, Action<Int32> shutdown, Int32 code = 0)
        {
            if (dispatcher is null)
            {
                Shutdown(shutdown, code);
                return;
            }
            
            dispatcher.Invoke(() => Shutdown(shutdown, code));
        }


        public const Int32 DefaultMilliRestart = 1000;
        public static void Restart(Int32 milli = DefaultMilliRestart)
        {
            Restart(milli, null);
        }

        public static void Restart(CancellationToken token)
        {
            Restart(DefaultMilliRestart, token);
        }
        
        public static void Restart(Int32 milli, CancellationToken token)
        {
            Restart(milli, null, token);
        }

        public static void Restart(Action<Int32> shutdown)
        {
            Restart(DefaultMilliRestart, shutdown);
        }
        
        public static void Restart(Int32 milli, Action<Int32> shutdown)
        {
            Restart(milli, shutdown, CancellationToken.None);
        }

        public static void Restart(Action<Int32> shutdown, CancellationToken token)
        {
            Restart(DefaultMilliRestart, shutdown, token);
        }
        
        public static void Restart(Int32 milli, Action<Int32> shutdown, CancellationToken token)
        {
            Restart(milli, null, shutdown, token);
        }

        public static void Restart(Dispatcher dispatcher, Action<Int32> shutdown)
        {
            Restart(DefaultMilliRestart, dispatcher, shutdown);
        }

        public static void Restart(Int32 milli, Dispatcher dispatcher, Action<Int32> shutdown)
        {
            Restart(milli, dispatcher, shutdown, CancellationToken.None);
        }

        public static void Restart(Dispatcher dispatcher, Action<Int32> shutdown, CancellationToken token)
        {
            Restart(DefaultMilliRestart, dispatcher, shutdown, token);
        }
        
        public static async void Restart(Int32 milli, Dispatcher dispatcher, Action<Int32> shutdown, CancellationToken token)
        {
            const Int32 close = 1000;
            MathUtils.ToRange(ref milli, close);
            
            Process restart = ProcessUtils.StartProcess(Domain.Path, milli, token);

            try
            {
                Int32 wait = milli - close;
                if (wait > 0)
                {
                    await Task.Delay(wait, token).ConfigureAwait(false);
                }

                Shutdown(dispatcher, shutdown);
            }
            catch (TaskCanceledException)
            {
                restart.Kill(true);
            }
        }
    }
}