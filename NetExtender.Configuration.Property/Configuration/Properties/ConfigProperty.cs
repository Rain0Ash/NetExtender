// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.Interfaces;
using NetExtender.Configuration.Properties.Interfaces;
using NetExtender.Configuration.Utilities;
using NetExtender.Types.Converters;
using NetExtender.Types.Converters.Interfaces;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Types;

namespace NetExtender.Configuration.Properties
{
    public class ConfigProperty<T> : IConfigProperty<T>
    {
        public event ConfigurationChangedEventHandler<T>? Changed;
        protected IConfigProperty Property { get; }
        protected DynamicLazy<T> Internal { get; }

        public String Path
        {
            get
            {
                return Property.Path;
            }
        }

        public String? Key
        {
            get
            {
                return Property.Key;
            }
        }

        public ImmutableArray<String> Sections
        {
            get
            {
                return Property.Sections;
            }
        }

        public ConfigPropertyOptions Options
        {
            get
            {
                return Property.Options;
            }
        }

        public Boolean HasValue
        {
            get
            {
                return Property.HasValue;
            }
        }

        public Boolean IsInitialize
        {
            get
            {
                return Property.IsInitialize;
            }
        }

        public Boolean IsCaching
        {
            get
            {
                return Property.IsCaching;
            }
        }

        public Boolean IsThrowWhenValueSetInvalid
        {
            get
            {
                return Property.IsThrowWhenValueSetInvalid;
            }
        }

        public Boolean IsReadOnly
        {
            get
            {
                return Property.IsReadOnly;
            }
        }

        public Boolean IsIgnoreEvent
        {
            get
            {
                return Property.IsIgnoreEvent;
            }
        }

        public Boolean IsDisableSave
        {
            get
            {
                return Property.IsDisableSave;
            }
        }

        public Boolean IsAlwaysDefault
        {
            get
            {
                return Property.IsAlwaysDefault;
            }
        }
        
        public Boolean IsThreadSafe
        {
            get
            {
                return Property.IsThreadSafe;
            }
        }

        public virtual T Value
        {
            get
            {
                return GetValue();
            }
            set
            {
                if (!SetValue(value) && IsThrowWhenValueSetInvalid)
                {
                    throw new InvalidOperationException($"Can't set value '{value}' to {Path}");
                }
            }
        }

        public T Alternate { get; }

        public Func<T, Boolean>? Validate { get; }

        public Boolean IsValid
        {
            get
            {
                return Validate?.Invoke(Internal.Value) is not false;
            }
        }

        public ITwoWayConverter<String?, T> Converter { get; }

        private event PropertyChangedEventHandler? PropertyChanged;
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

        protected internal ConfigProperty(IConfig config, String? key, T alternate, Func<T, Boolean>? validate, TryConverter<String?, T>? converter, ConfigPropertyOptions options, IEnumerable<String>? sections)
            : this(new ConfigProperty(config, key, null, options, sections), alternate, validate, converter)
        {
        }

        protected internal ConfigProperty(IConfig config, String? key, T alternate, Func<T, Boolean>? validate, IOneWayConverter<String?, T>? converter, ConfigPropertyOptions options, IEnumerable<String>? sections)
            : this(new ConfigProperty(config, key, null, options, sections), alternate, validate, converter)
        {
        }

        protected internal ConfigProperty(IConfigProperty property, T alternate, Func<T, Boolean>? validate, TryConverter<String?, T>? converter)
        {
            Property = property ?? throw new ArgumentNullException(nameof(property));
            Property.Changed += OnChanged;
            Property.PropertyChanged += OnChanged;
            Internal = new DynamicLazy<T>(Initialize);
            Alternate = alternate;
            Validate = validate;
            Converter = converter is not null ? TwoWayConverter<String?, T>.Combine(converter, TwoWayConverter<T>.String()) : TwoWayConverter<T>.String().Reverse();
            
            if (IsInitialize)
            {
                Internal.TryInitialize();
            }
        }

        protected internal ConfigProperty(IConfigProperty property, T alternate, Func<T, Boolean>? validate, IOneWayConverter<String?, T>? converter)
        {
            Property = property ?? throw new ArgumentNullException(nameof(property));
            Property.Changed += OnChanged;
            Property.PropertyChanged += OnChanged;
            Internal = new DynamicLazy<T>(Initialize);
            Alternate = alternate;
            Validate = validate;
            Converter = converter?.AsTwoWay(TwoWayConverter<T>.String()) ?? TwoWayConverter<T>.String().Reverse();

            if (IsInitialize)
            {
                Internal.TryInitialize();
            }
        }

