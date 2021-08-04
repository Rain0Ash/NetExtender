// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Core.Types.TextWriters;
using NetExtender.Utils.Static;
using NetExtender.Utils.Types;
using NetExtender.Windows.Services.Types.Services;
using NetExtender.Windows.Services.Types.Services.Interfaces;
using NetExtender.Windows.Services.Types.TextWriters;

namespace NetExtender.Windows.Services.Utils
{
    [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local")]
    [SuppressMessage("ReSharper", "CognitiveComplexity")]
    public static class WindowsServiceUtils
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private readonly struct ServiceDescription : IDisposable
        {
            public IntPtr Description { get; }

            public ServiceDescription(String description)
            {
                Description = Marshal.StringToHGlobalUni(description ?? throw new ArgumentNullException(nameof(description)));
            }

            public void Dispose()
            {
                Marshal.FreeHGlobal(Description);
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private readonly struct ServiceDelayedAutostartInfo
        {
            public Boolean AutoStart { get; }

            public ServiceDelayedAutostartInfo(Boolean autostart)
            {
                AutoStart = autostart;
            }
        }

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr OpenSCManager(String? lpMachineName, String? lpSCDB, Int32 scParameter);

        [DllImport("Advapi32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr CreateService(IntPtr SC_HANDLE, String? lpSvcName, String? lpDisplayName,
            Int32 dwDesiredAccess, Int32 dwServiceType, Int32 dwStartType, Int32 dwErrorControl, String? lpPathName,
            String? lpLoadOrderGroup, IntPtr lpdwTagId, String? lpDependencies, String? lpServiceStartName, String? lpPassword);

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern Boolean ChangeServiceConfig2(IntPtr serviceHandle, UInt32 infoLevel, ref ServiceDescription serviceDesc);

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern Boolean ChangeServiceConfig2(IntPtr serviceHandle, UInt32 infoLevel, ref ServiceDelayedAutostartInfo serviceDesc);

        [DllImport("advapi32.dll")]
        private static extern void CloseServiceHandle(IntPtr schandle);

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
        private static extern Int32 StartService(IntPtr svhandle, Int32 dwNumServiceArgs, String? lpServiceArgVectors);

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern IntPtr OpenService(IntPtr schandle, String lpSvcName, Int32 dwNumServiceArgs);

        [DllImport("advapi32.dll")]
        private static extern Int32 DeleteService(IntPtr svhandle);

        [DllImport("kernel32.dll")]
        private static extern Int32 GetLastError();

        public static IEnumerable<ServiceController> ConvertToServiceControllers(IEnumerable<String?> controllers)
        {
            if (controllers is null)
            {
                throw new ArgumentNullException(nameof(controllers));
            }

            foreach (String? name in controllers)
            {
                if (String.IsNullOrEmpty(name))
                {
                    continue;
                }

                ServiceController? controller = null;

                try
                {
                    controller = new ServiceController(name, ".");
                }
                catch (Exception)
                {
                    // ignored
                }

                if (controller is not null)
                {
                    yield return controller;
                }
            }
        }

        private static String? ConvertToDependencyString(this IEnumerable<String?>? dependency)
        {
            return dependency is not null ? ConvertToServiceControllers(dependency)?.ConvertToDependencyString() : null;
        }

        private static String? ConvertToDependencyString(this IEnumerable<ServiceController?>? controllers)
        {
            if (controllers is null)
            {
                return null;
            }

            StringBuilder? builder = null;

            foreach (ServiceController? controller in controllers)
            {
                if (controller is null)
                {
                    continue;
                }

                try
                {
                    String name = controller.ServiceName;
                    if (String.IsNullOrEmpty(name))
                    {
                        continue;
                    }

                    builder ??= new StringBuilder(80);
                    builder.Append(name);
                    builder.Append(Char.MinValue);
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            return builder?.Append(Char.MinValue)?.ToString();
        }

        public static Boolean IsValidServiceName(String name)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            static Boolean IsValidCharacter(Char character)
            {
                return character >= ' ' && character != '/' && character != '\\';
            }

            return name.Length is > 0 and <= 80 && name.All(IsValidCharacter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ServiceController[] GetDevices()
        {
            return GetDevices(null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ServiceController[] GetDevices(String? machine)
        {
            return String.IsNullOrEmpty(machine) ? ServiceController.GetDevices() : ServiceController.GetDevices(machine);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ServiceController[] GetServices()
        {
            return GetServices(null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ServiceController[] GetServices(String? machine)
        {
            return String.IsNullOrEmpty(machine) ? ServiceController.GetServices() : ServiceController.GetServices(machine);
        }

        private const String ServiceRunMessage =
            "Cannot start service from the command line or a debugger.  A Windows Service must first be installed and then started with the ServerExplorer, Windows Services Administrative tool or the NET START command.";

        public static ServiceBase Run(this ServiceBase service)
        {
            return Run(service, false);
        }
        
        public static WindowsService Run(this WindowsService service)
        {
            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            Run((ServiceBase) service);
            return service;
        }
        
        public static IWindowsService Run(this IWindowsService service)
        {
            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            Run(service.Service);
            return service;
        }

        public static ServiceBase RunQuiet(this ServiceBase service)
        {
            return Run(service, true);
        }

        public static WindowsService RunQuiet(this WindowsService service)
        {
            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            RunQuiet((ServiceBase) service);
            return service;
        }
        
        public static IWindowsService RunQuiet(this IWindowsService service)
        {
            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            RunQuiet(service.Service);
            return service;
        }

        public static ServiceBase Run(this ServiceBase service, Boolean quiet)
        {
            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (!quiet)
            {
                ServiceBase.Run(service);
                return service;
            }

            using FilterTextWriterWrapper filter = new ConsoleOutLockedFilterTextWriter {ServiceRunMessage};
            ServiceBase.Run(service);
            return service;
        }

        public static WindowsService Run(this WindowsService service, Boolean quiet)
        {
            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            Run((ServiceBase) service, quiet);
            return service;
        }

        public static IWindowsService Run(this IWindowsService service, Boolean quiet)
        {
            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            Run(service.Service, quiet);
            return service;
        }

        public static void Run(this ServiceBase[] services)
        {
            Run(services, false);
        }
        
        public static void Run(this WindowsService[] services)
        {
            Run(services, false);
        }

        public static void Run(this IWindowsService[] services)
        {
            Run(services, false);
        }

        public static void RunQuiet(this ServiceBase[] services)
        {
            Run(services, true);
        }

        public static void RunQuiet(this WindowsService[] services)
        {
            Run(services, true);
        }
        
        public static void RunQuiet(this IWindowsService[] services)
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

            using TextWriter filter = new ConsoleOutLockedFilterTextWriter {ServiceRunMessage};
            ServiceBase.Run(services);
        }
        
        public static void Run(this WindowsService[] services, Boolean quiet)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            
            Run(services.Cast<ServiceBase>().ToArray(), quiet);
        }

        public static void Run(this IWindowsService[] services, Boolean quiet)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            
            Run(services.SelectWhereNotNull(service => service.Service).ToArray(), quiet);
        }

        private static ServiceController CreateServiceController(String name, String? machine)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (!IsValidServiceName(name))
            {
                throw new ArgumentException("Service name is invalid.", nameof(name));
            }

            return String.IsNullOrEmpty(machine) ? new ServiceController(name) : new ServiceController(name, machine);
        }

        private static ServiceController? TryCreateServiceController(String name, String? machine)
        {
            try
            {
                return CreateServiceController(name, machine);
            }
            catch (Exception)
            {
                return null;
            }
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
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryStartService() ?? false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryStartService(String name, IEnumerable<String>? arguments)
        {
            return TryStartService(name, null, arguments);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryStartService(String name, String? machine, IEnumerable<String>? arguments)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryStartService(arguments) ?? false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryStartService(String name, Int32 milliseconds)
        {
            return TryStartService(name, (String?) null, milliseconds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryStartService(String name, String? machine, Int32 milliseconds)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryStartService(milliseconds) ?? false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryStartService(String name, TimeSpan timeout)
        {
            return TryStartService(name, (String?) null, timeout);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryStartService(String name, String? machine, TimeSpan timeout)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryStartService(timeout) ?? false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryStartService(String name, IEnumerable<String>? arguments, Int32 milliseconds)
        {
            return TryStartService(name, null, arguments, milliseconds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryStartService(String name, String? machine, IEnumerable<String>? arguments, Int32 milliseconds)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryStartService(arguments, milliseconds) ?? false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryStartService(String name, IEnumerable<String>? arguments, TimeSpan timeout)
        {
            return TryStartService(name, null, arguments, timeout);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryStartService(String name, String? machine, IEnumerable<String>? arguments, TimeSpan timeout)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryStartService(arguments, timeout) ?? false;
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
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryStartServiceAsync() ?? TaskUtils.False;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(String name, CancellationToken token)
        {
            return TryStartServiceAsync(name, (String?) null, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(String name, String? machine, CancellationToken token)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryStartServiceAsync(token) ?? TaskUtils.False;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(String name, IEnumerable<String>? arguments)
        {
            return TryStartServiceAsync(name, null, arguments);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(String name, String? machine, IEnumerable<String>? arguments)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryStartServiceAsync(arguments) ?? TaskUtils.False;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(String name, IEnumerable<String>? arguments, CancellationToken token)
        {
            return TryStartServiceAsync(name, null, arguments, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(String name, String? machine, IEnumerable<String>? arguments, CancellationToken token)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryStartServiceAsync(arguments, token) ?? TaskUtils.False;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(String name, Int32 milliseconds)
        {
            return TryStartServiceAsync(name, (String?) null, milliseconds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(String name, String? machine, Int32 milliseconds)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryStartServiceAsync(milliseconds) ?? TaskUtils.False;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(String name, Int32 milliseconds, CancellationToken token)
        {
            return TryStartServiceAsync(name, (String?) null, milliseconds, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(String name, String? machine, Int32 milliseconds, CancellationToken token)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryStartServiceAsync(milliseconds, token) ?? TaskUtils.False;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(String name, TimeSpan timeout)
        {
            return TryStartServiceAsync(name, (String?) null, timeout);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(String name, String? machine, TimeSpan timeout)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryStartServiceAsync(timeout) ?? TaskUtils.False;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(String name, TimeSpan timeout, CancellationToken token)
        {
            return TryStartServiceAsync(name, (String?) null, timeout, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(String name, String? machine, TimeSpan timeout, CancellationToken token)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryStartServiceAsync(timeout, token) ?? TaskUtils.False;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(String name, IEnumerable<String>? arguments, Int32 milliseconds)
        {
            return TryStartServiceAsync(name, null, arguments, milliseconds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(String name, String? machine, IEnumerable<String>? arguments, Int32 milliseconds)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryStartServiceAsync(arguments, milliseconds) ?? TaskUtils.False;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(String name, IEnumerable<String>? arguments, Int32 milliseconds, CancellationToken token)
        {
            return TryStartServiceAsync(name, null, arguments, milliseconds, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(String name, String? machine, IEnumerable<String>? arguments, Int32 milliseconds, CancellationToken token)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryStartServiceAsync(arguments, milliseconds, token) ?? TaskUtils.False;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(String name, IEnumerable<String>? arguments, TimeSpan timeout)
        {
            return TryStartServiceAsync(name, null, arguments, timeout);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(String name, String? machine, IEnumerable<String>? arguments, TimeSpan timeout)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryStartServiceAsync(arguments, timeout) ?? TaskUtils.False;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(String name, IEnumerable<String>? arguments, TimeSpan timeout, CancellationToken token)
        {
            return TryStartServiceAsync(name, null, arguments, timeout, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStartServiceAsync(String name, String? machine, IEnumerable<String>? arguments, TimeSpan timeout, CancellationToken token)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryStartServiceAsync(arguments, timeout, token) ?? TaskUtils.False;
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
        private static Task<Boolean> StartServiceInternalAsync(ServiceController controller, IEnumerable<String>? arguments, Int32 milliseconds, Boolean isThrow,
            CancellationToken token)
        {
            if (controller is null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            return StartServiceInternalAsync(controller, arguments, TimeSpan.FromMilliseconds(milliseconds), isThrow, token);
        }

        private static async Task<Boolean> StartServiceInternalAsync(ServiceController controller, IEnumerable<String>? arguments, TimeSpan timeout, Boolean isThrow,
            CancellationToken token)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean StopService(String name)
        {
            return StopService(name, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean StopService(String name, String? machine)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.StopService();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean StopService(String name, Int32 milliseconds)
        {
            return StopService(name, null, milliseconds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean StopService(String name, String? machine, Int32 milliseconds)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.StopService(milliseconds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean StopService(String name, TimeSpan timeout)
        {
            return StopService(name, null, timeout);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean StopService(String name, String? machine, TimeSpan timeout)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.StopService(timeout);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryStopService(String name)
        {
            return TryStopService(name, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryStopService(String name, String? machine)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryStopService() ?? false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryStopService(String name, Int32 milliseconds)
        {
            return TryStopService(name, null, milliseconds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryStopService(String name, String? machine, Int32 milliseconds)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryStopService(milliseconds) ?? false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryStopService(String name, TimeSpan timeout)
        {
            return TryStopService(name, null, timeout);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryStopService(String name, String? machine, TimeSpan timeout)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryStopService(timeout) ?? false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean StopService(this ServiceController controller)
        {
            return StopService(controller, Timeout.InfiniteTimeSpan);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean StopService(this ServiceController controller, Int32 milliseconds)
        {
            return StopServiceInternal(controller, milliseconds, true);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean StopService(this ServiceController controller, TimeSpan timeout)
        {
            return StopServiceInternal(controller, timeout, true);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryStopService(this ServiceController controller)
        {
            return TryStopService(controller, Timeout.InfiniteTimeSpan);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryStopService(this ServiceController controller, Int32 milliseconds)
        {
            return StopServiceInternal(controller, milliseconds, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryStopService(this ServiceController controller, TimeSpan timeout)
        {
            return StopServiceInternal(controller, timeout, false);
        }

        private static Boolean StopServiceInternal(ServiceController controller, Int32 milliseconds, Boolean isThrow)
        {
            if (controller is null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            return StopServiceInternal(controller, TimeSpan.FromMilliseconds(milliseconds), isThrow);
        }

        private static Boolean StopServiceInternal(ServiceController controller, TimeSpan timeout, Boolean isThrow)
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
                        controller.WaitForStatus(ServiceControllerStatus.Running, timeout);
                        goto case ServiceControllerStatus.Running;
                    case ServiceControllerStatus.StopPending:
                        controller.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                        goto case ServiceControllerStatus.Stopped;
                    case ServiceControllerStatus.Running:
                        if (!controller.CanStop)
                        {
                            return false;
                        }

                        controller.Stop();
                        controller.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                        goto case ServiceControllerStatus.Stopped;
                    case ServiceControllerStatus.ContinuePending:
                        controller.WaitForStatus(ServiceControllerStatus.Running, timeout);
                        goto case ServiceControllerStatus.Running;
                    case ServiceControllerStatus.PausePending:
                        controller.WaitForStatus(ServiceControllerStatus.Paused, timeout);
                        goto case ServiceControllerStatus.Paused;
                    case ServiceControllerStatus.Paused:
                        if (!controller.CanStop)
                        {
                            return false;
                        }

                        controller.Stop();
                        controller.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                        goto case ServiceControllerStatus.Stopped;
                    default:
                        throw new NotSupportedException();
                }
            }
            catch (System.ServiceProcess.TimeoutException)
            {
                if (controller.TryWaitForTimeoutAsync(ServiceControllerStatus.Stopped, timeout == Timeout.InfiniteTimeSpan ? Time.Second.Three : timeout).GetAwaiter().GetResult())
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
        public static Task<Boolean> StopServiceAsync(String name)
        {
            return StopServiceAsync(name, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StopServiceAsync(String name, String? machine)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.StopServiceAsync();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StopServiceAsync(String name, CancellationToken token)
        {
            return StopServiceAsync(name, null, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StopServiceAsync(String name, String? machine, CancellationToken token)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.StopServiceAsync(token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StopServiceAsync(String name, Int32 milliseconds)
        {
            return StopServiceAsync(name, null, milliseconds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StopServiceAsync(String name, String? machine, Int32 milliseconds)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.StopServiceAsync(milliseconds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StopServiceAsync(String name, Int32 milliseconds, CancellationToken token)
        {
            return StopServiceAsync(name, null, milliseconds, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StopServiceAsync(String name, String? machine, Int32 milliseconds, CancellationToken token)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.StopServiceAsync(milliseconds, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StopServiceAsync(String name, TimeSpan timeout)
        {
            return StopServiceAsync(name, null, timeout);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StopServiceAsync(String name, String? machine, TimeSpan timeout)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.StopServiceAsync(timeout);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StopServiceAsync(String name, TimeSpan timeout, CancellationToken token)
        {
            return StopServiceAsync(name, null, timeout, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StopServiceAsync(String name, String? machine, TimeSpan timeout, CancellationToken token)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.StopServiceAsync(timeout, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStopServiceAsync(String name)
        {
            return TryStopServiceAsync(name, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStopServiceAsync(String name, String? machine)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryStopServiceAsync() ?? TaskUtils.False;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStopServiceAsync(String name, CancellationToken token)
        {
            return TryStopServiceAsync(name, null, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStopServiceAsync(String name, String? machine, CancellationToken token)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryStopServiceAsync(token) ?? TaskUtils.False;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStopServiceAsync(String name, Int32 milliseconds)
        {
            return TryStopServiceAsync(name, null, milliseconds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStopServiceAsync(String name, String? machine, Int32 milliseconds)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryStopServiceAsync(milliseconds) ?? TaskUtils.False;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStopServiceAsync(String name, Int32 milliseconds, CancellationToken token)
        {
            return TryStopServiceAsync(name, null, milliseconds, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStopServiceAsync(String name, String? machine, Int32 milliseconds, CancellationToken token)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryStopServiceAsync(milliseconds, token) ?? TaskUtils.False;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStopServiceAsync(String name, TimeSpan timeout)
        {
            return TryStopServiceAsync(name, null, timeout);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStopServiceAsync(String name, String? machine, TimeSpan timeout)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryStopServiceAsync(timeout) ?? TaskUtils.False;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStopServiceAsync(String name, TimeSpan timeout, CancellationToken token)
        {
            return TryStopServiceAsync(name, null, timeout, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStopServiceAsync(String name, String? machine, TimeSpan timeout, CancellationToken token)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryStopServiceAsync(timeout, token) ?? TaskUtils.False;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StopServiceAsync(this ServiceController controller)
        {
            return StopServiceAsync(controller, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StopServiceAsync(this ServiceController controller, CancellationToken token)
        {
            return StopServiceAsync(controller, Timeout.InfiniteTimeSpan, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StopServiceAsync(this ServiceController controller, Int32 milliseconds)
        {
            return StopServiceAsync(controller, milliseconds, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StopServiceAsync(this ServiceController controller, Int32 milliseconds, CancellationToken token)
        {
            return StopServiceInternalAsync(controller, milliseconds, true, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StopServiceAsync(this ServiceController controller, TimeSpan timeout)
        {
            return StopServiceAsync(controller, timeout, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> StopServiceAsync(this ServiceController controller, TimeSpan timeout, CancellationToken token)
        {
            return StopServiceInternalAsync(controller, timeout, true, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStopServiceAsync(this ServiceController controller)
        {
            return TryStopServiceAsync(controller, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStopServiceAsync(this ServiceController controller, CancellationToken token)
        {
            return TryStopServiceAsync(controller, Timeout.InfiniteTimeSpan, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStopServiceAsync(this ServiceController controller, Int32 milliseconds)
        {
            return TryStopServiceAsync(controller, milliseconds, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStopServiceAsync(this ServiceController controller, Int32 milliseconds, CancellationToken token)
        {
            return StopServiceInternalAsync(controller, milliseconds, false, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStopServiceAsync(this ServiceController controller, TimeSpan timeout)
        {
            return TryStopServiceAsync(controller, timeout, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryStopServiceAsync(this ServiceController controller, TimeSpan timeout, CancellationToken token)
        {
            return StopServiceInternalAsync(controller, timeout, false, token);
        }

        private static Task<Boolean> StopServiceInternalAsync(ServiceController controller, Int32 milliseconds, Boolean isThrow, CancellationToken token)
        {
            if (controller is null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            return StopServiceInternalAsync(controller, TimeSpan.FromMilliseconds(milliseconds), isThrow, token);
        }

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
            catch (System.TimeoutException)
            {
                if (await controller.TryWaitForTimeoutAsync(ServiceControllerStatus.Stopped, timeout == Timeout.InfiniteTimeSpan ? Time.Second.Three : timeout, token))
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ContinueService(String name)
        {
            return ContinueService(name, (String?) null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ContinueService(String name, String? machine)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.ContinueService();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ContinueService(String name, IEnumerable<String>? arguments)
        {
            return ContinueService(name, null, arguments);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ContinueService(String name, String? machine, IEnumerable<String>? arguments)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.ContinueService(arguments);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ContinueService(String name, Int32 milliseconds)
        {
            return ContinueService(name, (String?) null, milliseconds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ContinueService(String name, String? machine, Int32 milliseconds)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.ContinueService(milliseconds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ContinueService(String name, TimeSpan timeout)
        {
            return ContinueService(name, (String?) null, timeout);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ContinueService(String name, String? machine, TimeSpan timeout)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.ContinueService(timeout);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ContinueService(String name, IEnumerable<String>? arguments, Int32 milliseconds)
        {
            return ContinueService(name, null, arguments, milliseconds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ContinueService(String name, String? machine, IEnumerable<String>? arguments, Int32 milliseconds)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.ContinueService(arguments, milliseconds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ContinueService(String name, IEnumerable<String>? arguments, TimeSpan timeout)
        {
            return ContinueService(name, null, arguments, timeout);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ContinueService(String name, String? machine, IEnumerable<String>? arguments, TimeSpan timeout)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.ContinueService(arguments, timeout);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryContinueService(String name)
        {
            return TryContinueService(name, (String?) null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryContinueService(String name, String? machine)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryContinueService() ?? false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryContinueService(String name, IEnumerable<String>? arguments)
        {
            return TryContinueService(name, null, arguments);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryContinueService(String name, String? machine, IEnumerable<String>? arguments)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryContinueService(arguments) ?? false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryContinueService(String name, Int32 milliseconds)
        {
            return TryContinueService(name, (String?) null, milliseconds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryContinueService(String name, String? machine, Int32 milliseconds)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryContinueService(milliseconds) ?? false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryContinueService(String name, TimeSpan timeout)
        {
            return TryContinueService(name, (String?) null, timeout);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryContinueService(String name, String? machine, TimeSpan timeout)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryContinueService(timeout) ?? false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryContinueService(String name, IEnumerable<String>? arguments, Int32 milliseconds)
        {
            return TryContinueService(name, null, arguments, milliseconds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryContinueService(String name, String? machine, IEnumerable<String>? arguments, Int32 milliseconds)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryContinueService(arguments, milliseconds) ?? false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryContinueService(String name, IEnumerable<String>? arguments, TimeSpan timeout)
        {
            return TryContinueService(name, null, arguments, timeout);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryContinueService(String name, String? machine, IEnumerable<String>? arguments, TimeSpan timeout)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryContinueService(arguments, timeout) ?? false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ContinueService(this ServiceController controller)
        {
            return ContinueService(controller, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ContinueService(this ServiceController controller, IEnumerable<String>? arguments)
        {
            return ContinueService(controller, arguments, Timeout.InfiniteTimeSpan);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ContinueService(this ServiceController controller, Int32 milliseconds)
        {
            return ContinueService(controller, null, milliseconds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ContinueService(this ServiceController controller, TimeSpan timeout)
        {
            return ContinueService(controller, null, timeout);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ContinueService(this ServiceController controller, IEnumerable<String>? arguments, Int32 milliseconds)
        {
            return ContinueServiceInternal(controller, arguments, milliseconds, true);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean ContinueService(this ServiceController controller, IEnumerable<String>? arguments, TimeSpan timeout)
        {
            return ContinueServiceInternal(controller, arguments, timeout, true);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryContinueService(this ServiceController controller)
        {
            return TryContinueService(controller, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryContinueService(this ServiceController controller, IEnumerable<String>? arguments)
        {
            return TryContinueService(controller, arguments, Timeout.InfiniteTimeSpan);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryContinueService(this ServiceController controller, Int32 milliseconds)
        {
            return TryContinueService(controller, null, milliseconds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryContinueService(this ServiceController controller, TimeSpan timeout)
        {
            return TryContinueService(controller, null, timeout);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryContinueService(this ServiceController controller, IEnumerable<String>? arguments, Int32 milliseconds)
        {
            return ContinueServiceInternal(controller, arguments, milliseconds, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryContinueService(this ServiceController controller, IEnumerable<String>? arguments, TimeSpan timeout)
        {
            return ContinueServiceInternal(controller, arguments, timeout, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        public static Task<Boolean> ContinueServiceAsync(String name)
        {
            return ContinueServiceAsync(name, (String?) null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> ContinueServiceAsync(String name, String? machine)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.ContinueServiceAsync();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> ContinueServiceAsync(String name, CancellationToken token)
        {
            return ContinueServiceAsync(name, (String?) null, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> ContinueServiceAsync(String name, String? machine, CancellationToken token)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.ContinueServiceAsync(token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> ContinueServiceAsync(String name, IEnumerable<String>? arguments)
        {
            return ContinueServiceAsync(name, null, arguments);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> ContinueServiceAsync(String name, String? machine, IEnumerable<String>? arguments)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.ContinueServiceAsync(arguments);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> ContinueServiceAsync(String name, IEnumerable<String>? arguments, CancellationToken token)
        {
            return ContinueServiceAsync(name, null, arguments, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> ContinueServiceAsync(String name, String? machine, IEnumerable<String>? arguments, CancellationToken token)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.ContinueServiceAsync(arguments, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> ContinueServiceAsync(String name, Int32 milliseconds)
        {
            return ContinueServiceAsync(name, (String?) null, milliseconds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> ContinueServiceAsync(String name, String? machine, Int32 milliseconds)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.ContinueServiceAsync(milliseconds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> ContinueServiceAsync(String name, Int32 milliseconds, CancellationToken token)
        {
            return ContinueServiceAsync(name, (String?) null, milliseconds, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> ContinueServiceAsync(String name, String? machine, Int32 milliseconds, CancellationToken token)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.ContinueServiceAsync(milliseconds, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> ContinueServiceAsync(String name, TimeSpan timeout)
        {
            return ContinueServiceAsync(name, (String?) null, timeout);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> ContinueServiceAsync(String name, String? machine, TimeSpan timeout)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.ContinueServiceAsync(timeout);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> ContinueServiceAsync(String name, TimeSpan timeout, CancellationToken token)
        {
            return ContinueServiceAsync(name, (String?) null, timeout, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> ContinueServiceAsync(String name, String? machine, TimeSpan timeout, CancellationToken token)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.ContinueServiceAsync(timeout, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> ContinueServiceAsync(String name, IEnumerable<String>? arguments, Int32 milliseconds)
        {
            return ContinueServiceAsync(name, null, arguments, milliseconds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> ContinueServiceAsync(String name, String? machine, IEnumerable<String>? arguments, Int32 milliseconds)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.ContinueServiceAsync(arguments, milliseconds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> ContinueServiceAsync(String name, IEnumerable<String>? arguments, Int32 milliseconds, CancellationToken token)
        {
            return ContinueServiceAsync(name, null, arguments, milliseconds, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> ContinueServiceAsync(String name, String? machine, IEnumerable<String>? arguments, Int32 milliseconds, CancellationToken token)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.ContinueServiceAsync(arguments, milliseconds, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> ContinueServiceAsync(String name, IEnumerable<String>? arguments, TimeSpan timeout)
        {
            return ContinueServiceAsync(name, null, arguments, timeout);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> ContinueServiceAsync(String name, String? machine, IEnumerable<String>? arguments, TimeSpan timeout)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.ContinueServiceAsync(arguments, timeout);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> ContinueServiceAsync(String name, IEnumerable<String>? arguments, TimeSpan timeout, CancellationToken token)
        {
            return ContinueServiceAsync(name, null, arguments, timeout, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> ContinueServiceAsync(String name, String? machine, IEnumerable<String>? arguments, TimeSpan timeout, CancellationToken token)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.ContinueServiceAsync(arguments, timeout, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryContinueServiceAsync(String name)
        {
            return TryContinueServiceAsync(name, (String?) null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryContinueServiceAsync(String name, String? machine)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryContinueServiceAsync() ?? TaskUtils.False;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryContinueServiceAsync(String name, CancellationToken token)
        {
            return TryContinueServiceAsync(name, (String?) null, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryContinueServiceAsync(String name, String? machine, CancellationToken token)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryContinueServiceAsync(token) ?? TaskUtils.False;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryContinueServiceAsync(String name, IEnumerable<String>? arguments)
        {
            return TryContinueServiceAsync(name, null, arguments);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryContinueServiceAsync(String name, String? machine, IEnumerable<String>? arguments)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryContinueServiceAsync(arguments) ?? TaskUtils.False;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryContinueServiceAsync(String name, IEnumerable<String>? arguments, CancellationToken token)
        {
            return TryContinueServiceAsync(name, null, arguments, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryContinueServiceAsync(String name, String? machine, IEnumerable<String>? arguments, CancellationToken token)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryContinueServiceAsync(arguments, token) ?? TaskUtils.False;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryContinueServiceAsync(String name, Int32 milliseconds)
        {
            return TryContinueServiceAsync(name, (String?) null, milliseconds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryContinueServiceAsync(String name, String? machine, Int32 milliseconds)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryContinueServiceAsync(milliseconds) ?? TaskUtils.False;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryContinueServiceAsync(String name, Int32 milliseconds, CancellationToken token)
        {
            return TryContinueServiceAsync(name, (String?) null, milliseconds, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryContinueServiceAsync(String name, String? machine, Int32 milliseconds, CancellationToken token)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryContinueServiceAsync(milliseconds, token) ?? TaskUtils.False;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryContinueServiceAsync(String name, TimeSpan timeout)
        {
            return TryContinueServiceAsync(name, (String?) null, timeout);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryContinueServiceAsync(String name, String? machine, TimeSpan timeout)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryContinueServiceAsync(timeout) ?? TaskUtils.False;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryContinueServiceAsync(String name, TimeSpan timeout, CancellationToken token)
        {
            return TryContinueServiceAsync(name, (String?) null, timeout, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryContinueServiceAsync(String name, String? machine, TimeSpan timeout, CancellationToken token)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryContinueServiceAsync(timeout, token) ?? TaskUtils.False;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryContinueServiceAsync(String name, IEnumerable<String>? arguments, Int32 milliseconds)
        {
            return TryContinueServiceAsync(name, null, arguments, milliseconds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryContinueServiceAsync(String name, String? machine, IEnumerable<String>? arguments, Int32 milliseconds)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryContinueServiceAsync(arguments, milliseconds) ?? TaskUtils.False;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryContinueServiceAsync(String name, IEnumerable<String>? arguments, Int32 milliseconds, CancellationToken token)
        {
            return TryContinueServiceAsync(name, null, arguments, milliseconds, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryContinueServiceAsync(String name, String? machine, IEnumerable<String>? arguments, Int32 milliseconds, CancellationToken token)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryContinueServiceAsync(arguments, milliseconds, token) ?? TaskUtils.False;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryContinueServiceAsync(String name, IEnumerable<String>? arguments, TimeSpan timeout)
        {
            return TryContinueServiceAsync(name, null, arguments, timeout);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryContinueServiceAsync(String name, String? machine, IEnumerable<String>? arguments, TimeSpan timeout)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryContinueServiceAsync(arguments, timeout) ?? TaskUtils.False;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryContinueServiceAsync(String name, IEnumerable<String>? arguments, TimeSpan timeout, CancellationToken token)
        {
            return TryContinueServiceAsync(name, null, arguments, timeout, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryContinueServiceAsync(String name, String? machine, IEnumerable<String>? arguments, TimeSpan timeout, CancellationToken token)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryContinueServiceAsync(arguments, timeout, token) ?? TaskUtils.False;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> ContinueServiceAsync(this ServiceController controller)
        {
            return ContinueServiceAsync(controller, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> ContinueServiceAsync(this ServiceController controller, CancellationToken token)
        {
            return ContinueServiceAsync(controller, null, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> ContinueServiceAsync(this ServiceController controller, IEnumerable<String>? arguments)
        {
            return ContinueServiceAsync(controller, arguments, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> ContinueServiceAsync(this ServiceController controller, IEnumerable<String>? arguments, CancellationToken token)
        {
            return ContinueServiceAsync(controller, arguments, Timeout.InfiniteTimeSpan, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> ContinueServiceAsync(this ServiceController controller, Int32 milliseconds)
        {
            return ContinueServiceAsync(controller, milliseconds, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> ContinueServiceAsync(this ServiceController controller, Int32 milliseconds, CancellationToken token)
        {
            return ContinueServiceAsync(controller, null, milliseconds, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> ContinueServiceAsync(this ServiceController controller, TimeSpan timeout)
        {
            return ContinueServiceAsync(controller, timeout, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> ContinueServiceAsync(this ServiceController controller, TimeSpan timeout, CancellationToken token)
        {
            return ContinueServiceAsync(controller, null, timeout, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> ContinueServiceAsync(this ServiceController controller, IEnumerable<String>? arguments, Int32 milliseconds)
        {
            return ContinueServiceAsync(controller, arguments, milliseconds, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> ContinueServiceAsync(this ServiceController controller, IEnumerable<String>? arguments, Int32 milliseconds, CancellationToken token)
        {
            return ContinueServiceInternalAsync(controller, arguments, milliseconds, true, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> ContinueServiceAsync(this ServiceController controller, IEnumerable<String>? arguments, TimeSpan timeout)
        {
            return ContinueServiceAsync(controller, arguments, timeout, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> ContinueServiceAsync(this ServiceController controller, IEnumerable<String>? arguments, TimeSpan timeout, CancellationToken token)
        {
            return ContinueServiceInternalAsync(controller, arguments, timeout, true, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryContinueServiceAsync(this ServiceController controller)
        {
            return TryContinueServiceAsync(controller, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryContinueServiceAsync(this ServiceController controller, CancellationToken token)
        {
            return TryContinueServiceAsync(controller, null, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryContinueServiceAsync(this ServiceController controller, IEnumerable<String>? arguments)
        {
            return TryContinueServiceAsync(controller, arguments, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryContinueServiceAsync(this ServiceController controller, IEnumerable<String>? arguments, CancellationToken token)
        {
            return TryContinueServiceAsync(controller, arguments, Timeout.InfiniteTimeSpan, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryContinueServiceAsync(this ServiceController controller, Int32 milliseconds)
        {
            return TryContinueServiceAsync(controller, milliseconds, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryContinueServiceAsync(this ServiceController controller, Int32 milliseconds, CancellationToken token)
        {
            return TryContinueServiceAsync(controller, null, milliseconds, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryContinueServiceAsync(this ServiceController controller, TimeSpan timeout)
        {
            return TryContinueServiceAsync(controller, timeout, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryContinueServiceAsync(this ServiceController controller, TimeSpan timeout, CancellationToken token)
        {
            return TryContinueServiceAsync(controller, null, timeout, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryContinueServiceAsync(this ServiceController controller, IEnumerable<String>? arguments, Int32 milliseconds)
        {
            return TryContinueServiceAsync(controller, arguments, milliseconds, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryContinueServiceAsync(this ServiceController controller, IEnumerable<String>? arguments, Int32 milliseconds, CancellationToken token)
        {
            return ContinueServiceInternalAsync(controller, arguments, milliseconds, false, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryContinueServiceAsync(this ServiceController controller, IEnumerable<String>? arguments, TimeSpan timeout)
        {
            return TryContinueServiceAsync(controller, arguments, timeout, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryContinueServiceAsync(this ServiceController controller, IEnumerable<String>? arguments, TimeSpan timeout, CancellationToken token)
        {
            return ContinueServiceInternalAsync(controller, arguments, timeout, false, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Task<Boolean> ContinueServiceInternalAsync(ServiceController controller, IEnumerable<String>? arguments, Int32 milliseconds, Boolean isThrow,
            CancellationToken token)
        {
            if (controller is null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            return ContinueServiceInternalAsync(controller, arguments, TimeSpan.FromMilliseconds(milliseconds), isThrow, token);
        }

        private static async Task<Boolean> ContinueServiceInternalAsync(ServiceController controller, IEnumerable<String>? arguments, TimeSpan timeout, Boolean isThrow,
            CancellationToken token)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean PauseService(String name)
        {
            return PauseService(name, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean PauseService(String name, String? machine)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.PauseService();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean PauseService(String name, Int32 milliseconds)
        {
            return PauseService(name, null, milliseconds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean PauseService(String name, String? machine, Int32 milliseconds)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.PauseService(milliseconds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean PauseService(String name, TimeSpan timeout)
        {
            return PauseService(name, null, timeout);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean PauseService(String name, String? machine, TimeSpan timeout)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.PauseService(timeout);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryPauseService(String name)
        {
            return TryPauseService(name, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryPauseService(String name, String? machine)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryPauseService() ?? false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryPauseService(String name, Int32 milliseconds)
        {
            return TryPauseService(name, null, milliseconds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryPauseService(String name, String? machine, Int32 milliseconds)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryPauseService(milliseconds) ?? false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryPauseService(String name, TimeSpan timeout)
        {
            return TryPauseService(name, null, timeout);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryPauseService(String name, String? machine, TimeSpan timeout)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryPauseService(timeout) ?? false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean PauseService(this ServiceController controller)
        {
            return PauseService(controller, Timeout.InfiniteTimeSpan);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean PauseService(this ServiceController controller, Int32 milliseconds)
        {
            return PauseServiceInternal(controller, milliseconds, true);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean PauseService(this ServiceController controller, TimeSpan timeout)
        {
            return PauseServiceInternal(controller, timeout, true);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryPauseService(this ServiceController controller)
        {
            return TryPauseService(controller, Timeout.InfiniteTimeSpan);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryPauseService(this ServiceController controller, Int32 milliseconds)
        {
            return PauseServiceInternal(controller, milliseconds, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryPauseService(this ServiceController controller, TimeSpan timeout)
        {
            return PauseServiceInternal(controller, timeout, false);
        }

        private static Boolean PauseServiceInternal(ServiceController controller, Int32 milliseconds, Boolean isThrow)
        {
            if (controller is null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            return PauseServiceInternal(controller, TimeSpan.FromMilliseconds(milliseconds), isThrow);
        }

        private static Boolean PauseServiceInternal(ServiceController controller, TimeSpan timeout, Boolean isThrow)
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
                        controller.WaitForStatus(ServiceControllerStatus.Running, timeout);
                        goto case ServiceControllerStatus.Running;
                    case ServiceControllerStatus.StopPending:
                        controller.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                        goto case ServiceControllerStatus.Stopped;
                    case ServiceControllerStatus.Running:
                        if (!controller.CanPauseAndContinue)
                        {
                            return false;
                        }

                        controller.Pause();
                        controller.WaitForStatus(ServiceControllerStatus.Paused, timeout);
                        goto case ServiceControllerStatus.Paused;
                    case ServiceControllerStatus.ContinuePending:
                        controller.WaitForStatus(ServiceControllerStatus.Running, timeout);
                        goto case ServiceControllerStatus.Running;
                    case ServiceControllerStatus.PausePending:
                        controller.WaitForStatus(ServiceControllerStatus.Paused, timeout);
                        goto case ServiceControllerStatus.Paused;
                    case ServiceControllerStatus.Paused:
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
            catch (System.ServiceProcess.TimeoutException)
            {
                if (controller.TryWaitForTimeoutAsync(ServiceControllerStatus.Paused, timeout == Timeout.InfiniteTimeSpan ? Time.Second.Three : timeout).GetAwaiter().GetResult())
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
        public static Task<Boolean> PauseServiceAsync(String name)
        {
            return PauseServiceAsync(name, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> PauseServiceAsync(String name, String? machine)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.PauseServiceAsync();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> PauseServiceAsync(String name, CancellationToken token)
        {
            return PauseServiceAsync(name, null, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> PauseServiceAsync(String name, String? machine, CancellationToken token)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.PauseServiceAsync(token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> PauseServiceAsync(String name, Int32 milliseconds)
        {
            return PauseServiceAsync(name, null, milliseconds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> PauseServiceAsync(String name, String? machine, Int32 milliseconds)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.PauseServiceAsync(milliseconds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> PauseServiceAsync(String name, Int32 milliseconds, CancellationToken token)
        {
            return PauseServiceAsync(name, null, milliseconds, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> PauseServiceAsync(String name, String? machine, Int32 milliseconds, CancellationToken token)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.PauseServiceAsync(milliseconds, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> PauseServiceAsync(String name, TimeSpan timeout)
        {
            return PauseServiceAsync(name, null, timeout);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> PauseServiceAsync(String name, String? machine, TimeSpan timeout)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.PauseServiceAsync(timeout);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> PauseServiceAsync(String name, TimeSpan timeout, CancellationToken token)
        {
            return PauseServiceAsync(name, null, timeout, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> PauseServiceAsync(String name, String? machine, TimeSpan timeout, CancellationToken token)
        {
            using ServiceController controller = CreateServiceController(name, machine);
            return controller.PauseServiceAsync(timeout, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryPauseServiceAsync(String name)
        {
            return TryPauseServiceAsync(name, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryPauseServiceAsync(String name, String? machine)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryPauseServiceAsync() ?? TaskUtils.False;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryPauseServiceAsync(String name, CancellationToken token)
        {
            return TryPauseServiceAsync(name, null, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryPauseServiceAsync(String name, String? machine, CancellationToken token)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryPauseServiceAsync(token) ?? TaskUtils.False;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryPauseServiceAsync(String name, Int32 milliseconds)
        {
            return TryPauseServiceAsync(name, null, milliseconds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryPauseServiceAsync(String name, String? machine, Int32 milliseconds)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryPauseServiceAsync(milliseconds) ?? TaskUtils.False;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryPauseServiceAsync(String name, Int32 milliseconds, CancellationToken token)
        {
            return TryPauseServiceAsync(name, null, milliseconds, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryPauseServiceAsync(String name, String? machine, Int32 milliseconds, CancellationToken token)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryPauseServiceAsync(milliseconds, token) ?? TaskUtils.False;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryPauseServiceAsync(String name, TimeSpan timeout)
        {
            return TryPauseServiceAsync(name, null, timeout);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryPauseServiceAsync(String name, String? machine, TimeSpan timeout)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryPauseServiceAsync(timeout) ?? TaskUtils.False;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryPauseServiceAsync(String name, TimeSpan timeout, CancellationToken token)
        {
            return TryPauseServiceAsync(name, null, timeout, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryPauseServiceAsync(String name, String? machine, TimeSpan timeout, CancellationToken token)
        {
            using ServiceController? controller = TryCreateServiceController(name, machine);
            return controller?.TryPauseServiceAsync(timeout, token) ?? TaskUtils.False;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> PauseServiceAsync(this ServiceController controller)
        {
            return PauseServiceAsync(controller, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> PauseServiceAsync(this ServiceController controller, CancellationToken token)
        {
            return PauseServiceAsync(controller, Timeout.InfiniteTimeSpan, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> PauseServiceAsync(this ServiceController controller, Int32 milliseconds)
        {
            return PauseServiceAsync(controller, milliseconds, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> PauseServiceAsync(this ServiceController controller, Int32 milliseconds, CancellationToken token)
        {
            return PauseServiceInternalAsync(controller, milliseconds, true, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> PauseServiceAsync(this ServiceController controller, TimeSpan timeout)
        {
            return PauseServiceAsync(controller, timeout, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> PauseServiceAsync(this ServiceController controller, TimeSpan timeout, CancellationToken token)
        {
            return PauseServiceInternalAsync(controller, timeout, true, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryPauseServiceAsync(this ServiceController controller)
        {
            return TryPauseServiceAsync(controller, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryPauseServiceAsync(this ServiceController controller, CancellationToken token)
        {
            return TryPauseServiceAsync(controller, Timeout.InfiniteTimeSpan, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryPauseServiceAsync(this ServiceController controller, Int32 milliseconds)
        {
            return TryPauseServiceAsync(controller, milliseconds, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryPauseServiceAsync(this ServiceController controller, Int32 milliseconds, CancellationToken token)
        {
            return PauseServiceInternalAsync(controller, milliseconds, false, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryPauseServiceAsync(this ServiceController controller, TimeSpan timeout)
        {
            return TryPauseServiceAsync(controller, timeout, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> TryPauseServiceAsync(this ServiceController controller, TimeSpan timeout, CancellationToken token)
        {
            return PauseServiceInternalAsync(controller, timeout, false, token);
        }

        private static Task<Boolean> PauseServiceInternalAsync(ServiceController controller, Int32 milliseconds, Boolean isThrow, CancellationToken token)
        {
            if (controller is null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            return PauseServiceInternalAsync(controller, TimeSpan.FromMilliseconds(milliseconds), isThrow, token);
        }

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

        public static Boolean CheckServiceExist(String name)
        {
            return IsServiceExistInternal(name, true);
        }

        public static Boolean IsServiceExist(String name)
        {
            return IsServiceExistInternal(name, false);
        }
        
        public static Boolean CheckServiceExist(this Types.Installers.WindowsServiceInstaller installer)
        {
            if (installer is null)
            {
                throw new ArgumentNullException(nameof(installer));
            }

            return IsServiceExistInternal(installer.Name, true);
        }

        public static Boolean IsServiceExist(this Types.Installers.WindowsServiceInstaller installer)
        {
            if (installer is null)
            {
                throw new ArgumentNullException(nameof(installer));
            }

            return IsServiceExistInternal(installer.Name, false);
        }

        private static Boolean IsServiceExistInternal(String name, Boolean isThrow)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (!IsValidServiceName(name))
            {
                throw new ArgumentException("Service name is invalid.", nameof(name));
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

        public static Boolean InstallService(this Types.Installers.WindowsServiceInstaller installer)
        {
            if (installer is null)
            {
                throw new ArgumentNullException(nameof(installer));
            }

            return InstallServiceInternal(installer.Path, installer.Name, installer.DisplayName, installer.Description, installer.ServiceType, installer.ServiceStartMode, installer.AutoStart, installer.Dependency, installer.Username, installer.Password, true);
        }
        
        public static Boolean TryInstallService(this Types.Installers.WindowsServiceInstaller installer)
        {
            if (installer is null)
            {
                throw new ArgumentNullException(nameof(installer));
            }

            return InstallServiceInternal(installer.Path, installer.Name, installer.DisplayName, installer.Description, installer.ServiceType, installer.ServiceStartMode, installer.AutoStart, installer.Dependency, installer.Username, installer.Password, false);
        }

        private static Boolean InstallServiceInternal(FileInfo info, String name, String? displayname, String? description, ServiceType type, ServiceStartMode mode, Boolean autostart,
            IEnumerable<ServiceController?>? dependency, String? username, String? password, Boolean isThrow)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (!IsValidServiceName(name))
            {
                throw new ArgumentException("Service name is invalid.", nameof(name));
            }

            if (String.IsNullOrEmpty(displayname))
            {
                displayname = name;
            }

            if (!info.Exists)
            {
                if (isThrow)
                {
                    throw new ArgumentException($"File '{info.FullName}' doesn't exist.", nameof(info));
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
                983551, (Int32) type, (Int32) mode, 1,
                path, null, IntPtr.Zero, dependency.ConvertToDependencyString(), username, password);

            if (service == IntPtr.Zero)
            {
                CloseServiceHandle(manager);

                if (isThrow)
                {
                    throw new Win32Exception(GetLastError());
                }

                return false;
            }

            try
            {
                if (Environment.OSVersion.Version.Major > 5 && mode == ServiceStartMode.Automatic)
                {
                    ServiceDelayedAutostartInfo autoinfo = new ServiceDelayedAutostartInfo(autostart);
                    if (!ChangeServiceConfig2(service, 3U, ref autoinfo))
                    {
                        if (isThrow)
                        {
                            throw new Win32Exception(GetLastError());
                        }

                        return false;
                    }
                }
            }
            catch (InvalidOperationException)
            {
            }

            if (!String.IsNullOrEmpty(description))
            {
                ServiceDescription descriptioninfo = new ServiceDescription(description);
                try
                {
                    if (!ChangeServiceConfig2(service, 1U, ref descriptioninfo))
                    {
                        if (isThrow)
                        {
                            throw new Win32Exception(GetLastError());
                        }

                        return false;
                    }
                }
                finally
                {
                    descriptioninfo.Dispose();
                }
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
        
        /// <summary>
        /// This method uninstalls the service from the service control manager.
        /// </summary>
        /// <param name="installer">Installer of the service.</param>
        public static Boolean UninstallService(this Types.Installers.WindowsServiceInstaller installer)
        {
            if (installer is null)
            {
                throw new ArgumentNullException(nameof(installer));
            }

            return UninstallServiceInternal(installer.Name, true);
        }

        /// <summary>
        /// This method uninstalls the service from the service control manager.
        /// </summary>
        /// <param name="installer">Installer of the service.</param>
        public static Boolean TryUninstallService(this Types.Installers.WindowsServiceInstaller installer)
        {
            if (installer is null)
            {
                throw new ArgumentNullException(nameof(installer));
            }

            return UninstallServiceInternal(installer.Name, false);
        }

        private static Boolean UninstallServiceInternal(String name, Boolean isThrow)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (!IsValidServiceName(name))
            {
                throw new ArgumentException("Service name is invalid.", nameof(name));
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
        
        public static Boolean ReinstallService(this Types.Installers.WindowsServiceInstaller installer)
        {
            if (installer is null)
            {
                throw new ArgumentNullException(nameof(installer));
            }

            return ReinstallServiceInternal(installer.Path, installer.Name, installer.DisplayName, installer.Description, installer.ServiceType, installer.ServiceStartMode, installer.AutoStart, installer.Dependency, installer.Username, installer.Password, true);
        }
        
        public static Boolean TryReinstallService(this Types.Installers.WindowsServiceInstaller installer)
        {
            if (installer is null)
            {
                throw new ArgumentNullException(nameof(installer));
            }

            return ReinstallServiceInternal(installer.Path, installer.Name, installer.DisplayName, installer.Description, installer.ServiceType, installer.ServiceStartMode, installer.AutoStart, installer.Dependency, installer.Username, installer.Password, false);
        }

        private static Boolean ReinstallServiceInternal(FileInfo info, String name, String? displayname, String? description, ServiceType type, ServiceStartMode mode, Boolean autostart,
            IEnumerable<ServiceController?>? dependency, String? username, String? password, Boolean isThrow)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (!IsValidServiceName(name))
            {
                throw new ArgumentException("Service name is invalid.", nameof(name));
            }

            if (!info.Exists)
            {
                if (isThrow)
                {
                    throw new ArgumentException($"File '{info.FullName}' doesn't exist.", nameof(info));
                }

                return false;
            }

            if (IsServiceExist(name))
            {
                TryUninstallService(name);
            }

            return InstallServiceInternal(info, name, displayname, description, type, mode, autostart, dependency, username, password, isThrow);
        }

        public static Boolean InstallServiceIfNotExist(this Types.Installers.WindowsServiceInstaller installer)
        {
            if (installer is null)
            {
                throw new ArgumentNullException(nameof(installer));
            }

            return InstallServiceIfNotExistInternal(installer.Path, installer.Name, installer.DisplayName, installer.Description, installer.ServiceType, installer.ServiceStartMode, installer.AutoStart, installer.Dependency, installer.Username, installer.Password, true);
        }
        
        public static Boolean TryInstallServiceIfNotExist(this Types.Installers.WindowsServiceInstaller installer)
        {
            if (installer is null)
            {
                throw new ArgumentNullException(nameof(installer));
            }

            return InstallServiceIfNotExistInternal(installer.Path, installer.Name, installer.DisplayName, installer.Description, installer.ServiceType, installer.ServiceStartMode, installer.AutoStart, installer.Dependency, installer.Username, installer.Password, false);
        }

        private static Boolean InstallServiceIfNotExistInternal(FileInfo info, String name, String? displayname, String? description, ServiceType type, ServiceStartMode mode, Boolean autostart,
            IEnumerable<ServiceController?>? dependency, String? username, String? password, Boolean isThrow)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (!IsValidServiceName(name))
            {
                throw new ArgumentException("Service name is invalid.", nameof(name));
            }

            if (info.Exists)
            {
                return IsServiceExist(name) || InstallServiceInternal(info, name, displayname, description, type, mode, autostart, dependency, username, password, isThrow);
            }

            if (isThrow)
            {
                throw new ArgumentException($"File '{info.FullName}' doesn't exist.", nameof(info));
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

        private static async Task<Boolean> WaitForTimeoutInternalAsync(this ServiceController controller, ServiceControllerStatus status, TimeSpan timeout, Boolean isThrow,
            CancellationToken token)
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