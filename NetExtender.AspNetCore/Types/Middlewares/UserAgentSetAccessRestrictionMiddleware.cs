// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace NetExtender.AspNetCore.Types.Middlewares
{
    public class UserAgentSetAccessRestrictionMiddleware : UserAgentAccessRestrictionMiddleware, ICollection<String?>
    {
        public new ISet<String?> UserAgentWhitelist
        {
            get
            {
                return base.UserAgentWhitelist;
            }
        }

        public Boolean IsReadOnly
        {
            get
            {
                return UserAgentWhitelist.IsReadOnly;
            }
        }

        public UserAgentSetAccessRestrictionMiddleware(RequestDelegate next)
            : base(next, Array.Empty<String?>())
        {
        }

        public UserAgentSetAccessRestrictionMiddleware(RequestDelegate next, HttpStatusCode code)
            : base(next, Array.Empty<String?>(), code)
        {
        }

        public UserAgentSetAccessRestrictionMiddleware(RequestDelegate next, Int32 code)
            : base(next, Array.Empty<String?>(), code)
        {
        }

        void ICollection<String?>.Add(String? item)
        {
            Add(item);
        }

        public Boolean Add(String? item)
        {
            return UserAgentWhitelist.Add(item);
        }

        public Boolean Remove(String? item)
        {
            return UserAgentWhitelist.Remove(item);
        }

        public void Clear()
        {
            UserAgentWhitelist.Clear();
        }

        public void CopyTo(String?[] array, Int32 arrayIndex)
        {
            UserAgentWhitelist.CopyTo(array, arrayIndex);
        }
    }
}