        protected void OnChanged(ConfigurationChangedEventArgs<T> args)
        {
            Changed?.Invoke(this, args);
        }

        protected void OnChanged(Object? sender, PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(this, args);
        }

        protected virtual void OnChanged(T value)
        {
            OnChanged(new ConfigurationChangedEventArgs<T>(new ConfigurationValueEntry<T>(Key, value, Sections)));
        }

        protected virtual void OnChanged(Object? sender, ConfigurationChangedEventArgs args)
        {
            if (IsIgnoreEvent)
            {
                return;
            }

            (String? key, String? value, ImmutableArray<String> sections) = args.Value;
            if (Converter.TryConvert(value, out T? result) && Validate?.Invoke(result) is not false)
            {
                ConfigurationValueEntry<T> entry = new ConfigurationValueEntry<T>(key, result, sections);
                Changed?.Invoke(this, new ConfigurationChangedEventArgs<T>(entry, args.Handled));
            }
            else
            {
                ConfigurationValueEntry<T> entry = new ConfigurationValueEntry<T>(key, Alternate, sections);
                Changed?.Invoke(this, new ConfigurationChangedEventArgs<T>(entry, true));
            }
        }

        protected virtual T Initialize()
        {
            if (IsAlwaysDefault)
            {
                return Alternate;
            }

            if (IsInitialize && Property.GetValue() is null)
            {
                return Value = Alternate;
            }
            
            return Property.GetValue(Alternate, Converter);
        }

        public virtual T GetValue()
        {
            return GetValue(Validate);
        }

        T IGetter<T>.Get()
        {
            return GetValue();
        }

        public virtual T GetValue(Func<T, Boolean>? predicate)
        {
            if (IsAlwaysDefault)
            {
                return Alternate;
            }

            T value = Property.GetValue(Alternate, Converter);
            return predicate?.Invoke(value) is not false ? value : Alternate;
        }

        public Task<T> GetValueAsync()
        {
            return GetValueAsync(CancellationToken.None);
        }

        async ValueTask<T> IAsyncGetter<T>.GetAsync()
        {
            return await GetValueAsync();
        }

        public virtual Task<T> GetValueAsync(CancellationToken token)
        {
            return GetValueAsync(Validate, token);
        }

        async ValueTask<T> IAsyncGetter<T>.GetAsync(CancellationToken token)
        {
            return await GetValueAsync(token);
        }

        public Task<T> GetValueAsync(Func<T, Boolean>? predicate)
        {
            return GetValueAsync(predicate, CancellationToken.None);
        }

        public virtual async Task<T> GetValueAsync(Func<T, Boolean>? predicate, CancellationToken token)
        {
            if (IsAlwaysDefault)
            {
                return Alternate;
            }

            T value = await Property.GetValueAsync(Alternate, Converter, token).ConfigureAwait(false);
            return predicate?.Invoke(value) is not false ? value : Alternate;
        }

        public virtual Boolean SetValue(T value)
        {
            return !IsReadOnly && Property.SetValue(value, Converter);
        }

        void ISetter<T>.Set(T value)
        {
            SetValue(value);
        }

        public Task<Boolean> SetValueAsync(T value)
        {
            return SetValueAsync(value, CancellationToken.None);
        }

        async ValueTask IAsyncSetter<T>.SetAsync(T value)
        {
            await SetValueAsync(value);
        }

        public virtual async Task<Boolean> SetValueAsync(T value, CancellationToken token)
        {
            return !IsReadOnly && await Property.SetValueAsync(value, Converter, token).ConfigureAwait(false);
        }

        async ValueTask IAsyncSetter<T>.SetAsync(T value, CancellationToken token)
        {
            await SetValueAsync(value, token);
        }

        public virtual Boolean RemoveValue()
        {
            return !IsReadOnly && Property.RemoveValue();
        }

        public Task<Boolean> RemoveValueAsync()
        {
            return RemoveValueAsync(CancellationToken.None);
        }

        public virtual async Task<Boolean> RemoveValueAsync(CancellationToken token)
        {
            return !IsAlwaysDefault && !IsReadOnly && await Property.RemoveValueAsync(token).ConfigureAwait(false);
        }

        public virtual Boolean KeyExist()
        {
            return Property.KeyExist();
        }

        public Task<Boolean> KeyExistAsync()
        {
            return KeyExistAsync(CancellationToken.None);
        }

        public virtual Task<Boolean> KeyExistAsync(CancellationToken token)
        {
            return Property.KeyExistAsync(token);
        }

        public virtual Boolean Read()
        {
            if (IsAlwaysDefault)
            {
                return false;
            }

            T value = Property.GetValue(Alternate, Converter);

            Internal.Reset(value);
            OnChanged(value);
            return true;
        }

