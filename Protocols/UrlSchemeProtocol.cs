// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using JetBrains.Annotations;
using Microsoft.Win32;
using NetExtender.Apps.Domains;
using NetExtender.Protocols.Interfaces;

namespace NetExtender.Protocols
{
    public class UrlSchemeProtocol : IUrlSchemeProtocol
    {
        public String Name { get; }

        private String URLAppName
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
                return RegisterStatus == true;
            }
            set
            {
                if (value)
                {
                    if (RegisterStatus == true)
                    {
                        return;
                    }

                    Register();
                    return;
                }

                if (RegisterStatus == false)
                {
                    return;
                }

                Unregister();
            }
        }

        public virtual Boolean? RegisterStatus
        {
            get
            {
                return IsRegisterInternal();
            }
        }

        private static String Path
        {
            get
            {
                return Domain.Path;
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
            : this(Domain.AppNameOrFriendlyName)
        {
        }

        public UrlSchemeProtocol([NotNull] String name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(@"Value cannot be null or whitespace.", nameof(name));
            }

            Name = name;
        }

        protected virtual Boolean? IsRegisterInternal()
        {
            try
            {
                if (String.IsNullOrEmpty(Path))
                {
                    return null;
                }

                using RegistryKey reg = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(Name);

                if (reg is null)
                {
                    return false;
                }

                if (String.IsNullOrEmpty(reg.GetValue(null)?.ToString()))
                {
                    return null;
                }

                if (reg.GetValue(URLProtocol)?.ToString() != String.Empty)
                {
                    return null;
                }

                using RegistryKey icon = reg.OpenSubKey(DefaultIcon);

                if (icon is null)
                {
                    return null;
                }

                if (icon.GetValue(null)?.ToString() != IconPath)
                {
                    return null;
                }

                using RegistryKey shell = reg.OpenSubKey(ShellSubKey);

                if (shell is null)
                {
                    return null;
                }

                if (shell.GetValue(null)?.ToString() != CommandPath)
                {
                    return null;
                }

                return true;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public virtual Boolean Register()
        {
            return Register(URLAppName);
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
                using RegistryKey reg = Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(Name);

                if (reg is null)
                {
                    return false;
                }

                reg.SetValue(null, about);

                reg.SetValue(URLProtocol, String.Empty);

                using RegistryKey icon = reg.CreateSubKey(DefaultIcon);

                if (icon is null)
                {
                    return false;
                }

                icon.SetValue(null, IconPath);

                using RegistryKey shell = reg.CreateSubKey(ShellSubKey);

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
            if (RegisterStatus == false)
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