// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net.Mime;

namespace NetExtender.AspNetCore.Types.Results
{
    public class HtmlJavaScriptResult : JavaScriptResult
    {
        public HtmlJavaScriptResult(String script)
        {
            if (script is null)
            {
                throw new ArgumentNullException(nameof(script));
            }

            Content = $"<html><body><script>{script}</script></body></html>";
            ContentType = MediaTypeNames.Text.Html;
        }
    }
}