        public Task<Boolean> ReadAsync()
        {
            return ReadAsync(CancellationToken.None);
        }

        public virtual async Task<Boolean> ReadAsync(CancellationToken token)
        {
            if (IsAlwaysDefault)
            {
                return false;
            }

            T value = await Property.GetValueAsync(Alternate, Converter, token).ConfigureAwait(false);

            Internal.Reset(value);
            OnChanged(value);
            return true;
        }

        public virtual Boolean Save()
        {
            return Property.Save();
        }

        public Task<Boolean> SaveAsync()
        {
            return SaveAsync(CancellationToken.None);
        }

        public virtual Task<Boolean> SaveAsync(CancellationToken token)
        {
            return Property.SaveAsync(token);
        }

        public virtual Boolean Reset()
        {
            return Property.Reset();
        }

        public Task<Boolean> ResetAsync()
        {
            return ResetAsync(CancellationToken.None);
        }

        public virtual Task<Boolean> ResetAsync(CancellationToken token)
        {
            return Property.ResetAsync(token);
        }

        public override String? ToString()
        {
            return new ConfigurationValueEntry<T>(Key, Value, Sections).GetString();
        }

        public String ToString(String? format, IFormatProvider? provider)
        {
            return new ConfigurationValueEntry<T>(Key, Value, Sections).GetString(format, provider) ?? String.Empty;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(Boolean disposing)
        {
            Changed = null;
            PropertyChanged = null;
            Property.Changed -= OnChanged;
            Property.PropertyChanged -= OnChanged;

            if (disposing)
            {
                Property.Dispose();
            }
        }

        ~ConfigProperty()
        {
            Dispose(false);
        }
    }

    public class ConfigProperty : ConfigPropertyInfo<String?>, IConfigProperty
    {
        protected IConfig Config { get; }

        public event ConfigurationChangedEventHandler? Changed;

        public sealed override String Path
        {
            get
            {
                return Config.Path;
            }
        }

        public virtual String? Value
        {
            get
            {
                return GetValue();
            }
            set
            {
                if (!SetValue(value) && IsThrowWhenValueSetInvalid)
                {
                    throw new InvalidOperationException($"Can't set value '{value}' to {Path}");
                }
            }
        }
        
        public sealed override Boolean IsThreadSafe
        {
            get
            {
                return Config.IsThreadSafe;
            }
        }

        protected override event PropertyChangedEventHandler? PropertyChanged;

        protected internal ConfigProperty(IConfig config, String? key, String? alternate, ConfigPropertyOptions options, IEnumerable<String>? sections)
            : base(key, alternate, options, sections)
        {
            Config = config ?? throw new ArgumentNullException(nameof(config));
            Config.Changed += OnChanged;
            
            if (IsInitialize)
            {
                Internal.TryInitialize();
            }
        }

        protected virtual void OnChanged(ConfigurationChangedEventArgs args)
        {
            Changed?.Invoke(this, args);
            PropertyChanged?.Invoke(this, new PropertyChanged(nameof(Value)));
        }

        protected virtual void OnChanged(String? value)
        {
            OnChanged(new ConfigurationChangedEventArgs(new ConfigurationValueEntry(Key, value, Sections)));
        }

        protected virtual void OnChanged(Object? sender, ConfigurationChangedEventArgs args)
        {
            if (IsIgnoreEvent || args.Handled)
            {
                return;
            }

            (String? key, String? value, ImmutableArray<String> sections) = args.Value;
            if (key != Key || Internal.IsValueCreated && value == Internal.Value || !sections.SequenceEqual(Sections))
            {
                return;
            }

            Internal.Reset(value);
            OnChanged(args);
        }

        protected override String? Initialize()
        {
            if (IsAlwaysDefault)
            {
                return Alternate;
            }

            if (IsInitialize && Config.GetValue(Key, Sections) is null)
            {
                return Value = Alternate;
            }

            return GetValueCore();
        }

        protected virtual String? GetValueCore()
        {
            return Config.GetValue(Key, Alternate, Sections);
        }

        protected virtual Task<String?> GetValueCoreAsync(CancellationToken token)
        {
            return Config.GetValueAsync(Key, Alternate, Sections, token);
        }

        protected virtual Boolean KeyExistCore()
        {
            return Config.KeyExist(Key, Sections);
        }

        protected virtual Task<Boolean> KeyExistCoreAsync(CancellationToken token)
        {
            return Config.KeyExistAsync(Key, Sections, token);
        }

        protected virtual Boolean SetValueCore()
        {
            return Config.SetValue(Key, Internal.Value, Sections);
        }

