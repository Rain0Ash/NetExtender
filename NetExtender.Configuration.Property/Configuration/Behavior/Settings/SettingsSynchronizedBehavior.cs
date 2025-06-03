// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Properties.Interfaces;
using NetExtender.Configuration.Synchronizers;
using NetExtender.Configuration.Synchronizers.Interfaces;
using NetExtender.Types.Singletons;
using NetExtender.Types.Singletons.Interfaces;

namespace NetExtender.Configuration.Behavior.Settings
{
    public abstract class SettingsSynchronizedBehavior<T> : SettingsSynchronizedBehavior where T : SettingsSynchronizedBehavior, new()
    {
        private static ISingleton<T> Internal { get; } = new Singleton<T>();

        public static T Instance
        {
            get
            {
                return Internal.Instance;
            }
        }
    }

    public abstract class SettingsSynchronizedBehavior : SettingsBehavior, IConfigPropertySynchronizer
    {
        protected virtual IConfigPropertySynchronizer Synchronizer { get; } = new ConfigPropertySynchronizer();

        public sealed override Boolean IsThreadSafe
        {
            get
            {
                return Synchronizer.IsThreadSafe;
            }
        }

        public Int32 Count
        {
            get
            {
                return Synchronizer.Count;
            }
        }

        public Boolean IsReadOnly
        {
            get
            {
                return Synchronizer.IsReadOnly;
            }
        }

        public sealed override Boolean Read()
        {
            return Synchronizer.Read();
        }

        public sealed override Task<Boolean> ReadAsync()
        {
            return Synchronizer.ReadAsync();
        }

        public sealed override Task<Boolean> ReadAsync(CancellationToken token)
        {
            return Synchronizer.ReadAsync(token);
        }

        public sealed override Boolean Save()
        {
            return Synchronizer.Save();
        }

        public sealed override Task<Boolean> SaveAsync()
        {
            return Synchronizer.SaveAsync();
        }

        public sealed override Task<Boolean> SaveAsync(CancellationToken token)
        {
            return Synchronizer.SaveAsync(token);
        }
        
        public sealed override Boolean Reset()
        {
            return Synchronizer.Reset();
        }

        public sealed override Task<Boolean> ResetAsync()
        {
            return Synchronizer.ResetAsync();
        }

        public sealed override Task<Boolean> ResetAsync(CancellationToken token)
        {
            return Synchronizer.ResetAsync(token);
        }

        public Boolean Contains(IConfigPropertyInfo item)
        {
            return Synchronizer.Contains(item);
        }

        void ICollection<IConfigPropertyInfo>.Add(IConfigPropertyInfo item)
        {
            Synchronizer.Add(item);
        }

        Boolean ICollection<IConfigPropertyInfo>.Remove(IConfigPropertyInfo item)
        {
            return Synchronizer.Remove(item);
        }

        void ICollection<IConfigPropertyInfo>.Clear()
        {
            Synchronizer.Clear();
        }

        public void CopyTo(IConfigPropertyInfo[] array, Int32 index)
        {
            Synchronizer.CopyTo(array, index);
        }

        public IEnumerator<IConfigPropertyInfo> GetEnumerator()
        {
            return Synchronizer.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}