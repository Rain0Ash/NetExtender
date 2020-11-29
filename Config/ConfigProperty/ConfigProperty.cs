// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Converters;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.Interfaces;
using NetExtender.Utils.Types;
using ReactiveUI.Fody.Helpers;

namespace NetExtender.Config
{
    [Flags]
    public enum ConfigPropertyOptions
    {
        None = 0,
        Caching = 1,
        ReadOnly = 2,
        AlwaysDefault = 4,
        DisableSave = 8,
    }
    
    public sealed class ConfigProperty<T> : ConfigPropertyBase, IConfigProperty<T>, IReadOnlyValidable<T>, IFormattable
    {
        public static implicit operator T(ConfigProperty<T> property)
        {
            return property.GetValue();
        }

        public static explicit operator String(ConfigProperty<T> property)
        {
            return property.ToString();
        }

        public Boolean ThrowOnInvalid { get; set; }

        [Reactive]
        public T DefaultValue { get; private set; }

        private Boolean _initialized;
        private T _value;

        [Reactive]
        public T Value
        {
            get
            {
                Initialize();
                return IsValid && !AlwaysDefault ? _value : DefaultValue;
            }
            private set
            {
                if (IsReadOnly)
                {
                    return;
                }
                
                if (_value.IsEquals(value))
                {
                    return;
                }
                
                Boolean valid = Validate?.Invoke(_value) != false;
                
                if (!valid && ThrowOnInvalid)
                {
                    throw new ArgumentException(@"Value is invalid", nameof(Value));
                }
                
                _value = value;

                IsValid = valid;
            }
        }

        [Reactive]
        public Boolean IsValid { get; private set; }

        public Func<T, Boolean> Validate { get; }
        
        public TryConverter<String, T> Converter { get; set; }

        internal ConfigProperty(Config config, String key, T defaultValue, Func<T, Boolean> validate, ICryptKey crypt, ConfigPropertyOptions options, TryConverter<String, T> converter, params String[] sections)
            : base(config, key, crypt, options, sections)
        {
            DefaultValue = defaultValue;
            Validate = validate;
            Converter = converter ?? ConvertUtils.TryConvert;
        }

        private void Initialize()
        {
            if (_initialized)
            {
                return;
            }

            Read();
            _initialized = true;
        }

        public void SetValue(T value)
        {
            Value = value;

            if (!Caching)
            {
                Save();
            }
        }

        public T GetValue()
        {
            if (!Caching)
            {
                Read();
            }

            return Value;
        }
        
        public T GetValue(Func<T, Boolean> validate)
        {
            if (!Caching)
            {
                Read();
            }

            return validate?.Invoke(_value) != false ? _value : DefaultValue;
        }

        public T GetOrSetValue()
        {
            if (!Caching)
            {
                Value = Config.GetOrSetValue(this);
                return Value;
            }

            Read();
            return Value;
        }

        public void ChangeDefaultValue(T value, Boolean changeValue = true)
        {
            if (changeValue && Value.IsEquals(DefaultValue))
            {
                Value = value;
            }

            DefaultValue = value;
        }

        public void ResetValue()
        {
            Value = DefaultValue;
        }

        public void RemoveValue()
        {
            if (!Caching)
            {
                Config.RemoveValue(Key, Sections);
            }

            ResetValue();
        }

        public override void Save()
        {
            if (DisableSave)
            {
                return;
            }
            
            Config.SetValue(this, Value);
        }

        public override void Read()
        {
            Value = Config.GetValue(this);
        }

        public override void Reset()
        {
            ResetValue();
            Save();
        }

        public override String ToString()
        {
            return GetValue().GetString();
        }

        public String ToString(String? format, IFormatProvider? provider)
        {
            return GetValue().GetString(provider);
        }
    }
}