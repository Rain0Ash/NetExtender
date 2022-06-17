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
    public class LocalizationProperty : LocalizationPropertyInfo, ILocalizationProperty
    {
        protected ILocalizationConfig Config { get; }

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
        event ConfigurationChangedEventHandler<ILocalizationString?>? IConfigProperty<ILocalizationString?>.Changed
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

        public virtual ILocalizationString? Value
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
        
        protected internal LocalizationProperty(ILocalizationConfig config, String? key, String? alternate, ConfigPropertyOptions options, IEnumerable<String>? sections)
            : this(config ?? throw new ArgumentNullException(nameof(config)), key, alternate is not null ? LocalizationString.Create(config.Default, alternate) : null, options, sections)
        {
        }

        protected internal LocalizationProperty(ILocalizationConfig config, String? key, ILocalizationString? alternate, ConfigPropertyOptions options, IEnumerable<String>? sections)
            : base(key, alternate, options | ConfigPropertyOptions.ReadOnly, sections)
        {
            Config = config ?? throw new ArgumentNullException(nameof(config));
            Config.Changed += OnLocalizationChanged;
            Config.ValueChanged += OnChanged;
        }

        protected virtual void OnLocalizationChanged(Object? sender, LocalizationChangedEventArgs args)
        {
            LocalizationChanged?.Invoke(this, args);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Identifier)));
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
        
        protected virtual Boolean SetValueInternal()
        {
            return Config.SetValue(Key, Internal.Value, Sections);
        }
        
        protected virtual Task<Boolean> SetValueInternalAsync(CancellationToken token)
        {
            return Config.SetValueAsync(Key, Internal.Value, Sections, token);
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

        public virtual Boolean SetValue(ILocalizationString? value)
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

        public Task<Boolean> SetValueAsync(ILocalizationString? value)
        {
            return SetValueAsync(value, CancellationToken.None);
        }

        public virtual async Task<Boolean> SetValueAsync(ILocalizationString? value, CancellationToken token)
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
                await SaveAsync(token);
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

        public virtual Boolean Save()
        {
            if (IsReadOnly || IsDisableSave || !Internal.IsValueCreated)
            {
                return false;
            }

            return SetValueInternal();
        }

        public Task<Boolean> SaveAsync()
        {
            return SaveAsync(CancellationToken.None);
        }

        public virtual Task<Boolean> SaveAsync(CancellationToken token)
        {
            if (IsReadOnly || IsDisableSave || !Internal.IsValueCreated)
            {
                return TaskUtilities.False;
            }

            return SetValueInternalAsync(token);
        }

        public virtual Boolean Reset()
        {
            if (IsReadOnly)
            {
                return false;
            }
            
            Internal.Reset(null);
            Save();
            return true;
        }

        public Task<Boolean> ResetAsync()
        {
            return ResetAsync(CancellationToken.None);
        }

        public virtual async Task<Boolean> ResetAsync(CancellationToken token)
        {
            if (IsReadOnly)
            {
                return false;
            }
            
            Internal.Reset(null);
            await SaveAsync(CancellationToken.None);
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
    
    public class LocalizationIdentifierProperty : LocalizationIdentifierPropertyInfo, ILocalizationIdentifierProperty
    {
        protected ILocalizationConfig Config { get; }
        
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
        event ConfigurationChangedEventHandler? IConfigProperty.Changed
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

        protected override event PropertyChangedEventHandler? PropertyChanged;

        protected internal LocalizationIdentifierProperty(ILocalizationConfig config, String? key, LocalizationIdentifier identifier, String? alternate, ConfigPropertyOptions options, IEnumerable<String>? sections)
            : base(key, identifier, alternate, options, sections)
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
        
        protected virtual Boolean SetValueInternal()
        {
            return Config.SetValue(Key, Identifier, Internal.Value, Sections);
        }
        
        protected virtual Task<Boolean> SetValueInternalAsync(CancellationToken token)
        {
            return Config.SetValueAsync(Key, Identifier, Internal.Value, Sections, token);
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
                await SaveAsync(token);
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

        public virtual Boolean Save()
        {
            if (IsReadOnly || IsDisableSave || !Internal.IsValueCreated)
            {
                return false;
            }

            return SetValueInternal();
        }

        public Task<Boolean> SaveAsync()
        {
            return SaveAsync(CancellationToken.None);
        }

        public virtual Task<Boolean> SaveAsync(CancellationToken token)
        {
            if (IsReadOnly || IsDisableSave || !Internal.IsValueCreated)
            {
                return TaskUtilities.False;
            }

            return SetValueInternalAsync(token);
        }

        public virtual Boolean Reset()
        {
            if (IsReadOnly)
            {
                return false;
            }
            
            Internal.Reset(null);
            Save();
            return true;
        }

        public Task<Boolean> ResetAsync()
        {
            return ReadAsync(CancellationToken.None);
        }

        public virtual async Task<Boolean> ResetAsync(CancellationToken token)
        {
            if (IsReadOnly)
            {
                return false;
            }
            
            Internal.Reset(null);
            await SaveAsync(CancellationToken.None);
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