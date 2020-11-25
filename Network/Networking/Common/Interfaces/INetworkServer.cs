// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Network.Networking.Common.Interfaces
{
    public interface INetworkServer : INetworkConnect
    {
        /// <summary>
        ///     Option: dual mode socket
        /// </summary>
        /// <remarks>
        ///     Specifies whether the Socket is a dual-mode socket used for both IPv4 and IPv6.
        ///     Will work only if socket is bound on IPv6 address.
        /// </remarks>
        public Boolean OptionDualMode { get; set; }

        /// <summary>
        ///     Option: reuse address
        /// </summary>
        /// <remarks>
        ///     This option will enable/disable SO_REUSEADDR if the OS support this feature
        /// </remarks>
        public Boolean OptionReuseAddress { get; set; }

        /// <summary>
        ///     Option: enables a socket to be bound for exclusive access
        /// </summary>
        /// <remarks>
        ///     This option will enable/disable SO_EXCLUSIVEADDRUSE if the OS support this feature
        /// </remarks>
        public Boolean OptionExclusiveAddressUse { get; set; }

        /// <summary>
        ///     Is the server started?
        /// </summary>
        public Boolean IsStarted { get; }
        
        public Boolean Start();
        public Boolean Stop();

        /// <summary>
        ///     Restart the server
        /// </summary>
        /// <returns>'true' if the server was successfully restarted, 'false' if the server failed to restart</returns>
        public Boolean Restart();
    }
}