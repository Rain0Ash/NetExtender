// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.Types.Intercept.Interfaces;

namespace NetExtender.Types.Network.Interfaces
{
    public interface IHttpInterceptClient : IIntercept<HttpInterceptClient, HttpInterceptEventArgs>
    {
        public new event HttpInterceptEventHandler? Intercept;
        public event HttpInterceptEventHandler? SendingRequest;
        public event HttpInterceptEventHandler? ResponseReceived;
    }
}