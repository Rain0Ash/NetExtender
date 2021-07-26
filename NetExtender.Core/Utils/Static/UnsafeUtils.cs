// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Utils.Types;

namespace NetExtender.Utils.Static
{
    public static class UnsafeUtils
    {
        public static class Segfault
        {
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            private delegate void UnmanagedCallback();
            
            private static UnmanagedCallback SegfaultDelegate { get; } = (UnmanagedCallback) Marshal.GetDelegateForFunctionPointer((IntPtr) 1, typeof(UnmanagedCallback));

            public static void Crash()
            {
                SegfaultDelegate?.Invoke();
            }

            public static Task CrashAsync(Int32 milliseconds)
            {
                return CrashAsync(milliseconds, CancellationToken.None);
            }
            
            public static Task CrashAsync(TimeSpan wait)
            {
                return CrashAsync(wait, CancellationToken.None);
            }

            public static Task CrashAsync(Int32 milliseconds, CancellationToken token)
            {
                return CrashAsync(TimeSpan.FromMilliseconds(milliseconds), token);
            }

            public static async Task CrashAsync(TimeSpan wait, CancellationToken token)
            {
                try
                {
                    await Task.Delay(wait, token).ContinueWith(Crash, token).ConfigureAwait(false);
                }
                catch (TaskCanceledException)
                {
                    //ignored
                }
            }
        }
    }
}