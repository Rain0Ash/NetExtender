// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace NetExtender.Protect.Debug
{
    public static class DebugHook
    {
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        private static extern Boolean CheckRemoteDebuggerPresent(IntPtr hProcess, ref Boolean isDebuggerPresent);

        public static event EmptyHandler DebugFinded;
        public static void StartDebugCheck(Int32 timeout = 1)
        {
            StartDebugCheckAsync(timeout, CancellationToken.None);
        }
        
        public static Task StartDebugCheckAsync(Int32 timeout, CancellationToken token)
        {
            IntPtr handle = Process.GetCurrentProcess().Handle;

            Task check = Task.CompletedTask;
            
            if (handle == IntPtr.Zero)
            {
                return check;
            }

            try
            {
                Boolean isDebuggerPresent = false;
                
                check = Task.Run(() =>
                {
                    while (!token.IsCancellationRequested)
                    {
                        CheckRemoteDebuggerPresent(handle, ref isDebuggerPresent);
                        if (isDebuggerPresent)
                        {
                            DebugFinded?.Invoke();
                            break;
                        }

                        Thread.Sleep(timeout);
                    }
                }, token);
            }
            catch (TaskCanceledException)
            {
                // ignored;
            }

            return check;
        }
    }
}