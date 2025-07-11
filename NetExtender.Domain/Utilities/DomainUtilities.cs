// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Domains.Applications;
using NetExtender.Domains.Applications.Interfaces;
using NetExtender.Domains.Initializer.Interfaces;
using NetExtender.Domains.Interfaces;
using NetExtender.Domains.View.Interfaces;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Types;

namespace NetExtender.Domains.Utilities
{
    public static class DomainUtilities
    {
        public static String Info(this ApplicationStatus value)
        {
            return value switch
            {
                ApplicationStatus.None => String.Empty,
                ApplicationStatus.NotFunctional => "NF",
                ApplicationStatus.PreAlpha => "PA",
                ApplicationStatus.ClosedAlpha => "CA",
                ApplicationStatus.Alpha => "A",
                ApplicationStatus.OpenAlpha => "OA",
                ApplicationStatus.PreBeta => "PB",
                ApplicationStatus.ClosedBeta => "CB",
                ApplicationStatus.Beta => "B",
                ApplicationStatus.OpenBeta => "OB",
                ApplicationStatus.Release => "R",
                _ => throw new EnumUndefinedOrNotSupportedException<ApplicationStatus>(value)
            };
        }

        public static String Info(this ApplicationBranch value)
        {
            return value switch
            {
                ApplicationBranch.Master => String.Empty,
                ApplicationBranch.Stable => "S",
                ApplicationBranch.Unstable => "U",
                ApplicationBranch.Development => "DEV",
                ApplicationBranch.Prototype => "P",
                ApplicationBranch.Nightly => "N",
                ApplicationBranch.NewArchitecture => "NA",
                _ => throw new EnumUndefinedOrNotSupportedException<ApplicationBranch>(value)
            };
        }
        
        private static Type? AutoApplication(this Assembly assembly, IDomain source, String @namespace)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }
            
            static Type? Find(Assembly assembly, IDomain source, String @namespace)
            {
                return assembly.GetTypeWithoutNamespace($"{source.ApplicationName}{nameof(Application)}")
                       ?? assembly.GetTypeWithoutNamespace($"{@namespace}{nameof(Application)}")
                       ?? assembly.GetTypeWithoutNamespace($"{nameof(Application)}");
            }
            
            Inherit.Result inherit = ReflectionUtilities.Inherit;
            if (inherit.Attribute[typeof(ApplicationInitializerAttribute)].Types is not { Count: > 0 } initializer)
            {
                return Find(assembly, source, @namespace);
            }
            
            if (initializer.Count > 1)
            {
                throw new ScanAmbiguousException($"Ambiguous '{nameof(ApplicationInitializerAttribute)}' types: {initializer.GetString()}.");
            }
            
            Type type = initializer.Single();
            
            if (type.IsInterface || type.IsAbstract || type.IsValueType)
            {
                throw new TypeNotSupportedException(type);
            }
            
            if (!inherit[typeof(IApplication)].Contains(type))
            {
                throw new TypeNotSupportedException(type, $"Type '{type}' must implement '{nameof(IApplication)}'.");
            }
            
            const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance;
            if (type.GetConstructor(binding, Type.EmptyTypes) is null)
            {
                throw new TypeNotSupportedException(type, $"Type '{type}' must have {ReflectionUtilities.Constructor}().");
            }
            
