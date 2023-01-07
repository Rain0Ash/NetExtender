// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Interfaces;
using NetExtender.Initializer.Types.Behavior.Interfaces;

namespace NetExtender.Configuration.Behavior.Settings
{
    public abstract class SettingsBehavior<T> : SettingsBehavior where T : SettingsBehavior, new()
    {
        private static Lazy<T> Internal { get; } = new Lazy<T>(() => new T(), true);

        public static T Instance
        {
            get
            {
                return Internal.Value;
            }
        }
    }
    
    public abstract class SettingsBehavior : IChangeableBehavior
    {
        protected abstract IConfigInfo Config { get; }
        public abstract Boolean IsThreadSafe { get; }
        
        public virtual Boolean IsDebug
        {
            get
            {
#if DEBUG
                return true;
#else
                return false;
#endif
            }
        }
        
        public abstract Boolean Read();
        public abstract Task<Boolean> ReadAsync();
        public abstract Task<Boolean> ReadAsync(CancellationToken token);
        public abstract Boolean Save();
        public abstract Task<Boolean> SaveAsync();
        public abstract Task<Boolean> SaveAsync(CancellationToken token);
        public abstract Boolean Reset();
        public abstract Task<Boolean> ResetAsync();
        public abstract Task<Boolean> ResetAsync(CancellationToken token);
    }
}