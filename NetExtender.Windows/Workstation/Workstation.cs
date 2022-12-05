// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Utilities.Windows;

namespace NetExtender.Workstation
{
    public static class WorkStation
    {
        public static OperationSystemInfo Data { get; } = Software.GetOperatingSystemInfo();

        public static String? CurrentUserSID { get; } = GetCurrentUserSID();

        private static String? GetCurrentUserSID()
        {
            using WindowsIdentity user = WindowsIdentity.GetCurrent();
            return user?.User?.Value;
        }

        public static Boolean? IsAdministrator
        {
            get
            {
                try
                {
                    using WindowsIdentity identity = WindowsIdentity.GetCurrent();
                    return identity.HasRole(WindowsBuiltInRole.Administrator);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public static Boolean IsLocked
        {
            get
            {
                return Process.GetProcessesByName("logonui").Any();
            }
            set
            {
                Boolean locked = IsLocked;

                switch (value)
                {
                    case true when locked:
                        return;
                    case false when locked:
                        throw new NotSupportedException("Windows can't be unlocked programmatically.");
                    case false:
                        return;
                    default:
                        LockWorkStation();
                        break;
                }
            }
        }

        [Flags]
        private enum ScreenSaverExecutionState : UInt32
        {
            SystemRequired = 0x00000001,
            DisplayRequired = 0x00000002,
            AwayModeRequired = 0x00000040,
            Continuous = 0x80000000
        }

        [DllImportAttribute("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern ScreenSaverExecutionState SetThreadExecutionState(ScreenSaverExecutionState state);

        public static Boolean ScreenSaverEnabled
        {
            set
            {
                if (value)
                {
                    SetThreadExecutionState(ScreenSaverExecutionState.DisplayRequired | ScreenSaverExecutionState.Continuous);
                    return;
                }

                SetThreadExecutionState(ScreenSaverExecutionState.Continuous);
            }
        }

        /// <summary>
        /// Use WMI to get the DateTime the current user logged on.
        /// <para>NOTE: Depending on Windows permissions settings, this may only work when the app is run as an administrator (i.e. the app has elevated privileges).</para>
        /// </summary>
        public static DateTime? LastLoginDateTime
        {
            get
            {
                try
                {
                    return Hardware.GetWmiPropertyValueAsDateTime("SELECT * FROM Win32_Session", "StartTime");
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        [DllImport("user32.dll")]
        private static extern Boolean LockWorkStation();

        private static Boolean WmiShutdown(Boolean reboot)
        {
            try
            {
                ManagementClass system = new ManagementClass("Win32_OperatingSystem");
                system.Get();

                system.Scope.Options.EnablePrivileges = true;

                ManagementBaseObject parameters = system.GetMethodParameters("Win32Shutdown");
                parameters["Flags"] = reboot ? "2" : "1";
                parameters["Reserved"] = "0";

                foreach (ManagementObject? management in system.GetInstances().OfType<ManagementObject>())
                {
                    management.InvokeMethod("Win32Shutdown", parameters, null);
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

        public static Task<Boolean> ShutdownAsync()
        {
            return ShutdownAsync(TimeSpan.Zero, CancellationToken.None);
        }

        public static Task<Boolean> ShutdownAsync(Int32 milliseconds)
        {
            return ShutdownAsync(milliseconds, CancellationToken.None);
        }

        public static Task<Boolean> ShutdownAsync(TimeSpan wait)
        {
            return ShutdownAsync(wait, CancellationToken.None);
        }

        public static Task<Boolean> ShutdownAsync(Int32 milliseconds, CancellationToken token)
        {
            return ShutdownAsync(TimeSpan.FromMilliseconds(milliseconds), token);
        }

        public static async Task<Boolean> ShutdownAsync(TimeSpan wait, CancellationToken token)
        {
            if (wait.Ticks <= 0)
            {
                WmiShutdown(false);
            }

            try
            {
                await Task.Delay(wait, token).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return false;
            }

            return WmiShutdown(false);
        }

        public static Boolean Restart()
        {
            return RestartAsync().GetAwaiter().GetResult();
        }

        public static Task<Boolean> RestartAsync()
        {
            return RestartAsync(TimeSpan.Zero, CancellationToken.None);
        }

        public static Task<Boolean> RestartAsync(Int32 milliseconds)
        {
            return RestartAsync(milliseconds, CancellationToken.None);
        }

        public static Task<Boolean> RestartAsync(TimeSpan wait)
        {
            return RestartAsync(wait, CancellationToken.None);
        }

        public static Task<Boolean> RestartAsync(Int32 milliseconds, CancellationToken token)
        {
            return RestartAsync(TimeSpan.FromMilliseconds(milliseconds), token);
        }

        public static async Task<Boolean> RestartAsync(TimeSpan wait, CancellationToken token)
        {
            if (wait.Ticks <= 0)
            {
                WmiShutdown(true);
            }

            try
            {
                await Task.Delay(wait, token).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return false;
            }

            return WmiShutdown(true);
        }

        public static Boolean Lock()
        {
            return LockAsync().GetAwaiter().GetResult();
        }

        public static Task<Boolean> LockAsync()
        {
            return LockAsync(TimeSpan.Zero, CancellationToken.None);
        }

        public static Task<Boolean> LockAsync(Int32 milliseconds)
        {
            return LockAsync(milliseconds, CancellationToken.None);
        }

        public static Task<Boolean> LockAsync(TimeSpan wait)
        {
            return LockAsync(wait, CancellationToken.None);
        }

        public static Task<Boolean> LockAsync(Int32 milliseconds, CancellationToken token)
        {
            if (milliseconds < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(milliseconds), milliseconds, null);
            }

            return LockAsync(TimeSpan.FromMilliseconds(milliseconds), token);
        }

        public static async Task<Boolean> LockAsync(TimeSpan wait, CancellationToken token)
        {
            if (wait.Ticks <= 0)
            {
                return LockWorkStation();
            }

            try
            {
                await Task.Delay(wait, token).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return false;
            }

            return LockWorkStation();
        }

        [DllImport("user32.dll")]
        private static extern Boolean ExitWindowsEx(UInt32 uFlags, UInt32 dwReason);

        public static Boolean Logoff()
        {
            return LogoffAsync().GetAwaiter().GetResult();
        }

        public static Task<Boolean> LogoffAsync()
        {
            return LogoffAsync(TimeSpan.Zero, CancellationToken.None);
        }

        public static Task<Boolean> LogoffAsync(Int32 milliseconds)
        {
            return LogoffAsync(milliseconds, CancellationToken.None);
        }

        public static Task<Boolean> LogoffAsync(TimeSpan wait)
        {
            return LogoffAsync(wait, CancellationToken.None);
        }

        public static Task<Boolean> LogoffAsync(Int32 milliseconds, CancellationToken token)
        {
            return LogoffAsync(TimeSpan.FromMilliseconds(milliseconds), token);
        }

        public static async Task<Boolean> LogoffAsync(TimeSpan wait, CancellationToken token)
        {
            if (wait.Ticks <= 0)
            {
                return ExitWindowsEx(0, 0);
            }

            try
            {
                await Task.Delay(wait, token).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return false;
            }

            return ExitWindowsEx(0, 0);
        }

        [DllImport("PowrProf.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern Boolean SetSuspendState(Boolean hibernate, Boolean forceCritical, Boolean disableWakeEvent);

        private static Boolean SetSuspendStateSleep(Boolean wake)
        {
            return SetSuspendState(false, true, !wake);
        }

        public static Boolean Sleep()
        {
            return Sleep(false);
        }

        public static Boolean Sleep(Boolean wake)
        {
            return SetSuspendStateSleep(wake);
        }

        public static Task<Boolean> SleepAsync()
        {
            return SleepAsync(TimeSpan.Zero);
        }

        public static Task<Boolean> SleepAsync(Boolean wake)
        {
            return SleepAsync(TimeSpan.Zero, wake);
        }

        public static Task<Boolean> SleepAsync(Int32 milliseconds)
        {
            return SleepAsync(milliseconds, CancellationToken.None);
        }

        public static Task<Boolean> SleepAsync(TimeSpan wait)
        {
            return SleepAsync(wait, CancellationToken.None);
        }

        public static Task<Boolean> SleepAsync(Int32 milliseconds, CancellationToken token)
        {
            return SleepAsync(milliseconds, false, token);
        }

        public static Task<Boolean> SleepAsync(TimeSpan wait, CancellationToken token)
        {
            return SleepAsync(wait, false, token);
        }

        public static Task<Boolean> SleepAsync(Int32 milliseconds, Boolean wake)
        {
            return SleepAsync(milliseconds, wake, CancellationToken.None);
        }

        public static Task<Boolean> SleepAsync(TimeSpan wait, Boolean wake)
        {
            return SleepAsync(wait, wake, CancellationToken.None);
        }

        public static Task<Boolean> SleepAsync(Int32 milliseconds, Boolean wake, CancellationToken token)
        {
            return SleepAsync(TimeSpan.FromMilliseconds(milliseconds), wake, token);
        }

        public static async Task<Boolean> SleepAsync(TimeSpan wait, Boolean wake, CancellationToken token)
        {
            if (wait.Ticks <= 0)
            {
                return SetSuspendStateSleep(wake);
            }

            try
            {
                await Task.Delay(wait, token).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return false;
            }

            return SetSuspendStateSleep(wake);
        }

        private static Boolean SetSuspendStateHibernate(Boolean wake)
        {
            return SetSuspendState(true, true, !wake);
        }

        public static Boolean Hibernate()
        {
            return Hibernate(false);
        }

        public static Boolean Hibernate(Boolean wake)
        {
            return SetSuspendStateHibernate(wake);
        }

        public static Task<Boolean> HibernateAsync()
        {
            return HibernateAsync(TimeSpan.Zero);
        }

        public static Task<Boolean> HibernateAsync(Boolean wake)
        {
            return HibernateAsync(TimeSpan.Zero, wake);
        }

        public static Task<Boolean> HibernateAsync(Int32 milliseconds)
        {
            return HibernateAsync(milliseconds, CancellationToken.None);
        }

        public static Task<Boolean> HibernateAsync(TimeSpan wait)
        {
            return HibernateAsync(wait, CancellationToken.None);
        }

        public static Task<Boolean> HibernateAsync(Int32 milliseconds, Boolean wake)
        {
            return HibernateAsync(milliseconds, wake, CancellationToken.None);
        }

        public static Task<Boolean> HibernateAsync(TimeSpan wait, Boolean wake)
        {
            return HibernateAsync(wait, wake, CancellationToken.None);
        }

        public static Task<Boolean> HibernateAsync(Int32 milliseconds, CancellationToken token)
        {
            return HibernateAsync(milliseconds, false, token);
        }

        public static Task<Boolean> HibernateAsync(TimeSpan wait, CancellationToken token)
        {
            return HibernateAsync(wait, false, token);
        }

        public static Task<Boolean> HibernateAsync(Int32 milliseconds, Boolean wake, CancellationToken token)
        {
            return HibernateAsync(TimeSpan.FromMilliseconds(milliseconds), wake, token);
        }

        public static async Task<Boolean> HibernateAsync(TimeSpan wait, Boolean wake, CancellationToken token)
        {
            if (wait.Ticks <= 0)
            {
                return SetSuspendStateHibernate(wake);
            }

            try
            {
                await Task.Delay(wait, token).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return false;
            }

            return SetSuspendStateHibernate(wake);
        }
    }
}