        protected virtual Task<Boolean> SetValueCoreAsync(CancellationToken token)
        {
            return Config.SetValueAsync(Key, Internal.Value, Sections, token);
        }

        public virtual String? GetValue()
        {
            if (IsAlwaysDefault)
            {
                return Alternate;
            }

            if (!IsCaching)
            {
                Read();
            }

            return Internal.Value;
        }

        public Task<String?> GetValueAsync()
        {
            return GetValueAsync(CancellationToken.None);
        }

        public virtual async Task<String?> GetValueAsync(CancellationToken token)
        {
            if (IsAlwaysDefault)
            {
                return Alternate;
            }

            if (!IsCaching)
            {
                await ReadAsync(token).ConfigureAwait(false);
            }

            return Internal.Value;
        }

        public virtual Boolean SetValue(String? value)
        {
            if (IsReadOnly)
            {
                return false;
            }

            if (Internal.IsValueCreated && value == Internal.Value)
            {
                return true;
            }

            Internal.Reset(value);

            if (!IsCaching)
            {
                Save();
            }

            OnChanged(Internal.Value);
            return true;
        }

        public Task<Boolean> SetValueAsync(String? value)
        {
            return SetValueAsync(value, CancellationToken.None);
        }

        public virtual async Task<Boolean> SetValueAsync(String? value, CancellationToken token)
        {
            if (IsReadOnly)
            {
                return false;
            }

            if (Internal.IsValueCreated && value == Internal.Value)
            {
                return true;
            }

            Internal.Reset(value);

            if (!IsCaching)
            {
                await SaveAsync(token).ConfigureAwait(false);
            }

            OnChanged(Internal.Value);
            return true;
        }

        public virtual Boolean RemoveValue()
        {
            return SetValue(null);
        }

        public Task<Boolean> RemoveValueAsync()
        {
            return RemoveValueAsync(CancellationToken.None);
        }

        public virtual Task<Boolean> RemoveValueAsync(CancellationToken token)
        {
            return SetValueAsync(null, token);
        }

        public virtual Boolean KeyExist()
        {
            return KeyExistCore();
        }

        public Task<Boolean> KeyExistAsync()
        {
            return KeyExistAsync(CancellationToken.None);
        }

        public virtual Task<Boolean> KeyExistAsync(CancellationToken token)
        {
            return KeyExistCoreAsync(token);
        }

        public virtual Boolean Read()
        {
            if (IsAlwaysDefault)
            {
                return false;
            }

            String? value = GetValueCore() ?? Alternate;

            if (Internal.IsValueCreated && value == Internal.Value)
            {
                return true;
            }

            Internal.Reset(value);
            OnChanged(value);
            return true;
        }

        public Task<Boolean> ReadAsync()
        {
            return ReadAsync(CancellationToken.None);
        }

        public virtual async Task<Boolean> ReadAsync(CancellationToken token)
        {
            if (IsAlwaysDefault)
            {
                return false;
            }

            String? value = await GetValueCoreAsync(token).ConfigureAwait(false) ?? Alternate;

            if (Internal.IsValueCreated && value == Internal.Value)
            {
                return true;
            }

            Internal.Reset(value);
            OnChanged(value);
            return true;
        }

        public virtual Boolean Save()
        {
            if (IsReadOnly || IsDisableSave || !IsInitialize && !Internal.IsValueCreated)
            {
                return false;
            }

            return SetValueCore();
        }

        public Task<Boolean> SaveAsync()
        {
            return SaveAsync(CancellationToken.None);
        }

        public virtual Task<Boolean> SaveAsync(CancellationToken token)
        {
            if (IsReadOnly || IsDisableSave || !IsInitialize && !Internal.IsValueCreated)
            {
                return TaskUtilities.False;
            }

            return SetValueCoreAsync(token);
        }

        public virtual Boolean Reset()
        {
            return SetValue(Alternate);
        }

        public Task<Boolean> ResetAsync()
        {
            return ResetAsync(CancellationToken.None);
        }

        public virtual Task<Boolean> ResetAsync(CancellationToken token)
        {
            return SetValueAsync(Alternate, token);
        }

        public override String? ToString()
        {
            return new ConfigurationValueEntry(Key, Value, Sections).GetString();
        }

        public String ToString(String? format, IFormatProvider? provider)
        {
            return new ConfigurationValueEntry(Key, Value, Sections).GetString(format, provider) ?? String.Empty;
        }

        protected override void Dispose(Boolean disposing)
        {
            Config.Changed -= OnChanged;
            Changed = null;
            PropertyChanged = null;
        }
    }
}