            return type;
        }
        
        private static IApplication AutoInitializeCore(IDomain source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            String @namespace = ReflectionUtilities.GetEntryAssemblyNamespace(out Assembly assembly);
            Type type = assembly.AutoApplication(source, @namespace) ?? throw new InvalidOperationException($"Application type not found at '{assembly.FullName}'.");
            return Activator.CreateInstance(type) as IApplication ?? throw new TypeNotSupportedException(type, "Application instance can't be instantiated.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDomain AutoInitialize(this IDomain source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Initialize(AutoInitializeCore(source));
        }

        // ReSharper disable once AsyncConverter.AsyncMethodNamingHighlighting
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IDomain> AutoInitialize(this Task<IDomain> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return AutoInitialize(await source.ConfigureAwait(false));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDomain AutoInitializeWithView(this IDomain source, IEnumerable<String>? args)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            IApplication application = AutoInitializeCore(source);
            return application is IApplicationInitializer initializer ? source.Initialize(initializer).View(initializer, args) : source.Initialize(application).AutoView(args);
        }
        
        // ReSharper disable once AsyncConverter.AsyncMethodNamingHighlighting
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IDomain> AutoInitializeWithView(this Task<IDomain> source, IEnumerable<String>? args)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return AutoInitializeWithView(await source.ConfigureAwait(false), args);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDomain AutoInitializeWithView(this IDomain source, params String[]? args)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            IApplication application = AutoInitializeCore(source);
            return application is IApplicationInitializer initializer ? source.Initialize(initializer).View(initializer, args) : source.Initialize(application).AutoView(args);
        }
        
        // ReSharper disable once AsyncConverter.AsyncMethodNamingHighlighting
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IDomain> AutoInitializeWithView(this Task<IDomain> source, params String[]? args)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return AutoInitializeWithView(await source.ConfigureAwait(false), args);
        }

        // ReSharper disable once AsyncConverter.AsyncMethodNamingHighlighting
        public static async Task<IDomain> Initialize<T>(this Task<IDomain> source) where T : IApplication, new()
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            IDomain domain = await source.ConfigureAwait(false);
            return domain.Initialize<T>();
        }

        // ReSharper disable once AsyncConverter.AsyncMethodNamingHighlighting
        public static async Task<IDomain> Initialize(this Task<IDomain> source, IApplication application)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (application is null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            IDomain domain = await source.ConfigureAwait(false);
            return domain.Initialize(application);
        }

        private static Type? AutoView(this Assembly assembly, IDomain source, String @namespace)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            return assembly.GetTypeWithoutNamespace($"{source.ApplicationName}ApplicationView") ??
                   assembly.GetTypeWithoutNamespace($"{source.ApplicationName}View") ??
                   assembly.GetTypeWithoutNamespace($"{@namespace}ApplicationView") ??
                   assembly.GetTypeWithoutNamespace($"{@namespace}View") ??
                   assembly.GetTypeWithoutNamespace("ApplicationView") ??
                   assembly.GetTypeWithoutNamespace("View");
        }

        private static IApplicationView AutoViewCore(IDomain source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            String @namespace = ReflectionUtilities.GetEntryAssemblyNamespace(out Assembly assembly);
            Type type = assembly.AutoView(source, @namespace) ?? throw new InvalidOperationException($"View type not found at '{assembly.FullName}'.");
            return Activator.CreateInstance(type) as IApplicationView ?? throw new InvalidOperationException("View instance can't be instantiated.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDomain AutoView(this IDomain source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.View(AutoViewCore(source));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDomain AutoView(this IDomain source, IEnumerable<String>? args)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.View(AutoViewCore(source), args);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDomain AutoView(this IDomain source, params String[]? args)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.View(AutoViewCore(source), args);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IDomain?> AutoViewAsync(this Task<IDomain> source)
        {
            return AutoViewAsync(source, CancellationToken.None);
        }

        public static async Task<IDomain?> AutoViewAsync(this Task<IDomain> source, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            IDomain domain = await source.ConfigureAwait(false);
            return await domain.ViewAsync(AutoViewCore(await source.ConfigureAwait(false)), token).ConfigureAwait(false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IDomain?> AutoViewAsync(this Task<IDomain> source, IEnumerable<String>? args)
        {
            return AutoViewAsync(source, args, CancellationToken.None);
        }

        public static async Task<IDomain?> AutoViewAsync(this Task<IDomain> source, IEnumerable<String>? args, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            IDomain domain = await source.ConfigureAwait(false);
            return await domain.ViewAsync(AutoViewCore(await source.ConfigureAwait(false)), args, token).ConfigureAwait(false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IDomain?> AutoViewAsync(this Task<IDomain> source, params String[]? args)
        {
            return AutoViewAsync(source, CancellationToken.None, args);
        }

        public static async Task<IDomain?> AutoViewAsync(this Task<IDomain> source, CancellationToken token, params String[]? args)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            IDomain domain = await source.ConfigureAwait(false);
            return await domain.ViewAsync(AutoViewCore(await source.ConfigureAwait(false)), args, token).ConfigureAwait(false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDomain View<T>(this Task<IDomain> source) where T : IApplicationView, new()
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return View(source, new T());
        }

        public static IDomain View(this Task<IDomain> source, IApplicationView view)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (view is null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            IDomain domain = source.GetAwaiter().GetResult();
            return domain.View(view);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDomain View<T>(this Task<IDomain> source, IEnumerable<String>? args) where T : IApplicationView, new()
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return View(source, new T(), args);
        }

        public static IDomain View(this Task<IDomain> source, IApplicationView view, IEnumerable<String>? args)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (view is null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            IDomain domain = source.GetAwaiter().GetResult();
            return domain.View(view, args);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDomain View<T>(this Task<IDomain> source, params String[]? args) where T : IApplicationView, new()
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return View(source, new T(), args);
        }

        public static IDomain View(this Task<IDomain> source, IApplicationView view, params String[]? args)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (view is null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            IDomain domain = source.GetAwaiter().GetResult();
            return domain.View(view, args);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IDomain> ViewAsync<T>(this Task<IDomain> source) where T : IApplicationView, new()
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return ViewAsync(source, new T());
        }

        public static async Task<IDomain> ViewAsync(this Task<IDomain> source, IApplicationView view)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (view is null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            IDomain domain = await source.ConfigureAwait(false);
            return await domain.ViewAsync(view).ConfigureAwait(false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IDomain> ViewAsync<T>(this Task<IDomain> source, CancellationToken token) where T : IApplicationView, new()
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return ViewAsync(source, new T(), token);
        }

        public static async Task<IDomain> ViewAsync(this Task<IDomain> source, IApplicationView view, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (view is null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            IDomain domain = await source.ConfigureAwait(false);
            return await domain.ViewAsync(view, token).ConfigureAwait(false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IDomain> ViewAsync<T>(this Task<IDomain> source, IEnumerable<String>? args) where T : IApplicationView, new()
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return ViewAsync(source, new T(), args);
        }

        public static async Task<IDomain> ViewAsync(this Task<IDomain> source, IApplicationView view, IEnumerable<String>? args)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (view is null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            IDomain domain = await source.ConfigureAwait(false);
            return await domain.ViewAsync(view, args).ConfigureAwait(false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IDomain> ViewAsync<T>(this Task<IDomain> source, params String[]? args) where T : IApplicationView, new()
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return ViewAsync(source, new T(), args);
        }

        public static async Task<IDomain> ViewAsync(this Task<IDomain> source, IApplicationView view, params String[]? args)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (view is null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            IDomain domain = await source.ConfigureAwait(false);
            return await domain.ViewAsync(view, args).ConfigureAwait(false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IDomain> ViewAsync<T>(this Task<IDomain> source, IEnumerable<String>? args, CancellationToken token) where T : IApplicationView, new()
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return ViewAsync(source, new T(), args, token);
        }

        public static async Task<IDomain> ViewAsync(this Task<IDomain> source, IApplicationView view, IEnumerable<String>? args, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (view is null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            IDomain domain = await source.ConfigureAwait(false);
            return await domain.ViewAsync(view, args, token).ConfigureAwait(false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IDomain> ViewAsync<T>(this Task<IDomain> source, CancellationToken token, params String[] args) where T : IApplicationView, new()
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return ViewAsync(source, new T(), token, args);
        }

        public static async Task<IDomain> ViewAsync(this Task<IDomain> source, IApplicationView view, CancellationToken token, params String[] args)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (view is null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            IDomain domain = await source.ConfigureAwait(false);
            return await domain.ViewAsync(view, token, args).ConfigureAwait(false);
        }

        public static T As<T>(this IApplication application) where T : class, IApplication
        {
            if (application is null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            while (application is IApplicationInitializer initializer)
            {
                application = initializer.Application;
            }

            return application as T ?? throw new InitializeException($"{nameof(application)} is not {typeof(T).Name}");
        }
    }
}