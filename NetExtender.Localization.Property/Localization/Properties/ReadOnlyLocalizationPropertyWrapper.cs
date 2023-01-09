// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
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
using NetExtender.Localization.Properties;
using NetExtender.Types.Culture;

namespace NetExtender.Localization.Properties
{
    public sealed class ReadOnlyLocalizationPropertyWrapper : IReadOnlyLocalizationProperty
    {
        private ILocalizationProperty Internal { get; }

        public event EventHandler? StringChanged
        {
            add
            {
                Internal.StringChanged += value;
            }
            remove
            {
                Internal.StringChanged -= value;
            }
        }

        public event LocalizationValueChangedEventHandler? Changed
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

        event LocalizationValueChangedEventHandler? ILocalizationPropertyMultiInfo.Changed
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

        public String Current
        {
            get
            {
                return Internal.Current;
            }
        }

        event ConfigurationChangedEventHandler<ILocalizationString?>? IConfigPropertyValueInfo<ILocalizationString?>.Changed
        {
            add
            {
                ((IConfigProperty<ILocalizationString?>) Internal).Changed += value;
            }
            remove
            {
                ((IConfigProperty<ILocalizationString?>) Internal).Changed -= value;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged
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

        public event LocalizationChangedEventHandler? LocalizationChanged
        {
            add
            {
                Internal.LocalizationChanged += value;
            }
            remove
            {
                Internal.LocalizationChanged -= value;
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

        public LocalizationIdentifier Identifier
        {
            get
            {
                return Internal.Identifier;
            }
        }

        public Int32 Count
        {
            get
            {
                return Internal.Count;
            }
        }

        public ILocalizationString? Value
        {
            get
            {
                return Internal.Value;
            }
        }

        public ILocalizationString? Alternate
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

        public Func<ILocalizationString?, Boolean>? Validate
        {
            get
            {
                return Internal.Validate;
            }
        }

        public TryConverter<String?, ILocalizationString?> Converter
        {
            get
            {
                return Internal.Converter;
            }
        }

        private Boolean Disposing { get; }

        internal ReadOnlyLocalizationPropertyWrapper(ILocalizationConfig config, String? key, String? alternate, ConfigPropertyOptions options, IEnumerable<String>? sections)
            : this(new LocalizationProperty(config, key, alternate, options, sections), true)
        {
        }


        internal ReadOnlyLocalizationPropertyWrapper(ILocalizationConfig config, String? key, ILocalizationString? alternate, ConfigPropertyOptions options, IEnumerable<String>? sections)
            : this(new LocalizationProperty(config, key, alternate, options, sections), true)
        {
        }


        internal ReadOnlyLocalizationPropertyWrapper(ILocalizationConfig config, String? key, IEnumerable<KeyValuePair<LocalizationIdentifier, String>>? alternate, ConfigPropertyOptions options, IEnumerable<String>? sections)
            : this(new LocalizationProperty(config, key, alternate, options, sections), true)
        {
        }


        internal ReadOnlyLocalizationPropertyWrapper(ILocalizationConfig config, String? key, IEnumerable<LocalizationValueEntry>? alternate, ConfigPropertyOptions options, IEnumerable<String>? sections)
            : this(new LocalizationProperty(config, key, alternate, options, sections), true)
        {
        }

        internal ReadOnlyLocalizationPropertyWrapper(ILocalizationProperty property)
            : this(property, false)
        {
        }

        internal ReadOnlyLocalizationPropertyWrapper(ILocalizationProperty property, Boolean disposing)
        {
            Internal = property ?? throw new ArgumentNullException(nameof(property));
            Disposing = disposing;
        }

        public ILocalizationString? GetValue()
        {
            return Internal.GetValue();
        }

        public ILocalizationString? GetValue(Func<ILocalizationString?, Boolean>? predicate)
        {
            return Internal.GetValue(predicate);
        }

        public Task<ILocalizationString?> GetValueAsync()
        {
            return Internal.GetValueAsync();
        }

        public Task<ILocalizationString?> GetValueAsync(CancellationToken token)
        {
            return Internal.GetValueAsync(token);
        }

        public Task<ILocalizationString?> GetValueAsync(Func<ILocalizationString?, Boolean>? predicate)
        {
            return Internal.GetValueAsync(predicate);
        }

        public Task<ILocalizationString?> GetValueAsync(Func<ILocalizationString?, Boolean>? predicate, CancellationToken token)
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

        public String? this[LocalizationIdentifier identifier]
        {
            get
            {
                return Internal[identifier];
            }
        }

        public void Dispose()
        {
            if (Disposing)
            {
                Internal.Dispose();
            }
        }
    }

    public sealed class ReadOnlyLocalizationIdentifierPropertyWrapper : IReadOnlyLocalizationIdentifierProperty
    {
        private ILocalizationIdentifierProperty Internal { get; }

        public event LocalizationChangedEventHandler? LocalizationChanged
        {
            add
            {
                Internal.LocalizationChanged += value;
            }
            remove
            {
                Internal.LocalizationChanged -= value;
            }
        }

        event ConfigurationChangedEventHandler? IConfigPropertyValueInfo.Changed
        {
            add
            {
                ((IConfigProperty) Internal).Changed += value;
            }
            remove
            {
                ((IConfigProperty) Internal).Changed -= value;
            }
        }

        public event LocalizationValueChangedEventHandler? Changed
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

        public event PropertyChangedEventHandler? PropertyChanged
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

        public LocalizationIdentifier Identifier
        {
            get
            {
                return Internal.Identifier;
            }
        }

        public String? Value
        {
            get
            {
                return Internal.Value;
            }
        }

        public String Current
        {
            get
            {
                return Internal.Current;
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

        internal ReadOnlyLocalizationIdentifierPropertyWrapper(ILocalizationConfig config, String? key, LocalizationIdentifier identifier, String? alternate, ConfigPropertyOptions options, IEnumerable<String>? sections)
            : this(new LocalizationIdentifierProperty(config, key, identifier, alternate, options, sections), true)
        {
        }

        internal ReadOnlyLocalizationIdentifierPropertyWrapper(ILocalizationIdentifierProperty property)
            : this(property, false)
        {
        }

        internal ReadOnlyLocalizationIdentifierPropertyWrapper(ILocalizationIdentifierProperty property, Boolean disposing)
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

        public String ToString(IFormatProvider? provider)
        {
            return Internal.ToString(null, provider);
        }

        public String ToString(String? format, IFormatProvider? provider)
        {
            return Internal.ToString(format, provider);
        }

        public String? this[LocalizationIdentifier identifier]
        {
            get
            {
                return Internal.Identifier == identifier ? Internal.Current : null;
            }
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