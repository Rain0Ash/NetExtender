// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using System.IO;
using System.ServiceProcess;
using NetExtender.Windows.Services.Utils;

namespace NetExtender.Windows.Services.Types.Installers
{
    public record WindowsServiceInstaller
    {
        public FileInfo Path { get; }
        public String Name { get; }
        public String DisplayName { get; }
        public String? Description { get; init; }
        public ServiceType ServiceType { get; init; } = ServiceType.Win32OwnProcess;
        public ServiceStartMode ServiceStartMode { get; init; } = ServiceStartMode.Automatic;
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
                    "NT AUTHORITY\\LocalService" => ServiceAccount.LocalService,
                    "NT AUTHORITY\\NetworkService" => ServiceAccount.NetworkService,
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
                throw new ArgumentException($"File '{path.FullName}' doesn't exist.");
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