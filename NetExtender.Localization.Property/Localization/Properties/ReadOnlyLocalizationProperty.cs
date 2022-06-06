// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.Properties.Interfaces;
using NetExtender.Localization.Common;
using NetExtender.Localization.Common.Interfaces;
using NetExtender.Localization.Events;
using NetExtender.Localization.Interfaces;
using NetExtender.Localization.Properties.Interfaces;
using NetExtender.Types.Culture;
using NetExtender.Utilities.Types;

namespace NetExtender.Localization.Property.Localization.Properties
{
    public class ReadOnlyLocalizationProperty : LocalizationPropertyInfo, IReadOnlyLocalizationProperty
    {
        protected IReadOnlyLocalizationConfig Config { get; }

        public event EventHandler? Reseted;
        public event LocalizationValueChangedEventHandler? Changed;
        public override event LocalizationChangedEventHandler? LocalizationChanged;

        public override String Path
        {
            get
            {
                return Config.Path;
            }
        }

        private event ConfigurationChangedEventHandler<ILocalizationString?>? ConfigurationChanged;
        event ConfigurationChangedEventHandler<ILocalizationString?>? IReadOnlyConfigProperty<ILocalizationString?>.Changed
        {
            add
            {
                ConfigurationChanged += value;
            }
            remove
            {
                ConfigurationChanged -= value;
            }
        }
        
        public override LocalizationIdentifier Identifier
        {
            get
            {
                return Config.Localization;
            }
        }

        public override ILocalizationString? Value
        {
            get
            {
                return GetValue();
            }
        }

        public override String? Current
        {
            get
            {
                return HasValue ? Internal.Value?.ToString() ?? Alternate?.ToString() : Alternate?.ToString();
            }
        }
        
        public virtual Func<ILocalizationString?, Boolean>? Validate
        {
            get
            {
                return null;
            }
        }

        public Boolean IsValid
        {
            get
            {
                return Validate?.Invoke(Internal.Value) != false;
            }
        }

        public virtual TryConverter<String?, ILocalizationString?> Converter
        {
            get
            {
                return ConvertUtilities.TryConvert;
            }
        }

        protected override event PropertyChangedEventHandler? PropertyChanged;
        
        protected internal ReadOnlyLocalizationProperty(IReadOnlyLocalizationConfig config, String? key, String? alternate, ConfigPropertyOptions options, IEnumerable<String>? sections)
            : this(config ?? throw new ArgumentNullException(nameof(config)), key, LocalizationString.Create(config.Default, alternate), options, sections)
        {
        }

        protected internal ReadOnlyLocalizationProperty(IReadOnlyLocalizationConfig config, String? key, ILocalizationString? alternate, ConfigPropertyOptions options, IEnumerable<String>? sections)
            : base(key, alternate, options | ConfigPropertyOptions.ReadOnly, sections)
        {
            Config = config ?? throw new ArgumentNullException(nameof(config));
            Config.Changed += OnLocalizationChanged;
            Config.ValueChanged += OnChanged;
        }

