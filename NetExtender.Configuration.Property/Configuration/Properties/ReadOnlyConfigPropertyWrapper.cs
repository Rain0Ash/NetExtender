// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.Interfaces;
using NetExtender.Configuration.Properties.Interfaces;
using NetExtender.Types.Converters.Interfaces;

namespace NetExtender.Configuration.Properties
{
    public sealed class ReadOnlyConfigPropertyWrapper<T> : IReadOnlyConfigProperty<T>
    {
        private IConfigProperty<T> Internal { get; }

        public event ConfigurationChangedEventHandler<T>? Changed
        {
            add
            {
                Internal.Changed += value;
            }
            remove
            {
                Internal.Changed -= value;
            }
        }

        event PropertyChangedEventHandler? INotifyPropertyChanged.PropertyChanged
        {
            add
            {
                Internal.PropertyChanged += value;
            }
            remove
            {
                Internal.PropertyChanged -= value;
            }
        }

        public String Path
        {
            get
            {
                return Internal.Path;
            }
        }

        public String? Key
        {
            get
            {
                return Internal.Key;
            }
        }

        public ImmutableArray<String> Sections
        {
            get
            {
                return Internal.Sections;
            }
        }

        public ConfigPropertyOptions Options
        {
            get
            {
                return Internal.Options;
            }
        }

        public Boolean HasValue
        {
            get
            {
                return Internal.HasValue;
            }
        }

        public Boolean IsInitialize
        {
            get
            {
                return Internal.IsInitialize;
            }
        }

        public Boolean IsCaching
        {
            get
            {
                return Internal.IsCaching;
            }
        }

        public Boolean IsThrowWhenValueSetInvalid
        {
            get
            {
                return Internal.IsThrowWhenValueSetInvalid;
            }
        }

        public Boolean IsReadOnly
        {
            get
            {
                return Internal.IsReadOnly;
            }
        }

        public Boolean IsIgnoreEvent
        {
            get
            {
                return Internal.IsIgnoreEvent;
            }
        }

        public Boolean IsDisableSave
        {
            get
            {
                return Internal.IsDisableSave;
            }
        }

        public Boolean IsAlwaysDefault
        {
            get
            {
                return Internal.IsAlwaysDefault;
            }
        }

        public Boolean IsThreadSafe
        {
            get
            {
                return Internal.IsThreadSafe;
            }
        }

        public T Value
        {
            get
            {
                return Internal.Value;
            }
        }

        public T Alternate
        {
            get
            {
                return Internal.Alternate;
            }
        }

        public Boolean IsValid
        {
            get
            {
                return Internal.IsValid;
            }
        }

        public Func<T, Boolean>? Validate
        {
            get
            {
                return Internal.Validate;
            }
        }

        public ITwoWayConverter<String?, T> Converter
        {
            get
            {
                return Internal.Converter;
            }
        }

        private Boolean Disposing { get; }

        internal ReadOnlyConfigPropertyWrapper(IConfig config, String? key, T alternate, Func<T, Boolean>? validate, TryConverter<String?, T>? converter, ConfigPropertyOptions options, IEnumerable<String>? sections)
            : this(new ConfigProperty(config, key, null, options, sections), alternate, validate, converter)
        {
        }

        internal ReadOnlyConfigPropertyWrapper(IConfig config, String? key, T alternate, Func<T, Boolean>? validate, IOneWayConverter<String?, T>? converter, ConfigPropertyOptions options, IEnumerable<String>? sections)
            : this(new ConfigProperty(config, key, null, options, sections), alternate, validate, converter)
        {
        }

        internal ReadOnlyConfigPropertyWrapper(IConfigProperty property, T alternate, Func<T, Boolean>? validate, TryConverter<String?, T>? converter)
            : this(new ConfigProperty<T>(property, alternate, validate, converter), true)
        {
        }

        internal ReadOnlyConfigPropertyWrapper(IConfigProperty property, T alternate, Func<T, Boolean>? validate, IOneWayConverter<String?, T>? converter)
            : this(new ConfigProperty<T>(property, alternate, validate, converter), true)
        {
        }

        internal ReadOnlyConfigPropertyWrapper(IConfigProperty<T> property)
            : this(property, false)
        {
        }

        internal ReadOnlyConfigPropertyWrapper(IConfigProperty<T> property, Boolean disposing)
        {
            Internal = property ?? throw new ArgumentNullException(nameof(property));
            Disposing = disposing;
        }

        public T GetValue()
        {
            return Internal.GetValue();
        }

        T IGetter<T>.Get()
        {
            return GetValue();
        }

        public T GetValue(Func<T, Boolean>? predicate)
        {
            return Internal.GetValue(predicate);
        }

        public Task<T> GetValueAsync()
        {
            return Internal.GetValueAsync();
        }

        async ValueTask<T> IAsyncGetter<T>.GetAsync()
        {
            return await GetValueAsync();
        }

        public Task<T> GetValueAsync(CancellationToken token)
        {
            return Internal.GetValueAsync(token);
        }

        async ValueTask<T> IAsyncGetter<T>.GetAsync(CancellationToken token)
        {
            return await GetValueAsync(token);
        }

