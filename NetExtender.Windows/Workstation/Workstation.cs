// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32;
using NetExtender.Utils.Windows;

namespace NetExtender.Workstation
{
    public static class WorkStation
    {
        static WorkStation()
        {
            SystemEvents.SessionSwitch += OnSessionSwitch;
        }

        public static OSData Data { get; } = Software.GetOSVersion();
        
        public static String? CurrentUserSID { get; } = GetCurrentUserSID();

        private static String? GetCurrentUserSID()
        {
            using WindowsIdentity user = WindowsIdentity.GetCurrent();
            return user?.User?.Value;
        }

        public static Boolean IsAdministrator
        {
            get
            {
                using WindowsIdentity identity = WindowsIdentity.GetCurrent();
                return identity.HasRole(WindowsBuiltInRole.Administrator);
            }
        }

        private static void OnSessionSwitch(Object sender, SessionSwitchEventArgs args)
        {
            switch (args.Reason)
            {
                case SessionSwitchReason.SessionLock:
                    islocked = true;
                    break;
                case SessionSwitchReason.SessionUnlock:
                    islocked = false;
                    break;
                default:
                    return;
            }
        }

        private static Boolean islocked = IsLocked;
        public static Boolean IsLocked
        {
            get
            {
                return islocked = Process.GetProcessesByName("logonui").Any();
            }
            set
            {
                if (value && islocked)
                {
                    return;
                }

                if (!value)
                {
                    if (islocked)
                    {
                        throw new NotSupportedException("Windows can't be unlocked programmatically.");
                    }
                    
                    return;
                }

                if (LockWorkStation())
                {
                    islocked = true;
                }
            }
        }
        
        [DllImport("user32.dll")]
        private static extern Boolean LockWorkStation();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static async Task<Boolean> ExecuteAsync(String execute, String arguments, String cancel, String cancelargs, Int32 seconds, CancellationToken token)
        {
            try
            {
                if (seconds < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(seconds));
                }
                
                Process? exit = Process.Start(execute, arguments);

                if (seconds <= 0 || !token.CanBeCanceled)
                {
                    return true;
                }

                try
                {
                    await Task.Delay(seconds * 1000, token).ConfigureAwait(false);
                }
                catch (Exception)
                {
                    // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                    if (exit is null)
                    {
                        return false;
                    }

                    exit.Kill(true);
                    Process.Start(cancel, cancelargs);
                    
                    return false;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Boolean Shutdown()
        {
            return ShutdownAsync().GetAwaiter().GetResult();
        }
        
        public static Task<Boolean> ShutdownAsync(Int32 seconds = 0)
        {
            return ShutdownAsync(seconds, CancellationToken.None);
        }
        
        public static Task<Boolean> ShutdownAsync(Int32 seconds, CancellationToken token)
        {
            return ExecuteAsync("shutdown", $"/s /t {seconds}", "shutdown", "/a", seconds, token);
        }
        
        public static Boolean Restart()
        {
            return RestartAsync().GetAwaiter().GetResult();
        }
        
        public static Task<Boolean> RestartAsync(Int32 seconds = 0)
        {
            return RestartAsync(seconds, CancellationToken.None);
        }
        
        public static Task<Boolean> RestartAsync(Int32 seconds, CancellationToken token)
        {
            return ExecuteAsync("shutdown", $"/r /t {seconds}", "shutdown", "/a", seconds, token);
        }
        
        public static Boolean Lock()
        {
            return LockAsync().GetAwaiter().GetResult();
        }
        
        public static Task<Boolean> LockAsync(Int32 milli = 0)
        {
            return LockAsync(milli, CancellationToken.None);
        }
        
        public static async Task<Boolean> LockAsync(Int32 milli, CancellationToken token)
        {
            if (milli < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(milli));
            }
            
            // ReSharper disable once InvertIf
            if (milli > 0)
            {
                try
                {
                    await Task.Delay(milli, token).ConfigureAwait(false);
                }
                catch (Exception)
                {
                    return false;
                }
            }

            Boolean succesfull = LockWorkStation();

            if (succesfull)
            {
                islocked = true;
            }

            return succesfull;
        }

        [DllImport("user32.dll")]
        private static extern Boolean ExitWindowsEx(UInt32 uFlags, UInt32 dwReason);

        public static Boolean Logoff()
        {
            return LogoffAsync().GetAwaiter().GetResult();
        }
        
        public static Task<Boolean> LogoffAsync(Int32 milli = 0)
        {
            return LogoffAsync(milli, CancellationToken.None);
        }
        
        public static async Task<Boolean> LogoffAsync(Int32 milli, CancellationToken token)
        {
            if (milli < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(milli));
            }
            
            // ReSharper disable once InvertIf
            if (milli > 0)
            {
                try
                {
                    await Task.Delay(milli, token).ConfigureAwait(false);
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return ExitWindowsEx(0, 0);
        }
        
        [DllImport("PowrProf.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern Boolean SetSuspendState(Boolean hibernate, Boolean forceCritical, Boolean disableWakeEvent);
        
        public static Boolean Sleep()
        {
            return Sleep(false);
        }
        
        public static Boolean Sleep(Boolean wake)
        {
            return SleepAsync().GetAwaiter().GetResult();
        }
        
        public static Task<Boolean> SleepAsync()
        {
            return SleepAsync(0);
        }
        
        public static Task<Boolean> SleepAsync(Int32 milli)
        {
            return SleepAsync(milli, CancellationToken.None);
        }
        
        public static Task<Boolean> SleepAsync(Int32 milli, CancellationToken token)
        {
            return SleepAsync(milli, false, token);
        }
        
        public static Task<Boolean> SleepAsync(Int32 milli, Boolean wake)
        {
            return SleepAsync(milli, wake, CancellationToken.None);
        }
        
        public static async Task<Boolean> SleepAsync(Int32 milli, Boolean wake, CancellationToken token)
        {
            if (milli < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(milli));
            }
            
            // ReSharper disable once InvertIf
            if (milli > 0)
            {
                try
                {
                    await Task.Delay(milli, token).ConfigureAwait(false);
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return SetSuspendState(false, true, !wake);
        }
        
        public static Boolean Hibernate()
        {
            return Hibernate(false);
        }
        
        public static Boolean Hibernate(Boolean wake)
        {
            return HibernateAsync(0, wake).GetAwaiter().GetResult();
        }
        
        public static Task<Boolean> HibernateAsync()
        {
            return HibernateAsync(0);
        }
        
        public static Task<Boolean> HibernateAsync(Int32 milli)
        {
            return HibernateAsync(milli, CancellationToken.None);
        }

        public static Task<Boolean> HibernateAsync(Int32 milli, Boolean wake)
        {
            return HibernateAsync(milli, wake, CancellationToken.None);
        }

        public static Task<Boolean> HibernateAsync(Int32 milli, CancellationToken token)
        {
            return HibernateAsync(milli, false, token);
        }

        public static async Task<Boolean> HibernateAsync(Int32 milli, Boolean wake, CancellationToken token)
        {
            if (milli < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(milli));
            }
            
            // ReSharper disable once InvertIf
            if (milli > 0)
            {
                try
                {
                    await Task.Delay(milli, token).ConfigureAwait(false);
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return SetSuspendState(true, true, !wake);
        }
    }
}