using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public sealed class LocalizationStringInfoWrapper : ILocalizationStringInfo
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        
        public event LocalizationChangedEventHandler? LocalizationChanged
        {
            add
            {
                if (Config is not null)
                {
                    Config.Changed += value;
                }
            }
            remove
            {
                if (Config is not null)
                {
                    Config.Changed -= value;
                }
            }
        }

        private ILocalizationConfigInfo? Config { get; }

        public LocalizationIdentifier Identifier
        {
            get
            {
                return Config?.Localization ?? LocalizationIdentifier.Default;
            }
        }

        private ILocalizationString Value { get; }

        public String Current
        {
            get
            {
                return ToString();
            }
        }

        Boolean IString.Immutable
        {
            get
            {
                return true;
            }
        }

        Boolean IString.Constant
        {
            get
            {
                return false;
            }
        }

        Int32 IString.Length
        {
            get
            {
                return Current.Length;
            }
        }

        String IString.Text
        {
            get
            {
                return Current;
            }
        }

        public LocalizationStringInfoWrapper(String value)
            : this(null, value)
        {
        }

        public LocalizationStringInfoWrapper(ILocalizationString value)
            : this(null, value)
        {
        }

        public LocalizationStringInfoWrapper(ILocalizationConfigInfo? config, String value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            Config = config;
            Value = config is null ? LocalizationString.Create(value) : new LocalizationString(config, new []{ new KeyValuePair<LocalizationIdentifier, String>(LocalizationIdentifier.Default, value) });
            LocalizationChanged += OnChanged;
        }

        public LocalizationStringInfoWrapper(ILocalizationConfigInfo? config, ILocalizationString value)
        {
            Config = config;
            Value = value ?? throw new ArgumentNullException(nameof(value));
            LocalizationChanged += OnChanged;
        }

        private void OnChanged(Object? sender, LocalizationChangedEventArgs args)
        {
            this.RaisePropertyChanged(nameof(Current));
        }

        public override String ToString()
        {
            return Value.ToString();
        }

        public String ToString(String? format)
        {
            return Value.ToString(format, null);
        }

        public String ToString(IFormatProvider? provider)
        {
            return Value.ToString(provider);
        }

        public String ToString(String? format, IFormatProvider? provider)
        {
            return Value.ToString(format, provider);
        }
    }
}