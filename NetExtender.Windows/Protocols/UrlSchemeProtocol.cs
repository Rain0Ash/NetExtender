// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.Win32;
using NetExtender.Exceptions;
using NetExtender.Utils.Application;
using NetExtender.Windows.Protocols.Interfaces;

namespace NetExtender.Windows.Protocols
{
    public class UrlSchemeProtocol : IProtocol
    {
        public String Name { get; }

        private String URLApplicationName
        {
            get
            {
                return $"\"URL:{Name} Protocol\"";
            }
        }

        public virtual Boolean IsRegister
        {
            get
            {
                return Status == ProtocolStatus.Register;
            }
            set
            {
                if (value)
                {
                    if (Status == ProtocolStatus.Register)
                    {
                        return;
                    }

                    Register();
                    return;
                }

                if (Status == ProtocolStatus.Unregister)
                {
                    return;
                }

                Unregister();
            }
        }

        public virtual ProtocolStatus Status
        {
            get
            {
                return IsRegisterInternal();
            }
        }

        private static String? Path
        {
            get
            {
                return ApplicationUtils.Path;
            }
        }

        private const String ShellSubKey = "shell\\open\\command";

        private const String URLProtocol = "URL Protocol";

        private const String DefaultIcon = "DefaultIcon";

        private static String IconPath
        {
            get
            {
                return $"\"{Path}\",1";
            }
        }

        private static String CommandPath
        {
            get
            {
                return $"\"{Path}\" \"%1\"";
            }
        }

        public UrlSchemeProtocol()
            : this(ApplicationUtils.FriendlyName ?? throw new InitializeException())
        {
        }

        public UrlSchemeProtocol(String name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(@"Value cannot be null or whitespace.", nameof(name));
            }

            Name = name;
        }

        protected virtual ProtocolStatus IsRegisterInternal()
        {
            try
            {
                if (String.IsNullOrEmpty(Path))
                {
                    return ProtocolStatus.Unknown;
                }

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

        public virtual Boolean Register()
        {
            return Register(URLApplicationName);
        }

        public virtual Boolean Register(String about)
        {
            if (IsRegister)
            {
                return true;
            }

            if (String.IsNullOrEmpty(Path))
            {
                return false;
            }

            about ??= String.Empty;

            try
            {
                using RegistryKey? registry = Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(Name);

                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                if (registry is null)
                {
                    return false;
                }

                registry.SetValue(null, about);

                registry.SetValue(URLProtocol, String.Empty);

                using RegistryKey? icon = registry.CreateSubKey(DefaultIcon);

                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                if (icon is null)
                {
                    return false;
                }

                icon.SetValue(null, IconPath);

                using RegistryKey? shell = registry.CreateSubKey(ShellSubKey);

                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                if (shell is null)
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

        public virtual Boolean Unregister()
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