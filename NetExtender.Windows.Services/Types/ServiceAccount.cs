// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace NetExtender.Windows.Services.Types
{
    /// <summary>Specifies a service's security context, which defines its logon type.</summary>
    public enum ServiceAccount
    {
        /// <summary>An account that acts as a non-privileged user on the local computer, and presents anonymous credentials to any remote server.</summary>
        LocalService,

        /// <summary>An account that provides extensive local privileges, and presents the computer's credentials to any remote server.</summary>
        NetworkService,

        /// <summary>An account, used by the service control manager, that has extensive privileges on the local computer and acts as the computer on the network.</summary>
        LocalSystem,

        /// <summary>An account defined by a specific user on the network. Specifying <see langword="User" /> for the <see cref="P:System.ServiceProcess.ServiceProcessInstaller.Account" /> member causes the system to prompt for a valid user name and password when the service is installed, unless you set values for both the <see cref="P:System.ServiceProcess.ServiceProcessInstaller.Username" /> and <see cref="P:System.ServiceProcess.ServiceProcessInstaller.Password" /> properties of your <see cref="T:System.ServiceProcess.ServiceProcessInstaller" /> instance.</summary>
        User,
    }
}