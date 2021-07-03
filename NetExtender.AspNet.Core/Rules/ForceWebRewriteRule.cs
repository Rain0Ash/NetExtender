// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Net.Http.Headers;

namespace AspNet.Core.Rules
{
    /// <summary>
    /// An <see cref="IRule" /> implementation that forces a site to be browsed using a www prefix
    /// </summary>
    public class ForceWebRewriteRule : IRule
    {
        /// <summary>
        /// Applies the rule to the context.  If the host starts with www no action is taken
        /// </summary>
        /// <param name="context">The rewrite context</param>
        public void ApplyRule(RewriteContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            HttpRequest request = context.HttpContext.Request;
            if (request.Host.Value.StartsWith("www.", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            HttpResponse response = context.HttpContext.Response;
            String redirect = $"{request.Scheme}://www.{request.Host}{request.Path}{request.QueryString}";

            response.Headers[HeaderNames.Location] = redirect;
            response.StatusCode = StatusCodes.Status301MovedPermanently;

            context.Result = RuleResult.EndResponse;
        }
    }
}