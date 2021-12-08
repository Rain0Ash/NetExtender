// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.AspNetCore.Mvc;

namespace NetExtender.AspNetCore.Types.Results
{
    public class JavaScriptResult : ContentResult
    {
        public JavaScriptResult()
        {
        }

        public JavaScriptResult(String script)
        {
            Content = script ?? throw new ArgumentNullException(nameof(script));
            ContentType = "application/javascript";
        }
    }
}