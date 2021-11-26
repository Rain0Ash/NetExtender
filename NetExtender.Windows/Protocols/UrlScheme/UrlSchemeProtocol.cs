// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using Microsoft.Win32;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.Application;
using NetExtender.Utilities.IO;

namespace NetExtender.Windows.Protocols
{
    public class UrlSchemeProtocol : ProtocolRegistration
    {
        private const String ShellSubKey = @"shell\open\command";

        private const String URLProtocol = "URL Protocol";

        private const String DefaultIcon = "DefaultIcon";

        public String Path { get; }
        
        private String IconPath
        {
            get
            {
                return $"\"{Path}\",1";
            }
        }

        private String CommandPath
        {
            get
            {
                return $"\"{Path}\" \"%1\"";
            }
        }
        
        private String URLApplicationName
        {
            get
            {
                return $"\"URL:{Name} Protocol\"";
            }
        }

        public override ProtocolStatus Status
        {
            get
            {
                return IsRegisterInternal();
            }
        }

        public UrlSchemeProtocol()
            : this(ApplicationUtilities.Path ?? throw new InitializeException(), ApplicationUtilities.FriendlyName ?? throw new InitializeException())
        {
        }

        public UrlSchemeProtocol(String path, String name)
            : base(name)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(path));
            }

            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(@"Value cannot be null or whitespace.", nameof(name));
            }

            if (!PathUtilities.IsExistAsFile(path))
            {
                throw new FileNotFoundException(null, nameof(path));
            }
            
            Path = path;
        }

        protected virtual ProtocolStatus IsRegisterInternal()
        {
            try
            {
                using RegistryKey? registry = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(Name);

                if (registry is null)
                {
                    return ProtocolStatus.Unregister;
                }

                if (String.IsNullOrEmpty(registry.GetValue(null)?.ToString()))
                {
                    return ProtocolStatus.Unknown;
                }

                if (registry.GetValue(URLProtocol)?.ToString() != String.Empty)
                {
                    return ProtocolStatus.Unknown;
                }

                using RegistryKey? icon = registry.OpenSubKey(DefaultIcon);

                if (icon is null)
                {
                    return ProtocolStatus.Unknown;
                }

                if (icon.GetValue(null)?.ToString() != IconPath)
                {
                    return ProtocolStatus.Unknown;
                }

                using RegistryKey? shell = registry.OpenSubKey(ShellSubKey);

                if (shell is null)
                {
                    return ProtocolStatus.Unknown;
                }

                if (shell.GetValue(null)?.ToString() != CommandPath)
                {
                    return ProtocolStatus.Unknown;
                }

                return ProtocolStatus.Register;
            }
            catch (Exception)
            {
                return ProtocolStatus.Unknown;
            }
        }

        public override Boolean Register()
        {
            return Register(URLApplicationName);
        }

        public virtual Boolean Register(String about)
        {
            if (IsRegister)
            {
                return true;
            }

            about ??= String.Empty;

            try
            {
                using RegistryKey? registry = Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(Name);

                if (registry is null!)
                {
                    return false;
                }

                registry.SetValue(null, about);

                registry.SetValue(URLProtocol, String.Empty);

                using RegistryKey? icon = registry.CreateSubKey(DefaultIcon);

                if (icon is null!)
                {
                    return false;
                }

                icon.SetValue(null, IconPath);

                using RegistryKey? shell = registry.CreateSubKey(ShellSubKey);
                
                if (shell is null!)
                {
                    return false;
                }

                shell.SetValue(null, CommandPath);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override Boolean Unregister()
        {
            if (Status == ProtocolStatus.Unregister)
            {
                return true;
            }

            try
            {
                Microsoft.Win32.Registry.ClassesRoot.DeleteSubKeyTree(Name);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}