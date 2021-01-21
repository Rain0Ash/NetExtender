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
using NetExtender.Utils.Types;
using ReactiveUI.Fody.Helpers;

namespace NetExtender.Localizations
{
    public static partial class InternalLocalization
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        private class InternalLocalization2 : Config, ILocalization
        {
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

            [Reactive]
            public CultureComparer Comparer { get; init; }
            
            [Reactive]
            public Boolean ChangeUIThreadLanguage { get; set; } = true;

            [Reactive]
            public Boolean UseSystemCulture { get; set; } = true;

            private CultureInfo _culture;

            [Reactive]
            public CultureInfo Culture
            {
                get
                {
                    return _culture;
                }
                set
                {
                    if (value is null)
                    {
                        throw new ArgumentNullException(nameof(value));
                    }

                    if (Equals(_culture, value))
                    {
                        return;
                    }

                    if (!Supported.Contains(value))
                    {
                        throw new NotSupportedException();
                    }
                    
                    _culture = value;

                    if (ChangeUIThreadLanguage)
                    {
                        SetUILanguage();
                    }
                    
                    LanguageChanged?.Invoke(Culture);
                }
            }

            public CultureInfo Standart
            {
                get
                {
                    return UseSystemCulture && Supported.Contains(CultureUtils.System) ? CultureUtils.System : CultureUtils.English;
                }
            }

            private SortedSet<CultureInfo> Supported { get; }

            public IReadOnlySet<CultureInfo> Languages
            {
                get
                {
                    return Supported;
                }
            }

            private static (CultureComparer, SortedSet<CultureInfo>) Convert(ILocalizationBehaviour behaviour)
            {
                CultureComparer comparer = behaviour?.Comparer ?? new CultureComparer();
                SortedSet<CultureInfo> supported = new SortedSet<CultureInfo>(behaviour?.Supported?.Prepend(Default) ?? EnumerableUtils.GetEnumerableFrom(Default));

                return (comparer, supported);
            }

            public InternalLocalization2([NotNull] IConfigBehavior config, ILocalizationBehaviour behaviour = null)
                : base(config)
            {
                (Comparer, Supported) = Convert(behaviour);
            }

            public InternalLocalization2(ConfigType type, ConfigOptions options, ILocalizationBehaviour behaviour = null)
                : base(type, options)
            {
                (Comparer, Supported) = Convert(behaviour);
            }

            public InternalLocalization2(String path, ConfigType type, ConfigOptions options, ILocalizationBehaviour behaviour = null)
                : base(path, type, options)
            {
                (Comparer, Supported) = Convert(behaviour);
            }

            public Boolean AddSupportedCulture([NotNull] CultureInfo info)
            {
                if (info is null)
                {
                    throw new ArgumentNullException(nameof(info));
                }

                return Supported.Add(info);
            }

            public Boolean RemoveSupportedCulture([NotNull] CultureInfo info)
            {
                if (info is null)
                {
                    throw new ArgumentNullException(nameof(info));
                }

                if (info.LCID == CultureUtils.Default || info.LCID == CultureUtils.English.LCID)
                {
                    return false;
                }

                return Supported.Remove(info);
            }

            public Boolean SetUILanguage()
            {
                UInt16 lcid;
                try
                {
                    lcid = Culture.Equals(CultureInfo.InvariantCulture) ? CultureUtils.English.LCID16() : Culture.LCID16();
                }
                catch (Exception)
                {
                    lcid = CultureUtils.English.LCID16();
                }

                if (CultureUtils.SetUILanguage(lcid))
                {
                    return true;
                }

                CultureUtils.English.SetUILanguage();
                return false;
            }

            public Int32 GetLanguageOrderID()
            {
                return GetLanguageOrderID(Culture.LCID);
            }

            public Int32 GetLanguageOrderID(Int32 lcid)
            {
                return Comparer.GetLanguageOrderID(lcid);
            }

            protected override void DisposeInternal(Boolean disposing)
            {
                LanguageChanged = null;
                LanguageChanged = null;
            }
        }
    }
}