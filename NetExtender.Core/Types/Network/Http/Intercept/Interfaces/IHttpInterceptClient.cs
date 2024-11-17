using NetExtender.Types.Interception.Interfaces;

namespace NetExtender.Types.Network.Interfaces
{
    public interface IHttpInterceptClient : IIntercept<HttpInterceptClient, HttpInterceptEventArgs>
    {
        public new event HttpInterceptEventHandler? Intercept;
        public event HttpInterceptEventHandler? SendingRequest;
        public event HttpInterceptEventHandler? ResponseReceived;
    }
}