// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.Extensions.Logging;

namespace NetExtender.Utils.AspNetCore.Types
{
    public static class LoggingBuilderUtils
    {
        public static void DisableLogging(this ILoggingBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.ClearProviders();
            builder.SetMinimumLevel(LogLevel.None);
        }
    }
}