// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Net;

namespace NetExtender.Network.Networking.Common.Interfaces
{
    public interface INetworkConnect : INetwork
    {
        /// <summary>
        ///     IP endpoint
        /// </summary>
        public IPEndPoint Endpoint { get; }
    }
}