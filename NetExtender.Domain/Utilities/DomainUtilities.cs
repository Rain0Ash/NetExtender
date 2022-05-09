// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Domains.Applications.Interfaces;
using NetExtender.Domains.Interfaces;
using NetExtender.Domains.View.Interfaces;

namespace NetExtender.Domain.Utilities
{
    public static class DomainUtilities
    {
        public static async Task<IDomain> Initialize<T>(this Task<IDomain> source) where T : IApplication, new()
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            IDomain domain = await source.ConfigureAwait(false);
            return domain.Initialize<T>();
        }
        
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
    }
}