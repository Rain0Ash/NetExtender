// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.Extensions.Hosting;

namespace NetExtender.Utilities.AspNetCore.Types
{
    public static class HostEnvironmentUtilities
    {
        public static Boolean IsNotStaging(this IHostEnvironment environment)
        {
            if (environment is null)
            {
                throw new ArgumentNullException(nameof(environment));
            }

            return !environment.IsStaging();
        }

        public static IHostEnvironment IfStaging(this IHostEnvironment environment, Action? action)
        {
            if (environment is null)
            {
                throw new ArgumentNullException(nameof(environment));
            }

            if (environment.IsStaging())
            {
                action?.Invoke();
            }

            return environment;
        }

        public static IHostEnvironment IfStaging(this IHostEnvironment environment, Action? @if, Action? @else)
        {
            if (environment is null)
            {
                throw new ArgumentNullException(nameof(environment));
            }

            if (environment.IsStaging())
            {
                @if?.Invoke();
                return environment;
            }

            @else?.Invoke();
            return environment;
        }

        public static IHostEnvironment IfNotStaging(this IHostEnvironment environment, Action? action)
        {
            if (environment is null)
            {
                throw new ArgumentNullException(nameof(environment));
            }

            if (!environment.IsStaging())
            {
                action?.Invoke();
            }

            return environment;
        }

        public static Boolean IsNotDevelopment(this IHostEnvironment environment)
        {
            if (environment is null)
            {
                throw new ArgumentNullException(nameof(environment));
            }

            return !environment.IsDevelopment();
        }

        public static IHostEnvironment IfDevelopment(this IHostEnvironment environment, Action? action)
        {
            if (environment is null)
            {
                throw new ArgumentNullException(nameof(environment));
            }

            if (environment.IsDevelopment())
            {
                action?.Invoke();
            }

            return environment;
        }

        public static IHostEnvironment IfDevelopment(this IHostEnvironment environment, Action? @if, Action? @else)
        {
            if (environment is null)
            {
                throw new ArgumentNullException(nameof(environment));
            }

            if (environment.IsDevelopment())
            {
                @if?.Invoke();
                return environment;
            }

            @else?.Invoke();
            return environment;
        }

        public static IHostEnvironment IfNotDevelopment(this IHostEnvironment environment, Action? action)
        {
            if (environment is null)
            {
                throw new ArgumentNullException(nameof(environment));
            }

            if (!environment.IsDevelopment())
            {
                action?.Invoke();
            }

            return environment;
        }

        public static Boolean IsNotProduction(this IHostEnvironment environment)
        {
            if (environment is null)
            {
                throw new ArgumentNullException(nameof(environment));
            }

            return !environment.IsProduction();
        }

        public static IHostEnvironment IfProduction(this IHostEnvironment environment, Action? action)
        {
            if (environment is null)
            {
                throw new ArgumentNullException(nameof(environment));
            }

            if (environment.IsProduction())
            {
                action?.Invoke();
            }

            return environment;
        }

        public static IHostEnvironment IfProduction(this IHostEnvironment environment, Action? @if, Action? @else)
        {
            if (environment is null)
            {
                throw new ArgumentNullException(nameof(environment));
            }

            if (environment.IsProduction())
            {
                @if?.Invoke();
                return environment;
            }

            @else?.Invoke();
            return environment;
        }

        public static IHostEnvironment IfNotProduction(this IHostEnvironment environment, Action? action)
        {
            if (environment is null)
            {
                throw new ArgumentNullException(nameof(environment));
            }

            if (!environment.IsProduction())
            {
                action?.Invoke();
            }

            return environment;
        }

        public static Boolean IsNotEnvironment(this IHostEnvironment environment, String name)
        {
            if (environment is null)
            {
                throw new ArgumentNullException(nameof(environment));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return !environment.IsEnvironment(name);
        }

        public static IHostEnvironment IfEnvironment(this IHostEnvironment environment, String name, Action? action)
        {
            if (environment is null)
            {
                throw new ArgumentNullException(nameof(environment));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (environment.IsEnvironment(name))
            {
                action?.Invoke();
            }

            return environment;
        }

        public static IHostEnvironment IfEnvironment(this IHostEnvironment environment, String name, Action? @if, Action? @else)
        {
            if (environment is null)
            {
                throw new ArgumentNullException(nameof(environment));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (environment.IsEnvironment(name))
            {
                @if?.Invoke();
                return environment;
            }

            @else?.Invoke();
            return environment;
        }

        public static IHostEnvironment IfNotEnvironment(this IHostEnvironment environment, String name, Action? action)
        {
            if (environment is null)
            {
                throw new ArgumentNullException(nameof(environment));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (!environment.IsEnvironment(name))
            {
                action?.Invoke();
            }

            return environment;
        }
    }
}