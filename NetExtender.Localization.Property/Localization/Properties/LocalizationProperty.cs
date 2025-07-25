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
using NetExtender.Types.Converters;
using NetExtender.Types.Converters.Interfaces;
using NetExtender.Localization.Common;
using NetExtender.Localization.Common.Interfaces;
using NetExtender.Localization.Events;
using NetExtender.Localization.Interfaces;
using NetExtender.Localization.Properties.Interfaces;
using NetExtender.Types.Culture;
using NetExtender.Types.Strings.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Localization.Properties
{
    public class LocalizationProperty : LocalizationPropertyInfo, ILocalizationProperty
    {
        protected internal ILocalizationConfig Config { get; }

        public event EventHandler? StringChanged;
        public event LocalizationValueChangedEventHandler? Changed;
        public override event LocalizationChangedEventHandler? LocalizationChanged;

        private event ConfigurationChangedEventHandler<ILocalizationString?>? ConfigurationChanged;
        event ConfigurationChangedEventHandler<ILocalizationString?>? IConfigPropertyValueInfo<ILocalizationString?>.Changed
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

        public sealed override String Path
        {
            get
            {
                return Config.Path;
            }
        }

        public sealed override Boolean IsThreadSafe
        {
            get
            {
                return Config.IsThreadSafe;
            }
        }

        public sealed override LocalizationIdentifier Identifier
        {
            get
            {
                return Config.Localization;
            }
        }

        public Int32 Count
        {
            get
            {
                return Value?.Count ?? Alternate?.Count ?? 0;
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
                return Validate?.Invoke(Internal.Value) is not false;
            }
        }

        public virtual ITwoWayConverter<String?, ILocalizationString?> Converter
        {
            get
            {
                return TwoWayConverter<String?, ILocalizationString?>.Default;
            }
        }
        
        protected override event PropertyChangedEventHandler? PropertyChanged;

        protected internal LocalizationProperty(LocalizationIdentifierProperty property, ILocalizationString? alternate)
            : this(property, property.Config, alternate)
        {
        }

        protected internal LocalizationProperty(ILocalizationIdentifierProperty property, ILocalizationConfig config, ILocalizationString? alternate)
            : base(property is not null ? property.Key : throw new ArgumentNullException(nameof(property)), alternate, property.Options, property.Sections)
        {
            Config = config ?? throw new ArgumentNullException(nameof(config));
            Config.Changed += OnLocalizationChanged;
            Config.ValueChanged += OnChanged;
        }

        protected internal LocalizationProperty(ILocalizationConfig config, String? key, String? alternate, ConfigPropertyOptions options, IEnumerable<String>? sections)
            : this(config ?? throw new ArgumentNullException(nameof(config)), key, alternate is not null ? LocalizationString.Create(config.Default, alternate) : null, options, sections)
        {
        }

        protected internal LocalizationProperty(ILocalizationConfig config, String? key, ILocalizationString? alternate, ConfigPropertyOptions options, IEnumerable<String>? sections)
            : base(key, alternate, options, sections)
        {
            Config = config ?? throw new ArgumentNullException(nameof(config));
            Config.Changed += OnLocalizationChanged;
            Config.ValueChanged += OnChanged;
        }

        protected internal LocalizationProperty(ILocalizationConfig config, String? key, IEnumerable<KeyValuePair<LocalizationIdentifier, String>>? alternate, ConfigPropertyOptions options, IEnumerable<String>? sections)
            : base(key, alternate is not null ? new LocalizationString(config ?? throw new ArgumentNullException(nameof(config)), alternate) : null, options, sections)
        {
            Config = config ?? throw new ArgumentNullException(nameof(config));
            Config.Changed += OnLocalizationChanged;
            Config.ValueChanged += OnChanged;
        }

        protected internal LocalizationProperty(ILocalizationConfig config, String? key, IEnumerable<LocalizationValueEntry>? alternate, ConfigPropertyOptions options, IEnumerable<String>? sections)
            : base(key, alternate is not null ? new LocalizationString(config ?? throw new ArgumentNullException(nameof(config)), alternate) : null, options, sections)
        {
            Config = config ?? throw new ArgumentNullException(nameof(config));
            Config.Changed += OnLocalizationChanged;
            Config.ValueChanged += OnChanged;
        }

        protected virtual void OnLocalizationChanged(Object? sender, LocalizationChangedEventArgs args)
        {
            LocalizationChanged?.Invoke(this, args);
            PropertyChanged?.Invoke(this, new PropertyChanged(nameof(Identifier)));
            PropertyChanged?.Invoke(this, new PropertyChanged(nameof(Current)));
        }

        protected virtual void OnChanged(LocalizationValueChangedEventArgs args)
        {
            Changed?.Invoke(this, args);

            (String? key, LocalizationIdentifier identifier, ImmutableArray<String> sections) = args.Value;
            IEnumerable<String>? configuration = sections;
            key = Config.Converter.Convert(key, identifier, ref configuration, Config.LocalizationOptions);
            ConfigurationValueEntry<ILocalizationString?> entry = new ConfigurationValueEntry<ILocalizationString?>(key, Value, configuration);
            ConfigurationChanged?.Invoke(this, new ConfigurationChangedEventArgs<ILocalizationString?>(entry, args.Handled));
            PropertyChanged?.Invoke(this, new PropertyChanged(nameof(Value)));
            PropertyChanged?.Invoke(this, new PropertyChanged(nameof(Current)));
        }

        protected virtual void OnChanged(String? value)
        {
            OnChanged(new LocalizationValueChangedEventArgs(new LocalizationValueEntry(Key, Identifier, value, Sections)));
        }

        protected virtual void OnChanged(ILocalizationString? value)
        {
            StringChanged?.Invoke(this, EventArgs.Empty);
            PropertyChanged?.Invoke(this, new PropertyChanged(nameof(Value)));
            PropertyChanged?.Invoke(this, new PropertyChanged(nameof(Current)));
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
            return !IsAlwaysDefault ? GetValueCore() : Alternate;
        }

        protected virtual ILocalizationString? GetValueCore()
        {
            return Config.GetValue(Key, Alternate, Sections);
        }

        protected virtual Task<ILocalizationString?> GetValueCoreAsync(CancellationToken token)
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
        
        ILocalizationString? IGetter<ILocalizationString?>.Get()
        {
            return GetValue();
        }

        public virtual ILocalizationString? GetValue(Func<ILocalizationString?, Boolean>? predicate)
        {
            if (IsAlwaysDefault)
            {
                return Alternate;
            }

            ILocalizationString? value = GetValue();
            return predicate?.Invoke(value) is not false ? value : Alternate;
        }

        public Task<ILocalizationString?> GetValueAsync()
        {
            return GetValueAsync(CancellationToken.None);
        }
        
        async ValueTask<ILocalizationString?> IAsyncGetter<ILocalizationString?>.GetAsync()
        {
            return await GetValueAsync();
        }

        public virtual async Task<ILocalizationString?> GetValueAsync(CancellationToken token)
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
        
        async ValueTask<ILocalizationString?> IAsyncGetter<ILocalizationString?>.GetAsync(CancellationToken token)
        {
            return await GetValueAsync(token);
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

            ILocalizationString? value = await GetValueAsync(token).ConfigureAwait(false);
            return predicate?.Invoke(value) is not false ? value : Alternate;
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

        void ISetter<ILocalizationString?>.Set(ILocalizationString? value)
        {
            SetValue(value);
        }

        public Task<Boolean> SetValueAsync(ILocalizationString? value)
        {
            return SetValueAsync(value, CancellationToken.None);
        }

        async ValueTask IAsyncSetter<ILocalizationString?>.SetAsync(ILocalizationString? value)
        {
            await SetValueAsync(value);
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
                await SaveAsync(token).ConfigureAwait(false);
            }

            OnChanged(Internal.Value);
            return true;
        }

        async ValueTask IAsyncSetter<ILocalizationString?>.SetAsync(ILocalizationString? value, CancellationToken token)
        {
            await SetValueAsync(value, token);
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

            ILocalizationString? value = GetValueCore() ?? Alternate;

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

            ILocalizationString? value = await GetValueCoreAsync(token).ConfigureAwait(false) ?? Alternate;

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

            return SetValueCore();
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
            ILocalizationString? value = Value;
            return value is not null ? new LocalizationValueEntry(Key, Identifier, value.Text, Sections).GetString() : null;
        }

        String IString.ToString()
        {
            return ToString() ?? String.Empty;
        }

        public override String ToString(IFormatProvider? provider)
        {
            ILocalizationString? value = Value;
            return value is not null ? new LocalizationValueEntry(Key, Identifier, value.Text, Sections).GetString(provider) ?? String.Empty : String.Empty;
        }

        public override String ToString(String? format, IFormatProvider? provider)
        {
            ILocalizationString? value = Value;
            return value is not null ? new LocalizationValueEntry(Key, Identifier, value.Text, Sections).GetString(format, provider) ?? String.Empty : String.Empty;
        }

        public String? this[LocalizationIdentifier identifier]
        {
            get
            {
                ILocalizationString? value = Value;

                if (value is not null && value.TryGetValue(identifier, out String? result))
                {
                    return result;
                }

                value = Alternate;
                return value is not null && value.TryGetValue(identifier, out result) ? result : null;
            }
        }

        protected override void Dispose(Boolean disposing)
        {
            LocalizationChanged = null;
            Changed = null;
            ConfigurationChanged = null;
            PropertyChanged = null;
            Config.Changed -= OnLocalizationChanged;
            Config.ValueChanged -= OnChanged;
        }
    }

    public class LocalizationIdentifierProperty : LocalizationIdentifierPropertyInfo, ILocalizationIdentifierProperty
    {
        protected internal ILocalizationConfig Config { get; }

        public override event LocalizationValueChangedEventHandler? Changed;
        public override event LocalizationChangedEventHandler? LocalizationChanged;
        
        private event ConfigurationChangedEventHandler? ConfigurationChanged;
        event ConfigurationChangedEventHandler? IConfigPropertyValueInfo.Changed
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

        public sealed override String Path
        {
            get
            {
                return Config.Path;
            }
        }
        
        public sealed override Boolean IsThreadSafe
        {
            get
            {
                return Config.IsThreadSafe;
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

        protected internal LocalizationIdentifierProperty(LocalizationProperty property, LocalizationIdentifier identifier, String? alternate)
            : this(property, property.Config, identifier, alternate)
        {
        }

        protected internal LocalizationIdentifierProperty(ILocalizationProperty property, ILocalizationConfig config, LocalizationIdentifier identifier, String? alternate)
            : base(property is not null ? property.Key : throw new ArgumentNullException(nameof(property)), identifier, alternate, property.Options, property.Sections)
        {
            Config = config ?? throw new ArgumentNullException(nameof(config));
            Config.Changed += OnLocalizationChanged;
            Config.ValueChanged += OnChanged;
        }

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
            PropertyChanged?.Invoke(this, new PropertyChanged(nameof(Value)));
            PropertyChanged?.Invoke(this, new PropertyChanged(nameof(Current)));
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
            return !IsAlwaysDefault ? GetValueCore() : Alternate;
        }

        protected virtual String? GetValueCore()
        {
            return Config.GetValue(Key, Identifier, Alternate, Sections);
        }

        protected virtual Task<String?> GetValueCoreAsync(CancellationToken token)
        {
            return Config.GetValueAsync(Key, Identifier, Alternate, Sections, token);
        }

        protected virtual Boolean KeyExistCore()
        {
            return Config.KeyExist(Key, Identifier, Sections);
        }

        protected virtual Task<Boolean> KeyExistCoreAsync(CancellationToken token)
        {
            return Config.KeyExistAsync(Key, Identifier, Sections, token);
        }

        protected virtual Boolean SetValueCore()
        {
            return Config.SetValue(Key, Identifier, Internal.Value, Sections);
        }

        protected virtual Task<Boolean> SetValueCoreAsync(CancellationToken token)
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
            if (IsReadOnly || IsDisableSave || !Internal.IsValueCreated)
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
            if (IsReadOnly || IsDisableSave || !Internal.IsValueCreated)
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
            return ReadAsync(CancellationToken.None);
        }

        public virtual Task<Boolean> ResetAsync(CancellationToken token)
        {
            return SetValueAsync(Alternate, token);
        }

        public override String? ToString()
        {
            return new LocalizationValueEntry(Key, Identifier, Value, Sections).GetString();
        }

        String IString.ToString()
        {
            return ToString() ?? String.Empty;
        }

        public override String ToString(IFormatProvider? provider)
        {
            return new LocalizationValueEntry(Key, Identifier, Value, Sections).GetString(provider) ?? String.Empty;
        }

        public override String ToString(String? format, IFormatProvider? provider)
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
        }
    }
}