        protected virtual void OnLocalizationChanged(Object? sender, LocalizationChangedEventArgs args)
        {
            LocalizationChanged?.Invoke(this, args);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Current)));
        }

        protected virtual void OnChanged(LocalizationValueChangedEventArgs args)
        {
            Changed?.Invoke(this, args);

            (String? key, LocalizationIdentifier identifier, ImmutableArray<String> sections) = args.Value;
            IEnumerable<String>? configuration = sections;
            key = Config.Converter.Convert(key, identifier, ref configuration, Config.LocalizationOptions);
            ConfigurationValueEntry<ILocalizationString?> entry = new ConfigurationValueEntry<ILocalizationString?>(key, Value, configuration);
            ConfigurationChanged?.Invoke(this, new ConfigurationChangedEventArgs<ILocalizationString?>(entry, args.Handled));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Current)));
        }
        
        protected virtual void OnChanged(String? value)
        {
            OnChanged(new LocalizationValueChangedEventArgs(new LocalizationValueEntry(Key, Identifier, value, Sections)));
        }

        protected virtual void OnChanged(ILocalizationString? value)
        {
            Reseted?.Invoke(this, EventArgs.Empty);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Current)));
        }

        protected virtual void OnChanged(Object? sender, LocalizationValueChangedEventArgs args)
        {
            if (IsIgnoreEvent || args.Handled)
            {
                return;
            }

            (String? key, LocalizationIdentifier identifier, String? value, ImmutableArray<String> sections) = args.Value;
            if (key != Key || !sections.SequenceEqual(Sections))
            {
                return;
            }

            if (!Internal.TryGetValue(out ILocalizationString? localization))
            {
                return;
            }

            IMutableLocalizationString? mutable = localization as IMutableLocalizationString ?? localization?.ToMutable();
            
            mutable?.Set(identifier, value);
            OnChanged(args);
        }

        protected override ILocalizationString? Initialize()
        {
            return !IsAlwaysDefault ? GetValueInternal() : Alternate;
        }
        
        protected virtual ILocalizationString? GetValueInternal()
        {
            return Config.GetValue(Key, Alternate, Sections);
        }
        
        protected virtual Task<ILocalizationString?> GetValueInternalAsync(CancellationToken token)
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

        public virtual ILocalizationString? GetValue()
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

        public virtual ILocalizationString? GetValue(Func<ILocalizationString?, Boolean>? predicate)
        {
            if (IsAlwaysDefault)
            {
                return Alternate;
            }
            
            ILocalizationString? value = GetValue();
            return predicate?.Invoke(value) != false ? value : Alternate;
        }

        public Task<ILocalizationString?> GetValueAsync()
        {
            return GetValueAsync(CancellationToken.None);
        }

        public virtual async Task<ILocalizationString?> GetValueAsync(CancellationToken token)
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

        public Task<ILocalizationString?> GetValueAsync(Func<ILocalizationString?, Boolean>? predicate)
        {
            return GetValueAsync(CancellationToken.None);
        }

        public virtual async Task<ILocalizationString?> GetValueAsync(Func<ILocalizationString?, Boolean>? predicate, CancellationToken token)
        {
            if (IsAlwaysDefault)
            {
                return Alternate;
            }
            
            ILocalizationString? value = await GetValueAsync(token);
            return predicate?.Invoke(value) != false ? value : Alternate;
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
            
            ILocalizationString? value = GetValueInternal() ?? Alternate;
            
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
            
            ILocalizationString? value = await GetValueInternalAsync(token) ?? Alternate;

            Internal.Reset(value);
            OnChanged(value);
            return true;
        }

        public override String? ToString()
        {
            ILocalizationString? value = Value;
            return value is not null ? new LocalizationValueEntry(Key, Identifier, value.Text, Sections).GetString() : null;
        }

        public String ToString(String? format, IFormatProvider? provider)
        {
            ILocalizationString? value = Value;
            return value is not null ? new LocalizationValueEntry(Key, Identifier, value.Text, Sections).GetString(format, provider) ?? String.Empty : String.Empty;
        }

        protected override void Dispose(Boolean disposing)
        {
            LocalizationChanged = null;
            Changed = null;
            ConfigurationChanged = null;
            PropertyChanged = null;
            Config.Changed -= OnLocalizationChanged;
            Config.ValueChanged -= OnChanged;
            base.Dispose();
        }
    }
    
    public class ReadOnlyLocalizationIdentifierProperty : LocalizationIdentifierPropertyInfo, IReadOnlyLocalizationIdentifierProperty
    {
        protected IReadOnlyLocalizationConfig Config { get; }
        
        public event LocalizationValueChangedEventHandler? Changed;
        public override event LocalizationChangedEventHandler? LocalizationChanged;

        public override String Path
        {
            get
            {
                return Config.Path;
            }
        }

        private event ConfigurationChangedEventHandler? ConfigurationChanged;
        event ConfigurationChangedEventHandler? IReadOnlyConfigProperty.Changed
        {
            add
            {
                ConfigurationChanged += value;
            }
            remove
            {
                ConfigurationChanged -= value;
            }
        }

        public override String? Value
        {
            get
            {
                return GetValue();
            }
        }

        protected override event PropertyChangedEventHandler? PropertyChanged;

        protected internal ReadOnlyLocalizationIdentifierProperty(IReadOnlyLocalizationConfig config, String? key, LocalizationIdentifier identifier, String? alternate, ConfigPropertyOptions options, IEnumerable<String>? sections)
            : base(key, identifier, alternate, options | ConfigPropertyOptions.ReadOnly, sections)
        {
            Config = config ?? throw new ArgumentNullException(nameof(config));
            Config.Changed += OnLocalizationChanged;
            Config.ValueChanged += OnChanged;
        }

        protected virtual void OnLocalizationChanged(Object? sender, LocalizationChangedEventArgs args)
        {
            LocalizationChanged?.Invoke(this, args);
        }

        protected virtual void OnChanged(LocalizationValueChangedEventArgs args)
        {
            Changed?.Invoke(this, args);
            ConfigurationChanged?.Invoke(this, new ConfigurationChangedEventArgs(args.Value, args.Handled));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
        }
        
        protected virtual void OnChanged(String? value)
        {
            OnChanged(new LocalizationValueChangedEventArgs(new LocalizationValueEntry(Key, Identifier, value, Sections)));
        }

        protected virtual void OnChanged(Object? sender, LocalizationValueChangedEventArgs args)
        {
            if (IsIgnoreEvent || args.Handled)
            {
                return;
            }

            (String? key, LocalizationIdentifier identifier, String? value, ImmutableArray<String> sections) = args.Value;
            if (key != Key || identifier != Identifier || Internal.IsValueCreated && value == Internal.Value || !sections.SequenceEqual(Sections))
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
            return Config.GetValue(Key, Identifier, Alternate, Sections);
        }
        
        protected virtual Task<String?> GetValueInternalAsync(CancellationToken token)
        {
            return Config.GetValueAsync(Key, Identifier, Alternate, Sections, token);
        }

        protected virtual Boolean KeyExistInternal()
        {
            return Config.KeyExist(Key, Identifier, Sections);
        }

        protected virtual Task<Boolean> KeyExistInternalAsync(CancellationToken token)
        {
            return Config.KeyExistAsync(Key, Identifier, Sections, token);
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
            return new LocalizationValueEntry(Key, Identifier, Value, Sections).GetString();
        }

        public String ToString(String? format, IFormatProvider? provider)
        {
            return new LocalizationValueEntry(Key, Identifier, Value, Sections).GetString(format, provider) ?? String.Empty;
        }

        protected override void Dispose(Boolean disposing)
        {
            LocalizationChanged = null;
            Changed = null;
            ConfigurationChanged = null;
            PropertyChanged = null;
            Config.Changed -= OnLocalizationChanged;
            Config.ValueChanged -= OnChanged;
            base.Dispose();
        }
    }
}