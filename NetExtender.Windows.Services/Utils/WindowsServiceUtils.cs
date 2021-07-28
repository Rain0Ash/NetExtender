// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Core.Types.TextWriters;
using NetExtender.Utils.Static;
using NetExtender.Utils.Types;
using NetExtender.Windows.Services.Types.TextWriters;

namespace NetExtender.Windows.Services.Utils
{
    public static class WindowsServiceUtils
    {
        //TODO: operations + async operations
        
        [DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr OpenSCManager(String? lpMachineName, String? lpSCDB, Int32 scParameter);

        [DllImport("Advapi32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr CreateService(IntPtr SC_HANDLE, String? lpSvcName, String? lpDisplayName,
            Int32 dwDesiredAccess, Int32 dwServiceType, Int32 dwStartType, Int32 dwErrorControl, String? lpPathName,
            String? lpLoadOrderGroup, Int32 lpdwTagId, String? lpDependencies, String? lpServiceStartName, String? lpPassword);

        [DllImport("advapi32.dll")]
        private static extern void CloseServiceHandle(IntPtr SCHANDLE);

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
        private static extern Int32 StartService(IntPtr SVHANDLE, Int32 dwNumServiceArgs, String? lpServiceArgVectors);

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern IntPtr OpenService(IntPtr SCHANDLE, String lpSvcName, Int32 dwNumServiceArgs);

        [DllImport("advapi32.dll")]
        private static extern Int32 DeleteService(IntPtr SVHANDLE);

        [DllImport("kernel32.dll")]
        private static extern Int32 GetLastError();

        private const String ServiceRunMessage = "Cannot start service from the command line or a debugger.  A Windows Service must first be installed and then started with the ServerExplorer, Windows Services Administrative tool or the NET START command.";
        
        public static void Run(this ServiceBase service)
        {
            Run(service, false);
        }
        
        public static void RunQuiet(this ServiceBase service)
        {
            Run(service, true);
        }

        public static void Run(this ServiceBase service, Boolean quiet)
        {
            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (!quiet)
            {
                ServiceBase.Run(service);
                return;
            }

            using FilterTextWriterWrapper filter = new ConsoleOutLockedFilterTextWriter{ ServiceRunMessage };
            ServiceBase.Run(service);
        }
        
        public static void Run(this ServiceBase[] services)
        {
            Run(services, false);
        }
        
        public static void RunQuiet(this ServiceBase[] services)
        {
            Run(services, true);
        }

        public static void Run(this ServiceBase[] services, Boolean quiet)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (!quiet)
            {
                ServiceBase.Run(services);
                return;
            }

            using TextWriter filter = new ConsoleOutLockedFilterTextWriter{ ServiceRunMessage };
            ServiceBase.Run(services);
        }

        public static Boolean CheckServiceExist(String name)
        {
            return IsServiceExistInternal(name, true);
        }
        
        public static Boolean IsServiceExist(String name)
        {
            return IsServiceExistInternal(name, false);
        }

        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Global
        private static Boolean IsServiceExistInternal(String name, Boolean isThrow)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            }

            IntPtr manager = OpenSCManager(null, null, 0x0005);
            if (manager == IntPtr.Zero)
            {
                if (isThrow)
                {
                    throw new Win32Exception(GetLastError());
                }
                
                return false;
            }

            IntPtr service = OpenService(manager, name, 0x20000);
            if (service == IntPtr.Zero)
            {
                CloseServiceHandle(manager);
                return false;
            }

            CloseServiceHandle(manager);
            return true;
        }

