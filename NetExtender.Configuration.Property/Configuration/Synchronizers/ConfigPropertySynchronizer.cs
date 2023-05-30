// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Properties.Interfaces;
using NetExtender.Configuration.Synchronizers.Interfaces;
using NetExtender.Types.Behavior.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Configuration.Synchronizers
{
    public sealed class ConfigPropertySynchronizer : IConfigPropertySynchronizer
    {
        private ImmutableList<IConfigPropertyInfo>? Internal { get; set; }
        private Object Synchronization { get; } = ConcurrentUtilities.Synchronization;

        public Int32 Count
        {
            get
            {
                lock (Synchronization)
                {
                    return Internal?.Count ?? 0;
                }
            }
        }

        public Boolean IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public Boolean IsThreadSafe
        {
            get
            {
                return true;
            }
        }

        public Boolean Contains(IConfigPropertyInfo item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            lock (Synchronization)
            {
                return Internal?.Contains(item) ?? false;
            }
        }

        public void Add(IConfigPropertyInfo item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            lock (Synchronization)
            {
                Internal = Internal?.Add(item) ?? ImmutableList.Create(item);
            }
        }

        public Boolean Remove(IConfigPropertyInfo item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            lock (Synchronization)
            {
                Boolean contains = Contains(item);
                Internal = Internal?.Remove(item);
                return contains;
            }
        }

        public void Clear()
        {
            lock (Synchronization)
            {
                Internal = Internal?.Clear();
            }
        }

        public void CopyTo(IConfigPropertyInfo[] array, Int32 arrayIndex)
        {
            lock (Synchronization)
            {
                ImmutableList<IConfigPropertyInfo> collection = Internal ?? ImmutableList<IConfigPropertyInfo>.Empty;
                collection.CopyTo(array, arrayIndex);
            }
        }

        public Boolean Read()
        {
            lock (Synchronization)
            {
                ImmutableList<IConfigPropertyInfo>? collection = Internal;
                return collection is not null && collection.OfType<IReadableBehavior>().Aggregate(false, (current, info) => current | info.Read());
            }
        }

        public async Task<Boolean> ReadAsync()
        {
            ImmutableList<IConfigPropertyInfo>? collection = Internal;

            if (collection is null)
            {
                return false;
            }

            Boolean successful = false;
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (IReadableBehavior info in collection.OfType<IReadableBehavior>())
            {
                successful |= await info.ReadAsync().ConfigureAwait(false);
            }
            
            return successful;
        }

        public async Task<Boolean> ReadAsync(CancellationToken token)
        {
            ImmutableList<IConfigPropertyInfo>? collection = Internal;

            if (collection is null)
            {
                return false;
            }

            Boolean successful = false;
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (IReadableBehavior info in collection.OfType<IReadableBehavior>())
            {
                if (token.IsCancellationRequested)
                {
                    return successful;
                }
                
                successful |= await info.ReadAsync(token).ConfigureAwait(false);
            }
            
            return successful;
        }

        public Boolean Save()
        {
            lock (Synchronization)
            {
                ImmutableList<IConfigPropertyInfo>? collection = Internal;
                return collection is not null && collection.OfType<ISaveableBehavior>().Aggregate(false, (current, info) => current | info.Save());
            }
        }

        public async Task<Boolean> SaveAsync()
        {
            ImmutableList<IConfigPropertyInfo>? collection = Internal;

            if (collection is null)
            {
                return false;
            }

            Boolean successful = false;
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (ISaveableBehavior info in collection.OfType<ISaveableBehavior>())
            {
                successful |= await info.SaveAsync().ConfigureAwait(false);
            }
            
            return successful;
        }

        public async Task<Boolean> SaveAsync(CancellationToken token)
        {
            ImmutableList<IConfigPropertyInfo>? collection = Internal;

            if (collection is null)
            {
                return false;
            }

            Boolean successful = false;
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (ISaveableBehavior info in collection.OfType<ISaveableBehavior>())
            {
                if (token.IsCancellationRequested)
                {
                    return successful;
                }
                
                successful |= await info.SaveAsync(token).ConfigureAwait(false);
            }
            
            return successful;
        }

        public Boolean Reset()
        {
            lock (Synchronization)
            {
                ImmutableList<IConfigPropertyInfo>? collection = Internal;
                return collection is not null && collection.OfType<IResetableBehavior>().Aggregate(false, (current, info) => current | info.Reset());
            }
        }

        public async Task<Boolean> ResetAsync()
        {
            ImmutableList<IConfigPropertyInfo>? collection = Internal;

            if (collection is null)
            {
                return false;
            }

            Boolean successful = false;
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (IResetableBehavior info in collection.OfType<IResetableBehavior>())
            {
                successful |= await info.ResetAsync().ConfigureAwait(false);
            }
            
            return successful;
        }

        public async Task<Boolean> ResetAsync(CancellationToken token)
        {
            ImmutableList<IConfigPropertyInfo>? collection = Internal;

            if (collection is null)
            {
                return false;
            }

            Boolean successful = false;
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (IResetableBehavior info in collection.OfType<IResetableBehavior>())
            {
                if (token.IsCancellationRequested)
                {
                    return successful;
                }
                
                successful |= await info.ResetAsync(token).ConfigureAwait(false);
            }
            
            return successful;
        }
        
        public IEnumerator<IConfigPropertyInfo> GetEnumerator()
        {
            return Internal?.GetEnumerator() ?? ImmutableList<IConfigPropertyInfo>.Empty.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}