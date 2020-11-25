// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.Win32;
using NetExtender.Apps.Data.Interfaces;
using NetExtender.Apps.Domains;

namespace NetExtender.Protocols
{
    public enum RegisterStatus
    {
        None,
        Unknown,
        Registered
    }

    internal class URLSchemeProtocol
    {
        private readonly IAppDataEx _data;

        public URLSchemeProtocol(IAppDataEx data)
        {
            if (String.IsNullOrWhiteSpace(data.AppName) || String.IsNullOrWhiteSpace(data.AppShortName))
            {
                throw new ArgumentException("Not null or whitespace app name");
            }

            _data = data;
        }

        public Boolean IsRegister
        {
            get
            {
                return RegisterStatus == RegisterStatus.Registered;
            }
        }

        public RegisterStatus RegisterStatus
        {
            get
            {
                try
                {
                    if (String.IsNullOrEmpty(Path))
                    {
                        return RegisterStatus.Unknown;
                    }

                    using RegistryKey reg = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(Name);

                    if (reg is null)
                    {
                        return RegisterStatus.None;
                    }

                    if (String.IsNullOrEmpty(reg.GetValue(null)?.ToString()))
                    {
                        return RegisterStatus.Unknown;
                    }

                    if (reg.GetValue(URLProtocol)?.ToString() != String.Empty)
                    {
                        return RegisterStatus.Unknown;
                    }

                    using RegistryKey icon = reg.OpenSubKey(DefaultIcon);

                    if (icon is null)
                    {
                        return RegisterStatus.Unknown;
                    }

                    if (icon.GetValue(null)?.ToString() != IconPath)
                    {
                        return RegisterStatus.Unknown;
                    }

                    using RegistryKey shell = reg.OpenSubKey(ShellSubKey);

                    if (shell is null)
                    {
                        return RegisterStatus.Unknown;
                    }

                    if (shell.GetValue(null)?.ToString() != CommandPath)
                    {
                        return RegisterStatus.Unknown;
                    }

                    return RegisterStatus.Registered;
                }
                catch (Exception)
                {
                    return RegisterStatus.Unknown;
                }
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

        private String AppName
        {
            get
            {
                return _data.AppName;
            }
        }

        public String Name
        {
            get
            {
                return _data.AppShortName;
            }
        }

        private String URLAppName
        {
            get
            {
                return $"\"URL:{AppName} Protocol\"";
            }
        }

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

        public Boolean Register()
        {
            return Register(URLAppName);
        }

        public Boolean Register(String about)
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

        public Boolean Unregister()
        {
            if (RegisterStatus == RegisterStatus.None)
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