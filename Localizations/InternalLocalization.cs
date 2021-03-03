// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using JetBrains.Annotations;
using NetExtender.Configuration;
using NetExtender.Configuration.Common;
using NetExtender.Cultures.Comparers;
using NetExtender.Localizations.Interfaces;
using NetExtender.Localizations.Sub.Interfaces;
using NetExtender.Types.Culture;
using NetExtender.Types.Strings.Interfaces;
using NetExtender.Utils.Types;
using ReactiveUI.Fody.Helpers;

namespace NetExtender.Localizations
{
    public static partial class Localization
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        private class InternalLocalization : Config, ILocalization
        {
            public event EmptyHandler SupportedLanguagesChanged;
            
            private event LanguageChangedHandler LanguageChanged;

            public event LanguageChangedHandler LanguageCultureChanged
            {
                add
                {
                    if (LanguageChanged.Contains(value))
                    {
                        return;
                    }

                    LanguageChanged += value;
                }
                remove
                {
                    LanguageChanged -= value;
                }
            }

            public CultureComparer Comparer { get; }

            [Reactive]
            public Boolean ChangeUIThreadLanguage { get; set; } = true;

            [Reactive]
            public Boolean UseSystemCulture { get; set; } = true;

            [Reactive]
            public CultureInfo Culture { get; private set; } = Default;

            public CultureInfo Standard
            {
                get
                {
                    return UseSystemCulture && Supported.ContainsKey(CultureUtils.System) ? CultureUtils.System : Default;
                }
            }

            protected Dictionary<CultureInfo, ISubLocalization> Supported { get; }

            public IOrderedEnumerable<CultureInfo> Languages
            {
                get
                {
                    return Supported.Keys.Sort(Comparer);
                }
            }

            private static (CultureComparer, Dictionary<CultureInfo, ISubLocalization>) Convert(ILocalizationBehaviour behaviour)
            {
                CultureComparer comparer = behaviour?.Comparer ?? new CultureComparer {Default};

                Dictionary<CultureInfo, ISubLocalization> supported;

                if (behaviour?.Supported is not null)
                {
                    supported = new Dictionary<CultureInfo, ISubLocalization>(behaviour.Supported);

                    if (!supported.ContainsKey(Default))
                    {
                        supported.Add(Default, null);
                    }
                }
                else
                {
                    supported = new Dictionary<CultureInfo, ISubLocalization>
                    {
                        {Default, null}
                    };
                }

                return (comparer, supported);
            }

            public InternalLocalization([NotNull] IConfigBehavior config, ILocalizationBehaviour behaviour = null)
                : base(config)
            {
                (Comparer, Supported) = Convert(behaviour);
            }

            public InternalLocalization(ConfigType type, ConfigOptions options, ILocalizationBehaviour behaviour = null)
                : base(type, options)
            {
                (Comparer, Supported) = Convert(behaviour);
            }

            public InternalLocalization(String path, ConfigType type, ConfigOptions options, ILocalizationBehaviour behaviour = null)
                : base(path, type, options)
            {
                (Comparer, Supported) = Convert(behaviour);
            }

            public Boolean AddSupportedCulture([NotNull] CultureInfo info)
            {
                return AddSupportedCulture(info, null);
            }

            public Boolean AddSupportedCulture([NotNull] CultureInfo info, [CanBeNull] ISubLocalization config)
            {
                if (info is null)
                {
                    throw new ArgumentNullException(nameof(info));
                }

                if (!Supported.TryAdd(info, config))
                {
                    return false;
                }
                
                SupportedLanguagesChanged?.Invoke();
                return true;
            }

            public Boolean RemoveSupportedCulture([NotNull] CultureInfo info)
            {
                if (info is null)
                {
                    throw new ArgumentNullException(nameof(info));
                }

                if (info.LCID == CultureUtils.Default || info.LCID == Default.LCID)
                {
                    return false;
                }

                if (!Supported.Remove(info))
                {
                    return false;
                }

                SupportedLanguagesChanged?.Invoke();
                return true;
            }

            public Boolean IsSupportCulture(CultureInfo info)
            {
                if (info is null)
                {
                    throw new ArgumentNullException(nameof(info));
                }

                if (info.LCID == CultureUtils.Default || info.LCID == Default.LCID)
                {
                    return true;
                }

                return Supported.ContainsKey(info);
            }

            public Boolean Update(UInt16 lcid)
            {
                return Update(CultureUtils.GetCultureInfo(lcid));
            }

            public Boolean Update(Int32 lcid)
            {
                return Update(CultureUtils.GetCultureInfo(lcid));
            }

            public Boolean Update(LCID lcid)
            {
                return Update(lcid.GetCultureInfo());
            }

            public Boolean Update(CultureLCID lcid)
            {
                return Update(lcid.GetCultureInfo());
            }

            public Boolean Update(CultureInfo info)
            {
                info ??= Default;

                if (info.IsCultureEquals(Culture))
                {
                    return false;
                }

                if (!Supported.ContainsKey(info))
                {
                    return false;
                }

                Culture = info;

                if (ChangeUIThreadLanguage)
                {
                    SetUILanguage();
                }

                LanguageChanged?.Invoke(Culture);
                return true;
            }

            public Boolean SetUILanguage()
            {
                UInt16 lcid;
                try
                {
                    lcid = Culture.IsCultureEquals(CultureInfo.InvariantCulture) ? Default.LCID16() : Culture.LCID16();
                }
                catch (Exception)
                {
                    lcid = Default.LCID16();
                }

                if (CultureUtils.SetUILanguage(lcid))
                {
                    return true;
                }

                Default.SetUILanguage();
                return false;
            }

            public Int32 GetLanguageOrderID()
            {
                return GetLanguageOrderID(Culture);
            }

            public Int32 GetLanguageOrderID(CultureInfo info)
            {
                return Comparer.GetOrder(info);
            }

            public ILocalizationProperty GetProperty(String key, IString value, IEnumerable<String> sections)
            {
                return new LocalizationProperty(this, Supported, key, value, sections);
            }

            private static IConfigProperty<T> GetOrAddProperty<T>(IConfigPropertyBase property)
            {
                if (ConfigPropertyObserver.GetOrAddProperty(property) is IConfigProperty<T> result)
                {
                    return result;
                }

                throw new ArgumentException(@$"Config already contains another property with same path '{property.Path}' and different generic type.", nameof(property));
            }

            [CanBeNull]
            public IEnumerable<IReadOnlyConfigPropertyBase> GetProperties()
            {
                return ConfigPropertyObserver.GetProperties(this);
            }

            private static void ReadProperty(IReadOnlyConfigPropertyBase property)
            {
                property?.Read();
            }

            private static void SaveProperty(IReadOnlyConfigPropertyBase property)
            {
                (property as IConfigPropertyBase)?.Save();
            }

            private static void ResetProperty(IReadOnlyConfigPropertyBase property)
            {
                (property as IConfigPropertyBase)?.Reset();
            }

            private static void ClearProperty(IReadOnlyConfigPropertyBase property)
            {
                (property as IConfigPropertyBase)?.Dispose();
            }

            public void RemoveProperty(IReadOnlyConfigPropertyBase property)
            {
                ConfigPropertyObserver.ThrowIfPropertyNotLinkedTo(this, property);
                ConfigPropertyObserver.RemoveProperty(property);
                ClearProperty(property);
            }

            private void ForEachProperty(Action<IReadOnlyConfigPropertyBase> action)
            {
                ConfigPropertyObserver.ForEachProperty(this, action);
            }

            public void ReadProperties()
            {
                ForEachProperty(ReadProperty);
            }

            public void SaveProperties()
            {
                ForEachProperty(SaveProperty);
            }

            public void ResetProperties()
            {
                ForEachProperty(ResetProperty);
            }

            public void ClearProperties()
            {
                ForEachProperty(ClearProperty);
                ConfigPropertyObserver.ClearProperties(this);
            }

            public Boolean SetValue<T>(IReadOnlyConfigProperty<T> property, T value)
            {
                ConfigPropertyObserver.ThrowIfPropertyNotLinked(property);
                return SetValue(property.Key, value, property.CryptKey, property.Sections);
            }

            public T GetValue<T>(IReadOnlyConfigProperty<T> property)
            {
                ConfigPropertyObserver.ThrowIfPropertyNotLinked(property);
                return GetValue(property.Key, property.DefaultValue, property.CryptKey, property.Converter, property.Sections);
            }

            public T GetOrSetValue<T>(IReadOnlyConfigProperty<T> property)
            {
                ConfigPropertyObserver.ThrowIfPropertyNotLinked(property);
                return GetOrSetValue(property, property.DefaultValue);
            }

            public T GetOrSetValue<T>(IReadOnlyConfigProperty<T> property, T value)
            {
                ConfigPropertyObserver.ThrowIfPropertyNotLinked(property);
                return GetOrSetValue(property.Key, value, property.CryptKey, property.Converter, property.Sections);
            }

            public Boolean KeyExist(IReadOnlyConfigPropertyBase property)
            {
                ConfigPropertyObserver.ThrowIfPropertyNotLinked(property);
                return KeyExist(property.Key, property.Sections);
            }

            public Boolean RemoveValue(IReadOnlyConfigPropertyBase property)
            {
                ConfigPropertyObserver.ThrowIfPropertyNotLinked(property);
                return RemoveValue(property.Key, property.Sections);
            }

            protected override void DisposeInternal(Boolean disposing)
            {
                base.DisposeInternal(disposing);
                LanguageChanged = null;
                ClearProperties();
            }
        }
    }
}