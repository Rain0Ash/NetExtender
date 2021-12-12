// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Http;
using NetExtender.Utilities.AspNetCore.Types;

namespace NetExtender.AspNetCore.Types.Middlewares
{
    public class UserAgentAccessRestrictionMiddleware : AccessRestrictionMiddleware, IReadOnlyCollection<String?>
    {
        protected Int32 RestrictionStatusCode { get; }
        protected ISet<String?> UserAgentWhitelist { get; }

        public Int32 Count
        {
            get
            {
                return UserAgentWhitelist.Count;
            }
        }

        public UserAgentAccessRestrictionMiddleware(RequestDelegate next, String? agent)
            : this(next, agent, DefaultRestrictionStatusCode)
        {
        }

        public UserAgentAccessRestrictionMiddleware(RequestDelegate next, String? agent, HttpStatusCode code)
            : this(next, agent, (Int32) code)
        {
        }

        public UserAgentAccessRestrictionMiddleware(RequestDelegate next, String? agent, Int32 code)
            : base(next)
        {
            RestrictionStatusCode = code;
            UserAgentWhitelist = new HashSet<String?>(1) { agent };
        }
        
        public UserAgentAccessRestrictionMiddleware(RequestDelegate next, IEnumerable<String?> agents)
            : this(next, agents, DefaultRestrictionStatusCode)
        {
        }

        public UserAgentAccessRestrictionMiddleware(RequestDelegate next, IEnumerable<String?> agents, HttpStatusCode code)
            : this(next, agents, (Int32) code)
        {
        }

        public UserAgentAccessRestrictionMiddleware(RequestDelegate next, IEnumerable<String?> agents, Int32 code)
            : base(next)
        {
            if (agents is null)
            {
                throw new ArgumentNullException(nameof(agents));
            }

            RestrictionStatusCode = code;
            UserAgentWhitelist = agents.ToHashSet();
        }

        protected override Int32 Access(HttpContext context)
        {
            String? agent = context.Request.GetUserAgent();
            return Contains(agent) ? AllowStatusCode : RestrictionStatusCode;
        }
        
        public Boolean Contains(String? item)
        {
            return UserAgentWhitelist.Contains(item);
        }

        public IEnumerator<String?> GetEnumerator()
        {
            return UserAgentWhitelist.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) UserAgentWhitelist).GetEnumerator();
        }
    }
}