using System;
using NetExtender.Types.Exceptions.Interfaces;

namespace NetExtender.AspNetCore.Identity.Interfaces
{
    public interface IIdentityException : IBusinessException
    {
        public IdentityException.Known Known { get; }
        public new Object? Id { get; }
        public IUserInfo? User { get; }
    }
}