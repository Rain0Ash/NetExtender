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
using NetExtender.Types.Monads;
using NetExtender.Utilities.Types;

namespace NetExtender.Configuration.Properties
{
    public class ReadOnlyConfigProperty<T> : IReadOnlyConfigProperty<T>, IFormattable
    {
        public event ConfigurationChangedEventHandler<T>? Changed;
        protected IReadOnlyConfigProperty Property { get; }
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

        public Boolean IsCaching
        {
            get
            {
                return Property.IsCaching;
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

        public virtual T Value
        {
            get
            {
                return GetValue();
            }
        }

        public T Alternate { get; }

        public Func<T, Boolean>? Validate { get; }

        public Boolean IsValid
        {
            get
            {
                return Validate?.Invoke(Internal.Value) != false;
            }
        }

        public TryConverter<String?, T> Converter { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected internal ReadOnlyConfigProperty(IReadOnlyConfig config, String? key, T alternate, Func<T, Boolean>? validate, TryConverter<String?, T>? converter, ConfigPropertyOptions options, IEnumerable<String>? sections)
            : this(new ReadOnlyConfigProperty(config, key, null, options, sections), alternate, validate, converter)
        {
        }
        
        protected internal ReadOnlyConfigProperty(IReadOnlyConfigProperty property, T alternate, Func<T, Boolean>? validate, TryConverter<String?, T>? converter)
        {
            Property = property ?? throw new ArgumentNullException(nameof(property));
            Property.Changed += OnChanged;
            Property.PropertyChanged += OnChanged;
            Internal = new DynamicLazy<T>(Initialize);
            Alternate = alternate;
            Validate = validate;
            Converter = converter ?? ConvertUtilities.TryConvert;
        }

        protected virtual void OnChanged(ConfigurationChangedEventArgs<T> args)
        {
            Changed?.Invoke(this, args);
        }

        protected virtual void OnChanged(Object? sender, PropertyChangedEventArgs args)
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
            if (Converter(value, out T? result) && Validate?.Invoke(result) != false)
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
            return !IsAlwaysDefault ? Property.GetValue(Alternate, Converter) : Alternate;
        }

        public virtual T GetValue()
        {
            return GetValue(Validate);
        }

        public virtual T GetValue(Func<T, Boolean>? predicate)
        {
            if (IsAlwaysDefault)
            {
                return Alternate;
            }
            
            T value = Property.GetValue(Alternate, Converter);
            return predicate?.Invoke(value) != false ? value : Alternate;
        }

        public Task<T> GetValueAsync()
        {
            return GetValueAsync(CancellationToken.None);
        }

        public virtual Task<T> GetValueAsync(CancellationToken token)
        {
            return GetValueAsync(Validate, token);
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
            
            T value = await Property.GetValueAsync(Alternate, Converter, token);
            return predicate?.Invoke(value) != false ? value : Alternate;
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
            
            T value = await Property.GetValueAsync(Alternate, Converter, token);

            Internal.Reset(value);
            OnChanged(value);
            return true;
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

        ~ReadOnlyConfigProperty()
        {
            Dispose(false);
        }
    }

    public class ReadOnlyConfigProperty : ConfigPropertyInfo<String?>, IReadOnlyConfigProperty, IFormattable
    {
        protected IReadOnlyConfig Config { get; }
        
        public event ConfigurationChangedEventHandler? Changed;

        public override String Path
        {
            get
            {
                return Config.Path;
            }
        }

        public override String? Value
        {
            get
            {
                return GetValue();
            }
        }

        public override event PropertyChangedEventHandler? PropertyChanged;

        protected internal ReadOnlyConfigProperty(IReadOnlyConfig config, String? key, String? alternate, ConfigPropertyOptions options, IEnumerable<String>? sections)
            : base(key, alternate, options | ConfigPropertyOptions.ReadOnly, sections)
        {
            Config = config ?? throw new ArgumentNullException(nameof(config));
            Config.Changed += OnChanged;
        }

        protected virtual void OnChanged(ConfigurationChangedEventArgs args)
        {
            Changed?.Invoke(this, args);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
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
            return !IsAlwaysDefault ? GetValueInternal() : Alternate;
        }
        
        protected virtual String? GetValueInternal()
        {
            return Config.GetValue(Key, Alternate, Sections);
        }
        
        protected virtual Task<String?> GetValueInternalAsync(CancellationToken token)
        {
            return Config.GetValueAsync(Key, Alternate, Sections, token);
        }

        protected virtual Boolean KeyExistInternal()
        {
            return Config.KeyExist(Key, Sections);
        }

        protected virtual Task<Boolean> KeyExistInternalAsync(CancellationToken token)
        {
            return Config.KeyExistAsync(Key, Sections, token);
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
                await ReadAsync(token);
            }

            return Internal.Value;
        }

        public virtual Boolean KeyExist()
        {
            return KeyExistInternal();
        }
        
        public Task<Boolean> KeyExistAsync()
        {
            return KeyExistAsync(CancellationToken.None);
        }
        
        public virtual Task<Boolean> KeyExistAsync(CancellationToken token)
        {
            return KeyExistInternalAsync(token);
        }
        
        public virtual Boolean Read()
        {
            if (IsAlwaysDefault)
            {
                return false;
            }
            
            String? value = GetValueInternal() ?? Alternate;

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
            
            String? value = await GetValueInternalAsync(token) ?? Alternate;

            if (Internal.IsValueCreated && value == Internal.Value)
            {
                return true;
            }
            
            Internal.Reset(value);
            OnChanged(value);
            return true;
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
            Changed = null;
            PropertyChanged = null;
            Config.Changed -= OnChanged;
            base.Dispose();
        }
    }
}