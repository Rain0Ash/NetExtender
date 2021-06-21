// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using NetExtender.Utils.Numerics;
using NetExtender.Utils.IO;

namespace NetExtender.Utils.Types
{
    public static class ProcessUtils
    {
        public static void OpenBrowser(String url)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Process.Start(new ProcessStartInfo(url) {UseShellExecute = true});
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.Start("xdg-open", url);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Process.Start("open", url);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public static Process? StartProcess(String path, Int32 milli = 0)
        {
            return StartProcess(path, milli, CancellationToken.None);
        }
        
        public static Process? StartProcess(String path, Int32 milli, CancellationToken token)
        {
            if (!PathUtils.IsExistAsFile(path))
            {
                throw new ArgumentException(@"File not exist", nameof(path));
            }

            milli = milli.ToRange();
            
            ProcessStartInfo info = new ProcessStartInfo
            {
                Arguments = $"/C {(milli > 0 ? $"ping 127.0.0.1 -n {(milli + 1000) / 1000} > nul && " : String.Empty)}\"{path}\"",
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                FileName = "cmd.exe"
            };
            
            Process? process = Process.Start(info);
            using CancellationTokenRegistration registration = token.Register(() =>
            {
                try
                {
                    if (process is null || process.HasExited)
                    {
                        return;
                    }
                    
                    process.Kill(true);
                }
                catch (Exception)
                {
                    //ignored
                }
            });
            
            return process;
        }
    }
}