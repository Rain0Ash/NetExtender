// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Utils.IO;

namespace NetExtender.Utils.Types
{
    /// <summary>
    /// Contains information about process after it has exited.
    /// </summary>
    public sealed class ProcessResult : IDisposable
    {
        public Process Process { get; }
        public Int32 ExitCode { get; }
        public TimeSpan RunTime { get; }
        public String[] StandardOutput { get; }
        public String[] StandardError { get; }

        public ProcessResult(Process process, DateTime time, String[] output, String[] error)
        {
            Process = process;
            ExitCode = process.ExitCode;
            RunTime = process.ExitTime - time;
            StandardOutput = output;
            StandardError = error;
        }

        public void Dispose()
        {
            Process.Dispose();
        }
    }

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

        public static Boolean TryKill(this Process process)
        {
            if (process is null)
            {
                throw new ArgumentNullException(nameof(process));
            }

            try
            {
                process.Kill();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Process StartProcess(String path)
        {
            return Process.Start(path);
        }

        public static Task<Process?> StartProcessAsync(String path, Int32 milliseconds)
        {
            return StartProcessAsync(path, milliseconds, CancellationToken.None);
        }

        public static Task<Process?> StartProcessAsync(String path, TimeSpan wait)
        {
            return StartProcessAsync(path, wait, CancellationToken.None);
        }

        public static Task<Process?> StartProcessAsync(String path, Int32 milliseconds, CancellationToken token)
        {
            return StartProcessAsync(path, TimeSpan.FromMilliseconds(milliseconds), token);
        }

        //TODO: refactoring
        public static async Task<Process?> StartProcessAsync(String path, TimeSpan wait, CancellationToken token)
        {
            if (!PathUtils.IsExistAsFile(path))
            {
                throw new ArgumentException(@"File not exist", nameof(path));
            }

            ProcessStartInfo info = new ProcessStartInfo
            {
                Arguments = $"/C {(wait.Ticks > 0 ? $"ping 127.0.0.1 -n {((Int64) wait.TotalMilliseconds + 1000) / 1000} > nul && " : String.Empty)}\"{path}\"",
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                FileName = "cmd.exe"
            };

            Process? process;

            try
            {
                process = Process.Start(info);
            }
            catch (Exception)
            {
                return null;
            }

            if (process is null)
            {
                return process;
            }

            await using CancellationTokenRegistration registration = token.Register(() =>
            {
                try
                {
                    if (process.HasExited)
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

        public static Task<Boolean> WaitForExitAsync(this Process process, Int32 milliseconds)
        {
            if (process is null)
            {
                throw new ArgumentNullException(nameof(process));
            }

            return Task.Run(() => process.WaitForExit(milliseconds));
        }

        public static Task<Boolean> WaitForExitAsync(this Process process, TimeSpan timeout)
        {
            return WaitForExitAsync(process, (Int32) timeout.TotalMilliseconds);
        }

        public static Task<Int32> WaitProcessExitAsync(this Process process)
        {
            return WaitProcessExitAsync(process, CancellationToken.None);
        }

        public static Task<Int32> WaitProcessExitAsync(this Process process, CancellationToken token)
        {
            if (process is null)
            {
                throw new ArgumentNullException(nameof(process));
            }

            TaskCompletionSource<Int32> source = new TaskCompletionSource<Int32>(token);

            process.EnableRaisingEvents = true;

            void Exit(Object? sender, EventArgs args)
            {
                source.SetResult(process.ExitCode);
                process.Exited -= Exit;
            }

            process.Exited += Exit;

            return source.Task;
        }

        /// <summary>
        /// Runs asynchronous process.
        /// </summary>
        /// <param name="filename">An application or document which starts the process.</param>
        public static Task<ProcessResult> RunAsync(String filename)
        {
            return RunAsync(new ProcessStartInfo(filename));
        }

        /// <summary>
        /// Runs asynchronous process.
        /// </summary>
        /// <param name="filename">An application or document which starts the process.</param>
        /// <param name="token">The token to monitor for cancellation requests.</param>
        public static Task<ProcessResult> RunAsync(String filename, CancellationToken token)
        {
            return RunAsync(new ProcessStartInfo(filename), token);
        }

        /// <summary>
        /// Runs asynchronous process.
        /// </summary>
        /// <param name="filename">An application or document which starts the process.</param>
        /// <param name="arguments">Command-line arguments to pass to the application when the process starts.</param>
        public static Task<ProcessResult> RunAsync(String filename, String arguments)
        {
            return RunAsync(new ProcessStartInfo(filename, arguments));
        }

        /// <summary>
        /// Runs asynchronous process.
        /// </summary>
        /// <param name="filename">An application or document which starts the process.</param>
        /// <param name="arguments">Command-line arguments to pass to the application when the process starts.</param>
        /// <param name="token">The token to monitor for cancellation requests.</param>
        public static Task<ProcessResult> RunAsync(String filename, String arguments, CancellationToken token)
        {
            return RunAsync(new ProcessStartInfo(filename, arguments), token);
        }

        /// <summary>
        /// Runs asynchronous process.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Diagnostics.ProcessStartInfo" /> that contains the information that is used to start the process, including the file name and any command-line arguments.</param>
        public static Task<ProcessResult> RunAsync(this ProcessStartInfo info)
        {
            return RunAsync(info, CancellationToken.None);
        }

        /// <summary>
        /// Runs asynchronous process.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Diagnostics.ProcessStartInfo" /> that contains the information that is used to start the process, including the file name and any command-line arguments.</param>
        /// <param name="token">The token to monitor for cancellation requests.</param>
        public static async Task<ProcessResult> RunAsync(this ProcessStartInfo info, CancellationToken token)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            info.UseShellExecute = false;
            info.RedirectStandardOutput = true;
            info.RedirectStandardError = true;

            TaskCompletionSource<ProcessResult> source = new TaskCompletionSource<ProcessResult>();

            Process process = new Process
            {
                StartInfo = info,
                EnableRaisingEvents = true
            };

            Task<String[]> output = process.GetProcessOutputAsync();
            Task<String[]> error = process.GetProcessErrorAsync();

            TaskCompletionSource<DateTime> start = new TaskCompletionSource<DateTime>();

            process.Exited += async (_, _) =>
            {
                source.TrySetResult(
                    new ProcessResult(process,
                        await start.Task.ConfigureAwait(false),
                        await output.ConfigureAwait(false),
                        await error.ConfigureAwait(false))
                );
            };

            await using (token.Register(() =>
            {
                source.TrySetCanceled();

                if (!process.HasExited)
                {
                    process.TryKill();
                }
            }))
            {
                token.ThrowIfCancellationRequested();

                DateTime time = DateTime.Now;
                if (!process.Start())
                {
                    source.TrySetException(new InvalidOperationException("Failed to start process"));
                    return await source.Task.ConfigureAwait(false);
                }

                try
                {
                    time = process.StartTime;
                }
                catch (Exception)
                {
                    // ignored
                }

                start.SetResult(time);

                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                return await source.Task.ConfigureAwait(false);
            }
        }

        public static Task<String[]> GetProcessOutputAsync(this Process process)
        {
            if (process is null)
            {
                throw new ArgumentNullException(nameof(process));
            }

            if (!process.StartInfo.RedirectStandardOutput)
            {
                throw new ArgumentException("Process must redirect standart output");
            }

            List<String> output = new List<String>(32);
            TaskCompletionSource<String[]> container = new TaskCompletionSource<String[]>();

            void Handler(Object? sender, DataReceivedEventArgs args)
            {
                if (args.Data is not null)
                {
                    output.Add(args.Data);
                    return;
                }

                process.OutputDataReceived -= Handler;
                container.SetResult(output.ToArray());
            }

            process.OutputDataReceived += Handler;

            return container.Task;
        }

        public static async Task<String[]?> TryGetProcessOutputAsync(this Process process)
        {
            if (process is null)
            {
                throw new ArgumentNullException(nameof(process));
            }

            if (!process.StartInfo.RedirectStandardOutput)
            {
                return null;
            }

            try
            {
                return await GetProcessOutputAsync(process)!;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Task<String[]> GetProcessErrorAsync(this Process process)
        {
            if (process is null)
            {
                throw new ArgumentNullException(nameof(process));
            }

            if (!process.StartInfo.RedirectStandardError)
            {
                throw new ArgumentException("Process must redirect standart output");
            }

            List<String> error = new List<String>(16);
            TaskCompletionSource<String[]> container = new TaskCompletionSource<String[]>();

            void Handler(Object? sender, DataReceivedEventArgs args)
            {
                if (args.Data is not null)
                {
                    error.Add(args.Data);
                    return;
                }

                process.OutputDataReceived -= Handler;
                container.SetResult(error.ToArray());
            }

            process.ErrorDataReceived += Handler;

            return container.Task;
        }

        public static async Task<String[]?> TryGetProcessErrorAsync(this Process process)
        {
            if (process is null)
            {
                throw new ArgumentNullException(nameof(process));
            }

            if (!process.StartInfo.RedirectStandardError)
            {
                return null;
            }

            try
            {
                return await GetProcessErrorAsync(process)!;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}