// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using NetExtender.Types.Culture;
using NetExtender.Types.Currency;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.Numerics;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Region
{
    public partial class CountryInfo : IEquatable<CountryInfo>
    {
        public static implicit operator UInt16(CountryInfo? info)
        {
            return info?.Code16 ?? 0;
        }

        public static implicit operator Int32(CountryInfo? info)
        {
            return info?.Code ?? 0;
        }

        public static implicit operator CountryIdentifier(CountryInfo? info)
        {
            return info?.Identifier ?? CountryIdentifier.Default;
        }

        public static implicit operator CountryInfo(CountryIdentifier identifier)
        {
            return Parse(identifier);
        }

        [return: NotNullIfNotNull("info")]
        public static implicit operator String?(CountryInfo? info)
        {
            return info?.OfficialName;
        }

        public static implicit operator CurrencyInfo?(CountryInfo? info)
        {
            return info?.Currency;
        }

        public static implicit operator CountryInfo?(CultureIdentifier identifier)
        {
            return identifier.ToCultureInfo();
        }

        public static implicit operator CountryInfo?(LocalizationIdentifier identifier)
        {
            return (CultureInfo) identifier;
        }

        public static implicit operator CountryInfo?(CultureInfo? info)
        {
            return info?.ToRegionInfo();
        }

        public static implicit operator CountryInfo?(RegionInfo? info)
        {
            return info is not null && EnumUtilities.TryParse(info.TwoLetterISORegionName, true, out CountryIdentifier identifier) ? identifier : null;
        }

        public static CountryInfo CurrentCountry
        {
            get
            {
                return Parse(CountryIdentifier.Default);
            }
        }

        public static CountryInfo[] Countries
        {
            get
            {
                return EnumUtilities.GetValuesWithoutDefault<CountryIdentifier>().Select(CountryInfoCache.Parse).ToArray();
            }
        }

        public String Name { get; }
        public String OfficialName { get; }
        public String NativeName { get; }
        public CountryIdentifier Identifier { get; }

        public Int32 Code
        {
            get
            {
                return Code16;
            }
        }

        public UInt16 Code16
        {
            get
            {
                return (UInt16) Identifier;
            }
        }

        public String TwoLetterISOCountryName
        {
            get
            {
                return EnumUtilities.GetName(Identifier);
            }
        }

        public RegionIdentifier Region
        {
            get
            {
                return Subregion.ToRegionIdentifier();
            }
        }

        public SubregionIdentifier Subregion { get; }

        public CountryDomain Domain { get; init; }
        public ImmutableHashSet<CountryInfo> Border { get; init; } = ImmutableHashSet<CountryInfo>.Empty;
        public CountryCurrency Currency { get; init; }
        public CountryCalling Calling { get; init; }

        protected CountryInfo(String name, String official, String native, CountryIdentifier identifier, SubregionIdentifier subregion)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            OfficialName = official ?? throw new ArgumentNullException(nameof(official));
            NativeName = native ?? throw new ArgumentNullException(nameof(native));
            Identifier = identifier;
            Subregion = subregion;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CountryInfo Parse(CountryIdentifier identifier)
        {
            return CountryInfoCache.Parse(identifier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CountryInfo Parse(UInt16 code)
        {
            return CountryInfoCache.Parse(code);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CountryInfo Parse(Int32 code)
        {
            return CountryInfoCache.Parse(code);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CountryInfo Parse(String name)
        {
            return CountryInfoCache.Parse(name);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(CountryIdentifier identifier, [MaybeNullWhen(false)] out CountryInfo result)
        {
            return CountryInfoCache.TryParse(identifier, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(UInt16 code, [MaybeNullWhen(false)] out CountryInfo result)
        {
            return CountryInfoCache.TryParse(code, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(Int32 code, [MaybeNullWhen(false)] out CountryInfo result)
        {
            return CountryInfoCache.TryParse(code, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(String name, [MaybeNullWhen(false)] out CountryInfo result)
        {
            return CountryInfoCache.TryParse(name, out result);
        }

        public override Int32 GetHashCode()
        {
            return Identifier.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return other is CountryInfo info && Equals(info);
        }

        public Boolean Equals(CountryInfo? other)
        {
            return other is not null && Identifier == other.Identifier;
        }

        public override String ToString()
        {
            return Name;
        }
    }

    public partial class CountryInfo
    {
        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        private static class CountryInfoCache
        {
            private static ImmutableDictionary<CountryIdentifier, CountryInfo> IdentifierCache { get; }
            private static ImmutableDictionary<Int32, CountryInfo> CodeCache { get; }
            private static ImmutableDictionary<String, CountryInfo> NameCache { get; }
            private static ImmutableDictionary<String, CountryInfo> OfficialNameCache { get; }
            private static ImmutableDictionary<String, CountryInfo> NativeNameCache { get; }
            private static ImmutableDictionary<String, CountryInfo> Iso2Cache { get; }

            static CountryInfoCache()
            {
                Action<CountryInfo, ImmutableHashSet<CountryInfo>>? setter = typeof(CountryInfo).GetProperty(nameof(Border))?.CreateSetExpression<CountryInfo, ImmutableHashSet<CountryInfo>>().Compile();

                if (setter is null)
                {
                    throw new InvalidOperationException($"Setter of {nameof(CountryInfo)}.{nameof(Border)} is null");
                }
                
                ImmutableDictionary<CountryIdentifier, CountryInfoData> cache = EnumUtilities.GetValuesWithoutDefault<CountryIdentifier>().ToImmutableDictionary(identifier => identifier, Create);
                IdentifierCache = cache.ToImmutableDictionary(pair => pair.Key, pair => pair.Value.ToInfo());
                
                static CountryInfo Hack(CountryInfo info, CountryInfoData data, Action<CountryInfo, ImmutableHashSet<CountryInfo>> setter)
                {
                    if (info is null)
                    {
                        throw new ArgumentNullException(nameof(info));
                    }

                    if (data is null)
                    {
                        throw new ArgumentNullException(nameof(data));
                    }

                    ImmutableHashSet<CountryInfo> border = data.Border.Select(Parse).ToImmutableHashSet();
                    setter.Invoke(info, border);
                    return info;
                }

                IdentifierCache = IdentifierCache.ToImmutableDictionary(pair => pair.Key, pair => Hack(pair.Value, cache[pair.Key], setter));
                CodeCache = IdentifierCache.Values.ToImmutableDictionary(country => country.Code, country => country);
                NameCache = IdentifierCache.Values.ToImmutableDictionary(country => country.Name, country => country, StringComparer.OrdinalIgnoreCase);
                OfficialNameCache = IdentifierCache.Values.ToImmutableDictionary(country => country.OfficialName, country => country, StringComparer.OrdinalIgnoreCase);
                NativeNameCache = IdentifierCache.Values.ToImmutableDictionary(country => country.NativeName, country => country, StringComparer.OrdinalIgnoreCase);
                Iso2Cache = IdentifierCache.Values.ToImmutableDictionary(country => country.TwoLetterISOCountryName, country => country, StringComparer.OrdinalIgnoreCase);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static CountryInfo Parse(CountryIdentifier identifier)
            {
                return TryParse(identifier, out CountryInfo? result) ? result : throw new EnumUndefinedOrNotSupportedException<CountryIdentifier>(identifier);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static CountryInfo Parse(UInt16 code)
            {
                return TryParse(code, out CountryInfo? result) ? result : throw new InvalidOperationException();
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static CountryInfo Parse(Int32 code)
            {
                return TryParse(code, out CountryInfo? result) ? result : throw new InvalidOperationException();
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static CountryInfo Parse(String name)
            {
                return TryParse(name, out CountryInfo? result) ? result : throw new InvalidOperationException();
            }

            public static Boolean TryParse(CountryIdentifier identifier, [MaybeNullWhen(false)] out CountryInfo result)
            {
                if (identifier != CountryIdentifier.Default)
                {
                    return IdentifierCache.TryGetValue(identifier, out result);
                }

                CountryInfo? country = RegionInfo.CurrentRegion;
                country ??= CultureInfo.CurrentCulture;

                if (country is null)
                {
                    result = default;
                    return false;
                }

                result = country;
                return true;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean TryParse(UInt16 code, [MaybeNullWhen(false)] out CountryInfo result)
            {
                return CodeCache.TryGetValue(code, out result);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean TryParse(Int32 code, [MaybeNullWhen(false)] out CountryInfo result)
            {
                return CodeCache.TryGetValue(code, out result);
            }

            public static Boolean TryParse(String name, [MaybeNullWhen(false)] out CountryInfo result)
            {
                if (name is null)
                {
                    throw new ArgumentNullException(nameof(name));
                }

                return Iso2Cache.TryGetValue(name, out result) || NameCache.TryGetValue(name, out result) || OfficialNameCache.TryGetValue(name, out result) || NativeNameCache.TryGetValue(name, out result);
            }

            private record CountryInfoData
            {
                public String Name { get; }
                public String OfficialName { get; }
                public String NativeName { get; }
                public CountryIdentifier Identifier { get; }
                public SubregionIdentifier Subregion { get; }
                public CountryDomain Domain { get; init; }
                public ImmutableHashSet<CountryIdentifier> Border { get; init; } = ImmutableHashSet<CountryIdentifier>.Empty;
                public CountryCurrency Currency { get; init; }
                public CountryCalling Calling { get; init; }

                public CountryInfoData(String name, String official, String native, CountryIdentifier identifier, SubregionIdentifier subregion)
                {
                    Name = name ?? throw new ArgumentNullException(nameof(name));
                    OfficialName = official ?? throw new ArgumentNullException(nameof(official));
                    NativeName = native ?? throw new ArgumentNullException(nameof(native));
                    Identifier = identifier;
                    Subregion = subregion;
                }

                public CountryInfo ToInfo()
                {
                    return new CountryInfo(Name, OfficialName, NativeName, Identifier, Subregion)
                    {
                        Domain = Domain,
                        Currency = Currency,
                        Calling = Calling
                    };
                }
            }

            private static CountryInfoData Create(CountryIdentifier identifier)
            {
                return identifier switch
                {
                    CountryIdentifier.Default => throw new ArgumentException(),
                    CountryIdentifier.Af => new CountryInfoData("Afghanistan", "Islamic Republic of Afghanistan", "افغانستان", CountryIdentifier.Af, SubregionIdentifier.SouthernAsia)
                    {
                        Domain = new CountryDomain(".af"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Ir, CountryIdentifier.Pk, CountryIdentifier.Tm, CountryIdentifier.Uz, CountryIdentifier.Tj, CountryIdentifier.Cn),
                        Currency = new CountryCurrency(CurrencyIdentifier.Afn),
                        Calling = new CountryCalling(93)
                    },
                    CountryIdentifier.Al => new CountryInfoData("Albania", "Republic of Albania", "Shqipëria", CountryIdentifier.Al, SubregionIdentifier.SouthernEurope)
                    {
                        Domain = new CountryDomain(".al"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Me, CountryIdentifier.Gr, CountryIdentifier.Mk),
                        Currency = new CountryCurrency(CurrencyIdentifier.All),
                        Calling = new CountryCalling(355)
                    },
                    CountryIdentifier.Aq => new CountryInfoData("Antarctica", "Antarctica", "Antarctica", CountryIdentifier.Aq, SubregionIdentifier.Antarctic)
                    {
                        Domain = new CountryDomain(".aq"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(),
                        Calling = new CountryCalling()
                    },
                    CountryIdentifier.Dz => new CountryInfoData("Algeria", "People's Democratic Republic of Algeria", "الجزائر", CountryIdentifier.Dz, SubregionIdentifier.NorthernAfrica)
                    {
                        Domain = new CountryDomain(".dz", "الجزائر."),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Tn, CountryIdentifier.Ly, CountryIdentifier.Ne, CountryIdentifier.Eh, CountryIdentifier.Mr, CountryIdentifier.Ml, CountryIdentifier.Ma),
                        Currency = new CountryCurrency(CurrencyIdentifier.Dzd),
                        Calling = new CountryCalling(213)
                    },
                    CountryIdentifier.As => new CountryInfoData("American Samoa", "American Samoa", "American Samoa", CountryIdentifier.As, SubregionIdentifier.Polynesia)
                    {
                        Domain = new CountryDomain(".as"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Usd),
                        Calling = new CountryCalling(1684)
                    },
                    CountryIdentifier.Ad => new CountryInfoData("Andorra", "Principality of Andorra", "Andorra", CountryIdentifier.Ad, SubregionIdentifier.SouthernEurope)
                    {
                        Domain = new CountryDomain(".ad"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Fr, CountryIdentifier.Es),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(376)
                    },
                    CountryIdentifier.Ao => new CountryInfoData("Angola", "Republic of Angola", "Angola", CountryIdentifier.Ao, SubregionIdentifier.MiddleAfrica)
                    {
                        Domain = new CountryDomain(".ao"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Cg, CountryIdentifier.Cd, CountryIdentifier.Zm, CountryIdentifier.Na),
                        Currency = new CountryCurrency(CurrencyIdentifier.Aoa),
                        Calling = new CountryCalling(244)
                    },
                    CountryIdentifier.Ag => new CountryInfoData("Antigua and Barbuda", "Antigua and Barbuda", "Antigua and Barbuda", CountryIdentifier.Ag, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".ag"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Xcd),
                        Calling = new CountryCalling(1268)
                    },
                    CountryIdentifier.Az => new CountryInfoData("Azerbaijan", "Republic of Azerbaijan", "Azərbaycan", CountryIdentifier.Az, SubregionIdentifier.WesternAsia)
                    {
                        Domain = new CountryDomain(".az"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Am, CountryIdentifier.Ge, CountryIdentifier.Ir, CountryIdentifier.Ru, CountryIdentifier.Tr),
                        Currency = new CountryCurrency(CurrencyIdentifier.Azn),
                        Calling = new CountryCalling(994)
                    },
                    CountryIdentifier.Ar => new CountryInfoData("Argentina", "Argentine Republic", "Argentina", CountryIdentifier.Ar, SubregionIdentifier.SouthAmerica)
                    {
                        Domain = new CountryDomain(".ar"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Bo, CountryIdentifier.Br, CountryIdentifier.Cl, CountryIdentifier.Py, CountryIdentifier.Uy),
                        Currency = new CountryCurrency(CurrencyIdentifier.Ars),
                        Calling = new CountryCalling(54)
                    },
                    CountryIdentifier.Au => new CountryInfoData("Australia", "Commonwealth of Australia", "Australia", CountryIdentifier.Au, SubregionIdentifier.AustraliaAndNewZealand)
                    {
                        Domain = new CountryDomain(".au"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Aud),
                        Calling = new CountryCalling(61)
                    },
                    CountryIdentifier.At => new CountryInfoData("Austria", "Republic of Austria", "Österreich", CountryIdentifier.At, SubregionIdentifier.WesternEurope)
                    {
                        Domain = new CountryDomain(".at"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Cz, CountryIdentifier.De, CountryIdentifier.Hu, CountryIdentifier.It, CountryIdentifier.Li, CountryIdentifier.Sk, CountryIdentifier.Si, CountryIdentifier.Ch),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(43)
                    },
                    CountryIdentifier.Bs => new CountryInfoData("Bahamas", "Commonwealth of the Bahamas", "Bahamas", CountryIdentifier.Bs, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".bs"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Bsd),
                        Calling = new CountryCalling(1242)
                    },
                    CountryIdentifier.Bh => new CountryInfoData("Bahrain", "Kingdom of Bahrain", "البحرين", CountryIdentifier.Bh, SubregionIdentifier.WesternAsia)
                    {
                        Domain = new CountryDomain(".bh"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Bhd),
                        Calling = new CountryCalling(973)
                    },
                    CountryIdentifier.Bd => new CountryInfoData("Bangladesh", "People's Republic of Bangladesh", "Bangladesh", CountryIdentifier.Bd, SubregionIdentifier.SouthernAsia)
                    {
                        Domain = new CountryDomain(".bd"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Mm, CountryIdentifier.In),
                        Currency = new CountryCurrency(CurrencyIdentifier.Bdt),
                        Calling = new CountryCalling(880)
                    },
                    CountryIdentifier.Am => new CountryInfoData("Armenia", "Republic of Armenia", "Հայաստան", CountryIdentifier.Am, SubregionIdentifier.WesternAsia)
                    {
                        Domain = new CountryDomain(".am"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Az, CountryIdentifier.Ge, CountryIdentifier.Ir, CountryIdentifier.Tr),
                        Currency = new CountryCurrency(CurrencyIdentifier.Amd),
                        Calling = new CountryCalling(374)
                    },
                    CountryIdentifier.Bb => new CountryInfoData("Barbados", "Barbados", "Barbados", CountryIdentifier.Bb, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".bb"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Bbd),
                        Calling = new CountryCalling(1246)
                    },
                    CountryIdentifier.Be => new CountryInfoData("Belgium", "Kingdom of Belgium", "België", CountryIdentifier.Be, SubregionIdentifier.WesternEurope)
                    {
                        Domain = new CountryDomain(".be"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Fr, CountryIdentifier.De, CountryIdentifier.Lu, CountryIdentifier.Nl),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(32)
                    },
                    CountryIdentifier.Bm => new CountryInfoData("Bermuda", "Bermuda", "Bermuda", CountryIdentifier.Bm, SubregionIdentifier.NorthAmerica)
                    {
                        Domain = new CountryDomain(".bm"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Bmd),
                        Calling = new CountryCalling(1441)
                    },
                    CountryIdentifier.Bt => new CountryInfoData("Bhutan", "Kingdom of Bhutan", "ʼbrug-yul", CountryIdentifier.Bt, SubregionIdentifier.SouthernAsia)
                    {
                        Domain = new CountryDomain(".bt"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Cn, CountryIdentifier.In),
                        Currency = new CountryCurrency(CurrencyIdentifier.Btn, CurrencyIdentifier.Inr),
                        Calling = new CountryCalling(975)
                    },
                    CountryIdentifier.Bo => new CountryInfoData("Bolivia", "Plurinational State of Bolivia", "Bolivia", CountryIdentifier.Bo, SubregionIdentifier.SouthAmerica)
                    {
                        Domain = new CountryDomain(".bo"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Ar, CountryIdentifier.Br, CountryIdentifier.Cl, CountryIdentifier.Py, CountryIdentifier.Pe),
                        Currency = new CountryCurrency(CurrencyIdentifier.Bob),
                        Calling = new CountryCalling(591)
                    },
                    CountryIdentifier.Ba => new CountryInfoData("Bosnia and Herzegovina", "Bosnia and Herzegovina", "Bosna i Hercegovina", CountryIdentifier.Ba, SubregionIdentifier.SouthernEurope)
                    {
                        Domain = new CountryDomain(".ba"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Hr, CountryIdentifier.Me, CountryIdentifier.Rs),
                        Currency = new CountryCurrency(CurrencyIdentifier.Bam),
                        Calling = new CountryCalling(387)
                    },
                    CountryIdentifier.Bw => new CountryInfoData("Botswana", "Republic of Botswana", "Botswana", CountryIdentifier.Bw, SubregionIdentifier.SouthernAfrica)
                    {
                        Domain = new CountryDomain(".bw"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Na, CountryIdentifier.Za, CountryIdentifier.Zm, CountryIdentifier.Zw),
                        Currency = new CountryCurrency(CurrencyIdentifier.Bwp),
                        Calling = new CountryCalling(267)
                    },
                    CountryIdentifier.Bv => new CountryInfoData("Bouvet Island", "Bouvet Island", "Bouvetøya", CountryIdentifier.Bv, SubregionIdentifier.Antarctic)
                    {
                        Domain = new CountryDomain(".bv"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Nok),
                        Calling = new CountryCalling()
                    },
                    CountryIdentifier.Br => new CountryInfoData("Brazil", "Federative Republic of Brazil", "Brasil", CountryIdentifier.Br, SubregionIdentifier.SouthAmerica)
                    {
                        Domain = new CountryDomain(".br"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Ar, CountryIdentifier.Bo, CountryIdentifier.Co, CountryIdentifier.Gf, CountryIdentifier.Gy, CountryIdentifier.Py, CountryIdentifier.Pe, CountryIdentifier.Sr, CountryIdentifier.Uy, CountryIdentifier.Ve),
                        Currency = new CountryCurrency(CurrencyIdentifier.Brl),
                        Calling = new CountryCalling(55)
                    },
                    CountryIdentifier.Bz => new CountryInfoData("Belize", "Belize", "Belize", CountryIdentifier.Bz, SubregionIdentifier.CentralAmerica)
                    {
                        Domain = new CountryDomain(".bz"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Gt, CountryIdentifier.Mx),
                        Currency = new CountryCurrency(CurrencyIdentifier.Bzd),
                        Calling = new CountryCalling(501)
                    },
                    CountryIdentifier.Io => new CountryInfoData("British Indian Ocean Territory", "British Indian Ocean Territory", "British Indian Ocean Territory", CountryIdentifier.Io, SubregionIdentifier.EasternAfrica)
                    {
                        Domain = new CountryDomain(".io"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Usd),
                        Calling = new CountryCalling(246)
                    },
                    CountryIdentifier.Sb => new CountryInfoData("Solomon Islands", "Solomon Islands", "Solomon Islands", CountryIdentifier.Sb, SubregionIdentifier.Melanesia)
                    {
                        Domain = new CountryDomain(".sb"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Sbd),
                        Calling = new CountryCalling(677)
                    },
                    CountryIdentifier.Vg => new CountryInfoData("British Virgin Islands", "Virgin Islands", "British Virgin Islands", CountryIdentifier.Vg, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".vg"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Usd),
                        Calling = new CountryCalling(1284)
                    },
                    CountryIdentifier.Bn => new CountryInfoData("Brunei", "Nation of Brunei, Abode of Peace", "Negara Brunei Darussalam", CountryIdentifier.Bn, SubregionIdentifier.SouthEasternAsia)
                    {
                        Domain = new CountryDomain(".bn"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.My),
                        Currency = new CountryCurrency(CurrencyIdentifier.Bnd),
                        Calling = new CountryCalling(673)
                    },
                    CountryIdentifier.Bg => new CountryInfoData("Bulgaria", "Republic of Bulgaria", "България", CountryIdentifier.Bg, SubregionIdentifier.EasternEurope)
                    {
                        Domain = new CountryDomain(".bg"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Gr, CountryIdentifier.Mk, CountryIdentifier.Ro, CountryIdentifier.Rs, CountryIdentifier.Tr),
                        Currency = new CountryCurrency(CurrencyIdentifier.Bgn),
                        Calling = new CountryCalling(359)
                    },
                    CountryIdentifier.Mm => new CountryInfoData("Myanmar", "Republic of the Union of Myanmar", "Myanma", CountryIdentifier.Mm, SubregionIdentifier.SouthEasternAsia)
                    {
                        Domain = new CountryDomain(".mm"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Bd, CountryIdentifier.Cn, CountryIdentifier.In, CountryIdentifier.La, CountryIdentifier.Th),
                        Currency = new CountryCurrency(CurrencyIdentifier.Mmk),
                        Calling = new CountryCalling(95)
                    },
                    CountryIdentifier.Bi => new CountryInfoData("Burundi", "Republic of Burundi", "Burundi", CountryIdentifier.Bi, SubregionIdentifier.EasternAfrica)
                    {
                        Domain = new CountryDomain(".bi"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Cd, CountryIdentifier.Rw, CountryIdentifier.Tz),
                        Currency = new CountryCurrency(CurrencyIdentifier.Bif),
                        Calling = new CountryCalling(257)
                    },
                    CountryIdentifier.By => new CountryInfoData("Belarus", "Republic of Belarus", "Белару́сь", CountryIdentifier.By, SubregionIdentifier.EasternEurope)
                    {
                        Domain = new CountryDomain(".by"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Lv, CountryIdentifier.Lt, CountryIdentifier.Pl, CountryIdentifier.Ru, CountryIdentifier.Ua),
                        Currency = new CountryCurrency(CurrencyIdentifier.Byn),
                        Calling = new CountryCalling(375)
                    },
                    CountryIdentifier.Kh => new CountryInfoData("Cambodia", "Kingdom of Cambodia", "Kâmpŭchéa", CountryIdentifier.Kh, SubregionIdentifier.SouthEasternAsia)
                    {
                        Domain = new CountryDomain(".kh"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.La, CountryIdentifier.Th, CountryIdentifier.Vn),
                        Currency = new CountryCurrency(CurrencyIdentifier.Khr),
                        Calling = new CountryCalling(855)
                    },
                    CountryIdentifier.Cm => new CountryInfoData("Cameroon", "Republic of Cameroon", "Cameroon", CountryIdentifier.Cm, SubregionIdentifier.MiddleAfrica)
                    {
                        Domain = new CountryDomain(".cm"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Cf, CountryIdentifier.Td, CountryIdentifier.Cg, CountryIdentifier.Gq, CountryIdentifier.Ga, CountryIdentifier.Ng),
                        Currency = new CountryCurrency(CurrencyIdentifier.Xaf),
                        Calling = new CountryCalling(237)
                    },
                    CountryIdentifier.Ca => new CountryInfoData("Canada", "Canada", "Canada", CountryIdentifier.Ca, SubregionIdentifier.NorthAmerica)
                    {
                        Domain = new CountryDomain(".ca"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Us),
                        Currency = new CountryCurrency(CurrencyIdentifier.Cad),
                        Calling = new CountryCalling(1)
                    },
                    CountryIdentifier.Cv => new CountryInfoData("Cape Verde", "Republic of Cabo Verde", "Cabo Verde", CountryIdentifier.Cv, SubregionIdentifier.WesternAfrica)
                    {
                        Domain = new CountryDomain(".cv"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Cve),
                        Calling = new CountryCalling(238)
                    },
                    CountryIdentifier.Ky => new CountryInfoData("Cayman Islands", "Cayman Islands", "Cayman Islands", CountryIdentifier.Ky, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".ky"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Kyd),
                        Calling = new CountryCalling(1345)
                    },
                    CountryIdentifier.Cf => new CountryInfoData("Central African Republic", "Central African Republic", "Ködörösêse tî Bêafrîka", CountryIdentifier.Cf, SubregionIdentifier.MiddleAfrica)
                    {
                        Domain = new CountryDomain(".cf"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Cm, CountryIdentifier.Td, CountryIdentifier.Cd, CountryIdentifier.Cg, CountryIdentifier.Ss, CountryIdentifier.Sd),
                        Currency = new CountryCurrency(CurrencyIdentifier.Xaf),
                        Calling = new CountryCalling(236)
                    },
                    CountryIdentifier.Lk => new CountryInfoData("Sri Lanka", "Democratic Socialist Republic of Sri Lanka", "śrī laṃkāva", CountryIdentifier.Lk, SubregionIdentifier.SouthernAsia)
                    {
                        Domain = new CountryDomain(".lk", null, ".இலங்கை", ".ලංකා"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.In),
                        Currency = new CountryCurrency(CurrencyIdentifier.Lkr),
                        Calling = new CountryCalling(94)
                    },
                    CountryIdentifier.Td => new CountryInfoData("Chad", "Republic of Chad", "Tchad", CountryIdentifier.Td, SubregionIdentifier.MiddleAfrica)
                    {
                        Domain = new CountryDomain(".td"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Cm, CountryIdentifier.Cf, CountryIdentifier.Ly, CountryIdentifier.Ne, CountryIdentifier.Ng, CountryIdentifier.Ss),
                        Currency = new CountryCurrency(CurrencyIdentifier.Xaf),
                        Calling = new CountryCalling(235)
                    },
                    CountryIdentifier.Cl => new CountryInfoData("Chile", "Republic of Chile", "Chile", CountryIdentifier.Cl, SubregionIdentifier.SouthAmerica)
                    {
                        Domain = new CountryDomain(".cl"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Ar, CountryIdentifier.Bo, CountryIdentifier.Pe),
                        Currency = new CountryCurrency(CurrencyIdentifier.Clp),
                        Calling = new CountryCalling(56)
                    },
                    CountryIdentifier.Cn => new CountryInfoData("China", "People's Republic of China", "中国", CountryIdentifier.Cn, SubregionIdentifier.EasternAsia)
                    {
                        Domain = new CountryDomain(".cn", null, ".中国", ".中國", ".公司", ".网络"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Af, CountryIdentifier.Bt, CountryIdentifier.Mm, CountryIdentifier.Hk, CountryIdentifier.In, CountryIdentifier.Kz, CountryIdentifier.Kp, CountryIdentifier.Kg, CountryIdentifier.La, CountryIdentifier.Mo, CountryIdentifier.Mn, CountryIdentifier.Pk, CountryIdentifier.Ru, CountryIdentifier.Tj, CountryIdentifier.Vn),
                        Currency = new CountryCurrency(CurrencyIdentifier.Cny),
                        Calling = new CountryCalling(86)
                    },
                    CountryIdentifier.Tw => new CountryInfoData("Taiwan", "Republic of China", "臺灣", CountryIdentifier.Tw, SubregionIdentifier.EasternAsia)
                    {
                        Domain = new CountryDomain(".tw", null, ".台湾", ".台灣"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Twd),
                        Calling = new CountryCalling(886)
                    },
                    CountryIdentifier.Cx => new CountryInfoData("Christmas Island", "Territory of Christmas Island", "Christmas Island", CountryIdentifier.Cx, SubregionIdentifier.AustraliaAndNewZealand)
                    {
                        Domain = new CountryDomain(".cx"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Aud),
                        Calling = new CountryCalling(61)
                    },
                    CountryIdentifier.Cc => new CountryInfoData("Cocos (Keeling) Islands", "Territory of the Cocos (Keeling) Islands", "Cocos (Keeling) Islands", CountryIdentifier.Cc, SubregionIdentifier.AustraliaAndNewZealand)
                    {
                        Domain = new CountryDomain(".cc"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Aud),
                        Calling = new CountryCalling(61)
                    },
                    CountryIdentifier.Co => new CountryInfoData("Colombia", "Republic of Colombia", "Colombia", CountryIdentifier.Co, SubregionIdentifier.SouthAmerica)
                    {
                        Domain = new CountryDomain(".co"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Br, CountryIdentifier.Ec, CountryIdentifier.Pa, CountryIdentifier.Pe, CountryIdentifier.Ve),
                        Currency = new CountryCurrency(CurrencyIdentifier.Cop),
                        Calling = new CountryCalling(57)
                    },
                    CountryIdentifier.Km => new CountryInfoData("Comoros", "Union of the Comoros", "Komori", CountryIdentifier.Km, SubregionIdentifier.EasternAfrica)
                    {
                        Domain = new CountryDomain(".km"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Kmf),
                        Calling = new CountryCalling(269)
                    },
                    CountryIdentifier.Yt => new CountryInfoData("Mayotte", "Department of Mayotte", "Mayotte", CountryIdentifier.Yt, SubregionIdentifier.EasternAfrica)
                    {
                        Domain = new CountryDomain(".yt"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(262)
                    },
                    CountryIdentifier.Cg => new CountryInfoData("Republic of the Congo", "Republic of the Congo", "République démocratique du Congo", CountryIdentifier.Cg, SubregionIdentifier.MiddleAfrica)
                    {
                        Domain = new CountryDomain(".cg"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Ao, CountryIdentifier.Cm, CountryIdentifier.Cf, CountryIdentifier.Cd, CountryIdentifier.Ga),
                        Currency = new CountryCurrency(CurrencyIdentifier.Xaf),
                        Calling = new CountryCalling(242)
                    },
                    CountryIdentifier.Cd => new CountryInfoData("DR Congo", "Democratic Republic of the Congo", "République du Congo", CountryIdentifier.Cd, SubregionIdentifier.MiddleAfrica)
                    {
                        Domain = new CountryDomain(".cd"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Ao, CountryIdentifier.Bi, CountryIdentifier.Cf, CountryIdentifier.Cg, CountryIdentifier.Rw, CountryIdentifier.Ss, CountryIdentifier.Tz, CountryIdentifier.Ug, CountryIdentifier.Zm),
                        Currency = new CountryCurrency(CurrencyIdentifier.Cdf),
                        Calling = new CountryCalling(243)
                    },
                    CountryIdentifier.Ck => new CountryInfoData("Cook Islands", "Cook Islands", "Cook Islands", CountryIdentifier.Ck, SubregionIdentifier.Polynesia)
                    {
                        Domain = new CountryDomain(".ck"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Nzd),
                        Calling = new CountryCalling(682)
                    },
                    CountryIdentifier.Cr => new CountryInfoData("Costa Rica", "Republic of Costa Rica", "Costa Rica", CountryIdentifier.Cr, SubregionIdentifier.CentralAmerica)
                    {
                        Domain = new CountryDomain(".cr"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Ni, CountryIdentifier.Pa),
                        Currency = new CountryCurrency(CurrencyIdentifier.Crc),
                        Calling = new CountryCalling(506)
                    },
                    CountryIdentifier.Hr => new CountryInfoData("Croatia", "Republic of Croatia", "Hrvatska", CountryIdentifier.Hr, SubregionIdentifier.SouthernEurope)
                    {
                        Domain = new CountryDomain(".hr"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Ba, CountryIdentifier.Hu, CountryIdentifier.Me, CountryIdentifier.Rs, CountryIdentifier.Si),
                        Currency = new CountryCurrency(CurrencyIdentifier.Hrk),
                        Calling = new CountryCalling(385)
                    },
                    CountryIdentifier.Cu => new CountryInfoData("Cuba", "Republic of Cuba", "Cuba", CountryIdentifier.Cu, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".cu"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Cuc, CurrencyIdentifier.Cup),
                        Calling = new CountryCalling(53)
                    },
                    CountryIdentifier.Cy => new CountryInfoData("Cyprus", "Republic of Cyprus", "Кэрспт", CountryIdentifier.Cy, SubregionIdentifier.EasternEurope)
                    {
                        Domain = new CountryDomain(".cy"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Gb),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(357)
                    },
                    CountryIdentifier.Cz => new CountryInfoData("Czechia", "Czech Republic", "Česká republika", CountryIdentifier.Cz, SubregionIdentifier.EasternEurope)
                    {
                        Domain = new CountryDomain(".cz"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.At, CountryIdentifier.De, CountryIdentifier.Pl, CountryIdentifier.Sk),
                        Currency = new CountryCurrency(CurrencyIdentifier.Czk),
                        Calling = new CountryCalling(420)
                    },
                    CountryIdentifier.Bj => new CountryInfoData("Benin", "Republic of Benin", "Bénin", CountryIdentifier.Bj, SubregionIdentifier.WesternAfrica)
                    {
                        Domain = new CountryDomain(".bj"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Bf, CountryIdentifier.Ne, CountryIdentifier.Ng, CountryIdentifier.Tg),
                        Currency = new CountryCurrency(CurrencyIdentifier.Xof),
                        Calling = new CountryCalling(229)
                    },
                    CountryIdentifier.Dk => new CountryInfoData("Denmark", "Kingdom of Denmark", "Danmark", CountryIdentifier.Dk, SubregionIdentifier.NorthernEurope)
                    {
                        Domain = new CountryDomain(".dk"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.De),
                        Currency = new CountryCurrency(CurrencyIdentifier.Dkk),
                        Calling = new CountryCalling(45)
                    },
                    CountryIdentifier.Dm => new CountryInfoData("Dominica", "Commonwealth of Dominica", "Dominica", CountryIdentifier.Dm, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".dm"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Xcd),
                        Calling = new CountryCalling(1767)
                    },
                    CountryIdentifier.Do => new CountryInfoData("Dominican Republic", "Dominican Republic", "República Dominicana", CountryIdentifier.Do, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".do"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Ht),
                        Currency = new CountryCurrency(CurrencyIdentifier.Dop),
                        Calling = new CountryCalling(1809, 1829, 1849)
                    },
                    CountryIdentifier.Ec => new CountryInfoData("Ecuador", "Republic of Ecuador", "Ecuador", CountryIdentifier.Ec, SubregionIdentifier.SouthAmerica)
                    {
                        Domain = new CountryDomain(".ec"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Co, CountryIdentifier.Pe),
                        Currency = new CountryCurrency(CurrencyIdentifier.Usd),
                        Calling = new CountryCalling(593)
                    },
                    CountryIdentifier.Sv => new CountryInfoData("El Salvador", "Republic of El Salvador", "El Salvador", CountryIdentifier.Sv, SubregionIdentifier.CentralAmerica)
                    {
                        Domain = new CountryDomain(".sv"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Gt, CountryIdentifier.Hn),
                        Currency = new CountryCurrency(CurrencyIdentifier.Svc, CurrencyIdentifier.Usd),
                        Calling = new CountryCalling(503)
                    },
                    CountryIdentifier.Gq => new CountryInfoData("Equatorial Guinea", "Republic of Equatorial Guinea", "Equatorial Guinea", CountryIdentifier.Gq, SubregionIdentifier.MiddleAfrica)
                    {
                        Domain = new CountryDomain(".gq"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Cm, CountryIdentifier.Ga),
                        Currency = new CountryCurrency(CurrencyIdentifier.Xaf),
                        Calling = new CountryCalling(240)
                    },
                    CountryIdentifier.Et => new CountryInfoData("Ethiopia", "Federal Democratic Republic of Ethiopia", "ኢትዮጵያ", CountryIdentifier.Et, SubregionIdentifier.EasternAfrica)
                    {
                        Domain = new CountryDomain(".et"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Dj, CountryIdentifier.Er, CountryIdentifier.Ke, CountryIdentifier.So, CountryIdentifier.Ss, CountryIdentifier.Sd),
                        Currency = new CountryCurrency(CurrencyIdentifier.Etb),
                        Calling = new CountryCalling(251)
                    },
                    CountryIdentifier.Er => new CountryInfoData("Eritrea", "State of Eritrea", "ኤርትራ", CountryIdentifier.Er, SubregionIdentifier.EasternAfrica)
                    {
                        Domain = new CountryDomain(".er"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Dj, CountryIdentifier.Et, CountryIdentifier.Sd),
                        Currency = new CountryCurrency(CurrencyIdentifier.Ern),
                        Calling = new CountryCalling(291)
                    },
                    CountryIdentifier.Ee => new CountryInfoData("Estonia", "Republic of Estonia", "Eesti", CountryIdentifier.Ee, SubregionIdentifier.NorthernEurope)
                    {
                        Domain = new CountryDomain(".ee"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Lv, CountryIdentifier.Ru),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(372)
                    },
                    CountryIdentifier.Fo => new CountryInfoData("Faroe Islands", "Faroe Islands", "Føroyar", CountryIdentifier.Fo, SubregionIdentifier.NorthernEurope)
                    {
                        Domain = new CountryDomain(".fo"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Dkk),
                        Calling = new CountryCalling(298)
                    },
                    CountryIdentifier.Fk => new CountryInfoData("Falkland Islands", "Falkland Islands", "Falkland Islands",
                        CountryIdentifier.Fk, SubregionIdentifier.SouthAmerica)
                    {
                        Domain = new CountryDomain(".fk"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Fkp),
                        Calling = new CountryCalling(500)
                    },
                    CountryIdentifier.Gs => new CountryInfoData("South Georgia", "South Georgia and the South Sandwich Islands", "South Georgia",
                        CountryIdentifier.Gs, SubregionIdentifier.Antarctic)
                    {
                        Domain = new CountryDomain(".gs"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Gbp),
                        Calling = new CountryCalling(500)
                    },
                    CountryIdentifier.Fj => new CountryInfoData("Fiji", "Republic of Fiji", "Fiji", CountryIdentifier.Fj, SubregionIdentifier.Melanesia)
                    {
                        Domain = new CountryDomain(".fj"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Fjd),
                        Calling = new CountryCalling(679)
                    },
                    CountryIdentifier.Fi => new CountryInfoData("Finland", "Republic of Finland", "Suomi", CountryIdentifier.Fi, SubregionIdentifier.NorthernEurope)
                    {
                        Domain = new CountryDomain(".fi"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.No, CountryIdentifier.Se, CountryIdentifier.Ru),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(358)
                    },
                    CountryIdentifier.Ax => new CountryInfoData("Åland Islands", "Åland Islands", "Åland", CountryIdentifier.Ax, SubregionIdentifier.NorthernEurope)
                    {
                        Domain = new CountryDomain(".ax"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(358)
                    },
                    CountryIdentifier.Fr => new CountryInfoData("France", "French Republic", "France", CountryIdentifier.Fr, SubregionIdentifier.WesternEurope)
                    {
                        Domain = new CountryDomain(".fr"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Ad, CountryIdentifier.Be, CountryIdentifier.De, CountryIdentifier.It, CountryIdentifier.Lu, CountryIdentifier.Mc, CountryIdentifier.Es, CountryIdentifier.Ch),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(33)
                    },
                    CountryIdentifier.Gf => new CountryInfoData("French Guiana", "Guiana", "Guyane française", CountryIdentifier.Gf, SubregionIdentifier.SouthAmerica)
                    {
                        Domain = new CountryDomain(".gf"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Br, CountryIdentifier.Sr),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(594)
                    },
                    CountryIdentifier.Pf => new CountryInfoData("French Polynesia", "French Polynesia", "Polynésie française", CountryIdentifier.Pf, SubregionIdentifier.Polynesia)
                    {
                        Domain = new CountryDomain(".pf"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Zar),
                        Calling = new CountryCalling(689)
                    },
                    CountryIdentifier.Tf => new CountryInfoData("French Southern and Antarctic Lands", "Territory of the French Southern and Antarctic Lands",
                        "Territoire des Terres australes et antarctiques françaises", CountryIdentifier.Tf, SubregionIdentifier.Antarctic)
                    {
                        Domain = new CountryDomain(".tf"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling()
                    },
                    CountryIdentifier.Dj => new CountryInfoData("Djibouti", "Republic of Djibouti", "Djibouti", CountryIdentifier.Dj, SubregionIdentifier.EasternAfrica)
                    {
                        Domain = new CountryDomain(".dj"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Er, CountryIdentifier.Et, CountryIdentifier.So),
                        Currency = new CountryCurrency(CurrencyIdentifier.Djf),
                        Calling = new CountryCalling(253)
                    },
                    CountryIdentifier.Ga => new CountryInfoData("Gabon", "Gabonese Republic", "Gabon", CountryIdentifier.Ga, SubregionIdentifier.MiddleAfrica)
                    {
                        Domain = new CountryDomain(".ga"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Cm, CountryIdentifier.Cg, CountryIdentifier.Gq),
                        Currency = new CountryCurrency(CurrencyIdentifier.Xaf),
                        Calling = new CountryCalling(241)
                    },
                    CountryIdentifier.Ge => new CountryInfoData("Georgia", "Georgia", "საქართველო", CountryIdentifier.Ge, SubregionIdentifier.WesternAsia)
                    {
                        Domain = new CountryDomain(".ge"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Am, CountryIdentifier.Az, CountryIdentifier.Ru, CountryIdentifier.Tr),
                        Currency = new CountryCurrency(CurrencyIdentifier.Gel),
                        Calling = new CountryCalling(995)
                    },
                    CountryIdentifier.Gm => new CountryInfoData("Gambia", "Republic of the Gambia", "Gambia", CountryIdentifier.Gm, SubregionIdentifier.WesternAfrica)
                    {
                        Domain = new CountryDomain(".gm"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Sn),
                        Currency = new CountryCurrency(CurrencyIdentifier.Gmd),
                        Calling = new CountryCalling(220)
                    },
                    CountryIdentifier.Ps => new CountryInfoData("Palestine", "State of Palestine", "فلسطين", CountryIdentifier.Ps, SubregionIdentifier.WesternAsia)
                    {
                        Domain = new CountryDomain(".ps", "فلسطين."),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Il, CountryIdentifier.Eg, CountryIdentifier.Jo),
                        Currency = new CountryCurrency(CurrencyIdentifier.Ils),
                        Calling = new CountryCalling(970)
                    },
                    CountryIdentifier.De => new CountryInfoData("Germany", "Federal Republic of Germany", "Deutschland", CountryIdentifier.De, SubregionIdentifier.WesternEurope)
                    {
                        Domain = new CountryDomain(".de"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.At, CountryIdentifier.Be, CountryIdentifier.Cz, CountryIdentifier.Dk, CountryIdentifier.Fr, CountryIdentifier.Lu, CountryIdentifier.Nl, CountryIdentifier.Pl, CountryIdentifier.Ch),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(49)
                    },
                    CountryIdentifier.Gh => new CountryInfoData("Ghana", "Republic of Ghana", "Ghana", CountryIdentifier.Gh, SubregionIdentifier.WesternAfrica)
                    {
                        Domain = new CountryDomain(".gh"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Bf, CountryIdentifier.Ci, CountryIdentifier.Tg),
                        Currency = new CountryCurrency(CurrencyIdentifier.Ghs),
                        Calling = new CountryCalling(233)
                    },
                    CountryIdentifier.Gi => new CountryInfoData("Gibraltar", "Gibraltar", "Gibraltar", CountryIdentifier.Gi, SubregionIdentifier.SouthernEurope)
                    {
                        Domain = new CountryDomain(".gi"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Es),
                        Currency = new CountryCurrency(CurrencyIdentifier.Gip),
                        Calling = new CountryCalling(350)
                    },
                    CountryIdentifier.Ki => new CountryInfoData("Kiribati", "Independent and Sovereign Republic of Kiribati", "Kiribati", CountryIdentifier.Ki, SubregionIdentifier.Micronesia)
                    {
                        Domain = new CountryDomain(".ki"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Aud),
                        Calling = new CountryCalling(686)
                    },
                    CountryIdentifier.Gr => new CountryInfoData("Greece", "Hellenic Republic", "ЕллЬдб", CountryIdentifier.Gr, SubregionIdentifier.SouthernEurope)
                    {
                        Domain = new CountryDomain(".gr"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Al, CountryIdentifier.Bg, CountryIdentifier.Tr, CountryIdentifier.Mk),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(30)
                    },
                    CountryIdentifier.Gl => new CountryInfoData("Greenland", "Greenland", "Kalaallit Nunaat", CountryIdentifier.Gl, SubregionIdentifier.NorthAmerica)
                    {
                        Domain = new CountryDomain(".gl"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Dkk),
                        Calling = new CountryCalling(299)
                    },
                    CountryIdentifier.Gd => new CountryInfoData("Grenada", "Grenada", "Grenada", CountryIdentifier.Gd, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".gd"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Xcd),
                        Calling = new CountryCalling(1473)
                    },
                    CountryIdentifier.Gp => new CountryInfoData("Guadeloupe", "Guadeloupe", "Guadeloupe", CountryIdentifier.Gp, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".gp"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(590)
                    },
                    CountryIdentifier.Gu => new CountryInfoData("Guam", "Guam", "Guam", CountryIdentifier.Gu, SubregionIdentifier.Micronesia)
                    {
                        Domain = new CountryDomain(".gu"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Usd),
                        Calling = new CountryCalling(1671)
                    },
                    CountryIdentifier.Gt => new CountryInfoData("Guatemala", "Republic of Guatemala", "Guatemala", CountryIdentifier.Gt, SubregionIdentifier.CentralAmerica)
                    {
                        Domain = new CountryDomain(".gt"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Bz, CountryIdentifier.Sv, CountryIdentifier.Hn, CountryIdentifier.Mx),
                        Currency = new CountryCurrency(CurrencyIdentifier.Gtq),
                        Calling = new CountryCalling(502)
                    },
                    CountryIdentifier.Gn => new CountryInfoData("Guinea", "Republic of Guinea", "Guinée", CountryIdentifier.Gn, SubregionIdentifier.WesternAfrica)
                    {
                        Domain = new CountryDomain(".gn"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Ci, CountryIdentifier.Gw, CountryIdentifier.Lr, CountryIdentifier.Ml, CountryIdentifier.Sn, CountryIdentifier.Sl),
                        Currency = new CountryCurrency(CurrencyIdentifier.Gnf),
                        Calling = new CountryCalling(224)
                    },
                    CountryIdentifier.Gy => new CountryInfoData("Guyana", "Co-operative Republic of Guyana", "Guyana", CountryIdentifier.Gy, SubregionIdentifier.SouthAmerica)
                    {
                        Domain = new CountryDomain(".gy"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Br, CountryIdentifier.Sr, CountryIdentifier.Ve),
                        Currency = new CountryCurrency(CurrencyIdentifier.Gyd),
                        Calling = new CountryCalling(592)
                    },
                    CountryIdentifier.Ht => new CountryInfoData("Haiti", "Republic of Haiti", "Haïti", CountryIdentifier.Ht, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".ht"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Do),
                        Currency = new CountryCurrency(CurrencyIdentifier.Htg, CurrencyIdentifier.Usd),
                        Calling = new CountryCalling(509)
                    },
                    CountryIdentifier.Hm => new CountryInfoData("Heard Island and McDonald Islands", "Heard Island and McDonald Islands", "Heard Island and McDonald Islands",
                        CountryIdentifier.Hm, SubregionIdentifier.Antarctic)
                    {
                        Domain = new CountryDomain(".hm", ".aq", null),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Aud),
                        Calling = new CountryCalling()
                    },
                    CountryIdentifier.Va => new CountryInfoData("Vatican City", "Vatican City State", "Stato della Città del Vaticano", CountryIdentifier.Va, SubregionIdentifier.SouthernEurope)
                    {
                        Domain = new CountryDomain(".va"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.It),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(379, 0, 0, 3906698)
                    },
                    CountryIdentifier.Hn => new CountryInfoData("Honduras", "Republic of Honduras", "Honduras", CountryIdentifier.Hn, SubregionIdentifier.CentralAmerica)
                    {
                        Domain = new CountryDomain(".hn"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Gt, CountryIdentifier.Sv, CountryIdentifier.Ni),
                        Currency = new CountryCurrency(CurrencyIdentifier.Hnl),
                        Calling = new CountryCalling(504)
                    },
                    CountryIdentifier.Hk => new CountryInfoData("Hong Kong", "Hong Kong Special Administrative Region of the People's Republic of China", "香港", CountryIdentifier.Hk, SubregionIdentifier.EasternAsia)
                    {
                        Domain = new CountryDomain(".hk", ".香港"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Cn),
                        Currency = new CountryCurrency(CurrencyIdentifier.Hkd),
                        Calling = new CountryCalling(852)
                    },
                    CountryIdentifier.Hu => new CountryInfoData("Hungary", "Hungary", "Magyarország", CountryIdentifier.Hu, SubregionIdentifier.EasternEurope)
                    {
                        Domain = new CountryDomain(".hu"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.At, CountryIdentifier.Hr, CountryIdentifier.Ro, CountryIdentifier.Rs, CountryIdentifier.Sk, CountryIdentifier.Si, CountryIdentifier.Ua),
                        Currency = new CountryCurrency(CurrencyIdentifier.Huf),
                        Calling = new CountryCalling(36)
                    },
                    CountryIdentifier.Is => new CountryInfoData("Iceland", "Iceland", "Ísland", CountryIdentifier.Is, SubregionIdentifier.NorthernEurope)
                    {
                        Domain = new CountryDomain(".is"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Isk),
                        Calling = new CountryCalling(354)
                    },
                    CountryIdentifier.In => new CountryInfoData("India", "Republic of India", "भारत", CountryIdentifier.In, SubregionIdentifier.SouthernAsia)
                    {
                        Domain = new CountryDomain(".in"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Af, CountryIdentifier.Bd, CountryIdentifier.Bt, CountryIdentifier.Mm, CountryIdentifier.Cn, CountryIdentifier.Np, CountryIdentifier.Pk, CountryIdentifier.Lk),
                        Currency = new CountryCurrency(CurrencyIdentifier.Inr),
                        Calling = new CountryCalling(91)
                    },
                    CountryIdentifier.Id => new CountryInfoData("Indonesia", "Republic of Indonesia", "Indonesia", CountryIdentifier.Id, SubregionIdentifier.SouthEasternAsia)
                    {
                        Domain = new CountryDomain(".id"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Tl, CountryIdentifier.My, CountryIdentifier.Pg),
                        Currency = new CountryCurrency(CurrencyIdentifier.Idr),
                        Calling = new CountryCalling(62)
                    },
                    CountryIdentifier.Ir => new CountryInfoData("Iran", "Islamic Republic of Iran", "ایران", CountryIdentifier.Ir, SubregionIdentifier.SouthernAsia)
                    {
                        Domain = new CountryDomain(".ir", "ایران."),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Af, CountryIdentifier.Am, CountryIdentifier.Az, CountryIdentifier.Iq, CountryIdentifier.Pk, CountryIdentifier.Tr, CountryIdentifier.Tm),
                        Currency = new CountryCurrency(CurrencyIdentifier.Irr),
                        Calling = new CountryCalling(98)
                    },
                    CountryIdentifier.Iq => new CountryInfoData("Iraq", "Republic of Iraq", "العراق", CountryIdentifier.Iq, SubregionIdentifier.WesternAsia)
                    {
                        Domain = new CountryDomain(".iq"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Ir, CountryIdentifier.Jo, CountryIdentifier.Kw, CountryIdentifier.Sa, CountryIdentifier.Sy, CountryIdentifier.Tr),
                        Currency = new CountryCurrency(CurrencyIdentifier.Iqd),
                        Calling = new CountryCalling(964)
                    },
                    CountryIdentifier.Ie => new CountryInfoData("Ireland", "Republic of Ireland", "Éire", CountryIdentifier.Ie, SubregionIdentifier.NorthernEurope)
                    {
                        Domain = new CountryDomain(".ie"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Gb),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(353)
                    },
                    CountryIdentifier.Il => new CountryInfoData("Israel", "State of Israel", "יִשְׂרָאֵל", CountryIdentifier.Il, SubregionIdentifier.WesternAsia)
                    {
                        Domain = new CountryDomain(".il"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Eg, CountryIdentifier.Jo, CountryIdentifier.Lb, CountryIdentifier.Sy),
                        Currency = new CountryCurrency(CurrencyIdentifier.Ils),
                        Calling = new CountryCalling(972)
                    },
                    CountryIdentifier.It => new CountryInfoData("Italy", "Italian Republic", "Italia", CountryIdentifier.It, SubregionIdentifier.SouthernEurope)
                    {
                        Domain = new CountryDomain(".it"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.At, CountryIdentifier.Fr, CountryIdentifier.Sm, CountryIdentifier.Si, CountryIdentifier.Ch, CountryIdentifier.Va),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(39)
                    },
                    CountryIdentifier.Ci => new CountryInfoData("Ivory Coast", "Republic of Côte d'Ivoire", "Côte d'Ivoire", CountryIdentifier.Ci, SubregionIdentifier.WesternAfrica)
                    {
                        Domain = new CountryDomain(".ci"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Bf, CountryIdentifier.Gh, CountryIdentifier.Gn, CountryIdentifier.Lr, CountryIdentifier.Ml),
                        Currency = new CountryCurrency(CurrencyIdentifier.Xof),
                        Calling = new CountryCalling(225)
                    },
                    CountryIdentifier.Jm => new CountryInfoData("Jamaica", "Jamaica", "Jamaica", CountryIdentifier.Jm, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".jm"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Jmd),
                        Calling = new CountryCalling(1876)
                    },
                    CountryIdentifier.Jp => new CountryInfoData("Japan", "Japan", "日本", CountryIdentifier.Jp, SubregionIdentifier.EasternAsia)
                    {
                        Domain = new CountryDomain(".jp", ".みんな"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Jpy),
                        Calling = new CountryCalling(81)
                    },
                    CountryIdentifier.Kz => new CountryInfoData("Kazakhstan", "Republic of Kazakhstan", "Қазақстан", CountryIdentifier.Kz, SubregionIdentifier.CentralAsia)
                    {
                        Domain = new CountryDomain(".kz", ".қаз"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Cn, CountryIdentifier.Kg, CountryIdentifier.Ru, CountryIdentifier.Tm, CountryIdentifier.Uz),
                        Currency = new CountryCurrency(CurrencyIdentifier.Kzt),
                        Calling = new CountryCalling(76, 77)
                    },
                    CountryIdentifier.Jo => new CountryInfoData("Jordan", "Hashemite Kingdom of Jordan", "الأردن", CountryIdentifier.Jo, SubregionIdentifier.WesternAsia)
                    {
                        Domain = new CountryDomain(".jo", "الاردن."),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Iq, CountryIdentifier.Il, CountryIdentifier.Sa, CountryIdentifier.Sy),
                        Currency = new CountryCurrency(CurrencyIdentifier.Jod),
                        Calling = new CountryCalling(962)
                    },
                    CountryIdentifier.Ke => new CountryInfoData("Kenya", "Republic of Kenya", "Kenya", CountryIdentifier.Ke, SubregionIdentifier.EasternAfrica)
                    {
                        Domain = new CountryDomain(".ke"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Et, CountryIdentifier.So, CountryIdentifier.Ss, CountryIdentifier.Tz, CountryIdentifier.Ug),
                        Currency = new CountryCurrency(CurrencyIdentifier.Kes),
                        Calling = new CountryCalling(254)
                    },
                    CountryIdentifier.Kp => new CountryInfoData("North Korea", "Democratic People's Republic of Korea", "북한", CountryIdentifier.Kp, SubregionIdentifier.EasternAsia)
                    {
                        Domain = new CountryDomain(".kp"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Cn, CountryIdentifier.Kr, CountryIdentifier.Ru),
                        Currency = new CountryCurrency(CurrencyIdentifier.Kpw),
                        Calling = new CountryCalling(850)
                    },
                    CountryIdentifier.Kr => new CountryInfoData("South Korea", "Republic of Korea", "대한민국", CountryIdentifier.Kr, SubregionIdentifier.EasternAsia)
                    {
                        Domain = new CountryDomain(".kr", ".한국"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Kp),
                        Currency = new CountryCurrency(CurrencyIdentifier.Krw),
                        Calling = new CountryCalling(82)
                    },
                    CountryIdentifier.Kw => new CountryInfoData("Kuwait", "State of Kuwait", "الكويت", CountryIdentifier.Kw, SubregionIdentifier.WesternAsia)
                    {
                        Domain = new CountryDomain(".kw"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Iq, CountryIdentifier.Sa),
                        Currency = new CountryCurrency(CurrencyIdentifier.Kwd),
                        Calling = new CountryCalling(965)
                    },
                    CountryIdentifier.Kg => new CountryInfoData("Kyrgyzstan", "Kyrgyz Republic", "Кыргызстан", CountryIdentifier.Kg, SubregionIdentifier.CentralAsia)
                    {
                        Domain = new CountryDomain(".kg"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Cn, CountryIdentifier.Kz, CountryIdentifier.Tj, CountryIdentifier.Uz),
                        Currency = new CountryCurrency(CurrencyIdentifier.Kgs),
                        Calling = new CountryCalling(996)
                    },
                    CountryIdentifier.La => new CountryInfoData("Laos", "Lao People's Democratic Republic", "ສປປລາວ", CountryIdentifier.La, SubregionIdentifier.SouthEasternAsia)
                    {
                        Domain = new CountryDomain(".la"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Mm, CountryIdentifier.Kh, CountryIdentifier.Cn, CountryIdentifier.Th, CountryIdentifier.Vn),
                        Currency = new CountryCurrency(CurrencyIdentifier.Lak),
                        Calling = new CountryCalling(856)
                    },
                    CountryIdentifier.Lb => new CountryInfoData("Lebanon", "Lebanese Republic", "لبنان", CountryIdentifier.Lb, SubregionIdentifier.WesternAsia)
                    {
                        Domain = new CountryDomain(".lb"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Il, CountryIdentifier.Sy),
                        Currency = new CountryCurrency(CurrencyIdentifier.Lbp),
                        Calling = new CountryCalling(961)
                    },
                    CountryIdentifier.Ls => new CountryInfoData("Lesotho", "Kingdom of Lesotho", "Lesotho", CountryIdentifier.Ls, SubregionIdentifier.SouthernAfrica)
                    {
                        Domain = new CountryDomain(".ls"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Za),
                        Currency = new CountryCurrency(CurrencyIdentifier.Lsl, CurrencyIdentifier.Xpf),
                        Calling = new CountryCalling(266)
                    },
                    CountryIdentifier.Lv => new CountryInfoData("Latvia", "Republic of Latvia", "Latvija", CountryIdentifier.Lv, SubregionIdentifier.NorthernEurope)
                    {
                        Domain = new CountryDomain(".lv"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.By, CountryIdentifier.Ee, CountryIdentifier.Lt, CountryIdentifier.Ru),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(371)
                    },
                    CountryIdentifier.Lr => new CountryInfoData("Liberia", "Republic of Liberia", "Liberia", CountryIdentifier.Lr, SubregionIdentifier.WesternAfrica)
                    {
                        Domain = new CountryDomain(".lr"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Gn, CountryIdentifier.Ci, CountryIdentifier.Sl),
                        Currency = new CountryCurrency(CurrencyIdentifier.Lrd),
                        Calling = new CountryCalling(231)
                    },
                    CountryIdentifier.Ly => new CountryInfoData("Libya", "State of Libya", "ليبيا", CountryIdentifier.Ly, SubregionIdentifier.NorthernAfrica)
                    {
                        Domain = new CountryDomain(".ly"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Dz, CountryIdentifier.Td, CountryIdentifier.Eg, CountryIdentifier.Ne, CountryIdentifier.Sd, CountryIdentifier.Tn),
                        Currency = new CountryCurrency(CurrencyIdentifier.Lyd),
                        Calling = new CountryCalling(218)
                    },
                    CountryIdentifier.Li => new CountryInfoData("Liechtenstein", "Principality of Liechtenstein", "Liechtenstein", CountryIdentifier.Li, SubregionIdentifier.WesternEurope)
                    {
                        Domain = new CountryDomain(".li"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.At, CountryIdentifier.Ch),
                        Currency = new CountryCurrency(CurrencyIdentifier.Chf),
                        Calling = new CountryCalling(423)
                    },
                    CountryIdentifier.Lt => new CountryInfoData("Lithuania", "Republic of Lithuania", "Lietuva", CountryIdentifier.Lt, SubregionIdentifier.NorthernEurope)
                    {
                        Domain = new CountryDomain(".lt"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.By, CountryIdentifier.Lv, CountryIdentifier.Pl, CountryIdentifier.Ru),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(370)
                    },
                    CountryIdentifier.Lu => new CountryInfoData("Luxembourg", "Grand Duchy of Luxembourg", "Luxembourg", CountryIdentifier.Lu, SubregionIdentifier.WesternEurope)
                    {
                        Domain = new CountryDomain(".lu"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Be, CountryIdentifier.Fr, CountryIdentifier.De),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(352)
                    },
                    CountryIdentifier.Mo => new CountryInfoData("Macau", "Macao Special Administrative Region of the People's Republic of China", "澳門", CountryIdentifier.Mo, SubregionIdentifier.EasternAsia)
                    {
                        Domain = new CountryDomain(".mo"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Cn),
                        Currency = new CountryCurrency(CurrencyIdentifier.Mop),
                        Calling = new CountryCalling(853)
                    },
                    CountryIdentifier.Mg => new CountryInfoData("Madagascar", "Republic of Madagascar", "Madagasikara", CountryIdentifier.Mg, SubregionIdentifier.EasternAfrica)
                    {
                        Domain = new CountryDomain(".mg"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Mga),
                        Calling = new CountryCalling(261)
                    },
                    CountryIdentifier.Mw => new CountryInfoData("Malawi", "Republic of Malawi", "Malawi", CountryIdentifier.Mw, SubregionIdentifier.EasternAfrica)
                    {
                        Domain = new CountryDomain(".mw"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Mz, CountryIdentifier.Tz, CountryIdentifier.Zm),
                        Currency = new CountryCurrency(CurrencyIdentifier.Mwk),
                        Calling = new CountryCalling(265)
                    },
                    CountryIdentifier.My => new CountryInfoData("Malaysia", "Malaysia", "Malaysia", CountryIdentifier.My, SubregionIdentifier.SouthEasternAsia)
                    {
                        Domain = new CountryDomain(".my"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Bn, CountryIdentifier.Id, CountryIdentifier.Th),
                        Currency = new CountryCurrency(CurrencyIdentifier.Myr),
                        Calling = new CountryCalling(60)
                    },
                    CountryIdentifier.Mv => new CountryInfoData("Maldives", "Republic of the Maldives", "Maldives", CountryIdentifier.Mv, SubregionIdentifier.SouthernAsia)
                    {
                        Domain = new CountryDomain(".mv"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Mvr),
                        Calling = new CountryCalling(960)
                    },
                    CountryIdentifier.Ml => new CountryInfoData("Mali", "Republic of Mali", "Mali", CountryIdentifier.Ml, SubregionIdentifier.WesternAfrica)
                    {
                        Domain = new CountryDomain(".ml"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Dz, CountryIdentifier.Bf, CountryIdentifier.Gn, CountryIdentifier.Ci, CountryIdentifier.Mr, CountryIdentifier.Ne, CountryIdentifier.Sn),
                        Currency = new CountryCurrency(CurrencyIdentifier.Xof),
                        Calling = new CountryCalling(223)
                    },
                    CountryIdentifier.Mt => new CountryInfoData("Malta", "Republic of Malta", "Malta", CountryIdentifier.Mt, SubregionIdentifier.SouthernEurope)
                    {
                        Domain = new CountryDomain(".mt"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(356)
                    },
                    CountryIdentifier.Mq => new CountryInfoData("Martinique", "Martinique", "Martinique", CountryIdentifier.Mq, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".mq"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(596)
                    },
                    CountryIdentifier.Mr => new CountryInfoData("Mauritania", "Islamic Republic of Mauritania", "موريتانيا", CountryIdentifier.Mr, SubregionIdentifier.WesternAfrica)
                    {
                        Domain = new CountryDomain(".mr"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Dz, CountryIdentifier.Ml, CountryIdentifier.Sn, CountryIdentifier.Eh),
                        Currency = new CountryCurrency(CurrencyIdentifier.Mro),
                        Calling = new CountryCalling(222)
                    },
                    CountryIdentifier.Mu => new CountryInfoData("Mauritius", "Republic of Mauritius", "Maurice", CountryIdentifier.Mu, SubregionIdentifier.EasternAfrica)
                    {
                        Domain = new CountryDomain(".mu"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Mur),
                        Calling = new CountryCalling(230)
                    },
                    CountryIdentifier.Mx => new CountryInfoData("Mexico", "United Mexican States", "México", CountryIdentifier.Mx, SubregionIdentifier.NorthAmerica)
                    {
                        Domain = new CountryDomain(".mx"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Bz, CountryIdentifier.Gt, CountryIdentifier.Us),
                        Currency = new CountryCurrency(CurrencyIdentifier.Mxn),
                        Calling = new CountryCalling(52)
                    },
                    CountryIdentifier.Mc => new CountryInfoData("Monaco", "Principality of Monaco", "Monaco", CountryIdentifier.Mc, SubregionIdentifier.WesternEurope)
                    {
                        Domain = new CountryDomain(".mc"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Fr),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(377)
                    },
                    CountryIdentifier.Mn => new CountryInfoData("Mongolia", "Mongolia", "Монгол улс", CountryIdentifier.Mn, SubregionIdentifier.EasternAsia)
                    {
                        Domain = new CountryDomain(".mn"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Cn, CountryIdentifier.Ru),
                        Currency = new CountryCurrency(CurrencyIdentifier.Mnt),
                        Calling = new CountryCalling(976)
                    },
                    CountryIdentifier.Md => new CountryInfoData("Moldova", "Republic of Moldova", "Moldova", CountryIdentifier.Md, SubregionIdentifier.EasternEurope)
                    {
                        Domain = new CountryDomain(".md"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Ro, CountryIdentifier.Ua),
                        Currency = new CountryCurrency(CurrencyIdentifier.Mdl),
                        Calling = new CountryCalling(373)
                    },
                    CountryIdentifier.Me => new CountryInfoData("Montenegro", "Montenegro", "Црна Гора", CountryIdentifier.Me, SubregionIdentifier.SouthernEurope)
                    {
                        Domain = new CountryDomain(".me"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Al, CountryIdentifier.Ba, CountryIdentifier.Hr, CountryIdentifier.Rs),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(382)
                    },
                    CountryIdentifier.Ms => new CountryInfoData("Montserrat", "Montserrat", "Montserrat", CountryIdentifier.Ms, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".ms"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Xcd),
                        Calling = new CountryCalling(1664)
                    },
                    CountryIdentifier.Ma => new CountryInfoData("Morocco", "Kingdom of Morocco", "المغرب", CountryIdentifier.Ma, SubregionIdentifier.NorthernAfrica)
                    {
                        Domain = new CountryDomain(".ma", "المغرب."),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Dz, CountryIdentifier.Eh, CountryIdentifier.Es),
                        Currency = new CountryCurrency(CurrencyIdentifier.Mad),
                        Calling = new CountryCalling(212)
                    },
                    CountryIdentifier.Mz => new CountryInfoData("Mozambique", "Republic of Mozambique", "Moçambique", CountryIdentifier.Mz, SubregionIdentifier.EasternAfrica)
                    {
                        Domain = new CountryDomain(".mz"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Mw, CountryIdentifier.Za, CountryIdentifier.Sz, CountryIdentifier.Tz, CountryIdentifier.Zm, CountryIdentifier.Zw),
                        Currency = new CountryCurrency(CurrencyIdentifier.Mzn),
                        Calling = new CountryCalling(258)
                    },
                    CountryIdentifier.Om => new CountryInfoData("Oman", "Sultanate of Oman", "عمان", CountryIdentifier.Om, SubregionIdentifier.WesternAsia)
                    {
                        Domain = new CountryDomain(".om"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Sa, CountryIdentifier.Ae, CountryIdentifier.Ye),
                        Currency = new CountryCurrency(CurrencyIdentifier.Omr),
                        Calling = new CountryCalling(968)
                    },
                    CountryIdentifier.Na => new CountryInfoData("Namibia", "Republic of Namibia", "Namibia", CountryIdentifier.Na, SubregionIdentifier.SouthernAfrica)
                    {
                        Domain = new CountryDomain(".na"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Ao, CountryIdentifier.Bw, CountryIdentifier.Za, CountryIdentifier.Zm),
                        Currency = new CountryCurrency(CurrencyIdentifier.Nad, CurrencyIdentifier.Xpf),
                        Calling = new CountryCalling(264)
                    },
                    CountryIdentifier.Nr => new CountryInfoData("Nauru", "Republic of Nauru", "Nauru", CountryIdentifier.Nr, SubregionIdentifier.Micronesia)
                    {
                        Domain = new CountryDomain(".nr"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Aud),
                        Calling = new CountryCalling(674)
                    },
                    CountryIdentifier.Np => new CountryInfoData("Nepal", "Federal Democratic Republic of Nepal", "नेपाल", CountryIdentifier.Np, SubregionIdentifier.SouthernAsia)
                    {
                        Domain = new CountryDomain(".np"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Cn, CountryIdentifier.In),
                        Currency = new CountryCurrency(CurrencyIdentifier.Npr),
                        Calling = new CountryCalling(977)
                    },
                    CountryIdentifier.Nl => new CountryInfoData("Netherlands", "Netherlands", "Nederland", CountryIdentifier.Nl, SubregionIdentifier.WesternEurope)
                    {
                        Domain = new CountryDomain(".nl"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Be, CountryIdentifier.De),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(31)
                    },
                    CountryIdentifier.Cw => new CountryInfoData("Curaçao", "Country of Curaçao", "Curaçao", CountryIdentifier.Cw, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".cw"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Ang),
                        Calling = new CountryCalling(5999)
                    },
                    CountryIdentifier.Aw => new CountryInfoData("Aruba", "Aruba", "Aruba", CountryIdentifier.Aw, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".aw"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Awg),
                        Calling = new CountryCalling(297)
                    },
                    CountryIdentifier.Sx => new CountryInfoData("Sint Maarten", "Sint Maarten", "Sint Maarten", CountryIdentifier.Sx, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".sx"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Mf),
                        Currency = new CountryCurrency(CurrencyIdentifier.Ang),
                        Calling = new CountryCalling(1721)
                    },
                    CountryIdentifier.Bq => new CountryInfoData("Caribbean Netherlands", "Caribbean Netherlands", "Caribbean Netherlands", CountryIdentifier.Bq, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".bq", ".nl", null),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Usd),
                        Calling = new CountryCalling(599)
                    },
                    CountryIdentifier.Nc => new CountryInfoData("New Caledonia", "New Caledonia", "Nouvelle-Calédonie", CountryIdentifier.Nc, SubregionIdentifier.Melanesia)
                    {
                        Domain = new CountryDomain(".nc"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Zar),
                        Calling = new CountryCalling(687)
                    },
                    CountryIdentifier.Vu => new CountryInfoData("Vanuatu", "Republic of Vanuatu", "Vanuatu", CountryIdentifier.Vu, SubregionIdentifier.Melanesia)
                    {
                        Domain = new CountryDomain(".vu"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Vuv),
                        Calling = new CountryCalling(678)
                    },
                    CountryIdentifier.Nz => new CountryInfoData("New Zealand", "New Zealand", "New Zealand", CountryIdentifier.Nz, SubregionIdentifier.AustraliaAndNewZealand)
                    {
                        Domain = new CountryDomain(".nz"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Nzd),
                        Calling = new CountryCalling(64)
                    },
                    CountryIdentifier.Ni => new CountryInfoData("Nicaragua", "Republic of Nicaragua", "Nicaragua", CountryIdentifier.Ni, SubregionIdentifier.CentralAmerica)
                    {
                        Domain = new CountryDomain(".ni"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Cr, CountryIdentifier.Hn),
                        Currency = new CountryCurrency(CurrencyIdentifier.Nio),
                        Calling = new CountryCalling(505)
                    },
                    CountryIdentifier.Ne => new CountryInfoData("Niger", "Republic of Niger", "Niger", CountryIdentifier.Ne, SubregionIdentifier.WesternAfrica)
                    {
                        Domain = new CountryDomain(".ne"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Dz, CountryIdentifier.Bj, CountryIdentifier.Bf, CountryIdentifier.Td, CountryIdentifier.Ly, CountryIdentifier.Ml, CountryIdentifier.Ng),
                        Currency = new CountryCurrency(CurrencyIdentifier.Xof),
                        Calling = new CountryCalling(227)
                    },
                    CountryIdentifier.Ng => new CountryInfoData("Nigeria", "Federal Republic of Nigeria", "Nigeria", CountryIdentifier.Ng, SubregionIdentifier.WesternAfrica)
                    {
                        Domain = new CountryDomain(".ng"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Bj, CountryIdentifier.Cm, CountryIdentifier.Td, CountryIdentifier.Ne),
                        Currency = new CountryCurrency(CurrencyIdentifier.Ngn),
                        Calling = new CountryCalling(234)
                    },
                    CountryIdentifier.Nu => new CountryInfoData("Niue", "Niue", "Niuē", CountryIdentifier.Nu, SubregionIdentifier.Polynesia)
                    {
                        Domain = new CountryDomain(".nu"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Nzd),
                        Calling = new CountryCalling(683)
                    },
                    CountryIdentifier.Nf => new CountryInfoData("Norfolk Island", "Territory of Norfolk Island", "Norfolk Island", CountryIdentifier.Nf, SubregionIdentifier.AustraliaAndNewZealand)
                    {
                        Domain = new CountryDomain(".nf"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Aud),
                        Calling = new CountryCalling(672)
                    },
                    CountryIdentifier.No => new CountryInfoData("Norway", "Kingdom of Norway", "Norge", CountryIdentifier.No, SubregionIdentifier.NorthernEurope)
                    {
                        Domain = new CountryDomain(".no"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Fi, CountryIdentifier.Se, CountryIdentifier.Ru),
                        Currency = new CountryCurrency(CurrencyIdentifier.Nok),
                        Calling = new CountryCalling(47)
                    },
                    CountryIdentifier.Mp => new CountryInfoData("Northern Mariana Islands", "Commonwealth of the Northern Mariana Islands", "Northern Mariana Islands", CountryIdentifier.Mp, SubregionIdentifier.Micronesia)
                    {
                        Domain = new CountryDomain(".mp"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Usd),
                        Calling = new CountryCalling(1670)
                    },
                    CountryIdentifier.Um => new CountryInfoData("United States Minor Outlying Islands", "United States Minor Outlying Islands", "United States Minor Outlying Islands",
                        CountryIdentifier.Um, SubregionIdentifier.NorthAmerica)
                    {
                        Domain = new CountryDomain(".us"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Usd),
                        Calling = new CountryCalling()
                    },
                    CountryIdentifier.Fm => new CountryInfoData("Micronesia", "Federated States of Micronesia", "Micronesia", CountryIdentifier.Fm, SubregionIdentifier.Micronesia)
                    {
                        Domain = new CountryDomain(".fm"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Usd),
                        Calling = new CountryCalling(691)
                    },
                    CountryIdentifier.Mh => new CountryInfoData("Marshall Islands", "Republic of the Marshall Islands", "M̧ajeļ", CountryIdentifier.Mh, SubregionIdentifier.Micronesia)
                    {
                        Domain = new CountryDomain(".mh"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Usd),
                        Calling = new CountryCalling(692)
                    },
                    CountryIdentifier.Pw => new CountryInfoData("Palau", "Republic of Palau", "Palau", CountryIdentifier.Pw, SubregionIdentifier.Micronesia)
                    {
                        Domain = new CountryDomain(".pw"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Usd),
                        Calling = new CountryCalling(680)
                    },
                    CountryIdentifier.Pk => new CountryInfoData("Pakistan", "Islamic Republic of Pakistan", "Pakistan", CountryIdentifier.Pk, SubregionIdentifier.SouthernAsia)
                    {
                        Domain = new CountryDomain(".pk"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Af, CountryIdentifier.Cn, CountryIdentifier.In, CountryIdentifier.Ir),
                        Currency = new CountryCurrency(CurrencyIdentifier.Pkr),
                        Calling = new CountryCalling(92)
                    },
                    CountryIdentifier.Pa => new CountryInfoData("Panama", "Republic of Panama", "Panamá", CountryIdentifier.Pa, SubregionIdentifier.CentralAmerica)
                    {
                        Domain = new CountryDomain(".pa"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Co, CountryIdentifier.Cr),
                        Currency = new CountryCurrency(CurrencyIdentifier.Pab, CurrencyIdentifier.Usd),
                        Calling = new CountryCalling(507)
                    },
                    CountryIdentifier.Pg => new CountryInfoData("Papua New Guinea", "Independent State of Papua New Guinea", "Papua Niugini", CountryIdentifier.Pg, SubregionIdentifier.Melanesia)
                    {
                        Domain = new CountryDomain(".pg"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Id),
                        Currency = new CountryCurrency(CurrencyIdentifier.Pgk),
                        Calling = new CountryCalling(675)
                    },
                    CountryIdentifier.Py => new CountryInfoData("Paraguay", "Republic of Paraguay", "Paraguay", CountryIdentifier.Py, SubregionIdentifier.SouthAmerica)
                    {
                        Domain = new CountryDomain(".py"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Ar, CountryIdentifier.Bo, CountryIdentifier.Br),
                        Currency = new CountryCurrency(CurrencyIdentifier.Pyg),
                        Calling = new CountryCalling(595)
                    },
                    CountryIdentifier.Pe => new CountryInfoData("Peru", "Republic of Peru", "Perú", CountryIdentifier.Pe, SubregionIdentifier.SouthAmerica)
                    {
                        Domain = new CountryDomain(".pe"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Bo, CountryIdentifier.Br, CountryIdentifier.Cl, CountryIdentifier.Co, CountryIdentifier.Ec),
                        Currency = new CountryCurrency(CurrencyIdentifier.Pen),
                        Calling = new CountryCalling(51)
                    },
                    CountryIdentifier.Ph => new CountryInfoData("Philippines", "Republic of the Philippines", "Pilipinas", CountryIdentifier.Ph, SubregionIdentifier.SouthEasternAsia)
                    {
                        Domain = new CountryDomain(".ph"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Php),
                        Calling = new CountryCalling(63)
                    },
                    CountryIdentifier.Pn => new CountryInfoData("Pitcairn Islands", "Pitcairn Group of Islands", "Pitcairn Islands", CountryIdentifier.Pn, SubregionIdentifier.Polynesia)
                    {
                        Domain = new CountryDomain(".pn"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Nzd),
                        Calling = new CountryCalling(64)
                    },
                    CountryIdentifier.Pl => new CountryInfoData("Poland", "Republic of Poland", "Polska", CountryIdentifier.Pl, SubregionIdentifier.EasternEurope)
                    {
                        Domain = new CountryDomain(".pl"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.By, CountryIdentifier.Cz, CountryIdentifier.De, CountryIdentifier.Lt, CountryIdentifier.Ru, CountryIdentifier.Sk, CountryIdentifier.Ua),
                        Currency = new CountryCurrency(CurrencyIdentifier.Pln),
                        Calling = new CountryCalling(48)
                    },
                    CountryIdentifier.Pt => new CountryInfoData("Portugal", "Portuguese Republic", "Portugal", CountryIdentifier.Pt, SubregionIdentifier.SouthernEurope)
                    {
                        Domain = new CountryDomain(".pt"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Es),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(351)
                    },
                    CountryIdentifier.Gw => new CountryInfoData("Guinea-Bissau", "Republic of Guinea-Bissau", "Guiné-Bissau", CountryIdentifier.Gw, SubregionIdentifier.WesternAfrica)
                    {
                        Domain = new CountryDomain(".gw"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Gn, CountryIdentifier.Sn),
                        Currency = new CountryCurrency(CurrencyIdentifier.Xof),
                        Calling = new CountryCalling(245)
                    },
                    CountryIdentifier.Tl => new CountryInfoData("Timor-Leste", "Democratic Republic of Timor-Leste", "Timor-Leste", CountryIdentifier.Tl, SubregionIdentifier.SouthEasternAsia)
                    {
                        Domain = new CountryDomain(".tl"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Id),
                        Currency = new CountryCurrency(CurrencyIdentifier.Usd),
                        Calling = new CountryCalling(670)
                    },
                    CountryIdentifier.Pr => new CountryInfoData("Puerto Rico", "Commonwealth of Puerto Rico", "Puerto Rico", CountryIdentifier.Pr, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".pr"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Usd),
                        Calling = new CountryCalling(1787, 1939)
                    },
                    CountryIdentifier.Qa => new CountryInfoData("Qatar", "State of Qatar", "قطر", CountryIdentifier.Qa, SubregionIdentifier.WesternAsia)
                    {
                        Domain = new CountryDomain(".qa", "قطر."),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Sa),
                        Currency = new CountryCurrency(CurrencyIdentifier.Qar),
                        Calling = new CountryCalling(974)
                    },
                    CountryIdentifier.Re => new CountryInfoData("Réunion", "Réunion Island", "La Réunion", CountryIdentifier.Re, SubregionIdentifier.EasternAfrica)
                    {
                        Domain = new CountryDomain(".re"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(262)
                    },
                    CountryIdentifier.Ro => new CountryInfoData("Romania", "Romania", "România", CountryIdentifier.Ro, SubregionIdentifier.EasternEurope)
                    {
                        Domain = new CountryDomain(".ro"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Bg, CountryIdentifier.Hu, CountryIdentifier.Md, CountryIdentifier.Rs, CountryIdentifier.Ua),
                        Currency = new CountryCurrency(CurrencyIdentifier.Ron),
                        Calling = new CountryCalling(40)
                    },
                    CountryIdentifier.Ru => new CountryInfoData("Russia", "Russian Federation", "Россия", CountryIdentifier.Ru, SubregionIdentifier.EasternEurope)
                    {
                        Domain = new CountryDomain(".ru", ".su", ".рф"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Az, CountryIdentifier.By, CountryIdentifier.Cn, CountryIdentifier.Ee, CountryIdentifier.Fi, CountryIdentifier.Ge, CountryIdentifier.Kz, CountryIdentifier.Kp, CountryIdentifier.Lv, CountryIdentifier.Lt, CountryIdentifier.Mn, CountryIdentifier.No, CountryIdentifier.Pl, CountryIdentifier.Ua),
                        Currency = new CountryCurrency(CurrencyIdentifier.Rub),
                        Calling = new CountryCalling(7)
                    },
                    CountryIdentifier.Rw => new CountryInfoData("Rwanda", "Republic of Rwanda", "Rwanda", CountryIdentifier.Rw, SubregionIdentifier.EasternAfrica)
                    {
                        Domain = new CountryDomain(".rw"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Bi, CountryIdentifier.Cd, CountryIdentifier.Tz, CountryIdentifier.Ug),
                        Currency = new CountryCurrency(CurrencyIdentifier.Rwf),
                        Calling = new CountryCalling(250)
                    },
                    CountryIdentifier.Bl => new CountryInfoData("Saint Barthélemy", "Collectivity of Saint BarthélemySaint Barthélemy", "Saint-Barthélemy", CountryIdentifier.Bl, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".bl"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(590)
                    },
                    CountryIdentifier.Sh => new CountryInfoData("Saint Helena, Ascension and Tristan da Cunha", "Saint Helena, Ascension and Tristan da Cunha", "Saint Helena",
                        CountryIdentifier.Sh, SubregionIdentifier.WesternAfrica)
                    {
                        Domain = new CountryDomain(".sh", ".ac", null),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Ar, CountryIdentifier.Br, CountryIdentifier.Cl, CountryIdentifier.Py, CountryIdentifier.Pe),
                        Currency = new CountryCurrency(CurrencyIdentifier.Shp, CurrencyIdentifier.Gbp),
                        Calling = new CountryCalling(290, 247)
                    },
                    CountryIdentifier.Kn => new CountryInfoData("Saint Kitts and Nevis", "Federation of Saint Christopher and Nevisa", "Saint Kitts and Nevis", CountryIdentifier.Kn, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".kn"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Xcd),
                        Calling = new CountryCalling(1869)
                    },
                    CountryIdentifier.Ai => new CountryInfoData("Anguilla", "Anguilla", "Anguilla", CountryIdentifier.Ai, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".ai"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Xcd),
                        Calling = new CountryCalling(1264)
                    },
                    CountryIdentifier.Lc => new CountryInfoData("Saint Lucia", "Saint Lucia", "Saint Lucia", CountryIdentifier.Lc, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".lc"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Xcd),
                        Calling = new CountryCalling(1758)
                    },
                    CountryIdentifier.Mf => new CountryInfoData("Saint Martin", "Saint Martin", "Saint Martin", CountryIdentifier.Mf, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".fr", ".gp", null),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Sx),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(590)
                    },
                    CountryIdentifier.Pm => new CountryInfoData("Saint Pierre and Miquelon", "Saint Pierre and Miquelon", "Saint Pierre et Miquelon", CountryIdentifier.Pm, SubregionIdentifier.NorthAmerica)
                    {
                        Domain = new CountryDomain(".pm"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(508)
                    },
                    CountryIdentifier.Vc => new CountryInfoData("Saint Vincent and the Grenadines", "Saint Vincent and the Grenadines", "Saint Vincent and the Grenadines",
                        CountryIdentifier.Vc, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".vc"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Xcd),
                        Calling = new CountryCalling(1784)
                    },
                    CountryIdentifier.Sm => new CountryInfoData("San Marino", "Most Serene Republic of San Marino", "San Marino", CountryIdentifier.Sm, SubregionIdentifier.SouthernEurope)
                    {
                        Domain = new CountryDomain(".sm"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.It),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(378)
                    },
                    CountryIdentifier.St => new CountryInfoData("São Tomé and Príncipe", "Democratic Republic of São Tomé and Príncipe", "São Tomé e Príncipe", CountryIdentifier.St, SubregionIdentifier.MiddleAfrica)
                    {
                        Domain = new CountryDomain(".st"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Std),
                        Calling = new CountryCalling(239)
                    },
                    CountryIdentifier.Sa => new CountryInfoData("Saudi Arabia", "Kingdom of Saudi Arabia", "العربية السعودية", CountryIdentifier.Sa, SubregionIdentifier.WesternAsia)
                    {
                        Domain = new CountryDomain(".sa", ".السعودية"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Iq, CountryIdentifier.Jo, CountryIdentifier.Kw, CountryIdentifier.Om, CountryIdentifier.Qa, CountryIdentifier.Ae, CountryIdentifier.Ye),
                        Currency = new CountryCurrency(CurrencyIdentifier.Sar),
                        Calling = new CountryCalling(966)
                    },
                    CountryIdentifier.Sn => new CountryInfoData("Senegal", "Republic of Senegal", "Sénégal", CountryIdentifier.Sn, SubregionIdentifier.WesternAfrica)
                    {
                        Domain = new CountryDomain(".sn"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Gm, CountryIdentifier.Gn, CountryIdentifier.Gw, CountryIdentifier.Ml, CountryIdentifier.Mr),
                        Currency = new CountryCurrency(CurrencyIdentifier.Xof),
                        Calling = new CountryCalling(221)
                    },
                    CountryIdentifier.Rs => new CountryInfoData("Serbia", "Republic of Serbia", "Србија", CountryIdentifier.Rs, SubregionIdentifier.SouthernEurope)
                    {
                        Domain = new CountryDomain(".rs", ".срб"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Ba, CountryIdentifier.Bg, CountryIdentifier.Hr, CountryIdentifier.Hu, CountryIdentifier.Mk, CountryIdentifier.Me, CountryIdentifier.Ro),
                        Currency = new CountryCurrency(CurrencyIdentifier.Rsd),
                        Calling = new CountryCalling(381)
                    },
                    CountryIdentifier.Sc => new CountryInfoData("Seychelles", "Republic of Seychelles", "Seychelles", CountryIdentifier.Sc, SubregionIdentifier.EasternAfrica)
                    {
                        Domain = new CountryDomain(".sc"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Scr),
                        Calling = new CountryCalling(248)
                    },
                    CountryIdentifier.Sl => new CountryInfoData("Sierra Leone", "Republic of Sierra Leone", "Sierra Leone", CountryIdentifier.Sl, SubregionIdentifier.WesternAfrica)
                    {
                        Domain = new CountryDomain(".sl"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Gn, CountryIdentifier.Lr),
                        Currency = new CountryCurrency(CurrencyIdentifier.Sll),
                        Calling = new CountryCalling(232)
                    },
                    CountryIdentifier.Sg => new CountryInfoData("Singapore", "Republic of Singapore", "Singapore", CountryIdentifier.Sg, SubregionIdentifier.SouthEasternAsia)
                    {
                        Domain = new CountryDomain(".sg", null, ".新加坡", ".சிங்கப்பூர்"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Sgd),
                        Calling = new CountryCalling(65)
                    },
                    CountryIdentifier.Sk => new CountryInfoData("Slovakia", "Slovak Republic", "Slovensko", CountryIdentifier.Sk, SubregionIdentifier.EasternEurope)
                    {
                        Domain = new CountryDomain(".sk"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.At, CountryIdentifier.Cz, CountryIdentifier.Hu, CountryIdentifier.Pl, CountryIdentifier.Ua),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(421)
                    },
                    CountryIdentifier.Vn => new CountryInfoData("Vietnam", "Socialist Republic of Vietnam", "Việt Nam", CountryIdentifier.Vn, SubregionIdentifier.SouthEasternAsia)
                    {
                        Domain = new CountryDomain(".vn"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Kh, CountryIdentifier.Cn, CountryIdentifier.La),
                        Currency = new CountryCurrency(CurrencyIdentifier.Vnd),
                        Calling = new CountryCalling(84)
                    },
                    CountryIdentifier.Si => new CountryInfoData("Slovenia", "Republic of Slovenia", "Slovenija", CountryIdentifier.Si, SubregionIdentifier.SouthernEurope)
                    {
                        Domain = new CountryDomain(".si"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.At, CountryIdentifier.Hr, CountryIdentifier.It, CountryIdentifier.Hu),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(386)
                    },
                    CountryIdentifier.So => new CountryInfoData("Somalia", "Federal Republic of Somalia", "Soomaaliya", CountryIdentifier.So, SubregionIdentifier.EasternAfrica)
                    {
                        Domain = new CountryDomain(".so"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Dj, CountryIdentifier.Et, CountryIdentifier.Ke),
                        Currency = new CountryCurrency(CurrencyIdentifier.Sos),
                        Calling = new CountryCalling(252)
                    },
                    CountryIdentifier.Za => new CountryInfoData("South Africa", "Republic of South Africa", "South Africa", CountryIdentifier.Za, SubregionIdentifier.SouthernAfrica)
                    {
                        Domain = new CountryDomain(".za"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Bw, CountryIdentifier.Ls, CountryIdentifier.Mz, CountryIdentifier.Na, CountryIdentifier.Sz, CountryIdentifier.Zw),
                        Currency = new CountryCurrency(CurrencyIdentifier.Xpf),
                        Calling = new CountryCalling(27)
                    },
                    CountryIdentifier.Zw => new CountryInfoData("Zimbabwe", "Republic of Zimbabwe", "Zimbabwe", CountryIdentifier.Zw, SubregionIdentifier.EasternAfrica)
                    {
                        Domain = new CountryDomain(".zw"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Bw, CountryIdentifier.Mz, CountryIdentifier.Za, CountryIdentifier.Zm),
                        Currency = new CountryCurrency(CurrencyIdentifier.Zwl),
                        Calling = new CountryCalling(263)
                    },
                    CountryIdentifier.Es => new CountryInfoData("Spain", "Kingdom of Spain", "España", CountryIdentifier.Es, SubregionIdentifier.SouthernEurope)
                    {
                        Domain = new CountryDomain(".es"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Ad, CountryIdentifier.Fr, CountryIdentifier.Gi, CountryIdentifier.Pt, CountryIdentifier.Ma),
                        Currency = new CountryCurrency(CurrencyIdentifier.Eur),
                        Calling = new CountryCalling(34)
                    },
                    CountryIdentifier.Ss => new CountryInfoData("South Sudan", "Republic of South Sudan", "South Sudan", CountryIdentifier.Ss, SubregionIdentifier.MiddleAfrica)
                    {
                        Domain = new CountryDomain(".ss"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Cf, CountryIdentifier.Cd, CountryIdentifier.Et, CountryIdentifier.Ke, CountryIdentifier.Sd, CountryIdentifier.Ug),
                        Currency = new CountryCurrency(CurrencyIdentifier.Ssp),
                        Calling = new CountryCalling(211)
                    },
                    CountryIdentifier.Sd => new CountryInfoData("Sudan", "Republic of the Sudan", "السودان", CountryIdentifier.Sd, SubregionIdentifier.NorthernAfrica)
                    {
                        Domain = new CountryDomain(".sd"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Cf, CountryIdentifier.Td, CountryIdentifier.Eg, CountryIdentifier.Er, CountryIdentifier.Et, CountryIdentifier.Ly, CountryIdentifier.Ss),
                        Currency = new CountryCurrency(CurrencyIdentifier.Sdg),
                        Calling = new CountryCalling(249)
                    },
                    CountryIdentifier.Eh => new CountryInfoData("Western Sahara", "Sahrawi Arab Democratic Republic", "الصحراء الغربية", CountryIdentifier.Eh, SubregionIdentifier.NorthernAfrica)
                    {
                        Domain = new CountryDomain(".eh"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Dz, CountryIdentifier.Mr, CountryIdentifier.Ma),
                        Currency = new CountryCurrency(CurrencyIdentifier.Mad, CurrencyIdentifier.Dzd, CurrencyIdentifier.Mro),
                        Calling = new CountryCalling(212)
                    },
                    CountryIdentifier.Sr => new CountryInfoData("Suriname", "Republic of Suriname", "Suriname", CountryIdentifier.Sr, SubregionIdentifier.SouthAmerica)
                    {
                        Domain = new CountryDomain(".sr"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Br, CountryIdentifier.Gf, CountryIdentifier.Gy),
                        Currency = new CountryCurrency(CurrencyIdentifier.Srd),
                        Calling = new CountryCalling(597)
                    },
                    CountryIdentifier.Sj => new CountryInfoData("Svalbard and Jan Mayen", "Svalbard og Jan Mayen", "Svalbard og Jan Mayen", CountryIdentifier.Sj, SubregionIdentifier.NorthernEurope)
                    {
                        Domain = new CountryDomain(".sj"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Nok),
                        Calling = new CountryCalling(4779)
                    },
                    CountryIdentifier.Sz => new CountryInfoData("Eswatini", "Kingdom of Swaziland", "Swaziland", CountryIdentifier.Sz, SubregionIdentifier.SouthernAfrica)
                    {
                        Domain = new CountryDomain(".sz"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Mz, CountryIdentifier.Za),
                        Currency = new CountryCurrency(CurrencyIdentifier.Szl),
                        Calling = new CountryCalling(268)
                    },
                    CountryIdentifier.Se => new CountryInfoData("Sweden", "Kingdom of Sweden", "Sverige", CountryIdentifier.Se, SubregionIdentifier.NorthernEurope)
                    {
                        Domain = new CountryDomain(".se"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Fi, CountryIdentifier.No),
                        Currency = new CountryCurrency(CurrencyIdentifier.Sek),
                        Calling = new CountryCalling(46)
                    },
                    CountryIdentifier.Ch => new CountryInfoData("Switzerland", "Swiss Confederation", "Schweiz", CountryIdentifier.Ch, SubregionIdentifier.WesternEurope)
                    {
                        Domain = new CountryDomain(".ch"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.At, CountryIdentifier.Fr, CountryIdentifier.It, CountryIdentifier.Li, CountryIdentifier.De),
                        Currency = new CountryCurrency(CurrencyIdentifier.Chf),
                        Calling = new CountryCalling(41)
                    },
                    CountryIdentifier.Sy => new CountryInfoData("Syria", "Syrian Arab Republic", "سوريا", CountryIdentifier.Sy, SubregionIdentifier.WesternAsia)
                    {
                        Domain = new CountryDomain(".sy", "سوريا."),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Iq, CountryIdentifier.Il, CountryIdentifier.Jo, CountryIdentifier.Lb, CountryIdentifier.Tr),
                        Currency = new CountryCurrency(CurrencyIdentifier.Syp),
                        Calling = new CountryCalling(963)
                    },
                    CountryIdentifier.Tj => new CountryInfoData("Tajikistan", "Republic of Tajikistan", "Тоҷикистон", CountryIdentifier.Tj, SubregionIdentifier.CentralAsia)
                    {
                        Domain = new CountryDomain(".tj"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Af, CountryIdentifier.Cn, CountryIdentifier.Kg, CountryIdentifier.Uz),
                        Currency = new CountryCurrency(CurrencyIdentifier.Tjs),
                        Calling = new CountryCalling(992)
                    },
                    CountryIdentifier.Th => new CountryInfoData("Thailand", "Kingdom of Thailand", "ประเทศไทย", CountryIdentifier.Th, SubregionIdentifier.SouthEasternAsia)
                    {
                        Domain = new CountryDomain(".th", ".ไทย"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Mm, CountryIdentifier.Kh, CountryIdentifier.La, CountryIdentifier.My),
                        Currency = new CountryCurrency(CurrencyIdentifier.Thb),
                        Calling = new CountryCalling(66)
                    },
                    CountryIdentifier.Tg => new CountryInfoData("Togo", "Togolese Republic", "Togo", CountryIdentifier.Tg, SubregionIdentifier.WesternAfrica)
                    {
                        Domain = new CountryDomain(".tg"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Bj, CountryIdentifier.Bf, CountryIdentifier.Gh),
                        Currency = new CountryCurrency(CurrencyIdentifier.Xof),
                        Calling = new CountryCalling(228)
                    },
                    CountryIdentifier.Tk => new CountryInfoData("Tokelau", "Tokelau", "Tokelau", CountryIdentifier.Tk, SubregionIdentifier.Polynesia)
                    {
                        Domain = new CountryDomain(".tk"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Nzd),
                        Calling = new CountryCalling(690)
                    },
                    CountryIdentifier.To => new CountryInfoData("Tonga", "Kingdom of Tonga", "Tonga", CountryIdentifier.To, SubregionIdentifier.Polynesia)
                    {
                        Domain = new CountryDomain(".to"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Top),
                        Calling = new CountryCalling(676)
                    },
                    CountryIdentifier.Tt => new CountryInfoData("Trinidad and Tobago", "Republic of Trinidad and Tobago", "Trinidad and Tobago", CountryIdentifier.Tt, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".tt"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Ttd),
                        Calling = new CountryCalling(1868)
                    },
                    CountryIdentifier.Ae => new CountryInfoData("United Arab Emirates", "United Arab Emirates", "دولة الإمارات العربية المتحدة", CountryIdentifier.Ae, SubregionIdentifier.WesternAsia)
                    {
                        Domain = new CountryDomain(".ae", "امارات."),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Om, CountryIdentifier.Sa),
                        Currency = new CountryCurrency(CurrencyIdentifier.Aed),
                        Calling = new CountryCalling(971)
                    },
                    CountryIdentifier.Tn => new CountryInfoData("Tunisia", "Tunisian Republic", "تونس", CountryIdentifier.Tn, SubregionIdentifier.NorthernAfrica)
                    {
                        Domain = new CountryDomain(".tn"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Dz, CountryIdentifier.Ly),
                        Currency = new CountryCurrency(CurrencyIdentifier.Tnd),
                        Calling = new CountryCalling(216)
                    },
                    CountryIdentifier.Tr => new CountryInfoData("Turkey", "Republic of Turkey", "Türkiye", CountryIdentifier.Tr, SubregionIdentifier.WesternAsia)
                    {
                        Domain = new CountryDomain(".tr"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Am, CountryIdentifier.Az, CountryIdentifier.Bg, CountryIdentifier.Ge, CountryIdentifier.Gr, CountryIdentifier.Ir, CountryIdentifier.Iq, CountryIdentifier.Sy),
                        Currency = new CountryCurrency(CurrencyIdentifier.Try),
                        Calling = new CountryCalling(90)
                    },
                    CountryIdentifier.Tm => new CountryInfoData("Turkmenistan", "Turkmenistan", "Türkmenistan", CountryIdentifier.Tm, SubregionIdentifier.CentralAsia)
                    {
                        Domain = new CountryDomain(".tm"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Af, CountryIdentifier.Ir, CountryIdentifier.Kz, CountryIdentifier.Uz),
                        Currency = new CountryCurrency(CurrencyIdentifier.Tmt),
                        Calling = new CountryCalling(993)
                    },
                    CountryIdentifier.Tc => new CountryInfoData("Turks and Caicos Islands", "Turks and Caicos Islands", "Turks and Caicos Islands", CountryIdentifier.Tc, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".tc"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Usd),
                        Calling = new CountryCalling(1649)
                    },
                    CountryIdentifier.Tv => new CountryInfoData("Tuvalu", "Tuvalu", "Tuvalu", CountryIdentifier.Tv, SubregionIdentifier.Polynesia)
                    {
                        Domain = new CountryDomain(".tv"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Aud),
                        Calling = new CountryCalling(688)
                    },
                    CountryIdentifier.Ug => new CountryInfoData("Uganda", "Republic of Uganda", "Uganda", CountryIdentifier.Ug, SubregionIdentifier.EasternAfrica)
                    {
                        Domain = new CountryDomain(".ug"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Cd, CountryIdentifier.Ke, CountryIdentifier.Rw, CountryIdentifier.Ss, CountryIdentifier.Tz),
                        Currency = new CountryCurrency(CurrencyIdentifier.Ugx),
                        Calling = new CountryCalling(256)
                    },
                    CountryIdentifier.Ua => new CountryInfoData("Ukraine", "Ukraine", "Україна", CountryIdentifier.Ua, SubregionIdentifier.EasternEurope)
                    {
                        Domain = new CountryDomain(".ua", ".укр"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.By, CountryIdentifier.Hu, CountryIdentifier.Md, CountryIdentifier.Pl, CountryIdentifier.Ro, CountryIdentifier.Ru, CountryIdentifier.Sk),
                        Currency = new CountryCurrency(CurrencyIdentifier.Uah),
                        Calling = new CountryCalling(380)
                    },
                    CountryIdentifier.Mk => new CountryInfoData("North Macedonia", "Republic of North Macedonia", "Северна Македонија", CountryIdentifier.Mk, SubregionIdentifier.SouthernEurope)
                    {
                        Domain = new CountryDomain(".mk"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Al, CountryIdentifier.Bg, CountryIdentifier.Gr, CountryIdentifier.Rs),
                        Currency = new CountryCurrency(CurrencyIdentifier.Mkd),
                        Calling = new CountryCalling(389)
                    },
                    CountryIdentifier.Eg => new CountryInfoData("Egypt", "Arab Republic of Egypt", "مصر", CountryIdentifier.Eg, SubregionIdentifier.NorthernAfrica)
                    {
                        Domain = new CountryDomain(".eg", ".مصر"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Il, CountryIdentifier.Ly, CountryIdentifier.Sd),
                        Currency = new CountryCurrency(CurrencyIdentifier.Egp),
                        Calling = new CountryCalling(20)
                    },
                    CountryIdentifier.Gb => new CountryInfoData("United Kingdom", "United Kingdom of Great Britain and Northern Ireland", "United Kingdom", CountryIdentifier.Gb, SubregionIdentifier.NorthernEurope)
                    {
                        Domain = new CountryDomain(".uk"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Ie),
                        Currency = new CountryCurrency(CurrencyIdentifier.Gbp),
                        Calling = new CountryCalling(44)
                    },
                    CountryIdentifier.Gg => new CountryInfoData("Guernsey", "Bailiwick of Guernsey", "Guernsey", CountryIdentifier.Gg, SubregionIdentifier.NorthernEurope)
                    {
                        Domain = new CountryDomain(".gg"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Gbp),
                        Calling = new CountryCalling(44)
                    },
                    CountryIdentifier.Je => new CountryInfoData("Jersey", "Bailiwick of Jersey", "Jersey", CountryIdentifier.Je, SubregionIdentifier.NorthernEurope)
                    {
                        Domain = new CountryDomain(".je"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Gbp),
                        Calling = new CountryCalling(44)
                    },
                    CountryIdentifier.Im => new CountryInfoData("Isle of Man", "Isle of Man", "Isle of Man", CountryIdentifier.Im, SubregionIdentifier.NorthernEurope)
                    {
                        Domain = new CountryDomain(".im"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Gbp),
                        Calling = new CountryCalling(44)
                    },
                    CountryIdentifier.Tz => new CountryInfoData("Tanzania", "United Republic of Tanzania", "Tanzania", CountryIdentifier.Tz, SubregionIdentifier.EasternAfrica)
                    {
                        Domain = new CountryDomain(".tz"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Bi, CountryIdentifier.Cd, CountryIdentifier.Ke, CountryIdentifier.Mw, CountryIdentifier.Mz, CountryIdentifier.Rw, CountryIdentifier.Ug, CountryIdentifier.Zm),
                        Currency = new CountryCurrency(CurrencyIdentifier.Tzs),
                        Calling = new CountryCalling(255)
                    },
                    CountryIdentifier.Us => new CountryInfoData("United States", "United States of America", "United States", CountryIdentifier.Us, SubregionIdentifier.NorthAmerica)
                    {
                        Domain = new CountryDomain(".us"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Ca, CountryIdentifier.Mx),
                        Currency = new CountryCurrency(CurrencyIdentifier.Usd),
                        Calling = new CountryCalling(1)
                    },
                    CountryIdentifier.Vi => new CountryInfoData("United States Virgin Islands", "Virgin Islands of the United States", "Virgin Islands of the United States",
                        CountryIdentifier.Vi, SubregionIdentifier.Caribbean)
                    {
                        Domain = new CountryDomain(".vi"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Usd),
                        Calling = new CountryCalling(1340)
                    },
                    CountryIdentifier.Bf => new CountryInfoData("Burkina Faso", "Burkina Faso", "Burkina Faso", CountryIdentifier.Bf, SubregionIdentifier.WesternAfrica)
                    {
                        Domain = new CountryDomain(".bf"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Bj, CountryIdentifier.Ci, CountryIdentifier.Gh, CountryIdentifier.Ml, CountryIdentifier.Ne, CountryIdentifier.Tg),
                        Currency = new CountryCurrency(CurrencyIdentifier.Xof),
                        Calling = new CountryCalling(226)
                    },
                    CountryIdentifier.Uy => new CountryInfoData("Uruguay", "Oriental Republic of Uruguay", "Uruguay", CountryIdentifier.Uy, SubregionIdentifier.SouthAmerica)
                    {
                        Domain = new CountryDomain(".uy"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Ar, CountryIdentifier.Br),
                        Currency = new CountryCurrency(CurrencyIdentifier.Uyu),
                        Calling = new CountryCalling(598)
                    },
                    CountryIdentifier.Uz => new CountryInfoData("Uzbekistan", "Republic of Uzbekistan", "O‘zbekiston", CountryIdentifier.Uz, SubregionIdentifier.CentralAsia)
                    {
                        Domain = new CountryDomain(".uz"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Af, CountryIdentifier.Kz, CountryIdentifier.Kg, CountryIdentifier.Tj, CountryIdentifier.Tm),
                        Currency = new CountryCurrency(CurrencyIdentifier.Uzs),
                        Calling = new CountryCalling(998)
                    },
                    CountryIdentifier.Ve => new CountryInfoData("Venezuela", "Bolivarian Republic of Venezuela", "Venezuela", CountryIdentifier.Ve, SubregionIdentifier.SouthAmerica)
                    {
                        Domain = new CountryDomain(".ve"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Br, CountryIdentifier.Co, CountryIdentifier.Gy),
                        Currency = new CountryCurrency(CurrencyIdentifier.Vef),
                        Calling = new CountryCalling(58)
                    },
                    CountryIdentifier.Wf => new CountryInfoData("Wallis and Futuna", "Territory of the Wallis and Futuna Islands", "Wallis et Futuna", CountryIdentifier.Wf, SubregionIdentifier.Polynesia)
                    {
                        Domain = new CountryDomain(".wf"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Zar),
                        Calling = new CountryCalling(681)
                    },
                    CountryIdentifier.Ws => new CountryInfoData("Samoa", "Independent State of Samoa", "Samoa", CountryIdentifier.Ws, SubregionIdentifier.Polynesia)
                    {
                        Domain = new CountryDomain(".ws"),
                        Border = ImmutableHashSet<CountryIdentifier>.Empty,
                        Currency = new CountryCurrency(CurrencyIdentifier.Wst),
                        Calling = new CountryCalling(685)
                    },
                    CountryIdentifier.Ye => new CountryInfoData("Yemen", "Republic of Yemen", "اليَمَن", CountryIdentifier.Ye, SubregionIdentifier.WesternAsia)
                    {
                        Domain = new CountryDomain(".ye"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Om, CountryIdentifier.Sa),
                        Currency = new CountryCurrency(CurrencyIdentifier.Yer),
                        Calling = new CountryCalling(967)
                    },
                    CountryIdentifier.Zm => new CountryInfoData("Zambia", "Republic of Zambia", "Zambia", CountryIdentifier.Zm, SubregionIdentifier.EasternAfrica)
                    {
                        Domain = new CountryDomain(".zm"),
                        Border = ImmutableHashSet.Create(CountryIdentifier.Ao, CountryIdentifier.Bw, CountryIdentifier.Cd, CountryIdentifier.Mw, CountryIdentifier.Mz, CountryIdentifier.Na, CountryIdentifier.Tz, CountryIdentifier.Zw),
                        Currency = new CountryCurrency(CurrencyIdentifier.Zmw),
                        Calling = new CountryCalling(260)
                    },
                    _ => throw new EnumUndefinedOrNotSupportedException<CountryIdentifier>(identifier, nameof(identifier), null)
                };
            }
        }
    }
}