        public Task<T> GetValueAsync(Func<T, Boolean>? predicate)
        {
            return Internal.GetValueAsync(predicate);
        }

        public Task<T> GetValueAsync(Func<T, Boolean>? predicate, CancellationToken token)
        {
            return Internal.GetValueAsync(predicate, token);
        }

        public Boolean KeyExist()
        {
            return Internal.KeyExist();
        }

        public Task<Boolean> KeyExistAsync()
        {
            return Internal.KeyExistAsync();
        }

        public Task<Boolean> KeyExistAsync(CancellationToken token)
        {
            return Internal.KeyExistAsync(token);
        }

        public Boolean Read()
        {
            return Internal.Read();
        }

        public Task<Boolean> ReadAsync()
        {
            return Internal.ReadAsync();
        }

        public Task<Boolean> ReadAsync(CancellationToken token)
        {
            return Internal.ReadAsync(token);
        }

        public override String? ToString()
        {
            return Internal.ToString();
        }

        public String ToString(String? format, IFormatProvider? provider)
        {
            return Internal.ToString(format, provider);
        }

        public void Dispose()
        {
            if (Disposing)
            {
                Internal.Dispose();
            }
        }
    }

    public sealed class ReadOnlyConfigPropertyWrapper : IReadOnlyConfigProperty
    {
        private IConfigProperty Internal { get; }

        public event ConfigurationChangedEventHandler? Changed
        {
            add
            {
                Internal.Changed += value;
            }
            remove
            {
                Internal.Changed -= value;
            }
        }

        event PropertyChangedEventHandler? INotifyPropertyChanged.PropertyChanged
        {
            add
            {
                Internal.PropertyChanged += value;
            }
            remove
            {
                Internal.PropertyChanged -= value;
            }
        }

        public String Path
        {
            get
            {
                return Internal.Path;
            }
        }

        public String? Key
        {
            get
            {
                return Internal.Key;
            }
        }

        public ImmutableArray<String> Sections
        {
            get
            {
                return Internal.Sections;
            }
        }

        public ConfigPropertyOptions Options
        {
            get
            {
                return Internal.Options;
            }
        }

        public Boolean HasValue
        {
            get
            {
                return Internal.HasValue;
            }
        }

        public Boolean IsInitialize
        {
            get
            {
                return Internal.IsInitialize;
            }
        }

        public Boolean IsCaching
        {
            get
            {
                return Internal.IsCaching;
            }
        }

        public Boolean IsThrowWhenValueSetInvalid
        {
            get
            {
                return Internal.IsThrowWhenValueSetInvalid;
            }
        }

        public Boolean IsReadOnly
        {
            get
            {
                return Internal.IsReadOnly;
            }
        }

        public Boolean IsIgnoreEvent
        {
            get
            {
                return Internal.IsIgnoreEvent;
            }
        }

        public Boolean IsDisableSave
        {
            get
            {
                return Internal.IsDisableSave;
            }
        }

        public Boolean IsAlwaysDefault
        {
            get
            {
                return Internal.IsAlwaysDefault;
            }
        }
        
        public Boolean IsThreadSafe
        {
            get
            {
                return Internal.IsThreadSafe;
            }
        }

        public String? Value
        {
            get
            {
                return Internal.Value;
            }
        }

        public String? Alternate
        {
            get
            {
                return Internal.Alternate;
            }
        }

        private Boolean Disposing { get; }

        internal ReadOnlyConfigPropertyWrapper(IConfig config, String? key, String? alternate, ConfigPropertyOptions options, IEnumerable<String>? sections)
            : this(new ConfigProperty(config, key, alternate, options, sections), true)
        {
        }

        internal ReadOnlyConfigPropertyWrapper(IConfigProperty property)
            : this(property, false)
        {
        }

        internal ReadOnlyConfigPropertyWrapper(IConfigProperty property, Boolean disposing)
        {
            Internal = property ?? throw new ArgumentNullException(nameof(property));
            Disposing = disposing;
        }

        public String? GetValue()
        {
            return Internal.GetValue();
        }

        public Task<String?> GetValueAsync()
        {
            return Internal.GetValueAsync();
        }

        public Task<String?> GetValueAsync(CancellationToken token)
        {
            return Internal.GetValueAsync(token);
        }

        public Boolean KeyExist()
        {
            return Internal.KeyExist();
        }

        public Task<Boolean> KeyExistAsync()
        {
            return Internal.KeyExistAsync();
        }

        public Task<Boolean> KeyExistAsync(CancellationToken token)
        {
            return Internal.KeyExistAsync(token);
        }

        public Boolean Read()
        {
            return Internal.Read();
        }

        public Task<Boolean> ReadAsync()
        {
            return Internal.ReadAsync();
        }

        public Task<Boolean> ReadAsync(CancellationToken token)
        {
            return Internal.ReadAsync(token);
        }

        public override String? ToString()
        {
            return Internal.ToString();
        }

        public String ToString(String? format, IFormatProvider? provider)
        {
            return Internal.ToString(format, provider);
        }

        public void Dispose()
        {
            if (Disposing)
            {
                Internal.Dispose();
            }
        }
    }
}