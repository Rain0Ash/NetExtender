// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Security;
using System.Security.AccessControl;
using Microsoft.Win32;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Protocols;
using NetExtender.Utilities.Application;
using NetExtender.Utilities.IO;

namespace NetExtender.Windows.Protocols
{
    public class AutorunProtocol : ProtocolRegistration
    {
        private const String ShellSubKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";

        public String Path { get; }

        public override ProtocolStatus Status
        {
            get
            {
                try
                {
                    if (String.IsNullOrEmpty(Name))
                    {
                        return ProtocolStatus.Unknown;
                    }

                    using RegistryKey? registry = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(ShellSubKey);

                    String? path = registry?.GetValue(Name)?.ToString();

                    return path switch
                    {
                        null => ProtocolStatus.Unregister,
                        "" => ProtocolStatus.Error,
                        _ => path == Path ? ProtocolStatus.Register : ProtocolStatus.Another
                    };
                }
                catch (SecurityException)
                {
                    return ProtocolStatus.Unknown;
                }
                catch (Exception)
                {
                    return ProtocolStatus.Error;
                }
            }
        }

        public AutorunProtocol()
            : this(ApplicationUtilities.Path ?? throw new InitializeException(), ApplicationUtilities.FriendlyName ?? throw new InitializeException())
        {
        }

        public AutorunProtocol(String path, String name)
            : base(name)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentNullOrEmptyStringException(path, nameof(path));
            }

            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullOrWhiteSpaceStringException(name, nameof(name));
            }

            if (!PathUtilities.IsExistAsFile(path))
            {
                throw new FileNotFoundException(null, nameof(path));
            }

            Path = path;
        }

        public override Boolean Register()
        {
            if (IsRegister)
            {
                return true;
            }

            try
            {
                using RegistryKey? registry = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(ShellSubKey, true);

                if (registry is null)
                {
                    return false;
                }

                registry.SetValue(Name, Path);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override Boolean Unregister(Boolean force)
        {
            if (Status == ProtocolStatus.Unregister)
            {
                return true;
            }

            if (!force && IsAnother)
            {
                return false;
            }

            try
            {
                using RegistryKey? registry = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(ShellSubKey, RegistryRights.Delete);

                if (registry is null)
                {
                    return false;
                }

                registry.DeleteValue(Name);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}