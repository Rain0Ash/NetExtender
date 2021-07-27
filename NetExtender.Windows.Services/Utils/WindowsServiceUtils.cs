// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;

namespace NetExtender.Windows.Services.Utils
{
    public static class WindowsServiceUtils
    {
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
        
        public static Boolean CheckServiceExist(String name)
        {
            return IsServiceExistInternal(name, true);
        }
        
        public static Boolean IsServiceExist(String name)
        {
            return IsServiceExistInternal(name, false);
        }

        public static Boolean IsServiceExistInternal(String name, Boolean isThrow)
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
    }
}