        private static ServiceController CreateServiceController(String name, String? machine)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            }

            return String.IsNullOrEmpty(machine) ? new ServiceController(name) : new ServiceController(name, machine);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean StartService(String name)
        {
            return StartService(name, (String?) null);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean StartService(String name, String? machine)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.StartService();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean StartService(String name, IEnumerable<String>? arguments)
        {
            return StartService(name, null, arguments);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean StartService(String name, String? machine, IEnumerable<String>? arguments)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.StartService(arguments);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean StartService(String name, Int32 milliseconds)
        {
            return StartService(name, (String?) null, milliseconds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean StartService(String name, String? machine, Int32 milliseconds)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.StartService(milliseconds);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean StartService(String name, TimeSpan timeout)
        {
            return StartService(name, (String?) null, timeout);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean StartService(String name, String? machine, TimeSpan timeout)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.StartService(timeout);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean StartService(String name, IEnumerable<String>? arguments, Int32 milliseconds)
        {
            return StartService(name, null, arguments, milliseconds);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean StartService(String name, String? machine, IEnumerable<String>? arguments, Int32 milliseconds)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.StartService(arguments, milliseconds);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean StartService(String name, IEnumerable<String>? arguments, TimeSpan timeout)
        {
            return StartService(name, null, arguments, timeout);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean StartService(String name, String? machine, IEnumerable<String>? arguments, TimeSpan timeout)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.StartService(arguments, timeout);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryStartService(String name)
        {
            return TryStartService(name, (String?) null);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryStartService(String name, String? machine)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.TryStartService();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryStartService(String name, IEnumerable<String>? arguments)
        {
            return TryStartService(name, null, arguments);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryStartService(String name, String? machine, IEnumerable<String>? arguments)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.TryStartService(arguments);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryStartService(String name, Int32 milliseconds)
        {
            return TryStartService(name, (String?) null, milliseconds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryStartService(String name, String? machine, Int32 milliseconds)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.TryStartService(milliseconds);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryStartService(String name, TimeSpan timeout)
        {
            return TryStartService(name, (String?) null, timeout);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryStartService(String name, String? machine, TimeSpan timeout)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.TryStartService(timeout);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryStartService(String name, IEnumerable<String>? arguments, Int32 milliseconds)
        {
            return TryStartService(name, null, arguments, milliseconds);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryStartService(String name, String? machine, IEnumerable<String>? arguments, Int32 milliseconds)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.TryStartService(arguments, milliseconds);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryStartService(String name, IEnumerable<String>? arguments, TimeSpan timeout)
        {
            return TryStartService(name, null, arguments, timeout);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryStartService(String name, String? machine, IEnumerable<String>? arguments, TimeSpan timeout)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.TryStartService(arguments, timeout);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean StartService(this ServiceController controller)
        {
            return StartService(controller, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean StartService(this ServiceController controller, IEnumerable<String>? arguments)
        {
            return StartService(controller, arguments, Timeout.InfiniteTimeSpan);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean StartService(this ServiceController controller, Int32 milliseconds)
        {
            return StartService(controller, null, milliseconds);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean StartService(this ServiceController controller, TimeSpan timeout)
        {
            return StartService(controller, null, timeout);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean StartService(this ServiceController controller, IEnumerable<String>? arguments, Int32 milliseconds)
        {
            return StartServiceInternal(controller, arguments, milliseconds, true);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean StartService(this ServiceController controller, IEnumerable<String>? arguments, TimeSpan timeout)
        {
            return StartServiceInternal(controller, arguments, timeout, true);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryStartService(this ServiceController controller)
        {
            return TryStartService(controller, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryStartService(this ServiceController controller, IEnumerable<String>? arguments)
        {
            return TryStartService(controller, arguments, Timeout.InfiniteTimeSpan);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryStartService(this ServiceController controller, Int32 milliseconds)
        {
            return TryStartService(controller, null, milliseconds);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryStartService(this ServiceController controller, TimeSpan timeout)
        {
            return TryStartService(controller, null, timeout);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryStartService(this ServiceController controller, IEnumerable<String>? arguments, Int32 milliseconds)
        {
            return StartServiceInternal(controller, arguments, milliseconds, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryStartService(this ServiceController controller, IEnumerable<String>? arguments, TimeSpan timeout)
        {
            return StartServiceInternal(controller, arguments, timeout, false);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean StartServiceInternal(ServiceController controller, IEnumerable<String>? arguments, Int32 milliseconds, Boolean isThrow)
        {
            if (controller is null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            return StartServiceInternal(controller, arguments, TimeSpan.FromMilliseconds(milliseconds), isThrow);
        }

        // ReSharper disable once CognitiveComplexity
        private static Boolean StartServiceInternal(ServiceController controller, IEnumerable<String>? arguments, TimeSpan timeout, Boolean isThrow)
        {
            if (controller is null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            try
            {
                switch (controller.Status)
                {
                    case ServiceControllerStatus.Stopped:
                        controller.Start(arguments);
                        controller.WaitForStatus(ServiceControllerStatus.Running, timeout);
                        goto case ServiceControllerStatus.Running;
                    case ServiceControllerStatus.StartPending:
                        controller.WaitForStatus(ServiceControllerStatus.Running, timeout);
                        goto case ServiceControllerStatus.Running;
                    case ServiceControllerStatus.StopPending:
                        controller.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                        goto case ServiceControllerStatus.Stopped;
                    case ServiceControllerStatus.Running:
                        break;
                    case ServiceControllerStatus.ContinuePending:
                        controller.WaitForStatus(ServiceControllerStatus.Running, timeout);
                        goto case ServiceControllerStatus.Running;
                    case ServiceControllerStatus.PausePending:
                        controller.WaitForStatus(ServiceControllerStatus.Paused, timeout);
                        goto case ServiceControllerStatus.Paused;
                    case ServiceControllerStatus.Paused:
                        controller.Continue();
                        controller.WaitForStatus(ServiceControllerStatus.Running, timeout);
                        goto case ServiceControllerStatus.Running;
                    default:
                        throw new NotSupportedException();
                }
            }
            catch (System.ServiceProcess.TimeoutException)
            {
                if (controller.TryWaitForTimeoutAsync(ServiceControllerStatus.Running, timeout == Timeout.InfiniteTimeSpan ? Time.Second.Three : timeout).GetAwaiter().GetResult())
                {
                    return true;
                }
                
                if (isThrow)
                {
                    throw new System.TimeoutException();
                }

                return false;
            }
            catch (Exception)
            {
                if (isThrow)
                {
                    throw;
                }

                return false;
            }

            return true;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StartServiceAsync(String name)
        {
            return StartServiceAsync(name, (String?) null);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StartServiceAsync(String name, String? machine)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.StartServiceAsync();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StartServiceAsync(String name, CancellationToken token)
        {
            return StartServiceAsync(name, (String?) null, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StartServiceAsync(String name, String? machine, CancellationToken token)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.StartServiceAsync(token);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StartServiceAsync(String name, IEnumerable<String>? arguments)
        {
            return StartServiceAsync(name, null, arguments);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StartServiceAsync(String name, String? machine, IEnumerable<String>? arguments)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.StartServiceAsync(arguments);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StartServiceAsync(String name, IEnumerable<String>? arguments, CancellationToken token)
        {
            return StartServiceAsync(name, null, arguments, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StartServiceAsync(String name, String? machine, IEnumerable<String>? arguments, CancellationToken token)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.StartServiceAsync(arguments, token);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StartServiceAsync(String name, Int32 milliseconds)
        {
            return StartServiceAsync(name, (String?) null, milliseconds);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StartServiceAsync(String name, String? machine, Int32 milliseconds)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.StartServiceAsync(milliseconds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StartServiceAsync(String name, Int32 milliseconds, CancellationToken token)
        {
            return StartServiceAsync(name, (String?) null, milliseconds, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StartServiceAsync(String name, String? machine, Int32 milliseconds, CancellationToken token)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.StartServiceAsync(milliseconds, token);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StartServiceAsync(String name, TimeSpan timeout)
        {
            return StartServiceAsync(name, (String?) null, timeout);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StartServiceAsync(String name, String? machine, TimeSpan timeout)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.StartServiceAsync(timeout);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StartServiceAsync(String name, TimeSpan timeout, CancellationToken token)
        {
            return StartServiceAsync(name, (String?) null, timeout, token);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StartServiceAsync(String name, String? machine, TimeSpan timeout, CancellationToken token)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.StartServiceAsync(timeout, token);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StartServiceAsync(String name, IEnumerable<String>? arguments, Int32 milliseconds)
        {
            return StartServiceAsync(name, null, arguments, milliseconds);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StartServiceAsync(String name, String? machine, IEnumerable<String>? arguments, Int32 milliseconds)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.StartServiceAsync(arguments, milliseconds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StartServiceAsync(String name, IEnumerable<String>? arguments, Int32 milliseconds, CancellationToken token)
        { 
            return StartServiceAsync(name, null, arguments, milliseconds, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StartServiceAsync(String name, String? machine, IEnumerable<String>? arguments, Int32 milliseconds, CancellationToken token)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.StartServiceAsync(arguments, milliseconds, token);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StartServiceAsync(String name, IEnumerable<String>? arguments, TimeSpan timeout)
        {
            return StartServiceAsync(name, null, arguments, timeout);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StartServiceAsync(String name, String? machine, IEnumerable<String>? arguments, TimeSpan timeout)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.StartServiceAsync(arguments, timeout);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StartServiceAsync(String name, IEnumerable<String>? arguments, TimeSpan timeout, CancellationToken token)
        {
            return StartServiceAsync(name, null, arguments, timeout, token);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StartServiceAsync(String name, String? machine, IEnumerable<String>? arguments, TimeSpan timeout, CancellationToken token)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.StartServiceAsync(arguments, timeout, token);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(String name)
        {
            return TryStartServiceAsync(name, (String?) null);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(String name, String? machine)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.TryStartServiceAsync();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(String name, CancellationToken token)
        {
            return TryStartServiceAsync(name, (String?) null, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(String name, String? machine, CancellationToken token)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.TryStartServiceAsync(token);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(String name, IEnumerable<String>? arguments)
        {
            return TryStartServiceAsync(name, null, arguments);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(String name, String? machine, IEnumerable<String>? arguments)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.TryStartServiceAsync(arguments);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(String name, IEnumerable<String>? arguments, CancellationToken token)
        {
            return TryStartServiceAsync(name, null, arguments, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(String name, String? machine, IEnumerable<String>? arguments, CancellationToken token)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.TryStartServiceAsync(arguments, token);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(String name, Int32 milliseconds)
        {
            return TryStartServiceAsync(name, (String?) null, milliseconds);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(String name, String? machine, Int32 milliseconds)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.TryStartServiceAsync(milliseconds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(String name, Int32 milliseconds, CancellationToken token)
        {
            return TryStartServiceAsync(name, (String?) null, milliseconds, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(String name, String? machine, Int32 milliseconds, CancellationToken token)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.TryStartServiceAsync(milliseconds, token);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(String name, TimeSpan timeout)
        {
            return TryStartServiceAsync(name, (String?) null, timeout);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(String name, String? machine, TimeSpan timeout)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.TryStartServiceAsync(timeout);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(String name, TimeSpan timeout, CancellationToken token)
        {
            return TryStartServiceAsync(name, (String?) null, timeout, token);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(String name, String? machine, TimeSpan timeout, CancellationToken token)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.TryStartServiceAsync(timeout, token);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(String name, IEnumerable<String>? arguments, Int32 milliseconds)
        {
            return TryStartServiceAsync(name, null, arguments, milliseconds);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(String name, String? machine, IEnumerable<String>? arguments, Int32 milliseconds)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.TryStartServiceAsync(arguments, milliseconds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(String name, IEnumerable<String>? arguments, Int32 milliseconds, CancellationToken token)
        { 
            return TryStartServiceAsync(name, null, arguments, milliseconds, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(String name, String? machine, IEnumerable<String>? arguments, Int32 milliseconds, CancellationToken token)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.TryStartServiceAsync(arguments, milliseconds, token);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(String name, IEnumerable<String>? arguments, TimeSpan timeout)
        {
            return TryStartServiceAsync(name, null, arguments, timeout);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(String name, String? machine, IEnumerable<String>? arguments, TimeSpan timeout)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.TryStartServiceAsync(arguments, timeout);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(String name, IEnumerable<String>? arguments, TimeSpan timeout, CancellationToken token)
        {
            return TryStartServiceAsync(name, null, arguments, timeout, token);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(String name, String? machine, IEnumerable<String>? arguments, TimeSpan timeout, CancellationToken token)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.TryStartServiceAsync(arguments, timeout, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StartServiceAsync(this ServiceController controller)
        {
            return StartServiceAsync(controller, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StartServiceAsync(this ServiceController controller, CancellationToken token)
        {
            return StartServiceAsync(controller, null, token);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StartServiceAsync(this ServiceController controller, IEnumerable<String>? arguments)
        {
            return StartServiceAsync(controller, arguments, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StartServiceAsync(this ServiceController controller, IEnumerable<String>? arguments, CancellationToken token)
        {
            return StartServiceAsync(controller, arguments, Timeout.InfiniteTimeSpan, token);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StartServiceAsync(this ServiceController controller, Int32 milliseconds)
        {
            return StartServiceAsync(controller, milliseconds, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StartServiceAsync(this ServiceController controller, Int32 milliseconds, CancellationToken token)
        {
            return StartServiceAsync(controller, null, milliseconds, token);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StartServiceAsync(this ServiceController controller, TimeSpan timeout)
        {
            return StartServiceAsync(controller, timeout, CancellationToken.None);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StartServiceAsync(this ServiceController controller, TimeSpan timeout, CancellationToken token)
        {
            return StartServiceAsync(controller, null, timeout, token);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StartServiceAsync(this ServiceController controller, IEnumerable<String>? arguments, Int32 milliseconds)
        {
            return StartServiceAsync(controller, arguments, milliseconds, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StartServiceAsync(this ServiceController controller, IEnumerable<String>? arguments, Int32 milliseconds, CancellationToken token)
        {
            return StartServiceInternalAsync(controller, arguments, milliseconds, true, token);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StartServiceAsync(this ServiceController controller, IEnumerable<String>? arguments, TimeSpan timeout)
        {
            return StartServiceAsync(controller, arguments, timeout, CancellationToken.None);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StartServiceAsync(this ServiceController controller, IEnumerable<String>? arguments, TimeSpan timeout, CancellationToken token)
        {
            return StartServiceInternalAsync(controller, arguments, timeout, true, token);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(this ServiceController controller)
        {
            return TryStartServiceAsync(controller, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(this ServiceController controller, CancellationToken token)
        {
            return TryStartServiceAsync(controller, null, token);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(this ServiceController controller, IEnumerable<String>? arguments)
        {
            return TryStartServiceAsync(controller, arguments, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(this ServiceController controller, IEnumerable<String>? arguments, CancellationToken token)
        {
            return TryStartServiceAsync(controller, arguments, Timeout.InfiniteTimeSpan, token);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(this ServiceController controller, Int32 milliseconds)
        {
            return TryStartServiceAsync(controller, milliseconds, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(this ServiceController controller, Int32 milliseconds, CancellationToken token)
        {
            return TryStartServiceAsync(controller, null, milliseconds, token);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(this ServiceController controller, TimeSpan timeout)
        {
            return TryStartServiceAsync(controller, timeout, CancellationToken.None);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(this ServiceController controller, TimeSpan timeout, CancellationToken token)
        {
            return TryStartServiceAsync(controller, null, timeout, token);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(this ServiceController controller, IEnumerable<String>? arguments, Int32 milliseconds)
        {
            return TryStartServiceAsync(controller, arguments, milliseconds, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(this ServiceController controller, IEnumerable<String>? arguments, Int32 milliseconds, CancellationToken token)
        {
            return StartServiceInternalAsync(controller, arguments, milliseconds, false, token);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(this ServiceController controller, IEnumerable<String>? arguments, TimeSpan timeout)
        {
            return TryStartServiceAsync(controller, arguments, timeout, CancellationToken.None);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(this ServiceController controller, IEnumerable<String>? arguments, TimeSpan timeout, CancellationToken token)
        {
            return StartServiceInternalAsync(controller, arguments, timeout, false, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Task<Boolean> StartServiceInternalAsync(ServiceController controller, IEnumerable<String>? arguments, Int32 milliseconds, Boolean isThrow, CancellationToken token)
        {
            if (controller is null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            return StartServiceInternalAsync(controller, arguments, TimeSpan.FromMilliseconds(milliseconds), isThrow, token);
        }

        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        // ReSharper disable once CognitiveComplexity
        private static async Task<Boolean> StartServiceInternalAsync(ServiceController controller, IEnumerable<String>? arguments, TimeSpan timeout, Boolean isThrow, CancellationToken token)
        {
            if (controller is null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            try
            {
                switch (controller.Status)
                {
                    case ServiceControllerStatus.Stopped:
                        controller.Start(arguments);
                        await controller.WaitForStatusAsync(ServiceControllerStatus.Running, timeout, token);
                        goto case ServiceControllerStatus.Running;
                    case ServiceControllerStatus.StartPending:
                        await controller.WaitForStatusAsync(ServiceControllerStatus.Running, timeout, token);
                        goto case ServiceControllerStatus.Running;
                    case ServiceControllerStatus.StopPending:
                        await controller.WaitForStatusAsync(ServiceControllerStatus.Stopped, timeout, token);
                        goto case ServiceControllerStatus.Stopped;
                    case ServiceControllerStatus.Running:
                        break;
                    case ServiceControllerStatus.ContinuePending:
                        await controller.WaitForStatusAsync(ServiceControllerStatus.Running, timeout, token);
                        goto case ServiceControllerStatus.Running;
                    case ServiceControllerStatus.PausePending:
                        await controller.WaitForStatusAsync(ServiceControllerStatus.Paused, timeout, token);
                        goto case ServiceControllerStatus.Paused;
                    case ServiceControllerStatus.Paused:
                        controller.Continue();
                        await controller.WaitForStatusAsync(ServiceControllerStatus.Running, timeout, token);
                        goto case ServiceControllerStatus.Running;
                    default:
                        throw new NotSupportedException();
                }
            }
            catch (System.TimeoutException)
            {
                if (await controller.TryWaitForTimeoutAsync(ServiceControllerStatus.Running, timeout == Timeout.InfiniteTimeSpan ? Time.Second.Three : timeout, token))
                {
                    return true;
                }
                
                if (isThrow)
                {
                    throw;
                }

                return false;
            }
            catch (Exception)
            {
                if (isThrow)
                {
                    throw;
                }

                return false;
            }

            return true;
        }
        
        private static Task<Boolean> StopServiceInternalAsync(ServiceController controller, Int32 milliseconds, Boolean isThrow, CancellationToken token)
        {
            if (controller is null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            return StopServiceInternalAsync(controller, TimeSpan.FromMilliseconds(milliseconds), isThrow, token);
        }

        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        private static async Task<Boolean> StopServiceInternalAsync(ServiceController controller, TimeSpan timeout, Boolean isThrow, CancellationToken token)
        {
            if (controller is null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            try
            {
                switch (controller.Status)
                {
                    case ServiceControllerStatus.Stopped:
                        break;
                    case ServiceControllerStatus.StartPending:
                        await controller.WaitForStatusAsync(ServiceControllerStatus.Running, timeout, token);
                        goto case ServiceControllerStatus.Running;
                    case ServiceControllerStatus.StopPending:
                        await controller.WaitForStatusAsync(ServiceControllerStatus.Stopped, timeout, token);
                        goto case ServiceControllerStatus.Stopped;
                    case ServiceControllerStatus.Running:
                        if (!controller.CanStop)
                        {
                            return false;
                        }
                        
                        controller.Stop();
                        await controller.WaitForStatusAsync(ServiceControllerStatus.Stopped, timeout, token);
                        goto case ServiceControllerStatus.Stopped;
                    case ServiceControllerStatus.ContinuePending:
                        await controller.WaitForStatusAsync(ServiceControllerStatus.Running, timeout, token);
                        goto case ServiceControllerStatus.Running;
                    case ServiceControllerStatus.PausePending:
                        await controller.WaitForStatusAsync(ServiceControllerStatus.Paused, timeout, token);
                        goto case ServiceControllerStatus.Paused;
                    case ServiceControllerStatus.Paused:
                        if (!controller.CanStop)
                        {
                            return false;
                        }
                        
                        controller.Stop();
                        await controller.WaitForStatusAsync(ServiceControllerStatus.Stopped, timeout, token);
                        goto case ServiceControllerStatus.Stopped;
                    default:
                        throw new NotSupportedException();
                }
            }
            catch (Exception)
            {
                if (isThrow)
                {
                    throw;
                }

                return false;
            }

            return true;
        }
        
        private static Boolean ContinueServiceInternal(ServiceController controller, IEnumerable<String>? arguments, Int32 milliseconds, Boolean isThrow)
        {
            if (controller is null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            return ContinueServiceInternal(controller, arguments, TimeSpan.FromMilliseconds(milliseconds), isThrow);
        }

        private static Boolean ContinueServiceInternal(ServiceController controller, IEnumerable<String>? arguments, TimeSpan timeout, Boolean isThrow)
        {
            if (controller is null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            return StartServiceInternal(controller, arguments, timeout, isThrow);
        }
        
        private static Task<Boolean> ContinueServiceInternalAsync(ServiceController controller, IEnumerable<String>? arguments, Int32 milliseconds, Boolean isThrow, CancellationToken token)
        {
            if (controller is null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            return ContinueServiceInternalAsync(controller, arguments, TimeSpan.FromMilliseconds(milliseconds), isThrow, token);
        }

        private static Task<Boolean> ContinueServiceInternalAsync(ServiceController controller, IEnumerable<String>? arguments, TimeSpan timeout, Boolean isThrow, CancellationToken token)
        {
            if (controller is null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            return StartServiceInternalAsync(controller, arguments, timeout, isThrow, token);
        }
        
        private static Task<Boolean> PauseServiceInternalAsync(ServiceController controller, Int32 milliseconds, Boolean isThrow, CancellationToken token)
        {
            if (controller is null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            return PauseServiceInternalAsync(controller, TimeSpan.FromMilliseconds(milliseconds), isThrow, token);
        }

        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        private static async Task<Boolean> PauseServiceInternalAsync(ServiceController controller, TimeSpan timeout, Boolean isThrow, CancellationToken token)
        {
            if (controller is null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            try
            {
                switch (controller.Status)
                {
                    case ServiceControllerStatus.Stopped:
                        break;
                    case ServiceControllerStatus.StartPending:
                        await controller.WaitForStatusAsync(ServiceControllerStatus.Running, timeout, token);
                        goto case ServiceControllerStatus.Running;
                    case ServiceControllerStatus.StopPending:
                        await controller.WaitForStatusAsync(ServiceControllerStatus.Stopped, timeout, token);
                        goto case ServiceControllerStatus.Stopped;
                    case ServiceControllerStatus.Running:
                        if (!controller.CanPauseAndContinue)
                        {
                            return false;
                        }
                        
                        controller.Pause();
                        await controller.WaitForStatusAsync(ServiceControllerStatus.Paused, timeout, token);
                        goto case ServiceControllerStatus.Paused;
                    case ServiceControllerStatus.ContinuePending:
                        await controller.WaitForStatusAsync(ServiceControllerStatus.Running, timeout, token);
                        goto case ServiceControllerStatus.Running;
                    case ServiceControllerStatus.PausePending:
                        await controller.WaitForStatusAsync(ServiceControllerStatus.Paused, timeout, token);
                        goto case ServiceControllerStatus.Paused;
                    case ServiceControllerStatus.Paused:
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
            catch (Exception)
            {
                if (isThrow)
                {
                    throw;
                }

                return false;
            }

            return true;
        }

        public static Boolean InstallService(String path, String name)
        {
            return InstallService(path, name, name);
        }

        public static Boolean InstallService(String path, String name, String? displayname)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(path));
            }

            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            }

            return InstallService(new FileInfo(path), name, displayname);
        }

        public static Boolean InstallService(FileInfo info, String name)
        {
            return InstallService(info, name, name);
        }
        
        public static Boolean InstallService(FileInfo info, String name, String? displayname)
        {
            return InstallServiceInternal(info, name, displayname, true);
        }
        
        public static Boolean TryInstallService(String path, String name)
        {
            return TryInstallService(path, name, name);
        }

        public static Boolean TryInstallService(String path, String name, String? displayname)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(path));
            }

            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            }

            return TryInstallService(new FileInfo(path), name, displayname);
        }

        public static Boolean TryInstallService(FileInfo info, String name)
        {
            return TryInstallService(info, name, name);
        }
        
        public static Boolean TryInstallService(FileInfo info, String name, String? displayname)
        {
            return InstallServiceInternal(info, name, displayname, false);
        }

        // ReSharper disable once CognitiveComplexity
        private static Boolean InstallServiceInternal(FileInfo info, String name, String? displayname, Boolean isThrow)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            }

            if (String.IsNullOrEmpty(displayname))
            {
                displayname = name;
            }

            if (!info.Exists)
            {
                if (isThrow)
                {
                    throw new ArgumentException("File doesn't exist", nameof(info));
                }
                
                return false;
            }

            String path;
            try
            {
                path = info.FullName;
            }
            catch (Exception)
            {
                if (isThrow)
                {
                    throw;
                }
                
                return false;
            }
            
            IntPtr manager = OpenSCManager(null, null, 0x0002);
            if (manager == IntPtr.Zero)
            {
                if (isThrow)
                {
                    throw new Win32Exception(GetLastError());
                }
                
                return false;
            }

            IntPtr service = CreateService(manager, name, displayname, 
                983551, 0x00000010, 0x00000002, 0x00000001,
                path, null, 0, null, null, null);
            
            if (service == IntPtr.Zero)
            {
                CloseServiceHandle(manager);

                if (isThrow)
                {
                    throw new Win32Exception(GetLastError());
                }
                
                return false;
            }

            Int32 i = StartService(service, 0, null);
            if (i == 0)
            {
                CloseServiceHandle(manager);
                
                if (isThrow)
                {
                    throw new Win32Exception(GetLastError());
                }
                
                return false;
            }

            CloseServiceHandle(manager);
            return true;
        }

        /// <summary>
        /// This method uninstalls the service from the service control manager.
        /// </summary>
        /// <param name="name">Name of the service to uninstall.</param>
        public static Boolean UninstallService(String name)
        {
            return UninstallServiceInternal(name, true);
        }
        
        /// <summary>
        /// This method uninstalls the service from the service control manager.
        /// </summary>
        /// <param name="name">Name of the service to uninstall.</param>
        public static Boolean TryUninstallService(String name)
        {
            return UninstallServiceInternal(name, false);
        }

        private static Boolean UninstallServiceInternal(String name, Boolean isThrow)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            }

            IntPtr manager = OpenSCManager(null, null, 0x40000000);
            if (manager == IntPtr.Zero)
            {
                if (isThrow)
                {
                    throw new Win32Exception(GetLastError());
                }
                
                return false;
            }

            IntPtr service = OpenService(manager, name, 0x10000);
            if (service == IntPtr.Zero)
            {
                if (isThrow)
                {
                    throw new Win32Exception(GetLastError());
                }
                
                return false;
            }

            Int32 i = DeleteService(service);
            if (i != 0)
            {
                CloseServiceHandle(manager);
                
                if (isThrow)
                {
                    throw new Win32Exception(GetLastError());
                }
                
                return true;
            }

            CloseServiceHandle(manager);
            return false;
        }

        public static Boolean ReinstallService(String path, String name)
        {
            return ReinstallService(path, name, name);
        }

        public static Boolean ReinstallService(String path, String name, String? displayname)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(path));
            }

            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            }

            return ReinstallService(new FileInfo(path), name, displayname);
        }

        public static Boolean ReinstallService(FileInfo info, String name)
        {
            return ReinstallService(info, name, name);
        }
        
        public static Boolean ReinstallService(FileInfo info, String name, String? displayname)
        {
            return ReinstallServiceInternal(info, name, displayname, true);
        }
        
        public static Boolean TryReinstallService(String path, String name)
        {
            return TryReinstallService(path, name, name);
        }

        public static Boolean TryReinstallService(String path, String name, String? displayname)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(path));
            }

            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            }

            return TryReinstallService(new FileInfo(path), name, displayname);
        }

        public static Boolean TryReinstallService(FileInfo info, String name)
        {
            return TryReinstallService(info, name, name);
        }
        
        public static Boolean TryReinstallService(FileInfo info, String name, String? displayname)
        {
            return ReinstallServiceInternal(info, name, displayname, false);
        }

        private static Boolean ReinstallServiceInternal(FileInfo info, String name, String? displayname, Boolean isThrow)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            }

            if (!info.Exists)
            {
                if (isThrow)
                {
                    throw new ArgumentException("File doesn't exist", nameof(info));
                }
                
                return false;
            }

            if (IsServiceExist(name))
            {
                TryUninstallService(name);
            }
            
            return InstallServiceInternal(info, name, displayname, isThrow);
        }
        
        public static Boolean InstallServiceIfNotExists(String path, String name)
        {
            return InstallServiceIfNotExists(path, name, name);
        }

        public static Boolean InstallServiceIfNotExists(String path, String name, String? displayname)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(path));
            }

            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            }

            return InstallServiceIfNotExists(new FileInfo(path), name, displayname);
        }

        public static Boolean InstallServiceIfNotExists(FileInfo info, String name)
        {
            return InstallServiceIfNotExists(info, name, name);
        }
        
        public static Boolean InstallServiceIfNotExists(FileInfo info, String name, String? displayname)
        {
            return InstallServiceIfNotExistsInternal(info, name, displayname, true);
        }
        
        public static Boolean TryInstallServiceIfNotExists(String path, String name)
        {
            return TryInstallServiceIfNotExists(path, name, name);
        }

        public static Boolean TryInstallServiceIfNotExists(String path, String name, String? displayname)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(path));
            }

            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            }

            return TryInstallServiceIfNotExists(new FileInfo(path), name, displayname);
        }

        public static Boolean TryInstallServiceIfNotExists(FileInfo info, String name)
        {
            return TryInstallServiceIfNotExists(info, name, name);
        }
        
        public static Boolean TryInstallServiceIfNotExists(FileInfo info, String name, String? displayname)
        {
            return InstallServiceIfNotExistsInternal(info, name, displayname, false);
        }

        private static Boolean InstallServiceIfNotExistsInternal(FileInfo info, String name, String? displayname, Boolean isThrow)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            }

            if (info.Exists)
            {
                return IsServiceExist(name) || InstallServiceInternal(info, name, displayname, isThrow);
            }

            if (isThrow)
            {
                throw new ArgumentException("File doesn't exist", nameof(info));
            }
                
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WaitForStatusAsync(this ServiceController controller, ServiceControllerStatus status)
        {
            return WaitForStatusAsync(controller, status, Timeout.InfiniteTimeSpan, CancellationToken.None);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WaitForStatusAsync(this ServiceController controller, ServiceControllerStatus status, Int32 milliseconds)
        {
            if (controller is null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            return WaitForStatusAsync(controller, status, milliseconds, CancellationToken.None);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WaitForStatusAsync(this ServiceController controller, ServiceControllerStatus status, TimeSpan timeout)
        {
            return WaitForStatusAsync(controller, status, timeout, CancellationToken.None);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WaitForStatusAsync(this ServiceController controller, ServiceControllerStatus status, CancellationToken token)
        {
            return WaitForStatusAsync(controller, status, Timeout.InfiniteTimeSpan, token);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WaitForStatusAsync(this ServiceController controller, ServiceControllerStatus status, Int32 milliseconds, CancellationToken token)
        {
            if (controller is null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            return WaitForStatusAsync(controller, status, TimeSpan.FromMilliseconds(milliseconds), token);
        }

        public static async Task WaitForStatusAsync(this ServiceController controller, ServiceControllerStatus status, TimeSpan timeout, CancellationToken token)
        {
            if (controller is null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            try
            {
                await Task.Run(() => controller.WaitForStatus(status, timeout), token);
            }
            catch (System.ServiceProcess.TimeoutException)
            {
                try
                {
                    controller.Refresh();
                    if (controller.Status == status)
                    {
                        return;
                    }
                }
                catch (Exception)
                {
                    // ignored
                }

                throw new System.TimeoutException();
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> WaitForTimeoutAsync(this ServiceController controller, ServiceControllerStatus status)
        {
            return WaitForTimeoutAsync(controller, status, CancellationToken.None);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> WaitForTimeoutAsync(this ServiceController controller, ServiceControllerStatus status, CancellationToken token)
        {
            return WaitForTimeoutAsync(controller, status, Timeout.InfiniteTimeSpan, token);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> WaitForTimeoutAsync(this ServiceController controller, ServiceControllerStatus status, Int32 timeout)
        {
            return WaitForTimeoutAsync(controller, status, timeout, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> WaitForTimeoutAsync(this ServiceController controller, ServiceControllerStatus status, Int32 timeout, CancellationToken token)
        {
            return WaitForTimeoutAsync(controller, status, TimeSpan.FromMilliseconds(timeout), token);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> WaitForTimeoutAsync(this ServiceController controller, ServiceControllerStatus status, TimeSpan timeout)
        {
            return WaitForTimeoutAsync(controller, status, timeout, CancellationToken.None);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> WaitForTimeoutAsync(this ServiceController controller, ServiceControllerStatus status, TimeSpan timeout, CancellationToken token)
        {
            return WaitForTimeoutInternalAsync(controller, status, timeout, true, token);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryWaitForTimeoutAsync(this ServiceController controller, ServiceControllerStatus status)
        {
            return TryWaitForTimeoutAsync(controller, status, CancellationToken.None);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryWaitForTimeoutAsync(this ServiceController controller, ServiceControllerStatus status, CancellationToken token)
        {
            return TryWaitForTimeoutAsync(controller, status, Timeout.InfiniteTimeSpan, token);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryWaitForTimeoutAsync(this ServiceController controller, ServiceControllerStatus status, Int32 timeout)
        {
            return WaitForTimeoutAsync(controller, status, timeout, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryWaitForTimeoutAsync(this ServiceController controller, ServiceControllerStatus status, Int32 timeout, CancellationToken token)
        {
            return WaitForTimeoutAsync(controller, status, TimeSpan.FromMilliseconds(timeout), token);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryWaitForTimeoutAsync(this ServiceController controller, ServiceControllerStatus status, TimeSpan timeout)
        {
            return WaitForTimeoutAsync(controller, status, timeout, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryWaitForTimeoutAsync(this ServiceController controller, ServiceControllerStatus status, TimeSpan timeout, CancellationToken token)
        {
            return WaitForTimeoutInternalAsync(controller, status, timeout, false, token);
        }

        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        private static async Task<Boolean> WaitForTimeoutInternalAsync(this ServiceController controller, ServiceControllerStatus status, TimeSpan timeout, Boolean isThrow, CancellationToken token)
        {
            if (controller is null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            Boolean Refresh()
            {
                controller.Refresh();
                return controller.Status == status;
            }

            try
            {
                await TaskUtils.TryWaitAsync(Refresh, Time.Milli.Hundred, timeout, token);
                return true;
            }
            catch (Exception)
            {
                if (isThrow)
                {
                    throw;
                }

                return false;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Start(this ServiceController controller, IEnumerable<String>? arguments)
        {
            if (controller is null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            if (arguments is null)
            {
                controller.Start();
                return;
            }
            
            controller.Start(arguments.WhereNotNull().ToArray());
        }
    }
}