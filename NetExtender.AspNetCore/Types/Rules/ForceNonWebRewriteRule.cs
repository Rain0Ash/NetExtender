// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Net.Http.Headers;

namespace NetExtender.AspNetCore.Types.Rules
{
    /// <summary>
    /// An <see cref="IRule" /> implementation that ensures a www prefix is not used with a domain
    /// </summary>
    public class ForceNonWebRewriteRule : IRule
    {
        /// <summary>
        /// Runs when applying the rule
        /// </summary>
        /// <param name="context">The rewrite context</param>
        public void ApplyRule(RewriteContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            HttpRequest request = context.HttpContext.Request;
            if (!request.Host.Value.StartsWith("www.", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            String host = request.Host.Value.Substring(4);
            String redirect = $"{request.Scheme}://{host}{request.Path}{request.QueryString}";

            HttpResponse response = context.HttpContext.Response;
            response.Headers[HeaderNames.Location] = redirect;
            response.StatusCode = StatusCodes.Status301MovedPermanently;

            context.Result = RuleResult.EndResponse;
        }
    }
}