using System;

namespace NetExtender.Types.Network.Interfaces
{
    public interface IAuthenticationToken
    {
        public String? AccessToken { get; }
        public Boolean CanBeRefreshed { get; }
    }
}