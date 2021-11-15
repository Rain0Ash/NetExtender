// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.Property.Interfaces;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.Interfaces;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.Types;

namespace NetExtender.Configuration.Property
{
    //TODO: refactoring
    public class ConfigProperty<T> : ConfigPropertyBase, IConfigProperty<T>, IReadOnlyValidable<T>, IFormattable
    {
        public static implicit operator T(ConfigProperty<T> property)
        {
            return property.GetValue();
        }

        public static explicit operator String(ConfigProperty<T> property)
        {
            return property.ToString();
        }

        public event EventHandler<T> ValueChanged = null!; 
        
        private new IPropertyConfig Config { get; }
        
        public Boolean ThrowOnInvalid { get; set; }
        public Boolean ThrowOnReadOnly { get; set; }

        private Boolean Initialized { get; set; }
        
        public T Alternate { get; set; }

        protected T InternalValue;

        public T Value
        {
            get
            {
                Initialize();
                return IsValid && !AlwaysDefault ? InternalValue : Alternate;
            }
            protected set
            {
                if (IsReadOnly)
                {
                    if (ThrowOnReadOnly)
                    {
                        throw new ReadOnlyException();
                    }
                    
                    return;
                }
                
                Boolean valid = Validate?.Invoke(value) != false;
                
                if (!valid && ThrowOnInvalid)
                {
                    throw new ArgumentException(@"Value is invalid", nameof(Value));
                }

                InternalValue = value;
                ValueChanged?.Invoke(this, value);
                IsValid = valid;
            }
        }

        public Boolean IsValid { get; private set; }

        public Func<T, Boolean> Validate { get; }
        
        public TryConverter<String, T?> Converter { get; set; }

        protected internal ConfigProperty(IPropertyConfig config, String key, T alternate, Func<T, Boolean> validate, ICryptKey crypt, ConfigPropertyOptions options, TryConverter<String, T?>? converter, IEnumerable<String> sections)
            : base(config, key ?? throw new ArgumentNullException(nameof(key)), crypt, options, sections)
        {
            Config = config ?? throw new ArgumentNullException(nameof(config));
            Alternate = alternate;
            Validate = validate;
            Converter = converter ?? ConvertUtilities.TryConvert;
        }

        private Boolean Initialize()
        {
            if (Initialized)
            {
                return false;
            }

            Read();
            
            Initialized = true;
            return Initialized;
        }

        protected void SetValueInternal(T value)
        {
            InternalValue = value;
            ValueChanged?.Invoke(this, value);
            IsValid = Validate?.Invoke(value) != false;
        }
        
        public void SetValue(T value)
        {
            Value = value;

            if (!Caching)
            {
                Save();
            }
        }
        
        protected T GetValueInternal()
        {
            return InternalValue;
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

            return validate?.Invoke(InternalValue) != false ? InternalValue : Alternate;
        }

        public T GetOrSetValue()
        {
            if (!Caching)
            {
                SetValueInternal(Config.GetOrSetValue(this));
                return Value;
            }

            Read();
            return Value;
        }
        
        public void ResetValue()
        {
            Value = Alternate;
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
            SetValueInternal(Config.GetValue(this));
        }

        public override Boolean KeyExist()
        {
            return Config.KeyExist(Key, Sections);
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