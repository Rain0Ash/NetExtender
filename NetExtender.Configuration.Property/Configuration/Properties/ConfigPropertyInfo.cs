// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.Properties.Interfaces;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Types;

namespace NetExtender.Configuration.Properties
{
    public abstract class ConfigPropertyInfo<T> : ConfigPropertyAbstraction
    {
        protected DynamicLazy<T> Internal { get; }
        public abstract T Value { get; }
        public T Alternate { get; }

        public override Boolean HasValue
        {
            get
            {
                return Internal.IsValueCreated && Internal.Value is not null;
            }
        }

        protected ConfigPropertyInfo(String? key, T alternate, ConfigPropertyOptions options, IEnumerable<String>? sections)
            : base(key, options, sections)
        {
            Internal = new DynamicLazy<T>(Initialize, true);
            Alternate = alternate;
        }
        
        protected abstract T Initialize();
    }
    
    public abstract class ConfigPropertyAbstraction : IConfigPropertyInfo
    {
        public abstract String Path { get; }
        
        public String? Key { get; }
        public ImmutableArray<String> Sections { get; }
        
        public ConfigPropertyOptions Options { get; }
        
        public abstract Boolean HasValue { get; }

        public Boolean IsCaching
        {
            get
            {
                return Options.HasFlag(ConfigPropertyOptions.Caching);
            }
        }

        public Boolean IsReadOnly
        {
            get
            {
                return Options.HasFlag(ConfigPropertyOptions.ReadOnly);
            }
        }
        
        public Boolean IsIgnoreEvent
        {
            get
            {
                return Options.HasFlag(ConfigPropertyOptions.IgnoreEvent);
            }
        }

        public Boolean IsDisableSave
        {
            get
            {
                return Options.HasFlag(ConfigPropertyOptions.DisableSave);
            }
        }
        
        public Boolean IsAlwaysDefault
        {
            get
            {
                return Options.HasFlag(ConfigPropertyOptions.AlwaysDefault);
            }
        }
        
        protected abstract event PropertyChangedEventHandler? PropertyChanged;
        event PropertyChangedEventHandler? INotifyPropertyChanged.PropertyChanged
        {
            add
            {
                PropertyChanged += value;
            }
            remove
            {
                PropertyChanged -= value;
            }
        }

        protected ConfigPropertyAbstraction(String? key, ConfigPropertyOptions options, IEnumerable<String>? sections)
        {
            Key = key;
            Sections = sections.AsImmutableArray();
            Options = options;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected abstract void Dispose(Boolean disposing);

        ~ConfigPropertyAbstraction()
        {
            Dispose(false);
        }
    }
}