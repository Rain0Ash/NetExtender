// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;

namespace NetExtender.Utilities.AspNetCore.Types
{
    public static class WebApplicationUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IApplicationBuilder AsBuilder(this WebApplication application)
        {
            return application ?? throw new ArgumentNullException(nameof(application));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static WebApplication AsBuilder(this WebApplication application, Action<IApplicationBuilder> action)
        {
            if (application is null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            action(application);
            return application;
        }
    }
}