using System;
using NetExtender.Exceptions.Interfaces;

namespace NetExtender.AspNetCore.Identity.Interfaces
{
    public interface IIdentityException : IUnsafeBusinessException
    {
        public IdentityException.Id Known { get; }
        public new Object? Id { get; }
        public IUserInfo? User { get; }
    }
}