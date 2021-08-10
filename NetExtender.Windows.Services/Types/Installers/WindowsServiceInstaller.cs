// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using System.IO;
using System.ServiceProcess;
using NetExtender.Windows.Services.Exceptions;
using NetExtender.Windows.Services.Utils;

namespace NetExtender.Windows.Services.Types.Installers
{
    public record WindowsServiceInstaller
    {
        public static WindowsServiceInstaller Open(String name)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (!WindowsServiceUtils.IsValidServiceName(name))
            {
                throw new ArgumentException("Service name is invalid.", nameof(name));
            }

            if (!WindowsServiceUtils.CheckServiceExist(name))
            {
                throw new WindowsServiceNotFoundException($"Windows service with name '{name}' not exist.", name);
            }

            using ServiceController controller = new ServiceController(name);

            String? path = controller.GetServicePath();

            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            return new WindowsServiceInstaller(path, controller.ServiceName, controller.DisplayName)
            {
                Description = controller.TryGetServiceDescription(),
                ServiceType = controller.ServiceType,
                StartMode = controller.StartType,
                ErrorControl = controller.TryGetServiceErrorControl(),
                AutoStart = controller.IsServiceDelayedAutostart(),
                Dependency = controller.DependentServices.ToImmutableArray(),
                Username = controller.TryGetServiceUsername()
            };
        }
        
        public FileInfo Path { get; }
        public String Name { get; }
        public String DisplayName { get; }
        public String? Description { get; init; }
        public ServiceType ServiceType { get; init; } = ServiceType.Win32OwnProcess;
        public ServiceStartMode StartMode { get; init; } = ServiceStartMode.Automatic;
        public ServiceErrorControl ErrorControl { get; init; } = ServiceErrorControl.Normal;
        public Boolean AutoStart { get; init; }
        public ImmutableArray<ServiceController>? Dependency { get; init; }
        public String? Username { get; init; }
        public String? Password { get; init; }

        public ServiceAccount? Account
        {
            get
            {
                return Username switch
                {
                    null => null,
                    "" => null,
                    "LocalService" => ServiceAccount.LocalService,
                    "NT AUTHORITY\\LocalService" => ServiceAccount.LocalService,
                    "NetworkService" => ServiceAccount.NetworkService,
                    "NT AUTHORITY\\NetworkService" => ServiceAccount.NetworkService,
                    "LocalSystem" => ServiceAccount.LocalSystem,
                    "NT AUTHORITY\\LocalSystem" => ServiceAccount.LocalSystem,
                    _ => ServiceAccount.User
                };
            }
            init
            {
                if (value is null)
                {
                    Username = null;
                    Password = null;
                    return;
                }
                
                Username = value switch
                {
                    ServiceAccount.LocalService => "NT AUTHORITY\\LocalService",
                    ServiceAccount.NetworkService => "NT AUTHORITY\\NetworkService",
                    ServiceAccount.LocalSystem => "NT AUTHORITY\\LocalSystem",
                    ServiceAccount.User => null,
                    _ => throw new NotSupportedException()
                };
            }
        }

        public WindowsServiceInstaller(String path, String name)
            : this(path, name, name)
        {
        }

        public WindowsServiceInstaller(String path, String name, String? displayname)
            : this(new FileInfo(path), name, displayname)
        {
        }

        public WindowsServiceInstaller(FileInfo path, String name)
            : this(path, name, name)
        {
        }

        public WindowsServiceInstaller(FileInfo path, String name, String? displayname)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (!path.Exists)
            {
                throw new FileNotFoundException("File doesn't exist.", path.FullName);
            }

            if (!WindowsServiceUtils.IsValidServiceName(name))
            {
                throw new ArgumentException("Service name is invalid.", nameof(name));
            }

            Path = path;
            Name = name;
            DisplayName = !String.IsNullOrEmpty(displayname) ? displayname : name;
        }
    }
}